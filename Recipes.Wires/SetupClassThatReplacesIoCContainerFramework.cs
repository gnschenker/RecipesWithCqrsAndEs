using System;
using Lokad.Cqrs;
using Lokad.Cqrs.AtomicStorage;
using Lokad.Cqrs.Build;
using Lokad.Cqrs.Partition;
using Lokad.Cqrs.StreamingStorage;
using Lokad.Cqrs.TapeStorage;
using Lokad.Cqrs.TimerService;
using Recipes.Contracts;
using Recipes.Projections;

namespace Recipes.Wires
{
    public sealed class SetupClassThatReplacesIoCContainerFramework
    {
        public IStreamingRoot Streaming;
        public ITapeStorageFactory Tapes;
        public Func<string, IQueueWriter> CreateQueueWriter;
        public Func<string, IPartitionInbox> CreateInbox;
        public Func<IAtomicStorageStrategy, NuclearStorage> CreateNuclear;


        public IEnvelopeStreamer Streamer = Contracts.CreateStreamer();

        public sealed class AssembledComponents
        {
            public SetupClassThatReplacesIoCContainerFramework Setup;
            public CqrsEngineBuilder Builder;
            public SimpleMessageSender Sender;
            public SimpleDispatcher Dispatcher;
            public IAtomicContainer ProjectionFactory;
        }

        public AssembledComponents AssembleComponents()
        {
            var documents = CreateNuclear(new DocumentStrategy());
            var streamer = Streamer;

            var tapes = Tapes;
            var streaming = Streaming;
            var routerQueue = CreateQueueWriter(Topology.RouterQueue);
            var aggregates = new AggregateFactory(tapes, streamer, routerQueue);
            var sender = new SimpleMessageSender(streamer, routerQueue);
            var flow = new MessageSender(sender);

            var builder = new CqrsEngineBuilder(streamer);

            var log = tapes.GetOrCreateStream(Topology.DomainLogName);
            var dispatcher = new SimpleDispatcher(aggregates, streamer, log);

            builder.Handle(CreateInbox(Topology.RouterQueue),
                Topology.Route(CreateQueueWriter, streamer, tapes), "router");
            builder.Handle(CreateInbox(Topology.EntityQueue), aggregates.Dispatch);

            var functions = new RedirectToDynamicEvent();
            // documents
            //functions.WireToWhen(new RegistrationUniquenessProjection(atomic.Factory.GetEntityWriter<unit, RegistrationUniquenessDocument>()));

            // UI projections
            var projectionStore = CreateNuclear(new ProjectionStrategy());
            foreach (var projection in BootstrapProjections.BuildProjectionsWithWhenConvention(projectionStore.Factory))
            {
                functions.WireToWhen(projection);
            }

            // processes
            //functions.WireToWhen(new Domain.RecipeProcess(flow));

            builder.Handle(CreateInbox(Topology.EventsQueue), aem => CallHandlers(functions, aem));


            var timer = new StreamingTimerService(CreateQueueWriter(Topology.RouterQueue),
                streaming.GetContainer(Topology.FutureMessagesContainer), streamer);
            builder.Handle(CreateInbox(Topology.TimerQueue), timer.PutMessage);
            builder.AddProcess(timer);

            // GNS: just for this simple sample. In reality one would have some command handlers involved
            Bus.SetBus(new SimpleBus(sender, dispatcher));

            return new AssembledComponents
                {
                    Builder = builder,
                    Sender = sender,
                    Dispatcher = dispatcher,
                    ProjectionFactory = projectionStore.Factory,
                    Setup = this
                };
        }

        static void CallHandlers(RedirectToDynamicEvent functions, ImmutableEnvelope aem)
        {
            if (aem.Items.Length != 1)
                throw new InvalidOperationException(
                    "Unexpected number of items in envelope that arrived to projections: " +
                        aem.Items.Length);
            // we wire envelope contents to both direct message call and sourced call (with date wrapper)
            var content = aem.Items[0].Content;
            functions.InvokeEvent(content);
            functions.InvokeEvent(Source.For(aem.EnvelopeId, aem.CreatedOnUtc, (IRecipeEvent) content));
        }
    }
}
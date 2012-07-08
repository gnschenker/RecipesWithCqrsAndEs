using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Lokad.Cqrs;
using Lokad.Cqrs.Envelope;
using Lokad.Cqrs.Partition;
using Lokad.Cqrs.TapeStorage;
using Recipes.Contracts;
using Recipes.Domain;

namespace Recipes.Wires
{
    public sealed class AggregateFactory
    {
        readonly ITapeStorageFactory _factory;
        readonly IEnvelopeStreamer _streamer;
        readonly IQueueWriter _writer;

        public AggregateFactory(ITapeStorageFactory factory, IEnvelopeStreamer streamer, IQueueWriter writer)
        {
            _factory = factory;
            _streamer = streamer;
            _writer = writer;
        }

        public Applied Load(ICollection<ICommand<IIdentity>> commands)
        {
            var id = commands.First().Id;
            var stream = _factory.GetOrCreateStream(IdentityConvert.ToStream(id));
            var records = stream.ReadRecords(0, int.MaxValue).ToList();
            var events = records
                .SelectMany(r => _streamer.ReadAsEnvelopeData(r.Data).Items.Select(m => (IEvent<IIdentity>)m.Content))
                .ToArray();

            var then = new Applied();

            if (records.Count > 0)
            {
                then.Version = records.Last().Version;
            }

            var recipeId = id as RecipeId;
            if (recipeId != null)
            {
                var state = new RecipeAggregateState(events);
                var agg = new RecipeAggregate(state, then.Events.Add);
                ExecuteSafely(agg, commands);
                return then;
            }

            throw new NotSupportedException("identity not supported " + id);
        }


        public static void ExecuteSafely<TIdentity>(IAggregate<TIdentity> self, IEnumerable<IRecipeCommand> commands)
            where TIdentity : IIdentity
        {
            foreach (var hubCommand in commands)
            {
                self.Execute((ICommand<TIdentity>)hubCommand);
            }
        }

        public sealed class Applied
        {
            public List<IEvent<IIdentity>> Events = new List<IEvent<IIdentity>>();
            public long Version;
        }

        public void Dispatch(ImmutableEnvelope e)
        {
            var commands = e.Items.Select(i => (ICommand<IIdentity>)i.Content).ToList();
            var id = commands.First().Id;
            var builder = new StringBuilder();
            var old = Context.SwapFor(s => builder.AppendLine(s));
            Applied results;
            try
            {
                results = Load(commands);
            }
            finally
            {
                Context.SwapFor(old);
            }

            var s1 = builder.ToString();
            if (!String.IsNullOrEmpty(s1))
            {
                Context.Debug(s1.TrimEnd('\r', '\n'));
            }
            AppendToStream(id, e.EnvelopeId, results, s1);
            PublishEvents(id, results);
        }

        void PublishEvents(IIdentity id, Applied then)
        {
            var arVersion = then.Version + 1;
            var arName = IdentityConvert.ToTransportable(id);
            var name = String.Format("{0}-{1}", arName, arVersion);
            var builder = new EnvelopeBuilder(name);
            builder.AddString("entity", arName);

            foreach (var @event in then.Events)
            {
                builder.AddItem((object)@event);
            }
            _writer.PutMessage(_streamer.SaveEnvelopeData(builder.Build()));
        }

        void AppendToStream(IIdentity id, string envelopeId, Applied then, string explanation)
        {
            var stream = _factory.GetOrCreateStream(IdentityConvert.ToStream(id));
            var b = new EnvelopeBuilder("unknown");
            b.AddString("caused-by", envelopeId);

            if (!String.IsNullOrEmpty(explanation))
            {
                b.AddString("explain", explanation);
            }
            foreach (var e in then.Events)
            {
                b.AddItem((object)e);
            }
            var data = _streamer.SaveEnvelopeData(b.Build());
            Context.Debug("?? Append {0} at v{3} to '{1}' in thread {2}", then.Events.Count,
                IdentityConvert.ToStream(id),
                Thread.CurrentThread.ManagedThreadId,
                then.Version);

            if (!stream.TryAppend(data, TapeAppendCondition.VersionIs(then.Version)))
            {
                throw new InvalidOperationException("Failed to update the stream - it has been changed concurrently");
            }
        }
    }
}

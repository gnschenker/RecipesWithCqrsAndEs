using System.Threading;
using System.Threading.Tasks;
using Lokad.Cqrs;
using Lokad.Cqrs.Build;
using Recipes.ReadModel;
using Recipes.Wires;

namespace Recipes.Client
{
    public class Bootstrap
    {
        private CancellationTokenSource cts;
        private CqrsEngineHost engine;
        private Task task;
        private const string IntegrationPath = @".\AppData";

        public void Start()
        {
            var config = FileStorage.CreateConfig(IntegrationPath, "files");

            var setup = new SetupClassThatReplacesIoCContainerFramework
            {
                CreateNuclear = strategy => config.CreateNuclear(strategy),
                Streaming = config.CreateStreaming(),
                Tapes = config.CreateTape(Topology.TapesContainer),
                CreateInbox = s => config.CreateInbox(s),
                CreateQueueWriter = s => config.CreateQueueWriter(s),
            };

            var components = setup.AssembleComponents();

            cts = new CancellationTokenSource();
            engine = components.Builder.Build();
            task = engine.Start(cts.Token);

            Bus.SetBus(new SimpleBus(components.Sender, components.Dispatcher));
            ProviderFactory.SetFactory(components.ProjectionFactory);
        }

        public bool TryStop()
        {
            cts.Cancel();
            return task.Wait(5000);
        }
    }
}
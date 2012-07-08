using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Audit.Util;
using Recipes.Contracts;
using Recipes.Projections;
using Recipes.Wires;
using Lokad.Cqrs;
using Lokad.Cqrs.TapeStorage;

namespace Audit.Views
{
    public partial class RepopulateView : UserControl
    {
        readonly ShellServices _services;
        readonly DomainQueryManager _manager;
        bool _loaded;

        public RepopulateView(IEnvelopeStreamer serializer, LocalEventStore localEventStore, ShellServices services)
        {
            _services = services;
            _manager = new DomainQueryManager(serializer, localEventStore, _services);

            InitializeComponent();
            ViewLoad();
        }

        ViewMapInfo[] _allMaps = new ViewMapInfo[0];

        public void ViewLoad()
        {
            if (_loaded)
                return;

            _loaded = true;

            WriteLine("Scanning view assemblies for the first time");


            var viewMaps = DomainScanner.GetActiveViewMaps()
                .GroupBy(vmi => vmi.ViewType)
                .OrderBy(g => g.Key.Name)
                .ToList();

            _allMaps = viewMaps.SelectMany(g => g).ToArray();

            foreach (var viewMap in viewMaps)
            {
                const string image = "view-entity";

                var name = viewMap.Key.Name;
                var viewNode = treeView1.AddNode(name, viewMap.ToArray(), image);

                // note, that we don't split handlers, since view should be repopulated as a whole
                foreach (var viewMapInfo in viewMap)
                {
                    foreach (var @event in viewMapInfo.Events)
                    {
                        viewNode.AddNode(@event.Name, image : "message-event");
                    }
                }
            }

            WriteLine("View analysis complete");
        }

        public void ViewUnload()
        {
            //throw new NotImplementedException();
        }

        public void WriteLine(string format, params object[] args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => WriteLine(format, args)));
            }
            else
            {
                textBox1.AppendText(string.Format(format, args) + Environment.NewLine);
            }
        }

        public void Write(string format, params object[] args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Write(format, args)));
            }
            else
            {
                textBox1.AppendText(string.Format(format, args));
            }
        }

        void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
        {
            _rebuild.Enabled = (e.Node != null) && (e.Node.Tag is ViewMapInfo[]);
        }

        readonly ProjectionStrategy _strategy = new ProjectionStrategy();

        DirectoryInfo[] ObserveViews(ViewMapInfo[] handlers, DomainQueryManager queryManager)
        {
            WriteLine("Configuring {0} view handlers", handlers.Length);

            var name = string.Format("view-{0:yyyy-MM-dd-HH-mm-ss}", DateTime.Now);
            var combined = Path.Combine(Path.GetTempPath(), name);
            WriteLine("Redirecting atomic views to {0}", combined);
            var directoryInfo = new DirectoryInfo(combined);

            var file = FileStorage.CreateConfig(combined, reset : true);
            var nuclear = file.CreateNuclear(new ProjectionStrategy());

            var watch = Stopwatch.StartNew();
            var projectionTypes = new HashSet<Type>(handlers.Select(v => v.Projection));

            WriteLine("Building consumers...");

            var projections =
                BootstrapProjections.BuildProjectionsWithWhenConvention(nuclear.Factory).Where(
                    x => projectionTypes.Contains(x.GetType()));

            var wire = new RedirectToDynamicEvent();

            foreach (var projection in projections)
            {
                wire.WireToWhen(projection);
            }
            var handlersWatch = Stopwatch.StartNew();


            queryManager.QueryConsumer(envelope => CallHandlers(wire, envelope));
            WriteLine("Saved to {0}.", directoryInfo.FullName);


            var timeTotal = watch.Elapsed.TotalSeconds;
            var handlerTicks = handlersWatch.ElapsedTicks;
            var timeInHandlers = Math.Round(TimeSpan.FromTicks(handlerTicks).TotalSeconds, 1);
            WriteLine("Total Elapsed: {0}sec ({1}sec in handlers)", Math.Round(timeTotal, 0), timeInHandlers);

            WriteLine("Saved to {0}.", directoryInfo.FullName);

            var directoryInfos = handlers
                .Select(h => _strategy.GetFolderForEntity(h.ViewType, h.KeyType))
                .Distinct()
                .Select(f => new DirectoryInfo(Path.Combine(directoryInfo.FullName, f.ToString())))
                .ToArray();

            return directoryInfos;
        }

        static void CallHandlers(RedirectToDynamicEvent functions, ImmutableEnvelope aem)
        {
            foreach (var item in aem.Items)
            {
                var e = item.Content as IRecipeEvent;

                if (e != null)
                {
                    // we wire envelope contents to both direct message call and sourced call (with date wrapper)
                    functions.InvokeEvent(e);
                    functions.InvokeEvent(Source.For(aem.EnvelopeId, aem.CreatedOnUtc, e));
                }
            }
        }


        void RebuildAllClick(object sender, EventArgs e)
        {
            const string text = @"This will rebuild all views! Do you want to review the documentation first?";
            const string help =
                @"https://sites.google.com/a/lokad.com/dev/Home/rinat-abdullins-work-log/cqrsviewsinsalescast";
            if (MessageBox.Show(this, text, @"Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start(help);
                return;
            }
            RunRebuild(_allMaps);
        }

        void RebuildClick(object sender, EventArgs e)
        {
            var handlers = (ViewMapInfo[]) treeView1.SelectedNode.Tag;
            var @join = handlers.GroupBy(h => h.ViewType).Select(n => n.Key.Name).ToArray();
            var s = string.Format(@"This will rebuild: {0}. Do you want to proceed?", @join);
            if (MessageBox.Show(this, s, @"Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            RunRebuild(handlers);
        }

        void RunRebuild(ViewMapInfo[] handlers)
        {
            var task = new Task<unit>(() => unit.it);

            var c = ContinueIf(task, unit => ObserveViews(handlers, _manager));

            task.Start();
        }

        Task<TResult> ContinueIf<TInput, TResult>(Task<TInput> task, Func<TInput, TResult> action)
        {
            task.ContinueWith(t => WriteLine("Exception {0}", t.Exception), TaskContinuationOptions.NotOnRanToCompletion);
            return task.ContinueWith(t => action(t.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        void _testRebuild_Click(object sender, EventArgs e)
        {
            using (var x = new OpenFileDialog())
            {
                x.Filter = @"*.tmd|*.tmd";
                x.Title = @"Pick a tmd file to run";
                if (x.ShowDialog(this) == DialogResult.OK)
                {
                    var cloned = _manager.CloneFor(new LocalEventStore(null, new FileTapeStream(x.FileName)));
                    WriteLine("Using for rebuild test: {0}", x.FileName);
                    var result = Task.Factory.StartNew(() => ObserveViews(_allMaps, cloned));
                    ContinueIf(result, di => unit.it);
                }
            }
        }
    }
}
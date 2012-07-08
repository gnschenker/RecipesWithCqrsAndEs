using System;
using System.IO;
using System.Windows.Forms;
using Audit.Util;
using Audit.Views;
using Recipes.Wires;
using Lokad.Cqrs;
using Lokad.Cqrs.TapeStorage;

namespace Audit
{
    static class Program
    {
        static readonly IEnvelopeStreamer EnvelopeStreamer;

        static Program()
        {
            EnvelopeStreamer = Contracts.CreateStreamer();
        }

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var form = new LoadSettingsForm())
            {
                form.BindArgs(args);
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var conf = form.GetAzureConfig();
                if (form.ConfigFileSelected)
                {
                    AttachToRemoteLog(conf);
                }
                else
                {
                    AttachToLocalLog(conf);
                }
            }
        }

        static void AttachToLocalLog(string filePath)
        {
            var config = FileStorage.CreateConfig(Path.GetDirectoryName(filePath) ?? "");

            var cache = new FileTapeStream(filePath);

            var store = new LocalEventStore(null, cache);
            var send = config.CreateQueueWriter(Topology.RouterQueue);
            var endpoint = new SimpleMessageSender(EnvelopeStreamer, send);

            Application.Run(new DomainLogView(store, endpoint, EnvelopeStreamer));
        }

        static void AttachToRemoteLog(string azureConfig)
        {
            throw new NotImplementedException();
        }
    }
}
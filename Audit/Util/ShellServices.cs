using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lokad.Cqrs;
using Recipes.Contracts;
using ServiceStack.Text;

namespace Audit.Util
{
    public sealed class ShellServices
    {
        readonly ConcurrentQueue<string> _log = new ConcurrentQueue<string>();

        readonly SimpleMessageSender _client;
        readonly Stopwatch _watch = Stopwatch.StartNew();

        readonly CancellationTokenSource _source = new CancellationTokenSource();

        public ShellServices(SimpleMessageSender client)
        {
            _client = client;
        }

        public Task ConnectLogger(RichTextBox box)
        {
            return Task.Factory.StartNew(() =>
                {
                    while (!_source.Token.IsCancellationRequested)
                    {
                        string result;
                        var builder = new StringBuilder();
                        while (_log.TryDequeue(out result))
                        {
                            builder.AppendLine(result);
                        }
                        if (builder.Length > 0)
                        {
                            box.Invoke(new Action(() =>
                                {
                                    box.AppendText(builder.ToString());
                                    box.ScrollToCaret();
                                }));
                        }
                        _source.Token.WaitHandle.WaitOne(TimeSpan.FromMilliseconds(250));
                    }
                }, _source.Token);
        }

        public SimpleMessageSender Client
        {
            get { return _client; }
        }

        public void SendCommand(IRecipeCommand message)
        {
            Log("BUS", "Sending '{0}'\r\n{1}", message.GetType().Name, JsonSerializer.SerializeToString(message));

            _client.SendOne(message);
        }


        public void Schedule(Action action, TimeSpan span)
        {
            Task.Factory
                .StartNew(() => _source.Token.WaitHandle.WaitOne(span))
                .ContinueWith(t => action(), _source.Token,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }


        public void Disconnect()
        {
            _source.Cancel();
        }

        public void Log(string sender, string format, params object[] args)
        {
            var prefix = string.Format("[{0}: {1:0000000}] ", sender, _watch.ElapsedMilliseconds);
            _log.Enqueue(prefix + string.Format(CultureInfo.InvariantCulture, format, args));
        }
    }
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Lokad.Cqrs;

namespace Audit.Util
{
    public sealed class DomainQueryManager
    {
        readonly IEnvelopeStreamer _serializer;
        readonly LocalEventStore _store;
        readonly ShellServices _services;

        public DomainQueryManager(IEnvelopeStreamer serializer, LocalEventStore store, ShellServices services)
        {
            _serializer = serializer;
            _store = store;
            _services = services;
        }

        public DomainQueryManager CloneFor(LocalEventStore store)
        {
            return new DomainQueryManager(_serializer, store, _services);
        }

        public bool IsSyncAvailable
        {
            get { return _store.IsSyncAvailable; }
        }

        public void Log(string text, params object[] args)
        {
            _services.Log("DomainAudit", text, args);
        }

        public Task<unit> SyncLog()
        {
            Log("Sync log");
            return _store.SyncLog(s => Log(s));
        }

        public void QueryConsumer(Action<ImmutableEnvelope> redirects)
        {
            Log("Loading infrastructure");

            var boundary = DateTime.MinValue;

            var watch = Stopwatch.StartNew();
            var count = 0;

            _store.ObserveAll(data =>
                {
                    var message = _serializer.ReadAsEnvelopeData(data.Data);

                    count += 1;
                    var date = message.CreatedOnUtc;

                    if (date.Month != boundary.Month)
                    {
                        Log("Scanned to {0:yyyy-MM}", date);
                        boundary = date;
                    }
                    redirects(message);
                });

            var seconds = watch.Elapsed.TotalSeconds;
            var speed = count / seconds;
            Log("Scanned {0} events in {1} sec ({2} per second)", count, Math.Round(seconds, 0), Math.Round(speed, 0));
        }

        public sealed class Stat
        {
            public readonly Type ConsumerType;
            public readonly Type MessageType;
            public readonly TimeSpan TimeSpent;
            public readonly int Messages;

            public long AverageTicks
            {
                get
                {
                    if (Messages == 0) return 0;
                    return TimeSpent.Ticks / Messages;
                }
            }

            public Stat(Type consumerType, Type messageType, TimeSpan timeSpent, int messages)
            {
                ConsumerType = consumerType;
                MessageType = messageType;
                TimeSpent = timeSpent;
                Messages = messages;
            }
        }
    }
}
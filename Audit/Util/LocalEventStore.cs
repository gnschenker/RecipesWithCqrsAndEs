using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lokad.Cqrs;
using Lokad.Cqrs.TapeStorage;

namespace Audit.Util
{
    public sealed class LocalEventStore
    {
        const int BatchSize = 5000;

        readonly ITapeStream _remote;
        readonly FileTapeStream _cache;

        public bool IsSyncAvailable { get; private set; }

        public LocalEventStore(ITapeStream remote, FileTapeStream cache)
        {
            IsSyncAvailable = remote != null;
            _remote = remote;
            _cache = cache;
        }

        public Task<unit> SyncLog(Action<string> logger)
        {
            return new Task<unit>(() =>
                {
                    SyncInternal(logger);
                    return unit.it;
                });
        }

        public void ObserveAll(Action<TapeRecord> observer)
        {
            foreach (var record in _cache.ReadRecords(0, int.MaxValue))
            {
                observer(record);
            }
        }

        /// <summary>
        /// Observes first <see cref="count"/> records from reversed list
        /// where version is less or equal to <see cref="max"/>.
        /// </summary>
        /// <param name="max">Max version observed.</param>
        /// <param name="count">Max count of observed records.</param>
        /// <param name="observer">Observer action.</param>
        public void ObserveTill(long max, int count, Action<TapeRecord> observer)
        {
            // It's assumed that FileTapeStream uses Versions as index
            // and Version will increase by one without gaps.

            // Read backward
            var start = Math.Min(max, _cache.GetCurrentVersion());
            var end = Math.Max(start - count, 0);
            var totalCount = (int) (start - end);

            var index = 0;
            while (true)
            {
                var readCount = Math.Min(BatchSize, totalCount - index);
                if (readCount == 0)
                    break;

                var records = _cache.ReadRecords(start - readCount - index, readCount).ToArray();
                if (records.Length == 0)
                    break;

                foreach (var record in records.Reverse())
                    observer(record);

                index += records.Length;
            }
        }

        T Retry<T>(Func<T> producer, Action<string> message)
        {
            Exception e = new NullReferenceException();
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    return producer();
                }
                catch (Exception ex)
                {
                    message("Retrying from: " + ex.Message);
                    e = ex;
                }
            }
            throw e;
        }

        void SyncInternal(Action<string> logger)
        {
            try
            {
                var next = _cache.GetCurrentVersion();

                while (true)
                {
                    long next1 = next;
                    var items = Retry(() => _remote.ReadRecords(next1, BatchSize).ToArray(), logger);
                    if (items.Length == 0)
                        break;

                    // Check that versions are contiguous
                    var nextCopy = next;
                    var versions = items
                        .Select((r, i) => new {Actual = r.Version, Expected = nextCopy + 1 + i})
                        .ToArray();

                    if (!versions.All(v => v.Actual == v.Expected))
                        throw new InvalidOperationException("Versions are not contiguous.");

                    next = items.Max(m => m.Version);

                    _cache.AppendNonAtomic(items);
                    logger(string.Format("Downloaded {0} records", items.Length));

                    if (items.Length <= 4)
                        break;
                }
            }
            catch (Exception ex)
            {
                logger(ex.Message);
                Trace.WriteLine(ex);
            }
        }
    }
}
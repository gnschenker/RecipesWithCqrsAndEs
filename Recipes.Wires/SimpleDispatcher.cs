using System;
using Lokad.Cqrs;
using Lokad.Cqrs.Envelope;
using Lokad.Cqrs.TapeStorage;

namespace Recipes.Wires
{
    public class SimpleDispatcher
    {
        private readonly AggregateFactory _aggregateFactory;
        private readonly IEnvelopeStreamer _serializer;
        private readonly ITapeStream _tapeStream;

        public SimpleDispatcher(AggregateFactory aggregateFactory, IEnvelopeStreamer serializer, ITapeStream tapeStream)
        {
            _aggregateFactory = aggregateFactory;
            _serializer = serializer;
            _tapeStream = tapeStream;
        }

        public void DispatchCommand(object command, Action<EnvelopeBuilder> builder = null)
        {
            var envelopeBuilder = new EnvelopeBuilder(Guid.NewGuid().ToString());
            envelopeBuilder.AddItem(command);
            if (builder != null) builder(envelopeBuilder);
            var envelope = envelopeBuilder.Build();

            var data = _serializer.SaveEnvelopeData(envelope);
            if (!_tapeStream.TryAppend(data))
                throw new InvalidOperationException("Failed to record domain log");

            _aggregateFactory.Dispatch(envelope);
        }
    }
}
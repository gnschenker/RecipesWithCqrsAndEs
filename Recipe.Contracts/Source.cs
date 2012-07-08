using System;
using System.Linq;

namespace Recipes.Contracts
{
    public sealed class Source<TEvent> where TEvent : IRecipeEvent
    {
        public readonly string MessageId;
        public readonly DateTime CreatedUtc;
        public readonly TEvent Event;

        public Source(string messageId, DateTime createdUtc, TEvent @event)
        {
            MessageId = messageId;
            CreatedUtc = createdUtc;
            Event = @event;
        }
    }

    public static class Source
    {
        public static object For(string messageId, DateTime date, IRecipeEvent instance)
        {
            return typeof(Source<>).MakeGenericType(instance.GetType()).GetConstructors()
                .First()
                .Invoke(new object[] {messageId, date, instance});
        }
    }
}
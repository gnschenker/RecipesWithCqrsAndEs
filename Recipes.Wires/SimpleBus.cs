using System;
using Lokad.Cqrs;
using Recipes.Contracts;

namespace Recipes.Wires
{
    public class SimpleBus : IBus
    {
        private static SimpleMessageSender _sender;
        private static SimpleDispatcher _dispatcher;

        public SimpleBus(SimpleMessageSender sender, SimpleDispatcher dispatcher)
        {
            _sender = sender;
            _dispatcher = dispatcher;
        }

        public void SendOne(object content)
        {
            _sender.SendOne(content);
        }

        public void Dispatch(object command, Action onSuccess, Action<Exception> onFailure = null)
        {
            _dispatcher.DispatchCommand(command, null);
            onSuccess();
        }
    }
}
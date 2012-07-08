using System;
using Recipes.Contracts;

namespace Recipes.Wires
{
    public static class Bus
    {
        private static IBus _bus;

        public static void SetBus(IBus bus) { _bus = bus; }

        public static void SendOne(object content)
        {
            _bus.SendOne(content);
        }

        public static void Send(IRecipeCommand commmand, Action onSuccess, Action<Exception> onError = null)
        {
            _bus.Dispatch(commmand, onSuccess, onError);
        }
    }
}
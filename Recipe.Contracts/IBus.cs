using System;

namespace Recipes.Contracts
{
    public interface IBus
    {
        void SendOne(object message);
        void Dispatch(object command, Action onSuccess, Action<Exception> onFailure = null);
    }
}
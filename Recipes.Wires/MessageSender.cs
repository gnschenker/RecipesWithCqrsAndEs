using System;
using Lokad.Cqrs;
using Recipes.Contracts;

namespace Recipes.Wires
{
    public sealed class MessageSender : IFunctionalFlow
    {
        readonly SimpleMessageSender _sender;

        public MessageSender(SimpleMessageSender sender)
        {
            _sender = sender;
        }

        public void Schedule(IRecipeCommand command, DateTime dateUtc)
        {
            _sender.SendOne(command, eb => eb.DeliverOnUtc(dateUtc));
        }

        public void SendCommandsAsBatch(IRecipeCommand[] commands)
        {
            _sender.SendBatch(commands);
        }
    }
}
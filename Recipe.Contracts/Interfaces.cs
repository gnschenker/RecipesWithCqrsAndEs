using System;

namespace Recipes.Contracts
{
    public interface IRecipeMessage {}

    public interface IRecipeCommand : IRecipeMessage {}

    public interface IRecipeEvent : IRecipeMessage {}

    public interface ICommand<out TIdentity> : IRecipeCommand
        where TIdentity : IIdentity
    {
        TIdentity Id { get; }
    }

    public interface IEvent<out TIdentity> : IRecipeEvent
        where TIdentity : IIdentity
    {
        TIdentity Id { get; }
    }

    public interface IFunctionalCommand : IRecipeCommand {}

    public interface IFunctionalEvent : IRecipeEvent {}

    /// <summary>The only messaging endpoint that is available to stateless services
    /// They are not allowed to send any other messages.</summary>
    public interface IServiceEndpoint
    {
        void Publish(IFunctionalEvent notification);
    }


    public interface IAggregate<in TIdentity>
        where TIdentity : IIdentity
    {
        void Execute(ICommand<TIdentity> c);
    }


    public interface IAggregateState
    {
        void Apply(IEvent<IIdentity> e);
    }


    /// <summary>
    /// Semi strongly-typed message sending endpoint made
    ///  available to stateless workflow processes.
    /// </summary>
    public interface IFunctionalFlow
    {
        void Schedule(IRecipeCommand command, DateTime dateUtc);

        /// <summary>
        /// This interface is intentionally made long and unusable. Generally within the domain 
        /// (as in Mousquetaires domain) there will be extension methods that provide strongly-typed
        /// extensions (that don't allow sending wrong command to wrong location).
        /// </summary>
        /// <param name="commands">The commands.</param>
        void SendCommandsAsBatch(IRecipeCommand[] commands);
    }
}
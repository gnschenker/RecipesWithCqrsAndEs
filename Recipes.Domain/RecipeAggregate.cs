using System;
using Recipes.Client;
using Recipes.Contracts;

namespace Recipes.Domain
{
    public class RecipeAggregate : IAggregate<RecipeId>
    {
        private readonly RecipeAggregateState state;
        private readonly Action<IEvent<IIdentity>> observer;

        // *1* Factory re-hydrates aggregate
        public RecipeAggregate(RecipeAggregateState state, Action<IEvent<IIdentity>> observer)
        {
            this.state = state;
            this.observer = observer;
        }

        // *2* factory forwards incoming command
        public void Execute(ICommand<RecipeId> c)
        {
            RedirectToWhen.InvokeCommand(this, c);
        }

        // *3* specific command is handeled
        public void When(CreateDraftRecipe c)
        {
            // validating invariants
            // ...

            // from now on things cannot be undone any more!
            Apply(new DraftRecipeCreated
                      {
                          Id = c.Id,
                          Title = c.Title,
                          CookingInstructions = c.CookingInstructions
                      });    
        }

        public void When(SubmitRecipe c)
        {
            if(state.RecipeStatus != RecipeStatus.Draft)
                throw new Exception("Cannot submit a recipe that is not in draft mode.");

            Apply(new RecipeSubmitted{Id = c.Id});
        }

        // *4* event is a) used to update agg state and b) forwarded to infrastructure
        private void Apply(IEvent<RecipeId> e)
        {
            state.Apply(e);
            observer(e);    // store in ES (sync) and async publish
        }
    }
}

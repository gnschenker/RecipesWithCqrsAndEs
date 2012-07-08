using System.Collections.Generic;
using Recipes.Client;
using Recipes.Contracts;

namespace Recipes.Domain
{
    public class RecipeAggregateState : IAggregateState
    {
        public RecipeAggregateState(IEnumerable<IEvent<IIdentity>> events)
        {
            foreach(var e in events)
                Apply(e);
        }

        public int Version { get; set; }
        public RecipeStatus RecipeStatus { get; set; }

        public void When(DraftRecipeCreated e)
        {
            RecipeStatus = RecipeStatus.Draft;
        }

        public void When(RecipeSubmitted e)
        {
            RecipeStatus = RecipeStatus.Submitted; ;
        }

        public void Apply(IEvent<IIdentity> e)
        {
            Version++;
            RedirectToWhen.InvokeEventOptional(this, e);
        }
    }
}
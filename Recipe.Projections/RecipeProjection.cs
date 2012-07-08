using Lokad.Cqrs.AtomicStorage;
using Recipes.Client;
using Recipes.Contracts;
using Recipes.Contracts.Projections;

namespace Recipes.Projections
{
    public class RecipeProjection
    {
        private readonly IAtomicWriter<RecipeId, RecipeView> writer;

        public RecipeProjection(IAtomicWriter<RecipeId, RecipeView> writer)
        {
            this.writer = writer;
        }

        public void When(DraftRecipeCreated e)
        {
            writer.Add(e.Id, new RecipeView
                                 {
                                     Id = e.Id,
                                     Title = e.Title,
                                     Instructions = e.CookingInstructions,
                                     RecipeStatus = RecipeStatus.Draft
                                 });
        }

        public void When(RecipeSubmitted e)
        {
            writer.UpdateOrThrow(e.Id, view => view.RecipeStatus = RecipeStatus.Submitted);
        }
    }
}
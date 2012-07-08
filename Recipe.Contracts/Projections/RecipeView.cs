using Recipes.Client;

namespace Recipes.Contracts.Projections
{
    public class RecipeView
    {
        public RecipeId Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public RecipeStatus RecipeStatus { get; set; }
    }
}
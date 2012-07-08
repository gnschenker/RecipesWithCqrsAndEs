using System.Collections.Generic;
using Recipes.Contracts.Projections;

namespace Recipes.Contracts.Providers
{
    public interface IRecipeProvider
    {
        IEnumerable<RecipeView> GetByName(int pagesize, int skip = 0);
    }
}
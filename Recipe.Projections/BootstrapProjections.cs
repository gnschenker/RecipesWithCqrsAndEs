using System.Collections.Generic;
using Lokad.Cqrs;
using Lokad.Cqrs.AtomicStorage;
using Recipes.Contracts;
using Recipes.Contracts.Projections;

namespace Recipes.Projections
{
    public class BootstrapProjections
    {
        public static IEnumerable<object> BuildProjectionsWithWhenConvention(IAtomicContainer factory)
        {
            yield return new RecipeProjection(factory.GetEntityWriter<RecipeId, RecipeView>());
            yield return new RecipeByNameIndexProjection(factory.GetEntityWriter<unit, RecipeByNameIndex>());
        }
    }
}

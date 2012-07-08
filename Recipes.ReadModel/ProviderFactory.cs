using Lokad.Cqrs;
using Lokad.Cqrs.AtomicStorage;
using Recipes.Contracts;
using Recipes.Contracts.Projections;
using Recipes.Contracts.Providers;

namespace Recipes.ReadModel
{
    public static class ProviderFactory
    {
        private static IAtomicContainer factory;

        public static void SetFactory(IAtomicContainer providerFactory)
        {
            factory = providerFactory;
        }

        public static IRecipeProvider CreateRecipeProvider()
        {
            return new RecipeProvider(
                factory.GetEntityReader<RecipeId, RecipeView>(),
                factory.GetEntityReader<unit, RecipeByNameIndex>());
        }
    }
}
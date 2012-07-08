using System.Collections.Generic;
using Lokad.Cqrs;
using Lokad.Cqrs.AtomicStorage;
using Recipes.Contracts;
using Recipes.Contracts.Projections;
using Recipes.Contracts.Providers;
using System.Linq;

namespace Recipes.ReadModel
{
    public class RecipeProvider : IRecipeProvider
    {
        private readonly IAtomicReader<RecipeId, RecipeView> reader;
        private readonly IAtomicReader<unit, RecipeByNameIndex> indexReader;

        public RecipeProvider(IAtomicReader<RecipeId, RecipeView> reader,
            IAtomicReader<unit, RecipeByNameIndex> indexReader)
        {
            this.reader = reader;
            this.indexReader = indexReader;
        }

        public IEnumerable<RecipeView> GetByName(int pagesize, int skip = 0)
        {
            var index = indexReader.Get(unit.it).Value.Index;
            var keys = index.Keys.OrderBy(x => x).Skip(skip).Take(pagesize);
            var ids = keys.SelectMany(k => index[k]);
            var views = ids.Select(id =>
                            {
                                RecipeView view;
                                reader.TryGet(new RecipeId(id), out view);
                                return view;
                            });
            return views;
        }
    }
}
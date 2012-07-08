using System;
using System.Collections.Generic;

namespace Recipes.Contracts.Projections
{
    public class RecipeByNameIndex
    {
        public RecipeByNameIndex()
        {
            Index = new Dictionary<string, List<Guid>>();
        }

        public Dictionary<string, List<Guid>> Index { get; set; }
    }
}
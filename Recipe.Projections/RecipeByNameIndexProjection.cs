using System;
using System.Collections.Generic;
using Lokad.Cqrs;
using Lokad.Cqrs.AtomicStorage;
using Recipes.Contracts;
using Recipes.Contracts.Projections;

namespace Recipes.Projections
{
    public class RecipeByNameIndexProjection
    {
        private readonly IAtomicWriter<unit, RecipeByNameIndex> writer;

        public RecipeByNameIndexProjection(IAtomicWriter<unit, RecipeByNameIndex> writer)
        {
            this.writer = writer;
        }

        public void When(DraftRecipeCreated e)
        {
            writer.UpdateEnforcingNew(unit.it,
                                      view =>
                                          {
                                              if(!view.Index.ContainsKey(e.Title))
                                                  view.Index[e.Title] = new List<Guid>();
                                              view.Index[e.Title].Add(e.Id.Id);
                                          });
        }
    }
}
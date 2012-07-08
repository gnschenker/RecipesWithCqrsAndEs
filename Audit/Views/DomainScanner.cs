#region (c) 2010-2011 Lokad CQRS - New BSD License 

// Copyright (c) Lokad SAS 2010-2012 (http://www.lokad.com)
// This code is released as Open Source under the terms of the New BSD License
// Homepage: http://lokad.github.com/lokad-cqrs/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Lokad.Cqrs.AtomicStorage;
using Recipes.Projections;
using Recipes.Wires;

namespace Audit.Views
{
    public static class DomainScanner
    {
        sealed class AtomicDetector : IAtomicContainer
        {
            public readonly List<Tuple<Type, Type>> List = new List<Tuple<Type, Type>>();

            public IAtomicWriter<TKey, TEntity> GetEntityWriter<TKey, TEntity>()
            {
                List.Add(Tuple.Create(typeof(TKey), typeof(TEntity)));
                return null;
            }

            public IAtomicReader<TKey, TEntity> GetEntityReader<TKey, TEntity>()
            {
                throw new NotImplementedException();
            }

            public IAtomicStorageStrategy Strategy
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerable<AtomicRecord> EnumerateContents()
            {
                throw new NotImplementedException();
            }

            public void WriteContents(IEnumerable<AtomicRecord> records)
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public static IEnumerable<ViewMapInfo> GetActiveViewMaps()
        {
            var marker = new AtomicDetector();
            var projections = BootstrapProjections.BuildProjectionsWithWhenConvention(marker)
                .ToArray();

            int idx = 0;
            foreach (var projection in projections)
            {
                var redirect = new RedirectToDynamicEvent();
                redirect.WireToWhen(projection);
                var mark = marker.List[idx];
                yield return new ViewMapInfo(mark.Item2, mark.Item1, projection.GetType(), redirect.Dict.Keys.ToArray())
                    ;
                idx += 1;
            }
        }
    }
}
using System;
using System.IO;
using Lokad.Cqrs;
using Lokad.Cqrs.AtomicStorage;
using Recipes.Contracts;
using ServiceStack.Text;

namespace Recipes.Wires
{
    public sealed class ProjectionStrategy : IAtomicStorageStrategy
    {
        public string GetFolderForEntity(Type entityType, Type keyType)
        {
            if (keyType == typeof(unit))
            {
                return "recipes-ui";
            }
            return "recipes-ui/" + entityType.Name.ToLowerInvariant();
        }

        public string GetNameForEntity(Type entity, object key)
        {
            if (key is unit)
                return entity.Name.ToLowerInvariant() + ".txt";
            if (key is IIdentity)
                return IdentityConvert.ToStream((IIdentity) key) + ".txt";
            return key.ToString().ToLowerInvariant() + ".txt";
        }

        public void Serialize<TEntity>(TEntity entity, Stream stream)
        {
            JsonSerializer.SerializeToStream(entity, stream);
        }

        public TEntity Deserialize<TEntity>(Stream stream)
        {
            return JsonSerializer.DeserializeFromStream<TEntity>(stream);
        }
    }
}
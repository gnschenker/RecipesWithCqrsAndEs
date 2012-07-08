using System;
using System.Runtime.Serialization;

namespace Recipes.Contracts
{
    public static class IdentityConvert
    {
        public static string ToStream(IIdentity identity)
        {
            return identity.GetTag() + "-" + identity.GetId();
        }

        public static string ToTransportable(IIdentity identity)
        {
            return identity.GetTag() + "-" + identity.GetId();
        }
    }

    [DataContract(Namespace = "Recipe")]
    public class RecipeId : AbstractIdentity<Guid>
    {
        public const string TagValue = "recipe";

        public RecipeId() { }

        public RecipeId(Guid id) { Id = id; }

        public override string GetTag() { return TagValue; }

        [DataMember(Order = 1)] public override Guid Id { get; protected set; }
    }
}
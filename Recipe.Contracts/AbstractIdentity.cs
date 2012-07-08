using System.Runtime.Serialization;

namespace Recipes.Contracts
{
    /// <summary>
    /// Strongly-typed identity class. Essentially just an ID with a 
    /// distinct type. It introduces strong-typing and speeds up development
    /// on larger projects. Idea by Jeremie, implementation by Rinat
    /// </summary>
    public interface IIdentity
    {
        /// <summary>
        /// Gets the id, converted to a string. Only alphanumerics and '-' are allowed.
        /// </summary>
        /// <returns></returns>
        string GetId();

        /// <summary>
        /// Unique tag (should be unique within the assembly) to distinguish
        /// between different identities, while deserializing.
        /// </summary>
        string GetTag();
    }

    [DataContract(Namespace = "Recipe")]
    public sealed class NullId : IIdentity
    {
        public const string TagValue = "";
        public static readonly IIdentity Instance = new NullId();

        public string GetId()
        {
            return "";
        }

        public string GetTag()
        {
            return "";
        }
    }

    /// <summary>
    /// Base implementation of <see cref="IIdentity"/>, which implements
    /// equality and ToString once and for all.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public abstract class AbstractIdentity<TKey> : IIdentity
    {
        public abstract TKey Id { get; protected set; }

        public string GetId()
        {
            return Id.ToString();
        }

        public virtual string GetTag()
        {
            return GetType().ToString().ToLowerInvariant();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            var identity = obj as AbstractIdentity<TKey>;

            if (identity != null)
            {
                return identity.Id.Equals(Id) && string.Equals(identity.GetTag(),GetTag());
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", GetType().Name.Replace("Id", ""), Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (GetTag().GetHashCode());
            }
        }
    }

    public interface IIdentityGenerator
    {
        long GetId();
    }
}
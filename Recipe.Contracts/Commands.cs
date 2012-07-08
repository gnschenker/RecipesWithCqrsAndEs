using System.Runtime.Serialization;

namespace Recipes.Contracts
{
    [DataContract(Namespace = "Recipe")]
    public class CreateDraftRecipe : ICommand<RecipeId>
    {
        [DataMember(Order = 1)] public RecipeId Id { get; set; }
        [DataMember(Order = 2)] public string Title { get; set; }
        [DataMember(Order = 3)] public string CookingInstructions { get; set; }
    }

    [DataContract(Namespace = "Recipe")]
    public class SubmitRecipe : ICommand<RecipeId>
    {
        [DataMember(Order = 1)] public RecipeId Id { get; set; }
    }
}
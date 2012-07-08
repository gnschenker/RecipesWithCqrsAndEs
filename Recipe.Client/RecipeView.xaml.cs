namespace Recipes.Client
{
    public interface IRecipeView
    {
        void SetContext(object context);
        void Close();
        void Show();
        void FocusTitle();
    }

    public partial class RecipeView : IRecipeView
    {
        public RecipeView()
        {
            InitializeComponent();
        }

        public void SetContext(object context)
        {
            DataContext = context;
        }

        public void FocusTitle()
        {
            RecipeTitle.Focus();
        }
    }
}

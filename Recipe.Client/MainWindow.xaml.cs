namespace Recipes.Client
{
    public interface IMainView
    {
        void Show();
        void SetContext(object context);
    }

    public partial class MainWindow : IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
            AddRecipe.Focus();
        }

        public void SetContext(object context)
        {
            DataContext = context;
        }
    }
}

namespace Recipes.Client
{
    public partial class App
    {
        private readonly Bootstrap bootstrap;

        public App()
        {
            bootstrap = new Bootstrap();
            Exit += (sender, e) => bootstrap.TryStop();
            bootstrap.Start();

            var model = new MainViewModel(new MainWindow());
            model.View.Show();
        }
    }
}

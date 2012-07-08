using System.Collections.ObjectModel;
using System.Windows.Input;
using Recipes.Contracts;
using Recipes.ReadModel;
using Recipes.Wires;

namespace Recipes.Client
{
    public class MainViewModel
    {
        public IMainView View { get; private set; }
        public ICommand AddNewRecipeCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ObservableCollection<RecipeViewModelState> Recipes { get; set; }

        public MainViewModel(IMainView view)
        {
            View = view;
            Recipes = new ObservableCollection<RecipeViewModelState>();
            AddNewRecipeCommand = new DelegateCommand(OnAddNewRecipe);
            ReloadCommand = new DelegateCommand(OnReload);
            view.SetContext(this);
        }

        private void OnAddNewRecipe(object obj)
        {
            var model = new RecipeViewModel(new RecipeView(), m => Recipes.Add(m), OnSubmit);
            model.View.Show();
        }

        private void OnReload(object obj)
        {
            Recipes.Clear();
            var provider = ProviderFactory.CreateRecipeProvider();
            foreach(var recipe in provider.GetByName(100))
            {
                var state = new RecipeViewModelState();
                state.SubmitCommand = new DelegateCommand<RecipeViewModelState>(z => OnSubmit(state), state, x => x.CanSubmit);
                state.Initialize(recipe);
                Recipes.Add(state);
            }
        }

        private void OnSubmit(RecipeViewModelState model)
        {
            var command = new SubmitRecipe { Id = new RecipeId(model.RecipeId) };
            Bus.Send(command, () => { model.RecipeStatus = RecipeStatus.Submitted; });
        }
    }
}
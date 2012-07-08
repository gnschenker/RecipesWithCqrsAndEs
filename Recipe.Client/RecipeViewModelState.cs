using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Recipes.Client
{
    public class RecipeViewModelState : INotifyPropertyChanged
    {
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public Guid RecipeId { get; set; }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(x => x.Title);
            }
        }

        private string instructions;

        public string Instructions
        {
            get { return instructions; }
            set
            {
                instructions = value;
                OnPropertyChanged(x => x.Instructions);
            }
        }

        private RecipeStatus recipeStatus;
        public RecipeStatus RecipeStatus
        {
            get { return recipeStatus; }
            set
            {
                recipeStatus = value;
                OnPropertyChanged(x => x.RecipeStatus);
                OnPropertyChanged(x => x.CanSubmit);
            }
        }

        public bool CanSubmit { get { return recipeStatus == RecipeStatus.Draft; } }

        private void OnPropertyChanged(Expression<Func<RecipeViewModelState, object>> expression)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(expression.GetPropertyName()));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        public void Initialize(Contracts.Projections.RecipeView view)
        {
            RecipeId = view.Id.Id;
            Title = view.Title;
            Instructions = view.Instructions;
            RecipeStatus = view.RecipeStatus;
        }
    }
}
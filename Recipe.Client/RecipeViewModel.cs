using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Recipes.Contracts;
using Recipes.Wires;

namespace Recipes.Client
{
    public class RecipeViewModel
    {
        private readonly Action<RecipeViewModelState> continuation;
        public IRecipeView View { get; private set; }
        public RecipeViewModelState State { get; set; }

        public RecipeViewModel(IRecipeView view, Action<RecipeViewModelState> continuation, Action<RecipeViewModelState> onSubmit)
        {
            this.continuation = continuation;
            View = view;
            State = new RecipeViewModelState
                        {
                            CancelCommand = new DelegateCommand(obj => view.Close()),
                            SaveCommand = new DelegateCommand(obj => OnSave()),
                        };
            State.SubmitCommand = new DelegateCommand<RecipeViewModelState>(obj => onSubmit(State), State, x => x.CanSubmit);
            view.SetContext(State);
            view.FocusTitle();
        }

        private void OnSave()
        {
            if (!Validate()) return;
            var tempId = Guid.NewGuid();
            var command = new CreateDraftRecipe
                              {
                                  Id = new RecipeId(tempId),
                                  Title = State.Title,
                                  CookingInstructions = State.Instructions
                              };
            Bus.Send(command,
                     () =>
                         {
                             State.RecipeId = tempId;
                             State.RecipeStatus = RecipeStatus.Draft;
                             View.Close();
                             continuation(State);
                         });
        }

        private bool Validate()
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(State.Title))
                errors.Add("Must enter a title.");
            if (string.IsNullOrWhiteSpace(State.Instructions))
                errors.Add("Must enter cooking instructions.");
            if (!errors.Any()) return true;

            MessageBox.Show(string.Join(Environment.NewLine, errors), "Validation Errors");
            return false;
        }
    }
}
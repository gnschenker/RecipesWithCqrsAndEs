using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Recipes.Client
{
    public class DelegateCommand : ICommand
    {
        protected Action<object> executeAction;
        protected Func<object, bool> canExecute;
        private bool canExecuteCache;

        public event EventHandler CanExecuteChanged = delegate { };

        protected DelegateCommand(){}

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute = null)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute ?? (param => true);
        }

        public bool CanExecute(object parameter)
        {
            var temp = canExecute(parameter);
            if (canExecuteCache != temp)
            {
                canExecuteCache = temp;
                TriggerCanExecuteChanged();
            }
            return canExecuteCache;
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

        public void TriggerCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    public class DelegateCommand<T> : DelegateCommand where T : INotifyPropertyChanged
    {
        public DelegateCommand(Action<object> executeAction, INotifyPropertyChanged model, Expression<Func<T, object>> canExecuteExpression)
        {
            this.executeAction = executeAction;
            var propertyName = canExecuteExpression.GetPropertyName();
            var invertValue = canExecuteExpression.IsInvertedPropertyExpression();
            var pi = model.GetType().GetProperty(propertyName);

            canExecute = obj =>
            {
                var value = (bool)pi.GetValue(model, null);
                return invertValue ? !value : value;
            };

            model.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName != propertyName) return;
                TriggerCanExecuteChanged();
            };

            // set initial state
            TriggerCanExecuteChanged();
        }
    }
}
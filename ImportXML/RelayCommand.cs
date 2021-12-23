using System;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp064.ImportXML
{
    public class RelayCommand<ParamType> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
        private Action<ParamType> methodToExecute;
        private Func<bool> canExecuteEvaluator;
        public RelayCommand(Action<ParamType> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public RelayCommand(Action<ParamType> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public void Execute(object parameter)
        {
            this.methodToExecute.Invoke((ParamType)parameter);
        }
    }
}

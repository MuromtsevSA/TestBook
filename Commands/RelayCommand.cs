using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestBook.Commands
{
    internal class RelayCommand : ICommand
    {
        Action execute;
        Func<object?, bool>? canExecute;

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            //this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute();
        }
    }
}

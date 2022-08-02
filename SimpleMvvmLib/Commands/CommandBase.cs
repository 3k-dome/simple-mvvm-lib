using System;
using System.Windows.Input;

namespace SimpleMvvmLib.Commands {

    public abstract class CommandBase : ICommand {

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter) => true;

        public abstract void Execute(object parameter);

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new());
    }
}

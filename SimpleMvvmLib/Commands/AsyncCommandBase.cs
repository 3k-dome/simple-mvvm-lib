using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleMvvmLib.Commands {

    public abstract class AsyncCommandBase : ICommand {

        readonly Action<Exception>? _onException;

        private bool _isExecuting;
        public bool IsExecuting {
            get { return this._isExecuting; }
            set {
                this._isExecuting = value;
                this.CanExecuteChanged?.Invoke(this.CanExecuteChanged, new());
            }
        }

        public event EventHandler CanExecuteChanged;

        public AsyncCommandBase(Action<Exception>? onException = null) {
            this._onException = onException;
        }

        public virtual bool CanExecute(object parameter) => !this.IsExecuting;

        public async void Execute(object parameter) {
            this.IsExecuting = true;
            try {
                await this.ExecuteAsync(parameter);
            }
            catch (Exception exception) {
                this._onException?.Invoke(exception);              
            }    
            this.IsExecuting = false;
        }

        protected abstract Task ExecuteAsync(object parameter);
    }
}

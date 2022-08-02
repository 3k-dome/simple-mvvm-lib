using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleMvvmLib.Commands {

    public abstract class AsyncCommandBase : ICommand {

        private readonly Action<Exception> _handler;

        private bool _isExecuting;
        public bool IsExecuting {
            get { return this._isExecuting; }
            set {
                this._isExecuting = value;
                this.CanExecuteChanged?.Invoke(this.CanExecuteChanged, new());
            }
        }

        public event EventHandler CanExecuteChanged;

        public AsyncCommandBase() : this(null) { }

        public AsyncCommandBase(Action<Exception> handler) {
            this._handler = handler;
        }

        public virtual bool CanExecute(object parameter) => !this.IsExecuting;

        public async void Execute(object parameter) {
            this.IsExecuting = true;
            try {
                await this.ExecuteAsync(parameter);
            }
            catch (Exception exception) {
                this._handler?.Invoke(exception);
            }
            this.IsExecuting = false;
        }

        protected abstract Task ExecuteAsync(object parameter);

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new());
    }
}

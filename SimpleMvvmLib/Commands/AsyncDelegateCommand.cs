using System;
using System.Threading.Tasks;

namespace SimpleMvvmLib.Commands {
    public class AsyncDelegateCommand : AsyncCommandBase {

        private readonly Func<Task> _task;
        private readonly Predicate<object> _predicate;

        public AsyncDelegateCommand(Func<Task> task) : this(task, null, null) { }

        public AsyncDelegateCommand(Func<Task> task, Predicate<object> predicate) : this(task, predicate, null) { }

        public AsyncDelegateCommand(Func<Task> task, Action<Exception> handler) : this(task, null, handler) { }

        public AsyncDelegateCommand(Func<Task> task, Predicate<object> predicate, Action<Exception> handler) : base(handler) {
            this._task = task;
            this._predicate = predicate;
        }

        public override bool CanExecute(object parameter) => (this._predicate?.Invoke(parameter) ?? true) && !this.IsExecuting;

        protected override async Task ExecuteAsync(object parameter) {
            await this._task();
        }
    }
}

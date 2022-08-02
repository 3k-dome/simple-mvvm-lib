using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvvmLib.Commands {
    public class AsyncDelegateCommand : AsyncCommandBase {

        readonly Func<Task> action;
        readonly Predicate<object>? predicate;

        public AsyncDelegateCommand(Func<Task> action) : this(action, null, null) { }

        public AsyncDelegateCommand(Func<Task> action, Predicate<object> predicate) : this(action, predicate, null) { }

        public AsyncDelegateCommand(Func<Task> action, Action<Exception> onException) : this(action, null, onException) { }

        public AsyncDelegateCommand(Func<Task> action, Predicate<object> predicate, Action<Exception> onException) : base(onException) {
            this.action = action;
            this.predicate = predicate;
        }

        public override bool CanExecute(object parameter) => this.predicate?.Invoke(parameter) ?? true && !this.IsExecuting;

        protected override async Task ExecuteAsync(object parameter) {
            await this.action();
        }
    }
}

using System;

namespace SimpleMvvmLib.Commands {

    public class DelegateCommand : CommandBase {

        private readonly Action<object> _action;
        private readonly Predicate<object> _predicate;

        public DelegateCommand(Action<object> action) : this(action, null) { }

        public DelegateCommand(Action<object> action, Predicate<object> predicate) {
            this._action = action;
            this._predicate = predicate;
        }

        public override bool CanExecute(object parameter) => this._predicate?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => this._action?.Invoke(parameter);
    }
}

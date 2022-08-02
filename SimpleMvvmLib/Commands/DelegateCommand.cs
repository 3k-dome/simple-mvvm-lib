using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvvmLib.Commands {

    public class DelegateCommand : CommandBase {

        readonly Action<object> action;
        readonly Predicate<object> predicate;

        public DelegateCommand(Action<object> action) : this(action, null) { }

        public DelegateCommand(Action<object> action, Predicate<object> predicate) {
            this.action = action;
            this.predicate = predicate;
        }

        public override bool CanExecute(object parameter) => this.predicate?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => this.action?.Invoke(parameter);
    }
}

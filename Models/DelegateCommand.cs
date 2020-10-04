using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lesson1.Models
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Func<object, bool> canExecute;
        Action<object> executeAction;
        bool canExecuteCache;

        public DelegateCommand(Action<object> executeAction,
                               Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool tempCanExecute = canExecute(parameter);
            if (canExecuteCache != tempCanExecute)
            {
                canExecuteCache = tempCanExecute;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }
            return canExecuteCache;
        }
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}

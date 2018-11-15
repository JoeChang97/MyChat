﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyChat
{
    class DelegateCommand : ICommand

    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        public event EventHandler CanExecuteChanged;
        public DelegateCommand(Action<object> execute, Predicate<object> canexecute)
        {
            _execute = execute;
            _canExecute = canexecute;

        }


        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}


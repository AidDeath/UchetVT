using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace UchetVT
{

        /// <summary>
        /// Абстрактная команда
        /// </summary>
        public abstract class Command : ICommand
        {
            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }

            public abstract bool CanExecute(object parameter);

            public abstract void Execute(object parameter);
        }

        /// <summary>
        /// Универсальная команда для вызова
        /// </summary>
        public class RelayCommand : Command
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly Action<object> _execute;

            /// <summary>
            /// 
            /// </summary>
            private readonly Func<object, bool> _canExecute;

            /// <summary>
            /// Конструктор команды
            /// </summary>
            /// <param name="execute">Вызов команды</param>
            /// <param name="canExecute">Проверка доступности команды</param>
            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(Execute));
                _canExecute = canExecute;

            }

            /// <summary>
            /// Вызов команды
            /// </summary>
            /// <param name="parameter"></param>
            /// <returns></returns>
            public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

            /// <summary>
            /// Проверка доступности команды
            /// </summary>
            /// <param name="parameter"></param>
            public override void Execute(object parameter) => _execute(parameter);
        
        }
}

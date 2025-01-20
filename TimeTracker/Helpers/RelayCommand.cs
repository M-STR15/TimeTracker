using System.Windows.Input;

namespace TimeTracker.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

		/// <summary>
		/// Určuje, zda může být příkaz vykonán.
		/// </summary>
		/// <param name="parameter">Parametr příkazu.</param>
		/// <returns>Vrací true, pokud příkaz může být vykonán; jinak false.</returns>
		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}

		/// <summary>
		/// Provede příkaz.
		/// </summary>
		/// <param name="parameter">Parametr příkazu.</param>
		public void Execute(object parameter)
		{
			_execute(parameter);
		}

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
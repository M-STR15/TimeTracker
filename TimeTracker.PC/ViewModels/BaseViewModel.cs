using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace TimeTracker.PC.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private string _title = string.Empty;

		public ICommand CloseCommand { get; private set; }

		public ICommand CloseWindowCommand { get; private set; }

		public ICommand MinimalizationCommand { get; private set; }

		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		public Window Window { get; set; }

		public BaseViewModel(string title)
		{
			CloseCommand = new Helpers.RelayCommand(cmd_Close);
			CloseWindowCommand = new Helpers.RelayCommand(cmd_CloseWindow);
			MinimalizationCommand = new Helpers.RelayCommand(cmd_minimalize);

			Title = title;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void cmd_Close(object parameter) => close();

		protected virtual void cmd_minimalize(object parameter) => minimalize();

		protected virtual void minimalize() => App.Current.MainWindow.WindowState = WindowState.Minimized;

		private void close()
		{
			App.Current.Shutdown();
		}

		/// <summary>
		/// Zavře okno.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="viewModel"> Je potřeba zadat ViewModel pro okno co se má zavřít</param>
		/// <returns>Funkce napíše, zda se podařilo okno zavřít či nikoliv.</returns>
		private bool closeWindow<T>(T viewModel)
			 where T : BaseViewModel
		{
			try
			{
				if (viewModel.Window is Window)
				{
					BlurEffect objBlur = new BlurEffect();
					objBlur.Radius = 0;
					viewModel.Window.Owner.Effect = objBlur;
				}

				viewModel.Window?.Close();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void cmd_CloseWindow(object parameter)
		{
			closeWindow<BaseViewModel>(this);
		}

		private void OnPropertyChanged(string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
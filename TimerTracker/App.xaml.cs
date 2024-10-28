using System.Windows;
using System.Windows.Input;
using TimerTracker.Stories;

namespace TimerTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _isDragging;
        private MainStory _mainStory;
        private Point _startPoint;
        private static Mutex s_mutex;

        public App()
        { }

        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            s_mutex = new Mutex(true, "TimerTracker", out createdNew);
            _mainStory = new MainStory();

            if (createdNew)
            {
                Current.MainWindow = _mainStory.ContainerStore.GetMainWindow();
                Current.MainWindow.Show();
            }
            else
            {
                // Pokud již aplikace běží, informujeme uživatele nebo zavřeme tuto instanci.
                MessageBox.Show("Aplikace je již spuštěna.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0); // Ukončíme tuto instanci aplikace.
            }
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _isDragging = true;
                _startPoint = e.GetPosition((IInputElement)sender);
                (sender as UIElement)?.CaptureMouse();
            }
        }

        private void grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                var window = Application.Current.MainWindow;
                if (window != null)
                {
                    var currentPosition = e.GetPosition(window);
                    window.Left += currentPosition.X - _startPoint.X;
                    window.Top += currentPosition.Y - _startPoint.Y;
                }
            }
        }

        private void grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            (sender as UIElement)?.ReleaseMouseCapture();
        }
    }
}
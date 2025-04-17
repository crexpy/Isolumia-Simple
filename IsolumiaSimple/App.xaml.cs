using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;

namespace IsolumiaSimple
{
    public partial class App : Application
    {
        private MainWindow _mainWindow;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _mainWindow = new MainWindow();
            _mainWindow.Visibility = Visibility.Hidden;

            ShowWarningDialog();
        }

        private void ShowWarningDialog()
        {
            var warningWindow = new Window
            {
                Title = "",
                Width = 600,
                Height = 350,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
                Foreground = Brushes.White,
                WindowStyle = WindowStyle.None,
                Topmost = true
            };

            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            var warningText = new TextBlock
            {
                Text = "DO NOT USE CHARACTERS YOU DO NOT OWN\n\n" +
                       "DBD brought back a security measure, and attempting to use a character you do not own will result in a DC penalty.\n\n" +
                       "----------------------------------------------------------------------------------------------------\n\n" +
                       "IF YOU PAID FOR THIS YOU GOT SCAMMED\n\n" +
                       "Sadly I have to add this giant warning box at the start because someone decided to sell my free unlocker - " +
                       "So yes, the tool is completely free (this is the Epic Games/Xbox version) and if you paid for it you got scammed.",
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 20, 20, 10)
            };

            var buttonContainer = new Border
            {
                Background = Brushes.Transparent,
                Padding = new Thickness(0, 10, 0, 20),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            var okButton = new Button
            {
                Content = "I UNDERSTAND",
                Width = 200,
                Height = 35,
                Background = new SolidColorBrush(Color.FromRgb(6, 151, 159)),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            okButton.MouseEnter += (sender, e) =>
            {
                okButton.Background = new SolidColorBrush(Color.FromRgb(5, 120, 130));
            };

            okButton.MouseLeave += (sender, e) =>
            {
                okButton.Background = new SolidColorBrush(Color.FromRgb(6, 151, 159));
            };

            okButton.Click += (sender, args) =>
            {
                warningWindow.DialogResult = true;
            };

            buttonContainer.Child = okButton;
            Grid.SetRow(warningText, 0);
            Grid.SetRow(buttonContainer, 1);
            mainGrid.Children.Add(warningText);
            mainGrid.Children.Add(buttonContainer);
            warningWindow.Content = mainGrid;

            if (warningWindow.ShowDialog() == true)
            {
                _mainWindow.Visibility = Visibility.Visible;
                _ = AutoUpdater.CheckForUpdateAsync("crexpy", "Isolumia-Simple");
            }
            else
            {
                Shutdown();
            }
        }
    }
}
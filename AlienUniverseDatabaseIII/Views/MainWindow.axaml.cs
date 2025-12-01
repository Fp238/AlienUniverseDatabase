using Avalonia.Controls;
using AlienUniverseDatabaseIII.ViewModels;

namespace AlienUniverseDatabaseIII.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}
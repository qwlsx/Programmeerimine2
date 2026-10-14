using System.Windows;

namespace KooliProjekt.WpfApplication;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainWindowViewModel();
        DataContext = _viewModel;
        Loaded += async (_, _) => await _viewModel.LoadDataAsync();
    }
}

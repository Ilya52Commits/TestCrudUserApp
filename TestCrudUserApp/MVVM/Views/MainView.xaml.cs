namespace TestCrudApp.MVVM.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView
{
  public MainView(CrudView view)
  {
    InitializeComponent();

    MainFrame.NavigationService.Navigate(view);
  }
}
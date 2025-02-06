using TestCrudApp.MVVM.ViewModels;

namespace TestCrudApp.MVVM.Views;

public partial class CrudView
{
  public CrudView(CrudViewModel viewModel)
  {
    InitializeComponent();

    // Установка DataContext
    DataContext = viewModel;
  }
}
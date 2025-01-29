using TestCrudApp.MVVM.ViewModels;

namespace TestApp.MVVM.Views;

public partial class CrudView
{
    public CrudView(CrudViewModel viewModel)
    {
        InitializeComponent();

        // Установка DataContext
        DataContext = viewModel;
    }
}
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCrudApp.MVVM.ViewModels;
using TestCrudApp.MVVM.Views;
using TestCrudAppDomain;

namespace TestCrudApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
  protected override void OnStartup(StartupEventArgs e)
  {
    // Загрузка конфигурации
    var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

    var serviceCollection = new ServiceCollection();
    ServiceConfigurator.ConfigureServices(serviceCollection, configuration);

    // Добавление ViewModel и View
    serviceCollection.AddTransient<CrudViewModel>();
    serviceCollection.AddTransient<CrudView>();

    var mainView = new MainView(serviceCollection.BuildServiceProvider().GetRequiredService<CrudView>());

    MainWindow = mainView;

    MainWindow.Show();
  }
}
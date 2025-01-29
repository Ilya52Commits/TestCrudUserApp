using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAppDomain.Exceptions;
using TestAppDomain.Repositories.Interfaces;
using TestAppDomain.Repositories.Repositories;
using TestAppDomain.Services;
using TestCrudAppDomain.EntityFramework;
using TestCrudAppDomain.Repositories.Repositories;

namespace TestCrudAppDomain;

public static class ServiceConfigurator
{
    /// <summary>
    ///     Логика конфигурации сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="configuration">Конфиг</param>
    /// <exception cref="ArgumentException">Ошибка конфигурации</exception>
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Добавление контекста базы данных
        services.AddDbContext<Context>(options =>
        {
            var dataProvider = configuration["DataProvider"];

            switch (dataProvider)
            {
                case "Sql":
                    options.UseSqlServer(configuration["ConnectionString"]);
                    break;
            }
        });

        // Добавление репозитория в зависимости от DataProvider
        var provider = configuration["DataProvider"];

        switch (provider)
        {
            case "Sql":
                services.AddSingleton<IUserRepository, SqlUserRepository>();
                break;
            case "Xml":
                services.AddSingleton<IUserRepository, XmlUserRepository>(_ => new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration["XmlFilePath"]!)));
                break;
            case "InMemory":
                services.AddSingleton<IUserRepository, InMemoryUserRepository>();
                break;
            default:
                throw new DataProviderConfigurationException("Invalid DataProvider specified in configuration.");
        }

        // Регистрация UserService
        services.AddSingleton<UserService>();
    }
}
namespace TestCrudAppDomain.Exceptions;

/// <summary>
///     Ошибка конфигурации провайдера данных
/// </summary>
/// <param name="message"></param>
public class DataProviderConfigurationException(string message) : Exception(message);
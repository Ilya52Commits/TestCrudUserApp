namespace TestAppDomain.Exceptions;

/// <summary>
///     Ошибка валидации пользователя
/// </summary>
/// <param name="message"></param>
public class UserValidationException(string message):Exception(message);
namespace TestCrudAppDomain.Data;

/// <summary>
///     Класс результата валидации
/// </summary>
public class ValidationResult
{
  /// <summary>
  ///     Флаг валидности
  /// </summary>
  public bool IsValid { get; set; }

  /// <summary>
  ///     Текст ошибки валидации
  /// </summary>
  public string Message { get; set; } = null!;
}
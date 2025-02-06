using TestCrudAppDomain.Data;
using TestCrudAppDomain.EntityFramework.Models;
using TestCrudAppDomain.Exceptions;
using TestCrudAppDomain.Repositories.Interfaces;

namespace TestCrudAppDomain.Services;

public class UserService(IUserRepository userRepository)
{
  private const string LoginCanNotBeEmptyValidationText = "Логин не может быть пустым.";
  private const string LoginMustBeUniqueValidationText = "Логин должен быть уникальным.";
  private const string UserValidValidationText = "Пользователь валиден.";

  /// <summary>
  ///     Логика добавления пользователя
  /// </summary>
  /// <param name="user">Пользователь для добавления</param>
  /// <exception cref="UserValidationException">Ошибка валидации данных пользователя</exception>
  public async Task AddUserAsync(User user)
  {
    var isUserValid = await IsUserValid(user);

    if (!isUserValid.IsValid)
      throw new UserValidationException(isUserValid.Message);

    await userRepository.AddAsync(user);
  }

  /// <summary>
  ///     Логика получения всех пользователей
  /// </summary>
  /// <returns></returns>
  public Task<IEnumerable<User>> GetAllUsersAsync()
  {
    return userRepository.GetAllAsync();
  }

  /// <summary>
  ///     Логика обновления пользователя
  /// </summary>
  /// <param name="updatableUser">Пользователь для обновления</param>
  /// <param name="updatedUser">Новые данные для обновления</param>
  /// <exception cref="UserValidationException">Ошибка валидации данных пользователя</exception>
  public async Task UpdateUserAsync(User updatableUser, User updatedUser)
  {
    if (updatableUser.Login != updatedUser.Login)
    {
      var isUserValid = await IsUserValid(updatedUser);

      if (!isUserValid.IsValid)
        throw new UserValidationException(isUserValid.Message);
    }

    updatableUser.Login = updatedUser.Login;
    updatableUser.FirstName = updatedUser.FirstName;
    updatableUser.LastName = updatedUser.LastName;

    await userRepository.UpdateAsync(updatableUser);
  }

  /// <summary>
  ///     Логика удаления пользователя
  /// </summary>
  /// <param name="id">Id для удаления</param>
  public async Task DeleteUserAsync(Guid id)
  {
    await userRepository.DeleteAsync(id);
  }

  /// <summary>
  ///     Логика проверки валидности данных пользователя
  /// </summary>
  /// <param name="user">Пользователь для проверки</param>
  /// <returns></returns>
  private async Task<ValidationResult> IsUserValid(User user)
  {
    var result = new ValidationResult();

    if (string.IsNullOrWhiteSpace(user.Login))
    {
      result.IsValid = false;
      result.Message = LoginCanNotBeEmptyValidationText;
      return result;
    }

    // Проверка на уникальность логина
    var existingUser = (await userRepository.GetAllAsync()).FirstOrDefault(u => u.Login == user.Login);

    if (existingUser != null)
    {
      result.IsValid = false;
      result.Message = LoginMustBeUniqueValidationText;
      return result;
    }

    // Если все проверки пройдены
    result.IsValid = true;
    result.Message = UserValidValidationText;
    return result;
  }
}
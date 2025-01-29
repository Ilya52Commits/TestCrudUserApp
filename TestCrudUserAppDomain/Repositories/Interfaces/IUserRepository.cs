using TestAppDomain.EntityFramework.Models;

namespace TestAppDomain.Repositories.Interfaces;

/// <summary>
///     Репозиторий для работы с данными
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     Логика добавления пользователя
    /// </summary>
    /// <param name="user">Пользователь для добавления</param>
    /// <returns></returns>
    public Task AddAsync(User user);

    /// <summary>
    ///     Логика получения пользователя по Id
    /// </summary>
    /// <param name="id">Id для поиска</param>
    /// <returns></returns>
    public Task<User?> GetByIdAsync(Guid id);

    /// <summary>
    ///     Логика получения всех пользователей
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    ///     Логика обновления пользователя
    /// </summary>
    /// <param name="user">Пользователь для обновления</param>
    /// <returns></returns>
    public Task UpdateAsync(User user);

    /// <summary>
    ///     Логика удаления пользователя по Id
    /// </summary>
    /// <param name="id">Id для удаления</param>
    /// <returns></returns>
    public Task DeleteAsync(Guid id);
}
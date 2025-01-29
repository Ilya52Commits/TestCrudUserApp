using TestAppDomain.EntityFramework.Models;
using TestAppDomain.Repositories.Interfaces;

namespace TestAppDomain.Repositories.Repositories;

internal class InMemoryUserRepository:IUserRepository
{
    private readonly List<User> _users = [];

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(Guid id) { return Task.FromResult(_users.FirstOrDefault(u => u.Id == id)); }

    public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult<IEnumerable<User>>(_users);

    public Task UpdateAsync(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);

        if (existingUser == null)
            return Task.CompletedTask;

        existingUser.Login = user.Login;
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user != null)
            _users.Remove(user);

        return Task.CompletedTask;
    }
}
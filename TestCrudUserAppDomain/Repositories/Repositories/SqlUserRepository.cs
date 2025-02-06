using Microsoft.EntityFrameworkCore;
using TestCrudAppDomain.EntityFramework;
using TestCrudAppDomain.EntityFramework.Models;
using TestCrudAppDomain.Repositories.Interfaces;

namespace TestCrudAppDomain.Repositories.Repositories;

internal class SqlUserRepository(Context context) : IUserRepository
{
  private readonly DbSet<User> _dbSet = context.Set<User>();

  public async Task<IEnumerable<User>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<User?> GetByIdAsync(Guid id)
  {
    return await _dbSet.FindAsync(id);
  }

  public async Task AddAsync(User item)
  {
    await _dbSet.AddAsync(item);
    await context.SaveChangesAsync();
  }

  public async Task UpdateAsync(User item)
  {
    _dbSet.Update(item);
    await context.SaveChangesAsync();
  }

  public async Task DeleteAsync(Guid id)
  {
    var user = await _dbSet.FindAsync(id);

    if (user == null)
      return;

    _dbSet.Remove(user);

    await context.SaveChangesAsync();
  }
}
using Microsoft.EntityFrameworkCore;
using TestCrudAppDomain.EntityFramework.Models;

namespace TestCrudAppDomain.EntityFramework;

public sealed class Context : DbContext
{
  public Context(DbContextOptions<Context> options) : base(options)
  {
    Database.EnsureCreated();
  }

  public DbSet<User> Users { get; set; }
}
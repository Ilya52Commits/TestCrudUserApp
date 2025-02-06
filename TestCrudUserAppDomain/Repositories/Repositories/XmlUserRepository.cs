using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestCrudAppDomain.EntityFramework.Models;
using TestCrudAppDomain.Repositories.Interfaces;

namespace TestCrudAppDomain.Repositories.Repositories;

internal class XmlUserRepository : IUserRepository
{
  private const string UsersElementName = "Users";
  private const string Id = "Id";
  private const string Login = "Login";
  private const string FirstName = "FirstName";
  private const string LastName = "LastName";
  private readonly string _filePath;

  public XmlUserRepository(string filePath)
  {
    _filePath = filePath;
    CreateXmlFileIfNotExists();
  }

  public async Task AddAsync(User user)
  {
    var users = (await GetAllAsync()).ToList(); // Преобразуем в List<User>

    users.Add(user);

    await SaveUsersAsync(users);
  }

  public async Task<User?> GetByIdAsync(Guid id)
  {
    var users = await GetAllAsync();
    return users.FirstOrDefault(u => u.Id == id);
  }

  public Task<IEnumerable<User>> GetAllAsync()
  {
    // Загружаем XML-документ
    var document = XDocument.Load(_filePath);

    // Извлекаем всех пользователей
    var users = document.Descendants("User")
      .Select(userElement => new User
      {
        Id = Guid.Parse(userElement.Element(Id)?.Value!),
        Login = userElement.Element(Login)?.Value!,
        FirstName = userElement.Element(FirstName)?.Value!,
        LastName = userElement.Element(LastName)?.Value!
      });

    return Task.FromResult(users.AsEnumerable()); // Преобразуем в IEnumerable
  }

  public async Task UpdateAsync(User user)
  {
    var users = (await GetAllAsync()).ToList(); // Преобразуем в List<User>
    var existingUser = users.FirstOrDefault(u => u.Id == user.Id);

    if (existingUser != null)
    {
      existingUser.Login = user.Login;
      existingUser.FirstName = user.FirstName;
      existingUser.LastName = user.LastName;
      await SaveUsersAsync(users);
    }
  }

  public async Task DeleteAsync(Guid id)
  {
    var users = (await GetAllAsync()).ToList(); // Преобразуем в List<User>
    var userToRemove = users.FirstOrDefault(u => u.Id == id);

    if (userToRemove != null)
    {
      users.Remove(userToRemove);
      await SaveUsersAsync(users);
    }
  }

  private void CreateXmlFileIfNotExists()
  {
    if (File.Exists(_filePath))
      return;

    // Получаем директорию из пути к файлу
    var directoryPath = Path.GetDirectoryName(_filePath);

    // Проверяем, существует ли директория, если нет - создаем её
    if (directoryPath != null && !Directory.Exists(directoryPath))
      Directory.CreateDirectory(directoryPath);

    var usersElement = new XElement(UsersElementName);
    var document = new XDocument(usersElement);
    document.Save(_filePath);
  }

  private async Task SaveUsersAsync(List<User> users)
  {
    await using var stream = new FileStream(_filePath, FileMode.Create);

    var serializer = new XmlSerializer(typeof(List<User>));
    serializer.Serialize(stream, users);
  }
}
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestCrudAppDomain.EntityFramework.Models;
using TestCrudAppDomain.Exceptions;
using TestCrudAppDomain.Services;

namespace TestCrudApp.MVVM.ViewModels;

public partial class CrudViewModel : ObservableObject
{
  private readonly UserService _userService;
  [ObservableProperty] private string _firstName = string.Empty;
  [ObservableProperty] private bool _isUpdating;
  [ObservableProperty] private string _lastName = string.Empty;

  [ObservableProperty] private string _login = string.Empty;
  [ObservableProperty] private User? _selectedUser;
  [ObservableProperty] private ObservableCollection<User> _users = [];

  public CrudViewModel(UserService userService)
  {
    _userService = userService;

    _ = LoadUsers();
  }

  /// <summary>
  ///     Загрузка пользователей
  /// </summary>
  private async Task LoadUsers()
  {
    Users = new ObservableCollection<User>(await _userService.GetAllUsersAsync());
  }

  /// <summary>
  ///     Логика выбора пользователя
  /// </summary>
  /// <param name="user"></param>
  [RelayCommand]
  private void SelectUser(User user)
  {
    Login = user.Login;
    FirstName = user.FirstName;
    LastName = user.LastName;

    IsUpdating = true;
    SelectedUser = user;
  }

  /// <summary>
  ///     Логика добавления пользователя
  /// </summary>
  [RelayCommand]
  private async Task AddUserAsync()
  {
    var newUser = new User
    {
      Id = Guid.NewGuid(),
      Login = Login,
      FirstName = FirstName,
      LastName = LastName
    };

    try
    {
      await _userService.AddUserAsync(newUser);
      Users.Add(newUser);
    }
    catch (UserValidationException ex)
    {
      // Обработка ошибок валидации
      MessageBox.Show($"Ошибка валидации пользователя \"{ex.Message}\"", "Ошибка", MessageBoxButton.OK,
        MessageBoxImage.Error);
    }
  }

  /// <summary>
  ///     Логика обновления пользователя
  /// </summary>
  [RelayCommand]
  private async Task UpdateUserAsync()
  {
    var updatedUser = new User
    {
      Id = Guid.NewGuid(),
      Login = Login,
      FirstName = FirstName,
      LastName = LastName
    };

    try
    {
      if (SelectedUser != null)
        await _userService.UpdateUserAsync(SelectedUser, updatedUser);
    }
    catch (UserValidationException ex)
    {
      MessageBox.Show($"Ошибка валидации пользователя \"{ex.Message}\"", "Ошибка", MessageBoxButton.OK,
        MessageBoxImage.Error);
    }

    SelectedUser = null;
    Login = string.Empty;
    FirstName = string.Empty;
    LastName = string.Empty;
    IsUpdating = false;

    await LoadUsers();
  }

  /// <summary>
  ///     Логика удаления пользователя
  /// </summary>
  [RelayCommand]
  private async Task DeleteUserAsync(User user)
  {
    await _userService.DeleteUserAsync(user.Id);
    Users.Remove(user);
  }
}
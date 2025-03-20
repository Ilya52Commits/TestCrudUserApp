## Задание / Task:
Необходимо разработать приложение для управления пользователями. Приложение должно реализовывать CRUD операции.
В зависимости от заказчика, данные могут храниться в разных источниках (БД, файловая система, веб-сервер и др.). Поэтому необходимо реализовать несколько поставщиков данных, например для работы с MsSql и с xml-файлом. Поставщики могут использовать тестовые in-memory данные, т.е. без обращения непосредственно к БД или файлу.
В момент выполнения приложением используется один из поставщиков данных. Предусмотреть возможность холодного (перезапуск приложения) переключения поставщика, например через файл конфигурации приложения.
Реализовать валидацию данных: одинаковый логин, пустой логин.
GUI можно использовать любой из: WinForms, WPF, ASP.NET Core.
Архитектура приложения должна позволять добавлять новые источники данных, а также безболезненно (с минимальными затратами) переходить на другой тип GUI.

You need to develop an application for user management. The application should implement CRUD operations.
Depending on the customer, data can be stored in different sources (database, file system, web server, etc.). Therefore, it is necessary to implement several data providers, for example to work with MsSql and with xml file. The providers can use test in-memory data, i.e. without accessing the database or file directly.
At runtime, one of the data providers is used by the application. Provide for the possibility of cold (application restart) switching the provider, e.g. via the application configuration file.
Realize data validation: same login, empty login.
GUI can be used any of: WinForms, WPF, ASP.NET Core.
The application architecture should allow adding new data sources, as well as painlessly (with minimal costs) migrate to another type of GUI.

---
Класс пользователя / User class:
```C#
public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```
В решении TestCrudUserApp appsettings.json хранятся тип контекста данных (InMemory, Xml, Sql), строка подключения к MS SQL, а также путь к xml-файлу.
Для взаимодействия есть поля ввода логина, имени и фамилии. После добавления пользователь отображается в списке ниже. Чтобы изменить данные у пользователя, необходимо нажать на кнопку "Выбрать" у необходимого пользователя, после этого данные перенесутся в поля воода, а кнопка поменяется с "Добавить" на "Обновить", вводите необходимые изменеия и нажимаете "Обновить".

The TestCrudUserApp appsettings.json solution stores the data context type (InMemory, Xml, Sql), the MS SQL connection string, and the path to the xml file.
There are login, first name and last name input fields for interaction. Once added, the user is displayed in the list below. To change the data of the user, you need to click on the “Select” button of the necessary user, after that the data will be transferred to the fields, and the button will change from “Add” to “Update”, enter the necessary changes and click “Update”.

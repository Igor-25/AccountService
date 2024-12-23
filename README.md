DDD REST WebAPI сервис для работы с учетными записями.

Методы сервиса выполняют следующий функционал:

1. Создание пользователя

2. Получение существующего пользователя по ID

3. Поиск пользователя по нескольким полям

4. Модель пользователя: ID, Фамилия, Имя, Отчество, Дата рождения, Номер паспорта, Телефон, Электронная почта, Адрес

5. При создании пользователя поля проходят валидацию:
- Номер паспорта имеет формат XXXX XXXXXX
- Телефон имеет формат 7ХХХХХХХХХХ
- Электронная почта должна иметь правильный формат.

6. Пользователь создается из разных приложений. Тип приложения определяется по обязательному HTTP-заголовку X-Device. В зависимости от переданного заголовка меняется набор обязательных полей для создания пользователя:   
- mail - обязательны только имя и электронная почта 
- mobile - обязательный только номер телефона
- web - обязательно ввести все поля, но необязательно электронную почту и адрес.

7. Электронная почта и телефон должны быть уникальными, создание пользователя с повторениями возвращает ошибку.

8. Поля, по которым можно искать пользователя: фамилия, имя, отчество, телефон, электронная почта. Поиск можно вести по одному, либо по нескольким полям из этого списка.

9. Основной стек для реализации: .NET 8, ASP.NET Core, EF Core, SQL Server

10. Логика приложения покрыта тестами.

11. Присутствует миграция БД, а также первоначальные данные для проверки работоспособности проекта.

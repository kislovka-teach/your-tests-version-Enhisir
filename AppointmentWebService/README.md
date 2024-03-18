
### Модели
- Специализация
    - Название

- Врач (1-1 пользователь)
    - Имя
    - Фамилия
    - Специализация

- Пациент (1-1 пользователь)
    - Имя
    - Фамилия

- Пользователь
    - Логин
    - Пароль
    - Role (Patient | Doctor)

- Прием
    - Дата
    - Врач
    - Пациент
    - Был ли? (IsSuccessful)
    - Заключение? (Finding)

### Эндпоинты

1. /login войти. Вход осуществляется по ASP.NET Core Identity

2. /register/patient - регистрация пациентов

3. /register/doctor - регистрация врачей

4. /doctors(?spec={specid}) - получить список докторов. При spec={specid} - фильтрация по конкретной специальности c идентификатором specid. Доступен только пациенту.

5. /doctors/{doctorUserName} - получить конкретного доктора. Доступен только пациенту.

6. GET /visits - список приемов у врачей. Доступен только пациенту.

7. POST /doctors/{doctorUserName}/newVisit - записаться на прием. Доступен только пациенту.

9. /patients - получить список пациентов, которые когда-то записывались к врачу. Доступен только врачам.

9. GET /patients/{patientUserName}/visits - список визитов (всех, удачных и неудачных) конкретного пациента. Доступен только врачу.

10. PATCH /patients/{patientUserName}/visits/{visitId}/accept  - заполнить новую заметку о приеме. Может быть создан, если запись присутствует и она зарегана на сегодня.


Для каждой сущности должен существовать собственный репозиторий
Для хэширования и проверки паролей должен существовать отдельный сервис.

Должна быть настроена фильтрация по ролям.
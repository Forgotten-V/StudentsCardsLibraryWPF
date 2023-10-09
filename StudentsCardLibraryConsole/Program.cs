using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;
using System.Security.Cryptography;

var AddRussian = new JsonSerializerOptions
{
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
    WriteIndented = true
};

string UserLibraryPath = Directory.GetCurrentDirectory();
UserLibraryPath = UserLibraryPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
UserLibrary User = ULManager.InitializateUserLibrary();


string[] UserCardCreateTemplate = { "Введите фамилию студента: ", "Введите имя студента: ", "Введите отчество студента: ", "Введите факультет студента: ", "Введите специальность студента: ", "Введите группу студента: ", "Введите курс студента: ", "Введите город проживания студента: ", "Введите E-mail студента: ", "Введите номер телефона студента: " };//Массив-шаблон, используемый при создании или редактировании карточки студента
string[] UserCard = { "Фамилия: ", "Имя: ", "Отчество: ", "Факультет: ", "Специальность: ", "Группа: ", "Курс: ", "Город проживания: ", "E-mail: ", "Номер телефона: " };//Массив-шаблон, создающий структуру файла карточки студента при её создании или редактировании

int UserNumbers = User.Numbers();



void StartPage()
{
    Console.Clear();
    Console.WriteLine("Добро пожаловать. Выберете действие.\n1. Просмотр списка студентов.\n2. Создать карточку студента.\n0. Закрыть приложение\n");
    string UserInput = Console.ReadLine().ToLower();
    if (UserInput == "1" || UserInput == "view" || UserInput == "просмотр")//Проверка полученного от пользователя значения и перенаправление его в необходимое окно программы
    {
        PickFilterMetod();
    }
    else if (UserInput == "2" || UserInput == "create" || UserInput == "создать")
    {
        CreateUser();
    }
    else if (UserInput == "0" || UserInput == "close" || UserInput == "закрыть" || UserInput == "exit" || UserInput == "выход")
    {
        Environment.Exit(0);
    }
    else
    {
        StartPage();
    }
}//Метод, отображающий главную страницу программы.

void CreateUser()
{
    Console.Clear();
    string PrevievInformation = "";
    string FileName = "";
    string[] UserInformation = new string[10];
    for (int i = 0; i < 10; i++)//Цикл, использующий массивы-шаблоны для создания карточки студента.
    {
        Console.WriteLine("Чтобы выйти из создания карточки студента, в любой момент введите 'Выход' или '0'\n");
        Console.WriteLine(UserCardCreateTemplate[i]);
        string CreateInput = Console.ReadLine();
        if (CreateInput == "0" || CreateInput == "Выход" || CreateInput == "Exit")//Условный оператор, позволяющий в любой момент прекратить создание карточки студента и вернуться в главное меню.
        {
            StartPage();
        }
        else
        {
            UserInformation[i] = CreateInput;
            PrevievInformation = PrevievInformation + "\n" + UserCard[i] + CreateInput;
            Console.Clear();
        }
        if (i == 0)//Условный оператор, генерирующий имя файла и его путь на основе введённых пользователем данных.
        {
            FileName = FileName + CreateInput + " ";
        }
        else if (i == 1 || i == 2)
        {
            FileName = FileName + CreateInput[0] + ".";
        }
        else if (i == 5)
        {
            FileName = FileName + " " + CreateInput;
        }
    }
    Console.Clear();
    Console.WriteLine("Проверьте результат и подтвердите создание карточки " + FileName + "\n" + PrevievInformation + "\n\n1. Подтвердить создание.\n2. Удалить и вернуться в главное меню.\n");//Заключительная часть создания карточки создания, необходимая для проверки введённых пользователем данных.
    string AcceptInput = Console.ReadLine().ToLower();
    if (AcceptInput == "1" || AcceptInput == "создать" || AcceptInput == "create" || AcceptInput == "accept" || AcceptInput == "принять")
    {
        UserNumbers++;
        User.ULibrary.Add(new UserCards
        {
            ID = UserNumbers,
            FIO = FileName,
            Surname = UserInformation[0],
            Name = UserInformation[1],
            LastName = UserInformation[2],
            Faculty = UserInformation[3],
            Speciality = UserInformation[4],
            Group = UserInformation[5],
            Course = UserInformation[6],
            City = UserInformation[7],
            Email = UserInformation[8],
            Phone = UserInformation[9]
        });
        ULManager.AddUser(User);
        StartPage();
    }
    else
    {
        StartPage();
    }
}//Метод, позволяющий создавать карточку студента и сохранять её в предназначенную для этого папку.

void EditUser(int UserID)
{
    Console.Clear();
    UserCards CurrentUser = User.ULibrary.FirstOrDefault(u => u.ID == UserID);
    string[] UserInformation = new string[10];
    UserInformation[0] = CurrentUser.Surname;
    UserInformation[1] = CurrentUser.Name;
    UserInformation[2] = CurrentUser.LastName;
    UserInformation[3] = CurrentUser.Faculty;
    UserInformation[4] = CurrentUser.Speciality;
    UserInformation[5] = CurrentUser.Group;
    UserInformation[6] = CurrentUser.Course;
    UserInformation[7] = CurrentUser.City;
    UserInformation[8] = CurrentUser.Email;
    UserInformation[9] = CurrentUser.Phone;
    string CurrentCardName = "Карточка студента " + CurrentUser.FIO;
    for (int i = 0; i < 10; i++)//Цикл, поочередно показывающий каждый параметр студента пользователю.
    {
        Console.WriteLine(CurrentCardName + "\n" + UserCard[i] + UserInformation[i] + "\n" + UserCardCreateTemplate[i][..^2] + ", если хотите изменить текущее значение.\nНичего не вводите и нажмите Enter, если ничего не хотите менять.");
        Console.WriteLine(UserCardCreateTemplate[i]);
        string EditInput = Console.ReadLine();
        if (EditInput == "0" || EditInput == "Выход" || EditInput == "Exit")
        {
            StartPage();
        }
        else if (EditInput == "" || EditInput == null)//Если пользователь ничего не введёт, то остаются старые значения файла.
        {
            Console.Clear();
        }
        else//Новые введённые пользователем данные заменяются на старые.
        {
            UserInformation[i] = EditInput;
            Console.Clear();
        }
    }
    string NewCardName = UserInformation[0] + " " + UserInformation[1][0] + ". " + UserInformation[2][0] + ". " + UserInformation[5];
    Console.Clear();
    Console.WriteLine($"Карточка студента {NewCardName}");
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine(UserCard[i] + UserInformation[i]);
    }
    Console.WriteLine("Проверьте результат и подтвердите редактирование карточки" + "\n1. Подтвердить редактирование.\n2. Отменить редактирование и вернуться в главное меню.\n");
    string AcceptInput = Console.ReadLine().ToLower();
    if (AcceptInput == "1" || AcceptInput == "редактировать" || AcceptInput == "edit" || AcceptInput == "accept" || AcceptInput == "принять")//Если пользователя устраивают изменения, то сначала удаляется старый файл карточки студента и на его месте записывается на его месте записывается новый файл карточки студента. После чего пользователя возвращает на главный экран.
    {
        UserCards EditedUser = new UserCards
        {
            ID = UserID,
            FIO = NewCardName,
            Surname = UserInformation[0],
            Name = UserInformation[1],
            LastName = UserInformation[2],
            Faculty = UserInformation[3],
            Speciality = UserInformation[4],
            Group = UserInformation[5],
            Course = UserInformation[6],
            City = UserInformation[7],
            Email = UserInformation[8],
            Phone = UserInformation[9]
        };
        ULManager.UpdateUser(EditedUser);
        User = ULManager.InitializateUserLibrary();
        StartPage();
    }
    else
    {
        StartPage();
    }
}//Метод, позволяющий редактировать уже существующие карточки студентов. Принимает в качестве аргумента старую информацию о студенте и путь к редактируемой карточке студента.

void InvalidInput()
{
    Console.Clear();
    Console.WriteLine("Было введено неверное значение. Для возвращения на начальную страницу введите любое значение: ");
    Console.ReadLine();
    StartPage();
}//Метод, на который будут ссылаться другие методы, в случае введённых пользователем некорректных значений. После ввода любого значения он возвращает на стартовую страницу. (Использовался в процессе написания программы, в качестве заглушки на случай непредвиденной ситуации и тестирования программы)

void VievUserCard(string IdInput)
{
    Console.Clear();
    int UserID = int.Parse(IdInput) - 1;
    UserCards CurrentUser = User.ULibrary.FirstOrDefault(u => u.ID == UserID);
    string UserCardView = $"Карточка студента {CurrentUser.FIO}\n";
    string[] UserInformation = new string[10];
    UserInformation[0] = CurrentUser.Surname;
    UserInformation[1] = CurrentUser.Name;
    UserInformation[2] = CurrentUser.LastName;
    UserInformation[3] = CurrentUser.Faculty;
    UserInformation[4] = CurrentUser.Speciality;
    UserInformation[5] = CurrentUser.Group;
    UserInformation[6] = CurrentUser.Course;
    UserInformation[7] = CurrentUser.City;
    UserInformation[8] = CurrentUser.Email;
    UserInformation[9] = CurrentUser.Phone;
    for (int i = 0; i < 10; i++)
    {
        UserCardView = UserCardView + UserCard[i] + UserInformation[i] + "\n";
    }
    Console.WriteLine(UserCardView + "\n1. Редактировать карточку студента.\n2. Удалить карточку студента.\n0. Вернуться в главное меню.\nВведите дальнейшее действие:");
    string UserInput = Console.ReadLine().ToLower();
    if (UserInput == "0" || UserInput == "close" || UserInput == "закрыть" || UserInput == "exit" || UserInput == "выход")
    {
        StartPage();
    }
    else if (UserInput == "1" || UserInput == "редактировать" || UserInput == "edit")//Ссылка на метод с редактированием карточки студента, если пользователь выбрал этот вариант.
    {
        EditUser(UserID);
    }
    else if (UserInput == "2" || UserInput == "delete" || UserInput == "удалить")//Подтверждение удаления карточки студента, с возможностью просмотра удаляемой информации. В случае отказа пользователь будет вновь направлен на текущую карточку студента. В случае соглашения карточку удаляется и пользователь возвращается на стартовую страницу.
    {
        Console.WriteLine("Подтвердите удаление карточки студента.\n1. Удалить.\n2. Отменить");
        string UserDecision = Console.ReadLine().ToLower();
        if (UserDecision == "1" || UserDecision == "confirm" || UserDecision == "подтвердить" || UserDecision == "delete" || UserDecision == "удалить")
        {
            UserCards LastIDUser = User.ULibrary.FirstOrDefault(u => u.ID == UserNumbers);
            string[] LastUser = new string[11];
            LastUser[0] = LastIDUser.FIO;
            LastUser[1] = LastIDUser.Surname;
            LastUser[2] = LastIDUser.Name;
            LastUser[3] = LastIDUser.LastName;
            LastUser[4] = LastIDUser.Faculty;
            LastUser[5] = LastIDUser.Speciality;
            LastUser[6] = LastIDUser.Group;
            LastUser[7] = LastIDUser.Course;
            LastUser[8] = LastIDUser.City;
            LastUser[9] = LastIDUser.Email;
            LastUser[10] = LastIDUser.Phone;

            ULManager.DeleteUser(UserID);
            ULManager.DeleteUser(UserNumbers);
            User = ULManager.InitializateUserLibrary();
            User.ULibrary.Add(new UserCards
            {
                ID = UserID,
                FIO = LastUser[0],
                Surname = LastUser[1],
                Name = LastUser[2],
                LastName = LastUser[3],
                Faculty = LastUser[4],
                Speciality = LastUser[5],
                Group = LastUser[6],
                Course = LastUser[7],
                City = LastUser[8],
                Email = LastUser[9],
                Phone = LastUser[10]
            });
            ULManager.AddUser(User);
            UserNumbers--;
            StartPage();
        }
        else
        {
            VievUserCard(IdInput);
        }
    }
    else
    {
        VievUserCard(IdInput);
    }
}//Метод, позволяющий просматривать карточки студентов. Принимает в качестве аргумента путь до необходимой карточки студента.



void FilterStudents(int MethodVariant)
{
    Console.Clear();
    string[] BuferValue = new string[3];
    string[][] SortValue = new string[UserNumbers][];
    {
        for (int i = 0; i < UserNumbers; i++)
        {
            SortValue[i] = new string[3];
        }
    }
    if (MethodVariant == 1)
    {
        for (int i = 1; i < UserNumbers + 1; i++)
        {
            UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
            SortValue[i - 1][0] = TempValue.FIO;
            SortValue[i - 1][1] = TempValue.Surname;
            SortValue[i - 1][2] = $"{i + 1}";
        }
    }
    else if (MethodVariant == 2)
    {
        for (int i = 1; i < UserNumbers + 1; i++)
        {
            UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
            SortValue[i - 1][0] = TempValue.FIO;
            SortValue[i - 1][1] = TempValue.Faculty;
            SortValue[i - 1][2] = $"{i + 1}";
        }
    }
    else if (MethodVariant == 3)
    {
        for (int i = 1; i < UserNumbers + 1; i++)
        {
            UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
            SortValue[i - 1][0] = TempValue.FIO;
            SortValue[i - 1][1] = TempValue.Speciality;
            SortValue[i - 1][2] = $"{i + 1}";
        }
    }
    else if (MethodVariant == 4)
    {
        for (int i = 1; i < UserNumbers + 1; i++)
        {
            UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
            SortValue[i - 1][0] = TempValue.FIO;
            SortValue[i - 1][1] = TempValue.Group;
            SortValue[i - 1][2] = $"{i + 1}";
        }
    }
    else if (MethodVariant == 5)
    {
        for (int i = 1; i < UserNumbers + 1; i++)
        {
            UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
            SortValue[i - 1][0] = TempValue.FIO;
            SortValue[i - 1][1] = TempValue.Course;
            SortValue[i - 1][2] = $"{i + 1}";
        }
    }

    for (int i = 0; i < UserNumbers; i++)//Цикл "пузырьковой" сортировки карточек студентов в зависимости от необходимого метода сортировки.
    {
        for (int j = i + 1; j < UserNumbers; j++)
        {
            if (string.Compare(SortValue[i][1], SortValue[j][1]) > 0)//Условный оператор, внутри которого проводится сравнение по алфавиту отдельных параметров сокращённого массива. В случае получения значения false запускает цикл.
            {
                BuferValue[0] = SortValue[j][0];
                BuferValue[1] = SortValue[j][1];
                BuferValue[2] = SortValue[j][2];
                SortValue[j][0] = SortValue[i][0];
                SortValue[j][1] = SortValue[i][1];
                SortValue[j][2] = SortValue[i][2];
                SortValue[i][0] = BuferValue[0];
                SortValue[i][1] = BuferValue[1];
                SortValue[i][2] = BuferValue[2];
            }
        }
    }

    if (MethodVariant == 1)
    {
        for (int i = 1; i < UserNumbers + 1; i++)
        {
            Console.WriteLine(i + ". " + SortValue[i - 1][0]);
        }
    }
    else
    {
        string BenchmarkValue = "";//Переменная, использующая в качестве эталона сортируемый параметр.
        string PresentText = "";//Переменная, выступающая шаблоном выводимого текста для разделения критериев сортировки.
        if (MethodVariant == 2)
        {
            PresentText = "\nСтуденты факультета ";
        }
        else if (MethodVariant == 3)
        {
            PresentText = "\nСтуденты специальности ";
        }
        else if (MethodVariant == 4)
        {
            PresentText = "\nСтуденты группы ";
        }
        else if (MethodVariant == 5)
        {
            PresentText = "\nСтуденты курса ";
        }
        for (int i = 1; i < UserNumbers + 1; i++)
        {
            if (BenchmarkValue != SortValue[i - 1][1])
            {
                BenchmarkValue = SortValue[i - 1][1];
                Console.WriteLine(PresentText + BenchmarkValue + "\n");
            }
            Console.WriteLine(i + ". " + SortValue[i - 1][0]);
        }//Цикл, выводящий результат сортировки карточек студентов с отображением полученных в ходе сортировки критериев.
    }
    Console.WriteLine("\nВыберете пользователя для редактирования или удаления. Для выхода в главное меню нажмите 0:");
    string UserInput = Console.ReadLine().ToLower();
    if (UserInput == "0" || UserInput == "close" || UserInput == "закрыть" || UserInput == "exit" || UserInput == "выход")//Условный оператор, позволяющий выбрать интересующую пользователя карточку студента. В случае ввода некорректного значения
    {
        StartPage();
    }
    else
    {
        if (int.TryParse(UserInput, out int UserInputInt))//Условный оператор, который пытается преобразовать введённое пользователем значение. Если это значение не числовое или выходит за границы количества карточек студентов - программа просто заново вызывает текущий метод.
        {
            UserInputInt--;
            if (UserInputInt >= 0 && UserInputInt <= UserNumbers)
            {
                VievUserCard(SortValue[UserInputInt][2]);
            }
            else
            {
                FilterStudents(MethodVariant); ;
            }
        }
        else
        {
            FilterStudents(MethodVariant);
        }
    }
}//Метод, позволяющий сортировать по факультету, специальности, группе и курсу студента, в зависимости от принятого аргумента номера сортировки. Также как и метод сортировки по фамилии принимет ещё и массив с данными для сортировки и количество файлов в папке для карточек студентов.

void PickFilterMetod()
{
    Console.Clear();
    Console.WriteLine("Выберете метод сортировки перед просмотром списка студентов.\n1. Сортировать по фамилии\n2. Сортировать по факультету\n3. Сортировать по специальности\n4. Сортировать по группе\n5. Сортировать по текущему курсу студента\n0. Вернуться в главное меню");
    string UserInput = Console.ReadLine().ToLower();//После предварительной подготовки, пользователю наконец предоставляется возможность выбора метода сортировки файлов карточек студентов.
    if (UserInput == "1" || UserInput == "фамилия" || UserInput == "surname")//Ссылка на метод сортировки по фамилии, самый простой и по сути принимает в себя только значение количества файлов карточек студентов в папке и сокращённый массив с необходимыми для сортировки данными.
    {
        FilterStudents(1);
    }
    else if (UserInput == "2" || UserInput == "факультет" || UserInput == "faculty")//Следующие варианты предоставляют ссылку на один и тот же метод. С разницей только передаваемого третьего аргумента, от которого и будет зависеть способ сортировки файлов карточек студентов.
    {
        FilterStudents(2);
    }
    else if (UserInput == "3" || UserInput == "специальность" || UserInput == "specialty")
    {
        FilterStudents(3);
    }
    else if (UserInput == "4" || UserInput == "группа" || UserInput == "group")
    {
        FilterStudents(4);
    }
    else if (UserInput == "5" || UserInput == "курс" || UserInput == "course")
    {
        FilterStudents(5);
    }
    else if (UserInput == "0" || UserInput == "close" || UserInput == "закрыть" || UserInput == "exit" || UserInput == "выход")
    {
        StartPage();
    }
    else
    {
        PickFilterMetod();
    }
}//Метод, позволяющий выбрать способ сортировки карточек студентов перед началом их просмотра. Также предварительно создаёт все необходимые шаблоны для сортировки карточек студентов.

StartPage();//Начало программы
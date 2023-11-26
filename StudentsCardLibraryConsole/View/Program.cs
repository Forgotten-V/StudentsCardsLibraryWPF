using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;
using System.Security.Cryptography;
using StudentsCardsLibraryWPF.Model;
using StudentsCardLibraryConsole.ViewModel;

MainVM ViewModel = new MainVM();

var AddRussian = new JsonSerializerOptions          //Параметр, добавляющий возможность записывать файлы JSON с использованием кирилици. Если честно, сложно
                                                    //представить, к какому элементу MVVM его отнести. Поэтому пускай останется здесь. Да и в целом перевод
                                                    //консольного приложения на паттерн MVVM довольно спорная затея.
{
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
    WriteIndented = true
};


string[] UserCardCreateTemplate = { "Введите фамилию: ", "Введите имя: ", "Введите отчество: ", "Введите факультет: ", "Введите специальность: ", "Введите группу: ", "Введите курс: ", "Введите город проживания: ", "Введите E-mail: ", "Введите номер телефона: " };//Массив-шаблон, используемый при создании или редактировании карточки студента
string[] UserCard = { "Фамилия: ", "Имя: ", "Отчество: ", "Факультет: ", "Специальность: ", "Группа: ", "Курс: ", "Город проживания: ", "E-mail: ", "Номер телефона: " };//Массив-шаблон, создающий структуру файла карточки студента при её создании или редактировании

void StartPage()
{
    Console.Clear();
    Console.WriteLine("Добро пожаловать. Выберете действие.\n1. Просмотр списка студентов.\n2. Создать профиль студента.\n0. Закрыть приложение\n");
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
        Environment.Exit(0);        //Инициализация закрытия программы.
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
    //string FileName = "";
    string[] UserInformation = new string[10];
    for (int i = 0; i < 10; i++)//Цикл, использующий массивы-шаблоны для создания карточки студента.
    {
        
        Console.WriteLine("Чтобы выйти из создания профиля студента, в любой момент введите 'Выход' или '0'\n");
        Console.WriteLine(UserCardCreateTemplate[i]);
        string CreateInput = Console.ReadLine();
        if (CreateInput == "0" || CreateInput == "Выход" || CreateInput == "Exit")//Условный оператор, позволяющий в любой момент прекратить создание карточки студента и вернуться в главное меню.
        {
            StartPage();
        }
        else
        {
            if (ViewModel.CheckInputData(CreateInput) == true)
            {
                UserInformation[i] = CreateInput;
                PrevievInformation = PrevievInformation + "\n" + UserCard[i] + CreateInput;
                Console.Clear();
            }
            else 
            {
                i--;
                Console.Clear();
            }
        }
    }
    Console.Clear();
    Console.WriteLine($"Проверьте результат и подтвердите создание профиля студента {ViewModel.FileName(UserInformation[0], UserInformation[1], UserInformation[2], UserInformation[5])}\n{PrevievInformation} \n\n1. Подтвердить создание.\n2. Удалить и вернуться в главное меню.\n");//Заключительная часть создания карточки создания, необходимая для проверки введённых пользователем данных.
    string AcceptInput = Console.ReadLine().ToLower();
    if (AcceptInput == "1" || AcceptInput == "создать" || AcceptInput == "create" || AcceptInput == "accept" || AcceptInput == "принять")
    {
        ViewModel.LoadUserInformation(UserInformation[0], UserInformation[1], UserInformation[2], UserInformation[3], UserInformation[4], UserInformation[5], UserInformation[6], UserInformation[7], UserInformation[8], UserInformation[9]);
        StartPage();
    }
    else
    {
        StartPage();
    }
}//Метод, позволяющий создавать карточку студента и сохранять её в предназначенную для этого папку.

void EditUser()
{
    Console.Clear();
    string[] OldUserInformation = new string[10];
    OldUserInformation = ViewModel.OldUserInformation();
    string[] NewUserInformation = new string[10];
    string CurrentCardName = $"Профиль студента {OldUserInformation[0]} {OldUserInformation[1][0]}. {OldUserInformation[2][0]}. {OldUserInformation[5]}";
    for (int i = 0; i < 10; i++)//Цикл, поочередно показывающий каждый параметр студента пользователю.
    {
        Console.WriteLine(CurrentCardName + "\n" + UserCard[i] + OldUserInformation[i] + "\n" + UserCardCreateTemplate[i][..^2] + ", если хотите изменить текущее значение.\nНичего не вводите и нажмите Enter, если ничего не хотите менять.");
        Console.WriteLine(UserCardCreateTemplate[i]);
        NewUserInformation[i] = Console.ReadLine();
        if (NewUserInformation[i] == "0" || NewUserInformation[i] == "Выход" || NewUserInformation[i] == "Exit")
        {
            StartPage();
        }
        else
        {
            Console.Clear();
        }
    }
    string []FinalUserInformation = new string[10];
    FinalUserInformation = ViewModel.PreapareToEditUser(NewUserInformation);
    string NewCardName = FinalUserInformation[0] + " " + FinalUserInformation[1][0] + ". " + FinalUserInformation[2][0] + ". " + FinalUserInformation[5];
    Console.Clear();
    Console.WriteLine($"Информация о студенте {NewCardName}");
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine(UserCard[i] + FinalUserInformation[i]);
    }
    Console.WriteLine("Проверьте результат и подтвердите редактирование профиля студента" + "\n1. Подтвердить редактирование.\n2. Отменить редактирование и вернуться в главное меню.\n");
    string AcceptInput = Console.ReadLine().ToLower();
    if (AcceptInput == "1" || AcceptInput == "редактировать" || AcceptInput == "edit" || AcceptInput == "accept" || AcceptInput == "принять")//Если пользователя устраивают изменения, то сначала удаляется старый файл карточки студента и на его месте записывается на его месте записывается новый файл карточки студента. После чего пользователя возвращает на главный экран.
    {
        ViewModel.EditUserInformation(FinalUserInformation);
        StartPage();
    }
    else
    {
        StartPage();
    }
}//Метод, вызываемый для начала редактирования пользователя.

void InvalidInput()
{
    Console.Clear();
    Console.WriteLine("Было введено неверное значение. Для возвращения на начальную страницу введите любое значение: ");
    Console.ReadLine();
    StartPage();
}//Метод, на который будут ссылаться другие методы, в случае введённых пользователем некорректных значений.
 //После ввода любого значения он возвращает на стартовую страницу. (Использовался в процессе написания
 //программы, в качестве заглушки на случай непредвиденной ситуации и тестирования программы)

void VievUserCard()
{
    Console.Clear();
    string UserCardView = ViewModel.PresentUserInformation();
    Console.WriteLine(UserCardView + "\n1. Редактировать профиль студента.\n2. Удалить профиль студента.\n0. Вернуться в главное меню.\nВведите дальнейшее действие:");
    string UserInput = Console.ReadLine().ToLower();
    if (UserInput == "0" || UserInput == "close" || UserInput == "закрыть" || UserInput == "exit" || UserInput == "выход")
    {
        StartPage();
    }
    else if (UserInput == "1" || UserInput == "редактировать" || UserInput == "edit")//Ссылка на метод с редактированием карточки студента, если пользователь выбрал этот вариант.
    {
        EditUser();
    }
    else if (UserInput == "2" || UserInput == "delete" || UserInput == "удалить")//Подтверждение удаления карточки студента, с возможностью просмотра удаляемой информации. В случае отказа пользователь будет вновь направлен на текущую карточку студента. В случае соглашения карточку удаляется и пользователь возвращается на стартовую страницу.
    {
        Console.WriteLine("Подтвердите удаление студента.\n1. Удалить.\n2. Отменить");
        string UserDecision = Console.ReadLine().ToLower();
        if (UserDecision == "1" || UserDecision == "confirm" || UserDecision == "подтвердить" || UserDecision == "delete" || UserDecision == "удалить")
        {
            ViewModel.InicializateDeleteProtocol();
            StartPage();
        }
        else
        {
            VievUserCard();
        }
    }
    else
    {
        VievUserCard();
    }
}//Метод, позволяющий просматривать карточки студентов. Принимает в качестве аргумента путь до необходимой карточки студента.



void ViewUsersList ()
{
    Console.Clear();
    Console.WriteLine(ViewModel.AllUsersList());
    Console.WriteLine("\nВыберете пользователя для редактирования или удаления. Для выхода в главное меню нажмите 0:");
    string UserInput = Console.ReadLine().ToLower();
    if (UserInput == "0" || UserInput == "close" || UserInput == "закрыть" || UserInput == "exit" || UserInput == "выход")//Условный оператор, позволяющий выбрать интересующую пользователя карточку студента. В случае ввода некорректного значения
    {
        StartPage();
    }
    else
    {
        if (ViewModel.CheckUserChoise(UserInput) == true)
        {
            ViewModel.SaveUserChoice(Int32.Parse(UserInput));
            VievUserCard();
        }
        else
        { 
            ViewUsersList();
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
        ViewModel.LoadFilterMethod(0);
        ViewUsersList();
    }
    else if (UserInput == "2" || UserInput == "факультет" || UserInput == "faculty")//Следующие варианты предоставляют ссылку на один и тот же метод. С разницей только передаваемого третьего аргумента, от которого и будет зависеть способ сортировки файлов карточек студентов.
    {
        ViewModel.LoadFilterMethod(1);
        ViewUsersList();
    }
    else if (UserInput == "3" || UserInput == "специальность" || UserInput == "specialty")
    {
        ViewModel.LoadFilterMethod(2);
        ViewUsersList();
    }
    else if (UserInput == "4" || UserInput == "группа" || UserInput == "group")
    {
        ViewModel.LoadFilterMethod(3);
        ViewUsersList();
    }
    else if (UserInput == "5" || UserInput == "курс" || UserInput == "course")
    {
        ViewModel.LoadFilterMethod(4);
        ViewUsersList();
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
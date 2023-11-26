using ModelClassLibrary;
using StudentsCardsLibraryWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentsCardLibraryConsole.Model
{

    public class MainModel
    {


        //public int GetFilterMethod()                          //Функция, ранее возвращавшая метод фильтрации, записанный в файл JSON.
        //{
        //    string PathFiles = Directory.GetCurrentDirectory();
        //    string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
        //    UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
        //    UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
        //    UserAppConfig Config = UConfig.InitializateUserAppConfig();
        //    int FilterMethod;
        //    FilterMethod = Config.FilterMethod;
        //    return FilterMethod;
        //}

        //public void SaveUserID(int UserID)                    //Функция, ранее записывавшая ID пользователя в файл JSON для его последующего использования
        //В просмотре карточки пользователя, а также её редактирования и удаления.
        //{
        //    string PathFiles = Directory.GetCurrentDirectory();
        //    string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
        //    UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
        //    UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
        //    UserAppConfig Config = UConfig.InitializateUserAppConfig();
        //    Config.UserIDForView = UserID;
        //    UConfig.UpdateAppConfig(Config);
        //}

        public void CreateUser (string Surname, string Name, string Lastname, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone, string FIO)  //Функция, создающая нового пользователя и записывающая его в JSON файл. В качестве аргументов принимает все необходимые данные о пользователе.
        {
            string UserLibraryPath = Directory.GetCurrentDirectory() + "\\../../../../UserLibrary.json";   //Получение текущего расположения приложения на диске и указание пути к файлу с библиотекой пользователей.
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();     //Создание экземпляра класса с данными о пользователе.
            User.ULibrary.Add(new UserCards
            {
                ID = User.Numbers()+1,
                FIO = FIO,
                Surname = Surname,
                Name = Name,
                LastName = Lastname,
                Faculty = Faculty,
                Speciality = Speciality,
                Group = Group,
                Course = Course,
                City = City,
                Email = Email,
                Phone = Phone
            });                         //Присвоение загруженных в функцию значений переменным экземпляра класса.
            ULManager.AddUser(User);    //Вызов функции, добавляющей в файл библиотеки данные о только-что созданном пользователе.
        }

        //public void SaveFilterMethod (int FilterMethod)       //Функция, ранее записывающая выбраный метод фильтрации, в последствие используемый для созданий
                                                                //предварительно отфильтрованной базы данных пользователей.
        //{
        //    string UserAppConfigPath = Directory.GetCurrentDirectory();
        //    UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
        //    UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
        //    UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
        //    UserAppConfig Config = UConfig.InitializateUserAppConfig();
        //    Config.FilterMethod = FilterMethod;
        //    UConfig.UpdateAppConfig(Config);
        //}

        //public int GetUserIDForView()         //Старая функция, ранее возвращавшее значение записанного в JSON файл ID пользователя.
        //{
        //    string PathFiles = Directory.GetCurrentDirectory();
        //    string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
        //    UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
        //    UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
        //    UserAppConfig Config = UConfig.InitializateUserAppConfig();
        //    int UserIdForView;
        //    UserIdForView = Config.UserIDForView;
        //    return UserIdForView;
        //}

        public int GetUsersNumbers ()       //Функция, возвращающая значение текущего количества существующих в JSON файле пользователей.
        {
            string UserLibraryPath = Directory.GetCurrentDirectory() + "\\../../../../UserLibrary.json";    //Получение текущего расположения приложения на диске и указание пути к файлу с библиотекой пользователей.
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            return User.Numbers();                                  //Инициализация функции, вовзвращающей значение числа пользователей.
        }

        public void StartDeleteUser ()      //Функция, начинающая процесс удаления выбранного пользователя
        {
            //string PathFiles = Directory.GetCurrentDirectory();       //
            //string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            //UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            //UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            //UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = Directory.GetCurrentDirectory() + "\\../../../../UserLibrary.json";      //Получение текущего расположения приложения на диске и указание пути к файлу с библиотекой пользователей.
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            if (User.Numbers() != GlobalVariables.UserID)       //Чтобы не удалить лишнего пользователя проверяется значение введённого ID и общего числа пользователей.
                                                                //Действия, выполняемые в случае удовлетворения условий, позволяют ID пользователей не превратиться со временем в швейцарский сыр с пустыми ячейками.
            {
                UserCards LastIDUser = User.ULibrary.FirstOrDefault(u => u.ID == User.Numbers());       //Создание массива, хранящего в себе данные о пользователе с самым высоким значением ID.
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
                ULManager.DeleteUser(GlobalVariables.UserID);   //Удаление выбранного пользователя.
                ULManager.DeleteUser(User.Numbers());           //Удаление пользователя с самым высоким значением ID
                User = ULManager.InitializateUserLibrary();     //Создание экземпляра класса для нового пользователя
                User.ULibrary.Add(new UserCards
                {
                    ID = GlobalVariables.UserID,
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
                });                                             //Создание профиля пользователя с использованием ранее сохранённых данных
                ULManager.AddUser(User);
            }
            else
            {
                ULManager.DeleteUser(GlobalVariables.UserID);   //В случае, если был выбран пользователь с самым высоким значением ID, его карточка просто удаляется и освобождает значение ID. 
            }
        }

        public void EditUser(string Surname, string Name, string LastName, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)     //Функция, предназначенная дле редактирования информаии о уже существующем
                                                                                                                                                                                        //пользователе. Она полностью заменяет старую информацию пользователя на новую, которую
                                                                                                                                                                                        //получает в виде аргументов.
        {
            //string PathFiles = Directory.GetCurrentDirectory();
            //string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            //UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            //UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            //UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = Directory.GetCurrentDirectory() + "\\../../../../UserLibrary.json";    //Получение текущего расположения приложения на диске и указание пути к файлу с библиотекой пользователей.
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserID = GlobalVariables.UserID;
            UserCards EditedUser = new UserCards            //Заполнение информации о пользователе, которая впоследствии заменит собой старую.
            {
                ID = UserID,
                FIO = Surname + " " + Name[0] + "." + LastName[0] + "." + " " + Group,
                Surname = Surname,
                Name = Name,
                LastName = LastName,
                Faculty = Faculty,
                Speciality = Speciality,
                Group = Group,
                Course = Course,
                City = City,
                Email = Email,
                Phone = Phone
            };
            ULManager.UpdateUser(EditedUser);
        }

        public string [] GetUserInformation()       //Функция, возвращающая в виде массива всю информацию о пользователе с указаным ID.
        {
            string UserLibraryPath = Directory.GetCurrentDirectory() + "\\../../../../UserLibrary.json";    //Получение текущего расположения приложения на диске и указание пути к файлу с библиотекой пользователей.
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            UserCards CurrentUser = User.ULibrary.FirstOrDefault(u => u.ID == GlobalVariables.UserID);
            string UserCardView = $"Информация о студенте {CurrentUser.FIO}\n";
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
            return UserInformation;
        }
        public string[][] GetUsersList (int FilterMethod)           //Функция, создающая двумерный массив со всеми существующими пользователями. Принимает в
                                                                    //качестве аргумента значение фильтра, по которому в последствии будет выбран сортируемый параметр.
        {
            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            string[][] SortValue = new string[User.Numbers()][];    //Создание массива, в который в последствии будет загружена основная информация о пользователе,
                                                                    //критерий, по которому будет происходить сортировка, и его ID.
            {
                for (int i = 0; i < User.Numbers(); i++)
                {
                    SortValue[i] = new string[3];
                }
            }
            if (FilterMethod == 0)                                  //В зависимости от метода фильтрации будет присвоено своё значение во вторую ячейку.
            {
                for (int i = 1; i < User.Numbers() + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Surname;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (FilterMethod == 1)
            {
                for (int i = 1; i < User.Numbers() + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Faculty;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (FilterMethod == 2)
            {
                for (int i = 1; i < User.Numbers() + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Speciality;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (FilterMethod == 3)
            {
                for (int i = 1; i < User.Numbers() + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Group;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (FilterMethod == 4)
            {
                for (int i = 1; i < User.Numbers() + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Course;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            return SortValue;
        }
    }
}

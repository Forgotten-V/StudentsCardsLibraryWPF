using System;
using StudentsCardsLibraryWPF.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModelClassLibrary;

namespace StudentsCardsLibraryWPF.Model
{
    public class MainModel : INotifyPropertyChanged
    {
        public void CreateNewUser (string Surname, string Name, string LastName, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)   //Функция, создающая нового пользователя и записывающая его в JSON файл.
                                                                                                                                                                                            //В качестве аргументов принимает все необходимые данные о пользователе.
        {
            UserLibraryManager ULManager = new UserLibraryManager(GlobalVariables.UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();     //Создание экземпляра класса с данными о пользователе.
            string FileName = Surname + " " + Name[0] + "." + LastName[0] + "." + " " + Group;
            User.ULibrary.Add(new UserCards
            {
                ID = User.Numbers()+1,
                FIO = FileName,
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
            });                         //Присвоение загруженных в функцию значений переменным экземпляра класса.
            ULManager.AddUser(User);    //Вызов функции, добавляющей в файл библиотеки данные о только-что созданном пользователе.
            MessageBox.Show($"Карточка студента {FileName} успешно создана.");
        }

        public void EditUser (string Surname, string Name, string LastName, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)        //Функция, предназначенная дле редактирования информаии о уже существующем
                                                                                                                                                                                            //пользователе. Она полностью заменяет старую информацию пользователя на новую, которую
                                                                                                                                                                                            //получает в виде аргументов.
        {
            UserLibraryManager ULManager = new UserLibraryManager(GlobalVariables.UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserID = GlobalVariables.UserID;
            UserCards EditedUser = new UserCards                                //Заполнение информации о пользователе, которая впоследствии заменит собой старую.
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
        public void DeleteUser ()       //Функция, начинающая процесс удаления выбранного пользователя
        {
            UserLibraryManager ULManager = new UserLibraryManager(GlobalVariables.UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            if (User.Numbers() != GlobalVariables.UserID)       //Чтобы не удалить лишнего пользователя проверяется значение введённого ID и общего числа пользователей.
                                                                //Действия, выполняемые в случае удовлетворения условий, позволяют ID пользователей не превратиться со временем в швейцарский сыр с пустыми ячейками.
            {
                UserCards LastIDUser = User.ULibrary.FirstOrDefault(u => u.ID == User.Numbers());
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
                ULManager.DeleteUser(GlobalVariables.UserID);       //Удаление выбранного пользователя.
                ULManager.DeleteUser(User.Numbers());               //Удаление пользователя с самым высоким значением ID
                User = ULManager.InitializateUserLibrary();         //Создание экземпляра класса для нового пользователя
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
                });
                ULManager.AddUser(User);
            }
            else                                        //В случае, если был выбран пользователь с самым высоким значением ID, его
                                                        //карточка просто удаляется и освобождает значение ID.
            {
                ULManager.DeleteUser(GlobalVariables.UserID);   
            }
            GlobalVariables.UserID = -1;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public int GetUsersNumbers()       //Функция, возвращающая значение текущего количества существующих в JSON файле пользователей.
        {
            UserLibraryManager ULManager = new UserLibraryManager(GlobalVariables.UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            return User.Numbers();                                  //Инициализация функции, вовзвращающей значение числа пользователей.
        }

        public string [] PresentUserInformation(int UserID)     //Функция, вовзвращающая в виде массива всю информацию о
                                                                //пользователе, ID которого было введено в качестве параметра.
        {
            string[] UserInformation = new string [11];
            string PathFiles = Directory.GetCurrentDirectory();
            UserLibraryManager ULManager = new UserLibraryManager(GlobalVariables.UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            UserCards CurrentUser = User.ULibrary.FirstOrDefault(u => u.ID == UserID);
            UserInformation[0] = CurrentUser.FIO;
            UserInformation[1] = CurrentUser.Surname;
            UserInformation[2] = CurrentUser.Name;
            UserInformation[3] = CurrentUser.LastName;
            UserInformation[4] = CurrentUser.Faculty;
            UserInformation[5] = CurrentUser.Speciality;
            UserInformation[6]  = CurrentUser.Group;
            UserInformation[7] = CurrentUser.Course;
            UserInformation[8] = CurrentUser.City;
            UserInformation[9] = CurrentUser.Email;
            UserInformation[10] = CurrentUser.Phone;
            return UserInformation;
        }


        public string[][] CreateUsersBase()         //Функция, создающая базу данных о пользователе. В зависимости от выбранного метода
                                                    //фильтрации (Который зависит от варианта отображения списка пользователей) может
                                                    //создать либо полную информацию обо всех пользователях, либо лишь основные данные.
        {
            string[][] UsersListSortValueModel;
            UserLibraryManager ULManager = new UserLibraryManager(GlobalVariables.UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserNumbers = User.Numbers();
            string[] BuferValue = new string[13];
            UsersListSortValueModel = new string[UserNumbers][];
            {
                for (int i = 0; i < UserNumbers; i++)
                {
                    UsersListSortValueModel[i] = new string[13];
                }
            }
            for (int i = 1; i < UserNumbers + 1; i++)       //Все данные о пользователе загружаются только в случае выбора сортировки
                                                            //по фамилии. В остальных случая в этом нет необходимости.
            {
                UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                UsersListSortValueModel[i - 1][0] = TempValue.FIO;
                UsersListSortValueModel[i - 1][1] = TempValue.Surname;
                UsersListSortValueModel[i - 1][2] = $"{i + 1}";
                UsersListSortValueModel[i - 1][3] = TempValue.Surname;
                UsersListSortValueModel[i - 1][4] = TempValue.Name;
                UsersListSortValueModel[i - 1][5] = TempValue.LastName;
                UsersListSortValueModel[i - 1][6] = TempValue.Faculty;
                UsersListSortValueModel[i - 1][7] = TempValue.Speciality;
                UsersListSortValueModel[i - 1][8] = TempValue.Group;
                UsersListSortValueModel[i - 1][9] = TempValue.Course;
                UsersListSortValueModel[i - 1][10] = TempValue.City;
                UsersListSortValueModel[i - 1][11] = TempValue.Email;
                UsersListSortValueModel[i - 1][12] = TempValue.Phone;
            }

            for (int i = 0; i < UserNumbers; i++)       //Цикл "пузырьковой" сортировки карточек студентов в зависимости от необходимого метода сортировки.
                                                        //(Хотя для данного уровня программы пузырьковая сортировка уже выглядит не солидно)
            {
                for (int j = i + 1; j < UserNumbers; j++)
                {
                    if (string.Compare(UsersListSortValueModel[i][1], UsersListSortValueModel[j][1]) > 0)       //Условный оператор, внутри которого проводится сравнение по алфавиту отдельных параметров сокращённого массива. В случае получения значения false запускает цикл.
                    {
                        for (int k = 0; i < 13; i++)
                        {
                            BuferValue[k] = UsersListSortValueModel[j][k];
                        }
                        for (int k = 0; i < 13; i++)
                        {
                            UsersListSortValueModel[j][k] = UsersListSortValueModel[i][k];
                        }
                        for (int k = 0; i < 13; i++)
                        {
                            UsersListSortValueModel[i][k] = BuferValue[k];
                        }
                    }
                }
            }
            return UsersListSortValueModel;
        }
    }
}

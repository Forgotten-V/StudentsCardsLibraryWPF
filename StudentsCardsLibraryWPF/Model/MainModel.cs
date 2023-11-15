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

namespace StudentsCardsLibraryWPF.Model
{
    public class MainModel : INotifyPropertyChanged
    {
        public void CreateNewUser (string Surname, string Name, string LastName, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)
        {
            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserNumbers = User.Numbers();
            string FileName = Surname + " " + Name[0] + "." + LastName[0] + "." + " " + Group;
            UserNumbers++;
            User.ULibrary.Add(new UserCards
            {
                ID = UserNumbers,
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
            });
            ULManager.AddUser(User);
            MessageBox.Show($"Карточка студента {FileName} успешно создана.");
        }

        public void EditUser (string Surname, string Name, string LastName, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserID = Config.UserIDForView;
            string UserFIO = Surname + " " + Name[0] + "." + LastName[0] + "." + " " + Group;
            UserCards EditedUser = new UserCards
            {
                ID = UserID,
                FIO = UserFIO,
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
        public void DeleteUser () 
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
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

            ULManager.DeleteUser(Config.UserIDForView);
            ULManager.DeleteUser(User.Numbers());
            User = ULManager.InitializateUserLibrary();
            User.ULibrary.Add(new UserCards
            {
                ID = Config.UserIDForView,
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

       public void EditFilterMethod(int Number)
        {
            string UserAppConfigPath = Directory.GetCurrentDirectory();
            UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.FilterMethod = Number;
            UConfig.UpdateAppConfig(Config);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void SaveUserID(int UserID)
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.UserIDForView = UserID;
            UConfig.UpdateAppConfig(Config);
        }
        public int GetFilterMethod ()
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            int FilterMethod;
            FilterMethod = Config.FilterMethod;
            return FilterMethod;
        }

        public string [] PresentUserInformation(int UserID)
        {
            string[] UserInformation = new string [11];
            string PathFiles = Directory.GetCurrentDirectory();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
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
        public int GetUserIDForView ()
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            int UserIdForView;
            UserIdForView = Config.UserIDForView;
            return UserIdForView;
        }
        public string[][] CreateUsersBase ()
        {
            string[][] UsersListSortValueModel;
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserNumbers = User.Numbers();
            string[] BuferValue = new string[3];
            UsersListSortValueModel = new string[UserNumbers][];
            {
                for (int i = 0; i < UserNumbers; i++)
                {
                    UsersListSortValueModel[i] = new string[3];
                }
            }
            if (Config.FilterMethod == 0)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    UsersListSortValueModel[i - 1][0] = TempValue.FIO;
                    UsersListSortValueModel[i - 1][1] = TempValue.Surname;
                    UsersListSortValueModel[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 1)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    UsersListSortValueModel[i - 1][0] = TempValue.FIO;
                    UsersListSortValueModel[i - 1][1] = TempValue.Faculty;
                    UsersListSortValueModel[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 2)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    UsersListSortValueModel[i - 1][0] = TempValue.FIO;
                    UsersListSortValueModel[i - 1][1] = TempValue.Speciality;
                    UsersListSortValueModel[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 3)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    UsersListSortValueModel[i - 1][0] = TempValue.FIO;
                    UsersListSortValueModel[i - 1][1] = TempValue.Group;
                    UsersListSortValueModel[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 4)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    UsersListSortValueModel[i - 1][0] = TempValue.FIO;
                    UsersListSortValueModel[i - 1][1] = TempValue.Course;
                    UsersListSortValueModel[i - 1][2] = $"{i + 1}";
                }
            }

            for (int i = 0; i < UserNumbers; i++)//Цикл "пузырьковой" сортировки карточек студентов в зависимости от необходимого метода сортировки.
            {
                for (int j = i + 1; j < UserNumbers; j++)
                {
                    if (string.Compare(UsersListSortValueModel[i][1], UsersListSortValueModel[j][1]) > 0)//Условный оператор, внутри которого проводится сравнение по алфавиту отдельных параметров сокращённого массива. В случае получения значения false запускает цикл.
                    {
                        BuferValue[0] = UsersListSortValueModel[j][0];
                        BuferValue[1] = UsersListSortValueModel[j][1];
                        BuferValue[2] = UsersListSortValueModel[j][2];
                        UsersListSortValueModel[j][0] = UsersListSortValueModel[i][0];
                        UsersListSortValueModel[j][1] = UsersListSortValueModel[i][1];
                        UsersListSortValueModel[j][2] = UsersListSortValueModel[i][2];
                        UsersListSortValueModel[i][0] = BuferValue[0];
                        UsersListSortValueModel[i][1] = BuferValue[1];
                        UsersListSortValueModel[i][2] = BuferValue[2];
                    }
                }
            }
            return UsersListSortValueModel;
        }
    }
}

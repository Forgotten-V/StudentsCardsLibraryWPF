using StudentsCardsLibraryWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentsCardLibraryConsole.Model
{

    public class MainModel
    {



        public int GetFilterMethod()
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            int FilterMethod;
            FilterMethod = Config.FilterMethod;
            return FilterMethod;
        }

        public void SaveUserID(int UserID)
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.UserIDForView = UserID;
            UConfig.UpdateAppConfig(Config);
        }
        public void CreateUser (string Surname, string Name, string Lastname, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone, string FIO)
        {
            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
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
            });
            ULManager.AddUser(User);
        }

        public void SaveFilterMethod (int FilterMethod)
        {
            string UserAppConfigPath = Directory.GetCurrentDirectory();
            UserAppConfigPath = UserAppConfigPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            Config.FilterMethod = FilterMethod;
            UConfig.UpdateAppConfig(Config);
        }

        public int GetUserIDForView()
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            int UserIdForView;
            UserIdForView = Config.UserIDForView;
            return UserIdForView;
        }
        public int GetUsersNumbers ()
        {
            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            return User.Numbers();
        }

        public void StartDeleteUser ()
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
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

        public void EditUser(string Surname, string Name, string LastName, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)
        {
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
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

        public string [] GetUserInformation()
        {
            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            UserCards CurrentUser = User.ULibrary.FirstOrDefault(u => u.ID == GetUserIDForView());
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
        public string[][] GetUsersList (int FilterMethod)
        {
            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\StudentsCardLibraryConsole\\bin\\Debug\\net7.0", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            string[][] SortValue = new string[User.Numbers()][];
            {
                for (int i = 0; i < User.Numbers(); i++)
                {
                    SortValue[i] = new string[3];
                }
            }
            if (FilterMethod == 0)
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

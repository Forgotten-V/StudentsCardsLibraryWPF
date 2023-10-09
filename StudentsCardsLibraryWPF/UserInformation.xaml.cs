using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentsCardsLibraryWPF
{
    /// <summary>
    /// Логика взаимодействия для UserInformation.xaml
    /// </summary>
    public partial class UserInformation : Window
    {
        int UserID;
        int UserNumbers;
        string UserFIO;
        string UserSurname;
        public UserInformation()
        {
            InitializeComponent();
            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            UserNumbers = User.Numbers();


            UserID = Config.UserIDForView - 1;
            UserCards CurrentUser = User.ULibrary.FirstOrDefault(u => u.ID == UserID);
            UserFIO = CurrentUser.FIO;
            PresentUserTextBlock.Text = $"Карточка студента {UserFIO}";
            UserSurname = CurrentUser.Surname;
            SurnameInformation.Text = CurrentUser.Surname;
            NameInformation.Text = CurrentUser.Name;
            LastnameInformation.Text = CurrentUser.LastName;
            FacultyInformation.Text = CurrentUser.Faculty;
            SpecialityInformation.Text = CurrentUser.Speciality;
            GroupInformation.Text = CurrentUser.Group;
            CourseInformation.Text = CurrentUser.Course;
            CityInformation.Text = CurrentUser.City;
            EmailInformation.Text = CurrentUser.Email;
            PhoneInformation.Text = CurrentUser.Phone;
        }



        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            EditUser OpenEditUserWindow = new EditUser();
            OpenEditUserWindow.Show();
            this.Close();
        }
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            string DeleteCheck = DeleteUserButton.Content.ToString();
            if (UserSurname != SurnameInformation.Text)
            {
                MessageBox.Show("Для подтверждения удаления карточки пользователя введите его (её) фамилию");
            }
            else
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

                MessageBox.Show($"Карточка студента {UserFIO} успешно удалена");
                MainWindow OpenMainMenuWindow = new MainWindow();
                OpenMainMenuWindow.Show();
                this.Close();
            }



        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow OpenMainMenuWindow = new MainWindow();
            OpenMainMenuWindow.Show();
            this.Close();
        }

    }
}

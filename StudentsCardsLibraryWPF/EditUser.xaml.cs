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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {

        int UserID;
        int UserNumbers;
        string UserFIO;
        public EditUser()
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
            string[] UserInformation = new string[10];
            InputSurname.Text = CurrentUser.Surname;
            InputName.Text = CurrentUser.Name;
            InputLastname.Text = CurrentUser.LastName;
            InputFaculty.Text = CurrentUser.Faculty;
            InputSpeciality.Text = CurrentUser.Speciality;
            InputGroup.Text = CurrentUser.Group;
            InputCourse.Text = CurrentUser.Course;
            InputCity.Text = CurrentUser.City;
            InputEmail.Text = CurrentUser.Email;
            InputPhone.Text = CurrentUser.Phone;
    }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
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

            if (InputSurname.Text != "" && InputName.Text != "" && InputLastname.Text != "" && InputFaculty.Text != "" && InputSpeciality.Text != "" && InputGroup.Text != "" && InputCourse.Text != "" && InputCity.Text != "" && InputEmail.Text != "" && InputPhone.Text != "")
            {
                UserFIO = InputSurname.Text + " " + InputName.Text[0] + "." + InputLastname.Text[0] + "." + " " + InputGroup.Text;
                UserCards EditedUser = new UserCards
                {
                    ID = UserID,
                    FIO = UserFIO,
                    Surname = InputSurname.Text,
                    Name = InputName.Text,
                    LastName = InputLastname.Text,
                    Faculty = InputFaculty.Text,
                    Speciality = InputSpeciality.Text,
                    Group = InputGroup.Text,
                    Course = InputCourse.Text,
                    City = InputCity.Text,
                    Email = InputEmail.Text,
                    Phone = InputPhone.Text
                };
                ULManager.UpdateUser(EditedUser);
                User = ULManager.InitializateUserLibrary();
                MessageBox.Show($"Карточка студента {UserFIO} успешно отредактирована");

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

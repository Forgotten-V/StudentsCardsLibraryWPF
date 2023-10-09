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
    /// Логика взаимодействия для CreateUser.xaml
    /// </summary>
    /// 

    public partial class CreateUser : Window
    {

        public CreateUser()
        {
            InitializeComponent();
        }

private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {

            string UserLibraryPath = Directory.GetCurrentDirectory();
            UserLibraryPath = UserLibraryPath.Replace("\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserNumbers = User.Numbers();


            string FileName = "";
            string[] UserInformation = new string[10];
            bool ReadyToSave = false;
            for (int i = 0; i < 10; i++)
            {UserInformation[i] = ""; }
            UserInformation[0] = InputSurname.Text.Trim();
            UserInformation[1] = InputName.Text.Trim();
            UserInformation[2] = InputLastname.Text.Trim();
            UserInformation[3] = InputFaculty.Text.Trim();
            UserInformation[4] = InputSpeciality.Text.Trim();
            UserInformation[5] = InputGroup.Text.Trim();
            UserInformation[6] = InputCourse.Text.Trim();
            UserInformation[7] = InputCity.Text.Trim();
            UserInformation[8] = InputEmail.Text.Trim();
            UserInformation[9] = InputPhone.Text.Trim();

            for (int i = 0; i < 10; i++)
            {
                if (UserInformation[i] == "")
                {
                    ReadyToSave = false;
                    i = 10;
                }
                else
                {
                    ReadyToSave = true;
                }
            }

            if (ReadyToSave ==  false)
            {
                MessageBox.Show("Для создания карточки студента заполните все поля");
            }
            else if (ReadyToSave == true) 
            {
                FileName = UserInformation[0] + " " + UserInformation[1][0] + "." + UserInformation[2][0] + "." + " " + UserInformation[5];
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
                MessageBox.Show($"Карточка студента {FileName} успешно создана");
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

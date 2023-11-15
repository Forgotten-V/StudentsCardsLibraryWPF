using StudentsCardsLibraryWPF.Model;
using StudentsCardsLibraryWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentsCardsLibraryWPF.ViewModel
{
    public class EditUserLogicBlock
    {
        public string[] UsersInformation = new string[11];
        public string InputSurname { get; set; } = "";
        public string InputName { get; set; } = "";
        public string InputLastname { get; set; } = "";
        public string InputFaculty { get; set; } = "";
        public string InputSpeciality { get; set; } = "";
        public string InputGroup { get; set; } = "";
        public string InputCourse { get; set; } = "";
        public string InputCity { get; set; } = "";
        public string InputEmail { get; set; } = "";
        public string InputPhone { get; set; } = "";

        int UserID;
        public EditUserLogicBlock() 
        {
            MainModel Model = new MainModel();
            UserID = Model.GetUserIDForView();
            UsersInformation = Model.PresentUserInformation(UserID);
            InputSurname = UsersInformation[1];
            InputName = UsersInformation[2];
            InputLastname = UsersInformation[3];
            InputFaculty = UsersInformation[4];
            InputSpeciality = UsersInformation[5];
            InputGroup = UsersInformation[6];
            InputCourse = UsersInformation[7];
            InputCity = UsersInformation[8];
            InputEmail = UsersInformation[9];
            InputPhone = UsersInformation[10];
        }
        public ICommand OpenMainPage
        {
            get { return new NavigateRelayCommand(VOpenMainPage); }
        }

        private void VOpenMainPage()
        {
            var OpenStartPage = new StartPage();
            App.Current.MainWindow.Content = OpenStartPage;
        }

        public ICommand SaveChanges
        {
            get { return new NavigateRelayCommand(VSaveChanges); }
        }

        private void VSaveChanges()
        {
            if (InputSurname == "" || InputName == "" || InputLastname == "" || InputFaculty == "" || InputSpeciality == "" || InputGroup == "" || InputCourse == "" || InputCity == "" || InputEmail == "" || InputPhone == "")
            {
                MessageBox.Show("Для завершения редактирования необходимо заполнить все поля.");
            }
            else
            {
                MainModel Model = new MainModel();
                Model.EditUser(InputSurname, InputName, InputLastname, InputFaculty, InputSpeciality, InputGroup, InputCourse, InputCity, InputEmail, InputPhone);
                var OpenStartPage = new StartPage();
                App.Current.MainWindow.Content = OpenStartPage;
            }
        }
    }
}

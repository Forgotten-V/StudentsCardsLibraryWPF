using StudentsCardsLibraryWPF.Model;
using StudentsCardsLibraryWPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace StudentsCardsLibraryWPF.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {

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


        public ICommand ShutDownApp
        {
            get { return new NavigateRelayCommand(VShutDownApp); }
        }

        private void VShutDownApp()
        {
            Application.Current.Shutdown();
        }

        public ICommand OpenCreateUserPage
        {
            get { return new NavigateRelayCommand(VOpenCreateUserPage); }
        }

        private void VOpenCreateUserPage()
        {
            var OpenCreateUser = new FrameCreateUser();
            App.Current.MainWindow.Content = OpenCreateUser;
        }

        public ICommand OpenPickFilterMethodPage
        {
            get { return new NavigateRelayCommand(VOpenPickFilterMethodPage); }
        }

        private void VOpenPickFilterMethodPage()
        {
            var OpenPickFilterMethod = new FramePickFilterMethod();
            App.Current.MainWindow.Content = OpenPickFilterMethod;
        }

        public ICommand OpenUsersListPage
        {
            get { return new NavigateRelayCommand(VOpenUsersListPage); }
        }

        private void VOpenUsersListPage()
        {
            var OpenUsersListPage = new FrameUsersList();
            App.Current.MainWindow.Content = OpenUsersListPage;
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

        public ICommand StartCreateUser
        {
            get { return new NavigateRelayCommand(VStartCreateUser); }
        }

        private void VStartCreateUser()
        {
            if (InputSurname == "" || InputName == "" || InputLastname == "" || InputFaculty == "" || InputSpeciality == "" || InputGroup == "" || InputCourse == "" || InputCity == "" || InputEmail == "" || InputPhone == "")
            {
                MessageBox.Show("Заполните все поля, для создания карточки студента."); 
            }
            else
            {
                MainModel NewUser = new MainModel();
                NewUser.CreateNewUser(InputSurname, InputName, InputLastname, InputFaculty, InputSpeciality, InputGroup, InputCourse, InputCity, InputEmail, InputPhone);
                var OpenStartPage = new StartPage();
                App.Current.MainWindow.Content = OpenStartPage;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

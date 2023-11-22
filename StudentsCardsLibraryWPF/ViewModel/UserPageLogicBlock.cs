using StudentsCardsLibraryWPF.Model;
using StudentsCardsLibraryWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace StudentsCardsLibraryWPF.ViewModel
{
    public class UserPageLogicBlock : INotifyPropertyChanged
    {
        public string[] UsersInformation = new string [11];
        public string OutputSurname { get; set; } = "";
        public string OutputName { get; set; } = "";
        public string OutputLastname { get; set; } = "";
        public string OutputFaculty { get; set; } = "";
        public string OutputSpeciality { get; set; } = "";
        public string OutputGroup { get; set; } = "";
        public string OutputCourse { get; set; } = "";
        public string OutputCity { get; set; } = "";
        public string OutputEmail { get; set; } = "";
        public string OutputPhone { get; set; } = "";

        public string TitleInformation { get; set; } = "Карточка студента ";

        int UserID;

        public string TargetToDelete { get; set; } = "Введите фамилию пользователя для подтверждения его удаления";

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

        public ICommand StartEditUser
        {
            get { return new NavigateRelayCommand(VStartEditUser); }
        }

        private void VStartEditUser()
        {
            var OpenEditUserPage = new FrameEditUser();
            App.Current.MainWindow.Content = OpenEditUserPage;
        }

        //public void UserPagePreLoaded()
        //{
        //    MainModel Model = new MainModel();
        //    UserID = Model.GetUserIDForView();
        //    UsersInformation = Model.PresentUserInformation(UserID);
        //    TitleInformation = TitleInformation + UsersInformation[0];
        //    OutputSurname = UsersInformation[1];
        //    OutputName = UsersInformation[2];
        //    OutputLastname = UsersInformation[3];
        //    OutputFaculty = UsersInformation[4];
        //    OutputSpeciality = UsersInformation[5];
        //    OutputGroup = UsersInformation[6];
        //    OutputCourse = UsersInformation[7];
        //    OutputCity = UsersInformation[8];
        //    OutputEmail = UsersInformation[9];
        //    OutputPhone = UsersInformation[10];
        //    MessageBox.Show($"{OutputSurname}");
        //}

        public UserPageLogicBlock()
        {
            MainModel Model = new MainModel();
            UserID = Model.GetUserIDForView();
            UsersInformation = Model.PresentUserInformation(UserID);
            TitleInformation = TitleInformation + UsersInformation[0];
            OutputSurname = UsersInformation[1];
            OutputName = UsersInformation[2];
            OutputLastname = UsersInformation[3];
            OutputFaculty = UsersInformation[4];
            OutputSpeciality = UsersInformation[5];
            OutputGroup = UsersInformation[6];
            OutputCourse = UsersInformation[7];
            OutputCity = UsersInformation[8];
            OutputEmail = UsersInformation[9];
            OutputPhone = UsersInformation[10];
        }

        public ICommand TryDeleteUser
        {
            get { return new NavigateRelayCommand(VTryDeleteUser); }
        }

        private void VTryDeleteUser()
        {
            if (UsersInformation[1] == TargetToDelete)
            {
                MainModel Model = new MainModel();
                Model.DeleteUser();
                MessageBox.Show($"Карточка студента {UsersInformation[0]} успешно удалена");
                var OpenStartPage = new StartPage();
                App.Current.MainWindow.Content = OpenStartPage;
            }
            else
            {
                MessageBox.Show("Для удаления пользователя введите его фамилию");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

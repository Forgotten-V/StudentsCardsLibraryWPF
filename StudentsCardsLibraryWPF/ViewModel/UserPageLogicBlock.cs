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
using ModelClassLibrary;

namespace StudentsCardsLibraryWPF.ViewModel
{
    public class UserPageLogicBlock : INotifyPropertyChanged        //Класс, отвечающий по большей части за предзагрузку информации на странице пользователя. 
    {
        public string[] UsersInformation = new string [11];         //Массив, который принимает всю информацию о пользователе.
        public string OutputSurname { get; set; } = "";             //Переменные, которым присваиваются значения данных пользователей и через привязку они выводятся для отображения.
        public string OutputName { get; set; } = "";
        public string OutputLastname { get; set; } = "";
        public string OutputFaculty { get; set; } = "";
        public string OutputSpeciality { get; set; } = "";
        public string OutputGroup { get; set; } = "";
        public string OutputCourse { get; set; } = "";
        public string OutputCity { get; set; } = "";
        public string OutputEmail { get; set; } = "";
        public string OutputPhone { get; set; } = "";

        public string TitleInformation { get; set; } = "Профиль пользователя ";    //Переменная, хранящая краткую информацию об открытом профиле.

        public string TargetToDelete { get; set; } = "Введите фамилию пользователя для подтверждения его удаления";     //Переменная, принимающая в себя фамилию текущего студента для его последующего удаления.
                                                                                                                        //Сделана в целях усложнения процесса удоления, чтобы предотвратить непреднамеренное удаление.

        public ICommand OpenUsersListPage       //Команда и еёфункция, позволяющая вернуться обратно к списку поьзователей.
        {
            get { return new NavigateRelayCommand(VOpenUsersListPage); }
        }

        private void VOpenUsersListPage()
        {
            var OpenUsersListPage = new FrameUsersList();
            App.Current.MainWindow.Content = OpenUsersListPage;
        }

        public ICommand OpenMainPage        //Команда и её функция, открывающая главное окно программы.
        {
            get { return new NavigateRelayCommand(VOpenMainPage); }
        }

        private void VOpenMainPage()
        {
            var OpenStartPage = new StartPage();
            App.Current.MainWindow.Content = OpenStartPage;
        }

        public ICommand StartEditUser       //Команда и её функция, позволяющая начать редактировать информацию о текущем пользователе.
        {
            get { return new NavigateRelayCommand(VStartEditUser); }
        }

        private void VStartEditUser()
        {
            var OpenEditUserPage = new FrameEditUser();
            App.Current.MainWindow.Content = OpenEditUserPage;
        }

        public UserPageLogicBlock()             //При открытии страницы пользователя происходит инициализация класса, который сначала получает информацию
                                                //о выбранном пользователе, а затем загружает её в переменные, отображающиеся на странице пользователя.
        {
            MainModel Model = new MainModel();      
            //UserID = Model.GetUserIDForView();    //Старый вариант получения ID пользователя json-файла
            UsersInformation = Model.PresentUserInformation(GlobalVariables.UserID);
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

        public ICommand TryDeleteUser           //Команда и её функция, позволяющая удалить текущего пользователя.
        {
            get { return new NavigateRelayCommand(VTryDeleteUser); }
        }

        private void VTryDeleteUser()
        {
            if (UsersInformation[1] == TargetToDelete)      //Условный оператор, сравнивающий введённую информацию с фамилией пользователя. В случае успеха запускает функцию удаления пользователя.
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

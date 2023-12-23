using ModelClassLibrary;
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
    public class MainVM : INotifyPropertyChanged        //Класс, представляющий из себя главный ViewModel программы. Однако вскоре он
                                                        //стал отвечать только за стартовую страницу и страницу создания пользователей,
                                                        //ввиду отсутствия необходимости делать внутри них предзагрузку..
    {
        public string InputSurname { get; set; } = "";      //Переменные, которые принимают данные для создания новой страницы пользователя.
        public string InputName { get; set; } = "";
        public string InputLastname { get; set; } = "";
        public string InputFaculty { get; set; } = "";
        public string InputSpeciality { get; set; } = "";
        public string InputGroup { get; set; } = "";
        public string InputCourse { get; set; } = "";
        public string InputCity { get; set; } = "";
        public string InputEmail { get; set; } = "";
        public string InputPhone { get; set; } = "";


        public ICommand ShutDownApp         //Команда и её функция, полностью выключающая приложение.
        {
            get { return new NavigateRelayCommand(VShutDownApp); }
        }

        private void VShutDownApp()
        {
            Application.Current.Shutdown();
        }

        public ICommand OpenCreateUserPage      //Команда и её функция, открывающая страницу создания пользователя.
        {
            get { return new NavigateRelayCommand(VOpenCreateUserPage); }
        }

        private void VOpenCreateUserPage()
        {
            var OpenCreateUser = new FrameCreateUser();
            App.Current.MainWindow.Content = OpenCreateUser;
        }

        public ICommand HotOpenUsersPage        //Команда и её функция, открывающая страницу последнего выбранного пользователя.
        {
            get { return new NavigateRelayCommand(VHotOpenUsersPage); }
        }

        private void VHotOpenUsersPage()        //В случае, если в текущей сесси ещё не было открыто профиля
                                                //пользователя, или он был удалён, выведет об этом
                                                //оповещение и предотвратит появление ошибки.
        {
            if (GlobalVariables.UserID == 0)
            {
                MessageBox.Show("В этой сесси ещё не открывался пользовательский профиль");
            }
            else if (GlobalVariables.UserID == -1)
            {
                MessageBox.Show("Этот профиль был удалён");
            }
            else
            {
                var OpenUserPage = new FrameUserPage();
                App.Current.MainWindow.Content = OpenUserPage;
            }
        }

        public ICommand OpenUsersListPage       //Команда и её функция, открывающая список пользователей.
        {
            get { return new NavigateRelayCommand(VOpenUsersListPage); }
        }

        private void VOpenUsersListPage()
        {
            MainModel Model = new MainModel();
            if (Model.GetUsersNumbers() == 0)                   //В случае, если в базе данных ещё не было создано ни одного пользователя, программа
                                                                //ничего не откроет и выведет оповещение о пустой базе пользователей.
            {
                MessageBox.Show("В базе данных ещё не было создано пользователей");
            }
            else
            {
                var OpenUsersListPage = new FrameUsersList();
                App.Current.MainWindow.Content = OpenUsersListPage;
            }
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

        public ICommand OpenMainPage            //Команда и её функция, открывающая главное окно программы.
        {
            get { return new NavigateRelayCommand(VOpenMainPage); }
        }

        private void VOpenMainPage()
        {
            var OpenStartPage = new StartPage();
            App.Current.MainWindow.Content = OpenStartPage;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

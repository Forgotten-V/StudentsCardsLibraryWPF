using ModelClassLibrary;
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
    public class EditUserLogicBlock     //Класс, отвечающий за логику взаимодейстивия с окном EditUser, и в целом на логику редактирования пользователя.
    {
        public string[] UsersInformation = new string[11];      //Массив, собирающий в себя информацию о пользователе для её последующего вывода.
        public string InputSurname { get; set; } = "";          //Переменные, которм привязаны TextBox из XAML файла.
        public string InputName { get; set; } = "";
        public string InputLastname { get; set; } = "";
        public string InputFaculty { get; set; } = "";
        public string InputSpeciality { get; set; } = "";
        public string InputGroup { get; set; } = "";
        public string InputCourse { get; set; } = "";
        public string InputCity { get; set; } = "";
        public string InputEmail { get; set; } = "";
        public string InputPhone { get; set; } = "";
        //public int UserID {get; set;} = 0;       В теории эту переменную можно так же использовать для вывода информации о ID пользователя.

        public EditUserLogicBlock()         //Стартовое действие класса при его инициализации - присвоение значений
                                            //переменным, которые будут выведены в графическом интерфейсе для просмотра.
        {
            MainModel Model = new MainModel();
            //UserID = GlobalVariables.UserID;
            UsersInformation = Model.PresentUserInformation(GlobalVariables.UserID);
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

        public ICommand OpenMainPage        //Команда и её функция, открывающая главное окно программы.
        {
            get { return new NavigateRelayCommand(VOpenMainPage); }
        }
        private void VOpenMainPage()
        {
            var OpenStartPage = new StartPage();
            App.Current.MainWindow.Content = OpenStartPage;
        }

        public ICommand OpenUsersListPage       //Команда и её функция, открывающая список всех пользователей.
        {
            get { return new NavigateRelayCommand(VOpenUsersListPage); }
        }

        private void VOpenUsersListPage()
        {
            if (GlobalVariables.WindowMode == 0)            //Открывает список пользователей, вид которого зависит
                                                            //от последнего выбранного способа его отображения.
            {
                GlobalVariables.FilterMethod = 0;
                var OpenUsersListPage = new FrameAlternativeUsersList();
                App.Current.MainWindow.Content = OpenUsersListPage;
            }
            else if (GlobalVariables.WindowMode == 1)
            {
                var OpenUsersListPage = new FrameUsersList();
                App.Current.MainWindow.Content = OpenUsersListPage;
            }
        }

        public ICommand OpenUserPage            //Команда и её функция, возвращающая на страницу редактируемого пользователя.
        {
            get { return new NavigateRelayCommand(VOpenUserPage); }
        }

        private void VOpenUserPage()
        {
            var OpenUserPage = new FrameUserPage();
            App.Current.MainWindow.Content = OpenUserPage;
        }

        public ICommand SaveChanges                 //Команда и её функция, сохраняющая изменения информации о пользователе
        {
            get { return new NavigateRelayCommand(VSaveChanges); }
        }

        private void VSaveChanges()
        {
            if (InputSurname == "" || InputName == "" || InputLastname == "" || InputFaculty == "" || InputSpeciality == "" || InputGroup == "" || InputCourse == "" || InputCity == "" || InputEmail == "" || InputPhone == "")        //Условный оператор, проверяющий отсутствие пустых текстовых блоков и выводящий предупреждение в случае их наличия.
            {
                MessageBox.Show("Для завершения редактирования необходимо заполнить все поля.");
            }
            else
            {
                MainModel Model = new MainModel();
                Model.EditUser(InputSurname, InputName, InputLastname, InputFaculty, InputSpeciality, InputGroup, InputCourse, InputCity, InputEmail, InputPhone);      //В случае отсутствия пустых текстовых блоков вызывает метод для редактирования пользователя и загружает в него все необходимые данные.
                MessageBox.Show($"Информация о пользователе {InputSurname} {InputName[0]}. {InputLastname[0]}. успешно обновлена.");
                if (GlobalVariables.WindowMode == 0)            //По завершению редактирования открывает список пользователей, вид
                                                                //которого зависит от последнего выбранного способа его отображения.
                {
                    GlobalVariables.FilterMethod = 0;           //Скорее всего, метод фильтрации уже был установлен как 0, но лишний раз сделать это не
                                                                //помешает - по умолчанию метод сортировки в таблице только по фамилии
                    var OpenUsersListPage = new FrameAlternativeUsersList();
                    App.Current.MainWindow.Content = OpenUsersListPage;
                }
                else if (GlobalVariables.WindowMode == 1)
                {
                    var OpenUsersListPage = new FrameUsersList();
                    App.Current.MainWindow.Content = OpenUsersListPage;
                }
            }
        }
    }
}

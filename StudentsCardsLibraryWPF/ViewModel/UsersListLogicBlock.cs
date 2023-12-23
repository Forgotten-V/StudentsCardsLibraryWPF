using ModelClassLibrary;
using StudentsCardsLibraryWPF.Model;
using StudentsCardsLibraryWPF.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace StudentsCardsLibraryWPF.ViewModel
{
    public class UsersListLogicBlock : INotifyPropertyChanged       //Класс, отвечающий сразу за отображения списка пользователей сразу на двух страницах.
                                                                    //Также имеет уникальный класс-команду, позволяющий это реализовывать..
    {

        public ICommand OpenMainPage            //Команда и её функция, открывающая главное окно программы.
        {
            get { return new NavigateRelayCommand(VOpenMainPage); }
        }

        private void VOpenMainPage()
        {
            var OpenStartPage = new StartPage();
            App.Current.MainWindow.Content = OpenStartPage;
        }

        public string FilterMethodInformation { get; set; } = "";

        public string[][] UsersListSortValue;

        UserListCollection? selectedUser;
        public ObservableCollection<UserListCollection> Users { get; set; }



        UsersListCommand? openUserCard;
        public UsersListCommand OpenUserCard
        {
            get
            {
                return openUserCard ??
                    (openUserCard = new UsersListCommand(obj =>
                    {
                        UserListCollection? User = obj as UserListCollection;
                        if (User != null)
                        {
                            MainModel Model = new MainModel();
                            UsersListSortValue = Model.CreateUsersBase();
                            UserListCollection UserInput = new UserListCollection(User.FIO, null, null, null, null, null, null, null, null, null, null);
                            string InputValue = UserInput.FIO;
                            //MessageBox.Show($"{UserInput.FIO}");
                            for (int i = 0; UsersListSortValue.Length > i; i++)
                            {
                                if (InputValue == UsersListSortValue[i][0])
                                {
                                    GlobalVariables.UserID = Int32.Parse(UsersListSortValue[i][2]) - 1; //Сохранение ID выбранного пользователя
                                    //Model.SaveUserID(Int32.Parse(UsersListSortValue[i][2]) - 1);      //Старый вариант сохранения ID пользователя в json файл
                                    i = UsersListSortValue.Length;//Остановка цикла
                                    var OpenUserPage = new FrameUserPage();
                                    App.Current.MainWindow.Content = OpenUserPage;
                                }
                            }
                        }
                    }));
            }
        }


        public UserListCollection? SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public UsersListLogicBlock()       //При открытии окна со списком пользователей происходит инициализация класса, где в
                                           //зависимости от выбранного метода фильтрации подготавливаются необходимые данные
                                           //для отображения, а также присваиваются значения переменным с информацией о пользователях.
        {
            MainModel Model = new MainModel();
            UsersListSortValue = Model.CreateUsersBase();
            int ListPosition = 1;
            Users = new ObservableCollection<UserListCollection>
            {
                new UserListCollection(UsersListSortValue[0][0], UsersListSortValue[0][3], UsersListSortValue[0][4], UsersListSortValue[0][5], UsersListSortValue[0][6], UsersListSortValue[0][7], UsersListSortValue[0][8], UsersListSortValue[0][9], UsersListSortValue[0][10], UsersListSortValue[0][11], UsersListSortValue[0][12]),
            };
            for (int i = 1; i < UsersListSortValue.Length; i++)
            {
                UserListCollection user = new UserListCollection("", null, null, null, null, null, null, null, null, null, null);
                user = new UserListCollection(UsersListSortValue[i][0], UsersListSortValue[i][3], UsersListSortValue[i][4], UsersListSortValue[i][5], UsersListSortValue[i][6], UsersListSortValue[i][7], UsersListSortValue[i][8], UsersListSortValue[i][9], UsersListSortValue[i][10], UsersListSortValue[i][11], UsersListSortValue[i][12]);
                Users.Insert(ListPosition, user);
                ListPosition++;
            }
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class UsersListCommand : ICommand        //Класс-команда, особенностью которого является возможность передавать
                                                    //ФИО выделенного пользователя и передавать её в качестве параметра.
    {
        Action<object?> execute;
        Func<object?, bool>? canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public UsersListCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }



    public class UserListCollection : INotifyPropertyChanged        //Коллекция, из которой берутся данные для отображения пользователей.
    {
        private string fio;
        private string surname;
        private string name;
        private string lastname;
        private string faculty;
        private string speciality;
        private string group;
        private string course;
        private string city;
        private string email;
        private string phone;

        public UserListCollection(string fio, string surname, string name, string lastname, string faculty, string speciality, string group, string course, string city, string email, string phone)
        {
            this.fio = fio;
            this.surname = surname;
            this.name = name;
            this.lastname = lastname;
            this.faculty = faculty;
            this.speciality = speciality;
            this.group = group;
            this.course = course;
            this.city = city;
            this.email = email;
            this.phone = phone;
        }

        public string FIO
        {
            get { return fio; }
            set
            {
                fio = value;
                OnPropertyChanged("Title");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Title");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Title");
            }
        }
        public string Lastname
        {
            get { return lastname; }
            set
            {
                fio = value;
                OnPropertyChanged("Title");
            }
        }
        public string Faculty
        {
            get { return faculty; }
            set
            {
                faculty = value;
                OnPropertyChanged("Title");
            }
        }
        public string Speciality
        {
            get { return speciality; }
            set
            {
                speciality = value;
                OnPropertyChanged("Title");
            }
        }
        public string Group
        {
            get { return group; }
            set
            {
                group = value;
                OnPropertyChanged("Title");
            }
        }
        public string Course
        {
            get { return course; }
            set
            {
                course = value;
                OnPropertyChanged("Title");
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("Title");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Title");
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Title");
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

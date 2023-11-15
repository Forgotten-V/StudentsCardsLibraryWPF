﻿using StudentsCardsLibraryWPF.Model;
using StudentsCardsLibraryWPF.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace StudentsCardsLibraryWPF.ViewModel
{
    public class UsersListLogicBlock : INotifyPropertyChanged
    {

        public ICommand OpenMainPage
        {
            get { return new NavigateRelayCommand(VOpenMainPage); }
        }

        private void VOpenMainPage()
        {
            var OpenStartPage = new StartPage();
            App.Current.MainWindow.Content = OpenStartPage;
        }


        public string[][] UsersListSortValue;

        UserListCollection? selectedUser;
        public ObservableCollection<UserListCollection> Users { get; set; }


        RelayCommand? openUserCard;
        public RelayCommand OpenUserCard
        {
            get
            {
                return openUserCard ??
                    (openUserCard = new RelayCommand(obj =>
                    {
                        UserListCollection? User = obj as UserListCollection;
                        if (User != null)
                        {
                            MainModel Model = new MainModel();
                            UsersListSortValue = Model.CreateUsersBase();
                            UserListCollection UserInput = new UserListCollection(User.Title);
                            string InputValue = UserInput.Title;
                            for (int i = 0; UsersListSortValue.Length > i; i++)
                            {
                                if (InputValue == UsersListSortValue[i][0])
                                {
                                    Model.SaveUserID(Int32.Parse(UsersListSortValue[i][2])-1);
                                    i = UsersListSortValue.Length;
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

        public UsersListLogicBlock()
        {

            MainModel Model = new MainModel();
            UsersListSortValue = Model.CreateUsersBase();
            int FilterMethod = Model.GetFilterMethod();
            int ListPosition = 1;
            if (UsersListSortValue.Length == 0)
            {
                MessageBox.Show("В базе данных ещё не было создано пользователей");
            }
            else
            {
                if (FilterMethod == 0)
                {
                    Users = new ObservableCollection<UserListCollection>
                    {
                        new UserListCollection("СОРТИРОВКА ПОЛЬЗОВАТЕЛЕЙ ПО ФАМИЛИИ"),
                    };
                    for (int i = 0;  i < UsersListSortValue.Length; i++)
                    {
                        UserListCollection phone = new UserListCollection("");
                        phone = new UserListCollection(UsersListSortValue[i][0]);
                        Users.Insert(ListPosition, phone);
                        ListPosition++;
                    }
                }
                else
                {
                    string PresentText = "";//Переменная, выступающая шаблоном выводимого текста для разделения критериев сортировки.
                    string PrsentFilterMethod = "";
                    string BenchmarkValue = "";//Переменная, использующая в качестве эталона сортируемого параметра.
                    if (FilterMethod == 1)
                    {
                        PresentText = "СТУДЕНТЫ ФАКУЛЬТЕТА: ";
                        PrsentFilterMethod = "СОРТИРОВКА ПОЛЬЗОВАТЕЛЕЙ ПО ФАКУЛЬТЕТАМ";
                    }
                    else if (FilterMethod == 2)
                    {
                        PresentText = "СТУДЕНТЫ СПЕЦИАЛЬНОСТИ: ";
                        PrsentFilterMethod = "СОРТИРОВКА ПОЛЬЗОВАТЕЛЕЙ ПО СПЕЦИАЛЬНОСТИ";
                    }
                    else if (FilterMethod == 3)
                    {
                        PresentText = "СТУДЕНТЫ УЧЕБНОЙ ГРУППЫ ГРУППЫ: ";
                        PrsentFilterMethod = "СОРТИРОВКА ПОЛЬЗОВАТЕЛЕЙ ПО УЧЕБНЫМ ГРУППАМ";
                    }
                    else if (FilterMethod == 4)
                    {
                        PresentText = "СТУДЕНТЫ КУРСА: ";
                        PrsentFilterMethod = "СОРТИРОВКА ПОЛЬЗОВАТЕЛЕЙ ПО ДЛИТЕЛЬНОСТИ ОБУЧЕНИЯ";
                    }
                    Users = new ObservableCollection<UserListCollection>
                    {
                        new UserListCollection($"{PrsentFilterMethod}"),
                    };
                    for (int i = 0; i < UsersListSortValue.Length; i++)
                    {
                        UserListCollection phone = new UserListCollection("");
                        if (BenchmarkValue != UsersListSortValue[i][1])
                        {
                            BenchmarkValue = UsersListSortValue[i][1];
                            phone = new UserListCollection($"{PresentText}{BenchmarkValue}");
                            Users.Insert(ListPosition, phone);
                            ListPosition++;
                        }
                        phone = new UserListCollection(UsersListSortValue[i][0]);
                        Users.Insert(ListPosition, phone);
                        ListPosition++;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class RelayCommand : ICommand
    {
        Action<object?> execute;
        Func<object?, bool>? canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
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
    public class UserListCollection : INotifyPropertyChanged
    {
        private string title;

        public UserListCollection(string title)
        {
            this.title = title;
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
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
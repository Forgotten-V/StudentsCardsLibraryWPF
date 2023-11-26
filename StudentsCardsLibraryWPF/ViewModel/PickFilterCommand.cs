using ModelClassLibrary;
using StudentsCardsLibraryWPF.Model;
using StudentsCardsLibraryWPF.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentsCardsLibraryWPF.ViewModel
{
    public class PickFilterVM : INotifyPropertyChanged      //Класс, отвечающий за функциональность страницы PickFilterMethod. Ввиду необходимости для его
                                                            //функционирования специализированного класса-комманды был вынесен вместе с ним в отдельный класс.
    {
        private string textField;
        public string FilterMetodString         //У каждой кнопки метода фильтрации есть свой параметр, обозначающий способ фильтрации.
                                                //При нажатии на неё значение этого параметра присваивается этой переменной
        {
            get { return textField; }
            set
            {
                if (textField != value)
                {
                    textField = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SetText(string text)
        {
            FilterMetodString = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand OpenUsersListPage { get; }
        public PickFilterVM()
        {
            OpenUsersListPage = new PickFilterCommand(SetText);
        }

        private void SetText(object parameter)      //Функция команды рассчитана на обязательное получение параметра. Поэтому
                                                    //даже у кнопке, отвечающей за закрытие окна, был прописан свой параметр.
        {
            string text = parameter as string;
            if (!string.IsNullOrEmpty(text))
            {
                FilterMetodString = text;
                int FilterMethod = Int32.Parse(FilterMetodString);
                if (FilterMethod == 5)
                {
                    var OpenStartPage = new StartPage();
                    App.Current.MainWindow.Content = OpenStartPage;
                }
                else 
                {
                    //MainModel PickFilter = new MainModel();
                    //PickFilter.EditFilterMethod(FilterMethod);
                    GlobalVariables.FilterMethod = FilterMethod;
                    var OpenUsersListPage = new FrameUsersList();
                    App.Current.MainWindow.Content = OpenUsersListPage;
                }
            }
        }
    }

    public class PickFilterCommand : ICommand           //Та самая специализированная класс-команда. Очень ограничена в своём применении, поэтому
                                                        //в текущем приложении подходит только для выбора метода фильтрации.
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public PickFilterCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}

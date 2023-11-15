using System;
using System.CodeDom.Compiler;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentsCardsLibraryWPF
{
    /// <summary>
    /// Логика взаимодействия для ViewUser.xaml
    /// </summary>
    public partial class ViewUser : Window
    {
        string[][] SortValue;
        public ViewUser()
        {
            
            InitializeComponent();

            string PathFiles = Directory.GetCurrentDirectory();
            string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
            UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
            UserAppConfig Config = UConfig.InitializateUserAppConfig();
            string UserLibraryPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
            UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
            UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
            UserLibrary User = ULManager.InitializateUserLibrary();
            int UserNumbers = User.Numbers();
            string[] BuferValue = new string[3];
            SortValue = new string[UserNumbers][];
            {
                for (int i = 0; i < UserNumbers; i++)
                {
                    SortValue[i] = new string[3];
                }
            }
            if (Config.FilterMethod == 1)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Surname;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 2)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Faculty;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 3)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Speciality;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 4)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Group;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }
            else if (Config.FilterMethod == 5)
            {
                for (int i = 1; i < UserNumbers + 1; i++)
                {
                    UserCards TempValue = User.ULibrary.FirstOrDefault(u => u.ID == i);
                    SortValue[i - 1][0] = TempValue.FIO;
                    SortValue[i - 1][1] = TempValue.Course;
                    SortValue[i - 1][2] = $"{i + 1}";
                }
            }

            for (int i = 0; i < UserNumbers; i++)//Цикл "пузырьковой" сортировки карточек студентов в зависимости от необходимого метода сортировки.
            {
                for (int j = i + 1; j < UserNumbers; j++)
                {
                    if (string.Compare(SortValue[i][1], SortValue[j][1]) > 0)//Условный оператор, внутри которого проводится сравнение по алфавиту отдельных параметров сокращённого массива. В случае получения значения false запускает цикл.
                    {
                        BuferValue[0] = SortValue[j][0];
                        BuferValue[1] = SortValue[j][1];
                        BuferValue[2] = SortValue[j][2];
                        SortValue[j][0] = SortValue[i][0];
                        SortValue[j][1] = SortValue[i][1];
                        SortValue[j][2] = SortValue[i][2];
                        SortValue[i][0] = BuferValue[0];
                        SortValue[i][1] = BuferValue[1];
                        SortValue[i][2] = BuferValue[2];
                    }
                }
            }

            if (Config.FilterMethod == 1)
            {
                if (UserNumbers == 0)
                {
                    MainTextBlock.Text = "В базе данных ещё не созданы карточки студентов";
                }
                else
                {
                    TextBlock TextInformation = new TextBlock();
                    TextInformation = new TextBlock();
                    TextInformation.FontSize = 18;
                    TextInformation.TextAlignment = TextAlignment.Center;
                    TextInformation.Text = "Выбрана сортировка по фамилиям";
                    UserList.Children.Add(TextInformation);
                    for (int i = 1; i < UserNumbers + 1; i++)
                    {
                        RadioButton UserUnderRB = new RadioButton { FontSize = 14, IsChecked = false, GroupName = "Users", Content = $"{SortValue[i - 1][0]}" };
                        UserList.Children.Add(UserUnderRB);
                        UserUnderRB.Checked += RadioButton_Checked;
                    }
                }
            }
            else
            {
                if (UserNumbers == 0)
                {
                    MainTextBlock.Text = "В базе данных ещё не созданы карточки студентов";
                }
                else
                {
                    string PresentText = "";//Переменная, выступающая шаблоном выводимого текста для разделения критериев сортировки.
                    string PrsentFilterMethod = "";
                    if (Config.FilterMethod == 2)
                    {
                        PresentText = "\nСтуденты факультета ";
                        PrsentFilterMethod = "Выбрана сортировка по факультетам";
                    }
                    else if (Config.FilterMethod == 3)
                    {
                        PresentText = "\nСтуденты специальности ";
                        PrsentFilterMethod = "Выбрана сортировка по специальности";
                    }
                    else if (Config.FilterMethod == 4)
                    {
                        PresentText = "\nСтуденты группы ";
                        PrsentFilterMethod = "Выбрана сортировка по учебным группам";
                    }
                    else if (Config.FilterMethod == 5)
                    {
                        PresentText = "\nСтуденты курса ";
                        PrsentFilterMethod = "Выбрана сортировка по длительности обучения";
                    }
                    string BenchmarkValue = "";//Переменная, использующая в качестве эталона сортируемый параметр.
                    TextBlock TextInformation = new TextBlock();
                    TextInformation = new TextBlock();
                    TextInformation.FontSize = 18;
                    TextInformation.TextAlignment = TextAlignment.Center;
                    TextInformation.Text = PrsentFilterMethod;
                    UserList.Children.Add(TextInformation);
                    for (int i = 1; i < UserNumbers + 1; i++)
                    {
                        if (BenchmarkValue != SortValue[i - 1][1])
                        {
                            BenchmarkValue = SortValue[i - 1][1];
                            TextInformation = new TextBlock();
                            TextInformation = new TextBlock();
                            TextInformation.FontSize = 18;
                            TextInformation.TextAlignment = TextAlignment.Center;
                            TextInformation.Text = $"{PresentText} {BenchmarkValue}";
                            UserList.Children.Add(TextInformation);
                        }
                        RadioButton UserUnderRB = new RadioButton { FontSize = 14, IsChecked = false, GroupName = "Users", Content = $"{SortValue[i - 1][0]}" };
                        UserList.Children.Add(UserUnderRB);
                        UserUnderRB.Checked += RadioButton_Checked;
                    }//Цикл, выводящий результат сортировки карточек студентов с отображением полученных в ходе сортировки критериев.
                }
            }
        }
        bool StudentChoise = false;
        string StudentFIO = "";
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            StudentChoise = true;
            RadioButton pressed = (RadioButton)sender;
            StudentFIO = (string)pressed.Content;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow OpenMainMenuWindow = new MainWindow();
            OpenMainMenuWindow.Show();
            this.Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentChoise == false) 
            {
                MessageBox.Show("Выберете одного из студентов");
            }
            else if (StudentChoise == true) 
            {
                string PathFiles = Directory.GetCurrentDirectory();
                string UserAppConfigPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
                UserAppConfigPath = UserAppConfigPath + "\\UserAppConfig.json";
                UserAppConfigManager UConfig = new UserAppConfigManager(UserAppConfigPath);
                UserAppConfig Config = UConfig.InitializateUserAppConfig();
                string UserLibraryPath = PathFiles.Replace("\\StudentsCardsLibraryWPF\\bin\\Debug\\net7.0-windows", "");
                UserLibraryPath = UserLibraryPath + "\\UserLibrary.json";
                UserLibraryManager ULManager = new UserLibraryManager(UserLibraryPath);
                UserLibrary User = ULManager.InitializateUserLibrary();
                int UserNumbers = User.Numbers();
                int TrueUserID = 0;
                for (int i = 0;  i < UserNumbers; i++) 
                {
                    if  (StudentFIO == SortValue[i][0])
                    {
                        TrueUserID = int.Parse(SortValue[i][2]);
                        Config.UserIDForView = TrueUserID;
                        UConfig.UpdateAppConfig(Config);
                        i = UserNumbers;
                        UserInformation OpenUserInformationWindow = new UserInformation();
                        OpenUserInformationWindow.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}

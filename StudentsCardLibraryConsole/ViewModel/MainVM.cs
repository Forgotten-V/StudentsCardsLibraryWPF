using StudentsCardLibraryConsole.Model;
using StudentsCardsLibraryWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCardLibraryConsole.ViewModel
{
    public class MainVM
    {
        MainModel Model = new MainModel();
        string[] UserCard = { "Фамилия: ", "Имя: ", "Отчество: ", "Факультет: ", "Специальность: ", "Группа: ", "Курс: ", "Город проживания: ", "E-mail: ", "Номер телефона: " };//Массив-шаблон, создающий структуру файла карточки студента при её создании или редактировании

        public bool CheckInputData(string InputData)
        {
            if (InputData == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string FileName (string Surname, string Name, string Lastname, string Group)
        {
            string FileName = $"{Surname} {Name[0]}. {Lastname[0]}. {Group}";
            return FileName;
        }

        public string [] OldUserInformation ()
        {
            return Model.GetUserInformation();
        }

        public void LoadUserInformation (string Surname, string Name, string Lastname, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)
        {
            Model.CreateUser(Surname, Name, Lastname, Faculty, Speciality, Group, Course, City, Email, Phone, FileName(Surname, Name, Lastname, Group));
        }

        public void LoadFilterMethod (int FilterMethod)
        {
            Model.SaveFilterMethod(FilterMethod);
        }

        public void SaveUserChoice (int UserChoice)
        {
            string[] BufferValue = new string[3];
            string[][] UsersList = new string[Model.GetUsersNumbers()][];
            string TotalUsersString = "";
            UsersList = Model.GetUsersList(Model.GetFilterMethod());
            for (int i = 0; i < Model.GetUsersNumbers(); i++)//Цикл "пузырьковой" сортировки карточек студентов в зависимости от необходимого метода сортировки.
            {
                for (int j = i + 1; j < Model.GetUsersNumbers(); j++)
                {
                    if (string.Compare(UsersList[i][1], UsersList[j][1]) > 0)//Условный оператор, внутри которого проводится сравнение по алфавиту отдельных параметров сокращённого массива. В случае получения значения false запускает цикл.
                    {
                        BufferValue[0] = UsersList[j][0];
                        BufferValue[1] = UsersList[j][1];
                        BufferValue[2] = UsersList[j][2];
                        UsersList[j][0] = UsersList[i][0];
                        UsersList[j][1] = UsersList[i][1];
                        UsersList[j][2] = UsersList[i][2];
                        UsersList[i][0] = BufferValue[0];
                        UsersList[i][1] = BufferValue[1];
                        UsersList[i][2] = BufferValue[2];
                    }
                }
            }
            Model.SaveUserID((Int32.Parse(UsersList[UserChoice - 1][2])) - 1);
        }

        public string PresentUserInformation ()
        {
            string TotalPresentUserInformation = "Карточка студента ";
            string[] UserInformation = new string[10];
            UserInformation = Model.GetUserInformation();
            TotalPresentUserInformation = $"{TotalPresentUserInformation} {UserInformation[0]} {UserInformation[1][0]}. {UserInformation[2][0]}. {UserInformation[5]}\n\n";
            for (int i = 0; i < 10; i++)
            {
                TotalPresentUserInformation = TotalPresentUserInformation + UserCard[i] + UserInformation[i] + "\n";
            }
            return TotalPresentUserInformation;
        }

        public void InicializateDeleteProtocol()
        {
            Model.StartDeleteUser();
        }
        public string AllUsersList ()
        {
            string[] BufferValue = new string[3];
            string[][] UsersList = new string[Model.GetUsersNumbers()][];
            string TotalUsersString = "";
            UsersList = Model.GetUsersList(Model.GetFilterMethod());
            for (int i = 0; i < Model.GetUsersNumbers(); i++)//Цикл "пузырьковой" сортировки карточек студентов в зависимости от необходимого метода сортировки.
            {
                for (int j = i + 1; j < Model.GetUsersNumbers(); j++)
                {
                    if (string.Compare(UsersList[i][1], UsersList[j][1]) > 0)//Условный оператор, внутри которого проводится сравнение по алфавиту отдельных параметров сокращённого массива. В случае получения значения false запускает цикл.
                    {
                        BufferValue[0] = UsersList[j][0];
                        BufferValue[1] = UsersList[j][1];
                        BufferValue[2] = UsersList[j][2];
                        UsersList[j][0] = UsersList[i][0];
                        UsersList[j][1] = UsersList[i][1];
                        UsersList[j][2] = UsersList[i][2];
                        UsersList[i][0] = BufferValue[0];
                        UsersList[i][1] = BufferValue[1];
                        UsersList[i][2] = BufferValue[2];
                    }
                }
            }
            if (Model.GetFilterMethod() == 0)
            {
                for (int i = 1; i < Model.GetUsersNumbers() + 1; i++)
                {
                    TotalUsersString = TotalUsersString + $"{i}. {UsersList[i - 1][0]} \n";
                }
            }
            else
            {
                string BenchmarkValue = "";//Переменная, использующая в качестве эталона сортируемый параметр.
                string PresentText = "";//Переменная, выступающая шаблоном выводимого текста для разделения критериев сортировки.
                if (Model.GetFilterMethod() == 1)
                {
                    PresentText = "\nСтуденты факультета ";
                }
                else if (Model.GetFilterMethod() == 2)
                {
                    PresentText = "\nСтуденты специальности ";
                }
                else if (Model.GetFilterMethod() == 3)
                {
                    PresentText = "\nСтуденты группы ";
                }
                else if (Model.GetFilterMethod() == 4)
                {
                    PresentText = "\nСтуденты курса ";
                }
                for (int i = 1; i < Model.GetUsersNumbers() + 1; i++)
                {
                    if (BenchmarkValue != UsersList[i - 1][1])
                    {
                        BenchmarkValue = UsersList[i - 1][1];
                        TotalUsersString = TotalUsersString + $"{PresentText} {BenchmarkValue} \n";
                    }
                    TotalUsersString = TotalUsersString + $"{i}. {UsersList[i - 1][0]} \n";
                }
            }

            return TotalUsersString;
        }

        public string [] PreapareToEditUser(string[] LoadedInformation)
        {
            string[] OldInformation = new string[10];
            OldInformation = OldUserInformation();
            string[] NewInformation = new string[10];
            for (int i = 0; i < 10; i++)
            {
                if (LoadedInformation[i] == "" || LoadedInformation[i] == null)
                { 
                    NewInformation[i] = OldInformation[i]; 
                }
                else
                {
                    NewInformation[i] = LoadedInformation[i];
                }
            }
            return NewInformation;
        }

        public void EditUserInformation (string[] NewUserInformation)
        {
            Model.EditUser(NewUserInformation[0], NewUserInformation[1], NewUserInformation[2], NewUserInformation[3], NewUserInformation[4], NewUserInformation[5], NewUserInformation[6], NewUserInformation[7], NewUserInformation[8], NewUserInformation[9]);
        }

        public bool CheckUserChoise(string UsersChoice)
        {
            {
                if (int.TryParse(UsersChoice, out int UserInputInt))//Условный оператор, который пытается преобразовать введённое пользователем значение. Если это значение не числовое или выходит за границы количества карточек студентов - программа просто заново вызывает текущий метод.
                {
                    if (UserInputInt >= 0 && UserInputInt <= Model.GetUsersNumbers())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

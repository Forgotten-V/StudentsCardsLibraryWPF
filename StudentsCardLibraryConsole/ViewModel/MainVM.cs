using ModelClassLibrary;
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

        public bool CheckInputData(string InputData)        //Функция, возвращающая false в случае получения пустого значения и предотвращения создания пустых данных о пользователе.
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

        public string FileName (string Surname, string Name, string Lastname, string Group)     //Функция, принимающая ФИО пользователи и название его группы, чтобы
                                                                                                //вернуть уже готовое значение общей информации о пользователе.
        {
            string FileName = $"{Surname} {Name[0]}. {Lastname[0]}. {Group}";
            return FileName;
        }

        public string [] OldUserInformation ()      //Функция-ретранслятор, возвращающая информацию о выбранном пользователе для её
                                                    //дальнейшего сравнения с новой информацией при редактировании пользователя.
        {
            return Model.GetUserInformation();
        }

        public void LoadUserInformation (string Surname, string Name, string Lastname, string Faculty, string Speciality, string Group, string Course, string City, string Email, string Phone)     //Функция-ретранслятор, передающая данные в функцию для создания карточки пользователя и
                                                                                                                                                                                                    //дополнительно генерируящая общую информацию о пользователе.
        {
            Model.CreateUser(Surname, Name, Lastname, Faculty, Speciality, Group, Course, City, Email, Phone, FileName(Surname, Name, Lastname, Group));
        }

        public void LoadFilterMethod (int FilterMethod)     //Небольшая функция, просто сохраняющая выбранный пользователем метод фильтрации.
                                                            //Ранее она инициализировала запись этого метода в файл JSON, теперь просто
                                                            //присваивает значение глобальной переменной.
        {
            GlobalVariables.FilterMethod = FilterMethod;
        }

        public void SaveUserChoice (int UserChoice)         //Функция, принимающая значение введённого пользователем ID. До ввода MVVM просто искала нужного пользователя в уже
                                                            //отсортированном списке. Однако теперь необходимо вновь отсортировать всех пользователей лишь для того, чтобы
                                                            //благодаря введённому значению пользователя корректно нашёлся необходимый ID.
                                                            //Совсем плохо. Так делать нельзя(
        {
            string[] BufferValue = new string[3];
            string[][] UsersList = new string[Model.GetUsersNumbers()][];
            UsersList = Model.GetUsersList(GlobalVariables.FilterMethod);
            for (int i = 0; i < Model.GetUsersNumbers(); i++)
            {
                for (int j = i + 1; j < Model.GetUsersNumbers(); j++)
                {
                    if (string.Compare(UsersList[i][1], UsersList[j][1]) > 0)
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
            GlobalVariables.UserID = (Int32.Parse(UsersList[UserChoice - 1][2])) - 1;
        }

        public string PresentUserInformation ()             //Функция, возвращающая готовую переменную, в которой уже имеется
                                                            //вся информация о пользователе для дальнейшего ознакомления с ней.
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

        public void InicializateDeleteProtocol()            //Функция-ретранслятор, которая запускает процесс удаления пользователя внутри Model.
        {
            Model.StartDeleteUser();
        }
        public string AllUsersList ()                       //Функция, возвращающая единственную переменную, в которой уже находится полностью отсортированный список пользователей.
        {
            string[] BufferValue = new string[3];
            string[][] UsersList = new string[Model.GetUsersNumbers()][];
            string TotalUsersString = "";
            UsersList = Model.GetUsersList(GlobalVariables.FilterMethod);
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
            if (GlobalVariables.FilterMethod == 0)      //Условный оператор, позволяющий сделать создаваему переменную более информативной.
                                                        //При желании можно вывести список пользователей и по одному шаблону, но будет уже не так красиво.
            {
                for (int i = 1; i < Model.GetUsersNumbers() + 1; i++)
                {
                    TotalUsersString = TotalUsersString + $"{i}. {UsersList[i - 1][0]} \n";
                }
            }
            else
            {
                string BenchmarkValue = "";     //Переменная, использующая в качестве эталона сортируемый параметр.
                string PresentText = "";        //Переменная, выступающая шаблоном выводимого текста для разделения критериев сортировки.
                if (GlobalVariables.FilterMethod == 1)
                {
                    PresentText = "\nСтуденты факультета ";
                }
                else if (GlobalVariables.FilterMethod == 2)
                {
                    PresentText = "\nСтуденты специальности ";
                }
                else if (GlobalVariables.FilterMethod == 3)
                {
                    PresentText = "\nСтуденты группы ";
                }
                else if (GlobalVariables.FilterMethod == 4)
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

        public string [] PreapareToEditUser(string[] LoadedInformation)     //Функция, возвращающая  результат редактирования пользователя для возможности
                                                                            //ознакомления с изменениями и подтверждением их применения.
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

        public void EditUserInformation (string[] NewUserInformation)       //Функция-ретранслятор, принимающая обновлённые данные о пользователе и
                                                                            //отправляющая их в модель для споследующего сохранения в файле JSON.
        {
            Model.EditUser(NewUserInformation[0], NewUserInformation[1], NewUserInformation[2], NewUserInformation[3], NewUserInformation[4], NewUserInformation[5], NewUserInformation[6], NewUserInformation[7], NewUserInformation[8], NewUserInformation[9]);
        }

        public bool CheckUserChoise(string UsersChoice)
        {
            {
                if (int.TryParse(UsersChoice, out int UserInputInt))//Условный оператор, который пытается преобразовать введённое пользователем значение.
                                                                    //Если это значение не числовое или выходит за границы количества карточек
                                                                    //студентов - возвращает значение false.
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

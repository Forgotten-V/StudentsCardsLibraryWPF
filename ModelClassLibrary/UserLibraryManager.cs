using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCardsLibraryWPF.Model
{
    public class UserLibraryManager     //Класс, отвечающий за взаимодействие с JSON файлом библиотеки пользователей.
    {

        public UserLibraryManager(string FilePath)      //При инициализации класса он принимает в себя путь до библиотеки пользователей
                                                        //и сохраняет её в отдельную переменную для последующего использования. 
        {
            UserLibraryPath = FilePath;
        }

        string UserLibraryPath;

        public UserLibrary InitializateUserLibrary()            //Метод, вызывающий класс, необходимый для создания коллекции пользователей
        {
            if (File.Exists(UserLibraryPath))                   //Проверка наличия библиотеки пользователей по указанному пути. В случае её наличия
                                                                //считывает JSON файл и создаёт из него коллекцию пользователей (С помощью класса UserLibrary).
            {
                using (var stream = File.OpenRead(UserLibraryPath))
                {
                    var serializer = new DataContractJsonSerializer(typeof(UserLibrary));
                    return (UserLibrary)serializer.ReadObject(stream);
                }
            }
            else                                                //В случае отсутствия JSON файла создаёт экземпляр класса UserLibrary.
            {
                return new UserLibrary();
            }
        }



        public void AddUser(UserLibrary User)                   //Функция, которая просто дописывает/переписывает в существющей библиотеку пользователей информацию.
                                                                //В случае отсутствия файла с библиотекой пользователей функция не вернёт ошибку, а как настоящий GigaChad
                                                                //сама же и создаст новый файл с только-что созданным пользователем..
        {
            using (var stream = File.Create(UserLibraryPath))
            {
                var serializer = new DataContractJsonSerializer(typeof(UserLibrary));
                serializer.WriteObject(stream, User);
            }
        }

        public void UpdateUser(UserCards EditedUser)            //Функция, по ID находящая в списке JSON файла пользователей необходимый профиль и присваивает
                                                                //ему принятые значения
        {
            UserLibrary User = InitializateUserLibrary();
            UserCards ExistingUser = User.ULibrary.FirstOrDefault(u => u.ID == EditedUser.ID);//Поиск того-самого необходимого ID
            if (ExistingUser != null)
            {
                ExistingUser.ID = EditedUser.ID;
                ExistingUser.FIO = EditedUser.FIO;
                ExistingUser.Surname = EditedUser.Surname;
                ExistingUser.Name = EditedUser.Name;
                ExistingUser.LastName = EditedUser.LastName;
                ExistingUser.Faculty = EditedUser.Faculty;
                ExistingUser.Speciality = EditedUser.Speciality;
                ExistingUser.Group = EditedUser.Group;
                ExistingUser.Course = EditedUser.Course;
                ExistingUser.City = EditedUser.City;
                ExistingUser.Email = EditedUser.Email;
                ExistingUser.Phone = EditedUser.Phone;
                AddUser(User);                                  //Вызов функции, которая переписывает новые присвоенные значения
            }
        }

        public void DeleteUser(int ID)                          //Чисто технически, данная функция удаляет двух пользователей, и создаёт одного нового.
                                                                //По факту она удаляет однго пользователя и на его место устанавливает значения пользователя с самым высоким значением ID
        {
            UserLibrary User = InitializateUserLibrary();
            User.ULibrary.RemoveAll(u => u.ID == ID);
            AddUser(User);
        }

    }
}

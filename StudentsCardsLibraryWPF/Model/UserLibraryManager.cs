using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCardsLibraryWPF.Model
{
    public class UserLibraryManager
    {
        string UserLibraryPath;

        public UserLibraryManager(string FilePath)
        {
            UserLibraryPath = FilePath;
        }

        public UserLibrary InitializateUserLibrary()
        {
            if (File.Exists(UserLibraryPath))
            {
                using (var stream = File.OpenRead(UserLibraryPath))
                {
                    var serializer = new DataContractJsonSerializer(typeof(UserLibrary));
                    return (UserLibrary)serializer.ReadObject(stream);
                }
            }
            else
            {
                return new UserLibrary();
            }
        }



        public void AddUser(UserLibrary User)
        {
            using (var stream = File.Create(UserLibraryPath))
            {
                var serializer = new DataContractJsonSerializer(typeof(UserLibrary));
                serializer.WriteObject(stream, User);
            }
        }

        public void UpdateUser(UserCards EditedUser)
        {
            UserLibrary User = InitializateUserLibrary();
            UserCards ExistingUser = User.ULibrary.FirstOrDefault(u => u.ID == EditedUser.ID);
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
                AddUser(User);
            }
        }

        public void DeleteUser(int ID)
        {
            UserLibrary User = InitializateUserLibrary();
            User.ULibrary.RemoveAll(u => u.ID == ID);
            AddUser(User);
        }

    }
}

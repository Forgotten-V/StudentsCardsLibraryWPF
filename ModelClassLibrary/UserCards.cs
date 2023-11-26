using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCardsLibraryWPF.Model
{
    public class UserCards          //Класс, из которого создаются пустые экземпляры, в будущем используемые для создания и редактирования пользователей.
    {
        public int ID { get; set; } = 0;
        public string FIO { get; set; } = null;
        public string Surname { get; set; } = null;
        public string Name { get; set; } = null;
        public string LastName { get; set; } = null;
        public string Faculty { get; set; } = null;
        public string Speciality { get; set; } = null;
        public string Group { get; set; } = null;
        public string Course { get; set; } = null;
        public string City { get; set; } = null;
        public string Email { get; set; } = null;
        public string Phone { get; set; } = null;
    }
}

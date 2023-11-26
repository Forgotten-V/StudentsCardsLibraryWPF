using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCardsLibraryWPF.Model
{
    public class UserLibrary        //Класс, создающий коллекцию пользователей, сохранённых в JSON файл. Так же при
                                    //необходимости способен возвращать количество пользователей в созданной коллекции.
    {
        public List<UserCards> ULibrary { get; set; } = new List<UserCards>();

        public int Numbers()
        {
            return ULibrary.Count;
        }
    }
}

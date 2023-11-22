using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsCardsLibraryWPF.Model
{
    public class UserLibrary
    {
        public List<UserCards> ULibrary { get; set; } = new List<UserCards>();

        public int Numbers()
        {
            return ULibrary.Count;
        }
    }
}

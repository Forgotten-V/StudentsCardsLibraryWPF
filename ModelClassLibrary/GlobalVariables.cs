using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public class GlobalVariables        //Класс с несколькими глобальными переменными, созданный для замены файла JSON, в который ранее записывались эти данные.
    {
        public static string UserLibraryPath { get; set; } = Directory.GetCurrentDirectory() + "\\../../../../UserLibrary.json";
        public static int UserID { get; set; } = 0;

        public static int FilterMethod { get; set;} = 0;
    }
}

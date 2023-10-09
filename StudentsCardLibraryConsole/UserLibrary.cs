using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

public class UserLibrary
{
    public List<UserCards> ULibrary { get; set; } = new List<UserCards>();

    public int Numbers()
    {
        return ULibrary.Count;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Classes
{
    public class Conflict
    {
        public Conflict(string tag1, string tag2)
        {
            Tag1 = tag1;
            Tag2 = tag2;
        }
        public Conflict()
        {
        }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack_algorytm.model
{
   public class Items
    {
        public Items(string name, int area, int value, int id)
        {
            this.name = name;
            this.area = area;
            this.value = value;
            this.id = id;
        }

        public int id { get; set; }
        public string name { get; set; }
        public int area { get; set; }
        public int value { get; set; }
        public Items() { }
    }
}

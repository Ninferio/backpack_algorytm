using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack_algorytm.model
{
    class Items
    {
        public Items(string name, int area, int value)
        {
            this.name = name;
            this.area = area;
            this.value = value;
        }

        public string name { get; set; }
        public int area { get; set; }
        public int value { get; set; }
        public Items() { }
    }
}

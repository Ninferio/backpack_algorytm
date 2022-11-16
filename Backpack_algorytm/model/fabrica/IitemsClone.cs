using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpack_algorytm.model.fabrica
{
    interface IitemsClone
    {
        IitemsClone Clone();
        void CreateItem();

        
    }
}

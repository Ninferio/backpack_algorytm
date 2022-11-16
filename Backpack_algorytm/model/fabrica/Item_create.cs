using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Backpack_algorytm.model.fabrica
{
     class Item_create : IitemsClone
        {
        StackPanel stp_main = new StackPanel();

        /// <summary>
        /// хз, кароч стакпанел сюда да
        /// </summary>
        /// <param name="st">Контейнер</param>
        public Item_create(StackPanel st)
        {
            this.stp_main = st;
        }
        /// <summary>
        /// Тут клонируем 
        /// </summary>
        /// <returns></returns>
        public IitemsClone Clone()
        {
            return new Item_create(this.stp_main);
        }
        /// <summary>
        /// Тут создаем 
        /// </summary>
        public void CreateItem()
        {
            stp_main.Orientation = Orientation.Horizontal;
            Label label = new Label();
            label.Content = "";
            
        }
    }
}

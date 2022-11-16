using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Backpack_algorytm.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add_item_wondow.xaml
    /// </summary>
    public partial class Add_item_wondow : Window
    {
        public Add_item_wondow()
        {
            InitializeComponent();
            /*
            Loaded += delegate
            {
                MinWidth = ActualWidth;
                MinHeight = ActualHeight;
            };*/
        }

        private void Btn_add_item_Click(object sender, RoutedEventArgs e)
        {
            string name = txb_name.Text;
            string value = txb_value.Text;
            string area = txb_area.Text;
            if(name != ""||value != "" ||area != "")
            {
                MessageBox.Show("lesgooo");
            }
        }
    }
}

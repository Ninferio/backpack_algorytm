using Backpack_algorytm.model;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Backpack_algorytm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //лист предметов
        List<Items> items = new List<Items>();
        int count;
        int max_space = 2000;
        int[,] table;
        int matrix_height;
        int matrix_wight;
        public MainWindow()
        {
            InitializeComponent();

            //@"items_example.txt"
            string path = @"Items.txt";

            GetItemCount(path);



            string line;
            //Items[] items = new Items[count];
            TextReader reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                Items item = new Items();
                string[] subs = line.Split(',');
                int i = 0;
                foreach (var word in subs)
                {
                    switch (i)
                    {
                        case 0:
                            i++;
                            item.name = word;
                            break;

                        case 1:
                            i++;
                            item.area = Convert.ToInt32(word);
                            break;

                        case 2:
                            i++;
                            item.value = Convert.ToInt32(word);
                            break;
                    }
                }
                items.Add(item);
            }
            reader.Close();
        }
        public void GetItemCount(string path)
        {

            string line;
            TextReader reader = new StreamReader(path);
            while ((line = reader.ReadLine()) != null)
            {
                count++;
            }
            reader.Close();

        }

        public void GoBackPack()
        {
            
            matrix_height = count + 1;
            matrix_wight = max_space + 1;
            table = new int[matrix_height, matrix_wight];
            

            //перебор предметам

            for (int i = 0; i < matrix_height; i++)
            {
               
                //перебор по весу
                for (int j = 0; j < matrix_wight; j++)
                {
                    //заполение 0-лями 0-левых элементов (предмет - 0, вес - 0)
                    if (i == 0 || j == 0)
                    {
                        table[i, j] = 0;
                    }
                    else
                    {
                        int backpack_weight = j;
                        //вес, объем
                        int area_item = items[i - 1].area;
                        //стоимость, значимость
                        int value_item = items[i - 1].value;
                        //вес предмета меньше веса рюкзака?
                        if (area_item < backpack_weight)
                        {
                            
                            //стоимость предмета с верху
                            int first = table[i-1, j];
                            //какая-то страшная формула
                            int second = table[i - 1, j - area_item] + value_item;

                            //максимум между 2-мя числами
                            int answer = Math.Max(first, second);
                            table[i, j] = answer;
                        }
                        else
                        {
                            //берем стоимость предмета выше
                            table[i, j] = table[i - 1, j];
                        }

                    }





                }




            }
            GetMatrix(table);


        }

        public void GetMatrix(int[,] Matrix)
        {
           // int answer = Matrix[matrix_height, matrix_wight];
           // MessageBox.Show(answer.ToString());
           
            for (int i = 0; i < matrix_height; i++)
            {
                string str = "";
                //перебор по весу
                for (int j = 0; j < matrix_wight; j++)
                {
                    str += " "+ Matrix[i, j];
                }
                lblmatrix.Content += str + "\n";
            }
            lblmatrix.Content += "=================" + "\n";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoBackPack();
        }
    }
}

using Backpack_algorytm.model;
using Backpack_algorytm.Windows;
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
using System.Text.RegularExpressions;

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
        string path = @"Items\";
 
        int max_space = 0;
        int[,] table;
        int matrix_height;
        int matrix_wight;
        public MainWindow()
        {
            InitializeComponent();
            
            Get_file_from_folder();
            //Get_items_from_file(cmb_files.Text);

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

        public void GoBackPack(Button button)
        {
            MessageBox.Show(button.Name);
            GetItemCount(@"Items\\" + cmb_files.SelectedItem);
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
                        //вес рюкзака
                        int backpack_weight = j;
                        //вес, объем предмета
                        int area_item = items[i - 1].area;
                        //стоимость, значимость предмета
                        int value_item = items[i - 1].value;
                        //вес предмета меньше веса рюкзака?
                        if (area_item <= backpack_weight)
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
            List<Items> enable_items = new List<Items>();
            int k = matrix_wight-1;
            //проходим по всем предметам с конца
            while (k >= 0)
            {
                for (int i = count; i > 0; i--)
                {

                    if (table[i, k] != table[i - 1, k])
                    {
                        enable_items.Add(items[i - 1]);
                        k -= items[i - 1].area;
                        break;
                    }
                    else
                    {
                        if (table[i, k] == 0& table[i - 1, k] == 0)
                        {
                            k -= 1;
                            break;
                        }
                    }
                    



                }
            }
                
            
            
                int all_value = 0;

            int all_area = 0;
            foreach (var a in enable_items)
            {
                all_value += a.value;
                all_area += a.area;
            }
            MessageBox.Show(String.Format("Стоимость: {0} \n Площадь: {1}", all_value, all_area));
            
            dtg_select_items_table.ItemsSource = enable_items;
                    //GetMatrix(table);
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
                //lblmatrix.Content += str + "\n";
            }
            
            //lblmatrix.Content += "=================" + "\n";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "btn_Bounded_Knapsack_Problem":
                    GoBackPack(button);
                    btn_Unbounded_Knapsack_Problem.IsEnabled = false;
                    btn_Continuous_Knapsack_Problem.IsEnabled = false;
                    break;
                case "btn_Unbounded_Knapsack_Problem":
                    GoBackPack(button);
                    btn_Bounded_Knapsack_Problem.IsEnabled = false;
                    btn_Continuous_Knapsack_Problem.IsEnabled = false;
                    break;
                case "btn_Continuous_Knapsack_Problem":
                    GoBackPack(button);
                    btn_Unbounded_Knapsack_Problem.IsEnabled = false;
                    btn_Bounded_Knapsack_Problem.IsEnabled = false;
                    break;

                case "btn_delete_result":
                    btn_Continuous_Knapsack_Problem.IsEnabled = false;
                    btn_Unbounded_Knapsack_Problem.IsEnabled = false;
                    btn_Bounded_Knapsack_Problem.IsEnabled = false;
                    break;
            }
            
        }

        private void Btn_delete_item_Click(object sender, RoutedEventArgs e)
        {
            Items selected_item = (Items)dtg_items_table.SelectedItem;
            //dtg_items_table.SelectedItem.
           
        }

        private void Btn_add_item_Click(object sender, RoutedEventArgs e)
        {
            Items selected_item = (Items)dtg_items_table.SelectedItem;

            //Window window = new Add_item_wondow();
            //window.Show();
            int max_id = items.Max(x => x.id) +1;
            items.Add(new Items("",0,0, max_id));
            ReloadDTG();
        }
        public void ReloadDTG()
        {
            dtg_items_table.ItemsSource = null;
            dtg_items_table.ItemsSource = items;
        }

        private void Dtg_items_table_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           

            btn_save_change.IsEnabled = true;
            btn_delete_item.IsEnabled = true;
           
            
        }
        public void UpdateItemFile(Items predmet)
        {
            
        }
        private void Btn_save_change_Click(object sender, RoutedEventArgs e)
        {
           
            List<Items> select_items = new List<Items>();
            foreach (var data in dtg_items_table.SelectedItems)
            {
                Items item = data as Items;
                select_items.Add(item);
            }
            //Удоление выбранных предметов
            foreach (var item in select_items)
            {
                string item_string = String.Format("{0},{1},{2}", item.name, item.area, item.value);
                //перепор файла
                string line;
                TextReader reader = new StreamReader(@"Items\\" + cmb_files.SelectedItem.ToString());

                while ((line = reader.ReadLine()) != null)
                {

                    if (line != item_string)
                    {

                    }

                }
                reader.Close();

            }

        }
        /// <summary>
        /// Создание списка файлов
        /// </summary>
        /// <param name="files">Массив строк</param>
        /// <returns>Масссив имен файлов</returns>
        public void Get_file_from_folder()
        {
            string path = @"Items";
            int i = 0;
            string[] allfiles = Directory.GetFiles(path);
            int count = allfiles.Length;
            string[] files = new string[count];
            foreach (string filename in allfiles)
            {
                
                
                files[i] = filename.Replace("Items\\", "");
                i++;
            }
            cmb_files.ItemsSource = files;
            cmb_files.SelectedItem = files[1];
            
           



        }
        public void Get_items_from_file(string path)
        {
            items = new List<Items>();
            //GetItemCount(path);
            int number = 0;

            
            string line;
            //Items[] items = new Items[count];
            TextReader reader = new StreamReader(@"Items\\"+path);
            while ((line = reader.ReadLine()) != null)
            {
                number++;
                Items item = new Items();
                item.id = number;
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
            ReloadDTG();
        }

        private void Cmb_files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Get_items_from_file(cmb_files.SelectedItem.ToString());
        }

        private void Txb_backpakc_area_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Txb_backpakc_area_TextChanged(object sender, TextChangedEventArgs e)
        {
            max_space = Convert.ToInt32(txb_backpakc_area.Text);
            
        }
    }
}

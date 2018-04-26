using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Спартак_Диплом
{
    /// <summary>
    /// Логика взаимодействия для InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {
        public InsertWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex++;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex--;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable Table = AdapterKeeper.ClientAdapter.GetData();

            foreach (DataRow Row in Table.Rows)
            {
                listBox.Items.Add(new ListBoxItem()
                {
                    Content = Row.Field<string>("ClientName").ToString(),
                    DataContext = Row.Field<int>("ClientCode").ToString()
                });
            }
        }
    }
}

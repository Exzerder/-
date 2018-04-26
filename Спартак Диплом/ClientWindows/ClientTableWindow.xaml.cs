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

namespace Спартак_Диплом
{
    /// <summary>
    /// Логика взаимодействия для ClientTableWindow.xaml
    /// </summary>
    public partial class ClientTableWindow : Window
    {
        public ClientTableWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddRecord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = AdapterKeeper.ClientAdapter.GetData();
        }

        private void ButtonAddRecord_Click_1(object sender, RoutedEventArgs e)
        {
            new ClientAddingWindow().ShowDialog();
            
        }
    }
}

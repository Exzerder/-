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
    /// Логика взаимодействия для TableSelectionWindow.xaml
    /// </summary>
    public partial class TableSelectionWindow : Window
    {
        public TableSelectionWindow()
        {
            InitializeComponent();
        }

        private void OpenOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new InsertWindow().Show();
            Close();
        }

        private void ButtonOpenProducts_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void ButtonOpenCliens_Click(object sender, RoutedEventArgs e)
        {
            new ClientTableWindow().Show();
            this.Close();
        }
    }
}

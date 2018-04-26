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
    /// Логика взаимодействия для ClientAddingWindow.xaml
    /// </summary>
    public partial class ClientAddingWindow : Window
    {
        public ClientAddingWindow()
        {
            InitializeComponent();
        }

        //Флаг физического лица
        private void ChkBoxIndividual_Click(object sender, RoutedEventArgs e)
        {
            if (ChkBoxIndividual.IsChecked.Value)
                GridIndividual.Visibility = Visibility.Visible;
            else
                GridIndividual.Visibility = Visibility.Hidden;
        }

        //Закрытие формы
        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Добавление записи 
        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            AdapterKeeper.ClientAdapter.Insert(TBxName.Text, TBxMail.Text, TBxAdress.Text, TBxPhone.Text, ChkBoxIndividual.IsChecked.Value);
            Close();
        }
    }
}

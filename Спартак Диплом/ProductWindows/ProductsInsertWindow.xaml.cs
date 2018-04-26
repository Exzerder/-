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
    /// Логика взаимодействия для ProductsInsertWindow.xaml
    /// </summary>
    public partial class ProductsInsertWindow : Window
    {
        public short ClassCode;
        public short GroupCode;
        public short SubGroupCode;

        public ProductsInsertWindow()
        {
            InitializeComponent();
        }

        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            AdapterKeeper.ProductAdapter.Insert(int.Parse(TBxProductCode.Text), TBxShortName.Text, TBxFullName.Text,
                TBxOKPCode.Text, ClassCode, GroupCode, SubGroupCode, TBxVedCode.Text, 0, 1);
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Спартак_Диплом
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        short ClassCode, GroupCode, SubGroupCode;
        
        // Функция рассчета уровня выбранного элемента дерева
        int TreeViewLevel()
        {
            var Current = ((TreeViewItem)treeView.SelectedItem);
            var Count = 0;

            while (Current.Parent is TreeViewItem)
            {
                Current = ((TreeViewItem)Current.Parent);
                Count++;
            }
            return Count;
        }

        //Загрузка окна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Формирование древовидного массива из записей БД
            TreeViewItem NewItem = new TreeViewItem();

            DataTable ClassTable = AdapterKeeper.ClassAdapter.GetData();
            DataTable GroupTable;
            DataTable SubGroupTable;
            
            for (int i = 0; i < ClassTable.Rows.Count; i++)
            {
                //Заполнение узлов из таблицы Class
                NewItem = new TreeViewItem();
                NewItem.Header = ClassTable.Rows[i].Field<string>("ClassName").ToString();
                NewItem.DataContext = ClassTable.Rows[i].Field<short>("ClassCode").ToString();
                treeView.Items.Add(NewItem);
                //
                GroupTable = AdapterKeeper.GroupAdapter.GetGroups(short.Parse(((TreeViewItem)treeView.Items[i]).DataContext.ToString()));

                for (int j = 0; j < GroupTable.Rows.Count; j++)
                {
                    //Заполнение узлов из таблицы Group
                    NewItem = new TreeViewItem();
                    NewItem.Header = GroupTable.Rows[j].Field<string>("GroupName").ToString();
                    NewItem.DataContext = GroupTable.Rows[j].Field<short>("GroupCode").ToString();
                    ((ItemsControl)treeView.Items[i]).Items.Add(NewItem);
                    //
                    SubGroupTable = AdapterKeeper.SubGroupAdapter.GetSubGroups(short.Parse(((TreeViewItem)treeView.Items[i]).DataContext.ToString()), 
                        short.Parse(((TreeViewItem)((TreeViewItem)treeView.Items[i]).Items[j]).DataContext.ToString()));

                    foreach(DataRow Row in SubGroupTable.Rows)
                    {
                        //Заполнение узлов из таблицы SubGroup
                        NewItem = new TreeViewItem();
                        NewItem.Header = Row.Field<string>("SubGroupName").ToString();
                        NewItem.DataContext = Row.Field<short>("SubGroupCode").ToString();
                        ((ItemsControl)((ItemsControl)treeView.Items[i]).Items[j]).Items.Add(NewItem);
                        //
                    }
                }
            }
            //
        }

        //Добавлени корня в дерево
        private void ButtonAddRoot_Click(object sender, RoutedEventArgs e)
        {
            new ProductsRootAddWindow().ShowDialog();
        }

        //Удаление записи из таблицы
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить выбранные записи?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                while(dataGrid.SelectedItems.Count != 0)
                {
                    AdapterKeeper.ProductAdapter.DeleteQuery(int.Parse((dataGrid.SelectedItems[0] as DataRowView).Row["ProductCode"].ToString()));
                    (dataGrid.SelectedItems[0] as DataRowView).Row.Delete();
                }
        }

        //Удаление узла из дерева
        private void ButtonDelUzel_Click(object sender, RoutedEventArgs e)
        {
            new ProductsNodeDeleteWindow().ShowDialog();
        }

        //Добавление записи
        private void ButtonAddRecord_Click(object sender, RoutedEventArgs e)
        {
            ProductsInsertWindow NewWindow = new ProductsInsertWindow()
            {
                ClassCode = ClassCode,
                GroupCode = GroupCode,
                SubGroupCode = SubGroupCode
            };
            NewWindow.Show();
        }

        //Изменение записи
        private void ButtonRewrite_Click(object sender, RoutedEventArgs e)
        {
            ProductsInsertWindow NewWindow = new ProductsInsertWindow()
            {
                ClassCode = ClassCode,
                GroupCode = GroupCode,
                SubGroupCode = SubGroupCode
            };
            NewWindow.ButAdd.Content = "Изменить";
            NewWindow.TBxProductCode.Text = (dataGrid.SelectedItem as DataRowView).Row["ProductCode"].ToString();
            NewWindow.TBxShortName.Text = (dataGrid.SelectedItem as DataRowView).Row["ProductNameShort"].ToString();
            NewWindow.TBxFullName.Text = (dataGrid.SelectedItem as DataRowView).Row["ProductName"].ToString();
            NewWindow.TBxVedCode.Text = (dataGrid.SelectedItem as DataRowView).Row["VedCode"].ToString();
            NewWindow.TBxOKPCode.Text = (dataGrid.SelectedItem as DataRowView).Row["OkpCode"].ToString();
            NewWindow.TBxCost.Text = (dataGrid.SelectedItem as DataRowView).Row["Cost"].ToString();
            NewWindow.Show();
            
        }

        //Изменение выбранного пункта в древе, отображение таблицы
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //Вывод пути в StatusBar
            string Path = "";

            var Current = ((TreeViewItem)treeView.SelectedItem);

            while (Current is TreeViewItem)
            {
                Path = Current.Header + "/" + Path;
                if (!(Current.Parent is TreeViewItem))
                    break;
                Current = ((TreeViewItem)Current.Parent);
            }
            this.Path.Content = Path;
            //

            //Открытие таблицы продуктов при выбранной подгруппе
            var ProductTable = AdapterKeeper.ProductAdapter;
            switch (TreeViewLevel())
            {
                case 2:
                    Current = ((TreeViewItem)treeView.SelectedItem);
                    SubGroupCode = short.Parse(Current.DataContext.ToString());
                    Current = ((TreeViewItem)Current.Parent);
                    GroupCode = short.Parse(Current.DataContext.ToString());
                    Current = ((TreeViewItem)Current.Parent);
                    ClassCode = short.Parse(Current.DataContext.ToString());
                    dataGrid.ItemsSource =  ProductTable.GetFullView(ClassCode, GroupCode, SubGroupCode).DefaultView;
                break;
                default:
                    break;
            }
            //
        }

        //Ввод текста в поле поиска
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView Table = AdapterKeeper.ProductAdapter.GetFullView(ClassCode, GroupCode, SubGroupCode).DefaultView;
            Table.RowFilter = string.Format("ProductName Like '%{0}%'", TBxSearch.Text);
            dataGrid.ItemsSource = Table.ToTable().DefaultView;
        }

        //Открытие меню при закрытии окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new TableSelectionWindow().Show();
        }
    }
}

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

namespace OOO_Sport_Products.View
{
    /// <summary>
    /// Логика взаимодействия для WindowOrder.xaml
    /// </summary>
    public partial class WindowOrder : Window
    {
        List<Classes.ProductInOrder> listOrder;

        public WindowOrder()
        {
            InitializeComponent();
        }
        public WindowOrder(List<Classes.ProductInOrder> listOrder)
        {
            InitializeComponent();
            this.listOrder = listOrder;	//Перенести из параметра в глобальный элемент окна
            ShowInfo();			//Показать товары в заказе
        }

        private void ShowInfo()
        {
            //Перенести данные о заказе в интерфейс
            listBoxProductsInOrder.ItemsSource = listOrder;
        }

        private void ButtonNavigation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

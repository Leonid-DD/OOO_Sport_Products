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
    /// Логика взаимодействия для WindowCatalog.xaml
    /// </summary>
    public partial class WindowCatalog : Window
    {
        public WindowCatalog()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Возврат на окно авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNavigation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Загрузка окна - отображение товаров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Model.Product> listProducts = new List<Model.Product>();
            listProducts = Classes.Helper.DB.Products.ToList();
            ListBoxProducts.ItemsSource = listProducts;
        }
    }
}

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
using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;

namespace OOO_Sport_Products.View
{
    /// <summary>
    /// Логика взаимодействия для WindowCatalog.xaml
    /// </summary>
    public partial class WindowCatalog : Window
    {
        int totalCount;

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
        /// Загрузка окна - отображение товаров и настройка фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Информация о пользователе
            //Зарегистрированный пользователь
            if (Helper.user != null)
            {
                tbUserName.Text = "Пользователь: " + Helper.user.UserFullName;
            }
            //Гость
            else
            {
                tbUserName.Text = "Пользователь: Гость";
            }
            SelectProducts();

            //Настройка ComboBox для фильтрации по скидке
            cbDiscount.Items.Clear();
            cbDiscount.Items.Add("Все диапазоны");
            cbDiscount.Items.Add("0-9,99%");
            cbDiscount.Items.Add("10-14,99%");
            cbDiscount.Items.Add("15% и более");
            cbDiscount.SelectedIndex = 0;

            //Перенос списка категорий в ComboBox для сортировки
            List<Category> categories = Helper.DB.Categories.ToList();
            Category allCat = new Category();
            allCat.CategoryID = 0;
            allCat.CategoryName = "Все категории";
            categories.Insert(0, allCat);
            cbCategory.Items.Clear();
            cbCategory.ItemsSource = categories;

            //Настройка фильтрации по категории
            cbCategory.DisplayMemberPath = "CategoryName";
            cbCategory.SelectedValuePath = "CategoryId";
            cbCategory.SelectedIndex = 0;
            cbCategory.SelectedValue = 0;

            //Установка сортировки по возрастанию цены
            rbSortAsc.IsChecked = true;

            //Отображение списка товаров
            SelectProducts();
        }

        /// <summary>
        /// Фильтрация списка товаров
        /// </summary>
        public void SelectProducts()
        {
            List<ProductExtended> listProductsExt = new List<ProductExtended>();

            //Список товаров из БД
            List<Model.Product> listProducts = new List<Model.Product>();
            listProducts = Classes.Helper.DB.Products.ToList();
            totalCount = listProducts.Count;

            //Фильтрация по скидке
            double minDiscount = 0;
            double maxDiscount = 100;
            switch (cbDiscount.SelectedIndex)
            {
                case 1:
                    maxDiscount = 9.99;
                    break;
                case 2:
                    minDiscount = 10;
                    maxDiscount = 14.99;
                    break;
                case 3:
                    minDiscount = 15;
                    break;
                default:
                    break;
            }
            //Выборка фильтрации по скидке
            listProducts = listProducts.Where(p => p.ProductDiscountMax <= maxDiscount && p.ProductDiscountCurrent >= minDiscount).ToList();

            //Фильтрация по категории
            if (cbCategory.SelectedIndex > 0)
            {
                listProducts = listProducts.Where(p => p.ProductCategory == cbCategory.SelectedIndex).ToList();
            }

            //Поиск по названию
            string search = tbSearch.Text;
            if (search.Length > 0)
            {
                listProducts = listProducts.Where(p => p.ProductName.Contains(search)).ToList();
            }

            //Вывод количества отфильтрованных товаров
            int filterCount = listProducts.Count;
            tbProductCount.Text = "Выбрано товаров: " + "\n" + filterCount.ToString() + " из " + totalCount.ToString();

            //Перенос списка товаров в расширенный список
            foreach (Model.Product product in listProducts) 
            {
                ProductExtended productExtended = new ProductExtended();
                productExtended.product = product;
                listProductsExt.Add(productExtended);
            }

            //Сортировка по цене
            if ((bool)rbSortAsc.IsChecked)
            {
                listProductsExt = listProductsExt.OrderBy(p => p.ProductDiscountCost).ToList();
            }
            else
            {
                listProductsExt = listProductsExt.OrderByDescending(p => p.ProductDiscountCost).ToList();
            }

            //Вывод отфильтрованного списка товаров
            ListBoxProducts.ItemsSource = listProductsExt;
        }

        //Изменение в поле поиска
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectProducts();
        }

        //Сортировка по скидке
        private void cbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectProducts();
        }

        //Сортировка по категории
        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectProducts();
        }

        //Сортировка по возрастанию цены
        private void rbSortAsc_Checked(object sender, RoutedEventArgs e)
        {
            SelectProducts();
        }

        //Сортировка по убыванию цены
        private void rbSortDesc_Checked(object sender, RoutedEventArgs e)
        {
            SelectProducts();
        }

        //Контекстное меню добавления товара в заказ
        private void miAddInOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
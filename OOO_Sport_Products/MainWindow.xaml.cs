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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOO_Sport_Products
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Количество оставшихся попыток
        int remainingTries = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Начальные настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Подключение к БД
            Classes.Helper.DB = new Model.DBSportProducts();
        }

        /// <summary>
        /// Завершение работы приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNavigation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Вход без ввода логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGuest_Click(object sender, RoutedEventArgs e)
        {

        }
        
        /// <summary>
        /// Обработка логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            //string password = TextBoxPassword.Text;
            string password = PasswordBoxAuthorization.Password;
            StringBuilder sb = new StringBuilder();

            //Обработка пустоты
            if (login == "") 
            {
                sb.Append("Логин не введен.\n");
            }
            if (password == "") 
            {
                sb.Append("Пароль не введен.\n");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Поиск логина и пароля в БД
            List<Model.User> users = Classes.Helper.DB.Users.ToList();

            //Авторизация через foreach
            ////foreach (Model.User user in users)
            ////{
            ////    if (user.UserLogin.Equals(login))
            ////    {
            ////        if (user.UserPassword.Equals(password))
            ////        {
            ////            //Переход на следующую страницу в соответствии с ролью пользователя
            ////            Classes.Helper.user = user;
            ////            sb.Append("Имя: " + user.UserFullName + " ; Код роли: " + user.UserRole + " ; Название роли: " + user.Role.RoleName);
            ////            return;
            ////        }
            ////        sb.Append("Пароль неверен. Осталась 1 попытка.");
            ////        break;
            ////    }
            ////}
            ////if (sb.Length == 0)
            ////{
            ////    sb.Append("Пользователь не найден.");
            ////}
            ////MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            
            //Авторизация через where
            Model.User user = users.Where(u=>u.UserLogin.Equals(login)).FirstOrDefault();
            if (user != null)
            {
                if (user.UserPassword.Equals(password))
                {
                    Classes.Helper.user = user;
                    sb.Append("Имя: " + user.UserFullName + " ; Код роли: " + user.UserRole + " ; Название роли: " + user.Role.RoleName);
                    MessageBox.Show(sb.ToString(), "Пользователь", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else if (remainingTries > 0)
                {
                    sb.Append("Введен неверный пароль. Осталось " + remainingTries + " попыток.");
                    remainingTries--;
                }
                else
                {

                }
            }
            else 
            {
                sb.Append("Пользователь не найден.");
            }
            MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Обработчик переключения видимости пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxPasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CheckBoxPasswordVisibility.IsChecked)
            {
                TextBoxPassword.Visibility = Visibility.Visible;
                PasswordBoxAuthorization.Visibility = Visibility.Hidden;
                TextBoxPassword.Text = PasswordBoxAuthorization.Password;
            }
            else
            {
                TextBoxPassword.Visibility = Visibility.Hidden;
                PasswordBoxAuthorization.Visibility = Visibility.Visible;
                PasswordBoxAuthorization.Password = TextBoxPassword.Text;
            }
        }
    }
}

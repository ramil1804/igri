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
using WpfApp1.db;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            var user = ConnectionDB.db1.logins.FirstOrDefault(log => log.login == login && log.passwod == password);

            var tempUser = new users() { Name = login };
            var tempLogin = new logins()
            {
                login = login,
                passwod = password,
                users = tempUser
            };
            ConnectionDB.db1.users.Add(tempUser);
            ConnectionDB.db1.logins.Add(tempLogin);
            ConnectionDB.db1.SaveChanges();
            MessageBox.Show("Регистрация прошла успешно");
            NavigationService.GoBack();
            return;
        }
       
    }
}

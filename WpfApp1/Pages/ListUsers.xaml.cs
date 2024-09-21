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
    /// Логика взаимодействия для ListUsers.xaml
    /// </summary>
    public partial class ListUsers : Page
    {
        public ListUsers()
        {
            InitializeComponent();
            UserList.ItemsSource = ConnectionDB.db1.users.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem != null)
            {
                users users = UserList.SelectedItem as users;                
                users.Balance = Convert.ToInt32(txtEditMoney.Text);               
                ConnectionDB.db1.SaveChanges();
                UserList.ItemsSource = ConnectionDB.db1.users.ToList();
            }
        }
    }
}

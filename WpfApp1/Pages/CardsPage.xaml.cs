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
    /// Логика взаимодействия для CardsPage.xaml
    /// </summary>
    public partial class CardsPage : Page
    {
        public CardsPage()
        {
            InitializeComponent();
            ListCard.ItemsSource = ConnectionDB.db1.Card.ToList();
            txtBalance.Text = ConnectionDB.user.Balance.ToString();
        }

        private void ZabratMoney_Click_1(object sender, RoutedEventArgs e)
        {
            if (ListCard.SelectedItem != null)
            {
                Card card = ListCard.SelectedItem as Card;
                card.money = card.money - Convert.ToInt32(txtBoxMoney.Text);
                //txtBalance.Text = Convert.ToString(Convert.ToInt32(txtBalance.Text) + Convert.ToInt32(txtBoxMoney.Text));
                ConnectionDB.user.Balance = Convert.ToInt32(txtBalance.Text) + Convert.ToInt32(txtBoxMoney.Text);
                txtBoxMoney.Text = "";
                ConnectionDB.db1.SaveChanges();
                ListCard.ItemsSource = ConnectionDB.db1.Card.ToList();
            }
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameSelection());
        }

        private void VivestiMoney_Click(object sender, RoutedEventArgs e)
        {
            if (ListCard.SelectedItem != null)
            {
                Card card = ListCard.SelectedItem as Card;
                card.money = card.money + Convert.ToInt32(txtBoxMoney.Text);
                //txtBalance.Text = Convert.ToString(Convert.ToInt32(txtBalance.Text) + Convert.ToInt32(txtBoxMoney.Text));
                ConnectionDB.user.Balance = Convert.ToInt32(txtBalance.Text) - Convert.ToInt32(txtBoxMoney.Text);
                txtBoxMoney.Text = "";
                ConnectionDB.db1.SaveChanges();
                ListCard.ItemsSource = ConnectionDB.db1.Card.ToList();
            }
        }
    }
}

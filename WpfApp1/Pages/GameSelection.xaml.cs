using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Threading;
using WpfApp1.db;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для GameSelection.xaml
    /// </summary>
    public partial class GameSelection : Page
    {
        double totalSpin;
        double spined;
        Random random;
        WinColor Color;
        WinColor Sel = WinColor.Red;
        DispatcherTimer SpinTimer;

        public GameSelection()
        {
            InitializeComponent();
            txtBalance.Text = ConnectionDB.user.Balance.ToString();
            SpinTimer = new DispatcherTimer();
            SpinTimer.Interval = TimeSpan.FromMilliseconds(1);
            SpinTimer.Tick += SpinTimer_Tick;
            random = new Random();
        }

        private void Bet(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtBalance.Text) < Convert.ToInt32(BetTB.Text))
            {
                MessageBox.Show("Недостаточно средств");
            }
            else
            {
                txtBalance.Text = Convert.ToString(Convert.ToInt32(txtBalance.Text) - Convert.ToInt32(BetTB.Text));              
                var selected = (sender as Button).DataContext;
                if (selected == "red")
                    Sel = WinColor.Red;
                if (selected == "green")
                    Sel = WinColor.Green;
                else
                    Sel = WinColor.Black;

                totalSpin = (random.NextDouble() + 0.5) * 360d;

                var sector = random.Next(1,38); 
                if (sector == 19)
                {
                    Color = WinColor.Green;
                }
                else if (sector >= 1 && sector < 19)
                {
                    Color = WinColor.Red;
                }
                else if (sector >= 20 && sector <= 38)
                {
                    Color = WinColor.Black;
                } 

                var bet = int.Parse(BetTB.Text);
                SpinTimer.IsEnabled = true;
                SpinTimer.Start();         
            }           
        }

        private void SpinTimer_Tick(object sender, EventArgs e)
        {                            
            spined += 1;
            if (spined > totalSpin)
            {
                SpinTimer.Stop();
                spined = 0;
                if (Color == Sel)
                {
                    var winRatio = 2;
                    if (Color == WinColor.Green)
                        winRatio = 15;

                    var bet = int.Parse(BetTB.Text) * winRatio;
                    var postavil = Convert.ToInt32(BetTB.Text);
                    var result = "Выйграл";
                    var history = ConnectionDB.db1.HistoryMatches.FirstOrDefault(id => id.postavil == postavil && id.result == result);

                    var tempHistory = new HistoryMatches()
                    {
                        postavil = postavil,
                        result = result
                    };
                    txtBalance.Text = Convert.ToString(Convert.ToInt32(txtBalance.Text) + bet);                  
                    ConnectionDB.user.Balance = Convert.ToInt32(txtBalance.Text);
                    ConnectionDB.db1.HistoryMatches.Add(tempHistory);
                    ConnectionDB.db1.SaveChanges();
                    BetTB.Text = "";
                    MessageBox.Show($"Вы выиграли {bet}");       
                }
                else
                {
                    var bet = int.Parse(BetTB.Text) * 0;
                    var postavil = Convert.ToInt32(BetTB.Text);
                    var result = "Проиграл";
                    var history = ConnectionDB.db1.HistoryMatches.FirstOrDefault(id => id.postavil == postavil && id.result == result);

                    var tempHistory = new HistoryMatches()
                    {
                        postavil = postavil,
                        result = result
                    };
                    ConnectionDB.user.Balance = Convert.ToInt32(txtBalance.Text);
                    ConnectionDB.db1.HistoryMatches.Add(tempHistory);
                    ConnectionDB.db1.SaveChanges();
                    MessageBox.Show($"Вы проиграли");                    
                }
                return;
            }
            RullerImage.RenderTransform = new RotateTransform(spined, 150, 150);
        }        

        private void BetTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = BetTB.Text;
            text = new string(text.Where(c => int.TryParse(c.ToString(), out int temp)).ToArray());
            BetTB.Text = text;
            BetsButtons.IsEnabled = BetTB.Text != "" && txtBalance.Text != "";
        }
        enum WinColor
        {
            Red, Black, Green
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageChanged());
        }

        private void txtPopolnit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CardsPage());
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HistoryMatchPage());
        }
    }
}

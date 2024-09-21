using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        static MainWindow _mainWindow;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            QRCode.Source = GenerateQrCodeBitmapImage("43242");
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {

            if(txtLogin.Text == "admin" && txtPassword.Password == "admin")
            {
                _mainWindow.MainFrame.NavigationService.Navigate(new ListUsers());
            }
            else
            {
                string login = txtLogin.Text;
                string password = txtPassword.Password;

                var user = ConnectionDB.db1.logins.FirstOrDefault(log => log.login == login && log.passwod == password);
           
                if(user == null)
                {
                    var tempUser = new users() { Name = "123" };
                    var tempLogin = new logins()
                    {
                        login = "123",
                        passwod = "123",
                        users = tempUser
                    };
                    ConnectionDB.db1.users.Add(tempUser);
                    ConnectionDB.db1.logins.Add(tempLogin);
                    ConnectionDB.db1.SaveChanges();
                    return;
                }
                ConnectionDB.user = user.users;           
                _mainWindow.MainFrame.NavigationService.Navigate(new PageChanged());
            }
            
       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.NavigationService.Navigate(new SignUpPage());
        }

        private BitmapImage GenerateQrCodeBitmapImage(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q); using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrBitmap.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            BitmapImage bitmapImage = new BitmapImage(); bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad; bitmapImage.StreamSource = ms;
                            bitmapImage.EndInit();
                            return bitmapImage;
                        }
                    }
                }
            }
        }
    }
}

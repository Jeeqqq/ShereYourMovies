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

namespace Elokuvatietue.Ikkunat
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public string userName { get; set; }
        public Login()
        {
            InitializeComponent();
            myIni();
        }

        private void myIni()
        {
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (authenticateUser())
            {
                userName = txtUsername.Text;
                this.Close();
            }
            else
            {
                txtUsername.Foreground = Brushes.Red;
                txtPassword.Foreground = Brushes.Red;
                txtUsername.Text = "DENIED!";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //Cancel
            this.Close();
        }

        private bool authenticateUser() 
        {
            var authService = new AuthenticationService.AuthenticationServiceClient();

            try
            {
                if (authService.Login(txtUsername.Text.ToString(), txtPassword.Password.ToString(), string.Empty, true))
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

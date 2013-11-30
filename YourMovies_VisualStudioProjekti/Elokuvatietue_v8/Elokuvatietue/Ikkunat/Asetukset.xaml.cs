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
namespace Elokuvatietue
{
    /// <summary>
    /// Interaction logic for Asetukset.xaml
    /// </summary>
    public partial class Asetukset : Window
    {
        public Asetukset(string username, ref YourMovies db)
        {
            InitializeComponent();
            myIni(username, ref db);
        }
        public void myIni(string username, ref YourMovies db)
        {
            lstAsetukset.Items.Add("Yleiset asetukset");
            gridAsetukset.Visibility = Visibility.Hidden;
            txtMaxikkuna.Text = Properties.Settings.Default.Maxikkuna.ToString();
            var listat = ElokuvaController.getListNames(username, ref db);
            if (listat != null)
            {
                txtOletusLista.ItemsSource = listat;
            }
           
            if (Properties.Settings.Default.uusiIkkuna == true)
            {
                chkIkkuna.IsChecked = true;
            }
            else
            {
                chkIkkuna.IsChecked = false;
            }
        }
        private void lstAsetukset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAsetukset.SelectedIndex == 0)
            {
                gridAsetukset.Visibility = Visibility.Visible;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chkIkkuna.IsChecked == true)
            {
                Properties.Settings.Default.uusiIkkuna = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.uusiIkkuna = false;
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.Maxikkuna = int.Parse(txtMaxikkuna.Text);
            Properties.Settings.Default.Save();
            this.Close();
        }
        private void txtOletusLista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = @"Listat";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string[] suodatus = { ".xml" };
                string[] tmp = dlg.SafeFileName.Split(suodatus, 2, StringSplitOptions.RemoveEmptyEntries);
                txtOletusLista.Text = tmp[0];
                Properties.Settings.Default.OletusListaNimi = tmp[0];
            }
        }



        private void txtOletusLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtOletusLista.SelectedIndex != -1)
                Properties.Settings.Default.OletusListaNimi = txtOletusLista.Items[txtOletusLista.SelectedIndex].ToString();
        }

        
    }
}
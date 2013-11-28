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
    /// Interaction logic for ListaName.xaml
    /// </summary>
    public partial class ListaName : Window
    {
        public string Nimi { get; set; }
        public ListaName()
        {
            InitializeComponent();
            Nimi = "Nimeton";
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Nimi = txtUusiLista.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Nimi = "Nimeton";
            this.Close();
        }

    }
}

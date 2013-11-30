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
using System.Printing;
using ImdbApi;
using System.IO;

namespace Elokuvatietue
{
    /// <summary>
    /// Interaction logic for Lisatiedot.xaml
    /// </summary>
    public partial class Lisatiedot : Window
    {
        int MovieID;
        Movie leffa;
        public Lisatiedot(Elokuva movie)
        {
            leffa = movie.DbTiedot;
            InitializeComponent();
            myIni(movie);
        }

        public void myIni(Elokuva movie)
        {
            MovieID = movie.MovieID;
            myGrid.DataContext = movie;
            loadImage(movie.DbTiedot);
        }

        private void loadImage(Movie movie)
        {
            try
            {
                cover.Source = new BitmapImage(new Uri(movie.Poster));
            }
            catch (Exception )
            {
                cover.Source = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"noImage.jpg")));
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            btnMuokkaa.DataContext = txtTitle.Text;
            this.Close();
        }

        private void btnMuokkaa_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            txtActors.IsReadOnly = false;
            txtGenre.IsReadOnly = false;
            txtPlot.IsReadOnly = false;
            txtTitle.IsReadOnly = false;
            txtDirector.IsReadOnly = false;
            this.Cursor = Cursors.Arrow;

        }

        private void btnEtsi_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            SearchWindow sWindow = new SearchWindow(leffa);
            sWindow.ShowDialog();
            this.Cursor = Cursors.Arrow;
            if (sWindow.Leffa.Title != null)
            {
                
                btnEtsi.DataContext = sWindow.Leffa;
                loadImage(sWindow.Leffa);
                btnMuokkaa.DataContext = txtTitle.Text;
            } 
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (File.Exists(txtFilepath.Text))
            {
                try
                {
                    System.Diagnostics.Process.Start(txtFilepath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Elokuvaa ei voida toistaa! "+ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Elokuvaa ei voida toistaa! Tiedostopolku on virheellinen!");
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnTulosta_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog().GetValueOrDefault(false))
            {
                printDlg.PrintVisual(this, "Printing Window");
            }
            this.Cursor = Cursors.Arrow;
        }
    }
}

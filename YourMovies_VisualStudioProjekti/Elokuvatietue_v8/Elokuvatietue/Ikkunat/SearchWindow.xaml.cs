using Elokuvatietue;
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

namespace ImdbApi
{
    public partial class SearchWindow : Window
    {
        Result result;
        public Movie Leffa { get; set; }

        public SearchWindow(Movie movie)
        {
            Leffa = movie;
            InitializeComponent();
            myIni();

        }

        private void myIni()
        {
            
            result = new Result();
            SearchResult tyhja = new SearchResult();
            tyhja.Title = "Etsi elokuva";
            tyhja.Year = 0000;
            tyhja.Type = "N/A";
            tyhja.ImdbID = "N/A";
            result.Movies.Add(tyhja);
            paivitaListbox();

        }
        private void paivitaListbox()
        {
            lbResults.ItemsSource = null;
            lbResults.ItemsSource = result.Movies;
        }

        #region Buttons
        private void btEtsi_Click(object sender, RoutedEventArgs e)
        {
            Search.DeSerialisoiSearch(txtSearch.Text,ref result);
            paivitaListbox();

        }

        private void btnHyvaksy_Click(object sender, RoutedEventArgs e)
        {
            if (lbResults.SelectedIndex != -1)
            {
                Movie movie = Leffa;
                Search.DeSerialisoiIdSeach(txbId.Text, ref movie);
                Leffa = movie;
            }
            else
            {
                this.Close();
            }
            
            this.Close();
        }
        #endregion

        private void lbResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lbResults.SelectedIndex;

            if(index != -1)
            currentMovie.DataContext = result.Movies[index];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}

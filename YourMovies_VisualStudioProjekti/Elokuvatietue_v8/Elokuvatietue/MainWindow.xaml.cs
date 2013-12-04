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
using System.Windows.Forms.Integration;
using System.IO;
using System.Printing;
using System.Configuration;
using Elokuvatietue.Ikkunat;
using System.Data.SqlClient;


namespace Elokuvatietue
{
    public partial class MainWindow : Window
    {
        List<Elokuva> etsityt;
        ElokuvaLista movies;
        MusiikkiLista musat;
        List<Lisatiedot> newWTiedot;
        List<MenuItem> esine;
        Asetukset newWAsetukset;
        Login login;
        string valittulista;
        YourMovies db;
        string username;
        RssLista  feedit;
        public MainWindow()
        {
            InitializeComponent();
            myIni();
            listatTyhjat();
        }

        public void myIni() 
        {
            if (username != null)
            {
                Tyokalut.IsEnabled = true;
                Menubaari.IsEnabled = true;

                feedit = new RssLista();
                string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(con);
                db = new YourMovies(myConnection);
                etsityt = new List<Elokuva>();
                movies = new ElokuvaLista();
                newWTiedot = new List<Lisatiedot>();
                esine = new List<MenuItem>();

                //ElokuvaController.initDatabase(ref db);
                //UserController.RegisterUser("teppo", "salasana", ref db);

                var leffat = ElokuvaController.getMoviesByUsers(username, ref db);

                bool exist = false;
                foreach (Elokuva leffa in leffat)
                {
                    MenuItem item = new MenuItem();
                    item.Header = leffa.Lista;
                    item.Click += new RoutedEventHandler(MenuItemClick);
                    foreach (MenuItem existingItem in esine)
                    {
                        if (item.Header.Equals(existingItem.Header) && item.Header.ToString() != "")
                        {
                            exist = true;
                        }
                    }
                    if (!exist)
                    {
                        esine.Add(item);
                        mnOmatelo.Items.Add(item);
                    }
                    exist = false;

                }
                valittulista = Properties.Settings.Default.OletusListaNimi.ToString();
                var oletusLeffat = ElokuvaController.getMoviesByListName(valittulista, username, ref db);
                if (oletusLeffat != null)
                {
                    movies.Movies = oletusLeffat.ToList<Elokuva>();
                }
                else
                {
                    MessageBox.Show("Oletus listaa ei ole enää olemassa!", "Virhe", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                etsityt = movies.Movies;

                paivitaDatagrid(movies.Movies);
                this.SizeToContent = SizeToContent.WidthAndHeight;
            }
            else
            {
                Tyokalut.IsEnabled = false;
                Menubaari.IsEnabled = false;

                while (username == null)
                {
                    login = new Login();
                    if (login.ShowDialog() == false)
                    {
                        if (login.userName != null)
                        {
                            username = login.userName;
                            myIni();
                        }
                        else
                        {
                            Environment.Exit(1);
                        }
                    }
                }
            }
        }
        private void paivitaDatagrid(List<Elokuva> leffat)
        {
            dtGrid.ItemsSource = null;
            dtGrid.ItemsSource = leffat;
            txbCount.Text = ""+leffat.Count;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
        private void paivitaDatagrid(List<Musiikki> mLista)
        {
            dtGrid.ItemsSource = null;
            dtGrid.ItemsSource = mLista;
            txbCount.Text = "" + mLista.Count;
        }
        public void listatTyhjat() 
        {
            if (mnOmatelo.Items.Count <= 0)
            {
                mnOmatelo.IsEnabled = false;
                PoistaLista.IsEnabled = false;
            }
            else
            {
                PoistaLista.IsEnabled = true;
                mnOmatelo.IsEnabled = true;
            }

            if (mnOmatmus.Items.Count <= 0)
            {
                mnOmatmus.IsEnabled = false;
            }
            else
            {
                mnOmatmus.IsEnabled = true;
            }
        }

        
        #region Listat
        private void ElokuvatUusiLista_Click_1(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //ilmoitus käyttäjälle
            Echo(string.Format("Valitse kansio josta haetaan elokuvia uutta listaa varten!"));
            dtGrid.ItemsSource = null;
            movies.Movies.Clear();

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result.ToString() == "OK")
            {

                //ilmoitus käyttäjälle
                Echo(string.Format("Anna listalle nimi!"));
                ListaName uusiNimi = new ListaName();
                uusiNimi.ShowDialog();
                this.Cursor = Cursors.Wait;
                Echo(string.Format("Etsitään videotiedostoja!"));
                esine.Add(new MenuItem());
                esine[esine.Count - 1].Header = uusiNimi.Nimi;
                esine[esine.Count - 1].Click += new RoutedEventHandler(MenuItemClick);
                mnOmatelo.Items.Add(esine[esine.Count - 1]);
                valittulista = uusiNimi.Nimi;

                etsiElokuvia(dialog.SelectedPath);
                //ilmoitus käyttäjälle
                Echo(string.Format("Elokuvat haettu kansiosta ja lisätty uuteen listaan nimeltä " + uusiNimi.Nimi));
            }
            this.Cursor = Cursors.Arrow;
        }
        //lisää uuden elokuvan listaan
        private void etsiElokuvia(string filepath)
        {
            DirectoryInfo di = new DirectoryInfo(filepath);

            IEnumerable<FileInfo> files = GetFilesByExtensions(di, ".mkv", ".avi", ".wmv", ".mp4");
            foreach (FileInfo file in files)
            {

                Elokuva eKuva = new Elokuva(file.Name, file.FullName);
                eKuva.Lista = valittulista;
                eKuva.UserName = username;
                feedit.feedit.Add(RssController.AddFeed(username, eKuva.Nimi, "new", ref db));
                db.Elokuva.InsertOnSubmit(eKuva);
                movies.Movies.Add(eKuva);

            }

            listatTyhjat();
            paivitaDatagrid(movies.Movies);
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
        private void ElokuvatPoistaLista_Click_1(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                MessageBoxResult rs;
                rs = MessageBox.Show("Haluatko varmasti poistaa listan " + valittulista + "?", "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rs == MessageBoxResult.No)
                {
                    return;
                }

                foreach (MenuItem item in esine)
                {
                    if (valittulista == item.Header.ToString())
                    {
                        dtGrid.ItemsSource = null;
                        mnOmatelo.Items.Remove(item);
                        
                        foreach (Elokuva l in movies.Movies)
                        {
                            db.Movie.DeleteOnSubmit(l.DbTiedot);
                        }
                        db.Elokuva.DeleteAllOnSubmit(movies.Movies);
                        db.SubmitChanges();
                    }
                }
                listatTyhjat();
                this.SizeToContent = SizeToContent.WidthAndHeight;
                //ilmoitus käyttäjälle
                Echo(string.Format("Valittu lista poistettiin onnistuneesti!"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Cursor = Cursors.Arrow;
        }
        private void ElokuvatTallennaLista_Click_1(object sender, RoutedEventArgs e)
        {
            //Serialisointi.SerialisoiXml(@"Listat\" + valittulista + ".xml", movies);

            RssController.SaveFeeds(ref feedit,ref db);
            
            db.SubmitChanges();
            
            MessageBox.Show("Tallentaminen onnistui", "Onnistu!", MessageBoxButton.OK, MessageBoxImage.Information);
            //ilmoitus käyttäjälle
            Echo(string.Format("Elokuvalista tallennettu onnistuneesti!"));
        }
        #endregion
        #region TiedostonHaut
        private void ElokuvatUusiKansio_Click_1(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //ilmoitus käyttäjälle
            Echo(string.Format("Valitse uusi kansio, josta haetaan elokuvia listaan!"));
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            

            if (result.ToString() == "OK")
            {
                etsiElokuvia(dialog.SelectedPath);
                //ilmoitus käyttäjälle
                Echo(string.Format("Elokuvat haettu kansiosta ja lisätty nykyiseen listaan!"));
            }
            this.Cursor = Cursors.Arrow;
        }
        public  IEnumerable<FileInfo> GetFilesByExtensions(DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.EnumerateFiles("*",SearchOption.AllDirectories);
            return files.Where(f => extensions.Contains(f.Extension));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //musiikkitiedostonejen haku
            //ilmoitus käyttäjälle
            Echo(string.Format("Valitse uusi kansio, josta haetaan Musiikkia listaan!"));
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();


            if (result.ToString() == "OK")
            {
                DirectoryInfo di = new DirectoryInfo(dialog.SelectedPath);

                IEnumerable<FileInfo> files = GetFilesByExtensions(di, ".mp3", ".waw", ".flac", ".midi",".wma");
                //di.GetFiles("*", SearchOption.AllDirectories);
                foreach (FileInfo file in files)
                {

                    musat.Musa.Add(new Musiikki(file.Name, file.FullName));

                }
                paivitaDatagrid(musat.Musa);
                //ilmoitus käyttäjälle
                Echo(string.Format("Muusikit haettu kansiosta ja lisätty nykyiseen listaan!"));
            }


        }
        #endregion

        #region Asetukset
        private void MenuExit_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        private void MenuAsetukset_Click_1(object sender, RoutedEventArgs e)
        {
            newWAsetukset = new Asetukset(username, ref db);

            foreach (Lisatiedot item in newWTiedot)
            {
                item.Close();
            }

            newWTiedot.Clear();

            newWAsetukset.ShowDialog();
            movies.Movies.Clear();
           
            valittulista = Properties.Settings.Default.OletusListaNimi.ToString();
            var OletusLista = ElokuvaController.getMoviesByListName(valittulista, username, ref db);
            if (OletusLista != null)
            {
                movies.Movies = OletusLista.ToList<Elokuva>();
            }
            else
            {
                MessageBox.Show("Oletus listaa ei ole enää olemassa!", "Virhe", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            paivitaDatagrid(movies.Movies);
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, @"Elokuvaohje.chm");
            //ilmoitus käyttäjälle
            Echo(string.Format("Näytetään sovelluksen Help-tiedosto"));
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog().GetValueOrDefault(false))
            {
            printDlg.PrintVisual(dtGrid, "Printing Grid");
            }

            //ilmoitus käyttäjälle
            Echo(string.Format("Tulostetaan nykyinen elokuvalista!"));
        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            valittulista = item.Header.ToString();
            movies.Movies = ElokuvaController.getMoviesByListName(valittulista, username, ref db).ToList<Elokuva>();

            paivitaDatagrid(movies.Movies);
            dtGrid.Columns[1].Width = new DataGridLength(0, DataGridLengthUnitType.SizeToCells);
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void mnAbout_Click_1(object sender, RoutedEventArgs e)
        {
            AboutBox1 abouttia = new AboutBox1();
            abouttia.ShowDialog();
        }

        #endregion

        private void btnOpenInfo_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
                
            if (Properties.Settings.Default.uusiIkkuna == true)
            {
                if (Properties.Settings.Default.Maxikkuna <= newWTiedot.Count)
                {
                    int j = newWTiedot.Count - Properties.Settings.Default.Maxikkuna + 1;

                    for (int i = 0; i < j; i++)
                    {
                        newWTiedot[i].Close();
                        newWTiedot.RemoveAt(i);
                    }

                }
                newWTiedot.Add(new Lisatiedot(movies.Movies[dtGrid.SelectedIndex]));//movies[dtGrid.SelectedIndex]
                newWTiedot[newWTiedot.Count - 1].Show();
                //ilmoitus käyttäjälle
                Echo(string.Format("Näytetään lisätietoja elokuvasta " + movies.Movies[dtGrid.SelectedIndex].Nimi));
            }
            else
            {
                if (newWTiedot.Count >= 1)
                {
                    foreach (Lisatiedot item in newWTiedot)
                    {
                        item.Close();
                    }
                }
                if (dtGrid.SelectedIndex != -1)
                {
                    newWTiedot.Add(new Lisatiedot(movies.Movies[dtGrid.SelectedIndex]));//movies[dtGrid.SelectedIndex]
                    newWTiedot[newWTiedot.Count - 1].Show();

                    //ilmoitus käyttäjälle
                    Echo(string.Format("Näytetään lisätietoja elokuvasta " + movies.Movies[dtGrid.SelectedIndex].Nimi));
                }
            }
            this.Cursor = Cursors.Arrow;
        }
        #region työkalut
        private void txtEtsi_KeyUp(object sender, KeyEventArgs e)
        {
            string hakuSana = txtEtsi.Text;
            etsityt = null;
            etsityt = movies.Movies.FindAll(
                                             delegate(Elokuva elo)
                                              {
                                                return elo.Nimi.Contains(hakuSana);

                                                });

            if(etsityt != null)
            paivitaDatagrid(etsityt);
            //ilmoitus käyttäjälle
            Echo(string.Format("Etsitään elokuvia..."));

        }

        private void RadioButtonTogle_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(rdKylla))
            {
                // etsitään Elokuvat jotka on jo nähty
                List<Elokuva> filteroidyt = movies.Movies.FindAll(
                                                                  delegate(Elokuva elo)
                                                                  {
                                                                      return elo.Watched == false;
                                                                  }
                                                                  );

                paivitaDatagrid(filteroidyt);
                //ilmoitus käyttäjälle
                Echo(string.Format("Näytetään vain elokuvat, jotka olet jo katsonut"));
            }
            else
            {
                paivitaDatagrid(movies.Movies);
                //ilmoitus käyttäjälle
                Echo(string.Format("Näytetään kaikki elokuvat"));
            }


        }
       
        private void btnPoista_Click(object sender, RoutedEventArgs e)
        {
          int index=  dtGrid.SelectedIndex;
          if (index > -1 && index != movies.Movies.Count)
          {
              db.Movie.DeleteOnSubmit(movies.Movies.ElementAt(index).DbTiedot);
              db.Elokuva.DeleteOnSubmit(movies.Movies.ElementAt(index));
              movies.Movies.RemoveAt(index);
              paivitaDatagrid(movies.Movies);
              //ilmoitus käyttäjälle
              Echo(string.Format("Elokuva poistettu onnistuneesti!"));
          }
        }

        private void Echo(string msg)
        {
            stbViestit.Text = msg;
        }


        private void tuoLista_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            //ilmoitus käyttäjälle
            Echo(string.Format("Valitse tuotava xml tiedosto!"));
            dtGrid.ItemsSource = null;
            movies.Movies.Clear();

            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result.ToString() == "OK")
            {

                string filepath = dialog.FileName;
                valittulista = dialog.SafeFileName;
                this.Cursor = Cursors.Wait;

                try{
                Serialisointi.DeSerialisoiXml(filepath, ref movies,valittulista,username);
                esine.Add(new MenuItem());
                esine[esine.Count - 1].Header = valittulista;
                esine[esine.Count - 1].Click += new RoutedEventHandler(MenuItemClick);
                mnOmatelo.Items.Add(esine[esine.Count - 1]);
                db.Elokuva.InsertAllOnSubmit(movies.Movies);
                paivitaDatagrid(movies.Movies);
                listatTyhjat();
                //ilmoitus käyttäjälle
                Echo(string.Format("Lista lisätty onnistuneesti"));
                }
                catch(Exception ex)
                {
                    //ilmoitus käyttäjälle
                    Echo(string.Format("Tapahtui virhe"));
                }
               
                

               
                
            }
            this.Cursor = Cursors.Arrow;
        }
        #endregion
    }
}

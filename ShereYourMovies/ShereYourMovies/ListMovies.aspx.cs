using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ShereYourMovies.Classes;
using System.Collections;
using ImdbApi;
using System.Configuration;
using System.Data.SqlClient;

namespace ShereYourMovies
{
    /*Listview1 listaa kaikki elokuvat
     * listView2 listaa yhden valitun elokuvan
     * ListVie3 listaa etsinnän tulokset
     * 
     * 
     */
    public partial class ListMovies : System.Web.UI.Page
    {
        List<Elokuva> elokuvalista = new List<Elokuva>();
        Result searchResults = new Result();
        Elokuva _leffa;
        List<string> usernames = new List<string>();
        Elokuva leffa
        { 
            get { return _leffa; } 
            set 
            {
                _leffa = value;
            } 
        }
        YourMovies db;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                myIni();
            }
            else 
            {
                elokuvalista = (List<Elokuva>)Session["elokuvalista"];
                db = (YourMovies)Session["db"];
                leffa = (Elokuva)Session["selectedMovie"];
                searchResults = (Result)Session["searchResults"];
               
            }
            //haetaan kaikkien käyttäjien nimet
            usernames = ElokuvaController.getAllUsers(ref db);
          
        }
        /*
         * initoitdaan muuuttuja ja tallenetaan ne sessioihin
         * 
         */
        private void myIni()
        {

            db = (YourMovies)Session["db"];
            String username = Context.User.Identity.Name;

            IQueryable<Elokuva> elokuva = ElokuvaController.getMoviesByUsers(username, ref db);

            
            List<Elokuva> elokuvalista = elokuva.ToList();
            leffa = null;
            Session["selectedMovie"] = leffa;

            
            toListView(elokuvalista);
            Session["elokuvalista"] = elokuvalista;
            otsikko.InnerText = "Omat Elokuvat";
        }
        #region databindaukset
        protected void bindSearchResult(Result r) 
        {      
            ListView3.DataSource = r.Movies;
            ListView3.DataBind();
        }
        protected void toListView(List<Elokuva> elokuva)
        {
            ListView1.DataSource = elokuva;
            ListView1.DataBind();
        }
        protected void toListView2()
        {
            toListView(null);
            ListView2.DataBind();
            
        }
        #endregion
        #region lisview1 toiminnalisuudet
        /*
         * Yhden elokuvan valitseminen ja sen näyttäminen
         * 
         */
        
        protected void openMovieInfo_Command(object sender, CommandEventArgs e)
        {
            int id =Int32.Parse(e.CommandArgument.ToString());
            List<Elokuva> elokuva=elokuvalista.FindAll(eKuva => eKuva.ElokuvaID == id);
            leffa = elokuva.ElementAt(0);
            Session["selectedMovie"]=leffa;
            toListView2();
            ListView3.DataSource = null;
            ListView3.DataBind();
            DataPagerMovies.Visible = false;
           

        }


        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //set current page startindex, max rows and rebind to false
            DataPagerMovies.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            
            //rebind List View
            toListView(elokuvalista);
        }

        #endregion
        #region listview2 toiminnalisuudet
        /*
         * Kaikki komennot kun vain yhtä ikkuunaa näytetaan
         * 
         */
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            toListView2();
            switch (e.CommandName)
            {
                case "Edit":
                    ListView2.EditIndex = 0;
                    break;
                case "Update":
                    break;
                case "Back":
                    leffa = null;
                    Session["selectedMovie"] = leffa;
                    toListView(elokuvalista);
                    DataPagerMovies.Visible = true;
                    ListView2.DataSource = null;
                    ListView2.DataBind();
                    ListView3.DataSource = null;
                    ListView3.DataBind();
                    break;
                case "Etsi":
                    searchResults = new Result();
                    SearchResult s = new SearchResult();
                    s.Title = "Etsi elokuvia nimen perusteella";
                    s.Year = 1;
                    s.Type = "Haku";
                    searchResults.Movies.Add(s);
                    bindSearchResult(searchResults);
                    break;
                case "Delete":
                    DataPagerMovies.Visible = true;
                    leffa = null;
                    break;
            }
        }

        protected void ListView2_ItemEditing(object sender, ListViewEditEventArgs e)
        {}
        /*
       * Yhden elokuvan päivittäminen
       * 
       */
        protected void ListView2_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            
            
            foreach (DictionaryEntry de in e.NewValues)
            {
                if (de.Value != null)
                {
                    if (!de.Value.Equals(e.OldValues[de.Key]))
                    {
                        if (de.Key.ToString().Contains("DbTiedot"))
                        {
                            string propertyName = de.Key.ToString().Split('.')[1];
                            leffa.DbTiedot.Update(propertyName, de.Value.ToString());
                            if (propertyName.Equals("Title"))
                            {
                                leffa.Update("Nimi", de.Value.ToString());
                            }
                        }
                        else
                            leffa.Update(de.Key.ToString(), de.Value.ToString());

                        if (de.Key.ToString().Equals("Watched") && bool.Parse(de.Value.ToString()) == true)
                        {
                            RssController.AddFeedAndSave(leffa.UserName, leffa.Nimi, "watch", ref db);
                        }
                    }
                    
                }

            }
            if (leffa.DbTiedot.Poster.Equals("~/Images/No-Photo-Available.jpg"))
            {
                leffa.DbTiedot.Poster = null;
            }

        }

       

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable ListView2_GetData()
        {
            db = (YourMovies)Session["db"];
            leffa = (Elokuva)Session["selectedMovie"];
           var leffat=from Elokuva in db.Elokuva
               where Elokuva.ElokuvaID == leffa.ElokuvaID
               select Elokuva;
           if (leffa == null)
               return null;
           else
            return leffat;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListView2_DeleteItem(int ElokuvaID)
        {
            Elokuva leffaToDelete =(Elokuva)( from Elokuva in db.Elokuva where Elokuva.ElokuvaID == ElokuvaID select Elokuva).First();
            ElokuvaController.deleteElokuva(leffaToDelete, ref db);

            myIni();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListView2_UpdateItem(int ElokuvaID)
        {
            Label msg = (Label)ListView2.FindControl("infoMsg");
            msg.Visible = true;
            if (!ElokuvaController.updateElokuva(leffa, ref db))
            {
                msg.Text = "Talentaminen epäonnistui";
                lblInfo.Text = "Tiedot muokattu onnistuneesti!";
            }
            else
            {
                msg.Visible = false;
                msg.Text = "";
            }
            ListView2.DataBind();
        }
        //ei anneta muiden käyttäjien muokata muidien leffoja
        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                Panel toolbaar = (Panel)e.Item.FindControl("toolbarpanel");
                toolbaar.Visible = true;
                if (Context.User.Identity.Name != leffa.UserName)
                {

                    toolbaar.Visible = false;
                }
            }
        }
        #endregion
        #region listview3 toiminnalisuudet
        /*
       * etsitään elokuvia internetistä
       * 
       */
        protected void btnSearch_Command(object sender, CommandEventArgs e)
        {
            TextBox txt = (TextBox)ListView3.FindControl("Search");
            Label label = (Label)ListView3.FindControl("searchInfo");
            label.Text = "";
            string nimi = txt.Text;
            Search.DeSerialisoiSearch(nimi, ref searchResults);
            toListView2();
            Session["searchResults"] = searchResults;
            if (searchResults.Movies.Count == 0)
            {
                
              
                label.Text = "Elokuvia ei löytynyt annetulla parametrillä";
            }
            else 
            bindSearchResult(searchResults);
        }

        protected void btnSelect_Command(object sender, CommandEventArgs e)
        {
            toListView2();
        }


        protected void ListView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ListView3.SelectedIndex>=0)
            {
                if (searchResults != null)
                {
                    if (searchResults.Movies.Count != 1 && !searchResults.Movies.ElementAt(0).Type.Equals("Haku"))
                    {
                        SearchResult sResult = searchResults.Movies.ElementAt(ListView3.SelectedIndex);

                        Movie movie = leffa.DbTiedot;
                        Search.DeSerialisoiIdSeach(sResult.ImdbID, ref movie);
                        leffa.Nimi = movie.Title;
                        ElokuvaController.updateElokuva(leffa, ref db);
                        toListView2();
                    }
                }
              ListView3.SelectedIndex = -1;
            }
        }

        protected void ListView3_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            if(searchResults != null)
            bindSearchResult(searchResults);
        }
        #endregion
        #region yleiset toiminnalisuudet
        /*
         * Etsitään toinen käyttäjä ja listataan käyttäjän elokuvat
         * 
         */

        protected void searchUser_Click(object sender, EventArgs e)
        {
            string username = txtFindUsers.Text;

            var isUser = usernames.Find(asd=>asd == username);

            if (!string.IsNullOrEmpty(isUser))
            {

                otsikko.InnerText = "Käyttäjän " + username + " Elokuvat";

                elokuvalista = (ElokuvaController.getMoviesByUsers(username, ref db)).ToList();
                Session["elokuvalista"] = elokuvalista;
                toListView(elokuvalista);
                Session["selectedMovie"] = null;
                ListView2.DataBind();
                ListView3.DataSource = null;
                ListView3.DataBind();
            }
            else
            {
                otsikko.InnerText = "Käyttäjää " + username + " ei löytynyt tai käyttäjälle ei ole elokuvia";
                Session["elokuvalista"] = null;
                toListView(null);
                Session["selectedMovie"]= null;
                ListView2.DataBind();
                ListView3.DataSource = null;
                ListView3.DataBind();
            }
        }
        
        /*
         * peukutetaan leffaaa
         * 
         */
        protected void ImageButton2_Command(object sender, CommandEventArgs e)
        {
           
            int id = Int32.Parse(e.CommandArgument.ToString());
            List<Elokuva> elokuvat = elokuvalista.FindAll(eKuva => eKuva.ElokuvaID == id);
            Elokuva elokuva = elokuvat.ElementAt(0);
            RssController.AddFeedAndSave(Context.User.Identity.Name, elokuva.Nimi, "like", ref db);

            lblInfo.Text = "Tykkäsit käyttäjän "+elokuva.UserName+" elokuvasta "+elokuva.Nimi;
        }

        #endregion
    }
}
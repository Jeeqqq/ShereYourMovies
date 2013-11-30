using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ShereYourMovies.Classes;

namespace ShereYourMovies
{
    public partial class ListMovies : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            myIni();
        }

        private void myIni()
        {
            YourMovies db = (YourMovies)Session["db"];
            String username = Context.User.Identity.Name;

            IQueryable<Elokuva> elokuva = ElokuvaController.getMoviesByUsers(username, ref db);
            lblDebug.InnerText = username;
            husername.InnerText = username;

            grdElokuvat.DataSource = elokuva;
            grdElokuvat.DataBind();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using ShereYourMovies.Classes;

namespace ShereYourMovies
{
    public partial class RSSFeeds : System.Web.UI.Page
    {
        public RssLista feed = new RssLista();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                feed = RssController.LoadFeeds();
                ViewState["feed"] = feed;
            }
            else
            {
                feed = (RssLista)ViewState["feed"];
            }

            if (feed != null)
            {
                bindFeeds();
            }
        }
        
        // Bindataan feedit
        private void bindFeeds()
        {
            DataList1.DataSource = feed.feedit;
            DataList1.DataBind();
        }

        // TEMP: Testi nappula
        protected void Button1_Click(object sender, EventArgs e)
        {
            RssController.AddFeed("Vepsä", "Hujasen Hapatukset", "like");
            RssController.AddFeed("Hujanen", "Hapatuksen Multihuipentuma", "watch");
            RssController.AddFeed("Narsuman", "FreeNest III", "vaara_type_testiksi");

            feed = RssController.LoadFeeds();
            bindFeeds();
        }
    }
}
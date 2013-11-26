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
        public RssLista feed;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RssLista feed1 = new RssLista();
                string uri = Server.MapPath(@".\App_Data\RSSFeeds1.xml");

                Serialisointi.DeSerialisoiXml(uri, ref feed1);
                feed = feed1;
                ViewState["feed"] = feed;
            }
            else
            {
                feed = (RssLista)ViewState["feed"];
            }

            updateFeeds();
        }
        
        private void updateFeeds()
        {
            DataList1.DataSource = feed.feedit;
            DataList1.DataBind();
        }
        
        // TEMP
        protected void AddFeed(string author, string title)
        {
            try 
            {
                string uri = Server.MapPath(@".\App_Data\RSSFeeds1.xml");
                Rss r = new Rss();
                r.author = author;
                r.Title = title;
                r.pubDate = DateTime.Now.ToString();
                feed.feedit.Add(r);
                Serialisointi.SerialisoiXml(uri, feed);
                updateFeeds();
            } 
            catch 
            {
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AddFeed("Vepsä", "Hapatuksen multihuipentuma");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShereYourMovies.Classes;

namespace ShereYourMovies
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable DataList1_GetData()
        {
            YourMovies db = (YourMovies)Session["db"];
            return (from Rss in db.Rss select Rss).OrderByDescending(rss => rss.pubDate);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            YourMovies db = (YourMovies)Session["db"];
            RssController.AddFeed("Vepsä", "Hujasen Hapatukset", "like", ref db);
            RssController.AddFeed("Hujanen", "Hapatuksen Multihuipentuma", "watch", ref db);
            RssController.AddFeed("Narsuman", "FreeNest III", "vaara_type_testiksi", ref db);
            DataList1.DataBind();
        }
    }
}
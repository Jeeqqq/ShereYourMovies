using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elokuvatietue;
namespace ShereYourMovies
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*SqlConnection myConnection = new SqlConnection("user id=G9206;" +
                                       "password=9OctOUAxyYiVz5CtCeoI8zBfsLMkUyK0;server=mysql.labranet.jamk.fi;" +
                                       "Trusted_Connection=yes;" +
                                       "database=G9206; " +
                                       "connection timeout=30");
            */
            YourMovies db = new YourMovies("d:\\G9206\\testi.sdf");
            db.CreateDatabase();

            
        }
    }
}
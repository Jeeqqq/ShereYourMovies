
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShereYourMovies.Classes
{
  
    public class ElokuvaController
    {
        public YourMovies db;

        public ElokuvaController()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=(LocalDb)\v11.0;Initial Catalog=shareyourmovies;Integrated Security=True";

            db = new YourMovies(myConnection);
         /*   if (!db.DatabaseExists())
            {
                db.DeleteDatabase();
                db.CreateDatabase();
            }*/



            
           
           
           
        }

    }
}
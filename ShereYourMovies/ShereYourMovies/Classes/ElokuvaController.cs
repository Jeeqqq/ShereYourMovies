
using System;
using System.Collections.Generic;
using System.Data.Linq;
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
            /*if (db.DatabaseExists())
            {
                db.DeleteDatabase();
                db.CreateDatabase();
            }*/
            //db.CreateDatabase();
          string msg=  UserController.RegisterUser("Teppo", "salasana", ref db);
          Elokuva leffa = new Elokuva();
          leffa.Nimi = "Vepsän Leffa";
          leffa.UserID = 1;

          db.Elokuva.InsertOnSubmit(leffa);
          db.SubmitChanges();
           
        }

        public static IQueryable<Elokuva> getMoviesByUsers(String username, ref YourMovies db) 
        {
            var m = from Elokuva in db.Elokuva
                    where Elokuva.User.UserName == username
                    select Elokuva;

            return m;
        }



    }
}

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
            /*
             * 
             * Poista kommentit päivittääksesi databasen!
             * 
             * if (db.DatabaseExists())
            {
                db.DeleteDatabase();
                db.CreateDatabase();
            }
          string msg=  UserController.RegisterUser("Teppo", "salasana", ref db);
          Elokuva leffa = new Elokuva();
          leffa.Nimi = "Vepsän Leffa";
          leffa.UserID = 1;

          db.Elokuva.InsertOnSubmit(leffa);
          db.SubmitChanges();*/

        }

        public static IQueryable<Elokuva> getMoviesByUsers(String username, ref YourMovies db) 
        {
            var m = from Elokuva in db.Elokuva
                    where Elokuva.User.UserName == username
                    select Elokuva;

            return m;
        }

        #region Querys
        public static bool insertElokuva(Elokuva elokuva, ref YourMovies db)
        {
            try
            {
                db.Elokuva.InsertOnSubmit(elokuva);
                db.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
                return false;
            }
            
        }

        public static bool updateElokuva(Elokuva elokuva, ref YourMovies db)
        {
            try
            {
                var q = (from Elokuva in db.Elokuva
                        where Elokuva.ID == elokuva.ID
                        select Elokuva).First();

                q = elokuva;

                db.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
                return false;
            }

        }


        public static bool deleteElokuva(Elokuva elokuva, ref YourMovies db)
        {
            try
            {
                db.Elokuva.DeleteOnSubmit(elokuva);
                db.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
                return false;
            }
        }
        #endregion

        public static List<Elokuva> sortList(List<Elokuva> elokuvat, string GridViewSortExpression, string SortDirection) 
        {
            if (elokuvat != null)
            {
                if (GridViewSortExpression != string.Empty)
                {
                    if (SortDirection == "ASC")
                    {
                        elokuvat = elokuvat.OrderBy
                            (a => a.GetType().GetProperty(GridViewSortExpression)
                                .GetValue(a, null)).ToList();
                    }
                    else
                    {
                        elokuvat = elokuvat.OrderByDescending
                            (a => a.GetType().GetProperty(GridViewSortExpression)
                                .GetValue(a, null)).ToList();
                    }
                }
                return elokuvat;
            }
            else
            {
                return elokuvat;
            }
        }

    }
}
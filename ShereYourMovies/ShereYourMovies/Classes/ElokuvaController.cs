
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;

namespace ShereYourMovies.Classes
{
  
    public class ElokuvaController
    {


        public static void initDatabase(ref YourMovies db)
        {

	        if (db.DatabaseExists())
            {
                db.DeleteDatabase();
                db.CreateDatabase();
            }
            else
            {
                db.CreateDatabase();
            }
            


            //string msg=  UserController.RegisterUser("Teppo", "salasana", ref db);
            db.SubmitChanges();

        }

        public static IQueryable<Elokuva> getMoviesByUsers(string username, ref YourMovies db)
        {
            var m = from Elokuva in db.Elokuva
                    where Elokuva.UserName == username
                    select Elokuva;

            return m;
        }
        public static List<string> getAllUsers(ref YourMovies db)
        {
            var m = from Elokuva in db.Elokuva
                    select Elokuva.UserName;
            List<string> lista = removeDublicates(m.ToList<string>());
            return lista;
        }
        public static List<string> removeDublicates(List<string> m)
        {
            bool inList = false;
            List<string> lista = new List<string>();
            foreach (string s in m)
            {
                foreach (string str in lista)
                {
                    if (s.Equals(str))
                    {
                        inList = true;
                    }
                }
                if (!inList)
                {
                    lista.Add(s);
                    inList = false;
                }
            }
            return lista;
        }
        public static List<string> getUsernamesLike(string username, ref YourMovies db) 
        {
            var m = from Elokuva in db.Elokuva
                    where Elokuva.UserName.StartsWith(username)
                    select Elokuva.UserName;
            List<string> lista = removeDublicates(m.ToList<string>());
            return lista;
        }

        public static IQueryable<Elokuva> getMoviesByListName(string listName, string username, ref YourMovies db)
        {
            var m = from Elokuva in db.Elokuva
                    where Elokuva.Lista == listName && Elokuva.UserName == username
                    select Elokuva;
            if (m.Count() == 0)
                return null;
            else
                return m;
        }

        public static List<string> getListNames(string username, ref YourMovies db)
        {
            var m = from Elokuva in db.Elokuva
                    where Elokuva.UserName == username
                    select Elokuva.Lista;

            if (m.Count() == 0)
                return null;

            else
            {
                List<string> listat = new List<string>();
                bool exist = false;
                foreach (string lista in m)
                {
                    foreach (string str in listat)
                    {
                        if (lista.Equals(str))
                            exist = true;
                    }
                    if (!exist)
                        listat.Add(lista);

                    exist = false;
                }
                return listat;
            }
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
                db.Movie.DeleteOnSubmit(elokuva.DbTiedot);
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
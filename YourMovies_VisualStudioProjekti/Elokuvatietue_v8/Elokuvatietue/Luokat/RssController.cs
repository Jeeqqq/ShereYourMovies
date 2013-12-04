using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elokuvatietue
{
    public static class RssController
    {


        // Haetaan kaikki feedit ja palautetaan ne
        public static List<Rss> LoadFeeds(ref YourMovies db)
        {
            List<Rss> lista = (from Rss in db.Rss select Rss).ToList<Rss>();
            return lista;
        }

        // Lisätään uusi feedi ja tallennetaan
        public static Rss AddFeed(string author, string movie, string type, ref YourMovies db)
        {


            Rss r = new Rss();
            r.author = author;
            r.pubDate = DateTime.Now.ToString();

            // Feedin title tyypin mukaan
            if (type.Equals("like"))
            {
                r.Title = author + " tykkäsi elokuvasta " + movie + ".";
            }
            else if (type.Equals("new"))
            {
                r.Title = author + " lisäsi elokuvan " + movie + ".";
            }
            else if (type.Equals("watch"))
            {
                r.Title = author + " katsoi elokuvan " + movie + ".";
            }
            else
            {
                r.Title = author + " teki nyt jotain kovin hassua!";
            }

            // Tallennetaan
            return r;


        }
        public static void SaveFeeds(ref RssLista feeds, ref YourMovies db)
        {
            db.Rss.InsertAllOnSubmit(feeds.feedit);
            db.SubmitChanges();
            feeds.feedit.Clear();
        }
    }

}
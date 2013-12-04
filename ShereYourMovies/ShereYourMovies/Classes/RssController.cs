using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShereYourMovies.Classes 
{
    public static class RssController
    {
        

        // Haetaan kaikki feedit ja palautetaan ne
        public static List<Rss> LoadFeeds(ref YourMovies db) 
        {
            List<Rss> lista = (from Rss in db.Rss select Rss).ToList<Rss>();
            return lista;
        }
        public static void AddFeedAndSave(string author, string movie, string type, ref YourMovies db)
        {
          Rss r=  AddFeed(author, movie, type, ref db);
          RssLista rssLista = new RssLista();
          rssLista.feedit.Add(r);
          SaveFeeds(rssLista, ref db);

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
                    r.Title = author + " peukutti elokuvaa " + movie + ".";
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
        public static void SaveFeeds(RssLista feeds,ref YourMovies db)
        {
            db.Rss.InsertAllOnSubmit(feeds.feedit);
            db.SubmitChanges();
        }
    }

}
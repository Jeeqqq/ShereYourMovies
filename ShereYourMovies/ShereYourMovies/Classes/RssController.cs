using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShereYourMovies.Classes 
{
    public static class RssController
    {
        private static RssLista feed;

        // Haetaan kaikki feedit ja palautetaan ne
        public static RssLista LoadFeeds() 
        {
            feed = new RssLista();
            string uri = HttpContext.Current.Server.MapPath(@".\App_Data\RSSFeeds1.xml");
            Serialisointi.DeSerialisoiXml(uri, ref feed);
            return feed;
        }
        
        // Lisätään uusi feedi ja tallennetaan
        public static void AddFeed(string author, string movie, string type)
        {
            try 
            {
                string uri = HttpContext.Current.Server.MapPath(@".\App_Data\RSSFeeds1.xml");
                Rss r = new Rss();
                r.author = author;
                r.pubDate = DateTime.Now.ToString();

                // Feedin title tyypin mukaan
                if (type.Equals("like"))
                {
                    r.Title = author + " tykkäsi elokuvasta " + movie + ".";
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
                feed.feedit.Add(r);
                Serialisointi.SerialisoiXml(uri, feed);
            } 
            catch 
            {
            }
        }
    }

}
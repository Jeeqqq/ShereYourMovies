using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ShereYourMovies.Classes
{
    [Serializable(), XmlRoot("rss")]
    public class RssLista
    {
        [XmlElement("item")]
        public List<Rss> feedit;

        
        public RssLista()
        {
            feedit = new List<Rss>();
        }
    }


    [Serializable()]
    public class Rss
    {
        [XmlElement("title")]
        public string Title { get; set;}
        [XmlElement("pubDate")]
        public string pubDate { get; set; }
        [XmlElement("author")]
        public string author { get; set; }

        public Rss()
        {
        }
    }
}
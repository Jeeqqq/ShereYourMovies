using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
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


    [Table(Name = "Rss")]
    public class Rss
    {
        private int _rssID;
        [Column(DbType = "BigInt IDENTITY NOT NULL", IsPrimaryKey = true, IsDbGenerated = true)]
        public int RssID
        {
            set { _rssID = value; }
            get { return _rssID; }
        }
        [Column]
        public string Title { get; set;}
        [Column]
        public string pubDate { get; set; }
        [Column]
        public string author { get; set; }

        public Rss()
        {
        }
    }
}
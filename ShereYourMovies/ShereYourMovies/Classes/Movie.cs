using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Elokuvatietue
{
    [Serializable(), XmlRoot("root")]
    public class Root
    {
        public Root()
        {
            leffa = new Movie();
        }
        [XmlElement("movie")]
        public Movie leffa { get; set; }
    }
   [Serializable()]
    public class Movie
    {
         [XmlAttribute ("title")]
        public string Title{get;set;}
         [XmlAttribute("year")]
        public string Year{get;set;}
         [XmlAttribute("rated")]
        public string Rated{get;set;}
         [XmlAttribute("released")]
        public string Released{get;set;}
         [XmlAttribute("runtime")]
        public string Runtime{get;set;}
         [XmlAttribute("genre")]
        public string Genre{get;set;}
         [XmlAttribute("director")]
        public string Director{get;set;}
         [XmlAttribute("Writer")]
        public string Writer{get;set;}
         [XmlAttribute("actors")]
        public string Actors{get;set;}
         [XmlAttribute("plot")]
        public string Plot{get;set;}
         [XmlAttribute("poster")]
        public string Poster{get;set;}
         [XmlAttribute("imdbRating")]
        public string ImdbRating{get;set;}
         [XmlAttribute("imdbVotes")]
        public string ImdbVotes{get;set;}
         [XmlAttribute("imdbID")]
        public string ImdbID{get;set;}
         [XmlAttribute("type")]
        public string Type{get;set;}
         
        public Movie()
        {

        }
    }
}

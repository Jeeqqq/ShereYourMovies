using Elokuvatietue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ImdbApi
{
    #region Yhden Elokuvan Tiedot
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
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute("year")]
        public string Year { get; set; }
        [XmlAttribute("rated")]
        public string Rated { get; set; }
        [XmlAttribute("released")]
        public string Released { get; set; }
        [XmlAttribute("runtime")]
        public string Runtime { get; set; }
        [XmlAttribute("genre")]
        public string Genre { get; set; }
        [XmlAttribute("director")]
        public string Director { get; set; }
        [XmlAttribute("Writer")]
        public string Writer { get; set; }
        [XmlAttribute("actors")]
        public string Actors { get; set; }
        [XmlAttribute("plot")]
        public string Plot { get; set; }
        [XmlAttribute("poster")]
        public string Poster { get; set; }
        [XmlAttribute("imdbRating")]
        public string ImdbRating { get; set; }
        [XmlAttribute("imdbVotes")]
        public string ImdbVotes { get; set; }
        [XmlAttribute("imdbID")]
        public string ImdbID { get; set; }
        [XmlAttribute("type")]
        public string Type { get; set; }

        public Movie()
        {

        }
    }
    #endregion
    #region Elokuvan etsinnän tulokset
    [Serializable()]
    [XmlRoot("root")]
    public class Result
    {
        public Result()
        {
            Movies = new List<SearchResult>();
        }
        [XmlElement("Movie")]
        public List<SearchResult> Movies { get; set; }
    }



    public class SearchResult
    {
        [XmlAttribute("Title")]
        public string Title { get; set; }
        [XmlAttribute("Year")]
        public int Year { get; set; }
        [XmlAttribute("Type")]
        public string Type { get; set; }
        [XmlAttribute("imdbID")]
        public string ImdbID { get; set; }

        public SearchResult()
        {

        }

        public override string ToString()
        {
            return Title+"@"+Year;
        }
    }

    #endregion

    #region etsinnän serialisointi
    public class Search
    {
        public static void DeSerialisoiSearch( string nimi, ref Result leffat)
        {
            string url = "http://www.omdbapi.com/?s="+nimi+"&r=xml" ;
            XmlSerializer deserializer = new XmlSerializer(typeof(Result));
            try
            {
                 HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                 myRequest.AllowAutoRedirect = true;
                 myRequest.Method = "GET";
                 myRequest.Timeout = 6000;
                 WebResponse myResponse = myRequest.GetResponse();


                 XmlTextReader reader = new XmlTextReader(myResponse.GetResponseStream());
                 
                  leffat = (Result)deserializer.Deserialize(reader);
                  reader.Close();
                  myResponse.Close(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }

        }
        public static void DeSerialisoiIdSeach(string imdbID, ref Root leffat)
        {
            string url = "http://www.omdbapi.com/?i=" + imdbID + "&r=xml&plot=full";
            XmlSerializer deserializer = new XmlSerializer(typeof(Root));
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.AllowAutoRedirect = true;
                myRequest.Method = "GET";
                myRequest.Timeout = 6000;
                WebResponse myResponse = myRequest.GetResponse();


                XmlTextReader reader = new XmlTextReader(myResponse.GetResponseStream());

                leffat = (Root)deserializer.Deserialize(reader);
                reader.Close();
                myResponse.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        public static Movie getMovieInfoFromDb(string Nimi)
        {

            string url = "http://www.omdbapi.com/?t=" + Nimi + "&r=xml&plot=full";
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.AllowAutoRedirect = true;
                myRequest.Method = "GET";
                myRequest.Timeout = 6000;
                WebResponse myResponse = myRequest.GetResponse();


                XmlTextReader reader = new XmlTextReader(myResponse.GetResponseStream());
                XmlSerializer deserializer = new XmlSerializer(typeof(Root));
                Root leffa = (Root)deserializer.Deserialize(reader);
                reader.Close();
                myResponse.Close();

                if (leffa.leffa.Title == null)
                {
                    Exception e = new Exception("Elokuvan tietoja ei löytynyt");
                    throw e;
                }
                return leffa.leffa;
            }
            catch (Exception)
            {
              Movie  DbTiedot = new Movie();
                DbTiedot.Actors = "Tietoja ei löytynyt";
                DbTiedot.Director = "Tietoja ei löytynyt";
                DbTiedot.Genre = "Tietoja ei löytynyt";
                DbTiedot.Title = "Tietoja ei löytynyt";
                DbTiedot.Plot = "Tietoja ei löytynyt";
                DbTiedot.ImdbID = "Tietoja ei löytynyt";
                DbTiedot.ImdbRating = "Tietoja ei löytynyt";
                DbTiedot.Rated = "Tietoja ei löytynyt";

                 return DbTiedot;
            }

        }
    }

#endregion


}

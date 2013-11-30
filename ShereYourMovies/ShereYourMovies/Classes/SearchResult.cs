using ShereYourMovies.Classes;
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
                 myRequest.Timeout = 10000;
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
        public static void DeSerialisoiIdSeach(string imdbID, ref Movie movie)
        {
            string url = "http://www.omdbapi.com/?i=" + imdbID + "&r=xml&plot=full";

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.AllowAutoRedirect = true;
                myRequest.Method = "GET";
                myRequest.Timeout = 6000;
                WebResponse myResponse = myRequest.GetResponse();


                XmlTextReader reader = new XmlTextReader(myResponse.GetResponseStream());

                Search.dezirialiseXML(ref reader, ref movie);
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
        public static void dezirialiseXML(ref XmlTextReader reader,ref Movie movie)
        {
           
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode node = doc.SelectSingleNode("//movie");
            foreach (XmlAttribute att in node.Attributes)
            {
                switch (att.Name)
                {
                    case "title":
                        movie.Title = att.Value;
                        break;
                    case "year":
                        movie.Year = att.Value;
                        break;
                    case "rated":
                        movie.Rated = att.Value;
                        break;
                    case "runtime":
                        movie.Runtime = att.Value;
                        break;
                    case "genre":
                        movie.Genre = att.Value;
                        break;
                    case "director":
                        movie.Director = att.Value;
                        break;
                    case "writer":
                        movie.Writer = att.Value;
                        break;
                    case "actors":
                        movie.Actors = att.Value;
                        break;
                    case "poster":
                        movie.Poster = att.Value;
                        break;
                    case "plot":
                        movie.Plot = att.Value;
                        break;
                    case "imdbRating":
                        movie.ImdbRating = att.Value;
                        break;
                    case "imdbVotes":
                        movie.ImdbVotes = att.Value;
                        break;
                    case "imdbID":
                        movie.ImdbID = att.Value;
                        break;
                    case "type":
                        movie.Type = att.Value;
                        break;
                }

            }
           
        }
        public static Movie getMovieInfoFromDb(string Nimi)
        {
            Movie movie=new Movie();
            string url = "http://www.omdbapi.com/?t=" + Nimi + "&r=xml&plot=full";
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.AllowAutoRedirect = true;
                myRequest.Method = "GET";
                myRequest.Timeout = 6000;
                WebResponse myResponse = myRequest.GetResponse();


                XmlTextReader reader = new XmlTextReader(myResponse.GetResponseStream());
                Search.dezirialiseXML(ref reader,ref movie);
                reader.Close();
                myResponse.Close();

                if (movie.Title == null)
                {
                    Exception e = new Exception("Elokuvan tietoja ei löytynyt");
                    throw e;
                }
                
            }
            catch (Exception ex)
            {

                movie.Actors = "Tietoja ei löytynyt";
                movie.Director = "Tietoja ei löytynyt";
                movie.Genre = "Tietoja ei löytynyt";
                movie.Title = "Tietoja ei löytynyt";
                movie.Plot = "Tietoja ei löytynyt";
                movie.ImdbID = "Tietoja ei löytynyt";
                movie.ImdbRating = "Tietoja ei löytynyt";
                movie.Rated = "Tietoja ei löytynyt";

                 

            }
            return movie;
        }
    }

#endregion
}

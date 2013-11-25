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
    }
}

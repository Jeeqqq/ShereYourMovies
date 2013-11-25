using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Xml;

namespace Elokuvatietue
{

    public class ElokuvaLista
    {
        public ElokuvaLista()
        {
            Movies = new List<Elokuva>();
        }
       
        public List<Elokuva> Movies { get; set; }
        

        
        public int movieCount
        {
            get
            {
                return Movies.Count;
            }

        }
    }
    public class Elokuva
    {
        //Muuttujat
      
        public string FilePath { get; set; }
  
       public string Pituus { get; set; }
        
        public string VideoEncoding { get; set; }
       
        public string SoundEncoding { get; set; }
      
        public string TiedostonKoko { get; set; }
       
        public string Resolution { get; set; }
        
        public string Fps { get; set; }
        
        public int Tahdet { get; set; }
        
         public string Nimi { get; set; }
        
         public bool Watched { get; set; }
        // databasesta saatu Objekti
        
         public Movie DbTiedot { get; set; }

        public Elokuva()
         {

         }
        public Elokuva(string nimi, string ohjaaja, string genre, int tahdet) 
        {
            DbTiedot = new Movie();
            Nimi = nimi;
            DbTiedot.Director = ohjaaja;
            DbTiedot.Genre = genre;
            Tahdet = tahdet;
            movieDatabase();
            
        }

         public override string ToString()
         {
             return Nimi+", Tähdet: "+Tahdet;
         }
         #region videoinfot
         public string GetVideoInfo
         {

            get {  return Resolution + " at " + Fps + ", " + VideoEncoding; }
         }
 
        #endregion

         private string TrimNimi(string nimi)
         {
             string[] splitArguments = { ".", "xvid", "hdtv", "x264"," ","2hd","-","repack"};
             nimi = nimi.ToLower();
             string[] suodatetut = nimi.Split(splitArguments,5, StringSplitOptions.RemoveEmptyEntries);
             string uusinimi="";
             try
             {
                 foreach(string s in suodatetut)
                 {
                     uusinimi += s + " ";

                 }
             }
             catch (Exception)
             {
                 uusinimi = nimi;
             }
             return uusinimi;
         }

         public void movieDatabase()
         {
             
             string url = "http://www.omdbapi.com/?t="+Nimi+"&r=xml&plot=full" ;
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
                     Exception e=new Exception("Elokuvan tietoja ei löytynyt");
                     throw e;
                 }
                 DbTiedot = leffa.leffa;
             }
             catch (Exception e)
             {
                 DbTiedot = new Movie();
                 DbTiedot.Actors = "Tietoja ei löytynyt";
                 DbTiedot.Director = "Tietoja ei löytynyt";
                 DbTiedot.Genre = "Tietoja ei löytynyt";
                 DbTiedot.Title= "Tietoja ei löytynyt";
                 DbTiedot.Plot= "Tietoja ei löytynyt";
                 DbTiedot.ImdbID = "Tietoja ei löytynyt";
                 DbTiedot.ImdbRating = "Tietoja ei löytynyt";
                 DbTiedot.Rated = "Tietoja ei löytynyt";
                 
                // return false;
             }

         }



    }
}

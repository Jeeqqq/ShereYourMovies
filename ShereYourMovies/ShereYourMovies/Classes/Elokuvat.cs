using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace ShereYourMovies.Classes
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
    [Table(Name="Elokuva")]
    public class Elokuva
    {
        //Muuttujat
        [Column(Storage = "ElokuvaID", Name = "ElokuvaID", DbType = "BigInt IDENTITY NOT NULL", IsPrimaryKey = true, IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        private int ElokuvaID;
        [Column]
        public string FilePath { get; set; }
        [Column]
        public string Pituus { get; set; }
        [Column]
        public string VideoEncoding { get; set; }
       [Column]
        public string SoundEncoding { get; set; }
      [Column]
        public string TiedostonKoko { get; set; }
       [Column]
        public string Resolution { get; set; }
        [Column]
        public string Fps { get; set; }
        [Column]
        public int Tahdet { get; set; }
        [Column]
         public string Nimi { get; set; }
        [Column]
         public bool Watched { get; set; }
        // yhdistetään Elokuva taulu MOvie tauluun.

        private EntitySet<Movie> _DbTiedot;
        [Association(Storage = "_DbTiedot", OtherKey = "ElokuvaID")]
        public EntitySet<Movie> DbTiedot 
        {
            get { return this._DbTiedot; }
            set { this._DbTiedot.Assign(value); }
        }

        //yhdistetään User Taulu Elokuva Tauluun
        [Column]
        public int UserID;
        private EntityRef<User> _User;
        [Association(Storage = "_User", ThisKey = "UserID")]
        public User User
        {
            get { return this._User.Entity; }
            set { this._User.Entity = value; }
        }

        public Elokuva()
         {
             this._DbTiedot = new EntitySet<Movie>();
             this._User = default(EntityRef<User>);
         }
        public Elokuva(string nimi, string ohjaaja, string genre, int tahdet) 
        {
            //DbTiedot = new Movie();
            Nimi = nimi;
           // DbTiedot.Director = ohjaaja;
            //DbTiedot.Genre = genre;
            Tahdet = tahdet;
            this._DbTiedot = new EntitySet<Movie>();
            this._User = default(EntityRef<User>);
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
                 DbTiedot.Add(leffa.leffa);
             }
             catch (Exception e)
             {
                 Movie movie = new Movie();
                 movie.Actors = "Tietoja ei löytynyt";
                 movie.Director = "Tietoja ei löytynyt";
                 movie.Genre = "Tietoja ei löytynyt";
                 movie.Title = "Tietoja ei löytynyt";
                 movie.Plot = "Tietoja ei löytynyt";
                 movie.ImdbID = "Tietoja ei löytynyt";
                 movie.ImdbRating = "Tietoja ei löytynyt";
                 movie.Rated = "Tietoja ei löytynyt";
                 DbTiedot.Add(movie);
                // return false;
             }

         }



    }
}

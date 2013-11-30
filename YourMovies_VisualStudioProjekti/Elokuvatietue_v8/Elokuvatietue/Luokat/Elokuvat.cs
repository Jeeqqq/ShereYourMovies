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
using ImdbApi;
using MediaInfoLib;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Elokuvatietue
{
    #region lista kaikista elokuvista
    [Serializable()]
    [XmlRoot("ElokuvaLista")]
    public class ElokuvaLista
    {
        public ElokuvaLista()
        {
            Movies = new List<Elokuva>();
           
        }

        [XmlElement("movie")]
        public List<Elokuva> Movies{ get; set; }

        public int movieCount
        {
            get
            {
                return Movies.Count;
            }

        }
       
      
    }
    #endregion
    #region Elukuva luokka
    [Table(Name = "Elokuva")]
    public class Elokuva 
    {
        #region muuttujat

        //yhdistetään User Taulu Elokuva Tauluun
        [Column]
        public string UserName;

        //Muuttujat
        private int _elokuvaID;
        [Column(DbType = "BigInt IDENTITY NOT NULL", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ElokuvaID 
        {
            set 
            { _elokuvaID = value;
           // NotifyPropertyChanged();
            }
            get { return _elokuvaID; } 
        
        }

        //elokuvia voidaan lisätä eri listoihin, tämä muuttuja pitää listan nimen sisällään.
        [Column]
        public string Lista { get; set; }
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

        
        
        private EntityRef<Movie> _DbTiedot= new EntityRef<Movie>();
        [Association(Name = "FK_ELOKUVA_MOVIE", IsForeignKey = true, Storage = "_DbTiedot", ThisKey = "movieId", OtherKey = "MovieID", DeleteRule = "CASCADE")]
        public Movie DbTiedot
        {
            get { return this._DbTiedot.Entity; }
            set {
                
                
                this._DbTiedot.Entity=value;
            
            
            }
        }

        [Column(DbType = "BigInt")]
        private int movieId;
        
        public int MovieID
        {
            get { return movieId; }
            set { movieId = value; }
        }
        
       
        #endregion

       

        #region konstruktorit
        public Elokuva()
        {
           
        }
        public Elokuva(string nimi, string ohjaaja, string genre, int tahdet) 
        {

            Movie movie = new Movie();
            Nimi = nimi;
            movie.Director = ohjaaja;
            movie.Genre = genre;
            Tahdet = tahdet;
            movie = Search.getMovieInfoFromDb(Nimi);
           // movie.Elokuva = this;
            DbTiedot=movie;
            
        }
         public Elokuva(string nimi, string filePath)
        {

            Watched = false;
            FilePath = filePath;
            MediaInfo MI = new MediaInfo();
            MI.Open(FilePath);
            nimi = MI.Get(StreamKind.General, 0, "FileName");
            Nimi = TrimNimi(nimi);
            Pituus = MI.Get(StreamKind.General, 0, "Duration/String");
            VideoEncoding = MI.Get(StreamKind.General, 0, "Format");
            SoundEncoding = MI.Get(StreamKind.Audio, 0, "Language/String") + "," + MI.Get(StreamKind.Audio, 0, "SamplingRate/String") + "," + MI.Get(StreamKind.Audio, 0, "Channel(s)/String") + "," + MI.Get(StreamKind.General, 0, "Audio_Format_List");
            TiedostonKoko = MI.Get(StreamKind.General, 0, "FileSize/String");
            Fps = MI.Get(StreamKind.Video, 0, "FrameRate/String");
            Resolution = MI.Get(StreamKind.Video, 0, "Width") + "x" + MI.Get(StreamKind.Video, 0, "Height");
            MI.Close();
            Movie movie = Search.getMovieInfoFromDb(Nimi);
          //  movie.Elokuva = this;
            DbTiedot=movie;

        }
        #endregion
         #region videoinfot
         public string GetVideoInfo
         {

            get {  return Resolution + " at " + Fps + ", " + VideoEncoding; }
         }
 
        #endregion

         private string TrimNimi(string nimi)
         {
             string[] splitArguments = { ".", "xvid", "hdtv", "x264"," ","2009","2010","2008","2011","_","2hsd","-","bluray","amiable","repack","2013","1080p","read","info","tommiecook","brrip","gaz","yify","publichd","720p","2012","ac3","zoo","sparks","yify","hdchina","[","]","dts","bokutox","simonklose","absurdity","dvdrip","h264","bokutox","bdrip","vision","souvlaaki","trips","blow","axxo","{","}","www","torrentz","noir","fxg","eng","max","maxspeed","arrow","fxm","2012","2007","2006","2005","2004","2003","2002","2001","2000","1999","1998","1997","1996","1995","1994","1993","1992","1991","~","1337","1337x","(",")"};
             nimi = nimi.ToLower();
             string[] suodatetut = nimi.Split(splitArguments,20, StringSplitOptions.RemoveEmptyEntries);
             string uusinimi="";
             try
             {
                 foreach(string s in suodatetut)
                 {
                     uusinimi += s;
                     if(s != suodatetut[suodatetut.Length-1])
                         uusinimi +=" ";
                 }
             }
             catch (Exception)
             {
                 uusinimi = nimi;
             }
             return uusinimi;
         }


    }
#endregion
}



    

        



  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MediaInfoLib;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Xml;
using ImdbApi;
using System.ComponentModel;
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

     
       
      
    }
    #endregion
    #region Elukuva luokka
    [Serializable()]
    public class Elokuva
    {
        #region muuttujat
        //Muuttujat
         [XmlElement("FilePath")]
        public string FilePath { get; set; }
        [XmlElement("Pituus")]
       public string Pituus { get; set; }
        [XmlElement("VideoEncoding")]
        public string VideoEncoding { get; set; }
        [XmlElement("SoundEncoding")]
        public string SoundEncoding { get; set; }
        [XmlElement("TiedostonKoko")]
        public string TiedostonKoko { get; set; }
        [XmlElement("Resolution")]
        public string Resolution { get; set; }
        [XmlElement("Fps")]
        public string Fps { get; set; }
         [XmlElement("Tahdet")]
        public int Tahdet { get; set; }
         [XmlElement("Nimi")]
         public string Nimi { get; set; }
         [XmlElement("Katsottu")]
         public bool Watched { get; set; }
        // databasesta saatu Objekti
        [XmlElement("movie")]
         public Movie DbTiedot { get; set; }

        #endregion
        #region konstruktorit
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
            DbTiedot = Search.getMovieInfoFromDb(Nimi);
            
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
            DbTiedot =Search.getMovieInfoFromDb(Nimi);

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

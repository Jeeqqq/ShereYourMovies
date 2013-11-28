using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MediaInfoLib;

namespace Elokuvatietue
{
    [Serializable()]
    [XmlRoot("MusiikkiLista")]
    class MusiikkiLista
    {
        public MusiikkiLista()
        {
            Musa = new List<Musiikki>();
           
        }

        [XmlElement("Kappale")]
        public List<Musiikki> Musa{ get; set; }
    }
     [Serializable()]
    class Musiikki
    {
        #region muuttujat
        //Muuttujat
        [XmlElement("FilePath")]
        public string FilePath { get; set; }
        [XmlElement("Pituus")]
        public string Pituus { get; set; }
        [XmlElement("SoundEncoding")]
        public string SoundEncoding { get; set; }
        [XmlElement("TiedostonKoko")]
        public string TiedostonKoko { get; set; }
        [XmlElement("Tahdet")]
        public int Tahdet { get; set; }
        [XmlElement("Nimi")]
        public string Nimi { get; set; }
        // databasesta saatu Objekti

        #endregion

         #region konstruktorit
        public Musiikki()
         {

         }

        public Musiikki(string nimi, string filePath)
        {

            FilePath = filePath;
            MediaInfo MI = new MediaInfo();
            MI.Open(FilePath);
            Nimi = MI.Get(StreamKind.General, 0, "FileName");
            Pituus = MI.Get(StreamKind.General, 0, "Duration/String");
            SoundEncoding = MI.Get(StreamKind.Audio, 0, "Language/String") + "," + MI.Get(StreamKind.Audio, 0, "SamplingRate/String") + "," + MI.Get(StreamKind.Audio, 0, "Channel(s)/String") + "," + MI.Get(StreamKind.General, 0, "Audio_Format_List");
            TiedostonKoko = MI.Get(StreamKind.General, 0, "FileSize/String");
            MI.Close();
        }
        #endregion
    }
}

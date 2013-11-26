using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;


namespace ShereYourMovies.Classes
{
    public class Serialisointi
    {
        #region XmlTiedostoMetodit
        public static void SerialisoiXml(string tiedosto, RssLista ic)
        {
            XmlSerializer xs = new XmlSerializer(ic.GetType());
            TextWriter tw = new StreamWriter(tiedosto);
            try
            {
                xs.Serialize(tw, ic);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                tw.Close();
            }
        }
        public static void DeSerialisoiXml(string filePath, ref RssLista feed)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RssLista));
            try
            {

                FileStream xmlFile = new FileStream(filePath, FileMode.Open);
                feed = (RssLista)deserializer.Deserialize(xmlFile);
                xmlFile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        #endregion
    }
}
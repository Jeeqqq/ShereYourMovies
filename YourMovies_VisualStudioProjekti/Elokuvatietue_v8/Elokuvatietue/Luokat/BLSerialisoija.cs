using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Elokuvatietue;

namespace Elokuvatietue
{
    public class Serialisointi
    {
        #region XmlTiedostoMetodit
        public static void SerialisoiXml(string tiedosto, ElokuvaLista ic)
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
        public static void DeSerialisoiXml(string filePath, ref ElokuvaLista leffat)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ElokuvaLista));
            try
            {

                FileStream xmlFile = new FileStream(filePath, FileMode.Open);
                leffat = (ElokuvaLista)deserializer.Deserialize(xmlFile);
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
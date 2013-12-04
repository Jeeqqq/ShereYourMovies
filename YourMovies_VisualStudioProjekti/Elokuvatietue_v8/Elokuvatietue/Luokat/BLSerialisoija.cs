using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Elokuvatietue;
using System.Xml;
using ImdbApi;
using System.Net;

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
        public static void DeSerialisoiXml(string filePath, ref ElokuvaLista leffat,string lista,string username)
        {
            
            try
            {
                
                FileStream xmlFile = new FileStream(filePath, FileMode.Open);
                XmlTextReader reader = new XmlTextReader(xmlFile);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                XmlNodeList nodes = doc.SelectSingleNode("//ElokuvaLista").ChildNodes;
                
                foreach(XmlNode node in nodes)
                {
                    Elokuva leffa = new Elokuva();
                    XmlNodeList elements = node.ChildNodes;
                    leffa.Lista = lista;
                    leffa.UserName = username;
                    leffa.FilePath = elements[0].InnerText;
                    leffa.Pituus = elements[1].InnerText;
                    leffa.VideoEncoding = elements[2].InnerText;
                    leffa.SoundEncoding = elements[3].InnerText;
                    leffa.TiedostonKoko = elements[4].InnerText;
                    leffa.Resolution = elements[5].InnerText;
                    leffa.Fps = elements[6].InnerText;
                    leffa.Tahdet = Int32.Parse(elements[7].InnerText);
                    leffa.Nimi = elements[8].InnerText;
                    leffa.Watched = Convert.ToBoolean(elements[9].InnerText);
                    leffa.DbTiedot = new Movie();
                    foreach (XmlAttribute att in elements[10].Attributes)
                    {
                        switch (att.Name)
                        {
                            case "title":
                                leffa.DbTiedot.Title = att.Value;
                                break;
                            case "year":
                                leffa.DbTiedot.Year = att.Value;
                                break;
                            case "rated":
                                leffa.DbTiedot.Rated = att.Value;
                                break;
                            case "runtime":
                                leffa.DbTiedot.Runtime = att.Value;
                                break;
                            case "genre":
                                leffa.DbTiedot.Genre = att.Value;
                                break;
                            case "director":
                                leffa.DbTiedot.Director = att.Value;
                                break;
                            case "writer":
                                leffa.DbTiedot.Writer = att.Value;
                                break;
                            case "actors":
                                leffa.DbTiedot.Actors = att.Value;
                                break;
                            case "poster":
                                leffa.DbTiedot.Poster = att.Value;
                                break;
                            case "plot":
                                leffa.DbTiedot.Plot = att.Value;
                                break;
                            case "imdbRating":
                                leffa.DbTiedot.ImdbRating = att.Value;
                                break;
                            case "imdbVotes":
                                leffa.DbTiedot.ImdbVotes = att.Value;
                                break;
                            case "imdbID":
                                leffa.DbTiedot.ImdbID = att.Value;
                                break;
                            case "type":
                                leffa.DbTiedot.Type = att.Value;
                                break;
                        }
                    }
                    leffat.Movies.Add(leffa);

                }
                xmlFile.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }
        public static void DeSerialisoiXml(string uri, ref RssLista feed)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RssLista));
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(uri);
                myRequest.AllowAutoRedirect = true;
                myRequest.Method = "GET";
                myRequest.Timeout = 10000;
                WebResponse myResponse = myRequest.GetResponse();


                XmlTextReader reader = new XmlTextReader(myResponse.GetResponseStream());
                feed = (RssLista)deserializer.Deserialize(reader);
                reader.Close();
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
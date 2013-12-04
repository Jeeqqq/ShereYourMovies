using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShereYourMovies.Classes
{
    /// <summary>
    /// Summary description for DownloadFile
    /// </summary>
    public class DownloadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=YourMovies_64bit.zip;");
            response.TransmitFile(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/YourMovies_64bit.zip"));
            response.Flush();
            response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
using ShereYourMovies.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShereYourMovies
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
       
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
               
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;
               
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            if(Session["UserAuthentication"] == null )
            {
                if (Request.Url.AbsolutePath == "/Account/Login" || Request.Url.AbsolutePath == "/Account/Register")
                {
                    
                }
                else Response.Redirect("/Account/Login"); ;
            }
            




            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["db"] == null)
                {
                    SqlConnection myConnection = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=shareyourmovies;Integrated Security=True");
                    YourMovies db = new YourMovies(myConnection);
                    Session["db"] = db;
                }
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
                
                
            }
            else
            {
                if (Session["UserAuthentication"] != null)
                {

                    ViewState[AntiXsrfUserNameKey] = Session["UserAuthentication"];
                }
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //aja tämä vain kerran ja sitten kommentoi tämä rivi. tuon construoktori luo yhen leffan ja yhen käyttäjän
            //username:Teppo ja pass: salasana
            ElokuvaController eController = new ElokuvaController();
        }
        protected void logout(object sender, EventArgs e)
        {
            Session["UserAuthentication"] = null;
            
        }
    }
}
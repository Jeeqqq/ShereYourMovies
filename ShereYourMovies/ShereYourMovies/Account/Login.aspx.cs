using ShereYourMovies.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShereYourMovies.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";

        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            
            YourMovies db =(YourMovies) Session["db"];

            if (UserController.Authenticate(loginWindow.UserName.ToString(), loginWindow.Password.ToString(),ref db))
            {
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }
        }
        protected void Login_LoginError(object sender, EventArgs e)
        {
            Session["UserAuthentication"] = null;
        }
        protected void Login_LoggedIn(object sender, EventArgs e)
        {
            Session["UserAuthentication"] = loginWindow.UserName.ToString();
            Response.Redirect("~");
        }

    }
}
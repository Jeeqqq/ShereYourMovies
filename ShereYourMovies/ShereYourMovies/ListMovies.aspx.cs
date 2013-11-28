using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ShereYourMovies.Classes;

namespace ShereYourMovies
{
    public partial class ListMovies : System.Web.UI.Page
    {
        List<Elokuva> elokuvalista = new List<Elokuva>();
        YourMovies db;

        #region Sorting parameters
        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }
        private string GridViewSortExpression
        {
            get { return ViewState["SortExpression"] as string ?? string.Empty; }
            set { ViewState["SortExpression"] = value; }
        }
        private string GetSortDirection()
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;
                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }

            return GridViewSortDirection;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                myIni();
            }
            else 
            {
                elokuvalista = (List<Elokuva>)Session["elokuvalista"];
                db = (YourMovies)Session["db"];
            }
        }

        private void myIni()
        {
            db = (YourMovies)Session["db"];
            String username = (String)Session["UserAuthentication"];

            IQueryable<Elokuva> elokuva = ElokuvaController.getMoviesByUsers(username, ref db);

            List<Elokuva> elokuvalista = elokuva.ToList();

            lblDebug.InnerText = elokuvalista.Count().ToString();
            husername.InnerText = username;

            toGridView(elokuvalista);
        }

        protected void toGridView(List<Elokuva> elokuva) 
        {
            Session["elokuvalista"] = elokuva;

            grdElokuvat.DataSource = elokuva;
            grdElokuvat.DataBind();
        }


        #region grdElokuvat controls
        protected void grdElokuvat_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpression = e.SortExpression;
            toGridView(ElokuvaController.sortList(elokuvalista, GridViewSortExpression, GetSortDirection()));
        }

        protected void grdElokuvat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdElokuvat.PageIndex = e.NewPageIndex;
            toGridView(elokuvalista);
        }

        protected void grdElokuvat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdElokuvat.EditIndex = -1;
            toGridView(elokuvalista);
        }

        protected void grdElokuvat_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ElokuvaController.deleteElokuva(elokuvalista.ElementAt(e.RowIndex), ref db))
            {
                elokuvalista.RemoveAt(e.RowIndex);
                lblDebug.InnerText = "Poistaminen onnistui!";
            }
            else
            {
                lblDebug.InnerText = "Poistaminen ei onnistunut!";
            }
            toGridView(elokuvalista);
        }

        protected void grdElokuvat_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdElokuvat.EditIndex = e.NewEditIndex;
            toGridView(elokuvalista);
        }

        protected void grdElokuvat_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = grdElokuvat.Rows[e.RowIndex];

            elokuvalista[e.RowIndex].Nimi = ((TextBox)(row.Cells[1].Controls[0])).Text;

            if (ElokuvaController.updateElokuva(elokuvalista.ElementAt(e.RowIndex), ref db))
            {
                grdElokuvat.EditIndex = -1;
                lblDebug.InnerText = "Päivitys onnistui!";
            }
            else
            {
                GridViewCancelEditEventArgs a = new GridViewCancelEditEventArgs(e.RowIndex);
                lblDebug.InnerText = "Päivitys ei onnistunut!";
                grdElokuvat_RowCancelingEdit(sender, a);
            }

            myIni();
        }

        protected void grdElokuvat_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Session["elokuvainfo"] = elokuvalista.ElementAt(e.NewSelectedIndex);
            Server.TransferRequest("~/MovieInfo.aspx");
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ShereYourMovies.Classes;
using System.Collections;

namespace ShereYourMovies
{
    public partial class ListMovies : System.Web.UI.Page
    {
        List<Elokuva> elokuvalista = new List<Elokuva>();
        Elokuva _leffa;
        Elokuva leffa
        { 
            get { return _leffa; } 
            set 
            {
                _leffa = value;
                Session["selectedMovie"] = value;
            } 
        }
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
                leffa = (Elokuva)Session["selectedMovie"];
            }
        }

        private void myIni()
        {

            YourMovies db = (YourMovies)Session["db"];
            String username = Context.User.Identity.Name;

            IQueryable<Elokuva> elokuva = ElokuvaController.getMoviesByUsers(username, ref db);

            List<Elokuva> elokuvalista = elokuva.ToList();

            

           // toGridView(elokuvalista);
            toListView(elokuvalista);
            Session["elokuvalista"] = elokuvalista;
        }

        protected void toGridView(List<Elokuva> elokuva) 
        {
            Session["elokuvalista"] = elokuva;

            grdElokuvat.DataSource = elokuva;
            grdElokuvat.DataBind();
        }
        protected void toListView(List<Elokuva> elokuva)
        {

            ListView1.DataSource = elokuva;
            ListView1.DataBind();
        }
        protected void toListView2()
        {
            toListView(null);
            List<Elokuva> elokuva = new List<Elokuva>();
            elokuva.Add(leffa);
            ListView2.DataSource = elokuva;
            ListView2.DataBind();
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
                //lblDebug.InnerText = "Poistaminen onnistui!";
            }
            else
            {
               // lblDebug.InnerText = "Poistaminen ei onnistunut!";
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
               // lblDebug.InnerText = "Päivitys onnistui!";
            }
            else
            {
                GridViewCancelEditEventArgs a = new GridViewCancelEditEventArgs(e.RowIndex);
               // lblDebug.InnerText = "Päivitys ei onnistunut!";
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

        protected void openMovieInfo_Command(object sender, CommandEventArgs e)
        {
            int id =Int32.Parse(e.CommandArgument.ToString());
            List<Elokuva> elokuva=elokuvalista.FindAll(eKuva => eKuva.ElokuvaID == id);
            leffa = elokuva.ElementAt(0);
            toListView2();

            DataPagerMovies.Visible = false;

        }

        protected void btnBack_Command(object sender, CommandEventArgs e)
        {
            leffa = null;
            toListView(elokuvalista);
            DataPagerMovies.Visible = true;
            ListView2.DataSource = null;
            ListView2.DataBind();
        }

        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //set current page startindex, max rows and rebind to false
            DataPagerMovies.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            
            //rebind List View
            toListView(elokuvalista);
            ListView2.DataSource = null;
            ListView2.DataBind();
        }

        protected void btnEtsi_Command(object sender, CommandEventArgs e)
        {
            toListView2();
        }


        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            toListView2();
            switch (e.CommandName)
            {
                case "Edit":
                    ListView2.EditIndex = 0;
                    break;
                case "Update":
                    
                    break;
            }
        }

        protected void ListView2_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            toListView2();
            
        }

        protected void ListView2_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var asd = ListView2.Items[e.ItemIndex];
            foreach (DictionaryEntry de in e.NewValues)
            {
                // Check if the value is null or empty.
                if (de.Value == null || de.Value.ToString().Trim().Length == 0)
                {
                   
                    e.Cancel = true;
                }
            } 
            ListView2.EditIndex = -1;
            toListView2();
            
            
        }

        protected void ListView2_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
        {
            toListView2();
            leffa = (Elokuva)ListView2.EditItem.DataItem;
            ElokuvaController.updateElokuva(leffa, ref db);
        }

    }
}
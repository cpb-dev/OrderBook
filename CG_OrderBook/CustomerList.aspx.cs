using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CG_OrderBook
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void LoadSearch()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderDBConnectionString"].ConnectionString);
            con.Open();
            SqlCommand sqlFindCust;

            //If statements depending on the fields the user inputs data to
            if (string.IsNullOrEmpty(tbSearchFN.Text))
            {
                //Only searching surname as no first name has been entered
                sqlFindCust = new SqlCommand("FindCustomerSN", con);
                sqlFindCust.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindCust.Parameters.AddWithValue("@sname", tbSearchSN.Text.Trim());
            } 
            else if (string.IsNullOrEmpty(tbSearchSN.Text))
            {
                //Only searching first name as no surname has been entered
                sqlFindCust = new SqlCommand("FindCustomerFN", con);
                sqlFindCust.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindCust.Parameters.AddWithValue("@fname", tbSearchFN.Text.Trim());
            }
            else
            {
                //Searching both name fields
                sqlFindCust = new SqlCommand("FindCustomer", con);
                sqlFindCust.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindCust.Parameters.AddWithValue("@fname", tbSearchFN.Text.Trim());
                sqlFindCust.Parameters.AddWithValue("@sname", tbSearchSN.Text.Trim());
            }

            //Inputting the searched info into an adapter to be displayed in the searched grid view
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlFindCust);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            gvSearch.DataSource = dataTable;
            gvSearch.DataBind();
            gvSearch.Visible = true;
            gvCustomers.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSearch();
        }

        protected void gvSearch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSearch.EditIndex = e.NewEditIndex;
            LoadSearch();
        }

        protected void gvSearch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSearch.EditIndex = -1; //closing the selection made
            LoadSearch();
        }

        protected void gvSearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Getting the information on the row selected and the relevant rows they correspond to
            int custID = Convert.ToInt32(gvSearch.DataKeys[e.RowIndex].Value.ToString());
            string custEmail = ((TextBox)(gvSearch.Rows[e.RowIndex].Cells[3].Controls[0])).Text;
            string custPhone = ((TextBox)(gvSearch.Rows[e.RowIndex].Cells[4].Controls[0])).Text;

            //Sql connection
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderDBConnectionString"].ConnectionString);
            con.Open();
            //Defining the procedure
            SqlCommand updateCommand = new SqlCommand("AmendCustomer", con);
            updateCommand.CommandType = CommandType.StoredProcedure;
            //Updating the database based on the user inputs, using the procedure defined
            updateCommand.Parameters.AddWithValue("@customerID", custID);
            updateCommand.Parameters.AddWithValue("@email", custEmail);
            updateCommand.Parameters.AddWithValue("@phone", custPhone);

            updateCommand.ExecuteNonQuery();

            //Closing the edit function on text boxes
            gvSearch.EditIndex = -1;
            LoadSearch(); //refreshing the page
        }
    }
}
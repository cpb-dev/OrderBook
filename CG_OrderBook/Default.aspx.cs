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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSearch(); //Loading the search method
        }
        public void LoadSearch()
        {
            //Opening DB connection
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderDBConnectionString"].ConnectionString);
            con.Open();

            DataTable dataTable = new DataTable(); //Creating a data table for the relevent user inputs

            //If statements to change the search criteria based on the users choice
            if (dlSearchBy.SelectedValue == "0")
            {
                SqlCommand sqlFindDate = new SqlCommand("SearchDate", con);
                sqlFindDate.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindDate.Parameters.AddWithValue("@date", DateTime.Parse(tbDate.Text).ToString("dd/MM/yyyy"));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlFindDate);
                dataAdapter.Fill(dataTable);
            } 
            else if(dlSearchBy.SelectedValue == "1")
            {
                SqlCommand sqlFindCust;
                //If statement that depends on the name fields a user has inputted into
                if (string.IsNullOrEmpty(tbFName.Text))
                { //If first name is null then the search has to be done on the surname
                    sqlFindCust = new SqlCommand("SearchSName", con);
                    sqlFindCust.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlFindCust.Parameters.AddWithValue("@sname", tbSName.Text.Trim());
                } 
                else if (string.IsNullOrEmpty(tbSName.Text))
                { //This is the opposite to the last statement
                    sqlFindCust = new SqlCommand("SearchFName", con);
                    sqlFindCust.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlFindCust.Parameters.AddWithValue("@fname", tbFName.Text.Trim());
                }
                else
                { //If both name inputs are found then a direct search on both of them will be done
                    sqlFindCust = new SqlCommand("SearchName", con);
                    sqlFindCust.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlFindCust.Parameters.AddWithValue("@fname", tbFName.Text.Trim());
                    sqlFindCust.Parameters.AddWithValue("@sname", tbSName.Text.Trim());
                }
                
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlFindCust);
                dataAdapter.Fill(dataTable);
            } 
            else if(dlSearchBy.SelectedValue == "2")
            {
                SqlCommand sqlFindStatus = new SqlCommand("SearchStatus", con);
                sqlFindStatus.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindStatus.Parameters.AddWithValue("@status", tbSearch.Text.Trim());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlFindStatus);
                dataAdapter.Fill(dataTable);
            } 
            else if (dlSearchBy.SelectedValue == "3")
            {
                SqlCommand sqlFindBrand = new SqlCommand("SearchBrand", con);
                sqlFindBrand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindBrand.Parameters.AddWithValue("@brand", Convert.ToString(dlBrands.SelectedItem.ToString()));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlFindBrand);
                dataAdapter.Fill(dataTable);
            } 
            else if (dlSearchBy.SelectedValue == "4")
            {
                SqlCommand sqlFindPaid = new SqlCommand("SearchPaid", con);
                sqlFindPaid.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindPaid.Parameters.AddWithValue("@paid", dlPaid.SelectedValue.ToString());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlFindPaid);
                dataAdapter.Fill(dataTable);
            } 
            else if (dlSearchBy.SelectedValue == "5")
            {
                SqlCommand sqlFindSCode = new SqlCommand("SearchSCode", con);
                sqlFindSCode.CommandType = System.Data.CommandType.StoredProcedure;
                sqlFindSCode.Parameters.AddWithValue("@scode", tbSearch.Text.Trim());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlFindSCode);
                dataAdapter.Fill(dataTable);
            }

            //Setting the search table as visible and the original table with all fields as invisible
            gvSearchOrder.DataSource = dataTable;
            gvSearchOrder.DataSource = dataTable;
            gvSearchOrder.DataBind();
            gvSearchOrder.Visible = true;
            gvOrders.Visible = false;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request.Path); //Reloading the page
        }

        protected void dlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Changing the input box area based on user choice
            //This enables the user to input using the right format
            if(dlSearchBy.SelectedValue == "0")
            {
                //Making visible the desired field and all the others invisible
                tbDate.Visible = true;
                tbSearch.Visible = false;
                tbFName.Visible = false;
                tbSName.Visible = false;
                dlBrands.Visible = false;
                dlPaid.Visible = false;
                dlStatus.Visible = false;
            }
            else if(dlSearchBy.SelectedValue == "1")
            {
                tbDate.Visible = false;
                tbSearch.Visible = false;
                tbFName.Visible = true;
                tbSName.Visible = true;
                dlBrands.Visible = false;
                dlPaid.Visible = false;
                dlStatus.Visible = false;
            }
            else if(dlSearchBy.SelectedValue == "2")
            {
                tbDate.Visible = false;
                tbSearch.Visible = false;
                tbFName.Visible = false;
                tbSName.Visible = false;
                dlBrands.Visible = false;
                dlPaid.Visible = false;
                dlStatus.Visible = true;
            }
            else if(dlSearchBy.SelectedValue == "3")
            {
                BrandsDropDown();
                tbDate.Visible = false;
                tbSearch.Visible = false;
                tbFName.Visible = false;
                tbSName.Visible = false;
                dlBrands.Visible = true;
                dlPaid.Visible = false;
                dlStatus.Visible = false;
            } else if(dlSearchBy.SelectedValue =="4")
            {
                tbDate.Visible = false;
                tbSearch.Visible = false;
                tbFName.Visible = false;
                tbSName.Visible = false;
                dlBrands.Visible = false;
                dlPaid.Visible = true;
                dlStatus.Visible = false;
            } else if(dlSearchBy.SelectedValue == "5")
            {
                tbDate.Visible = false;
                tbSearch.Visible = true;
                tbFName.Visible = false;
                tbSName.Visible = false;
                dlBrands.Visible = false;
                dlPaid.Visible = false;
                dlStatus.Visible = false;
            }
        }
        private void BrandsDropDown()
        {
            //Same method as in Default.aspx to gather all the brands from the database
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderDBConnectionString"].ConnectionString);
            con.Open();

            SqlCommand brandList = new SqlCommand("select * from brands_list", con);
            SqlDataAdapter brandDA = new SqlDataAdapter(brandList);
            DataSet dataset = new DataSet();
            brandDA.Fill(dataset);
            dlBrands.DataTextField = dataset.Tables[0].Columns["Brand_Name"].ToString();
            dlBrands.DataValueField = dataset.Tables[0].Columns["Brand_Name"].ToString();
            dlBrands.DataSource = dataset.Tables[0];
            dlBrands.DataBind();
        }

        protected void gvSearchOrder_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Allowing the user to edit the row after the search has been made
            //Due to new table being used
            gvSearchOrder.EditIndex = e.NewEditIndex;
            LoadSearch();
        }

        protected void gvSearchOrder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSearchOrder.EditIndex = -1;
            LoadSearch();
        }

        protected void gvSearchOrder_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Gathing the row chosen and the user inputs on the gridview
            int orderID = Convert.ToInt32(gvSearchOrder.DataKeys[e.RowIndex].Value.ToString());
            string status = ((TextBox)(gvSearchOrder.Rows[e.RowIndex].Cells[7].Controls[0])).Text;
            string paid = ((TextBox)(gvSearchOrder.Rows[e.RowIndex].Cells[8].Controls[0])).Text;

            //Opening a connection to the database
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderDBConnectionString"].ConnectionString);
            con.Open();

            //Getting the input data via the stored procedure for updating database
            SqlCommand updateCommand = new SqlCommand("AmendOrder", con);
            updateCommand.CommandType = CommandType.StoredProcedure;

            //Inputting amends based on the user data to database
            updateCommand.Parameters.AddWithValue("@Order_ID", orderID);
            updateCommand.Parameters.AddWithValue("@Paid", paid);
            updateCommand.Parameters.AddWithValue("@OrderStatus", status);

            updateCommand.ExecuteNonQuery();

            gvSearchOrder.EditIndex = -1;
            LoadSearch();
        }
    }
}
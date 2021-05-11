using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace CG_OrderBook
{
    public partial class OrderForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OrderDBConnectionString"].ConnectionString);
        String txtDate = DateTime.Today.ToString("dd/MM/yyyy"); //Retrieving date for order
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BrandsDropDown();
            }
        }

        private void BrandsDropDown()
        {
            //Gathering all the brand names from the table in DB
            con.Open();

            SqlCommand brandList = new SqlCommand("select * from brands_list", con);
            SqlDataAdapter brandDA = new SqlDataAdapter(brandList);
            DataSet dataset = new DataSet();
            brandDA.Fill(dataset);
            dlBrand.DataTextField = dataset.Tables[0].Columns["Brand_Name"].ToString();
            dlBrand.DataValueField = dataset.Tables[0].Columns["Brand_Name"].ToString();
            dlBrand.DataSource = dataset.Tables[0];
            dlBrand.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            checkValidation();
        }
        public void checkValidation()
        {
            //Gathering the validation from the models of the different classes
            Models.Customer cust = new Models.Customer();
            Models.products prod = new Models.products();

            //Assigning the user inputs to the classes relevant fields
            cust.FirstName = txtCustFName.Text.ToString();
            cust.Surname = txtCustSName.Text.ToString();
            cust.Email = txtEmail.Text.ToString();
            cust.Phone = txtPhone.Text.ToString();
            prod.ProductName = txtItem.Text.ToString();
            prod.SCode = txtSCode.Text.ToString();

            //Checking the data and assigning validation context to them
            var contextCust = new ValidationContext(cust, serviceProvider: null, items: null);
            var contextProd = new ValidationContext(prod, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValidCust = Validator.TryValidateObject(cust, contextCust, results, true);
            var isValidProd = Validator.TryValidateObject(prod, contextProd, results, true);

            //If statements for ensuring the validations go through before uploading to the database
            if (!isValidCust && !isValidProd)
            {
                foreach(var validationResult in results)
                {
                    Response.Write(validationResult.ErrorMessage.ToString());
                }
                return;
            }
            else
            {
                uploadData();
            }
        }
        public void uploadData()
        {
            try
            {
                String txtPaid; //Gathering check box data
                if (cbPaid.Checked)
                {
                    txtPaid = "Yes";
                }
                else
                {
                    txtPaid = "No";
                }

                using (con)
                {
                    con.Open(); //Opening the connection to DB

                    //Retrieving the next value for the IDs within the DB
                    SqlCommand countCust = new SqlCommand("SELECT COUNT(*) FROM customer_info", con);
                    SqlCommand countOrder = new SqlCommand("SELECT COUNT(*) FROM order_info", con);
                    SqlCommand countProduct = new SqlCommand("SELECT COUNT(*) FROM product_info", con);

                    //Finding the current values for new order data
                    Int32 custNo = (Int32)countCust.ExecuteScalar() + 4; //Because of test data this number will need to change
                    Int32 orderNo = (Int32)countOrder.ExecuteScalar() + 4;
                    Int32 productNo = (Int32)countProduct.ExecuteScalar() + 1;

                    //Creating a link to the SQL procedures through the command function
                    SqlCommand sqlcmdOrder = new SqlCommand("CreateOrder", con);
                    SqlCommand sqlcmdCustomer = new SqlCommand("CreateCustomer", con);
                    SqlCommand sqlcmdProduct = new SqlCommand("AddProduct", con);
                    SqlCommand sqlcmdItems = new SqlCommand("AddItems", con);
                    sqlcmdOrder.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcmdCustomer.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcmdProduct.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcmdItems.CommandType = System.Data.CommandType.StoredProcedure;

                    //Inputting the relevant data to the commands in relation to the values made in the respective procedures
                    sqlcmdCustomer.Parameters.AddWithValue("@customerID", custNo);
                    sqlcmdCustomer.Parameters.AddWithValue("@fname", txtCustFName.Text.Trim());
                    sqlcmdCustomer.Parameters.AddWithValue("@sname", txtCustSName.Text.Trim());
                    sqlcmdCustomer.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    sqlcmdCustomer.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());

                    sqlcmdProduct.Parameters.AddWithValue("@productID", productNo);
                    sqlcmdProduct.Parameters.AddWithValue("@productName", txtItem.Text.Trim());
                    sqlcmdProduct.Parameters.AddWithValue("@brand", dlBrand.Text.Trim());
                    sqlcmdProduct.Parameters.AddWithValue("@stylecode", txtSCode.Text.Trim());
                    sqlcmdProduct.Parameters.AddWithValue("@spec", txtSpec.Text.Trim());

                    sqlcmdOrder.Parameters.AddWithValue("@orderID", orderNo);
                    sqlcmdOrder.Parameters.AddWithValue("@customerID", custNo);
                    sqlcmdOrder.Parameters.AddWithValue("@date", txtDate);
                    sqlcmdOrder.Parameters.AddWithValue("@paid", txtPaid);
                    sqlcmdOrder.Parameters.AddWithValue("@status", dlOrderStatus.Text.Trim());
                    sqlcmdOrder.Parameters.AddWithValue("@notes", txtOrderNotes.Text.Trim());

                    sqlcmdItems.Parameters.AddWithValue("@orderID", orderNo);
                    sqlcmdItems.Parameters.AddWithValue("@productID", productNo);

                    sqlcmdCustomer.ExecuteNonQuery(); //Executing all the procedures
                    sqlcmdProduct.ExecuteNonQuery();
                    sqlcmdOrder.ExecuteNonQuery();
                    sqlcmdItems.ExecuteNonQuery();

                    con.Close(); //Closing the db connection

                    Server.Transfer("Default.aspx");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Server.Transfer("Default.aspx"); //Taking the user back to the homepage, all inputs will be lost
        }
    }
}
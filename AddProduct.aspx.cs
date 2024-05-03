using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EKhadori
{
    public partial class AddProduct : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EShoppDBConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("SELECT id,Name FROM Categories", con);
                con.Open();
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "id";
                ddlCategory.DataSource = cmd.ExecuteReader();
                ddlCategory.DataBind();

                con.Close();
            }
            else
            {
                // process submitted data
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            insert();
            UploadImage();
            Response.Redirect("ProductsList");
        }

        public void insert()
        {
            string AddProducts = "insert into Products (Name,CategoryId,Price,Description,Image,Quantity,Date) values (@NameCategories,@CategoryId,@Price,@Description,@ImageMainProducts,@Quantity,@Date) ";
            SqlCommand cmd = new SqlCommand(AddProducts, con);
            cmd.Parameters.AddWithValue("@NameCategories", txtName.Text);
            cmd.Parameters.AddWithValue("@CategoryId", ddlCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@Price", Convert.ToInt32(txtPrice.Text));
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);

            if (fuImage.FileName.IsNullOrWhiteSpace())
            {
                cmd.Parameters.AddWithValue("@ImageMainProducts", "unknown_product.png");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImageMainProducts", fuImage.FileName);
            }

            con.Open();
            cmd.ExecuteNonQuery();
            // MsgBox("Data Stored Successfully", MsgBoxStyle.Information, "Success");
            con.Close();
        }

        public void UploadImage()
        {
            if (fuImage.HasFile)
            {
                string fileName = Path.GetFileName(fuImage.PostedFile.FileName);
                fuImage.PostedFile.SaveAs(Server.MapPath("Image/ImageProducts/") + fileName);
                // Response.Redirect(Request.Url.AbsoluteUri);
                // lblAlert.Visible = true;
                // lblAlert.Text = "Success";
            }
        }

    }
}
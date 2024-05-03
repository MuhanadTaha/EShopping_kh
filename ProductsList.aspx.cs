using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace EKhadori
{
    public partial class ProductsList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EShoppDBConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrad();
            }
           
        }

        protected void BindGrad()
        {
            SqlCommand cmd = new SqlCommand("select * from Products", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrad();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrad();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label l1 = GridView1.Rows[e.RowIndex].FindControl("idlbl") as Label;
            TextBox tName = GridView1.Rows[e.RowIndex].FindControl("nametext") as TextBox;
            TextBox tPrice = GridView1.Rows[e.RowIndex].FindControl("pricetext") as TextBox;
            TextBox tDesc = GridView1.Rows[e.RowIndex].FindControl("desctext") as TextBox;
            TextBox tQuantity = GridView1.Rows[e.RowIndex].FindControl("Quantitytxt") as TextBox;
            FileUpload fUpload = GridView1.Rows[e.RowIndex].FindControl("FileUpload1") as FileUpload;

            SqlCommand cmd = new SqlCommand();

            con.Open();
            cmd.Connection = con;
            if (fUpload.FileName.IsNullOrWhiteSpace())
            {
                cmd.CommandText = "update [Products]  set Name = @nm, price = @price, Description = @Description, quantity=@quantity where ID =@id1 ";
            }
            else
            {
                cmd.CommandText = "update [Products]  set Name = @nm, price = @price, Description = @Description, Image=@image, quantity=@quantity where ID =@id1 ";
                cmd.Parameters.AddWithValue("@image", fUpload.FileName);
            }
            cmd.Parameters.AddWithValue("@id1", l1.Text);
            cmd.Parameters.AddWithValue("@nm", tName.Text);
            cmd.Parameters.AddWithValue("@price", tPrice.Text);
            cmd.Parameters.AddWithValue("@Description", tDesc.Text);
            cmd.Parameters.AddWithValue("@quantity", tQuantity.Text);

            cmd.Connection = con;
            UploadImage(fUpload);
            cmd.ExecuteNonQuery();
            GridView1.EditIndex = -1;
            BindGrad();
            con.Close();
        }

        public void UploadImage(FileUpload fUpload)
        {
            if (fUpload.HasFile)
            {
                string fileName = Path.GetFileName(fUpload.PostedFile.FileName);
                fUpload.PostedFile.SaveAs(Server.MapPath("Image/ImageProducts/") + fileName);
                fUpload.Visible = true;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label l1 = GridView1.Rows[e.RowIndex].FindControl("idlbl") as Label;
            SqlCommand cmd = new SqlCommand();

            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from [Products] where ID =@id1 ";
            cmd.Parameters.AddWithValue("@id1", l1.Text);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            GridView1.EditIndex = -1;
            BindGrad();
        }

    }
}
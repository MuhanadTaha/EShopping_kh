using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EKhadori
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EShoppDBConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from signup where email = '" + txtEmail.Text + "' ";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Response.Write("<script>alert('Email Already Exist');</script>");
                con.Close();
            }
            else
            {
                con.Close();
                con.Open();
                cmd = new SqlCommand("insert into signup values('" + txtFirstName.Text + "'  , '" + txtLastName.Text + "' , '" + txtMobile.Text + "' ,  '" + txtDOB.Text + "' ,  '" + txtEmail.Text + "',  '" + txtPassword.Text + "' , '" + txtCPassword.Text + "' , '" + ddlGender.Text + "' ,'customer')", con);

                cmd.ExecuteNonQuery();
                //if (txtEmail.Text == "" || txtPassword.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || txtCPassword.Text == "" || txtDOB.Text == "" || txtMobile.Text == "" || ddlGender.Text == "")
                //{
                //    Response.Write("<script>alert('Please Enter the Details');</script>");
                //}
                //else
                //{
                //    if (txtCPassword.Text != txtPassword.Text)
                //    {
                //        Response.Write("<script>alert('Confirm password is not correct');</script>");
                //    }
                //    else
                //    {
                //        cmd.ExecuteNonQuery();
                //        Response.Write("<script>alert('Successfully');</script>");
                //        Response.Redirect("LogIn.aspx");
                //    }
                //}
                Response.Redirect("LogIn.aspx");
                con.Close();
            }
            con.Close();
        }

    }
}
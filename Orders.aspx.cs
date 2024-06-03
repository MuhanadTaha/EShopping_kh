using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EKhadori
{
    public partial class Orders : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EShoppDBConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                resultTotalLbl.Text = TotalPrice().ToString();
            }
            else
            {
                //process submitted data
            }
        }

        protected void GV_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int crow = Convert.ToInt32(e.CommandArgument.ToString());
            int ID = Convert.ToInt32(GridView1.Rows[crow].Cells[0].Text);
            con.Open();

            if (e.CommandName == "OkCommand")
            {
                Confirmed(ID);
            }

            con.Close();
            Response.Redirect("Orders.aspx");
        }

        protected void Confirmed(int _ID)
        {
            string str = "update orders set arrive = @arrive where idOrder = @ID";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@arrive", "Yes");
            cmd.Parameters.AddWithValue("@ID", _ID);
            cmd.ExecuteNonQuery();
        }

        protected int TotalPrice()
        {
            string str = "SELECT Sum(Price * Count) FROM Orders where BuyerUser = @BuyerUser";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@BuyerUser", Session["Email"]);

            con.Open();
            object result = cmd.ExecuteScalar();
            int totalPrice = (result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            con.Close();

            return totalPrice;
        }

        protected void btnConfime_Click(object sender, EventArgs e)
        {
            string str = "delete from Orders where BuyerUser = @BuyerUser";
            Invoce(); // يجب أن يتم استدعاء Invoce() قبل فتح الاتصال
            int idInvoice = LastId();
            btnAddinHistory(idInvoice);

            con.Open();
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.Parameters.AddWithValue("@BuyerUser", Session["Email"]);
                cmd.ExecuteNonQuery();
            }
            con.Close(); // يجب أن يتم إغلاق الاتصال بعد الاستخدام
            Response.Redirect("~/Orders");
        }


        protected void Invoce()
        {
            if (TotalPrice() > 0)
            {
                string str = "INSERT INTO Invoice ([TotalOrder], [Username], [Date]) values (@TotalOrder, @Username, @Date)";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@TotalOrder", resultTotalLbl.Text);
                cmd.Parameters.AddWithValue("@Username", Session["Email"]);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void btnAddinHistory(int LastIdInvoice)
        {
            string str = "INSERT INTO HistoryOrders ([NameProducts], [NameCategories], [BuyerUser], [Price], [Arrive], [Count] ,[IdInvoice]) SELECT [NameProducts], [NameCategories], [BuyerUser], [Price], [Arrive], [Count] , @LastIdInvoice FROM Orders  WHERE Orders.BuyerUser= @BuyerUser";
            con.Open(); // قم بفتح الاتصال قبل استدعاء ExecuteNonQuery()
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.Parameters.AddWithValue("@LastIdInvoice", LastIdInvoice);
                cmd.Parameters.AddWithValue("@BuyerUser", Session["Email"]);
                cmd.ExecuteNonQuery();
            }
            con.Close(); // تأكد من إغلاق الاتصال بعد الانتهاء من استخدامه
        }


        protected int LastId()
        {
            string str = "SELECT Top(1) id FROM Invoice where username = @Username ORDER BY [DATE] DESC";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@Username", Session["Email"]);
            con.Open();
            int Res = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return Res;
        }

       
    }
}
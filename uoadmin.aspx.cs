using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

public partial class uoadmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ValidateUser(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Users where UserName =@username and Password=@password", con);
        cmd.Parameters.AddWithValue("@username", username.Text);
        cmd.Parameters.AddWithValue("@password", password.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Session["login"] = true;
            Response.Redirect("Details.aspx");
        }
        else
        {
            Session["login"] = false;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Username and Password')</script>");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class remove : System.Web.UI.Page
{
    public static DataTable Rs = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        String ID = "";
        String email = "";

        ID = Request.QueryString["nm"];
        if (ID.Trim() == "")
        {
            Session["Message"] = "Error : Missing ID. <p><a href='http://UoLoot.com'>Return to UoLoot.com</a></p>";
        }

        //get email to effect
        Rs = kaleb.DataLayer.GetData("select * from removal where UUID like '" + ID + "';");
        if (Rs.Rows.Count == 0)
        {
            Session["Message"] = "No request was found for that ID. <p><a href='http://UoLoot.com'>Return to UoLoot.com</a></p>";
            return;
        }
        else
        {
            foreach (DataRow dr in Rs.Rows)
            {
                email = dr["email"].ToString();
                break;
            }
        }

        // are there notifications?
        Rs = kaleb.DataLayer.GetData("select * from notification where email like '" + email + "';");
        if (Rs.Rows.Count == 0)
        {
            Session["Message"] = "There are no active notifications related to this email address : " + email + ".<p><a href = 'http://UoLoot.com'> Return to UoLoot.com </a></p>";
            return;
        }
        else
        {
            string sqlQuery = string.Empty;
            sqlQuery = "delete from notification where email like '" + email + "';";

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    try
                    {
                        sqlCmd.ExecuteNonQuery();
                        Session["Message"] = "All notifications related to the email address [" + email + "] have been removed.<p><a href='http://UoLoot.com'> Return to UoLoot.com </a></p>";                        
                    }
                    catch (Exception ex)
                    {
                        Session["Message"] = "There was an Error, please try again later.<p><a href='http://UoLoot.com'> Return to UoLoot.com </a></p>";
                        return;
                    }
                    sqlConn.Close();
                }
            }

            sqlQuery = "delete from removal where email like '" + email + "';";

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    try
                    {
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Session["Message"] = "There was an Error, please try again later.<p><a href='http://UoLoot.com'> Return to UoLoot.com </a></p>";
                        return;
                    }
                    sqlConn.Close();
                }
            }
        }
    }

    private string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString;
    }
}
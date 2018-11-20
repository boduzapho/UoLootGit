using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace kaleb
{
    public class DataLayer
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                connectionString = value;
            }
        }
        
        public static DataTable GetData(string Sql)
        {
            DataTable RT = new DataTable();
            DataSet DS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString.ToString());
            SqlCommand command = new SqlCommand(Sql, con);
            DA.SelectCommand = command;

            try
            {
                DA.Fill(DS);
                RT = DS.Tables[0];
                return RT;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static DataTable recover(string email)
        {

            DataTable RT = new DataTable();
            DataSet DS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString.ToString());
            string sql = "Select * from users where email=@email";
            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@email", email);
            DA.SelectCommand = command;

            try
            {
                DA.Fill(DS);
                RT = DS.Tables[0];
                return RT;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return null;
            }
        }

        public static Boolean InsertData(string username, string password, string email, string rlusername)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString()))
            {
                connection.Open();
                string sql = "INSERT INTO [dbo].[users] ([username],[password],[email],[enabled],[account],[rlusername]) VALUES (@username,@password,@email,@enabled,@account,@rlusername)";
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {                    
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@enabled", 0);
                    command.Parameters.AddWithValue("@account", "BETA");
                    command.Parameters.AddWithValue("@rlusername", rlusername);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    return false;
                }
            }
        }

        public static Boolean InsertPayment(string ID, string UID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString()))
            {
                connection.Open();
                string sql = "Update [dbo].[users] set [PaymentID] = @PaymentID where id_pk = " + UID;
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    command.Parameters.AddWithValue("@PaymentID", ID);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    return false;
                }
            }
        }

        public static void UpdateEnabled(string ID, string UID)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString()))
            {
                connection.Open();
                string sql = "Update [dbo].[users] set [enabled] = @enabled where id_pk = " + UID;
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    command.Parameters.AddWithValue("@enabled", ID);
                    command.ExecuteNonQuery();
                    return;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    return;
                }
            }
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Details : System.Web.UI.Page
{
    string fileName = "";
    public static DataTable Rs = new DataTable();
    public static DataTable Rs1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["login"] == null) Response.Redirect("uoadmin.aspx");
        bool log = bool.Parse(Session["login"].ToString());
        if (!log) Response.Redirect("uoadmin.aspx");

        if (!IsPostBack)
        {
            message.Text = "";
            message2.Text = "";
            message3.Text = "";
            message4.Text = "";
            Session["Item"] = "";
            Session["Item1"] = "";
            Session["Itemc"] = "";
            Session["Itemcd"] = "";
            Rs = kaleb.DataLayer.GetData("Select * from items where showcase = 1;");
            if (Rs.Rows.Count > 0) // existing?
            {
                foreach (DataRow dr in Rs.Rows)
                {
                    Session["Item1"] = Session["Item1"] + dr["id"].ToString().Trim() + "<br/>";
                }
            }
        }
        else
        {
            Session["Item"] = "";
            Session["Item1"] = "";
            Rs = kaleb.DataLayer.GetData("Select * from items where showcase = 1;");
            if (Rs.Rows.Count > 0) // existing?
            {
                foreach (DataRow dr in Rs.Rows)
                {
                    Session["Item1"] = Session["Item1"] + dr["id"].ToString().Trim() + "<br/>";
                }
            }
        }
    }

    public DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
    {
        try
        {
            ArrayList UniqueRecords = new ArrayList();
            ArrayList DuplicateRecords = new ArrayList();

            // Check if records is already added to UniqueRecords otherwise,
            // Add the records to DuplicateRecords
            foreach (DataRow dRow in table.Rows)
            {
                if (UniqueRecords.Contains(dRow[DistinctColumn]))
                    DuplicateRecords.Add(dRow);
                else
                    UniqueRecords.Add(dRow[DistinctColumn]);
            }

            // Remove dupliate rows from DataTable added to DuplicateRecords
            foreach (DataRow dRow in DuplicateRecords)
            {
                table.Rows.Remove(dRow);
            }

            // Return the clean DataTable which contains unique records.
            return table;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    protected void Upload(object sender, EventArgs e)
    {
        try
        {
            fileName = System.IO.Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Server.MapPath("~/data/") + fileName);
            
            DataTable csvData = GetDataTabletFromCSVFile(Server.MapPath("~/data/") + fileName);

            DataTable UniqueRecords = RemoveDuplicateRows(csvData, "ID");

            InsertDataIntoSQLServerUsingSQLBulkCopy(csvData);
            //LoadDataToDatabase("items", Server.MapPath("~/data/") + fileName, ",");
            if (autoprocess.Checked)
            {
                message.Text = "Upload successful, Please wait while I process the notifications.";
                processnotifications();
            }
            else
            { message.Text = "Upload successful."; }
        }
        catch (Exception ex)
        {
            message.Text = "Upload failed, check your internet connection and server : " + ex.ToString();
        }
    }

    private string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString;
    }

    static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
    {
        using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString))
        {
            dbConnection.Open();
            using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
            {
                s.DestinationTableName = "items";                
                s.ColumnMappings.Add("ID", "ID");
                s.ColumnMappings.Add("Container", "Container");
                s.ColumnMappings.Add("Name", "Name");                
                s.ColumnMappings.Add("Category", "Category");
                s.ColumnMappings.Add("Description", "Description");
                s.WriteToServer(csvFileData);
            }
        }
    }

    private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
    {
        DataTable csvData = new DataTable();
        try
        {
            using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                DataColumn datecolumn = new DataColumn("ID");
                csvData.Columns.Add("ID");

                datecolumn = new DataColumn("Container");
                csvData.Columns.Add("Container");

                datecolumn = new DataColumn("Name");
                csvData.Columns.Add("Name");

                datecolumn = new DataColumn("Category");
                csvData.Columns.Add("Category");

                datecolumn = new DataColumn("Description");
                csvData.Columns.Add("Description");
                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < 4; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }
        }
        catch (Exception ex)
        {
        }
        return csvData;
    }

    private void LoadDataToDatabase(string tableName, string fileFullPath, string delimeter)
    {
        try
        {
            string sqlQuery = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(string.Format("BULK INSERT {0} ", tableName));
            sb.AppendFormat(string.Format(" FROM '{0}'", fileFullPath));
            sb.AppendFormat(string.Format(" WITH ( FIELDTERMINATOR = '{0}' , ROWTERMINATOR = '\n' )", delimeter));
            sqlQuery = sb.ToString();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            message.Text = "Upload and import successful";
        }
        catch (Exception ex)
        {
            message.Text = "There was an error: " + ex.Message;
        }
    }

    protected void RI_Click(object sender, EventArgs e)
    {

        if (ID.Text.Trim() == "") return;

        try
        {
            string sqlQuery = string.Empty;
            sqlQuery = "delete from items where ID like '" + ID.Text + "';";

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            message2.Text = "Item was removed successfuly";
        }
        catch (Exception ex)
        {
            message2.Text = "There was an error: " + ex.Message;
        }
    }

    protected void dbclean_Click(object sender, EventArgs e)
    {
        try
        {
            string sqlQuery = string.Empty;
            sqlQuery = "delete from items;";

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            message3.Text = "Database cleared successful";
        }
        catch (Exception ex)
        {
            message3.Text = "There was an error: " + ex.Message;
        }
    }

    protected string finditems()
    {
        String sql = "";
        Rs = kaleb.DataLayer.GetData(sql);
        if (Rs.Rows.Count > 0) // existing?
        {
            Session["Item"] = "";

            foreach (DataRow dr in Rs.Rows)
            {
                String detarray = "";
                string req = "";

                Char delimiter = '$';
                String[] dets = dr["description"].ToString().Split(delimiter);

                int cnt = 1;
                int totalfound = 0;

                //check here for values
                if (SearchBox.Items.Count > 0)
                {
                    for (int i = 0; i <= SearchBox.Items.Count - 1; i++)
                    {
                        //looping through listbox itemzs
                        string[] itm = SearchBox.Items[i].ToString().Split(':');

                        foreach (var det in dets)
                        {
                            if (det.ToString().Trim() != null)
                            {
                                string ci = det.ToString().Trim().ToLower();

                                if (det.Trim().ToLower().Contains(itm[0].Trim().ToLower()))
                                {
                                    String numbersOnly = Regex.Replace(det, @"[^\d]", String.Empty);
                                    int cin = Int32.Parse(numbersOnly);
                                    int ucin = Int32.Parse(itm[1]);

                                    if (cin >= ucin)
                                    {
                                        totalfound++;
                                    }
                                }
                            }
                        }
                    }
                }

                int yy = SearchBox.Items.Count;
                if (totalfound == yy)
                {
                    foreach (var det in dets)
                    {
                        if (cnt > 2 && cnt < dets.Length)
                        {
                            if (det.ToString().Trim() != null)
                            {
                                int ii = 0;
                                ii = det.ToString().Trim().IndexOf('a', ii);
                                string input = det.ToString().Trim();
                                string output = input.Substring(input.IndexOf('.') + 1);
                                string fill1 = "<span style='color:white;'>";
                                string fill2 = "</span>";

                                if (input.Contains("* Requires"))
                                {
                                    fill1 = "<span style='color:#FF3030; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                    req = "<center>" + fill1 + input + fill2 + "</center>";
                                }

                                if (input.ToLower().Contains("cold"))
                                {
                                    fill1 = "<span style='color:DeepSkyBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }

                                if (input.ToLower().Contains("fire"))
                                {
                                    fill1 = "<span style='color:#CD0000; font-weight:bold; text-shadow: 2px 2px 8px #fff;'>";
                                    fill2 = "</span>";
                                }

                                if (input.ToLower().Contains("mana"))
                                {
                                    fill1 = "<span style='color:Lime ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }
                                if (input.ToLower().Contains("lightning"))
                                {
                                    fill1 = "<span style='color:LightBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }

                                if (req == "") detarray = detarray + "<center>" + fill1 + input + fill2 + "</center>";
                            }
                        }
                        cnt++;
                    }
                    Session["Item"] = Session["Item"] + "<div class='swiper-slide GoldBorder' style='clear:both; background:white; width:400px; height:500px; box - shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                    Session["Item"] = Session["Item"] + "       <div style='color:purple;text-shadow: 2px 2px 8px #000000;'><h4 style='margin:0px;'><center><b>" + dr["Name"].ToString() + "</b></center></h4></div><hr/>";
                    Session["Item"] = Session["Item"] + "       <div style='clear:both;color:black;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                    Session["Item"] = Session["Item"] + "       <div style='clear:both;color:gray; position: fixed; bottom:40px; font-family:ariel;font-size:medium;'>Code: " + dr["id"].ToString() + "</div>";
                    Session["Item"] = Session["Item"] + "</div>";
                }
            }
            return Session["Item"].ToString();
        }
        return "";
    }

    protected void processnotifications()
    {
        Rs = kaleb.DataLayer.GetData("select * from notification;");

        string html = "";
        string email = "";
        string lastitem = "";
        string conditions = "";
        int cntt = 0;
        int Notify = 0;
        int totaln = 0;
        string msg = "";
        int idpk = 0;

        if (Rs.Rows.Count > 0)
        {
            totaln = Rs.Rows.Count;
            foreach (DataRow dr in Rs.Rows)
            {
                email = dr["email"].ToString();
                lastitem = dr["lastitem"].ToString();
                conditions = dr["conditions"].ToString();
                idpk = Int32.Parse(dr["id_pk"].ToString());
                html = "";
                SearchBox.Items.Clear();

                if (!DBNull.Value.Equals(dr["lb"]))
                {
                    string[] lbi = dr["lb"].ToString().Split('~');
                    foreach (string llb in lbi)
                    {
                        SearchBox.Items.Add(llb);
                    }
                }

                cntt = 0;
                Rs1 = kaleb.DataLayer.GetData(conditions);
                if (Rs1.Rows.Count > 0 && cntt < 5)
                {
                    cntt++;
                    foreach (DataRow dr1 in Rs1.Rows)
                    {
                        String detarray = "";
                        string req = "";

                        Char delimiter = '$';
                        String[] dets = dr1["description"].ToString().Split(delimiter);

                        int cnt = 1;
                        int totalfound = 0;

                        if (SearchBox.Items.Count > 0)
                        {
                            for (int i = 0; i <= SearchBox.Items.Count - 1; i++)
                            {
                                //looping through listbox itemzs
                                string[] itm = SearchBox.Items[i].ToString().Split(':');

                                foreach (var det in dets)
                                {
                                    if (det.ToString().Trim() != null)
                                    {
                                        string ci = det.ToString().Trim().ToLower();

                                        if (det.Trim().ToLower().Contains(itm[0].Trim().ToLower()))
                                        {
                                            String numbersOnly = Regex.Replace(det, @"[^\d]", String.Empty);
                                            int cin = Int32.Parse(numbersOnly);
                                            int ucin = Int32.Parse(itm[1]);

                                            if (cin >= ucin)
                                            {
                                                totalfound++;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        int yy = SearchBox.Items.Count;
                        if (totalfound == yy)
                        {
                            foreach (var det in dets)
                            {
                                if (cnt > 2 && cnt < dets.Length)
                                {
                                    if (det.ToString().Trim() != null)
                                    {
                                        int ii = 0;
                                        ii = det.ToString().Trim().IndexOf('a', ii);
                                        string input = det.ToString().Trim();
                                        string output = input.Substring(input.IndexOf('.') + 1);
                                        string fill1 = "<span style='color:white;'>";
                                        string fill2 = "</span>";

                                        if (input.Contains("* Requires"))
                                        {
                                            fill1 = "<span style='color:#FF3030; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                            fill2 = "</span>";
                                            req = "<center>" + fill1 + input + fill2 + "</center>";
                                        }

                                        if (input.ToLower().Contains("cold"))
                                        {
                                            fill1 = "<span style='color:DeepSkyBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                            fill2 = "</span>";
                                        }

                                        if (input.ToLower().Contains("fire"))
                                        {
                                            fill1 = "<span style='color:#CD0000; font-weight:bold; text-shadow: 2px 2px 8px #fff;'>";
                                            fill2 = "</span>";
                                        }

                                        if (input.ToLower().Contains("mana"))
                                        {
                                            fill1 = "<span style='color:Lime ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                            fill2 = "</span>";
                                        }

                                        if (input.ToLower().Contains("lightning"))
                                        {
                                            fill1 = "<span style='color:LightBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                            fill2 = "</span>";
                                        }

                                        if (req == "") detarray = detarray + "<center>" + fill1 + input + fill2 + "</center>";
                                    }
                                }
                                cnt++;
                            }
                            html = html + "<div class='GoldBorder' style='clear:both; margin-left:auto;margin-right:auto; background:white; width:400px; height:500px; box - shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                            html = html + "       <div style='color:purple;text-shadow: 2px 2px 8px #000000;'><h4 style='margin:0px;'><center><b>" + dr1["Name"].ToString() + "</b></center></h4></div><hr/>";
                            html = html + "       <div style='clear:both;color:black;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                            html = html + "       <br /><br /><br /><div style='clear:both;color:gray; position: relative; bottom:40px; font-family:ariel;font-size:medium;'>Code: " + dr1["id"].ToString() + "</div>";
                            html = html + "</div><br /><br />";
                        }
                    }
                }


                if (html.Trim() != "")
                {
                    if (cntt > 4) msg = "<p>We found more than four items, see them all on UoLoot.com.</p><br/>";
                    cntt = 0;
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.html")))
                    {
                        body = reader.ReadToEnd();
                    }

                    body = body.Replace("{UserName}", email);
                    body = body.Replace("{Description}", "Hey! an item you were looking for is now available on UoLoot.com. <br/>(The notification for items like this will be deleted now.)<br/>" + msg + "<br /><hr /><br />" + html);

                    var fromAddress = new MailAddress("uoloot@gmail.com", "UoLoot.com Notifications");
                    var toAddress = new MailAddress(email, "UoLoot.com User");
                    const string fromPassword = "LootimusMaximus";
                    const string subject = "Your Item was found on UoLoot.com";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                    }
                }

                //Remove the record
                try
                {
                    string sqlQuery = string.Empty;
                    sqlQuery = "delete from notification where id_pk like '" + idpk + "';";

                    using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
                    {
                        sqlConn.Open();
                        using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                        {
                            sqlCmd.ExecuteNonQuery();
                            sqlConn.Close();
                        }
                    }

                    Notify++;
                }
                catch (Exception ex)
                {
                }
            }
        }
        message4.Text = Notify.ToString() + " user notifications were satisified out of " + totaln + " notification records on file.";
    }

    protected void process_Click(object sender, EventArgs e)
    {
        processnotifications();
    }

    protected string advancedfinalReturn(string sql, ListBox activeLB)
    {
        string html = "";
        Rs = kaleb.DataLayer.GetData(sql);
        if (Rs.Rows.Count > 0) // existing?
        {
            foreach (DataRow dr in Rs.Rows)
            {
                String detarray = "";
                string req = "";
                int cnt = 1;

                Char delimiter = '$';
                String[] dets = dr["description"].ToString().Split(delimiter);

                int totalfound = 0;

                //check here for values
                if (activeLB.Items.Count > 0)
                {
                    for (int i = 0; i <= activeLB.Items.Count - 1; i++)
                    {
                        //looping through listbox itemzs
                        string[] itm = activeLB.Items[i].ToString().Split(':');

                        foreach (var det in dets)
                        {
                            if (det.ToString().Trim() != null)
                            {
                                string ci = det.ToString().Trim().ToLower();

                                if (det.Trim().ToLower().Contains(itm[0].Trim().ToLower()))
                                {
                                    String numbersOnly = Regex.Replace(det, @"[^\d]", String.Empty);
                                    int cin = Int32.Parse(numbersOnly);
                                    int ucin = Int32.Parse(itm[1]);

                                    if (cin >= ucin)
                                    {
                                        totalfound++;
                                    }
                                }
                            }
                        }
                    }
                }

                int yy = activeLB.Items.Count;
                if (totalfound == yy)
                {
                    foreach (var det in dets)
                    {
                        if (cnt > 2 && cnt < dets.Length)
                        {
                            if (det.ToString().Trim() != null)
                            {
                                int ii = 0;
                                ii = det.ToString().Trim().IndexOf('a', ii);
                                string input = det.ToString().Trim();
                                string output = input.Substring(input.IndexOf('.') + 1);
                                string fill1 = "<span style='color:white;'>";
                                string fill2 = "</span>";

                                if (input.Contains("* Requires"))
                                {
                                    fill1 = "<span style='color:#FF3030; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                    req = "<center>" + fill1 + input + fill2 + "</center>";
                                }

                                if (input.ToLower().Contains("cold"))
                                {
                                    fill1 = "<span style='color:DeepSkyBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }

                                if (input.ToLower().Contains("fire"))
                                {
                                    fill1 = "<span style='color:#CD0000; font-weight:bold; text-shadow: 2px 2px 8px #fff;'>";
                                    fill2 = "</span>";
                                }

                                if (input.ToLower().Contains("mana"))
                                {
                                    fill1 = "<span style='color:Lime ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }
                                if (input.ToLower().Contains("lightning"))
                                {
                                    fill1 = "<span style='color:LightBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }

                                if (req == "") detarray = detarray + "<center>" + fill1 + input + fill2 + "</center>";
                            }
                        }
                        cnt++;
                    }
                    html = html + "<div class='swiper-slide GoldBorder' style='clear:both; background:white; width:400px; height:500px; box - shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                    html = html + "       <div style='color:purple;text-shadow: 2px 2px 8px #000000;'><h4 style='margin:0px;'><center><b>" + dr["Name"].ToString() + "</b></center></h4></div><hr/>";
                    html = html + "       <div style='clear:both;color:black;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                    html = html + "       <div style='clear:both;color:gray; position: relative; bottom:40px; font-family:ariel;font-size:medium;'>Code: " + dr["id"].ToString() + "</div>";
                    html = html + "</div>";
                }
            }
        }
        return html;
    }

    protected void add_Click(object sender, EventArgs e)
    {
        //add the record
        try
        {
            string sqlQuery = string.Empty;
            sqlQuery = "update items set [showcase] = 1 where [ID] like '" + addf.Text.Trim() + "';";

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                    arf.Text = "Item " + removef.Text.Trim() + " was added to the featured list";
                }
            }
        }
        catch (Exception ex)
        {
            arf.Text = "Update failed.";
        }
        Session["Item1"] = "";
        Rs = kaleb.DataLayer.GetData("Select * from items where showcase = 1;");
        if (Rs.Rows.Count > 0) // existing?
        {
            foreach (DataRow dr in Rs.Rows)
            {
                Session["Item1"] = Session["Item1"] + dr["id"].ToString().Trim() + "<br/>";
            }
        }
        addf.Text = "";
    }

    protected void remove_Click(object sender, EventArgs e)
    {
        //Remove the record
        try
        {
            string sqlQuery = string.Empty;
            sqlQuery = "update items set [showcase] = 0 where [ID] like '" + removef.Text.Trim() + "';";

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                    arf.Text = "Item " + removef.Text.Trim() + " was removed from the featured list";
                }
            }
        }
        catch (Exception ex)
        {
            arf.Text = "Update failed.";
        }
        Session["Item1"] = "";
        Rs = kaleb.DataLayer.GetData("Select * from items where showcase = 1;");
        if (Rs.Rows.Count > 0) // existing?
        {
            foreach (DataRow dr in Rs.Rows)
            {
                Session["Item1"] = Session["Item1"] + dr["id"].ToString().Trim() + "<br/>";
            }
        }
        removef.Text = "";
    }

    protected void FindContainer_Click(object sender, EventArgs e)
    {
        if (ItemIDc.Text.Trim() == "") return;

        String sql = "Select * from items where [ID] like '" + ItemIDc.Text.Trim() + "'; ";

        // ItemIDc
        Rs = kaleb.DataLayer.GetData(sql);
        if (Rs.Rows.Count > 0) // existing?
        {
            Session["Itemc"] = "";

            foreach (DataRow dr in Rs.Rows)
            {
                Session["Itemc"] = dr["container"].ToString().Trim();
                break;
            }

            if (Session["Itemc"].ToString().Trim() == "") return;

            Session["Itemcd"] = "";

            Rs = kaleb.DataLayer.GetData("Select * from items where [ID] like '" + ItemIDc.Text.Trim() + "';");
            if (Rs.Rows.Count > 0) // existing?
            {
                int totlr = 1;
                foreach (DataRow dr in Rs.Rows)
                {
                    Char delimiter = '$';
                    String detarray = "";
                    string req = "";
                    String[] dets = dr["description"].ToString().Split(delimiter);
                    int cnt = 1;
                    foreach (var det in dets)
                    {
                        if (cnt > 2 && cnt < dets.Length)
                        {
                            if (det.ToString().Trim() != null)
                            {
                                int i = 0;
                                i = det.ToString().Trim().IndexOf('a', i);
                                string input = det.ToString().Trim();
                                string output = input.Substring(input.IndexOf('.') + 1);
                                string fill1 = "<span style='color:white;'>";
                                string fill2 = "</span>";

                                if (input.Contains("* Requires"))
                                {
                                    fill1 = "<span style='color:gold; font-weight:bold; text-shadow: 2px 2px 8px #000;'>";
                                    fill2 = "</span>";
                                    req = "<center>" + fill1 + input + fill2 + "</center>";
                                }

                                if (input.ToLower().Contains("cold"))
                                {
                                    fill1 = "<span style='color:DeepSkyBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }

                                if (input.ToLower().Contains("fire"))
                                {
                                    fill1 = "<span style='color:#CD0000; font-weight:bold; text-shadow: 2px 2px 8px #fff;'>";
                                    fill2 = "</span>";
                                }

                                if (input.ToLower().Contains("mana"))
                                {
                                    fill1 = "<span style='color:Lime ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }
                                if (input.ToLower().Contains("lightning"))
                                {
                                    fill1 = "<span style='color:LightBlue  ; font-weight:bold; text-shadow: 2px 2px 8px #000000;'>";
                                    fill2 = "</span>";
                                }

                                if (req == "") detarray = detarray + "<center>" + fill1 + input + fill2 + "</center>";
                            }
                        }
                        cnt++;
                    }

                    string graident = "background-image: url('images/treasureitem.jpg'); background-repeat: no-repeat;";
                    Session["Itemcd"] = Session["Itemcd"] + "<div class='swiper-slide GoldBorder' style='clear:both; width:400px; height:500px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                    Session["Itemcd"] = Session["Itemcd"] + "       <div style='padding:10px;color:purple;font-weight:bold;font-size: 30px;text-shadow: rgb(255, 255, 255) 0px 0px 10px; '><span style='margin:0px;'><center>" + dr["Name"].ToString() + "</center></span></div>";
                    Session["Itemcd"] = Session["Itemcd"] + "       <div style='clear:both;line-height: 1.2em;letter-spacing: 0.025em;padding:5px;color:gold;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                    //Session["Itemcd"] = Session["Itemcd"] + "       <div style='clear: both;color: white;position: absolute;left: 0px;bottom: 0px;font-family: ariel;font-size: medium;width: 100%;text-align: center;background-color: darkgoldenrod; '>Code: " + dr["id"].ToString() + "</div>";
                    Session["Itemcd"] = Session["Itemcd"] + "</div>";
                    totlr++;
                }
            }

        }
    }

    protected void Itemr_Click(object sender, EventArgs e)
    {
        try
        {
            string sqlQuery = string.Empty;
            sqlQuery = "delete from items where [ID] like '" + ItemRemove.Text.Trim() + "';";

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.ExecuteNonQuery();
                    sqlConn.Close();
                    removalitem.Text = "Item " + ItemRemove.Text.Trim() + " was removed from the database";
                    ItemRemove.Text = "";
                    Session["Itemc"] = "";
                    ItemIDc.Text = "";
                    Session["Itemcd"] = "";
                }
            }
        }
        catch (Exception ex)
        {
            arf.Text = "Removal failed.";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    public static DataTable Rs = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

        }
        else
        {
            Session["Item"] = "";
            Rs = kaleb.DataLayer.GetData("Select * from items where showcase = 1;");
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
                    Session["Item"] = Session["Item"] + "<div class='swiper-slide GoldBorder' style='clear:both; width:400px; height:620px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                    Session["Item"] = Session["Item"] + "       <div style='padding:10px;color:purple;font-weight:bold;font-size: 30px;text-shadow: rgb(255, 255, 255) 0px 0px 10px; '><span style='margin:0px;'><center>" + dr["Name"].ToString() + "</center></span></div>";
                    Session["Item"] = Session["Item"] + "       <div style='clear:both;line-height: 1.2em;letter-spacing: 0.025em;padding:5px;color:gold;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                    Session["Item"] = Session["Item"] + "       <div style='clear: both;color: white;position: absolute;left: 0px;bottom: 0px;font-family: ariel;font-size: medium;width: 100%;text-align: center;background-color: darkgoldenrod; '>Code: " + dr["id"].ToString() + "</div>";
                    Session["Item"] = Session["Item"] + "</div>";
                    totlr++;
                }
            }
        }
        Session["message"] = "";
    }
}
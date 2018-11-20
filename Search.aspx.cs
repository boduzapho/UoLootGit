using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    public static DataTable Rs = new DataTable();
    public static string sg1 = "";
    public static string sg2 = "";
    public static string sg3 = "";
    public static string sg4 = "";
    public static string sg5 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

        }
        else
        {
            addDDweapons();
            addDDarmor();
            addDDJewelry();
            addDDShields();

            Session["Item"] = "";

            Session["SQL"] = "";
            Session["tot"] = "";

            ast.Text = "Weapons";
            AVSOO(1);
            skillgroups();
            SkillValue.Items.Clear();
            for (int t = 1; t < 21; t++) SkillValue.Items.Add(t.ToString());
        }
        Session["message"] = "";
        Session["regError"] = "";
    }

    protected void skillgroups()
    {
        Rs = kaleb.DataLayer.GetData("Select * from skillg where sgroup = " + (skilllist.SelectedIndex + 1).ToString() + ";");
        if (Rs.Rows.Count > 0) // existing?
        {
            GroupSkills.Items.Clear();
            foreach (DataRow dr in Rs.Rows)
            {
                GroupSkills.Items.Add(dr["skill"].ToString().Trim());
            }
        }
    }

    protected void addDDarmor()
    {
        ddArmor.Items.Add("Physical Resist");
        ddArmor.Items.Add("Fire Resist");
        ddArmor.Items.Add("Cold Resist");
        ddArmor.Items.Add("Poison Resist");
        ddArmor.Items.Add("Energy Resist");
        ddArmor.Items.Add("Mana Regeneration");
        ddArmor.Items.Add("Hit Point Regeneration");
        ddArmor.Items.Add("Stamina Regeneration");
        ddArmor.Items.Add("Mana Increase");
        ddArmor.Items.Add("Stamina Increase");
        ddArmor.Items.Add("Hit Point Increase");
        ddArmor.Items.Add("Lower Mana Cost");
        ddArmor.Items.Add("Lower Reagent Cost");
        ddArmor.Items.Add("Casting Focus");
        ddArmor.Items.Add("Luck");
        ddArmor.Items.Add("Damage Eater");
        ddArmor.Items.Add("Kinetic Eater");
        ddArmor.Items.Add("Fire Eater");
        ddArmor.Items.Add("Cold Eater");
        ddArmor.Items.Add("Poison Eater");
        ddArmor.Items.Add("Energy Eater");
        ddArmor.Items.Add("Self Repair");
        ddArmor.Items.Add("Damage Increase");
        ddArmor.Items.Add("Hit Chance Increase");
        ddArmor.Items.Add("Defense Chance Increase");
        ddArmor.Items.Add("Enhance Potions");
        ddArmor.Items.Add("Strength Bonus");
        ddArmor.Items.Add("Intelligence Bonus");
        ddArmor.Items.Add("Dexterity Bonus");

        for (int t = 1; t < 101; ++t) weaponValue.Items.Add(t.ToString());
    }

    protected void addDDweapons()
    {
        ddWeapons.Items.Add("Physical Resist");
        ddWeapons.Items.Add("Fire Resist");
        ddWeapons.Items.Add("Cold Resist");
        ddWeapons.Items.Add("Poison Resist");
        ddWeapons.Items.Add("Energy Resist");
        ddWeapons.Items.Add("Mana Regeneration");
        ddWeapons.Items.Add("Hit Point Regeneration");
        ddWeapons.Items.Add("Stamina Regeneration");
        ddWeapons.Items.Add("Mana Increase");
        ddWeapons.Items.Add("Stamina Increase");
        ddWeapons.Items.Add("Hit Point Increase");
        ddWeapons.Items.Add("Lower Mana Cost");
        ddWeapons.Items.Add("Luck");
        ddWeapons.Items.Add("Self Repair");
        ddWeapons.Items.Add("Splintering Weapon");
        ddWeapons.Items.Add("Damage Increase");
        ddWeapons.Items.Add("Physical Damage");
        ddWeapons.Items.Add("Fire Damage");
        ddWeapons.Items.Add("Cold Damage");
        ddWeapons.Items.Add("Poison Damage");
        ddWeapons.Items.Add("Energy Damage");
        ddWeapons.Items.Add("Mage Weapon  -");
        ddWeapons.Items.Add("Swing Speed Increase");
        ddWeapons.Items.Add("Hit Chance Increase");
        ddWeapons.Items.Add("Defense Chance Increase");
        ddWeapons.Items.Add("Hit Poison Area");
        ddWeapons.Items.Add("Hit Phyiscal Area");
        ddWeapons.Items.Add("Hit Fire Area");
        ddWeapons.Items.Add("Hit Energy Area");
        ddWeapons.Items.Add("Hit Cold Area");
        ddWeapons.Items.Add("Hit Stamina Leech");
        ddWeapons.Items.Add("Hit Mana Leech");
        ddWeapons.Items.Add("Hit Magic Arrow");
        ddWeapons.Items.Add("Hit Lower Defense");
        ddWeapons.Items.Add("Hit Lower Attack");
        ddWeapons.Items.Add("Hit Lightning");
        ddWeapons.Items.Add("Hit Life Leech");
        ddWeapons.Items.Add("Hit Harm");
        ddWeapons.Items.Add("Hit Fireball");
        ddWeapons.Items.Add("Hit Dispel");
        ddWeapons.Items.Add("Balanced");
        ddWeapons.Items.Add("Velocity");
        ddWeapons.Items.Add("Hit Mana Drain");
        ddWeapons.Items.Add("Hit Fatigue");
        ddWeapons.Items.Add("Faster Casting");
        ddWeapons.Items.Add("Enhance Potions");
        ddWeapons.Items.Add("Strength Bonus");
        ddWeapons.Items.Add("Intelligence Bonus");
        ddWeapons.Items.Add("Dexterity Bonus");

        for (int t = 1; t < 101; ++t) armorValue.Items.Add(t.ToString());
    }

    protected void addDDJewelry()
    {
        ddJewelry.Items.Add("Physical Resist");
        ddJewelry.Items.Add("Fire Resist");
        ddJewelry.Items.Add("Cold Resist");
        ddJewelry.Items.Add("Poison Resist");
        ddJewelry.Items.Add("Energy Resist");
        ddJewelry.Items.Add("Mana Regeneration");
        ddJewelry.Items.Add("Hit Point Regeneration");
        ddJewelry.Items.Add("Stamina Regeneration");
        ddJewelry.Items.Add("Mana Increase");
        ddJewelry.Items.Add("Stamina Increase");
        ddJewelry.Items.Add("Lower Mana Cost");
        ddJewelry.Items.Add("Lower Reagent Cost");
        ddJewelry.Items.Add("Luck");
        ddJewelry.Items.Add("Damage Increase");
        ddJewelry.Items.Add("Swing Speed Increase");
        ddJewelry.Items.Add("Hit Chance Increase");
        ddJewelry.Items.Add("Defense Chance Increase");
        ddJewelry.Items.Add("Spell Damage Increase");
        ddJewelry.Items.Add("Faster Casting");
        ddJewelry.Items.Add("Faster Cast Recovery");
        ddJewelry.Items.Add("Enhance Potions");
        ddJewelry.Items.Add("Strength Bonus");
        ddJewelry.Items.Add("Intelligence Bonus");
        ddJewelry.Items.Add("Dexterity Bonus");

        for (int t = 1; t < 101; ++t) jewelryValue.Items.Add(t.ToString());
    }

    protected void addDDShields()
    {
        ddShields.Items.Add("Physical Resist");
        ddShields.Items.Add("Fire Resist");
        ddShields.Items.Add("Cold Resist");
        ddShields.Items.Add("Poison Resist");
        ddShields.Items.Add("Energy Resist");
        ddShields.Items.Add("Mana Regeneration");
        ddShields.Items.Add("Hit Point Regeneration");
        ddShields.Items.Add("Stamina Regeneration");
        ddShields.Items.Add("Mana Increase");
        ddShields.Items.Add("Stamina Increase");
        ddShields.Items.Add("Hit Point Increase");
        ddShields.Items.Add("Lower Mana Cost");
        ddShields.Items.Add("Luck");
        ddShields.Items.Add("Damage Eater");
        ddShields.Items.Add("Kinetic Eater");
        ddShields.Items.Add("Fire Eater");
        ddShields.Items.Add("Cold Eater");
        ddShields.Items.Add("Poison Eater");
        ddShields.Items.Add("Energy Eater");
        ddShields.Items.Add("Self Repair");
        ddShields.Items.Add("Damage Increase");
        ddShields.Items.Add("Swing Speed Increase");
        ddShields.Items.Add("Hit Chance Increase");
        ddShields.Items.Add("Defense Chance Increase");
        ddShields.Items.Add("Faster Casting");
        ddShields.Items.Add("Enhance Potions");
        ddShields.Items.Add("Strength Bonus");
        ddShields.Items.Add("Intelligence Bonus");
        ddShields.Items.Add("Dexterity Bonus");
        ddShields.Items.Add("Soul Charge");

        for (int t = 1; t < 101; ++t) shieldValue.Items.Add(t.ToString());
    }

    protected void SimpleSearch_Click(object sender, EventArgs e)
    {
        DoSearch("");
    }

    protected void DoSearch(string additional)
    {
        string sql = buildsqlS1("");

        Session["SQL"] = sql + "<br/><hr/>";
        Rs = kaleb.DataLayer.GetData(sql);
        if (Rs.Rows.Count > 0) // existing?
        {
            Session["Item"] = "";
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
                //Session["Item"] = Session["Item"] + "<div class='swiper-slide GoldBorder' style='line-height: 70%;  background:white; width:400px; height:500px; box - shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                //Session["Item"] = Session["Item"] + "       <div style='color:purple;text-shadow: 2px 2px 8px #000000;'><h4 style='margin:0px;'><center><b>" + dr["Name"].ToString() + "</b></center></h4></div><br/>";
                //Session["Item"] = Session["Item"] + "       <div style='line-height: 110%;color:black;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                //Session["Item"] = Session["Item"] + "       <div style='color:gray; position: fixed; bottom:40px; font-family:ariel;font-size:medium;'>Code: " + dr["id"].ToString() + "</div>";
                //Session["Item"] = Session["Item"] + "</div>";
                Session["Item"] = Item(Session["Item"].ToString(), detarray, dr["Name"].ToString(), req, dr["id"].ToString());

                
                totlr++;
            }
        }
        else
        {
            Session["Item"] = "";
            Session["regError"] = "<span style='color:#FF3030; font-weight:bold; font-size: xx-large; text-shadow: 2px 2px 8px #000000;'>No items found</span>";
        }
        LockPane.Visible = false;
    }

    protected void ItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string itm = ItemType.SelectedItem.Text;

        switch (itm)
        {
            case "One-handed":
            case "Two-Handed":
                {
                    ast.Text = "Weapons";
                    AVSOO(1);
                    break;
                }
            case "Arms":
            case "Chest":
            case "Legs":
            case "Gorget":
            case "Helm":
            case "Kilt":
            case "Necklace":
            case "Earrings ":
                {
                    ast.Text = "Armor";
                    AVSOO(2);
                    break;
                }
            case "Ring":
            case "Bracelet":
                {
                    ast.Text = "Jewelry";
                    AVSOO(3);
                    break;
                }
            case "Shield":
                {
                    ast.Text = "Shields";
                    AVSOO(4);
                    break;
                }
        }

        DoSearch("");
    }

    private int whichtab()
    {
        string itm = ItemType.SelectedItem.Text;
        int num = 0;
        switch (itm)
        {
            case "One-handed":
            case "Two-Handed":
                {
                    ast.Text = "Weapons";
                    num = 1;
                    break;
                }
            case "Arms":
            case "Chest":
            case "Legs":
            case "Gorget":
            case "Helm":
            case "Kilt":
            case "Necklace":
            case "Earrings ":
                {
                    ast.Text = "Armor";
                    num = 2;
                    break;
                }
            case "Ring":
            case "Bracelet":
                {
                    ast.Text = "Jewelry";
                    num = 3;
                    break;
                }
            case "Shield":
                {
                    ast.Text = "Shields";
                    num = 4;
                    break;
                }
        }
        return num;
    }

    protected void AVSOO(int id)
    {
        Weapons.Visible = false;
        Armor.Visible = false;
        Jewelry.Visible = false;
        Shields.Visible = false;

        switch (id)
        {
            case 1:
                Weapons.Visible = true;
                break;
            case 2:
                Armor.Visible = true;
                break;
            case 3:
                Jewelry.Visible = true;
                break;
            case 4:
                Shields.Visible = true;
                break;
        }
    }

    protected void weaponAdd_Click(object sender, EventArgs e)
    {
        string itm = ddWeapons.SelectedItem.Text;
        string val = weaponValue.Text.ToString();
        string input = itm + ":" + val;
        string output = Regex.Replace(input, @"[\d-]", string.Empty);

        if (weaponfilters.Items.Count >= 0)
        {
            for (int i = 0; i <= weaponfilters.Items.Count - 1; i++)
            {
                string filter = Regex.Replace(weaponfilters.Items[i].Text, @"[\d-]", string.Empty);
                if (filter.Contains(output))
                    weaponfilters.Items.RemoveAt(i);
            }
        }
        weaponfilters.Items.Add(input);
        //DoSearch();
    }

    protected void armorAdd_Click(object sender, EventArgs e)
    {
        string itm = ddArmor.SelectedItem.Text;
        string val = armorValue.Text.ToString();
        string input = itm + ":" + val;
        string output = Regex.Replace(input, @"[\d-]", string.Empty);

        if (armorfilters.Items.Count >= 0)
        {
            for (int i = 0; i <= armorfilters.Items.Count - 1; i++)
            {
                string filter = Regex.Replace(armorfilters.Items[i].Text, @"[\d-]", string.Empty);
                if (filter.Contains(output))
                    armorfilters.Items.RemoveAt(i);
            }
        }
        armorfilters.Items.Add(input);
        //DoSearch();
    }

    protected void jewelryAdd_Click(object sender, EventArgs e)
    {
        string itm = ddJewelry.SelectedItem.Text;
        string val = jewelryValue.Text.ToString();
        string input = itm + ":" + val;
        string output = Regex.Replace(input, @"[\d-]", string.Empty);

        if (jewelryfilters.Items.Count >= 0)
        {
            for (int i = 0; i <= jewelryfilters.Items.Count - 1; i++)
            {
                string filter = Regex.Replace(jewelryfilters.Items[i].Text, @"[\d-]", string.Empty);
                if (filter.Contains(output))
                    jewelryfilters.Items.RemoveAt(i);
            }
        }
        jewelryfilters.Items.Add(input);
        //DoSearch();
    }

    protected void shieldAdd_Click(object sender, EventArgs e)
    {
        string itm = ddShields.SelectedItem.Text;
        string val = shieldValue.Text.ToString();
        string input = itm + ":" + val;
        string output = Regex.Replace(input, @"[\d-]", string.Empty);

        if (shieldfilters.Items.Count >= 0)
        {
            for (int i = 0; i <= shieldfilters.Items.Count - 1; i++)
            {
                string filter = Regex.Replace(shieldfilters.Items[i].Text, @"[\d-]", string.Empty);
                if (filter.Contains(output))
                    shieldfilters.Items.RemoveAt(i);
            }
        }
        shieldfilters.Items.Add(input);
        //DoSearch();
    }

    protected void removeweaponfilter_Click(object sender, EventArgs e)
    {
        int idx = weaponfilters.SelectedIndex;
        ListItem item = weaponfilters.SelectedItem;
        weaponfilters.Items.Remove(item);
        //DoSearch();
    }

    protected void removearmorfilter_Click(object sender, EventArgs e)
    {
        int idx = armorfilters.SelectedIndex;
        ListItem item = armorfilters.SelectedItem;
        armorfilters.Items.Remove(item);
        //DoSearch();
    }

    protected void removejewelryfilter_Click(object sender, EventArgs e)
    {
        int idx = jewelryfilters.SelectedIndex;
        ListItem item = jewelryfilters.SelectedItem;
        jewelryfilters.Items.Remove(item);
        //DoSearch();
    }

    protected void removeshieldfilter_Click(object sender, EventArgs e)
    {
        int idx = shieldfilters.SelectedIndex;
        ListItem item = shieldfilters.SelectedItem;
        shieldfilters.Items.Remove(item);
        //DoSearch();
    }

    //--------------------------------------------------------------------

    protected string buildsqlS1(string sql)
    {
        int wflag = 1;
        sql = sql = "SELECT * FROM [dbo].[items] where category like '" + ItemType.SelectedItem.Text.Trim() + "'";
        if (!Legendary.Checked && !Major.Checked && !Antique.Checked && !Prized.Checked && !Brittle.Checked)
        { }
        else
        {
            if (Legendary.Checked)
            {
                if (wflag == 1)
                {
                    sql = sql + " and description like '%Legendary Artifact%'";
                    wflag = 2;
                }
                else
                {
                    sql = sql + " or category like '" + ItemType.SelectedItem.Text.Trim() + "' and description like '%Legendary Artifact%'";
                }
            }
            if (Major.Checked)
            {
                if (wflag == 1)
                {
                    sql = sql + " and description like '%Major Artifact%'";
                    wflag = 2;
                }
                else
                {
                    sql = sql + " or category like '" + ItemType.SelectedItem.Text.Trim() + "' and description like '%Major Artifact%'";
                }
            }
            if (Antique.Checked)
            {
                if (wflag == 1)
                {
                    sql = sql + " and description like '%Antique%'";
                    wflag = 2;
                }
                else
                {
                    sql = sql + " or category like '" + ItemType.SelectedItem.Text.Trim() + "' and description like '%Antique%'";
                }
            }
            if (Prized.Checked)
            {
                if (wflag == 1)
                {
                    sql = sql + " and description like '%Prized%'";
                    wflag = 2;
                }
                else
                {
                    sql = sql + " or category like '" + ItemType.SelectedItem.Text.Trim() + "' and description like '%Prized%'";
                }
            }
            if (Brittle.Checked)
            {
                if (wflag == 1)
                {
                    sql = sql + " and description like '%Brittle%'";
                    wflag = 2;
                }
                else
                {
                    sql = sql + " or category like '" + ItemType.SelectedItem.Text.Trim() + "' and description like '%Brittle%'";
                }
            }
        }

        int it = 1;
        if (Weapons.Visible) it = 1;
        if (Armor.Visible) it = 2;
        if (Jewelry.Visible) it = 3;
        if (Shields.Visible) it = 4;

        dochecks(sql, it);
        return sql;
    }

    protected void advancedfinal(string sql, ListBox activeLB)
    {
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
                    //Session["Item"] = Session["Item"] + "<div class='swiper-slide GoldBorder' style=' background:white; width:400px; height:500px; box - shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                    //Session["Item"] = Session["Item"] + "       <div style='color:purple;text-shadow: 2px 2px 8px #000000;'><h4 style='margin:0px;'><center><b>" + dr["Name"].ToString() + "</b></center></h4></div><br/>";
                    //Session["Item"] = Session["Item"] + "       <div style='line-height: 110%;color:black;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                    //Session["Item"] = Session["Item"] + "       <div style='color:gray; position: fixed; bottom:40px; font-family:ariel;font-size:medium;'>Code: " + dr["id"].ToString() + "</div>";
                    //Session["Item"] = Session["Item"] + "</div>";

                    Session["Item"] = Item(Session["Item"].ToString(), detarray, dr["Name"].ToString(), req,dr["id"].ToString());
                    
                }
            }
        }
        else
        {
            Session["regError"] = "<div class='alert-box error'><span>Error: </span>No items found matching your filters.</div>";
            Session["Item"] = "";
        }
        LockPane.Visible = false;
    }

    protected void update_Click(object sender, EventArgs e)
    {
        Button clickedButton = sender as Button;
        ListBox activeLB = null;
        int items = 0;
        if (clickedButton == null)
            return;

        string sql = buildsqlS1("");

        if (clickedButton.ID == "wupdate")
        {
            activeLB = weaponfilters;
            items = 1;
        }
        else if (clickedButton.ID == "aupdate")
        {
            activeLB = armorfilters;
            items = 2;
        }
        else if (clickedButton.ID == "jupdate")
        {
            activeLB = jewelryfilters;
            items = 3;
        }
        else if (clickedButton.ID == "supdate")
        {
            activeLB = shieldfilters;
            items = 4;
        }

        //list box filters
        if (activeLB.Items.Count > 0)
        {
            for (int i = activeLB.Items.Count - 1; i >= 0; i--)
            {
                string[] itm = activeLB.Items[i].ToString().Split(':');
                sql = sql + " and description like '%" + itm[0].ToString().Trim() + "%'";
            }
        }

        sql = dochecks(sql, items);
        advancedfinal(sql, activeLB);
    }

    private string startSQL(int num)
    {
        ListBox activeLB = null;
        if (num < 1 || num > 4) return "";

        string sql = buildsqlS1("");

        if (num == 1) activeLB = weaponfilters;
        else if (num == 2) activeLB = armorfilters;
        else if (num == 3) activeLB = jewelryfilters;
        else if (num == 4) activeLB = shieldfilters;

        //list box filters
        if (activeLB.Items.Count > 0)
        {
            for (int i = activeLB.Items.Count - 1; i >= 0; i--)
            {
                string[] itm = activeLB.Items[i].ToString().Split(':');
                sql = sql + " and description like '%" + itm[0].ToString().Trim() + "%'";
            }
        }
        sql = dochecksFilter(sql, num);
        return sql;
    }

    public static string GetNonNumericData(string input)
    {
        Regex regex = new Regex("[^a-zA-Z0-9- ]");
        return regex.Replace(input, "");
    }

    protected string dochecksFilter(string sql, int id)
    {
        switch (id)
        {
            case 1:
                string wrs = GetNonNumericData(wtext.Text.Trim());
                wtext.Text = wrs;
                if (wSC.Checked) sql = sql + " and description like '%Spell Channeling%'";
                if (wUBWS.Checked) sql = sql + " and description like '%Use Best Weapon Skill%'";
                if (wRP.Checked) sql = sql + " and description like '%Reactive Paralyze%'";
                if (wBL.Checked) sql = sql + " and description like '%Battle Lust%'";
                if (wtitle.Checked) if (wtext.Text.Trim() != "") sql = sql + " and description like '%" + wrs + "%'";
                break;
            case 2:
                string ars = GetNonNumericData(atext.Text.Trim());
                atext.Text = ars;
                if (aNS.Checked) sql = sql + " and description like '%Night Stalker%'";
                if (aMA.Checked) sql = sql + " and description like '%Mage Armor%'";
                if (atitle.Checked) if (atext.Text.Trim() != "") sql = sql + " and description like '%" + ars + "%'";
                break;
            case 3:
                string jrs = GetNonNumericData(jtext.Text.Trim());
                jtext.Text = jrs;
                if (jNS.Checked) sql = sql + " and description like '%Night Sight%'";
                if (jtitle.Checked) if (jtext.Text.Trim() != "") sql = sql + " and description like '%" + jrs + "%'";
                break;
            case 4:
                string srs = GetNonNumericData(stext.Text.Trim());
                stext.Text = srs;
                if (sNS.Checked) sql = sql + " and description like '%Night Sight%'";
                if (sSC.Checked) sql = sql + " and description like '%Spell Channeling%'";
                if (sRP.Checked) sql = sql + " and description like '%Reactive Paralyze%'";
                if (stitle.Checked) if (stext.Text.Trim() != "") sql = sql + " and description like '%" + srs + "%'";
                break;
        }
        return sql;
    }

    protected string dochecks(string sql, int id)
    {
        switch (id)
        {
            case 1:
                string wrs = GetNonNumericData(wtext.Text.Trim());
                wtext.Text = wrs;
                if (wSC.Checked) sql = sql + " and description like '%Spell Channeling%'";
                if (wUBWS.Checked) sql = sql + " and description like '%Use Best Weapon Skill%'";
                if (wRP.Checked) sql = sql + " and description like '%Reactive Paralyze%'";
                if (wBL.Checked) sql = sql + " and description like '%Battle Lust%'";
                if (wtitle.Checked) if (wtext.Text.Trim() != "") sql = sql + " and description like '%" + wrs + "%'";
                break;
            case 2:
                string ars = GetNonNumericData(atext.Text.Trim());
                atext.Text = ars;
                if (aNS.Checked) sql = sql + " and description like '%Night Stalker%'";
                if (aMA.Checked) sql = sql + " and description like '%Mage Armor%'";
                if (atitle.Checked) if (atext.Text.Trim() != "") sql = sql + " and description like '%" + ars + "%'";
                break;
            case 3:
                string jrs = GetNonNumericData(jtext.Text.Trim());
                jtext.Text = jrs;
                if (jNS.Checked) sql = sql + " and description like '%Night Sight%'";
                if (jtitle.Checked) if (jtext.Text.Trim() != "") sql = sql + " and description like '%" + jrs + "%'";
                break;
            case 4:
                string srs = GetNonNumericData(stext.Text.Trim());
                stext.Text = srs;
                if (sNS.Checked) sql = sql + " and description like '%Night Sight%'";
                if (sSC.Checked) sql = sql + " and description like '%Spell Channeling%'";
                if (sRP.Checked) sql = sql + " and description like '%Reactive Paralyze%'";
                if (stitle.Checked) if (stext.Text.Trim() != "") sql = sql + " and description like '%" + srs + "%'";
                break;
        }
        return sql;
    }

    protected void skilllist_SelectedIndexChanged(object sender, EventArgs e)
    {
        skillgroups();
    }

    protected void SkillGroupBtn_Click(object sender, EventArgs e)
    {
        string itm = GroupSkills.SelectedItem.Text;
        string val = SkillValue.Text.ToString();
        string input = itm + ": +" + val;
        string output = Regex.Replace(input, @"[\d-]", string.Empty);

        if (jewelryfilters.Items.Count >= 0)
        {
            string toremove = "";
            if (skilllist.SelectedIndex == 0) toremove = sg1;
            if (skilllist.SelectedIndex == 1) toremove = sg2;
            if (skilllist.SelectedIndex == 2) toremove = sg3;
            if (skilllist.SelectedIndex == 3) toremove = sg4;
            if (skilllist.SelectedIndex == 4) toremove = sg5;

            if (toremove != "")
            {
                for (int i = 0; i <= jewelryfilters.Items.Count - 1; i++)
                {
                    if (jewelryfilters.Items[i].Text.Contains(toremove))
                        jewelryfilters.Items.RemoveAt(i);
                }
            }
        }

        if (skilllist.SelectedIndex == 0) sg1 = input;
        if (skilllist.SelectedIndex == 1) sg2 = input;
        if (skilllist.SelectedIndex == 2) sg3 = input;
        if (skilllist.SelectedIndex == 3) sg4 = input;
        if (skilllist.SelectedIndex == 4) sg5 = input;

        jewelryfilters.Items.Add(input);
    }

    protected void emailsignup_Click(object sender, EventArgs e)
    {
        //first find out how many they already have
        Session["Message"] = "";
        string email = uemaul.Text.Trim();



        if (isValidEmail(email))
        {
            Rs = kaleb.DataLayer.GetData("select * from notification where email like '" + email + "';");
            if (Rs.Rows.Count >= 4)
            {
                Session["Message"] = "You already have four notifications, as they are used they are removed.";
                return;
            }
            else
            {
                int tab = whichtab();
                ListBox activeLB = null;
                if (tab == 1) activeLB = weaponfilters;
                else if (tab == 2) activeLB = armorfilters;
                else if (tab == 3) activeLB = jewelryfilters;
                else if (tab == 4) activeLB = shieldfilters;
                string html = advancedfinalReturn(startSQL(tab), activeLB);
                string sql = startSQL(tab);

                if (activeLB.Items.Count < 1)
                {
                    Session["Message"] = "You must select and add one filter from the drop downs to use this feature.";
                    return;
                }

                string lbi = "";
                for (int i = 0; i <= activeLB.Items.Count - 1; i++)
                {
                    lbi = lbi + activeLB.Items[i].ToString() + "~";
                }
                lbi = lbi.TrimEnd('~');

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString.ToString()))
                {
                    connection.Open();
                    string sql1 = "insert into notification(email, conditions, lastitem, lb) values (@email, @conditions, @lastindex, @lb)";
                    SqlCommand command = new SqlCommand(sql1, connection);
                    try
                    {
                        command.Parameters.AddWithValue("@email", email.Trim());
                        command.Parameters.AddWithValue("@conditions", sql);
                        command.Parameters.AddWithValue("@lastindex", 0);
                        command.Parameters.AddWithValue("@lb", lbi);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Session["Message"] = "Eror : " + ex.ToString();
                        return;
                    }
                }
                Session["Message"] = "The notification was added.";
                return;
            }
        }
        else Session["Message"] = "Please use a valid email address.";
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

                Char delimiter = '$';
                String[] dets = dr["description"].ToString().Split(delimiter);

                int cnt = 1;
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
                    //html = html + "<div class='swiper-slide GoldBorder' style=' background:white; width:400px; height:500px; box - shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
                    //html = html + "       <div style='color:purple;text-shadow: 2px 2px 8px #000000;'><h4 style='margin:0px;'><center><b>" + dr["Name"].ToString() + "</b></center></h4></div><br/>";
                    //html = html + "       <div style='line-height: 110%;color:black;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
                    //html = html + "       <div style='color:gray; position: fixed; bottom:40px; font-family:ariel;font-size:medium;'>Code: " + dr["id"].ToString() + "</div>";
                    //html = html + "</div>";

                    html= Item(html,detarray, dr["Name"].ToString(), req, dr["id"].ToString());
                }
            }
        }
        return html;
    }

    public static bool isValidEmail(string inputEmail)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(inputEmail))
            return (true);
        else
            return (false);
    }

    protected void ClearNotices_Click(object sender, EventArgs e)
    {
        if (uemaul.Text.Trim() == "") return;
        String UUID = Guid.NewGuid().ToString();
        string email = uemaul.Text.Trim();

        //db add
        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["kalebConnectionString"].ConnectionString.ToString()))
        {
            connection.Open();
            string sql1 = "insert into removal(UUID, email) values (@UUID, @email)";
            SqlCommand command = new SqlCommand(sql1, connection);
            try
            {
                command.Parameters.AddWithValue("@UUID", UUID);
                command.Parameters.AddWithValue("@email", email.Trim());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Session["Message"] = "There was an error, please try later.";
                return;
            }
        }

        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", email);
        string URL = "http://Uoloot.com/remove.aspx?nm=" + UUID;

        body = body.Replace("{Description}", "Click or paste this url into your browser to remove all notifications associated with " + email + ". <br/><a href='" + URL + "'>" + URL + "</a><br /><hr /><br />");

        var fromAddress = new MailAddress("uoloot@gmail.com", "UoLoot.com Notifications");
        var toAddress = new MailAddress(email, "UoLoot.com User");
        const string fromPassword = "LootimusMaximus";
        const string subject = "Notification removal request";

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
            try
            {
                smtp.Send(message);
                Session["Message"] = "Please check your email for further instructions.";
            }
            catch (Exception)
            {
                Session["Message"] = "There was an error, please try again later.";
            }
        }
    }

    public string Item(string html, string detarray, string name, string req, string code)
    {
        string html2 = "";
        html2 = html2 + "<div class='swiper-slide GoldBorder' style='clear:both; width:400px; height:620px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>";
        html2 = html2 + "       <div style='padding:10px;color:purple;font-weight:bold;font-size: 30px;text-shadow: rgb(255, 255, 255) 0px 0px 10px; '><span style='margin:0px;'><center>" + name + "</center></span></div>";
        html2 = html2 + "       <div style='clear:both;line-height: 1.2em;letter-spacing: 0.025em;padding:5px;color:gold;background-color:DimGray;'>" + detarray + "</div><br />" + req + "<br />";
        html2 = html2 + "       <div style='clear: both;color: white;position: absolute;left: 0px;bottom: 0px;font-family: ariel;font-size: medium;width: 100%;text-align: center;background-color: darkgoldenrod; '>Code: " + code + "</div>";
        html2 = html2 + "</div>";
        return html + html2;
    }
}
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UoLoot</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--[if lte IE 8]><script src="assets/js/ie/html5shiv.js"></script><![endif]-->
    <link rel="stylesheet" href="assets/css/main.css" />
    <!--[if lte IE 8]><link rel="stylesheet" href="assets/css/ie8.css" /><![endif]-->

    <link href="assets/css/swiper.css" rel="stylesheet" />
    <link href="assets/css/modal.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />

    <script src="assets/js/jquery-1.11.2.min.js"></script>
    <script src="assets/js/jquery-ui-1.11.2.custom.min.js"></script>

    <style type="text/css">
        label {
            display: inline-table;
            vertical-align: middle;
        }
    </style>

</head>
<body class="homepage">
    <form id="form1" runat="server">

        <script src="https://cdnjs.cloudflare.com/ajax/libs/Swiper/3.3.1/js/swiper.jquery.min.js"></script>

        <asp:ScriptManager ID="ScriptManager" EnablePartialRendering="true" runat="server" />

        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                var elem = args.get_postBackElement();
                var lock = document.getElementById('LockPane');
                lock.style.visibility = 'visible';
            }

            function EndRequestHandler(sender, args) {
                var lock = document.getElementById('LockPane');
                lock.style.visibility = 'hidden';
            }
        </script>

        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:kalebConnectionString %>" SelectCommand="SELECT [id_pk], [cName], [imageURL] FROM [Category]"></asp:SqlDataSource>

        <div id="page-wrapper">

            <!-- Header -->
            <div id="header-wrapper" class="wrapper" style="">

                <div id="logo" style="margin: 0 auto; padding-top: 20px;">
                    <h1><a href="Home.aspx">Uo Loot</a></h1>
                    <p>The BEST loot items available!</p>
                </div>

                <div id="header">

                    <!-- Nav -->
                    <nav id="nav">
                        <ul>
                            <li class="current"><a href="Home.aspx">Home</a></li>
                            <li><a href="Search.aspx">Search</a></li>

                        </ul>
                    </nav>

                </div>
            </div>

            <div style="text-align: center; visibility: hidden;" class="LockOn" id="LockPane" runat="server">
                We are processing your request ...
            </div>

            <!-- Intro -->
            <div id="intro-wrapper" class="wrapper style1">
                <div class="title">
                    The Search Tool
                    <a href="#openModal" id="emailbutton" class="box" style="margin-left: 10px; margin-top: 5px;">
                        <div id="email" runat="server">
                            <img src="images/sendemail.png" />
                        </div>
                    </a>
                </div>
                <section id="intro" class="container">
                    <!------------------------------------------------------- guts start -->

                    <asp:UpdatePanel ID="Content" runat="server">
                        <ContentTemplate>
                            <div id="openModal" class="modalDialog">
                                <div style="vertical-align: middle; text-align: center; font-family: Arial, Helvetica, sans-serif; color: #000000">
                                    <a href="#close" title="Close" class="close">X</a>
                                    <h2>Notify Me!</h2>
                                    <p>
                                        Enter your email below to be notified about items matching your current search, up to four are allowed per email address.<br />
                                        <br />
                                        <span style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-size: xx-small; color: #000000">Be sure to add UoLoot@gmail.com to your sopam filters, or you may not see our emails.</span><br />
                                        <span style='font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif; font-size: x-small; font-weight: bold'>WE DOT NOT SHARE YOUR EMAIL INFORMATION WITH ANYONE EVER!</span><br />
                                        <p>
                                            <span style="font-family: Arial, Helvetica, sans-serif; color: #AE0000; font-weight: bold;">Your Email:</span>
                                            <asp:TextBox ID="uemaul" runat="server"></asp:TextBox>
                                            &nbsp;<span style="font-family: Arial, Helvetica, sans-serif; color: #AE0000; font-weight: bold;"> is required for both.</span>
                                        </p>
                                        <p>
                                            <asp:Button ID="emailsignup" runat="server" Text="Click to add the notification" OnClick="emailsignup_Click" />
                                        </p>
                                        <p>
                                            <asp:Button ID="ClearNotices" runat="server" Text="Clear your notices" OnClick="ClearNotices_Click" ForeColor="White" />
                                        </p>
                                </div>
                            </div>

                            <div class="GoldBorder">
                                <table style="vertical-align: middle; text-align: center; font-family: Arial; color: #FFFFFF; width: 100%; display: inline-table; background-color: #232323;">
                                    <tr>
                                        <td>
                                            <table style="vertical-align: middle; text-align: center; font-family: Arial; color: #FFFFFF;">
                                                <tr>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <asp:DropDownList ID="ItemType" runat="server" DataSourceID="SqlDataSource" DataTextField="cName" DataValueField="id_pk" OnSelectedIndexChanged="ItemType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
                                                    <td>
                                                        <asp:CheckBox ID="Legendary" runat="server" Font-Names="Arial" ForeColor="White" Text="Legendary" /></td>
                                                    <td>
                                                        <asp:CheckBox ID="Major" runat="server" Font-Names="Arial" ForeColor="White" Text="Major" /></td>
                                                    <td>
                                                        <asp:CheckBox ID="Antique" runat="server" Font-Names="Arial" ForeColor="White" Text="Antique" /></td>
                                                    <td>
                                                        <asp:CheckBox ID="Prized" runat="server" Font-Names="Arial" ForeColor="White" Text="Prized" /></td>
                                                    <td>
                                                        <asp:CheckBox ID="Brittle" runat="server" Font-Names="Arial" ForeColor="White" Text="Brittle" /></td>
                                                    <td style="text-align: center; vertical-align: middle">
                                                        <asp:Button ID="SimpleSearch" runat="server" Text="Search" OnClick="SimpleSearch_Click" Style="float: right;" ForeColor="White" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                                <table style="width: 100%; vertical-align: middle; text-align: center; font-family: Arial; color: #FFFFFF;">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Weapons" runat="server" Visible="False">
                                                <table>
                                                    <tr>
                                                        <td style="vertical-align: top; display: block">
                                                            <table>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td>Filter <%=ast.Text.ToString().Trim() %>  </td>
                                                                    <td>with a value of </td>
                                                                    <td rowspan="2" style="vertical-align: middle; text-align: left">
                                                                        <asp:Button ID="weaponAdd" runat="server" Text="Add Filter" OnClick="weaponAdd_Click" ForeColor="White" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddWeapons" runat="server"></asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="weaponValue" runat="server"></asp:DropDownList>
                                                                        or better.</td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; text-align: left;">
                                                                    <td style="vertical-align: middle; text-align: left">
                                                                        <asp:CheckBox ID="wtitle" runat="server" />
                                                                        <asp:Label AssociatedControlID="wtitle" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="include a text search."></asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="wtext" runat="server" Width="235px" Style="text-align: left;"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="text-align: left">
                                                                        <asp:CheckBox ID="wSC" runat="server" />
                                                                        <asp:Label AssociatedControlID="wSC" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Spell Channeling"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBox ID="wUBWS" runat="server" />
                                                                        <asp:Label AssociatedControlID="wUBWS" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Use Best Weapon Skill"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBox ID="wRP" runat="server" />
                                                                        <asp:Label AssociatedControlID="wRP" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Reactive Paralyze"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBox ID="wBL" runat="server" />
                                                                        <asp:Label Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Battle Lust" AssociatedControlID="wBL"></asp:Label>

                                                                    </td>
                                                                    <td style="text-align: left; vertical-align: middle;">
                                                                        <asp:Button ID="wupdate" runat="server" Text="Update" OnClick="update_Click" ForeColor="White" />
                                                                    </td>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <hr />
                                                                        </td>
                                                                    </tr>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td colspan="3">
                                                                        <div class="GoldBorder" style="width: 596px; height: 80px; border-width: 0px; text-align: left;">
                                                                            1. Select the filter you want from the drop down list with the attributes.<br />
                                                                            2. Select the value (Number or a percentage, depending on the attribute).<br />
                                                                            3. Lastly click "Add Filter" and the list will update.
                                                                                <br />
                                                                            <span style="font-family: Arial, Helvetica, sans-serif; color: #FF0000; font-weight: bold">Note: You can only specify one of each attribute at a time.</span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td rowspan="2" style="vertical-align: top; text-align: center">
                                                            <asp:ListBox ID="weaponfilters" runat="server" BackColor="Black" Font-Size="Small" ForeColor="#FF9900" Rows="9" Width="251px" Style="border-color: #c7882a;"></asp:ListBox>
                                                            <br />
                                                            <asp:Button ID="removeweaponfilters" runat="server" Text="Remove" Width="251px" OnClick="removeweaponfilter_Click" ForeColor="White" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Armor" runat="server" Visible="False">
                                                <table>
                                                    <tr>
                                                        <td style="vertical-align: top; display: block">
                                                            <table>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td>Filter <%=ast.Text.ToString().Trim() %>  </td>
                                                                    <td>with a value of </td>
                                                                    <td rowspan="2" style="vertical-align: middle; text-align: left">
                                                                        <asp:Button ID="armorAdd" runat="server" Text="Add Filter" OnClick="armorAdd_Click" ForeColor="White" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddArmor" runat="server"></asp:DropDownList></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="armorValue" runat="server"></asp:DropDownList>
                                                                        or better.</td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; text-align: center;">
                                                                    <td style="vertical-align: middle; text-align: left">
                                                                        <asp:CheckBox ID="atitle" runat="server" />
                                                                        <asp:Label AssociatedControlID="atitle" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="include a text search."></asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="atext" runat="server" Width="235px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="text-align: left">
                                                                        <asp:CheckBox ID="aNS" runat="server" />
                                                                        <asp:Label AssociatedControlID="aNS" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Night Stalker"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBox ID="aMA" runat="server" />
                                                                        <asp:Label AssociatedControlID="aMA" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Mage Armor"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Button ID="aupdate" runat="server" Text="Update" OnClick="update_Click" ForeColor="White" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td colspan="3">
                                                                        <div class="GoldBorder" style="width: 596px; height: 80px; border-width: 0px; text-align: left;">
                                                                            1. Select the filter you want from the drop down list with the attributes.<br />
                                                                            2. Select the value (Number or a percentage, depending on the attribute).<br />
                                                                            3. Lastly click "Add Filter" and the list will update.
                                                                                <br />
                                                                            <span style="font-family: Arial, Helvetica, sans-serif; color: #FF0000; font-weight: bold">Note: You can only specify one of each attribute at a time.</span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td rowspan="2" style="vertical-align: top; text-align: center">
                                                            <asp:ListBox ID="armorfilters" runat="server" BackColor="Black" Font-Size="Small" ForeColor="#FF9900" Rows="9" Width="251px" Style="border-color: #c7882a;"></asp:ListBox>
                                                            <br />
                                                            <asp:Button ID="removearmorfilter" runat="server" Text="Remove" Width="251px" OnClick="removearmorfilter_Click" ForeColor="White" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Jewelry" runat="server" Visible="False">
                                                <table>
                                                    <tr>
                                                        <td style="vertical-align: top; display: block">
                                                            <table>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td>Filter <%=ast.Text.ToString().Trim() %>  </td>
                                                                    <td>with a value of </td>
                                                                    <td rowspan="2" style="vertical-align: middle; text-align: left">
                                                                        <asp:Button ID="jewelryAdd" runat="server" Text="Add Filter" OnClick="jewelryAdd_Click" ForeColor="White" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddJewelry" runat="server"></asp:DropDownList>
                                                                        <td>
                                                                            <asp:DropDownList ID="jewelryValue" runat="server"></asp:DropDownList>
                                                                            or better.</td>
                                                                        <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>

                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; text-align: center;">
                                                                    <td style="vertical-align: middle; text-align: left">
                                                                        <asp:CheckBox ID="jtitle" runat="server" />
                                                                        <asp:Label AssociatedControlID="jtitle" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="include a text search."></asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="jtext" runat="server" Width="235px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; text-align: center;">
                                                                    <td style="vertical-align: middle; text-align: left">
                                                                        <asp:CheckBox ID="jNS" runat="server" />
                                                                        <asp:Label AssociatedControlID="jNS" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Night Sight"></asp:Label>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6" style="vertical-align: middle;">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td style="text-align: center; vertical-align: middle;">
                                                                                    <asp:DropDownList ID="skilllist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="skilllist_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="1">Skill Group 1</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Skill Group 2</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Skill Group 3</asp:ListItem>
                                                                                        <asp:ListItem Value="4">Skill Group 4</asp:ListItem>
                                                                                        <asp:ListItem Value="5">Skill group 5</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;&nbsp;<asp:DropDownList ID="GroupSkills" runat="server">
                                                                                    </asp:DropDownList>
                                                                                    &nbsp;
                                                                                <asp:DropDownList ID="SkillValue" runat="server">
                                                                                </asp:DropDownList>&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="SkillGroupBtn" runat="server" Text="Add Skill" OnClick="SkillGroupBtn_Click" ForeColor="White" />
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Button ID="jupdate" runat="server" OnClick="update_Click" Text="Update" ForeColor="White" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td colspan="3">
                                                                        <div class="GoldBorder" style="width: 596px; height: 80px; border-width: 0px; text-align: left;">
                                                                            1. Select the filter you want from the drop down list with the attributes.<br />
                                                                            2. Select the value (Number or a percentage, depending on the attribute).<br />
                                                                            3. Lastly click "Add Filter" and the list will update.
                                                                                <br />
                                                                            <span style="font-family: Arial, Helvetica, sans-serif; color: #FF0000; font-weight: bold">Note: You can only specify one of each attribute at a time.</span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListBox ID="jewelryfilters" runat="server" BackColor="Black" Font-Size="Small" ForeColor="#FF9900" Rows="9" Width="251px" Style="border-color: #c7882a;"></asp:ListBox>
                                                                        <br />
                                                                        <asp:Button ID="removejewelryfilter" runat="server" Text="Remove" Width="251px" OnClick="removejewelryfilter_Click" ForeColor="White" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Panel ID="Shields" runat="server" Visible="False">
                                                <table>
                                                    <tr>
                                                        <td style="vertical-align: top; display: block">
                                                            <table>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td>Filter <%=ast.Text.ToString().Trim() %>  </td>
                                                                    <td>with a value of </td>
                                                                    <td rowspan="2" style="vertical-align: middle; text-align: left">
                                                                        <asp:Button ID="addShield" runat="server" Text="Add Filter" OnClick="shieldAdd_Click" ForeColor="White" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddShields" runat="server"></asp:DropDownList>
                                                                        <td>
                                                                            <asp:DropDownList ID="shieldValue" runat="server"></asp:DropDownList>
                                                                            or better.</td>
                                                                        <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; text-align: center;">
                                                                    <td style="vertical-align: middle; text-align: left">
                                                                        <asp:CheckBox ID="stitle" runat="server" />
                                                                        <asp:Label AssociatedControlID="stitle" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="include a text search."></asp:Label>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="stext" runat="server" Width="235px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; text-align: center;">
                                                                    <td style="vertical-align: middle; text-align: left">
                                                                        <asp:CheckBox ID="sNS" runat="server" />
                                                                        <asp:Label AssociatedControlID="sNS" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Night Sight"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBox ID="sSC" runat="server" />
                                                                        <asp:Label AssociatedControlID="sSC" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Spell Channeling"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBox ID="sRP" runat="server" />
                                                                        <asp:Label AssociatedControlID="sRP" Style="color: #FFFFFF; display: inline-table; vertical-align: middle;" runat="server" Text="Reactive Paralyze"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Button ID="supdate" runat="server" Text="Update" OnClick="update_Click" ForeColor="White" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr style="vertical-align: top; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                                    <td colspan="3">
                                                                        <div class="GoldBorder" style="width: 596px; height: 80px; border-width: 0px; text-align: left;">
                                                                            1. Select the filter you want from the drop down list with the attributes.<br />
                                                                            2. Select the value (Number or a percentage, depending on the attribute).<br />
                                                                            3. Lastly click "Add Filter" and the list will update.
                                                                                <br />
                                                                            <span style="font-family: Arial, Helvetica, sans-serif; color: #FF0000; font-weight: bold">Note: You can only specify one of each attribute at a time.</span>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListBox ID="shieldfilters" runat="server" BackColor="Black" Font-Size="Small" ForeColor="#FF9900" Rows="9" Width="251px" Style="border-color: #c7882a;"></asp:ListBox>
                                                                        <br />
                                                                        <asp:Button ID="removeshieldfilters" runat="server" Text="Remove" Width="251px" OnClick="removejewelryfilter_Click" ForeColor="White" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                </table>
                                <asp:Label ID="ast" runat="server" Text="" Visible="False"></asp:Label>
                                <%=Session["message"].ToString() %>
                            </div>

                            <br />

                            <div id="content">
                                <div class="GoldBorder swiper-container" id="gb" style="background-image: url('images/bk.jpg'); background-repeat: no-repeat; background-attachment: fixed; background-position: -10px -10px; height: 700px; padding-top: 20px;">
                                    <!-- Additional required wrapper -->
                                    <div class="swiper-wrapper">
                                        <%if (Session["regError"].ToString().Trim() != "")
                                            {%> <%=Session["regError"] %> <%} %>
                                        <%if (Session["Item"].ToString().Trim() != "")
                                            {%> <%=Session["Item"] %> <%} %>
                                    </div>
                                    <!-- Add Arrows -->
                                    <div class="swiper-button-next"></div>
                                    <div class="swiper-button-prev"></div>
                                    <div class="swiper-pagination" style="font-family: Arial, Helvetica, sans-serif; color: #fff; vertical-align: bottom; text-align: center; text-shadow: 1px 1px 2px black, 0 0 25px yellow, 0 0 5px gold;"></div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <!------------------------------------------------------- guts end -->
                </section>
            </div>

            <!-- Highlights -->

            <!-- Footer -->
            <div id="footer-wrapper" class="wrapper" style="padding-bottom: 0px; padding-top: 10px;">
                <div class="title">The Rest Of It</div>
                <div id="footer" class="container">

                    <div class="row 150%">

                        <!-- Contact -->
                        <section class="feature-list small">
                            <div class="row">

                                <div class="6u 12u(mobile)">
                                    <section>
                                        <h3 class="icon fa-comment">Social</h3>
                                        <p>
                                            <a href="#">@untitled-corp</a><br />
                                            <a href="#">linkedin.com/untitled</a><br />
                                            <a href="#">facebook.com/untitled</a>
                                        </p>
                                    </section>
                                </div>

                                <div class="6u 12u(mobile)">
                                    <section>
                                        <h3 class="icon fa-envelope">Email</h3>
                                        <p>
                                            <a href="#">info@untitled.tld</a>
                                        </p>
                                    </section>
                                </div>

                            </div>
                        </section>
                    </div>
                </div>
                <div id="copyright">
                    <ul>
                        <li>&copy; 2016</li>
                        <li>Design: <a href="http://logicaldevelopment.solutions">LDS</a></li>
                    </ul>
                </div>
            </div>

            <!-- sticky foot -->
            <div class="foot">
                <img src="assets/css/images/empty-cart-light.png" style="height: 50px" />
                <br />0 Items in Cart
            </div>
        </div>
    </form>
    <!-- Scripts -->
    <script src="assets/Scripts/swiper.min.js"></script>
    <script type="text/javascript">

        function BindControlEvents() {
            MyStuff();
        }

        //Initial bind
        $(document).ready(function () {
            BindControlEvents();
        });

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            BindControlEvents();
        });

        //<![CDATA[
        function pageLoad() { // this gets fired when the UpdatePanel.Update() completes
            MyStuff();
        }

        function applicationUnloadHandler() {
            LockScreen();
        }

        function MyStuff() {
            var swiper = new Swiper('.swiper-container', {
                nextButton: '.swiper-button-next',
                prevButton: '.swiper-button-prev',
                pagination: '.swiper-pagination',
                paginationType: 'fraction',

                effect: 'coverflow',
                grabCursor: true,
                centeredSlides: true,
                slidesPerView: 'auto',
                coverflow: {
                    rotate: 50,
                    stretch: 0,
                    depth: 100,
                    modifier: 1,
                    slideShadows: true
                },
                mousewheelControl: true
            });
        }
        //]]>
    </script>
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/jquery.dropotron.min.js"></script>
    <script src="assets/js/skel.min.js"></script>
    <script src="assets/js/skel-viewport.min.js"></script>
    <script src="assets/js/util.js"></script>
    <!--[if lte IE 8]><script src="assets/js/ie/respond.min.js"></script><![endif]-->
    <script src="assets/js/main.js"></script>
</body>
</html>

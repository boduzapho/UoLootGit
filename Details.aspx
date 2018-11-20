<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-2.2.1.js"></script>
    <link href="css/login.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
            font-size: 10pt;
        }

        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }

        .myButton1 {
            -moz-box-shadow: inset 0px 1px 0px 0px #fff6af;
            -webkit-box-shadow: inset 0px 1px 0px 0px #fff6af;
            box-shadow: inset 0px 1px 0px 0px #fff6af;
            background-color: #ffec64;
            border: 1px solid #ffaa22;
            display: inline-block;
            cursor: pointer;
            color: #333333;
            font-family: Arial;
            font-size: 15px;
            font-weight: bold;
            padding: 6px 24px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #ffee66;
        }

        .myButton {
            -moz-box-shadow: inset 0px 1px 0px 0px #ffffff;
            -webkit-box-shadow: inset 0px 1px 0px 0px #ffffff;
            box-shadow: inset 0px 1px 0px 0px #ffffff;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ffffff), color-stop(1, #f6f6f6));
            background: -moz-linear-gradient(top, #ffffff 5%, #f6f6f6 100%);
            background: -webkit-linear-gradient(top, #ffffff 5%, #f6f6f6 100%);
            background: -o-linear-gradient(top, #ffffff 5%, #f6f6f6 100%);
            background: -ms-linear-gradient(top, #ffffff 5%, #f6f6f6 100%);
            background: linear-gradient(to bottom, #ffffff 5%, #f6f6f6 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffffff', endColorstr='#f6f6f6',GradientType=0);
            background-color: #ffffff;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px;
            border: 1px solid #dcdcdc;
            display: inline-block;
            cursor: pointer;
            color: #666666;
            font-family: Arial;
            font-size: 15px;
            font-weight: bold;
            padding: 6px 24px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #ffffff;
        }

            .myButton:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #f6f6f6), color-stop(1, #ffffff));
                background: -moz-linear-gradient(top, #f6f6f6 5%, #ffffff 100%);
                background: -webkit-linear-gradient(top, #f6f6f6 5%, #ffffff 100%);
                background: -o-linear-gradient(top, #f6f6f6 5%, #ffffff 100%);
                background: -ms-linear-gradient(top, #f6f6f6 5%, #ffffff 100%);
                background: linear-gradient(to bottom, #f6f6f6 5%, #ffffff 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f6f6f6', endColorstr='#ffffff',GradientType=0);
                background-color: #f6f6f6;
            }

            .myButton:active {
                position: relative;
                top: 1px;
            }
    </style>
</head>
<body style="background-color: black;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div style="border: thin solid #FF9900; padding: 20px; margin: 20px;">
            <center>
                <div class="myButton1">Find Item Container by ID</div>
            </center>
            <br />
            <br />
            <span style="font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; font-size: large">Find Item Container by ID</span><br />
            <br />
            <br />

            <asp:TextBox ID="ItemIDc" runat="server" Columns="10" Rows="1" Font-Size="X-Large"></asp:TextBox>&nbsp;

            <%if (Session["Itemc"].ToString().Trim() != "") {%>
            <span style="font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; font-size: x-large">is in container : <%=Session["Itemc"] %></span>
            <%} %>

            <br />
            <br />
            <asp:Button ID="FindContainer" Text="Find container" runat="server" CssClass="myButton" OnClick="FindContainer_Click" />
            <br />
            <br />
            <br />
            <br />
            <asp:TextBox ID="ItemRemove" runat="server" Columns="10" Rows="1" Font-Size="X-Large"></asp:TextBox><br />
            <br />
            <asp:Button ID="Itemr" Text="Remove item" runat="server" CssClass="myButton" OnClick="Itemr_Click" /><br />
            <br />
            <div class="">
                <br />
                <asp:Label ID="removalitem" runat="server" Font-Size="Larger" Font-Names="ariel" Font-Bold="True" ForeColor="Yellow"></asp:Label><br />
            </div>
            <div class="">
                <%if (Session["Itemcd"].ToString().Trim() != "") {%>
                <span style="font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; font-size: x-large"><%=Session["Itemcd"] %></span>
                <%} %>
            </div>
        </div>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modal">
                    <div class="center">
                        <img alt="" src="images/loader.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="border: thin solid #FF9900; padding: 20px; margin: 20px;">
                    <center>
                        <div class="myButton1">Upload and append a CSV file to the database</div>
                    </center>
                    <asp:FileUpload ID="FileUpload1" runat="server" ForeColor="White" />
                    <br />
                    <br />
                    <asp:Button ID="btnUpload" Text="Upload and Import (append)" runat="server" OnClick="Upload" CssClass="myButton" />

                    <div class="">
                        <br />
                        <asp:Label ID="message" runat="server" Text="" Font-Size="Larger" Font-Names="ariel" Font-Bold="True" ForeColor="Yellow"></asp:Label><br />
                    </div>
                    <p>
                        <asp:CheckBox ID="autoprocess" runat="server" Text="Automatically process notifications." ForeColor="White" />
                    </p>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
        <script type="text/javascript">
            window.onsubmit = function () {
                if (Page_IsValid) {
                    var updateProgress = $find("<%= UpdateProgress1.ClientID %>");
                    window.setTimeout(function () {
                        updateProgress.set_visible(true);
                    }, 100);
                }
            }
        </script>
        

        <div style="border: thin solid #FF9900; padding: 20px; margin: 20px;">
            <center>
                <div class="myButton1">Remove an item from the database by ID</div>
            </center>
            <br />
            <asp:TextBox ID="ID" runat="server" MaxLength="10" TextMode="SingleLine" Font-Names="ariel" Font-Size="Larger"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="RI" Text="Remove item" runat="server" CssClass="myButton" OnClick="RI_Click" />

            <div class="">
                <br />
                <asp:Label ID="message2" runat="server" Text="" Font-Size="Larger" Font-Names="ariel" Font-Bold="True" ForeColor="Yellow"></asp:Label><br />
            </div>
        </div>

        <div style="border: thin solid #FF9900; padding: 20px; margin: 20px;">
            <center>
                <div class="myButton1">Clean the database (Warning this will remove all items)</div>
            </center>
            <br />

            <br />
            <asp:Button ID="dbclean" Text="Clean Database" runat="server" CssClass="myButton" OnClick="dbclean_Click" />

            <div class="">
                <br />
                <asp:Label ID="message3" runat="server" Text="" Font-Size="Larger" Font-Names="ariel" Font-Bold="True" ForeColor="Yellow"></asp:Label><br />
            </div>
        </div>

        <div style="border: thin solid #FF9900; padding: 20px; margin: 20px;">
            <center>
                <div class="myButton1">Process all user notifications</div>
            </center>
            <br />

            <br />
            <asp:Button ID="process" Text="Process Notifications" runat="server" CssClass="myButton" OnClick="process_Click" />

            <div class="">
                <br />
                <asp:Label ID="message4" runat="server" Text="" Font-Size="Larger" Font-Names="ariel" Font-Bold="True" ForeColor="Yellow"></asp:Label><br />
            </div>
        </div>

        <div style="border: thin solid #FF9900; padding: 20px; margin: 20px;">
            <center>
                <div class="myButton1">Add and remove items from Featured</div>
            </center>
            <br /><br />
            <span style="font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; font-size: large">Current featured items:</span><br />
            <span style="font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; font-size: x-large"><%=Session["Item1"] %></span>
            <br /><br />
            <asp:TextBox ID="addf" runat="server" Columns="10" Rows="1" Font-Size="X-Large"></asp:TextBox><br /><br />
            <asp:Button ID="add" Text="Add to featured" runat="server" CssClass="myButton" OnClick="add_Click" />
            <br />
            <br />
            <br /><br />
            <asp:TextBox ID="removef" runat="server" Columns="10" Rows="1" Font-Size="X-Large"></asp:TextBox><br/><br />
            <asp:Button ID="remove" Text="Remove from featured" runat="server" CssClass="myButton" OnClick="remove_Click" /><br />
            <br />
            <div class="">
                <br />
                <asp:Label ID="arf" runat="server" Text="" Font-Size="Larger" Font-Names="ariel" Font-Bold="True" ForeColor="Yellow"></asp:Label><br />
            </div>
        </div>

        

        <asp:ListBox ID="SearchBox" runat="server" Visible="False"></asp:ListBox>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uoadmin.aspx.cs" Inherits="uoadmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <link href="css/login.css" rel="stylesheet" />

    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        input[type=text], input[type=password]
        {
            width: 200px;
        }
        table
        {
            border: 1px solid #ccc;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
</head>
<body>
    <div id="wrapper">
    <form id="form1" runat="server" class="login-form">
        <div class="header">
            <h1>Login Form</h1>
            <span>Fill out the form below to login to UoLoot.</span>
        </div>

        <div class="content">
            <asp:TextBox CssClass="input username" placeholder="Username" runat="server" ID="username"></asp:TextBox>            
            <div class="user-icon"></div>
            <asp:TextBox CssClass="input password" placeholder="Password" runat="server" ID="password" TextMode="Password"></asp:TextBox>
            <div class="pass-icon"></div>
        </div>

        <div class="footer">
            <asp:Button ID="submit" CssClass="button" runat="server" Text="Login" OnClick="ValidateUser" />
        </div>
    </form>
    </div>
    <div class="gradient"></div>
</body>
</html>

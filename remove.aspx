<%@ Page Language="C#" AutoEventWireup="true" CodeFile="remove.aspx.cs" Inherits="remove" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UoLoot</title>
    <meta charset="utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/modal.css" rel="stylesheet" />

    <style type="text/css">
        .LockOff {
            display: none;
            visibility: hidden;
        }

        .LockOn {
            display: block;
            visibility: visible;
            position: fixed;
            z-index: 99999;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            background: url('/images/loading2.gif') no-repeat center center;
            background-color: #dcdcdc;
            text-align: center;
            padding-top: 20%;
            filter: alpha(opacity=75);
            opacity: 0.75;
        }

        .counter {
            display: block;
            font-weight: bold;
            margin: 10px auto 0;
            padding: 5px;
            text-align: center;
            width: 60px;
        }

        .bTag {
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ffec64), color-stop(1, #ffab23));
            background: -moz-linear-gradient(top, #ffec64 5%, #ffab23 100%);
            background: -webkit-linear-gradient(top, #ffec64 5%, #ffab23 100%);
            background: -o-linear-gradient(top, #ffec64 5%, #ffab23 100%);
            background: -ms-linear-gradient(top, #ffec64 5%, #ffab23 100%);
            background: linear-gradient(to bottom, #ffec64 5%, #ffab23 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffec64', endColorstr='#ffab23',GradientType=0);
            background-color: #ffec64;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            border: 1px solid #ffaa22;
            display: inline-block;
            cursor: pointer;
            color: #333333;
            font-family: Arial;
            font-size: 10px;
            font-weight: bold;
            padding: 3px 6px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #ffee66;
        }

            .bTag:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #ffab23), color-stop(1, #ffec64));
                background: -moz-linear-gradient(top, #ffab23 5%, #ffec64 100%);
                background: -webkit-linear-gradient(top, #ffab23 5%, #ffec64 100%);
                background: -o-linear-gradient(top, #ffab23 5%, #ffec64 100%);
                background: -ms-linear-gradient(top, #ffab23 5%, #ffec64 100%);
                background: linear-gradient(to bottom, #ffab23 5%, #ffec64 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffab23', endColorstr='#ffec64',GradientType=0);
                background-color: #ffab23;
            }

            .bTag:active {
                position: relative;
                top: 1px;
            }

        #emailbutton {
            top: 692px;
            left: 1314px;
        }

        div.container {
            height: 10em;
            display: flex;
            align-items: center;
            justify-content: center;
        }

            div.container div {
                margin: 0;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="GoldBorder container" style="text-align: center;">
            <%=Session["message"].ToString() %>
        </div>
    </form>
</body>
</html>

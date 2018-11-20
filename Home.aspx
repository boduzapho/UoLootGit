<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="title">Our Featured items</div>
    <div id="highlights" class="container">
        
            <div id="content" style="clear: both;">
                <div class="swiper-container" id="gb" style="height: 700px; padding-top: 20px;">
                    <!-- Additional required wrapper -->
                    <div class="swiper-wrapper">
                        <%if (Session["Item"].ToString().Trim() != "")
                                {%> <%=Session["Item"] %> <%} %>
                    </div>
                    <!-- Add Arrows -->
                    <div class="swiper-button-next"></div>
                    <div class="swiper-button-prev"></div>
                    <div class="swiper-pagination" style="font-family: Arial, Helvetica, sans-serif; color: #fff; vertical-align: bottom; text-align: center; text-shadow: 1px 1px 2px black, 0 0 25px yellow, 0 0 5px gold;"></div>
                </div>            
            </div>
    </div>
</asp:Content>


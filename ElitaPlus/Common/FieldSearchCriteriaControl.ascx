﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FieldSearchCriteriaControl.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.FieldSearchCriteriaControl" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<asp:Label runat="server" ID="moSearchLabel"></asp:Label>
<br />
<asp:DropDownList runat="server" ID="moSearchType" CssClass="exsmall">
</asp:DropDownList>
<asp:TextBox runat="server" ID="moSearchCriteria1"  CssClass="small"  />
<asp:ImageButton ID="imgCalender1" runat="server" Visible="false" Style="vertical-align: bottom"
    ImageUrl="~/App_Themes/Default/Images/calendar.png" />
<asp:Label runat="server" ID="moAndLabel" Style="display: none">and</asp:Label>
<asp:TextBox runat="server" ID="moSearchCriteria2" Style="display: none"  CssClass="small"  />
<asp:ImageButton ID="imgCalender2" runat="server" Visible="false" Style="vertical-align: bottom;
    display: none" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
<script type="text/javascript">$(document).ready(function(){ShowHideControls('#<%= moSearchType.ClientId %>','#<%= moAndLabel.ClientId %>','#<%= moSearchCriteria2.ClientId %>','#<%= imgCalender2.ClientId %>',false);$("#<%= moSearchType.ClientId %>").change(function(){ShowHideControls('#<%= moSearchType.ClientId %>','#<%= moAndLabel.ClientId %>','#<%= moSearchCriteria2.ClientId %>','#<%= imgCalender2.ClientId %>',true);});});</script>

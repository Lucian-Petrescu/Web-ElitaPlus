<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TreeController.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Generic.TreeController" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<iewc:treeview id="moTree" runat="server"
    DefaultStyle="background:none;color#000033;font-family: Verdana,Arial, Helvetica, Verdana, Helvetica, sans-serif;font-size: 11px;"
	SelectedStyle="background:none;color#000033;"
	HoverStyle="background:none;color#000033;text-decoration:underline;"
    BorderStyle="None" 
    CssClass="StdTextOtherCtls" 
	SelectExpands="True"></iewc:treeview>

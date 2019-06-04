<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VendorInventoryFileForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.VendorInventoryFileForm"
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="uc1" TagName="InventoryFileProcessedController" Src="FileProcessedControllerNew.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <uc1:InventoryFileProcessedController ID="moFileController" runat="server"
        ShowCountry="true" ShowCompanyGroup="false" 
        ShowReference="true" ShowDealer="false"
        ShowCompany="false" ReferenceCaption="SELECT_SERVICE_CENTER"
        FileType="VendorInv" FileNameCaption="INVENTORY_FILE"></uc1:InventoryFileProcessedController>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>


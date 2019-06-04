<%@ Page Language="vb" AutoEventWireup="false" Theme="Default"
    CodeBehind="CertRegItemHistoryForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertRegItemHistoryForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo_New.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed"  cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <Elita:UserControlCertificateInfo ID="moCertificateInfoController" runat="server">
            </Elita:UserControlCertificateInfo>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">REGISTER_ITEM_HISTORY</asp:Label>
        </h2>
        <table class="dataGrid" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr id="trPageSize" runat="server" visible="false" >
                <td valign="top" style="text-align: left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Selected="true" Value="10">10</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                    <asp:ListItem Value="35">35</asp:ListItem>
                    <asp:ListItem Value="40">40</asp:ListItem>
                    <asp:ListItem Value="45">45</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                </asp:DropDownList>
            </td>
                <td style="text-align: right;">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
            </tr>
        </table>
        <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="True"
            CellPadding="1" AutoGenerateColumns="False" OnRowCreated="ItemCreated" 
            SkinID="DetailPageGridView">
            <Columns>
             <asp:TemplateField HeaderText="registered_item_name"/>
                 <asp:TemplateField HeaderText="device_Type"/>
                 <asp:TemplateField HeaderText="Item_Desc" />
                 <asp:TemplateField HeaderText="Make" />
                 <asp:TemplateField HeaderText="Model" />
                 <asp:TemplateField HeaderText="purchased_date" />
                 <asp:TemplateField HeaderText="purchase_price" />
                 <asp:TemplateField HeaderText="serial_number" />
                 <asp:TemplateField HeaderText="modified_date"/>
                 <asp:TemplateField HeaderText="item_registration_status" />
                 <asp:TemplateField Visible="False"/>
                 <asp:TemplateField HeaderText="registration_date" />
                 <asp:TemplateField HeaderText="retail_price" />
                 <asp:TemplateField HeaderText="indixid" />
                 <asp:TemplateField HeaderText="expiration_date" />                 
            </Columns>
            <PagerSettings PageButtonCount="15" Mode="Numeric" />
            </asp:GridView>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" SkinID="PrimaryLeftButton" Text="BACK">
        </asp:Button>
    </div>
</asp:Content>
<%@ Page ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="ClaimFulfillmentMilestonesReconWrkForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ClaimFulfillmentMilestonesReconWrkForm" 
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="right" width="10%">
                <asp:Label ID="moDealerNameLabel" runat="server">DEALER_NAME:</asp:Label>
            </td>            
            <td>
                <asp:TextBox ID="moDealerNameText" runat="server" Visible="True" ReadOnly="True"
                    SkinID="MediumTextBox" Enabled="False"></asp:TextBox>
            </td>            
            <td align="right" width="10%">
                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME:</asp:Label>
            </td>            
            <td>
                <asp:TextBox ID="moFileNameText" runat="server" SkinID="MediumTextBox" Visible="True"
                    ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        
    </table>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
   <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_CLAIM_FULFILLMENT_MILESTONES_RECORDS</asp:Label>
        </h2>
       <div>
           <table width="100%" class="dataGrid">
               <tr id="trPageSize" runat="server">
                   <td class="bor" align="left">
                       <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label>
                       &nbsp;
                       <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                                         SkinID="SmallDropDown">
                           <asp:ListItem Value="5">5</asp:ListItem>
                           <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
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
                   <td class="bor" align="right">
                       <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                   </td>
               </tr>
           </table>
       </div>
   <div style="width: 100%">
       <asp:GridView ID="Grid" runat="server" Width="100%" 
                     AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
           <SelectedRowStyle Wrap="true" />
           <EditRowStyle Wrap="true" />
           <AlternatingRowStyle Wrap="true" />
           <RowStyle Wrap="true" />
           <Columns>            
               <asp:BoundField DataField="RecordType" HeaderText="RECORD_TYPE" />
               <asp:BoundField DataField="ExternalAuthorizationNumber" HeaderText="EXTERNAL_AUTHORIZATION_NUMBER" />
               <asp:BoundField DataField="EventType" HeaderText="EVENT_TYPE" />
               <asp:BoundField DataField="Comment" HeaderText="COMMENT" />
               <asp:BoundField DataField="SkuOfDamagedDevice" HeaderText="SKU_OF_DAMAGED_DEVICE" /> 
           </Columns>
           <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
           <PagerStyle HorizontalAlign="Center" />
       </asp:GridView>
   </div>
   <div class="btnZone">           
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
    </div>  
       </div>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimSelectPayables.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimSelectPayables" 
EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="Elita" TagName="FieldSearchCriteriaNumber" Src="~/Common/FieldSearchCriteriaControl.ascx" %>
<asp:Content ID="Header" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
 <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
 <script type="text/javascript" language="javascript">
     window.latestClick = '';
     function isNotDblClick() {
         if (window.latestClick != "clicked") {
             window.latestClick = "clicked";
             return true;
         } else {
             return false;
         }
     }
    </script>
</asp:Content>
<asp:Content ID="Message" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Summary" ContentPlaceHolderID="SummaryPlaceHolder" runat="server" >
    <asp:Panel runat = "server" DefaultButton ="btnSearch">
      <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblServiceCenter">SERVICE_CENTER</asp:Label>
                <br />
                <asp:TextBox ID="moServiceCenter" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblInvoiceGroupNumber">INVOICE_GROUP_NUMBER</asp:Label>
                <br />
                <asp:TextBox ID="moInvoiceGroupNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblInvoiceNumber">INVOICE_NUMBER</asp:Label>
                <br />
                <asp:TextBox ID="moInvoiceNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td>
                 <Elita:FieldSearchCriteriaNumber runat="server" ID="moInvoiceDateRange" DataType="Date" Text="INVOICE_DATE" />
             </td>          
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblAccountNumber">ACCOUNT_NUMBER</asp:Label>
                <br />
                <asp:TextBox ID="moAccountNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>                            
            </td>
            <td>
                <asp:Label runat="server" ID="lblMobileNumber">MOBILE_NUMBER</asp:Label>
                <br />
                <asp:TextBox ID="moMobileNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>                            
            </td> 
            <td>
                <asp:Label runat="server" ID="lblClaimNumber">CLAIM_NUMBER</asp:Label>
                <br />
                <asp:TextBox ID="moClaimNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>                            
            </td>
            <td>
                <asp:Label runat="server" ID="lblAuthNumber">AUTH_NUMBER</asp:Label>
                <br />
                <asp:TextBox ID="moAuthNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>                            
            </td>  
        </tr>
        <tr>
            <td colspan="2">
            </td>
            <td align="left">
                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                </asp:Button>
                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
            </td>
        </tr>
      </table>
      </asp:Panel>    
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<div id="divDataContainer" class="dataContainer" runat="server">
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames=""
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <HeaderStyle />
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblClaimAuthID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("CLAIM_AUTHORIZATION_ID"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" Width="10px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAddClaimAuth" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.SELECTED") %>'></asp:CheckBox>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAllClaimAuths" runat="server"></asp:CheckBox>
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EXCLUDE_DEDUCTIBLE">
                            <HeaderStyle HorizontalAlign="Center" Width="10px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkExcludeDeductible" runat="server" Checked="False"></asp:CheckBox>
                            </ItemTemplate>                            
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SERVICE_CENTER" SortExpression="DESCRIPTION">
                        <ItemTemplate>
                            <asp:Label ID="lblSvcCenterName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIPTION") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CLAIM_NUMBER" SortExpression="CLAIM_NUMBER">
                        <ItemTemplate>
                            <asp:Label ID="lblClaimNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CLAIM_NUMBER") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AUTHORIZATION_NUMBER" SortExpression="AUTHORIZATION_NUMBER">
                        <ItemTemplate>
                            <asp:Label ID="lblAuthorizationNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AUTHORIZATION_NUMBER") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="INVOICE_NUMBER" SortExpression="INVOICE_NUMBER">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INVOICE_NUMBER") %>' />                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="INVOICE_DATE" SortExpression="INVOICE_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# GetDateFormattedStringNullable(DataBinder.Eval(Container, "DataItem.INVOICE_DATE")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TOTAL_AMOUNT" SortExpression="RECONCILED_AMOUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RECONCILED_AMOUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="DUE_DATE" SortExpression="DUE_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceDueDate" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>                
    </div>
     <div class="btnZone">                       
        <asp:Button ID="btnBack" runat="server" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>        
        <asp:Button ID="btnContinue" runat="server" Text="CONTINUE" SkinID="AlternateRightButton" ></asp:Button>
     </div>

     <script type="text/javascript">
         function SelectAll(id) {
             //get reference of GridView control
             var grid = document.getElementById("<%= Grid.ClientID %>");
             //variable to contain the cell of the grid
             var cell;

             if (grid.rows.length > 0) {
                 //loop starts from 1. rows[0] points to the header.
                 for (i = 1; i < grid.rows.length; i++) {
                     //get the reference of first column
                     cell = grid.rows[i].cells[0];

                     //loop according to the number of childNodes in the cell
                     // for (j=0; j<cell.childNodes.length; j++)
                     // {           
                     //if childNode type is CheckBox
                     if (cell.childNodes[0].type == "checkbox") {
                         //assign the status of the Select All checkbox to the cell checkbox within the grid
                         cell.childNodes[0].checked = document.getElementById(id).checked;
                     }
                     // }
                 }
             }
         }
    </script>
</asp:Content>

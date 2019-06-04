<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master" CodeBehind="DealerInvoiceDatesOverwriteForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DealerInvoiceDatesOverwriteForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="Table1" class="searchGrid" border="0" cellpadding="0" cellspacing="0" width= "100%" runat="server">
        <tbody>
           <tr>
                <td align="left" style="width:1%; white-space:nowrap;">
                    <asp:Label ID="Label1" runat="server">Dealer</asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:Label ID="Label2" runat="server">Accounting_Period</asp:Label>
                </td>                       
            </tr>
            <tr>
                <td align="left" style="width:1%; white-space:nowrap; padding-right:30px;">
                    <asp:DropDownList ID="ddlDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td align="left" style="width:1%; white-space:nowrap; padding-right:35px;">
                    <asp:DropDownList ID="ddlAcctPeriodYear" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;"></asp:DropDownList>
                    <asp:DropDownList ID="ddlAcctPeriodMonth" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;">                        
                    </asp:DropDownList>                    
                </td>
                <td align="left">
                    <asp:Button runat="server" ID="btnClear" SkinID="AlternateLeftButton" Text="CLEAR" />
                    <asp:Button runat="server" ID="btnSearch" SkinID="SearchLeftButton" Text="SEARCH" />                    
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="lblResultDatesOnInvoice">SEARCH_RESULTS</asp:Label>: 
            <asp:Label runat="server" ID="lblMappedCnt" Font-Bold="true"></asp:Label>
        </h2>        
        <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" SkinID="DetailPageGridView" >
            <Columns>  
                <asp:TemplateField Visible="True" HeaderText="DEALER">
                    <ItemTemplate>
                        <asp:Label ID="lblDealer" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "DEALER_NAME")%>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>            
                <asp:TemplateField Visible="True" HeaderText="Accounting_Period" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAcctPeriod" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "INVOICE_MONTH")%>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="INVOICE_DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblInvDate" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "InvoiceDate")%>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>                          
                <asp:TemplateField Visible="True" HeaderText="DUE_DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblDueDate" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "DueDate")%>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="MANUAL_INVOICE_DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblManualInvDate" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "ManualInvoiceDate")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtManualInvDate" runat="server" Visible="True" SkinID="SmallTextBox" Width="200px" MaxLength="50" Text='<%# Databinder.Eval(Container.DataItem, "ManualInvoiceDate")%>'></asp:TextBox>
                        <asp:ImageButton ID="imgInvoiceDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </EditItemTemplate>
                </asp:TemplateField>                          
                <asp:TemplateField Visible="True" HeaderText="MANUAL_DUE_DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblManualDueDate" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "ManualDueDate")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtManualDueDate" runat="server" Visible="True" SkinID="SmallTextBox" Width="200px" MaxLength="50" Text='<%# Databinder.Eval(Container.DataItem, "ManualDueDate")%>'></asp:TextBox>
                        <asp:ImageButton ID="imgDueDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Button ID="BtnOverwriteDates_WRITE" runat="server" Text="OVERWRITE_DATES" Visible="true" SkinID="AlternateLeftButton" CommandName="OverwriteDates" CommandArgument='<%#Container.DisplayIndex %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="BtnUpdateInvoice_WRITE" runat="server" Text="UPDATE_INVOICE" Visible="true" SkinID="AlternateLeftButton" CommandName="UpdateInvoice" CommandArgument='<%# Databinder.Eval(Container.DataItem, "AFA_INVOICE_DATA_ID")%>'></asp:Button>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="btnSave" runat="server" CommandName="SaveRecord" Text="Save" EnableViewState="false"></asp:Button>&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="CancelRecord" Text="Cancel" EnableViewState="false"></asp:LinkButton>
                    </EditItemTemplate>                    
                </asp:TemplateField>               
            </Columns>
        </asp:GridView>  
         <input id="HiddenSaveChangesPromptResponse" type="hidden" runat="server" name="HiddenSaveChangesPromptResponse" />       
    </div>
    
</asp:Content>

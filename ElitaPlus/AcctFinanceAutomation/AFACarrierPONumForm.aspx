<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master" CodeBehind="AFACarrierPONumForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AFACarrierPONumForm" %>
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
                    <asp:Label ID="Label2" runat="server">EFFECTIVE_GREATER_THAN</asp:Label>
                </td>                       
            </tr>
            <tr>
                <td align="left" style="width:1%; white-space:nowrap; padding-right:30px;">
                    <asp:DropDownList ID="ddlDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td align="left">
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
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
        </h2>        
        <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>  
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblPONumberID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("AFA_INVOICE_MANUAL_DATA_ID")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>              
                <asp:TemplateField Visible="True" HeaderText="DEALER">
                    <ItemTemplate>
                        <asp:Label ID="lblDealer" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "dealer_name")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGridDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblAcctPeriod" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "Invoice_Month_Desc")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGridAcctPeriodYear" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;" ></asp:DropDownList>
                        <asp:DropDownList ID="ddlGridAcctPeriodMonth" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;" ></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PO_NUMBER">
                    <ItemTemplate>
                        <asp:Label ID="lblPONumber" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "PONumber")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPONumber" runat="server" Visible="True" SkinID="SmallTextBox" MaxLength="20" Text='<%# Databinder.Eval(Container.DataItem, "PONumber")%>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>                          
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditAction"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument='<%#Container.DisplayIndex %>' />&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                            runat="server" CommandName="DeleteRecord" CommandArgument='<%# Container.DisplayIndex %>' />
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
    <div class="btnZone">        
        <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateLeftButton" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"/>
    </div>
</asp:Content>

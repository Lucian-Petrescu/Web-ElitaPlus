<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master" CodeBehind="BluegrassPayCodeByMarketForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BluegrassPayCodeByMarketForm" %>
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
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0" runat="server">
        <tbody>
           <tr>
                <td style="width:1%; white-space:nowrap; padding-right:10px; text-align:left;">
                    <asp:Label ID="Label2" runat="server">EFFECTIVE_GREATER_THAN</asp:Label>:
                    <br />
                    <asp:DropDownList ID="ddlAcctPeriodYear" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;"></asp:DropDownList>
                    <asp:DropDownList ID="ddlAcctPeriodMonth" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:150px;">                        
                    </asp:DropDownList>                    
                </td>
                <td style="width:1%;white-space:nowrap; padding-right:10px;text-align:left;">
                    <asp:Label ID="Label1" runat="server">MARKET_CODE</asp:Label>:
                    <br />
                    <asp:TextBox ID="txtMarketCodeSearch" runat="server" SkinID="SmallTextBox" style="width:250px;"></asp:TextBox>
                </td>                
                <td colspan="2">
                    &nbsp;<br />
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
        <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" SkinID="DetailPageGridView" >
            <Columns>  
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblPayCodeRowID" Text='<%# Databinder.Eval(Container.DataItem, "id") %>' runat="server"></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>              
                <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAcctPeriod" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "InvoiceMonth")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGridAcctPeriodYear" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;" ></asp:DropDownList>
                        <asp:DropDownList ID="ddlGridAcctPeriodMonth" runat="server" SkinID="SmallDropDown" AutoPostBack="False" style="width:100px;" ></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="MARKET_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblMarketCode" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "DataText")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMarketCode" runat="server" Visible="True" SkinID="SmallTextBox" Width="200px" MaxLength="50" Text='<%# Databinder.Eval(Container.DataItem, "DataText")%>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>                          
                <asp:TemplateField Visible="True" HeaderText="AP_PAY_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblAPCode" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "DataText2")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAPCode" runat="server" Visible="True" SkinID="SmallTextBox" Width="200px" MaxLength="50" Text='<%# Databinder.Eval(Container.DataItem, "DataText2")%>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="BCI_PAY_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblBCICode" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "DataText3")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBCICode" runat="server" Visible="True" SkinID="SmallTextBox" Width="200px" MaxLength="50" Text='<%# Databinder.Eval(Container.DataItem, "DataText3")%>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>                          
                <asp:TemplateField Visible="True" HeaderText="KY_PAY_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblKYCode" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "DataText4")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtKYCode" runat="server" Visible="True" SkinID="SmallTextBox" Width="200px" MaxLength="50" Text='<%# Databinder.Eval(Container.DataItem, "DataText4")%>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditAction"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument='<%# Container.DisplayIndex %>' />&nbsp;&nbsp;&nbsp;
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

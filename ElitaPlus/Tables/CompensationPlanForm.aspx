<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompensationPlanForm.aspx.vb" Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CompensationPlanForm"
   EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                <tr>
                    <td runat="server" colspan="2">
                        <table>
                            <tbody>
                                <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moEffectiveLabel" runat="server">Effective</asp:Label>:
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moEffectiveText_WRITE" TabIndex="17" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="BtnEffectiveDate_WRITE" runat="server" mageAlign="AbsMiddle"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moExpirationLabel" runat="server">Expiration</asp:Label>:
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moExpirationText_WRITE" TabIndex="17" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="BtnExpirationDate_WRITE" runat="server" ImageAlign="AbsMiddle"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moCodeLabel" runat="server" Font-Bold="false">Code</asp:Label>:
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moCodeText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moDescriptionLabel" runat="server" Font-Bold="false">Description</asp:Label>:
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moDescription" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel runat="server" ID="CommPlanExtPanel">
            <div class="container">
                <div class="contentZoneHome">
                    <div class="dataContainer">
                        <h2 class="dataGridHeader">COMMISSION_EXTRACT</h2>
                        <table border="0" align="center" width="100%" class="dataGrid">
                            <tr id="trPageSize" runat="server">
                                <td valign="top" align="left">
                                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                            <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
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
                                <td align="right">
                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" OnItemCreated="ItemCreated"
                                AutoGenerateColumns="False" SkinID="DetailPageDataGrid" AllowPaging="True" AllowSorting="True"
                                OnItemCommand="ItemCommand">
                                <SelectedItemStyle Wrap="True"></SelectedItemStyle>
                                <EditItemStyle Wrap="True"></EditItemStyle>
                                <AlternatingItemStyle Wrap="True"></AlternatingItemStyle>
                                <ItemStyle Wrap="True"></ItemStyle>
                                <HeaderStyle></HeaderStyle>
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton Style="cursor: hand;" ID="btnEdit" CommandName="BtnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                runat="server"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="commission_plan_id" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COMMISSION_PLAN_ID"))%>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="comm_plan_extract_id" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COMM_PLAN_EXTRACT_ID"))%>'
                                                runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="CODE" SortExpression="CODE" HeaderText="COMM_PLAN_CODE">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIPTION" SortExpression="DESCRIPTION" HeaderText="COMM_PLAN_DESC">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="AMOUNT_SOURCE_XCD" SortExpression="AMOUNT_SOURCE_XCD" HeaderText="COMM_PAYMT_TYPE">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EXTRACT_TYPE" SortExpression="EXTRACT_TYPE" HeaderText="COMM_EXT_TYPE">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SEQUENCE_NUMBER" SortExpression="SEQUENCE_NUMBER" HeaderText="SEQUENCE_NUMBER">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE" HeaderText="COMM_EFFECTIVE_DATE">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EXPIRATION_DATE" SortExpression="EFFECTIVE_DATE" HeaderText="COMM_EXPIRATION_DATE">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>
                                <PagerStyle PageButtonCount="15" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <table border="0" class="btnZone">
                        <tr>
                            <td align="left">
                                <asp:Button ID="AddCommiPlanExt" runat="server" SkinID="PrimaryLeftButton" Text="ADD_COMMI_PLAN_EXT" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back"></asp:Button>
        <asp:Button ID="btnSave_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" SkinID="AlternateRightButton" Text="Undo"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" SkinID="AlternateRightButton" Text="NEW_WITH_COPY" CausesValidation="False"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="CenterButton" Text="Delete"> </asp:Button>
    </div>        
 </asp:Content>
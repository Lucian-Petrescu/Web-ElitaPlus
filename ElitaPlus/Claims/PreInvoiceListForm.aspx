<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PreInvoiceListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PreInvoiceListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table style="width: 50%;" cellspacing="0" cellpadding="0" border="0" class="searchGrid">
        <tr>
            <td>
                <Elita:MultipleColumnDDLabelControl runat="server" ID="moMultipleColumnDrop" />
                <br />
            </td>
        </tr>
    </table>
    <tabel style="width: 100%">
        <tr id="trSeparator" runat="server">
            <td>
                <hr />
            </td>
        </tr>
    </table>
    <table style="width: 50%; padding-left:2%; " cellspacing="0" cellpadding="0" border="0"
        class="searchGrid">
        <tr>
            <td>
                <asp:Label ID="moBatchNumberLabel" runat="server">BATCH_NUMBER</asp:Label><br />
                <asp:TextBox ID="txtBatchNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblStatus" runat="server">STATUS</asp:Label><br />
                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="moCreatedDateFromLabel" runat="server">CREATED_DATE</asp:Label><asp:Label
                    ID="Label3" runat="server">FROM</asp:Label><br />
                <asp:TextBox ID="txtCreatedDateFrom" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>
                <asp:ImageButton ID="btnCreatedDateFromCert" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                    valign="bottom"></asp:ImageButton>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server">CREATED_DATE</asp:Label><asp:Label ID="Label5"
                    runat="server">TO</asp:Label><br />
                <asp:TextBox ID="txtCreatedDateTo" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>
                <asp:ImageButton ID="btnCreatedDateToCert" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                    valign="bottom"></asp:ImageButton>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td align="right">
                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"
                    OnClientClick="return clearControls();"></asp:Button>
                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            Search results for Pre-invoice</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
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
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                DataKeyNames="Batch_Number" SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Batch_Number" SortExpression="Batch_Number">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditBatchNumber" runat="server" CommandName="SelectAction"
                                CommandArgument="<%#Container.DisplayIndex %>" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="STATUS" SortExpression="STATUS" ReadOnly="true" HeaderText="STATUS"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="CREATED_DATE" SortExpression="CREATED_DATE" ReadOnly="true"
                        HeaderText="CREATED_DATE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="DISPLAY_DATE" SortExpression="DISPLAY_DATE" ReadOnly="true"
                        HeaderText="DISPLAY_DATE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="claims_count" SortExpression="claims_count" ReadOnly="true"
                        HeaderText="#_OF_CLAIMS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="TOTAL_AUTH_AMOUNT" SortExpression="TOTAL_AUTH_AMOUNT" ReadOnly="true"
                        HeaderText="TOTAL_AUTH_AMOUNT" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="BONUS_AMOUNT" SortExpression="BONUS_AMOUNT" ReadOnly="true"
                        HeaderText="TOTAL_BONUS_AMOUNT" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                      <asp:BoundField DataField="DEDUCTIBLE" SortExpression="DEDUCTIBLE" ReadOnly="true"
                        HeaderText="DEDUCTIBLE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="TOTAL_AMOUNT" SortExpression="TOTAL_AMOUNT" ReadOnly="true"
                        HeaderText="TOTAL_AMOUNT" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:TemplateField Visible="False"></asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnNewInvCycle" runat="server" Text="NEW_INVOICING_CYCLE" SkinID="AlternateRightButton">
            </asp:Button>
        </div>
    </div>
    <!-- end new layout -->
    <script language="javascript" type="text/javascript">
        function resizeScroller(item) {
            var browseWidth, browseHeight;

            if (document.layers) {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all) {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600") {
                newHeight = browseHeight - 220;
            }
            else {
                newHeight = browseHeight - 975;
            }

            if (newHeight < 470) {
                newHeight = 470;
            }

            item.style.height = String(newHeight) + "px";

            return newHeight;
        }

        function clearControls() {
            document.getElementById('ctl00_SummaryPlaceHolder_txtBatchNumber').value = "";
            document.getElementById('ctl00_SummaryPlaceHolder_ddlStatus').selectedIndex = 0;
            document.getElementById('ctl00_SummaryPlaceHolder_txtCreatedDateFrom').value = "";
            document.getElementById('ctl00_SummaryPlaceHolder_txtCreatedDateTo').value = "";
            return false;
        }
        //resizeScroller(document.getElementById("scroller"));
    </script>
</asp:Content>

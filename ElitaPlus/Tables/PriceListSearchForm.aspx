<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PriceListSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.PriceListSearchForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
<asp:Content ID="Content1" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <%--<table class="formGrid" id="tblMain1" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="4">
                <uc1:ErrorController ID="moErrorController" runat="server"></uc1:ErrorController>
            </td>
        </tr>
    </table>--%>
</asp:Content>

<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" id="searchTable" runat="server" cellspacing="0" cellpadding="0"
        border="0">
        <tbody>
            <tr>
                <td colspan="3">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCode" runat="server">CODE:</asp:Label><br />
                                    <asp:TextBox ID="txtCode" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblDescription" runat="server">DESCRIPTION:</asp:Label><br />
                                    <asp:TextBox ID="txtDesription" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblServiceType" runat="server">SERVICE_TYPE:</asp:Label><br />
                                    <asp:DropDownList ID="ddlSvcType" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCountry" runat="server">COUNTRY:</asp:Label><br />
                                    <asp:DropDownList ID="ddlCountry" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblSvcCntrName" runat="server">SERVICE_CENTER_NAME:</asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtSvcCntrName" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                                </td>
                                <td>
                                    <span>
                                        <asp:Label ID="lblActiveOnDate" runat="server">ACTIVE_ON:</asp:Label><br />
                                        <asp:TextBox ID="txtActiveOnDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButtonActiveOnDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                            ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </span><span style="padding-left: 10px; white-space: nowrap;">
                                        <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                        </asp:Button>
                                        <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                    </span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            Search results for Price List
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
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
                AllowSorting="true" SkinID="DetailPageGridView">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <HeaderStyle />
                <Columns>
                    <asp:TemplateField HeaderText="CODE" SortExpression="code">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="Select" ID="EditButton_WRITE" runat="server" CausesValidation="False"
                          OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"  Text='<%# Container.DataItem("code")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DESCRIPTION" SortExpression="DESCRIPTION" ReadOnly="true"
                        HeaderText="DESCRIPTION" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="COUNTRY" SortExpression="COUNTRY" ReadOnly="true" HeaderText="COUNTRY"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="VENDORCOUNT" SortExpression="VENDORCOUNT" ReadOnly="true"
                        HeaderText="vendor_Count" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="Status" SortExpression="Status" ReadOnly="true"
                        HeaderText="Status" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="EFFECTIVE" SortExpression="EFFECTIVE" ReadOnly="true"
                        HeaderText="EFFECTIVE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                     <asp:BoundField SortExpression="EXPIRATION" DataField="EXPIRATION" HeaderText="EXPIRATION"
                        ReadOnly="true" HtmlEncode="false" />
                    <%--<asp:BoundField DataField="price_list_id" ReadOnly="true"  HtmlEncode="false" Visible="true"/>--%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblListID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("price_list_id"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <div class="">
            <%--<asp:Button ID="btnAddRecord" runat="server" SkinID="AlternateLeftButton" Text="Add" />--%>
            <asp:Button ID="btnNew" runat="server" CausesValidation="False" SkinID="AlternateLeftButton"
                Text="NEW" />
        </div>
    </div>
</asp:Content>

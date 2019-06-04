<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RepairAndLogisticsListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RepairAndLogisticsListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">

        window.latestClick = '';

        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }

        function comboSelectedServiceCenter(source, eventArgs) {
            //put the selected value in a hidden textbox - runat server so you can read it on postback
            // alert( " Key : "+ eventArgs.get_text() +"  Value :  "+eventArgs.get_value());
            var inpId = document.getElementById('<%=inpServiceCenterId.ClientID%>');
            var inpDesc = document.getElementById('<%=inpServiceCenterDesc.ClientID%>');
            inpId.value = eventArgs.get_value();
            inpDesc.value = eventArgs.get_text();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelSerialNumber" runat="server">SERIAL_NUMBER</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBoxSearchClaimNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSerialNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSearchCustomerName" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelSearchTaxID" runat="server">TAX_ID</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelSearchVerificationNumber" runat="server">VERIFICATION_NUMBER</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelSearchCellPhone" runat="server">CELL_PHONE</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBoxTaxId" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSearchVerificationNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSearchCellPhone" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelAuthorizatioNumber" runat="server">AUTHORIZATION_NUMBER</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelServiceCenter" runat="server">SERVICE_CENTER</asp:Label>:
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBoxSearchAuthNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxServiceCenter" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            <input id="inpServiceCenterId" type="hidden" name="inpServiceCenterId" runat="server" />
                            <input id="inpServiceCenterDesc" type="hidden" name="inpServiceCenterDesc" runat="server" />
                            <cc1:AutoCompleteExtender ID="aCompServiceCenter" OnClientItemSelected="comboSelectedServiceCenter"
                                CompletionSetCount="10" runat="server" TargetControlID="TextBoxServiceCenter"
                                ServiceMethod="PopulateServiceCenterDrop" MinimumPrefixLength='1' CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSortBy" runat="server">Sort By</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="cboSortBy" runat="server" AutoPostBack="False" SkinID="SmallDropDown">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                            </asp:Button>
                            <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            Search results for Claim(s)/Authorization(s)</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
                            SkinID="SmallDropDown">
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
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Claim_Number" SortExpression="clnum">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="SelectActionClaim" ID="btnEditClaim" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Claim_Status" SortExpression="status_code" HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Authorization_Number" SortExpression="authorization_number">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="SelectActionAuthorization" ID="btnEditAuthorization"
                                runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Authorization_Status" SortExpression="claim_authorization_status_id"
                        HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Customer_Name" SortExpression="custnm" HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Tax_Id" SortExpression="taxid" HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cell_Phone" SortExpression="cellnum" HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Serial_Number" SortExpression="sernum" HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Service_Center" SortExpression="service_center" HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Verification_Number" SortExpression="authnum" HtmlEncode="false">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField Visible="False"></asp:BoundField>
                    <asp:BoundField Visible="False"></asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" CodeBehind="ConsentActionListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ConsentActionListForm" %>
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
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="moReferenceTypeLabel" runat="server">REFERENCE_TYPE</asp:Label><br />
                    <asp:DropDownList ID="moReferenceTypeDrop" runat="server" SkinID="MediumDropDown" AutoPostBack="True">  </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="moReferenceValueLabel" runat="server">REFERENCE_VALUE</asp:Label><br />
                    <asp:DropDownList ID="moReferenceValueDrop" runat="server" SkinID="MediumDropDown" AutoPostBack="True">  </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="moConsentTypeLabel" runat="server">CONSENT_TYPE</asp:Label><br />
                    <asp:DropDownList ID="moConsentTypeDrop" runat="server" SkinID="MediumDropDown" AutoPostBack="True">  </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="moConsentFieldNameLabel" runat="server">CONSENT_FIELD_NAME</asp:Label><br />
                    <asp:DropDownList ID="moConsentFieldNameDrop" runat="server" SkinID="MediumDropDown" AutoPostBack="True">  </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;<br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
        </h2>
        <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr id="trPageSize" runat="server">
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label >
                        <asp:Label ID="colonSepertor" runat="server">:</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
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
                    <td align="right" class="bor">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>

        <div>
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="Grid_RowCommand"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="moConsentId" Text='<%# GetGuidStringFromByteArray(Container.DataItem("CONSENT_VALUE_ID"))%>'
                                runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="reference_type" DataField="reference_type" SortExpression="reference_type" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="reference_value" DataField="reference_value" SortExpression="reference_value" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="consent_type" DataField="consent_type_xcd" SortExpression="consent_type_xcd" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="consent_field_name" DataField="consent_field_name_xcd" SortExpression="consent_field_name_xcd" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="field_value" DataField="field_value" SortExpression="field_value" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="effective" DataField="effective" SortExpression="effective" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="expiration" DataField="expiration" SortExpression="expiration" HtmlEncode="false"></asp:BoundField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="btndelete" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                                runat="server"  CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
            </asp:GridView>
        </div>
    </div>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
</asp:Content>

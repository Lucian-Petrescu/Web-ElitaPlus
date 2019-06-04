<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimPaymentGroupListForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimPaymentGroupListForm" EnableSessionState="True"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="Elita" TagName="FieldSearchCriteriaNumber" Src="~/Common/FieldSearchCriteriaControl.ascx" %>
<asp:Content ID="Header" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
 <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" > </script>
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
<asp:Content ID="Message" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Summary" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td class="bor" width="25%">
            <asp:UpdatePanel runat="server" UpdateMode="Always" RenderMode="Inline" ChildrenAsTriggers="true" >
            <ContentTemplate >              
                <table width="100%" border="0">
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="moCountryLabel">COUNTRY</asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlCountryDropDown" runat="server" SkinID="SmallDropDown"  AutoPostBack="true">
                            </asp:DropDownList>
                            <%--<cc1:CascadingDropDown runat="server" ID="ccddCountry" TargetControlID="ddlCountryDropDown"
                                ServiceMethod="GetCountries" Category="GetCountry" PromptText="Select Country">
                            </cc1:CascadingDropDown>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="moSvcCenterLabel">SERVICE_CENTER</asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlSvcCenterDropDown" runat="server" SkinID="MediumDropDown"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <%--<cc1:CascadingDropDown runat="server" ID="ccddSvcCenter" TargetControlID="ddlSvcCenterDropDown"
                                ServiceMethod="GetSvcCentersForCountry" Category="GetSvcCenters" PromptText="Select Service Center">
                            </cc1:CascadingDropDown>--%>
                        </td>
                    </tr>
                </table>
               </ContentTemplate> 
             </asp:UpdatePanel>
            </td>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblPaymentGroupNumber">PAYMENT_GROUP_NUMBER</asp:Label>
                            <br />
                            <asp:TextBox ID="moPaymentGroupNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                        </td> 
                        <td colspan = "2">
                            <Elita:FieldSearchCriteriaNumber runat="server" ID="moPaymentGroupDateRange" DataType="Date" Text="PAYMENT_GROUP_DATE" />
                        </td> 
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="Label1">PAYMENT_GROUP_STATUS</asp:Label>
                            <br />
                            <asp:DropDownList ID="PmntGrpStatusDropDown" runat="server" SkinID="SmallDropDown"
                                AutoPostBack="true">
                            </asp:DropDownList>
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
                    </tr>
                    <tr>
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
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="divDataContainer" class="dataContainer" runat="server">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_PAYMENT_GROUPS" Visible="true" ></asp:Label>
        </h2>
        <div style="width: 100%">
            <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
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
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <HeaderStyle />
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPaymentGroupID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("payment_group_id"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAYMENT_GROUP_NUMBER" SortExpression="PAYMENT_GROUP_NUMBER">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="SelectAction" ID="btnEditPaymentGroup" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAYMENT_GROUP_DATE" SortExpression="PAYMENT_GROUP_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblPymntGroupDate" runat="server" Text='<%# GetDateFormattedString(DataBinder.Eval(Container, "DataItem.PAYMENT_GROUP_DATE")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAYMENT_GROUP_STATUS" SortExpression="PAYMENT_GROUP_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblPymntGroupStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_GROUP_STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAYMENT_GROUP_TOTAL" SortExpression="PAYMENT_GROUP_TOTAL">
                        <ItemTemplate>
                            <asp:Label ID="lblPymntGroupTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_GROUP_TOTAL") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="EXPECTED_PAYMENT_DATE" SortExpression="EXPECTED_PAYMENT_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblExpectedPymntDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EXPECTED_PAYMENT_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="BtnNew_WRITE" runat="server" Text="Add_New" SkinID="AlternateLeftButton">
        </asp:Button>
    </div>
</asp:Content>

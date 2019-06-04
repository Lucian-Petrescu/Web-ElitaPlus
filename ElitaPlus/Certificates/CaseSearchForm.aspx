<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CaseSearchForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CaseSearchForm" 
    MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default"%>

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
                            <asp:Label ID="LabelCompany" runat="server">Company</asp:Label><br />
                            <asp:DropDownList ID="ddlCompanyName" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LabelCaseNumber" runat="server">CASE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxCaseNumber" runat="server" SkinID="MediumTextBox" 
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelCaseStatus" runat="server">CASE_STATUS</asp:Label><br />
                            <asp:DropDownList ID="ddlCaseStatus" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="LabelCallerFirstName" runat="server">CALLER_FIRST_NAME</asp:Label><br />
                            <asp:TextBox ID="TextBoxCallerFirstName" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                         <td>
                            <asp:Label ID="LabelCallerLastName" runat="server">CAllER_LAST_NAME</asp:Label><br />
                              <asp:TextBox ID="TextBoxCallerLastName" runat="server" SkinID="MediumTextBox" 
                                 AutoPostBack="False" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelCasePurpose" runat="server">CASE_PURPOSE</asp:Label><br />
                            <asp:DropDownList ID="ddlCasePurpose" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelCertificateNumber" runat="server">CERTIFICATE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxCertificateNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelCaseClosedReason" runat="server">CASE_CLOSED_REASON</asp:Label><br />
                            <asp:DropDownList ID="ddlCaseClosedReason" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="LabelCaseOpenDateFrom" runat="server">CASE_OPEN_DATE_FROM</asp:Label><br />
                            <asp:TextBox ID="TextBoxCaseOpenDateFrom" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>                           
                                <asp:ImageButton ID="btnCaseOpenDateFrom" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                        </td>
                         <td>
                            <asp:Label ID="LabelCaseOpenDateTo" runat="server">CASE_OPEN_DATE_TO</asp:Label><br />
                             <asp:TextBox ID="TextBoxCaseOpenDateTo" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>                           
                                <asp:ImageButton ID="btnCaseOpenToDateTo" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblcboSearchSortBy" runat="server">Sort By</asp:Label><br />
                            <asp:DropDownList ID="cboSortBy" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
  
                    <tr>
                        <td colspan="3" align="right">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                            </label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_CASE" Visible="true" ></asp:Label>
        </h2>
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
                    <asp:TemplateField HeaderText="case_number" SortExpression="case_number">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="SelectAction" ID="btnEditCase" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="case_open_date"  DataField="case_open_date" SortExpression="case_open_date"  HtmlEncode="false">
                    </asp:BoundField>
                    <asp:BoundField  HeaderText="case_status" DataField="case_status" SortExpression="case_status" HtmlEncode="false">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="case_close_date"  DataField="case_close_date" SortExpression="case_close_date"  HtmlEncode="false">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="first_name"  DataField="first_name" SortExpression="first_name"  HtmlEncode="false">
                    </asp:BoundField>
                    <asp:BoundField  HeaderText="last_name" DataField="last_name" SortExpression="last_name" HtmlEncode="false">
                    </asp:BoundField>
                    <asp:BoundField  HeaderText="channel" DataField="channel" SortExpression="channel" HtmlEncode="false">
                    </asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <!-- end new layout -->
</asp:Content>

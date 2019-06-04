<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AccountingEventListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingEventListForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table class="TABLETITLE" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:&nbsp;<asp:Label
                                    ID="Label7" runat="server"  CssClass="TITLELABELTEXT">ACCOUNTING_EVENT</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
		<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 93%"
		cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4" border="0"> <!--d5d6e4-->
			<tr>
					<td>&nbsp;</td>
			</tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
							<tr>
                                <td height="1">
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" >
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="2">
                                                <table class="TABLESEARCH" id="tblSearch" cellspacing="0" cellpadding="6" align="center"
                                                    border="0">
                                                    <tr>
                                                        <td height="100%" valign="top">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td align="center" colspan="3">
                                                                        <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left" width="33%">
                                                                        <asp:Label ID="lblDescription" runat="server">ACCOUNTING_COMPANY:</asp:Label></td>
                                                                    <td nowrap align="left" width="33%">
                                                                        <asp:Label ID="lblCode" runat="server">ACCOUNTING_EVENT:</asp:Label></td>
                                                                    <td nowrap align="left" width="33%">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <asp:DropDownList ID="moAccountingCompanyDropDown" runat="server" AutoPostBack="false"
                                                                            Width="95%">
                                                                        </asp:DropDownList></td>
                                                                    <td nowrap align="left">
                                                                        <asp:DropDownList ID="moAccountingEventTypeDropDown" runat="server" AutoPostBack="false"
                                                                            Width="95%">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="5px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td nowrap align="right">
                                                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            CssClass="FLATBUTTON" Width="90px" Height="20px" Text="Clear"></asp:Button>&nbsp;
                                                                        <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            CssClass="FLATBUTTON" Width="90px" Height="20px" Text="Search"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr style="height: 1px">
                                            </td>
                                        </tr>
                                        <tr id="trPageSize" runat="server">
                                            <td style="height: 22px" valign="top" align="left">
                                                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                                                <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
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
                                                </asp:DropDownList></td>
                                            <td style="height: 22px" align="right">
                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView id="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
														AllowPaging="True" AllowSorting="True"  CellPadding="1" AutoGenerateColumns="False" CssClass="DATAGRID">
														<SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
														 <EditRowStyle CssClass="EDITROW"></EditRowStyle>
														<AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
														<RowStyle CssClass="ROW"></RowStyle>
                                                         <HeaderStyle CssClass="HEADER"></HeaderStyle>    
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnView" runat="server" CausesValidation="False"
                                                                            CommandName="SelectAction" ImageUrl="~/Navigation/images/icons/edit2.gif" 
                                                                            CommandArgument="<%#Container.DisplayIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>                                                        
                                                        <asp:TemplateField  HeaderText="ACCOUNTING_EVENT"></asp:TemplateField>
                                                        <asp:TemplateField Visible="False"></asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
														<PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                </asp:GridView></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="bottom">
                                    <hr style="width: 100%; height: 1px" size="1" />
                                    <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        CssClass="FLATBUTTON" Width="100px" Height="20px" Text="New" CommandName="WRITE">
                                    </asp:Button>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

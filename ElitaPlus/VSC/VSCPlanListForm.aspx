<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VSCPlanListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.VSCPlanListForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>VSC_Plan</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:&nbsp;<asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">VSC_PLAN</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="WorkingPanel" runat="server">
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
            border="0">
            <!--d5d6e4-->
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                   
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            height="98%" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                            bgcolor="#fef9ea" border="0">
                            <tr>
                                <td align="center">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="2">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid; height: 50px"
                                                    cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#f1f1f1"
                                                    border="0">
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellspacing="2" cellpadding="2" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="white-space: nowrap" align="left" colspan="4">&nbsp;
																				<div style="width: 73.3%; height: 39px" align="center">
                                                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                                </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="white-space: nowrap" align="right">
                                                                            <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                runat="server" CssClass="FLATBUTTON" Font-Bold="false" Width="90px" Text="Clear"></asp:Button>&nbsp;
																				<asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                    runat="server" CssClass="FLATBUTTON" Font-Bold="false" Width="90px" Text="Search"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
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
                                        </asp:DropDownList></td>
                                <td style="height: 22px" align="right">
                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" colspan="2">
                                    <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#DEE3E7"
                                        BorderColor="#999999" BorderStyle="Solid" CellPadding="1" BorderWidth="1px" AllowPaging="True"
                                        AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
                                        <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
                                        <EditItemStyle Wrap="False"></EditItemStyle>
                                        <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                        <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                        <HeaderStyle></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                        runat="server" CommandName="SelectAction"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn SortExpression="CODE" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn SortExpression="DESCRIPTION" HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Center" Width="65%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn Visible="False" HeaderText="id"></asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
                                            Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                        runat="server" CssClass="FLATBUTTON" Font-Bold="false" Width="100px" Text="New" Height="20px"
                        CommandName="WRITE"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        </asp:panel>
    </form>
   
</body>
</html>

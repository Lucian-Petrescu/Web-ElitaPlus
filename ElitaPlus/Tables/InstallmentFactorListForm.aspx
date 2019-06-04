<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InstallmentFactorListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InstallmentFactorListForm" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout"
    border="0">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table class="TABLETITLE" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="TablesLabel" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABELTEXT">INSTALLMENT_FACTOR</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td style="height: 8px">
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                            cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td valign="top" height="1">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" width="100%">
                                    <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                                                    height: 76px" cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#f1f1f1"
                                                    border="0">
                                                    <!--fef9ea-->
                                                    <tr>
                                                        <td align="center">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td nowrap align="left" colspan="3">
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <div style="width: 74%; height: 39px" align="center">
                                                                            <uc1:multiplecolumnddlabelcontrol id="multipleDropControl" runat="server">
                                                                            </uc1:multiplecolumnddlabelcontrol></div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="right" colspan="2">
                                                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            CssClass="FLATBUTTON" Width="90px" Text="Clear" Height="20px"></asp:Button>&nbsp;&nbsp;
                                                                        <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            CssClass="FLATBUTTON" Width="90px" Text="Search" Height="20px"></asp:Button></td>
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
                                            <td valign="top" align="left">
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
                                            <td align="right">
                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid"
                                                    BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True"
                                                    AllowSorting="true" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
                                                    <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue">
                                                    </SelectedItemStyle>
                                                    <EditItemStyle Wrap="False"></EditItemStyle>
                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn SortExpression="1" HeaderText="DEALER">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="60%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="2" HeaderText="START_DATE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="3" HeaderText="END_DATE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="DEALER_ID"></asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                        PageButtonCount="10" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr height="30px">
                            <td align="right" colspan="2">
                                <hr style="height: 1px">
                                <table id="Table2" style="width: 678px;" cellspacing="1" cellpadding="1" align="left"
                                    border="0">
                                    <tr>
                                        <td colspan="6" align="left">
                                            <asp:Button ID="BtnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        Width="100px" Text="New" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;</td>         
                                    </tr>
                                </table>
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


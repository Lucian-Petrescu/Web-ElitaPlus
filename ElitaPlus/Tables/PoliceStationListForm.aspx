<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PoliceStationListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PoliceStationListForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Police_Station</title>
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
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:&nbsp;
								<asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">POLICE_STATION</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid; height: 93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            height="98%" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                            bgcolor="#fef9ea" border="0">
                            <tr>
                                <td height="1"></td>
                            </tr>
                            <tr>
                                <td style="height: 1px" align="center">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table style="height: 100%" cellspacing="0" cellpadding="0" width="100%" align="center"
                                        border="0">
                                        <tr>
                                            <td style="height: 84px" valign="top" colspan="2">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 84px"
                                                    cellspacing="0" cellpadding="4" rules="cols" width="100%" align="center" bgcolor="#f1f1f1"
                                                    border="0">
                                                    <tr>
                                                        <td valign="top" align="left" colspan="1" rowspan="1">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td style="white-space:nowrap"  align="left">
                                                                        <asp:Label ID="moCountryLabel" runat="server">Country:</asp:Label></td>
                                                                    <td style="white-space:nowrap"  align="left" width="30%">
                                                                        <asp:Label ID="lblDescription" runat="server">Police_Station_Name:</asp:Label></td>
                                                                    <td style="white-space:nowrap"  align="left" width="5%"></td>
                                                                    <td style="white-space:nowrap"  align="left" width="25%">
                                                                        <asp:Label ID="lblCode" runat="server">Police_Station_Code:</asp:Label></td>
                                                                    <td style="white-space:nowrap"  align="left"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="white-space:nowrap"  align="left" colspan="1" rowspan="1">
                                                                        <asp:DropDownList ID="moCountryDrop" runat="server" Width="120px"></asp:DropDownList>
                                                                    </td>
                                                                        <td style="white-space:nowrap"  align="left">
                                                                            <asp:TextBox ID="SearchDescriptionTextBox" runat="server" Width="304px" Height="20px" AutoPostBack="False"></asp:TextBox></td>
                                                                        <td style="white-space:nowrap"  align="left"></td>
                                                                        <td style="white-space:nowrap"  align="left">
                                                                            <asp:TextBox ID="SearchCodeTextBox" runat="server" Width="150px" Height="20px" AutoPostBack="False"></asp:TextBox></td>
                                                                        <td style="white-space:nowrap"  align="left"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="white-space:nowrap" align="left"></td>
                                                                    <td style="white-space:nowrap" align="left"></td>
                                                                    <td style="white-space:nowrap" align="right"></td>
                                                                    <td style="white-space:nowrap" align="right" colspan="2" rowspan="1">
                                                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                            runat="server" Font-Bold="false" Width="90px" Text="Clear" CssClass="FLATBUTTON" Height="20px"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                    runat="server" Font-Bold="false" Width="90px" Text="Search" CssClass="FLATBUTTON" Height="20px"></asp:Button></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 1px" colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px" colspan="2">&nbsp;
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
                                        </asp:DropDownList>

                                            </td>
                                            <td style="height: 22px" align="right">
                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" colspan="2">
                                                <asp:DataGrid ID="Grid" runat="server" Width="100%" AllowSorting="True" AllowPaging="True" BorderWidth="1px"
                                                    CellPadding="1" BorderStyle="Solid" BorderColor="#999999" BackColor="#DEE3E7" AutoGenerateColumns="False"
                                                    OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
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
                                                        <asp:BoundColumn SortExpression="POLICE_STATION_NAME" HeaderText="Police_Station_Name">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="65%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="POLICE_STATION_CODE" HeaderText="Police_Station_Code">
                                                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="police_station_id"></asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
                                                        Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid></td>
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
                                        runat="server" Font-Bold="false" Width="100px" Text="New" CssClass="FLATBUTTON" Height="20px"
                                        CommandName="WRITE"></asp:Button>&nbsp;
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

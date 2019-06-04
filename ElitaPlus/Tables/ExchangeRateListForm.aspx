<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ExchangeRateListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ExchangeRateListForm" %>

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
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
            border-left: black 1px solid; border-bottom: black 1px solid; height: 16px" cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:&nbsp;<asp:Label
                                    ID="Label7" runat="server" Cssclass="TITLELABELTEXT">EXCHANGE_RATE</asp:Label></td>
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
                <td style="height: 11px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                            cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td style="height: 1px" align="center" colspan="2">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 79px"
                                                    cellspacing="0" cellpadding="4" rules="cols" width="100%" align="center" bgcolor="#f1f1f1"
                                                    border="0">
                                                    <tr>
                                                        <td style="width: 100%" align="center" colspan="4">
                                                            <table style="height:7.64%" cellspacing="0" cellpadding="0" width="100%" align="right"
                                                                border="0">
                                                                <tr>
                                                                    <td valign="top" align="left" colspan="4">
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tr>
                                                                                <td nowrap align="left" colspan="4" style="height: 18px">
                                                                                <DIV style="WIDTH: 73.30%; HEIGHT: 39px" align="center">                                                                                      <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></DIV>
                                                                                  
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" height="18px">
                                                                        <hr style="height: 1px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <%--<TD style="WIDTH: 3.6%; HEIGHT: 15px" noWrap align="left" colSpan="1" rowSpan="1">
																			<asp:label id="lblDealer" runat="server">DEALER</asp:label>:</TD>--%>
                                                                    <td style=" width: 3.6% ;height: 18px" nowrap align="left" colspan=1>
                                                                        &nbsp;
                                                                        <asp:Label ID="lblFromDate" runat="server" Height="4px">FROM_DATE</asp:Label>:
                                                                        <asp:TextBox ID="txtFromDate" runat="server" Wrap="False"></asp:TextBox>
                                                                        <asp:ImageButton ID="btnFromDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton></TD>                                                                                                                  <td style="width: 3.6%;height: 18px" nowrap align="left" colspan=1>     
                                                                        <asp:Label ID="lblToDate" runat="server" Height="4px">TO_DATE</asp:Label>:                                                                  
                                                                        <asp:TextBox ID="txtToDate" runat="server" Wrap="False"></asp:TextBox>
                                                                        <asp:ImageButton ID="btnToDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton></td>
                                                                         <td style="width: 3.57%; height: 18px" nowrap align="left" colspan=2>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                              
                                                                <tr>
                                                                    <td style="width: 3.6%; height: 19px" nowrap align="left"></td>
                                                                    <td style="width: 3.5%; height: 19px" nowrap align="center"></td>
                                                                    <td style="width: 3.57%; height: 19px" nowrap align="center" colspan="2">
                                                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            Width="90px" Text="Clear" Height="20px" CssClass="FLATBUTTON"></asp:Button>
                                                                        <asp:Button ID="btnSearch" Style="background-image: url(../ Navigation/images/icons/search_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            Width="90px" Text="Search" Height="20px" CssClass="FLATBUTTON"></asp:Button></td>
                                                                </tr>                                                           
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </TR>
                                        <tr>
                                            <td colspan="2">
                                            &nbsp;
                                             <hr style="height: 1px">
                                            </td>
                                        </tr>
                                        <tr id="trPageSize" runat="server">
                                            <td style="width: 407px" valign="top" align="left">
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
                                            <td align="center" colspan="2">
                                                <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid"
                                                    BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True"
                                                    AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
                                                    <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue">
                                                    </SelectedItemStyle>
                                                    <EditItemStyle Wrap="False"></EditItemStyle>
                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn HeaderText="Dealer_Code">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn HeaderText="Dealer">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="45%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="3" HeaderText="Effective_Date">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="Currency_Rate_Id"></asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 407px">
                                            </td>
                                        </tr>
                                    </TABLE> 
                                </TD>
                            </tr>
                            <tr>
                                <td style="height: 40px">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        Width="100px" Text="New" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

</body>
</html>

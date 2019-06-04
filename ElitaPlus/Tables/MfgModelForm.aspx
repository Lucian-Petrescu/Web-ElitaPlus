<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MfgModelForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.MfgModelForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>MfgModelForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
            border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                &nbsp;<asp:Label ID="TablesLabel" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:
                                <asp:Label ID="MaintainMfgModelLabel" runat="server"  Cssclass="TITLELABELTEXT">Mfg_Model</asp:Label></td>
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
                <td style="height: 12px">
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
                                <td height="1">
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" height="1">
                                    <uc1:ErrorController ID="ErrController" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                                                    height: 76px" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                                                    bgcolor="#f1f1f1" border="0">
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td valign="top" align="left" colspan="5">
                                                                        <table cellspacing="0" cellpadding="0" border="0" width="85%">
                                                                            <tr>
                                                                                <td align="left" valign="middle">
                                                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <hr style="height: 1px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left" style="width: 1%;" valign="middle">
                                                                        <asp:Label ID="Label1" runat="server">MANUFACTURER</asp:Label>:</td>
                                                                    <td align="left" width="35%">
                                                                        &nbsp;
                                                                        <asp:DropDownList ID="cboManufacturer" runat="server" Width="150px" AutoPostBack="False">
                                                                        </asp:DropDownList></td>
                                                                    <td nowrap width="1%" align="left">
                                                                        <asp:Label ID="SearchDescriptionLabel" runat="server">MODEL</asp:Label>:</td>
                                                                    <td nowrap align="left" width="30%">
                                                                        &nbsp;
                                                                        <asp:TextBox ID="SearchDescriptionTextBox" runat="server" Width="150px" AutoPostBack="False"
                                                                            CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                                    <td colspan="1">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5" height="10px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <tr>
                                                                        <td nowrap align="center">
                                                                        </td>
                                                                        <td nowrap align="center" colspan="2">
                                                                        </td>
                                                                        <td align="right" colspan="2" style="height: 20px">
                                                                            <asp:Button ID="ClearButton" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                Width="90px" CssClass="FLATBUTTON" Text="Clear" Height="20px"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                                            <asp:Button ID="SearchButton" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                Width="90px" CssClass="FLATBUTTON" Text="Search" Height="20px"></asp:Button></td>
                                                                    </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" height="5">
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
                                            <td align="right">
                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:DataGrid ID="Grid" runat="server" Width="100%" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated"
                                                    BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1"
                                                    AllowSorting="True" BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True">
                                                    <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                    CommandName="EditRecord"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>&nbsp;</EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                                                    runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>&nbsp;</EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                                                        <asp:TemplateColumn SortExpression="dealer_name" HeaderText="DEALER_NAME">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemTemplate>
                                                                DealerNameInGridLabel
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="cboDealerInGrid" runat="server">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="manufacturer_name" HeaderText="MANUFACTURER">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemTemplate>
                                                                ManufacturerInGridLabel
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="cboManufacturerInGrid" runat="server">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn SortExpression="description" HeaderText="MODEL">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left" Width="28%"></ItemStyle>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBoxGridDescription" CssClass="FLATTEXTBOX_TAB" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr style="height: 1px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 32px">
                                    <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        Width="100px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>
                                    <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        Width="100px" CssClass="FLATBUTTON" Text="Save" Height="20px"></asp:Button>&nbsp;
                                    <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        Width="100px" CssClass="FLATBUTTON" Text="Cancel" Height="20px"></asp:Button>&nbsp;
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

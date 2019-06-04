<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductGroupListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ProductGroupListForm" %>

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
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <asp:Label ID="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:&nbsp;
                            <asp:Label ID="Label7" runat="server" Cssclass="TITLELABELTEXT">Product_Group</asp:Label>
                        </td>
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
            <td>
                &nbsp;
            </td>
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
                            <td align="center">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="2">
                                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                                                height: 50px" cellspacing="0" cellpadding="3" rules="cols" width="98%" align="center"
                                                bgcolor="#f1f1f1" border="0">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td valign="middle" align="left">
                                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDealerDropControl" runat="server">
                                                                    </uc1:MultipleColumnDDLabelControl>
                                                                </td>
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
                                                    <td>
                                                        <table cellpadding="0" cellspacing="2" border="0">
                                                            <tr>
                                                                <td valign="middle" align="left">
                                                                    <asp:Label ID="LabelDescription" runat="server" Font-Bold="false">Group_Name</asp:Label>:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="middle" align="left">
                                                                    <asp:TextBox ID="TextboxDescription" TabIndex="3" runat="server" Width="210px" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" colspan="1">
                                                                    <asp:Label ID="moProductLabel" runat="server" Font-Bold="false">Product_Code</asp:Label>:
                                                                </td>
                                                                <td colspan="1" style="width: 40px">
                                                                </td>
                                                                <td align="left" colspan="1">
                                                                    <asp:Label ID="moRiskLabel" runat="server" Font-Bold="false">Risk_Type</asp:Label>:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="1" rowspan="1">
                                                                    <asp:DropDownList ID="moProductDrop" TabIndex="4" runat="server" Width="210px" 
                                                                        AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td colspan="1" style="width: 40px">
                                                                </td>
                                                                <td colspan="1">
                                                                    <asp:DropDownList ID="moRiskDrop" TabIndex="6" runat="server" Width="210px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="25%">
                                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                            Width="90px" CssClass="FLATBUTTON" Text="Clear"></asp:Button>&nbsp;
                                                        <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                            Width="90px" CssClass="FLATBUTTON" Text="Search"></asp:Button>
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
                                        <td align="right">
                                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:DataGrid ID="Grid" runat="server" Width="100%" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated"
                                                AllowSorting="True" AllowPaging="True" CellPadding="1" BorderColor="#999999"
                                                BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False">
                                                <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
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
                                                    <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                                                    <asp:BoundColumn SortExpression="2" HeaderText="DEALER">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn SortExpression="1" HeaderText="Group_Name">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="60%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
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
                            <td align="left">
                                <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;
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

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MasterClaimForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MasterClaimForm" %>

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
                                <asp:Label ID="TablesLabel" runat="server" CssClass="TITLELABEL">Claims</asp:Label>:
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABELTEXT">MASTER_CLAIM_DETAIL</asp:Label></td>
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
                                                                    <td nowrap align="left" width="30%">
                                                                        <asp:Label ID="LabelMasterClaimNumber" runat="server">MASTER_CLAIM_NUMBER</asp:Label>:
                                                                    </td>
                                                                    <td nowrap align="left" width="30%">
                                                                        <asp:Label ID="LabelTotalAmountAuthorized" runat="server">TOTAL_AMOUNT_AUTHORIZED</asp:Label>:
                                                                    </td>
                                                                    <td nowrap align="left" width="30%">
                                                                        <asp:Label ID="LabelTotalAmountPaid" runat="server">TOTAL_AMOUNT_PAID</asp:Label>:
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left" width="30%">
                                                                        <asp:TextBox ID="TextBoxMasterClaimNumber" runat="server" CssClass="FLATTEXTBOX"
                                                                            AutoPostBack="False" Width="98%"></asp:TextBox></td>
                                                                    <td nowrap align="left" width="30%">
                                                                        <asp:TextBox ID="TextBoxTotalAmountAuthorized" runat="server" CssClass="FLATTEXTBOX"
                                                                            AutoPostBack="False" Width="98%"></asp:TextBox></td>
                                                                    <td nowrap align="left" width="30%">
                                                                        <asp:TextBox ID="TextBoxTotalAmountPaid" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
                                                                            Width="98%"></asp:TextBox></td>
                                                                </tr>
                                                                <asp:Panel ID="serviceCenterPanel" runat="server">
                                                                    <tr>
                                                                        <td nowrap align="left" width="30%">
                                                                            <asp:Label ID="LabelDealer" runat="server">DEALER</asp:Label>:</td>
                                                                        <td nowrap align="left" width="30%">
                                                                            <asp:Label ID="LabelCertificate" runat="server">CERTIFICATE</asp:Label>:</td>
                                                                        <td nowrap align="left" width="30%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap align="left" width="30%">
                                                                            <asp:TextBox ID="TextBoxDealer" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
                                                                                Width="98%"></asp:TextBox></td>
                                                                        <td nowrap align="left" width="30%">
                                                                            <asp:TextBox ID="TextBoxCertificate" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
                                                                                Width="98%"></asp:TextBox></td>
                                                                        <td nowrap align="left" width="30%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
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
                                            <td colspan="3">
                                                <asp:DataGrid ID="Grid" runat="server" Width="100%" OnItemCreated="ItemCreated" AutoGenerateColumns="False"
                                                    BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                                                    CellPadding="1" AllowPaging="True" AllowSorting="False" OnItemCommand="ItemCommand">
                                                    <SelectedItemStyle Wrap="False" BackColor="Magenta"></SelectedItemStyle>
                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                    <HeaderStyle  HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                                                                    ImageUrl="../Navigation/images/icons/yes_icon.gif" runat="server" CausesValidation="false">
                                                                </asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn SortExpression="1" HeaderText="Claim_Number">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="2" HeaderText="STATUS">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="6%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="3" HeaderText="Authorized_Amount">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="4" HeaderText="INVOICE_NUMBER">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="18%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="5" HeaderText="PAYEE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="25%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="6" HeaderText="DATE_CREATED">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="7" HeaderText="AMOUNT_PAID">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="13%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="Claim_ID"></asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="Cert_ID"></asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="Claim_Invoice_ID"></asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                        Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="5">
                                                <table id="Table7" height="40" cellspacing="1" cellpadding="1" width="60%" align="left"
                                                    border="0">
                                                    <asp:Panel ID="buttonlinks" runat="server">
                                                        <tbody>
                                                            <tr>
                                                                <td width="135">
                                                                    <asp:Button ID="btnClaimDetail" Style="cursor: hand; background-repeat: no-repeat"
                                                                        TabIndex="1" runat="server" Font-Bold="false" Width="100%" CssClass="FLATBUTTON"
                                                                        Height="20px" Text="CLAIM_DETAIL"></asp:Button></td>
                                                                <td width="135">
                                                                    <asp:Button ID="btnInvoiceDetail" Style="cursor: hand; background-repeat: no-repeat"
                                                                        TabIndex="2" runat="server" Font-Bold="false" Width="100%" CssClass="FLATBUTTON"
                                                                        Height="20px" Text="INVOICE_DETAIL"></asp:Button></td>
                                                                <td width="135">
                                                                    <asp:Button ID="btnCertificate" Style="cursor: hand; background-repeat: no-repeat"
                                                                        TabIndex="3" runat="server" Font-Bold="false" Width="100%" CssClass="FLATBUTTON"
                                                                        Height="20px" Text="Certificate"></asp:Button></td>
                                                                <td width="135">
                                                                    &nbsp;</td>
                                                                <td width="135">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </tbody>
                                                    </asp:Panel>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="bottom">
                                <td align="right" colspan="2">
                                    <hr style="height: 1px">
                                    <table id="Table2" cellspacing="1" cellpadding="1" width="300" align="left" border="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CausesValidation="False"
                                                    Height="20px" CssClass="FLATBUTTON" Text="BACK"></asp:Button></td>
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

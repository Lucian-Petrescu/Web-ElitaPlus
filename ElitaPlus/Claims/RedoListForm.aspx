<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RedoListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RedoListForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>DealerListForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <style type="text/css">
        .style1
        {
            width: 180px;
        }
        .style2
        {
            width: 131px;
        }
        .style3
        {
            width: 222px;
        }
        .style4
        {
            width: 58px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Claim</asp:Label>:&nbsp;
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">ReDo</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="WorkingPanel" runat="server">
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid; height: 93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                        height="98%"
                        cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tbody>
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
                                        <tbody>
                                            <tr>
                                                <td style="height: 84px" valign="top" colspan="2">
                                                    <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 84px"
                                                        cellspacing="0" cellpadding="4" rules="cols" width="98%" align="center" bgcolor="#f1f1f1"
                                                        border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="top" align="left" colspan="1" rowspan="1" class="style3">
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="right" valign="middle" class="style3">
                                                                    <asp:Label ID="Label1" runat="server">Customer_Name</asp:Label>:</td>
                                                                <td nowrap align="left" class="style1">
                                                                    <asp:TextBox ID="txtCustomerName"
                                                                        runat="server" AutoPostBack="False" Width="211px"></asp:TextBox>
                                                                </td>
                                                                <td class="style4" />
                                                                <td nowrap align="right" class="style2">
                                                                    <asp:Label ID="Label2" runat="server">Certificate_Number</asp:Label>:</td>
                                                                <td nowrap align="left" class="style1">
                                                                    <asp:TextBox ID="txtCertificateNumber" runat="server" Width="236px" AutoPostBack="False"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td nowrap align="left" colspan="3">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="right" valign="middle" class="style3">
                                                                    <asp:Label ID="Label4" runat="server">Redo_Claim_Number</asp:Label>:</td>
                                                                <td nowrap align="left" class="style1">
                                                                    <asp:TextBox ID="txtRedoClaimNumber"
                                                                        runat="server" AutoPostBack="False" Width="211px"></asp:TextBox>
                                                                </td>
                                                                <td class="style4" />
                                                                <td nowrap align="right" class="style2">
                                                                    <asp:Label ID="Label5" runat="server">Service_center</asp:Label>:</td>
                                                                <td nowrap align="left" class="style1">
                                                                    <asp:TextBox ID="txtServiceCenter" runat="server" Width="236px" AutoPostBack="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" colspan="3">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="right" valign="middle" class="style3">
                                                                    <asp:Label ID="Label3" runat="server">Extended_Status</asp:Label>:</td>
                                                                <td nowrap align="left" class="style1">
                                                                    <asp:TextBox ID="txtExtendedStatus"
                                                                        runat="server" AutoPostBack="False" Width="211px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 14px" colspan="2">&nbsp;
                                <hr style="height: 1px">
                </td>
            </tr>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                runat="server" />
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
                    <asp:GridView ID="Grid" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
                        CellPadding="1" AutoGenerateColumns="False" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                        CssClass="DATAGRID">
                        <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                        <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                        <RowStyle CssClass="ROW"></RowStyle>
                        <HeaderStyle CssClass="HEADER"></HeaderStyle>
                        <Columns>

                            <asp:TemplateField ShowHeader="false" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                                        ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                                        ImageUrl="../Navigation/images/icons/yes_icon.gif" CommandArgument="<%#Container.DisplayIndex %>"
                                        runat="server" CausesValidation="false"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False" HeaderText="claim_id"></asp:TemplateField>
                            <asp:TemplateField SortExpression="CLAIM_NUMBER" HeaderText="Claim Number">
                                <HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="CODE" HeaderText="Service Center Code">
                                <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="PICK_UP_DATE" HeaderText="Service Warranty begins">
                                <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False" HeaderText="master_claim_number"></asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="15" Mode="Numeric" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif); cursor: hand; background-repeat: no-repeat"
                        TabIndex="5" runat="server" Width="90px"
                        CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                            <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand; background-repeat: no-repeat"
                                TabIndex="5" runat="server" Width="90px"
                                CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;        
                </td>
            </tr>
        </table>
      </asp:Panel>
    </form>
  
</body>
</html>

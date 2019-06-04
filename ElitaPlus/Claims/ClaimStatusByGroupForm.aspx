<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimStatusByGroupForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimStatusByGroupForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ClaimStatusByGroupForm</title>
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
                            <asp:Label ID="Label5" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:
                            <asp:Label ID="Label6" runat="server" CssClass="TITLELABELTEXT">EXTENDED_CLAIM_STATUS</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>      
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0"
        frame="void">
                    <tr>
                <td style="height: 8px" valign="top" align="center">
                </td>
            </tr>
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="4" rules="cols" height="100%" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr valign="top">
                            <td>
                                &nbsp;
                                <uc1:ErrorController ID="ErrorControl" runat="server" Visible="False"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <table cellspacing="0" cellpadding="0" width="99%" border="0" align="center">
                                    <tr valign="top">
                                        <td width="55%" colspan="2">
                                            <div style="width: 50%; height: 39px" align="center">
                                                <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                            </div>
                                        </td>
                                        <td nowrap align="left" width="30%" rowspan="2" valign="middle">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td width="55%" colspan="3">
                                            <asp:Label ID="lblCompanyGroup" runat="server" Visible="true">COMPANY_GROUP</asp:Label>
                                            <asp:TextBox ID="txtCompanyGroup" TabIndex="2" runat="server" Width="280"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="center" style="height: 14px" width="100%" colspan="3">
                                            <hr size="1" style="height: 1px">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="center" valign="top" colspan="3">
                                            <asp:DataGrid ID="DataGridDropdowns" runat="server" Width="99%" AutoGenerateColumns="False"
                                                BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                                                CellPadding="1" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated"
                                                PageSize="15" OnItemCommand="DataGridDropdowns_ItemCommand">
                                                <SelectedItemStyle Wrap="False"></SelectedItemStyle>
                                                <EditItemStyle Wrap="False"></EditItemStyle>
                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                <HeaderStyle ForeColor="#12135B" ></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateColumn Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblListItemId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("list_item_id")) %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderImageUrl="../Navigation/images/icons/check.gif">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBoxItemSel" runat="server" Checked='<%# Container.DataItem("SELECTED")="Y" %>'>
                                                            </asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="EXTENDED_CLAIM_STATUS">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="47%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label Width="100%" ID="lblExtendedClaimStatus" runat="server" Text='<%# Container.DataItem("extended_claim_status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="OWNER">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="dropDownOwner" runat="server" Visible="True">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="SKIPPING_ALLOWED">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="13%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="dropDownSkippingAllowed" runat="server" Visible="True">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATUS_ORDER">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox Width="100%" ID="txtStatusOrder" runat="server" Text='<%# Container.DataItem("STATUS_ORDER") %>'
                                                                CssClass="FLATTEXTBOX">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="ACTIVE">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="8%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="dropDownActive" runat="server" Visible="True">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="GROUP_NUMBER">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox Width="100%" ID="txtGroupNumber" runat="server" Text='<%# Container.DataItem("GROUP_NUMBER") %>'
                                                                CssClass="FLATTEXTBOX">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIsNew" runat="server" Visible="False" Text='<%# Container.DataItem("ISNEW") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="bottom" width="100%" colspan="3">
                                <hr size="1">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="90px" Text="Return" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="90px" Text="Save" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                                <asp:Button ID="btnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="90px" Text="Cancel" Height="20px" CssClass="FLATBUTTON"></asp:Button>
                                <asp:Button ID="btnButtomNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" CssClass="FLATBUTTON"
                                    Height="20px" Text="New"></asp:Button>
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="140px" CssClass="FLATBUTTON"
                                    Height="20px" Text="New_With_Copy"></asp:Button>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnActionSettings" runat="server" CssClass="FLATBUTTON" 
                                    Height="20px" 
                                    Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat" 
                                    Text="Action_Settings" Width="140px" Enabled="False" />
                                &nbsp;&nbsp;<asp:Button ID="btnDefaultStatuses" runat="server" CssClass="FLATBUTTON" 
                                    Height="20px" 
                                    Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat" 
                                    Text="DFAULT_STATUSES" Width="140px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <input type="hidden" id="CMD" value="" runat="server" />
    <input type="hidden" id="CopyDealerId" value="" runat="server" />
    </form>
</body>
</html>

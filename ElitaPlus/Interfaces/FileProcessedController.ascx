<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="~/Interfaces/InterfaceProgressControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FileProcessedController.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.FileProcessedController" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<table id="moTablelMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
    border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
    cellpadding="6" rules="cols" width="100%" align="center" bgcolor="#fef9ea" border="0">
    <tr>
        <td>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <table id="moTableSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; width: 692px; border-bottom: #999999 1px solid;
                            height: 64px" cellspacing="0" cellpadding="0" rules="cols" align="center"
                            bgcolor="#fef9ea" border="0">
                            <tr><td><br /></td></tr>
                            <tr>
                                <td style="width: 600px" align="right">
                                    <uc1:MultipleColumnDDLabelControl ID="moCompanyGroup" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 600px" align="right">
                                    <uc1:MultipleColumnDDLabelControl ID="moCompany" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 600px" align="right">
                                    <uc1:MultipleColumnDDLabelControl ID="moDealer" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 600px" align="right">
                                    <uc1:MultipleColumnDDLabelControl ID="moReference" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
                                </td>
                            </tr>
                            <tr><td><br /></td></tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                            BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                            CellPadding="1" AllowPaging="True" AllowSorting="True">
                            <SelectedItemStyle Wrap="False" BackColor="Magenta"></SelectedItemStyle>
                            <EditItemStyle Wrap="False"></EditItemStyle>
                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                            <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" CommandName="EditRecord"
                                            ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CausesValidation="false">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                                            ImageUrl="../Navigation/images/icons/yes_icon.gif" runat="server" CausesValidation="false">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="FILE_NAME" HeaderText="FILENAME">
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RECEIVED" HeaderText="RECEIVED">
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COUNTED" HeaderText="COUNTED">
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="REJECTED" HeaderText="REJECTED">
                                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VALIDATED" HeaderText="VALIDATED">
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                 <asp:BoundColumn DataField="BYPASSED" HeaderText="BYPASSED">
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="LOADED" HeaderText="PROCESSED">
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="LAYOUT" HeaderText="LAYOUT">
                                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <hr style="width: 100%; height: 1px" size="1">
        </td>
    </tr>
    <tr>
        <td style="height: 80px" align="left">
            <asp:Panel ID="moButtonPanel" runat="server" Visible="False">
                <asp:Button ID="BtnValidate_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" Width="85px" runat="server" Font-Bold="false"
                    CssClass="FLATBUTTON" Height="20px" Text="VALIDATE" CausesValidation="False"
                    Enabled="False"></asp:Button>&nbsp;
                <asp:Button ID="BtnLoad_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" Width="152px" runat="server" Font-Bold="false"
                    CssClass="FLATBUTTON" Height="20px" Text="PROCESS_RECORDS" CausesValidation="False"
                    Enabled="False"></asp:Button>&nbsp;
                <asp:Button ID="BtnDeleteFile_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" Width="215px" runat="server" Font-Bold="false"
                    CssClass="FLATBUTTON" Height="20px" Text="DELETE" CausesValidation="False"
                    Enabled="False"></asp:Button>&nbsp;
                <asp:Button ID="BtnRejectReport" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" Width="119px" runat="server" Font-Bold="false"
                    CssClass="FLATBUTTON" Height="20px" Text="REJECT_REPORT" CausesValidation="False"
                    Enabled="False"></asp:Button>&nbsp;
                <asp:Button ID="BtnProcessedExport" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" Width="151px" runat="server" Font-Bold="false"
                    CssClass="FLATBUTTON" Height="20px" Text="PROCESSED_EXPORT" CausesValidation="False"
                    Enabled="False"></asp:Button></asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="moUpLoadPanel" runat="server" Visible="False">
                <table id="moTableSearch2" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; width: 693px; border-bottom: #999999 1px solid;
                    height: 67px" cellspacing="0" cellpadding="0" rules="cols" width="693" align="center"
                    bgcolor="#fef9ea" border="0">
                    <tr>
                        <td style="width: 539px">
                            <table style="width: 680px" cellspacing="0" cellpadding="0" width="680" border="0">
                                <tr>
                                    <td style="width: 159px; height: 22px" nowrap align="right">
                                        *
                                        <asp:Label ID="moFilenameLabel" runat="server">Filename</asp:Label>:
                                    </td>
                                    <td style="height: 10px" nowrap align="left">
                                        <input id="FileInput" style="width: 269px; height: 19px" type="file" size="25"
                                            name="FileInput" runat="server">
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCopyFile_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" Width="215px" runat="server" Font-Bold="false"
                                            CssClass="FLATBUTTON" Height="20px" Text="COPY"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 159px; height: 22px" nowrap align="right">
                                        <asp:Label ID="moExpectedFileLabel" runat="server">Expected_File</asp:Label>:
                                    </td>
                                    <td style="height: 10px" nowrap align="left">
                                        &nbsp;
                                        <asp:Label ID="moExpectedFileLabel_NO_TRANSLATE" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<uc1:InterfaceProgressControl ID="moInterfaceProgressControl" runat="server"></uc1:InterfaceProgressControl>
<asp:Button ID="btnAfterProgressBar" Style="background-color: #fef9ea" runat="server"
    Width="0" Height="0"></asp:Button>

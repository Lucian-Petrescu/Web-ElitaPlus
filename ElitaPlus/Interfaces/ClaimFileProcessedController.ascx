<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ClaimFileProcessedController.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ClaimFileProcessedController" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<table id="moTablelMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
    cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center" bgcolor="#fef9ea"
    border="0">

    <tr>
        <td>
            <asp:Panel ID="ClaimInterfacePanel" runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="height: 70px">
                            <table id="moTableSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 692px; border-bottom: #999999 1px solid; height: 30px"
                                cellspacing="0" cellpadding="0" rules="cols" width="692" align="center" bgcolor="#fef9ea"
                                border="0">
                                <tr>
                                    <td style="width: 539px">
                                        <table style="width: 680px; height: 30px" cellspacing="0" cellpadding="0" width="680" border="0">
                                            <tr>
                                                <td align="center">
                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>

                                            <tr>
                                                <td nowrap align="center">
                                                    <asp:Label ID="moClaimInterfaceLabel" runat="server">CLAIM_INTERFACE:</asp:Label>
                                                    <asp:DropDownList ID="moClaimInterfaceDrop" runat="server" Width="242px"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="moBtnClear" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                        runat="server" Width="90px" Font-Bold="false" Visible="False" CssClass="FLATBUTTON" Height="20px"
                                                        Text="Clear" CausesValidation="False"></asp:Button>
                                                    <asp:Button ID="moBtnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                        runat="server" Width="90px" Font-Bold="false" Visible="False" CssClass="FLATBUTTON" Height="20px"
                                                        Text="Search" CausesValidation="False" Enabled="true"></asp:Button></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>

        </td>
        <tr>
            <td colspan="3">
                <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" OnItemCommand="ItemCommand" AllowSorting="True"
                    AllowPaging="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid"
                    AutoGenerateColumns="False" OnItemCreated="ItemCreated">
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
                                <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" CommandName="EditRecord" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CausesValidation="false"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord" ImageUrl="../Navigation/images/icons/yes_icon.gif"
                                    runat="server" CausesValidation="false"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False" HeaderText="claimfile_processed_id"></asp:BoundColumn>
                        <asp:BoundColumn DataField="FILENAME" HeaderText="FILENAME">
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
                        <asp:BoundColumn DataField="BYPASSED" HeaderText="BYPASSED">
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
                        <asp:BoundColumn DataField="LOADED" HeaderText="PROCESSED">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="PROCESSED_AMOUNT" HeaderText="VALIDATED_AMOUNT">
                            <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid></td>
        </tr>
</table>
<table>

	<tr>
        <td align="left">
            <hr style="width: 100%; height: 1px" size="1">
        </td>
    </tr>
<tr>
    <td align="left">
        <asp:Panel ID="moButtonPanel" runat="server" Visible="False">
            <asp:Button ID="BtnValidate_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                Width="121px" runat="server" CausesValidation="False" Text="VALIDATE" Height="20px"
                CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:Button>&nbsp; 
      &nbsp; 
            <asp:Button ID="BtnLoadCertificate_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                Width="174px" runat="server" CausesValidation="False" Text="PROCESS_RECORDS"
                Height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:Button>&nbsp;&nbsp; 
            <asp:Button ID="BtnDeleteDealerFile_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                Width="185px" runat="server" CausesValidation="False" Text="DELETE_CLAIM_FILE"
                Height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:Button>&nbsp;&nbsp; 
            <asp:Button ID="BtnRejectReport" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                Width="161px" runat="server" CausesValidation="False" Text="REJECT_REPORT"
                Height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:Button>
            <asp:Button ID="BtnProcessedExport" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                Width="161px" runat="server" CausesValidation="False" Text="PROCESSED_EXPORT"
                Height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:Button>
        </asp:Panel>
    </td>

</tr>
<tr>
    <td style='height: 27px' align="center">&nbsp;</td>
</tr>
<tr>
    <td></td>
</tr>
<tr>
    <td>
        <asp:Panel ID="moUpLoadPanel" runat="server" Visible="False">
            <table id="moTableSearch2" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 693px; border-bottom: #999999 1px solid; height: 67px"
                cellspacing="0" cellpadding="0" rules="cols" width="693" align="center" bgcolor="#fef9ea"
                border="0">
                <tr>
                    <td style="width: 539px">
                        <table style="width: 680px" cellspacing="0" cellpadding="0" width="680" border="0">
                            <tr>
                                <td style="width: 159px; height: 22px" nowrap align="left">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td style="height: 10px" nowrap align="center">&nbsp;&nbsp;&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 159px; height: 22px" nowrap align="right">*
										<asp:Label ID="moFilenameLabel" runat="server">Filename</asp:Label>:</td>
                                <td style="height: 10px" nowrap align="left">
                                    <input id="claimFileInput" style="width: 269px; height: 19px" type="file" size="25" name="claimFileInput"
                                           runat="server"/></td>
                                <td>
                                    <asp:Button ID="btnCopyDealerFile_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                        Width="236px" runat="server" Text="COPY_CLAIM_INTERFACE_FILE" Height="20px" CssClass="FLATBUTTON"
                                        Font-Bold="false"></asp:Button></td>
                            </tr>
                            <tr>
                                <td style="width: 159px; height: 22px" nowrap align="right">
                                    <asp:Label ID="moExpectedFileLabel" runat="server">Expected_File</asp:Label>:</td>
                                <td style="height: 10px" nowrap align="left">&nbsp;
										<asp:Label ID="moExpectedFileLabel_NO_TRANSLATE" runat="server"></asp:Label></td>
                                <td></td>
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
<asp:Button ID="btnAfterProgressBar" Style="background-color: #fef9ea; display: none;" runat="server" Width="0"
    Height="0"></asp:Button>

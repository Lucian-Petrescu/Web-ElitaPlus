<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="DealerFileProcessedController.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.DealerFileProcessedController" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<TABLE id="moTablelMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
	cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center" bgColor="#fef9ea"
	border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td style="height: 70px">
                        <table id="TABLE1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; width: 692px; border-bottom: #999999 1px solid;
                            height: 64px" cellspacing="0" cellpadding="0" rules="cols" width="600" align="center"
                            bgcolor="#fef9ea" border="0">
                            <tr>
                                <td style="width: 539px">
                                    <uc1:MultipleColumnDDLabelControl ID="multipleDealerGrpDropControl" runat="server">
                                    </uc1:MultipleColumnDDLabelControl>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <hr style="width: 100%; height: 1px" size="1">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 539px">
                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <TR>
					<TD colSpan="3"><asp:datagrid id="moDataGrid" runat="server" Width="100%" OnItemCreated="ItemCreated" AutoGenerateColumns="False"
							BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True"
							AllowSorting="True" OnItemCommand="ItemCommand">
							<SelectedItemStyle Wrap="False" BackColor="Magenta"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
							<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
							<HeaderStyle  HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" CommandName="EditRecord" ImageUrl="../Navigation/images/icons/edit2.gif"
											runat="server" CausesValidation="false"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton style="cursor:hand;" id="btnSelect" CommandName="SelectRecord" ImageUrl="../Navigation/images/icons/yes_icon.gif"
											runat="server" CausesValidation="false"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" HeaderText="dealerfile_processed_id"></asp:BoundColumn>
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
									<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
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
								<asp:BoundColumn Visible="False" DataField="LAYOUT" HeaderText="LAYOUT">
									<HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD align="left">
			<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
		</TD>
	</TR>
	<TR>
		<TD  style="height: 80px"align="left" ><asp:panel id="moButtonPanel" runat="server" Visible="False">
<asp:button id="BtnValidate_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="85px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
					Text="VALIDATE" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
<asp:button id="BtnLoadCertificate_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="132px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
					Text="PROCESS_RECORDS" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
<asp:button id="BtnDeleteDealerFile_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="140px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
					Text="DELETE_DEALER_FILE" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
<asp:button id="BtnRejectReport" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="115px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
					Text="REJECT_REPORT" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
<asp:button id="BtnErrorExport" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="104px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
					Text="ERROR_EXPORT" CausesValidation="False" Enabled="False"></asp:button>&nbsp;
<asp:button id="BtnProcessedExport" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="135px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
					Text="PROCESSED_EXPORT" CausesValidation="False" Enabled="False"></asp:button></asp:panel></TD>
	</TR>	
	<TR>
		<TD><asp:panel id="moUpLoadPanel" runat="server" Visible="False">
				<TABLE id="moTableSearch2" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 693px; BORDER-BOTTOM: #999999 1px solid; HEIGHT:67px"
					cellSpacing="0" cellPadding="0" rules="cols" width="693" align="center" bgColor="#fef9ea"
					border="0">
					<TR>
						<TD style="WIDTH: 539px">
							<TABLE style="WIDTH: 680px" cellSpacing="0" cellPadding="0" width="680" border="0">
								
								<TR>
									<TD style="WIDTH: 159px; HEIGHT: 22px" noWrap align="right">*
										<asp:label id="moFilenameLabel" runat="server">Filename</asp:label>:</TD>
									<TD style="HEIGHT: 10px" noWrap align="left"><INPUT id="dealerFileInput" style="WIDTH: 269px; HEIGHT: 19px" type="file" size="25" name="dealerFileInput"
											runat="server"></TD>
									<TD>
										<asp:button id="btnCopyDealerFile_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											Width="176px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px" Text="COPY_DEALER_FILE"></asp:button></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 159px; HEIGHT: 22px" noWrap align="right">
										<asp:label id="moExpectedFileLabel" runat="server">Expected_File</asp:label>:</TD>
									<TD style="HEIGHT: 10px" noWrap align="left">&nbsp;
										<asp:Label id="moExpectedFileLabel_NO_TRANSLATE" runat="server"></asp:Label></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
</TABLE>
<uc1:interfaceprogresscontrol id="moInterfaceProgressControl" runat="server"></uc1:interfaceprogresscontrol>
<asp:button id="btnAfterProgressBar" style="BACKGROUND-COLOR: #fef9ea" runat="server" Width="0"
	Height="0"></asp:button>

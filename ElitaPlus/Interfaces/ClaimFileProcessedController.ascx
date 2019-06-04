<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ClaimFileProcessedController.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ClaimFileProcessedController" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<TABLE id="moTablelMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
	cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center" bgColor="#fef9ea"
	border="0">
	
	<TR>
		<TD><asp:Panel ID="ClaimInterfacePanel" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD style="HEIGHT: 70px">
						<TABLE id="moTableSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 692px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 30px"
							cellSpacing="0" cellPadding="0" rules="cols" width="692" align="center" bgColor="#fef9ea"
							border="0">
							<TR>
								<TD style="WIDTH: 539px">
									<TABLE style="WIDTH: 680px; HEIGHT: 30px" cellSpacing="0" cellPadding="0" width="680" border="0">
									     <tr><td align="center">
									         <uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
								         </td>
								         </tr>
								         <tr><td>&nbsp;</td></tr>
								         
										<tr>
											<td noWrap align="center">
												<asp:label id="moClaimInterfaceLabel" runat="server" >CLAIM_INTERFACE:</asp:label>
											<asp:dropdownlist id="moClaimInterfaceDrop" runat="server" Width="242px" 
                                                    AutoPostBack="True"></asp:dropdownlist>
												<asp:button id="moBtnClear" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="90px" Font-Bold="false" Visible="False" CssClass="FLATBUTTON" height="20px"
													Text="Clear" CausesValidation="False"></asp:button>
												<asp:button id="moBtnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="90px" Font-Bold="false" Visible="False" CssClass="FLATBUTTON" height="20px"
													Text="Search" CausesValidation="False" Enabled="true"></asp:button></td>
										</tr>
										 <tr><td>&nbsp;</td></tr>
										
						           </TABLE>
								</TD>
							</TR>
			            </TABLE>
					</TD>
				</TR>
				<tr><td>&nbsp;</td></tr>
				 </asp:Panel> 
				<TR>
					<TD colSpan="3"><asp:datagrid id="moDataGrid" runat="server" Width="100%" OnItemCommand="ItemCommand" AllowSorting="True"
							AllowPaging="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid"
							AutoGenerateColumns="False" OnItemCreated="ItemCreated">
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
		<TD align="left"><asp:panel id="moButtonPanel" runat="server" Visible="False">
<asp:button id="BtnValidate_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="121px" runat="server" CausesValidation="False" Text="VALIDATE" height="20px"
					CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:button>&nbsp; 
      &nbsp; 
<asp:button id="BtnLoadCertificate_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="174px" runat="server" CausesValidation="False" Text="PROCESS_RECORDS"
					height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:button>&nbsp;&nbsp; 
<asp:button id="BtnDeleteDealerFile_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="185px" runat="server" CausesValidation="False" Text="DELETE_CLAIM_FILE"
					height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:button>&nbsp;&nbsp; 
<asp:button id="BtnRejectReport" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="161px" runat="server" CausesValidation="False" Text="REJECT_REPORT"
					height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:button>
<asp:button id="BtnProcessedExport" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
					Width="161px" runat="server" CausesValidation="False" Text="PROCESSED_EXPORT"
					height="20px" CssClass="FLATBUTTON" Font-Bold="false" Enabled="False"></asp:button></asp:panel></TD>

	</TR>
	<TR>
		<TD style="HEIGHT: 27px" align="center">&nbsp;</TD>
	</TR>
	<TR>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:panel id="moUpLoadPanel" runat="server" Visible="False">
				<TABLE id="moTableSearch2" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 693px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 67px"
					cellSpacing="0" cellPadding="0" rules="cols" width="693" align="center" bgColor="#fef9ea"
					border="0">
					<TR>
						<TD style="WIDTH: 539px">
							<TABLE style="WIDTH: 680px" cellSpacing="0" cellPadding="0" width="680" border="0">
								<TR>
									<TD style="WIDTH: 159px; HEIGHT: 22px" noWrap align="left">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD style="HEIGHT: 10px" noWrap align="center">&nbsp;&nbsp;&nbsp;</TD>
									<TD></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 159px; HEIGHT: 22px" noWrap align="right">*
										<asp:label id="moFilenameLabel" runat="server">Filename</asp:label>:</TD>
									<TD style="HEIGHT: 10px" noWrap align="left">
                                        <INPUT id="claimFileInput" style="WIDTH: 269px; HEIGHT: 19px" type="file" size="25" name="claimFileInput"
											runat="server"></TD>
									<TD>
										<asp:button id="btnCopyDealerFile_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											Width="236px" runat="server" Text="COPY_CLAIM_INTERFACE_FILE" height="20px" CssClass="FLATBUTTON"
											Font-Bold="false"></asp:button></TD>
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
<uc1:interfaceprogresscontrol id="moInterfaceProgressControl" runat="server"></uc1:interfaceprogresscontrol><asp:button id="btnAfterProgressBar" style="BACKGROUND-COLOR: #fef9ea;display:none;" runat="server" Width="0"
	Height="0"></asp:button>

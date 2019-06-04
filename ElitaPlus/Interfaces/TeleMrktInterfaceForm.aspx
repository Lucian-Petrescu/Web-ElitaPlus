<%@ Page Language="vb" MasterPageFile="~/Navigation/masters/content_default.Master"  AutoEventWireup="false" CodeBehind="TeleMrktInterfaceForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TeleMrktInterfaceForm" title="Telemarketing Interface"%>
<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" ID="cntMain">
    <style type="text/css">
        tr.HEADER th { text-align:center}        
    </style>
    <table id="moTableOuter" style="border:solid 1px #999999;" cellspacing="0" cellpadding="6" width="100%">
			<tr>
			<td style="HEIGHT: 70px" >
				<table id="tblMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 692px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
					cellspacing="0" cellpadding="0" rules="cols" width="600" align="center" bgcolor="#fef9ea"
					border="0">
					<tr>
						<td style="WIDTH: 539px" align = "right">
							<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server" AutoPostBackDD="True"></uc1:MultipleColumnDDLabelControl>
						</td>
					</tr>
					<tr><td>&nbsp;</td></tr>
				</table>
			</td>
		</tr>
	    <tr>
		    <td>
			    <table cellspacing="0" cellpadding="0" width="100%" style="border:none">
				    <tr id="Tr1" iid="trPageSize" runat="SERVER"  style="padding-bottom:5px">
					    <td align="left"><asp:panel id="pnlPagesize" runat="server" Visible="False">
						    <asp:label id="lblPageSize" runat="server">Page_Size</asp:label>: &nbsp;
						    <asp:dropdownlist id="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
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
						    </asp:dropdownlist></asp:panel>
						</td>
					    <td style="text-align:right">
						    <asp:label id="lblRecordCount" Runat="server"></asp:label>
					    </td>
				    </tr>
				    <tr><td>&nbsp;</td></tr>
				    <tr>
					    <td colspan="2">
					        <asp:GridView id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" 
                                cellpadding="1" AllowPaging="True" AllowSorting="True" CssClass="DATAGRID" Visible="true">
                                <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                                <EditRowStyle Wrap="False" CssClass="EDITROW" />
                                <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                                <RowStyle Wrap="False" CssClass="ROW" />
					            <HeaderStyle CssClass="HEADER"/>
                                <Columns>
                                    <asp:TemplateField>
							            <ItemStyle CssClass="CenteredTD" />
							            <ItemTemplate>
								            <asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
									            runat="server" CommandName="EditAction"></asp:ImageButton>
							            </ItemTemplate>
						            </asp:TemplateField>
						            <asp:TemplateField>
							            <ItemStyle CssClass="CenteredTD" />
							            <ItemTemplate>
								            <asp:ImageButton style="cursor:hand;" id="btnSelect" ImageUrl="../Navigation/images/icons/yes_icon.gif"
									            runat="server" CommandName="SelectRecord"></asp:ImageButton>
							            </ItemTemplate>
						            </asp:TemplateField>
						            <asp:BoundField HeaderText="FILENAME" DataField="FILENAME" HeaderStyle-CssClass="CenteredTD"/>
						            <asp:BoundField HeaderText="RECEIVED" DataField="RECEIVED" HeaderStyle-CssClass="CenteredTD" ItemStyle-Width="1%"/>
						            <asp:BoundField HeaderText="COUNTED" DataField="COUNTED" HeaderStyle-CssClass="CenteredTD" ItemStyle-Width="1%"/>
						            <asp:BoundField HeaderText="REJECTED" DataField="REJECTED" HeaderStyle-CssClass="CenteredTD" ItemStyle-Width="1%"/>
						            <asp:BoundField HeaderText="VALIDATED" DataField="VALIDATED" HeaderStyle-CssClass="CenteredTD" ItemStyle-Width="1%"/>
						            <asp:BoundField HeaderText="LOADED" DataField="LOADED" HeaderStyle-CssClass="CenteredTD" ItemStyle-Width="1%"/>
                                </Columns>
                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                            </asp:GridView>                    
                        </td>
				    </tr>
			    </table>
		    </td>
	    </tr>
	    <tr>
		    <td align="left">
			    <hr style="WIDTH: 100%; HEIGHT: 1px" size="1"/>
		    </td>
	    </tr>
	    <tr>
		    <td  style="height: 40px"align="left" >
		        <asp:panel id="moButtonPanel" runat="server">
                    <asp:button id="BtnValidate_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
			            Width="85px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
			            Text="VALIDATE" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
                    <asp:button id="BtnProcess_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
                        Width="132px" runat="server" Font-Bold="false" Visible="false" CssClass="FLATBUTTON" height="20px"
                        Text="PROCESS_RECORDS" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
                    <asp:button id="BtnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
                        Width="140px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
                        Text="DELETE_FILE_FROM_LIST" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
                </asp:panel>
            </td>
	    </tr>	
	    <tr>
		    <td>
		        <asp:panel id="moUpLoadPanel" runat="server">
				    <table id="moTableSearch2" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 693px; BORDER-BOTTOM: #999999 1px solid; HEIGHT:67px"
					    cellspacing="0" cellpadding="0" rules="cols" width="693" align="center" bgcolor="#fef9ea"
					    border="0">
					    <tr>
						    <td style="WIDTH: 539px">
							    <table style="WIDTH: 680px" cellspacing="0" cellpadding="10" width="680" border="0">    								
								    <tr>
									    <td style="WIDTH: 159px; HEIGHT: 22px; text-align:right; white-space:nowrap">
									        *<asp:label id="moFilenameLabel" runat="server">Filename</asp:label>:
									    </td>
									    <td style="HEIGHT: 10px;white-space:nowrap;text-align:left" >
									        <input id="teleMrktFileInput" style="WIDTH: 269px; HEIGHT: 19px" type="file" size="25" name="claimFileInput" runat="server" />
								        </td>
									    <td>
										    <asp:button id="btnCopyDealerFile_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											    Width="200px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px" Text="COPY_TELEMARKETING_FILE"></asp:button>
									    </td>
								    </tr>
								    <tr>
									<td style="WIDTH: 159px; HEIGHT: 22px; text-align:right; white-space:nowrap">
										<asp:label id="moExpectedFileLabel" runat="server">Expected_File</asp:label>:</td>
									<td style="HEIGHT: 10px" noWrap align="left">&nbsp;
										<asp:Label id="moExpectedFileLabel_NO_TRANSLATE" runat="server"></asp:Label></td>
									<td></td>
								    </tr>
							    </table>
						    </td>
					    </tr>
				    </table>
			    </asp:panel>
		    </td>
	    </tr>
    </table>
<uc1:interfaceprogresscontrol id="moInterfaceProgressControl" runat="server"></uc1:interfaceprogresscontrol>
<asp:button id="btnAfterProgressBar" style="BACKGROUND-COLOR: #fef9ea" runat="server" Width="0"
	Height="0"></asp:button>
</asp:Content>

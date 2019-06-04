<%@ Page Language="vb" MasterPageFile="~/Navigation/masters/content_default.Master"  AutoEventWireup="false" CodeBehind="ClaimLoadForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimLoadForm" title="Claim File"%>
<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" ID="cntMain">
    <style type="text/css">
        tr.HEADER th { text-align:center}        
        .style1
        {
            width: 524px;
        }
        .display
        {
            display: none;
        }
    </style>
    <table id="moTablelMain" style="border:solid 1px #999999;" cellspacing="0" cellpadding="6" width="100%">
    <tr><asp:Panel ID="pnlSearch" runat="server">
        <td>
        <table style="border: 1px solid #999999; width:98%; padding:5px; margin:5px; height: 90px;">
                    <tr id="trFileType" runat="server">
                        <td style="text-align:right" width="20%" ><asp:label id="lblFileType" runat="server">FILE_TYPE</asp:label>:</td>
                        <td style="text-align:left" width="80%" class="style1">
                            <asp:DropDownList ID="ddlFileType" runat="server">

                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trCountry" runat="server">
                    <td style="text-align:right">
                        <asp:Label ID="CountryLabel" runat="server" Font-Bold="false">Country:</asp:Label></td>
                    <td class="style1">
                        <asp:DropDownList ID="ddlCountry"  runat="server" Width="194px"
                            AutoPostBack="True">
                        </asp:DropDownList></td>
                    </tr>
                    <tr runat="server">
                    <td style="text-align:right" width="20%">
                        <asp:Label ID="FileNameLabel" runat="server" Font-Bold="false">Filename:</asp:Label></td>
                    <td class="style1" width="80%">
                        <asp:TextBox ID="FileNameTextBox"  runat="server" Width="194px"
                            AutoPostBack="True">
                        </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right;" colspan="2" class="style1">
                            <asp:Button ID="moBtnClearSearch" runat="server" 
                                CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" Text="Clear" />
                            &nbsp;
                            <asp:Button ID="moBtnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
                                Text="Search"  ></asp:Button>
                        </td>
                    </tr>
              </table>
              </td>
         </asp:Panel>
         </tr>
	    <tr>
		    <td>
			    <table cellSpacing="0" cellPadding="0" width="100%" style="border:none">
				    <tr iid="trPageSize" runat="SERVER" style="padding-bottom:5px">
					    <td align="left">
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
						    </asp:dropdownlist>
						</td>
					    <td style="text-align:right">
						    <asp:label id="lblRecordCount" Runat="server"></asp:label>
					    </td>
				    </tr>
				    <tr><td>&nbsp;</td></tr>
				    <tr>
					    <td colspan="2">
					        <asp:GridView id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" 
                                CellPadding="1" AllowPaging="True" AllowSorting="True" CssClass="DATAGRID" Visible="true">
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
                                    <asp:BoundField DataField="FILE_TYPE" HeaderStyle-CssClass="display" ItemStyle-CssClass="display"/>
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
			    <hr style="WIDTH: 100%; HEIGHT: 1px" size="1">
		    </td>
	    </tr>
	    <tr>
		    <td  style="height: 80px"align="left" >
		        <asp:panel id="moButtonPanel" runat="server">
                    <asp:button id="BtnValidate_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
			            Width="85px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
			            Text="VALIDATE" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
                    <asp:button id="BtnProcess_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
                        Width="132px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
                        Text="PROCESS_RECORDS" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
                    <asp:button id="BtnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
                        Width="140px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
                        Text="DELETE_DEALER_FILE" CausesValidation="False" Enabled="False"></asp:button>&nbsp; 
                    <asp:button id="BtnRejectReport" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
                        Width="115px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px"
                        Text="REJECT_REPORT" CausesValidation="False" Enabled="False"></asp:button>&nbsp;                 
                </asp:panel>
            </TD>
	    </tr>	
	    <tr>
		    <td>
		        <asp:panel id="moUpLoadPanel" runat="server">
				    <table id="moTableSearch2" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 693px; BORDER-BOTTOM: #999999 1px solid; HEIGHT:67px"
					    cellSpacing="0" cellPadding="0" rules="cols" width="693" align="center" bgColor="#fef9ea"
					    border="0">
					    <tr>
						    <td style="WIDTH: 539px">
							    <table style="WIDTH: 680px" cellspacing="0" cellpadding="10" width="680" border="0">    								
								    <tr>
									    <td style="WIDTH: 159px; HEIGHT: 22px; text-align:right; white-space:nowrap">
									        *<asp:label id="moFilenameLabel" runat="server">Filename</asp:label>:
									    </td>
									    <td style="HEIGHT: 10px;white-space:nowrap;text-align:left" >
									        <INPUT id="claimFileInput" style="WIDTH: 269px; HEIGHT: 19px" type="file" size="25" name="claimFileInput" runat="server" />
								        </td>
									    <td>
										    <asp:button id="btnCopyDealerFile_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											    Width="236px" runat="server" Font-Bold="false" CssClass="FLATBUTTON" height="20px" Text="COPY CLAIM INTERFACE FILE"></asp:button>
									    </td>
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
    <asp:button id="btnAfterProgressBar" CssClass="display"  runat="server" Width="0" Height="0"></asp:button>

</asp:Content>

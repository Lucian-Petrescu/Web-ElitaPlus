<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="VSCPlanRateForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.VSCPlanRateForm" 
    title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;background-color:#f1f1f1; height:98%; width:100%">
                    <tr><td style="height:7px;" colspan="4"></td></tr>
                    <tr>
                        <td style="vertical-align:text-top; text-align:right;white-space: nowrap;">
                            <asp:Label ID="lblSearchOn" runat="server">SEARCH ON</asp:Label>:
                        </td>
                        <td colspan="3" align="left">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rdoDealer" runat="server" style="margin-right:10px;" TextAlign="right" Text="DEALER" Checked="true" GroupName="rdoSearchOnGroup" onclick="DisableDealer(event.srcElement.checked);" />
                                    </td>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblDealerCode" runat="server">CODE:</asp:Label>
                                    </td>
                                    <td style="white-space: nowrap;">
                                        <asp:Label runat="server" ID="lblDealerName">NAME</asp:Label>:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:text-bottom">
                                        <asp:RadioButton ID="rdoDealerGroup" runat="server" style="margin-right:10px;"  Text="DEALER_GROUP" Checked="false" GroupName="rdoSearchOnGroup" onclick="DisableDealer(event.srcElement.checked);"/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDealerCode" runat="Server" CssClass="FLATTEXTBOX" Columns="25"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="FLATTEXTBOX" Columns="35" ID="txtDealerName"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rdoCompanyGroup" runat="server" style="margin-right:10px;"  Text="COMPANY_GROUP" Checked="false" GroupName="rdoSearchOnGroup" onclick="DisableDealer(!event.srcElement.checked);"/>
                                    </td>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td colspan="4"><hr style="HEIGHT:1px"/></td></tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="Label1" runat="server">PLAN</asp:Label>:
                        </td>
                        <td colspan="3" align="left"> 
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
		                            <td style="white-space: nowrap;"><asp:Label ID="Label2" runat="server">By_Code</asp:Label></td>
		                            <td>&nbsp;&nbsp;</td>
		                            <td style="white-space: nowrap;"><asp:Label ID="Label3" runat="server">By_Description</asp:Label></td>
	                            </tr>
	                            <tr>
	                                <td><asp:DropDownList ID="ddlPlanCode" runat="server" Width="103px"></asp:DropDownList></td>
	                                <td>&nbsp;&nbsp;</td>
                                    <td><asp:DropDownList ID="ddlPlanDesc" runat="server" Width="300px"></asp:DropDownList></td>
	                            </tr>
                            </table>               
                        </td>
                    </tr>
                    
                    <tr>
                                <td style="text-align:right;white-space: nowrap;">
                                    <asp:Label ID="lblEffectiveDate" runat="server">Effective_Date</asp:Label>:                
                                </td>
                                <td align="left">
                                    <asp:textbox id="txtEffectiveDate" runat="server" CssClass="FLATTEXTBOX" Columns="20"></asp:textbox>
                                    <asp:imagebutton id="btnEffectiveDate" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                                </td>
                                <td align="left" style="text-align:right;white-space: nowrap;">
                                    <asp:Label ID="lblVersionNumber" runat="server">VERSION</asp:Label>:                
                                </td>
                                <td align="left">
                                    <asp:textbox id="txtVersionNumber" runat="server" CssClass="FLATTEXTBOX"></asp:textbox>
                                </td>                            
                     </tr>
                     <tr><td colspan="4"><hr style="HEIGHT: 1px"/></td></tr>
                     <tr>
                        <td colspan="4">
                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                <tr>
		                            <td style="white-space:nowrap" align="left">
                                         <asp:RadioButton runat="Server" Text="HIGHEST_VERSION_ONLY" ID="rdoHighestVersionOnly" Checked="true" GroupName="rdoShowResultGroup"/>            
                                         <asp:RadioButton runat="Server" Text="SHOW _ALL" ID="rdoAllVersion" Checked="false" GroupName="rdoShowResultGroup"/>
		                            </td>
		                            <td style="text-align:right;">
                                        <asp:button id="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear"></asp:button>&nbsp;
			                            <asp:button id="btnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" Text="Search"></asp:button>
		                            </td>
		                        </tr>
                            </table>
                        </td>
                     </tr>
                     <tr><td colspan="4" style="height:5px;"></td></tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;<br /><hr style="HEIGHT: 1px"/></td>
        </tr>
        <tr id="trPageSize" runat="SERVER" visible="False">
            <td align="left">
                <asp:label id="lblPageSize" runat="server">Page_Size:</asp:label>&nbsp;
                <asp:dropdownlist id="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
        <tr>
            <td colspan="2">
                <asp:datagrid id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#DEE3E7"
                    BorderColor="#999999" BorderStyle="Solid" CellPadding="1" BorderWidth="1px" AllowPaging="True"
                    AllowSorting="False" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand" CssClass="DATAGRID">
					<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
					<EditItemStyle Wrap="False"></EditItemStyle>
					<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
					<ItemStyle Wrap="False" BackColor="White" HorizontalAlign="Center"></ItemStyle>
					<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <Columns>
						<asp:TemplateColumn>
							<HeaderStyle ForeColor="#12135B"></HeaderStyle>
							<ItemStyle CssClass="CenteredTD" Width="30px"></ItemStyle>
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
									runat="server" CommandName="SelectAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle CssClass="CenteredTD" Width="30px"></ItemStyle>
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/yes_icon.gif"
									runat="server" CommandName="EditAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False">
						    <ItemTemplate>
								<asp:Label id="RateVersionId" runat="server" text='<%# Container.DataItem("ROWNUM")%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="PLAN">
						    <HeaderStyle CssClass="CenteredTD" />
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblGridPlan" runat="server" text='<%#Container.DataItem("PlanCode") & " - " & Container.DataItem("Plan")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="DEALER_GROUP" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="CenteredTD" />
                            <HeaderStyle CssClass="CenteredTD" />
							<ItemTemplate>
                                <asp:Label id="lblGridDEALERGROUP" runat="server" text='<%# Container.DataItem("DEALERGROUPCODE")& " - " & Container.DataItem("DEALERGROUP")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="DEALER" ItemStyle-HorizontalAlign="Center">
						    <ItemStyle CssClass="CenteredTD" />
                            <HeaderStyle CssClass="CenteredTD" />
							<ItemTemplate>
                                <asp:Label id="lblGridDEALERCODE" runat="server" text='<%# Container.DataItem("DEALERCODE") & " - " &  Container.DataItem("DEALERNAME")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="VERSION" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="CenteredTD" />
                            <HeaderStyle CssClass="CenteredTD" />
							<ItemTemplate>
                                <asp:Label id="lblGridVersion" runat="server" text='<%# Container.DataItem("Version")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="EFFECTIVE_DATE" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="CenteredTD" />
                            <HeaderStyle CssClass="CenteredTD" />
							<ItemTemplate>
                                <asp:Label id="EFFECTIVEDATE" runat="server" text='<%# CType(Container.DataItem("EFFECTIVE_DATE"), DateTime).ToString("dd-MMM-yyyy")%>'></asp:Label>
                            </ItemTemplate>
                            <%--START PBI 554831 changes--%>
                            <EditItemTemplate>
                                <asp:TextBox id="txtEffectiveDate" runat="server" text='<%# CType(Container.DataItem("EFFECTIVE_DATE"), DateTime).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                <asp:imagebutton id="btnEffectiveDate" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                            </EditItemTemplate>
                            <%--END --%>
                        </asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="EXPIRATION_DATE" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="CenteredTD" />
                            <HeaderStyle CssClass="CenteredTD" />
							<ItemTemplate>
                                <asp:Label id="lblExpirationDate" runat="server" text='<%# CType(Container.DataItem("EXPIRATION_DATE"), DateTime).ToString("dd-MMM-yyyy") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox id="txtExpirationDate" runat="server" text='<%# CType(Container.DataItem("EXPIRATION_DATE"), DateTime).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                <asp:imagebutton id="btnExpirationDate" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
					</Columns>
					<PagerStyle ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15" Mode="NumericPages"></PagerStyle>					
                </asp:datagrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnSave" TabIndex="186" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE"></asp:Button>&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCancel" TabIndex="185" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="CANCEL"></asp:Button>
    <script type="text/javascript">
        function UpdateList(dest){
            var objS = event.srcElement
            var val = objS.options[objS.selectedIndex].value
            //alert("source: " + val);
            var objD = document.getElementById(dest)
            // alert("Desctination: " + objD.options.length);
            for(i=0; i<objD.options.length; i++){
                if (objD.options[i].value == val){
                    objD.selectedIndex = i;
                    //alert("found i:" + i);
                    break;
                }
            }
        }
    </script>
</asp:Content>


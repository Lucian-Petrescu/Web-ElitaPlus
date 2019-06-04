<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="MessageCodeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MessageCodeForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th { text-align:center}        
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;background-color:#f1f1f1; height:98%; width:100%">
                    <tr><td style="height:7px;" colspan="3"></td></tr>
                    <tr>
                        <td style="text-align:left;white-space:nowrap; vertical-align:middle">
                            <asp:Label ID="lblInvoice" runat="server">MSG_TYPE</asp:Label>:
                        </td>
                        <td style="text-align:left;white-space:nowrap; vertical-align:middle">
                            <asp:Label ID="lblInvDate" runat="server">MSG_CODE</asp:Label>:
                        </td>
                        <td style="text-align:left;white-space:nowrap; vertical-align:middle">
                            <asp:Label ID="Label3" runat="server">UI_PROG_CODE</asp:Label>:
                        </td>                        
                    </tr>
                    <tr>
                        <td style="text-align:left;"><asp:DropDownList ID="ddlSMSGTypeSearch" runat="server" ></asp:DropDownList></td>	                         
                        <td style="text-align:left;"><asp:TextBox runat="server" Columns="12" ID="txtMSGCodeSearch" TabIndex="1"></asp:TextBox></td>
                        <td style="text-align:left;"><asp:TextBox runat="server" Columns="50" ID="txtUIProgCodeSearch" TabIndex="2"></asp:TextBox></td>
                    </tr>
                    <tr><td colspan="3"><hr style="HEIGHT: 1px"/></td></tr>
                    <tr>
                        <td style="text-align:right;" colspan="3">
                            <asp:button id="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear" TabIndex="3"></asp:button>&nbsp;
			                <asp:button id="btnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" Text="Search" TabIndex="4"></asp:button>
                        </td>
                    </tr>
                    <tr><td style="height:5px;" colspan="3"></td></tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:7px;" colspan="2"></td></tr>
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
                <asp:GridView id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" 
                    CellPadding="1" AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
					<HeaderStyle CssClass="HEADER"/>
                    <Columns>
                        <asp:TemplateField>
							<ItemStyle CssClass="CenteredTD" Width="18px" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
									runat="server" CommandName="SelectAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField>
                            <ItemStyle CssClass="CenteredTD" Width="20px" />
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                    runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
						<asp:TemplateField Visible="False">
						    <ItemTemplate>
								<asp:Label id="lblMSGCodeID" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("MSG_CODE_ID"))%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="MSG_TYPE" SortExpression="MSG_TYPE">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblMSGType" runat="server" Visible="True" TEXT='<%# GetMsgTypeDropDownDescription(Container.DataItem("MSG_TYPE"))%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="ddlMSGType" runat="server" Visible="True" Width="200px"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>  
						<asp:TemplateField HeaderText="MSG_CODE" SortExpression="MSG_CODE">
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblMSGCode" runat="server" text='<%#Container.DataItem("MSG_CODE")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtMSGCode" runat="server" Visible="True" Columns="6" MaxLength="5" text='<%#Container.DataItem("MSG_CODE")%>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UI_PROG_CODE" SortExpression="UI_PROG_CODE">
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblUIProgCode" runat="server" text='<%#Container.DataItem("UI_PROG_CODE")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtUIProgCode" runat="server" Visible="True" Columns="50" MaxLength="255" Text='<%# Container.DataItem("UI_PROG_CODE")%>' />
                            </EditItemTemplate>
                        </asp:TemplateField>	
                        <asp:TemplateField HeaderText="MSG_PARAMETER_COUNT" SortExpression="MSG_PARAMETER_COUNT">
						    <ItemStyle CssClass="CenteredTD" Width="85px" />
                            <ItemTemplate>
                                <asp:Label id="lblMsgParamCnt" runat="server" text='<%#Container.DataItem("MSG_PARAMETER_COUNT")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlMSGParamCnt" runat="server" Visible="True" Width="35px">
                                    <asp:ListItem Value="0" >0</asp:ListItem>
                                    <asp:ListItem Value="1" >1</asp:ListItem>
                                    <asp:ListItem Value="2" >2</asp:ListItem>
                                    <asp:ListItem Value="3" >3</asp:ListItem>
                                    <asp:ListItem Value="4" >4</asp:ListItem>
                                    <asp:ListItem Value="5" >5</asp:ListItem>
                                    <asp:ListItem Value="6" >6</asp:ListItem>
                                    <asp:ListItem Value="7" >7</asp:ListItem>
                                    <asp:ListItem Value="8" >8</asp:ListItem>
                                    <asp:ListItem Value="9" >9</asp:ListItem>
                                </asp:DropDownList> 
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MSG_TEXT">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblMSGText" runat="server" Visible="True" Text='<%#TranlateLabel(Container.DataItem("UI_PROG_CODE").ToString) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>					
					</Columns>
					<PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                </asp:GridView>
            </td>
        </tr>
        <asp:Literal runat="server" ID="spanFiller"></asp:Literal>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW"></asp:Button>
    <asp:Button ID="btnSave" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE" Visible="false"></asp:Button>                
    <asp:Button ID="btnCancel" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Cancel" Visible="false"></asp:Button>
    <script language="javascript" type="text/javascript">
        //var objD = document.getElementById("moTableOuter")
        //objD.className = "BorderedTbl";
    </script>
</asp:Content>

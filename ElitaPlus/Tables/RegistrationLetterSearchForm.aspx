<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="RegistrationLetterSearchForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.RegistrationLetterSearchForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        img.sortingarrow { margin-left: 15px;}      
        .sortingarrow { margin-left: 15px; }
    </style>
    <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
        <tr>
            <td valign="top" colspan="2">            
                <table id="moTableSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                    height: 76px" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                    bgcolor="#f1f1f1" border="0">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td valign="top" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                            <tr>
                                                <td align="left" valign="middle" colspan="2">
                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <hr style="height: 1px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap align="right">
                                                </td>
                                                <td nowrap style="text-align: right" colspan="2">
                                                    <asp:Button ID="moBtnClear" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="90px" Text="Clear" CssClass="FLATBUTTON" Height="20px"></asp:Button>&nbsp;
                                                    <asp:Button ID="moBtnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="90px" Text="Search" CssClass="FLATBUTTON" Height="20px"></asp:Button>&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="server">
            <td valign="top" align="left">
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
            <td style="height: 22px; text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">                
                <asp:GridView id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1" AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
					<HeaderStyle CssClass="HEADER"/>
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="18px" CssClass="CenteredTD" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CommandName="Select"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField Visible="False">
						    <ItemTemplate>
								<asp:Label id="lblLetterID" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("REGISTRATION_LETTER_ID"))%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField SortExpression="DEALER_CODE" HeaderText="DEALER_CODE" HeaderStyle-HorizontalAlign="Center">
						<ItemTemplate>  
						   <%#Container.DataItem("DEALER_CODE")%>
					    </ItemTemplate>
					     <ItemStyle HorizontalAlign="Center"></ItemStyle>
						</asp:TemplateField>
						<asp:TemplateField SortExpression="DEALER_NAME"  HeaderText="DEALER_NAME" HeaderStyle-HorizontalAlign="Center" >
						 <ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>  
									  <%#Container.DataItem("DEALER_NAME")%>
						 </ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField SortExpression="LETTER_TYPE" HeaderText="LETTER_TYPE" HeaderStyle-HorizontalAlign="Center">
						 <ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>  
									  <%#Container.DataItem("LETTER_TYPE")%>
						 </ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField SortExpression="NUMBER_OF_DAYS" HeaderText="NUMBER_OF_DAYS" HeaderStyle-HorizontalAlign="Center">
						 <ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>  
									  <%#Container.DataItem("NUMBER_OF_DAYS")%>
						 </ItemTemplate>
						</asp:TemplateField>
					</Columns>
					<PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                </asp:GridView>                
            </td>
            
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW">
    </asp:Button>
</asp:Content>

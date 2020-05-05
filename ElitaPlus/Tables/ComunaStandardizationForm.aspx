<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="ComunaStandardizationForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ComunaStandardizationForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th { text-align:center} 
        tr.LeftAlign td {text-align:left;padding-right:25px;} 
        INPUT.BUTTONSTYLE_EDIT {background-image:url(../Navigation/images/icons/edit2.gif); width: 165px;}         
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center; padding-left:8px; padding-right:8px;">
                <table cellpadding="2" cellspacing="2" border="0" style="BORDER: #999999 1px solid;background-color:#f1f1f1; height:50px; width:100%; padding:0px;">
                            <tr>
                            <td style="HEIGHT: 12px" noWrap align=left width="1%">
                                <asp:label id="moComunaAliasLabel" runat="server">COMUNA_ALIAS</asp:label>:</td>
                            <td style="HEIGHT: 12px" noWrap align=left width="1%">
                                <asp:label id="moComunaLabel" runat="server">COMUNA</asp:label>:</td>
                            </tr>
                            <tr>
                            <td noWrap align=left>
                                <asp:textbox id="moComunaAliasTextBox" runat="server" Width="95%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:textbox></td>
                            <td noWrap align=left>
                                <asp:textbox id="moComunaTextBox" runat="server" Width="99%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:textbox></td>
                            </tr>                
                    <tr>
                        <td style="text-align:right;vertical-align:text-bottom; padding-right:4px;padding-bottom:5px" colspan="3">
                            <asp:Button ID="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear" TabIndex="5" />&nbsp;&nbsp;
                            <asp:Button ID="btnSearch" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" runat="server" Text="Search" TabIndex="4" />
                        </td>
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr><td style="padding-top:5px; padding-bottom:5px;" colspan="2"><hr style="height:1px" /></td></tr>
        <tr id="trPageSize" runat="server" visible="False">
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
                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1" AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
					<HeaderStyle CssClass="HEADER"/>
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="3%" CssClass="CenteredTD" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CommandName="SelectAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="3%" CssClass="CenteredTD" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField Visible="False">
						    <ItemTemplate>
								<asp:Label id="IdLabel" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("COMUNA_ALIAS_ID"))%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="COMUNA_ALIAS" SortExpression="COMUNA_ALIAS">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="moComunaAliasLabel" runat="server" Visible="True" TEXT='<%#Container.DataItem("COMUNA_ALIAS")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="moComunaAliasTextBox" runat="server" Visible="True" Columns="60" MaxLength="255" Text='<%# Container.DataItem("COMUNA_ALIAS")%>' />                                
                            </EditItemTemplate>
                        </asp:TemplateField>  
						<asp:TemplateField HeaderText="COMUNA" SortExpression="COMUNA">
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="moComunaLabel" runat="server" text='<%#Container.DataItem("COMUNA")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="moComunaDropdown" runat="server" Visible="True" Width="150px"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                </asp:GridView>
            </td>
        </tr>
        <asp:Literal runat="server" ID="spanFiller"></asp:Literal>
        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align:left"> 
                <asp:Button ID="btnBack" Visible="false" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="Back"></asp:Button>&nbsp;
                <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW"></asp:Button>
                <asp:Button ID="btnSave" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE" Visible="false"></asp:Button>                
                <asp:Button ID="btnCancel" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Cancel" Visible="false"></asp:Button>
            </td>
         </tr>
    </table>
    <script language="javascript" type="text/javascript">
        //find the height of the iFrame client area
        var h = parent.document.getElementById("Navigation_Content").clientHeight; 
        if (document.getElementById('moTableOuter')){
            document.getElementById('moTableOuter').height = h - 60;
        }        
        if (document.getElementById('tblMain')){
            document.getElementById('tblMain').height = h - 65;
        }                    
    </script>
</asp:Content>

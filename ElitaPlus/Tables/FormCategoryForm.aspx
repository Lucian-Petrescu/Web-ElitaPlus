<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="FormCategoryForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.FormCategoryForm" 
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
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;background-color:#f1f1f1; height:50px; width:100%; padding:0px;">
                    <tr class="LeftAlign">
                        <td style="width:31%;">
                            <asp:Label ID="lblTab" runat="server">TAB</asp:Label>:<br />
                            <asp:dropdownlist id="ddlTab" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="1"></asp:dropdownlist>
                        </td>
                        <td style="width:31%;">
                            <asp:Label ID="lblCode" runat="server">CODE</asp:Label>:<br />
                            <asp:TextBox ID="txtCode" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="80%" TabIndex="2"></asp:TextBox>
                        </td>
                        <td style="padding-right:9px;">
                            <asp:Label ID="lblDesc" runat="server">DESCRIPTION</asp:Label>:<br />
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="80%" TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr><td style="padding-top:5px; padding-bottom:5px;" colspan="3"><hr style="height:1px" /></td></tr>
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
                            <ItemStyle Width="18px" CssClass="CenteredTD" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CommandName="SelectAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="18px" CssClass="CenteredTD" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField Visible="False">
						    <ItemTemplate>
								<asp:Label id="lblFormCategoryID" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("Form_Category_Id"))%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="TAB" SortExpression="Tab_Name">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblTab" runat="server" Visible="True" TEXT='<%#Container.DataItem("Tab_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="ddlTab" runat="server" Visible="True" Width="150px"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>  
						<asp:TemplateField HeaderText="CODE" SortExpression="CODE">
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblCode" runat="server" text='<%#Container.DataItem("CODE")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtCode" runat="server" Visible="True" Columns="35" MaxLength="100" text='<%#Container.DataItem("CODE")%>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DESCRIPTION" SortExpression="DESCRIPTION">
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblDesc" runat="server" text='<%#Container.DataItem("DESCRIPTION")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtDesc" runat="server" Visible="True" Columns="60" MaxLength="255" Text='<%# Container.DataItem("DESCRIPTION")%>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FORM_COUNT">
                            <ItemTemplate>
                                <asp:Label id="lblFormCnt" runat="server" text='<%#Container.DataItem("FORM_COUNT")%>' />
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
    <table width="100%">
        <tr>
            <td style="text-align:left"> 
                <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW"></asp:Button>
                <asp:Button ID="btnSave" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE" Visible="false"></asp:Button>                
                <asp:Button ID="btnCancel" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Cancel" Visible="false"></asp:Button>
            </td>
            <td style="text-align:right"> 
                <asp:Button ID="btnAssignForm" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_EDIT" Text="ASSIGN_FORM_CATEGORY" Visible="true"></asp:Button>
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

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AppObjectForm.aspx.vb" MasterPageFile="~/Navigation/masters/content_default.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AppObjectForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th { text-align:center} 
        tr.LeftAlign td {text-align:left;padding-right:25px;} 
        INPUT.BUTTONSTYLE_IMPORT {background-image:url(../Navigation/images/icons/load_icon.gif); width:270px; height:20px;}         
        INPUT.BUTTONSTYLE_CLEAR {background-image:url(../Navigation/images/icons/clear_icon.gif); width:130px; height:20px;}
    </style>
    <asp:Literal ID="litScriptArray" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
        /*var TabList = ['REPORTS', 'TABLES'];
        var Items1 = ['RPT_CLAIMS|Claims', 'RPT_CONFIGURATION|Configuration', 'RPT_FINANCIAL|Financial', 'RPT_INTERFACES|Interfaces', 'RPT_OTHER|Other', 'RPT_PREMIUM|Premium'];
        var Items2 = ['TBL_ACCOUNTING|Accounting', 'TBL_COMPANYCONFIGURATION|Company Configuration', 'TBL_DEALERCONFIGURATION|Dealer Configuration', 'TBL_GENERAL|General', 'TBL_SERVICENETWORK|Service Network'];
        var TabItems = [Items1, Items2];*/
     </script>
     <script language="javascript" type="text/javascript">
         function SetFormCatList(ddlFormCat) {
             if (ddlFormCat != null && ddlFormCat.options != null) {
                var objS = event.srcElement;
                var valTC = objS.options[objS.selectedIndex].value;
                ddlFormCat.disabled = true;
                var varCat;
                for (var i = 0; i < TabList.length; i++) {
                    if (TabList[i] == valTC) {
                        ddlFormCat.disabled = false;
                        ddlFormCat.options.length = 1
                        for (var j = 0; j < TabItems[i].length; j++) {
                            varCat = TabItems[i][j].split("|");
                            ddlFormCat.options[ddlFormCat.options.length] = new Option(varCat[1], varCat[0], false, false);
                        }
                    }
                }
            }
        }
        function SetFormCatSelection(hiddenField) {
            var objS = event.srcElement;
            hiddenField.value = objS.options[objS.selectedIndex].value;
        }
    </script>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td ><asp:label id="lblAppObjectTabs" runat="server"  Height="18px">Tabs to Add</asp:label></td>
        </tr>
        <tr>
            <td >
                <asp:GridView ID="GridTab" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1" AllowPaging="false" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
					<HeaderStyle CssClass="HEADER"/>
                    <Columns>
                        <asp:BoundField HeaderText="CODE" DataField="CODE" />
						<asp:BoundField HeaderText="ENGLISH" DataField="ENGLISH" />
						<asp:BoundField HeaderText="TAB_ICON_IMG" DataField="TAB_ICON_IMG" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height:30px; vertical-align:bottom">
                <asp:label id="lblNewTabs" runat="server" ForeColor="#12135b">New Tabs:</asp:label>&nbsp;
                <asp:textbox id="txtNewTabs" runat="server" Font-Bold="false" Columns="10"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:label id="lblAddedTabs" runat="server" ForeColor="#12135b">Added Tabs:</asp:label>&nbsp;
                <asp:textbox id="txtAddedTabs" runat="server" Font-Bold="false" Columns="10"></asp:textbox>
            </td>
        </tr>
        <tr><td style="height:30px; vertical-align:middle"><hr style="height:1px" /></td></tr>
        <tr>
            <td ><asp:label id="lblAppObjectForms" runat="server"  Height="18px">Forms to Add</asp:label></td>
        </tr>
        <tr>
            <td >
                <asp:GridView ID="GridForm" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1" AllowPaging="false" CssClass="DATAGRID">
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
								<asp:Label id="lblNewFormID" runat="server" text='<%# GetGuidStringFromByteArrayNullable(Container.DataItem("New_Application_Form_Id"))%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="TAB" SortExpression="Tab">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblTab" runat="server" Visible="True" TEXT='<%#Container.DataItem("Tab")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="ddlTab" runat="server" Visible="True" Width="135px"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
						<asp:TemplateField HeaderText="CODE" SortExpression="CODE">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblFormCode" runat="server" Visible="True" TEXT='<%#Container.DataItem("CODE")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtFormCode" runat="server" Visible="True" Columns="25" MaxLength="255" text='<%#Container.DataItem("CODE")%>' Enabled=false />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ENGLISH" SortExpression="ENGLISH">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblEnglish" runat="server" Visible="True" TEXT='<%#Container.DataItem("ENGLISH")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtEnglish" runat="server" Visible="True" Columns="35" MaxLength="255" text='<%#Container.DataItem("ENGLISH")%>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RELATIVE_URL" SortExpression="RELATIVE_URL">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblRelativeURL" runat="server" Visible="True" TEXT='<%#Container.DataItem("RELATIVE_URL")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtRelativeURL" runat="server" Visible="True" Columns="35" MaxLength="100" text='<%#Container.DataItem("RELATIVE_URL")%>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NAV_ALWAYS_ALLOWED" SortExpression="NAV_ALWAYS_ALLOWED">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblNavAllowed" runat="server" Visible="True" TEXT='<%#Container.DataItem("NAV_ALWAYS_ALLOWED")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="ddlNavAllowed" runat="server" Visible="True" Width="50px">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="N">N</asp:ListItem>
                                    <asp:ListItem Value="Y">Y</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FORM_CATEGORY">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblFormCategory" runat="server" Visible="True" TEXT='<%#Container.DataItem("FormCat_Desc")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList ID="ddlFormCategory" runat="server" Visible="True" Width="135px"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QUERY_STRING">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblQueryString" runat="server" Visible="True" TEXT='<%#Container.DataItem("Query_String")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:textbox ID="txtQueryString" runat="server" Visible="True" Columns="35" MaxLength="255" text='<%#Container.DataItem("Query_String")%>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>                    
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height:40px; vertical-align:bottom">
                <asp:label id="lblNewForms" runat="server" ForeColor="#12135b">New Forms:</asp:label>&nbsp;
                <asp:textbox id="txtNewForms" runat="server" Font-Bold="false" Columns="10"></asp:textbox>&nbsp;&nbsp;&nbsp;
                <asp:label id="lblAddedForms" runat="server" ForeColor="#12135b" >Added Forms:</asp:label>&nbsp;
                <asp:textbox id="txtAddedForms" runat="server" Font-Bold="false" Columns="10"></asp:textbox>
            </td>
        </tr>
        <tr><td style="height:5px">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; margin-top:10px;">
                <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW"></asp:Button>&nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE" Visible="false"></asp:Button>&nbsp;               
                <asp:Button ID="btnCancel" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Cancel" Visible="false"></asp:Button>&nbsp;
            </td>
        </tr>
        <tr><td ><hr style="height:1px" /></td></tr>        
    </table>
    <input type="hidden" id="hiddenFormCatSelect" name="hiddenFormCatSelect" runat="server" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align:left"> 
                <asp:button id="btnImport" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_IMPORT" Text="Import New Objects to" Visible="true"></asp:button>
                <asp:button id="btnClearTables" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" Text="Clear Tables" Visible="true"></asp:button>
            </td>                
        </tr>
    </table>
</asp:Content>
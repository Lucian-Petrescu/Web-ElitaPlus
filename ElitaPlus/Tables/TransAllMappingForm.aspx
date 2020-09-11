<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="TransAllMappingForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TransAllMappingForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet">  
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <script type="text/javascript">
    $(function () {
        $("#tabs").tabs({
            activate: function() {
                var selectedTab = $('#tabs').tabs('option', 'active');
                $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
            },
            active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
        });
    });
    </script>

    <table cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left; padding-left: 67px;">
                <uc1:MultipleColumnDDLabelControl ID="ddlDealer" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                &nbsp;<asp:Label ID="moInboundLABEL" runat="server">FILE_NAME:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moInboundTEXT" runat="server" Style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                &nbsp;<asp:Label ID="moOutputPathLABEL" runat="server">OUTPUT_PATH:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moOutputPathTEXT" runat="server" Style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                &nbsp;<asp:Label ID="moPackageNameLABEL" runat="server">PACKAGE_NAME:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moPackageNameTEXT" runat="server" Style="width: 600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                &nbsp;<asp:Label ID="moNumFilesLABEL" runat="server">NUM_FILES:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moNumFilesTEXT" runat="server" Style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                &nbsp;<asp:Label ID="moLayoutCodeLABEL" runat="server">LAYOUT_CODE:</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="moLayoutCodeCBX" Style="width: 305px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                &nbsp;<asp:Label ID="moFTPSiteLABEL" runat="server">ALTERNATE_OUTPUT_LOCATION:</asp:Label>
            </td>
            <td>
                <asp:DropDownList runat="server" ID="moFTPSiteCBX" Style="width: 305px;">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: right; width: 1%; white-space: nowrap;
                padding-right: 10px;">
                &nbsp;<asp:Label ID="moLogEmailsLABEL" runat="server">LOG_EMAILS:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moLogEmailsTEXT" runat="server" TextMode="MultiLine" Height="50" Style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
                <div id="tabs" class="style-tabs-old" style="border:none;">
                    <ul style="border:none;">
                    <li style="background:#d5d6e4"><a href="#tabsOutput" rel="noopener noreferrer"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">OUTPUT</asp:Label></a></li>
                    </ul>
                    <div id="tabsOutput" style="background:#d5d6e4">
                        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand" AllowPaging="True" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False" CssClass="DATAGRID">
                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                            <RowStyle CssClass="ROW"></RowStyle>
                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
                            <Columns>
                                <asp:TemplateField Visible="true" ShowHeader="false">
                                    <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord" ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DeleteButton_WRITE" runat="server" CausesValidation="False" CommandName="DeleteRecord" ImageUrl="../Navigation/images/icons/trash.gif" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        &gt;
                                        <asp:Label ID="lblTransallMappingOutId" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        &gt;
                                        <asp:Label ID="lblTransallMappingId" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OUTPUT">
                                    <ItemStyle HorizontalAlign="Left" Width="47%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblColOutputMask" runat="server" Visible="True">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtColOutputMask" CssClass="FLATTEXTBOX_TAB" runat="server" Visible="True"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LAYOUT_CODE">
                                    <ItemStyle HorizontalAlign="Left" Width="47%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblColLayoutCode" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlColLayoutCode" runat="server" Width="216px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings PageButtonCount="15" Mode="Numeric"></PagerSettings>
                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                        </asp:GridView>
                        <br>
                        <asp:Button ID="btnNewOutput_WRITE" Style="background-image: url(../Navigation/images/icons/new_icon.gif);
                            cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Width="90px" Font-Bold="false" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                        <asp:Button ID="GridSaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                            cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px" Text="Save" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>&nbsp;
                        <asp:Button ID="GridCancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                            cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px" Text="Cancel" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server">
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Back" Height="20px"></asp:Button>&nbsp;
    <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Save" Height="20px"></asp:Button>&nbsp;
    <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Undo" Height="20px"></asp:Button>&nbsp;
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false" Width="81px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;&nbsp;
    <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="136px" Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
    </asp:Button>
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button>
</asp:Content>
; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button>
</asp:Content>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FileProcessedControllerNew.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.FileProcessedControllerNew" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="uc2" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
<div class="dataContainer" style="margin-left: 0%; margin-top: 0px">
    <table border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px; margin-top: 0px" width="100%"
        class="searchGrid">
        <tr>
            <td colspan="2"></td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="moCountry" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="moCompanyGroup" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="moCompany" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="moDealer" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>

            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="moReference" Visible="false" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
    </table>
</div>

<div class="dataContainer" style="margin-left: 0%">
    <h2 class="dataGridHeader">
        <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
    </h2>
    <div class="Page" runat="server" id="moDataGrid_WRITE1" style="height: 100%; overflow: auto; width: 100%;">
        <asp:GridView ID="moDataGrid" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
            CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <SelectedRowStyle Wrap="False"></SelectedRowStyle>
            <EditRowStyle Wrap="False"></EditRowStyle>
            <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle Wrap="False"></HeaderStyle>
            <Columns>
                <asp:BoundField Visible="False"></asp:BoundField>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                            ImageUrl="../Navigation/images/icons/yes_icon.gif" runat="server" CommandArgument="<%#Container.DisplayIndex %>"
                            CausesValidation="false"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" CommandName="EditRecord"
                            ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CommandArgument="<%#Container.DisplayIndex %>"
                            CausesValidation="false"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FILE_NAME" HeaderText="FILENAME">
                    <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="RECEIVED" HeaderText="RECEIVED">
                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="COUNTED" HeaderText="COUNTED">
                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="BYPASSED">
                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnShowBypassed" runat="server" CommandName="ShowRecordByp" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="REJECTED">
                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnShowRejected" runat="server" CommandName="ShowRecordRej" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VALIDATED">
                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnShowValidated" runat="server" CommandName="ShowRecordVal" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PROCESSED">
                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnShowLoaded" runat="server" CommandName="ShowRecordLod" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField Visible="False" DataField="LAYOUT" HeaderText="LAYOUT">
                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Status">
                    <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="BtnShowStatus" runat="server" CommandName="ShowStatusDesc" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField Visible="False" DataField="Status_Desc"></asp:BoundField>
            </Columns>
            <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
    </div>
    <asp:LinkButton runat="server" ID="dummybutton"></asp:LinkButton>
    <div id="AddNewContainer">
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="dummybutton"
            PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="ModalBackground"
            BehaviorID="addNewModal" PopupDragHandleControlID="pnlPopup" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" Style="display: none; width: 650px;">
            <div id="light" class="overlay_message_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="inline">
                    <ContentTemplate>
                        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtExtendedContent" runat="server" ReadOnly="true" Width="650px"
                                        TextMode="MultiLine" Height="75px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnExtendedContentPopupCancel" runat="server" CssClass="popWindowCancelbtn floatR"
                                Text="CANCEL" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
</div>
<div class="btnZone">
    <asp:Panel ID="moButtonPanel" runat="server" Visible="False">
        <asp:Button ID="BtnValidate_WRITE" runat="server" Text="VALIDATE" CausesValidation="False"
            Enabled="False" SkinID="AlternateLeftButton"></asp:Button>&nbsp;
                <asp:Button ID="BtnLoad_WRITE" runat="server" SkinID="AlternateLeftButton"
                    Text="PROCESS_RECORDS" CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
                <asp:Button ID="BtnDeleteFile_WRITE" runat="server" SkinID="AlternateLeftButton"
                    Text="DELETE_DEALER_FILE" CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
                <asp:Button ID="BtnRejectReport" runat="server" SkinID="AlternateLeftButton" Text="REJECT_REPORT"
                    CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
                <asp:Button ID="BtnProcessedExport" runat="server" SkinID="AlternateLeftButton" Text="PROCESSED_EXPORT"
                    CausesValidation="False" Enabled="False"></asp:Button>
    </asp:Panel>
</div>
<div class="datacontainer">
    <asp:Panel ID="moUpLoadPanel" runat="server" Visible="False">
        <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
            width="100%">
            <tr>
                <td>
                    <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                        width="100%">
                        <tr>
                            <td nowrap align="right">*
                                        <asp:Label ID="moFilenameLabel" runat="server">Filename</asp:Label>:
                            </td>
                            <td nowrap align="left">
                                <input id="FileInput" type="file" name="FileInput" runat="server"/>
                                &nbsp;  
                                <asp:Button ID="btnCopyFile_WRITE" runat="server" Text="COPY" SkinID="PrimaryLeftButton"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="right">
                                <asp:Label ID="moExpectedFileLabel" runat="server">Expected_File</asp:Label>:
                            </td>
                            <td nowrap align="left">&nbsp;
                                        <asp:Label ID="moExpectedFileLabel_NO_TRANSLATE" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<div id="ModalProgressBar" class="overlay">
    <uc2:InterfaceProgressControl ID="moInterfaceProgressControl" runat="server"></uc2:InterfaceProgressControl>
    <asp:Button ID="btnAfterProgressBar" Style="background-color: #fef9ea" runat="server"
        Width="0" Height="0"></asp:Button>
    <div id="fade" class="black_overlay">
    </div>
</div>

<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DealerFileProcessedController_New.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.DealerFileProcessedController_New" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
<div class="dataContainer" style="margin-left:0%; margin-top:0px" >
    <table border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px; margin-top: 0px" width="100%"
        class="searchGrid">
  
        <tr>
           <td colspan="2"></td>
           <td id="Td1" runat="server" rowspan="2">
                <table>
                    <tbody>
                        <Elita:MultipleColumnDDLabelControl runat="server" ID="multipleDealerGrpDropControl" />
                    </tbody>
                </table>
           </td>
        </tr>
        <tr>
          <td width="2%" valign="middle"><asp:RadioButton ID="rdParentFile" runat="server" Visible="False" AutoPostBack="true"></asp:RadioButton></td>
          <td><asp:Label ID="lblParentFile"  runat="server" Text="Parent_File" Visible="False" ></asp:Label></td>
        </tr>
        <tr>
            <td><br /></td>
            <td id="TRPlanCode" runat="server" rowspan="2" colspan="2">
                <table>
                    <tbody>
                        <Elita:MultipleColumnDDLabelControl runat="server" ID="multipleDropControl" />
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
           <td width="2%" valign="middle"><asp:RadioButton ID="rdDealerFile" runat="server" Visible="False" AutoPostBack="true"></asp:RadioButton></td>
        </tr>
    </table>
</div>

<div class="dataContainer"  style="margin-left:0%" >
    <h2 class="dataGridHeader">
        <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_DEALER</asp:Label>
    </h2>
    <div class="Page" runat="server" id="moDataGrid_WRITE1" style="height: 100%; overflow: auto; width:100%;">
    <asp:GridView ID="moDataGrid" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
        CellPadding="1" AutoGenerateColumns="False"  CssClass="dataGrid">
        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
        <EditRowStyle Wrap="False"></EditRowStyle>
        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
        <RowStyle Wrap="False"></RowStyle>
        <HeaderStyle Wrap="False"></HeaderStyle>
        <Columns>
            <asp:BoundField Visible="False" HeaderText="dealerfile_processed_id"></asp:BoundField>
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
            <asp:BoundField DataField="FILENAME" HeaderText="Filename">
                <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="RECEIVED" HeaderText="Received">
                <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="COUNTED" HeaderText="Counted">
                <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Bypassed">
                <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton ID="BtnShowBypassed" runat="server" CommandName="ShowRecordByp" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rejected">
                <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton ID="BtnShowRejected" runat="server" CommandName="ShowRecordRej" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Remaining Rejected">
                <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton ID="BtnShowRemainingRejected" runat="server" CommandName="ShowRecordRemRej" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Validated">
                <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton ID="BtnShowValidated" runat="server" CommandName="ShowRecordVal" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Processed">
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
<%-- <div class="btnZone"> --%>
<div class="btnZone">
    <asp:Panel ID="moButtonPanel" runat="server" Visible="False">
        <asp:Button ID="BtnValidate_WRITE" runat="server" Text="VALIDATE" CausesValidation="False"
            Enabled="False" CssClass="altBtn"></asp:Button>&nbsp;
        <asp:Button ID="BtnLoadCertificate_WRITE" runat="server"  CssClass="altBtn"
            Text="PROCESS_RECORDS" CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
        <asp:Button ID="BtnDeleteDealerFile_WRITE" runat="server"  CssClass="altBtn"
            Text="DELETE_DEALER_FILE" CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
        <asp:Button ID="BtnRejectReport" runat="server"  CssClass="altBtn" Text="REJECT_REPORT"
            CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
        <asp:Button ID="BtnErrorExport" runat="server"  CssClass="altBtn" Text="ERROR_EXPORT"
            CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
        <asp:Button ID="BtnProcessedExport" runat="server"  CssClass="altBtn" Text="PROCESSED_EXPORT"
            CausesValidation="False" Enabled="False"></asp:Button>&nbsp;
        <asp:Button ID="BtnGenerateResponse" runat="server"  CssClass="altBtn" Text="GENERATE_RESPONSE"
            CausesValidation="False" Enabled="False"></asp:Button>
    </asp:Panel>
</div>
<%--  </div> --%>
<div class="datacontainer">
    <asp:Panel ID="moUpLoadPanel" runat="server" Visible="False">
        <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
            width="100%">
            <tr>
                <td>
                    <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                        width="100%">
                        <tr>
                            <td nowrap align="right">
                                *
                                <asp:Label ID="moFilenameLabel" runat="server">Filename</asp:Label>:
                            </td>
                            <td nowrap align="left">
                                <input id="dealerFileInput" type="file" name="dealerFileInput" runat="server"/> &nbsp;                           
                                <asp:Button ID="btnCopyDealerFile_WRITE" CssClass="primaryBtn" runat="server"
                                    Text="COPY_DEALER_FILE"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="right">
                                <asp:Label ID="moExpectedFileLabel" runat="server">Expected_File</asp:Label>:
                            </td>
                            <td nowrap align="left">
                                &nbsp;
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
<uc1:InterfaceProgressControl ID="moInterfaceProgressControl" runat="server"></uc1:InterfaceProgressControl>
<asp:Button ID="btnAfterProgressBar" runat="server" Style="background-color: #fef9ea"
    Width="0" Height="0"></asp:Button>
    <div id="fade" class="black_overlay">
        </div>
    </div> 


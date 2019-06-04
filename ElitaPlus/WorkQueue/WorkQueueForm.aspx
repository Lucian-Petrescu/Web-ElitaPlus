<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkQueueForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.WorkQueueForm" EnableSessionState="True"
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>  
    <script type="text/javascript">        
        $(function () {
            // Disable tabs.
            var disabledTabs = $("input[id$='hdnDisabledTabs']").val().split(',');
            var disabledTabsIndexArr = [];
            $.each(disabledTabs, function () {
                var tabIndex = parseInt(this);
                if (tabIndex != NaN) {
                    disabledTabsIndexArr.push(tabIndex);
                }
            });
            $("#tabs").tabs({
                activate: function() {
                    var selectedTab = $('#tabs').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value,            
                disabled: disabledTabsIndexArr
            });
        });    
    </script>

    <script type="text/jscript">
        //<![CDATA[
        var theForm = document.forms['aspnetForm'];
        if (!theForm) {
            theForm = document.aspnetForm;
        }
           
        function ToggleSelection(ctlCodeDropDown, ctlDescDropDown, change_Desc_Or_Code, lblCaption) {
            var objCodeDropDown = document.getElementById(ctlCodeDropDown);
            var objDescDropDown = document.getElementById(ctlDescDropDown);
            if (change_Desc_Or_Code == 'C') {
                objCodeDropDown.value = objDescDropDown.options[objDescDropDown.selectedIndex].value;
            }
            else {
                objDescDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
            }
            if (lblCaption != '') {
                document.all.item(lblCaption).style.color = '';
            }
        }

        function YesButtonClick() {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = document.getElementById('<%=DeleteButtonId.ClientID %>').Value;
                theForm.__EVENTARGUMENT.value = document.getElementById('<%=DeleteButtonArgument.ClientID %>').Value;
                theForm.submit();
            }
        }

        function ShowDeleteConfirmation(ctrlDeleteButton, commandArgument) {
            document.getElementById('<%=DeleteButtonId.ClientID %>').Value = ctrlDeleteButton;
            document.getElementById('<%=DeleteButtonArgument.ClientID %>').Value = commandArgument;
            return revealModal('ModalCancel');
        }
        //]]>
    </script>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" />
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <input runat="server" name="DeleteButtonId" id="DeleteButtonId" type="hidden" />
    <input runat="server" name="DeleteButtonArgument" id="DeleteButtonArgument" type="hidden"  />
    <div id="ModalCancel" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a></p>
            <table class="formGrid" width="98%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="imgMsgIcon" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png"
                                height="28" />
                        </td>
                        <td id="tdModalMessage" colspan="2" runat="server">
                            <asp:Label ID="lblCancelMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td id="tdBtnArea" nowrap="nowrap" runat="server" colspan="2">
                            <input id="btnModalCancelYes" class="primaryBtn floatR" runat="server" type="button"
                                value="Yes" />
                            <input id="Button1" class="popWindowAltbtn floatR" runat="server" type="button" value="No"
                                onclick="hideModal('ModalCancel');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div class="dataContainer">
        <div class="stepformZone">
            <table width="100%" class="formGrid" border="0" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moWorkQueueNameLabel" Text="WORK_QUEUE_NAME" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moWorkQueueName" SkinID="MediumTextBox" />
                        </td>
                        <td colspan="2" />
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moCompanyLabel" Text="COMPANY" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moCompanyDropDown" SkinID="MediumDropDown">
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="moCompanyText" SkinID="MediumTextBox" ReadOnly="true" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moTimeZoneLabel" Text="TIME_ZONE_NAME" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moTimeZoneDropDown" SkinID="MediumDropDown">
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="moTimeZoneText" SkinID="MediumTextBox" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moEffectiveDateLabel" Text="EFFECTIVE_DATE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox ID="moEffectiveDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="btnEffectiveDate" runat="server" Style="vertical-align: bottom"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moExpirationDateLabel" Text="EXPIRATION_DATE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox ID="moExpirationDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="btnExpirationDate" runat="server" Style="vertical-align: bottom"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moStartItemDelayMinutesLabel" Text="START_ITEM_DELAY_MINUTES" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moStartItemDelayMinutes" SkinID="MediumTextBox" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moTimeToCompleteMinutesLabel" Text="TIME_TO_COMPLETE_MINUTES" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moTimeToCompleteMinutes" SkinID="MediumTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moWorkQueueTypeLabel" Text="PROCESSING_METHOD" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moWorkQueueTypeDropDown" SkinID="MediumDropDown">
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="moWorkQueueTypeText" SkinID="MediumTextBox" ReadOnly="true" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moAdminRoleLabel" Text="ADMIN_ROLE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moAdminRole" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moActionLabel" Text="ACTION" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moActionDropDown" SkinID="MediumDropDown">
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="moActionText" SkinID="MediumTextBox" ReadOnly="true" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moTransformationFileLabel" Text="TRANSFORMATION_FILE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moTransformationFile" SkinID="MediumTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moLockableDataTypeLabel" Text="LOCKABLE_DATA_TYPE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moLockableDataType" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moMaximumReQueueLabel" Text="MAXIMUM_REQUEUE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moMaximumReQueue" SkinID="MediumTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moReQueueDelayLabel" Text="REQUEUE_DELAY" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moReQueueDelay" SkinID="MediumTextBox" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnDisabledTabs" runat="server" Value="" />
        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsSchedules"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">SCHEDULES</asp:Label></a></li>
                <li><a href="#tabsRequeueReason"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">REQUEUE_REASONS</asp:Label></a></li>
                <li><a href="#tabsRedirectReason"><asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">REDIRECT_REASONS</asp:Label></a></li>
            </ul>
            <div id="tabsSchedules">
                <div class="Page" runat="server" id="moSchedulesPage" style="height: 300px; overflow: auto">
                    <asp:GridView runat="server" ID="GridViewSchedules" SkinID="DetailPageGridView" AllowPaging="false"
                        AllowSorting="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="CODE">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moCodeLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="moCodeDropDown" Visible="false" SkinID="SmallDropDown" />
                                    <asp:Label runat="server" ID="moCodeLabel" Visible="false" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moDescriptionLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="moDescriptionDropDown" Visible="false" SkinID="MediumDropDown" />
                                    <asp:Label runat="server" ID="moDescriptionLabel" Visible="false" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EFFECTIVE_DATE">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moEffectiveDateLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="moEffectiveDateText" Visible="false" SkinID="SmallTextBox" />
                                    <asp:ImageButton ID="btnEffectiveDate" runat="server" Style="vertical-align: bottom"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                                    <asp:Label runat="server" ID="moEffectiveDateLabel" Visible="false" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EXPIRATION_DATE">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moExpirationDateLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="moExpirationDateText" Visible="false" SkinID="SmallTextBox" />
                                    <asp:ImageButton ID="btnExpirationDate" runat="server" Style="vertical-align: bottom"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                                    <asp:Label runat="server" ID="moExpirationDateLabel" Visible="false" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="moEdit" CommandName="EditRecord" ImageUrl="~/App_Themes/Default/Images/edit.png" />
                                    <asp:ImageButton runat="server" ID="moDelete" CommandName="DeleteRecord" ImageUrl="~/App_Themes/Default/Images/icon_delete.png" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="SaveRecord" SkinID="PrimaryRightButton">
                                    </asp:Button>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CommandName="CancelRecord"
                                        SkinID="AlternateRightButton"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table class="tabBtnAreaZone" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td />
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnAddNewSchedule_WRITE" SkinID="PrimaryLeftButton"
                                        Text="ADD_NEW" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="tabsRequeueReason">
                <div class="Page" runat="server" id="moReQueueReasonsPage" style="height: 300px;
                    overflow: auto">
                    <asp:GridView runat="server" ID="GridViewReQueueReasons" SkinID="DetailPageGridView"
                        AllowPaging="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="REASON_CODE">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moCodeLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="moCodeDropDown" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moDescriptionLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="moDescriptionDropDown" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="moDelete" CommandName="DeleteRecord" ImageUrl="~/App_Themes/Default/Images/icon_delete.png" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="SaveRecord" SkinID="PrimaryRightButton">
                                    </asp:Button>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CommandName="CancelRecord"
                                        SkinID="AlternateRightButton"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table class="tabBtnAreaZone" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td />
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnAddNewReQueueReason_WRITE" SkinID="PrimaryLeftButton"
                                        Text="ADD_NEW" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="tabsRedirectReason">
                <div class="Page" runat="server" id="moReDirectReasonsPage" style="height: 300px;
                    overflow: auto">
                    <asp:GridView runat="server" ID="GridViewReDirectReasons" SkinID="DetailPageGridView"
                        AllowPaging="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="REASON_CODE">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moCodeLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="moCodeDropDown" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="moDescriptionLabel" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="moDescriptionDropDown" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="moDelete" CommandName="DeleteRecord" ImageUrl="~/App_Themes/Default/Images/icon_delete.png" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="SaveRecord" SkinID="PrimaryRightButton">
                                    </asp:Button>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CommandName="CancelRecord"
                                        SkinID="AlternateRightButton"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table class="tabBtnAreaZone" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td />
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnAddNewReDirectReason_WRITE" SkinID="PrimaryLeftButton"
                                        Text="ADD_NEW" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>        
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" />
        <asp:Button runat="server" ID="btnCopy_WRITE" Text="NEW_WITH_COPY" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnUndo_Write" Text="Undo" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnDelete_WRITE" Text="Delete" SkinID="CenterButton" />
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RoleDetailForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RoleDetailForm"
    EnableSessionState="True" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/jscript">
        //        var theForm = document.forms['aspnetForm'];
        //        if (!theForm) {
        //            theForm = document.aspnetForm;
        //        }

        //        function ToggleSelection(ctlCodeDropDown, ctlDescDropDown, change_Desc_Or_Code, lblCaption) {
        //            var objCodeDropDown = document.getElementById(ctlCodeDropDown);
        //            var objDescDropDown = document.getElementById(ctlDescDropDown);
        //            if (change_Desc_Or_Code == 'C') {
        //                objCodeDropDown.value = objDescDropDown.options[objDescDropDown.selectedIndex].value;
        //            }
        //            else {
        //                objDescDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
        //            }
        //            if (lblCaption != '') {
        //                document.all.item(lblCaption).style.color = '';
        //            }
        //        }

        //        function YesButtonClick() {
        //            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        //                theForm.__EVENTTARGET.value = document.getElementById('<%=DeleteButtonId.ClientID %>').Value;
        //                theForm.__EVENTARGUMENT.value = document.getElementById('<%=DeleteButtonArgument.ClientID %>').Value;
        //                theForm.submit();
        //            }
        //        }

        //        function ShowDeleteConfirmation(ctrlDeleteButton, commandArgument) {
        //            document.getElementById('<%=DeleteButtonId.ClientID %>').Value = ctrlDeleteButton;
        //            document.getElementById('<%=DeleteButtonArgument.ClientID %>').Value = commandArgument;
        //            return revealModal('ModalCancel');
        //        }
    </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>  
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
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
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <input runat="server" name="DeleteButtonId" id="DeleteButtonId" type="hidden" />
    <input runat="server" name="DeleteButtonArgument" id="DeleteButtonArgument" type="hidden" />
    <div id="ModalCancel" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');">
                    <img id="moModalCloseImage" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
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
                            <input class="popWindowAltbtn floatR" runat="server" type="button" value="No"
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
                            <asp:Label runat="server" ID="moRoleCodeLabel" Text="CODE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moRoleCode" SkinID="MediumTextBox" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moRoleDescriptionLabel" Text="DESCRIPTION" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moRoleDescription" SkinID="MediumTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moIhqOnlyLabel" Text="IHQ_ONLY" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moIhqOnly" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moRoleProviderLabel" Text="ROLE_PROVIDER" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moRoleProvider" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <div id="tabs" class="style-tabs" style ="border:none;">
            <ul>
            <li><a href="#tabPermission"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">PERMISSION</asp:Label></a></li>
            </ul>
            <div id="tabPermission" class="style-tab" style="border:#999 1px solid;">
                <table id="moPermissionsTable" cellspacing="0" cellpadding="0" width="300" align="center" border="0"
                    class="formGrid">
                    <tr>
                        <td>
                            <asp:Label ID="moAvailableLabel" runat="server">Available</asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="moSelectedTable" runat="server">Selected</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstAvailablePermission" Style="width: 300px; height: 150px" runat="server">
                            </asp:ListBox>
                        </td>
                        <td>
                            <table id="moPermissionActionTable" style="width: 75px; height: 27px" cellspacing="1" cellpadding="1"
                                width="75" border="0">
                                <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnAddPermissionToSelected" runat="server" Width="55px" Text=">>"
                                            SkinID=""></asp:Button>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnRemovePermission" runat="server" Text="<<" Width="55px" SkinID="">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:ListBox ID="lstSelectedPermission" Style="width: 300px; height: 150px" runat="server">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" />
        <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnUndo_Write" Text="Undo" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnDelete_WRITE" Text="Delete" SkinID="CenterButton" />
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
        
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
</asp:Content>

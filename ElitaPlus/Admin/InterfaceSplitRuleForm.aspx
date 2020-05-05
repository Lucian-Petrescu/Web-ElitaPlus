<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InterfaceSplitRuleForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InterfaceSplitRuleForm" EnableSessionState="True"
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
                        <td id="tdBtnArea" style="white-space:nowrap" runat="server" colspan="2">
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
                            <asp:Label runat="server" ID="moSourceLabel" Text="SOURCE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moSourceDropDown" SkinID="MediumDropDown">
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="moSourceTextBox" SkinID="MediumTextBox" ReadOnly="true" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moSourceCodeLabel" Text="SOURCE_CODE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moSourceCode" SkinID="MediumTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                        <td align="right" nowrap="noWrap">
                            
                        </td>
                        <td nowrap="noWrap">
                           
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <div class="Page" runat="server" id="moRulesPage" style="height: 300px; overflow: auto">
            <asp:GridView runat="server" ID="ChildGrid" SkinID="DetailPageGridView" AllowPaging="false"
                AllowSorting="false" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="FIELD NAME">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="moFieldName" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="moFieldNameDropDown" Visible="false" SkinID="MediumDropDown" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OPERATOR">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="moOperator" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="moOperatorDropDown" Visible="false" SkinID="SmallDropDown" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIELD VALUE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="moFieldValue" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="moFieldValueTextBox" Visible="false" SkinID="MediumTextBox" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SOURCE_CODE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="moSourceCode" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="moSourceCodeTextBox" Visible="false" SkinID="MediumTextBox" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="moEdit" CommandName="EditRecord" ImageUrl="~/App_Themes/Default/Images/edit.png" />
                            <asp:ImageButton runat="server" ID="moDelete" CommandName="DeleteRecord" ImageUrl="~/App_Themes/Default/Images/icon_delete.png" />
                        </ItemTemplate>
                        <EditItemTemplate>
                        <table><tr><td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="SaveRecord" SkinID="PrimaryRightButton">
                            </asp:Button></td><td>
                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CommandName="CancelRecord"
                                SkinID="AlternateRightButton"></asp:LinkButton></td></tr>
                        </table>
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
                            <asp:Button runat="server" ID="btnAddNewRule" SkinID="PrimaryLeftButton"
                                Text="ADD_NEW" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" />
        <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnUndo_Write" Text="Undo" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnDelete_WRITE" Text="Delete" SkinID="CenterButton" />
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="vscModelForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.vscModelForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function GenerateCarCode() {
            'moExternalCarCodeText', 'moCarCodeText', 'moYearText'
            var objExternalCarCode = document.getElementById('<%=moExternalCarCodeText.ClientID%>');
            var objCarCode = document.getElementById('<%=moCarCodeText.ClientID%>');
            var objYear = document.getElementById('<%=moYearText.ClientID%>');
            if (objExternalCarCode.value != '') {
                objCarCode.value = objExternalCarCode.value + '-' + objYear.value;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <%--<Elita:MessageController runat="server" ID="moMessageController" Visible="false" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
   <input runat="server" name="DeleteButtonId" id="DeleteButtonId" type="hidden" />
    <input runat="server" name="DeleteButtonArgument" id="DeleteButtonArgument" type="hidden"  />
    
    <div class="dataContainer">
        <table id="Table1" class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;">
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="moMakeLabel" runat="server">MAKE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moMakeDrop" runat="server" AutoPostBack="True" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moNewClassCodeLable" runat="server">NEW_CLASS_CODE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moClassCodeNewDrop" runat="server" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="moYearLabel" runat="server">YEAR</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moYearText" runat="server" SkinID="MediumTextBox" onChange="GenerateCarCode();"></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moActiveNewLabel" runat="server">ACTIVE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:CheckBox ID="CHK_NEW_ACTIVE" runat="server"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="moModelLabel" runat="server">MODEL</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moModelText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moUsedClassCodeLable" runat="server">USED_CLASS_CODE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moClassCodeUsedDrop" runat="server" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="moEngineVersionLabel" runat="server">ENGINE_VERSION</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moEngineVersionText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moActiveUsedLabel" runat="server">ACTIVE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:CheckBox ID="CHK_USED_ACTIVE" runat="server"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="moCoverageLimitCodeLabel" runat="server">MFG_WARRANTY_LIMIT</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moCoverageLimitCodeDrop" runat="server" AutoPostBack="True"
                        SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moCarCodeLabel" runat="server">CAR_CODE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moCarCodeText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                </td>
                <td align="left" nowrap="nowrap">
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moExternalCarCodeLabel" runat="server">EXTERNAL_CAR_CODE</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moExternalCarCodeText" runat="server" Width="55%" onKeyUp="GenerateCarCode();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                </td>
                <td align="left" nowrap="nowrap">
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                        runat="server">
                </td>
                <td align="right" nowrap="nowrap">
                </td>
                <td align="left" nowrap="nowrap">
                </td>
            </tr>
        </table>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnApply_WRITE" Text="Save" SkinID="PrimaryRightButton" />
        <asp:Button runat="server" ID="btnCopy_WRITE" Text="NEW_WITH_COPY" SkinID="AlternateRightButton"
            CausesValidation="False" />
        <asp:Button runat="server" ID="btnUndo_WRITE" Text="Undo" SkinID="AlternateRightButton"
            CausesValidation="False" />
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton"
            CausesValidation="False" />
        <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateLeftButton"
            CausesValidation="False" />
        <asp:Button runat="server" ID="btnDelete_WRITE" Text="Delete" SkinID="CenterButton"
            CausesValidation="False" />
    </div>
</asp:Content>

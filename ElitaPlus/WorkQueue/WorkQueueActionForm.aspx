<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkQueueActionForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.WorkQueueActionForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
   <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblItemAssignmentTimestamp" runat="server" SkinID="SummaryLabel">QUERY_ITEM_ASSIGN_TIME</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblItemAssignmentTimestampValue" runat="server"></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblQueueName" runat="server" SkinID="SummaryLabel">QUEUE_NAME</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblQueueNameValue" runat="server" SkinID="SummaryLabel"></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblCompany" runat="server" SkinID="SummaryLabel">COMPANY</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="padRight">
                <asp:Label ID="lblCompanyValue" runat="server" SkinID="SummaryLabel"></asp:Label>
            </td>
        </tr>
    </table>
   </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="mcWorkQueueAction" Visible="false"></Elita:MessageController>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<asp:PlaceHolder ID="PlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <asp:Xml ID="xmlSource" runat="server"></asp:Xml>
    <div class="btnZone" runat="server" id="btnZone">
        <asp:Button ID="btnRedirect" runat="server" Text="REDIRECT" SkinID="PrimaryRightButton" OnClientClick="return revealModal('ModalRedirectReason');"></asp:Button>
        <asp:Button ID="btnRequeue" runat="server" Text="REQUEUE" SkinID="PrimaryRightButton" OnClientClick="return revealModal('ModalRequeueReasons');"></asp:Button>
        <asp:Button ID="btnProceed" runat="server" SkinID="PrimaryRightButton" Text="PROCEED"></asp:Button>
    </div>
    <div id="ModalRequeueReasons" class="overlay">
        <div id="light" class="overlay_message_content" style="width: 500px">
            <p class="modalTitle">
                <asp:Label ID="lblRequeueReasons" runat="server" Text="REQUEUE_REASONS"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalRequeueReasons');" rel="noopener noreferrer">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server" width="16" height="18" align="absmiddle" class="floatR"></a>
            </p>
            <div class="dataContainer" style="margin-bottom: 0px">
                <div runat="server" id="modalMessageBoxRequeue" class="errorMsg" style="display: none;
                    width: 80%">
                    <p>
                        <img id="imgRequeueReasonMsg" width="16" height="13" align="middle" runat="server" src="~/App_Themes/Default/Images/icon_error.png">
                        <asp:Literal runat="server" ID="msgRequeueReasons"></asp:Literal>
                    </p>
                </div>
            </div>
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <span class="mandatory">*</span><asp:Label ID="lblRequeueReason" runat="server"></asp:Label>:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rdbtlstRequeueReasons" runat="server" CssClass="formGrid"></asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td id="tdBtnArea" nowrap="nowrap" runat="server" colspan="2">
                        <asp:Button ID="btnRequeueContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue"></asp:Button>
                        <input id='btnRequeueCancel' runat="server" type="button" name="Cancel" value="Cancel" class='popWindowCancelbtn floatR'>
                    </td>
                </tr>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalRedirectReason" class="overlay">
        <div id="light" class="overlay_message_content" style="width: 500px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="inline" ChildrenAsTriggers="True">
                <ContentTemplate>
                    <p class="modalTitle">
                        <asp:Label ID="lblRedirectModalTitle" runat="server"></asp:Label>
                        <a href="javascript:void(0)" onclick="hideModal('ModalRedirectReason');" rel="noopener noreferrer">
                            <img id="Img2" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server" width="16" height="18" align="absmiddle" class="floatR"></a>
                    </p>
                    <div class="dataContainer" style="margin-bottom: 0px">
                        <div runat="server" id="modalMessageBoxRedirect" class="errorMsg" style="display: none;
                            width: 80%">
                            <p>
                                <img id="imgRedirectReasonMsg" width="16" height="13" align="middle" runat="server" src="~/App_Themes/Default/Images/icon_error.png">
                                <asp:Literal runat="server" ID="msgRedirectReasons"></asp:Literal>
                            </p>
                        </div>
                    </div>
                    <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <span class="mandatory">*</span><asp:Label ID="lblQueueToRedirect" runat="server"> : </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWorkQueueList" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                        <tr id="trLblRedirectRsn" runat="server">
                            <td>
                                <span class="mandatory">*</span><asp:Label ID="lblRedirectReasons" runat="server"></asp:Label>:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rdbtRedirectRsn" runat="server" CssClass="formGrid"></asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlWorkQueueList" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnRedirectContinue" runat="server" CssClass="primaryBtn floatR" Text="Continue" CausesValidation="false"></asp:Button>
                        <input id="btnRedirectCancel" runat="server" type="button" name="Cancel" value="Cancel" class='popWindowCancelbtn floatR'>
                    </td>
                </tr>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <asp:HiddenField ID="hdnSelectedReason" runat="server"></asp:HiddenField>
    <script type="text/javascript">
        function validate(ctrlRadioButtonList, ctrlMessageBox)
        {
            var flag = false;
            var lstRadioButtons = document.getElementById(ctrlRadioButtonList);

            if (lstRadioButtons != null)
            {
                var inputs = lstRadioButtons.getElementsByTagName("input");

                for (var x = 0; x < inputs.length; x++)
                {
                    if (inputs[x].checked)
                    {
                        flag = true;
                        var hdnSelectedReason = document.getElementById('<%=hdnSelectedReason.ClientID %>');
                        hdnSelectedReason.value = inputs[x].value;
                    }
                }
            }

            if (flag == false)
            {
                var msgBox = document.getElementById(ctrlMessageBox);
                msgBox.style.display = 'block';
                return false;
            }

            return true;
        }

        function HideErrorAndModal(divId, ctrlradionButtonList, ctrlMessageBox)
        {
            var msgBox = document.getElementById(ctrlMessageBox);
            msgBox.style.display = 'none';
            var lstRadioButtons = document.getElementById(ctrlradionButtonList);
            if (lstRadioButtons != null)
            {
                var inputs = lstRadioButtons.getElementsByTagName("input");
                for (var x = 0; x < inputs.length; x++)
                {
                    if (inputs[x].checked)
                    {
                        inputs[x].checked = false;
                    }
                }
            }
            hideModal(divId);
        }

        function UncheckRadioButton(ctrlradionButtonList)
        {
            var lstRadioButtons = document.getElementById(ctrlradionButtonList);
            if (lstRadioButtons != null)
            {
                var inputs = lstRadioButtons.getElementsByTagName("input");
                for (var x = 0; x < inputs.length; x++)
                {
                    if (inputs[x].checked)
                    {
                        inputs[x].checked = false;
                    }
                }
            }
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args)
        {
            document.body.style.cursor = 'default'
        }

        


    </script>
   </asp:PlaceHolder>
</asp:Content>
</asp:PlaceHolder>
</asp:Content>

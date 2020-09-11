<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CaseRecordingForm.aspx.vb"
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CaseRecordingForm" MasterPageFile="~/Navigation/masters/ElitaBase.Master"
Theme="Default" EnableSessionState="True"%>

<%@ Register TagPrefix="Elita" TagName="CaseHeaderInformation" Src="~/Common/UserControlCaseHeaderInfo.ascx" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="Elita" TagName="UserControlQuestion" Src="../Common/UserControlQuestion.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCallerInfo" Src="../Common/UserControlCallerInfo.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .ModalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.5;
        }

        .black_show {
            cursor: wait;
            position: absolute;
            top: 0;
            left: 0;
            background-color: #777777;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .75;
            filter: alpha(opacity=90);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:CaseHeaderInformation ID="ucCaseHeaderInformation" runat="server" align="center" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div id="ModalCancel" class="overlay">
        <div class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');" rel="noopener noreferrer">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server" width="16" height="18" align="absmiddle" class="floatR" alt="" />
                </a>
            </p>
            <table class="formGrid" width="98%">
                <tbody>
                <tr>
                    <td>
                        <img id="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png" height="28" />
                    </td>
                    <td id="tdModalMessage" colspan="2" runat="server">
                        <asp:Label ID="lblCancelMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td id="tdBtnArea" runat="server" colspan="2">
                        <asp:Button ID="btnCancelYes" class="primaryBtn floatR" runat="server" Text="Yes"></asp:Button>
                        <input id="btnModalCancelNo" class="popWindowAltbtn floatR" runat="server" type="button"
                               value="No" onclick="hideModal('ModalCancel');" />
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <asp:MultiView ID="mvClaimsRecording" runat="server" ActiveViewIndex="0">
        <asp:View ID="vCaller" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader" runat="server" id="hCallerinformation">
                    <asp:Label runat="server" ID="lblCallerInformation" Text="CALLER_INFORMATION"></asp:Label></h2>
                <div style="width: 100%">
                    <table style="width: 100%; height: 100%" class="dataGrid">
                        <tr>
                            <td class="bor" align="left">
                                <asp:Label ID="lblPurpose" runat="server">Purpose_Type</asp:Label>:
                                <asp:Label ID="LabelPurposeValue" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <Elita:UserControlCallerInfo runat="server" ID="ucCallerInfo" />
                            </td>
                        </tr>                        

                    </table>
                </div>
            </div>
            <div class="dataContainer">
                <h2 class="dataGridHeader" runat="server" id="hprevCallerinformation">
                    <asp:Label runat="server" ID="lvlPreCaller" Text="PREVIOUS_CALLER_INFORMATION"></asp:Label></h2>
                <div style="width: 100%">
                    <table style="width: 100%; height: 100%" class="dataGrid">                                            
                        <tr>
                            <td style="text-align: center">
                                <Elita:UserControlCallerInfo runat="server" ID="ucPrevCallerInfo" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:View>                                
        <asp:View ID="vQuestion" runat="server">
            <div class="dataContainer">
                <Elita:UserControlQuestion runat="server" ID="questionUserControl" />
            </div>
        </asp:View>
        <asp:View ID="vCaseInteraction" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader" runat="server" id="hCaseInteraction">
                    <asp:Label runat="server" ID="lblCaseInteraction" Text="CASE_INTERACTION"></asp:Label></h2>
                <div style="width: 100%">
                    <table id="tblCaseInteraction" class="formGrid" width="100%" border="0" runat="server">
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <span class="mandate">*</span><asp:Label ID="LabelCaseNote" runat="server" Text="CASE_NOTE"></asp:Label>:
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextBoxCaseNotes" runat="server" SkinID="LargeTextBox" TextMode="MultiLine" Rows="10" Style="white-space: pre-wrap; width: 540px;" Columns="40" MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelCaseDispositionReason" runat="server" Text="CASE_DISPOSITION_REASON"></asp:Label>:
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlCaseDispositionReason" runat="server" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <div class="btnZone">
        <asp:Button ID="button_Continue" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
        <asp:LinkButton ID="btn_Cancel" runat="server" SkinID="TabZoneAddButton" Text="Cancel"
                        OnClientClick="return revealModal('ModalCancel');" />
    </div>

</asp:Content>

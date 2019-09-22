<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimForm"
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True"%>

<%@ Register TagPrefix="uc1" TagName="UserControlContactInfo" Src="../Common/UserControlContactInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlClaimInfo" Src="UserControlClaimInfo.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlSelectServiceCenter" Src="~/Certificates/UserControlSelectServiceCenter.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlConsequentialDamage" Src="UserControlConsequentialDamage.ascx" %>
<%@ Register assembly="Microsoft.Web.UI.WebControls" namespace="Microsoft.Web.UI.WebControls" tagprefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .style1
        {
            height: 20px; 
        }

        /* commented because IE 11 doesn't care about :not() and therefore has a different behaviour than Firefox, Chrome and others */
        /*li.ui-state-default.ui-state-hidden[role=tab]:not(.ui-tabs-active) {
            display: none;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:UserControlClaimInfo ID="moClaimInfoController" runat="server" align="center" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManagerMaster" runat="server" />
    <script type="text/javascript">
        //debugger;
        var newRptWin
        function mywindowOpen(url) {
            var windowProperties = "width = " + (screen.width - 100) + ", height = " + (screen.height - 200) + ", toolbar = no, location = no, status = yes, resizable = yes, scrollbars = yes";
            newRptWin = window.open(url, "", windowProperties);
            newRptWin.moveTo(50, 90);
        }

        function toglePassCode() {
            var chkBox = document.getElementById("chkPasscode");
            var txtBox = document.getElementById("txtPasscode");
            if (chkBox.checked == true) {
                txtBox.style.display = "block";
            } else {
                txtBox.style.display = "none";
            }
        }
               
        $(function () {
            var disabledTabs = $("input[id$='hdnDisabledTab']").val().split(',');
            $.each(disabledTabs, function () {
                var tabIndex = parseInt(this);
                if (tabIndex != NaN) {
                    $($("#tabs").find("li")[tabIndex]).addClass('ui-state-hidden');
                }
            });            
        });
    </script>
    <div id="ModalChangeServiceCenter" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblChangeServiceCenterPrompt" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalChangeServiceCenter');">
                    <img id="Img2" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a></p>
            <table class="formGrid" width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="imgMsgIcon" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png"
                                height="28" />
                        </td>
                        <td id="tdModalMessage" colspan="2" runat="server">
                            <asp:Label ID="lblChangeServiceCenterMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td id="tdBtnArea" nowrap="nowrap" runat="server" colspan="2">
                           <asp:Button ID="btnModalSelectServiceCenterYes" runat="server" TEXT="YES" SkinID="PrimaryRightButton"  />
                           <asp:Button ID="btnModalSelectServiceCenterNo" runat="server" TEXT="NO"  SkinID="AlternateRightButton"  />                           
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalServiceCenter" class="overlay">
        <div id="light" class="overlay_message_content" style="left:5%;right:5%;top:5%;max-height:80%">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="SEARCH_SERVICE_CENTER"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalServiceCenter');">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a></p>
            <Elita:UserControlSelectServiceCenter ID="ucSelectServiceCenter" runat="server" />
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalReopenClaim" class="overlay">
        <div id="Div1" class="overlay_message_content">
            <p class="modalTitle">
            <table width ="30%"><tr><td align ="left">
                <asp:Label ID="Label1" runat="server" Text="CONFIRM"></asp:Label></td><td align="right">
                <a href="javascript:void(0)" onclick="hideModal('ModalReopenClaim');">
                    <img id="Img3" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle"/></a></p></td></tr></table>
            <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="img4" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png"
                                height="28" />
                        </td>
                        <td id="td1" colspan="2" runat="server">
                            <asp:Label ID="lblReopenMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td id="td2" nowrap="nowrap" runat="server" colspan="2">
                            <input id="btnModalReopenClaimYes" class="primaryBtn floatR" runat="server" type="button"
                                value="Yes" />
                            <input id="btnModalReopenClaimNo" class="popWindowAltbtn floatR" runat="server" type="button"
                                value="No" onclick="hideModal('ModalReopenClaim');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="Div2" class="black_overlay">
        </div>
    </div>
    <asp:Panel ID="Panel2" runat="server" Height="98%" Width="100%">
        <div class="dataContainer">
            <div class="stepformZone">
                <asp:Panel ID="ViewPanel_READ" runat="server" Height="40%" Width="100%">
                    <table class="formGrid" width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td nowrap="nowrap" align="right" width="18%">
                                    <asp:Label ID="LabelMasterClaimNumber" runat="server">Master_Claim_Number</asp:Label>
                                </td>
                                <td nowrap="nowrap" width="30%">
                                    <asp:TextBox ID="TextboxMasterClaimNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right" width="15%">
                                    <asp:Label ID="LabelLoanerCenter" runat="server">Loaner_Center</asp:Label>
                                </td>
                                <td nowrap="nowrap" width="37%">
                                    <asp:TextBox ID="TextboxLoanerCenter" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelClaimNumber" runat="server">Claim_Number</asp:Label>:
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxClaimNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelRiskType" runat="server">Risk_Type</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxRiskType" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCallerName" runat="server">Name_Of_Caller</asp:Label>:
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCallerName" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelServiceCenter" runat="server">Service_Center</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxServiceCenter" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelMethodOfRepair" runat="server">Method_Of_Repair</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxMethodOfRepair" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCaller_Tax_Number" runat="server">CALLER_TAX_NUMBER</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCaller_Tax_Number" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelLossDate" runat="server">Date_Of_Loss</asp:Label>:
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxLossDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelContactName" runat="server">Contact_Name</asp:Label>:
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxContactName" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCauseOfLoss" runat="server">Cause_Of_Loss</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboCauseOfLossId" TabIndex="1" runat="server" SkinID="MediumDropDown"
                                        Enabled="false" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCertificateNumber" runat="server">Certificate</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCertificateNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDateReported" runat="server">DATE_REPORTED</asp:Label>:
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxReportedDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDealerName" runat="server">Dealer</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDealerName" TabIndex="-1" runat="server" ReadOnly="true"
                                        SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCoverageType" runat="server">Coverage_Type</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCoverageType" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="Lbluseshipaddress" runat="server" Visible="false">Use_Ship_Address:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cbousershipaddress" TabIndex="1" runat="server" SkinID="SmallDropDown"
                                        AutoPostBack="true" Visible="false">
                                    </asp:DropDownList>
                                    <asp:Label ID="LabelNotificationType" runat="server">Notification_Type</asp:Label>
                                    <asp:TextBox ID="TextboxNotificationType" runat="server" SkinID="SmallTextBox" ReadOnly="True"
                                        TabIndex="-1"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCurrentRetailPrice" runat="server">Registered_Device_Current_Price</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="txtCurrentRetailPrice" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="lblPaymentPassedDue" runat="server">Payment_Passed_Due</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="txtPaymentPassedDue" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                                                  ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelAuthorizationNumber" runat="server">SVC_REFERENCE_NUMBER</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxAuthorizationNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelSource" runat="server">Source</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxSource" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelStoreNumber" runat="server">Store_Number</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxStoreNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right"></td>
                                <td nowrap="nowrap">&nbsp;</td>
                            </tr>
                            <tr runat="server" id="trUseEquipment1">
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCurrentSKU" runat="server">Current_Device_SKU:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCurrentDeviceSKU" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelPrice" runat="server">Price:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxPrice" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="trUseEquipment2">
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelMobileNumber" runat="server">Mobile_Number:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxMobileNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelSerialNumber" runat="server">Serial_Number:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxSerialNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="trUseEquipment3">
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelManufacturer" runat="server">Manufacturer:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxManufacturer" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelModel" runat="server">Model:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxModel" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right"  width="18%">
                                    <asp:Label ID="LabelCurrentOdometer" runat="server">CURRENT_ODOMETER</asp:Label>
                                </td>
                                <td nowrap="nowrap"  width="30%">
                                    <asp:TextBox ID="TextboxCurrentOdometer" TabIndex="-1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right"  width="15%">
                                    <asp:Label ID="LabelLiabilityLimit" runat="server">Liability_Limit</asp:Label>
                                </td>
                                <td nowrap="nowrap"  width="37%">
                                    <asp:TextBox ID="TextboxLiabilityLimit" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelLastModifiedBy" runat="server">OPERATOR_LAST_MAINTAINED</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxLastModifiedBy" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDeductible" runat="server">Deductible</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDeductible" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelClaimsAdjuster" runat="server">Claims_Adjuster</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxClaimsAdjuster" TabIndex="-1" runat="server" ReadOnly="true"
                                        SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDiscount" runat="server">DISCOUNT</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDiscount" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelUserName" runat="server">User_Name</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxUserName" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelAboveLiability" runat="server">Above_Liability</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxAboveLiability" runat="server" SkinID="MediumTextBox" ReadOnly="True"
                                        TabIndex="-1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelAddedDate" runat="server">Date_Added</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxAddedDate" TabIndex="-1" runat="server" ReadOnly="true" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelSalvageAmount" runat="server">Salvage_Amount</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxSlavageAmount" runat="server" SkinID="MediumTextBox" ReadOnly="True"
                                        TabIndex="-1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelLastModifiedDate" runat="server">DATE_LAST_MAINTAINED</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxLastModifiedDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelBonusAmount" runat="server">BONUS_AMOUNT</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxBonusAmount" runat="server" SkinID="MediumTextBox" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelStatusCode" runat="server">Claim_Status</asp:Label>:
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxStatusCode" TabIndex="-1" runat="server" Width="19px" ReadOnly="True"></asp:TextBox>&#160;
                                    <asp:TextBox ID="TextboxClaimStatus" runat="server" Width="263px" ReadOnly="True"
                                        TabIndex="-1" Visible="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelAssurantPays" runat="server">Assurant_Pay</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxAssurantPays" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr runat="server" id="trDueToSCFromAssurant">
                                <td colspan="1">
                                </td>
                                <td colspan="2" align="right" style="height: 18px">
                                    <asp:Label ID="LabelDueToSCFromAssurant" runat="server">DUE_TO_SC_FROM_ASSURANT</asp:Label>
                                </td>
                                <td style="height: 18px">
                                    <asp:TextBox ID="TextboxDueToSCFromAssurant" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelSpecialService" runat="server">Special_Service</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxSpecialService" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True">
                                    </asp:TextBox>
                                </td>
                               <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelConsumerPays" runat="server">Consumer_Pay</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxConsumerPays" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2"> </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelTotalPaid" runat="server">Total_Paid</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxTotalPaid" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="trDedCollection">
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelDedCollMethod" runat="server">DED_COLLECTION</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDedCollMethod" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelCCAuthCode" runat="server">CC_AUTH_CODE</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCCAuthCode" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelPolicyNumber" runat="server">Policy_Number</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxPolicyNumber" runat="server" SkinID="MediumTextBox" TabIndex="1"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelWhoPays" runat="server">Who_Pays</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboWhoPays" TabIndex="1" runat="server" SkinID="MediumDropDown"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelDEVICE_ACTIVATION_DATE" runat="server">DEVICE_ACTIVATION_DATE</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDEVICE_ACTIVATION_DATE" runat="server" ReadOnly="True" SkinID="MediumTextBox" TabIndex="-1"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonDeviceActivationDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="labelEMPLOYEE_NUMBER" runat="server">EMPLOYEE_NUMBER</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxEMPLOYEE_NUMBER" runat="server" ReadOnly="True" SkinID="MediumTextBox" TabIndex="-1"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="ViewPanel_READ1" runat="server" Width="100%" Height="18%">
                    <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnDisabledTab" runat="server" />
                    <div id="tabs" class="style-tabs">
                      <ul>
                        <li><a href="#tbDeviceInfo"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">DEVICE_INFORMATION</asp:Label></a></li>
                        <li><a href="#tbClaimShippingInfo"><asp:Label ID="Label3" runat="server" CssClass="tabHeaderText">CLAIM_SHIPPING</asp:Label></a></li>  
                        <li><a href="#tbClaimAuthorization"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">CLAIM_AUTHORIZATIONS</asp:Label></a></li>                        
                        <li><a href="#tbClaimConsequentialDamage"><asp:Label ID="Label2" runat="server" CssClass="tabHeaderText">CLAIM_CONSEQUENTIAL_DAMAGE</asp:Label></a></li>  
                        <li><a href="#tbClaimFulfillmentDetails"><asp:Label ID="lblClaimFulfillmentDetails" runat="server" CssClass="tabHeaderText">CLAIM_FULFILLMENT</asp:Label></a></li>  
                      </ul>
          
                      <div id="tbDeviceInfo">
                         <div class="Page">
                             <table>
                                 <tr>
                                     <td>
                                         <div id="dvClaimEquipment" runat="server">
                                            <table class="formGrid" width="66%">
                                        <tr>
                                            <td align="right"  width="15%">
                                            </td>
                                            <td align="left" width="18%">
                                                <asp:Label ID="lblEnrolledDevice" runat="server" Text="ENROLLED_DEVICE_INFO" Font-Bold="True"> </asp:Label>
                                            </td>
                                            <td align="right" width="15%">
                                            </td>
                                            <td align="left" width="18%">
                                                <asp:Label ID="lblClaimedDevice" runat="server" Text="CLAIMED_DEVICE_INFO" Font-Bold="True"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LBLeNROLLEDmAKE" runat="server" Text="MAKE"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEnrolledMake" runat="server" CssClass="FLATTEXTBOX" Enabled="false"
                                                    SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblClaimedMake" runat="server" Text="MAKE"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtClaimedMake" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                    SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblEnrolledModel" runat="server" Text="MODEL"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEnrolledModel" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                    SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblClaimedModel" runat="server" Text="MODEL"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtClaimedModel" runat="server" Enabled="false" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblEnrolledSerialNumber" runat="server" CssClass="FLATTEXTBOX" Text="SERIAL_NUMBER"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtenrolledSerial" runat="server" Enabled="false" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblClaimedSerialNumber" runat="server" CssClass="FLATTEXTBOX" Text="SERIAL_NUMBER"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtClaimedSerial" runat="server" Enabled="false" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblEnrolledSKu" runat="server" Text="SKU_NUMBER"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEnrolledSku" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                    SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblClaimedSKu" runat="server" Text="SKU_NUMBER"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtClaimedSku" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                    SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblEnrolledIMEI" runat="server" Text="IMEI_NUMBER"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEnrolledIMEI" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                             SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblClaimedIMEI" runat="server" Text="IMEI_NUMBER"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtClaimedIMEI" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                             SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblEnrolledComments" runat="server" Text="COMMENTS"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEnrolledComments" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                                             TextMode="MultiLine" ReadOnly="false" Rows="5"></asp:TextBox>
 
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblClaimedComments" runat="server" Text="COMMENTS"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtClaimedComments" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                                             TextMode="MultiLine" ReadOnly="false" Rows="5"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                         </div>
                                     </td>
                                     <td>
                                          <div id="dvRefurbReplaceEquipment" runat="server">
                                            <table class="formGrid" width="64%">
                                                    <tr>
                                                        <td align="right" width="15%">
                                                        </td>
                                                        <td align="left" width="19%">
                                                            <asp:Label ID="lblRefurbReplaceDevice" runat="server" Text="REFURB_REPLACE_DEVICE_INFO" Font-Bold="True"> </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblRefurbReplaceMake" runat="server" Text="MAKE"> </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRefurbReplaceMake" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                                         SkinID="MediumTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblRefurbReplaceModel" runat="server" Text="MODEL"> </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRefurbReplaceModel" runat="server" Enabled="false" SkinID="MediumTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblRefurbReplaceSerialNumber" runat="server" CssClass="FLATTEXTBOX" Text="SERIAL_NUMBER"> </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRefurbReplaceSerial" runat="server" Enabled="false" SkinID="MediumTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblRefurbReplaceSKu" runat="server" Text="SKU_NUMBER"> </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRefurbReplaceSku" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                                         SkinID="MediumTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblRefurbReplaceIMEI" runat="server" Text="IMEI_NUMBER"> </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRefurbReplaceIMEI" runat="server" Enabled="false" CssClass="FLATTEXTBOX"
                                                                         SkinID="MediumTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblRefubReplaceComments" runat="server" Text="COMMENTS"> </asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRefurbReplaceComments" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                                                         TextMode="MultiLine" ReadOnly="false" Rows="5"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                            </table>
                                          </div>
                                     </td>
                                 </tr>
                             </table>
                            </div>
                      </div>
          
                      <div id="tbClaimAuthorization">
                        <%-- <div class="Page">--%>
                                <div class="Page dataContainer" id="dvClaimAuthorizationDetails" runat="server">
                                    <%--<h2 class="dataGridHeader">
                                        <asp:Label runat="server" ID="lblGrdHdr" Text="CLAIM_AUTHORIZATIONS"></asp:Label>
                                    </h2>--%>
                                    <%--<div style="width: 100%">--%>
                                        <asp:GridView ID="GridClaimAuthorization" runat="server" Width="100%" AutoGenerateColumns="False"
                                            SkinID="DetailPageGridView" AllowSorting="true">
                                            <%--<SelectedRowStyle Wrap="True" />
                                            <EditRowStyle Wrap="True" />
                                            <AlternatingRowStyle Wrap="True" />
                                            <RowStyle Wrap="True" />
                                            <HeaderStyle />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="CLAIM_AUTHORIZATION_NUMBER" SortExpression="AuthorizationNumber">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="Select" ID="EditButton_WRITE" runat="server" CausesValidation="False"
                                                            Text=""></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ServiceCenterName" SortExpression="ServiceCenterName"
                                                    ReadOnly="true" HtmlEncode="false" HeaderText="SERVICE_CENTER_NAME" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="AuthorizedAmount" SortExpression="AuthorizedAmount" ReadOnly="true"
                                                    HtmlEncode="false" HeaderText="AUTHORIZED_AMOUNT" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" ReadOnly="true"
                                                    HtmlEncode="false" HeaderText="CREATED_DATE" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ClaimAuthStatus" ReadOnly="true" HeaderText="Status" SortExpression="ClaimAuthStatus"
                                                    HtmlEncode="false" />
                                            </Columns>
                                        </asp:GridView>
                                    <%--</div>--%>
                                </div>
                            <%--</div>--%>
                      </div>
                                       
                      <div id="tbClaimConsequentialDamage">
                        <Elita:UserControlConsequentialDamage ID="ucClaimConsequentialDamage" runat="server"></Elita:UserControlConsequentialDamage>
                      </div>
                    
                      <div id="tbClaimShippingInfo">
                                <div class="Page">
                                    <div id="dvClaimShipping" runat="server">
                                        <table class="formGrid" width="100%">
                                            <tr>
                                                <td align="right"  width="15%">
                                                </td>
                                                <td align="left" width="35%">
                                                    <asp:Label ID="lblInboundShippingInfo" runat="server" Text="INBOUND_SHIPPING_INFO" Font-Bold="True"> </asp:Label>
                                                </td>
                                                <td align="right" width="15%">
                                                </td>
                                                <td align="left" width="35%">
                                                    <asp:Label ID="lblOutboundShipingInfo" runat="server" Text="OUTBOUND_SHIPPING_INFO" Font-Bold="True"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblShipperToSC" runat="server" Text="SHIPPER_NAME"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShipperToSC" runat="server" CssClass="FLATTEXTBOX" Enabled="false" 
                                                                 SkinID="MediumTextBox"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblShipperToCust" runat="server" Text="SHIPPER_NAME"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShipperToCust" runat="server"  Enabled="false" CssClass="FLATTEXTBOX"
                                                                 SkinID="MediumTextBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblShipToSC" runat="server" Text="TRACKING_NUMBER"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShipToSC" runat="server" CssClass="FLATTEXTBOX" ReadOnly="false" 
                                                        SkinID="MediumTextBox"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblShipToCust" runat="server" Text="TRACKING_NUMBER"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShipToCust" runat="server"  ReadOnly="false"  CssClass="FLATTEXTBOX"
                                                        SkinID="MediumTextBox"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                     
                        
                      <div id="tbClaimFulfillmentDetails">
                          <div class="Page">
                                    <div id="dvClaimFulfillmentDetails" runat="server">
                                        <table class="formGrid" width="100%">
                                            <tr>
                                                <td align="right"  width="15%">
                                                </td>
                                                <td align="right" width="35%">                                                    
                                                    <asp:Label ID="lblFwdLogistics" runat="server" Text="FORWARD_LOGISTICS" Font-Bold="True"> </asp:Label>
                                                </td>
                                                <td align="left" width="15%">												
                                                </td>
                                                <td align="left" width="35%">                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblOptionDescription" runat="server" Text="DELIVERY_OPTION"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtOptionDescription" runat="server" ReadOnly="true"
                                                                     SkinID="MediumTextBox"></asp:TextBox>
                                                </td>
                                                <td align="right">
												    <asp:Label ID="lblTrackingNumber" runat="server" Text="TRACKING_NUMBER"> </asp:Label>
											    </td>
											    <td align="left">
												    <asp:TextBox ID="txtTrackingNumber" runat="server" ReadOnly="true" 
															     SkinID="MediumTextBox"></asp:TextBox>
											    </td>                                                 
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblShippingDate" runat="server" Text="ACTUAL_SHIPPING_DATE"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingDate" runat="server" ReadOnly="true" 
                                                                    SkinID="MediumTextBox"></asp:TextBox>
                                                </td> 
											    <td align="right">
												    <asp:Label ID="lblActualDeliveryDate" runat="server" Text="ACTUAL_DELIVERY_DATE"> </asp:Label>
											    </td>
											    <td align="left">
												    <asp:TextBox ID="txtActualDeliveryDate" runat="server" ReadOnly="true"
															     SkinID="MediumTextBox"></asp:TextBox>
											    </td>                                                
                                            </tr>										
										    <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblExpectedShippingDate" runat="server" Text="EXPECTED_SHIPPING_DATE"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtExpectedShippingDate" runat="server" ReadOnly="true" 
                                                                    SkinID="MediumTextBox"></asp:TextBox>
                                                </td> 
                                                <td align="right">
                                                    <asp:Label ID="lblExpectedDeliveryDate" runat="server" Text="EXPECTED_DELIVERY_DATE"> </asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" ReadOnly="true" 
                                                                 SkinID="MediumTextBox"></asp:TextBox>
                                                </td>											                                                   
                                            </tr>                                        
										<tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                            <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress1" runat="server" Text="ADDRESS"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAddress1" runat="server" ReadOnly="true" 
                                                                SkinID="LargeTextBox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblCity" runat="server" Text="CITY"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCity" runat="server" ReadOnly="true"
                                                             SkinID="MediumTextBox"></asp:TextBox>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress2" runat="server" Text="ADDRESS2"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAddress2" runat="server" ReadOnly="true" 
                                                                SkinID="LargeTextBox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblState" runat="server" Text="STATE"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtState" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox> 
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress3" runat="server" Text="ADDRESS3"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAddress3" runat="server" ReadOnly="true" 
                                                                SkinID="LargeTextBox" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            </td>                                            
                                            <td align="right">
                                                <asp:Label ID="lblCountry" runat="server" Text="COUNTRY"> </asp:Label>
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCountry" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>                                             											
                                            <td></td>
                                            <td></td>
                                            <td align="right">
                                                <asp:Label ID="lblPostalCode" runat="server" Text="POSTAL_CODE" > </asp:Label>
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPostalCode" runat="server" ReadOnly="true" 
                                                                SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>                                            
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblStoreCode" runat="server" Text="STORE_CODE"> </asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtStoreCode" runat="server" ReadOnly="true" 
                                                                SkinID="MediumTextBox"></asp:TextBox>
                                            </td> 
                                                <td align="right">
                                                    <asp:Label ID="lblStoreType" runat="server" Text="STORE_TYPE"> </asp:Label>
                                                </td>
                                                <td align="left" nowrap="nowrap">
													<asp:TextBox ID="txtStoreType" runat="server" ReadOnly="true" 
                                                                SkinID="MediumTextBox"></asp:TextBox>                                                    
                                                </td>                                                
                                        </tr>
                                        <tr>
                                            <td align="right">                                                
                                                <asp:Label ID="lblStoreName" runat="server" Text="STORE_NAME"> </asp:Label>                                                
                                            </td>
                                            <td align="left" >
                                                <asp:TextBox ID="txtStoreName" runat="server" ReadOnly="true" 
                                                                SkinID="MediumTextBox"></asp:TextBox>
                                            </td> 
                                            <td align="right">
                                               <input type="checkbox" id="chkPasscode" onclick="toglePassCode();" />
                                               <label for="chkPasscode">Show Passcode</label>
                                            </td>											
                                            <td align="left">
							                    <asp:TextBox ID="txtPasscode" runat="server" ClientIdMode="Static" 
                                                    ReadOnly="true" hidden="true" SkinID="MediumTextBox"></asp:TextBox>
											</td>                                            
                                        </tr>
                                        </table>
                                    </div>
                                </div>
                      </div>
                    </div>
                    
                </asp:Panel>                
                <asp:Panel ID="ViewPanel_READ2" runat="server" Height="40%" Width="100%">
                    <table class="formGrid" width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelDeniedReason" runat="server">Denied_Reason</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboDeniedReason" TabIndex="1" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td nowrap="nowrap" align="right" width="18%">
                                    <asp:Label ID="LabelDeniedReasons" runat="server">Denied_Reasons</asp:Label>
                                </td>
                                <td nowrap="nowrap" width="30%">
                                    <asp:TextBox ID="TextboxDeniedReasons" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                                 TextMode="MultiLine" ReadOnly="true" Rows="5" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right" class="style1">&nbsp;
                                    <asp:Label ID="lblPotFraudulent" runat="server">Potentially_Fraudulent</asp:Label>
                                </td>
                                <td nowrap="nowrap" class="style1">
                                    <asp:TextBox ID="TextboxFraudulent" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True">
                                    </asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">&nbsp;</td>
                                <td nowrap="nowrap"></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="lblComplaint" runat="server">Complaint</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxComplaint" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True">
                                    </asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">&nbsp;</td>
                                <td nowrap="nowrap"></td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelReasonClosed" runat="server">Reason_Closed</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboReasonClosed" TabIndex="2" runat="server" SkinID="MediumDropDown"
                                        Enabled="false" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelFollowupDate" runat="server">Followup_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxFollowupDate" TabIndex="2" runat="server" ReadOnly="true"
                                        SkinID="MediumTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonFollowupDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">&nbsp;
                                    <asp:Label ID="LabelClaimActivity" runat="server">Claim_Activity</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxClaimActivity" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelVisitDate" runat="server">Visit_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxVisitDate" TabIndex="2" runat="server" ReadOnly="true" SkinID="MediumTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonVisitDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelInvoiceProcessDate" runat="server">DATE_PROCESS_INVOICE</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxInvoiceProcessDate" TabIndex="2" runat="server" ReadOnly="true"
                                        SkinID="MediumTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonInvoiceProcessDate" TabIndex="2" runat="server" Visible="False"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelRepairDate" runat="server">Repair_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxRepairDate" TabIndex="2" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonRepairDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelInvoiceDate" runat="server">INVOICE_DATE</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxInvoiceDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelBatchNumber" runat="server">BATCH_NUMBER</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxBatchNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelClaimClosedDate" runat="server">Date_Claim_Closed</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxClaimClosedDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelPickUpDate" runat="server">PickUp_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxPickupDate" TabIndex="2" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonPickupDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelRepairCode" runat="server">Repair_Code</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxRepairCode" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelLoanerReturnedDate" runat="server">Loaner_Returned</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxLoanerReturnedDate" TabIndex="2" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonLoanerReturnedDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right" width="18%">
                                    <asp:Label ID="LabelAuthorizedAmount" runat="server">Authorized_Amount</asp:Label>
                                </td>
                                <td nowrap="nowrap" width="30%">
                                    <asp:TextBox ID="TextboxAuthorizedAmount" TabIndex="2" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right" width="15%">
                                    <asp:Label ID="LabelIsLawsuitId" runat="server">Lawsuit</asp:Label>
                                </td>
                                <td nowrap="nowrap" width="37%">
                                    <asp:DropDownList ID="cboLawsuitId" TabIndex="2" runat="server" SkinID="SmallDropDown" Enabled="false"></asp:DropDownList>
                                 </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDefectReason" runat="server">DEFECT_REASON</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDefectReason" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelExpectedRepairDate" runat="server">EXPECTED_REPAIR_DATE</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxExpectedRepairDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right" width="18%">
                                    <asp:Label ID="LabelProblemDescription" runat="server">Problem_Description</asp:Label>
                                </td>
                                <td nowrap="nowrap" width="30%">
                                    <asp:TextBox ID="TextboxProblemDescription" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                        TextMode="MultiLine" ReadOnly="true" Rows="5"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                     <asp:Label ID="LabelTrackingNumber" runat="server">Tracking_Number</asp:Label>:
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxTrackingNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelSpecialInstruction" runat="server">Special_Instructions</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxSpecialInstruction" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                        TextMode="MultiLine" ReadOnly="true" Rows="5"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right" width="15%">
                                    <asp:Label ID="LabelFulfilmentMethod" runat="server">FULFILMENT_METHOD</asp:Label>
                                </td>
                                <td nowrap="nowrap" width="37%">
                                    <asp:DropDownList ID="cboFulfilmentMethod" TabIndex="2" AutoPostBack="true" runat="server" SkinID="SmallDropDown" ></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelTechnicalReport" runat="server">TECHNICAL_REPORT</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxTechnicalReport" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                        TextMode="MultiLine" ReadOnly="true" Rows="5"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelAccountNumber" Visible="false" runat="server">ACCOUNT_NUMBER</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxAccountNumber" TabIndex="-1" Visible="false" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <uc1:UserControlContactInfo ID="moUserControlContactInfo" runat="server" Visible="false"></uc1:UserControlContactInfo>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <div class="btnZone">
            <div>
                <asp:Button ID="btnSave_WRITE" TabIndex="5" runat="server" SkinID="PrimaryRightButton"
                    Text="Save" />
                <asp:Button ID="btnEdit_WRITE" TabIndex="5" runat="server" SkinID="PrimaryRightButton"
                    Text="Edit" />
                <asp:LinkButton ID="btnUndo_WRITE" TabIndex="5" runat="server" SkinID="AlternateRightButton"
                    Text="Undo" />
                <asp:Button ID="btnBack" SkinID="AlternateLeftButton" TabIndex="5" runat="server"
                    Text="Back" />                
                <asp:Button ID="ActionButton" runat="server" TabIndex="5" SkinID="ActionButton" Text="LabelActionButtonMenu" />
                <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="ActionButton"
                    PopupControlID="PanButtonsHidden" PopupPosition="top" PopDelay="25" HoverCssClass="popupBtnHover">
                </ajaxToolkit:HoverMenuExtender>
                <asp:Panel ID="PanButtonsHidden" runat="server" SkinID="PopUpMenuPanel">
                    <asp:Button ID="btnDenyClaim" Visible="false" runat="server" SkinID="PopMenuButton"
                        Text="DENY_CLM" />
                    <asp:Button ID="btnAuthDetail" runat="server" Text="AUTH_DETAIL" SkinID="PopMenuButton" />
                    <asp:Button ID="btnChangeCoverage" runat="server" Text="CHANGE_COVERAGE" SkinID="PopMenuButton" />
                    <asp:Button ID="btnClaimHistoryDetails" runat="server" Text="Claim_History_Details"
                        SkinID="PopMenuButton" />
                    <asp:Button ID="btnOutboundCommHistory" runat="server" Text="Outbound_Comm_History"
                        SkinID="PopMenuButton" />
                    <asp:Button ID="btnCertificate" runat="server" Text="Certificate" SkinID="PopMenuButton" />
                    <asp:Button ID="btnComment" runat="server" Text="Comments" SkinID="PopMenuButton" />
                    <asp:Button ID="btnServiceCenterInfo" runat="server" Text="Center_Info" SkinID="PopMenuButton" />
                    <asp:Button ID="btnClaimDeniedInformation" runat="server" SkinID="PopMenuButton"
                        Text="Claim_Denied_Letter" />
                    <asp:Button ID="btnStatusDetail" runat="server" Text="EXTENDED_STATUS" SkinID="PopMenuButton" />
                    <asp:Button ID="btnItem" runat="server" Text="Item" SkinID="PopMenuButton" />
                    <asp:Button ID="btnMasterClaim" runat="server" Text="MASTERCLAIM" SkinID="PopMenuButton" />
                    <asp:Button ID="btnNewServiceCenter" runat="server" Text="New_Center" SkinID="PopMenuButton" />
                    <asp:Button ID="btnNewItemInfo" runat="server" Text="New Item Info" SkinID="PopMenuButton" />
                    <asp:Button ID="btnPoliceReport" runat="server" Text="Police_Report" SkinID="PopMenuButton" />
                    <asp:Button ID="btnPartsInfo" runat="server" Text="Parts   Info" SkinID="PopMenuButton" />
                    <asp:Button ID="btnPrint" runat="server" Text="RePrint" SkinID="PopMenuButton" />
                    <asp:Button ID="btnUseRecoveries" runat="server" Text="RECOVERY" SkinID="PopMenuButton" />
                    <asp:Button ID="btnRedo" runat="server" Text="Redo" SkinID="PopMenuButton" />
                    <asp:Button ID="btnReplaceItem" runat="server" Text="Replace_Item" SkinID="PopMenuButton" />
                    <asp:Button ID="btnReopen_WRITE" runat="server" Text="Re_Open_Claim" SkinID="PopMenuButton" />
                    <asp:Button ID="btnShipping" runat="server" Text="SHIPPING" SkinID="PopMenuButton" />
                    <asp:Button ID="btnServiceWarranty" runat="server" Text="Service_Warranty" SkinID="PopMenuButton" />
                    <asp:Button ID="btnRepairLogistics" runat="server" Text="Repair_And_Logistics" SkinID="PopMenuButton" />
                    <asp:Button ID="btnClaimIssues" runat="server" Text="CLAIM_ISSUES" SkinID="PopMenuButton" />
                    <asp:Button ID="btnClaimImages" runat="server" Text="CLAIM_IMAGES" SkinID="PopMenuButton" />
                    <asp:Button ID="btnClaimCaseList" runat="server" Text="CLAIM_CASE_DETAILS" SkinID="PopMenuButton" />
                    <asp:Button ID="btnAddConseqDamage" Visible="false" runat="server" Text="CONSEQ_DAMAGE" SkinID="PopMenuButton" />
                    <asp:Button ID="btnPriceRetailSearch" Visible="false"  runat="server" Text="RETAIL_PRICE_SEARCH" SkinID="PopMenuButton" />
                </asp:Panel>
            </div>
        </div>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
            runat="server" /><input id="HiddenLimitExceeded" type="hidden" name="HiddenLimitExceeded"
                runat="server" />
    </asp:Panel>

</asp:Content>

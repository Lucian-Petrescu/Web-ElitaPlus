<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommPlanExtractForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CommPlanExtractForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.js"></script>

    <script type="text/javascript">
     
        function enableToggle()
        {
            var commissionAmt = document.getElementById("ctl00_BodyPlaceHolder_txtCommiAmt").value;
            var commissionPercent = document.getElementById("ctl00_BodyPlaceHolder_txtCommiPerct").value;

            if(commissionAmt != "")
            {
                $("#ctl00_BodyPlaceHolder_txtCommiAmt").removeAttr("disabled");
                $("#ctl00_BodyPlaceHolder_txtCommiPerct").attr("disabled", "disabled");
            }
            else if (commissionPercent != "")
            {
                $("#ctl00_BodyPlaceHolder_txtCommiPerct").removeAttr("disabled");
                $("#ctl00_BodyPlaceHolder_txtCommiAmt").attr("disabled", "disabled");
            }
            else if(commissionAmt =="" && commissionPercent == "")
            {
                $("#ctl00_BodyPlaceHolder_txtCommiPerct").removeAttr("disabled");
                $("#ctl00_BodyPlaceHolder_txtCommiAmt").removeAttr("disabled");
            }
                
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommPlanCode" runat="server">COMMI_PLAN_CODE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCommPlanCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" colspan="1">
                                <asp:Label ID="lblCommPlanDescr" runat="server" Font-Bold="false">COMMI_PLAN_DESC</asp:Label>&nbsp;
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCommPlanDesc" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommiEffectDate" runat="server">COMMI_EFFECT_DATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCommiEffectDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="BtnCommiEffectDate" runat="server" mageAlign="AbsMiddle"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommiExpDate" runat="server">COMMI_EXP_DATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCommiExpDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="BtnCommiExpDate" runat="server" mageAlign="AbsMiddle"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </td>                                                   
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCyclFreqXcd" runat="server">CYCLE_FREQ</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                   <asp:TextBox ID="txtCyclFreXcd" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCyclRunDay" runat="server">CYC_RUN_DAY</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCyclRunDay" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCyclCutOffDay" runat="server">CYCLE_CUT_OFF_DAY</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                     <asp:TextBox ID="txtCyclCutOffDay" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCyclSrcXcd" runat="server">CYCLE_CUT_OFF_SRC</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCyclSrcXcd" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblAmtXcd" runat="server">AMOUNT_SOURCE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlAmtXcd" runat="server" SkinID="SmallDropDown" AutoPostBack="false" />
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommiExtType" runat="server">COMMI_EXT_TYPE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlCommiExtType" runat="server" SkinID="MediumDropDown" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommRateXcd" runat="server">COMM_AT_RATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlCommRateXcd" runat="server" SkinID="SmallDropDown" AutoPostBack="true" />
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblSeqNumber" runat="server">SEQUENCE_NUMBER</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtlblSeqNumber" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                 <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommiPerct" runat="server">COMMI_PERCENT</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCommiPerct"  runat="server" SkinID="MediumTextBox" onChange="enableToggle();"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommiAmt" runat="server">COMMI_AMT</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCommiAmt" runat="server" SkinID="MediumTextBox" onChange="enableToggle();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCommPlanExtId" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:Label ID="moIsNewCommPlanExt" runat="server" Visible="False"></asp:Label>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

    </div>

    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE"
            SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
            SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete" Enabled="false"
            SkinID="CenterButton"></asp:Button>
    </div>
</asp:Content>



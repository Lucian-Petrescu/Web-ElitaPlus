<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CancellationReasonForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CancellationReasonForm" EnableSessionState="True"  Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl"  Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                 <tr>
                    <td id="Td1" runat="server" colspan="2">
                        <table>
                            <tbody>
                                    <Elita:MultipleColumnDDLabelControl runat="server" ID="CompanyDropControl" />
                             </tbody>
                        </table>
                    </td>
                </tr> 
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr runat="server">
                                <td align="right">
                                    <asp:Label ID="lblCancCode" runat="server">CANCELLATION_CODE:</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextboxCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCancDesc" runat="server">CANCELLATION_REASON:</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextboxDescription" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="labelRefundComputeMethod" runat="server">Refund_Compute_Method:</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboRefundComputeMethod" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="labelRefundDestination" runat="server">Refund_Dest:</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboRefundDestination" runat="server" SkinID="MediumDropDown" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="labelInputAmtReq" runat="server">Input_Amt_Req:</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboInputAmtReq" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="labelDefRefPaymentMethod" runat="server">Def_Refund_Payment_Method:</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboDefRefPaymentMethod" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="labelDisplayCode" runat="server">Display_Code:</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboDisplayCode" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                        runat="server" />
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="labelIsLawful" runat="server">Is_Lawful:</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboIsLawful" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td align="right">
                                    <asp:Label ID="lblBenefitCancelCode" runat="server">BENEFIT_CANCEL_CODE:</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBenefitCancelCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right"></td>
                                <td align="left"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

       <div class="dataContainer">    
            <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsRoles">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Roles</asp:Label></a></li>
            </ul>

            <div id="tabsRoles">
                <div class="Page" runat="server" id="moRolesList" style="height: 300px; overflow: auto">
                    <Elita:UserControlAvailableSelected ID="UserControlAvailableExcludeRoles" runat="server"></Elita:UserControlAvailableSelected>
                </div>
            </div>
        </div>
       </div>
    <div class="btnZone">
        <asp:Button ID="btnSave_WRITE" runat="server" CausesValidation="False" Text="SAVE"
            SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
            SkinID="AlternateRightButton"></asp:Button>
         <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton">
        </asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
            SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
            SkinID="CenterButton"></asp:Button>
    </div>    

</asp:Content>

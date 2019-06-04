<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimDetailByExtendedStatusByDealerReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimDetailByExtendedStatusByDealerReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master" Theme="Default" %>
<%@ Register TagPrefix="uc2" TagName="multiplecolumnddlabelcontrol" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="dataContainer">
        <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
            <tr>
                <td colspan="2">
                    <table class="formGrid">
                        <tr>
                           <td align="right" colspan="2">*
                                <asp:Label ID="moDealerLabel" runat="server">SELECT_DEALER</asp:Label>
                                <span>:</span>
                            </td> 
                            <td align="left">
                                <asp:DropDownList ID="cboDealer" runat="server" AutoPostBack="false" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trDealer_space" runat="server" visible="true">
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="moClaimCreatedBeginDateLabel" runat="server">CLAIM_CREATED_BEGIN_DATE</asp:Label>:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moClaimCreatedBeginDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnClaimCreatedBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                            <td align="right">
                                <asp:Label ID="moClaimCreatedEndDateLabel" runat="server">CLAIM_CREATED_END_DATE</asp:Label>:    
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moClaimCreatedEndDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnClaimCreatedEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr id="trDates_space" runat="server" visible="true">
                            <td colspan="4"></td>
                        </tr>
                             <tr>
                            <td align="right">
                                <asp:Label ID="moClaimClosedBeginDateLabel" runat="server">CLAIM_CLOSED_BEGIN_DATE</asp:Label>:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moClaimClosedBeginDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnClaimClosedBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                            <td align="right">
                                <asp:Label ID="moClaimClosedEndDateLabel" runat="server">CLAIM_CLOSED_END_DATE</asp:Label>:   
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moClaimClosedEndDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnClaimClosedEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr id="tr2" runat="server" visible="true">
                            <td colspan="4"></td>
                        </tr>
                       <tr>
                            <td align="right">
                                <asp:Label ID="moExtStatusBeginDateLabel" runat="server">EXTENDED_STATUS_BEGIN_DATE</asp:Label>:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moExtStatusBeginDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnExtStatusBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                            <td align="right">
                                <asp:Label ID="moExtStatusEndDateLabel" runat="server">EXTENDED_STATUS_END_DATE</asp:Label>:    
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moExtStatusEndDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnExtStatusEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr id="tr1" runat="server" visible="true">
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Label ID="moExtendedLabel" runat="server">SELECT_CLAIM_EXT_STATUS</asp:Label>
                                  <span>:</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cboExtendedStatus" runat="server" AutoPostBack="false" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr id="trClaimExtStatus_space" runat="server" visible="true">
                            <td colspan="4">&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report Request" SkinID="AlternateLeftButton"></asp:Button>
    </div>

</asp:Content>

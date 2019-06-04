<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimDetailByExtendedStatusReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimDetailByExtendedStatusReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master" Theme="Default" %>

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
                            <td align="right">
                                <asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moBeginDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                            <td align="right">
                                <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>:    
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moEndDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr id="trDates_space" runat="server" visible="false">
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                           <td align="right" colspan="2">
                                <asp:Label ID="moDealerLabel" runat="server">SELECT_DEALER</asp:Label>
                                 <span>:</span>
                            </td> 
                            <td align="left">
                                <asp:RadioButtonList ID="moDealerList" AutoPostBack="true" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                    <asp:ListItem Text="SELECT_ALL_DEALERS" Value="*" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="OR A SINGLE DEALER" Value="SINGLE"></asp:ListItem>
                                </asp:RadioButtonList>
								 <asp:DropDownList ID="cboDealer" runat="server" AutoPostBack="false" SkinID="MediumDropDown" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trDealer_space" runat="server" visible="false">
                            <td colspan="4"></td>
                        </tr>

                        <tr>
                           <td align="right" colspan="2">
                                <asp:Label ID="moSvcCtrLabel" runat="server">SELECT_SERVICE_CENTER</asp:Label>
                               <span>:</span>
                            </td>
                            <td align="left">
                               <asp:RadioButtonList ID="moSvcCtrList" AutoPostBack="true" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                    <asp:ListItem Text="PLEASE SELECT ALL SERVICE CENTERS" Value="*" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="OR A SINGLE SERVICE CENTER" Value="SINGLE"></asp:ListItem>
                                </asp:RadioButtonList>
								 <asp:DropDownList ID="cboSvcCtr" runat="server" AutoPostBack="false" SkinID="MediumDropDown" Enabled="false">
                                </asp:DropDownList>
                            </td>                      

                        </tr>
                        <tr id="trSVC_space" runat="server" visible="false">
                            <td colspan="4"></td>
                        </tr>

                        <tr>
                            <td align="right" colspan="2">
                                <asp:Label ID="moExtendedLabel" runat="server">SELECT_CLAIM_EXT_STATUS</asp:Label>
                                  <span>:</span>
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="moClmExtStatusList" AutoPostBack="true" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                    <asp:ListItem Text="SELECT_ALL_EXT_STATUS" Value="*" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="SELECT_ALL_LST_EXT_STATUS" Value="LST"></asp:ListItem>
                                    <asp:ListItem Text="SELECT_SINGLE_EXTENDED_STATUS" Value="SINGLE"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:DropDownList ID="cboExtendedStatus" runat="server" AutoPostBack="false" SkinID="MediumDropDown" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr id="trClaimExtStatus_space" runat="server" visible="false">
                            <td colspan="4"></td>
                        </tr>


                        <tr>
                            <td align="right">
                                <asp:Label ID="moClaimAutoApprovelbl" runat="server">CLAIM_AUTO_APPROVE</asp:Label>
                            </td>
                            <td align="left" colspan="4">
                                <asp:DropDownList ID="moClaimAutoApproveDrop" runat="server" AutoPostBack="false" SkinID="SmallDropDown">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr id="trClmAutoApp_space" runat="server" visible="false">
                            <td colspan="4"></td>
                        </tr>

                        <tr>
                            <td align="right">
                                <asp:Label ID="lblClaimExtStatusSort" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                    <asp:ListItem Text="Claim_Number" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Date Claim Opened" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Last_Extended_Status_Date" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr id="trClaimExtStatusSort_space" runat="server" visible="false">
                            <td colspan="4"></td>
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

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MultipleDealerLoadRejectForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.MultipleDealerLoadRejectForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <tr>
                            <td id="Td2" runat="server" colspan="2">
                                <table>
                                    <tbody>
                                        <uc1:MultipleColumnDDLabelControl_New ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td3" runat="server" colspan="2">
                                <table>
                                    <tbody>
                                        <uc1:MultipleColumnDDLabelControl_New ID="moDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" style="padding-left: 1px;">
                                <table class="formGrid" border="0" cellpadding="2" cellspacing="3">
                                    <tr>
                                        <td align="left">*
                                            <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBeginDate" runat="server" SkinID="SmallTextBox" AutoPostBack="true"></asp:TextBox>
                                            <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                        </td>
                                        <td align="left">*
                                            <asp:Label ID="lblEndDate" runat="server">END_DATE:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEndDate" runat="server" SkinID="SmallTextBox" AutoPostBack="true"></asp:TextBox>
                                            <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">*
                                            <asp:Label ID="lblFileType" runat="server">FILE_TYPE:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFileType" runat="server" TextAlign="right" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                        <td align="right" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblAllRejStar" runat="server">*</asp:Label>
                                            <asp:Label ID="lblAllrej" runat="server">ORIGINAL_REJECTS:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rAllRej" Text="" runat="server" AutoPostBack="true" TextAlign="right"
                                                GroupName="RejectType"></asp:RadioButton>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblReconcileRejStar" runat="server">*</asp:Label>
                                            <asp:Label ID="lblReconcileRej" runat="server">OUTSTANDING_REJECTS:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rReconcileRej" Text="" runat="server" AutoPostBack="true" TextAlign="right"
                                                GroupName="RejectType"></asp:RadioButton>
                                            &nbsp;&nbsp;<asp:CheckBox ID="moInclBypassedRecCheck" runat="server" Checked="true" Visible="false" 
                                                BorderWidth="0" Text="INCLUDE_BYPASSED_RECORDS"
                                                TextAlign="Left" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left">*
                                            <asp:Label ID="lblRptlayout" runat="server">Report_Layout:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dpRptType" runat="server" AutoPostBack="false" TextAlign="right"></asp:DropDownList>
                                        </td>
                                        <td align="right" colspan="2"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="formGrid" width="100%">
                                    <tr>
                                        <td runat="server" colspan="2">
                                            <asp:Label ID="lblFiles" runat="server">Files:</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="Td1" runat="server" colspan="2">
                                            <table>
                                                <tbody>
                                                    <Elita:UserControlAvailableSelected ID="AvailableSelectedFiles" runat="server" />
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </table>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton"></asp:Button>
        </div>
    </div>   
</asp:Content>


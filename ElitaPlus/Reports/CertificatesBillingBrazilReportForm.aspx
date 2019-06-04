<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertificatesBillingBrazilReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.CertificatesBillingBrazilReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master" Theme="Default" %>

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
                            <td align="left">
                                <asp:Label ID="moDealerLabel" runat="server">SELECT_DEALER</asp:Label>
                                <span>:</span>
                            </td> 
                            <td align="left">
                                <%--<asp:RadioButtonList ID="moDealerList" AutoPostBack="true" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                    <asp:ListItem Text="SELECT_ALL_DEALERS" Value="*" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="OR A SINGLE DEALER" Value="SINGLE"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                                <asp:DropDownList ID="cboDealer" runat="server" AutoPostBack="false" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trDealer_space" runat="server" visible="false">
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="moRunDateLabel" runat="server">PROCESS_DATE</asp:Label>
                                <span>:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="moRunDateText" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                <asp:ImageButton ID="BtnRunDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr id="trDates_space" runat="server" visible="false">
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblRejectedRecords" runat="server">REJECTED</asp:Label> 
                                <span>:</span>
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkRejectedRecords" Runat="server" TextAlign="Left" />
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr id="trReject_space" runat="server" visible="false">
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

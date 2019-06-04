<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/ElitaReportBase.Master" CodeBehind="AMLPremiumReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.AMLPremiumReportForm" Theme="Default" %>

<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="" style="padding-left: 15px;"
                width="55%">
                <tr>
                    <td style="white-space: nowrap;">
                        <asp:Label ID="lblCertificatesBy" runat="server">CERTIFICATES_BY:</asp:Label></td>
                    <td align="left" style="white-space: nowrap;">
                        <asp:RadioButtonList ID="rblCertificatesBy" runat="server" RepeatDirection="vertical">
                            <asp:ListItem Text="DATE_ADDED" Value="CR" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="WARRANTY_SALE_DATE" Value="WSD"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>

                    <td align="left">*
                        <asp:Label ID="BeginDateLabel" runat="server">BEGIN_DATE:</asp:Label>

                        <asp:TextBox ID="BeginDateText" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                    </td>
                    <td align="left">*
                        <asp:Label ID="EndDateLabel" runat="server">END_DATE:</asp:Label>

                        <asp:TextBox ID="EndDateText" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
            </table>
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" width="35%">
                <tr id="trcomp" runat="server">
                    <td align="left">
                        <uc3:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc3:MultipleColumnDDLabelControl>
                        <br />
                    </td>
                </tr>
            </table>
            <table class="formGrid" border="0" cellspacing="1" cellpadding="0" style="padding-left: 15px;">
                <tr>

                    <td style="white-space: nowrap;">*
                        <asp:Label ID="lblActiveCerts" runat="server">NUMBER_OF_ACTIVE_CERTS:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtActiveCerts" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER:</asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="VERTICAL">
                            <asp:ListItem Value="C" Selected="True">CUSTOMER_NAME</asp:ListItem>
                            <asp:ListItem Value="T">TAX_ID</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton"></asp:Button>
    </div>
</asp:Content>

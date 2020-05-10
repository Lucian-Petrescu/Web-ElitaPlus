<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlPoliceReport_New.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlPoliceReport_New" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<div class="stepformZone">
    <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <uc1:MultipleColumnDDLabelControl ID="mPoliceMultipleColumnDropControl" runat="server">
            </uc1:MultipleColumnDDLabelControl>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="LabelReportNumber" runat="server">REPORT_NUMBER</asp:Label>:
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="TextboxReportNumber" TabIndex="1" runat="server" CssClass="small"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="LabelOfficerName" runat="server">OFFICER_NAME</asp:Label>:
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="TextboxOfficerName" TabIndex="1" runat="server" CssClass="small"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="right">
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
</div>

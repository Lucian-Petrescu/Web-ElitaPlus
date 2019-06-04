<%@ Page Language="vb" MasterPageFile="~/Navigation/masters/content_default.Master"  EnableEventValidation="False"
    AutoEventWireup="false" Codebehind="AccountingInterface.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingInterface" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" ID="cntMain">
    <table cellpadding="0" cellspacing="0" border="0">
         <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                *&nbsp;<asp:Label ID="moAccountingCompanyLABEL" runat="server">COMPANY:</asp:Label>
            </td>
            <td>
                <uc1:MultipleColumnDDLabelControl id="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="lblEvent" runat="server" Text="ACCOUNTING_EVENT:"></asp:Label>
            </td>
            <td>
                 &nbsp;&nbsp;<asp:RadioButton ID="rdoEventAll" runat="server" TextAlign="left" Text="All"  Checked=true GroupName="rdosGroup" />&nbsp;
                  <asp:RadioButton ID="rdoEventSpecific" runat="server" TextAlign="left" Text="EVENT_TYPE"
                    GroupName="rdosGroup" />&nbsp;&nbsp;<asp:DropDownList ID="ddlAccountingEvent" runat="server" AutoPostBack="false" Width="298" Enabled=False>
                </asp:DropDownList>  
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="lblVendorFiles" runat="server" Text="VENDOR_FILES:"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;<asp:DropDownList ID="ddlVendorFiles" runat="server" AutoPostBack="false" Width="250">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
         <tr>
            <td class="BLANKROW" colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="lblRunPending" runat="server" Text="INCLUDE_PENDING:"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;<asp:DropDownList ID="ddlIncludePending" runat="server" AutoPostBack="false" Width="250">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntButtons" runat="server" ContentPlaceHolderID="ContentPanelButtons">
    <asp:Button ID="btnBack" TabIndex="185" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK"
        Text="Back"></asp:Button>
    <asp:Button ID="btnExecute" TabIndex="186" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE"
        Text="PROCESS_RECORDS" ></asp:Button>
</asp:Content>

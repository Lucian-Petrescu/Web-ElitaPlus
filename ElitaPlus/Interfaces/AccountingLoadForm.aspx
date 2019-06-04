<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    Codebehind="AccountingLoadForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingLoadForm"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN" style="vertical-align:bottom">
                *&nbsp;<asp:Label ID="moAccountingCompanyLABEL" runat="server">COMPANY:</asp:Label>
            </td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
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
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="BORDERED">
                <asp:Label Text="FileName:" ID="lblFileName" runat="server"></asp:Label>&nbsp;&nbsp;<asp:FileUpload
                    ID="filinput" runat="server" Width="250" />
                &nbsp;&nbsp;
                <asp:Button ID="btnValidate" TabIndex="185" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
                    Text="VALIDATE"></asp:Button>
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
            <td colspan="2">
                <table cellpadding="0" border="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="lblVendorFiles" runat="server" Text="VENDOR_FILES:"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlVendorFiles" runat="server" AutoPostBack="false"
                                width="200"  Enabled="False" >
                            </asp:DropDownList>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label1" runat="server" Text="WORKSHEET:"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlWorksheetVendor" runat="server" AutoPostBack="false"
                                Enabled="False" width="200">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="lblAccountingEvent" runat="server" Text="ACCOUNTING_EVENT:"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlAccountingEvents" runat="server" AutoPostBack="false"
                                width="200"  Enabled="False" >
                            </asp:DropDownList>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="lblWorksheet" runat="server" Text="WORKSHEET:"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlWorksheetEvents" runat="server" AutoPostBack="false"
                                Enabled="False" width="200">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptEvents" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="LABELCOLUMN">
                                    <asp:Label ID="lblEventDetailID_NOTRANSLATE" runat="server" Text="" Visible=false></asp:Label>
                                    <asp:Label ID="lblEventDetail" runat="server" Text="ACCOUNTING_EVENTS:"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;&nbsp;<asp:DropDownList ID="ddlEventDetailYesNo" runat="server" AutoPostBack="false"
                                        Enabled="False" width="200">
                                    </asp:DropDownList>
                                </td>
                                <td class="LABELCOLUMN">
                                    <asp:Label ID="lblWorksheet" runat="server" Text="WORKSHEET"></asp:Label>:
                                </td>
                                <td>
                                    &nbsp;&nbsp;<asp:DropDownList ID="ddlWorksheet" runat="server" AutoPostBack="false"
                                        Enabled="False" width="200">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" TabIndex="185" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK"
        Text="Back"></asp:Button>
    <asp:Button ID="btnExecute" TabIndex="186" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE"
        Text="PROCESS_RECORDS"  Enabled="false" ></asp:Button>
</asp:Content>

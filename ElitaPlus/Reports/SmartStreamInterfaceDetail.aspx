<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master"
    CodeBehind="SmartStreamInterfaceDetail.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.SmartStreamInterfaceDetail" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">
        <tr>
            <td class="BLANKROW" colspan="4">
            </td>
        </tr>
        <tr id="trcomp" runat="server">
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td style="vertical-align: bottom; text-align:right;white-space:nowrap; ">
                *&nbsp;<asp:Label ID="lblCompany" runat="server">SELECT_COMPANY:</asp:Label>
            </td>
            <td colspan="3" style="text-align:left">
                <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="5">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="5">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td style="text-align:right; white-space:nowrap; vertical-align:baseline">
                *<asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
            </td>
            <td style="text-align:left;white-space:nowrap;padding-left:12px;">
                <asp:TextBox ID="moBeginDateText" runat="server" AutoPostBack="true" CssClass="FLATTEXTBOX"></asp:TextBox>
                <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
            </td>
            <td style="text-align:right; white-space:nowrap; vertical-align:baseline; padding-left:25px;">
                *<asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>:
            </td>
            <td style="text-align:left; white-space:nowrap; width:50%; padding-left:8px;">
                <asp:TextBox ID="moEndDateText" runat="server" AutoPostBack="true" CssClass="FLATTEXTBOX"></asp:TextBox>
                <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
            </td>            
        </tr>
        <tr>
            <td colspan="5" style="height: 25px">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" style="width: 25%">
            </td>
            <td style="text-align:right; white-space:nowrap; vertical-align:baseline">
                *<asp:Label ID="lblFileName" runat="server">FILE_NAME</asp:Label>:
            </td>
            <td colspan="3" style="text-align:left;white-space:nowrap;padding-left:12px;">
                <asp:DropDownList ID="cboFileName" runat="server" AutoPostBack="false" Width="500px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" CssClass="FLATBUTTON BUTTONSTYLE_BACK"
        Height="20px" CausesValidation="False" Visible="false"></asp:Button>
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
    <asp:Button ID="btnDownLoadXML_WRITE" Style="cursor: hand; background-repeat: no-repeat" runat="server" Text="DOWNLOAD_XML" Width="120px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>

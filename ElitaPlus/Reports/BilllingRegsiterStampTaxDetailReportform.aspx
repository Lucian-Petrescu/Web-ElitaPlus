
<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Reports/content_Report.Master" 
     Codebehind="BilllingRegsiterStampTaxDetailReportform.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BilllingRegsiterStampTaxDetailReportform" %>     
<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" >
    <table cellpadding="0" cellspacing="0" border="0"  style="vertical-align: top;">
    <tr>
            <td class="BLANKROW" style="vertical-align: top;">
            </td>
            <td>
            </td>
             <td>
            </td>
        </tr>
        <tr id = "trcomp" runat="server">
          <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;">
                *&nbsp;<asp:Label ID="lblCompany"  runat="server">SELECT_COMPANY:</asp:Label>
            </td>
            <td>
                <uc3:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN">
                *&nbsp;<asp:Label ID="lblMonthYear" runat="server" Text="SELECT MONTH AND YEAR:"></asp:Label>
            </td>
            <td>
                 &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="moMonthDropDownList" runat="server" Width="25%" AutoPostBack="false">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="moYearDropDownList" runat="server" Width="15%" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>        
        </table>
    
 </asp:Content>      
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">    
        <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>                  

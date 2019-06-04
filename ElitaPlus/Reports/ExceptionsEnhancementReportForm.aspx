<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master" CodeBehind="ExceptionsEnhancementReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ExceptionsEnhancementReportForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" >

 <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">                        
     <tr>
         <td align="center" width="100%"  style="height: 13px">
         </td>
     </tr>
     <tr>
         <td align="left">
             &nbsp;
             <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                 cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
                 CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
         </td>
     </tr>
 </table>
    

 </asp:Content>      
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat; align: left" TabIndex="185" runat="server"
        CssClass="FLATBUTTON" Text="Back" Height="20px" Width="96px"></asp:Button>
</asp:Content>   

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
        CodeBehind="ReportCeScheduleForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeScheduleForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    
      <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="TD_LABEL" >
                *<asp:Label ID="moStartDateLabel" runat="server">START_DATE</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moStartDateText" runat="server" Width="210px" CssClass="FLATTEXTBOX"></asp:TextBox>
                <asp:ImageButton ID="BtnStartDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                </asp:ImageButton>
            </td>
          
        </tr>
        <tr>
            <td  colspan="4" style="height: 12px; width: 1%;">   
            </td>  
        </tr>
        
      
    </table>
</asp:Content>
<asp:Content ID="cntButtons" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="BACK">
    </asp:Button>
    <asp:Button ID="btnApply_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="SAVE"
        CssClass="FLATBUTTON" Height="20px"></asp:Button>
</asp:Content>

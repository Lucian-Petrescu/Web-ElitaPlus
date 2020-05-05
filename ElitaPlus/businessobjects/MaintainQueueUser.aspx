<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaintainQueueUser.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.business.MaintainQueueUser" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="ur2" TagName="UserControlQuestionsAvailable" Src="../common/UserControlAvailableSelected_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"> 
    <div class="dataGridHeader">
        <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" nowrap="nowrap"><asp:Label ID="lblClaimNumber" runat="server" SkinID="SummaryLabel">NETWORK_ID</asp:Label>:</td>
                <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblNewrokId" runat="server" SkinID="SummaryLabel"></asp:Label></td>
                <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblClaimStatus" runat="server" SkinID="SummaryLabel">LANGUAGE</asp:Label>:</td>
                <td id="ClaimStatusTD" runat="server" align="left" style="white-space:nowrap" class="padRight"><asp:Label ID="lblUserLanguage" runat="server" SkinID="SummaryLabel"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap" ><asp:Label ID="lblWorkPhoneNumber" runat="server" SkinID="SummaryLabel">USER_NAME</asp:Label>:</td>
                <td align="left" nowrap="nowrap" class="bor padRight" ><asp:Label ID="lblUsername" runat="server" SkinID="SummaryLabel"></asp:Label></td>
                <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblSubscriberStatus" runat="server" SkinID="SummaryLabel">STATUS</asp:Label>:</td>
                <td id="SubStatusTD" align="left" style="white-space:nowrap" class="padRight"  runat="server"><asp:Label ID="lblUserStatus" runat="server" SkinID="SummaryLabel"></asp:Label></td>       
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
        <div class="dataContainer">
            <div class="stepformZone">
            <table border="0" cellpadding="0" cellspacing="0" width="95%" class="formGrid">
                    <tr><td width="13%" align="right">
                    <asp:Label ID="Label1" runat="server">SELECT_COMPANY</asp:Label>&nbsp&nbsp</td>
                    <td>
                    <asp:DropDownList ID="ddlCompanyList" runat="server" Width="27%"  SkinID="MediumDropDown"
                        AutoPostBack="True"></asp:DropDownList>
                    </td></tr>
                    <tr><td colspan="2">
                    <ur2:UserControlQuestionsAvailable ID="UC_AvaSel_Rule" runat="server" style="background: #d5d6e4;"
                                tabindex="12">
                    </ur2:UserControlQuestionsAvailable>
                    </td></tr>
            </table>
            </div>
             <div class="btnZone">
                <asp:Button ID="btnSave_WRITE"  runat="server" Width="100px" Text="Save"
                    SkinID="PrimaryRightButton" Height="25px" ToolTip="Save"></asp:Button>&nbsp&nbsp&nbsp
                <asp:LinkButton ID="btnCancel" runat="server" Height="18px" Width="75px" Text="Cancel" SkinID="AlternateRightButton"
                     ToolTip="Cancel" >
                </asp:LinkButton>
             </div>
         </div>
    </asp:Content>

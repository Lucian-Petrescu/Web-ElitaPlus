<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SearchAvailableDealer.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SearchAvailableDealer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableDealer" Src="../Common/UserControlAvailableSelected.ascx" %>
<div align="center" ms_positioning="GridLayout" style="background-color: #d5d6e4;
    width: 100%; height: 98%;">
    <table id="moOutTable" style="background-color: #d5d6e4; width: 98%; height: 98%;"
        cellspacing="1" cellpadding="1" align="center" runat="server">
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Visible="false"
                    CssClass="FLATBUTTON" Width="85px" Text="Search" Height="20px" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr style="padding-top: 10px">
            <td>
                <uc1:UserControlAvailableDealer ID="UserControlAvailableSelectedDealerCodes" runat="server">
                </uc1:UserControlAvailableDealer>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp&nbsp&nbsp
                <asp:Button ID="btnSave" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="SAVE"
                    CssClass="FLATBUTTON" Height="20px"></asp:Button>
                &nbsp&nbsp&nbsp
                <asp:Button ID="btnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="CANCEL"></asp:Button>
            </td>
        </tr>
    </table>
</div>

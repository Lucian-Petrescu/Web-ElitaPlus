<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAvailableSelected_New.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<style type="text/css">
    .noborder {border:none;}
</style>
<div align="center" style="position: relative">
    <table id="moOutTable" runat="server" class="availableSelectBox" border="0">
        <tr>
            <td style="text-align: center">
                <strong>
                    <asp:Label ID="moAvailableTitle" runat="server" Width="100%">Available</asp:Label></strong>
            </td>
            <td align="center" style="width: 30px">
            </td>
            <td style="text-align: center">
                <strong>
                    <asp:Label ID="moSelectedTitle" runat="server" Width="100%">Selected</asp:Label></strong>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" width="45%">
                <asp:ListBox ID="moAvailableList" runat="server" Height="135px" Width="100%" SelectionMode="Multiple"
                    Rows="6"></asp:ListBox>
            </td>
            <td valign="middle" width="10%">
                <table id="moButtonsTable" cellspacing="2" cellpadding="2" width="100%" border="0"
                    bordercolor="#ffffff" runat="server">
                    <tr>
                        <td align="center" class="CenteredTD noborder">
                            <input type="Button" id="AddBtn" class="moveRight" style="cursor: hand;" onclick="AddAction(this);"  />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="CenteredTD noborder">
                            <input type="Button" id="RemoveBtn" class="moveLeft" style="cursor: hand;" onclick="RemoveAction(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="CenteredTD noborder">
                            <input type="Button" id="AddAllbtn" class="moveRightAll" style="cursor: hand;"  onclick="AddAllAction(this);" />
                        </td>
                     </tr>
                    <tr>
                        <td align="center" class="CenteredTD noborder">
                            <input type="Button" id="RemoveAllbtn" class="moveLeftAll" style="cursor: hand;" onclick="RemoveAllAction(this);" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="top" width="45%">
                <asp:ListBox ID="moSelectedList" runat="server" Height="135px" Width="100%" SelectionMode="Multiple"
                    Rows="6"></asp:ListBox>
            </td>
            <td valign="middle" width="10%">
                <asp:Button ID="btnMoveUp" Style="background-image: url(../Navigation/images/icons/up.gif);
                    cursor: hand; background-repeat: no-repeat;" runat="server" Width="25px" CssClass="FLATBUTTON"
                    ToolTip="MOVE_UP"></asp:Button>
                <br />
                <br />
                <asp:Button ID="btnMoveDown" Style="background-image: url(../Navigation/images/icons/down.gif);
                    cursor: hand; background-repeat: no-repeat;" runat="server" Width="25px" CssClass="FLATBUTTON"
                    ToolTip="MOVE_DOWN"></asp:Button>
            </td>
        </tr>
    </table>
</div>
<input id="moAvailableTexts" type="hidden" name="moAvailableTexts" runat="server">
<input id="moAvailableValues" type="hidden" name="moAvailableValues" runat="server">
<input id="moSelectedTexts" type="hidden" name="moSelectedTexts" runat="server">
<input id="moSelectedValues" type="hidden" name="moSelectedValues" runat="server">

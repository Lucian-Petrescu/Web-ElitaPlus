<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAvailableSelected.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div align="center" ms_positioning="GridLayout" style="position: relative">
    <style type="text/css">
            .valignTop {vertical-align: top;}
            .valignMiddle {vertical-align: middle;}
            .alignCernter {text-align:center;}
            .whiteborder {border:1px solid white;}
        </style>
    <table id="moOutTable" bordercolor="#ffffff" cellspacing="1" cellpadding="1" align="center"
        border="0" bgcolor="#fef9ea" runat="server" style="width: 98%; height: 160px">        
        <!--#99eb69-->
        <tr>
            <td style="text-align: center">
                <asp:Label ID="moAvailableTitle" ForeColor="#12135b" runat="server" Width="100%">Available</asp:Label>
            </td>
            <td align="center" style="width: 30px">
            </td>
            <td style="text-align: center">
                <asp:Label ID="moSelectedTitle" ForeColor="#12135b" runat="server" Width="100%">Selected</asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" width="45%">
                <asp:ListBox ID="moAvailableList" runat="server" Height="135px" Width="100%" SelectionMode="Multiple"
                    Rows="6" style="background-color:white;"></asp:ListBox>
            </td>
            <td valign="middle" width="10%">
                <table id="moButtonsTable" cellspacing="2" cellpadding="2" width="100%" class="whiteborder" runat="server">
                    <tr>
                        <td align="center" class="CenteredTD whiteborder">
                            <input type="Button" id="AddBtn" style="cursor: hand; width: 35px;" class="FLATBUTTON"
                                onclick="AddAction(this);" value=">" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="CenteredTD whiteborder">
                            <input type="Button" id="RemoveBtn" style="cursor: hand; width: 35px;" class="FLATBUTTON"
                                onclick="RemoveAction(this);" value="<" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="CenteredTD whiteborder">
                            <input type="Button" id="RemoveAllBtn" style="cursor: hand; width: 35px;" class="FLATBUTTON"
                                onclick="RemoveAllAction(this);" value="<<" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="CenteredTD whiteborder">
                            <input type="Button" id="AddAllBtn" style="cursor: hand; width: 35px;" class="FLATBUTTON"
                                onclick="AddAllAction(this);" value=">>" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="top" width="45%">
                <asp:ListBox ID="moSelectedList" runat="server" Height="135px" Width="100%" SelectionMode="Multiple"
                    Rows="6" style="background-color:white;" ></asp:ListBox>
            </td>
            <td valign="middle" width="10%">
                <asp:Button ID="btnMoveUp" Style="background-image: url(../Navigation/images/icons/up.gif);
                    cursor: hand; background-repeat: no-repeat;" runat="server" Width="25px" CssClass="FLATBUTTON"
                    ToolTip="MOVE_UP"></asp:Button>
                <br>
                <br>
                <asp:Button ID="btnMoveDown" Style="background-image: url(../Navigation/images/icons/down.gif);
                    cursor: hand; background-repeat: no-repeat;" runat="server" Width="25px" CssClass="FLATBUTTON"
                    ToolTip="MOVE_DOWN"></asp:Button>
            </td>
        </tr>
    </table>
</div>
<input id="moAvailableTexts" type="hidden" name="moAvailableTexts" runat="server"/>
<input id="moAvailableValues" type="hidden" name="moAvailableValues" runat="server"/>
<input id="moSelectedTexts" type="hidden" name="moSelectedTexts" runat="server"/>
<input id="moSelectedValues" type="hidden" name="moSelectedValues" runat="server"/>

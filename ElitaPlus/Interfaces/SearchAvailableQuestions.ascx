<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SearchAvailableQuestions.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SearchAvailableQuestions" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableQuestions" Src="../Common/UserControlAvailableSelected.ascx" %>
<div align="center" ms_positioning="GridLayout" style="background-color: #d5d6e4;
    width: 100%; height: 98%;">
    <table id="moOutTable" style="background-color: #d5d6e4; width: 98%; height: 98%;"
        cellspacing="1" cellpadding="1" align="center" runat="server">
        <tr>
            <td>
                <table id="tblDynamic" style="width: 98%; height: 100%;">
                    <tr>
                        <td style="width: 5%;" align="right">
                            <asp:Label ID="LabelIssue" runat="server" Width="100%">ISSUE:</asp:Label>
                        </td>
                        <td style="width: 15%;">
                            <asp:DropDownList ID="cboIssue" runat="server" AutoPostBack="False" Width="85%">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%;" align="right">
                            <asp:Label ID="LabelQuestionList" Width="100%" runat="server">QUESTION_LIST:</asp:Label>
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="moQuestionList" runat="server" AutoPostBack="False" Width="85%"></asp:TextBox>
                        </td>
                        <td style="width: 10%;" align="right">
                            <asp:Label ID="LabelSearchTags" Width="100%" runat="server">SEARCH_TAGS:</asp:Label>
                        </td>
                        <td style="width: 20%;">
                            <asp:TextBox ID="moSearchTags" runat="server" AutoPostBack="False" Width="85%"></asp:TextBox>
                        </td>
                        <td style="width: 5%;">
                        </td>
                        <td style="width: 7%;">
                            <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                CssClass="FLATBUTTON" Width="85px" Text="Search" Height="20px" />
                        </td>
                        <td style="width: 8%;">
                            <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                CssClass="FLATBUTTON" Width="85px" Text="Clear" Height="20px"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="padding-top: 10px">
            <td>
                <uc1:UserControlAvailableQuestions ID="UserControlAvailableSelectedQuestionsCodes"
                    runat="server">
                </uc1:UserControlAvailableQuestions>
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

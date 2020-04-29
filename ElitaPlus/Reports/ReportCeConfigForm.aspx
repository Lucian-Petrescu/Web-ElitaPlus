<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="ReportCeConfigForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeConfigForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td valign="top" align="left" colspan="4">
                <uc1:MultipleColumnDDLabelControl ID="moCompanyMult" runat="server" />
            </td>
        </tr>
        <tr>
            <td  colspan="4" style="height: 30px; width: 1%;">   
            </td>  
        </tr>
        <%--<tr>
                                                    <td colspan="4">
                                                        <hr style="height: 1px">
                                                    </td>
                                                </tr>--%>
      </table>
      <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="TD_LABEL" >
                *<asp:Label ID="moReportLabel" runat="server">REPORT</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:DropDownList ID="moReportDrop" runat="server">
                </asp:DropDownList>
            </td>
            <td class="TD_LABEL">
                *<asp:Label ID="moReportCeLabel" runat="server">REPORT_CE_NAME</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:DropDownList ID="moReportCeDrop" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  colspan="4" style="height: 12px; width: 1%;">   
            </td>  
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td class="TD_LABEL" style="height: 12px; width: 1%;">
                *<asp:Label ID="moLargeLabel" runat="server">LARGE_REPORT</asp:Label>:
            </td>
            <td style="white-space: nowrap; height: 12px" align="left">
                &nbsp;<asp:DropDownList ID="moLargeDrop" runat="server">
                </asp:DropDownList>
                <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                       runat="server"/>
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
    <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="UNDO"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="New"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
        Text="New_With_Copy" Width="136px"></asp:Button>
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="Delete"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>

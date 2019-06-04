<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="AccountingCompanyDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.AccountingCompanyDetailForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table id="moOutTable" height="98%" cellspacing="0" cellpadding="6" width="98%" align="center"
        bgcolor="#fef9ea" border="0">
        <tr>
            <td style="vertical-align:top;">
                <table id="Table1" style="width: 100%; height: 168px" cellspacing="1" cellpadding="0"
                    width="100%" border="0">
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="DescriptionLabel" runat="server" Visible="True" Text="ACCOUNTING_COMPANY">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="DescriptionTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" Width="200px"
                                Visible="True"></asp:TextBox>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="CodeLabel" runat="server" Visible="True" Text="CODE">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="CodeTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" Width="200px"
                                Visible="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="AccountingSystemLabel" runat="server" Visible="True" Text="ACCOUNTING_SYSTEM">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="AccountingSystemDropDown" runat="server" Width="207px" Visible="True">
                            </asp:DropDownList>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="UseAccountingLabel" runat="server" Visible="True" Text="USE_ACCOUNTING">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="UseAccountingDropdown" runat="server" Width="207px" Visible="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="ProcessMethodLabel" runat="server" Visible="True" Text="PROCESSING_METHOD">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="ProcessMethodDropDown" runat="server" Width="207px" Visible="True">
                            </asp:DropDownList>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="UseElitaBankInfoLabel" runat="server" Visible="True" Text="USE_ELITA_BANK_INFO">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="UseElitaBankInfoDropdown" runat="server" Width="207px" Visible="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="RptCommissionLabel" runat="server" Visible="True" Text="REPORT_COMMISSION_BREAKDOWN">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="RptCommissionDropDown" runat="server" Width="207px" Visible="True">
                            </asp:DropDownList>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="ftpDirectoryLabel" runat="server" Visible="True" Text="FTP_HOST_PATH">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="ftpDirectoryTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" Width="200px"
                                Visible="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="balanceDirectoryLabel" runat="server" Visible="True" Text="BALANCE_DIRECTORY">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="balanceDirectoryTextBox" CssClass="FLATTEXTBOX_TAB" Width="200px"
                                runat="server" Visible="True"></asp:TextBox>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="notifyEmailLabel" runat="server" Visible="True" Text="EMAIL">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="notifyEmailTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" Width="200px"
                                Visible="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="UseCoverageEntityLabel" runat="server" Visible="True" Text="USE_COV_ENTITY">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="UseCoverageEntityDropdown" runat="server" Width="207px" Visible="True">
                            </asp:DropDownList>
                        </td>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="CoverageEntityByRegionLabel" runat="server" Visible="True" Text="COV_ENTITY_BY_REGION">
                            </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                            <asp:DropDownList ID="CoverageEntityByRegionDropdown" Width="207px" runat="server"
                                Visible="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" size="14" name="HiddenSaveChangesPromptResponse"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Width="90px"
        Text="Back" Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Width="90px"
        Text="New" Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
    <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Width="90px"
        Text="Save" Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
    <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Width="90px"
        Text="Undo" Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="135px" Text="New_With_Copy"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False" TabIndex="105">
    </asp:Button>
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Width="116px"
        Text="Delete" Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
</asp:Content>

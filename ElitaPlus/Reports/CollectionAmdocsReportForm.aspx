<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CollectionAmdocsReportForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.CollectionAmdocsReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master" Theme="Default" %>


<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">

                <tr>
                    <td colspan="2">
                        <table class="formGrid">


                            <tr id="Tr1" runat="server">
                                <td align="right">*
                                        <asp:Label ID="lblReportBasedOnList" runat="server">SELECT_REPORT_BASED_ON</asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:RadioButtonList ID="moReportBasedOnList" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                        <%--<asp:ListItem Text="BASED_ON_BILLING_DATE" Value="DATEBASEON_BILLING" Selected="True"></asp:ListItem>--%>
                                        <asp:ListItem Text="BASED_ON_COLLECTION_DATE" Value="DATEBASEON_COLLECTION" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="BASED_ON_PROCESSED_DATE" Value="DATEBASEON_PROCESSED"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>

                            <tr id="Tr2" runat="server">
                                <td align="right">*
                                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE</asp:Label>:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                    <asp:ImageButton ID="btnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                                <td align="right">*
                                        <asp:Label ID="lblEndDate" runat="server">END_DATE</asp:Label>:    
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                    <asp:ImageButton ID="btnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4"></td>
                            </tr>

                            <tr id="Tr3" runat="server">
                                <td align="right">*
                                        <asp:Label ID="Label1" runat="server">SELECT_THE_DEALER</asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:RadioButtonList ID="moDealerOptionList" AutoPostBack="true" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                        <asp:ListItem Text="SELECT_ALL_DEALERS" Value="DEALER_ALL" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="SELECT_SINGLE_DEALER" Value="DEALER_SINGLE"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Panel ID="pnlDealerDropControl" runat="server" Visible="false">
                                        <Elita:MultipleColumnDDLabelControl runat="server" ID="DealerDropControl" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="Generate Report Request" SkinID="AlternateLeftButton"></asp:Button>
    </div>


</asp:Content>

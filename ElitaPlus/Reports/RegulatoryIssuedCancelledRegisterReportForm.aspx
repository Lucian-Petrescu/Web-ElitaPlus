<%@ Register TagPrefix="ELita" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegulatoryIssuedCancelledRegisterReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.RegulatoryIssuedCancelledRegisterReportForm" MasterPageFile="~/Reports/ElitaReportBase.Master"    
    Theme="Default" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">     	        
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                <tr id="trcomp" runat="server">
                    <td colspan="2">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl_New ID="moUserCompanyMultipleDrop" runat="server">
                                </Elita:MultipleColumnDDLabelControl_New>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
            <div class="dataContainer" id="divView" runat="server" visible="false">
                <table class="formGrid">
                    <tr id="Tr2" runat="server">
                        <td align="right">
                            *
                            <asp:Label ID="lblMonth" runat="server">MONTH:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList  ID="moMonthDropDownList" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            *
                            <asp:Label ID="lblYear" runat="server">YEAR:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="moYearDropDownList" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dataContainer" id="divPDF" runat="server" visible="false">
                <table class="formGrid">
                    <tr>
                        <td align="right">
                            *
                            <asp:Label ID="lblLPeriod" runat="server" Text="Last_Generated_Period:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLPeriod" runat="server" SkinID="MediumTextBox" AutoPostBack="false">
                            </asp:TextBox>
                            <input type="hidden" id="hidden_LPeriod" runat="server" name="hidden_LPeriod" />
                        </td>
                        <td align="right">
                            *
                            <asp:Label ID="lblLPage" runat="server" Text="Last_Page_Printed:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLPage" runat="server" SkinID="MediumTextBox" AutoPostBack="false">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            *
                            <asp:Label ID="lblPGenerate" runat="server" Text="Period_To_Generate:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPGenerate" runat="server" SkinID="MediumTextBox" AutoPostBack="false">
                            </asp:TextBox>
                            <input type="hidden" id="hidden_PGenerate" runat="server" name="hidden_PGenerate" />
                        </td>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton">
        </asp:Button>
            <input id="HiddenRptPromptResponse" type="hidden" name="HiddenRptPromptResponse"
                runat="server" />        
    </div>
</asp:Content>

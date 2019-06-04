<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Reports/content_Report.Master" 
     Codebehind="RegulatoryBillingRegister.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.RegulatoryBillingRegister" %>     
<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" >
    <table cellpadding="0" cellspacing="0" border="0"  style="vertical-align: top;">
        <tr>
            <td class="BLANKROW" style="vertical-align: top;">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr id = "trcomp" runat="server">
          <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;">
                &nbsp;<asp:Label ID="lblCompany"  runat="server">SELECT_COMPANY:</asp:Label>
            </td>
            <td>
                <uc3:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <asp:Panel ID="pnlView" runat="server" Visible="false">
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN">
                *&nbsp;<asp:Label ID="lblMonthYear" runat="server" Text="SELECT MONTH AND YEAR:"></asp:Label>
            </td>
            <td>
                 &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="moMonthDropDownList" runat="server" Width="25%" AutoPostBack="false">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="moYearDropDownList" runat="server" Width="15%" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>  
         <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr> 
        </asp:Panel>
        <asp:Panel ID="pnlPDF" runat="server" Visible="false">
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td colspan="2" align="left" style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="LABELCOLUMN" style="width: 5%">
                            *&nbsp;<asp:Label ID="lblLPeriod" runat="server" Text="Last_Generated_Period:"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtLPeriod" runat="server" Width="50%" AutoPostBack="false">
                            </asp:TextBox>
                            <input type="hidden" id="hidden_LPeriod" runat="server" name="hidden_LPeriod" />
                        </td>
                        <td class="LABELCOLUMN" style="width: 5%">
                            *&nbsp;<asp:Label ID="lblLPage" runat="server" Text="Last_Page_Printed:"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtLPage" runat="server" Width="50%" AutoPostBack="false">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
         <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td colspan="2" align="left" style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="LABELCOLUMN" style="width: 7.8%">
                            *&nbsp;<asp:Label ID="lblPGenerate" runat="server" Text="Period_To_Generate:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPGenerate" runat="server" Width="48.5%" 
                                AutoPostBack="false"></asp:TextBox>
                                <input type="hidden" id="hidden_PGenerate" runat="server" name="hidden_PGenerate" />
                        </td>
                        <td colspan="2" style="width: 25%">                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </asp:Panel>
        <input id="HiddenRptPromptResponse" type="hidden" name="HiddenRptPromptResponse" runat="server" />
    </table>
    
 </asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">    
        <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>                  

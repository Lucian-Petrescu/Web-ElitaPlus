
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IssuedCancelledRegisterwithPremium.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.IssuedCancelledRegisterwithPremium" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
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
                    <tr>
                        <td id="Td2" runat="server" colspan="2">
                            <table>
                                <tbody>
                                    <uc1:MultipleColumnDDLabelControl_New ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                                </tbody>
                            </table>
                        </td>
                    </tr>                  
                    <tr>
                        <td colspan= "2">
                            <table class="formGrid">                                
                                <tr id="Tr2" runat="server">
                                    <td align="right">*
                                        <asp:Label ID="lblMonth" runat="server">MONTH:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moMonthList" runat="server" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">*
                                        <asp:Label ID="lblYear" runat="server">YEAR:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moYearList" runat="server" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server">
                                    <td style="vertical-align: bottom;">*
                                        <asp:RadioButton ID="rdealer" AutoPostBack="false" Checked="True" runat="server" TextAlign="left" Text="SELECT_ALL_DEALERS" ></asp:RadioButton>
                                    </td>
                                    <td id="Td1" runat="server" colspan="3">
                                        <table>
                                            <tbody>
                                                <uc1:MultipleColumnDDLabelControl_New ID="moDealerMultipleDrop" runat="server" />
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                 <tr id="Tr1" runat="server">
                                    <td align="right">*
                                        <asp:Label ID="lblReportOptionList" runat="server">SELECT_REPORT_OPTION:</asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                       <asp:RadioButtonList ID="moReportOptionList" runat="server" RepeatDirection="Horizontal">
                                          <asp:ListItem Text="BY_CERTIFICATE" Value="C" Selected="True"></asp:ListItem>
                                          <asp:ListItem Text="BY_AGENT_CODE" Value="A"></asp:ListItem>
                                       </asp:RadioButtonList>
                                    </td>
                                 </tr>                                                                                                                 
                            </table>
                        </td>
                    </tr>            
                </table>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="Generate Report Request" SkinID="AlternateLeftButton">
            </asp:Button>
        </div>
    </asp:Content>

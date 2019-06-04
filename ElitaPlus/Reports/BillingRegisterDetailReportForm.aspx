
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BillingRegisterDetailReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingRegisterDetailReportForm" MasterPageFile="~/Reports/ElitaReportBase.Master"    
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
                            <tr id="Tr3" runat="server">
                                    <td align="right">*
                                        <asp:Label ID="lblRptBasedOn" runat="server">REPORT_BASED_ON:</asp:Label>
                                    </td>
                                    <td align="left">
                                    <asp:RadioButtonList id="rdReportBasedOn" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="PROCDATE" Selected="True">PROCESSING_DATE</asp:ListItem>
										<asp:ListItem Value="ACTDATE">ACTUAL_DATE</asp:ListItem>
										</asp:RadioButtonList>
                                    </td>                                                                                                    
                                    <td align="left">
                                    </td>
                                    <td align="left" >  
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server">
                                    <td align="right">*
                                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                    </td>                                                                                                    
                                    <td align="right" >*
                                        <asp:Label ID="lblEndDate" runat="server">END_DATE:</asp:Label>    
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                    </td>
                                </tr>                                                                                    
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
                                <tr runat="server">
                                    <td style="vertical-align: bottom;">*
                                        <asp:RadioButton ID="rdealer" AutoPostBack="false" Checked="True" runat="server" TextAlign="left" Text="SELECT_ALL_DEALERS" valign ="botton" >
                                        </asp:RadioButton>
                                    </td>
                                    <td runat="server" colspan="3">
                                        <table>
                                            <tbody>
                                               <uc1:MultipleColumnDDLabelControl_New ID="moDealerMultipleDrop" runat="server" />
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>                                
                            </table>
                        </td>
                    </tr>            
                </table>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton">
            </asp:Button>
        </div>
    </asp:Content>

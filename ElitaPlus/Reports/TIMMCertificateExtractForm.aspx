
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TIMMCertificateExtractForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.TIMMCertificateExtractForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
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
                        <td colspan= "2">
                            <table class="formGrid">    
                                 <tr id="Tr3" runat="server">
                                    <td align="right"> *
                                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                        <asp:ImageButton ID="btnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>                                                                                                    
                                    <td align="right" >*
                                        <asp:Label ID="lblEndDate" runat="server">END_DATE</asp:Label>:    
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="175px" ></asp:TextBox>
                                        <asp:ImageButton ID="btnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr><td  colspan="4" ></td></tr> 
                                 <tr id="Tr1" runat="server">
                                    <td align="right">*
                                        <asp:Label ID="lblExtractTypeList" runat="server">SELECT_EXTRACT_TYPE:</asp:Label>
                                    </td>
                                    <td align="left" colspan="3" >
                                       <asp:RadioButtonList ID="moExtractTypeList" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                          <asp:ListItem Text="ALL_ACTIVE_CERTIFICATES_BEING_BILLED" Value="ACTBLCERTS" Selected="True"></asp:ListItem>
                                          <asp:ListItem Text="CANCELLED_CERTIFICATES_GREATER_THAN_30_DAYS_AND_LESS_THAN_12_MONTHS_BEING_BILLED" Value="CANCERTSGRT30DLTEQ12MN"></asp:ListItem>
                                          <asp:ListItem Text="ALL_CANCELLED_CERTIFICATES_WITHIN_30_DAYS" Value="CANCERTSREFUND" ></asp:ListItem>
                                          <asp:ListItem Text="CANCELLED_CERTIFICATES_GREATER_THAN_12_MONTHS" Value="CANCERTSGRT12MN"></asp:ListItem>
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

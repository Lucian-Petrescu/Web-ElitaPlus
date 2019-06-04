<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GapInCertNumberSeqReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.GapInCertNumberSeqReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
    Theme="Default" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
         <style>
            .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
        </style>     	        
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
        <div class="dataContainer">
            <div class="stepformZone">
                <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">                  
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
                            <table class="formGrid"  border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;">
                                <tr id="Tr1" runat="server">
                                    <td style="vertical-align: bottom;">*
                                        <asp:Label ID="Label1" runat="server">SELECT_ALL_DEALERS</asp:Label>:
                                        <asp:RadioButton ID="rdealer" Checked="True" runat="server" Text="">
                                        </asp:RadioButton>
                                    </td>
                                    <td id="Td1" runat="server" colspan="3">
                                        <table>
                                            <tbody>
                                               <uc1:MultipleColumnDDLabelControl_New ID="moDealerMultipleDrop" runat="server" />
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>                                
                                <tr><td>&nbsp;</td></tr>                                                                                    
                                <tr id="Tr2" runat="server">
                                    <td align="left">
                                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="175px" onchange="DateChanged();"></asp:TextBox>
                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>                                                                                                    
                                    <td align="right" >
                                        <asp:Label ID="lblEndDate" runat="server">END_DATE</asp:Label>:    
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="175px" onchange="DateChanged();"></asp:TextBox>
                                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">&nbsp;
                                        <asp:Label ID="lblOrAnd" runat="server">OR_AND</asp:Label>
                                    </td>
                                </tr> 
                                <tr id="Tr3" runat="server">
                                    <td align="left">
                                        <asp:Label ID="lblCertNumberFrom" runat="server">CERT_NUMBER_FROM</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCertNumberFrom" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                    </td>                                                                                                    
                                    <td align="right" >
                                        <asp:Label ID="lblCertNumberTo" runat="server">CERT_NUMBER_TO</asp:Label>:    
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtCertNumberTo" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>                                                                                    
                                <tr id="Tr4" runat="server">
                                    <td align="left">
                                     <asp:Label ID="lblCertPrefixLength" runat="server">CERT_PREFIX_LENGTH</asp:Label>:    
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCertPrefixLength" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                    </td>   
                                    <td align="right">
                                     <asp:Label ID="lblShowTotalsOnly" runat="server">SHOW TOTALS ONLY</asp:Label>:    
                                    </td>
                                    <td align="left">
                                    <asp:CheckBox ID="chkShowTotalsOnly" Runat="server" TextAlign="Left" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>            
                </table>
            </div>
          </div>
        <div class="btnZone">
                <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Text="Generate Report Request" Width="200px" />
        </div>
        <script type="text/javascript">

                $(document).ready(function () {
                    $("form > *").change(function () {
                        enableReport();
                    });
                });

                function DateChanged() {
                    enableReport();
                }


                function enableReport() {
                    var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
                    btnGenReport.disabled = false;
                }
    </script>
    </asp:Content>

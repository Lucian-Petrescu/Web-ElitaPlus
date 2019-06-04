
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UpgradeReconciliationReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.UpgradeReconciliationReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
    Theme="Default" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../common/UserControlAvailableSelected_New.ascx" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server"> 
            <style type="text/css">
            .auto-style1 {
                height: 10px;
                width: 48%;
            }
            .auto-style2 {
                width: 48%;
            }
            .auto-style3 {
                width: 48%;
                height: 289px;
            }
        .auto-style4 {
            width: 129px;
        }
        </style>
       <style>
        .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
    </style>      	        
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
                                        <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="150px"></asp:TextBox>
                                        <asp:ImageButton ID="btnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>                                                                                                    
                                    <td align="right" >*
                                        <asp:Label ID="lblEndDate" runat="server">END_DATE</asp:Label>:    
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="150px" ></asp:TextBox>
                                        <asp:ImageButton ID="btnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr><td  colspan="4" ></td></tr> 
                             </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="Td3" runat="server" colspan="2">
                            <table>
                                <tbody>
                                    <uc1:MultipleColumnDDLabelControl_New ID="moDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                                </tbody>
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
        <script type="text/javascript" language="javascript">

        
        $(document).ready(function () {
            $("form > *").change(function () {
                enableReport();
            });
        });


        function enableReport() {
            var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
            btnGenReport.disabled = false;
        }

        </script>
    </asp:Content>

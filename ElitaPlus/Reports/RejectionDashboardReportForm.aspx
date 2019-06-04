
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RejectionDashboardReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.RejectionDashboardReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
    Theme="Default" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server"> 
         <style>
                .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
        </style>    	        
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
        <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js" type="text/javascript"></script>
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
                             <tr id="Tr1" runat="server">
                                    <td style="vertical-align: bottom;">*
                                        <asp:Label ID="Label1" runat="server">SELECT_ALL_DEALERS</asp:Label>:
                                        <asp:RadioButton ID="rdealer" AutoPostBack="false" Checked="True" runat="server" Text="">
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
                                    <td align="left">*
                                       <asp:Label ID="lblAllMonths" runat="server">SELECT_ALL_MONTHS</asp:Label>:
                                        <asp:RadioButton ID="rbMonths" AutoPostBack="false" Checked="True" runat="server" Text="" onclick="ToggleSingleAllMonths('0');">
                                        </asp:RadioButton>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblMonth" runat="server">OR_A_SINGLE_MONTH:</asp:Label>
                                        <asp:DropDownList ID="moMonthList" runat="server" SkinID="MediumDropDown" onclick="ToggleSingleAllMonths('1');">
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
                                <tr><td>&nbsp;</td></tr> 
                                <tr id="Tr3" runat="server">
                                        <td align="left">*
                                            <asp:Label ID="lblFileType" runat="server">FILE_TYPE:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFileType" runat="server" TextAlign="right" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                        <td align="right" colspan="2"></td>
                                </tr>
                                <tr><td>&nbsp;</td></tr> 
                                <tr id="Tr4" runat="server">
                                    <td align="left">
                                        <asp:Label ID="lblShowCancelRecords" runat="server">SHOW_CANCEL_RECORDS</asp:Label>    
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkShowCancelRecords" Runat="server" TextAlign="Left" />
                                    </td>
                                    <td align="left">
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


                function enableReport() {
                    var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
                    btnGenReport.disabled = false;
                }

		    function ToggleSingleAllMonths(MonthVal) {
		        var objAllMonths = document.getElementById('<%=rbMonths.ClientID%>');
		        var objMonthList = document.getElementById('<%=moMonthList.ClientID%>');

		        if (MonthVal == '0') 
                {
		           objMonthList.selectedIndex = -1;

		        }
		       else 
                {
                    if (MonthVal == '1') 
                    {
		                objAllMonths.checked = false;
		            }
		        }
		    }
		</script>
    </asp:Content>

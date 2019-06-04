<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExtendedStatusAgingReportForm.aspx.vb"
     Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ExtendedStatusAgingReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
    Theme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server"> 
    <style>
        .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
    </style>    	        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
            <div class="stepformZone">
                <table class="formGrid" align="center" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                    width="100%">                  
                                   
                    <tr>
                        <td colspan= "4">
                            <table class="formGrid">
                             <tr id="Tr1" runat="server">
                                    <td colspan="2" style="vertical-align: bottom;" align="right">*
                                        <asp:Label ID="Label2" runat="server">SELECT_ALL_DEALERS</asp:Label>:
                                        <asp:RadioButton ID="rDealer"  AutoPostBack="false" Checked="True" runat="server" Text="" onclick="toggleAllDealersSelectionEx('0');">
                                        </asp:RadioButton>
                                    </td>

                                 <td align="center" nowrap width="25%">&nbsp;&nbsp;
                                        <asp:Label ID="moDealerLabel" runat="server">OR A SINGLE DEALER</asp:Label>
                                        &nbsp;&nbsp;

                                 </td>
                                <td align="left" nowrap width="40%">
                                    <asp:DropDownList ID="cboDealer" runat="server" onchange="toggleAllDealersSelectionEx('1');" width="100%">
                                    </asp:DropDownList>
                                </td>
                                   <%-- <td id="Td1" runat="server" colspan="3">
                                        <table>
                                            <tbody>
                                               <uc1:MultipleColumnDDLabelControl_New ID="moDealerMultipleDrop" runat="server" />
                                            </tbody>
                                        </table>--%>
<%--                                    </td>--%>
                             </tr>     
                             <tr><td>&nbsp;</td></tr> 
                             <tr id="Tr2" runat="server">
                                    <td  colspan="2" style="vertical-align: bottom;" align="right">*
                                       <asp:Label ID="lblAllStages" runat="server">SELECT_ALL_STAGES</asp:Label>:
                                        <asp:RadioButton ID="rbStages" AutoPostBack="false" Checked="True" runat="server" Text="" onclick="ToggleSingleAllStages('0');">
                                        </asp:RadioButton>
                                    </td>
                                    <td align="center"  nowrap width="25%">&nbsp;&nbsp;
                                        <asp:Label ID="lblStageName" runat="server">OR_A_SINGLE_STAGE_NAME</asp:Label> &nbsp;&nbsp;</td>
                                      <td align="left" nowrap width="40%">  
                                        <asp:DropDownList ID="moStageList" runat="server"  width="100%" onclick="ToggleSingleAllStages('1');">
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td align="right">*
                                        <asp:Label ID="lblYear" runat="server">YEAR:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moYearList" runat="server" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                    </td>--%>
                                </tr>
                                <tr><td>&nbsp;</td></tr> 
                                <tr id="Tr3" runat="server">
                                  <td  colspan="2" style="vertical-align: bottom;" align="right">*
                                       <asp:Label ID="lblAllStageStatus" runat="server">SELECT_ALL_STAGE_STATUS</asp:Label>:
                                        <asp:RadioButton ID="rbStageStatus" AutoPostBack="false" Checked="True" runat="server" Text="" onclick="ToggleSingleAllStageStatus('0');">
                                        </asp:RadioButton>
                                    </td>
                                     <td align="center"  nowrap width="25%">&nbsp;&nbsp;
                                        <asp:Label ID="lblStageStatus" runat="server">OR_A_SINGLE_STAGE_STATUS</asp:Label>&nbsp;&nbsp;</td>
                                  <td align="left" nowrap width="40%"> <asp:DropDownList ID="moStageStatusList" runat="server"  width="100%" onclick="ToggleSingleAllStageStatus('1');">
                                        </asp:DropDownList>
                                    </td>
                              
                                </tr>
                                <tr><td>&nbsp;</td></tr> 
                                <tr id="Tr4" runat="server">
                                      
                                   <td colspan="2" style="vertical-align: bottom;" align="right">
                                       
                                        <asp:Label ID="lblNoofDayssincestageopened" runat="server">NUMBER_OF_DAYS_SINCE_STAGE_OPENED</asp:Label>:  
                                      
                                    </td>
                                    <td colspan="2" align="left">&nbsp;&nbsp;
                                   
                                  
                                        <asp:textbox ID="tbdayssincestageopened"  runat="server" CssClass="FLATTEXTBOX" width="200px" TextAlign="Left"></asp:textbox>&nbsp;&nbsp;
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
                                                     
            <%--<asp:Button ID="Button1" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton">
            </asp:Button>--%>
        </div>

     <script type="text/javascript">

         $(document).ready(function () {
             $("form > *").change(function () {
                 enableReport();
             });
         });


         function enableReport() {
             //debugger
             var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
                btnGenReport.disabled = false;
            }

         function ToggleSingleAllStages(StageVal) {
             var objAllStages = document.getElementById('<%=rbStages.ClientID%>');
             var objStageList = document.getElementById('<%=moStageList.ClientID%>');

             if (StageVal == '0') {
                 objStageList.selectedIndex = -1;

             }
             else {
                 if (StageVal == '1') {
                     objAllStages.checked = false;
                 }
             }
         }


         function ToggleSingleAllStageStatus(StageStatusVal) {
             var objAllStageStatus = document.getElementById('<%=rbStageStatus.ClientID%>');
             var objStageStatusList = document.getElementById('<%=moStageStatusList.ClientID%>');

             if (StageStatusVal == '0') {
                 objStageStatusList.selectedIndex = -1;

             }
             else {
                 if (StageStatusVal == '1') {
                     objAllStageStatus.checked = false;
                 }
             }
         }

         function toggleAllDealersSelectionEx(DealerVal) {
             var objAllDealers = document.getElementById('<%=rDealer.ClientID%>');
             var objDealerList = document.getElementById('<%=cboDealer.ClientID%>');

             if (DealerVal == '0') {
                 objDealerList.selectedIndex = -1;

             }
             else {
                 if (DealerVal == '1') {
                     objAllDealers.checked = false;
                 }
             }
         }




		</script>
</asp:Content>









































































































































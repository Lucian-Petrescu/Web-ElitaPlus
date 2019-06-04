<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewCertificatesChinaReportForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.NewCertificatesChinaReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"    
    Theme="Default" %><%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %><%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %><%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
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
    <script type="text/javascript" language="javascript" src="../navigation/scripts/ReportCEMainScripts.js"></script>
   
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                <!--d5d6e4-->
                <tr>
                      <td align='center' class="auto-style1">
                        <table class="formGrid" border='0' width="80%">
                           <tr style="height: 40px">
                                <td align="left"  width="25%" nowrap="nowrap" >*
									
                                    <asp:label id="mobegindatelabel" runat="server">BEGIN_DATE</asp:label>:
                                
                                	
                                    <asp:textbox id="moBeginDateText" tabindex="1" runat="server" SkinID="SmallTextBox"></asp:textbox>
                                    
                                    <asp:imagebutton id="btnBeginDate" runat="server" width="20px" ImageUrl="~/App_Themes/Default/Images/calendar.png" style="vertical-align:middle"></asp:imagebutton>
                                </td>
                                <td align="left"  width="25%" nowrap="nowrap" >
									
                                    <asp:label id="moenddatelabel" runat="server">END_DATE</asp:label>:
                                
					
                                    <asp:textbox id="moEndDateText" tabindex="1" runat="server" SkinID="SmallTextBox"></asp:textbox>
                                    <asp:imagebutton id="btnEndDate" runat="server" width="20px" ImageUrl="~/App_Themes/Default/Images/calendar.png" style="vertical-align:middle"></asp:imagebutton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <hr style="width: 99.43%; height: 1px" />
                    </td>
                </tr>
                <tr>
                    <td align='center' class="auto-style3">
                        <table  class="formGrid" border='0' width="80%">
                            <tr style="height: 40px">
                                <td align="left" nowrap="nowrap" >
                                     <asp:label id="esclabel" runat="server">SELECT_DEALER_TYPE_ESC_MOBILE</asp:label>
                                    <asp:RadioButton ID="rbDealerTypeESC" GroupName="rbgDealerType" runat="server" Checked="True" AutoPostBack="true" />
                                    <asp:label id="hwlabel" runat="server">SELECT_DEALER_TYPE_HW</asp:label>
                                    <asp:RadioButton ID="rbDealerTypeHW" GroupName="rbgDealerType" runat="server" AutoPostBack="true" Checked="False" />
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td align="left" nowrap="nowrap" >
                                    * 
									
                                    <asp:label id="lblReportType" runat="server">SELECT_ACCIDENTAL_PROTECTION_REPORT_TYPE</asp:label>:
                                    
                                    <asp:dropdownlist id="ddlReportType" SkinID="MediumDropDown" runat="server" autopostback="false"></asp:dropdownlist>
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td align="left" nowrap="nowrap" >*
                                      <asp:label id="Label4" runat="server">SELECT_ALL_DEALERS</asp:label>
                                    <asp:RadioButton id="rdealer" Checked="true" GroupName="a"  AutoPostBack="true" Runat="server" TextAlign="left" />
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td align="left" nowrap="nowrap">
                                    *
                                    <asp:label id="Label1" runat="server">OR_SELECT_MULTIPLE_DEALERS</asp:label>
                                    <asp:RadioButton ID="rdealer2" runat="server" GroupName="a" AutoPostBack="true" TextAlign="left" OnCheckedChanged="rdealer2_CheckedChanged"/>
                                    <div>
                                        <Elita:UserControlAvailableSelected ID="AvailableSelectedDealers" runat="server" />
                                    </div>
                                    <div  onclick="toggleDealerType('hidden');"></div>
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td align="left" nowrap="nowrap" >
                                    *
                                    <asp:label id="mocoveragelabel" runat="server">SELECT_ALL_COVERAGES</asp:label>
                                    <asp:radiobutton id="rbCoverage"  Checked="False" Runat="server" TextAlign="left" AutoPostBack="false" onclick="toggleAllCovSelection('0');"/>
                                    <asp:label id="lblCoverage" runat="server">OR_A_SINGLE_COVERAGE_TYPE</asp:label>:
									
                                    <asp:dropdownlist id="ddlCoverage" 	width="212px" runat="server" autopostback="false" onclick="toggleAllCovSelection('1');"></asp:dropdownlist>
                                </td>
                            </tr>

                             <tr style="height: 40px">
                                <td align="left" nowrap="nowrap" >
                                     <asp:label id="Label2" runat="server">SHOW TOTALS ONLY</asp:label>
                                    <asp:RadioButton ID="RadiobuttonTotalsOnly" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(false);"
                                                                        TextAlign="left" GroupName="b"  Checked="true"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:label id="Label3" runat="server">OR SHOW DETAIL WITH TOTALS</asp:label>
                                    
                                  <asp:RadioButton ID="RadiobuttonDetail" runat="server" AutoPostBack="false" GroupName="b" onclick="toggleDetailSelection(true);"
                                                                         TextAlign="left" />
                                      </td>                         
                                                             
                             </tr>
                                                           
                        
                        </table>
                    </td>
                </tr>
            </table>
        </div>
         <div class="btnZone">
         <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Width="200px"  Text="Generate Report Request" />
             </div>
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

         function toggleAllCovSelection(isSingleCov) {
             var objAllCoverages = document.getElementById('<%=rbCoverage.ClientID%>');
             var objCoverage = document.getElementById('<%=ddlCoverage.ClientID%>');
         if (isSingleCov == '0') {
             objCoverage.selectedIndex = -1;
         }
         else {
             if (isSingleCov == '1')
             objAllCoverages.checked = false;
         }
         }

        </script>
</asp:Content>


			

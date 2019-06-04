<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LossSummaryAndPaymentsReportForm.aspx.vb"
    MasterPageFile="~/Reports/ElitaReportExtractBase.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.LossSummaryAndPaymentsReportForm" 
    Theme="Default" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .btnZone input.altBtn[disabled] {
            visibility: visible !important;
            display: block !important;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0%;" width="100%">
                        <tr>
                            <td align="left" colspan="1" style="width:20%">
                            </td>
                            <td colspan="2">
                                <table id="Table1" class="formGrid">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server">SELECT_REPORTING_PERIOD</asp:Label>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdoAccountingPeriod" runat="server" Text="ACCOUNTING_PERIOD_MTD" Checked="True" AutoPostBack="false" GroupName="PeriodGroup" OnClick="return toggleRadioButton();"></asp:RadioButton>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdoCutOff" runat="server" Text="CUTOFF_DATE" AutoPostBack="false" GroupName="PeriodGroup" OnClick="return toggleRadioButton();"></asp:RadioButton>
                                        </td>
                                    </tr>
                                </table>
                             </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr id="trSelectMonthYear" runat="server">
                            <td align="left" colspan="1">
                            </td>
                            <td colspan="2">
                                  <table id="Table2" class="formGrid">
                                        <tr>
                                            <td>
                                                <asp:Label ID="MonthYearLabel" runat="server">SELECT_MONTH_AND_YEAR</asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="MonthDropDownList" runat="server" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="YearDropDownList" runat="server" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                         <tr id="trCutOff" runat="server" style="display:none">
                            <td align="left" colspan="1">
                            </td>
                             <td colspan="2">
                                  <table id="Table3" class="formGrid">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCutOff" runat="server">SELECT_CUTOFF_DATE</asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCutOffDate" runat="server" SkinID="SmallTextBox" AutoPostBack="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;<asp:ImageButton ID="imgCutOffDate" runat="server" Visible="True" TabIndex="7" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="Top" AutoPostBack="false" />
                                            </td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1">
                            </td>
                            <td colspan="2">
                                <table id="Table4" class="formGrid">
                                            <uc1:MultipleColumnDDLabelControl_New ID="moUserCompanyMultipleDrop" runat="server">
                                            </uc1:MultipleColumnDDLabelControl_New>
                                </table>
                            </td>
                        </tr>
                       <tr>
                            <td align="left" colspan="1">
                            </td>
                            <td colspan="2">
                                <table id="Table5" class="formGrid">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSelectAllDealers" runat="server">SELECT_ALL_DEALERS</asp:Label>
                                            <asp:RadioButton ID="rdealer" AutoPostBack="false" type="radio" GroupName="Dealer" Text="" runat="server" Checked="True" ></asp:RadioButton>
                                        </td>
                                    </tr>
                                </table>
                             </td>
                        </tr>

                        <tr>
                            <td align="left" colspan="1">
                            </td>
                            <td colspan="2">
                                  <table id="Table6" class="formGrid">
                                     <uc1:MultipleColumnDDLabelControl_New ID="multipleDealerDropControl" runat="server" >
                                          </uc1:MultipleColumnDDLabelControl_New>
                                  </table>
                             </td>
                        </tr>
                         <tr>
                            <td align="left" colspan="3">
                                <hr />
                            </td>
                        </tr>
                       <tr>
                        <td align="left" colspan="1">
                        </td>
                        <td colspan="2">
                            <table id="tblReportType" class="formGrid">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" >REPORT_TYPE</asp:Label>:&nbsp;
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rdoSummarized" runat="server" Text="REPORT_TYPE_SUMMARIZED" GroupName="ReportTypeGroup"
                                            Checked="True" AutoPostBack="false"></asp:RadioButton>
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rdoDetailed" runat="server" Text="REPORT_TYPE_DETAILED" GroupName="ReportTypeGroup" AutoPostBack="false">
                                        </asp:RadioButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
    </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Text="GENERATE_REPORT_REQUEST" Width="200px"></asp:Button>
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

        function toggleRadioButton() {
            var rdoAccounting = document.getElementById("<%=rdoAccountingPeriod.ClientID%>");
            var rdoCutOff = document.getElementById("<%=rdoCutOff.ClientID%>");
            if (rdoAccounting.checked)
            {
                var trCutOff = document.getElementById("<%=trCutOff.ClientID%>");
                trCutOff.style.display = 'none';
                var trSelectMonthYear = document.getElementById("<%=trSelectMonthYear.ClientID%>");
                trSelectMonthYear.style.display = 'block';
            }
            else
            {
                var trSelectMonthYear = document.getElementById("<%=trSelectMonthYear.ClientID%>");
                trSelectMonthYear.style.display = 'none';
                var trCutOff = document.getElementById("<%=trCutOff.ClientID%>");
                trCutOff.style.display = 'block';
            }
        }
   </script>
</asp:Content>
            



















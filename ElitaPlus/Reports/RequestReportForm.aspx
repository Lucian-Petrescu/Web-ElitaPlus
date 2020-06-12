<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RequestReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.RequestReportForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"  Theme="Default" %>


<%@ Register TagPrefix="uc1" TagName="multiplecolumnddlabelcontrol" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="multiplecolumnddlabelcontrol" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                <tr>
                    <td colspan="2">
                        <table class="formGrid">                  
                         
                              <tr id="trDates" runat="server" visible="false">
                                <td align="right">
                                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE</asp:Label>:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                    <asp:ImageButton ID="btnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                                <td align="right">
                                        <asp:Label ID="lblEndDate" runat="server">END_DATE</asp:Label>:    
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox" Width="175px"></asp:TextBox>
                                    <asp:ImageButton ID="btnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr id="trMonthandYear" runat="server" visible="false">
                                <td align="left">
                                    <asp:Label runat="server" ID="lblAcctPeriod">SELECT_MONTH_AND_YEAR</asp:Label>
                                    <asp:DropDownList ID="ddlAcctPeriodYear" runat="server" SkinID="SmallDropDown"  AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlAcctPeriodMonth" runat="server" SkinID="SmallDropDown"  AutoPostBack="true">                    
                                    </asp:DropDownList>
                                </td>                    
                            </tr>

                             <tr id ="trDates_space" runat="server" visible="false">
                                <td colspan="4"></td>
                            </tr>

                            <tr id="trCompany" runat="server" visible="false">
                                <td align="left" colspan="4">
                                    <asp:Panel ID="pnlCompanyDropControl" runat="server" Visible="true">
                                        <uc1:MultipleColumnDDLabelControl runat="server" ID="moCompanyMultipleDrop" />
                                    </asp:Panel>
                                </td>
                            </tr>

                            <tr id="trCompany_space" runat="server" visible="false">
                                <td colspan="4" ></td>
                            </tr>

                            <tr id="trDealer" runat="server" visible="false">
                                <td align="right">
                                        <asp:Label ID="lblDealer" runat="server">SELECT_THE_DEALER</asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:RadioButtonList ID="moDealerOptionList" AutoPostBack="true" runat="server" RepeatDirection="Vertical" CellPadding="2" CellSpacing="2">
                                        <asp:ListItem Text="SELECT_ALL_DEALERS" Value="DEALER_ALL" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="SELECT_SINGLE_DEALER" Value="DEALER_SINGLE"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Panel ID="pnlDealerDropControl" runat="server" Visible="false">
                                        <uc2:MultipleColumnDDLabelControl runat="server" ID="moDealerMultipleDrop" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            
                            <tr id="trDealer_space" runat="server" visible="false">
                                <td colspan="4" ></td>
                            </tr>

                            <tr id="trExtStatus" runat="server" visible="false">
                                <td align="left" colspan="4">                                   
                                    <asp:Panel ID="pnlExtStatusDropControl" runat="server" Visible="true">
                                        <uc2:MultipleColumnDDLabelControl runat="server" ID="moExtStatusMultipleDrop" />
                                    </asp:Panel>
                                </td>
                            </tr>

                            <tr id="trExtStatus_space" runat="server" visible="false">
                                <td colspan="4" ></td>
                            </tr>

                            <tr id="trbatchnumber" runat="server" visible="false">
                                <td align="right">
                                        <asp:Label ID="lblBatchNumber" runat="server">BATCH_NUMBER</asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtBatchNumber" runat="server" SkinID="MediumTextBox"/>
                                </td>
                            </tr>

                            <tr id="trbatchnumber_space" runat="server" visible="false">
                                <td colspan="4"></td>
                            </tr>
                 </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report Request" SkinID="AlternateLeftButton"></asp:Button>
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
    </script>
</asp:Content>

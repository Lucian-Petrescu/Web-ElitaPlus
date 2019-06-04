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

                             <tr id ="trDates_space" runat="server" visible="false">
                                <td colspan="4"></td>
                            </tr>

                            <%--<tr id="Tr2" runat="server">
                                <td align="right">*
                                        <asp:Label ID="lblCompany" runat="server">SELECT_THE_COMPANY</asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <asp:Panel ID="pnlCompanyDropControl" runat="server" Visible="true">
                                        <uc1:MultipleColumnDDLabelControl runat="server" ID="moCompanyMultipleDrop" />
                                    </asp:Panel>
                                </td>
                            </tr>--%>

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

                          <%--  <tr id="Tr4" runat="server">
                                <td align="right">*
                                        <asp:Label ID="lblExtStatus" runat="server">SELECT_THE_EXTENDED_STATUS</asp:Label>
                                </td>
                                <td align="left" colspan="3">                                   
                                    <asp:Panel ID="pnlExtStatusDropControl" runat="server" Visible="true">
                                        <uc2:MultipleColumnDDLabelControl runat="server" ID="moExtStatusMultipleDrop" />
                                    </asp:Panel>
                                </td>
                            </tr>--%>

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
<%--

                      <%--  <td colspan="2">
                            <table class="formGrid">
                                <tr id ="tr1Date" runat ="Server">
                                    <td align="left" width="30%"></td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE</asp:Label>
                                        <asp:TextBox ID="txtBeginDate" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="btnbeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>
                                    <td align="left">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEndDate" runat="server">END_DATE</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEndDate" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                                    <asp:ImageButton ID="btnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id ="tr1space" runat ="Server">
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr id ="tr2company" runat ="Server">
                                    <td align="left" width="30%"></td>
                                    <td style="width: 100%; height: 0.01%" valign="top" align="left" colspan="3" rowspan="1">
                                        <uc1:MultipleColumnDDLabelControl id="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                                    </td>
                                </tr>
                                <tr id ="tr2space" runat ="Server">
                                    <td align="right" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr id ="tr3dealer" runat ="Server">
                                    <td align="left" width="30%"></td>
                                    <td style="white-space: nowrap;" valign="middle" align="left">*
                                        <asp:RadioButton ID="rdealer" AutoPostBack="false" type="radio" Text="SELECT_ALL_DEALERS" TextAlign="left" runat="server" Checked="False"></asp:RadioButton>
                                         <%--<asp:RadioButton ID="rdealer" onclick=" 
                                             document.getElementById('ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDrop').selectedIndex = 0;
                                             document.getElementById('ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDropDesc').selectedIndex = 0; 
                                             ToggleExt(this, arrDealerCtr); "
                                             AutoPostBack="false" type="radio" GroupName="Dealer" Text="SELECT_ALL_DEALERS"
                                             TextAlign="left" runat="server" Checked="False"></asp:RadioButton>-
                                    </td>
                                    <td align="left" colspan="2">&nbsp;&nbsp;&nbsp;<uc2:MultipleColumnDDLabelControl id="moUserDealerMultipleDrop" runat="server"></uc2:MultipleColumnDDLabelControl>
                                    </td>
                                </tr>
                                 <tr id ="tr3space" runat ="Server">
                                    <td align="right" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="30%"></td>
                                    <td align="left" nowrap>&nbsp;&nbsp;
                                          <asp:RadioButton ID="rdealerGroup" Width="197px" AutoPostBack="false" type="radio"  Text="SELECT_ALL_GROUPS" TextAlign="left"
                                              runat="server" Checked="False"></asp:RadioButton>
                                     <%--  <asp:RadioButton ID="rdealerGroup" onclick="ToggleExt(this, arrDealerGroupCtr);"
                                              Width="197px" AutoPostBack="false" type="radio"  Text="SELECT_ALL_GROUPS" TextAlign="left" runat="server" GroupName="Dealer" Checked="False">
                                       </asp:RadioButton>
                                    </td>
                                    <td style="width: 100%;" valign="top" align="left" colspan="2" rowspan="1">
                                        <asp:Label ID="lbldealergroup" runat="server">OR_A_SINGLE_GROUP</asp:Label>:
                                        <asp:DropDownList ID="cboDealerGroup" runat="server" Width="190px" type="DropDown" AutoPostBack="false">
                                        </asp:DropDownList>
                                       <%-- <asp:DropDownList ID = "cboDealerGroup" runat="server" Width="190px" type="DropDown" AutoPostBack="false"
                                                   onchange = "ToggleExt(this, arrDealerGroupCtr);
                                                            document.getElementById('ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDrop').selectedIndex = 0;
                                                            document.getElementById('ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDropDesc').selectedIndex = 0;" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id ="tr4space" runat ="Server">
                                    <td align="right" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="30%"></td>
                                    <td align="left">&nbsp;&nbsp;
                                        <asp:Label ID="moExtendedLabel" runat="server">EXTENDED_STATUS</asp:Label>&nbsp;
                                        <asp:DropDownList ID="cboExtendedStatus" runat="server"></asp:DropDownList>
                                    </td>
                                    <td colspan="2" rowspan="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="30%"></td>
                                    <td align="left" width="25%">&nbsp;&nbsp;
                                        <asp:Label ID="lblBatchNumber" runat="server">BATCH_NUMBER</asp:Label>&nbsp;
                                        <asp:TextBox ID="txtBatchNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="false">
                                        </asp:TextBox>
                                    </td>
                                    <td align="left" colspan="2"></td>
                                </tr>
                            </table>
                        </td>
                    </tr> --%>
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

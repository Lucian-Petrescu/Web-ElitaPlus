<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoicesToBePaidForm.aspx.vb"
    MasterPageFile="~/Reports/content_Report.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.InvoicesToBePaidForm" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
     <table cellpadding="2" cellspacing="2" border="0" style="vertical-align: top; width:100%; text-align: center;">
        <tr id="trSelectAllComp" runat="server">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td colspan="3">
                <asp:RadioButton ID="rbnSelectAllComp" runat="server" AutoPostBack="true" Text="SELECT_ALL_COMPANIES"
                TextAlign="Left" />
            </td>
        </tr>
        <tr id="trcomp" runat="server">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;">
                *&nbsp;<asp:Label ID="lblCompany" runat="server">SELECT_COMPANY:</asp:Label>
            </td>
            <td colspan="2">
                <uc1:MultipleColumnDDLabelControl ID="multipleCompDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr runat="server" id="trHrAfterCompanyRow">
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr runat="server" id="trSelectAllDealers">
            <td align="left" width="30%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom; white-space: nowrap;">
                <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left"
                    runat="server" Checked="True"></asp:RadioButton>
            </td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="multipleDealerDropControl" runat="server">
                </uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr runat="server" id="trHrAfterSelectAllDealersRow">
            <td align="left" width="30%">
            </td>
            <td align="left" width="90%" colspan="3">
            </td>
        </tr>
        <tr runat="server" id="trOnlyDealersWith">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td style="white-space: nowrap;">
                <asp:Label ID="moProductLabel" runat="server">Or only Dealers With</asp:Label>:
            </td>
            <td align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlDealerCurrency" runat="server" AutoPostBack="false"
                    onchange="javascript:return ToggleDropdownsforCurrency();" Width="212px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trHrAfterOnlyDealersWithRow">
            <td align="right" colspan="4">
                <hr />
            </td>
        </tr>
        <tr runat="server" id="trCurrency">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom; white-space: nowrap;">
                <asp:Label ID="lblCurrency" runat="server">Currency</asp:Label>:
            </td>
            <td align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="false"
                    Width="212px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trHrAfterCurrencyRow">
            <td align="right" colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom; white-space: nowrap;" colspan="3">
                <table cellspacing="2" cellpadding="0" width="75%" border="0">
                    <tr>
                        <td nowrap align="right">
                            <asp:RadioButton ID="RadiobuttonByReportingPeriod" onclick="toggleOptionSelection(true);"
                                TextAlign="left" Text="Base Report On Reporting Period" runat="server" AutoPostBack="false">
                            </asp:RadioButton>
                        </td>
                        <td nowrap align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RadiobuttonByInvoiceNumber" onclick="toggleOptionSelection(false);"
                                TextAlign="left" Text="Base Report On An Invoice Number" runat="server" AutoPostBack="false">
                            </asp:RadioButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                       
                        <td align="center" width="50%" colspan="3">
                            <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                <tr id="DateRow" <%=toggleDisplay("period")%>>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                    <td valign="middle" nowrap align="right">
                                        <asp:Label ID="BeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
                                    </td>
                                    <td nowrap>
                                        &nbsp;
                                        <asp:TextBox ID="BeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                            Height="17px" Width="20px"></asp:ImageButton>
                                    </td>
                                    <td valign="middle" nowrap align="right">
                                        &nbsp;&nbsp;
                                        <asp:Label ID="EndDateLabel" runat="server">END_DATE</asp:Label>:
                                    </td>
                                    <td nowrap>
                                        &nbsp;
                                        <asp:TextBox ID="EndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                            Width="125px"></asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                            Height="17px" Width="20px"></asp:ImageButton>
                                    </td>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="InvoiceRow" <%=toggleDisplay("invoice")%>>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                    <td valign="middle" nowrap align="right">
                                        <asp:Label ID="InvoiceNumberLabel" runat="server">INVOICE_NUMBER</asp:Label>:
                                    </td>
                                    <td nowrap>
                                        &nbsp;
                                        <asp:TextBox ID="InvoiceNumberTextbox" TabIndex="1" runat="server" AutoPostBack="True"
                                            CssClass="FLATTEXTBOX" onblur="DisplayPayeeRow()"></asp:TextBox>&nbsp;
                                    </td>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="PayeeRow" <%=toggleDisplay("payee")%>>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                    <td valign="middle" nowrap align="right">
                                        <asp:Label ID="PayeeLabel" runat="server">Payee</asp:Label>:
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:DropDownList ID="cboPayee" runat="server" Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="InvoiceOrClaim" style="display:block;">
                                    <td nowrap align="right">
                                        <asp:RadioButton ID="RadiobuttonClaims" GroupName="InvoiceClaim"
                                                         TextAlign="left" Text="Claims" runat="server" AutoPostBack="false">
                                        </asp:RadioButton>
                                    </td>
                                    <td nowrap align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="RadiobuttonInvoices" GroupName="InvoiceClaim"
                                                         TextAlign="left" Text="Invoices" runat="server" AutoPostBack="false">
                                        </asp:RadioButton>
                                    </td>
                                 </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>

                                <tr>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                    <td valign="middle" nowrap align="right" colspan="2">
                                        <asp:CheckBox ID="chkSvcCode" Text="INCLUDE_SERVICE_CENTER_CODE" AutoPostBack="false"
                                            runat="server" TextAlign="Left"></asp:CheckBox>
                                        <td width="50%">
                                            &nbsp;
                                        </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        &nbsp;
                                    </td>
                                    <td valign="middle" nowrap align="right" colspan="2">
                                        <asp:CheckBox ID="chkCustomerAddress" Text="INCLUDE_CUSTOMER_ADDRESS" AutoPostBack="false"
                                            runat="server" TextAlign="Left"></asp:CheckBox>
                                        <td width="50%">
                                            &nbsp;
                                        </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1" style="width: 30%">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                   
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function DisplayPayeeRow() {
            document.getElementById("PayeeRow").style.display = 'block';
        }

        function toggleOptionSelection(isByReportingPeriod) {
            //debugger;

            if (isByReportingPeriod) {
                document.getElementById("ctl00_ContentPanelMainContentBody_RadiobuttonByInvoiceNumber").checked = false;
                document.getElementById("InvoiceRow").style.display = 'none';
                document.getElementById("PayeeRow").style.display = 'none';
                document.getElementById("DateRow").style.display = 'block';
            }
            else {
                document.getElementById("ctl00_ContentPanelMainContentBody_RadiobuttonByReportingPeriod").checked = false;
                document.getElementById("InvoiceRow").style.display = 'block';
                document.getElementById("DateRow").style.display = 'none';
                document.getElementById("PayeeRow").style.display = 'none';
            }
        }

        function toggleReportFormatViewSelection(isView) {
            //debugger;
            if (isView) {
                document.getElementById("InvoiceOrClaim").style.display = 'block';
            }

        }

        function toggleReportFormatTXTSelection(isTXT) {
            //debugger;
            if (isTXT) {
                document.getElementById("InvoiceOrClaim").style.display = 'none';
            }

        }

        function ToggleDropdownsforCurrency() {
            var curdropdownValue = document.getElementById("ctl00_ContentPanelMainContentBody_ddlDealerCurrency").value;




            //if only dealers with is chosen, then select all dealer and individual dealers have to be cleared
            if (curdropdownValue != '00000000-0000-0000-0000-000000000000') {
                document.getElementById("ctl00_ContentPanelMainContentBody_rdealer").checked = false;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDropDesc").selectedIndex = 0;

            }
            else {
                document.getElementById("ctl00_ContentPanelMainContentBody_rdealer").checked = true;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>
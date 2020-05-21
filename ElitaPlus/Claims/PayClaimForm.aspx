<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PayClaimForm.aspx.vb"
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PayClaimForm" %>

<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="searchGrid">
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelClaimNumber" runat="server">Claim_Number</asp:Label>:
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="TextboxClaimNumber" TabIndex="1" SkinID="SmallTextBox" runat="server"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td valign="middle" nowrap align="right">
                <asp:Label ID="LabelCertificateNumber" runat="server">Certificate</asp:Label>:
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="TextboxCertificateNumber" SkinID="SmallTextBox" TabIndex="2" runat="server"
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelSerialNumber" runat="server">Serial_Number</asp:Label>:
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="TextSerialNumber" TabIndex="3" runat="server" SkinID="SmallTextBox"
                    MaxLength="30"></asp:TextBox>
            </td>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelModel" runat="server">Model</asp:Label>:
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="TextModel" TabIndex="4" SkinID="SmallTextBox" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelManufacturer" runat="server">Manufacturer</asp:Label>:
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="TextManufacturer" SkinID="SmallTextBox" TabIndex="5" runat="server"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelRiskType" runat="server">Risk_Type</asp:Label>:
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="TextboxRiskType" SkinID="SmallTextBox" TabIndex="6" runat="server"
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="bottom" nowrap align="left" colspan="4" height="10">
                <hr style="width: 100%;" />
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="left" width="1%" colspan="2">
                <asp:Label ID="LabelCustomerName" runat="server">Customer_Name</asp:Label>
            </td>
            <td valign="middle" align="left" width="1%" colspan="2">
                <asp:Label ID="LabelBillingSvcCenter" runat="server">Billing_Service_Center</asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="left" width="50%" colspan="2">
                <asp:TextBox ID="CustomerAddressLabel" SkinID="SmallTextBox" TabIndex="23" runat="server"
                    ReadOnly="True" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </td>
            <td valign="middle" align="left" width="50%" colspan="2">
                <asp:TextBox ID="ServiceCenterAddressLabel" SkinID="SmallTextBox" TabIndex="24" runat="server"
                    ReadOnly="True" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="bottom" nowrap align="left" colspan="4">
                <hr style="width: 100%;" />
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelPayee" runat="server">Payee</asp:Label>
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:DropDownList ID="cboPayeeSelector" TabIndex="10" runat="server" SkinID="SmallDropDown"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelInvNumber" runat="server">Invoice_Number</asp:Label>
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="txtInvoiceNumber" TabIndex="11" runat="server" SkinID="SmallTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="baseline" nowrap align="right" width="1%" colspan="2"></td>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelInvDateAsterisk" runat="server" ForeColor="Red">*</asp:Label>
                <asp:Label ID="LabelInvoiceDate" runat="server">Invoice_Date</asp:Label>
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="txtInvoiceDate" TabIndex="11" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonInvoiceDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                    Visible="True" CausesValidation="False" ImageAlign="AbsMiddle"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="Req_Doc_TypeLabel" runat="server" ForeColor="Red">*</asp:Label>
                <asp:Label ID="moDocumentTypeLabel" runat="server">DOCUMENT_TYPE</asp:Label>
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:DropDownList ID="cboDocumentTypeId" TabIndex="12" runat="server" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="Req_Doc_NumLabel" runat="server">*</asp:Label>
                <asp:Label ID="moTaxIdLabel" runat="server">DOCUMENT_NUMBER</asp:Label>
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="moTaxIdText" TabIndex="13" runat="server" SkinID="SmallTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 17px" valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="Label2" runat="server">Name</asp:Label>:
            </td>
            <td style="height: 17px" valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="txtPayeeName" TabIndex="14" runat="server" SkinID="SmallTextBox"></asp:TextBox>
            </td>
            <td style="height: 17px" valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelCauseOfLoss" runat="server">Cause_of_loss</asp:Label>
            </td>
            <td style="height: 17px" valign="middle" align="left" width="50%">&nbsp;
                <asp:DropDownList ID="cboCauseOfLossID" TabIndex="15" runat="server" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="bottom" nowrap align="left" colspan="4" height="10">
                <hr style="width: 100%;" />
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="Req_Pay_MethodLabel" runat="server">*</asp:Label>
                <asp:Label ID="moPaymentMethodLabel" runat="server">PAYMENT_METHOD</asp:Label>
            </td>
            <td valign="middle">&nbsp;
                <asp:DropDownList ID="PaymentMethodDrop" TabIndex="16" runat="server" SkinID="SmallDropDown"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td valign="middle" align="right"></td>
            <td valign="middle"></td>
        </tr>
        <uc1:UserControlAddress ID="PayeeAddress" runat="server"></uc1:UserControlAddress>
        <uc1:UserControlBankInfo ID="PayeeBankInfo" runat="server"></uc1:UserControlBankInfo>
        <tr id="hrSeprator" runat="server">
            <td style="height: 21px" valign="bottom" nowrap align="right" width="1%" colspan="4">
                <hr style="width: 100%;" />
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelRepairDate" runat="server">Repair_date</asp:Label>
            </td>
            <td valign="middle">&nbsp;
                <asp:TextBox ID="txtRepairDate" TabIndex="21" runat="server" SkinID="SmallTextBox"
                    Width="50%"></asp:TextBox>
                <asp:TextBox ID="txtAcctStatusDate" TabIndex="21" runat="server" SkinID="SmallTextBox"
                    Width="50%"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonRepairDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                    Visible="True" CausesValidation="False" ImageAlign="AbsMiddle"></asp:ImageButton>
            </td>
            <td style="height: 21px" valign="middle" align="right">
                <asp:Label ID="LabelRepairCode" runat="server">REPAIR_CODE</asp:Label>
            </td>
            <td valign="middle">&nbsp;
                <asp:DropDownList ID="cboRepairCode" TabIndex="24" runat="server" SkinID="SmallDropDown">
                </asp:DropDownList>
                <asp:TextBox ID="txtAcctStatusCode" TabIndex="21" runat="server" SkinID="SmallTextBox"
                    Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" nowrap align="right" width="1%">
                <asp:Label ID="LabelPickUpDate" runat="server">PickUp_Date</asp:Label>
            </td>
            <td valign="middle" align="left" width="50%">&nbsp;
                <asp:TextBox ID="TextboxPickupDate" TabIndex="23" runat="server" SkinID="SmallTextBox"
                    Width="50%"></asp:TextBox>
                <asp:TextBox ID="txtboxTrackingNumber" TabIndex="23" runat="server" SkinID="SmallTextBox"
                    Width="50%"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonPickupDate" TabIndex="30" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                    Visible="True" ImageAlign="AbsMiddle"></asp:ImageButton>
            </td>
            <td valign="middle" nowrap align="right" background=" ">
                <asp:Label ID="LabelLoanerReturnedDate" runat="server">Loaner_returned_date</asp:Label>
            </td>
            <td valign="middle">&nbsp;
                <asp:TextBox ID="txtLoanerReturnedDate" TabIndex="20" runat="server" SkinID="SmallTextBox"
                    Width="50%"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonLoanerReturnedDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                    Visible="True" CausesValidation="False"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <%--<asp:TextBox ID="TextBox1" TabIndex="21" runat="server" CssClass="FLATTEXTBOX" Width="50%"></asp:TextBox>--%>
            <td style="height: 21px" nowrap valign="middle" align="right">
                <asp:Label ID="lblRigion" runat="server">Perception_IIBB_Province</asp:Label>
            </td>
            <td valign="middle">&nbsp;
                <asp:DropDownList ID="cboRegionDropID" TabIndex="24" runat="server" SkinID="SmallDropDown">
                </asp:DropDownList>
                <%--<asp:TextBox ID="TextBox1" TabIndex="21" runat="server" CssClass="FLATTEXTBOX" Width="50%"></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td style="height: 26px" valign="bottom" nowrap align="left" colspan="4" height="26">
                <hr style="width: 100%;" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table valign="top" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr runat="server" visible="true">
                        <td id="tdLiabilityLimit" valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelLiabilityLimit" runat="server">Liability_Limit</asp:Label>:
                        </td>
                        <td id="tdLiabilityLimittxt" valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtLiabilityLimit" SkinID="SmallTextBox" TabIndex="25" runat="server"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">&nbsp;</td>
                        <td valign="middle" align="Center" width="10%">&nbsp;</td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:Label ID="LabelTaxType" runat="server">Tax_Type</asp:Label>&nbsp;<asp:Label ID="LabelTaxAmount" runat="server">Tax_Amount</asp:Label>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:Label ID="LabelWithholding" runat="server">Withholding</asp:Label>
                        </td>
                    </tr>
                    <tr id="trDeductiblelabor" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelDeductible" runat="server">Deductible</asp:Label>:
                        </td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtDeductible" SkinID="SmallTextBox" TabIndex="26" runat="server"
                                ReadOnly="False"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">
                            <asp:Label ID="LabelLabor" runat="server">Labor</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtLabor" TabIndex="100" runat="server" SkinID="SmallTextBox"></asp:TextBox></td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtLaborTax" TabIndex="166" Width="90%" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxLaborWithhodling" TabIndex="211" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trDiscountparts" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelDiscount" runat="server">DISCOUNT</asp:Label>:
                        </td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtDiscount" SkinID="SmallTextBox" TabIndex="27" runat="server"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">
                            <asp:Label ID="LabelParts" runat="server">Parts</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtParts" TabIndex="101" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtPartsTax" TabIndex="177" Width="90%" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxPartWithhodling" TabIndex="212" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trAuthamtsrvcharge" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelAuthAmt" runat="server">Auth_amt</asp:Label>:
                        </td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtAuthAmt" SSkinID="SmallTextBox" TabIndex="27" runat="server"
                                SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">
                            <asp:Label ID="LabelSvcCharge" runat="server">Service_Charge</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtServiceCharge" TabIndex="102" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtServiceChargeTax" TabIndex="188" Width="90%" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxServiceWithhodling" TabIndex="213" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trAbovelibtripamt" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelAboveLiability" runat="server">Above_Liability</asp:Label>:
                        </td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtAboveLiability" SkinID="SmallTextBox" TabIndex="28" runat="server"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">
                            <asp:Label ID="LabelTripAmount" runat="server">Trip_Amount</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtTripAmt" SkinID="SmallTextBox" TabIndex="103" runat="server"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtTripAmtTax" TabIndex="199" Width="90%" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxTripWithhodling" TabIndex="214" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trSalvsgeamtShipping" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelSalvageAmt" runat="server">Salvage_Amount</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtSalvageAmt" SkinID="SmallTextBox" TabIndex="28" runat="server"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">
                            <asp:Label ID="LabelShipping" runat="server">Shipping</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtShipping" SkinID="SmallTextBox" TabIndex="104" runat="server"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtShippingTax" TabIndex="199" Width="90%" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxShippingWithhodling" TabIndex="215" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trConsumerpayDisposition" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="Label1" runat="server">CONSUMER_PAY</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtConsumerPays" SkinID="SmallTextBox" TabIndex="29" runat="server"
                                Width="55%" ReadOnly="False"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">
                            <asp:Label ID="LabelDisposition" runat="server">Disposition</asp:Label>:
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtDisposition" runat="server" SkinID="SmallTextBox" TabIndex="105"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtDispositionTax" Width="90%" runat="server" SkinID="SmallTextBox" TabIndex="211" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxDispositionWithhodling" TabIndex="216" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trAssurantPaysDiadnostics" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelAssurantPays" runat="server">ASSURANT_PAY</asp:Label></td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtAssurantPays" SkinID="SmallTextBox" TabIndex="30" runat="server"
                                Width="55%" ReadOnly="False"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="10%">
                            <asp:Label ID="LabelDiagnostics" runat="server">Diagnostics</asp:Label>:
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtDiagnostics" runat="server" SkinID="SmallTextBox" TabIndex="106"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtDiagnosticsTax" Width="90%" runat="server" SkinID="SmallTextBox" TabIndex="212" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxDiagnosticsWithhodling" TabIndex="217" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelAlreadyPaid" runat="server">Already_Paid</asp:Label></td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtAlreadyPaid" SkinID="SmallTextBox" TabIndex="32" runat="server"
                                Width="55%" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <div runat="server" id="divlblPaymenttoCustomer">
                                <asp:Label ID="lblPaymenttoCustomer" runat="server">PAYMENT_TO_CUSTOMER</asp:Label>:
                            </div>

                        </td>
                        <td valign="middle" align="left" width="10%"> <div runat="server" id="divtxtPaymenttoCustomer"> &nbsp;
                            
                            <asp:TextBox ID="txtPaymenttoCustomer" runat="server" SkinID="SmallTextBox" TabIndex="107"
                            onchange="UpdateGrandTotalAmount();"></asp:TextBox>
                                </div>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                             <div runat="server" id="divtxtPaymenttoCustomertax">
                            <asp:TextBox ID="txtPaymenttoCustomertax" Width="90%" runat="server" SkinID="SmallTextBox" Text="0" TabIndex="212"
                                ReadOnly="True"></asp:TextBox>
                                 </div>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <div runat="server" id="divCheckBoxPaymenttocustomer">
                            <asp:CheckBox ID="CheckBoxPaymenttocustomer" TabIndex="219" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                                </div>
                        </td>
                    </tr>
                    <tr id="trRemainingAmtOtherAmt" runat="server" visible="true">
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelRemainingAmt" runat="server">Remaining_Amount</asp:Label>
                        </td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                            <asp:TextBox ID="txtRemainingAmt" SkinID="SmallTextBox" TabIndex="34" runat="server"
                                Width="55%" ReadOnly="False"></asp:TextBox>
                        </td>

                        <td valign="middle" align="right" width="10%">
                            <asp:TextBox ID="txtOtherDesc" TabIndex="107" runat="server" SkinID="SmallTextBox"
                                Width="90%" Style="text-align: 'right';"></asp:TextBox>:
                        </td>
                        <td valign="middle" align="left" width="10%">&nbsp;<asp:TextBox ID="txtOtherAmt" SkinID="SmallTextBox" TabIndex="108" runat="server"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:TextBox ID="txtOtherTax" TabIndex="144" Width="90%" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td valign="middle" nowrap align="left" width="10%">
                            <asp:CheckBox ID="CheckBoxOtherWithhodling" TabIndex="218" runat="server" CssClass="disabled" Enabled="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" colspan="4">
                            <hr style="width: 100%;" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" nowrap align="right" width="1%"></td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                        </td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelSubTotal" runat="server">Sub_Total</asp:Label>:
                        </td>
                        <td valign="middle" align="left" width="15%" colspan="3">&nbsp;
                            <asp:TextBox ID="txtSubTotal" SkinID="SmallTextBox" TabIndex="31" runat="server"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trTotalTax_Amount">
                        <td valign="middle" nowrap align="right" width="1%">&nbsp;</td>
                        <td valign="middle" align="left" width="25%">&nbsp;</td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelTotalTaxAmount" runat="server">TOTAL_TAX_AMOUNT</asp:Label>:
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtTotalTaxAmount" runat="server"
                                SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trIVA_Amount">
                        <td valign="middle" nowrap align="right" width="1%"></td>
                        <td valign="middle" align="left" width="25%">&nbsp;
                        </td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelIvaTax" runat="server" Font-Bold="false">IVA_AMOUNT:</asp:Label>
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtIvaTax" SkinID="SmallTextBox" TabIndex="33" runat="server"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trDeductibleAmount">
                        <td colspan="2"></td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelDeductibleAmount" runat="server">Deductible:</asp:Label>
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtDeductibleAmount" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trDeductibleTaxAmount">
                        <td colspan="2"></td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelDeductibleTaxAmount" runat="server">Deductible_Tax_Amount:</asp:Label>
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtDeductibleTaxAmount" runat="server"
                                SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" nowrap align="right" width="1%"></td>
                        <td valign="top" align="left" width="25%">&nbsp;
                        </td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelTotal" runat="server">Total</asp:Label>
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtTotal" SkinID="SmallTextBox" TabIndex="12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="LabelTotalWithholdingAmount" runat="server">TOTAL_WITHHOLDING_AMOUNT</asp:Label>:</td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtTotalWithholdingAmount" runat="server"
                                SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trPerception_IVA" runat="server" visible="false">
                        <td colspan="2"></td>
                        <td valign="middle" nowrap align="right" width="1%">
                            <asp:Label ID="lblPerception_Iva" runat="server">Perception_IVA</asp:Label>
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtPerceptionIva" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trPerception_IIBB" runat="server" visible="false">
                        <td colspan="2"></td>
                        <td valign="middle" style="white-space: nowrap" align="right" width="1%">
                            <asp:Label ID="lblPerception_IIBB" runat="server">Perception_IIBB</asp:Label>
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtPerceptionIIBB" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trManualTax" runat="server" visible="false">
                        <td colspan="2"></td>
                        <td valign="middle" style="white-space: nowrap" align="right" width="1%">
                            <asp:Label ID="lblManualTax" runat="server">MANUALLY_ENTERED_TAXES:</asp:Label>
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtManualTax" runat="server" SkinID="SmallTextBox" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trGrandTotal" runat="server" visible="false">
                        <td colspan="2"></td>
                        <td valign="middle" style="white-space: nowrap" align="right" width="1%">
                            <asp:Label ID="lblGrandTotal" runat="server">GRAND_TOTAL</asp:Label>:
                        </td>
                        <td valign="middle" align="left" colspan="3">&nbsp;
                            <asp:TextBox ID="txtGrandTotal" runat="server" SkinID="SmallTextBox"
                                RealOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="bottom" nowrap align="left" colspan="4">
                <div class="btnZone" style="width: 100%;">
                    <asp:Button ID="btnBack" SkinID="AlternateLeftButton" TabIndex="35" runat="server"
                        FCausesValidation="False" Text="Back"></asp:Button>&nbsp;
                    <asp:Button ID="btnSave_WRITE" SkinID="AlternateLeftButton" TabIndex="36" runat="server"
                        CausesValidation="False" Text="Pay Invoice"></asp:Button>&nbsp;
                    <asp:Button ID="btnUndo_Write" SkinID="AlternateLeftButton" TabIndex="37" runat="server"
                        CausesValidation="False" Text="Undo"></asp:Button>&nbsp;
                    <asp:Button ID="btnPartsInfo_WRITE" SkinID="AlternateLeftButton" TabIndex="38" runat="server"
                        CausesValidation="False" Text="PARTS   INFO"></asp:Button>&nbsp;
                    <asp:Button ID="btnAuthDetail_WRITE" SkinID="AlternateLeftButton" TabIndex="39" runat="server"
                        CausesValidation="False" Text="AUTH_DETAIL"></asp:Button>&nbsp;
                    <asp:Button ID="btnReplacement_WRITE" SkinID="AlternateLeftButton" TabIndex="40"
                        runat="server" CausesValidation="False" Text="REPLACEMENT"></asp:Button>&nbsp;
                    <asp:Button ID="btnTaxes_WRITE" SkinID="AlternateLeftButton" TabIndex="41" runat="server"
                        CausesValidation="False" Text="TAXES"></asp:Button>&nbsp;
                </div>
                <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
                <input id="MasterCenterIvaResponsible" type="hidden" name="MasterCenterIvaResponsible" runat="server" />
                <input id="ServiceCenterIvaResponsible" type="hidden" name="ServiceCenterIvaResponsible" runat="server" />
                <input id="LoanerCenterIvaResponsible" type="hidden" name="LoanerCenterIvaResponsible" runat="server" />
                <input id="TaxRate" type="hidden" name="TaxRate" runat="server" />
                <input id="PayDeductible" type="hidden" name="PayDeductible" runat="server" />
                <input id="RemainingDeductible" type="hidden" name="RemainingDeductible" runat="server" />
                <!--DEF-1631 -->
                <input id="hdDeductAmtByUser" type="hidden" name="hdDeductAmtByUser" runat="server" />
                <!--End of DEF-1631 -->
                <input id="DeductibleTaxRate" type="hidden" name="DeductibleTaxRate" runat="server" />
                <input id="PayeeCode" type="hidden" name="PayeeCode" runat="server" />
                <input id="hdTotal" type="hidden" name="hdTotal" runat="server" />
                <input id="hdTotalTaxAmount" type="hidden" name="hdTotalTaxAmount" runat="server" />
                <input id="hdPerceptionTax" type="hidden" name="hdPerceptionTax" runat="server" value="0.00" />
                <input id="hdAssurantPays" type="hidden" name="hdAssurantPays" runat="server" />
                <input id="hdAlreadyPaid" type="hidden" name="hdAlreadyPaid" runat="server" />
                <input id="hdPartsAmt" type="hidden" name="hdPartsAmt" runat="server" />
                <input id="hdLaborAmt" type="hidden" name="hdLaborAmt" runat="server" />
                <input id="hdServiceChargeAmt" type="hidden" name="hdServiceChargeAmt" runat="server" />
                <input id="hdTripAmt" type="hidden" name="hdTripAmt" runat="server" />
                <input id="hdShippingAmt" type="hidden" name="hShippingAmt" runat="server" />
                <input id="hdDispositionAmt" type="hidden" name="hdDispositionAmt" runat="server" />
                <input id="hdDiagnosticsAmt" type="hidden" name="hdDiagnosticsAmt" runat="server" />
                <input id="hdOtherAmt" type="hidden" name="hdOtherAmt" runat="server" />
                <input id="hdwithholdingAmt" type="hidden" name="hdwithholdingAmt" runat="server" />
                <input id="hdSalvageAmt" type="hidden" name="hdSalvageAmt" runat="server" />
                <input id="hdDeliveryFeeOnly" type="hidden" name="hdDeliveryFeeOnly" runat="server" />
                <input id="hdDeductibleAmt" type="hidden" name="hdDeductibleAmt" runat="server" />
                <input id="InvoiceMethod" type="hidden" name="InvoiceMethod" runat="server" />
                <input id="hdpaymenttocustomer" type="hidden" name="hdpaymenttocustomer" runat="server" />

                <input id="hdLaborTaxAmt" type="hidden" name="hdLaborTaxAmt" runat="server" />
                <input id="hdPartsTaxAmt" type="hidden" name="hdPartsTaxAmt" runat="server" />
                <input id="hdServiceChargeTaxAmt" type="hidden" name="hdServiceChargeTaxAmt" runat="server" />
                <input id="hdTripTaxAmt" type="hidden" name="hdTripTaxAmt" runat="server" />
                <input id="hdShippingTaxAmt" type="hidden" name="hdShippingTaxAmt" runat="server" />
                <input id="hdDispositionTaxAmt" type="hidden" name="hdDispositionTaxAmt" runat="server" />
                <input id="hdDiagnosticsTaxAmt" type="hidden" name="hdDiagnosticsTaxAmt" runat="server" />
                <input id="hdOtherTaxAmt" type="hidden" name="hdOtherTaxAmt" runat="server" />
                <input id="hdSubTotalAmt" type="hidden" name="hdSubTotalAmt" runat="server" />
                <input id="hdTotalAmt" type="hidden" name="hdTotalAmt" runat="server" />
                <input id="hdTotalTaxAmt" type="hidden" name="hdTotalTaxAmt" runat="server" />
                <input id="hdGrandTotalAmt" type="hidden" name="hdGrandTotalAmt" runat="server" />
                <input id="hdpaymenttocustomertax" type="hidden" name="hdpaymenttocustomertax" runat="server" />

                <input id="hdTaxRateClaimDiagnostics" type="hidden" name="hdTaxRateClaimDiagnostics" runat="server" />
                <input id="hdComputeMethodClaimDiagnostics" type="hidden" name="hdComputeMethodClaimDiagnostics" runat="server" />
                <input id="hdApplyWithholdingFlagClaimDiagnostics" type="hidden" name="hdApplyWithholdingFlagClaimDiagnostics" runat="server" />

                <input id="hdTaxRateClaimOther" type="hidden" name="hdTaxRateClaimOther" runat="server" />
                <input id="hdComputeMethodClaimOther" type="hidden" name="hdComputeMethodClaimOther" runat="server" />
                <input id="hdApplyWithholdingFlagClaimOther" type="hidden" name="hdApplyWithholdingFlagClaimOther" runat="server" />

                <input id="hdTaxRateClaimDisposition" type="hidden" name="hdTaxRateClaimDisposition" runat="server" />
                <input id="hdComputeMethodClaimDisposition" type="hidden" name="hdComputeMethodClaimDisposition" runat="server" />
                <input id="hdApplyWithholdingFlagClaimDisposition" type="hidden" name="hdApplyWithholdingFlagClaimDisposition" runat="server" />

                <input id="hdTaxRateClaimLabor" type="hidden" name="hdTaxRateClaimLabor" runat="server" />
                <input id="hdComputeMethodClaimLabor" type="hidden" name="hdComputeMethodClaimLabor" runat="server" />
                <input id="hdApplyWithholdingFlagClaimLabor" type="hidden" name="hdApplyWithholdingFlagClaimLabor" runat="server" />

                <input id="hdTaxRateClaimParts" type="hidden" name="hdTaxRateClaimParts" runat="server" />
                <input id="hdComputeMethodClaimParts" type="hidden" name="hdComputeMethodClaimParts" runat="server" />
                <input id="hdApplyWithholdingFlagClaimParts" type="hidden" name="hdApplyWithholdingFlagClaimParts" runat="server" />

                <input id="hdTaxRateClaimShipping" type="hidden" name="hdTaxRateClaimShipping" runat="server" />
                <input id="hdComputeMethodClaimShipping" type="hidden" name="hdComputeMethodClaimShipping" runat="server" />
                <input id="hdApplyWithholdingFlagClaimShipping" type="hidden" name="hdApplyWithholdingFlagClaimShipping" runat="server" />

                <input id="hdTaxRateClaimService" type="hidden" name="hdTaxRateClaimService" runat="server" />
                <input id="hdComputeMethodClaimService" type="hidden" name="hdComputeMethodClaimService" runat="server" />
                <input id="hdApplyWithholdingFlagClaimService" type="hidden" name="hdApplyWithholdingFlagClaimTrip" runat="server" />

                <input id="hdTaxRateClaimTrip" type="hidden" name="hdTaxRateClaimTrip" runat="server" />
                <input id="hdComputeMethodClaimTrip" type="hidden" name="hdComputeMethodClaimTrip" runat="server" />
                <input id="hdApplyWithholdingFlagClaimTrip" type="hidden" name="hdApplyWithholdingFlagClaimTrip" runat="server" />

                <input id="hdClaimMethodOfRepair" type="hidden" name="hdClaimMethodOfRepair" runat="server" />
                <input id="hdWithholdingRate" type="hidden" name="hdWithholdingRate" runat="server" />
                <input id="hdTotalWithholdings" type="hidden" name="hdTotalWithholdings" runat="server" />&nbsp;
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        var selectedPayeeValue = document.getElementById("ctl00_SummaryPlaceHolder_cboPayeeSelector").value;

        var objManualTax = document.getElementById("ctl00_SummaryPlaceHolder_txtManualTax");
        if (objManualTax)
            doAmtCalc(objManualTax);

        var originalAmount = 0;
        var isPerceptionTax = false;
        function getTotalAmount(obj) {
            if (isPerceptionTax == false) {
                originalAmount = obj.value;
            }
            isPerceptionTax = true;
            doAmtCalc(obj);
        }

        function trim(str) {
            s = str.replace(/^(\s)*/, '');
            s = s.replace(/(\s)*$/, '');
            return s;
        }

        function round_num(number, x) { // function added by Oscar on 06/01/2005 
            x = (!x ? 2 : x);
            return Math.round(number * Math.pow(10, x)) / Math.pow(10, x);
        }

        function isPointFormat() {
            return IsPointFormat;
        }

        function FormatToDecimal1(value) {
            //debugger;
            if (trim(value) == "")
                value = 0;

            if (value > 0)
                value = parseFloat(value) + 0.0001;
            else
                value = parseFloat(value) - 0.0001;

            var svalue = value.toString();
            var index = svalue.indexOf(".");
            var result = svalue.substring(0, index + 3);
            if (result == "-0.00")
                result = "0.00";
            return result;
        }

        function computeTaxAmtByComputeMethod(amount, TaxRate, ComputeMethodCode) {
            var taxAmount = 0;
            if (amount > 0) {
                if (ComputeMethodCode == 'N')
                    taxAmount = round_num(((amount * TaxRate) / 100), 2);
                else {
                    var gross = 0;
                    gross = round_num((amount) / (1.0 + TaxRate), 2);
                    if (gross != 0) {
                        taxAmount = round_num(((amount / gross) - 1), 2);
                    }
                }
            }

            return taxAmount;
        }

        function doAmtCalc(obj) {
            //debugger;
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var AuthDetailReq = '<%=AuthDetailRequired%>';

            //obj.value = setCultureFormat(setJsFormat(obj.value, decSep));
            //obj.value = convertNumberToCulture(obj.value, decSep, groupSep);

            // DEF-3258 : START
            if (obj.value.length == 0) // : If value is blank consider it as 0.00 (Earlier it was displaying message and then making value as 0.00)
            {
                obj.value = convertNumberToCulture(FormatToDecimal(round_num(0, 2).toString()), decSep, groupSep);
            } // DEF-3258 : END

            if (ValidCultureValue(parseFloat(setJsFormat(obj.value, decSep)))) {
                var hdTotal = document.getElementById("ctl00_SummaryPlaceHolder_hdTotal");
                var hdPerceptionTax = document.getElementById("ctl00_SummaryPlaceHolder_hdPerceptionTax");
                var hdDispositionAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdDiagnosticsAmt");
                var hdDiagnosticsAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdDispositionAmt");
                var hdPartsAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdPartsAmt");
                var hdLaborAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdLaborAmt");
                var hdpaymenttocustomer = document.getElementById("ctl00_SummaryPlaceHolder_hdpaymenttocustomer");
                var hdServiceChargeAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdServiceChargeAmt");
                var hdTripAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdTripAmt");
                var hdShippingAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdShippingAmt");
                var hdOtherAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdOtherAmt");
                var hdwithholdingAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdwithholdingAmt");
                var hdSalvageAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdSalvageAmt");
                var hdDeductibleAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdDeductibleAmt");
                //var payeeList = document.getElementById("cboPayeeSelector")[document.getElementById('cboPayeeSelector').selectedIndex].innerHTML;
                var PayeeCodeVal = document.getElementById("ctl00_SummaryPlaceHolder_PayeeCode").value;
                var PerceptionIva = document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIva");
                var PerceptionIIBB = document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIIBB");
                //DEF-1631
                var hdDeductAmtByUser = document.getElementById("ctl00_SummaryPlaceHolder_hdDeductAmtByUser");
                //End of DEF-1631
                var PerceptionIIBBVal = 0;
                var PerceptionIvaVal = 0;
                //var originalAmount = 0;
                var bUseIVATax = false;

                var hdTaxRateClaimDiagnostics = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimDiagnostics");
                var hdTaxRateClaimOther = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimOther");
                var hdTaxRateClaimDisposition = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimDisposition");
                var hdTaxRateClaimLabor = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimLabor");
                var hdTaxRateClaimParts = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimParts");
                var hdTaxRateClaimShipping = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimShipping");
                var hdTaxRateClaimService = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimService");
                var hdTaxRateClaimTrip = document.getElementById("ctl00_SummaryPlaceHolder_hdTaxRateClaimTrip");
                var hdWithholdingRate = document.getElementById("ctl00_SummaryPlaceHolder_hdWithholdingRate");
                var hdTotalWithholdings = document.getElementById("ctl00_SummaryPlaceHolder_hdTotalWithholdings");

                var hdLaborTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdLaborTaxAmt");
                var hdpaymenttocustomertax = document.getElementById("ctl00_SummaryPlaceHolder_hdpaymenttocustomertaxt");
                var hdPartsTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdPartsTaxAmt");
                var hdServiceChargeTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdServiceChargeTaxAmt");
                var hdTripTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdTripTaxAmt");
                var hdShippingTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdShippingTaxAmt");
                var hdDispositionTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdDispositionTaxAmt");
                var hdDiagnosticsTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdDiagnosticsTaxAmt");
                var hdOtherTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdOtherTaxAmt");

                var hdSubTotalAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdSubTotalAmt");
                var hdTotalTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdTotalTaxAmt");
                var hdGrandTotalAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdGrandTotalAmt");

                var bClaimTaxRatesExist = false;

                var ManualTax = document.getElementById("ctl00_SummaryPlaceHolder_txtManualTax");
                var ManualTaxVal = 0;
                if (ManualTax != null) {
                    if (ManualTax.value.length != 0) {
                        ManualTaxVal = parseFloat(setJsFormat(ManualTax.value, decSep));
                    }
                }

                // switch (payeeList.value) {
                switch (PayeeCodeVal) {
                    case "1":
                        var masterIva = document.getElementById("ctl00_SummaryPlaceHolder_MasterCenterIvaResponsible");
                        if (masterIva.value == "Y") {
                            bUseIVATax = true;
                        }
                        break
                    case "2":
                        var svcIva = document.getElementById("ctl00_SummaryPlaceHolder_ServiceCenterIvaResponsible");
                        if (svcIva.value == "Y") {
                            bUseIVATax = true;
                        }
                        break
                    case "3":
                        var loanerIva = document.getElementById("ctl00_SummaryPlaceHolder_LoanerCenterIvaResponsible");
                        if (loanerIva.value == "Y") {
                            bUseIVATax = true;
                        }
                        break;
                    default:
                        bUseIVATax = false;
                }

                if (hdTaxRateClaimDiagnostics.value > 0 || hdTaxRateClaimOther.value > 0 || hdTaxRateClaimDisposition.value > 0 ||
                    hdTaxRateClaimLabor.value > 0 || hdTaxRateClaimParts.value > 0 || hdTaxRateClaimShipping.value > 0 ||
                    hdTaxRateClaimService.value > 0 || hdTaxRateClaimTrip.value > 0) {
                    bClaimTaxRatesExist = true;
                }

                var assurantAmt, alreadyPaidAmt, taxAmt;
                var totalAmt = 0;
                var taxRate = 0;
                var deductibleTaxRate = 0;
                if (bUseIVATax) {
                    var taxRateText = document.getElementById("ctl00_SummaryPlaceHolder_TaxRate");
                    if (taxRateText.value.length != 0) {
                        taxRate = parseFloat(setJsFormat(taxRateText.value, decSep));
                    }

                    var deductibleTaxRateText = document.getElementById("ctl00_SummaryPlaceHolder_DeductibleTaxRate");
                    if (deductibleTaxRateText.value.length != 0) {
                        deductibleTaxRate = parseFloat(setJsFormat(deductibleTaxRateText.value, decSep));
                    }
                }

                var totalDeductibleAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtDeductible").value, decSep));
                var consumerPays = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtConsumerPays").value, decSep));
                var remainingDeductibleAmount = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_RemainingDeductible").value, decSep));
                var PayDeductible = document.getElementById("ctl00_SummaryPlaceHolder_PayDeductible").value;
                var invMethod = document.getElementById("ctl00_SummaryPlaceHolder_InvoiceMethod");
                var PayDeductible = document.getElementById("ctl00_SummaryPlaceHolder_PayDeductible").value;

                var deductibleAmount = 0;
                var deductibleTax = 0;
                var deductiblePaymentByAssurant = 0;
                var remMyDeductible = 0;
                var remDed = 0;
                var remDedTax = 0;

                if (invMethod.value == "1") {

                    var labor = document.getElementById("ctl00_SummaryPlaceHolder_txtLabor");
                    var parts = document.getElementById("ctl00_SummaryPlaceHolder_txtParts");
                    var svcCharge = document.getElementById("ctl00_SummaryPlaceHolder_txtServiceCharge");
                    var tripAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtTripAmt");
                    var shippingAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtShipping");
                    var otherAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtOtherAmt");
                    var DispositionAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtDisposition");
                    var DiagnosticsAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtDiagnostics");
                    var withholdingAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtWithholdingAmt");
                    var laborVal = 0;
                    var partsVal = 0;
                    var svcChargeVal = 0;
                    var tripAmtVal = 0;
                    var shippingAmtVal = 0;
                    var otherAmtVal = 0;
                    var DispositionAmtVal = 0;
                    var DiagnosticsAmtVal = 0;
                    var WithholdingRateVal = 0;

                    var laborTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtLaborTax");
                    var partsTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtPartsTax");
                    var svcChargeTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtServiceChargeTax");
                    var tripTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtTripAmtTax");
                    var shippingTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtShippingTax");
                    var otherTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtOtherTax");
                    var DispositionTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtDispositionTax");
                    var DiagnosticsTaxAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtDiagnosticsTax");
                    var laborTaxAmtVal = 0;
                    var partsTaxAmtVal = 0;
                    var svcChargeTaxAmtVal = 0;
                    var tripTaxAmtVal = 0;
                    var shippingTaxAmtVal = 0;
                    var otherTaxAmtVal = 0;
                    var DispositionTaxAmtVal = 0;
                    var DiagnosticsTaxAmtVal = 0;

                    if (document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIIBB") != null) {
                        if (PerceptionIIBB.value.length != 0) {
                            PerceptionIIBBVal = parseFloat(setJsFormat(PerceptionIIBB.value, decSep));
                        }
                    }
                    if (document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIva") != null) {
                        if (PerceptionIva.value.length != 0) {
                            PerceptionIvaVal = parseFloat(setJsFormat(PerceptionIva.value, decSep));
                        }
                    }
                    if (labor.value.length != 0) {
                        laborVal = parseFloat(setJsFormat(labor.value, decSep));
                        hdLaborAmt.value = convertNumberToCulture(laborVal, decSep, groupSep);
                    }
                    if (parts.value.length != 0) {
                        partsVal = parseFloat(setJsFormat(parts.value, decSep));
                        hdPartsAmt.value = convertNumberToCulture(partsVal, decSep, groupSep);
                    }
                    if (svcCharge.value.length != 0) {
                        svcChargeVal = parseFloat(setJsFormat(svcCharge.value, decSep));
                        hdServiceChargeAmt.value = convertNumberToCulture(svcChargeVal, decSep, groupSep);
                    }
                    if (tripAmt.value.length != 0) {
                        tripAmtVal = parseFloat(setJsFormat(tripAmt.value, decSep));
                        hdTripAmt.value = convertNumberToCulture(tripAmtVal, decSep, groupSep);
                    }
                    if (shippingAmt.value.length != 0) {
                        shippingAmtVal = parseFloat(setJsFormat(shippingAmt.value, decSep));
                        hdShippingAmt.value = convertNumberToCulture(shippingAmtVal, decSep, groupSep);
                    }
                    if (otherAmt.value.length != 0) {
                        otherAmtVal = parseFloat(setJsFormat(otherAmt.value, decSep));
                        hdOtherAmt.value = convertNumberToCulture(otherAmtVal, decSep, groupSep);
                    }
                    if (DispositionAmt.value.length != 0) {
                        DispositionAmtVal = parseFloat(setJsFormat(DispositionAmt.value, decSep));
                        hdDispositionAmt.value = convertNumberToCulture(DispositionAmtVal, decSep, groupSep);
                    }
                    if (DiagnosticsAmt.value.length != 0) {
                        DiagnosticsAmtVal = parseFloat(setJsFormat(DiagnosticsAmt.value, decSep));
                        hdDiagnosticsAmt.value = convertNumberToCulture(DiagnosticsAmtVal, decSep, groupSep);
                    }
                    if (laborTaxAmt.value.length != 0) {
                        laborTaxAmtVal = parseFloat(setJsFormat(laborTaxAmt.value, decSep));
                        laborTaxAmt.value = convertNumberToCulture(laborTaxAmtVal, decSep, groupSep);
                    }
                    if (partsTaxAmt.value.length != 0) {
                        partsTaxAmtVal = parseFloat(setJsFormat(partsTaxAmt.value, decSep));
                        partsTaxAmt.value = convertNumberToCulture(partsTaxAmtVal, decSep, groupSep);
                    }
                    if (svcChargeTaxAmt.value.length != 0) {
                        svcChargeTaxAmtVal = parseFloat(setJsFormat(svcChargeTaxAmt.value, decSep));
                        svcChargeTaxAmt.value = convertNumberToCulture(svcChargeTaxAmtVal, decSep, groupSep);
                    }
                    if (tripTaxAmt.value.length != 0) {
                        tripTaxAmtVal = parseFloat(setJsFormat(tripTaxAmt.value, decSep));
                        tripTaxAmt.value = convertNumberToCulture(tripTaxAmtVal, decSep, groupSep);
                    }
                    if (shippingTaxAmt.value.length != 0) {
                        shippingTaxAmtVal = parseFloat(setJsFormat(shippingTaxAmt.value, decSep));
                        shippingTaxAmt.value = convertNumberToCulture(shippingTaxAmtVal, decSep, groupSep);
                    }
                    if (otherTaxAmt.value.length != 0) {
                        otherTaxAmtVal = parseFloat(setJsFormat(otherTaxAmt.value, decSep));
                        otherTaxAmt.value = convertNumberToCulture(otherTaxAmtVal, decSep, groupSep);
                    }
                    if (DispositionTaxAmt.value.length != 0) {
                        DispositionTaxAmtVal = parseFloat(setJsFormat(DispositionTaxAmt.value, decSep));
                        DispositionTaxAmt.value = convertNumberToCulture(DispositionTaxAmtVal, decSep, groupSep);
                    }
                    if (DiagnosticsTaxAmt.value.length != 0) {
                        DiagnosticsTaxAmtVal = parseFloat(setJsFormat(DiagnosticsTaxAmt.value, decSep));
                        DiagnosticsTaxAmt.value = convertNumberToCulture(DiagnosticsTaxAmtVal, decSep, groupSep);
                    }

                    alreadyPaidAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdAlreadyPaid").value, decSep));
                    var salvageAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtSalvageAmt").value, decSep));
                    var existingSalvageAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdSalvageAmt").value, decSep));

                    var subTotal = laborVal + partsVal + svcChargeVal + tripAmtVal + otherAmtVal + shippingAmtVal + DiagnosticsAmtVal + DispositionAmtVal;
                    var totalClaimTaxAmountAmtVal = laborTaxAmtVal + partsTaxAmtVal + svcChargeTaxAmtVal + tripTaxAmtVal + otherTaxAmtVal + shippingTaxAmtVal + DispositionTaxAmtVal + DiagnosticsTaxAmtVal;

                    //if (alreadyPaidAmt == 0 && salvageAmt <= subTotal)
                    //    subTotal = subTotal - salvageAmt;

                    //if (existingSalvageAmt == 0 && salvageAmt <= subTotal)  //commented by ravi on 30th dec 2011
                    subTotal = subTotal - salvageAmt;

                    hdSubTotalAmt.value = subTotal;

                    if (bUseIVATax) {
                        taxAmt = ((subTotal * taxRate) / 100);
                    };

                    if (bClaimTaxRatesExist) {
                        //debugger;
                        // Labor Tax                        
                        if (hdTaxRateClaimLabor != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtLabor" && hdTaxRateClaimLabor.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - laborTaxAmtVal;
                                var hdComputeMethodClaimLabor = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimLabor");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimLabor.value, decSep));
                                laborTaxAmtVal = computeTaxAmtByComputeMethod(laborVal, taxRateVal, hdComputeMethodClaimLabor.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + laborTaxAmtVal;
                                if (laborTaxAmtVal != 0) {
                                    laborTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(laborTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtLaborTax").innerText = laborTaxAmtVal;
                                hdLaborTaxAmt.value = laborTaxAmtVal;
                            }
                        }

                        // Parts Tax
                        if (hdTaxRateClaimParts != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtParts" && hdTaxRateClaimParts.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - partsTaxAmtVal;
                                var hdComputeMethodClaimParts = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimParts");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimParts.value, decSep));
                                partsTaxAmtVal = computeTaxAmtByComputeMethod(partsVal, taxRateVal, hdComputeMethodClaimParts.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + partsTaxAmtVal;
                                if (partsTaxAmtVal != 0) {
                                    partsTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(partsTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtPartsTax").innerText = partsTaxAmtVal;
                                hdPartsTaxAmt.value = partsTaxAmtVal;
                            }
                        }

                        // Service Tax
                        if (hdTaxRateClaimService != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtServiceCharge" && hdTaxRateClaimService.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - svcChargeTaxAmtVal;
                                var hdComputeMethodClaimService = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimService");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimService.value, decSep));
                                svcChargeTaxAmtVal = computeTaxAmtByComputeMethod(svcChargeVal, taxRateVal, hdComputeMethodClaimService.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + svcChargeTaxAmtVal;
                                if (svcChargeTaxAmtVal != 0) {
                                    svcChargeTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(svcChargeTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtServiceChargeTax").innerText = svcChargeTaxAmtVal;
                                hdServiceChargeTaxAmt.value = svcChargeTaxAmtVal;
                            }
                        }

                        // Trip Tax
                        if (hdTaxRateClaimTrip != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtTripAmt" && hdTaxRateClaimTrip.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - tripTaxAmtVal;
                                var hdComputeMethodClaimTrip = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimTrip");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimTrip.value, decSep));
                                tripTaxAmtVal = computeTaxAmtByComputeMethod(tripAmtVal, taxRateVal, hdComputeMethodClaimTrip.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + tripTaxAmtVal;
                                if (tripTaxAmtVal != 0) {
                                    tripTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(tripTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtTripAmtTax").innerText = tripTaxAmtVal;
                                hdTripTaxAmt.value = tripTaxAmtVal;
                            }
                        }

                        // Shipping Tax
                        if (hdTaxRateClaimShipping != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtShipping" && hdTaxRateClaimShipping.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - shippingTaxAmtVal;
                                var hdComputeMethodClaimShipping = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimShipping");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimShipping.value, decSep));
                                shippingTaxAmtVal = computeTaxAmtByComputeMethod(shippingAmtVal, taxRateVal, hdComputeMethodClaimShipping.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + shippingTaxAmtVal;
                                if (shippingTaxAmtVal != 0) {
                                    shippingTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(shippingTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtShippingTax").innerText = shippingTaxAmtVal;
                                hdShippingTaxAmt.value = shippingTaxAmtVal;
                            }
                        }

                        // Diagnostics Tax
                        if (hdTaxRateClaimDiagnostics != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtDiagnostics" && hdTaxRateClaimDiagnostics.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - DiagnosticsTaxAmtVal;
                                var hdComputeMethodClaimDiagnostics = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimDiagnostics");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimDiagnostics.value, decSep));
                                DiagnosticsTaxAmtVal = computeTaxAmtByComputeMethod(DiagnosticsAmtVal, taxRateVal, hdComputeMethodClaimDiagnostics.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + DiagnosticsTaxAmtVal;
                                if (DiagnosticsTaxAmtVal != 0) {
                                    DiagnosticsTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(DiagnosticsTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtDiagnosticsTax").innerText = DiagnosticsTaxAmtVal;
                                hdDiagnosticsTaxAmt.value = DiagnosticsTaxAmtVal;
                            }
                        }

                        // Disposition Tax
                        if (hdTaxRateClaimDisposition != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtDisposition" && hdTaxRateClaimDisposition.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - DispositionTaxAmtVal;
                                var hdComputeMethodClaimDisposition = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimDisposition");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimDisposition.value, decSep));
                                DispositionTaxAmtVal = computeTaxAmtByComputeMethod(DispositionAmtVal, taxRateVal, hdComputeMethodClaimDisposition.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + DispositionTaxAmtVal;
                                if (DispositionTaxAmtVal != 0) {
                                    DispositionTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(DispositionTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtDispositionTax").innerText = DispositionTaxAmtVal;
                                hdDispositionTaxAmt.value = DispositionTaxAmtVal;
                            }
                        }

                        // Other Tax
                        if (hdTaxRateClaimOther != 0) {
                            if (obj.id == "ctl00_SummaryPlaceHolder_txtOtherAmt" && hdTaxRateClaimOther.value > 0) {
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - otherTaxAmtVal;
                                var hdComputeMethodClaimOther = document.getElementById("ctl00_SummaryPlaceHolder_hdComputeMethodClaimOther");
                                var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimOther.value, decSep));
                                otherTaxAmtVal = computeTaxAmtByComputeMethod(otherAmtVal, taxRateVal, hdComputeMethodClaimOther.value);
                                totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + otherTaxAmtVal;
                                if (otherTaxAmtVal != 0) {
                                    otherTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(otherTaxAmtVal, 2).toString()), decSep, groupSep);
                                }
                                document.getElementById("ctl00_SummaryPlaceHolder_txtOtherTax").innerText = otherTaxAmtVal;
                                hdOtherTaxAmt.value = otherTaxAmtVal;
                            }
                        }

                        
                            var txttotaltaxamt = document.getElementById("ctl00_SummaryPlaceHolder_txtTotalTaxAmount");
                            if (txttotaltaxamt != null) {
                                document.getElementById("ctl00_SummaryPlaceHolder_txtTotalTaxAmount").innerText = convertNumberToCulture(FormatToDecimal(round_num(totalClaimTaxAmountAmtVal, 2).toString()), decSep, groupSep);
                                hdTotalTaxAmt.value = convertNumberToCulture(FormatToDecimal(round_num(totalClaimTaxAmountAmtVal, 2).toString()), decSep, groupSep);
                            }

                       
                    };

                    WithholdingRateVal = parseFloat(setJsFormat(hdWithholdingRate.value, decSep));

                    var hdTotalWithholdingsVal = parseFloat(setJsFormat(hdTotalWithholdings.value, decSep));

                    if (WithholdingRateVal != 0) {
                        var totalAmountSubToWithholdingVal = 0;

                        var hdApplyWithholdingFlagClaimLabor = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimLabor");
                        if (hdApplyWithholdingFlagClaimLabor.value == "Y") {
                            totalAmountSubToWithholdingVal += laborVal;
                        }

                        var hdApplyWithholdingFlagClaimParts = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimParts");
                        if (hdApplyWithholdingFlagClaimParts.value == "Y") {
                            totalAmountSubToWithholdingVal += partsVal;
                        }

                        var hdApplyWithholdingFlagClaimService = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimService");
                        if (hdApplyWithholdingFlagClaimService.value == "Y") {
                            totalAmountSubToWithholdingVal += svcChargeVal;

                        }

                        var hdApplyWithholdingFlagClaimTrip = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimTrip");
                        if (hdApplyWithholdingFlagClaimTrip.value == "Y") {
                            totalAmountSubToWithholdingVal += tripAmtVal;
                        }

                        var hdApplyWithholdingFlagClaimShipping = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimShipping");
                        if (hdApplyWithholdingFlagClaimShipping.value == "Y") {
                            totalAmountSubToWithholdingVal += shippingAmtVal;
                        }

                        var hdApplyWithholdingFlagClaimDiagnostics = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimDiagnostics");
                        if (hdApplyWithholdingFlagClaimDiagnostics.value == "Y") {
                            totalAmountSubToWithholdingVal += DiagnosticsAmtVal;
                        }

                        var hdApplyWithholdingFlagClaimDisposition = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimDisposition");
                        if (hdApplyWithholdingFlagClaimDisposition.value == "Y") {
                            totalAmountSubToWithholdingVal += DispositionAmtVal;
                        }

                        var hdApplyWithholdingFlagClaimOther = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimOther");
                        if (hdApplyWithholdingFlagClaimOther.value == "Y") {
                            totalAmountSubToWithholdingVal += otherAmtVal;
                        }

                        hdTotalWithholdingsVal = computeTaxAmtByComputeMethod(totalAmountSubToWithholdingVal, WithholdingRateVal, "N");
                        document.getElementById("ctl00_SummaryPlaceHolder_txtTotalWithholdingAmount").innerText = convertNumberToCulture(hdTotalWithholdingsVal, decSep, groupSep);

                        hdTotalWithholdings.value = hdTotalWithholdingsVal;
                    }

                    //DEF-1631
                    if (PayDeductible == "Y_AUTH_LESS_DEDUCT") {
                        remDed = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleAmount").value, decSep));
                        remDedTax = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleTaxAmount").value, decSep));
                        if (hdDeductAmtByUser.value == "" || hdDeductAmtByUser.value != remDed) {
                            if (remDed > 0) {
                                deductiblePaymentByAssurant = remDed;
                            }
                            else {
                                deductiblePaymentByAssurant = remainingDeductibleAmount;
                            }
                            deductibleAmount = round_num(((deductiblePaymentByAssurant) * 100) / (100 + deductibleTaxRate), 2);
                            deductibleTax = round_num(deductiblePaymentByAssurant - deductibleAmount, 2);

                        }
                        else {

                            deductibleAmount = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleAmount").value, decSep));
                            deductibleTax = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleTaxAmount").value, decSep));
                            //remMyDeductible = deductibleAmount + deductibleTax;
                            deductiblePaymentByAssurant = deductibleAmount + deductibleTax;
                        }
                        remMyDeductible = deductibleAmount + deductibleTax;
                    }
                    //End of DEF-1631

                    if (bUseIVATax) {
                        totalAmt = subTotal + taxAmt + deductibleAmount + deductibleTax;
                    }
                    else {
                        totalAmt = subTotal + totalClaimTaxAmountAmtVal + deductibleAmount + deductibleTax;
                        hdTotal.value = totalAmt;
                    };


                    if (AuthDetailReq == 'ADR') {
                        //alert("Deductible:" + totalDeductibleAmt);
                        //alert("totalAmt before Deductible:" + totalAmt);
                        if (totalAmt > 0) {
                            totalAmt = totalAmt - totalDeductibleAmt;
                        }
                    }

                    //originalAmount = totalAmt
                    //totalAmt = round_num((subTotal + taxAmt + PerceptionIIBBVal + PerceptionIvaVal), 2);

                    hdPerceptionTax.value = convertNumberToCulture((PerceptionIvaVal + PerceptionIIBBVal), decSep, groupSep);

                    document.getElementById("ctl00_SummaryPlaceHolder_txtSubTotal").innerText = convertNumberToCulture(round_num(subTotal, 2), decSep, groupSep);

                    if (document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleTaxAmount") != null) {
                        document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleTaxAmount").innerText = convertNumberToCulture(deductibleTax, decSep, groupSep);
                    }
                    if (document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleAmount") != null) {
                        document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleAmount").innerText = convertNumberToCulture(deductibleAmount, decSep, groupSep);
                        //DEF-1631
                        document.getElementById("ctl00_SummaryPlaceHolder_hdDeductAmtByUser").innerText = convertNumberToCulture(deductibleAmount, decSep, groupSep);
                        //End of DEF-1631
                    }

                    if (document.getElementById("ctl00_SummaryPlaceHolder_txtIvaTax") != null) {
                        document.getElementById("ctl00_SummaryPlaceHolder_txtIvaTax").innerText = convertNumberToCulture(round_num(taxAmt, 2), decSep, groupSep);
                    }
                    if (document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIIBB") != null) {
                        document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIva").innerText = convertNumberToCulture(FormatToDecimal(PerceptionIvaVal.toString()), decSep, groupSep);
                        document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIIBB").innerText = convertNumberToCulture(FormatToDecimal(PerceptionIIBBVal.toString()), decSep, groupSep);
                    }
                    document.getElementById("ctl00_SummaryPlaceHolder_txtTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num(totalAmt, 2).toString()), decSep, groupSep);
                    //DEF-674
                    //hdTotal.value = convertNumberToCulture(totalAmt, decSep, groupSep);
                    hdTotal.value = convertNumberToCulture(FormatToDecimal(round_num(totalAmt, 2).toString()), decSep, groupSep);

                    assurantAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdAssurantPays").value, decSep));

                    //REQ-791 start
                    if (obj.id == "ctl00_SummaryPlaceHolder_txtSalvageAmt") {
                        assurantAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdAssurantPays").value, decSep)) + parseFloat(setJsFormat(hdSalvageAmt.value, decSep)) - parseFloat(setJsFormat(obj.value, decSep));
                        if (assurantAmt < 0) {
                            alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                             obj.value = convertNumberToCulture(FormatToDecimal(round_num(hdSalvageAmt.value, 2).toString()), decSep, groupSep);
                             doAmtCalc(obj);
                             return false;
                         }
                         document.getElementById("ctl00_SummaryPlaceHolder_txtAssurantPays").innerText = convertNumberToCulture(FormatToDecimal(round_num(assurantAmt, 2).toString()), decSep, groupSep);
                         //document.getElementById("hdAssurantPays").value = convertNumberToCulture(FormatToDecimal(round_num(assurantAmt, 2).toString()), decSep, groupSep);
                     }
                     //var salvageAmt = parseFloat(setJsFormat(document.getElementById("txtSalvageAmt").value, decSep));
                     //REQ-791 end

                     //REQ-786 start
                     if (obj.id == "ctl00_SummaryPlaceHolder_txtDeductible") {
                         assurantAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdAssurantPays").value, decSep)) + parseFloat(setJsFormat(document.getElementById("hdDeductibleAmt").value, decSep)) - parseFloat(setJsFormat(obj.value, decSep));
                         document.getElementById("ctl00_SummaryPlaceHolder_txtAssurantPays").innerText = convertNumberToCulture(FormatToDecimal(round_num(assurantAmt, 2).toString()), decSep, groupSep);

                     }
                     //REQ-786 end

                     //alreadyPaidAmt = parseFloat(setJsFormat(document.getElementById("hdAlreadyPaid").value, decSep));
                     //DEF-2253
                     if ((obj.id == "ctl00_SummaryPlaceHolder_txtSalvageAmt" || obj.id == "ctl00_SummaryPlaceHolder_txtDeductible") && remainingDeductibleAmount != 0)
                         document.getElementById("ctl00_SummaryPlaceHolder_txtRemainingAmt").innerText = convertNumberToCulture(FormatToDecimal1(round_num((assurantAmt + deductiblePaymentByAssurant + parseFloat(setJsFormat(hdDeductibleAmt.value, decSep)) - alreadyPaidAmt - totalAmt - remMyDeductible), 2).toString()), decSep, groupSep);

                     else if (obj.id == "ctl00_SummaryPlaceHolder_txtSalvageAmt" || obj.id == "ctl00_SummaryPlaceHolder_txtDeductible")
                         document.getElementById("ctl00_SummaryPlaceHolder_txtRemainingAmt").innerText = convertNumberToCulture(FormatToDecimal1(round_num((assurantAmt + deductiblePaymentByAssurant + parseFloat(setJsFormat(hdDeductibleAmt.value, decSep)) - alreadyPaidAmt - totalAmt), 2).toString()), decSep, groupSep);
                     //End of DEF-2253
                     //DEF-1631
                     else if (remainingDeductibleAmount == 0)
                         document.getElementById("ctl00_SummaryPlaceHolder_txtRemainingAmt").innerText = convertNumberToCulture(FormatToDecimal1(round_num((assurantAmt + deductiblePaymentByAssurant + parseFloat(setJsFormat(hdSalvageAmt.value, decSep)) + parseFloat(setJsFormat(hdDeductibleAmt.value, decSep)) - alreadyPaidAmt - totalAmt - salvageAmt), 2).toString()), decSep, groupSep);
                     else
                         document.getElementById("ctl00_SummaryPlaceHolder_txtRemainingAmt").innerText = convertNumberToCulture(FormatToDecimal1(round_num((assurantAmt + deductiblePaymentByAssurant + parseFloat(setJsFormat(hdSalvageAmt.value, decSep)) + parseFloat(setJsFormat(hdDeductibleAmt.value, decSep)) - alreadyPaidAmt - totalAmt - salvageAmt - remMyDeductible), 2).toString()), decSep, groupSep);
                     //End of DEF-1631

                     if (document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal") != null) {
                         if (WithholdingRateVal != 0) {
                             document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep)) + hdTotalWithholdingsVal), 2).toString()), decSep, groupSep);
                             hdGrandTotalAmt.value = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep)) + hdTotalWithholdingsVal), 2).toString()), decSep, groupSep);
                         }
                         else {
                             document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep))), 2).toString()), decSep, groupSep);
                             hdGrandTotalAmt.value = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep))), 2).toString()), decSep, groupSep);
                         }


                     }
                 }
                 else {
                     //alert('ON 2');
                     //debugger;
                     if (document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIva") != null) {
                         if (PerceptionIva.value.length != 0) {
                             PerceptionIvaVal = parseFloat(setJsFormat(PerceptionIva.value, decSep));
                         }
                     }

                     if (document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIIBB") != null) {
                         if (PerceptionIIBB.value.length != 0) {
                             PerceptionIIBBVal = parseFloat(setJsFormat(PerceptionIIBB.value, decSep));
                         }
                     }
                     //var totalAmtVal = originalAmount;
                     var totalAmtVal = document.getElementById("ctl00_SummaryPlaceHolder_txtTotal");
                     totalAmt = 0;
                     //if (totalAmtVal.value > 0) {
                     if (totalAmtVal.length != 0) {
                         totalAmt = parseFloat(setJsFormat(totalAmtVal.value, decSep));
                     }
                     //}

                     if (totalAmt != 0) {
                         totalAmt = totalAmt + parseFloat(setJsFormat(hdPerceptionTax.value, decSep));
                     }
                     if (totalAmt != originalAmount) {
                         totalAmt = totalAmt - parseFloat(setJsFormat(hdPerceptionTax.value, decSep));
                         originalAmount = totalAmt;
                     }

                     //originalAmount = totalAmt
                     if (totalAmt > 0) {
                         totalAmt = round_num((totalAmt - PerceptionIvaVal - PerceptionIIBBVal), 2);
                     }

                     var preTaxAmt;

                     // debugger;
                     assurantAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtAssurantPays").value, decSep));
                     alreadyPaidAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdAlreadyPaid").value, decSep));

                     if (taxRate == 0) {
                         taxRate = hdTaxRateClaimOther.value;
                     }

                     if (PayDeductible == "Y_AUTH_LESS_DEDUCT") {
                         if (alreadyPaidAmt + totalAmt - assurantAmt > 0) {
                             if (alreadyPaidAmt + totalAmt - assurantAmt > remainingDeductibleAmount) {
                                 deductiblePaymentByAssurant = remainingDeductibleAmount;
                             }
                             else {
                                 deductiblePaymentByAssurant = alreadyPaidAmt + totalAmt - assurantAmt;
                             }
                         }
                         else {
                             deductiblePaymentByAssurant = 0;
                         }
                         deductibleAmount = round_num(((deductiblePaymentByAssurant) * 100) / (100 + parseFloat(deductibleTaxRate)), 2);
                         deductibleTax = round_num(deductiblePaymentByAssurant - deductibleAmount, 2);
                         preTaxAmt = round_num(((totalAmt - deductiblePaymentByAssurant) * 100) / (100 + parseFloat(taxRate)), 2);
                         taxAmt = round_num(totalAmt - deductiblePaymentByAssurant - preTaxAmt, 2);
                     }
                     else {
                         preTaxAmt = round_num((totalAmt * 100) / (100 + parseFloat(taxRate)), 2);
                         taxAmt = round_num(totalAmt - preTaxAmt, 2);
                     }

                     document.getElementById("ctl00_SummaryPlaceHolder_txtOtherTax").innerText = taxAmt;
                     if (!bUseIVATax) {
                         var txttotaltax = document.getElementById("ctl00_SummaryPlaceHolder_txtTotalTaxAmount");

                         if (txttotaltax != null) {
                             document.getElementById("ctl00_SummaryPlaceHolder_txtTotalTaxAmount").innerText = convertNumberToCulture(FormatToDecimal(round_num(taxAmt, 2).toString()), decSep, groupSep);
                         }

                     }
                     hdOtherTaxAmt.value = taxAmt;
                     hdSubTotalAmt.value = preTaxAmt;

                     hdTotalTaxAmt.value = hdOtherTaxAmt.value;

                     hdPerceptionTax.value = convertNumberToCulture((PerceptionIvaVal + PerceptionIIBBVal), decSep, groupSep);
                     if (document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleTaxAmount") != null) {
                         document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleTaxAmount").innerText = convertNumberToCulture(deductibleTax, decSep, groupSep);
                     }
                     if (document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleAmount") != null) {
                         document.getElementById("ctl00_SummaryPlaceHolder_txtDeductibleAmount").innerText = convertNumberToCulture(deductibleAmount, decSep, groupSep);
                     }
                     document.getElementById("ctl00_SummaryPlaceHolder_txtSubTotal").innerText = convertNumberToCulture(preTaxAmt, decSep, groupSep);
                     if (document.getElementById("ctl00_SummaryPlaceHolder_txtIvaTax") != null) {
                         document.getElementById("ctl00_SummaryPlaceHolder_txtIvaTax").innerText = convertNumberToCulture(taxAmt, decSep, groupSep);
                     }
                     if (document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIIBB") != null) {
                         document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIva").innerText = convertNumberToCulture(FormatToDecimal(PerceptionIvaVal.toString()), decSep, groupSep);
                         document.getElementById("ctl00_SummaryPlaceHolder_txtPerceptionIIBB").innerText = convertNumberToCulture(FormatToDecimal(PerceptionIIBBVal.toString()), decSep, groupSep);
                     }
                     document.getElementById("ctl00_SummaryPlaceHolder_txtTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num(totalAmt, 2).toString()), decSep, groupSep);
                     //DEF-674
                     //hdTotal.value = convertNumberToCulture(totalAmt, decSep, groupSep);
                     hdTotal.value = convertNumberToCulture(FormatToDecimal(round_num(totalAmt, 2).toString()), decSep, groupSep);

                     hdOtherAmt.value = convertNumberToCulture(preTaxAmt, decSep, groupSep);
                     document.getElementById("ctl00_SummaryPlaceHolder_txtOtherAmt").innerText = convertNumberToCulture(preTaxAmt, decSep, groupSep);
                     document.getElementById("ctl00_SummaryPlaceHolder_txtOtherDesc").innerText = "NET";
                     //var originalAmount = round_num(totalAmt - parseFloat(setJsFormat(hdPerceptionTax.value, decSep)))

                     //REQ-791 start
                     if (obj.id == "ctl00_SummaryPlaceHolder_txtSalvageAmt") {
                         assurantAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdAssurantPays").value, decSep)) + parseFloat(setJsFormat(document.getElementById("hdSalvageAmt").value, decSep)) - parseFloat(setJsFormat(obj.value, decSep));
                         if (assurantAmt < 0) {
                             alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                            obj.value = convertNumberToCulture(FormatToDecimal(round_num(hdSalvageAmt.value, 2).toString()), decSep, groupSep);
                            doAmtCalc(obj);
                            return false;
                        }
                        document.getElementById("ctl00_SummaryPlaceHolder_txtAssurantPays").innerText = convertNumberToCulture(FormatToDecimal(round_num(assurantAmt, 2).toString()), decSep, groupSep);
                        //document.getElementById("hdAssurantPays").value = convertNumberToCulture(FormatToDecimal(round_num(assurantAmt, 2).toString()), decSep, groupSep);
                    }
                    var salvageAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_txtSalvageAmt").value, decSep));
                    //REQ-791 end

                    //REQ-786 start
                    if (obj.id == "ctl00_SummaryPlaceHolder_txtDeductible") {
                        assurantAmt = parseFloat(setJsFormat(document.getElementById("ctl00_SummaryPlaceHolder_hdAssurantPays").value, decSep)) + parseFloat(setJsFormat(document.getElementById("hdDeductibleAmt").value, decSep)) - parseFloat(setJsFormat(obj.value, decSep));
                        document.getElementById("ctl00_SummaryPlaceHolder_txtAssurantPays").innerText = convertNumberToCulture(FormatToDecimal(round_num(assurantAmt, 2).toString()), decSep, groupSep);

                    }

                    // Withhodling begin
                    var withholdingAmt = document.getElementById("ctl00_SummaryPlaceHolder_txtWithholdingAmt");
                    var WithholdingRateVal = 0;


                    WithholdingRateVal = parseFloat(setJsFormat(hdWithholdingRate.value, decSep));

                    var hdTotalWithholdingsVal = parseFloat(setJsFormat(hdTotalWithholdings.value, decSep));

                    if (WithholdingRateVal != 0) {
                        var totalAmountSubToWithholdingVal = 0;

                        var hdApplyWithholdingFlagClaimOther = document.getElementById("ctl00_SummaryPlaceHolder_hdApplyWithholdingFlagClaimOther");
                        if (hdApplyWithholdingFlagClaimOther.value == "Y") {
                            hdTotalWithholdingsVal = computeTaxAmtByComputeMethod(hdOtherAmt.value, WithholdingRateVal, "N");
                            document.getElementById("ctl00_SummaryPlaceHolder_txtTotalWithholdingAmount").innerText = convertNumberToCulture(hdTotalWithholdingsVal, decSep, groupSep);
                        }
                    }

                    hdTotalWithholdings.value = hdTotalWithholdingsVal;

                    if (document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal") != null) {
                        if (WithholdingRateVal != 0) {
                            document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep)) + hdTotalWithholdingsVal), 2).toString()), decSep, groupSep);
                            hdGrandTotalAmt.value = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep)) + hdTotalWithholdingsVal), 2).toString()), decSep, groupSep);
                        }
                        else {
                            document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep))), 2).toString()), decSep, groupSep);
                            hdGrandTotalAmt.value = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep))), 2).toString()), decSep, groupSep);
                        }
                    }
                    // Withhodling END


                    //REQ-786 end
                    //debugger;
                    //if (obj.id == "txtSalvageAmt" || obj.id == "txtDeductible")
                    var subTotal = round_num(assurantAmt - alreadyPaidAmt - totalAmt, 2);
                    //else
                    //var subTotal = round_num(assurantAmt - alreadyPaidAmt - totalAmt - salvageAmt, 2);

                    if (PayDeductible == "Y_AUTH_LESS_DEDUCT") {
                        subTotal = subTotal + totalDeductibleAmt;
                    }

                    var subTotal = convertNumberToCulture(subTotal, decSep, groupSep);
                    if (subTotal == "-0.00") {
                        subTotal = "0.00";
                    }

                    document.getElementById("ctl00_SummaryPlaceHolder_txtRemainingAmt").innerText = subTotal;

                    // repated code.
                    // if (document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal") != null) {
                    //     document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num((totalAmt + ManualTaxVal + parseFloat(setJsFormat(hdPerceptionTax.value, decSep))), 2).toString()), decSep, groupSep);
                    // }
                }
            }
            else {
                alert(obj.name + obj.value + '<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                obj.value = convertNumberToCulture(FormatToDecimal(round_num(0, 2).toString()), decSep, groupSep);
                doAmtCalc(obj);

            }

        }
        function ValidCultureValue(val) {
            var US = /^(((\d{1,3})(,\d{3})*)|(\d+))(\.\d{1,2})?$/;
            var EU = /^(((\d{1,3})(\.\d{3})*)|(\d+))(,\d{1,2})?$/
            var ReturnValue = false

            var validNum = /^(((\d{1,3})([\.,]\d{3})*)|(\d+))([\.,]\d{1,2})?$/;

            if (US.test(val)) {
                ReturnValue = true;
                //alert('1 ' + ReturnValue);
            } else {
                if (EU.test(val)) {
                    //alert('2 ' + ReturnValue);
                    ReturnValue = true;
                }
            }
            return ReturnValue;
        }

        function rcDDOnChange(dropdown) {

            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';

            var hdDeductibleAmt = parseFloat(setJsFormat(document.getElementById("hdDeductibleAmt").value, decSep))
            var consumerPays = parseFloat(setJsFormat(document.getElementById("txtConsumerPays").value, decSep))
            var deductible = parseFloat(setJsFormat(document.getElementById("txtDeductible").value, decSep))

            var hdDeliveryFeeOnly = document.getElementById("ctl00_SummaryPlaceHolder_hdDeliveryFeeOnly");


            var myindex = dropdown.selectedIndex;
            var SelValue = dropdown.options[myindex].value;

            var buttonID = '<%= btnReplacement_WRITE.ClientID %>';
            var button = document.getElementById(buttonID);

            if (SelValue == hdDeliveryFeeOnly.value) {
                document.getElementById("ctl00_SummaryPlaceHolder_txtDeductible").innerText = convertNumberToCulture(FormatToDecimal(round_num(0, 2).toString()), decSep, groupSep);
                document.getElementById("ctl00_SummaryPlaceHolder_txtConsumerPays").innerText = convertNumberToCulture(FormatToDecimal(round_num((consumerPays - hdDeductibleAmt), 2).toString()), decSep, groupSep);
                doAmtCalc(document.getElementById("ctl00_SummaryPlaceHolder_txtDeductible"));
                if (button) { button.style.display = 'none'; }

            }
            else {
                if (deductible == 0) {
                    document.getElementById("ctl00_SummaryPlaceHolder_txtDeductible").innerText = convertNumberToCulture(FormatToDecimal(round_num(hdDeductibleAmt, 2).toString()), decSep, groupSep);
                    document.getElementById("ctl00_SummaryPlaceHolder_txtConsumerPays").innerText = convertNumberToCulture(FormatToDecimal(round_num((consumerPays + hdDeductibleAmt), 2).toString()), decSep, groupSep);
                    doAmtCalc(document.getElementById("ctl00_SummaryPlaceHolder_txtDeductible"));
                }

                if (button) { button.style.display = 'inline'; }
            }
            return true;
        }

        function UpdateGrandTotalAmount() {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';

            var obj = document.getElementById("ctl00_SummaryPlaceHolder_txtPaymenttoCustomer");
            var paymentToCustomer = obj.value;
            var hdpaymenttocustomer = document.getElementById("ctl00_SummaryPlaceHolder_hdpaymenttocustomer");
            hdpaymenttocustomer.value = paymentToCustomer;

            if (paymentToCustomer < 0) {
                alert(obj.name + obj.value + '<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                return false;
            }

            //Net Amount
            //document.getElementById("ctl00_SummaryPlaceHolder_txtOtherAmt").innerText = convertNumberToCulture(FormatToDecimal(round_num(paymentToCustomer, 2).toString()), decSep, groupSep);

            //subTotal
            document.getElementById("ctl00_SummaryPlaceHolder_txtSubTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num(paymentToCustomer, 2).toString()), decSep, groupSep);
            var hdSubTotalAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdSubTotalAmt");
            hdSubTotalAmt.value = paymentToCustomer;

            //Total Tax Amount
            document.getElementById("ctl00_SummaryPlaceHolder_txtTotalTaxAmount").innerText = 0;

            //Total
            document.getElementById("ctl00_SummaryPlaceHolder_txtTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num(paymentToCustomer, 2).toString()), decSep, groupSep);
            var hdTotal = document.getElementById("ctl00_SummaryPlaceHolder_hdTotal");
            hdTotal.value = paymentToCustomer;
            //alert(hdTotal.value);

            //Grand Total
            document.getElementById("ctl00_SummaryPlaceHolder_txtGrandTotal").innerText = convertNumberToCulture(FormatToDecimal(round_num(paymentToCustomer, 2).toString()), decSep, groupSep);
            var hdGrandTotalAmt = document.getElementById("ctl00_SummaryPlaceHolder_hdGrandTotalAmt");
            hdGrandTotalAmt.value = paymentToCustomer;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>


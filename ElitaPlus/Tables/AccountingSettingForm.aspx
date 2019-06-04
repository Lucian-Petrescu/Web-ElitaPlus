<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountingSettingForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingSettingForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="scroller" style="overflow: auto; width: 100%; height: 500px" align="center">
                    <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                        <table style="vertical-align: middle" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr style="vertical-align: middle" valign="middle">
                                <td style="vertical-align: middle" valign="middle">
                                    <table id="Table1" style="width: 100%;" cellspacing="1" cellpadding="0" width="100%"
                                         border="0">
                                        <tr>
                                            <td class="LABELCOLUMN">
                                                *
                                                <asp:Label ID="lblTargetName" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="moAccountNameTEXT" runat="server" Width="360px" ReadOnly="True"></asp:TextBox>
                                                <asp:DropDownList ID="ddlTargetist" runat="server" Width="360px" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="branchName_tr">
                                            <td class="LABELCOLUMN">
                                                *
                                                <asp:Label ID="moBranchNameLABEL" runat="server">Branch_Name</asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="moBranchNameTEXT" runat="server" Width="360px" ReadOnly="True"></asp:TextBox>
                                                <asp:DropDownList ID="moBranchNameDROP" runat="server" Width="360px" AutoPostBack="true"
                                                    Enabled="false">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="TABS">
                                                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Receivable" >
                                                        <ContentTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAccountCodeLABEL" runat="server">Account_Code</asp:Label>
                                                                    </td>
                                                                    <td width="20%">
                                                                        <asp:TextBox ID="moAccountCodeTEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moCompanyLABEL" runat="server">Accounting_Company</asp:Label>
                                                                    </td>
                                                                    <td width="20%">
                                                                        <asp:DropDownList ID="cboCompany" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtCompany" ReadOnly="true" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moConversionCodeControlLABEL" runat="server">Conversion_Code_Control</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboConversionCodeControl" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moDefaultCurrencyCodeLABEL" runat="server">Default_Currency_Code</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moDefaultCurrencyCodeTEXT" runat="server" Width="170px" MaxLength="3"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT1LABEL" runat="server">Acct_Analysis_TCode_1</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal1" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT1TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT2LABEL" runat="server">Acct_Analysis_TCode_2</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal2" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT2TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT3LABEL" runat="server">Acct_Analysis_TCode_3</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal3" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT3TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT4LABEL" runat="server">Acct_Analysis_TCode_4</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal4" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT4TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT5LABEL" runat="server">Acct_Analysis_TCode_5</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal5" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT5TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT6LABEL" runat="server">Acct_Analysis_TCode_6</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal6" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT6TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT7LABEL" runat="server">Acct_Analysis_TCode_7</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal7" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT7TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT8LABEL" runat="server">Acct_Analysis_TCode_8</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal8" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT8TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT9LABEL" runat="server">Acct_Analysis_TCode_9</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal9" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT9TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT10LABEL" runat="server">Acct_Analysis_TCode_10</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal10" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT10TEXT" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moReportConversionControlLABEL" runat="server">Report_Conversion_Control</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboReportConversionControl" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moBalanceTypeLABEL" runat="server">Balance_Type</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moBalanceTypeTEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAddressStatusLABEL" runat="server">Address_Status</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAddressStatus" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAddressLookupCodeLABEL" runat="server">Address_Lookup_Code</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAddressLookupCodeTEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSupplierStatusLABEL" runat="server">Supplier_Status</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboSupplierStatus" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSupplierLookUpCodeLABEL" runat="server">Supplier_Lookup_Code</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSupplierLookUpCodeTEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal1LABEL" runat="server">Analysis_Code_1</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal1TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal2LABEL" runat="server">Analysis_Code_2</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal2TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal3LABEL" runat="server">Analysis_Code_3</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal3TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal4LABEL" runat="server">Analysis_Code_4</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal4TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal5LABEL" runat="server">Analysis_Code_5</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal5TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal6LABEL" runat="server">Analysis_Code_6</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal6TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal7LABEL" runat="server">Analysis_Code_7</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal7TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal8LABEL" runat="server">Analysis_Code_8</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal8TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr valign="middle">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal9LABEL" runat="server">Analysis_Code_9</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal9TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal10LABEL" runat="server">Analysis_Code_10</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal10TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel01">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moDataAccessGroupCodeLABEL" runat="server">Data_Access_Group_Code</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="moDataAccessGroupCodeTEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppressReevaluationLABEL" runat="server">Suppress_Reevaluation</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="moSuppressReevaluationTEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel02">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moPayAsPaidAccountTypeLABEL" runat="server">Pay_As_Paid_Account_Type</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="cboPayAsPaidAccountType" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moPaymentMethodLABEL" runat="server">Payment_Method</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="cboPaymentMethod" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel03">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moPaymentTermsLABEL" runat="server">PAYMENT_TERMS</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboPaymentTerms" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAddressSequenceNumberLABEL" runat="server">Address_Sequence_Number</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAddressSequenceNumberTEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel05">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal1LABEL" runat="server">Suppl_Analysis_Code_1</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal1TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal2LABEL" runat="server">Suppl_Analysis_Code_2</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal2TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel06">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal3LABEL" runat="server">Suppl_Analysis_Code_3</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal3TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal4LABEL" runat="server">Suppl_Analysis_Code_4</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal4TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel07">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal5LABEL" runat="server">Suppl_Analysis_Code_5</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal5TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal6LABEL" runat="server">Suppl_Analysis_Code_6</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal6TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel08">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal7LABEL" runat="server">Suppl_Analysis_Code_7</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal7TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal8LABEL" runat="server">Suppl_Analysis_Code_8</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal8TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel09">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal9LABEL" runat="server">Suppl_Analysis_Code_9</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal9TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal10LABEL" runat="server">Suppl_Analysis_Code_10</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal10TEXT" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel10">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moDescriptionLABEL" runat="server">DESCRIPTION</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moDescriptionTEXT" runat="server" Width="170px" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moUserAreaLABEL" runat="server">USER_AREA</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moUserAreaTEXT" runat="server" Width="170px" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                           
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Payable" Height="80%">
                                                        <ContentTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAccountCodeLABEL_P" runat="server">Account_Code</asp:Label>
                                                                    </td>
                                                                    <td width="20%">
                                                                        <asp:TextBox ID="moAccountCodeTEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moCompanyLABEL_P" runat="server">Accounting_Company</asp:Label>
                                                                    </td>
                                                                    <td width="20%">
                                                                        <asp:DropDownList ID="cboCompany_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtCompany_P" ReadOnly="true" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moConversionCodeControlLABEL_P" runat="server">Conversion_Code_Control</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboConversionCodeControl_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moDefaultCurrencyCodeLABEL_P" runat="server">Default_Currency_Code</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moDefaultCurrencyCodeTEXT_P" runat="server" Width="170px" MaxLength="3"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT1LABEL_P" runat="server">Acct_Analysis_TCode_1</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal1_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT1TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT2LABEL_P" runat="server">Acct_Analysis_TCode_2</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal2_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT2TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT3LABEL_P" runat="server">Acct_Analysis_TCode_3</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal3_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT3TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT4LABEL_P" runat="server">Acct_Analysis_TCode_4</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal4_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT4TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT5LABEL_P" runat="server">Acct_Analysis_TCode_5</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal5_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT5TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT6LABEL_P" runat="server">Acct_Analysis_TCode_6</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal6_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT6TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT7LABEL_P" runat="server">Acct_Analysis_TCode_7</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal7_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT7TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT8LABEL_P" runat="server">Acct_Analysis_TCode_8</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal8_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT8TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT9LABEL_P" runat="server">Acct_Analysis_TCode_9</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal9_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT9TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnalT10LABEL_P" runat="server">Acct_Analysis_TCode_10</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAcctAnal10_P" runat="server" Width="85px">
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="moAcctAnalT10TEXT_P" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moReportConversionControlLABEL_P" runat="server">Report_Conversion_Control</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboReportConversionControl_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moBalanceTypeLABEL_P" runat="server">Balance_Type</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moBalanceTypeTEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAddressStatusLABEL_P" runat="server">Address_Status</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboAddressStatus_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAddressLookupCodeLABEL_P" runat="server">Address_Lookup_Code</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAddressLookupCodeTEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSupplierStatusLABEL_P" runat="server">Supplier_Status</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboSupplierStatus_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSupplierLookUpCodeLABEL_P" runat="server">Supplier_Lookup_Code</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSupplierLookUpCodeTEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal1LABEL_P" runat="server">Analysis_Code_1</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal1TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal2LABEL_P" runat="server">Analysis_Code_2</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal2TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal3LABEL_P" runat="server">Analysis_Code_3</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal3TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal4LABEL_P" runat="server">Analysis_Code_4</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal4TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal5LABEL_P" runat="server">Analysis_Code_5</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal5TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal6LABEL_P" runat="server">Analysis_Code_6</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal6TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal7LABEL_P" runat="server">Analysis_Code_7</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal7TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal8LABEL_P" runat="server">Analysis_Code_8</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal8TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr valign="middle">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal9LABEL_P" runat="server">Analysis_Code_9</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal9TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAcctAnal10LABEL_P" runat="server">Analysis_Code_10</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAcctAnal10TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel01_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moDataAccessGroupCodeLABEL_P" runat="server">Data_Access_Group_Code</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="moDataAccessGroupCodeTEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppressReevaluationLABEL_P" runat="server">Suppress_Reevaluation</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="moSuppressReevaluationTEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel02_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moPayAsPaidAccountTypeLABEL_P" runat="server">Pay_As_Paid_Account_Type</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="cboPayAsPaidAccountType_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moPaymentMethodLABEL_P" runat="server">Payment_Method</asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="cboPaymentMethod_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel03_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moPaymentTermsLABEL_P" runat="server">PAYMENT_TERMS</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboPaymentTerms_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moAddressSequenceNumberLABEL_P" runat="server">Address_Sequence_Number</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moAddressSequenceNumberTEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel04_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal1LABEL_P" runat="server">Suppl_Analysis_Code_1</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                         <asp:DropDownList ID="cboSuppAnal1_P" runat="server" Width="170px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal2LABEL_P" runat="server">Suppl_Analysis_Code_2</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal2TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel05_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal3LABEL_P" runat="server">Suppl_Analysis_Code_3</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal3TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal4LABEL_P" runat="server">Suppl_Analysis_Code_4</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal4TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel06_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal5LABEL_P" runat="server">Suppl_Analysis_Code_5</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal5TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal6LABEL_P" runat="server">Suppl_Analysis_Code_6</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal6TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel07_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal7LABEL_P" runat="server">Suppl_Analysis_Code_7</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal7TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal8LABEL_P" runat="server">Suppl_Analysis_Code_8</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal8TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel08_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal9LABEL_P" runat="server">Suppl_Analysis_Code_9</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal9TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moSuppAnal10LABEL_P" runat="server">Suppl_Analysis_Code_10</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moSuppAnal10TEXT_P" runat="server" Width="170px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel09_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moDescriptionLABEL_P" runat="server">DESCRIPTION</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moDescriptionTEXT_P" runat="server" Width="170px" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moUserAreaLABEL_P" runat="server">USER_AREA</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moUserAreaTEXT_P" runat="server" Width="170px" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="fel10_P">
                                                                    <td class="LABELCOLUMN">
                                                                        <asp:Label ID="moDefaultBankSubCodeLABEL_P" runat="server">DEFAULT_BANK_SUB_CODE</asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="moDefaultBankSubCodeTEXT_P" runat="server" Width="170px" MaxLength="30"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="BANK_INFO">
                                                        <ContentTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="1">
                                                                <uc1:UserControlBankInfo ID="moBankInfo" runat="server"></uc1:UserControlBankInfo>
                                                            </table>
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                </cc1:TabContainer>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server">
    <script language="javascript">
	    <asp:literal id="litScriptVars" runat="server"></asp:literal>
		function EnableControl(value1,value2)
			{	
			
			    var cboName;
			    var txtName;
			    
			    cboName = value1;
			    txtName = value2;
			    
			    if (document.getElementById(cboName).value==prohib) 
				{
			       document.getElementById(txtName).disabled=true;
			       document.getElementById(txtName).value='';
				}
				else
				{
				document.getElementById(txtName).disabled=false;
				}
				
		    }
				
			function validate()
			{	// if account analysis is mandatory, then account analysis desc must be entered 
				
			   return true;
			}
			
		function SetAcctSystem(){
		
		    var elem = document.getElementsByTagName('tr');
            for(var i=0;i<elem.length;i++)
                 {
                    try
                    {
                        if (elem[i].id.substring(0,3).toUpperCase() == 'FEL')
                        {
                     
                             if (elem[i].id.substring(0,3).toUpperCase() != ACCT_SYSTEM_ID)
                            {
                                elem[i].style.display = 'none';
                            } else {
                                elem[i].style.display = '';
                            }
                         }
                                             
                    } catch(err) {
                    //nothing happening
                    }
                   
                 };

		}
		function SetTabSize(){
		    var tc = document.getElementById("<%=TabContainer1.ClientId%>");
            if (tc.childNodes[1].childNodes[2] != null){
		        tc.childNodes[1].childNodes[2].style.height = tc.childNodes[1].childNodes[0].clientHeight;
            }
        }   
        
        function pageLoad(sender,e){   
            //alert($find('<%=TabContainer1.ClientID%>')); 
            var tc = $find('<%=TabContainer1.ClientID%>');
            var tabIndex = parseInt(tc.get_activeTabIndex(), 10);
            tc.set_activeTabIndex(0); 
            SetTabSize();            
            tc.set_activeTabIndex(tabIndex); 
        }
        
        //var t=setTimeout("alert('0.1 seconds!')",1000);
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="14" runat="server" Width="90px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="BACK"></asp:Button>
    <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="15" runat="server" Width="80px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="SAVE"></asp:Button>
    <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="16" runat="server" Width="90px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="UNDO"></asp:Button>
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="17" runat="server" Width="80px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="New"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="18" runat="server" Width="135px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="New_With_Copy">
    </asp:Button>
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="19" runat="server" Width="80px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="Delete"></asp:Button>
</asp:Content>

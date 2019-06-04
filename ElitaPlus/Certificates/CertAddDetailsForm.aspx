<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="CertAddDetailsForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CertAddDetailsForm" 
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        .NoneDisplay {
            display: none;
        }
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="text-align: center">
                <table cellpadding="2" cellspacing="0" border="0" style="border: #999999 1px solid; width: 100%; background-color: #f1f1f1;">
                    <tr>
                        <td style="height: 8px;" colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">*<asp:Label ID="lblCertNum" runat="server">CERTIFICATE NUMBER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtCertNum" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label7" runat="server">DEALER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtDealer" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">*<asp:Label ID="lblProdCode" runat="server">PRODUCT CODE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlProdCode" runat="server" Width="205px" TabIndex="503">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtProdCode" runat="server" Columns="25" ReadOnly="true" CssClass="FLATTEXTBOX"
                                Visible="false" Width="200px"></asp:TextBox>
                            <asp:DropDownList ID="ddlProdCodeInfo" runat="server" CssClass="NoneDisplay">
                            </asp:DropDownList>

                        </td>
                        <td class="TD_LABEL">*<asp:Label ID="lblCertDuration" runat="server">CERTIFICATE DURATION</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtCertDuration" runat="server" Columns="20" TabIndex="509"></asp:TextBox>
                            (<asp:Label ID="label2" runat="server">MONTH</asp:Label>)
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtProdDesc" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                Visible="true"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">*<asp:Label ID="lblMfgWarranty" runat="server">MANUFACTURER_WARRANTY</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtMfgWarranty" runat="server" Columns="20" TabIndex="511"></asp:TextBox>
                            (<asp:Label ID="label33" runat="server">MONTH</asp:Label>)
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">*<asp:Label ID="lblWarrSalesDate" runat="server">WARR_SALES_DATE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtWarrSalesDate" runat="server" Columns="20" TabIndex="505"></asp:TextBox>
                            <asp:ImageButton ID="btnWarrSalesDate" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                TabIndex="506" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>

                        <td class="TD_LABEL">*<asp:Label ID="lblWarrPrice" runat="server">WARRANTY_PRICE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtWarrPrice" runat="server" Columns="20" TabIndex="513"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">*<asp:Label ID="lblProdSalesDate" runat="server">PRODUCT_SALES_DATE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtProdSalesDate" runat="server" Columns="20" TabIndex="507"></asp:TextBox>
                            <asp:ImageButton ID="btnProdSalesDate" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                TabIndex="508" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>

                        <td class="TD_LABEL">
                            <asp:Label ID="lblPYMTType" runat="server">PAYMENT_TYPE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlPYMTType" runat="server" Width="205px" TabIndex="514"></asp:DropDownList>
                            <asp:TextBox ID="txtPYMTType" runat="server" Width="200px" ReadOnly="true" CssClass="FLATTEXTBOX" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 8px;" colspan="4">
                            <hr style="height: 1px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">*<asp:Label ID="lblRetailPrice" runat="server">RETAIL_PRICE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtRetailPrice" runat="server" Columns="20" TabIndex="515"></asp:TextBox>
                            <asp:Literal ID="literalCurrency" runat="server"></asp:Literal>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label14" runat="server">MANUFACTURER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlManufacturer" runat="server" Width="205px" TabIndex="523">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtManufacturer" runat="server" Width="200px" ReadOnly="true" CssClass="FLATTEXTBOX"
                                Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label10" runat="server">INVOICE_NUMBER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtInvNum" runat="server" Width="200px" MaxLength="30" TabIndex="517"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label15" runat="server">MODEL</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtModel" runat="server" Width="200px" MaxLength="30" TabIndex="525"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label12" runat="server">BRANCH_CODE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtBranchCode" runat="server" Columns="20" MaxLength="10" TabIndex="519"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label16" runat="server">SERIAL_NUMBER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtSerialNum" runat="server" Width="200px" MaxLength="30" TabIndex="527"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label13" runat="server">SALES_REP_NUMBER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtSalesRepNum" runat="server" Width="200px" MaxLength="30" TabIndex="521"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label17" runat="server">ITEM_CODE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtItemCode" runat="server" Width="200px" MaxLength="10" TabIndex="529"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label20" runat="server">COUNTRY_OF_PURCHASE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtPurchaseCountry" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label18" runat="server">ITEM_DESCRIPTION</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtItemDesc" runat="server" Width="200px" MaxLength="50" TabIndex="531"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 8px;" colspan="4">
                            <hr style="height: 1px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="moSalutationLabel" runat="server">SALUTATION</asp:Label><asp:Label ID="laSemi" Text=":" runat="server"></asp:Label>
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="cboSalutationId" TabIndex="18" runat="server" Width="30%">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSalutation" runat="server" Width="200px" ReadOnly="true" CssClass="FLATTEXTBOX"
                                Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr  runat="server" id="moNotUseCustomerProfile">
                        <td class="TD_LABEL">
                            <asp:Label ID="label22" runat="server">CUSTOMER_NAME</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtCustName" runat="server" Width="200px" MaxLength="50" TabIndex="533"></asp:TextBox>
                        </td>
                    </tr>
                    <tr  runat="server" id="moUseCustomerProfile1">
                        <td class="TD_LABEL">
                            <asp:Label ID="lblFirstName" runat="server">FIRST_NAME</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="200px" MaxLength="50" TabIndex="533" />
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="lblMiddleName" runat="server">MIDDLE_NAME</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtMiddleName" runat="server" Width="200px" MaxLength="50" TabIndex="533" />
                        </td>
                    </tr>
                    <tr  runat="server" id="moUseCustomerProfile2">
                        <td class="TD_LABEL">
                            <asp:Label ID="lblLastName" runat="server">LAST NAME</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtLastName" runat="server" Width="200px" MaxLength="50" TabIndex="533" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label23" runat="server">TAX_ID</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtTaxID" runat="server" Width="200px" MaxLength="20" TabIndex="535"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label26" runat="server">ADDRESS1</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtAddress1" runat="server" Width="200px" MaxLength="50" TabIndex="545"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label24" runat="server">HOME_PHONE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtHomePhone" runat="server" Width="200px" MaxLength="20" TabIndex="539"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label27" runat="server">ADDRESS2</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtAddress2" runat="server" Width="200px" MaxLength="50" TabIndex="547"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label25" runat="server">WORK_PHONE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtWorkPhone" runat="server" Width="200px" MaxLength="20" TabIndex="541"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="label28" runat="server">CITY</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtCity" runat="server" Width="200px" MaxLength="50" TabIndex="549"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label32" runat="server">EMAIL</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="50" TabIndex="543"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="lblState" runat="server">STATE</asp:Label>
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtState" runat="server" Width="200px" MaxLength="50" TabIndex="551"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="TD_LABEL">
                            <asp:Label ID="label31" runat="server">CUST_COUNTRY</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtCustCountry" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="lblZIP" runat="server">ZIP_CODE</asp:Label>
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtZIP" runat="server" Width="200px" MaxLength="10" TabIndex="553"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="moAMLRegulations1">
                        <td class="TD_LABEL">
                            <asp:Label ID="labelMaritalStatus" runat="server">MARITAL_STATUS</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="205px" TabIndex="50"></asp:DropDownList>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="lblGender" runat="server">GENDER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlGender" runat="server" Width="205px" TabIndex="50"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="moAMLRegulations2">
                        <td class="TD_LABEL">
                            <asp:Label ID="labelNationality" runat="server">NATIONALITY</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlNationality" runat="server" Width="205px" TabIndex="50"></asp:DropDownList>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="lblPlaceOfBirth" runat="server">PLACE_OF_BIRTH</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlPlaceOfBirth" runat="server" Width="205px" TabIndex="50"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="moAMLRegulations3">
                        <td class="TD_LABEL">
                            <asp:Label ID="labelPersonType" runat="server">PERSON_TYPE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlPersonType" runat="server" Width="205px" TabIndex="50"></asp:DropDownList>
                        </td>
                        <td class="TD_LABEL">
                            <asp:Label ID="lblCUIT_CUIL" runat="server">CUIT_CUIL</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="moCUIT_CUIL" TabIndex="60" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="moAMLRegulations4">
                        <td class="TD_LABEL">
                            <asp:Label ID="labelDateOfBirth" runat="server">DATE_OF_BIRTH</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtDateOfBirth" runat="server" Columns="20" TabIndex="505"></asp:TextBox>
                            <asp:ImageButton ID="btnDateOfBirth" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                TabIndex="506" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>

                    </tr>
                    <tr>
                        <td align="right" id="tdMarketingPromoSer" runat="server" class="TD_LABEL">
                            <asp:Label ID="moMarketingPromoSerLabel" runat="server">MARKETING_PROMO_SER</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moMarketingPromoSerText" TabIndex="30" runat="server" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                        </td>
                        <td align="right" id="td1" runat="server" class="TD_LABEL">
                            <asp:Label ID="moMarketingPromoNumLabel" runat="server">MARKETING_PROMO_NUM</asp:Label>:
                        </td>
                        <td align="left" id="tdMarketigPromoNum" runat="server">
                            <asp:TextBox ID="moMarketingPromoNumText" runat="server" TabIndex="31" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="mobileDealerFields1">
                        <td align="right" runat="server" class="TD_LABEL">
                            <asp:Label ID="lblServiceLineNum" runat="server">SERVICE LINE NUMBER:</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtServiceLineNum" runat="server" TabIndex="31" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                        </td>
                        <td align="right" runat="server" class="TD_LABEL">
                            <asp:Label ID="Label3" runat="server">SUBSCRIBER_STATUS</asp:Label>:                            
                        </td>
                        <td align="left" id="td2" runat="server">
                            <asp:DropDownList ID="ddlSubscriberStatus" runat="server" Width="205px" TabIndex="50"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="mobileDealerFields2">
                        <td align="right" runat="server" class="TD_LABEL">
                            <asp:Label ID="lblMembershipType" runat="server">MEMBERSHIP_TYPE</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlMembershipType" runat="server" Width="205px" TabIndex="50"></asp:DropDownList>
                        </td>
                        <td align="right" runat="server" class="TD_LABEL">
                            <asp:Label ID="lblMembershipNum" runat="server">MEMBERSHIP_NUMBER</asp:Label>:
                        </td>
                        <td align="left" id="td4" runat="server">
                            <asp:TextBox ID="txtMembershipNum" runat="server" TabIndex="31" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trInsD">
                        <td style="height: 8px;" colspan="4">
                            <hr style="height: 1px" />
                        </td>
                    </tr>
                    <tr id="trIns1">
                        <td class="TD_LABEL">*<asp:Label ID="lblBankAcctNum" runat="server">BANK_ACCOUNT_NO</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtBankAcctNum" runat="server" Width="200px" MaxLength="29" TabIndex="563"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">*<asp:Label ID="lblBankRntNum" runat="server">BANK_ROUTING_NUMBER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtBankRntNum" runat="server" Width="200px" MaxLength="10" TabIndex="573"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trIns2">
                        <td class="TD_LABEL">*<asp:Label ID="lblBankOwner" runat="server">BANK_ACCT_OWNER_NAME</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtBankOwner" runat="server" Width="200px" MaxLength="50" TabIndex="583"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL" id="tdInsAmt1">*<asp:Label ID="lblInsAmt" runat="server">INSTALLMENT_AMOUNT</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left" id="tdInsAmt2">
                            <asp:TextBox ID="txtInsAmt" runat="server" Width="200px" MaxLength="16" TabIndex="593"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trIns3">
                        <td class="TD_LABEL">*<asp:Label ID="lblBillFreq" runat="server">BILLING_FREQUENCY</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:DropDownList ID="ddlBillFreq" runat="server" Width="205px" TabIndex="600"></asp:DropDownList>
                            <asp:TextBox ID="txtBillFreq" runat="server" Width="200px" ReadOnly="true" CssClass="FLATTEXTBOX" Visible="false"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL">*<asp:Label ID="lblInsNum" runat="server">NUMBER_OF_INSTALLMENTS</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtInsNum" runat="server" Width="200px" MaxLength="2" TabIndex="610"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trIns4">
                        <td class="TD_LABEL"><asp:Label ID="Label4" runat="server">OCCUPATION</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:TextBox ID="txtOccupation" runat="server" Width="200px" MaxLength="80" TabIndex="610"></asp:TextBox>
                        </td>
                        <td class="TD_LABEL"><asp:Label ID="Label5" runat="server"></asp:Label>
                        </td>
                        <td style="white-space: nowrap;" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="divBundle">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="Label1" runat="server">BUNDLE</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#DEE3E7"
                                                BorderColor="#999999" BorderStyle="Solid" CellPadding="1" BorderWidth="1px" AllowPaging="False"
                                                AllowSorting="False" CssClass="DATAGRID">
                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1" />
                                                <ItemStyle Wrap="False" BackColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Make" HeaderText="MAKE" ItemStyle-CssClass="CenteredTD"
                                                        HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Model" HeaderText="MODEL" ItemStyle-CssClass="CenteredTD"
                                                        HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SerialNumber" HeaderText="SERIAL_NUMBER" ItemStyle-CssClass="CenteredTD"
                                                        HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Description" HeaderText="DESCRIPTION" ItemStyle-CssClass="CenteredTD"
                                                        HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Price" DataFormatString="{0:#,0.00}" HeaderText="PRICE"
                                                        ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ProductCode" HeaderText="PRODUCT CODE" ItemStyle-CssClass="CenteredTD"
                                                        HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MfgWarranty" HeaderText="MAN_WARRANTY" ItemStyle-CssClass="CenteredTD"
                                                        HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />

    <script type="text/javascript">
        function ShowDesc(txtbox, ddlInfo) {
            var objS = event.srcElement;
            var val = objS.options[objS.selectedIndex].value;
            txtbox.value = ddlInfo.options[objS.selectedIndex].text;
            ddlInfo.selectedIndex = objS.selectedIndex;
            if (ddlInfo.options[ddlInfo.selectedIndex].value == 'Y') {
                HideBundleGrid(false);
            }
            else {
                HideBundleGrid(true);
            }
        }
        function HideBundleGrid(blnHidden) {
            if (blnHidden) {
                document.getElementById("divBundle").style.display = "none";
                document.getElementById("divBundleBtn").style.display = "none";
            }
            else {
                document.getElementById("divBundle").style.display = "inline";
                document.getElementById("divBundleBtn").style.display = "inline";
            }
        }

        function ShowInstallInfo(blnShowBank, blnShowIns) {
            if (blnShowBank) {
                document.getElementById("trInsD").style.display = "inline";
                document.getElementById("trIns1").style.display = "inline";
                document.getElementById("trIns2").style.display = "inline";
            } else {
                document.getElementById("trInsD").style.display = "none";
                document.getElementById("trIns1").style.display = "none";
                document.getElementById("trIns2").style.display = "none";
            }
            if (blnShowBank && blnShowIns) {
                document.getElementById("trIns3").style.display = "inline";
                document.getElementById("tdInsAmt1").style.display = "inline";
                document.getElementById("tdInsAmt2").style.display = "inline";
            } else {
                document.getElementById("trIns3").style.display = "none";
                document.getElementById("tdInsAmt1").style.display = "none";
                document.getElementById("tdInsAmt2").style.display = "none";
            }
        }
        function PYMTTypeChanged(blnInsRequired) {
            var objS = event.srcElement;
            var val = objS.options[objS.selectedIndex].value;
            if (val == '1') {
                ShowInstallInfo(false, false);
            }
            else {
                if (blnInsRequired) {
                    ShowInstallInfo(true, true);
                } else {
                    ShowInstallInfo(true, false);
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK" TabIndex="1105"></asp:Button>
                <asp:Button ID="btnUndo" runat="server" Text="UNDO" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" TabIndex="1110"></asp:Button>
                <asp:Button ID="btnSave" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE" TabIndex="1101"></asp:Button>
                <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW"></asp:Button>
                <asp:Button ID="btnCopyNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW_WITH_COPY" Width="125px" TabIndex="1115"></asp:Button>
            </td>
            <td style="text-align: right;">
                <div id="divBundleBtn">
                    <asp:Button ID="btnBundle" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="BUNDLE" TabIndex="1112"></asp:Button>
                </div>
            </td>
        </tr>
    </table>
    <asp:Literal ID="LitScript" runat="server"></asp:Literal>
</asp:Content>

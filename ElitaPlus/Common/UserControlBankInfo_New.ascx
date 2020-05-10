<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlBankInfo_New.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlBankInfo_New"
TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<tr id="bankInfo_hr2">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="labelNameonAccount" runat="server">NAME_ON_ACCOUNT:</asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="textboxNameAccount" runat="server" MaxLength="50" CssClass="medium" />
    </td>
    <td align="right">
        <asp:Label ID="labelCountryOfBank" runat="server">COUNTRY_OF_BANK:</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="moCountryDrop_WRITE" runat="server" AutoPostBack="True" CssClass="medium">
        </asp:DropDownList>
    </td>
</tr>

  <tr id="bankInfo_hr3">
            <td align="right" class="<%= HiddenClassName %>">
                <asp:Label ID="lblBankName" runat="server">BANK_NAME:</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtBankName" runat="server" CssClass="medium" MaxLength="50"></asp:TextBox>
                <asp:DropDownList ID="moBankName" runat="server" CssClass="medium" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblBranchName" runat="server">BANK_BRANCH_NAME:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblSpaceBranchName" runat="server"></asp:Label>
                <asp:TextBox ID="txtBankBranchName" runat="server" CssClass="medium" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr id="bankInfo_hr4">
            <td align="right" class="<%= HiddenClassName %>">
                <asp:Label ID="labelBankID" runat="server">BANK_ID:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LabelSpaceBankID" runat="server"></asp:Label>
                <asp:TextBox ID="textboxBankID" runat="server" CssClass="medium" MaxLength="10"></asp:TextBox>
            </td>
            <td align="right" class="<%= HiddenClassName %>">
                <asp:Label ID="lblBranchNumber" runat="server">BRANCH_NUMBER:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblBankBranchNumber" runat="server"></asp:Label>
                <asp:TextBox ID="txtBranchNumber" runat="server" CssClass="medium" MaxLength="8"></asp:TextBox>
            </td>
</tr>

<tr id="bankInfo_hr5">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="lblBranchDigit" Font-Bold="false" runat="server">BRANCH_DIGIT:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblBankBranchDigit" runat="server"></asp:Label>
        <asp:TextBox ID="txtBranchDigit" runat="server" MaxLength="8" CssClass="medium" />
    </td>
    <td align="right">
        <asp:Label ID="labelBankAccountNo" runat="server">BANK_ACCOUNT_NO:</asp:Label>
        <asp:Label ID="labelBankAccountNo_Last4Digits" Visible="false" runat="server">BANK_ACCOUNT_NO_LAST4DIGITS:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="LabelSpaceAccNo" runat="server"></asp:Label>
        <asp:TextBox ID="textboxBankAccountNo" runat="server" CssClass="medium" MaxLength="30"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr6">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="labelAccountType" runat="server">ACCOUNT_TYPE:</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="moAccountTypeDrop" runat="server" CssClass="medium">
        </asp:DropDownList>
    </td>
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="lblAcctDigit" runat="server">ACCOUNT_DIGIT:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblBankAcctDigit" runat="server"></asp:Label>
        <asp:TextBox ID="txtAcctDigit" runat="server" CssClass="medium" MaxLength="8"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr7">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="labelSwiftCode" runat="server">SWIFT_CODE:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="LabelSpaceSwiftCode" runat="server"></asp:Label>
        <asp:TextBox ID="textboxSwiftCode" runat="server" CssClass="medium" MaxLength="40"></asp:TextBox>
    </td>
    <td align="right">
        <asp:Label ID="labelIBAN_Number" runat="server">IBAN_NUMBER:</asp:Label>
        <asp:Label ID="labelIBAN_Number_Last4Digits" runat="server" Visible="false">IBAN_NUMBER_LAST4DIGITS:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="LabelSpaceIBAN" runat="server"></asp:Label>
        <asp:TextBox ID="textboxIBAN_Number" runat="server" MaxLength="40" CssClass="medium"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr8">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="labelBanklookup" Font-Bold="false" runat="server">BANK_LOOKUP_CODE:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblBankLookupCode" runat="server"></asp:Label>
        <asp:TextBox ID="txtBankLookupCode" runat="server" MaxLength="15" CssClass="medium" />
    </td>
    <td align="right">
        <asp:Label ID="labelbanksubcode" runat="server">BANK_SUB_CODE:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblBankSubcode" runat="server"></asp:Label>
        <asp:TextBox ID="txtBankSubcode" runat="server" CssClass="medium" MaxLength="5"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr9">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="labelBankSortCode" runat="server">BANK_SORT_CODE:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblBankSortCode" runat="server"></asp:Label>
        <asp:TextBox ID="txtBankSortCode" runat="server" CssClass="medium" MaxLength="15"></asp:TextBox>
        <asp:DropDownList ID="cboBankSortCodes" runat="server" CssClass="medium" Visible="false">
        </asp:DropDownList>
    </td>
    <td align="right">
        <asp:Label ID="labelTranslimit" runat="server">TRANSACTION_LIMIT:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblTransLimit" runat="server"></asp:Label>
        <asp:TextBox ID="txtTransLimit" runat="server" MaxLength="18" CssClass="medium"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr10">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:HiddenField ID="HiddenClassName" runat="server" />
        <asp:Label ID="labelTaxId" runat="server">TAX_ID</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblTaxId" runat="server"></asp:Label>
        <asp:TextBox ID="txtTaxId" runat="server" CssClass="medium" MaxLength="15"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_Address1">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="labelAddress1" runat="server">ADDRESS1:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblAddress1" runat="server"></asp:Label>
        <asp:TextBox ID="txtAddress1" runat="server" CssClass="medium" MaxLength="50"></asp:TextBox>
    </td>
    <td align="right">
        <asp:Label ID="labelAddress2" runat="server">ADDRESS2:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblAddress2" runat="server"></asp:Label>
        <asp:TextBox ID="txtAddress2" runat="server" MaxLength="50" CssClass="medium"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_Address2">
    <td align="right" class="<%= HiddenClassName %>">
        <asp:Label ID="labelCity" runat="server">CITY:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblCity" runat="server"></asp:Label>
        <asp:TextBox ID="txtCity" runat="server" CssClass="medium" MaxLength="50"></asp:TextBox>
    </td>
    <td align="right">
        <asp:Label ID="labelPostalCode" runat="server">POSTAL_CODE:</asp:Label>
    </td>
    <td align="left">
        <asp:Label ID="lblPostalCode" runat="server"></asp:Label>
        <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="25" CssClass="medium"></asp:TextBox>
    </td>
</tr>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlBankInfo.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlBankInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<tr id="bankInfo_hr1">
    <td valign="bottom" nowrap align="left" colspan="4">
        <hr style="width: 100%; height: 1px" size="1"/>
    </td>
</tr>
<tr id="bankInfo_hr2">
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelNameonAccount" Font-Bold="false" runat="server">NAME_ON_ACCOUNT:</asp:Label>
    </td>
    <td>
        &nbsp;
        <asp:TextBox ID="textboxNameAccount" TabIndex="1" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="50"></asp:TextBox>
    </td>
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelCountryOfBank" Font-Bold="false" runat="server">COUNTRY_OF_BANK:</asp:Label>
    </td>
    <td>
        &nbsp;
        <asp:DropDownList ID="moCountryDrop_WRITE" TabIndex="2" runat="server" Width="220px"
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
</tr>
<tr id="bankInfo_hr3">
    <td valign="middle" nowrap align="right" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelBankID" Font-Bold="false" runat="server">BANK_ID:</asp:Label>
    </td>
    <td>
        <asp:Label ID="LabelSpaceBankID" Font-Bold="False" Width="8" runat="server"></asp:Label>
        <asp:TextBox ID="textboxBankID" TabIndex="3" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="10"></asp:TextBox>
    </td>
    <td valign="middle" nowrap align="right" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelBankAccountNo" Font-Bold="false" runat="server">BANK_ACCOUNT_NO:</asp:Label>
    </td>
    <td>
        <asp:Label ID="LabelSpaceAccNo" Font-Bold="False" Width="8" runat="server"></asp:Label>
        <asp:TextBox ID="textboxBankAccountNo" TabIndex="4" runat="server" Width="220px"
            CssClass="FLATTEXTBOX" MaxLength="30"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr7">
    <td valign="middle" nowrap align="right" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="lblBankName" Font-Bold="false" runat="server">BANK_NAME:</asp:Label>
    </td>
    <td>
        &nbsp;
        <asp:TextBox ID="txtBankName" TabIndex="3" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="50"></asp:TextBox>
    </td>
    <td valign="middle" nowrap align="right" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="label4" Font-Bold="false" runat="server">BANK_BRANCH_NAME:</asp:Label>
    </td>
    <td>
        &nbsp;
        <asp:TextBox ID="txtBankBranchName" TabIndex="4" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="50"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr6">
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelSwiftCode" Font-Bold="false" runat="server">SWIFT_CODE:</asp:Label>
    </td>
    <td width="50%">
        <asp:Label ID="LabelSpaceSwiftCode" Font-Bold="False" Width="8" runat="server"></asp:Label>
        <asp:TextBox ID="textboxSwiftCode" TabIndex="5" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="40"></asp:TextBox>
    </td>
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelIBAN_Number" Font-Bold="false" runat="server">IBAN_NUMBER:</asp:Label>
    </td>
    <td width="50%">
        <asp:Label ID="LabelSpaceIBAN" Font-Bold="False" Width="8" runat="server"></asp:Label>
        <asp:TextBox ID="textboxIBAN_Number" TabIndex="6" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="40"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr4">
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelAccountType" Font-Bold="false" runat="server">ACCOUNT_TYPE:</asp:Label>
    </td>
    <td width="50%">
        &nbsp;
        <asp:DropDownList ID="moAccountTypeDrop" TabIndex="7" runat="server" Width="220px">
        </asp:DropDownList>
    </td>
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelTranslimit" runat="server">TRANSACTION_LIMIT</asp:Label>:
    </td>
    <td width="50%">
        &nbsp;
        <asp:Label ID="lblTransLimit" Font-Bold="False" runat="server"></asp:Label>
        <asp:TextBox ID="txtTransLimit" TabIndex="8" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="18"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr5">
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelBanklookup" Font-Bold="false" runat="server">BANK_LOOKUP_CODE:</asp:Label>
    </td>
    <td width="50%">
        &nbsp;
        <asp:Label ID="lblBankLookupCode" runat="server"></asp:Label>
        <asp:TextBox ID="txtBankLookupCode" TabIndex="9" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="15"></asp:TextBox>
    </td>
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelbanksubcode" runat="server">BANK_SUB_CODE:</asp:Label>
    </td>
    <td width="50%">
        &nbsp;
        <asp:Label ID="lblBankSubcode" runat="server"></asp:Label>
        <asp:TextBox ID="txtBankSubcode" TabIndex="10" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="5"></asp:TextBox>
    </td>
</tr>
<tr id="bankInfo_hr8">
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
        <asp:Label ID="labelBankSortCode" Font-Bold="false" runat="server">BANK_SORT_CODE:</asp:Label>
    </td>
    <td width="50%">
        &nbsp;
        <asp:Label ID="lblBankSortCode" runat="server"></asp:Label>
        <asp:TextBox ID="txtBankSortCode" TabIndex="9" runat="server" Width="220px" CssClass="FLATTEXTBOX"
            MaxLength="15"></asp:TextBox>
    </td>
    <td valign="middle" nowrap align="right" width="1%" style="text-align: right; vertical-align: middle;">
    </td>
    <td width="50%">
        &nbsp;
    </td>
</tr>
<tr id="bankInfo_hr9">
    <td height="10" colspan="4" align="left" nowrap valign="bottom">
        <hr style="width: 100%; height: 1px" size="1"/>
    </td>
</tr>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="ClaimDeniedInformationForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.ClaimDeniedInformationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moClaimNumberLabel" runat="server">Claim_Number</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moClaimNumberText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moCertificateLabel" runat="server">Certificate</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moCertificateText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moDealerLabel" runat="server">Dealer</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moDealerText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moProductCodeLabel" runat="server">Product_code</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moProductCodeText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan=4>
                &nbsp;
                <hr style="width: 100%; height: 1px" size="1">
                &nbsp;
            </td>
        </tr>
      
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moMakeLabel" runat="server">Make</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moMakeText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
            
            <td class="LABELCOLUMN">
                <asp:Label ID="moModelLabel" runat="server">Model</asp:Label>:  </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moModelText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
            </tr>
            <tr>
              
            <td class="LABELCOLUMN">
                <asp:Label ID="moCustomerNameLabel" runat="server">Customer_Name</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moCustomerNameText" runat="server" Columns="40" MaxLength="40" TabIndex="501"></asp:TextBox>
            </td>
             <td></td>
            <td></td>
        </tr>
       
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moAddressLabe" runat="server">Address</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moAddressText" runat="server" Columns="50" MaxLength="50" TabIndex="501"></asp:TextBox>
            </td>
             <td></td>
            <td></td>
        </tr>
    
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moAddressLabe2" runat="server">Address2</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moAddressText2" runat="server" Columns="50" MaxLength="50" TabIndex="501"></asp:TextBox>
            </td>
             <td></td>
            <td></td>
        </tr>
       
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moCityLabel" runat="server">City</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moCityText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
             <td></td>
            <td></td>
        </tr>
        <tr>
          
        <td class="LABELCOLUMN">
            <asp:Label ID="moStateLabel" runat="server">State_Province</asp:Label>:
        </td>
        <td style="white-space: nowrap;" align="left">
            <asp:TextBox ID="moStateText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
        </td>
         <td></td>
            <td></td>
        </tr>
       
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moZipLabel" runat="server">Zip</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                <asp:TextBox ID="moZipText" runat="server" Columns="25" MaxLength="20" TabIndex="501"></asp:TextBox>
            </td>
             <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <%--<span ></span>--%>
    <asp:Button ID="btnBack_WRITE" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="110px" Height="20px"
        Text="BACK" CssClass="FLATBUTTON"></asp:Button>
    <asp:Button ID="btnNext_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="110px" Height="20px"
        Text="Next" CssClass="FLATBUTTON"></asp:Button>
</asp:Content>

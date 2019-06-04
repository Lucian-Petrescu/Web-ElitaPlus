<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlCertificateInfo.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.UserControlCertificateInfo"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="TableFixed" style="width: 100%" cellspacing="1" cellpadding="0" width="100%"
    align="center" border="0">
    <tr>
        
        <td nowrap style="text-align: left; vertical-align: middle">
            <asp:Label ID="LabelCustomerName"  Font-Bold="false" runat="server">CUSTOMER_NAME</asp:Label>
        </td>
        <td  style="text-align:left;">
            <asp:TextBox ID="TextboxCustomerName" TabIndex="1" Width="95%" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
        </td>
        <td nowrap style="text-align: right; vertical-align: middle">
            <asp:Label ID="LabelRiskType"  Font-Bold="false" runat="server">RISK_TYPE</asp:Label>
        </td>
        <td>
            &nbsp;
            <asp:TextBox ID="TextboxRiskType" TabIndex="1"  runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td nowrap style="text-align: right; vertical-align: middle" width="10%">
            <asp:Label ID="moCertificateLabel"  Font-Bold="false" runat="server">CERTIFICATE</asp:Label>:
        </td>
        <td  style="text-align: left;" width="25%">&nbsp;
           <asp:TextBox ID="moCertificateText" TabIndex="1"  runat="server" CssClass="FLATTEXTBOX" Width="164px"></asp:TextBox>
        </td>
        <td nowrap style="text-align: right; vertical-align: middle" width="15%">
            <asp:Label ID="moWarrantySoldOnLabel"  Font-Bold="false" runat="server">WARRANTY_SALES_DATE</asp:Label>:
        </td>
        <td style="text-align: left;" width="20%">
            &nbsp;
            <asp:TextBox ID="moWarrantySoldOnText" TabIndex="1"  runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
        </td>
        <td  nowrap style="text-align: right; vertical-align: middle" width="10%">
            <asp:Label ID="LabelCompanyName" Font-Bold="false" runat="server">COMPANY_CODE</asp:Label>:
         </td>
         <td width="20%" align="left">&nbsp;
            <asp:TextBox ID="moCompanyNameText" TabIndex="1" Width="20%" runat="server" CssClass="FLATTEXTBOX">&nbsp;&nbsp;</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td nowrap style="text-align: right; vertical-align: middle" >
            <asp:Label ID="moDealerNameLabel"  Font-Bold="false" runat="server">DEALER_NAME</asp:Label>:
        </td>
        <td  style="text-align: left;">&nbsp;
            <asp:TextBox ID="moDealerNameText" TabIndex="1" Width="95%" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
        </td>
        <td nowrap style="text-align: right; vertical-align: middle">
            <asp:Label ID="Label1"  Font-Bold="false" runat="server">DEALER_GROUP</asp:Label>:
        </td>
        <td nowrap align="left">
            &nbsp;
            <asp:TextBox ID="moDealerGroupText" TabIndex="1" Width="80%" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
         </td>
        <td  nowrap style="text-align: right; vertical-align: middle">
            <asp:Label ID="moStatusLabel" Width="40%" Font-Bold="false" runat="server">STATUS:</asp:Label>
        </td>
        <td align="left">&nbsp;            
            <asp:TextBox ID="moStatusText" TabIndex="1" Width="8%" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td nowrap style="text-align: right; vertical-align: middle" >
            
        </td>
        <td  style="text-align: left;">&nbsp;
            
        </td>
        <td nowrap style="text-align: right; vertical-align: middle">
            
        </td>
        <td nowrap align="left">
            &nbsp;
            
         </td>
        <td  nowrap style="text-align: right; vertical-align: middle">
            <asp:Label ID="moSubStatusLabel" Font-Bold="false" runat="server">SUBSCRIBER_STATUS:</asp:Label>
        </td>
                  
        <td align="left">&nbsp;            
            <asp:TextBox ID="moSubStatusText" TabIndex="1" Width="8%" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
        </td>
        
         <td  nowrap style="text-align: right; vertical-align: middle">
            <asp:Label ID="moRestictedStatus"  runat="server">Restricted</asp:Label>
        </td>
    </tr>
</table>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="InvoiceControlDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceControlDetailForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;WIDTH: 100%; background-color:#f1f1f1;">
                    <tr><td style="height:8px;" colspan="4"></td></tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label1" runat="server">COMPANY</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">  
                            <asp:textbox id="txtCompany" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="55"></asp:textbox>
                        </td>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label7" runat="server">INVOICE_NUMBER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtInvNum" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label5" runat="server">DEALER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left"> 
                            <asp:textbox id="txtDealer" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="55"></asp:textbox>
                        </td>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label8" runat="server">CREDIT_NOTE_NUMBER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCreditNoteNum" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label3" runat="server">BRANCH_NAME</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtBranch" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="55"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label4" runat="server">PREVIOUS_INVOICE_DATE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtPreInvDate" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;" colspan="2">
                            &nbsp;
                        </td>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label6" runat="server">INVOICE_DATE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtInvDate" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:center;white-space: nowrap; vertical-align:middle">
                            <hr style="HEIGHT: 1px"/>
                            <asp:Label ID="label11" runat="server">NEW CERTIFICATES</asp:Label>
                            <hr style="HEIGHT: 1px"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label9" runat="server">CERTIFICATE_COUNT</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTotalCert" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label14" runat="server">TAX1</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTax1" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label10" runat="server">GROSS_AMT_RECEIVED</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewGWP" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label15" runat="server">TAX2</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTax2" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label12" runat="server">PREMIUM_WRITTEN</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewWP" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label16" runat="server">TAX3</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTax3" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label13" runat="server">COMMISSION</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewCommission" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label17" runat="server">TAX4</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTax4" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label20" runat="server">TOTAL</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTotalAmt" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label18" runat="server">TAX5</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTax5" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label19" runat="server">TAX6</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtNewTax6" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>                    
                    <tr>
                        <td colspan="4" style="text-align:center;white-space: nowrap; vertical-align:middle">
                            <hr style="HEIGHT: 1px"/>
                            <asp:Label ID="label21" runat="server">CANCELLATIONS</asp:Label>
                            <hr style="HEIGHT: 1px"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label22" runat="server">CERTIFICATE_COUNT</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTotalCert" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label26" runat="server">TAX1</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTax1" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label23" runat="server">GROSS_AMT_RECEIVED</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclGWP" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label27" runat="server">TAX2</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTax2" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label24" runat="server">PREMIUM_WRITTEN</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclWP" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label28" runat="server">TAX3</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTax3" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label25" runat="server">COMMISSION</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclCommission" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label29" runat="server">TAX4</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTax4" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label32" runat="server">TOTAL</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTotalAmt" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label30" runat="server">TAX5</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTax5" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td style="text-align:right;white-space: nowrap; vertical-align:middle">
                            <asp:Label ID="label31" runat="server">TAX6</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCanclTax6" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                    </tr>                    
                </table>            
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK"></asp:Button>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DataProtection.CustomerReportForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="../Reports/ReportCeInputControl.ascx" %>
<%--<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo.ascx" %>--%>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';
        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }


    </script>

</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">

        <tr>
            <td colspan="3" style="padding-left: 28px">

                <span style="z-index: -1; color: red">*</span>
                <asp:Label ID="lblRequestId" runat="server">REQUEST_ID</asp:Label><br />
                <asp:TextBox ID="txtRequestID" runat="server" SkinID="MediumTextBox"></asp:TextBox>
            </td>

        </tr>
        <tr>

            <td>
                <fieldset>
                    <legend>Search Criteria</legend>
                    <table width="100%">
                        <tr>
                            <td>

                                 <asp:Label ID="lblSerial" runat="server">SERIAL_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtSerial" runat="server" SkinID="MediumTextBox" AutoPostBack="False" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblPhoneNumber" runat="server">PHONE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtPhoneNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False" MaxLength="50"></asp:TextBox>
                            </td>

                            <td>
                                <asp:Label ID="lblEmail" runat="server">EMAIL</asp:Label><br />
                                <asp:TextBox ID="txtEmail" runat="server" SkinID="MediumTextBox" AutoPostBack="False" MaxLength="50"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>

                            <td>
                               <asp:Label ID="lblAccount" runat="server">ACCOUNT_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtAccount" runat="server" SkinID="MediumTextBox" AutoPostBack="False" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblInvoice" runat="server">INVOICE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtInvoice" runat="server" SkinID="MediumTextBox" AutoPostBack="False" MaxLength="50"></asp:TextBox>

                            </td>

                            <td>
                                <asp:Label ID="lblCertificate" runat="server">CERTIFICATE</asp:Label><br />
                                <asp:TextBox ID="txtCertificate" runat="server" SkinID="MediumTextBox" AutoPostBack="False" MaxLength="50"></asp:TextBox>

                            </td>

                        </tr>

                        <tr>


                            <td>
                                 <asp:Label ID="lblIDNumber" runat="server">CertTaxIdER</asp:Label><br />
                                <asp:TextBox ID="txtTaxIDNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False" MaxLength="50"></asp:TextBox>

                               </td> 
                           
                            
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                              <b> <p style="font-size:10px; font-family:Verdana,Arial, Helvetica, sans-serif;"> <asp:Label ID="lblNote" runat="server">NOTE_SEARCH_CRITERIA_REQUIRED</asp:Label> </p></b>

                            </td>

                        </tr>
                       
                    </table>
                </fieldset>
            </td>

        </tr>

        
         <tr>
                            <td  style="padding-right:50px; padding-top:20px" colspan="3" align="right">
                                <label>
                                    <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                                    <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                </label>
                            </td>
                        </tr>
    </table>

</asp:Content>


<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SearchConsumerForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DataProtection.SearchConsumerForm"
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
            <td colspan="4" style="padding-left: 18px">

                <span style="z-index: -1; color: red">*</span>
                <asp:Label ID="lblRequestId" runat="server">REQUEST_ID</asp:Label><br />
                <asp:TextBox ID="txtRequestID" runat="server" SkinID="MediumTextBox"></asp:TextBox>
            </td>

        </tr>
        <tr>

            <td>
                <fieldset>
                    <legend><asp:Label ID="lblSearchCriteria"  runat="server" >SEARCH_CRITERIA</asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td width ="25%">

                                <asp:Label ID="lblCustomerName" runat="server">CUSTOMER_NAME</asp:Label><br />
                                <asp:TextBox ID="txtCustomerName" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td width ="25%">
                                <asp:Label ID="lblPhoneNumber" runat="server">PHONE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtPhoneNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>

                            <td width ="25%">
                                <asp:Label ID="lblEmail" runat="server">EMAIL</asp:Label><br />
                                <asp:TextBox ID="txtEmail" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>

                      

                            <td width ="25%">
                                <asp:Label ID="lblIDNumber" runat="server">CertTaxIdER</asp:Label><br />
                                <asp:TextBox ID="txtTaxIDNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td> 
                         </tr>

                        <tr>
                            <td width ="25%">
                                <asp:Label ID="lblInvoice" runat="server">INVOICE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtInvoice" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>

                            </td>

                            <td width ="25%">
                                <asp:Label ID="lblCertificate" runat="server">CERTIFICATE</asp:Label><br />
                                <asp:TextBox ID="txtCertificate" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>

                            </td>



                            <td width ="25%">
                                <asp:Label ID="lblAccount" runat="server">ACCOUNT_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtAccount" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>

                               </td> 
                            <td width ="25%">
                                <asp:Label ID="lblSerial" runat="server">SERIAL_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtSerial" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                              <b> <p style="font-size:11px; font-family:Verdana,Arial, Helvetica, sans-serif; color:#0066cc"> <asp:Label ID="lblNote" runat="server">NOTE_SEARCH_CRITERIA_REQUIRED</asp:Label> </p></b>

                            </td>

                        </tr>
                       
                    </table>
                </fieldset>
            </td>
            </tr>
            <tr>
            <td>
                
                <fieldset>
                    <legend><asp:Label ID="lblAddtional" runat="server">ADDITIONAL_SEARCH_CRITERIA</asp:Label></legend>
                    <table  width="100%">
                        <tr>

                            <td width ="25%">
                                <asp:Label ID="lblAddress" runat="server">ADDRESS</asp:Label>
                                <br />
                                <asp:TextBox ID="txtAddress" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>

                            <td width ="25%">
                                <asp:Label ID="lblGender" runat="server">GENDER</asp:Label>
                                <br />
                                <asp:DropDownList runat="server" ID="ddlGender" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                            <td width ="25%">
                                <asp:Label ID="lblDealer" runat="server">DEALER</asp:Label><br />
                                <asp:DropDownList ID="ddlDealer" runat="server" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>                                          
                            <td width ="25%">
                                <asp:Label ID="lblZip" runat="server">ZIP</asp:Label>
                                <br />
                                <asp:TextBox ID="txtZip" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                             </tr>

                        <tr>
 <td>
                                <asp:Label ID="lblBirthDate" runat="server">BIRTH_DATE</asp:Label><br />
                                <asp:TextBox ID="txtBirthDate" runat="server" SkinID="smallTextBox"></asp:TextBox>
                                <asp:ImageButton ID="btntxtBirthDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>

                            </td>
                            
                           <td valign="bottom" style="padding-top: 20px;">

                                <asp:Label ID="lblIncludeRecon" runat="server">INCLUDE_RECON</asp:Label>
                                <asp:CheckBox ID="chkIncludeRecon" runat="server"></asp:CheckBox>

                            </td>
                            <td colspan ="2"></td>
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


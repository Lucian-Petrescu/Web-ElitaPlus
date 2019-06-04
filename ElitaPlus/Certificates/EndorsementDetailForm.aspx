<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EndorsementDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.EndorsementDetailForm" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>EndorsementDetailForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body ms_positioning="GridLayout">
    <div style="left: 0px; width: 100%; position: absolute; top: 0px; height: 100%" ms_positioning="FlowLayout">
        <form id="Form1" method="post" runat="server">
            <!--Start Header-->
            <table id="Table1" style="border-right: black 1px solid; border-top: black 1px solid;
                margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid; height: 20px"
                cellspacing="0" cellpadding="0" width="100%" bgcolor="#d5d6e4" border="0">
                <tr>
                    <td valign="top">
                        <table id="Table2" style="width: 100%; height: 24px" border="0">
                            <tr>
                                <td height="20">
                                    <asp:Label ID="moTitleLabel" runat="server"  CssClass="TITLELABEL">Certificates</asp:Label>:&nbsp;<asp:Label
                                        ID="moTitle2Label" runat="server"  CssClass="TITLELABELTEXT">ENDORSEMENT_DETAIL</asp:Label>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
                margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid;" height="91.2%"
                cellspacing="0" cellpadding="0" rules="none" width="100%" bgcolor="#d5d6e4" border="0">
                <!--d5d6e4-->
                <tr>
                    <td style="height: 2%">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" valign="top" align="center">
                        <asp:Panel ID="WorkingPanel" runat="server" Width="100%" Height="98%">
                            <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 100%"
                                height="100%" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                                bgcolor="#fef9ea" border="0">
                                <tbody>
                                    <tr>
                                        <td valign="top" height="1" style="width: 100%">
                                            <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" style="width: 100%">
                                            <table id="Table3" style="width: 100%; height: 224px" cellspacing="1" cellpadding="1"
                                                width="100%" align="center" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100%; height: 54px;" align="center" colspan="2">
                                                            <uc1:UserControlCertificateInfo ID="moCertificateInfoController" runat="server"></uc1:UserControlCertificateInfo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="middle" style="width: 100%; height: 2px;">
                                                            <hr style="height: 1px"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%; height: 5px;" align="center" colspan="2">
                                                            <asp:Label ID="LbDetail" runat="server" >Certificate Information</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%; height: 8px" valign="top" nowrap align="left">
                                                            <table id="Table7" style="height: 100px" cellspacing="0" cellpadding="0" width="100%"
                                                                align="center" border="0">
                                                                <tr>
                                                                    <td style="width: 45%; height: 8px" align="center">
                                                                        <asp:Label ID="Label5" runat="server" >BEFORE</asp:Label></td>
                                                                    <td style="width: 55%; height: 8px" align="center">
                                                                        <asp:Label ID="Label4" runat="server" >AFTER</asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 45%; height: 13px" align="left">
                                                                        <hr style="width: 100%; height: 1px" width="98%" size="2"/>
                                                                    </td>
                                                                    <td style="width: 55%; height: 13px" align="left">
                                                                        <hr style="width: 100%; height: 1px" width="98%" size="2"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 45%; height: 120px" align="left" colspan="1">
                                                                        <table id="Table5" style="width: 100%; height: 61px" cellspacing="0" cellpadding="0"
                                                                            width="100%" border="0">
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblCustomerNamePre" runat="server" Font-Bold="false">CUSTOMER_NAME</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCustNamePre" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblEmailAddrPre" runat="server" Font-Bold="false">EMAIL</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtEmailAddrPre" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                           </tr> 
                                                                           <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblHomePhonePre" runat="server" Font-Bold="false">HOME_PHONE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtHomePhonePre" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                           </tr>   
                                                                           <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblWorkPhonePre" runat="server" Font-Bold="false">WORK_PHONE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtWorkPhonePre" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                           </tr> 
                                                                           <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblLangPrefPre" runat="server" Font-Bold="false">LANGUAGE_PREF</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLangPrefPre" TabIndex="35" runat="server" Width="190px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>                
                                                                            <tr style="height: 7px">
                                                                                <td style="width: 100%; height: 7px" align="right">
                                                                                    <asp:Label ID="lblWarrantySalesDatePre" runat="server" Font-Bold="false" align="right">WARRANTY_SALES_DATE</asp:Label><asp:Label
                                                                                        ID="lblTermPre" runat="server" Font-Bold="false" align="right">MANUFACTURER_TERM</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td style="width: 100%; height: 7px">
                                                                                    <asp:TextBox ID="txtTermPre" TabIndex="35" runat="server" Width="40px" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtWarrantySalesDatePre" TabIndex="35" runat="server" Width="90px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                                            </tr>
                                                                            <asp:Panel ID="moProductSaleDatePrePanel" runat="server">
                                                                                <tr style="height: 7px">
                                                                                    <td align="right">
                                                                                        <asp:Label ID="lblProductSaleDatePre" runat="server" Font-Bold="false" align="right">PRODUCT_SALES_DATE</asp:Label>:
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtProductSalesDatepre" TabIndex="35" runat="server" Width="90px"
                                                                                            CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                                                </tr>
                                                                            </asp:Panel>
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblSalesPricePre" runat="server" Font-Bold="False">SALES_PRICE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBoxSalesPricePre" TabIndex="35" runat="server" 
                                                                                        Width="90px" CssClass="FLATTEXTBOX" Enabled="True"></asp:TextBox></td>
                                                                            </tr>
                                                                         </table>
                                                                    </td>
                                                                    <td style="width: 55%; height: 120px" align="left">
                                                                        <table id="Table10" style="width: 100%; height: 61px" cellspacing="0" cellpadding="0" width="406" border="0">
                                                                            <tr style="height: 7px">
                                                                                <td align="right" style="height: 16px">
                                                                                    <asp:Label ID="lblCustomerNamePos" runat="server" Font-Bold="false">CUSTOMER_NAME</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td style="height: 16px">
                                                                                    <asp:TextBox ID="txtCustNamePos" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblEmailAddrPost" runat="server" Font-Bold="false">EMAIL</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtEmailAddrPost" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr> 
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblHomePhonePost" runat="server" Font-Bold="false">HOME_PHONE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtHomePhonePost" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>   
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblWorkPhonePost" runat="server" Font-Bold="false">WORK_PHONE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtWorkPhonePost" TabIndex="35" runat="server" Width="240px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr> 
                                                                                                                                                                      
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblLangPrefPost" runat="server" Font-Bold="false">LANGUAGE_PREF</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLangPrefPost" TabIndex="35" runat="server" Width="190px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>                                                                             
                                                                            <tr style="height: 7px">
                                                                                <td  align="right">
                                                                                    <asp:Label ID="lblWarrantySalesDatePos" runat="server" Font-Bold="false">WARRANTY_SALES_DATE</asp:Label><asp:Label
                                                                                        ID="lblTermPos" runat="server" Font-Bold="false" align="right">MANUFACTURER_TERM</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td >
                                                                                    <asp:TextBox ID="txtTermPos" TabIndex="35" runat="server" Width="40px" CssClass="FLATTEXTBOX"></asp:TextBox><asp:TextBox
                                                                                        ID="txtWarrantySalesDatePost" TabIndex="35" runat="server" Width="90px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                                            </tr>
                                                                            <asp:Panel ID="moProductSaleDatePosPanel" runat="server">
                                                                                <tr style="height: 7px">
                                                                                    <td align="right">
                                                                                        <asp:Label ID="lblProductSaleDatePos" runat="server" Font-Bold="false">PRODUCT_SALES_DATE</asp:Label>:
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtProductSalesDatePost" TabIndex="35" runat="server" Width="90px"
                                                                                            CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                                                </tr>
                                                                            </asp:Panel>
                                                                              <tr style="height: 7px">
                                                                                 <td align="right">
                                                                                    <asp:Label ID="lblSalesPricePos" runat="server" Font-Bold="False">SALES_PRICE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBoxSalesPricePost" TabIndex="35" runat="server" 
                                                                                        Width="90px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>
                                                                         </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr> 
                                                  <tr>
                                                        <td valign="middle" style="width: 100%; height: 2px;">
                                                            <hr style="height: 1px"/>
                                                        </td>
                                                    </tr>
                                                <tr>
                                                     <td style="width: 100%; height: 5px;" align="center" colspan="2">
                                                            <asp:Label ID="Label18" runat="server">Tax_ID_Information</asp:Label></td>
                                                </tr>                                            
                                                   <tr>
                                                        <td style="width: 100%; height: 8px" valign="top" nowrap align="left">
                                                            <table id="Table9" style="height: 100px" cellspacing="0" cellpadding="0" width="100%"
                                                                align="center" border="0">
                                                                <tr>
                                                                    <td style="width: 45%; height: 8px" align="center">
                                                                        <asp:Label ID="Label11" runat="server" >BEFORE</asp:Label></td>
                                                                    <td style="width: 55%; height: 8px" align="center">
                                                                        <asp:Label ID="Label12" runat="server" >AFTER</asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 45%; height: 13px" align="left">
                                                                        <hr style="width: 100%; height: 1px" width="98%" size="2"/>
                                                                    </td>
                                                                    <td style="width: 55%; height: 13px" align="left">
                                                                        <hr style="width: 100%; height: 1px" width="98%" size="2"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 45%; height: 120px" align="left" colspan="1">
                                                                        <table id="Table11" style="width: 100%; height: 61px" cellspacing="0" cellpadding="0"
                                                                            width="100%" border="0">
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblDocumentTypePre" runat="server" Font-Bold="false">DOCUMENT_TYPE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDocumentTypePre" TabIndex="35" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblNewTaxIdPre" runat="server" Font-Bold="false">DOCUMENT_NUMBER</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtNewTaxIdPre" TabIndex="35" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                           </tr> 
                                                                           <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblIDTypePre" runat="server" Font-Bold="false">ID_TYPE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtIDTypePre" TabIndex="35" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                           </tr>   
                                                                           <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblRGNumberPre" runat="server" Font-Bold="false">RG_NUMBER</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRGNumberPre" TabIndex="35" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                           </tr> 
                                                                           <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblDocumentAgencyPre" runat="server" Font-Bold="false">DOCUMENT_AGENCY</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDocumentAgencyPre" TabIndex="35" runat="server" 
                                                                                        Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>                
                                                                           <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblDocumentIssueDatePre" runat="server" Font-Bold="False">DOCUMENT_ISSUE_DATE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDocumentIssueDatePre" TabIndex="35" runat="server" 
                                                                                        Width="200px" CssClass="FLATTEXTBOX" Enabled="True"></asp:TextBox></td>
                                                                            </tr>
                                                                         </table>
                                                                    </td>
                                                                    <td style="width: 55%; height: 120px" align="left">
                                                                        <table id="Table12" style="width: 100%; height: 61px" cellspacing="0" cellpadding="0" width="406" border="0">
                                                                            <tr style="height: 7px">
                                                                                <td align="right" style="height: 16px">
                                                                                    <asp:Label ID="lblDocumentTypePost" runat="server" Font-Bold="False">DOCUMENT_TYPE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td style="height: 16px">
                                                                                    <asp:TextBox ID="txtDocumentTypePost" TabIndex="35" runat="server" 
                                                                                        Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblNewTaxIdPost" runat="server" Font-Bold="False">DOCUMENT_NUMBER</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtNewTaxIdPost" TabIndex="35" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr> 
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblIDTypePost" runat="server" Font-Bold="False">ID_TYPE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtIDTypePost" TabIndex="35" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>   
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblRGNumberPost" runat="server" Font-Bold="False">RG_NUMBER</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRGNumberPost" TabIndex="35" runat="server" Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr> 
                                                                                                                                                                      
                                                                            <tr style="height: 7px">
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblDocumentAgencyPost" runat="server" Font-Bold="False">DOCUMENT_AGENCY</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDocumentAgencyPost" TabIndex="35" runat="server" 
                                                                                        Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>                                                                             
                                                                            <tr style="height: 7px">
                                                                                 <td align="right">
                                                                                    <asp:Label ID="lblDocumentIssueDatePost" runat="server" Font-Bold="False">DOCUMENT_ISSUE_DATE</asp:Label>:
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDocumentIssueDatePost" TabIndex="35" runat="server" 
                                                                                        Width="200px" CssClass="FLATTEXTBOX"
                                                                                        Enabled="True"></asp:TextBox></td>
                                                                            </tr>


                                                                         </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>                                                      
                                            </tbody> 
                                            </table>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                    <td valign="top" align="center" style="width: 100%">
                                    <table id="Table4" style="height: 100px" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                         <tr><td style="width: 100%; " align="center" colspan="3">
                                           <hr style="width: 100%; height: 1px" width="98%" size="2"/></td>                                    
                                        </tr>

                                        <tr>
                                           <td style="width: 100%; height: 5px;" align="center" colspan="3">
                                           <asp:Label ID="Label10" runat="server" >Address Information</asp:Label></td>
                                        </tr>                         
                                        <tr>
                                            <td style="width: 49%; height: 8px" align="center">
                                                <asp:Label ID="Label1" runat="server" >BEFORE</asp:Label></td>
                                            <td width="2%">&nbsp;</td>     
                                            <td style="width: 49%; height: 8px" align="center">
                                                <asp:Label ID="Label9" runat="server" >AFTER</asp:Label></td>
                                        </tr>
                                        <tr>
                                        <td style="width: 49%;"  align="left">
                                        <table>
                                        <uc1:UserControlAddress ID="moAddrPreController" runat="server"></uc1:UserControlAddress>    
                                        </table>
                                        </td>
                                        <td width="2%">&nbsp;</td> 
                                        <td style="width: 49%;" align="right">
                                        <table>
                                        <uc1:UserControlAddress ID="moAddrPostController" runat="server"></uc1:UserControlAddress>
                                        </table>
                                        </td> 
                                        </tr>
                                    </table>                            
                                    </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 100%; height: 10px" valign="top" nowrap align="center">
                                            <asp:Label ID="LbCoverage" runat="server" >Coverage Information</asp:Label>
                                            <asp:Panel ID="pnManufacturer" runat="server" Width="100%">
                                                <table id="tblManufacturer" style="border-right: black 1px solid; border-top: black 1px solid;
                                                    border-left: black 1px solid; width: 100%; border-bottom: black 1px solid; height: 120px"
                                                    cellspacing="1" cellpadding="1" width="100%" bgcolor="#d5d6e4" border="0">
                                                    <tr id="Tr1" runat="server">
                                                        <td style="width: 20px; height: 20px" valign="top" align="left">
                                                        </td>
                                                        <td style="width: 20px; height: 20px" align="right">
                                                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 120px" colspan="3">
                                                            <div id="scroller" style="overflow: auto; width: 100%; height: 137px" align="center">
                                                                <table id="Table8" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td style="width: 15%" nowrap>
                                                                        </td>
                                                                        <td style="width: 28%" nowrap align="center" colspan="4">
                                                                            <asp:Label ID="Label3" runat="server"  BackColor="#D5D6E4">BEGIN_DATE</asp:Label></td>
                                                                        <td style="width: 28%" nowrap align="center">
                                                                            <asp:Label ID="Label2" runat="server"  BackColor="#D5D6E4">END_DATE</asp:Label></td>
                                                                        <td nowrap align="center">
                                                                            <asp:Label ID="gridTerm" runat="server"  BackColor="#D5D6E4">TERM</asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                                <asp:DataGrid ID="gridEndorseCov" runat="server" Width="100%" BackColor="#DEE3E7"
                                                                    Height="8px" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px"
                                                                    BorderColor="#999999" CellPadding="1" AllowPaging="True">
                                                                    <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue">
                                                                    </SelectedItemStyle>
                                                                    <EditItemStyle Wrap="False"></EditItemStyle>
                                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                    <HeaderStyle></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn HeaderText="Coverage_Type">
                                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="BEFORE">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="AFTER">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="BEFORE">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="AFTER">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="BEFORE">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="AFTER">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                    </Columns>
                                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                        Mode="NumericPages"></PagerStyle>
                                                                </asp:DataGrid></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 28px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnWarranty" runat="server" Width="100%">
                                                <table id="tblWarranty" style="border-right: black 1px solid; border-top: black 1px solid;
                                                    border-left: black 1px solid; width: 100%; border-bottom: black 1px solid; height: 32px"
                                                    cellspacing="1" cellpadding="1" width="100%" bgcolor="#d5d6e4" border="0">
                                                    <tr id="Tr2" runat="server">
                                                        <td style="width: 20px; height: 20px" valign="top" align="left">
                                                        </td>
                                                        <td style="width: 20px; height: 20px" align="right">
                                                            <asp:Label ID="Label8" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 140px; width: 100%;" colspan="3">
                                                            <div id="Div1" style="overflow: auto; width: 100%; " align="center">
                                                                <table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td style="width: 15%" nowrap>
                                                                        </td>
                                                                        <td style="width: 28%" nowrap align="center" colspan="4">
                                                                            <asp:Label ID="Label7" runat="server"  BackColor="#D5D6E4">BEGIN_DATE</asp:Label></td>
                                                                        <td style="width: 28%" nowrap align="center">
                                                                            <asp:Label ID="Label6" runat="server"  BackColor="#D5D6E4">END_DATE</asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                                <asp:DataGrid ID="gridEndorseCov1" runat="server" Width="700px" BackColor="#DEE3E7"
                                                                    Height="8px" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px"
                                                                    BorderColor="#999999" CellPadding="1" AllowPaging="True">
                                                                    <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue">
                                                                    </SelectedItemStyle>
                                                                    <EditItemStyle Wrap="False"></EditItemStyle>
                                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                    <HeaderStyle></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn HeaderText="Coverage_Type">
                                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="BEFORE">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="AFTER">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="BEFORE">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn HeaderText="AFTER">
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="14%"></HeaderStyle>
                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                                        </asp:BoundColumn>
                                                                    </Columns>
                                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                        Mode="NumericPages"></PagerStyle>
                                                                </asp:DataGrid></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" height="28" style="width: 716px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <!--<tr>
                                        <td style="width: 100%; height: 1px" valign="top" align="center" colspan="2" height="1">
                                            <DIV id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 400px" align="center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" valign="top" align="left" colspan="2" height="100%">
                                            &nbsp;&nbsp;&nbsp;</td>
                                    </tr>-->
                                    <tr>
                                        <td style="width: 100%;" valign="top" align="left" colspan="2">
                                            <hr width="100%"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%; height: 100%;" valign="top" align="left" colspan="2">
                                            <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" Height="24px" Text="Back"></asp:Button></td>
                                    </tr>
                            </tbody> 
                            </table>

                            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                runat="server" designtimedragdrop="261"/></asp:Panel>
                </tr>
                <tr><td>&nbsp;</td></tr>
            </table>
 
        </form>

        <script>
	function resizeScroller(item)
			{
				var browseWidth, browseHeight;
				
				if (document.layers)
				{
					browseWidth=window.outerWidth;
					browseHeight=window.outerHeight;
				}
				if (document.all)
				{
					browseWidth=document.body.clientWidth;
					browseHeight=document.body.clientHeight;
				}
				
				if (screen.width == "800" && screen.height == "600") 
				{
					newHeight = browseHeight - 220;
				}
				else
				{
					newHeight = browseHeight - 975;
				}
				
				if (newHeight < 470)
				{
					newHeight = 470;
				}
					
				item.style.height = String(newHeight) + "px";
				
				return newHeight;
			}
			
			//resizeScroller(document.getElementById("scroller"));
        </script>

    </div>
</body>
</html>

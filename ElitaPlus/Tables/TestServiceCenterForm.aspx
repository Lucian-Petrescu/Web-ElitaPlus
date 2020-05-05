<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestServiceCenterForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TestServiceCenterForm" %>

<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (10/7/2004)  ******************** -->
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <script>
			function showProcessingFee(obj) 
			{ 
				var objLabelProcessingFee = document.getElementById('LabelProcessingFee');
				var objTextboxPROCESSING_FEE = document.getElementById('TextboxPROCESSING_FEE');
				objTextboxPROCESSING_FEE.value ="";
				if (obj.checked)
				{
					objLabelProcessingFee.style.display = '';  
					objTextboxPROCESSING_FEE.style.display = '';
				}
				else
				{
					objLabelProcessingFee.style.display = 'none'
					objTextboxPROCESSING_FEE.style.display = 'none';
				}
			}
				
    </script>

</head>
<body onresize="resizeForm(document.getElementById('scroller'));" leftmargin="0"
    topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout" border="0">
    <form id="Form1" method="post" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"  ScriptMode="Auto">        
            <Scripts>
                <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" /> 
            </Scripts>
       </asp:ScriptManager>
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
            border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <p>
                                    &nbsp;
                                    <asp:Label ID="Label1" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:
                                    <asp:Label ID="Label2" runat="server"  Cssclass="TITLELABELTEXT">Service_Center</asp:Label></p>
                            </td>
                            <td align="right" height="20">
                                <strong>*</strong>
                                <asp:Label ID="Label3" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tbody>
                <tr>
                    <td style="width: 914px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td valign="top" align="center" style="width: 914px">
                        <asp:Panel ID="WorkingPanel" runat="server" Width="98%" Height="98%">
                            <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                height: 100%" cellspacing="0" cellpadding="0" rules="cols" width="100%" align="center"
                                bgcolor="#fef9ea" border="0">
                                <tr>
                                    <td style="height: 4px" valign="top" align="center" colspan="4">
                                        <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 212px" valign="top">
                                        <div id="scroller" style="overflow: auto; width: 100%; height: 110%" align="center">
                                            <table id="Table1" style="width: 100%" cellspacing="1" cellpadding="0" width="100%"
                                                border="0">
                                                <asp:Panel ID="EditPanel_WRITE" runat="server" Height="100%" Width="100%">
                                                    <tr>
                                                        <td style="height: 12px" valign="middle" nowrap align="right" width="1%">
                                                            <asp:Label ID="moCountryLabel" runat="server" Font-Bold="false">Country</asp:Label>:</td>
                                                        <td style="width: 50%; height: 12px" valign="middle" align="left">
                                                            &nbsp;
                                                            <asp:TextBox ID="moCountryLabel_NO_TRANSLATE" TabIndex="1" runat="server" Width="160px"
                                                                CssClass="FLATTEXTBOX" Enabled="False"></asp:TextBox>
                                                            <asp:DropDownList ID="moCountryDrop" TabIndex="1" runat="server" AutoPostBack="True">
                                                            </asp:DropDownList></td>
                                                        <td style="width: 1%; height: 12px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelStatusCode" runat="server" Font-Bold="false">STATUS</asp:Label></td>
                                                        <td style="width: 50%; height: 12px">
                                                            &nbsp;
                                                            <asp:DropDownList ID="cboStatusCode" TabIndex="5" runat="server" Width="15%" CssClass="FLATTEXTBOX"></asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelCode" runat="server" Font-Bold="false">SERVICE_CENTER_CODE</asp:Label></td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxCode" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="160px"></asp:TextBox></td>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelDescription" runat="server" Font-Bold="false">SERVICE_CENTER_NAME</asp:Label></td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxDescription" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 14px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelDateAdded" runat="server" Font-Bold="false">DATE_ADDED</asp:Label></td>
                                                        <td style="width: 50%; height: 14px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxDateAdded" TabIndex="3" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="160px"></asp:TextBox></td>
                                                        <td style="width: 1%; height: 14px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelPriceGroupId" runat="server" Font-Bold="false">PRICE_GROUP</asp:Label></td>
                                                        <td style="width: 50%; height: 14px">
                                                            &nbsp;
                                                            <asp:DropDownList ID="cboPriceGroupId" TabIndex="7" runat="server" Width="220px">
                                                            </asp:DropDownList></td>
                                                        <tr>
                                                            <td style="width: 1%; height: 11px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelDateLastMaintained" runat="server" Font-Bold="false">DATE_LAST_MAINTAINED</asp:Label></td>
                                                            <td style="width: 50%; height: 11px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxDateLastMaintained" TabIndex="4" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox></td>
                                                            <td style="width: 1%; height: 11px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelServiceGroupId" runat="server" Font-Bold="false">SERVICE_GROUP</asp:Label></td>
                                                            <td style="width: 50%; height: 11px">
                                                                &nbsp;
                                                                <asp:DropDownList ID="cboServiceGroupId" TabIndex="8" runat="server" Width="220px">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 14px" valign="middle" nowrap align="right">
                                                        </td>
                                                        <td style="width: 50%; height: 14px">
                                                            &nbsp;</td>
                                                        <td style="width: 1%; height: 14px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelBusinessHours" runat="server" Font-Bold="false">BUSINESS_HOURS</asp:Label></td>
                                                        <td style="width: 50%; height: 14px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxBusinessHours" TabIndex="9" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 2px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelFtpAddress" runat="server" Font-Bold="false">FTP_ADDRESS</asp:Label></td>
                                                        <td style="width: 50%; height: 14px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxFtpAddress" TabIndex="10" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="160px"></asp:TextBox></td>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelOwnerName" runat="server" Font-Bold="false">OWNER_NAME</asp:Label></td>
                                                        <td style="width: 50%; height: 14px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxOwnerName" TabIndex="17" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelTaxId" runat="server" Font-Bold="false">TAX_ID</asp:Label></td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxTaxId" TabIndex="11" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="160px"></asp:TextBox></td>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelContactName" runat="server" Font-Bold="false">CONTACT_NAME</asp:Label></td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxContactName" TabIndex="18" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelServiceWarrantyDays" runat="server" Font-Bold="false">SERVICE_WARRANTY_DAYS</asp:Label></td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxServiceWarrantyDays" TabIndex="12" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="15%"></asp:TextBox></td>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelPhone1" runat="server" Font-Bold="false">PHONE1</asp:Label></td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxPhone1" TabIndex="19" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelRatingCode" runat="server" Font-Bold="false">RATING_CODE</asp:Label></td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxRatingCode" TabIndex="13" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="15%"></asp:TextBox></td>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelPhone2" runat="server" Font-Bold="false">PHONE2</asp:Label></td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxPhone2" TabIndex="20" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelIvaResponsible" runat="server" Font-Bold="false">IVA_Responsible</asp:Label>:</td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:CheckBox ID="CheckBoxIvaResponsible" TabIndex="14" runat="server"></asp:CheckBox></td>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelFax" runat="server" Font-Bold="false">FAX</asp:Label></td>
                                                        <td width="50%">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxFax" TabIndex="21" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 20px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelShipping" runat="server" Font-Bold="false">SHIPPING</asp:Label>:</td>
                                                        <td style="width: 50%; height: 20px">
                                                            &nbsp;
                                                            <asp:CheckBox ID="CheckBoxShipping" TabIndex="15" runat="server"></asp:CheckBox></td>
                                                        <td style="width: 1%; height: 20px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelDefaultToEmail" runat="server" Font-Bold="false">Default_To_Email</asp:Label>:</td>
                                                        <td style="width: 50%; height: 20px">
                                                            &nbsp;
                                                            <asp:CheckBox ID="CheckBoxDefaultToEmail" TabIndex="22" runat="server"></asp:CheckBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 15px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelProcessingFee" runat="server" Font-Bold="false">PROCESSING_FEE</asp:Label></td>
                                                        <td style="width: 50%; height: 15px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxPROCESSING_FEE" TabIndex="16" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="160px"></asp:TextBox></td>
                                                        <td style="width: 1%; height: 15px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelEmail" runat="server" Font-Bold="false">EMAIL</asp:Label></td>
                                                        <td style="width: 50%; height: 20px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxEmail" TabIndex="23" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 15px" valign="middle" nowrap align="right">
                                                        </td>
                                                        <td style="width: 50%; height: 15px">
                                                        </td>
                                                        <td style="width: 1%; height: 15px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelCcEmail" runat="server" Font-Bold="false">CC_EMAIL</asp:Label></td>
                                                        <td style="width: 50%; height: 20px">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxCcEmail" TabIndex="23" runat="server" CssClass="FLATTEXTBOX"
                                                                Width="220px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 2px" size="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelServiceTypeId" runat="server" Font-Bold="false">SERVICE_TYPE</asp:Label></td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                            <asp:DropDownList ID="cboServiceTypeId" TabIndex="24" runat="server" Width="160px">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtOriginalDealer" runat="server" Visible="False"></asp:TextBox></td>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelMasterCenterId" runat="server" Font-Bold="false">MASTER_CENTER</asp:Label></td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                            <asp:DropDownList ID="cboMasterCenterId" TabIndex="26" runat="server" Width="220px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="PaymentMethodDrpLabel" runat="server" Font-Bold="false">PAYMENT_METHOD</asp:Label></td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                            <asp:DropDownList ID="cboPaymentMethodId" TabIndex="25" runat="server" AutoPostBack="False"
                                                                Width="160px">
                                                            </asp:DropDownList></td>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelLoanerCenterId" runat="server" Font-Bold="false">LOANER_CENTER</asp:Label></td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                            <asp:DropDownList ID="cboLoanerCenterId" TabIndex="27" runat="server" Width="220px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                        </td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                            <asp:Label ID="lblDealer" runat="server" Font-Bold="false">ORIGINAL_DEALER</asp:Label></td>
                                                        <td style="width: 50%; height: 17px">
                                                            &nbsp;
                                                            <asp:DropDownList ID="cboOriginalDealer" TabIndex="28" runat="server" Width="220px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                        </td>
                                                        <td style="width: 50%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                        </td>
                                                        <td style="width: 50%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <uc1:UserControlBankInfo ID="moBankInfoController" runat="server"></uc1:UserControlBankInfo>
                                                    <tr>
                                                        <td colspan="4" height="5">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 2px" size="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" height="5">
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td valign="top" align="left" width="100%" colspan="4">
                                                        <mytab:TabStrip ID="tsHoriz"  runat="server" TargetID="mpHoriz"
                                                            SepDefaultStyle="border-bottom:solid 1px #000000;" TabSelectedStyle="border:solid 1px black;border-bottom:none;background:#d5d6e4;padding-left:7px;padding-right:7px;"
                                                            TabHoverStyle="background:#faecc2" TabDefaultStyle="border:solid 1px black;background:#f1f1f1;padding-top:2px;padding-bottom:2px;padding-left:7px;padding-right:7px;">
                                                            <mytab:Tab ID="tab_moAddressTabPanel_WRITE" Text="Address" DefaultImageUrl=""></mytab:Tab>
                                                            <mytab:Tab ID="tab_moMfg_Auth_Svc_CtrTabPanel_WRITE" Text="Mfg_Auth_Svc_Ctr" DefaultImageUrl="">
                                                            </mytab:Tab>
                                                            <mytab:Tab ID="tab_moCovered_DistrictTabPanel_WRITE" Text="Covered_District" DefaultImageUrl="">
                                                            </mytab:Tab>
                                                            <mytab:Tab ID="tab_moDealer_PreferredTabPanel_WRITE" Text="Dealer_Preferred" DefaultImageUrl="">
                                                            </mytab:Tab>
                                                            <mytab:Tab ID="tab_moSeviceNetworkTabPanel_WRITE" Text="Service_Network" DefaultImageUrl="">
                                                            </mytab:Tab>
                                                            <mytab:Tab ID="tab_moCommentsInformationTabPanel_WRITE" Text="Comment" DefaultImageUrl="">
                                                            </mytab:Tab>
                                                        </mytab:TabStrip>
                                                        <mytab:MultiPage ID="mpHoriz" Style="border-right: #000000 1px solid; padding-right: 5px;
                                                            border-top: medium none; padding-left: 5px; background: #d5d6e4; padding-bottom: 5px;
                                                            border-left: #000000 1px solid; padding-top: 5px; border-bottom: #000000 1px solid"
                                                            runat="server" Width="100%">
                                                            <mytab:PAGEVIEW>
                                                                <asp:Panel ID="moAddressTabPanel_WRITE" runat="server" Width="100%">
                                                                    <table id="tblAddress" style="border-right: #999999 0px solid; border-top: #999999 0px solid;
                                                                        border-left: #999999 0px solid; border-bottom: #999999 0px solid" cellspacing="4"
                                                                        cellpadding="4" rules="cols" height="100%" width="100%" background="" border="0">
                                                                        <tr>
                                                                            <td align="left" colspan="1" height="100%">
                                                                                <uc1:UserControlAddress ID="moAddressController" runat="server"></uc1:UserControlAddress>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </mytab:PAGEVIEW>
                                                            <mytab:PAGEVIEW>
                                                                <!-- Tab begin -->
                                                                <asp:Panel ID="moMfg_Auth_Svc_CtrTabPanel_WRITE" runat="server" Width="100%">
                                                                    <div id="scroller1" style="overflow: auto; width: 99.53%; height: 90%" align="center">
                                                                        <table id="tblMfgDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                                                                            rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top" colspan="2" width="50%">
                                                                                    <table id="TableMfg" cellspacing="1" cellpadding="1" width="100%" border="0" designtimedragdrop="200">
                                                                                        <tr>
                                                                                            <td align="center" colspan="2">
                                                                                                <uc1:UserControlAvailableSelected ID="UserControlAvailableSelectedManufacturers"
                                                                                                    runat="server"></uc1:UserControlAvailableSelected>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <!-- Tab end -->
                                                                </asp:Panel>
                                                            </mytab:PAGEVIEW>
                                                            <mytab:PAGEVIEW>
                                                                <!-- Tab begin -->
                                                                <asp:Panel ID="moCovered_DistrictTabPanel_WRITE" runat="server" Width="100%">
                                                                    <div id="scroller2" style="overflow: auto; width: 99.53%; height: 150px" align="center">
                                                                        <table id="tblDstDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                                                                            rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top" colspan="2" width="50%">
                                                                                    <table id="TableDst" cellspacing="1" cellpadding="1" width="100%" border="0" designtimedragdrop="200">
                                                                                        <tr>
                                                                                            <td align="center" colspan="2">
                                                                                                <uc1:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDistricts" runat="server">
                                                                                                </uc1:UserControlAvailableSelected>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <!-- Tab end -->
                                                                </asp:Panel>
                                                            </mytab:PAGEVIEW>
                                                            <mytab:PAGEVIEW>
                                                                <!-- Tab begin -->
                                                                <asp:Panel ID="moDealer_PreferredTabPanel_WRITE" runat="server" Width="100%">
                                                                    <div id="scroller3" style="overflow: auto; width: 99.53%; height: 150px" align="center">
                                                                        <table id="tblDlrDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                                                                            rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top" colspan="2" width="50%">
                                                                                    <table id="TableDlr" cellspacing="1" cellpadding="1" width="100%" border="0" designtimedragdrop="200">
                                                                                        <tr>
                                                                                            <td align="center" colspan="2">
                                                                                                <uc1:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDealers" runat="server">
                                                                                                </uc1:UserControlAvailableSelected>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <!-- Tab end -->
                                                                </asp:Panel>
                                                            </mytab:PAGEVIEW>
                                                            <mytab:PAGEVIEW>
                                                                <!-- Tab begin -->
                                                                <asp:Panel ID="moServiceNetworkTabPanel_WRITE" runat="server" Width="100%">
                                                                    <div id="scroller4" style="overflow: auto; width: 99.53%; height: 150px" align="center">
                                                                        <table id="tblExcDlrDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                                                                            rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top" width="50%" colspan="2">
                                                                                    <table id="TableExcDlr" cellspacing="1" cellpadding="1" width="100%" border="0" designtimedragdrop="200">
                                                                                        <tr>
                                                                                            <td align="center" colspan="2">
                                                                                                <uc1:UserControlAvailableSelected ID="UsercontrolavailableselectedServiceNetworks"
                                                                                                    runat="server"></uc1:UserControlAvailableSelected>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <!-- Tab end -->
                                                                </asp:Panel>
                                                            </mytab:PAGEVIEW>
                                                            <mytab:PAGEVIEW>
                                                                <!-- Tab begin -->
                                                                <asp:Panel ID="moCommentsInformationTabPanel_WRITE" runat="server" Width="100%">
                                                                    <div id="scroller5" style="overflow: auto; width: 99.53%; height: 150px" align="center">
                                                                        <table id="tblCommentsInformation" style="width: 100%; height: 90%" cellspacing="2"
                                                                            cellpadding="2" rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top" width="50%" colspan="2">
                                                                                    <table id="Table5" cellspacing="1" cellpadding="1" width="100%" border="0" designtimedragdrop="200">
                                                                                        <tr>
                                                                                            <td valign="top" align="right" width="10%">
                                                                                                <asp:Label ID="LabelComment1" runat="server" Font-Bold="false">COMMENT</asp:Label>&nbsp;
                                                                                            </td>
                                                                                            <td width="90%">
                                                                                                <asp:TextBox ID="TextboxComment" runat="server"  TabIndex="41"
                                                                                                     Width="99%" TextMode="MultiLine" Rows="9"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <!-- Tab end -->
                                                                </asp:Panel>
                                                            </mytab:PAGEVIEW>
                                                        </mytab:MultiPage></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr style="width: 100%; height: 2px" size="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" nowrap align="left" height="20">
                                        &nbsp;
                                        <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" TabIndex="42" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                        <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" TabIndex="43" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                        <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" TabIndex="44" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                        <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" TabIndex="45" runat="server" Font-Bold="false"
                                            Width="81px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                        <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" TabIndex="46" runat="server" Height="20px"
                                            Width="120px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" TabIndex="47" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="5">
                                    </td>
                                </tr>
                            </table>
                        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server" designtimedragdrop="261"/>
                        </asp:Panel>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>

    <script>
		
			function resizeForm(item)
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
					newHeight = browseHeight - 175;
				}
				
				if (newHeight < 470)
				{
					newHeight = 470;
				}
					
				item.style.height = String(newHeight) + "px";
				
				return newHeight;
			}
			
			resizeForm(document.getElementById("scroller"));
				
    </script>

</body>
</html>

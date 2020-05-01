<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertAddNewItemForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertAddNewItemForm" %>

<%@ Register TagPrefix="uc1" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (11/9/2004)  ******************** -->
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; width: 98%; border-bottom: black 1px solid; height: 20px"
        cellspacing="0" cellpadding="0" width="869" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <asp:Label ID="LabelTables" runat="server"  CssClass="TITLELABEL">Certificates</asp:Label>:
                            <asp:Label ID="Label40" runat="server"  CssClass="TITLELABELTEXT">New_Item</asp:Label>
                        </td>
                        <td align="right" height="20">
                            <strong>*</strong>
                            <asp:Label ID="Label9" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%" Width="98%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                        height: 98%" cellspacing="0" cellpadding="6" width="100%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td valign="top" height="1">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TableFixed" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td width="100%">
                                            <uc1:UserControlCertificateInfo ID="moCertificateInfoController" runat="server"></uc1:UserControlCertificateInfo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%">
                                            <hr style="height: 1px"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left">
                                <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                                    <div id="scroller" style="border-right: 1px solid; border-top: 1px solid; overflow: auto;
                                        border-left: 1px solid; width: 100%; border-bottom: 1px solid; background-color: #f1f1f1"
                                        align="center">
                                        <table id="Table1" style="width: 100%" cellspacing="0" cellpadding="2" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 10px">
                                                    </td>
                                                    <td colspan="4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="width: 1%" valign="middle" nowrap align="right">
                                                        <asp:Label ID="LabelCoverageType" runat="server" Font-Bold="false">Coverage_Type</asp:Label>
                                                    </td>
                                                    <td style="width: 50%" valign="middle" align="left">
                                                        &nbsp;
                                                            <asp:DropDownList ID="cboCoverageType" TabIndex="1" runat="server" Width="40%" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                    </td>
                                                   
                                                     <td style="width: 1%" valign="middle" align="right">
                                                        <asp:Label ID="LabelMakeId" runat="server" Font-Bold="false">Make</asp:Label>
                                                    </td>
                                                    <td style="width: 50%" valign="middle" nowrap align="left">
                                                        &nbsp;
                                                        <asp:DropDownList ID="cboManufacturerId" TabIndex="11" runat="server">
                                                        </asp:DropDownList>
                                                    </td></tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    <td style="width: 1%" valign="middle" nowrap align="right">
                                                        <asp:Label ID="LabelBeginDate" runat="server" Font-Bold="false">Begin_Date</asp:Label>
                                                    </td>
                                                    <td style="width: 50%" valign="middle" nowrap align="left">
                                                        &nbsp;
                                                        <asp:TextBox ID="TextboxBeginDate" TabIndex="2" runat="server" Width="40%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                    </td>
                                                        
                                                         <td style="width: 1%" valign="middle" align="right">
                                                            <asp:Label ID="LabelModel" runat="server" Font-Bold="false">Model</asp:Label>
                                                        </td>
                                                        <td style="width: 50%" valign="middle" align="left">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxModel" TabIndex="12" runat="server" Width="75%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                        </td></tr>
                                                     <tr>
                                                     <td>
                                                     </td>
                                                     <td style="width: 1%" valign="middle" nowrap align="right">
                                                            <asp:Label ID="LabelEndDate" runat="server" Font-Bold="false">End_Date</asp:Label>
                                                      </td>
                                                      <td style="width: 50%" valign="middle" align="left">
                                                            &nbsp;
                                                            <asp:TextBox ID="TextboxEndDate" TabIndex="3" runat="server" Width="40%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButtonEndDate" TabIndex="75" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                      </td>
                                                            
                                                             <td style="width: 1%; height: 22px" valign="middle" align="right">
                                                                <asp:Label ID="LabelDealerItemDesc" runat="server" Font-Bold="false">Description:</asp:Label>
                                                            </td>
                                                            <td style="width: 50%" valign="middle" align="left">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxDealerItemDesc" TabIndex="13" runat="server" Width="75%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td></tr>
                                                        <tr>
                                                                <td>
                                                                </td>
                                                               <td style="width: 1%" valign="middle" nowrap align="right">
                                                                    <asp:Label ID="LabelDateAdded" runat="server" Font-Bold="false">Date_Added</asp:Label>:
                                                                </td>
                                                                <td style="width: 50%" nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="TextboxDateAdded" TabIndex="4" runat="server" Width="40%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                </td>
                                                                
                                                                <td id="Td1" style="width: 1%" valign="middle" align="right" runat="server">
                                                                    
                                                                </td>
                                                                <td style="width: 50%" valign="middle" align="left">
                                                                    &nbsp;
                                                                </td></tr>
                                                        <tr>
                                                                    <td>
                                                                    </td>
                                                               <td style="width: 1%" valign="middle" nowrap align="right">
                                                                    <asp:Label ID="LabelRiskType" runat="server" Font-Bold="false">Risk_Type</asp:Label>
                                                                </td>
                                                                <td style="width: 50%" nowrap>
                                                                    &nbsp;
                                                                    <asp:DropDownList ID="cboRiskTypeId" TabIndex="5" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>

                                                                     <td style="width: 1%" valign="middle" align="right">
                                                                        <asp:Label ID="LabelSerialNumber" runat="server" Font-Bold="false" Width="124px">Serial_Number</asp:Label>
                                                                    </td>
                                                                    <td style="width: 50%" nowrap>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="TextboxSerialNumber" TabIndex="14" runat="server" Width="75%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                    </td></tr>
                                                       <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                                            <asp:Label ID="LabelMethodOfRepair" runat="server" Font-Bold="false">Method_Of_Repair</asp:Label>:
                                                                        </td>
                                                                        <td style="width: 50%" valign="middle" align="left">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="TextboxMethodOfRepair" TabIndex="6" runat="server" Width="75%"
                                                                                CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 1%" valign="middle" nowrap align="right">
                                                                            <asp:Label ID="LabelInvNum" runat="server" Font-Bold="false">Invoice_Number</asp:Label>:
                                                                        </td>
                                                                        <td style="width: 50%" valign="middle" align="left">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="TextboxInvNum" TabIndex="15" runat="server" Width="75%" CssClass="FLATTEXTBOX" MaxLength="30"></asp:TextBox>
                                                                        </td></tr>
                                                       <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                                                <asp:Label ID="LabelProductCode" runat="server" Font-Bold="false">Product_Code</asp:Label>:
                                                                            </td>
                                                                            <td style="width: 50%" valign="middle" align="left">
                                                                                &nbsp;
                                                                                <asp:TextBox ID="TextboxProductCode" TabIndex="7" runat="server" Width="75%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                                                <asp:Label ID="LabelComputeDeductible" runat="server" Font-Bold="false">Compute_Deductible_Based_On</asp:Label>:
                                                                            </td>
                                                                            <td style="width: 50%" valign="middle" align="left">
                                                                                &nbsp;
                                                                                <asp:TextBox ID="TextboxComputeDeductible" TabIndex="16" runat="server" Width="75%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                       <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                                                <asp:Label ID="LabelLiabilityLimit" runat="server" Font-Bold="false">Liability_Limit</asp:Label>:
                                                                            </td>
                                                                            <td style="width: 50%" valign="middle" align="left">
                                                                                &nbsp;
                                                                                <asp:TextBox ID="TextboxLiabilityLimit" TabIndex="8" runat="server" Width="75%"
                                                                                    CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                                                <asp:Label ID="LabelDeductible" runat="server" Font-Bold="false">Deductible</asp:Label>:
                                                                            </td>
                                                                            <td style="width: 50%" valign="middle" align="left">
                                                                                &nbsp;
                                                                                <asp:TextBox ID="TextboxDeductible" TabIndex="16" runat="server" Width="75%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                       <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td align="right" nowrap="nowrap" style="width: 1%" valign="middle">
                                                                                <asp:Label ID="labelRepairDiscountPct" runat="server" Font-Bold="False">REPAIR_DISCOUNT_PCT</asp:Label>:
                                                                            </td>
                                                                            <td align="left" style="width: 50%" valign="middle">
                                                                                &nbsp;
                                                                                <asp:TextBox ID="TextboxRepairDiscountPct" runat="server" CssClass="FLATTEXTBOX"
                                                                                    TabIndex="9" Width="75%"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 1%" valign="middle" nowrap align="right">
                                                                                <asp:Label ID="LabelDeductiblePercent" runat="server" Font-Bold="false">Deductible_Percent</asp:Label>:
                                                                            </td>
                                                                            <td style="width: 50%" valign="middle" align="left">
                                                                                &nbsp;
                                                                                <asp:TextBox ID="TextboxDeductiblePercent" TabIndex="17" runat="server" Width="75%"
                                                                                    CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                            </td>
                                                       </tr>
                                                       <tr>
                                                                            <td style="height: 16px">
                                                                            </td>
                                                                            <td align="right" nowrap="nowrap" style="width: 1%; height: 16px" valign="middle">
                                                                                <asp:Label ID="labelReplacementDiscountPct" runat="server" Font-Bold="False">REPLACEMENT_DISCOUNT_PCT</asp:Label>:
                                                                            </td>
                                                                            <td align="left" style="width: 50%; height: 16px" valign="middle">
                                                                                &nbsp;
                                                                                <asp:TextBox ID="TextboxReplacementDiscountPct" runat="server" CssClass="FLATTEXTBOX"
                                                                                    TabIndex="10" Width="75%"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" nowrap="nowrap" style="width: 1%; height: 16px" valign="middle">
                                                                            </td>
                                                                            <td align="left" style="width: 50%; height: 16px" valign="middle">
                                                                            </td>
                                                       </tr>
                                                       <tr>                                                                   
                                                                            <td style="width: 10px">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                &nbsp;
                                                                            </td>
                                                      </tr>
                                              </tbody>
                                            </table>
                                     </div>
                                </asp:Panel>
                             </td>                               
                        </tr>
                       </table>
                   </asp:Panel>
                </td>
          </tr>
        <tr>
                <td width="100%">
                    <hr style="height: 1px"/>
                </td>
            </tr>
        <tr height="20%">
            <td valign="bottom" nowrap align="left" height="20">&nbsp;&nbsp;
                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                    Width="80px" CssClass="FLATBUTTON" Text="Back" Height="20px"></asp:Button>&nbsp;
                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                    cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                    Width="80px" CssClass="FLATBUTTON" Text="Save" Height="20px"></asp:Button>&nbsp;
                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                    cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                    Width="80px" CssClass="FLATBUTTON" Text="Undo" Height="20px"></asp:Button>&nbsp;
            </td>
          </tr>
            <tr>                                                                   
              <td colspan="1">
                &nbsp;
            </td>
        </tr>
     </table>
     <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server"/>
    </form>
    <!--
		<script>
			resizeForm(document.getElementById("scroller"));
		</script>
		onresize="resizeForm(document.getElementById('scroller'));"
		-->
</body>
</html>

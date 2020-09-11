<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimAuthDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimAuthDetailForm" %>

<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>RegionForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/Tabs.js" > </script> 
    <script language="javascript" type="text/javascript">
			
			function trim(str){
				s = str.replace(/^(\s)*/, '');
			    s = s.replace(/(\s)*$/, '');
			    return s;
			}
			
			function round_num(number,x) { // function added by Oscar on 06/01/2005 
				x = (!x ? 2 : x);
				return Math.round(number*Math.pow(10,x))/Math.pow(10,x);
            }
			
			function isPointFormat(){
			    return IsPointFormat;
			}
			
			function FormatToDecimal(value){ 
		     	if (trim(value) == "")
			        value = 0;
		
				var value = parseFloat(value) + 0.0001;
				var svalue = value.toString();
				var index = svalue.indexOf(".");
				var result = svalue.substring(0, index+3);
				return result;
			}
		    
			function setJsFormat(value){
			    return value.replace(',','.');
			}
			
			function setCultureFormat(value){
				value = FormatToDecimal(value.toString());
				if (isPointFormat() != 1){
				  	value = value.replace('.',',');					
				}
				
				return value;
			}
			
			function setClientCultureFormat(obj, textBoxId, laborVal, partsVal, svcChargeVal, tripAmtVal, otherAmtVal, shippingAmtVal, diagnosticsAmtVal, dispositionAmtVal, totalClaimTaxAmountAmtVal)
            {             
			    PageMethods.SetClientCultureFormat(textBoxId, obj.value, laborVal, partsVal, svcChargeVal, tripAmtVal, otherAmtVal, shippingAmtVal, diagnosticsAmtVal, dispositionAmtVal, totalClaimTaxAmountAmtVal, OnComplete);
            }

			function computeTaxAmtByComputeMethod(amount, TaxRate, ComputeMethodCode) {
			    var taxAmount = 0;
			    if (amount > 0) {
			        if (ComputeMethodCode == 'N')
			            taxAmount = round_num(((amount * TaxRate) / 100), 2);
			        else {
			            var gross = 0;
			            gross = round_num((amount) / (1.0 + TaxRate), 2);
			            if (gross != 0) {
			                taxAmount = round_num(((amount / gross) - 1), 2);
			            }
			        }
			    }

			    return taxAmount;
			}

            function OnComplete(result)
            {   
                var ar = result.split("|");
                var obj = document.getElementById(ar[0]);
                obj.value = ar[1];
                var subTotal = ar[2];
                var totalAmt = ar[3];
                var totalTaxAmt = ar[4];
						
				document.getElementById("txtSubTotal").innerText = subTotal
				document.getElementById("txtTotal").innerText = totalAmt
				document.getElementById("txtTotalTaxAmount").innerText = totalTaxAmt
            }
				    
			function doAmtCalc(obj, textBoxId) 
            {
			    //debugger;
				var labor = document.getElementById("txtLabor")
				var parts = document.getElementById("txtParts")
				var svcCharge = document.getElementById("txtServiceCharge")
				var tripAmt = document.getElementById("txtTripAmt")
				var otherAmt = document.getElementById("txtOtherAmt")
				var shippingAmt = document.getElementById("txtShippingAmt")
				var diagnosticsAmt = document.getElementById("txtDiagnostics")
				var dispositionAmt = document.getElementById("txtDisposition")

				var hdTaxRateClaimDiagnostics = document.getElementById("hdTaxRateClaimDiagnostics");
				var hdTaxRateClaimOther = document.getElementById("hdTaxRateClaimOther");
				var hdTaxRateClaimDisposition = document.getElementById("hdTaxRateClaimDisposition");
				var hdTaxRateClaimLabor = document.getElementById("hdTaxRateClaimLabor");
				var hdTaxRateClaimParts = document.getElementById("hdTaxRateClaimParts");
				var hdTaxRateClaimShipping = document.getElementById("hdTaxRateClaimShipping");
				var hdTaxRateClaimService = document.getElementById("hdTaxRateClaimService");
				var hdTaxRateClaimTrip = document.getElementById("hdTaxRateClaimTrip");
				
				var laborVal = 0
				var partsVal = 0
				var svcChargeVal = 0
				var tripAmtVal = 0
				var otherAmtVal = 0
				var shippingAmtVal = 0
				var diagnosticsAmtVal = 0
				var dispositionAmtVal = 0

				if (labor.value.length != 0)
				{
					laborVal = labor.value
				}
				if (parts.value.length != 0)
				{
					partsVal = parts.value
				}
				if (svcCharge.value.length != 0)
				{
					svcChargeVal = svcCharge.value
				}
				if (tripAmt.value.length != 0)
				{
					tripAmtVal = tripAmt.value
				}
				if (otherAmt.value.length != 0)
				{
					otherAmtVal = otherAmt.value
				}
				if (shippingAmt.value.length != 0)
				{
					shippingAmtVal = shippingAmt.value
				}
				if (diagnosticsAmt.value.length != 0) {
				    diagnosticsAmtVal = diagnosticsAmt.value
				}
				if (dispositionAmt.value.length != 0) {
				    dispositionAmtVal = dispositionAmt.value
				}

				var bClaimTaxRatesExist = false;
				var totalClaimTaxAmountAmtVal;

				if (hdTaxRateClaimDiagnostics.value > 0 || hdTaxRateClaimOther.value > 0 || hdTaxRateClaimDisposition.value > 0 ||
                    hdTaxRateClaimLabor.value > 0 || hdTaxRateClaimParts.value > 0 || hdTaxRateClaimShipping.value > 0 ||
                    hdTaxRateClaimService.value > 0 || hdTaxRateClaimTrip.value > 0) {
				    bClaimTaxRatesExist = true;}
				else
				{
				    totalClaimTaxAmountAmtVal = 0;
				}

				if (bClaimTaxRatesExist) {
				    //debugger;
                    var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
                    var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';

				    var hdLaborTaxAmt = document.getElementById("hdLaborTaxAmt");
				    var hdPartsTaxAmt = document.getElementById("hdPartsTaxAmt");
				    var hdServiceChargeTaxAmt = document.getElementById("hdServiceChargeTaxAmt");
				    var hdTripTaxAmt = document.getElementById("hdTripTaxAmt");
				    var hdShippingTaxAmt = document.getElementById("hdShippingTaxAmt");
				    var hdDispositionTaxAmt = document.getElementById("hdDispositionTaxAmt");
				    var hdDiagnosticsTaxAmt = document.getElementById("hdDiagnosticsTaxAmt");
				    var hdOtherTaxAmt = document.getElementById("hdOtherTaxAmt");
				    var hdTotalTaxAmt = document.getElementById("hdTotalTaxAmt");

				    var laborTaxAmt = document.getElementById("txtLaborTax");
				    var partsTaxAmt = document.getElementById("txtPartsTax");
				    var svcChargeTaxAmt = document.getElementById("txtServiceChargeTax");
				    var tripTaxAmt = document.getElementById("txtTripAmtTax");
				    var shippingTaxAmt = document.getElementById("txtShippingTax");
				    var otherTaxAmt = document.getElementById("txtOtherTax");
				    var DispositionTaxAmt = document.getElementById("txtDispositionTax");
				    var DiagnosticsTaxAmt = document.getElementById("txtDiagnosticsTax");
				    var laborTaxAmtVal = 0;
				    var partsTaxAmtVal = 0;
				    var svcChargeTaxAmtVal = 0;
				    var tripTaxAmtVal = 0;
				    var shippingTaxAmtVal = 0;
				    var otherTaxAmtVal = 0;
				    var DispositionTaxAmtVal = 0;
				    var DiagnosticsTaxAmtVal = 0;

				    if (laborTaxAmt.value.length != 0) {
				        laborTaxAmtVal = parseFloat(setJsFormat(laborTaxAmt.value, decSep));
				        laborTaxAmt.value = convertNumberToCulture(laborTaxAmtVal, decSep, groupSep);
				    }
				    if (partsTaxAmt.value.length != 0) {
				        partsTaxAmtVal = parseFloat(setJsFormat(partsTaxAmt.value, decSep));
				        partsTaxAmt.value = convertNumberToCulture(partsTaxAmtVal, decSep, groupSep);
				    }
				    if (svcChargeTaxAmt.value.length != 0) {
				        svcChargeTaxAmtVal = parseFloat(setJsFormat(svcChargeTaxAmt.value, decSep));
				        svcChargeTaxAmt.value = convertNumberToCulture(svcChargeTaxAmtVal, decSep, groupSep);
				    }
				    if (tripTaxAmt.value.length != 0) {
				        tripTaxAmtVal = parseFloat(setJsFormat(tripTaxAmt.value, decSep));
				        tripTaxAmt.value = convertNumberToCulture(tripTaxAmtVal, decSep, groupSep);
				    }
				    if (shippingTaxAmt.value.length != 0) {
				        shippingTaxAmtVal = parseFloat(setJsFormat(shippingTaxAmt.value, decSep));
				        shippingTaxAmt.value = convertNumberToCulture(shippingTaxAmtVal, decSep, groupSep);
				    }
				    if (otherTaxAmt.value.length != 0) {
				        otherTaxAmtVal = parseFloat(setJsFormat(otherTaxAmt.value, decSep));
				        otherTaxAmt.value = convertNumberToCulture(otherTaxAmtVal, decSep, groupSep);
				    }
				    if (DispositionTaxAmt.value.length != 0) {
				        DispositionTaxAmtVal = parseFloat(setJsFormat(DispositionTaxAmt.value, decSep));
				        DispositionTaxAmt.value = convertNumberToCulture(DispositionTaxAmtVal, decSep, groupSep);
				    }
				    if (DiagnosticsTaxAmt.value.length != 0) {
				        DiagnosticsTaxAmtVal = parseFloat(setJsFormat(DiagnosticsTaxAmt.value, decSep));
				        DiagnosticsTaxAmt.value = convertNumberToCulture(DiagnosticsTaxAmtVal, decSep, groupSep);
				    }

				    totalClaimTaxAmountAmtVal = laborTaxAmtVal + partsTaxAmtVal + svcChargeTaxAmtVal + tripTaxAmtVal + otherTaxAmtVal + shippingTaxAmtVal + DispositionTaxAmtVal + DiagnosticsTaxAmtVal;

				   // debugger;
				    // Labor Tax                        
				    if (hdTaxRateClaimLabor != 0) {
				        if (obj.id == "txtLabor" && hdTaxRateClaimLabor.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - laborTaxAmtVal;
				            var hdComputeMethodClaimLabor = document.getElementById("hdComputeMethodClaimLabor");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimLabor.value, decSep));
				            laborTaxAmtVal = computeTaxAmtByComputeMethod(laborVal, taxRateVal, hdComputeMethodClaimLabor.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + laborTaxAmtVal;
				            if (laborTaxAmtVal != 0) {
				                laborTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(laborTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtLaborTax").innerText = laborTaxAmtVal;
				            hdLaborTaxAmt.value = laborTaxAmtVal;
				        }
				    }

				    // Parts Tax
				    if (hdTaxRateClaimParts != 0) {
				        if (obj.id == "txtParts" && hdTaxRateClaimParts.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - partsTaxAmtVal;
				            var hdComputeMethodClaimParts = document.getElementById("hdComputeMethodClaimParts");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimParts.value, decSep));
				            partsTaxAmtVal = computeTaxAmtByComputeMethod(partsVal, taxRateVal, hdComputeMethodClaimParts.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + partsTaxAmtVal;
				            if (partsTaxAmtVal != 0) {
				                partsTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(partsTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtPartsTax").innerText = partsTaxAmtVal;
				            hdPartsTaxAmt.value = partsTaxAmtVal;
				        }
				    }

				    // Service Tax
				    if (hdTaxRateClaimService != 0) {
				        if (obj.id == "txtServiceCharge" && hdTaxRateClaimService.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - svcChargeTaxAmtVal;
				            var hdComputeMethodClaimService = document.getElementById("hdComputeMethodClaimService");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimService.value, decSep));
				            svcChargeTaxAmtVal = computeTaxAmtByComputeMethod(svcChargeVal, taxRateVal, hdComputeMethodClaimService.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + svcChargeTaxAmtVal;
				            if (svcChargeTaxAmtVal != 0) {
				                svcChargeTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(svcChargeTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtServiceChargeTax").innerText = svcChargeTaxAmtVal;
				            hdServiceChargeTaxAmt.value = svcChargeTaxAmtVal;
				        }
				    }

				    // Trip Tax
				    if (hdTaxRateClaimTrip != 0) {
				        if (obj.id == "txtTripAmt" && hdTaxRateClaimTrip.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - tripTaxAmtVal;
				            var hdComputeMethodClaimTrip = document.getElementById("hdComputeMethodClaimTrip");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimTrip.value, decSep));
				            tripTaxAmtVal = computeTaxAmtByComputeMethod(tripAmtVal, taxRateVal, hdComputeMethodClaimTrip.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + tripTaxAmtVal;
				            if (tripTaxAmtVal != 0) {
				                tripTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(tripTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtTripAmtTax").innerText = tripTaxAmtVal;
				            hdTripTaxAmt.value = tripTaxAmtVal;
				        }
				    }

				    // Shipping Tax
				    if (hdTaxRateClaimShipping != 0) {
				        if (obj.id == "txtShippingAmt" && hdTaxRateClaimShipping.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - shippingTaxAmtVal;
				            var hdComputeMethodClaimShipping = document.getElementById("hdComputeMethodClaimShipping");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimShipping.value, decSep));
				            shippingTaxAmtVal = computeTaxAmtByComputeMethod(shippingAmtVal, taxRateVal, hdComputeMethodClaimShipping.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + shippingTaxAmtVal;
				            if (shippingTaxAmtVal != 0) {
				                shippingTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(shippingTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtShippingTax").innerText = shippingTaxAmtVal;
				            hdShippingTaxAmt.value = shippingTaxAmtVal;
				        }
				    }

				    // Diagnostics Tax
				    if (hdTaxRateClaimDiagnostics != 0) {
				        if (obj.id == "txtDiagnostics" && hdTaxRateClaimDiagnostics.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - DiagnosticsTaxAmtVal;
				            var hdComputeMethodClaimDiagnostics = document.getElementById("hdComputeMethodClaimDiagnostics");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimDiagnostics.value, decSep));
				            DiagnosticsTaxAmtVal = computeTaxAmtByComputeMethod(diagnosticsAmtVal, taxRateVal, hdComputeMethodClaimDiagnostics.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + DiagnosticsTaxAmtVal;
				            if (DiagnosticsTaxAmtVal != 0) {
				                DiagnosticsTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(DiagnosticsTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtDiagnosticsTax").innerText = DiagnosticsTaxAmtVal;
				            hdDiagnosticsTaxAmt.value = DiagnosticsTaxAmtVal;
				        }
				    }

				    // Disposition Tax
				    if (hdTaxRateClaimDisposition != 0) {
				        if (obj.id == "txtDisposition" && hdTaxRateClaimDisposition.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - DispositionTaxAmtVal;
				            var hdComputeMethodClaimDisposition = document.getElementById("hdComputeMethodClaimDisposition");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimDisposition.value, decSep));
				            DispositionTaxAmtVal = computeTaxAmtByComputeMethod(dispositionAmtVal, taxRateVal, hdComputeMethodClaimDisposition.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + DispositionTaxAmtVal;
				            if (DispositionTaxAmtVal != 0) {
				                DispositionTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(DispositionTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtDispositionTax").innerText = DispositionTaxAmtVal;
				            hdDispositionTaxAmt.value = DispositionTaxAmtVal;
				        }
				    }

				    // Other Tax
				    if (hdTaxRateClaimOther != 0) {
				        if (obj.id == "txtOtherAmt" && hdTaxRateClaimOther.value > 0) {
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 - otherTaxAmtVal;
				            var hdComputeMethodClaimOther = document.getElementById("hdComputeMethodClaimOther");
				            var taxRateVal = parseFloat(setJsFormat(hdTaxRateClaimOther.value, decSep));
				            otherTaxAmtVal = computeTaxAmtByComputeMethod(otherAmtVal, taxRateVal, hdComputeMethodClaimOther.value);
				            totalClaimTaxAmountAmtVal = totalClaimTaxAmountAmtVal * 1 + otherTaxAmtVal;
				            if (otherTaxAmtVal != 0) {
				                otherTaxAmtVal = convertNumberToCulture(FormatToDecimal(round_num(otherTaxAmtVal, 2).toString()), decSep, groupSep);
				            }
				            document.getElementById("txtOtherTax").innerText = otherTaxAmtVal;
				            hdOtherTaxAmt.value = otherTaxAmtVal;
				        }
				    }

				    document.getElementById("txtTotalTaxAmount").innerText = convertNumberToCulture(FormatToDecimal(round_num(totalClaimTaxAmountAmtVal, 2).toString()), decSep, groupSep);
				    hdTotalTaxAmt.value = convertNumberToCulture(FormatToDecimal(round_num(totalClaimTaxAmountAmtVal, 2).toString()), decSep, groupSep);

				};

			    setClientCultureFormat(obj, textBoxId, laborVal, partsVal, svcChargeVal, tripAmtVal, otherAmtVal, shippingAmtVal, diagnosticsAmtVal, dispositionAmtVal, totalClaimTaxAmountAmtVal);

			   // document.getElementById("txtSubTotal").innerText = subTotal
			   // document.getElementById("txtTotal").innerText = totalAmt
			   // document.getElementById("txtTotalTaxAmount").innerText = totalTaxAmt
			} 
			 function toggleCheckBox(ctrl)
            {   
                document.getElementById(ctrl).checked = false;
            }
    </script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <asj:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="true">
    </asj:ScriptManager>
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            &nbsp;<asp:Label ID="TablesLabel" runat="server" CssClass="TITLELABEL">Claims</asp:Label>:
                            <asp:Label ID="MaintainAuthDetailLabel" runat="server" CssClass="TITLELABELTEXT">AUTH_DETAIL</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%" Width="98%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                        height: 100%" cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center"
                        bgcolor="#fef9ea" border="0" valign="top">
                        <tr>
                            <td align="center" width="75%" colspan="2" valign="top" height="1">
                                <uc1:ErrorController ID="ErrController" runat="server">
                                </uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table id="tblHeader" cellspacing="0" cellpadding="0" rules="cols" width="100%" align="center"
                                    bgcolor="#fef9ea" border="0">
                                    <tr>
                                        <td valign="bottom" nowrap align="left" colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 45%" align="left">
                                            <asp:Label ID="LabelCustomerName" runat="server">Customer_Name</asp:Label>:
                                        </td>
                                        <td style="width: 20%" align="left">
                                            <asp:Label ID="LabelClaimNumber" runat="server">Claim_Number</asp:Label>:
                                        </td>
                                        <td style="width: 35%" align="left">
                                            <asp:Label ID="LabelRiskGroup" runat="server">RISK_GROUP</asp:Label>:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:TextBox ID="TextboxCustomerName" Style="background-color: whitesmoke" TabIndex="1"
                                                runat="server" ReadOnly="True" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextboxClaimNumber" Style="background-color: whitesmoke" TabIndex="1"
                                                runat="server" ReadOnly="True" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextboxRiskGroup" Style="background-color: whitesmoke" TabIndex="1"
                                                runat="server" ReadOnly="True" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="bottom" nowrap align="left" colspan="3">
                                            <hr style="width: 100%; height: 1px" size="1">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left" width="100%" colspan="2" >
                                    <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnDisabledTab" runat="server" />
                                    <div class="dataContainer">
                                        <div id="tabs" class="style-tabs">
                                            <ul>
                                                <li><a href="#tabPartsInfo" rel="noopener noreferrer">
                                                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">PARTS   INFO</asp:Label></a></li>
                                                <li><a href="#tabsAuthDetaily" rel="noopener noreferrer">
                                                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">AUTH_DETAIL</asp:Label></a></li>
                                            </ul>

                                            <div id="tabPartsInfo">
                                                <asp:Panel ID="moPartsInfoTabPanel_WRITE" runat="server" Width="100%">
                                                    <div id="scroller1" style="overflow: auto; width: 99.53%; height: 90%" align="center">
                                                        <table id="tblPartsInfo" style="border-right: #999999 0px solid; border-top: #999999 0px solid; border-left: #999999 0px solid; border-bottom: #999999 0px solid; width: 100%; height: 178px"
                                                            cellspacing="4" cellpadding="4" rules="cols" background="" border="0">
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                                                                    AllowPaging="True" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False"
                                                                                    CssClass="DATAGRID">
                                                                                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                                                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                                                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                                                    <RowStyle CssClass="ROW"></RowStyle>
                                                                                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                                                    <Columns>
                                                                                        <asp:TemplateField ShowHeader="false">
                                                                                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                                                                                                    ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="DeleteButton_WRITE" Style="cursor: hand" runat="server" CommandName="DeleteRecord"
                                                                                                    ImageUrl="../Navigation/images/icons/trash.gif"></asp:ImageButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="False" HeaderText="Id">
                                                                                            <ItemTemplate>
                                                                                                &gt;
                                                                                        <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("parts_info_id"))%>'>
                                                                                        </asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="DescriptionLabel" runat="server" Visible="True" Text='<%# Container.DataItem("description")%>'>
                                                                                                </asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:DropDownList ID="DescriptionDropDownList" runat="server" Visible="True">
                                                                                                </asp:DropDownList>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="INSTOCK">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="InstockLabel" runat="server" Visible="True" Text='<%# Container.DataItem("in_stock_description")%>'>
                                                                                                </asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:DropDownList ID="InStockDropDownList" runat="server" Visible="True">
                                                                                                </asp:DropDownList>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="COST">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="CostLabel" runat="server" Visible="True" Text='<%# Container.DataItem("cost")%>'>
                                                                                                </asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:TextBox ID="CostTextBox" runat="server" Visible="True"></asp:TextBox>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 45%" align="right">
                                                                                <asp:Label ID="LabelTotalCost" runat="server">Total_Cost</asp:Label>:&nbsp;
                                                                        <asp:TextBox ID="TextTotalCost" Style="background-color: whitesmoke" runat="server"
                                                                            ReadOnly="True" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 45%" align="right">
                                                                                <asp:Label ID="LabelPartsTax1" runat="server">PARTS_TAX</asp:Label>:&nbsp;
                                                                        <asp:TextBox ID="TextPartsTax1" Style="background-color: whitesmoke" runat="server"
                                                                            ReadOnly="True" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left" valign="bottom">
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="right" colspan="2">
                                                                                <hr style="width: 100%; height: auto" size="1">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="btnNew_PI_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                    runat="server" Width="100px" CssClass="FLATBUTTON"
                                                                                    Text="New" Height="20px"></asp:Button>
                                                                                &nbsp;<asp:Button ID="btnSave_PI_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                    runat="server" Width="100px" CssClass="FLATBUTTON"
                                                                                    Text="Save" Height="20px"></asp:Button>
                                                                                &nbsp;<asp:Button ID="Cancel_PI_Button" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                    runat="server" Width="100px" CssClass="FLATBUTTON"
                                                                                    Text="Cancel" Height="20px"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                            <div id="tabsAuthDetaily">
                                                <table id="tblAuthDetail" style="border-right: #999999 0px solid; border-top: #999999 0px solid; border-left: #999999 0px solid; border-bottom: #999999 0px solid; width: 100%; height: 178px"
                                                    cellspacing="4" cellpadding="4" background="" border="0">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Panel ID="moAuthDetailTabPanel_WRITE" runat="server" Width="100%">
                                                                <table cellpadding="4" height="100%" width="100%" background="" border="0">
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right" width="20%" style="height: 18px">
                                                                            &nbsp;</td>
                                                                        <td valign="middle" align="left" width="20%" style="height: 18px">&nbsp;</td>
                                                                        <td width="50%">
                                                                            <asp:Label ID="LabelTaxType" runat="server">Tax_Type</asp:Label>
                                                                            &nbsp;<asp:Label ID="LabelTaxAmount" runat="server">Tax_Amount</asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" nowrap style="height: 18px" valign="middle" width="20%">
                                                                            <asp:Label ID="LabelLabor" runat="server">Labor</asp:Label>
                                                                            : </td>
                                                                        <td align="left" style="height: 18px" valign="middle" width="20%">&nbsp;
                                                                            <asp:TextBox ID="txtLabor" runat="server" CssClass="FLATTEXTBOX" ReadOnly="true" Style="text-align: right" TabIndex="6" Width="55%"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtLaborTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right" width="20%">
                                                                            <asp:Label ID="LabelParts" runat="server">Parts</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtParts" Style="text-align: right" TabIndex="7" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtPartsTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right" width="20%">
                                                                            <asp:Label ID="LabelSvcCharge" runat="server">Service_Charge</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtServiceCharge" Style="text-align: right" TabIndex="8" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtServiceChargeTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right" width="20%">
                                                                            <asp:Label ID="LabelTripAmount" runat="server">Trip_Amount</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtTripAmt" Style="text-align: right" TabIndex="9" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtTripAmtTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right" width="20%">
                                                                            <asp:Label ID="LabelShippingAmount" runat="server">SHIPPING_AMOUNT</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtShippingAmt" Style="text-align: right" TabIndex="9" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtShippingTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" nowrap valign="middle" width="20%">&nbsp;
                                                                            <asp:Label ID="LabelDiagnostics" runat="server">DIAGNOSTICS</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtDiagnostics" Style="text-align: right" TabIndex="9" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtDiagnosticsTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" nowrap valign="middle" width="20%">&nbsp;
                                                                            <asp:Label ID="LabelDisposition" runat="server">DISPOSITION</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtDisposition" Style="text-align: right" TabIndex="9" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtDispositionTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" align="right" width="15%">
                                                                            <asp:TextBox ID="txtOtherDesc" TabIndex="20" runat="server" CssClass="FLATTEXTBOX"
                                                                                Width="98%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtOtherAmt" Style="text-align: right" TabIndex="11" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <asp:TextBox ID="txtOtherTax" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="15%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" colspan="3">
                                                                            <hr style="width: 100%; height: 1px" size="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right" width="20%">
                                                                            <asp:Label ID="LabelSubTotal" runat="server">Sub_Total</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtSubTotal" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="31" runat="server" CssClass="FLATTEXTBOX" Width="55%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" nowrap valign="middle" width="20%">
                                                                            <asp:Label ID="LabelTotalTaxAmount" runat="server">TOTAL_TAX_AMOUNT</asp:Label>
                                                                            :</td>
                                                                        <td align="left" valign="middle" width="20%">&nbsp;
                                                                            <asp:TextBox ID="txtTotalTaxAmount" Style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid; border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; text-align: right"
                                                                        TabIndex="32" runat="server" CssClass="FLATTEXTBOX" Width="55%" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right" width="20%">
                                                                            <asp:Label ID="LabelTotal" runat="server">Total</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="20%">&nbsp;
                                                                    <asp:TextBox ID="txtTotal" Style="text-align: right" TabIndex="12" runat="server"
                                                                        CssClass="FLATTEXTBOX" Width="55%" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                        <td width="50%"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="left">
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <asp:Panel ID="pnlApprove_Disapprove" runat="server" Visible="false">
                                                                                    <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="false" TabIndex="15"
                                                                                        Checked="False" onclick="toggleCheckBox('chkDisapproved')" Text="Approved" />
                                                                                    <asp:CheckBox ID="chkDisapproved" runat="server" AutoPostBack="false" TabIndex="15"
                                                                                        Checked="False" onclick="toggleCheckBox('chkApproved')" Text="Disapproved" />
                                                                                </asp:Panel>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left" valign="bottom">
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="right" colspan="2">
                                                                        <hr style="width: 100%; height: auto" size="1">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnNew_AD_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                            runat="server" Width="100px" CssClass="FLATBUTTON"
                                                                            Text="New" Height="20px"></asp:Button>
                                                                        <asp:Button ID="BtnEdit_AD_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                            runat="server" Width="100px" CssClass="FLATBUTTON"
                                                                            Height="20px" Text="Edit"></asp:Button>
                                                                        &nbsp;<asp:Button ID="btnSave_AD_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                            runat="server" Width="100px" CssClass="FLATBUTTON"
                                                                            Text="Save" Height="20px"></asp:Button>
                                                                        &nbsp;<asp:Button ID="Cancel_AD_Button" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                            runat="server" Width="100px" CssClass="FLATBUTTON"
                                                                            Text="Cancel" Height="20px"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>

                                        </div>
                                    </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 1005px" valign="bottom">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="42" runat="server" Width="90px"
                                                CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                            <asp:Button ID="btnSave_WRITE" runat="server" CssClass="FLATBUTTON" Height="20px"
                                                Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand;
                                                background-repeat: no-repeat" TabIndex="5" Text="Save" Width="90px" />
                                            &nbsp;<asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CssClass="FLATBUTTON"
                                                Height="20px" Text="Undo"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                        runat="server" designtimedragdrop="261" />

                    <input id="hdTaxRateClaimDiagnostics" type="hidden" name="hdTaxRateClaimDiagnostics" runat="server" />
                    <input id="hdComputeMethodClaimDiagnostics" type="hidden" name="hdComputeMethodClaimDiagnostics" runat="server" />
                
                    <input id="hdTaxRateClaimOther" type="hidden" name="hdTaxRateClaimOther" runat="server" />
                    <input id="hdComputeMethodClaimOther" type="hidden" name="hdComputeMethodClaimOther" runat="server" />

                    <input id="hdTaxRateClaimDisposition" type="hidden" name="hdTaxRateClaimDisposition" runat="server" />
                    <input id="hdComputeMethodClaimDisposition" type="hidden" name="hdComputeMethodClaimDisposition" runat="server" />

                    <input id="hdTaxRateClaimLabor" type="hidden" name="hdTaxRateClaimLabor" runat="server" />
                    <input id="hdComputeMethodClaimLabor" type="hidden" name="hdComputeMethodClaimLabor" runat="server" />

                    <input id="hdTaxRateClaimParts" type="hidden" name="hdTaxRateClaimParts" runat="server" />
                    <input id="hdComputeMethodClaimParts" type="hidden" name="hdComputeMethodClaimParts" runat="server" />

                    <input id="hdTaxRateClaimShipping" type="hidden" name="hdTaxRateClaimShipping" runat="server" />
                    <input id="hdComputeMethodClaimShipping" type="hidden" name="hdComputeMethodClaimShipping" runat="server" />

                    <input id="hdTaxRateClaimService" type="hidden" name="hdTaxRateClaimService" runat="server" />
                    <input id="hdComputeMethodClaimService" type="hidden" name="hdComputeMethodClaimService" runat="server" />

                    <input id="hdTaxRateClaimTrip" type="hidden" name="hdTaxRateClaimTrip" runat="server" />
                    <input id="hdComputeMethodClaimTrip" type="hidden" name="hdComputeMethodClaimTrip" runat="server" />
                    
                    <input id="hdLaborTaxAmt" type="hidden" name="hdLaborTaxAmt" runat="server" />
                    <input id="hdPartsTaxAmt" type="hidden" name="hdPartsTaxAmt" runat="server" /> 
                    <input id="hdServiceChargeTaxAmt" type="hidden" name="hdServiceChargeTaxAmt" runat="server" />
                    <input id="hdTripTaxAmt" type="hidden" name="hdTripTaxAmt" runat="server" />
                    <input id="hdShippingTaxAmt" type="hidden" name="hdShippingTaxAmt" runat="server" />
                    <input id="hdDispositionTaxAmt" type="hidden" name="hdDispositionTaxAmt" runat="server" /> 
                    <input id="hdDiagnosticsTaxAmt" type="hidden" name="hdDiagnosticsTaxAmt" runat="server" />
                    <input id="hdOtherTaxAmt" type="hidden" name="hdOtherTaxAmt" runat="server" />
                    <input id="hdSubTotalAmt" type="hidden" name="hdSubTotalAmt" runat="server" />
                    <input id="hdTotalAmt" type="hidden" name="hdTotalAmt" runat="server" />
                    <input id="hdTotalTaxAmt" type="hidden" name="hdTotalTaxAmt" runat="server" />

                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

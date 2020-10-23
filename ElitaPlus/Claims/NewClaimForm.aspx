<%@ Register TagPrefix="uc1" TagName="UserControlPoliceReport" Src="../Common/UserControlPoliceReport_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%@ Register assembly="Microsoft.Web.UI.WebControls" namespace="Microsoft.Web.UI.WebControls" tagprefix="iewc" %>
<%--REQ-784--%>
<%@ Register TagPrefix="uc1" TagName="UserControlContactInfo" Src="../Common/UserControlContactInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlWizard" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" TagName="BestReplacementOption" Src="~/Interfaces/ReplacementOptions.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlLogisticalInfo" Src="~/Claims/UserControlLogisticalInfo.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlClaimDeviceInfo" Src="~/Interfaces/ClaimDeviceInformationController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddressInfo" Src="~/Common/UserControlAddress_New.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewClaimForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.NewClaimForm" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:ProtectionAndEventDetails ID="moProtectionEvtDtl" runat="server" align="center" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
    <Elita:MessageController runat="server" ID="mcIssueStatus" Visible="false" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="true" ScriptMode="Auto" AsyncPostBackTimeout="100">
    </asp:ScriptManager>
    <script type="text/javascript">
        var newRptWin
        function mywindowOpen(url) {
            var windowProperties = "width = " + (screen.width - 100) + ", height = " + (screen.height - 200) + ", toolbar = no, location = no, status = yes, resizable = yes, scrollbars = yes";
            newRptWin = window.open(url, "", windowProperties);
            newRptWin.moveTo(50, 90);
        }
        //*************************************************************************************************
        // TextBoxes Changes

        //This function converts short month in characters into integer


        function ConvertCharMonthToIntMonth(strMonth) {
            var i;
            var intMonth;
            var strMonthArray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            for (i = 0; i < 12; i++) {
                if (strMonth.toUpperCase() == strMonthArray[i].toUpperCase()) {
                    intMonth = i + 1;
                    strMonth = strMonthArray[i];
                    i = 12;
                }
            }
            //alert(intMonth);	
            return intMonth;
        }

        function monthsBetweenDates(Date1, Date2) {
            var number = 0;
            if (Date1 > Date2) return number;  //if date1>date2 return 0 months

            //if date1=date2 return 1 month
            if ((Date2.getFullYear() == Date1.getFullYear()) && (Date1.getMonth() == Date2.getMonth())) {
                if (Date2.getDate() == Date1.getDate())
                    return 1;
            }

            //if date1<date2 then
            number = number + (Date2.getFullYear() - Date1.getFullYear()) * 12;
            number = number - (Date1.getMonth() - Date2.getMonth());

            if (Date2.getDate() > Date1.getDate())
                number = number + 1;

            return number;
        }

        function GetMonthsBetLossDateAndPrdSalesDate() {
            var Months;
            var strDay, strMonth, strYear, intDay, intMonth, intYear;
            var lossDateArray = new Array();
            var prodSalesDateArray = new Array();
            var lossDate = document.getElementById('<%=TextboxLossDate.ClientID %>');
            var prodSalesDate = '<%=dProductSalesDate%>';
            var dateOfLoss = lossDate.value;
            Months = 0;
            prodSalesDateArray = prodSalesDate.split("-")
            if (prodSalesDateArray.length != 3) {
                return 0;
            }
            else {
                strDay = prodSalesDateArray[0];
                strMonth = prodSalesDateArray[1];
                strYear = prodSalesDateArray[2];
                intDay = parseInt(strDay, 10);
                intYear = parseInt(strYear, 10);
                intMonth = parseInt(strMonth, 10);
                if (isNaN(intMonth)) {
                    intMonth = ConvertCharMonthToIntMonth(strMonth)
                }
                // alert(intDay);  alert(intMonth);alert(intYear);     
                prodSalesDate = new Date(intYear, intMonth - 1, intDay);

                if (dateOfLoss.indexOf("-") > 0)
                    lossDateArray = dateOfLoss.split("-");
                else if (dateOfLoss.indexOf("/") > 0)
                    lossDateArray = dateOfLoss.split("/");
                else if (dateOfLoss.indexOf(".") > 0)
                    lossDateArray = dateOfLoss.split(".");
                else if (dateOfLoss.indexOf(",") > 0)
                    lossDateArray = dateOfLoss.split(",");

                if (lossDateArray.length != 3) {
                    return 0;
                }
                else {
                    strDay = ''; strMonth = ''; strYear = '';
                    strDay = lossDateArray[0];
                    strMonth = lossDateArray[1];
                    strYear = lossDateArray[2];
                    intDay = parseInt(strDay, 10);
                    intYear = parseInt(strYear, 10);
                    intMonth = parseInt(strMonth, 10);
                    if (isNaN(intMonth)) {
                        intMonth = ConvertCharMonthToIntMonth(strMonth);
                    }
                    else {
                        strDay = lossDateArray[1];
                        strMonth = lossDateArray[0];
                        intDay = parseInt(strDay, 10);
                        intMonth = parseInt(strMonth, 10);
                    }
                    //  alert(intDay);  alert(intMonth);alert(intYear);         
                    dateOfLoss = new Date(intYear, intMonth - 1, intDay);

                    //alert(prodSalesDate);//  alert(dateOfLoss);

                    Months = monthsBetweenDates(prodSalesDate, dateOfLoss);
                }
            }
            return Months;
        }

        function GetDepreciationSchedule() {
            var strLowMonth = '<%=strLowMonth%>';
            var strHighMonth = '<%=strHighMonth%>';
            var strPercent = '<%=strPercent%>';
            var strAmount = '<%=strAmount%>';
            var LowMonth = new Array();
            LowMonth = strLowMonth.split(",");
            var HighMonth = new Array();
            HighMonth = strHighMonth.split(",");
            var Percent = new Array();
            Percent = strPercent.split(",");
            var Amount = new Array();
            Amount = strAmount.split(",");
            var DeprSchedule = new Array(LowMonth, HighMonth, Percent, Amount);
            return DeprSchedule;
        }

        function UpdateInfo() {
            var lossDate = document.getElementById('<%=TextboxLossDate.ClientID %>');
            var dateOfLoss = lossDate.value;
            var curCertId = '<%=curCertId%>';
            var curContractId = '<%=curContractId%>';
            var curCertItemCoverageId = '<%=curCertItemCoverageId%>';
            var curMethodOfRepairCode = '<%=curMethodOfRepairCode%>';

            PageMethods.CalculateLiability(dateOfLoss, curCertId, curContractId, curCertItemCoverageId, curMethodOfRepairCode, OnComplete);
        }

        function OnComplete(result) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var liab = document.getElementById('<%=TextboxLiabilityLimit.ClientID %>');
            var auth = document.getElementById('<%=TextboxAuthorizedAmount.ClientID %>');
            var liab2 = document.getElementById('<%=TextboxLiabilityLimitShadow.ClientID %>');

            liab.value = convertNumberToCulture(round_num(result), decSep, groupSep);
            liab2.value = liab.value;

            var authlimit = parseFloat(setJsFormat(auth.value, decSep));
            AssurantPays(authlimit);
            ConsumerPays(authlimit);
            return true;
        }

        function UpdateLiabLimitAssPays() {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var liab = document.getElementById('<%=TextboxLiabilityLimit.ClientID %>');
            var auth = document.getElementById('<%=TextboxAuthorizedAmount.ClientID %>');
            var liab2 = document.getElementById('<%=TextboxLiabilityLimitShadow.ClientID %>');
            var Months;
            var i;
            Months = GetMonthsBetLossDateAndPrdSalesDate();
            //alert('Months=' + Months);
            if (Months == 0) return false;
            var liabLimit = '<%=nLiabilityLimit%>';
            var DeprSchCount = '<%=DeprSchCount%>';
            DeprSchCount = parseInt(DeprSchCount);
            //  alert('DeprSchCount=' + DeprSchCount);
            if (DeprSchCount == 0) return false;
            var DeprSch = new Array();
            DeprSch = GetDepreciationSchedule();
            liabLimit = parseFloat(liabLimit);
            //  alert('liabLimit='+ liabLimit);
            if (liabLimit <= 0) return false;
            //alert(DeprSch[0][0]);

            for (i = 0; i < DeprSchCount; i++) {
                if (DeprSch[0][i] <= Months && DeprSch[1][i] >= Months) {
                    if (DeprSch[2][i] != 0) {
                        liabLimit = liabLimit * (1 - DeprSch[2][i] / 100);
                    }
                    else {
                        if (DeprSch[3][i] != 0) {
                            liabLimit = liabLimit - DeprSch[3][i];
                        }
                    }
                    // alert(liabLimit);
                    liab.value = convertNumberToCulture(round_num(liabLimit), decSep, groupSep);
                    liab2.value = liab.value;
                    var authlimit = parseFloat(setJsFormat(auth.value, decSep));
                    AssurantPays(authlimit);
                    ConsumerPays(authlimit);
                    return true;
                }
            }
        }

        function AssurantPays(authValue) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var assurPays = document.getElementById('<%=TextboxAssurantPays.ClientID %>');
            var liab = document.getElementById('<%=TextboxLiabilityLimit.ClientID %>');
            var deduct = document.getElementById('<%=TextboxDeductible_WRITE.ClientID %>');
            var MethodOfRepair = '<%=MethodOfRepairCode%>';
            var liabLimit = parseFloat(setJsFormat(liab.value, decSep));

            Discount(authValue);

            if (liabLimit == 0) {
                liabLimit = 999999999.99;
            }
            if (authValue > liabLimit) {
                assurPays.value = convertNumberToCulture(round_num(liabLimit - parseFloat(setJsFormat(deduct.value, decSep))), decSep, groupSep);
            }
            else {
                assurPays.value = convertNumberToCulture(round_num(authValue - parseFloat(setJsFormat(deduct.value, decSep))), decSep, groupSep);
            }
            if (MethodOfRepair != 'RC') {
                if (parseFloat(setJsFormat(assurPays.value)) < 0) {
                    assurPays.value = convertNumberToCulture('0.00', decSep, groupSep);
                }
                if (assurPays.value == 0) {
                    assurPays.value = convertNumberToCulture('0.00', decSep, groupSep);
                }
            }
            else {
                if (assurPays.value.substr(0, 1) == '-') {
                    val = assurPays.value.substring(1);
                    assurPays.value = '-' + convertNumberToCulture(val1, decSep, groupSep);
                }
            }
            DueToScFromAssurant(authValue);
        }

        function ConsumerPays(authValue) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var assurPays = document.getElementById('<%=TextboxAssurantPays.ClientID %>');
            var cPays = document.getElementById('<%=TextboxConsumerPays.ClientID %>');

            cPays.value = convertNumberToCulture('0.00', decSep, groupSep);
            if (authValue > parseFloat(setJsFormat(assurPays.value, decSep))) {
                var total = round_num(authValue - parseFloat(setJsFormat(assurPays.value, decSep)), 2);
                cPays.value = convertNumberToCulture(parseFloat(total), decSep, groupSep);
            }
        }

        function UpdateDeductible() {
            var US = /^(((\d{1,3})(,\d{3})*)|(\d+))(\.\d{1,2})?$/;
            var EU = /^(((\d{1,3})(\.\d{3})*)|(\d+))(,\d{1,2})?$/;
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var deductible = document.getElementById('<%=TextboxDeductible_WRITE.ClientID %>');
            var deductibleShadow = document.getElementById('<%=TextboxDeductibleShadow.ClientID %>');
            var replacementCost = document.getElementById('<%=TextBoxReplacementCost.ClientID %>');
            var authorizedAmount = document.getElementById('<%=TextboxAuthorizedAmount.ClientID %>');
            if (ValidCulture(deductible)) {
                if (decSep == '.') {
                    if (EU.test(deductible.value)) {
                        deductible.value = parseFloat(setJsFormat(deductible.value, ','));
                    }
                    else {
                        deductible.value = parseFloat(setJsFormat(deductible.value, decSep));
                    }
                }
                else {
                    if (US.test(deductible.value)) {
                        deductible.value = parseFloat(setJsFormat(deductible.value, '.'));
                    }
                    else {
                        deductible.value = parseFloat(setJsFormat(deductible.value, decSep));
                    }
                }
            }
            else {
                alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>');
                return;
            }
            deductible.value = convertNumberToCulture(parseFloat(deductible.value), decSep, groupSep);
            if (parseFloat(setJsFormat(deductible.value, decSep)) == parseFloat(setJsFormat(deductibleShadow.value, decSep))) {
                return;
            }

            if (authorizedAmount != null) {
                UpdateAuth(authorizedAmount);
            }
            if (replacementCost != null) {
                UpdateAuth(replacementCost);
            }
        }

        function DueToScFromAssurant(authValue) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var assurPays = document.getElementById('<%=TextboxAssurantPays.ClientID %>');
            var deductible = document.getElementById('<%=TextboxDeductible_WRITE.ClientID %>');
            var dueToScFromA = document.getElementById('<%=TextboxDueToSCFromAssurant.ClientID %>');
            var total;

            if (authValue - parseFloat(setJsFormat(assurPays.value, decSep)) < parseFloat(setJsFormat(deductible.value, decSep))) {
                total = authValue - parseFloat(setJsFormat(assurPays.value, decSep));
            }
            else {
                total = parseFloat(setJsFormat(deductible.value, decSep)) + parseFloat(setJsFormat(assurPays.value, decSep));
            }

            if (dueToScFromA != null) {
                dueToScFromA.value = convertNumberToCulture(parseFloat(total), decSep, groupSep);
            }
        }

        function Deductible(authValue) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var liab = document.getElementById('<%=TextboxLiabilityLimit.ClientID %>');
            var deduct = document.getElementById('<%=TextboxDeductible_WRITE.ClientID %>');
            var liabLimit = parseFloat(setJsFormat(liab.value, decSep));
            var deductpercent = '<%=nDeductiblePercent%>';
            if (liabLimit == 0) {
                deduct.value = convertNumberToCulture(((authValue * parseFloat(setJsFormat(deductpercent, decSep))) / 100), decSep, groupSep);
            }
            if (liabLimit > 0) {
                if (liabLimit > authValue) {
                    // alert('inside 2 ' + liabLimit);
                    deduct.value = convertNumberToCulture(((authValue * parseFloat(setJsFormat(deductpercent, decSep))) / 100), decSep, groupSep);
                }
                else {
                    deduct.value = convertNumberToCulture(((liabLimit * parseFloat(setJsFormat(deductpercent, decSep))) / 100), decSep, groupSep);
                }
            }
            if (deduct.value == 0) {
                deduct.value = convertNumberToCulture('0.00', decSep, groupSep);

            }

        }

        function Discount(authValue) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var disc = document.getElementById('<%=TextBoxDiscount.ClientID %>');
            var discountpercent = '<%=nDiscountPercent%>';
            //alert('inside 1 0 ' + disc.value);
            if (authValue == 0.00) {
                disc.value = convertNumberToCulture('0.00', decSep, groupSep);
                //alert('inside A ' + disc.value);
            } else {
                if (discountpercent == '') {
                    //alert('inside Aa ' + discountpercent);    
                    disc.value = convertNumberToCulture('0.00', decSep, groupSep);
                } else {
                    //alert('inside Ab ' + discountpercent);    
                    disc.value = convertNumberToCulture(((authValue * parseFloat(setJsFormat(discountpercent, decSep))) / 100), decSep, groupSep);
                    //alert('inside B ' + disc.value);
                }
            }
            if (disc == 0) {
                //alert('inside 1 2' + disc);
                disc = convertNumberToCulture('0.00', decSep, groupSep);
                //alert('inside 1 3' + disc);   
            }
        }

        function AuthorizationWarning(authValue) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var userAuth = document.getElementById('<%=HiddenUserAuthorization.ClientID %>');
        }

        function CallUpdateAuth(webControl) {
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var ctl = document.getElementById("ctl00_BodyPlaceHolder_" + webControl);
            UpdateAuth(ctl);
            var tb9 = document.getElementById('<%=TextboxDeductible_WRITE.ClientID %>');
            var tb10 = document.getElementById('<%=TextboxDeductibleShadow.ClientID %>');
            tb9.value = convertNumberToCulture('0.00', decSep, groupSep);
            if (tb9 != null) { tb10.value = tb9.value; }
            var auth = document.getElementById('<%=TextboxAuthorizedAmount.ClientID %>');
            var authlimit = parseFloat(setJsFormat(auth.value, decSep));
            AssurantPays(authlimit);
            ConsumerPays(authlimit);
        }

        //debugger;

        function UpdateAuth(webControl) {
            var auth = document.getElementById('<%=TextboxAuthorizedAmount.ClientID %>');
            var auth2 = document.getElementById('<%=TextboxAuthorizedAmountShadow.ClientID %>');
            var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
            var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
            var US = /^(((\d{1,3})(,\d{3})*)|(\d+))(\.\d{1,2})?$/;
            //var EU = /^(((\d{1,3})(.\d{3})*)|(\d+))(\,\d{1,2})?$/;
            var EU = /^(((\d{1,3})(\.\d{3})*)|(\d+))(,\d{1,2})?$/;
            var dBYpercent = '<%=isODeductibleByPercent%>';
            //auth.readOnly = 'false';
            if (ValidCulture(webControl)) {
                if (decSep == '.') {
                    if (EU.test(webControl.value)) {
                        auth.value = parseFloat(setJsFormat(webControl.value, ','));
                    } else {
                        auth.value = parseFloat(setJsFormat(webControl.value, decSep));
                    }
                } else {
                    if (US.test(webControl.value)) {
                        auth.value = parseFloat(setJsFormat(webControl.value, '.'));
                    } else {
                        auth.value = parseFloat(setJsFormat(webControl.value, decSep));
                    }
                }
                //	alert('outside ' + dBYpercent);
                if (dBYpercent == 'True') {
                    //  alert('inside ' + dBYpercent);
                    Deductible(auth.value);
                }
                AssurantPays(auth.value);
                ConsumerPays(auth.value);
                AuthorizationWarning(auth.value);
                auth.value = convertNumberToCulture(auth.value, decSep, groupSep);
                // Make dummy field equal to real field 
                auth2.value = auth.value;
            } else {
                if (webControl.value.substr(0, 1) == '-') {
                    val = webControl.value.substring(1);
                    if (ValidCultureValue(val)) {
                        if (decSep == '.') {
                            if (EU.test(val))
                            { val1 = parseFloat(setJsFormat(val, ',')); }
                            else { val1 = parseFloat(setJsFormat(val, decSep)); }
                        }
                        else {
                            if (US.test(val))
                            { val1 = parseFloat(setJsFormat(val, '.')); }
                            else { val1 = parseFloat(setJsFormat(val, decSep)); }
                        }
                        auth.value = '-' + convertNumberToCulture(val1, decSep, groupSep);
                        // Make dummy field equal to real field 
                        AssurantPays(0 - val1);
                        auth2.value = auth.value;
                    }
                    else
                    { alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>'); }
                }
                else
                { alert('<%=Assurant.ElitaPlus.BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)%>'); }
            }

            //auth.value = parseFloat(setJsFormat(webControl.value,decSep));
            //auth.value = webControl.value;
            //auth.readOnly = 'true';

            var tb1 = document.getElementById('<%=TextBoxCarryInPrice.ClientID %>');
            var tb2 = document.getElementById('<%=TextBoxHomePrice.ClientID %>');
            if (tb1 != null) {
                //tb1.className = "FLATTEXTBOX";
                tb1.readOnly = 'true';
            }
            if (tb2 != null) {
                //tb2.className = "FLATTEXTBOX";
                tb2.readOnly = 'true';

            }
            //alert('TextboxDeductibleShadow ' + tb10.value);
            var tb9 = document.getElementById('<%=TextboxDeductible_WRITE.ClientID %>');
            var tb10 = document.getElementById('<%=TextboxDeductibleShadow.ClientID %>');
            if (tb9 != null) { tb10.value = tb9.value; }
            //alert('TextboxDeductibleShadow ' + tb10.value);
            // alert('TextboxAssurantPaysShadow ' + tb4.value);
            //alert('TextboxAssurantPays ' + tb3.value);
            var tb3 = document.getElementById('<%=TextboxAssurantPays.ClientID %>');
            var tb4 = document.getElementById('<%=TextboxAssurantPaysShadow.ClientID %>');
            if (tb3 != null) { tb4.value = tb3.value; }
            // alert('TextboxAssurantPaysShadow ' + tb4.value);
            // alert('TextboxAssurantPays ' + tb3.value);
            // alert('TextboxConsumerPaysShadow ' + tb6.value);
            var tb5 = document.getElementById('<%=TextboxConsumerPays.ClientID %>');
            var tb6 = document.getElementById('<%=TextboxConsumerPaysShadow.ClientID %>');
            if (tb6 != null) { tb6.value = tb5.value; }
            // alert('TextboxConsumerPaysShadow ' + tb6.value);
            // alert('TextBoxOtherPriceShadow ' + tb8.value); 
            var tb7 = document.getElementById('<%=TextBoxOtherPrice.ClientID %>');
            var tb8 = document.getElementById('<%=TextBoxOtherPriceShadow.ClientID %>');
            if (tb8 != null) { tb8.value = tb7.value; }
            //  alert('TextBoxOtherPriceShadow ' + tb8.value);
            var tb9 = document.getElementById('<%=TextboxDueToSCFromAssurant.ClientID %>');
            var tb10 = document.getElementById('<%=TextboxDueToSCFromAssurantShadow.ClientID %>');
            if (tb10 != null & tb9 != null) { tb10.value = tb9.value; }
        }

        function ValidCulture(webControl) {
            var US = /^(((\d{1,3})(,\d{3})*)|(\d+))(\.\d{1,2})?$/;
            //var EU = /^(((\d{1,3})(.\d{3})*)|(\d+))(\,\d{1,2})?$/;
            var EU = /^(((\d{1,3})(\.\d{3})*)|(\d+))(,\d{1,2})?$/;
            var ReturnValue = false;

            var validNum = /^(((\d{1,3})([\.,]\d{3})*)|(\d+))([\.,]\d{1,2})?$/;

            if (US.test(webControl.value)) {
                ReturnValue = true;
                //alert('1 ' + ReturnValue);
            } else {
                if (EU.test(webControl.value)) {
                    //alert('2 ' + ReturnValue);
                    ReturnValue = true;
                }
            }
            return ReturnValue;
        }

        function ValidCultureValue(val) {
            var US = /^(((\d{1,3})(,\d{3})*)|(\d+))(\.\d{1,2})?$/;
            //var EU = /^(((\d{1,3})(.\d{3})*)|(\d+))(\,\d{1,2})?$/;
            var EU = /^(((\d{1,3})(\.\d{3})*)|(\d+))(,\d{1,2})?$/
            var ReturnValue = false

            var validNum = /^(((\d{1,3})([\.,]\d{3})*)|(\d+))([\.,]\d{1,2})?$/;

            if (US.test(val)) {
                ReturnValue = true;
                //alert('1 ' + ReturnValue);
            } else {
                if (EU.test(val)) {
                    //alert('2 ' + ReturnValue);
                    ReturnValue = true;
                }
            }
            return ReturnValue;
        }
        //*************************************************************************************************
        //  Detail CheckBox Changes
        function UpdateMainDetailCheck() {

            var sCheckHome = document.getElementById('<%=CheckBoxHomePrice.ClientID %>');
            var sCheckCarry = document.getElementById('<%=CheckBoxCarryInPrice.ClientID %>');

        }
        //debugger;
        function UpdateDetailCheck(sCheckControl, sTextControl) {
            var sCheckHome = document.getElementById('<%=CheckBoxHomePrice.ClientID %>');
            var sCheckCarry = document.getElementById('<%=CheckBoxCarryInPrice.ClientID %>');
            //	var sCheckReplacement = document.getElementById("CheckBoxReplacement");
            var sCheckCleaning = document.getElementById('<%=CheckBoxCleaningPrice.ClientID %>');
            var sCheckEstimate = document.getElementById('<%=CheckBoxEstimatePrice.ClientID %>');
            var sCheckOther = document.getElementById('<%=CheckBoxOtherPrice.ClientID %>');
            var sTextHome = document.getElementById('<%=TextBoxHomePrice.ClientID %>');
            var sTextCarry = document.getElementById('<%=TextBoxCarryInPrice.ClientID %>');
            //var sTextReplacement = document.getElementById("TextBoxReplacementCost");
            var sTextCleaning = document.getElementById('<%=TextBoxCleaningPrice.ClientID %>');
            var sTextEstimate = document.getElementById('<%=TextBoxEstimatePrice.ClientID %>');
            var sTextOther = document.getElementById('<%=TextBoxOtherPrice.ClientID %>');
            //alert('1 ' + ReturnValue);

            if (sCheckHome != null) {
                sCheckHome.checked = false;
                //sTextHome.className = "FLATTEXTBOX";
                sTextHome.readOnly = true;
            }

            if (sCheckCarry != null) {

                sCheckCarry.checked = false;
                //sTextCarry.className = "FLATTEXTBOX";
                sTextCarry.readOnly = true;
            }
            //sCheckReplacement.checked = false;
            sCheckCleaning.checked = false;
            sCheckEstimate.checked = false;
            sCheckOther.checked = false;
            sCheckControl.checked = true;
            // UpdateMainDetailCheck();
            sCheckControl.disabled = false;
            // sTextReplacement.className="FLATTEXTBOX";
            // sTextReplacement.readOnly = true;
            //sTextCleaning.className = "FLATTEXTBOX";
            sTextCleaning.readOnly = true;
            //sTextEstimate.className = "FLATTEXTBOX";
            sTextEstimate.readOnly = true;
            //sTextOther.className = "FLATTEXTBOX";
            sTextOther.readOnly = true;
            sTextControl.className = "";
            sTextControl.readOnly = false;
            //sTextCleaning.className = "FLATTEXTBOX";
            sTextCleaning.readOnly = true;
            //sTextEstimate.className = "FLATTEXTBOX";
            sTextEstimate.readOnly = true;
            UpdateAuth(sTextControl);


            sTextControl.select();


        }

        function ClearCallerTaxNumber() {
            var objCallerTaxNumber = document.getElementById('<%=TextboxCALLER_TAX_NUMBER.ClientID %>');
            objCallerTaxNumber.value = "";
            objCallerTaxNumber.disabled = false;
            var objCallerTaxNumberEnabledState = document.getElementById('<%=HiddenCallerTaxNumber.ClientID %>');
            objCallerTaxNumberEnabledState.value = "True";
        }

        function validate() {
            var ddlIssueCode = document.getElementById('<%=ddlIssueCode.ClientID %>');
            if (ddlIssueCode.options[ddlIssueCode.selectedIndex].value == '00000000-0000-0000-0000-000000000000') {
                var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
                msgBox.style.display = 'block';
                return false;
            }
            return true;
        }

        function RefreshDropDownsAndSelect(ctlCodeDropDown, ctlDecDropDown, isSingleSelection, change_Dec_Or_Code) {
            RefreshDualDropDownsSelection(ctlCodeDropDown, ctlDecDropDown, isSingleSelection, change_Dec_Or_Code);
            var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
            var objDecDropDown = document.getElementById(ctlDecDropDown);
            var hdnSelectedIssue = document.getElementById('<%=hdnSelectedIssueCode.ClientID %>');
            hdnSelectedIssue.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
            var ddlIssueCode = document.getElementById('<%=ddlIssueCode.ClientID %>');
            if (ddlIssueCode.options[ddlIssueCode.selectedIndex].value != '00000000-0000-0000-0000-000000000000') {
                var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
                msgBox.style.display = 'none';
            }
        }

        function HideErrorAndModal(divId) {
            var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
            msgBox.style.display = 'none';
            var objCodeDropDown = document.getElementById('<%=ddlIssueCode.ClientID %>'); // "By Code" DropDown control
            var objDecDropDown = document.getElementById('<%=ddlIssueDescription.ClientID %>');
            objCodeDropDown.selectedIndex = 0;
            objDecDropDown.selectedIndex = 0;
            hideModal(divId);
        }

        function RevealModalWithMessage(divId) {
            var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
            msgBox.style.display = 'block';
            revealModal(divId);
        }

        var hdnDealerId = '<%=hdnDealerId.ClientId%>';

        function LoadSKU(ctrlManufaturer,ctrlModel,ctrlSKU,ctrlHdnField)
        {

            var claimedManufacturerId = $('#' + ctrlManufaturer).val();
            var claimedmodel = $('#' + ctrlModel).val();
            var dealerId = $('#' + hdnDealerId).val();

            if (claimedManufacturerId.length > 0 && claimedManufacturerId != '00000000-0000-0000-0000-000000000000' && claimedmodel.length > 0)
            {
                $.ajax({
                    type: "POST",
                    url: "ClaimWizardForm.aspx/LoadSku",
                    data: '{ manufacturerId: "' + claimedManufacturerId + '",model:"' + claimedmodel + '",dealerId:"' + dealerId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg)
                    {
                        $('#' + ctrlSKU).empty();
                        if (msg.d == null) { return }

                        var jsonArray = jQuery.parseJSON(msg.d);
                       
                        var listItems = "";
                        $.each(jsonArray, function ()
                        {
                            listItems += "<option value='" + this + "'>" + this + "</option>";

                        });
                        $('#' + ctrlSKU).html(listItems);
                        $('#' + ctrlSKU).selectedIndex = 0;
                        var hdnSelectedIssue = document.getElementById(ctrlHdnField);
                        hdnSelectedIssue.value = $('#' + ctrlSKU).val();
                    }
                });
            }
        }

        function FillHiddenField(sourceDropDownClientId, destinationControlClientId)
        {
            var hdnSelectedIssue = document.getElementById(destinationControlClientId);
            var objDecDropDown = document.getElementById(sourceDropDownClientId);
            hdnSelectedIssue.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
        }

	    
    </script>
    <div id="ModalCancel" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
            <table width="525"><tr><td align="left">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label></td><td align="right">
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="right"/></a></td></tr></table></p>
            <table class="formGrid" cellspacing="0" cellpadding="0" border="0" width="525">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="imgMsgIcon" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png"
                                height="28" />
                        </td>
                        <td id="tdModalMessage" colspan="2" runat="server">
                            <asp:Label ID="lblCancelMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td id="tdBtnArea" nowrap="nowrap" runat="server" colspan="2">
                            <input id="btnModalCancelYes" class="primaryBtn floatR" runat="server" type="button"
                                value="Yes" />
                            <input id="Button1" class="popWindowAltbtn floatR" runat="server" type="button" value="No"
                                onclick="hideModal('ModalCancel');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
        <div class="dataContainer" id="dvStepWizBox" visible="false" runat="server">
            <div id="Div1" class="stepWizBox" runat="server">
                <Elita:UserControlWizard ID="ucWizardControl" runat="server">
                </Elita:UserControlWizard>
            </div>
        </div>
        <div class="dataContainer">
            <h2 class="dataGridHeader">
                <asp:Label runat="server" ID="LabelNewClaimDtl">NEW_CLAIM_DETAILS</asp:Label>
            </h2>
            <div class="stepformZone">
                <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelCertificateNumber" runat="server" Font-Bold="false">Certificate</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxCertificateNumber" TabIndex="200" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelClaimNumber" runat="server">Claim_Number</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxClaimNumber" TabIndex="171" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelServiceCenter" runat="server">Service_Center_Name</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxServiceCenter" TabIndex="200" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelContactName" runat="server">Contact_Name</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="cboContactSalutationId" TabIndex="64" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="TextboxContactName" TabIndex="65" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelCallerName" runat="server">NAME_OF_CALLER</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="cboCallerSalutationId" TabIndex="64" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="TextboxCallerName" TabIndex="70" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelConditionalReqquired" runat="server">LabelConditionalReqquired</asp:Label>
                                <asp:Label ID="LabelCALLER_TAX_NUMBER" runat="server">CALLER_TAX_NUMBER</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxCALLER_TAX_NUMBER" TabIndex="71" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelLossDate" runat="server">Date_Of_Loss</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxLossDate" TabIndex="73" runat="server" SkinID="SmallTextBox"
                                    onchange="UpdateInfo();" AutoPostBack="true"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonLossDate" TabIndex="75" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelLiabilityLimit" runat="server">Liability_Limit</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxLiabilityLimit" TabIndex="76" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                <asp:TextBox ID="TextboxLiabilityLimitShadow" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelReportDate" runat="server">DATE_REPORTED</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxReportDate" TabIndex="73" runat="server" SkinID="SmallTextBox"
                                    onchange="UpdateInfo();"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonReportDate" TabIndex="75" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelDeductible" runat="server">Deductible</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxDeductible_WRITE" TabIndex="78" runat="server" SkinID="SmallTextBox"
                                    onchange="UpdateDeductible();"></asp:TextBox>
                                <asp:TextBox ID="TextboxDeductibleShadow" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelOutstandingPremAmt" runat="server">OUTSTANDING_PREMIUM_AMOUNT</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxOutstandingPremAmt" TabIndex="75" runat="server" ReadOnly="true"
                                    SkinID="MediumTextBox" ForeColor="Red" Font-Bold="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelCauseOfLossId" runat="server">Cause_Of_Loss</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="cboCauseOfLossId" TabIndex="77" runat="server" SkinID="MediumDropDown"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelDiscount" runat="server">DISCOUNT</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextBoxDiscount" TabIndex="78" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                <asp:TextBox ID="TextBoxDiscountShadow" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelReplacementCost" runat="server">Replacement_Cost</asp:Label>
                                <asp:Label ID="LabelAuthorizedAmount" runat="server">Authorized_Amount</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextBoxReplacementCost" runat="server" SkinID="MediumTextBox" onchange="UpdateAuth(this);"
                                    TabIndex="78"></asp:TextBox>
                                <asp:TextBox ID="TextboxAuthorizedAmountShadow" runat="server" ReadOnly="true" SkinID="SmallTextBox"
                                    TabIndex="78"></asp:TextBox>
                                <asp:TextBox ID="TextboxAuthorizedAmount" TabIndex="78" runat="server" SkinID="SmallTextBox"
                                    onchange="UpdateAuth(this);"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelAssurantPays" runat="server">ASSURANT_PAY</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxAssurantPays" TabIndex="265" runat="server" SkinID="SmallTextBox"
                                    DESIGNTIMEDRAGDROP="538"></asp:TextBox>
                                <asp:TextBox ID="TextboxAssurantPaysShadow" runat="server" SkinID="SmallTextBox"
                                    DESIGNTIMEDRAGDROP="538"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelLoaner" runat="server" Font-Bold="false">Loaner_Taken</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:CheckBox ID="CheckBoxLoanerTaken" runat="server" TabIndex="79"></asp:CheckBox>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelConsumerPays" runat="server">CONSUMER_PAY</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxConsumerPays" TabIndex="270" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                <asp:TextBox ID="TextboxConsumerPaysShadow" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="lblLoanerRequested" runat="server" Font-Bold="false">Loaner_Requested</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:CheckBox ID="chkLoanerRequested" runat="server" TabIndex="79"></asp:CheckBox>
                            </td>
                            <td nowrap="nowrap" align="right" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelPolicyNumber" runat="server">Policy_Number</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxPolicyNumber" TabIndex="80" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelDueToSCFromAssurant" runat="server">DUE_TO_SC_FROM_ASSURANT</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxDueToSCFromAssurant" TabIndex="271" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                <asp:TextBox ID="TextboxDueToSCFromAssurantShadow" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelPickUpDate" runat="server">PickUp_Date</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxPickUpDate" TabIndex="83" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonPickUpDate" TabIndex="84" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelRepairDate" runat="server">REPAIR_DATE</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxRepairdate" TabIndex="81" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonRepairDate" TabIndex="82" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    ImageAlign="AbsMiddle"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                            <%--REQ-784--%>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelUseShipAddress" runat="server" Visible="false">USE_SHIP_ADDRESS</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="cboUseShipAddress" TabIndex="85" runat="server" SkinID="MediumDropDown"
                                    AutoPostBack="True" Visible="false">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelIsLawsuitId" runat="server">Lawsuit</asp:Label>
                            </td>
                             <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboLawsuitId" TabIndex="86" runat="server" SkinID="SmallDropDown"></asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="LabelInvoiceNumber" runat="server">Invoice_Numb</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TextboxInvoiceNumber" TabIndex="87" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <asp:Label ID="lblNewDeviceSKU" runat="server">NEW_DEVICE_SKU:</asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtNewDeviceSKU" TabIndex="88" runat="server" AutoPostBack="true"
                                    SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <Elita:BestReplacementOption id="ReplacementOption" runat="server" visible="True" >
        </Elita:BestReplacementOption>
        <div class="dataContainer">
            <h2 class="dataGridHeader">
                <asp:Label runat="server" ID="lblGrdHdr"></asp:Label>
                <span class=""><a onclick="RevealModalWithMessage('ModalIssue');" href="javascript:void(0)">
                    <asp:Label ID="lblFileNewIssue" runat="server"></asp:Label>
                </a></span>
            </h2>
            <div style="width: 100%;">

                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />        
                <div id="tabs" class="style-tabs">
                  <ul>
                    <li><a href="#tabClaimIssues"><asp:Label ID="Label21" runat="server" CssClass="tabHeaderText">CLAIM_ISSUES</asp:Label></a></li>
                    <li><a href="#tabClaimImages"><asp:Label ID="Label23" runat="server" CssClass="tabHeaderText">CLAIM_IMAGES</asp:Label></a></li>
                    <li><a href="#tabDeviceInformation"><asp:Label ID="Label24" runat="server" CssClass="tabHeaderText">DEVICE_INFORMATION</asp:Label></a></li>
                    <li><a href="#tabServiceCenterInformation"><asp:Label ID="Label25" runat="server" CssClass="tabHeaderText">SERVICE_CENTER_INFORMATION</asp:Label></a></li>
                    <li><a href="#tabLogisticalInformation"><asp:Label ID="Label26" runat="server" CssClass="tabHeaderText">LOGISTICAL_INFORMATION</asp:Label></a></li>
                    <li><a href="#tabExtendedStatusAging"><asp:Label ID="Label27" runat="server" CssClass="tabHeaderText">EXTENDED_STATUS_AGING</asp:Label></a></li>
                    <li><a href="#tabsQuestionAnswerInfo"><asp:Label ID="Label28" runat="server" CssClass="tabHeaderText">CASE_QUESTION_ANSWER</asp:Label></a></li>
                    <li><a href="#tabsActionInfo"><asp:Label ID="Label29" runat="server" CssClass="tabHeaderText">CASE_ACTION</asp:Label></a></li>
                  </ul>

                  <div id="tabClaimIssues">
                     <div class="Page">
                                <div id="dvGridPager" runat="server">
                                    <table width="100%" class="dataGrid">
                                        <tr id="trPageSize" runat="server">
                                            <td class="bor" align="left">
                                                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                                                    runat="server">:</asp:Label>
                                                &nbsp;
                                                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                                                    SkinID="SmallDropDown">
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="bor" align="right">
                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                                    SkinID="DetailPageGridView" AllowSorting="true">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />
                                    <HeaderStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Issue" SortExpression="ISSUE_DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="Select" ID="EditButton_WRITE" runat="server" CausesValidation="False"
                                                    Text=""></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CREATED_DATE" SortExpression="CREATED_DATE" ReadOnly="true"
                                            HtmlEncode="false" HeaderText="CREATED_DATE" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="CREATED_BY" SortExpression="CREATED_BY" ReadOnly="true"
                                            HtmlEncode="false" HeaderText="CREATED_BY" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PROCESSED_DATE" SortExpression="PROCESSED_DATE" ReadOnly="true"
                                            HtmlEncode="false" HeaderText="PROCESSED_DATE" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PROCESSED_BY" SortExpression="PROCESSED_BY" ReadOnly="true"
                                            HtmlEncode="false" HeaderText="PROCESSED_BY" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="STATUS_CODE" ReadOnly="true" HeaderText="Status" SortExpression="Status"
                                            HtmlEncode="false" />
                                    </Columns>
                                    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                    <PagerStyle />
                                </asp:GridView>
                            </div>
                  </div>
          
                  <div id="tabClaimImages">
                     <asp:Panel runat="server" ID="AddImagePanel" CssClass="dataContainer">
                                <h2 class="dataGridHeader">
                                    <asp:Label runat="server" ID="AddImageHealder">ADD_IMAGE</asp:Label>
                                </h2>
                                <div class="stepformZone">
                                    <table width="100%" class="formGrid" border="0" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td align="right" nowrap="nowrap">
                                                    <asp:Label runat="server" ID="DocumentTypeLabel" Text="DOCUMENT_TYPE"></asp:Label>
                                                </td>
                                                <td nowrap="nowrap">
                                                    <asp:DropDownList runat="server" ID="DocumentTypeDropDown" SkinID="MediumDropDown" />
                                                </td>
                                                <td align="right" nowrap="nowrap">
                                                    <asp:Label runat="server" ID="ScanDateLabel" Text="SCAN_DATE"></asp:Label>
                                                </td>
                                                <td nowrap="nowrap">
                                                    <asp:TextBox runat="server" ID="ScanDateTextBox" ReadOnly="true" SkinID="MediumTextBox" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap="nowrap">
                                                    <asp:Label runat="server" ID="FileNameLabel" Text="FileName"></asp:Label>
                                                </td>
                                                <td colspan="3" nowrap="nowrap">
                                                    <input id="ImageFileUpload" style="width: 80%" type="file" name="ImageFileUpload"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap="nowrap">
                                                    <asp:Label runat="server" ID="CommentLabel" Text="COMMENT"></asp:Label>
                                                </td>
                                                <td colspan="3" nowrap="nowrap">
                                                    <asp:TextBox runat="server" ID="CommentTextBox" Width="80%" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <div class="btnZone">
                                                        <asp:LinkButton ID="ClearButton" runat="server" SkinID="AlternateRightButton" Text="Cancel"></asp:LinkButton>
                                                        <asp:Button ID="AddImageButton" runat="server" SkinID="PrimaryLeftButton" Text="Add_Image"></asp:Button>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </asp:Panel>
                    <div class="Page">
                        <asp:GridView ID="GridClaimImages" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="true">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="FILE_NAME" SortExpression="FILE_NAME">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnImageLink" CommandName="SelectActionImage"
                                            Text="Image Link"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SCAN_DATE" SortExpression="SCAN_DATE" ReadOnly="true"
                                    HtmlEncode="false" HeaderText="SCAN_DATE" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="DOCUMENT_TYPE" SortExpression="DOCUMENT_TYPE" ReadOnly="true"
                                    HtmlEncode="false" HeaderText="DOCUMENT_TYPE" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="STATUS" SortExpression="SCAN_DATE" ReadOnly="true"
                                    HtmlEncode="false" HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" />
                            </Columns>
                            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                            <PagerStyle />
                        </asp:GridView>
                    </div>
                  </div>
          
                  <div id="tabDeviceInformation">
                      <div class="Page">
                          <div id="dvClaimEquipment" runat="server">
                          <table border="0" width="100%">
                              <tr>
                                  <td width="100%" align="left">
                                      <Elita:UserControlClaimDeviceInfo ID="ucClaimDeviceInfo" runat="server"></Elita:UserControlClaimDeviceInfo>
                                  </td>
                              </tr>
                          </table>
                              </div>
                      </div>
                  </div>
                    
                  <div id="tabServiceCenterInformation">
                      <div class="Page">
                                <div id="dvServiceCenter" runat="server">
                                    <table class="formGrid">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label2" runat="server" Text="SERVICE_CENTER_CODE"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterCode" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label3" runat="server" Text="CONTACT_NAME"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterContactName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label4" runat="server" Text="SERVICE_CENTER_NAME"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterName" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label6" runat="server" Text="PHONE1"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterPhone1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label5" runat="server" Text="ADDRESS"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterAddress1" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label8" runat="server" Text="PHONE2"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterPhone2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label7" runat="server" Text="ADDRESS2"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterAddress2" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label10" runat="server" Text="FAX"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterFax" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label9" runat="server" Text="City"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterCity" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label12" runat="server" Text="BUSINESS_HOURS"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterBussHours" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label11" runat="server" Text="STATE/PROVICE"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterState" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label14" runat="server" Text="PROCESSING_FEE"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterProcessFee" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label13" runat="server" Text="COUNTRY"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterCountry" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label16" runat="server" Text="EMAIL"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterEmail" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label15" runat="server" Text="ZIP"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterZip" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label18" runat="server" Text="CC_EMAIL"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterCCEmail" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label17" runat="server" Text="ORIGINAL_DEALER"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblServiceCenterOrigDealer" runat="server"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label20" runat="server" Text="DEFAULT_TO_EMAIL"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkServiceCenterDefToEmail" runat="server" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                            
                                        </td>
                                        <td align="left">
                            
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label22" runat="server" Text="SHIPPING"></asp:Label>:
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkServiceCenterShipping" runat="server" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label19" runat="server" Text="COMMENTS"></asp:Label>:
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox ID="txtServiceCenterComments" runat="server" Columns="125" TextMode="MultiLine"
                                            Rows="4"></asp:TextBox>
                                        </td>
                        
                                    </tr>
                                    </table>
                                </div>
                            </div>
                  </div>
          
                  <div id="tabLogisticalInformation">
                      <div class="Page">
                                <div id="Div7" runat="server">
                                    <Elita:UserControlLogisticalInfo runat="server" Id="ClaimLogisticalInfo">
                                    </Elita:UserControlLogisticalInfo>
                                </div>
                            </div>
                  </div>
          
                  <div id="tabExtendedStatusAging">
                     <div class="Page">
	                            <div id="dvExtendedStatusAging" runat="server">
	                            <table width="100%" class="dataGrid" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
		                        <tbody>
		                            <asp:Repeater runat="server" ID="moExtendedStatusAgingRepeater" OnItemDataBound="moExtendedStatusAgingRepeater_ItemDataBound">
			                        <HeaderTemplate>
			                            <tr>
				                        <th align="center" scope="col" style="color:#12135B;width:20%;"><asp:Label ID="lblhdStageName" runat="server" Text="STAGE_NAME"></asp:Label></th>
				                        <th align="center" scope="col" style="color:#12135B;width:20%;"><asp:Label ID="lblhdAgingStartStatus" runat="server" Text="AGING_START_STATUS"></asp:Label></th>
				                        <th align="center" scope="col" style="color:#12135B;width:20%;"><asp:Label ID="lblhdAgingEndStatus" runat="server" Text="AGING_END_STATUS"></asp:Label></th>
				                        <th align="center" scope="col" style="color:#12135B;width:15%;"><asp:Label ID="lblhdAgingStatus" runat="server" Text="STATUS_AGING"></asp:Label></th>
				                        <th align="center" scope="col" style="color:#12135B;width:25%;"><asp:Label ID="lblhdAgingSinceClaimInception" runat="server" Text="AGING_SINCE_CLAIM_INCEPTION"></asp:Label></th>
			                            </tr>
			                        </HeaderTemplate>
			                        <ItemTemplate>
			                            <tr style="background-color:#FFFFFF">
				                        <td rowspan="2" align="center" valign="middle"><asp:Label ID="lblitmStageName" runat="server" Text='<%# Eval("stage_name")%>' Style="vertical-align:central;"></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingStartStatus" runat="server" Text='<%# Eval("aging_start_status")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingEndStatus" runat="server" Text='<%# Eval("aging_end_status")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingStatusDays" runat="server" Text='<%# String.Concat(Eval("aging_days"), " Day(s)")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingSinceClaimInceptionDays" runat="server" Text='<%# String.Concat(Eval("aging_since_claim_days"), " Day(s)")%>'></asp:Label></td>
			                            </tr>
			                            <tr style="background-color:#FFFFFF">
				                        <td align="center"><asp:Label ID="lblitmAgingStartDateTime" runat="server"></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingEndDateTime" runat="server"></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingStatusHours" runat="server" Text='<%# String.Concat(Eval("aging_hours"), " Hour(s)")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingSinceClaimInceptionHours" runat="server" Text='<%# String.Concat(Eval("aging_since_claim_hours"), " Hour(s)")%>'></asp:Label></td>
			                            </tr>
			                        </ItemTemplate>
			                        <AlternatingItemTemplate>
			                            <tr style="background-color:#F1F1F1">
				                        <td rowspan="2" align="center" valign="middle"><asp:Label ID="lblitmStageName" runat="server" Text='<%# Eval("stage_name")%>' Style="vertical-align:central;"></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingStartStatus" runat="server" Text='<%# Eval("aging_start_status")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingEndStatus" runat="server" Text='<%# Eval("aging_end_status")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingStatusDays" runat="server" Text='<%# String.Concat(Eval("aging_days"), " Day(s)")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingSinceClaimInceptionDays" runat="server" Text='<%# String.Concat(Eval("aging_since_claim_days"), " Day(s)")%>'></asp:Label></td>
			                            </tr>
			                            <tr style="background-color:#F1F1F1">
				                        <td align="center"><asp:Label ID="lblitmAgingStartDateTime" runat="server"></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingEndDateTime" runat="server"></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingStatusHours" runat="server" Text='<%# String.Concat(Eval("aging_hours"), " Hour(s)")%>'></asp:Label></td>
				                        <td align="center"><asp:Label ID="lblitmAgingSinceClaimInceptionHours" runat="server" Text='<%# String.Concat(Eval("aging_since_claim_hours"), " Hour(s)")%>'></asp:Label></td>
			                            </tr>
			                        </AlternatingItemTemplate>
		                            </asp:Repeater>
		                        </tbody>
	                            </table>
	                        </div>
                            </div>
                  </div>
                  <div id="tabsQuestionAnswerInfo">
                     <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="lblQuestionRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                     </table>
                     <div class="Page" runat="server" id="CaseQuestionAnswerTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                                <asp:GridView ID="CaseQuestionAnswerGrid" runat="server" Width="100%"  AllowPaging="false" AllowSorting="false"
                                    SkinID="DetailPageGridView">
                                    <Columns>
                                        <asp:BoundField HeaderText="case_number"  DataField="case_number" SortExpression="case_number"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="interaction_number"  DataField="interaction_number" SortExpression="interaction_number"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Question"  DataField="Question" SortExpression="Question"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="answer"  DataField="answer" SortExpression="answer"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="created_date"  DataField="created_date" SortExpression="created_date"  HtmlEncode="false">
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="30" Mode="Numeric" />
                                </asp:GridView>
                     </div>
                  </div>  
                                
                <div id="tabsActionInfo">
                    <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="lblClaimActionRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                     </table>
                     <div class="Page" runat="server" id="ClaimActionTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                            
                            <asp:GridView ID="ClaimActionGrid" runat="server" Width="100%"  AllowPaging="false" AllowSorting="false"
                                SkinID="DetailPageGridView">
                                <Columns>
                                    <asp:BoundField HeaderText="action_owner"  DataField="action_owner" SortExpression="action_owner"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="action_type"  DataField="action_type" SortExpression="action_type"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="document_type_descr"  DataField="document_type_descr" SortExpression="document_type_descr"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="status"  DataField="status" SortExpression="status"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="created_date"  DataField="created_date" SortExpression="created_date"  HtmlEncode="false">
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric" />
                            </asp:GridView>
                     </div>
                </div> 
                </div>

            </div>
            <!-- -->
        </div>
        <div id="modalClaimImages" class="overlay">
            <div id="Div3" class="overlay_message_content" style="width: 1100px; left: 8%">
                <p class="modalTitle">
                    <asp:Label ID="lblClaimImage" runat="server" Text="CLAIM_IMAGE"></asp:Label>
                    <a href="javascript:void(0)" onclick="hideModal('modalClaimImages');">
                        <img id="img3" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                            width="16" height="18" align="absmiddle" class="floatR" /></a></p>
                <iframe class="pdfContainer" align="left" runat="server" id="pdfIframe"></iframe>
            </div>
            <div id="Div5" class="black_overlay">
            </div>
        </div>
        <div id="modalCollectDeductible" class="overlay">
            <div id="Div4" class="overlay_message_content" style="width: 45%; top: 50px; overflow: hidden;">
                <p class="modalTitle">
                    <asp:Label ID="lblCollectDeductible" runat="server" Text="COLLECT_DEDUCTIBLE"></asp:Label>
                    <a href="javascript:void(0)" onclick="hideModal('modalCollectDeductible');">
                        <img id="img5" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                            width="16" height="18" align="middle" class="floatR" /></a>
                </p>
                <Elita:MessageController runat="server" ID="moModalCollectDivMsgController" />                
                <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <span class="mandatory">*</span><asp:Label ID="lblDedCollMethod" runat="server">DED_COLL_METHOD</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboDedCollMethod" runat="server" SkinID="MediumDropDown" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDedCollAuthCode" runat="server">CC_AUTH_CODE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDedCollAuthCode" runat="server" SkinID="MediumTextBox" Enabled = "false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnDedCollContinue" runat="server" CssClass="primaryBtn floatR"  Text="CONTINUE" CausesValidation="false" />
                        </td>
                    </tr>
                </table>              
             </div>
            <div id="Div6" class="black_overlay">
            </div>
        </div>
        <div id="ModalIssue" class="overlay">
            <div id="Div2" class="overlay_message_content" style="width: 500px">
                <p class="modalTitle">
                    <asp:Label ID="Label1" runat="server" Text="NEW_CLAIM_ISSUE"></asp:Label>
                    <a href="javascript:void(0)" onclick="HideErrorAndModal('ModalIssue');">
                        <img id="img2" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                            width="16" height="18" align="absmiddle" class="floatR" /></a></p>
                <div class="dataContainer">
                    <div runat="server" id="modalMessageBox" class="errorMsg" style="display: none">
                        <p>
                            <img id="imgIssueMsg" width="16" height="13" align="middle" runat="server" src="~/App_Themes/Default/Images/icon_error.png" />
                            <asp:Literal runat="server" ID="MessageLiteral" />
                        </p>
                    </div>
                </div>
                <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblIssueCode" runat="server" Text="ISSUE_CODE"></asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlIssueCode" runat="server" SkinID="MediumDropDown" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblIssueDescription" runat="server" Text="ISSUE_DESCRIPTION"></asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlIssueDescription" runat="server" SkinID="MediumDropDown"
                                AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="seperator">
                            <img id="Img4" src="~/App_Themes/Default/Images/icon_dash.png" runat="server" width="6"
                                height="5" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCreatedDate" runat="server" Text="CREATED_DATE"></asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCreatedDate" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCreatedBy" runat="server" Text="CREATED_BY"></asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCreatedBy" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" SkinID="PrimaryRightButton" Text="SAVE" OnClientClick="return validate();" />
                            <input id='btnCancel' runat="server" type="button" name="Cancel" value="Cancel" onclick="HideErrorAndModal('ModalIssue');"
                                class='popWindowCancelbtn floatR' />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="Div3" class="black_overlay">
            </div>
        </div>
        <asp:Panel ID="PanelRepair" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="LabelMethodPrice" runat="server">Method_Price</asp:Label>
                </h2>
                <asp:Panel ID="PanelMethodPrice" runat="server">
                    <div class="stepformZone">
                        <table id="TableMethodPrice" class="formGrid" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td valign="middle" nowrap="nowrap" align="right">
                                        <asp:CheckBox ID="CheckBoxCarryInPrice" runat="server"></asp:CheckBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="right">
                                        <asp:TextBox ID="TextBoxCarryInPrice" runat="server" onchange="UpdateAuth(this);"></asp:TextBox>
                                    </td>
                                    <td class="padRight20" valign="middle" nowrap="nowrap" align="left">
                                        <asp:Label ID="LabelCarryInPrice" runat="server">Carry_In_Price</asp:Label>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:CheckBox ID="CheckBoxHomePrice" runat="server"></asp:CheckBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:TextBox ID="TextBoxHomePrice" runat="server" onchange="UpdateAuth(this);"></asp:TextBox>
                                    </td>
                                    <td class="padRight20" valign="middle" nowrap="nowrap" align="left">
                                        <asp:Label ID="LabelHomePrice" runat="server">Home_Price</asp:Label>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:CheckBox ID="CheckBoxCleaningPrice" runat="server"></asp:CheckBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:TextBox ID="TextBoxCleaningPrice" runat="server" onchange="UpdateAuth(this);"></asp:TextBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:Label ID="LabelCleaningPrice" runat="server">Cleaning_Price</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" nowrap="nowrap" align="right">
                                        <asp:CheckBox ID="CheckBoxEstimatePrice" runat="server"></asp:CheckBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="right">
                                        <asp:TextBox ID="TextBoxEstimatePrice" runat="server" onchange="UpdateAuth(this);"></asp:TextBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:Label ID="LabelEstimatePrice" runat="server">Estimate_Price</asp:Label>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:CheckBox ID="CheckBoxOtherPrice" runat="server"></asp:CheckBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:TextBox ID="TextBoxOtherPrice" runat="server" onchange="UpdateAuth(this);"></asp:TextBox>
                                        <asp:TextBox ID="TextBoxOtherPriceShadow" runat="server"></asp:TextBox>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        <asp:Label ID="LabelOtherPrice" runat="server">Other_Price</asp:Label>
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        &nbsp;
                                    </td>
                                    <td valign="middle" nowrap="nowrap" align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelPoliceReport" Width="100%" runat="server">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="LabelPoliceRptDtl" runat="server">POLICE_REPORT_DETAILS</asp:Label>
                </h2>
                <uc1:UserControlPoliceReport ID="mcUserControlPoliceReport" runat="server"></uc1:UserControlPoliceReport>
            </div>
        </asp:Panel>
        <div class="dataContainer">
            <table width="70%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td>
                            <h2 class="dataGridHeader">
                                <asp:Label ID="LabelProblemDescription" runat="server">Problem_Description</asp:Label>
                            </h2>
                            <div class="stepformZone">
                                <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td nowrap="nowrap" align="right">
                                                <asp:TextBox ID="TextboxProblemDescription" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                        <td>
                            <h2 class="dataGridHeader">
                                <asp:Label ID="LabelSpecialInstruction" runat="server">SPECIAL_INSTRUCTIONS</asp:Label>
                            </h2>
                            <div class="stepformZone">
                                <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td nowrap="nowrap" align="right">
                                                <asp:TextBox ID="TextboxSpecialInstruction" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
            <tbody>
                <uc1:UserControlContactInfo ID="moUserControlContactInfo" runat="server" Visible="false"></uc1:UserControlContactInfo>
            </tbody>
        </table>
    <div class="dataContainer">
        <table style="width:100%;">
            <tbody>
            <tr>
                <td style="width:100%;">
                    <h2 class="dataGridHeader">
                        <asp:Label ID="lblLogisticStageAddress" runat="server">LOGISTIC_STAGE_ADDRESSES</asp:Label>
                    </h2>
                </td>
            </tr>
            <tr>
                <td style="width:100%;">
                    <asp:Repeater ID="repAddress" runat="server" OnItemDataBound="repAddress_OnItemDataBound" Visible="True">
                        <ItemTemplate>
                            <table class="dataRep" style="width: 65%; border-collapse: collapse; border: 0;">
                                <tbody>
                                <tr>
                                    <td style="width: 100%; text-align: left;">
                                        <asp:Label ID="LogisticStage" runat="server" Font-Bold="true" /> :
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;text-align: left;">
                                        <Elita:UserControlAddressInfo ID="moAddressController" runat="server" Visible="True"></Elita:UserControlAddressInfo>
                                    </td>
                                </tr>
                                </tbody>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            </tbody>
        </table>
    </div>

        <table border="0"></table>
    </asp:Panel>
    <div class="btnZone">
        <div>
            <asp:Button ID="btnCreateClaim_WRITE" TabIndex="190" runat="server" Text="CONTINUE" SkinID="PrimaryRightButton"/>
            <asp:Button ID="ButtonUpdateClaim_Write" TabIndex="190" runat="server" Text="UPDATE_CLAIM" SkinID="PrimaryRightButton"/>
            <asp:LinkButton ID="ButtonCancel_Write" TabIndex="190" runat="server" Text="Cancel" OnClientClick="return revealModal('ModalCancel');" SkinID="AlternateRightButton" Visible="false"/>
            <asp:LinkButton ID="btnCancelClaim" TabIndex="190" runat="server" Text="Cancel Claim" SkinID="AlternateRightButton" Visible="false"/>
            <asp:Button ID="btnBack" TabIndex="185" runat="server" Text="Back" SkinID="AlternateLeftButton"/>
            <asp:Button ID="btnUnlock" runat="server" TabIndex="187" Text="UNLOCK_CLAIM" Visible="false" SkinID="AlternateLeftButton"/>
            <asp:Button ID="ButtonOverride_Write" TabIndex="190" runat="server" Text="Override" SkinID="AlternateLeftButton"/>
            <asp:Button ID="btnComment" TabIndex="190" runat="server" Text="Comments" SkinID="AlternateLeftButton"/>
            <asp:Button ID="btnDenyClaim_Write" TabIndex="195" runat="server" Text="Deny_Claim" SkinID="AlternateLeftButton"/>
        </div>
    </div>
    <asp:TextBox ID="TextBoxNewLiabilityLimit" runat="server" Visible="False" Wrap="False"></asp:TextBox>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
    <input id="HiddenUserAuthorization" type="hidden" name="HiddenUserAuthorization"
        runat="server" />
    <input id="HiddenCallerTaxNumber" type="hidden" name="HiddenCallerTaxNumber" runat="server"
        designtimedragdrop="261" />
    <asp:HiddenField ID="hdnSelectedIssueCode" runat="server" />
     <asp:HiddenField id="hdnDealerId" runat="server" />
     <asp:HiddenField id="hdnSelectedEnrolledSku" runat="server" />
    <asp:HiddenField id="hdnSelectedClaimedSku"  runat="server" />
</asp:Content>

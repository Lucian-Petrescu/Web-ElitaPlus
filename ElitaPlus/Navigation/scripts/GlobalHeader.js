
//function DuplicateErrorWindow() {
//	win = window.showModalDialog('DuplicateEntry.aspx','','dialogHeight:500px;dialogWidth:750px;center:yes');
//}

//*******************************************************************************
//Start Modal Dialog Script
//debugger;
function OpenModalDialog(goToURL, modalArgument) {
    win = window.showModalDialog(goToURL, modalArgument, 'dialogHeight:500px;dialogWidth:750px;center:yes');
    //window.location.href = window.location.href;
}

//End Modal Dialog Script
//***********************************************************************************

//*********************************************************************************
//Start SupportWindow Script

var supportWindowHandle;

function openSupportWindow(goToURL, windowName) {
    var windowProperties = "toolbar = no, location = no, height = 440, width = 770, left = 20, top = 90, status = yes, resizable = yes, scrollbars = yes";
    supportWindowHandle = open(goToURL, windowName, windowProperties);
    return true;
}

function openSupportWindowVoid(goToURL, windowName) {
    var windowProperties = "toolbar = no, location = no, height = 440, width = 770, left = 20, top = 90, status = yes, resizable = yes, scrollbars = yes";
    supportWindowHandle = open(goToURL, windowName, windowProperties);
}

var rptWin
function windowOpen(url, name) {
    var windowProperties = "width = " + (screen.width - 100) + ", height = " + (screen.height - 200) + ", toolbar = no, location = no, status = yes, resizable = yes, scrollbars = yes";
    rptWin = window.open("ReportContainerform.aspx?title=" + name + "&url=" + url, "", windowProperties);
    rptWin.document.title = name;
    rptWin.moveTo(50, 90);
}

//End SupportWindow Script
//*********************************************************************************

//Start Calendar Script
//*********************************************************************************

var version4 = (navigator.appVersion.charAt(0) == "4");
var popupHandle;

function closePopup() {
    if (popupHandle != null && !popupHandle.closed) popupHandle.close();
}

function openCalendar(formItemName) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();

    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    displayPopup(1, 'calendar.aspx?formname=' + formItemName + '&initdate=' + initDate , 'calendarWindow', 132, 177, event);
    return true;
}

function openCalendar(formItemName, callerValue, calendarFormName) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();
    //debugger;
    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    //debugger;
    //displayPopup(1,calendarFormName +'?caller=' + callerValue + '&formname='+formItemName+'&initdate='+initDate,'calendarWindow',177,258,(version4 ? event : null));
    displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate, 'calendarWindow', 177, 258, (version4 ? event : null));
    return false;
}

function openCalendar_New(formItemName, callerValue, calendarFormName, setDateTime, disablePreviousDates) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();
    //debugger;
    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    //debugger;
    //displayPopup(1,calendarFormName +'?caller=' + callerValue + '&formname='+formItemName+'&initdate='+initDate,'calendarWindow',177,258,(version4 ? event : null));
    if (setDateTime && setDateTime == "Y") {
        //displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=' + setDateTime, 'calendarWindow', 195, 250, (version4 ? event : null));
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=' + setDateTime + '&disablePreviousDates=' + disablePreviousDates, 'calendarWindow', 195, 250, event);
    }
    else {
        //displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=N', 'calendarWindow', 195, 250, (version4 ? event : null));
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=N' + '&disablePreviousDates=' + disablePreviousDates, 'calendarWindow', 195, 250, event);
    }
    return false;
}

function openCalendar_New_enableDates(formItemName, callerValue, calendarFormName, setDateTime, enabledDates) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();
    //debugger;
    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    //debugger;
    if (setDateTime && setDateTime == "Y") {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=' + setDateTime + '&enabledDates=' + enabledDates, 'calendarWindow', 195, 250, event);
    }
    else {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=N' + '&enabledDates=' + enabledDates, 'calendarWindow', 195, 250, event);
    }
    return false;
}

function openCalendar_New_disableBeforeDate(formItemName, callerValue, calendarFormName, setDateTime, disableBeforeDate) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();
    //debugger;
    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    //debugger;
    if (setDateTime && setDateTime == "Y") {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=' + setDateTime + '&disableBeforeDate=' + disableBeforeDate, 'calendarWindow', 195, 250, event);
    }
    else {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=N' + '&disableBeforeDate=' + disableBeforeDate, 'calendarWindow', 195, 250, event);
    }
    return false;
}


function openCalendarwithtime_New(formItemName, callerValue, calendarFormName, setDateTime) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();
    //debugger;
    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    //debugger;
    //displayPopup(1,calendarFormName +'?caller=' + callerValue + '&formname='+formItemName+'&initdate='+initDate,'calendarWindow',177,258,(version4 ? event : null));
    if (setDateTime && setDateTime == "Y") {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=' + setDateTime, 'calendarWindow', 235, 250, event);
    }
    else {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=N', 'calendarWindow', 235, 250, event);
    }
    return false;
}

function openCalendar(formItemName, callerValue, calendarFormName, setDateTime, disablePreviousDates) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();
    //debugger;
    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    //debugger;
    //displayPopup(1,calendarFormName +'?caller=' + callerValue + '&formname='+formItemName+'&initdate='+initDate,'calendarWindow',177,258,(version4 ? event : null));
    if (setDateTime && setDateTime == "Y") {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=' + setDateTime + '&disablePreviousDates=' + disablePreviousDates, 'calendarWindow', 177, 258, event);
    }
    else {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=N' + '&disablePreviousDates=' + disablePreviousDates, 'calendarWindow', 177, 258, event);
    }
    return false;
}

function openCalendarwithtime(formItemName, callerValue, calendarFormName, setDateTime) {
    var param = new String();
    var formName = new String();
    var fieldName = new String();
    //debugger;
    try {
        initDate = document.getElementById(formItemName).value;
    } catch (exception) {
        try {
            param = formItemName;
            if (param.indexOf('.') >= 0) {
                formName = param.substring(0, param.indexOf('.'));
                fieldName = param.substr(param.indexOf('.') + 1, param.length - param.indexOf('.') - 1);
                initDate = document[formName][fieldName].value
            }
        } catch (exception) {
            initDate = "";
        }
    }
    window.status = formItemName;
    //debugger;
    //displayPopup(1,calendarFormName +'?caller=' + callerValue + '&formname='+formItemName+'&initdate='+initDate,'calendarWindow',177,258,(version4 ? event : null));
    if (setDateTime && setDateTime == "Y") {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=' + setDateTime, 'calendarWindow', 227, 258, event);
    }
    else {
        displayPopup(1, calendarFormName + '?caller=' + callerValue + '&formname=' + formItemName + '&initdate=' + initDate + '&setDateTime=N', 'calendarWindow', 227, 258, event);
    }
    return false;
}

function isIE() {
    return true;//will add code later if need browser detection 
}

function displayPopup(position, url, name, height, width, evnt) {
    // position=1 POPUP: makes screen display up and/or left, down and/or right 
    // depending on where cursor falls and size of window to open
    // position=2 CENTER: makes screen fall in center
    var properties = "toolbar = 0, location = 0, height = " + height;
    properties = properties + ", width=" + width;
    var leftprop, topprop, screenX, screenY, cursorX, cursorY, padAmt;

    if (navigator.appName == "Microsoft Internet Explorer") {
        screenY = document.body.offsetHeight;
        screenX = window.screen.availWidth;
    }
    else {
        screenY = window.outerHeight
        screenX = window.outerWidth
    }

    if (position == 1) { // if POPUP not CENTER
        cursorX = evnt.screenX;
        cursorY = evnt.screenY;
        padAmtX = 10;
        padAmtY = 10;

        if ((cursorY + height + padAmtY) > screenY) {
            // make sizes a negative number to move left/up
            padAmtY = (-30) + (height * -1);
            // if up or to left, make 30 as padding amount
        }

        if ((cursorX + width + padAmtX) > screenX) {
            padAmtX = (-30) + (width * -1);
            // if up or to left, make 30 as padding amount
        }

        if (isIE() || navigator.appName == "Microsoft Internet Explorer") {
            leftprop = cursorX + padAmtX;
            topprop = cursorY + padAmtY;
        }
        else {
            leftprop = (cursorX - pageXOffset + padAmtX);
            topprop = (cursorY - pageYOffset + padAmtY);
        }
    }
    else {
        leftvar = (screenX - width) / 2;
        rightvar = (screenY - height) / 2;

        if (isIE() || navigator.appName == "Microsoft Internet Explorer") {
            leftprop = leftvar;
            topprop = rightvar;
        }
        else {
            leftprop = (leftvar - pageXOffset);
            topprop = (rightvar - pageYOffset);
        }
    }

    if (evnt != null) {
        properties = properties + ", left = " + leftprop;
        properties = properties + ", top = " + topprop;
    }

    closePopup();
    popupHandle = open(url, name, properties);
}

//End Calendar Script
//*********************************************************************************


//*************************************************************************
// Start List Swap Code

// Original:  Phil Webb (phil@philwebb.com)
// Web Site:  http://www.philwebb.com 
// This script and many more are available free online at 
// The JavaScript Source!! http://javascript.internet.com

// Modified by E.Sotto 7/25/2002

function SwapList(fbox, tbox) {
    var arrFbox = new Array();
    var arrTbox = new Array();
    var arrLookup = new Array();
    var i;

    for (i = 0; i < tbox.options.length; i++) {
        arrLookup[tbox.options[i].text] = tbox.options[i].value;
        arrTbox[i] = tbox.options[i].text;
    }

    var fLength = 0;
    var tLength = arrTbox.length;

    for (i = 0; i < fbox.options.length; i++) {
        arrLookup[fbox.options[i].text] = fbox.options[i].value;

        if (fbox.options[i].selected && fbox.options[i].value != "") {
            arrTbox[tLength] = fbox.options[i].text;
            tLength++;
        }
        else {
            arrFbox[fLength] = fbox.options[i].text;
            fLength++;
        }
    }

    arrFbox.sort();
    arrTbox.sort();
    fbox.length = 0;
    tbox.length = 0;
    var c;

    for (c = 0; c < arrFbox.length; c++) {
        var no = new Option();
        no.value = arrLookup[arrFbox[c]];
        no.text = arrFbox[c];
        fbox[c] = no;
    }

    for (c = 0; c < arrTbox.length; c++) {
        var no = new Option();
        no.value = arrLookup[arrTbox[c]];
        no.text = arrTbox[c];
        tbox[c] = no;
    }
}

// End List Swap Code
//*************************************************************************

//*************************************************************************
// Have Popup check if parent window and page still up
// Based on Script by Dexter Brown

var msParentButtonName = '';
var mintTimer2;

function StartParentWindowChecking(formItemName) {
    msParentButtonName = formItemName;
    if (window.parent.opener != null)
        mintTimer2 = window.setInterval("CheckWindowsParent()", 1500);
}

function CheckWindowsParent() {
    if (window.parent.opener && window.parent.opener.closed) {
        if (mintTimer2 != 0)
            window.clearInterval(mintTimer2);

        window.focus();
        //alert("This window's parent has been closed, so this window must close.");
        window.parent.close();
    }
    else {
        var objTest;

        objTest = MM_findObj(msParentButtonName, window.opener.document)

        if (objTest == null) {

            if (mintTimer2 != 0)
                window.clearInterval(mintTimer2);

            //		window.focus();
            //		alert("The popup window's parent is gone so this window must close.");
            window.parent.close();
        }
        else {
            objTest = null;
        }
    }
}

//********* End Parent window checking			
//****************************************************************

//**************************************************************
// Start - Dago's MacroMedia functions

/* Functions that swaps images. */

function MM_swapImage() {
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}

function MM_swapImgRestore() {
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}

/* Functions that handle preload. */
function MM_preloadImages() {
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; } 
    }
}

function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function printit() {
    if (window.print) {
        window.print();
    }
    else {
        var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
        document.body.insertAdjacentHTML('beforeEnd', WebBrowser);
        WebBrowser1.ExecWB(6, 2); //Use a 1 vs. a 2 for a prompting dialog box    WebBrowser1.outerHTML = "";  
    }
}

// End - Dago's MacroMedia fuctions
//**************************************************************


//Set Focus To First Editable Element (arf 11-1-02)
function SetFocusToFirstEditableElement() {
    try {
        //debugger; 
        minTabIndex = 1000000;
        for (var i = 0; i < document.forms[0].elements.length; i++) {
            //if (document.forms[0].elements[i].isContentEditable && !document.forms[0].elements[i].isDisabled){
            if (document.forms[0].elements[i].type != 'hidden' && !document.forms[0].elements[i].isDisabled) {
                if (document.forms[0].elements[i].tabIndex < minTabIndex) {
                    minTabIndex = document.forms[0].elements[i].tabIndex;
                    document.forms[0].elements[i].focus();
                }
            }
        }
    } catch (exception) {
    }
}

//Set Focus To First Editable Element (arf 08-11-2004)
function SetFocusToElement(elementId) {
    try {
        //debugger; 
        //document.forms[0].elements[elementId].focus();          
        elem = document.getElementById(elementId);
        elem.focus();
    } catch (exception) {
    }
}

//Start Change Scrollbar Script
//Set the style for the page's scrollbar

function changeScrollbarColor() {
    if (document.all) {
        document.body.style.scrollbarArrowColor = "black";
        document.body.style.scrollbarFaceColor = "#e6e6ee";
        document.body.style.scrollbar3dlightColor = "#e6e6ee";
        document.body.style.scrollbarDarkshadowColor = "#e6e6ee";
        document.body.style.scrollbarShadowColor = "#fef9ea";
    }
}
//End Change Scrollbar Script

function DoSubmit() {
    if (!IsSubmitting()) {
        _isSubmitting = true;
        document.forms[0].submit();
    }
}

function ConfirmAndSubmit(Message, ResultControlId) {
    var arg = Message + "~" + "WindowConfirmWithSubmit" + "~" + "4" + "~" + "1";
    //debugger;
    retVal = window.showModalDialog("../Common/MessageBoxForm.aspx?arg=" + arg, arg, "dialogHeight:14;dialogWidth:23;edge:Raised;center:Yes;help:No;resizable:no;status:No;scrolling:none;");

    //debugger;

    if (retVal > 0) {
        if (ResultControlId != "null") {
            if (retVal == 3) //The user selected "No"
            {
                document.getElementById(ResultControlId).value = "Cancel";
            }
            else {
                document.getElementById(ResultControlId).value = "Ok";
            }

            //	document.forms[0].submit();	
            DoSubmit();
        }
        else {
            if (retVal == 3) //The user selected "No"
            {
                return false;
            }

        }
        return true;
    }
    else {
        if (ResultControlId != "null") {
            document.getElementById(ResultControlId).value = retVal;
            //	document.forms[0].submit();
            DoSubmit();
        }
        return false;
    }

}

function AcctSettingTypeSubmit(Message, ResultControlId) {
    var arg = Message + "~" + "WindowAcctSettingTypeSubmit" + "~" + "6" + "~" + "1";
    //debugger;
    retVal = window.showModalDialog("../Common/MessageBoxForm.aspx?arg=" + arg, arg, "dialogHeight:14;dialogWidth:23;edge:Raised;center:Yes;help:No;resizable:no;status:No;scrolling:none;");

    //debugger;

    if (retVal > 0) {
        if (ResultControlId != "null") {
            if (retVal == 1) //The user selected "Dealer"
            {
                document.getElementById(ResultControlId).value = "Dealer";
            }
            else {
                document.getElementById(ResultControlId).value = "ServiceCenter";
            }

            //	document.forms[0].submit();	
            DoSubmit();
        }
        else {
            if (retVal == 3) //The user selected "No"
            {
                return false;
            }

        }
        return true;
    }
    else {
        if (ResultControlId != "null") {
            document.getElementById(ResultControlId).value = retVal;
            //	document.forms[0].submit();
            DoSubmit();
        }
        return false;
    }

}

function AlertAndSubmit(Message, ResultControlId) {
    var arg = Message + "~" + "WindowConfirmWithSubmit" + "~" + "7" + "~" + "0";
    //debugger;
    retVal = window.showModalDialog("../Common/MessageBoxForm.aspx?arg=" + arg, arg, "dialogHeight:14;dialogWidth:23;edge:Raised;center:Yes;help:No;resizable:no;status:No;scrolling:none;");

    //debugger;

    if (retVal > 0) {
        if (ResultControlId != "null") {
            if (retVal == 3) //The user selected "No"
            {
                document.getElementById(ResultControlId).value = "Cancel";
            }
            else {
                document.getElementById(ResultControlId).value = "Ok";
            }

            //	document.forms[0].submit();	
            DoSubmit();
        }
        else {
            if (retVal == 3) //The user selected "No"
            {
                return false;
            }

        }
        return true;
    }
    else {
        if (ResultControlId != "null") {
            document.getElementById(ResultControlId).value = retVal;
            //	document.forms[0].submit();
            DoSubmit();
        }
        return false;
    }

}


function resizeForm(item) {
    var browseWidth, browseHeight;

    if (document.layers) {
        browseWidth = window.outerWidth;
        browseHeight = window.outerHeight;
    }
    if (document.all) {
        browseWidth = document.body.clientWidth;
        browseHeight = document.body.clientHeight;
    }

    if (screen.width == "800" && screen.height == "600") {
        newHeight = browseHeight - 220;
    }
    else {
        newHeight = browseHeight - 210;
    }

    item.style.height = String(newHeight) + "px";

    return newHeight;
}

//var _isConfirmDialogInProgress = false;	 
var retVal = 0;

function showMessageAfterLoaded(msg, title, buttons, icon, ResultControlId) {
    if (typeof jQuery == 'undefined') {
        showMessage(msg, title, buttons, icon, ResultControlId);
    } else {
        $(document).ready(function () {
            showMessage(msg, title, buttons, icon, ResultControlId);
        });
    }
}

function showMessage(msg, title, buttons, icon, ResultControlId) {
    var arg = encodeURIComponent(msg + "~" + title + "~" + buttons + "~" + icon);
    retVal = window.showModalDialog("../Common/MessageBoxForm.aspx?arg=" + arg, arg, "dialogHeight:14;dialogWidth:23;edge:Raised;center:Yes;help:No;resizable:no;status:No;scrolling:none;");
    //	if (icon == "1")
    //	{
    //		_isConfirmDialogInProgress = true;
    //	}
    //debugger;

    if (retVal > 0) {
        if (ResultControlId != "null") {
            document.getElementById(ResultControlId).value = retVal;
            //	document.forms[0].submit();
            DoSubmit();
        }
        else {
            if (icon == "1" && retVal == 3) //The user selected "No"
            {
                return false;
            }

        }
        return true;
    }
    else {
        if (ResultControlId != "null") {
            document.getElementById(ResultControlId).value = retVal;
            //	document.forms[0].submit();
            DoSubmit();
        }
        return false;
    }
}

function revealModalAfterLoaded(divID) {
    if (typeof jQuery == 'undefined') {
        revealModal(divID);
    } else {
        $(document).ready(function () {
            revealModal(divID);
        });
    }
}

function revealModal(divID) {
    window.onscroll = function () { document.getElementById(divID).style.top = document.body.scrollTop; };
    document.getElementById(divID).style.display = "block";
    document.getElementById(divID).style.top = document.body.scrollTop;
    return false;
}

function hideModal(divID) {
    document.getElementById(divID).style.display = "none";
    return false;
}

function ExecuteButtonClick(btnUniqueID)
{
    __doPostBack(btnUniqueID, '');
}

function evaluate(retVal, ResultControlId) {

    hideModal("modalPage");
    if (retVal > 0) {
        if (ResultControlId != "null" && ResultControlId != '') {
            document.getElementById(ResultControlId).value = retVal;
            DoSubmit();
        }
        else {
                if (retVal == 3) //The user selected "No"
                {
                    return false;
                }
                if (retVal == 1)
                {
                    DoSubmit();
                }
            }       
        return true;
    }
    else {
        if (ResultControlId != "null") {
            document.getElementById(ResultControlId).value = retVal;
            DoSubmit();
        }
        return false;
    }
}


function showMessageWithSubmit(msg, title, buttons, icon) {
    var arg = encodeURIComponent(msg + "~" + title + "~" + buttons + "~" + icon);
    retVal = window.showModalDialog("../Common/MessageBoxForm.aspx?arg=" + arg, arg, "dialogHeight:14;dialogWidth:23;edge:Raised;center:Yes;help:No;resizable:no;status:No;scrolling:none;");
    //	if (icon == "1")
    //	{
    //		_isConfirmDialogInProgress = true;
    //	}
    if (retVal > 0) {
        if (icon == "1" && retVal == 3) //The user selected "No"
        {
            return false;
        }
        else {
            //	document.forms[0].submit();
            DoSubmit();
            return true;
        }
    }
    else {
        return false;
    }
}

function showModal(url) {
    window.showModalDialog(url, "", "dialogHeight:14;dialogWidth:20;edge:Raised;center:Yes;help:No;resizable:yes;status:No;scrolling:yes;");
    return true;
}


function toggleSideMenu(serverURL) {
    if (document.all("imgShowHideMenu").nameProp == "hideMenuBtn.gif") {
        document.all("imgShowHideMenu").src = String(serverURL) + "Navigation/images/showMenuBtn.gif"
        document.frames["Navigation_Side"].frameElement.width = "0px"
    }
    else {
        document.all("imgShowHideMenu").src = String(serverURL) + "Navigation/images/hideMenuBtn.gif"
        document.frames["Navigation_Side"].frameElement.width = "195px"
    }
}

function saveScrollPosition() {
    if (document.getElementById("scrollPos")) {
        document.getElementById("scrollPos").value = document.getElementById("scroller").scrollTop;
    }
}

function setScrollPosition() {
    if (document.getElementById("scrollPos")) {
        if (document.getElementById("scrollPos").value != "") {
            document.getElementById("scroller").scrollTop = document.getElementById("scrollPos").value
        }
    }
}

function fnTrapKD(btn, event) {
    if (document.all) {

        if (event.keyCode == 13) {

            event.returnValue = false;
            event.cancel = true;
            if (event.preventDefault) {
                event.preventDefault();
            }
            btn.click();

        }
    }

    else if (document.getElementById) {

        if (event.which == 13) {

            event.returnValue = false;
            event.cancel = true;
            if (event.preventDefault) {
                event.preventDefault();
            }
            btn.click();

        }
    }

    else if (document.layers) {

        if (event.which == 13) {

            event.returnValue = false;
            event.cancel = true;
            if (event.preventDefault) {
                event.preventDefault();
            }
            btn.click();

        }
    }
}


/***************************************
PROGRESS FRAME CODE
****************************************/

function showProgressFrame(msg, source) {
    try {
        resizeForm();
    } catch (e) {
    }
    source = source + '?Message=' + encodeURIComponent(msg)
    document.getElementById("progressFrame").src = source;
    document.getElementById("progressFrame").style.display = "";
    document.getElementById("progressFrame").style.top = (document.body.clientHeight / 2) - 100;
    document.getElementById("progressFrame").style.left = (document.body.clientWidth / 2) - 200;
    //blockEvents();
    //setTimeout("blockEvents()", 10000); 
}

function showNewProgressFrame(msg, source) {
    try {
        resizeForm();
    } catch (e) {
    }
    source = source + '?Message=' + encodeURIComponent(msg)
    document.getElementById("progressFrame").src = source;
    document.getElementById("progressFrame").style.display = "";
    document.getElementById("progressFrame").style.top = (document.body.clientHeight / 2) + 200;
    document.getElementById("progressFrame").style.left = (document.body.clientWidth / 2) - 200;
}


function closeProgressFrame() {
    var progressFrame = document.getElementById("progressFrame")
    if (progressFrame == null) {
        progressFrame = parent.document.getElementById("progressFrame");
    }
    if (progressFrame != null && progressFrame.style.display == "") {
        progressFrame.src = '';
        progressFrame.style.display = "none";
    }
}

function showReportViewerFrame(source) {
    document.getElementById("reportViewerFrame").src = source;
    document.getElementById("reportViewerFrame").style.display = "";
    document.getElementById("reportViewerFrame").style.top = (document.body.clientHeight / 2) - 100;
    document.getElementById("reportViewerFrame").style.left = (document.body.clientWidth / 2) - 100;
}

function closeReportViewerFrame() {
    var reportViewerFrame = document.getElementById("reportViewerFrame")
    if (reportViewerFrame != null && reportViewerFrame.style.display == "") {
        reportViewerFrame.src = '';
        reportViewerFrame.style.display = "none";
    }
}

// Grab all Navigator events that might get through to form
// elements while dialog is open. For IE, disable form elements.
function blockEvents() {
    if (Nav4) {
        window.captureEvents(Event.CLICK | Event.MOUSEDOWN | Event.MOUSEUP | Event.FOCUS)
        window.onclick = deadend
    } else {
        disableForms()
    }
    window.onfocus = checkModal
}
// As dialog closes, restore the main window's original
// event mechanisms.
function unblockEvents() {
    if (Nav4) {
        window.releaseEvents(Event.CLICK | Event.MOUSEDOWN | Event.MOUSEUP | Event.FOCUS)
        window.onclick = null
        window.onfocus = null
    } else {
        enableForms()
    }
}

var defaultMessage = new String('');
var mintElipsis = 0;
var elapsedTime = 0;

function ShowWaitMsg() {
    var objDivMsg = document.all.item('lblCounter');

    if (defaultMessage.length == 0)
        defaultMessage = new String('Please Wait');

    mintElipsis += 1;
    objDivMsg.innerText = defaultMessage + AddElipsis(mintElipsis) + ' Elapsed Time: ' + ElapsedTime(elapsedTime);
    objDivMsg.style.visibility = 'visible';

    //reset after five seconds

    if (mintElipsis == 6)
        mintElipsis = 0;
}

function AddElipsis(intNum) {
    var functionReturn = new String();
    functionReturn = '';
    for (var counter = 0; counter < intNum; counter++) { functionReturn += '.'; }
    for (var counter = functionReturn.length; counter < 6; counter++) { functionReturn += ' '; }
    return functionReturn;
}

function ElapsedTime() {
    var objDate = new Date();
    var hours = new String();
    var minutes = new String();
    var seconds = new String();
    var intMinutes = 0;
    var intHours = 0;
    var intTemp = 0;
    var intNumSeconds = 0;
    var delimiter = ":";
    var functionReturn = '';

    hours = '00'; minutes = '00'; seconds = '00';

    if ((elapsedTime > 0) && (elapsedTime < 61)) {
        seconds = elapsedTime.toString();
    }
    else if ((elapsedTime > 60) && (elapsedTime < 3599)) {
        //handle minutes and seconds
        minutes = (elapsedTime / 60).toString();

        if (minutes.indexOf('.', 0) > -1) {
            minutes = minutes.substr(0, minutes.indexOf('.', 0));
            seconds = (elapsedTime - (parseInt(minutes) * 60)).toString();
        }
    }
    else {
        //handle hours,minutes,seconds
        hours = (elapsedTime / 3600).toString();
        if (hours.indexOf('.', 0) > -1) {
            hours = hours.substr(0, hours.indexOf('.', 0));
            intTemp = elapsedTime - (parseInt(hours) * 3600)
            minutes = (intTemp / 60).toString();

            if (minutes.indexOf('.', 0) > -1) {
                minutes = minutes.substr(0, minutes.indexOf('.', 0));
                seconds = (intTemp - (parseInt(minutes) * 60)).toString();
            }
        }
    }

    elapsedTime += 1;

    if (hours.length == 1)
        hours = '0' + hours;

    if (minutes.length == 1)
        minutes = '0' + minutes;

    if (seconds.length == 1)
        seconds = '0' + seconds;

    functionReturn += hours + delimiter + minutes + delimiter + seconds;
    return functionReturn;
}

function SetScrollAreaCtrTreeV(obj, width, height) {
    var otree = document.getElementById(obj);
    otree.style.width = width;
    otree.style.height = height;
}




function arrFind(arrCtr, elem) {
    var arrCtrLength = arrCtr.length
    for (i = 0; i < arrCtrLength; i++) {
        var arrCtriLength = arrCtr[i].length
        for (j = 0; j < arrCtriLength; j++) {
            if (arrCtr[i][j] == elem)
                return arrCtr[i]
        }
    }
}

function changeCrtState(ctr, elem, state) {
    var obj = document.getElementById(ctr)
    var RADIO = 'radio'
    var DROPDOWN = 'DropDown'
    var SELECT_ONE = 'select-one'

    switch (obj.type) {
        case RADIO:
            obj.checked = (state == true ? true : false)
            break
        case DROPDOWN:
            obj.selectedIndex = (state == true ? obj.selectedIndex : -1);
            break
        case SELECT_ONE:
            obj.selectedIndex = (state == true ? obj.selectedIndex : -1);
            break
        default:
    }
}

function ToggleListExt(arrCtr, elem, state) {
    var arrCtriLength = arrCtr.length
    for (j = 0; j < arrCtriLength; j++) {
        changeCrtState(arrCtr[j], elem, state)
    }
}

function ToggleExt(objelem, arrCtr) {
    var elem = objelem.id
    var arrElem = arrFind(arrCtr, elem)
    var arrCtrLength = arrCtr.length
    for (i = 0; i < arrCtrLength; i++)
        ToggleListExt(arrCtr[i], elem, arrElem != arrCtr[i] ? false : true)
}

//
//  If key pressed is not Yy,Nn don't allow it through.
//
var VK_Y = 89;
var VK_N = 78;
var VK_y = 121;
var VK_n = 110;
function onlyYesNo(elem) {
    var iKeyCode;
    iKeyCode = event.keyCode;
    //  If key pressed is not Yy,Nn don't allow it through.
    switch (iKeyCode) {
        case VK_Y:
        case VK_y: { elem.value = ""; event.keyCode = VK_Y; break; }
        case VK_N:
        case VK_n: { elem.value = ""; event.keyCode = VK_N; break; }
        default: { event.keyCode = null; }
    }
}

// arrowKeyHandler
//
var VK_LEFT = 0x25;
var VK_UP = 0x26;
var VK_RIGHT = 0x27;
var VK_DOWN = 0x28;
var READ_MODE = 0;
var EDIT_MODE = 1;
var VC_EDIT_FONT = "white"
var VC_EDIT_BACKGROUND = "#0a246a"
var CurrMode = READ_MODE;
var VC_READONLY_FONT = "black"
var VC_READONLY_BACKGROUND = "white"
var reNameParser = /^moDataGrid:_ctl(\d+)(.+)$/;

function arrowKeyHandler() {

    if (window.event.keyCode == 13) {
        window.event.returnValue = false;
        var ae = document.activeElement;
        if (CurrMode == READ_MODE) {
            ae.style.background = VC_EDIT_BACKGROUND
            ae.style.color = VC_EDIT_FONT
            CurrMode = EDIT_MODE;
        } else {
            ae.style.background = VC_READONLY_BACKGROUND
            ae.style.color = VC_READONLY_FONT
            CurrMode = READ_MODE;
        }
        return;
    } else {
        window.event.returnValue = true;
    }

    if (CurrMode == EDIT_MODE)
        return;

    var ae = document.activeElement;

    //if(ae.tagName.toLowerCase() != "input"){return}
    //if(ae.type.toLowerCase() != "text"){return}
    var sMsg;

    if (ae.tagName.toLowerCase() == "select" ||
       CurrMode == READ_MODE)
        window.event.returnValue = false;

    // ignore event if not an arrow navigation key
    //

    switch (window.event.keyCode) {
        case VK_LEFT: { sMsg = "VK_LEFT from: "; break }
        case VK_UP: { sMsg = "VK_UP from: "; break }
        case VK_RIGHT: { sMsg = "VK_RIGHT from: "; break }
        case VK_DOWN: { sMsg = "VK_DOWN from: "; break }
        default: return;
    }

    // ignore event if name not "txtPerson<row><colName>" format
    //
    if (!reNameParser.exec(ae.name)) { return }
    if (!RegExp.$2) { return }

    // extract and increment row/col values
    //
    var row, col;
    row = RegExp.$1;

    var colName
    colName = RegExp.$2;

    if (!arColumnMap[colName]) {
        return;
    }

    col = arColumnMap[colName];

    switch (window.event.keyCode) {
        case VK_LEFT: { col--; break; }
        case VK_UP: { row--; break; }
        case VK_RIGHT: { col++; break; }
        case VK_DOWN: { row++; break; }
    }

    if (!arColumnMap["col" + col]) {
        return;
    }

    var sNextElement;
    sNextElement = "moDataGrid:_ctl" + row + arColumnMap["col" + col];

    // try to get row<row>text<col> element from
    // the wrapper//s immediate children collection
    //
    var nextElement;
    nextElement = scroller.all.item(sNextElement);

    // exit if it doesn//t exist
    //
    if (!nextElement) {
        return;
    }

    // otherwise give it the focus
    //
    nextElement.focus();


} // arrowKeyHandler

var currentElement = null
function ClickHandler() {
    var ae = document.activeElement;
    if (!ae.type) {
        return;
    }
    if (ae.tagName.toLowerCase() != "input" &&
	   ae.type.toLowerCase() != "text" &&
	   ae.tagName.toLowerCase() != "select") { return }
    if (currentElement != null && currentElement != ae) {
        currentElement.style.background = VC_READONLY_BACKGROUND
        currentElement.style.color = VC_READONLY_FONT
    }
    currentElement = ae;
    currentElement.style.background = VC_EDIT_BACKGROUND;
    currentElement.style.color = VC_EDIT_FONT;
    CurrMode = EDIT_MODE;
}

var oldBackgroundColor = "white"
var VC_FONT = "black"
var oldItem = null
function setHighlighter(item) {
    if (item == oldItem) { return }
    if (oldItem != null && oldItem != currentElement) {
        oldItem.style.background = oldBackgroundColor;
        oldItem.style.color = VC_READONLY_FONT;
    }

    if (item == currentElement) { return }
    oldItem = item;
    oldBackgroundColor = item.style.background;
    item.style.background = '#DEE3E7';
}


function RefreshDualDropDownsSelection(ctlCodeDropDown, ctlDecDropDown, isSingleSelection, change_Dec_Or_Code) {
    var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
    var objDecDropDown = document.getElementById(ctlDecDropDown);   // "By Description" DropDown control 

    if (isSingleSelection) {
        if (change_Dec_Or_Code == 'C') {
            objCodeDropDown.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
        } else {
            objDecDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
        }
    } else {
        objCodeDropDown.selectedIndex = -1;
        objDecDropDown.selectedIndex = -1;
    }
}


function trim(str) {
    s = str.replace(/^(\s)*/, '');
    s = s.replace(/(\s)*$/, '');
    return s;
}

function round_num(number, x) { // function added by Oscar on 06/01/2005 
    x = (!x ? 2 : x);
    return Math.round(number * Math.pow(10, x)) / Math.pow(10, x);
}

function isPointFormat() {
    return IsPointFormat;
}

function FormatToDecimal(value) {
    if (trim(value) == "")
        value = 0;

    var value = parseFloat(value) + 0.0001;
    var svalue = value.toString();
    var index = svalue.indexOf(".");
    var result = svalue.substring(0, index + 3);
    return result;
}

function setJsFormat(value) {
    return value.replace(',', '.');
}
function setJsFormat(value, decSep) {
    if (decSep == '.') {
        var intIndexOfMatch = value.indexOf(',');
        while (intIndexOfMatch != -1) {
            // Relace out the current instance.
            value = value.replace(',', '')

            // Get the index of any next matching substring.
            intIndexOfMatch = value.indexOf(',');
        }
        return value;
    }
    else {
        value = value.replace('.', '');
        value = value.replace(',', '.');
        //Alert('My '+ Value);
        return value;
    }
}
function setCultureFormat(value) {

    value = FormatToDecimal(value.toString());
    if (isPointFormat() != 1) {
        value = value.replace('.', ',');
    }

    return value;
}
// function to format a number with separators. returns formatted number.
// num - the number to be formatted
// decpoint - the decimal point character. if skipped, "." is used
// sep - the separator character. if skipped, "," is used
function convertNumberToCulture(num, decpoint, sep) {
    // check for missing parameters and use defaults if so
    if (arguments.length == 2) {
        sep = ",";
    }
    if (arguments.length == 1) {
        sep = ",";
        decpoint = ".";
    }
    // need a string for operations
    num = num.toString();
    // separate the whole number and the fraction if possible
    a = num.split(decpoint);
    //x = a[0]; // decimal
    //y = a[1]; // fraction
    z = "";

    if (num.indexOf('.') >= 0) {
        x = (num.substring(0, num.indexOf('.')));
        y = num.substring(num.indexOf('.') + 1);
    } else {
        x = num;
        y = "";
    }

    if (typeof (x) != "undefined") {
        // reverse the digits. regexp works from left to right.
        for (i = x.length - 1; i >= 0; i--)
            z += x.charAt(i);
        // add seperators. but undo the trailing one, if there
        z = z.replace(/(\d{3})/g, "$1" + sep);
        if (z.slice(-sep.length) == sep)
            z = z.slice(0, -sep.length);
        x = "";
        // reverse again to get back the number
        for (i = z.length - 1; i >= 0; i--)
            x += z.charAt(i);
        // add the fraction back in, if it was there
        if (typeof (y) != "undefined" && y.length > 1) {
            // do nothing.... 
        } else if (typeof (y) != "undefined" && y.length == 1) {
            y = y + '' + '0';
        } else {
            y = '00';
        }
        x += decpoint + y;
    }
    return x;
}

/**********************************************************************
* Capture the backspace that acts as the browser back button
**********************************************************************/
document.onkeydown = backSpaceKeyHandler;

function backSpaceKeyHandler() {
    var t = event.srcElement.type;
    var kc = event.keyCode;
    return ((kc != 8) || (t == 'text') || (t == 'textarea'));
}
/**********************************************************************
* disable the IE back function from context menu
**********************************************************************/
window.history.go(1);

function ClearCodeValue(GVid, eIndex) {
    var table = document.getElementById(GVid);
    var aIndex = eIndex + 1;
    var Row = table.rows[aIndex];
    Row.cells[3].InnerText = "";
}

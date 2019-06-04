
var jTimerAlertUser;
var jDisplayReport;
var jTimeOut;
var InstanceResponse = null;
var arrRptInstanceList = [];
var ReportStatus;
var alertuser = null;
var checkstatus = null;

var RptStatus;
var ErrorMsg;
var strReportsPending;
var strReportsRunning;
var RptName;
var viewerName;
var closetimer = "No";

 
    function ReportInstanceStatus() {
        //   window.setTimeout('getReportStatus()', 8000);        
        if (checkstatus == null) {
            jTimerAlertUser = setInterval("AlertIfPending()", 180000);
            checkstatus = "Yes";
        }
       
        GetReportInstanceStatus();
    }
    
    function GetReportInstanceStatus() {
        if ((InstanceResponse == null) || (InstanceResponse == "YES")) {
        PageMethods.GetRptInstanceStatus(OnCompleteList, OnErrorList, OnTimeOut);
        }
        InstanceResponse = "NO";
        return (true);
        ReportInstanceStatus();           
         }

         var ExceptionCount = 0;
        function OnCompleteList(arg) {
                    
            InstanceResponse = "YES";
            alertuser = "YES"
            arrRptInstanceList = arg;

            RptStatus = "";
            ErrorMsg = "";
            strReportsPending = "";
            strReportsRunning = "";
            RptName = "";
            viewerName = "";
            rptAction = "";
            rptFtp = "";

            RptStatus = arrRptInstanceList[0];

            if (RptStatus != "ViewHistory") {

                if (RptStatus != 'Request Submitted') {
                    ErrorMsg = arrRptInstanceList[1];
                    strReportsPending = arrRptInstanceList[2];
                    strReportsRunning = arrRptInstanceList[3];
                    RptName = arrRptInstanceList[4];
                    viewerName = arrRptInstanceList[5];
                    rptAction = arrRptInstanceList[6];
                    rptFtp = arrRptInstanceList[7];
                }

                //ReportStatus = strRptStatus;

                if (RptStatus != "") {
                    if (closetimer == "No") {
                        GetInstanceStatus(RptStatus);
                    }
                }

                if (rptAction == "") {
                    // Empty

                    if (RptStatus == "Request Submitted" || RptStatus == "Pending" || RptStatus == "Running") {
                        ExceptionCount = 0;
                        ErrorCount = 0;
                        TimeOutCount = 0;
                        ReportInstanceStatus();
                    }
                    else if (RptStatus == RptStatus == "Success" || "Failure") {
                        ExceptionCount = 0;
                        ErrorCount = 0;
                        TimeOutCount = 0;
                        closetimer = "Yes";
                        jDisplayReport = setInterval("DisplayReport(rptAction)", 5000);
                    }
                    else {

                        if (ExceptionCount == 5) {
                            DisplayErrorMsg();
                            closeProgressFrame();
                            ErrorMsg = RptStatus;
                            alert("Report Failed with Error Message " + ErrorMsg + ".");
                        }
                        ExceptionCount = ExceptionCount + 1;
                        ReportInstanceStatus();
                    }

                    arrList = "";
                } // End of Then Action = Empty
                
                
                if (rptAction == "SCHEDULE_VIEW") {
                    // Schedule and View

                    if (RptStatus == "Request Submitted" || RptStatus == "Pending" || RptStatus == "Running") {
                        ExceptionCount = 0;
                        ErrorCount = 0;
                        TimeOutCount = 0;
                        ReportInstanceStatus();
                    }
                    else if (RptStatus == "Success" || RptStatus == "Failure") {
                        ExceptionCount = 0;
                        ErrorCount = 0;
                        TimeOutCount = 0;
                        closetimer = "Yes";
                        jDisplayReport = setInterval("DisplayReport(rptAction)", 5000);
                    }
                    else {

                        if (ExceptionCount == 5) {
                            DisplayErrorMsg();
                            closeProgressFrame();
                            ErrorMsg = RptStatus;
                            alert("Report Failed with Error Message " + ErrorMsg + ".");
                        }
                        ExceptionCount = ExceptionCount + 1;
                        ReportInstanceStatus();
                    }

                    arrList = "";
                } // End of Then Schedule and View

                if (rptAction == "SCHEDULE") {
                    // Schedule

                    if (RptStatus == "Request Submitted" ) {
                        ExceptionCount = 0;
                        ErrorCount = 0;
                        TimeOutCount = 0;
                        ReportInstanceStatus();
                    }
                    else if (RptStatus == "Pending" || RptStatus == "Running" || RptStatus == "Success" || RptStatus == "Failure") {
                        ExceptionCount = 0;
                        ErrorCount = 0;
                        TimeOutCount = 0;
                        closetimer = "Yes";
                        jDisplayReport = setInterval("DisplayReport(rptAction)", 5000);
                    }
                    else {

                        if (ExceptionCount == 5) {
                            DisplayErrorMsg();
                            closeProgressFrame();
                            ErrorMsg = RptStatus;
                            alert("Report Failed with Error Message " + ErrorMsg + ".");
                        }
                        ExceptionCount = ExceptionCount + 1;
                        ReportInstanceStatus();
                    }

                    arrList = "";
                } // End of Then Schedule
                
                
                
                
                
            }  //  End of Then <> ViewHistory
        } // End of function OnCompleteList

        var TimeOutCount = 0;
        function OnTimeOut(arg) {
            InstanceResponse = "YES";
            arrRptInstanceList = arg;
            ErrorMsg = "";
            
            if (arg != null) {
                ErrorMsg = arg._exceptionType; //arrRptInstanceList[0];
            }
            
            //ReportStatus = ErrorMsg;
            if (ErrorMsg != "") {
                GetInstanceStatus(ErrorMsg);
            }

            if (TimeOutCount == 0) {
                jTimeOut = setInterval("TimedOutCloseProgressBar()", 60000);
            }
                     
            TimeOutCount = TimeOutCount + 1;
            ReportInstanceStatus();
                 
        }

        var ErrorCount = 0;
        function OnErrorList(arg) {
            InstanceResponse = "YES";
            arrRptInstanceList = arg;

            ErrorMsg = "";
            
            if (arg != null) {
                ErrorMsg = arg._exceptionType;  //arrRptInstanceList[0];
            }
            
            //ReportStatus = ErrorMsg;

            if (ErrorMsg != "") {
                GetInstanceStatus(ErrorMsg);
            }

            if (ErrorCount == 5) {
                DisplayErrorMsg();
                closeProgressFrame();
                alert("Error Message: " + ErrorMsg + ".");
            }
            ErrorCount = ErrorCount + 1;
            ReportInstanceStatus();

        }
        
        function GetInstanceStatus(status) {
            if (status != null) {
                if (document.all("lblStatus") != null) {
                    document.all("lblStatus").innerText = status;
                }
            }
        }

        function DisplayReport(rptAction) 
        {
            try {

                if (rptAction == "SCHEDULE") {
                    if (RptStatus == "Pending" || RptStatus == "Running" || RptStatus == 'Success') {
                        if (rptFtp != "") {
                            // Ftp
                            alert("Ftp B.O. Server Date & Destination: " + rptFtp);
                        }
                        closeProgressFrame();
                        clearTimeout(jDisplayReport);
                    }
                    else {
                        DisplayErrorMsg();
                        closeProgressFrame();
                        alert("Report Failed with Error Message " + ErrorMsg + ".");
                        clearTimeout(jDisplayReport);
                    }
                
                
                }
                else {

                    if (RptStatus == 'Success') {
                        if (rptFtp == "") {
                            OpenNewWindow('../Reports/ReportCeOpenWindowForm.aspx?file=file.pdf', RptName, viewerName);
                        }
                        else {
                            // Ftp
                            alert("Ftp Date & Destination: " + rptFtp);
                        }
                        closeProgressFrame();
                        clearTimeout(jDisplayReport);
                        
                    }
                    else {
                        DisplayErrorMsg();
                        closeProgressFrame();
                        alert("Report Failed with Error Message " + ErrorMsg + ".");
                        clearTimeout(jDisplayReport);
                    }
                }
            }
            catch (e) 
             {
                //DisplayErrorMsg();
                closeProgressFrame();
               // alert("Report is Completed.Please Check in Report History");
                clearTimeout(jDisplayReport);
                
             }
         }

        var newWindow;
        function OpenNewWindow(url, name, type) {
            var windowProperties = "width = " + (screen.width - 100) + ", height = " + (screen.height - 200) + ", toolbar = no, location = no, status = yes, resizable = yes, scrollbars = yes";
            if (type == 'IFRAME') {
                newWindow = window.open("../Reports/ReportCeOpenWindowForm.aspx", "", windowProperties);
                newWindow.document.title = name;
                newWindow.moveTo(50, 90);
            }
            else {
                newWindow = window.open("../Reports/ReportContainerform.aspx?title=" + name + "&url=" + url, "", windowProperties);
                newWindow.document.title = name;
                newWindow.moveTo(50, 90);
            }
        }


        function AlertIfPending() {
            if (RptStatus == "Pending") {
                if (alertuser == "YES") {
                    alert("There are " + strReportsRunning + " Reports Running And " + strReportsPending + " Reports are Pending on the Server ahead of you.Please wait...");
                }
            }
            if (RptStatus == "Running" || RptStatus == "Success" || RptStatus == "Failure") {
                clearTimeout(jTimeOut);
            }
        }

        function TimedOutCloseProgressBar() {
            if (TimeOutCount > 5) {
                DisplayErrorMsg();
                closeProgressFrame();
                alert("TimeOut Encountered: Please Try Again");
                clearTimeout(jTimeOut);
            }
        }

        function DisplayErrorMsg() {
        
            var ErrorbtnId = document.all('moReportErrorButton').value;
            if (ErrorbtnId != "") {
                var Errorbtn = parent.document.getElementById(ErrorbtnId);
            }
            else {
                var Errorbtn = parent.document.getElementById("moReportCeInputControl_btnErrorHidden");
            }

            var ReportStatusId = document.all('moReportStatus').value;
            if (ReportStatusId != "") {
                parent.document.getElementById(ReportStatusId).value = RptStatus;
            }
            else {
                parent.document.getElementById("moReportCeInputControl_moReportCeStatus").value = RptStatus;
                
            }

            var ReportErrorMsgId = document.all('moReportErrorMsg').value;
            if (ReportErrorMsgId != "") {
                parent.document.getElementById(ReportErrorMsgId).value = ErrorMsg;
            }
            else {
                parent.document.getElementById("moReportCeInputControl_moReportCeErrorMsg").value = ErrorMsg;
            }            

            Errorbtn.click();
                    
        }
        

        function sleep(ms) {
            var complete = new Date();
            complete.setTime(complete.getTime() + ms);
            while (new Date().getTime() < complete.getTime());
        }

    /*

    function UserReportStatus() {
        //   window.setTimeout('getReportStatus()', 8000);
        jTimerId1 = setInterval("getUserReportStatus()", 60000);
        //getUserReportStatus();

    }
    //window.onload = fncSetTimeout();


    var arrList = [];
    var ReportResponse = null;
    
    function getUserReportStatus() {
        if ((ReportResponse == null) || (ReportResponse == "YES")) {
            PageMethods.GetUserReportStatus(OnCompleteList1, OnErrorList1, OnTimeOut1);
        }
        ReportResponse = "NO";
        return (true);
        UserReportStatus();
    }

    function OnCompleteList1(arg) {
        ReportResponse = "YES";
        arrList = arg;
        getRptStatus(arg); 
       // UserReportStatus();        
    }

    function OnTimeOut1(arg) {
        ReportResponse = "YES";
        arrList = arg;
        getRptStatus(arg); 
        UserReportStatus();
        alert("TimeOut encountered");
    }

    function OnErrorList1(arg) {
        ReportResponse = "YES";
        arrList = arg;
        getRptStatus(arg); 
        UserReportStatus();
        alert("Error : " + arg);
    }

    var strRptStatus = "";
    function getRptStatus(status) {        
        
        for (var i = 0; i < arrList.length; i++) {
            strRptStatus = strRptStatus + arrList[i] + '\r\n';
        }
        if (strRptStatus != "") {            
            document.getElementById("navigation_menu1_rptStatus").value = strRptStatus;
        }
        arrList = "";
        strRptStatus = "";
        } 
        
*/        
        
 /*
function fncSetTimeout1() 
{ 
        window.setTimeout('alertUser()',1000);      
    }

function ShowReport() 
    {
    var xmlData
    xmlData = GetXML('../Reports/ReportCeStatusForm.aspx');
    //   alert(xmlData);
    var result = "";
    var message = "";
    var url = "";
    var rptname = "";
    var windowProperties = "width = " + (screen.width - 100) + ", height = " + (screen.height - 200) + ", toolbar = no, location = no, status = yes, resizable = yes, scrollbars = yes";
    var newWindow;
  	
    try{
    
    if (xmlData)
    {
    result = "";
    // <data><result>OK</result><message>Report ready!</message></data>
    result = xmlData.selectSingleNode("/data/result").text;
    //message = xmlData.selectSingleNode("/data/message").text;
    rpturl = xmlData.selectSingleNode("/data/url").text;
    rptname = xmlData.selectSingleNode("/data/name").text;
    rpttype = xmlData.selectSingleNode("/data/type").text;
    	    	    
    if (result == 'OK')
    {
    //  if (rptstatus == '0')
    // {
    result = "";
    if (rptname != "") 
    {	
    makeNewWindow(rpturl, rptname,rpttype);                             
    //   alert('Report ready!');
    //window.open("../Reports/ReportCeOpenWindowForm.aspx","mywindow",windowProperties);
    //window.open("../Reports/ReportContainerform.aspx","mywindow",windowProperties);
    //  window.clearTimeout();
    }
    else
    {
    alert('Report Failed!');
    }
    //  }
    //  else
    // {
    //        alert(rptname + 'Failed with'+ rptErrormsg);
    // }  
    }	    
    } 
    }
    catch (e) {
    fncSetTimeout();           
    }
    
    fncSetTimeout();           
    }


  var newWindow;
   function makeNewWindow(url, name,type)    
    {    	
      var windowProperties = "width = " + (screen.width - 100) + ", height = " + (screen.height - 200) + ", toolbar = no, location = no, status = yes, resizable = yes, scrollbars = yes";
      if (type == 'TEXT_CSV')
      {
       newWindow = window.open("../Reports/ReportCeOpenWindowForm.aspx", "", windowProperties);
       newWindow.moveTo(50, 90);                 
      }
      else
      {
       newWindow = window.open("../Reports/ReportContainerform.aspx?url=" + url + "&title=" + name, "", windowProperties);
       newWindow.moveTo(50, 90);                 
      }
	  
    }

function GetXML(postUrl) {

   var oXml = new ActiveXObject("Microsoft.XMLDOM");   
   oXml.async = false;

   var xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");   
   xmlhttp.Open("POST", postUrl, false);

   xmlhttp.send();

   if (xmlhttp.readyState == 4) {
       if (xmlhttp.status == 200) {
           oXml.loadXML(xmlhttp.responseXML.xml);
           return oXml;
       }
   }
   else {
       return ('<?xml version="1.0"?><data><result>NOT OK</result><url>1</url><name>Report NOT ready!</name><type></type></data>');
   }
   
   //	oXml.loadXML(xmlhttp.responseXML.xml);
   
   //oXml.loadXML('<?xml version="1.0"?><data><result>NOT OK</result><url>1</url><name>Report NOT ready!</name><type></type></data>'

    //	return oXml;
}

*/
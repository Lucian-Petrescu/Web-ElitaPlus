        function toggleReportFormatViewSelection(isView) {
            //debugger;
            if (isView) {
                document.getElementById(PDFControl).checked = false;
                document.getElementById(TXTControl).checked = false;
            }

        }
        function toggleReportFormatPDFSelection(isPDF) {
            //debugger;
            if (isPDF) {
                document.getElementById(VIEWControl).checked = false;
                document.getElementById(TXTControl).checked = false;
            }

        }
        function toggleReportFormatTXTSelection(isTXT) {
            //debugger;
            if (isTXT) {
                document.getElementById(VIEWControl).checked = false;
                document.getElementById(PDFControl).checked = false;
            }

        }

        var jTimerId
        function SetProgressTimeOut() {
            restoreClientOptions();
            showProgressFrame(document.title, 'ReportCeProgressForm.aspx');
            jTimerId = setInterval("VerifyProgressTimeOut()", 8000);
        }        

        function VerifyProgressTimeOut() {
            //	alert("Verify");
            if (document.getElementById(StatusControl).value != "") {
                //alert(document.getElementById('<%#moReportCeInputControl.ReportControlViewer.ClientID%>').value); // =  viewername ;
                //	closeProgressFrame();
                //	clearInterval(jTimerId);

                //	alertdocument.getElementById('<%#moReportCeInputControl.ReportControlStatus.ClientID%>').value);
                if (document.getElementById(StatusControl).value != "SUCCESS") {
                    var btn = document.getElementById(ControlErrorHidden);
                    btn.click();
                }
                else {
                    if (document.getElementById(FtpControl).value != "") {
                        // Ftp
                        alert("Ftp Date & Destination: " + document.getElementById(FtpControl).value);
                    }
                    document.getElementById(StatusControl).value = "";

                    if (document.getElementById(ViewerControl).value == "IFRAME") {
                        if (document.getElementById(ActionControl).value != "SCHEDULE") {
                            showReportViewerFrame("ReportCeOpenWindowForm.aspx?file=pdf.pdf");
                        }
                        SetCloseTimerVariable('True');
                    }
                    else {
                        closeReportViewerFrame();
                    }
                }
                document.getElementById(ProgressVisibleControl).value = "False"
                //closeProgressFrame();
            }
            CloseReportTimer();
        }

        function CloseReportTimer() {
            if (document.getElementById(ProgressVisibleControl).value == "False") {
                if (document.getElementById(CloseTimerControl).value == "True") {
                    closeProgressFrame();
                    document.getElementById(CloseTimerControl).value = "False";
                }
            }
        }

        function SetCloseTimerVariable(status) {
            document.getElementById(CloseTimerControl).value = status;
        }


        function EnableReportCe() {
            //alert("TestProgress")
            var btn = document.getElementById(ControlViewHidden);
            btn.click();

        }

        function ShowReportCeContainer() {
            document.all.item('iframe1').style.display = '';
            document.all.item('iframe1').style.visibility = 'visible';
            document.all.item('iframe1').src = "ReportCeBaseForm.aspx";
        }

        function HideReportCeContainer() {
            document.all.item('iframe1').style.display = 'none';
            document.all.item('iframe1').style.visibility = 'hidden'
            document.all.item('iframe1').src = "";
        }

        function CloseProgressBarParent(transMsg, viewerName, errorMsg, rptAction, rptFtp) {

            //alert("TestProgress2");
            document.getElementById(StatusControl).value = transMsg;
            document.getElementById(ViewerControl).value = viewerName;
            document.getElementById(ControlErrMsg).value = errorMsg;
            document.getElementById(ActionControl).value = rptAction;
            document.getElementById(FtpControl).value = rptFtp;
            //If (viewerName = Reports.ReportCeBase.RptViewer.GetName(GetType(Reports.ReportCeBase.RptViewer), Reports.ReportCeBase.RptViewer.WINDOWOPEN))
            //{
            //   buttonClick();
            //}                        
        }

        function SendReportError(transMsg, errorMsg) {
            document.getElementById(StatusControl).value = transMsg;
            document.getElementById(ControlErrMsg).value = errorMsg;
        }

        /* By Abdullah Alkhwlani
        * This function "ToggleSingleDropDownSelection" is replacing the individual functions for each form.
        * This function is used to toggle the selection between Radio Button control used for "All Selection" and 
        * a single dropdown control that is used for "single Selection". 
        * Function arguments:
        * -------------------
        * 1- ctlDropDown        -->  drop down
        * 2- ctlRadioButton     --> "All Selection" radio button
        * 3- isSingleSelection  --> True if radio button checked false otherwise
        *
        * Function Use Examples:
        * ----------------------
        * 1- From Radio button control:
        *     onclick="ToggleSingleDropDownSelection('cboDealer', 'rdealer',false);"
        *
        * 2- From drop down control:
        *     onchange="ToggleSingleDropDownSelection('cboDealer', 'rdealer', true);"
        *
        */
        function ToggleSingleDropDownSelection(ctlDropDown, ctlRadioButton, isSingleSelection) {
            var objDropDown = document.getElementById(ctlDropDown); 		//  DropDown control
            var objRadioButton = document.getElementById(ctlRadioButton);   // Radio button control

            if (isSingleSelection) {
                if (objDropDown.selectedIndex == 0) {
                    objRadioButton.checked = true;
                }
                else {
                    objRadioButton.checked = false;
                }
            }
            else {
                objDropDown.selectedIndex = -1;
            }
        }

        function ToggleSingleTextBoxSelection(ctlTextBox, ctlRadioButton, isSingleSelection) {

            var objTextBox = document.getElementById(ctlTextBox); 		//  TextBox control
            var objRadioButton = document.getElementById(ctlRadioButton);   // Radio button control

            if (isSingleSelection) {
                objRadioButton.checked = false;
            }
            else {
                objTextBox.value = "";
            }
        }

        function ToggleDoubleDropDownSelection(ctlDropDown1, ctlDropDown2, ctlRadioButton, isSingleSelection) {
            var objDropDown1 = document.getElementById(ctlDropDown1); 		//  DropDown control 1
            var objDropDown2 = document.getElementById(ctlDropDown2); 		//  DropDown control 2
            var objRadioButton = document.getElementById(ctlRadioButton); 	// Radio button control

            if (isSingleSelection) {
                objRadioButton.checked = false;
            }
            else {
                objDropDown1.selectedIndex = -1;
                objDropDown2.selectedIndex = -1;
            }
        }

        /* By Abdullah Alkhwlani
        * This function "ToggleDualDropDownsSelection" is replacing the individual functions for each form.
        * This function is used to toggle the selection between Radio Button control used for "All Selection" and 
        * dual dropdowns controls "By Code" and "By Description" that is used for "single Selection". 
        * Function arguments:
        * -------------------
        * 1- ctlCodeDropDown    --> "By Code" drop down
        * 2- ctlDecDropDown     --> "By Description" drop down
        * 3- ctlRadioButton     --> "All Selection" radio button
        * 4- isSingleSelection  --> True if radio button checked false otherwise
        * 5- change_Dec_Or_Code --> Value "D" will assigned "By Code" value to "By Description".
        *		 					 Value "C" will assigned "By Description" value to "By Code". 
        * Function Use Examples:
        * ----------------------
        * 1- From Radio button control:
        *     onclick="ToggleDualDropDownsSelection('cboServiceCentersCode', 'cboServiceCentersDec', 'rServiceCenters', false,'');"
        *
        * 2- From "By Code" control:
        *     onchange="ToggleDualDropDownsSelection('cboServiceCentersCode', 'cboServiceCentersDec', 'rServiceCenters', true,'D');"
        *
        * 3- From "By Description" control:
        *     onchange="ToggleDualDropDownsSelection('cboServiceCentersCode', 'cboServiceCentersDec', 'rServiceCenters', true,'C');"
        *
        */
        function ToggleDualDropDownsSelection(ctlCodeDropDown, ctlDecDropDown, ctlRadioButton, isSingleSelection, change_Dec_Or_Code) {
            var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
            var objDecDropDown = document.getElementById(ctlDecDropDown);   // "By Description" DropDown control 
            var objRadioButton = document.getElementById(ctlRadioButton);   // Radio button control

            if (isSingleSelection) {
                objRadioButton.checked = false;
                //Select Code or Dec drop down
                if (change_Dec_Or_Code == 'C') {
                    objCodeDropDown.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
                }
                else {
                    objDecDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
                }
            }
            else {
                objCodeDropDown.selectedIndex = -1;
                objDecDropDown.selectedIndex = -1;
            }
        }

        /* By Abdullah Alkhwlani
        * This function "SetDualDropDownsValue" is replacing the individual functions for each form.
        * This function is used to set the value of one dropdowns controls by the value of the selected one. 
        * Function arguments:
        * -------------------
        * 1- ctlCodeDropDown    --> "By Code" drop down
        * 2- ctlDecDropDown     --> "By Description" drop down
        * 3- change_Dec_Or_Code --> Value "D" will assigned "By Code" value to "By Description".
        *		 					 Value "C" will assigned "By Description" value to "By Code". 
        * Function Use Examples:
        * ----------------------
        * 1- From "By Code" control:
        *     onchange="SetDualDropDownsValue('cboServiceCentersCode', 'cboServiceCentersDec', 'D');"
        *
        * 2- From "By Description" control:
        *     onchange="SetDualDropDownsValue('cboServiceCentersCode', 'cboServiceCentersDec', 'C');"
        *
        */
        function SetDualDropDownsValue(ctlCodeDropDown, ctlDecDropDown, change_Dec_Or_Code) {
            var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
            var objDecDropDown = document.getElementById(ctlDecDropDown);   // "By Description" DropDown control 

            //Select Code or Dec drop down
            if (change_Dec_Or_Code == 'C') {
                objCodeDropDown.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
            }
            else {
                objDecDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
            }
        }
        function toggleRadioButtonSelection(ctlRadioBtn1, ctlRadioBtn2, isSelected) {
            //debugger;
            var objctlRadioBtn1 = document.getElementById(ctlRadioBtn1); 		//  DropDown control
            var objctlRadioBtn2 = document.getElementById(ctlRadioBtn2);

            if (!isSelected) {
                objctlRadioBtn2.checked = false;
            }
            else {
                objctlRadioBtn1.checked = false;
            }
        }

        function toggleCheckBox(moCheckBox1, moCheckBox2) {

            var objmoCheckBox1 = document.getElementById(moCheckBox1);
            var objmoCheckBox2 = document.getElementById(moCheckBox2);

            if (objmoCheckBox1.checked) {
                objmoCheckBox2.checked = false;
            }
            else if (objmoCheckBox2.checked) {
                objmoCheckBox1.checked = false;
            }
            else {
                objmoCheckBox1.checked = false;
                objmoCheckBox2.checked = false;
            }
        }
        /* 
        * This function "Toggle_RB_DD" 
        * This function is used to reset the "All Radio Button" and the selection of a Drop Down List and
        * set the label color back to black. 
        * FYI.  It is a good idea to name all the object with the same sofix. If you are creating Objects for Coverage use
        *   id=rbCoverage = rb for Radio Button
        *   id=lblCoverage = lbl for Label
        *   id=ddlCoverage = ddl for DropDownList
        *
        * Function arguments:
        * -------------------
        * 1- inObj              --> "Mandatory - The Objet that the scrip is assign to. Always pass This object 
        * 2- LabelName          --> "Optional  - The Id Name of the label Object.  If not pass it will search for the same object with a prefix of lbl.
        * 3- OtherObjName       --> "Optional  - The Id Name of the object that should be assosiated to the current object. If not pass it will search for the same object with a prefix of 'ddl' for dropdown list or 'rd' for RadioBitton..
        *		 					 
        * Function Use Examples:
        * ----------------------
        * 1- From Radio button control:
        *     onclick="Toggle_RB_DD(this','lblCoverage', 'ddlCoverage');"
        *
        *  - If you Radio Button was named rdCoverage and the assosiated objects where named lblCoverage and ddlCoverage you can use.
        *     onclick="Toggle_RB_DD(this');"
        *
        * 1- From DropDowncontrol:
        *     onclick="Toggle_RB_DD(this','lblCoverage', 'rdCoverage');"
        *
        *  - If you Radio Button was named rdCoverage and the assosiated objects where named lblCoverage and ddlCoverage you can use.
        *     onchange="Toggle_RB_DD(this');"
        */

        function Toggle_RB_DD(inObj, LabelName, OtherObjName) {
            var objName = inObj.id;
            if (LabelName == undefined) { var LabelName = objName.replace('rb', 'lbl'); }

            var objLabel = document.getElementById(LabelName);


            if (objLabel) { objLabel.style.color = ''; }

            if (inObj.nodeName == 'INPUT') {
                if (inObj.type == 'radio') {
                    if (OtherObjName == undefined) { var OtherObjName = objName.replace('rb', 'ddl'); }

                    var objDropDown = document.getElementById(OtherObjName);

                    if (objDropDown) { objDropDown.selectedIndex = -1; }
                }
            }

            if (inObj.nodeName == 'SELECT') {
                if (OtherObjName == undefined) { var OtherObjName = objName.replace('ddl', 'rb'); }

                var objRadioButton = document.getElementById(OtherObjName);

                if (objRadioButton) { objRadioButton.checked = false; }
            }

        }	

        
               
function ToggleSelection(ctlCodeDropDown, ctlDescDropDown, change_Desc_Or_Code, lblCaption) {
    var objCodeDropDown = document.getElementById(ctlCodeDropDown);
    var objDescDropDown = document.getElementById(ctlDescDropDown);
    if (change_Desc_Or_Code == 'C') {
        objCodeDropDown.value = objDescDropDown.options[objDescDropDown.selectedIndex].value;
    }
    else {
        objDescDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
    }
    if (lblCaption != '') {
        document.all.item(lblCaption).style.color = '';
    }
}
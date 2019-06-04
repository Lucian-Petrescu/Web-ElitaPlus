// JScript File

// Insert on Target and Delete Source
//function UpdateInsertDelete(sourceTexts, sourceValues, targetTexts, targetValues, targetOption)
//{
//    var separator = "@"; //  Common.AppConfig.FIELD_SEPARATOR
//    var found = false;
//    
//    if ( sourceValues.value != "" ) {
//        // Update Source
//        arrSourceTexts = new Array(); 
//        arrSourceValues = new Array();
//        arrSourceTexts = sourceTexts.value.split(separator);
//        arrSourceValues = sourceValues.value.split(separator);
//        sourceTexts.value = "";
//        sourceValues.value = "";
//    
//    for (var i = 0; i < arrSourceValues.length; i++) {
//        if (arrSourceValues[i] == targetOption.value){
//            // The new value in Source. Therefore, we need to delete it from source
//            found = true;
//         }  // if
//         else {
//            if ( sourceValues.value == "" ) {
//                // First Value
//                    sourceTexts.value = arrSourceTexts[i];
//                    sourceValues.value = arrSourceValues[i];
//            } // if 
//              else {
//                    sourceTexts.value = sourceTexts.value + separator + arrSourceTexts[i];
//                    sourceValues.value = sourceValues.value + separator + arrSourceValues[i];
//              } // else 
//         }  // else
//    } // for
//    } // if source
//    
//    // Target
//    if ( found == false) {
//        // If not found in Source, then we insert in the Target
//        if ( targetTexts.value == "" ) {
//            // First Value
//                    targetTexts.value = targetOption.text;
//                    targetValues.value = targetOption.value;
//         } // if
//              else  {
//                    targetTexts.value = targetTexts.value + separator + targetOption.text;
//                    targetValues.value = targetValues.value + separator + targetOption.value;
//                    } // else      
//    } // if found
//}

// If the Selected Value is in Source Then delete it from source
function UpdateSource(source, selected)
{
    var separator = "¦"; //  Common.AppConfig.FIELD_SEPARATOR
    var found = false;
    var pos = -1;
    var modSource;
    var sourceLength, selectedLenght;
    
        sourceLength = source.value.length;
        selectedLenght = selected.length;
        modSource = separator + source.value + separator;
        pos = modSource.indexOf(separator + selected + separator);
        if ( pos > -1 ) {
            // Found @item@
            found = true;
            if ( pos == 0 ) {
                // The item is in first position. Remove item
                if ( sourceLength == selectedLenght ) {
                    // The item to remove is the only item that is in source
                    source.value = "";
                } else {
                    source.value = source.value.substring(selectedLenght + 1);
                }
            } else {
                // The item is the middle or last position. Remove @item
                modSource = source.value.substring(0, pos - 1);
                if ( sourceLength == ( pos + selectedLenght ) ) {
                    // The item is the last position.
                    source.value = modSource;
                } else {
                    // The item is the middle position.
                    source.value = modSource + source.value.substring(pos + selectedLenght);
                }
            } // END The item is the middle or last position
        } // END Found @item@
        return found;
}

function UpdateTarget(targetTexts, targetValues, targetTextOption, targetValueOption)
{
    var separator = "¦"; //  Common.AppConfig.FIELD_SEPARATOR
    
    // If not found in Source, then we insert in the Target
        if ( targetTexts.value == "" ) {
            // First Value
                    targetTexts.value = targetTextOption;
                    targetValues.value = targetValueOption;
         } // if
         else  {
                    targetTexts.value = targetTexts.value + separator + targetTextOption;
                    targetValues.value = targetValues.value + separator + targetValueOption;
                } // else
}

// If the Selected Value is in Source Then delete it from source
//                                    Else insert it in target
function UpdateInsertDelete(sourceTexts, sourceValues, targetTexts, targetValues, targetOption)
{
  //  var separator = "@"; //  Common.AppConfig.FIELD_SEPARATOR
    var found = false;
    
    if ( sourceValues.value != "" ) {
        found = UpdateSource(sourceValues, targetOption.value);
        if ( found == true ) {
         UpdateSource(sourceTexts, targetOption.text);
        }
    } // END if source != ""
    
    // Target
    if ( found == false) {
        UpdateTarget(targetTexts, targetValues, targetOption.text, targetOption.value);
//        // If not found in Source, then we insert in the Target
//        if ( targetTexts.value == "" ) {
//            // First Value
//                    targetTexts.value = targetOption.text;
//                    targetValues.value = targetOption.value;
//         } // if
//              else  {
//                    targetTexts.value = targetTexts.value + separator + targetOption.text;
//                    targetValues.value = targetValues.value + separator + targetOption.value;
//                    } // else      
    } // if found
}

//// Remove the Selected elements from source
//function RemoveFromList(sourceBox, targetBox)
//{
//	for(var i = 0; i < sourceBox.options.length; i++) {
//	    for(var j = 0; j < targetBox.options.length; j++) {
//	        if (sourceBox.options[i].text == targetBox.options[j].text) {
//	            targetBox.remove(j);
//	            break;
//	        }    
//	    }        
//	}
//}

// Remove the Selected elements from source
//function RemoveFromList(sourceBox, targetBox, isAll)
//{
//    if ( isAll == true )
//    {   // Remove all elements
//        sourceBox.options.length = 0;
//    }
//    else {
//	    for(var i = 0; i < targetBox.options.length; i++) {
//	        for(var j = 0; j < sourceBox.options.length; j++) {
//	            if (targetBox.options[i].text == sourceBox.options[j].text) {
//	                sourceBox.remove(j);
//	                break;
//	            }    
//	        }        
//	    }
//	} // END else
//}

function RemoveFromList(sourceBox, index, isAll)
{
    if ( isAll == false )
    {
        sourceBox.remove(index);
    } else {   // Remove all elements
        sourceBox.options.length = 0;
    }
//    else {
//	    for(var i = 0; i < targetBox.options.length; i++) {
//	        for(var j = 0; j < sourceBox.options.length; j++) {
//	            if (targetBox.options[i].text == sourceBox.options[j].text) {
//	                sourceBox.remove(j);
//	                break;
//	            }    
//	        }        
//	    }
//	} // END else
}

//function FilterSelected(element, index, source)
//{
//    return element.selected
//}

function AddSelectedToList(sourceBox, sourceTexts, sourceValues, targetBox, targetTexts, targetValues)
{
	for(var i = 0; i < sourceBox.options.length; i++) {
	    if (sourceBox.options[i].selected) {
	        var targetOption = new Option(sourceBox.options[i].text, 
	            sourceBox.options[i].value, false, false);
	        targetBox.add(targetOption);
	        UpdateInsertDelete(sourceTexts, sourceValues, targetTexts, targetValues, targetOption);
	        RemoveFromList(sourceBox, i, false);
	        i--;
	    }
	}
}

//function AddSelectedToList(sourceBox, sourceTexts, sourceValues, targetBox, targetTexts, targetValues)
//{
//    var selectedArr;
//    
//    selectedArr = sourceBox.options.filter(FilterSelected);
//	for(var i = 0; i < sourceBox.options.length; i++) {
//	    if (sourceBox.options[i].selected) {
//	        var targetOption = new Option(sourceBox.options[i].text, 
//	            sourceBox.options[i].value, false, false);
//	        targetBox.add(targetOption);
//	        UpdateInsertDelete(sourceTexts, sourceValues, targetTexts, targetValues, targetOption);
//	    }
//	}
//}

//function AddAllToList(sourceBox, sourceTexts, sourceValues, targetBox, targetTexts, targetValues)
//{
//	for(var i = 0; i < sourceBox.options.length; i++) {
//	        var targetOption = new Option(sourceBox.options[i].text, 
//	            sourceBox.options[i].value, false, false);
//	        targetBox.add(targetOption);
//	        UpdateInsertDelete(sourceTexts, sourceValues, targetTexts, targetValues, targetOption);
//	}
//	RemoveFromList(sourceBox, -1, true);
//}

function AddAllToList(sourceBox, sourceTexts, sourceValues, targetBox, targetTexts, targetValues)
{
    var separator = "¦"; //  Common.AppConfig.FIELD_SEPARATOR
    var arrTargetTexts = new Array();
    var arrTargetValues = new Array();
    var strTargetTexts;
    var strTargetValues;
    var modSource;
    var pos;
    var index = -1;
    
    modSource = separator + sourceValues.value + separator;
    for(i=0; i<sourceBox.options.length; i++)  {
        var targetOption = new Option(sourceBox.options[i].text, 	            
        	            sourceBox.options[i].value, false, false);
	        targetBox.add(targetOption);
        pos = modSource.indexOf(separator + sourceBox.options[i].value + separator);
        if ( pos == -1 ) {
            // The Selected Value is not in Source, Then Insert in Target	
            index++;
            arrTargetTexts[index] = sourceBox.options[i].text;
            arrTargetValues[index] = sourceBox.options[i].value;
        }   
    }   // END for
    if ( index > -1 ) {
        // insert new targets
        strTargetTexts = arrTargetTexts.join(separator);
        strTargetValues = arrTargetValues.join(separator);
        UpdateTarget(targetTexts, targetValues, strTargetTexts, strTargetValues);
    }
    // Clean Source
    sourceTexts.value = "";
    sourceValues.value = "";
//	for(var i = 0; i < sourceBox.options.length; i++) {
//	        var targetOption = new Option(sourceBox.options[i].text, 
//	            sourceBox.options[i].value, false, false);
//	        targetBox.add(targetOption);
//	        UpdateInsertDelete(sourceTexts, sourceValues, targetTexts, targetValues, targetOption);
//	}
	RemoveFromList(sourceBox, -1, true);
}

//function Sort(lb)
//{
//    arrToSort = new Array();
//    arrTexts = new Array();
//    arrValues = new Array();
//    for(i=0; i<lb.length; i++)  {
//        arrToSort[i] = lb.options[i].text;
//        arrTexts[i] = lb.options[i].text;
//        arrValues[i] = lb.options[i].value;
//    }
//    arrToSort.sort();
//    for(i=0; i<lb.length; i++)  {
//        lb.options[i].text = arrToSort[i];
//        for(j=0; j<lb.length; j++)  {
//            if (arrTexts[j] == arrToSort[i]){
//                lb.options[i].value = arrValues[j];
//                break;
//            }
//        }
//    }
//}
function myObject(text,value)
{
    this.text = text;
    this.value = value;
}

function setObject(option, index, text, value)
{
    option[index] = new myObject(text,value);
}

function compareText(a,b)
{
    var res = 0;
  
   if ( a.text > b.text )
   { 
        res = 1;
   } else if ( a.text < b.text )
   {
        res = -1;
   }
   return res;
}


function Sort(lb)
{
    var arrOption = new Array();
    var index = -1;
    // Copy to a Temporary Array for Sorting
    for(i=0; i<lb.length; i++)  {
        setObject(arrOption, i, lb.options[i].text, lb.options[i].value)
    }
    arrOption.sort(compareText);
    // Copy The Sorted Array to the options
    for(i=0; i<lb.length; i++)  {
        lb.options[i].text = arrOption[i].text;
        lb.options[i].value = arrOption[i].value;
    }

}

function ExecuteAction(action, sender)
{
    var parentId = sender.parentElement.parentElement.parentElement.parentElement.id;
    var endPos = parentId.indexOf("_moButtonsTable");
    var userControlName = parentId.substring(0, endPos);
    var available = document.getElementById(userControlName + "_moAvailableList");
    var availableTexts = document.getElementById(userControlName + "_moAvailableTexts");
    var availableValues = document.getElementById(userControlName + "_moAvailableValues");
    var selected = document.getElementById(userControlName + "_moSelectedList");
    var selectedTexts = document.getElementById(userControlName + "_moSelectedTexts");
    var selectedValues = document.getElementById(userControlName + "_moSelectedValues");
    var source, target;
     
    switch(action) {
        case "Add":       
            source = available;
            target = selected;
            if (source.options.length == 0) {
                return;
            }
            AddSelectedToList(source, availableTexts, availableValues, target, selectedTexts, selectedValues); 
        //    RemoveFromList(source, target, false);  
            break;
        case "AddAll":
            source = available;
            target = selected;
            if (source.options.length == 0) {
                return;
            }
            AddAllToList(source, availableTexts, availableValues, target, selectedTexts, selectedValues);
       //     RemoveFromList(source, target, true);
            break;
        case "Remove":
            source = selected;
            target = available;
            if (source.options.length == 0) {
                return;
            }
            AddSelectedToList(source, selectedTexts, selectedValues, target, availableTexts, availableValues);
        //    RemoveFromList(source, target, false);
            break;
        case "RemoveAll":
            source = selected;
            target = available;
            if (source.options.length == 0) {
                return;
            }
            AddAllToList(source, selectedTexts, selectedValues, target, availableTexts, availableValues);
       //     RemoveFromList(source, target, true);
            break;
        default:
            break;
    }
	Sort(target);
//	RemoveFromList(target, source);
}

function AddAction(sender) 
{
    ExecuteAction("Add", sender);
    if (sender.ownerDocument.location.pathname.indexOf("Reports") > 0 ) {
        DisableReportControls();
    }    
}

function AddAllAction(sender)
{
    ExecuteAction("AddAll", sender);
    if (sender.ownerDocument.location.pathname.indexOf("Reports") > 0) {
        DisableReportControls();
    }  
}

function RemoveAction(sender)
{
    ExecuteAction("Remove", sender);
    if (sender.ownerDocument.location.pathname.indexOf("Reports") > 0) {
        DisableReportControls();
    }  
}

function RemoveAllAction(sender)
{
    ExecuteAction("RemoveAll", sender);
    if (sender.ownerDocument.location.pathname.indexOf("Reports") > 0) {
        DisableReportControls();
    }  
}

function DisableReportControls() {
    //For Reports, uncheck all dealers radio button and dealer dropdowns
    var chk = document.getElementById("rdealer");
    var dlrCode = document.getElementById("multipleDropControl_moMultipleColumnDrop");
    var dlrDesc = document.getElementById("multipleDropControl_moMultipleColumnDropDesc");
    var dlrGroup = document.getElementById("moDealerGroupList");
    chk.checked = false;
    dlrCode.selectedIndex = 0;
    dlrDesc.selectedIndex = 0;
    dlrGroup.selectedIndex = 0;
}

function RemoveAllSelectedDealersForReports(userControlName) {
    var userControlName = userControlName;
    var available = document.getElementById(userControlName + "_moAvailableList");
    var availableTexts = document.getElementById(userControlName + "_moAvailableTexts");
    var availableValues = document.getElementById(userControlName + "_moAvailableValues");
    var selected = document.getElementById(userControlName + "_moSelectedList");
    var selectedTexts = document.getElementById(userControlName + "_moSelectedTexts");
    var selectedValues = document.getElementById(userControlName + "_moSelectedValues");
    var source, target;

    source = selected;
    target = available;
    if (source.options.length == 0) {
        return;
            }
     AddAllToList(source, selectedTexts, selectedValues, target, availableTexts, availableValues);
    
    Sort(target);
}
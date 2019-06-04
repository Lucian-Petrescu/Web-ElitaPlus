      var strOldText;
        var dvObject;
        var cnt = 0;
        var arrList = [];
        var bDone = false; 
       
//       window.onload = GetAvailableList()
//       {
//          getComunaList(dropdownName)
//       }
       

       function getComunaList(txtProvinceName) {
         bDone = false; 
          if(document.getElementById(txtProvinceName) == null)
              {
               return(true);
              } 
            var regionID = document.getElementById(txtProvinceName).value;

            //Assurant.ElitaPlus.ElitaPlusWebApp.RetrieveComuna.GetListOfComunas(regionID,OnCompleteList, OnErrorList, OnTimeOut);
            // Move this page method to the base class 
            PageMethods.GetListOfComunas(regionID,OnCompleteList);
            return (true);
        }
        
        function getCompleteComunaList() {
           
           var countryID = document.getElementById(txtName).value;
            RetrieveComuna.GetListOfComunas(countryID,OnCompleteList, OnErrorList, OnTimeOut);

            return (true);
        }
        
        function getList(txtName,txtProvinceName) {
            var txtSearch;
            var intKeyCode;
            
           
            
            // The initial load may not have the data
            if(arrList.length == 0)
            {
               // NOTE: bDone is set is to avoid repeated calls to countries that have no comuna.
                if(bDone == true)
                    {
                        return(true); 
                    }
              getComunaList(txtProvinceName);
              return(true); 
            }
            
            intKeyCode = event.keyCode;

            // See if a valid key key is pressed such as 0-9, A-Z, a-z, Hyphen, Underscore
            if (((intKeyCode >= 48) && (intKeyCode <= 57)) || // Numbers 0-9
        ((intKeyCode >= 65) && (intKeyCode <= 90)) || // Upper case A-Z
        ((intKeyCode >= 97) && (intKeyCode <= 122)) ||  // Lower case a-z
        (intKeyCode == 189)) // Hyphen
            {
               
                txtSearch = document.getElementById(txtName).value;
                strOldText = txtSearch;
                
                // Loop through the arraylist and find the word
                arrList.sort();
                for (var i = 0; i < arrList.length; i++) {
                    var txt = arrList[i] ;
                    var pos = txt.toUpperCase().indexOf(txtSearch.toUpperCase(), 0);
                    if (pos == 0) {
                        document.getElementById(txtName).value = arrList[i];
                        break;
                    }
                }
                // Select the text inside the textbox (except the prefix)
                SelectText(strOldText.length, document.getElementById(txtName).value.length,txtName);
                return (true);
            }
            else // return false. 
                return (false);
        }
       function getCompleteList(txtName,txtProvince) {
            var txtSearch;
            var intKeyCode;
            
            intKeyCode = event.keyCode;

            // See if a valid key key is pressed such as 0-9, A-Z, a-z, Hyphen, Underscore
            if (((intKeyCode >= 48) && (intKeyCode <= 57)) || // Numbers 0-9
        ((intKeyCode >= 65) && (intKeyCode <= 90)) || // Upper case A-Z
        ((intKeyCode >= 97) && (intKeyCode <= 122)) ||  // Lower case a-z
        (intKeyCode == 189)) // Hyphen
            {
               
                txtSearch = document.getElementById(txtName).value;
                strOldText = txtSearch;
                 var provincia = document.getElementById(txtProvince).value;
                // Loop through the arraylist and find the word
                //arrList.sort();
                var iCount = 0;
                for(var i = 0; i < dvObject.length; i++)
                {
                     if(provincia == dvObject[i].Province)
                     {
                       arrList[iCount] = dvObject[i].Comuna;
                       iCount = iCount + 1;
                     }
                }
                arrList.sort();
                for (var i = 0; i < arrList.length; i++) {
                    var txt = arrList[i] ;
                    var pos = txt.toUpperCase().indexOf(txtSearch.toUpperCase(), 0);
                    if (pos == 0) {
                        document.getElementById(txtName).value = arrList[i];
                        break;
                    }
                }
                // Select the text inside the textbox (except the prefix)
                SelectText(strOldText.length, document.getElementById(txtName).value.length,txtName);
                return (true);
            }
            else // return false. 
                return (false);
        }

        
        function OnCompleteList(arg) {
            arrList = arg; 
            bDone = true;             
        }

        function OnTimeOut(arg) {
            alert("TimeOut encountered");
        }

        function OnErrorList(arg) {
            alert("Error : " + arg);
        }

        function SelectText(intStart, intLength,txtName) {
            var myElement = document.getElementById(txtName); // get reference to the Textbox 
            if (myElement.createTextRange) // If IE
            {
                var myRange = myElement.createTextRange();
                myRange.moveStart("character", intStart);
                myRange.moveEnd("character", intLength - myElement.value.length);
                myRange.select();
            }
            else if (myElement.setSelectionRange) // if FireFox
            {
                myElement.setSelectionRange(intStart, intLength);
            }

            myElement.focus();
        }

        function SetUniqueRadioButton(current) {
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'radio') {
                    elm.checked = false;
                }
            }
            current.checked = true;
        }
/***************************************************************************************************************************/
/************************ DYNAMIC FULFILLMENT CODE *************************************************************************/
/***************************************************************************************************************************/
/**
 * CustomEvent() polyfill
 * Use for IE 9 and up to allow implementation of Custom Events
 * https://developer.mozilla.org/en-US/docs/Web/API/CustomEvent/CustomEvent#Polyfill
 */
(function () {

    if (typeof window.CustomEvent === "function") return false;

    function CustomEvent(event, params) {
        params = params || { bubbles: false, cancelable: false, detail: null };
        var evt = document.createEvent('CustomEvent');
        evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
        return evt;
    }

    window.CustomEvent = CustomEvent;
})();

window.addEventListener('dynamicFulfillmentWorkCompletedEvent', function (e) {    
    if (document.getElementsByName('ctl00$BodyPlaceHolder$btnLegacyContinue') === null || document.getElementsByName('ctl00$BodyPlaceHolder$btnContinue') === null) {
        __doPostBack('', '');
    }
    if (e.detail.isLegacy) {  
        var hdnInput = document.getElementById('hdnInput');
        $("#" + hdnInput.value).val(JSON.stringify(e.detail));
        __doPostBack('ctl00$BodyPlaceHolder$btnLegacyContinue', 'OnClick');
    }
    else {        
        __doPostBack('ctl00$BodyPlaceHolder$btnContinue', 'OnClick');
    }
   
});
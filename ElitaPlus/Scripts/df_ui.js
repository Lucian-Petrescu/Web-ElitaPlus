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
    //selectedOption.value = e.detail.fulfillmentOption;
    //instanceIdField.value = e.detail.authorizationId;
    //ticketIdField.value = e.detail.offerId;
    //isLegacy.checked = e.detail.isLegacy;
    if (e.detail.isLegacy) {
        //needToConfirm = false;
        __doPostBack('ctl00$BodyPlaceHolder$btnLegacyContinue', 'OnClick');
    }
    else {
        ////needToConfirm = false;
        ////showNextButton(true);
        __doPostBack('ctl00$BodyPlaceHolder$btnContinue', 'OnClick');
    }
   
});
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DynamicFulfillmentUI.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DynamicFulfillmentUI" %>
<%If Not IsLoadError Then %>
<link rel="stylesheet" type="text/css" href="<%= CssUri %>" />
<div id="app"></div>
<script type="text/javascript">
      
    window.bootstrapDynamicFulfillmentApp = {
        claimNumber: '<%= ClaimNumber %>',
        sourceSystem: '<%= SourceSystem %>',
        accessToken: '<%= AccessToken %>',
        subscriptionKey: '<%= SubscriptionKey %>'
    };
</script>
<script type="text/javascript">
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


    </script>
<script type="text/javascript" src="<%= ScriptUri %>"></script>
<%Else %>
    <p>Error loading Dynamic Fulfillment UI</p>
<%End If %>
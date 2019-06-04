/*
*  Creates the tab control using Jquery tabs.
*/
(function ($) {
  $(function () {
    var tabControl = $("#tabs");

    if (!(tabControl.length > 0)) {
      // Tab not exists in page hence return.
      return;
    }

    // Tab exists in page.
    var selectedTab = $("input[id$='hdnSelectedTab']");
    var disabledTabsCtrl = $("input[id$='hdnDisabledTab']");

    if (disabledTabsCtrl.length > 0) {
      // Disable tabs control exists in page so create tab control with Enable/Disable tab functionality.
      var disabledTabs = disabledTabsCtrl.val().split(',');

      // Disable tabs.
      var disabledTabsIndexArr = [];
      $.each(disabledTabs, function () {
        var tabIndex = parseInt(this);
        if (tabIndex != NaN) {
            disabledTabsIndexArr.push(tabIndex);
        }
      });

      tabControl.tabs({
        active: selectedTab.val(),
        disabled: disabledTabsIndexArr,

        activate: function () {
          selectedTab.val(tabControl.tabs('option', 'active'));
        }
      });
    }
    else {
      if (selectedTab.length > 0) {
        // Select tabs control exists in page so create multi tabs control.
        tabControl.tabs({
            active: selectedTab.val(),

            activate: function () {
              selectedTab.val(tabControl.tabs('option', 'active'));
            }
        });
      }
      else {
        // Create single tab control.
        tabControl.tabs({
            active: 0
        });
      }
    }
  });
})(jQuery);
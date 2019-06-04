/*
*  If the Parent window url is not having ‘MainPage’ then redirect to ‘default.aspx’ page. 
*/

if (window.parent.location.href.toLowerCase().indexOf('mainpage') == -1) {

  var urlPath = location.pathname.split('/');

  if (urlPath.length > 1) {
    window.location = location.protocol + '//' + location.host + '/' + urlPath[1] + '/default.aspx';
  }
}

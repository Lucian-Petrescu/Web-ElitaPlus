Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class wsUpdateServiceCenter
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function UpdateServiceCenter(ByVal Token As String, ByVal UpdateRequest As ServiceCenterUpdateRequest) As String



    End Function

  

End Class
Imports System.ServiceModel

' NOTE: If you change the class name "GvsWcf" here, you must also update the reference to "GvsWcf" in Web.config and in the associated .svc file.

Namespace Gvs

    <ServiceBehavior(Namespace:="http://elita.assurant.com/gvsNamespace")> _
    Public Class GvsWcf
        Inherits ElitaWcf
        Implements Gvs.IGvsWcf

#Region "Operations"
        Public Function Hello(ByVal name As String) As String Implements IGvsWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IGvsWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(ByVal networkID As String, ByVal password As String, _
                        ByVal group As String) As String Implements IGvsWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(ByVal token As String, _
                                    ByVal functionToProcess As String, _
                                    ByVal xmlStringDataIn As String) As String _
                                        Implements IGvsWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "GVSWS")
            Return sRet
        End Function

#End Region

    End Class

End Namespace

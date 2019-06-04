Imports System.ServiceModel

' NOTE: If you change the class name "OlitaWcf" here, you must also update the reference to "OlitaWcf" in Web.config and in the associated .svc file.
Namespace Olita

    <ServiceBehavior(Namespace:="http://elita.assurant.com/olitaNamespace")> _
    Public Class OlitaWcf
        Inherits ElitaWcf
        Implements Olita.IOlitaWcf

#Region "Operations"

        Public Function Hello(ByVal name As String) As String Implements IOlitaWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IOlitaWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) _
                                            As String Implements IOlitaWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(ByVal token As String, _
                                    ByVal functionToProcess As String, _
                                    ByVal xmlStringDataIn As String) As String _
                                        Implements IOlitaWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "OLITAWS")
            Return sRet
        End Function

#End Region

    End Class

End Namespace

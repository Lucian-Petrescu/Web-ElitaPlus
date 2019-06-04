Imports System.ServiceModel

' NOTE: If you change the class name "VscWcf" here, you must also update the reference to "VscWcf" in Web.config and in the associated .svc file.
Namespace Vsc

    <ServiceBehavior(Namespace:="http://elita.assurant.com/vscNamespace")> _
    Public Class VscWcf
        Inherits ElitaWcf
        Implements Vsc.IVscWcf

#Region "Operations"

        Public Function Hello(ByVal name As String) As String Implements IVscWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IVscWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) _
                                            As String Implements IVscWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(ByVal token As String, _
                                    ByVal functionToProcess As String, _
                                    ByVal xmlStringDataIn As String) As String _
                                        Implements IVscWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "VSCWS")
            Return sRet
        End Function

#End Region


    End Class

End Namespace

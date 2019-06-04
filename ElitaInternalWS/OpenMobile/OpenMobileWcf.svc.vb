Imports System.ServiceModel

' NOTE: If you change the class name "OpenMobileWcf" here, you must also update the reference to "OpenMobileWcf" in Web.config and in the associated .svc file.
Namespace OpenMobile

    <ServiceBehavior(Namespace:="http://elita.assurant.com/openmobileNamespace")> _
    Public Class OpenMobileWcf
        Inherits ElitaWcf
        Implements OpenMobile.IOpenMobileWcf

#Region "Operations"

        Public Function Hello(ByVal name As String) As String Implements IOpenMobileWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IOpenMobileWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) _
                                            As String Implements IOpenMobileWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(ByVal token As String, _
                                    ByVal functionToProcess As String, _
                                    ByVal xmlStringDataIn As String) As String _
                                        Implements IOpenMobileWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "OPENMOBILEWS")
            Return sRet
        End Function

#End Region

    End Class

End Namespace


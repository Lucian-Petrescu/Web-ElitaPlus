Imports System.ServiceModel

' NOTE: If you change the class name "GalaxyWcf" here, you must also update the reference to "GalaxyWcf" in Web.config and in the associated .svc file.
Namespace Galaxy

    <ServiceBehavior(Namespace:="http://elita.assurant.com/galaxyNamespace")> _
    Public Class GalaxyWcf
        Inherits ElitaWcf
        Implements Galaxy.IGalaxyWcf

#Region "Operations"

        Public Function Hello(ByVal name As String) As String Implements IGalaxyWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IGalaxyWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) _
                                            As String Implements IGalaxyWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(ByVal token As String, _
                                    ByVal functionToProcess As String, _
                                    ByVal xmlStringDataIn As String) As String _
                                        Implements IGalaxyWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "GALAXYWS")
            Return sRet
        End Function

#End Region


    End Class

End Namespace

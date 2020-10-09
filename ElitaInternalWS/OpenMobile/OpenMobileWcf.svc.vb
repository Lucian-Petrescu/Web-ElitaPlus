Imports System.ServiceModel

' NOTE: If you change the class name "OpenMobileWcf" here, you must also update the reference to "OpenMobileWcf" in Web.config and in the associated .svc file.
Namespace OpenMobile

    <ServiceBehavior(Namespace:="http://elita.assurant.com/openmobileNamespace")> _
    Public Class OpenMobileWcf
        Inherits ElitaWcf
        Implements OpenMobile.IOpenMobileWcf

#Region "Operations"

        Public Function Hello(name As String) As String Implements IOpenMobileWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IOpenMobileWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(networkID As String, password As String, group As String) _
                                            As String Implements IOpenMobileWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(token As String, _
                                    functionToProcess As String, _
                                    xmlStringDataIn As String) As String _
                                        Implements IOpenMobileWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "OPENMOBILEWS")
            Return sRet
        End Function

#End Region

    End Class

End Namespace


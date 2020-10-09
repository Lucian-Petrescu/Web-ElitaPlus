Imports System.ServiceModel

' NOTE: If you change the class name "GvsWcf" here, you must also update the reference to "GvsWcf" in Web.config and in the associated .svc file.

Namespace Gvs

    <ServiceBehavior(Namespace:="http://elita.assurant.com/gvsNamespace")> _
    Public Class GvsWcf
        Inherits ElitaWcf
        Implements Gvs.IGvsWcf

#Region "Operations"
        Public Function Hello(name As String) As String Implements IGvsWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IGvsWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(networkID As String, password As String, _
                        group As String) As String Implements IGvsWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(token As String, _
                                    functionToProcess As String, _
                                    xmlStringDataIn As String) As String _
                                        Implements IGvsWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "GVSWS")
            Return sRet
        End Function

#End Region

    End Class

End Namespace

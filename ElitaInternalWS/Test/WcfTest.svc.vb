' NOTE: If you change the class name "WcfTest" here, you must also update the reference to "WcfTest" in Web.config and in the associated .svc file.
Imports System.ServiceModel
Namespace Test

    <ServiceBehavior(Namespace:="http://elita.assurant.com/testNamespace")> _
    Public Class WcfTest
        Inherits ElitaWcf
        Implements Test.IWcfTest

#Region "Operations"
        Public Function Hello(ByVal name As String) As String Implements IWcfTest.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IWcfTest.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(ByVal networkID As String, ByVal password As String, ByVal group As String) _
                                            As String Implements IWcfTest.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(ByVal token As String, _
                                    ByVal functionToProcess As String, _
                                    ByVal xmlStringDataIn As String) As String _
                                        Implements IWcfTest.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "OLITAWS")
            Return sRet
        End Function

#End Region


    End Class

End Namespace

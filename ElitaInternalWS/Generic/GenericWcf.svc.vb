
Imports System.ServiceModel

' NOTE: If you change the class name "GenericWcf" here, you must also update the reference to "GenericWcf" in Web.config and in the associated .svc file.

Namespace Generic

    <ServiceBehavior(Namespace:="http://elita.assurant.com/genericNamespace")> _
    Public Class GenericWcf
        Inherits ElitaWcf
        Implements Generic.IGenericWcf

#Region "Operations"
        Public Function Hello(ByVal name As String) As String Implements IGenericWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IGenericWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(ByVal networkID As String, ByVal password As String, _
                        ByVal group As String) As String Implements IGenericWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(ByVal token As String, _
                                    ByVal functionToProcess As String, _
                                    ByVal xmlStringDataIn As String) As String _
                                        Implements IGenericWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "GenericWS")
            Return sRet
        End Function

#End Region

    End Class

End Namespace

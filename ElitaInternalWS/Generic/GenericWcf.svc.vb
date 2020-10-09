
Imports System.ServiceModel

' NOTE: If you change the class name "GenericWcf" here, you must also update the reference to "GenericWcf" in Web.config and in the associated .svc file.

Namespace Generic

    <ServiceBehavior(Namespace:="http://elita.assurant.com/genericNamespace")> _
    Public Class GenericWcf
        Inherits ElitaWcf
        Implements Generic.IGenericWcf

#Region "Operations"
        Public Function Hello(name As String) As String Implements IGenericWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IGenericWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(networkID As String, password As String, _
                        group As String) As String Implements IGenericWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(token As String, _
                                    functionToProcess As String, _
                                    xmlStringDataIn As String) As String _
                                        Implements IGenericWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "GenericWS")
            Return sRet
        End Function

#End Region

    End Class

End Namespace

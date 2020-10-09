Imports System.ServiceModel

' NOTE: If you change the class name "VscWcf" here, you must also update the reference to "VscWcf" in Web.config and in the associated .svc file.
Namespace Vsc

    <ServiceBehavior(Namespace:="http://elita.assurant.com/vscNamespace")> _
    Public Class VscWcf
        Inherits ElitaWcf
        Implements Vsc.IVscWcf

#Region "Operations"

        Public Function Hello(name As String) As String Implements IVscWcf.Hello
            Dim sRet As String

            sRet = MyBase.Hello(name)
            Return sRet
        End Function

        Public Function Login() As String Implements IVscWcf.Login
            Dim sRet As String

            sRet = MyBase.Login()
            Return sRet
        End Function

        Public Function LoginBody(networkID As String, password As String, group As String) _
                                            As String Implements IVscWcf.LoginBody
            Dim sRet As String

            sRet = MyBase.LoginBody(networkID, password, group)
            Return sRet
        End Function

        Public Function ProcessRequest(token As String, _
                                    functionToProcess As String, _
                                    xmlStringDataIn As String) As String _
                                        Implements IVscWcf.ProcessRequest
            Dim sRet As String

            sRet = MyBase.ProcessRequest(token, functionToProcess, xmlStringDataIn, "VSCWS")
            Return sRet
        End Function

#End Region


    End Class

End Namespace

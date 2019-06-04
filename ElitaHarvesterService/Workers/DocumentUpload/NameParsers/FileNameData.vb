Imports System.Reflection

Namespace Workers.DocumentUpload.NameParsers

    Public Class FileNameData

        Public Property DealerFileType As String

        Public Property CertificateNumber As String

        Public Property DocumentType As String

        Public Property CustomerName As String

        Public Property CheckNumber As Integer?

        Public Property Amount As Decimal?

        Public Property FileDate As DateTime

        Public Property Status As String

        Public Function GetHeaders(ByVal Optional separator As String = ";") As String
            Dim properties As PropertyInfo() = [GetType]().GetProperties(bindingAttr:=BindingFlags.[Public] Or BindingFlags.Instance)
            Dim header As String = String.Join(separator, properties.[Select](Function(x) x.Name))
            Return header
        End Function

        Public Function GetData(ByVal Optional separator As String = ";") As String
            Dim properties As PropertyInfo() = [GetType]().GetProperties(bindingAttr:=BindingFlags.[Public] Or BindingFlags.Instance)
            Dim body As String = String.Join(separator, properties.[Select](Function(x) x.GetValue(Me)))
            Return body
        End Function
    End Class
End NameSpace
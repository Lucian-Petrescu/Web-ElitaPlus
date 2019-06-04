Imports System.IO
Imports System.Text.RegularExpressions

Namespace Workers.DocumentUpload.NameParsers
    Public Class DartyDocNameParser
        Inherits DocumentNameParser

        Public Sub New()
            CompanyCode = "AIF"
            DealerFileType = "Darty"
            GeneratesOutput = False
            NameFormat = New Regex("^(?<date>(19|20)\d\d(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01]))[_](?<certificate>[a-zA-Z0-9]{18})[_](IMEI_)?(?<docType>\S+)[.]")
            LayoutTuple = New Tuple(Of String, String)("NA", ";")
        End Sub

        Public Sub New(file As FileInfo) 
            Me.New() 
            Document = file
        End Sub
    End Class
End Namespace

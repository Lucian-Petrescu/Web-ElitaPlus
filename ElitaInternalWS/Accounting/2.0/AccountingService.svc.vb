Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

' ReSharper disable once CheckNamespace
Namespace Accounting
    Public Class AccountingServiceV2
        Implements IAccountingServiceV2

        Public Function ResendFile(accountingTransmissionId As String) As Object Implements IAccountingServiceV2.ResendFile
            Try
                Dim fs As New FelitaEngine
                fs.ResendFile(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(accountingTransmissionId)))

                Return Boolean.TrueString

            Catch ex As Exception
                AppConfig.Log(ex)
                Return Boolean.FalseString
            End Try
        End Function
    End Class
End Namespace
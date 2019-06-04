Imports System.ServiceModel.Security
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Namespace Accounting
    ' ReSharper disable once CheckNamespace
    Public Class AccountingServiceV1
        Inherits ElitaWcf
        Implements IAccountingServiceV1

        Public Function ProcessAccounting(networkId As String, password As String, ldapGroup As String, companyId As String, accountingEventId As String, vendorFiles As String) As String Implements IAccountingServiceV1.ProcessAccounting

            Dim strReturn As String
            Try
                Try
                    Dim uunused As String = LoginBody(networkId, password, ldapGroup)
                    AppConfig.Log("Accounting ProcessAccounting:  Login Successful")
                    Dim fs As New FelitaEngine()
                    Dim IsVendorFiles As Boolean

                    If vendorFiles = "1" Then
                        IsVendorFiles = True
                    Else
                        IsVendorFiles = False
                    End If


                    Dim _felitaEngine As FelitaEngine
                    Dim dr As FelitaEngineDs.FelitaEngineRow
                    Dim _FEDs As New FelitaEngineDs

                    If _FEDs.Tables.Count = 0 Then
                        _FEDs.Tables.Add(New FelitaEngineDs.FelitaEngineDataTable)
                    End If

                    dr = CType(_FEDs.Tables(0).NewRow, FelitaEngineDs.FelitaEngineRow)

                    dr.CompanyId = companyId
                    dr.VendorFiles = vendorFiles
                    dr.AccountingEventId = accountingEventId
                    _FEDs.Tables(0).Rows.Add(dr)
                    _felitaEngine = New FelitaEngine(_FEDs)

                    strReturn = _felitaEngine.ProcessWSRequest()

                Catch ex As SecurityAccessDeniedException
                    AppConfig.Log("Accounting ProcessAccounting:  Login Failed")
                    Return "Accounting ProcessAccounting:  Login Failed"
                End Try

                Return strReturn

            Catch ex As Exception
                AppConfig.Log(ex)
                Return ex.Message

            End Try
        End Function
    End Class
End Namespace
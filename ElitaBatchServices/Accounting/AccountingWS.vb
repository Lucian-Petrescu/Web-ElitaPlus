Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Public Class AccountingWS
    Implements IAccountingWS


    Public Function ProcessRequest(networkID As String,
                                   password As String,
                                   LDAPGroup As String,
                                   functionToProcess As String,
                                   xmlStringDataIn As String,
                                   hubRegion As String,
                                    Optional ByVal isAsync As String = "false") As String _
                                    Implements IAccountingWS.ProcessRequest
        Try

            Dim sRet As String
            Dim _BS As New BatchService
            Instrumentation.WriteLog("STARTED : networkID : " & networkID & ", password : " & password & ", LDAPGroup : " & LDAPGroup & ", functionToProcess : " & functionToProcess & ", xmlStringDataIn : " & xmlStringDataIn & ", hubRegion : " & hubRegion & ", isAsync : " & isAsync)
            sRet = _BS.ProcessRequest(networkID, password, LDAPGroup, functionToProcess, xmlStringDataIn, hubRegion, Boolean.Parse(isAsync))
            Instrumentation.WriteLog("FINISHED")
            Return sRet

        Catch ex As Exception
            Instrumentation.WriteLog("--------------------------------------------")
            Instrumentation.WriteLog("FAILED WITH EXCEPTION : " & ex.Message)
            Instrumentation.WriteLog("FAILED WITH STACK TRACE : " & ex.StackTrace)
            Instrumentation.WriteLog("--------------------------------------------")
            Throw
        End Try
    End Function

    Public Function ResendFile(networkID As String, _
                                  password As String, _
                                  LDAPGroup As String, _
                                  hubRegion As String, _
                                  AccountingTransmissionIdString As String) As String _
                                   Implements IAccountingWS.ResendFile

        Dim bs As New BatchService

        Try
            If bs.VerifyLogin(networkID, password, LDAPGroup, hubRegion) Then
                bs.EventLog.WriteEntry("Accounting Resend:  Login Successful")
                Dim fs As New FelitaEngine
                fs.ResendFile(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(AccountingTransmissionIdString)))

            Else
                bs.EventLog.WriteEntry("Accounting Resend:  Login Failed")
            End If

            Return Boolean.TrueString

        Catch ex As Exception
            bs.EventLog.WriteEntry(String.Format("Accounting Resend:  Error:  {0}", ex.StackTrace))
            Return Boolean.FalseString
        End Try


    End Function

End Class

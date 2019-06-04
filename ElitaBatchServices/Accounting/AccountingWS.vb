Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Public Class AccountingWS
    Implements IAccountingWS


    Public Function ProcessRequest(ByVal networkID As String,
                                   ByVal password As String,
                                   ByVal LDAPGroup As String,
                                   ByVal functionToProcess As String,
                                   ByVal xmlStringDataIn As String,
                                   ByVal hubRegion As String,
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

    Public Function ResendFile(ByVal networkID As String, _
                                  ByVal password As String, _
                                  ByVal LDAPGroup As String, _
                                  ByVal hubRegion As String, _
                                  ByVal AccountingTransmissionIdString As String) As String _
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

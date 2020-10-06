Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Public Class TestService
    Implements ITestService

    Function HealthCheck(networkID As String, _
                           password As String, _
                           LDAPGroup As String, _
                           hubRegion As String) As String Implements ITestService.HealthCheck

        Dim bs As New BatchService
        Dim returnStr As String

        Try
            If bs.VerifyLogin(networkID, password, LDAPGroup, hubRegion) Then
                returnStr = "Batch Services Health Check:  Login Successful. "
            Else
                returnStr = "Batch Services Health Check:  Login Failed "
            End If

            bs.EventLog.WriteEntry(returnStr)

            returnStr += String.Format(", Computer Name: {0}, ", My.Computer.Name)

            Return returnStr

        Catch ex As Exception
            returnStr = String.Format("Batch Services Health Check:  Error:  {0}", ex.StackTrace)
            bs.EventLog.WriteEntry(returnStr)
            Return returnStr
        End Try
    End Function

End Class

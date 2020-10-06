Imports Assurant.ElitaPlus.BusinessObjectsNew

Public Class GVSInterfaceHistoryWS
    Implements IGVSInterfaceHistoryWS

    Public Function CheckTransactionHistory(networkID As String, _
                                  password As String, _
                                  LDAPGroup As String, _
                                  hubRegion As String, _
                                  Optional ByVal HoursToCheck As Integer = 0) As String _
                                   Implements IGVSInterfaceHistoryWS.CheckTransactionHistory

        Dim bs As New BatchService

        Try
            If bs.VerifyLogin(networkID, password, LDAPGroup, hubRegion) Then
                bs.EventLog.WriteEntry("GVS Check Transaction:  Login Successful")
                If HoursToCheck > 0 Then
                    TransactionLogHeader.CheckLastSuccessfulTransmissionTimeByType(HoursToCheck)
                Else
                    TransactionLogHeader.CheckLastSuccessfulTransmissionTimeByType()
                End If
            Else
                bs.EventLog.WriteEntry("GVS Check Transaction:  Login Failed")
            End If

            Return Boolean.TrueString

        Catch ex As Exception
            Return Boolean.FalseString
        End Try


    End Function

End Class

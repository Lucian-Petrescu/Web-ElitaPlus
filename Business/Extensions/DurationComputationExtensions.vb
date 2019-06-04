Imports System.Runtime.CompilerServices
Public Module DurationComputationExtensions


    <Extension()>
    Public Function GetFVSTMonths(ByVal pFromDate As Date, ByVal pToDate As Date) As Integer

        If (pFromDate > pToDate) Then
            Throw New InvalidOperationException("From Date greather than To Date")
        End If

        Dim returnValue As Integer = (pToDate - pFromDate).TotalDays / 30

        If ((pToDate - pFromDate.AddMonths(returnValue).Subtract(New TimeSpan(TimeSpan.TicksPerDay))).TotalDays > 14) Then
            returnValue = returnValue + 1
        End If

        Return returnValue

    End Function
End Module

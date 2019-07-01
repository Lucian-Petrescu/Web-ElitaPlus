
#Region "Connection"

Public Structure ceHelperConnection
    Public ceHttpProtocol As String
    Public ceMachineName As String
    Public ceViewerMachineName As String
    Public ceUserID As String
    Public cePW As String
    Public dbServer As String
    Public dbUserID As String
    Public dbPW As String
End Structure

#End Region

#Region "Report Parameters"

' <summary>
' This struct is used to hold parameter information. Multiple
' instances of this struct are placed in an ArrayList and 
' the ArrayList is then transferred.
' </summary>
Public Structure ceHelperParameter
    Public Const EMPTY_CEPARAM As String = ""
    Public Name As String
    Public Value As String
    Public subReportName As String

    Public Sub New(ByVal parmName As String, ByVal parmValue As String,
                          Optional ByVal parmSubReportName As String = EMPTY_CEPARAM)
        Name = parmName
        Value = parmValue
        subReportName = parmSubReportName
    End Sub
End Structure

#End Region

#Region "Schedule"

Public Class ceSchedule
    Public startDateTime As DateTime
    Public ftp As ceFtp
    Public uDisk As ceUDisk
    Public email As ceEMAIL

    Public Sub New(ByVal oStartDateTime As DateTime)
        startDateTime = oStartDateTime
    End Sub
End Class

#Region "Destination"

Public Enum ceDestination
    WBARH
    UDISK
    FTP
    EMAIL
End Enum

Public Class ceFtp
    Public host As String
    Public port As Integer
    Public userName As String
    Public password As String
    Public account As String
    Public directory As String
    Public filename As String
End Class

Public Class ceEMAIL
    Public FromAddress As String
    Public ToAddres As String
    Public CCAddress As String
    Public Subject As String
    Public Message As String
    Public directory As String
    Public FileName As String
End Class

Public Class ceUDisk
    Public userName As String
    Public password As String
    Public directory As String
    Public filename As String
End Class

#End Region

#End Region


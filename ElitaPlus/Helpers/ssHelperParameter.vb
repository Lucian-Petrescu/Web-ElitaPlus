
#Region "Connection"

Public Enum SsReportServerType
    ssServerTypeInvalid = 0
    ssServerTypeODBC = 1
    ssServerTypeOracle = 2
    ssServerTypeDB2 = 3
    ssServerTypeSybase = 4
    ssServerTypeInformix = 5
    ssServerTypeUserSpecified = 1000
End Enum

Public Structure ssHelperConnection
    Public ssHttpProtocol As String
    Public ssMachineName As String
    Public ssViewerMachineName As String
    Public ssUserID As String
    Public ssPW As String
    Public dbServerType As SsReportServerType
    Public dbServer As String
    Public dbUserID As String
    Public dbPW As String
    Public domain As String
    Public ReportPath As String
    Public ReportServerUrl As String
    Public rootDir As String
End Structure

#End Region

#Region "Report Parameters"

' <summary>
' This struct is used to hold parameter information. Multiple
' instances of this struct are placed in an ArrayList and 
' the ArrayList is then transferred.
' </summary>
Public Structure ssHelperParameter
    Public Const EMPTY_SSPARAM As String = ""
    Public Name As String
    Public Value As String
    Public subReportName As String

    Public Sub New(parmName As String, parmValue As String,
                          Optional ByVal parmSubReportName As String = EMPTY_SSPARAM)
        Name = parmName
        Value = parmValue
        subReportName = parmSubReportName
    End Sub
End Structure

#End Region

#Region "Schedule"

Public Class ssSchedule
    Public startDateTime As DateTime
    Public ftp As ssFtp
    Public uDisk As ssUDisk
    Public email As ssEMAIL

    Public Sub New(oStartDateTime As DateTime)
        startDateTime = oStartDateTime
    End Sub
End Class

#Region "Destination"

Public Enum ssDestination
    WBARH
    UDISK
    FTP
    EMAIL
End Enum

Public Class ssFtp
    Public host As String
    Public port As Integer
    Public userName As String
    Public password As String
    Public account As String
    Public directory As String
    Public filename As String
End Class

Public Class ssEMAIL
    Public FromAddress As String
    Public ToAddres As String
    Public CCAddress As String
    Public Subject As String
    Public Message As String
    Public directory As String
    Public FileName As String
End Class

Public Class ssUDisk
    Public userName As String
    Public password As String
    Public directory As String
    Public filename As String
End Class

#End Region

#End Region


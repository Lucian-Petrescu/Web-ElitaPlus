Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms

<Serializable()>
Public NotInheritable Class SSRSReportServerCredentials
    Implements IReportServerCredentials

    Private _userName As String
    Private _password As String
    Private _domain As String
    Public Sub New(ByVal conn As ssHelperConnection)
        _userName = conn.dbUserID
        _password = conn.dbPW
        _domain = conn.domain
    End Sub
    Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
            Implements IReportServerCredentials.ImpersonationUser
        Get

            'Use the default windows user.  Credentials will be
            'provided by the NetworkCredentials property.
            Return Nothing

        End Get
    End Property

    Public ReadOnly Property NetworkCredentials() As ICredentials _
            Implements IReportServerCredentials.NetworkCredentials
        Get
            Return New NetworkCredential(_userName, _password, _domain)
        End Get
    End Property

    Public Function GetFormsCredentials(ByRef authCookie As Cookie,
                                        ByRef userName As String,
                                        ByRef password As String,
                                        ByRef authority As String) _
                                        As Boolean _
            Implements IReportServerCredentials.GetFormsCredentials

        authCookie = Nothing
        userName = Nothing
        password = Nothing
        authority = Nothing

        'Not using form credentials
        Return False

    End Function

End Class





Public Class SSHelper

#Region "Format"
    Public Enum SsReportFormat
        ceFormatCrystalReport = 0
        ceFormatExcel = 1
        ceFormatWord = 2
        ceFormatPDF = 3
        ceFormatRTF = 4
        ceFormatTextPlain = 5
        ceFormatTextPaginated = 6
        ceFormatTextTabSeparated = 7
        ceFormatTextCharacterSeparated = 8
        ceFormatExcelDataOnly = 9
        ceFormatTextTabSeparatedText = 10
        ceFormatRTFEditable = 11
        ceFormatUserDefined = 1000
    End Enum

    Public Enum RptViewer
        CRYSTAL
        EXCEL
        JAVA
        HTML_CLIENT
        PDF
        TEXT_CSV
        TEXT_TAB
    End Enum

    Public Enum RptFormat
        CRYSTAL
        EXCEL
        PDF
        TEXT_CSV
        TEXT_TAB
        JAVA
    End Enum

    Public Structure RepFormat
        Public moSsScheduleFormat As SsReportFormat
        Public moViewer As RptViewer
        Public csvSeparator As String
        Public csvDelimiter As String
        Public msFileExt As String
    End Structure


#End Region

#Region "Status"

    Public Enum RptStatus
        SUCCESS
        CANCEL
        PENDING
        SS_CONNECTION_PROBLEM
        SS_PATH_PROBLEM
        SS_PARAMETER_NOT_FOUND
        SS_SCHEDULE_FAILED
        SS_VIEW_PROBLEM
        SS_UNKNOWN_PROBLEM
        SS_MAX_REPORTS
        SS_FTP_PROBLEM
    End Enum

    Public Structure RptError
        Public status As RptStatus
        Public msg As String
    End Structure

#End Region


    'this is the singleton object - a shared instance of myself
    '  Private Shared mySSHelper As SSHelper

#Region " Constants "
    Public Const AUTHENTICATION_ENTERPRISE As String = "Enterprise"
    ' Public Const SAVEAS_MIME As String = "application/octet-stream"
    Public Const SAVEAS_MIME As String = "application/octet"
    Public Const CSV_EXTENSION As String = ".txt"
    Public Const EXCEL_EXTENSION As String = ".xls"
    Public Const CRYSTAL_EXTENSION As String = ".rpt"
    Public Const PDF_EXTENSION As String = ".pdf"
    Public Const ZIP_EXTENSION As String = ".zip"
    Protected Const SUCSsSS_STATUS As String = "SucSsSs"

    ' CMC Format
    Private Const CMC_CRYSTAL As String = "u2fcr:0"
    Private Const CMC_PDF As String = "crxf_pdf:0"
    Private Const CMC_TEXT_CSV As String = "u2fsepv:3"
    Private Const CMC_TEXT_TAB As String = "u2ftext:1"

    Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
    Public Const SP_YEAR_MONTH_FORMAT As String = "yyyyMM"
    Public Const Date_Length As Integer = 8
    Public Const Year_Month_Length As Integer = 6
    Public Const Default_Date As String = "01"

    Public Const SP_DATETIME_12HR_FORMAT As String = "yyyy.MM.dd.hh.mm.ss"
    Public Const SP_DATETIME_24HR_FORMAT As String = "yyyy.MM.dd.HH.mm.ss"

    Public Const CRYSTAL_FTP As String = "CrystalEnterprise.Ftp"

    Public Const Default_Date_value As String = "01/01/1999 00:00:00 AM"

#End Region

#Region "Variables"

    'Private _entSession As EnterpriseSession
    ' Private _infoStore As InfoStore
    'crystal enterprise login info
    Private SSHttpProtocol As String
    Private _SSUserID As String
    Private _SSPW As String
    Private _SSMachineName As String
    Private _SSViewerMachineName As String
    'db login info
    Private _dbUserID As String
    Private _dbPW As String
    Private _dbServer As String
    'Private _dbServerType As SsReportServerType

    Private _domain As String
    Private _rootDir As String
    Private _report_path As String
    Private _ReportServerUri As String
    Private _SSToken As String
    Private _SSCMS As String
    Private moInstanceId As Long
    '  Private moStatus As RptStatus
    Private moError As RptError

#End Region

#Region " Constructors "

    'do not allow to create instanSsS of this class, force them to
    'use the shared GetSSHelper method.  Note however that this method
    'is called internally by the CreateSession method
    Public Sub New(ByVal conn As ssHelperConnection)
        Status = RptStatus.SUCCESS
        With conn
            Dim SSRSReportViewer As ReportViewer
            Try

                SSRSReportViewer = New ReportViewer
                SSRSReportViewer.ServerReport.ReportServerCredentials = New SSRSReportServerCredentials(conn)
                SSRSReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
                SSRSReportViewer.ServerReport.ReportServerUrl = New Uri(conn.ReportServerUrl) '("http://atl0wsrsd020.cead.prd/ReportServer")
                SSRSReportViewer.ServerReport.ReportPath = "/" & conn.rootDir & "/" & conn.ReportPath & "/" '"/ElitaPlus1/Reports/CommentsAdded"

            Catch exc As Exception
                Status = RptStatus.SS_CONNECTION_PROBLEM
                System.Diagnostics.Trace.WriteLine("**** HERE IS THE ERROR : " & exc.Message)
                Throw New Exception("Sorry - you could not be logged on: " & exc.Message)
            Finally
                SSRSReportViewer = Nothing
            End Try


            'initialize 
            SSHttpProtocol = .ssHttpProtocol
            _SSUserID = .ssUserID
            _SSPW = .ssPW
            _SSMachineName = .ssMachineName
            _SSViewerMachineName = .ssViewerMachineName
            _dbUserID = .dbUserID
            _dbPW = .dbPW
            _report_path = .ReportPath
            _ReportServerUri = .ReportServerUrl
            _dbServer = .dbServer
            _domain = conn.domain
            _rootDir = .rootDir
            '_SSToken = _entSession.LogonTokenMgr.DefaultToken
            '_ssToken = _entSession.LogonTokenMgr.CreateWCATokenEx("", 1440, 100)
            '_SSCMS = _entSession.CMSName
        End With
    End Sub


#End Region

#Region " Properties "

    Public ReadOnly Property dbUserID() As String
        Get
            Return _dbUserID
        End Get
    End Property

    Public ReadOnly Property dbPW() As String
        Get
            Return _dbPW
        End Get
    End Property

    'Public ReadOnly Property dbServerType() As SsReportServerType
    '    Get
    '        Return _dbServerType
    '    End Get
    'End Property

    Public ReadOnly Property dbServer() As String
        Get
            Return _dbServer
        End Get
    End Property

    Public ReadOnly Property logonTokenDefault() As String
        Get
            Return _SSToken
        End Get
    End Property

    Public ReadOnly Property cmsName() As String
        Get
            Return _SSCMS
        End Get
    End Property

    Public Property InstanceId() As Long
        Get
            Return moInstanceId
        End Get
        Set(ByVal Value As Long)
            moInstanceId = Value
        End Set
    End Property

    Public Property Status() As RptStatus
        Get
            Return moError.status
        End Get
        Set(ByVal Value As RptStatus)
            moError.status = Value
        End Set
    End Property

    Public Property ErrorMsg() As String
        Get
            Return moError.msg
        End Get
        Set(ByVal Value As String)
            moError.msg = Value.Replace(Microsoft.VisualBasic.Constants.vbLf, String.Empty)
        End Set
    End Property

#End Region
#Region " Public Methods "

    Public Function GetReportServerCredentials() As SSRSReportServerCredentials
        Dim conn As ssHelperConnection = New ssHelperConnection
        conn.dbPW = _SSPW '_dbPW
        conn.dbUserID = _SSUserID ' _dbUserID
        conn.domain = _domain

        Return New SSRSReportServerCredentials(conn)

    End Function

    Public Function GetDataSourceCredentials(ByVal oSSRptViwer As ReportViewer) As DataSourceCredentials

        Dim dsCredential As New DataSourceCredentials
        Dim dataSource As ReportDataSourceInfoCollection
        dataSource = oSSRptViwer.ServerReport.GetDataSources()
        If (Not oSSRptViwer.ServerReport.GetDataSources() Is Nothing AndAlso oSSRptViwer.ServerReport.GetDataSources().Count > 0) Then
            dsCredential.Name = dataSource.Item(0).Name
            dsCredential.UserId = dbUserID
            dsCredential.Password = dbPW
        Else
            dsCredential = Nothing
        End If


        Return dsCredential

    End Function
#End Region

End Class

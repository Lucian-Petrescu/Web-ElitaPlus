Imports System.IO
Imports Assurant.Common.Zip.aZip
Imports System.Collections.Generic
Imports System.Text

Namespace Reports


    Public Class ReportExtractBase
        Inherits ElitaPlusSearchPage


        Public Enum RptStatus
            SUCCESS
            CANCEL
            PENDING
        End Enum

        Public Structure RptError
            Public status As RptStatus
            Public msg As String
        End Structure


#Region "Page Parameters"

        Public Enum RptFormat
            JAVA
            PDF
            TEXT_CSV
            TEXT_TAB
        End Enum

        Public Enum RptAction
            SCHEDULE
            SCHEDULE_VIEW
            VIEW
        End Enum

        Public Class Params
            Public msRptName As String
            Public msRptWindowName As String

            Public Sub New()
            End Sub
        End Class

#End Region

#Region "Viewer"
        Public Enum RptViewerWindow
            IFRAME
            WINDOWOPEN
        End Enum

#End Region

#Region "Constants"

        Public Shared REPORTS_UICODE As String = "Reports"
        Public Const SESSION_PARAMETERS_KEY As String = "REPORTCE_BASE_SESSION_PARAMETERS_KEY"
        Public Const SESSION_PARAMETERS_DOWNLOAD_KEY As String = "DOWNLOAD_BASE_SESSION_PARAMETERS_KEY"
        Private ReadOnly MAX_SCHED_MINUTES As Integer = Convert.ToInt32(AppConfig.Application.CeTimeout)
        Private ReadOnly MAX_SCHED_POLL As Integer = Convert.ToInt32((MAX_SCHED_MINUTES * 60) / 5)
        Private Const NO_INSTANCE As Long = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"

        Public Shared MAX_REPORTS As Integer = 8
        Public Shared MAX_REPORTS_BY_USER As Integer = 3
        Public Shared MAX_SAME_REPORTS_BY_USER As Integer = 2
        Public Shared SESSION_INSTANCE_ID As String = "INSTANCE_ID"

#End Region

#Region "Variables"

        Private moError As ReportExtractBase.RptError

#End Region

#Region " Properties "
        Public Property Status() As ReportExtractBase.RptStatus
            Get
                Return moError.status
            End Get
            Set(ByVal Value As ReportExtractBase.RptStatus)
                moError.status = Value
            End Set
        End Property

        Public Property ErrorMsg() As String
            Get
                Return moError.msg
            End Get
            Set(ByVal Value As String)
                moError.msg = Value
            End Set
        End Property

#End Region

#Region "Page State"

#Region "MyState"


        Class MyState

            Public moParams As Params
            Private sError As ReportExtractBase.RptError

            Public Property Err() As ReportExtractBase.RptError
                Get
                    Return sError
                End Get
                Set(ByVal Value As ReportExtractBase.RptError)
                    sError = Value
                End Set
            End Property

            Public Property ErrStatus() As ReportExtractBase.RptStatus
                Get
                    Return sError.status
                End Get
                Set(ByVal Value As ReportExtractBase.RptStatus)
                    sError.status = Value
                End Set
            End Property

            Public Property ErrMsg() As String
                Get
                    Return sError.msg
                End Get
                Set(ByVal Value As String)
                    sError.msg = Value
                End Set
            End Property

            Sub New()
                sError.status = RptStatus.SUCCESS
            End Sub

        End Class
#End Region

        Public Sub New()
            MyBase.New(True)
        End Sub

        Public Sub New(ByVal useExistingState As Boolean)
            MyBase.New(useExistingState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Dim st As MyState = Nothing
                Dim key As Type = Me.GetType()
                If Me.StateSession.Contains(key) Then
                    st = CType(Me.StateSession.Item(key), MyState)
                End If
                If st Is Nothing Then
                    st = New MyState
                    Me.StateSession.Item(key) = st
                End If
                Return st
            End Get
        End Property

        Protected Sub SetStateProperties()
            SetStateProperties_NoErrStatus()
            Me.State.ErrStatus = RptStatus.SUCCESS
        End Sub

        Protected Sub SetStateProperties_NoErrStatus()
            Me.State.moParams = CType(Session(SESSION_PARAMETERS_KEY), Params)
        End Sub

#End Region




#Region "View Report"

        Private Sub CreateZipFiles(ByVal zipFileName As String, ByVal sourceDirectory As String)
            Try
                Dim sourceDir As String = sourceDirectory.Replace("/", "\")
                Dim destinationFileName As String = sourceDir & "\" & zipFileName
                Dim filter As String = "TXT"
                CreateZipFile(destinationFileName, sourceDir, True, filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub DownloadFile(ByVal fileName As String, ByVal sourceDirectory As String)

            Dim pathFileName As String = sourceDirectory & "\" & fileName

            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = "application/zip"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)
            Response.TransmitFile(pathFileName)
            Response.Flush()
            Response.End()

        End Sub


        Private Sub SendResponse(ByVal mimeString As String, ByVal byteStream() As Byte, ByVal filename As String)
            Dim sAttachFilename As String

            sAttachFilename = "inline; filename=" & MiscUtil.RemoveInvalidChar(filename)
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("content-disposition", sAttachFilename)
            Response.ContentType = mimeString
            Response.BinaryWrite(byteStream)
            Response.Flush()
            Response.End()
        End Sub



        Private Sub CreateFolder(ByVal folderName As String)
            Dim objDir As New DirectoryInfo(folderName)
            Try
                If Not objDir.Exists() Then
                    objDir.Create()
                End If
            Catch ex As Exception
                '  HandleErrors(ex, Me.ErrCollection)
            End Try
        End Sub




#End Region

#Region "Formating & Validation"

        Public Shared Function FormatDate(ByVal lbl As Label, ByVal sDate As String) As String
            Dim sFormated As String
            Dim tempDate As Date

            GUIException.ValidateDate(lbl, sDate)
            tempDate = DateHelper.GetDateValue(sDate)
            sFormated = tempDate.ToString(SP_DATE_FORMAT)

            Return sFormated
        End Function

        Public Shared Sub ValidateBeginEndDate(ByVal beginLbl As Label, ByVal beginDate As String, ByVal endLbl As Label, ByVal endDate As String)
            Dim tempEndDate As Date
            Dim tempBeginDate As Date

            GUIException.ValidateDate(beginLbl, beginDate)
            tempBeginDate = DateHelper.GetDateValue(beginDate)
            GUIException.ValidateDate(endLbl, endDate)
            tempEndDate = DateHelper.GetDateValue(endDate)
            If tempEndDate < tempBeginDate Then
                ElitaPlusPage.SetLabelError(beginLbl)
                ElitaPlusPage.SetLabelError(endLbl)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
            End If
        End Sub

#End Region

#Region "JavaScript"

        Protected Sub SendReportError(ByVal statusMsg As String, ByVal errorMsg As String)
            Dim sJavaScript As String
            Dim transMsg As String = TranslationBase.TranslateLabelOrMessage(statusMsg)

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "parent.SendReportError('" & transMsg & "','" & errorMsg & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("SendReportError", sJavaScript)
        End Sub

        Protected Sub ContinueWaitingForReport()
            Dim sJavaScript As String

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "buttonContinueForReportClick();" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("ContinueWaitingForReport", sJavaScript)
        End Sub
#End Region


    End Class

End Namespace

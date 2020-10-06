Imports System.Text

Namespace Reports
    Partial Public Class CancellationsRequestReportForm
        Inherits ElitaPlusPage

#Region "Handlers-Init"

        Private Sub InitializeForm()
            SetStateProperties()
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            ErrControllerMaster.Clear_Hide()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    'JavascriptCalls()
                    TheReportCeInputControl.SetExportOnly()
                    TheReportCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
                    InitializeForm()

                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)


        End Sub
#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "REJECTED_REQUESTS_REPORT"
        Private Const RPT_FILENAME As String = "RejectedRequestsReport"
        Private Const RPT_FILENAME_EXPORT As String = "RejectedRequestsReport-Exp"

        Public Const CRYSTAL As String = "0"
        'Public Const PDF As String = "1"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"
        'Public Const EXCEL As String = "4"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1
        Private Const Max_Records As String = "1000000"

        Public Const PAGETITLE As String = "REJECTED_REQUESTS_REPORT"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "REJECTED_REQUESTS_REPORT"

        Public Const Default_Date As String = "2999/01/01"
        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/CancellationsRequestReportForm.aspx"
        Public Const EMPTY_GUID As String = "00000000-0000-0000-0000-000000000000"
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
        Dim reportName As String = RPT_FILENAME_EXPORT
        Dim params As ReportCeBaseForm.Params
#End Region

#Region "Page State"

        Private Class PageStatus
            Public Sub New()
            End Sub
        End Class

        Public Class MyState
            Public TRANSACTION_TYPE As String
            Public MOBILE_NUMBER As String
            Public TRANS_DATE_FROM As String
            Public TRANS_DATE_TO As String
            Public ERROR_CODE As String
            Public Page_Index As Integer
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            Dim oExpState As Assurant.ElitaPlus.ElitaPlusWebApp.CancellationRequestExceptionForm.MyState
            'DirectCast(DirectCast(Me.CallingParameters, System.Object), Assurant.ElitaPlus.ElitaPlusWebApp.TransExceptionManagementForm.MyState)
            State.TRANSACTION_TYPE = CType(CallingParameters, CancellationRequestExceptionForm.MyState).searchTransactionType.ToString
            State.MOBILE_NUMBER = CType(CallingParameters, CancellationRequestExceptionForm.MyState).searchMobileNumber.ToString
            State.TRANS_DATE_FROM = CType(CallingParameters, CancellationRequestExceptionForm.MyState).searchFrom.ToString
            State.TRANS_DATE_TO = CType(CallingParameters, CancellationRequestExceptionForm.MyState).searchTo.ToString
            State.ERROR_CODE = CType(CallingParameters, CancellationRequestExceptionForm.MyState).searchErrorCode.ToString
            State.Page_Index = CType(CallingParameters, CancellationRequestExceptionForm.MyState).PageIndex
        End Sub

#End Region

#Region "ParameterData"

        Public Structure ParameterData
            'Public COMPANY_GROUP_ID As Guid
            Public TRANSACTION_TYPE As String
            Public MOBILE_NUMBER As String
            Public TRANS_DATE_FROM As String
            Public TRANS_DATE_TO As String
            'Public ERROR_CODE As String
            'Public NUM_REC As String
            'Public TRANS_LOG_HEADER_ID As String
            Public CULTURE_VALUE As String
            Public LANGUAGE As String
        End Structure

#End Region

#Region "Crystal Enterprise"

        Private Sub btnGenRpt_Click(sender As Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                Dim oparamdate As ParameterData
                ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)
                oparamdate = AssignParamValuesAndRunReport()
                params = SetParameters(oparamdate)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Function AssignParamValuesAndRunReport() As ParameterData
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim ParamData As ParameterData
            Dim odate As Date

            With ParamData
                If Not State.TRANSACTION_TYPE = EMPTY_GUID Then
                    Dim gID As Guid
                    gID = New Guid(State.TRANSACTION_TYPE)
                    .TRANSACTION_TYPE = GuidToSQLString(gID)
                Else
                    .TRANSACTION_TYPE = String.Empty.ToString()
                End If
                .MOBILE_NUMBER = State.MOBILE_NUMBER
                If Not State.TRANS_DATE_FROM = String.Empty Then
                    odate = DateHelper.GetDateValue(State.TRANS_DATE_FROM)
                    .TRANS_DATE_FROM = odate.ToString(SP_DATE_FORMAT)
                Else
                    .TRANS_DATE_FROM = String.Empty
                End If
                If Not State.TRANS_DATE_TO = String.Empty Then
                    odate = DateHelper.GetDateValue(State.TRANS_DATE_TO)
                    .TRANS_DATE_TO = odate.ToString(SP_DATE_FORMAT)
                Else
                    .TRANS_DATE_TO = String.Empty
                End If
                .CULTURE_VALUE = TheReportCeInputControl.getCultureValue(True)
                .LANGUAGE = langCode
            End With
            Return (ParamData)

        End Function

        Function SetParameters(data As ParameterData) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(True)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)


            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim repParams() As ReportCeBaseForm.RptParam
            With data
                'If .TRANS_DATE_FROM = String.Empty Then
                '    .TRANS_DATE_FROM = Default_Date

                'End If
                'If .TRANS_DATE_TO = String.Empty Then
                '    .TRANS_DATE_TO = Default_Date
                'End If
                repParams = New ReportCeBaseForm.RptParam() _
                  { _
                        New ReportCeBaseForm.RptParam("V_TRANSACTION_TYPE", .TRANSACTION_TYPE), _
                        New ReportCeBaseForm.RptParam("V_MOBILE_NUMBER", .MOBILE_NUMBER), _
                        New ReportCeBaseForm.RptParam("V_TRANS_DATE_FROM", .TRANS_DATE_FROM), _
                        New ReportCeBaseForm.RptParam("V_TRANS_DATE_TO", .TRANS_DATE_TO), _
                        New ReportCeBaseForm.RptParam("V_LANGUAGE", .LANGUAGE), _
                        New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .CULTURE_VALUE)}

            End With

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params

        End Function

        Public Shared Function GuidToSQLString(Value As Guid) As String
            Dim byteArray As Byte() = Value.ToByteArray
            Dim i As Integer
            Dim result As New StringBuilder("")
            For i = 0 To byteArray.Length - 1
                Dim hexStr As String = byteArray(i).ToString("X")
                If hexStr.Length < 2 Then
                    hexStr = "0" & hexStr
                End If
                result.Append(hexStr)
            Next
            Return result.ToString
        End Function

#End Region

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            '  Dim otrans As New TransExceptionManagementForm            
            Dim retType As New CancellationRequestExceptionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State)
            ReturnToCallingPage(retType)
        End Sub
    End Class
End Namespace

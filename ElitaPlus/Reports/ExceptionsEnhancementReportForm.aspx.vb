Namespace Reports
    Partial Public Class ExceptionsEnhancementReportForm
        Inherits ElitaPlusPage

#Region "Handlers-Init"

        Private Sub InitializeForm()
            SetStateProperties()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Me.ErrControllerMaster.Clear_Hide()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    'JavascriptCalls()
                    TheReportCeInputControl.SetExportOnly()
                    TheReportCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
                    InitializeForm()

                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)


        End Sub
#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "TRANSACTION_EXCEPTIONS"
        Private Const RPT_FILENAME As String = "TransactionExceptions"
        Private Const RPT_FILENAME_EXPORT As String = "TransactionExceptions-Exp"

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

        Public Const PAGETITLE As String = "TRANSACTION_EXCEPTIONS"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "TRANSACTION_EXCEPTIONS"

        Public Const Default_Date As String = "01/01/2999"
        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/ExceptionsEnhancementReportForm.aspx"

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
            Public CLAIM_NUMBER As String
            Public AUTH_NUMBER As String
            Public SVC_CODE As String
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
            Dim oExpState As Assurant.ElitaPlus.ElitaPlusWebApp.TransExceptionManagementForm.MyState
            'DirectCast(DirectCast(Me.CallingParameters, System.Object), Assurant.ElitaPlus.ElitaPlusWebApp.TransExceptionManagementForm.MyState)
            Me.State.CLAIM_NUMBER = CType(Me.CallingParameters, TransExceptionManagementForm.MyState).searchClaimNumber.ToString
            Me.State.AUTH_NUMBER = CType(Me.CallingParameters, TransExceptionManagementForm.MyState).searchAuthNumber.ToString
            Me.State.SVC_CODE = CType(Me.CallingParameters, TransExceptionManagementForm.MyState).searchServiceCenterCode.ToString
            Me.State.TRANS_DATE_FROM = CType(Me.CallingParameters, TransExceptionManagementForm.MyState).searchFrom.ToString
            Me.State.TRANS_DATE_TO = CType(Me.CallingParameters, TransExceptionManagementForm.MyState).searchTo.ToString
            Me.State.ERROR_CODE = CType(Me.CallingParameters, TransExceptionManagementForm.MyState).searchErrorCode.ToString
            Me.State.Page_Index = CType(Me.CallingParameters, TransExceptionManagementForm.MyState).PageIndex
        End Sub

#End Region
#Region "ParameterData"

        Public Structure ParameterData
            Public COMPANY_GROUP_ID As Guid
            Public CLAIM_NUMBER As String
            Public AUTH_NUMBER As String
            Public SVC_CODE As String
            Public TRANS_DATE_FROM As String
            Public TRANS_DATE_TO As String
            Public ERROR_CODE As String
            Public NUM_REC As String
            Public TRANS_LOG_HEADER_ID As String
            Public CULTURE_VALUE As String
        End Structure

#End Region

#Region "Crystal Enterprise"
        Private Sub btnGenRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                Dim oparamdate As ParameterData
                ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)
                oparamdate = AssignParamValuesAndRunReport()
                params = SetParameters(oparamdate)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
        Public Function AssignParamValuesAndRunReport() As ParameterData

            Dim ParamData As ParameterData
            With ParamData
                .COMPANY_GROUP_ID = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                .CLAIM_NUMBER = Me.State.CLAIM_NUMBER
                .AUTH_NUMBER = Me.State.AUTH_NUMBER
                .SVC_CODE = Me.State.SVC_CODE
                .TRANS_DATE_FROM = Me.State.TRANS_DATE_FROM
                .TRANS_DATE_TO = Me.State.TRANS_DATE_TO
                .ERROR_CODE = Me.State.ERROR_CODE
                .NUM_REC = Max_Records
                .TRANS_LOG_HEADER_ID = String.Empty
                .CULTURE_VALUE = TheReportCeInputControl.getCultureValue(True)
            End With
            Return (ParamData)

        End Function


        Function SetParameters(ByVal data As ParameterData) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(True)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim repParams() As ReportCeBaseForm.RptParam
            With data
                If .TRANS_DATE_FROM = String.Empty Then
                    .TRANS_DATE_FROM = Default_Date

                End If
                If .TRANS_DATE_TO = String.Empty Then
                    .TRANS_DATE_TO = Default_Date
                End If
                repParams = New ReportCeBaseForm.RptParam() _
                   { _
                         New ReportCeBaseForm.RptParam("P_COMPANY_GROUP_ID", GuidControl.GuidToHexString(.COMPANY_GROUP_ID)), _
                         New ReportCeBaseForm.RptParam("P_CLAIM_NUMBER", .CLAIM_NUMBER), _
                         New ReportCeBaseForm.RptParam("P_AUTH_NUMBER", .AUTH_NUMBER), _
                         New ReportCeBaseForm.RptParam("P_SVC_CODE", .SVC_CODE), _
                         New ReportCeBaseForm.RptParam("P_TRANS_DATE_FROM", .TRANS_DATE_FROM), _
                         New ReportCeBaseForm.RptParam("P_TRANS_DATE_TO", .TRANS_DATE_TO), _
                         New ReportCeBaseForm.RptParam("P_ERROR_CODE", .ERROR_CODE), _
                         New ReportCeBaseForm.RptParam("P_NUM_REC", .NUM_REC), _
                         New ReportCeBaseForm.RptParam("P_TRANS_LOG_HEADER_ID", .TRANS_LOG_HEADER_ID), _
                         New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .CULTURE_VALUE)}

            End With
            ' Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params

        End Function

#End Region

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            '  Dim otrans As New TransExceptionManagementForm            
            Dim retType As New TransExceptionManagementForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State)
            Me.ReturnToCallingPage(retType)
        End Sub
    End Class
End Namespace

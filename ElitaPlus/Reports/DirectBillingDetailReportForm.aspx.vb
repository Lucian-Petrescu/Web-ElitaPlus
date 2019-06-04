Namespace Reports
    Partial Public Class DirectBillingDetailReportForm
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
                    'Page.RegisterStartupScript("ResizeForm", "<script language='javascript'>resizeForm();</script>")
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

        Private Const RPT_FILENAME_WINDOW As String = "Direct_Billing_Detail"
        Private Const RPT_FILENAME As String = "DirectBillingDetail"
        Private Const RPT_FILENAME_EXPORT As String = "DirectBillingDetail-Exp"

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

        Public Const PAGETITLE As String = "Direct_Billing_Detail"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Direct_Billing_Detail"

        Public Const Default_Date As String = "01/01/2999"
        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/DirectBillingDetailReportForm.aspx"

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
            Public BillingHeaderId As Guid
            Public ByDealer As String
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
            Dim oExpState As Assurant.ElitaPlus.ElitaPlusWebApp.DirectBillingDetailForm.MyState
            Me.State.BillingHeaderId = CType(Me.CallingParameters, DirectBillingDetailForm.MyState).BillingHeaderId
            If (CType(Me.CallingParameters, DirectBillingDetailForm.MyState).BillingFileByDealer = True) Then
                Me.State.ByDealer = "Y"
            Else
                Me.State.ByDealer = "N"
            End If
            Me.State.Page_Index = CType(Me.CallingParameters, DirectBillingDetailForm.MyState).PageIndex
        End Sub

#End Region
#Region "ParameterData"

        Public Structure ParameterData
            Public BILLING_HEADER_ID As Guid
            Public LANGUAGE_ID As Guid
            Public BYDEALER As String
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
                .BILLING_HEADER_ID = Me.State.BillingHeaderId
                .LANGUAGE_ID = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                .BYDEALER = Me.State.ByDealer
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
                repParams = New ReportCeBaseForm.RptParam() _
                   {
                         New ReportCeBaseForm.RptParam("P_BILLING_HEADER_ID", GuidControl.GuidToHexString(.BILLING_HEADER_ID)),
                         New ReportCeBaseForm.RptParam("P_LANGUAGE_ID", GuidControl.GuidToHexString(.LANGUAGE_ID)),
                         New ReportCeBaseForm.RptParam("P_BYDEALER", .BYDEALER),
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
            Dim retType As New DirectBillingDetailForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State)
            Me.ReturnToCallingPage(retType)
        End Sub
    End Class
End Namespace

Namespace Reports
    Partial Public Class RepairLogisticsClaimsExportForm
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
                    'Page.RegisterStartupScript("ResizeForm", "<script language='javascript'>resizeForm();</script>")
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

        Private Const RPT_FILENAME_WINDOW As String = "Repair_Logistics_Claims"
        Private Const RPT_FILENAME As String = "RepairLogisticsClaims"
        Private Const RPT_FILENAME_EXPORT As String = "RepairLogisticsClaims-Exp"

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

        Public Const PAGETITLE As String = "Repair_Logistics_Claims"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Repair_Logistics_Claims"

        Public Const Default_Date As String = "01/01/2999"
        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/RepairLogisticsClaimsExportForm.aspx"

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
            Public claimNumber As String
            Public serialNumber As String
            Public certificate As String
            Public serviceCenterid As Guid
            Public Countryid As Guid
            Public manufacturerid As Guid
            Public Model As String
            Public claimstatus As String
            Public extclaimstatusid As Guid
            Public skuclaimed As String
            Public skureplaced As String
            Public coveragetypeid As Guid
            Public servicelevelid As Guid
            Public risktypeid As Guid
            Public skureppart As String
            Public replacementtypeid As Guid
            Public claimcreateddate As SearchCriteriaStructType(Of Date)
            Public datesearchtype As String
            'Public enddate As String
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
            Dim oExpState As Assurant.ElitaPlus.ElitaPlusWebApp.PendingReviewPaymentClaimListForm.MyState
            State.claimNumber = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).claimNumber
            State.serialNumber = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).serialNumber
            State.serviceCenterid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).servicecenterid
            State.Countryid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).Countryid
            State.coveragetypeid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).coveragetypeid
            State.claimstatus = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).claimstatus
            State.extclaimstatusid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).extclaimstatusid
            State.manufacturerid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).Manufacturerid
            State.Model = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).Model
            State.claimcreateddate = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).claimcreateddate
            'Me.State.enddate = CType(Me.CallingParameters, Claims.PendingReviewPaymentClaimListForm.MyState).enddate
            State.certificate = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).certificate
            State.skuclaimed = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).skuclaimed
            State.skureplaced = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).skureplaced
            State.servicelevelid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).servicelevelid
            State.risktypeid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).risktypeid
            State.skureppart = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).skureppart
            State.replacementtypeid = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).replacementtypeid
            State.datesearchtype = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).datesearchtype
            State.Page_Index = CType(CallingParameters, PendingReviewPaymentClaimListForm.MyState).PageIndex
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public claimNumber As String
            Public serialNumber As String
            Public certificate As String
            Public serviceCenterid As Guid
            Public Countryid As Guid
            Public manufacturerid As Guid
            Public Model As String
            Public claimstatus As String
            Public extclaimstatusid As Guid
            Public skuclaimed As String
            Public skureplaced As String
            Public coveragetypeid As Guid
            Public claimcreateddate As SearchCriteriaStructType(Of Date)
            Public datesearchtype As String
            Public servicelevelid As Guid
            Public risktypeid As Guid
            Public skureppart As String
            Public replacementtypeid As Guid

            Public page_index As Integer
            Public HasDataChanged As Boolean = False
            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, returnPar As Object)
                LastOperation = LastOp
                claimNumber = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).claimNumber
                serialNumber = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).serialNumber
                certificate = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).certificate
                serviceCenterid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).serviceCenterid
                Countryid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).Countryid
                manufacturerid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).manufacturerid
                Model = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).Model
                claimstatus = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).claimstatus
                extclaimstatusid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).extclaimstatusid
                skuclaimed = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).skuclaimed
                skureplaced = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).skureplaced
                coveragetypeid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).coveragetypeid
                claimcreateddate = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).claimcreateddate
                datesearchtype = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).datesearchtype
                servicelevelid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).servicelevelid
                risktypeid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).risktypeid
                skureppart = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).skureppart
                replacementtypeid = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).replacementtypeid
                page_index = CType(returnPar, Reports.RepairLogisticsClaimsExportForm.MyState).Page_Index

            End Sub
        End Class

#End Region
#Region "ParameterData"

        Public Structure ParameterData
            Public COMPANY_GROUP_ID As Guid
            Public CLAIM_NUMBER As String
            Public SERIAL_NUMBER As String
            Public CERTIFICATE As String
            Public SERVICE_CENTER_ID As Guid
            Public COUNTRY_ID As Guid
            Public MANUFACTURER_ID As Guid
            Public MODEL As String
            Public CLAIM_STATUS As String
            Public EXTENDED_CLAIM_STATUS_ID As Guid
            Public SKU_CLAIMED As String
            Public SKU_REPLACED As String
            Public COVERAGE_TYPE_ID As Guid
            Public BEGIN_DATE As String
            Public END_DATE As String
            Public DATE_SEARCH_TYPE As String
            Public SERVICE_LEVEL_ID As Guid
            Public RISK_TYPE_ID As Guid
            Public SKU_REPLACEMENT_PART As String
            Public REPLACEMENT_TYPE_ID As Guid

            Public LANGUAGE_ID As Guid
            Public CULTURE_VALUE As String
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

            Dim ParamData As ParameterData
            With ParamData
                .COMPANY_GROUP_ID = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                .CLAIM_NUMBER = State.claimNumber
                .SERIAL_NUMBER = State.serialNumber
                .SERVICE_CENTER_ID = State.serviceCenterid
                .COUNTRY_ID = State.Countryid
                .CERTIFICATE = State.certificate
                .MANUFACTURER_ID = State.manufacturerid
                .MODEL = State.Model
                .SKU_CLAIMED = State.skuclaimed
                .SKU_REPLACED = State.skureplaced
                .CLAIM_STATUS = State.claimstatus
                .EXTENDED_CLAIM_STATUS_ID = State.extclaimstatusid
                .COVERAGE_TYPE_ID = State.coveragetypeid
                If State.claimcreateddate.FromValue.HasValue Then
                    .BEGIN_DATE = CDate(State.claimcreateddate.FromValue).ToString("MM/dd/yyyy")
                End If

                If State.claimcreateddate.ToValue.HasValue Then
                    .END_DATE = CDate(State.claimcreateddate.ToValue).ToString("MM/dd/yyyy")
                End If

                .DATE_SEARCH_TYPE = State.datesearchtype.ToString()
                .SERVICE_LEVEL_ID = State.servicelevelid
                .RISK_TYPE_ID = State.risktypeid
                .SKU_REPLACEMENT_PART = State.skureppart
                .REPLACEMENT_TYPE_ID = State.replacementtypeid
                .LANGUAGE_ID = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                .CULTURE_VALUE = TheReportCeInputControl.getCultureValue(True)
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
                repParams = New ReportCeBaseForm.RptParam() _
                   {
                         New ReportCeBaseForm.RptParam("P_COMPANY_GROUP_ID", GuidControl.GuidToHexString(.COMPANY_GROUP_ID)),
                         New ReportCeBaseForm.RptParam("P_CLAIM_NUMBER", .CLAIM_NUMBER),
                          New ReportCeBaseForm.RptParam("P_SERIAL_NUMBER", .SERIAL_NUMBER),
                          New ReportCeBaseForm.RptParam("P_COUNTRY_ID", If(.COUNTRY_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.COUNTRY_ID))),
                          New ReportCeBaseForm.RptParam("P_CLAIM_STATUS", .CLAIM_STATUS),
                          New ReportCeBaseForm.RptParam("P_EXT_CLAIM_STATUS_ID", If(.EXTENDED_CLAIM_STATUS_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.EXTENDED_CLAIM_STATUS_ID))),
                          New ReportCeBaseForm.RptParam("P_MANUFACTURER_ID", If(.MANUFACTURER_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.MANUFACTURER_ID))),
                          New ReportCeBaseForm.RptParam("P_MODEL", .MODEL),
                          New ReportCeBaseForm.RptParam("P_SKU_CLAIMED", .SKU_CLAIMED),
                          New ReportCeBaseForm.RptParam("P_SKU_REPLACED", .SKU_REPLACED),
                          New ReportCeBaseForm.RptParam("P_CERTIFICATE", .CERTIFICATE),
                          New ReportCeBaseForm.RptParam("P_COVERAGE_TYPE_ID", If(.COVERAGE_TYPE_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.COVERAGE_TYPE_ID))),
                          New ReportCeBaseForm.RptParam("P_SERVICE_CENTER_ID", If(.SERVICE_CENTER_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.SERVICE_CENTER_ID))),
                          New ReportCeBaseForm.RptParam("P_SERVICE_LEVEL_ID", If(.SERVICE_LEVEL_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.SERVICE_LEVEL_ID))),
                          New ReportCeBaseForm.RptParam("P_RISK_TYPE_ID", If(.RISK_TYPE_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.RISK_TYPE_ID))),
                          New ReportCeBaseForm.RptParam("P_SKU_REPLACEMENT_PART", .SKU_REPLACEMENT_PART),
                          New ReportCeBaseForm.RptParam("P_REPLACEMENT_TYPE_ID", If(.REPLACEMENT_TYPE_ID = Guid.Empty, Nothing, GuidControl.GuidToHexString(.REPLACEMENT_TYPE_ID))),
                          New ReportCeBaseForm.RptParam("P_BEGIN_DATE", .BEGIN_DATE),
                          New ReportCeBaseForm.RptParam("P_END_DATE", .END_DATE),
                          New ReportCeBaseForm.RptParam("P_DATE_SEARCH_TYPE", .DATE_SEARCH_TYPE),
                          New ReportCeBaseForm.RptParam("P_LANGUAGE_ID", GuidControl.GuidToHexString(.LANGUAGE_ID))}
                ' New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .CULTURE_VALUE)}

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

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

            Dim retType As New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State)
            ReturnToCallingPage(retType)
        End Sub
    End Class
End Namespace

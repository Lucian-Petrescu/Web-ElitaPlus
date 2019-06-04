Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Public Class BBYClaimInvoicingReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "BBY_CLAIM_INVOICING"
        Private Const RPT_FILENAME As String = "BBYClaimInvoicing"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const RPT_FILENAME_EXPORT As String = "BBYClaimInvoicing-Exp"
        Private Const TOTALPARAMS As Integer = 23  ' 23
        Private Const TOTALEXPPARAMS As Integer = 12 ' 7
        Private Const PARAMS_PER_REPORT As Integer = 12 '8
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const ASM_DEALER As String = "EA02"
        Private Const MDS_DEALER As String = "PA01"

#End Region

#Region "parameters"
        Public Structure ReportParams
            Public companycode As String
            Public companyDesc As String
            Public dealerCode As String
            Public begindate As String
            Public enddate As String
            Public SVCenterCode As String
            Public SVCenterDesc As String
            Public notificationCode As String
            Public notificationDesc As String
            Public isdetail As String
            Public langcode As String
            Public culturevalue As String
        End Structure

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Dim osvcenter As ServiceCenter
        Dim dealerId As Guid
        Dim dealerCode As String

#End Region

#Region "Properties"

        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
            End Get
        End Property

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private designerPlaceholderDeclaration As System.Object
        Private currentAccountingMonth As Integer
        Private currentAccountingYear As Integer


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateServiceCenters()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ' Me.ClearLabelErrSign(MonthYearLabel)
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
        End Sub

#End Region

#Region "Populate"


        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub

        Sub PopulateServiceCenters()
            'Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID            
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                If UserCompanyMultipleDrop.SelectedCode = "ASM" Then
                    dealerCode = ASM_DEALER
                    dealerId = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALERS, ASM_DEALER)
                    'Me.BindListControlToDataView(Me.cbosvcenter, LookupListNew.GetServiceCenterByDealerLookupList(dealerId))
                    PopulateServiceCenter(dealerId)
                ElseIf UserCompanyMultipleDrop.SelectedCode = "MDS" Then
                    dealerCode = MDS_DEALER
                    dealerId = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALERS, MDS_DEALER)
                    'Me.BindListControlToDataView(Me.cbosvcenter, LookupListNew.GetServiceCenterByDealerLookupList(dealerId))
                    PopulateServiceCenter(dealerId)
                Else
                    dealerCode = String.Empty
                    dealerId = Guid.Empty
                    'Me.BindListControlToDataView(Me.cbosvcenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
                    PopulateServiceCenterByCountry()
                End If
            Else
                If Page.IsPostBack Then
                    ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                End If
            End If

        End Sub

        Private Sub PopulateServiceCenterByCountry()
            Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)

            For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                If ServiceCenters.Count > 0 Then
                    If Not ServiceCenterList Is Nothing Then
                        ServiceCenterList.AddRange(ServiceCenters)
                    Else
                        ServiceCenterList = ServiceCenters.Clone()
                    End If
                End If
            Next

            Me.cbosvcenter.Populate(ServiceCenterList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        End Sub

        Private Sub PopulateServiceCenter(dealerId As Guid)
            Dim ServiceCentes As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByDealer",
                                                                            context:=New ListContext() With
                                                                            {
                                                                              .DealerId = dealerId
                                                                            })

            Me.cbosvcenter.Populate(ServiceCentes.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        End Sub

        Sub PopulateServiceNotifications()
            'Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            'Me.BindListControlToDataView(Me.cbosvnotification, LookupListNew.GetNotificationTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            Dim Notifications As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="SNTYP",
                                                                            languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            Me.cbosvnotification.Populate(Notifications.ToArray(),
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True
                                            })
        End Sub
        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateServiceCenters()
            PopulateServiceNotifications()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rsvcenter.Checked = True
            Me.rsvnotification.Checked = True
            Me.RadiobuttonTotalsOnly.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal companyCode As String, ByVal companyDesc As String, ByVal dealerCode As String, ByVal begindate As String, ByVal enddate As String,
                               ByVal SVCenterCode As String, ByVal SVCenterDesc As String, ByVal notificationCode As String,
                               ByVal notificationDesc As String, ByVal isdetail As String, ByVal langcode As String) As ReportCeBaseForm.Params

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .companycode = companyCode
                .companyDesc = companyDesc
                .dealerCode = dealerCode
                .begindate = begindate
                .enddate = enddate
                .SVCenterCode = SVCenterCode
                .SVCenterDesc = SVCenterDesc
                .notificationCode = notificationCode
                .notificationDesc = notificationDesc
                .isdetail = isdetail
                .langcode = langcode
                .culturevalue = culturevalue
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            isdetail = NO
            SetReportParams(rptParams, repParams, "Total", PARAMS_PER_REPORT * 1)     ' Main Report

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params

        End Function

        Function SetExpParameters(ByVal companyCode As String, ByVal companyDesc As String, ByVal dealerCode As String, ByVal begindate As String, ByVal enddate As String,
                               ByVal SVCenterCode As String, ByVal SVCenterDesc As String, ByVal notificationCode As String,
                               ByVal notificationDesc As String, ByVal isdetail As String, ByVal langcode As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .companycode = companyCode
                .companyDesc = companyDesc
                .dealerCode = dealerCode
                .begindate = begindate
                .enddate = enddate
                .SVCenterCode = SVCenterCode
                .SVCenterDesc = SVCenterDesc
                .notificationCode = notificationCode
                .notificationDesc = notificationDesc
                .isdetail = isdetail
                .langcode = langcode
                .culturevalue = culturevalue
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))
            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                          ByVal reportName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companycode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMPANY_DESC", .companyDesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_SVCENTER", .SVCenterCode, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_SVCENTER_DESC", .SVCenterDesc, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_NOTIFICATION", .notificationCode, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("V_NOTIFICATION_DESC", .notificationDesc, reportName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("V_ISDETAIL", .isdetail, reportName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", .langcode, reportName)
                repParams(startIndex + 11) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub
        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langcode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_LANGUAGES, languageid)
            Dim SVCenterId As Guid = Me.GetSelectedItem(Me.cbosvcenter)
            Dim NotificationId As Guid = Me.GetSelectedItem(Me.cbosvnotification)
            Dim CompanyId As String = GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid)
            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc

            Dim endDate As String
            Dim beginDate As String
            Dim sortorder As String
            Dim svcenterCode As String
            Dim svcenterDesc As String
            Dim notificationCode As String
            Dim notificationDesc As String
            Dim isdetail As String
            Dim dealerCode As String

            Dim params As ReportCeBaseForm.Params

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'Validating the selection
            If Me.rsvcenter.Checked Then
                svcenterCode = ALL
                svcenterDesc = ALL
            ElseIf Not SVCenterId.Equals(Guid.Empty) Then
                osvcenter = New ServiceCenter(SVCenterId)
                svcenterCode = osvcenter.Code
                svcenterDesc = osvcenter.Description
            Else
                ElitaPlusPage.SetLabelError(cbosvcenterlbl)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
            End If

            If Me.rsvnotification.Checked Then
                notificationCode = ALL
                notificationDesc = ALL
            ElseIf Not NotificationId.Equals(Guid.Empty) Then
                notificationCode = LookupListNew.GetCodeFromId(LookupListCache.LK_NOTIICATION_TYPES, NotificationId)
                notificationDesc = LookupListNew.GetDescriptionFromId(LookupListCache.LK_NOTIICATION_TYPES, NotificationId)
            Else
                ElitaPlusPage.SetLabelError(cbosvnotificationbl)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_NOTIFICATION_TYPE_MUST_BE_SELECTED_ERR)
            End If

            If Me.RadiobuttonDetail.Checked Then
                isdetail = YES
            Else
                isdetail = NO
            End If

            'Dealer

            If UserCompanyMultipleDrop.SelectedCode = "ASM" Then
                dealerCode = ASM_DEALER
            ElseIf UserCompanyMultipleDrop.SelectedCode = "MDS" Then
                dealerCode = MDS_DEALER
            Else
                dealerCode = String.Empty
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)
            moReportFormat = ReportCeBase.GetReportFormat(Me)

            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                isdetail = YES
                params = SetExpParameters(CompanyCode, CompanyDesc, dealerCode, beginDate, endDate, svcenterCode, svcenterDesc, notificationCode, notificationDesc, isdetail, langcode)
            Else
                'View Report
                params = SetParameters(CompanyCode, CompanyDesc, dealerCode, beginDate, endDate, svcenterCode, svcenterDesc, notificationCode, notificationDesc, isdetail, langcode)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class
End Namespace
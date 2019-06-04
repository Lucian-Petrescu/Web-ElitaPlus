Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Public Class ServiceActivityReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "SERVICE_ACTIVITY"
        Private Const RPT_FILENAME As String = "ServiceActivity"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const RPT_FILENAME_EXPORT As String = "ServiceActivity-Exp"
        Private Const TOTALPARAMS As Integer = 7  ' 23
        Private Const TOTALEXPPARAMS As Integer = 7  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 7 '8
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const MANUFACTURER As String = "Manufacturer"
        Private Const SVC As String = "Svc"
        Private Const SVCANDSTORE As String = "SvcAndStore"
        Private Const STORE As String = "Store"

#End Region

#Region "parameters"
        Public Structure ReportParams
            Public companycode As String
            Public begindate As String
            Public enddate As String
            Public selectiontype As String
            Public selectioncode As String
            Public selectiondesc As String
            Public langcode As String
            Public culturevalue As String
        End Structure
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
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
            UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub

        Sub PopulateManufacturerDropDown()
            'Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            'Me.BindListControlToDataView(Me.cboManufacturer, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim manufactureLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.cboManufacturer.Populate(manufactureLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
                })
        End Sub
        Sub PopulateServiceCenters()
            'Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Me.BindListControlToDataView(Me.cboSVC, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
            Dim oSVCList = GetServiceCenter()

            Me.cboSVC.Populate(oSVCList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
        End Sub

        Private Function GetServiceCenter() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCountries As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

            Dim SVCLkl As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCountries.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CountryId = UserCountries(Index)
                Dim oSvcList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oSvcList.Count > 0 Then
                    If Not SVCLkl Is Nothing Then
                        SVCLkl.AddRange(oSvcList)
                    Else
                        SVCLkl = oSvcList.Clone()
                    End If

                End If
            Next

            Return SVCLkl.ToArray()

        End Function
        Sub PopulateStores()
            'Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            'Me.BindListControlToDataView(Me.cboStore, LookupListNew.GetReplacementStoresLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
            Dim oStoreList = GetStore()

            Me.cboStore.Populate(oStoreList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
        End Sub
        Private Function GetStore() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCountries As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

            Dim StoreLkl As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCountries.Count - 1
                oListContext.CountryId = UserCountries(Index)
                Dim oSvcList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="StoresWithMethodOfRepairs", context:=oListContext)
                If oSvcList.Count > 0 Then
                    If Not StoreLkl Is Nothing Then
                        StoreLkl.AddRange(oSvcList)
                    Else
                        StoreLkl = oSvcList.Clone()
                    End If

                End If
            Next

            Return StoreLkl.ToArray()

        End Function
        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateManufacturerDropDown()
            PopulateServiceCenters()
            PopulateStores()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rAllSVCandStore.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            TheRptCeInputControl.ExcludeExport()
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal CompanyCode As String, ByVal begindate As String, ByVal enddate As String,
                               ByVal selectiontype As String, ByVal selectioncode As String, ByVal selectiondesc As String, ByVal langcode As String) As ReportCeBaseForm.Params

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .companycode = CompanyCode
                .begindate = begindate
                .enddate = enddate
                .selectiontype = selectiontype
                .selectioncode = selectioncode
                .selectiondesc = selectiondesc
                .langcode = langcode
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

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

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                          ByVal reportName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companycode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_SELECTION_TYPE", .selectiontype, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_SELECTION_CODE", .selectioncode, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_SELECTION_DESC", .selectiondesc, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", .langcode, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub
        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langcode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_LANGUAGES, languageid)
            Dim endDate As String
            Dim beginDate As String
            Dim sortorder As String
            Dim selectiontype As String
            Dim selectioncode As String
            Dim selectiondesc As String
            Dim ManufacturerId As Guid = Me.GetSelectedItem(Me.cboManufacturer)
            Dim SVCId As Guid = Me.GetSelectedItem(Me.cboSVC)
            Dim StoreId As Guid = Me.GetSelectedItem(Me.cboStore)

            Dim CompanyId As String = GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid)
            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim osvcenter As ServiceCenter

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
            If Me.rmanufacturer.Checked Then
                selectiontype = MANUFACTURER
                selectioncode = ALL
                selectiondesc = ALL
            ElseIf Not ManufacturerId.Equals(Guid.Empty) Then
                selectiontype = MANUFACTURER
                selectioncode = Me.GetSelectedDescription(Me.cboManufacturer)
                selectiondesc = Me.GetSelectedDescription(Me.cboManufacturer)
            ElseIf Me.rAllSVCandStore.Checked Then
                selectiontype = SVCANDSTORE
                selectioncode = ALL
                selectiondesc = ALL
            ElseIf Me.rAllSVC.Checked Then
                selectiontype = SVC
                selectioncode = ALL
                selectiondesc = ALL
            ElseIf Me.rAllStore.Checked Then
                selectiontype = STORE
                selectioncode = ALL
                selectiondesc = ALL
            ElseIf Not SVCId.Equals(Guid.Empty) Then
                selectiontype = SVC
                osvcenter = New ServiceCenter(SVCId)
                selectioncode = osvcenter.Code
                selectiondesc = Me.GetSelectedDescription(Me.cboSVC)
            ElseIf Not StoreId.Equals(Guid.Empty) Then
                selectiontype = STORE
                osvcenter = New ServiceCenter(StoreId)
                selectioncode = osvcenter.Code
                selectiondesc = Me.GetSelectedDescription(Me.cboStore)
                'ElseIf ManufacturerId.Equals(Guid.Empty) And BranchId.Equals(Guid.Empty) Then
                '    ElitaPlusPage.SetLabelError(cboManufacturerlbl)
                '   Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MANUFACTURER_MUST_BE_SELECTED_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            'View Report
            params = SetParameters(CompanyCode, beginDate, endDate, selectiontype, selectioncode, selectiondesc, langcode)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region
    End Class
End Namespace
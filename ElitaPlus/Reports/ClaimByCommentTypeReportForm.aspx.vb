Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class ClaimByCommentTypeReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public dealerId As Guid
            Public serviceCenterId As Guid
            Public commentTypeId As Guid
            Public daysFromLastComment As String
            Public includeAllClaims As String
            Public userId As Guid
            Public languageId As Guid
            Public languageCultureValue As String
            Public createdBy As String
        End Structure

#End Region

#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "SEARCH_BY_COMMENT_TYPE"

        Private Const RPT_FILENAME As String = "ClaimByCommentType"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimByCommentType-Exp"

        Public Const CRYSTAL As String = "0"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"

        Public Const BY_NUMBER_ACTIVE_DAYS As String = "0"
        Public Const BY_CLAIM_NUMBER As String = "1"
        Public Const BY_SERVICE_CENTER_NAME As String = "2"
        Public Const BY_DEALER As String = "3"

        Public Const SORT_BY_NUMBER_ACTIVE_DAYS As String = "A"
        Public Const SORT_BY_CLAIM_NUMBER As String = "C"
        Public Const SORT_BY_SERVICE_CENTER_NAME As String = "S"
        Public Const SORT_BY_DEALER As String = "D"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Public Const ReportName_LangSep As String = "_"
        Public Const numeric_cultureSep As String = "¦"

        Private Const TOTALPARAMS As Integer = 9
        Private Const TOTALEXPPARAMS As Integer = 9
        Private Const PARAMS_PER_REPORT As Integer = 9

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Public Const DEFAULT_NUMBER_ACTIVE_DAYS As String = "3"
        Public Const None As Integer = 0

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Dim rptLangSelected As String
        Dim reportName As String
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

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

#End Region

#Region "Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents RadiobuttonEXCEL As System.Web.UI.WebControls.RadioButton
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    moReportCeInputControl.populateReportLanguages(RPT_FILENAME)
                End If
                EnableOrDisableControls()
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Private Sub InitializeForm()
            PopulateCommentTypeDropDown()
            PopulateDealerDropDown()
            PopulateServiceCenterDropDown()
            Me.rCommentType.Checked = True
            Me.rsvccenter.Checked = True
            Me.rdealer.Checked = True
            Me.rAllUsers.Checked = True
            Me.RadiobuttonAllClaims.Checked = True
            Me.PopulateControlFromBOProperty(Me.txtActiveDays, Me.DEFAULT_NUMBER_ACTIVE_DAYS)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False,
                                              DealerMultipleDrop.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER),
                                              True,
                                              False,
                                              " document.forms[0].rdealer.checked = false;",
                                              "moDealerMultipleDrop_moMultipleColumnDrop",
                                              "moDealerMultipleDrop_moMultipleColumnDropDesc", "moDealerMultipleDrop_lb_DropDown")
        End Sub

        Sub PopulateServiceCenterDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim oServiceCenterList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Each _country As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                oListContext.CountryId = _country
                Dim oServiceCenterListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oServiceCenterListForCountry.Count > 0 Then
                    If Not oServiceCenterList Is Nothing Then
                        oServiceCenterList.AddRange(oServiceCenterListForCountry)
                    Else
                        oServiceCenterList = oServiceCenterListForCountry.Clone()
                    End If
                End If
            Next

            Me.cboSvcCenter.Populate(oServiceCenterList.ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
            'Me.BindListControlToDataView(Me.cboSvcCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
        End Sub

        Sub PopulateCommentTypeDropDown()
            Dim commentTypes As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="COMMT", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.cboCommentType.Populate(commentTypes, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.cboCommentType, LookupListNew.GetCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
                Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                ''Dim langCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, languageId)
                Dim langCultureValue As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, languageId)
                Dim dealerId As Guid = DealerMultipleDrop.SelectedGuid
                Dim serviceCenterId As Guid = Me.GetSelectedItem(Me.cboSvcCenter)
                Dim daysFromLastComment As String
                Dim daysFromLastCommentNumber As Integer
                Dim commentTypeId As Guid = Me.GetSelectedItem(Me.cboCommentType)
                Dim includeAllClaims As String = NO
                Dim params As ReportCeBaseForm.Params
                Dim createdby As String

                If Me.rCommentType.Checked Then
                    commentTypeId = Guid.Empty
                Else
                    If commentTypeId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMMENT_TYPE_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If Me.rdealer.Checked Then
                    dealerId = Guid.Empty
                Else
                    If dealerId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If Me.rsvccenter.Checked Then
                    serviceCenterId = Guid.Empty
                Else
                    If serviceCenterId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If txtActiveDays.Text.Trim.ToString = String.Empty Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_ACTIVE_DAYS_ERR)
                Else
                    daysFromLastComment = Me.txtActiveDays.Text.Trim()
                    daysFromLastCommentNumber = CType(Me.txtActiveDays.Text, Integer)
                    If ((daysFromLastCommentNumber < 0) OrElse (daysFromLastCommentNumber > 999)) Then
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_ACTIVE_DAYS_ERR)
                    End If
                End If

                If Me.RadiobuttonExcludeRepairedClaims.Checked Then
                    includeAllClaims = YES
                End If


                If Me.rAllUsers.Checked Then
                    createdby = Nothing
                Else
                    createdby = txtUserId.Text.Trim.ToString()
                    If createdby = String.Empty Then
                        'ElitaPlusPage.SetLabelError(lblUserId)
                        Throw New GUIException(Message.MSG_INVALID_USER_ID, Assurant.ElitaPlus.Common.ErrorCodes.GUI_USER_MUST_BE_ENTERED_ERR)
                    End If
                End If

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                moReportFormat = ReportCeBase.GetReportFormat(Me)
                If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                    'Export Report                    
                    langCultureValue = moReportCeInputControl.getCultureValue(True)
                    params = SetExpParameters(dealerId, serviceCenterId, commentTypeId, daysFromLastComment,
                                              includeAllClaims, createdby, languageId, langCultureValue,
                                              ElitaPlusIdentity.Current.ActiveUser.Id)
                Else
                    'View Report
                    languageId = moReportCeInputControl.LanguageSelected
                    langCultureValue = moReportCeInputControl.getCultureValue(False)
                    params = SetParameters(dealerId, serviceCenterId, commentTypeId, daysFromLastComment,
                                              includeAllClaims, createdby, languageId, langCultureValue,
                                              ElitaPlusIdentity.Current.ActiveUser.Id)
                End If
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Crystal Enterprise"

        Sub SetReportParams(ByVal oReportParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                            ByVal reportName As String, ByVal startIndex As Integer)
            With oReportParams
                If (.dealerId.Equals(Guid.Empty)) Then
                    repParams(startIndex) = New ReportCeBaseForm.RptParam("V_DEALER_ID", Nothing, reportName)
                Else
                    repParams(startIndex) = New ReportCeBaseForm.RptParam("V_DEALER_ID", GuidControl.GuidToHexString(.dealerId), reportName)
                End If

                If (.serviceCenterId.Equals(Guid.Empty)) Then
                    repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_SERVICE_CENTER_ID", Nothing, reportName)
                Else
                    repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_SERVICE_CENTER_ID", GuidControl.GuidToHexString(.serviceCenterId), reportName)
                End If

                If (.commentTypeId.Equals(Guid.Empty)) Then
                    repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_COMMENT_TYPE_ID", Nothing, reportName)
                Else
                    repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_COMMENT_TYPE_ID", GuidControl.GuidToHexString(.commentTypeId), reportName)
                End If

                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DAYS_FROM_LAST_COMMENT", .daysFromLastComment, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_INCLUDE_ALL_CLAIMS", .includeAllClaims, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_CREATED_BY", .createdBy, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_LANGUAGE_ID", GuidControl.GuidToHexString(.languageId), reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_USER_ID", GuidControl.GuidToHexString(.userId), reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .languageCultureValue, reportName)
            End With
        End Sub

        Function SetParameters(ByVal dealerId As Guid, ByVal serviceCenterId As Guid, ByVal commentTypeId As Guid,
                                ByVal daysFromLastComment As String, ByVal includeAllClaims As String,
                                ByVal createdBy As String, ByVal languageId As Guid, ByVal languageCultureValue As String,
                                ByVal userId As Guid) _
                            As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            reportName = moReportCeInputControl.getReportName(RPT_FILENAME, False)
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams
            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))


            With oReportParams
                .dealerId = dealerId
                .serviceCenterId = serviceCenterId
                .commentTypeId = commentTypeId
                .daysFromLastComment = daysFromLastComment
                .includeAllClaims = includeAllClaims
                .userId = userId
                .languageId = languageId
                .languageCultureValue = languageCultureValue
                .createdBy = createdBy
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            'SetReportParams(oReportParams, repParams, "Days", PARAMS_PER_REPORT * 1)           ' Days SubReport
            'SetReportParams(oReportParams, repParams, "Days - 01", PARAMS_PER_REPORT * 2)      ' Days - 01 SubReport

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With


            Return params
        End Function

        Function SetExpParameters(ByVal dealerId As Guid, ByVal serviceCenterId As Guid, ByVal commentTypeId As Guid,
                                ByVal daysFromLastComment As String, ByVal includeAllClaims As String,
                                ByVal createdBy As String, ByVal languageId As Guid, ByVal languageCultureValue As String,
                                ByVal userId As Guid) _
                            As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            ' rptLangSelected = moReportCeInputControl.LanguageCodeSelected
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
            reportName = moReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))
            Dim oReportParams As ReportParams

            With oReportParams
                .dealerId = dealerId
                .serviceCenterId = serviceCenterId
                .commentTypeId = commentTypeId
                .daysFromLastComment = daysFromLastComment
                .includeAllClaims = includeAllClaims
                .userId = userId
                .languageId = languageId
                .languageCultureValue = languageCultureValue
                .createdBy = createdBy
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

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

        Private Sub EnableOrDisableControls()
            If Me.rdealer.Checked = True Then
                DealerMultipleDrop.SelectedIndex = -1
            End If
        End Sub

    End Class
End Namespace
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class ReportExtractInputControl
        Inherits System.Web.UI.UserControl

#Region "Contants"

        Private ReadOnly SCHED_START_TIME As Double = 23.5 ' 23:30 11:30 PM      
        Private ReadOnly SCHED_DELAYED_START_TIME As Double = 0.01 ' 12:01 12:01 AM      
        Private Company_China As String = "ACN"
        Private DEST_EMAIL As String = "Email"

#End Region
#Region "Properties"

        Public ReadOnly Property RadioButtonTXTControl() As RadioButton
            Get
                Return RadiobuttonTXT
            End Get
        End Property
        Public ReadOnly Property DropDownControlVisible() As DropDownList
            Get
                Return moDestDrop
            End Get
        End Property

        Public ReadOnly Property RadioButtonPDFControl() As RadioButton
            Get
                Return RadiobuttonPDF
            End Get
        End Property
        Public ReadOnly Property RadioButtonVIEWControl() As RadioButton
            Get
                Return RadiobuttonView
            End Get
        End Property
        Public ReadOnly Property ProgressControlVisible() As System.Web.UI.HtmlControls.HtmlInputHidden
            Get
                Return isProgressVisible
            End Get
        End Property
        Public ReadOnly Property ReportControlStatus() As System.Web.UI.HtmlControls.HtmlInputHidden
            Get
                Return moReportCeStatus
            End Get
        End Property
        Public ReadOnly Property ReportControlViewer() As System.Web.UI.HtmlControls.HtmlInputHidden
            Get
                Return moReportCeViewer
            End Get
        End Property
        Public ReadOnly Property ReportControlErrMsg() As System.Web.UI.HtmlControls.HtmlInputHidden
            Get
                Return moReportCeErrorMsg
            End Get
        End Property
        Public ReadOnly Property ReportControlAction() As System.Web.UI.HtmlControls.HtmlInputHidden
            Get
                Return moReportCeAction
            End Get
        End Property
        Public ReadOnly Property ReportControlFtp() As System.Web.UI.HtmlControls.HtmlInputHidden
            Get
                Return moReportCeFtp
            End Get
        End Property
        Public ReadOnly Property ReportControlViewHidden() As Button
            Get
                Return btnViewHidden
            End Get
        End Property
        Public ReadOnly Property ReportControlErrorHidden() As Button
            Get
                Return btnErrorHidden
            End Get
        End Property
        Public ReadOnly Property ReportCloseTimer() As System.Web.UI.HtmlControls.HtmlInputHidden
            Get
                Return moReportCeCloseTimer
            End Get
        End Property

        Public Property IsBarProgressVisible() As String
            Get
                Return isProgressVisible.Value
            End Get
            Set(ByVal Value As String)
                isProgressVisible.Value = Value
            End Set
        End Property

        Public Property IsTheReportCeVisible() As String
            Get
                Return isReportCeVisible.Value
            End Get
            Set(ByVal Value As String)
                isReportCeVisible.Value = Value
            End Set
        End Property

        Public Property IsFormatVisible() As Boolean
            Get
                Return moFormatPanel.Visible
            End Get
            Set(ByVal Value As Boolean)
                moFormatPanel.Visible = Value
            End Set
        End Property

        Private Property ViewEnable() As Boolean
            Get
                Return RadiobuttonView.Enabled
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonView.Enabled = Value
            End Set
        End Property

        Private Property PdfEnable() As Boolean
            Get
                Return RadiobuttonView.Enabled
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonPDF.Enabled = Value
            End Set
        End Property

        Private Property ExportDataEnable() As Boolean
            Get
                Return RadiobuttonTXT.Enabled
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonTXT.Enabled = Value
            End Set
        End Property

        Public Property ViewVisible() As Boolean
            Get
                Return RadiobuttonView.Visible
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonView.Visible = Value
                imgView.Visible = Value
            End Set
        End Property

        Public Property SchedulerVisible() As Boolean
            Get
                Return RSched.Visible
            End Get
            Set(ByVal Value As Boolean)
                RSched.Visible = Value
            End Set
        End Property

        Public Property PdfVisible() As Boolean
            Get
                Return RadiobuttonPDF.Visible
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonPDF.Visible = Value
                imgPdf.Visible = Value
            End Set
        End Property

        Public Property ExportDataVisible() As Boolean
            Get
                Return RadiobuttonTXT.Visible
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonTXT.Visible = Value
                imgTxt.Visible = Value
            End Set
        End Property
        Public Property DestinationVisible() As Boolean
            Get
                Return moDestDrop.Visible
            End Get
            Set(ByVal Value As Boolean)
                moDestDrop.Visible = Value
                moDestLabel.Visible = Value
            End Set
        End Property
        Private Property ViewChecked() As Boolean
            Get
                Return RadiobuttonView.Checked
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonView.Checked = Value
            End Set
        End Property

        Private Property PdfChecked() As Boolean
            Get
                Return RadiobuttonPDF.Checked
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonPDF.Checked = Value
            End Set
        End Property

        Private Property ExportDataChecked() As Boolean
            Get
                Return RadiobuttonTXT.Checked
            End Get
            Set(ByVal Value As Boolean)
                RadiobuttonTXT.Checked = Value
            End Set
        End Property

        Public Property DestinationCodeSelected() As String
            Get
                If Not moDestDrop.SelectedIndex = -1 Then
                    Return LookupListNew.GetCodeFromId(LookupListNew.GetReportCeDestLookupList(Authentication.LangId, False), New Guid(moDestDrop.SelectedItem.Value))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
                Me.moDestDrop.ClearSelection()
                Dim dv As DataView = LookupListNew.GetReportCeDestLookupList(Authentication.LangId, False)
                Me.moDestDrop.Items.FindByValue(LookupListNew.GetIdFromCode(dv, Value.ToString).ToString).Selected = True
            End Set
        End Property

        Public ReadOnly Property LanguageSelected() As Guid
            Get
                Return New Guid(DPLang.SelectedItem.Value)
            End Get
        End Property

        Public ReadOnly Property LanguageCodeSelected() As String
            Get
                If Not DPLang.SelectedIndex = -1 Then
                    Return LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, New Guid(DPLang.SelectedItem.Value))
                Else
                    Return String.Empty
                End If
            End Get
        End Property
        Public ReadOnly Property LanguageDescSelected() As String
            Get
                If Not DPLang.SelectedIndex = -1 Then
                    Return LookupListNew.GetDescriptionFromId(LookupListNew.LK_LANGUAGES, New Guid(DPLang.SelectedItem.Value))
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property LanguageCultureSelected() As String
            Get
                If Not DPLang.SelectedIndex = -1 Then
                    Return LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, New Guid(DPLang.SelectedItem.Value))
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public Property ModifyFileNameChecked() As Boolean
            Get
                Return chkUpdateFileName.Checked
            End Get
            Set(ByVal Value As Boolean)
                chkUpdateFileName.Checked = Value
            End Set
        End Property
        Public ReadOnly Property CheckBoxModifyFileControl() As CheckBox
            Get
                Return chkUpdateFileName
            End Get
        End Property

        Public Property SetModifiedFileName() As String
            Get
                Return moModifiedFileName.Value
            End Get
            Set(ByVal Value As String)
                moModifiedFileName.Value = Value
            End Set
        End Property

#End Region

#Region "MyState"

        Public Class MyState
            Public ActionInProgress As ElitaPlusPage.DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public ReportName As String
            Public Instanceid As Long
            Public ReportFormat As String

            Public Sub New()

            End Sub
        End Class

#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            RestoreAttributes()
            If Not IsPostBack Then
                Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)
                oPage.AddCalendar(Me.BtnSchedDate, Me.moSchedDateText, "", "N", "Y")
                JSchedDate()
                PopulateForm()
            Else
                resizeform()
                CheckIfComingFromSaveConfirm()
            End If
        End Sub

#End Region

#Region "Handlers-Button"

        Private Sub btnViewHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewHidden.Click
            'Dim oCEHelper As CEHelper
            'Dim rptCeBase As New ReportCeBase
            'Try
            '    oCEHelper = rptCeBase.GetCEHelper()
            'Catch ex As Exception
            '    AppConfig.DebugLog(ex.Message)
            '    ' Connection Problem               
            '    DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_CONNECTION_PROBLEM"))
            '    Exit Sub
            'End Try
            Try
                'ObtainSchedParams()
                If moSchedCheck.Checked = True Then
                    ' Schedule
                    Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)
                    oPage.SetProgressTimeOutScript()
                    'Else
                    '    VerifyInstances(rptCeBase)
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub btnErrorHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrorHidden.Click
            Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)
            Dim oErrorController As ErrorController
            Dim oMsgController As MessageController
            oErrorController = CType(Me.BindingContainer.FindControl("ErrorCtrl"), ErrorController)
            If Not oErrorController Is Nothing Then
                oErrorController.AddError(moReportCeStatus.Value, False)
                oErrorController.AddError(moReportCeErrorMsg.Value, False)
                oErrorController.Show()
            Else
                If Not oPage.ErrControllerMaster Is Nothing Then
                    oErrorController = oPage.ErrControllerMaster
                    oErrorController.AddError(moReportCeStatus.Value, False)
                    oErrorController.AddError(moReportCeErrorMsg.Value, False)
                    oErrorController.Show()
                Else
                    oMsgController = oPage.MasterErrController
                    oMsgController.AddError(moReportCeStatus.Value, False)
                    oMsgController.AddError(moReportCeErrorMsg.Value, False)
                    oMsgController.Show()
                End If
            End If

            AppConfig.Log(New Exception(moReportCeStatus.Value & " " & moReportCeErrorMsg.Value))
            moReportCeStatus.Value = String.Empty
            moReportCeCloseTimer.Value = "True"
            '   IsBarProgressVisible = "False"
        End Sub

        Private Sub DisplayErrorMsg(ByVal errormsg As String)
            Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)
            Dim oErrorController As ErrorController
            Dim oMsgController As MessageController
            oErrorController = CType(Me.BindingContainer.FindControl("ErrorCtrl"), ErrorController)
            If Not oErrorController Is Nothing Then
                oErrorController.AddError(moReportCeStatus.Value, False)
                oErrorController.AddError(moReportCeErrorMsg.Value, False)
                oErrorController.AddError(errormsg, False)
                oErrorController.Show()
            Else
                If Not oPage.ErrControllerMaster Is Nothing Then
                    oErrorController = oPage.ErrControllerMaster
                    oErrorController.AddError(moReportCeStatus.Value, False)
                    oErrorController.AddError(moReportCeErrorMsg.Value, False)
                    oErrorController.AddError(errormsg, False)
                    oErrorController.Show()
                Else
                    oMsgController = oPage.MasterErrController
                    oMsgController.AddError(moReportCeStatus.Value, False)
                    oMsgController.AddError(moReportCeErrorMsg.Value, False)
                    oMsgController.AddError(errormsg, False)
                    oMsgController.Show()
                End If
            End If

            AppConfig.Log(New Exception(moReportCeStatus.Value & " " & moReportCeErrorMsg.Value))
            moReportCeCloseTimer.Value = "True"
        End Sub

#End Region

#End Region

#Region "Designer"

        Public Sub SetExportOnly()
            ViewChecked = False
            PdfChecked = False
            ExportDataChecked = True
            ViewEnable = False
            ViewVisible = False
            PdfEnable = False
            PdfVisible = False
            ExportDataEnable = True
        End Sub
        Public Sub SetPdfOnly()
            ViewChecked = False
            PdfChecked = True
            ExportDataChecked = False
            ViewEnable = False
            PdfEnable = True
            ExportDataEnable = False
        End Sub

        Public Sub ExcludeExport()
            ViewChecked = True
            PdfChecked = False
            ExportDataChecked = False
            ViewEnable = True
            PdfEnable = True
            ExportDataEnable = False
        End Sub
        Public Sub LangDPVisible(ByVal isvisible As Boolean)
            RLang.Visible = isvisible
            lblLang.Visible = isvisible
            DPLang.Visible = isvisible
        End Sub
        Public Sub UpdateFileNameControlVisible(ByVal isvisible As Boolean)
            lblUpdateFileName.Visible = isvisible
            chkUpdateFileName.Visible = isvisible
        End Sub

#End Region

#Region "Constants"
        Dim strEmpty As String = ""
        Public Const ReportName_LangSep As String = "_"
        Public Const ExpRptName_Sep As String = "-"
        Public Const numeric_cultureSep As String = "¦"
        Public strExportRptExt As String = "_Exp"
        Public strExp As String = "Exp"
        Public SessionInstanceParams As String = "SessionInstanceParams"

#End Region

#Region "Variables"
        Private moState As MyState
        Public Event SelectedViewPDFOption(ByVal sender As Object, ByVal e As System.EventArgs)
        Public Event SelectedDestOptionChanged(ByVal sender As Object, ByVal e As System.EventArgs)

#End Region

#Region "Enable/Diable"

        Private Sub RestoreAttributes()
            If moSchedCheck.Checked Then
                TdSchedDate1.Attributes.Add("style", "display:''")
                TdSchedDate2.Attributes.Add("style", "display:''")
            Else
                TdSchedDate1.Attributes.Add("style", "display:none")
                TdSchedDate2.Attributes.Add("style", "display:none")
            End If
        End Sub
#End Region
#Region "Language"

        'Public Sub populateReportLanguages(ByVal oReportName As String)
        '    'Dim oCEHelper As CEHelper
        '    'Dim drResult As String = oReportName + strExportRptExt
        '    'Dim rowView As DataRowView

        '    'If oReportName = String.Empty Then
        '    '    RLang.Visible = False
        '    '    'rptname = stringemp
        '    'Else
        '    '    'rptname = oReportName
        '    '    Try
        '    '        oCEHelper = ReportCeBase.GetCEHelper()
        '    '    Catch ex As Exception
        '    '        ' Connection Problem
        '    '        Throw New GUIException(Message.MSG_CONN_PROBLEM, oCEHelper.RptStatus.CE_CONNECTION_PROBLEM.ToString)
        '    '    End Try
        '    '    Dim oDataView As DataView
        '    '    Try
        '    '        oDataView = oCEHelper.GetReports(AppConfig.CE.RootDir)
        '    '        oDataView.RowFilter = CEHelper.ReportDataview.COL_REP_NAME & " like '" & oReportName & "_%'"
        '    '        If oDataView.Count > 0 Then
        '    '            For i As Integer = 0 To oDataView.Count - 1
        '    '                If (oDataView.Item(i)(CEHelper.ReportDataview.COL_REP_NAME).ToString = drResult.ToString) Then
        '    '                    oDataView.Delete(i)
        '    '                End If
        '    '            Next
        '    '        End If
        '    '        If oDataView.Count = 0 Then
        '    '            ' Report Not Found
        '    '            Throw New GUIException(Message.MSG_NO_REPORTS_FOUND, oCEHelper.RptStatus.CE_PATH_PROBLEM.ToString)
        '    '        End If
        '    '    Catch ex As Exception
        '    '        ' Report Not Found
        '    '        Throw New GUIException(Message.MSG_NO_REPORTS_FOUND, oCEHelper.RptStatus.CE_PATH_PROBLEM.ToString)
        '    '    End Try

        '    '    Me.BindListTextToDataView(DPLang, oDataView, CEHelper.ReportDataview.COL_REP_NAME,
        '    '    CEHelper.ReportDataview.COL_REP_ID)

        '    'End If
        'End Sub

        Protected Sub BindListTextToDataView(ByVal lstControl As ListControl, ByVal Data As DataView,
    Optional ByVal TextColumnName As String = "DESCRIPTION", Optional ByVal ValueColumnName As String = "ID", Optional ByVal AddNothingSelected As Boolean = True)
            Dim i As Integer
            Dim descColumnValue As String
            Dim idCoumnValue As Guid
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            lstControl.Items.Clear()

            For i = 0 To Data.Count - 1
                descColumnValue = Data(i)(TextColumnName).ToString.Remove(0, Data(i)(TextColumnName).ToString.IndexOf(ReportName_LangSep) + 1)

                If Not LookupListNew.GetDescriptionFromCode(LookupListNew.LK_LANGUAGES, descColumnValue) Is Nothing Then

                    ' If (descColumnValue <> strExp) Then
                    descColumnValue = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_LANGUAGES, descColumnValue)
                    idCoumnValue = LookupListNew.GetIdFromDescription(LookupListNew.LK_LANGUAGES, descColumnValue)
                    lstControl.Items.Add(New System.Web.UI.WebControls.ListItem(descColumnValue.ToString, idCoumnValue.ToString))
                End If
            Next

            If (lstControl.Items.Count > 0) Then
                For i = 0 To lstControl.Items.Count - 1
                    If lstControl.Items(i).Value = languageId.ToString Then
                        lstControl.Items(i).Selected = True
                    Else
                        lstControl.Items(i).Selected = False
                    End If
                Next
                If lstControl.Items.Count > 1 Then
                    LangDPVisible(True)
                End If
            Else
                LangDPVisible(False)
            End If
        End Sub

        Public Function getReportName(ByVal orptname As String, ByVal isExport As Boolean) As String
            If isExport = False Then
                Return (orptname + ReportName_LangSep + LanguageCodeSelected)
            Else
                Return (orptname + ReportName_LangSep + LanguageCodeSelected)
            End If

        End Function
        Public Function getReportWindowTitle(ByVal orptwindowtitle As String) As String
            ' Return (TranslationBase.TranslateLabelOrMessage(orptwindowtitle) + ExpRptName_Sep + LanguageDescSelected)
            Return (orptwindowtitle + ExpRptName_Sep + LanguageDescSelected)
        End Function

        Public Function getCultureValue(ByVal isExport As Boolean, Optional ByVal company_id As String = "") As String
            Dim culturecode As String
            Dim dvCurrency As DataView

            If isExport = True Then
                If company_id = String.Empty Then
                    culturecode = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    dvCurrency = LookupListNew.GetCurrencyNotationLookupList(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
                Else
                    culturecode = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    dvCurrency = LookupListNew.GetCurrencyNotationLookupList(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(company_id)))
                End If

            Else
                Dim langid As Guid = LookupListNew.GetIdFromCode("LANGUAGES", LanguageCodeSelected)
                culturecode = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, langid)
                If company_id = String.Empty Then
                    dvCurrency = LookupListNew.GetCurrencyNotationLookupList(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
                Else
                    dvCurrency = LookupListNew.GetCurrencyNotationLookupList(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(company_id)))
                End If

            End If

            Dim CurrencyNotation As String

            If Not dvCurrency Is Nothing AndAlso dvCurrency.Count > 0 Then
                If Not dvCurrency.Item(0)(LookupListNew.COL_CURRENCY_NOTATION) Is System.DBNull.Value Then
                    CurrencyNotation = dvCurrency.Item(0)(LookupListNew.COL_CURRENCY_NOTATION).ToString
                Else
                    CurrencyNotation = String.Empty
                End If
            Else
                CurrencyNotation = String.Empty
            End If

            If isExport = False Then
                Return (MiscUtil.GetDecimalSeperator(culturecode) + numeric_cultureSep +
                                   MiscUtil.GetGroupSeperator(culturecode) +
                                   numeric_cultureSep + CurrencyNotation + numeric_cultureSep +
                                   MiscUtil.GetShortDateFormat(culturecode) + numeric_cultureSep + MiscUtil.GetDateSeperator(culturecode))
                ' MiscUtil.GetCurrencySymbol(LanguageCodeSelected))
            Else
                Return (MiscUtil.GetDecimalSeperator(culturecode) + numeric_cultureSep +
                                  MiscUtil.GetGroupSeperator(culturecode) +
                                  numeric_cultureSep + CurrencyNotation + numeric_cultureSep +
                                   MiscUtil.GetShortDateFormat(culturecode) + numeric_cultureSep + MiscUtil.GetDateSeperator(culturecode))
            End If

            '   CurrencyNotation = System.Globalization.CultureInfo.CreateSpecificCulture(culturecode).DateTimeFormat.ShortDatePattern

        End Function

#End Region

#Region "Schedule"

        Private Sub PopulateForm()
            ' Destination DropDown
            'Dim langId As Guid = Authentication.LangId
            Dim repDestId As Guid
            'Dim repDestLkl As DataView
            'repDestLkl = LookupListNew.GetReportCeDestLookupList(langId, False)
            Dim repDesLKl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("RCEDEST", Thread.CurrentPrincipal.GetLanguageCode())
            Dim filteredList As DataElements.ListItem() = repDesLKl

            Dim oComp As Company
            oComp = New Company(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
            If Not oComp.Code.Equals(Company_China) Then
                'repDestLkl.RowFilter = "code <> '" & Codes.REPORTCE_DEST__EMAIL & "' and language_id = '" & GuidControl.GuidToHexString(langId) & "'"
                filteredList = (From x In repDesLKl
                                Where Not x.Code = Codes.REPORTCE_DEST__EMAIL
                                Select x).ToArray()

            End If

            'dvRepairCode.RowFilter = " code = '" + Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE + "'"
            'ElitaPlusPage.BindListControlToDataView(moDestDrop, repDestLkl, , , False)
            moDestDrop.Populate(filteredList, New PopulateOptions())

            'repDestId = LookupListNew.GetIdFromCode(filteredList, Codes.REPORTCE_DEST__WEB)
            repDestId = (From lst In filteredList
                         Where lst.Code = Codes.REPORTCE_DEST__WEB
                         Select lst.ListItemId).FirstOrDefault()
            ElitaPlusPage.SetSelectedItem(moDestDrop, repDestId)

            ' Schedule Date
            Dim t As Date = Date.Now
            Me.moSchedDateText.Text = ElitaPlusPage.GetDateFormattedString(t)

        End Sub

        'Private Sub ObtainDestParam(ByVal oParams As ReportCeBase.Params)
        '    Dim langId As Guid = Authentication.LangId
        '    Dim repDestLkl As DataView = LookupListNew.GetReportCeDestLookupList(langId, False)
        '    Dim repDestId As Guid = ElitaPlusPage.GetSelectedItem(moDestDrop)
        '    Dim repDestCode As String = LookupListNew.GetCodeFromId(repDestLkl, repDestId)
        '    oParams.moDest = CType([Enum].Parse(GetType(ceDestination), repDestCode), ceDestination)
        'End Sub

        'Private Sub ObtainSchedDate(ByVal oParams As ReportCeBase.Params)
        '    Dim oStartDateTime As DateTime = Nothing
        '    Dim sStartDate As String

        '    If moSchedCheck.Checked = True Then
        '        ' Schedule
        '        oParams.moAction = ReportCeBase.RptAction.SCHEDULE
        '        GUIException.ValidateDate(moSchedDateLabel, moSchedDateText.Text)
        '        oStartDateTime = DateHelper.GetDateValue(moSchedDateText.Text)
        '        oStartDateTime = oStartDateTime.AddHours(SCHED_START_TIME)
        '    End If

        '    oParams.moSched = New ceSchedule(oStartDateTime)
        'End Sub


        Public Function GetSchedDate() As DateTime
            Dim oStartDateTime As DateTime = Nothing


            If moSchedCheck.Checked = True Then
                GUIException.ValidateDate(moSchedDateLabel, moSchedDateText.Text)
                If DateHelper.GetDateValue(moSchedDateText.Text) = CDate(DateTime.Now.ToString("yyyy-MM-dd")) Then
                    'And DateTime.Now.TimeOfDay.ToString() < SCHED_START_TIME.ToString() Then
                    oStartDateTime = DateHelper.GetDateValue(moSchedDateText.Text)
                    oStartDateTime = oStartDateTime.AddHours(SCHED_START_TIME)
                Else

                    oStartDateTime = DateHelper.GetDateValue(moSchedDateText.Text)
                    oStartDateTime = oStartDateTime.AddHours(SCHED_DELAYED_START_TIME)

                End If

            Else
                oStartDateTime = DateHelper.GetDateValue(DateTime.Now.ToString())

            End If

            Return oStartDateTime
        End Function




#End Region

#Region "Report Instances"


        Protected Sub CheckIfComingFromSaveConfirm()
            Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)
            Dim confResponse As String = Me.HiddenRptPromptResponse.Value
            Me.HiddenRptPromptResponse.Value = Nothing

            Try

                Me.moState = New MyState()
                If Not Session(SessionInstanceParams) Is Nothing Then
                    Me.moState = CType(Session(SessionInstanceParams), MyState)
                End If

                If Not Me.moState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_ Then
                    If Not confResponse Is Nothing AndAlso confResponse = oPage.MSG_VALUE_YES Then
                        oPage.SetProgressTimeOutScript()
                        'ViewReport(Me.moState.Instanceid, Me.moState.ReportFormat)
                    ElseIf Not confResponse Is Nothing AndAlso confResponse = oPage.MSG_VALUE_NO Then
                        oPage.SetProgressTimeOutScript()
                    End If
                    Session(SessionInstanceParams) = Nothing
                End If

            Catch ex As Exception
                'Throw New GUIException(Message.MSG_CONN_PROBLEM, oCEHelper.RptStatus.CE_UNKNOWN_PROBLEM.ToString)
                DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_UNKNOWN_PROBLEM"))
                Exit Sub
            End Try

        End Sub


#End Region

#Region "JavaScript"

        Protected Sub resizeform()
            Dim sJavaScript As String
            Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            'sJavaScript &= "resizeForm();" & Environment.NewLine
            sJavaScript &= "try{resizeForm();}catch(e){}" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            oPage.RegisterStartupScript("resizeForm", sJavaScript)
        End Sub

        Public Sub JSchedDate()
            moSchedCheck.Attributes.Add("onclick", "return toggleSchedDate('" + moSchedCheck.ClientID + "','" + TdSchedDate1.ClientID + "','" + TdSchedDate2.ClientID + "');")
        End Sub

#End Region

        Private Sub RadiobuttonView_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadiobuttonView.CheckedChanged

            If Me.RadiobuttonView.Checked = True Then
                Me.RadiobuttonPDF.Checked = False
                Me.RadiobuttonTXT.Checked = False
                ' Dim args As New EventArgs()
                'ButtonClick(sender, e)
                'SelectedViewPDFOption(sender, args)
                RaiseEvent SelectedViewPDFOption(sender, e)
            End If

        End Sub

        Private Sub RadiobuttonPDF_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadiobuttonPDF.CheckedChanged
            If Me.RadiobuttonPDF.Checked = True Then
                Me.RadiobuttonView.Checked = False
                Me.RadiobuttonTXT.Checked = False
                'RaiseEvent SelectedViewPDFOption(Me)
                'ButtonClick(sender, e)
                RaiseEvent SelectedViewPDFOption(sender, e)
            End If
        End Sub

        Private Sub RadiobuttonTXT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadiobuttonTXT.CheckedChanged

            If Me.RadiobuttonTXT.Checked = True Then
                Me.RadiobuttonPDF.Checked = False
                Me.RadiobuttonView.Checked = False
                ' Dim args As New EventArgs()
                'ButtonClick(sender, e)
                'SelectedViewPDFOption(sender, args)
                RaiseEvent SelectedViewPDFOption(sender, e)
            End If

        End Sub

        Private Sub moDestDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moDestDrop.SelectedIndexChanged
            RaiseEvent SelectedDestOptionChanged(sender, e)
        End Sub


    End Class
End Namespace


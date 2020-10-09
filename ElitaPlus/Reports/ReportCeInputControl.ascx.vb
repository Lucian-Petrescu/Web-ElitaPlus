Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class ReportCeInputControl
        Inherits System.Web.UI.UserControl

#Region "Contants"

        Private ReadOnly SCHED_START_TIME As Double = 23.5 ' 23:30 11:30 PM      
        Private ReadOnly SCHED_DELAYED_START_TIME As Double = 0.01 ' 12:01 12:01 AM      
        Private Company_China As String = "ACN"
        Private DEST_EMAIL As String = "Email"
        Private SSRS_NOT_CONFIGURED As String = "SSRS Not Configured"
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
            Set(Value As String)
                isProgressVisible.Value = Value
            End Set
        End Property

        Public Property ExcludeSSRSConfigurationCheck() As Boolean
            Get
                Return hdExcludeSSRSConfigurationCheck.Visible
            End Get
            Set(Value As Boolean)
                hdExcludeSSRSConfigurationCheck.Visible = Value
            End Set
        End Property

        Public Property IsTheReportCeVisible() As String
            Get
                Return isReportCeVisible.Value
            End Get
            Set(Value As String)
                isReportCeVisible.Value = Value
            End Set
        End Property

        Public Property IsFormatVisible() As Boolean
            Get
                Return moFormatPanel.Visible
            End Get
            Set(Value As Boolean)
                moFormatPanel.Visible = Value
            End Set
        End Property

        Private Property ViewEnable() As Boolean
            Get
                Return RadiobuttonView.Enabled
            End Get
            Set(Value As Boolean)
                RadiobuttonView.Enabled = Value
            End Set
        End Property

        Private Property PdfEnable() As Boolean
            Get
                Return RadiobuttonView.Enabled
            End Get
            Set(Value As Boolean)
                RadiobuttonPDF.Enabled = Value
            End Set
        End Property

        Private Property ExportDataEnable() As Boolean
            Get
                Return RadiobuttonTXT.Enabled
            End Get
            Set(Value As Boolean)
                RadiobuttonTXT.Enabled = Value
            End Set
        End Property

        Public Property ViewVisible() As Boolean
            Get
                Return RadiobuttonView.Visible
            End Get
            Set(Value As Boolean)
                RadiobuttonView.Visible = Value
                imgView.Visible = Value
            End Set
        End Property

        Public Property SchedulerVisible() As Boolean
            Get
                Return RSched.Visible
            End Get
            Set(Value As Boolean)
                RSched.Visible = Value
            End Set
        End Property

        Public Property PdfVisible() As Boolean
            Get
                Return RadiobuttonPDF.Visible
            End Get
            Set(Value As Boolean)
                RadiobuttonPDF.Visible = Value
                imgPdf.Visible = Value
            End Set
        End Property

        Public Property ExportDataVisible() As Boolean
            Get
                Return RadiobuttonTXT.Visible
            End Get
            Set(Value As Boolean)
                RadiobuttonTXT.Visible = Value
                imgTxt.Visible = Value
            End Set
        End Property
        Public Property DestinationVisible() As Boolean
            Get
                Return moDestDrop.Visible
            End Get
            Set(Value As Boolean)
                moDestDrop.Visible = Value
                moDestLabel.Visible = Value
            End Set
        End Property
        Private Property ViewChecked() As Boolean
            Get
                Return RadiobuttonView.Checked
            End Get
            Set(Value As Boolean)
                RadiobuttonView.Checked = Value
            End Set
        End Property

        Private Property PdfChecked() As Boolean
            Get
                Return RadiobuttonPDF.Checked
            End Get
            Set(Value As Boolean)
                RadiobuttonPDF.Checked = Value
            End Set
        End Property

        Private Property ExportDataChecked() As Boolean
            Get
                Return RadiobuttonTXT.Checked
            End Get
            Set(Value As Boolean)
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
            Set(Value As String)
                moDestDrop.ClearSelection()
                Dim dv As DataView = LookupListNew.GetReportCeDestLookupList(Authentication.LangId, False)
                moDestDrop.Items.FindByValue(LookupListNew.GetIdFromCode(dv, Value.ToString).ToString).Selected = True
            End Set
        End Property

        Public ReadOnly Property LanguageSelected() As Guid
            Get
                Return New Guid(DPLang.SelectedItem.Value)
            End Get
            'Set(ByVal Value As Boolean)
            '    RadiobuttonTXT.Checked = Value
            'End Set
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

        'Public Property ReportName() As String
        '    Get
        '        Return orptname
        '    End Get
        '    Set(ByVal Value As String)
        '        orptname = Value
        '    End Set
        'End Property


        Public Property ModifyFileNameChecked() As Boolean
            Get
                Return chkUpdateFileName.Checked
            End Get
            Set(Value As Boolean)
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
            Set(Value As String)
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            RestoreAttributes()
            If Not IsPostBack Then
                Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)
                oPage.AddCalendar(BtnSchedDate, moSchedDateText, "", "N", "Y")
                JSchedDate()
                PopulateForm()
            Else
                resizeform()
                CheckIfComingFromSaveConfirm()
            End If
        End Sub

#End Region

#Region "Handlers-Button"

        Private Sub btnViewHidden_Click(sender As System.Object, e As System.EventArgs) Handles btnViewHidden.Click
            Dim oCEHelper As SSHelper
            Dim rptCeBase As New ReportCeBase

            Dim oSSHelper As SSHelper

            Dim strReportServer As String = ""
            Dim strReportName As String = ""

            Try
                strReportServer = Request.QueryString("REPORT_SERVER")

                If strReportServer IsNot Nothing Then
                    oSSHelper = ReportCeBase.GetSSHelper()
                End If

            Catch ex As Exception
                AppConfig.DebugLog(ex.Message)
                ' Connection Problem               
                DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_CONNECTION_PROBLEM"))
                Exit Sub
            End Try
            Try
                If strReportServer IsNot Nothing AndAlso strReportServer.Equals("SSRS") Then
                    Dim oParams As ReportCeBase.Params
                    oParams = CType(Session(ReportCeBase.SESSION_PARAMETERS_KEY), ReportCeBase.Params)
                    strReportName = oParams.msRptName
                    If oParams.msRptName.ToUpper.IndexOf("_EXP") > 0 OrElse oParams.msRptName.ToUpper.IndexOf("-EXP") > 0 Then
                        If moSchedCheck.Checked = True Then
                            ObtainSchedDate(oParams)
                        End If

                        RunOracleExportReport(oParams, Request.QueryString("REPORT_TYPE"), Request.QueryString("EXP_REPORT_PROC"))

                    Else
                        'CType(Me.BindingContainer.Page(), ElitaPlusPage).SetProgressTimeOutScript()
                        Session("REPORT_SERVER") = strReportServer
                        Session("REPORT_NAME") = strReportName
                        WindowOpen(ReportCeOpenWindowForm.URL & "?REPORT_SERVER=" & strReportServer, TranslationBase.TranslateLabelOrMessage(ReportCeBaseForm.REPORTS_UICODE) &
                                                ": " & oParams.msRptWindowName)
                    End If
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub btnErrorHidden_Click(sender As System.Object, e As System.EventArgs) Handles btnErrorHidden.Click
            Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)
            Dim oErrorController As ErrorController
            Dim oMsgController As MessageController
            oErrorController = CType(BindingContainer.FindControl("ErrorCtrl"), ErrorController)
            If oErrorController IsNot Nothing Then
                oErrorController.AddError(moReportCeStatus.Value, False)
                oErrorController.AddError(moReportCeErrorMsg.Value, False)
                oErrorController.Show()
            Else
                If oPage.ErrControllerMaster IsNot Nothing Then
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

        Protected Sub WindowOpen(url As String, name As String)
            Dim sJavaScript As String

            '   AddHiddenForWindowOpen()
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "var newWindow;" & Environment.NewLine
            sJavaScript &= "newWindow = windowOpen('" & url & "','" & name & "');" & Environment.NewLine
            sJavaScript &= "newWindow.document.title = '" & name & "';" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)
            oPage.RegisterStartupScript("MaximizedWindowOpen", sJavaScript)
        End Sub

        Private Sub DisplayErrorMsg(errormsg As String)
            Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)
            Dim oErrorController As ErrorController
            Dim oMsgController As MessageController
            oErrorController = CType(BindingContainer.FindControl("ErrorCtrl"), ErrorController)
            If oErrorController IsNot Nothing Then
                oErrorController.AddError(moReportCeStatus.Value, False)
                oErrorController.AddError(moReportCeErrorMsg.Value, False)
                oErrorController.AddError(errormsg, False)
                oErrorController.Show()
            Else
                If oPage.ErrControllerMaster IsNot Nothing Then
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
        Public Sub LangDPVisible(isvisible As Boolean)
            RLang.Visible = isvisible
            lblLang.Visible = isvisible
            DPLang.Visible = isvisible
        End Sub
        Public Sub UpdateFileNameControlVisible(isvisible As Boolean)
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
        ' Public rptname As String
        ' Dim orptname As String    
        Private moState As MyState
        'Public Event SelectedViewPDFOption(ByVal aSrc As ReportCeInputControl)
        Public Event SelectedViewPDFOption(sender As Object, e As System.EventArgs)
        Public Event SelectedDestOptionChanged(sender As Object, e As System.EventArgs)
        'Public ButtonClick As EventHandler(ByVal sender As Object, ByVal e As System.EventArgs)


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

        Public Sub populateReportLanguages(oReportName As String)
            Dim oCEHelper As SSHelper
            Dim drResult As String = oReportName + strExportRptExt
            Dim rowView As DataRowView


            Dim oSSHelper As SSHelper
            Dim strReportServer As String = ""
            strReportServer = Request.QueryString("REPORT_SERVER")

            If oReportName = String.Empty Then
                RLang.Visible = False
                'rptname = stringemp
            Else
                'rptname = oReportName
                Try
                    If strReportServer IsNot Nothing Then
                        oSSHelper = ReportCeBase.GetSSHelper()
                    End If
                Catch ex As Exception
                    ' Connection Problem
                    Throw New GUIException(Message.MSG_CONN_PROBLEM, oCEHelper.RptStatus.SS_CONNECTION_PROBLEM.ToString)
                End Try
                Dim oDataView As DataView
                Try
                    If strReportServer IsNot Nothing AndAlso strReportServer.Equals("SSRS") Then
                        'Future SSRS code
                        Dim dt As DataTable = New DataTable()
                        dt.Columns.Add("Language_id", GetType(Byte()))
                        dt.Columns.Add("Language_code", GetType(String))
                        Dim dr As DataRow = dt.NewRow

                        dr("Language_id") = ElitaPlusIdentity.Current.ActiveUser.LanguageId.ToByteArray
                        dr("Language_code") = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        dt.Rows.Add(dr)
                        'ds.Tables.Add(dt)
                        oDataView = dt.DefaultView
                    End If
                Catch ex As Exception
                    ' Report Not Found
                    Throw New GUIException(Message.MSG_NO_REPORTS_FOUND, oCEHelper.RptStatus.SS_PATH_PROBLEM.ToString)
                End Try

                If strReportServer Is Nothing Then
                    'Me.BindListTextToDataView(DPLang, oDataView, CEHelper.ReportDataview.COL_REP_NAME, CEHelper.ReportDataview.COL_REP_ID)
                Else
                    BindListTextToDataView(DPLang, oDataView, "Language_code", "Language_id")
                End If


            End If
        End Sub

        Protected Sub BindListTextToDataView(lstControl As ListControl, Data As DataView,
    Optional ByVal TextColumnName As String = "DESCRIPTION", Optional ByVal ValueColumnName As String = "ID", Optional ByVal AddNothingSelected As Boolean = True)
            Dim i As Integer
            Dim descColumnValue As String
            Dim idCoumnValue As Guid
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            lstControl.Items.Clear()

            For i = 0 To Data.Count - 1
                descColumnValue = Data(i)(TextColumnName).ToString.Remove(0, Data(i)(TextColumnName).ToString.IndexOf(ReportName_LangSep) + 1)

                If LookupListNew.GetDescriptionFromCode(LookupListNew.LK_LANGUAGES, descColumnValue) IsNot Nothing Then

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

        Public Function getReportName(orptname As String, isExport As Boolean) As String
            If isExport = False Then
                Return (orptname + ReportName_LangSep + LanguageCodeSelected)
            Else
                Return (orptname + ReportName_LangSep + LanguageCodeSelected)
            End If

        End Function
        Public Function getReportWindowTitle(orptwindowtitle As String) As String
            ' Return (TranslationBase.TranslateLabelOrMessage(orptwindowtitle) + ExpRptName_Sep + LanguageDescSelected)
            Return (orptwindowtitle + ExpRptName_Sep + LanguageDescSelected)
        End Function

        Public Function getCultureValue(isExport As Boolean, Optional ByVal company_id As String = "") As String
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

            If dvCurrency IsNot Nothing AndAlso dvCurrency.Count > 0 Then
                If dvCurrency.Item(0)(LookupListNew.COL_CURRENCY_NOTATION) IsNot System.DBNull.Value Then
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
            Dim langId As Guid = Authentication.LangId
            Dim repDestId As Guid
            ' Dim repDestLkl As DataView
            'repDestLkl = LookupListNew.GetReportCeDestLookupList(langId, False)

            Dim repDesLKl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("RCEDEST", Thread.CurrentPrincipal.GetLanguageCode())
            Dim filteredList As DataElements.ListItem() = repDesLKl
            Dim oComp As Company
            oComp = New Company(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
            If Not oComp.Code.Equals(Company_China) Then
                'repDestLkl.RowFilter = "code <> '" & Codes.REPORTCE_DEST__EMAIL & "' and language_id = '" & GuidControl.GuidToHexString(langId) & "'"
                'repDestLkl.RowFilter = "code <> '" & Codes.REPORTCE_DEST__EMAIL & "' and language_id = '" & GuidControl.GuidToHexString(langId) & "'"
                filteredList = (From x In repDesLKl
                                Where Not x.Code = Codes.REPORTCE_DEST__EMAIL
                                Select x).ToArray()
            End If

            'dvRepairCode.RowFilter = " code = '" + Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE + "'"
            'ElitaPlusPage.BindListControlToDataView(moDestDrop, repDestLkl, , , False)
            moDestDrop.Populate(filteredList, New PopulateOptions())

            'repDestId = LookupListNew.GetIdFromCode(repDestLkl, Codes.REPORTCE_DEST__WEB)
            repDestId = (From lst In filteredList
                         Where lst.Code = Codes.REPORTCE_DEST__WEB
                         Select lst.ListItemId).FirstOrDefault()
            ElitaPlusPage.SetSelectedItem(moDestDrop, repDestId)

            ' Schedule Date
            Dim t As Date = Date.Now
            moSchedDateText.Text = ElitaPlusPage.GetDateFormattedString(t)

            If Request.QueryString("REPORT_SERVER") IsNot Nothing AndAlso Request.QueryString("REPORT_SERVER").Equals("SSRS") Then
                PdfVisible = False
            Else
                If Not ExcludeSSRSConfigurationCheck Then
                    DisplayErrorMsg(SSRS_NOT_CONFIGURED)
                    Exit Sub
                End If
            End If
        End Sub

        Private Sub ObtainDestParam(oParams As ReportCeBase.Params)
            Dim langId As Guid = Authentication.LangId
            Dim repDestLkl As DataView = LookupListNew.GetReportCeDestLookupList(langId, False)
            Dim repDestId As Guid = ElitaPlusPage.GetSelectedItem(moDestDrop)
            Dim repDestCode As String = LookupListNew.GetCodeFromId(repDestLkl, repDestId)
            oParams.moDest = CType([Enum].Parse(GetType(ceDestination), repDestCode), ceDestination)
        End Sub

        Private Sub ObtainSchedDate(oParams As ReportCeBase.Params)
            Dim oStartDateTime As DateTime = Nothing
            Dim sStartDate As String

            If moSchedCheck.Checked = True Then
                ' Schedule
                oParams.moAction = ReportCeBase.RptAction.SCHEDULE
                GUIException.ValidateDate(moSchedDateLabel, moSchedDateText.Text)
                oStartDateTime = DateHelper.GetDateValue(moSchedDateText.Text)
                oStartDateTime = oStartDateTime.AddHours(SCHED_START_TIME)
            End If

            oParams.moSched = New ceSchedule(oStartDateTime)
        End Sub


        Public Function GetSchedDate() As DateTime
            Dim oStartDateTime As DateTime = Nothing
            Dim sStartDate As String
            GUIException.ValidateDate(moSchedDateLabel, moSchedDateText.Text)

            If moSchedCheck.Checked = True Then

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



        Private Sub ObtainFtpParams(oParams As ReportCeBase.Params)

            Dim oFtpSiteId As Guid
            If Not ElitaPlusIdentity.Current.ActiveUser.Company.FtpSiteId = Guid.Empty OrElse Nothing Then
                oFtpSiteId = ElitaPlusIdentity.Current.ActiveUser.Company.FtpSiteId
            Else
                oFtpSiteId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.FtpSiteId
            End If
            Dim fileName As String

            If chkUpdateFileName.Checked = True Then
                fileName = MiscUtil.GetModifiedUniqueFullPath(String.Empty,
                                 SetModifiedFileName,
                                 oParams.msRptWindowName)
            Else
                fileName = MiscUtil.GetUniqueFullPath(String.Empty,
                                 ElitaPlusPrincipal.Current.Identity.Name,
                                 oParams.msRptWindowName)
            End If

            Dim oReportFormat As SSHelper.RepFormat = ReportCeBase.GetRepFormat(oParams)

            fileName = fileName & oReportFormat.msFileExt
            If Not oFtpSiteId.Equals(Guid.Empty) Then
                Dim oFtpSite As New FtpSite(oFtpSiteId)
                oParams.moSched.ftp = New ceFtp
                With oParams.moSched.ftp
                    .host = oFtpSite.Host
                    .port = CInt(oFtpSite.Port.Value)
                    .userName = oFtpSite.UserName
                    .password = oFtpSite.Password
                    .account = oFtpSite.Account
                    .directory = oFtpSite.Directory
                    .filename = fileName
                End With
            Else
                DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_FTP_PROBLEM"))
                Dim ex As New Exception("FTP")
                Throw ex
            End If

        End Sub

        Private Sub ObtainUDiskParams(oParams As ReportCeBase.Params)

            Dim fileName As String
            If chkUpdateFileName.Checked = True Then
                fileName = MiscUtil.GetModifiedUniqueFullPath(String.Empty,
                                 SetModifiedFileName,
                                 oParams.msRptWindowName)
            Else
                fileName = MiscUtil.GetUniqueFullPath(String.Empty,
                                 ElitaPlusPrincipal.Current.Identity.Name,
                                 oParams.msRptWindowName)
            End If
            Dim oReportFormat As SSHelper.RepFormat = ReportCeBase.GetRepFormat(oParams)

            fileName = fileName & oReportFormat.msFileExt

            oParams.moSched.uDisk = New ceUDisk
            With oParams.moSched.uDisk
                .userName = String.Empty
                .password = String.Empty
                .directory = AppConfig.SS.DestinationDir
                '.directory = "ElitaUDisk\"
                .filename = fileName
            End With


        End Sub


        Private Sub ObtainEmailParams(oParams As ReportCeBase.Params)

            Dim oEmail As String
            If Not ElitaPlusIdentity.Current.ActiveUser.Company.Email = String.Empty OrElse Nothing Then
                oEmail = ElitaPlusIdentity.Current.ActiveUser.Company.Email
                'Else
                '  oEmail = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.email
            End If
            Dim fileName As String

            If chkUpdateFileName.Checked = True Then
                fileName = MiscUtil.GetModifiedUniqueFullPath(String.Empty,
                                 SetModifiedFileName,
                                 oParams.msRptWindowName)
            Else
                fileName = MiscUtil.GetUniqueFullPath(String.Empty,
                                 ElitaPlusPrincipal.Current.Identity.Name,
                                 oParams.msRptWindowName)
            End If

            Dim oReportFormat As SSHelper.RepFormat = ReportCeBase.GetRepFormat(oParams)

            fileName = fileName & oReportFormat.msFileExt
            If Not oEmail.Equals(String.Empty) Then
                'Dim oFtpSite As New FtpSite(oFtpSiteId)
                oParams.moSched.email = New ceEMAIL
                With oParams.moSched.email
                    .Subject = fileName
                    .FromAddress = "ElitaPlus@Assurant.com"
                    .ToAddres = oEmail
                    .CCAddress = oEmail
                    .FileName = fileName
                End With
            Else
                DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_EMAIL_PROBLEM"))
                Dim ex As New Exception("EMAIL")
                Throw ex
            End If

        End Sub

        Private Sub ObtainSchedParams()
            Dim oParams As ReportCeBase.Params
            Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)
            Try
                oParams = CType(Session(ReportCeBase.SESSION_PARAMETERS_KEY), ReportCeBase.Params)
                ObtainDestParam(oParams)
                ObtainSchedDate(oParams)

                If oParams.moRptFormat = Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeBase.RptFormat.JAVA _
                    AndAlso (oParams.moDest = ceDestination.FTP OrElse oParams.moDest = ceDestination.EMAIL) Then
                    DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("DEST_FTP_NOT_AVAILBLE_FOR_VIEW_REPORT"))
                    Dim ex As New Exception("VIEW")
                    Throw ex
                ElseIf oParams.moDest = ceDestination.FTP Then
                    ObtainFtpParams(oParams)

                ElseIf oParams.moDest = ceDestination.EMAIL Then
                    ObtainEmailParams(oParams)
                Else
                    ' UnManaged Disk
                    ObtainUDiskParams(oParams)
                End If
            Catch ex As Exception
                If ex.Message <> "FTP" AndAlso ex.Message <> "VIEW" Then
                    DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_UNKNOWN_PROBLEM"))
                End If
                Throw ex
            End Try
        End Sub

#End Region

#Region "Report Instances"

        'Private Sub VerifyInstances(ByVal rptCeBase As ReportCeBase)
        '    Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)
        '    Dim instanceId As String = Nothing
        '    Dim instanceFormat As String = Nothing
        '    Dim instancetimeStamp As Date = Nothing
        '    Dim moParams As ReportCeBase.Params
        '    Dim moRptParams As ArrayList
        '    Dim dsrptInstance As DataSet

        '    Dim moRptPath As String
        '    Dim sbMsg As New System.Text.StringBuilder

        '    Try

        '        moParams = CType(Session(rptCeBase.SESSION_PARAMETERS_KEY), ReportCeBase.Params)


        '        moRptParams = rptCeBase.GetCeRptParams(moParams.moRptParams)
        '        moRptPath = AppConfig.CE.RootDir & "\" & moParams.msRptName

        '        dsrptInstance = rptCeBase.RptParamsInstanceStatus(moRptPath, moRptParams)
        '        If dsrptInstance.Tables(0).Rows.Count > 0 Then
        '            instanceId = dsrptInstance.Tables(0).Rows(0)("INSTANCE_KEY").ToString
        '            instanceFormat = dsrptInstance.Tables(0).Rows(0)("FORMAT").ToString
        '            instancetimeStamp = DateHelper.GetDateValue(dsrptInstance.Tables(0).Rows(0)("INSTANCE_TIMESTAMP").ToString)
        '        End If
        '        ', instanceId, intanceFormat, intancetimeStamp)

        '        sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Message.MSG_INSTANCE_EXITS))
        '        sbMsg.Append(" " + oPage.GetLongDateFormattedString(instancetimeStamp) + " ")
        '        sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Message.MSG_VIEW_REPORT))

        '        If Not instanceId Is Nothing Then
        '            oPage.DisplayMessage(sbMsg.ToString, "", oPage.MSG_BTN_YES_NO, oPage.MSG_TYPE_CONFIRM, Me.HiddenRptPromptResponse, False)

        '            Me.moState = New MyState
        '            With moState
        '                .ActionInProgress = oPage.DetailPageCommand.Back
        '                .ReportName = moParams.msRptName
        '                .Instanceid = CType(instanceId.ToString, Long)
        '                .ReportFormat = instanceFormat
        '            End With
        '            Session(SessionInstanceParams) = moState
        '        Else
        '            oPage.SetProgressTimeOutScript()
        '        End If

        '    Catch ex As Exception
        '        DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_UNKNOWN_PROBLEM"))
        '        Exit Sub
        '    End Try
        'End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)
            Dim confResponse As String = HiddenRptPromptResponse.Value
            HiddenRptPromptResponse.Value = Nothing
            Dim oSSHelper As SSHelper

            Dim strReportServer As String = ""

            Try
                strReportServer = Request.QueryString("REPORT_SERVER")

                If strReportServer IsNot Nothing AndAlso strReportServer.Equals("SSRS") Then
                    oSSHelper = ReportCeBase.GetSSHelper()
                Else
                    If Not ExcludeSSRSConfigurationCheck Then
                        DisplayErrorMsg(SSRS_NOT_CONFIGURED)
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                AppConfig.DebugLog(ex.Message)
                ' Connection Problem
                'Throw New GUIException(Message.MSG_CONN_PROBLEM, oCEHelper.RptStatus.CE_CONNECTION_PROBLEM.ToString)
                DisplayErrorMsg(TranslationBase.TranslateLabelOrMessage("CE_CONNECTION_PROBLEM"))
                Exit Sub
            End Try
        End Sub

        Function SetParameters(nInstanceId As Long, ceFormat As String) As ReportCeBaseForm.Params
            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = moState.ReportName

            Dim moReportFormat As ReportCeBaseForm.RptFormat
            moReportFormat = ReportCeBase.GetReportFormat(ceFormat)

            With params
                .msRptName = reportName
                .msRptWindowName = reportName
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.VIEW
                .instanceId = nInstanceId
            End With
            Return params
        End Function

        Private Sub ViewReport(nInstanceId As Long, ceFormat As String)

            Dim params As ReportCeBaseForm.Params = SetParameters(nInstanceId, ceFormat)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

        Private Sub RunOracleExportReport(oParams As ReportCeBase.Params, strReportType As String, strReportProc As String)
            Dim oReportRequests As ReportRequests = New ReportRequests
            oReportRequests.ReportType = strReportType
            oReportRequests.ReportProc = strReportProc
            oReportRequests.ReportParameters = GetOracleParamsList(oParams)

            Dim reportParams As New System.Text.StringBuilder

            Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)
            If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                oPage.DisplayMessage(Message.MSG_Email_not_configured, "", oPage.MSG_BTN_OK, oPage.MSG_TYPE_ALERT, , True)
            Else
                oReportRequests.UserEmailAddress = ElitaPlusIdentity.Current.EmailAddress
                oReportRequests.Save()
                Dim scheduleDate As Date
                Try
                    scheduleDate = Date.Parse(oParams.moSched.startDateTime)
                Catch ex As Exception
                    scheduleDate = DateHelper.GetDateValue(DateTime.Now.ToString())
                End Try

                oReportRequests.CreateJob(scheduleDate)
                oPage.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", oPage.MSG_BTN_OK, oPage.MSG_TYPE_ALERT, , True)
            End If
        End Sub

        Private Function GetOracleParamsList(oParams As ReportCeBase.Params) As String
            Dim reportParams As New System.Text.StringBuilder
            Dim oRptParam As ReportCeBase.RptParam
            If oParams.moRptParams IsNot Nothing Then
                For Each oRptParam In oParams.moRptParams
                    If oRptParam IsNot Nothing Then
                        reportParams.AppendFormat(oRptParam.moCeHelperParameter.Name & " => '{0}',", oRptParam.moCeHelperParameter.Value)
                    Else
                    End If
                Next
            End If

            Return reportParams.ToString.Trim().Remove(reportParams.ToString.Length - 1)

        End Function
#End Region

#Region "JavaScript"

        Protected Sub resizeform()
            Dim sJavaScript As String
            Dim oPage As ElitaPlusPage = CType(BindingContainer.Page(), ElitaPlusPage)

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

        Private Sub RadiobuttonView_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadiobuttonView.CheckedChanged

            If RadiobuttonView.Checked = True Then
                RadiobuttonPDF.Checked = False
                RadiobuttonTXT.Checked = False
                ' Dim args As New EventArgs()
                'ButtonClick(sender, e)
                'SelectedViewPDFOption(sender, args)
                RaiseEvent SelectedViewPDFOption(sender, e)
            End If

        End Sub

        Private Sub RadiobuttonPDF_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadiobuttonPDF.CheckedChanged
            If RadiobuttonPDF.Checked = True Then
                RadiobuttonView.Checked = False
                RadiobuttonTXT.Checked = False
                'RaiseEvent SelectedViewPDFOption(Me)
                'ButtonClick(sender, e)
                RaiseEvent SelectedViewPDFOption(sender, e)
            End If
        End Sub

        Private Sub RadiobuttonTXT_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadiobuttonTXT.CheckedChanged

            If RadiobuttonTXT.Checked = True Then
                RadiobuttonPDF.Checked = False
                RadiobuttonView.Checked = False
                ' Dim args As New EventArgs()
                'ButtonClick(sender, e)
                'SelectedViewPDFOption(sender, args)
                RaiseEvent SelectedViewPDFOption(sender, e)
            End If

        End Sub

        Private Sub moDestDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moDestDrop.SelectedIndexChanged
            RaiseEvent SelectedDestOptionChanged(sender, e)
        End Sub
    End Class



End Namespace


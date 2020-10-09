Option Explicit On

Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.Common.Zip
Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class SmartStreamInterfaceDetail
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "SMARTSTREAM_INTERFACE_DETAIL"

        Private Const FILETYPE_SMARTSTREAM As String = "SS"
        Private Const FILETYPE_INVOICE As String = "INVOICE"
        Private Const FILETYPE_JOURNAL As String = "JOURNAL"

        Private Const FILETYPE_AG_JOURNAL As String = "JOURNAL"
        Private Const FILETYPE_AG_ADDRESS As String = "ADDRESS"
        Private Const FILETYPE_AG_CLIENT As String = "CUSTOMER"
        Private Const FILETYPE_AG_BANK As String = "BANKDETAILS"
        Private Const FILETYPE_AG_ACCOUNT As String = "ACCOUNT"
        Private Const FILETYPE_AG_SUPPLIER As String = "SUPPLIER"

        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Public Const PAGETITLE As String = "SMARTSTREAM_INTERFACE_DETAIL"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "SMARTSTREAM_INTERFACE_DETAIL"

        Private Const SELECTED_NONE As Integer = -1
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const REPORT_DATE_FORMAT As String = "MM/dd/yyyy"
        Private Const OUTPUT_FILE_NAME As String = "Accounting Interface Detail.txt"
        Private Const OUTPUT_ZIP_FILE_NAME As String = "Accounting Interface Detail.zip"

        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/SMARTSTREAMINTERFACEDETAIL.aspx"
        Public Shared XSLT_URL As String = HttpContext.Current.Server.MapPath("SmartStreamInterfaceDetails.xslt")

#End Region

#Region "Properties"

        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region

#Region "variables"
        Public endDate As Date
        Public beginDate As Date

        Public Class MyState
            Public CompanyId As Guid
            Public CompanyDescription As String
            Public FileName As String
            Public BeginDate As String
            Public EndDate As String
            Public TransmissionId As Guid
            Public SelectedIndex As Integer
            Public JournalType As String
            Public InputXML As String
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        '  Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Validation"

        Private Sub Validate_Begin_End_Dates()

            If moBeginDateText.Text.Trim.ToString = String.Empty Then
                ElitaPlusPage.SetLabelError(moBeginDateLabel)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_REQUIRED_ERR)
            End If

            If moEndDateText.Text.Trim.ToString = String.Empty Then
                ElitaPlusPage.SetLabelError(moEndDateLabel)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_REQUIRED_ERR)
            End If

            GUIException.ValidateDate(moBeginDateLabel, moBeginDateText.Text.Trim.ToString)
            beginDate = DateHelper.GetDateValue(moBeginDateText.Text.Trim.ToString)

            GUIException.ValidateDate(moEndDateLabel, moEndDateText.Text.Trim.ToString)
            endDate = DateHelper.GetDateValue(moEndDateText.Text.Trim.ToString)

            If endDate < beginDate Then
                ElitaPlusPage.SetLabelError(moBeginDateLabel)
                ElitaPlusPage.SetLabelError(moEndDateLabel)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
            End If

        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrControllerMaster.Clear_Hide()
            ClearErrLabels()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

            InstallProgressBar()

            If CompanyMultipleDrop.Visible = False Then
                HideHtmlElement(trcomp.ClientID)
            End If

            btnGenRpt.UseSubmitBehavior = False
            btnGenRpt.OnClientClick = "_spFormOnSubmitCalled = false; _spSuppressFormOnSubmitWrapper=true;"

            btnDownLoadXML_WRITE.UseSubmitBehavior = False
            btnDownLoadXML_WRITE.OnClientClick = "_spFormOnSubmitCalled = false; _spSuppressFormOnSubmitWrapper=true;"

            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    InitializeForm()
                    'Date Calendars
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)

                    If CallingParameters IsNot Nothing Then
                        State.TransmissionId = CType(CallingParameters, MyState).TransmissionId
                        GenerateReport(CType(CallingParameters, MyState))
                    End If
                Else
                    ClearErrLabels()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnDownLoadXML_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDownLoadXML_WRITE.Click
            Try
                DownLoadXML()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
                   Handles multipleDropControl.SelectedDropChanged
            Try
                If moBeginDateText.Text <> String.Empty Then
                    PopulateFileName()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBeginDateText_TextChanged(sender As Object, e As System.EventArgs) Handles moBeginDateText.TextChanged
            Try
                If moBeginDateText.Text.Trim.ToString <> String.Empty AndAlso moEndDateText.Text.Trim.ToString <> String.Empty Then
                    PopulateFileName()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moEndDateText_TextChanged(sender As Object, e As EventArgs) Handles moEndDateText.TextChanged
            Try
                If moBeginDateText.Text.Trim.ToString <> String.Empty AndAlso moEndDateText.Text.Trim.ToString <> String.Empty Then
                    PopulateFileName()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            ReturnToCallingPage(State.TransmissionId)
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(CompanyMultipleDrop.CaptionLabel)
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(moEndDateLabel)
            ClearLabelErrSign(lblFileName)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateCompanyDropDown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            If dv.Count <= 1 Then
                CompanyMultipleDrop.SetControl(False, CompanyMultipleDrop.MODES.NEW_MODE, False, dv, CompanyMultipleDrop.NO_CAPTION, True, False)
            Else
                CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, True, dv, CompanyMultipleDrop.NO_CAPTION, True, False)
            End If
        End Sub

        Sub PopulateFileName()

            cboFileName.Items.Clear()

            If CompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_COMPANY_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            Validate_Begin_End_Dates()

            Dim sTmpFileFilterCondition As String = "Description not like '%.TMP'"
            'Me.BindListControlToDataView(Me.cboFileName, LookupListNew.getAccountingFileNames(CompanyMultipleDrop.SelectedGuid, beginDate.ToString(SP_DATE_FORMAT), endDate.ToString(SP_DATE_FORMAT), sTmpFileFilterCondition), , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = CompanyMultipleDrop.SelectedGuid
            Dim fileLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.AccountingTransmissionFileByCompany, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

            Dim filterList As DataElements.ListItem() = (From x In fileLkl
                                                         Where Not x.Translation Like "%.TMP"
                                                         Select x).ToArray()
            Dim filteredAcctFileLst As DataElements.ListItem() = (From acclist In filterList
                                                                  Where ((Date.ParseExact(acclist.ExtendedCode, "MMddyyyy", DateTimeFormatInfo.InvariantInfo) >= beginDate) AndAlso (Date.ParseExact(acclist.ExtendedCode, "MMddyyyy", DateTimeFormatInfo.InvariantInfo) <= endDate))
                                                                  Select acclist).ToArray()
            cboFileName.Populate(filteredAcctFileLst, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

        End Sub

        Private Sub InitializeForm()
            TheReportCeInputControl.SetExportOnly()
            TheReportCeInputControl.ExcludeSSRSConfigurationCheck = True
            PopulateCompanyDropDown()
            moBeginDateText.Text = String.Empty
            moEndDateText.Text = String.Empty
        End Sub

#End Region

#Region "Report"

        Private Sub DownLoadXML()

            'Dates
            Validate_Begin_End_Dates()

            If cboFileName.SelectedIndex = SELECTED_NONE Then
                ElitaPlusPage.SetLabelError(lblFileName)
                Throw New GUIException(Message.MSG_INVALID_FILE_NAME, Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_NAME_MUST_BE_SELECTED_ERR)
            End If

            Dim FileId As Guid = New Guid(cboFileName.SelectedItem.Value)
            Dim _accttransmission As AcctTransmission = New AcctTransmission(FileId, False)

            Dim downloadFileName As String = _accttransmission.FileName.ToUpper()
            If Not downloadFileName.EndsWith(".XML") Then
                downloadFileName = downloadFileName & ".XML"
            End If

            Dim compressionmethod As ICompressionProvider
            compressionmethod = CompressionProviderFactory.Current.CreateInstance(CompressionProviderType.IonicZip)
            Dim outputmemorystream As MemoryStream = New MemoryStream()
            compressionmethod.Compress(_accttransmission.FileText, outputmemorystream, downloadFileName)

            Dim downloadResponse As System.Web.HttpResponse
            downloadResponse = HttpContext.Current.Response
            downloadResponse.ClearContent()
            downloadResponse.ClearHeaders()
            downloadResponse.ContentType = "text/csv"
            downloadResponse.AddHeader("Content-Disposition", "attachment; filename=" & downloadFileName.Replace(".XML", ".ZIP"))
            downloadResponse.BinaryWrite(outputmemorystream.ToArray())
            downloadResponse.Flush()
            downloadResponse.End()

        End Sub

        Private Sub GenerateReport()

            Dim CompanyId As Guid = CompanyMultipleDrop.SelectedGuid
            Dim FileName As String
            Dim Filetype As String

            If CompanyId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dates
            Validate_Begin_End_Dates()

            If cboFileName.SelectedIndex = SELECTED_NONE Then
                ElitaPlusPage.SetLabelError(lblFileName)
                Throw New GUIException(Message.MSG_INVALID_FILE_NAME, Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_NAME_MUST_BE_SELECTED_ERR)
            End If

            FileName = cboFileName.SelectedItem.ToString()
            Filetype = GetfileType(CompanyId, cboFileName.SelectedItem.ToString().ToUpper())

            Dim _accttransmissionsearchDev As AcctTransmission.AcctTransmissionSearchDV = New AcctTransmission().GetTransmissionsForReport(CompanyId, FileName, New Guid(cboFileName.SelectedItem.Value))

            If _accttransmissionsearchDev.Count > 0 Then
                Dim _accttransmission As AcctTransmission = New AcctTransmission(GuidControl.ByteArrayToGuid(_accttransmissionsearchDev.Item(0)(AcctTransmission.AcctTransmissionSearchDV.COL_ACCT_TRANSMISSION_ID)), False)

                ' Fetching Actual FileType for SmartStream type
                If Filetype = FILETYPE_SMARTSTREAM Then
                    Dim XML_Root_Node As String
                    XML_Root_Node = GetXMLRootNode(_accttransmission.FileText)
                    If XML_Root_Node.ToUpper().Contains(FILETYPE_INVOICE) Then
                        Filetype = FILETYPE_SMARTSTREAM & FILETYPE_INVOICE
                    ElseIf XML_Root_Node.ToUpper().Contains(FILETYPE_JOURNAL) Then
                        Filetype = FILETYPE_SMARTSTREAM & FILETYPE_JOURNAL
                    End If
                End If

                GenerateExport(_accttransmission.FileText, Filetype, _accttransmission.CreatedDate.Value, _accttransmission.FileName, XSLT_URL)
            Else
                ElitaPlusPage.SetLabelError(lblFileName)
                Throw New GUIException(Message.MSG_INVALID_FILE_NAME, Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_NOT_FOUND_ERR)
            End If

        End Sub

        Private Sub GenerateReport(CallingParams As MyState)

            Dim FileName As String
            Dim Filetype As String
            Dim arrfileype() As String
            Dim xslt_file_path As String = XSLT_URL.ToLower().Replace("interfaces", "reports")

            'Use this section to set fields based on parameters passed.
            'Because we're calling via other page and not directly, disable controls
            EnableDisableControls(BtnBeginDate, True)
            EnableDisableControls(BtnEndDate, True)
            EnableDisableControls(cboFileName, True)
            EnableDisableControls(moBeginDateText, True)
            EnableDisableControls(moEndDateText, True)
            EnableDisableControls(multipleDropControl, True)

            ControlMgr.SetVisibleControl(Me, btnGenRpt, False)
            ControlMgr.SetVisibleControl(Me, btnBack, True)
            MenuEnabled = False

            moBeginDateText.Text = CallingParams.BeginDate
            moEndDateText.Text = CallingParams.EndDate

            multipleDropControl.SelectedGuid = CallingParams.CompanyId
            cboFileName.Items.Add(CallingParams.FileName)

            Validate_Begin_End_Dates()

            Filetype = GetfileType(CallingParams.CompanyId, CallingParams.FileName)

            Dim _accttransmission As AcctTransmission = New AcctTransmission(CallingParams.TransmissionId, False)

            ' Fetching Actual FileType for SmartStream type
            If Filetype = FILETYPE_SMARTSTREAM Then
                Dim XML_Root_Node As String
                XML_Root_Node = GetXMLRootNode(_accttransmission.FileText)
                If XML_Root_Node.ToUpper().Contains(FILETYPE_INVOICE) Then
                    Filetype = FILETYPE_SMARTSTREAM & FILETYPE_INVOICE
                ElseIf XML_Root_Node.ToUpper().Contains(FILETYPE_JOURNAL) Then
                    Filetype = FILETYPE_SMARTSTREAM & FILETYPE_JOURNAL
                End If
            End If

            GenerateExport(_accttransmission.FileText, Filetype, _accttransmission.CreatedDate.Value, _accttransmission.FileName, xslt_file_path)

        End Sub

        Private Sub GenerateExport(input_XML As String, file_type As String, file_created_date As Date, file_name As String, xslt_file_path As String, Optional ByVal field_separator As String = "¦")
            Try

                Dim transform As XslCompiledTransform = New XslCompiledTransform()
                Dim XSLTContent As String

                If Not System.IO.File.Exists(xslt_file_path) Then
                    Throw New System.IO.IOException("Required .XSLT file not found.")
                End If

                Dim stream_Reader As StreamReader = New StreamReader(xslt_file_path)
                XSLTContent = stream_Reader.ReadToEnd()

                Dim sr As System.IO.StringReader
                sr = New System.IO.StringReader(XSLTContent)

                Dim xr As XmlReader
                xr = XmlReader.Create(sr)
                transform.Load(xr)

                Dim sw As StringWriter = New StringWriter()
                sr = New System.IO.StringReader(input_XML)

                xr = XmlReader.Create(sr)

                Dim accounting_interface_xslt As New XsltArgumentList()
                accounting_interface_xslt.AddParam("report_type", "", file_type)
                accounting_interface_xslt.AddParam("field_separator", "", field_separator)

                'Setting File Name to Report
                Try
                    accounting_interface_xslt.AddParam("file_name", "", file_name.Substring(file_name.LastIndexOf("-") + 1, (file_name.LastIndexOf(".") - (file_name.LastIndexOf("-") + 1))))
                Catch ex As Exception
                    ex = Nothing
                    accounting_interface_xslt.AddParam("file_name", "", file_name)
                End Try

                'Setting Date format to Report
                accounting_interface_xslt.AddParam("created_date", "", file_created_date.ToString(REPORT_DATE_FORMAT))
                transform.Transform(xr, accounting_interface_xslt, sw)

                Dim compressionmethod As ICompressionProvider
                compressionmethod = CompressionProviderFactory.Current.CreateInstance(CompressionProviderType.IonicZip)
                Dim outputmemorystream As MemoryStream = New MemoryStream()
                compressionmethod.Compress(sw.ToString(), outputmemorystream, OUTPUT_FILE_NAME)

                Dim downloadResponse As System.Web.HttpResponse
                downloadResponse = HttpContext.Current.Response

                downloadResponse.ClearContent()
                downloadResponse.ClearHeaders()
                downloadResponse.ContentType = "text/csv"
                downloadResponse.AddHeader("Content-Disposition", "attachment; filename=" & OUTPUT_ZIP_FILE_NAME)
                downloadResponse.BinaryWrite(outputmemorystream.ToArray())
                downloadResponse.Flush()
                downloadResponse.End()

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Function GetXMLRootNode(filetext As String) As String
            Dim doc As New System.Xml.XmlDocument
            doc.LoadXml(filetext)
            Return doc.DocumentElement.Name
        End Function

        Private Function GetfileType(company_Id As Guid, filename As String) As String
            Dim rtnvalue As String = String.Empty
            Dim arrfileype() As String

            If GetAccountingSystem(company_Id) = FelitaEngine.SMARTSTREAM_PREFIX Then
                ' This is temporary value, actual value will be calculated from XML 
                rtnvalue = FILETYPE_SMARTSTREAM
            Else
                arrfileype = filename.Split(CChar("-"))

                Select Case arrfileype(1)
                    Case FILETYPE_AG_JOURNAL
                        rtnvalue = FILETYPE_AG_JOURNAL
                        Exit Select
                    Case FILETYPE_AG_ADDRESS
                        rtnvalue = FILETYPE_AG_ADDRESS
                        Exit Select
                    Case FILETYPE_AG_CLIENT
                        rtnvalue = FILETYPE_AG_CLIENT
                        Exit Select
                    Case FILETYPE_AG_BANK
                        rtnvalue = FILETYPE_AG_BANK
                        Exit Select
                    Case FILETYPE_AG_ACCOUNT
                        rtnvalue = FILETYPE_AG_ACCOUNT
                        Exit Select
                    Case FILETYPE_AG_SUPPLIER
                        rtnvalue = FILETYPE_AG_SUPPLIER
                        Exit Select
                    Case Else
                        rtnvalue = String.Empty
                End Select
            End If

            Return rtnvalue
        End Function

        Private Function GetAccountingSystem(CompanyId As Guid) As String
            Dim _company As New Company(CompanyId)
            Dim _acctCompany As New AcctCompany(_company.AcctCompanyId)

            Return LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _acctCompany.AcctSystemId)
        End Function

#End Region

    End Class

End Namespace
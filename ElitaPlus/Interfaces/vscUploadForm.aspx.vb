Imports Assurant.ElitaPlus.DALObjects
Imports System.Data.OleDb
Imports System.Xml
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad
Imports System.IO
Imports System.Text
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Interfaces

    Partial Class vscUploadForm
        Inherits ElitaPlusSearchPage


#Region "Page State"

        Class MyState
            Public SelectedTableId As Guid
            'Public SelectedSplitFileProcessedId As Guid
            Public FileName As String
            Public xmlData As String
            ' Public allDs As DataSet
            Public SelectedTableLayout As String = ""
            Public intStatusId As Guid
            Public searchDV As DataView = Nothing
            Public UploadType As String = String.Empty
            'Public errorStatus As InterfaceStatusWrk.IntError
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

#Region "Constants"
        Public Const PAGETITLE As String = "VSC_UPLOADS"
        Public Const PAGETAB As String = "INTERFACES"
        'Private Const SP_SPLIT As Integer = 0
        'Private Const SP_DELETE As Integer = 1
        'Private Const PORT As Integer = 21

        'Private Const INTERFACE_CODE_SPLIT_SYSTEM As String = "003"

        'Private Const SPLIT_FILE_REQUIRED As String = "SPLIT_FILE_REQUIRED"


#End Region

#Region "Variables"

        'Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moInterfaceProgressControl As InterfaceProgressControl


#End Region

#Region "Properties"

        Public ReadOnly Property TheInterfaceProgress() As InterfaceProgressControl
            Get
                If moInterfaceProgressControl Is Nothing Then
                    moInterfaceProgressControl = CType(FindControl("moInterfaceProgressControl"), InterfaceProgressControl)
                End If
                Return moInterfaceProgressControl
            End Get
        End Property

        Private _ProgBarBaseController As String

        Public ReadOnly Property ProgressBarBaseController() As String
            Get
                If _ProgBarBaseController = String.Empty Then
                    _ProgBarBaseController = moInterfaceProgressControl.ClientID.Replace("moInterfaceProgressControl", "")
                End If
                Return _ProgBarBaseController
            End Get
        End Property


#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


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

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            'Me.ErrorCtrl.Clear_Hide()
            Me.MasterPage.MessageController.Clear_Hide()
            Try
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                Me.UpdateBreadCrum()
                If Not Me.IsPostBack Then
                    InitializeForm()
                End If
                Me.InstallReportViewer()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            '   InstallInterfaceProgressBar()
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnCopyTable_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyTable_WRITE.Click
            Dim dv As DataView
            Dim params As DownLoadBase.DownLoadParams

            Try
                Me.State.SelectedTableId = Me.GetSelectedItem(moTableDrop)
                dv = LookupListNew.GetVscUploadsLookupList(Authentication.LangId, False)
                Me.State.SelectedTableLayout = LookupListNew.GetCodeFromId(dv, Me.State.SelectedTableId)
                UploadFile()
                'UploadTable(params)
                DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                ' ExecuteAndWait()
                'ExecuteSp(params)
                '  DownLoadDs(params)

            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub uploadFile()
            Dim fileLines As New Collections.Generic.List(Of String)

            Using sr As New StreamReader(tableInput.PostedFile.InputStream, Encoding.Default)
                Dim line As String = sr.ReadLine()
                While Not line Is Nothing
                    If line.Trim <> String.Empty Then
                        fileLines.Add(line.Trim)
                    End If
                    line = sr.ReadLine()
                End While
            End Using

            If fileLines.Count > 0 Then
                Dim strUploadType As String, strErrMsg As String, strResult As String
                strUploadType = moTableDrop.SelectedItem.Text
                If strUploadType = "Coverage Rate" Then
                    strUploadType = "VSCCOVRATE"
                    Me.State.UploadType = strUploadType
                End If

                strResult = commonUpload.InitUpload(System.IO.Path.GetFileName(tableInput.PostedFile.FileName), strUploadType, strErrMsg)
                If strResult = "S" Then
                    commonUpload.DumpFileToTableNew(strUploadType, fileLines, System.IO.Path.GetFileName(tableInput.PostedFile.FileName))
                    'Process the file async
                    Dim intStatusID As Guid
                    Dim params As Interfaces.InterfaceBaseForm.Params

                    Try
                        intStatusID = InterfaceStatusWrk.CreateInterfaceStatus("Upload")
                        commonUpload.ProcessUploadedFile(strUploadType, intStatusID, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code, ElitaPlusIdentity.Current.EmailAddress, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                        params = SetParameters(intStatusID, ProgressBarBaseController)
                        Session(Interfaces.InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                        'TheInterfaceProgress.EnableInterfaceProgress(ProgressBarBaseController)
                        afterUpload()
                    Catch ex As Threading.ThreadAbortException
                    Catch ex As Exception
                        HandleErrors(ex, Me.MasterPage.MessageController)
                    End Try
                ElseIf strResult = "F" Then   'display the error message
                    Dim ErrList() As String = {"UPLOAD_FILE_PROGRESS"}
                    MasterPage.MessageController.AddErrorAndShow(ErrList, True)
                Else
                    Dim ErrList() As String = {"UPLOAD_FILE_INVALID_FORMAT"}
                    MasterPage.MessageController.AddErrorAndShow(ErrList, True)
                End If
            Else
                Dim ErrList() As String = {"DEALERLOADFORM_FORM001"}
                MasterPage.MessageController.AddErrorAndShow(ErrList, True)
            End If
        End Sub

        Private Sub afterUpload()
            State.searchDV = Nothing
            PopulateGrid()
            'panelResult.Visible = True
            Me.MasterPage.MessageController.AddInformation(Message.MSG_INTERFACES_HAS_COMPLETED)
        End Sub

        Private Sub PopulateGrid()
            If State.searchDV Is Nothing Then
                State.searchDV = commonUpload.GetProcessingError(State.UploadType)
            End If
            'SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Nothing, Me.Grid, Me.State.PageIndex, False)
            Grid.DataSource = State.searchDV
            Grid.DataBind()
        End Sub


#End Region

#Region "Handlers-Progress Buttons"

        'Private Sub btnAfterProgressBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAfterProgressBar.Click
        '    AfterProgressBar()
        'End Sub

#End Region

#End Region

#Region "Progress Bar"

        Public Sub InstallInterfaceProgressBar()
            Me.InstallDisplayProgressBar()
        End Sub

        Private Sub ExecuteAndWait()
            'Dim intStatus As InterfaceStatusWrk
            'Dim params As InterfaceBaseForm.Params

            'Try
            '    ExecuteSp()

            '    params = SetParameters(Me.State.intStatusId)
            '    Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
            '    TheInterfaceProgress.EnableInterfaceProgress()
            'Catch ex As Threading.ThreadAbortException
            'Catch ex As Exception
            '    Me.HandleErrors(ex, Me.ErrorCtrl)
            'End Try
        End Sub

        Function SetParameters(ByVal intStatusId As Guid, ByVal baseController As String) As InterfaceBaseForm.Params
            Dim params As New InterfaceBaseForm.Params

            With params
                .intStatusId = intStatusId
                .baseController = baseController
            End With
            Return params
        End Function

        'Private Sub AfterProgressBar()
        '    '  ClearSelectedClaimFile(POPULATE_ACTION_SAVE)
        '    Me.DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
        'End Sub

#End Region

#Region "Populate"

        Sub PopulateDropDown()
            Try

                'Me.BindListControlToDataView(moTableDrop, LookupListNew.GetVscUploadsLookupList(Authentication.LangId, False), , , False) 'VSCU
                moTableDrop.Populate(CommonConfigManager.Current.ListManager.GetList("VSCU", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
                '  BindSelectItem(Me.State.SelectedSplitId.ToString, ddSplit)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub InitializeForm()
            PopulateDropDown()
        End Sub

#End Region

#Region "Controlling Logic"

        Private Sub ExecuteSp(ByRef params As DownLoadBase.DownLoadParams)
            '  Dim ds As DataSet
            Dim oVscTableProcessedData As New VscTableProcessedData

            With oVscTableProcessedData
                '   .allDs = Me.State.allDs
                .layout = Me.State.SelectedTableLayout
                ' .filename = Me.State.FileName
            End With

            VscTableProcessed.ProcessFileRecords(oVscTableProcessedData)
            '  params.data = ds

            '  Me.State.intStatusId = oVscTableProcessedData.interfaceStatus_id
        End Sub

        
        'Private Sub ExecuteSp()
        '    Dim oVscTableProcessedData As New VscTableProcessedData

        '    With oVscTableProcessedData
        '        .filename = Me.State.FileName
        '        .layout = Me.State.SelectedTableLayout
        '    End With

        '    VscTableProcessed.ProcessFileRecords(oVscTableProcessedData)

        '    Me.State.intStatusId = oVscTableProcessedData.interfaceStatus_id
        'End Sub


#Region "Testing"


        'Public Sub ShowCompileError(ByVal sender As Object, ByVal e As System.Xml.Schema.ValidationEventArgs)
        '    Console.WriteLine("Validation Error: {0}", e.Message)
        'End Sub


        'Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='ISO-8859-1' ?>"

        ''Display the validation error.
        'Private Sub ValidationEventHandle(ByVal sender As Object, ByVal args As System.Xml.Schema.ValidationEventArgs)
        '    Dim m_success As Boolean = False
        '    Console.WriteLine("Validation error: " & args.Message)
        'End Sub 'ValidationEventHandle


        'Public Function ValidateXML2(ByRef xml As String, ByVal schema As String) As Boolean

        '    ' Verify the schema
        '    Dim xsd As System.Xml.Schema.XmlSchema = System.Xml.Schema.XmlSchema.Read(XMLHelper.GetXMLStream(schema), Nothing)
        '    '    xsd.Compile(Nothing)
        '    Dim e As System.Xml.Schema.ValidationEventArgs
        '    xsd.Compile(AddressOf ShowCompileError)

        '    If Not xml.StartsWith("<?xml") Then xml = XML_VERSION_AND_ENCODING & xml

        '    ' Verify the xml
        '    Dim xmlStream As XmlTextReader = XMLHelper.GetXMLStream(xml)
        '    If Not Microsoft.VisualBasic.Information.IsNothing(xmlStream) Then

        '        Dim validator As XmlValidatingReader = New XmlValidatingReader(xmlStream)

        '        ' Set the validation event handler
        '        AddHandler validator.ValidationEventHandler, AddressOf Me.ValidationEventHandle

        '        validator.ValidationType = ValidationType.Schema
        '        validator.Schemas.Add(xsd)

        '        ' Go through the whole xml
        '        While (validator.Read())
        '            Select Case validator.NodeType
        '                Case XmlNodeType.Element
        '                    Console.Write("<{0}>", validator.Name)
        '                Case XmlNodeType.Text
        '                    Console.Write(validator.Value)
        '                Case XmlNodeType.CDATA
        '                    Console.Write("<![CDATA[{0}]]>", validator.Value)
        '                Case XmlNodeType.ProcessingInstruction
        '                    Console.Write("<?{0} {1}?>", validator.Name, validator.Value)
        '                Case XmlNodeType.Comment
        '                    Console.Write("<!--{0}-->", validator.Value)
        '                Case XmlNodeType.XmlDeclaration
        '                    Console.Write("<?xml version='1.0'?>")
        '                Case XmlNodeType.Document
        '                Case XmlNodeType.DocumentType
        '                    Console.Write("<!DOCTYPE {0} [{1}]", validator.Name, validator.Value)
        '                Case XmlNodeType.EntityReference
        '                    Console.Write(validator.Name)
        '                Case XmlNodeType.EndElement
        '                    Console.Write("</{0}>", validator.Name)
        '            End Select
        '        End While
        '        'Close the reader.
        '        If Not (validator Is Nothing) Then
        '            validator.Close()
        '        End If
        '    Else

        '        Return False

        '    End If




        '    Return True

        'End Function

        'Public Sub ValidateXML(ByRef ds As DataSet, ByVal schemaPath As String)

        '    Try

        '        '_xsd = XMLHelper.ReadXML(Server.MapPath(schemaPath))
        '        'If Not XMLHelper.ValidateXML(_xml, _xsd) Then Throw New ElitaWSException(ErrorCodes.WS_XML_INVALID)
        '        ds.ReadXml(XMLHelper.GetXMLStream(_xml))

        '    Catch ex As Exception

        '        Throw New ElitaWSException(ErrorCodes.WS_XML_INVALID, ex)

        '    End Try

        'End Sub

        'Private Sub UploadTable()
        '    Dim tableFileName As String
        '    Dim xlsFullPath, csvFullPath As String
        '    Dim filename As String

        '    ' Obtain Unique file names
        '    filename = System.IO.Path.GetFileNameWithoutExtension(tableInput.PostedFile.FileName)
        '    If filename = String.Empty Then
        '        Throw New GUIException("You must select a file name", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_MUST_BE_SELECTED_ERR)
        '    End If
        '    xlsFullPath = MiscUtil.GetUniqueFullPath(AppConfig.UnixServer.InterfaceDirectory, _
        '                            ElitaPlusPrincipal.Current.Identity.Name, _
        '                            filename & ".xls")
        '    csvFullPath = MiscUtil.GetUniqueFullPath(AppConfig.UnixServer.InterfaceDirectory, _
        '                            ElitaPlusPrincipal.Current.Identity.Name, _
        '                            filename & ".csv")
        '    tableFileName = System.IO.Path.GetFileName(tableInput.PostedFile.FileName)

        '    ' Save the File with Excel Format
        '    tableInput.PostedFile.SaveAs(xlsFullPath)

        '    ' Andres Testing
        '    Dim xml As String
        '    Dim ds As InterfaceModelDs
        '    Try


        '        Dim schemaName As String = "InterfaceModel.xsd"
        '        xml = XMLHelper.FromExcelToXml(xlsFullPath)
        '        Dim xsd As String = XMLHelper.ReadXML(Server.MapPath(ELPWebConstants.SETTING_SCHEMA_PATH & schemaName))
        '        '   If Not XMLHelper.ValidateXML(xml, xsd) Then Throw New Exception("hello")
        '        If Not ValidateXML2(xml, xsd) Then
        '            Throw New Exception("hello")
        '        End If
        '        '   Dim inputDS As Object = InterfaceModelDs
        '        ' Dim ds As DataSet = New InterfaceModelDs
        '        ds = New InterfaceModelDs
        '        ds.ReadXml(XMLHelper.GetXMLStream(xml))

        '    Catch ex As Exception

        '    End Try
        '    Exit Sub
        '    ' Save the File with CSV Format
        '    MiscUtil.FromExcelToCsv(xlsFullPath, csvFullPath)
        '    ' Send the CSV file to the Unix Server
        '    MiscUtil.SendFtp(csvFullPath, tableFileName, Me.State.SelectedTableLayout)
        '    Me.State.FileName = System.IO.Path.GetFileName(csvFullPath)

        'End Sub

        ' For CSV
        'Private Sub UploadTable()
        '    Dim tableFileName As String
        '    Dim xlsFullPath, csvFullPath As String
        '    Dim filename As String

        '    ' Obtain Unique file names
        '    filename = System.IO.Path.GetFileNameWithoutExtension(tableInput.PostedFile.FileName)
        '    If filename = String.Empty Then
        '        Throw New GUIException("You must select a file name", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_MUST_BE_SELECTED_ERR)
        '    End If
        '    xlsFullPath = MiscUtil.GetUniqueFullPath(AppConfig.UnixServer.InterfaceDirectory, _
        '                            ElitaPlusPrincipal.Current.Identity.Name, _
        '                            filename & ".xls")
        '    csvFullPath = MiscUtil.GetUniqueFullPath(AppConfig.UnixServer.InterfaceDirectory, _
        '                            ElitaPlusPrincipal.Current.Identity.Name, _
        '                            filename & ".csv")
        '    tableFileName = System.IO.Path.GetFileName(tableInput.PostedFile.FileName)

        '    ' Save the File with Excel Format
        '    tableInput.PostedFile.SaveAs(xlsFullPath)

        '    ' Andres Testing
        '    Dim xml As String
        '    Dim ds As InterfaceModelDs
        '    Try
        '        Dim schemaName As String = "InterfaceModel.xsd"
        '        xml = XMLHelper.FromExcelToXml(xlsFullPath)
        '    Catch ex As Exception

        '    End Try
        '    Exit Sub
        '    ' Save the File with CSV Format
        '    MiscUtil.FromExcelToCsv(xlsFullPath, csvFullPath)
        '    ' Send the CSV file to the Unix Server
        '    MiscUtil.SendFtp(csvFullPath, tableFileName, Me.State.SelectedTableLayout)
        '    Me.State.FileName = System.IO.Path.GetFileName(csvFullPath)

        'End Sub
#End Region

        'Private Sub UploadTable(ByRef params As DownLoadBase.DownLoadParams)
        '    Dim xlsFullPath, xmlFullPath As String
        '    Dim filename As String
        '    '  Dim xml As String

        '    ' Obtain Unique file names
        '    filename = System.IO.Path.GetFileNameWithoutExtension(MiscUtil.ReplaceSpaceByUnderscore(tableInput.PostedFile.FileName))
        '    If filename = String.Empty Then
        '        Throw New GUIException("You must select a file name", _
        '                        Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_MUST_BE_SELECTED_ERR)
        '    End If
        '    params.fileName = filename & "_Result.xls"
        '    xlsFullPath = MiscUtil.GetUniqueFullPath(AppConfig.UnixServer.InterfaceDirectory, _
        '                            ElitaPlusPrincipal.Current.Identity.Name, _
        '                            filename & ".xls")

        '    ' Save the File with Excel Format
        '    tableInput.PostedFile.SaveAs(xlsFullPath)
        '    ' Obtains Xml from Excel
        '    Me.State.xmlData = XMLHelper.FromExcelToXml(xlsFullPath, Me.State.SelectedTableLayout)
        '    xmlFullPath = MiscUtil.GetUniqueFullPath(String.Empty, _
        '                            ElitaPlusPrincipal.Current.Identity.Name, _
        '                            filename & ".xml")
        '    Dim oStream As Stream = MiscUtil.FromStringToStream(Me.State.xmlData)
        '    ' Me.State.allDs = XMLHelper.FromExcelToDataset(xlsFullPath, Me.State.SelectedTableLayout)
        '    '    ' Send the CSV file to the Unix Server
        '    '  MiscUtil.SendFtp(csvFullPath, tableFileName, Me.State.SelectedTableLayout)
        '    MiscUtil.SendFileToUnix(oStream, xmlFullPath)
        '    Me.State.FileName = xmlFullPath
        'End Sub

        Private Sub UploadTable(ByRef params As DownLoadBase.DownLoadParams)
            Dim webServerPath, webServerFullPathFile, xmlFullPath As String
            Dim filename As String
            Dim fileLen As Integer = tableInput.PostedFile.ContentLength
            Dim objStream As System.IO.Stream = tableInput.PostedFile.InputStream
            
            ' Obtain Unique file names
            'filename = System.IO.Path.GetFileNameWithoutExtension(MiscUtil.ReplaceSpaceByUnderscore(tableInput.PostedFile.FileName))
            'filename = System.IO.Path.GetFileName(MiscUtil.ReplaceSpaceByUnderscore(tableInput.PostedFile.FileName))
            filename = tableInput.PostedFile.FileName
            If ((filename = String.Empty) OrElse (System.IO.Path.GetExtension(filename) <> ".csv")) Then
                Throw New GUIException("You must select a file name with csv extension", _
                                Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_MUST_BE_SELECTED_ERR)
            End If
            webServerPath = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, _
                                                            ElitaPlusPrincipal.Current.Identity.Name)
            'params.fileName = MiscUtil.GetUniqueFullPath(String.Empty, _
            '                        ElitaPlusPrincipal.Current.Identity.Name, _
            '                        filename & ".csv")
            params.layout = Me.State.SelectedTableLayout
            params.fileName = "VSC_" & params.layout & "_EXT.csv"
            'webServerFullPathFile = AppConfig.UnixServer.InterfaceDirectory & params.fileName
            webServerFullPathFile = webServerPath & "\" & params.fileName


            'MiscUtil.DownloadFileToWebServer(AppConfig.UnixServer.InterfaceDirectory, _
            '                                 webServerFullPathFile, fileLen, objStream)
            MiscUtil.DownloadFileToWebServer(webServerPath, _
                                             webServerFullPathFile, fileLen, objStream)
            MiscUtil.SendFileToUnix(webServerFullPathFile)
        End Sub

        'Private Sub DownLoadDs(ByRef params As DownLoadBase.DownLoadParams)
        '    Dim sJavaScript As String

        '    params.downLoadCode = DownLoadBase.DownLoadParams.DownLoadTypeCode.GRID

        '    Session(DownLoadBase.SESSION_PARAMETERS_DOWNLOAD_KEY) = params
        '    sJavaScript = "<SCRIPT>" & Environment.NewLine
        '    sJavaScript &= "showReportViewerFrame('../Common/DownLoadWindowForm.aspx'); " & Environment.NewLine
        '    sJavaScript &= "</SCRIPT>" & Environment.NewLine
        '    RegisterStartupScript("EnableReportCe", sJavaScript)
        'End Sub
#End Region



    End Class

End Namespace
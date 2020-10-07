Imports System.IO
Imports System.Text


Public Class ReactivateUploadForm
    Inherits ElitaPlusSearchPage
#Region "Constants"
    Public Const URL As String = "Interfaces/ReactivateUploadForm.aspx"
    'Public Const PAGETITLE As String = "REACTIVATEUPLOADTYPE"
    Public Const PAGETITLE As String = "UPLOAD_TYPE"
    Public Const FORMCODE As String = "REACTIVATEUPLOADFORM"
    Public Const PAGETAB As String = "INTERFACES"
#End Region

#Region "properties"
    Private _ProgBarBaseController As String
    Public ReadOnly Property TheInterfaceProgress() As Interfaces.InterfaceProgressControl
        Get
            If moInterfaceProgressControl Is Nothing Then
                moInterfaceProgressControl = CType(FindControl("moInterfaceProgressControl"), Interfaces.InterfaceProgressControl)
            End If
            Return moInterfaceProgressControl
        End Get
    End Property

    Public ReadOnly Property ProgressBarBaseController() As String
        Get
            If _ProgBarBaseController = String.Empty Then
                _ProgBarBaseController = moInterfaceProgressControl.ClientID.Replace("moInterfaceProgressControl", "")
            End If
            Return _ProgBarBaseController
        End Get
    End Property
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public UploadType As String = String.Empty
        Public extractFilename As String = String.Empty
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

#Region "Page Event"
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear_Hide()
        Try
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            UpdateBreadCrum()
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                PopulateDropdown()
            End If
            DisplayNewProgressBarOnClick(btnLoadFile_WRITE, "LOADING")
            panelIntProgControl.Visible = False

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        InstallDisplayProgressBar()
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Helper function"
    Private Sub uploadFile()
        Dim fileLines As New Collections.Generic.List(Of String)

        Using sr As New StreamReader(InputFile.PostedFile.InputStream, Encoding.Default)
            Dim line As String = sr.ReadLine()
            While line IsNot Nothing
                If line.Trim <> String.Empty Then
                    fileLines.Add(line.Trim)
                End If
                line = sr.ReadLine()
            End While
        End Using

        If fileLines.Count > 0 Then
            Dim strUploadType As String, strErrMsg As String, strResult As String
            strUploadType = ddlUploadType.SelectedValue.Trim
            State.UploadType = strUploadType
            strResult = commonUpload.InitUpload(System.IO.Path.GetFileName(InputFile.PostedFile.FileName), strUploadType, strErrMsg)
            If strResult = "S" Then
                commonUpload.DumpFileToTableNew(strUploadType, fileLines, System.IO.Path.GetFileName(InputFile.PostedFile.FileName))
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
                    HandleErrors(ex, MasterPage.MessageController)
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

    Function SetParameters(intStatusId As Guid, baseController As String) As Interfaces.InterfaceBaseForm.Params
        Dim params As New Interfaces.InterfaceBaseForm.Params
        With params
            .intStatusId = intStatusId
            .baseController = baseController
        End With
        Return params
    End Function

    Private Sub PopulateGrid()
        If State.searchDV Is Nothing Then
            State.searchDV = commonUpload.GetProcessingError(State.UploadType)
        End If
        SetPageAndSelectedIndexFromGuid(State.searchDV, Nothing, Grid, State.PageIndex, False)
        Grid.DataSource = State.searchDV
        Grid.DataBind()
    End Sub

    Private Sub PopulateDropdown()
        Dim Data As DataView = LookupListNew.DropdownLookupList("ADJUST", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        Dim i As Integer
        ddlUploadType.Items.Clear()
        ddlUploadType.Items.Add(New ListItem("", ""))
        For i = 0 To Data.Count - 1
            ddlUploadType.Items.Add(New ListItem(Data(i)("DESCRIPTION").ToString, Data(i)("CODE").ToString))
        Next
    End Sub
    Private Sub afterUpload()
        State.searchDV = Nothing
        PopulateGrid()
        panelResult.Visible = True
        MasterPage.MessageController.AddInformation(Message.MSG_INTERFACES_HAS_COMPLETED)
    End Sub
#End Region

#Region "control event handler"

    Private Sub btnLoadFile_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnLoadFile_WRITE.Click
        Dim ErrList As New Collections.Generic.List(Of String)

        panelResult.Visible = False
        lblHelpText.Visible = False
        If ddlUploadType.SelectedValue <> "CLAIMUPDATE" Then
            btnExtract_Report.Visible = True
        End If
        State.extractFilename = InputFile.PostedFile.FileName.Trim
        Try
            If InputFile.PostedFile.FileName.Trim = String.Empty Then
                ErrList.Add("FILE_NAME_IS_REQUIRED")
            End If

            If (System.IO.Path.GetFileName(InputFile.PostedFile.FileName.ToString()).Length() > 31) Then
                ErrList.Add("FILE NAME CAN NOT BE LONGER THAN 30 CHARACTERS")
            End If

            If ddlUploadType.SelectedValue.Trim = String.Empty Then
                ErrList.Add("UPLOAD_TYPE_IS_REQUIRED")
            End If

            If ErrList.Count > 0 Then
                MasterPage.MessageController.AddErrorAndShow(ErrList.ToArray, True)
            Else
                uploadFile()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#Region "Grid Related"
    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnExtract_Report_Click(sender As Object, e As EventArgs) Handles btnExtract_Report.Click
        Dim strUploadType As String
        Dim extractFile As String
        strUploadType = ddlUploadType.SelectedValue.Trim
        extractFile = State.extractFilename

        Try
            Dim strEmailAddress As String = ElitaPlusIdentity.Current.EmailAddress
            Dim strCompanyGroupCode As String = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code

            If String.IsNullOrEmpty(strEmailAddress) Then
                DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
            Else
                commonUpload.ExtractReport(strUploadType, strEmailAddress, strCompanyGroupCode, extractFile)
                DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        lblHelpText.Text = TranslationBase.TranslateLabelOrMessage(commonUpload.getScreenHelp(FORMCODE))
        lblHelpText.Visible = True
    End Sub
#End Region

#End Region

End Class
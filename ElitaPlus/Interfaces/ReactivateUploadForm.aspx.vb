Imports System.IO
Imports System.Text


Public Class ReactivateUploadForm
    Inherits ElitaPlusSearchPage
#Region "Constants"
    Public Const URL As String = "Interfaces/ReactivateUploadForm.aspx"
    'Public Const PAGETITLE As String = "REACTIVATEUPLOADTYPE"
    Public Const PAGETITLE As String = "UPLOADTYPE"
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
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear_Hide()
        Try
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.UpdateBreadCrum()
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                PopulateDropdown()
            End If
            Me.DisplayNewProgressBarOnClick(Me.btnLoadFile_WRITE, "LOADING")
            Me.panelIntProgControl.Visible = False

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        InstallDisplayProgressBar()
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Helper function"
    Private Sub uploadFile()
        Dim fileLines As New Collections.Generic.List(Of String)

        Using sr As New StreamReader(InputFile.PostedFile.InputStream, Encoding.Default)
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

    Function SetParameters(ByVal intStatusId As Guid, ByVal baseController As String) As Interfaces.InterfaceBaseForm.Params
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
        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Nothing, Me.Grid, Me.State.PageIndex, False)
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
        Me.MasterPage.MessageController.AddInformation(Message.MSG_INTERFACES_HAS_COMPLETED)
    End Sub
#End Region

#Region "control event handler"

    Private Sub btnLoadFile_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadFile_WRITE.Click
        Dim ErrList As New Collections.Generic.List(Of String)

        panelResult.Visible = False
        btnExtract_Report.Visible = True

        Try
            If InputFile.PostedFile.FileName.Trim = String.Empty Then
                ErrList.Add("FILE_NAME_IS_REQUIRED")
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
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#Region "Grid Related"
    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnExtract_Report_Click(sender As Object, e As EventArgs) Handles btnExtract_Report.Click

        Dim strUploadType As String
        strUploadType = ddlUploadType.SelectedValue.Trim

        Try
            Dim strEmailAddress As String = ElitaPlusIdentity.Current.EmailAddress
            Dim strCompanyGroupCode As String = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code

            If String.IsNullOrEmpty(strEmailAddress) Then
                Me.DisplayMessage(Message.MSG_Email_not_configured, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
            Else
                commonUpload.ExtractReport(strUploadType, strEmailAddress, strCompanyGroupCode)
                Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
            End If


        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region


#End Region

End Class
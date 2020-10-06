Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Security

    Partial Public Class FtpSiteForm
        Inherits ElitaPlusPage

#Region "Constants"
        Public Shared URL As String = "~/Security/FtpSiteForm.aspx"
        Public Const PAGETITLE As String = "FTP_SITE"
        Public Const PAGETAB As String = "ADMIN"

        ' Property Name
        Public Const CODE_PROPERTY As String = "Code"
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const HOST_PROPERTY As String = "Host"
        Public Const PORT_PROPERTY As String = "Port"
        Public Const USERNAME_PROPERTY As String = "UserName"
        Public Const PASSWORD_PROPERTY As String = "Password"
        Public Const ACCOUNT_PROPERTY As String = "Account"
        Public Const DIRECTORY_PROPERTY As String = "Directory"
#End Region

#Region "Page State"

#Region "MyState"

        Class MyState
            Public MyBo As FtpSite
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public HasDataChanged As Boolean = False
            Public userName, password As String
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBo = New FtpSite(CType(CallingParameters, Guid))
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Handlers"

#Region "Handler-Init"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()
                ClearLabelsErrSign()
                RecoverEncryptedValue()
                If Not Page.IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, _
                                                                        MSG_TYPE_CONFIRM, True)
                    If State.MyBo Is Nothing Then
                        State.MyBo = New FtpSite
                    End If
                    PopulateAll()
                    EnableDisableFields()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New FtpSiteListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                            State.MyBo.Id, State.HasDataChanged)
            ReturnToCallingPage(retType)
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBo.IsDirty = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, _
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnApply_WRITE_Click(sender As Object, e As EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Protected Sub btnUndo_WRITE_Click(sender As Object, e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNew()
            State.MyBo = New FtpSite
            ClearAll()
            PopulateAll()
            EnableDisableFields()
        End Sub

        Protected Sub btnNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBo.IsDirty = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            State.MyBo = New FtpSite
            EnableDisableFields()
        End Sub

        Protected Sub btnCopy_WRITE_Click(sender As Object, e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If State.MyBo.IsDirty = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnDelete_WRITE_Click(sender As Object, e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteFtpSite() = True Then
                    State.HasDataChanged = True
                    Dim retType As New FtpSiteListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                                    Guid.Empty)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBo, CODE_PROPERTY, moCodeLabel)
            BindBOPropertyToLabel(State.MyBo, DESCRIPTION_PROPERTY, moDescriptionLabel)
            BindBOPropertyToLabel(State.MyBo, HOST_PROPERTY, moHostLabel)
            BindBOPropertyToLabel(State.MyBo, PORT_PROPERTY, moPortLabel)
            BindBOPropertyToLabel(State.MyBo, USERNAME_PROPERTY, moUsernameLabel)
            BindBOPropertyToLabel(State.MyBo, PASSWORD_PROPERTY, moPasswordLabel)
            BindBOPropertyToLabel(State.MyBo, ACCOUNT_PROPERTY, moAccountLabel)
            BindBOPropertyToLabel(State.MyBo, DIRECTORY_PROPERTY, moDirectoryLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(moCodeLabel)
            ClearLabelErrSign(moDescriptionLabel)
            ClearLabelErrSign(moHostLabel)
            ClearLabelErrSign(moPortLabel)
            ClearLabelErrSign(moUsernameLabel)
            ClearLabelErrSign(moPasswordLabel)
            ClearLabelErrSign(moAccountLabel)
            ClearLabelErrSign(moDirectoryLabel)
        End Sub
#End Region

#End Region

#Region "Enable-Disable"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, moCodeText, bIsNew)
            ControlMgr.SetEnableControl(Me, moDescriptionText, bIsNew)
        End Sub

        Protected Sub EnableDisableFields()
            SetButtonsState(State.MyBo.IsNew)
        End Sub

        Public Sub RecoverEncryptedValue()
            moUsernameText.Attributes.Add("value", State.userName)
            moPasswordText.Attributes.Add("value", State.password)
        End Sub
#End Region

#Region "Clear"

        Private Sub ClearAll()
            moCodeText.Text = String.Empty
            moDescriptionText.Text = String.Empty
            moHostText.Text = String.Empty
            moPortText.Text = String.Empty
            moUsernameText.Text = String.Empty
            '  Me.State.userName = moUsernameText.Text
            moPasswordText.Text = String.Empty
            ' Me.State.password = moPasswordText.Text
            moAccountText.Text = String.Empty
            moDirectoryText.Text = String.Empty
        End Sub

#End Region

#Region "Populate"


        Protected Sub PopulateFormFromBOs()
            With State.MyBo
                moCodeText.Text = .Code
                moDescriptionText.Text = .Description
                moHostText.Text = .Host
                If (.Port Is Nothing) Then
                    moPortText.Text = String.Empty
                Else
                    moPortText.Text = .Port.ToString
                End If
                'Me.moUsernameText.Text = .UserName
                moUsernameText.Attributes.Add("value", .UserName)
                State.userName = .UserName
                'Me.moPasswordText.Text = .Password
                moPasswordText.Attributes.Add("value", .Password)
                State.password = .Password
                moAccountText.Text = .Account
                moDirectoryText.Text = .Directory
            End With
        End Sub

        Protected Sub PopulateBOsFromForm()
            PopulateBOProperty(State.MyBo, CODE_PROPERTY, moCodeText)
            PopulateBOProperty(State.MyBo, DESCRIPTION_PROPERTY, moDescriptionText)
            PopulateBOProperty(State.MyBo, HOST_PROPERTY, moHostText)
            PopulateBOProperty(State.MyBo, PORT_PROPERTY, moPortText)
            PopulateBOProperty(State.MyBo, USERNAME_PROPERTY, moUsernameText)
            PopulateBOProperty(State.MyBo, PASSWORD_PROPERTY, moPasswordText)
            PopulateBOProperty(State.MyBo, ACCOUNT_PROPERTY, moAccountText)
            PopulateBOProperty(State.MyBo, DIRECTORY_PROPERTY, moDirectoryText)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateAll()
            ClearAll()
            PopulateFormFromBOs()
        End Sub

#End Region

#Region "Business Part"

        Private Function ApplyChanges() As Boolean
            Dim isOK As Boolean = True
            Try
                PopulateBOsFromForm()
                If State.MyBo.IsDirty Then
                    State.MyBo.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                isOK = False
            End Try
            Return isOK
        End Function

        Private Function DeleteFtpSite() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With State.MyBo
                    .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                    .EndEdit()
                End With
            Catch ex As Exception
                State.MyBo.RejectChanges()
                ' Me.State.MyBo.EndEdit()
                HandleErrors(ex, ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "State Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            State.HasDataChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNew()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            State.HasDataChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        CreateNew()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            State.HasDataChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region


    End Class

End Namespace
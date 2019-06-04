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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBo = New FtpSite(CType(Me.CallingParameters, Guid))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Handlers"

#Region "Handler-Init"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()
                ClearLabelsErrSign()
                RecoverEncryptedValue()
                If Not Page.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
                                                                        Me.MSG_TYPE_CONFIRM, True)
                    If Me.State.MyBo Is Nothing Then
                        Me.State.MyBo = New FtpSite
                    End If
                    PopulateAll()
                    EnableDisableFields()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try

            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New FtpSiteListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                            Me.State.MyBo.Id, Me.State.HasDataChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBo.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, _
                                                Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Protected Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.MyBo = New FtpSite
            ClearAll()
            Me.PopulateAll()
            EnableDisableFields()
        End Sub

        Protected Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBo.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Me.State.MyBo = New FtpSite
            EnableDisableFields()
        End Sub

        Protected Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If Me.State.MyBo.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteFtpSite() = True Then
                    Me.State.HasDataChanged = True
                    Dim retType As New FtpSiteListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                                    Guid.Empty)
                    retType.BoChanged = True
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBo, CODE_PROPERTY, Me.moCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, DESCRIPTION_PROPERTY, Me.moDescriptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, HOST_PROPERTY, Me.moHostLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, PORT_PROPERTY, Me.moPortLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, USERNAME_PROPERTY, Me.moUsernameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, PASSWORD_PROPERTY, Me.moPasswordLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, ACCOUNT_PROPERTY, Me.moAccountLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, DIRECTORY_PROPERTY, Me.moDirectoryLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(Me.moCodeLabel)
            Me.ClearLabelErrSign(Me.moDescriptionLabel)
            Me.ClearLabelErrSign(Me.moHostLabel)
            Me.ClearLabelErrSign(Me.moPortLabel)
            Me.ClearLabelErrSign(Me.moUsernameLabel)
            Me.ClearLabelErrSign(Me.moPasswordLabel)
            Me.ClearLabelErrSign(Me.moAccountLabel)
            Me.ClearLabelErrSign(Me.moDirectoryLabel)
        End Sub
#End Region

#End Region

#Region "Enable-Disable"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, moCodeText, bIsNew)
            ControlMgr.SetEnableControl(Me, moDescriptionText, bIsNew)
        End Sub

        Protected Sub EnableDisableFields()
            SetButtonsState(Me.State.MyBo.IsNew)
        End Sub

        Public Sub RecoverEncryptedValue()
            Me.moUsernameText.Attributes.Add("value", Me.State.userName)
            Me.moPasswordText.Attributes.Add("value", Me.State.password)
        End Sub
#End Region

#Region "Clear"

        Private Sub ClearAll()
            Me.moCodeText.Text = String.Empty
            Me.moDescriptionText.Text = String.Empty
            Me.moHostText.Text = String.Empty
            Me.moPortText.Text = String.Empty
            Me.moUsernameText.Text = String.Empty
            '  Me.State.userName = moUsernameText.Text
            Me.moPasswordText.Text = String.Empty
            ' Me.State.password = moPasswordText.Text
            Me.moAccountText.Text = String.Empty
            Me.moDirectoryText.Text = String.Empty
        End Sub

#End Region

#Region "Populate"


        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBo
                Me.moCodeText.Text = .Code
                Me.moDescriptionText.Text = .Description
                Me.moHostText.Text = .Host
                If (.Port Is Nothing) Then
                    Me.moPortText.Text = String.Empty
                Else
                    Me.moPortText.Text = .Port.ToString
                End If
                'Me.moUsernameText.Text = .UserName
                Me.moUsernameText.Attributes.Add("value", .UserName)
                Me.State.userName = .UserName
                'Me.moPasswordText.Text = .Password
                Me.moPasswordText.Attributes.Add("value", .Password)
                Me.State.password = .Password
                Me.moAccountText.Text = .Account
                Me.moDirectoryText.Text = .Directory
            End With
        End Sub

        Protected Sub PopulateBOsFromForm()
            Me.PopulateBOProperty(Me.State.MyBo, CODE_PROPERTY, Me.moCodeText)
            Me.PopulateBOProperty(Me.State.MyBo, DESCRIPTION_PROPERTY, Me.moDescriptionText)
            Me.PopulateBOProperty(Me.State.MyBo, HOST_PROPERTY, Me.moHostText)
            Me.PopulateBOProperty(Me.State.MyBo, PORT_PROPERTY, Me.moPortText)
            Me.PopulateBOProperty(Me.State.MyBo, USERNAME_PROPERTY, Me.moUsernameText)
            Me.PopulateBOProperty(Me.State.MyBo, PASSWORD_PROPERTY, Me.moPasswordText)
            Me.PopulateBOProperty(Me.State.MyBo, ACCOUNT_PROPERTY, Me.moAccountText)
            Me.PopulateBOProperty(Me.State.MyBo, DIRECTORY_PROPERTY, Me.moDirectoryText)

            If Me.ErrCollection.Count > 0 Then
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
                Me.PopulateBOsFromForm()
                If Me.State.MyBo.IsDirty Then
                    Me.State.MyBo.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    EnableDisableFields()
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
                isOK = False
            End Try
            Return isOK
        End Function

        Private Function DeleteFtpSite() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With Me.State.MyBo
                    .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                    .EndEdit()
                End With
            Catch ex As Exception
                Me.State.MyBo.RejectChanges()
                ' Me.State.MyBo.EndEdit()
                Me.HandleErrors(ex, ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "State Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.HasDataChanged = True
                            GoBack()
                        End If
                    Case Me.MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.HasDataChanged = True
                            CreateNew()
                        End If
                    Case Me.MSG_VALUE_NO
                        CreateNew()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.HasDataChanged = True
                            CreateNewCopy()
                        End If
                    Case Me.MSG_VALUE_NO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region


    End Class

End Namespace
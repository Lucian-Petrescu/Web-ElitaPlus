Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Tables

    Partial Public Class RegistrationLetterForm
        Inherits ElitaPlusPage

#Region "Page State"

#Region "MyState"

        Class MyState
            Public moRegistrationLetterId As Guid = Guid.Empty
            Public IsRegistrationLetterNew As Boolean = False

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False

            Public dvLetterVariable As DataView

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

        Private Sub SetStateProperties()
            State.moRegistrationLetterId = CType(CallingParameters, Guid)
            'Me.State.dealerTypeVSC = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
            'Me.State.DealerTypeID = GetDealerTypeID()
            If State.moRegistrationLetterId.Equals(Guid.Empty) Then
                State.IsRegistrationLetterNew = True
                ClearAll()
                SetButtonsState(True)
            Else
                State.IsRegistrationLetterNew = False
                SetButtonsState(False)
            End If
            PopulateAll()
        End Sub

#End Region

#Region "Constants"
        Private Const LABEL_DEALER As String = "DEALER"
        Public Const URL As String = "RegistrationLetterForm.aspx"
        Public Const PAGETITLE As String = "REGISTRATION_LETTER"
        Public Const PAGETAB As String = "TABLES"
        ' Property Name
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const LETTER_TYPE_PROPERTY As String = "LetterType"
        Public Const NUMBER_OF_DAYS_PROPERTY As String = "NumberOfDays"
        Public Const EMAIL_SUBJECT_PROPERTY As String = "EmailSubject"
        Public Const EMAIL_TEXT_PROPERTY As String = "EmailText"
        Public Const EMAIL_FROM_PROPERTY As String = "EmailFrom"
        Public Const EMAIL_TO_PROPERTY As String = "EmailTo"
#End Region

#Region "Variables"
        Public emailText As String = ""
#End Region

#Region "Attributes"

        Private moRegistrationLetter As RegistrationLetter

#End Region

#Region "Properties"

        Private ReadOnly Property TheRegistrationLetter() As RegistrationLetter
            Get
                If moRegistrationLetter Is Nothing Then
                    If State.IsRegistrationLetterNew = True Then
                        ' For creating, inserting
                        moRegistrationLetter = New RegistrationLetter
                        State.moRegistrationLetterId = moRegistrationLetter.Id
                    Else
                        ' For updating, deleting
                        moRegistrationLetter = New RegistrationLetter(State.moRegistrationLetterId)
                    End If
                End If

                Return moRegistrationLetter
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDealerDropControl Is Nothing Then
                    multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDealerDropControl
            End Get
        End Property

#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()
                ClearLabelsErrSign()
                TranslateGridHeader(gridVariable)

                If Not Page.IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, _
                                                                        MSG_TYPE_CONFIRM, True)
                    '  EnableDisableFields()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Public Function RenderTextEditor() As String
            Dim textEditorCtlStr As String
            textEditorCtlStr = "<script type=""text/javascript"">"
            textEditorCtlStr = textEditorCtlStr & "var sBasePath = sBasePath = ""../Common/Editor/fckeditor/""; "
            textEditorCtlStr = textEditorCtlStr & "var oFCKeditor = new FCKeditor( 'EmailText' ); "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.BasePath	= sBasePath; "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.Height	= 300; "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.Value	= '" & emailText.Replace(Environment.NewLine, "") & "'; "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.Create(); "
            textEditorCtlStr = textEditorCtlStr & "</script>"

            Return textEditorCtlStr
        End Function

        Public Function GetOnClickScript(strCode As String) As String
            Return "return CopyToClipboard(""" & strCode & """);"
        End Function

        Public Function TranlateLabel(strLabel As String) As String
            If strLabel.Trim <> String.Empty Then
                Return TranslationBase.TranslateLabelOrMessage(strLabel)
            Else
                Return String.Empty
            End If
        End Function
#End Region

#Region "Handlers-Buttons"

        Protected Sub btnApply_WRITE_Click(sender As Object, e As EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New RegistrationLetterSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                                                State.moRegistrationLetterId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
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

        Protected Sub btnUndo_WRITE_Click(sender As Object, e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNew()
            State.moRegistrationLetterId = Guid.Empty
            TheRegistrationLetter.AttachmentFileName = ""
            State.IsRegistrationLetterNew = True
            ClearAll()
            SetButtonsState(True)
            PopulateAll()
            TheDealerControl.ChangeEnabledControlProperty(True)

        End Sub

        Protected Sub btnNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
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
            State.moRegistrationLetterId = Guid.Empty
            TheRegistrationLetter.AttachmentFileName = ""
            txtAttachment.Text = ""
            State.IsRegistrationLetterNew = True

            SetButtonsState(True)
            TheDealerControl.ChangeEnabledControlProperty(True)

        End Sub

        Protected Sub btnCopy_WRITE_Click(sender As Object, e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
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
                If DeleteRegistrationLetter() = True Then
                    State.boChanged = True
                    Dim retType As New RegistrationLetterSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                                    State.moRegistrationLetterId)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnDeleteAttach_Click(sender As Object, e As EventArgs) Handles btnDeleteAttach.Click
            Try
                emailText = Request.Params("EmailText")
                TheRegistrationLetter.AttachmentFileName = ""
                TheRegistrationLetter.Save()
                TheRegistrationLetter.UpdateAttachment(TheRegistrationLetter.Id, Nothing)
                txtAttachment.Text = ""
                txtAttachment.Visible = False
                btnDeleteAttach.Visible = False
                fileAttachment.Visible = True
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearTexts()
            moLetterTypeText.Text = Nothing
            moNumberOfDaysText.Text = Nothing
            moEmailSubjectText.Text = Nothing
            txtEmailFrom.Text = String.Empty
            txtEmailTo.Text = String.Empty
            emailText = String.Empty
            txtAttachment.Text = String.Empty
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            TheDealerControl.ClearMultipleDrop()
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealer()
            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                TheDealerControl.SetControl(False, TheDealerControl.MODES.NEW_MODE, True, dv, TheDealerControl.NO_CAPTION, True)
                If State.IsRegistrationLetterNew = True Then
                    TheDealerControl.SelectedGuid = Guid.Empty
                    TheDealerControl.ChangeEnabledControlProperty(True)
                Else
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    TheDealerControl.SelectedGuid = TheRegistrationLetter.DealerId
                End If
                TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                With TheRegistrationLetter
                    If State.IsRegistrationLetterNew = True Then
                        emailText = ""
                    Else
                        moLetterTypeText.Text = .LetterType
                        moNumberOfDaysText.Text = .NumberOfDays.ToString
                        moEmailSubjectText.Text = .EmailSubject
                        txtEmailFrom.Text = .EmailFrom
                        txtEmailTo.Text = .EmailTo
                        emailText = .EmailText
                        txtAttachment.Text = .AttachmentFileName
                    End If
                    SetButtonsState(State.IsRegistrationLetterNew)
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub PopulateGrid()
            If State.dvLetterVariable Is Nothing Then
                State.dvLetterVariable = LookupListNew.DropdownLookupList("RLV", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Dim drv As DataRowView, i As Integer
                For i = 0 To State.dvLetterVariable.Count - 1
                    State.dvLetterVariable.Item(i)("CODE") = "{" & State.dvLetterVariable.Item(i)("CODE").ToString & "}"
                Next
                State.dvLetterVariable.Table.AcceptChanges()
            End If
            gridVariable.AutoGenerateColumns = False
            gridVariable.DataSource = State.dvLetterVariable
            gridVariable.DataBind()
        End Sub

        Private Sub PopulateAll()
            PopulateGrid()
            If State.IsRegistrationLetterNew = True Then
                PopulateDealer()
            Else
                ClearAll()
                PopulateDealer()
                PopulateTexts()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()
            With TheRegistrationLetter
                .DealerId = TheDealerControl.SelectedGuid

                emailText = Request.Params("EmailText")
                If Not (.EmailText IsNot Nothing AndAlso emailText IsNot Nothing AndAlso emailText.Replace(Environment.NewLine, "") = .EmailText.Replace(Environment.NewLine, "")) Then
                    PopulateBOProperty(TheRegistrationLetter, EMAIL_TEXT_PROPERTY, emailText)
                End If

                If fileAttachment.HasFile Then
                    .AttachmentFileName = fileAttachment.FileName
                    '.AttachmentFileData = fileAttachment.FileBytes
                End If

                PopulateBOProperty(TheRegistrationLetter, LETTER_TYPE_PROPERTY, moLetterTypeText)
                PopulateBOProperty(TheRegistrationLetter, NUMBER_OF_DAYS_PROPERTY, moNumberOfDaysText)
                PopulateBOProperty(TheRegistrationLetter, EMAIL_SUBJECT_PROPERTY, moEmailSubjectText)
                PopulateBOProperty(TheRegistrationLetter, EMAIL_FROM_PROPERTY, txtEmailFrom)
                PopulateBOProperty(TheRegistrationLetter, EMAIL_TO_PROPERTY, txtEmailTo)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub
#End Region

#Region "Gui-Validation"
        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            TheDealerControl.ChangeEnabledControlProperty(bIsNew)
            ControlMgr.SetEnableControl(Me, moNumberOfDaysText, bIsNew)
            If txtAttachment.Text.Trim = "" Then
                fileAttachment.Visible = True
                btnDeleteAttach.Visible = False
                txtAttachment.Visible = False
            Else
                fileAttachment.Visible = False
                btnDeleteAttach.Visible = True
                txtAttachment.Visible = True
            End If
        End Sub
#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                With TheRegistrationLetter
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim isOK As Boolean = True
            Try
                
                PopulateBOsFromForm()
                If TheRegistrationLetter.IsDirty() Then
                    TheRegistrationLetter.Save()
                    If fileAttachment.HasFile Then
                        TheRegistrationLetter.UpdateAttachment(TheRegistrationLetter.Id, fileAttachment.FileBytes)
                    End If
                    State.boChanged = True
                    If State.IsRegistrationLetterNew = True Then
                        State.IsRegistrationLetterNew = False
                    End If
                    PopulateAll()
                    SetButtonsState(State.IsRegistrationLetterNew)
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

        Private Function DeleteRegistrationLetter() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheRegistrationLetter
                    '  .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                'undo the delete
                TheRegistrationLetter.RejectChanges()
                HandleErrors(ex, ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Labels"
        Private Sub BindBoPropertiesToLabels()
            'Me.BindBOPropertyToLabel(TheRegistrationLetter, DEALER_ID_PROPERTY, Me.moRegistrationDealerLabel)
            BindBOPropertyToLabel(TheRegistrationLetter, LETTER_TYPE_PROPERTY, moLetterTypeLabel)
            BindBOPropertyToLabel(TheRegistrationLetter, NUMBER_OF_DAYS_PROPERTY, moNumberOfDaysLabel)
            BindBOPropertyToLabel(TheRegistrationLetter, EMAIL_SUBJECT_PROPERTY, moEmailSubjectLabel)
            BindBOPropertyToLabel(TheRegistrationLetter, EMAIL_TEXT_PROPERTY, moEmailTextLabel)
            BindBOPropertyToLabel(TheRegistrationLetter, EMAIL_FROM_PROPERTY, lblEmailFrom)
            BindBOPropertyToLabel(TheRegistrationLetter, EMAIL_TO_PROPERTY, lblEmailTo)
        End Sub

        Private Sub ClearLabelsErrSign()
            'Me.ClearLabelErrSign(Me.moRegistrationDealerLabel)
            ClearLabelErrSign(moLetterTypeLabel)
            ClearLabelErrSign(moNumberOfDaysLabel)
            ClearLabelErrSign(moEmailSubjectLabel)
            ClearLabelErrSign(moEmailTextLabel)
            ClearLabelErrSign(lblEmailFrom)
            ClearLabelErrSign(lblEmailTo)
        End Sub
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If Not confResponse = String.Empty Then
                ' Return from the Back Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
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
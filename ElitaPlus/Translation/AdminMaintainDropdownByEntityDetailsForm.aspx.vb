Namespace Translation
    Public Class AdminMaintainDropdownByEntityDetailsForm
        Inherits ElitaPlusPage

#Region "Page Event"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    ShowMissingTranslations(ErrorControl)
                    'Disable the Menu Navigation on this page to force the exit only by Cancel
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    ' Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY_DETAILS_FORM")
                    UpdateBreadCrum()
                    MenuEnabled = False
                    SetStateProperties()
                    PopulateUserControlAvailableSelectedListItem()


                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorControl)
            End Try
        End Sub
        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            'Enable the Menu Navigation Back after returning from the child
            Try
                MenuEnabled = True
            Catch ex As Exception
                HandleErrors(ex, ErrorControl)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("ADMIN") & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY_DETAILS_FORM")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY_DETAILS_FORM")

                End If
            End If
        End Sub
#End Region

#Region "Parameters"
        Public Class Parameters
            Public ListItemByEntityBO As ListItemByEntity
            Public Sub New(listItemByEntityBO As ListItemByEntity)
                Me.ListItemByEntityBO = listItemByEntityBO
            End Sub
        End Class

#End Region
#Region "MyState"

        Class MyState
            Public MyBo As ListItemByEntity
            Public moParams As Parameters
            Public IsDirty As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            Try
                State.moParams = CType(CallingParameters, Parameters)
                '    TestClaim()
                If (State.moParams Is Nothing) OrElse (State.moParams.ListItemByEntityBO.ListCode.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If

                PopulateFormFromBo()
            Catch ex As Exception
                HandleErrors(ex, ErrorControl)
            End Try
        End Sub
#End Region
#Region "CONSTANTS"
        Public Const PAGE_NAME As String = "~/Translation/AdminMaintainDropdownByEntityDetailsForm.aspx"
        Private Const COL_DESCRIPTION_NAME As String = "description"
        Private Const COL_ID_NAME As String = "id"
        Public Const AVAILABLE_COMPANIES As String = "AVAILABLE_LIST_ITEM"
        Public Const SELECTED_COMPANIES As String = "SELECTED_LIST_ITEM"

#End Region
#Region "Handlers-Buttons"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If State.IsDirty = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM,
                                            HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorControl)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                UpdateEntityItems()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorControl)
            End Try
        End Sub
        Sub UpdateEntityItems()
            State.MyBo = New ListItemByEntity()
            Dim nNewListItem As New ArrayList

            Dim userListItemIdStr As String
            For Each userListItemIdStr In UserControlAvailableSelectedEntityListItem.SelectedList
                nNewListItem.Add(New Guid(userListItemIdStr))
            Next

            If UserControlAvailableSelectedEntityListItem.SelectedList.Count = 0 Then
                'ElitaPlusPage.SetLabelError(UserControlAvailableSelectedCompanies.SelectedTitleLabel)
                Throw New GUIException(Message.MSG_LIST_ITEM_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_ITEM_REQUIRED)
            End If
            With State.moParams.ListItemByEntityBO
                State.MyBo.ListCode = .ListCode
                State.MyBo.EntityReference = .EntityReference
                State.MyBo.EntityReferenceId = .EntityReferenceId
            End With
            State.MyBo.UpdateListItem(nNewListItem)
            DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)

        End Sub
        Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
            Try
                NavigationHistory.LastPage() 'does a pop of the last page
                ReturnToCallingPage()
            Catch ex As Exception
                HandleErrors(ex, ErrorControl)
            End Try
        End Sub

#End Region
#Region "Populate"
        Private Sub PopulateFormFromBo()
            Try
                With State.moParams.ListItemByEntityBO

                    TextBoxListCode.Text = .ListCode
                    EnableDisableControls(TextBoxListCode, True)
                    TextBoxListDescription.Text = .ListDescription
                    EnableDisableControls(TextBoxListDescription, True)
                    TextBoxEntityType.Text = .EntityType
                    EnableDisableControls(TextBoxEntityType, True)
                    TextBoxEntityDescription.Text = .EntityDescription
                    EnableDisableControls(TextBoxEntityDescription, True)

                End With

            Catch ex As Exception
                HandleErrors(ex, ErrorControl)
            End Try

        End Sub

        Sub PopulateUserControlAvailableSelectedListItem()
            Dim oLanguageCode As String = Codes.ENGLISH_LANG_CODE
            Dim oEntityRefId As Guid = State.moParams.ListItemByEntityBO.EntityReferenceId
            Dim oListCode As String = State.moParams.ListItemByEntityBO.ListCode
            Dim availableDv As DataView = State.MyBo.GetAvailableListItem(oLanguageCode, oEntityRefId, oListCode)
            Dim selectedDv As DataView = State.MyBo.GetSelectedListItem(oLanguageCode, oEntityRefId, oListCode)
            UserControlAvailableSelectedEntityListItem.SelectedList.Clear()
            UserControlAvailableSelectedEntityListItem.AvailableList.Clear()
            UserControlAvailableSelectedEntityListItem.SetSelectedData(selectedDv, COL_DESCRIPTION_NAME, COL_ID_NAME)
            UserControlAvailableSelectedEntityListItem.SetAvailableData(availableDv, COL_DESCRIPTION_NAME, COL_ID_NAME)
            UserControlAvailableSelectedEntityListItem.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_COMPANIES)
            UserControlAvailableSelectedEntityListItem.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_COMPANIES)
        End Sub

#End Region

    End Class


End Namespace

Namespace Translation
    Public Class AdminMaintainDropdownByEntityDetailsForm
        Inherits ElitaPlusPage

#Region "Page Event"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                If Not Me.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.ShowMissingTranslations(ErrorControl)
                    'Disable the Menu Navigation on this page to force the exit only by Cancel
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    ' Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY_DETAILS_FORM")
                    UpdateBreadCrum()
                    Me.MenuEnabled = False
                    SetStateProperties()
                    PopulateUserControlAvailableSelectedListItem()


                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub
        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            'Enable the Menu Navigation Back after returning from the child
            Try
                Me.MenuEnabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("ADMIN") & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY_DETAILS_FORM")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY_DETAILS_FORM")

                End If
            End If
        End Sub
#End Region

#Region "Parameters"
        Public Class Parameters
            Public ListItemByEntityBO As ListItemByEntity
            Public Sub New(ByVal listItemByEntityBO As ListItemByEntity)
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
                Me.State.moParams = CType(Me.CallingParameters, Parameters)
                '    TestClaim()
                If (Me.State.moParams Is Nothing) OrElse (Me.State.moParams.ListItemByEntityBO.ListCode.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If

                PopulateFormFromBo()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
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

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If Me.State.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM,
                                            Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                UpdateEntityItems()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub
        Sub UpdateEntityItems()
            Me.State.MyBo = New ListItemByEntity()
            Dim nNewListItem As New ArrayList

            Dim userListItemIdStr As String
            For Each userListItemIdStr In Me.UserControlAvailableSelectedEntityListItem.SelectedList
                nNewListItem.Add(New Guid(userListItemIdStr))
            Next

            If Me.UserControlAvailableSelectedEntityListItem.SelectedList.Count = 0 Then
                'ElitaPlusPage.SetLabelError(UserControlAvailableSelectedCompanies.SelectedTitleLabel)
                Throw New GUIException(Message.MSG_LIST_ITEM_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_ITEM_REQUIRED)
            End If
            With Me.State.moParams.ListItemByEntityBO
                Me.State.MyBo.ListCode = .ListCode
                Me.State.MyBo.EntityReference = .EntityReference
                Me.State.MyBo.EntityReferenceId = .EntityReferenceId
            End With
            Me.State.MyBo.UpdateListItem(nNewListItem)
            Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)

        End Sub
        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Try
                NavigationHistory.LastPage() 'does a pop of the last page
                Me.ReturnToCallingPage()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

#End Region
#Region "Populate"
        Private Sub PopulateFormFromBo()
            Try
                With Me.State.moParams.ListItemByEntityBO

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
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try

        End Sub

        Sub PopulateUserControlAvailableSelectedListItem()
            Dim oLanguageCode As String = Codes.ENGLISH_LANG_CODE
            Dim oEntityRefId As Guid = Me.State.moParams.ListItemByEntityBO.EntityReferenceId
            Dim oListCode As String = Me.State.moParams.ListItemByEntityBO.ListCode
            Dim availableDv As DataView = Me.State.MyBo.GetAvailableListItem(oLanguageCode, oEntityRefId, oListCode)
            Dim selectedDv As DataView = Me.State.MyBo.GetSelectedListItem(oLanguageCode, oEntityRefId, oListCode)
            Me.UserControlAvailableSelectedEntityListItem.SelectedList.Clear()
            Me.UserControlAvailableSelectedEntityListItem.AvailableList.Clear()
            Me.UserControlAvailableSelectedEntityListItem.SetSelectedData(selectedDv, COL_DESCRIPTION_NAME, COL_ID_NAME)
            Me.UserControlAvailableSelectedEntityListItem.SetAvailableData(availableDv, COL_DESCRIPTION_NAME, COL_ID_NAME)
            Me.UserControlAvailableSelectedEntityListItem.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_COMPANIES)
            Me.UserControlAvailableSelectedEntityListItem.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_COMPANIES)
        End Sub

#End Region

    End Class


End Namespace

Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic

Namespace Tables
    Partial Class TemplateGroupForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"
        Class MyState
            Public TemplateGroupId As Guid = Guid.Empty
            Public IsTemplateGroupNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public TemplateGroup As OcTemplateGroup
            Public ScreenSnapShotBO As OcTemplateGroup

            'Templates Grid
            Public PageIndex As Integer
            Public TemplateDV As OcTemplate.TemplateDV = Nothing
            Public TemplateId As Guid
            Public TemplateSortExpression As String = OcTemplate.TemplateDV.COL_TEMPLATE_CODE
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
            If Not IsReturningFromChild Then
                Me.State.TemplateGroupId = CType(Me.CallingParameters, Guid)
            End If

            If Me.State.TemplateGroupId.Equals(Guid.Empty) Then
                Me.State.IsTemplateGroupNew = True
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(TheTemplateGroup)
                ClearAll()
                PopulateAll()
            Else
                Me.State.IsTemplateGroupNew = False
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(TheTemplateGroup)
                PopulateAll()
            End If
        End Sub
#End Region

#Region "Constants"
        Private Const TEMPLATE_GROUP_FORM001 As String = "TEMPLATE_GROUP_FORM001" 'Template Group List Exception
        Private Const TEMPLATE_GROUP_FORM002 As String = "TEMPLATE_GROUP_FORM002" 'Template Group Code Field Exception
        Private Const TEMPLATE_GROUP_FORM003 As String = "TEMPLATE_GROUP_FORM003" 'Template Group Update Exception
        Private Const TEMPLATE_GROUP_FORM004 As String = "TEMPLATE_GROUP_FORM004" 'Template Group Delete Exception
        Private Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK" '"The record has been successfully saved"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const CANCEL_COMMAND As String = "CancelRecord"
        Private Const SAVE_COMMAND As String = "SaveRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"
        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"
        Private Const LABEL_DEALER As String = "DEALER_NAME"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"

        Public Const URL As String = "TemplateGroupForm.aspx"

        Public Const CODE_PROPERTY As String = "Code"
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const GROUP_ACCOUNT_USERNAME_PROPERTY As String = "GroupAccountUserName"
        Public Const GROUP_ACCOUNT_PASSWORD_PROPERTY As String = "GroupAccountPassword"

        Private Const GRID_COL_EDIT_IDX As Integer = 0
        Private Const GRID_COL_TEMPLATE_CODE As Integer = 0
        Private Const GRID_COL_TEMPLATE_DESCRIPTION As Integer = 1
        Private Const GRID_COL_TEMPLATE_ID As Integer = 2
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public TemplateGroupId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal templateGroupId As Guid, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.TemplateGroupId = templateGroupId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Properties"
        Private ReadOnly Property TheTemplateGroup As OcTemplateGroup
            Get
                If Me.State.TemplateGroup Is Nothing Then
                    If Me.State.IsTemplateGroupNew = True Then
                        ' For creating, inserting
                        Me.State.TemplateGroup = New OcTemplateGroup
                        Me.State.TemplateGroupId = Me.State.TemplateGroup.Id
                    Else
                        ' For updating, deleting
                        Me.State.TemplateGroup = New OcTemplateGroup(Me.State.TemplateGroupId)
                    End If
                End If

                Return Me.State.TemplateGroup
            End Get
        End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init"
        Protected WithEvents moErrorController As ErrorController

        Protected WithEvents moPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents moCoverageEditPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl

#Region " Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    TranslateGridHeader(TemplatesGrid)
                    UpdateBreadCrum()

                    Me.SetStateProperties()
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

                    If Me.State.IsTemplateGroupNew = True Then
                        CreateNew()
                    End If
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()

                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(TheTemplateGroup)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                Me.MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                Me.State.LastOperation = DetailPageCommand.Nothing_
            Else
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            End If
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.TemplateGroup = New OcTemplateGroup(CType(Me.CallingParameters, Guid))
                Else
                    Me.State.IsTemplateGroupNew = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private IsReturningFromChild As Boolean = False

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Try
                Me.MenuEnabled = False
                Me.IsReturningFromChild = True
                Dim retObj As TemplateForm.ReturnType = CType(ReturnPar, TemplateForm.ReturnType)

                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    Me.State.TemplateGroup = Nothing
                    Me.State.TemplateDV = Nothing
                    Me.State.boChanged = True
                End If

                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            If Not retObj.TemplateId = Guid.Empty Then
                                Me.State.TemplateId = retObj.TemplateId
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                            Me.State.TemplateId = Guid.Empty
                        Case Else
                            Me.State.TemplateId = Guid.Empty
                    End Select
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
#End Region

#Region "Handlers-Buttons"
        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New TemplateGroupForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.TemplateGroupId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.IsTemplateGroupNew Then
                    'Reload from the DB
                    Me.State.TemplateGroup = New OcTemplateGroup(Me.State.TemplateGroupId)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.TemplateGroup.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.TemplateGroup = New OcTemplateGroup
                End If

                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing
            Me.State.TemplateGroupId = Guid.Empty
            Me.State.IsTemplateGroupNew = True
            Me.State.TemplateGroup = New OcTemplateGroup
            ClearAll()
            Me.SetButtonsState(True)
            Me.DisabledTabsList.Clear()
            Me.DisabledTabsList.Add(Tab_Templates)
            Me.PopulateAll()

            If Me.State.IsTemplateGroupNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Me.PopulateBOsFromForm()

            Dim newObj As New OcTemplateGroup
            newObj.Copy(TheTemplateGroup)

            Me.State.TemplateGroup = newObj
            Me.State.TemplateGroupId = Guid.Empty
            Me.State.IsTemplateGroupNew = True

            With TheTemplateGroup
                .Code = Nothing
                .Description = Nothing
                .GroupAccountUserName = Nothing
                .GroupAccountPassword = Nothing
            End With

            Me.PopulateDealerList()
            Me.PopulateTemplatesGrid()

            Me.SetButtonsState(True)
            Me.DisabledTabsList.Clear()
            Me.DisabledTabsList.Add(Tab_Templates)

            'create the backup copy
            Me.State.ScreenSnapShotBO = New OcTemplateGroup
            Me.State.ScreenSnapShotBO.Copy(TheTemplateGroup)
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteTemplateGroup() = True Then
                    Me.State.boChanged = True
                    Dim retType As New TemplateGroupForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.TemplateGroupId, True)
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNewTemplate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewTemplate_WRITE.Click
            Try
                Me.State.TemplateId = Guid.Empty
                Dim callObject = New TemplateForm.CallType(Guid.Empty, TheTemplateGroup.Id)
                Me.callPage(TemplateForm.URL, callObject)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Clear"
        Private Sub ClearTexts()
            txtTemplateGroupCode.Text = Nothing
            txtTemplateGroupDescription.Text = Nothing
            txtGroupAccountUserName.Text = Nothing
            txtGroupAccountPassword.Text = Nothing
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            'ClearList(moDateOfPaymentOptionDrop)
        End Sub
#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("TEMPLATE_GROUP_DETAIL")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("TEMPLATE_GROUP_DETAIL")
                End If
            End If
        End Sub

        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'BindListControlToDataView(moDateOfPaymentOptionDrop, LookupListNew.GetDateOfPaymentOPtionsList(oLanguageId), , , True)
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TEMPLATE_GROUP_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Dim TwoDecimalsId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetNumberOfDigitsRoundOffList(oLanguageId), "TWO_DECIMAL")

                With TheTemplateGroup
                    If Me.State.IsTemplateGroupNew = True Then
                        'BindSelectItem(Nothing, moDateOfPaymentOptionDrop)
                        'SetSelectedItem(Me.moDateOfPaymentOptionDrop, TwoDecimalsId)
                        txtTemplateGroupCode.Text = Nothing
                        txtTemplateGroupDescription.Text = Nothing
                        txtGroupAccountUserName.Text = Nothing
                        txtGroupAccountPassword.Text = Nothing
                    Else
                        'BindSelectItem(.DateOfPaymentOptionId.ToString, moDateOfPaymentOptionDrop)
                        Me.PopulateControlFromBOProperty(Me.txtTemplateGroupCode, .Code)
                        Me.PopulateControlFromBOProperty(Me.txtTemplateGroupDescription, .Description)
                        Me.PopulateControlFromBOProperty(Me.txtGroupAccountUserName, .GroupAccountUserName)
                        Me.PopulateControlFromBOProperty(Me.txtGroupAccountPassword, .GroupAccountPassword)
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If Me.State.IsTemplateGroupNew = True Then
                PopulateDropDowns()
                PopulateDealerList()
                PopulateTemplatesGrid()
            Else
                ClearAll()
                PopulateDropDowns()
                PopulateTexts()
                PopulateDateFields()
                PopulateDealerList()
                PopulateTemplatesGrid()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()
            With Me.TheTemplateGroup
                Me.PopulateBOProperty(TheTemplateGroup, CODE_PROPERTY, Me.txtTemplateGroupCode)
                Me.PopulateBOProperty(TheTemplateGroup, DESCRIPTION_PROPERTY, Me.txtTemplateGroupDescription)
                Me.PopulateBOProperty(TheTemplateGroup, GROUP_ACCOUNT_USERNAME_PROPERTY, Me.txtGroupAccountUserName)
                Me.PopulateBOProperty(TheTemplateGroup, GROUP_ACCOUNT_PASSWORD_PROPERTY, Me.txtGroupAccountPassword)
            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Public Sub PopulateDateFields()
        End Sub
#End Region

#Region "Tabs"
        Public Const Tab_Dealers As String = "0"
        Public Const Tab_Templates As String = "1"

        Dim _DisabledTabsList As List(Of String)

        Private ReadOnly Property DisabledTabsList As List(Of String)
            Get
                If _DisabledTabsList Is Nothing Then
                    _DisabledTabsList = New List(Of String)
                    Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")

                    If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
                        _DisabledTabsList.AddRange(DisabledTabs)
                    End If
                End If

                Return _DisabledTabsList
            End Get
        End Property

        Sub PopulateDealerList()
            With TheTemplateGroup
                Dim availableDv As DataView = .GetAvailableDealers(ElitaPlusIdentity.Current.ActiveUser.Companies)
                Dim selectedDv As DataView = .GetSelectedDealers(ElitaPlusIdentity.Current.ActiveUser.Companies)
                Me.UserControlAvailableSelectedDealers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                Me.UserControlAvailableSelectedDealers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            End With
        End Sub

        Private Sub UserControlAvailableSelectedDealers_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Attach
            Try
                If attachedList.Count > 0 Then
                    TheTemplateGroup.AttachDealers(attachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedDealers_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Detach
            Try
                If detachedList.Count > 0 Then
                    TheTemplateGroup.DetachDealers(detachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Gui-Validation"
        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            'ControlMgr.SetEnableControl(Me, txtTemplateGroupCode, bIsNew)
        End Sub
#End Region

#Region "Business Part"
        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            Try
                With TheTemplateGroup
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty Or TheTemplateGroup.IsChildrenDirty
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TEMPLATE_GROUP_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Try
                Me.PopulateBOsFromForm()
                Dim errors As List(Of ValidationError) = New List(Of ValidationError)

                If TheTemplateGroup.DealerList.Count = 0 Then
                    errors.Add(New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.ERR_NO_DEALER_ASSOCIATED, GetType(OcTemplateGroup), Nothing, Nothing, Nothing))
                Else
                    For Each templateGroupDealer As OcTemplateGroupDealer In TheTemplateGroup.DealerList
                        If OcTemplateGroupDealer.GetAssociatedTemplateGroupCount(templateGroupDealer.DealerId, templateGroupDealer.OcTemplateGroupId) > 0 Then
                            errors.Add(New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.ERR_DEALER_ALREADY_ASSOCIATED_TO_TEMPLATE_GROUP, GetType(OcTemplateGroup), Nothing, Nothing, Nothing))
                            Exit For
                        End If
                    Next
                End If

                If errors.Count > 0 Then
                    Throw New BOValidationException(errors.ToArray, GetType(OcTemplateGroup).FullName)
                End If

                SetLabelColor(Me.lblTemplateGroupCode)
                SetLabelColor(Me.lblTemplateGroupDescription)
                SetLabelColor(Me.lblGroupAccountUserName)
                SetLabelColor(Me.lblGroupAccountPassword)

                If TheTemplateGroup.IsDirty OrElse TheTemplateGroup.IsChildrenDirty Then
                    Me.TheTemplateGroup.Save()
                    Me.State.boChanged = True
                    If Me.State.IsTemplateGroupNew = True Then
                        Me.State.IsTemplateGroupNew = False
                        DisabledTabsList.Clear()
                    End If
                    PopulateAll()
                    Me.SetButtonsState(Me.State.IsTemplateGroupNew)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Function

        Private Function DeleteATemplateGroupDealer(ByVal oRow As DataRow) As Boolean
            Dim bIsOk As Boolean = True
            Try
                Dim templateGroupDealer As OcTemplateGroupDealer = New OcTemplateGroupDealer(New Guid(CType(oRow(OcTemplateGroupDealerDAL.COL_NAME_OC_TEMPLATE_GROUP_DEALER_ID), Byte())))
                templateGroupDealer.Delete()
                templateGroupDealer.Save()
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(TEMPLATE_GROUP_FORM004, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Me.MasterPage.MessageController.AddError(ex.Message)
                Me.MasterPage.MessageController.Show()
                bIsOk = False
            End Try

            Return bIsOk
        End Function

        Private Function DeleteAllTemplateGroupDealers() As Boolean
            Dim bIsOk As Boolean = True
            Dim oRows As DataRowCollection
            Dim oRow As DataRow

            Try
                oRows = OcTemplateGroupDealer.GetList(TheTemplateGroup.Id).Table.Rows
                For Each oRow In oRows
                    If DeleteATemplateGroupDealer(oRow) = False Then Return False
                Next
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(TEMPLATE_GROUP_FORM004, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Me.MasterPage.MessageController.AddError(ex.Message)
                Me.MasterPage.MessageController.Show()
                bIsOk = False
            End Try

            Return bIsOk
        End Function

        Private Function DeleteTemplateGroup() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheTemplateGroup
                    PopulateBOsFromForm()
                    'Check if there are any templates associated with the coverage
                    If .GetAssociatedTemplateCount(.Id) > 0 Then
                        Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.ERR_DELETE_TEMPLATE_GROUP_WITH_TEMPLATES_PRESENT, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                        .cancelEdit()
                        bIsOk = False
                    ElseIf DeleteAllTemplateGroupDealers() = False Then
                        .cancelEdit()
                        bIsOk = False
                    Else
                        .Delete()
                        .Save()
                    End If
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(TEMPLATE_GROUP_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        'Private Function DeleteTemplateGroup() As Boolean
        '    Dim bIsOk As Boolean = True

        '    Try
        '        With TheTemplateGroup
        '            .BeginEdit()
        '            PopulateBOsFromForm()
        '            .Delete()
        '            .Save()
        '        End With
        '    Catch ex As Exception
        '        Me.MasterPage.MessageController.AddError(TEMPLATE_GROUP_FORM002)
        '        Me.MasterPage.MessageController.AddError(ex.Message, False)
        '        Me.MasterPage.MessageController.Show()
        '        bIsOk = False
        '    End Try
        '    Return bIsOk
        'End Function
#End Region

#Region "State-Management"
        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If
        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If
        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button
                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"
        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(TheTemplateGroup, CODE_PROPERTY, lblTemplateGroupCode)
            Me.BindBOPropertyToLabel(TheTemplateGroup, DESCRIPTION_PROPERTY, lblTemplateGroupDescription)
            Me.BindBOPropertyToLabel(TheTemplateGroup, GROUP_ACCOUNT_USERNAME_PROPERTY, lblGroupAccountUserName)
            Me.BindBOPropertyToLabel(TheTemplateGroup, GROUP_ACCOUNT_PASSWORD_PROPERTY, lblGroupAccountPassword)
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(lblTemplateGroupCode)
            Me.ClearLabelErrSign(lblTemplateGroupDescription)
            Me.ClearLabelErrSign(lblGroupAccountUserName)
            Me.ClearLabelErrSign(lblGroupAccountPassword)
        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

        'Private Sub moDateOfPaymentOptionDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moDateOfPaymentOptionDrop.SelectedIndexChanged,
        '                                                                                                     moDateOfPaymentOffsetDaysText.TextChanged,
        '                                                                                                     moStartDayText.TextChanged,
        '                                                                                                     moEndDayText.TextChanged,
        '                                                                                                     moBillingRunDateOffsetDaysText.TextChanged
        '    ClearLabelsErrSign()
        '    Try
        '        If Not moStartDayText.Text = String.Empty AndAlso Not IsNumeric(moStartDayText.Text) Then
        '            SetLabelError(moStartDayLabel)
        '            Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
        '        ElseIf Not moEndDayText.Text = String.Empty AndAlso Not IsNumeric(moEndDayText.Text) Then
        '            SetLabelError(moEndDayLabel)
        '            Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
        '        ElseIf Not moBillingRunDateOffsetDaysText.Text = String.Empty AndAlso Not IsNumeric(moBillingRunDateOffsetDaysText.Text) Then
        '            SetLabelError(moBillingRunDateOffsetDaysLabel)
        '            Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
        '        ElseIf Not moDateOfPaymentOffsetDaysText.Text = String.Empty AndAlso Not IsNumeric(moDateOfPaymentOffsetDaysText.Text) Then
        '            SetLabelError(moDateOfPaymentOffsetDaysLabel)
        '            Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
        '        Else
        '            Me.PopulateBOProperty(TheTemplateGroup, DATE_OF_PAYMENT_OPTION_ID_PROPETRY, Me.moDateOfPaymentOptionDrop)
        '            PopulateDateFields()
        '        End If

        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub
#End Region

#Region "Datagrid Related"
        Public Sub Grid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles TemplatesGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton

                If Not dvRow Is Nothing And Me.State.TemplateDV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(OcTemplate.TemplateDV.COL_TEMPLATE_CODE).ToString

                        e.Row.Cells(Me.GRID_COL_TEMPLATE_DESCRIPTION).Text = dvRow(OcTemplate.TemplateDV.COL_DESCRIPTION).ToString
                        e.Row.Cells(Me.GRID_COL_TEMPLATE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(OcTemplate.TemplateDV.COL_TEMPLATE_ID), Byte()))
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub Grid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Dim TemplateId = New Guid(Me.TemplatesGrid.Rows(index).Cells(Me.GRID_COL_TEMPLATE_ID).Text)
                    Me.callPage(TemplateForm.URL, New TemplateForm.CallType(TemplateId, TheTemplateGroup.Id))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid__PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles TemplatesGrid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.TemplatesGrid.PageIndex = Me.State.PageIndex
                Me.PopulateTemplatesGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateTemplatesGrid()
            Dim oDataView As DataView

            Try
                With TheTemplateGroup
                    If Not .Id.Equals(Guid.Empty) Then
                        If Me.State.TemplateDV Is Nothing Then
                            Me.State.TemplateDV = GetTemplateDV()
                        End If
                    End If
                End With

                If Not Me.State.TemplateDV Is Nothing Then
                    Dim dv As OcTemplate.TemplateDV

                    If Me.State.TemplateDV.Count = 0 Then
                        dv = Me.State.TemplateDV.AddNewRowToEmptyDV
                        SetPageAndSelectedIndexFromGuid(dv, Me.State.TemplateId, Me.TemplatesGrid, Me.State.PageIndex)
                        Me.TemplatesGrid.DataSource = dv
                    Else
                        SetPageAndSelectedIndexFromGuid(Me.State.TemplateDV, Me.State.TemplateId, Me.TemplatesGrid, Me.State.PageIndex)
                        Me.TemplatesGrid.DataSource = Me.State.TemplateDV
                    End If

                    Me.State.TemplateDV.Sort = Me.State.TemplateSortExpression

                    'LL: TODO: Map this functionality to edits and deletes from Template page?
                    'If (Me.State.IsProductPolicyAfterSave) Then
                    '    Me.State.IsProductPolicyAfterSave = False
                    '    Me.SetPageAndSelectedIndexFromGuid(Me.State.ProductPolicySearchDV, Me.State.ProductPolicyId, Me.moProductPolicyDatagrid, Me.moProductPolicyDatagrid.PageIndex)
                    'ElseIf (Me.State.IsProductPolicyEditMode) Then
                    '    Me.SetPageAndSelectedIndexFromGuid(Me.State.ProductPolicySearchDV, Me.State.ProductPolicyId, Me.moProductPolicyDatagrid, Me.moProductPolicyDatagrid.PageIndex, Me.State.IsProductPolicyEditMode)
                    'Else
                    '    'In a Delete scenario...
                    '    Me.SetPageAndSelectedIndexFromGuid(Me.State.ProductPolicySearchDV, Guid.Empty, Me.moProductPolicyDatagrid, Me.moProductPolicyDatagrid.PageIndex, Me.State.IsProductPolicyEditMode)
                    'End If

                    Me.TemplatesGrid.AutoGenerateColumns = False

                    If Me.State.TemplateDV.Count = 0 Then
                        SortAndBindGrid(dv)
                    Else
                        SortAndBindGrid(Me.State.TemplateDV)
                    End If

                    If Me.State.TemplateDV.Count = 0 Then
                        For Each gvRow As GridViewRow In Me.TemplatesGrid.Rows
                            gvRow.Visible = False
                            gvRow.Controls.Clear()
                        Next
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SortAndBindGrid(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Me.TemplatesGrid.DataSource = dvBinding
            HighLightSortColumn(Me.TemplatesGrid, Me.State.TemplateSortExpression)
            Me.TemplatesGrid.DataBind()
            If Not Me.TemplatesGrid.BottomPagerRow.Visible Then Me.TemplatesGrid.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In Me.TemplatesGrid.Rows
                    gvRow.Controls.Clear()
                Next
            End If
            Session("recCount") = Me.State.TemplateDV.Count
        End Sub

        Private Function GetTemplateDV() As OcTemplate.TemplateDV
            Dim dv As OcTemplate.TemplateDV
            dv = GetDataView()
            dv.Sort = Me.TemplatesGrid.DataMember()
            Me.TemplatesGrid.DataSource = dv
            Return (dv)
        End Function

        Private Function GetDataView() As OcTemplate.TemplateDV
            Dim dt As DataTable = TheTemplateGroup.TemplateList.Table
            Return New OcTemplate.TemplateDV(dt)
        End Function
#End Region
#End Region
    End Class
End Namespace

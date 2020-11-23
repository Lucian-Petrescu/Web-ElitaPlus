'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (9/20/2004)  ********************

Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class ServiceGroupForm
    Inherits ElitaPlusSearchPage



    Protected WithEvents ErrorCtrl As ErrorController



    Protected WithEvents UserControlAvailableSelectedManufacturers As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

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

#Region "Constants"
    Public Const URL As String = "ServiceGroupForm.aspx"
    Public Const GRID_COL_DELETE_BUTTON As Integer = 0
    Public Const GRID_COL_RISK_TYPE_ID As Integer = 1
    Public Const GRID_COL_RISK_TYPE As Integer = 2
    Public Const GRID_COL_MANUFACTURER As Integer = 3
    Public Const GRID_COL_RISK_TYPE_MANUFACTURER_ID As Integer = 4
    Public Const RISK_TYPE_COL_NAME As String = "risk_type"
    Public Const MANUFACTURER_COL_NAME As String = "manufacturer"
    Public Const SERVICE_GROUP_RISK_COL_ID As String = "service_group_risk_type_id"
    Public Const SERVICE_GROUP_RISK_MAN_COL_ID As String = "service_group_risk_manu_id"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceGroup
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ServiceGroup, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As ServiceGroup
        Public ScreenSnapShotBO As ServiceGroup
        Public SelectedRiskTypeId As Guid = Guid.Empty
        Public SortExpressionDetailGrid As String = "RISK_TYPE_ENGLISH"
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public IsNew As Boolean = False
    End Class

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
                Me.State.MyBO = New ServiceGroup(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.ErrorCtrl.Clear_Hide()
            If Not Me.IsPostBack Then
                'Date Calendars
                Me.MenuEnabled = False
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ServiceGroup
                    Me.State.IsNew = True
                End If
                ' Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)

                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim FilteredRecord As ListItem() = (From x In countryLKl
                                                    Where (ElitaPlusIdentity.Current.ActiveUser.Countries).Contains(x.ListItemId)
                                                    Select x).ToArray()

                moCountryDrop.Populate(FilteredRecord, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })
                PopulateCountry()
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'Now disable depebding on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
        ControlMgr.SetEnableControl(Me, TextboxShortDesc, True)
        If Not Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, TextboxShortDesc, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ShortDesc", Me.LabelShortDesc)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        If Me.State.IsNew Then
            ' New one
            If moCountryDrop.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
        Else
            oCountry = New Country(Me.State.MyBO.CountryId)
            Me.SetSelectedItem(moCountryDrop, Me.State.MyBO.CountryId)
            Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso Me.State.IsNew) Then
            ' Multiple Countries
            ControlMgr.SetVisibleControl(Me, moCountryDrop, True)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
        Else
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, True)
        End If
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Me.BindListControlToDataView(Me.DropDownRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        Me.DropDownRiskType.Populate(riskTypeLkl, New PopulateOptions() With
     {
       .AddBlankItem = True
        })
    End Sub

#Region "Detail Grid"
    Sub PopulateDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As DataSet
        Dim countofrecords As Double
        countofrecords = Me.State.MyBO.countofrecords(Me.State.MyBO.Id)

        dv = GetDV()

        Me.DataGridDetail.DataSource = dv
        Me.DataGridDetail.AllowCustomPaging = True
        Me.DataGridDetail.VirtualItemCount = countofrecords
        Me.DataGridDetail.AutoGenerateColumns = False
        Me.DataGridDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Private Function GetDV() As DataSet
        Dim dv As DataSet
        dv = Me.State.MyBO.LoadGrid(Me.State.MyBO.Id, DataGridDetail.CurrentPageIndex, Me.State.SortExpressionDetailGrid)
        Return (dv)
    End Function

    Sub PopulateUserControlAvailableSelectedManufacturers()
        Me.UserControlAvailableSelectedManufacturers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, False)
        If Not Me.State.SelectedRiskTypeId.Equals(Guid.Empty) Then
            If Me.RadioButtonListManufacturer.SelectedItem.Value <> "Any" Then
                Dim availableDv As DataView = Me.State.MyBO.GetAvailableManufacturers(Me.State.SelectedRiskTypeId)
                Dim selectedDv As DataView = Me.State.MyBO.GetSelectedManufacturers(Me.State.SelectedRiskTypeId)
                Me.UserControlAvailableSelectedManufacturers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                Me.UserControlAvailableSelectedManufacturers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, True)
            End If
        End If
    End Sub

    Sub PopulateDetailControls()
        Me.PopulateDetailGrid()
        PopulateUserControlAvailableSelectedManufacturers()
    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.TextboxShortDesc, .ShortDesc)
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)

        End With

        PopulateDetailControls()

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "ShortDesc", Me.TextboxShortDesc)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "CountryId", moCountryDrop)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New ServiceGroup
        Me.State.IsNew = True
        PopulateCountry()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New ServiceGroup
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        PopulateCountry()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New ServiceGroup
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then

                Dim result As Integer
                Dim Strarr As String
                Dim risktypeid As Guid = Guid.Parse(Me.DropDownRiskType.SelectedValue.ToString())

                For result = 0 To Me.UserControlAvailableSelectedManufacturers.SelectedList.Count() - 1
                    Strarr = Strarr + Assurant.ElitaPlus.Common.GuidControl.GuidToHexString(Guid.Parse(Me.UserControlAvailableSelectedManufacturers.SelectedList.Item(result))).ToString() + ";"
                Next

                Me.State.MyBO.sgrtmanusave(Me.State.MyBO.Id, risktypeid, Strarr, ElitaPlusIdentity.Current.ActiveUser.NetworkId, TextboxShortDesc.Text, Me.TextboxDescription.Text, ElitaPlusIdentity.Current.ActiveUser.Company.CountryId)
                Me.State.IsNew = False
                Me.State.HasDataChanged = False
                PopulateCountry()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New ServiceGroup(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New ServiceGroup
            End If
            PopulateCountry()
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Page Control Events"

#End Region

#Region "Handle-Drop"

    Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
            PopulateCountry()
            PopulateDropdowns()
            Me.PopulateFormFromBOs()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Detail Grid Events"

    Private Sub DataGridDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_RISK_TYPE).Text = dvRow(RISK_TYPE_COL_NAME).ToString
                e.Item.Cells(Me.GRID_COL_MANUFACTURER).Text = dvRow(MANUFACTURER_COL_NAME).ToString
                e.Item.Cells(Me.GRID_COL_RISK_TYPE_ID).Text = dvRow(SERVICE_GROUP_RISK_COL_ID).ToString
                e.Item.Cells(Me.GRID_COL_RISK_TYPE_MANUFACTURER_ID).Text = dvRow(SERVICE_GROUP_RISK_MAN_COL_ID).ToString

                Dim del As ImageButton
                del = CType(e.Item.Cells(Me.GRID_COL_DELETE_BUTTON).FindControl("btnDeleteRiskType_WRITE"), ImageButton)
                If Not del Is Nothing Then
                    Me.AddConfirmation(del, ElitaPlus.ElitaPlusWebApp.Message.DELETE_RECORD_PROMPT)
                End If

            End If

            'if new record or new with copy, hide the delete column
            If Me.State.MyBO.IsNew Then DataGridDetail.Columns(Me.GRID_COL_DELETE_BUTTON).Visible = False
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub DataGridDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetail.PageIndexChanged
        Try
            Me.DataGridDetail.CurrentPageIndex = e.NewPageIndex
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetail.SortCommand
        Try
            If Me.State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If Me.State.SortExpressionDetailGrid.EndsWith(" DESC") Then
                    Me.State.SortExpressionDetailGrid = e.SortExpression
                Else
                    Me.State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpressionDetailGrid = e.SortExpression
            End If
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDetail.ItemCommand

        Try
            If e.CommandName = "DeleteRiskType" Then

                Me.PopulateBOsFormFrom()

                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                End If

                If Not e.Item.Cells(Me.GRID_COL_MANUFACTURER).Text.Trim.Equals("*") Then
                    Dim _serviceGroupRiskTypeMan As New SgRtManufacturer(New Guid(e.Item.Cells(Me.GRID_COL_RISK_TYPE_MANUFACTURER_ID).Text))
                    _serviceGroupRiskTypeMan.Delete()
                    _serviceGroupRiskTypeMan.Save()
                Else
                    Dim _serviceGroupRiskType As New ServiceGroupRiskType(New Guid(e.Item.Cells(Me.GRID_COL_RISK_TYPE_ID).Text))
                    _serviceGroupRiskType.Delete()
                    _serviceGroupRiskType.Save()
                End If

                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New ServiceGroup(Me.State.MyBO.Id)
                End If

                PopulateDetailControls()
            End If

        Catch ex As Exception
            Me.ErrorCtrl.AddErrorAndShow(ex.Message, False)
        End Try
    End Sub

#End Region


#Region "Attach - Detach Event Handlers"
    Private Sub RadioButtonListManufacturer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonListManufacturer.SelectedIndexChanged
        Try
            If Not Me.State.SelectedRiskTypeId.Equals(Guid.Empty) Then
                If Me.RadioButtonListManufacturer.SelectedItem.Value <> "Any" Then
                    If Me.State.MyBO.IsRiskTypeAssociatedForAnyManufacturer(Me.State.SelectedRiskTypeId) Then
                        Me.State.MyBO.DetachRiskType(Me.State.SelectedRiskTypeId)
                        PopulateDetailControls()
                    End If
                Else
                    Me.State.MyBO.AttachRiskTypeForAnyManufacturer(Me.State.SelectedRiskTypeId)
                    PopulateDetailControls()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub DropDownRiskType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownRiskType.SelectedIndexChanged
        Try
            Me.State.SelectedRiskTypeId = Me.GetSelectedItem(Me.DropDownRiskType)
            Me.RadioButtonListManufacturer.SelectedIndex = 0 'Selected
            If Not Me.State.SelectedRiskTypeId.Equals(Guid.Empty) Then
                If Me.State.MyBO.IsRiskTypeAssociatedForAnyManufacturer(Me.State.SelectedRiskTypeId) Then
                    Me.RadioButtonListManufacturer.SelectedIndex = 1 'Any
                End If
            End If
            Me.PopulateUserControlAvailableSelectedManufacturers()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachManufacturers(Me.State.SelectedRiskTypeId, attachedList)
                Me.PopulateDetailGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachManufacturers(Me.State.SelectedRiskTypeId, detachedList)
                Me.PopulateDetailGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region



End Class



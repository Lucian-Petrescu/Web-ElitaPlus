Option Strict On
Imports System.Text
Imports System.IO
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class AcctAmtSrcMappingDetailForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "~/AcctFinanceAutomation/AcctAmtSrcMappingDetailForm.aspx"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"
    Public Const PAGETITLE As String = "ACCT_AMT_SOURCE_MAPPING"

    Public Const GRID_CTRL_NAME_PRODUCT As String = "ddlProduct"
    Public Const GRID_CTRL_NAME_LOSSTYPE As String = "ddlLossType"
    Public Const GRID_CTRL_NAME_OPERATION As String = "ddlOperation"
    Public Const GRID_CTRL_NAME_RATEBUCKET As String = "ddlBucket"
    Public Const GRID_CTRL_NAME_MappingDetailID As String = "lblDetailID"
    Public Const GRID_CTRL_NAME_COUNTFIELD As String = "ddlCountField"


    Private dvRateBuckets As DataView
    Private listFormularToSave As Collections.Generic.List(Of AfaAcctAmtSrcMapping)
#End Region

#Region "Page Call Parm Type"
    Public Enum PageCallActionType
        AddNewMapping = 0
        EditExistingMapping = 1
    End Enum

    Public Enum EditingAction
        None = 0
        AddNew = 1
        EditExisting = 2
        Delete = 3
        AddFirst = 4
    End Enum
    Public Class PageCallType
        Public ID As Guid
        Public ActionType As PageCallActionType
        Public DealerID As Guid
    End Class

#End Region

#Region "Page State"

    Class MyState
        Public MyParentBO As AfaAcctAmtSrc
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public CurrentAction As EditingAction = EditingAction.None
        Public HasDataChanged As Boolean
        Public dvMappingList As DataView
        Public listFormular As Collections.Generic.List(Of AfaAcctAmtSrcMapping)
        Public EditingDetailID As Integer
        Public LastErrMsg As String
        Public EditingProductID As Guid
        Public EditingLossType As String
        Public EditingCntToUse As String
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

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Try
            UpdateBreadCrum()
            If (Not Me.IsPostBack) Then
                PopulateDropdowns()
                ' Translate Grid Headers
                Me.TranslateGridHeader(Me.gridMapping)
                Me.TranslateGridHeader(Me.gridEditFormular)
                ' Populate the page
                PopulatePage()
            Else
                CheckIfComingFromSaveConfirm()
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub AcctAmtSrcMappingDetailForm_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles Me.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Dim objParm As PageCallType = CType(CallingPar, PageCallType)
                If objParm.ActionType = PageCallActionType.EditExistingMapping Then
                    Me.State.MyParentBO = New AfaAcctAmtSrc(objParm.ID)
                Else 'new mapping, create a new object
                    Me.State.MyParentBO = New AfaAcctAmtSrc()
                    Me.State.MyParentBO.DealerId = objParm.DealerID
                    Me.State.MyParentBO.AcctAmtSrcFieldTypeId = objParm.ID
                    Me.State.MyParentBO.EntityByRegion = "N"
                    NewMappings()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Helper Functions"

    
    Private Sub PopulateDropdowns()
        ddlEntityByRegion.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("NO"), "N"))
        ddlEntityByRegion.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("YES"), "Y"))

        Dim dv As DataView = ReppolicyClaimCount.GetCoverageTypeListByDealer(State.MyParentBO.DealerId)
        dv.Sort = "description ASC"

        ddlEntityByRegionCovType.Items.Add(New ListItem("", ""))
        For i As Integer = 0 To dv.Count - 1
            ddlEntityByRegionCovType.Items.Add(New ListItem(dv(i)("description").ToString, dv(i)("code").ToString))
        Next

        ddlReconWithInvoice.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("NO"), "N"))
        ddlReconWithInvoice.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("RECONCILE_AS_DEBIT"), "D"))
        ddlReconWithInvoice.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("RECONCILE_AS_CREDIT"), "C"))
        ddlReconWithInvoice.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("RECONCILE_AS_BOTH"), "B"))

        ddlUseFormulaForClip.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("NO"), "N"))
        ddlUseFormulaForClip.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("YES"), "Y"))
        ddlUseFormulaForClip.SelectedValue = "N"
    End Sub

    Private Sub PopulatePage()
        Dim AcctFieldTypedv As DataView = LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_ACCT_FIELD_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        txtAmtSrc.Text = (LookupListNew.GetDescriptionFromId(AcctFieldTypedv, State.MyParentBO.AcctAmtSrcFieldTypeId))
        ddlEntityByRegion.SelectedValue = State.MyParentBO.EntityByRegion
        ddlEntityByRegionCovType.SelectedValue = State.MyParentBO.EntityByRegionCoverageType
        ddlReconWithInvoice.SelectedValue = State.MyParentBO.ReconcilWithInvoice
        ddlUseFormulaForClip.SelectedValue = State.MyParentBO.UseFormulaForClip

        PopulateMappingGrid(Guid.Empty, String.Empty, String.Empty)
    End Sub

    Private Sub PopulateMappingGrid(ByVal prodID As Guid, ByVal LossType As String, ByVal CntToUse As String, Optional ByVal blnGetMatchedOnly As Boolean = False)
        Try

            If Me.State.dvMappingList Is Nothing Then
                State.dvMappingList = AfaAcctAmtSrcMapping.getFormularByProdLossType(State.MyParentBO.Id, prodID, LossType, CntToUse, blnGetMatchedOnly)
            End If

            If State.CurrentAction = EditingAction.None Then
                gridMapping.ShowFooter = True
            Else
                gridMapping.ShowFooter = False
            End If

            If State.dvMappingList.Count = 0 Then
                'CreateHeaderForEmptyGrid(gridMapping, "")
                ShowHeaderForEmptyGrid(gridMapping, State.dvMappingList)
            Else
                Me.gridMapping.DataSource = State.dvMappingList
                Me.gridMapping.DataBind()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateFormularGrid(ByVal prodID As Guid, ByVal LossType As String, ByVal CntToUse As String)
        Try
            If State.CurrentAction = EditingAction.AddNew Then ' New formular, no need to check the existings
                State.listFormular = New Collections.Generic.List(Of AfaAcctAmtSrcMapping)
            Else
                If Me.State.listFormular Is Nothing Then
                    State.listFormular = AfaAcctAmtSrcMapping.getList(State.MyParentBO.Id, prodID, LossType, CntToUse, True)
                End If
            End If


            Dim cntFormular As Integer = State.listFormular.Count, intMore As Integer
            If cntFormular < 10 Then
                intMore = 10 - cntFormular
            Else ' add 3 more spaces
                intMore = 3
            End If

            Dim obj As AfaAcctAmtSrcMapping

            For i As Integer = 1 To intMore
                obj = New AfaAcctAmtSrcMapping()
                obj.AcctAmtSrcId = State.MyParentBO.Id
                State.listFormular.Add(obj)
            Next

            Me.gridEditFormular.DataSource = State.listFormular
            Me.gridEditFormular.DataBind()
            gridEditFormular.Visible = True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Private Sub ShowHeaderForEmptyGrid(ByVal objGrid As GridView, ByRef dv As DataView)

        dv.Table.Rows.Add(dv.Table.NewRow())

        objGrid.PagerSettings.Visible = True
        objGrid.DataSource = dv
        objGrid.DataBind()
        objGrid.Rows(0).Visible = False
    End Sub

    Private Sub populateEditingGrids()
        
        State.dvMappingList.RowFilter = "DetailID=" & State.EditingDetailID
        If State.dvMappingList.Item(0)("AFA_PRODUCT_ID") Is DBNull.Value Then
            State.EditingProductID = Guid.Empty
        Else
            State.EditingProductID = New Guid(CType(State.dvMappingList.Item(0)("AFA_PRODUCT_ID"), Byte()))
        End If

        If State.dvMappingList.Item(0)("Loss_type") Is DBNull.Value Then
            State.EditingLossType = String.Empty
        Else
            State.EditingLossType = State.dvMappingList.Item(0)("Loss_type").ToString
        End If

        If State.dvMappingList.Item(0)("count_field_to_use") Is DBNull.Value Then
            State.EditingCntToUse = String.Empty
        Else
            State.EditingCntToUse = State.dvMappingList.Item(0)("count_field_to_use").ToString
        End If

        gridMapping.EditIndex = 0

        PopulateMappingGrid(State.EditingProductID, State.EditingLossType, State.EditingCntToUse, True)
        PopulateFormularGrid(State.EditingProductID, State.EditingLossType, State.EditingCntToUse)
        EnableDisablePageControls()
    End Sub

    Private Function PopulateBOFromForm(ByRef errMsg As Collections.Generic.List(Of String)) As Boolean
        Dim blnSuccess As Boolean = True
        Dim guidProdID As Guid, strLossType As String, strCountField As String

        State.MyParentBO.EntityByRegion = ddlEntityByRegion.SelectedValue
        State.MyParentBO.EntityByRegionCoverageType = ddlEntityByRegionCovType.SelectedValue
        State.MyParentBO.ReconcilWithInvoice = ddlReconWithInvoice.SelectedValue
        State.MyParentBO.UseFormulaForClip = ddlUseFormulaForClip.SelectedValue

        listFormularToSave = New Collections.Generic.List(Of AfaAcctAmtSrcMapping)

        Dim ind As Integer = gridMapping.EditIndex
        Dim ddl As DropDownList = CType(gridMapping.Rows(ind).FindControl(GRID_CTRL_NAME_PRODUCT), DropDownList)
        guidProdID = New Guid(ddl.SelectedValue)
        ddl = Nothing

        ddl = CType(gridMapping.Rows(ind).FindControl(GRID_CTRL_NAME_LOSSTYPE), DropDownList)
        strLossType = ddl.SelectedValue
        ddl = Nothing

        ddl = CType(gridMapping.Rows(ind).FindControl(GRID_CTRL_NAME_COUNTFIELD), DropDownList)
        strCountField = ddl.SelectedValue
        ddl = Nothing

        Dim obj As AfaAcctAmtSrcMapping, strOperation As String, guidBucket As Guid, guidID As Guid
        Dim lbl As Label
        For i As Integer = 0 To gridEditFormular.Rows.Count - 1

            lbl = CType(gridEditFormular.Rows(i).FindControl(GRID_CTRL_NAME_MappingDetailID), Label)
            guidID = New Guid(lbl.Text)

            obj = State.listFormular.Find(Function(r) r.Id = guidID)

            obj.AfaProductId = guidProdID
            obj.LossType = strLossType
            obj.Countfieldtouse = strCountField

            ddl = CType(gridEditFormular.Rows(i).FindControl(GRID_CTRL_NAME_OPERATION), DropDownList)
            obj.Operation = ddl.SelectedValue

            ddl = Nothing

            ddl = CType(gridEditFormular.Rows(i).FindControl(GRID_CTRL_NAME_RATEBUCKET), DropDownList)
            obj.InvRateBucketId = New Guid(ddl.SelectedValue)
            ddl = Nothing

            listFormularToSave.Add(obj)
        Next

        Return blnSuccess
    End Function

    Private Function ValidateChangesBeforeSave() As Boolean
        Dim blnResult As Boolean = True
        '1 validate a formular is not empty
        If Not listFormularToSave.Exists(Function(r) (Not r.Operation Is Nothing) AndAlso (r.Operation <> String.Empty) AndAlso (r.InvRateBucketId <> Guid.Empty)) Then
            blnResult = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("FORMULAR") + ":" + TranslationBase.TranslateLabelOrMessage("EMPTY_FORMULAR"), False)
            Return blnResult
        End If


        '2. validate there is no duplicate (same product/coverage value)
        Dim obj As AfaAcctAmtSrcMapping = listFormularToSave.Find(Function(r) (Not r.Operation Is Nothing) AndAlso (r.Operation <> String.Empty) AndAlso (r.InvRateBucketId <> Guid.Empty))

        If obj.LossType Is Nothing Then
            obj.LossType = String.Empty
        End If

        Dim strFilter As String, guidTemp As Guid, strTemp As String, strCntField As String
        strFilter = "DetailID<>" & State.EditingDetailID
        State.dvMappingList.RowFilter = strFilter

        For Each drv As DataRowView In State.dvMappingList
            If drv("AFA_PRODUCT_ID") Is DBNull.Value Then
                guidTemp = Guid.Empty
            Else
                guidTemp = New Guid(CType(drv("AFA_PRODUCT_ID"), Byte()))
            End If

            If drv("Loss_type") Is DBNull.Value Then
                strTemp = String.Empty
            Else
                strTemp = drv("Loss_type").ToString
            End If

            If drv("count_field_to_use") Is DBNull.Value Then
                strCntField = String.Empty
            Else
                strCntField = drv("count_field_to_use").ToString
            End If

            If obj.LossType = strTemp AndAlso obj.AfaProductId = guidTemp AndAlso obj.Countfieldtouse = strCntField Then 'found duplicate
                blnResult = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("FORMULAR:") + ": " + TranslationBase.TranslateLabelOrMessage("DUPLICATE_FORMULAR_EXIST"), False)
                Exit For
            End If
        Next

        '3. validate operation and bucket, if one has value, another also need to have value
        For Each objTest As AfaAcctAmtSrcMapping In listFormularToSave.FindAll(Function(r) ((Not r.Operation Is Nothing) AndAlso (r.Operation <> String.Empty)) OrElse (r.InvRateBucketId <> Guid.Empty))
            If (objTest.InvRateBucketId <> Guid.Empty AndAlso (objTest.Operation Is Nothing OrElse objTest.Operation = String.Empty)) _
                OrElse (objTest.InvRateBucketId = Guid.Empty AndAlso ((Not objTest.Operation Is Nothing) AndAlso objTest.Operation <> String.Empty)) Then
                blnResult = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("FORMULAR:") + ": " + TranslationBase.TranslateLabelOrMessage("MUST_SELECT_BOTH_OPERATION_AND_BUCKET"), False)
                Exit For
            End If
        Next

        Return blnResult
    End Function

    Private Sub SaveFormular()
        Try
            Dim ErrMsg As New Collections.Generic.List(Of String)
            If PopulateBOFromForm(ErrMsg) Then
                'valiate the records before saving
                If ValidateChangesBeforeSave() = False Then
                    MasterPage.MessageController.Show()
                    Exit Sub
                End If

                'save the parent reord first
                If State.MyParentBO.IsDirty OrElse State.MyParentBO.IsNew Then 'new mapping, save the parent first
                    State.MyParentBO.Save()
                    State.HasDataChanged = True
                End If
                'save the mapping details
                For Each obj As AfaAcctAmtSrcMapping In listFormularToSave
                    If obj.IsNew AndAlso obj.Operation <> String.Empty AndAlso obj.InvRateBucketId <> Guid.Empty Then
                        obj.SaveWithoutCheckDSCreator()
                    End If
                    If obj.IsNew = False Then
                        If obj.Operation = String.Empty AndAlso obj.InvRateBucketId = Guid.Empty Then 'erase existing bucket row
                            obj.Delete()
                            obj.SaveWithoutCheckDSCreator()
                            State.HasDataChanged = True
                        ElseIf obj.IsDirty Then ' update existing bucket row
                            obj.SaveWithoutCheckDSCreator()
                            State.HasDataChanged = True
                        End If
                    End If
                Next

                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                'Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If

            State.CurrentAction = EditingAction.None

            gridMapping.SelectedIndex = -1
            gridMapping.EditIndex = -1


            gridEditFormular.Visible = False
            ClearStateParams()
            PopulateMappingGrid(Guid.Empty, String.Empty, String.Empty)
            EnableDisablePageControls()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClearStateParams()
        State.EditingProductID = Guid.Empty
        State.EditingLossType = String.Empty
        State.EditingDetailID = 0
        State.dvMappingList = Nothing
        State.listFormular = Nothing
        listFormularToSave = Nothing
        State.CurrentAction = EditingAction.None
    End Sub

    Private Sub EnableDisablePageControls()
        If State.CurrentAction = EditingAction.AddNew OrElse State.CurrentAction = EditingAction.EditExisting Then
            btnBack.Enabled = False
            btnSave_WRITE.Enabled = False
        ElseIf State.CurrentAction = EditingAction.AddFirst Then
            btnBack.Enabled = True
            btnSave_WRITE.Enabled = False
        ElseIf State.CurrentAction = EditingAction.None OrElse State.CurrentAction = EditingAction.Delete Then
            btnBack.Enabled = True
            btnSave_WRITE.Enabled = True
        End If

        If State.CurrentAction = EditingAction.EditExisting Then
            ddlEntityByRegion.Enabled = False
            ddlEntityByRegionCovType.Enabled = False
            ddlReconWithInvoice.Enabled = False
            ddlUseFormulaForClip.Enabled = False
        End If
    End Sub

    Private Sub AddNewFormulars()
        Try
            With State.dvMappingList
                Dim objRow As DataRow = .Table.NewRow()
                State.EditingDetailID = 9999
                objRow.Item("DetailID") = 9999
                .Table.Rows.Add(objRow)
            End With

            gridMapping.EditIndex = 0
            populateEditingGrids()
            EnableDisablePageControls()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DeleteFormulars()
        Try
            'validate delete
            State.dvMappingList.RowFilter = "DetailID<>" & State.EditingDetailID
            If State.dvMappingList.Count = 0 Then
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("FIELD_TYPE") + ": " + TranslationBase.TranslateLabelOrMessage("LAST_ENTRY_CAN_NOT_DELETE"), False)
                MasterPage.MessageController.Show()
                State.CurrentAction = EditingAction.None
                Exit Sub
            End If

            State.dvMappingList.RowFilter = "DetailID=" & State.EditingDetailID
            If State.dvMappingList.Item(0)("AFA_PRODUCT_ID") Is DBNull.Value Then
                State.EditingProductID = Guid.Empty
            Else
                State.EditingProductID = New Guid(CType(State.dvMappingList.Item(0)("AFA_PRODUCT_ID"), Byte()))
            End If

            If State.dvMappingList.Item(0)("Loss_type") Is DBNull.Value Then
                State.EditingLossType = String.Empty
            Else
                State.EditingLossType = State.dvMappingList.Item(0)("Loss_type").ToString
            End If

            State.listFormular = AfaAcctAmtSrcMapping.getList(State.MyParentBO.Id, State.EditingProductID, State.EditingLossType, State.EditingCntToUse, True)
            'save the mapping details
            For Each obj As AfaAcctAmtSrcMapping In State.listFormular
                obj.Delete()
                obj.SaveWithoutCheckDSCreator()
                State.HasDataChanged = True
            Next

            Me.MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION, True)

            ClearStateParams()
            PopulateMappingGrid(Guid.Empty, String.Empty, String.Empty)
            EnableDisablePageControls()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'Amount source not mapped yet
    Private Sub NewMappings()
        Try
            State.dvMappingList = AfaAcctAmtSrcMapping.getFormularByProdLossType(State.MyParentBO.Id, Guid.Empty, String.Empty, String.Empty)
            State.CurrentAction = EditingAction.AddFirst
            AddNewFormulars()
            EnableDisablePageControls()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region


#Region "Button event handlers"
    Protected Sub CheckIfComingFromSaveConfirm()

        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Try
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        'SaveFormular()
                        State.MyParentBO.Save()
                        State.HasDataChanged = True
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DeleteFormulars()
                        State.CurrentAction = EditingAction.None
                End Select

            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(Me.State.HasDataChanged)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        State.CurrentAction = EditingAction.None
                End Select
            End If
        Catch ex As Exception
            Throw ex
        Finally
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Try
    End Sub

    Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            With State.MyParentBO
                .EntityByRegion = ddlEntityByRegion.SelectedValue
                .EntityByRegionCoverageType = ddlEntityByRegionCovType.SelectedValue
                .ReconcilWithInvoice = ddlReconWithInvoice.SelectedValue
                .UseFormulaForClip = ddlUseFormulaForClip.SelectedValue
                If .IsDirty AndAlso State.CurrentAction <> EditingAction.AddFirst Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(Me.State.HasDataChanged)
                End If
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            With State.MyParentBO
                .EntityByRegion = ddlEntityByRegion.SelectedValue
                .EntityByRegionCoverageType = ddlEntityByRegionCovType.SelectedValue
                .ReconcilWithInvoice = ddlReconWithInvoice.SelectedValue
                .UseFormulaForClip = ddlUseFormulaForClip.SelectedValue
                If .IsDirty Then
                    .Save()
                    State.HasDataChanged = True
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                End If
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Save
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub
#End Region

#Region "Handle Grids"
    Private Sub gridMapping_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridMapping.RowCommand
        Try
            Select Case e.CommandName.ToString()
                Case "EditRecord"
                    State.EditingDetailID = Integer.Parse(e.CommandArgument.ToString)
                    State.CurrentAction = EditingAction.EditExisting
                    gridMapping.EditIndex = 0
                    populateEditingGrids()
                Case "CancelRecord"
                    State.EditingDetailID = -1
                    State.CurrentAction = EditingAction.None
                    gridMapping.EditIndex = -1
                    State.dvMappingList = Nothing
                    State.listFormular = Nothing
                    PopulateMappingGrid(Guid.Empty, String.Empty, String.Empty)
                    gridEditFormular.Visible = False
                    EnableDisablePageControls()
                Case "SaveRecord"
                    SaveFormular()
                Case "AddRecord"
                    State.CurrentAction = EditingAction.AddNew
                    AddNewFormulars()
                Case "DeleteRecord"
                    State.EditingDetailID = Integer.Parse(e.CommandArgument.ToString)
                    State.CurrentAction = EditingAction.Delete
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    'DeleteFormulars()
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gridMapping_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridMapping.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Private Sub gridMapping_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridMapping.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim objDDL As DropDownList, dv As DataView

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row

                    If .RowIndex = gridMapping.EditIndex Then
                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_PRODUCT), DropDownList)
                        If Not objDDL Is Nothing Then
                            Try
                                dv = AfaAcctAmtSrcMapping.getProductByDealer(State.MyParentBO.DealerId, String.Empty)
                                dv.Sort = "code ASC"

                                For i As Integer = 0 To dv.Count - 1
                                    objDDL.Items.Add(New ListItem(dv(i)("code").ToString & " - " & dv(i)("description").ToString, New Guid(CType(dv(i)("afa_product_id"), Byte())).ToString))
                                Next
                                objDDL.Items.Insert(0, New ListItem("", Guid.Empty.ToString))
                                Me.SetSelectedItem(objDDL, New Guid(CType(dvRow("afa_product_id"), Byte())))
                            Catch ex As Exception
                            End Try
                        End If
                        objDDL = Nothing
                        dv = Nothing

                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_LOSSTYPE), DropDownList)
                        If Not objDDL Is Nothing Then
                            Try
                                dv = ReppolicyClaimCount.GetCoverageTypeListByDealer(State.MyParentBO.DealerId)
                                dv.Sort = "description ASC"

                                For i As Integer = 0 To dv.Count - 1
                                    objDDL.Items.Add(New ListItem(dv(i)("description").ToString, dv(i)("code").ToString))
                                Next
                                objDDL.Items.Insert(0, New ListItem("", ""))
                                Me.SetSelectedItem(objDDL, dvRow("Loss_type").ToString)
                            Catch ex As Exception
                            End Try
                        End If
                        objDDL = Nothing
                        dv = Nothing

                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_COUNTFIELD), DropDownList)
                        If Not objDDL Is Nothing Then
                            Try
                                dv = LookupListNew.DropdownLookupList("CNTFIELDEPRISM", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                                dv.Sort = "description ASC"

                                For i As Integer = 0 To dv.Count - 1
                                    objDDL.Items.Add(New ListItem(dv(i)("description").ToString, dv(i)("code").ToString))
                                Next
                                Me.SetSelectedItem(objDDL, dvRow("count_field_to_use").ToString)
                            Catch ex As Exception
                            End Try
                        End If
                        objDDL = Nothing
                        dv = Nothing

                        If State.CurrentAction = EditingAction.AddFirst Then
                            Dim btn As LinkButton
                            btn = CType(e.Row.FindControl("BtnCancel"), LinkButton)
                            btn.Visible = False
                        End If

                    End If
                End With
            End If
            BaseItemBound(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gridEditFormular_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridEditFormular.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gridEditFormular_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridEditFormular.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As AfaAcctAmtSrcMapping


            If Not e.Row.DataItem Is Nothing Then
                'If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                dvRow = CType(e.Row.DataItem, AfaAcctAmtSrcMapping)
                'edit item, populate dropdown and set value

                Dim objDDL As DropDownList
                'set selected opration value 
                objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_OPERATION), DropDownList)
                If dvRow.Operation <> String.Empty Then
                    Me.SetSelectedItem(objDDL, dvRow.Operation)
                End If
                objDDL = Nothing

                'populate and set rate bucket 
                objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_RATEBUCKET), DropDownList)

                'If dvRateBuckets Is Nothing Then
                '    dvRateBuckets = LookupListNew.DropdownLookupList("IRB", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                'End If
                'BindListControlToDataView(objDDL, dvRateBuckets)

                Dim RateBucketsList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="IRB",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                objDDL.Populate(RateBucketsList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })


                If dvRow.InvRateBucketId <> Guid.Empty Then
                    Me.SetSelectedItem(objDDL, dvRow.InvRateBucketId.ToString)
                End If
                objDDL = Nothing
                'End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region


    
End Class
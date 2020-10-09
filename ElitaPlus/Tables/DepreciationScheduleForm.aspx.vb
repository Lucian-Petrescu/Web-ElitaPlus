Option Strict On
Option Explicit On
Imports Assurant.Common.Zip.aZip
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad
Imports System.IO
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables
    Partial Class DepreciationScheduleForm
        Inherits ElitaPlusSearchPage
#Region "Member Variables"
#End Region
#Region "Properties"
        Private ReadOnly Property TheDepreciationSchedule As DepreciationScd
            Get
                If State.DepreciationScheduleBo Is Nothing Then
                    Select Case State.IsDepreciationScheduleNew
                        Case True
                            ' For creating, inserting
                            State.DepreciationScheduleBo = New DepreciationScd
                        Case Else
                            ' For updating, deleting
                            State.DepreciationScheduleBo = New DepreciationScd(State.DepreciationScheduleId)
                    End Select
                End If
                Return State.DepreciationScheduleBo
            End Get
        End Property
#End Region

#Region "Page State"
        Class MyState
            Public MyBo As DepreciationScdDetails
            Public DepreciationScheduleBo As DepreciationScd
            Public IsDepreciationScheduleNew As Boolean = False

            Public PageIndex As Integer = 0
            Public CompanyId As Guid = Guid.Empty
            Public DepreciationScheduleId As Guid = Guid.Empty
            Public LowMonth As Integer
            Public HighMonth As Integer
            Public Percent As Integer
            Public Amount As Integer
            Public Id As Guid 'DEPRECIATION_SCHEDULE_ITEM_ID
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean = False
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public SearchDv As DataView = Nothing
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public SortExpression As String = DepreciationScdDetails.DepreciationScheduleDV.ColLowMonth
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

#End Region
#Region "Constants"

        Public Const Url As String = "~/Tables/DepreciationScheduleForm.aspx"

        Private Const IdCol As Integer = 2
        Private Const LowMonthCol As Integer = 3
        Private Const HighMonthCol As Integer = 4
        Private Const PercentCol As Integer = 5
        Private Const AmountCol As Integer = 6
        Private Const DepreciationScheduleIdCol As Integer = 7


        Private Const LowMonthControlName As String = "moLowMonthText"
        Private Const IdControlName As String = "moDepreciationScheduleItemIDlabel"
        Private Const HighMonthControlName As String = "moHighMonthText"
        Private Const PercentControlName As String = "moPercentText"
        Private Const AmountControlName As String = "moAmountText"
        Private Const DepreciationScheduleIdControlName As String = "moDepreciationScheduleIDlabel"

        Private Const HighMonthPropertyName As String = "High_Month"
        Private Const DepreciationScheduleCodePropertyName As String = "Code"
        Private Const DepreciationScheduleDescriptionPropertyName As String = "Description"
        Private Const DepreciationScheduleCompanyidPropertyName As String = "CompanyId"
        Private Const DepreciationScheduleActiveXcdPropertyName As String = "ActiveXcd"

        Private Const EditCommand As String = "EditRecord"
        Private Const DeleteCommand As String = "DeleteRecord"

        Private Const NoRowSelectedIndex As Integer = -1

        Private Const LabelCompany As String = "COMPANY"
        Private Const DepreciationScheduleForm001 As String = "DEPRECIATION_SCHEDULE_FORM001"  ' Error while populating company

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public CompanyBo As Company
            Public HasDataChanged As Boolean

            Public Sub New(lastOp As DetailPageCommand, hasDataChanged As Boolean)
                Me.New(lastOp, Nothing, hasDataChanged)
            End Sub

            Public Sub New(lastOp As DetailPageCommand, companyBo As Company, hasDataChanged As Boolean)
                LastOperation = lastOp
                Me.CompanyBo = companyBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page_Events"

        Private Sub Page_PageCall(callFromUrl As String, callingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.DepreciationScheduleId = CType(CallingParameters, Guid)
                    If State.DepreciationScheduleId.Equals(Guid.Empty) Then
                        State.IsDepreciationScheduleNew = True
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                ErrorControllerDS.Clear_Hide()

                If Not Page.IsPostBack Then

                    UpdateBreadCrum()

                    If State.MyBo Is Nothing Then
                        State.MyBo = New DepreciationScdDetails
                    End If

                    txtDepreciationScheduleCode.Text = TheDepreciationSchedule.Code
                    txtDepreciationScheduleDescription.Text = TheDepreciationSchedule.Description

                    PopulateDropDown()

                    SetButtonsState()
                    SetFieldsState()

                    TranslateGridHeader(DepSchDetailsGridView)
                    SetGridItemStyleColor(DepSchDetailsGridView)
                    State.PageIndex = 0
                    PopulateGrid()

                End If
                BindBoPropertiesToLabels()
                ClearLabelsErrSign()
                BindBoPropertiesToGridHeaders()
                ClearGridViewHeadersAndLabelsErrorSign()
                AddLabelDecorations(TheDepreciationSchedule)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Private Methods"


        Private Sub UpdateBreadCrum()
            Dim pageTitle As String = TranslationBase.TranslateLabelOrMessage("DEPRECIATION_SCHEDULE")
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("Tables") & ElitaBase.Sperator & pageTitle
            MasterPage.PageTitle = pageTitle
            MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub
        Private Sub PopulateDropDown()
            PopulateCompany()
            PopulateDsStatus()
        End Sub
        Private Sub PopulateCompany()
            Try
                CompanyMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LabelCompany)
                CompanyMultipleDrop.AutoPostBackDD = False
                CompanyMultipleDrop.NothingSelected = False
                CompanyMultipleDrop.BindData(LookupListNew.GetUserCompaniesLookupList())

                State.CompanyId = TheDepreciationSchedule.CompanyId
                If Not State.CompanyId.Equals(Guid.Empty) Then
                    CompanyMultipleDrop.SelectedGuid = State.CompanyId
                End If

            Catch ex As Exception
                MasterPage.MessageController.AddError(DepreciationScheduleForm001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub
        Private Sub PopulateDsStatus()
            Dim yesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            ddlDepreciationScheduleActive.Populate(yesNoList, New PopulateOptions() With
                {
                    .AddBlankItem = False,
                    .TextFunc = AddressOf .GetDescription,
                    .ValueFunc = AddressOf .GetExtendedCode,
                    .SortFunc = AddressOf .GetDescription
                })

            'ddlDepreciationScheduleActive.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)

            If TheDepreciationSchedule.ActiveXcd IsNot Nothing AndAlso Not String.IsNullOrEmpty(TheDepreciationSchedule.ActiveXcd) Then
                SetSelectedItem(ddlDepreciationScheduleActive, TheDepreciationSchedule.ActiveXcd)
            Else
                SetSelectedItem(ddlDepreciationScheduleActive, "YESNO-Y")
            End If
        End Sub
        Private Sub PopulateGrid()
            Dim dv As DataView
            Dim maxHighMonth As Long

            Try
                If (State.SearchDv Is Nothing) Then
                    State.SearchDv = DepreciationScdDetails.LoadList(State.DepreciationScheduleId)
                End If

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.SearchDv, State.Id, DepSchDetailsGridView, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.SearchDv, State.Id, DepSchDetailsGridView, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.SearchDv, Guid.Empty, DepSchDetailsGridView, State.PageIndex)
                End If

                DepSchDetailsGridView.AutoGenerateColumns = False
                BindGrid()

                dv = State.SearchDv
                dv.Sort = HighMonthPropertyName + " ASC"

                If (dv.Count > 0) Then
                    If Not dv(dv.Count - 1)(HighMonthPropertyName).Equals(DBNull.Value) Then
                        maxHighMonth = CType(dv(dv.Count - 1)(HighMonthPropertyName), Long)
                        DisableDelControl(DepSchDetailsGridView, maxHighMonth)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub BindGrid()
            DepSchDetailsGridView.DataSource = State.SearchDv
            DepSchDetailsGridView.DataBind()
            ControlMgr.SetVisibleControl(Me, DepSchDetailsGridView, True)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, DepSchDetailsGridView)
        End Sub

        Private Sub DisableDelControl(grid As GridView, maxHighMonth As Long)
            Dim i As Integer
            Dim del As ImageButton
            Dim highMonth As Label

            For i = 0 To (grid.Rows.Count - 1)
                del = CType(grid.Rows(i).Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
                highMonth = CType(grid.Rows(i).Cells(HighMonthCol).FindControl("moHighMonthLabel"), Label)
                If del IsNot Nothing AndAlso highMonth IsNot Nothing Then
                    If CType(highMonth.Text, Long) <> maxHighMonth Then
                        del.Enabled = False
                        del.Visible = False
                    Else
                        del.Enabled = True
                        del.Visible = True
                    End If
                End If
            Next

        End Sub

        Private Function GetGridDataView() As DataView
            Return (DepreciationScdDetails.LoadList(State.DepreciationScheduleId))
        End Function

        Private Sub AddNew()
            State.SearchDv = GetGridDataView()

            State.MyBo = New DepreciationScdDetails
            State.Id = State.MyBo.Id
            State.MyBo.DepreciationScheduleId = State.DepreciationScheduleId

            State.SearchDv = State.MyBo.GetNewDataViewRow(State.SearchDv, State.Id, State.MyBo)

            DepSchDetailsGridView.DataSource = State.SearchDv
            SetPageAndSelectedIndexFromGuid(State.SearchDv, State.Id, DepSchDetailsGridView, State.PageIndex, State.IsEditMode)
            DepSchDetailsGridView.DataBind()

            SetGridControls(DepSchDetailsGridView, False)

            'Set focus on the Low Month TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(LowMonthCol, LowMonthControlName, DepSchDetailsGridView.EditIndex)

            AssignSelectedRecordFromBo()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, DepSchDetailsGridView)
        End Sub

        Private Sub AssignBoFromSelectedRecord()

            If State.MyBo.DepreciationScheduleId.Equals(Guid.Empty) Then
                PopulateBOProperty(State.MyBo, "DepreciationScheduleId", State.DepreciationScheduleId)
            End If
            PopulateBOProperty(State.MyBo, "LowMonth", CType(GetSelectedGridControl(DepSchDetailsGridView, LowMonthCol), TextBox))
            PopulateBOProperty(State.MyBo, "HighMonth", CType(GetSelectedGridControl(DepSchDetailsGridView, HighMonthCol), TextBox))
            PopulateBOProperty(State.MyBo, "Percent", CType(GetSelectedGridControl(DepSchDetailsGridView, PercentCol), TextBox))
            PopulateBOProperty(State.MyBo, "Amount", CType(GetSelectedGridControl(DepSchDetailsGridView, AmountCol), TextBox))

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Private Sub AssignSelectedRecordFromBo()

            Dim gridRowIdx As Integer = DepSchDetailsGridView.EditIndex
            Try
                With State.MyBo
                    If .LowMonth IsNot Nothing Then
                        CType(DepSchDetailsGridView.Rows(gridRowIdx).Cells(LowMonthCol).FindControl(LowMonthControlName), TextBox).Text = .LowMonth.ToString()
                    End If
                    If .HighMonth IsNot Nothing Then
                        CType(DepSchDetailsGridView.Rows(gridRowIdx).Cells(HighMonthCol).FindControl(HighMonthControlName), TextBox).Text = .HighMonth.ToString()
                    End If
                    If .Percent IsNot Nothing Then
                        CType(DepSchDetailsGridView.Rows(gridRowIdx).Cells(PercentCol).FindControl(PercentControlName), TextBox).Text = .Percent.ToString()
                    End If
                    If .Amount IsNot Nothing Then
                        CType(DepSchDetailsGridView.Rows(gridRowIdx).Cells(AmountCol).FindControl(AmountControlName), TextBox).Text = .Amount.ToString()
                    End If
                    If Not .DepreciationScheduleId.Equals(Guid.Empty) Then
                        CType(DepSchDetailsGridView.Rows(gridRowIdx).Cells(DepreciationScheduleIdCol).FindControl(DepreciationScheduleIdControlName), Label).Text = GuidControl.GuidToHexString(.DepreciationScheduleId)
                    ElseIf Not State.DepreciationScheduleId.Equals(Guid.Empty) Then
                        CType(DepSchDetailsGridView.Rows(gridRowIdx).Cells(DepreciationScheduleIdCol).FindControl(DepreciationScheduleIdControlName), Label).Text = GuidControl.GuidToHexString(State.DepreciationScheduleId)
                    End If

                    CType(DepSchDetailsGridView.Rows(gridRowIdx).Cells(IdCol).FindControl(IdControlName), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ReturnFromEditing()
            DepSchDetailsGridView.EditIndex = NoRowSelectedIndex
            If DepSchDetailsGridView.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, DepSchDetailsGridView, False)
            Else
                ControlMgr.SetVisibleControl(Me, DepSchDetailsGridView, True)
            End If
            State.IsEditMode = False
            PopulateGrid()
            SetButtonsState()
            SetFieldsState()
        End Sub

        Private Sub SetButtonsState()
            If (State.IsEditMode) Then
                ControlMgr.SetEnableControl(Me, btnSaveDepScheduleItem, True)
                ControlMgr.SetEnableControl(Me, btnCancelDepScheduleItem, True)
                ControlMgr.SetEnableControl(Me, btnNewDepScheduleItem, False)

                ' Lower Button
                ControlMgr.SetEnableControl(Me, btnBack, False)
                ControlMgr.SetEnableControl(Me, btnApply, False)
                ControlMgr.SetEnableControl(Me, btnNew, False)
                ControlMgr.SetEnableControl(Me, btnCopy, False)
                MenuEnabled = False
            Else
                ControlMgr.SetEnableControl(Me, btnSaveDepScheduleItem, False)
                ControlMgr.SetEnableControl(Me, btnCancelDepScheduleItem, False)

                If State.IsDepreciationScheduleNew Then
                    ControlMgr.SetEnableControl(Me, btnNewDepScheduleItem, False)
                    ' lower button
                    ControlMgr.SetEnableControl(Me, btnBack, True)
                    ControlMgr.SetEnableControl(Me, btnApply, True)
                    ControlMgr.SetEnableControl(Me, btnNew, False)
                    ControlMgr.SetEnableControl(Me, btnCopy, False)
                    MenuEnabled = False
                Else
                    ControlMgr.SetEnableControl(Me, btnNewDepScheduleItem, True)
                    ' lower button
                    ControlMgr.SetEnableControl(Me, btnBack, True)
                    ControlMgr.SetEnableControl(Me, btnApply, True)
                    ControlMgr.SetEnableControl(Me, btnNew, True)
                    ControlMgr.SetEnableControl(Me, btnCopy, True)
                    MenuEnabled = True
                End If
            End If
        End Sub
        Private Sub SetFieldsState()
            If State.IsDepreciationScheduleNew Then
                CompanyMultipleDrop.ChangeEnabledControlProperty(True)
                ControlMgr.SetEnableControl(Me, txtDepreciationScheduleCode, True)
                ControlMgr.SetEnableControl(Me, txtDepreciationScheduleDescription, True)
            Else
                CompanyMultipleDrop.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, txtDepreciationScheduleCode, False)
                ControlMgr.SetEnableControl(Me, txtDepreciationScheduleDescription, False)
            End If
        End Sub

#End Region

#Region "Button Click Handlers"

        Private Sub btnNewDepScheduleItem_Click(sender As Object, e As EventArgs) Handles btnNewDepScheduleItem.Click
            Try
                State.IsEditMode = True
                State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSaveDepScheduleItem_Click(sender As Object, e As EventArgs) Handles btnSaveDepScheduleItem.Click

            Try
                AssignBoFromSelectedRecord()

                If (State.MyBo.IsDirty) Then
                    State.MyBo.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    State.SearchDv = Nothing
                    ReturnFromEditing()
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    ReturnFromEditing()
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                ' HandleErrors(ex, ErrorControllerDS)
                HandleErrors(ex, MasterPage.MessageController)

            End Try

        End Sub

        Private Sub btnCancelDepScheduleItem_Click(sender As Object, e As EventArgs) Handles btnCancelDepScheduleItem.Click

            Try
                DepSchDetailsGridView.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.SearchDv = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                ReturnToCallingPage(New PageReturnType(Of Object)(DetailPageCommand.Back, Nothing, True))
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            Try
                State.IsDepreciationScheduleNew = True
                State.DepreciationScheduleId = Guid.Empty
                State.DepreciationScheduleBo = Nothing
                State.SearchDv = Nothing

                txtDepreciationScheduleCode.Text = ""
                txtDepreciationScheduleDescription.Text = ""
                PopulateDropDown()
                PopulateGrid()
                State.IsEditMode = False
                State.AddingNewRow = False
                SetButtonsState()
                SetFieldsState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
            Try
                Dim copyCompanyId As Guid = State.CompanyId

                State.IsDepreciationScheduleNew = True
                State.DepreciationScheduleBo = Nothing

                txtDepreciationScheduleCode.Text = ""
                txtDepreciationScheduleDescription.Text = ""
                PopulateDropDown()
                CompanyMultipleDrop.SelectedGuid = copyCompanyId

                'Disable all Edit and Delete icon buttons on the DepSchDetailsGridView
                SetGridControls(DepSchDetailsGridView, False)

                SetButtonsState()
                SetFieldsState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
            Try

                Dim copyId As Guid = State.DepreciationScheduleId

                PopulateBOsFromForm()
                If TheDepreciationSchedule.IsDirty Then
                    TheDepreciationSchedule.Save()
                    State.DepreciationScheduleId = TheDepreciationSchedule.Id

                    If State.IsDepreciationScheduleNew Then
                        Dim dv As DataView = DepreciationScdDetails.LoadList(copyId)
                        Dim dt As DataTable = dv.Table

                        For Each row As DataRow In dt.Rows
                            State.MyBo = New DepreciationScdDetails
                            State.Id = State.MyBo.Id
                            State.MyBo.DepreciationScheduleId = State.DepreciationScheduleId
                            State.MyBo.LowMonth = CType(row(DepreciationScdDetails.LowMonthColName), Long)
                            State.MyBo.HighMonth = CType(row(DepreciationScdDetails.HighMonthColName), Long)

                            If (row(DepreciationScdDetails.PercentColName).Equals(DBNull.Value)) Then
                                State.MyBo.Percent = Nothing
                            Else
                                State.MyBo.Percent = CType(row(DepreciationScdDetails.PercentColName), Long)
                            End If

                            If (row(DepreciationScdDetails.AmountColName).Equals(DBNull.Value)) Then
                                State.MyBo.Amount = Nothing
                            Else
                                State.MyBo.Amount = CType(row(DepreciationScdDetails.AmountColName), Long)
                            End If

                            State.MyBo.Save()
                        Next
                        State.IsDepreciationScheduleNew = False
                    End If

                    State.IsAfterSave = True
                    State.SearchDv = Nothing
                    ReturnFromEditing()
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBo, "LowMonth", DepSchDetailsGridView.Columns(LowMonthCol))
            BindBOPropertyToGridHeader(State.MyBo, "HighMonth", DepSchDetailsGridView.Columns(HighMonthCol))
            BindBOPropertyToGridHeader(State.MyBo, "Amount", DepSchDetailsGridView.Columns(AmountCol))
            BindBOPropertyToGridHeader(State.MyBo, "Percent", DepSchDetailsGridView.Columns(PercentCol))

        End Sub
        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.DepreciationScheduleBo, "Code", lblDepreciationScheduleCode)
            BindBOPropertyToLabel(State.DepreciationScheduleBo, "Description", lblDepreciationScheduleDescription)
            BindBOPropertyToLabel(State.DepreciationScheduleBo, "ActiveXcd", lblDepreciationScheduleActive)
        End Sub
        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(lblDepreciationScheduleCode)
            ClearLabelErrSign(lblDepreciationScheduleDescription)
            ClearLabelErrSign(lblDepreciationScheduleActive)
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Low Month TextBox for the EditItemIndex row
            Dim lowMonth As TextBox = CType(DepSchDetailsGridView.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(lowMonth)
        End Sub

        Protected Sub PopulateBOsFromForm()
            With TheDepreciationSchedule
                PopulateBOProperty(TheDepreciationSchedule, DepreciationScheduleCodePropertyName, txtDepreciationScheduleCode)
                PopulateBOProperty(TheDepreciationSchedule, DepreciationScheduleDescriptionPropertyName, txtDepreciationScheduleDescription)
                PopulateBOProperty(TheDepreciationSchedule, DepreciationScheduleCompanyidPropertyName, CompanyMultipleDrop.CodeDropDown)
                PopulateBOProperty(TheDepreciationSchedule, DepreciationScheduleActiveXcdPropertyName, ddlDepreciationScheduleActive, False, True)
            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
#End Region

#Region " Datagrid Related "
        Protected Sub DepSchDetailsGridView_RowCommand(source As Object, e As GridViewCommandEventArgs)

            Try
                Dim index As Integer = CInt(e.CommandArgument)

                If (e.CommandName = EditCommand) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.Id = New Guid(CType(DepSchDetailsGridView.Rows(index).Cells(IdCol).FindControl(IdControlName), Label).Text)
                    State.MyBo = New DepreciationScdDetails(State.Id)

                    PopulateGrid()

                    'Disable all Edit and Delete icon buttons on the DepSchDetailsGridView
                    SetGridControls(DepSchDetailsGridView, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(LowMonthCol, LowMonthControlName, index)

                    AssignSelectedRecordFromBo()
                    SetButtonsState()
                ElseIf (e.CommandName = DeleteCommand) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    DepSchDetailsGridView.SelectedIndex = NoRowSelectedIndex

                    'Save the Id in the Session

                    State.Id = New Guid(CType(DepSchDetailsGridView.Rows(index).Cells(IdCol).FindControl(IdControlName), Label).Text)
                    State.MyBo = New DepreciationScdDetails(State.Id)

                    Try
                        State.MyBo.Delete()
                        State.MyBo.Save()
                    Catch ex As Exception
                        State.MyBo.RejectChanges()
                        Throw
                    End Try

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    State.IsAfterSave = True
                    State.SearchDv = Nothing
                    PopulateGrid()
                    MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub DepSchDetailsGridView_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles DepSchDetailsGridView.RowCreated
            BaseItemCreated(sender, e)
        End Sub

#End Region
    End Class


End Namespace

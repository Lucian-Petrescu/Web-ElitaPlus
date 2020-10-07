Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Translation
    Public Class AdminMaintainDropdownByEntityForm
        Inherits ElitaPlusSearchPage


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants "
        Protected Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected dropdowns?"

        Public Const GRID_COL_LIST_CODE As Integer = 0
        Public Const GRID_COL_LIST_DESCRIPTION As Integer = 1
        Private Const GRID_COL_ENTITY_TYPE_CODE As Integer = 2
        Private Const GRID_COL_ENTITY_TYPE_DESCRIPTION As Integer = 2
        Private Const GRID_COL_ENTITY_REFERENCE_ID As Integer = 2
        Private Const GRID_COL_ENTITY_CODE As Integer = 3
        Private Const GRID_COL_ENTITY_DESCRIPTION As Integer = 4
        Private Const ITEMS_CIDX As Integer = 5
        Private Const SELECTED_CIDX As Integer = 0
        Private Const SEARCH_FLAG As String = "D"

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public searchDV As DataView = Nothing
            Public ListId As Guid = Guid.Empty
            Public dropdownByEntityBO As ListItemByEntity
            Public SortExpression As String = String.Empty

        End Class

        Public Sub New()
            MyBase.New(New MyState())
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "Page Load "
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here

            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    ShowMissingTranslations(ErrorControl)
                    ' Populate the header and bredcrumb
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    ' Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY")
                    UpdateBreadCrum()
                    PopulateListItem()
                    TranslateGridHeader(GridDropdownsByEntity)
                    If State.searchDV IsNot Nothing Then
                        If State.dropdownByEntityBO IsNot Nothing AndAlso State.dropdownByEntityBO.SearchType IsNot Nothing Then
                            PopulateBoFromForm()
                        End If
                        PopulateGrid()
                    End If

                End If
                If IsPostBack Then

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Datagrid Related "
        Protected Sub PopulateGrid()

            Dim oLanguageCode As String = Codes.ENGLISH_LANG_CODE
            Dim sortBy As String = String.Empty
            If (State.searchDV Is Nothing) Then
                sortBy = String.Empty
            End If

            State.searchDV = ListItemByEntity.LoadEntityList(oLanguageCode, State.dropdownByEntityBO.EntityReferenceId, State.dropdownByEntityBO.ListCode, State.dropdownByEntityBO.EntityReference, State.SortExpression)

            GridDropdownsByEntity.AutoGenerateColumns = False
            GridDropdownsByEntity.DataSource = State.searchDV
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            HighLightSortColumn(GridDropdownsByEntity, State.SortExpression)


            GridDropdownsByEntity.DataBind()
        End Sub
        Protected Sub GridDropdownsByEntity_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridDropdownsByEntity.RowDataBound

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim entityType As TextBox
                entityType = CType(e.Row.FindControl("TextBoxEntityType"), TextBox)
                entityType.Text = TranslationBase.TranslateLabelOrMessage(entityType.Text)
            End If



        End Sub
        Public Sub GridDropdowns_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDropdownsByEntity.RowCommand
            Try

                If e.CommandName = "ViewEdit" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Dim searchFlag As String = Nothing
                    Dim listCode As String = CType(GridDropdownsByEntity.Rows(index).Cells(GRID_COL_LIST_CODE).FindControl("TextBoxListCode"), TextBox).Text
                    Dim listDescription As String = CType(GridDropdownsByEntity.Rows(index).Cells(GRID_COL_LIST_DESCRIPTION).FindControl("TextBoxListDescription"), TextBox).Text
                    Dim entity_Type_Code As String = CType(GridDropdownsByEntity.Rows(index).Cells(GRID_COL_ENTITY_TYPE_CODE).FindControl("LabelEntityTypeCode"), Label).Text
                    Dim entityTypeDescription As String = CType(GridDropdownsByEntity.Rows(index).Cells(GRID_COL_ENTITY_TYPE_DESCRIPTION).FindControl("TextBoxEntityType"), TextBox).Text
                    Dim entityCode As String = CType(GridDropdownsByEntity.Rows(index).Cells(GRID_COL_ENTITY_CODE).FindControl("TextBoxEntityCode"), TextBox).Text
                    Dim entityDescription As String = CType(GridDropdownsByEntity.Rows(index).Cells(GRID_COL_ENTITY_DESCRIPTION).FindControl("TextBoxEntityDescription"), TextBox).Text
                    Dim entityReferenceId As New Guid(CType(GridDropdownsByEntity.Rows(index).Cells(GRID_COL_ENTITY_TYPE_CODE).FindControl("LabelEntityRefId"), Label).Text)
                    State.dropdownByEntityBO = New ListItemByEntity()
                    Dim entityRefId As Guid
                    If moDropdownList.Text.Trim = String.Empty Then
                        searchFlag = SEARCH_FLAG
                    End If


                    With State.dropdownByEntityBO
                        .ListCode = listCode
                        .ListDescription = listDescription
                        .EntityReferenceId = entityReferenceId
                        .EntityDescription = entityDescription
                        .EntityType = entityTypeDescription
                        .EntityReference = entity_Type_Code
                        .SearchType = searchFlag
                    End With


                    callPage(AdminMaintainDropdownByEntityDetailsForm.PAGE_NAME, BuildParameters)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub GridDropdownsByEntity_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridDropdownsByEntity.PageIndexChanging
            Try
                GridDropdownsByEntity.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridDropdownsByEntity.RowCreated


            BaseItemCreated(sender, e)
        End Sub
        Private Sub GridDropdownsByEntity_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridDropdownsByEntity.Sorting
            Try
                If State.SortExpression.StartsWith(e.SortExpression) Then
                    If State.SortExpression.EndsWith(" DESC") Then
                        State.SortExpression = e.SortExpression
                    Else
                        State.SortExpression &= " DESC"
                    End If
                Else
                    State.SortExpression = e.SortExpression
                End If
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            ErrorControl.Clear_Hide()
            Try

                If moDropdownList IsNot Nothing AndAlso moDropdownList.Text.Trim = String.Empty Then
                    PopulateListItem()
                End If
                PopulateBoFromForm()
                PopulateGrid()

                ErrorControl.Show()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try

                If moDropdownList IsNot Nothing AndAlso Not moDropdownList.Text.Trim = String.Empty Then

                    moDropdownEntityType.Items.Clear()
                    moDropdownEntity.Items.Clear()
                    PopulateListItem()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub bntAdd_Click(sender As System.Object, e As System.EventArgs) Handles bntAdd.Click
            'ErrorControl.Clear_Hide()
            Try
                ValidateForm()
                PopulateBoFromForm()
                Dim oLanguageCode As String = Codes.ENGLISH_LANG_CODE
                State.searchDV = ListItemByEntity.LoadEntityList(oLanguageCode, State.dropdownByEntityBO.EntityReferenceId, State.dropdownByEntityBO.ListCode, State.dropdownByEntityBO.EntityReference, State.SortExpression)

                If State.searchDV.Count > 0 Then
                    Throw New GUIException(Message.MSG_VALUE_ALREADY_IN_USE, Assurant.ElitaPlus.Common.ErrorCodes.ENTITY_LIST_ALREADY_EXISTS_ERR)
                Else
                    callPage(AdminMaintainDropdownByEntityDetailsForm.PAGE_NAME, BuildParameters)
                End If


                'Me.ErrorControl.Show()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
            ErrorControl.Clear_Hide()
            Try
                Dim i As Integer
                Dim deleteCount As Integer = 0
                Dim TotalChecked As Integer = 0
                Dim retVal As Integer
                State.dropdownByEntityBO = New ListItemByEntity()

                For i = 0 To GridDropdownsByEntity.Rows.Count - 1
                    If CType(GridDropdownsByEntity.Rows(i).Cells(SELECTED_CIDX).FindControl("CheckBoxItemSel"), CheckBox).Checked Then
                        TotalChecked += 1
                        Dim listCode As String = CType(GridDropdownsByEntity.Rows(i).Cells(GRID_COL_LIST_CODE).FindControl("TextBoxListCode"), TextBox).Text
                        Dim entityReferenceId As New Guid(CType(GridDropdownsByEntity.Rows(i).Cells(GRID_COL_ENTITY_REFERENCE_ID).FindControl("LabelEntityRefId"), Label).Text)

                        Try
                            retVal = State.dropdownByEntityBO.DeleteDropdown(listCode, entityReferenceId)
                            If retVal = 0 Then
                                deleteCount += 1
                            Else

                                ErrorControl.AddError(Message.ERR_DELETING_DATA)
                            End If
                        Catch ex As Exception

                            DisplayMessage(Message.ERR_DELETING_DATA, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        End Try
                    End If
                Next
                If (deleteCount > 0) Then
                    PopulateBoFromForm()
                    PopulateGrid()
                End If

                Dim ProcessingResultMsg As String = TranslationBase.TranslateLabelOrMessage(Message.BATCH_DELETE_PROCESS)
                ProcessingResultMsg = ProcessingResultMsg.Replace("?1", deleteCount.ToString)
                ProcessingResultMsg = ProcessingResultMsg.Replace("?2", TotalChecked.ToString)
                AddInfoMsg(ProcessingResultMsg, False)
                ErrorControl.Show()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("ADMIN") & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY")
                End If
            End If
        End Sub
        Public Sub ValidateForm()

            If moDropdownList.Text.Trim = String.Empty Then
                'display error
                ElitaPlusPage.SetLabelError(lblDropdownList)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR, TranslationBase.TranslateLabelOrMessage("DROPDOWN_LIST") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            Else
                SetLabelColor(lblDropdownList)
            End If
            If moDropdownEntityType.Text.Trim = String.Empty Then
                'display error
                ElitaPlusPage.SetLabelError(LabelEntityType)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR, TranslationBase.TranslateLabelOrMessage("ENTITY_TYPE") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            Else
                SetLabelColor(LabelEntityType)
            End If

            If moDropdownEntity.Text.Trim = String.Empty Then
                'display error
                ElitaPlusPage.SetLabelError(LabelEntity)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR, TranslationBase.TranslateLabelOrMessage("ENTITY") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            Else
                SetLabelColor(LabelEntity)
            End If


        End Sub
        Public Sub PopulateBoFromForm()

            State.dropdownByEntityBO = New ListItemByEntity()
            Dim dvList As DataView
            If moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY_GROUP" Then

                dvList = LookupListNew.GetCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            ElseIf moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY" Then

                dvList = LookupListNew.GetCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            ElseIf moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER_GROUP" Then

                dvList = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            ElseIf moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER" Then
                dvList = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            End If

            Dim entityRefId As Guid
            If moDropdownEntity.Text.Trim = String.Empty Then
                entityRefId = Guid.Empty

            Else
                entityRefId = LookupListNew.GetIdFromCode(dvList, moDropdownEntity.SelectedValue)

            End If

            With State.dropdownByEntityBO
                .ListCode = moDropdownList.SelectedValue.Trim
                .ListDescription = moDropdownList.SelectedItem.Text.Trim
                .EntityReferenceId = entityRefId
                .EntityDescription = moDropdownEntity.SelectedItem.Text.Trim
                .EntityType = moDropdownEntityType.SelectedItem.Text.Trim
                .EntityReference = moDropdownEntityType.SelectedValue.Trim

            End With


        End Sub

        Public Shared Sub SetLabelColor(lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

#End Region

#Region "Controlling Dropdown"
        Private Sub PopulateListItem()
            Dim EngLangId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetLanguageLookupList(), Codes.ENGLISH_LANG_CODE)
            Dim dvList As DataView = LookupListNew.AllDropdownLanguageLookupLists(EngLangId)
            Dim i As Integer
            moDropdownList.Items.Clear()
            moDropdownList.Items.Add(New ListItem("", ""))
            If dvList IsNot Nothing Then
                For i = 0 To dvList.Count - 1
                    moDropdownList.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                Next
            End If
            PopulateEntityList()
        End Sub
        Private Sub PopulateEntityList()
            moDropdownEntityType.Items.Clear()
            moDropdownEntityType.Items.Add(New ListItem("", ""))
            If (moDropdownList.SelectedIndex > 0) Then
                moDropdownEntityType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("ELP_COMPANY_GROUP"), "ELP_COMPANY_GROUP"))
                moDropdownEntityType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("ELP_COMPANY"), "ELP_COMPANY"))
                moDropdownEntityType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("ELP_DEALER"), "ELP_DEALER"))
                moDropdownEntityType.Items.Add(New ListItem(TranslationBase.TranslateLabelOrMessage("ELP_DEALER_GROUP"), "ELP_DEALER_GROUP"))
            End If
            PopulateDefaultEntity()
        End Sub
        Private Sub PopulateDefaultEntity()
            moDropdownEntity.Items.Clear()
            moDropdownEntity.Items.Add(New ListItem("", ""))
        End Sub
        Protected Sub moDropdownEntityType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moDropdownEntityType.SelectedIndexChanged

            If moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY_GROUP" Then

                '  Dim dvList As DataView = LookupListNew.GetCompanyGroupCountryLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

                Dim dvList As DataView = LookupListNew.GetCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If dvList IsNot Nothing Then
                    For i = 0 To dvList.Count - 1
                        moDropdownEntity.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                    Next
                End If

            ElseIf moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY" Then

                Dim dvList As DataView = LookupListNew.GetCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If dvList IsNot Nothing Then
                    For i = 0 To dvList.Count - 1
                        moDropdownEntity.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                    Next
                End If

            ElseIf moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER_GROUP" Then

                Dim dvList As DataView = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If dvList IsNot Nothing Then
                    For i = 0 To dvList.Count - 1
                        moDropdownEntity.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                    Next
                End If

            ElseIf moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER" Then
                Dim dvList As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If dvList IsNot Nothing Then
                    For i = 0 To dvList.Count - 1
                        moDropdownEntity.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                    Next
                End If

            End If

        End Sub

        Protected Sub moDropdownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moDropdownList.SelectedIndexChanged
            If (moDropdownList.SelectedValue.Length > 0) Then
                PopulateEntityList()
            End If
        End Sub

#End Region

#Region "Parameter Logic"
        Function BuildParameters() As Translation.AdminMaintainDropdownByEntityDetailsForm.Parameters
            Dim dropdownByEntityBO As ListItemByEntity = State.dropdownByEntityBO
            Return New Translation.AdminMaintainDropdownByEntityDetailsForm.Parameters(dropdownByEntityBO)
        End Function

#End Region

    End Class

End Namespace
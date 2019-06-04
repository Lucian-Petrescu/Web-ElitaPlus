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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here

            Try
                If Not Me.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.ShowMissingTranslations(ErrorControl)
                    ' Populate the header and bredcrumb
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    ' Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY")
                    UpdateBreadCrum()
                    PopulateListItem()
                    Me.TranslateGridHeader(Me.GridDropdownsByEntity)
                    If Not Me.State.searchDV Is Nothing Then
                        If Not Me.State.dropdownByEntityBO Is Nothing AndAlso Not Me.State.dropdownByEntityBO.SearchType Is Nothing Then
                            PopulateBoFromForm()
                        End If
                        PopulateGrid()
                    End If

                End If
                If Me.IsPostBack Then

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Datagrid Related "
        Protected Sub PopulateGrid()

            Dim oLanguageCode As String = Codes.ENGLISH_LANG_CODE
            Dim sortBy As String = String.Empty
            If (Me.State.searchDV Is Nothing) Then
                sortBy = String.Empty
            End If

            Me.State.searchDV = ListItemByEntity.LoadEntityList(oLanguageCode, Me.State.dropdownByEntityBO.EntityReferenceId, Me.State.dropdownByEntityBO.ListCode, Me.State.dropdownByEntityBO.EntityReference, Me.State.SortExpression)

            Me.GridDropdownsByEntity.AutoGenerateColumns = False
            Me.GridDropdownsByEntity.DataSource = Me.State.searchDV
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            HighLightSortColumn(GridDropdownsByEntity, State.SortExpression)


            Me.GridDropdownsByEntity.DataBind()
        End Sub
        Protected Sub GridDropdownsByEntity_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridDropdownsByEntity.RowDataBound

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim entityType As TextBox
                entityType = CType(e.Row.FindControl("TextBoxEntityType"), TextBox)
                entityType.Text = TranslationBase.TranslateLabelOrMessage(entityType.Text)
            End If



        End Sub
        Public Sub GridDropdowns_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDropdownsByEntity.RowCommand
            Try

                If e.CommandName = "ViewEdit" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Dim searchFlag As String = Nothing
                    Dim listCode As String = CType(Me.GridDropdownsByEntity.Rows(index).Cells(Me.GRID_COL_LIST_CODE).FindControl("TextBoxListCode"), TextBox).Text
                    Dim listDescription As String = CType(Me.GridDropdownsByEntity.Rows(index).Cells(Me.GRID_COL_LIST_DESCRIPTION).FindControl("TextBoxListDescription"), TextBox).Text
                    Dim entity_Type_Code As String = CType(Me.GridDropdownsByEntity.Rows(index).Cells(Me.GRID_COL_ENTITY_TYPE_CODE).FindControl("LabelEntityTypeCode"), Label).Text
                    Dim entityTypeDescription As String = CType(Me.GridDropdownsByEntity.Rows(index).Cells(Me.GRID_COL_ENTITY_TYPE_DESCRIPTION).FindControl("TextBoxEntityType"), TextBox).Text
                    Dim entityCode As String = CType(Me.GridDropdownsByEntity.Rows(index).Cells(Me.GRID_COL_ENTITY_CODE).FindControl("TextBoxEntityCode"), TextBox).Text
                    Dim entityDescription As String = CType(Me.GridDropdownsByEntity.Rows(index).Cells(Me.GRID_COL_ENTITY_DESCRIPTION).FindControl("TextBoxEntityDescription"), TextBox).Text
                    Dim entityReferenceId As New Guid(CType(Me.GridDropdownsByEntity.Rows(index).Cells(Me.GRID_COL_ENTITY_TYPE_CODE).FindControl("LabelEntityRefId"), Label).Text)
                    Me.State.dropdownByEntityBO = New ListItemByEntity()
                    Dim entityRefId As Guid
                    If Me.moDropdownList.Text.Trim = String.Empty Then
                        searchFlag = SEARCH_FLAG
                    End If


                    With Me.State.dropdownByEntityBO
                        .ListCode = listCode
                        .ListDescription = listDescription
                        .EntityReferenceId = entityReferenceId
                        .EntityDescription = entityDescription
                        .EntityType = entityTypeDescription
                        .EntityReference = entity_Type_Code
                        .SearchType = searchFlag
                    End With


                    Me.callPage(AdminMaintainDropdownByEntityDetailsForm.PAGE_NAME, BuildParameters)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub GridDropdownsByEntity_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridDropdownsByEntity.PageIndexChanging
            Try
                GridDropdownsByEntity.PageIndex = e.NewPageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridDropdownsByEntity.RowCreated


            BaseItemCreated(sender, e)
        End Sub
        Private Sub GridDropdownsByEntity_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridDropdownsByEntity.Sorting
            Try
                If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.SortExpression.EndsWith(" DESC") Then
                        Me.State.SortExpression = e.SortExpression
                    Else
                        Me.State.SortExpression &= " DESC"
                    End If
                Else
                    Me.State.SortExpression = e.SortExpression
                End If
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            ErrorControl.Clear_Hide()
            Try

                If Not Me.moDropdownList Is Nothing AndAlso Me.moDropdownList.Text.Trim = String.Empty Then
                    PopulateListItem()
                End If
                PopulateBoFromForm()
                PopulateGrid()

                Me.ErrorControl.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try

                If Not Me.moDropdownList Is Nothing AndAlso Not Me.moDropdownList.Text.Trim = String.Empty Then

                    moDropdownEntityType.Items.Clear()
                    moDropdownEntity.Items.Clear()
                    PopulateListItem()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub bntAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAdd.Click
            'ErrorControl.Clear_Hide()
            Try
                ValidateForm()
                PopulateBoFromForm()
                Dim oLanguageCode As String = Codes.ENGLISH_LANG_CODE
                Me.State.searchDV = ListItemByEntity.LoadEntityList(oLanguageCode, Me.State.dropdownByEntityBO.EntityReferenceId, Me.State.dropdownByEntityBO.ListCode, Me.State.dropdownByEntityBO.EntityReference, Me.State.SortExpression)

                If Me.State.searchDV.Count > 0 Then
                    Throw New GUIException(Message.MSG_VALUE_ALREADY_IN_USE, Assurant.ElitaPlus.Common.ErrorCodes.ENTITY_LIST_ALREADY_EXISTS_ERR)
                Else
                    Me.callPage(AdminMaintainDropdownByEntityDetailsForm.PAGE_NAME, BuildParameters)
                End If


                'Me.ErrorControl.Show()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Me.ErrorControl.Clear_Hide()
            Try
                Dim i As Integer
                Dim deleteCount As Integer = 0
                Dim TotalChecked As Integer = 0
                Dim retVal As Integer
                Me.State.dropdownByEntityBO = New ListItemByEntity()

                For i = 0 To Me.GridDropdownsByEntity.Rows.Count - 1
                    If CType(Me.GridDropdownsByEntity.Rows(i).Cells(Me.SELECTED_CIDX).FindControl("CheckBoxItemSel"), CheckBox).Checked Then
                        TotalChecked += 1
                        Dim listCode As String = CType(Me.GridDropdownsByEntity.Rows(i).Cells(Me.GRID_COL_LIST_CODE).FindControl("TextBoxListCode"), TextBox).Text
                        Dim entityReferenceId As New Guid(CType(Me.GridDropdownsByEntity.Rows(i).Cells(Me.GRID_COL_ENTITY_REFERENCE_ID).FindControl("LabelEntityRefId"), Label).Text)

                        Try
                            retVal = Me.State.dropdownByEntityBO.DeleteDropdown(listCode, entityReferenceId)
                            If retVal = 0 Then
                                deleteCount += 1
                            Else

                                Me.ErrorControl.AddError(Message.ERR_DELETING_DATA)
                            End If
                        Catch ex As Exception

                            Me.DisplayMessage(Message.ERR_DELETING_DATA, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        End Try
                    End If
                Next
                If (deleteCount > 0) Then
                    PopulateBoFromForm()
                    Me.PopulateGrid()
                End If

                Dim ProcessingResultMsg As String = TranslationBase.TranslateLabelOrMessage(Message.BATCH_DELETE_PROCESS)
                ProcessingResultMsg = ProcessingResultMsg.Replace("?1", deleteCount.ToString)
                ProcessingResultMsg = ProcessingResultMsg.Replace("?2", TotalChecked.ToString)
                Me.AddInfoMsg(ProcessingResultMsg, False)
                Me.ErrorControl.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("ADMIN") & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("MAINTAIN_DROPDOWN_BY_ENTITY")
                End If
            End If
        End Sub
        Public Sub ValidateForm()

            If Me.moDropdownList.Text.Trim = String.Empty Then
                'display error
                ElitaPlusPage.SetLabelError(Me.lblDropdownList)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR, TranslationBase.TranslateLabelOrMessage("DROPDOWN_LIST") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            Else
                SetLabelColor(Me.lblDropdownList)
            End If
            If Me.moDropdownEntityType.Text.Trim = String.Empty Then
                'display error
                ElitaPlusPage.SetLabelError(Me.LabelEntityType)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR, TranslationBase.TranslateLabelOrMessage("ENTITY_TYPE") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            Else
                SetLabelColor(Me.LabelEntityType)
            End If

            If Me.moDropdownEntity.Text.Trim = String.Empty Then
                'display error
                ElitaPlusPage.SetLabelError(Me.LabelEntity)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR, TranslationBase.TranslateLabelOrMessage("ENTITY") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            Else
                SetLabelColor(Me.LabelEntity)
            End If


        End Sub
        Public Sub PopulateBoFromForm()

            Me.State.dropdownByEntityBO = New ListItemByEntity()
            Dim dvList As DataView
            If Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY_GROUP" Then

                dvList = LookupListNew.GetCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            ElseIf Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY" Then

                dvList = LookupListNew.GetCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            ElseIf Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER_GROUP" Then

                dvList = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            ElseIf Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER" Then
                dvList = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            End If

            Dim entityRefId As Guid
            If Me.moDropdownEntity.Text.Trim = String.Empty Then
                entityRefId = Guid.Empty

            Else
                entityRefId = LookupListNew.GetIdFromCode(dvList, moDropdownEntity.SelectedValue)

            End If

            With Me.State.dropdownByEntityBO
                .ListCode = Me.moDropdownList.SelectedValue.Trim
                .ListDescription = Me.moDropdownList.SelectedItem.Text.Trim
                .EntityReferenceId = entityRefId
                .EntityDescription = Me.moDropdownEntity.SelectedItem.Text.Trim
                .EntityType = Me.moDropdownEntityType.SelectedItem.Text.Trim
                .EntityReference = moDropdownEntityType.SelectedValue.Trim

            End With


        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
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
            If Not dvList Is Nothing Then
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

            If Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY_GROUP" Then

                '  Dim dvList As DataView = LookupListNew.GetCompanyGroupCountryLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

                Dim dvList As DataView = LookupListNew.GetCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If Not dvList Is Nothing Then
                    For i = 0 To dvList.Count - 1
                        moDropdownEntity.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                    Next
                End If

            ElseIf Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_COMPANY" Then

                Dim dvList As DataView = LookupListNew.GetCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If Not dvList Is Nothing Then
                    For i = 0 To dvList.Count - 1
                        moDropdownEntity.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                    Next
                End If

            ElseIf Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER_GROUP" Then

                Dim dvList As DataView = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If Not dvList Is Nothing Then
                    For i = 0 To dvList.Count - 1
                        moDropdownEntity.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                    Next
                End If

            ElseIf Me.moDropdownEntityType.SelectedValue.Trim.ToUpper = "ELP_DEALER" Then
                Dim dvList As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim i As Integer
                moDropdownEntity.Items.Clear()
                moDropdownEntity.Items.Add(New ListItem("", ""))
                If Not dvList Is Nothing Then
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
            Dim dropdownByEntityBO As ListItemByEntity = Me.State.dropdownByEntityBO
            Return New Translation.AdminMaintainDropdownByEntityDetailsForm.Parameters(dropdownByEntityBO)
        End Function

#End Region

    End Class

End Namespace
﻿Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Interfaces
    Public Class EquipmentReconWrkForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Private Const EMPTY As String = ""
        Public Const URL As String = "EquipmentReconWrkForm.aspx"
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const CANCELLATIONS_TYPE As String = "N"
        Private Const DEFAULT_PAGE_INDEX As Integer = 0
        Private Const ID_COL As Integer = 1
        Public Const RECORD_TYPE_COL As Integer = 2
        Public Const REJECT_CODE_COL As Integer = 3
        Private Const REJECT_REASON_COL As Integer = 4
        Public Const MANUFACTURER_COL As Integer = 5
        Public Const MODEL_COL As Integer = 6
        Public Const DESCRIPTION_COL As Integer = 7
        Public Const IS_MASTER_COL As Integer = 8
        Public Const MASTER_EQUIPMENT_DESCRIPTION_COL As Integer = 9
        Public Const REPAIRABLE_COL As Integer = 10
        Private Const EQUIPMENT_CLASS_COL As Integer = 11
        Private Const EQUIPMENT_TYPE_COL As Integer = 12
        Private Const MANUFACTURER_WARRANTY_COL As Integer = 13
        Public Const ATTRIBUTES As Integer = 14
        Public Const NOTE1 As Integer = 15
        Public Const NOTE2 As Integer = 16
        Public Const NOTE3 As Integer = 17
        Public Const NOTE4 As Integer = 18
        Public Const NOTE5 As Integer = 19
        Public Const NOTE6 As Integer = 20
        Public Const NOTE7 As Integer = 21
        Public Const NOTE8 As Integer = 22
        Public Const NOTE9 As Integer = 23
        Public Const NOTE10 As Integer = 24
        Private Const MODIFIED_DATE_COL As Integer = 25
        Public Const CREATED_DATE_COL As Integer = 26
        Public Const CREATED_BY_COL As Integer = 27
        Public Const REJECT_MSG_PARMS_CODE As Integer = 28
        Public Const MODIFIED_BY As Integer = 29

        'Property Name
        Public Const RECORD_TYPE_Property As String = "RecordType"
        Public Const MANUFACTURER_Property As String = "Manufacturer"
        Public Const MODEL_Property As String = "Model"
        Public Const DESCRIPTION_Property As String = "Description"
        Public Const IS_MASTER_Property As String = "IsMaster"
        Public Const MASTER_EQUIPMENT_DESCRIPTION_Property As String = "MasterEquipmentDescription"
        Public Const REPAIRABLE_Property As String = "Repairable"
        Public Const EQUIPMENT_CLASS_Property As String = "EquipmentClass"
        Public Const EQUIPMENT_TYPE_Property As String = "EquipmentType"
        Public Const MANUFACTURER_WARRANTY_Property As String = "ManufacturerWarranty"
        Public Const ATTRIBUTES_Property As String = "Attributes"
        Public Const NOTE1_Property As String = "Note1"
        Public Const NOTE2_Property As String = "Note2"
        Public Const NOTE3_Property As String = "Note3"
        Public Const NOTE4_Property As String = "Note4"
        Public Const NOTE5_Property As String = "Note5"
        Public Const NOTE6_Property As String = "Note6"
        Public Const NOTE7_Property As String = "Note7"
        Public Const NOTE8_Property As String = "Note8"
        Public Const NOTE9_Property As String = "Note9"
        Public Const NOTE10_Property As String = "Note10"
        Public Const REJECT_REASON_Property As String = "RejectReason"
        Public Const REJECT_CODE_Property As String = "RejectCode"
        Public Const REJECT_MSG_PARMS_Property As String = "RejectMsgParms"
        Public Const CREATED_DATE_Property As String = "CreatedDate"
        Public Const CREATED_BY_Property As String = "CreatedBy"
        Public Const MODIFIED_DATE_Property As String = "ModifiedDate"
        Public Const MODIFIED_BY_Property As String = "ModifiedBy"
        Public Const LOAD_STATUS_Property As String = "LoadStatus"
        Private Const EQUIPMENT_RECON As String = "EQUIPMENT_RECON_FILE"
#End Region
#Region "Member Variables"

        Protected TempDataView As DataView = New DataView
        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Protected WithEvents btnSave As System.Web.UI.WebControls.Button
        Protected WithEvents btnUndo As System.Web.UI.WebControls.Button
        Protected WithEvents LbPage As System.Web.UI.WebControls.Label
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        Protected WithEvents tsHoriz As Microsoft.Web.UI.WebControls.TabStrip
        Protected WithEvents ddlCancellationReason As System.Web.UI.WebControls.DropDownList
        Protected WithEvents mpHoriz As Microsoft.Web.UI.WebControls.MultiPage

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub
#End Region
#Region "Properties"
        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.moDataGrid.EditIndex > Me.NO_ITEM_SELECTED_INDEX)
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page State"
        Public Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub
        End Class
        Class MyState
            Public SortExpression As String = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID
            Public PageIndex As Integer = 0
            Public oReturnType As FileProcessedController.ReturnType
            Public fileProcessedId As Guid
            Public BundlesHashTable As Hashtable
            Public BestReplacementReconWrkId As Guid
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public sortBy As String
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
            Me.State.oReturnType = CType(Me.CallingParameters, FileProcessedController.ReturnType)
            Me.State.fileProcessedId = Me.State.oReturnType.FileProcessedId
        End Sub

#End Region
#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            Me.ErrController.Clear_Hide()
            Me.SetStateProperties()
            Me.MenuEnabled = False
            If Not Page.IsPostBack Then
                Me.SortDirection = EMPTY
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(ErrController)
                Me.State.PageIndex = 0
                Me.TranslateGridHeader(moDataGrid)
                Me.TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                PopulateGrid()
                CheckFileTypeColums()
            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub
#End Region
#Region "Contorlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value
            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = Me.MSG_VALUE_YES Then
                        SavePage()
                        Me.HiddenIsBundlesPageDirty.Value = EMPTY
                        'Me.HiddenIfComingFromBundlesScreen.Value = EMPTY
                    ElseIf confResponse = Me.MSG_VALUE_NO Then
                    End If
                    Me.HiddenSavePagePromptResponse.Value = EMPTY
                    Me.HiddenIsPageDirty.Value = EMPTY

                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(Me.State.oReturnType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            Me.moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case ElitaPlusPage.DetailPageCommand.GridColSort
                            'Me.State.sortBy = CType(e.CommandArgument, String)
                        Case Else
                            Me.moDataGrid.PageIndex = Me.State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(ByVal index As Integer) As EquipmentReconWrk
            Dim EquipmentReconWrkId As Guid
            Dim EquipmentReconWrkInfo As EquipmentReconWrk
            Dim sModifiedDate As String
            moDataGrid.SelectedIndex = index
            EquipmentReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moEquipmentReconWrkIdLabel"), Label).Text)
            sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            EquipmentReconWrkInfo = New EquipmentReconWrk(EquipmentReconWrkId, sModifiedDate)
            Return EquipmentReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim totc As Integer = Me.moDataGrid.Columns.Count()
            Dim index As Integer = 0
            Dim EquipmentReconWrkInfo As EquipmentReconWrk
            Dim totItems As Integer = Me.moDataGrid.Rows.Count

            If totItems > 0 Then
                EquipmentReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(EquipmentReconWrkInfo)
                PopulateBOFromForm(EquipmentReconWrkInfo)
                EquipmentReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                EquipmentReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(EquipmentReconWrkInfo)
                PopulateBOFromForm(EquipmentReconWrkInfo)
                EquipmentReconWrkInfo.Save()
            Next
        End Sub
        Function IsDataGPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Private Sub SetColumnState(ByVal column As Byte, ByVal state As Boolean)
            Me.moDataGrid.Columns(column).Visible = state
        End Sub
        Function IsCancellationsFileType(ByVal fileName As String) As Boolean
            Return fileName.Substring(4, 1).Equals(CANCELLATIONS_TYPE)
        End Function

        Private Sub CheckFileTypeColums()
            If IsCancellationsFileType(moFileNameText.Text) Then
                SetColumnState(RECORD_TYPE_COL, False)
                SetColumnState(MANUFACTURER_COL, False)
                SetColumnState(MODEL_COL, False)
                SetColumnState(DESCRIPTION_COL, False)
                SetColumnState(IS_MASTER_COL, False)
                SetColumnState(MASTER_EQUIPMENT_DESCRIPTION_COL, False)
                SetColumnState(REPAIRABLE_COL, False)
                SetColumnState(REJECT_REASON_COL, False)
                SetColumnState(EQUIPMENT_CLASS_COL, False)
                SetColumnState(EQUIPMENT_TYPE_COL, False)
                SetColumnState(REJECT_REASON_COL, False)
                SetColumnState(EQUIPMENT_CLASS_COL, False)
                SetColumnState(EQUIPMENT_TYPE_COL, False)
                SetColumnState(MANUFACTURER_WARRANTY_COL, False)
                SetColumnState(ATTRIBUTES, False)
                SetColumnState(NOTE1, False)
                SetColumnState(NOTE2, False)
                SetColumnState(NOTE3, False)
                SetColumnState(NOTE4, False)
                SetColumnState(NOTE5, False)
                SetColumnState(NOTE6, False)
                SetColumnState(NOTE7, False)
                SetColumnState(NOTE8, False)
                SetColumnState(NOTE9, False)
                SetColumnState(NOTE10, False)
                SetColumnState(REJECT_REASON_COL, False)
                SetColumnState(REJECT_CODE_COL, False)
                SetColumnState(MODIFIED_DATE_COL, False)
            Else
                'SetColumnState(CANCELLATION_CODE_COL, False)
            End If
        End Sub
#End Region

#Region "Populate"
        Private Sub PopulateReadOnly()
            Try
                Dim oFile As FileProcessed = New FileProcessed(Me.State.fileProcessedId)
                With oFile
                    moFileNameText.Text = .FileName
                    moCompanyGroupText.Text = .CompanyGroupLoad
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
        Sub PopulateRecordTypeDrop(ByVal recordTypeDrop As DropDownList)
            Try
                Dim oLangId As Guid = Authentication.LangId
                Dim recordTypeList As DataView = LookupListNew.GetRecordTypeLookupList(oLangId)
                ' Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , False)
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
        Private Function GetGridDataView() As DataView

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.EquipmentReconWrk.LoadList(.fileProcessedId))
            End With

        End Function
        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = GetGridDataView()
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function
        Private Sub PopulateGrid()

            Dim dv As DataView
            Dim recCount As Integer = 0
            Try
                dv = GetDV()
                'dv.Sort = Me.State.sortBy
                If Not Me.SortDirection.Equals(EMPTY) Then
                    dv.Sort = Me.SortDirection
                    HighLightSortColumn(moDataGrid, Me.SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                Me.moDataGrid.PageSize = Me.State.selectedPageSize
                Me.moDataGrid.DataSource = dv
                Me.moDataGrid.DataBind()
                Me.lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub PopulateBOItem(ByVal EquipmentReconWrkInfo As EquipmentReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(EquipmentReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(ByVal EquipmentReconWrkInfo As EquipmentReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(EquipmentReconWrkInfo, oPropertyName,
                                CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBOFromForm(ByVal EquipmentReconWrkInfo As EquipmentReconWrk)

            PopulateBODrop(EquipmentReconWrkInfo, RECORD_TYPE_Property, RECORD_TYPE_COL)
            PopulateBOItem(EquipmentReconWrkInfo, REJECT_REASON_Property, REJECT_REASON_COL)
            PopulateBOItem(EquipmentReconWrkInfo, REJECT_CODE_Property, REJECT_CODE_COL)
            PopulateBOItem(EquipmentReconWrkInfo, MANUFACTURER_Property, MANUFACTURER_COL)
            PopulateBOItem(EquipmentReconWrkInfo, MODEL_Property, MODEL_COL)
            PopulateBOItem(EquipmentReconWrkInfo, DESCRIPTION_Property, DESCRIPTION_COL)
            PopulateBOItem(EquipmentReconWrkInfo, IS_MASTER_Property, IS_MASTER_COL)
            PopulateBOItem(EquipmentReconWrkInfo, MASTER_EQUIPMENT_DESCRIPTION_Property, MASTER_EQUIPMENT_DESCRIPTION_COL)
            PopulateBOItem(EquipmentReconWrkInfo, REPAIRABLE_Property, REPAIRABLE_COL)
            PopulateBOItem(EquipmentReconWrkInfo, EQUIPMENT_CLASS_Property, EQUIPMENT_CLASS_COL)
            PopulateBOItem(EquipmentReconWrkInfo, EQUIPMENT_TYPE_Property, EQUIPMENT_TYPE_COL)
            PopulateBOItem(EquipmentReconWrkInfo, MANUFACTURER_WARRANTY_Property, MANUFACTURER_WARRANTY_COL)
            PopulateBOItem(EquipmentReconWrkInfo, ATTRIBUTES_Property, ATTRIBUTES)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE1_Property, NOTE1)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE2_Property, NOTE2)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE3_Property, NOTE3)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE4_Property, NOTE4)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE5_Property, NOTE5)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE6_Property, NOTE6)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE7_Property, NOTE7)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE8_Property, NOTE8)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE9_Property, NOTE9)
            PopulateBOItem(EquipmentReconWrkInfo, NOTE10_Property, NOTE10)
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Sub PopulateFormItem(ByVal oCellPosition As Integer, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(Me.GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub
#End Region

#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                Me.State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    Me.moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oTextBox As TextBox

            'translate the reject message
            Dim strMsg As String, dr As DataRow, intParamCnt As Integer = 0, strParamList As String = String.Empty
            If Not dvRow Is Nothing Then
                dr = dvRow.Row
                strMsg = dr(EquipmentReconDAL.COL_TRANSLATED_MSG).ToString.Trim
                If strMsg <> String.Empty Then
                    If Not dr(EquipmentReconDAL.COL_MSG_PARAMETER_COUNT) Is DBNull.Value Then
                        intParamCnt = CType(dr(EquipmentReconDAL.COL_MSG_PARAMETER_COUNT), Integer)
                    End If
                    If intParamCnt > 0 Then
                        If Not dr(EquipmentReconDAL.COL_REJECT_MSG_PARMS) Is DBNull.Value Then
                            strParamList = dr(EquipmentReconDAL.COL_REJECT_MSG_PARMS).ToString.Trim
                        End If
                        strMsg = TranslationBase.TranslateParameterizedMsg(strMsg, intParamCnt, strParamList).Trim
                    End If
                    'If strMsg <> "" Then dr(EquipmentReconDAL.COL_REJECT_REASON) = strMsg
                    dr.AcceptChanges()
                End If
            End If

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row

                    Me.PopulateControlFromBOProperty(.FindControl("moEquipmentReconWrkIdLabel"), dvRow(EquipmentReconWrk.COL_NAME_EQUIPMENT_RECON_WRK_ID))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(EquipmentReconWrk.COL_NAME_RECORD_TYPE), String)
                    oTextBox = CType(.FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_REJECT_REASON))
                    oTextBox = CType(.FindControl("moRejectCode"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_REJECT_CODE))
                    Me.SetSelectedItemByText(oDrop, oValue)
                    oTextBox = CType(.FindControl("moManufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_MANUFACTURER))
                    oTextBox = CType(.FindControl("moModelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_MODEL))
                    oTextBox = CType(.FindControl("moDescriptionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_DESCRIPTION))
                    oTextBox = CType(.FindControl("moIsMasterTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_IS_MASTER))
                    oTextBox = CType(.FindControl("moMasterEquipmentDescriptionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_MASTER_EQUIPMENT_DESCRIPTION))
                    oTextBox = CType(.FindControl("moRepairableTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_REPAIRABLE))
                    oTextBox = CType(.FindControl("moEquipmentClassTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_EQUIPMENT_CLASS))
                    oTextBox = CType(.FindControl("moEquipmentTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_EQUIPMENT_TYPE))
                    oTextBox = CType(.FindControl("moManufacturerWarentyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_MANUFACTURER_WARRANTY))
                    oTextBox = CType(.FindControl("moAttributesTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_ATTRIBUTES))
                    oTextBox = CType(.FindControl("monote1TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE1))
                    oTextBox = CType(.FindControl("monote2TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE2))
                    oTextBox = CType(.FindControl("monote3TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE3))
                    oTextBox = CType(.FindControl("monote4TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE4))
                    oTextBox = CType(.FindControl("monote5TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE5))
                    oTextBox = CType(.FindControl("monote6TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE6))
                    oTextBox = CType(.FindControl("monote7TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE7))
                    oTextBox = CType(.FindControl("monote8TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE8))
                    oTextBox = CType(.FindControl("monote9TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE9))
                    oTextBox = CType(.FindControl("monote10TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(EquipmentReconWrk.COL_NAME_NOTE10))
                End With
            End If
            BaseItemBound(source, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")
                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
        Protected Sub BindBoPropertiesToGridHeaders(ByVal EquipmentRconWrkInfo As EquipmentReconWrk)

            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "RecordType", Me.moDataGrid.Columns(Me.RECORD_TYPE_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Manufacturer", Me.moDataGrid.Columns(Me.MANUFACTURER_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Model", Me.moDataGrid.Columns(Me.MODEL_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Description", Me.moDataGrid.Columns(Me.DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "IsMaster", Me.moDataGrid.Columns(Me.IS_MASTER_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "MasterEquipmentDescription", Me.moDataGrid.Columns(Me.MASTER_EQUIPMENT_DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Repairable", Me.moDataGrid.Columns(Me.REPAIRABLE_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "EquipmentClass", Me.moDataGrid.Columns(Me.EQUIPMENT_CLASS_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "EquipmentType", Me.moDataGrid.Columns(Me.EQUIPMENT_TYPE_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "ManufacturerWarranty", Me.moDataGrid.Columns(Me.MANUFACTURER_WARRANTY_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Attributes", Me.moDataGrid.Columns(Me.ATTRIBUTES))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note1", Me.moDataGrid.Columns(Me.NOTE1))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note2", Me.moDataGrid.Columns(Me.NOTE2))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note3", Me.moDataGrid.Columns(Me.NOTE3))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note4", Me.moDataGrid.Columns(Me.NOTE4))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note5", Me.moDataGrid.Columns(Me.NOTE5))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note5", Me.moDataGrid.Columns(Me.NOTE6))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note7", Me.moDataGrid.Columns(Me.NOTE7))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note8", Me.moDataGrid.Columns(Me.NOTE8))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note9", Me.moDataGrid.Columns(Me.NOTE9))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "Note10", Me.moDataGrid.Columns(Me.NOTE10))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "RejectReason", Me.moDataGrid.Columns(Me.REJECT_REASON_COL))
            Me.BindBOPropertyToGridHeader(EquipmentRconWrkInfo, "RejectMode", Me.moDataGrid.Columns(Me.REJECT_CODE_COL))

            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub
        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub
#End Region

#Region "Button Click Events"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Or (Not Me.State.BundlesHashTable Is Nothing AndAlso Me.State.BundlesHashTable.Count > 0) Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(Me.State.oReturnType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                Me.State.BundlesHashTable = Nothing
                PopulateGrid()
                Me.HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
#End Region

    End Class
End Namespace

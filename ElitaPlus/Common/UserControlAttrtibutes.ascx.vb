Imports Assurant.ElitaPlus.Common.GuidControl
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports System.Collections.Generic
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class UserControlAttrtibutes
    Inherits UserControl

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Public Sub BindBoProperties()
        Dim attributeValues As IEnumerable(Of AttributeValue) = ParentBusinessObject.AttributeValues
        For Each av As AttributeValue In attributeValues
            Page.BindBOPropertyToGridHeader(av, "AttributeId", AttributeValueGridView.Columns(GRID_COL_UI_PROG_CODE_IDX))
            Page.BindBOPropertyToGridHeader(av, "Value", AttributeValueGridView.Columns(GRID_COL_ATTRIBUTE_VALUE_IDX))
            Page.BindBOPropertyToGridHeader(av, "EffectiveDate", AttributeValueGridView.Columns(GRID_COL_EFFECTIVE_DATE_IDX))
            Page.BindBOPropertyToGridHeader(av, "ExpirationDate", AttributeValueGridView.Columns(GRID_COL_EXPIRATION_DATE_IDX))
        Next

        If (State.IsNew = True) Then
            Page.BindBOPropertyToGridHeader(State.MyBO, "AttributeId", AttributeValueGridView.Columns(GRID_COL_UI_PROG_CODE_IDX))
            Page.BindBOPropertyToGridHeader(State.MyBO, "Value", AttributeValueGridView.Columns(GRID_COL_ATTRIBUTE_VALUE_IDX))
            Page.BindBOPropertyToGridHeader(State.MyBO, "EffectiveDate", AttributeValueGridView.Columns(GRID_COL_EFFECTIVE_DATE_IDX))
            Page.BindBOPropertyToGridHeader(State.MyBO, "ExpirationDate", AttributeValueGridView.Columns(GRID_COL_EXPIRATION_DATE_IDX))
        End If

    End Sub

    Public Sub TranslateHeaders()
        Page.TranslateGridHeader(AttributeValueGridView)
    End Sub

#End Region

#Region "Constants"

    Private Const GRID_COL_UI_PROG_CODE_IDX As Integer = 0
    Private Const GRID_COL_ATTRIBUTE_VALUE_IDX As Integer = 1
    Private Const GRID_COL_EFFECTIVE_DATE_IDX As Integer = 2
    Private Const GRID_COL_EXPIRATION_DATE_IDX As Integer = 3

    Private Const UI_PROG_CODE_LABEL_NAME As String = "UiProgCodeLabel"
    Private Const ATTRIBUTE_VALUE_LABEL_NAME As String = "AttributeValueLabel"
    Private Const EFFECTIVE_DATE_LABEL_NAME As String = "EffectiveDateLabel"
    Private Const EXPIRATION_DATE_LABEL_NAME As String = "ExpirationDateLabel"
    Private Const UI_PROG_CODE_DROPDOWN_NAME As String = "UiProgCodeDropDown"
    Private Const ATTRIBUTE_VALUE_DROPDOWN_NAME As String = "AttributeValueDropDown"
    Private Const ATTRIBUTE_VALUE_TEXTBOX_NAME As String = "AttributeValueTextBox"
    Private Const ATTRIBUTE_VALUE_IMAGEBUTTON_NAME As String = "AttributeValueImageButton"
    Private Const EFFECTIVE_DATE_TEXTBOX_NAME As String = "EffectiveDateTextBox"
    Private Const EFFECTIVE_DATE_IMAGEBUTTON_NAME As String = "EffectiveDateImageButton"
    Private Const EXPIRATION_DATE_TEXTBOX_NAME As String = "ExpirationDateTextBox"
    Private Const EXPIRATION_DATE_IMAGEBUTTON_NAME As String = "ExpirationDateImageButton"
    Private Const EDIT_BUTTON_NAME As String = "EditButton"
    Private Const DELETE_BUTTON_NAME As String = "DeleteButton"
    Private Const SAVE_BUTTON_NAME As String = "SaveButton"
    Private Const CANCEL_BUTTON_NAME As String = "CancelButton"

    Private Const DELETE_COMMAND_NAME As String = "DeleteRecord"
    Private Const EDIT_COMMAND_NAME As String = "EditRecord"
    Private Const SAVE_COMMAND_NAME As String = "SaveRecord"
    Private Const CANCEL_COMMAND_NAME As String = "CancelRecord"

#End Region

#Region "Private Types"

    Private Class MyState

        Public MyBO As AttributeValue = Nothing
        Public IsNew As Boolean = False

    End Class

#End Region

#Region "Fields"

    'Private _YesNoDataView As DataView
    'Private _AttributeCodeDataView As DataView
    'Private _DataTypeDataView As DataView

#End Region

#Region "Properties"

    Public Property ParentBusinessObject As IAttributable

    Private ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

    Private ReadOnly Property YesNoDataView As List(Of ListItem)
        Get
            If (YesNoDataView Is Nothing) Then
                YesNoDataView = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return YesNoDataView
        End Get
    End Property

    Private ReadOnly Property YesNoIdFromCode(code As String) As Guid
        Get
            Return (From YesNoList In YesNoDataView
                    Where YesNoList.Code = code
                    Select YesNoList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property YesNoDescriptionFromCode(code As String) As String
        Get
            Return (From YesNoList In YesNoDataView
                    Where YesNoList.Code = code
                    Select YesNoList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property YesNoCodeFromId(id As Guid) As String
        Get
            Return (From YesNoList In YesNoDataView
                    Where YesNoList.ListItemId = id
                    Select YesNoList.Code).FirstOrDefault()
        End Get
    End Property

    'Private ReadOnly Property YesNoDataView As DataView
    '    Get
    '        If (_YesNoDataView Is Nothing) Then
    '            _YesNoDataView = LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
    '        End If
    '        Return _YesNoDataView
    '    End Get
    'End Property

    Private ReadOnly Property ReInsStatusDataView As List(Of ListItem)
        Get
            If (ReInsStatusDataView Is Nothing) Then
                ReInsStatusDataView = (From ris In (CommonConfigManager.Current.ListManager.GetList("REINSURANCE_STATUSES", Thread.CurrentPrincipal.GetLanguageCode()))
                                       Where ris.Code <> "PARTIALLY_REINSURED" AndAlso ris.Code <> "PARTIALLY_REJECTED"
                                       Select ris).ToList()
            End If
            Return ReInsStatusDataView
        End Get
    End Property

    Private ReadOnly Property ReInsStatusIdFromCode(code As String) As Guid
        Get
            Return (From ReInsStatusList In ReInsStatusDataView
                    Where ReInsStatusList.Code = code
                    Select ReInsStatusList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property ReInsStatusDescriptionFromCode(code As String) As String
        Get
            Return (From ReInsStatusList In ReInsStatusDataView
                    Where ReInsStatusList.Code = code
                    Select ReInsStatusList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property ReInsStatusCodeFromId(id As Guid) As String
        Get
            Return (From ReInsStatusList In ReInsStatusDataView
                    Where ReInsStatusList.ListItemId = id
                    Select ReInsStatusList.Code).FirstOrDefault()
        End Get
    End Property

    'Private ReadOnly Property ReInsStatusDataView As DataView
    '    Get
    '        If (ReInsStatusDataView Is Nothing) Then
    '            ReInsStatusDataView = LookupListNew.GetReInsStatusesWithoutPartialStatuesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
    '        End If
    '        Return ReInsStatusDataView
    '    End Get
    'End Property

    Private ReadOnly Property PostMigCndDataView As List(Of ListItem)
        Get
            If (PostMigCndDataView Is Nothing) Then
                PostMigCndDataView = CommonConfigManager.Current.ListManager.GetList("POST_MIG_CONDITIONS", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return PostMigCndDataView
        End Get
    End Property

    Private ReadOnly Property PostMigCndIdFromCode(code As String) As Guid
        Get
            Return (From PostMigCndList In PostMigCndDataView
                    Where PostMigCndList.Code = code
                    Select PostMigCndList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property PostMigCndDescriptionFromCode(code As String) As String
        Get
            Return (From PostMigCndList In PostMigCndDataView
                    Where PostMigCndList.Code = code
                    Select PostMigCndList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property PostMigCndCodeFromId(id As Guid) As String
        Get
            Return (From PostMigCndList In PostMigCndDataView
                    Where PostMigCndList.ListItemId = id
                    Select PostMigCndList.Code).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AcctProrateDataView As List(Of ListItem)
        Get
            If (AcctProrateDataView Is Nothing) Then
                AcctProrateDataView = CommonConfigManager.Current.ListManager.GetList("ACCT_PRORATE", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return AcctProrateDataView
        End Get
    End Property
    
    Private ReadOnly Property AcctProrateIdFromCode(code As String) As Guid
        Get
            Return (From AcctProrateList In AcctProrateDataView
                Where AcctProrateList.Code = code
                Select AcctProrateList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AcctProrateDescriptionFromCode(code As String) As String
        Get
            Return (From AcctProrateList In AcctProrateDataView
                Where AcctProrateList.Code = code
                Select AcctProrateList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AcctProrateCodeFromId(id As Guid) As String
        Get
            Return (From AcctProrateList In AcctProrateDataView
                Where AcctProrateList.ListItemId = id
                Select AcctProrateList.Code).FirstOrDefault()
        End Get
    End Property
    Private ReadOnly Property AutoRenewCovLimitDataView As List(Of ListItem)
        Get
            If (AutoRenewCovLimitDataView Is Nothing) Then
                AutoRenewCovLimitDataView = CommonConfigManager.Current.ListManager.GetList("AUTO_RENEW_COV_LIMIT", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return AutoRenewCovLimitDataView
        End Get
    End Property
    Private ReadOnly Property AutoRenewCovLimitIdFromCode(code As String) As Guid
        Get
            Return (From AutoRenewCovLimitList In AutoRenewCovLimitDataView
                Where AutoRenewCovLimitList.Code = code
                Select AutoRenewCovLimitList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AutoRenewCovLimitDescriptionFromCode(code As String) As String
        Get
            Return (From AutoRenewCovLimitList In AutoRenewCovLimitDataView
                Where AutoRenewCovLimitList.Code = code
                Select AutoRenewCovLimitList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AutoRenewCovLimitCodeFromId(id As Guid) As String
        Get
            Return (From AutoRenewCovLimitList In AutoRenewCovLimitDataView
                Where AutoRenewCovLimitList.ListItemId = id
                Select AutoRenewCovLimitList.Code).FirstOrDefault()
        End Get
    End Property

    'Private ReadOnly Property PostMigCndDataView As DataView
    '    Get
    '        If (PostMigCndDataView Is Nothing) Then
    '            PostMigCndDataView = LookupListNew.GetPostMigConditionLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
    '        End If
    '        Return PostMigCndDataView
    '    End Get
    'End Property

    Private ReadOnly Property AttributeCodeDataView As List(Of ListItem)
        Get
            If (AttributeCodeDataView Is Nothing) Then
                AttributeCodeDataView = CommonConfigManager.Current.ListManager.GetList("ATTRIBUTE", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return AttributeCodeDataView
        End Get
    End Property

    Private ReadOnly Property AttributeCodeIdFromCode(code As String) As Guid
        Get
            Return (From AttributeCodeList In AttributeCodeDataView
                    Where AttributeCodeList.Code = code
                    Select AttributeCodeList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AttributeCodeDescriptionFromCode(code As String) As String
        Get
            Return (From AttributeCodeList In AttributeCodeDataView
                    Where AttributeCodeList.Code = code
                    Select AttributeCodeList.Translation).FirstOrDefault()
        End Get
    End Property

    'Private ReadOnly Property AttributeCodeDataView As DataView
    '    Get
    '        If (AttributeCodeDataView Is Nothing) Then
    '            AttributeCodeDataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ATTRIBUTE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
    '        End If
    '        Return AttributeCodeDataView
    '    End Get
    'End Property

    Private ReadOnly Property DataTypeDataView As List(Of ListItem)
        Get
            If (DataTypeDataView Is Nothing) Then
                DataTypeDataView = CommonConfigManager.Current.ListManager.GetList("ATBDTYP", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return DataTypeDataView
        End Get
    End Property

    Private ReadOnly Property DataTypeCodeFromId(id As Guid) As String
        Get
            Return (From DataTypeList In DataTypeDataView
                    Where DataTypeList.ListItemId = id
                    Select DataTypeList.Code).FirstOrDefault()
        End Get
    End Property

    'Private ReadOnly Property DataTypeDataView As DataView
    '    Get
    '        If (DataTypeDataView Is Nothing) Then
    '            DataTypeDataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ATTRIBUTE_DATA_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
    '        End If
    '        Return DataTypeDataView
    '    End Get
    'End Property
#End Region

#Region "Methods"

    Public Overrides Sub DataBind()

        PopulateGrid()

    End Sub

    Sub PopulateGrid()

        If (ParentBusinessObject Is Nothing) Then
            Throw New BOInvalidOperationException("Value of ParentBusinessObject can not be null")
        End If

        ' Get View based on Sort Data
        Dim attributeValues As IEnumerable(Of AttributeValue) = ParentBusinessObject.AttributeValues
        Dim dummyAttributeValue As AttributeValue = Nothing
        If (attributeValues.Count() = 0) Then
            ' Add Dummy Object
            dummyAttributeValue = ParentBusinessObject.AttributeValues.GetNewAttributeChild()
            Dim avl As New List(Of AttributeValue)
            avl.Add(dummyAttributeValue)
            attributeValues = avl
            AttributeValueGridView.EditIndex = 0
            State.IsNew = True
        Else
            ' Find Index of Item when being Added or Edited
            If (State.MyBO Is Nothing) Then
                AttributeValueGridView.EditIndex = Page.NO_ITEM_SELECTED_INDEX
            Else
                AttributeValueGridView.EditIndex = DataSourceExtensions.GetSelectedRowIndex(Of AttributeValue)(attributeValues, Function(av) av.Id = State.MyBO.Id)
            End If

        End If

        ' Bind Data to Grid
        AttributeValueGridView.DataSource = attributeValues
        AttributeValueGridView.DataBind()

        ' Hide Dummy Object Row
        If (dummyAttributeValue IsNot Nothing) Then
            AttributeValueGridView.Rows(0).Visible = False
            dummyAttributeValue.Delete()
            State.IsNew = False
        End If

    End Sub

    Public Sub PopulateAttributeValuesGrid(attributeValues As List(Of AttributeValue))
        AttributeValueGridView.EditIndex = Page.NO_ITEM_SELECTED_INDEX
        AttributeValueGridView.DataSource = attributeValues
        AttributeValueGridView.DataBind()
    End Sub
#End Region

#Region "Grid Events"

    Public Sub UiProgCodeDropDown_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = DirectCast(DirectCast(sender, DropDownList).Parent.Parent, GridViewRow)
        Page.PopulateBOProperty(State.MyBO, "AttributeId", DirectCast(sender, DropDownList))
        ShowHideEditRowControls(row, State.MyBO)
    End Sub

    Private Sub ShowHideEditRowControls(pGridViewRow As GridViewRow, oAttributeValue As AttributeValue)

        Dim attributeValueDropDown As DropDownList = CType(pGridViewRow.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
        Dim attributeValueTextBox As TextBox = CType(pGridViewRow.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox)
        Dim attributeValueImageButton As ImageButton = CType(pGridViewRow.FindControl(ATTRIBUTE_VALUE_IMAGEBUTTON_NAME), ImageButton)
        Dim effectiveDateTextBox As TextBox = CType(pGridViewRow.FindControl(EFFECTIVE_DATE_TEXTBOX_NAME), TextBox)
        Dim effectiveDateImageButton As ImageButton = CType(pGridViewRow.FindControl(EFFECTIVE_DATE_IMAGEBUTTON_NAME), ImageButton)
        Dim effectiveDateLabel As Label = CType(pGridViewRow.FindControl(EFFECTIVE_DATE_LABEL_NAME), Label)
        Dim expirationDateTextBox As TextBox = CType(pGridViewRow.FindControl(EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
        Dim expirationDateImageButton As ImageButton = CType(pGridViewRow.FindControl(EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)
        Dim expirationDateLabel As Label = CType(pGridViewRow.FindControl(EXPIRATION_DATE_LABEL_NAME), Label)
        Dim saveImageButton As Button = CType(pGridViewRow.FindControl(SAVE_BUTTON_NAME), Button)
        Dim cancelImageButton As LinkButton = CType(pGridViewRow.FindControl(CANCEL_BUTTON_NAME), LinkButton)

        If (oAttributeValue.AttributeId = Guid.Empty) Then
            attributeValueDropDown.Visible = False
            attributeValueTextBox.Visible = False
            attributeValueImageButton.Visible = False
            effectiveDateTextBox.Visible = False
            effectiveDateImageButton.Visible = False
            effectiveDateLabel.Visible = False
            expirationDateTextBox.Visible = False
            expirationDateImageButton.Visible = False
            expirationDateLabel.Visible = False
            Return
        End If

        Select Case DataTypeCodeFromId(oAttributeValue.Attribute.DataTypeId)
        'Select Case LookupListNew.GetCodeFromId(Me.DataTypeDataView, oAttributeValue.Attribute.DataTypeId)
            Case Codes.ATTRIBUTE_DATE_TYPE__DATE
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = True
                Page.AddCalendarwithTime_New(attributeValueImageButton, attributeValueTextBox)
                Dim dateValue As Date
                If (Date.TryParse(oAttributeValue.Value, dateValue)) Then
                    Page.PopulateControlFromBOProperty(attributeValueTextBox, dateValue)
                Else
                    attributeValueTextBox.Text = String.Empty
                End If

            Case Codes.ATTRIBUTE_DATE_TYPE__YESNO
                attributeValueDropDown.Populate(YesNoDataView.ToArray(), New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
                'Me.Page.BindListControlToDataView(attributeValueDropDown, YesNoDataView)
                attributeValueDropDown.Visible = True
                attributeValueTextBox.Visible = False
                attributeValueImageButton.Visible = False
                Page.SetSelectedItem(attributeValueDropDown, YesNoIdFromCode(oAttributeValue.Value))
                'Me.Page.SetSelectedItem(attributeValueDropDown, LookupListNew.GetIdFromCode(Me.YesNoDataView, oAttributeValue.Value))

            Case Codes.ATTRIBUTE_DATE_TYPE__REINSURANCESTATUS
                attributeValueDropDown.Populate(ReInsStatusDataView.ToArray(), New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
                'Me.Page.BindListControlToDataView(attributeValueDropDown, ReInsStatusDataView)
                attributeValueDropDown.Visible = True
                attributeValueTextBox.Visible = False
                attributeValueImageButton.Visible = False
                Page.SetSelectedItem(attributeValueDropDown, ReInsStatusIdFromCode(oAttributeValue.Value))
                'Me.Page.SetSelectedItem(attributeValueDropDown, LookupListNew.GetIdFromCode(Me.ReInsStatusDataView, oAttributeValue.Value))

            Case Codes.ATTRIBUTE_DATE_TYPE__ACCT_PRORATE
                attributeValueDropDown.Populate(AcctProrateDataView.ToArray(), New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
                'Me.Page.BindListControlToDataView(attributeValueDropDown, AcctProrateDataView)
                attributeValueDropDown.Visible = True
                attributeValueTextBox.Visible = False
                attributeValueImageButton.Visible = False
                Page.SetSelectedItem(attributeValueDropDown, AcctProrateIdFromCode(oAttributeValue.Value))

            Case Codes.ATTRIBUTE_DATE_TYPE__AUTO_RENEW_COV_LIMIT
                attributeValueDropDown.Populate(AutoRenewCovLimitDataView.ToArray(), New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
                'Me.Page.BindListControlToDataView(attributeValueDropDown, AutoRenewCovLimitDataView)
                attributeValueDropDown.Visible = True
                attributeValueTextBox.Visible = False
                attributeValueImageButton.Visible = False
                Page.SetSelectedItem(attributeValueDropDown, AutoRenewCovLimitIdFromCode(oAttributeValue.Value))


            Case Codes.ATTRIBUTE_DATE_TYPE__POST_MIGRATION_CONDITION
                attributeValueDropDown.Populate(PostMigCndDataView.ToArray(), New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
                'Me.Page.BindListControlToDataView(attributeValueDropDown, PostMigCndDataView)
                attributeValueDropDown.Visible = True
                attributeValueTextBox.Visible = False
                attributeValueImageButton.Visible = False
                Page.SetSelectedItem(attributeValueDropDown, PostMigCndIdFromCode(oAttributeValue.Value))
                'Me.Page.SetSelectedItem(attributeValueDropDown, LookupListNew.GetIdFromCode(Me.PostMigCndDataView, oAttributeValue.Value))

            Case Codes.ATTRIBUTE_DATE_TYPE__HEXADECIMAL
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = False
                Page.PopulateControlFromBOProperty(attributeValueTextBox, oAttributeValue.Value)

            Case Codes.ATTRIBUTE_DATE_TYPE__NUMBER
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = False
                Page.PopulateControlFromBOProperty(attributeValueTextBox, oAttributeValue.Value)

            Case Codes.ATTRIBUTE_DATE_TYPE__TEXT
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = False
                Page.PopulateControlFromBOProperty(attributeValueTextBox, oAttributeValue.Value)
        End Select

        If (oAttributeValue.Attribute.UseEffectiveDate = Codes.YESNO_Y) Then

            effectiveDateTextBox.Visible = True
            effectiveDateImageButton.Visible = True
            effectiveDateLabel.Visible = False
            Page.PopulateControlFromBOProperty(effectiveDateTextBox, oAttributeValue.EffectiveDate)
            ' Date Calendars
            Page.AddCalendar_New(effectiveDateImageButton, effectiveDateTextBox)

            ' Expiration Date is available and in past
            expirationDateTextBox.Visible = True
            expirationDateImageButton.Visible = True
            expirationDateLabel.Visible = False
            Page.PopulateControlFromBOProperty(expirationDateTextBox, oAttributeValue.ExpirationDate)
            ' Date Calendars
            Page.AddCalendar_New(expirationDateImageButton, expirationDateTextBox)
        Else
            '''TODO: Transalations for N/A
            effectiveDateTextBox.Visible = False
            effectiveDateImageButton.Visible = False
            effectiveDateLabel.Visible = True
            effectiveDateLabel.Text = "N/A"
            expirationDateTextBox.Visible = False
            expirationDateImageButton.Visible = False
            expirationDateLabel.Visible = True
            expirationDateLabel.Text = "N/A"
        End If
    End Sub

    Public Sub AttributeValueGridView_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles AttributeValueGridView.RowDataBound
        Try

            Dim rowType As DataControlRowType = CType(e.Row.RowType, DataControlRowType)
            If (rowType = DataControlRowType.DataRow) AndAlso (e.Row.DataItem IsNot Nothing) Then
                Dim oAttributeValue As AttributeValue = CType(e.Row.DataItem, AttributeValue)
                Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)

                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                    Dim uiProgCodeDropDown As DropDownList = CType(e.Row.FindControl(UI_PROG_CODE_DROPDOWN_NAME), DropDownList)
                    Dim uiProgCodeLabel As Label = CType(e.Row.FindControl(UI_PROG_CODE_LABEL_NAME), Label)
                    Dim attributeValueDropDown As DropDownList = CType(e.Row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    Dim attributeValueTextBox As TextBox = CType(e.Row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox)
                    Dim attributeValueImageButton As ImageButton = CType(e.Row.FindControl(ATTRIBUTE_VALUE_IMAGEBUTTON_NAME), ImageButton)
                    Dim effectiveDateTextBox As TextBox = CType(e.Row.FindControl(EFFECTIVE_DATE_TEXTBOX_NAME), TextBox)
                    Dim effectiveDateImageButton As ImageButton = CType(e.Row.FindControl(EFFECTIVE_DATE_IMAGEBUTTON_NAME), ImageButton)
                    Dim effectiveDateLabel As Label = CType(e.Row.FindControl(EFFECTIVE_DATE_LABEL_NAME), Label)
                    Dim expirationDateTextBox As TextBox = CType(e.Row.FindControl(EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
                    Dim expirationDateImageButton As ImageButton = CType(e.Row.FindControl(EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)
                    Dim expirationDateLabel As Label = CType(e.Row.FindControl(EXPIRATION_DATE_LABEL_NAME), Label)
                    Dim saveImageButton As Button = CType(e.Row.FindControl(SAVE_BUTTON_NAME), Button)
                    Dim cancelImageButton As LinkButton = CType(e.Row.FindControl(CANCEL_BUTTON_NAME), LinkButton)

                    ' Populate Attributes
                    If (State.IsNew) Then
                        Dim attributeList As New List(Of WebControls.ListItem)
                        For Each ea As ElitaAttribute In ParentBusinessObject.AttributeValues.Attribues
                            If (ea.UseEffectiveDate = "Y") OrElse (ea.AllowDuplicates = "Y") OrElse (ea.Id = oAttributeValue.AttributeId) Then
                                attributeList.Add(New WebControls.ListItem() With {.Value = ea.Id.ToString(), .Text = AttributeCodeDescriptionFromCode(ea.UiProgCode)})
                                'attributeList.Add(New ListItem() With {.Value = ea.Id.ToString(), .Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, ea.UiProgCode))})
                            Else
                                If (ParentBusinessObject.AttributeValues.Where(Function(av) av.AttributeId = ea.Id).Count() = 0) Then
                                    attributeList.Add(New WebControls.ListItem() With {.Value = ea.Id.ToString(), .Text = AttributeCodeDescriptionFromCode(ea.UiProgCode)})
                                    'attributeList.Add(New ListItem() With {.Value = ea.Id.ToString(), .Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, ea.UiProgCode))})
                                End If
                            End If
                        Next
                        Page.BindListControlToArray(uiProgCodeDropDown, attributeList.ToArray(), , , Guid.Empty.ToString)
                        If (oAttributeValue.AttributeId <> Guid.Empty) Then
                            Page.SetSelectedItem(uiProgCodeDropDown, AttributeCodeIdFromCode(oAttributeValue.Attribute.UiProgCode))
                            'Me.Page.SetSelectedItem(uiProgCodeDropDown, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, oAttributeValue.Attribute.UiProgCode))
                        End If
                        uiProgCodeLabel.Visible = False
                        uiProgCodeDropDown.Visible = True
                    Else
                        ' Edit Flow
                        uiProgCodeLabel.Text = AttributeCodeDescriptionFromCode(oAttributeValue.Attribute.UiProgCode)
                        'uiProgCodeLabel.Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, oAttributeValue.Attribute.UiProgCode))
                        uiProgCodeLabel.Visible = True
                        uiProgCodeDropDown.Visible = False
                    End If

                    ShowHideEditRowControls(e.Row, oAttributeValue)

                Else

                    Dim uiProgCodeLabel As Label = CType(e.Row.FindControl(UI_PROG_CODE_LABEL_NAME), Label)
                    Dim attributeValueLabel As Label = CType(e.Row.FindControl(ATTRIBUTE_VALUE_LABEL_NAME), Label)
                    Dim effectiveDateLabel As Label = CType(e.Row.FindControl(EFFECTIVE_DATE_LABEL_NAME), Label)
                    Dim expirationDateLabel As Label = CType(e.Row.FindControl(EXPIRATION_DATE_LABEL_NAME), Label)
                    Dim editButton As ImageButton = CType(e.Row.FindControl(EDIT_BUTTON_NAME), ImageButton)
                    Dim deleteButton As ImageButton = CType(e.Row.FindControl(DELETE_BUTTON_NAME), ImageButton)

                    If (State.MyBO IsNot Nothing) Then
                        editButton.Visible = False
                        deleteButton.Visible = False
                    Else
                        editButton.CommandArgument = oAttributeValue.Id.ToString()
                        deleteButton.CommandArgument = oAttributeValue.Id.ToString()
                        'deleteButton.Attributes.Add("onclick", String.Format("ShowDeleteConfirmation('{0}', '{1}${2}'); return false;", (DirectCast(sender, GridView)).UniqueID, DELETE_COMMAND_NAME, oAttributeValue.Id.ToString()))
                        'deleteButton.Attributes.Add("onclick1", Me.Page.ClientScript.GetPostBackEventReference(DirectCast(sender, GridView), String.Format("{0}${1}", DELETE_COMMAND_NAME, oAttributeValue.Id.ToString())))
                    End If

                    uiProgCodeLabel.Text = AttributeCodeDescriptionFromCode(oAttributeValue.Attribute.UiProgCode)
                    'uiProgCodeLabel.Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, oAttributeValue.Attribute.UiProgCode))
                    Select Case DataTypeCodeFromId(oAttributeValue.Attribute.DataTypeId)
                    'Select Case LookupListNew.GetCodeFromId(Me.DataTypeDataView, oAttributeValue.Attribute.DataTypeId)
                        Case Codes.ATTRIBUTE_DATE_TYPE__DATE
                            Page.PopulateControlFromBOProperty(attributeValueLabel, Date.Parse(oAttributeValue.Value))
                        Case Codes.ATTRIBUTE_DATE_TYPE__YESNO
                            Page.PopulateControlFromBOProperty(attributeValueLabel, YesNoDescriptionFromCode(oAttributeValue.Value))
                            'Me.Page.PopulateControlFromBOProperty(attributeValueLabel, LookupListNew.GetDescriptionFromId(Me.YesNoDataView, LookupListNew.GetIdFromCode(Me.YesNoDataView, oAttributeValue.Value)))
                        Case Codes.ATTRIBUTE_DATE_TYPE__REINSURANCESTATUS
                            Page.PopulateControlFromBOProperty(attributeValueLabel, ReInsStatusDescriptionFromCode(oAttributeValue.Value))
                            'Me.Page.PopulateControlFromBOProperty(attributeValueLabel, LookupListNew.GetDescriptionFromId(Me.ReInsStatusDataView, LookupListNew.GetIdFromCode(Me.ReInsStatusDataView, oAttributeValue.Value)))
                        Case Codes.ATTRIBUTE_DATE_TYPE__POST_MIGRATION_CONDITION
                            Page.PopulateControlFromBOProperty(attributeValueLabel, PostMigCndDescriptionFromCode(oAttributeValue.Value))
                            
                            'Me.Page.PopulateControlFromBOProperty(attributeValueLabel, LookupListNew.GetDescriptionFromId(Me.PostMigCndDataView, LookupListNew.GetIdFromCode(Me.PostMigCndDataView, oAttributeValue.Value)))
                        Case Codes.ATTRIBUTE_DATE_TYPE__ACCT_PRORATE
                            Page.PopulateControlFromBOProperty(attributeValueLabel, AcctProrateDescriptionFromCode(oAttributeValue.Value))
                        Case Codes.ATTRIBUTE_DATE_TYPE__AUTO_RENEW_COV_LIMIT
                            Page.PopulateControlFromBOProperty(attributeValueLabel, AutoRenewCovLimitDescriptionFromCode(oAttributeValue.Value))                       
                        Case Codes.ATTRIBUTE_DATE_TYPE__HEXADECIMAL
                            Page.PopulateControlFromBOProperty(attributeValueLabel, oAttributeValue.Value)
                        Case Codes.ATTRIBUTE_DATE_TYPE__NUMBER
                            Page.PopulateControlFromBOProperty(attributeValueLabel, oAttributeValue.Value)
                        Case Codes.ATTRIBUTE_DATE_TYPE__TEXT
                            Page.PopulateControlFromBOProperty(attributeValueLabel, oAttributeValue.Value)
                    End Select

                    If (oAttributeValue.Attribute.UseEffectiveDate = Codes.YESNO_Y) Then
                        Page.PopulateControlFromBOProperty(effectiveDateLabel, oAttributeValue.EffectiveDate)
                        Page.PopulateControlFromBOProperty(expirationDateLabel, oAttributeValue.ExpirationDate)
                    Else
                        '''TODO: Transalations for N/A
                        effectiveDateLabel.Text = "N/A"
                        expirationDateLabel.Text = "N/A"
                    End If

                    End If
            End If
        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateBOFromForm(pGridView As GridView)
        Dim row As GridViewRow = pGridView.Rows(pGridView.EditIndex)
        With State.MyBO
            If (.AttributeId = Guid.Empty) Then
                .EffectiveDate = Nothing
                .ExpirationDate = Nothing
                .Value = Nothing
                Return
            End If

            If .Attribute.UseEffectiveDate = Codes.YESNO_Y Then
                Dim dateValue As Date
                If (Date.TryParse(CType(row.FindControl(EFFECTIVE_DATE_TEXTBOX_NAME), TextBox).Text, dateValue)) Then
                    .EffectiveDate = dateValue
                Else
                    .EffectiveDate = Nothing
                End If

                If (Date.TryParse(CType(row.FindControl(EXPIRATION_DATE_TEXTBOX_NAME), TextBox).Text, dateValue)) Then
                    .ExpirationDate = dateValue
                Else
                    .ExpirationDate = Nothing
                End If
            Else
                .EffectiveDate = Nothing
                .ExpirationDate = Nothing
            End If

            Select Case DataTypeCodeFromId(.Attribute.DataTypeId)
            'Select Case LookupListNew.GetCodeFromId(Me.DataTypeDataView, .Attribute.DataTypeId)
                Case Codes.ATTRIBUTE_DATE_TYPE__TEXT
                    State.MyBO.Value = CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text
                Case Codes.ATTRIBUTE_DATE_TYPE__HEXADECIMAL
                    State.MyBO.Value = CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text
                Case Codes.ATTRIBUTE_DATE_TYPE__DATE
                    Dim dateValue As Date
                    If (dateValue.TryParse(CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text, dateValue)) Then
                        State.MyBO.Value = dateValue.ToString("MM/dd/yyyyy HH:mm:ss")
                    Else
                        State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__NUMBER
                    Dim numberValue As Double
                    If (Double.TryParse(CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text, numberValue)) Then
                        State.MyBO.Value = numberValue.ToString()
                    Else
                        State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__YESNO
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        State.MyBO.Value = YesNoCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.YesNoDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__REINSURANCESTATUS
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        State.MyBO.Value = ReInsStatusCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.ReInsStatusDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__POST_MIGRATION_CONDITION
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        State.MyBO.Value = PostMigCndCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.PostMigCndDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        State.MyBO.Value = Nothing
                    End If

                Case Codes.ATTRIBUTE_DATE_TYPE__ACCT_PRORATE
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        State.MyBO.Value = AcctProrateCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.PostMigCndDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__AUTO_RENEW_COV_LIMIT
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        State.MyBO.Value = AutoRenewCovLimitCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.PostMigCndDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        State.MyBO.Value = Nothing
                    End If

            End Select

        End With

    End Sub

    Public Sub moAttributeGridView_OnRowCommand(sender As Object, e As GridViewCommandEventArgs) Handles AttributeValueGridView.RowCommand
        Try
            Select Case e.CommandName
                Case CANCEL_BUTTON_NAME
                Case CANCEL_COMMAND_NAME
                    If (State.IsNew) Then
                        State.MyBO.Delete()
                        If (e.CommandName.Equals(CANCEL_BUTTON_NAME)) Then
                            State.MyBO.Save()
                        End If
                        State.MyBO = Nothing
                        State.IsNew = False
                    Else
                        State.MyBO.cancelEdit()
                        State.MyBO = Nothing
                    End If
                    NewButton.Enabled = True

                Case EDIT_COMMAND_NAME
                    State.MyBO = ParentBusinessObject.AttributeValues.Where(Function(av) av.Id = New Guid(e.CommandArgument.ToString())).First()
                    State.MyBO.BeginEdit()
                    NewButton.Enabled = False

                Case DELETE_COMMAND_NAME
                    State.MyBO = ParentBusinessObject.AttributeValues.Where(Function(av) av.Id = New Guid(e.CommandArgument.ToString())).First()
                    State.MyBO.Delete()
                    State.MyBO.Save()

                    State.MyBO = Nothing
                    NewButton.Enabled = True

                Case SAVE_COMMAND_NAME
                    PopulateBOFromForm(CType(sender, GridView))
                    State.MyBO.EndEdit()
                    State.MyBO.Save()
                    State.MyBO = Nothing
                    State.IsNew = False
                    NewButton.Enabled = True
            End Select

            PopulateGrid()

        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_OnRowCreated(sender As Object, e As GridViewRowEventArgs) Handles AttributeValueGridView.RowCreated
        Try
            Page.BaseItemCreated(sender, e)
        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Events"

    Public Sub NewButton_Click(sender As Object, e As EventArgs) Handles NewButton.Click
        Try
            State.MyBO = ParentBusinessObject.AttributeValues.GetNewAttributeChild()
            State.IsNew = True
            NewButton.Enabled = False

            ' Request Re-Population of Grid
            PopulateGrid()

        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class
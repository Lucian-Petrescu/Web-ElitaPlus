Imports Assurant.ElitaPlus.Common.GuidControl
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports System.Collections.Generic

Public Class UserControlAttrtibutes
    Inherits System.Web.UI.UserControl

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BindBoProperties()
        Dim attributeValues As IEnumerable(Of AttributeValue) = ParentBusinessObject.AttributeValues
        For Each av As AttributeValue In attributeValues
            Me.Page.BindBOPropertyToGridHeader(av, "AttributeId", Me.AttributeValueGridView.Columns(GRID_COL_UI_PROG_CODE_IDX))
            Me.Page.BindBOPropertyToGridHeader(av, "Value", Me.AttributeValueGridView.Columns(GRID_COL_ATTRIBUTE_VALUE_IDX))
            Me.Page.BindBOPropertyToGridHeader(av, "EffectiveDate", Me.AttributeValueGridView.Columns(GRID_COL_EFFECTIVE_DATE_IDX))
            Me.Page.BindBOPropertyToGridHeader(av, "ExpirationDate", Me.AttributeValueGridView.Columns(GRID_COL_EXPIRATION_DATE_IDX))
        Next

        If (Me.State.IsNew = True) Then
            Me.Page.BindBOPropertyToGridHeader(Me.State.MyBO, "AttributeId", Me.AttributeValueGridView.Columns(GRID_COL_UI_PROG_CODE_IDX))
            Me.Page.BindBOPropertyToGridHeader(Me.State.MyBO, "Value", Me.AttributeValueGridView.Columns(GRID_COL_ATTRIBUTE_VALUE_IDX))
            Me.Page.BindBOPropertyToGridHeader(Me.State.MyBO, "EffectiveDate", Me.AttributeValueGridView.Columns(GRID_COL_EFFECTIVE_DATE_IDX))
            Me.Page.BindBOPropertyToGridHeader(Me.State.MyBO, "ExpirationDate", Me.AttributeValueGridView.Columns(GRID_COL_EXPIRATION_DATE_IDX))
        End If

    End Sub

    Public Sub TranslateHeaders()
        Me.Page.TranslateGridHeader(Me.AttributeValueGridView)
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
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

    Private ReadOnly Property YesNoDataView As Collections.Generic.List(Of DataElements.ListItem)
        Get
            If (YesNoDataView Is Nothing) Then
                YesNoDataView = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return YesNoDataView
        End Get
    End Property

    Private ReadOnly Property YesNoIdFromCode(ByVal code As String) As Guid
        Get
            Return (From YesNoList In YesNoDataView
                    Where YesNoList.Code = code
                    Select YesNoList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property YesNoDescriptionFromCode(ByVal code As String) As String
        Get
            Return (From YesNoList In YesNoDataView
                    Where YesNoList.Code = code
                    Select YesNoList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property YesNoCodeFromId(ByVal id As Guid) As String
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

    Private ReadOnly Property ReInsStatusDataView As Collections.Generic.List(Of DataElements.ListItem)
        Get
            If (ReInsStatusDataView Is Nothing) Then
                ReInsStatusDataView = (From ris In (CommonConfigManager.Current.ListManager.GetList("REINSURANCE_STATUSES", Thread.CurrentPrincipal.GetLanguageCode()))
                                       Where ris.Code <> "PARTIALLY_REINSURED" And ris.Code <> "PARTIALLY_REJECTED"
                                       Select ris).ToList()
            End If
            Return ReInsStatusDataView
        End Get
    End Property

    Private ReadOnly Property ReInsStatusIdFromCode(ByVal code As String) As Guid
        Get
            Return (From ReInsStatusList In ReInsStatusDataView
                    Where ReInsStatusList.Code = code
                    Select ReInsStatusList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property ReInsStatusDescriptionFromCode(ByVal code As String) As String
        Get
            Return (From ReInsStatusList In ReInsStatusDataView
                    Where ReInsStatusList.Code = code
                    Select ReInsStatusList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property ReInsStatusCodeFromId(ByVal id As Guid) As String
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

    Private ReadOnly Property PostMigCndDataView As Collections.Generic.List(Of DataElements.ListItem)
        Get
            If (PostMigCndDataView Is Nothing) Then
                PostMigCndDataView = CommonConfigManager.Current.ListManager.GetList("POST_MIG_CONDITIONS", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return PostMigCndDataView
        End Get
    End Property

    Private ReadOnly Property PostMigCndIdFromCode(ByVal code As String) As Guid
        Get
            Return (From PostMigCndList In PostMigCndDataView
                    Where PostMigCndList.Code = code
                    Select PostMigCndList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property PostMigCndDescriptionFromCode(ByVal code As String) As String
        Get
            Return (From PostMigCndList In PostMigCndDataView
                    Where PostMigCndList.Code = code
                    Select PostMigCndList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property PostMigCndCodeFromId(ByVal id As Guid) As String
        Get
            Return (From PostMigCndList In PostMigCndDataView
                    Where PostMigCndList.ListItemId = id
                    Select PostMigCndList.Code).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AcctProrateDataView As Collections.Generic.List(Of DataElements.ListItem)
        Get
            If (AcctProrateDataView Is Nothing) Then
                AcctProrateDataView = CommonConfigManager.Current.ListManager.GetList("ACCT_PRORATE", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return AcctProrateDataView
        End Get
    End Property
    
    Private ReadOnly Property AcctProrateIdFromCode(ByVal code As String) As Guid
        Get
            Return (From AcctProrateList In AcctProrateDataView
                Where AcctProrateList.Code = code
                Select AcctProrateList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AcctProrateDescriptionFromCode(ByVal code As String) As String
        Get
            Return (From AcctProrateList In AcctProrateDataView
                Where AcctProrateList.Code = code
                Select AcctProrateList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AcctProrateCodeFromId(ByVal id As Guid) As String
        Get
            Return (From AcctProrateList In AcctProrateDataView
                Where AcctProrateList.ListItemId = id
                Select AcctProrateList.Code).FirstOrDefault()
        End Get
    End Property
    Private ReadOnly Property AutoRenewCovLimitDataView As Collections.Generic.List(Of DataElements.ListItem)
        Get
            If (AutoRenewCovLimitDataView Is Nothing) Then
                AutoRenewCovLimitDataView = CommonConfigManager.Current.ListManager.GetList("AUTO_RENEW_COV_LIMIT", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return AutoRenewCovLimitDataView
        End Get
    End Property
    Private ReadOnly Property AutoRenewCovLimitIdFromCode(ByVal code As String) As Guid
        Get
            Return (From AutoRenewCovLimitList In AutoRenewCovLimitDataView
                Where AutoRenewCovLimitList.Code = code
                Select AutoRenewCovLimitList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AutoRenewCovLimitDescriptionFromCode(ByVal code As String) As String
        Get
            Return (From AutoRenewCovLimitList In AutoRenewCovLimitDataView
                Where AutoRenewCovLimitList.Code = code
                Select AutoRenewCovLimitList.Translation).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AutoRenewCovLimitCodeFromId(ByVal id As Guid) As String
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

    Private ReadOnly Property AttributeCodeDataView As Collections.Generic.List(Of DataElements.ListItem)
        Get
            If (AttributeCodeDataView Is Nothing) Then
                AttributeCodeDataView = CommonConfigManager.Current.ListManager.GetList("ATTRIBUTE", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return AttributeCodeDataView
        End Get
    End Property

    Private ReadOnly Property AttributeCodeIdFromCode(ByVal code As String) As Guid
        Get
            Return (From AttributeCodeList In AttributeCodeDataView
                    Where AttributeCodeList.Code = code
                    Select AttributeCodeList.ListItemId).FirstOrDefault()
        End Get
    End Property

    Private ReadOnly Property AttributeCodeDescriptionFromCode(ByVal code As String) As String
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

    Private ReadOnly Property DataTypeDataView As Collections.Generic.List(Of DataElements.ListItem)
        Get
            If (DataTypeDataView Is Nothing) Then
                DataTypeDataView = CommonConfigManager.Current.ListManager.GetList("ATBDTYP", Thread.CurrentPrincipal.GetLanguageCode()).ToList()
            End If
            Return DataTypeDataView
        End Get
    End Property

    Private ReadOnly Property DataTypeCodeFromId(ByVal id As Guid) As String
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
            Me.State.IsNew = True
        Else
            ' Find Index of Item when being Added or Edited
            If (Me.State.MyBO Is Nothing) Then
                AttributeValueGridView.EditIndex = Me.Page.NO_ITEM_SELECTED_INDEX
            Else
                AttributeValueGridView.EditIndex = DataSourceExtensions.GetSelectedRowIndex(Of AttributeValue)(attributeValues, Function(av) av.Id = Me.State.MyBO.Id)
            End If

        End If

        ' Bind Data to Grid
        AttributeValueGridView.DataSource = attributeValues
        AttributeValueGridView.DataBind()

        ' Hide Dummy Object Row
        If (Not dummyAttributeValue Is Nothing) Then
            AttributeValueGridView.Rows(0).Visible = False
            dummyAttributeValue.Delete()
            Me.State.IsNew = False
        End If

    End Sub

    Public Sub PopulateAttributeValuesGrid(attributeValues As List(Of AttributeValue))
        AttributeValueGridView.EditIndex = Me.Page.NO_ITEM_SELECTED_INDEX
        AttributeValueGridView.DataSource = attributeValues
        AttributeValueGridView.DataBind()
    End Sub
#End Region

#Region "Grid Events"

    Public Sub UiProgCodeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim row As GridViewRow = DirectCast(DirectCast(sender, System.Web.UI.WebControls.DropDownList).Parent.Parent, GridViewRow)
        Me.Page.PopulateBOProperty(Me.State.MyBO, "AttributeId", DirectCast(sender, System.Web.UI.WebControls.DropDownList))
        ShowHideEditRowControls(row, Me.State.MyBO)
    End Sub

    Private Sub ShowHideEditRowControls(ByVal pGridViewRow As GridViewRow, ByVal oAttributeValue As AttributeValue)

        Dim attributeValueDropDown As DropDownList = CType(pGridViewRow.FindControl(Me.ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
        Dim attributeValueTextBox As TextBox = CType(pGridViewRow.FindControl(Me.ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox)
        Dim attributeValueImageButton As ImageButton = CType(pGridViewRow.FindControl(Me.ATTRIBUTE_VALUE_IMAGEBUTTON_NAME), ImageButton)
        Dim effectiveDateTextBox As TextBox = CType(pGridViewRow.FindControl(Me.EFFECTIVE_DATE_TEXTBOX_NAME), TextBox)
        Dim effectiveDateImageButton As ImageButton = CType(pGridViewRow.FindControl(Me.EFFECTIVE_DATE_IMAGEBUTTON_NAME), ImageButton)
        Dim effectiveDateLabel As Label = CType(pGridViewRow.FindControl(Me.EFFECTIVE_DATE_LABEL_NAME), Label)
        Dim expirationDateTextBox As TextBox = CType(pGridViewRow.FindControl(Me.EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
        Dim expirationDateImageButton As ImageButton = CType(pGridViewRow.FindControl(Me.EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)
        Dim expirationDateLabel As Label = CType(pGridViewRow.FindControl(Me.EXPIRATION_DATE_LABEL_NAME), Label)
        Dim saveImageButton As Button = CType(pGridViewRow.FindControl(Me.SAVE_BUTTON_NAME), Button)
        Dim cancelImageButton As LinkButton = CType(pGridViewRow.FindControl(Me.CANCEL_BUTTON_NAME), LinkButton)

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

        Select Case Me.DataTypeCodeFromId(oAttributeValue.Attribute.DataTypeId)
        'Select Case LookupListNew.GetCodeFromId(Me.DataTypeDataView, oAttributeValue.Attribute.DataTypeId)
            Case Codes.ATTRIBUTE_DATE_TYPE__DATE
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = True
                Me.Page.AddCalendarwithTime_New(attributeValueImageButton, attributeValueTextBox)
                Dim dateValue As Date
                If (Date.TryParse(oAttributeValue.Value, dateValue)) Then
                    Me.Page.PopulateControlFromBOProperty(attributeValueTextBox, dateValue)
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
                Me.Page.SetSelectedItem(attributeValueDropDown, Me.YesNoIdFromCode(oAttributeValue.Value))
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
                Me.Page.SetSelectedItem(attributeValueDropDown, Me.ReInsStatusIdFromCode(oAttributeValue.Value))
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
                Me.Page.SetSelectedItem(attributeValueDropDown, Me.AcctProrateIdFromCode(oAttributeValue.Value))

            Case Codes.ATTRIBUTE_DATE_TYPE__AUTO_RENEW_COV_LIMIT
                attributeValueDropDown.Populate(AutoRenewCovLimitDataView.ToArray(), New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
                'Me.Page.BindListControlToDataView(attributeValueDropDown, AutoRenewCovLimitDataView)
                attributeValueDropDown.Visible = True
                attributeValueTextBox.Visible = False
                attributeValueImageButton.Visible = False
                Me.Page.SetSelectedItem(attributeValueDropDown, Me.AutoRenewCovLimitIdFromCode(oAttributeValue.Value))


            Case Codes.ATTRIBUTE_DATE_TYPE__POST_MIGRATION_CONDITION
                attributeValueDropDown.Populate(PostMigCndDataView.ToArray(), New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
                'Me.Page.BindListControlToDataView(attributeValueDropDown, PostMigCndDataView)
                attributeValueDropDown.Visible = True
                attributeValueTextBox.Visible = False
                attributeValueImageButton.Visible = False
                Me.Page.SetSelectedItem(attributeValueDropDown, Me.PostMigCndIdFromCode(oAttributeValue.Value))
                'Me.Page.SetSelectedItem(attributeValueDropDown, LookupListNew.GetIdFromCode(Me.PostMigCndDataView, oAttributeValue.Value))

            Case Codes.ATTRIBUTE_DATE_TYPE__HEXADECIMAL
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = False
                Me.Page.PopulateControlFromBOProperty(attributeValueTextBox, oAttributeValue.Value)

            Case Codes.ATTRIBUTE_DATE_TYPE__NUMBER
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = False
                Me.Page.PopulateControlFromBOProperty(attributeValueTextBox, oAttributeValue.Value)

            Case Codes.ATTRIBUTE_DATE_TYPE__TEXT
                attributeValueDropDown.Visible = False
                attributeValueTextBox.Visible = True
                attributeValueImageButton.Visible = False
                Me.Page.PopulateControlFromBOProperty(attributeValueTextBox, oAttributeValue.Value)
        End Select

        If (oAttributeValue.Attribute.UseEffectiveDate = Codes.YESNO_Y) Then

            effectiveDateTextBox.Visible = True
            effectiveDateImageButton.Visible = True
            effectiveDateLabel.Visible = False
            Me.Page.PopulateControlFromBOProperty(effectiveDateTextBox, oAttributeValue.EffectiveDate)
            ' Date Calendars
            Me.Page.AddCalendar_New(effectiveDateImageButton, effectiveDateTextBox)

            ' Expiration Date is available and in past
            expirationDateTextBox.Visible = True
            expirationDateImageButton.Visible = True
            expirationDateLabel.Visible = False
            Me.Page.PopulateControlFromBOProperty(expirationDateTextBox, oAttributeValue.ExpirationDate)
            ' Date Calendars
            Me.Page.AddCalendar_New(expirationDateImageButton, expirationDateTextBox)
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

    Public Sub AttributeValueGridView_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles AttributeValueGridView.RowDataBound
        Try

            Dim rowType As DataControlRowType = CType(e.Row.RowType, DataControlRowType)
            If (rowType = DataControlRowType.DataRow) AndAlso (Not e.Row.DataItem Is Nothing) Then
                Dim oAttributeValue As AttributeValue = CType(e.Row.DataItem, AttributeValue)
                Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)

                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                    Dim uiProgCodeDropDown As DropDownList = CType(e.Row.FindControl(Me.UI_PROG_CODE_DROPDOWN_NAME), DropDownList)
                    Dim uiProgCodeLabel As Label = CType(e.Row.FindControl(Me.UI_PROG_CODE_LABEL_NAME), Label)
                    Dim attributeValueDropDown As DropDownList = CType(e.Row.FindControl(Me.ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    Dim attributeValueTextBox As TextBox = CType(e.Row.FindControl(Me.ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox)
                    Dim attributeValueImageButton As ImageButton = CType(e.Row.FindControl(Me.ATTRIBUTE_VALUE_IMAGEBUTTON_NAME), ImageButton)
                    Dim effectiveDateTextBox As TextBox = CType(e.Row.FindControl(Me.EFFECTIVE_DATE_TEXTBOX_NAME), TextBox)
                    Dim effectiveDateImageButton As ImageButton = CType(e.Row.FindControl(Me.EFFECTIVE_DATE_IMAGEBUTTON_NAME), ImageButton)
                    Dim effectiveDateLabel As Label = CType(e.Row.FindControl(Me.EFFECTIVE_DATE_LABEL_NAME), Label)
                    Dim expirationDateTextBox As TextBox = CType(e.Row.FindControl(Me.EXPIRATION_DATE_TEXTBOX_NAME), TextBox)
                    Dim expirationDateImageButton As ImageButton = CType(e.Row.FindControl(Me.EXPIRATION_DATE_IMAGEBUTTON_NAME), ImageButton)
                    Dim expirationDateLabel As Label = CType(e.Row.FindControl(Me.EXPIRATION_DATE_LABEL_NAME), Label)
                    Dim saveImageButton As Button = CType(e.Row.FindControl(Me.SAVE_BUTTON_NAME), Button)
                    Dim cancelImageButton As LinkButton = CType(e.Row.FindControl(Me.CANCEL_BUTTON_NAME), LinkButton)

                    ' Populate Attributes
                    If (Me.State.IsNew) Then
                        Dim attributeList As New List(Of ListItem)
                        For Each ea As ElitaAttribute In Me.ParentBusinessObject.AttributeValues.Attribues
                            If (ea.UseEffectiveDate = "Y") OrElse (ea.AllowDuplicates = "Y") OrElse (ea.Id = oAttributeValue.AttributeId) Then
                                attributeList.Add(New ListItem() With {.Value = ea.Id.ToString(), .Text = Me.AttributeCodeDescriptionFromCode(ea.UiProgCode)})
                                'attributeList.Add(New ListItem() With {.Value = ea.Id.ToString(), .Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, ea.UiProgCode))})
                            Else
                                If (Me.ParentBusinessObject.AttributeValues.Where(Function(av) av.AttributeId = ea.Id).Count() = 0) Then
                                    attributeList.Add(New ListItem() With {.Value = ea.Id.ToString(), .Text = Me.AttributeCodeDescriptionFromCode(ea.UiProgCode)})
                                    'attributeList.Add(New ListItem() With {.Value = ea.Id.ToString(), .Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, ea.UiProgCode))})
                                End If
                            End If
                        Next
                        Me.Page.BindListControlToArray(uiProgCodeDropDown, attributeList.ToArray(), , , Guid.Empty.ToString)
                        If (oAttributeValue.AttributeId <> Guid.Empty) Then
                            Me.Page.SetSelectedItem(uiProgCodeDropDown, Me.AttributeCodeIdFromCode(oAttributeValue.Attribute.UiProgCode))
                            'Me.Page.SetSelectedItem(uiProgCodeDropDown, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, oAttributeValue.Attribute.UiProgCode))
                        End If
                        uiProgCodeLabel.Visible = False
                        uiProgCodeDropDown.Visible = True
                    Else
                        ' Edit Flow
                        uiProgCodeLabel.Text = Me.AttributeCodeDescriptionFromCode(oAttributeValue.Attribute.UiProgCode)
                        'uiProgCodeLabel.Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, oAttributeValue.Attribute.UiProgCode))
                        uiProgCodeLabel.Visible = True
                        uiProgCodeDropDown.Visible = False
                    End If

                    ShowHideEditRowControls(e.Row, oAttributeValue)

                Else

                    Dim uiProgCodeLabel As Label = CType(e.Row.FindControl(Me.UI_PROG_CODE_LABEL_NAME), Label)
                    Dim attributeValueLabel As Label = CType(e.Row.FindControl(Me.ATTRIBUTE_VALUE_LABEL_NAME), Label)
                    Dim effectiveDateLabel As Label = CType(e.Row.FindControl(Me.EFFECTIVE_DATE_LABEL_NAME), Label)
                    Dim expirationDateLabel As Label = CType(e.Row.FindControl(Me.EXPIRATION_DATE_LABEL_NAME), Label)
                    Dim editButton As ImageButton = CType(e.Row.FindControl(Me.EDIT_BUTTON_NAME), ImageButton)
                    Dim deleteButton As ImageButton = CType(e.Row.FindControl(Me.DELETE_BUTTON_NAME), ImageButton)

                    If (Not Me.State.MyBO Is Nothing) Then
                        editButton.Visible = False
                        deleteButton.Visible = False
                    Else
                        editButton.CommandArgument = oAttributeValue.Id.ToString()
                        deleteButton.CommandArgument = oAttributeValue.Id.ToString()
                        'deleteButton.Attributes.Add("onclick", String.Format("ShowDeleteConfirmation('{0}', '{1}${2}'); return false;", (DirectCast(sender, GridView)).UniqueID, DELETE_COMMAND_NAME, oAttributeValue.Id.ToString()))
                        'deleteButton.Attributes.Add("onclick1", Me.Page.ClientScript.GetPostBackEventReference(DirectCast(sender, GridView), String.Format("{0}${1}", DELETE_COMMAND_NAME, oAttributeValue.Id.ToString())))
                    End If

                    uiProgCodeLabel.Text = Me.AttributeCodeDescriptionFromCode(oAttributeValue.Attribute.UiProgCode)
                    'uiProgCodeLabel.Text = LookupListNew.GetDescriptionFromId(Me.AttributeCodeDataView, LookupListNew.GetIdFromCode(Me.AttributeCodeDataView, oAttributeValue.Attribute.UiProgCode))
                    Select Case Me.DataTypeCodeFromId(oAttributeValue.Attribute.DataTypeId)
                    'Select Case LookupListNew.GetCodeFromId(Me.DataTypeDataView, oAttributeValue.Attribute.DataTypeId)
                        Case Codes.ATTRIBUTE_DATE_TYPE__DATE
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, Date.Parse(oAttributeValue.Value))
                        Case Codes.ATTRIBUTE_DATE_TYPE__YESNO
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, Me.YesNoDescriptionFromCode(oAttributeValue.Value))
                            'Me.Page.PopulateControlFromBOProperty(attributeValueLabel, LookupListNew.GetDescriptionFromId(Me.YesNoDataView, LookupListNew.GetIdFromCode(Me.YesNoDataView, oAttributeValue.Value)))
                        Case Codes.ATTRIBUTE_DATE_TYPE__REINSURANCESTATUS
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, ReInsStatusDescriptionFromCode(oAttributeValue.Value))
                            'Me.Page.PopulateControlFromBOProperty(attributeValueLabel, LookupListNew.GetDescriptionFromId(Me.ReInsStatusDataView, LookupListNew.GetIdFromCode(Me.ReInsStatusDataView, oAttributeValue.Value)))
                        Case Codes.ATTRIBUTE_DATE_TYPE__POST_MIGRATION_CONDITION
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, Me.PostMigCndDescriptionFromCode(oAttributeValue.Value))
                            
                            'Me.Page.PopulateControlFromBOProperty(attributeValueLabel, LookupListNew.GetDescriptionFromId(Me.PostMigCndDataView, LookupListNew.GetIdFromCode(Me.PostMigCndDataView, oAttributeValue.Value)))
                        Case Codes.ATTRIBUTE_DATE_TYPE__ACCT_PRORATE
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, Me.AcctProrateDescriptionFromCode(oAttributeValue.Value))
                        Case Codes.ATTRIBUTE_DATE_TYPE__AUTO_RENEW_COV_LIMIT
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, Me.AutoRenewCovLimitDescriptionFromCode(oAttributeValue.Value))                       
                        Case Codes.ATTRIBUTE_DATE_TYPE__HEXADECIMAL
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, oAttributeValue.Value)
                        Case Codes.ATTRIBUTE_DATE_TYPE__NUMBER
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, oAttributeValue.Value)
                        Case Codes.ATTRIBUTE_DATE_TYPE__TEXT
                            Me.Page.PopulateControlFromBOProperty(attributeValueLabel, oAttributeValue.Value)
                    End Select

                    If (oAttributeValue.Attribute.UseEffectiveDate = Codes.YESNO_Y) Then
                        Me.Page.PopulateControlFromBOProperty(effectiveDateLabel, oAttributeValue.EffectiveDate)
                        Me.Page.PopulateControlFromBOProperty(expirationDateLabel, oAttributeValue.ExpirationDate)
                    Else
                        '''TODO: Transalations for N/A
                        effectiveDateLabel.Text = "N/A"
                        expirationDateLabel.Text = "N/A"
                    End If

                    End If
            End If
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateBOFromForm(ByVal pGridView As GridView)
        Dim row As GridViewRow = pGridView.Rows(pGridView.EditIndex)
        With Me.State.MyBO
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

            Select Case Me.DataTypeCodeFromId(.Attribute.DataTypeId)
            'Select Case LookupListNew.GetCodeFromId(Me.DataTypeDataView, .Attribute.DataTypeId)
                Case Codes.ATTRIBUTE_DATE_TYPE__TEXT
                    Me.State.MyBO.Value = CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text
                Case Codes.ATTRIBUTE_DATE_TYPE__HEXADECIMAL
                    Me.State.MyBO.Value = CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text
                Case Codes.ATTRIBUTE_DATE_TYPE__DATE
                    Dim dateValue As Date
                    If (dateValue.TryParse(CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text, dateValue)) Then
                        Me.State.MyBO.Value = dateValue.ToString("MM/dd/yyyyy HH:mm:ss")
                    Else
                        Me.State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__NUMBER
                    Dim numberValue As Double
                    If (Double.TryParse(CType(row.FindControl(ATTRIBUTE_VALUE_TEXTBOX_NAME), TextBox).Text, numberValue)) Then
                        Me.State.MyBO.Value = numberValue.ToString()
                    Else
                        Me.State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__YESNO
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        Me.State.MyBO.Value = Me.YesNoCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.YesNoDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        Me.State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__REINSURANCESTATUS
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        Me.State.MyBO.Value = Me.ReInsStatusCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.ReInsStatusDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        Me.State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__POST_MIGRATION_CONDITION
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        Me.State.MyBO.Value = Me.PostMigCndCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.PostMigCndDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        Me.State.MyBO.Value = Nothing
                    End If

                Case Codes.ATTRIBUTE_DATE_TYPE__ACCT_PRORATE
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        Me.State.MyBO.Value = Me.AcctProrateCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.PostMigCndDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        Me.State.MyBO.Value = Nothing
                    End If
                Case Codes.ATTRIBUTE_DATE_TYPE__AUTO_RENEW_COV_LIMIT
                    Dim attributeValueDropDown As DropDownList = CType(row.FindControl(ATTRIBUTE_VALUE_DROPDOWN_NAME), DropDownList)
                    If (attributeValueDropDown.SelectedIndex <> -1) Then
                        Me.State.MyBO.Value = Me.AutoRenewCovLimitCodeFromId(New Guid(attributeValueDropDown.SelectedValue))
                        'Me.State.MyBO.Value = LookupListNew.GetCodeFromId(Me.PostMigCndDataView, New Guid(attributeValueDropDown.SelectedValue))
                    Else
                        Me.State.MyBO.Value = Nothing
                    End If

            End Select

        End With

    End Sub

    Public Sub moAttributeGridView_OnRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles AttributeValueGridView.RowCommand
        Try
            Select Case e.CommandName
                Case CANCEL_BUTTON_NAME
                Case CANCEL_COMMAND_NAME
                    If (Me.State.IsNew) Then
                        Me.State.MyBO.Delete()
                        If (e.CommandName.Equals(CANCEL_BUTTON_NAME)) Then
                            Me.State.MyBO.Save()
                        End If
                        Me.State.MyBO = Nothing
                        Me.State.IsNew = False
                    Else
                        Me.State.MyBO.cancelEdit()
                        Me.State.MyBO = Nothing
                    End If
                    Me.NewButton.Enabled = True

                Case EDIT_COMMAND_NAME
                    Me.State.MyBO = Me.ParentBusinessObject.AttributeValues.Where(Function(av) av.Id = New Guid(e.CommandArgument.ToString())).First()
                    Me.State.MyBO.BeginEdit()
                    Me.NewButton.Enabled = False

                Case DELETE_COMMAND_NAME
                    Me.State.MyBO = Me.ParentBusinessObject.AttributeValues.Where(Function(av) av.Id = New Guid(e.CommandArgument.ToString())).First()
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()

                    Me.State.MyBO = Nothing
                    Me.NewButton.Enabled = True

                Case SAVE_COMMAND_NAME
                    PopulateBOFromForm(CType(sender, GridView))
                    Me.State.MyBO.EndEdit()
                    Me.State.MyBO.Save()
                    Me.State.MyBO = Nothing
                    Me.State.IsNew = False
                    Me.NewButton.Enabled = True
            End Select

            PopulateGrid()

        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles AttributeValueGridView.RowCreated
        Try
            Me.Page.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Events"

    Public Sub NewButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewButton.Click
        Try
            Me.State.MyBO = Me.ParentBusinessObject.AttributeValues.GetNewAttributeChild()
            Me.State.IsNew = True
            Me.NewButton.Enabled = False

            ' Request Re-Population of Grid
            PopulateGrid()

        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class
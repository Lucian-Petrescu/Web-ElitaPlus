'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (10/22/2004)  ********************
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class ZipDistrictForm
    Inherits ElitaPlusPage



    Protected WithEvents ErrorCtrl As ErrorController


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

#Region "Constants"
    Public Const URL As String = "ZipDistrictForm.aspx"
    Private Const MULTIPLIER As Integer = 6
    Private Const BASE_WIDTH As Integer = 50
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ZipDistrict
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ZipDistrict, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As ZipDistrict
        Public ScreenSnapShotBO As ZipDistrict

        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public IsNewBO As Boolean = False
        Public IsAnyAdded As Boolean = False
        Public IsAny As Boolean = False
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New ZipDistrict(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                'Date Calendars
                MenuEnabled = False
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New ZipDistrict
                    State.IsNewBO = True
                End If
                PopulateFormFromBOs()
                EnableDisableFields()
                AdjustAddButtonWidthBasedOnTranslationWidth()
            End If
            DisplayProgressBarOnClick(btnSave_WRITE, Message.MSG_PERFORMING_REQUEST)
            DisplayProgressBarOnClick(ButtonAddZipCode, Message.MSG_PERFORMING_REQUEST)
            'Me.DisplayProgressBarOnClick(Me.btnDelete_WRITE, Message.MSG_PERFORMING_REQUEST)
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetVisibleControl(Me, btnNew_WRITE, True)
        ControlMgr.SetVisibleControl(Me, RadioButtonListSingleOrRange, True)
        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            'ControlMgr.SetEnableControl(Me, Me.mpHoriz, False)
        End If

        'WRITE YOU OWN CODE HERE
        ControlMgr.SetVisibleControl(Me, TextboxFrom, False)
        ControlMgr.SetVisibleControl(Me, LabelSeparator, False)
        ControlMgr.SetVisibleControl(Me, TextboxTo, False)
        Select Case RadioButtonListSingleOrRange.SelectedValue
            Case "Single"
                ControlMgr.SetVisibleControl(Me, TextboxFrom, True)
                ControlMgr.SetVisibleControl(Me, LabelSeparator, False)
                ControlMgr.SetVisibleControl(Me, TextboxTo, False)
                ControlMgr.SetVisibleControl(Me, ButtonAddZipCode, True)
            Case "Range"
                ControlMgr.SetVisibleControl(Me, TextboxFrom, True)
                ControlMgr.SetVisibleControl(Me, LabelSeparator, True)
                ControlMgr.SetVisibleControl(Me, TextboxTo, True)
                ControlMgr.SetVisibleControl(Me, ButtonAddZipCode, True)
            Case "Any"
                ControlMgr.SetVisibleControl(Me, LabelSeparator, False)
                ControlMgr.SetVisibleControl(Me, TextboxTo, False)
                ControlMgr.SetVisibleControl(Me, TextboxFrom, False)
                ControlMgr.SetVisibleControl(Me, ButtonAddZipCode, False)
                AddAnyZipCode()
        End Select

        If State.IsAny Then
            ControlMgr.SetVisibleControl(Me, TextboxFrom, False)
            ControlMgr.SetVisibleControl(Me, LabelSeparator, False)
            ControlMgr.SetVisibleControl(Me, TextboxTo, False)
            ControlMgr.SetVisibleControl(Me, ButtonAddZipCode, False)
            ControlMgr.SetVisibleControl(Me, RadioButtonListSingleOrRange, False)
        End If

    End Sub

    Private Sub AddAnyZipCode()
        If State.IsAnyAdded = False Then
            SetSelectedAll(ListBoxZipCodes)
            State.MyBO.DeleteZipCodes(GetSelectedList(ListBoxZipCodes))
            State.MyBO.AddZipCode("*")
            State.IsAnyAdded = True
            PopulateZipCodes()

        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "ShortDesc", LabelShortDesc)
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateZipCodes()
        'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        Dim dv As ZipDistrict.ZipCodeSelectionView = State.MyBO.GetZipCodeSelectionView
        BindListControlToDataView(ListBoxZipCodes, dv, ZipDistrict.ZipCodeSelectionView.COL_NAME_ZIP_CODE, ZipDistrict.ZipCodeSelectionView.COL_NAME_DETAIL_ID, False)
        State.IsAny = isAnyFound()
    End Sub

    Protected Sub PopulateFormFromBOs()
        Dim oCountry As Country
        With State.MyBO

            PopulateControlFromBOProperty(TextboxShortDesc, .ShortDesc)
            PopulateControlFromBOProperty(TextboxDescription, .Description)
            'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            Dim usercountryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
            Dim FilteredRecord As ListItem() = (From x In usercountryLkl
                                                Where (ElitaPlusIdentity.Current.ActiveUser.Countries).Contains(x.ListItemId)
                                                Select x).ToArray()
            moCountryDrop.Populate(FilteredRecord, New PopulateOptions())
            If State.IsNewBO Then
                ' New one
                moCountryDrop.SelectedIndex = 0
                PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, GetSelectedDescription(moCountryDrop))
                PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop)
            Else
                oCountry = New Country(.CountryId)
                SetSelectedItem(moCountryDrop, .CountryId)
                PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
            End If

            If moCountryDrop.Items.Count > 1 Then
                ' Multiple Countries
                ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
            Else
                ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            End If
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, True)
            PopulateZipCodes()

        End With

        If Not IsPostBack Then
            LabelZipCodes.Text &= ":"
        End If

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "ShortDesc", TextboxShortDesc)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New ZipDistrict
        State.IsNewBO = True
        PopulateFormFromBOs()
        EnableDisableFields()

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub AdjustAddButtonWidthBasedOnTranslationWidth()
        ButtonAddZipCode.Width = New Unit(BASE_WIDTH + (ButtonAddZipCode.Text.Length * MULTIPLIER))
    End Sub

    Private Function SetSelectedAll(oListControl As ListControl) As Integer
        Dim oItem As System.Web.UI.WebControls.ListItem

        For Each oItem In oListControl.Items
            oItem.Selected = True
        Next
    End Function

    Private Function isAnyFound() As Boolean
        Dim oItem As System.Web.UI.WebControls.ListItem

        oItem = ListBoxZipCodes.Items.FindByText("*")

        If oItem Is Nothing Then
            Return False
        End If

        Return True

    End Function

#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                'If Me.RadioButtonListSingleOrRange.SelectedValue <> "Any" Then
                'If Me.RadioButtonListSingleOrRange.SelectedValue = "Single" Or Not Me.State.MyBO.IsNew Then
                'If Me.State.MyBO.IsNew Then
                State.MyBO.Save()
                State.IsNewBO = False
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                'ElseIf Me.RadioButtonListSingleOrRange.SelectedValue <> "Any" Then
                '    Dim intResult As Integer = Me.State.MyBO.ZDAndDetail_Batch_Insert()
                '    If intResult = Me.State.MyBO.NO_ERROR Then
                '        Me.State.MyBO.Save()
                '        Me.State.IsNewBO = False
                '        Me.State.HasDataChanged = True
                '        Me.PopulateFormFromBOs()
                '        Me.EnableDisableFields()
                '        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                '    End If
                'Else
                '    If Not Me.State.MyBO.IsNew Then
                '        Me.State.MyBO.Save()
                '        Me.State.IsNewBO = False
                '        Me.State.HasDataChanged = True
                '        Me.PopulateFormFromBOs()
                '        Me.EnableDisableFields()
                '        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                '    End If
                'End If
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New ZipDistrict(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New ZipDistrict
            End If
            State.IsAnyAdded = False
            RadioButtonListSingleOrRange.SelectedValue = "Single"
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub



    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            'Me.State.MyBO.Delete()
            'Me.State.MyBO.Save()
            State.MyBO.ZDAndDetail_Batch_Delete()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#Region "Detail Clicks"
    Private Sub RadioButtonListSingleOrRange_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RadioButtonListSingleOrRange.SelectedIndexChanged
        Try
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub ButtonAddZipCode_Click(sender As Object, e As System.EventArgs) Handles ButtonAddZipCode.Click
        Try

            'If Me.State.IsNewBO Then
            '    Me.PopulateBOProperty(Me.State.MyBO, "CountryId", moCountryDrop)
            '    Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            'End If

            If RadioButtonListSingleOrRange.SelectedValue = "Single" Then
                State.MyBO.AddZipCode(TextboxFrom.Text)
            ElseIf RadioButtonListSingleOrRange.SelectedValue = "Range" Then
                If State.IsNewBO Then
                    State.MyBO.AddZipCodeRangeNew(TextboxFrom.Text, TextboxTo.Text)
                Else
                    State.MyBO.AddZipCodeRange(TextboxFrom.Text, TextboxTo.Text)
                End If

            End If
            PopulateZipCodes()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteZipCode_Click(sender As Object, e As System.EventArgs) Handles btnDeleteZipCode.Click
        Try
            If GetSelectedList(ListBoxZipCodes) IsNot Nothing Then
                State.MyBO.DeleteZipCodes(GetSelectedList(ListBoxZipCodes))
                State.IsAnyAdded = False
                RadioButtonListSingleOrRange.SelectedValue = "Single"
                PopulateZipCodes()
                EnableDisableFields()
            Else
                DisplayMessage(Message.MSG_NOTHING_SELECTED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region



#End Region





End Class



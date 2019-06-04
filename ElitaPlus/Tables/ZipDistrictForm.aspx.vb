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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ZipDistrict, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New ZipDistrict(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                'Date Calendars
                Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ZipDistrict
                    Me.State.IsNewBO = True
                End If
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.AdjustAddButtonWidthBasedOnTranslationWidth()
            End If
            Me.DisplayProgressBarOnClick(Me.btnSave_WRITE, Message.MSG_PERFORMING_REQUEST)
            Me.DisplayProgressBarOnClick(Me.ButtonAddZipCode, Message.MSG_PERFORMING_REQUEST)
            'Me.DisplayProgressBarOnClick(Me.btnDelete_WRITE, Message.MSG_PERFORMING_REQUEST)
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
        ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetVisibleControl(Me, btnNew_WRITE, True)
        ControlMgr.SetVisibleControl(Me, RadioButtonListSingleOrRange, True)
        'Now disable depebding on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            'ControlMgr.SetEnableControl(Me, Me.mpHoriz, False)
        End If

        'WRITE YOU OWN CODE HERE
        ControlMgr.SetVisibleControl(Me, TextboxFrom, False)
        ControlMgr.SetVisibleControl(Me, LabelSeparator, False)
        ControlMgr.SetVisibleControl(Me, TextboxTo, False)
        Select Case Me.RadioButtonListSingleOrRange.SelectedValue
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

        If Me.State.IsAny Then
            ControlMgr.SetVisibleControl(Me, TextboxFrom, False)
            ControlMgr.SetVisibleControl(Me, LabelSeparator, False)
            ControlMgr.SetVisibleControl(Me, TextboxTo, False)
            ControlMgr.SetVisibleControl(Me, ButtonAddZipCode, False)
            ControlMgr.SetVisibleControl(Me, RadioButtonListSingleOrRange, False)
        End If

    End Sub

    Private Sub AddAnyZipCode()
        If Me.State.IsAnyAdded = False Then
            SetSelectedAll(Me.ListBoxZipCodes)
            Me.State.MyBO.DeleteZipCodes(Me.GetSelectedList(Me.ListBoxZipCodes))
            Me.State.MyBO.AddZipCode("*")
            Me.State.IsAnyAdded = True
            PopulateZipCodes()

        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ShortDesc", Me.LabelShortDesc)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateZipCodes()
        'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        Dim dv As ZipDistrict.ZipCodeSelectionView = Me.State.MyBO.GetZipCodeSelectionView
        Me.BindListControlToDataView(Me.ListBoxZipCodes, dv, ZipDistrict.ZipCodeSelectionView.COL_NAME_ZIP_CODE, ZipDistrict.ZipCodeSelectionView.COL_NAME_DETAIL_ID, False)
        Me.State.IsAny = isAnyFound()
    End Sub

    Protected Sub PopulateFormFromBOs()
        Dim oCountry As Country
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.TextboxShortDesc, .ShortDesc)
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            Dim usercountryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
            Dim FilteredRecord As ListItem() = (From x In usercountryLkl
                                                Where (ElitaPlusIdentity.Current.ActiveUser.Countries).Contains(x.ListItemId)
                                                Select x).ToArray()
            moCountryDrop.Populate(FilteredRecord, New PopulateOptions())
            If Me.State.IsNewBO Then
                ' New one
                moCountryDrop.SelectedIndex = 0
                Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
                Me.PopulateBOProperty(Me.State.MyBO, "CountryId", moCountryDrop)
            Else
                oCountry = New Country(.CountryId)
                Me.SetSelectedItem(moCountryDrop, .CountryId)
                Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
            End If

            If moCountryDrop.Items.Count > 1 Then
                ' Multiple Countries
                ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
            Else
                ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            End If
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, True)
            Me.PopulateZipCodes()

        End With

        If Not Me.IsPostBack Then
            Me.LabelZipCodes.Text &= ":"
        End If

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

        Me.State.MyBO = New ZipDistrict
        Me.State.IsNewBO = True
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

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
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub AdjustAddButtonWidthBasedOnTranslationWidth()
        Me.ButtonAddZipCode.Width = New Unit(Me.BASE_WIDTH + (ButtonAddZipCode.Text.Length * Me.MULTIPLIER))
    End Sub

    Private Function SetSelectedAll(ByVal oListControl As ListControl) As Integer
        Dim oItem As System.Web.UI.WebControls.ListItem

        For Each oItem In oListControl.Items
            oItem.Selected = True
        Next
    End Function

    Private Function isAnyFound() As Boolean
        Dim oItem As System.Web.UI.WebControls.ListItem

        oItem = Me.ListBoxZipCodes.Items.FindByText("*")

        If oItem Is Nothing Then
            Return False
        End If

        Return True

    End Function

#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                'If Me.RadioButtonListSingleOrRange.SelectedValue <> "Any" Then
                'If Me.RadioButtonListSingleOrRange.SelectedValue = "Single" Or Not Me.State.MyBO.IsNew Then
                'If Me.State.MyBO.IsNew Then
                Me.State.MyBO.Save()
                Me.State.IsNewBO = False
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
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
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New ZipDistrict(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New ZipDistrict
            End If
            Me.State.IsAnyAdded = False
            Me.RadioButtonListSingleOrRange.SelectedValue = "Single"
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub



    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            'Me.State.MyBO.Delete()
            'Me.State.MyBO.Save()
            Me.State.MyBO.ZDAndDetail_Batch_Delete()
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
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#Region "Detail Clicks"
    Private Sub RadioButtonListSingleOrRange_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonListSingleOrRange.SelectedIndexChanged
        Try
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub ButtonAddZipCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddZipCode.Click
        Try

            'If Me.State.IsNewBO Then
            '    Me.PopulateBOProperty(Me.State.MyBO, "CountryId", moCountryDrop)
            '    Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            'End If

            If Me.RadioButtonListSingleOrRange.SelectedValue = "Single" Then
                Me.State.MyBO.AddZipCode(Me.TextboxFrom.Text)
            ElseIf Me.RadioButtonListSingleOrRange.SelectedValue = "Range" Then
                If Me.State.IsNewBO Then
                    Me.State.MyBO.AddZipCodeRangeNew(Me.TextboxFrom.Text, Me.TextboxTo.Text)
                Else
                    Me.State.MyBO.AddZipCodeRange(Me.TextboxFrom.Text, Me.TextboxTo.Text)
                End If

            End If
            Me.PopulateZipCodes()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteZipCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteZipCode.Click
        Try
            If Not Me.GetSelectedList(Me.ListBoxZipCodes) Is Nothing Then
                Me.State.MyBO.DeleteZipCodes(Me.GetSelectedList(Me.ListBoxZipCodes))
                Me.State.IsAnyAdded = False
                Me.RadioButtonListSingleOrRange.SelectedValue = "Single"
                Me.PopulateZipCodes()
                EnableDisableFields()
            Else
                Me.DisplayMessage(Message.MSG_NOTHING_SELECTED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region



#End Region





End Class



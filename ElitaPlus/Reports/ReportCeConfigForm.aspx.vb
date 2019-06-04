Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Public Class ReportCeConfigForm
        Inherits ElitaPlusPage

#Region "Constants"
        Public Shared URL As String = "~/Reports/ReportCeConfigForm.aspx"
        Public Const PAGETITLE As String = "REPORT_CONFIG"
        Public Const PAGETAB As String = "REPORTS"

        ' Property Name
        Public Const COMPANY_ID_PROPERTY As String = "CompanyId"
        Public Const REPORT_CE_NAME_PROPERTY As String = "ReportCeName"
        Public Const FORM_ID_PROPERTY As String = "FormId"
        Public Const LARGE_REPORT_PROPERTY As String = "LargeReport"
#End Region

#Region "Properties"

        Public ReadOnly Property TheCompanyMControl() As MultipleColumnDDLabelControl
            Get
                If moCompanyMult Is Nothing Then
                    moCompanyMult = CType(FindControl("moCompanyMult"), MultipleColumnDDLabelControl)
                End If
                Return moCompanyMult
            End Get
        End Property

#End Region

#Region "Page State"

#Region "MyState"

        Class MyState
            Public MyBo As ReportConfig
           Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public HasDataChanged As Boolean = False
        End Class
#End Region

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
                    Me.State.MyBo = New ReportConfig(CType(Me.CallingParameters, Guid))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Handlers"

#Region "Handler-Init"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                ErrControllerMaster.Clear_Hide()
                ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
                                                                        Me.MSG_TYPE_CONFIRM, True)
                    If Me.State.MyBo Is Nothing Then
                        Me.State.MyBo = New ReportConfig
                    End If
                    PopulateAll()
                    EnableDisableFields()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try

            Me.ShowMissingTranslations(ErrControllerMaster)

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New ReportCeConfigListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                            Me.State.MyBo.Id, Me.State.HasDataChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                If (Not moReportCeDrop.Items.Count > 0) Then
                    Me.ReturnToAppHomePage()
                End If

                Me.PopulateBOsFromForm()
                If Me.State.MyBo.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Protected Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.MyBo = New ReportConfig
            ClearAll()
           Me.PopulateAll()
            EnableDisableFields()
        End Sub

        Protected Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBo.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
       
        Private Sub CreateNewCopy()
            Me.State.MyBo = New ReportConfig
            EnableDisableFields()
        End Sub

        Protected Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If Me.State.MyBo.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteReportConfig() = True Then
                    Me.State.HasDataChanged = True
                    Dim retType As New ReportCeConfigListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                                    Guid.Empty)
                    retType.BoChanged = True
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBo, COMPANY_ID_PROPERTY, TheCompanyMControl.CaptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, REPORT_CE_NAME_PROPERTY, Me.moReportCeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, FORM_ID_PROPERTY, Me.moReportLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBo, LARGE_REPORT_PROPERTY, Me.moLargeLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(TheCompanyMControl.CaptionLabel)
            Me.ClearLabelErrSign(Me.moReportCeLabel)
            Me.ClearLabelErrSign(Me.moReportLabel)
            Me.ClearLabelErrSign(Me.moLargeLabel)
        End Sub
#End Region

#End Region

#Region "Enable-Disable"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            TheCompanyMControl.ChangeEnabledControlProperty(bIsNew)
        End Sub

        Protected Sub EnableDisableFields()
           SetButtonsState(Me.State.MyBo.IsNew)
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearAll()
            TheCompanyMControl.ClearMultipleDrop()
            ClearList(moReportDrop)
            ClearList(moLargeDrop)
        End Sub

#End Region

#Region "Populate"

        Private Sub InitializeCompanyDropDowns()
            TheCompanyMControl.SetControl(True, TheCompanyMControl.MODES.NEW_MODE, False, Nothing)
            TheCompanyMControl.Visible = TheCompanyMControl.Count > 1
        End Sub

        Protected Sub PopulateFormFromBOs()
            Dim langId As Guid = Authentication.LangId
            Dim yesNoLkL As DataView = LookupListNew.GetYesNoLookupList(langId, False)
            Dim yesNoId As Guid
            Dim oReportCeName As String = String.Empty

            With Me.State.MyBo
                Me.SetSelectedItem(Me.moReportDrop, .FormId)
                If ((Not .ReportCeName Is Nothing) AndAlso (Not .ReportCeName Is String.Empty)) Then
                    oReportCeName = .ReportCeName
                End If

                If .LargeReport Is Nothing Then
                    .LargeReport = Codes.YESNO_N
                End If
                yesNoId = LookupListNew.GetIdFromCode(yesNoLkL, .LargeReport)
                Me.SetSelectedItem(Me.moLargeDrop, yesNoId)

            End With
        End Sub

        Protected Sub PopulateBOsFromForm()
            Dim langId As Guid = Authentication.LangId
            Dim yesNoLkL As DataView = LookupListNew.GetYesNoLookupList(langId, False)
            Dim yesNoId As Guid
            Dim yesNoCode As String

           Me.State.MyBo.CompanyId = Me.moCompanyMult.SelectedGuid
            Me.PopulateBOProperty(Me.State.MyBo, FORM_ID_PROPERTY, Me.moReportDrop)

            yesNoId = Me.GetSelectedItem(Me.moLargeDrop)
            yesNoCode = LookupListNew.GetCodeFromId(yesNoLkL, yesNoId)
            Me.PopulateBOProperty(Me.State.MyBo, LARGE_REPORT_PROPERTY, yesNoCode)
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateAll()
            ClearAll()
            PopulateDropdowns()
            PopulateFormFromBOs()
        End Sub

        Private Sub PopulateDropdowns()
            'InitializeCompanyDropDowns()
            'Dim langId As Guid = Authentication.LangId
            'Dim userId As Guid = Authentication.CurrentUser.Id
            'Dim repLkL As DataView = LookupListNew.GetReportFormLookupList(userId)
            'Me.BindListControlToDataView(Me.moReportDrop, repLkL, , , True)
            moReportDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()),
                                        New PopulateOptions() With
                                                    {
                                                      .AddBlankItem = True
                                                     })

            'ReportCeBase.PopulateAllReportDrop(moReportCeDrop)

            'Dim yesNoLkL As DataView = LookupListNew.GetYesNoLookupList(langId, False)

            'Me.BindListControlToDataView(moLargeDrop, yesNoLkL, , , False)
            moLargeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()),
                                        New PopulateOptions() With
                                                    {
                                                      .AddBlankItem = True
                                                     })

        End Sub

#End Region

#Region "Business Part"

        Private Function ApplyChanges() As Boolean
            Dim isOK As Boolean = True
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBo.IsDirty Then
                    Me.State.MyBo.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    EnableDisableFields()
                   Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
                isOK = False
            End Try
            Return isOK
        End Function

        Private Function DeleteReportConfig() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With Me.State.MyBo
                    .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                    .EndEdit()
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "State Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.HasDataChanged = True
                            GoBack()
                        End If
                    Case Me.MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.HasDataChanged = True
                            CreateNew()
                        End If
                    Case Me.MSG_VALUE_NO
                        CreateNew()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.HasDataChanged = True
                            CreateNewCopy()
                        End If
                    Case Me.MSG_VALUE_NO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

    End Class

End Namespace
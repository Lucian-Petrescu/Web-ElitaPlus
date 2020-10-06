'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (9/23/2004)  ********************
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables


    Partial Class CreditCardFormatForm
        Inherits ElitaPlusPage



        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents LabelReformatFileInputFlag As System.Web.UI.WebControls.Label

        'Private _CreditCardTypeList As DataView

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
        Public Const URL As String = "CreditCardFormatForm.aspx"
#End Region

#Region "Page State"


        Class MyState
            Public MyBO As CreditCardFormat
            Public ScreenSnapShotBO As CreditCardFormat

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
        'Public ReadOnly Property CreditCardTypeLIST() As DataView
        '    Get
        '        If _CreditCardTypeList Is Nothing Then
        '            _CreditCardTypeList = LookupListNew.DropdownLookupList(LookupListNew.LK_CREDIT_CARD_TYPES_1,
        '                                                                   ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        '        End If
        '        Return _CreditCardTypeList
        '    End Get
        'End Property
        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New CreditCardFormat(CType(CallingParameters, Guid))
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Page Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrorCtrl.Clear_Hide()
                If Not IsPostBack Then
                    'Date Calendars
                    MenuEnabled = False
                    AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New CreditCardFormat
                    End If
                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                End If
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
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            'Now disable depebding on the object state
            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If

            'WRITE YOU OWN CODE HERE
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO.RegularExpressionBO, "Format", LabelFormat)
            BindBOPropertyToLabel(State.MyBO, "CreditCardTypeId", moCreditCardTypesLabel)
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateDropdowns()
            Try
                'Dim dv As DataView = Me.CreditCardTypeLIST
                ' dv.Sort = "Description"
                'BindListControlToDataView(Me.moCreditCardTypesDrop, dv, "Description", "Id", True)

                Dim creditcardtypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("CCTYPE", Thread.CurrentPrincipal.GetLanguageCode())
                creditcardtypes.OrderBy("Description", LinqExtentions.SortDirection.Ascending)
                moCreditCardTypesDrop.Populate(creditcardtypes, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                If Not State.MyBO.IsNew Then
                    If .RegularExpressionBO.BuildMethod.Equals("BUILDER") Then
                        PopulateControlFromBOProperty(TextboxFormat, .RegularExpressionBO.Format)
                        TextboxFormatManual.Text = ""
                    Else
                        PopulateControlFromBOProperty(TextboxFormatManual, .RegularExpressionBO.Format)
                        TextboxFormat.Text = ""
                    End If

                End If

                PopulateControlFromBOProperty(moCreditCardTypesDrop, .CreditCardTypeId)

            End With

        End Sub

        Protected Sub PopulateBOsFormFrom()
            With State.MyBO
                If TextboxFormat.Text.Equals("") Then
                    PopulateBOProperty(State.MyBO.RegularExpressionBO, "Format", TextboxFormatManual)
                    PopulateBOProperty(State.MyBO.RegularExpressionBO, "BuildMethod", "MANUAL")
                Else
                    PopulateBOProperty(State.MyBO.RegularExpressionBO, "Format", TextboxFormat)
                    PopulateBOProperty(State.MyBO.RegularExpressionBO, "BuildMethod", "BUILDER")
                End If

                PopulateBOProperty(State.MyBO, "CreditCardTypeId", moCreditCardTypesDrop)

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New CreditCardFormat
            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub CreateNewWithCopy()
            State.MyBO = New CreditCardFormat
            PopulateBOsFormFrom()
            EnableDisableFields()

            'create the backup copy
            State.ScreenSnapShotBO = New CreditCardFormat
            State.ScreenSnapShotBO.Clone(State.MyBO)
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_OK Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Id, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(State.ActionInProgress, State.MyBO.Id, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Id, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub


#End Region


#Region "Button Clicks"


        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Or State.MyBO.IsFamilyDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Id, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
                AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Or State.MyBO.IsFamilyDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    EnableDisableFields()
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New CreditCardFormat(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New CreditCardFormat
                End If
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub



        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                State.MyBO.Delete()
                State.MyBO.Save()
                State.HasDataChanged = True
                ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Guid.Empty, State.HasDataChanged))
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
                If State.MyBO.IsDirty Or State.MyBO.IsFamilyDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub



        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Or State.MyBO.IsFamilyDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnFormatPostalCode_Click(sender As System.Object, e As System.EventArgs) Handles btnFormatPostalCode.Click
            Try
                Session("PostalCodeRegEx") = TextboxFormat.Text
                isPostalCodeVisible.Value = "True"
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page Control Events"

#End Region


#Region "Error Handling"

#End Region


    End Class


End Namespace
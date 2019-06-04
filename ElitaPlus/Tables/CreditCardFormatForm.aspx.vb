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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New CreditCardFormat(CType(Me.CallingParameters, Guid))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Page Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrorCtrl.Clear_Hide()
                If Not Me.IsPostBack Then
                    'Date Calendars
                    Me.MenuEnabled = False
                    Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New CreditCardFormat
                    End If
                    PopulateDropdowns()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                End If
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
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            'Now disable depebding on the object state
            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If

            'WRITE YOU OWN CODE HERE
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO.RegularExpressionBO, "Format", Me.LabelFormat)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CreditCardTypeId", Me.moCreditCardTypesLabel)
            Me.ClearGridHeadersAndLabelsErrSign()
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                If Not Me.State.MyBO.IsNew Then
                    If .RegularExpressionBO.BuildMethod.Equals("BUILDER") Then
                        Me.PopulateControlFromBOProperty(Me.TextboxFormat, .RegularExpressionBO.Format)
                        Me.TextboxFormatManual.Text = ""
                    Else
                        Me.PopulateControlFromBOProperty(Me.TextboxFormatManual, .RegularExpressionBO.Format)
                        Me.TextboxFormat.Text = ""
                    End If

                End If

                Me.PopulateControlFromBOProperty(Me.moCreditCardTypesDrop, .CreditCardTypeId)

            End With

        End Sub

        Protected Sub PopulateBOsFormFrom()
            With Me.State.MyBO
                If Me.TextboxFormat.Text.Equals("") Then
                    Me.PopulateBOProperty(Me.State.MyBO.RegularExpressionBO, "Format", Me.TextboxFormatManual)
                    Me.PopulateBOProperty(Me.State.MyBO.RegularExpressionBO, "BuildMethod", "MANUAL")
                Else
                    Me.PopulateBOProperty(Me.State.MyBO.RegularExpressionBO, "Format", Me.TextboxFormat)
                    Me.PopulateBOProperty(Me.State.MyBO.RegularExpressionBO, "BuildMethod", "BUILDER")
                End If

                Me.PopulateBOProperty(Me.State.MyBO, "CreditCardTypeId", Me.moCreditCardTypesDrop)

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New CreditCardFormat
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        End Sub

        Protected Sub CreateNewWithCopy()
            Me.State.MyBO = New CreditCardFormat
            Me.PopulateBOsFormFrom()
            Me.EnableDisableFields()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New CreditCardFormat
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Id, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(Me.State.ActionInProgress, Me.State.MyBO.Id, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Id, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub


#End Region


#Region "Button Clicks"


        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Or Me.State.MyBO.IsFamilyDirty Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Id, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Or Me.State.MyBO.IsFamilyDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.EnableDisableFields()
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New CreditCardFormat(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New CreditCardFormat
                End If
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub



        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                Me.State.MyBO.Delete()
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.ReturnToCallingPage(New CreditCardFormatListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Guid.Empty, Me.State.HasDataChanged))
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
                If Me.State.MyBO.IsDirty Or Me.State.MyBO.IsFamilyDirty Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub



        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Or Me.State.MyBO.IsFamilyDirty Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnFormatPostalCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFormatPostalCode.Click
            Try
                Session("PostalCodeRegEx") = TextboxFormat.Text
                isPostalCodeVisible.Value = "True"
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page Control Events"

#End Region


#Region "Error Handling"

#End Region


    End Class


End Namespace
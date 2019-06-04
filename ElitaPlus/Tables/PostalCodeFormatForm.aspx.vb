'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (9/23/2004)  ********************


Partial Class PostalCodeFormatForm
    Inherits ElitaPlusPage



    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents LabelReformatFileInputFlag As System.Web.UI.WebControls.Label


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
    Public Const URL As String = "PostalCodeFormatForm.aspx"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PostalCodeFormat
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As PostalCodeFormat, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As PostalCodeFormat
        Public ScreenSnapShotBO As PostalCodeFormat

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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New PostalCodeFormat(CType(Me.CallingParameters, Guid))
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
                    Me.State.MyBO = New PostalCodeFormat
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
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LocatorStartPosition", Me.LabelLocatorStartPosition)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LocatorLength", Me.LabelLocatorLength)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, "ReformatFileInputFlag", Me.LabelReformatFileInputFlag)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId

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

            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            Me.PopulateControlFromBOProperty(Me.TextboxLocatorStartPosition, .LocatorStartPosition)
            Me.PopulateControlFromBOProperty(Me.TextboxLocatorLength, .LocatorLength)
            Me.PopulateControlFromBOProperty(Me.CheckBoxReformatFileInputFlag, .ReformatFileInputFlag)
            Me.PopulateControlFromBOProperty(Me.CheckBoxComunaEnabled, .ComunaEnabled)
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

            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "LocatorStartPosition", Me.TextboxLocatorStartPosition)
            Me.PopulateBOProperty(Me.State.MyBO, "LocatorLength", Me.TextboxLocatorLength)
            Me.PopulateBOProperty(Me.State.MyBO, "ReformatFileInputFlag", Me.CheckBoxReformatFileInputFlag)
            Me.PopulateBOProperty(Me.State.MyBO, "ComunaEnabled", Me.CheckBoxComunaEnabled)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New PostalCodeFormat
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.State.MyBO = New PostalCodeFormat
        Me.PopulateBOsFormFrom()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New PostalCodeFormat
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
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
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
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
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
                Me.State.MyBO = New PostalCodeFormat(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New PostalCodeFormat
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



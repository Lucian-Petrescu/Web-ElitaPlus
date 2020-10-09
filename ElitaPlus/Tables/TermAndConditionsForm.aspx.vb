Namespace Tables
    Partial Public Class TermAndConditionsForm
        Inherits ElitaPlusPage

        Protected WithEvents moTitleLabel1 As System.Web.UI.WebControls.Label
        Protected WithEvents moTitleLabel2 As System.Web.UI.WebControls.Label
        Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
        Protected WithEvents btnBack As System.Web.UI.WebControls.Button
        Protected WithEvents btnApply_WRITE As System.Web.UI.WebControls.Button
        Protected WithEvents btnUndo_WRITE As System.Web.UI.WebControls.Button
        Protected WithEvents HiddenSaveChangesPromptResponse As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents ErrorCtrl As ErrorController

        Protected WithEvents LabelComment1 As System.Web.UI.WebControls.Label
        Protected WithEvents TextboxComment As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblContractID As System.Web.UI.WebControls.Label

        Public Shared urlStr As String = ""

#Region "Constants"
        Public Shared URL As String = "~/Tables/TermAndConditionsForm.aspx"
        Public Const CONTRACT_ID_PROPERTY As String = "contract_id"
        Public Const COMMENT1_PROPERTY As String = "comment1"
        Public Const FORM_DIRTY_COLUMNS_COUNT As Integer = 1
        Private Const NO_CONTRACT_FOUND As String = "NO_CONTRACT_FOUND"

        Private Const CALLING_FROM_CERTIFICATE As String = "Certificate"
        Private Const CALLING_FROM_CONTRACT As String = "Guid"

#End Region

#Region "Page State"

        Class MyState
            Public MyBO As Contract
            Public ScreenSnapShotBO As Contract
            Public IsNew As Boolean
            Public ContractId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public CallingFromOBJ As String
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

                    Dim objType As System.Type = CallingParameters.GetType()
                    If objType.Name = CALLING_FROM_CERTIFICATE Then
                        EnableDisableButtons(False)
                        Dim objCert As Certificate = CType(CallingParameters, Certificate)
                        State.MyBO = Contract.GetContract(objCert.DealerId, objCert.WarrantySalesDate.Value)
                        If State.MyBO Is Nothing Then
                            Throw New DataNotFoundException(NO_CONTRACT_FOUND)
                        End If
                        State.CallingFromOBJ = CALLING_FROM_CERTIFICATE
                    ElseIf objType.Name = CALLING_FROM_CONTRACT Then
                        'Dim objContract As Contract = CType(Me.CallingParameters, Contract)
                        Dim contractId As Guid = CType(CallingParameters, Guid)
                        State.MyBO = New Contract(contractId)
                        If State.MyBO Is Nothing Then
                            Throw New DataNotFoundException(NO_CONTRACT_FOUND)
                        End If
                        State.CallingFromOBJ = CALLING_FROM_CONTRACT
                    End If
                Else
                    State.IsNew = True
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Contract
            Public HasDataChanged As Boolean

            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Contract, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    MenuEnabled = False

                    If State.IsNew = True Then
                        CreateNew()
                    End If
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
            TextboxComment.Enabled = True
            If Not State.CallingFromOBJ.Trim.Equals(CALLING_FROM_CONTRACT) OrElse btnApply_WRITE.Enabled = False Then
                TextboxComment.ReadOnly = True
            End If
        End Sub

        Protected Sub EnableDisableButtons(Optional ByVal flag As Boolean = True)
            btnBack.Visible = True
            btnApply_WRITE.Visible = flag
            btnUndo_WRITE.Visible = flag
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, COMMENT1_PROPERTY, LabelComment1)
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                PopulateControlFromBOProperty(TextboxComment, .Comment1)

                If Not State.CallingFromOBJ = CALLING_FROM_CONTRACT Then
                    Dim urlPattern As String = "(http[s]?://)?([\w-]+\.)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                    'Dim dirPattern As String = "^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*))+\.(txt|TXT|pdf|PDF|doc|DOC|htm|HTM|html|HTML|xls|XLS|csv|CSV)$"

                    If .Comment1 IsNot Nothing AndAlso .Comment1.Length > 0 Then
                        urlStr = GetFormatedString(.Comment1, urlPattern)
                    End If

                    If (urlStr IsNot Nothing) AndAlso urlStr <> "" Then
                        urlStr = urlStr.Replace(Convert.ToString(Microsoft.VisualBasic.Chr(13)) & Convert.ToString(Microsoft.VisualBasic.Chr(10)), "<br>")
                        urlStr = urlStr.Replace(Convert.ToString(Microsoft.VisualBasic.Chr(13)), "<br>")
                        urlStr = urlStr.Replace(Convert.ToString(Microsoft.VisualBasic.Chr(10)), "<br>")
                    End If

                    'urlStr = GetFormatedString(urlStr, dirPattern)
                    ControlMgr.SetVisibleControl(Me, TextboxComment, False)
                Else
                    urlStr = ""
                    ControlMgr.SetVisibleControl(Me, TextboxComment, True)
                End If
            End With

        End Sub

        Public Function GetFormatedString(str As String, pattern As String) As String
            Dim matches As System.Text.RegularExpressions.MatchCollection
            Dim reg As New System.Text.RegularExpressions.Regex(pattern)

            matches = reg.Matches(str)

            Dim successfulMatch As System.Text.RegularExpressions.Match
            For Each successfulMatch In matches
                Dim url As String = successfulMatch.Value
                If url.Length > 8 Then
                    If url.Substring(0, 7).ToLower <> "http://" AndAlso url.Substring(0, 8).ToLower <> "https://" Then
                        url = "http://" & url
                    End If
                End If
                str = str.Replace(successfulMatch.Value, "<a href='" & url & "' target='_blank'><u><font color='#0000FF'>" & successfulMatch.Value & "</font></u></a>")
            Next

            Return str
        End Function

        Protected Sub PopulateBOsFromForm()

            With State.MyBO
                PopulateBOProperty(State.MyBO, "Comment1", TextboxComment)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = New Contract
            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso (confResponse = CONFIRM_MESSAGE_OK OrElse confResponse = MSG_VALUE_YES) Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
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

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
                AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrorCtrl.Text
            End Try
        End Sub


        Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    AddInfoMsgWithSubmit(Message.SAVE_RECORD_CONFIRMATION, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Contract(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
#End Region



    End Class

End Namespace
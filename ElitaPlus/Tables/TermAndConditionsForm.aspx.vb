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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then

                    Dim objType As System.Type = Me.CallingParameters.GetType()
                    If objType.Name = CALLING_FROM_CERTIFICATE Then
                        EnableDisableButtons(False)
                        Dim objCert As Certificate = CType(Me.CallingParameters, Certificate)
                        Me.State.MyBO = Contract.GetContract(objCert.DealerId, objCert.WarrantySalesDate.Value)
                        If Me.State.MyBO Is Nothing Then
                            Throw New DataNotFoundException(NO_CONTRACT_FOUND)
                        End If
                        Me.State.CallingFromOBJ = CALLING_FROM_CERTIFICATE
                    ElseIf objType.Name = CALLING_FROM_CONTRACT Then
                        'Dim objContract As Contract = CType(Me.CallingParameters, Contract)
                        Dim contractId As Guid = CType(Me.CallingParameters, Guid)
                        Me.State.MyBO = New Contract(contractId)
                        If Me.State.MyBO Is Nothing Then
                            Throw New DataNotFoundException(NO_CONTRACT_FOUND)
                        End If
                        Me.State.CallingFromOBJ = CALLING_FROM_CONTRACT
                    End If
                Else
                    Me.State.IsNew = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Contract
            Public HasDataChanged As Boolean

            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Contract, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    Me.MenuEnabled = False

                    If Me.State.IsNew = True Then
                        CreateNew()
                    End If
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
            TextboxComment.Enabled = True
            If Not Me.State.CallingFromOBJ.Trim.Equals(CALLING_FROM_CONTRACT) OrElse btnApply_WRITE.Enabled = False Then
                TextboxComment.ReadOnly = True
            End If
        End Sub

        Protected Sub EnableDisableButtons(Optional ByVal flag As Boolean = True)
            btnBack.Visible = True
            btnApply_WRITE.Visible = flag
            btnUndo_WRITE.Visible = flag
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, COMMENT1_PROPERTY, Me.LabelComment1)
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.TextboxComment, .Comment1)

                If Not Me.State.CallingFromOBJ = CALLING_FROM_CONTRACT Then
                    Dim urlPattern As String = "(http[s]?://)?([\w-]+\.)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                    'Dim dirPattern As String = "^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*))+\.(txt|TXT|pdf|PDF|doc|DOC|htm|HTM|html|HTML|xls|XLS|csv|CSV)$"

                    If Not .Comment1 Is Nothing AndAlso .Comment1.Length > 0 Then
                        urlStr = GetFormatedString(.Comment1, urlPattern)
                    End If

                    If (Not urlStr Is Nothing) AndAlso urlStr <> "" Then
                        urlStr = urlStr.Replace(Convert.ToString(Microsoft.VisualBasic.Chr(13)) & Convert.ToString(Microsoft.VisualBasic.Chr(10)), "<br>")
                        urlStr = urlStr.Replace(Convert.ToString(Microsoft.VisualBasic.Chr(13)), "<br>")
                        urlStr = urlStr.Replace(Convert.ToString(Microsoft.VisualBasic.Chr(10)), "<br>")
                    End If

                    'urlStr = GetFormatedString(urlStr, dirPattern)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxComment, False)
                Else
                    urlStr = ""
                    ControlMgr.SetVisibleControl(Me, Me.TextboxComment, True)
                End If
            End With

        End Sub

        Public Function GetFormatedString(ByVal str As String, ByVal pattern As String) As String
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

            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "Comment1", Me.TextboxComment)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = New Contract
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso (confResponse = Me.CONFIRM_MESSAGE_OK Or confResponse = Me.MSG_VALUE_YES) Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
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

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
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


        Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    Me.AddInfoMsgWithSubmit(Message.SAVE_RECORD_CONFIRMATION, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Contract(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region



    End Class

End Namespace
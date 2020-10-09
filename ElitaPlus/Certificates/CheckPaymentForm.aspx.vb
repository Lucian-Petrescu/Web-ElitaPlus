Namespace Certificates

    Partial Class CheckPaymentForm
        Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

        Protected WithEvents moCertificateInfoController As UserControlCertificateInfo_New

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
        Public Const PAGETITLE As String = "CHECKPAYMENT"
        Public Const PAGETAB As String = "CERTIFICATE"
        Public Const URL As String = "~/Certificates/CheckPaymentForm.aspx"
        Public Const CERTIFICATE_FORM As String = "CertificateForm"
#End Region

#Region "Variables"

        Private mbIsFirstPass As Boolean = True

#End Region

#Region "Parameters"
        Public Class Parameters
            Public CertId As Guid
            Public Sub New(certid As Guid)
                Me.CertId = certid
            End Sub
        End Class
#End Region

#Region "Properties"

        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public Property IsEdit() As Boolean
            Get
                Return State._IsEdit
            End Get
            Set(Value As Boolean)
                State._IsEdit = Value
            End Set
        End Property

        Public Property moCertificate() As Certificate
            Get
                Return State._moCertificate
            End Get
            Set(Value As Certificate)
                State._moCertificate = Value
            End Set
        End Property

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Certificate
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Certificate, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"

        Class BaseState
            Public NavCtrl As INavigationController
        End Class

        Class MyState
            Public ScreenSnapShotBO As CheckPayment
            Public MyBO As CheckPayment
            Public StateCertId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public boChanged As Boolean = False
            Public _IsEdit As Boolean
            Public _moCertificate As Certificate
            Public companyCode As String
            'Public searchDV As DataView
        End Class

        Public Sub New()
            MyBase.New(New BaseState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    Me.State.MyBO = New CheckPayment
                    Me.State.MyBO.CertId = CType(NavController.ParametersPassed, Parameters).CertId
                    moCertificate = (CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
                    If Me.State.MyBO IsNot Nothing Then
                        Me.State.MyBO.BeginEdit()
                    End If
                    IsEdit = True
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.boChanged = False
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Page Events"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            MasterPage.MessageController.Clear_Hide()

            Try
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                UpdateBreadCrum()

                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        If CallingParameters IsNot Nothing Then
                            State.StateCertId = CType(CallingParameters, Guid)
                            State._moCertificate = New Certificate(State.StateCertId)
                        End If
                        CreateNew()
                    End If

                    AddCalendar(ImageButtonCheckReceivedDate, txtCheckReceivedOn)
                    UserCertificateCtr.PopulateFormFromCertificateCtrl(State._moCertificate)
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                ChangeEnabledProperty(txtCheckReceivedOn, True)
                ChangeEnabledProperty(txtCheckCustomerName, True)
                ChangeEnabledProperty(txtCheckBankName, True)
                ChangeEnabledProperty(txtCheckNumber, True)
                ChangeEnabledProperty(txtCheckAmount, True)
                ChangeEnabledProperty(txtCheckComments, True)
                btnBack.Enabled = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                ClearGridHeadersAndLabelsErrSign()
                BindBOPropertyToLabel(State.MyBO, "PaymentDate", lblCheckReceivedOn)
                BindBOPropertyToLabel(State.MyBO, "CustomerName", lblCheckCustomerName)
                BindBOPropertyToLabel(State.MyBO, "BankName", lblCheckBankName)
                BindBOPropertyToLabel(State.MyBO, "CheckNumber", lblCheckNumber)
                BindBOPropertyToLabel(State.MyBO, "CheckAmount", lblCheckAmount)
                BindBOPropertyToLabel(State.MyBO, "Comments", lblCheckComments)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                With State.MyBO
                    PopulateControlFromBOProperty(txtCheckReceivedOn, .PaymentDate)
                    PopulateControlFromBOProperty(txtCheckCustomerName, .CustomerName)
                    PopulateControlFromBOProperty(txtCheckBankName, .BankName)
                    PopulateControlFromBOProperty(txtCheckNumber, .CheckNumber)
                    PopulateControlFromBOProperty(txtCheckAmount, .CheckAmount)
                    PopulateControlFromBOProperty(txtCheckComments, .Comments)
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBOsFormFrom()
            Try
                With State.MyBO
                    PopulateBOProperty(State.MyBO, "PaymentDate", txtCheckReceivedOn)
                    PopulateBOProperty(State.MyBO, "CustomerName", txtCheckCustomerName)
                    PopulateBOProperty(State.MyBO, "BankName", txtCheckBankName)
                    PopulateBOProperty(State.MyBO, "CheckNumber", txtCheckNumber)
                    PopulateBOProperty(State.MyBO, "CheckAmount", txtCheckAmount)
                    PopulateBOProperty(State.MyBO, "Comments", txtCheckComments)
                End With
                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CleanPopupInput()
            Try
                If State IsNot Nothing Then
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Try
                Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
                Dim actionInProgress As ElitaPlusPage.DetailPageCommand = State.ActionInProgress
                'Clean after consuming the action
                CleanPopupInput()
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        Try
                            CreateNew()
                            PopulateBOsFormFrom()

                            Dim payment_id As Guid = Guid.Empty
                            Dim Err_Code As Integer = 0
                            Dim Err_Message As String = String.Empty

                            Dim chkPay As CheckPayment = New CheckPayment()

                            State.MyBO.EndEdit()
                            chkPay.AddCheckPayment(State.MyBO.PaymentDate, State.MyBO.CheckAmount, State.MyBO.CertId, State.MyBO.CheckNumber,
                                                   State.MyBO.CustomerName, State.MyBO.BankName, State.MyBO.Comments, ElitaPlusIdentity.Current.ActiveUser.NetworkId,
                                                   payment_id, Err_Code, Err_Message)

                            If Err_Code = 0 Then
                                MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                                btnAdd_WRITE.Enabled = False
                                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                                NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                            Else
                                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED + "<BR/>" + Err_Message)
                            End If
                        Catch ex As Threading.ThreadAbortException
                        Catch ex As Exception
                            HandleErrors(ex, MasterPage.MessageController)
                        End Try
                    Else
                        NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                    End If
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    Select Case actionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.MyBO.cancelEdit()
                            NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    End Select
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNew()
            MasterPage.MessageController.Clear_Hide()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = Nothing '
            NavController.State = Nothing
            State.MyBO.BeginEdit()
        End Sub

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_ Then
                    State.MyBO.cancelEdit()
                    NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                End If

                If State.MyBO.IsDirty AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    State.MyBO.cancelEdit()
                    NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                CreateNew()
                PopulateBOsFormFrom()
                State.MyBO.Validate()

                Dim payment_id As Guid = Guid.Empty
                Dim Err_Code As Integer = 0
                Dim Err_Message As String = String.Empty

                Dim chkPay As CheckPayment = New CheckPayment()
                If State.MyBO.IsDirty Then

                    State.MyBO.EndEdit()
                    chkPay.AddCheckPayment(State.MyBO.PaymentDate, State.MyBO.CheckAmount, State.MyBO.CertId, State.MyBO.CheckNumber,
                                           State.MyBO.CustomerName, State.MyBO.BankName, State.MyBO.Comments, ElitaPlusIdentity.Current.ActiveUser.NetworkId,
                                           payment_id, Err_Code, Err_Message)

                    If Err_Code = 0 Then
                        MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                        btnAdd_WRITE.Enabled = False
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED + "<BR/>" + Err_Message)
                    End If
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Catch ex As BOValidationException
                HandleErrors(ex, MasterPage.MessageController)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class

End Namespace
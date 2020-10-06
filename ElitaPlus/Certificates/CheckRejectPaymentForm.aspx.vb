Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Certificates

    Partial Class CheckRejectPaymentForm
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
        Public Const PAGETITLE As String = "ADD_REJECT_PAYMENT"
        Public Const PAGETAB As String = "CERTIFICATE"
        Public Const URL As String = "~/Certificates/CheckRejectPaymentForm.aspx"
        Public Const CERTIFICATE_FORM As String = "CertificateForm"
#End Region

#Region "Variables"

        Private mbIsFirstPass As Boolean = True

#End Region


#Region "Parameters"
        Public Class Parameters
            Public PaymentId As Guid
            Public CertId As Guid
            Public PayInstrumentNo As String
            Public PaymentDate As String
            Public Sub New(paymnetid As Guid, certid As Guid, payinstrumentno As String, paymentdate As String)
                PaymentId = paymnetid
                Me.PayInstrumentNo = payinstrumentno
                Me.CertId = certid
                Me.PaymentDate = paymentdate
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
            Public ScreenSnapShotBO As CheckReject
            Public MyBO As CheckReject
            Public StateCertId As Guid
            Public SelectedPaymentId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public boChanged As Boolean = False
            Public _IsEdit As Boolean
            Public _moCertificate As Certificate
            Public companyCode As String
        End Class

        Public Sub New()
            MyBase.New(New BaseState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    Me.State.MyBO = New CheckReject
                    Me.State.SelectedPaymentId = CType(NavController.ParametersPassed, Parameters).PaymentId
                    Me.State.MyBO.CertId = CType(NavController.ParametersPassed, Parameters).CertId
                    moCertificate = (CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
                    If (CType(NavController.ParametersPassed, Parameters).PayInstrumentNo.ToString <> String.Empty) Then
                        Me.State.MyBO.SelectRejectCheck = CType(NavController.ParametersPassed, Parameters).PayInstrumentNo.ToString
                    End If
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
                            State._moCertificate = New Certificate(State.MyBO.CertId)
                        End If
                        CreateNew()
                    End If

                    AddCalendar(ImageButtonCheckRejectedDate, txtCheckRejectedOn)
                    UserCertificateCtr.PopulateFormFromCertificateCtrl(State._moCertificate)
                    PopulateFormFromBOs()
                    PopulateDropDowns()
                    EnableDisableFields()
                Else
                    ClearErrLabels()
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

        Private Sub ClearErrLabels()
            ClearLabelErrSign(lblCheckRejectedOn)
            ClearLabelErrSign(lblRejectReason)
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                ChangeEnabledProperty(txtCheckRejectedOn, True)
                ChangeEnabledProperty(txtSltRejectedChk, True)
                ChangeEnabledProperty(ddlRejectReason, True)
                ChangeEnabledProperty(txtCheckComments, True)
                btnBack.Enabled = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'Me.ddlRejectReason.PopulateOld("PYMT_REJ_CODE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                ddlRejectReason.Populate(CommonConfigManager.Current.ListManager.GetList("PYMT_REJ_CODE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                ClearGridHeadersAndLabelsErrSign()
                BindBOPropertyToLabel(State.MyBO, "RejectDate", lblCheckRejectedOn)
                BindBOPropertyToLabel(State.MyBO, "SelectRejectCheck", lblSltRejectedChk)
                BindBOPropertyToLabel(State.MyBO, "RejectReasonXcd", lblRejectReason)
                BindBOPropertyToLabel(State.MyBO, "Comments", lblCheckComments)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                With State.MyBO
                    PopulateControlFromBOProperty(txtCheckRejectedOn, .RejectDate)
                    PopulateControlFromBOProperty(txtSltRejectedChk, .SelectRejectCheck)
                    PopulateControlFromBOProperty(txtCheckComments, .Comments)
                    BindSelectItem(.RejectReasonXcd, ddlRejectReason)
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBOsFormFrom()
            Try
                With State.MyBO
                    PopulateBOProperty(State.MyBO, "RejectDate", txtCheckRejectedOn)
                    PopulateBOProperty(State.MyBO, "SelectRejectCheck", txtSltRejectedChk)
                    PopulateBOProperty(State.MyBO, "RejectReasonXcd", ddlRejectReason, False, True)
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

                            Dim rejectpayment_id As Guid = Guid.Empty
                            Dim Err_Code As Integer = 0
                            Dim Err_Message As String = String.Empty

                            Dim chkPay As CheckReject = New CheckReject()

                            State.MyBO.EndEdit()
                            'need to write method
                            chkPay.AddRejectPayment(State.MyBO.RejectDate, State.MyBO.Id, State.MyBO.RejectReasonXcd, State.MyBO.Comments, Err_Code, Err_Message)

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
            ' Me.NavController.State = Nothing
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
                'Me.CreateNew()
                PopulateBOsFormFrom()
                State.MyBO.Validate()

                'Dim rejectpayment_id As Guid = Guid.Empty
                Dim Err_Code As Integer = 0
                Dim Err_Message As String = String.Empty

                Dim Paydate As Date
                Dim Rejectdate As Date
                Paydate = CType(NavController.ParametersPassed, Parameters).PaymentDate.ToString
                Rejectdate = DateHelper.GetDateValue(txtCheckRejectedOn.Text)

                If CDate(Paydate) >= CDate(Rejectdate) Or CDate(Rejectdate) > DateTime.Today Then
                    ElitaPlusPage.SetLabelError(lblCheckRejectedOn)
                    Throw New GUIException(Message.MSG_REJECT_DATE, Assurant.ElitaPlus.Common.ErrorCodes.REJECT_DATE_ERR)
                End If

                Dim chkPay As CheckReject = New CheckReject()
                State.MyBO.EndEdit()
                chkPay.AddRejectPayment(State.MyBO.RejectDate, State.SelectedPaymentId, State.MyBO.RejectReasonXcd, State.MyBO.Comments, Err_Code, Err_Message)

                If Err_Code = 0 Then
                    MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                    btnAdd_WRITE.Enabled = False
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED + "<BR/>" + Err_Message)
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
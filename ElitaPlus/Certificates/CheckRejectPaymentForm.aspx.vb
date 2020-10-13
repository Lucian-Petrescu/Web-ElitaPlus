Imports System.Diagnostics
Imports Assurant.Elita.CommonConfiguration
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Certificates

    Partial Class CheckRejectPaymentForm
        Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

        Protected WithEvents moCertificateInfoController As UserControlCertificateInfo_New

        'This call is required by the Web Form Designer.
        <DebuggerStepThrough> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
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
                Me.PaymentId = paymnetid
                Me.PayInstrumentNo = payinstrumentno
                Me.CertId = certid
                Me.PaymentDate = paymentdate
            End Sub
        End Class
#End Region

#Region "Properties"

        Public ReadOnly Property UserCertificateCtr As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public Property IsEdit As Boolean
            Get
                Return Me.State._IsEdit
            End Get
            Set
                Me.State._IsEdit = Value
            End Set
        End Property

        Public Property moCertificate As Certificate
            Get
                Return Me.State._moCertificate
            End Get
            Set
                Me.State._moCertificate = Value
            End Set
        End Property

#End Region


#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Certificate
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Certificate, hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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

        Protected Shadows ReadOnly Property State As MyState
            Get
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    Me.State.MyBO = New CheckReject
                    Me.State.SelectedPaymentId = CType(Me.NavController.ParametersPassed, Parameters).PaymentId
                    Me.State.MyBO.CertId = CType(Me.NavController.ParametersPassed, Parameters).CertId
                    moCertificate = (CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
                    If (CType(Me.NavController.ParametersPassed, Parameters).PayInstrumentNo.ToString <> String.Empty) Then
                        Me.State.MyBO.SelectRejectCheck = CType(Me.NavController.ParametersPassed, Parameters).PayInstrumentNo.ToString
                    End If
                    If Me.State.MyBO IsNot Nothing Then
                        Me.State.MyBO.BeginEdit()
                    End If
                    Me.IsEdit = True
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If Me.CallingParameters IsNot Nothing Then
                    Me.State.boChanged = False
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region


#Region "Page Events"

        Private Sub UpdateBreadCrum()
            If (Me.State IsNot Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End Sub

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Me.MasterPage.MessageController.Clear_Hide()

            Try
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                Me.UpdateBreadCrum()

                If Not Me.IsPostBack Then
                    If Me.State.MyBO Is Nothing Then
                        If Me.CallingParameters IsNot Nothing Then
                            Me.State._moCertificate = New Certificate(Me.State.MyBO.CertId)
                        End If
                        Me.CreateNew()
                    End If

                    Me.AddCalendar(Me.ImageButtonCheckRejectedDate, Me.txtCheckRejectedOn)
                    Me.UserCertificateCtr.PopulateFormFromCertificateCtrl(Me.State._moCertificate)
                    Me.PopulateFormFromBOs()
                    Me.PopulateDropDowns()
                    Me.EnableDisableFields()
                Else
                    ClearErrLabels()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()

                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(lblCheckRejectedOn)
            Me.ClearLabelErrSign(lblRejectReason)
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                Me.ChangeEnabledProperty(Me.txtCheckRejectedOn, True)
                Me.ChangeEnabledProperty(Me.txtSltRejectedChk, True)
                Me.ChangeEnabledProperty(Me.ddlRejectReason, True)
                Me.ChangeEnabledProperty(Me.txtCheckComments, True)
                Me.btnBack.Enabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                'Me.ddlRejectReason.PopulateOld("PYMT_REJ_CODE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                Me.ddlRejectReason.Populate(CommonConfigManager.Current.ListManager.GetList("PYMT_REJ_CODE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                Me.ClearGridHeadersAndLabelsErrSign()
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RejectDate", Me.lblCheckRejectedOn)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "SelectRejectCheck", Me.lblSltRejectedChk)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RejectReasonXcd", Me.lblRejectReason)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "Comments", Me.lblCheckComments)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                With Me.State.MyBO
                    Me.PopulateControlFromBOProperty(Me.txtCheckRejectedOn, .RejectDate)
                    Me.PopulateControlFromBOProperty(Me.txtSltRejectedChk, .SelectRejectCheck)
                    Me.PopulateControlFromBOProperty(Me.txtCheckComments, .Comments)
                    BindSelectItem(.RejectReasonXcd, ddlRejectReason)
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBOsFormFrom()
            Try
                With Me.State.MyBO
                    Me.PopulateBOProperty(Me.State.MyBO, "RejectDate", Me.txtCheckRejectedOn)
                    Me.PopulateBOProperty(Me.State.MyBO, "SelectRejectCheck", Me.txtSltRejectedChk)
                    Me.PopulateBOProperty(Me.State.MyBO, "RejectReasonXcd", Me.ddlRejectReason, False, True)
                    Me.PopulateBOProperty(Me.State.MyBO, "Comments", Me.txtCheckComments)
                End With
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CleanPopupInput()
            Try
                If Me.State IsNot Nothing Then
                    'Clean after consuming the action
                    Me.State.ActionInProgress = DetailPageCommand.Nothing_
                    Me.HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Try
                Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
                Dim actionInProgress As DetailPageCommand = Me.State.ActionInProgress
                'Clean after consuming the action
                CleanPopupInput()
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    If actionInProgress <> DetailPageCommand.BackOnErr Then
                        Try
                            Me.CreateNew()
                            Me.PopulateBOsFormFrom()

                            Dim rejectpayment_id As Guid = Guid.Empty
                            Dim Err_Code As Integer = 0
                            Dim Err_Message As String = String.Empty

                            Dim chkPay As CheckReject = New CheckReject()

                            Me.State.MyBO.EndEdit()
                            'need to write method
                            chkPay.AddRejectPayment(Me.State.MyBO.RejectDate, Me.State.MyBO.Id, Me.State.MyBO.RejectReasonXcd, Me.State.MyBO.Comments, Err_Code, Err_Message)

                            If Err_Code = 0 Then
                                Me.MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                                btnAdd_WRITE.Enabled = False
                                Me.State.ActionInProgress = DetailPageCommand.Accept
                                Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                            Else
                                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED + "<BR/>" + Err_Message)
                            End If
                        Catch ex As ThreadAbortException
                        Catch ex As Exception
                            Me.HandleErrors(ex, Me.MasterPage.MessageController)
                        End Try
                    Else
                        Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                    End If
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    Select Case actionInProgress
                        Case DetailPageCommand.Back
                            Me.State.MyBO.cancelEdit()
                            Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                        Case DetailPageCommand.BackOnErr
                            Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNew()
            Me.MasterPage.MessageController.Clear_Hide()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = Nothing '
            ' Me.NavController.State = Nothing
            Me.State.MyBO.BeginEdit()
        End Sub

#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                If Me.State.ActionInProgress = DetailPageCommand.Nothing_ Then
                    Me.State.MyBO.cancelEdit()
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                End If

                If Me.State.MyBO.IsDirty AndAlso Me.State.ActionInProgress <> DetailPageCommand.Accept Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = DetailPageCommand.Back
                Else
                    Me.State.MyBO.cancelEdit()
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnAdd_WRITE_Click(sender As Object, e As EventArgs) Handles btnAdd_WRITE.Click
            Try
                'Me.CreateNew()
                Me.PopulateBOsFormFrom()
                Me.State.MyBO.Validate()

                'Dim rejectpayment_id As Guid = Guid.Empty
                Dim Err_Code As Integer = 0
                Dim Err_Message As String = String.Empty

                Dim Paydate As Date
                Dim Rejectdate As Date
                Paydate = CType(Me.NavController.ParametersPassed, Parameters).PaymentDate.ToString
                Rejectdate = DateHelper.GetDateValue(txtCheckRejectedOn.Text)

                If CDate(Paydate) >= CDate(Rejectdate) OrElse CDate(Rejectdate) > DateTime.Today Then
                    SetLabelError(lblCheckRejectedOn)
                    Throw New GUIException(Message.MSG_REJECT_DATE, Assurant.ElitaPlus.Common.ErrorCodes.REJECT_DATE_ERR)
                End If

                Dim chkPay As CheckReject = New CheckReject()
                Me.State.MyBO.EndEdit()
                chkPay.AddRejectPayment(Me.State.MyBO.RejectDate, Me.State.SelectedPaymentId, Me.State.MyBO.RejectReasonXcd, Me.State.MyBO.Comments, Err_Code, Err_Message)

                If Err_Code = 0 Then
                    Me.MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                    btnAdd_WRITE.Enabled = False
                    Me.State.ActionInProgress = DetailPageCommand.Back
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED + "<BR/>" + Err_Message)
                End If

            Catch ex As BOValidationException
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class
End Namespace
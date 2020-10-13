Imports System.Diagnostics
Imports System.Threading

Namespace Certificates

    Partial Class CheckPaymentForm
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

        Protected Shadows ReadOnly Property State As MyState
            Get
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    Me.State.MyBO = New CheckPayment
                    Me.State.MyBO.CertId = CType(Me.NavController.ParametersPassed, Parameters).CertId
                    moCertificate = (CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
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
                            Me.State.StateCertId = CType(Me.CallingParameters, Guid)
                            Me.State._moCertificate = New Certificate(Me.State.StateCertId)
                        End If
                        Me.CreateNew()
                    End If

                    Me.AddCalendar(Me.ImageButtonCheckReceivedDate, Me.txtCheckReceivedOn)
                    Me.UserCertificateCtr.PopulateFormFromCertificateCtrl(Me.State._moCertificate)
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
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
#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                Me.ChangeEnabledProperty(Me.txtCheckReceivedOn, True)
                Me.ChangeEnabledProperty(Me.txtCheckCustomerName, True)
                Me.ChangeEnabledProperty(Me.txtCheckBankName, True)
                Me.ChangeEnabledProperty(Me.txtCheckNumber, True)
                Me.ChangeEnabledProperty(Me.txtCheckAmount, True)
                Me.ChangeEnabledProperty(Me.txtCheckComments, True)
                Me.btnBack.Enabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                Me.ClearGridHeadersAndLabelsErrSign()
                Me.BindBOPropertyToLabel(Me.State.MyBO, "PaymentDate", Me.lblCheckReceivedOn)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CustomerName", Me.lblCheckCustomerName)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "BankName", Me.lblCheckBankName)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CheckNumber", Me.lblCheckNumber)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CheckAmount", Me.lblCheckAmount)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "Comments", Me.lblCheckComments)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                With Me.State.MyBO
                    Me.PopulateControlFromBOProperty(Me.txtCheckReceivedOn, .PaymentDate)
                    Me.PopulateControlFromBOProperty(Me.txtCheckCustomerName, .CustomerName)
                    Me.PopulateControlFromBOProperty(Me.txtCheckBankName, .BankName)
                    Me.PopulateControlFromBOProperty(Me.txtCheckNumber, .CheckNumber)
                    Me.PopulateControlFromBOProperty(Me.txtCheckAmount, .CheckAmount)
                    Me.PopulateControlFromBOProperty(Me.txtCheckComments, .Comments)
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBOsFormFrom()
            Try
                With Me.State.MyBO
                    Me.PopulateBOProperty(Me.State.MyBO, "PaymentDate", Me.txtCheckReceivedOn)
                    Me.PopulateBOProperty(Me.State.MyBO, "CustomerName", Me.txtCheckCustomerName)
                    Me.PopulateBOProperty(Me.State.MyBO, "BankName", Me.txtCheckBankName)
                    Me.PopulateBOProperty(Me.State.MyBO, "CheckNumber", Me.txtCheckNumber)
                    Me.PopulateBOProperty(Me.State.MyBO, "CheckAmount", Me.txtCheckAmount)
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

                            Dim payment_id As Guid = Guid.Empty
                            Dim Err_Code As Integer = 0
                            Dim Err_Message As String = String.Empty

                            Dim chkPay As CheckPayment = New CheckPayment()

                            Me.State.MyBO.EndEdit()
                            chkPay.AddCheckPayment(Me.State.MyBO.PaymentDate, Me.State.MyBO.CheckAmount, Me.State.MyBO.CertId, Me.State.MyBO.CheckNumber,
                                                   Me.State.MyBO.CustomerName, Me.State.MyBO.BankName, Me.State.MyBO.Comments, ElitaPlusIdentity.Current.ActiveUser.NetworkId,
                                                   payment_id, Err_Code, Err_Message)

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
            Me.NavController.State = Nothing
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
                Me.CreateNew()
                Me.PopulateBOsFormFrom()
                Me.State.MyBO.Validate()

                Dim payment_id As Guid = Guid.Empty
                Dim Err_Code As Integer = 0
                Dim Err_Message As String = String.Empty

                Dim chkPay As CheckPayment = New CheckPayment()
                If Me.State.MyBO.IsDirty Then

                    Me.State.MyBO.EndEdit()
                    chkPay.AddCheckPayment(Me.State.MyBO.PaymentDate, Me.State.MyBO.CheckAmount, Me.State.MyBO.CertId, Me.State.MyBO.CheckNumber,
                                           Me.State.MyBO.CustomerName, Me.State.MyBO.BankName, Me.State.MyBO.Comments, ElitaPlusIdentity.Current.ActiveUser.NetworkId,
                                           payment_id, Err_Code, Err_Message)

                    If Err_Code = 0 Then
                        Me.MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                        btnAdd_WRITE.Enabled = False
                        Me.State.ActionInProgress = DetailPageCommand.Back
                    Else
                        Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED + "<BR/>" + Err_Message)
                    End If
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
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
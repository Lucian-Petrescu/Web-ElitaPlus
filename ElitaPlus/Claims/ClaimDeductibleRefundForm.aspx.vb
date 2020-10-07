Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class ClaimDeductibleRefundForm
    Inherits ElitaPlusSearchPage


#Region "Constants"

    Public Const NO_DATA As String = " - "
    Private Const STATE_ACTIVE As String = "StatActive"
    Private Const STATE_CLOSED As String = "StatClosed"
    Private Const STRING_CLASS As String = "Class"
    Private Const LIST_CODE_RFM As String = "RFM"
    Private Const LBL_TXT_CLAIM_DEDUCTIBLE_REFUND As String = "CLAIM_DEDUCTIBLE_REFUND"
    Private Const LBL_TXT_CLAIM_SUMMARY As String = "CLAIM_SUMMARY"
    Public Const URL As String = "~/Claims/ClaimDeductibleRefundForm.aspx"

#End Region

#Region "Page State"

    Class MyState
        Public MyBO As ClaimBase
        Public InputParameters As Parameters

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If (NavController Is Nothing) Then
                Return CType(MyBase.State, MyState)
            Else
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    InitializeFromFlowSession()
                End If
                Return CType(NavController.State, MyState)
            End If
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        State.InputParameters = CType(NavController.ParametersPassed, Parameters)

        Try
            If State.InputParameters IsNot Nothing Then
                State.MyBO = CType(State.InputParameters.ClaimBO, ClaimBase)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimBase, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase

        Public Sub New(claimBO As ClaimBase)
            Me.ClaimBO = claimBO
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage("CLAIM") & ElitaBase.Sperator & MasterPage.PageTab
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.InputParameters = CType(CallingPar, Parameters)
                State.MyBO = State.InputParameters.ClaimBO
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(LBL_TXT_CLAIM_DEDUCTIBLE_REFUND)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LBL_TXT_CLAIM_SUMMARY)

        UpdateBreadCrum()
        MasterPage.MessageController.Clear()

        lblGrdHdr.Text = TranslationBase.TranslateLabelOrMessage(LBL_TXT_CLAIM_DEDUCTIBLE_REFUND)

        Try
            If Not IsPostBack Then
                PopulateFormFromBO()
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateFormFromBO()
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With State.MyBO
            PopulateControlFromBOProperty(lblCustomerNameValue, .CustomerName)
            PopulateControlFromBOProperty(lblClaimNumberValue, .ClaimNumber)
            PopulateControlFromBOProperty(lblDealerNameValue, .DealerName)
            PopulateControlFromBOProperty(lblCertificateNumberValue, .CertificateNumber)
            PopulateControlFromBOProperty(lblClaimStatusValue, LookupListNew.GetClaimStatusFromCode(langId, .StatusCode))
            PopulateControlFromBOProperty(lblDateOfLossValue, GetDateFormattedStringNullable(.LossDate.Value))
            PopulateControlFromBOProperty(lblSerialNumberImeiValue, .SerialNumber)
            PopulateControlFromBOProperty(lblWorkPhoneNumberValue, .MobileNumber)

            If (State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = STATE_ACTIVE
            Else
                cssClassName = STATE_CLOSED
            End If
            ClaimStatusTD.Attributes.Item(STRING_CLASS) = cssClassName
        End With

        Dim oCertificate As Certificate = New Certificate(State.MyBO.CertificateId)
        Dim oDealer As New Dealer(State.MyBO.CompanyId, State.MyBO.DealerCode)

        PopulateControlFromBOProperty(lblDealerGroupValue, oDealer.DealerGroupName)
        PopulateControlFromBOProperty(lblSubscriberStatusValue, LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode))
        If (oCertificate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
            cssClassName = STATE_ACTIVE
        Else
            cssClassName = STATE_CLOSED
        End If
        SubStatusTD.Attributes.Item(STRING_CLASS) = SubStatusTD.Attributes.Item(STRING_CLASS) & " " & cssClassName

        Dim LanguageCode = Thread.CurrentPrincipal.GetLanguageCode()
        Dim oRefundTypeDropDown As ListItem() = CommonConfigManager.Current.ListManager.GetList(LIST_CODE_RFM, LanguageCode)

        ddlRefundType.Populate(oRefundTypeDropDown, New PopulateOptions() With {.AddBlankItem = False, .ValueFunc = AddressOf PopulateOptions.GetExtendedCode})
        txtClmDedRefundAmount.Text = GetAmountFormattedString(State.MyBO.Deductible)

        If (State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED OrElse State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED) Then
            MasterPage.MessageController.AddWarning(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_DEDUCTIBLE_REFUND_CLAIM_STATUS))
        End If

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple AndAlso State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING) Then
            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO)
            MyBase.ReturnToCallingPage(retObj)
        Else
            NavController.Navigate(Me, FlowEvents.EVENT_CANCEL, New ClaimForm.Parameters(State.MyBO.Id))
        End If

    End Sub

    Private Sub SaveClaimDeductibleRefund(sender As Object, Args As EventArgs) Handles btnSubmit.Click

        Try
            If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then
                If validateRefundAmount(txtClmDedRefundAmount.Text, State.MyBO.Deductible) Then
                    Dim multauth As MultiAuthClaim = CType(State.MyBO, MultiAuthClaim)
                    multauth.AddClaimAuthForDeductibleRefund(State.MyBO.ServiceCenterId, txtClmDedRefundAmount.Text, ddlRefundType.SelectedValue)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    btnSubmit.Enabled = False
                End If
            Else
                MasterPage.MessageController.AddWarning(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_DEDUCTIBLE_REFUND_MULTI_AUTH_CLAIM))
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function validateRefundAmount(deductibleRefundAmt As String, claimDedutiable As Decimal) As Boolean

        Dim blnSuccess As Boolean = True
        Dim decAmt As Decimal

        If Decimal.TryParse(deductibleRefundAmt, decAmt) Then
            If (decAmt > 0 AndAlso decAmt <= claimDedutiable) Then
                blnSuccess = True
            Else
                blnSuccess = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_REFUND_AMOUNT))
            End If
        Else
            blnSuccess = False
            MasterPage.MessageController.AddError(lblClmDedRefundAmount.Text & ": " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR))
        End If
        Return blnSuccess
    End Function

#End Region

End Class
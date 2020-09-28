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
            If (Me.NavController Is Nothing) Then
                Return CType(MyBase.State, MyState)
            Else
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    InitializeFromFlowSession()
                End If
                Return CType(Me.NavController.State, MyState)
            End If
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)

        Try
            If Not Me.State.InputParameters Is Nothing Then
                Me.State.MyBO = CType(Me.State.InputParameters.ClaimBO, ClaimBase)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public BoChanged As Boolean = False
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ClaimBase, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand)
            Me.LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase

        Public Sub New(ByVal claimBO As ClaimBase)
            Me.ClaimBO = claimBO
        End Sub
    End Class
#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage("CLAIM") & ElitaBase.Sperator & Me.MasterPage.PageTab
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.InputParameters = CType(CallingPar, Parameters)
                State.MyBO = Me.State.InputParameters.ClaimBO
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(LBL_TXT_CLAIM_DEDUCTIBLE_REFUND)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LBL_TXT_CLAIM_SUMMARY)

        UpdateBreadCrum()
        Me.MasterPage.MessageController.Clear()

        lblGrdHdr.Text = TranslationBase.TranslateLabelOrMessage(LBL_TXT_CLAIM_DEDUCTIBLE_REFUND)

        Try
            If Not Me.IsPostBack Then
                PopulateFormFromBO()
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateFormFromBO()
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.lblCustomerNameValue, .CustomerName)
            Me.PopulateControlFromBOProperty(Me.lblClaimNumberValue, .ClaimNumber)
            Me.PopulateControlFromBOProperty(Me.lblDealerNameValue, .DealerName)
            Me.PopulateControlFromBOProperty(Me.lblCertificateNumberValue, .CertificateNumber)
            Me.PopulateControlFromBOProperty(Me.lblClaimStatusValue, LookupListNew.GetClaimStatusFromCode(langId, .StatusCode))
            Me.PopulateControlFromBOProperty(Me.lblDateOfLossValue, GetDateFormattedStringNullable(.LossDate.Value))
            Me.PopulateControlFromBOProperty(Me.lblSerialNumberImeiValue, .SerialNumber)
            Me.PopulateControlFromBOProperty(Me.lblWorkPhoneNumberValue, .MobileNumber)

            If (Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                cssClassName = STATE_ACTIVE
            Else
                cssClassName = STATE_CLOSED
            End If
            ClaimStatusTD.Attributes.Item(STRING_CLASS) = cssClassName
        End With

        Dim oCertificate As Certificate = New Certificate(Me.State.MyBO.CertificateId)
        Dim oDealer As New Dealer(Me.State.MyBO.CompanyId, Me.State.MyBO.DealerCode)

        Me.PopulateControlFromBOProperty(Me.lblDealerGroupValue, oDealer.DealerGroupName)
        Me.PopulateControlFromBOProperty(Me.lblSubscriberStatusValue, LookupListNew.GetClaimStatusFromCode(langId, oCertificate.StatusCode))
        If (oCertificate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
            cssClassName = STATE_ACTIVE
        Else
            cssClassName = STATE_CLOSED
        End If
        SubStatusTD.Attributes.Item(STRING_CLASS) = SubStatusTD.Attributes.Item(STRING_CLASS) & " " & cssClassName

        Dim LanguageCode = Thread.CurrentPrincipal.GetLanguageCode()
        Dim oRefundTypeDropDown As ListItem() = CommonConfigManager.Current.ListManager.GetList(LIST_CODE_RFM, LanguageCode)

        ddlRefundType.Populate(oRefundTypeDropDown, New PopulateOptions() With {.AddBlankItem = False, .ValueFunc = AddressOf PopulateOptions.GetExtendedCode})
        txtClmDedRefundAmount.Text = Me.GetAmountFormattedString(Me.State.MyBO.Deductible)

        If (Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED Or Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__DENIED) Then
            Me.MasterPage.MessageController.AddWarning(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_DEDUCTIBLE_REFUND_CLAIM_STATUS))
        End If

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple AndAlso Me.State.MyBO.StatusCode = Codes.CLAIM_STATUS__PENDING) Then
            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO)
            MyBase.ReturnToCallingPage(retObj)
        Else
            Me.NavController.Navigate(Me, FlowEvents.EVENT_CANCEL, New ClaimForm.Parameters(Me.State.MyBO.Id))
        End If

    End Sub

    Private Sub SaveClaimDeductibleRefund(ByVal sender As Object, ByVal Args As EventArgs) Handles btnSubmit.Click

        Try
            If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then
                If validateRefundAmount(txtClmDedRefundAmount.Text, Me.State.MyBO.Deductible) Then
                    Dim multauth As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
                    multauth.AddClaimAuthForDeductibleRefund(Me.State.MyBO.ServiceCenterId, txtClmDedRefundAmount.Text, ddlRefundType.SelectedValue)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    btnSubmit.Enabled = False
                End If
            Else
                Me.MasterPage.MessageController.AddWarning(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_DEDUCTIBLE_REFUND_MULTI_AUTH_CLAIM))
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function validateRefundAmount(ByVal deductibleRefundAmt As String, claimDedutiable As Decimal) As Boolean

        Dim blnSuccess As Boolean = True
        Dim decAmt As Decimal

        If Decimal.TryParse(deductibleRefundAmt, decAmt) Then
            If (decAmt > 0 And decAmt <= claimDedutiable) Then
                blnSuccess = True
            Else
                blnSuccess = False
                Me.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_DEDUCTIBLE_REFUND_AMOUNT))
            End If
        Else
            blnSuccess = False
            Me.MasterPage.MessageController.AddError(lblClmDedRefundAmount.Text & ": " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR))
        End If
        Return blnSuccess
    End Function

#End Region

End Class
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class ClaimReimbursementForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Page State"
    Class MyState
        Public BankInfoBO As BankInfo
        Public claimBO As Claim
        Public claimIssueBO As ClaimIssue
        Public EntityIssueId As Guid
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
                'StartNavControl()
                Dim params As Parameters = CType(CallingParameters, Parameters)
                If Not params.ClaimIssueID.Equals(Guid.Empty) Then
                    State.claimIssueBO = New ClaimIssue(params.ClaimIssueID)
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "Navigation Control"
    'Sub StartNavControl()
    '    'If Me.NavController Is Nothing Then
    '    Dim nav As New ElitaPlusNavigation
    '    Me.NavController = New NavControllerBase(nav.Flow("PAY_CLAIM_DETAIL"))
    '    'End If
    'End Sub
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimIssueID As Guid
        Public Sub New(claimIssueID As Guid)
            Me.ClaimIssueID = claimIssueID
            Me.ClaimIssueID = New Guid(GuidControl.HexToByteArray("5D16DE71C40841B0E053E505640ADE35"))
        End Sub

    End Class
#End Region
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear_Hide()

        If (Not Page.IsPostBack) Then
            UpdateBreadCrum()

            State.EntityIssueId = New Guid(GuidControl.HexToByteArray("5D16DE71C40841B0E053E505640ADE35"))
            State.claimIssueBO = New ClaimIssue(State.EntityIssueId)
            populateDropdowns()
            EnableDisableFields()
        End If
    End Sub

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("CLAIM") & " " & TranslationBase.TranslateLabelOrMessage("PAYMENT_INSTRUMENT")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PAYMENT_INSTRUMENT")
            End If
        End If
    End Sub

    Private Sub EnableDisableFields()
        moBankInfoController.Visible = False
        ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
    End Sub

    Private Sub populateDropdowns()
        PopulatePaymentMethodDropdown()

    End Sub

    Private Sub PopulatePaymentMethodDropdown()

        Try
            Dim filteredYearList As ListItem()
            Dim paymentList As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTHD", Thread.CurrentPrincipal.GetLanguageCode())
            filteredYearList = (From x In paymentList
                                Where x.Code = "CTT" Or x.Code = "DGFT"
                                Select x).ToArray()

            ddlPaymentList.Populate(filteredYearList, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True
                                               })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub ddlPaymentList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentList.SelectedIndexChanged
        Try
            If (ddlPaymentList.SelectedIndex > 0) Then
                If (ddlPaymentList.SelectedItem.Text.ToUpper() = "BANK TRANSFER") Then
                    moBankInfoController.Visible = True
                    State.BankInfoBO = New BankInfo()
                    moBankInfoController.State.myBankInfoBo = State.BankInfoBO
                    moBankInfoController.Bind(State.BankInfoBO)
                    moBankInfoController.State.myBankInfoBo.SepaEUBankTransfer = True
                    moBankInfoController.SetCountryValue(State.claimIssueBO.Claim.Certificate.CountryPurchaseId)

                    moBankInfoController.EnableDisableRequiredControls()
                    ControlMgr.SetVisibleControl(Me, btnSave_WRITE, True)
                Else
                    moBankInfoController.Visible = False
                    ControlMgr.SetVisibleControl(Me, btnSave_WRITE, True)
                End If
            Else
                moBankInfoController.Visible = False
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_WRITE.Click
        Try
            If (ddlPaymentList.SelectedIndex > 0) Then
                If (ddlPaymentList.SelectedItem.Text.ToUpper() <> "BANK TRANSFER") Then
                    SaveGiftCardResponse()
                Else
                    SaveBankInfoResponse()
                End If
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                EnableDisableAfterSave()
            Else
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION, True)
                'Throw New GUIException(Message.ERR_SAVING_DATA, ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SaveBankInfoResponse()

        Dim strAnswerCode As String = String.Empty
        moBankInfoController.PopulateBOFromControl()
        Dim bankInfoId As Guid = moBankInfoController.State.myBankInfoBo.Id

        If (Not bankInfoId.Equals(Guid.Empty)) Then
            'Get the Answer Data based on the Answer code 
            If (State.claimIssueBO.IssueCode = "CFI_PYMINSSEL") Then
                strAnswerCode = "FI_PYMINS_BT"
            ElseIf (State.claimIssueBO.IssueCode = "CFI_LFLDEVNA") Then
                strAnswerCode = "FI_CR_BT"
            End If

            CreateClaimIssueResponse(strAnswerCode, bankInfoId)

        End If
    End Sub

    Private Sub SaveGiftCardResponse()
        'Get the Answer Data based on the Answer code 
        Dim strAnswerCode As String = String.Empty

        If (State.claimIssueBO.IssueCode = "CFI_PYMINSSEL") Then
            strAnswerCode = "FI_PYMINS_GC"
        ElseIf (State.claimIssueBO.IssueCode = "CFI_LFLDEVNA") Then
            strAnswerCode = "FI_CR_GC"
        End If

        CreateClaimIssueResponse(strAnswerCode, Nothing)
        'If (Not dsAnswerData Is Nothing AndAlso dsAnswerData.Tables.Count > 0 AndAlso dsAnswerData.Tables(0).Rows.Count > 0) Then
        '    Dim dtTemp As DataTable = dsAnswerData.Tables(0)
        '    SaveClaimIssueResponseData(GuidControl.ByteArrayToGuid(dtTemp.Rows(0)("answer_id")),
        '                               GuidControl.ByteArrayToGuid(dtTemp.Rows(0)("supports_claim_id")),
        '                               dtTemp.Rows(0)("description"))
        'End If

    End Sub

    Private Sub CreateClaimIssueResponse(answerCode As String, bankInfoId As Guid)

        Dim dsAnswerData As DataSet
        dsAnswerData = Answer.GetAnswerDataByCode(answerCode)

        If (dsAnswerData IsNot Nothing AndAlso dsAnswerData.Tables.Count > 0 AndAlso dsAnswerData.Tables(0).Rows.Count > 0) Then
            Dim dtTemp As DataTable = dsAnswerData.Tables(0)

            Dim claimIssResp As ClaimIssueResponse = New ClaimIssueResponse()
            claimIssResp.ClaimIssueId = State.claimIssueBO.ClaimIssueId
            claimIssResp.AnswerId = GuidControl.ByteArrayToGuid(dtTemp.Rows(0)("answer_id"))
            claimIssResp.SupportsClaimId = GuidControl.ByteArrayToGuid(dtTemp.Rows(0)("supports_claim_id"))
            claimIssResp.AnswerDescription = dtTemp.Rows(0)("description")
            claimIssResp.AnswerValue = If(bankInfoId.Equals(Guid.Empty), String.Empty, GuidControl.GuidToHexString(bankInfoId))

            SaveClaimIssueResponseData(claimIssResp)
        End If

    End Sub

    Private Sub SaveClaimIssueResponseData(cir As ClaimIssueResponse)
        Try
            If (cir IsNot Nothing) Then
                cir.Save()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try


    End Sub

    Private Sub EnableDisableAfterSave()
        ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ReturnToCallingPage()
    End Sub

End Class
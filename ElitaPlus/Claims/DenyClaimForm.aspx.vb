Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Public Class DenyClaimForm
    Inherits ElitaPlusPage

#Region "Constants"
    Public Const URL As String = "DenyClaimForm.aspx"
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As ClaimBase
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Try
            Me.State.MyBO = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM), ClaimBase)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrorCtrl.Clear_Hide()
        Try

            If Me.NavController.CurrentNavState.Name <> "DENY_CLAIM" Then
                Return
            End If

            If Not Me.IsPostBack Then
                If Me.State.MyBO Is Nothing Then
                    Return
                End If
                Trace(Me, "Claim Id=" & GuidControl.GuidToHexString(Me.State.MyBO.Id))
                PopulateDropdowns()
            End If
            BindBoPropertiesToLabels()
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

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DeniedReasonId", Me.lblDeniedReason)
        If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
            Me.BindBOPropertyToLabel(CType(Me.State.MyBO, Claim), "AuthorizationNumber", Me.lblInvoiceNumber)
        End If
    End Sub

    Protected Sub PopulateDropdowns()
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId, True)
        'Me.BindListControlToDataView(Me.cboDeniedReason, LookupListNew.GetDeniedReasonLookupList(Authentication.LangId))
        'KDDI CHANGES
        Dim listcontextForMgList As ListContext = New ListContext()
        listcontextForMgList.CompanyGroupId = Me.State.MyBO.Company.CompanyGroupId
        listcontextForMgList.DealerId = Me.State.MyBO.Dealer.Id
        listcontextForMgList.CompanyId = Me.State.MyBO.CompanyId
        'listcontextForMgList.DealerGroupId = Me.State.MyBO.Dealer.DealerGroupId


        Dim deniedReason As ListItem() = CommonConfigManager.Current.ListManager.GetList("DNDREASON", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontextForMgList)
        'If Not (deniedReason Is Nothing) Then
        '    If Not deniedReason.Length > 0 Then
        '        deniedReason = CommonConfigManager.Current.ListManager.GetList("DNDREASON", Thread.CurrentPrincipal.GetLanguageCode())
        '    End If
        'End If
        cboDeniedReason.Populate(deniedReason, New PopulateOptions() With
                                  {
                                    .AddBlankItem = True
                                   })


        Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        cboFraudulent.Populate(yesNoLkL, New PopulateOptions() With
                                  {
                                    .AddBlankItem = True
                                   })
        cboComplaint.Populate(yesNoLkL, New PopulateOptions() With
                                  {
                                    .AddBlankItem = True
                                   })
        'Me.BindListControlToDataView(Me.cboFraudulent, yesNoLkL)
        'Me.BindListControlToDataView(Me.cboComplaint, yesNoLkL)
    End Sub

    Protected Sub PopulateBOsFromForm()

        Me.State.MyBO.CalculateFollowUpDate()
        Me.PopulateBOProperty(Me.State.MyBO, "DeniedReasonId", Me.cboDeniedReason)
        Me.PopulateBOProperty(Me.State.MyBO, "Fraudulent", Me.cboFraudulent)
        Me.PopulateBOProperty(Me.State.MyBO, "Complaint", Me.cboComplaint)

        If (Me.State.MyBO.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
            Me.PopulateBOProperty(CType(Me.State.MyBO, Claim), "AuthorizationNumber", Me.txtInvoiceNumber)
        End If

    End Sub
#End Region

#Region "Button Clicks"
    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.NavController.Navigate(Me, FlowEvents.EVENT_CANCEL)
    End Sub

    Protected Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
        Try
            PopulateBOsFromForm()
            If Me.State.MyBO.DeniedReasonId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(Me.lblDeniedReason)
                Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DENIED_REASON_IS_REQUIRED_ERR)
            End If

            Select Case Me.State.MyBO.ClaimAuthorizationType
                Case ClaimAuthorizationType.Single
                    Dim claim As Claim = CType(Me.State.MyBO, Claim)
                    claim.AuthorizedAmount = New DecimalType(0D)
                    claim.Deductible = New DecimalType(0D)
                    claim.DenyClaim()
                    claim.IsUpdatedComment = True
                    claim.Save()
                Case ClaimAuthorizationType.Multiple
                    Dim claim As MultiAuthClaim = CType(Me.State.MyBO, MultiAuthClaim)
                    claim.Deductible = New DecimalType(0D)
                    claim.VoidAuthorizations()
                    claim.DenyClaim()
                    claim.IsUpdatedComment = True
                    claim.Save()
            End Select


            If Me.NavController.Context = "CLAIM_DETAIL-DENY_CLAIM" Then
                Me.NavController.Navigate(Me, FlowEvents.EVENT_NEXT, Message.MSG_CLAIM_UPDATED)
            Else
                Me.NavController.Navigate(Me, FlowEvents.EVENT_NEXT, Message.MSG_CLAIM_ADDED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region
End Class
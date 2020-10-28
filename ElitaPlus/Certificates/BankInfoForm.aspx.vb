Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Microsoft.VisualBasic
Imports policyservice = Assurant.ElitaPlus.ElitaPlusWebApp.PolicyService

Namespace Certificates
    Public Class BankInfoForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "~/Certificates/BankInfoForm.aspx"
#End Region

#Region "Page State"
        ' This class keeps the current state for the page.
        Class MyState
            Public MyBO As BankInfo
            Public MyCertificate As Certificate
            Public CertificateId As Guid
            Public MyCertInstallment As CertInstallment
            Public MyCompany As Company
            Public MyBankInfo As BankInfo
            Public MyBankInfoEditable As BankInfo
            Public MyCountry As Country
            Public IsEditMode As Boolean
            Public IsAfterSave As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "Properties"

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CertRegisteredItem
            Public HasDataChanged As Boolean
            Public CallingObjName As String
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CertRegisteredItem, ByVal hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                hasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("BankInformation")

            Try
                MasterPage.MessageController.Clear_Hide()
                MenuEnabled = False

                If Not Me.IsPostBack Then
                    PopulateFormFromBOs()



                End If
            Catch ex As Exception
                CleanPopupInput()
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then

                    'Get the id from the parent
                    Me.State.CertificateId = CType(Me.CallingParameters, Guid)
                    Me.State.MyCertificate = Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE)
                    Me.State.MyCertInstallment = Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_INSTALLMENT)
                    Me.State.MyBankInfo = Me.NavController.FlowSession(FlowSessionKeys.SESSION_BANK_INFO)
                    Me.State.MyCompany = New Company(Me.State.MyCertificate.CompanyId)
                    If Not Me.State.MyCompany Is Nothing Then
                        Me.State.MyCountry = New Country(Me.State.MyCompany.CountryId)
                    End If

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster, False)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (Not State Is Nothing) Then
                If (Not Me.State.MyBO Is Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("Bank_Information")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Bank_Information") & " " & "Register Bank Information"
                End If
            End If
        End Sub

        Private Sub CleanPopupInput()
            Try
                If Not State Is Nothing Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try

        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                Me.moCertificateInfoController.InitController(Me.State.CertificateId, , Me.State.MyCompany.Code)

                Me.State.MyBankInfoEditable = New BankInfo()

                If Not Me.State.MyBankInfo Is Nothing Then

                    With Me.State.MyBankInfoEditable
                        '.Id = Me.State.MyBankInfo.Id
                        .CountryID = Me.State.MyBankInfo.CountryID
                        .Account_Name = Me.State.MyBankInfo.Account_Name
                        .Bank_Id = Me.State.MyBankInfo.Bank_Id
                        .Account_Number = Me.State.MyBankInfo.Account_Number
                        .SwiftCode = Me.State.MyBankInfo.SwiftCode
                        .IbanNumber = Me.State.MyBankInfo.IbanNumber
                        .AccountTypeId = Me.State.MyBankInfo.AccountTypeId
                        .PaymentReasonID = Me.State.MyBankInfo.PaymentReasonID
                        .BranchName = Me.State.MyBankInfo.BranchName
                        .BankName = Me.State.MyBankInfo.BankName
                        .BankSortCode = Me.State.MyBankInfo.BankSortCode
                        .BankSubCode = Me.State.MyBankInfo.BankSubCode
                        .TransactionLimit = Me.State.MyBankInfo.TransactionLimit
                        .BankLookupCode = Me.State.MyBankInfo.BankLookupCode
                        .SourceCountryID = Me.State.MyBankInfo.SourceCountryID
                        .ValidateFieldsforBR = Me.State.MyBankInfo.ValidateFieldsforBR
                        .DomesticTransfer = Me.State.MyBankInfo.DomesticTransfer
                        .InternationalTransfer = Me.State.MyBankInfo.InternationalTransfer
                        .InternationalEUTransfer = Me.State.MyBankInfo.InternationalEUTransfer
                        .BranchDigit = Me.State.MyBankInfo.BranchDigit
                        .AccountDigit = Me.State.MyBankInfo.AccountDigit
                        .BranchNumber = Me.State.MyBankInfo.BranchNumber
                        .PaymentMethodId = Me.State.MyBankInfo.PaymentMethodId
                        .PayeeId = Me.State.MyBankInfo.PayeeId
                        .TaxId = Me.State.MyBankInfo.TaxId
                    End With

                End If


                Me.moPremiumBankInfoController.Bind(Me.State.MyBankInfo)

                Me.moPremiumBankInfoController.DisableAllFields()
                If String.IsNullOrEmpty(Me.State.MyBankInfo.BankLookupCode) AndAlso String.IsNullOrEmpty(Me.State.MyBankInfo.IbanNumber) Then
                    Me.moPremiumBankInfoController.SwitchToEditView()
                End If

                Me.moPremiumBankInfoController.Visible = True

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Handlers"
        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                Me.ReturnToCallingPage()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                If Not Me.State.MyCertInstallment Is Nothing Then
                    Me.moPremiumBankInfoController.PopulateBOFromControl()

                    BankInfoEndorseRequest(Me.State.MyBankInfo)

                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    PopulateFormFromBOs()
                    Me.moPremiumBankInfoController.SwitchToReadOnlyView()
                End If

            Catch ex As ApplicationException
                Me.HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub
        Private Sub btnEdit_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                Me.moPremiumBankInfoController.SwitchToEditView()

            Catch ex As ApplicationException
                Me.HandleErrors(ex, Me.MasterPage.MessageController)

            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As EventArgs) Handles btnUndo_WRITE.Click
            PopulateFormFromBOs()
            Me.moPremiumBankInfoController.SwitchToReadOnlyView()

        End Sub
#End Region
#Region "Private Methods"
        Private Function GetClient() As PolicyService.PolicyServiceClient
            Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__SVC_CERT_ENROLL), False)
            Dim client = New PolicyService.PolicyServiceClient("CustomBinding_IPolicyService", oWebPasswd.Url)
            client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            client.ClientCredentials.UserName.Password = oWebPasswd.Password
            Return client
        End Function
        Private Sub BankInfoEndorseRequest(ByVal bankinfo As BankInfo)
            Dim endorseRequest As PolicyService.EndorsePolicyRequest = New PolicyService.EndorsePolicyRequest
            Dim updateBankInfo As PolicyService.UpdateBankInfo = New PolicyService.UpdateBankInfo
            updateBankInfo.EndorsementReason = PolicyService.EndorsementPolicyReasons.BankFulfillment

            updateBankInfo.AccountOwnerName = bankinfo.Account_Name
            updateBankInfo.BankSortCode = bankinfo.BankSortCode
            updateBankInfo.SwiftCode = bankinfo.SwiftCode
            updateBankInfo.BankLookupCode = bankinfo.BankLookupCode
            updateBankInfo.BankName = bankinfo.BankName
            updateBankInfo.BranchNumber = bankinfo.BranchNumber
            updateBankInfo.IbanCode = bankinfo.IbanNumber

            endorseRequest.DealerCode = Me.State.MyCertificate.Dealer.Dealer
            endorseRequest.CertificateNumber = Me.State.MyCertificate.CertNumber
            endorseRequest.Requests = New PolicyService.BasePolicyEndorseAction() {updateBankInfo}
            Try
                WcfClientHelper.Execute(Of PolicyService.PolicyServiceClient, PolicyService.IPolicyService, PolicyService.EndorseResponse)(
                                                                            GetClient(),
                                                                            New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                             Function(ByVal c As PolicyService.PolicyServiceClient)
                                                                                 Return c.Endorse(endorseRequest)
                                                                             End Function)
            Catch ex As Exception
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_POLICYSERVICE_SERVICE_ERR, True)
                Throw
            End Try
        End Sub

#End Region
    End Class
End Namespace
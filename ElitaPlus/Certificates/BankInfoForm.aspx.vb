Imports Microsoft.VisualBasic

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
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As CertRegisteredItem, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                hasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("BankInformation")

            Try
                MasterPage.MessageController.Clear_Hide()
                MenuEnabled = False

                If Not IsPostBack Then
                    PopulateFormFromBOs()



                End If
            Catch ex As Exception
                CleanPopupInput()
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then

                    'Get the id from the parent
                    State.CertificateId = CType(CallingParameters, Guid)
                    State.MyCertificate = NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE)
                    State.MyCertInstallment = NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_INSTALLMENT)
                    State.MyBankInfo = NavController.FlowSession(FlowSessionKeys.SESSION_BANK_INFO)
                    State.MyCompany = New Company(State.MyCertificate.CompanyId)
                    If State.MyCompany IsNot Nothing Then
                        State.MyCountry = New Country(State.MyCompany.CountryId)
                    End If



                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster, False)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State.MyBO IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("Bank_Information")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Bank_Information") & " " & "Register Bank Information"
                End If
            End If
        End Sub

        Private Sub CleanPopupInput()
            Try
                If State IsNot Nothing Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try

        End Sub

        Protected Sub PopulateFormFromBOs()
            Try
                moCertificateInfoController.InitController(State.CertificateId, , State.MyCompany.Code)

                State.MyBankInfoEditable = New BankInfo()

                If State.MyBankInfo IsNot Nothing Then

                    With State.MyBankInfoEditable
                        '.Id = Me.State.MyBankInfo.Id
                        .CountryID = State.MyBankInfo.CountryID
                        .Account_Name = State.MyBankInfo.Account_Name
                        .Bank_Id = State.MyBankInfo.Bank_Id
                        .Account_Number = State.MyBankInfo.Account_Number
                        .SwiftCode = State.MyBankInfo.SwiftCode
                        .IbanNumber = State.MyBankInfo.IbanNumber
                        .AccountTypeId = State.MyBankInfo.AccountTypeId
                        .PaymentReasonID = State.MyBankInfo.PaymentReasonID
                        .BranchName = State.MyBankInfo.BranchName
                        .BankName = State.MyBankInfo.BankName
                        .BankSortCode = State.MyBankInfo.BankSortCode
                        .BankSubCode = State.MyBankInfo.BankSubCode
                        .TransactionLimit = State.MyBankInfo.TransactionLimit
                        .BankLookupCode = State.MyBankInfo.BankLookupCode
                        .SourceCountryID = State.MyBankInfo.SourceCountryID
                        .ValidateFieldsforBR = State.MyBankInfo.ValidateFieldsforBR
                        .DomesticTransfer = State.MyBankInfo.DomesticTransfer
                        .InternationalTransfer = State.MyBankInfo.InternationalTransfer
                        .InternationalEUTransfer = State.MyBankInfo.InternationalEUTransfer
                        .BranchDigit = State.MyBankInfo.BranchDigit
                        .AccountDigit = State.MyBankInfo.AccountDigit
                        .BranchNumber = State.MyBankInfo.BranchNumber
                        .PaymentMethodId = State.MyBankInfo.PaymentMethodId
                        .PayeeId = State.MyBankInfo.PayeeId
                        .TaxId = State.MyBankInfo.TaxId
                    End With

                End If


                moPremiumBankInfoController.Bind(State.MyBankInfo)

                moPremiumBankInfoController.DisableAllFields()
                If String.IsNullOrEmpty(State.MyBankInfo.BankLookupCode) AndAlso String.IsNullOrEmpty(State.MyBankInfo.IbanNumber) Then
                    moPremiumBankInfoController.SwitchToEditView()
                End If

                moPremiumBankInfoController.Visible = True

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region


#Region "Button Handlers"
        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                ReturnToCallingPage()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                If State.MyCertInstallment IsNot Nothing Then
                    moPremiumBankInfoController.PopulateBOFromControl()
                    State.MyBankInfo.Save()

                    State.MyCertInstallment.BankInfoId = State.MyBankInfo.Id
                    State.MyCertInstallment.BillingStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_STATUS, Codes.BILLING_STATUS__ACTIVE)
                    State.MyCertInstallment.Save()

                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    PopulateFormFromBOs()
                    moPremiumBankInfoController.SwitchToReadOnlyView()
                End If

            Catch ex As ApplicationException
                HandleErrors(ex, MasterPage.MessageController)

            End Try
        End Sub
        Private Sub btnEdit_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                moPremiumBankInfoController.SwitchToEditView()

            Catch ex As ApplicationException
                HandleErrors(ex, MasterPage.MessageController)

            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As EventArgs) Handles btnUndo_WRITE.Click
            PopulateFormFromBOs()
            moPremiumBankInfoController.SwitchToReadOnlyView()

        End Sub
#End Region
    End Class
End Namespace
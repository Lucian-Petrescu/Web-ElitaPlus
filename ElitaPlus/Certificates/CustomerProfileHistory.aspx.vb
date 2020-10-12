Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualBasic
Imports System.Web.Services
Namespace Certificates
    Partial Class CustomerProfileHistory
        Inherits ElitaPlusPage
#Region "Constants"
        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const PAID_COMMAND As String = "PaidAction"

        Public Const URL As String = "~/Certificates/CustomerProfileHistory.aspx"
        Public Const PAGETITLE As String = "CUSTOMER_PROFILE_HISTORY"
        Public Const PAGETAB As String = "CERTIFICATES"

#End Region
#Region "Page State"
        ' This class keeps the current state for the page.
        Class MyState

            Public oCert As Certificate
            Public oDealer As Dealer
            Public oCustBankInfo As Certificate.CustomerBankDetailDV = Nothing
            Public CustPersonalHistDV As Certificate.CustPersonelHistoryDV = Nothing
            Public CustAddressHistDV As Certificate.CustAddressHistoryDV = Nothing
            Public CustContactHistDV As Certificate.CustContactHistoryDV = Nothing
            Public CustBankDetailHistDV As Certificate.CustBankDetailHistoryDV = Nothing
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

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State.oCert Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("Customer_Profile_History")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Customer_Profile_History")
                End If
            End If
        End Sub
#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
            Me.UpdateBreadCrum()
            Try
                If Not Me.IsPostBack Then
                    PopulateCustomerProfileInfo()
                    PopulateCustomerBankInfo()
                    Me.TranslateGridHeader(Me.CustPersonalHistory)
                    PopulateCustPersonalHistory()
                    Me.TranslateGridHeader(Me.CustAddressHistory)
                    PopulateCustAddressHistory()
                    Me.TranslateGridHeader(Me.CustContactHistory)
                    PopulateCustContactHistory()
                    Me.TranslateGridHeader(Me.CustBankDetailHistory)
                    PopulateCustBankDetailHistory()
                    Me.MasterPage.MessageController.Clear()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Private Sub CustomerProfileHistory_PageCall(CallFromUrl As String, CallingPar As Object) Handles Me.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.oCert = New Certificate(CType(Me.CallingParameters, Guid))
                    Me.State.oDealer = New Dealer(Me.State.oCert.DealerId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster, False)
            End Try
        End Sub
#End Region
        Public Sub PopulateCustomerProfileInfo()
            If Not Me.State.oCert Is Nothing Then
                Dim dv As DataView = Me.State.oCert.GetOtherCustomerDetails(Me.State.oCert.CustomerId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.oDealer.IdentificationNumberType)

                If Not dv.Table Is Nothing AndAlso dv.Table.Rows.Count > 0 Then


                    Me.CustomerNameTD.InnerText = dv.Table.Rows(0)("salutation") & " " & dv.Table.Rows(0)("Customer_first_name") & " " & dv.Table.Rows(0)("customer_middle_name") & " " & dv.Table.Rows(0)("customer_last_name")
                    If Not dv.Table.Rows(0)("identification_number") Is System.DBNull.Value Then
                        Me.IdentificationNumberTD.InnerText = dv.Table.Rows(0)("identification_number")
                    End If
                    If Not dv.Table.Rows(0)("gender") Is System.DBNull.Value Then
                        Me.GenderTD.InnerText = dv.Table.Rows(0)("gender")
                    End If
                    If Not dv.Table.Rows(0)("marital_status") Is System.DBNull.Value Then
                        Me.MaritalStatusTD.InnerText = dv.Table.Rows(0)("marital_status")
                    End If
                    If Not dv.Table.Rows(0)("place_of_birth") Is System.DBNull.Value Then
                        Me.PlaceOfBirthTD.InnerText = dv.Table.Rows(0)("place_of_birth")
                    End If
                    If Not dv.Table.Rows(0)("date_of_birth") Is System.DBNull.Value Then
                        Me.DateOfBirthTD.InnerText = dv.Table.Rows(0)("date_of_birth")
                    End If
                    If Not dv.Table.Rows(0)("nationality") Is System.DBNull.Value Then
                        Me.NationalityTD.InnerText = dv.Table.Rows(0)("nationality")
                    End If
                    If Not dv.Table.Rows(0)("email") Is System.DBNull.Value Then
                        Me.EmailTD.InnerText = dv.Table.Rows(0)("email")
                    End If
                    If Not dv.Table.Rows(0)("home_phone") Is System.DBNull.Value Then
                        Me.HomePhoneTD.InnerText = dv.Table.Rows(0)("home_phone")
                    End If
                    If Not dv.Table.Rows(0)("work_phone") Is System.DBNull.Value Then
                        Me.WorkPhoneTD.InnerText = dv.Table.Rows(0)("work_phone")
                    End If
                    If Not dv.Table.Rows(0)("address1") Is System.DBNull.Value Or Not dv.Table.Rows(0)("address2") Is System.DBNull.Value Or Not dv.Table.Rows(0)("address3") Is System.DBNull.Value Then
                        Me.AddressTD.InnerText = dv.Table.Rows(0)("address1") & " " & dv.Table.Rows(0)("address2") & " " & dv.Table.Rows(0)("address3")
                    End If
                    If Not dv.Table.Rows(0)("city") Is System.DBNull.Value Then
                        Me.CityTD.InnerText = dv.Table.Rows(0)("city")
                    End If
                    If Not dv.Table.Rows(0)("state") Is System.DBNull.Value Then
                        Me.StateTD.InnerText = dv.Table.Rows(0)("state")
                    End If
                    If Not dv.Table.Rows(0)("postal_code") Is System.DBNull.Value Then
                        Me.PostalCodeTD.InnerText = dv.Table.Rows(0)("postal_code")
                    End If
                    If Not dv.Table.Rows(0)("country") Is System.DBNull.Value Then
                        Me.CountryCodeTD.InnerText = dv.Table.Rows(0)("country")
                    End If
                    If Not dv.Table.Rows(0)("corporate_name") Is System.DBNull.Value Then
                        Me.CorporateNameTD.InnerText = dv.Table.Rows(0)("corporate_name")
                    End If
                End If
            End If
        End Sub
        Public Sub PopulateCustomerBankInfo()
            If Not Me.State.oCert Is Nothing Then
                Dim dv As DataView = Me.State.oCert.GetCustomerCurrentBankInfo(Me.State.oCert.Id)

                If Not dv.Table Is Nothing AndAlso dv.Table.Rows.Count > 0 Then

                    If Not dv.Table.Rows(0)("account_name") Is System.DBNull.Value Then
                        Me.NameOnAccountTD.InnerText = dv.Table.Rows(0)("account_name")
                    End If
                    If Not dv.Table.Rows(0)("bank_name") Is System.DBNull.Value Then
                        Me.BankNameTD.InnerText = dv.Table.Rows(0)("bank_name")
                    End If
                    If Not dv.Table.Rows(0)("bank_id") Is System.DBNull.Value Then
                        Me.BankIDTD.InnerText = dv.Table.Rows(0)("bank_id")
                    End If
                    If Not dv.Table.Rows(0)("account_type") Is System.DBNull.Value Then
                        Me.AccountTypeTD.InnerText = dv.Table.Rows(0)("account_type")
                    End If
                    If Not dv.Table.Rows(0)("bank_lookup_code") Is System.DBNull.Value Then
                        Me.BankLookupCodeTD.InnerText = dv.Table.Rows(0)("bank_lookup_code")
                    End If
                    If Not dv.Table.Rows(0)("bank_sort_code") Is System.DBNull.Value Then
                        Me.BankSortCodeTD.InnerText = dv.Table.Rows(0)("bank_sort_code")
                    End If
                    If Not dv.Table.Rows(0)("branch_name") Is System.DBNull.Value Then
                        Me.BranchNameTD.InnerText = dv.Table.Rows(0)("branch_name")
                    End If
                    If Not dv.Table.Rows(0)("bank_sub_code") Is System.DBNull.Value Then
                        Me.BankSubCodeTD.InnerText = dv.Table.Rows(0)("bank_sub_code")
                    End If
                    If Not dv.Table.Rows(0)("account_number") Is System.DBNull.Value Then
                        Me.AccountNumberTD.InnerText = dv.Table.Rows(0)("account_number")
                    End If
                    If Not dv.Table.Rows(0)("iban_number") Is System.DBNull.Value Then
                        Me.IBANNumberTD.InnerText = dv.Table.Rows(0)("iban_number")
                    End If

                End If
            End If
        End Sub

        Public Sub PopulateCustPersonalHistory()
            Me.State.CustPersonalHistDV = Me.State.oCert.GetCustPersonalHistory(Me.State.oCert.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Me.CustPersonalHistory.AutoGenerateColumns = False
            Me.CustPersonalHistory.DataSource = Me.State.CustPersonalHistDV
            Me.CustPersonalHistory.DataBind()
        End Sub
        Public Sub PopulateCustAddressHistory()
            Me.State.CustAddressHistDV = Me.State.oCert.GetCustAddressHistory(Me.State.oCert.Id)
            Me.CustAddressHistory.AutoGenerateColumns = False
            Me.CustAddressHistory.DataSource = Me.State.CustAddressHistDV
            Me.CustAddressHistory.DataBind()
        End Sub
        Public Sub PopulateCustContactHistory()
            Me.State.CustContactHistDV = Me.State.oCert.GetCustContactHistory(Me.State.oCert.Id)
            Me.CustContactHistory.AutoGenerateColumns = False
            Me.CustContactHistory.DataSource = Me.State.CustContactHistDV
            Me.CustContactHistory.DataBind()
        End Sub
        Public Sub PopulateCustBankDetailHistory()
            Me.State.CustBankDetailHistDV = Me.State.oCert.GetCustBankDetailHistory(Me.State.oCert.Id)
            Me.CustBankDetailHistory.AutoGenerateColumns = False
            Me.CustBankDetailHistory.DataSource = Me.State.CustBankDetailHistDV
            Me.CustBankDetailHistory.DataBind()
        End Sub
        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                Me.ReturnToCallingPage()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
    End Class
End Namespace
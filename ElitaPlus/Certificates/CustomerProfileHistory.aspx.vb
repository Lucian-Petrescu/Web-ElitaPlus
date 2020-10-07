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
            If (State IsNot Nothing) Then
                If (State.oCert IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("Customer_Profile_History")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Customer_Profile_History")
                End If
            End If
        End Sub
#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
            UpdateBreadCrum()
            Try
                If Not IsPostBack Then
                    PopulateCustomerProfileInfo()
                    TranslateGridHeader(CustPersonalHistory)
                    PopulateCustPersonalHistory()
                    TranslateGridHeader(CustAddressHistory)
                    PopulateCustAddressHistory()
                    TranslateGridHeader(CustContactHistory)
                    PopulateCustContactHistory()
                    TranslateGridHeader(CustBankDetailHistory)
                    PopulateCustBankDetailHistory()
                    MasterPage.MessageController.Clear()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Private Sub CustomerProfileHistory_PageCall(CallFromUrl As String, CallingPar As Object) Handles Me.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.oCert = New Certificate(CType(CallingParameters, Guid))
                    State.oDealer = New Dealer(State.oCert.DealerId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster, False)
            End Try
        End Sub
#End Region
        Public Sub PopulateCustomerProfileInfo()
            If State.oCert IsNot Nothing Then
                Dim dv As DataView = State.oCert.GetOtherCustomerDetails(State.oCert.CustomerId, ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.oDealer.IdentificationNumberType)

                If dv.Table IsNot Nothing AndAlso dv.Table.Rows.Count > 0 Then


                    CustomerNameTD.InnerText = dv.Table.Rows(0)("salutation") & " " & dv.Table.Rows(0)("Customer_first_name") & " " & dv.Table.Rows(0)("customer_middle_name") & " " & dv.Table.Rows(0)("customer_last_name")
                    If dv.Table.Rows(0)("identification_number") IsNot System.DBNull.Value Then
                        IdentificationNumberTD.InnerText = dv.Table.Rows(0)("identification_number")
                    End If
                    If dv.Table.Rows(0)("gender") IsNot System.DBNull.Value Then
                        GenderTD.InnerText = dv.Table.Rows(0)("gender")
                    End If
                    If dv.Table.Rows(0)("marital_status") IsNot System.DBNull.Value Then
                        MaritalStatusTD.InnerText = dv.Table.Rows(0)("marital_status")
                    End If
                    If dv.Table.Rows(0)("place_of_birth") IsNot System.DBNull.Value Then
                        PlaceOfBirthTD.InnerText = dv.Table.Rows(0)("place_of_birth")
                    End If
                    If dv.Table.Rows(0)("date_of_birth") IsNot System.DBNull.Value Then
                        DateOfBirthTD.InnerText = dv.Table.Rows(0)("date_of_birth")
                    End If
                    If dv.Table.Rows(0)("nationality") IsNot System.DBNull.Value Then
                        NationalityTD.InnerText = dv.Table.Rows(0)("nationality")
                    End If
                    If dv.Table.Rows(0)("email") IsNot System.DBNull.Value Then
                        EmailTD.InnerText = dv.Table.Rows(0)("email")
                    End If
                    If dv.Table.Rows(0)("home_phone") IsNot System.DBNull.Value Then
                        HomePhoneTD.InnerText = dv.Table.Rows(0)("home_phone")
                    End If
                    If dv.Table.Rows(0)("work_phone") IsNot System.DBNull.Value Then
                        WorkPhoneTD.InnerText = dv.Table.Rows(0)("work_phone")
                    End If
                    If dv.Table.Rows(0)("address1") IsNot System.DBNull.Value Or dv.Table.Rows(0)("address2") IsNot System.DBNull.Value Or dv.Table.Rows(0)("address3") IsNot System.DBNull.Value Then
                        AddressTD.InnerText = dv.Table.Rows(0)("address1") & " " & dv.Table.Rows(0)("address2") & " " & dv.Table.Rows(0)("address3")
                    End If
                    If dv.Table.Rows(0)("city") IsNot System.DBNull.Value Then
                        CityTD.InnerText = dv.Table.Rows(0)("city")
                    End If
                    If dv.Table.Rows(0)("state") IsNot System.DBNull.Value Then
                        StateTD.InnerText = dv.Table.Rows(0)("state")
                    End If
                    If dv.Table.Rows(0)("postal_code") IsNot System.DBNull.Value Then
                        PostalCodeTD.InnerText = dv.Table.Rows(0)("postal_code")
                    End If
                    If dv.Table.Rows(0)("country") IsNot System.DBNull.Value Then
                        CountryCodeTD.InnerText = dv.Table.Rows(0)("country")
                    End If
                    If dv.Table.Rows(0)("corporate_name") IsNot System.DBNull.Value Then
                        CorporateNameTD.InnerText = dv.Table.Rows(0)("corporate_name")
                    End If
                End If
            End If
        End Sub

        Public Sub PopulateCustPersonalHistory()
            State.CustPersonalHistDV = State.oCert.GetCustPersonalHistory(State.oCert.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            CustPersonalHistory.AutoGenerateColumns = False
            CustPersonalHistory.DataSource = State.CustPersonalHistDV
            CustPersonalHistory.DataBind()
        End Sub
        Public Sub PopulateCustAddressHistory()
            State.CustAddressHistDV = State.oCert.GetCustAddressHistory(State.oCert.Id)
            CustAddressHistory.AutoGenerateColumns = False
            CustAddressHistory.DataSource = State.CustAddressHistDV
            CustAddressHistory.DataBind()
        End Sub
        Public Sub PopulateCustContactHistory()
            State.CustContactHistDV = State.oCert.GetCustContactHistory(State.oCert.Id)
            CustContactHistory.AutoGenerateColumns = False
            CustContactHistory.DataSource = State.CustContactHistDV
            CustContactHistory.DataBind()
        End Sub
        Public Sub PopulateCustBankDetailHistory()
            State.CustBankDetailHistDV = State.oCert.GetCustBankDetailHistory(State.oCert.Id)
            CustBankDetailHistory.AutoGenerateColumns = False
            CustBankDetailHistory.DataSource = State.CustBankDetailHistDV
            CustBankDetailHistory.DataBind()
        End Sub
        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                ReturnToCallingPage()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
    End Class
End Namespace
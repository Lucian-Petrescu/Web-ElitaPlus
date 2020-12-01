Imports System.Collections.Generic
Imports Assurant.Elita.ClientIntegration

Public Class CommonBankInfo
    Public Shared Sub BankInfoEndorseRequest(ByVal dealerCode As String, ByVal certificateNumber As String, ByVal bankinfo As BankInfo)
        Dim endorseRequest As PolicyService.EndorsePolicyRequest = New PolicyService.EndorsePolicyRequest
        Dim updateBankInfo As PolicyService.UpdateBankInfo = New PolicyService.UpdateBankInfo

        updateBankInfo.EndorsementReason = PolicyService.EndorsementPolicyReasons.BankFulfillment
        updateBankInfo.AccountOwnerName = bankinfo.Account_Name
        updateBankInfo.AccountNumber = bankinfo.Account_Number
        updateBankInfo.RoutingNumber = bankinfo.Bank_Id
        updateBankInfo.AccountOwnerName = bankinfo.Account_Name
        updateBankInfo.BankSortCode = bankinfo.BankSortCode
        updateBankInfo.SwiftCode = bankinfo.SwiftCode
        updateBankInfo.BankLookupCode = bankinfo.BankLookupCode
        updateBankInfo.BankName = bankinfo.BankName
        If Not bankinfo.BranchNumber Is Nothing Then
            updateBankInfo.BranchNumber = bankinfo.BranchNumber
        End If
        updateBankInfo.IbanCode = bankinfo.IbanNumber

        endorseRequest.DealerCode = dealerCode
        endorseRequest.CertificateNumber = certificateNumber

        endorseRequest.Requests = New PolicyService.BasePolicyEndorseAction() {updateBankInfo}

        Try
            WcfClientHelper.Execute(Of PolicyService.PolicyServiceClient, PolicyService.IPolicyService, PolicyService.EndorseResponse)(
                                                                        GetClient(),
                                                                        New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                         Function(ByVal c As PolicyService.PolicyServiceClient)
                                                                             Return c.Endorse(endorseRequest)
                                                                         End Function)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Shared Function GetClient() As PolicyService.PolicyServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__SVC_CERT_ENROLL), False)
        Dim client = New PolicyService.PolicyServiceClient("CustomBinding_IPolicyService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
End Class

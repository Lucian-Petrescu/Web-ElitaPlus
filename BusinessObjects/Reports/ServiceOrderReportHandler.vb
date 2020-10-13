

Public Class ServiceOrderReportHandler
#Region "Constructors"

    'New BO attaching to a BO family
    Public Sub New(oClaim As ClaimBase, Optional claimAuthId As Guid = Nothing)
        soDS = New ServiceOrderReport
        ClaimBO = oClaim
        ClaimAuthorizationId = claimAuthId
        Load()
    End Sub

    Protected Sub Load()

        Dim ClaimAuthorizationBO As ClaimAuthorization
        Dim soRow As ServiceOrderReport.ServiceOrderRow = SODataSet.ServiceOrder.NewServiceOrderRow()

        soRow.COMPANY_ID = ClaimBO.CompanyId.ToByteArray()
        soRow.CLAIM_ID = ClaimBO.Id.ToByteArray()
        soRow.SERVICE_CENTER_ID = ClaimBO.ServiceCenterId.ToByteArray()
        soRow.LOSS_DATE = ClaimBO.LossDate.Value

        If Not (ClaimAuthorizationId.Equals(Guid.Empty)) Then
            ClaimAuthorizationBO = New ClaimAuthorization(ClaimAuthorizationId)
            soRow.AUTHORIZATION_NUMBER = ClaimAuthorizationBO.AuthorizationNumber
        Else
            soRow.AUTHORIZATION_NUMBER = ClaimBO.AuthorizationNumber
        End If

        soRow.EXTENDED_CLAIM_STATUS = New ClaimDAL().GetLatestExtendedClaimStatus(cliamId:=ClaimBO.Id, languageId:=ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        Dim svc As ServiceCenter

        If (ClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) AndAlso Not (ClaimAuthorizationId.Equals(Guid.Empty)) Then
            svc = New ServiceCenter(ClaimAuthorizationBO.ServiceCenterId)
            soRow.SPECIAL_INSTRUCTION = ClaimAuthorizationBO.SpecialInstruction
            soRow.AUTHORIZATION_AMOUNT = ClaimAuthorizationBO.AuthorizationAmount.Value
        Else
            svc = New ServiceCenter(ClaimBO.ServiceCenterId)
            soRow.SPECIAL_INSTRUCTION = ClaimBO.SpecialInstruction
            soRow.AUTHORIZATION_AMOUNT = ClaimBO.AuthorizedAmount.Value
        End If

        soRow.SVC_CODE = svc.Code
        soRow.SVC_ADDRESS1 = svc.Address.Address1
        soRow.SVC_ADDRESS2 = svc.Address.Address2
        soRow.SVC_CITY = svc.Address.City
        soRow.SVC_EMAIL = svc.Email
        soRow.SVC_FAX = svc.Fax
        soRow.SVC_NAME = svc.Description
        soRow.SVC_PHONE = svc.Phone1
        soRow.SVC_CONTACT = svc.ContactName

        If Not svc.Address.RegionId.Equals(Guid.Empty) Then
            soRow.SVC_STATE_PROVINCE = New Region(svc.Address.RegionId).Description
        End If

        soRow.SVC_ZIP = svc.Address.PostalCode
        soRow.SVC_ADDR_MAILING_LABEL = svc.Address.MailingAddressLabel

        If Not (ClaimBO.LoanerCenterId.Equals(Guid.Empty)) Then
            Dim lc As ServiceCenter = New ServiceCenter(ClaimBO.LoanerCenterId)
            soRow.LC_CODE = lc.Code
            soRow.LC_ADDRESS1 = lc.Address.Address1
            soRow.LC_ADDRESS2 = lc.Address.Address2
            soRow.LC_CITY = lc.Address.City
            soRow.LC_EMAIL = lc.Email
            soRow.LC_FAX = lc.Fax
            soRow.LC_NAME = lc.Description
            soRow.LC_PHONE = lc.Phone1
            soRow.LC_STATE_PROVINCE = New Region(lc.Address.RegionId).Description
            soRow.LC_ZIP = lc.Address.PostalCode
            soRow.LC_ADDR_MAILING_LABEL = lc.Address.MailingAddressLabel
        End If

        soRow.REPAIR_METHOD = ClaimBO.MethodOfRepairDescription
        soRow.DATE_CREATED = System.DateTime.Now

        Dim cert As Certificate = New Certificate(ClaimBO.CertificateId)
        soRow.CERTIFICATE = cert.CertNumber
        soRow.IDENTIFICATION_NUMBER = cert.IdentificationNumber
        soRow.PROBLEM_DESCRIPTION = ClaimBO.ProblemDescription
        soRow.CUSTOMER_NAME = cert.CustomerName
        soRow.ADDRESS1 = cert.AddressChild.Address1
        soRow.ADDRESS2 = cert.AddressChild.Address2
        soRow.ADDRESS3 = cert.AddressChild.Address3
        soRow.CITY = cert.AddressChild.City
        soRow.ADDR_MAILING_LABEL = cert.AddressChild.MailingAddressLabel

        If Not cert.AddressChild.RegionId.Equals(Guid.Empty) Then
            soRow.STATE_PROVINCE = New Region(cert.AddressChild.RegionId).Description
            soRow.STATE_PROVINCE_CODE = New Region(cert.AddressChild.RegionId).ShortDesc
        End If

        soRow.ZIP = cert.AddressChild.PostalCode
        soRow.HOME_PHONE = cert.HomePhone
        soRow.RETAILER = cert.Retailer
        soRow.INVOICE_NUMBER = cert.InvoiceNumber

        'Reqs-784
        If Not ClaimBO.ContactInfoId.Equals(Guid.Empty) Then
            Dim ContactInfo As ContactInfo = New ContactInfo(ClaimBO.ContactInfoId)
            If Not ContactInfo.SalutationId.Equals(Guid.Empty) Then
                Dim dv As DataView = LookupListNew.GetSalutationLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                soRow.SHIPPING_SALUTATIONID = LookupListNew.GetDescriptionFromId(dv, ContactInfo.SalutationId)
            End If
            soRow.SHIPPING_NAME = ContactInfo.Name
            soRow.SHIPPING_HOMEPHONE = ContactInfo.HomePhone
            soRow.SHIPPING_WORKPHONE = ContactInfo.WorkPhone
            soRow.SHIPPING_CELLPHONE = ContactInfo.CellPhone
            soRow.SHIPPING_EMAIL = ContactInfo.Email

            If ContactInfo IsNot Nothing Then
                Dim Address As Address = New Address(ContactInfo.AddressId)
                soRow.SHIPPING_ADDRESS1 = Address.Address1
                soRow.SHIPPING_ADDRESS2 = Address.Address2
                soRow.SHIPPING_ADDRESS3 = Address.Address3
                soRow.SHIPPING_POSTALCODE = Address.PostalCode
                If Not Address.RegionId.Equals(Guid.Empty) Then
                    soRow.SHIPPING_REGION = New Region(Address.RegionId).Description
                End If
                soRow.SHIPPING_CITY = Address.City
                If Not Address.CountryId.Equals(Guid.Empty) Then
                    soRow.SHIPPING_COUNTRY = New Country(Address.CountryId).Description
                End If
                soRow.ADDRESS1 = Address.Address1
                soRow.ADDRESS2 = Address.Address2
                soRow.ADDRESS3 = Address.Address3
                soRow.CITY = Address.City
                soRow.ZIP = Address.PostalCode
                If Not Address.RegionId.Equals(Guid.Empty) Then
                    soRow.STATE_PROVINCE = New Region(Address.RegionId).Description
                    soRow.STATE_PROVINCE_CODE = New Region(Address.RegionId).ShortDesc
                End If

            End If
            End If
        ' EndReqs-784

        Dim certCov As CertItemCoverage = New CertItemCoverage(ClaimBO.CertItemCoverageId)
        Dim oCertItem As CertItem = New CertItem(certCov.CertItemId)
        Dim certRegItemId As Guid = ClaimBO.GetCertRegisterItemIdByMasterNumber(ClaimBO.MasterClaimNumber, ClaimBO.CompanyId)
        Dim claimInvoice As ClaimInvoice = New ClaimInvoice()
        Dim taxRateData As ClaimInvoiceDAL.TaxRateData = New ClaimInvoiceDAL.TaxRateData()
        Dim claimTaxRateData As ClaimInvoiceDAL.TaxRateData = New ClaimInvoiceDAL.TaxRateData()
        Dim oCompany As Company = New Company(ClaimBO.CompanyId)
        Dim oDealer As Dealer = New Dealer(cert.DealerId)

        taxRateData.countryID = oCompany.CountryId
        taxRateData.regionID = svc.Address.RegionId
        taxRateData.dealerID = oDealer.Id
        taxRateData.salesDate = cert.ProductSalesDate

        taxRateData.taxtypeID = LookupListNew.GetIdFromCode(LookupListCache.LK_TAX_TYPES, "7")

        If oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y) Then
            claimTaxRateData = claimInvoice.GetTaxRate(taxRateData)
        Else
            claimTaxRateData.taxRate = 0
        End If


        Dim CertificateRegisteredItem As CertRegisteredItem
        If Not certRegItemId.Equals(Guid.Empty) Then
            CertificateRegisteredItem = New CertRegisteredItem(certRegItemId)
        End If

        Dim ManufacturerDescription As String = String.Empty
        If Not oCertItem.ManufacturerId.Equals(Guid.Empty) Then
            Dim oManufacturer As Manufacturer = New Manufacturer(oCertItem.ManufacturerId)
            ManufacturerDescription = oManufacturer.Description
            soRow.PRODUCT_DESCRIPTION = oCertItem.ItemDescription
            soRow.MODEL = oCertItem.Model
            soRow.SERIAL_NUMBER = oCertItem.SerialNumber
        ElseIf Not certRegItemId.Equals(Guid.Empty) Then
            If Not CertificateRegisteredItem.ManufacturerId.Equals(Guid.Empty) Then
                Dim oManufacturer As Manufacturer = New Manufacturer(CertificateRegisteredItem.ManufacturerId)
                ManufacturerDescription = oManufacturer.Description
                soRow.PRODUCT_DESCRIPTION = CertificateRegisteredItem.ItemDescription
                soRow.MODEL = CertificateRegisteredItem.Model
                soRow.SERIAL_NUMBER = CertificateRegisteredItem.SerialNumber
            End If
        Else
            ManufacturerDescription = String.Empty
            soRow.PRODUCT_DESCRIPTION = String.Empty
            soRow.MODEL = String.Empty
            soRow.SERIAL_NUMBER = String.Empty
        End If

        soRow.MANUFACTURER = ManufacturerDescription
        soRow.CAMPAIGN_NUMBER = cert.CampaignNumber
        soRow.COVERAGE_TYPE = ClaimBO.CoverageTypeDescription
        soRow.IMEI = oCertItem.IMEINumber
        soRow.PRODUCT_SALES_DATE = cert.ProductSalesDate.Value
        soRow.DEALER_NAME = oDealer.DealerName
        soRow.DEALER_CODE = oDealer.Dealer
        soRow.NAME_OF_CONTACT = ClaimBO.ContactName
        soRow.DEDUCTIBLE_AMOUNT = ClaimBO.Deductible.Value

        If claimTaxRateData.taxRate = 0 Then
            soRow.TAX_AMOUNT = 0
        Else
            soRow.TAX_AMOUNT = ClaimBO.AuthorizedAmount.Value - (ClaimBO.AuthorizedAmount.Value * 100 / (100 + claimTaxRateData.taxRate))
        End If

        soRow.AUTHORIZED_BY = ClaimBO.ClaimsAdjusterName
        soRow.CLAIM_NUMBER = ClaimBO.ClaimNumber
        soRow.MASTER_CLAIM_NUMBER = ClaimBO.MasterClaimNumber
        soRow.DEDUCTIBLE_AMT_DISCLAIMER_ON = "N"

        ' Exception by Company
        Dim compCode As String = oCompany.Code
        Dim compDesc As String = oCompany.Description

        Dim dealerCode As String = ClaimBO.DealerCode

        Select Case compCode
            Case Codes.COMPANY__TBR
                ' Exception by Dealer of a Company
                Select Case dealerCode
                    Case Codes.DEALER__DUDR
                        compDesc = "Assurant Services Brasil Ltda"
                End Select
            Case Codes.COMPANY__APR
                If dealerCode = Codes.DEALER__TMOBIL OrElse dealerCode = Codes.DEALER__CLARO Then
                    soRow.DEDUCTIBLE_AMT_DISCLAIMER_ON = "Y"
                End If
            Case Codes.COMPANY__PRC
                If dealerCode = Codes.DEALER__CLAC Then
                    soRow.DEDUCTIBLE_AMT_DISCLAIMER_ON = "Y"
                End If
        End Select

        soRow.COMPANY_NAME = compDesc
        soRow.COMPANY_PHONE = oCompany.Phone
        soRow.COMPANY_FAX = oCompany.Fax
        soRow.COMPANY_EMAIL = oCompany.Email
        soRow.COMPANY_CODE = oCompany.Code
        soRow.COMPANY_ADDRESS1 = oCompany.Address1
        soRow.COMPANY_ADDRESS2 = oCompany.Address2
        soRow.COMPANY_CITY = oCompany.City
        soRow.COMPANY_ZIP = oCompany.PostalCode

        If Not oCompany.RegionId.Equals(Guid.Empty) Then
            soRow.COMPANY_STATE = New Region(oCompany.RegionId).Description
        End If

        soRow.COMPANY_ADDR_MAILING_LABEL = "Not in used. Please contact the development department."
        soRow.WARRANTY_SALES_DATE = cert.WarrantySalesDate.Value
        soRow.LIABILITY_LIMIT = ClaimBO.LiabilityLimit.Value
        soRow.CLAIM_REASON_CLOSED = ClaimBO.ReasonClosed
        soRow.CLAIM_ACTIVITY = ClaimBO.ClaimActivityCode

        If ClaimBO.MethodOfRepairCode = "R" Then
            soRow.RPC_CODE = svc.Code
            soRow.RPC_ADDRESS1 = svc.Address.Address1
            soRow.RPC_ADDRESS2 = svc.Address.Address2
            soRow.RPC_CITY = svc.Address.City
            soRow.RPC_EMAIL = svc.Email
            soRow.RPC_FAX = svc.Fax
            soRow.RPC_NAME = svc.Description
            soRow.RPC_PHONE = svc.Phone1

            If Not svc.Address.RegionId.Equals(Guid.Empty) Then
                soRow.RPC_STATE_PROVINCE = New Region(svc.Address.RegionId).Description
            End If

            soRow.RPC_ZIP = svc.Address.PostalCode
            soRow.RPC_ADDR_MAILING_LABEL = svc.Address.MailingAddressLabel

            Dim oldClaimNumber = ClaimBO.ClaimNumber.Remove(ClaimBO.ClaimNumber.Length - 1, 1)
            Dim claimDV As Claim.ClaimSearchDV = Claim.getList(oldClaimNumber, "", Nothing, "", Nothing)

            If claimDV.Table.Rows.Count = 1 Then
                Dim oldSeviceCenterID As Guid = ClaimBO.ServiceCenterId
                Dim oldSvcCenter As ServiceCenter = New ServiceCenter(oldSeviceCenterID)

                soRow.SVC_CODE = oldSvcCenter.Code
                soRow.SVC_ADDRESS1 = oldSvcCenter.Address.Address1
                soRow.SVC_ADDRESS2 = oldSvcCenter.Address.Address2
                soRow.SVC_CITY = oldSvcCenter.Address.City
                soRow.SVC_EMAIL = oldSvcCenter.Email
                soRow.SVC_FAX = oldSvcCenter.Fax
                soRow.SVC_NAME = oldSvcCenter.Description
                soRow.SVC_PHONE = oldSvcCenter.Phone1

                If Not oldSvcCenter.Address.RegionId.Equals(Guid.Empty) Then
                    soRow.SVC_STATE_PROVINCE = New Region(oldSvcCenter.Address.RegionId).Description
                End If

                soRow.SVC_ZIP = oldSvcCenter.Address.PostalCode
                soRow.SVC_ADDR_MAILING_LABEL = oldSvcCenter.Address.MailingAddressLabel

            End If

        End If

        If ClaimBO.ReasonClosed = "TBRP" OrElse ClaimBO.ClaimActivityCode = "TBREP" Then
            Dim claimNumber As String = ClaimBO.ClaimNumber
            Dim replacementClaimNumber As String = ""
            If claimNumber.Length = 9 Then
                If claimNumber.EndsWith("S") Then
                    replacementClaimNumber = claimNumber.Replace("S", "R")
                End If
            Else
                Do While (Not IsNumeric(claimNumber.Substring(claimNumber.Length - 1))) AndAlso (claimNumber.Length > 1)
                    claimNumber = claimNumber.Substring(0, claimNumber.Length - 2)
                Loop
                replacementClaimNumber = claimNumber & "R"
            End If

            Dim claimDV As Claim.ClaimSearchDV = Claim.getList(replacementClaimNumber, "", svc.Id, "", Nothing)
            If claimDV.Table.Rows.Count = 1 Then
                Dim claimID As Guid = New Guid(CType(claimDV.Table.Rows(0)(Claim.ClaimSearchDV.COL_CLAIM_ID), Byte()))
                Dim rpcClaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(claimID)
                soRow.PROBLEM_DESCRIPTION = rpcClaim.ProblemDescription
            End If
        End If

        soRow.ASSURANT_AMOUNT = ClaimBO.AssurantPays.Value
        soRow.CONSUMER_AMOUNT = ClaimBO.ConsumerPays.Value

        'Add columns for WR @ 784391
        soRow.MOBILE_PHONE = cert.WorkPhone
        soRow.CAUSE_OF_LOSS = ClaimBO.CauseOfLoss
        soRow.CUSTOMER_EMAIL = cert.Email
        soRow.SALES_PRICE = cert.SalesPrice.Value
        soRow.TOTAL_PAID = ClaimBO.TotalPaidForCert.Value
        soRow.TAX_ID = oCompany.TaxIdNumber
        soRow.WARRANTY_END_DATE = certCov.EndDate.Value

        Dim yesValueId As Guid = LookupListNew.GetIdFromCode("YES_NO", "Y")

        soRow.IMAGE_PATH = ElitaPlusParameters.CurrentParameters.ServiceOrderImageHostName

        If oCompany.ServiceOrdersByDealerId.Equals(yesValueId) AndAlso oDealer.SvcOrdersAddress IsNot Nothing Then
            Try
                If oDealer.SvcOrdersAddress.Address IsNot Nothing Then
                    soRow.RPC_ADDRESS1 = oDealer.SvcOrdersAddress.Address.Address1
                    soRow.RPC_ADDRESS2 = oDealer.SvcOrdersAddress.Address.Address2
                    soRow.RPC_CITY = oDealer.SvcOrdersAddress.Address.City
                    soRow.RPC_NAME = oDealer.SvcOrdersAddress.Name
                    soRow.RPC_ZIP = oDealer.SvcOrdersAddress.Address.PostalCode
                    soRow.TAX_ID = oDealer.SvcOrdersAddress.TaxIdNumber

                    If Not oDealer.SvcOrdersAddress.Address.RegionId.Equals(Guid.Empty) Then
                        soRow.RPC_STATE_PROVINCE = New Region(oDealer.SvcOrdersAddress.Address.RegionId).Description
                    End If
                End If
            Catch ex As DataNotFoundException
                Throw New StoredProcedureGeneratedException("ServiceOrderReportHandler", Common.ErrorCodes.NO_SVC_ORDER_ADDRESS_FOR_THIS_DEALER_ERR)
            End Try
        End If

        soRow.RISK_TYPE = ClaimBO.RiskType

        SODataSet.ServiceOrder.AddServiceOrderRow(soRow)

    End Sub

#End Region
#Region "Members"
    'Reqs-784
    Private soContact As ContactInfo
    Private soAddress As Address

    Private soDS As ServiceOrderReport
#End Region

#Region "Properties"

    Private Property ClaimBO As ClaimBase
    Public ClaimAuthorizationId As Guid
    'Reqs-784
    Private Property ContactInfoBO As ContactInfo
        Get
            Return soContact
        End Get
        Set
            soContact = Value
        End Set
    End Property
    'Reqs-784
    Private Property AddressBO As Address
        Get
            Return soAddress
        End Get
        Set
            soAddress = Value
        End Set
    End Property
    Public ReadOnly Property SODataSet As ServiceOrderReport
        Get
            Return soDS
        End Get
    End Property


#End Region


End Class

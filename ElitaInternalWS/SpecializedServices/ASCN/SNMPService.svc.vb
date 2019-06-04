Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.Common.ErrorCodes
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.Fulfillment.BusinessRulesEngineInterface


Namespace SpecializedServices.Ascn
    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecialzedServices/Ascn")>
    Public Class SNMPService
        Implements ISNMPService

#Region "Member Variables"
        Private _country_id As Guid = Guid.Empty
        Private _company_id As Guid = Guid.Empty
        Private _Risk_Group_id As Guid = Guid.Empty
        Private _claimId As Guid = Guid.Empty
        Private _claimBo As Claim = Nothing
        Private _PartsInfoDV As DataView = Nothing
        Private _PartsAmount As Decimal = 0

        Private IsVisitDateNull As Boolean = True
        Private IsRepairDateNull As Boolean = True
        Private IsShippingAmountNull As Boolean = True
        Private IsLaborAmountNull As Boolean = True
        Private IsServiceChargeAmountNull As Boolean = True
        Private IsTripAmountNull As Boolean = True
        Private IsOtherAmountNull As Boolean = True
        Private IsOtherDescriptionNull As Boolean = True
        Private IsAuthorizationNumberNull As Boolean = True
        Private PartsDataHasChanged As Boolean = False





#End Region

        Private _fulfilmentRuleProvider As IFulfillmentRulesClientProvider
        Public Sub New(ByVal pfulfilmentRuleProvider As IFulfillmentRulesClientProvider)
            _fulfilmentRuleProvider = pfulfilmentRuleProvider
        End Sub

#Region "Extended Properties"
        Private ReadOnly Property CountryId() As Guid
            Get
                Return _country_id
            End Get
        End Property

        Public Property CompanyId() As Guid
            Get
                Return _company_id
            End Get
            Set(ByVal Value As Guid)
                _company_id = Value
            End Set
        End Property

        Private Property ClaimBO() As Claim
            Get
                Return Me._claimBo
            End Get
            Set(ByVal value As Claim)
                Me._claimBo = value
            End Set
        End Property


        Private ReadOnly Property RiskGroupId() As Guid
            Get
                Return _Risk_Group_id
            End Get
        End Property

        Private ReadOnly Property ClaimId() As Guid
            Get
                Return _claimId
            End Get
        End Property

#End Region

#Region "Member Methods"
        Public Function GetServiceCenterClaims(ByVal request As GetServiceCenterClaimsRequest) As GetServiceCenterClaimsResponse Implements ISNMPService.GetServiceCenterClaims
            request.Validate("request").HandleFault()

            Dim response As New GetServiceCenterClaimsResponse
            Dim dsClaims As DataSet
            Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim oScClaimsData As New ClaimDAL.ServiceCenterClaimsSearchData
            Dim i As Integer = Nothing

            Try
                For i = 0 To objCompaniesAL.Count - 1
                    Dim objCompany As New Company(CType(objCompaniesAL.Item(i), Guid))
                    If Not objCompany Is Nothing AndAlso objCompany.Code.Equals(request.CompanyCode.ToUpper) Then
                        CompanyId = objCompany.Id
                    End If
                Next
                If CompanyId.Equals(Guid.Empty) Then
                    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.CompanyCode)
                End If
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.CompanyCode)
            End Try
            Try
                If Not request.ServiceCenterCode = Nothing Then
                    Dim oServiceCenter As New ServiceCenter(request.ServiceCenterCode)
                    If oServiceCenter Is Nothing Then
                        Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_SERVICE_CENTER_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    End If
                    oScClaimsData.ServiceCenterId = oServiceCenter.Id
                End If
            Catch ex As Exception
                Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_SERVICE_CENTER_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            If Not request.ClaimStatus = Nothing Then
                oScClaimsData.ClaimStatus = request.ClaimStatus.ToUpper
            End If

            If Not request.ClaimType = Nothing Then
                Dim _claimTypeIds As New ArrayList
                Dim oList As String() = request.ClaimType.Split(New Char() {"|"c})
                Dim CLT As String
                For Each CLT In oList
                    Dim oClaimTypeID As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_TYPES, CLT)
                    If oClaimTypeID.Equals(Guid.Empty) Then
                        Throw New FaultException(Of CoverageNotFoundFault)(New CoverageNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COVERAGE_NOT_FOUND,
                                                                                                              ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimType)
                    Else
                        _claimTypeIds.Add(oClaimTypeID)
                    End If
                Next
                oScClaimsData.ClaimTypeIds = _claimTypeIds
            End If

            If Not request.MethodofRepair = Nothing Then
                Dim _methodOfRprIds As New ArrayList
                Dim oList As String() = request.MethodofRepair.Split(New Char() {"|"c})
                Dim MOR As String
                For Each MOR In oList
                    Dim oMethodOfRprId As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, MOR)
                    If oMethodOfRprId.Equals(Guid.Empty) Then
                        Throw New FaultException(Of MethodOfRepairNotFoundFault)(New MethodOfRepairNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_METHOD_OF_REPAIR, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    Else
                        _methodOfRprIds.Add(oMethodOfRprId)
                    End If
                Next
                oScClaimsData.MethodOfRepairIds = _methodOfRprIds
            End If


            If Not request.ClaimExStusCode = Nothing Then
                Dim _claimExtendedStatusIds As New ArrayList
                Dim dv As DataView = LookupListNew.GetExtendedStatusLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Dim oClaimExtendedStatusIds As Guid = LookupListNew.GetIdFromCode(dv, request.ClaimExStusCode)
                If Not oClaimExtendedStatusIds.Equals(Guid.Empty) Then
                    _claimExtendedStatusIds.Add(oClaimExtendedStatusIds)
                    oScClaimsData.ClaimExtendedStatusIds = _claimExtendedStatusIds
                End If
            Else
                oScClaimsData.ClaimExtendedStatusIds = Nothing
            End If

            If Not request.ClaimExStusOwnCode = Nothing Then
                Dim _claimExtendedStatusOwnerIds As New ArrayList
                Dim oList As String() = request.ClaimExStusOwnCode.Split(New Char() {"|"c})
                Dim COD As String
                For Each COD In oList
                    Dim oClaimExtendedStatusOwnerIds As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_EXTENDED_CLAIM_STATUS_OWNER, COD)
                    If Not oClaimExtendedStatusOwnerIds.Equals(Guid.Empty) Then
                        _claimExtendedStatusOwnerIds.Add(oClaimExtendedStatusOwnerIds)
                        oScClaimsData.ClaimExtendedStatusOwnerCodeIds = _claimExtendedStatusOwnerIds
                    End If
                Next
            Else
                oScClaimsData.ClaimExtendedStatusOwnerCodeIds = Nothing
            End If

            oScClaimsData.SortBy = request.SortBy
            oScClaimsData.SortOrder = request.SortOrder
            oScClaimsData.PageSize = request.PageSize
            oScClaimsData.PageNumber = request.PageNumber
            oScClaimsData.ClaimNumber = request.ClaimNumber
            oScClaimsData.CertificateNumber = request.CertificateNumber

            If Not request.AuthorizationNumber = Nothing Then
                oScClaimsData.AuthorizationNumber = request.AuthorizationNumber
            Else
                oScClaimsData.AuthorizationNumber = Nothing
            End If

            If Not request.CustomerName = Nothing Then
                oScClaimsData.CustomerName = request.CustomerName
            Else
                oScClaimsData.CustomerName = Nothing
            End If

            If Not request.FromClaimCrtDate = Nothing Then
                oScClaimsData.FromClaimCreatedDate = request.FromClaimCrtDate
            Else
                oScClaimsData.FromClaimCreatedDate = Nothing
            End If

            If Not request.ToClaimCrtDate = Nothing Then
                oScClaimsData.ToClaimCreatedDate = request.ToClaimCrtDate
            Else
                oScClaimsData.ToClaimCreatedDate = Nothing
            End If

            If Not request.FromVisitDate = Nothing Then
                oScClaimsData.FromVisitDate = request.FromVisitDate
            Else
                oScClaimsData.FromVisitDate = Nothing
            End If

            If Not request.ToVisitDate = Nothing Then
                oScClaimsData.ToVisitDate = request.ToVisitDate
            Else
                oScClaimsData.ToVisitDate = Nothing
            End If

            If Not request.TATRangeCode = Nothing Then
                oScClaimsData.TurnAroundTimeRangeCode = request.TATRangeCode
            Else
                oScClaimsData.TurnAroundTimeRangeCode = Nothing
            End If

            oScClaimsData.CompanyCode = request.CompanyCode

            If Not request.ServiceCenterCode = Nothing Then
                dsClaims = Claim.GetClaimsForServiceCenter(oScClaimsData, oScClaimsData.PageNumber = 1)
                If dsClaims Is Nothing OrElse dsClaims.Tables.Count <= 0 OrElse dsClaims.Tables(0).Rows.Count <= 0 Then
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIMS_NOT_FOUND_FOR_SERVICE_CENTER,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Else
                    response.ServiceCenterClaims = dsClaims _
                        .Tables(0) _
                        .AsEnumerable() _
                        .[Select](Function(row) New ServiceCenterClaimsInfo() With
                                  {
                                        .ClaimNumber = row("claim_number").ToString(),
                                        .AuthorizationNumber = row("authorization_number").ToString(),
                                        .ClaimTypeDescription = row("claim_type_description").ToString(),
                                        .ClaimStatus = row("claim_status").ToString(),
                                        .MethodofRepair = row("method_of_repair").ToString(),
                                        .ClaimCreatedtDate = row.Field(Of Date?)("claim_created_date"),
                                        .CertificateNumber = row("certificate_number").ToString(),
                                        .ProductDescription = row("product_description").ToString(),
                                        .ProblemDescription = row("problem_description").ToString(),
                                        .Model = row("item_model").ToString(),
                                        .Make = row("item_manufacturer_description").ToString(),
                                        .CustomerName = row("customer_name").ToString(),
                                        .ClaimExtendedStatusDescription = row("claim_extended_status").ToString(),
                                        .ClaimExtendedStatusOwner = row("extended_status_owner").ToString(),
                                        .AuthorizationAmount = row("authorized_amount").ToString(),
                                        .ServiceCenterCode = row("service_center_code").ToString(),
                                        .ServiceCenterDescription = row("service_center_description").ToString(),
                                        .VisitDate = row.Field(Of Date?)("visit_date"),
                                        .RepairDate = row.Field(Of Date?)("repair_date"),
                                        .LossDate = row.Field(Of Date?)("loss_date"),
                                        .BatchNumber = row("batch_number").ToString(),
                                        .SerialNumber = row("serial_number").ToString(),
                                        .CustomerAddress = row("address1").ToString(),
                                        .CustomerEmail = row("email").ToString(),
                                        .CustomerCity = row("city").ToString(),
                                        .CustomerProvince = row("province").ToString(),
                                        .WorkPhone = row("work_phone").ToString(),
                                        .HomePhone = row("home_phone").ToString(),
                                        .CoverageType = row("coverage_type").ToString(),
                                        .CoverageStartDate = row.Field(Of Date?)("coverage_start_date"),
                                        .CoverageEndDate = row.Field(Of Date?)("coverage_end_date"),
                                        .AssurantPays = row("Assurant_Pays").ToString(),
                                        .ConsumerPaid = row("Consumer_Pays").ToString(),
                                        .SalvageAmount = row("Salvage_Amount").ToString(),
                                        .ClaimPaidAmount = row("claim_paid_amount").ToString(),
                                        .ProductSalePrice = row("product_sales_price").ToString(),
                                        .BonusTotal = row("bonus_total").ToString(),
                                        .ClaimTAT = row("Claim_TAT").ToString(),
                                        .ServiceCenterTAT = row("sc_turn_around_time").ToString()
                                 }) _
                               .ToList()

                    If dsClaims.Tables(0).Rows.Count <> 0 Then
                        With response
                            .TotalRecordCount = dsClaims.Tables(0).Rows(0)("COUNT").ToString()
                        End With
                    End If
                End If
            End If
            Return response
        End Function

        Public Function GetParts(request As GetPartsRequest) As GetPartsResponse Implements ISNMPService.GetParts
            request.Validate("request").HandleFault()

            Dim response As New GetPartsResponse
            Dim ds As New DataSet
            Dim PartDiscriptionList As DataTable

            Dim dvRiskGroups As DataView = LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            If Not dvRiskGroups Is Nothing AndAlso dvRiskGroups.Count > 0 Then
                _Risk_Group_id = LookupListNew.GetIdFromCode(dvRiskGroups, request.RiskGroupCode)
                If _Risk_Group_id.Equals(Guid.Empty) Then
                    Throw New FaultException(Of PartInfoNotFoundFault)(New PartInfoNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_RISK_GROUP,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End If
            End If

            Try
                PartDiscriptionList = PartsDescription.getListForWS(RiskGroupId)
                If PartDiscriptionList Is Nothing OrElse PartDiscriptionList.Rows.Count <= 0 Then
                    Throw New FaultException(Of PartInfoNotFoundFault)(New PartInfoNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_PART_INFO,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Else
                    ds.Tables.Add(PartDiscriptionList.Copy)
                    response.PartInfo = ds _
                    .Tables(0) _
                    .AsEnumerable() _
                    .[Select](Function(row) New PartsDescriptionInfo() With
                            {
                                    .RiskGroup = row.Field(Of String)("Risk_Group"),
                                    .Description = row.Field(Of String)("description"),
                                    .EnglishDescription = row.Field(Of String)("description_english"),
                                    .Code = row.Field(Of String)("code")
                                }) _
                    .ToList()
                End If
                Return response
            Catch ex As Exception
                Throw New FaultException(Of PartInfoNotFoundFault)(New PartInfoNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_PART_INFO,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try
        End Function


        Public Function GetClaimDetail(request As GetClaimDetailRequest) As GetClaimDetailResponse Implements ISNMPService.GetClaimDetail

            request.Validate("request").HandleFault()
            Dim dsClaim As DataSet
            Dim response As New GetClaimDetailResponse

            Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim i As Integer = Nothing

            Try
                For i = 0 To objCompaniesAL.Count - 1
                    Dim objCompany As New Company(CType(objCompaniesAL.Item(i), Guid))
                    If Not objCompany Is Nothing AndAlso objCompany.Code.Equals(request.CompanyCode.ToUpper) Then
                        CompanyId = objCompany.Id
                    End If
                Next
                If CompanyId.Equals(Guid.Empty) Then
                    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.CompanyCode)
                End If
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.CompanyCode)
            End Try

            Try
                dsClaim = Claim.ClaimDetailForWS(request.ClaimNumber, CompanyId, request.ForServiceCenterUse, request.IncludePartDescriptions)
                If dsClaim Is Nothing Or dsClaim.Tables.Count <= 0 Or dsClaim.Tables(0).Rows.Count = 0 Then
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
                Else
                    Dim row As DataRow
                    For Each row In dsClaim.Tables(0).Rows
                        With response
                            .CompanyCode = request.CompanyCode
                            .ClaimNumber = row("claim_number").ToString()
                            .CoverageType = row("coverage_type").ToString()
                            .ClaimTypeCode = row("claim_type_code").ToString()
                            .AuthorizationNumber = row("authorization_number").ToString()
                            .MethodofRepair = row("method_of_repair").ToString()
                            .ClaimCreatedtDate = row.Field(Of Date?)("created_date")
                            .ProblemDescription = row("problem_description").ToString()
                            .VisitDate = row.Field(Of Date?)("visit_date")
                            .RepairDate = row.Field(Of Date?)("repair_date")
                            .CertificateNumber = row("certificate_number").ToString()
                            .ProductDescription = row("product_description").ToString()
                            .Model = row("item_model").ToString()
                            .Make = row("Item_Manufacturer_Description").ToString()
                            .SerialNumber = row("serial_number").ToString()
                            .IvaResponsible = row("iva_responsible").ToString()
                            .TaxId = row("tax_id").ToString()
                            .Zipcode = row("zip_code").ToString()
                            .StatusCode = row("status_code").ToString()
                            .DealerCode = row("dealer_code").ToString()
                            .DealerBranchCode = row("dealer_branch_code").ToString()
                            .CoverageStartDate = row.Field(Of Date?)("coverage_start_date")
                            .CoverageEndDate = row.Field(Of Date?)("coverage_end_date")
                            .NonReplacementClaimsCount = row("non_replacement_claims_count").ToString()
                            .TotalAuthorizedAmount = row("claims_total_auth_amt").ToString()
                            .ProductSalePrice = row("product_sales_price").ToString()
                            .CustomerName = row("customer_name").ToString()
                            .CustomerAddress = row("address1").ToString()
                            .CustomerCity = row("city").ToString()
                            .CustomerProvince = row("province").ToString()
                            .CustomerHomePhone = row("home_phone").ToString()
                            .CustomerWorkPhone = row("work_phone").ToString()
                            .CustomerEmail = row("email").ToString()
                            .LabourAmount = row("labor_amount").ToString()
                            .OtherAmount = row("other_amount").ToString()
                            .ShipmentAmount = row("shipping_amount").ToString()
                            .SalvageAmount = row("salvage_amount").ToString()
                            .AssurantPays = row("Assurant_Pays").ToString()
                            .ConsumerPaid = row("Consumer_Pays").ToString()
                            .TotalPaid = row("total_paid").ToString()
                            .OtherDescription = row("other_explanation").ToString()
                            .ServiceChargeAmount = row("service_charge_amount").ToString()
                            .TripAmount = row("trip_amount").ToString()
                            .AuthorizedAmount = row("authorized_amount").ToString()
                        End With
                    Next

                    If dsClaim.Tables(1).Rows.Count <> 0 Then
                        response.ClaimExtendedStatusList = dsClaim _
                        .Tables(1) _
                        .AsEnumerable() _
                        .[Select](Function(row1) New ClaimStatusInfo() With
                                     {
                                    .ClaimExtendedStatusCode = row1.Field(Of String)("status_code"),
                                    .ClaimExtendedStatusDescription = row1.Field(Of String)("status_description"),
                                    .ClaimExtendedStatusDate = row1.Field(Of Date?)("status_date"),
                                    .ClaimExtendedStatusComments = row1.Field(Of String)("comments"),
                                    .ClaimExtendedStatusOwnerDescription = row1.Field(Of String)("Extended_Status_Owner")
                                }) _
                    .ToList()
                    End If

                    If dsClaim.Tables(2).Rows.Count <> 0 Then
                        response.ClaimCommentsList = dsClaim _
                        .Tables(2) _
                        .AsEnumerable() _
                        .[Select](Function(row2) New ClaimCommentList() With
                                     {
                                    .Comments = row2.Field(Of String)("comments"),
                                    .CommentDate = row2.Field(Of Date?)("created_date")
                                }) _
                    .ToList()
                    End If

                    If dsClaim.Tables(3).Rows.Count <> 0 Then
                        response.PartsListInfo = dsClaim _
                      .Tables(3) _
                      .AsEnumerable() _
                      .[Select](Function(row3) New PartsListInfo() With
                              {
                                      .InStockDescription = row3.Field(Of String)("in_stock_description"),
                                      .StockCode = row3.Field(Of String)("in_stock_code"),
                                      .Amount = row3.Field(Of Double?)("cost"),
                                      .Code = row3.Field(Of String)("code"),
                                      .Description = row3.Field(Of String)("description")
                                  }) _
                      .ToList()
                    End If

                    If dsClaim.Tables(4).Rows.Count <> 0 Then
                        response.PartsDescriptionInfo = dsClaim _
                        .Tables(4) _
                        .AsEnumerable() _
                        .[Select](Function(row4) New PartsDescriptionInfo() With
                                {
                                        .RiskGroup = row4.Field(Of String)("Risk_Group"),
                                        .Description = row4.Field(Of String)("description"),
                                        .EnglishDescription = row4.Field(Of String)("description_english"),
                                        .Code = row4.Field(Of String)("code")
                                    }) _
                        .ToList()
                    End If
                End If
                Return response
            Catch ex As Exception
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
            End Try
        End Function

        Public Function GetServiceCenter(request As GetServiceCenterRequest) As GetServiceCenterResponse Implements ISNMPService.GetServiceCenter

            request.Validate("request").HandleFault()

            Dim response As New GetServiceCenterResponse
            Dim ds As DataSet
            Dim row As DataRow
            Dim oCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            _country_id = oCompany.CountryId

            Try
                ds = ServiceCenter.GetServiceCenterForWS(request.ServiceCenterCode, CountryId)
                If ds Is Nothing OrElse ds.Tables.Count <= 0 OrElse ds.Tables(0).Rows.Count <= 0 Then
                    Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_SERVICE_CENTER_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Else
                    For Each row In ds.Tables(0).Rows
                        With response
                            .Code = row("code").ToString()
                            .Description = row("description").ToString()
                            .IvaResponsible = row("iva_responsible").ToString()
                            .TaxId = row("tax_id").ToString()
                            .ContactName = row("contact_name").ToString()
                            .OwnerName = row("owner_name").ToString()
                            .Phone1 = row("phone1").ToString()
                            .Phone2 = row("phone2").ToString()
                            .Fax = row("fax").ToString()
                            .Email = row("email").ToString()
                            .CcEmail = row("cc_email").ToString()
                            .BusinessHours = row("business_hours").ToString()
                            .PayMaster = row("pay_master").ToString()
                        End With
                    Next
                End If
                Return response
            Catch ex As Exception
                Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_SERVICE_CENTER_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try
        End Function


        Private Sub DeleteExistingParts()
            Dim i As Integer

            For i = 0 To _PartsInfoDV.Count - 1
                Dim objPartInfoId As Guid = New Guid(CType(_PartsInfoDV.Item(i)(PartsInfoDAL.COL_NAME_PARTS_INFO_ID), Byte()))
                Dim objPartInfo As PartsInfo = Me.ClaimBO.AddPartsInfo(objPartInfoId)
                objPartInfo.Delete()
            Next
        End Sub
        Public Sub UpdateClaim(request As UpdateClaimRequest) Implements ISNMPService.UpdateClaim

            request.Validate("request").HandleFault()
            Dim oClaimStatus As ClaimStatus
            Dim oPartInfo As PartsInfo
            Dim ClaimStatusByGroupID As Guid
            Dim objClaimAuthDetail As ClaimAuthDetail
            Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim i As Integer = Nothing
            Dim ByPassClaimAuthValidation As Boolean = False
            Dim Const_ClaimExtStatus_ASPWCDP As String = "ASPWCDP" 'Accepted by SP and waiting for customer to deliver product to SP
            Dim Const_ClaimExtStatus_ASPSAPP As String = "ASPSAPP" 'Accepted by SP and schedule appointment

            'US 224089
            Dim RunClaimAuthValidationRules As Boolean = False
            Dim Const_ClaimExtStatus_Waiting2nd As String = "SPW2A" 'SP waiting for 2nd Authorization
            Dim Const_ClaimExtStatus_2ndApproved As String = "2AA" '2nd Authorization is approved 
            Dim Const_ClaimExtStatus_WaitingAuth As String = "ASRTESTAUTHDTLPLSWAIT" 'Assurant is estimating the authorization detail you submitted, please wait 

            Try
                For i = 0 To objCompaniesAL.Count - 1
                    Dim objCompany As New Company(CType(objCompaniesAL.Item(i), Guid))
                    If Not objCompany Is Nothing AndAlso objCompany.Code.Equals(request.CompanyCode.ToUpper) Then
                        CompanyId = objCompany.Id
                    End If
                Next
                If CompanyId.Equals(Guid.Empty) Then
                    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.CompanyCode)
                End If
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.CompanyCode)
            End Try

            Try
                _claimId = Claim.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, request.ClaimNumber)

                If Not ClaimId.Equals(Guid.Empty) Then
                    _claimBo = ClaimFacade.Instance.GetClaim(Of Claim)(_claimId)
                Else
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
                End If
            Catch ex As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
            End Try

            Try
                If request.ClaimExtendedStatusList IsNot Nothing Then
                    For i = 0 To request.ClaimExtendedStatusList.Count - 1
                        With request.ClaimExtendedStatusList(i)
                            If .ClaimExtendedStatusCode = Const_ClaimExtStatus_ASPSAPP OrElse .ClaimExtendedStatusCode = Const_ClaimExtStatus_ASPWCDP Then
                                ByPassClaimAuthValidation = True
                            ElseIf .ClaimExtendedStatusCode.Equals(Const_ClaimExtStatus_Waiting2nd, StringComparison.InvariantCultureIgnoreCase) Then 'US 224089 - If submitted extended code is 'SP waiting for 2nd Authorization'
                                RunClaimAuthValidationRules = True
                            End If
                            ClaimStatusByGroupID = ClaimStatusByGroup.GetClaimStatusByGroupID(.ClaimExtendedStatusCode)
                            oClaimStatus = ClaimBO.AddExtendedClaimStatus(Guid.Empty)
                            oClaimStatus.ClaimId = ClaimBO.Id
                            oClaimStatus.ClaimStatusByGroupId = ClaimStatusByGroupID
                            oClaimStatus.StatusDate = .ClaimExtendedStatusDate
                            oClaimStatus.Comments = .ClaimExtendedStatusComments
                            oClaimStatus.Validate()
                        End With
                    Next
                End If
            Catch ex As Exception
                Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_EXT_STATUS_CODE,
                                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            If request.ClaimExtendedStatusList IsNot Nothing Then
                If oClaimStatus.Comments.Length > 300 Then
                    Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), "The field ClaimExtendedStatusComment must be a String With a maximum length Of 300 ")
                End If
            End If

            ''' This section should be enabled back once the BRAND / MODEL standardization is implemented.  This section is tested and worked FINE
            '''US 224089 
            ''Dim newPartsList As List(Of PartsInfo)

            Try
                If request.PartsListInfo IsNot Nothing Then
                    _PartsInfoDV = PartsInfo.getSelectedList(Me.ClaimBO.Id)
                    DeleteExistingParts()

                    ''' This section should be enabled back once the BRAND / MODEL standardization is implemented.  This section is tested and worked FINE
                    ''If (request.PartsListInfo.Count > 0) Then
                    ''    newPartsList = New List(Of PartsInfo)()
                    ''End If

                    For i = 0 To request.PartsListInfo.Count - 1
                        With request.PartsListInfo(i)
                            Dim objPartDescriptionId As Guid = PartsDescription.GetPartDescriptionByCode(.Code, ClaimId)
                            oPartInfo = ClaimBO.AddPartsInfo(Guid.Empty)
                            oPartInfo.PartsDescriptionId = objPartDescriptionId
                            oPartInfo.ClaimId = ClaimBO.Id
                            oPartInfo.Cost = .Amount
                            oPartInfo.InStockID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, .StockCode.ToString)
                            oPartInfo.Validate()

                            ''' This section should be enabled back once the BRAND / MODEL standardization is implemented.  This section is tested and worked FINE
                            '''US 224089 
                            ''newPartsList.Add(oPartInfo)

                            _PartsAmount += oPartInfo.Cost.Value
                            PartsDataHasChanged = True
                        End With
                    Next
                End If
            Catch ex As Exception
                Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_PART_CODE,
                                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            Try
                If Not request.RepairDate = Nothing Then
                    If request.RepairDate.ToShortDateString > Today OrElse request.RepairDate < ClaimBO.LossDate OrElse request.RepairDate < ClaimBO.CreatedDate Then
                        Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_INVALID_REPAIR_DATE,
                                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    Else
                        ClaimBO.RepairDate = request.RepairDate
                    End If
                End If
            Catch ex As Exception
                Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_INVALID_REPAIR_DATE,
                                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            Try
                If Not request.VisitDate = Nothing Then
                    'Req 6292 / US 214283 - Updating validation to prevent exception when RepairDate is null and VisitDate has value
                    If request.VisitDate.ToShortDateString > Today OrElse (ClaimBO.LossDate IsNot Nothing AndAlso request.VisitDate < ClaimBO.LossDate) OrElse (ClaimBO.RepairDate IsNot Nothing AndAlso request.VisitDate > ClaimBO.RepairDate) Then
                        Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(INVALID_VISIT_DATE_ERR,
                                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    Else
                        ClaimBO.VisitDate = request.VisitDate

                    End If
                End If
            Catch ex As Exception
                Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(INVALID_VISIT_DATE_ERR,
                                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            'Req 6292 / US 214283 - Adding Pick-Up date and salvage Amount
            If (request.PickupDate.HasValue()) Then
                ClaimBO.PickUpDate = request.PickupDate.Value
            End If

            If (request.SalvageAmount.HasValue()) Then
                If (request.SalvageAmount.Value < 0) Then
                    Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_INVALID_SALVAGE_AMOUNT,
                                                                                                                                            ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                Else
                    ClaimBO.SalvageAmount = request.SalvageAmount
                End If
            End If

            If request.PartAmount = 0.0 And request.ShipmentAmount = 0.0 And request.LabourAmount = 0.0 And request.ServiceChargeAmount = 0.0 And request.TripAmount = 0.0 And request.OtherAmount = 0.0 And ClaimBO.ClaimNumber.EndsWith("S") Then
                ByPassClaimAuthValidation = True
            End If

            If Not ByPassClaimAuthValidation Then

                Try

                    'This claim may/may not have a claimAuthDetail record.
                    Try
                        objClaimAuthDetail = ClaimBO.AddClaimAuthDetail(ClaimBO.Id, True)
                    Catch ex As Exception
                        objClaimAuthDetail = ClaimBO.AddClaimAuthDetail(Guid.Empty)
                        objClaimAuthDetail.ClaimId = ClaimBO.Id
                    End Try

                    If request.PartsListInfo IsNot Nothing Then
                        objClaimAuthDetail.PartAmount = _PartsAmount
                    End If

                    If Not request.ShipmentAmount = Nothing Then
                        objClaimAuthDetail.ShippingAmount = request.ShipmentAmount
                    End If

                    If Not request.LabourAmount = Nothing Then
                        objClaimAuthDetail.LaborAmount = request.LabourAmount
                    End If

                    If Not request.ServiceChargeAmount = Nothing Then
                        objClaimAuthDetail.ServiceCharge = request.ServiceChargeAmount
                    End If

                    If Not request.TripAmount = Nothing Then
                        objClaimAuthDetail.TripAmount = request.TripAmount
                    End If

                    If Not request.OtherAmount = Nothing Then
                        objClaimAuthDetail.OtherAmount = request.OtherAmount
                    End If

                    If Not request.OtherDescription = Nothing Then
                        objClaimAuthDetail.OtherExplanation = request.OtherDescription
                    End If

                    objClaimAuthDetail.Validate()
                    ClaimBO.AuthDetailDataHasChanged = True

                Catch ex As Exception
                    Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_UPDATE_CLAIM_ERROR,
                                                                                                                                            ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End Try
            Else
                ClaimBO.AuthDetailDataHasChanged = False
            End If



            Try
                If ClaimBO.AuthDetailDataHasChanged = False AndAlso Not ByPassClaimAuthValidation Then
                    ClaimBO.AuthDetailDataHasChanged = PartsDataHasChanged
                End If

                ClaimBO.Save()

            Catch boEx As BOValidationException 'Req 6292 / US 214283 - Adding Validation from BO to catch exception when PickUp date has invalid values as per PickUp date's validation rules
                Dim msg As String = If(Not boEx.ValidationErrorList Is Nothing AndAlso boEx.ValidationErrorList.Count > 0, boEx.ValidationErrorList(0).Message, boEx.Message)
                Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(msg, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            Catch ex As Exception
                Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_UPDATE_CLAIM_ERROR,
                                                                                                 ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            ''''''' IMPORTANT!!!!
            ''' 
            ''' This section is disabled to avoid veryfing Rules for China as per Jinia on 8/31/2018 - China requested to include Parts' brand as when obtaining the Price List which is not supported at this time 
            ''' This section should be enabled back once the BRAND / MODEL standardization is implemented.  This section is tested and worked FINE
            ''' 
            ''' 
            ''''US 224089 - If submitted extended code Is 'SP waiting for 2nd Authorization'
            '''If (RunClaimAuthValidationRules) Then

            '''    Dim automaticStatusCode As String = String.Empty

            '''    Dim rulesResponse As Contracts.ClaimAuthorizedAmountResponse

            '''    Try
            '''        If (_fulfilmentRuleProvider Is Nothing) Then
            '''            Throw New ArgumentNullException("FulfillmentRuleProvider")
            '''        End If

            '''        rulesResponse = FulfillmentRulesHelper.ExecuteClaimsAuthorizedAmountRules(ClaimBO,
            '''                                                                                    objClaimAuthDetail,
            '''                                                                                    newPartsList,
            '''                                                                                    _fulfilmentRuleProvider)



            '''    Catch notSupportedEx As NotSupportedException
            '''        Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_FULFILLMENT_RULES, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " - " & notSupportedEx.Message)
            '''    Catch validationEx As FaultException
            '''        Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_FULFILLMENT_RULES, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " - " & validationEx.Message)
            '''    Catch ex As Exception
            '''        Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_UPDATE_CLAIM_ERROR, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " " &
            '''                                                                                        TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_FULFILLMENT_RULES, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            '''    End Try


            '''    'US 224089 - If Rules were checked due to 'SP waiting for 2nd Authorization' extended status sent
            '''    If (rulesResponse IsNot Nothing) Then

            '''        'US 224089 - If no rule was violated
            '''        If (rulesResponse.Triggered.Equals("false", StringComparison.InvariantCultureIgnoreCase)) Then

            '''            ClaimBO.AuthorizedAmount = objClaimAuthDetail.LaborAmount.Value +
            '''                                   objClaimAuthDetail.ShippingAmount.Value +
            '''                                   objClaimAuthDetail.PartAmount.Value +
            '''                                   objClaimAuthDetail.TripAmount.Value +
            '''                                   objClaimAuthDetail.ServiceCharge.Value +
            '''                                   objClaimAuthDetail.OtherAmount.Value


            '''            automaticStatusCode = Const_ClaimExtStatus_2ndApproved
            '''        Else 'US 224089 - If at least 1 rule was violated
            '''            automaticStatusCode = Const_ClaimExtStatus_WaitingAuth
            '''        End If
            '''        Try


            '''            Dim approvedoClaimStatus As ClaimStatus
            '''            ClaimStatusByGroupID = ClaimStatusByGroup.GetClaimStatusByGroupID(automaticStatusCode)
            '''            approvedoClaimStatus = ClaimBO.AddExtendedClaimStatus(Guid.Empty)
            '''            approvedoClaimStatus.ClaimId = ClaimBO.Id
            '''            approvedoClaimStatus.ClaimStatusByGroupId = ClaimStatusByGroupID
            '''            approvedoClaimStatus.StatusDate = DateTime.Now
            '''            approvedoClaimStatus.Comments = String.Empty
            '''            approvedoClaimStatus.Validate()

            '''        Catch ex As Exception
            '''            Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_EXT_STATUS_CODE, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & ": " & automaticStatusCode)
            '''        End Try

            '''    End If

            '''    'Adding a delay of 1 second to ensure Extended status are saved in correct order
            '''    System.Threading.Thread.Sleep(1000)

            '''    Try
            '''        'Saving ClaimBO for 2nd time to update Extended Status
            '''        ClaimBO.Save()
            '''    Catch boEx As BOValidationException 'Req 6292 / US 214283 - Adding Validation from BO to catch exception when PickUp date has invalid values as per PickUp date's validation rules
            '''        Dim msg As String = If(Not boEx.ValidationErrorList Is Nothing AndAlso boEx.ValidationErrorList.Count > 0, boEx.ValidationErrorList(0).Message, boEx.Message)
            '''        Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(msg, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            '''    Catch ex As Exception
            '''        Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_UPDATE_CLAIM_ERROR, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            '''    End Try

            '''    'US 224089 - If no rule was violated, re-generate service order
            '''    If (automaticStatusCode.Equals(Const_ClaimExtStatus_2ndApproved, StringComparison.InvariantCultureIgnoreCase)) Then
            '''        Dim sOrder As ServiceOrder = ServiceOrder.GenerateServiceOrder(ClaimBO)
            '''        If (sOrder Is Nothing) Then
            '''            Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_UPDATE_CLAIM_ERROR,
            '''                                                                                                                                        ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            '''        End If
            '''    End If
            '''End If

        End Sub

        Private Sub UpdateClaimInvoiceBOWithClaimAuthDetail(clmAuthDetail As ClaimAuthDetail, clmInvoice As ClaimInvoice)
            With clmInvoice
                .LaborAmt = clmAuthDetail.LaborAmount
                .PartAmount = clmAuthDetail.PartAmount
                .ServiceCharge = clmAuthDetail.ServiceCharge
                .TripAmount = clmAuthDetail.TripAmount
                .OtherExplanation = clmAuthDetail.OtherExplanation
                .OtherAmount = clmAuthDetail.OtherAmount
                .Amount = Me.GetAuthDetailTotal(clmAuthDetail)
            End With
        End Sub

        Private Function GetAuthDetailTotal(clmAuthDetail As ClaimAuthDetail) As Decimal
            Dim amount As Decimal = 0
            If Not clmAuthDetail.LaborAmount Is Nothing Then amount += clmAuthDetail.LaborAmount.Value
            If Not clmAuthDetail.PartAmount Is Nothing Then amount += clmAuthDetail.PartAmount.Value
            If Not clmAuthDetail.ServiceCharge Is Nothing Then amount += clmAuthDetail.ServiceCharge.Value
            If Not clmAuthDetail.TripAmount Is Nothing Then amount += clmAuthDetail.TripAmount.Value
            If Not clmAuthDetail.ShippingAmount Is Nothing Then amount += clmAuthDetail.ShippingAmount.Value
            If Not clmAuthDetail.OtherAmount Is Nothing Then amount += clmAuthDetail.OtherAmount.Value


            Return amount
        End Function
#End Region
    End Class
End Namespace
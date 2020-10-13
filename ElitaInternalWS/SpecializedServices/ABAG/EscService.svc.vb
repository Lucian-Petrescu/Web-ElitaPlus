Imports System.Collections.Generic
Imports System.Globalization
Imports System.ServiceModel
Imports System.Threading
Imports Assurant.Common.Validation
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.Common.ErrorCodes
Imports Assurant.ElitaPlus.DALObjects

Namespace SpecializedServices.Abag

    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecialzedServices/Abag")>
    Public Class EscService
        Implements IEscService

#Region "Member Variables"
        Private _country_id As Guid = Guid.Empty
        Private _company_id As Guid = Guid.Empty
        Private _company_group_id As Guid = Guid.Empty
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
            Set(Value As Guid)
                _company_id = Value
            End Set
        End Property

        Public Property CompanyGroupId() As Guid
            Get
                Return _company_group_id
            End Get
            Set(Value As Guid)
                _company_group_id = Value
            End Set
        End Property

        Private Property ClaimBO() As Claim
            Get
                Return _claimBo
            End Get
            Set(value As Claim)
                _claimBo = value
            End Set
        End Property


        Private ReadOnly Property RiskGroupId() As Guid
            Get
                Return _Risk_Group_id
            End Get
        End Property

        'Private ReadOnly Property ClaimId() As Guid
        '    Get
        '        Return _claimId
        '    End Get
        'End Property

#End Region

#Region "Member Methods"
        Public Function GetServiceCenterClaims(request As GetServiceCenterClaimsRequest) As GetServiceCenterClaimsResponse Implements IEscService.GetServiceCenterClaims
            request.Validate("request").HandleFault()

            Dim response As New GetServiceCenterClaimsResponse
            Dim dsClaims As DataSet
            Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim oScClaimsData As New ClaimDAL.ServiceCenterClaimsSearchData
            Dim i As Integer = Nothing

            If Not request.CompanyCode = Nothing Then
                ValidateCompanyCode(request.CompanyCode)
            End If

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

            If Not request.CompanyCode = Nothing Then
                oScClaimsData.CompanyCode = request.CompanyCode
            Else
                oScClaimsData.CompanyCode = Nothing
            End If

            'oScClaimsData.CompanyCode = request.CompanyCode

            If Not request.BatchNumber = Nothing Then
                oScClaimsData.BatchNumber = request.BatchNumber
            Else
                oScClaimsData.BatchNumber = Nothing
            End If

            If Not request.SerialNumber = Nothing Then
                oScClaimsData.SerialNumber = request.SerialNumber
            Else
                oScClaimsData.SerialNumber = Nothing
            End If

            If Not request.WorkPhone = Nothing Then
                oScClaimsData.WorkPhone = request.WorkPhone
            Else
                oScClaimsData.WorkPhone = Nothing
            End If

            If Not request.ServiceCenterCode = Nothing Then
                dsClaims = Claim.GetClaimsForServiceCenterAC(oScClaimsData, oScClaimsData.PageNumber = 1)
                If dsClaims Is Nothing OrElse dsClaims.Tables.Count <= 0 OrElse dsClaims.Tables(0).Rows.Count <= 0 Then
                    'Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIMS_NOT_FOUND_FOR_SERVICE_CENTER,
                    '                                                                                      ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    response.ServiceCenterClaims = New List(Of ServiceCenterClaimsInfo)
                    response.TotalRecordCount = 0


                Else
                    Dim decimalAux As Decimal

                    response.ServiceCenterClaims = dsClaims _
                        .Tables(0) _
                        .AsEnumerable() _
                        .[Select](Function(row) New ServiceCenterClaimsInfo() With
                                  {
                                        .ClaimId = GuidControl.ByteArrayToGuid(row("claim_id")).ToString(),
                                        .ClaimNumber = row("claim_number").ToString(),
                                        .CompanyCode = row("company_code").ToString(),
                                        .AuthorizationNumber = row("authorization_number").ToString(),
                                        .ClaimTypeDescription = row("claim_type_description").ToString(),
                                        .ClaimStatus = row("claim_status").ToString(),
                                        .MethodofRepair = row("method_of_repair").ToString(),
                                        .ClaimCreatedtDate = row.Field(Of Date?)("claim_created_date"),
                                        .CertificateNumber = row("certificate_number").ToString(),
                                        .ProductDescription = row("product_description").ToString(),
                                        .Model = row("item_model").ToString(),
                                        .Make = row("Item_Manufacturer_Description").ToString(),
                                        .CustomerName = row("customer_name").ToString(),
                                        .ClaimExtendedStatusCode = row("claim_extended_status_code").ToString(),
                                        .ClaimExtendedStatusDescription = row("claim_extended_status").ToString(),
                                        .ClaimExtendedStatusDate = row.Field(Of Date?)("extended_status_date"),
                                        .ClaimExtendedStatusOwner = row("extended_status_owner").ToString(),
                                        .AuthorizationAmount = If(Decimal.TryParse(row("authorized_amount").ToString(), decimalAux), decimalAux, Nothing),
                                        .ServiceCenterCode = row("service_center_code").ToString(),
                                        .ServiceCenterDescription = row("service_center_description").ToString(),
                                        .VisitDate = row.Field(Of Date?)("visit_date"),
                                        .RepairDate = row.Field(Of Date?)("repair_date"),
                                        .LossDate = row.Field(Of Date?)("loss_date"),
                                        .BatchNumber = row("batch_number").ToString(),
                                        .SerialNumber = row("serial_number").ToString(),
                                        .WorkPhone = row("work_phone").ToString(),
                                        .HomePhone = row("home_phone").ToString(),
                                        .ClaimPaidAmount = If(Decimal.TryParse(row("claim_paid_amount").ToString(), decimalAux), decimalAux, Nothing),
                                        .BonusTotal = If(Decimal.TryParse(row("bonus_total").ToString(), decimalAux), decimalAux, Nothing),
                                        .Deductible = If(Decimal.TryParse(row("deductible_amount").ToString(), decimalAux), decimalAux, Nothing),
                                        .ClaimTAT = row("Claim_TAT").ToString(),
                                        .ServiceCenterTAT = row("sc_turn_around_time").ToString(),
                                        .ClaimTypeCode = row("claim_type_code").ToString(),
                                        .CoverageType = row("coverage_type").ToString(),
                                        .TaxId = row("tax_id").ToString(),
                                        .DealerCode = row("dealer_code").ToString(),
                                        .DealerName = row("dealer_name").ToString()
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

        Public Function GetParts(request As GetPartsRequest) As GetPartsResponse Implements IEscService.GetParts
            request.Validate("request").HandleFault()

            Dim response As New GetPartsResponse
            Dim ds As New DataSet
            Dim PartDiscriptionList As DataTable

            Dim dvRiskGroups As DataView = LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            If dvRiskGroups IsNot Nothing AndAlso dvRiskGroups.Count > 0 Then
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


        Public Function GetClaimDetail(request As GetClaimDetailRequest) As GetClaimDetailResponse Implements IEscService.GetClaimDetail

            request.Validate("request").HandleFault()
            Dim dsClaim As DataSet
            Dim response As New GetClaimDetailResponse

            ValidateCompanyCode(request.CompanyCode)

            Try
                dsClaim = Claim.ClaimDetailForWS(request.ClaimNumber, CompanyId, request.ForServiceCenterUse, request.IncludePartDescriptions)
                If dsClaim Is Nothing OrElse dsClaim.Tables.Count <= 0 OrElse dsClaim.Tables(0).Rows.Count = 0 Then
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
                Else
                    Dim intAux As Integer
                    Dim decimalAux As Decimal


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
                            .NonReplacementClaimsCount = IF(Integer.TryParse(row("non_replacement_claims_count").ToString(), intAux) , intAux, 0)
                            .TotalAuthorizedAmount = IF(Decimal.TryParse(row("claims_total_auth_amt").ToString(), decimalAux) , decimalAux, Nothing)
                            .ProductSalePrice = IF(Decimal.TryParse(row("product_sales_price").ToString(), decimalAux) , decimalAux, Nothing) 
                            .CustomerName = row("customer_name").ToString()
                            .CustomerAddress = row("address1").ToString()
                            .CustomerCity = row("city").ToString()
                            .CustomerProvince = row("province").ToString()
                            .CustomerHomePhone = row("home_phone").ToString()
                            .CustomerWorkPhone = row("work_phone").ToString()
                            .CustomerEmail = row("email").ToString()
                            .LabourAmount = IF(Decimal.TryParse(row("labor_amount").ToString(), decimalAux) , decimalAux, Nothing)
                            .OtherAmount = IF(Decimal.TryParse(row("other_amount").ToString(), decimalAux) , decimalAux, Nothing)
                            .ShipmentAmount = IF(Decimal.TryParse(row("shipping_amount").ToString(), decimalAux) , decimalAux, Nothing)
                            .SalvageAmount = IF(Decimal.TryParse(row("salvage_amount").ToString(), decimalAux) , decimalAux, Nothing)
                            .AssurantPays = IF(Decimal.TryParse(row("Assurant_Pays").ToString(), decimalAux) , decimalAux, Nothing)
                            .ConsumerPaid = IF(Decimal.TryParse(row("Consumer_Pays").ToString(), decimalAux) , decimalAux, Nothing)
                            .Deductible = IF(Decimal.TryParse(row("deductible").ToString(), decimalAux) , decimalAux, Nothing)
                            .TotalPaid = IF(Decimal.TryParse(row("total_paid").ToString(), decimalAux) , decimalAux, Nothing) 
                            .OtherDescription = row("other_explanation").ToString()
                            .ServiceChargeAmount = IF(Decimal.TryParse(row("service_charge_amount").ToString(), decimalAux) , decimalAux, Nothing) 
                            .TripAmount = IF(Decimal.TryParse(row("trip_amount").ToString(), decimalAux) , decimalAux, Nothing)
                            .AuthorizedAmount = If(Decimal.TryParse(row("authorized_amount").ToString(), decimalAux), decimalAux, Nothing)
                            .DealerName = row("dealer_name").ToString()
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

        Public Function GetServiceCenter(request As GetServiceCenterRequest) As GetServiceCenterResponse Implements IEscService.GetServiceCenter

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
                            .MasterCenterCode = row("master_center_code").ToString()
                            .ServiceCenterAddress = row("address1").ToString() &
                                " " & row("address2").ToString() &
                                " " & row("city").ToString() &
                                " " & row("postal_code").ToString() &
                                " " & row("region").ToString() &
                                " " & row("country").ToString()
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
                Dim objPartInfo As PartsInfo = ClaimBO.AddPartsInfo(objPartInfoId)
                objPartInfo.Delete()
            Next
        End Sub


        Private Sub ValidateCompanyCode(CompanyCode As String)
            Try
                Dim i As Integer = Nothing
                Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                Dim userAssignedCompaniesDv As DataView = oUser.GetSelectedAssignedCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)

                For i = 0 To userAssignedCompaniesDv.Count - 1
                    Dim oCompanyId As New Guid(CType(userAssignedCompaniesDv.Table.Rows(i)("COMPANY_ID"), Byte()))
                    If Not oCompanyId = Nothing AndAlso userAssignedCompaniesDv.Table.Rows(i)("CODE").Equals(CompanyCode.ToUpper) Then
                        CompanyId = oCompanyId
                        Exit For
                    End If
                Next
                If CompanyId.Equals(Guid.Empty) Then
                    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                                  ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                                  " : " & CompanyCode)
                End If
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
                                                                                                                  ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                                  " : " & CompanyCode)
            End Try

            'Try
            '    Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            '    Dim i As Integer = Nothing

            '    For i = 0 To objCompaniesAL.Count - 1
            '        Dim objCompany As New Company(CType(objCompaniesAL.Item(i), Guid))
            '        If Not objCompany Is Nothing AndAlso objCompany.Code.Equals(CompanyCode.ToUpper) Then
            '            CompanyId = objCompany.Id
            '        End If
            '    Next
            '    If CompanyId.Equals(Guid.Empty) Then
            '        Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
            '                                                                                              ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
            '                                                                                              " : " & CompanyCode)
            '    End If
            'Catch conf As CompanyNotFoundException
            '    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_COMPANY_NOT_FOUND,
            '                                                                                              ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
            '                                                                                              " : " & CompanyCode)
            'End Try

        End Sub

        Public Sub UpdateClaim(request As UpdateClaimRequest) Implements IEscService.UpdateClaim

            request.Validate("request").HandleFault()
            Dim oClaimStatus As ClaimStatus
            Dim oPartInfo As PartsInfo
            Dim ClaimStatusByGroupID As Guid
            Dim objClaimAuthDetail As ClaimAuthDetail
            Dim i As Integer = Nothing
            Dim ByPassClaimAuthValidation As Boolean = False
            Dim Const_ClaimExtStatus_ASPWCDP As String = "ASPWCDP" 'Accepted by SP and waiting for customer to deliver product to SP
            Dim Const_ClaimExtStatus_ASPSAPP As String = "ASPSAPP" 'Accepted by SP and schedule appointment

            ValidateCompanyCode(request.CompanyCode)

            Try
                _claimBo = ClaimFacade.Instance.GetClaim(Of Claim)(request.ClaimNumber, CompanyId)

                If ClaimBO Is Nothing Then
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                                                                                                        ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
                End If

                '_claimId = Claim.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, request.ClaimNumber)

                'If Not ClaimId.Equals(Guid.Empty) Then
                '    _claimBo = ClaimFacade.Instance.GetClaim(Of Claim)(request.ClaimNumber, CompanyId)
                'Else
                '    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                '                                                                                    ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
                'End If
            Catch ex As Exception
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), TranslationBase.TranslateLabelOrMessage(ERR_CLAIM_NOT_FOUND,
                                                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)
            End Try

            Try
                If request.ClaimExtendedStatusList IsNot Nothing Then
                    For i = 0 To request.ClaimExtendedStatusList.Count - 1
                        With request.ClaimExtendedStatusList(i)
                            If .ClaimExtendedStatusCode = Const_ClaimExtStatus_ASPSAPP OrElse .ClaimExtendedStatusCode = Const_ClaimExtStatus_ASPWCDP Then
                                ByPassClaimAuthValidation = True
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

            Try
                If request.PartsListInfo IsNot Nothing Then
                    _PartsInfoDV = PartsInfo.getSelectedList(ClaimBO.Id)
                    DeleteExistingParts()
                    For i = 0 To request.PartsListInfo.Count - 1
                        With request.PartsListInfo(i)
                            Dim objPartDescriptionId As Guid = PartsDescription.GetPartDescriptionByCode(.Code, ClaimBO.Id)
                            oPartInfo = ClaimBO.AddPartsInfo(Guid.Empty)
                            oPartInfo.PartsDescriptionId = objPartDescriptionId
                            oPartInfo.ClaimId = ClaimBO.Id
                            oPartInfo.Cost = .Amount
                            oPartInfo.InStockID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, .StockCode.ToString)
                            oPartInfo.Validate()
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
                    If request.RepairDate?.ToShortDateString > Today OrElse request.RepairDate < ClaimBO.LossDate OrElse request.RepairDate < ClaimBO.CreatedDate Then
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
                    If request.VisitDate?.ToShortDateString > Today OrElse request.VisitDate < ClaimBO.LossDate OrElse request.VisitDate > ClaimBO.RepairDate Then
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
                    ElseIf  Not request.PartAmount.Equals(Nothing) Then
                        objClaimAuthDetail.PartAmount = request.PartAmount
                    End If

                    If Not request.ShipmentAmount.Equals(Nothing) Then
                        objClaimAuthDetail.ShippingAmount = request.ShipmentAmount
                    End If

                    If Not request.LabourAmount.Equals(Nothing) Then
                        objClaimAuthDetail.LaborAmount = request.LabourAmount
                    End If

                    If Not request.ServiceChargeAmount.Equals(Nothing) Then
                        objClaimAuthDetail.ServiceCharge = request.ServiceChargeAmount
                    End If

                    If Not request.TripAmount.Equals(Nothing) Then
                        objClaimAuthDetail.TripAmount = request.TripAmount
                    End If

                    If Not request.OtherAmount.Equals(Nothing) Then
                        objClaimAuthDetail.OtherAmount = request.OtherAmount
                    End If

                    If Not request.OtherDescription = Nothing Then
                        objClaimAuthDetail.OtherExplanation = request.OtherDescription
                    End If

                    objClaimAuthDetail.Validate()
                    ClaimBO.AuthDetailDataHasChanged = True
                Catch ex As BOValidationException
                    Dim validationExc As BOValidationException = ex
                    Dim fault As New ValidationFault()
                    fault.ValidationErrors.Add(ex.Code, ex.Message)

                    For Each err as ValidationError In validationExc.ValidationErrorList
                        fault.ValidationErrors.Add(err.PropertyName, TranslationBase.TranslateLabelOrMessage(err.Message, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    Next

                    Throw New FaultException(Of ValidationFault)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))
                Catch ex As Exception                   
                    Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_UPDATE_CLAIM_ERROR, ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " | Exception Message : " & ex.Message & " | StackTrace : " & ex.StackTrace)
                End Try
            Else
                ClaimBO.AuthDetailDataHasChanged = False
            End If

            Try
                If ClaimBO.AuthDetailDataHasChanged = False AndAlso Not ByPassClaimAuthValidation Then
                    ClaimBO.AuthDetailDataHasChanged = PartsDataHasChanged
                End If

                ClaimBO.Save()
            Catch ex As Exception
                Throw New FaultException(Of UpdateClaimErrorFault)(New UpdateClaimErrorFault(), TranslationBase.TranslateLabelOrMessage(ERR_UPDATE_CLAIM_ERROR, ElitaPlusIdentity.Current.ActiveUser.LanguageId)  & " | Exception Message : " & ex.Message & " | StackTrace : " & ex.StackTrace)
            End Try

        End Sub

        Public Function GetPreInvoice(request As GetPreInvoiceRequest) As GetPreInvoiceResponse Implements IEscService.GetPreInvoice

            request.Validate("request").HandleFault()
            Dim dsPreInvoice As DataSet
            Dim row As DataRow

            Dim preInvoices As New List(Of PreInvoiceInfo)()
            Dim preInvoiceResponse As New GetPreInvoiceResponse

            Try
                If request.CompanyCode = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must Provide Company Code ", WS_INVALID_COMPANY_CODE)
                Else
                    ValidateCompanyCode(request.CompanyCode)
                End If

                If request.ServiceCenterCode = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must Provide Service Center Code ", INVALID_SERVICE_CENTER_CODE)
                End If

                If Not PreInvoice.ValidateServiceCenterCode(request.ServiceCenterCode, PreInvoice.GetCountryId(PreInvoice.GetCompanyId(request.CompanyCode))) Then
                    Throw New BOValidationException("GetPreInvoice Error: Invalid Service Center Code ", INVALID_SERVICE_CENTER_CODE)
                End If

                If request.SCPreInvoiceDateFrom = Nothing AndAlso Not request.SCPreInvoiceDateTo = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must provide SC_PreInvoice_Date_From", WS_PREINVOICE_DATEFROM_NOT_FOUND)
                End If
                If request.SCPreInvoiceDateTo = Nothing AndAlso Not request.SCPreInvoiceDateFrom = Nothing Then
                    Throw New BOValidationException("GetPreInvoice Error: Must provide SC_PreInvoice_Date_To", WS_PREINVOICE_DATETO_NOT_FOUND)
                End If

                dsPreInvoice = PreInvoice.GetPreInvoiceBAL(request.CompanyCode, request.ServiceCenterCode, request.SCPreInvoiceDateFrom, request.SCPreInvoiceDateTo)

                'Fixing the current culture to English US for dates.
                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", False)

                Dim auxdate As Date

                If dsPreInvoice.Tables(0).Rows.Count <= 0 Then
                    Throw New BOValidationException("GetPreInvoice Error: No Data Found ", BO_DATA_NOT_FOUND)
                Else
                    For Each row In dsPreInvoice.Tables(0).Rows
                        Dim pi As New PreInvoiceInfo
                        With pi
                            .BatchNumber = row("BATCH_NUMBER")
                            .PreInvoiceDate = If(Date.TryParseExact(row("PREINVOICE_DATE"), "yyyy-MM-dd", New CultureInfo("en-US"), DateTimeStyles.None, auxdate), auxdate, row("PREINVOICE_DATE"))
                        End With
                        preInvoices.Add(pi)
                    Next
                End If

                preInvoiceResponse.PreInvoices = preInvoices

                Return preInvoiceResponse
            Catch ex As BOValidationException
                Dim fault As New ValidationFault()
                fault.ValidationErrors.Add(ex.Code, ex.Message)
                Throw New FaultException(Of ValidationFault)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))
            Catch ex As Exception
                Throw New FaultException(Of RequestWasNotSuccessFull)(New RequestWasNotSuccessFull(), New FaultReason(ex.ToString()), New FaultCode(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function GetPriceList(request As GetPriceListDetailRequest) As GetPriceListDetailResponse Implements IEscService.GetPriceList

            request.Validate("request").HandleFault()
            Dim dsPriceList As DataSet
            Dim row As DataRow

            Dim pricesDetails As New List(Of PriceDetailInfo)()
            Dim priceListDetailResponse As New GetPriceListDetailResponse

            If Not request.CompanyCode = Nothing Then
                ValidateCompanyCode(request.CompanyCode)
            End If


            Try

                Dim oSearch As New PriceListSearch

                If Not request.ClaimNumber = Nothing AndAlso Not request.CompanyCode = Nothing _
                    AndAlso Not request.ServiceCenterCode = Nothing AndAlso (Not request.EquipmentClassCode = Nothing OrElse Not request.RiskTypeCode = Nothing) Then

                    Throw New BOValidationException("GetPriceList Error: Search must be based on Claim or Service Center", WS_PRICELIST_INVALID_INPUT)
                End If

                If (Not request.ClaimNumber = Nothing AndAlso Not request.CompanyCode = Nothing) Then

                    Dim oSearchByClaim As New PriceListSearchByClaim()
                    oSearchByClaim.InForceDate = request.InForceDate
                    oSearchByClaim.ClaimNumber = request.ClaimNumber
                    oSearchByClaim.CompanyCode = request.CompanyCode
                    oSearchByClaim.ServiceClassCode = request.ServiceClassCode
                    oSearchByClaim.ServiceTypeCode = request.ServiceTypeCode
                    oSearchByClaim.Make = request.Make
                    oSearchByClaim.Model = request.Model
                    oSearchByClaim.LowPrice = request.LowPrice
                    oSearchByClaim.HighPrice = request.HighPrice
                    oSearchByClaim.ServiceLevelCode = request.ServiceLevelCode
                    oSearchByClaim.Validate()

                    oSearch.action = oSearchByClaim
                    dsPriceList = oSearch.GetPriceList(New PriceListSearchDC()) 'dummy empty PriceListSearchDC

                ElseIf (Not request.ServiceCenterCode = Nothing AndAlso Not request.RiskTypeCode = Nothing) _
                    OrElse (Not request.ServiceCenterCode = Nothing AndAlso Not request.EquipmentClassCode = Nothing) Then

                    If request.CompanyCode = Nothing Then
                        Dim userCompany As Company = ElitaPlusIdentity.Current.ActiveUser.Company
                        request.CompanyCode = userCompany.Code
                    End If

                    Dim oSearchByServiceCenter As New PriceListSearchByServiceCenter()
                    oSearchByServiceCenter.InForceDate = request.InForceDate
                    oSearchByServiceCenter.ServiceCenterCode = request.ServiceCenterCode
                    oSearchByServiceCenter.EquipmentClassCode = request.EquipmentClassCode
                    oSearchByServiceCenter.RiskTypeCode = request.RiskTypeCode
                    oSearchByServiceCenter.DealerCode = request.DealerCode
                    oSearchByServiceCenter.CompanyCode = request.CompanyCode
                    oSearchByServiceCenter.ServiceClassCode = request.ServiceClassCode
                    oSearchByServiceCenter.ServiceTypeCode = request.ServiceTypeCode
                    oSearchByServiceCenter.Make = request.Make
                    oSearchByServiceCenter.Model = request.Model
                    oSearchByServiceCenter.LowPrice = request.LowPrice
                    oSearchByServiceCenter.HighPrice = request.HighPrice
                    oSearchByServiceCenter.ServiceLevelCode = request.ServiceLevelCode
                    oSearchByServiceCenter.Validate()

                    oSearch.action = oSearchByServiceCenter
                    dsPriceList = oSearch.GetPriceList(New PriceListSearchDC()) 'dummy empty PriceListSearchDC
                Else
                    Throw New BOValidationException("GetPriceList Error: Must Provide Service Center Code with Risk Type Code/Equipment Class Code or Claim Number with Company Code", WS_PRICELIST_INVALID_INPUT)
                End If

                If (dsPriceList.Tables(0).Rows.Count = 0) Then
                    Throw New BOValidationException("GetPriceList Error: No Data Found ", BO_DATA_NOT_FOUND)
                Else
                    For Each row In dsPriceList.Tables(0).Rows
                        Dim pi As New PriceDetailInfo
                        With pi
                            .Service_Center_Code = row("SERVICE_CENTER_CODE").ToString()
                            .Risk_Type_Code = row("RISK_TYPE_CODE").ToString()
                            .Method_Of_Repair_Code = row("METHOD_OF_REPAIR_CODE").ToString()
                            .Service_Class_Code = row("SERVICE_CLASS_CODE").ToString()
                            .Service_Class_Translation = row("SERVICE_CLASS_TRANSLATION").ToString()
                            .Service_Type_Code = row("SERVICE_TYPE_CODE").ToString()
                            .Service_Type_Translation = row("SERVICE_TYPE_TRANSLATION").ToString()
                            .Service_Level_Code = row("SERVICE_LEVEL_CODE").ToString()
                            .Service_Level_Translation = row("SERVICE_LEVEL_TRANSLATION").ToString()
                            .Low_Price = row("LOW_PRICE")
                            .High_Price = row("HIGH_PRICE")
                            .Price = row("PRICE")
                        End With
                        pricesDetails.Add(pi)
                    Next
                End If

                priceListDetailResponse.PricesDetails = pricesDetails

                Return priceListDetailResponse
            Catch ex As BOValidationException
                Dim fault As New ValidationFault()
                fault.ValidationErrors.Add(ex.Code, ex.Message)
                Throw New FaultException(Of ValidationFault)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))
            Catch ex As Exception
                Throw New FaultException(Of RequestWasNotSuccessFull)(New RequestWasNotSuccessFull(), New FaultReason(ex.ToString()), New FaultCode(Codes.WEB_EXPERIENCE__FATAL_ERROR))
            End Try
        End Function

        Public Function GetExtendedClaimStatusList(request As GetExtendedStatusesRequest) As GetExtendedStatusesResponse Implements IEscService.GetExtendedClaimStatusList


            request.Validate("request").HandleFault()
            Dim dsExtendedStatuses As DataSet
            Dim row As DataRow

            Dim extendedStatuses As New List(Of ClaimExtendedStatusInfo)()
            Dim extendedStatusesResponse As New GetExtendedStatusesResponse

            Try


                If request.CompanyGroupCode IsNot Nothing AndAlso request.CompanyGroupCode <> "" Then

                    Dim list As DataView = LookupListNew.GetCompanyGroupLookupList()
                    If list Is Nothing Then
                        Throw New BOValidationException("Get Extended Claim Status List Error: ", DB_ERROR)
                    End If
                    CompanyGroupId = LookupListNew.GetIdFromCode(list, request.CompanyGroupCode)
                    If CompanyGroupId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("Get Extended Claim Status List Error: ", ERR_COMPANY_NOT_FOUND)
                    End If
                    list = Nothing
                Else
                    Throw New BOValidationException(TranslationBase.TranslateLabelOrMessage(ERR_INVALID_COMPANY_GROUP,
                                             ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                             " : " & request.CompanyGroupCode, ERR_INVALID_COMPANY_GROUP)
                End If

                If (CompanyGroupId <> ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) Then
                    Throw New BOValidationException(TranslationBase.TranslateLabelOrMessage(ERR_INVALID_COMPANY_GROUP,
                                             ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                             " : " & request.CompanyGroupCode, ERR_INVALID_COMPANY_GROUP)
                End If

                Dim companyGroup As CompanyGroup = New CompanyGroup(CompanyGroupId)
                companyGroup.Validate()

                dsExtendedStatuses = ClaimStatusByGroup.LoadListByCompanyGroup(companyGroup.Id)

                If dsExtendedStatuses Is Nothing Then
                    Throw New BOValidationException("Get Extended Claim Status List Error: ", DB_ERROR)
                ElseIf dsExtendedStatuses.Tables(0).Rows.Count <= 0 Then
                    Throw New BOValidationException("Get Extended Claim Status List: No Data Found ", BO_DATA_NOT_FOUND)
                ElseIf dsExtendedStatuses.Tables.Count > 0 AndAlso dsExtendedStatuses.Tables(0).Rows.Count > 0 Then

                    For Each row In dsExtendedStatuses.Tables(0).Rows
                        Dim pi As New ClaimExtendedStatusInfo
                        With pi
                            .Code = row("code").ToString()
                            .Description = row("description").ToString()
                            .OwnerCode = row("owner_code").ToString()
                            .OrderNumber = row("order_number").ToString()
                            .SkippingAllowedCode = row("skipping_allowed_code").ToString()
                            .GroupNumber = row("group_number").ToString()
                        End With
                        extendedStatuses.Add(pi)
                    Next
                End If

                extendedStatusesResponse.ExtendedStatuses = extendedStatuses

                Return extendedStatusesResponse

            Catch ex As BOValidationException
                Dim fault As New ValidationFault()
                fault.ValidationErrors.Add(ex.Code, ex.Message)
                Throw New FaultException(Of ValidationFault)(fault, New FaultReason(Codes.WEB_EXPERIENCE__VALIDATION_ERROR))
            Catch ex As StoredProcedureGeneratedException
                Throw ex
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try

        End Function

#End Region
    End Class
End Namespace
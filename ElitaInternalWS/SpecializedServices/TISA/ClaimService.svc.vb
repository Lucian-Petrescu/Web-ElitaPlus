
' NOTE: You can use the "Rename" command on the context menu to change the class name "SNMPortalNew" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select SNMPortalNew.svc or SNMPortalNew.svc.vb at the Solution Explorer and start debugging.
Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccess
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.DALObjects
Imports businessObj = Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Namespace SpecializedServices.Tisa
    <ServiceBehavior(Namespace:="http://elita.assurant.com/SpecializedServices/TISA")>
    Public Class ClaimService
        Implements IClaimService
        Private Property CertificateManager As ICertificateManager
        Private Property ClaimManager As IClaimManager
        Private Property DealerManager As IDealerManager
        Private Property CommonManager As ICommonManager
        Private Property CountryManager As ICountryManager
        Private Property CompanyManager As ICompanyManager
        Private Property CompanyGroupManager As ICompanyGroupManager

        Public Sub New(ByVal pCertificateManager As ICertificateManager,
                       ByVal pDealerManager As IDealerManager,
                       ByVal pCommonManager As ICommonManager,
                       ByVal pClaimManager As IClaimManager,
                       ByVal pCountryManager As ICountryManager,
                       ByVal pCompanyManager As ICompanyManager,
                       ByVal pCompanyGroupManager As ICompanyGroupManager)

            If (pCertificateManager Is Nothing) Then
                Throw New ArgumentNullException("pCertificateManager")
            End If
            If (pDealerManager Is Nothing) Then
                Throw New ArgumentNullException("pDealerManager")
            End If
            If (pCommonManager Is Nothing) Then
                Throw New ArgumentNullException("pCommonManager")
            End If
            If (pClaimManager Is Nothing) Then
                Throw New ArgumentNullException("pClaimManager")
            End If
            If (pCountryManager Is Nothing) Then
                Throw New ArgumentNullException("pCountryManager")
            End If
            If (pCompanyManager Is Nothing) Then
                Throw New ArgumentNullException("pCompanyManager")
            End If
            If (pCompanyGroupManager Is Nothing) Then
                Throw New ArgumentNullException("pCompanyGroupManager")
            End If
            Me.CertificateManager = pCertificateManager
            Me.DealerManager = pDealerManager
            Me.CommonManager = pCommonManager
            Me.ClaimManager = pClaimManager
            Me.CountryManager = pCountryManager
            Me.CompanyManager = pCompanyManager
            Me.CompanyGroupManager = pCompanyGroupManager
        End Sub

        Public Sub DenyClaim(request As DenyClaimRequest) Implements IClaimService.DenyClaim
            request.Validate("request").HandleFault()
            Dim oCompany As Company
            Dim oRepairClaim As Claim
            Dim oCert As Certificate
            '''TODO : Remove the Fault Check condition once the ValidateUpdateAction is in place
            ''If request.UpdateAction = UpdateActionType.UnderMFW OrElse
            ''   request.UpdateAction = UpdateActionType.NoProblemFound Then
            ''    'If Diagnostic is Repaired or Denied, No replacement claim is expected
            Try
                oCompany = CompanyManager.GetCompany(request.CompanyCode)
                oRepairClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)

                oCert = CertificateManager.GetCertifcateByItemCoverage(oRepairClaim.CertItemCoverageId)

            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_COMPANY_NOT_FOUND,
                                                                                                          businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.CompanyCode)

            Catch cnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(),
                                                                businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REPAIR_CLAIM_NOT_FOUND,
                                                                                                          businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                                                                                                          " : " & request.ClaimNumber)
            End Try

            'Deny the Repair Claim
            Try

                Dim claimExtendedStatuses As New List(Of IClaimManager.ExtendedStatus)
                ValidateAndMapExtendedStatuses(oCompany, request.ExtendedStatuses, claimExtendedStatuses)

                Dim oBasicUpdateFields As New IClaimManager.ClaimBasicUpdateFields
                oBasicUpdateFields.ExtendedStatuses = claimExtendedStatuses

                ClaimManager.DenyClaim(oRepairClaim, oCert, oCompany, oBasicUpdateFields)

            Catch rplcf As ReplacementClaimFoundException
                Throw New FaultException(Of ReplacementClaimFoundFault)(New ReplacementClaimFoundFault(),
                                                    rplcf.Message)
            End Try

        End Sub

        Private Function HasCoverageChanged(ByVal pClaim As Claim, ByVal pCert As Certificate, pCoverageTypeCode As String) As Boolean

            Dim claimCertItemCovg As CertificateItemCoverage = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = pClaim.CertItemCoverageId).FirstOrDefault()

            If pCoverageTypeCode <> claimCertItemCovg.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType) Then

                'Raise Fault 'Invalid Change in Coverage Type' if the incoming Coverage Type is Theft (Covg Codes - T/T1/T2)
                If pCoverageTypeCode = CoverageTypeCodes.Theft OrElse
                   pCoverageTypeCode = CoverageTypeCodes.TheftLoss OrElse
                   pCoverageTypeCode = CoverageTypeCodes.TheftNoClaims Then

                    Throw New FaultException(Of InvalidChangeInCoverageFault)(New InvalidChangeInCoverageFault(),
                                                        businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_CHANGE_IN_COVERAGE_TYPE_CANNOT_BE_THEFT,
                                                                     businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " - " & pCoverageTypeCode)
                End If
                'Raise Fault 'Invalid Change in Coverage Type' if the Claim's original Method of Repair is Replacement
                If pClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair) = MethodofRepairCodes.Replacement Then
                    Throw New FaultException(Of InvalidChangeInCoverageFault)(New InvalidChangeInCoverageFault(),
                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_CHANGE_IN_COVERAGE_TYPE_ORIGINAL_METHOD_OF_REPAIR_IS_REPLACEMENT,
                                                                     businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End If

                'Raise Fault 'Invalid Change in Coverage Type' if the Coverage Type does Not exist on the Certificate 
                'Or if exists, that Coverage Is Not in effect on the Date of loss for the Claim
                If ClaimManager.IsCoverageChangeValid(pCert, pCoverageTypeCode, pClaim.LossDate) Then
                    HasCoverageChanged = True
                End If

            End If
        End Function

        Public Sub UpdateRepairableClaim(request As UpdateRepairableClaimRequest) Implements IClaimService.UpdateRepairableClaim

            'Dim isReplacementClaim As Boolean = False
            Dim isSvcWarrantyClaim As Boolean = False
            Dim bHasCoverageChanged As Boolean = False
            Dim bHasSvcCenterChanged As Boolean = False

            request.Validate("request").HandleFault()

            'Get the Claim that needs to be updated
            Dim oClaim As Claim
            Dim oCompany As Company
            Try
                oCompany = CompanyManager.GetCompany(request.CompanyCode)
                oClaim = Me.ClaimManager.GetClaim(request.ClaimNumber.ToUpperInvariant, oCompany.CompanyId)
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_COMPANY_NOT_FOUND,
                                                                     businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.CompanyCode)
            Catch clnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REPAIR_CLAIM_NOT_FOUND,
                                                                     businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ClaimNumber)

            End Try

            If (Not oClaim.RepairDate Is Nothing AndAlso (request.RepairDate < oClaim.LossDate OrElse request.RepairDate > DateTime.Today)) Then
                Throw New FaultException(Of InvalidRepairDateFault)(New InvalidRepairDateFault(),
                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_REPAIR_DATE,
                                                                     businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            End If

            ''Repair claim should be active
            If Not oClaim.ClaimNumber.ToUpper.EndsWith("S") Or Not oClaim.ClaimNumber.ToUpper.EndsWith("R") Then
                If (oClaim.StatusCode <> "A") Then
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(),
                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REPAIR_CLAIM_SHOULD_BE_ACTIVE,
                                                                     businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End If
            End If
            'Do we need to validate the incoming ServiceLevel ?  Are we doing it at the time of Opening the Claim?
            'elp_price_list_utility.validateservicelevel

            'If the incoming Request has Auth Amount value, do we update it or check for the least value with Price List

            'Change in Coverage : When the incoming Coverage Type is different than on the existing Claim
            Dim oCert As Certificate = Me.CertificateManager.GetCertifcateByItemCoverage(oClaim.CertItemCoverageId)
            bHasCoverageChanged = Me.HasCoverageChanged(oClaim, oCert, request.CoverageTypeCode)

            'Change the Service Center if there is a Valid Change! 
            Dim oClaimSvcCenter As ServiceCenter = Me.CountryManager.GetServiceCenterById(oCompany.BusinessCountryId, oClaim.ServiceCenterId)
            If request.ServiceCenterCode <> oClaimSvcCenter.CODE Then
                bHasSvcCenterChanged = True
                'validate service center code
                Try
                    Dim testSvc As ServiceCenter = Me.CountryManager.GetServiceCenterByCode(oCert.GetDealer(Me.DealerManager).GetCompany(Me.CompanyManager).BusinessCountryId, request.ServiceCenterCode)
                Catch svcnf As ServiceCenterNotFoundException
                    Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(),
                                                                            businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICE_CENTER_CODE,
                                                                            businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End Try
            End If

            If oClaim.ClaimNumber.ToUpper.EndsWith("S") Then
                isSvcWarrantyClaim = True
            End If

            'Update Basic Claim Fields
            Dim claimExtendedStatuses As New List(Of IClaimManager.ExtendedStatus)
            ValidateAndMapExtendedStatuses(oCompany, request.ExtendedStatuses, claimExtendedStatuses)

            Dim oBasicUpdateFields As New IClaimManager.ClaimBasicUpdateFields
            oBasicUpdateFields.AuthNumber = request.AuthorizationNumber
            oBasicUpdateFields.ProblemDescription = request.ProblemDescription
            oBasicUpdateFields.RepairDate = request.RepairDate
            oBasicUpdateFields.Specialnstructions = request.SpecialInstructions
            oBasicUpdateFields.TechnicalReport = request.TechnicalReport
            oBasicUpdateFields.ExtendedStatuses = claimExtendedStatuses

            'After all the Validations call the UpdateRepairableClaim Function 
            Try
                Me.ClaimManager.UpdateRepairableClaim(oClaim, oCert, oCompany, request.CoverageTypeCode,
                                                 request.ServiceCenterCode, request.ServiceLevel,
                                                 bHasCoverageChanged, bHasSvcCenterChanged,
                                                 isSvcWarrantyClaim,
                                                 request.Manufacturer, request.Model, request.SerialNumber,
                                                 request.AuthorizedAmount, oBasicUpdateFields)

                'If Diagnostic is Repaired (All valid update Actions of this Operation) or Denied, No Replacement Claim is expected
            Catch rplcf As ReplacementClaimFoundException
                Throw New FaultException(Of ReplacementClaimFoundFault)(New ReplacementClaimFoundFault(),
                                                    rplcf.Message)
            Catch InvSvcL As InvalidServiceLevelException
                Throw New FaultException(Of InvalidServiceLevelFault)(New InvalidServiceLevelFault(),
                                                                        businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICELEVEL,
                                                                            businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ServiceLevel)


            Catch plnc As PriceListNotConfiguredException
                Throw New FaultException(Of PriceListNotConfiguredFault)(New PriceListNotConfiguredFault(), plnc.Message)
            End Try

        End Sub

        Private Sub PopulateIrreparableClaim(ByVal pRequest As UpdateClaimReplacedWithRefubishedRequest,
                                             ByRef pRepairClaim As Claim,
                                             ByRef pCompany As Company,
                                             ByRef pCert As Certificate,
                                             ByRef pLossType As String,
                                             ByRef pHasCoverageChanged As Boolean,
                                             ByRef pHasSvcCenterChanged As Boolean,
                                             ByRef pIsSvcWarrantyClaim As Boolean,
                                             ByRef pClaimExtendedStatuses As List(Of IClaimManager.ExtendedStatus),
                                             ByRef pBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields
                                             )

            pCompany = Me.CompanyManager.GetCompany(pRequest.CompanyCode)

            Try
                pRepairClaim = Me.ClaimManager.GetClaim(pRequest.ClaimNumber, pCompany.CompanyId)
            Catch cnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(),
                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REPAIR_CLAIM_NOT_FOUND,
                                                                            businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & pRequest.ClaimNumber)

            End Try

            ''c.Repair Claim Is Not active
            If Not pRepairClaim.ClaimNumber.ToUpper.EndsWith("S") Or Not pRepairClaim.ClaimNumber.ToUpper.EndsWith("R") Then
                If pRepairClaim.StatusCode <> "A" Then
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(),
                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REPAIR_CLAIM_SHOULD_BE_ACTIVE,
                                                                            businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & pRequest.ClaimNumber)
                End If
            End If

            If pRepairClaim.ClaimNumber.ToUpper.EndsWith("S") Then
                pIsSvcWarrantyClaim = True
            End If

            pCert = Me.CertificateManager.GetCertifcateByItemCoverage(pRepairClaim.CertItemCoverageId)
            '''''if Coveragetypecode is supplied , check it otherwise default to false "coverage has not changed"
            If Not pRequest.CoverageTypeCode Is Nothing Then
                pHasCoverageChanged = Me.HasCoverageChanged(pRepairClaim, pCert, pRequest.CoverageTypeCode)
            Else
                pHasCoverageChanged = False
            End If
            'Change the Service Center if there is a Valid Change! 
            Dim oClaimSvcCenter As ServiceCenter = Me.CountryManager.GetServiceCenterById(pCompany.BusinessCountryId, pRepairClaim.ServiceCenterId)
            'validate service center code if sent in request.
            If (Not pRequest.ServiceCenterCode Is Nothing) Then
                If pRequest.ServiceCenterCode <> oClaimSvcCenter.CODE Then
                    pHasSvcCenterChanged = True

                    Try

                        If (Me.CountryManager.GetServiceCenterByCode(pCert.GetDealer(Me.DealerManager).GetCompany(Me.CompanyManager).BusinessCountryId, pRequest.ServiceCenterCode) Is Nothing) Then
                            Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(),
                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICE_CENTER_CODE,
                                                                            businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & pRequest.ServiceCenterCode)

                        End If

                    Catch ex As Exception
                        Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(),
                                                                        businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICE_CENTER_CODE,
                                                                            businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & pRequest.ServiceCenterCode)
                    End Try

                End If
            End If
            ValidateAndMapExtendedStatuses(pCompany, pRequest.ExtendedStatuses, pClaimExtendedStatuses)

            pBasicUpdateFields.AuthNumber = pRequest.AuthorizationNumber
            pBasicUpdateFields.ProblemDescription = pRequest.ProblemDescription
            pBasicUpdateFields.RepairDate = pRequest.RepairDate
            pBasicUpdateFields.Specialnstructions = pRequest.SpecialInstructions
            pBasicUpdateFields.TechnicalReport = pRequest.TechnicalReport
            pBasicUpdateFields.ExtendedStatuses = pClaimExtendedStatuses

            If pRequest.LossType = LossTypeEnum.TotalLoss Then
                pLossType = LossTypeCodes.TotalLoss
            ElseIf pRequest.LossType = LossTypeEnum.PartialLoss Then
                pLossType = LossTypeCodes.PartialLoss
            End If

        End Sub


        Public Sub UpdateIrrepairableWithReplacement(ByVal request As UpdateClaimReplacedWithRefubishedRequest) Implements IClaimService.UpdateIrrepairableWithReplacement

            Dim oCompany As Company
            Dim oRepairClaim As Claim
            Dim oCert As Certificate
            Dim bHasCoverageChanged As Boolean = False
            Dim bHasSvcCenterChanged As Boolean = False
            Dim isSvcWarrantyClaim As Boolean = False
            Dim claimExtendedStatuses As New List(Of IClaimManager.ExtendedStatus)
            Dim oBasicUpdateFields As New IClaimManager.ClaimBasicUpdateFields
            Dim lossType As String

            request.Validate("request").HandleFault()

            ''''DEF-27227---REQ-5753-Update Claim-Refurbished-Zero cost device record is accepted
            'Expected behaviour as per Jane
            'Update the Replacement Claim as below
            ''''''Claim Authorized Amount
            ''''''If no Then authorized amount Is provided (blank Or zero) And Device type indicated Is Refurbished, 
            ''''''System shall Not update the claim And shall Return an Error message As response To the portal "Refurbished cost is required for Refurbished device";
            'Otherwise, System shall calculate it based on the claims method of repair
            If (request.Condition = DeviceConditionEnum.Refurbished) Then
                If (request.AuthorizedAmount Is Nothing Or (Not request.AuthorizedAmount Is Nothing AndAlso request.AuthorizedAmount = 0)) Then
                    Throw New FaultException(Of RefurbishedCostRequiredFault)(New RefurbishedCostRequiredFault(),
                                                                              businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REFURBISHED_COST_IS_REQUIRED_FOR_REFURBISHED_DEVICE,
                                                                                businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End If
            End If

            PopulateIrreparableClaim(request, oRepairClaim, oCompany, oCert, lossType,
                                     bHasCoverageChanged, bHasSvcCenterChanged, isSvcWarrantyClaim,
                                     claimExtendedStatuses, oBasicUpdateFields)

            If (Not oRepairClaim.RepairDate Is Nothing AndAlso (request.RepairDate < oRepairClaim.LossDate OrElse request.RepairDate > DateTime.Today)) Then
                Throw New FaultException(Of InvalidRepairDateFault)(New InvalidRepairDateFault(),
                                                                        businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_REPAIR_DATE,
                                                                                businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            Try
                'After all the Validations call the UpdateClaimReplacedWithRefurbished Function 
                Me.ClaimManager.UpdateClaimReplacedWithRefubished(oRepairClaim, oCert, oCompany, request.CoverageTypeCode,
                                                        request.ServiceCenterCode, request.ServiceLevel, bHasCoverageChanged,
                                                        bHasSvcCenterChanged, isSvcWarrantyClaim, request.AuthorizedAmount, If(request.SimCardAmount Is Nothing, 0, request.SimCardAmount),
                                                        request.SerialNumber, request.Model, request.Manufacturer, request.UpdateAction,
                                                        CType(request.Condition, IClaimManager.DeviceConditionEnum),
                                                        lossType, oBasicUpdateFields)
            Catch rplcf As ReplacementClaimFoundException
                Throw New FaultException(Of ReplacementClaimFoundFault)(New ReplacementClaimFoundFault(),
                                                    rplcf.Message)
            Catch InvSvcL As InvalidServiceLevelException
                Throw New FaultException(Of InvalidServiceLevelFault)(New InvalidServiceLevelFault(),
                                                                      businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICELEVEL,
                                                                                businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ServiceLevel)
            Catch mnf As ManufacturerNotFoundException
                Throw New FaultException(Of ManufacturerNotFoundFault)(New ManufacturerNotFoundFault(),
                                                                       businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_MANUFACTURER,
                                                                                businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.Manufacturer)
            Catch plnc As PriceListNotConfiguredException
                Throw New FaultException(Of PriceListNotConfiguredFault)(New PriceListNotConfiguredFault, If(plnc.Message Is Nothing,
                                                                         businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_PRICE_LIST_NOT_CONFIGURED_FOR_SERVICE_CENTER,
                                                                                businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " + request.ServiceCenterCode, plnc.Message))
            End Try

        End Sub

        'Public Sub UpdateIrrepairableClaim(ByVal request As UpdateIrrepairableClaimRequest) Implements IClaimService.UpdateIrrepairableClaim

        '    'Get the Claim that needs to be updated
        '    Dim oCompany As Company
        '    Dim oRepairClaim As Claim
        '    Dim oCert As Certificate
        '    Dim bHasCoverageChanged As Boolean = False
        '    Dim bHasSvcCenterChanged As Boolean = False
        '    Dim isSvcWarrantyClaim As Boolean = False
        '    Dim claimExtendedStatuses As New List(Of IClaimManager.ExtendedStatus)
        '    Dim oBasicUpdateFields As New IClaimManager.ClaimBasicUpdateFields
        '    Dim lossType As String

        '    request.Validate("request").HandleFault()

        '    PopulateIrreparableClaim(request, oRepairClaim, oCompany, oCert, lossType,
        '                             bHasCoverageChanged, bHasSvcCenterChanged, isSvcWarrantyClaim,
        '                             claimExtendedStatuses, oBasicUpdateFields)

        '    Try
        '        '''''For New Device, Authorization amount is not mandatory
        '        If (request.AuthorizedAmount Is Nothing) Then
        '            request.AuthorizedAmount = 0D
        '        End If
        '        'After all the Validations call the UpdateRepairableClaim Function 
        '        Me.ClaimManager.UpdateIrRepairableClaim(oRepairClaim, oCert, oCompany, request.CoverageTypeCode,
        '                                                request.ServiceCenterCode, request.ServiceLevel, bHasCoverageChanged,
        '                                                bHasSvcCenterChanged, isSvcWarrantyClaim, request.AuthorizedAmount, request.SimCardAmount,
        '                                                request.SerialNumber, request.Model, request.Manufacturer,
        '                                                CType(request.Condition, IClaimManager.DeviceConditionEnum),
        '                                                lossType, oBasicUpdateFields)

        '    Catch InvSvcL As InvalidServiceLevelException
        '        Throw New FaultException(Of InvalidServiceLevelFault)(New InvalidServiceLevelFault(), "Invalid ServiceLevel : " & request.ServiceLevel)
        '    Catch InvManfExep As ManufacturerNotFoundException
        '        Throw New FaultException(Of ManufacturerNotFoundFault)(New ManufacturerNotFoundFault(), "Invalid Manufacturer")
        '    End Try


        'End Sub

        Private Sub ValidateAndMapExtendedStatuses(ByVal pCompany As Company,
                                                   ByVal pReqExtStatuses As List(Of ExtendedStatus),
                                                   ByRef pClaimExtendedStatuses As List(Of IClaimManager.ExtendedStatus))
            Dim extStatus As ExtendedStatus
            Dim extStatId As Guid
            Dim oCompanyGroup As CompanyGroup
            oCompanyGroup = CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId)

            If Not pReqExtStatuses Is Nothing Then
                For Each extStatus In pReqExtStatuses

                    If CommonManager.GetListItems(ListCodes.ClaimExtendedStatusCode).Where(Function(li) li.Code = extStatus.Code).Count > 0 Then

                        extStatId = CommonManager.GetListItems(ListCodes.ClaimExtendedStatusCode).Where(Function(li) li.Code = extStatus.Code).FirstOrDefault.ListItemId

                        If oCompanyGroup.ClaimStatusByGroups.Where(Function(csg) csg.ListItemId = extStatId).Count > 0 Then
                            'Do Nothing when the Code is Valid
                        Else
                            Throw New FaultException(Of InvalidExtendedStatusFault)(New InvalidExtendedStatusFault(),
                                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_EXTENDED_STATUS_FAULT,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & extStatus.Code)
                        End If
                    Else
                        Throw New FaultException(Of InvalidExtendedStatusFault)(New InvalidExtendedStatusFault(),
                                                                                businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_EXTENDED_STATUS_FAULT,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & extStatus.Code)
                    End If


                    'Do the Mapping to populate the Internal List of Extended Status
                    pClaimExtendedStatuses.Add(New IClaimManager.ExtendedStatus() With
                        {
                            .Code = extStatus.Code,
                            .StatusDate = extStatus.StatusDate
                        })
                Next
            End If
        End Sub
        'Do we need to validate the Make,Model  - against the Warranty Master  Yes!
        'And Serial Number in case of Replacement Claims?
        'If So, what validations..?

        'TODO : Check below when its needed or Not
        '''''Replacement Claim Not Found, Update the Repair Claim
        '''''System should only update the Basic Fields, Authorized amount should not be updated
        '''''Me.ClaimManager.UpdateBasicClaimFields(oClaim)
        Public Function CreateRepairReplacementClaim(request As CreateRepairReplacementClaimRequest) As CreateClaimResponse Implements IClaimService.CreateRepairReplacementClaim
            Dim response As New CreateClaimResponse
            request.Validate("request").HandleFault()

            Dim oCompany As Company = CompanyManager.GetCompany(request.CompanyCode)

            If (oCompany Is Nothing) Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault,
                                                                  businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_COMPANY_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If


            ''''Get the Repair claim information
            Dim oRepairClaim As Claim
            Try
                If (request.ClaimNumber.ToUpperInvariant.EndsWith("R") Or request.ClaimNumber.ToUpperInvariant.EndsWith("S")) Then
                    request.ClaimNumber = request.ClaimNumber.Substring(0, request.ClaimNumber.Length - 1)
                End If
                oRepairClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)

            Catch ex As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault,
                                                                businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_CLAIM_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try


            ''''Get certificate based on the coverage Id
            Dim oCert As Certificate = CertificateManager.GetCertifcateByItemCoverage(oRepairClaim.CertItemCoverageId)
            Dim oReplacementClaim As Claim

            If (oCert Is Nothing) Then
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(),
                                                                      businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_CERTIFICATE_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            oReplacementClaim = ClaimManager.CreateRepairReplacmentClaim(oCert, oRepairClaim)

            If (Not request.ExtendedStatuses Is Nothing AndAlso request.ExtendedStatuses.Count > 0) Then
                For Each es As ExtendedStatus In request.ExtendedStatuses
                    ClaimManager.InsertClaimExtendedStatus(CertificateManager.GetCertifcateByItemCoverage(oRepairClaim.CertItemCoverageId).GetDealer(DealerManager),
                                                           es.Code, es.StatusDate, oReplacementClaim)
                Next

            End If

            ''''Save the replacement claim and send back the claim number
            ClaimManager.SaveClaim(oReplacementClaim)
            response.ClaimNumber = oReplacementClaim.ClaimNumber

            Return response
        End Function

        Public Function CreateServiceWarrantyClaim(request As CreateServiceWarrantyClaimRequest) As CreateClaimResponse Implements IClaimService.CreateServiceWarrantyClaim
            Dim response As New CreateClaimResponse
            request.Validate("request").HandleFault()

            Dim oCompany As Company = CompanyManager.GetCompany(request.CompanyCode)

            If (oCompany Is Nothing) Then
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault,
                                                                  businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_COMPANY_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            ''''Get the Repair claim information
            Dim oRepairClaim As Claim
            Try
                If (request.ClaimNumber.ToUpperInvariant.EndsWith("R") Or request.ClaimNumber.ToUpperInvariant.EndsWith("S")) Then
                    request.ClaimNumber = request.ClaimNumber.Substring(0, request.ClaimNumber.Length - 1)
                End If
                oRepairClaim = ClaimManager.GetClaim(request.ClaimNumber, oCompany.CompanyId)

            Catch ex As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault,
                                                                businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_CLAIM_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            ''''check whether the repair date is available in the Repair claim
            If (oRepairClaim.RepairDate Is Nothing) Then
                Throw New FaultException(Of RepairClaimNotFulfilledFault)(New RepairClaimNotFulfilledFault,
                                                                          businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REPAIR_CLAIM_NOT_FULFILLED,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            ''''Get certificate based on the coverage Id
            Dim oCert As Certificate = CertificateManager.GetCertifcateByItemCoverage(oRepairClaim.CertItemCoverageId)
            If (oCert Is Nothing) Then
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(),
                                                                      businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_CERTIFICATE_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            Dim oServiceWarrantyClaim As New Claim
            oServiceWarrantyClaim = ClaimManager.CreateServiceWarrantyClaim(oCert, oRepairClaim)

            ''''Save the replacement claim and send back the claim number
            ClaimManager.SaveClaim(oServiceWarrantyClaim)
            response.ClaimNumber = oServiceWarrantyClaim.ClaimNumber

            Return response
        End Function

        Public Function CreateClaim(request As CreateClaimRequest) As CreateClaimResponse Implements IClaimService.CreateClaim
            Dim response As New CreateClaimResponse

            request.Validate("request").HandleFault()

            '''''check the incoming claim type
            If (request.ClaimType <> ClaimTypeCodes.Repair AndAlso request.ClaimType <> ClaimTypeCodes.OriginalReplacement) Then
                Throw New FaultException(Of InvalidClaimTypeFault)(New InvalidClaimTypeFault(),
                                                                   businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_CLAIM_TYPE,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            ''''Locate/Validate Dealer
            Dim oDealer As Dealer
            Dim oCompany As Company
            Try
                oDealer = DealerManager.GetDealer(request.DealerCode)
                oCompany = oDealer.GetCompany(CompanyManager)
            Catch dnfe As DealerNotFoundException
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(),
                                                                 businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_DEALER_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            ''''Locate Certificate
            Dim oCert As Certificate = Me.CertificateManager.GetCertificate(request.DealerCode, request.CertificateNumber)
            If (oCert Is Nothing) Then
                Throw New FaultException(Of CertificateNotFoundFault)(New CertificateNotFoundFault(),
                                                                      businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_CERTIFICATE_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            '''Locate Coverage
            Dim cic As CertificateItemCoverage
            Try
                cic = Me.ClaimManager.LocateCoverage(oCert, request.DateOfLoss, request.CoverageTypeCode, Nothing)
            Catch cnf As CoverageNotFoundException
                Throw New FaultException(Of CoverageNotFoundFault)(New CoverageNotFoundFault(),
                                                                   businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_COVERAGE_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            Catch mcf As MultipleCoverageFoundException
                Throw New FaultException(Of MultipleCoveragesFoundFault)(New MultipleCoveragesFoundFault(),
                                                                         businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_MULTIPLE_COVERAGE_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            '''''validate incoming coverage type with the found coverage
            If (request.CoverageTypeCode <> cic.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType)) Then
                Throw New FaultException(Of CoverageNotFoundFault)(New CoverageNotFoundFault(),
                                                                   businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_COVERAGE_CODE,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            '''''if thef/loss coverage found and claim type in request is Repair
            If ((request.CoverageTypeCode = CoverageTypeCodes.Theft OrElse
            request.CoverageTypeCode = CoverageTypeCodes.TheftLoss OrElse
                    request.CoverageTypeCode = CoverageTypeCodes.Loss) And request.ClaimType = ClaimTypeCodes.Repair) Then
                Throw New FaultException(Of CoverageNotFoundFault)(New CoverageNotFoundFault(),
                                                                   businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_COVERAGE_CODE,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If
            '''''if thef/loss coverage found and claim type in request is Repair
            If ((request.CoverageTypeCode = CoverageTypeCodes.Theft OrElse
            request.CoverageTypeCode = CoverageTypeCodes.TheftLoss OrElse
                    request.CoverageTypeCode = CoverageTypeCodes.Loss) And request.ClaimType = ClaimTypeCodes.ServiceWarranty) Then
                Throw New FaultException(Of CoverageNotFoundFault)(New CoverageNotFoundFault(),
                                                                   businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_COVERAGE_CODE,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            ''''validate Date of Loss
            If (request.DateOfLoss > DateTime.Today OrElse request.DateOfLoss < cic.BeginDate OrElse
                request.DateOfLoss > cic.EndDate) Then
                Throw New FaultException(Of InvalidDateOfLossFault)(New InvalidDateOfLossFault(),
                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_DATE_OF_LOSS,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End If

            ''''validate service center
            Try
                If (Me.CountryManager.GetServiceCenterByCode(oDealer.GetCompany(Me.CompanyManager).BusinessCountryId, request.ServiceCenterCode) Is Nothing) Then
                    Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(),
                                                                            businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_SERVICE_CENTER_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End If
            Catch ex As Exception
                Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(),
                                                                        businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_SERVICE_CENTER_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            End Try

            ''''''Extended statuses
            Dim oClaimExtendedStatuses As New List(Of IClaimManager.ExtendedStatus)
            ValidateAndMapExtendedStatuses(oCompany, request.ExtendedStatuses, oClaimExtendedStatuses)

            ''''Create Claim Shell
            Dim oClaim As Claim = New Claim()
            oClaim.ClaimId = Guid.NewGuid()

            oClaim.LossDate = request.DateOfLoss

            If (request.ContactName = String.Empty) Then
                oClaim.ContactName = oCert.CustomerName
                oClaim.CallerName = oCert.CustomerName
            Else
                oClaim.ContactName = request.ContactName
                oClaim.CallerName = request.ContactName
            End If

            ExtractWorkOrder(request)

            oClaim.RemAuthNumber = request.WorkOrderNumber
            oClaim.ProblemDescription = request.ProblemDescription


            Try
                oClaim = Me.ClaimManager.CreateClaim(cic,
                                                     oClaim,
                                                     oDealer,
                                                     request.CoverageTypeCode,
                                                     request.ServiceCenterCode,
                                                     request.ClaimType,
                                                     request.Make,
                                                     request.Model)
            Catch ex As PriceListNotConfiguredException
                Throw New FaultException(Of PriceListNotConfiguredFault)(New PriceListNotConfiguredFault,
                                                                         businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_PRICE_LIST_NOT_CONFIGURED_FOR_SERVICE_CENTER,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " + request.ServiceCenterCode)

            Catch mnf As CertificateItemNotFoundException
                Throw New FaultException(Of MakeAndModelNotFoundFault)(New MakeAndModelNotFoundFault(),
                                                                       businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_MAKE_IS_REQUIRED,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            Catch mnf As ManufacturerNotFoundException
                Throw New FaultException(Of ManufacturerNotFoundFault)(New ManufacturerNotFoundFault(),
                                                                      businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_MANUFACTURER,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " + request.Make)
            End Try

            If (oClaimExtendedStatuses.Count > 0) Then
                For Each es As ExtendedStatus In request.ExtendedStatuses
                    ClaimManager.InsertClaimExtendedStatus(oDealer, es.Code, es.StatusDate, oClaim)
                Next
            Else
                Try
                    ClaimManager.AddDefaultExtendedStatus(oClaim, oDealer, ClaimActions.NewClaim)
                Catch ex As Exception
                    Throw New FaultException(Of DefaultExtendedStatusNotFoundFault)(New DefaultExtendedStatusNotFoundFault,
                                                                                   businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_DEFAULT_EXTENDED_STATUS_NOT_FOUND,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End Try

            End If

            oClaim = ClaimManager.SaveClaim(oClaim)
            response.ClaimNumber = oClaim.ClaimNumber

            'raise a harvester event for damage/repair claim for SLA monitoring
            With oClaim
                businessObj.PublishedTask.AddEvent(companyGroupId:=Nothing,
                                                           companyId:=Nothing,
                                                           countryId:=Nothing,
                                                           dealerId:=oDealer.DealerId,
                                                           productCode:=Nothing,
                                                           coverageTypeId:=Nothing,
                                                           sender:="CreateClaim Web Service",
                                                           arguments:="ClaimId:" & DALBase.GuidToSQLString(.ClaimId),
                                                           eventDate:=DateTime.UtcNow,
                                                           eventTypeId:=businessObj.LookupListNew.GetIdFromCode(businessObj.Codes.EVNT_TYP, businessObj.Codes.EVNT_TYP__CLM_EXT_STATUS),
                                                           eventArgumentId:=businessObj.LookupListNew.GetExtendedStatusByGroupId(businessObj.ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId, businessObj.LookupListNew.GetIdFromCode(businessObj.Codes.EXTENDED_CLAIM_STATUSES, businessObj.Codes.CLM_STAT__COM_FB_CLM_UPD_DMG)))
            End With

            'raise a harvester event if invoke external service attribute is set on the dealer
            Dim eventTypeID As Guid

            If oClaim.StatusCode = ClaimStatusCodes.Active Then
                eventTypeID = businessObj.LookupListNew.GetIdFromCode(businessObj.Codes.EVNT_TYP, businessObj.Codes.EVNT_TYP__CLAIM_APPROVED)
            ElseIf oClaim.StatusCode = ClaimStatusCodes.Denied Then
                eventTypeID = businessObj.LookupListNew.GetIdFromCode(businessObj.Codes.EVNT_TYP, businessObj.Codes.EVNT_TYP__CLAIM_DENIED)
            End If

            Try
                businessObj.PublishedTask.AddEvent(
                                      companyGroupId:=oCompany.CompanyGroupId,
                                      companyId:=oCompany.CompanyId,
                                      countryId:=oCompany.CountryId,
                                      dealerId:=oDealer.DealerId,
                                      productCode:=oCert.ProductCode,
                                      coverageTypeId:=cic.CoverageTypeId,
                                      sender:="TISA Claim Service",
                                      arguments:="ClaimId:" & DALBase.GuidToSQLString(oClaim.ClaimId),
                                      eventDate:=DateTime.UtcNow,
                                      eventTypeId:=eventTypeID,
                                      eventArgumentId:=Guid.Empty)
            Catch ex As Exception


            End Try


            Return response
        End Function

        Private Shared Sub ExtractWorkOrder(request As CreateClaimRequest)

            Const sepparator As String = "|"
            Const falabellaCode As String = "FLGC"

            If request.DealerCode = falabellaCode Then

                '''logic to sepparate the work order number from the problem description in Falabella 
                If (request.WorkOrderNumber Is Nothing Or String.IsNullOrEmpty(request.WorkOrderNumber)) AndAlso
                   request.ProblemDescription.Contains(sepparator) Then
                    Dim workOrderAndProblem = request.ProblemDescription.Split(sepparator)
                    request.WorkOrderNumber = workOrderAndProblem(0)
                    request.ProblemDescription = workOrderAndProblem(1)
                End If

            End If
        End Sub

        Public Sub UpdateTheftClaim(request As UpdateTheftClaimRequest) Implements IClaimService.UpdateTheftClaim

            'Get the Claim that needs to be updated
            Dim oCompany As Company
            Dim oReplacementClaim As Claim
            Dim bHasCoverageChanged As Boolean = False
            Dim bHasSvcCenterChanged As Boolean = False
            Dim oCert As Certificate
            Dim claimExtendedStatuses As New List(Of IClaimManager.ExtendedStatus)
            Dim oBasicUpdateFields As New IClaimManager.ClaimBasicUpdateFields
            Dim lossType As String

            request.Validate("request").HandleFault()

            PopulateTheftClaim(request, request.Manufacturer, request.Model, oReplacementClaim, oCompany, oCert, lossType,
                                     bHasCoverageChanged, bHasSvcCenterChanged,
                                     claimExtendedStatuses, oBasicUpdateFields)


            Try

                '''''For refurbished Device, Authorization amount is not mandatory
                If (request.Condition = DeviceConditionEnum.Refurbished) Then
                    If (request.AuthorizedAmount Is Nothing Or (Not request.AuthorizedAmount Is Nothing AndAlso request.AuthorizedAmount = 0)) Then
                        Throw New FaultException(Of RefurbishedCostRequiredFault)(New RefurbishedCostRequiredFault(),
                                                                                  businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REFURBISHED_COST_IS_REQUIRED_FOR_REFURBISHED_DEVICE,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    End If
                End If

                If (Not oReplacementClaim.RepairDate Is Nothing AndAlso (request.RepairDate < oReplacementClaim.LossDate OrElse request.RepairDate > DateTime.Today)) Then
                    Throw New FaultException(Of InvalidRepairDateFault)(New InvalidRepairDateFault(),
                                                                        businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_REPAIR_DATE,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                End If

                'After all the Validations call the UpdateRepairableClaim Function 
                Me.ClaimManager.UpdateTheftClaim(oReplacementClaim, oCert, oCompany, bHasSvcCenterChanged, request.CoverageTypeCode,
                                                        request.ServiceCenterCode, request.ServiceLevel,
                                                         request.AuthorizedAmount,
                                                        request.SerialNumber, request.Model, request.Manufacturer,
                                                        CType(request.Condition, IClaimManager.DeviceConditionEnum),
                                                        lossType, request.SimCardAmount, oBasicUpdateFields)

            Catch InvSvcL As InvalidServiceLevelException
                Throw New FaultException(Of InvalidServiceLevelFault)(New InvalidServiceLevelFault(),
                                                                      businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICELEVEL,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & request.ServiceLevel)
            Catch InvManfExep As ManufacturerNotFoundException
                Throw New FaultException(Of ManufacturerNotFoundFault)(New ManufacturerNotFoundFault(),
                                                                       businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_MANUFACTURER,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            Catch plnc As PriceListNotConfiguredException
                Throw New FaultException(Of PriceListNotConfiguredFault)(New PriceListNotConfiguredFault, If(plnc.Message Is Nothing,
                                                                         businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_PRICE_LIST_NOT_CONFIGURED_FOR_SERVICE_CENTER,
                                                                                        businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " + request.ServiceCenterCode, plnc.Message))
            End Try
        End Sub

        Private Sub PopulateTheftClaim(ByVal pRequest As UpdateTheftClaimRequest,
                                       ByVal pMake As String,
                                       ByVal pModel As String,
                                            ByRef pReplacementClaim As Claim,
                                            ByRef pCompany As Company,
                                            ByRef pCert As Certificate,
                                            ByRef pLossType As String,
                                            ByRef pHasCoverageChanged As Boolean,
                                            ByRef pHasSvcCenterChanged As Boolean,
                                            ByRef pClaimExtendedStatuses As List(Of IClaimManager.ExtendedStatus),
                                            ByRef pBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields
                                            )

            pCompany = Me.CompanyManager.GetCompany(pRequest.CompanyCode)

            Try
                pReplacementClaim = Me.ClaimManager.GetClaim(pRequest.ClaimNumber, pCompany.CompanyId)
            Catch cnf As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(),
                                                                businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_REPLACEMENT_CLAIM_NOT_FOUND,
                                                                  businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & pRequest.ClaimNumber)

            End Try


            pCert = Me.CertificateManager.GetCertifcateByItemCoverage(pReplacementClaim.CertItemCoverageId)
            '''''if Coveragetypecode is supplied , check it otherwise default to false "coverage has not changed"
            If Not pRequest.CoverageTypeCode Is Nothing Then
                pHasCoverageChanged = Me.HasCoverageChanged(pReplacementClaim, pCert, pRequest.CoverageTypeCode)
            Else
                pHasCoverageChanged = False
            End If
            'Change the Service Center if there is a Valid Change! 
            Dim oClaimSvcCenter As ServiceCenter = Me.CountryManager.GetServiceCenterById(pCompany.BusinessCountryId, pReplacementClaim.ServiceCenterId)
            'validate service center code if sent in request.
            If (Not pRequest.ServiceCenterCode Is Nothing) Then
                If pRequest.ServiceCenterCode <> oClaimSvcCenter.CODE Then
                    pHasSvcCenterChanged = True

                    Try

                        If (Me.CountryManager.GetServiceCenterByCode(pCert.GetDealer(Me.DealerManager).GetCompany(Me.CompanyManager).BusinessCountryId, pRequest.ServiceCenterCode) Is Nothing) Then
                            Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(),
                                                                                    businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICE_CENTER_CODE,
                                                                                    businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & pRequest.ServiceCenterCode)
                        End If

                    Catch ex As Exception
                        Throw New FaultException(Of ServiceCenterNotFoundFault)(New ServiceCenterNotFoundFault(),
                                                                                businessObj.TranslationBase.TranslateLabelOrMessage(ErrorCodes.ERR_INVALID_SERVICE_CENTER_CODE,
                                                                                businessObj.ElitaPlusIdentity.Current.ActiveUser.LanguageId) & " : " & pRequest.ServiceCenterCode)
                    End Try

                End If
            End If
            ValidateAndMapExtendedStatuses(pCompany, pRequest.ExtendedStatuses, pClaimExtendedStatuses)

            pBasicUpdateFields.AuthNumber = pRequest.AuthorizationNumber
            pBasicUpdateFields.ProblemDescription = pRequest.ProblemDescription
            pBasicUpdateFields.RepairDate = pRequest.RepairDate
            pBasicUpdateFields.Specialnstructions = pRequest.SpecialInstructions
            pBasicUpdateFields.TechnicalReport = pRequest.TechnicalReport
            pBasicUpdateFields.ExtendedStatuses = pClaimExtendedStatuses
            pBasicUpdateFields.Make = pMake
            pBasicUpdateFields.Model = pModel


            If pRequest.LossType = LossTypeEnum.TotalLoss Then
                pLossType = LossTypeCodes.TotalLoss
            ElseIf pRequest.LossType = LossTypeEnum.PartialLoss Then
                pLossType = LossTypeCodes.PartialLoss
            End If

        End Sub
    End Class
End Namespace
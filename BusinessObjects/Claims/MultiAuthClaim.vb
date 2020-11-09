Imports System.Collections.Generic
Imports System.Net
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.BusinessObjectsNew.ClaimFulfillmentWebAppGatewayService
Imports Assurant.ElitaPlus.BusinessObjectsNew.LegacyBridgeService

Public NotInheritable Class MultiAuthClaim
    Inherits ClaimBase : Implements IFullfillable

#Region "Constructors"
    'New BO
    Friend Sub New()
        MyBase.New()
    End Sub

    'New BO attaching to a BO family
    Friend Sub New(ByVal familyDS As DataSet)
        MyBase.New(familyDS)
    End Sub


    'Existing BO
    Friend Sub New(ByVal id As Guid)
        MyBase.New(id)
    End Sub

    'Existing BO attaching to a BO family
    Friend Sub New(ByVal id As Guid, ByVal familyDS As DataSet, Optional ByVal blnMustReload As Boolean = False)
        MyBase.New(id, familyDS, blnMustReload)
    End Sub

    Friend Sub New(ByVal row As DataRow)
        MyBase.New(row)
    End Sub

    Friend Sub New(ByVal claimNumber As String, ByVal companyId As Guid)
        MyBase.New(claimNumber, companyId)
    End Sub

#End Region

#Region "Claim Authorization"
    Public ReadOnly Property ClaimAuthorizationChildren() As ClaimAuthorizationList
        Get
            Return New ClaimAuthorizationList(Me)
        End Get
    End Property
    Public ReadOnly Property NonVoidClaimAuthorizationList() As IEnumerable(Of ClaimAuthorization)
        Get
            Return ClaimAuthorizationChildren.Where(Function(item) Not item.ClaimAuthStatus = ClaimAuthorizationStatus.Void)
        End Get
    End Property
#End Region

#Region "Claim Issues"
    Public Overrides Function CanIssuesReopen() As Boolean
        Dim flag As Boolean = False
        If (Me.ClaimAuthorizationChildren.Count = 0) Then Return True

        If (Me.HasActiveAuthorizations) Then
            Dim claimNumber As String = If(Me.ClaimNumber = String.Empty, "0", Me.ClaimNumber)
            If (ClaimInvoice.getPaymentsList(Me.CompanyId, claimNumber).Count = 0) Then
                If (Me.Status = BasicClaimStatus.Active Or Me.Status = BasicClaimStatus.Pending Or Me.Status = BasicClaimStatus.Denied) Then
                    flag = True
                End If
            End If
        End If
        Return flag
    End Function

#End Region

#Region "Instance Fields"
    Private _repairDate As DateType
    Private _invoiceProcessDate As DateType
    Private _pickUpDate As DateType
#End Region

#Region "Properties"

    <ValidateContainsDeductible("")>
    Public ReadOnly Property Id() As Guid
        Get
            Return MyBase.Id
        End Get
    End Property

    Public Overrides ReadOnly Property IsDaysLimitExceeded() As Boolean
        Get
            Dim flag = False
            If (Not Me.IsNew) Then
                For Each auth As ClaimAuthorization In Me.ClaimAuthorizationChildren
                    If Not Me.ClaimActivityCode Is Nothing AndAlso Me.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso Not auth.RepairDate Is Nothing AndAlso Not auth.ServiceCenter Is Nothing Then
                        Dim elpasedDaysSinceRepaired As Long
                        If Not auth.PickUpDate Is Nothing Then
                            elpasedDaysSinceRepaired = Date.Now.Subtract(auth.PickUpDate.Value).Days
                        Else
                            elpasedDaysSinceRepaired = Date.Now.Subtract(auth.RepairDate.Value).Days
                        End If

                        flag = elpasedDaysSinceRepaired > auth.ServiceCenter.ServiceWarrantyDays.Value
                    Else
                        flag = False
                    End If

                    If (flag = False) Then Exit For
                Next
            End If
            Return flag
        End Get
    End Property
    Public Overrides ReadOnly Property IsMaxSvcWrtyClaimsReached() As Boolean
        Get
            Dim result As Boolean = False
            Dim claimdal As New ClaimDAL

            Dim maxNoOfSvcWrntyAllowed As Integer = Me.Certificate.Product.AttributeValues.Where(Function(av) av.Attribute.UiProgCode = "").FirstOrDefault().Value.ToString()
            Dim totalSvcWrntyForCert As Integer = claimdal.GetTotalSvcWarrantyByCert(Certificate.Id, Dealer.Id)

            If (totalSvcWrntyForCert >= maxNoOfSvcWrntyAllowed) Then
                result = True
            End If

            Return result
        End Get
    End Property

    Public ReadOnly Property AuthorizedAmount As DecimalType
        Get

            MyBase.AuthorizedAmount = CalculateAuthAmount()
            Return MyBase.AuthorizedAmount
        End Get
    End Property

    Public ReadOnly Property RepairDate As DateType
        Get

            MyBase.RepairDate = GetRepairDate()
            Return MyBase.RepairDate
        End Get
    End Property

    Public ReadOnly Property PickUpDate As DateType
        Get

            MyBase.PickUpDate = GetPickUpDate()
            Return MyBase.PickUpDate
        End Get
    End Property

    Public ReadOnly Property VisitDate As DateType
        Get
            Return MyBase.VisitDate
        End Get
    End Property

    Public ReadOnly Property HasMultipleServiceCenters() As Boolean
        Get
            Return Not Me.EvaluateForSingleServiceCenter()
        End Get
    End Property

    Public ReadOnly Property ReserveAmount() As DecimalType
        Get
            Dim dal As New ClaimDAL
            Return dal.GetClaimReserveAmount(Me.Id)
        End Get
    End Property
#End Region

#Region "Instance Methods"
    Public Function GetLegacyBridgeServiceClient() As LegacyBridgeServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_LEGACY_BRIDGE_SERVICE), False)
        Dim client = New LegacyBridgeServiceClient("CustomBinding_ILegacyBridgeService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As WebAppGatewayClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE), False)
        Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Public Overrides Sub CreateClaim()
        MyBase.CreateClaim()
    End Sub

    Public Overrides Sub Save(Optional ByVal Transaction As IDbTransaction = Nothing)
        'Assigning it in case it is not loaded even one time

        MyBase.AuthorizedAmount = CalculateAuthAmount()
        MyBase.RepairDate = GetRepairDate()
        MyBase.PickUpDate = GetPickUpDate()

        'Updating the amount in case some one changed deductible on Claim
        Me.UpdateDeductibleAmountOnLineItems()

        MyBase.Save(Transaction)

    End Sub
    Public Function AddClaimAuthorization(ByVal serviceCenterId As Guid) As ClaimAuthorization
        Dim newClaimAuth As ClaimAuthorization
        Try
            newClaimAuth = CType(Me.ClaimAuthorizationChildren.GetNewChild(), BusinessObjectsNew.ClaimAuthorization)
            newClaimAuth.Prepopulate(serviceCenterId, Me.Id)
            Me.CalculateAuthAmount()
            If Me.DeductibleType.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT Then
                Me.PrepopulateDeductible()
            End If
            If Me.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetPayDeductLookupList(Authentication.LangId), Codes.AUTH_LESS_DEDUCT_Y) And
                newClaimAuth.ContainsDeductible Then
                newClaimAuth.AddDeductibleLineItem()
            End If
            newClaimAuth.Save()
            Return newClaimAuth
        Catch ex As DataBaseAccessException
            Me.ClaimAuthorizationChildren.GetChild(newClaimAuth.Id).Delete()
            Throw ex
        End Try
    End Function

    Private Function AddClaimAuthorization(ByVal serviceCenterId As Guid, ByVal dv As PriceListDetail.PriceListResultsDV, Optional ByVal specialInstructions As String = Nothing) As ClaimAuthorization
        Dim newClaimAuth As ClaimAuthorization

        newClaimAuth = CType(Me.ClaimAuthorizationChildren.GetNewChild(), BusinessObjectsNew.ClaimAuthorization)
        newClaimAuth.Prepopulate(serviceCenterId, Me.Id, dv)
        newClaimAuth.Save()
        Me.CalculateAuthAmount()
        Me.PrepopulateDeductible()
        If Not specialInstructions Is Nothing Then newClaimAuth.SpecialInstruction = specialInstructions

        Return newClaimAuth
    End Function

    Private Function CalculateAuthAmount() As Decimal
        Dim amount As Decimal = New Decimal(0)
        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            amount = amount + auth.AuthorizedAmount
        Next
        Return amount
    End Function

    Private Function GetRepairDate() As DateType
        Dim repairDate As DateType = Nothing
        Dim createdOrModifiedDate As DateType = Nothing
        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If (Not auth.RepairDate Is Nothing) Then
                If (createdOrModifiedDate Is Nothing) Then
                    createdOrModifiedDate = If(auth.ModifiedDate Is Nothing, auth.CreatedDate, auth.ModifiedDate)
                    repairDate = auth.RepairDate
                Else
                    If (createdOrModifiedDate.Value < If(auth.ModifiedDate Is Nothing, auth.CreatedDate, auth.ModifiedDate)) Then
                        repairDate = auth.RepairDate
                    End If
                End If
            End If
        Next
        Return repairDate
    End Function

    Private Function GetPickUpDate() As DateType
        Dim pickUpDate As DateType = Nothing
        Dim createdOrModifiedDate As DateType = Nothing
        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If (Not auth.RepairDate Is Nothing) Then
                If (createdOrModifiedDate Is Nothing) Then
                    createdOrModifiedDate = If(auth.ModifiedDate Is Nothing, auth.CreatedDate, auth.ModifiedDate)
                    pickUpDate = auth.PickUpDate
                Else
                    If (createdOrModifiedDate.Value < If(auth.ModifiedDate Is Nothing, auth.CreatedDate, auth.ModifiedDate)) Then
                        pickUpDate = auth.PickUpDate
                    End If
                End If
            End If
        Next
        If pickUpDate Is Nothing Then
            Return MyBase.PickUpDate
        End If
        Return pickUpDate
    End Function

    Public Function HasActiveAuthorizations() As Boolean
        Dim flag As Boolean = True
        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If (auth.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled _
                Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Paid _
                Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Void) Then
                flag = False
                Exit For
            End If
        Next
        Return flag
    End Function

    Public Function HasNoReconsiledAuthorizations() As Boolean
        Dim flag As Boolean = True
        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If (auth.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled _
                Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Paid _
                Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Void) Then
                flag = False
                Exit For
            End If
        Next
        Return flag
    End Function

    Public Function CanChangeCoverage() As Boolean
        Return HasActiveAuthorizations() AndAlso (Me.Status = BasicClaimStatus.Active)
    End Function

    Public Sub ChangeCoverageType(ByVal certItemCoverageTypeId As Guid, ByVal causeOfLossId As Guid)

        If (Not CanChangeCoverage()) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, "COVERAGE_NOT_IN_EFFECT")
        End If

        Dim searchDV As CertItemCoverage.CertItemCoverageSearchDV = Nothing
        searchDV = CertItemCoverage.GetClaimCoverageType(Me.CertificateId, Me.CertItemCoverageId, Me.LossDate, Me.StatusCode, Nothing)
        If (searchDV.Count > 0) Then

            Me.CertItemCoverageId = certItemCoverageTypeId
            Me.CalculateFollowUpDate()
            Me.CauseOfLossId = causeOfLossId

            Me.CheckForRules()

            If (Not Me.Status = BasicClaimStatus.Pending Or Not Me.Status = BasicClaimStatus.Denied) Then
                For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
                    If (Not auth.IsNew) Then
                        auth.Void()
                        'Create New Authorization on basis of new method of Repair
                        Me.AddClaimAuthorization(auth.ServiceCenterId)
                    End If
                Next
            End If

            Me.Save()

        End If

    End Sub

    Private Function GetServiceCenterId() As Guid

        'Assumption : MultiAuthClaim will be only having one service center
        Dim serviceCenterId As Guid = Nothing

        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If (auth.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Pending) Then
                serviceCenterId = auth.ServiceCenterId
                Exit For
            End If
        Next
        Return serviceCenterId
    End Function

    Public Sub VoidAuthorizations()
        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            auth.Void()
        Next
    End Sub

    Public Sub CreateReplacementFromRepair(ByVal newServiceCenterId As Guid)

        If newServiceCenterId.Equals(Guid.Empty) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, "NO_SERVICE_CENTER")
        End If
        Dim isNewServiceCenter As Boolean = True

        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            'Commented for DEF-17541
            'If (Not auth.IsNew) Then

            Dim serviceLevelCode As String = String.Empty
            If Not auth.ServiceLevelId.Equals(Guid.Empty) Then serviceLevelCode = LookupListNew.GetCodeFromId(Codes.SERVICE_LEVEL, auth.ServiceLevelId)

            Dim dvEstimatePrice As PriceListDetail.PriceListResultsDV
            Dim dvReplacementPrice As PriceListDetail.PriceListResultsDV

            'Check first if Price List is configured for estimate price..
            dvEstimatePrice = Me.GetPricesForServiceType(auth.ServiceCenter.Code, Codes.SERVICE_CLASS__REPAIR, Codes.SERVICE_TYPE__ESTIMATE_PRICE, serviceLevelCode)

            If (dvEstimatePrice Is Nothing OrElse dvEstimatePrice.Count = 0) Then
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
            End If

            Dim repairEstimate As Decimal = CType(dvEstimatePrice(0)(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE), Decimal)

            Dim servCenter As ServiceCenter = New ServiceCenter(newServiceCenterId, Me.Dataset)

            'Check first if Price List is configured for replacement price..
            dvReplacementPrice = Me.GetPricesForServiceType(servCenter.Code, Codes.SERVICE_CLASS__REPLACEMENT, Codes.SERVICE_TYPE__REPLACEMENT_PRICE, serviceLevelCode)

            If (dvReplacementPrice Is Nothing OrElse dvReplacementPrice.Count = 0) Then
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
            End If


            Me.CalculateFollowUpDate()
            Me.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)
            Me.MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)
            Me.ReasonClosedId = Guid.Empty
            Me.ClaimClosedDate = Nothing

            auth.Void()

            If (auth.ServiceCenterId = newServiceCenterId) Then
                isNewServiceCenter = False
                Dim claimAuth As ClaimAuthorization = Me.AddClaimAuthorization(newServiceCenterId)

                If (Not repairEstimate.Equals(New Decimal(0D))) Then claimAuth.PopulateClaimAuthItems(dvEstimatePrice)
            Else
                If (Not repairEstimate.Equals(New Decimal(0D))) Then Me.AddClaimAuthorization(auth.ServiceCenterId, dvEstimatePrice)

            End If
            'Commented for DEF-17541
            'End If

        Next

        If isNewServiceCenter Then Me.AddClaimAuthorization(newServiceCenterId)
        Me.AddNewComment()

        Me.Save()
    End Sub

    Public Function GetPricesForServiceType(ByVal serviceCenterCode As String, ByVal serviceClassCode As String, ByVal serviceTypeCode As String, ByVal serviceLeveCode As String) As DataView

        Dim equipmentId As Guid, equipmentclassId As Guid, conditionId As Guid
        If (Me.Dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
            If (Not Me.ClaimedEquipment Is Nothing) Then
                equipmentId = Me.ClaimedEquipment.EquipmentId
                equipmentclassId = Me.ClaimedEquipment.EquipmentBO.EquipmentClassId
                conditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW)
            Else
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, "NO_CLAIMED_EQUIPMENT_FOUND")
            End If
        End If



        Dim dv As PriceListDetail.PriceListResultsDV = PriceListDetail.GetPricesForServiceType(Me.CompanyId, serviceCenterCode,
                                                                                                       Me.RiskTypeId, Me.LossDate,
                                                                                                       Me.Certificate.SalesPrice,
                                                                                                       LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, serviceClassCode),
                                                                                                       LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, serviceTypeCode),
                                                                                                       equipmentclassId, equipmentId, conditionId, Me.Dealer.Id, serviceLeveCode)
        Return dv
    End Function


    Private Function EvaluateForSingleServiceCenter() As Boolean
        Dim flag As Boolean = True
        Dim serviceCenterId As Guid = Nothing
        For Each Auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If (Not Auth.ClaimAuthStatus = ClaimAuthorizationStatus.Void) Then
                If (serviceCenterId.Equals(Guid.Empty)) Then
                    serviceCenterId = Auth.ServiceCenterId
                Else
                    If (Not serviceCenterId.Equals(Auth.ServiceCenterId)) Then flag = False
                    Exit For
                End If
            End If
        Next
        Return flag
    End Function

    Public Sub CreateServiceWarranty()
        'Resolve the service center
        Dim serviceCenter As ServiceCenter = Nothing
        Dim specialInstructions As String = String.Empty

        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If auth.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled Or
                auth.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid Or auth.ClaimAuthStatus = ClaimAuthorizationStatus.Paid Then

                For Each authItem As ClaimAuthItem In auth.ClaimAuthorizationItemChildren
                    If (authItem.ServiceClassCode = Codes.SERVICE_CLASS__REPAIR) Then
                        serviceCenter = auth.ServiceCenter
                        specialInstructions = auth.SpecialInstruction
                        Exit For
                    End If
                Next
            End If
        Next


        Dim dv As PriceListDetail.PriceListResultsDV = Me.GetPricesForServiceType(serviceCenter.Code, Codes.SERVICE_CLASS__REPAIR,
                                                                                  Codes.SERVICE_TYPE__SERVICE_WARRANTY, String.Empty)


        If (dv Is Nothing OrElse dv.Count = 0) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
        End If

        'Set the price to zero
        For Each row As DataRowView In dv
            row(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE) = New Decimal(0D)
        Next

        Me.AddClaimAuthorization(serviceCenter.Id, dv, specialInstructions)
        Me.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REWORK)
        Me.ReasonClosedId = Guid.Empty
        Me.ClaimClosedDate = Nothing
        If Me.Status <> BasicClaimStatus.Active Then Me.Status = BasicClaimStatus.Active
        Me.Save()
    End Sub

    Private Sub UpdateDeductibleAmountOnLineItems()

        If Me.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetPayDeductLookupList(Authentication.LangId), Codes.AUTH_LESS_DEDUCT_Y) Then

            For Each item As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
                If item.ContainsDeductible Then
                    For Each lineItem As ClaimAuthItem In item.ClaimAuthorizationItemChildren.Where(Function(i) i.ServiceTypeCode = Codes.SERVICE_TYPE__PAY_DEDUCTIBLE)
                        lineItem.Amount = Me.Deductible
                    Next
                End If
                item.Save()
            Next
        End If

    End Sub

    Public Overrides Sub CloseTheClaim()
        MyBase.CloseTheClaim()
    End Sub

    Public Overrides Sub ReopenClaim()
        Dim claimClosedDate As Date = Me.ClaimClosedDate.Value
        MyBase.ReopenClaim()
        Me.ReopenClaimAuthorizations(claimClosedDate)

    End Sub

    Public Sub ReopenClaimAuthorizations(ByVal claimClosedDate As Date)

        'If Claim has atleast one non void Authorizations, it was not closed abruptly
        If Me.NonVoidClaimAuthorizationList.Count > 1 Then Return

        'Find the Claim Closed DateTime
        Dim claimClosedDateTime As DateTime = Me.ClaimHistoryChildren.OrderByDescending(Function(item) item.CreatedDateTime).Where(Function(item) item.CreatedDate.Value.Date = claimClosedDate.Date AndAlso
                                                                            item.StatusCodeOld <> item.StatusCodeNew AndAlso
                                                                            item.StatusCodeNew = Codes.CLAIM_STATUS__CLOSED).FirstOrDefault().CreatedDateTime

        'Find all authorizations which were modified within 1 sec of claim Closed DateTime
        For Each auth As ClaimAuthorization In Me.ClaimAuthorizationChildren.Where(Function(item) _
                                                   (item.ModifiedDate.Value.Ticks > claimClosedDateTime.AddSeconds(-1).Ticks And
                                                    item.ModifiedDate.Value.Ticks < claimClosedDateTime.AddSeconds(1).Ticks))

            'Find the Authorization History which was created within 1 sec of claim Closed DateTime and whose status was changed
            Dim claimAuthHist As ClaimAuthHistory = auth.ClaimAuthorizationHistoryChildren.Where(Function(item) _
                                                  (item.HistCreatedDate.Value.Ticks > claimClosedDateTime.AddSeconds(-1).Ticks And
                                                    item.HistCreatedDate.Value.Ticks < claimClosedDateTime.AddSeconds(1).Ticks) And
                                                   item.ClaimAuthStatus <> auth.ClaimAuthStatus).FirstOrDefault()
            If Not claimAuthHist Is Nothing Then

                auth.ClaimAuthStatus = claimAuthHist.ClaimAuthStatus
                auth.Save()
            End If

        Next
    End Sub

    Public Function AddClaimAuthForDeductibleRefund(ByVal serviceCenterId As Guid, ByVal refundAmount As Decimal, ByVal refundMethod As String) As ClaimAuthorization
        Dim newClaimAuth As ClaimAuthorization
        Try
            newClaimAuth = CType(Me.ClaimAuthorizationChildren.GetNewChild(), BusinessObjectsNew.ClaimAuthorization)
            newClaimAuth.PrepopulateClaimAuthForDeductibleRefund(serviceCenterId, Me.Id, Me.CertificateId, refundMethod)
            newClaimAuth.AddDeductibleRefundLineItem(refundAmount)
            newClaimAuth.Save()
            Return newClaimAuth
        Catch ex As DataBaseAccessException
            Me.ClaimAuthorizationChildren.GetChild(newClaimAuth.Id).Delete()
            Throw ex
        End Try
    End Function

    Public Function IsDeductibleRefundAllowed() As Boolean
        Dim flag As Boolean = False
        For Each auth As ClaimAuthorization In Me.NonVoidClaimAuthorizationList
            If (auth.ClaimAuthStatus = ClaimAuthorizationStatus.Collected And auth.AuthTypeXcd = Codes.CLAIM_EXTENDED_STATUS_AUTH_TYPE_SALES_ORDER) Then

                Dim authItem = auth.ClaimAuthorizationItemChildren.Where(Function(i) i.ServiceTypeCode = Codes.SERVICE_TYPE__DEDUCTIBLE).ToList()

                If authItem.Count > 0 Then
                    flag = True
                End If
            End If
        Next
        Return flag

    End Function

    Public Function IsDeductibleRefundExist() As Boolean
        Dim flag As Boolean = False
        Dim auth = Me.NonVoidClaimAuthorizationList.Where(Function(c) c.AuthTypeXcd = Codes.CLAIM_EXTENDED_STATUS_AUTH_TYPE_CREDIT_NOTE And c.PartyTypeXcd = Codes.CLAIM_EXTENDED_STATUS_PARTY_TYPE_CUSTOMER And
                                                                    (c.ClaimAuthStatus = ClaimAuthorizationStatus.Pending Or c.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized)).ToList()
        If auth.Count > 0 Then
            flag = True
        End If
        Return flag

    End Function
#End Region

#Region "Validations"
    Public NotInheritable Class ValidateContainsDeductible
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.CLAIM_CANNOT_HAVE_TWO_AUTHORIZATIONS_CONTAINING_DEDUCTIBLE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As MultiAuthClaim = CType(objectToValidate, MultiAuthClaim)

            Dim flag As Boolean = obj.NonVoidClaimAuthorizationList.Where(Function(i) i.ContainsDeductible).Count > 1
            Return Not flag

        End Function
    End Class
#End Region

    Public Function GetFulfillmentDetails(claimNumber As String, companyCode As String) As FulfillmentDetails Implements IFullfillable.GetFulfillmentDetails
        Dim wsRequest As GetFulfillmentDetailsRequest = New GetFulfillmentDetailsRequest()
        wsRequest.ClaimNumber = claimNumber
        wsRequest.CompanyCode = companyCode
        Dim wsResponseObject As New FulfillmentDetails


        If Not String.IsNullOrEmpty(claimNumber) AndAlso Not String.IsNullOrEmpty(companyCode) Then

                wsResponseObject = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, FulfillmentDetails)(
                    GetClaimFulfillmentWebAppGatewayClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As WebAppGatewayClient)
                        Return c.GetFulfillmentDetails(wsRequest)
                    End Function)
        End If

        Return wsResponseObject
    End Function
End Class
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/2/2004)  ********************
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.Common
Imports Assurant.Common
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices.WindowsRuntime
Imports System.Threading
Imports Assurant.ElitaPlus.BusinessObjectsNew.ClaimFulfillmentWebAppGatewayService
Imports Assurant.ElitaPlus.BusinessObjectsNew.LegacyBridgeService
Imports Assurant.ElitaPlus.DataEntities
Imports Microsoft.Web.Services3.Addressing

Public NotInheritable Class Claim
    Inherits ClaimBase : Implements IInvoiceable, IFullfillable

    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"


#Region "Constructors"

    'New BO
    Friend Sub New()
        MyBase.New()
        Me.ReportedDate = Date.Today
    End Sub

    'New BO attaching to a BO family
    Friend Sub New(ByVal familyDS As DataSet)
        MyBase.New(familyDS)
        Me.ReportedDate = Date.Today
    End Sub

    'Existing BO
    Friend Sub New(ByVal id As Guid)
        MyBase.New(id)
        Me.originalAuthorizedAmount = Me.AuthorizedAmount
    End Sub

    'Existing BO attaching to a BO family
    Friend Sub New(ByVal id As Guid, ByVal familyDS As DataSet, Optional ByVal blnMustReload As Boolean = False)
        MyBase.New(id, familyDS, blnMustReload)
        Me.originalAuthorizedAmount = Me.AuthorizedAmount
    End Sub

    Friend Sub New(ByVal row As DataRow)
        MyBase.New(row)
    End Sub

    Friend Sub New(ByVal claimNumber As String, ByVal companyId As Guid)
        MyBase.New(claimNumber, companyId)
        Me.originalAuthorizedAmount = Me.AuthorizedAmount
    End Sub

#End Region

#Region "Private Members"
    'Initialization code for new objects

    Private originalAuthorizedAmount As DecimalType = New DecimalType(0D)
    Private parentClaim As Claim

    'REQ-5404 begin
    Private _cert_RemainingBalance As Decimal
    Private _vehicle_condition As String
    Private _MFG_MAX_Mileage_Limit As Integer
    Private _coverage_mi_km As Integer
    Private _sourceForCancellation As String
    'REQ-5404 end

    Public Function GetExistClaimNumber(ByVal companyId As Guid, ByVal claimNumber As String, ByVal CoverageCode As String, ByVal UnitNumber As Integer, Optional ByVal IsPayClaim As Boolean = False) As String
        Try
            Dim dal As New ClaimDAL
            Dim retClaimNumber As String = dal.GetExistClaimNumber(companyId, claimNumber, CoverageCode, UnitNumber, IsPayClaim)

            Return retClaimNumber
        Catch ex As DataBaseAccessException
            If ex.Code = Common.ErrorCodes.ERR_NO_CLAIM_INFO_FOUND Then
                Throw New BOValidationException("Claim Error: ", Common.ErrorCodes.ERR_NO_CLAIM_INFO_FOUND)
            Else
                Throw ex
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function getDeductible(ByVal pCert_Item_Cov_Id As Guid, ByVal pService_Center_Id As Guid) As DecimalType
        getDeductible = New ClaimDAL().LoadDeductible(pCert_Item_Cov_Id, pService_Center_Id)
    End Function



#End Region

#Region "Constants"

    Public Const REPLACED_ON As String = "REPLACED_ON"
    Public ZERO_DECIMAL As Decimal = 0D
    Public Const ACTIVE_STATUS As String = "A"
    Public Const DATE_COL_NAME_PRODUCT_CODE_ID As String = "Product_Code_Id"
    Public Const COL_NAME_SPECIAL_SERVICE_ID As String = "special_service_id"
    Public Const COL_NAME_SPECIAL_SERVICE_OCCURANCES As String = "special_service_Occurances"
    Public Const DATA_COL_NAME_MFG_MAX_MILEAGE_LIMIT As String = "MFG_Max_Mileage_Limit"
    Public Const DATA_COL_NAME_MFG_NEW_USED As String = "new_used"
    Public Const DATA_COL_NAME_REMAINING_BALANCE As String = "remaining_balance"
    Public Const CLAIM_COUNT As String = "claim_count"
    Public Const COL_PRICE_DV As String = "Price"

    Public Const CANCELLATION_REASON_CODE As String = "EXM" 'Expiration Due Mileage Limit


#End Region

#Region "Variables"

    Private cmpBO As Company
    Private moIsUpdatedComment As Boolean = True
    Private moIsComingFromPayClaim As Boolean = False
    Private salutationDesc As String
    Private objCurrentClaimAuthDetail As ClaimAuthDetail
    Private _AuthDetailUsage As String = String.Empty
    Private _NeedToCreateTranLogHeader As Boolean = False
    Private _IsLossDateCheckforCancelledCert As Boolean = False
    Private splsvcOccurances As String '= "special_service_Occurances"
    Private splsvcDesc As String '= "Special_Service_Description"
    Private splsvcPriceGrp As String  'Special_Service_Price_Group
    Private splsvcSvcCls As String  'Special_Service_Service_Class
    Private splsvcSvcType As String  'Special_Service_Service_Type
    Private moMasterClaimId As Guid
#End Region

#Region "Properties"

    '<ValueMandatory("")> _
    Public Property ClaimGroupId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIM_GROUP_ID, Value)
        End Set
    End Property

    Private _servCenterObject As ServiceCenter
    Public ReadOnly Property ServiceCenterObject() As ServiceCenter Implements IInvoiceable.ServiceCenterObject
        Get
            If Me._servCenterObject Is Nothing Then
                If Not Me.ServiceCenterId.Equals(Guid.Empty) Then
                    Me._servCenterObject = New ServiceCenter(Me.ServiceCenterId)
                End If
            End If
            Return Me._servCenterObject
        End Get
    End Property

    Public ReadOnly Property CreatedDate() As DateType Implements IInvoiceable.CreatedDate
        Get
            Return MyBase.CreatedDate
        End Get
    End Property

    Public ReadOnly Property CreatedDateTime() As DateTimeType Implements IInvoiceable.CreatedDateTime
        Get
            Return MyBase.CreatedDateTime
        End Get
    End Property

    Public ReadOnly Property AssurantPays() As DecimalType Implements IInvoiceable.AssurantPays
        Get
            Return MyBase.AssurantPays
        End Get
    End Property

    Public ReadOnly Property ConsumerPays() As DecimalType Implements IInvoiceable.ConsumerPays
        Get
            Return MyBase.ConsumerPays
        End Get
    End Property

    Public ReadOnly Property MethodOfRepairCode() As String Implements IInvoiceable.MethodOfRepairCode
        Get
            Return MyBase.MethodOfRepairCode
        End Get
    End Property

    Public ReadOnly Property RiskType() As String Implements IInvoiceable.RiskType
        Get
            Return MyBase.RiskType
        End Get
    End Property

    Public Property ServiceCenterId() As Guid Implements IInvoiceable.ServiceCenterId
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_SERVICE_CENTER_ID, Value)
            If Not Value.Equals(Guid.Empty) Then
                Dim servCenterObj As New ServiceCenter(Value)
                Me.SetValue(ClaimDAL.COL_NAME_SERVICE_CENTER, servCenterObj.Description)
            Else
                Me.SetValue(ClaimDAL.COL_NAME_SERVICE_CENTER, Nothing)
            End If
            'invalidate the servcenter object
            Me._servCenterObject = Nothing
        End Set
    End Property

    Public Property LoanerCenterId() As Guid Implements IInvoiceable.LoanerCenterId
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOANER_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_LOANER_CENTER_ID), Byte()))
            End If
        End Get
        Private Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LOANER_CENTER_ID, Value)
        End Set
    End Property

    <ValidAuthorizedAmount(""), ValidAuthAmountEdit("")>
    Public Property AuthorizedAmount() As DecimalType Implements IInvoiceable.AuthorizedAmount
        Get
            Return MyBase.AuthorizedAmount
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            MyBase.AuthorizedAmount = Value
        End Set
    End Property


    Public Property InvoiceProcessDate() As DateType Implements IInvoiceable.InvoiceProcessDate
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_INVOICE_PROCESS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimDAL.COL_NAME_INVOICE_PROCESS_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_INVOICE_PROCESS_DATE, Value)
        End Set
    End Property

    <ValidLoanerReturnedDate("")>
    Public Property LoanerReturnedDate() As DateType Implements IInvoiceable.LoanerReturnedDate
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOANER_RETURNED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimDAL.COL_NAME_LOANER_RETURNED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LOANER_RETURNED_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property Source() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property SpareParts() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SPARE_PARTS) Is DBNull.Value Then
                Return "N"
            Else
                Return CType(Row(ClaimDAL.COL_NAME_SPARE_PARTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()

            'Added logic to default to "Y" for spare parts if the current value is "Y", else default to "N"
            If (Not Row(ClaimDAL.COL_NAME_SPARE_PARTS) Is DBNull.Value) AndAlso (Row(ClaimDAL.COL_NAME_SPARE_PARTS) = "Y") Then
                Me.SetValue(ClaimDAL.COL_NAME_SPARE_PARTS, "Y")
            ElseIf Value = Nothing Then
                Me.SetValue(ClaimDAL.COL_NAME_SPARE_PARTS, "N")
            Else
                Me.SetValue(ClaimDAL.COL_NAME_SPARE_PARTS, Value)
            End If

        End Set
    End Property

    Public ReadOnly Property ServiceCenter() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SERVICE_CENTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_SERVICE_CENTER), String)
            End If
        End Get

    End Property

    Public ReadOnly Property LoanerCenter() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOANER_CENTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_LOANER_CENTER), String)
            End If
        End Get

    End Property
    <ValidStringLength("", Max:=30)>
    Public Property LoanerRquestedXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_LOANER_REQUESTED_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_LOANER_REQUESTED_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_LOANER_REQUESTED_XCD, Value)
        End Set
    End Property

    Public ReadOnly Property DefectReason() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEFECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_DEFECT_REASON), String)
            End If
        End Get

    End Property

    Public ReadOnly Property TechnicalReport() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_TECHNICAL_REPORT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_TECHNICAL_REPORT), String)
            End If
        End Get

    End Property

    Public ReadOnly Property ExpectedRepairDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_EXPECTED_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimDAL.COL_NAME_EXPECTED_REPAIR_DATE), Date))
            End If
        End Get

    End Property

    Public Property LoanerTaken() As Boolean
        Get
            Return Not Me.LoanerCenterId.Equals(Guid.Empty)
        End Get
        Set(ByVal Value As Boolean)
            If Value Then
                Me.LoanerCenterId = Me.ServiceCenterObject.LoanerCenterId
            Else
                Me.LoanerCenterId = Guid.Empty
            End If
        End Set
    End Property

    Public ReadOnly Property CoverageTypeDescription() As String
        Get
            If Row(ClaimDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Dim certItemCoverage As New CertItemCoverage(CertItemCoverageId)
                Dim dv As DataView = LookupListNew.GetCoverageTypeLookupList(
                        ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Dim desc As String = LookupListNew.GetDescriptionFromId(dv, certItemCoverage.CoverageTypeId)

                Me.SetValue(ClaimDAL.COL_NAME_COVERAGE_TYPE_ID, certItemCoverage.CoverageTypeId)
                Return desc
            Else
                Dim dv As DataView = LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_COVERAGE_TYPE_ID), Byte())))
            End If
        End Get
    End Property

    Public ReadOnly Property StoreServiceCenterId() As Guid
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_STORE_SERVICE_CENTER_ID) Is DBNull.Value)) Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_STORE_SERVICE_CENTER_ID), Byte()))
            End If
        End Get

    End Property

    Public Property WhoPaysId() As Guid
        Get
            CheckDeleted()
            If ((Row(ClaimDAL.COL_NAME_WHO_PAYS_ID) Is DBNull.Value)) Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_WHO_PAYS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_WHO_PAYS_ID, Value)
        End Set
    End Property

    Public ReadOnly Property WhoPaysDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_WHO_PAYS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dv As DataView = LookupListNew.GetWhoPaysLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_WHO_PAYS_ID), Byte())))

            End If
        End Get
    End Property

    Public Overrides ReadOnly Property IsDaysLimitExceeded() As Boolean
        Get
            'For service warranties
            'Since the RepairDate for the new Service Warranty Claim is Blank, we need to get the 
            'RepairDate for the parent Repair Claim.
            'BEGIN - New logic - Ravi
            If (Me.parentClaim Is Nothing AndAlso Not Me.ClaimNumber Is Nothing AndAlso Me.ClaimNumber.EndsWith("S")) Then
                Me.parentClaim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.ClaimNumber.TrimEnd("S"), Me.CompanyId)
            End If
            If (Me.parentClaim Is Nothing OrElse (Not Me.ClaimNumber Is Nothing AndAlso Not Me.ClaimNumber.EndsWith("S"))) Then
                Return False
            End If
            If Not Me.ClaimActivityCode Is Nothing AndAlso Me.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REWORK AndAlso Not Me.parentClaim.RepairDate Is Nothing AndAlso Not Me.ServiceCenterObject Is Nothing Then
                Dim elpasedDaysSinceRepaired As Long
                If Not Me.parentClaim.PickUpDate Is Nothing Then
                    elpasedDaysSinceRepaired = Date.Now.Subtract(Me.parentClaim.PickUpDate.Value).Days
                Else
                    elpasedDaysSinceRepaired = Date.Now.Subtract(Me.parentClaim.RepairDate.Value).Days
                End If

                Return elpasedDaysSinceRepaired > Me.ServiceCenterObject.ServiceWarrantyDays.Value
            Else
                Return False
            End If
            'END - New logic - Ravi
        End Get
    End Property
    Public Function CheckSvcWrantyClaimControl() As Boolean
        Dim certBO As Certificate = New Certificate(Certificate.Id)

        If (certBO.Product.AttributeValues.Where(Function(av) av.Attribute.UiProgCode = Codes.MAX_SVC_WARRANT_COUNT).Count > 0) Then
            Return True
        End If
        Return False
    End Function

    Public Overrides ReadOnly Property IsMaxSvcWrtyClaimsReached() As Boolean
        Get
            Dim result As Boolean = False
            Dim claimdal As New ClaimDAL

            Dim certBO As Certificate = New Certificate(Certificate.Id)

            If (certBO.Product.AttributeValues.Where(Function(av) av.Attribute.UiProgCode = Codes.MAX_SVC_WARRANT_COUNT).Count > 0) Then
                Dim maxNoOfSvcWrntyAllowed As Integer = certBO.Product.AttributeValues.Where(Function(av) av.Attribute.UiProgCode = Codes.MAX_SVC_WARRANT_COUNT).FirstOrDefault().Value.ToString()

                Dim totalSvcWrntyForCert As Integer = claimdal.GetTotalSvcWarrantyByCert(Certificate.Id, Dealer.Id)

                ''''as you are trying to create a servicve warranty claim, add 1 to the totalsvcwrntyCountForCert

                If (maxNoOfSvcWrntyAllowed > totalSvcWrntyForCert) Then
                    result = False
                Else
                    result = True
                End If
            End If

            Return result
        End Get
    End Property


    Private _IsFirstClaimRecordFortheIncident As Boolean = False

    Public ReadOnly Property IsFirstClaimRecordFortheIncident() As Boolean
        Get
            Return _IsFirstClaimRecordFortheIncident
        End Get
    End Property

    Public Property IsComingFromPayClaim() As Boolean Implements IInvoiceable.IsComingFromPayClaim
        Get
            Return moIsComingFromPayClaim
        End Get
        Set(ByVal Value As Boolean)
            moIsComingFromPayClaim = Value
        End Set
    End Property



    <ValidVisitDate("")>
    Public Property VisitDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimDAL.COL_NAME_VISIT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_VISIT_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property

    Public Property ClaimSpecialServiceId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_IS_SPECIAL_SERVICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_IS_SPECIAL_SERVICE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CLAIM_IS_SPECIAL_SERVICE_ID, Value)
        End Set
    End Property

    Public Property SpecialServiceOccurrenceType() As String
        Get
            Return splsvcOccurances
        End Get
        Set(ByVal Value As String)
            splsvcOccurances = Value
        End Set
    End Property
    Public Property SpecialServiceServiceClass() As String
        Get
            Return splsvcSvcCls
        End Get
        Set(ByVal Value As String)
            splsvcSvcCls = Value
        End Set
    End Property
    Public Property SpecialServiceServiceType() As String
        Get
            Return splsvcSvcType
        End Get
        Set(ByVal Value As String)
            splsvcSvcType = Value
        End Set
    End Property


    'TODO: Following property needs to be removed once Price List requirement is completed
    'Public Property SpecialServicePriceGroupType() As String
    '    Get
    '        Return splsvcPriceGrp
    '    End Get
    '    Set(ByVal Value As String)
    '        splsvcPriceGrp = Value
    '    End Set
    'End Property

    Public Property SpecialServiceDesc() As String
        Get
            Return splsvcDesc
        End Get
        Set(ByVal Value As String)
            splsvcDesc = Value
        End Set
    End Property

    Public Property IsUpdatedMasterClaimComment() As Boolean
        Get
            Return moIsUpdatedMasterClaimComment
        End Get
        Set(ByVal Value As Boolean)
            moIsUpdatedMasterClaimComment = Value
        End Set
    End Property

    Public Property MasterClaimId() As Guid
        Get
            Return moMasterClaimId
        End Get
        Set(ByVal Value As Guid)
            moMasterClaimId = Value
        End Set
    End Property

    Public ReadOnly Property SpecialService() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CLAIM_IS_SPECIAL_SERVICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dv As DataView
                dv = LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Return LookupListNew.GetDescriptionFromId(dv, New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_IS_SPECIAL_SERVICE_ID), Byte())))
            End If
        End Get
    End Property

    Public Property InvoiceDate() As DateType Implements IInvoiceable.InvoiceDate
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property

    Public Property CurrentOdometer() As LongType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_CURRENT_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimDAL.COL_NAME_CURRENT_ODOMETER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_CURRENT_ODOMETER, Value)
        End Set
    End Property

    Public Property ReverseLogisticsId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_REVERSE_LOGISTICS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_REVERSE_LOGISTICS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REVERSE_LOGISTICS_ID, Value)
        End Set
    End Property

    Public ReadOnly Property IsSupervisorAuthorizationRequired() As Boolean
        Get
            Dim bIsReq As Boolean
            Dim bDaysExceeded As Boolean = Me.IsDaysLimitExceeded
            Dim bAuthorizationExceeded As Boolean = Me.IsAuthorizationLimitExceeded

            If bAuthorizationExceeded Then
                bIsReq = True
            Else
                If (Not bDaysExceeded) Then
                    bIsReq = False
                Else
                    If (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__CLAIMS_MANAGER) OrElse
                        ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
                        bIsReq = False
                    Else
                        bIsReq = True
                    End If
                End If
            End If
            Return bIsReq
        End Get
    End Property

    Public ReadOnly Property StoreNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_STORE_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Dim serviceCenter As New ServiceCenter(StoreServiceCenterId)
                Return serviceCenter.Code
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=500)>
    Public Property SpecialInstruction() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property

    Public Property CertRemainingBalance() As Decimal
        Get
            Return _cert_RemainingBalance
        End Get
        Set(ByVal Value As Decimal)
            _cert_RemainingBalance = Value
        End Set
    End Property

    Public Property VehicleCondition() As String
        Get
            Return _vehicle_condition
        End Get
        Set(ByVal Value As String)
            _vehicle_condition = Value
        End Set
    End Property
    Public Property MFG_MAX_MileageLimit() As Integer
        Get
            Return _MFG_MAX_Mileage_Limit
        End Get
        Set(ByVal Value As Integer)
            _MFG_MAX_Mileage_Limit = Value
        End Set
    End Property
    Public Property CoverageMiKm() As Integer
        Get
            Return _coverage_mi_km
        End Get
        Set(ByVal Value As Integer)
            _coverage_mi_km = Value
        End Set
    End Property

    Public Property SourceForCancellation() As String
        Get
            Return _sourceForCancellation
        End Get
        Set(ByVal Value As String)
            _sourceForCancellation = Value
        End Set
    End Property

    Public Property DeviceReceptionDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEVICE_RECEPTION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimDAL.COL_NAME_DEVICE_RECEPTION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEVICE_RECEPTION_DATE, Value)
        End Set
    End Property

    Public Property DeviceActivationDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_DEVICE_ACTIVATION_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property EmployeeNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_EMPLOYEE_NUMBER, Value)
        End Set
    End Property

    Public Property FulfilmentMethod() As String
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_FULFILMENT_METHOD_XCD, Value)
        End Set
    End Property
    Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property
#End Region

#Region "Extended Properties"
    Public ReadOnly Property CanDisplayVisitAndPickUpDates() As Boolean Implements IInvoiceable.CanDisplayVisitAndPickUpDates
        Get
            'Pickup and visit dates: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            'JP -- If the notification type field is not null then display the pick up date and 
            '      do not base the display on the source field else let the source field handle it as usual

            If Not (Me.NotificationTypeId.Equals(Guid.Empty)) Then
                Return True
            ElseIf ((Me.Source Is Nothing) Or (Not Me.Source Is Nothing AndAlso Me.Source.Equals(String.Empty))) _
                             And ((Me.ClaimActivityCode Is Nothing) Or ((Not Me.ClaimActivityCode Is Nothing) AndAlso
                             ((Me.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REPLACED) And
                               Me.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT))) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Property AuthDetailUsage() As String
        Get
            Return _AuthDetailUsage
        End Get
        Set(ByVal Value As String)
            _AuthDetailUsage = Value
        End Set
    End Property

    Public ReadOnly Property MyDataset() As DataSet
        Get
            Return Me.Dataset
        End Get
    End Property


#End Region

#Region "Invoiceable properties and Overridden Properties"

    <ValidRepairDate("")>
    Public Property RepairDate() As DateType Implements IInvoiceable.RepairDate
        Get
            Return MyBase.RepairDate
        End Get
        Set(ByVal Value As DateType)
            MyBase.RepairDate = Value
        End Set
    End Property

    <ValidPickUpDate("")>
    Public Property PickUpDate() As DateType Implements IInvoiceable.PickUpDate
        Get
            Return MyBase.PickUpDate
        End Get
        Set(ByVal Value As DateType)
            MyBase.PickUpDate = Value
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property AuthorizationNumber() As String Implements IInvoiceable.AuthorizationNumber
        Get
            CheckDeleted()
            If Row(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    'Public ReadOnly Property SvcControlNumber() As String Implements IInvoiceable.SvcControlNumber
    '    Get
    '        Return Me.AuthorizationNumber
    '    End Get
    'End Property

    Public ReadOnly Property ClaimActivityCode() As String Implements IInvoiceable.ClaimActivityCode
        Get
            Return MyBase.ClaimActivityCode
        End Get
    End Property

    Public ReadOnly Property Claim_Id() As Guid Implements IInvoiceable.Claim_Id
        Get
            Return Me.Id
        End Get
    End Property

    Public ReadOnly Property IsDirty() As Boolean Implements IInvoiceable.IsDirty
        Get
            Return MyBase.IsDirty
        End Get
    End Property

    Public ReadOnly Property ClaimAuthorizationId() As Guid Implements IInvoiceable.ClaimAuthorizationId
        Get
            Return Guid.Empty
        End Get
    End Property

    Public Property RepairEstimate() As DecimalType Implements IInvoiceable.RepairEstimate
        Get
            CheckDeleted()
            Dim rEstimate As Decimal = 0D
            If Row(ClaimDAL.COL_NAME_REPAIR_ESTIMATE) Is DBNull.Value Then
                Return New DecimalType(rEstimate)
                'Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimDAL.COL_NAME_REPAIR_ESTIMATE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimDAL.COL_NAME_REPAIR_ESTIMATE, Value)
        End Set
    End Property

    Public Overrides Property RepairCodeId() As Guid Implements IInvoiceable.RepairCodeId
        Get
            Return MyBase.RepairCodeId
        End Get
        Set(ByVal value As Guid)
            MyBase.RepairCodeId = value
        End Set
    End Property

    Public Overrides Property ClaimActivityId() As Guid Implements IInvoiceable.ClaimActivityId
        Get
            Return MyBase.ClaimActivityId
        End Get
        Set(ByVal value As Guid)
            MyBase.ClaimActivityId = value
        End Set
    End Property

    Public Property RiskTypeId() As Guid Implements IInvoiceable.RiskTypeId
        Get
            Return MyBase.RiskTypeId
        End Get
        Set(ByVal Value As Guid)
            MyBase.RiskTypeId = Value
        End Set
    End Property

    Public Overrides Property ReasonClosedId() As Guid Implements IInvoiceable.ReasonClosedId
        Get
            Return MyBase.ReasonClosedId
        End Get
        Set(ByVal value As Guid)
            MyBase.ReasonClosedId = value
        End Set
    End Property

    Public Property ClaimNumber() As String Implements IInvoiceable.ClaimNumber
        Get
            Return MyBase.ClaimNumber
        End Get
        Set(ByVal value As String)
            MyBase.ClaimNumber = value
        End Set
    End Property

    Public ReadOnly Property CustomerName() As String Implements IInvoiceable.CustomerName
        Get
            Return MyBase.Certificate.CustomerName
        End Get
    End Property

    Public Sub VerifyConcurrency(ByVal sModifiedDate As String) Implements IInvoiceable.VerifyConcurrency
        MyBase.VerifyConcurrency(sModifiedDate)
    End Sub

    Public Property StatusCode() As String Implements IInvoiceable.StatusCode
        Get
            Return MyBase.StatusCode
        End Get
        Set(ByVal value As String)
            MyBase.StatusCode = value
        End Set
    End Property

    Public Overrides Property ClaimClosedDate() As DateType Implements IInvoiceable.ClaimClosedDate
        Get
            Return MyBase.ClaimClosedDate
        End Get
        Set(ByVal value As DateType)
            MyBase.ClaimClosedDate = value
        End Set
    End Property

    Public Overrides Property CauseOfLossId() As Guid Implements IInvoiceable.CauseOfLossId
        Get
            Return MyBase.CauseOfLossId
        End Get
        Set(ByVal value As Guid)
            MyBase.CauseOfLossId = value
        End Set
    End Property

    Public Overrides Property CompanyId() As Guid Implements IInvoiceable.CompanyId
        Get
            Return MyBase.CompanyId
        End Get
        Set(ByVal value As Guid)
            MyBase.CompanyId = value
        End Set
    End Property

    Public Overrides ReadOnly Property CertificateId() As Guid Implements IInvoiceable.CertificateId
        Get
            Return MyBase.CertificateId
        End Get
    End Property

    Public ReadOnly Property PayDeductibleId() As Guid Implements IInvoiceable.PayDeductibleId
        Get
            Return MyBase.Dealer.PayDeductibleId
        End Get
    End Property

    Public Property CertItemCoverageId() As Guid Implements IInvoiceable.CertItemCoverageId
        Get
            Return MyBase.CertItemCoverageId
        End Get
        Set(ByVal value As Guid)
            MyBase.CertItemCoverageId = value
        End Set
    End Property

    Public Overrides Property IsRequiredCheckLossDateForCancelledCert() As Boolean Implements IInvoiceable.IsRequiredCheckLossDateForCancelledCert
        Get
            Return MyBase.IsRequiredCheckLossDateForCancelledCert
        End Get
        Set(ByVal value As Boolean)
            MyBase.IsRequiredCheckLossDateForCancelledCert = value
        End Set
    End Property

    Public Property SalvageAmount() As DecimalType Implements IInvoiceable.SalvageAmount
        Get
            Return MyBase.SalvageAmount
        End Get
        Set(ByVal value As DecimalType)
            MyBase.SalvageAmount = value
        End Set
    End Property

    Public Property Deductible() As DecimalType Implements IInvoiceable.Deductible
        Get
            Return MyBase.Deductible
        End Get
        Set(ByVal value As DecimalType)
            MyBase.Deductible = value
        End Set
    End Property

    Public Property DiscountAmount() As DecimalType Implements IInvoiceable.DiscountAmount
        Get
            Return MyBase.DiscountAmount
        End Get
        Set(ByVal value As DecimalType)
            MyBase.DiscountAmount = value
        End Set
    End Property

    Public Property LiabilityLimit() As DecimalType Implements IInvoiceable.LiabilityLimit
        Get
            Return MyBase.LiabilityLimit
        End Get
        Set(ByVal value As DecimalType)
            MyBase.LiabilityLimit = value
        End Set
    End Property

    Public ReadOnly Property AboveLiability() As DecimalType Implements IInvoiceable.AboveLiability
        Get
            Return MyBase.AboveLiability
        End Get
    End Property

    Public Sub CalculateFollowUpDate() Implements IInvoiceable.CalculateFollowUpDate
        MyBase.CalculateFollowUpDate()
    End Sub

    'KDDDI Changes 
    Public ReadOnly Property IsReshipmentAllowed() As String Implements IInvoiceable.IsReshipmentAllowed
        Get
            Return MyBase.Dealer.Is_Reshipment_Allowed
        End Get
    End Property
    Public ReadOnly Property IsCancelShipmentAllowed() As String Implements IInvoiceable.IsCancelShipmentAllowed
        Get
            Return MyBase.Dealer.Is_Cancel_Shipment_Allowed
        End Get
    End Property

    Public Sub SaveClaim(Optional ByVal Transaction As IDbTransaction = Nothing) Implements IInvoiceable.SaveClaim
        MyBase.Save(Transaction)
        'REQ-1333, Cancel cert after replacement if needed
        CancelCertBasedOnContractReplacementPolicy()
    End Sub


#End Region

#Region "Public Members"
    Public Sub CancelCertBasedOnContractReplacementPolicy()

        If Me.MethodOfRepairCode = "R" AndAlso (Not Me.RepairDate Is Nothing) Then 'Replacement claim is fulfilled
            'Get a refreshed the certificate BO
            Dim certBO As Certificate = New Certificate(Certificate.Id)
            If certBO.StatusCode = "A" Then 'certificate is still active
                Dim contractBO As Contract = New Contract(Contract.GetContractID(Me.CertificateId))
                Dim blnCancelCert As Boolean = False
                If contractBO.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCLAF)) Then

                    Dim lngRepPolicyClaimCnt As Long = ReppolicyClaimCount.GetReplacementPolicyClaimCntByClaim(contractBO.Id, Me.Id)

                    If lngRepPolicyClaimCnt = 1 Then
                        blnCancelCert = True
                    ElseIf lngRepPolicyClaimCnt > 1 Then
                        Dim Claimlist As Certificate.CertificateClaimsDV
                        Dim paidReplacementClaimCnt As Integer
                        Claimlist = Certificate.ClaimsForCertificate(Certificate.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        paidReplacementClaimCnt = 0
                        For i = 0 To Claimlist.Count - 1
                            If Claimlist(i)(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER) <> Me.ClaimNumber _
                                AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_Method_Of_Repair_code) = "R" _
                                AndAlso (Not IsDBNull(Claimlist(i)(Certificate.CertificateClaimsDV.COL_Repair_Date))) Then 'other fulfilled replacement claim
                                paidReplacementClaimCnt = paidReplacementClaimCnt + 1
                                If lngRepPolicyClaimCnt - 1 <= paidReplacementClaimCnt Then
                                    blnCancelCert = True
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                End If
                If blnCancelCert Then 'cancel the certificate
                    Dim cancelCertificateData = New CertCancellationData
                    Dim dal As CertCancellationDAL
                    With cancelCertificateData
                        .companyId = Me.CompanyId
                        .dealerId = certBO.DealerId
                        .certificate = certBO.CertNumber
                        .source = certBO.Source
                        .cancellationDate = Me.LossDate.Value.AddDays(1)
                        .cancellationCode = Codes.REASON_CLOSED__TO_BE_REPAIRED
                        .customerPaid = 0
                        .quote = "N"
                        dal = New CertCancellationDAL
                        dal.ExecuteCancelSP(cancelCertificateData)

                        If certBO.LinkedCertNumber <> String.Empty Then 'Cancel linked cert also
                            Dim linkedCert As Certificate.CertificateSearchDV
                            linkedCert = Certificate.GetCertificatesList(certBO.LinkedCertNumber, String.Empty, String.Empty, String.Empty, String.Empty, certBO.Dealer.Dealer)
                            If linkedCert.Count > 0 Then ' linked cert exists
                                If linkedCert(0)(Certificate.CombinedCertificateSearchDV.COL_STATUS_CODE) = "A" Then ' linked cert active
                                    .certificate = certBO.LinkedCertNumber ' cancel the active linked cert
                                    dal.ExecuteCancelSP(cancelCertificateData)
                                End If
                            End If
                        End If
                    End With

                End If
            End If
        End If
    End Sub

    Public Overrides Sub Validate()
        If Me.IsNew Then Me.IsRequiredCheckLossDateForCancelledCert = True 'Always check loss date for Cancelled certificate
        MyBase.Validate()
    End Sub

    Public Sub HandleSpecialServiceClaimCreation()
        ' Try
        Dim claimdal As New ClaimDAL
        Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim certItemCoverage As New CertItemCoverage(CertItemCoverageId)
        Dim certItem As New CertItem(certItemCoverage.CertItemId)
        Dim cert As New Certificate(certItem.CertId)
        Dim claimExsists As Boolean = False
        Dim Denied_MaxOccr_Desc As String = LookupListNew.GetDescriptionFromCode(LookupListCache.LK_DENIED_REASON, Codes.DENIED_MAX__OCCURRENCES_REACHED)
        Dim Denied_MaxOccr_Id As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.DENIED_MAX__OCCURRENCES_REACHED)
        Dim AssurantPaysId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
        Dim timespan As TimeSpan
        Dim oyear As Long
        Dim dsClaim As DataSet '= claimdal.GetClaimsCntByLossDatesSplService(cert.Id, Me.LossDate)

        If SpecialServiceOccurrenceType = Codes.OCCURRENCES_ALWD_SPL_SVC_ONE_PER_YEAR Then
            dsClaim = claimdal.GetClaimsCntByLossDatesSplService(cert.Id, Me.CauseOfLossId, CType(Me.LossDate, Date).ToString("MM/dd/yyyy"))
            If CType(dsClaim.Tables(0).Rows(0)(Me.CLAIM_COUNT), Integer) > 0 Then
                claimExsists = True
            End If
        End If
        If SpecialServiceOccurrenceType = Codes.OCCURRENCES_ALWD_SPL_SVC_ONE_PER_CERT_PERIOD Then
            dsClaim = claimdal.GetClaimsCntByLossDatesSplService(cert.Id, Me.CauseOfLossId)
            If CType(dsClaim.Tables(0).Rows(0)(Me.CLAIM_COUNT), Integer) > 0 Then
                claimExsists = True
            End If
        End If

        If claimExsists = True Then

            If Not MasterClaimNumber Is Nothing Then
                IsUpdatedMasterClaimComment = True
                Dim ds As DataSet = Claim.GetClaimDetailbyClaimNumAndDealer(MasterClaimNumber, cert.DealerId)
                MasterClaimId = GuidControl.ByteArrayToGuid(ds.Tables(0).Rows(0)(ClaimDAL.COL_NAME_CLAIM_ID))

                Dim claimBO As Claim = Me.AddClaim(MasterClaimId)
                claimBO.ProblemDescription = "  ***" & splsvcDesc & "  " &
                                             Denied_MaxOccr_Desc & "***  " _
                                             & vbCrLf & claimBO.ProblemDescription

                StatusCode = Codes.CLAIM_STATUS__DENIED
                DeniedReasonId = Denied_MaxOccr_Id
                WhoPaysId = AssurantPaysId
            Else
                StatusCode = Codes.CLAIM_STATUS__DENIED
                DeniedReasonId = Denied_MaxOccr_Id
                WhoPaysId = AssurantPaysId
            End If

        End If
        'Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
        '   Throw New DataBaseAccessException(ex.ErrorType, ex)
        'End Try

    End Sub

    Public Function GetSpecialServiceValue() As Guid
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        Dim ProductCodeID As Guid
        Dim boSplSvc As New SpecialService
        Dim dalSplSvc As New SpecialServiceDAL
        Dim boCovloss As New CoverageLoss
        Dim dalCovloss As New CoverageLossDAL
        Dim boPrdcode As New ProductCode
        Dim boPrdSplSvc As New ProductSpecialService

        Dim certItemCoverage As New CertItemCoverage(CertItemCoverageId)
        Dim certItem As New CertItem(certItemCoverage.CertItemId)
        Dim cert As New Certificate(certItem.CertId)
        'Dim dlr As New Dealer(cert.DealerId)
        Dim dvProductCodeID As DataView = boPrdcode.GetProductCodeId(cert.DealerId, cert.ProductCode)
        If Not dvProductCodeID Is Nothing AndAlso dvProductCodeID.Count > 0 Then
            If Not dvProductCodeID.Item(0)(DATE_COL_NAME_PRODUCT_CODE_ID).Equals(Guid.Empty) Then
                ProductCodeID = GuidControl.ByteArrayToGuid(dvProductCodeID.Item(0)(DATE_COL_NAME_PRODUCT_CODE_ID))
            End If
        End If

        If Not Me.CauseOfLossId.Equals(Guid.Empty) Then
            Dim dsCovLoss As DataSet = boCovloss.LoadSelectedCovLossFromCovandCauseOfLoss(Me.CauseOfLossId, certItemCoverage.CoverageTypeId)
            If dsCovLoss.Tables(0).Rows.Count > 0 Then
                Dim dsSplSvc As DataSet = boSplSvc.ValidateCoverageLoss(cert.DealerId, GuidControl.ByteArrayToGuid(dsCovLoss.Tables(0).Rows(0)(dalCovloss.COL_NAME_COVERAGE_LOSS_ID)))
                If Not dsSplSvc Is Nothing Then
                    If dsSplSvc.Tables(0).Rows.Count > 0 Then
                        SpecialServiceOccurrenceType = LookupListNew.GetCodeFromId(LookupListNew.GetOccurancesAllowedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GuidControl.ByteArrayToGuid(dsSplSvc.Tables(0).Rows(0)(dalSplSvc.COL_NAME_ALLOWED_OCCURRENCES_ID)))
                        If dsSplSvc.Tables(0).Rows(0)(dalSplSvc.DB_COL_NAME_SERVICE_CLASS_ID).ToString() <> "" Then
                            SpecialServiceServiceClass = LookupListNew.GetCodeFromId(LookupListNew.GetServiceClassLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GuidControl.ByteArrayToGuid(dsSplSvc.Tables(0).Rows(0)(dalSplSvc.DB_COL_NAME_SERVICE_CLASS_ID)))
                        End If
                        If dsSplSvc.Tables(0).Rows(0)(dalSplSvc.DB_COL_NAME_SERVICE_TYPE_ID).ToString() <> "" Then
                            SpecialServiceServiceType = LookupListNew.GetCodeFromId(LookupListNew.GetServiceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GuidControl.ByteArrayToGuid(dsSplSvc.Tables(0).Rows(0)(dalSplSvc.DB_COL_NAME_SERVICE_TYPE_ID)))
                        End If
                        'SpecialServiceServiceClass = LookupListNew.GetCodeFromId(LookupListNew.GetPriceGroupDPLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GuidControl.ByteArrayToGuid(dsSplSvc.Tables(0).Rows(0)(dalSplSvc.DB_COL_NAME_SERVICE_CLASS_ID)))
                        'SpecialServiceServiceType = LookupListNew.GetCodeFromId(LookupListNew.GetPriceGroupDPLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GuidControl.ByteArrayToGuid(dsSplSvc.Tables(0).Rows(0)(dalSplSvc.DB_COL_NAME_SERVICE_TYPE_ID)))
                        ''SpecialServicePriceGroupType = LookupListNew.GetCodeFromId(LookupListNew.GetPriceGroupDPLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), GuidControl.ByteArrayToGuid(dsSplSvc.Tables(0).Rows(0)(dalSplSvc.DB_COL_NAME_SERVICE_TYPE_ID)))
                        SpecialServiceDesc = dsSplSvc.Tables(0).Rows(0)(dalSplSvc.COL_NAME_DESCRIPTION).ToString
                        Dim dsProdSplSvc As DataSet = boPrdSplSvc.LoadProdSplSvcList(GuidControl.ByteArrayToGuid(dsSplSvc.Tables(0).Rows(0)(COL_NAME_SPECIAL_SERVICE_ID)), ProductCodeID)
                        If Not dsProdSplSvc Is Nothing AndAlso dsProdSplSvc.Tables(0).Rows.Count > 0 Then
                            Return yesId
                            'Else
                            '   Return noId
                        End If
                        'Else
                        '   Return noId
                    End If
                End If
                'Else
                '   Return noId
            End If
            'Else
            '   Return noId
        End If
        Return noId
    End Function

    Public Overrides Sub Save(Optional ByVal Transaction As IDbTransaction = Nothing)
        If (Me.StatusCode <> Codes.CLAIM_STATUS__DENIED AndAlso Me.IsNew AndAlso (Me.IsSupervisorAuthorizationRequired)) Then
            Me.StatusCode = Codes.CLAIM_STATUS__PENDING
        End If
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        'Special Service Validation
        ClaimSpecialServiceId = GetSpecialServiceValue()
        If ClaimSpecialServiceId = yesId AndAlso Not Me.IsComingFromPayClaim AndAlso Me.IsNew Then
            HandleSpecialServiceClaimCreation()
        End If
        Dim blnGVSCall As Boolean = False
        If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
            ' Create transaction log header if the service center is integrated with GVS 
            If Not Me.ServiceCenterObject Is Nothing AndAlso Me.ServiceCenterObject.IntegratedWithGVS AndAlso Not Me.ServiceCenterObject.IntegratedAsOf Is Nothing AndAlso (Me.IsNew Or (Not Me.IsNew AndAlso Me.CreatedDateTime.Value >= Me.ServiceCenterObject.IntegratedAsOf.Value)) Then
                If Me.IsNew Then
                    ' Add a NEW extended claim status when open a new claim with GVS integrated
                    Dim newClaimStatusByGroupId As Guid = ClaimStatusByGroup.GetClaimStatusByGroupID(DALObjects.ClaimStatusDAL.NEW_EXTENDED_CLAIM_STATUS)
                    If Me._claimStatusBO Is Nothing Then
                        Me.AddExtendedClaimStatus(Guid.Empty)
                        _claimStatusBO.ClaimId = Me.Id
                        _claimStatusBO.ClaimStatusByGroupId = newClaimStatusByGroupId
                        _claimStatusBO.StatusDate = DateTime.Now
                        _claimStatusBO.HandelTimeZoneForClaimExtStatusDate(Me)
                    End If
                End If
            End If
            blnGVSCall = True
        End If

        Dim bIsNew As Boolean = Me.IsNew
        MyBase.Save(Transaction)

        If blnGVSCall Then
            ' Create transaction log header if the service center is integrated with GVS 
            Me.HandleGVSTransactionCreation(Guid.Empty, bIsNew)
        End If

        'REQ-1333, cancel certificate after replacement based on contract configuration
        CancelCertBasedOnContractReplacementPolicy()

        'REQ-5404
        If Me.DealerTypeCode = Codes.DEALER_TYPES__VSC AndAlso Me.CurrentOdometer.Value > 0 AndAlso Me.Certificate.StatusCode = Codes.CERTIFICATE_STATUS__ACTIVE Then
            Dim t As Thread = New Thread(AddressOf Me.CancelCertBasedOnCurrentOdometerExceedsAIZMiles)
            t.Start()
        End If

    End Sub


    Public Sub PrePopulate(ByVal serviceCenterId As Guid, ByVal certItemCoverageId As Guid, ByVal mstrClaimNumber As String, ByVal DateOfLoss As DateType, Optional ByVal RecoveryButtonClick As Boolean = False, Optional ByVal WsUse As Boolean = False, Optional ByVal ComingFromDenyClaim As Boolean = False, Optional ByVal comingFromCert As Boolean = False, Optional ByVal callerName As String = Nothing, Optional ByVal problemDescription As String = Nothing, Optional ByVal ClaimedEquipment As ClaimEquipment = Nothing)

        MyBase.PrePopulate(certItemCoverageId, mstrClaimNumber, DateOfLoss, RecoveryButtonClick, WsUse, ComingFromDenyClaim, comingFromCert, callerName, problemDescription, , ClaimedEquipment)

        'REQ-5546
        If serviceCenterId.Equals(Guid.Empty) Then
            Dim objCountry As New Country(Me.Company.CountryId)

            If Not objCountry.DefaultSCId.Equals(Guid.Empty) Then
                Me.ServiceCenterId = objCountry.DefaultSCId
            Else
                Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.SERVICE_CENTER_IS_REQUIRED_ERROR, GetType(Claim), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(Claim).FullName)
                'Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.SERVICE_CENTER_IS_REQUIRED_ERROR)
            End If
        Else
            Me.ServiceCenterId = serviceCenterId
        End If


        Dim dv As DataView = Me.GetRePairPricesByMethodOfRepair

        Me.AuthorizedAmount = 0
        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
            Me.AuthorizedAmount = CDec(dv.Table.Rows(0)(COL_PRICE_DV))
            'Else
            '   Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
        End If

        If Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
            Me.Deductible = New DecimalType(0)
            Me.LiabilityLimit = New DecimalType(0)
            Me.DeductiblePercent = New DecimalType(0)
            Me.DeductiblePercentID = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        Else
            Me.LiabilityLimit = Me.CertificateItemCoverage.LiabilityLimits
            PrepopulateDeductible()
        End If

    End Sub

    Public Function GetRePairPricesByMethodOfRepair() As DataView
        Dim dv As DataView
        Dim servCenter As New ServiceCenter(Me.ServiceCenterId)
        Dim equipConditionid As Guid
        Dim equipmentId As Guid
        Dim equipClassId As Guid
        'Dim servCenter As ServiceCenter

        'get the equipment information'if equipment not used then get the prices based on risktypeid
        If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
            equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
            If Me.ClaimedEquipment Is Nothing OrElse Me.ClaimedEquipment.EquipmentBO Is Nothing Then
                Me.CreateClaimedEquipment(Me.CertificateItem.CopyEnrolledEquip_into_ClaimedEquip())
            End If

            If Not Me.ClaimedEquipment Is Nothing AndAlso Not Me.ClaimedEquipment.EquipmentBO Is Nothing Then
                equipmentId = Me.ClaimedEquipment.EquipmentId
                equipClassId = Me.ClaimedEquipment.EquipmentBO.EquipmentClassId
                dv = PriceListDetail.GetRepairPricesforMethodofRepair(Me.MethodOfRepairId, Me.CompanyId, servCenter.Code, Me.RiskTypeId, DateTime.Now,
                           Me.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
            End If
        Else
            dv = PriceListDetail.GetRepairPricesforMethodofRepair(Me.MethodOfRepairId, Me.CompanyId, servCenter.Code, Me.RiskTypeId, DateTime.Now,
                           Me.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
        End If

        'If (Not ServiceCenterId = Guid.Empty) Then
        '    servCenter = New ServiceCenter(Me.ServiceCenterId)
        'Else
        '    Return dv
        'End If

        'If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
        '    equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
        '    If Me.ClaimedEquipment Is Nothing OrElse Me.ClaimedEquipment.EquipmentBO Is Nothing Then
        '        Me.CreateClaimedEquipment(Me.CertificateItem.CopyEnrolledEquip_into_ClaimedEquip())
        '    End If

        '    If Not ClaimedEquipment Is Nothing AndAlso Not ClaimedEquipment.EquipmentBO Is Nothing Then
        '        equipmentId = ClaimedEquipment.EquipmentBO.Id
        '        equipClassId = ClaimedEquipment.EquipmentBO.EquipmentClassId

        '    ElseIf Not ClaimedEquipment Is Nothing Then
        '        equipmentId = Equipment.FindEquipment(Dealer.Dealer, ClaimedEquipment.Manufacturer, ClaimedEquipment.Model, Date.Today)
        '        If (Not equipmentId = Guid.Empty) Then
        '            equipClassId = New Equipment(equipmentId).EquipmentClassId
        '        End If

        '    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Dealer.UseEquipmentId) = Codes.YESNO_Y Then
        '        Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
        '        Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
        '    End If
        'End If

        'dv = PriceListDetail.GetRepairPricesforMethodofRepair(Me.MethodOfRepairId, Me.CompanyId, servCenter.Code, Me.RiskTypeId, DateTime.Now,
        '       Me.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)

        Return dv
    End Function

    Public Function GetPricesForServiceType(ByVal serviceClassCode As String, ByVal serviceTypeCode As String) As DataView

        Dim dv As DataView
        Dim servCenter As New ServiceCenter(Me.ServiceCenterId)
        Dim equipConditionid As Guid
        Dim equipmentId As Guid
        Dim equipClassId As Guid

        'get the equipment information'if equipment not used then get the prices based on risktypeid
        If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
            equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
            If Not Me.ClaimedEquipment Is Nothing AndAlso Not Me.ClaimedEquipment.EquipmentBO Is Nothing Then
                equipmentId = Me.ClaimedEquipment.EquipmentId
                equipClassId = Me.ClaimedEquipment.EquipmentBO.EquipmentClassId
                dv = PriceListDetail.GetPricesForServiceType(Me.CompanyId, Me.ServiceCenterObject.Code, Me.RiskTypeId,
                              DateTime.Now, Me.Certificate.SalesPrice.Value,
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, serviceClassCode),
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, serviceTypeCode),
                              equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
            End If
        Else
            dv = PriceListDetail.GetPricesForServiceType(Me.CompanyId, Me.ServiceCenterObject.Code, Me.RiskTypeId,
                              DateTime.Now, Me.Certificate.SalesPrice.Value,
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, serviceClassCode),
                              LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, serviceTypeCode),
                              equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
        End If

        Return dv

    End Function

    Public Sub CancelCertBasedOnCurrentOdometerExceedsAIZMiles()
        If Me.SourceForCancellation <> "GALAXY_INSERT_CLAIM_WS" Then
            Me.PopulateRequiredData()
        End If
        If Me.CurrentOdometer.Value > 0 AndAlso Me._cert_RemainingBalance <= 0 Then
            Dim _intMaxAssurantMileageLimit As Integer = 0
            If Me._vehicle_condition = Codes.VEHICLE_COND__NEW Then
                _intMaxAssurantMileageLimit = Me._MFG_MAX_Mileage_Limit + Me._coverage_mi_km
            ElseIf Me._vehicle_condition = Codes.VEHICLE_COND__USED Then
                _intMaxAssurantMileageLimit = Me._coverage_mi_km
            End If

            If _intMaxAssurantMileageLimit > 0 AndAlso Me.CurrentOdometer.Value > _intMaxAssurantMileageLimit Then
                Dim dal As New CertCancellationDAL
                dal.VSC_CancelPolicy(Me.CertificateId, Me.CANCELLATION_REASON_CODE, ElitaPlusIdentity.Current.ActiveUser.Id, Me.SourceForCancellation, Nothing, _intMaxAssurantMileageLimit, Me.CurrentOdometer)

            End If
        End If
    End Sub
    Private Sub PopulateRequiredData()

        Dim _CertificateDetailDataSet As DataSet = Certificate.GalaxyGetCertificateDetail(Me.CertificateNumber, Me.DealerCode)
        If Not _CertificateDetailDataSet Is Nothing AndAlso _CertificateDetailDataSet.Tables.Count > 0 AndAlso _CertificateDetailDataSet.Tables(0).Rows.Count > 0 Then
            _MFG_MAX_Mileage_Limit = CType(_CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_MFG_MAX_MILEAGE_LIMIT), Integer)
            _vehicle_condition = _CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_MFG_NEW_USED)
            _cert_RemainingBalance = _CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_REMAINING_BALANCE)

            Dim dsItemCoverages As DataSet = CertItemCoverage.LoadAllItemCoveragesForGalaxyClaim(Me.CertificateId)

            If Not dsItemCoverages Is Nothing AndAlso dsItemCoverages.Tables.Count > 0 AndAlso dsItemCoverages.Tables(0).Rows.Count > 0 Then
                Me._coverage_mi_km = dsItemCoverages.Tables(0).Compute("Max(coverage_km_mi)", "")
            End If

        End If

    End Sub

    Public Sub PreValidate()
        MyBase.Validate()
    End Sub


    Public Sub UpdateAssociatedClaimInfoBeforeSave()
        'Generate a New Comment if the ClaimBO is Dirty and no new comment has already been added
        Dim commentBO As Comment = Me.AddNewComment()
        commentBO.CallerName = Me.CallerName
        commentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CUSTOMER_CALL)
        commentBO.Comments = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COMMENT_TYPES, commentBO.CommentTypeId)
    End Sub

    Public Function CreateNewClaim(Optional ByVal oldClaimId As Guid = Nothing) As Claim

        Dim newClaimBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)(Me, Me.Dataset)
        newClaimBO.CertItemCoverageId = Me.CertItemCoverageId
        newClaimBO.ServiceCenterId = Me.ServiceCenterId
        newClaimBO.CopyFrom(Me, True)
        newClaimBO._isDSCreator = True
        newClaimBO.CalculateFollowUpDate()
        newClaimBO.RepairDate = Nothing
        newClaimBO.PickUpDate = Nothing
        newClaimBO.VisitDate = Nothing
        newClaimBO.AuthorizationNumber = Nothing
        newClaimBO.LoanerReturnedDate = Nothing
        newClaimBO.Source = Nothing
        newClaimBO.AuthorizedAmount = New DecimalType(0)
        newClaimBO.RepairCodeId = Guid.Empty
        newClaimBO.RepairCode = Nothing
        newClaimBO.LastOperatorName = ElitaPlusIdentity.Current.ActiveUser.UserName
        newClaimBO.ClaimsAdjuster = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        newClaimBO.ClaimsAdjusterName = ElitaPlusIdentity.Current.ActiveUser.UserName
        'Set the loaner center ID to that of the source claim
        newClaimBO.LoanerCenterId = Me.LoanerCenterId
        newClaimBO.InvoiceProcessDate = Nothing
        newClaimBO.ProblemDescription = Nothing
        newClaimBO.SpecialInstruction = Nothing
        newClaimBO.ContactSalutationID = Guid.Empty
        newClaimBO.CallerSalutationID = Guid.Empty
        newClaimBO.MasterClaimNumber = Me.MasterClaimNumber


        'REQ 1106 start
        newClaimBO.CreateEnrolledEquipment()
        If ((Not newClaimBO.CertificateItem Is Nothing) _
             AndAlso (Not newClaimBO.CertificateItem.ManufacturerId.Equals(Guid.Empty))) Then
            newClaimBO.CreateClaimedEquipment(newClaimBO.CertificateItem.CopyEnrolledEquip_into_ClaimedEquip())
            newClaimBO.CreateReplacementOptions()
        ElseIf (newClaimBO.Certificate.Product.AllowRegisteredItems = "YESNO-Y") Then
            If (Not oldClaimId = Nothing) Then
                Dim certRegItemId As Guid = GetCertRegisterItemId(oldClaimId)
                Dim CertificateRegisteredItem As CertRegisteredItem = New CertRegisteredItem(certRegItemId)
                newClaimBO.CreateReplaceClaimedEquipment(CertificateRegisteredItem.CopyEnrolledEquip_into_ClaimedEquip)
            End If
        End If
        'REQ 1106 end

        Return (newClaimBO)

    End Function

    Public Shared Sub PopulateNewReplacementClaim(ByRef newClaimBO As Claim)

        'For a new Replacement Claim
        With newClaimBO
            If .ClaimNumber.ToUpper.EndsWith("S") Then
                .ClaimNumber = .ClaimNumber.Substring(0, .ClaimNumber.Length - 1)
            End If
            .ClaimNumber &= "R"
            .ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)
            .MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)
            newClaimBO.PrepopulateDeductible()
            '.StatusCode = Codes.CLAIM_STATUS__ACTIVE
            .ReasonClosedId = Guid.Empty
            .RepairDate = Nothing
            .ClaimClosedDate = Nothing

            .AttachIssues()
            .LoadResponses()
        End With

    End Sub

    Public Sub PopulateNewServiceWarrantyClaim(ByRef newClaimBO As Claim)

        'For a new Service Warranty Claim
        With newClaimBO
            If .ClaimNumber.ToUpper.EndsWith("S") Then
                .ClaimNumber = .ClaimNumber.Substring(0, .ClaimNumber.Length - 1)
            End If
            .ClaimNumber &= "S"
            .ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REWORK)
            .StatusCode = Codes.CLAIM_STATUS__ACTIVE
            .ReasonClosedId = Guid.Empty
            .RepairDate = Nothing
            .ClaimClosedDate = Nothing
            .ProblemDescription = Me.ProblemDescription
            .SpecialInstruction = Me.SpecialInstruction
        End With

    End Sub

    Public Function NumberOfAvailableClaims(ByVal certItemCoverageId As Guid, ByVal createdDate As Date, ByVal redoClaimID As Guid) As Boolean
        Dim dal As New ClaimDAL
        Return dal.NumberOfAvailableClaims(certItemCoverageId, createdDate, redoClaimID)
    End Function

    Public Function IsClaimActive(ByVal ClaimId As Guid) As Integer
        Dim dal As New ClaimDAL
        Return dal.IsClaimActive(ClaimId)
    End Function

    Public Function IsClaimAuthNumberExists(ByVal ClaimId As Guid, ByVal AuthNumber As String) As Integer
        Dim dal As New ClaimDAL
        Return dal.IsClaimAuthNumberExists(ClaimId, AuthNumber)
    End Function
    Public Function GetDepreciationSchedule(ByVal contractId As Guid) As DataView
        Dim dal As New ClaimDAL
        Dim dv As DataView, ds As DataSet
        ds = dal.GetDepreciationSchedule(contractId)
        dv = New DataView(ds.Tables(0))
        Return dv
    End Function

    Public Function Handle_Replaced_Items(ByVal replaceAll As Integer, ByVal claimId As Guid, ByVal certId As Guid, ByVal certItemCoverageId As Guid, ByVal replaceDate As Date) As Integer
        Dim dal As New ClaimDAL

        Return dal.Handle_Replaced_Items(replaceAll, claimId, certId, certItemCoverageId, replaceDate)
    End Function

    Public Sub ProcessExistingClaim()
        'Perform the Replacement Claim processing logic (for an existing Claim that is NOT a Service Warranty Claim)

        'This is a Repair Claim
        'Get the EstimatePrice for the PriceListDetail associated with the ServiceCenter for the Claim
        If (Not (Me.ServiceCenterId.Equals(Guid.Empty))) Then
            Dim serviceCenterBO As ServiceCenter = New ServiceCenter(Me.ServiceCenterId)
            If (Not (serviceCenterBO.PriceListCode = String.Empty)) Then
                'Dim priceGroupBO As PriceGroup = New PriceGroup(serviceCenterBO.PriceGroupId)
                'Get the Price List ID from Price List Code
                'Dim priceListID As Guid = Guid.Empty
                'Dim list As DataView = LookupListNew.GetPriceListLookupList(serviceCenterBO.CountryId)
                'priceListID = LookupListNew.GetIdFromCode(list, serviceCenterBO.PriceListCode)
                'Dim priceListBO As PriceList = New PriceList(priceListID)
                'Me.CertItemCoverageId = CertItemCoverageId
                'Dim certItemCoverage As New CertItemCoverage(CertItemCoverageId)
                'Dim certItem As New CertItem(certItemCoverage.CertItemId)
                'Dim certificate As New Certificate(certItem.CertId)
                ' Dim priceGroupDetailBO As PriceGroupDetail = priceGroupBO.GetCurrentPriceGroupDetail(RiskTypeId, certificate.SalesPrice.Value)
                'calculating the estimate price
                Dim nEstimatePrice As New DecimalType(0)

                Dim price As Decimal = 0
                price = 0
                Dim dvEstimate As DataView = Me.GetPricesForServiceType(Codes.SERVICE_CLASS__REPAIR, Codes.SERVICE_TYPE__ESTIMATE_PRICE)

                If Not dvEstimate Is Nothing AndAlso dvEstimate.Count > 0 Then
                    price = CDec(dvEstimate(0)(COL_PRICE_DV))
                    nEstimatePrice = price
                End If
                ''''''''''''''''''''''''''''''''''''''''''
                If PreserveAuthAmount() Then
                    Me.AuthorizedAmount = Me.AuthorizedAmount
                Else
                    Me.AuthorizedAmount = nEstimatePrice
                End If


                If (nEstimatePrice = 0) Then
                    If Not Me.PreserveAuthAmount() Then
                        Me.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPLACED)
                        Me.CloseTheClaim()
                    End If
                    If PreserveAuthAmount() Then
                        Me.AuthorizedAmount = Me.AuthorizedAmount
                    Else
                        Me.AuthorizedAmount = New DecimalType(ZERO_DECIMAL)
                    End If
                Else
                    'EstimatePrice <> 0, so set the ClaimActivityCode for the Existing RepairClaim = "TBREP"
                    Me.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Assurant.ElitaPlus.BusinessObjectsNew.Codes.CLAIM_ACTIVITY__TO_BE_REPLACED)
                End If
                If Me.Deductible.Value > 0 Then
                    Me.Deductible = New DecimalType(ZERO_DECIMAL)
                End If
            End If
        End If
        'If (Not (serviceCenterBO.PriceGroupId.Equals(Guid.Empty))) Then
        '    Dim priceGroupBO As PriceGroup = New PriceGroup(serviceCenterBO.PriceGroupId)
        '    Me.CertItemCoverageId = CertItemCoverageId
        '    Dim certItemCoverage As New CertItemCoverage(CertItemCoverageId)
        '    Dim certItem As New CertItem(certItemCoverage.CertItemId)
        '    Dim certificate As New Certificate(certItem.CertId)
        '    Dim priceGroupDetailBO As PriceGroupDetail = priceGroupBO.GetCurrentPriceGroupDetail(RiskTypeId, certificate.SalesPrice.Value)

        '    If (Not (priceGroupDetailBO Is Nothing)) Then
        '        Dim dTaxRate As Decimal = GetIVATaxRate()
        '        Me.AuthorizedAmount = priceGroupDetailBO.EstimatePrice * (1 + dTaxRate)
        '        'Me.AuthorizedAmount = priceGroupDetailBO.EstimatePrice
        '        If (priceGroupDetailBO.EstimatePrice.Value.Equals(ZERO_DECIMAL)) Then
        '            'EstimatePrice = 0, so set the ReasonClosed = "TBREP" for existing Repair Claim AND
        '            'Close the existing Repair Claim (set the StatusCode = "C")
        '            Me.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPLACED)
        '            Me.CloseTheClaim()
        '        Else
        '            'EstimatePrice <> 0, so set the ClaimActivityCode for the Existing RepairClaim = "TBREP"
        '            Me.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Assurant.ElitaPlus.BusinessObjectsNew.Codes.CLAIM_ACTIVITY__TO_BE_REPLACED)
        '        End If
        '    Else
        '        Me.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPLACED)
        '        Me.CloseTheClaim()
        '        Me.AuthorizedAmount = New DecimalType(ZERO_DECIMAL)
        '    End If
        '    If Me.Deductible.Value > 0 Then
        '        Me.Deductible = New DecimalType(ZERO_DECIMAL)
        '    End If
        'End If
    End Sub
    Public Function PreserveAuthAmount() As Boolean
        Dim certBO As Certificate = New Certificate(Certificate.Id)
        If certBO.Dealer.AttributeValues.Where(Function(av) av.Attribute.UiProgCode = Codes.PRESERVE_AUTH_AMOUNT_AT_REPLACE_ITEM).Count > 0 Then
            Dim attributeval As String = certBO.Dealer.AttributeValues.Where(Function(av) av.Attribute.UiProgCode = Codes.PRESERVE_AUTH_AMOUNT_AT_REPLACE_ITEM).FirstOrDefault().Value.ToString()
            If attributeval.Equals(Codes.YESNO_Y) Then
                Return True
            End If
        Else
            Return False
        End If

    End Function

    Public Function GetManufacturerIdForServiceCenter() As Guid

        Dim cicBO As CertItemCoverage = New CertItemCoverage(Me.CertItemCoverageId)
        Dim ciBO As CertItem = New CertItem(cicBO.CertItemId)

        Return (ciBO.ManufacturerId)

    End Function

    Public Function GetCoverageTypeCodeForServiceCenter() As String

        Dim cicBO As CertItemCoverage = New CertItemCoverage(Me.CertItemCoverageId)
        Dim ciBO As CertItem = New CertItem(cicBO.CertItemId)

        Return (ciBO.GetCoverageTypeCode(cicBO.CoverageTypeId))

    End Function

    Public Sub SetAuthorizedAmount()

        'It is a Repair Claim
        'commented for REQ-1106
        ''Dim pgDetailBO As PriceGroupDetail = Me.GetCurrentPriceGroupDetail
        ''If (Not (pgDetailBO Is Nothing)) Then
        ''Dim dTaxRate As Decimal = GetIVATaxRate()
        ''    'Found the Correct Price Group entry for the Service Center
        ''    If (Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__CARRY_IN) Then
        ''        Me.AuthorizedAmount = pgDetailBO.CarryInPrice * (1 + dTaxRate)
        ''    ElseIf (Me.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__AT_HOME) Then
        ''        Me.AuthorizedAmount = pgDetailBO.HomePrice * (1 + dTaxRate)
        ''    End If
        ''Else
        ''    'Did NOT find the Correct Price Group entry for the Service Center
        ''    Me.AuthorizedAmount = New DecimalType(0D)
        ''End If

        Dim dv As DataView = Me.GetRePairPricesByMethodOfRepair
        'Me.AuthorizedAmount = 0
        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
            Me.AuthorizedAmount = CDec(dv.Table.Rows(0)(COL_PRICE_DV))
            'Else
            '   Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, Nothing, Messages.PRICE_LIST_NOT_FOUND)
        End If

    End Sub

    Public Function GetIVATaxRate() As Decimal
        Dim retval As Decimal = 0D, taxtypeID As Guid, dtEff As Date
        Dim svcCtrBO As New ServiceCenter(Me.ServiceCenterId)

        If Not svcCtrBO.IvaResponsibleFlag Then
            Return retval
        End If
        If Me.CreatedDate Is Nothing Then
            dtEff = System.DateTime.Now
        Else
            dtEff = Me.CreatedDate.Value
        End If
        If MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            'Replacement claim, find the applicable IVA tax type
            Dim ProductPrice As Decimal = Me.Certificate.SalesPrice.Value
            Dim dal As ClaimInvoiceDAL = New ClaimInvoiceDAL
            dal.GetReplacementTaxType(ServiceCenterId, RiskTypeId, dtEff, ProductPrice, taxtypeID)
            If taxtypeID = Guid.Empty Then
                taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, CountryTax.TaxTypeCode.REPAIRS)
            End If
        Else
            taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, CountryTax.TaxTypeCode.REPAIRS)
        End If

        'REQ 1150
        Return CountryTax.GetTaxRate(svcCtrBO.Address.CountryId, taxtypeID, svcCtrBO.Address.RegionId, dtEff, Me.Certificate.DealerId)
    End Function

    'Public Function GetCurrentPriceGroupDetail() As PriceGroupDetail
    '    Dim svcCtrBO As ServiceCenter
    '    svcCtrBO = New ServiceCenter(Me.ServiceCenterId)
    '    'Dim pgBO As PriceGroup = New PriceGroup
    '    ''pgBO = New PriceGroup(svcCtrBO.PriceGroupId)
    '    'Dim pgdetailBO As PriceGroupDetail
    '    'pgdetailBO = pgBO.GetCurrentPriceGroupDetail(RiskTypeId, Me.Certificate.SalesPrice.Value)
    '    'If Not pgdetailBO Is Nothing Then
    '    '    Return pgdetailBO
    '    'End If


    '    Me.ServiceCenterId = ServiceCenterId
    '    Dim servCenter As New ServiceCenter(ServiceCenterId)
    '    Dim equipConditionId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
    '    Dim dv As DataView = PriceListDetail.GetRepairPrices(Me.CompanyId, svcCtrBO.Code, DateTime.Now, Me.RiskTypeId, _
    '                                                                        Me.Certificate.SalesPrice.Value, Me.ClaimedEquipment.EquipmentBO.EquipmentClassId, _
    '                                                                        Me.ClaimedEquipment.EquipmentBO.Id, equipConditionId, Me.Dealer.Id)

    'End Function

    Public Sub SetPickUpDateFromLoanerReturnedDate() Implements IInvoiceable.SetPickUpDateFromLoanerReturnedDate

        If Not Me.LoanerReturnedDate Is Nothing Then Me.PickUpDate = Me.LoanerReturnedDate

    End Sub

    Public Sub UpdateClaimAuthorizedAmount(ByVal objClaimAuthDetail As ClaimAuthDetail)
        Dim subTotal As Decimal = 0
        Dim tax As Decimal = 0
        Dim Total As Decimal
        With objClaimAuthDetail
            If Not .LaborAmount Is Nothing Then subTotal += .LaborAmount.Value
            If Not .PartAmount Is Nothing Then subTotal += .PartAmount.Value
            If Not .ServiceCharge Is Nothing Then subTotal += .ServiceCharge.Value
            If Not .TripAmount Is Nothing Then subTotal += .TripAmount.Value
            If Not .OtherAmount Is Nothing Then subTotal += .OtherAmount.Value
            If Not .ShippingAmount Is Nothing Then subTotal += .ShippingAmount.Value
            If Not .DispositionAmount Is Nothing Then subTotal += .DispositionAmount.Value
            If Not .DiagnosticsAmount Is Nothing Then subTotal += .DiagnosticsAmount.Value
            If Not .TotalTaxAmount Is Nothing Then tax = .TotalTaxAmount.Value

        End With
        Total = subTotal + tax
        Me.AuthorizedAmount = Total

        '' Auth amt is being changed, calculate the new deductible amt if by percent
        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.DeductiblePercentID) = Codes.YESNO_Y Then
            If Not Me.DeductiblePercent Is Nothing Then
                If Me.DeductiblePercent.Value > 0 Then
                    Calculate_deductible_if_by_percentage()
                End If
            End If
        End If

    End Sub

    Public Function AddClaim(ByVal claimID As Guid) As Claim
        Dim objClaim As Claim

        If Not claimID.Equals(Guid.Empty) Then
            objClaim = ClaimFacade.Instance.GetClaim(Of Claim)(claimID, Me.Dataset)
        Else
            objClaim = ClaimFacade.Instance.CreateClaim(Of Claim)(Me.Dataset)
        End If

        Return objClaim
    End Function

    Public Function AddExtendedClaimStatus(ByVal claimStatusId As Guid) As ClaimStatus Implements IInvoiceable.AddExtendedClaimStatus
        Return MyBase.AddExtendedClaimStatus(claimStatusId)
    End Function

    Public Function CalculateAuthorizedAmountFromPGPrices() As Boolean

        Dim AssurantPays As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
        Dim dTaxRate As Decimal
        Dim nCarryInPrice As New DecimalType(0)
        Dim nCleaningPrice As New DecimalType(0)
        Dim nEstimatePrice As New DecimalType(0)
        Dim nHomePrice As New DecimalType(0)
        Dim nOtherPrice As New DecimalType(0)
        Dim nReplacementCost As New DecimalType(0)
        Dim nReplacementPrice As New DecimalType(0)
        Dim nZeroValue As New DecimalType(0)
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")
        Dim splSvcPriceGrp As String

        'Dim pgDetail As PriceGroupDetail = GetCurrentPriceGroupDetail()
        'If Not pgDetail Is Nothing Then dTaxRate = GetIVATaxRate()

        'If Not pgDetail Is Nothing Then
        '    With pgDetail
        '        If MethodOfRepairCode = Codes.METHOD_OF_REPAIR__SEND_IN Then
        '            .CarryInPrice = .SendInPrice.Value * (1 + dTaxRate)
        '        ElseIf MethodOfRepairCode = Codes.METHOD_OF_REPAIR__PICK_UP Then
        '            .CarryInPrice = .PickUpPrice.Value * (1 + dTaxRate)
        '        Else
        '            .CarryInPrice = .CarryInPrice.Value * (1 + dTaxRate)
        '        End If
        '        .CleaningPrice = .CleaningPrice.Value * (1 + dTaxRate)
        '        .EstimatePrice = .EstimatePrice.Value * (1 + dTaxRate)
        '        .HomePrice = .HomePrice.Value * (1 + dTaxRate)
        '    End With
        '    nCarryInPrice = pgDetail.CarryInPrice
        '    nCleaningPrice = pgDetail.CleaningPrice
        '    nEstimatePrice = pgDetail.EstimatePrice
        '    nHomePrice = pgDetail.HomePrice
        'End If
        'Get the price
        Dim dv As DataView = Me.GetRePairPricesByMethodOfRepair
        Dim price As Decimal = 0
        If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
            price = CDec(dv.Table.Rows(0)(COL_PRICE_DV))

            Select Case dv.Table.Rows(0)(PriceListDetailDAL.COL_NAME_SERVICE_TYPE_CODE).ToString()
                Case Codes.METHOD_OF_REPAIR_SEND_IN
                    nCarryInPrice = price 'CDec(dv.Table.Rows(0)(COL_PRICE_DV))
                Case Codes.METHOD_OF_REPAIR_PICK_UP
                    nCarryInPrice = price
                Case Codes.METHOD_OF_REPAIR_CLEANING
                    nCleaningPrice = price
                Case Codes.METHOD_OF_REPAIR_AT_HOME
                    nHomePrice = price
                Case Codes.METHOD_OF_REPAIR_DISCOUNTED
                    nReplacementCost = price
                Case Codes.METHOD_OF_REPAIR_REPLACEMENT
                    nReplacementPrice = price
            End Select
            ' Else
            '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, , Messages.PRICE_LIST_NOT_FOUND)
        End If
        'calculating the estimate price
        price = 0
        Dim dvEstimate As DataView = Me.GetPricesForServiceType(Codes.SERVICE_CLASS__REPAIR, Codes.SERVICE_TYPE__ESTIMATE_PRICE)

        If Not dvEstimate Is Nothing AndAlso dvEstimate.Count > 0 Then
            price = CDec(dvEstimate(0)(COL_PRICE_DV))
            nEstimatePrice = price
        End If

        If ClaimSpecialServiceId = yesId Then
            With Me
                splSvcPriceGrp = .SpecialServiceServiceType

                If splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_CARRY_IN Then 'carry in price  
                    .AuthorizedAmount = nCarryInPrice
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_CLEANING Then 'cleaning price
                    .AuthorizedAmount = nCleaningPrice
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_ESTIMATE Then 'estimate price
                    .AuthorizedAmount = nEstimatePrice
                ElseIf splSvcPriceGrp = Codes.PRICEGROUP_SPL_SVC_HOME Then 'home price
                    .AuthorizedAmount = nHomePrice
                Else 'Manual
                    Return True
                End If
                Return False
            End With
        End If
        Return False
    End Function

    Private Function ChangeCoverage(ByVal claimSpecialServiceId As Guid) As Boolean
        Dim AuthAmtText As String
        Dim AssurantPaysAmt As New DecimalType(0)
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), Codes.YESNO_Y)
        Dim AssurantPaysId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
        Dim oClaimDal As ClaimDAL
        If claimSpecialServiceId = yesId Then
            Me.WhoPaysId = AssurantPaysId
            Me.Deductible = New DecimalType(ZERO_DECIMAL)
            Dim myContractId As Guid = Contract.GetContractID(Me.CertificateId)
            If Me.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                Dim al As ArrayList = Me.CalculateLiabilityLimit(Me.CertificateId, myContractId, Me.CertItemCoverageId, Me.LossDate)
                Me.LiabilityLimit = CType(al(0), Decimal)
            End If
        End If
    End Function

    Public Overrides Sub CloseTheClaim() Implements IInvoiceable.CloseTheClaim
        MyBase.CloseTheClaim()
    End Sub

    Public Function AddNewComment() As Comment
        Dim c As Comment = MyBase.AddNewComment
        If Not Me.SpecialInstruction Is Nothing Then c.Comments &= Environment.NewLine & Me.SpecialInstruction
        Return c
    End Function

#End Region

#Region "GVS"

    Public Sub HandleGVSTransactionCreation(ByVal commentId As Guid, ByVal pIsNew As Nullable(Of Boolean)) Implements IInvoiceable.HandleGVSTransactionCreation
        ' Create transaction log header if the service center is integrated with GVS
        If Not Me.ServiceCenterObject Is Nothing AndAlso Me.ServiceCenterObject.IntegratedWithGVS AndAlso Not Me.ServiceCenterObject.IntegratedAsOf Is Nothing AndAlso (Me.IsNew Or (Not Me.IsNew AndAlso Me.CreatedDateTime.Value >= Me.ServiceCenterObject.IntegratedAsOf.Value)) Then
            ' GVS Function Type = NEW_CLAIM or UPDATE_CLAIM
            ' Question: need to create the log for Replace or Service Warranty claim?
            Dim dal As New ClaimDAL
            dal.GVSTransactionCreation(Me.Id,
                                       If(pIsNew.GetValueOrDefault(Me.IsNew), "1", "2"),
                                       If(commentId.Equals(Guid.Empty), Nothing, "COMMENT_ID_BEGIN" & GuidControl.GuidToHexString(commentId) & "COMMENT_ID_END"))
        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    'Manually added method
    Public Shared Function getActiveClaimsList(ByVal claimNumber As String, ByVal customerName As String,
                                      ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                                      ByVal authorizedAmount As String, ByVal Dealerid As Guid, Optional ByVal sortBy As String = ClaimDAL.SORT_BY_CLAIM_NUMBER) As ClaimSearchDV

        Try
            Dim compIds As ArrayList
            Dim dal As New ClaimDAL
            Dim externalUserDealerId As Guid = Guid.Empty
            Dim dealerGroupCode As String = ""
            With ElitaPlusIdentity.Current.ActiveUser
                compIds = .Companies
                If Not (Dealerid = Guid.Empty) Then
                    externalUserDealerId = Dealerid
                End If
                If .IsDealer Then
                    externalUserDealerId = .ScDealerId
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroupCode = ExternalUserDealergroup
                End If
            End With

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber Is Nothing)) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (customerName Is Nothing)) Then
                customerName = customerName.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (serviceCenterName Is Nothing)) Then
                serviceCenterName = serviceCenterName.ToUpper
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso
                serviceCenterName.Equals(String.Empty) AndAlso authorizationNumber.Equals(String.Empty) AndAlso
                authorizedAmount.Equals(String.Empty) AndAlso externalUserDealerId.Equals(Guid.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If


            Return New ClaimSearchDV(dal.LoadActiveClaimList(compIds,
                                                  claimNumber, customerName, serviceCenterName,
                                                  authorizationNumber, authorizedAmount, externalUserDealerId, sortBy, dealerGroupCode).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimslist(ByVal cert_item_id As Guid, ByVal begin_date As Date,
                                         ByVal end_date As Date,
                                         ByVal isManufacturerWaranty As Boolean) As ClaimSearchDV

        Try
            Dim dal As New ClaimDAL

            If isManufacturerWaranty Then
                Return New ClaimSearchDV(dal.LoadListWithManuf(cert_item_id, begin_date, end_date).Tables(0))
            Else
                Return New ClaimSearchDV(dal.LoadListWithOutManuf(cert_item_id, begin_date, end_date).Tables(0))
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetCertClaims(ByVal certId As Guid) As DataView
        Try
            Dim dal As New ClaimDAL
            Dim ds As New DataSet

            ds = dal.GetCertClaims(certId)
            Return ds.Tables(ClaimDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimslistByCoverageId(ByVal cert_item_coverage_id As Guid, ByVal begin_date As Date,
                                         ByVal end_date As Date,
                                         ByVal isManufacturerWaranty As Boolean) As ClaimSearchDV

        Try
            Dim dal As New ClaimDAL

            Return New ClaimSearchDV(dal.LoadListByCoverageId(cert_item_coverage_id, begin_date, end_date).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getClaimListForImageIndexing(ByVal claimNumber As String, ByVal claimStatus As String, ByVal companyCode As String, ByVal customerName As String,
                                   ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                                   ByVal authorizedAmount As String, ByVal serviceCenterIds As ArrayList,
                                   Optional ByVal sortBy As String = ClaimDAL.SORT_BY_CLAIM_NUMBER) As ClaimSearchDV

        Try
            Dim dal As New ClaimDAL
            Dim externalUserServiceCenterIds As ArrayList '= ElitaPlusIdentity.Current.ActiveUser.DealerOrSvcList
            Dim compIds As ArrayList = New ArrayList
            Dim externalUserDealerId As Guid = Guid.Empty, dealerGroupCode As String = ""
            With ElitaPlusIdentity.Current.ActiveUser
                'compIds = .Companies 'DEF 3068
                compIds.Add(LookupListNew.GetIdFromCode("COMPANIES", companyCode))
                If .IsServiceCenter Then
                    externalUserServiceCenterIds = .DealerOrSvcList
                ElseIf .IsDealer Then
                    externalUserDealerId = .ScDealerId
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroupCode = ExternalUserDealergroup
                End If
            End With

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (customerName.Equals(String.Empty))) Then
                customerName = customerName.ToUpper
            End If
            'Convert the Service Center Name to UPPER Case
            If (Not (serviceCenterName Is Nothing)) AndAlso (Not (serviceCenterName.Equals(String.Empty))) Then
                serviceCenterName = serviceCenterName.ToUpper
            End If
            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso
                serviceCenterName.Equals(String.Empty) AndAlso authorizationNumber.Equals(String.Empty) AndAlso
                authorizedAmount.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If
            'If oAppUser.IsServiceCenter Then
            '    Me.State.selectedServiceCenterIds = oAppUser.DealerOrSvcList
            Return New ClaimSearchDV(dal.LoadListforImageIndexing(compIds, claimStatus,
                                    claimNumber, customerName, serviceCenterName,
                                    authorizationNumber, authorizedAmount, sortBy,
                                    externalUserServiceCenterIds, serviceCenterIds, externalUserDealerId, dealerGroupCode).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getListFromArray(ByVal claimNumber As String, ByVal customerName As String,
                                   ByVal serviceCenterName As String, ByVal svcRefNumber As String,
                                   ByVal authorizedAmount As String, ByVal hasPendingAuthId As Guid,
                                   ByVal serviceCenterIds As ArrayList, ByVal certNumber As String, ByVal dealerid As Guid,
                                   ByVal Status As String, Optional ByVal sortBy As String = ClaimDAL.SORT_BY_CLAIM_NUMBER, Optional ByVal trackingNumber As String = "",
                                   Optional ByVal authorizationNumber As String = "", Optional ByVal claimAuthStatusId As Guid = Nothing) As ClaimSearchDV

        Try
            Dim dal As New ClaimDAL
            Dim externalUserServiceCenterIds As ArrayList '= ElitaPlusIdentity.Current.ActiveUser.DealerOrSvcList
            Dim compIds As ArrayList, externalUserDealerId As Guid = Guid.Empty
            Dim dealerGroupCode As String = ""

            With ElitaPlusIdentity.Current.ActiveUser
                compIds = .Companies
                If .IsServiceCenter Then
                    externalUserServiceCenterIds = .DealerOrSvcList
                ElseIf .IsDealer Then
                    externalUserDealerId = .ScDealerId
                    dealerid = externalUserDealerId
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroupCode = ExternalUserDealergroup
                End If
            End With

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (customerName.Equals(String.Empty))) Then
                customerName = customerName.ToUpper
            End If
            'Convert the Service Center Name to UPPER Case
            If (Not (serviceCenterName Is Nothing)) AndAlso (Not (serviceCenterName.Equals(String.Empty))) Then
                serviceCenterName = serviceCenterName.ToUpper
            End If

            'Convert the Certificate Number to UPPER Case
            If (Not IsNothing(certNumber)) Then
                certNumber = certNumber.ToUpper
            End If

            'Convert the Claim Authorization Number to UPPER Case
            If (Not (authorizationNumber.Equals(String.Empty))) Then
                authorizationNumber = authorizationNumber.ToUpper
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso
                serviceCenterName.Equals(String.Empty) AndAlso svcRefNumber.Equals(String.Empty) AndAlso
                authorizedAmount.Equals(String.Empty) AndAlso certNumber.Equals(String.Empty) AndAlso trackingNumber.Equals(String.Empty) _
                AndAlso hasPendingAuthId.Equals(Guid.Empty) AndAlso dealerid.Equals(Guid.Empty) AndAlso String.IsNullOrEmpty(Status) _
                AndAlso String.IsNullOrEmpty(authorizationNumber) AndAlso claimAuthStatusId = Nothing) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If
            'If oAppUser.IsServiceCenter Then
            '    Me.State.selectedServiceCenterIds = oAppUser.DealerOrSvcList
            Return New ClaimSearchDV(dal.LoadList(compIds, claimNumber, customerName, serviceCenterName,
                                                  svcRefNumber, authorizedAmount, hasPendingAuthId, sortBy,
                                                  externalUserServiceCenterIds, serviceCenterIds, dealerid, certNumber, Status, Authentication.CurrentUser.NetworkId, trackingNumber, dealerGroupCode,
                                                  authorizationNumber, claimAuthStatusId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimsByIssue(ByVal issueTypeCode As String, ByVal issueTypeId As Guid,
                                                   ByVal issueId As Guid?, ByVal issueStatusXcd As String,
                                                   ByVal claimStatusCode As String, ByVal dealerId As Guid?,
                                                   ByVal issueAddedFromDate As Date?, ByVal issueAddedToDate As Date?
                                   ) As ClaimIssueSearchDV

        Dim dal As New ClaimDAL
        Dim userId, languageId As Guid

        With ElitaPlusIdentity.Current.ActiveUser
            userId = .Id
            languageId = .LanguageId
        End With

        Return New ClaimIssueSearchDV(dal.LoadClaimByIssue(userId, languageId,
                                                        issueTypeCode, issueTypeId,
                                                        issueId, issueStatusXcd,
                                                        claimStatusCode, dealerId,
                                                        issueAddedFromDate, issueAddedToDate, Authentication.CurrentUser.NetworkId).Tables(0))
    End Function

    Public Shared Function getAdjusterList(ByVal claimNumber As String, ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                               ByVal claim_status As String, ByVal claimTypeId As Guid, ByVal claimExtendedStatusId As Guid,
                                ByVal claimExtendedStatusOwnerId As Guid, ByVal scTATId As Guid, ByVal AutoApprove As String,
                                ByVal BeginDate As String, ByVal EndDate As String, ByVal claimAdjuster As String, ByVal claimAddedBy As String, Optional ByVal sortOrder As String = ClaimDAL.SORT_ORDER_DESC,
                                Optional ByVal sortBy As String = ClaimDAL.SORT_BY_SC_TAT) As ClaimAdjusterSearchDV

        Try
            Dim dal As New ClaimDAL
            Dim userNetworkId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Authorization Number to UPPER Case
            If (Not (authorizationNumber.Equals(String.Empty))) Then
                authorizationNumber = authorizationNumber.ToUpper
            End If
            'Convert the Service Center Name to UPPER Case
            If (Not (serviceCenterName Is Nothing)) AndAlso (Not (serviceCenterName.Equals(String.Empty))) Then
                serviceCenterName = serviceCenterName.ToUpper
            End If

            If (Not (claimAdjuster Is Nothing)) AndAlso (Not (claimAdjuster.Equals(String.Empty))) Then
                claimAdjuster = claimAdjuster.ToUpper
            End If

            If (Not (claimAddedBy Is Nothing)) AndAlso (Not (claimAddedBy.Equals(String.Empty))) Then
                claimAddedBy = claimAddedBy.ToUpper
            End If
            'Check if the user has entered any search criteria... if NOT, then display an error
            If (String.IsNullOrEmpty(claimNumber) AndAlso String.IsNullOrEmpty(authorizationNumber) AndAlso
                String.IsNullOrEmpty(serviceCenterName) AndAlso String.IsNullOrEmpty(claim_status) AndAlso
                String.IsNullOrEmpty(claimAdjuster) AndAlso String.IsNullOrEmpty(claimAddedBy) AndAlso
                claimTypeId.Equals(Guid.Empty) AndAlso claimExtendedStatusId.Equals(Guid.Empty) AndAlso
                claimExtendedStatusOwnerId.Equals(Guid.Empty) AndAlso scTATId.Equals(Guid.Empty)) AndAlso
               String.IsNullOrEmpty(AutoApprove) AndAlso String.IsNullOrEmpty(BeginDate) AndAlso String.IsNullOrEmpty(EndDate) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Dim scTATLowLimit, scTATHighLimit As LongType
            If Not scTATId.Equals(Guid.Empty) Then
                Dim scTATObj As New TurnAroundTimeRange(scTATId)
                scTATLowLimit = scTATObj.MinDays
                scTATHighLimit = scTATObj.MaxDays
                Return New ClaimAdjusterSearchDV(dal.LoadAdjusterList(userNetworkId, claimNumber, serviceCenterName, authorizationNumber, claim_status,
                                                                      claimTypeId, claimExtendedStatusId, AutoApprove, BeginDate, EndDate, claimExtendedStatusOwnerId, sortOrder, sortBy,
                                                                      claimAdjuster, claimAddedBy, scTATLowLimit, scTATHighLimit).Tables(0))
            Else
                Return New ClaimAdjusterSearchDV(dal.LoadAdjusterList(userNetworkId, claimNumber, serviceCenterName, authorizationNumber, claim_status,
                                                                      claimTypeId, claimExtendedStatusId, AutoApprove, BeginDate, EndDate, claimExtendedStatusOwnerId, sortOrder, sortBy,
                                                                      claimAdjuster, claimAddedBy, Nothing, Nothing).Tables(0))
            End If


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimsByCommentType(ByVal claimNumber As String, ByVal customerName As String,
                                              ByVal authorizationNumber As String,
                                              ByVal commentTypeId As Guid,
                                              ByVal claimStatus As String,
                                              ByVal languageId As Guid,
                                              Optional ByVal sortBy As String = ClaimDAL.SORT_BY_CLAIM_NUMBER) As ClaimSearchDV

        Try
            Dim dal As New ClaimDAL
            Dim externalUserServiceCenterIds As ArrayList
            Dim compIds As ArrayList, externalUserDealerId As Guid = Guid.Empty
            Dim dealerGroupCode As String = ""
            With ElitaPlusIdentity.Current.ActiveUser
                compIds = .Companies
                If .IsServiceCenter Then
                    externalUserServiceCenterIds = .DealerOrSvcList
                ElseIf .IsDealer Then
                    externalUserDealerId = .ScDealerId
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroupCode = ExternalUserDealergroup
                End If
            End With

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (customerName.Equals(String.Empty))) Then
                customerName = customerName.ToUpper
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso
                commentTypeId.Equals(Guid.Empty) AndAlso authorizationNumber.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Return New ClaimSearchDV(dal.GetClaimsByCommentType(compIds,
                                    claimNumber, customerName, commentTypeId,
                                    authorizationNumber, claimStatus, languageId, sortBy, externalUserDealerId, dealerGroupCode).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimID(ByVal companyIds As ArrayList, ByVal claimNumber As String) As Guid
        Dim claimID As Guid = Guid.Empty
        Dim dal As New ClaimDAL
        Dim ds As DataSet = dal.GetClaimID(companyIds, claimNumber)

        If (ds.Tables.Count > 0 And ds.Tables(0).Rows.Count > 0) Then
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            claimID = New Guid(CType(dr(dal.COL_NAME_CLAIM_ID), Byte()))
        End If

        Return claimID

    End Function

    Public Shared Function getMasterClaimListFromArray(ByVal claimNumber As String, ByVal customerName As String,
                                   ByVal masterClaimNumber As String, ByVal authorizationNumber As String) As ClaimSearchDV
        Try
            Dim dal As New ClaimDAL
            Dim compIds As ArrayList
            Dim externalUserDealerId As Guid = Guid.Empty, dealerGroupCode As String = ""
            With ElitaPlusIdentity.Current.ActiveUser
                compIds = .Companies
                If .IsDealer Then
                    externalUserDealerId = .ScDealerId
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroupCode = ExternalUserDealergroup
                End If
            End With

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Master Claim Number to UPPER Case
            If (Not (masterClaimNumber.Equals(String.Empty))) Then
                masterClaimNumber = masterClaimNumber.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (customerName.Equals(String.Empty))) Then
                customerName = customerName.ToUpper
            End If

            'Convert the Authorized Number to UPPER Case
            If (Not (authorizationNumber.Equals(String.Empty))) Then
                authorizationNumber = authorizationNumber.ToUpper
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso
                masterClaimNumber.Equals(String.Empty) AndAlso authorizationNumber.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Return New ClaimSearchDV(dal.LoadMasterClaimList(compIds, masterClaimNumber,
                                    claimNumber, customerName, authorizationNumber, externalUserDealerId, dealerGroupCode).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimsByMasterClaimNumber(ByVal masterClaimNumber As String) As DataSet
        Try
            Dim dal As New ClaimDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            'Convert the Master Claim Number to UPPER Case
            If (Not (masterClaimNumber.Equals(String.Empty))) Then
                masterClaimNumber = masterClaimNumber.ToUpper
            End If

            Return dal.GetClaimsByMasterClaimNumber(compIds, masterClaimNumber)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getMasterClaimDetailFromArray(ByVal masterClaimNumber As String, ByVal certId As Guid) As ClaimSearchDV
        Try
            Dim dal As New ClaimDAL
            Dim certIdStr As String
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Master Claim Number to UPPER Case
            If (Not (masterClaimNumber.Equals(String.Empty))) Then
                masterClaimNumber = masterClaimNumber.ToUpper
            End If

            If (Not (certId.Equals(Guid.Empty))) Then
                certIdStr = DBHelper.ValueToSQLString(certId)
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (masterClaimNumber.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Return New ClaimSearchDV(dal.LoadMasterClaimDetail(compIds, masterClaimNumber, certIdStr).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getMasterClaimDetailListFromArray(ByVal masterClaimNumber As String, ByVal certId As Guid) As ClaimSearchDV
        Try
            Dim dal As New ClaimDAL
            Dim certIdStr As String
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Master Claim Number to UPPER Case
            If (Not (masterClaimNumber.Equals(String.Empty))) Then
                masterClaimNumber = masterClaimNumber.ToUpper
            End If

            If (Not (certId.Equals(Guid.Empty))) Then
                certIdStr = DBHelper.ValueToSQLString(certId)
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (masterClaimNumber.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Return New ClaimSearchDV(dal.LoadMasterClaimDetailList(compIds, masterClaimNumber, certIdStr).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getList(ByVal claimNumber As String, ByVal customerName As String,
                                   ByVal selectedServiceCenterId As Guid, ByVal authorizationNumber As String,
                                   ByVal authorizedAmount As String, Optional ByVal sortBy As String = ClaimDAL.SORT_BY_CLAIM_NUMBER) As ClaimSearchDV

        Dim oSvcIds As New ArrayList

        oSvcIds.Add(selectedServiceCenterId)
        Return getListFromArray(claimNumber, customerName, Nothing, authorizationNumber, authorizedAmount, Guid.Empty, oSvcIds, String.Empty, Guid.Empty, String.Empty, sortBy)


    End Function

    Public Shared Function GetClaimFollowUpList(ByVal followUpDate As String,
                             ByVal serviceCenterName As String,
                             ByVal claimNumber As String,
                             ByVal claimAdjusterName As String,
                             ByVal customerName As String, ByVal claimStatus As String, ByVal dealerId As Guid,
                             ByVal claimTATId As Guid, ByVal claimExtendedStatusId As Guid, ByVal noActivityId As Guid, ByVal ownerId As Guid,
                             ByVal languageId As Guid, ByVal nonOperatedClaims As String, Optional ByVal sortBy As String = Nothing) As ClaimFollowUpSearchDV
        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim dal As New ClaimDAL
            Dim ds As DataSet
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (followUpDate.Equals(String.Empty) AndAlso
                serviceCenterName.Equals(String.Empty) AndAlso claimNumber.Equals(String.Empty) AndAlso
                claimAdjusterName.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso
                claimStatus.Equals(String.Empty) AndAlso dealerId.Equals(Guid.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Return New ClaimFollowUpSearchDV(dal.LoadListClaimFollowUp(followUpDate,
                                     serviceCenterName, claimNumber, claimAdjusterName, customerName, claimStatus,
                                     dealerId, claimTATId, claimExtendedStatusId, noActivityId, ownerId, compIds, languageId, nonOperatedClaims, sortBy).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetPendingClaimList(ByVal claimNumber As String, ByVal certNumber As String,
                                         ByVal dealerName As String) As PendingClaimSearchDV

        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New ClaimDAL

            'Convert the Claim Number to UPPER Case
            If (Not IsNothing(claimNumber)) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Certificate Number to UPPER Case
            If (Not IsNothing(certNumber)) Then
                certNumber = certNumber.ToUpper
            End If

            Return New PendingClaimSearchDV(dal.LoadPendingClaimList(compIds,
                                claimNumber, certNumber, dealerName).Tables(0))


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetPendingApprovalClaimList(ByVal claimNumber As String, ByVal certNumber As String,
                                        ByVal serviceCenterName As String) As PendingApprovalClaimSearchDV

        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New ClaimDAL


            'Convert the Claim Number to UPPER Case
            If (Not IsNothing(claimNumber)) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Certificate Number to UPPER Case
            If (Not IsNothing(certNumber)) Then
                certNumber = certNumber.ToUpper
            End If

            'Convert the Certificate Number to UPPER Case
            If (Not IsNothing(serviceCenterName)) Then
                serviceCenterName = serviceCenterName.ToUpper
            End If

            Return New PendingApprovalClaimSearchDV(dal.LoadPendingApprovalClaimList(compIds,
                                claimNumber, certNumber, serviceCenterName).Tables(0))


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetPendingReviewPaymentClaimList(ByVal claimNumber As String, ByVal srlNumber As String, ByVal certNumber As String, ByVal serviceCenterId As Guid,
                                                            ByVal countryid As Guid, ByVal manufacturerid As Guid, ByVal model As String, ByVal skuclaimed As String, ByVal skureplaced As String,
                                                           ByVal claimstatus As String, ByVal extclaimstatusid As Guid, ByVal coveragetypeId As Guid, ByVal servicelevelid As Guid,
                                                            ByVal Risktypeid As Guid, ByVal skureppart As String, ByVal ReplacementtypeId As Guid,
                                                            ByVal ClaimCreatedDate As SearchCriteriaStructType(Of Date)) As PendingReviewPaymentClaimSearchDV

        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New ClaimDAL
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            If (claimNumber.Equals(String.Empty) AndAlso srlNumber.Equals(String.Empty) AndAlso certNumber.Equals(String.Empty) AndAlso
                serviceCenterId.Equals(Guid.Empty) AndAlso countryid.Equals(Guid.Empty) AndAlso manufacturerid.Equals(Guid.Empty) AndAlso
                model.Equals(String.Empty) AndAlso skuclaimed.Equals(String.Empty) AndAlso skureplaced.Equals(String.Empty) AndAlso
                claimstatus.Equals(String.Empty) AndAlso extclaimstatusid.Equals(Guid.Empty) AndAlso coveragetypeId.Equals(Guid.Empty) AndAlso
                 servicelevelid.Equals(Guid.Empty) AndAlso Risktypeid.Equals(Guid.Empty) AndAlso skureppart.Equals(String.Empty) AndAlso ReplacementtypeId.Equals(Guid.Empty) _
                 AndAlso ClaimCreatedDate.IsEmpty) Then

                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If




            'Convert the Claim Number to UPPER Case
            If (Not IsNothing(claimNumber)) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Serial Number to UPPER Case
            If (Not IsNothing(srlNumber)) Then
                srlNumber = srlNumber.ToUpper
            End If

            'Convert the Certificate Number to UPPER Case
            If (Not IsNothing(certNumber)) Then
                certNumber = certNumber.ToUpper
            End If



            If (Not IsNothing(model)) Then
                model = model.ToUpper
            End If

            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New PendingReviewPaymentClaimSearchDV(dal.LoadPendingReviewPaymentClaimList(claimNumber, srlNumber, certNumber, serviceCenterId, countryid, manufacturerid, model, skuclaimed,
                                skureplaced, claimstatus, extclaimstatusid, coveragetypeId, servicelevelid, Risktypeid, skureppart, ReplacementtypeId, ClaimCreatedDate, language_id,
                                ElitaPlusIdentity.Current.ActiveUser.Id, LookupListNew.GetCodeFromId(LookupListNew.LK_SERVICE_LEVEL, servicelevelid),
                                LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__MULTIPLE),
                                LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_AUTHORIZATION_TYPE, Codes.CLAIM_AUTHORIZATION_TYPE__SINGLE),
                                LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__VOID),
                                LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__CLAIMED)).Tables(0))


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    Public Shared Function getNonReworkClaimsList(ByVal claimNumber As String, ByVal customerName As String,
                                      ByVal selectedServiceCenter As String, ByVal authorizationNumber As String,
                                      ByVal authorizedAmount As String, Optional ByVal sortBy As String = ClaimDAL.SORT_BY_CLAIM_NUMBER) As ClaimSearchDV

        Try
            Dim dal As New ClaimDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (customerName.Equals(String.Empty))) Then
                customerName = customerName.ToUpper
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso
                selectedServiceCenter.Equals(String.Empty) AndAlso authorizationNumber.Equals(String.Empty) AndAlso
                authorizedAmount.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Return New ClaimSearchDV(dal.LoadNonReworkClaimList(compIds,
                                                  claimNumber, customerName, selectedServiceCenter,
                                                  authorizationNumber, authorizedAmount, sortBy).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimNumberForOpenMobile(ByVal cert_number As String, ByVal serial_number As String) As DataSet
        Dim dal As New ClaimDAL
        Dim ds As DataSet
        Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Try
            Return dal.GetClaimNumberForOpenMobile(cert_number, serial_number, compIds)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetActiveClaimsForSvc(ByVal serviceCenterID As Guid, ByVal sortOrder As Integer, ByVal ExtendedClaimStatusListItemID As Guid, ByVal ExcludeRepairedClaims As String) As DataSet

        Try
            Dim dal As New ClaimDAL
            Dim companies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Return dal.GetActiveClaimsForSvc(companyGroupID, serviceCenterID, sortOrder, ExtendedClaimStatusListItemID, companies, ExcludeRepairedClaims, language_id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetActiveClaimsForSvcGeneric(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet

        Try
            Dim dal As New ClaimDAL
            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim companies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Return dal.GetActiveClaimsForSvcGeneric(oServiceCenterClaimsSearchData, companies, companyGroupID, language_id, IncludeTotalCount)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimsForServiceCenter(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet

        Try
            Dim dal As New ClaimDAL
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId

            Return dal.GetClaimsForServiceCenter(oServiceCenterClaimsSearchData, userName, IncludeTotalCount)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimsForServiceCenterAC(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet

        Try
            Dim dal As New ClaimDAL
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId

            Return dal.GetClaimsForServiceCenterAC(oServiceCenterClaimsSearchData, userName, IncludeTotalCount)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function



    Public Shared Function GetActiveClaimsByClaimNumberorCertificate(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet

        Try
            Dim dal As New ClaimDAL
            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim companies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Return dal.GetActiveClaimsByClaimNumberorCertificate(oServiceCenterClaimsSearchData, companies, companyGroupID, language_id, IncludeTotalCount)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetClaimDetailbyClaimNumAndDealer(ByVal claimnunber As String, ByVal dealerId As Guid) As DataSet
        Try
            Dim dal As New ClaimDAL

            Return dal.LoadClaimDetailbyClaimNumAndDealer(claimnunber, dealerId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetClaimDetailbyClaimNumAndCompany(ByVal claimnunber As String) As DataSet
        Try
            Dim dal As New ClaimDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return dal.LoadClaimDetailbyClaimNumAndCompany(claimnunber, compIds)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetClaimDetailForWS(ByVal claimId As String, ByVal claimNumber As String, ByVal company_id As Guid) As DataSet
        Try
            Dim dal As New ClaimDAL
            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return dal.LoadClaimDetailForWS(claimId, claimNumber, company_id, language_id)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function ClaimDetailForWS(ByVal claimNumber As String, ByVal company_id As Guid, ByVal getComments As Integer, ByVal getPartsDesc As Integer) As DataSet
        Try
            Dim dal As New ClaimDAL
            Dim language_id As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return dal.LoadGetClaimsForWS(claimNumber, company_id, language_id, getComments, getPartsDesc)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function ApproveOrRejectClaims(ByVal cmd As String, ByVal claimIds As String) As DBHelper.DBHelperParameter()
        Try
            Dim dal As New ClaimDAL
            Return dal.ApproveOrRejectClaims(cmd, claimIds, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ApproveOrRejectClaims(ByVal cmd As String, ByVal claimIds As String, ByVal comments As String, ByVal risktypes As String) As DBHelper.DBHelperParameter()
        Try
            Dim dal As New ClaimDAL
            Return dal.ApproveOrRejectClaims(cmd, claimIds, comments, risktypes, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetProblemDescription(ByVal claim_number As String) As String

        Dim retVal As String
        Dim dal As New ClaimDAL
        Dim dataset As DataSet = dal.GetProblemDescription(claim_number)
        Dim dataTable As DataTable = dataset.Tables(0)

        If dataTable.Rows(0).Item(0) Is DBNull.Value Then
            retVal = "N/A"
        Else
            retVal = dataTable.Rows(0).Item(0)
        End If

        Return retVal

    End Function
    Public Shared Function GetExtendedStatusComment(ByVal claim_number As String) As String

        Dim retVal As String
        Dim dal As New ClaimDAL
        Dim dataset As DataSet = dal.GetExtendedStatusComment(claim_number)
        Dim dataTable As DataTable = dataset.Tables(0)

        If dataTable.Rows(0).Item(0) Is DBNull.Value Then
            retVal = "N/A"
        Else
            retVal = dataTable.Rows(0).Item(0)
        End If

        Return retVal

    End Function

    Public Shared Function GetTechnicalReport(ByVal claim_number As String) As String

        Dim retVal As String
        Dim dal As New ClaimDAL
        Dim dataset As DataSet = dal.GetTechnicalReport(claim_number)
        Dim dataTable As DataTable = dataset.Tables(0)
        If dataTable.Rows(0).Item(0) Is DBNull.Value Then
            retVal = "N/A"
        Else
            retVal = dataTable.Rows(0).Item(0)
        End If

        Return retVal

    End Function

    'REQ-6230
    Public Shared Function GetCountryCodeOverwrite(ByVal companyId As Guid) As String
        Dim claimDalObj As ClaimDAL = New ClaimDAL()
        Dim countryCode As String = claimDalObj.GetCountryCodeOverwrite(companyId)

        Return countryCode
    End Function

    Public Shared Function GetPreviousInProgressClaimCount(ByVal claimId As Guid) As Integer
        Dim claimDalObj As ClaimDAL = New ClaimDAL()
        Return claimDalObj.LoadPreviousInProgressClaimCount(claimId)
    End Function


    Public Class ClaimIssueSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_CLAIM_NUMBER As String = "claim_number"

        'Public Const COL_CLAIM_NUM As String = "Claim_Number"
        Public Const COL_STATUS_CODE As String = "status_code"
        'Public Const COL_CUSTOMER_NAME As String = "custn"
        'Public Const COL_SERVICE_CENTER_ID As String = "service_center_id"
        'Public Const COL_SERVICE_CENTER_CODE As String = "service_center_code"
        Public Const COL_SERVICE_CENTER_NAME As String = "service_center_name"
        'Public Const COL_SERVICE_CENTER_REF_NUMBER As String = "svc_reference_number"
        Public Const COL_AUTHORIZATION_NUMBER As String = "authorization_number"
        'Public Const COL_TRACKING_NUMBER As String = "tracking_number"
        'Public Const COL_AUTHORIZED_AMOUNT As String = "auth"
        'Public Const COL_DEALER_CODE As String = "dlrcd"
        Public Const COL_CERTIFICATE_NUMBER As String = "cert_number"
        Public Const COL_ISSUE_ADDED_DATE As String = "issue_added_Date"
        'Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_CLAIM_AUTH_TYPE_ID As String = "claim_auth_type_id"
        Public Const COL_DEALER_NAME As String = "dealer_name"

        Public Const COL_CLAIM_ISSUE_STATUS As String = "issue_status"
        Public Const COL_CLAIM_ISSUE_TYPE As String = "issue_type"
        Public Const COL_CLAIM_ISSUE_DESCRIPTION As String = "issue_description"

        'Public Const COL_AUTHORIZATION_STATUS As String = "auth_status"
        'Public Const COL_DEALER_ID As String = "dealer_id"

        'Public Const COL_CLAIM_STATUS As String = "clstatoption"
        'Public Const COL_COMMENT_TYPE As String = "cmtypsrch"
        'Public Const COL_COMMENTS As String = "clcmts"

        'Public Const COL_NAME_MASTER_CLAIM_NUMBER As String = "master_claim_number"
        'Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
        'Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
        'Public Const COL_NAME_TOTAL_PAID As String = "total_paid"
        'Public Const COL_NAME_AUTHORIZED_AMOUNT As String = "authorized_amount"
        'Public Const COL_NAME_STATUS As String = "status"
        'Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
        'Public Const COL_NAME_PAYEE As String = "payee"
        'Public Const COL_NAME_DATE_CREATED As String = "date_created"
        'Public Const COL_NAME_AMOUNT_PAID As String = "amount_paid"
        'Public Const COL_NAME_CLAIM_ID As String = "claim_id"
        'Public Const COL_NAME_CERT_ID As String = "cert_id"
        'Public Const COL_NAME_CLAIM_INVOICE_ID As String = "claim_invoice_id"

        'Public Const COL_NAME_LOSS_DATE As String = "loss_date"


#End Region
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ClaimId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property ClaimNumber(ByVal row As DataRow) As String
            Get
                Return row(COL_CLAIM_NUMBER).ToString
            End Get
        End Property
    End Class

    Public Class ClaimSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_CLAIM_NUMBER As String = "clnum"
        Public Const COL_CLAIM_NUM As String = "Claim_Number"
        Public Const COL_STATUS_CODE As String = "status_code"
        Public Const COL_CUSTOMER_NAME As String = "custn"
        Public Const COL_SERVICE_CENTER_ID As String = "service_center_id"
        Public Const COL_SERVICE_CENTER_CODE As String = "service_center_code"
        Public Const COL_SERVICE_CENTER_NAME As String = "svcna"
        Public Const COL_AUTHORIZATION_NUMBER As String = "authn"
        Public Const COL_TRACKING_NUMBER As String = "tracking_number"
        Public Const COL_AUTHORIZED_AMOUNT As String = "auth"
        Public Const COL_DEALER_CODE As String = "dlrcd"
        Public Const COL_CERTIFICATE_NUMBER As String = "certn"
        Public Const COL_DATE_ADDED As String = "created_date"
        Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_CLAIM_AUTH_TYPE_ID As String = "claim_auth_type_id"
        Public Const COL_DEALER_NAME As String = "Dealer_name"

        Public Const COL_CLAIM_ISSUE_STATUS As String = "issue_status"
        Public Const COL_CLAIM_ISSUE_TYPE As String = "issue_type"

        'Public Const COL_AUTHORIZATION_NUMBER As String = "authorization_number"
        Public Const COL_AUTHORIZATION_STATUS As String = "auth_status"
        Public Const COL_DEALER_ID As String = "dealer_id"

        Public Const COL_CLAIM_STATUS As String = "clstatoption"
        Public Const COL_COMMENT_TYPE As String = "cmtypsrch"
        Public Const COL_COMMENTS As String = "clcmts"

        Public Const COL_NAME_MASTER_CLAIM_NUMBER As String = "master_claim_number"
        Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
        Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
        Public Const COL_NAME_TOTAL_PAID As String = "total_paid"
        Public Const COL_NAME_AUTHORIZED_AMOUNT As String = "authorized_amount"
        Public Const COL_NAME_STATUS As String = "status"
        Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
        Public Const COL_NAME_PAYEE As String = "payee"
        Public Const COL_NAME_DATE_CREATED As String = "date_created"
        Public Const COL_NAME_AMOUNT_PAID As String = "amount_paid"
        Public Const COL_NAME_CLAIM_ID As String = "claim_id"
        Public Const COL_NAME_CERT_ID As String = "cert_id"
        Public Const COL_NAME_CLAIM_INVOICE_ID As String = "claim_invoice_id"

        Public Const COL_NAME_LOSS_DATE As String = "loss_date"
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ClaimId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property ClaimNumber(ByVal row As DataRow) As String
            Get
                Return row(COL_CLAIM_NUMBER).ToString
            End Get
        End Property

    End Class

    Public Class ClaimFollowUpSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_ID As String = "Claim_Id"
        Public Const COL_FOLLOWUP_DATE As String = "cfdat"
        Public Const COL_DENIED_REASON As String = "cfdnr"
        Public Const COL_COMMENT_TYPE As String = "cfcty"
        Public Const COL_EXTENDED_STATUS As String = "cfexst"
        Public Const COL_SERVICE_CENTER_NAME As String = "cfsvn"
        Public Const COL_CLAIM_NUMBER As String = "cfcln"
        Public Const COL_COMMENTS As String = "cfcom"
        Public Const COL_CUST_NAME As String = "cfcnm"
        Public Const COL_DEALER_CODE As String = "cfdcd"
        Public Const COL_CLAIMS_AJDUSTER As String = "cfadj"
        Public Const COL_CLAIM_TAT As String = "cfclmTAT"
        Public Const COL_SVC_TAT As String = "cfscTAT"
        Public Const COL_STATUS_CODE As String = "cfstcd"
        Public Const COL_OWNER As String = "cfowr"
        Public Const COL_NO_ACTIVITY As String = "cfnoatv"
        Public Const COL_NUM_OF_REMINDERS As String = "Num_of_Reminders"
        Public Const COL_LAST_REMINDER_SEND_DATE As String = "Last_Reminder_Send_Date"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
    Public Class ClaimRedoDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_ID As String = ClaimDAL.COL_NAME_CLAIM_ID
        Public Const COL_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_SERVICE_CENTER_CODE As String = ClaimDAL.COL_NAME_SERVICE_CENTER_CODE
        Public Const COL_PICK_UP_DATE As String = ClaimDAL.COL_NAME_PICKUP_DATE
        Public Const COL_MASTER_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class PendingClaimSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_ID As String = ClaimDAL.COL_NAME_CLAIM_ID
        Public Const COL_NAME_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_NAME_DEALER_CODE As String = ClaimDAL.COL_NAME_DEALER_CODE
        Public Const COL_NAME_DATE_ADDED As String = ClaimDAL.COL_NAME_CREATED_DATE
        Public Const COL_NAME_CERTIFICATE_NUMBER As String = ClaimDAL.COL_NAME_CERTIFICATE_NUMBER
        Public Const COL_NAME_PRODUCT_CODE As String = ClaimDAL.COL_NAME_PRODUCT_CODE
        Public Const COL_NAME_AUTHORIZED_AMOUNT As String = ClaimDAL.COL_NAME_AUTHORIZED_AMOUNT


#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class PendingApprovalClaimSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_ID As String = ClaimDAL.COL_NAME_CLAIM_ID
        Public Const COL_NAME_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_NAME_CUSTOMER_NAME As String = ClaimDAL.COL_NAME_CUSTOMER_NAME
        Public Const COL_NAME_SERVICE_CENTER_NAME As String = ClaimDAL.COL_NAME_SERVICE_CENTER_NAME
        Public Const COL_NAME_STATUS_DATE As String = ClaimDAL.COL_NAME_STATUS_DATE

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class PendingReviewPaymentClaimSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_ID As String = ClaimDAL.COL_NAME_CLAIM_ID
        Public Const COL_NAME_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_NAME_SERIAL_NUMBER As String = ClaimDAL.COL_NAME_SERIAL_NUMBER
        Public Const COL_NAME_SERVICE_CENTER_NAME As String = "service_center_description"
        Public Const COL_NAME_CLAIM_CREATED_DATE As String = "claim_created_date"
        Public Const COL_NAME_SERVICE_CENTER_COUNTRY As String = "country_description"
        Public Const COL_NAME_CLAIM_STATUS As String = "claim_status_code"
        Public Const COL_NAME_CLAIM_EXT_STATUS As String = "extended_status_description"
        Public Const COL_NAME_CLAIM_EXT_STATUS_ID As String = "extended_status_id"
        Public Const COL_NAME_MAKE As String = "manufacturer_description"
        Public Const COL_NAME_MODEL As String = ClaimDAL.COL_NAME_MODEL
        Public Const COL_NAME_SKU_CLAIMED As String = "claimed_sku"
        Public Const COL_NAME_SKU_REPLACED As String = "replaced_sku"
        Public Const COL_NAME_COVERAGE_TYPE As String = ClaimDAL.COL_NAME_COVERAGE_TYPE
        Public Const COL_NAME_REPAIR_DATE As String = ClaimDAL.COL_NAME_REPAIR_DATE

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class ClaimAdjusterSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_CLAIM_TAT As String = "claim_tat"
        Public Const COL_SVC_TAT As String = "sc_tat"
        Public Const COL_CLAIM_STATUS As String = "claim_status"
        Public Const COL_CLAIM_ADJUSTER As String = "claims_adjuster_username"
        Public Const COL_ADDED_BY As String = "claim_created_by_username"
        Public Const COL_CLAIM_EXTENDED_STATUS As String = "claim_extended_status"
        Public Const COL_AUTO_APPROVED As String = "Auto_Approved"
        Public Const COL_CLAIM_EXTENDED_STATUS_OWNER As String = "extended_status_owner"
        Public Const COL_CLAIM_NUMBER As String = "claim_number"
        Public Const COL_SERVICE_CENTER_NAME As String = "service_center_name"
        Public Const COL_CLAIM_TYPE_DESCRIPTION As String = "claim_type_description"
        Public Const COL_AUTHORIZATION_NUMBER As String = "authorization_number"
        Public Const COL_PRODUCT_DESCRIPTION As String = "product_description"

        Public Const COL_SALES_PRICE As String = "Sales_Price"
        Public Const COL_COVERAGE_TYPE As String = "Coverage_Type"
        Public Const COL_EXPIRATION_DATE As String = "End_Date"
        Public Const COL_PAYMENT_AMOUNT As String = "Payment_Amount"

        'Public Const COL_ITEM_MODEL As String = "item_model"
        'Public Const COL_MANUFACTURER As String = "manufacturer"
        Public Const COL_AUTHORIZED_AMOUNT As String = "authorized_amount"
        Public Const COL_PROPOSED_AMOUNT As String = "proposed_amount"
        Public Const COL_LABOR_AMOUNT As String = "labor_amount"
        Public Const COL_PART_AMOUNT As String = "part_amount"
        Public Const COL_SERVICE_CHARGE As String = "service_charge"
        Public Const COL_TRIP_AMOUNT As String = "trip_amount"
        Public Const COL_OTHER_AMOUNT As String = "other_amount"
        Public Const COL_SHIPPING_AMOUNT As String = "shipping_amount"
        Public Const COL_RISK_TYPE_ID As String = "risk_type_id"

        Public Const COL_INBOUND_TRACKING_NUMBER As String = "inbound_tracking_number"
        Public Const COL_OUTBOUND_TRACKING_NUMBER As String = "outbound_tracking_number"
        Public Const COL_REPLACEMENT_DEVICE As String = "replacement_device"
        Public Const COL_REPLACEMENT_DEVICE_COMMENTS As String = "replacement_device_comments"

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        'Public Shared ReadOnly Property ClaimId(ByVal row) As Guid
        '    Get
        '        Return New Guid(CType(row(COL_CLAIM_ID), Byte()))
        '    End Get
        'End Property

        'Public Shared ReadOnly Property ClaimNumber(ByVal row As DataRow) As String
        '    Get
        '        Return row(COL_CLAIM_NUMBER).ToString
        '    End Get
        'End Property

    End Class
    '10/12/2006 - ALR - Added for the batch process.
    Public Shared Function getClaimsForBatchProcess(ByVal serviceCenterId As Guid, ByVal batchNumber As String, ByVal InvoiceTransId As Guid, ByVal languageId As Guid) As ClaimsForBatchProcessDV

        Try
            Dim dal As New ClaimDAL
            Return New ClaimsForBatchProcessDV(dal.GetClaimsForBatch(serviceCenterId, batchNumber, InvoiceTransId, ElitaPlusIdentity.Current.ActiveUser.Id, languageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    '10/12/2006 - ALR - Added for the batch process.
    Public Class ClaimsForBatchProcessDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_ID As String = ClaimDAL.COL_NAME_CLAIM_ID
        Public Const COL_NAME_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_NAME_CONTACT_NAME As String = ClaimDAL.COL_NAME_CONTACT_NAME
        Public Const COL_NAME_MODIFIED_DATE As String = ClaimDAL.COL_NAME_MODIFIED_DATE
        Public Const COL_NAME_MODIFIED_BY As String = ClaimDAL.COL_NAME_MODIFIED_BY
        Public Const COL_NAME_RESERVE_AMOUNT As String = ClaimDAL.COL_CAL_RESERVE_AMOUNT
        Public Const COL_NAME_PAYMENT_AMOUNT As String = ClaimDAL.COL_CAL_PAYMENT_AMOUNT
        Public Const COL_NAME_PICKUP_DATE As String = ClaimDAL.COL_NAME_PICKUP_DATE
        Public Const COL_NAME_REPAIR_DATE As String = ClaimDAL.COL_NAME_REPAIR_DATE
        Public Const COL_NAME_SPARE_PARTS As String = ClaimDAL.COL_NAME_SPARE_PARTS
        Public Const COL_NAME_AUTHORIZATION_NUMBER As String = ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER
        Public Const COL_NAME_SELECTED As String = ClaimDAL.COL_NAME_SELECTED
        Public Const COL_NAME_SERVICE_CENTER As String = ClaimDAL.COL_NAME_SERVICE_CENTER_NAME
        Public Const COL_NAME_INVOICE_TRANS_DETAIL_ID As String = "invoice_trans_detail_id"
        Public Const COL_NAME_INVOICE_TRANS_ID As String = "invoice_trans_id"
        Public Const COL_NAME_BATCH_NUMBER As String = ClaimDAL.COL_NAME_BATCH_NUMBER
        Public Const COL_NAME_EXCLUDE_DEDUCTIBLE As String = "exclude_deductible"
        Public Const COL_NAME_CLAIM_EXTENDED_STATUS As String = "claim_extended_status"
        Public Const COL_NAME_TOTAL_BONUS As String = "TotalBonus"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidRepairDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Claim = CType(objectToValidate, Claim)


            If obj.RepairDate Is Nothing Then Return True

            'if backend claim then both dates will be not null...then skip validation
            If Not obj.RepairDate Is Nothing And Not obj.PickUpDate Is Nothing Then
                Return True
            End If

            Dim repairDate As Date = obj.GetShortDate(obj.RepairDate.Value)


            Dim createdDate As Date = Today

            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 757668: For a claim that is added from a claim interface:
            ' Repair Date is EQ to or LT the current date and EQ to or GT the Date of Loss
            ' A claim is originated from an interface when the Source field in the Claim record is not null
            If Not obj.Source Is Nothing Then
                If ((repairDate >= obj.GetShortDate(obj.LossDate.Value)) AndAlso
                (repairDate <= obj.GetShortDate(Today))) Then
                    Return True
                End If
            Else
                If ((repairDate >= obj.GetShortDate(createdDate)) AndAlso
                (repairDate <= obj.GetShortDate(Today))) Then
                    Return True
                End If
            End If
            If Not obj.LoanerReturnedDate Is Nothing AndAlso repairDate <= obj.GetShortDate(obj.LoanerReturnedDate.Value) Then
                Return True
            End If

            Return False

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidLoanerReturnedDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_LOANER_RETURNED_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Claim = CType(objectToValidate, Claim)


            If obj.LoanerReturnedDate Is Nothing Then Return True

            Dim loanerReturnedDate As Date = obj.GetShortDate(obj.LoanerReturnedDate.Value)

            Dim createdDate As Date = Today

            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            If loanerReturnedDate < createdDate Then
                Me.Message = Common.ErrorCodes.INVALID_LOANER_RETURN_DATE_ERR1 '"Loaner Return Date Must Be Greater Than Or Equal To Date Added."
                Return False
            End If

            If loanerReturnedDate > obj.GetShortDate(Today) Then
                Me.Message = Common.ErrorCodes.INVALID_LOANER_RETURN_DATE_ERR2 '"Loaner Return Date Must Be Less Than Or Equal To Today."
                Return False
            End If

            If Not obj.RepairDate Is Nothing AndAlso loanerReturnedDate < obj.GetShortDate(obj.RepairDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_LOANER_RETURN_DATE_ERR3 '"Loaner Return Date Must Be Greater Than Or Equal To Repair Date."
                Return False
            End If

            'Old code
            'If ((loanerReturnedDate >= createdDate) AndAlso _
            '    (loanerReturnedDate <= obj.GetShortDate(Today)) And (loanerReturnedDate >= repairDate)) Then
            '    Return True
            'End If

            'Return False
            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidAuthorizedAmount
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.AUTHORIZED_AMOUNT_HAS_EXCEEDED_YOUR_AUTHORIZATION_LIMIT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Claim = CType(objectToValidate, Claim)

            If obj.AuthorizedAmount Is Nothing Then
                MyBase.Message = Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR
                Return False
            End If

            If ((obj.AuthorizedAmount.Value >= 0) OrElse
                (obj.IsNew) OrElse
                (obj.AuthorizedAmount.Value <= obj.AuthorizationLimit.Value) OrElse
                (obj.AuthorizedAmount.Value = obj.originalAuthorizedAmount.Value)) Then
                'Either a New Claim
                'Or AuthorizedAmount has NOT been changed by the user
                Return True
            End If

            Return False

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidVisitDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_VISIT_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Claim = CType(objectToValidate, Claim)
            If obj.VisitDate Is Nothing Then Return True
            Dim visitDate As Date = obj.GetShortDate(obj.VisitDate.Value)
            Dim createdDate As Date = Today
            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 756735: For a claim that is not added from a claim interface or a replacement:
            ' Visit Date:
            ' Must Not be GT today. 
            ' Must be GT or EQ to Date Of Loss. 
            ' Must be LT or EQ to Repair Date.
            ' Must be LT or EQ to Pick-Up Date if not NULL. 

            If visitDate > Today Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR1 ' "Visit Date Must Be Less Than Or Equal To Today."
                Return False
            End If
            If Not obj.LossDate Is Nothing AndAlso visitDate < obj.GetShortDate(obj.LossDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR4 ' "Visit Date Must Be Greater Than Or Equal To Date Of Loss."
                Return False
            End If
            If Not obj.RepairDate Is Nothing AndAlso visitDate > obj.GetShortDate(obj.RepairDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR2 ' "Visit Date Must Be Less Than Or Equal To Repair Date."
                Return False
            End If
            If Not obj.PickUpDate Is Nothing AndAlso visitDate > obj.GetShortDate(obj.PickUpDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_VISIT_DATE_ERR3 ' "Visit Date Must Be Less Than Or Equal To Pick-Up Date."
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidPickUpDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Claim = CType(objectToValidate, Claim)
            If obj.PickUpDate Is Nothing Then Return True
            Dim pickUpDate As Date = obj.GetShortDate(obj.PickUpDate.Value)
            Dim createdDate As Date = Today
            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 756735: For a claim that is not added from a claim interface or a replacement:
            ' PickUp Date:
            ' Must be LT or EQ today. 
            ' Must be GT or EQ to Repair Date. 
            ' Must be GT or EQ to Visit Date if not NULL. 
            ' Must be GT or EQ to Loaner Returned Date if not NULL. 

            If obj.LoanerTaken Then Return True

            If pickUpDate > Today Then
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR1 '"Pick-Up Date Must Be Less Than Or Equal To Today."
                Return False
                'End If
            ElseIf Not obj.RepairDate Is Nothing Then
                If pickUpDate < obj.GetShortDate(obj.RepairDate.Value) Then
                    Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR2  '"Pick-Up Date Must Be Greater Than Or Equal To Repair Date."
                    Return False
                End If
                'ElseIf pickUpDate > Today AndAlso pickUpDate < obj.GetShortDate(obj.RepairDate.Value) Then
                '    Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR6 '"Pick-Up Date Must Be Between Repair Date and Today."
                '    Return False
            Else
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR5  '"Pick-Up Date Requires The Entry Of A Repair Date."
                Return False
            End If

            If Not obj.LoanerReturnedDate Is Nothing AndAlso pickUpDate < obj.GetShortDate(obj.LoanerReturnedDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR4 '"Pick-Up Date Must Be Greater Than Or Equal To Loaner Returned Date."
                Return False
            End If
            If Not obj.VisitDate Is Nothing AndAlso pickUpDate < obj.GetShortDate(obj.VisitDate.Value) Then
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR3 '"Pick-Up Date Must Be Greater Than Or Equal To Visit Date."
                Return False
            End If

            Return True

        End Function
    End Class

    'Me.State.ClaimInvoiceBO = New ClaimInvoice
    '                    Me.State.DisbursementBO = Me.State.ClaimInvoiceBO.AddNewDisbursement()
    '                    Me.State.ClaimInvoiceBO.PrepopulateClaimInvoice(Me.State.ClaimBO)


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidAuthAmountEdit
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            'MyBase.New(fieldDisplayName, Common.ErrorCodes.AUTHORIZED_AMOUNT_HAS_EXCEEDED_YOUR_AUTHORIZATION_LIMIT_ERR)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.NEW_AUTHORIZED_AMOUNT_LESS_DEDUCTIBLE_WOULD_BE_LOWER_THAN_SUM_OF_PAYMENTS_ALREADY_MADE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Claim = CType(objectToValidate, Claim)
            Dim ClaimInvoiceBO As New ClaimInvoice
            ClaimInvoiceBO.PrepopulateClaimInvoice(obj)

            If obj.IsNew Then
                Return True
            End If

            'If obj.AuthorizedAmount.Value < ClaimInvoiceBO.AlreadyPaid.Value Then  'Old logic
            'Ticket # 855,627: Assurant Pays amount minus all that was already paid for the claim is at least (equal to or greater than) 0.
            If obj.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                If (obj.AssurantPays.Value - ClaimInvoiceBO.AlreadyPaid.Value) < 0 Then   'New logic
                    Return False
                End If
            End If
            Return True

        End Function

    End Class


#End Region

#Region "Iterator"
    Public Shared Function GetIterator(ByVal familyMember As BusinessObjectBase) As BusinessObjectIteratorBase
        Return New BusinessObjectIteratorBase(familyMember.Dataset.Tables(ClaimDAL.TABLE_NAME), GetType(Claim))
    End Function
#End Region

#Region "Children"

    Public Function AddPartsInfo(ByVal partInfoID As Guid) As PartsInfo
        If partInfoID.Equals(Guid.Empty) Then
            Dim objPartsInfo As New PartsInfo(Me.Dataset)
            Return objPartsInfo
        Else
            Dim objPartsInfo As New PartsInfo(partInfoID, Me.Dataset)
            Return objPartsInfo
        End If
    End Function

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal certItemCoverageId As Guid, ByVal allowdifferentcoverage As Boolean, ByVal masterClaimProcCode As String, Optional ByVal selDateOfLoss As Date = Nothing) As MaterClaimDV
        Try
            Dim dal As New ClaimDAL, MaterClaimFilterDV As MaterClaimDV
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            If masterClaimProcCode = Codes.MasterClmProc_ANYMC Then
                Return New MaterClaimDV(dal.LoadMasterClaimList(certItemCoverageId, allowdifferentcoverage, masterClaimProcCode).Tables(0))
            ElseIf masterClaimProcCode = Codes.MasterClmProc_BYDOL Then
                MaterClaimFilterDV = New MaterClaimDV(dal.LoadMasterClaimList(certItemCoverageId, allowdifferentcoverage, masterClaimProcCode, selDateOfLoss).Tables(0))
                If MaterClaimFilterDV.Count = 0 Then
                    MaterClaimFilterDV = New MaterClaimDV(dal.LoadMasterClaimList(certItemCoverageId, allowdifferentcoverage, masterClaimProcCode).Tables(0))
                End If
                Return MaterClaimFilterDV
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getRepairedMasterClaimsList(ByVal certItemCoverageId As Guid, ByVal selDateOfLoss As Date) As MaterClaimDV
        Try
            Dim dal As New ClaimDAL, MaterClaimFilterDV As MaterClaimDV
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            MaterClaimFilterDV = New MaterClaimDV(dal.GetRepairedMasterClaimList(certItemCoverageId, selDateOfLoss).Tables(0))
            Return MaterClaimFilterDV
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getRedoList(ByVal certItemCoverageId As Guid, ByVal createdDate As DateType, ByVal redoClaimID As Guid) As ClaimRedoDV
        Try
            Dim dal As New ClaimDAL

            Return New ClaimRedoDV(dal.LoadRedoClaimList(certItemCoverageId, createdDate.Value, redoClaimID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function ProductRemainLiabilityAmount(ByVal CertId As Guid, ByVal lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.ProductRemainLiabilityAmount(CertId, lossDate)
    End Function
    Public Shared Function CoverageRemainLiabilityAmount(ByVal CertItemCoverageId As Guid, ByVal lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.CoverageRemainLiabilityAmount(CertItemCoverageId, lossDate)
    End Function

#Region "claimSearchDV"
    Public Class MaterClaimDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_ID As String = "claim_id"
        Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
        Public Const COL_NAME_MASTER_CLAIM_NUMBER As String = "master_claim_number"
        Public Const COL_NAME_LOSS_DATE As String = "loss_date"
        Public Const COL_SERVICE_CENTER As String = "description"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#End Region

#Region "Claim Logistics"

    Public Shared Function WS_GetClaimStatusInfo(ByVal CustomerIdentifier As String, ByVal IdentifierType As String, ByVal DealerId As Guid, ByVal BillingZipCode As String, ByVal LanguageISOCode As String, ByVal Certificate_Number As String, ByRef ValidateErrorCode As Integer) As DataSet
        Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
        Dim dal As New ClaimDAL

        Return dal.WS_GetClaimStatusInfo(CustomerIdentifier, IdentifierType, DealerId, userId, BillingZipCode, LanguageISOCode, Certificate_Number, ValidateErrorCode)
    End Function

    Public Shared Function WebSubmitClaimPreValidate(ByVal oWebSubmitClaimPreValidateInputData As ClaimDAL.WebSubmitClaimPreValidateInputData, ByRef oWebSubmitClaimPreValidateOutputData As ClaimDAL.WebSubmitClaimPreValidateOutputData) As DataSet
        Dim dal As New ClaimDAL

        Return dal.WebSubmitClaimPreValidate(oWebSubmitClaimPreValidateInputData, oWebSubmitClaimPreValidateOutputData)
    End Function



#End Region

#Region "Claim Issues"

    Public Overrides Function CanIssuesReopen() As Boolean
        Dim flag As Boolean = False

        If (Me.RepairDate Is Nothing And Me.PickUpDate Is Nothing) Then
            Dim claimNumber As String = If(Me.ClaimNumber = String.Empty, "0", Me.ClaimNumber)
            If (ClaimInvoice.getPaymentsList(Me.CompanyId, claimNumber).Count = 0) Then
                If (Me.StatusCode = Codes.CLAIM_STATUS__ACTIVE Or Me.StatusCode = Codes.CLAIM_STATUS__PENDING Or Me.StatusCode = Codes.CLAIM_STATUS__DENIED) Then
                    flag = True
                End If
            End If
        End If
        Return flag

    End Function

    Public Shared Function CheckClaimPaymentInProgress(ByVal claimId As Guid, ByVal companyGroupId As Guid) As Boolean
        Dim dal As New ClaimDAL
        Dim ds As DataSet
        ds = dal.CheckClaimPaymentInProgress(claimId, companyGroupId)
        If CType((ds.Tables(0).Rows(0).Item(0)), Integer) = 1 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region

#Region "Claim Locking"

    Public Function Lock() As Boolean
        Try
            Lock = False
            Dim _dal As New ClaimDAL
            If Not Me.IsNew Then
                _dal.Lock_Claim(Me.Id, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            End If
            Me.LockedBy = ElitaPlusIdentity.Current.ActiveUser.Id
            Me.lockedOn = Date.Now
            Me.IsLocked = Codes.YESNO_Y
            Lock = True
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Function UnLock() As Boolean
        Try
            UnLock = False
            Dim _dal As New ClaimDAL
            If Not Me.IsNew Then
                _dal.UnLock_Claim(Me.Id, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            End If
            Me.LockedBy = Guid.Empty
            Me.lockedOn = Nothing
            Me.IsLocked = Codes.YESNO_N
            UnLock = True
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

#End Region

#Region "Claim Extensions"
    Public Shared Function GetFraudulentClaimExtensions(ByVal claimId As Guid) As ClaimExtensionsDV

        Dim dal As New ClaimDAL

        Return New ClaimExtensionsDV(dal.GetFraudulentClaimExtensions(claimId).Tables(0))

    End Function

    Public Class ClaimExtensionsDV
        Inherits DataView
        Public Const COL_FIELD_NAME As String = "field_name"
        Public Const COL_FIELD_VALUE As String = "field_value"

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region

#Region "External References"
    Public Shared Function GetLegacyBridgeServiceClient() As LegacyBridgeServiceClient
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_LEGACY_BRIDGE_SERVICE), False)
        Dim client = New LegacyBridgeServiceClient("CustomBinding_ILegacyBridgeService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function

    Public Function GetFulfillmentDetails(claimNumber As String, companyCode As String) As FulfillmentDetails Implements IFullfillable.GetFulfillmentDetails

        Dim response As New FulfillmentDetails
        Dim fees As New Fee()
        'Dim logisticStages As New SelectedLogisticStage()
        Dim logisticStage As New List(Of SelectedLogisticStage)
        Dim charges As New Charge()

        response.Charges = New Charge() {New Charge()}
        response.Fees = New Fee() {New Fee()}
        response.LogisticStages = New SelectedLogisticStage() {New SelectedLogisticStage() With {
                                                                                                .Address = New FulfillmentAddress With {.Address1 = ContactInfo.Address.Address1,
                                                                                                     .Address2 = Me.ContactInfo.Address.Address2,
                                                                                                     .Address3 = Me.ContactInfo.Address.Address3,
                                                                                                     .City = Me.ContactInfo.Address.City,
                                                                                                     .Country = Me.ContactInfo.Address.countryBO.Code,
                                                                                                     .PostalCode = Me.ContactInfo.Address.PostalCode,
                                                                                                     .State = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_REGIONS, False), Me.ContactInfo.Address.RegionId)
                                                                                                                },
                                                                        .OptionCode = Me.MethodOfRepairCode,
                                                                        .OptionDescription = Me.MethodOfRepairDescription,
                                                                        .Code = "FW",
                                                                        .Description = "Forward Logistics",
                                                                        .HandlingStore = New HandlingStore(),
                                                                        .ServiceCenterCode = Me.ServiceCenterObject.Code,
                                                                        .ServiceCenterDescription = Me.ServiceCenterObject.Description,
                                                                        .Shipping = New ClaimFulfillmentWebAppGatewayService.ShippingInfo()
            }}
        Return response
    End Function

#End Region

End Class

Public Class ListCodes
    Public Const CoverageType As String = "CTYP"
    Public Const CertificateStatus As String = "CSTAT"
    Public Const DealerType As String = "DLTYP"
    Public Const DealerTypeCode As String = "DEALER_TYPE"
    Public Const YesNo As String = "YESNO"
    Public Const PostPrePaid As String = "POSTPRE"
    Public Const SubscriberStatus As String = "SUBSTAT"
    Public Const MembershipType As String = "MEMTYPE"
    Public Const Nationality As String = "NATIONALITY"
    Public Const PlaceOfBirth As String = "PLACEOFBIRTH"
    Public Const Gender As String = "GENDER"
    Public Const MaritalStatus As String = "MARITAL_STATUS"
    Public Const ClaimLimitBasedOn As String = "REPLOG"
    Public Const EquipmentType As String = "TEQP" ''''Eg. Used, New , NewOrUsed
    Public Const DeductibleBasedOn As String = "COMDEDUCTBASED"
    Public Const Salutation As String = "SLTN"
    Public Const MethodOfRepair As String = "METHR"
    Public Const PaymentInstrumentType As String = "PMTINSTR"
    Public Const CollectionMethod As String = "COLLMETHOD"
    Public Const ClaimActivityCode As String = "CLACT"
    Public Const ClaimEquipmentTypeCode As String = "CLAIM_EQUIP_TYPE" 'Enrolled, Claimed, Replacement
    Public Const TypeOfEquipment As String = "EQPTYPE" ''''Eg.. Smart Phone, Charger,Battery
    Public Const CommentType As String = "COMMT"
    Public Const ReplacementBasedOn As String = "REPLOG"
    Public Const DeniedReasonCode As String = "DNDREASON"
    Public Const DeductibleCollectionMethodCode As String = "DEDCOLLMTHD"
    Public Const ClaimIssueStatusCode As String = "CLMISSUESTATUS"
    Public Const WhoPaysCode As String = "WPAYS"
    Public Const ClaimAuthorizationTypeCode As String = "CLM_AUTH_TYP"
    Public Const ListPriceAmountTypeCode As String = "LPAMOUNTTYPE"
    Public Const ClaimExtendedStatusCode As String = "CLMSTAT"
    Public Const ReasonClosedCode As String = "RESCL"
    Public Const ClaimExtendedStatusDefaultType As String = "ECSDT"
    Public Const PersonType As String = "PERSON_TYPE"
    Public Const EquipmentClassCode As String = "EQPCLS" ''''Eg. PremiumTier, Base, DataCard
    Public Const DeviceCondition As String = "DEVICE" ''''Eg..New,Refurbished
    Public Const TaxTypeCode As String = "TTYP"
    Public Const ProductTaxTypeCode As String = "PTT"
    Public Const CauseOfLoss As String = "CAUSE"
    Public Const PeriodicBilling As String = "PERIODRENEW"
End Class

Public Class TaxTypeCodes
    Public Const Pos As String = "1"
    Public Const Premiums As String = "2"
    Public Const Claims As String = "7"
    Public Const Commissions As String = "6"
End Class

Public Class ProductTaxTypeCodes
    Public Const All As String = "AL"
    Public Const HomeWarranty As String = "HW"
End Class

Public Class CertificateStatusCodes
    Public Const Active As String = "A"
    Public Const Cancelled As String = "C"
End Class

Public Class ClaimDenialCodes
    Public Const DeniedUnderDeductible As String = "DDUCT"
    Public Const Voided As String = "DVOID"
    Public Const Denied As String = "DENY"
End Class

Public Class CoverageTypeCodes
    Public Const Upgrade As String = "UPG"
    Public Const EngineComponents As String = "1"
    Public Const ManualTransmission As String = "2"
    Public Const CoolingComponents As String = "4"
    Public Const PowerSteeringComponents As String = "5"
    Public Const SuspensionComponents As String = "8"
    Public Const FuelDeliveryComponents As String = "9"
    Public Const BrakeComponentsWithoutABS As String = "6"
    Public Const DriveAxleComponents As String = "3"
    Public Const Electrical As String = "7"
    Public Const SealsAndGaskets As String = "10"
    Public Const Others As String = "11"
    Public Const AC As String = "A1"
    Public Const AutomaticTransmission As String = "B1"
    Public Const Drive4X4 As String = "C1"
    Public Const ABS As String = "D1"
    Public Const HighTech As String = "E1"
    Public Const Turbo As String = "F1"
    Public Const RoadSide As String = "G1"
    Public Const Propane As String = "H1"
    Public Const BusinessUse As String = "I1"
    Public Const Plan As String = "0"
    Public Const Legal As String = "L"
    Public Const Extended As String = "E"
    Public Const Manufacturer As String = "M"
    Public Const MechanicalBreakdown As String = "B"
    Public Const TheftLoss As String = "T"
    Public Const Accidental As String = "A"
    Public Const AccidentalDeath As String = "D"
    Public Const TotalPermanentDisability As String = "P"
    Public Const DealerManufacturerWarranty As String = "DMW"
    Public Const AdminExpenses As String = "ADM"
    Public Const UnauthorizedCalls As String = "UC"
    Public Const AuditInspection As String = "13"
    Public Const CashRepair As String = "R"
    Public Const Loss As String = "L1"
    Public Const Theft As String = "T2"
    Public Const Accessories As String = "S"
    Public Const HydraulicSteering As String = "K1"
    Public Const Installation As String = "I"
    Public Const Conversion As String = "C"
    Public Const PickUp As String = "J1"
    Public Const OEM As String = "O"
    Public Const LegalInspection As String = "14"
    Public Const TheftNoClaims As String = "T1"
    Public Const SoftwareFailure As String = "SF"
    Public Const TechnicalInspection As String = "12"
    Public Const CardWarranty As String = "CW"
    Public Const PowerSurge As String = "PS"
    Public Const ESC As String = "CTYP_I_ESC"
    Public Const JUMP As String = "CTYP_I_JUMP"
    Public Const Insurance As String = "CTYP_I_INS"
    Public Const GoogleCredit As String = "CRD"

End Class

Public Class MethodofRepairCodes
    Public Const CarryIn As String = "C"
    Public Const Home As String = "H"
    Public Const Replacement As String = "R"
    Public Const Legal As String = "L"
    Public Const General As String = "G"
    Public Const Automotive As String = "A"
    Public Const Recovery As String = "RC"
    Public Const PickUp As String = "P"
    Public Const SendIn As String = "S"
    Public Const Labor As String = "LA"

End Class

Public Class ClaimActivityCodes
    Public Const PendingReplacement As String = "PREP"
    Public Const Replaced As String = "CLREP"
    Public Const ToBeReplaced As String = "TBREP"
    Public Const Rework As String = "REWRK"
    Public Const LegalGeneral As String = "LGGL"
End Class

Public Class YesNoCodes
    Public Const Yes As String = "Y"
    Public Const No As String = "N"
End Class

Public Class ClaimEquipmentTypeCodes
    Public Const Claimed As String = "C"
    Public Const Enrolled As String = "E"
    Public Const Replacement As String = "R"
    Public Const ReplacementOption As String = "RO"
End Class

Public Class DeductibleBasedOnCodes
    Public Const Fixed As String = "FIXED"
    Public Const PercentageOfItemRetailPrice As String = "ITEM"
    Public Const PercentageOfAuthAmount As String = "AUTH"
    Public Const SalesPrice As String = "SALES"
    Public Const PercentageOfListPrice As String = "LIST"
    Public Const PercentageOfOrigRetailPrice As String = "ORIG"
    Public Const Expression As String = "EXP"
    Public Const ComputedExternally As String = "COMPUTED_EXTERNALLY"
End Class

Public Class DealerTypeCodes
    Public Const Esc As String = "1"
    Public Const Vsc As String = "2"
    Public Const Wepp As String = "3"
End Class

Public Class RuleCodes
    Public Const PoliceReportRequired As String = "RQPRPT"
    Public Const Troubleshooting As String = "TRBSHTRL"
    Public Const Upgrade As String = "UPGRDRL"
    Public Const DeductibleCollection As String = "DEDCOLRL"
    Public Const ClaimDocumentsRequired As String = "CLMDOCRL"
    Public Const Device As String = "DEVICE"
    Public Const TheftDocumentationRules As String = "THFDOCRL"
End Class

Public Class ClaimStatusCodes
    Public Const Active As String = "A"
    Public Const Pending As String = "P"
    Public Const Closed As String = "C"
    Public Const Denied As String = "D"
End Class

Public Class CommentTypeCodes
    Public Const ComplaintCustomer As String = "COMC"
    Public Const CustomerCall As String = "CCAL"
    Public Const ReplacementRecordCreated As String = "RPCR"
    Public Const ReworkRecordCreated As String = "RWKR"
    Public Const LegalGeneralRecordCreated As String = "LEGE"
    Public Const ClaimRecordCreated As String = "CLCR"
    Public Const PendingClaimNotApproved As String = "PCNA"
    Public Const PendingClaimApproved As String = "PCAPR"
    Public Const PaymentReversal As String = "PAYR"
    Public Const PaymentAdjustment As String = "PADJ"
    Public Const ClaimDeniedReplacementExceed As String = "DMAXR"
    Public Const ClaimDeniedReportTimeExceed As String = "DNOL"
    Public Const ClaimPendingSubscriberStatusNotValid As String = "PCDTS"
    Public Const ClaimDenied As String = "CLMDN"
    Public Const ClaimSetToPending As String = "PEND"
    Public Const CertCancelRequest As String = "CNREQ"
    Public Const PendingPaymentOnOutstandingPremium As String = "PPOP"
    Public Const MakeModelImeiMismatch As String = "WEBCLM"
    Public Const Other As String = "TYOTH"
    Public Const CallBackCustomer As String = "CBC"
    Public Const ClaimedEquipmentNotConfigured As String = "CPCENR"
    Public Const EnrolledEquipmentNotConfigured As String = "CPENR"
    Public Const Completed As String = "CUMP"
    '5623  
    Public Const ClaimDeniedReportTimeNotWithInGracePeriod As String = "DNGP"
    Public Const ClaimDeniedCoverageTypeMissing As String = "DNCTM"
End Class
Public Class SubscriberStatusCodes
    Public Const Active As String = "A"
    Public Const Cancelled As String = "C"
    Public Const Suspended As String = "S"
    Public Const PastDue As String = "P"
    Public Const PastDueClaimsAllowed As String = "PA"
End Class

Public Class ReplacementBasedOnCodes
    Public Const DateOfLoss As String = "DOL"
    Public Const InsuranceActivationDate As String = "IAD"
End Class

Public Class DeniedReasonCodes
    'Denied Reason
    Public Const MaxOccurencesReached As String = "DMXOC"
    Public Const ClaimIssueRejected As String = "DCIR"
    Public Const ManufacturerWarranty As String = "DMF"
    Public Const NoCoverage As String = "DNOCO"
    Public Const SerialNumberMismtach As String = "DSNMIS"
    Public Const MaxCoverageLiabilityLimitReached As String = "DMXCL"
    Public Const MaxProductLiabilityLimitReached As String = "DMXPL"
    Public Const NoProof As String = "DNOPF"
    Public Const ElectricalDischarge As String = "DNELED"
    Public Const NotReportedWithinGracePeriod As String = "DNGP"
    Public Const CoverageTypeMissing As String = "DNCTM"
    Public Const ReplacementsExceeded As String = "DMXAR"
    Public Const NoProblemFound As String = "DNPF"
    Public Const UnderMFGWarranty As String = "DMF"
    Public Const CustomerDesistClaim As String = "DCUSTDC"
End Class

Public Class ReasonClosedCodes
    Public Const REASON_CLOSED__TO_BE_REPLACED As String = "TBRP"
    Public Const REASON_CLOSED__TO_BE_PAID As String = "PAID"
    Public Const REASON_CLOSED__TO_BE_REPAIRED As String = "REP"
    Public Const REASON_CLOSED__DENIED_UNDER_DEDUCTIBLE As String = "DDUCT"
    Public Const REASON_CLOSED__PENDING_CLAIM_NOT_APPROVED As String = "PCNA"
    Public Const REASON_CLOSED__DENIED_VOIDED As String = "DVOID"
    Public Const ReplacementExceeded As String = "DMAXR"
    Public Const NotReportedWithinPeriod As String = "DNOL"
    Public Const REASON_CLOSED__ACTIVE_TRADEIN_QUOTE_EXISTS As String = "DATQ"
    Public Const REASON_CLOSED__NO_ACTIVITY As String = "NOACT"
    Public Const REASON_CLOSED_DENIED As String = "DENY"
    Public Const FLP_NO = "NO"
    Public Const REASON_CLOSED_LLE As String = "LLE"
End Class

Public Class DeductibleCollectionCodes
    Public Const CashOnDelivery As String = "COD"
    Public Const CreditCard As String = "CC"
    Public Const DeferredCollection As String = "DEFCOLL"
    Public Const CreditCardAuthCodeLength As String = "6"

End Class

Public Class ClaimIssueStatusCodes
    Public Const Open As String = "OPEN"
    Public Const Waived As String = "WAIVED"
    Public Const Rejected As String = "REJECTED"
    Public Const Resolved As String = "RESOLVED"
    Public Const Pending As String = "PENDING"
    Public Const ReOpen As String = "REOPEN"
End Class

Public Class WhoPaysCodes
    Public Const Assurant As String = "AIZ"
    Public Const Manufacturer As String = "MFG"
    Public Const Dealer As String = "DLR"
    Public Const Customer As String = "CUS"

End Class

Public Class ClaimAuthorizationTypes
    Public Const Single_Auth As String = "S"
    Public Const Multi_Auth As String = "M"
End Class

Public Class ServiceClassCodes
    Public Const Repair As String = "REPAIR"
    Public Const Replacement As String = "REPLACEMENT"
    Public Const Deductible As String = "DEDUCTIBLE"
End Class

Public Class ServiceTypeCodes
    Public Const HomePrice As String = "HOME_PRICE"
    Public Const RepairPrice As String = "REPAIR"
    Public Const SendInPrice As String = "SEND_IN_PRICE"
    Public Const PickUpPrice As String = "PICK_UP_PRICE"
    Public Const CarryInPrice As String = "CARRY_IN_PRICE"
    Public Const CleaningPrice As String = "CLEANING_PRICE"
    Public Const EstimatePrice As String = "ESTIMATE_PRICE"
    Public Const ReplacementPrice As String = "REPLACEMENT_PRICE"
    Public Const DiscountedPrice As String = "DISCOUNTED_PRICE"
    Public Const Labor As String = "LA"
    Public Const Deductible As String = "DEDUCTIBLE"
    Public Const ServiceWarranty As String = "SERVICE_WARRANTY"
    Public Const Dianostics As String = "DA"
    Public Const Logistics As String = "LGA"
    Public Const Parts As String = "PA"
    Public Const DeductibleBasePrice As String = "DED_BASE_PRICE"

End Class

Public Class LossTypeCodes
    Public Const TotalLoss As String = "T"
    Public Const PartialLoss As String = "P"
End Class

Public Class ListPriceAmountTypeCodes
    Public Const ListPrice As String = "LPRICE"
    Public Const RepairAuthorizedAmount As String = "RAAMOUNT"
End Class

Public Class ClaimExtendedStatusDefaultTypeCodes
    Public Const ClaimReadyForPayment As String = "CRFP"
    Public Const ReplaceClaim As String = "RC"
    Public Const ClaimTurnaroundTimeTo As String = "CTT"
    Public Const ServiceCenterTurnaroundTimeFrom As String = "SCTF"
    Public Const ClaimPreinvoiceApproved As String = "CPIA"
    Public Const ClaimRemovedFromPreinvoice As String = "CRFPI"
    Public Const ServiceCenterTurnaroundTimeTo As String = "SCTT"
    Public Const NewClaim As String = "NC"
    Public Const ClaimWaitingBudgetApproval As String = "CWBA"
    Public Const PayClaim As String = "PC"
    Public Const ClaimTurnaroundTimeFrom As String = "CTF"
    Public Const DefaultServiceCenterAssignedToClaim As String = "DSCATC"

End Class

Public Class ClaimTypeCodes
    Public Const Repair As Integer = 0
    Public Const OriginalReplacement As Integer = 1
    Public Const Replacement As Integer = 2
    Public Const ServiceWarranty As Integer = 3
End Class

Public Class EquipmentConditionCodes
    Public Const [New] As String = "N"
    Public Const USED As String = "U"
    Public Const NEWORUSED As String = "R"
End Class

Public Class UpdateActionTypeCodes
    Public Const UnderMFW As String = "RUOEM"
    Public Const NoProblemFound As String = "NVF"

End Class

Public Class EquipmentClassCodes
    Public Const HandSet As String = "HSET"
    Public Const HighTier As String = "HTIER"
    Public Const LowTier As String = "LTIER"
    Public Const MidTier As String = "MTIER"
    Public Const PremiumTier As String = "PTIER"
    Public Const DataCard As String = "DC"
    Public Const Base As String = "BASE"
End Class

Public Class CauseOfLossTypeCodes
    Public Const LiquidDamage As String = "LIQUIDONLY"
    Public Const ScreenAndLiquid As String = "SCREENLQ"
    Public Const BrokenScreen As String = "BRSCREEN"
    Public Const OtherDamage As String = "OTHERDMG"
End Class

'Accounting Status
Public Class AccountingStatusCodes
    Public Const REQUESTED As String = "R"
    Public Const ISSUED As String = "I"
    Public Const REFERFINANCE As String = "F"
    Public Const NOTISSUED As String = "N"
End Class
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectData.Common
Imports System.Threading

Public Class GalaxyUpdateClaim
    Inherits BusinessObjectBase

#Region "Member Variables"
    Private IsVisitDateNull As Boolean = True
    Private IsLiabilityLimitNull As Boolean = True
    Private IsStatusCodeNull As Boolean = True
    Private IsServiceCenterCodeNull As Boolean = True
    Private IsProblemDescriptionNull As Boolean = True
    Private IsSpecialInstructionNull As Boolean = True
    Private IsReasonClosedCodeNull As Boolean = True
    Private CauseOfLossId As Guid = Guid.Empty
    Private dsItemCoverages As DataSet
    Private dsCoverageInfo As DataSet
    Private Cert_Id As Guid = Guid.Empty
    Private _coverage_mi_km As Integer
    Private _MFG_MAX_Mileage_Limit As Integer
    Private _new_used As String
    Private _cert_RemainingBalance As Decimal
#End Region

#Region "Constants"

    Public TABLE_NAME As String = "GalaxyUpdateClaim"
    Public Const INSERT_FAILED As String = "ERR_INSERT_FAILED"
    Public Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Private Const REASON_CODE_OPEN_IN_ERROR = "OERR"
    Private Const REASON_CODE_EXPENSE_REMOVED = "RMEXP"


    Public TABLE_NAME_COVERAGE As String = "COVERAGES"
    Public TABLE_NAME_COVERAGE_INFO As String = "COVERAGES_INFO"

    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"
    Private Const ERR_REASON_CLOSED_CODE_REQUIRED As String = "ERR_REASON_CLOSED_CODE_REQUIRED"
    Private Const ERR_REASON_CLOSED_CODE_NOT_FOUND As String = "ERR_REASON_CLOSED_CODE_NOT_FOUND"
    Private Const ERR_STATUS_CODE_AND_REASON_CLOSED_CODE_CONFLICT As String = "ERR_STATUS_CODE_AND_REASON_CLOSED_CODE_CONFLICT"
    Private Const ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE As String = "ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE"
    Private Const INVALID_REPAIR_CODE As String = "INVALID_REPAIR_CODE_ERR"
    Private Const INVALID_CAUSE_OF_LOSS_CODE As String = "INVALID_CAUSE_OF_LOSS_CODE_ERR"
    Private Const INVALID_STATUS_CODE As String = "INVALID_STATUS_CODE"
    Private Const INVALID_AMOUNT As String = "INVALID_AMOUNT"

    Private Const CERTIFICATE_COVERAGES_NOT_FOUND As String = "ERR_CERTIFICATE_COVERAGES_NOT_FOUND"
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"

    Private Const SOURCE_COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const SOURCE_COL_STATUS_CODE As String = "STATUS_CODE"
    Private Const SOURCE_COL_SERVICE_CENTER_CODE As String = "SERVICE_CENTER_CODE"
    Private Const SOURCE_COL_REASON_CLOSED_CODE As String = "REASON_CLOSED_CODE"
    Private Const SOURCE_COL_PROBLEM_DESCRIPTION As String = "PROBLEM_DESCRIPTION"
    Private Const SOURCE_COL_SPECIAL_INSTRUCTION As String = "SPECIAL_INSTRUCTION"
    Private Const SOURCE_COL_ASSURANT_PAY_AMOUNT As String = "ASSURANT_PAY_AMOUNT"
    Private Const SOURCE_COL_LIABILITY_LIMIT As String = "LIABILITY_LIMIT"
    Private Const SOURCE_COL_VISIT_DATE As String = "VISIT_DATE"
    Private Const SOURCE_COL_PICK_UP_DATE As String = "PICK_UP_DATE"
    Private Const SOURCE_COL_CERT_ITEM_COVERAGE_ID As String = "CERT_ITEM_COVERAGE_ID"
    Private Const SOURCE_COL_COVERAGE_CODE = "COVERAGE_CODE"
    Private Const SOURCE_COL_DEDUCTIBLE = "DEDUCTIBLE"
    Private Const SOURCE_COL_EXIST_IN_ELITA = "EXIST_IN_ELITA"
    Private Const SOURCE_COL_CLAIM_ID = "CLAIM_ID"
    Private Const SOURCE_COL_UNIT_NUMBER As String = "UNIT_NUMBER"
    Private Const SOURCE_COL_CAUSE_OF_LOSS_CODE As String = "CAUSE_OF_LOSS_CODE"
    Private Const SOURCE_COL_LOSS_DATE As String = "LOSS_DATE"
    Private Const CLAIM_NUMBER_OFFSET As Integer = 50
    Private Const DATA_COL_NAME_COVERAGE_KM_MI As String = "coverage_km_mi"
    Private Const DATA_COL_NAME_MFG_MAX_MILEAGE_LIMIT As String = "MFG_Max_Mileage_Limit"
    Private Const DATA_COL_NAME_MFG_NEW_USED As String = "new_used"
    Private Const DATA_COL_NAME_REMAINING_BALANCE As String = "remaining_balance"


    'Added for Def-1782
    Public Const SOURCE_COL_INVOICE_DATE As String = "INVOICE_DATE"
    Private Const SOURCE_COL_CURRENT_ODOMETER = "CURRENT_ODOMETER"
    Public Const CANCELLATION_REASON_CODE As String = "EXM" 'Expiration Due Mileage Limit

#End Region

#Region "Constructors"

    Public Sub New(ds As GalaxyUpdateClaimDs)
        MyBase.New()

        dsCoverageInfo = New DataSet
        Dim dt As DataTable = New DataTable(TABLE_NAME_COVERAGE_INFO)
        dt.Columns.Add(SOURCE_COL_CERT_ITEM_COVERAGE_ID, GetType(Guid))
        dt.Columns.Add(SOURCE_COL_ASSURANT_PAY_AMOUNT, GetType(Decimal))
        dt.Columns.Add(SOURCE_COL_CLAIM_NUMBER, GetType(String))
        dt.Columns.Add(SOURCE_COL_COVERAGE_CODE, GetType(String))
        dt.Columns.Add(SOURCE_COL_DEDUCTIBLE, GetType(Decimal))
        dsCoverageInfo.Tables.Add(dt)

        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Member Methods"

    Private Sub PopulateBOFromWebService(ds As GalaxyUpdateClaimDs)
        Try
            If ds.GalaxyUpdateClaim.Count = 0 Then Exit Sub
            With ds.GalaxyUpdateClaim.Item(0)
                ClaimNumber = .CLAIM_NUMBER  ' Galaxy Claim Number
                UnitNumber = .UNIT_NUMBER
                CauseOfLossCode = .CAUSE_OF_LOSS_CODE
                CauseOfLossId = LookupListNew.GetIdFromCode(LookupListCache.LK_CAUSES_OF_LOSS, CauseOfLossCode)

                If CauseOfLossId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GalaxyUpdateClaim Error: ", INVALID_CAUSE_OF_LOSS_CODE)
                End If
                LossDate = .LOSS_DATE

                If Not .IsSTATUS_CODENull Then
                    StatusCode = .STATUS_CODE
                    IsStatusCodeNull = False

                    If Not (StatusCode.Equals(Codes.CLAIM_STATUS__PENDING) Or _
                        StatusCode.Equals(Codes.CLAIM_STATUS__ACTIVE) Or _
                        StatusCode.Equals(Codes.CLAIM_STATUS__CLOSED)) Then
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", INVALID_STATUS_CODE)
                    End If
                End If

                If Not .IsSERVICE_CENTER_CODENull Then
                    ServiceCenterCode = .SERVICE_CENTER_CODE
                    IsServiceCenterCodeNull = False
                End If

                If Not .IsPROBLEM_DESCRIPTIONNull Then
                    ProblemDescription = .PROBLEM_DESCRIPTION
                    IsProblemDescriptionNull = False
                End If

                If Not .IsSPECIAL_INSTRUCTIONNull Then
                    SpecialInstruction = .SPECIAL_INSTRUCTION
                    IsSpecialInstructionNull = False
                End If

                If Not .IsVISIT_DATENull Then
                    VisitDate = .VISIT_DATE
                    IsVisitDateNull = False
                End If

                If Not .IsREASON_CLOSED_CODENull Then
                    ReasonClosedCode = .REASON_CLOSED_CODE
                    IsReasonClosedCodeNull = False
                End If

                If Not .IsLIABILITY_LIMITNull Then
                    LiabilityLimit = .LIABILITY_LIMIT
                    IsLiabilityLimitNull = False
                End If

                'Added for Def-1782
                If Not .IsINVOICE_DATENull Then
                    InvoiceDate = .INVOICE_DATE
                End If
                If Not .IsCURRENT_ODOMETERNull Then
                    CurrentOdometer = .CURRENT_ODOMETER
                End If
                ' For Galaxy web service, ClaimNumber = MasterClaimNumber in Elita.
                ' For Elita, ClaimNumber = ClaimNumber + CoverageTypeCode in Galaxy web service
                dsItemCoverages = CertItemCoverage.LoadAllItemCoveragesForGalaxyClaimUpdate(ClaimNumber)  ' Galaxy Claim Number

                If dsItemCoverages Is Nothing OrElse dsItemCoverages.Tables.Count <= 0 OrElse dsItemCoverages.Tables(0).Rows.Count <= 0 Then
                    Throw New BOValidationException("GalaxyUpdateClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                End If

                Dim claimTmpBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
                Dim i As Integer
                For i = 0 To ds.COVERAGES.Count - 1
                    Dim coverageCode As String = ds.COVERAGES(i).CERT_ITEM_COVERAGE_CODE
                    Dim assurant_pay_amount As Decimal
                    Dim deductible As Decimal

                    If CType(ds.COVERAGES(i).ASSURANT_PAY_AMOUNT, DecimalType) IsNot Nothing Then
                        assurant_pay_amount = ds.COVERAGES(i).ASSURANT_PAY_AMOUNT
                    End If

                    deductible = ds.COVERAGES(i).DEDUCTIBLE

                    If CType(UnitNumber, Long) = 1 Then
                        'primary claim
                        If (assurant_pay_amount + deductible) < 0 Then
                            Throw New BOValidationException("GalaxyUpdateClaim Error: ", INVALID_AMOUNT)
                        End If
                    End If

                    Dim certItemCoverageId As Guid = Guid.Empty
                    Dim companyId As Guid = Guid.Empty

                    If dsItemCoverages IsNot Nothing AndAlso dsItemCoverages.Tables.Count > 0 AndAlso dsItemCoverages.Tables(0).Rows.Count > 0 Then
                        Dim dr() As DataRow = dsItemCoverages.Tables(0).Select(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_CODE & "='" & coverageCode & "'")
                        If dr IsNot Nothing AndAlso dr.Length > 0 Then
                            companyId = New Guid(CType(dr(0)(CertItemCoverageDAL.COL_NAME_COMPANY_ID), Byte()))
                            certItemCoverageId = New Guid(CType(dr(0)(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
                            If certItemCoverageId.Equals(Guid.Empty) Then
                                Throw New BOValidationException("GalaxyUpdateClaim Error: ", CERTIFICATE_COVERAGES_NOT_FOUND)
                            End If
                        Else
                            Throw New BOValidationException("GalaxyUpdateClaim Error: ", CERTIFICATE_COVERAGES_NOT_FOUND)
                        End If
                    Else
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", CERTIFICATE_COVERAGES_NOT_FOUND)
                    End If

                    Dim newRow As DataRow = dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).NewRow()
                    newRow(SOURCE_COL_CERT_ITEM_COVERAGE_ID) = certItemCoverageId
                    newRow(SOURCE_COL_ASSURANT_PAY_AMOUNT) = assurant_pay_amount
                    Try
                        ' Returns Elita Claim Number           Input  Galaxy Claim Number
                        newRow(SOURCE_COL_CLAIM_NUMBER) = claimTmpBO.GetExistClaimNumber(companyId, .CLAIM_NUMBER, coverageCode, UnitNumber)
                    Catch ex As Exception
                        ' If the Claim does not exists, It will not qualify for closing or update.  It will need to be inserted
                        newRow(SOURCE_COL_CLAIM_NUMBER) = DBNull.Value
                    End Try

                    newRow(SOURCE_COL_COVERAGE_CODE) = coverageCode
                    newRow(SOURCE_COL_DEDUCTIBLE) = deductible

                    dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).Rows.Add(newRow)

                Next
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Function ProcessWSRequest() As String
        Dim selfThrownException As Boolean = False
        Dim row As DataRow
        '  Dim arrClaimGroup As New ArrayList
        Dim claimFamilyBO As Claim = Nothing
        Dim galaxyClaimNumberList As New ArrayList
        Dim oGalaxyClaimNumber As ClaimDAL.GalaxyClaimNumber

        Try

            Validate()

            ' Get call the claims based on the master claim number
            ' Me.ClaimNumber is Galaxy claim number and Elita master claim number
            Dim closeClaimBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            Dim dsCloseClaims As DataSet = closeClaimBO.GetClaimsByMasterClaimNumber(ClaimNumber)
            Dim isFirstOne As Boolean = True
            Dim defaultServiceCenterID As Guid = Guid.Empty

            'Close claims that not passed for update
            If (dsCloseClaims IsNot Nothing) AndAlso (dsCloseClaims.Tables.Count > 0) AndAlso (dsCloseClaims.Tables(0) IsNot Nothing AndAlso (dsCloseClaims.Tables(0).Rows.Count > 0)) Then
                claimFamilyBO = ClaimFacade.Instance.GetClaim(Of Claim)(New Guid(CType(dsCloseClaims.Tables(0).Rows(0)(SOURCE_COL_CLAIM_ID), Byte())))
                defaultServiceCenterID = claimFamilyBO.ServiceCenterId
            Else
                Throw New BOValidationException("GalaxyUpdateClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
            End If

            ' Close primary claims ONLY
            If (CType(UnitNumber, Long) = 1) Then
                For Each row In dsCloseClaims.Tables(0).Rows
                    Dim closeClaimID As New Guid(CType(row(SOURCE_COL_CLAIM_ID), Byte()))
                    Dim claimBO As Claim

                    If (dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).Select(SOURCE_COL_CLAIM_NUMBER & "='" & CType(row(SOURCE_COL_CLAIM_NUMBER), String) & "'").Length <= 0) Then
                        Dim reasonClosedId As Guid = Guid.Empty
                        Dim dv As DataView = LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        reasonClosedId = LookupListNew.GetIdFromCode(dv, REASON_CODE_EXPENSE_REMOVED)
                        If (reasonClosedId = Guid.Empty) Then
                            reasonClosedId = LookupListNew.GetIdFromCode(dv, REASON_CODE_OPEN_IN_ERROR)
                        End If

                        If (reasonClosedId.Equals(Guid.Empty)) Then
                            selfThrownException = True
                            Throw New BOValidationException("GalaxyUpdateClaim Error: ", ERR_REASON_CLOSED_CODE_NOT_FOUND)
                        End If

                        If claimFamilyBO.Id.Equals(closeClaimID) Then
                            claimFamilyBO.StatusCode = Codes.CLAIM_STATUS__CLOSED
                            claimFamilyBO.ReasonClosedId = reasonClosedId
                        Else
                            claimBO = claimFamilyBO.AddClaim(closeClaimID)
                            claimBO.StatusCode = Codes.CLAIM_STATUS__CLOSED
                            claimBO.ReasonClosedId = reasonClosedId
                        End If
                    End If

                Next
            End If

            For Each row In dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).Rows
                Dim claimID As Guid = Guid.Empty
                Dim claimBO As Claim

                If (row(SOURCE_COL_CLAIM_NUMBER) IsNot DBNull.Value) Then
                    claimID = claimFamilyBO.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, CType(row(SOURCE_COL_CLAIM_NUMBER), String))
                End If

                If Not (CType(UnitNumber, Long) = 1) Then
                    ' Here is supplymental claim   
                    If claimID.Equals(Guid.Empty) Then
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", Common.ErrorCodes.ERR_PRIMARY_CLAIM_NOT_FOUND)
                    End If

                    claimBO = claimFamilyBO.AddClaim(claimID)
                    claimBO.AuthorizedAmount = CType(row(SOURCE_COL_ASSURANT_PAY_AMOUNT), Decimal) + CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)

                    claimBO.Deductible = CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)

                Else
                    ' Here is primary claim  
                    If ((row(SOURCE_COL_CLAIM_NUMBER) Is DBNull.Value) OrElse _
                        (dsCloseClaims.Tables(0).Select(SOURCE_COL_CLAIM_NUMBER & "='" & CType(row(SOURCE_COL_CLAIM_NUMBER), String) & "'").Length <= 0)) Then
                        ' New Claim to Insert
                        claimID = Guid.Empty
                        claimBO = claimFamilyBO.AddClaim(claimID)
                        claimBO.CertItemCoverageId = CType(row(SOURCE_COL_CERT_ITEM_COVERAGE_ID), Guid)
                        claimBO.ServiceCenterId = defaultServiceCenterID
                        claimBO.ClaimNumber = Nothing
                        claimBO.CauseOfLossId = CauseOfLossId
                        claimBO.LossDate = LossDate
                        '  claimBO.ClaimNumber = CType(row(SOURCE_COL_CLAIM_NUMBER), String)

                        '  claimBO.PrePopulate(claimBO.ServiceCenterId, claimBO.CertItemCoverageId, claimBO.ClaimNumber, claimBO.LossDate)
                        claimBO.PrePopulate(claimBO.ServiceCenterId, claimBO.CertItemCoverageId, ClaimNumber, claimBO.LossDate)
                        claimBO.ContactName = "."
                        claimBO.CallerName = "."

                        'Added for Def-1782
                        claimBO.InvoiceDate = InvoiceDate
                        claimBO.CurrentOdometer = CurrentOdometer
                        ' assign ClaimGroupId, ClaimNumber, MasterClaimNumber
                        'If (Not claimBO.AssignClaimNumberInfo(claimBO.ClaimNumber, CType(row(SOURCE_COL_COVERAGE_CODE), String), Me.UnitNumber)) Then
                        '    arrClaimGroup.Add(claimBO.ClaimGroupId)
                        '    Throw New BOValidationException("GalaxyUpdateClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                        'Else
                        '    arrClaimGroup.Add(claimBO.ClaimGroupId)
                        'End If
                        ' claimBO.MasterClaimNumber = Me.ClaimNumber
                        ' It pass Galaxy Claim Number
                        oGalaxyClaimNumber = New ClaimDAL.GalaxyClaimNumber(claimBO.Id, _
                                                ClaimNumber, _
                                                CType(row(SOURCE_COL_COVERAGE_CODE), String), UnitNumber)
                        galaxyClaimNumberList.Add(oGalaxyClaimNumber)

                    Else
                        If claimFamilyBO.Id.Equals(claimID) Then
                            claimBO = claimFamilyBO
                        Else
                            claimBO = claimFamilyBO.AddClaim(claimID)
                        End If
                    End If

                    claimBO.AuthorizedAmount = CType(row(SOURCE_COL_ASSURANT_PAY_AMOUNT), Decimal) + CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)
                    claimBO.Deductible = CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)
                    'Added for Def-1782
                    claimBO.InvoiceDate = InvoiceDate

                End If

                If (IsServiceCenterCodeNull = False) Then
                    If claimBO.RepairDate IsNot Nothing Then
                        selfThrownException = True
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE)
                    Else
                        Dim dvServiceCenter As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
                        If dvServiceCenter IsNot Nothing AndAlso dvServiceCenter.Count > 0 Then
                            Dim ServiceCenterId As Guid = LookupListNew.GetIdFromCode(dvServiceCenter, ServiceCenterCode)
                            If ServiceCenterId.Equals(Guid.Empty) Then
                                selfThrownException = True
                                Throw New BOValidationException("GalaxyUpdateClaim Error: ", INVALID_SERVICE_CENTER_CODE)
                            End If

                            If Not (claimBO.ServiceCenterId.Equals(ServiceCenterId)) Then
                                claimBO.ServiceCenterId = ServiceCenterId
                            End If
                        Else
                            selfThrownException = True
                            Throw New BOValidationException("GalaxyUpdateClaim Error: ", INVALID_SERVICE_CENTER_CODE)
                        End If
                    End If
                End If

                claimBO.CurrentOdometer = CurrentOdometer

                If (IsReasonClosedCodeNull = False) Then
                    If (StatusCode Is Nothing OrElse StatusCode <> Codes.CLAIM_STATUS__CLOSED) Then
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", ERR_STATUS_CODE_AND_REASON_CLOSED_CODE_CONFLICT)
                    Else
                        Dim dv As DataView = LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        claimBO.ReasonClosedId = LookupListNew.GetIdFromCode(dv, ReasonClosedCode)
                        If (claimBO.ReasonClosedId.Equals(Guid.Empty)) Then
                            selfThrownException = True
                            Throw New BOValidationException("GalaxyUpdateClaim Error: ", ERR_REASON_CLOSED_CODE_NOT_FOUND)
                        End If
                    End If
                Else
                    If (IsStatusCodeNull = False AndAlso StatusCode = Codes.CLAIM_STATUS__CLOSED) Then
                        selfThrownException = True
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", ERR_REASON_CLOSED_CODE_REQUIRED)
                    Else
                        If (IsStatusCodeNull = False AndAlso StatusCode <> Codes.CLAIM_STATUS__CLOSED) Then
                            claimBO.ReasonClosedId = Nothing
                        End If
                    End If
                End If

                If (IsStatusCodeNull = False) Then
                    claimBO.StatusCode = StatusCode
                Else
                    claimBO.StatusCode = Codes.CLAIM_STATUS__ACTIVE
                    claimBO.ReasonClosedId = Nothing
                End If

                If (IsProblemDescriptionNull = False) Then
                    claimBO.ProblemDescription = ProblemDescription
                End If

                If (IsSpecialInstructionNull = False) Then
                    claimBO.SpecialInstruction = SpecialInstruction
                End If

                If (IsLiabilityLimitNull = False) Then

                    If claimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Then
                        If LiabilityLimit <> 0 Then
                            Throw New BOValidationException("GalaxyUpdateClaim Error: ", Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                        End If
                    End If

                    If LiabilityLimit < 0 Then
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                    End If

                    'if entered amount > existing liability limit then
                    If LiabilityLimit > claimBO.LiabilityLimit.Value Then
                        Throw New BOValidationException("GalaxyUpdateClaim Error: ", Common.ErrorCodes.INVALID_LIABILITY_LIMIT_ERR)
                    End If

                    claimBO.LiabilityLimit = CType(LiabilityLimit, DecimalType)
                End If

                If (IsVisitDateNull = False) Then
                    claimBO.VisitDate = CType(VisitDate, DateType)
                End If

                claimBO.CalculateFollowUpDate()

            Next

            If Not claimFamilyBO.IsValid Then
                If Not IsValidFollowupDate(claimFamilyBO) Then
                    claimFamilyBO.CalculateFollowUpDate()
                End If
            End If
            claimFamilyBO.moGalaxyClaimNumberList = galaxyClaimNumberList
            claimFamilyBO.Save()

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            'If (arrClaimGroup.Count > 0) Then
            '    claimFamilyBO.RemoveClaimGroup(arrClaimGroup)
            'End If
            Throw New BOValidationException(ex.Message, ex.Code)
        Catch ex As DALConcurrencyAccessException
            'If (arrClaimGroup.Count > 0) Then
            '    claimFamilyBO.RemoveClaimGroup(arrClaimGroup)
            'End If
            Throw ex
        Catch ex As DataBaseUniqueKeyConstraintViolationException
            'If (arrClaimGroup.Count > 0) Then
            '    claimFamilyBO.RemoveClaimGroup(arrClaimGroup)
            'End If
            Throw ex
        Catch ex As Exception
            'If (arrClaimGroup.Count > 0) Then
            '    claimFamilyBO.RemoveClaimGroup(arrClaimGroup)
            'End If
            Throw ex
        End Try
    End Function

    Private Sub MapDataSet(ds As GalaxyUpdateClaimDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    Private Sub Load(ds As GalaxyUpdateClaimDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw New BOValidationException(ex.Message, ex.Code)
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

    Public Function IsValidFollowupDate(claimBO As Claim) As Boolean
        Dim obj As Claim = claimBO

        If ((obj.FollowupDate Is Nothing) OrElse _
            (obj.StatusCode = Codes.CLAIM_STATUS__CLOSED) OrElse _
            (obj.StatusCode = Codes.CLAIM_STATUS__PENDING) OrElse _
            ((obj.FollowupDate.Value >= obj.GetShortDate(Today)) AndAlso _
                (obj.FollowupDate.Value <= NonbusinessCalendar.GetNextBusinessDate(obj.MaxFollowUpDays.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)) AndAlso _
                (NonbusinessCalendar.GetSameBusinessDaysCount(obj.FollowupDate.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) <= 0))) Then
            Return True
        End If

        If ((obj.GetShortDate(obj.FollowupDate.Value) = obj.OriginalFollowUpDate) AndAlso _
            (obj.ReasonClosedId.Equals(Guid.Empty))) Then
            obj.CalculateFollowUpDate()
            Return True
        End If

        If (obj.FollowupDate.Value < obj.GetShortDate(Today)) Then
            Return False
        End If

    End Function

#End Region


#Region "Properties"

    <ValueMandatory("")> _
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property StatusCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_STATUS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_STATUS_CODE, Value)
        End Set
    End Property

    Public Property ServiceCenterCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    Public Property ProblemDescription As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property

    Public Property SpecialInstruction As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property

    Public Property AuthorizedAmount As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_ASSURANT_PAY_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_ASSURANT_PAY_AMOUNT), Decimal)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_ASSURANT_PAY_AMOUNT, Value)
        End Set
    End Property

    Public Property VisitDate As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_VISIT_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_VISIT_DATE, Value)
        End Set
    End Property

    Public Property ReasonClosedCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REASON_CLOSED_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REASON_CLOSED_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_REASON_CLOSED_CODE, Value)
        End Set
    End Property

    Public Property LiabilityLimit As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_LIABILITY_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_LIABILITY_LIMIT), Decimal)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_LIABILITY_LIMIT, Value)
        End Set
    End Property

    Public Property UnitNumber As LongType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_UNIT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(SOURCE_COL_UNIT_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_UNIT_NUMBER, Value)
        End Set
    End Property

    Public Property CauseOfLossCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CAUSE_OF_LOSS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CAUSE_OF_LOSS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CAUSE_OF_LOSS_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property LossDate As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_LOSS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_LOSS_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_LOSS_DATE, Value)
        End Set
    End Property

    'Added for Def-1782
    Public Property InvoiceDate As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_INVOICE_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_INVOICE_DATE, Value)
        End Set
    End Property

    Public Property CurrentOdometer As Integer
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CURRENT_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CURRENT_ODOMETER), Integer)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CURRENT_ODOMETER, Value)
        End Set
    End Property
#End Region

End Class


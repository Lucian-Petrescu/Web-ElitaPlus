Imports System.Threading

Public Class GalaxyInsertClaim
    Inherits BusinessObjectBase

#Region "Member Variables"

    Private CertItemCoverageId As Guid = Guid.Empty
    Private Cert_Id As Guid = Guid.Empty
    Private _coverage_mi_km As Integer
    Private _MFG_MAX_Mileage_Limit As Integer
    Private _new_used As String
    Private _cert_RemainingBalance As Decimal
    Private MethodOfRepairId As Guid = Guid.Empty
    Private FollowupDate As Date
    Private CreatedByUser As String
    Private ClaimGroupId As Guid = Guid.Empty
    Private CompanyId As Guid = Guid.Empty
    Private MasterClaimNumber As String
    Private DeductibleByPercentId As Guid = Guid.Empty
    Private OriginalRiskTypeId As Guid = Guid.Empty
    Private ClaimActivityId As Guid = Guid.Empty
    Private ServiceCenterId As Guid = Guid.Empty
    Private RepairCodeId As Guid = Guid.Empty
    Private CauseOfLossId As Guid = Guid.Empty
    Private DeductiblePercent As Decimal
    Private dsItemCoverages As DataSet
    Private dsCoverageInfo As DataSet
    Private isVisitDateNull As Boolean = False

#End Region

#Region "Constants"

    Public TABLE_NAME As String = "GalaxyInsertClaim"
    Public TABLE_NAME_COVERAGE As String = "COVERAGES"
    Public TABLE_NAME_COVERAGE_INFO As String = "COVERAGES_INFO"
    Public Const INSERT_FAILED As String = "ERR_INSERT_FAILED"
    Public Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"
    Private Const INVALID_REPAIR_CODE As String = "INVALID_REPAIR_CODE_ERR"
    Private Const INVALID_CAUSE_OF_LOSS_CODE As String = "INVALID_CAUSE_OF_LOSS_CODE_ERR"
    Private Const INVALID_STATUS_CODE As String = "INVALID_STATUS_CODE"

    Private Const CERTIFICATE_COVERAGES_NOT_FOUND As String = "ERR_CERTIFICATE_COVERAGES_NOT_FOUND"
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"

    Private Const SOURCE_COL_CERTIFICATE_NUMBER As String = "CERTIFICATE_NUMBER"
    Private Const SOURCE_COL_DEALER_CODE As String = "DEALER_CODE"
    Private Const SOURCE_COL_CERT_ITEM_COVERAGE_CODE As String = "CERT_ITEM_COVERAGE_CODE"
    Private Const SOURCE_COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const SOURCE_COL_CONTACT_NAME As String = "CONTACT_NAME"
    Private Const SOURCE_COL_CALLER_NAME As String = "CALLER_NAME"
    Private Const SOURCE_COL_SERVICE_CENTER_CODE As String = "SERVICE_CENTER_CODE"
    Private Const SOURCE_COL_REPAIR_CODE_CODE As String = "REPAIR_CODE_CODE"
    Private Const SOURCE_COL_CAUSE_OF_LOSS_CODE As String = "CAUSE_OF_LOSS_CODE"
    Private Const SOURCE_COL_PROBLEM_DESCRIPTION As String = "PROBLEM_DESCRIPTION"
    Private Const SOURCE_COL_SPECIAL_INSTRUCTION As String = "SPECIAL_INSTRUCTION"
    Private Const SOURCE_COL_ASSURANT_PAY_AMOUNT As String = "ASSURANT_PAY_AMOUNT"
    Private Const SOURCE_COL_LOSS_DATE As String = "LOSS_DATE"
    Private Const SOURCE_COL_REPAIR_ESTIMATE As String = "REPAIR_ESTIMATE"
    Private Const SOURCE_COL_VISIT_DATE As String = "VISIT_DATE"
    Private Const SOURCE_COL_CALLER_TAX_NUMBER As String = "CALLER_TAX_NUMBER"
    Private Const SOURCE_COL_STATUS_CODE As String = "STATUS_CODE"
    Private Const SOURCE_COL_UNIT_NUMBER As String = "UNIT_NUMBER"
    Private Const DATA_COL_NAME_CERT_ID As String = "cert_id"
    Private Const DATA_COL_NAME_COVERAGE_KM_MI As String = "coverage_km_mi"
    Private Const DATA_COL_NAME_MFG_MAX_MILEAGE_LIMIT As String = "MFG_Max_Mileage_Limit"
    Private Const DATA_COL_NAME_MFG_NEW_USED As String = "new_used"
    Private Const DATA_COL_NAME_REMAINING_BALANCE As String = "remaining_balance"
    Private Const DATA_COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Private Const SOURCE_COL_CERT_ITEM_COVERAGE_ID As String = "CERT_ITEM_COVERAGE_ID"
    Private Const CLAIM_NUMBER_OFFSET As Integer = 50
    Private Const SOURCE_COL_DEDUCTIBLE = "DEDUCTIBLE"
    Private Const SOURCE_COL_CURRENT_ODOMETER = "CURRENT_ODOMETER"

    'Added for Def-1782
    Public Const SOURCE_COL_INVOICE_DATE As String = "INVOICE_DATE"
    Public Const CANCELLATION_REASON_CODE As String = "EXM" 'Expiration Due Mileage Limit
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GalaxyInsertClaimDs)
        MyBase.New()

        dsCoverageInfo = New DataSet
        Dim dt As DataTable = New DataTable(TABLE_NAME_COVERAGE_INFO)
        dt.Columns.Add(SOURCE_COL_CERT_ITEM_COVERAGE_ID, GetType(Guid))
        dt.Columns.Add(SOURCE_COL_ASSURANT_PAY_AMOUNT, GetType(Decimal))
        dt.Columns.Add(SOURCE_COL_CLAIM_NUMBER, GetType(String))
        dt.Columns.Add(SOURCE_COL_DEDUCTIBLE, GetType(Decimal))
        dt.Columns.Add(SOURCE_COL_CERT_ITEM_COVERAGE_CODE, GetType(String))
        dsCoverageInfo.Tables.Add(dt)

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region


#Region "Private Members"

    'Private Sub CheckIfCurrentOdometerExceedsAIZMiles()
    '    If Me.CurrentOdometer > 0 AndAlso Me._cert_RemainingBalance <= 0 Then
    '        Dim _intMaxAssurantMileageLimit As Integer = 0
    '        If Me._new_used = Codes.VEHICLE_COND__NEW Then
    '            _intMaxAssurantMileageLimit = Me._MFG_MAX_Mileage_Limit + Me._coverage_mi_km
    '        ElseIf Me._new_used = Codes.VEHICLE_COND__USED Then
    '            _intMaxAssurantMileageLimit = Me._coverage_mi_km
    '        End If

    '        If _intMaxAssurantMileageLimit > 0 AndAlso Me.CurrentOdometer > _intMaxAssurantMileageLimit Then
    '            Dim dal As New CertCancellationDAL
    '            dal.VSC_CancelPolicy(Me.Cert_Id, Me.CANCELLATION_REASON_CODE, ElitaPlusIdentity.Current.ActiveUser.Id, "GALAXYWS", Nothing, _intMaxAssurantMileageLimit, Me.CurrentOdometer)

    '        End If
    '    End If
    'End Sub

#End Region


#Region "Member Methods"

    Private Sub PopulateBOFromWebService(ByVal ds As GalaxyInsertClaimDs)
        Try
            If ds.GalaxyInsertClaim.Count = 0 Then Exit Sub
            With ds.GalaxyInsertClaim.Item(0)
                Me.CertificateNumber = .CERTIFICATE_NUMBER
                Me.DealerCode = .DEALER_CODE
                Me.ClaimNumber = .CLAIM_NUMBER  ' Galaxy Claim Number
                Me.ContactName = .CONTACT_NAME
                Me.CallerName = .CALLER_NAME
                Me.StatusCode = .STATUS_CODE
                Me.UnitNumber = .UNIT_NUMBER
                Me.CauseOfLossCode = .CAUSE_OF_LOSS_CODE
                Me.LossDate = .LOSS_DATE
                If Not .IsINVOICE_DATENull Then
                    Me.InvoiceDate = .INVOICE_DATE
                End If
                If Not .IsCURRENT_ODOMETERNull Then
                    Me.CurrentOdometer = .CURRENT_ODOMETER
                End If

                '5546
                If Not .IsSERVICE_CENTER_CODENull Then
                    Me.ServiceCenterCode = .SERVICE_CENTER_CODE

                    Dim dvServiceCenter As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
                    If Not dvServiceCenter Is Nothing AndAlso dvServiceCenter.Count > 0 Then
                        'dvServiceCenter.RowFilter = "code=" & Me.ServiceCenterCode
                        ServiceCenterId = LookupListNew.GetIdFromCode(dvServiceCenter, Me.ServiceCenterCode)
                        If ServiceCenterId.Equals(Guid.Empty) Then
                            Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.INVALID_SERVICE_CENTER_CODE)
                        End If
                    Else
                        Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.INVALID_SERVICE_CENTER_CODE)
                    End If
                End If

                If Not (Me.StatusCode.Equals(Codes.CLAIM_STATUS__PENDING) Or Me.StatusCode.Equals(Codes.CLAIM_STATUS__ACTIVE)) Then
                    Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.INVALID_STATUS_CODE)
                End If

                If Not .IsPROBLEM_DESCRIPTIONNull Then
                    Me.ProblemDescription = .PROBLEM_DESCRIPTION
                Else
                    Me.ProblemDescription = "."
                End If

                If Not .IsSPECIAL_INSTRUCTIONNull Then Me.SpecialInstruction = .SPECIAL_INSTRUCTION
                If Not .IsREPAIR_ESTIMATENull Then Me.RepairEstimate = .REPAIR_ESTIMATE
                If Not .IsVISIT_DATENull Then
                    Me.VisitDate = .VISIT_DATE
                Else
                    isVisitDateNull = True
                End If

                If Not .IsCALLER_TAX_NUMBERNull Then Me.CallerTaxNumber = .CALLER_TAX_NUMBER

                Dim i As Integer, dtWSD As Date

                ' Get certificate item coverage id
                Dim _CertificateDetailDataSet As DataSet = Certificate.GalaxyGetCertificateDetail(Me.CertificateNumber, Me.DealerCode)
                If Not _CertificateDetailDataSet Is Nothing AndAlso _CertificateDetailDataSet.Tables.Count > 0 AndAlso _CertificateDetailDataSet.Tables(0).Rows.Count > 0 Then
                    If _CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID) Is DBNull.Value Then
                        Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.CERTIFICATE_NOT_FOUND)
                    Else
                        _CertificateDetailDataSet.DataSetName = Me.TABLE_NAME
                        Cert_Id = New Guid(CType(_CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte()))
                        If Cert_Id.Equals(Guid.Empty) Then
                            Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.CERTIFICATE_NOT_FOUND)
                        End If

                        dtWSD = CType(_CertificateDetailDataSet.Tables(0).Rows(0).Item(DATA_COL_NAME_WARRANTY_SALES_DATE), Date)
                        _MFG_MAX_Mileage_Limit = CType(_CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_MFG_MAX_MILEAGE_LIMIT), Integer)
                        _new_used = _CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_MFG_NEW_USED)
                        _cert_RemainingBalance = _CertificateDetailDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_REMAINING_BALANCE)

                        dsItemCoverages = CertItemCoverage.LoadAllItemCoveragesForGalaxyClaim(Cert_Id)

                        If dsItemCoverages Is Nothing OrElse dsItemCoverages.Tables.Count <= 0 OrElse dsItemCoverages.Tables(0).Rows.Count <= 0 Then
                            Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.CERTIFICATE_COVERAGES_NOT_FOUND)
                        End If

                        Me._coverage_mi_km = dsItemCoverages.Tables(0).Compute("Max(coverage_km_mi)", "")

                    End If
                Else
                    Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.CERTIFICATE_NOT_FOUND)
                End If

                CauseOfLossId = LookupListNew.GetIdFromCode(LookupListNew.LK_CAUSES_OF_LOSS, Me.CauseOfLossCode)

                If CauseOfLossId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.INVALID_CAUSE_OF_LOSS_CODE)
                End If

                For i = 0 To ds.COVERAGES.Count - 1
                    Dim coverageCode As String = ds.COVERAGES(i).CERT_ITEM_COVERAGE_CODE
                    Dim assurant_pay_amount As Decimal
                    Dim deductible As Decimal

                    If Not CType(ds.COVERAGES(i).ASSURANT_PAY_AMOUNT, DecimalType) Is Nothing Then
                        assurant_pay_amount = ds.COVERAGES(i).ASSURANT_PAY_AMOUNT
                    End If

                    deductible = ds.COVERAGES(i).DEDUCTIBLE

                    Dim certItemCoverageId As Guid = Guid.Empty

                    If Not dsItemCoverages Is Nothing AndAlso dsItemCoverages.Tables.Count > 0 AndAlso dsItemCoverages.Tables(0).Rows.Count > 0 Then
                        Dim dr() As DataRow = dsItemCoverages.Tables(0).Select(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_CODE & "='" & coverageCode & "'")
                        If Not dr Is Nothing AndAlso dr.Length > 0 Then
                            certItemCoverageId = New Guid(CType(dr(0)(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
                            If certItemCoverageId.Equals(Guid.Empty) Then
                                Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.CERTIFICATE_COVERAGES_NOT_FOUND)
                            End If
                            'user story 259424 - if the date of loss is between WSD and coverage begin date, log as manufaturer coverage
                            Dim covBeginDate As Date = CType(dr(0)(CertItemCoverageDAL.COL_NAME_BEGIN_DATE),Date)
                            If LossDate < covBeginDate AndAlso LossDate >= dtWSD then
                                'convert to manufacture coverage
                                Dim mCertItemCoverageId As Guid = Guid.Empty
                                'Step 1, check whether there is existing manufacturer coverage, 
                                Dim mdr() As DataRow = dsItemCoverages.Tables(0).Select(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_CODE & "='M'")
                                If mdr Is Nothing OrElse mdr.Length = 0 Then
                                    'no existing manufacturer coverage, add manufacturer coverage to certificate
                                    mCertItemCoverageId = VSCEnrollment.AddManufacturerCoverage(Cert_Id)
                                Else
                                    mCertItemCoverageId = New Guid(CType(mdr(0)(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
                                End If

                                'Step 2, set coverageCode = M and certItemCoverageId as the manufacturer coverage id
                                If (Not mCertItemCoverageId.Equals(Guid.Empty)) Then
                                    certItemCoverageId = mCertItemCoverageId
                                    coverageCode = "M"
                                End If                                
                            End If
                        Else
                            Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.CERTIFICATE_COVERAGES_NOT_FOUND)
                        End If
                    Else
                        Throw New BOValidationException("GalaxyInsertClaim Error: ", Me.CERTIFICATE_COVERAGES_NOT_FOUND)
                    End If

                    Dim newRow As DataRow = dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).NewRow()
                    newRow(SOURCE_COL_CERT_ITEM_COVERAGE_ID) = certItemCoverageId
                    newRow(SOURCE_COL_ASSURANT_PAY_AMOUNT) = assurant_pay_amount
                    newRow(SOURCE_COL_CLAIM_NUMBER) = .CLAIM_NUMBER  ' Galaxy Claim Number
                    newRow(SOURCE_COL_DEDUCTIBLE) = deductible
                    newRow(SOURCE_COL_CERT_ITEM_COVERAGE_CODE) = coverageCode

                    dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).Rows.Add(newRow)

                Next
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Function ProcessWSRequest() As String
        Dim row As DataRow
        '  Dim arrClaimGroup As New ArrayList
        Dim claimFamilyBO As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
        Dim IsFirstOne As Boolean = True
        Dim galaxyClaimNumberList As New ArrayList
        Dim oGalaxyClaimNumber As ClaimDAL.GalaxyClaimNumber

        Try

            Me.Validate()

            'If claimFamilyBO.ServiceCenterId.Equals(Guid.Empty) Then
            '    UpdateClaimServiceCenter(claimFamilyBO)
            '    '{need to find default SC from the country of the claim.Company_Id
            '    'from CompanyObj u go to country }
            'End If

            For Each row In dsCoverageInfo.Tables(TABLE_NAME_COVERAGE_INFO).Rows
                Dim claimBO As Claim

                If IsFirstOne Then
                    claimFamilyBO.CertItemCoverageId = CType(row(SOURCE_COL_CERT_ITEM_COVERAGE_ID), Guid)
                    claimFamilyBO.ServiceCenterId = Me.ServiceCenterId
                    claimFamilyBO.LossDate = Me.LossDate
                    '  claimFamilyBO.ClaimNumber = CType(row(SOURCE_COL_CLAIM_NUMBER), String)
                    claimFamilyBO.ClaimNumber = Nothing

                    '  claimFamilyBO.PrePopulate(claimFamilyBO.ServiceCenterId, claimFamilyBO.CertItemCoverageId, claimFamilyBO.ClaimNumber, claimFamilyBO.LossDate, False, True)
                    claimFamilyBO.PrePopulate(claimFamilyBO.ServiceCenterId, claimFamilyBO.CertItemCoverageId, Me.ClaimNumber, claimFamilyBO.LossDate, False, True)

                    claimFamilyBO.Deductible = CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)
                    claimFamilyBO.AuthorizedAmount = CType(row(SOURCE_COL_ASSURANT_PAY_AMOUNT), Decimal) + CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)

                    claimFamilyBO.CauseOfLossId = Me.CauseOfLossId
                    claimFamilyBO.ContactName = Me.ContactName
                    claimFamilyBO.CallerName = Me.CallerName
                    claimFamilyBO.ProblemDescription = Me.ProblemDescription
                    claimFamilyBO.SpecialInstruction = Me.SpecialInstruction
                    claimFamilyBO.RepairEstimate = Me.RepairEstimate

                    If isVisitDateNull = True Then
                        claimFamilyBO.VisitDate = Nothing
                    Else
                        claimFamilyBO.VisitDate = Me.VisitDate
                    End If

                    claimFamilyBO.CallerTaxNumber = Me.CallerTaxNumber
                    claimFamilyBO.StatusCode = Me.StatusCode

                    'Added for Def-1782
                    claimFamilyBO.InvoiceDate = Me.InvoiceDate

                    claimFamilyBO.CurrentOdometer = Me.CurrentOdometer

                    ' assign ClaimGroupId, ClaimNumber, MasterClaimNumber
                    'If (Not claimFamilyBO.AssignClaimNumberInfo(CType(row(SOURCE_COL_CLAIM_NUMBER), String), CType(row(SOURCE_COL_CERT_ITEM_COVERAGE_CODE), String), Me.UnitNumber)) Then
                    '    Throw New BOValidationException("GalaxyInsertClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                    'Else
                    '    arrClaimGroup.Add(claimFamilyBO.ClaimGroupId)
                    'End If
                    '  claimFamilyBO.MasterClaimNumber = Me.ClaimNumber
                    oGalaxyClaimNumber = New ClaimDAL.GalaxyClaimNumber(claimFamilyBO.Id, _
                                                Me.ClaimNumber, _
                                                CType(row(SOURCE_COL_CERT_ITEM_COVERAGE_CODE), String), Me.UnitNumber)
                    galaxyClaimNumberList.Add(oGalaxyClaimNumber)


                    ' For this new claim, check if supervisor authorization required.
                    If (claimFamilyBO.IsSupervisorAuthorizationRequired) Then
                        claimFamilyBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                    End If
                Else
                    claimBO = claimFamilyBO.AddClaim(Guid.Empty)
                    claimBO.CertItemCoverageId = CType(row(SOURCE_COL_CERT_ITEM_COVERAGE_ID), Guid)
                    claimBO.ServiceCenterId = Me.ServiceCenterId
                    claimBO.LossDate = Me.LossDate
                    ' claimBO.ClaimNumber = CType(row(SOURCE_COL_CLAIM_NUMBER), String)   'Galaxy Claim Number == MasterClaim Number
                    claimBO.ClaimNumber = Nothing

                    claimBO.PrePopulate(claimBO.ServiceCenterId, claimBO.CertItemCoverageId, Me.ClaimNumber, claimBO.LossDate)

                    claimBO.Deductible = CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)

                    claimBO.AuthorizedAmount = CType(row(SOURCE_COL_ASSURANT_PAY_AMOUNT), Decimal) + CType(row(SOURCE_COL_DEDUCTIBLE), Decimal)
                    claimBO.CauseOfLossId = Me.CauseOfLossId
                    claimBO.ContactName = Me.ContactName
                    claimBO.CallerName = Me.CallerName
                    claimBO.ProblemDescription = Me.ProblemDescription
                    claimBO.SpecialInstruction = Me.SpecialInstruction
                    claimBO.RepairEstimate = Me.RepairEstimate

                    If isVisitDateNull = True Then
                        claimBO.VisitDate = Nothing
                    Else
                        claimBO.VisitDate = Me.VisitDate
                    End If

                    claimBO.CallerTaxNumber = Me.CallerTaxNumber
                    claimBO.StatusCode = Me.StatusCode

                    'Added for Def-1782
                    claimBO.InvoiceDate = Me.InvoiceDate

                    ' assign ClaimGroupId, ClaimNumber, MasterClaimNumber
                    'If (Not claimBO.AssignClaimNumberInfo(CType(row(SOURCE_COL_CLAIM_NUMBER), String), CType(row(SOURCE_COL_CERT_ITEM_COVERAGE_CODE), String), Me.UnitNumber)) Then
                    '    Throw New BOValidationException("GalaxyInsertClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NUMBER_ERR)
                    'Else
                    '    arrClaimGroup.Add(claimBO.ClaimGroupId)
                    'End If

                    'claimBO.MasterClaimNumber = Me.ClaimNumber
                    oGalaxyClaimNumber = New ClaimDAL.GalaxyClaimNumber(claimBO.Id, _
                                                Me.ClaimNumber, _
                                                CType(row(SOURCE_COL_CERT_ITEM_COVERAGE_CODE), String), Me.UnitNumber)
                    galaxyClaimNumberList.Add(oGalaxyClaimNumber)

                    ' For this new claim, check if supervisor authorization required.
                    If (claimBO.IsSupervisorAuthorizationRequired) Then
                        claimBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                    End If
                End If

                IsFirstOne = False
            Next
            claimFamilyBO.CertRemainingBalance = Me._cert_RemainingBalance
            claimFamilyBO.VehicleCondition = Me._new_used
            claimFamilyBO.MFG_MAX_MileageLimit = Me._MFG_MAX_Mileage_Limit
            claimFamilyBO.CoverageMiKm = Me._coverage_mi_km
            claimFamilyBO.SourceForCancellation = "GALAXY_INSERT_CLAIM_WS"

            claimFamilyBO.moGalaxyClaimNumberList = galaxyClaimNumberList
            claimFamilyBO.Validate()

            claimFamilyBO.Save()

            'If Me.CurrentOdometer > 0 Then
            '    Dim t As Thread = New Thread(AddressOf Me.CheckIfCurrentOdometerExceedsAIZMiles)
            '    t.Start()
            'End If


            'Dim ds As DataSet = New DataSet
            'Dim dt As DataTable = New DataTable(TABLE_RESULT)
            'dt.Columns.Add(TABLE_RESULT, GetType(String))
            'Dim rw As DataRow = dt.NewRow
            'rw(0) = VALUE_OK
            'dt.Rows.Add(rw)
            'ds.Tables.Add(dt)

            'Return XMLHelper.FromDatasetToXML(ds)

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As Exception
            'If (arrClaimGroup.Count > 0) Then
            '    claimFamilyBO.RemoveClaimGroup(arrClaimGroup)
            'End If
            Throw ex
        End Try
    End Function

    Private Sub MapDataSet(ByVal ds As GalaxyInsertClaimDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    Private Sub Load(ByVal ds As GalaxyInsertClaimDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
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

    Protected Sub UpdateClaimServiceCenter(ByRef ClaimObj As Claim)
        Dim objCompany As Company = ClaimObj.Company
        Dim objCountry As New Country(objCompany.CountryId)

        If Not objCountry.DefaultSCId.Equals(Guid.Empty) Then
            ClaimObj.ServiceCenterId = objCountry.DefaultSCId
        Else
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.SERVICE_CENTER_IS_REQUIRED_ERROR)
        End If

    End Sub

#End Region


#Region "Properties"

    <ValueMandatory("")> _
    Public Property CertificateNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CERTIFICATE_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ContactName() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CONTACT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CONTACT_NAME, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CallerName() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CALLER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CALLER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CALLER_NAME, Value)
        End Set
    End Property

    Public Property ServiceCenterCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    Public Property RepairCodeCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REPAIR_CODE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REPAIR_CODE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_REPAIR_CODE_CODE, Value)
        End Set
    End Property

    Public Property CauseOfLossCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CAUSE_OF_LOSS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CAUSE_OF_LOSS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CAUSE_OF_LOSS_CODE, Value)
        End Set
    End Property

    Public Property ProblemDescription() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property

    Public Property SpecialInstruction() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SPECIAL_INSTRUCTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SPECIAL_INSTRUCTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_SPECIAL_INSTRUCTION, Value)
        End Set
    End Property

    'Public Property AuthorizedAmount() As Decimal
    '    Get
    '        CheckDeleted()
    '        If Row(SOURCE_COL_ASSURANT_PAY_AMOUNT) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(SOURCE_COL_ASSURANT_PAY_AMOUNT), Decimal)
    '        End If
    '    End Get
    '    Set(ByVal Value As Decimal)
    '        CheckDeleted()
    '        Me.SetValue(SOURCE_COL_ASSURANT_PAY_AMOUNT, Value)
    '    End Set
    'End Property

    <ValueMandatory("")> _
    Public Property LossDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_LOSS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_LOSS_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_LOSS_DATE, Value)
        End Set
    End Property

    Public Property RepairEstimate() As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REPAIR_ESTIMATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REPAIR_ESTIMATE), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_REPAIR_ESTIMATE, Value)
        End Set
    End Property

    Public Property VisitDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_VISIT_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_VISIT_DATE, Value)
        End Set
    End Property

    Public Property CallerTaxNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CALLER_TAX_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CALLER_TAX_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CALLER_TAX_NUMBER, Value)
        End Set
    End Property

    Public Property StatusCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_STATUS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_STATUS_CODE, Value)
        End Set
    End Property

    Public Property UnitNumber() As LongType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_UNIT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(SOURCE_COL_UNIT_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_UNIT_NUMBER, Value)
        End Set
    End Property

    'Added for Def-1782
    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_INVOICE_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_INVOICE_DATE, Value)
        End Set
    End Property

    Public Property CurrentOdometer() As Integer
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CURRENT_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CURRENT_ODOMETER), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CURRENT_ODOMETER, Value)
        End Set
    End Property
#End Region

End Class


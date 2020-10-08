Imports System.Text.RegularExpressions

Public Class UpdateClaimData
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

    Private Const TABLE_NAME As String = "UpdateClaimData"
    Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"
    Private Const ERR_REASON_CLOSED_CODE_NOT_FOUND As String = "ERR_REASON_CLOSED_CODE_NOT_FOUND"
    Private Const ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE As String = "ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE"
    Private Const INVALID_REPAIR_CODE As String = "INVALID_REPAIR_CODE_ERR"
    Private Const INVALID_CAUSE_OF_LOSS_CODE As String = "INVALID_CAUSE_OF_LOSS_CODE_ERR"
    Private Const INVALID_STATUS_CODE As String = "INVALID_STATUS_CODE"
    Private Const INVALID_AMOUNT As String = "INVALID_AMOUNT"
    Private Const INVALID_COMMENTS_REQUIRED_ON_COD As String = "INVALID_COMMENTS_REQUIRED_ON_COD"
    Private Const INVALID_STATUS_COMMENTS_REQUIRED_ON_COD As String = "INVALID_STATUS_COMMENTS_REQUIRED_ON_COD"
    Private Const INVALID_STATUS_COMMENTS_REQUIRED_ON_ATSPL_ATSVCPL As String = "INVALID_STATUS_COMMENTS_REQUIRED_ON_ATSPL_ATSVCPL"
    Private Const INVALID_AMOUNT_REQUIRED_ON_COD As String = "INVALID_AMOUNT_REQUIRED_ON_COD"
    Private Const INVALID_AMOUNT_ONLY_REQUIRED_ON_COD As String = "INVALID_AMOUNT_ONLY_REQUIRED_ON_COD"
    Private Const INVALID_CLAIM_STATUS_REQUIRED As String = "INVALID_CLAIM_STATUS_REQUIRED"
    Private Const INVALID_CCOD_CLAIM_STATUS_NOT_FOUND As String = "INVALID_CCOD_CLAIM_STATUS_NOT_FOUND"
    Private Const INVALID_CLAIM_STATUS_NOT_FOUND As String = "INVALID_CLAIM_STATUS_NOT_FOUND"
    Private Const INVALID_CLAIM_NOT_FOUND As String = "INVALID_CLAIM_NOT_FOUND"
    Private Const INVALID_WHO_PAY_NOT_FOUND As String = "INVALID_WHO_PAY_NOT_FOUND"
    Private Const INVALID_COVERAGE_TYPE As String = "INVALID_COVERAGE_TYPE"
    Private Const INVALID_SPECIAL_SERVICE As String = "INVALID_SPECIAL_SERVICE"
    Private Const INVALID_SPECIAL_SERVICE_CLAIM As String = "INVALID_SPECIAL_SERVICE_CLAIM"

    Private Const COD_CLAIM_STATUS_CODE As String = "COD"
    Private Const ATSPL_CLAIM_STATUS_CODE As String = "ATSPL"
    Private Const ATSVCPL_CLAIM_STATUS_CODE As String = "ATSVCPL"
    Private Const CCOD_CLAIM_STATUS_CODE As String = "CCOD"
    Private Const ADH_CLAIM_STATUS_CODE As String = "ADH"
    Private Const MCHB_CLAIM_STATUS_CODE As String = "MCHB"
    Private Const REPAIRED_CLAIM_STATUS_CODE As String = "REPRD"
    Private Const NOTIFICATION_TYPE_LIST_CODE As String = "SNTYP"
    Private Const NOTIFICATION_TYPE_Z0 As String = "Z0"
    Private Const NOTIFICATION_TYPE_Z1 As String = "Z1"
    Private Const NOTIFICATION_TYPE_Z3 As String = "Z3"
    Private Const CASH_REPAIR_COVERAGE_TYPE As String = "CR"
    Private Const WHO_PAYS_LIST_CODE As String = "WPAYS"
    Private Const CUSTOMER_PAYS_CODE As String = "CUS"
    Private Const ASSURANT_PAYS_CODE As String = "AIZ"
    Private Const CERT_ITEM_COVERAGE_TYPE_LIST_CODE As String = "CTYP"
    Private Const ACCIDENTAL_COVERAGE_TYPE_CODE As String = "A"
    Private Const MECHANICAL_BREAKDOWN_COVERAGE_TYPE_CODE As String = "B"
    Private Const ACCIDENTAL_CAUSE_OF_LOSS_CODE As String = "ACCID"
    Private Const SALVAGE_METHOD_OF_REPAIR_CODE As String = "SA"
    Private Const DELIVERED_TO_SERVICE_CENTER_STATUS_CODE As String = "DASC"
    Private Const CLOSE_CLAIM_STATUS_CODE As String = "C"
    Private Const REASON_CLOSE_LIST_CODE As String = "RESCL"
    Private Const SALVAGE_REASON_CLOSE_CODE As String = "SAL"

    Private Const SOURCE_COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const SOURCE_COL_CERT_ITEM_COVERAGE_CODE As String = "CERT_ITEM_COVERAGE_CODE"
    Private Const SOURCE_COL_CLAIM_STATUS As String = "CLAIM_STATUS"
    Private Const SOURCE_COL_SERVICE_CENTER_CODE As String = "SERVICE_CENTER_CODE"
    Private Const SOURCE_COL_REASON_CLOSED_CODE As String = "REASON_CLOSED_CODE"
    Private Const SOURCE_COL_PROBLEM_DESCRIPTION As String = "PROBLEM_DESCRIPTION"
    Private Const SOURCE_COL_SPECIAL_INSTRUCTION As String = "SPECIAL_INSTRUCTION"
    Private Const SOURCE_COL_STATUS_COMMENTS As String = "STATUS_COMMENTS"
    Private Const SOURCE_COL_CLAIM_COMMENTS As String = "CLAIM_COMMENTS"
    Private Const SOURCE_COL_AMOUNT As String = "AMOUNT"
    Private Const SOURCE_COL_LIABILITY_LIMIT As String = "LIABILITY_LIMIT"
    Private Const SOURCE_COL_VISIT_DATE As String = "VISIT_DATE"
    Private Const SOURCE_COL_PICK_UP_DATE As String = "PICK_UP_DATE"
    Private Const SOURCE_COL_CERT_ITEM_COVERAGE_ID As String = "CERT_ITEM_COVERAGE_ID"
    Private Const SOURCE_COL_COVERAGE_CODE As String = "COVERAGE_CODE"
    Private Const SOURCE_COL_DEDUCTIBLE As String = "DEDUCTIBLE"
    Private Const SOURCE_COL_EXIST_IN_ELITA As String = "EXIST_IN_ELITA"
    Private Const SOURCE_COL_CLAIM_ID As String = "CLAIM_ID"
    Private Const SOURCE_COL_EXTERNAL_USER_NAME As String = "EXTERNAL_USER_NAME"
    Private Const SOURCE_COL_SPECIAL_SERVICE_CODE As String = "SPECIAL_SERVICE_CODE"
    Private Const SOURCE_COL_SPECIAL_SERVICE_ONLY_CODE As String = "SPECIAL_SERVICE_ONLY_CODE"

    Private _claimId As Guid = Guid.Empty
    Private _claimStatusByGroupId As Guid = Guid.Empty
    Private _serviceCenterId As Guid = Guid.Empty
    Private _reasonCloseId As Guid = Guid.Empty

    Public Const COL_PRICE_LIST As String = "Price"
#End Region

#Region "Constructors"

    Public Sub New(ds As UpdateClaimDataDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty
    Private _serviceNetworkID As Guid = Guid.Empty


    Private Sub MapDataSet(ds As UpdateClaimDataDs)

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

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ds As UpdateClaimDataDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdateClaimData Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As UpdateClaimDataDs)
        Try
            If ds.UpdateClaimData.Count = 0 Then Exit Sub

            With ds.UpdateClaimData.Item(0)
                ClaimNumber = ds.UpdateClaimData.Item(0).CLAIM_NUMBER
                CertItemCoverageCode = ds.UpdateClaimData.Item(0).CERT_ITEM_COVERAGE_CODE
                ClaimStatusCode = ds.UpdateClaimData.Item(0).CLAIM_STATUS

                If Not .IsSERVICE_CENTER_CODENull Then
                    ServiceCenterCode = ds.UpdateClaimData.Item(0).SERVICE_CENTER_CODE
                End If

                If Not .IsREASON_CLOSED_CODENull Then
                    ReasonClosedCode = ds.UpdateClaimData.Item(0).REASON_CLOSED_CODE
                End If

                If Not .IsPROBLEM_DESCRIPTIONNull Then
                    ProblemDescription = ds.UpdateClaimData.Item(0).PROBLEM_DESCRIPTION
                End If

                If Not .IsSPECIAL_INSTRUCTIONNull Then
                    SpecialInstruction = ds.UpdateClaimData.Item(0).SPECIAL_INSTRUCTION
                End If

                If Not .IsVISIT_DATENull Then
                    VisitDate = ds.UpdateClaimData.Item(0).VISIT_DATE
                End If

                If Not .IsSTATUS_COMMENTSNull Then
                    StatusComments = ds.UpdateClaimData.Item(0).STATUS_COMMENTS
                End If

                If Not .IsAMOUNTNull Then
                    Amount = ds.UpdateClaimData.Item(0).AMOUNT
                End If

                If Not .IsCLAIM_COMMENTSNull Then
                    ClaimComments = ds.UpdateClaimData.Item(0).CLAIM_COMMENTS
                End If

                If Not .IsEXTERNAL_USER_NAMENull Then
                    ExternalUserName = ds.UpdateClaimData.Item(0).EXTERNAL_USER_NAME
                End If

                If Not .IsSPECIAL_SERVICE_CODENull Then
                    SpecialServiceCode = ds.UpdateClaimData.Item(0).SPECIAL_SERVICE_CODE
                End If

                If Not .IsSPECIAL_SERVICE_ONLY_CODENull Then
                    SpecialServiceOnlyCode = ds.UpdateClaimData.Item(0).SPECIAL_SERVICE_ONLY_CODE
                End If
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdateClaimData Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Function ProcessSpecialServiceClaim(oclaim As Claim) As Boolean

        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")

        'Get the special Service
        Dim covLoss As New CoverageLoss
        Dim CoverageLossdv As DataView = covLoss.getCoverageLossForSpecialService(SpecialServiceCode, oclaim.DealerCode)

        Dim strAvailableforServiceCenter As String = LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, New Guid(CType(CoverageLossdv(0)(SpecialServiceDAL.COL_NAME_AVAILABLE_FOR_SERV_CENTER_ID), Byte())))
        If strAvailableforServiceCenter = Codes.YESNO_N Then
            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_SPECIAL_SERVICE_CLAIM)
        End If

        If CoverageLossdv Is Nothing Then
            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_SPECIAL_SERVICE)
        End If

        Dim CauseofLossId As Guid = New Guid(CType(CoverageLossdv(0)(CoverageLossDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
        Dim CoverageTypeID As Guid = New Guid(CType(CoverageLossdv(0)(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
        Dim priceGroupId As Guid = New Guid(CType(CoverageLossdv(0)(SpecialServiceDAL.COL_NAME_PRICE_GROUP_FIELD_ID), Byte()))

        '' Verify the existing cert item qualify for the change
        'If oclaim.CoverageTypeId.Equals(CoverageTypeID) Then
        '    Throw New BOValidationException("UpdateClaimData Error: ", INVALID_COVERAGE_TYPE)
        'End If

        Dim searchCoverageDV As CertItemCoverage.CertItemCoverageSearchDV

        If oclaim.InvoiceProcessDate Is Nothing Then
            searchCoverageDV = CertItemCoverage.GetClaimCoverageType(oclaim.CertificateId, Guid.Empty, oclaim.LossDate, oclaim.StatusCode, Nothing)
        Else
            searchCoverageDV = CertItemCoverage.GetClaimCoverageType(oclaim.CertificateId, Guid.Empty, oclaim.LossDate, oclaim.StatusCode, oclaim.InvoiceProcessDate)
        End If

        If searchCoverageDV Is Nothing Then
            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_COVERAGE_TYPE)
        End If

        Dim coveragetypeCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_COVERAGE_TYPES, CoverageTypeID)
        searchCoverageDV.RowFilter = "coverage_type_code = '" + coveragetypeCode + "'"

        If searchCoverageDV.Count <> 1 Then
            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_COVERAGE_TYPE)
        End If

        If SpecialServiceOnlyCode = Codes.YESNO_N Then  'Combined with Repair NO
            Dim ChildClaim As Claim = oclaim.CreateNewClaim
            ChildClaim.MasterClaimId = oclaim.Id
            ChildClaim.MasterClaimNumber = oclaim.ClaimNumber
            ChildClaim.ReportedDate = DateTime.Now

            ChildClaim.StatusCode = Codes.CLAIM_STATUS__ACTIVE
            ChildClaim.ServiceCenterId = oclaim.ServiceCenterId
            ChildClaim.LossDate = oclaim.LossDate
            ChildClaim.RiskTypeId = oclaim.RiskTypeId
            ChildClaim.CallerName = oclaim.CallerName
            ChildClaim.ContactName = oclaim.ContactName
            Dim claimNumberInfo As ClaimDAL.ClaimNumberInfo

            Dim dal As New ClaimDAL
            claimNumberInfo = dal.GetClaimNumber(oclaim.CompanyId)
            ChildClaim.ClaimGroupId = claimNumberInfo.ClaimGroupId
            ChildClaim.ClaimNumber = claimNumberInfo.ClaimNumber
            ChildClaim.MethodOfRepairId = oclaim.MethodOfRepairId

            Dim calimStat As ClaimStatus = Nothing
            calimStat = oclaim.AddExtendedClaimStatus(Guid.Empty)
            calimStat.ClaimId = ChildClaim.Id
            calimStat.ClaimStatusByGroupId = ClaimStatusByGroupID
            calimStat.StatusDate = DateTime.Now

            ChildClaim.CertItemCoverageId = New Guid(CType(searchCoverageDV(0)(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
            ChildClaim.CalculateFollowUpDate()
            ChildClaim.CauseOfLossId = CauseofLossId

            Dim oCertItemCoverage As New CertItemCoverage(ChildClaim.CertItemCoverageId)
            ChildClaim.Deductible = oCertItemCoverage.Deductible
            ChildClaim.LiabilityLimit = oCertItemCoverage.LiabilityLimits

            ChildClaim.ClaimSpecialServiceId = ChildClaim.GetSpecialServiceValue()
            If Not ChildClaim.CalculateAuthorizedAmountFromPGPrices() Then
                'Set the assurant Pays ID
                ChildClaim.WhoPaysId = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
            End If

            If ExternalUserName IsNot Nothing Then
                calimStat.ExternalUserName = ExternalUserName
            End If

            If StatusComments IsNot Nothing Then
                calimStat.Comments = StatusComments
            End If

            If Not ReasonCloseID.Equals(Guid.Empty) Then
                'Close claim if reason close given
                ChildClaim.StatusCode = Codes.CLAIM_STATUS__CLOSED
                ChildClaim.ReasonClosedId = ReasonCloseID
                ChildClaim.ClaimClosedDate = Now
            ElseIf oclaim.MethodOfRepairCode = SALVAGE_METHOD_OF_REPAIR_CODE AndAlso calimStat.StatusCode = DELIVERED_TO_SERVICE_CENTER_STATUS_CODE Then
                'if method of repair is salvage and the service center received the item, then close the claim
                ChildClaim.StatusCode = Codes.CLAIM_STATUS__CLOSED
                ChildClaim.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), SALVAGE_REASON_CLOSE_CODE)
                ChildClaim.ClaimClosedDate = Now
            End If

            If Not ServiceCenterID.Equals(Guid.Empty) Then
                ' Can not update service center if RepairDate exists
                If ChildClaim.RepairDate IsNot Nothing Then
                    Throw New BOValidationException("UpdateClaimData Error: ", ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE)
                Else
                    ChildClaim.ServiceCenterId = ServiceCenterID
                End If
            End If

            If ClaimStatusCode = REPAIRED_CLAIM_STATUS_CODE Then
                ChildClaim.RepairDate = CType(calimStat.StatusDate.Value, DateType)
            End If

            If ProblemDescription IsNot Nothing Then
                ChildClaim.ProblemDescription = ProblemDescription
            End If

            If SpecialInstruction IsNot Nothing Then
                ChildClaim.SpecialInstruction = SpecialInstruction
            End If

            If VisitDate IsNot Nothing Then
                ChildClaim.VisitDate = VisitDate
            End If

            ' Create new claim comments
            If ClaimComments IsNot Nothing AndAlso ClaimComments <> "" Then
                Dim blnExceedMaxReplacements As Boolean = False
                'If replacement, check max replacement allowed per calendar year
                'REQ-660 Check for both repair and replacement
                blnExceedMaxReplacements = ChildClaim.IsMaxReplacementExceeded(ChildClaim.CertificateId, ChildClaim.LossDate.Value)

                'Call the Create Comment Logic
                Dim c As Comment = ChildClaim.AddNewComment()
                c.Comments = ClaimComments

                'Add comments to indicate that the claim will be closed
                If blnExceedMaxReplacements Then
                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)
                End If
            End If

            If Not ChildClaim.IsValid Then
                If Not IsValidFollowupDate(oclaim) Then
                    ChildClaim.CalculateFollowUpDate()
                End If
            End If
            ChildClaim.Save()       'save the child claim
            Return True
        ElseIf Not String.IsNullOrEmpty(SpecialServiceOnlyCode) AndAlso Not SpecialServiceOnlyCode = Codes.YESNO_Y Then
            Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.WS_XML_INVALID)
        End If

        oclaim.CertItemCoverageId = New Guid(CType(searchCoverageDV(0)(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
        oclaim.CalculateFollowUpDate()
        oclaim.CauseOfLossId = CauseofLossId
        oclaim.HandleSpecialServiceClaimCreation()
        Dim oCertItemCoverage1 As New CertItemCoverage(oclaim.CertItemCoverageId)
        oclaim.Deductible = oCertItemCoverage1.Deductible
        oclaim.LiabilityLimit = oCertItemCoverage1.LiabilityLimits
        Return False
    End Function

    Private Function GetcovergeSearchDV(oClaim As Claim, coveragetypeCode As String) As CertItemCoverage.CertItemCoverageSearchDV
        Dim searchCoverageDV As CertItemCoverage.CertItemCoverageSearchDV = Nothing
        If oClaim.InvoiceProcessDate Is Nothing Then
            searchCoverageDV = CertItemCoverage.GetClaimCoverageType(oClaim.CertificateId, oClaim.CertItemCoverageId, oClaim.LossDate, oClaim.StatusCode, Nothing)
        Else
            searchCoverageDV = CertItemCoverage.GetClaimCoverageType(oClaim.CertificateId, oClaim.CertItemCoverageId, oClaim.LossDate, oClaim.StatusCode, oClaim.InvoiceProcessDate)
        End If

        If searchCoverageDV Is Nothing Then
            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_COVERAGE_TYPE)
        End If

        searchCoverageDV.RowFilter = "coverage_type_code = '" + coveragetypeCode + "'"

        Return searchCoverageDV

    End Function


#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim DiagnosticFeeComment As String = ""

        Try
            Validate()

            If ClaimStatusCode = ATSPL_CLAIM_STATUS_CODE OrElse ClaimStatusCode = ATSVCPL_CLAIM_STATUS_CODE Then
                ClaimStatus.AddClaimToNewPickList(ClaimID, ClaimStatusByGroupID, ExternalUserName, StatusComments)
            Else
                Dim oClaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(ClaimID)
                Dim oClaimStatus As ClaimStatus = Nothing

                If Not ClaimStatusByGroupID.Equals(Guid.Empty) Then
                    If ClaimStatusCode = COD_CLAIM_STATUS_CODE Then
                        ' *** Handling change to COD

                        ' Status comments is required if extended claim status = COD; this field will be output in GetClaimInfo web service.
                        If StatusComments Is Nothing Then
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_STATUS_COMMENTS_REQUIRED_ON_COD)
                        Else
                            ' Copy status comments to claim comments if it is blank
                            If ClaimComments Is Nothing OrElse ClaimComments = "" Then
                                ClaimComments = StatusComments
                            End If
                        End If

                        ' Amount is required if extended claim status = COD
                        If Amount Is Nothing Then
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_AMOUNT_REQUIRED_ON_COD)
                        End If

                        If Not oClaim.NotificationTypeId.Equals(Guid.Empty) Then
                            Dim notificationTypeCode As String = LookupListNew.GetCodeFromId(LookupListNew.GetNotificationTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), oClaim.NotificationTypeId)
                            Dim maxClaimStatus As ClaimStatus = ClaimStatus.GetLatestClaimStatus(ClaimID)

                            ' Insert CCOD (Changed to COD) status prior to the COD (Waiting on Budget Approval) status
                            ' to differ from wheather the claim is cash repair initially or changed to COD from others.
                            If notificationTypeCode IsNot Nothing AndAlso
                                (notificationTypeCode = NOTIFICATION_TYPE_Z0 OrElse (notificationTypeCode = NOTIFICATION_TYPE_Z1 AndAlso oClaim.CoverageTypeCode <> CASH_REPAIR_COVERAGE_TYPE)) AndAlso
                                (maxClaimStatus IsNot Nothing AndAlso maxClaimStatus.StatusCode <> COD_CLAIM_STATUS_CODE) Then

                                Dim ccodClaimStatusByGroupId As Guid = Guid.Empty
                                ccodClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(CCOD_CLAIM_STATUS_CODE)

                                If ccodClaimStatusByGroupId.Equals(Guid.Empty) Then
                                    Throw New BOValidationException("UpdateClaimData Error: ", INVALID_CCOD_CLAIM_STATUS_NOT_FOUND)
                                End If

                                Dim ccodClaimStatus As ClaimStatus = Nothing
                                ccodClaimStatus = oClaim.AddExtendedClaimStatus(Guid.Empty)
                                ccodClaimStatus.ClaimId = ClaimID
                                ccodClaimStatus.ClaimStatusByGroupId = ccodClaimStatusByGroupId
                                ccodClaimStatus.StatusDate = DateTime.Now.AddMilliseconds(-1.0)

                                If ExternalUserName IsNot Nothing Then
                                    ccodClaimStatus.ExternalUserName = ExternalUserName
                                End If

                                If StatusComments IsNot Nothing Then
                                    ccodClaimStatus.Comments = StatusComments
                                End If
                            ElseIf notificationTypeCode IsNot Nothing AndAlso notificationTypeCode = NOTIFICATION_TYPE_Z3 Then
                                ' Dim oPriceDetail As PriceGroupDetail = oClaim.GetCurrentPriceGroupDetail()
                                Dim priceListdv As DataView
                                Dim equipConditionid As Guid
                                Dim equipmentId As Guid
                                Dim equipClassId As Guid

                                If (LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, oClaim.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
                                    equipConditionid = LookupListNew.GetIdFromCode(LookupListCache.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
                                    If oClaim.ClaimedEquipment Is Nothing OrElse oClaim.ClaimedEquipment.EquipmentBO Is Nothing Then
                                        Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
                                        Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
                                    End If
                                    equipmentId = oClaim.ClaimedEquipment.EquipmentBO.Id
                                    equipClassId = oClaim.ClaimedEquipment.EquipmentBO.EquipmentClassId
                                End If

                                'If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, oClaim.Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
                                '    equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'

                                '    If Not oClaim.ClaimedEquipment Is Nothing AndAlso Not oClaim.ClaimedEquipment.EquipmentBO Is Nothing Then
                                '        equipmentId = oClaim.ClaimedEquipment.EquipmentBO.Id
                                '        equipClassId = oClaim.ClaimedEquipment.EquipmentBO.EquipmentClassId

                                '    ElseIf Not oClaim.ClaimedEquipment Is Nothing Then
                                '        equipmentId = Equipment.FindEquipment(oClaim.Dealer.Dealer, oClaim.ClaimedEquipment.Manufacturer, oClaim.ClaimedEquipment.Model, Date.Today)
                                '        If (Not equipmentId = Guid.Empty) Then
                                '            equipClassId = New Equipment(equipmentId).EquipmentClassId
                                '        End If

                                '    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oClaim.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
                                '        Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
                                '        Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
                                '    End If
                                'End If

                                priceListdv = PriceListDetail.GetRepairPrices(oClaim.CompanyId, oClaim.ServiceCenterObject.Code, oClaim.LossDate, oClaim.RiskTypeId,
                                                                      oClaim.Certificate.SalesPrice.Value, equipClassId,
                                                                     equipmentId, equipConditionid, oClaim.Dealer.Id, String.Empty)
                                If priceListdv Is Nothing Then
                                    Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.INVALID_PRICE_LIST)
                                End If
                                'If oPriceDetail Is Nothing Then
                                '    Throw New BOValidationException("UpdateClaimData Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PRICE_GROUP)
                                'End If
                                'calculating the estimate price
                                Dim nestimatePrice As New DecimalType(0)
                                Dim dvEstimate As DataView = PriceListDetail.GetPricesForServiceType(oClaim.CompanyId, oClaim.ServiceCenterObject.Code,
                                                oClaim.RiskTypeId, oClaim.LossDate, oClaim.Certificate.SalesPrice.Value,
                                                  LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, Codes.SERVICE_CLASS__REPAIR),
                                                  LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, Codes.SERVICE_TYPE__ESTIMATE_PRICE),
                                                  equipClassId, equipmentId, equipConditionid, oClaim.Dealer.Id, String.Empty)

                                If dvEstimate IsNot Nothing AndAlso dvEstimate.Count > 0 Then
                                    nestimatePrice = CDec(dvEstimate(0)(COL_PRICE_LIST))
                                End If
                                'DiagnosticFeeComment = "Tarifa Diagnostico: " & CType(oPriceDetail.EstimatePrice, String) & ". "
                                DiagnosticFeeComment = "Tarifa Diagnostico: " & CType(nestimatePrice, String) & ". "
                            End If
                        End If

                        ' Assign to deductible, AuthorizedAmount for COD; Assurant not paying anything!!!
                        oClaim.Deductible = Amount
                        oClaim.AuthorizedAmount = Amount

                        ' Need to update the who pay flag to customer (CUS) in the claim 
                        Dim whoPayId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), CUSTOMER_PAYS_CODE)
                        If whoPayId.Equals(Guid.Empty) Then
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_WHO_PAY_NOT_FOUND)
                        Else
                            oClaim.WhoPaysId = whoPayId
                        End If

                    ElseIf ClaimStatusCode = ADH_CLAIM_STATUS_CODE Then
                        ' *** Handling change of coverage type to Accidental Damage

                        ' Verify the existing cert item qualify for the change
                        Dim accidentalCoverageTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), ACCIDENTAL_COVERAGE_TYPE_CODE)
                        If oClaim.CoverageTypeId.Equals(accidentalCoverageTypeId) Then
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_COVERAGE_TYPE)
                        End If

                        Dim searchCoverageDV As CertItemCoverage.CertItemCoverageSearchDV = GetcovergeSearchDV(oClaim, ACCIDENTAL_COVERAGE_TYPE_CODE)

                        If searchCoverageDV.Count <> 1 Then
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_COVERAGE_TYPE)
                        Else
                            ' Need to update the cause of loss of the claim to Accidental Damage
                            Dim dvCauseOfLoss As DataView = LookupListNew.GetCauseOfLossByCoverageTypeLookupList(Authentication.LangId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, accidentalCoverageTypeId, , True)
                            Dim causeOfLossId As Guid = LookupListNew.GetIdFromCode(dvCauseOfLoss, ACCIDENTAL_CAUSE_OF_LOSS_CODE)
                            If causeOfLossId.Equals(Guid.Empty) Then
                                Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.GUI_CAUSE_OF_LOSS_IS_REQUIRED)
                            End If

                            oClaim.CertItemCoverageId = New Guid(CType(searchCoverageDV(0)(CertItemCoverageDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
                            oClaim.CalculateFollowUpDate()
                            oClaim.CauseOfLossId = causeOfLossId

                            'Dim oPriceDetail As PriceGroupDetail = oClaim.GetCurrentPriceGroupDetail()
                            Dim priceListdv As DataView
                            Dim equipmentId As Guid
                            Dim equipClassId As Guid
                            Dim equipConditionId As Guid

                            If (LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, oClaim.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
                                equipConditionId = LookupListNew.GetIdFromCode(LookupListCache.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
                                If oClaim.ClaimedEquipment Is Nothing Or oClaim.ClaimedEquipment.EquipmentBO Is Nothing Then
                                    Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
                                    Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
                                End If
                                equipmentId = oClaim.ClaimedEquipment.EquipmentBO.Id
                                equipClassId = oClaim.ClaimedEquipment.EquipmentBO.EquipmentClassId
                            End If

                            'Dim equipConditionId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
                            'If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, oClaim.Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
                            '    equipConditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'

                            '    If Not oClaim.ClaimedEquipment Is Nothing AndAlso Not oClaim.ClaimedEquipment.EquipmentBO Is Nothing Then
                            '        equipmentId = oClaim.ClaimedEquipment.EquipmentBO.Id
                            '        equipClassId = oClaim.ClaimedEquipment.EquipmentBO.EquipmentClassId

                            '    ElseIf Not oClaim.ClaimedEquipment Is Nothing Then
                            '        equipmentId = Equipment.FindEquipment(oClaim.Dealer.Dealer, oClaim.ClaimedEquipment.Manufacturer, oClaim.ClaimedEquipment.Model, Date.Today)
                            '        'equipClassId = Equipment.GetEquipmentClassIdByEquipmentId(equipmentId)
                            '        equipClassId = New Equipment(equipmentId).EquipmentClassId

                            '    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oClaim.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
                            '        Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
                            '        Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
                            '    End If
                            'End If

                            priceListdv = PriceListDetail.GetRepairPrices(oClaim.CompanyId, oClaim.ServiceCenterObject.Code, oClaim.LossDate, oClaim.RiskTypeId,
                                                                 oClaim.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionId,
                                                                 oClaim.Dealer.Id, String.Empty)
                            If priceListdv Is Nothing Then
                                Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.INVALID_PRICE_LIST)
                            End If
                            'If oPriceDetail Is Nothing Then
                            '    Throw New BOValidationException("UpdateClaimData Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PRICE_GROUP)
                            'End If
                            priceListdv.RowFilter = "Service_type_code = '" + Codes.SERVICE_TYPE__CARRY_IN_PRICE + "'"
                            oClaim.AuthorizedAmount = priceListdv(0)("Price")
                            'oClaim.AuthorizedAmount = oPriceDetail.CarryInPrice

                            Dim oCertItemCoverage As New CertItemCoverage(oClaim.CertItemCoverageId)
                            oClaim.Deductible = oCertItemCoverage.Deductible
                        End If

                        ' Need to update the who pay flag to Assurant (AIZ) in the claim 
                        Dim whoPayId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), ASSURANT_PAYS_CODE)
                        If whoPayId.Equals(Guid.Empty) Then
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_WHO_PAY_NOT_FOUND)
                        Else
                            oClaim.WhoPaysId = whoPayId
                        End If
                    ElseIf ClaimStatusCode = MCHB_CLAIM_STATUS_CODE Then
                        ' MCHB Claim Status will trigger the special Service Logic after REQ- 603
                        'Start REQ-603
                        If Not String.IsNullOrEmpty(SpecialServiceCode) Then
                            Dim ChildClaimCreated As Boolean = ProcessSpecialServiceClaim(oClaim)
                            If ChildClaimCreated Then
                                'nothing mroe needs to be done 
                                ' Set the acknoledge OK response
                                Return XMLHelper.GetXML_OK_Response
                            End If
                        Else
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_SPECIAL_SERVICE)
                        End If
                        'End REQ-603 
                    Else
                        ' Amount is ONLY required for COD
                        If Amount IsNot Nothing Then
                            Throw New BOValidationException("UpdateClaimData Error: ", INVALID_AMOUNT_ONLY_REQUIRED_ON_COD)
                        End If
                    End If
                    oClaimStatus = oClaim.AddExtendedClaimStatus(Guid.Empty)
                    oClaimStatus.ClaimId = ClaimID
                    oClaimStatus.ClaimStatusByGroupId = ClaimStatusByGroupID
                    oClaimStatus.StatusDate = DateTime.Now
                    If ExternalUserName IsNot Nothing Then
                        oClaimStatus.ExternalUserName = ExternalUserName
                    End If

                    If StatusComments IsNot Nothing Then
                        oClaimStatus.Comments = DiagnosticFeeComment & StatusComments
                    Else
                        If DiagnosticFeeComment <> "" Then
                            oClaimStatus.Comments = DiagnosticFeeComment
                        End If
                    End If

                    If Not ReasonCloseID.Equals(Guid.Empty) Then
                        ' Close claim if reason close given
                        oClaim.StatusCode = Codes.CLAIM_STATUS__CLOSED
                        oClaim.ReasonClosedId = ReasonCloseID
                        oClaim.ClaimClosedDate = Now
                    ElseIf oClaim.MethodOfRepairCode = SALVAGE_METHOD_OF_REPAIR_CODE AndAlso oClaimStatus.StatusCode = DELIVERED_TO_SERVICE_CENTER_STATUS_CODE Then
                        ' if method of repair is salvage and the service center received the item, then close the claim
                        oClaim.StatusCode = Codes.CLAIM_STATUS__CLOSED
                        oClaim.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), SALVAGE_REASON_CLOSE_CODE)
                        oClaim.ClaimClosedDate = Now
                    End If
                Else
                    Throw New BOValidationException("UpdateClaimData Error: ", INVALID_CLAIM_STATUS_REQUIRED)
                End If

                If Not ServiceCenterID.Equals(Guid.Empty) Then
                    ' Can not update service center if RepairDate exists
                    If oClaim.RepairDate IsNot Nothing Then
                        Throw New BOValidationException("UpdateClaimData Error: ", ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE)
                    Else
                        oClaim.ServiceCenterId = ServiceCenterID
                    End If
                End If

                If ClaimStatusCode = REPAIRED_CLAIM_STATUS_CODE Then
                    oClaim.RepairDate = CType(oClaimStatus.StatusDate.Value, DateType)
                End If

                If ProblemDescription IsNot Nothing Then
                    oClaim.ProblemDescription = ProblemDescription
                End If

                If SpecialInstruction IsNot Nothing Then
                    oClaim.SpecialInstruction = SpecialInstruction
                End If

                If VisitDate IsNot Nothing Then
                    oClaim.VisitDate = VisitDate
                End If

                ' Create new claim comments
                If ClaimComments IsNot Nothing AndAlso ClaimComments <> "" Then
                    Dim blnExceedMaxReplacements As Boolean = False
                    'If replacement, check max replacement allowed per calendar year
                    'REQ-660 Check for both repair and replacement
                    blnExceedMaxReplacements = oClaim.IsMaxReplacementExceeded(oClaim.CertificateId, oClaim.LossDate.Value)

                    'Call the Create Comment Logic
                    Dim c As Comment = oClaim.AddNewComment()
                    c.Comments = ClaimComments

                    'Add comments to indicate that the claim will be closed
                    If blnExceedMaxReplacements Then
                        c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED)
                    End If
                End If

                ' Re-calculate followup date if it is invalid
                If Not oClaim.IsValid Then
                    If Not IsValidFollowupDate(oClaim) Then
                        oClaim.CalculateFollowUpDate()
                    End If
                End If
                oClaim.Save()
            End If

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
            'Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function IsValidFollowupDate(claimBO As Claim) As Boolean
        Dim obj As Claim = claimBO

        If ((obj.FollowupDate Is Nothing) OrElse
            (obj.StatusCode = Codes.CLAIM_STATUS__CLOSED) OrElse
            (obj.StatusCode = Codes.CLAIM_STATUS__PENDING) OrElse
            ((obj.FollowupDate.Value >= obj.GetShortDate(Today)) AndAlso
                (obj.FollowupDate.Value <= NonbusinessCalendar.GetNextBusinessDate(obj.MaxFollowUpDays.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)) AndAlso
                (NonbusinessCalendar.GetSameBusinessDaysCount(obj.FollowupDate.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) <= 0))) Then
            Return True
        End If

        If ((obj.GetShortDate(obj.FollowupDate.Value) = obj.OriginalFollowUpDate) AndAlso
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

    <ValueMandatory("")>
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

    <ValueMandatory("")>
    Public Property CertItemCoverageCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CERT_ITEM_COVERAGE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CERT_ITEM_COVERAGE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CERT_ITEM_COVERAGE_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ClaimStatusCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CLAIM_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CLAIM_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CLAIM_STATUS, Value)
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

    Public Property StatusComments As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_STATUS_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_STATUS_COMMENTS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_STATUS_COMMENTS, Value)
        End Set
    End Property

    Public Property ClaimComments As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CLAIM_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CLAIM_COMMENTS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_CLAIM_COMMENTS, Value)
        End Set
    End Property

    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(SOURCE_COL_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_AMOUNT, Value)
        End Set
    End Property

    Public Property ExternalUserName As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_EXTERNAL_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_EXTERNAL_USER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_EXTERNAL_USER_NAME, Value)
        End Set
    End Property

    Public Property SpecialServiceCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SPECIAL_SERVICE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SPECIAL_SERVICE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SPECIAL_SERVICE_CODE, Value)
        End Set
    End Property

    Public Property SpecialServiceOnlyCode As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SPECIAL_SERVICE_ONLY_CODE) Is DBNull.Value Then
                Return Codes.YESNO_N    'default value is N
            Else
                Return CType(Row(SOURCE_COL_SPECIAL_SERVICE_ONLY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SOURCE_COL_SPECIAL_SERVICE_ONLY_CODE, Value)
        End Set
    End Property
#End Region

#Region "Extended Properties"

    Private ReadOnly Property ClaimID As Guid
        Get
            If _claimId.Equals(Guid.Empty) Then
                _claimId = PickupListHeader.GetClaimIDByCode(ClaimNumber, CertItemCoverageCode)

                If _claimId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
                End If

            End If

            Return _claimId
        End Get
    End Property

    Public ReadOnly Property ClaimStatusByGroupID As Guid
        Get
            If _claimStatusByGroupId.Equals(Guid.Empty) Then
                _claimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(CType(Row(SOURCE_COL_CLAIM_STATUS), String))

                If _claimStatusByGroupId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("UpdateClaimData Error: ", INVALID_CLAIM_STATUS_NOT_FOUND)
                End If
            End If

            Return _claimStatusByGroupId
        End Get
    End Property

    Public ReadOnly Property ServiceCenterID As Guid
        Get
            If _serviceCenterId.Equals(Guid.Empty) AndAlso ServiceCenterCode IsNot Nothing AndAlso ServiceCenterCode <> "" Then

                Dim dvServiceCenter As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)

                If dvServiceCenter IsNot Nothing AndAlso dvServiceCenter.Count > 0 Then
                    _serviceCenterId = LookupListNew.GetIdFromCode(dvServiceCenter, ServiceCenterCode)

                    If _serviceCenterId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("UpdateClaimData Error: ", INVALID_SERVICE_CENTER_CODE)
                    End If
                Else
                    Throw New BOValidationException("UpdateClaimData Error: ", INVALID_SERVICE_CENTER_CODE)
                End If

            End If

            Return _serviceCenterId
        End Get
    End Property

    Public ReadOnly Property ReasonCloseID As Guid
        Get
            If _reasonCloseId.Equals(Guid.Empty) AndAlso ReasonClosedCode IsNot Nothing AndAlso ReasonClosedCode <> "" Then

                Dim dv As DataView = LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                _reasonCloseId = LookupListNew.GetIdFromCode(dv, ReasonClosedCode)

                If (_reasonCloseId.Equals(Guid.Empty)) Then
                    Throw New BOValidationException("UpdateClaimData Error: ", ERR_REASON_CLOSED_CODE_NOT_FOUND)
                End If

            End If

            Return _reasonCloseId
        End Get
    End Property
#End Region

End Class

Imports Assurant.ElitaPlus.DataEntities
Imports System.Linq
Imports Oracle.ManagedDataAccess.Client
Imports System.Data.Entity.Infrastructure

Public Class ClaimContext
    Public Sub New()
        MyBase.New("Claims.ClaimDataModel")
    End Sub

    Private Sub CheckDBConnection()
        If Me.Database.Connection.State = ConnectionState.Closed Then
            Database.Connection.Open()
        End If
    End Sub

    Friend Function GetPriceList(ByVal pCompanyId As Guid,
                                 ByVal pServiceCenterCode As String,
                                 ByVal pLookupDate As Date,
                                 ByVal pSalesPrice As Decimal,
                                 ByVal pRiskTypeId As Guid,
                                 ByVal pEquipmentClassId As Nullable(Of Guid),
                                 ByVal pEquipmentId As Nullable(Of Guid),
                                 ByVal pEquipmentConditionId As Nullable(Of Guid),
                                 ByVal pDealerId As Guid,
                                 ByVal pServiceLevelCode As String,
                                 ByVal pServiceClassCode As String,
                                 ByVal pServiceTypeCode As String,
                                 ByVal pCurrencyCode As String,
                                 ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord)
        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        If (String.IsNullOrEmpty(pServiceLevelCode)) Then
            dbCommand.CommandText = "ELP_PRICE_LIST_UTILITY.Find_Repair_Price"
        Else
            dbCommand.CommandText = "ELP_PRICE_LIST_UTILITY.Find_Repair_Price_By_SVCLvl"
        End If

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCompanyId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_service_center_code", .OracleDbType = OracleDbType.Varchar2, .Size = 16, .Value = pServiceCenterCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_effective_date", .OracleDbType = OracleDbType.Date, .Value = pLookupDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_sales_price", .OracleDbType = OracleDbType.Decimal, .Value = pSalesPrice})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_risk_type_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pRiskTypeId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_equip_class_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentClassId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_equipment_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_condition_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentConditionId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pDealerId})
        If (Not String.IsNullOrEmpty(pServiceLevelCode)) Then
            dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_svc_lvl_code", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceLevelCode})
        End If
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_currency_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCurrencyCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_currency_conversion_date", .OracleDbType = OracleDbType.Date, .Value = pCurrencyConversionDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_svc_code_table", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_return_code", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})
        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)
        Dim dsPriceList As New DataSet
        Try
            dbAdapter.Fill(dsPriceList)
        Catch ex As Exception
            Throw New InvalidOperationException("Exchange Rate not configured")
        End Try


        If (dsPriceList.Tables.Count > 0) Then
            Return (
                        From dr As DataRow In dsPriceList.Tables(0).AsEnumerable
                        Select New PriceListDetailRecord() With
                            {
                                .ServiceClassId = New Guid(DirectCast(dr("service_class_id"), Byte())),
                                .ServiceClassCode = dr("service_class_code"),
                                .ServiceTypeId = New Guid(DirectCast(dr("service_type_id"), Byte())),
                                .ServiceTypeCode = dr("service_type_code"),
                                .ServiceLevelId = If(dr("service_level_id").Equals(DBNull.Value) OrElse dr("service_level_id").Equals(Guid.Empty), Nothing, New Guid(DirectCast(dr("service_level_id"), Byte()))),
                                .ServiceLevelCode = dr("service_level_code").ToString(),
                                .Price = DirectCast(dr("Price"), Decimal),
                                .VendorSku = dr("Vendor_Sku").ToString(),
                                .VendorSkuDescription = dr("Vendor_Sku_Description").ToString(),
                                .PriceListId = New Guid(DirectCast(dr("price_list_id"), Byte())),
                                .IsDeductibleId = New Guid(DirectCast(dr("is_deductible_id"), Byte())),
                                .IsDeductibleCode = dr("is_deductible_code").ToString(),
                                .IsStandardId = New Guid(DirectCast(dr("is_standard_id"), Byte())),
                                .IsStandardCode = dr("is_standard_code").ToString(),
                                .ContainsDeductibleId = New Guid(DirectCast(dr("contains_deductible_id"), Byte())),
                                .ContainsDeductibleCode = dr("contains_deductible_code").ToString(),
                                .Priority = dr("priority").ToString(),
                                .CurrencyId = New Guid(DirectCast(dr("currency_id"), Byte())),
                                .CurrencyCode = dr("currency_code")}).ToList
        End If
        Return Nothing

    End Function

    Friend Function GetPriceListByMakeAndModel(ByVal pForceDate As Date,
                                               ByVal pClaimNumber As String,
                                               ByVal pCompanyCode As String,
                                               ByVal pServiceCenterCode As String,
                                               ByVal pRiskTypeCode As String,
                                               ByVal pEquipmentClassCode As String,
                                               ByVal pEquipmentId As Nullable(Of Guid),
                                               ByVal pEquipmentConditionId As Nullable(Of Guid),
                                               ByVal pDealerCode As String,
                                               ByVal pServiceClassCode As String,
                                               ByVal pServiceTypeCode As String,
                                               ByVal pMake As String,
                                               ByVal pModel As String,
                                               ByVal pLowPrice As Decimal,
                                               ByVal pHighPrice As Decimal,
                                               ByVal pServiceLevelCode As String,
                                               ByVal pCurrencyCode As String,
                                               ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord)
        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "ELP_PRICE_LIST_UTILITY.GetPriceList"

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_force_date", .OracleDbType = OracleDbType.Date, .Value = pForceDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_claim_number", .OracleDbType = OracleDbType.Varchar2, .Value = pClaimNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCompanyCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_service_center_code", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceCenterCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_risk_type_code", .OracleDbType = OracleDbType.Varchar2, .Value = pRiskTypeCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_equip_class_code", .OracleDbType = OracleDbType.Varchar2, .Value = pEquipmentClassCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_equipment_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_condition_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentConditionId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_code", .OracleDbType = OracleDbType.Varchar2, .Value = pDealerCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_svc_cls_code", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceClassCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_svc_typ_code", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceTypeCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_make", .OracleDbType = OracleDbType.Varchar2, .Value = pMake})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_model", .OracleDbType = OracleDbType.Varchar2, .Value = pModel})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_low_price", .OracleDbType = OracleDbType.Decimal, .Value = pLowPrice})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_high_price", .OracleDbType = OracleDbType.Decimal,  .Value = pHighPrice})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_svc_lvl_code", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceLevelCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_currency_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCurrencyCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_currency_conversion_date", .OracleDbType = OracleDbType.Date, .Value = pCurrencyConversionDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_price_table", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_return_code", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})
        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)
        Dim dsPriceList As New DataSet
        Try
            dbAdapter.Fill(dsPriceList)
        Catch ex As Exception
            Throw New InvalidOperationException("Exchange Rate not configured")
        End Try

        If (dsPriceList.Tables.Count > 0) Then
            Return (
                        From dr As DataRow In dsPriceList.Tables(0).AsEnumerable
                        Select New PriceListDetailRecord() With
                            {
                                .ServiceClassId = New Guid(DirectCast(dr("service_class_id"), Byte())),
                                .ServiceClassCode = dr("service_class_code"),
                                .ServiceTypeId = New Guid(DirectCast(dr("service_type_id"), Byte())),
                                .ServiceTypeCode = dr("service_type_code"),
                                .ServiceLevelId = If(dr("service_level_id").Equals(DBNull.Value) OrElse dr("service_level_id").Equals(Guid.Empty), Nothing, New Guid(DirectCast(dr("service_level_id"), Byte()))),
                                .ServiceLevelCode = dr("service_level_code").ToString(),
                                .Price = DirectCast(dr("Price"), Decimal),
                                .Priority = dr("priority").ToString(),
                                .Make = dr("Make").ToString(),
                                .Model = dr("Model").ToString(),
                                .CurrencyCode = dr("currency_code")}).ToList

        End If
        Return Nothing

    End Function

    Friend Function GetNextClaimNumber(ByVal pCompanyId As Guid) As String
        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elp_claims.next_claim_number"
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_company_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCompanyId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_claim_number_input", .OracleDbType = OracleDbType.Varchar2, .Value = Nothing})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_coverage_code_input", .OracleDbType = OracleDbType.Varchar2, .Value = Nothing})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_unit_number_input", .OracleDbType = OracleDbType.Int64, .Value = Nothing})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_claim_number", .OracleDbType = OracleDbType.Varchar2, .Size = 30, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_claim_group_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_return", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_exception_msg", .OracleDbType = OracleDbType.Varchar2, .Size = 20, .Direction = ParameterDirection.Output})

        CheckDBConnection()
        dbCommand.ExecuteNonQuery()

        Return dbCommand.Parameters(4).Value.ToString() '''' CLAIM NUMBER PARAMETER

    End Function

    Friend Function GetProductRemainingLiabilityLimit(ByVal pCertificateId As Guid, ByVal pLossDate As Date) As Nullable(Of Decimal)

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)

        CheckDBConnection()

        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elp_claims.CalcProdRemainLiabilityLimit"

        Dim oLiabLimit As OracleParameter = New OracleParameter("v_RemainLiabilityLimit", OracleDbType.Int32)
        oLiabLimit.Direction = ParameterDirection.ReturnValue
        dbCommand.Parameters.Add(oLiabLimit)

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_cert_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCertificateId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_loss_date", .OracleDbType = OracleDbType.Date, .Value = pLossDate})

        dbCommand.ExecuteNonQuery()
        Database.Connection.Close()

        Try
            Return Convert.ToDecimal(oLiabLimit.Value.ToString())
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Friend Function GetRemainingCoverageLiabilityLimit(ByVal pCertItemCoverageId As Guid, ByVal pLossDate As Date) As Nullable(Of Decimal)

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)

        CheckDBConnection()

        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elp_claims.CalcRemainCovLiabilityLimit"

        Dim oLiabLimit As OracleParameter = New OracleParameter("v_RemainCovLiabilityLimit", OracleDbType.Int32)
        oLiabLimit.Direction = ParameterDirection.ReturnValue
        dbCommand.Parameters.Add(oLiabLimit)

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_cert_item_coverage_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCertItemCoverageId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_loss_date", .OracleDbType = OracleDbType.Date, .Value = pLossDate})

        dbCommand.ExecuteNonQuery()
        Database.Connection.Close()

        Try
            Return Convert.ToDecimal(oLiabLimit.Value.ToString())
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Friend Function IsMaxReplacementsExceeded(ByVal pCertificateId As Guid,
                                              ByVal pMethodOfRepairId As Guid,
                                              ByVal pLossDate As Date,
                                              ByVal pReplacementBasedOn As String,
                                              ByVal pInsuranceActivationDate As Date) As Boolean

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)

        CheckDBConnection()

        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elp_claim_interface_process.CheckforMaximumReplacements"

        Dim oReplExceeded As OracleParameter = New OracleParameter("w_retValue", OracleDbType.Int64)
        oReplExceeded.Direction = ParameterDirection.ReturnValue
        dbCommand.Parameters.Add(oReplExceeded)
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCertificateId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_method_of_repair_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pMethodOfRepairId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_loss_date", .OracleDbType = OracleDbType.Date, .Value = pLossDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_replacement_based_on", .OracleDbType = OracleDbType.Varchar2, .Value = pReplacementBasedOn})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_insurance_activation_date", .OracleDbType = OracleDbType.Date, .Value = pInsuranceActivationDate})

        dbCommand.ExecuteNonQuery()
        Database.Connection.Close()

        Try
            Return If(oReplExceeded.Value.ToString() = "0", False, True)
        Catch ex As Exception
            Return Nothing
        End Try

        Return Nothing
    End Function

    Friend Function IsServiceLevelValid(ByVal pServiceCenterId As Guid,
                                        ByVal pRiskTypeId As Guid,
                                        ByVal pEffectiveDate As Date,
                                        ByVal pSalesPrice As Decimal,
                                        ByVal pServiceLevel As String) As Boolean

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)

        CheckDBConnection()

        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "ELP_PRICE_LIST_UTILITY.ValidateServiceLevel"

        Dim iSvcLevelValid As OracleParameter = New OracleParameter("w_retValue", OracleDbType.Int64)
        iSvcLevelValid.Direction = ParameterDirection.ReturnValue
        dbCommand.Parameters.Add(iSvcLevelValid)
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_servicecenterid", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pServiceCenterId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_risktypeid", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pRiskTypeId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_effectivedate", .OracleDbType = OracleDbType.Date, .Value = pEffectiveDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_salesprice", .OracleDbType = OracleDbType.Decimal, .Value = pSalesPrice})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_servicelevel", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceLevel})

        dbCommand.ExecuteNonQuery()
        Database.Connection.Close()

        Try
            Return If(iSvcLevelValid.Value.ToString() = "0", False, True)
        Catch ex As Exception
            Return Nothing
        End Try

        Return Nothing
    End Function
    Friend Function GetClaimsPaidAmountByCertificate(ByVal pCertificateId As Guid) As Decimal

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)

        CheckDBConnection()

        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "getclaimspaidamountbycert"

        Dim oPaidAmount As OracleParameter = New OracleParameter("w_result", OracleDbType.Int64)
        oPaidAmount.Direction = ParameterDirection.ReturnValue
        dbCommand.Parameters.Add(oPaidAmount)
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCertificateId})

        dbCommand.ExecuteNonQuery()
        Database.Connection.Close()

        Return Convert.ToDecimal(oPaidAmount.Value.ToString())


    End Function

    Friend Function ValidateManufacturerByCompanyGroup(ByVal pManufacturerDesc As String,
                                                       ByVal pCompanyId As Guid,
                                                       ByVal pCertItemCoverageId As Guid
                                                       ) As Guid

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elp_claim_interface_validate.ValidateManufByCompanyGroup"
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_manufacturer_desc", .OracleDbType = OracleDbType.Varchar2, .Value = pManufacturerDesc})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCompanyId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_item_coverage_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCertItemCoverageId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_manufacturer_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = Guid.Empty, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_reject_reason", .OracleDbType = OracleDbType.Varchar2, .Size = 50, .Value = Nothing, .Direction = ParameterDirection.Output})

        CheckDBConnection()
        dbCommand.ExecuteNonQuery()

        If (Not dbCommand.Parameters(3).Value.IsNull AndAlso dbCommand.Parameters(3).Value IsNot Nothing) Then
            Return New Guid(DirectCast(dbCommand.Parameters(3).Value.Value, Byte())) '''' ManufacturerId
        End If
        Return Nothing

    End Function

    Friend Function GetPriceListByMethodOfRepair(ByVal pMethodofRepairId As Guid,
                                 ByVal pCompanyId As Guid,
                                 ByVal pServiceCenterCode As String,
                                 ByVal pLookupDate As Date,
                                 ByVal pSalesPrice As Decimal,
                                 ByVal pRiskTypeId As Guid,
                                 ByVal pEquipmentClassId As Nullable(Of Guid),
                                 ByVal pEquipmentId As Nullable(Of Guid),
                                 ByVal pEquipmentConditionId As Nullable(Of Guid),
                                 ByVal pDealerId As Guid,
                                 ByVal pServiceLevelCode As String,
                                 ByVal pCurrencyCode As String,
                                 ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord)
        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "ELP_PRICE_LIST_UTILITY.GetMethodofRepairPriceList"

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_method_of_repair_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pMethodofRepairId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCompanyId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_service_center_code", .OracleDbType = OracleDbType.Varchar2, .Size = 16, .Value = pServiceCenterCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_effective_date", .OracleDbType = OracleDbType.Date, .Value = pLookupDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_sales_price", .OracleDbType = OracleDbType.Decimal, .Value = pSalesPrice})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_risk_type_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pRiskTypeId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_equip_class_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentClassId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_equipment_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_condition_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pEquipmentConditionId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pDealerId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_svc_lvl_code", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceLevelCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_currency_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCurrencyCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_currency_conversion_date", .OracleDbType = OracleDbType.Date, .Value = pCurrencyConversionDate})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_svc_table_cur", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_return_code", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})
        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)
        Dim dsPriceList As New DataSet
        Try
            dbAdapter.Fill(dsPriceList)
        Catch ex As Exception
            Throw New InvalidOperationException("Exchange Rate not configured")
        End Try


        If (dsPriceList.Tables.Count > 0) Then
            Return (
                        From dr As DataRow In dsPriceList.Tables(0).AsEnumerable
                        Select New PriceListDetailRecord() With
                            {
                                .ServiceClassId = New Guid(DirectCast(dr("service_class_id"), Byte())),
                                .ServiceClassCode = dr("service_class_code"),
                                .ServiceTypeId = New Guid(DirectCast(dr("service_type_id"), Byte())),
                                .ServiceTypeCode = dr("service_type_code"),
                                .ServiceLevelId = If(dr("service_level_id").Equals(DBNull.Value) OrElse dr("service_level_id").Equals(Guid.Empty), Nothing, New Guid(DirectCast(dr("service_level_id"), Byte()))),
                                .ServiceLevelCode = dr("service_level_code").ToString(),
                                .Price = DirectCast(dr("Price"), Decimal),
                                .VendorSku = dr("Vendor_Sku").ToString(),
                                .VendorSkuDescription = dr("Vendor_Sku_Description").ToString(),
                                .PriceListId = New Guid(DirectCast(dr("price_list_id"), Byte())),
                                .IsDeductibleId = New Guid(DirectCast(dr("is_deductible_id"), Byte())),
                                .IsDeductibleCode = dr("is_deductible_code").ToString(),
                                .IsStandardId = New Guid(DirectCast(dr("is_standard_id"), Byte())),
                                .IsStandardCode = dr("is_standard_code").ToString(),
                                .ContainsDeductibleId = New Guid(DirectCast(dr("contains_deductible_id"), Byte())),
                                .ContainsDeductibleCode = dr("contains_deductible_code").ToString(),
                                .Priority = dr("priority").ToString(),
                                .CurrencyId = New Guid(DirectCast(dr("currency_id"), Byte())),
                                .CurrencyCode = dr("currency_code")}).ToList
        End If
        Return Nothing

    End Function


    Friend Function ReturnAdvanceExchange(ByVal pCert As String,
                                          ByVal pDealer As String,
                                          ByVal pCoverageTypeCode As String) As String
        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elp_ws_policyservice.refund_credit"
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_Cert_Number", .OracleDbType = OracleDbType.Varchar2, .Size = 30, .Value = pCert})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_Dealer_Code", .OracleDbType = OracleDbType.Varchar2, .Value = pDealer})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_Coverage_Code", .OracleDbType = OracleDbType.Varchar2, .Value = pCoverageTypeCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "p_Err_Msg", .OracleDbType = OracleDbType.Varchar2, .Size = 50, .Value = Nothing, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "P_ErrMsg_UIProgCode", .OracleDbType = OracleDbType.Varchar2, .Size = 50, .Value = Nothing, .Direction = ParameterDirection.Output})

        CheckDBConnection()
        dbCommand.ExecuteNonQuery()

        Return Nothing

        'Return dbCommand.Parameters(4).Value.ToString()

    End Function

    Friend Function GetClaimCaseReserve(ByVal pClaimId As Guid) As Decimal

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)

        CheckDBConnection()

        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "Case_Reserve"

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "P_CLAIM_ID", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pClaimId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "P_CASE_RESERVE", .OracleDbType = OracleDbType.Int64, .Size = 10, .Direction = ParameterDirection.Output})

        dbCommand.ExecuteNonQuery()
        Database.Connection.Close()

        Try
            Return Convert.ToDecimal(dbCommand.Parameters(1).Value.ToString())
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

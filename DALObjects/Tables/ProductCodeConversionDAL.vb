'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/22/2004)********************

Public Class ProductConversionData

    Public companyIds As ArrayList
    Public dealerId As Guid
    Public externalProductCode As String
    Public productCodeId As Guid
   
End Class


Public Class ProductCodeConversionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_CONVERSION"
    Public Const TABLE_KEY_NAME As String = "product_conversion_id"
    Public Const DSNAME As String = "LIST"

    Public Const COL_NAME_PRODUCT_CONVERSION_ID As String = "product_conversion_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_EXTERNAL_PROD_CODE As String = "external_prod_code"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_CERTIFICATE_DURATION As String = "certificate_duration"
    Public Const COL_NAME_MANUFACTURER_WARRANTY As String = "manufacturer_warranty"
    Public Const COL_NAME_GROSS_AMOUNT As String = "gross_amount"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SALES_PRICE As String = "sales_price"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const PARAM_CRUD_STATUS As String = "po_crud_Status"
    Public Const PARAM_PRODUCT_CONV As String = "po_Product_conv"
    

    Public Const DEALER_ID = 0
    Public Const PRODUCT_CODE_ID = 1
    Public Const EXTERNAL_PROD_CODE_ID = 2
    Public Const TOTAL_PARAM = 2 '3 '4

    Public Const WILDCARD As Char = "%"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_conversion_id", id.ToByteArray)}
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        outputParameter(0) = New DBHelper.DBHelperParameter(PARAM_PRODUCT_CONV, GetType(DataSet))
        
        Try
           DBHelper.FetchSp(selectStmt,parameters, outputParameter,familyDS, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal oProductConversionData As ProductConversionData) As DataSet
        Dim selectStmt As String
        If oProductConversionData.companyIds.Count > 1 Then
            selectStmt = Me.Config("/SQL/LOAD_LIST_MULTIPLE_COMPANIES")
        Else
            selectStmt = Me.Config("/SQL/LOAD_LIST")
        End If

        Dim parameters(TOTAL_PARAM) As OracleParameter

        With oProductConversionData

            'parameters(COMPANY_ID) = New OracleParameter(COL_NAME_COMPANY_ID, .companyId.ToByteArray)
            If .dealerId.Equals(Guid.Empty) Then
                parameters(DEALER_ID) = New OracleParameter(COL_NAME_DEALER_ID, WILDCARD)
            Else
                parameters(DEALER_ID) = New OracleParameter(COL_NAME_DEALER_ID, .dealerId.ToByteArray)
            End If

            If .productCodeId.Equals(Guid.Empty) Then
                parameters(PRODUCT_CODE_ID) = New OracleParameter(COL_NAME_PRODUCT_CODE_ID, WILDCARD)
            Else
                parameters(PRODUCT_CODE_ID) = New OracleParameter(COL_NAME_PRODUCT_CODE_ID, .productCodeId.ToByteArray)
            End If

            '.externalProductCode &= WILDCARD
            Dim extProdCode As String = .externalProductCode
            .externalProductCode = GetFormattedSearchStringForSQL(extProdCode)
            parameters(EXTERNAL_PROD_CODE_ID) = New OracleParameter(COL_NAME_EXTERNAL_PROD_CODE, .externalProductCode)

            Dim inClauseCondition As String = MiscUtil.BuildListForSql("AND d." & Me.COL_NAME_COMPANY_ID, .companyIds, True)
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

        End With

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function  

    Public Function LoadListWithDescByDealer(ByVal DealerID As Guid) As DataSet
        Dim selectStmt As String
        selectStmt = Me.Config("/SQL/LOAD_LIST_WITH_DESC")
        Dim ds As DataSet = New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", DealerID.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, "ProductCode", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckForDealerProdCodeMfgCombination(ByVal DealerID As Guid, ByVal ExtProdCode As String, ByVal Manufacturer As String, _
                                                         ByVal ProductConversionId As Guid, ByVal EffectiveDate as date) As DataSet
        Dim selectStmt As String
        Dim whereClauseConditions As String = ""
       
        selectStmt = Me.Config("/SQL/CHK_DEALER_PRODCODE_MFG_COMBINATION")
        Dim ds As DataSet = New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_dealer_id", DealerID.ToByteArray), 
                                                                                           New DBHelper.DBHelperParameter("pi_product_conversion_id", ProductConversionId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("pi_external_prod_code", ExtProdCode),  
                                                                                           New DBHelper.DBHelperParameter("pi_manufacturer", Manufacturer),
                                                                                           New DBHelper.DBHelperParameter("pi_effective_date", EffectiveDate)
                                                                                          }

        Try
           
            Dim outputParameter(0) As DBHelper.DBHelperParameter
            outputParameter(0) = New DBHelper.DBHelperParameter(PARAM_PRODUCT_CONV, GetType(DataSet))
            DBHelper.FetchSp(selectStmt,parameters, outputParameter,ds, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckOverlappingProductCodeConversion(ByVal DealerID As Guid, ByVal ExtProdCode As String, ByVal Manufacturer As String, _
                                                         ByVal ProductConversionId As Guid, ByVal EffectiveDate as date) As DataSet
        Dim selectStmt As String
       
        selectStmt = Me.Config("/SQL/CHK_OVERLAPPING_PROD_CODE_CONVERSION")
        Dim ds As DataSet = New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { New DBHelper.DBHelperParameter("pi_external_prod_code", ExtProdCode),
                                                                                            New DBHelper.DBHelperParameter("pi_dealer_id", DealerID.ToByteArray), 
                                                                                            New DBHelper.DBHelperParameter("pi_manufacturer", Manufacturer),
                                                                                            New DBHelper.DBHelperParameter("pi_effective_date", EffectiveDate)
                                                                                          }

        Try
            Dim outputParameter(0) As DBHelper.DBHelperParameter
            outputParameter(0) = New DBHelper.DBHelperParameter(PARAM_PRODUCT_CONV, GetType(DataSet))
            DBHelper.FetchSp(selectStmt,parameters, outputParameter,ds, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region


    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
    End Sub

    public Function SaveProductCodeConversion(ByVal row As DataRow) As String
        
        Try
            Dim stmtToExecute As String
            Dim rowState As DataRowState = row.RowState
            Dim updateby As string
               
            Select Case rowState
                    Case DataRowState.Added
                        'Insert
                        stmtToExecute = Me.Config("/SQL/INSERT")
                        updateby= COL_NAME_CREATED_BY
                    Case DataRowState.Deleted
                        'delete
                        stmtToExecute = Me.Config("/SQL/DELETE")
                    Case DataRowState.Modified
                        'update
                        stmtToExecute = Me.Config("/SQL/UPDATE")
                        updateby= COL_NAME_MODIFIED_BY
            End Select

            Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {
                        New DBHelper.DBHelperParameter(Me.PARAM_CRUD_STATUS, GetType(Integer))
                    }
            
           If rowState = DataRowState.Deleted Then
               Dim inParametersdel() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {
                           New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_PRODUCT_CONVERSION_ID.ToLower(),   row(Me.COL_NAME_PRODUCT_CONVERSION_ID,DataRowVersion.Original))
                       }
               DBHelper.ExecuteSp(stmtToExecute, inParametersdel, outputParameters)
               row.AcceptChanges()
           Else 
               Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_DEALER_ID.ToLower(), row(Me.COL_NAME_DEALER_ID)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_EXTERNAL_PROD_CODE.ToLower(), row(Me.COL_NAME_EXTERNAL_PROD_CODE)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_PRODUCT_CODE_ID.ToLower(), row(Me.COL_NAME_PRODUCT_CODE_ID)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_PRODUCT_CONVERSION_ID.ToLower(), row(Me.COL_NAME_PRODUCT_CONVERSION_ID)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_CERTIFICATE_DURATION.ToLower(), row(Me.COL_NAME_CERTIFICATE_DURATION)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_MANUFACTURER_WARRANTY.ToLower(), row(Me.COL_NAME_MANUFACTURER_WARRANTY)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_GROSS_AMOUNT.ToLower(), row(Me.COL_NAME_GROSS_AMOUNT)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_MANUFACTURER.ToLower(), row(Me.COL_NAME_MANUFACTURER)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_MODEL.ToLower(), row(Me.COL_NAME_MODEL)),
                        New DBHelper.DBHelperParameter("pi_"& Me.COL_NAME_SALES_PRICE.ToLower(), row(Me.COL_NAME_SALES_PRICE)),
                        New DBHelper.DBHelperParameter("pi_"& updateby.ToLower(), row(updateby)),
                        New DBHelper.DBHelperParameter("pi_" & Me.COL_NAME_EFFECTIVE_DATE.ToLower(), row(Me.COL_NAME_EFFECTIVE_DATE)),
                        New DBHelper.DBHelperParameter("pi_" & Me.COL_NAME_EXPIRATION_DATE.ToLower(), row(Me.COL_NAME_EXPIRATION_DATE))
                       }
               DBHelper.ExecuteSp(stmtToExecute, inParameters, outputParameters)
               row.AcceptChanges()
           End If
           

            If CType(outputParameters(0).Value, Integer) <> 1 Then
                Dim e As New ApplicationException("Return Value = " & outputParameters(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            Else
                Return outputParameters(0).Value.ToString
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
       
    End Function

End Class



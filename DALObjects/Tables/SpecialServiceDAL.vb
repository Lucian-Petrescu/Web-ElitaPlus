
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/22/2010)********************


Public Class SpecialServiceDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SPECIAL_SERVICE"
    Public Const TABLE_NAME_SERVICE_CLASS_TYPE As String = "ELP_SERVICE_CLASS_TYPE"
    Public Const TABLE_KEY_NAME As String = "special_service_id"

    Public Const COL_NAME_SPECIAL_SERVICE_ID As String = "special_service_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COVERAGE_LOSS_ID As String = "coverage_loss_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_AVAILABLE_FOR_SERV_CENTER_ID As String = "available_for_serv_center_id"
    Public Const COL_NAME_ADD_ITEM_ALLOWED As String = "add_item_allowed"
    Public Const COL_NAME_ADD_ITEM_AFTER_EXPIRED As String = "add_item_after_expired"
    Public Const COL_NAME_PRICE_GROUP_FIELD_ID As String = "price_group_field_id"
    Public Const COL_NAME_ALLOWED_OCCURRENCES_ID As String = "allowed_occurrences_id"
    Public Const COL_NAME_COMBINED_WITH_REPAIR As String = "combined_with_repair"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"

    Public Const WS_COL_NAME_CERTIFICATE_NUMBER As String = "CertificateNumber"
    Public Const WS_COL_NAME_CLAIME_NUMBER As String = "claimNumber"
    Public Const WS_COL_NAME_COVERAGE_TYPE_CODE As String = "CoverageTypecode"

    Public Const DB_COL_NAME_CERTIFICATE_NUMBER As String = "cert_number"
    Public Const DB_COL_NAME_CLAIME_NUMBER As String = "claim_number"
    Public Const DB_COL_NAME_COVERAGE_TYPE_CODE As String = "coverage_type_id"

    Public Const DB_COL_NAME_SERVICE_CLASS_ID As String = "service_class_id"
    Public Const DB_COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"


    Public Const DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER As String = "--dynamic_where_clause"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("special_service_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList(LanguageId As Guid, compIds As ArrayList, _
                             dealerId As Guid, CoverageTypeId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean = False

        inClausecondition &= "And d." & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds, False)

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "d.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If Not CoverageTypeId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "cov_loss.coverage_type_id = " & MiscUtil.GetDbStringFromGuid(CoverageTypeId)
        End If

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray)}


        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Public Function ValidateCoverageLoss(ByVal Dealer_id As Guid, ByVal coverage_loss_id As Guid) As DataSet
    '    Dim ds As New DataSet
    '    Dim parameters() As OracleParameter
    '    Dim selectStmt As String = Me.Config("/SQL/VALIDATE_COVERAGE_LOSS")
    '    Try
    '        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, Dealer_id.ToByteArray), _
    '                            New OracleParameter(CoverageLossDAL.COL_NAME_COVERAGE_LOSS_ID, coverage_loss_id.ToByteArray)}
    '        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function

    Public Function ValidateCoverageLoss(Dealer_id As Guid, coverage_loss_id As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/VALIDATE_COVERAGE_LOSS")
        Try
            parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, Dealer_id.ToByteArray), _
                                New OracleParameter(CoverageLossDAL.COL_NAME_COVERAGE_LOSS_ID, coverage_loss_id.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetPriceGroupsList(language_id As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/PRICE_GROUPS_LIST")
        Try
            parameters = New OracleParameter() {New OracleParameter("language_id", language_id.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAvailSplSvcList(Company_Group_Id As Guid, Coverage_Type_Id As Guid, Dealer_Id As Guid, product_code As String, Optional ByVal LoadNoneActive As Boolean = False) As DataSet
        Dim ds As New DataSet
        Dim sFilterCondition As String '= MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        If LoadNoneActive Then
            sFilterCondition += " and active is null"
        End If

        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_AVAIABLE_SPL_SVC_LIST_FOR_COVERAGE_DEALER_PRODCODE")

        Try
            parameters = New OracleParameter() {New OracleParameter(CoverageLossDAL.COL_NAME_COMPANY_GROUP_ID, Company_Group_Id.ToByteArray), _
                                New OracleParameter(CoverageLossDAL.COL_NAME_COVERAGE_TYPE_ID, Coverage_Type_Id.ToByteArray), _
                                New OracleParameter(COL_NAME_DEALER_ID, Dealer_Id.ToByteArray), _
                                New OracleParameter(ProductCodeDAL.COL_NAME_PRODUCT_CODE, product_code)}

            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim PSplSvcDal As New ProductSpecialServiceDAL
        
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions           
            PSplSvcDal.Update(familyDataset, tr, DataRowState.Deleted)                        
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes            
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            PSplSvcDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            'At the end delete the Address
            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

#End Region

#Region "Method for Web Service call"

    Public Function getSpecialServiceByCertificate(CertificateNumber As String, CoverageTypeId As Guid, languageid As Guid, AvailableForServiceCenter As Guid, CompanyGroupId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/WS_GETSPLSVCBYCERT")
        Try
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID.ToLower, languageid.ToByteArray), _
                                                New DBHelper.DBHelperParameter(DB_COL_NAME_CERTIFICATE_NUMBER, CertificateNumber.ToString), _
                                                New DBHelper.DBHelperParameter(COL_NAME_AVAILABLE_FOR_SERV_CENTER_ID, AvailableForServiceCenter.ToByteArray), _
                                                New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}

            If Not CoverageTypeId.Equals(Guid.Empty) Then
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, " AND ECL.COVERAGE_TYPE_ID =" & MiscUtil.GetDbStringFromGuid(CoverageTypeId))
            Else
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function getspecialServicebyClaimNumber(ClaimNumber As String, CoverageTypeId As Guid, languageid As Guid, AvailableForServiceCenter As Guid, CompanyGroupId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/WS_GETSPLSVCBYCLAIM")
        Try
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID.ToLower, languageid.ToByteArray), _
                                                New DBHelper.DBHelperParameter(DB_COL_NAME_CLAIME_NUMBER, ClaimNumber.ToString), _
                                                New DBHelper.DBHelperParameter(COL_NAME_AVAILABLE_FOR_SERV_CENTER_ID, AvailableForServiceCenter.ToByteArray), _
                                                New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
            If Not CoverageTypeId.Equals(Guid.Empty) Then
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, " AND ECL.COVERAGE_TYPE_ID =" & MiscUtil.GetDbStringFromGuid(CoverageTypeId))
            Else
                selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function getServiceTypebyServiceClass(ServiceClassId As Guid, language_id As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/GET_SERVICE_TYPE_FROM_SERVICE_CLASS")
        Try
            'parameters = New OracleParameter() {New OracleParameter(DB_COL_NAME_SERVICE_CLASS_ID, OracleDbType.Raw, 16)}
            'parameters(0).Value = ServiceClassId.ToByteArray

            Dim parameters = New OracleParameter() {New OracleParameter(DB_COL_NAME_SERVICE_CLASS_ID.ToLower, ServiceClassId.ToByteArray), _
                                                   New OracleParameter("language_id", language_id.ToByteArray)}


            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class



'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/16/2005)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class CertEndorseDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_ENDORSE"
    Public Const TABLE_KEY_NAME As String = "cert_endorse_id"
    Public Const TABLE_CUSTOMER As String = "GET_CUSTOMER_DETAILS"

    Public Const COL_NAME_CERT_ENDORSE_ID As String = "cert_endorse_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
    Public Const COL_NAME_ENDORSEMENT_NUMBER As String = "endorsement_number"
    Public Const COL_NAME_CUST_NAME_PRE As String = "cust_name_pre"
    Public Const COL_NAME_CUST_NAME_POST As String = "cust_name_post"
    Public Const COL_NAME_HOME_PHONE_PRE As String = "home_phone_pre"
    Public Const COL_NAME_HOME_PHONE_POST As String = "home_phone_post"
    Public Const COL_NAME_CUST_FIRST_NAME As String = "cust_first_name"
    Public Const COL_NAME_CUST_MIDDLE_NAME As String = "cust_middle_name"
    Public Const COL_NAME_CUST_LAST_NAME As String = "cust_last_name"
    Public Const COL_NAME_WORK_PHONE_PRE As String = "work_phone_pre"
    Public Const COL_NAME_WORK_PHONE_POST As String = "work_phone_post"
    Public Const COL_NAME_EMAIL_PRE As String = "email_pre"
    Public Const COL_NAME_EMAIL_POST As String = "email_post"

    Public Const COL_NAME_PRODUCT_SALES_DATE_PRE As String = "product_sales_date_pre"
    Public Const COL_NAME_PRODUCT_SALES_DATE_POST As String = "product_sales_date_post"
    Public Const COL_NAME_WARRANTY_SALES_DATE_PRE As String = "warranty_sales_date_pre"
    Public Const COL_NAME_WARRANTY_SALES_DATE_POST As String = "warranty_sales_date_post"
    Public Const COL_NAME_SALES_PRICE_PRE As String = "sales_price_pre"
    Public Const COL_NAME_SALES_PRICE_POST As String = "sales_price_post"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_ADDRESS_ID_PRE As String = "address_id_pre"
    Public Const COL_NAME_ADDRESS_ID_POST As String = "address_id_post"
    Public Const COL_NAME_LANGUAGE_ID_PRE As String = "language_id_pre"
    Public Const COL_NAME_LANGUAGE_ID_POST As String = "language_id_post"
    Public Const COL_NAME_DOCUMENT_TYPE_ID_PRE As String = "document_type_id_pre"
    Public Const COL_NAME_DOCUMENT_TYPE_ID_POST As String = "document_type_id_post"
    Public Const COL_NAME_ID_TYPE_PRE As String = "id_type_pre"
    Public Const COL_NAME_ID_TYPE_POST As String = "id_type_post"
    Public Const COL_NAME_DOCUMENT_AGENCY_PRE As String = "document_agency_pre"
    Public Const COL_NAME_DOCUMENT_AGENCY_POST As String = "document_agency_post"
    Public Const COL_NAME_IDENTIFICATION_NUMBER_PRE As String = "identification_number_pre"
    Public Const COL_NAME_IDENTIFICATION_NUMBER_POST As String = "identification_number_post"
    Public Const COL_NAME_RG_NUMBER_PRE As String = "rg_number_pre"
    Public Const COL_NAME_RG_NUMBER_POST As String = "rg_number_post"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE_PRE As String = "document_issue_date_pre"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE_POST As String = "document_issue_date_post"

    Public Const COL_NAME_CERT_ITEM_COVERAGE_ID As String = "cert_item_coverage_id"
    Public Const COL_NAME_BEGIN_DATE As String = "begin_date"
    Public Const COL_NAME_END_DATE As String = "end_date"
    Public Const COL_ENDORSEMENT_REASON As String = "endorsement_reason"
    Public Const COL_ENDORSEMENT_TYPE As String = "endorsement_type"
    Public Const COL_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_LANGUAGE_ID As String = "language_id"

    Public Const PI_TABLE_NAME As String = "pi_table_name"
    Public Const PI_REFERENCE_ID As String = "pi_reference_id"
    Public Const PI_UI_PROG_CODE As String = "pi_ui_prog_code"
    Public Const PI_LOOKUP_DATE As String = "pi_lookup_date"
    Public Const CUSTOMER_ID As String = "pi_customer_id"
    Public Const CUSTOMER_FIRST_NAME As String = "pi_customer_first_name"
    Public Const CUSTOMER_MIDDLE_NAME As String = "pi_customer_middle_name"
    Public Const CUSTOMER_LAST_NAME As String = "pi_customer_last_name"
    Public Const po_reject_code As String = "po_reject_code"
    Public Const po_reject_reason As String = "po_reject_reason"

    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const PAR_NAME_CLAIM_COUNT As String = "p_claim_count"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_endorse_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function
    Public Function LoadList(ByVal certid As Guid, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter

        parameters = New OracleParameter(){New OracleParameter(COL_LANGUAGE_ID, languageId.ToByteArray), _
                                           New OracleParameter(COL_NAME_CERT_ID, certid.ToByteArray)}

        Try
            ' Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListForEndorse(ByVal endorseId As Guid, Optional ByVal familyDataset As DataSet = Nothing) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CERT_ENDORSMENT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("endorse_id", endorseId)}
        Try
            Dim ds = familyDataset
            If ds Is Nothing Then
                ds = New DataSet
            End If
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ValidateCertSalesPrice(ByVal CertId As Guid, ByVal NewSalesPrice As Decimal, ByVal CovTypeId As Guid, ByVal CertDur As Integer, ByVal CovDur As Integer) As Integer
        Dim inputParameters(4) As DBHelperParameter
        Dim outputParameters(0) As DBHelperParameter
        Dim RetVal As Integer

        Dim selectStmt As String = Me.Config("/SQL/VALIDATE_CERT_SALES_PRICE")
        Try
            inputParameters(0) = New DBHelper.DBHelperParameter("p_cert_id", CertId.ToByteArray)
            inputParameters(1) = New DBHelper.DBHelperParameter("p_new_sales_price", NewSalesPrice)
            inputParameters(2) = New DBHelper.DBHelperParameter("p_coverage_type_id", CovTypeId.ToByteArray)
            inputParameters(3) = New DBHelper.DBHelperParameter("p_cert_duration", CertDur)
            inputParameters(4) = New DBHelper.DBHelperParameter("p_cov_duration", CovDur)

            outputParameters(0) = New DBHelper.DBHelperParameter("p_return", GetType(Integer))

            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            Select Case CType(outputParameters(0).Value, Integer)
                Case 0, 100, 200
                Case Else
                    Dim e As New ApplicationException("Return Value = " & outputParameters(0).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End Select

            RetVal = CType(outputParameters(0).Value, Integer)

            Return RetVal
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function CalculateLiabilityLimitUsingCovTemplate(ByVal CertId As Guid, ByVal CoverageTypeId As Guid, ByVal NewSalesPrice As Decimal) As ArrayList
        Dim inputParameters(2) As DBHelperParameter
        Dim outputParameters(1) As DBHelperParameter

        Dim selectStmt As String = Me.Config("/SQL/CAL_LIABILITY_LIMIT_USING_COVERAGE_TEMPLATE")
        Try
            Dim al As New ArrayList
            inputParameters(0) = New DBHelper.DBHelperParameter("p_cert_id", CertId.ToByteArray)
            inputParameters(1) = New DBHelper.DBHelperParameter("p_coverage_type_id", CoverageTypeId.ToByteArray)
            inputParameters(2) = New DBHelper.DBHelperParameter("p_new_sales_price", NewSalesPrice)

            outputParameters(0) = New DBHelper.DBHelperParameter("p_new_liability_limit", GetType(Decimal))
            outputParameters(1) = New DBHelper.DBHelperParameter("p_return", GetType(Integer))

            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            Select Case CType(outputParameters(1).Value, Integer)
                Case 0, 100, 200
                Case Else
                    Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End Select

            al.Add(CType(outputParameters(0).Value, Decimal))
            al.Add(CType(outputParameters(1).Value, Integer))

            Return al

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsLowestCovStrtDtEqual2PrdSalesDt(ByVal certId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_COVERAGE_LOWEST_START_DATE")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CERT_ID, certId.ToByteArray)}
        Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
    End Function

    Public Function LoadListByCovIdClaimLossDate(ByVal certItemCoverageId As Guid, ByVal begin_date As Date, ByVal end_date As Date) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ACTIVE_CLAIMS_BY_COVERAGEID_CLAIMLOSSDATE")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray),
                                            New OracleParameter(COL_NAME_BEGIN_DATE, begin_date),
                                            New OracleParameter(COL_NAME_END_DATE, end_date)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function ClaimCountForParentAndChildCert(ByVal cert_Id As Guid) As Integer
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_CLAIMS_PARENT_CHILD_CERTS")
        Dim inputParameters = New DBHelperParameter() {New DBHelperParameter(Me.COL_NAME_CERT_ID, cert_Id.ToByteArray)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_COUNT, GetType(Integer)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 100)}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
        If CType(outputParameters(1).Value, Integer) <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameters(2).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
        Else
            Return CType(outputParameters(0).Value, Integer)
        End If

    End Function
#End Region

#Region "Overloaded Methods"

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim certItemDAL As New CertItemDAL
        Dim itemcoverageDA As New CertItemCoverageDAL
        Dim endorsecovDAL As New CertEndorseCovDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'updates additions and changes
            certItemDAL.UpdateFamily(familyDataset, tr)

            ' if endorsement flag on dealer is on then dont insert endorsement as that is done by trigger
            ' US 258702 Ind. Policy

            'Dim dealerId As Guid = New Guid(CType(familyDataset.Tables("ELP_CERT").Rows(0)("Dealer_Id"), Byte()))

            'If IsDealerEndorsementAttributeFlagOn(dealerId) <> "Y" Then
            Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            endorsecovDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            ' End If
            itemcoverageDA.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

#End Region

    Public Sub SaveCustomerData(ByVal custid As Guid, ByVal custfirstname As String, ByVal custmidname As String, ByVal custlastname As String, ByVal po_code As String, ByVal po_reason As String)
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_CUST_DETAILS")
        Dim inParameters(3) As DBHelper.DBHelperParameter

        inParameters(0) = New DBHelper.DBHelperParameter(Me.CUSTOMER_ID, custid)
        inParameters(1) = New DBHelper.DBHelperParameter(Me.CUSTOMER_FIRST_NAME, custfirstname)
        inParameters(2) = New DBHelper.DBHelperParameter(Me.CUSTOMER_MIDDLE_NAME, custmidname)
        inParameters(3) = New DBHelper.DBHelperParameter(Me.CUSTOMER_LAST_NAME, custlastname)


        Dim outParameters(1) As DBHelper.DBHelperParameter
        outParameters(0) = New DBHelper.DBHelperParameter(Me.po_reject_code, GetType(String))
        outParameters(1) = New DBHelper.DBHelperParameter(Me.po_reject_reason, GetType(String))

        Dim ds As New DataSet
        Dim tbl As String = Me.TABLE_CUSTOMER

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)

        If outParameters(1).Value > 0 Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        End If

    End Sub

    Public Function IsDealerEndorsementAttributeFlagOn(dealerId As Guid)

        Dim endorsementFlag As String

        Dim selectStmt As String = Me.Config("/SQL/GET_Dealer_Attribute_Value")

        Using command As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure)

            command.BindByName = True
            command.AddParameter(PI_TABLE_NAME, OracleDbType.Varchar2, 25, "ELP_DEALER", ParameterDirection.Input)
            command.AddParameter(PI_REFERENCE_ID, OracleDbType.Raw, 16, dealerId, ParameterDirection.Input)
            command.AddParameter(PI_UI_PROG_CODE, OracleDbType.Varchar2, 30, "GEN_ENDORSEMENT", ParameterDirection.Input)
            command.AddParameter(PI_LOOKUP_DATE, OracleDbType.Date, 12, DateTime.Now.Date, ParameterDirection.Input)

            command.AddParameter("pReturnValue", OracleDbType.Char, 1, Nothing, ParameterDirection.ReturnValue)

            Try
                OracleDbHelper.ExecuteNonQuery(command)
                endorsementFlag = command.Parameters.Item("pReturnValue").Value.ToString()

                Return endorsementFlag

            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Using

    End Function
End Class

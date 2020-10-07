'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/9/2015)********************


Public Class AfaAcctAmtSrcMappingDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_ACCT_AMT_SRC_MAPPING"
    Public Const TABLE_KEY_NAME As String = "afa_acct_amt_src_mapping_id"

    Public Const COL_NAME_AFA_ACCT_AMT_SRC_MAPPING_ID As String = "afa_acct_amt_src_mapping_id"
    Public Const COL_NAME_ACCT_AMT_SRC_ID As String = "acct_amt_src_id"
    Public Const COL_NAME_OPERATION As String = "operation"
    Public Const COL_NAME_INV_RATE_BUCKET_ID As String = "inv_rate_bucket_id"
    Public Const COL_NAME_AFA_PRODUCT_ID As String = "afa_product_id"
    Public Const COL_NAME_LOSS_TYPE As String = "loss_type"
    Public Const COL_NAME_COUNTFIELDTOUSE As String = "count_field_to_use"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_acct_amt_src_mapping_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadFormularByProdLossType(AcctAmtSrcID As Guid, languageID As Guid, _
                             prodID As Guid, LossType As String, CntToUse As String, _
                             Optional ByVal blnGetMatchedOnly As Boolean = False) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_FORMULAR_BY_RPDO_LOSSTYPE")
        Dim para() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray), _
                                                                                     New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray), _
                                                                                     New DBHelper.DBHelperParameter("language_id", languageID.ToByteArray), _
                                                                                     New DBHelper.DBHelperParameter("acct_amt_src_id", AcctAmtSrcID.ToByteArray)}
        Dim ds As New DataSet
        Dim inClauseCondition As String = String.Empty
        If prodID <> Guid.Empty Then
            inClauseCondition += " and p.AFA_PRODUCT_ID = " & MiscUtil.GetDbStringFromGuid(prodID, True)
        Else
            If blnGetMatchedOnly Then
                inClauseCondition += " and p.AFA_PRODUCT_ID is null"
            End If
        End If

        If LossType <> String.Empty Then
            inClauseCondition += " and m.Loss_type = '" & LossType & "'"
        Else
            If blnGetMatchedOnly Then
                inClauseCondition += " and m.Loss_type is null "
            End If
        End If

        If CntToUse <> String.Empty Then
            inClauseCondition += " and m.COUNT_FIELD_TO_USE = '" & CntToUse & "'"
        End If



        Try
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, para)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Function LoadList(AcctAmtSrcID As Guid, prodID As Guid, LossType As String, CntToUse As String, Optional ByVal blnGetMatchedOnly As Boolean = False) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim para() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_amt_src_id", AcctAmtSrcID.ToByteArray)}
        Dim ds As New DataSet
        Dim inClauseCondition As String
        If prodID <> Guid.Empty Then
            inClauseCondition += " and AFA_PRODUCT_ID = " & MiscUtil.GetDbStringFromGuid(prodID, True)
        Else
            If blnGetMatchedOnly Then
                inClauseCondition += " and AFA_PRODUCT_ID is null"
            End If
        End If
        If LossType <> String.Empty Then
            inClauseCondition += " and Loss_type = '" & LossType & "'"
        Else
            If blnGetMatchedOnly Then
                inClauseCondition += " and Loss_type is null "
            End If
        End If

        If CntToUse <> String.Empty Then
            inClauseCondition += " and COUNT_FIELD_TO_USE = '" & CntToUse & "'"
        End If

        Try
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, para)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function


    Public Function LoadProductByDealer(dealerId As Guid, productCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PRODUCT_LIST_BY_DEALER")
        Dim whereClauseConditions As String = "", strTemp As String

        If FormatSearchMask(productCode) Then
            whereClauseConditions &= Environment.NewLine & " and UPPER(p.code) " & productCode.ToUpper.Trim
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class

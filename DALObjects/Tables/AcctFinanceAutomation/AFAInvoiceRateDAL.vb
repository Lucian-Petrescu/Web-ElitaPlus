'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/20/2015)********************


Public Class AFAInvoiceRateDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_INVOICE_RATE"
    Public Const TABLE_KEY_NAME As String = "afa_invoice_rate_id"

    Public Const COL_NAME_AFA_INVOICE_RATE_ID As String = "afa_invoice_rate_id"
    Public Const COL_NAME_AFA_PRODUCT_ID As String = "afa_product_id"
    Public Const COL_NAME_INSURANCE_CODE As String = "insurance_code"
    Public Const COL_NAME_TIER As String = "tier"
    Public Const COL_NAME_REGULATORY_STATE As String = "regulatory_state"
    Public Const COL_NAME_LOSS_TYPE As String = "loss_type"
    Public Const COL_NAME_RETAIL_AMT As String = "retail_amt"
    Public Const COL_NAME_PREMIUM_AMT As String = "premium_amt"
    Public Const COL_NAME_COMM_AMT As String = "comm_amt"
    Public Const COL_NAME_ADMIN_AMT As String = "admin_amt"
    Public Const COL_NAME_ANCILLARY_AMT As String = "ancillary_amt"
    Public Const COL_NAME_OTHER_AMT As String = "other_amt"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

    Public Const COL_MIN_EFECTIVE As String = "min_efective"
    Public Const COL_MAX_EXPIRATION As String = "max_expiration"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_invoice_rate_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function


    Public Function LoadList(afaProductId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/INV_LOAD_LIST")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_product_id", afaProductId.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

    Public Function IsRateDefinitionUnique(afaProductId As Guid, LossType As String, InsuranceCode As String,
                                           RegulatoryState As String, Tier As String, Effective As String,
                                           Expiration As String, afaInvoiceRateId As Guid) As Boolean

        Dim selectStmt As String = Config("/SQL/GET_DUPLIC_RATE_DEFINITN")
        Dim whereClauseConditions As String = ""

        If Not Effective = String.Empty Then
            whereClauseConditions &= Environment.NewLine & " and trunc(inv.effective_date) = to_date('" & Effective & "','mm/dd/yyyy') "
        End If

        If Not Expiration = String.Empty Then
            whereClauseConditions &= Environment.NewLine & " and trunc(inv.expiration_date) = to_date('" & Expiration & "','mm/dd/yyyy') "
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)


        If InsuranceCode Is Nothing Then
            InsuranceCode = String.Empty
        End If
        If Tier Is Nothing Then
            Tier = String.Empty
        End If

        ' Because this RegulatoryState?.ToUpper is not supported by HPFortify
        Dim strState = String.Empty
        If Not String.IsNullOrEmpty(RegulatoryState) Then
            strState = RegulatoryState.ToUpper
        End If


        Dim parameters() As DBHelper.DBHelperParameter =
            New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_AFA_PRODUCT_ID, afaProductId.ToByteArray),
                                              New DBHelper.DBHelperParameter(COL_NAME_LOSS_TYPE, LossType.ToUpper),
                                              New DBHelper.DBHelperParameter(COL_NAME_TIER, Tier.ToUpper),
                                              New DBHelper.DBHelperParameter(COL_NAME_REGULATORY_STATE, strState),
                                              New DBHelper.DBHelperParameter(COL_NAME_AFA_INVOICE_RATE_ID, afaInvoiceRateId.ToByteArray),
                                              New DBHelper.DBHelperParameter(COL_NAME_AFA_PRODUCT_ID, afaProductId.ToByteArray),
                                              New DBHelper.DBHelperParameter(COL_NAME_LOSS_TYPE, LossType.ToUpper),
                                              New DBHelper.DBHelperParameter(COL_NAME_INSURANCE_CODE, InsuranceCode.ToUpper),
                                              New DBHelper.DBHelperParameter(COL_NAME_REGULATORY_STATE, strState),
                                              New DBHelper.DBHelperParameter(COL_NAME_AFA_INVOICE_RATE_ID, afaInvoiceRateId.ToByteArray)}

        Dim count As Integer
        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Function LoadMinEffectiveMaxExpiration(afaProductId As Guid, InsuranceCode As String, LossType As String, Tier As String, RegulatoryState As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_MIN_EFFECTIVE_MAX_EXPIRATION")

        If InsuranceCode Is Nothing Then
            InsuranceCode = String.Empty
        End If
        If Tier Is Nothing Then
            Tier = String.Empty
        End If
        If String.IsNullOrEmpty(RegulatoryState) Then
            RegulatoryState = String.Empty
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_AFA_PRODUCT_ID, afaProductId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_LOSS_TYPE, LossType.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_TIER, Tier.Trim.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_REGULATORY_STATE, RegulatoryState.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_AFA_PRODUCT_ID, afaProductId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_LOSS_TYPE, LossType.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_TIER, Tier.Trim.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_INSURANCE_CODE, InsuranceCode.Trim.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_REGULATORY_STATE, RegulatoryState.ToUpper)
                                                                                           }
        Try
            Dim ds As New DataSet
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Function LoadRatesWithSameDefinition(afaProductId As Guid, afaInvoiceRateId As Guid, InsuranceCode As String, LossType As String, Tier As String, RegulatoryState As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_RATES_WITH_SAME_DEFINITION")

        If InsuranceCode Is Nothing Then
            InsuranceCode = String.Empty
        End If
        If String.IsNullOrEmpty(RegulatoryState) Then
            RegulatoryState = String.Empty
        End If
        If Tier Is Nothing Then
            Tier = String.Empty
        End If
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_AFA_PRODUCT_ID, afaProductId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_AFA_INVOICE_RATE_ID, afaInvoiceRateId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_LOSS_TYPE, LossType.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_TIER, Tier.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_REGULATORY_STATE, RegulatoryState.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_AFA_PRODUCT_ID, afaProductId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_AFA_INVOICE_RATE_ID, afaInvoiceRateId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_LOSS_TYPE, LossType.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_TIER, Tier.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_INSURANCE_CODE, InsuranceCode.ToUpper),
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_REGULATORY_STATE, RegulatoryState.ToUpper)
                                                                                           }
        Try
            Dim ds As New DataSet
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

End Class
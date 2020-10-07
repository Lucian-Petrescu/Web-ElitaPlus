'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/21/2013)********************
Imports System.Reflection

Public Class ClaimPaymentGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PAYMENT_GROUP"
    Public Const TABLE_KEY_NAME As String = "payment_group_id"

    Public Const COL_NAME_PAYMENT_GROUP_ID As String = "payment_group_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_PAYMENT_GROUP_NUMBER As String = "payment_group_number"
    Public Const COL_NAME_PAYMENT_GROUP_STATUS_ID As String = "payment_group_status_id"
    Public Const COL_NAME_PAYMENT_GROUP_STATUS As String = "payment_group_status"
    Public Const COL_NAME_PAYMENT_GROUP_DATE As String = "payment_group_date"
    Public Const COL_NAME_PAYMENT_GROUP_TOTAL As String = "payment_group_total"

    Public Const PAR_NAME_COMPANY As String = "p_company_id"
    Public Const PAR_NAME_PYMNT_GRP_NUMBER As String = "par_pymnt_grp_number"
    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("payment_group_id", id.ToByteArray)}
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

    Public Function GetPaymentGroup(svcCenterId As Guid, _
                                    PymntGrpNumber As String, _
                                    PymntGrpStatusId As Guid, _
                                    PaymentGroupDateRange As SearchCriteriaStructType(Of Date), _
                                    InvoiceNumber As String, _
                                    InvoiceGrpNumber As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_PAYMENT_GROUP")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_NUMBER_OF_ROWS)}

        Dim innerWhereClauseSubSelect As String = "AND payment_group_id in (select payment_group_id from elp_payment_group_detail pgd" & _
                                                " inner join elp_invoice_extended_mv  mv on  pgd.CLAIM_AUTHORIZATION_ID = mv.CLAIM_AUTHORIZATION_ID"
        Dim InnerWhereClauseConditions As String = String.Empty
        Dim OuterWhereClauseConditions As String = String.Empty

        If FormatSearchMask(PymntGrpNumber) Then
            OuterWhereClauseConditions &= " AND " & " pymntGrp." & COL_NAME_PAYMENT_GROUP_NUMBER & " " & PymntGrpNumber
        End If

        If Not ((PymntGrpStatusId = Guid.Empty) OrElse (PymntGrpStatusId = Nothing)) Then
            OuterWhereClauseConditions &= Environment.NewLine & " AND pymntGrp.PAYMENT_GROUP_STATUS_ID = hextoraw(" & MiscUtil.GetDbStringFromGuid(PymntGrpStatusId) & ")"
        End If

        OuterWhereClauseConditions &= PaymentGroupDateRange.ToSqlString("pymntGrp", COL_NAME_PAYMENT_GROUP_DATE, parameters)

        If FormatSearchMask(InvoiceNumber) Then
            InnerWhereClauseConditions &= " AND " & Environment.NewLine & " mv.invoice_number" & InvoiceNumber
        End If

        If FormatSearchMask(InvoiceGrpNumber) Then
            InnerWhereClauseConditions &= " AND " & Environment.NewLine & _
                                    " mv.INVOICE_RECONCILIATION_ID IN" & "(" & _
                                    "select invgd.INVOICE_RECONCILIATION_ID from elp_invoice_group_detail invgd inner join elp_invoice_group ingrp" & _
                                    " on invgd.INVOICE_GROUP_ID = ingrp.INVOICE_GROUP_ID" & " where ingrp.invoice_group_number" & InvoiceGrpNumber & ")"
        End If

        If Not ((svcCenterId = Guid.Empty) OrElse (svcCenterId = Nothing)) Then
            InnerWhereClauseConditions &= Environment.NewLine & " AND mv.SERVICE_CENTER_ID = hextoraw(" & MiscUtil.GetDbStringFromGuid(svcCenterId) & ")"
        End If


        If InnerWhereClauseConditions.Length > 0 Then
            selectStmt = selectStmt.Replace("--dynamic_inner_where_clause", innerWhereClauseSubSelect & InnerWhereClauseConditions & ")")
        End If
        If OuterWhereClauseConditions.Length > 0 Then
            selectStmt = selectStmt.Replace("--dynamic_outer_where_clause", OuterWhereClauseConditions)
        End If

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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, companyId As Guid, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim pymntGrpDetail As New ClaimPaymentGroupDetailDAL
        Dim invRecons As New InvoiceReconciliationDAL
        Dim claimAuth As New ClaimAuthorizationDAL
        Dim claimInv As New ClaimInvoiceDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If

        Try
            'First Pass updates Deletions
            pymntGrpDetail.Update(familyDataset, tr, DataRowState.Deleted) 'Delete Payment Group Detail from the Payment Group
            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'For a new Payment Group, generate a new Payment Group Number
            ObtainAndAssignPymntGrpNumber(familyDataset, companyId)

            'Second Pass updates additions and changes
            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            pymntGrpDetail.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            'Note : We do not Update Claim Invoice in the Update Family but we control the Claim Invoice Save in the same Transaction

            ' Update Invoice Recons and Claim Auth Updates
            invRecons.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            claimAuth.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
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

#Region "Properties"

    Public ReadOnly Property IsNew(Row As DataRow) As Boolean
        Get
            Return (Row.RowState = DataRowState.Added)
        End Get
    End Property

    Public Property PaymentGroupNumber(Row As DataRow) As String
        Get
            If Row(COL_NAME_PAYMENT_GROUP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_NAME_PAYMENT_GROUP_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            SetValue(Row, COL_NAME_PAYMENT_GROUP_NUMBER, Value)
        End Set
    End Property

#End Region

#Region "Private Methods"

    Private Sub ObtainAndAssignPymntGrpNumber(familyDataset As DataSet, companyId As Guid)
        Dim oRow As DataRow

        For Each oRow In familyDataset.Tables(TABLE_NAME).Rows
            If IsNew(oRow) Then
                PaymentGroupNumber(oRow) = GetPaymentGroupNumber(companyId)
            End If
        Next
    End Sub

    Private Function GetPaymentGroupNumber(companyId As Guid) As String
        Dim selectStmt As String = Config("/SQL/GET_NEXT_PYMNT_GRP_NUMBER_SP")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(PAR_NAME_COMPANY, companyId.ToByteArray)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter(PAR_NAME_PYMNT_GRP_NUMBER, GetType(String)), _
                            New DBHelper.DBHelperParameter(PAR_NAME_RETURN, GetType(Integer)), _
                            New DBHelper.DBHelperParameter(PAR_NAME_EXCEPTION_MSG, GetType(String), 50)}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
        If CType(outputParameters(2).Value, Integer) <> 0 Then
            If CType(outputParameters(2).Value, Integer) = 300 Then
                Throw New StoredProcedureGeneratedException("Payment Group DAL Insert Generated Error: ", outputParameters(PAR_NAME_EXCEPTION_MSG).Value)
            Else
                Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End If
        Else
            Return CType(outputParameters(0).Value, String)
        End If
    End Function

    Protected Sub SetValue(Row As DataRow, columnName As String, newValue As Object)
        If Not newValue Is Nothing And Row(columnName) Is DBNull.Value Then
            'new value is something and old value is DBNULL
            If newValue.GetType Is GetType(BooleanType) Then
                '- BooleanType, special case - convert to string Y or N
                If CType(newValue, BooleanType).Value Then
                    Row(columnName) = "Y"
                Else
                    Row(columnName) = "N"
                End If
            ElseIf newValue.GetType Is GetType(Guid) Then
                'ElseIf newValue.GetType Is GetType(Guid) Then
                If Not newValue.Equals(Guid.Empty) Then
                    Row(columnName) = CType(newValue, Guid).ToByteArray
                End If
            ElseIf newValue.GetType Is GetType(Byte()) Then
                Row(columnName) = CType(newValue, Byte())
            ElseIf newValue.GetType Is GetType(DateType) Then
                Row(columnName) = CType(newValue.ToString, DateTime)
            ElseIf newValue.GetType Is GetType(Double) Then
                Row(columnName) = CType(newValue, Double)
            ElseIf newValue.GetType Is GetType(Decimal) Then
                Row(columnName) = CType(newValue, Decimal)
            Else
                '- DateType, DecimalType, etc... all our other custome types
                '- see if 'newValue Type' has a Value property (only our custom types do)
                Dim propInfo As PropertyInfo = newValue.GetType.GetProperty("Value")
                If Not (propInfo Is Nothing) Then
                    '- call the Value property to extract the native .NET type (double, decimal, etc...)
                    newValue = propInfo.GetValue(newValue, Nothing)
                End If

                '- let the DataColumn convert the value to its internal data type
                Row(columnName) = newValue
            End If
        ElseIf Not newValue Is Nothing Then
            'new value is something and old value is also something
            '- convert current value to a string
            Dim currentValue As Object = Row(columnName)
            If newValue.GetType Is GetType(Guid) Then
                currentValue = New Guid(CType(currentValue, Byte()))
            ElseIf newValue.GetType Is GetType(Byte()) Then
                currentValue = CType(currentValue, Byte())
            Else
                currentValue = currentValue.ToString
                '- create an array of types containing one type, the String type
                Dim types() As Type = {GetType(String)}
                '- see if the 'newValue Type' has a 'Parse(String)' method taking a String parameter
                Dim miMethodInfo As MethodInfo = newValue.GetType.GetMethod("Parse", types)
                If Not miMethodInfo Is Nothing Then
                    '- it does have a Parse method, newValue must be a number type.
                    '- extract the current value as a string
                    Dim args() As Object = {Row(columnName).ToString}
                    '- call the Parse method to convert the currentValue string to a number
                    currentValue = miMethodInfo.Invoke(newValue, args)
                End If
            End If
            '- only dirty the BO if new value is different from old value
            If Not newValue.Equals(currentValue) Then
                If newValue.GetType Is GetType(BooleanType) Then
                    '- BooleanType, special case - convert to string Y or N
                    If CType(newValue, BooleanType).Value Then
                        newValue = "Y"
                    Else
                        newValue = "N"
                    End If
                ElseIf newValue.GetType Is GetType(Byte()) Then
                    newValue = CType(newValue, Byte())
                Else
                    '- DateType, DecimalType, etc... all our other custome types
                    '- see if 'newValue Type' has a Value property (only our custom types do)
                    Dim propInfo As PropertyInfo = newValue.GetType.GetProperty("Value")
                    If Not (propInfo Is Nothing) Then
                        '- call the Value property to extract the native .NET type (double, decimal, etc...)
                        newValue = propInfo.GetValue(newValue, Nothing)
                    End If
                End If
                '- at this point, newValue has a native .NET type
                If newValue.GetType Is GetType(Guid) Then
                    If newValue.Equals(Guid.Empty) Then
                        newValue = DBNull.Value
                    Else
                        newValue = CType(newValue, Guid).ToByteArray
                    End If
                End If
                Row(columnName) = newValue
            End If
        ElseIf newValue Is Nothing And Not Row(columnName) Is DBNull.Value Then
            Row(columnName) = DBNull.Value
        End If
    End Sub

#End Region


End Class
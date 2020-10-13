'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/29/2004)********************


Public Class ServiceOrderDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_ORDER"
    Public Const TABLE_KEY_NAME As String = "service_order_id"

    Public Const COL_NAME_SERVICE_ORDER_ID As String = "service_order_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_SERVICE_ORDER_IMAGE As String = "service_order_image"

    '8/22/2006 - ALR - Added for the modification of storage of service order images
    Public Const COL_NAME_SERVICE_ORDER_IMAGE_ID As String = "service_order_image_id"
    Public Const COL_NAME_SERVICE_ORDER_IMAGE_DATA As String = "service_order_image_data"

    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"

    Public Const PAR_NAME_CLAIM_ID As String = "p_claim_id"
    Public Const PAR_NAME_CLAIM_AUTHORIZATION_ID As String = "p_claim_authorization_id"
    Public Const PAR_NAME_SERVICE_ORDER_ID As String = "p_service_order_id"
    Public Const PAR_NAME_SERVICE_ORDER_IMAGE_DATA As String = "p_service_order_image_data"


    Public Const P_RETURN As Integer = 0
    Public Const P_EXCEPTION_MSG As Integer = 1

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_order_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadLatest(familyDS As DataSet, claimID As Guid)
        Dim selectStmt As String = Config("/SQL/LOADLATEST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimID.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    '08/22/2006 - ALR - Created to load the image field for the service order image table if the 
    '                   record contains the PDF file.
    Public Function LoadImage(id As Guid) As Byte()
        Dim selectStmt As String = Config("/SQL/LOADIMAGE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_order_image_id", id.ToByteArray)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return ds.Tables(TABLE_NAME).Rows(0)(COL_NAME_SERVICE_ORDER_IMAGE)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function GetLatestID(claimID As Guid, Optional claimAuthID As Guid = Nothing) As Guid

        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        If Not (claimAuthID.Equals(Guid.Empty)) Then
            selectStmt = Config("/SQL/LOADLATESTMULTI")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimID.ToByteArray),
                                                           New DBHelper.DBHelperParameter("claim_authorization_id", claimAuthID.ToByteArray)}
        Else
            selectStmt = Config("/SQL/LOADLATEST")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", claimID.ToByteArray)}

        End If
        Dim ds As DataSet = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return New Guid(CType(ds.Tables(TABLE_NAME).Rows(0)(COL_NAME_SERVICE_ORDER_ID), Byte()))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.UpdateWithParam(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            'to be used by maintain invoice use case
            If familyDataset.Tables(ClaimStatusDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(ClaimStatusDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oClaimStatusDAL As New ClaimStatusDAL
                oClaimStatusDAL.Update(familyDataset.Tables(ClaimStatusDAL.TABLE_NAME), tr, DataRowState.Deleted)
            End If
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            If familyDataset.Tables(ClaimStatusDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(ClaimStatusDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oClaimStatusDAL As New ClaimStatusDAL
                oClaimStatusDAL.Update(familyDataset.Tables(ClaimStatusDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If
            'DEF-1555
            'Me.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            InsertCustom(familyDataset.Tables(TABLE_NAME).Rows(0), tr)

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

    Public Sub InsertCustom(dr As DataRow, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim conn As OracleConnection = DBHelper.GetConnection
        Dim tr As IDbTransaction = DBHelper.GetNewTransaction(conn)
        Dim selectStmt As String = Config("/SQL/INSERT_CUSTOM")

        Try

            Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                    New DBHelper.DBHelperParameter(PAR_NAME_CLAIM_ID, CType(dr(COL_NAME_CLAIM_ID), Byte())),
                    New DBHelper.DBHelperParameter(PAR_NAME_CLAIM_AUTHORIZATION_ID, IIf(IsDBNull(dr(COL_NAME_CLAIM_AUTHORIZATION_ID)), "", dr(COL_NAME_CLAIM_AUTHORIZATION_ID))),
                    New DBHelper.DBHelperParameter(PAR_NAME_SERVICE_ORDER_ID, CType(dr(COL_NAME_SERVICE_ORDER_ID), Byte())),
                    New DBHelper.DBHelperParameter(PAR_NAME_SERVICE_ORDER_IMAGE_DATA, dr(COL_NAME_SERVICE_ORDER_IMAGE_DATA), GetType(System.Text.StringBuilder))}
            Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                New DBHelper.DBHelperParameter(PAR_NAME_RETURN, GetType(Integer)), _
                                New DBHelper.DBHelperParameter(PAR_NAME_EXCEPTION_MSG, GetType(String))}

            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters, tr)

            If outputParameters(P_RETURN).Value <> 0 Then Throw New StoredProcedureGeneratedException("Error: ", outputParameters(P_EXCEPTION_MSG).Value)

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

#Region "Public Methods"
    Public Function GetSericeOrderEmailContent(company_id As Guid) As String
        Dim selectStmt As String = Config("/SQL/LOADEMAILCONTENT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("REFERENCE_ID", company_id.ToByteArray)}
        Dim ds As DataSet = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return CType(ds.Tables(TABLE_NAME).Rows(0)(PAR_NAME_RETURN), String)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

End Class



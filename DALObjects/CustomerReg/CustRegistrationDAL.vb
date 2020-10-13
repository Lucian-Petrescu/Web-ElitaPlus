'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/3/2013)********************


Public Class CustRegistrationDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_CUSTOMER_REGISTRATION"
	Public Const TABLE_KEY_NAME As String = "registration_id"
	
	Public Const COL_NAME_REGISTRATION_ID As String = "registration_id"
	Public Const COL_NAME_TAX_ID As String = "tax_id"
	Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CONTACT_INFO_ID As String = "contact_info_id"

    'Columns in ELP_CONTACT_INFO
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_ADDRESS_TYPE_ID As String = "address_type_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("registration_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList(emailId As String, addressTypeId As Guid, dealerId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EMAIL, emailId), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_ADDRESS_TYPE_ID, addressTypeId.ToByteArray), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetRegistration(emailId As String, addressTypeId As Guid, dealerId As Guid) As Guid
       Dim selectStmt As String = Config("/SQL/GET_REGISTRATION_FROM_EMAIL")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EMAIL, emailId), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_ADDRESS_TYPE_ID, addressTypeId.ToByteArray), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}
        Try
            Dim obj As Object
            obj = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (obj IsNot Nothing) Then
                Return New Guid(CType(obj, Byte()))
            End If

            Return Guid.Empty

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

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim contactInfoDAL As New ContactInfoDAL
        Dim addressDAL As New AddressDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            addressDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            contactInfoDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

            'At the end delete the Address
            addressDAL.Update(familyDataset, tr, DataRowState.Deleted)
            contactInfoDAL.Update(familyDataset, tr, DataRowState.Deleted)

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

#Region "Functions"
    Public Function CheckEmail(emailId As String, addressTypeId As Guid, dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CHECK_EMAIL")

        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EMAIL, emailId), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_ADDRESS_TYPE_ID, addressTypeId.ToByteArray), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

End Class



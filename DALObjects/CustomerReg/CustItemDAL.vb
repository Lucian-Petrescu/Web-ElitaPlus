Imports Assurant.ElitaPlus.DALObjects.DBHelper

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/3/2013)********************


Public Class CustItemDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_CUSTOMER_REGISTRATION_ITEM"
	Public Const TABLE_KEY_NAME As String = "registration_item_id"
	
	Public Const COL_NAME_REGISTRATION_ITEM_ID As String = "registration_item_id"
	Public Const COL_NAME_REGISTRATION_ID As String = "registration_id"
	Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
	Public Const COL_NAME_REGISTRATION_DATE As String = "registration_date"
    Public Const COL_NAME_MAKE As String = "make"
    Public Const COL_NAME_MODEL As String = "model"
	Public Const COL_NAME_ITEM_NAME As String = "item_name"
	Public Const COL_NAME_REGISTRATION_STATUS_ID As String = "registration_status_id"
	Public Const COL_NAME_COVERAGE As String = "coverage"
	Public Const COL_NAME_IMEI_NUMBER As String = "imei_number"
    Public Const COL_NAME_PRODUCT_KEY As String = "product_key"
    Public Const COL_NAME_PRODUCT_PROCUREMENT_DATE As String = "product_procurement_date"
    Public Const COL_NAME_ORDER_REF_NUM As String = "order_ref_num"
    Public Const COL_NAME_IS_DELETED_ID As String = "is_deleted_id"
    Public Const COL_NAME_CELL_PHONE As String = "cell_phone"

    Public Const PAR_NAME_CERT_ID As String = "p_certId"
    Public Const PAR_NAME_REGISTRATION_ID As String = "p_registration_id"
    Public Const PAR_NAME_REGISTRATION_ITEM_ID As String = "p_registration_item_id"

    Public Const PAR_NAME_IMEI_NUMBER As String = "p_imei_number"
    Public Const PAR_NAME_IMEI_RETURN As String = "p_return"

    'Column in ELP_CUSTOMER_REGISTRATION
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_TAX_ID As String = "tax_id"

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

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("registration_item_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function    
    

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
		If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Functions"
    Public Function GetItemFromEmail(ByVal emailId As String, ByVal addressTypeId As Guid, ByVal dealerId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_ITEM_FROM_EMAIL")

        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EMAIL, emailId), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_ADDRESS_TYPE_ID, addressTypeId.ToByteArray), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetItemforDealerAndRegistration(ByVal imeiNumber As String, ByVal dealerId As Guid, ByVal registrationId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_ITEM_FOR_DEALER_AND_REGISTRATION")

        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_IMEI_NUMBER, imeiNumber), _
                                                                                                   New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                                                                                   New DBHelper.DBHelperParameter(COL_NAME_REGISTRATION_ID, registrationId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetItemByRegistrationAndIMEI(ByVal registrationId As Guid, ByVal imeiNumber As String) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_ITEM_BY_REG_AND_IMEI")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_REGISTRATION_ID, registrationId.ToByteArray), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_IMEI_NUMBER, imeiNumber)}
        Try
            Dim obj As Object
            obj = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (Not obj Is Nothing) Then
                Return New Guid(CType(obj, Byte()))
            End If

            Return Guid.Empty

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckItemforDealer(ByVal imeiNumber As String, ByVal dealerId As Guid) As Integer

        Dim selectStmt As String = Me.Config("/SQL/CHECK_ITEM_FOR_DEALER")

        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_IMEI_NUMBER, imeiNumber), _
                                                                                                    New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

            Dim obj As Object
            obj = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (Not obj Is Nothing) Then
                Return CType(obj, Integer)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCertItemIDforTaxImei(ByVal taxID As String, ByVal imeiNumber As String, ByVal dealerId As Guid, ByVal cellNumber As String) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_ITEM_ID_FOR_IMEI_AND_TAX_ID")

        Dim whereClauseConditions As String = ""

        If Not cellNumber Is Nothing Then
            If cellNumber.Trim <> String.Empty Then
                whereClauseConditions &= " AND c.work_phone = '" & cellNumber.Trim & "'"
            End If
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_TAX_ID, taxID), _
                                                                                               New DBHelper.DBHelperParameter(COL_NAME_IMEI_NUMBER, imeiNumber), _
                                                                                                New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

            Dim obj As Object
            obj = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (Not obj Is Nothing) Then
                Return New Guid(CType(obj, Byte()))
            End If

            Return Guid.Empty

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
#Region "Public Methods"
    Public Structure RegistrationDetails
        Public RegistrationId As String
        Public RegistrationItemId As String
    End Structure


    Public Function FindRegistration(ByVal certificateId As Guid) As RegistrationDetails
        Dim returnValue As RegistrationDetails

        returnValue.RegistrationId = String.Empty
        returnValue.RegistrationItemId = String.Empty

        Dim selectStmt As String = Me.Config("/SQL/FIND_REGISTRATION")
        Dim inputParameters(0) As DBHelperParameter
        Dim outputParameter(1) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter(Me.PAR_NAME_CERT_ID, certificateId.ToByteArray)

        outputParameter(0) = New DBHelperParameter(Me.PAR_NAME_REGISTRATION_ID, GetType(String), 32)
        outputParameter(1) = New DBHelperParameter(Me.PAR_NAME_REGISTRATION_ITEM_ID, GetType(String), 32)

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0) Is Nothing) Then
                returnValue.RegistrationId = outputParameter(0).Value
            End If
            If (Not outputParameter(1) Is Nothing) Then
                returnValue.RegistrationItemId = outputParameter(1).Value
            End If
            Return returnValue
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ValidIMEI(ByVal imeiNumber As String) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/VALIDATE_IMEI")
        Dim inputParameters(0) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        Dim iRet As Integer

        inputParameters(0) = New DBHelperParameter(PAR_NAME_IMEI_NUMBER, imeiNumber)
        outputParameter(0) = New DBHelperParameter(PAR_NAME_IMEI_RETURN, GetType(Integer))

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            If (Not outputParameter(0) Is Nothing) Then
                iRet = outputParameter(0).Value
            End If

            If iRet = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

End Class



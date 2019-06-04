'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/5/2017)********************


Public Class CertRegisteredItemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_REGISTERED_ITEM"
	Public Const TABLE_KEY_NAME As String = "cert_registered_item_id"
	
	Public Const COL_NAME_CERT_REGISTERED_ITEM_ID As String = "cert_registered_item_id"
	Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
	Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
	Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
	Public Const COL_NAME_MODEL As String = "model"
	Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
	Public Const COL_NAME_ITEM_DESCRIPTION As String = "item_description"
	Public Const COL_NAME_PURCHASED_DATE As String = "purchased_date"
	Public Const COL_NAME_PURCHASE_PRICE As String = "purchase_price"
	Public Const COL_NAME_ENROLLMENT_ITEM As String = "enrollment_item"
	Public Const COL_NAME_ITEM_STATUS As String = "item_status"
	Public Const COL_NAME_VALIDATED_BY As String = "validated_by"
	Public Const COL_NAME_VALIDATED_DATE As String = "validated_date"
	Public Const COL_NAME_PROD_ITEM_MANUF_EQUIP_ID As String = "prod_item_manuf_equip_id"
    Public Const COL_NAME_DEVICE_TYPE_ID As String = "device_type_id"
    Public Const COL_NAME_DEVICE_TYPE As String = "device_type"
    Public Const COL_NAME_REGISTERED_ITEM_NAME As String = "registered_item_name"
    Public Const COL_NAME_REGISTERED_ITEM_IDENTIFIER As String = "registered_item_identifier"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"

    'REQ-6002
    Public Const COL_NAME_REGISTRATION_DATE As String = "registration_date"
    Public Const COL_NAME_RETAIL_PRICE As String = "retail_price"
    Public Const COL_NAME_INDIXID As String = "indixid"

    'User Story 188650
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

    Public Const PAR_I_CERT_NUMBER As String = "pi_cert_number"
    Public Const PAR_I_DEALER_CODE As String = "pi_dealer"
    Public Const PAR_I_REGISTERED_ITEM_IDENTIFIER As String = "pi_registered_item_identifier"
    Public Const PAR_I_MANUFACTURER As String = "pi_manufacturer"
    Public Const PAR_I_MODEL As String = "pi_model"
    Public Const PAR_I_DEVICE_TYPE As String = "pi_device_type"
    Public Const PAR_I_SERIAL_NUMBER As String = "pi_serial_number"
    Public Const PAR_I_ITEM_DESCRIPTION As String = "pi_item_description"
    Public Const PAR_I_PURCHASED_DATE As String = "pi_purchased_date"
    Public Const PAR_I_PURCHASE_PRICE As String = "pi_purchase_price"
    Public Const PAR_I_REGISTERED_ITEM_NAME As String = "pi_registered_item_name"
    Public Const PAR_I_ENROLLMENT_ITEM As String = "pi_enrollment_item"
    Public Const PAR_I_MODIFIED_BY As String = "pi_modified_by"
    Public Const PAR_I_CREATED_BY As String = "pi_created_by"
    Public Const PAR_I_COMMIT As String = "pi_commit"
    Public Const PAR_I_ITEM_STATUS As String = "pi_item_status"

    'REQ-6002
    Public Const PAR_I_REGISTRATION_DATE As String = "pi_registration_date"
    Public Const PAR_I_RETAIL_PRICE As String = "pi_retail_price"
    Public Const PAR_I_INDIXID As String = "pi_indixID"
    Public Const PAR_I_REGISTEREDITEMNAMETOREPLACE As String = "pi_RegisteredItemNameToReplace"
    Public Const PAR_I_OVERRIDE_UPDATE_NAME_ONLY As String = "pi_override_update_name_only"
    Public Const PAR_I_ID As String = "pi_id"

    Public Const PAR_O_CERT_REGISTERED_ITEM_ID As String = "po_cert_registered_item_id"
    Public Const PAR_O_REJECT_CODE As String = "po_reject_code"
    Public Const PAR_O_REJECT_REASON As String = "po_reject_reason"
    Public Const PAR_O_REJECT_UI_PROG_CODE As String = "po_reject_ui_prog_code"
    Public Const PAR_O_REJECT_MSG_PARMS As String = "po_reject_msg_parms"



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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_registered_item_id", id.ToByteArray)}
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
    Public Function getDeviceTypeDesc(ByVal languageId As Guid, ByVal deviceTypeId As Guid)

        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_DEVICE_TYPE_DESC")

        parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("device_type_id", deviceTypeId.ToByteArray),
                                                       New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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
    Public Function UpdateRegisterItem(ByVal certNumber As String, ByVal dealerCode As String, ByVal regItemIdentifier As String, ByVal manufacturer As String,
                                  ByVal model As String, ByVal serialNumber As String, ByVal itemDesc As String,
                                  ByVal purchaseDate As Date, ByVal purchasePrice As Double, ByVal registeredItemName As String,
                                  ByVal user As String, ByVal itemStatus As String,
                                  ByVal retailPrice As DecimalType, ByVal registrationDate As DateType, ByVal indixID As String,
                                  ByVal deviceType As String, ByVal Id As Guid,
                                  ByRef CertRegItemID As Guid,
                                  ByRef ErrRejectCode As String, ByRef ErrRejectReason As String,
                                  ByRef ErrMsgUIProgCode As String, ByRef ErrMsgParam As String)
        Dim inputParameters(19) As DBHelper.DBHelperParameter
        Dim outputParameter(4) As DBHelper.DBHelperParameter
        Dim sqlStmt As String

        sqlStmt = Me.Config("/SQL/UPDATE_REG_ITEM")

        Try
            inputParameters(0) = New DBHelper.DBHelperParameter(PAR_I_CERT_NUMBER, certNumber, GetType(String))
            inputParameters(1) = New DBHelper.DBHelperParameter(PAR_I_DEALER_CODE, dealerCode, GetType(String))
            inputParameters(2) = New DBHelper.DBHelperParameter(PAR_I_REGISTERED_ITEM_IDENTIFIER, regItemIdentifier, GetType(String))
            inputParameters(3) = New DBHelper.DBHelperParameter(PAR_I_MANUFACTURER, manufacturer, GetType(String))
            inputParameters(4) = New DBHelper.DBHelperParameter(PAR_I_MODEL, model, GetType(String))
            inputParameters(5) = New DBHelper.DBHelperParameter(PAR_I_SERIAL_NUMBER, serialNumber, GetType(String))
            inputParameters(6) = New DBHelper.DBHelperParameter(PAR_I_ITEM_DESCRIPTION, itemDesc, GetType(String))
            inputParameters(7) = New DBHelper.DBHelperParameter(PAR_I_PURCHASED_DATE, purchaseDate, GetType(Date))
            inputParameters(8) = New DBHelper.DBHelperParameter(PAR_I_PURCHASE_PRICE, purchasePrice, GetType(Decimal))
            inputParameters(9) = New DBHelper.DBHelperParameter(PAR_I_REGISTERED_ITEM_NAME, registeredItemName, GetType(String))
            inputParameters(10) = New DBHelper.DBHelperParameter(PAR_I_MODIFIED_BY, user, GetType(String))
            inputParameters(11) = New DBHelper.DBHelperParameter(PAR_I_COMMIT, "Y", GetType(String))
            inputParameters(12) = New DBHelper.DBHelperParameter(PAR_I_ITEM_STATUS, itemStatus, GetType(String))

            'REQ-6002
            inputParameters(13) = New DBHelper.DBHelperParameter(PAR_I_REGISTRATION_DATE, DBHelper.GetValueOrDBNull(registrationDate), GetType(Date))
            inputParameters(14) = New DBHelper.DBHelperParameter(PAR_I_RETAIL_PRICE, DBHelper.GetValueOrDBNull(retailPrice), GetType(Decimal))
            inputParameters(15) = New DBHelper.DBHelperParameter(PAR_I_INDIXID, indixID, GetType(String))
            inputParameters(16) = New DBHelper.DBHelperParameter(PAR_I_DEVICE_TYPE, deviceType, GetType(String))
            inputParameters(17) = New DBHelper.DBHelperParameter(PAR_I_REGISTEREDITEMNAMETOREPLACE, Nothing, GetType(String))
            inputParameters(18) = New DBHelper.DBHelperParameter(PAR_I_OVERRIDE_UPDATE_NAME_ONLY, "Y", GetType(String))
            inputParameters(19) = New DBHelper.DBHelperParameter(PAR_I_ID, Id.ToByteArray)

            outputParameter(0) = New DBHelper.DBHelperParameter(PAR_O_CERT_REGISTERED_ITEM_ID, CertRegItemID.ToByteArray.GetType, 16)
            outputParameter(1) = New DBHelper.DBHelperParameter(PAR_O_REJECT_CODE, GetType(String), 12)
            outputParameter(2) = New DBHelper.DBHelperParameter(PAR_O_REJECT_REASON, GetType(String), 255)
            outputParameter(3) = New DBHelper.DBHelperParameter(PAR_O_REJECT_UI_PROG_CODE, GetType(String), 100)
            outputParameter(4) = New DBHelper.DBHelperParameter(PAR_O_REJECT_MSG_PARMS, GetType(String), 250)

            DBHelper.ExecuteSp(sqlStmt, inputParameters, outputParameter)

            If Not outputParameter(1).Value Is Nothing Then

                ErrRejectCode = outputParameter(1).Value.ToString().Trim
                ErrRejectReason = outputParameter(2).Value.ToString().Trim
                If Not outputParameter(3).Value Is Nothing Then
                    ErrMsgUIProgCode = outputParameter(3).Value.ToString().Trim
                End If
                If Not outputParameter(4).Value Is Nothing Then
                    ErrMsgParam = outputParameter(4).Value.ToString().Trim
                End If

            End If

            If ErrRejectCode = Nothing AndAlso (Not outputParameter(0).Value Is Nothing) Then
                CertRegItemID = CType(outputParameter(0).Value, Guid)
            End If

            If Trim(ErrRejectCode) <> String.Empty Then 'Error exists
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function RegisterItem(ByVal certNumber As String, ByVal dealerCode As String, ByVal manufacturer As String,
                                  ByVal model As String, ByVal deviceType As String, ByVal serialNumber As String, ByVal itemDesc As String,
                                  ByVal purchaseDate As Date, ByVal purchasePrice As Double, ByVal registeredItemName As String,
                                  ByVal registrationDate As DateType, ByVal retailPrice As DecimalType, ByVal indixID As String,
                                  ByVal user As String, ByRef CertRegItemID As Guid,
                                  ByRef ErrRejectCode As String, ByRef ErrRejectReason As String,
                                  ByRef ErrMsgUIProgCode As String, ByRef ErrMsgParam As String)

        Dim inputParameters(15) As DBHelper.DBHelperParameter
        Dim outputParameter(4) As DBHelper.DBHelperParameter
        Dim sqlStmt As String

        sqlStmt = Me.Config("/SQL/REG_ITEM")

        Try
            inputParameters(0) = New DBHelper.DBHelperParameter(PAR_I_CERT_NUMBER, certNumber, GetType(String))
            inputParameters(1) = New DBHelper.DBHelperParameter(PAR_I_DEALER_CODE, dealerCode, GetType(String))
            inputParameters(2) = New DBHelper.DBHelperParameter(PAR_I_MANUFACTURER, manufacturer, GetType(String))
            inputParameters(3) = New DBHelper.DBHelperParameter(PAR_I_MODEL, model, GetType(String))
            inputParameters(4) = New DBHelper.DBHelperParameter(PAR_I_DEVICE_TYPE, deviceType, GetType(String))
            inputParameters(5) = New DBHelper.DBHelperParameter(PAR_I_SERIAL_NUMBER, serialNumber, GetType(String))
            inputParameters(6) = New DBHelper.DBHelperParameter(PAR_I_ITEM_DESCRIPTION, itemDesc, GetType(String))
            inputParameters(7) = New DBHelper.DBHelperParameter(PAR_I_PURCHASED_DATE, purchaseDate, GetType(Date))
            inputParameters(8) = New DBHelper.DBHelperParameter(PAR_I_PURCHASE_PRICE, purchasePrice, GetType(Decimal))
            inputParameters(9) = New DBHelper.DBHelperParameter(PAR_I_REGISTERED_ITEM_NAME, registeredItemName, GetType(String))
            inputParameters(10) = New DBHelper.DBHelperParameter(PAR_I_ENROLLMENT_ITEM, "N", GetType(String))
            inputParameters(11) = New DBHelper.DBHelperParameter(PAR_I_CREATED_BY, user, GetType(String))
            inputParameters(12) = New DBHelper.DBHelperParameter(PAR_I_COMMIT, "Y", GetType(String))

            'REQ-6002
            inputParameters(13) = New DBHelper.DBHelperParameter(PAR_I_REGISTRATION_DATE, DBHelper.GetValueOrDBNull(registrationDate), GetType(Date))
            inputParameters(14) = New DBHelper.DBHelperParameter(PAR_I_RETAIL_PRICE, DBHelper.GetValueOrDBNull(retailPrice), GetType(Decimal))
            inputParameters(15) = New DBHelper.DBHelperParameter(PAR_I_INDIXID, indixID, GetType(String))

            outputParameter(0) = New DBHelper.DBHelperParameter(PAR_O_CERT_REGISTERED_ITEM_ID, CertRegItemID.ToByteArray.GetType, 16)
            outputParameter(1) = New DBHelper.DBHelperParameter(PAR_O_REJECT_CODE, GetType(String), 12)
            outputParameter(2) = New DBHelper.DBHelperParameter(PAR_O_REJECT_REASON, GetType(String), 255)
            outputParameter(3) = New DBHelper.DBHelperParameter(PAR_O_REJECT_UI_PROG_CODE, GetType(String), 100)
            outputParameter(4) = New DBHelper.DBHelperParameter(PAR_O_REJECT_MSG_PARMS, GetType(String), 250)

            DBHelper.ExecuteSp(sqlStmt, inputParameters, outputParameter)

            If Not outputParameter(1).Value Is Nothing Then

                ErrRejectCode = outputParameter(1).Value.ToString().Trim
                ErrRejectReason = outputParameter(2).Value.ToString().Trim
                If Not outputParameter(3).Value Is Nothing Then
                    ErrMsgUIProgCode = outputParameter(3).Value.ToString().Trim
                End If
                If Not outputParameter(4).Value Is Nothing Then
                    ErrMsgParam = outputParameter(4).Value.ToString().Trim
                End If

            End If

            If ErrRejectCode = Nothing AndAlso (Not outputParameter(0).Value Is Nothing) Then
                CertRegItemID = CType(outputParameter(0).Value, Guid)
            End If

            If Trim(ErrRejectCode) <> String.Empty Then 'Error exists
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCertRegisterItemId(ByVal ClaimID As Guid) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_REG_ITEM_BY_CLAIM_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", ClaimID.ToByteArray)}
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

    Public Function GetCertRegisterItemIdByMasterClaimNo(ByVal ClaimNumber As String, ByVal CompanyId As Guid) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_REG_ITEM_BY_MASTER_CLAIM_NO")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_number", ClaimNumber),
                                                                                           New DBHelper.DBHelperParameter("company_id", CompanyId.ToByteArray)}
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
#End Region
End Class



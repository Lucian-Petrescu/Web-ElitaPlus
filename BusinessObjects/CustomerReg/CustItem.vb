'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/3/2013)  ********************

Public Class CustItem
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const NO_RECORDS_FOUND = "NO RECORDS FOUND."
    Public Const OK_RESPONSE = "OK"
    Public Const REGN_DAYS = 14

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub
    
    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()             
        Try
            Dim dal As New CustItemDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New CustItemDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(CustItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CustItemDAL.COL_NAME_REGISTRATION_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RegistrationId As Guid
        Get
            CheckDeleted()
            If row(CustItemDAL.COL_NAME_REGISTRATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CustItemDAL.COL_NAME_REGISTRATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_REGISTRATION_ID, Value)
        End Set
    End Property



    Public Property CertItemId As Guid
        Get
            CheckDeleted()
            If row(CustItemDAL.COL_NAME_CERT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CustItemDAL.COL_NAME_CERT_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_CERT_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RegistrationDate As DateType
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_REGISTRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CustItemDAL.COL_NAME_REGISTRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_REGISTRATION_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Make As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_MAKE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_MAKE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=255)> _
    Public Property ItemName As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_ITEM_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_ITEM_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_ITEM_NAME, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RegistrationStatusId As Guid
        Get
            CheckDeleted()
            If row(CustItemDAL.COL_NAME_REGISTRATION_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CustItemDAL.COL_NAME_REGISTRATION_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_REGISTRATION_STATUS_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property Coverage As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_COVERAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_COVERAGE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_COVERAGE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property ImeiNumber As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_IMEI_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_IMEI_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_IMEI_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)> _
    Public Property ProductKey As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_PRODUCT_KEY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_PRODUCT_KEY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_PRODUCT_KEY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)> _
    Public Property OrderRefNum As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_ORDER_REF_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_ORDER_REF_NUM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_ORDER_REF_NUM, Value)
        End Set
    End Property

    Public Property ProductProcurementDate As DateType
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_PRODUCT_PROCUREMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CustItemDAL.COL_NAME_PRODUCT_PROCUREMENT_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_PRODUCT_PROCUREMENT_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IsDeletedId As Guid
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_IS_DELETED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustItemDAL.COL_NAME_IS_DELETED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_IS_DELETED_ID, Value)
        End Set
    End Property

    <ValueMandatoryConditionally("")> _
    Public Property CellPhone As String
        Get
            CheckDeleted()
            If Row(CustItemDAL.COL_NAME_CELL_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustItemDAL.COL_NAME_CELL_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CustItemDAL.COL_NAME_CELL_PHONE, Value)
        End Set
    End Property


    Private _dealer As Dealer
    Private _custReg As CustRegistration
    Private _equipmentId As Guid

    Public Property CustomerRegistration As CustRegistration
        Get
            If (_custReg Is Nothing) Then
                _custReg = New CustRegistration(RegistrationId)
            End If
            Return _custReg
        End Get
        Set
            _custReg = value
        End Set
    End Property

    Public Property Dealer As Dealer
        Get
            If (_dealer Is Nothing) Then
                If (CustomerRegistration IsNot Nothing) Then
                    _dealer = New Dealer(CustomerRegistration.DealerId)
                End If
            End If
            Return _dealer
        End Get
        Set
            _dealer = value
        End Set
    End Property

    Public Property EquipmentId As Guid
        Get
            If (_equipmentId = Guid.Empty) Then
                If (Dealer IsNot Nothing) Then
                    _equipmentId = Equipment.FindEquipment(Dealer.Dealer, Make, Model, RegistrationDate)
                End If
            End If
            Return _equipmentId
        End Get
        Set
            _equipmentId = value
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Shared Function FindRegistration(certificateId As Guid) As CustItemDAL.RegistrationDetails
        Dim dal As New CustItemDAL
        Try
            Return dal.FindRegistration(certificateId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function CreateItemElements(customerItem As CustItemDC, custRegBO As CustRegistration) As String
        Try
            Dim ds As DataSet
            Dim noId As Guid
            Dim regStatusActiveId As Guid
            Dim regStatusPendingId As Guid
            Dim certItemId As Guid
            Dim equipmentId As Guid
            Dim cellNumber As String

            noId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
            regStatusActiveId = LookupListNew.GetIdFromCode(LookupListCache.LK_REGSTATUS, Codes.REGSTATUS_ACTIVE)
            regStatusPendingId = LookupListNew.GetIdFromCode(LookupListCache.LK_REGSTATUS, Codes.REGSTATUS_PENDING)

            If IsIMEINumberValid(customerItem.IMEINumber) Then
                equipmentId = Equipment.FindEquipment(customerItem.DealerCode, customerItem.Make, customerItem.Model, customerItem.RegistrationDate)

                If (equipmentId <> Guid.Empty) Then
                    'check if enrollment came in for the customer's device & is in active state
                    If customerItem.CellPhone IsNot Nothing Then
                        cellNumber = customerItem.CellPhone.Trim()
                    End If
                    certItemId = GetCertItemIDforTaxImei(custRegBO.TaxId, customerItem.IMEINumber, custRegBO.DealerId, cellNumber)

                    ds = GetItemforDealerAndRegistration(customerItem.IMEINumber, custRegBO.DealerId, custRegBO.Id)
                    If ds.Tables(0).Rows.Count = 0 Then
                        If CheckItemforDealer(customerItem.IMEINumber, custRegBO.DealerId) > 0 Then
                            'IMEI used by other user in the current dealer 
                            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.IMEI_NUMBER_ALREADY_IN_USE_ERR, GetType(CustItem), Nothing, "", Nothing)}
                            Throw New BOValidationException(errors, GetType(CustItem).FullName)
                        End If
                        'Add a new device/item for the user
                        Dim objInsertCustItem As New CustItem()

                        objInsertCustItem.CustomerRegistration = custRegBO
                        objInsertCustItem.EquipmentId = equipmentId

                        objInsertCustItem.RegistrationId = custRegBO.Id
                        objInsertCustItem.RegistrationDate = customerItem.RegistrationDate
                        objInsertCustItem.ImeiNumber = customerItem.IMEINumber
                        objInsertCustItem.Make = customerItem.Make
                        objInsertCustItem.Model = customerItem.Model
                        objInsertCustItem.ItemName = customerItem.ItemName
                        objInsertCustItem.OrderRefNum = customerItem.OrderReferenceNumber
                        objInsertCustItem.IsDeletedId = noId
                        If certItemId <> Guid.Empty Then
                            objInsertCustItem.RegistrationStatusId = regStatusActiveId
                            objInsertCustItem.CertItemId = certItemId
                        Else
                            objInsertCustItem.RegistrationStatusId = regStatusPendingId
                            objInsertCustItem.CertItemId = Guid.Empty
                        End If
                        objInsertCustItem.CellPhone = customerItem.CellPhone
                        objInsertCustItem.Save()
                        Return OK_RESPONSE
                    Else
                        If (LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, New Guid(CType(ds.Tables(0).Rows(0)("is_deleted_id"), Byte()))) = Codes.YESNO_N) Then
                            'IMEI already added by the current user under the current dealer and is not deleted
                            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.ITEM_ALREADY_EXISTS_ERR, GetType(CustItem), Nothing, "", Nothing)}
                            Throw New BOValidationException(errors, GetType(CustItem).FullName)
                        End If
                        'undelete the device/item for the user and reassign the make and model and cert item id and registration status
                        Dim objUpdateCustItem As New CustItem(New Guid(CType(ds.Tables(0).Rows(0)("registration_item_id"), Byte())))

                        objUpdateCustItem.CustomerRegistration = custRegBO
                        objUpdateCustItem.EquipmentId = equipmentId

                        objUpdateCustItem.Make = customerItem.Make
                        objUpdateCustItem.Model = customerItem.Model
                        objUpdateCustItem.IsDeletedId = noId
                        If certItemId <> Guid.Empty Then
                            objUpdateCustItem.RegistrationStatusId = regStatusActiveId
                            objUpdateCustItem.CertItemId = certItemId
                        Else
                            objUpdateCustItem.RegistrationStatusId = regStatusPendingId
                            objUpdateCustItem.CertItemId = Guid.Empty
                        End If
                        objUpdateCustItem.ItemName = customerItem.ItemName
                        objUpdateCustItem.CellPhone = customerItem.CellPhone
                        objUpdateCustItem.Save()
                        Return OK_RESPONSE
                    End If
                Else
                    'Combination of Dealer, make, model and Registration not found in the system
                    Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.INVALID_MAKE_MODEL_ERR, GetType(CustItem), Nothing, "", Nothing)}
                    Throw New BOValidationException(errors, GetType(CustItem).FullName)
                End If
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function DeleteItem(custItemBO As CustItem) As String
        Try
            Dim yesId As Guid

            yesId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)

            custItemBO.IsDeletedId = yesId

            custItemBO.Save()

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CustItemDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function IsIMEINumberValid(imeiNumber As String) As Boolean
        Dim dal As New CustItemDAL

        If (imeiNumber = String.Empty) Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.IMEI_IS_REQUIRED_ERR, GetType(CustItem), Nothing, "", Nothing)}
            Throw New BOValidationException(errors, GetType(CustItem).FullName)
        ElseIf Not dal.ValidIMEI(imeiNumber) Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.INVALID_IMEI_NUMBER_ERR, GetType(CustItem), Nothing, "", Nothing)}
            Throw New BOValidationException(errors, GetType(CustItem).FullName)
        Else
            Return True
        End If

    End Function

    Public Shared Function ActivateItem(custItemDeleteActivate As CustItemDeleteActivateDC, custRegBO As CustRegistration, custItemBO As CustItem) As String
        Try
            Dim regStatusActiveId As Guid
            Dim regStatusPendingId As Guid
            Dim regStatusInActiveId As Guid
            Dim evntActivateId As Guid
            Dim evntReactivateId As Guid
            Dim argumentsToAddEvent As String

            regStatusActiveId = LookupListNew.GetIdFromCode(LookupListCache.LK_REGSTATUS, Codes.REGSTATUS_ACTIVE)
            regStatusPendingId = LookupListNew.GetIdFromCode(LookupListCache.LK_REGSTATUS, Codes.REGSTATUS_PENDING)
            regStatusInActiveId = LookupListNew.GetIdFromCode(LookupListCache.LK_REGSTATUS, Codes.REGSTATUS_INACTIVE)

            evntActivateId = LookupListNew.GetIdFromCode(LookupListCache.LK_EVNT_TYPE, Codes.EVNT_TYPE_ACTIVATE)
            evntReactivateId = LookupListNew.GetIdFromCode(LookupListCache.LK_EVNT_TYPE, Codes.EVNT_TYPE_REACTIVATE)

            argumentsToAddEvent = PublishedTask.REGISTRATION_ID & ":" & DALBase.GuidToSQLString(custRegBO.Id) & ";" & PublishedTask.REGISTRATION_ITEM_ID & ":" & DALBase.GuidToSQLString(custItemBO.Id)

            If (LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, custItemBO.IsDeletedId) = Codes.YESNO_N) Then

                If (custItemBO.RegistrationStatusId = regStatusActiveId OrElse custItemBO.RegistrationStatusId = regStatusPendingId) Then
                    'Raise Activate and Reactive events based on the status, product key
                    If (custItemBO.ProductKey <> String.Empty) Then
                        'if No product key available and status is Active or Pending then Activate the item
                        PublishedTask.AddEvent(Guid.Empty, Guid.Empty, Guid.Empty, custRegBO.DealerId, String.Empty, Guid.Empty, "CustomerRegistrationSvc_ActivateItem",
                                              argumentsToAddEvent, DateTime.UtcNow, evntReactivateId, Guid.Empty)

                    Else
                        'if product key available and status is Active or Pending then Activate the item
                        PublishedTask.AddEvent(Guid.Empty, Guid.Empty, Guid.Empty, custRegBO.DealerId, String.Empty, Guid.Empty, "CustomerRegistrationSvc_ActivateItem",
                                                  argumentsToAddEvent, DateTime.UtcNow, evntActivateId, Guid.Empty)
                    End If
                    Return OK_RESPONSE
                ElseIf (custItemBO.RegistrationStatusId = regStatusInActiveId) Then
                    Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.INACTIVE_REGISTRATION_DUE_TO_ENROLLMENT_ERR, GetType(CustItem), Nothing, "", Nothing)}
                    Throw New BOValidationException(errors, GetType(CustItem).FullName)
                End If
            Else
                Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.DELETED_ITEM_UNABLE_TO_ACTIVATE_ERR, GetType(CustItem), Nothing, "", Nothing)}
                Throw New BOValidationException(errors, GetType(CustItem).FullName)
            End If


        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Data Retrieveing Methods"
    Public Shared Function GetItemFromEmail(emailId As String, dealerId As Guid) As DataSet
        Try
            Dim dal As New CustItemDAL
            Dim ds As DataSet

            If (emailId = String.Empty) Then
                Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_EMAIL_IS_REQUIRED_ERR, GetType(CustRegistration), Nothing, "", Nothing)}
                Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
            ElseIf CustRegistration.IsEmailIDValid(emailId) Then
                ds = dal.GetItemFromEmail(emailId, CustRegistration.GetAddressTypeID(Codes.ADDRESS_TYPE__BILLING), dealerId)
                If ds.Tables(0).Rows.Count = 0 Then
                    Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.WS_NO_RECORDS_FOUND, GetType(CustRegistration), Nothing, "", Nothing)}
                    Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
                Else
                    Return ds
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetItemforDealerAndRegistration(imeiNumber As String, dealerId As Guid, registrationId As Guid) As DataSet
        Try
            Dim dal As New CustItemDAL

            Return dal.GetItemforDealerAndRegistration(imeiNumber, dealerId, registrationId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetItemByRegistrationAndIMEI(registrationId As Guid, imeiNumber As String) As Guid
        Try
            Dim dal As New CustItemDAL

            Return dal.GetItemByRegistrationAndIMEI(registrationId, imeiNumber)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function CheckItemforDealer(imeiNumber As String, dealerId As Guid) As Integer
        Try
            Dim dal As New CustItemDAL

            Return dal.CheckItemforDealer(imeiNumber, dealerId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCertItemIDforTaxImei(taxID As String, imeiNumber As String, dealerId As Guid, cellNumber As String) As Guid
        Try
            Dim dal As New CustItemDAL

            Return dal.GetCertItemIDforTaxImei(taxID, imeiNumber, dealerId, cellNumber)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.CELL_PHONE_NUMBER_IS_REQUIRED)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CustItem = CType(objectToValidate, CustItem)
            Dim returnVal As Boolean = True
            If (Not obj.EquipmentId.Equals(Guid.Empty)) Then
                Dim eqp As New Equipment(obj.EquipmentId)
                If (LookupListNew.GetCodeFromId(LookupListCache.LK_EQUIPMENT_TYPE, eqp.EquipmentTypeId) = Codes.EQUIPMENT_TYPE__SMARTPHONE _
                        OrElse LookupListNew.GetCodeFromId(LookupListCache.LK_EQUIPMENT_TYPE, eqp.EquipmentTypeId) = Codes.EQUIPMENT_TYPE__FEATUREPHONE) Then
                    If (obj.CellPhone Is Nothing OrElse obj.CellPhone.Trim = String.Empty) Then
                        returnVal = False
                    End If
                End If
            End If
            Return returnVal

        End Function
    End Class
#End Region
End Class



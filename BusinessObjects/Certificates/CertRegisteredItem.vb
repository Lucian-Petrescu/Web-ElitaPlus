Imports System.Collections.Generic
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/5/2017)  ********************

Public Class CertRegisteredItem
    Inherits BusinessObjectBase

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
            Dim dal As New CertRegisteredItemDAL
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
            Dim dal As New CertRegisteredItemDAL
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

    Public Function getDeviceTypeDesc(languageId As Guid, deviceTypeId As Guid) As String
        Dim dal As New CertRegisteredItemDAL
        Dim ds As DataSet
        Dim strDeviceTypeDesc As String = String.Empty
        ds = dal.getDeviceTypeDesc(languageId, deviceTypeId)
        If (ds.Tables(0) IsNot Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            strDeviceTypeDesc = ds.Tables(0).Rows(0)("device_type_desc").ToString()
        End If
        Return strDeviceTypeDesc

    End Function
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
            If row(CertRegisteredItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemDAL.COL_NAME_CERT_REGISTERED_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    Public Property CertItemId As Guid
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_CERT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertRegisteredItemDAL.COL_NAME_CERT_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_CERT_ITEM_ID, Value)
        End Set
    End Property
    Public Property CertId As Guid
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertItemDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property
    Public Property ManufacturerId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=255)>
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property Model As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)>
    Public Property SerialNumber As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property ItemDescription As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_ITEM_DESCRIPTION, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property PurchasedDate As DateType
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_PURCHASED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertRegisteredItemDAL.COL_NAME_PURCHASED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_PURCHASED_DATE, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property PurchasePrice As DecimalType
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertRegisteredItemDAL.COL_NAME_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property EnrollmentItem As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_ENROLLMENT_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_ENROLLMENT_ITEM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_ENROLLMENT_ITEM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)>
    Public Property ItemStatus As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_ITEM_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_ITEM_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_ITEM_STATUS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)>
    Public Property ValidatedBy As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_VALIDATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_VALIDATED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_VALIDATED_BY, Value)
        End Set
    End Property

    Public Property ValidatedDate As DateType
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_VALIDATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertRegisteredItemDAL.COL_NAME_VALIDATED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_VALIDATED_DATE, Value)
        End Set
    End Property

    Public Property ProdItemManufEquipId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID, Value)
        End Set
    End Property

    Public Property DeviceTypeId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_DEVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemDAL.COL_NAME_DEVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_DEVICE_TYPE_ID, Value)
        End Set
    End Property
    <ValueMandatory("")>
    Public Property DeviceType As String
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_DEVICE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertRegisteredItemDAL.COL_NAME_DEVICE_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_DEVICE_TYPE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property RegisteredItemName As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemDAL.COL_NAME_REGISTERED_ITEM_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemDAL.COL_NAME_REGISTERED_ITEM_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_REGISTERED_ITEM_NAME, Value)
        End Set
    End Property

    Public Property RegisteredItemIdentifier As String
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_REGISTERED_ITEM_IDENTIFIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertRegisteredItemDAL.COL_NAME_REGISTERED_ITEM_IDENTIFIER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_REGISTERED_ITEM_IDENTIFIER, Value)
        End Set
    End Property
    Public Property CertNumber As String
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertRegisteredItemDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property
    Public Property DealerCode As String
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertRegisteredItemDAL.COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_DEALER_CODE, Value)
        End Set
    End Property
    Public ReadOnly Property GetCertificate(certID As Guid) As Certificate
        Get
            Return New Certificate(certID, Dataset)
        End Get
    End Property
    Public Property RetailPrice As DecimalType
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_RETAIL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertRegisteredItemDAL.COL_NAME_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_RETAIL_PRICE, Value)
        End Set
    End Property
    Public Property RegistrationDate As DateType
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_REGISTRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertRegisteredItemDAL.COL_NAME_REGISTRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_REGISTRATION_DATE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=32)>
    Public Property IndixID As String
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_INDIXID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertRegisteredItemDAL.COL_NAME_INDIXID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_INDIXID, Value)
        End Set
    End Property

    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(CertRegisteredItemDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertRegisteredItemDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Function UpdateRegisterItem(ByRef ErrMsg As Collections.Generic.List(Of String), Id As Guid) As Boolean
        Dim dal As New CertRegisteredItemDAL
        Dim CertRegItemID As Guid
        Dim blnSuccess As Boolean = True
        Dim ErrRejectCode As String, ErrRejectReason As String
        Dim ErrMsgUIProgCode As String, ErrMsgParam As String
        Dim strErrMsg As String = "", strErrMsgUIProgCode As String = "", strErrMsgParamList As String = "", intParamCnt As Integer = 0
        Validate()
        blnSuccess = dal.UpdateRegisterItem(CertNumber, DealerCode, RegisteredItemIdentifier, Manufacturer, Model, SerialNumber, ItemDescription,
                                          PurchasedDate.Value, PurchasePrice.Value, RegisteredItemName, ElitaPlusIdentity.Current.ActiveUser.NetworkId,
                                           ItemStatus, RetailPrice, RegistrationDate, IndixID, DeviceType, Id, CertRegItemID, ErrRejectCode, ErrRejectReason, ErrMsgUIProgCode, ErrMsgParam)

        If Not blnSuccess Then
            'Translate the error message
            If ErrMsgUIProgCode <> "" Then
                Dim strTranslatedMsg As String = TranslationBase.TranslateLabelOrMessage(ErrMsgUIProgCode).Trim
                If ErrMsgUIProgCode <> String.Empty Then
                    strErrMsg = strTranslatedMsg
                End If
            End If
            ErrMsg.Add(strErrMsg)
        Else
            Load(Id)
        End If
        Return blnSuccess
    End Function
    Public Function RegisterItem(certificateNumber As String, dealer As String, ByRef ErrMsg As Collections.Generic.List(Of String), ByRef CertRegItemID As Guid) As Boolean
        Dim dal As New CertRegisteredItemDAL
        Dim blnSuccess As Boolean = True
        Dim ErrRejectCode As String, ErrRejectReason As String
        Dim ErrMsgUIProgCode As String, ErrMsgParam As String
        Dim strErrMsg As String = "", strErrMsgUIProgCode As String = "", strErrMsgParamList As String = "", intParamCnt As Integer = 0
        Validate()
        blnSuccess = dal.RegisterItem(certificateNumber,
                                      dealer,
                                      Manufacturer,
                                      Model,
                                      DeviceType,
                                      SerialNumber,
                                      ItemDescription,
                                      PurchasedDate.Value,
                                      PurchasePrice.Value,
                                      RegisteredItemName,
                                      RegistrationDate,
                                      RetailPrice,
                                      IndixID,
                                      ElitaPlusIdentity.Current.ActiveUser.NetworkId,
                                      CertRegItemID,
                                      ErrRejectCode,
                                      ErrRejectReason,
                                      ErrMsgUIProgCode,
                                      ErrMsgParam)

        If Not blnSuccess Then
            'Translate the error message
            If ErrMsgUIProgCode <> "" Then
                Dim strTranslatedMsg As String = TranslationBase.TranslateLabelOrMessage(ErrMsgUIProgCode).Trim
                If ErrMsgUIProgCode <> String.Empty Then
                    strErrMsg = strTranslatedMsg
                End If
            End If
            ErrMsg.Add(strErrMsg)
        Else
            Load(CertRegItemID)
        End If

        Return blnSuccess
    End Function

    Public Function CopyEnrolledEquip_into_ClaimedEquip() As ClaimEquipment
        Dim objClaimedEquipment As New ClaimEquipment()
        Dim cert As Certificate = New Certificate(CertId, Dataset)
        Dim dealer As Dealer = New Dealer(cert.DealerId, Dataset)
        Try
            With objClaimedEquipment
                .ManufacturerId = ManufacturerId
                .Model = Model
                'Resolve the equipment
                .EquipmentId = Equipment.GetEquipmentIdByEquipmentList(dealer.EquipmentListCode, DateTime.Today, ManufacturerId, Model)
                '.EquipmentId = Me.EquipmentId
                .SerialNumber = SerialNumber
                .IMEINumber = SerialNumber
                .ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, "C")
            End With
            Return objClaimedEquipment
        Catch ex As Exception
            'equipment not found exception may come so eating that
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class



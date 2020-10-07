Imports System.Collections.Generic

Public Class ClaimEquipment
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
            Dim dal As New ClaimEquipmentDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ClaimEquipmentDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
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

    Public Sub SaveClaimDeviceInfo()
        Try
            MyBase.Save()
            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimEquipmentDAL
                dal.Update(Row)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ClaimEquipmentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimEquipmentDAL.COL_NAME_CLAIM_EQUIPMENT_ID), Byte()))
            End If
        End Get
    End Property

    Public Property ClaimEquipmentDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_CLAIM_EQUIPMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimEquipmentDAL.COL_NAME_CLAIM_EQUIPMENT_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_CLAIM_EQUIPMENT_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimEquipmentDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimEquipmentDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property

    Public Property DeviceTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_DEVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimEquipmentDAL.COL_NAME_DEVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_DEVICE_TYPE_ID, Value)
        End Set
    End Property

    Public Property Comments() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_COMMENTS).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property

    Public ReadOnly Property Manufacturer As String
        Get
            CheckDeleted()
            Return LookupListNew.GetDescriptionFromId(LookupListNew.LK_MANUFACTURERS, ManufacturerId)
        End Get
    End Property

    <ValidateModelConditionally("")>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_MODEL).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_MODEL, Value)
        End Set
    End Property
    Public Property Color() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_COLOR) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_COLOR).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_COLOR, Value)
        End Set
    End Property
    Public Property Memory() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_MEMORY) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_MEMORY).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_MEMORY, Value)
        End Set
    End Property
    Public Property SKU() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_SKU) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_SKU).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_SKU, Value)
        End Set
    End Property

    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_SERIAL_NUMBER).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property

    Public Property IMEINumber() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_IMEI_NUMBER) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_IMEI_NUMBER).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_IMEI_NUMBER, Value)
        End Set
    End Property
    Public Property Price() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimEquipmentDAL.COL_NAME_PRICE), Decimal))
            End If
        End Get
        Set(Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_PRICE, Value)
        End Set
    End Property

    Public Property EquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimEquipmentDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_EQUIPMENT_ID, Value)
            _EquipmentBO = Nothing
        End Set
    End Property

    Private _EquipmentBO As Equipment
    Public ReadOnly Property EquipmentDescription As String
        Get
            CheckDeleted()
            If (Not IsNothing(EquipmentId)) Then
                If _EquipmentBO Is Nothing Then
                    _EquipmentBO = New Equipment(EquipmentId, Dataset)
                End If
                Return _EquipmentBO.Description
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property EquipmentBO As Equipment
        Get
            CheckDeleted()
            If (Not IsNothing(EquipmentId)) Then
                If _EquipmentBO Is Nothing Then
                    _EquipmentBO = New Equipment(EquipmentId, Dataset)
                End If
                Return _EquipmentBO
            Else
                Return Nothing
            End If
        End Get
    End Property
    <ValueMandatory("")>
    Public Property ClaimEquipmentTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_CLAIM_EQUIPMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimEquipmentDAL.COL_NAME_CLAIM_EQUIPMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_CLAIM_EQUIPMENT_TYPE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property ClaimEquipmentType As String
        Get
            CheckDeleted()
            LookupListNew.GetDescriptionFromId(LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), ClaimEquipmentTypeId)
        End Get
    End Property


    Public Property Priority() As LongType
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_PRIORITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimEquipmentDAL.COL_NAME_PRIORITY), Long))
            End If
        End Get
        Set(Value As LongType)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_PRIORITY, Value)
        End Set
    End Property

    Private _claim As ClaimBase
    Public Property Claim() As ClaimBase
        Get
            If (_claim Is Nothing) Then
                If Not ClaimId.Equals(Guid.Empty) Then
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId, Dataset)
                End If
            End If
            Return _claim
        End Get
        Private Set(value As ClaimBase)
            _claim = value
        End Set
    End Property


    Private ReadOnly Property OriginalModel() As String
        Get
            Return CType(Row(ClaimEquipmentDAL.COL_NAME_MODEL, DataRowVersion.Original), String)
        End Get
    End Property

    Public Property ClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimEquipmentDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    Public Property shippingFromName() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_SHIPPINGFROMNAME) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_SHIPPINGFROMNAME).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_SHIPPINGFROMNAME, Value)
        End Set
    End Property
    Public Property shippingFromDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_SHIPPINGFROMDESCRIPTION) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_SHIPPINGFROMDESCRIPTION).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_SHIPPINGFROMDESCRIPTION, Value)
        End Set
    End Property

    Public Property DeviceType() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_DEVICE_TYPE) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_DEVICE_TYPE).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_DEVICE_TYPE, Value)
        End Set
    End Property

    Public Property EquipmentType() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_EQUIPMENT_TYPE) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_EQUIPMENT_TYPE).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_EQUIPMENT_TYPE, Value)
        End Set
    End Property
    Public Property PurchasedDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_PURCHASED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimEquipmentDAL.COL_NAME_PURCHASED_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_PURCHASED_DATE, Value)
        End Set
    End Property
    Public Property PurchasedPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimEquipmentDAL.COL_NAME_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set(Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property
    Public Property RegisteredItemName() As String
        Get
            CheckDeleted()
            If Row(ClaimEquipmentDAL.COL_NAME_REGISTERED_ITEM_NAME) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(ClaimEquipmentDAL.COL_NAME_REGISTERED_ITEM_NAME).ToString
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimEquipmentDAL.COL_NAME_REGISTERED_ITEM_NAME, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimEquipmentDAL
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

    Public Sub Copy(original As ClaimEquipment)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Claim Equipment.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    Public Function ValidateForClaimProcess(ByRef msgList As List(Of String)) As Boolean
        Dim flag As Boolean = True
        If ManufacturerId.Equals(Guid.Empty) Then
            flag = flag And False
            msgList.Add("CLAIMED_DEVICE_MANUFACTURER_IS_EMPTY")
        End If
        If String.IsNullOrEmpty(Model) Then
            flag = flag And False
            msgList.Add("CLAIMED_DEVICE_MODEL_IS_EMPTY")
        End If
        If String.IsNullOrEmpty(SerialNumber) Then
            flag = flag And False
            msgList.Add("CLAIMED_DEVICE_SERIAL_NUMBER_IS_EMPTY")
        End If
        Return flag
    End Function



    Public Shared Function GetLatestClaimEquipmentInfo(claimId As Guid, equipTypeId As Guid) As ClaimEquipmentDV
        Try
            Dim dal As New ClaimEquipmentDAL
            
            If (Not ClaimId.Equals(Guid.Empty)) Then
                Return New ClaimEquipmentDV(dal.GetLatestClaimEquipmentInfo(claimId, equipTypeId).Tables(0))
            End If

        Catch ex As Exception
            
        End Try
    End Function


    Public Shared Sub UpdateClaimEquipmentInfo(claimEquipmentId As Guid, comments As String)
        Dim dal As New ClaimEquipmentDAL
        dal.UpdateClaimEquipmentInfo(claimEquipmentId, comments)
    End Sub

    Public Shared Function GetReplacementItemInfo(claimId As Guid) As ReplacementEquipmentDV
        Try
            Dim dal As New ClaimEquipmentDAL

            If (Not claimId.Equals(Guid.Empty)) Then
                Return New ReplacementEquipmentDV(dal.GetReplacementItemInfo(claimId).Tables(0))
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetReplacementItemStatus(claimId As Guid, equipTypeId As Guid) As ReplacementItemStatusDV
        Try
            Dim dal As New ClaimEquipmentDAL

            If (Not claimId.Equals(Guid.Empty)) Then
                Return New ReplacementItemStatusDV(dal.GetReplacementItemStatus(claimId, equipTypeId).Tables(0))
            End If
        Catch ex As Exception

        End Try
    End Function

#End Region

    Public Class ClaimEquipmentDV
        Inherits DataView

        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_MAKE As String = "make"
        Public Const COL_MODEL As String = "model"
        Public Const COL_SERIAL_NUMBER As String = "serial_number"
        Public Const COL_SKU As String = "sku"
        Public Const COL_IMEI_NUMBER As String = "imei_number"
        Public Const COL_COMMENTS As String = "comments"
        Public Const COL_CLAIM_EQUIPMENT_ID As String = "claim_equipment_id"
        

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
    Public Class ClaimEquipmentList
        Inherits BusinessObjectListEnumerableBase(Of ClaimBase, ClaimEquipment)

        Public Sub New(parent As ClaimBase)
            MyBase.New(LoadTable(parent), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimEquipment).ClaimId.Equals(CType(Parent, ClaimBase).Id)
        End Function

        Private Shared Function LoadTable(parent As ClaimBase) As DataTable
            Try
                'If Not parent.IsChildrenCollectionLoaded(GetType(ClaimEquipmentList)) Then
                Dim dal As New ClaimEquipmentDAL
                dal.LoadDeviceInfoList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                parent.AddChildrenCollection(GetType(ClaimEquipmentList))
                'End If
                Return parent.Dataset.Tables(ClaimEquipmentDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

#Region "Replacement Equipment"
    Public Class ReplacementEquipmentDV
        Inherits DataView


        Public Const COL_MAKE As String = "make"
        Public Const COL_MODEL As String = "model"
        Public Const COL_AUTHORIZATION_NUMBER As String = "authorization_number"
        Public Const COL_SERIAL_NUMBER As String = "serial_number"
        Public Const COL_IMEI_NUMBER As String = "imei_number"
        Public Const COL_DEVICE_TYPE As String = "Device_Type"
        Public Const COL_TYPE As String = "Type"
        Public Const COL_CLAIM_EQUIPMENT_ID As String = "claim_equipment_id"
        Public Const COL_VOID_REASON As String = "void_reason_xcd"
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class ReplacementItemStatusDV
        Inherits DataView

        Public Const COL_STATUS As String = "claim_equipment_status"
        Public Const COL_STATUS_DATE As String = "claim_equipment_status_date"
        Public Const COL_CLAIM_EQUIPMENT_STATUS_ID As String = "claim_equipment_status_id"


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateModelConditionally
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim objclaimEquip As ClaimEquipment = CType(objectToValidate, ClaimEquipment)
            Dim objClaim As ClaimBase = objclaimEquip.Claim

            If (objClaim Is Nothing) Then
                Return True
            End If

            If ((LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objClaim.Dealer.UseEquipmentId) = Codes.YESNO_Y) AndAlso
            ((objclaimEquip.IsNew OrElse
            (Not objclaimEquip.IsNew AndAlso
            Not String.IsNullOrEmpty(objclaimEquip.OriginalModel))))) Then
                Return New ValueMandatoryAttribute(DisplayName).IsValid(valueToCheck, objectToValidate)
            End If

            Return True
        End Function

    End Class

End Class

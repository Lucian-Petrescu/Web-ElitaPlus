'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/9/2017)  ********************

Public Class ProductEquipment
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    Public Sub New(ByVal familyDS As DataSet, ByVal id As Guid, ByVal tableName As String)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(tableName)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ProductEquipmentDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Protected Sub Load(ByVal tableName As String)
        Try
            Dim dal As New ProductEquipmentDAL
            If Me.Dataset.Tables.IndexOf(tableName) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(tableName).NewRow
            Me.Dataset.Tables(tableName).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ProductEquipmentDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Dim _modelDescription As String = String.Empty
    Dim _makeDescription As String = String.Empty

    Private Sub Initialize()
    End Sub
#End Region
#Region "Constants"
    Private Const PRODUCT_EQUIPMENT_FORM001 As String = "PRODUCT_EQUIPMENT_FORM001" ' Expiration date must be greater than or equal to Effective date
    Private Const PRODUCT_EQUIPMENT_FORM002 As String = "PRODUCT_EQUIPMENT_FORM002" ' Check if the expiration is valid and do not overlap with another range
    Private Const PRODUCT_EQUIPMENT_FORM003 As String = "PRODUCT_EQUIPMENT_FORM003" ' Effective date  must be greater than Today
    Private Const PRODUCT_EQUIPMENT_FORM004 As String = "PRODUCT_EQUIPMENT_FORM004" ' Equipment is required for Benefits
    Private Const PRODUCT_EQUIPMENT_FORM005 As String = "PRODUCT_EQUIPMENT_FORM005" ' There is an overlapping for the Make/Model in the same time frame

    Private Const PRODUCT_EQUIPMENT_EFFECTIVE_DATE As Integer = 0
    Private Const PRODUCT_EQUIPMENT_EXPIRATION_DATE As Integer = 1
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ProductEquipmentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductEquipmentDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID), Byte()))
            End If
        End Get
    End Property
    <ValueMandatory("")>
    Public Property ProductCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductEquipmentDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property
    Public Property ItemId() As Guid
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductEquipmentDAL.COL_NAME_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_ITEM_ID, Value)
        End Set
    End Property

    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductEquipmentDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property
    <EquipmentRequired("Make/Model"), EquipmentOverlappingValidation("")>
    Public Property EquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductEquipmentDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidEffectiveDate("")>
    Public Property EffectiveDateProductEquip() As DateType
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_EFFECTIVE_DATE_PRODUCT_EQUIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ProductEquipmentDAL.COL_NAME_EFFECTIVE_DATE_PRODUCT_EQUIP), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_EFFECTIVE_DATE_PRODUCT_EQUIP, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidExpirationDate("")>
    Public Property ExpirationDateProductEquip() As DateType
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_EXPIRATION_DATE_PRODUCT_EQUIP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ProductEquipmentDAL.COL_NAME_EXPIRATION_DATE_PRODUCT_EQUIP), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_EXPIRATION_DATE_PRODUCT_EQUIP, Value)
        End Set
    End Property
    Public Property DeviceTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_DEVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductEquipmentDAL.COL_NAME_DEVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_DEVICE_TYPE_ID, Value)
        End Set
    End Property
    Public Property MethodOfRepairXcd() As String
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_METHOD_OF_REPAIR_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductEquipmentDAL.COL_NAME_METHOD_OF_REPAIR_XCD), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_METHOD_OF_REPAIR_XCD, value)
        End Set
    End Property
    Public Property ConfigPurposeXcd() As String
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_CONFIG_PURPOSE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductEquipmentDAL.COL_NAME_CONFIG_PURPOSE_XCD), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_CONFIG_PURPOSE_XCD, value)
        End Set
    End Property

    Public Property EquipmentMake As String
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_EQUIPMENT_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductEquipmentDAL.COL_NAME_EQUIPMENT_MAKE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_EQUIPMENT_MAKE, value)
        End Set
    End Property
    Public Property EquipmentModel As String
        Get
            CheckDeleted()
            If Row(ProductEquipmentDAL.COL_NAME_EQUIPMENT_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductEquipmentDAL.COL_NAME_EQUIPMENT_MODEL), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(ProductEquipmentDAL.COL_NAME_EQUIPMENT_MODEL, value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductEquipmentDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal ProductCodeId As Guid) As DataView
        Try
            Dim dal As New ProductEquipmentDAL
            Return New DataView(dal.LoadList(ProductCodeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetBenefitsList(ByVal ProductCodeId As Guid) As ProductEquipmentSearchDV
        Try
            Dim dal As New ProductEquipmentDAL

            Throw New NotImplementedException

            'Return New ProductEquipmentSearchDV(dal.LoadBenefitsList(ProductCodeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Private Function GetProdManuEquipList(ByVal ProductCodeId As Guid, ByVal ManufacturerId As Guid, ByVal EquipmentId As Guid) As DataView
        Try
            Dim dal As New ProductEquipmentDAL
            Return New DataView(dal.LoadProdManuEquipList(ProductCodeId, ManufacturerId, EquipmentId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_EQUIPMENT_FORM003)
        End Sub
        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductEquipment = CType(objectToValidate, ProductEquipment)

            Dim bValid As Boolean = True


            If Not obj Is Nothing AndAlso obj.ConfigPurposeXcd = ProductEquipmentDAL.BENEFITS_PURPOSE Then
                ' This should be always greater than Today's date in case of new record, 
                ' but for edit record if effective date is passed then no changes should be allowed
                If obj.IsNew AndAlso
                    Not obj.EffectiveDateProductEquip Is Nothing AndAlso
                    Not Convert.ToDateTime(obj.EffectiveDateProductEquip.Value) > Date.Today Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class EquipmentRequired
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_EQUIPMENT_FORM004)
        End Sub
        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductEquipment = CType(objectToValidate, ProductEquipment)

            If Not obj Is Nothing AndAlso obj.ConfigPurposeXcd = ProductEquipmentDAL.BENEFITS_PURPOSE Then
                If obj.EquipmentId.IsEmpty Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class EquipmentOverlappingValidation
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_EQUIPMENT_FORM005)
        End Sub
        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductEquipment = CType(objectToValidate, ProductEquipment)

            If Not obj Is Nothing AndAlso obj.ConfigPurposeXcd = ProductEquipmentDAL.BENEFITS_PURPOSE Then

                Dim equipmentTable As DataTable = obj.Dataset.Tables(ProductEquipmentDAL.TABLE_NAME_BENEFITS)
                If Not equipmentTable Is Nothing Then
                    For Each dataRowEquipment As DataRow In equipmentTable.Rows

                        Dim prodEquipmentID As Guid = New Guid(CType(dataRowEquipment(ProductEquipmentDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID), Byte()))
                        If Not obj.Id.Equals(prodEquipmentID) Then
                            Dim equipmentID As Guid = New Guid(CType(dataRowEquipment(ProductEquipmentDAL.COL_NAME_EQUIPMENT_ID), Byte()))
                            If obj.EquipmentId.Equals(equipmentID) Then

                                Dim startDate As Date = DateHelper.GetDateValue(dataRowEquipment(ProductEquipmentDAL.COL_NAME_EFFECTIVE_DATE_PRODUCT_EQUIP))
                                Dim endDate As Date = DateHelper.GetDateValue(dataRowEquipment(ProductEquipmentDAL.COL_NAME_EXPIRATION_DATE_PRODUCT_EQUIP))

                                If obj.EffectiveDateProductEquip.Value >= startDate AndAlso obj.EffectiveDateProductEquip.Value <= endDate Then
                                    ' overlap
                                    Return False

                                End If
                                If obj.ExpirationDateProductEquip.Value >= startDate AndAlso obj.ExpirationDateProductEquip.Value <= endDate Then
                                    ' overlap
                                    Return False
                                End If
                                If obj.EffectiveDateProductEquip.Value <= startDate AndAlso obj.ExpirationDateProductEquip.Value >= endDate Then
                                    ' overlap
                                    Return False
                                End If
                            End If
                        End If
                    Next
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidExpirationDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_EQUIPMENT_FORM001)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductEquipment = CType(objectToValidate, ProductEquipment)

            Dim bValid As Boolean = True

            If Not obj.ExpirationDateProductEquip Is Nothing And Not obj.EffectiveDateProductEquip Is Nothing Then
                If Convert.ToDateTime(obj.EffectiveDateProductEquip.Value) > Convert.ToDateTime(obj.ExpirationDateProductEquip.Value) Then
                    Me.Message = PRODUCT_EQUIPMENT_FORM001
                    bValid = False
                ElseIf ValidateExpirationRange(obj.EffectiveDateProductEquip, obj.ExpirationDateProductEquip, obj) = False Then
                    Me.Message = PRODUCT_EQUIPMENT_FORM002
                    bValid = False
                End If
            End If

            Return bValid

        End Function

        Private Function ValidateExpirationRange(ByVal NewEffectiveDateProductEquip As Assurant.Common.Types.DateType, ByVal NewExpirationDateProductEquip As Assurant.Common.Types.DateType, ByVal oProductEquipment As ProductEquipment) As Boolean

            Dim bValid As Boolean = True
            Dim bChangeRec As Boolean = False
            Dim EffectiveDate, ExpirationDate As Date


            Dim oProductManuEquip As DataView = oProductEquipment.GetProdManuEquipList(oProductEquipment.ProductCodeId, oProductEquipment.ManufacturerId, oProductEquipment.EquipmentId)
            Dim ProductManuEquipRows As DataRowCollection = oProductManuEquip.Table.Rows
            Dim ProductManuEquipRow As DataRow

            If ProductManuEquipRows.Count = 1 Then
                'only one record for the combination
                bValid = True
            Else
                For Each ProductManuEquipRow In ProductManuEquipRows
                    EffectiveDate = ProductManuEquipRow(PRODUCT_EQUIPMENT_EFFECTIVE_DATE)
                    ExpirationDate = ProductManuEquipRow(PRODUCT_EQUIPMENT_EXPIRATION_DATE)

                    If NewEffectiveDateProductEquip.Value = EffectiveDate Then
                        bChangeRec = True
                    Else
                        If bChangeRec = True And NewExpirationDateProductEquip.Value >= EffectiveDate Then
                            bValid = False
                            Exit For
                        End If
                    End If
                Next
            End If
            Return bValid
        End Function
    End Class
#End Region

#Region "ProductEquipmentSearchDV"


    Public Class ProductEquipmentSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PROD_ITEM_MANUF_EQUIP_ID As String = "prod_item_manuf_equip_id"
        Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
        Public Const COL_NAME_ITEM_ID As String = "item_id"
        Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
        Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
        Public Const COL_NAME_CREATED_DATE As String = "created_date"
        Public Const COL_NAME_CREATED_BY As String = "created_by"
        Public Const COL_NAME_MODIFIED_DATE As String = "modified_date"
        Public Const COL_NAME_MODIFIED_BY As String = "modified_by"
        Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date_product_equip"
        Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date_product_equip"
        Public Const COL_NAME_DEVICE_TYPE_ID As String = "device_type_id"
        Public Const COL_NAME_METHOD_OF_REPAIR_XCD As String = "method_of_repair_xcd"
        Public Const COL_NAME_CONFIG_PURPOSE_XCD As String = "config_purpose_xcd"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ProductEquipmentSearchDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(ProductEquipmentSearchDV.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID) = Guid.NewGuid.ToByteArray
            row(ProductEquipmentSearchDV.COL_NAME_PRODUCT_CODE_ID) = Guid.Empty.ToByteArray
            row(ProductEquipmentSearchDV.COL_NAME_MANUFACTURER_ID) = DBNull.Value
            row(ProductEquipmentSearchDV.COL_NAME_EFFECTIVE_DATE) = DBNull.Value
            row(ProductEquipmentSearchDV.COL_NAME_EXPIRATION_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New ProductEquipmentSearchDV(dt)
        End Function

    End Class
#End Region


#Region "ProductBenefitsSearchDV"


    Public Class ProductBenefitsSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PROD_ITEM_MANUF_EQUIP_ID As String = "prod_item_manuf_equip_id"
        Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
        Public Const COL_NAME_ITEM_ID As String = "item_id"
        Public Const COL_NAME_MAKE As String = "EQUIPMENT_MAKE"
        Public Const COL_NAME_MODEL As String = "EQUIPMENT_MODEL"
        Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
        Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
        Public Const COL_NAME_CREATED_DATE As String = "created_date"
        Public Const COL_NAME_CREATED_BY As String = "created_by"
        Public Const COL_NAME_MODIFIED_DATE As String = "modified_date"
        Public Const COL_NAME_MODIFIED_BY As String = "modified_by"
        Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date_product_equip"
        Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date_product_equip"
        Public Const COL_NAME_DEVICE_TYPE_ID As String = "device_type_id"
        Public Const COL_NAME_METHOD_OF_REPAIR_XCD As String = "method_of_repair_xcd"
        Public Const COL_NAME_CONFIG_PURPOSE_XCD As String = "config_purpose_xcd"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ProductBenefitsSearchDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(ProductBenefitsSearchDV.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID) = Guid.NewGuid.ToByteArray
            row(ProductBenefitsSearchDV.COL_NAME_PRODUCT_CODE_ID) = Guid.Empty.ToByteArray
            row(ProductBenefitsSearchDV.COL_NAME_MANUFACTURER_ID) = DBNull.Value
            row(ProductBenefitsSearchDV.COL_NAME_EFFECTIVE_DATE) = DBNull.Value
            row(ProductBenefitsSearchDV.COL_NAME_EXPIRATION_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New ProductBenefitsSearchDV(dt)
        End Function

    End Class
#End Region

    Public Class ProductBenefitsDetailList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As ProductCode)
            MyBase.New(LoadTable(parent), GetType(ProductEquipment), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ProductEquipment).ProductCodeId.Equals(CType(Parent, ProductCode).Id)
        End Function

        Public Overridable Function CreateNewChild() As BusinessObjectBase
            'Dim bo As BusinessObjectBase = Me.BOType.GetConstructor(Reflection.BindingFlags.Instance Or
            '                                                  Reflection.BindingFlags.NonPublic Or
            '                                                  Reflection.BindingFlags.Public, Nothing,
            '                                                  New Type() {GetType(DataSet)}, Nothing).Invoke(New Object() {Me._table.DataSet})
            'Return bo
            Dim bo As BusinessObjectBase = Me.GetNewChild(CType(Parent, ProductCode).Id, ProductEquipmentDAL.TABLE_NAME_BENEFITS)
            Return bo
        End Function

        Public Function Find(ByVal ProductBenefitsId As Guid) As ProductEquipment
            Dim bo As ProductEquipment
            For Each bo In Me
                If bo.Id.Equals(ProductBenefitsId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Function Delete(ByVal ProductBenefitsId As Guid)
            Dim bo As ProductEquipment
            Dim dr As DataRow

            dr = FindRow(ProductBenefitsId, ProductEquipmentSearchDV.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID, Parent.Dataset.Tables(ProductEquipmentDAL.TABLE_NAME_BENEFITS))

            If Not (dr Is Nothing) Then
                Parent.Dataset.Tables(ProductEquipmentDAL.TABLE_NAME_BENEFITS).Rows.Remove(dr)
            End If

        End Function

        Private Shared Function LoadTable(ByVal parent As ProductCode) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ProductBenefitsDetailList)) Then
                    parent.LoadProductBenefits()
                End If
                Return parent.Dataset.Tables(ProductEquipmentDAL.TABLE_NAME_BENEFITS)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
End Class



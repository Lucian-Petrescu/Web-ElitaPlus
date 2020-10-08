'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/3/2006)  ********************

Public Class Item
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const COL_ITEM_ID As String = "ITEM_ID"
    Public Const COL_DEALER_NAME As String = "DEALER_NAME"
    Public Const COL_PRODUCT_CODE As String = "PRODUCT_CODE"
    Public Const COL_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const COL_RISK_TYPE As String = "RISK_TYPE"

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
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
            Dim dal As New ItemDAL
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
            Dim dal As New ItemDAL
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
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        ' Default value for new objects
        Inuseflag = "N"
    End Sub

    Private pDealerId As Guid

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(ItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ItemDAL.COL_NAME_ITEM_ID), Byte()))
            End If
        End Get
    End Property


    <ValueMandatory("")> _
    Public Property DealerID As Guid
        Get
            If pDealerId.Equals(guid.Empty) Then
                Dim itemDAL As New ItemDAL
                Dim dealerDs As DataSet = itemDAL.getDealerId(Id)

                If dealerDs.Tables(0).Rows.Count = 0 Then
                    Return Nothing
                Else
                    Return New Guid(CType(dealerDs.Tables(0).Rows(0).Item(0), Byte()))
                End If
            Else
                Return pDealerId
            End If
        End Get
        Set
            pDealerId = Value
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProductCodeId As Guid
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ItemDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ItemDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ItemDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ItemDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_LONG)> _
    Public Property MaxReplacementCost As DecimalType
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_MAX_REPLACEMENT_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ItemDAL.COL_NAME_MAX_REPLACEMENT_COST), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ItemDAL.COL_NAME_MAX_REPLACEMENT_COST, Value)
        End Set
    End Property


    Public Property ItemNumber As LongType
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ItemDAL.COL_NAME_ITEM_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ItemDAL.COL_NAME_ITEM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property OptionalItem As Guid
        Get
            CheckDeleted()
            If row(ItemDAL.COL_NAME_OPTIONAL_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ItemDAL.COL_NAME_OPTIONAL_ITEM), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ItemDAL.COL_NAME_OPTIONAL_ITEM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5), ValidOptionalItemRequired(""), ValidOptionalItemDuplicate("")>
    Public Property OptionalItemCode As String
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_OPTIONAL_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ItemDAL.COL_NAME_OPTIONAL_ITEM_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ItemDAL.COL_NAME_OPTIONAL_ITEM_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property Inuseflag As String
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_INUSEFLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ItemDAL.COL_NAME_INUSEFLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ItemDAL.COL_NAME_INUSEFLAG, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ItemDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function ProductCodeExists() As Boolean
        Dim dal As New ItemDAL
        Dim lngItemNum As Long = 0
        If Not ItemNumber Is Nothing Then
            lngItemNum = ItemNumber.Value
        End If
        Return (dal.ProductCodeExists(ProductCodeId, RiskTypeId, lngItemNum))
    End Function

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(dealerId As Guid, ProductCodeId As Guid, RiskTypeId As Guid) As DataView
        Try
            Dim dal As New ItemDAL
            Return New DataView(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, dealerId, ProductCodeId, RiskTypeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Used by Olita Web Service 
    Public Shared Function getDealerItemsInfo(ByRef ds As DataSet, dealerId As Guid) As DataSet
        Try
            Dim dal As New ItemDAL
            Return dal.LoadDealerItemsInfo(ds, dealerId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidOptionalItemRequired
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "CODE_REQUIRED_FOR_OPTIONAL_ITEM")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As Item = CType(objectToValidate, Item)

            If LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_Y) = obj.OptionalItem AndAlso (obj.OptionalItemCode Is Nothing OrElse obj.OptionalItemCode.Trim = String.Empty) Then
                Return False
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidOptionalItemDuplicate
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "DUPLICATE_OPTIONAL_ITEM_CODE")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As Item = CType(objectToValidate, Item)
            If Not obj.OptionalItemCode Is Nothing AndAlso obj.OptionalItemCode.Trim <> String.Empty Then
                Dim lngItemNum As Long = 0
                Dim blnDup As Boolean = True
                If Not obj.ItemNumber Is Nothing Then
                    lngItemNum = obj.ItemNumber.Value
                End If
                Dim dal As New ItemDAL
                blnDup = dal.OptionalItemCodeExists(obj.ProductCodeId, lngItemNum, obj.OptionalItemCode.Trim)
                If blnDup Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region
End Class



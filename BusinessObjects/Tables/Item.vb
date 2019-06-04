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
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New ItemDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ItemDAL
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
    Private Sub Initialize()
        ' Default value for new objects
        Me.Inuseflag = "N"
    End Sub

    Private pDealerId As Guid

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ItemDAL.COL_NAME_ITEM_ID), Byte()))
            End If
        End Get
    End Property


    <ValueMandatory("")> _
    Public Property DealerID() As Guid
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
        Set(ByVal Value As Guid)
            pDealerId = Value
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProductCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ItemDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ItemDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ItemDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ItemDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_LONG)> _
    Public Property MaxReplacementCost() As DecimalType
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_MAX_REPLACEMENT_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ItemDAL.COL_NAME_MAX_REPLACEMENT_COST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ItemDAL.COL_NAME_MAX_REPLACEMENT_COST, Value)
        End Set
    End Property


    Public Property ItemNumber() As LongType
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ItemDAL.COL_NAME_ITEM_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ItemDAL.COL_NAME_ITEM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property OptionalItem() As Guid
        Get
            CheckDeleted()
            If row(ItemDAL.COL_NAME_OPTIONAL_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ItemDAL.COL_NAME_OPTIONAL_ITEM), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ItemDAL.COL_NAME_OPTIONAL_ITEM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5), ValidOptionalItemRequired(""), ValidOptionalItemDuplicate("")>
    Public Property OptionalItemCode() As String
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_OPTIONAL_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ItemDAL.COL_NAME_OPTIONAL_ITEM_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ItemDAL.COL_NAME_OPTIONAL_ITEM_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property Inuseflag() As String
        Get
            CheckDeleted()
            If Row(ItemDAL.COL_NAME_INUSEFLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ItemDAL.COL_NAME_INUSEFLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ItemDAL.COL_NAME_INUSEFLAG, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ItemDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function ProductCodeExists() As Boolean
        Dim dal As New ItemDAL
        Dim lngItemNum As Long = 0
        If Not Me.ItemNumber Is Nothing Then
            lngItemNum = Me.ItemNumber.Value
        End If
        Return (dal.ProductCodeExists(Me.ProductCodeId, Me.RiskTypeId, lngItemNum))
    End Function

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal dealerId As Guid, ByVal ProductCodeId As Guid, ByVal RiskTypeId As Guid) As DataView
        Try
            Dim dal As New ItemDAL
            Return New DataView(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, dealerId, ProductCodeId, RiskTypeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Used by Olita Web Service 
    Public Shared Function getDealerItemsInfo(ByRef ds As DataSet, ByVal dealerId As Guid) As DataSet
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

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "CODE_REQUIRED_FOR_OPTIONAL_ITEM")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
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

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "DUPLICATE_OPTIONAL_ITEM_CODE")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
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



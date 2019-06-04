Public Class ProductCodeParent
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

    Public Sub New(ByVal dealerId As Guid, ByVal productCode As String, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(dealerId, productCode)
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
            Dim dal As New ProductCodeParentDAL
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

    Protected Sub Load(ByVal dealerId As Guid, ByVal productCode As String)
        Try
            Dim dal As New ProductCodeParentDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(Id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                'dal.LoadByDealerProduct(Me.Dataset, dealerId, productCode)
                'Me.Row = Me.FindRow(dealerId, dal.COL_NAME_DEALER_ID, productCode, dal.COL_NAME_PRODUCT_CODE, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ProductCodeParentDAL
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
    End Sub
#End Region
#Region "Properties"

    Public ReadOnly Property Id As Guid
        Get
            If Row(ProductCodeParentDAL.COL_NAME_PRODUCT_CODE_PARENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeParentDAL.COL_NAME_PRODUCT_CODE_PARENT_ID), Byte()))
            End If
        End Get

    End Property

    <ValueMandatory("")>
    Public Property ProductCodeId As Guid
        Get
            If Row(ProductCodeParentDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeParentDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            Me.SetValue(ProductCodeParentDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set

    End Property

    <ValueMandatory("")>
    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If Row(ProductCodeParentDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ProductCodeParentDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ProductCodeParentDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property Expiration() As DateType
        Get
            If Row(ProductCodeParentDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ProductCodeParentDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(value As DateType)
            CheckDeleted()
            Me.SetValue(ProductCodeParentDAL.COL_NAME_EXPIRATION, value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=9999999999999.99)>
    Public Property SmartBundleFlatAmt() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductCodeParentDAL.COL_NAME_SMART_BUNDLE_FLAT_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductCodeParentDAL.COL_NAME_SMART_BUNDLE_FLAT_AMT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ProductCodeParentDAL.COL_NAME_SMART_BUNDLE_FLAT_AMT, Value)
        End Set
    End Property

    Public Property SmartBundleFlatAmtCurrency() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeParentDAL.COL_NAME_SMART_BUNDLE_FLAT_AMT_CURRENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeParentDAL.COL_NAME_SMART_BUNDLE_FLAT_AMT_CURRENCY), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductCodeParentDAL.COL_NAME_SMART_BUNDLE_FLAT_AMT_CURRENCY, Value)
        End Set
    End Property

    Public Property PaymentSplitRuleId() As Guid
        Get
            CheckDeleted()
            If Row(ProductCodeParentDAL.COL_NAME_PAYMENT_SPLIT_RULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductCodeParentDAL.COL_NAME_PAYMENT_SPLIT_RULE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductCodeParentDAL.COL_NAME_PAYMENT_SPLIT_RULE_ID, Value)
        End Set
    End Property
#End Region

#Region "ProductCodeSearchDV"

    Public Class ProductCodeSearchDV
        Inherits DataView

        Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_EFFECTIVE As String = "effective"
        Public Const COL_EXPIRATION As String = "expiration"
        Public Const COL_SMART_BUNDLE_AMT As String = "smart_bundle_flat_amt"
        Public Const COL_SMART_BUNDLE_CURRENCY As String = "smart_bundle_flat_amt_currency"
        Public Const COL_PRODUCT_CODE_ID As String = "product_code_id"
        Public Const COL_PRODUCT_CODE_PARENT_ID As String = "product_code_parent_id"
        Public Const COL_PAYMENT_SPLIT_RULE_ID As String = "payment_split_rule"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class


#End Region
#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso (Me.IsDirty OrElse Me.IsFamilyDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductCodeParentDAL
                dal.Update(Me.Row)
                'MyBase.UpdateFamily(Me.Dataset)
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

    Public Shared Function GetList(ByVal DealerId As Guid, ByVal ProductId As Guid) As DataView
        Try
            Dim dal As New ProductCodeParentDAL
            Dim ds As New DataSet

            ds = dal.LoadList(DealerId, ProductId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return New ProductCodeSearchDV(ds.Tables(0))

        Catch ex As Exception

        End Try
    End Function
#End Region

End Class

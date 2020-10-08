'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/17/2013)  ********************

Public Class ProductPolicy
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
            Dim dal As New ProductPolicyDAL
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
            Dim dal As New ProductPolicyDAL
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


    Public Sub InitTable()
        Dataset.Tables(ProductPolicyDAL.TABLE_NAME).Rows.Clear()
    End Sub

    Public Sub AddRowsToTable(rowval As DataRow, Optional ByVal updateRowVal As Boolean = False)
        Dim dal As New ProductPolicyDAL
        Row = FindRow(Id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
        Row(1) = rowval(1)
        Row(2) = rowval(2)
        Row(3) = rowval(3)

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
            If Row(ProductPolicyDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductPolicyDAL.COL_NAME_PRODUCT_POLICY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ProductCodeId As Guid
        Get
            CheckDeleted()
            If Row(ProductPolicyDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductPolicyDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductPolicyDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TypeOfEquipmentId As Guid
        Get
            CheckDeleted()
            If Row(ProductPolicyDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductPolicyDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductPolicyDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ExternalProdCodeId As Guid
        Get
            CheckDeleted()
            If Row(ProductPolicyDAL.COL_NAME_EXTERNAL_PROD_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductPolicyDAL.COL_NAME_EXTERNAL_PROD_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductPolicyDAL.COL_NAME_EXTERNAL_PROD_CODE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Policy As LongType
        Get
            CheckDeleted()
            If Row(ProductPolicyDAL.COL_NAME_POLICY_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductPolicyDAL.COL_NAME_POLICY_NUM), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductPolicyDAL.COL_NAME_POLICY_NUM, Value)
        End Set
    End Property

    Public TypeOfEqup As String
    Public ExtProdCode As String
    Public Property TypeOfEquipment As String
        Get            
            Return TypeOfEqup
        End Get
        Set
            TypeOfEqup = Value
        End Set
    End Property

    Public Property ExternalProdCode As String
        Get
            Return ExtProdCode
        End Get
        Set
            ExtProdCode = Value
        End Set
    End Property



#End Region

#Region "ProductPolicySearchDV"
    '    Public Class ProductPolicySearchDV
    '        Inherits DataView

    '#Region "Constants"
    '        Public Const COL_PRODUCT_POLICY_ID As String = "product_policy_id"
    '        Public Const COL_PRODUCT_CODE_ID As String = "Product_Code_Id"
    '        Public Const COL_TYPE_OF_EQUIPMENT_ID As String = "type_of_equipment_id"
    '        Public Const COL_DEALER_ID As String = "dealer_id"
    '        Public Const COL_TYPE_OF_EQUIPMENT As String = "type_of_equipment"
    '        Public Const COL_EXTERNAL_PROD_CODE_ID As String = "external_prod_code_id"
    '        Public Const COL_EXTERNAL_PROD_CODE As String = "external_prod_code"
    '        Public Const COL_POLICY_NUM As String = "policy"

    '#End Region


    '        Public Sub New()
    '            MyBase.New()
    '        End Sub

    '        Public Sub New(ByVal table As DataTable)
    '            MyBase.New(table)
    '        End Sub

    '        Public Function AddNewRowToEmptyDV() As ProductPolicySearchDV
    '            Dim dt As DataTable = Me.Table.Clone()
    '            Dim row As DataRow = dt.NewRow
    '            row(ProductPolicySearchDV.COL_PRODUCT_POLICY_ID) = (New Guid()).ToByteArray
    '            row(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID) = Guid.Empty.ToByteArray
    '            row(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT) = DBNull.Value
    '            row(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID) = Guid.Empty.ToByteArray
    '            row(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE) = DBNull.Value
    '            row(ProductPolicySearchDV.COL_POLICY_NUM) = DBNull.Value

    '            dt.Rows.Add(row)
    '            Return New ProductPolicySearchDV(dt)
    '        End Function
    '    End Class

    Public Class ProductPolicySearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PRODUCT_POLICY_ID As String = "product_policy_id"
        Public Const COL_PRODUCT_CODE_ID As String = "Product_Code_Id"
        Public Const COL_TYPE_OF_EQUIPMENT_ID As String = "type_of_equipment_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_TYPE_OF_EQUIPMENT As String = "type_of_equipment"
        Public Const COL_EXTERNAL_PROD_CODE_ID As String = "external_prod_code_id"
        Public Const COL_EXTERNAL_PROD_CODE As String = "external_prod_code"
        Public Const COL_POLICY_NUM As String = "policy"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ProductPolicyId(row As DataRow) As Guid
            Get
                If row(ProductPolicyDAL.COL_NAME_PRODUCT_POLICY_ID) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(row(ProductPolicyDAL.COL_NAME_PRODUCT_POLICY_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property ProductCodeId(row As DataRow) As Guid
            Get
                If row(ProductPolicyDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(row(ProductPolicyDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property TypeOfEquipmentId(row As DataRow) As Guid
            Get
                If row(ProductPolicyDAL.COL_NAME_PRODUCT_POLICY_ID) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(row(ProductPolicyDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property ExternalProdCodId(row As DataRow) As Guid
            Get
                If row(ProductPolicyDAL.COL_NAME_PRODUCT_POLICY_ID) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(row(ProductPolicyDAL.COL_NAME_EXTERNAL_PROD_CODE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Policy(row As DataRow) As Long
            Get
                If row(ProductPolicyDAL.COL_NAME_PRODUCT_POLICY_ID) Is DBNull.Value Then Return Nothing
                Return CType(row(ProductPolicyDAL.COL_NAME_POLICY_NUM), Long)
            End Get
        End Property

        Public Shared ReadOnly Property ExternalProdCod(row As DataRow) As String
            Get
                If row(ProductPolicyDAL.COL_NAME_EXTERNAL_PROD_CODE) Is DBNull.Value Then Return Nothing
                Return CType(row(ProductPolicyDAL.COL_NAME_EXTERNAL_PROD_CODE), String)
            End Get
        End Property

        Public Shared ReadOnly Property TypeOfEquipment(row As DataRow) As String
            Get
                If row(ProductPolicyDAL.COL_NAME_TYPE_OF_EQUIPMENT) Is DBNull.Value Then Return Nothing
                Return CType(row(ProductPolicyDAL.COL_NAME_TYPE_OF_EQUIPMENT), String)
            End Get
        End Property

        Public Function AddNewRowToEmptyDV() As ProductPolicySearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(ProductPolicySearchDV.COL_PRODUCT_POLICY_ID) = (New Guid()).ToByteArray
            row(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID) = Guid.Empty.ToByteArray
            row(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT) = DBNull.Value
            row(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID) = Guid.Empty.ToByteArray
            row(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE) = DBNull.Value
            row(ProductPolicySearchDV.COL_POLICY_NUM) = DBNull.Value
            dt.Rows.Add(row)
            Return New ProductPolicySearchDV(dt)
        End Function

    End Class
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductPolicyDAL
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ProductCodeId As Guid) As ProductPolicySearchDV
        Try
            Dim dal As New ProductPolicyDAL
            Dim BOProd As ProductCode
            '  Return New ProductPolicySearchDV(BOProd.ProductPolicyDetailChildren)
            Return New ProductPolicySearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ProductCodeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

  
    Public Shared Sub AddNewRowToSearchDV(ByRef dv As ProductPolicySearchDV, NewProductPolicyBO As ProductPolicy)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        dv.Sort = ""
        If NewProductPolicyBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(ProductPolicySearchDV.COL_PRODUCT_POLICY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ProductPolicySearchDV.COL_PRODUCT_CODE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ProductPolicySearchDV.COL_POLICY_NUM, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(ProductPolicySearchDV.COL_PRODUCT_POLICY_ID) = NewProductPolicyBO.Id.ToByteArray
            row(ProductPolicySearchDV.COL_PRODUCT_CODE_ID) = NewProductPolicyBO.ProductCodeId.ToByteArray
            row(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID) = NewProductPolicyBO.TypeOfEquipmentId.ToByteArray
            row(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID) = NewProductPolicyBO.ExternalProdCodeId.ToByteArray
            row(ProductPolicySearchDV.COL_POLICY_NUM) = Nothing
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New ProductPolicySearchDV(dt)
        End If
    End Sub

    Public Shared Sub AddRowToSearchDV(ByRef dv As ProductPolicySearchDV, ProductPolicyBO As ProductPolicy)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False


        Dim row As DataRow
        dt = dv.Table

        row = dt.NewRow
        row(ProductPolicySearchDV.COL_PRODUCT_POLICY_ID) = ProductPolicyBO.Id.ToByteArray
        row(ProductPolicySearchDV.COL_PRODUCT_CODE_ID) = ProductPolicyBO.ProductCodeId.ToByteArray
        row(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID) = ProductPolicyBO.TypeOfEquipmentId.ToByteArray
        row(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID) = ProductPolicyBO.ExternalProdCodeId.ToByteArray
        row(ProductPolicySearchDV.COL_POLICY_NUM) = Nothing
        dt.Rows.Add(row)
        dv = New ProductPolicySearchDV(dt)
    End Sub

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid, bo As ProductPolicy) As ProductPolicySearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow
            row(ProductPolicyDAL.COL_NAME_PRODUCT_POLICY_ID) = bo.Id.ToByteArray
            row(ProductPolicyDAL.COL_NAME_TYPE_OF_EQUIPMENT_ID) = Guid.Empty.ToByteArray
            row(ProductPolicyDAL.COL_NAME_EXTERNAL_PROD_CODE_ID) = Guid.Empty.ToByteArray
            row(ProductPolicyDAL.COL_NAME_POLICY_NUM) = DBNull.Value
            dt.Rows.Add(row)
        End If

        'Return (dv)
        Return New ProductPolicySearchDV(dt)
    End Function

    Public Class ProductPolicyAssignedDV
        Inherits DataView

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property PRODUCTPOLICYID(row As DataRow) As String
            Get
                Return row(ProductPolicySearchDV.COL_PRODUCT_POLICY_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property PRODUCTCODEID(row As DataRow) As String
            Get
                Return row(ProductPolicySearchDV.COL_PRODUCT_CODE_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property TYPEOFEQUIPMENTID(row As DataRow) As String
            Get
                Return row(ProductPolicySearchDV.COL_TYPE_OF_EQUIPMENT_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property EXTERNALPRODCODEID(row As DataRow) As String
            Get
                Return row(ProductPolicySearchDV.COL_EXTERNAL_PROD_CODE_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property POLICYNUM(row As DataRow) As Long
            Get
                Return row(ProductPolicySearchDV.COL_POLICY_NUM).ToString
            End Get
        End Property

    End Class

#End Region

    Public Class ProductPolicyDetailList
        Inherits BusinessObjectListBase

        Public Sub New(parent As ProductCode)
            MyBase.New(LoadTable(parent), GetType(ProductPolicy), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, ProductPolicy).ProductCodeId.Equals(CType(Parent, ProductCode).Id)
        End Function

        Public Function Find(ProductPolicyId As Guid) As ProductPolicy
            Dim bo As ProductPolicy
            For Each bo In Me
                If bo.Id.Equals(ProductPolicyId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Function Delete(ProductPolicyId As Guid)
            Dim bo As ProductPolicy
            Dim dr As DataRow

            dr = FindRow(ProductPolicyId, ProductPolicySearchDV.COL_PRODUCT_POLICY_ID, Parent.Dataset.Tables(ProductPolicyDAL.TABLE_NAME))

            ' dr = Parent.Dataset.Tables(ProductPolicyDAL.TABLE_NAME).Rows.Find(ProductPolicyId)
            If Not (dr Is Nothing) Then
                Parent.Dataset.Tables(ProductPolicyDAL.TABLE_NAME).Rows.Remove(dr)
            End If

        End Function

        Private Shared Function LoadTable(parent As ProductCode) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ProductPolicyDetailList)) Then
                    Dim dal As New ProductPolicyDAL
                    dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, parent.Id, parent.Dataset)
                    ' dal.GetPaymentGroupDetail(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ProductPolicyDetailList))
                End If
                Return parent.Dataset.Tables(ProductPolicyDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
End Class







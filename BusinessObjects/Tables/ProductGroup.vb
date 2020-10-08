'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (09/16/2008)  ********************
'Namespace Table
Public Class ProductGroup
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
            Dim dal As New ProductGroupDAL
            If Dataset.Tables.IndexOf(dal.TABLE_PRODUCT_GROUP_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME).NewRow
            Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ProductGroupDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_PRODUCT_GROUP_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME))
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
            If Row(ProductGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(ProductGroupDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductGroupDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME, Value)
        End Set
    End Property

    Dim _Route As Route
    Public ReadOnly Property moRoute As Route
        Get
            If (_Route Is Nothing) Then
                _Route = New Route(Id, Nothing)
            End If

            Return (_Route)
        End Get

    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductGroupDAL
                dal.UpdateFamily(Dataset) 'New Code Added Manually
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

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsFamilyDirty
        End Get
    End Property

    Public Sub Copy(original As ProductGroup)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Product Group")
        End If
        'Copy myself
        CopyFrom(original)

    End Sub

#End Region

#Region "Children Related"

    Public Sub AttachProductcodes(selectedProductCodeStrCollection As ArrayList)
        Dim pgPcIdStr As String
        For Each pgPcIdStr In selectedProductCodeStrCollection
            Dim pgPcBO As ProductGroupPrc = New ProductGroupPrc(Dataset)
            If pgPcIdStr IsNot Nothing Then
                pgPcBO.ProductGroupId = Id
                pgPcBO.ProductCodeId = New Guid(pgPcIdStr)
                pgPcBO.Save()
            End If
        Next
    End Sub

    Public Sub DetachProductCodes(selectedProductCodeGuidStrCollection As ArrayList)
        Dim pgPcIdStr As String
        For Each pgPcIdStr In selectedProductCodeGuidStrCollection
            Dim pgPcBO As ProductGroupPrc = New ProductGroupPrc(Dataset, Id, New Guid(pgPcIdStr))
            If pgPcBO IsNot Nothing Then
                pgPcBO.Delete()
                pgPcBO.Save()
            End If
        Next
    End Sub

    Public Shared Function GetAvailableProductCodes(dealerId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim pgDAL As ProductGroupDAL = New ProductGroupDAL
        pgDAL.LoadAvailableProductCodes(ds, dealerId)
        Return ds
    End Function


    Public Shared Function GetSelectedProductCodes(dealerId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim pgDAL As ProductGroupDAL = New ProductGroupDAL
        pgDAL.LoadSelectedProductCodes(ds, dealerId)
        Return ds
    End Function

    Public Sub DeleteAndSave()
        CheckDeleted()
        BeginEdit()
        Try
            Delete()
            Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieving Methods"

    Public Shared Function getList(groupName As String, dealerID As Guid, productCodeId As String, riskTypeId As String) As ProductGroupSearchDV
        Try
            Dim dal As New ProductGroupDAL
            Return New ProductGroupSearchDV(dal.LoadList(Authentication.CompIds, dealerID, groupName, productCodeId, riskTypeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class ProductGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_DESCRIPTION As String = ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME
        Public Const COL_NAME_DEALER As String = ProductGroupDAL.COL_NAME_DEALER
        Public Const COL_NAME_DEALER_NAME As String = ProductGroupDAL.COL_NAME_DEALER_NAME
        Public Const COL_NAME_PRODUCT_GROUP_ID As String = ProductGroupDAL.COL_NAME_PRODUCT_GROUP_ID
#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ProductGroupId(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_PRODUCT_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property DealerCodeAndName(row As DataRow) As String
            Get
                Return row(COL_NAME_DEALER).ToString + "-" + row(COL_NAME_DEALER_NAME)
            End Get
        End Property


    End Class
#End Region

End Class
'End Namespace




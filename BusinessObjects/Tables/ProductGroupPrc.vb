'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/17/2008)  ********************

Public Class ProductGroupPrc
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
    Public Sub New(familyDS As DataSet, Id As Guid, ServCentId As Guid)
        MyBase.New(False)
        Dataset = familyDS
        LoadByProductGroupIdProductCodeId(Id, ServCentId)
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
            Dim dal As New ProductGroupPrcDAL
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
            Dim dal As New ProductGroupPrcDAL
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


    Protected Sub LoadByProductGroupIdProductCodeId(productGroupId As Guid, productCodeId As Guid)
        Try
            Dim dal As New ProductGroupPrcDAL

            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(productGroupId, dal.COL_NAME_PRODUCT_GROUP_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByProductGroupIdProductCodeId(Dataset, productGroupId, productCodeId)
                Row = FindRow(productCodeId, dal.COL_NAME_PRODUCT_CODE_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Variables"
    Private moProductCodeIDs As ArrayList
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
            If Row(ProductGroupPrcDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ProductGroupId As Guid
        Get
            CheckDeleted()
            If Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProductCodeId As Guid
        Get
            CheckDeleted()
            If Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductGroupPrcDAL
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

#End Region
#Region "Extended Functionality"

    Public Shared Function GetProductGroupIDs(oProductGroupId As Guid) As DataView
        Dim dal As New ProductGroupPrcDAL
        Dim ds As DataSet

        ds = dal.LoadGroupProductCodeIDs(oProductGroupId)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Shared Function GetAllProductGroupIDs(dealerID As Guid) As DataView
        Dim dal As New ProductGroupPrcDAL
        Dim ds As DataSet

        ds = dal.LoadAllGroupProductCodeIDs(dealerID)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Function ProductCodeIDs(isNetwork As Boolean, dealerID As Guid) As ArrayList
        Dim oPrcDv As DataView
        If moProductCodeIDs Is Nothing Then
            If isNetwork Then
                oPrcDv = GetProductGroupIDs(ProductGroupId)
            Else
                oPrcDv = GetAllProductGroupIDs(dealerID)
            End If

            moProductCodeIDs = New ArrayList

            If oPrcDv.Table.Rows.Count > 0 Then
                Dim index As Integer
                ' Create Array
                For index = 0 To oPrcDv.Table.Rows.Count - 1
                    If Not oPrcDv.Table.Rows(index)(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID) Is System.DBNull.Value Then
                        moProductCodeIDs.Add(New Guid(CType(oPrcDv.Table.Rows(index)(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID), Byte())))
                    End If
                Next
            End If
        End If
        Return moProductCodeIDs
    End Function
    '----------

#End Region
End Class







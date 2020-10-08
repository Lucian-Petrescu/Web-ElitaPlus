'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/17/2007)  ********************

Public Class RegionTaxDetail
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

    'Existing BO attaching to a BO family
    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RegionTaxDetailDAL
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
            Dim dal As New RegionTaxDetailDAL
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

#Region "Constant"

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private mTaxBucketDesc As String = String.Empty

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(RegionTaxDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionTaxDetailDAL.COL_NAME_REGION_TAX_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RegionTaxId As Guid
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_REGION_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionTaxDetailDAL.COL_NAME_REGION_TAX_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RegionTaxDetailDAL.COL_NAME_REGION_TAX_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TaxBucket As LongType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_TAX_BUCKET) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(RegionTaxDetailDAL.COL_NAME_TAX_BUCKET), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RegionTaxDetailDAL.COL_NAME_TAX_BUCKET, Value)
        End Set
    End Property



    Public Property Percent As DecimalType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(RegionTaxDetailDAL.COL_NAME_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RegionTaxDetailDAL.COL_NAME_PERCENT, Value)
        End Set
    End Property



    Public Property NonTaxable As DecimalType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_NON_TAXABLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(RegionTaxDetailDAL.COL_NAME_NON_TAXABLE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RegionTaxDetailDAL.COL_NAME_NON_TAXABLE, Value)
        End Set
    End Property



    Public Property MinimumTax As DecimalType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_MINIMUM_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(RegionTaxDetailDAL.COL_NAME_MINIMUM_TAX), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RegionTaxDetailDAL.COL_NAME_MINIMUM_TAX, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property GlAccountNumber As String
        Get
            CheckDeleted()
            If Row(RegionTaxDetailDAL.COL_NAME_GL_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionTaxDetailDAL.COL_NAME_GL_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RegionTaxDetailDAL.COL_NAME_GL_ACCOUNT_NUMBER, Value)
        End Set
    End Property


    Public Property Description As String
        Get
            Return mTaxBucketDesc
        End Get
        Set
            mTaxBucketDesc = Value
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RegionTaxDetailDAL
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
    Public Shared Function getList(RegionTaxID As Guid) As System.Data.DataView
        Try
            Dim dal As New RegionTaxDetailDAL, ds As New DataSet
            dal.LoadList(RegionTaxID, ds)
            Return New System.Data.DataView(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class



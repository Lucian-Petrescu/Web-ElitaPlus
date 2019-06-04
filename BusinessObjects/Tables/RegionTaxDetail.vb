'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/17/2007)  ********************

Public Class RegionTaxDetail
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

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    'Existing BO attaching to a BO family
    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RegionTaxDetailDAL
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
            Dim dal As New RegionTaxDetailDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(RegionTaxDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionTaxDetailDAL.COL_NAME_REGION_TAX_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RegionTaxId() As Guid
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_REGION_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionTaxDetailDAL.COL_NAME_REGION_TAX_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RegionTaxDetailDAL.COL_NAME_REGION_TAX_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TaxBucket() As LongType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_TAX_BUCKET) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(RegionTaxDetailDAL.COL_NAME_TAX_BUCKET), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(RegionTaxDetailDAL.COL_NAME_TAX_BUCKET, Value)
        End Set
    End Property



    Public Property Percent() As DecimalType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(RegionTaxDetailDAL.COL_NAME_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(RegionTaxDetailDAL.COL_NAME_PERCENT, Value)
        End Set
    End Property



    Public Property NonTaxable() As DecimalType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_NON_TAXABLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(RegionTaxDetailDAL.COL_NAME_NON_TAXABLE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(RegionTaxDetailDAL.COL_NAME_NON_TAXABLE, Value)
        End Set
    End Property



    Public Property MinimumTax() As DecimalType
        Get
            CheckDeleted()
            If row(RegionTaxDetailDAL.COL_NAME_MINIMUM_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(RegionTaxDetailDAL.COL_NAME_MINIMUM_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(RegionTaxDetailDAL.COL_NAME_MINIMUM_TAX, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property GlAccountNumber() As String
        Get
            CheckDeleted()
            If Row(RegionTaxDetailDAL.COL_NAME_GL_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionTaxDetailDAL.COL_NAME_GL_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegionTaxDetailDAL.COL_NAME_GL_ACCOUNT_NUMBER, Value)
        End Set
    End Property


    Public Property Description() As String
        Get
            Return mTaxBucketDesc
        End Get
        Set(ByVal Value As String)
            mTaxBucketDesc = Value
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RegionTaxDetailDAL
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
    Public Shared Function getList(ByVal RegionTaxID As Guid) As System.Data.DataView
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



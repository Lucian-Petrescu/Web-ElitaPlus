'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/22/2010)  ********************

Public Class AcctCovEntity
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

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AcctCovEntityDAL
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
            Dim dal As New AcctCovEntityDAL
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

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(AcctCovEntityDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_ACCT_COV_ENTITY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BusinessEntityId() As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AcctBusinessUnitId() As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCovEntityDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID, Value)
        End Set
    End Property



    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCovEntityDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property AcctCoverageTypeCode As String
        Get
            CheckDeleted()
            If Row(AcctCovEntityDAL.COL_NAME_ACCT_COVERAGE_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCovEntityDAL.COL_NAME_ACCT_COVERAGE_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCovEntityDAL.COL_NAME_ACCT_COVERAGE_TYPE_CODE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctCovEntityDAL
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

    Public Const COL_COVERAGE_TYPE As String = "coverage_type"

    Public Shared Function LoadList(ByVal acctCompany As Guid, ByVal RegionId As Guid, ByVal CoverageTypeId As Guid, ByVal BusinessUnitId As Guid) As DataView
        Try
            Dim dal As New AcctCovEntityDAL
            Dim ds As DataSet

            ds = dal.LoadList(acctCompany, RegionId, CoverageTypeId, BusinessUnitId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(AcctCovEntityDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRegions(ByVal CoverageTypeId As Guid, ByVal BusinessEntityId As Guid, ByVal AcctBusinessUnitId As Guid, ByVal RegionId As Guid)

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As AcctCovEntity) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(AcctCovEntityDAL.COL_NAME_ACCT_COV_ENTITY_ID) = bo.Id.ToByteArray
            row(AcctCovEntityDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID) = bo.AcctBusinessUnitId.ToByteArray
            row(AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID) = bo.BusinessEntityId.ToByteArray
            row(AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID) = bo.CoverageTypeId.ToByteArray        
            row(AcctCovEntityDAL.COL_NAME_REGION_ID) = bo.RegionId.ToByteArray            

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function
#End Region

End Class



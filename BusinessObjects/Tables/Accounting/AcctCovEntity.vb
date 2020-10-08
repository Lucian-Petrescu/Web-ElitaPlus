'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/22/2010)  ********************

Public Class AcctCovEntity
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
            Dim dal As New AcctCovEntityDAL
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
            Dim dal As New AcctCovEntityDAL
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
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(AcctCovEntityDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_ACCT_COV_ENTITY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CoverageTypeId As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BusinessEntityId As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AcctBusinessUnitId As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctCovEntityDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID, Value)
        End Set
    End Property



    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If row(AcctCovEntityDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCovEntityDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctCovEntityDAL.COL_NAME_REGION_ID, Value)
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
        Set
            CheckDeleted()
            SetValue(AcctCovEntityDAL.COL_NAME_ACCT_COVERAGE_TYPE_CODE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctCovEntityDAL
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

    Public Const COL_COVERAGE_TYPE As String = "coverage_type"

    Public Shared Function LoadList(acctCompany As Guid, RegionId As Guid, CoverageTypeId As Guid, BusinessUnitId As Guid) As DataView
        Try
            Dim dal As New AcctCovEntityDAL
            Dim ds As DataSet

            ds = dal.LoadList(acctCompany, RegionId, CoverageTypeId, BusinessUnitId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(AcctCovEntityDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRegions(CoverageTypeId As Guid, BusinessEntityId As Guid, AcctBusinessUnitId As Guid, RegionId As Guid)

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid, bo As AcctCovEntity) As DataView

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



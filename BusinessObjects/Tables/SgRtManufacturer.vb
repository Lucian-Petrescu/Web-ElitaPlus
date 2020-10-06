'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/27/2004)  ********************

Public Class SgRtManufacturer
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New SgRtManufacturerDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New SgRtManufacturerDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(SgRtManufacturerDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SgRtManufacturerDAL.COL_NAME_SGRT_MANUFACTURER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServiceGroupRiskTypeId() As Guid
        Get
            CheckDeleted()
            If row(SgRtManufacturerDAL.COL_NAME_SERVICE_GROUP_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SgRtManufacturerDAL.COL_NAME_SERVICE_GROUP_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(SgRtManufacturerDAL.COL_NAME_SERVICE_GROUP_RISK_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If row(SgRtManufacturerDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SgRtManufacturerDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(SgRtManufacturerDAL.COL_NAME_MANUFACTURER_ID, Value)
            'Set the Manufacturer Description
            Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim manufacturerDesc As String = LookupListNew.GetDescriptionFromId(dv, Value)
            SetValue(SgRtManufacturerDAL.COL_NAME_MANUFACTURER_DESCRIPTION, manufacturerDesc)
        End Set
    End Property

    Public ReadOnly Property ManufacturerDescription() As String
        Get
            CheckDeleted()
            If row(SgRtManufacturerDAL.COL_NAME_MANUFACTURER_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SgRtManufacturerDAL.COL_NAME_MANUFACTURER_DESCRIPTION), String)
            End If
        End Get
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SgRtManufacturerDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "List Methods"
    
#End Region


#Region "DataView Retrieving Methods"

#End Region

End Class



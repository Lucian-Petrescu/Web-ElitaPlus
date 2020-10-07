'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/2/2012)  ********************

Public Class EquipmentListDetail
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
            Dim dal As New EquipmentListDetailDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New EquipmentListDetailDAL
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

#Region "Constants"
    Friend Const EQUIPMENT_EXPIRATION_DEFAULT As String = "12/31/2499 23:59:59"
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
            If row(EquipmentListDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_LIST_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentId As Guid
        Get
            CheckDeleted()
            If row(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property



    Public Property EquipmentListId As Guid
        Get
            CheckDeleted()
            If row(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_LIST_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_LIST_ID, Value)
        End Set
    End Property



    Public Property Effective As DateType
        Get
            CheckDeleted()
            If row(EquipmentListDetailDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(EquipmentListDetailDAL.COL_NAME_EFFECTIVE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDetailDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property



    Public Property Expiration As DateType
        Get
            CheckDeleted()
            If row(EquipmentListDetailDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(EquipmentListDetailDAL.COL_NAME_EXPIRATION).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentListDetailDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New EquipmentListDetailDAL
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


    Public Sub AttachEquipments(ByVal selectedEquipmentGuidStrCollection As ArrayList)
        Dim cmpEquipmentIdStr As String
        For Each cmpEquipmentIdStr In selectedEquipmentGuidStrCollection
            Dim newBO As EquipmentListDetail = New EquipmentListDetail(Dataset)
            If Not newBO Is Nothing Then
                newBO.EquipmentId = ID
                newBO.EquipmentId = New Guid(cmpEquipmentIdStr)
                newBO.Save()
            End If
        Next

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Public Methods"
    Public Shared Function IsChild(ByVal EquipListId As Guid, ByVal EquipId As Guid) As Byte()

        Try
            Dim dal As New EquipmentListDetailDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(EquipListId, EquipId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(EquipmentListDetailDAL.TABLE_NAME).Rows.Count > 0 Then
                    Return ds.Tables(EquipmentListDetailDAL.TABLE_NAME).Rows(0)(EquipmentListDetailDAL.COL_NAME_EQUIPMENT_LIST_DETAIL_ID)
                Else
                    Return Guid.Empty.ToByteArray
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Sub Copy(ByVal original As EquipmentListDetail)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    'Public Shared Function ExpireExistingListEquipments(ByVal EquipmentListId As Guid) As Boolean

    '    Try
    '        Dim dal As New EquipmentListDetailDAL
    '        Dim oCompanyGroupIds As ArrayList

    '        oCompanyGroupIds = New ArrayList
    '        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

    '        Return dal.ExpireListEquipments(EquipmentListId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try

    '    Return False
    'End Function

    Public Shared Function GetEquipmentsInList(ByVal EquipmentListId As Guid) As ArrayList

        Try
            Dim dal As New EquipmentListDetailDAL
            Dim oCompanyGroupIds As ArrayList
            Dim oEquipmentsList As ArrayList
            Dim oDataTable As DataTable

            oCompanyGroupIds = New ArrayList
            oEquipmentsList = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            oDataTable = dal.GetEquipmentsInList(EquipmentListId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            For Each row As DataRow In oDataTable.Rows
                oEquipmentsList.Add(row(0))
            Next
            Return oEquipmentsList
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetEquipmentExpiration(ByVal EquipmentId As Guid) As DateTime

        Try
            Dim dal As New EquipmentListDetailDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.GetEquipmentExpiration(EquipmentId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("EXPIRATION")
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetEquipmentEffective(ByVal EquipmentId As Guid) As DateTime

        Try
            Dim dal As New EquipmentListDetailDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.GetEquipmentEffective(EquipmentId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("EFFECTIVE")
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCurrentDateTime() As DateTime
        Try
            Dim dal As New EquipmentListDetailDAL
            Return dal.GetCurrentDateTime()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class



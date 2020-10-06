Public Class CompanyFormExclusion
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal oFormId As Guid, ByVal oCompanyId As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(oFormId, oCompanyId)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New CompanyFormExclusionDAL
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
            Dim dal As New CompanyFormExclusionDAL
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

    Protected Sub Load(ByVal oFormId As Guid, ByVal oCompanyId As Guid)
        Try
            Dim dal As New CompanyFormExclusionDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(oFormId, dal.COL_NAME_FORM_ID, oCompanyId, dal.COL_NAME_COMPANY_ID, _
                                Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, oFormId, oCompanyId)
                Row = FindRow(oFormId, dal.COL_NAME_FORM_ID, oCompanyId, dal.COL_NAME_COMPANY_ID, _
                                Dataset.Tables(dal.TABLE_NAME))
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
            If row(CompanyFormExclusionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CompanyFormExclusionDAL.COL_NAME_COMPANY_FORM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property FormId() As Guid
        Get
            CheckDeleted()
            If row(CompanyFormExclusionDAL.COL_NAME_FORM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CompanyFormExclusionDAL.COL_NAME_FORM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CompanyFormExclusionDAL.COL_NAME_FORM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(CompanyFormExclusionDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CompanyFormExclusionDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CompanyFormExclusionDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CompanyFormExclusionDAL
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

#Region "DATA ACCESS ROUTINES"

    Public Shared Function PopulateList(ByVal oLanguageID As Guid) As DataView
        Dim oDs As DataSet
        Try
            Dim dal As New CompanyFormExclusionDAL
            oDs = dal.PopulateList(oLanguageID)
            Return oDs.Tables(CompanyFormExclusionDAL.TABLE_NAME).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetFormListByTabID(ByVal TabID As Guid, ByVal oLanguageID As Guid) As DataView
        Dim oDs As DataSet
        Try
            Dim dal As New CompanyFormExclusionDAL
            oDs = dal.LoadFormListByTabID(TabID, oLanguageID)
            Return oDs.Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCompanyPermissionByFormID(ByVal FormID As Guid) As DataView
        Dim oDs As DataSet
        Try
            Dim dal As New CompanyFormExclusionDAL
            oDs = dal.LoadCompanyPermissionListByFormID(FormID)
            Return oDs.Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class

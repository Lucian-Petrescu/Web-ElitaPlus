'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/15/2019)  ********************

Public Class SearchConfigAssignment
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
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
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
            If Dataset.Tables.IndexOf(SearchConfigAssignmentDAL.TableName) < 0 Then
                Dim dal As New SearchConfigAssignmentDAL
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(SearchConfigAssignmentDAL.TableName).NewRow
            Dataset.Tables(SearchConfigAssignmentDAL.TableName).Rows.Add(newRow)
            Row = newRow
            setvalue(SearchConfigAssignmentDAL.TableKeyName, Guid.NewGuid)
            Initialize() 
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)               
        Try
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(SearchConfigAssignmentDAL.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(SearchConfigAssignmentDAL.TableName) >= 0 Then
                Row = FindRow(id, SearchConfigAssignmentDAL.TableKeyName, Dataset.Tables(SearchConfigAssignmentDAL.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New SearchConfigAssignmentDAL            
                dal.Load(Dataset, id)
                Row = FindRow(id, SearchConfigAssignmentDAL.TableKeyName, Dataset.Tables(SearchConfigAssignmentDAL.TableName))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
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
            If row(SearchConfigAssignmentDAL.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SearchConfigAssignmentDAL.ColNameSearchConfigAssignmentId), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property SearchConfigId As Guid
        Get
            CheckDeleted()
            If row(SearchConfigAssignmentDAL.ColNameSearchConfigId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SearchConfigAssignmentDAL.ColNameSearchConfigId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigAssignmentDAL.ColNameSearchConfigId, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If row(SearchConfigAssignmentDAL.ColNameCompanyId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SearchConfigAssignmentDAL.ColNameCompanyId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigAssignmentDAL.ColNameCompanyId, Value)
        End Set
    End Property
	
	
    
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(SearchConfigAssignmentDAL.ColNameDealerId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SearchConfigAssignmentDAL.ColNameDealerId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigAssignmentDAL.ColNameDealerId, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SearchConfigAssignmentDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetDynamicSearchCriteriaFields(ByVal companyId As Guid, ByVal dealerId As Guid, ByVal languageCode As String, ByVal searchType As String) As DataView
        Try
            Dim dal As New SearchConfigAssignmentDAL
            Dim ds As DataSet

            ds = dal.LoadSearchCriteriaFields(companyId,dealerId,languageCode,searchType)
            Return (ds.Tables(SearchConfigAssignmentDAL.TableName).DefaultView)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

End Class



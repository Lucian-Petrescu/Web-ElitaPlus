'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/15/2019)  ********************

Public Class SearchConfigCriteria
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
    Public Sub New(id As Guid, familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDs As DataSet)
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
            If Dataset.Tables.IndexOf(SearchConfigCriteriaDAL.TableName) < 0 Then
                Dim dal As New SearchConfigCriteriaDAL
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(SearchConfigCriteriaDAL.TableName).NewRow
            Dataset.Tables(SearchConfigCriteriaDAL.TableName).Rows.Add(newRow)
            Row = newRow
            setvalue(SearchConfigCriteriaDAL.TableKeyName, Guid.NewGuid)
            Initialize() 
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)               
        Try
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(SearchConfigCriteriaDAL.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(SearchConfigCriteriaDAL.TableName) >= 0 Then
                Row = FindRow(id, SearchConfigCriteriaDAL.TableKeyName, Dataset.Tables(SearchConfigCriteriaDAL.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New SearchConfigCriteriaDAL            
                dal.Load(Dataset, id)
                Row = FindRow(id, SearchConfigCriteriaDAL.TableKeyName, Dataset.Tables(SearchConfigCriteriaDAL.TableName))
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
            If row(SearchConfigCriteriaDAL.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SearchConfigCriteriaDAL.ColNameSearchConfigCriteriaId), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property SearchConfigId As Guid
        Get
            CheckDeleted()
            If row(SearchConfigCriteriaDAL.ColNameSearchConfigId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SearchConfigCriteriaDAL.ColNameSearchConfigId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigCriteriaDAL.ColNameSearchConfigId, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property FieldNameXcd As String
        Get
            CheckDeleted()
            If row(SearchConfigCriteriaDAL.ColNameFieldNameXcd) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SearchConfigCriteriaDAL.ColNameFieldNameXcd), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigCriteriaDAL.ColNameFieldNameXcd, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property SequenceNumber As LongType
        Get
            CheckDeleted()
            If row(SearchConfigCriteriaDAL.ColNameSequenceNumber) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(SearchConfigCriteriaDAL.ColNameSequenceNumber), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigCriteriaDAL.ColNameSequenceNumber, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SearchConfigCriteriaDAL
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
    
#End Region

End Class



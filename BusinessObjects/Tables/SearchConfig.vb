Public Class SearchConfig
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
            If Dataset.Tables.IndexOf(SearchConfigDAL.TableName) < 0 Then
                Dim dal As New SearchConfigDAL
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(SearchConfigDAL.TableName).NewRow
            Dataset.Tables(SearchConfigDAL.TableName).Rows.Add(newRow)
            Row = newRow
            setvalue(SearchConfigDAL.TableKeyName, Guid.NewGuid)
            Initialize() 
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)               
        Try
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(SearchConfigDAL.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(SearchConfigDAL.TableName) >= 0 Then
                Row = FindRow(id, SearchConfigDAL.TableKeyName, Dataset.Tables(SearchConfigDAL.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New SearchConfigDAL
                dal.Load(Dataset, id)
                Row = FindRow(id, SearchConfigDAL.TableKeyName, Dataset.Tables(SearchConfigDAL.TableName))
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
            If row(SearchConfigDAL.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SearchConfigDAL.ColNameSearchConfigId), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(SearchConfigDAL.ColNameCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SearchConfigDAL.ColNameCode), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigDAL.ColNameCode, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=4000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(SearchConfigDAL.ColNameDescription) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SearchConfigDAL.ColNameDescription), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigDAL.ColNameDescription, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property SearchTypeXcd As String
        Get
            CheckDeleted()
            If row(SearchConfigDAL.ColNameSearchTypeXcd) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SearchConfigDAL.ColNameSearchTypeXcd), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SearchConfigDAL.ColNameSearchTypeXcd, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SearchConfigDAL
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



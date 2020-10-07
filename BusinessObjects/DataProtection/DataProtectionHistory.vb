'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/21/2018)  ********************

Public Class DataProtectionHistory
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
            Dim dal As New DataProtectionHistoryDAL
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
            Dim dal As New DataProtectionHistoryDAL            
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
    Private _restrict As String
    Private _isRequestIdUsed As String
    Private Sub Initialize()        
    End Sub
#End Region


#Region "Properties"

    Public ReadOnly Property Comment() As String
        Get
            If Row(DataProtectionHistoryDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DataProtectionHistoryDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
    End Property
    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(DataProtectionHistoryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DataProtectionHistoryDAL.COL_NAME_DATA_PROTECTION_HISTORY_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory(""),ValidStringLength("", Max:=50)> _
    Public Property EntityType() As String
        Get
            CheckDeleted()
            If row(DataProtectionHistoryDAL.COL_NAME_ENTITY_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DataProtectionHistoryDAL.COL_NAME_ENTITY_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_ENTITY_TYPE, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If row(DataProtectionHistoryDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DataProtectionHistoryDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property RequestId() As String
        Get
            CheckDeleted()
            If row(DataProtectionHistoryDAL.COL_NAME_REQUEST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(DataProtectionHistoryDAL.COL_NAME_REQUEST_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_REQUEST_ID, Value)
        End Set
    End Property




    Public Property CommentId() As Guid
        Get
            CheckDeleted()
            If Row(DataProtectionHistoryDAL.COL_NAME_COMMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DataProtectionHistoryDAL.COL_NAME_COMMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_COMMENT_ID, Value)
        End Set
    End Property



    Public Property Status() As Guid
        Get
            CheckDeleted()
            If row(DataProtectionHistoryDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DataProtectionHistoryDAL.COL_NAME_STATUS), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_STATUS, Value)
        End Set
    End Property



    Public Property StartDate() As DateType
        Get
            CheckDeleted()
            If row(DataProtectionHistoryDAL.COL_NAME_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DataProtectionHistoryDAL.COL_NAME_START_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_START_DATE, Value)
        End Set
    End Property



    Public Property EndDate() As DateType
        Get
            CheckDeleted()
            If row(DataProtectionHistoryDAL.COL_NAME_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DataProtectionHistoryDAL.COL_NAME_END_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_END_DATE, Value)
        End Set
    End Property

    Public Property AddedBy() As String
        Get
            If Row(DataProtectionHistoryDAL.COL_NAME_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DataProtectionHistoryDAL.COL_NAME_CREATED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DataProtectionHistoryDAL.COL_NAME_CREATED_BY, Value)
        End Set

    End Property

    Public Property RestrictedStatus() As String
        Get
            Return _restrict
        End Get
        Set(ByVal Value As String)
            _restrict = Value
        End Set
    End Property

    Public Property IsRequestIdUsed() As String
        Get
            Return _isRequestIdUsed
        End Get
        Set(ByVal Value As String)
            _isRequestIdUsed = Value
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Function AddComment(ByVal CommentId As Guid) As Comment
        If (CommentId.Equals(Guid.Empty)) Then
            Dim objComment As New Comment(Dataset)
            Return objComment
        Else
            Dim objComment As New Comment(CommentId, Dataset)
            Return objComment
        End If
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DataProtectionHistoryDAL
                dal.UpdateFamily(Dataset)
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
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    #End Region

#Region " Validate Methods"

    Public Shared Function GetRequestIdUsedInfo(ByVal requestId As String, Optional ByVal restrictionCode As String = Nothing, Optional ByVal certId As Guid = Nothing) As Boolean
        Try
            Dim dal As New DataProtectionHistoryDAL
            Return dal.GetRequestIdUsedInfo(requestId, restrictionCode, certId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal certId As Guid) As DataProtectionCommentDV
        Try
            Dim dal As New DataProtectionHistoryDAL
            Return New DataProtectionCommentDV(dal.LoadList(certId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class DataProtectionCommentDV
        Inherits DataView
        Public Const COL_COMMENT_ID As String = DataProtectionHistoryDAL.COL_NAME_COMMENT_ID
        Public Const COL_ADDED_BY As String = DataProtectionHistoryDAL.COL_NAME_CREATED_BY
        Public Const COL_CREATED_DATE As String = DataProtectionHistoryDAL.COL_NAME_CREATED_DATE
        Public Const COL_REQUEST_ID As String = DataProtectionHistoryDAL.COL_NAME_REQUEST_ID
        Public Const COL_COMMENTS As String = DataProtectionHistoryDAL.COL_NAME_COMMENTS
        Public Const COL_STATUS As String = DataProtectionHistoryDAL.COL_NAME_STATUS
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Public Function AddComments(Comment_id As Guid) As Comment
        If Comment_id.Equals(Guid.Empty) Then
            Dim objComment As New Comment(Dataset)
            Return objComment
        Else
            Dim objComment As New Comment(Comment_id, Dataset)
            Return objComment
        End If
    End Function

    Public Function AddCertificate(Certificate_id As Guid) As Certificate
        If Certificate_id.Equals(Guid.Empty) Then
            Dim objCertificate As New Certificate(Dataset)
            Return objCertificate
        Else
            Dim objCertificate As New Certificate(Certificate_id, Dataset)
            Return objCertificate
        End If
    End Function
#End Region

End Class
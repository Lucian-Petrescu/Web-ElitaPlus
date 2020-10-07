'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/10/2018)  ********************

Public Class CertForgotRequest
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
            Dim dal As New CertForgotRequestDAL
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
            Dim dal As New CertForgotRequestDAL            
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
    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(CertForgotRequestDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertForgotRequestDAL.COL_NAME_CERT_FORGOT_REQUEST_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If row(CertForgotRequestDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertForgotRequestDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CertForgotRequestDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(CertForgotRequestDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertForgotRequestDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CertForgotRequestDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property CertNumber() As String
        Get
            CheckDeleted()
            If row(CertForgotRequestDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertForgotRequestDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CertForgotRequestDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=1)> _
    Public Property IsForgotten() As String
        Get
            CheckDeleted()
            If row(CertForgotRequestDAL.COL_NAME_IS_FORGOTTEN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertForgotRequestDAL.COL_NAME_IS_FORGOTTEN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CertForgotRequestDAL.COL_NAME_IS_FORGOTTEN, Value)
        End Set
    End Property
	
	
    
    Public Property DeletedDate() As DateType
        Get
            CheckDeleted()
            If row(CertForgotRequestDAL.COL_NAME_DELETED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertForgotRequestDAL.COL_NAME_DELETED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(CertForgotRequestDAL.COL_NAME_DELETED_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property RequestId() As String
        Get
            CheckDeleted()
            If row(CertForgotRequestDAL.COL_NAME_REQUEST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertForgotRequestDAL.COL_NAME_REQUEST_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CertForgotRequestDAL.COL_NAME_REQUEST_ID, Value)
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

    Public Function AddCertificate(Certificate_id As Guid) As Certificate
        If Certificate_id.Equals(Guid.Empty) Then
            Dim objCertificate As New Certificate(Dataset)
            Return objCertificate
        Else
            Dim objCertificate As New Certificate(Certificate_id, Dataset)
            Return objCertificate
        End If
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertForgotRequestDAL
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

#Region "DataView Retrieveing Methods"

#End Region

End Class


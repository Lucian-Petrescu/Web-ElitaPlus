'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/10/2018)  ********************

Public Class CertForgotRequest
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
            Dim dal As New CertForgotRequestDAL
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
            Dim dal As New CertForgotRequestDAL            
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
            Me.SetValue(CertForgotRequestDAL.COL_NAME_CERT_ID, Value)
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
            Me.SetValue(CertForgotRequestDAL.COL_NAME_DEALER_ID, Value)
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
            Me.SetValue(CertForgotRequestDAL.COL_NAME_CERT_NUMBER, Value)
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
            Me.SetValue(CertForgotRequestDAL.COL_NAME_IS_FORGOTTEN, Value)
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
            Me.SetValue(CertForgotRequestDAL.COL_NAME_DELETED_DATE, Value)
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
            Me.SetValue(CertForgotRequestDAL.COL_NAME_REQUEST_ID, Value)
        End Set
    End Property


#End Region

#Region "Public Members"

    Public Function AddComment(ByVal CommentId As Guid) As Comment
        If (CommentId.Equals(Guid.Empty)) Then
            Dim objComment As New Comment(Me.Dataset)
            Return objComment
        Else
            Dim objComment As New Comment(CommentId, Me.Dataset)
            Return objComment
        End If
    End Function

    Public Function AddCertificate(Certificate_id As Guid) As Certificate
        If Certificate_id.Equals(Guid.Empty) Then
            Dim objCertificate As New Certificate(Me.Dataset)
            Return objCertificate
        Else
            Dim objCertificate As New Certificate(Certificate_id, Me.Dataset)
            Return objCertificate
        End If
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertForgotRequestDAL
                dal.UpdateFamily(Me.Dataset)
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
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/10/2017)  ********************

Public Class CustCommunication
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
            Dim dal As New CustCommunicationDAL
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
            Dim dal As New CustCommunicationDAL
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
            If Row(CustCommunicationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustCommunicationDAL.COL_NAME_CUST_COMMUNICATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property EntityName() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_ENTITY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_ENTITY_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_ENTITY_NAME, Value)
        End Set
    End Property



    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustCommunicationDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property Direction() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_DIRECTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_DIRECTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_DIRECTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property CommunicationChannel() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_CHANNEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_CHANNEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_COMMUNICATION_CHANNEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property CommunicationFormat() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_FORMAT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_FORMAT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_COMMUNICATION_FORMAT, Value)
        End Set
    End Property



    Public Property CustContactId() As Guid
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_CUST_CONTACT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustCommunicationDAL.COL_NAME_CUST_CONTACT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_CUST_CONTACT_ID, Value)
        End Set
    End Property


    Public Property CustomerId() As Guid
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_CUSTOMER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustCommunicationDAL.COL_NAME_CUSTOMER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_CUSTOMER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property CommunicationComponent() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_COMPONENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_COMPONENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_COMMUNICATION_COMPONENT, Value)
        End Set
    End Property



    Public Property ComponentRefId() As Guid
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_COMPONENT_REF_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustCommunicationDAL.COL_NAME_COMPONENT_REF_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_COMPONENT_REF_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property CommunicationStatus() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_COMMUNICATION_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_COMMUNICATION_STATUS, Value)
        End Set
    End Property



    Public Property CommResponseId() As Guid
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_COMM_RESPONSE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustCommunicationDAL.COL_NAME_COMM_RESPONSE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_COMM_RESPONSE_ID, Value)
        End Set
    End Property



    Public Property CommResponseXml() As Object
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_COMM_RESPONSE_XML) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_COMM_RESPONSE_XML), Object)
            End If
        End Get
        Set(ByVal Value As Object)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_COMM_RESPONSE_XML, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)>
    Public Property IsRetryable() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_IS_RETRYABLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_IS_RETRYABLE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_IS_RETRYABLE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property RetryCompoReference() As String
        Get
            CheckDeleted()
            If Row(CustCommunicationDAL.COL_NAME_RETRY_COMPO_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustCommunicationDAL.COL_NAME_RETRY_COMPO_REFERENCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CustCommunicationDAL.COL_NAME_RETRY_COMPO_REFERENCE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CustCommunicationDAL
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

#End Region

End Class



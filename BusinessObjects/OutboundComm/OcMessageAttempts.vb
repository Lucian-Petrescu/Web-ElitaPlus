Public Class OcMessageAttempts
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
            Dim dal As New OcMessageAttemptsDAL
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
            Dim dal As New OcMessageAttemptsDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(OcMessageAttemptsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcMessageAttemptsDAL.COL_NAME_OC_MESSAGE_ATTEMPS_ID), Byte()))
            End If
        End Get
    End Property

    Public Property OcMessageId As Guid
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_OC_MESSAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcMessageAttemptsDAL.COL_NAME_OC_MESSAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_OC_MESSAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=600)>
    Public Property RecipientAddress As String
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_RECIPIENT_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageAttemptsDAL.COL_NAME_RECIPIENT_ADDRESS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_RECIPIENT_ADDRESS, Value)
        End Set
    End Property

    Public Property ProcessStatusXCD As String
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_PROCESS_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageAttemptsDAL.COL_NAME_PROCESS_STATUS_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_PROCESS_STATUS_XCD, Value)
        End Set
    End Property

    Public Property ProcessStatusDescription As String
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_PROCESS_STATUS_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageAttemptsDAL.COL_NAME_PROCESS_STATUS_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_PROCESS_STATUS_DESCRIPTION, Value)
        End Set
    End Property

    Public Property MessageAttemptedOn As DateTime
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageAttemptsDAL.COL_NAME_CREATED_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_CREATED_DATE, Value)
        End Set
    End Property

    Public Property MessageAttemptedBy As String
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageAttemptsDAL.COL_NAME_CREATED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_CREATED_BY, Value)
        End Set
    End Property

    Public Property RecipientDescription As String
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_RECIPIENT_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageAttemptsDAL.COL_NAME_RECIPIENT_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_RECIPIENT_DESCRIPTION, Value)
        End Set
    End Property

    Public Property MessageError As String
        Get
            CheckDeleted()
            If Row(OcMessageAttemptsDAL.COL_NAME_ERR_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageAttemptsDAL.COL_NAME_ERR_MESSAGE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageAttemptsDAL.COL_NAME_ERR_MESSAGE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Sub SaveNewMsgAttempt(ByVal messageId As Guid, ByVal recipientAddress As String, ByVal description As String, ByVal senderReason As String, ByRef returnCode As Integer, ByRef returnMessage As String)
        Dim dal As New OcMessageAttemptsDAL
        dal.AddMessageAttempt(messageId, recipientAddress, description, senderReason, returnCode, returnMessage)
    End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(ByVal ocMessageId As Guid) As DataView
        Try
            Dim dal As New OcMessageAttemptsDAL
            Return New DataView(dal.LoadList(ocMessageId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "MessageAttemptsDV"
    Public Class MessageAttemptsDV
        Inherits DataView

#Region "Constants"
        Public Const COL_OC_MESSAGE_ATTEMPS_ID As String = "OC_MESSAGE_ATTEMPS_ID"
        Public Const COL_OC_MESSAGE_ID As String = "OC_MESSAGE_ID"
        Public Const COL_RECIPIENT_ADDRESS As String = "RECIPIENT_ADDRESS"
        Public Const COL_PROCESS_STATUS_XCD As String = "PROCESS_STATUS_XCD"
        Public Const COL_PROCESS_STATUS_DESCRIPTION As String = "PROCESS_STATUS_DESCRIPTION"
        Public Const COL_MESSAGE_ATTEMPTED_ON As String = "MESSAGE_ATTEMPTED_ON"
        Public Const COL_MESSAGE_ATTEMPTED_BY As String = "MESSAGE_ATTEMPTED_BY"
        Public Const COL_RECIPIENT_DESCRIPTION As String = "RECIPIENT_DESCRIPTION"
        Public Const COL_MESSAGE_ERROR As String = "ERR_MESSAGE"
        Public Const COL_CREATED_BY As String = "CREATED_BY"
        Public Const COL_CREATED_DATE As String = "CREATED_DATE"
        Public Const COL_MODIFIED_BY As String = "MODIFIED_BY"
        Public Const COL_MODIFIED_DATE As String = "MODIFIED_DATE"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As MessageAttemptsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(MessageAttemptsDV.COL_OC_MESSAGE_ATTEMPS_ID) = (New Guid()).ToByteArray
            row(MessageAttemptsDV.COL_OC_MESSAGE_ID) = Guid.Empty.ToByteArray
            row(MessageAttemptsDV.COL_RECIPIENT_ADDRESS) = DBNull.Value
            row(MessageAttemptsDV.COL_PROCESS_STATUS_XCD) = DBNull.Value
            row(MessageAttemptsDV.COL_PROCESS_STATUS_DESCRIPTION) = DBNull.Value
            row(MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_ON) = DBNull.Value
            row(MessageAttemptsDV.COL_MESSAGE_ATTEMPTED_BY) = DBNull.Value
            row(MessageAttemptsDV.COL_RECIPIENT_DESCRIPTION) = DBNull.Value
            row(MessageAttemptsDV.COL_MESSAGE_ERROR) = DBNull.Value
            row(MessageAttemptsDV.COL_MODIFIED_BY) = DBNull.Value
            row(MessageAttemptsDV.COL_MODIFIED_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New MessageAttemptsDV(dt)
        End Function

    End Class
#End Region
#End Region

End Class

Imports Assurant.ElitaPlus.BusinessObjectsNew

Public Class OcMessage
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
            Dim dal As New OcMessageDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New OcMessageDAL
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
    Private Sub Initialize()
    End Sub

#End Region

#Region "Properties"
    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(OcMessageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcMessageDAL.COL_NAME_OC_MESSAGE_ID), Byte()))
            End If
        End Get
    End Property

    Public Property OcTemplateId() As Guid
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_OC_TEMPLATE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcMessageDAL.COL_NAME_OC_TEMPLATE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_OC_TEMPLATE_ID, Value)
        End Set
    End Property

    Public Property TemplateCode() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_TEMPLATE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_TEMPLATE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_TEMPLATE_CODE, Value)
        End Set
    End Property

    Public Property TemplateDescription() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_TEMPLATE_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_TEMPLATE_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_TEMPLATE_DESCRIPTION, Value)
        End Set
    End Property

    Public Property SenderReason() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_SENDER_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_SENDER_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_SENDER_REASON, Value)
        End Set
    End Property

    Public Property RecipientAddress() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_RECIPIENT_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_RECIPIENT_ADDRESS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_RECIPIENT_ADDRESS, Value)
        End Set
    End Property

    Public Property LastAttemptedOn() As DateTime
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_LAST_ATTEMPTED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_LAST_ATTEMPTED_ON), String)
            End If
        End Get
        Set(ByVal Value As DateTime)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_LAST_ATTEMPTED_ON, Value)
        End Set
    End Property

    Public Property LastAttemptedStatus() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_LAST_ATTEMPTED_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_LAST_ATTEMPTED_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_LAST_ATTEMPTED_STATUS, Value)
        End Set
    End Property

    Public Property CertificateNumber() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property CaseNumber() As String
        Get
            CheckDeleted()
            If Row(OcMessageDAL.COL_NAME_CASE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageDAL.COL_NAME_CASE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(OcMessageDAL.COL_NAME_CASE_NUMBER, Value)
        End Set
    End Property

    Public ReadOnly Property MessageParametersList() As OcMessageParamsList
        Get
            Return New OcMessageParamsList(Me)
        End Get
    End Property

    Public ReadOnly Property MessageAttemptsList() As OcMessageAttemptsList
        Get
            Return New OcMessageAttemptsList(Me)
        End Get
    End Property

#End Region

#Region "Public Members"

    Public Function GetParameterChild(ByVal childId As Guid) As OcMessageParams
        Return Me.MessageParametersList.Find(childId)
    End Function

    Public Function GetNewParameterChild() As OcMessageParams
        Dim child As OcMessageParams = Me.MessageParametersList.GetNewChild
        child.OcMessageId = Me.Id
        Return child
    End Function

    Public Function GetMessageAttemptChild(ByVal childId As Guid) As OcMessageAttempts
        Return Me.MessageAttemptsList.Find(childId)
    End Function

    Public Function GetNewMessageAttemptChild() As OcMessageAttempts
        Dim child As OcMessageAttempts = Me.MessageAttemptsList.GetNewChild
        child.OcMessageId = Me.Id
        Return child
    End Function

    Public Sub SendAdhocMessage(ByVal dealer_id As Guid,
                                ByVal msg_for As String,
                                ByVal id As Guid,
                                ByVal template_code As String,
                                ByVal std_recipient As String,
                                ByVal cst_recipient As String,
                                ByVal std_parameter As String,
                                ByVal cst_parameter As String,
                                ByVal sender As String,
                                ByRef message_id As Guid,
                                ByRef err_no As Integer,
                                ByRef err_msg As String)

        Try
            Dim dal As New OcMessageDAL
            dal.SendAdhocMessage(dealer_id, msg_for, id, template_code, std_recipient, cst_recipient, std_parameter, cst_parameter, sender,
                                 message_id, err_no, err_msg)
        Catch ex As Exception
            Throw New Exception()
        End Try

    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#Region "TemplateSearchDV"
    Public Class MessageSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_MESSAGE_ID As String = "OC_MESSAGE_ID"
        Public Const COL_TEMPLATE_ID As String = "OC_TEMPLATE_ID"
        Public Const COL_CERTIFICATE_ID As String = "CERT_ID"
        Public Const COL_CLAIM_ID As String = "CLAIM_ID"
        Public Const COL_CASE_ID As String = "CASE_ID"

        Public Const COL_TEMPLATE_DESCRIPTION As String = "TEMPLATE_DESCRIPTION"
        Public Const COL_SENDER_REASON As String = "SENDER_REASON"
        Public Const COL_RECIPIENT_ADDRESS As String = "RECIPIENT_ADDRESS"
        Public Const COL_LAST_ATTEMPTED_ON As String = "LAST_ATTEMPTED_ON"
        Public Const COL_LAST_ATTEMPTED_STATUS As String = "LAST_ATTEMPTED_STATUS"

        Public Const COL_CERT_NUMBER As String = "CERTIFICATE_NUMBER"
        Public Const COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
        Public Const COL_CASE_NUMBER As String = "CASE_NUMBER"

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
    End Class

    Shared Function GetMessageSearchDV(ByVal dealerId As Guid,
                                       ByVal searchBy As String,
                                       ByVal conditionMask As String,
                                       ByVal languageId As Guid) As MessageSearchDV
        Try
            Dim dal As New OcMessageDAL
            Dim ds As DataSet = dal.LoadList(dealerId, searchBy, conditionMask, languageId)
            If ds.Tables.Count > 0 Then
                Return New MessageSearchDV(ds.Tables(0))
            Else
                Return New MessageSearchDV()
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "MessageDV"
    Public Class MessageDV
        Inherits DataView

#Region "Constants"
        Public Const COL_MESSAGE_ID As String = "OC_MESSAGE_ID"
        Public Const COL_TEMPLATE_ID As String = "OC_TEMPLATE_ID"
        Public Const COL_TEMPLATE_CODE As String = "TEMPLATE_CODE"
        Public Const COL_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_SENDER_REASON As String = "SENDER_REASON"
        Public Const COL_RECIPIENT_ADDRESS As String = "RECIPIENT_ADDRESS"
        Public Const COL_MESSAGE_CREATED_ON As String = "MESSAGE_CREATED_ON"
        Public Const COL_CERTIFICATE_NUMBER As String = "CERTIFICATE_NUMBER"
        Public Const COL_CERT_ID As String = "CERT_ID"
        Public Const COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
        Public Const COL_CLAIM_ID As String = "CLAIM_ID"
        Public Const COL_CASE_NUMBER As String = "CASE_NUMBER"
        Public Const COL_CASE_ID As String = "CASE_ID"
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

        Public Function AddNewRowToEmptyDV() As MessageDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(MessageDV.COL_MESSAGE_ID) = (New Guid()).ToByteArray
            row(MessageDV.COL_TEMPLATE_ID) = Guid.Empty.ToByteArray
            row(MessageDV.COL_TEMPLATE_CODE) = DBNull.Value
            row(MessageDV.COL_DESCRIPTION) = DBNull.Value
            row(MessageDV.COL_SENDER_REASON) = DBNull.Value
            row(MessageDV.COL_RECIPIENT_ADDRESS) = DBNull.Value
            row(MessageDV.COL_CERTIFICATE_NUMBER) = DBNull.Value
            row(MessageDV.COL_CERT_ID) = DBNull.Value
            row(MessageDV.COL_CLAIM_NUMBER) = DBNull.Value
            row(MessageDV.COL_CLAIM_ID) = DBNull.Value
            row(MessageDV.COL_CASE_NUMBER) = DBNull.Value
            row(MessageDV.COL_CASE_ID) = DBNull.Value
            row(MessageDV.COL_CREATED_BY) = DBNull.Value
            row(MessageDV.COL_CREATED_DATE) = DBNull.Value
            row(MessageDV.COL_MODIFIED_BY) = DBNull.Value
            row(MessageDV.COL_MODIFIED_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New MessageDV(dt)
        End Function
    End Class
#End Region

#End Region

End Class

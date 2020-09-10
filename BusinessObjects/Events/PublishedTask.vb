'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/4/2013)  ********************

Imports Assurant.ElitaPlus.BusinessObjectsNew.Codes
Imports System.Text

Public Class PublishedTask
    Inherits BusinessObjectBase

    Public Shared ReadOnly Property TaskStatusFailedId As Guid
        Get
            LookupListNew.GetIdFromCode(LookupListNew.LK_TASK_STATUS, TASK_STATUS__FAILED)
        End Get
    End Property

    Public Shared ReadOnly Property TaskStatusOpenId As Guid
        Get
            LookupListNew.GetIdFromCode(LookupListNew.LK_TASK_STATUS, TASK_STATUS__OPEN)
        End Get
    End Property

    Public Shared ReadOnly Property TaskStatusInProgress As Guid
        Get
            LookupListNew.GetIdFromCode(LookupListNew.LK_TASK_STATUS, TASK_STATUS__IN_PROGRESS)
        End Get
    End Property


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
            Dim dal As New PublishedTaskDAL
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
            Dim dal As New PublishedTaskDAL
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
    Private _syncRoot As New Object
    Private _taskArguments As KeyValueDictionary
    Public Const REGISTRATION_ID As String = "RegistrationId"
    Public Const REGISTRATION_ITEM_ID As String = "RegistrationItemId"
    Public Const CERTIFICATE_ID As String = "CertificateId"
    Public Const CERT_ITEM_ID As String = "CertificateItemId"
    Public Const OLD_PHONE_NUMBER As String = "OldPhoneNumber"
    Public Const NEW_PHONE_NUMBER As String = "NewPhoneNumber"
    Public Const CLAIMLOAD_FILE_PROCESSED_ID As String = "ClaimLoadFileProcessedId"
    Public Const FROM_EMAIL_ADDRESS As String = "FromEmailAddress"
    Public Const CLAIM_ID As String = "ClaimId"
    Public Const CLAIM_AUTHORIZATION_ID As String = "ClaimAuthorizationId"
    Public Const SMTP_SERVER_ADDRESS As String = "SmtpServerAddress"
    Public Const CLAIM_NUMBER As String = "ClaimNumber"
    Public Const GiftCardAmount As String = "Amount"
    Public Const GiftCardType As String = "GiftCardType"
    Public Const CERT_CANCEL_REQUEST_ID As String = "CertCancelRequestID"
    Public Const Request_Action As String = "RequestAction"
    Public Const Action_Code As String = "ActionTypeCode"
    Public Const SEND_WELCOME_EMAIL_RESP_ID = "CommWeResponseId"
    Public Const SEND_ENR_REWARD_EMAIL_RESP_ID = "CommRewResponseId"
    Public Const SEND_CLM_GIFTCARD_EMAIL_RESP_ID = "CommGifResponseId"
    Public const GIFT_CARD_REQUEST_ID = "GiftCardRequestId"


    Public Const DEALER_CODE As String = "DealerCode"
    Public Const DESTINATION As String = "Destination"
    Public Const REP_IMEI_NO As String = "ReplacementIMEINumber"
    Public Const IMEI_NO As String = "IMEINumber"
    Public Const CERT_NO As String = "CertificateNumber"

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(PublishedTaskDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_PUBLISHED_TASK_ID), Byte()))
            End If
        End Get
    End Property


    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property



    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property



    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property



    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(PublishedTaskDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PublishedTaskDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PublishedTaskDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property



    Public Property EventTypeId() As Guid
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_EVENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_EVENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_EVENT_TYPE_ID, Value)
        End Set
    End Property



    Public Property EventDate() As DateType
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_EVENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(PublishedTaskDAL.COL_NAME_EVENT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_EVENT_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1020)>
    Public Property Sender() As String
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_SENDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PublishedTaskDAL.COL_NAME_SENDER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_SENDER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)>
    Public Property Arguments() As String
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_ARGUMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PublishedTaskDAL.COL_NAME_ARGUMENTS), String)
            End If
        End Get
        Private Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_ARGUMENTS, Value)
        End Set
    End Property

    Default Public ReadOnly Property Argument(ByVal key As String) As String
        Get
            Dim returnValue As String = String.Empty
            If (_taskArguments Is Nothing) Then
                SyncLock (_syncRoot)
                    If (_taskArguments Is Nothing) Then
                        _taskArguments = New KeyValueDictionary(Me.Arguments)
                    End If
                End SyncLock
            End If
            If (_taskArguments.ContainsKey(key)) Then
                returnValue = _taskArguments(key)
            End If
            Return returnValue
        End Get
    End Property


    <ValueMandatory("")>
    Public Property TaskId() As Guid
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_TASK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_TASK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_TASK_ID, Value)
        End Set
    End Property



    Public Property LockDate() As DateType
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_LOCK_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(PublishedTaskDAL.COL_NAME_LOCK_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_LOCK_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property TaskStatusId() As Guid
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_TASK_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PublishedTaskDAL.COL_NAME_TASK_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_TASK_STATUS_ID, Value)
        End Set
    End Property



    Public Property RetryCount() As LongType
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_RETRY_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(PublishedTaskDAL.COL_NAME_RETRY_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_RETRY_COUNT, Value)
        End Set
    End Property



    Public Property LastAttemptDate() As DateType
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_LAST_ATTEMPT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(PublishedTaskDAL.COL_NAME_LAST_ATTEMPT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_LAST_ATTEMPT_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property MachineName() As String
        Get
            CheckDeleted()
            If row(PublishedTaskDAL.COL_NAME_MACHINE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PublishedTaskDAL.COL_NAME_MACHINE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_MACHINE_NAME, Value)
        End Set
    End Property

    Public ReadOnly Property Task As Task
        Get
            If (Me.TaskId.Equals(Guid.Empty)) Then
                Return Nothing
            Else
                Return New Task(Me.TaskId, Me.Dataset)
            End If
        End Get
    End Property

    Public Property DealerGroupId As Guid
        Get
            CheckDeleted()
            If Row(PublishedTaskDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PublishedTaskDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set(value As Guid)
            CheckDeleted()
            Me.SetValue(PublishedTaskDAL.COL_NAME_DEALER_GROUP_ID, value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New PublishedTaskDAL
                dal.Update(Me.Row)
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
        End Try
    End Sub

    Public Shared Function GetList(
          ByVal companyId As Guid _
        , ByVal dealerId As Guid _
        , ByVal countryId As Guid _
        , ByVal product As String _
        , ByVal coverageTypeId As Guid _
        , ByVal eventTypeId As Guid _
        , ByVal task As String _
        , ByVal statusId As Guid _
        , ByVal sender As String _
        , ByVal arguments As String _
        , ByVal machineName As String _
        , ByVal startDate As String _
        , ByVal endDate As String _
        , Optional ByVal LimitResultset As Integer = PublishedTaskDAL.MAX_NUMBER_OF_ROWS) As PublishedTask.PublishedTaskSearchDV

        Try
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim LanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup().Id
            Dim dal As New PublishedTaskDAL

            Return New PublishedTaskSearchDV(
                dal.LoadList(LanguageId _
                            , companyGroupId _
                            , companyId _
                            , dealerId _
                            , countryId _
                            , product _
                            , coverageTypeId _
                            , eventTypeId _
                            , task _
                            , statusId _
                            , sender _
                            , arguments _
                            , machineName _
                            , startDate _
                            , endDate _
                            , LimitResultset).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Action Methods"

    Public Shared Sub AddEvent(ByVal companyGroupId As Guid,
                               ByVal companyId As Guid,
                               ByVal countryId As Guid,
                               ByVal dealerId As Guid,
                               ByVal productCode As String,
                               ByVal coverageTypeId As Guid,
                               ByVal sender As String,
                               ByVal arguments As String,
                               ByVal eventDate As DateTime,
                               ByVal eventTypeId As Guid,
                               ByVal eventArgumentId As Guid,
                               Optional ByVal dealergroupId As Guid = Nothing)
        Try
            Dim dal As New PublishedTaskDAL
            dal.AddEvent(companyGroupId,
                         companyId,
                         countryId,
                         dealerId,
                         productCode,
                         coverageTypeId,
                         sender,
                         arguments,
                         eventDate,
                         eventTypeId,
                         eventArgumentId,
                         dealergroupId
                         )
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Function GetNextTask(ByVal subscriberId As Guid, ByVal machineName As String, ByVal processThreadName As String) As PublishedTask

        Dim p_task_id As Guid
        Dim p_task As PublishedTask
        Try

            Dim dal As New PublishedTaskDAL
            p_task_id = dal.GetNextTaskId(subscriberId, machineName, processThreadName)

            If (p_task_id <> Nothing) Then
                p_task = New PublishedTask(p_task_id)
            End If
            Return p_task
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Logger.AddError(ex)
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
    End Function

    Public Sub CompleteTask(ByVal machineName As String, ByVal processThreadName As String)
        Try
            Dim dal As New PublishedTaskDAL
            dal.CompleteTask(Me.Id, machineName, processThreadName)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Sub FailedTask(ByVal machineName As String, ByVal processThreadName As String, ByVal failReason As String)
        Try
            Dim dal As New PublishedTaskDAL
            dal.FailedTask(Me.Id, machineName, processThreadName, failReason)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub ResetTask(ByVal publishedTaskId As Guid)
        Try
            Dim dal As New PublishedTaskDAL
            dal.ResetTask(publishedTaskId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub DeleteTask(ByVal publishedTaskId As Guid)
        Try
            Dim dal As New PublishedTaskDAL
            dal.DeleteTask(publishedTaskId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub GetOutBoundMessageDetails(ByVal publishedTaskId As Guid, ByRef oErrCode As Integer, ByRef oErrMsg As String,
                                                ByRef oMessageId As Guid, ByRef oTemplateCode As String, ByRef oWhiteList As String,
                                                ByRef oTemplateUserName As String, ByRef oTemplatePassword As String,
                                                ByRef oRecipients As System.Collections.Generic.List(Of String),
                                                ByRef oTemplateParams As System.Collections.Generic.Dictionary(Of String, String))
        Dim dal As New PublishedTaskDAL
        dal.GetOutBoundMessageDetails(publishedTaskId, oErrCode, oErrMsg, oMessageId, oTemplateCode, oWhiteList, oTemplateUserName, oTemplatePassword, oRecipients, oTemplateParams)
    End Sub
    Public Shared Function UpdateOutBoundMessageStatus(guidMsgId As Guid, strRecipient As String, strProcessStatus As String,
                                                  strCommReferenceId As String, strErrMsg As String, strProcessComments As String) As Integer
        Dim dal As New PublishedTaskDAL, oErrCode As Integer = 0
        dal.UpdateOutBoundMessageStatus(guidMsgId, strRecipient, strProcessStatus, strCommReferenceId, strErrMsg, strProcessComments, oErrCode)
        Return oErrCode
    End Function

    Public Shared Function UpdateResendMessageStatus(guidMsgRecipientId As Guid, strProcessStatus As String,
                                                     strErrMsg As String, strProcessComments As String) As Integer
        Dim dal As New PublishedTaskDAL, oErrCode As Integer = 0
        dal.UpdateResendMessageStatus(guidMsgRecipientId, strProcessStatus, strErrMsg, strProcessComments, oErrCode)
        Return oErrCode
    End Function
    Public Shared Sub CheckSLAClaimStatus(ClaimId As Guid, ByRef oErrCode As Integer, ByRef strErrMsg As String)
        Dim dal As New PublishedTaskDAL
        dal.CheckSLAClaimStatus(ClaimId, oErrCode, strErrMsg)
    End Sub
#End Region

#Region "PublishedTaskSearchDV"
    Public Class PublishedTaskSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PUBLISHED_TASK_ID As String = PublishedTaskDAL.COL_NAME_PUBLISHED_TASK_ID
        Public Const COL_NAME_COMPANY_GROUP As String = "COMPANY_GROUP"
        Public Const COL_NAME_COMPANY As String = "COMPANY"
        Public Const COL_NAME_COUNTRY As String = "COUNTRY"
        Public Const COL_NAME_DEALER As String = "DEALER"
        Public Const COL_NAME_PRODUCT_CODE As String = PublishedTaskDAL.COL_NAME_PRODUCT_CODE
        Public Const COL_NAME_COVERAGE_TYPE As String = "COVERAGE_TYPE"
        Public Const COL_NAME_EVENT_TYPE As String = "EVENT_TYPE"
        Public Const COL_NAME_EVENT_DATE As String = PublishedTaskDAL.COL_NAME_EVENT_DATE
        Public Const COL_NAME_SENDER As String = PublishedTaskDAL.COL_NAME_SENDER
        Public Const COL_NAME_TASK_STATUS As String = "TASK_STATUS"
        Public Const COL_NAME_ARGUMENTS As String = PublishedTaskDAL.COL_NAME_ARGUMENTS
        Public Const COL_NAME_TASK As String = "TASK"
        Public Const COL_NAME_LOCK_DATE As String = PublishedTaskDAL.COL_NAME_LOCK_DATE
        Public Const COL_NAME_RETRY_COUNT As String = PublishedTaskDAL.COL_NAME_RETRY_COUNT
        Public Const COL_NAME_LAST_ATTEMPT_DATE As String = PublishedTaskDAL.COL_NAME_LAST_ATTEMPT_DATE
        Public Const COL_NAME_MACHINE_NAME As String = PublishedTaskDAL.COL_NAME_MACHINE_NAME
        Public Const COL_NAME_FAIL_REASON As String = PublishedTaskDAL.COL_NAME_FAIL_REASON
        Public Const COL_NAME_TASK_STATUS_CODE As String = "TASK_STATUS_CODE"
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property PublishedTaskId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_PUBLISHED_TASK_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CompanyGroup(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_COMPANY_GROUP).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Company(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_COMPANY).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Country(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_COUNTRY).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Dealer(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DEALER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ProductCode(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_PRODUCT_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CoverageType(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_COVERAGE_TYPE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property EventType(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EVENT_TYPE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property EventDate(ByVal row As DataRow) As DateType
            Get
                If row(COL_NAME_EVENT_DATE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Nullable(Of Date)(CType(row(COL_NAME_EVENT_DATE), Date))

                    '                    Return CType(row(COL_NAME_EVENT_DATE), Date)
                End If
            End Get
        End Property

        Public Shared ReadOnly Property Sender(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_SENDER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property TaskStatus(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_TASK_STATUS).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Arguments(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_ARGUMENTS).ToString().Replace(";", "<br />").Replace(":", " = ")
            End Get
        End Property

        Public Shared ReadOnly Property Task(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_TASK).ToString
            End Get
        End Property

        Public Shared ReadOnly Property LockDate(ByVal row As DataRow) As DateType
            Get
                If row(COL_NAME_LOCK_DATE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Nullable(Of Date)(CType(row(COL_NAME_LOCK_DATE), Date))
                End If
            End Get
        End Property

        Public Shared ReadOnly Property RetryCount(ByVal row As DataRow) As Nullable(Of Integer)
            Get
                If row(COL_NAME_RETRY_COUNT) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Nullable(Of Integer)(CType(row(COL_NAME_RETRY_COUNT), Integer))
                End If
            End Get
        End Property

        Public Shared ReadOnly Property LastAttemptDate(ByVal row As DataRow) As Nullable(Of Date)
            Get
                If row(COL_NAME_LAST_ATTEMPT_DATE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Nullable(Of Date)(CType(row(COL_NAME_LAST_ATTEMPT_DATE), Date))
                End If
            End Get
        End Property

        Public Shared ReadOnly Property MachineName(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_MACHINE_NAME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property TaskStatusCode(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_TASK_STATUS_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property FailReason(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_FAIL_REASON).ToString
            End Get
        End Property
    End Class
#End Region

#Region "GiftCard Info"
    Public Shared Sub InsertGiftCardInfo(giftCardNumber As String, serialNumber As String,
                                              securityCode1 As String, securityCode2 As String, expirationDate As Date,
                                              giftCardRequestId As Guid, encryptedGiftCardLink As String)
        Dim dal As New PublishedTaskDAL, oErrCode As Integer = 0
        dal.InsertGiftCardInfo(giftCardNumber, serialNumber, securityCode1, securityCode2, expirationDate, giftCardRequestId, encryptedGiftCardLink)

    End Sub

    Public Shared sub UpdateGiftCardStatus(giftCardRequestId As guid, status as string)

        Dim dal As New PublishedTaskDAL
        dal.UpdateGiftCardStatus(giftCardRequestId, status)
        
    End sub
#End Region

End Class



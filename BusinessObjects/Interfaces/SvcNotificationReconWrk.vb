'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/4/2008)  ********************

Public Class SvcNotificationReconWrk
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const COL_NAME_SVC_NOTIFICATION_RECON_WRK_ID As String = "svc_notification_recon_wrk_id"
    Public Const COL_NAME_SVC_NOTIFICATION_PROCESSED_ID As String = "svc_notification_processed_id"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CLAIM_LOADED As String = "claim_loaded"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_SVC_NOTIFICATION_NUMBER As String = "svc_notification_number"
    Public Const COL_NAME_SVC_NOTIFICATION_TYPE As String = "svc_notification_type"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CREATED_ON As String = "created_on"
    Public Const COL_NAME_CHANGED_ON As String = "changed_on"
    Public Const COL_NAME_REQUIRED_START_DATE As String = "required_start_date"
    Public Const COL_NAME_REQUIRED_START_TIME As String = "required_start_time"
    Public Const COL_NAME_REQUIRED_END_DATE As String = "required_end_date"
    Public Const COL_NAME_REQUESTED_END_TIME As String = "requested_end_time"
    Public Const COL_NAME_ARTICLE_NUMBER As String = "article_number"
    Public Const COL_NAME_CUST_ACCT_NUMBER As String = "cust_acct_number"
    Public Const COL_NAME_CUST_NAME_1 As String = "cust_name_1"
    Public Const COL_NAME_CUST_NAME_2 As String = "cust_name_2"
    Public Const COL_NAME_CUST_CITY As String = "cust_city"
    Public Const COL_NAME_CUST_POSTAL_CODE As String = "cust_postal_code"
    Public Const COL_NAME_CUST_REGION As String = "cust_region"
    Public Const COL_NAME_CUST_ADDRESS As String = "cust_address"
    Public Const COL_NAME_CUST_PHONE_NUMBER As String = "cust_phone_number"
    Public Const COL_NAME_CUST_FAX_NUMBER As String = "cust_fax_number"
    Public Const COL_NAME_EQUIPMENT As String = "equipment"
    Public Const COL_NAME_MFG_NAME As String = "mfg_name"
    Public Const COL_NAME_MODEL_NUMBER As String = "model_number"
    Public Const COL_NAME_MFG_PART_NUMBER As String = "mfg_part_number"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_SVC_NOTIFICATION_STATUS As String = "svc_notification_status"
    Public Const COL_NAME_SEQ_TASK_NUMBER As String = "seq_task_number"
    Public Const COL_NAME_SEQ_TASK_DESCRIPTION As String = "seq_task_description"
    Public Const COL_NAME_CONSECUTIVE_ACTIVITY_NUMBER As String = "consecutive_activity_number"
    Public Const COL_NAME_ACTIVITY_TEXT As String = "activity_text"
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = "problem_description"
    Public Const COL_NAME_SITE As String = "site"
    Public Const COL_NAME_PRP_COD_AMT As String = "prp_cod_amt"
    Public Const COL_NAME_OP_INDICATOR As String = "op_indicator"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_by"
    Public Const COL_NAME_TRANSACTION_NUMBER As String = "transaction_number"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        ' Me.VerifyConcurrency(sModifiedDate)
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
            Dim dal As New SvcNotificationReconWrkDAL
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
#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal NotificationfileProcessedID As Guid) As DataView
        Try
            Dim dal As New SvcNotificationReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(NotificationfileProcessedID)
            Return (ds.Tables(SvcNotificationReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


#End Region

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New SvcNotificationReconWrkDAL
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
            If Row(SvcNotificationReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property SvcNotificationProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_PROCESSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3)> _
    Public Property RejectCode() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)> _
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property ClaimLoaded() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CLAIM_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CLAIM_LOADED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CLAIM_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3000)> _
    Public Property EntireRecord() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_ENTIRE_RECORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_ENTIRE_RECORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_ENTIRE_RECORD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=12)> _
    Public Property SvcNotificationNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2)> _
    Public Property SvcNotificationType() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property



    Public Property CreatedOn() As DateType
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CREATED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CREATED_ON), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CREATED_ON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=8)> _
    Public Property ChangedOn() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CHANGED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CHANGED_ON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CHANGED_ON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=32)> _
    Public Property RequiredStartDate() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_START_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_START_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=24)> _
    Public Property RequiredStartTime() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_START_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_START_TIME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_START_TIME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=32)> _
    Public Property RequiredEndDate() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_END_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_REQUIRED_END_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=24)> _
    Public Property RequestedEndTime() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_REQUESTED_END_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_REQUESTED_END_TIME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_REQUESTED_END_TIME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=18)> _
    Public Property ArticleNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_ARTICLE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_ARTICLE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_ARTICLE_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property CustAcctNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_ACCT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_ACCT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_ACCT_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=35)> _
    Public Property CustName1() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_NAME_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_NAME_1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_NAME_1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=35)> _
    Public Property CustName2() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_NAME_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_NAME_2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_NAME_2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=35)> _
    Public Property CustCity() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_CITY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=25)> _
    Public Property CustPostalCode() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_POSTAL_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3)> _
    Public Property CustRegion() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_REGION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=35)> _
    Public Property CustAddress() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_ADDRESS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_ADDRESS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)> _
    Public Property CustPhoneNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_PHONE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_PHONE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_PHONE_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=31)> _
    Public Property CustFaxNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_FAX_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CUST_FAX_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CUST_FAX_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property Equipment() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_EQUIPMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_EQUIPMENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_EQUIPMENT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property MfgName() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_MFG_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_MFG_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_MFG_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property ModelNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_MODEL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_MODEL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_MODEL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property MfgPartNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_MFG_PART_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_MFG_PART_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_MFG_PART_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property SvcNotificationStatus() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SVC_NOTIFICATION_STATUS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property SeqTaskNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SEQ_TASK_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SEQ_TASK_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SEQ_TASK_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property SeqTaskDescription() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SEQ_TASK_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SEQ_TASK_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SEQ_TASK_DESCRIPTION, Value)
        End Set
    End Property



    Public Property ConsecutiveActivityNumber() As LongType
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_CONSECUTIVE_ACTIVITY_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(SvcNotificationReconWrkDAL.COL_NAME_CONSECUTIVE_ACTIVITY_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_CONSECUTIVE_ACTIVITY_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property ActivityText() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_ACTIVITY_TEXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_ACTIVITY_TEXT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_ACTIVITY_TEXT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=500)> _
    Public Property ProblemDescription() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property Site() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_SITE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_SITE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_SITE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property PrpCodAmt() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_PRP_COD_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_PRP_COD_AMT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_PRP_COD_AMT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property OpIndicator() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_OP_INDICATOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_OP_INDICATOR), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_OP_INDICATOR, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=35)> _
    Public Property TransactionNumber() As String
        Get
            CheckDeleted()
            If Row(SvcNotificationReconWrkDAL.COL_NAME_TRANSACTION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcNotificationReconWrkDAL.COL_NAME_TRANSACTION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SvcNotificationReconWrkDAL.COL_NAME_TRANSACTION_NUMBER, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SvcNotificationReconWrkDAL
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



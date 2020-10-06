'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/26/2014)  ********************

Public Class Notification
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
            Dim dal As New NotificationDAL
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
            Dim dal As New NotificationDAL
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
            If row(NotificationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(NotificationDAL.COL_NAME_NOTIFICATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property NotificationTypeId() As Guid
        Get
            CheckDeleted()
            If row(NotificationDAL.COL_NAME_NOTIFICATION_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(NotificationDAL.COL_NAME_NOTIFICATION_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_NOTIFICATION_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property NotificationBeginDate() As DateTimeType
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_NOTIFICATION_BEGIN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(NotificationDAL.COL_NAME_NOTIFICATION_BEGIN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_NOTIFICATION_BEGIN_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property NotificationEndDate() As DateTimeType
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_NOTIFICATION_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(NotificationDAL.COL_NAME_NOTIFICATION_END_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_NOTIFICATION_END_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AudianceTypeId() As Guid
        Get
            CheckDeleted()
            If row(NotificationDAL.COL_NAME_AUDIANCE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(NotificationDAL.COL_NAME_AUDIANCE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_AUDIANCE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property NotificationName() As String
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_NOTIFICATION_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(NotificationDAL.COL_NAME_NOTIFICATION_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_NOTIFICATION_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1000)> _
    Public Property NotificationDetails() As String
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_NOTIFICATION_DETAILS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(NotificationDAL.COL_NAME_NOTIFICATION_DETAILS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_NOTIFICATION_DETAILS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property OutageBeginDate() As DateTimeType
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_OUTAGE_BEGIN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(NotificationDAL.COL_NAME_OUTAGE_BEGIN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_OUTAGE_BEGIN_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property OutageEndDate() As DateTimeType
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_OUTAGE_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(NotificationDAL.COL_NAME_OUTAGE_END_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_OUTAGE_END_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property ContactInfo() As String
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_CONTACT_INFO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(NotificationDAL.COL_NAME_CONTACT_INFO), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_CONTACT_INFO, Value)
        End Set
    End Property

    Public ReadOnly Property SerialNo() As LongType
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_SERIAL_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(NotificationDAL.COL_NAME_SERIAL_NO), Long))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property Enabled() As String
        Get
            CheckDeleted()
            If Row(NotificationDAL.COL_NAME_ENABLED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(NotificationDAL.COL_NAME_ENABLED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(NotificationDAL.COL_NAME_ENABLED, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New NotificationDAL
                dal.Update(Row)
                If Not IsNew Then
                    'Update user notifications
                    Dim dalUN As New UserNotificationDAL
                    dalUN.DeleteUserNotifications(Id)
                End If
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

#Region "Children"


#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal NotificationNameMask As String, _
                                   ByVal NotificationDetailMask As String, _
                                   ByVal NotificationTypeId As Guid, _
                                   ByVal AudianceTypeId As Guid, _
                                   ByVal BeginDate As String, _
                                   ByVal EndDate As String, _
                                   ByVal BeginDateOutage As String, _
                                   ByVal EndDateOutage As String, _
                                   ByVal IncludeDisabled As Boolean, _
                                   Optional ByVal sortOrder As String = NotificationDAL.SORT_ORDER_DESC, _
                                   Optional ByVal sortBy As String = NotificationDAL.SORT_BY_NOTIFICATION_TYPE, _
                                   Optional ByVal userType As String = "ADMIN", _
                                   Optional ByVal LimitResultset As Integer = PublishedTaskDAL.MAX_NUMBER_OF_ROWS) As NotificationSearchDV


        Try

            Dim AudianceTypeExternalId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__EXTERNAL_USER)
            Dim AudianceTypeInternalId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__INTERNAL_USER)
            Dim AudianceTypeAllId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__ALL_USERS)

            Return New NotificationSearchDV((New NotificationDAL).LoadList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, _
                                                                            NotificationNameMask, _
                                                                            NotificationDetailMask, _
                                                                            NotificationTypeId, _
                                                                            AudianceTypeId, _
                                                                            BeginDate, _
                                                                            EndDate, _
                                                                            BeginDateOutage, _
                                                                            EndDateOutage, _
                                                                            IncludeDisabled, _
                                                                            ElitaPlusIdentity.Current.ActiveUser.External, _
                                                                            AudianceTypeExternalId, AudianceTypeInternalId, AudianceTypeAllId, _
                                                                            LimitResultset, sortOrder, sortBy, userType).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Shared Function GetListForUser(Optional ByVal sortOrder As String = NotificationDAL.SORT_ORDER_DESC, _
                           Optional ByVal sortBy As String = NotificationDAL.SORT_BY_NOTIFICATION_TYPE, _
                           Optional ByVal LimitResultset As Integer = PublishedTaskDAL.MAX_NUMBER_OF_ROWS) As NotificationSearchDV


        Try

            Dim AudianceTypeExternalId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__EXTERNAL_USER)
            Dim AudianceTypeInternalId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__INTERNAL_USER)
            Dim AudianceTypeAllId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__ALL_USERS)



            Return New NotificationSearchDV((New NotificationDAL).LoadListForUser(ElitaPlusIdentity.Current.ActiveUser.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.External, _
                                                                                  AudianceTypeExternalId, AudianceTypeInternalId, AudianceTypeAllId, _
                                                                                  LimitResultset, sortOrder, sortBy).Tables(0))



        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
#End Region

#Region "Notification Search Dataview"
    Public Class NotificationSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_NOTIFICATION_ID As String = NotificationDAL.COL_NAME_NOTIFICATION_ID
        Public Const COL_NAME_NOTIFICATION_TYPE_ID As String = NotificationDAL.COL_NAME_NOTIFICATION_TYPE_ID
        Public Const COL_NAME_NOTIFICATION_TYPE As String = NotificationDAL.COL_NAME_NOTIFICATION_TYPE
        Public Const COL_NAME_AUDIANCE_TYPE As String = NotificationDAL.COL_NAME_AUDIANCE_TYPE_ID
        Public Const COL_NAME_BEGIN_DATE As String = NotificationDAL.COL_NAME_NOTIFICATION_BEGIN_DATE
        Public Const COL_NAME_END_DATE As String = NotificationDAL.COL_NAME_NOTIFICATION_END_DATE
        Public Const COL_NAME_NOTIFICATION_NAME As String = NotificationDAL.COL_NAME_NOTIFICATION_NAME
        Public Const COL_NAME_NOTIFICATION_DETAILS As String = NotificationDAL.COL_NAME_NOTIFICATION_DETAILS
        Public Const COL_NAME_SERIAL_NO As String = NotificationDAL.COL_NAME_SERIAL_NO

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region
End Class



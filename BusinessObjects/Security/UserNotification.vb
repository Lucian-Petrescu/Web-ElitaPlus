'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/26/2014)  ********************

Public Class UserNotification
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
            Dim dal As New UserNotificationDAL
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
            Dim dal As New UserNotificationDAL
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
            If row(UserNotificationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(UserNotificationDAL.COL_NAME_USER_NOTIFICATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property UserId() As Guid
        Get
            CheckDeleted()
            If row(UserNotificationDAL.COL_NAME_USER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(UserNotificationDAL.COL_NAME_USER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(UserNotificationDAL.COL_NAME_USER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property NotificationId() As Guid
        Get
            CheckDeleted()
            If row(UserNotificationDAL.COL_NAME_NOTIFICATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(UserNotificationDAL.COL_NAME_NOTIFICATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(UserNotificationDAL.COL_NAME_NOTIFICATION_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New UserNotificationDAL
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function UserHasNotifications(userId As Guid) As Boolean

        Dim dal As New UserNotificationDAL

        Dim AudianceTypeExternalId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__EXTERNAL_USER)
        Dim AudianceTypeInternalId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__INTERNAL_USER)
        Dim AudianceTypeAllId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_AUDIANCE_TYPES, Codes.AUDIANCE_TYPE__ALL_USERS)

        If userId.Equals(Guid.Empty) Then
            Return dal.UserHasNotifications(ElitaPlusIdentity.Current.ActiveUser.Id, ElitaPlusIdentity.Current.ActiveUser.External, AudianceTypeExternalId, AudianceTypeInternalId, AudianceTypeAllId)
        Else
            Return dal.UserHasNotifications(userId, ElitaPlusIdentity.Current.ActiveUser.External, AudianceTypeExternalId, AudianceTypeInternalId, AudianceTypeAllId)
        End If
 
    End Function

    Public Shared Function InsertUserNotifications(ByVal userId As Guid) As Boolean
        Dim dal As New UserNotificationDAL
        Return dal.InsertUserNotifications(ElitaPlusIdentity.Current.ActiveUser.Id)

    End Function
#End Region

#Region "Children"

    Public Function AddUserNotification(ByVal UserNotificationID As Guid) As UserNotification
        If UserNotificationID.Equals(Guid.Empty) Then
            Dim objUserNotification As New UserNotification(Me.Dataset)
            Return objUserNotification
        Else
            Dim objUserNotification As New UserNotification(UserNotificationID, Me.Dataset)
            Return objUserNotification
        End If
    End Function

#End Region
End Class



'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/14/2017)  ********************

Public Class AuditSecurityLogs
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AuditSecurityLogsDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New AuditSecurityLogsDAL
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
            If Row(AuditSecurityLogsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AuditSecurityLogsDAL.COL_NAME_AUDIT_SECURITY_LOG_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property AuditDate As DateType
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_AUDIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AuditSecurityLogsDAL.COL_NAME_AUDIT_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_AUDIT_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property LogSource As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_LOG_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_LOG_SOURCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_LOG_SOURCE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=2000)>
    Public Property AuditSecurityTypeCode As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_AUDIT_SECURITY_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_AUDIT_SECURITY_TYPE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_AUDIT_SECURITY_TYPE_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property ClientIpAddress As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_CLIENT_IP_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_CLIENT_IP_ADDRESS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_CLIENT_IP_ADDRESS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)>
    Public Property IpAddressChain As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_IP_ADDRESS_CHAIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_IP_ADDRESS_CHAIN), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_IP_ADDRESS_CHAIN, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)>
    Public Property X509Certificate As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_X509_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_X509_CERTIFICATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_X509_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)>
    Public Property UserName As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_USER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_USER_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1000)>
    Public Property RequestUrl As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_REQUEST_URL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_REQUEST_URL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_REQUEST_URL, Value) 
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)>
    Public Property ActionName As String
        Get
            CheckDeleted()
            If Row(AuditSecurityLogsDAL.COL_NAME_ACTION_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AuditSecurityLogsDAL.COL_NAME_ACTION_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AuditSecurityLogsDAL.COL_NAME_ACTION_NAME, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AuditSecurityLogsDAL
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
    Public Shared Function GetAuditLogsList(AuditBeginDate As String,
                                            AuditEndDate As String,
                                            AuditSource As String,
                                            AuditSecurityTypeCode As String,
                                            IPAddress As String,
                                            UserName As String) As AuditLogsSearchDV

        Try
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim dal As New AuditSecurityLogsDAL()
            Return New AuditLogsSearchDV(dal.GetAuditLogsList(AuditBeginDate, AuditEndDate, AuditSource, AuditSecurityTypeCode, IPAddress, UserName, langId).Tables(0))


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "APS Publishing Search Dataview"
    Public Class AuditLogsSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_AUDIT_DATE As String = AuditSecurityLogsDAL.COL_NAME_AUDIT_DATE
        Public Const COL_NAME_AUDIT_SOURCE As String = AuditSecurityLogsDAL.COL_NAME_LOG_SOURCE
        Public Const COL_NAME_AUDIT_SECURITY_TYPE_CODE As String = AuditSecurityLogsDAL.COL_NAME_AUDIT_SECURITY_TYPE_CODE
        Public Const COL_NAME_IP_ADDRESS As String = AuditSecurityLogsDAL.COL_NAME_CLIENT_IP_ADDRESS
        Public Const COL_NAME_IP_ADDRESS_CHAIN As String = AuditSecurityLogsDAL.COL_NAME_IP_ADDRESS_CHAIN
        Public Const COL_NAME_X509_CERTIFICATE As String = AuditSecurityLogsDAL.COL_NAME_X509_CERTIFICATE
        Public Const COL_NAME_USER_NAME As String = AuditSecurityLogsDAL.COL_NAME_USER_NAME
        Public Const COL_NAME_REQUEST_URL As String = AuditSecurityLogsDAL.COL_NAME_REQUEST_URL
        Public Const COL_NAME_ACTION_NAME As String = AuditSecurityLogsDAL.COL_NAME_ACTION_NAME

#End Region
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub



    End Class

#End Region
End Class



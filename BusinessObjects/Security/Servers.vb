Imports System.ServiceModel

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/17/2006)  ********************

Public Class Servers
    Inherits BusinessObjectBase

#Region "Constants"

    Public Shared BatchTestURL As String = "http://{0}/ElitaBatchServices/TestService/"


#End Region
#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    Public Sub New(ByVal sHubRegion As String, ByVal sMachinePrefix As String, Optional ByVal webServiceName As String = Nothing,
                    Optional ByVal webServiceFunctionName As String = Nothing)
        MyBase.New()
        Dim sEnvironment As String = EnvironmentContext.Current.EnvironmentName

        Me.Dataset = New DataSet
        Me.Load(sHubRegion, sMachinePrefix, sEnvironment, webServiceName, webServiceFunctionName)
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
            Dim dal As New ServersDAL
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
            Dim dal As New ServersDAL
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

    Protected Sub Load(ByVal sHubRegion As String, ByVal sMachinePrefix As String, ByVal sEnvironment As String,
                       Optional ByVal webServiceName As String = Nothing, Optional ByVal webServiceFunctionName As String = Nothing)
        Try

            sHubRegion = "P1"
            sMachinePrefix = "ATL0"
            sEnvironment = "MODL"

            Dim dal As New ServersDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(sHubRegion.ToUpper, dal.COL_NAME_HUB_REGION, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, sHubRegion, sMachinePrefix, sEnvironment, webServiceName, webServiceFunctionName)
                Me.Row = Me.FindRow(sHubRegion.ToUpper, dal.COL_NAME_HUB_REGION, Me.Dataset.Tables(dal.TABLE_NAME))
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
            If Row(ServersDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServersDAL.COL_NAME_SERVER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=30)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=2)>
    Public Property HubRegion() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_HUB_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_HUB_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_HUB_REGION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)>
    Public Property MachinePrefix() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_MACHINE_PREFIX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_MACHINE_PREFIX), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_MACHINE_PREFIX, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=11)>
    Public Property Environment() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_ENVIRONMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_ENVIRONMENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_ENVIRONMENT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property FtpHostname() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_FTP_HOSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_FTP_HOSTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_FTP_HOSTNAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property FelitaFtpHostname() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_FELITA_FTP_HOSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_FELITA_FTP_HOSTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_FELITA_FTP_HOSTNAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=30)>
    Public Property LdapIp() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_LDAP_IP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_LDAP_IP), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_LDAP_IP, Value)
        End Set
    End Property

    '<ValueMandatory(""), ValidStringLength("", Max:=30)> _
    'Public Property LdapIpDr() As String
    '    Get
    '        CheckDeleted()
    '        If Row(ServersDAL.COL_NAME_LDAP_IP_DR) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(ServersDAL.COL_NAME_LDAP_IP_DR), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(ServersDAL.COL_NAME_LDAP_IP_DR, Value)
    '    End Set
    'End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property FtpHostPath() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_FTP_HOST_PATH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_FTP_HOST_PATH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_FTP_HOST_PATH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property FtpTriggerExtension() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_FTP_TRIGGER_EXTENSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_FTP_TRIGGER_EXTENSION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_FTP_TRIGGER_EXTENSION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property FtpSplitPath() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_FTP_SPLIT_PATH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_FTP_SPLIT_PATH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_FTP_SPLIT_PATH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)>
    Public Property SmartStreamHostName() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_SMARTSTREAM_HOSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_SMARTSTREAM_HOSTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_SMARTSTREAM_HOSTNAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property ServiceOrderImageHost() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_SERVICEORDER_IMAGE_HOSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_SERVICEORDER_IMAGE_HOSTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_SERVICEORDER_IMAGE_HOSTNAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property PrivacyLevelXCD() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_PRIVACY_LEVEL_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_PRIVACY_LEVEL_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_PRIVACY_LEVEL_XCD, Value.ToUpper)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10)>
    Public Property DatabaseName() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_DATABASE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_DATABASE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_DATABASE_NAME, If(Value Is Nothing, Value, Value.ToUpper))
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property BatchHostname() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_BATCH_HOSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_BATCH_HOSTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_BATCH_HOSTNAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property AcctBalanceHostname() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_ACCT_BALANCE_HOSTNAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_ACCT_BALANCE_HOSTNAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_ACCT_BALANCE_HOSTNAME, Value)
        End Set
    End Property

    Public ReadOnly Property WebServiceOffLineMessage() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_WEB_SERVICE_OFF_LINE_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_WEB_SERVICE_OFF_LINE_MESSAGE), String)
            End If
        End Get
    End Property

    Public ReadOnly Property WebServiceFunctionOffLineMessage() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_WEB_SERVICE_FUNCTION_OFF_LINE_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_WEB_SERVICE_FUNCTION_OFF_LINE_MESSAGE), String)
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property SmartStreamGLStatus() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_SMARTSTREAM_GL_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_SMARTSTREAM_GL_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_SMARTSTREAM_GL_STATUS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property SmartStreamGLUpload() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_SMARTSTREAM_GL_UPLOAD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_SMARTSTREAM_GL_UPLOAD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_SMARTSTREAM_GL_UPLOAD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property SmartStreamAPUpload() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_SMARTSTREAM_AP_UPLOAD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_SMARTSTREAM_AP_UPLOAD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_SMARTSTREAM_AP_UPLOAD, Value)
        End Set
    End Property
    <ValidNumericRange("", Max:=999)>
    Public Property NoOfParallelProcesses() As Integer
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_NO_OF_PARALLEL_PROCESSES) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_NO_OF_PARALLEL_PROCESSES), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_NO_OF_PARALLEL_PROCESSES, Value)
        End Set
    End Property
    <ValidNumericRange("", Max:=99999)>
    Public Property CommitFrequency() As Integer
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_COMMIT_FREQUENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_COMMIT_FREQUENCY), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_COMMIT_FREQUENCY, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=30)>
    Public Property DBUniqueName() As String
        Get
            CheckDeleted()
            If Row(ServersDAL.COL_NAME_DB_UNIQUE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServersDAL.COL_NAME_DB_UNIQUE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServersDAL.COL_NAME_DB_UNIQUE_NAME, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServersDAL
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

    Public Sub DeleteAndSave()
        Me.CheckDeleted()
        Me.BeginEdit()
        Try
            Me.Delete()
            Me.Save()
        Catch ex As Exception
            Me.cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(ByVal original As Servers)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing server")
        End If
        'Copy myself
        Me.CopyFrom(original)
    End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetServersList(ByVal environmentMask As String, ByVal descriptionMask As String) As SearchDV
        Try
            Dim dal As New ServersDAL
            Dim ds As DataSet

            Return New SearchDV(dal.LoadList(environmentMask, descriptionMask).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "ServerSearchDV"
    Public Class SearchDV
        Inherits DataView

        Public Const COL_SERVER_ID As String = ServersDAL.COL_NAME_SERVER_ID
        Public Const COL_ENVIRONMENT As String = ServersDAL.COL_NAME_ENVIRONMENT
        Public Const COL_DESCRIPTION As String = ServersDAL.COL_NAME_DESCRIPTION
        Public Const COL_BATCH_HOSTNAME As String = ServersDAL.COL_NAME_BATCH_HOSTNAME


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
        Public Function AddNewRowToEmptyDV() As SearchDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(SearchDV.COL_SERVER_ID) = (New Guid()).ToByteArray

            dt.Rows.Add(row)
            Return New SearchDV(dt)
        End Function
    End Class
#End Region

#Region "Additional Logic"

    Public Shared Function TestCurrentBatchService(ByVal UserName As String, ByVal Password As String, ByVal Group As String) As String

        Try
            Dim _dal As New DALObjects.ServersDAL
            Return _dal.TestBatchServices(UserName, Password, Group)

        Catch ex As Exception
            Throw New ElitaPlusException(ex.Message, "", ex)
        End Try

    End Function

    Public Shared Function TestBatchService(ByVal UserName As String, ByVal Password As String, ByVal Group As String, ByVal URL As String) As String

        Try
            Dim response As String
            Dim eab As EndpointAddressBuilder
            Dim ea As EndpointAddress
            Dim _bind As New BasicHttpBinding

            eab = New EndpointAddressBuilder
            eab.Uri = New Uri(String.Format(Servers.BatchTestURL, URL))
            ea = eab.ToEndpointAddress
            Dim wc As New TestBatchService.TestServiceClient(_bind, ea)
            response = wc.HealthCheck(UserName, Password, Group)

            Return response

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region
End Class




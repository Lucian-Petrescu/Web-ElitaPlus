'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/7/2009)  ********************
Imports System.Collections.Generic

Public Class WebPasswd
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(isEnvCmpGp As Boolean)
        MyBase.New()

        Dim env As String = EnvironmentContext.Current.EnvironmentName
        Dim cmpGpId As Guid = Authentication.CompanyGroupId
        'Fix for Def-2229 
        Dim hub As String = ElitaPlusIdentity.Current.ConnectionType

        Dataset = New DataSet
        Load(env, cmpGpId, hub)
    End Sub

    Public Sub New(companyGroupId As Guid, serviceTypeId As Guid, isExternal As Boolean)
        MyBase.New()

        Dim env As String = EnvironmentContext.Current.EnvironmentName
        Dim hub As String = ElitaPlusIdentity.Current.ConnectionType

        Dataset = New DataSet
        Load(companyGroupId, serviceTypeId, env, isExternal, hub)
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
            Dim dal As New WebPasswdDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New WebPasswdDAL
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
    'Fix for Def-2229
    Protected Sub Load(env As String, cmpGpId As Guid, Optional ByVal hub As String = Nothing)
        Try
            Dim dal As New WebPasswdDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(env, dal.COL_NAME_ENV, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                'Fix for Def-2229
                dal.Load(Dataset, env, cmpGpId, hub)
                Row = FindRow(env, dal.COL_NAME_ENV, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(companyGroupId As Guid, serviceTypeId As Guid, env As String, _
                    isExternal As Boolean, hub As String)
        Try
            Dim dal As New WebPasswdDAL
            Dim keyValueList As New List(Of KeyValuePair(Of String, Object))
            keyValueList.Add(New KeyValuePair(Of String, Object)(WebPasswdDAL.COL_NAME_IS_EXTERNAL, IIf(isExternal, "Y", "N")))
            If (Not IsNothing(companyGroupId)) Then
                keyValueList.Add(New KeyValuePair(Of String, Object)(WebPasswdDAL.COL_NAME_COMPANY_GROUP_ID, companyGroupId))
            End If
            If (Not IsNothing(serviceTypeId)) Then
                keyValueList.Add(New KeyValuePair(Of String, Object)(WebPasswdDAL.COL_NAME_SERVICE_TYPE_ID, serviceTypeId))
            End If
            If (Not IsNothing(env)) Then
                keyValueList.Add(New KeyValuePair(Of String, Object)(WebPasswdDAL.COL_NAME_ENV, env))
            End If
            If (Not IsNothing(serviceTypeId)) Then
                keyValueList.Add(New KeyValuePair(Of String, Object)(WebPasswdDAL.COL_NAME_HUB, hub))
            End If
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(keyValueList, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, companyGroupId, serviceTypeId, env, isExternal, hub)
                Row = FindRow(keyValueList, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                'check if there is entry based on CompanyGroup , Env , hub 
                AppConfig.Log("WebPasswd, No record found for : Environment =" & env & ", Hub=" & hub & ", CompanyGroupId=" & MiscUtil.GetDbStringFromGuid(companyGroupId) & ", ServiceTypeId=" & MiscUtil.GetDbStringFromGuid(serviceTypeId))
                dal.Load(Dataset, env, companyGroupId, hub)
                Row = FindRow(env, dal.COL_NAME_ENV, Dataset.Tables(dal.TABLE_NAME))
                If Row Is Nothing Then
                    Throw New DataNotFoundException
                End If
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
            If row(WebPasswdDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WebPasswdDAL.COL_NAME_WEB_PASS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property UserId As String
        Get
            CheckDeleted()
            If Row(WebPasswdDAL.COL_NAME_USER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_USER_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_USER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Password As String
        Get
            CheckDeleted()
            If Row(WebPasswdDAL.COL_NAME_PASSWORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_PASSWORD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_PASSWORD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Env As String
        Get
            CheckDeleted()
            If Row(WebPasswdDAL.COL_NAME_ENV) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_ENV), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_ENV, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property Url As String
        Get
            CheckDeleted()
            If row(WebPasswdDAL.COL_NAME_URL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_URL), String).Replace("[LOCALHOST]", Environment.MachineName)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_URL, Value)
        End Set
    End Property



    Public Property Token As String
        Get
            CheckDeleted()
            If Row(WebPasswdDAL.COL_NAME_TOKEN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_TOKEN), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_TOKEN, Value)
        End Set
    End Property



    Public Property TokenCreatedDate As DateType
        Get
            CheckDeleted()
            If row(WebPasswdDAL.COL_NAME_TOKEN_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(WebPasswdDAL.COL_NAME_TOKEN_CREATED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_TOKEN_CREATED_DATE, Value)
        End Set
    End Property



    Public Property NumPerProcess As LongType
        Get
            CheckDeleted()
            If row(WebPasswdDAL.COL_NAME_NUM_PER_PROCESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(WebPasswdDAL.COL_NAME_NUM_PER_PROCESS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_NUM_PER_PROCESS, Value)
        End Set
    End Property



    Public Property TokenDuration As LongType
        Get
            CheckDeleted()
            If row(WebPasswdDAL.COL_NAME_TOKEN_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(WebPasswdDAL.COL_NAME_TOKEN_DURATION), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_TOKEN_DURATION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If row(WebPasswdDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WebPasswdDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property IsExternal As String
        Get
            CheckDeleted()
            If row(WebPasswdDAL.COL_NAME_IS_EXTERNAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(WebPasswdDAL.COL_NAME_IS_EXTERNAL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_IS_EXTERNAL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=250)> _
    Public Property AuthenticationKey As String
        Get
            CheckDeleted()
            If Row(WebPasswdDAL.COL_NAME_AUTHENTICATION_KEY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_AUTHENTICATION_KEY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_AUTHENTICATION_KEY, Value)
        End Set
    End Property

    'Fix for Def-2229
    <ValidStringLength("", Max:=40)> _
    Public Property Hub As String
        Get
            CheckDeleted()
            If Row(WebPasswdDAL.COL_NAME_HUB) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_HUB), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_HUB, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property GenericUrl As String
        Get
            CheckDeleted()
            If Row(WebPasswdDAL.COL_NAME_GENERIC_URL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WebPasswdDAL.COL_NAME_GENERIC_URL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebPasswdDAL.COL_NAME_GENERIC_URL, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New WebPasswdDAL
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



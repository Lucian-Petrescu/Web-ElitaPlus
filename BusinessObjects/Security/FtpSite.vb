'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/26/2009)  ********************

Public Class FtpSite
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
            Dim dal As New FtpSiteDAL
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
            Dim dal As New FtpSiteDAL
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
            If row(FtpSiteDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(FtpSiteDAL.COL_NAME_FTP_SITE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=5)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(FtpSiteDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FtpSiteDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=30)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(FtpSiteDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FtpSiteDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Host() As String
        Get
            CheckDeleted()
            If Row(FtpSiteDAL.COL_NAME_HOST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FtpSiteDAL.COL_NAME_HOST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_HOST, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Port() As LongType
        Get
            CheckDeleted()
            If row(FtpSiteDAL.COL_NAME_PORT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(FtpSiteDAL.COL_NAME_PORT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_PORT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property UserName() As String
        Get
            CheckDeleted()
            If Row(FtpSiteDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FtpSiteDAL.COL_NAME_USER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_USER_NAME, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Password() As String
        Get
            CheckDeleted()
            If Row(FtpSiteDAL.COL_NAME_PASSWORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FtpSiteDAL.COL_NAME_PASSWORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_PASSWORD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property Account() As String
        Get
            CheckDeleted()
            If Row(FtpSiteDAL.COL_NAME_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FtpSiteDAL.COL_NAME_ACCOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_ACCOUNT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Directory() As String
        Get
            CheckDeleted()
            If Row(FtpSiteDAL.COL_NAME_DIRECTORY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(FtpSiteDAL.COL_NAME_DIRECTORY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(FtpSiteDAL.COL_NAME_DIRECTORY, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New FtpSiteDAL
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

    Public Shared Function getList(ByVal code As String, ByVal description As String) As FtpSiteSearchDV
        Try
            Dim dal As New FtpSiteDAL
            Return New FtpSiteSearchDV(dal.LoadList(code, description).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region


#Region "ReportConfigSearchDV"
    Public Class FtpSiteSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_FTP_SITE_ID As String = "ftp_site_id"
        Public Const COL_CODE As String = "code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_HOST As String = "host"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As FtpSiteSearchDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(FtpSiteSearchDV.COL_FTP_SITE_ID) = (New Guid()).ToByteArray
            dt.Rows.Add(row)
            Return New FtpSiteSearchDV(dt)
        End Function

    End Class

#End Region

End Class




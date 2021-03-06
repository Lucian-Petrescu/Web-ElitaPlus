﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/24/2009)  ********************

Public Class WsFunctions
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_NAME_WEB_SERVICE_FUNCTION_NAME = WsFunctionsDAL.COL_NAME_FUNCTION_NAME
    Public Const COL_NAME_WEBSERVICE_FUNCTION_ID = WsFunctionsDAL.COL_NAME_WS_FUNCTION_ID
#End Region
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
            Dim dal As New WsFunctionsDAL                           
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
            Dim dal As New WsFunctionsDAL
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
            If row(WsFunctionsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WsFunctionsDAL.COL_NAME_WS_FUNCTION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property WebserviceId() As Guid
        Get
            CheckDeleted()
            If row(WsFunctionsDAL.COL_NAME_WEBSERVICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WsFunctionsDAL.COL_NAME_WEBSERVICE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(WsFunctionsDAL.COL_NAME_WEBSERVICE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property FunctionName() As String
        Get
            CheckDeleted()
            If row(WsFunctionsDAL.COL_NAME_FUNCTION_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(WsFunctionsDAL.COL_NAME_FUNCTION_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(WsFunctionsDAL.COL_NAME_FUNCTION_NAME, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property OnLineId() As Guid
        Get
            CheckDeleted()
            If row(WsFunctionsDAL.COL_NAME_ON_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WsFunctionsDAL.COL_NAME_ON_LINE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(WsFunctionsDAL.COL_NAME_ON_LINE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property OffLineMessage() As String
        Get
            CheckDeleted()
            If row(WsFunctionsDAL.COL_NAME_OFF_LINE_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(WsFunctionsDAL.COL_NAME_OFF_LINE_MESSAGE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(WsFunctionsDAL.COL_NAME_OFF_LINE_MESSAGE, Value)
        End Set
    End Property

    Public Property LastOperationDate() As DateType
        Get
            CheckDeleted()
            If Row(WsFunctionsDAL.COL_NAME_LAST_OPERATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(WsFunctionsDAL.COL_NAME_LAST_OPERATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(WsFunctionsDAL.COL_NAME_LAST_OPERATION_DATE, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New WsFunctionsDAL
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
    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal webservice_id As Guid, ByVal webservice_function_id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(WsFunctionsDAL.COL_NAME_FUNCTION_NAME) = String.Empty
        row(WsFunctionsDAL.COL_NAME_WS_FUNCTION_ID) = webservice_function_id.ToByteArray
        row(WsFunctionsDAL.COL_NAME_WEBSERVICE_ID) = webservice_id.ToByteArray
        row(WsFunctionsDAL.COL_NAME_OFF_LINE_MESSAGE) = String.Empty
        row(WsFunctionsDAL.COL_NAME_ON_LINE_ID) = (Guid.Empty).ToByteArray

        dt.Rows.Add(row)

        Return (dv)

    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetWsFunctions(ByVal webservice_id As Guid) As WsFunctionsSearchDV

        Try
            Dim dal As New WsFunctionsDAL
            Return New WsFunctionsSearchDV(dal.LoadList(webservice_id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try


    End Function

    Public Class WsFunctionsSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_WS_FUNCTION_ID As String = "ws_function_id"
        Public Const COL_FUNCTION_NAME As String = "function_name"
        Public Const COL_ON_LINE As String = "on_line"
        Public Const COL_OFF_LINE_MESSAGE As String = "off_line_message"
        Public Const COL_MODIFIED_BY As String = "modified_by"
        Public Const COL_MODIFIED_DATE As String = "modified_date"
        Public Const COL_LAST_OPERATION_DATE As String = "last_operation_date"


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


﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/9/2015)  ********************
Imports System.Collections.Generic
Imports System.Linq

Public Class ElitaAttribute
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DUPLICATE_ELITA_ATTRIBUTE As String = "DUPLICATE_ELITA_ATTRIBUTE"

    Public Const COL_NAME_ATTRIBUTE_COUNT As String = "attribute_count"
    Public Const COL_NAME_TABLE_NAME As String = "table_name"
    Public Const COL_NAME_DATA_TYPE_ID As String = AttributeDAL.COL_NAME_DATA_TYPE_ID
    Public Const COL_NAME_ATTRIBUTE_ID As String = AttributeDAL.COL_NAME_ATTRIBUTE_ID
    Public Const COL_NAME_UI_PROG_CODE As String = AttributeDAL.COL_NAME_UI_PROG_CODE
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
            Dim dal As New AttributeDAL
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
            Dim dal As New AttributeDAL
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
        Me.AllowDuplicates = Codes.YESNO_N
        Me.UseEffectiveDate = Codes.YESNO_N
        Me.UiProgCode = String.Empty

    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(AttributeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AttributeDAL.COL_NAME_ATTRIBUTE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DataTypeId() As Guid
        Get
            CheckDeleted()
            If row(AttributeDAL.COL_NAME_DATA_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AttributeDAL.COL_NAME_DATA_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AttributeDAL.COL_NAME_DATA_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=255), CheckDuplicateAttribute("")> _
    Public Property UiProgCode() As String
        Get
            CheckDeleted()
            If Row(AttributeDAL.COL_NAME_UI_PROG_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AttributeDAL.COL_NAME_UI_PROG_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AttributeDAL.COL_NAME_UI_PROG_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=30)> _
    Public Property TableName() As String
        Get
            CheckDeleted()
            If Row(AttributeDAL.COL_NAME_TABLE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AttributeDAL.COL_NAME_TABLE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AttributeDAL.COL_NAME_TABLE_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AllowDuplicates() As String
        Get
            CheckDeleted()
            If Row(AttributeDAL.COL_NAME_ALLOW_DUPLICATES) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AttributeDAL.COL_NAME_ALLOW_DUPLICATES), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AttributeDAL.COL_NAME_ALLOW_DUPLICATES, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property UseEffectiveDate() As String
        Get
            CheckDeleted()
            If Row(AttributeDAL.COL_NAME_USE_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AttributeDAL.COL_NAME_USE_EFFECTIVE_DATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AttributeDAL.COL_NAME_USE_EFFECTIVE_DATE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AttributeDAL
                dal.Update(Me.Dataset)
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

    Public Shared Function GetTableList(ByVal tableName As String, ByVal sortExpression As String) As DataView
        Dim dal As New AttributeDAL
        Dim ds As DataSet
        ds = dal.LoadTableList(tableName, sortExpression)
        Return ds.Tables(0).DefaultView
    End Function

    Shared Function GetTableAttributes(ByVal pTableName As String) As DataSet
        Dim dal As New AttributeDAL
        Dim ds As DataSet
        ds = dal.LoadAttributeList(pTableName)
        Return ds
    End Function

#End Region

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicateAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_ELITA_ATTRIBUTE)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ElitaAttribute = CType(objectToValidate, ElitaAttribute)
            For Each dr As DataRow In obj.Row.Table.Rows
                Dim oEa As ElitaAttribute = New ElitaAttribute(dr)
                If (oEa.Id = obj.Id) Then
                    Continue For
                End If

                If (oEa.TableName = obj.TableName) AndAlso (oEa.UiProgCode = obj.UiProgCode) Then
                    Return False
                End If
            Next

            Return True
        End Function
    End Class

End Class



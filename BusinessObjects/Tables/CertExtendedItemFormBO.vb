'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/9/2015)  ********************
Imports System.Collections.Generic
Imports System.Linq

Public Class CertExtendedItemFormBO
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const DUPLICATE_ELITA_CERT_EXTENDED_ITEM As String = "DUPLICATE_ELITA_CERT_EXTENDED_ITEM"

    Public Const COL_NAME_CERT_EXTENDED_ITEM_COUNT As String = "cert_extended_item_count"
    Public Const COL_NAME_TABLE_NAME As String = "table_name"
    Public Const COL_NAME_FIELD_NAME As String = CertExtendedItemFormDAL.COL_NAME_FIELD_NAME
    Public Const COL_NAME_CERT_EXTENDED_ITEM_ID As String = CertExtendedItemFormDAL.COL_NAME_CERT_EXTENDED_ITEM_ID
    Public Const COL_NAME_DEFAULT_VALUE As String = CertExtendedItemFormDAL.COL_NAME_DEFAULT_VALUE
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
        'Me.Dataset = New DataSet
        'Me.Load()
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
            Dim dal As New CertExtendedItemFormDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            Me.Row(dal.TABLE_KEY_NAME) = Guid.NewGuid
            'SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CertExtendedItemFormDAL
            If Me._isDSCreator Then
                If Me.Row IsNot Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
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
        Me.InEnrollment = Codes.YESNO_Y
        Me.AllowUpdate = Codes.YESNO_Y
        Me.FieldName = String.Empty
        Me.DefaultValue = String.Empty
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Shared Function GetData() As DataSet
        Dim dal As New CertExtendedItemFormDAL
        Dim ds As DataSet
        ds = dal.LoadList()
        Return ds
    End Function

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CertExtendedItemFormDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(CertExtendedItemFormDAL.COL_NAME_CERT_EXTENDED_ITEM_ID)
            End If
        End Get
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=30)>
    Public Property TableName() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDAL.COL_NAME_TABLE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDAL.COL_NAME_TABLE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDAL.COL_NAME_TABLE_NAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255), CheckDuplicateCertExtendedItem("")>
    Public Property FieldName() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDAL.COL_NAME_FIELD_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDAL.COL_NAME_FIELD_NAME), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDAL.COL_NAME_FIELD_NAME, value)
        End Set
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property InEnrollment() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDAL.COL_NAME_IN_ENROLLMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDAL.COL_NAME_IN_ENROLLMENT), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDAL.COL_NAME_IN_ENROLLMENT, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255), CheckDuplicateCertExtendedItem("")>
    Public Property DefaultValue() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDAL.COL_NAME_DEFAULT_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDAL.COL_NAME_DEFAULT_VALUE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDAL.COL_NAME_DEFAULT_VALUE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property AllowUpdate() As String
        Get
            CheckDeleted()
            If Row(CertExtendedItemFormDAL.COL_NAME_ALLOW_UPDATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertExtendedItemFormDAL.COL_NAME_ALLOW_UPDATE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(CertExtendedItemFormDAL.COL_NAME_ALLOW_UPDATE, value)
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

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicateCertExtendedItem
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_ELITA_CERT_EXTENDED_ITEM)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertExtendedItemFormBO = CType(objectToValidate, CertExtendedItemFormBO)
            For Each dr As DataRow In obj.Row.Table.Rows
                Dim oEa As CertExtendedItemFormBO = New CertExtendedItemFormBO(dr)
                If (oEa.Id = obj.Id) Then
                    Continue For
                End If
            Next

            Return True
        End Function
    End Class

End Class



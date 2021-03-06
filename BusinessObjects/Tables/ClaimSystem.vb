﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/23/2010)  ********************

Public Class ClaimSystem
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
            Dim dal As New ClaimSystemDAL
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
            Dim dal As New ClaimSystemDAL
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
            If row(ClaimSystemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_CLAIM_SYSTEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimSystemDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimSystemDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimSystemDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimSystemDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property



    Public Property NewClaimId() As Guid
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID, Value)
        End Set
    End Property



    Public Property PayClaimId() As Guid
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID, Value)
        End Set
    End Property



    Public Property MaintainClaimId() As Guid
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimSystemDAL
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

#Region "ClaimSystemDV"
    Public Class ClaimSystemDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_SYSTEM_ID As String = ClaimSystemDAL.COL_NAME_CLAIM_SYSTEM_ID
        Public Const COL_DESCRIPTION As String = ClaimSystemDAL.COL_NAME_DESCRIPTION
        Public Const COL_CODE As String = ClaimSystemDAL.COL_NAME_CODE
        Public Const COL_MAINTAIN_CLAIM As String = ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM
        Public Const COL_NEW_CLAIM As String = ClaimSystemDAL.COL_NAME_NEW_CLAIM
        Public Const COL_PAY_CLAIM As String = ClaimSystemDAL.COL_NAME_PAY_CLAIM

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal descriptionMask As String) As DataView
        Try
            Dim dal As New ClaimSystemDAL
            Dim ds As DataSet

            ds = dal.LoadList(descriptionMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(ClaimSystemDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As ClaimSystem) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(ClaimSystemDAL.COL_NAME_DESCRIPTION) = bo.Description 'String.Empty
            row(ClaimSystemDAL.COL_NAME_CODE) = bo.Code 'String.Empty
            row(ClaimSystemDAL.COL_NAME_CLAIM_SYSTEM_ID) = bo.Id.ToByteArray
            row(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID) = bo.NewClaimId.ToByteArray
            row(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID) = bo.PayClaimId.ToByteArray
            row(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID) = bo.MaintainClaimId.ToByteArray
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

#End Region

End Class




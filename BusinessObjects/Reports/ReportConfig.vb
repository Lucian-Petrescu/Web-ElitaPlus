﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/27/2009)  ********************

Public Class ReportConfig
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
            Dim dal As New ReportConfigDAL
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
            Dim dal As New ReportConfigDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
            If row(ReportConfigDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReportConfigDAL.COL_NAME_REPORT_CONFIG_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If row(ReportConfigDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReportConfigDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportConfigDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property ReportCeName As String
        Get
            CheckDeleted()
            If row(ReportConfigDAL.COL_NAME_REPORT_CE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportConfigDAL.COL_NAME_REPORT_CE_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportConfigDAL.COL_NAME_REPORT_CE_NAME, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property FormId As Guid
        Get
            CheckDeleted()
            If row(ReportConfigDAL.COL_NAME_FORM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReportConfigDAL.COL_NAME_FORM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportConfigDAL.COL_NAME_FORM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property LargeReport As String
        Get
            CheckDeleted()
            If row(ReportConfigDAL.COL_NAME_LARGE_REPORT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportConfigDAL.COL_NAME_LARGE_REPORT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportConfigDAL.COL_NAME_LARGE_REPORT, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReportConfigDAL
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

    Public Shared Function getList(report As String, reportCe As String) As ReportConfigSearchDV
        Try
            Dim dal As New ReportConfigDAL
            Return New ReportConfigSearchDV(dal.LoadList(Authentication.CompIds, _
                                                        report, reportCe, _
                                                        Authentication.LangId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "ReportConfigSearchDV"
    Public Class ReportConfigSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_REPORT_CONFIG_ID As String = "report_config_id"
        Public Const COL_COMPANY As String = "company"
        Public Const COL_REPORT As String = "report"
        Public Const COL_REPORT_CE_NAME As String = "report_ce_name"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ReportConfigSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(COL_REPORT_CONFIG_ID) = (New Guid()).ToByteArray
            dt.Rows.Add(row)
            Return New ReportConfigSearchDV(dt)
        End Function

    End Class

#End Region
#End Region

End Class




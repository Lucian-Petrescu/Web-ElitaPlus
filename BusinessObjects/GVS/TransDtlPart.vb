﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/12/2008)  ********************

Public Class TransDtlPart
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
            Dim dal As New TransDtlPartDAL
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
            Dim dal As New TransDtlPartDAL
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
            If row(TransDtlPartDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlPartDAL.COL_NAME_TRANS_DTL_PART_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property TransDtlClmUpdte2elitaId As Guid
        Get
            CheckDeleted()
            If row(TransDtlPartDAL.COL_NAME_TRANS_DTL_CLM_UPDTE_2ELITA_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlPartDAL.COL_NAME_TRANS_DTL_CLM_UPDTE_2ELITA_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlPartDAL.COL_NAME_TRANS_DTL_CLM_UPDTE_2ELITA_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property XmlMfgPartCode As String
        Get
            CheckDeleted()
            If row(TransDtlPartDAL.COL_NAME_XML_MFG_PART_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlPartDAL.COL_NAME_XML_MFG_PART_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlPartDAL.COL_NAME_XML_MFG_PART_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property XmlPartDescriptionCode As Guid
        Get
            CheckDeleted()
            If row(TransDtlPartDAL.COL_NAME_XML_PART_DESCRIPTION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlPartDAL.COL_NAME_XML_PART_DESCRIPTION_CODE), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlPartDAL.COL_NAME_XML_PART_DESCRIPTION_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property XmlPartCost As DecimalType
        Get
            CheckDeleted()
            If row(TransDtlPartDAL.COL_NAME_XML_PART_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(TransDtlPartDAL.COL_NAME_XML_PART_COST), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlPartDAL.COL_NAME_XML_PART_COST, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlPartDefect As String
        Get
            CheckDeleted()
            If row(TransDtlPartDAL.COL_NAME_XML_PART_DEFECT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlPartDAL.COL_NAME_XML_PART_DEFECT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlPartDAL.COL_NAME_XML_PART_DEFECT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlPartSolution As String
        Get
            CheckDeleted()
            If row(TransDtlPartDAL.COL_NAME_XML_PART_SOLUTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlPartDAL.COL_NAME_XML_PART_SOLUTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlPartDAL.COL_NAME_XML_PART_SOLUTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property XmlInStock As String
        Get
            CheckDeleted()
            If row(TransDtlPartDAL.COL_NAME_XML_IN_STOCK) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlPartDAL.COL_NAME_XML_IN_STOCK), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlPartDAL.COL_NAME_XML_IN_STOCK, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransDtlPartDAL
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



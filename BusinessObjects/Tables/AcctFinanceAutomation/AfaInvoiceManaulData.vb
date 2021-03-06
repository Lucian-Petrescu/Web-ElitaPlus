﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/20/2015)  ********************

Public Class AfaInvoiceManaulData
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
            Dim dal As New AfaInvoiceManaulDataDAL
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
            Dim dal As New AfaInvoiceManaulDataDAL
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
            If row(AfaInvoiceManaulDataDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_AFA_INVOICE_MANUAL_DATA_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(AfaInvoiceManaulDataDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=6)> _
    Public Property InvoiceMonth() As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_INVOICE_MONTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_INVOICE_MONTH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_INVOICE_MONTH, Value)
        End Set
    End Property



    Public Property DataAmount() As DecimalType
        Get
            CheckDeleted()
            If row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)> _
    Public Property AmountTypeCode() As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_AMOUNT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_AMOUNT_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_AMOUNT_TYPE_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=500)> _
    Public Property DataText() As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=500)> _
    Public Property DataText2() As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property DataText3() As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT3), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT3, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property DataText4() As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT4), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT4, Value)
        End Set
    End Property

    Public Property DataDate() As DateType
        Get
            CheckDeleted()
            If row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_DATE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaInvoiceManaulDataDAL
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

    'Save to database even not the dataset creator
    Public Sub SaveWithoutCheckDSCreator()
        Try
            MyBase.Save()
            If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaInvoiceManaulDataDAL
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

    Public Shared Function StartInvoiceProcess(ByVal dealerId As Guid, ByVal BillingMonth As String) As Boolean
        Try
            Dim dal As New AfaInvoiceManaulDataDAL
            Return dal.StartInvoiceProcess(dealerId, BillingMonth, ElitaPlusIdentity.Current.ActiveUser.UserName)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub UpdateInvoicewithManualDates(ByVal dealerId As Guid, ByVal BillingMonth As String, ByRef strMsg As String)
        Try
            Dim dal As New AfaInvoiceManaulDataDAL
            dal.UpdateInvoiceWithManualDates(dealerId, BillingMonth, strMsg)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getListByDealer(ByVal dealerID As Guid, ByVal PeriodYear As String, ByVal PeriodMonth As String) As DataView
        Dim dal As New AfaInvoiceManaulDataDAL
        Return dal.LoadListByDealer(dealerID, PeriodYear, PeriodMonth).Tables(0).DefaultView
    End Function

    Public Shared Function getPONumberListByDealer(ByVal dealerID As Guid, ByVal PeriodGreaterThan As String) As DataView
        Dim dal As New AfaInvoiceManaulDataDAL
        Return dal.LoadPONumberListByDealer(dealerID, PeriodGreaterThan).Tables(0).DefaultView
    End Function

    Public Shared Sub AddEmptyRowToSearchDV(ByRef dv As DataView, ByVal NewBO As AfaInvoiceManaulData)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid, amtTemp As Double = 0.0
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add("dealer_id", guidTemp.ToByteArray.GetType)
                dt.Columns.Add("invoice_month", GetType(String))
                dt.Columns.Add("MDFReconAmount", amtTemp.GetType)
                dt.Columns.Add("CessionLossAmount", amtTemp.GetType)
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row("dealer_id") = NewBO.DealerId.ToByteArray
            row("invoice_month") = NewBO.InvoiceMonth
            row("MDFReconAmount") = 0
            row("CessionLossAmount") = 0

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New DataView(dt)
        End If
    End Sub

    Public Shared Sub AddEmptyRowToPONumSearchDV(ByRef dv As DataView, ByVal NewBO As AfaInvoiceManaulData)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid, amtTemp As Double = 0.0
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add("AFA_INVOICE_MANUAL_DATA_ID", guidTemp.ToByteArray.GetType)
                dt.Columns.Add("dealer_id", guidTemp.ToByteArray.GetType)
                dt.Columns.Add("invoice_month", GetType(String))
                dt.Columns.Add("PONumber", GetType(String))
                dt.Columns.Add("dealer_name", GetType(String))
                dt.Columns.Add("Invoice_Month_Desc", GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row("AFA_INVOICE_MANUAL_DATA_ID") = NewBO.Id.ToByteArray
            row("dealer_id") = NewBO.DealerId.ToByteArray
            row("invoice_month") = NewBO.InvoiceMonth
            row("PONumber") = String.Empty
            row("dealer_name") = String.Empty
            row("Invoice_Month_Desc") = String.Empty
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New DataView(dt)
        End If
    End Sub

    Public Shared Function getDealerMonthlyRecords(ByVal dealerID As Guid, ByVal AccountingMonth As String) As Collections.Generic.List(Of AfaInvoiceManaulData)
        Dim dal As New AfaInvoiceManaulDataDAL
        Dim ds As DataSet = dal.LoadDealerMonthlyRecords(dealerID, AccountingMonth)
        Dim objList As New Collections.Generic.List(Of AfaInvoiceManaulData)
        For Each dr As DataRow In ds.Tables(0).Rows
            objList.Add(New AfaInvoiceManaulData(dr))
        Next
        Return objList
    End Function


    Public Shared Function GetListByType(ByVal DealerID As Guid, ByVal InvoiceMonth As String, ByVal ManualDataType As String) As Collections.Generic.List(Of AfaInvoiceManaulData)        
        Return GetListByTypeByPeriod(DealerID, ManualDataType, InvoiceMonth, InvoiceMonth)
    End Function

    Public Shared Function GetListByTypeByPeriod(ByVal DealerID As Guid, ByVal ManualDataType As String, ByVal InvoiceMonthStart As String, ByVal InvoiceMonthEnd As String) As Collections.Generic.List(Of AfaInvoiceManaulData)
        Dim dal As New AfaInvoiceManaulDataDAL
        Dim ds As DataSet = dal.LoadListByTypeForPeriod(DealerID, ManualDataType, InvoiceMonthStart, InvoiceMonthEnd)
        Dim objList As New Collections.Generic.List(Of AfaInvoiceManaulData)
        For Each dr As DataRow In ds.Tables(0).Rows
            objList.Add(New AfaInvoiceManaulData(dr))
        Next
        Return objList
    End Function

    Public Shared Function GetDealerInvoiceDates(ByVal dealerID As Guid, ByVal AccountingMonth As String) As DataView
        Dim dal As New AfaInvoiceManaulDataDAL
        Return dal.LoadDealerInvoiceDates(dealerID, AccountingMonth).Tables(0).DefaultView
    End Function
#End Region

End Class
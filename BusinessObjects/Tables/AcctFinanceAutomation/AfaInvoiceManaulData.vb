'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/20/2015)  ********************

Public Class AfaInvoiceManaulData
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
            Dim dal As New AfaInvoiceManaulDataDAL
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
            Dim dal As New AfaInvoiceManaulDataDAL
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
            If row(AfaInvoiceManaulDataDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_AFA_INVOICE_MANUAL_DATA_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(AfaInvoiceManaulDataDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=6)> _
    Public Property InvoiceMonth As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_INVOICE_MONTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_INVOICE_MONTH), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_INVOICE_MONTH, Value)
        End Set
    End Property



    Public Property DataAmount As DecimalType
        Get
            CheckDeleted()
            If row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)> _
    Public Property AmountTypeCode As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_AMOUNT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_AMOUNT_TYPE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_AMOUNT_TYPE_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=500)> _
    Public Property DataText As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=500)> _
    Public Property DataText2 As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property DataText3 As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT3, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property DataText4 As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT4), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_TEXT4, Value)
        End Set
    End Property

    Public Property DataDate As DateType
        Get
            CheckDeleted()
            If row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(AfaInvoiceManaulDataDAL.COL_NAME_DATA_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceManaulDataDAL.COL_NAME_DATA_DATE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaInvoiceManaulDataDAL
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

    'Save to database even not the dataset creator
    Public Sub SaveWithoutCheckDSCreator()
        Try
            MyBase.Save()
            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AfaInvoiceManaulDataDAL
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

    Public Shared Function StartInvoiceProcess(dealerId As Guid, BillingMonth As String) As Boolean
        Try
            Dim dal As New AfaInvoiceManaulDataDAL
            Return dal.StartInvoiceProcess(dealerId, BillingMonth, ElitaPlusIdentity.Current.ActiveUser.UserName)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub UpdateInvoicewithManualDates(dealerId As Guid, BillingMonth As String, ByRef strMsg As String)
        Try
            Dim dal As New AfaInvoiceManaulDataDAL
            dal.UpdateInvoiceWithManualDates(dealerId, BillingMonth, strMsg)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getListByDealer(dealerID As Guid, PeriodYear As String, PeriodMonth As String) As DataView
        Dim dal As New AfaInvoiceManaulDataDAL
        Return dal.LoadListByDealer(dealerID, PeriodYear, PeriodMonth).Tables(0).DefaultView
    End Function

    Public Shared Function getPONumberListByDealer(dealerID As Guid, PeriodGreaterThan As String) As DataView
        Dim dal As New AfaInvoiceManaulDataDAL
        Return dal.LoadPONumberListByDealer(dealerID, PeriodGreaterThan).Tables(0).DefaultView
    End Function

    Public Shared Sub AddEmptyRowToSearchDV(ByRef dv As DataView, NewBO As AfaInvoiceManaulData)
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

    Public Shared Sub AddEmptyRowToPONumSearchDV(ByRef dv As DataView, NewBO As AfaInvoiceManaulData)
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

    Public Shared Function getDealerMonthlyRecords(dealerID As Guid, AccountingMonth As String) As Collections.Generic.List(Of AfaInvoiceManaulData)
        Dim dal As New AfaInvoiceManaulDataDAL
        Dim ds As DataSet = dal.LoadDealerMonthlyRecords(dealerID, AccountingMonth)
        Dim objList As New Collections.Generic.List(Of AfaInvoiceManaulData)
        For Each dr As DataRow In ds.Tables(0).Rows
            objList.Add(New AfaInvoiceManaulData(dr))
        Next
        Return objList
    End Function


    Public Shared Function GetListByType(DealerID As Guid, InvoiceMonth As String, ManualDataType As String) As Collections.Generic.List(Of AfaInvoiceManaulData)        
        Return GetListByTypeByPeriod(DealerID, ManualDataType, InvoiceMonth, InvoiceMonth)
    End Function

    Public Shared Function GetListByTypeByPeriod(DealerID As Guid, ManualDataType As String, InvoiceMonthStart As String, InvoiceMonthEnd As String) As Collections.Generic.List(Of AfaInvoiceManaulData)
        Dim dal As New AfaInvoiceManaulDataDAL
        Dim ds As DataSet = dal.LoadListByTypeForPeriod(DealerID, ManualDataType, InvoiceMonthStart, InvoiceMonthEnd)
        Dim objList As New Collections.Generic.List(Of AfaInvoiceManaulData)
        For Each dr As DataRow In ds.Tables(0).Rows
            objList.Add(New AfaInvoiceManaulData(dr))
        Next
        Return objList
    End Function

    Public Shared Function GetDealerInvoiceDates(dealerID As Guid, AccountingMonth As String) As DataView
        Dim dal As New AfaInvoiceManaulDataDAL
        Return dal.LoadDealerInvoiceDates(dealerID, AccountingMonth).Tables(0).DefaultView
    End Function
#End Region

End Class
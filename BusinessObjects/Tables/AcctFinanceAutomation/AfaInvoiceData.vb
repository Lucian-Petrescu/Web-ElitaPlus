'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/6/2015)  ********************

Public Class AfaInvoiceData
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    Public Sub New(ByVal dealerID As Guid, ByVal invoiceMonth As String)
        MyBase.New()
        Dataset = New DataSet
        Load(dealerID, invoiceMonth)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AfaInvoiceDataDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New AfaInvoiceDataDAL
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

    Protected Sub Load(ByVal dealerID As Guid, ByVal invoiceMonth As String)
        Try
            Dim dal As New AfaInvoiceDataDAL
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
                dal.LoadActiveInvoice(Dataset, dealerID, invoiceMonth)
                'Me.Row = Me.FindRow(Id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
                Row = FindRow(invoiceMonth, dal.COL_NAME_INVOICE_MONTH, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                'Throw New DataNotFoundException
                'Invoice not found, load a new row
                Load()
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
            If row(AfaInvoiceDataDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaInvoiceDataDAL.COL_NAME_AFA_INVOICE_DATA_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(AfaInvoiceDataDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AfaInvoiceDataDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=24)> _
    Public Property InvoiceMonth As String
        Get
            CheckDeleted()
            If row(AfaInvoiceDataDAL.COL_NAME_INVOICE_MONTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AfaInvoiceDataDAL.COL_NAME_INVOICE_MONTH), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_INVOICE_MONTH, Value)
        End Set
    End Property



    Public Property InvoiceXmlData As Object
        Get
            CheckDeleted()
            If row(AfaInvoiceDataDAL.COL_NAME_INVOICE_XML_DATA) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AfaInvoiceDataDAL.COL_NAME_INVOICE_XML_DATA), Object)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_INVOICE_XML_DATA, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property Deleted As String
        Get
            CheckDeleted()
            If row(AfaInvoiceDataDAL.COL_NAME_DELETED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AfaInvoiceDataDAL.COL_NAME_DELETED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_DELETED, Value)
        End Set
    End Property

    Public Property InvoiceHtml As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceDataDAL.COL_NAME_INVOICE_HTML) Is DBNull.Value Then
                Return Nothing
            Else
                'Return CType(Row(AfaInvoiceDataDAL.COL_NAME_INVOICE_HTML), Object)
                Return Row(AfaInvoiceDataDAL.COL_NAME_INVOICE_HTML).ToString()
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_INVOICE_HTML, Value)
        End Set
    End Property

    Public Property InvoiceCSV As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceDataDAL.COL_NAME_INVOICE_CSV) Is DBNull.Value Then
                Return Nothing
            Else
                'Return CType(Row(AfaInvoiceDataDAL.COL_NAME_INVOICE_HTML), Object)
                Return Row(AfaInvoiceDataDAL.COL_NAME_INVOICE_CSV).ToString()
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_INVOICE_CSV, Value)
        End Set
    End Property


    Public Property Filename As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceDataDAL.COL_NAME_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(AfaInvoiceDataDAL.COL_NAME_FILENAME).ToString()
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_FILENAME, Value)
        End Set
    End Property

    Public Property DirectoryName As String
        Get
            CheckDeleted()
            If Row(AfaInvoiceDataDAL.COL_NAME_DIRECTORY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(AfaInvoiceDataDAL.COL_NAME_DIRECTORY_NAME).ToString()
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AfaInvoiceDataDAL.COL_NAME_DIRECTORY_NAME, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    'Public Overrides Sub Save()
    '    Try
    '        MyBase.Save()
    '        If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
    '            Dim dal As New AfaInvoiceDataDAL
    '            dal.Update(Me.Row)
    '            'Reload the Data from the DB
    '            If Me.Row.RowState <> DataRowState.Detached Then
    '                Dim objId As Guid = Me.Id
    '                Me.Dataset = New DataSet
    '                Me.Row = Nothing
    '                Me.Load(objId)
    '            End If
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
    '    End Try
    'End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetDealerInvoiceData(ByVal dealerID As Guid, ByVal invoiceMonth As String) As String
        Dim dal As New AfaInvoiceDataDAL
        Return dal.LoadInvoiceData(dealerID, invoiceMonth)
    End Function

    Public Shared Sub SaveInvoiceHTML(ByVal id As Guid, ByVal strHTML As String)
        Dim dal As New AfaInvoiceDataDAL
        dal.SaveInvoiceHTML(id, strHTML)
    End Sub
    Public Shared Sub SaveInvoiceCSV(ByVal id As Guid, ByVal strCSV As String)
        Dim dal As New AfaInvoiceDataDAL
        dal.SaveInvoiceCSV(id, strCSV)
    End Sub


#End Region

End Class



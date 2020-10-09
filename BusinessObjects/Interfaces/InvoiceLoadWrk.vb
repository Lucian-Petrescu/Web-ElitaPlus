'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/7/2013)  ********************

Public Class InvoiceLoadWrk
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
            Dim dal As New InvoiceLoadWrkDAL
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
            Dim dal As New InvoiceLoadWrkDAL
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
            If row(InvoiceLoadWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceLoadWrkDAL.COL_NAME_INVOICE_LOAD_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=8)> _
    Public Property RecordType As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Company As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_COMPANY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_COMPANY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_COMPANY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property InvoiceNumber As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property



    Public Property InvoiceDate As DateType
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(InvoiceLoadWrkDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property Attributes As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_ATTRIBUTES) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_ATTRIBUTES), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_ATTRIBUTES, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property LineItemNumber As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property LineItemCode As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property LineItemDescription As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_LINE_ITEM_DESCRIPTION, Value)
        End Set
    End Property



    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(InvoiceLoadWrkDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property EntireRecord As String
        Get
            CheckDeleted()
            If row(InvoiceLoadWrkDAL.COL_NAME_ENTIRE_RECORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceLoadWrkDAL.COL_NAME_ENTIRE_RECORD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceLoadWrkDAL.COL_NAME_ENTIRE_RECORD, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceLoadWrkDAL
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



'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/11/2005)  ********************

Public Class IbnrLossPaid
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
            Dim dal As New IbnrLossPaidDAL
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
            Dim dal As New IbnrLossPaidDAL
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
            If row(IbnrLossPaidDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IbnrLossPaidDAL.COL_NAME_IBNR_LOSS_PAID_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IbnrLossPaidDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IbnrLossPaidDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property



    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IbnrLossPaidDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=3)> _
    Public Property CompanyKey() As String
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_COMPANY_KEY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(IbnrLossPaidDAL.COL_NAME_COMPANY_KEY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_COMPANY_KEY, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(IbnrLossPaidDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AmountOfLoss() As DecimalType
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_AMOUNT_OF_LOSS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(IbnrLossPaidDAL.COL_NAME_AMOUNT_OF_LOSS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_AMOUNT_OF_LOSS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DateOfLoss() As DateType
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_DATE_OF_LOSS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(IbnrLossPaidDAL.COL_NAME_DATE_OF_LOSS), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_DATE_OF_LOSS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DateInvoicePaid() As DateType
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_DATE_INVOICE_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(IbnrLossPaidDAL.COL_NAME_DATE_INVOICE_PAID), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_DATE_INVOICE_PAID, Value)
        End Set
    End Property



    Public Property PeriodInMonths() As LongType
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_PERIOD_IN_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(IbnrLossPaidDAL.COL_NAME_PERIOD_IN_MONTHS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_PERIOD_IN_MONTHS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property CoverageType() As String
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_COVERAGE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(IbnrLossPaidDAL.COL_NAME_COVERAGE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_COVERAGE_TYPE, Value)
        End Set
    End Property



    Public Property AccountingDate() As DateType
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=7)> _
    Public Property AccountingMmyyyy() As String
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_MMYYYY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_MMYYYY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_MMYYYY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=7)> _
    Public Property AccountingMmyyyyPaid() As String
        Get
            CheckDeleted()
            If row(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_MMYYYY_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_MMYYYY_PAID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IbnrLossPaidDAL.COL_NAME_ACCOUNTING_MMYYYY_PAID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New IbnrLossPaidDAL
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

#Region "DataView Retrieveing Methods"


    Public Shared Function GetIBNRLossPaidAccountingDate(ByVal companyId As Guid) As DataView
        Try
            Dim dal As New IbnrLossPaidDAL
            Dim ds As Dataset

            ds = dal.GetIBNRLossPaidAccountingDate(companyId)
            Return (ds.Tables(IbnrLossPaidDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


#End Region

End Class



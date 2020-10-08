'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/17/2015)  ********************

Public Class PreInvoice
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
            Dim dal As New PreInvoiceDAL
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
            Dim dal As New PreInvoiceDAL            
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
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private _totalAmount As Decimal
    Private _totalBonusAmount As Decimal
    Private TotalAndBonusPopulated As Boolean = False

    Private Sub PopulateTotalAndBonus()
        If Row(PreInvoiceDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
            _totalAmount = Decimal.Zero
            _totalBonusAmount = Decimal.Zero
        Else
            Dim Ds As New DataSet
            Dim dal As New PreInvoiceDAL
            Dim Dv As New DataView
            Try
                Ds = dal.GetTotalBonusAndAmount(Row(PreInvoiceDAL.COL_NAME_BATCH_NUMBER))
                If Ds IsNot Nothing AndAlso Ds.Tables.Count > 0 Then
                    Dv = Ds.Tables(dal.TABLE_NAME).DefaultView
                    If Dv(0)("total_Amount") Is DBNull.Value Then
                        _totalAmount = Decimal.Zero
                    Else
                        _totalAmount = CType(Dv(0)("total_Amount"), Decimal)
                    End If
                    If Dv(0)("total_bonus_Amount") Is DBNull.Value Then
                        _totalBonusAmount = Decimal.Zero
                    Else
                        _totalBonusAmount = CType(Dv(0)("total_bonus_Amount"), Decimal)
                    End If
                Else
                    _totalAmount = Decimal.Zero
                    _totalBonusAmount = Decimal.Zero
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            End Try
        End If
        TotalAndBonusPopulated = True
    End Sub

    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(PreInvoiceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PreInvoiceDAL.COL_NAME_PRE_INVOICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property PreInvoiceCreationDate As DateType
        Get
            CheckDeleted()
            If Row(PreInvoiceDAL.COL_NAME_PRE_INVOICE_CREATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PreInvoiceDAL.COL_NAME_PRE_INVOICE_CREATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PreInvoiceDAL.COL_NAME_PRE_INVOICE_CREATION_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ScDisplayDate As DateType
        Get
            CheckDeleted()
            If Row(PreInvoiceDAL.COL_NAME_SC_DISPLAY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PreInvoiceDAL.COL_NAME_SC_DISPLAY_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PreInvoiceDAL.COL_NAME_SC_DISPLAY_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property BatchNumber As String
        Get
            CheckDeleted()
            If Row(PreInvoiceDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PreInvoiceDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PreInvoiceDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PreInvoiceStatusId As Guid
        Get
            CheckDeleted()
            If Row(PreInvoiceDAL.COL_NAME_PRE_INVOICE_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PreInvoiceDAL.COL_NAME_PRE_INVOICE_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PreInvoiceDAL.COL_NAME_PRE_INVOICE_STATUS_ID, Value)
        End Set
    End Property

    Public Property TotalAuthAmount As DecimalType
        Get
            CheckDeleted()
            If Row(PreInvoiceDAL.COL_NAME_TOTAL_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PreInvoiceDAL.COL_NAME_TOTAL_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PreInvoiceDAL.COL_NAME_TOTAL_AMOUNT, Value)
        End Set
    End Property

    Public ReadOnly Property TotalAmount As DecimalType
        Get
            If TotalAndBonusPopulated Then
                Return _totalAmount
            Else
                PopulateTotalAndBonus()
                Return _totalAmount
            End If
        End Get
    End Property

    Public ReadOnly Property TotalBonusAmount As DecimalType
        Get
            If TotalAndBonusPopulated Then
                Return _totalBonusAmount
            Else
                PopulateTotalAndBonus()
                Return _totalBonusAmount
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property TotalClaims As DecimalType
        Get
            CheckDeleted()
            If Row(PreInvoiceDAL.COL_NAME_TOTAL_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PreInvoiceDAL.COL_NAME_TOTAL_CLAIMS), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PreInvoiceDAL.COL_NAME_TOTAL_CLAIMS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(PreInvoiceDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PreInvoiceDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PreInvoiceDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PreInvoiceDAL
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

    Public Shared Function LoadPreInvoiceProcess(companyId As Guid, statusId As Guid, batchNumber As String, createdDateFrom As String, createdDateTo As String) As PreinvoiceSearchDV

        Dim dal As New PreInvoiceDAL

        Return New PreinvoiceSearchDV(dal.LoadPreInvoiceProcess(companyId, statusId, batchNumber, createdDateFrom, createdDateTo).Tables(0))

    End Function

    Public Shared Function GeneratePreInvoice(companyCode As String) As String

        Dim dal As New PreInvoiceDAL

        Dim errorMsg As String = dal.GeneratePreInvoice(companyCode)

        Return errorMsg

    End Function

#End Region

#Region "Public Methods"
    Public Shared Function GetCountryId(companyId As Guid) As Guid
        Return New Company(companyId).CountryId
    End Function

    Public Shared Function ValidateServiceCenterCode(ServiceCenterCode As String, countryId As Guid) As Boolean
        Dim ds As DataSet = ServiceCenter.GetServiceCenterForWS(ServiceCenterCode, countryId)
        If ds Is Nothing OrElse ds.Tables.Count <= 0 OrElse ds.Tables(0).Rows.Count <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function GetCompanyId(Companycode As String) As Guid
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Dim userAssignedCompaniesDv As DataView = oUser.GetSelectedAssignedCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
        Dim companyId As System.Guid = Guid.Empty

        For i = 0 To userAssignedCompaniesDv.Count - 1
            Dim oCompanyId As New Guid(CType(userAssignedCompaniesDv.Table.Rows(i)("COMPANY_ID"), Byte()))
            If Not oCompanyId = Nothing AndAlso userAssignedCompaniesDv.Table.Rows(i)("CODE").Equals(Companycode.ToUpper) Then
                companyId = oCompanyId
                Exit For
            End If
        Next

        If companyId.Equals(Guid.Empty) Then
            Throw New BOValidationException("GetPreInvoice Error: Invalid Company Code ", Common.ErrorCodes.WS_INVALID_COMPANY_CODE)
        Else
            Return companyId
        End If
    End Function

    Public Shared Function GetPreInvoiceBAL(CompanyCode As String, ServiceCenterCode As String, SCPreInvoiceDateFrom As DateTime, SCPreInvoiceDateTo As DateTime) As DataSet
        Dim dal As New PreInvoiceDAL
        Return dal.GetPreInvoiceDAL(CompanyCode, ServiceCenterCode, SCPreInvoiceDateFrom, SCPreInvoiceDateTo)
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Class PreinvoiceSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PRE_INVOICE_ID As String = "Pre_invoice_id"
        Public Const COL_BATCH_NUMBER As String = "batch_number"
        Public Const COL_STATUS As String = "pre_invoice_Status_id"
        Public Const COL_SC_DISPLAY_DATE As String = "sc_display_date"
        Public Const COL_PRE_INVOICE_CREATION_DATE As String = "pre_invoice_creation_date"
        Public Const COL_CLAIMS_COUNT = "claims_count"
        Public Const COL_TOTAL_AUTH_AMOUNT = "total_auth_amount"
        Public Const COL_BONUS_AMOUNT = "bonus_amount"
        Public Const COL_TOTAL_AMOUNT = "total_amount"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        'Public Shared ReadOnly Property PreInvoiceId(ByVal row) As Guid
        '    Get
        '        Return New Guid(CType(row(COL_PRE_INVOICE_ID), Byte()))
        '    End Get
        'End Property

    End Class
#End Region

End Class



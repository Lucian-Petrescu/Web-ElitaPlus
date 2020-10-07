'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/29/2015)  ********************

Public Class AfaReportingRates
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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
            Dim dal As New AFARepRateDAL
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
            Dim dal As New AFARepRateDAL
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
            If Row(AFARepRateDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AFARepRateDAL.COL_NAME_AFA_REPORTING_RATE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AfaInvoiceRateId As Guid
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_AFA_INVOICE_RATE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AFARepRateDAL.COL_NAME_AFA_INVOICE_RATE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_AFA_INVOICE_RATE_ID, Value)
        End Set
    End Property



    Public Property RiskFee As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_RISK_FEE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_RISK_FEE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_RISK_FEE, Value)
        End Set
    End Property



    Public Property SpmCoe As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_SPM_COE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_SPM_COE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_SPM_COE, Value)
        End Set
    End Property



    Public Property FullfillmentNotification As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_FULLFILLMENT_NOTIFICATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_FULLFILLMENT_NOTIFICATION), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_FULLFILLMENT_NOTIFICATION, Value)
        End Set
    End Property



    Public Property MarketingExpenses As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_MARKETING_EXPENSES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_MARKETING_EXPENSES), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_MARKETING_EXPENSES, Value)
        End Set
    End Property



    Public Property PremiumTaxes As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_PREMIUM_TAXES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_PREMIUM_TAXES), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_PREMIUM_TAXES, Value)
        End Set
    End Property



    Public Property LossReserveCost As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_LOSS_RESERVE_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_LOSS_RESERVE_COST), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_LOSS_RESERVE_COST, Value)
        End Set
    End Property



    Public Property Overhead As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_OVERHEAD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_OVERHEAD), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_OVERHEAD, Value)
        End Set
    End Property



    Public Property GeneralExpenses As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_GENERAL_EXPENSES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_GENERAL_EXPENSES), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_GENERAL_EXPENSES, Value)
        End Set
    End Property



    Public Property Assessments As DecimalType
        Get
            CheckDeleted()
            If row(AFARepRateDAL.COL_NAME_ASSESSMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AFARepRateDAL.COL_NAME_ASSESSMENTS), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_ASSESSMENTS, Value)
        End Set
    End Property


    Public Property Lae As DecimalType
        Get
            CheckDeleted()
            If Row(AFARepRateDAL.COL_NAME_LAE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AFARepRateDAL.COL_NAME_LAE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFARepRateDAL.COL_NAME_LAE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AFARepRateDAL
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
    Public Shared Function getList(ByVal afaInvoiceRateId As Guid) As RptRateSearchDV
        Try
            Dim dal As New AFARepRateDAL
            Return New RptRateSearchDV(dal.LoadList(afaInvoiceRateId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"
    Public Class RptRateSearchDV
        Inherits DataView

        Public Const COL_AFA_REPORTING_RATE_ID As String = "afa_reporting_rate_id"
        Public Const COL_AFA_INVOICE_RATE_ID As String = "afa_invoice_rate_id"
        Public Const COL_RISK_FEE As String = "risk_fee"
        Public Const COL_SPM_COE As String = "spm_coe"
        Public Const COL_FULLFILLMENT_NOTIFICATION As String = "fullfillment_notification"
        Public Const COL_MARKETING_EXPENSES As String = "marketing_expenses"
        Public Const COL_PREMIUM_TAXES As String = "premium_taxes"
        Public Const COL_LOSS_RESERVE_COST As String = "loss_reserve_cost"
        Public Const COL_OVERHEAD As String = "overhead"
        Public Const COL_GENERAL_EXPENSES As String = "general_expenses"
        Public Const COL_ASSESSMENTS As String = "assessments"
        Public Const COL_LAE As String = "lae"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class


    Public Shared Sub AddNewRowToSearchDV(ByRef dv As RptRateSearchDV, ByVal NewBO As AfaReportingRates)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(RptRateSearchDV.COL_AFA_REPORTING_RATE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(RptRateSearchDV.COL_AFA_INVOICE_RATE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(RptRateSearchDV.COL_RISK_FEE, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_SPM_COE, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_FULLFILLMENT_NOTIFICATION, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_MARKETING_EXPENSES, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_PREMIUM_TAXES, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_LOSS_RESERVE_COST, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_OVERHEAD, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_GENERAL_EXPENSES, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_ASSESSMENTS, GetType(Long))
                dt.Columns.Add(RptRateSearchDV.COL_LAE, GetType(Long))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(RptRateSearchDV.COL_AFA_REPORTING_RATE_ID) = NewBO.Id.ToByteArray
            row(RptRateSearchDV.COL_AFA_INVOICE_RATE_ID) = NewBO.AfaInvoiceRateId.ToByteArray

            If Not NewBO.RiskFee Is Nothing Then
                row(RptRateSearchDV.COL_RISK_FEE) = NewBO.RiskFee.Value
            End If
            If Not NewBO.SpmCoe Is Nothing Then
                row(RptRateSearchDV.COL_SPM_COE) = NewBO.SpmCoe.Value
            End If
            If Not NewBO.FullfillmentNotification Is Nothing Then
                row(RptRateSearchDV.COL_FULLFILLMENT_NOTIFICATION) = NewBO.FullfillmentNotification.Value
            End If
            If Not NewBO.MarketingExpenses Is Nothing Then
                row(RptRateSearchDV.COL_MARKETING_EXPENSES) = NewBO.MarketingExpenses.Value
            End If
            If Not NewBO.PremiumTaxes Is Nothing Then
                row(RptRateSearchDV.COL_PREMIUM_TAXES) = NewBO.PremiumTaxes.Value
            End If
            If Not NewBO.LossReserveCost Is Nothing Then
                row(RptRateSearchDV.COL_LOSS_RESERVE_COST) = NewBO.LossReserveCost.Value
            End If
            If Not NewBO.Overhead Is Nothing Then
                row(RptRateSearchDV.COL_OVERHEAD) = NewBO.Overhead.Value
            End If
            If Not NewBO.GeneralExpenses Is Nothing Then
                row(RptRateSearchDV.COL_GENERAL_EXPENSES) = NewBO.GeneralExpenses.Value
            End If
            If Not NewBO.Assessments Is Nothing Then
                row(RptRateSearchDV.COL_ASSESSMENTS) = NewBO.Assessments.Value
            End If
            If Not NewBO.Lae Is Nothing Then
                row(RptRateSearchDV.COL_LAE) = NewBO.Lae.Value
            End If
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New RptRateSearchDV(dt)
        End If
    End Sub


#End Region


End Class
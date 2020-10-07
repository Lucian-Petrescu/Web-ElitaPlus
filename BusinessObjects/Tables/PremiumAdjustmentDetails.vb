'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/19/2004)  ********************

Public Class PremiumAdjustmentDetails
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
            Dim dal As New PremiumAdjustmentDetailsDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New PremiumAdjustmentDetailsDAL
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

#Region "CONSTANTS"

    'Public Const COL_BILLING_PLAN_ID As String = "BILLING_PLAN_ID"
    'Public Const COL_DEALER_GROUP_ID As String = "DEALER_GROUP_ID"
    Public Const COL_DEALER_ID As String = "DEALER_ID"
    ' Public Const COL_CODE As String = "CODE"
    'Public Const COL_DESCRIPTION As String = "DESCRIPTION"
#End Region



#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(PremiumAdjustmentDetailsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_PREMIUM_ADJUSTMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProcessDate As DateType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_PROCESS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_PROCESS_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_PROCESS_DATE, Value)
        End Set
    End Property


    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedGrossAmtReceived As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_GROSS_AMT_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_GROSS_AMT_RECEIVED), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_GROSS_AMT_RECEIVED, Value)
        End Set
    End Property
   

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedPremium As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREMIUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREMIUM), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREMIUM, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedCommission As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_COMMISSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_COMMISSION), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_COMMISSION, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedPremiumTax1 As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX1) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX1), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX1, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedPremiumTax2 As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX2) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX2), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX2, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedPremiumTax3 As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX3) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX3), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX3, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedPremiumTax4 As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX4) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX4), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX4, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedPremiumTax5 As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX5) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX5), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX5, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustedPremiumTax6 As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX6) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX6), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentDetailsDAL.COL_NAME_ADJUSTED_PREM_TAX6, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PremiumAdjustmentDetailsDAL
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


    Public Shared Sub Delete(ByVal Premium_Adjustment_Id As Guid)

        Dim dal As New PremiumAdjustmentDetailsDAL
        dal.Delete(Premium_Adjustment_Id)
    End Sub




#End Region

#Region "DataView Retrieveing Methods"


    Public Shared Function getList(ByVal DealerId As Guid, ByVal CompanyId As Guid) As PremiumAdjustmentSearchDV
        Try
            Dim dal As New PremiumAdjustmentDetailsDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Return New PremiumAdjustmentSearchDV(dal.LoadList(DealerId, CompanyId, compIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As PremiumAdjustmentSearchDV, ByVal bo As PremiumAdjustmentDetails) As PremiumAdjustmentSearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(PremiumAdjustmentSearchDV.COL_PREMIUM_ADJUSTMENT_ID) = bo.Id.ToByteArray
            row(PremiumAdjustmentSearchDV.COL_DEALER_ID) = bo.DealerId.ToByteArray
            'row(PremiumAdjustmentSearchDV.COL_PROCESS_DATE) = bo.ProcessDate
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_PREMIUM) = bo.AdjustedPremium
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_COMMISSION) = bo.AdjustedCommission
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_PREM_TAX1) = bo.AdjustedPremiumTax1
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_PREM_TAX2) = bo.AdjustedPremiumTax2
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_PREM_TAX3) = bo.AdjustedPremiumTax3
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_PREM_TAX4) = bo.AdjustedPremiumTax4
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_PREM_TAX5) = bo.AdjustedPremiumTax5
            'row(PremiumAdjustmentSearchDV.COL_ADJUSTED_PREM_TAX6) = bo.AdjustedPremiumTax6
            'row(BillingPlanDAL.COL_NAME_ACCTING_BY_GROUP_DESC) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_YESNO, bo.AcctingByGroupId)
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function
#End Region

#Region "PremiumAdjustmentSearchDV"
    Public Class PremiumAdjustmentSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PREMIUM_ADJUSTMENT_ID As String = "premium_adjustment_id"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_COMPANY_CODE As String = "company_code"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_PROCESS_DATE As String = "process_date"
        Public Const COL_ADJUSTED_GROSS_AMT_RECEIVED As String = "adjusted_gross_amt_received"
        Public Const COL_ADJUSTED_PREMIUM As String = "adjusted_premium"
        Public Const COL_ADJUSTED_COMMISSION As String = "adjusted_commission"
        Public Const COL_ADJUSTED_PREM_TAX1 As String = "adjusted_prem_tax1"
        Public Const COL_ADJUSTED_PREM_TAX2 As String = "adjusted_prem_tax2"
        Public Const COL_ADJUSTED_PREM_TAX3 As String = "adjusted_prem_tax3"
        Public Const COL_ADJUSTED_PREM_TAX4 As String = "adjusted_prem_tax4"
        Public Const COL_ADJUSTED_PREM_TAX5 As String = "adjusted_prem_tax5"
        Public Const COL_ADJUSTED_PREM_TAX6 As String = "adjusted_prem_tax6"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region


    

End Class




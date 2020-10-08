'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/27/2011)  ********************
Imports Common = Assurant.ElitaPlus.Common
Public Class CcBillingSchedule
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
            Dim dal As New CcBillingScheduleDAL
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
            Dim dal As New CcBillingScheduleDAL
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

#Region "Constants"
    Public Const COL_NAME_BILLING_DATE = "Description"
    Public Const COL_NAME_CC_BILLING_SCHEDULE_IDD = "CC_BILLING_SCHEDULE_ID"
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
    Private mIsDateEnable As Boolean
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(CcBillingScheduleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CcBillingScheduleDAL.COL_NAME_CC_BILLING_SCHEDULE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyCreditCardId As Guid
        Get
            CheckDeleted()
            If row(CcBillingScheduleDAL.COL_NAME_COMPANY_CREDIT_CARD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CcBillingScheduleDAL.COL_NAME_COMPANY_CREDIT_CARD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CcBillingScheduleDAL.COL_NAME_COMPANY_CREDIT_CARD_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BillingDate As DateType
        Get
            CheckDeleted()
            If row(CcBillingScheduleDAL.COL_NAME_BILLING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CcBillingScheduleDAL.COL_NAME_BILLING_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CcBillingScheduleDAL.COL_NAME_BILLING_DATE, Value)
        End Set
    End Property

    Public Property isDateEnable As Boolean
        Get
            Return mIsDateEnable
        End Get
        Set
            mIsDateEnable = Value
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CcBillingScheduleDAL
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

    Private Shared Function GetGetCCSchedulingInfoList(parent As CompanyCreditCard, companyCreditCardId As Guid) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(CCSchedulingDatesInfoList)) Then
                Dim dal As New CcBillingScheduleDAL
                dal.LoadList(companyCreditCardId, parent.Dataset)
                parent.AddChildrenCollection(GetType(CCSchedulingDatesInfoList))
            End If
            'Return New CovLossList(parent)
            Return parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function GetBillingYears(companyCreditCardId As Guid) As DataView
        Try
            Dim dal As New CcBillingScheduleDAL
            Dim ds As DataSet

            ds = dal.GetBillingYears(companyCreditCardId)
            Return (ds.Tables(CcBillingScheduleDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetBillingYearsByUser(userId As Guid) As DataView
        Try
            Dim dal As New CcBillingScheduleDAL
            Dim ds As DataSet

            ds = dal.GetBillingYearsByUser(userId)
            Return (ds.Tables(CcBillingScheduleDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetCCBillingScheduleDates(companyCreditCardId As Guid, Year As String) As DataView
        Try
            Dim dal As New CcBillingScheduleDAL
            Dim ds As DataSet

            ds = dal.GetCCSchedulingBillDates(companyCreditCardId, Year)
            Return (ds.Tables(CcBillingScheduleDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    '
    Public Shared Function GetLastBillingDate(companyCreditCardId As Guid) As CCSchedulingInfoSearchDV

        Try
            Dim dal As New CcBillingScheduleDAL
            'Dim LastDate As String = CType(AccountingCloseInfo.AccountingCloseInfoSearchDV.COL_NAME_CLOSE_DATE, Date)

            Return New CCSchedulingInfoSearchDV(dal.GetLastBillingDate(companyCreditCardId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetCCSchedulingBillDate(companyCreditCardId As Guid, forThisDate As Date, Optional ByVal minCloseDate As Boolean = False) As Date
        Try
            Dim dal As New CcBillingScheduleDAL
            Dim dateRow As DataRow = dal.GetCCSchedulingBillDate(companyCreditCardId, forThisDate)
            Dim dtInqAccountingCloseDate As Date

            If minCloseDate = True Then
                If Not dateRow Is Nothing AndAlso Not dateRow.Item(dal.COL_NAME_BILLING_DATE) Is System.DBNull.Value Then
                    Return CType(dateRow.Item(dal.COL_NAME_BILLING_DATE), Date)
                Else
                    dtInqAccountingCloseDate = GetMinCCSchedulingBillDate(companyCreditCardId)
                    Return dtInqAccountingCloseDate
                End If
            Else
                If Not dateRow Is Nothing AndAlso Not dateRow.Item(dal.COL_NAME_BILLING_DATE) Is System.DBNull.Value Then
                    Return CType(dateRow.Item(dal.COL_NAME_BILLING_DATE), Date)
                Else
                    'Throw New BOValidationException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND)
                    Throw New DataNotFoundException
                End If
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetMinCCSchedulingBillDate(companyCreditCardId As Guid) As Date
        Try
            Dim dal As New CcBillingScheduleDAL
            Dim dateRow As DataRow = dal.GetMinCCSchedulingBillDate(companyCreditCardId)
            If Not dateRow Is Nothing AndAlso Not dateRow.Item(dal.COL_NAME_BILLING_DATE) Is System.DBNull.Value Then
                Return CType(dateRow.Item(dal.COL_NAME_BILLING_DATE), Date)
            Else
                'Throw New BOValidationException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND)
                Throw New DataNotFoundException
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetAllCCSchedulingBillDates(companyCreditCardId As Guid) As DataView
        Try
            Dim dal As New CcBillingScheduleDAL
            Dim ds As Dataset

            ds = dal.GetAllCCSchedulingBillDates(companyCreditCardId)
            Return (ds.Tables(CcBillingScheduleDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Class AccountingCloseInfoSearchDV
        Inherits DataView

#Region "Constants"
        'Public Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID = "Accounting_Close_Info_id"
        Public Const COL_NAME_CLOSE_DATE = "Description"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "DataView Retrieveing Methods"

    Public Class SchedulingInfoSearchDV
        Inherits DataView

#Region "Constants"
        'Public Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID = "Accounting_Close_Info_id"
        Public Const COL_NAME_CLOSE_DATE = "Description"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "List Methods"
    Public Class CCSchedulingDatesInfoList
        Inherits BusinessObjectListBase
        Public Sub New(parent As CompanyCreditCard, companyCreditCardId As Guid)
            MyBase.New(GetGetCCSchedulingInfoList(parent, companyCreditCardId), GetType(CcBillingSchedule), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(CcBillingScheduleInfoId As Guid) As CcBillingSchedule
            Dim bo As CcBillingSchedule
            For Each bo In Me
                If bo.Id.Equals(CcBillingScheduleInfoId) Then Return bo
            Next
            Return Nothing
        End Function

        'Public Function FindByPosition(ByVal position As LongType) As AccountingCloseInfo
        '    Dim bo As AccountingCloseInfo
        '    For Each bo In Me
        '        If bo.Position.Equals(position) Then Return bo
        '    Next
        '    Return Nothing
        'End Function

    End Class

#End Region

#Region ""

    Public Class CCSchedulingInfoSearchDV
        Inherits DataView

#Region "Constants"
        'Public Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID = "Accounting_Close_Info_id"
        Public Const COL_NAME_CLOSE_DATE = "Description"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
End Class



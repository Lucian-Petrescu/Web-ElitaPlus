'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/9/2005)  ********************
Imports Common = Assurant.ElitaPlus.Common

Public Class AccountingCloseInfo
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
        Dim dal As New AccountingCloseInfoDAL
        'If Me.Dataset.Tables(dal.TABLE_NAME).Rows.Count < 12 Then
        Load()
        'End If
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AccountingCloseInfoDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New AccountingCloseInfoDAL
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

#Region "Constants"
    Public Const COL_NAME_CLOSE_DATE = "Description"
    Public Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID = "Accounting_Close_Info_id"
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
            If Row(AccountingCloseInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AccountingCloseInfoDAL.COL_NAME_ACCOUNTING_CLOSE_INFO_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(AccountingCloseInfoDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AccountingCloseInfoDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AccountingCloseInfoDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidateEntedClossingDate("")> _
    Public Property ClosingDate As DateType
        Get
            CheckDeleted()
            If Row(AccountingCloseInfoDAL.COL_NAME_CLOSING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AccountingCloseInfoDAL.COL_NAME_CLOSING_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AccountingCloseInfoDAL.COL_NAME_CLOSING_DATE, Value)
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
                Dim dal As New AccountingCloseInfoDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
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

    Private Shared Function GetAccountingCloseInfoList(parent As Company, cmpId As Guid) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(AccountingCloseInfoList)) OrElse If(parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME) Is Nothing, True, parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME).Rows.Count <= 0) Then
                Dim dal As New AccountingCloseInfoDAL
                dal.LoadList(cmpId, parent.Dataset)
                parent.AddChildrenCollection(GetType(AccountingCloseInfoList))
            End If
            'Return New CovLossList(parent)
            Return parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Shared Function GetAccountingCloseInfoListCountNumber(parent As Company, cmpId As Guid) As DataTable
        Try
            Return parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function GetClosingYears(companyId As Guid) As DataView
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim ds As Dataset

            ds = dal.GetClosingYears(companyId)
            Return (ds.Tables(AccountingCloseInfoDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClosingYearsByUser(userId As Guid) As DataView
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim ds As Dataset

            ds = dal.GetClosingYearsByUser(userId)
            Return (ds.Tables(AccountingCloseInfoDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetAccountingCloseDates(companyId As Guid, Year As String) As DataView
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim ds As Dataset

            ds = dal.GetAccountingCloseDates(companyId, Year)
            Return (ds.Tables(AccountingCloseInfoDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    '
    Public Shared Function GetLastClosingDate(companyId As Guid) As AccountingCloseInfoSearchDV

        Try
            Dim dal As New AccountingCloseInfoDAL
            'Dim LastDate As String = CType(AccountingCloseInfo.AccountingCloseInfoSearchDV.COL_NAME_CLOSE_DATE, Date)

            Return New AccountingCloseInfoSearchDV(dal.GetLastClosingDate(companyId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetAccountingCloseDate(companyId As Guid, forThisDate As Date, Optional ByVal minCloseDate As Boolean = False) As Date
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim dateRow As DataRow = dal.GetAccountingCloseDate(companyId, forThisDate)
            Dim dtInqAccountingCloseDate As Date

            If minCloseDate = True Then
                If dateRow IsNot Nothing AndAlso dateRow.Item(dal.COL_NAME_CLOSING_DATE) IsNot DBNull.Value Then
                    Return CType(dateRow.Item(dal.COL_NAME_CLOSING_DATE), Date)
                Else
                    dtInqAccountingCloseDate = GetMinAccountingCloseDate(companyId)
                    Return dtInqAccountingCloseDate                    
                End If
            Else
                If dateRow IsNot Nothing AndAlso dateRow.Item(dal.COL_NAME_CLOSING_DATE) IsNot DBNull.Value Then
                    Return CType(dateRow.Item(dal.COL_NAME_CLOSING_DATE), Date)
                Else
                    'Throw New BOValidationException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND)
                    Throw New DataNotFoundException
                End If
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetMinAccountingCloseDate(companyId As Guid) As Date
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim dateRow As DataRow = dal.GetMinAccountingCloseDate(companyId)
            If dateRow IsNot Nothing AndAlso dateRow.Item(dal.COL_NAME_CLOSING_DATE) IsNot DBNull.Value Then
                Return CType(dateRow.Item(dal.COL_NAME_CLOSING_DATE), Date)
            Else
                'Throw New BOValidationException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND)
                Throw New DataNotFoundException
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetAllAccountingCloseDates(companyId As Guid) As DataView
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim ds As Dataset

            ds = dal.GetAllAccountingCloseDates(companyId)
            Return (ds.Tables(AccountingCloseInfoDAL.TABLE_NAME).DefaultView)

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
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
              Public NotInheritable Class ValidateEntedClossingDate
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DATE_ENTER_CANNOT_BE_LOWER_THAT_TODAYS_DATE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As AccountingCloseInfo = CType(objectToValidate, AccountingCloseInfo)
            Dim EntedClossingDate As Date

            If Not obj.mIsDateEnable Then
                Return True
            End If

            EntedClossingDate = obj.GetShortDate(obj.ClosingDate.Value)

            If EntedClossingDate < Now Then
                Return False
            End If

            Return True

        End Function

    End Class
#End Region
#Region "List Methods"

    Public Class AccountingCloseInfoListCount
        Inherits BusinessObjectListBase
        Public Sub New(parent As Company, cmpId As Guid)
            MyBase.New(GetAccountingCloseInfoListCountNumber(parent, cmpId), GetType(AccountingCloseInfo), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return True
        End Function
    End Class

    Public Class AccountingCloseInfoList
        Inherits BusinessObjectListBase
        Public Sub New(parent As Company, cmpId As Guid)
            MyBase.New(GetAccountingCloseInfoList(parent, cmpId), GetType(AccountingCloseInfo), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(AccountingCloseInfoId As Guid) As AccountingCloseInfo
            Dim bo As AccountingCloseInfo
            For Each bo In Me
                If bo.Id.Equals(AccountingCloseInfoId) Then Return bo
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
End Class




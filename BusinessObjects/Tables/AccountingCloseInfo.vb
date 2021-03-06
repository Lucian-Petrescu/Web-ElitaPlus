'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/9/2005)  ********************
Imports Common = Assurant.ElitaPlus.Common

Public Class AccountingCloseInfo
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
        Dim dal As New AccountingCloseInfoDAL
        'If Me.Dataset.Tables(dal.TABLE_NAME).Rows.Count < 12 Then
        Me.Load()
        'End If
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AccountingCloseInfoDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New AccountingCloseInfoDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(AccountingCloseInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AccountingCloseInfoDAL.COL_NAME_ACCOUNTING_CLOSE_INFO_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(AccountingCloseInfoDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AccountingCloseInfoDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AccountingCloseInfoDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidateEntedClossingDate("")> _
    Public Property ClosingDate() As DateType
        Get
            CheckDeleted()
            If Row(AccountingCloseInfoDAL.COL_NAME_CLOSING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AccountingCloseInfoDAL.COL_NAME_CLOSING_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AccountingCloseInfoDAL.COL_NAME_CLOSING_DATE, Value)
        End Set
    End Property

    Public Property isDateEnable() As Boolean
        Get
            Return mIsDateEnable
        End Get
        Set(ByVal Value As Boolean)
            mIsDateEnable = Value
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AccountingCloseInfoDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
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

    Private Shared Function GetAccountingCloseInfoList(ByVal parent As Company, ByVal cmpId As Guid) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(AccountingCloseInfoList)) Or If(parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME) Is Nothing, True, parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME).Rows.Count <= 0) Then
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

    Private Shared Function GetAccountingCloseInfoListCountNumber(ByVal parent As Company, ByVal cmpId As Guid) As DataTable
        Try
            Return parent.Dataset.Tables(AccountingCloseInfoDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function GetClosingYears(ByVal companyId As Guid) As DataView
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim ds As Dataset

            ds = dal.GetClosingYears(companyId)
            Return (ds.Tables(AccountingCloseInfoDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClosingYearsByUser(ByVal userId As Guid) As DataView
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim ds As Dataset

            ds = dal.GetClosingYearsByUser(userId)
            Return (ds.Tables(AccountingCloseInfoDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetAccountingCloseDates(ByVal companyId As Guid, ByVal Year As String) As DataView
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
    Public Shared Function GetLastClosingDate(ByVal companyId As Guid) As AccountingCloseInfoSearchDV

        Try
            Dim dal As New AccountingCloseInfoDAL
            'Dim LastDate As String = CType(AccountingCloseInfo.AccountingCloseInfoSearchDV.COL_NAME_CLOSE_DATE, Date)

            Return New AccountingCloseInfoSearchDV(dal.GetLastClosingDate(companyId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetAccountingCloseDate(ByVal companyId As Guid, ByVal forThisDate As Date, Optional ByVal minCloseDate As Boolean = False) As Date
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim dateRow As DataRow = dal.GetAccountingCloseDate(companyId, forThisDate)
            Dim dtInqAccountingCloseDate As Date

            If minCloseDate = True Then
                If Not dateRow Is Nothing AndAlso Not dateRow.Item(dal.COL_NAME_CLOSING_DATE) Is System.DBNull.Value Then
                    Return CType(dateRow.Item(dal.COL_NAME_CLOSING_DATE), Date)
                Else
                    dtInqAccountingCloseDate = GetMinAccountingCloseDate(companyId)
                    Return dtInqAccountingCloseDate                    
                End If
            Else
                If Not dateRow Is Nothing AndAlso Not dateRow.Item(dal.COL_NAME_CLOSING_DATE) Is System.DBNull.Value Then
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
    Public Shared Function GetMinAccountingCloseDate(ByVal companyId As Guid) As Date
        Try
            Dim dal As New AccountingCloseInfoDAL
            Dim dateRow As DataRow = dal.GetMinAccountingCloseDate(companyId)
            If Not dateRow Is Nothing AndAlso Not dateRow.Item(dal.COL_NAME_CLOSING_DATE) Is System.DBNull.Value Then
                Return CType(dateRow.Item(dal.COL_NAME_CLOSING_DATE), Date)
            Else
                'Throw New BOValidationException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND)
                Throw New DataNotFoundException
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function GetAllAccountingCloseDates(ByVal companyId As Guid) As DataView
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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
              Public NotInheritable Class ValidateEntedClossingDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DATE_ENTER_CANNOT_BE_LOWER_THAT_TODAYS_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
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
        Public Sub New(ByVal parent As Company, ByVal cmpId As Guid)
            MyBase.New(GetAccountingCloseInfoListCountNumber(parent, cmpId), GetType(AccountingCloseInfo), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function
    End Class

    Public Class AccountingCloseInfoList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As Company, ByVal cmpId As Guid)
            MyBase.New(GetAccountingCloseInfoList(parent, cmpId), GetType(AccountingCloseInfo), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(ByVal AccountingCloseInfoId As Guid) As AccountingCloseInfo
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




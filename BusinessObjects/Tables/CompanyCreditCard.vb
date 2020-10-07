'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/12/2010)  ********************

Public Class CompanyCreditCard
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO by CreditCardFormatId and companyId
    Public Sub New(ByVal CreditCardFormatId As Guid, ByVal CompanyId As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(CreditCardFormatId, CompanyId)
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
            Dim dal As New CompanyCreditCardDAL
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
            Dim dal As New CompanyCreditCardDAL
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

    Protected Sub Load(ByVal CreditCardFormatId As Guid, ByVal CompanyId As Guid)
        Try
            Dim dal As New CompanyCreditCardDAL
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
                dal.Load(Dataset, CreditCardFormatId, CompanyId)
                Row = FindRow(CreditCardFormatId, dal.COL_NAME_CREDIT_CARD_FORMAT_ID, Dataset.Tables(dal.TABLE_NAME))
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
            If row(CompanyCreditCardDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CompanyCreditCardDAL.COL_NAME_COMPANY_CREDIT_CARD_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If row(CompanyCreditCardDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CompanyCreditCardDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyCreditCardDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CreditCardFormatId As Guid
        Get
            CheckDeleted()
            If row(CompanyCreditCardDAL.COL_NAME_CREDIT_CARD_FORMAT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CompanyCreditCardDAL.COL_NAME_CREDIT_CARD_FORMAT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyCreditCardDAL.COL_NAME_CREDIT_CARD_FORMAT_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=31), ValueMandatory("")> _
    Public Property BillingDate As LongType
        Get
            CheckDeleted()
            If Row(CompanyCreditCardDAL.COL_NAME_BILLING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CompanyCreditCardDAL.COL_NAME_BILLING_DATE), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CompanyCreditCardDAL.COL_NAME_BILLING_DATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CompanyCreditCardDAL
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

#Region "CCFORMATSearchDV"
    Public Class CompanyCreditCardSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMPANY_CREDIT_CARD_ID As String = "company_credit_card_id"
        Public Const COL_CREDIT_CARD_FORMAT_ID As String = "credit_card_format_id"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_COMPANY_CODE As String = "company_code"
        Public Const COL_CREDIT_CARD_TYPE As String = "Credit_Card_Type"
        Public Const COL_BILLING_DATE As String = "Billing_Date"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As CompanyCreditCardSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(CompanyCreditCardSearchDV.COL_COMPANY_CREDIT_CARD_ID) = (New Guid()).ToByteArray
            row(CompanyCreditCardSearchDV.COL_CREDIT_CARD_FORMAT_ID) = Guid.Empty.ToByteArray
            row(CompanyCreditCardSearchDV.COL_COMPANY_ID) = Guid.Empty.ToByteArray
            row(CompanyCreditCardSearchDV.COL_COMPANY_CODE) = ""
            row(CompanyCreditCardSearchDV.COL_CREDIT_CARD_TYPE) = ""
            row(CompanyCreditCardSearchDV.COL_BILLING_DATE) = Long.MinValue
            dt.Rows.Add(row)
            Return New CompanyCreditCardSearchDV(dt)
        End Function
    End Class
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(Optional alCompanies As ArrayList = Nothing) As CompanyCreditCardSearchDV
        Try
            Dim dal As New CompanyCreditCardDAL
            If alCompanies Is Nothing Then
                Return New CompanyCreditCardSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.Companies).Tables(0))
            Else
                Return New CompanyCreditCardSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, alCompanies).Tables(0))
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As CompanyCreditCardSearchDV, ByVal NewCompanyCreditCardBO As CompanyCreditCard)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        dv.Sort = ""
        If NewCompanyCreditCardBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(CompanyCreditCardSearchDV.COL_COMPANY_CREDIT_CARD_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CompanyCreditCardSearchDV.COL_CREDIT_CARD_FORMAT_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CompanyCreditCardSearchDV.COL_COMPANY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CompanyCreditCardSearchDV.COL_BILLING_DATE, GetType(Integer))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(CompanyCreditCardSearchDV.COL_COMPANY_CREDIT_CARD_ID) = NewCompanyCreditCardBO.Id.ToByteArray
            row(CompanyCreditCardSearchDV.COL_CREDIT_CARD_FORMAT_ID) = NewCompanyCreditCardBO.CreditCardFormatId.ToByteArray
            row(CompanyCreditCardSearchDV.COL_COMPANY_ID) = NewCompanyCreditCardBO.CompanyId.ToByteArray
            row(CompanyCreditCardSearchDV.COL_BILLING_DATE) = DBNull.Value
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New CompanyCreditCardSearchDV(dt)
        End If
    End Sub

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As CompanyCreditCard) As CompanyCreditCardSearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(CompanyCreditCardDAL.COL_NAME_COMPANY_CREDIT_CARD_ID) = bo.Id.ToByteArray
            row(CompanyCreditCardDAL.COL_NAME_COMPANY_ID) = bo.CompanyId.ToByteArray
            row(CompanyCreditCardDAL.COL_NAME_CREDIT_CARD_FORMAT_ID) = bo.CreditCardFormatId.ToByteArray
            row(CompanyCreditCardDAL.COL_NAME_BILLING_DATE) = Long.MinValue
            dt.Rows.Add(row)
        End If

        'Return (dv)
        Return New CompanyCreditCardSearchDV(dt)
    End Function
#End Region

End Class



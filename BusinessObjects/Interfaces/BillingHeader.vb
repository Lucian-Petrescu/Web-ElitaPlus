'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/22/2008)  ********************

Public Class BillingHeader
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
            Dim dal As New BillingHeaderDAL
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
            Dim dal As New BillingHeaderDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(BillingHeaderDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(BillingHeaderDAL.COL_NAME_BILLING_HEADER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(BillingHeaderDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(BillingHeaderDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(BillingHeaderDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DateFileSent() As DateType
        Get
            CheckDeleted()
            If row(BillingHeaderDAL.COL_NAME_DATE_FILE_SENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(BillingHeaderDAL.COL_NAME_DATE_FILE_SENT), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(BillingHeaderDAL.COL_NAME_DATE_FILE_SENT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Filename() As String
        Get
            CheckDeleted()
            If Row(BillingHeaderDAL.COL_NAME_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingHeaderDAL.COL_NAME_FILENAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(BillingHeaderDAL.COL_NAME_FILENAME, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TotalBilledAmt() As DecimalType
        Get
            CheckDeleted()
            If row(BillingHeaderDAL.COL_NAME_TOTAL_BILLED_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(BillingHeaderDAL.COL_NAME_TOTAL_BILLED_AMT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(BillingHeaderDAL.COL_NAME_TOTAL_BILLED_AMT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property Status() As String
        Get
            CheckDeleted()
            If row(BillingHeaderDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(BillingHeaderDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(BillingHeaderDAL.COL_NAME_STATUS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=48)> _
    Public Property ReferenceNumber() As String
        Get
            CheckDeleted()
            If row(BillingHeaderDAL.COL_NAME_REFERENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(BillingHeaderDAL.COL_NAME_REFERENCE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(BillingHeaderDAL.COL_NAME_REFERENCE_NUMBER, Value)
        End Set
    End Property


#End Region

#Region "Properties External BOs"

    Public ReadOnly Property DealerCode() As String
        Get
            If DealerId.Equals(Guid.Empty) Then Return Nothing
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerId)
        End Get
    End Property

    Public ReadOnly Property DealerNameLoad() As String
        Get
            If DealerId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_DEALERS)
            Return LookupListNew.GetDescriptionFromId(dv, DealerId)
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New BillingHeaderDAL
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
#Region "BillingSearchDV"
    Public Class BillingSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_BILLING_HEADER_ID As String = "billing_header_id"
        Public Const COL_DEALER_CODE As String = "Dealer"
        Public Const COL_DATE_FILE_SENT As String = "Date_File_Sent"
        Public Const COL_TOTAL_BILLED_AMT As String = "Total_Billed_Amt"
        Public Const COL_STATUS As String = "Status"
        Public Const COL_SOURCE As String = "source"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal CompanyIds As ArrayList, ByVal DealerId As Guid, ByVal BeginDate As Date, ByVal EndDate As Date) As DataView
        Dim dal As New BillingHeaderDAL
        Dim ds As DataSet
        Try
            ds = dal.LoadList(CompanyIds, DealerId, BeginDate, EndDate)

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListByCompany(ByVal CompanyIds As ArrayList, ByVal BeginDate As Date, ByVal EndDate As Date) As DataView
        Dim dal As New BillingHeaderDAL
        Dim ds As DataSet
        Try
            ds = dal.LoadListByCompany(CompanyIds, BeginDate, EndDate)

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class



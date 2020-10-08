'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/12/2013)  ********************

Public Class InvoiceGroup
    Inherits BusinessObjectBase
#Region "Constants"
    Private Const SEARCH_EXCEPTION As String = "INVOICEGROUPLIST_FORM001"
#End Region
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
            Dim dal As New InvoiceGroupDAL
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
            Dim dal As New InvoiceGroupDAL
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
            If Row(InvoiceGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=80)> _
    Public Property InvoiceGroupNumber As String
        Get
            CheckDeleted()
            If Row(InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_NUMBER, Value)
        End Set
    End Property



    Public Property InvoiceGroupStatusId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_STATUS_ID, Value)
        End Set
    End Property

    Public ReadOnly Property ReceiptDate As Date
        Get
            CheckDeleted()
            If Row(InvoiceGroupDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceGroupDAL.COL_NAME_CREATED_DATE), Date)
            End If
        End Get

    End Property


    Public Property User As String
        Get
            CheckDeleted()
            If Row(InvoiceGroupDAL.COL_NAME_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceGroupDAL.COL_NAME_CREATED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceGroupDAL.COL_NAME_CREATED_BY, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceGroupDAL
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
    Public Shared Function getList(Invgrpnum As String, claimnumber As String, _
                                    oCountryId As Guid, groupnofrom As String, mobilenum As String, _
                                   duedate As String, svcname As String, groupnoto As String, _
                                  invoicenum As String, Invstatusid As Guid, Membershipnumber As String, Certificate As String, Optional ByVal btnsearchclick As Boolean = False) As InvoiceGroupSearchDV
        Try
            Dim dal As New InvoiceGroupDAL
            Dim oCountryIds As ArrayList = Nothing
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(InvoiceGroup), Nothing, "Search", Nothing)}

            'If DALBase.IsNothing(oCountryId) Then
            '    ' Get All User Countries
            '    oCountryIds = ElitaPlusIdentity.Current.ActiveUser.Countries
            '    oCountryId = Guid.Empty
            'Else
            '    oCountryIds = New ArrayList
            '    oCountryIds.Add(oCountryId)
            'End If



            If (Invgrpnum.Equals(String.Empty) AndAlso claimnumber.Equals(String.Empty) AndAlso oCountryId.Equals(Guid.Empty) AndAlso _
                groupnofrom.Equals(String.Empty) AndAlso mobilenum.Equals(String.Empty) AndAlso _
                duedate.Equals(String.Empty) AndAlso svcname.Equals(String.Empty) AndAlso groupnoto.Equals(String.Empty) AndAlso _
                invoicenum.Equals(String.Empty) AndAlso Invstatusid.Equals(Guid.Empty) AndAlso Membershipnumber.Equals(String.Empty) AndAlso Certificate.Equals(String.Empty)) Then

                If btnsearchclick Then
                    Throw New BOValidationException(errors, GetType(InvoiceGroup).FullName)
                End If




            Else
                Return New InvoiceGroupSearchDV(dal.LoadList(Invgrpnum, claimnumber, _
                                    oCountryId, groupnofrom, mobilenum, _
                                       duedate, svcname, groupnoto, _
                                      invoicenum, Invstatusid, Membershipnumber, Certificate).Tables(0))
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

   
#End Region

    Public Class InvoiceGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_INVOICE_GROUP_ID As String = InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_ID
        Public Const COL_INVOICE_GROUP_NUMBER As String = InvoiceGroupDAL.COL_NAME_INVOICE_GROUP_NUMBER
        Public Const COL_SERVICE_CENTER_NAME As String = "Service_Center_Name"
        Public Const COL_INVOICE_GROUP_CREATED_DATE As String = InvoiceGroupDAL.COL_NAME_CREATED_DATE
        Public Const COL_INVOICE_GROUP_USER As String = InvoiceGroupDAL.COL_NAME_CREATED_BY


#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property InvoiceGroupId(row) As Guid
            Get
                Return New Guid(CType(row(COL_INVOICE_GROUP_ID), Byte()))
            End Get
        End Property

    End Class


   
End Class









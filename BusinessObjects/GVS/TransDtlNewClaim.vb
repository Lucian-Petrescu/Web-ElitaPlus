'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/12/2008)  ********************

Public Class TransDtlNewClaim
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
            Dim dal As New TransDtlNewClaimDAL
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
            Dim dal As New TransDtlNewClaimDAL
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
            If row(TransDtlNewClaimDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlNewClaimDAL.COL_NAME_TRANS_DTL_NEW_CLAIM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property TransactionLogHeaderId() As Guid
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlNewClaimDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ItemNumber() As LongType
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TransDtlNewClaimDAL.COL_NAME_ITEM_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Response() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_RESPONSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_RESPONSE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_RESPONSE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property ResponseDetail() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_RESPONSE_DETAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_RESPONSE_DETAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_RESPONSE_DETAIL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property XmlClaimNumber() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property XmlCreatedDate() As DateType
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransDtlNewClaimDAL.COL_NAME_XML_CREATED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_CREATED_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property XmlServiceCenterCode() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_SERVICE_CENTER_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlCustomerName() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_CUSTOMER_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property XmlIdentificationNumber() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_IDENTIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_IDENTIFICATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_IDENTIFICATION_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlAddress1() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_ADDRESS1, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlAddress2() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_ADDRESS2, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlCity() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_CITY, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property XmlRegion() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_REGION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_REGION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=160)> _
    Public Property XmlPostalCode() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_POSTAL_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=320)> _
    Public Property XmlHomePhone() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_HOME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_HOME_PHONE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=240)> _
    Public Property XmlWorkPhone() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_WORK_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_WORK_PHONE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property XmlEmail() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_EMAIL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property XmlContactName() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_CONTACT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_CONTACT_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property XmlProductCode() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property XmlDescription() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlItemDescription() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_ITEM_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property XmlSerialNumber() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=320)> _
    Public Property XmlInvoiceNumber() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_INVOICE_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property XmlProblemDescription() As String
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlNewClaimDAL.COL_NAME_XML_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property XmlMethodOfRepair() As Guid
        Get
            CheckDeleted()
            If row(TransDtlNewClaimDAL.COL_NAME_XML_METHOD_OF_REPAIR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlNewClaimDAL.COL_NAME_XML_METHOD_OF_REPAIR), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransDtlNewClaimDAL.COL_NAME_XML_METHOD_OF_REPAIR, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransDtlNewClaimDAL
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

#End Region

End Class



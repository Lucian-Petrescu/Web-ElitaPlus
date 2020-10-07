'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/18/2012)  ********************

Public Class TransCcauthVscbrazil
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
            Dim dal As New TransCcauthVscbrazilDAL
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
            Dim dal As New TransCcauthVscbrazilDAL
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
            If row(TransCcauthVscbrazilDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransCcauthVscbrazilDAL.COL_NAME_TRANS_CCAUTH_VSCBRAZIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=400)> _
    Public Property CustomerName As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property DocumentNum As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_DOCUMENT_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_DOCUMENT_NUM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_DOCUMENT_NUM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property CertificateNum As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_CERTIFICATE_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_CERTIFICATE_NUM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_CERTIFICATE_NUM, Value)
        End Set
    End Property



    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(TransCcauthVscbrazilDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property



    Public Property NumOfInstallments As LongType
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_NUM_OF_INSTALLMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TransCcauthVscbrazilDAL.COL_NAME_NUM_OF_INSTALLMENTS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_NUM_OF_INSTALLMENTS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property NameOnCard As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_NAME_ON_CARD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_NAME_ON_CARD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_NAME_ON_CARD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property CardNum As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_CARD_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_CARD_NUM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_CARD_NUM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)>
    Public Property CardSecurityCode As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_CARD_SECURITY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_CARD_SECURITY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_CARD_SECURITY_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property DbsCompanyCode As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_COMPANY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_COMPANY_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property DbsProductCode As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_PRODUCT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_PRODUCT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property DbsSystemCode As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_SYSTEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_SYSTEM_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_SYSTEM_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)>
    Public Property DealerCode As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)>
    Public Property Email As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_EMAIL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_EMAIL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property MobileAreaCode As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_MOBILE_AREA_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_MOBILE_AREA_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_MOBILE_AREA_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property Mobile As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_MOBILE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_MOBILE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_MOBILE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property PhoneAreaCode As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_PHONE_AREA_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_PHONE_AREA_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_PHONE_AREA_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property Phone As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_PHONE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property WarrantySalesDate As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_WARRANTY_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_WARRANTY_SALES_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_WARRANTY_SALES_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=400)>
    Public Property CardOwnerTaxId As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_CARD_OWNER_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_CARD_OWNER_TAX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_CARD_OWNER_TAX_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property CardExpiration As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_CARD_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_CARD_EXPIRATION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_CARD_EXPIRATION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property DbsPaymentType As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_PAYMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_PAYMENT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_DBS_PAYMENT_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property DueDate As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_DUE_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_DUE_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=80)>
    Public Property ExpiredDate As String
        Get
            CheckDeleted()
            If Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_EXPIRED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransCcauthVscbrazilDAL.DATA_COL_NAME_EXPIRED_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.DATA_COL_NAME_EXPIRED_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property CardType As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_CARD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_CARD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_CARD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property AuthStatus As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_AUTH_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_AUTH_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_AUTH_STATUS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property AuthNum As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_AUTH_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_AUTH_NUM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_AUTH_NUM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property RejectCode As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1000)> _
    Public Property RejectMsg As String
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_REJECT_MSG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransCcauthVscbrazilDAL.COL_NAME_REJECT_MSG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_REJECT_MSG, Value)
        End Set
    End Property



    Public Property AuthDate As DateType
        Get
            CheckDeleted()
            If row(TransCcauthVscbrazilDAL.COL_NAME_AUTH_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransCcauthVscbrazilDAL.COL_NAME_AUTH_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransCcauthVscbrazilDAL.COL_NAME_AUTH_DATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransCcauthVscbrazilDAL
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

#End Region

End Class
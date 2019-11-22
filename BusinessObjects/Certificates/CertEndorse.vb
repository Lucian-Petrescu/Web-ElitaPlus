'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/16/2005)  ********************

Public Class CertEndorse
    Inherits BusinessObjectBase
#Region "Constants"
    Private Const ERR_ENDORSEMENT_NOT_ALLOWED As String = "ERR_ENDORSEMENT_NOT_ALLOWED"
    Private Const NAME_IS_REQUIRED As String = "NAME_IS_REQUIRED"
    Private Const MAX_MONTH_DIFF As Integer = 6
    Private Const MANUFACTURER As String = "M"
    Private Const MONTH As String = "M"

    Public Const DOC_TYPE_CPF As String = "CPF"
    Public Const DOC_TYPE_CNPJ As String = "CNPJ"
    Public Const DOC_TYPE_CON As String = "CON"

    Public Const VALIDATION_FLAG_FULL As String = "1"
    Public Const VALIDATION_FLAG_PARTIAL As String = "2"
    Public Const VALIDATION_FLAG_NONE As String = "3"
    Public Const VALIDATION_FLAG_CPF_CNPJ As String = "4"
    Public Const VALIDATION_FLAG_NO_VALIDATION As String = "5"

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New CertEndorseDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            If Me.Dataset.Tables(dal.TABLE_NAME).Rows.Count = 1 Then
                Me.Dataset.Tables(dal.TABLE_NAME).Rows(0).Delete()
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
            Dim dal As New CertEndorseDAL
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

    Private _ManufaturerWarranty As Boolean
    Private _dealerTypeCode As String = Nothing
    Private _dealerEndorsementFlag As String = "N"
#End Region

#Region "Attributes"
    Dim langPrefDesc As String
    Dim documentTypeCode As String
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub LoadNewTerm()

        Dim dNewExtendedEndate As Date
        Dim dNewCoverageEndate As Date
        Dim dOrgCoverageEndate As Date
        Dim intDuration As Integer

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.CoverageTypeCode = Codes.COVERAGE_TYPE__MANUFACTURER Then
                If Not LocateActiveClaimsByCoverageId(cov.Id, cov.BeginDate.Value, DateAdd("d", -1, DateAdd(MONTH, Me.TermPos, cov.BeginDate.Value)), True) Then
                    dOrgCoverageEndate = cov.EndDate.Value
                    cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, Me.TermPos, cov.BeginDate.Value)))
                    dNewCoverageEndate = cov.EndDate.Value
                    cov.ModifiedById = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    updateEndorseCov(cov)
                End If
            End If
        Next

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.CoverageTypeCode = Codes.COVERAGE_TYPE__EXTENDED Then
                intDuration = CType(DateDiff(MONTH, cov.BeginDate.Value, DateAdd("d", 1, cov.EndDate.Value)), Integer)
                If dOrgCoverageEndate = DateAdd("d", -1, cov.BeginDate.Value) Then
                    If Not LocateActiveClaimsByCoverageId(cov.Id, DateAdd("D", 1, dNewCoverageEndate), DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)), False) Then
                        cov.BeginDate = New DateType(DateAdd("D", 1, dNewCoverageEndate))
                        If isECSDurationFix Then
                            cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)))
                            cov.ModifiedById = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                        End If
                    End If
                Else
                    If Not LocateActiveClaimsByCoverageId(cov.Id, DateAdd("D", 1, dNewCoverageEndate), DateAdd(MONTH, CInt(intDuration), dNewCoverageEndate), False) Then
                        cov.BeginDate = New DateType(dNewCoverageEndate)
                        If isECSDurationFix Then
                            cov.EndDate = New DateType(DateAdd(MONTH, CInt(intDuration), dNewCoverageEndate))
                            cov.ModifiedById = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                        End If
                    End If
                End If
                dNewExtendedEndate = cov.EndDate.Value
            End If
            updateEndorseCov(cov)
        Next

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.CoverageTypeCode <> Codes.COVERAGE_TYPE__MANUFACTURER AndAlso cov.CoverageTypeCode <> Codes.COVERAGE_TYPE__EXTENDED Then
                If isECSDurationFix Then
                    cov.EndDate = New DateType(dNewExtendedEndate)
                    cov.ModifiedById = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    updateEndorseCov(cov)
                End If
            End If
        Next

    End Sub

    Private Sub LoadNewProductSalesDate()

        Dim dNewCoverageEndate As Date
        Dim dOrgCoverageEndate As Date
        Dim intDuration As Integer

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.CoverageTypeCode = Codes.COVERAGE_TYPE__MANUFACTURER Then
                dOrgCoverageEndate = cov.EndDate.Value
                intDuration = CType(DateDiff(MONTH, cov.BeginDate.Value, DateAdd("d", 1, cov.EndDate.Value)), Integer)
                If Not LocateActiveClaimsByCoverageId(cov.Id, Me.ProductSalesDatePost.Value, DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)), True) Then
                    cov.BeginDate = New DateType(Me.ProductSalesDatePost.Value)
                    Cert.ProductSalesDate = cov.BeginDate
                    cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)))
                    dNewCoverageEndate = cov.EndDate.Value
                    Cert.ProductSalesDate = cov.BeginDate
                End If
                updateEndorseCov(cov)
            End If
        Next

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.CoverageTypeCode <> Codes.COVERAGE_TYPE__MANUFACTURER Then
                intDuration = CType(DateDiff(MONTH, Cov.BeginDate.Value, DateAdd("d", 1, Cov.EndDate.Value)), Integer)
                If dOrgCoverageEndate = DateAdd("d", -1, cov.BeginDate.Value) Then
                    If Not LocateActiveClaimsByCoverageId(cov.Id, DateAdd("D", 1, dNewCoverageEndate), DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)), False) Then
                        cov.BeginDate = New DateType(DateAdd("D", 1, dNewCoverageEndate))
                        cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)))
                    End If
                Else
                    If Not LocateActiveClaimsByCoverageId(cov.Id, Me.ProductSalesDatePost.Value, DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)), False) Then
                        cov.BeginDate = New DateType(Me.ProductSalesDatePost.Value)
                        cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)))
                    End If
                End If
                updateEndorseCov(cov)
            End If
        Next

    End Sub

    Private Sub LoadNewWarrantySalesDate()

        Dim intDuration As Integer

        Cert.WarrantySalesDate = New DateType(Me.WarrantySalesDatePost.Value)

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.CoverageTypeCode <> Codes.COVERAGE_TYPE__MANUFACTURER Then
                intDuration = CType(DateDiff(MONTH, cov.BeginDate.Value, DateAdd("d", 1, cov.EndDate.Value)), Integer)
                If Not Me.LocateActiveClaimsByCoverageId(cov.Id, Me.WarrantySalesDatePost.Value, DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)), True) Then
                    cov.BeginDate = New DateType(Me.WarrantySalesDatePost.Value)
                    cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)))
                End If
            End If
            Me.Cert.WarrantySalesDate = Me.WarrantySalesDatePost
            updateEndorseCov(cov)
        Next

    End Sub

    Private Sub LoadNewProductSalesDateIfEqualsLowestCovStrtDt()

        Dim intDuration As Integer

        Cert.ProductSalesDate = New DateType(Me.ProductSalesDatePost.Value)

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.CoverageTypeCode <> Codes.COVERAGE_TYPE__MANUFACTURER Then
                intDuration = CType(DateDiff(MONTH, cov.BeginDate.Value, DateAdd("d", 1, cov.EndDate.Value)), Integer)
                If Not Me.LocateActiveClaimsByCoverageId(cov.Id, Me.ProductSalesDatePost.Value, DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)), True) Then
                    cov.BeginDate = New DateType(Me.ProductSalesDatePost.Value)
                    cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, CInt(intDuration), cov.BeginDate.Value)))
                End If
            End If
            Me.Cert.ProductSalesDate = Me.ProductSalesDatePost
            updateEndorseCov(cov)
        Next

    End Sub
    Private Sub UpdateLiabilityLimit()
        Dim oContractId As Guid
        Dim oContract As Contract
        Dim a1 As ArrayList
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")

        oContractId = Contract.GetContractID(Me.CertId)
        oContract = New Contract(oContractId)
        If oContract.AutoSetLiabilityId.Equals(yesId) Then
            For Each certCov As CertItemCoverage In Me.AssociatedItemCoverages
                If certCov.LiabilityLimits.Value > 0 Then
                    a1 = CalculateLiabilityLimitUsingCovTemplate(Me.CertId, certCov.CoverageTypeId, Me.SalesPricePost)
                    If a1(1) = 0 Then
                        certCov.LiabilityLimits = New DecimalType(CType(a1(0), Decimal))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub SetEndorseCovProsInf(ByVal cov As CertItemCoverage, ByVal newendorscov As CertEndorseCov)

        newendorscov.BeginDatePre = cov.BeginDate
        newendorscov.EndDatePre = cov.EndDate

    End Sub

#End Region

#Region "Address"

    Private _addressPre As Address = Nothing
    Public ReadOnly Property AddressPre() As Address
        Get
            If Me._addressPre Is Nothing Then
                If Me.AddressIdPre.Equals(Guid.Empty) Then
                    Me._addressPre = New Address(Me.Dataset, Nothing)
                    Me.AddressIdPre = Me._addressPre.Id
                Else
                    Me._addressPre = New Address(Me.AddressIdPre, Me.Dataset, Nothing)
                End If
            End If
            Return Me._addressPre
        End Get
    End Property

    Private _address As Address = Nothing
    Public ReadOnly Property AddressPost() As Address
        Get
            If Me._address Is Nothing Then
                If Me.AddressIdPost.Equals(Guid.Empty) Then
                    Me._address = New Address(Me.Dataset, Nothing)
                    Me.AddressIdPost = Me._address.Id
                Else
                    Me._address = New Address(Me.AddressIdPost, Me.Dataset, Nothing)
                End If
            End If
            Return Me._address
        End Get
    End Property
#End Region
#Region "Properties"

    'Key Property
    Public ReadOnly Property CertEndorseId() As Guid
        Get
            If Row(CertEndorseDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_CERT_ENDORSE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CertItemId() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_CERT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_CERT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_CERT_ITEM_ID, Value)
        End Set
    End Property
      

    <ValueMandatory("")> _
    Public Property EndorsementNumber() As LongType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_ENDORSEMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertEndorseDAL.COL_NAME_ENDORSEMENT_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_ENDORSEMENT_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property CustNamePre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_CUST_NAME_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_CUST_NAME_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_CUST_NAME_PRE, Value)
        End Set
    End Property


    <ValidStringLength("", Min:=1, Max:=50)> _
    Public Property CustNamePost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_CUST_NAME_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_CUST_NAME_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_CUST_NAME_POST, Value)
        End Set
    End Property
    <ValidStringLength("", Min:=1, Max:=50)> _
    Public Property EmailPre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_EMAIL_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_EMAIL_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_EMAIL_PRE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50), EmailAddress("")> _
   Public Property EmailPost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_EMAIL_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_EMAIL_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_EMAIL_POST, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
   Public Property HomePhonePre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_HOME_PHONE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_HOME_PHONE_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_HOME_PHONE_PRE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
   Public Property HomePhonePost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_HOME_PHONE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_HOME_PHONE_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_HOME_PHONE_POST, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property WorkPhonePre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_WORK_PHONE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_WORK_PHONE_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_WORK_PHONE_PRE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property WorkPhonePost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_WORK_PHONE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_WORK_PHONE_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_WORK_PHONE_POST, Value)
        End Set
    End Property

    Public Property AddressIdPre() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_ADDRESS_ID_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_ADDRESS_ID_PRE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_ADDRESS_ID_PRE, Value)
        End Set
    End Property

    Public Property AddressIdPost() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_ADDRESS_ID_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_ADDRESS_ID_POST), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_ADDRESS_ID_POST, Value)
        End Set
    End Property

    Public Property LangaugeIdPre() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_LANGUAGE_ID_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_LANGUAGE_ID_PRE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_LANGUAGE_ID_PRE, Value)
        End Set
    End Property

    Public Property LangaugeIdPost() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_LANGUAGE_ID_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_LANGUAGE_ID_POST), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_LANGUAGE_ID_POST, Value)
        End Set
    End Property

    Public ReadOnly Property getLanguagePrefPreDesc() As String

        Get
            Dim dv As DataView = LookupListNew.GetLanguageLookupList()
            langPrefDesc = LookupListNew.GetDescriptionFromId(dv, Me.LangaugeIdPre)
            Return langPrefDesc
        End Get

    End Property

    Public ReadOnly Property getLanguagePrefPostDesc() As String

        Get
            Dim dv As DataView = LookupListNew.GetLanguageLookupList()
            langPrefDesc = LookupListNew.GetDescriptionFromId(dv, Me.LangaugeIdPost)
            Return langPrefDesc
        End Get

    End Property

    Dim _EditedCovItemId As Guid = Guid.Empty
    Public Property getEditedCertItemCovId() As Guid
        Get
            Return _EditedCovItemId
        End Get
        Set(ByVal Value As Guid)
            _EditedCovItemId = Value
        End Set
    End Property

    Dim _NewBeginDate As DateType
    Public Property getNewBeginDateEditedCertItemCov() As DateType
        Get
            Return _NewBeginDate
        End Get
        Set(ByVal Value As DateType)
            _NewBeginDate = Value
        End Set
    End Property

    Public Property ProductSalesDatePre() As DateType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_PRODUCT_SALES_DATE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertEndorseDAL.COL_NAME_PRODUCT_SALES_DATE_PRE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_PRODUCT_SALES_DATE_PRE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidProductSalesDate(""), LowDateEnterForProductSalesDate(""), HigherDateEnterForProductSalesDate("")> _
    Public Property ProductSalesDatePost() As DateType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_PRODUCT_SALES_DATE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertEndorseDAL.COL_NAME_PRODUCT_SALES_DATE_POST).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_PRODUCT_SALES_DATE_POST, Value)
        End Set
    End Property

    Public Property WarrantySalesDatePre() As DateType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_WARRANTY_SALES_DATE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertEndorseDAL.COL_NAME_WARRANTY_SALES_DATE_PRE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_WARRANTY_SALES_DATE_PRE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidWarrantySalesDate(""), LowDateEnterForWarrantySalesDate(""), HigherDateEnterForWarrantySalesDate("")> _
        Public Property WarrantySalesDatePost() As DateType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_WARRANTY_SALES_DATE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertEndorseDAL.COL_NAME_WARRANTY_SALES_DATE_POST).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_WARRANTY_SALES_DATE_POST, Value)
        End Set

    End Property

    Public Property SalesPricePre() As DecimalType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_SALES_PRICE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertEndorseDAL.COL_NAME_SALES_PRICE_PRE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_SALES_PRICE_PRE, Value)
        End Set
    End Property

    <ValidSalesPrice("")> _
    Public Property SalesPricePost() As DecimalType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_SALES_PRICE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertEndorseDAL.COL_NAME_SALES_PRICE_POST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_SALES_PRICE_POST, Value)
        End Set
    End Property

    Public Property DocumentTypeIDPre() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_DOCUMENT_TYPE_ID_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_DOCUMENT_TYPE_ID_PRE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_DOCUMENT_TYPE_ID_PRE, Value)
        End Set
    End Property
    <ValueMandatoryDocumentType("")> _
    Public Property DocumentTypeIDPost() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_DOCUMENT_TYPE_ID_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_DOCUMENT_TYPE_ID_POST), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_DOCUMENT_TYPE_ID_POST, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
        Public Property IdTypePre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_ID_TYPE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_ID_TYPE_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_ID_TYPE_PRE, Value)
        End Set
    End Property

    <NewValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property IdTypePost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_ID_TYPE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_ID_TYPE_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_ID_TYPE_POST, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property DocumentAgencyPre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_DOCUMENT_AGENCY_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_DOCUMENT_AGENCY_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_DOCUMENT_AGENCY_PRE, Value)
        End Set
    End Property

    <NewValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property DocumentAgencyPost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_DOCUMENT_AGENCY_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_DOCUMENT_AGENCY_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_DOCUMENT_AGENCY_POST, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property TaxIDNumbPre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_IDENTIFICATION_NUMBER_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_IDENTIFICATION_NUMBER_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_IDENTIFICATION_NUMBER_PRE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20), ValueMustBeBlankForDocumentNumber(""), SPValidationDocumentNumber(""), ValueTaxIdLenht("")> _
    Public Property TaxIDNumbPost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_IDENTIFICATION_NUMBER_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_IDENTIFICATION_NUMBER_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_IDENTIFICATION_NUMBER_POST, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property RgNumberPre() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_RG_NUMBER_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_RG_NUMBER_PRE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_RG_NUMBER_PRE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20), NewValueMandatory("")> _
    Public Property RgNumberPost() As String
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_RG_NUMBER_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertEndorseDAL.COL_NAME_RG_NUMBER_POST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_RG_NUMBER_POST, Value)
        End Set
    End Property

    Public Property DocumentIssueDatePre() As DateType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_DOCUMENT_ISSUE_DATE_PRE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertEndorseDAL.COL_NAME_DOCUMENT_ISSUE_DATE_PRE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_DOCUMENT_ISSUE_DATE_PRE, Value)
        End Set
    End Property

    <NewValueMandatory(""), NonFutureDocumentIssueDate("")> _
    Public Property DocumentIssueDatePost() As DateType
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_DOCUMENT_ISSUE_DATE_POST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(CertEndorseDAL.COL_NAME_DOCUMENT_ISSUE_DATE_POST).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_DOCUMENT_ISSUE_DATE_POST, Value)
        End Set
    End Property

    ' <ValidStringLength("", Min:=1)> 
    Private _TermPre As Integer
    '[<ValidTerm("")> _
    Public Property TermPre() As Integer
        Get
            Return _TermPre
        End Get
        Set(ByVal Value As Integer)
            _TermPre = Value
        End Set
    End Property

    Private _TermPos As Integer
    <ValueMandatory(""), ValidNumericRange("", Min:=1, Max:=99)> _
    Public Property TermPos() As Integer
        Get
            Return _TermPos
        End Get
        Set(ByVal Value As Integer)
            _TermPos = Value
        End Set
    End Property

    Public Property ManufaturerWarranty() As Boolean
        Get
            Return _ManufaturerWarranty
        End Get
        Set(ByVal Value As Boolean)
            _ManufaturerWarranty = Value
        End Set

    End Property

    Dim _DateAdded As Date
    Public Property DateAdded() As Date
        Get
            Return _DateAdded
        End Get
        Set(ByVal Value As Date)
            _DateAdded = Value
        End Set

    End Property

    'Public Shared ReadOnly Property GetCurrentContract(ByVal dealerID As Guid) As Contract
    '    Get
    '        Dim contractID As Guid
    '        Dim dv As DataView = Contract.getList(dealerID)
    '        Dim oContract As Contract
    '        dv.Sort = Contract.ContractSearchDV.COL_EFFECTIVE & " DESC," & Contract.ContractSearchDV.COL_EXPIRATION & " DESC"
    '        Dim dt As DataTable = dv.Table

    '        For Each row As DataRow In dt.Rows
    '            Dim MinEffective As Date = CType(row(Contract.ContractSearchDV.COL_EFFECTIVE), Date)
    '            Dim MaxExpiration As Date = CType(row(Contract.ContractSearchDV.COL_EXPIRATION), Date)
    '            If (System.DateTime.Now >= MinEffective) And (System.DateTime.Now < MaxExpiration) Then
    '                contractID = CType(row(Contract.ContractSearchDV.COL_CONTRACT_ID), Guid)
    '                oContract = New Contract(contractID)
    '                Return oContract
    '            End If
    '        Next
    '    End Get
    'End Property

    Public ReadOnly Property AssociatedItemCoverages(Optional blnParentOnly As Boolean = False) As BusinessObjectListBase
        Get

            Dim oDealer As New Dealer(Me.Cert.DealerId)
            Dim attValueEnableChangingMFG As AttributeValue = oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_ENABLE_CHANGING_MFG_TERM_If_NO_CLAIMS_EXIST_In_PARENT_CHILD).FirstOrDefault
            If Not attValueEnableChangingMFG Is Nothing AndAlso attValueEnableChangingMFG.Value = Codes.YESNO_Y Then
                Return CertItemCoverage.GetItemCovListWithChildOrParentForCertificate(Me.CertId, Me.Cert)
            Else
                Return CertItemCoverage.GetItemCovListForCertificate(Me.CertId, Me.Cert)
            End If

        End Get
    End Property

    Public ReadOnly Property AssociatedItemCoveragesWithChildOrParent() As BusinessObjectListBase
        Get

        End Get
    End Property

    Public ReadOnly Property AssociatedCertItems() As BusinessObjectListBase
        Get
            Return CertItem.GetItemListForCertificate(Me.CertId, Me.Cert)
        End Get
    End Property

    'REQ-1162
    Public ReadOnly Property DealerTypeCode() As String
        Get
            If _dealerTypeCode Is Nothing Then
                Dim oDealer As New Dealer(Me.Cert.DealerId)
                _dealerTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, oDealer.DealerTypeId)
            End If
            Return _dealerTypeCode
        End Get
    End Property

    Public ReadOnly Property AssociatedEndorseCoverages() As BusinessObjectListBase
        Get
            Return CertEndorseCov.GetEndorsementCovListForEndorsement(Me.CertEndorseId, Me)
        End Get
    End Property

    Private _certItem As CertItem
    Public ReadOnly Property CertItem() As CertItem
        Get
            If _certItem Is Nothing Then
                Me._certItem = New CertItem(Me.CertItemId)
            End If
            Return Me._certItem
        End Get
    End Property

    Private _cert As Certificate
    Public ReadOnly Property Cert() As Certificate
        Get
            If _cert Is Nothing Then
                Me._cert = New Certificate(Me.CertId, Me.Dataset)
            End If
            _DateAdded = Me._cert.DateAdded.Value
            Return Me._cert
        End Get
    End Property

    Private _addr As Address
    Public ReadOnly Property Addr() As Address
        Get
            If _addr Is Nothing Then
                Me._addr = New Address(Me.Dataset, Me)
            End If
            Return Me._addr
        End Get
    End Property
    Private _certId As Guid
   <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CertEndorseDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertEndorseDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertEndorseDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

    Private _CovisDirty As Boolean = False
    Public Property CovisDirty() As Boolean
        Get
            Return _CovisDirty
        End Get
        Set(ByVal Value As Boolean)
            _CovisDirty = Value
        End Set
    End Property

    Private _TermisDirty As Boolean = False
    Public Property TermisDirty() As Boolean
        Get
            Return _TermisDirty
        End Get
        Set(ByVal Value As Boolean)
            _TermisDirty = Value
        End Set
    End Property

    Private _NameisDirty As Boolean = False
    Public Property NameisDirty() As Boolean
        Get
            Return _NameisDirty
        End Get
        Set(ByVal Value As Boolean)
            _NameisDirty = Value
        End Set
    End Property

    Private _HomePhoneisDirty As Boolean = False
    Public Property HomePhoneisDirty() As Boolean
        Get
            Return _HomePhoneisDirty
        End Get
        Set(ByVal Value As Boolean)
            _HomePhoneisDirty = Value
        End Set
    End Property

    Private _EmailisDirty As Boolean = False
    Public Property EmailisDirty() As Boolean
        Get
            Return _EmailisDirty
        End Get
        Set(ByVal Value As Boolean)
            _EmailisDirty = Value
        End Set
    End Property

    Private _WorkPhoneisDirty As Boolean = False
    Public Property WorkPhoneisDirty() As Boolean
        Get
            Return _WorkPhoneisDirty
        End Get
        Set(ByVal Value As Boolean)
            _WorkPhoneisDirty = Value
        End Set
    End Property

    Private _SalesPriceisDirty As Boolean = False
    Public Property SalesPriceisDirty() As Boolean
        Get
            Return _SalesPriceisDirty
        End Get
        Set(ByVal Value As Boolean)
            _SalesPriceisDirty = Value
        End Set
    End Property

    Private _LanguageisDirty As Boolean = False
    Public Property LanguageisDirty() As Boolean
        Get
            Return _LanguageisDirty
        End Get
        Set(ByVal Value As Boolean)
            _LanguageisDirty = Value
        End Set
    End Property

    Private _AddressisDirty As Boolean = False
    Public Property AddressisDirty() As Boolean
        Get
            Return _AddressisDirty
        End Get
        Set(ByVal Value As Boolean)
            _AddressisDirty = Value
        End Set
    End Property

    Private _ProductSalesDateisDirty As Boolean = False
    Public Property ProductSalesDatesisDirty() As Boolean
        Get
            Return _ProductSalesDateisDirty
        End Get
        Set(ByVal Value As Boolean)
            _ProductSalesDateisDirty = Value
        End Set
    End Property

    Private _WarrantySalesDateisDirty As Boolean = False
    Public Property WarrantySalesDatesisDirty() As Boolean
        Get
            Return _WarrantySalesDateisDirty
        End Get
        Set(ByVal Value As Boolean)
            _WarrantySalesDateisDirty = Value
        End Set
    End Property

    Private _DocTypeisDirty As Boolean = False
    Public Property DocTypeisDirty() As Boolean
        Get
            Return _DocTypeisDirty
        End Get
        Set(ByVal Value As Boolean)
            _DocTypeisDirty = Value
        End Set
    End Property

    Private _IDTypeisDirty As Boolean = False
    Public Property IDTypeisDirty() As Boolean
        Get
            Return _IDTypeisDirty
        End Get
        Set(ByVal Value As Boolean)
            _IDTypeisDirty = Value
        End Set
    End Property

    Private _DocAgencyisDirty As Boolean = False
    Public Property DocAgencyisDirty() As Boolean
        Get
            Return _DocAgencyisDirty
        End Get
        Set(ByVal Value As Boolean)
            _DocAgencyisDirty = Value
        End Set
    End Property

    Private _DocNumberisDirty As Boolean = False
    Public Property DocNumberisDirty() As Boolean
        Get
            Return _DocNumberisDirty
        End Get
        Set(ByVal Value As Boolean)
            _DocNumberisDirty = Value
        End Set
    End Property

    Private _RGNumberisDirty As Boolean = False
    Public Property RGNumberisDirty() As Boolean
        Get
            Return _RGNumberisDirty
        End Get
        Set(ByVal Value As Boolean)
            _RGNumberisDirty = Value
        End Set
    End Property

    Private _DocIssueDateisDirty As Boolean = False
    Public Property DocIssueDateisDirty() As Boolean
        Get
            Return _DocIssueDateisDirty
        End Get
        Set(ByVal Value As Boolean)
            _DocIssueDateisDirty = Value
        End Set
    End Property

    Private _isECSDurationFix As Boolean
    Public Property isECSDurationFix() As Boolean
        Get
            Return _isECSDurationFix
        End Get
        Set(ByVal Value As Boolean)
            _isECSDurationFix = Value
        End Set
    End Property

    Public ReadOnly Property getDocTypeCode() As String
        Get
            Dim dv As DataView = LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            documentTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, Me.DocumentTypeIDPost)
            Return documentTypeCode
        End Get

    End Property

    Public ReadOnly Property getDocTypePreCode() As String
        Get
            Dim dv As DataView = LookupListNew.GetDocumentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            documentTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, Me.DocumentTypeIDPre)
            Return documentTypeCode
        End Get

    End Property

    Public Property DealerEndorsementFlagValue() As String

        Get
            Return _dealerEndorsementFlag
        End Get
        Set(ByVal Value As String)
            _dealerEndorsementFlag = Value
        End Set

    End Property
#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Try

            _dealerEndorsementFlag = IsDealerEndorsementAttributeFlagOn()

            'If (Not _cert Is Nothing) Then Me.LoadCoveragesProperties(Me.ManufaturerWarranty)
            If getEditedCertItemCovId.Equals(Guid.Empty) Then
                Me.LoadCoveragesProperties(Me.ManufaturerWarranty)
                MyBase.Save()
            Else
                Me.LoadCertItemCovForSplitWarrProduct()
            End If

            'Removed the validation to check if open claim fall out of the new coverage range for ticket 1492531
            'validate new coverage terms
            'Dim dr As DataRow, objCECov As CertEndorseCov, objCE As CertEndorse, ds As DataSet
            'For Each dr In Me.Dataset.Tables(CertEndorseCovDAL.TABLE_NAME).Rows
            'objCECov = New CertEndorseCov(dr)

            'Validate if any open claim fall out of the new coverage range
            'ds = New DataSet()
            'objCECov.IsCertEndorsable(ds, Me.CertId, objCECov.BeginDatePost, objCECov.EndDatePost)
            'If Not ds.Tables Is Nothing AndAlso ds.Tables.Count > 0 AndAlso CType(ds.Tables(0).Rows(0)(0), Integer) > 0 Then
            '    Throw New StoredProcedureGeneratedException("CertEndorse Error: ", Common.ErrorCodes.EXISTING_CLAIMS_WILL_FALL_OUTSIDE_THE_NEW_DATE_RANGE)
            'End If

            'objCECov.Validate()
            'Next

            'ValidateSalesPrice()
            If (Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached) Or (Me.CovisDirty) Then
                'Me.LoadCoveragesProperties(Me.ManufaturerWarranty)
                Dim dal As New CertEndorseDAL
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached And _dealerEndorsementFlag <> "Y" Then
                    Dim objId As Guid = Me.CertEndorseId
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Sub LoadCertItemCovForSplitWarrProduct()
        Dim covDur As Integer
        If Me.Dataset.Tables.IndexOf(CertEndorseCovDAL.TABLE_NAME) < 0 Then
            For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
                AddNewEndorseCV(cov)
            Next
        End If

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            If cov.Id.Equals(getEditedCertItemCovId) Then
                covDur = DateDiff(MONTH, cov.BeginDate.Value, cov.EndDate.Value)
                cov.BeginDate = getNewBeginDateEditedCertItemCov
                cov.EndDate = New DateType(DateAdd("d", -1, DateAdd(MONTH, covDur, cov.BeginDate.Value)))
                updateEndorseCov(cov)
            End If
        Next
    End Sub

    Public Sub LoadCoveragesProperties(ByVal manufaturerWarranty As Boolean)

        Dim dNewCoverageBeginDate As Date
        Dim dNewCoverageEndate As Date
        Dim dOrgCoverageBeginDate As Date
        Dim dOrgCoverageEndate As Date
        Dim intDuration As Integer

        If Me.Dataset.Tables.IndexOf(CertEndorseCovDAL.TABLE_NAME) < 0 Then
            For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
                AddNewEndorseCV(cov)
            Next
        End If

        If manufaturerWarranty Then
            If Me.TermisDirty Then
                LoadNewTerm()
            End If
            If Me.ProductSalesDatesisDirty Then
                LoadNewProductSalesDate()
            End If
        Else
            'If Me.WarrantySalesDatesisDirty Then
            ' LoadNewWarrantySalesDate()
            'End If
            'Commented the above code to implement the same logic in LoadNewProductSalesDateIfEqualsLowestCovStrtDt()
            If Me.ProductSalesDatesisDirty Then
                LoadNewProductSalesDateIfEqualsLowestCovStrtDt()
            End If
        End If

        Me.Cert.CustomerName = Me.CustNamePost

        If Me.SalesPriceisDirty Then
            Me.Cert.SalesPrice = Me.SalesPricePost

            If Me.AssociatedCertItems.Count = 1 Then
                For Each CrtItm As CertItem In Me.AssociatedCertItems
                    CrtItm.ItemRetailPrice = Me.SalesPricePost
                Next
            End If
            UpdateLiabilityLimit()
        End If
        If Me.EmailisDirty Then
            Me.Cert.Email = Me.EmailPost
        End If
        If Me.WorkPhoneisDirty Then
            Me.Cert.WorkPhone = Me.WorkPhonePost
        End If
        If Me.HomePhoneisDirty Then
            Me.Cert.HomePhone = Me.HomePhonePost
        End If
        If Me.LanguageisDirty Then
            Me.Cert.LanguageId = Me.LangaugeIdPost
        End If
        If Me.AddressisDirty Then
            Me.Cert.AddressId = Me.AddressPost.Id
        End If

        If Me.DocTypeisDirty Then
            Me.Cert.DocumentTypeID = Me.DocumentTypeIDPost
        End If

        If Me.IDTypeisDirty Then
            Me.Cert.IdType = Me.IdTypePost
        End If

        If Me.DocNumberisDirty Then
            Me.Cert.IdentificationNumber = Me.TaxIDNumbPost
        End If

        If Me.DocIssueDateisDirty Then
            Me.Cert.DocumentIssueDate = Me.DocumentIssueDatePost
        End If

        If Me.DocAgencyisDirty Then
            Me.Cert.DocumentAgency = Me.DocumentAgencyPost
        End If

        If Me.RGNumberisDirty Then
            Me.Cert.RgNumber = Me.RgNumberPost
        End If
    End Sub

    Public Sub SaveCustomerData(ByVal custid As Guid, ByVal custfirstname As String, ByVal custmidname As String, ByVal custlastname As String)
        Dim po_code As String
        Dim po_reason As String
        Dim endorseDAL As New CertEndorseDAL
        endorseDAL.SaveCustomerData(custid, custfirstname, custmidname, custlastname, po_code, po_reason)
        _cert = Nothing
    End Sub

    Public Sub PopulateWithDefaultValues(ByVal Id As Guid, ByVal ManuWarranty As Boolean)

        Me.CertId = Cert.Id
        Me.CustNamePre = Cert.CustomerName
        Me.CustNamePost = Cert.CustomerName
        Me.HomePhonePre = Cert.HomePhone
        Me.HomePhonePost = Cert.HomePhone
        Me.WorkPhonePre = Cert.WorkPhone
        Me.WorkPhonePost = Cert.WorkPhone
        Me.EmailPre = Cert.Email
        Me.EmailPost = Cert.Email
        Me.CompanyId = Cert.CompanyId
        Me.WarrantySalesDatePre = Cert.WarrantySalesDate
        Me.WarrantySalesDatePost = Cert.WarrantySalesDate
        Me.ProductSalesDatePre = Cert.ProductSalesDate
        Me.ProductSalesDatePost = Cert.ProductSalesDate
        Me.SalesPricePre = Cert.SalesPrice
        Me.SalesPricePost = Cert.SalesPrice
        Me.AddressIdPre = Cert.AddressId
        Me.LangaugeIdPre = Cert.LanguageId
        Me.LangaugeIdPost = Cert.LanguageId
        Me.DocumentTypeIDPre = Cert.DocumentTypeID
        Me.DocumentTypeIDPost = Cert.DocumentTypeID
        Me.IdTypePre = Cert.IdType
        Me.IdTypePost = Cert.IdType
        Me.DocumentAgencyPre = Cert.DocumentAgency
        Me.DocumentAgencyPost = Cert.DocumentAgency
        Me.TaxIDNumbPre = Cert.IdentificationNumber
        Me.TaxIDNumbPost = Cert.IdentificationNumber
        Me.RgNumberPre = Cert.RgNumber
        Me.RgNumberPost = Cert.RgNumber
        Me.DocumentIssueDatePre = Cert.DocumentIssueDate
        Me.DocumentIssueDatePost = Cert.DocumentIssueDate            

        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
            Me.CertItemId = cov.CertItemId
        Next

        Dim EndorseDV As CertEndorse.EndorseSearchDV = CertEndorse.getList(Me.CertId)
        Dim oCount As New LongType(EndorseDV.Count + 1)
        Me.EndorsementNumber = oCount
        Me.ManufaturerWarranty = ManuWarranty
        Me.SetValue(DALBase.COL_NAME_CREATED_BY, ElitaPlusIdentity.Current.ActiveUser.NetworkId)

    End Sub

    Public Sub PopulateWithDefaultValues(ByVal oCertId As Guid, ByVal oCertItemId As Guid)
        Me.CertId = oCertId
        Me.CustNamePre = Cert.CustomerName
        Me.CustNamePost = Cert.CustomerName
        Me.HomePhonePre = Cert.HomePhone
        Me.HomePhonePost = Cert.HomePhone
        Me.WorkPhonePre = Cert.WorkPhone
        Me.WorkPhonePost = Cert.WorkPhone
        Me.EmailPre = Cert.Email
        Me.EmailPost = Cert.Email
        Me.CompanyId = Cert.CompanyId
        Me.WarrantySalesDatePre = Cert.WarrantySalesDate
        Me.WarrantySalesDatePost = Cert.WarrantySalesDate
        Me.ProductSalesDatePre = Cert.ProductSalesDate
        Me.ProductSalesDatePost = Cert.ProductSalesDate
        Me.SalesPricePre = Cert.SalesPrice
        Me.SalesPricePost = Cert.SalesPrice
        Me.AddressIdPre = Cert.AddressId
        Me.LangaugeIdPre = Cert.LanguageId
        Me.LangaugeIdPost = Cert.LanguageId
        Me.DocumentTypeIDPre = Cert.DocumentTypeID
        Me.DocumentTypeIDPost = Cert.DocumentTypeID
        Me.IdTypePre = Cert.IdType
        Me.IdTypePost = Cert.IdType
        Me.DocumentAgencyPre = Cert.DocumentAgency
        Me.DocumentAgencyPost = Cert.DocumentAgency
        Me.TaxIDNumbPre = Cert.IdentificationNumber
        Me.TaxIDNumbPost = Cert.IdentificationNumber
        Me.RgNumberPre = Cert.RgNumber
        Me.RgNumberPost = Cert.RgNumber
        Me.DocumentIssueDatePre = Cert.DocumentIssueDate
        Me.DocumentIssueDatePost = Cert.DocumentIssueDate

        Me.CertItemId = oCertItemId       

        Dim EndorseDV As CertEndorse.EndorseSearchDV = CertEndorse.getList(Me.CertItemId)
        Dim oCount As New LongType(EndorseDV.Count + 1)
        Me.EndorsementNumber = oCount

        Me.SetValue(DALBase.COL_NAME_CREATED_BY, ElitaPlusIdentity.Current.ActiveUser.NetworkId)

        Me.TermPos = 99  ' Default => No Term

    End Sub

    Public Function LocateActiveClaims(ByVal CertItemId, ByVal BeginDate, ByVal EndDate, ByVal ManufaturerWarranty) As Boolean

        Dim DV As Claim.ClaimSearchDV = Claim.GetClaimslist(CertItemId, BeginDate, EndDate, ManufaturerWarranty)

        If DV.Count > 0 Then
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.EXISTING_CLAIMS_WILL_FALL_OUTSIDE_THE_NEW_DATE_RANGE, GetType(CertEndorse), Nothing, "LabelProductSaleDate", Nothing)}
            Throw New BOValidationException(errors, GetType(CertEndorse).FullName, Me.UniqueId)
        End If

        Return False

    End Function

    Public Function LocateActiveClaimsByCoverageId(ByVal CertItemCoverageId, ByVal BeginDate, ByVal EndDate, ByVal ManufaturerWarranty) As Boolean

        Dim DV As Claim.ClaimSearchDV = Claim.GetClaimslistByCoverageId(CertItemCoverageId, BeginDate, EndDate, ManufaturerWarranty)

        If DV.Count > 0 Then
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.EXISTING_CLAIMS_WILL_FALL_OUTSIDE_THE_NEW_DATE_RANGE, GetType(CertEndorse), Nothing, "LabelProductSaleDate", Nothing)}
            Throw New BOValidationException(errors, GetType(CertEndorse).FullName, Me.UniqueId)
        End If

        Return False

    End Function

    Public Function LocateActiveClaimsByCovIdClaimLossDate(ByVal CertItemCoverageId, ByVal BeginDate, ByVal EndDate) As Boolean
        Dim dal As New CertEndorseDAL
        Dim ds As DataSet = dal.LoadListByCovIdClaimLossDate(CertItemCoverageId, BeginDate, EndDate)
        Dim dv As DataView = New DataView(ds.Tables(0))
        If dv.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsDealerEndorsementAttributeFlagOn() As String

        Dim dal As New CertEndorseDAL
        Dim oCert As New Certificate(Me.CertId)
        _dealerEndorsementFlag = dal.IsDealerEndorsementAttributeFlagOn(oCert.DealerId)

        Return _dealerEndorsementFlag

    End Function

    Public Shared Function GetNewCertEndorse(ByVal original As CertEndorse) As CertEndorse
        Dim c As New CertEndorse
        c.CopyFrom(original)
        c.SetValue(DALBase.COL_NAME_CREATED_BY, original.CreatedById)
        c.SetValue(DALBase.COL_NAME_CREATED_DATE, original.CreatedDate)
        Return c
    End Function

    Public Shared Function GetClaimCountForParentAndChildCert(ByVal cert_Id As Guid) As Integer
        Try
            Dim dal As New CertEndorseDAL
            Dim dv As New DataView

            Return dal.ClaimCountForParentAndChildCert(cert_Id)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"
    'Manually added method

    Public Shared Function getList(ByVal CertID As Guid) As EndorseSearchDV

        Try
            Dim dal As New CertEndorseDAL

            Return New EndorseSearchDV(dal.LoadList(CertID, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function ValidateCertSalesPrice(ByVal CertId As Guid, ByVal NewSalesPrice As Decimal, ByVal covTypeId As Guid, ByVal certDur As Integer, ByVal covDur As Integer) As Integer
        Dim dal As New CertEndorseDAL
        Return dal.ValidateCertSalesPrice(CertId, NewSalesPrice, covTypeId, certDur, covDur)
    End Function

    Public Shared Function CalculateLiabilityLimitUsingCovTemplate(ByVal CertId As Guid, ByVal CoverageTypeId As Guid, ByVal NewSalesPrice As Decimal) As ArrayList
        Dim dal As New CertEndorseDAL
        Return dal.CalculateLiabilityLimitUsingCovTemplate(CertId, CoverageTypeId, NewSalesPrice)
    End Function
    Public Shared Function IsLowestCovStrtDtEqual2PrdSalesDt(ByVal certId As Guid) As Boolean
        Dim ds As DataSet, blnFlag As Boolean = False
        Dim dal As New CertEndorseDAL
        ds = dal.IsLowestCovStrtDtEqual2PrdSalesDt(certId)
        If (ds.Tables(0).Rows.Count > 0) Then
            blnFlag = True
        Else
            blnFlag = False
        End If

        Return blnFlag
    End Function

    Public Class EndorseSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_ENDORSEMENT_ID As String = CertEndorseDAL.COL_NAME_CERT_ENDORSE_ID
        Public Const COL_ADDED_BY As String = CertEndorseDAL.COL_NAME_CREATED_BY
        Public Const COL_CREATED_DATE As String = CertEndorseDAL.COL_NAME_CREATED_DATE
        Public Const COL_ENDORSE_NUMB As String = CertEndorseDAL.COL_NAME_ENDORSEMENT_NUMBER
        Public Const COL_ENDORSEMENT_REASON As String = CertEndorseDAL.COL_ENDORSEMENT_REASON
        Public Const COL_ENDORSEMENT_TYPE As String = CertEndorseDAL.COL_ENDORSEMENT_TYPE
        Public Const COL_EFFECTIVE_DATE As String = CertEndorseDAL.COL_EFFECTIVE_DATE
        Public Const COL_EXPIRATION_DATE As String = CertEndorseDAL.COL_EXPIRATION_DATE

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "Private Functions"


    'Private Function isECSDurationFix(ByVal DealerId As Guid) As Boolean

    '    Dim oContract As Contract = Contract.GetCurrentContract(DealerId)

    '    If Not oContract Is Nothing Then
    '        Return True
    '    End If

    '    Return False

    'End Function

#End Region

#Region "Children"
    Public ReadOnly Property CertEndorseCovChildren() As CertEndorseCov.CertEndorsementCovCollection
        Get
            If Me.Dataset.Tables.IndexOf(CertEndorseCovDAL.TABLE_NAME) < 0 Then
                CertEndorseCov.LoadListIntoParentFamily(Me)
            End If
            Return New CertEndorseCov.CertEndorsementCovCollection(Me)
        End Get
    End Property


    Public Function AddNewEndorseCV(ByVal cov As CertItemCoverage) As CertEndorseCov

        Dim child As CertEndorseCov = Me.CertEndorseCovChildren.GetNewChild
        child.CertEndorseId = Me.CertEndorseId
        child.CoverageTypeId = cov.CoverageTypeId
        child.BeginDatePre = New DateType(cov.BeginDate)
        child.BeginDatePost = New DateType(cov.BeginDate)
        child.EndDatePre = New DateType(cov.EndDate)
        child.EndDatePost = New DateType(cov.EndDate)

        'Return child
    End Function

    Private Sub updateEndorseCov(ByVal cov As CertItemCoverage)

        For Each endorseCovRow As DataRow In Me.Dataset.Tables(CertEndorseCovDAL.TABLE_NAME).Rows
            Dim oGuid As Guid = New Guid(CType(endorseCovRow(CertEndorseCovDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))

            If oGuid.Equals(cov.CoverageTypeId) Then
                endorseCovRow(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST) = cov.BeginDate.Value
                endorseCovRow(CertEndorseCovDAL.COL_NAME_END_DATE_POST) = cov.EndDate.Value
            End If
        Next

    End Sub

    Public Sub LoadTerms(ByVal parent As CertEndorse)

        For Each endcov As CertEndorseCov In Me.AssociatedEndorseCoverages
            For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
                If endcov.CoverageTypeId.Equals(cov.CoverageTypeId) Then
                    If cov.CoverageTypeCode = MANUFACTURER Then
                        Me.TermPre = CType(DateDiff(DateInterval.Month, endcov.BeginDatePre.Value, DateAdd("d", 1, endcov.EndDatePre.Value)), Integer)
                        Me.TermPos = CType(DateDiff(DateInterval.Month, endcov.BeginDatePost.Value, DateAdd("d", 1, endcov.EndDatePost.Value)), Integer)
                    End If
                End If
            Next
        Next

    End Sub
#End Region

#Region "Validations"

    'Private Sub ValidateSalesPrice()
    '    Dim validateCode As Integer = 0
    '    Dim MinCovBeginDate, MaxCovEndDate,CovBeginDate,CovEndDate As Date, certDur, covDur As Integer
    '    Dim dv As DataView, i As Integer

    '    If Me.SalesPriceisDirty Or Me.TermisDirty Then

    '        dv = Me.AssociatedItemCoverages.Parent.Dataset.Tables("elp_cert_endorse_cov").DefaultView
    '        If dv.Count > 0 Then
    '            MinCovBeginDate = Date.Parse(Convert.ToDateTime(dv(0)(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST)))
    '            MaxCovEndDate = Date.Parse(Convert.ToDateTime(dv(0)(CertEndorseCovDAL.COL_NAME_END_DATE_POST)))
    '            For i = 0 To dv.Count - 1
    '                If MinCovBeginDate > Date.Parse(Convert.ToDateTime(dv(i)(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST))) Then
    '                    MinCovBeginDate = Date.Parse(Convert.ToDateTime(dv(i)(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST)))
    '                End If
    '                If MaxCovEndDate < Date.Parse(Convert.ToDateTime(dv(i)(CertEndorseCovDAL.COL_NAME_END_DATE_POST))) Then
    '                    MaxCovEndDate = Date.Parse(Convert.ToDateTime(dv(i)(CertEndorseCovDAL.COL_NAME_END_DATE_POST)))
    '                End If
    '                certDur = DateDiff(MONTH, MaxCovEndDate, MinCovBeginDate)
    '            Next
    '        End If
    '        For Each cov As CertItemCoverage In Me.AssociatedItemCoverages
    '            CovBeginDate = Date.Parse(Convert.ToDateTime(dv(i)(CertEndorseCovDAL.COL_NAME_BEGIN_DATE_POST)))
    '            CovEndDate = Date.Parse(Convert.ToDateTime(dv(i)(CertEndorseCovDAL.COL_NAME_END_DATE_POST)))
    '            covDur = DateDiff(MONTH, CovEndDate, CovBeginDate)
    '            validateCode = CertEndorse.ValidateCertSalesPrice(Me.CertId, Me.SalesPricePost.Value, certDur, covDur)
    '            If validateCode <> 0 Then
    '                Select Case validateCode
    '                    Case 100
    '                        Throw New BOValidationException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEFINITION_NOT_FOUND_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEFINITION_NOT_FOUND_ERR)
    '                    Case 200
    '                        Throw New BOValidationException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SALES_PRICE_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SALES_PRICE_ERR)
    '                End Select
    '            End If
    '        Next
    '    End If

    'End Sub

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
     Public NotInheritable Class EmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)

            If obj.EmailPost Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.EmailPost)

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
       Public NotInheritable Class ValidSalesPrice
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DEFINITION_NOT_FOUND_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim validateCode As Integer = 0
            Dim oErrMess As String
            Dim MinCovBeginDate, MaxCovEndDate, CovBeginDate, CovEndDate As Date
            Dim dv As DataView
            Dim i, certDur, covDur As Integer, CovTypeId As Guid

            'REQ-1162
            If obj.DealerTypeCode = Codes.DEALER_TYPES__VSC Then Return True

            dv = obj.Dataset.Tables("elp_cert_item_coverage").DefaultView
            If dv.Count > 0 Then
                MinCovBeginDate = Date.Parse(Convert.ToDateTime(dv(0)(CertItemCoverageDAL.COL_NAME_BEGIN_DATE)))
                MaxCovEndDate = Date.Parse(Convert.ToDateTime(dv(0)(CertItemCoverageDAL.COL_NAME_END_DATE)))
                For i = 0 To dv.Count - 1
                    If MinCovBeginDate > Date.Parse(Convert.ToDateTime(dv(i)(CertItemCoverageDAL.COL_NAME_BEGIN_DATE))) Then
                        MinCovBeginDate = Date.Parse(Convert.ToDateTime(dv(i)(CertItemCoverageDAL.COL_NAME_BEGIN_DATE)))
                    End If
                    If MaxCovEndDate < Date.Parse(Convert.ToDateTime(dv(i)(CertItemCoverageDAL.COL_NAME_END_DATE))) Then
                        MaxCovEndDate = Date.Parse(Convert.ToDateTime(dv(i)(CertItemCoverageDAL.COL_NAME_END_DATE)))
                    End If
                Next
                MaxCovEndDate = DateAdd(DateInterval.Day, 1, MaxCovEndDate)
                certDur = DateDiff(MONTH, MinCovBeginDate, MaxCovEndDate)
            End If
            For i = 0 To dv.Count - 1
                If Convert.ToDecimal(dv(i)(CertItemCoverageDAL.COL_NAME_PREMIUM_WRITTEN)) <> 0 Then
                    CovTypeId = New Guid(CType(dv(i)(CertItemCoverageDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
                    CovBeginDate = Date.Parse(Convert.ToDateTime(dv(i)(CertItemCoverageDAL.COL_NAME_BEGIN_DATE)))
                    CovEndDate = Date.Parse(Convert.ToDateTime(dv(i)(CertItemCoverageDAL.COL_NAME_END_DATE)))
                    CovEndDate = DateAdd(DateInterval.Day, 1, CovEndDate)
                    covDur = DateDiff(MONTH, CovBeginDate, CovEndDate)
                    validateCode = CertEndorse.ValidateCertSalesPrice(obj.CertId, obj.SalesPricePost.Value, CovTypeId, certDur, covDur)
                    If validateCode <> 0 Then
                        Select Case validateCode
                            Case 100
                                oErrMess = Common.ErrorCodes.GUI_DEFINITION_NOT_FOUND_ERR
                                MyBase.Message = oErrMess
                            Case 200
                                oErrMess = Common.ErrorCodes.GUI_INVALID_SALES_PRICE_ERR
                                MyBase.Message = oErrMess
                        End Select
                        Return False
                    End If
                End If
            Next
            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class ValidProductSalesDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_CANNOT_BE_HIGHER_THAN_WARRANTY_SALES_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim WarrantySalesDate As Date = obj.GetShortDate(obj.WarrantySalesDatePost.Value)
            Dim ProductSalesDate As Date = obj.GetShortDate(obj.ProductSalesDatePost.Value)
            Dim oManufaturerWarranty As Boolean = obj.ManufaturerWarranty

            If Not oManufaturerWarranty Then Return True

            If ProductSalesDate > WarrantySalesDate Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class ValidWarrantySalesDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_CANNOT_BE_LOWER_THAN_PRODUCT_SALES_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim WarrantySalesDate As Date = obj.GetShortDate(obj.WarrantySalesDatePost.Value)
            Dim ProductSalesDate As Date = obj.GetShortDate(obj.ProductSalesDatePost.Value)
            Dim oManufaturerWarranty As Boolean = obj.ManufaturerWarranty

            If oManufaturerWarranty Then Return True

            If ProductSalesDate > WarrantySalesDate Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
        Public NotInheritable Class LowDateEnterForProductSalesDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_CANNOT_BE_LOWER_THAN_6_M0NTHS_FROM_CURRENT_COVERAGE_BEGIN_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim EnterSalesDate As Date

            If Not obj.ManufaturerWarranty Then Return True
            If Not obj.ProductSalesDatesisDirty Then Return True

            EnterSalesDate = obj.GetShortDate(obj.ProductSalesDatePost.Value)

            If EnterSalesDate >= obj.ProductSalesDatePre.Value Then Return True

            Dim intDuration As Integer = DateDiff(MONTH, EnterSalesDate, obj.ProductSalesDatePre.Value)

            If intDuration > MAX_MONTH_DIFF Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
            Public NotInheritable Class LowDateEnterForWarrantySalesDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_CANNOT_BE_LOWER_THAN_6_M0NTHS_FROM_CURRENT_COVERAGE_BEGIN_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim EnterSalesDate As Date

            If obj.ManufaturerWarranty Then Return True
            If Not obj.WarrantySalesDatesisDirty Then Return True

            EnterSalesDate = obj.GetShortDate(obj.WarrantySalesDatePost.Value)

            If EnterSalesDate >= obj.WarrantySalesDatePre.Value Then Return True

            Dim intDuration As Integer = DateDiff(MONTH, EnterSalesDate, obj.WarrantySalesDatePre.Value)

            If intDuration > MAX_MONTH_DIFF Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
               Public NotInheritable Class HigherDateEnterForProductSalesDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_CANNOT_BE_HIGHER_THAN_CERTIFICATE_DATE_ADDED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim EnterSalesDate As Date

            EnterSalesDate = obj.GetShortDate(obj.ProductSalesDatePost.Value)

            If EnterSalesDate > obj.DateAdded Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
               Public NotInheritable Class HigherDateEnterForWarrantySalesDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_CANNOT_BE_HIGHER_THAN_CERTIFICATE_DATE_ADDED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim EnterSalesDate As Date

            EnterSalesDate = obj.GetShortDate(obj.WarrantySalesDatePost.Value)

            If EnterSalesDate > obj.DateAdded Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class NonFutureDocumentIssueDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.FUTURE_DATES_NOT_ALLOWED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim objCert As New Certificate(obj.CertId)
            Dim strValFlag As String
            strValFlag = objCert.GetValFlag()
            If strValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If

            If strValFlag = VALIDATION_FLAG_FULL Then
                If Not obj.DocumentIssueDatePost Is Nothing Then
                    If obj.DocumentIssueDatePost.Value > Date.Today Then
                        Return False
                    End If
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class NewValueMandatory
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim objCert As New Certificate(obj.CertId)
            Dim bIsOk As Boolean = True
            Dim DocType As String = obj.getDocTypeCode
            Dim Dealer As Dealer
            Dim DealerTypeCode As Guid
            Dim strValFlag As String
            DealerTypeCode = Dealer.GetDealerTypeId(objCert.DealerId)
            strValFlag = objCert.GetValFlag()

            If strValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If

            If strValFlag = VALIDATION_FLAG_FULL Then
                'DOC_TYPE_CNPJ
                If UCase(DocType) = DOC_TYPE_CNPJ Then
                    If obj.RgNumberPost Is Nothing And obj.DocumentAgencyPost Is Nothing And obj.DocumentIssueDatePost Is Nothing And obj.IdTypePost Is Nothing Then
                        Return True
                    Else
                        MyBase.Message = Common.ErrorCodes.GUI_FIELD_MUST_BE_BLANK
                        Return False
                    End If
                End If

                'DOC_TYPE_CPF
                'VSC
                If (LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, DealerTypeCode) = "2" And UCase(DocType) = DOC_TYPE_CPF) Then
                    If obj.RgNumberPost Is Nothing And obj.DocumentAgencyPost Is Nothing And obj.DocumentIssueDatePost Is Nothing And obj.IdTypePost Is Nothing Then
                        Return False
                    End If
                End If

                'ESC
                If (LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, DealerTypeCode) = "1" And UCase(DocType) = DOC_TYPE_CPF) Then
                    Return True
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMustBeBlankForDocumentNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_FIELD_MUST_BE_BLANK)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim objCert As New Certificate(obj.CertId)
            Dim strValFlag As String
            strValFlag = objCert.GetValFlag()

            If strValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If

            If strValFlag = VALIDATION_FLAG_CPF_CNPJ Then
                If UCase(obj.getDocTypeCode) = DOC_TYPE_CON Then
                    Return True

                End If
            End If

            If UCase(obj.getDocTypeCode) = DOC_TYPE_CON Then
                If obj.TaxIDNumbPost <> Nothing AndAlso obj.TaxIDNumbPost <> String.Empty Then
                    Return False
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class SPValidationDocumentNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ERROR_FOUND_BY_ORACLE_VALIDATION)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim dal As New CertificateDAL
            Dim oErrMess As String
            Dim objCert As New Certificate(obj.CertId)
            Dim strValFlag As String
            strValFlag = objCert.GetValFlag()

            Try

                Select Case strValFlag
                    Case VALIDATION_FLAG_NONE
                        Return True
                    Case VALIDATION_FLAG_FULL
                        If obj.TaxIDNumbPost Is Nothing Or _
                            obj.TaxIDNumbPost = String.Empty Then
                            If obj.getDocTypeCode = DOC_TYPE_CON Then
                                Return True
                            Else
                                oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.TaxIDNumbPost)
                                If Not oErrMess Is Nothing Then
                                    MyBase.Message = UCase(oErrMess)
                                    Return False

                                End If
                            End If
                        Else
                            oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.TaxIDNumbPost)
                            If Not oErrMess Is Nothing Then
                                MyBase.Message = UCase(oErrMess)
                                Return False
                            End If
                        End If

                    Case VALIDATION_FLAG_PARTIAL
                        If (obj.getDocTypeCode <> DOC_TYPE_CPF AndAlso obj.getDocTypeCode <> DOC_TYPE_CNPJ) _
                            AndAlso (obj.TaxIDNumbPost Is Nothing OrElse obj.TaxIDNumbPost.Trim.Length = 0) Then
                            Return True
                        End If

                        'If Me.DisplayName = IDENTIFICATION_NUMBER _
                        '    AndAlso (Not obj.DocumentIssueDatePost Is Nothing _
                        '        Or Not obj.IdTypePost Is Nothing _
                        '        Or Not obj.DocumentAgencyPost Is Nothing _
                        '        Or Not obj.RgNumberPost Is Nothing _
                        '        Or Not obj.DocumentTypeIDPost.Equals(Guid.Empty)) Then
                        '    Return True
                        'End If

                        oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.TaxIDNumbPost)
                        If Not oErrMess Is Nothing Then
                            MyBase.Message = UCase(oErrMess)
                            Return False
                        End If

                    Case VALIDATION_FLAG_CPF_CNPJ
                        If obj.TaxIDNumbPost Is Nothing Then
                            obj.TaxIDNumbPost = String.Empty
                        End If
                        oErrMess = dal.ExecuteSP(obj.getDocTypeCode, obj.TaxIDNumbPost)
                        If Not oErrMess Is Nothing Then
                            MyBase.Message = UCase(oErrMess)
                            Return False
                        End If
                    Case Else
                        Return True
                End Select

                Return True

            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueTaxIdLenht
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_IS_TOO_SHORT_OR_TOO_LONG)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim objCert As New Certificate(obj.CertId)

            If Not objCert.IsCompanyTypeInsurance Then
                Return True
            End If

            If obj.TaxIDNumbPost Is Nothing Then
                Return True
            End If

            If obj.TaxIDNumbPost.Length > 15 Then
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryDocumentType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_DOCUMENT_TYPE_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertEndorse = CType(objectToValidate, CertEndorse)
            Dim objCert As New Certificate(obj.CertId)
            Dim strValFlag As String
            strValFlag = objCert.GetValFlag()
            If strValFlag = VALIDATION_FLAG_NONE Then
                Return True
            End If

            If strValFlag = VALIDATION_FLAG_NO_VALIDATION Then
                Return True
            End If

            Dim docType As String = obj.getDocTypeCode
            If strValFlag = VALIDATION_FLAG_CPF_CNPJ Then
                If UCase(docType) = DOC_TYPE_CPF _
                    Or UCase(docType) = DOC_TYPE_CNPJ Then
                    Return True
                Else
                    MyBase.Message = UCase(Common.ErrorCodes.GUI_DOCUMENT_TYPE_4)
                    Return False
                End If
            End If

            If UCase(docType) = DOC_TYPE_CPF _
               Or UCase(docType) = DOC_TYPE_CON _
               Or UCase(docType) = DOC_TYPE_CNPJ Then
                Return True
            Else
                MyBase.Message = UCase(Common.ErrorCodes.GUI_DOCUMENT_TYPE)
                Return False
            End If

        End Function
    End Class
#End Region

End Class




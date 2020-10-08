Imports System.Globalization
Imports System.ServiceModel
Imports System.Xml
Imports Assurant.Elita
Imports Assurant.Elita.PciSecure.Attributes

Public Class CreditCardAuthVSCBrazil
    Inherits BusinessObjectBase
#Region "Constants"

    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "Customer_Name"
    Public Const DATA_COL_NAME_DOC_NUM As String = "Document_Num"
    Public Const DATA_COL_NAME_CERT_NUM As String = "Certificate_num"
    Public Const DATA_COL_NAME_AMOUNT As String = "Amount"
    Public Const DATA_COL_NAME_NUM_OF_INSTALLMENTS As String = "Num_Of_Installments"
    Public Const DATA_COL_NAME_CARD_TYPE As String = "Card_Type"
    Public Const DATA_COL_NAME_NAME_ON_CARD As String = "Name_On_Card"
    Public Const DATA_COL_NAME_CARD_NUM As String = "Credit_Card_Num"
    Public Const DATA_COL_NAME_CARD_SECURITY_CODE As String = "Card_Security_Code"
    Public Const DATA_COL_NAME_CARD_EXPIRATION As String = "Card_Expiration"
    Public Const DATA_COL_NAME_DBS_COMPANY_CODE As String = "Dbs_Company_Code"
    Public Const DATA_COL_NAME_DBS_PRODUCT_CODE As String = "Dbs_Product_Code"
    Public Const DATA_COL_NAME_DBS_SYSTEM_CODE As String = "Dbs_System_Code"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "Dealer_Code"
    Public Const DATA_COL_NAME_EMAIL As String = "Email"
    Public Const DATA_COL_NAME_MOBILE_AREA_CODE As String = "Mobile_Area_Code"
    Public Const DATA_COL_NAME_MOBILE As String = "Mobile"
    Public Const DATA_COL_NAME_PHONE_AREA_CODE As String = "Phone_Area_Code"
    Public Const DATA_COL_NAME_PHONE As String = "Phone"
    Public Const DATA_COL_NAME_WARRANTY_SALES_DATE As String = "Warranty_Sales_Date"
    Public Const DATA_COL_NAME_CARD_OWNER_TAX_ID As String = "Card_Owner_Tax_Id"
    Public Const DATA_COL_NAME_DBS_PAYMENT_TYPE As String = "Dbs_Payment_Type"
    Public Const DATA_COL_NAME_DUE_DATE As String = "Due_Date"
    Public Const DATA_COL_NAME_EXPIRED_DATE As String = "Expired_Date"

    Private Const TABLE_NAME As String = "CreditCardAuthVSCBrazil"
    Private Const DATASET_NAME As String = "CreditCardAuthVSCBrazilDs"

#End Region
#Region "Constructors"

    Public Sub New(ds As CreditCardAuthVSCBrazilDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region
#Region "Private Members"
    Private Sub MapDataSet(ds As CreditCardAuthVSCBrazilDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    Private Sub Load(ds As CreditCardAuthVSCBrazilDs)
        Try
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            ValidateInput()
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("CreditCardAuthVSCBrazil Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As CreditCardAuthVSCBrazilDs)
        Try
            If ds.CreditCardAuthVSCBrazil.Count = 0 Then Exit Sub

            With ds.CreditCardAuthVSCBrazil.Item(0)
                'todo - Initialize the incoming search criteria
                CustomerName = .Customer_Name.Trim
                DocumentNum = .Document_Num.Trim
                CertNum = .Certificate_num.Trim
                Amount = .Amount
                NumberOfInstallments = .Num_Of_Installments
                NameOnCard = .Name_On_Card.Trim
                CardNum = .Credit_Card_Num.Trim
                CardSecurityCode = .Card_Security_Code.Trim
                CardExpiration = .Card_Expiration.Trim
                CardType = MapCardType(.Card_Type.Trim)
                DbsCompanyCode = .Dbs_Company_Code
                DbsProductCode = .Dbs_Product_Code
                DbsSystemCode = .Dbs_System_Code
                DealerCode = .Dealer_Code
                Email = .Email.Trim
                MobileAreaCode = .Mobile_Area_Code
                Mobile = .Mobile
                PhoneAreaCode = .Phone_Area_Code
                Phone = .Phone
                WarrantySalesDate = .Warranty_Sales_Date
                CardOwnerTaxId = .Card_Owner_Tax_Id.Trim
                DbsPaymentType = .Dbs_Payment_Type
                DueDate = .Due_Date
                ExpiredDate = .Expired_Date

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetClaimNumByCertAndPhone Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Private Function MapCardType(strCardType As String) As String
        Dim strNewType As String
        strNewType = CodeMapping.GetCovertedCode(Authentication.CompId, "CCTYPE", strCardType)
        'Select Case strCardType
        '    Case "VI"
        '        strNewType = "40"
        '    Case "MS"
        '        strNewType = "41"
        '    Case "AE"
        '        strNewType = "42"
        '    Case "AU"
        '        strNewType = "43"
        '    Case "DI"
        '        strNewType = "44"
        '    Case "HI"
        '        strNewType = "45"
        '    Case Else
        '        strNewType = strCardType
        'End Select
        If strNewType = String.Empty Then strNewType = strCardType
        Return strNewType
    End Function

    Private Sub ValidateInput()
        ' todo: validate required fields
        If CardNum = String.Empty OrElse CardType = String.Empty OrElse CardExpiration = String.Empty _
            OrElse CardSecurityCode = String.Empty OrElse NameOnCard = String.Empty OrElse Amount <= 0 _
            OrElse NumberOfInstallments <= 0 Then
            Throw New BOValidationException("VSC Enrollment Error: ", Common.ErrorCodes.WS_XML_INVALID)
        End If

    End Sub

    Private Shared Function GetEndPoint(url As String) As EndpointAddress
        Dim eab As EndpointAddressBuilder

        eab = New EndpointAddressBuilder
        eab.Uri = New Uri(url)

        Return eab.ToEndpointAddress
    End Function

    Private Function GetBinding() As BasicHttpBinding
        Dim bind As New BasicHttpBinding()

        bind.Name = "BillingServiceBeanServiceSoapBinding"
        bind.CloseTimeout = TimeSpan.Parse("00:01:00")
        bind.OpenTimeout = TimeSpan.Parse("00:01:00")
        bind.ReceiveTimeout = TimeSpan.Parse("00:10:00")
        bind.SendTimeout = TimeSpan.Parse("00:01:00")
        bind.AllowCookies = False
        bind.BypassProxyOnLocal = False
        bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard
        bind.MaxBufferSize = 65536
        bind.MaxBufferPoolSize = 524288
        bind.MaxReceivedMessageSize = 65536
        bind.MessageEncoding = WSMessageEncoding.Text
        bind.TextEncoding = Text.Encoding.UTF8
        bind.TransferMode = TransferMode.Buffered
        bind.UseDefaultWebProxy = True
        ' readerQuotas
        bind.ReaderQuotas.MaxDepth = 32
        bind.ReaderQuotas.MaxStringContentLength = 8192
        bind.ReaderQuotas.MaxArrayLength = 16384
        bind.ReaderQuotas.MaxBytesPerRead = 4096
        bind.ReaderQuotas.MaxNameTableCharCount = 16384
        ' Security
        bind.Security.Mode = SecurityMode.None
        '   Transport
        bind.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
        bind.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None
        '   Message
        bind.Security.Message.AlgorithmSuite = ServiceModel.Security.SecurityAlgorithmSuite.Default
        'bind.Security.Message.ClientCredentialType = MessageCredentialType.UserName

        Return bind
    End Function

    Private Shared Function GenerateToken() As String
        Dim Key = "AssurantBrasil"
        Dim dbsToken As String = ""
        Dim time As Date
        If (Key.Trim().Length() > 0) Then
            Try
                Dim strBase As String = Key + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString()
                Dim strMap As String = " ABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÃÕÑÂÊÎÔÛÄËÏÖÜÀÈÌÒÙabcdefghijklmnopqrstuvwxyz0123456789áéíóúãõñâêîôûäëïöüàèìòùÇç/"
                Dim strEncrypt As String = "89áéíxyz01ìòùÇç67óúãÒÙabcvw23lmJKLM ABCD45õñâpqrstuêîÜÀÈÌôûäëïöüÃÕÑàèÏÖdeÉÍÔÛÄYZÁÓÚÂfgOPQ/XÊÎËINSTVWhijkGHUnEFoR"
                Dim position As Integer = 0
                Dim character As Char = ControlChars.Lf

                For i As Integer = 0 To strBase.Length - 1
                    Dim length_i As Integer = (i + 1) - i
                    Dim length_position As Integer = (position + 1) - position

                    position = strMap.IndexOf(strBase.Substring(i, length_i))
                    If position > 0 Then
                        character = strEncrypt.Substring(position, length_position)(0)
                    Else
                        character = strBase.Substring(i, length_i)(0)
                    End If
                    dbsToken = dbsToken + CharToAsc(character, DateTime.Today.Day).ToString()
                Next
            Catch ex As Exception
                Return String.Empty
            End Try
        End If
        Return dbsToken
    End Function


    Private Shared Function CharToAsc(character As Char, increase As Integer) As Integer
        Return (Convert.ToInt32(character) + increase)
    End Function

    Private Function GetResponseXML(blnSuccess As Boolean, strAuthNum As String,
                                    strRejectCode As String, strRejectMsg As String) As String

        Dim objDoc As New Xml.XmlDocument
        Dim objRoot As Xml.XmlElement
        Dim objE As XmlElement

        Dim objDecl As XmlDeclaration = objDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)

        objRoot = objDoc.CreateElement("CreditCardAuthVSCBrazilResult")
        objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
        objDoc.AppendChild(objRoot)

        objE = objDoc.CreateElement("Status")
        objRoot.AppendChild(objE)
        objE.InnerText = blnSuccess.ToString


        objE = objDoc.CreateElement("Reject_code")
        objRoot.AppendChild(objE)
        objE.InnerText = strRejectCode

        objE = objDoc.CreateElement("Reject_message")
        objRoot.AppendChild(objE)
        objE.InnerText = strRejectMsg

        objE = objDoc.CreateElement("Authorization_Num")
        objRoot.AppendChild(objE)
        objE.InnerText = strAuthNum

        Return objDoc.OuterXml


    End Function
#End Region

#Region "Properties"

    Public Property CertNum As String
        Get
            If Row(DATA_COL_NAME_CERT_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERT_NUM), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_CERT_NUM, Value)
        End Set
    End Property

    Public Property CustomerName As String
        Get
            If Row(DATA_COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property


    Public Property DocumentNum As String
        Get
            If Row(DATA_COL_NAME_DOC_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DOC_NUM), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DOC_NUM, Value)
        End Set
    End Property

    Public Property Amount As Decimal
        Get
            If Row(DATA_COL_NAME_AMOUNT) Is DBNull.Value Then
                Return 0
            Else
                Return (CType(Row(DATA_COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_AMOUNT, Value)
        End Set
    End Property

    Public Property NumberOfInstallments As Integer
        Get
            If Row(DATA_COL_NAME_NUM_OF_INSTALLMENTS) Is DBNull.Value Then
                Return 0
            Else
                Return CType(Row(DATA_COL_NAME_NUM_OF_INSTALLMENTS), Integer)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_NUM_OF_INSTALLMENTS, Value)
        End Set
    End Property

    Public Property NameOnCard As String
        Get
            If Row(DATA_COL_NAME_NAME_ON_CARD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_NAME_ON_CARD), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_NAME_ON_CARD, Value)
        End Set
    End Property
    <PciReveal(PciDataType.CreditCardNumber), PciProtect(PciDataType.CreditCardNumber)>
    Public Property CardNum As String
        Get
            If Row(DATA_COL_NAME_CARD_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CARD_NUM), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_CARD_NUM, Value)
        End Set
    End Property

    Public Property CardSecurityCode As String
        Get
            If Row(DATA_COL_NAME_CARD_SECURITY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CARD_SECURITY_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_CARD_SECURITY_CODE, Value)
        End Set
    End Property

    Public Property CardExpiration As String
        Get
            If Row(DATA_COL_NAME_CARD_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CARD_EXPIRATION), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_CARD_EXPIRATION, Value)
        End Set
    End Property

    Public Property CardType As String
        Get
            If Row(DATA_COL_NAME_CARD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CARD_TYPE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_CARD_TYPE, Value)
        End Set
    End Property

    Public Property DbsCompanyCode As String
        Get
            If Row(DATA_COL_NAME_DBS_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DBS_COMPANY_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DBS_COMPANY_CODE, Value)
        End Set
    End Property

    Public Property DbsProductCode As String
        Get
            If Row(DATA_COL_NAME_DBS_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DBS_PRODUCT_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DBS_PRODUCT_CODE, Value)
        End Set
    End Property

    Public Property DbsSystemCode As String
        Get
            If Row(DATA_COL_NAME_DBS_SYSTEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DBS_SYSTEM_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DBS_SYSTEM_CODE, Value)
        End Set
    End Property

    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    Public Property Email As String
        Get
            If Row(DATA_COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_EMAIL), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_EMAIL, Value)
        End Set
    End Property

    Public Property Mobile As String
        Get
            If Row(DATA_COL_NAME_MOBILE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_MOBILE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_MOBILE, Value)
        End Set
    End Property

    Public Property MobileAreaCode As String
        Get
            If Row(DATA_COL_NAME_MOBILE_AREA_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_MOBILE_AREA_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_MOBILE_AREA_CODE, Value)
        End Set
    End Property

    Public Property PhoneAreaCode As String
        Get
            If Row(DATA_COL_NAME_PHONE_AREA_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_PHONE_AREA_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_PHONE_AREA_CODE, Value)
        End Set
    End Property

    Public Property Phone As String
        Get
            If Row(DATA_COL_NAME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_PHONE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_PHONE, Value)
        End Set
    End Property

    Public Property WarrantySalesDate As String
        Get
            If Row(DATA_COL_NAME_WARRANTY_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_WARRANTY_SALES_DATE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_WARRANTY_SALES_DATE, Value)
        End Set
    End Property

    Public Property CardOwnerTaxId As String
        Get
            If Row(DATA_COL_NAME_CARD_OWNER_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CARD_OWNER_TAX_ID), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_CARD_OWNER_TAX_ID, Value)
        End Set
    End Property

    Public Property DbsPaymentType As String
        Get
            If Row(DATA_COL_NAME_DBS_PAYMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DBS_PAYMENT_TYPE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DBS_PAYMENT_TYPE, Value)
        End Set
    End Property

    Public Property DueDate As String
        Get
            If Row(DATA_COL_NAME_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DUE_DATE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DUE_DATE, Value)
        End Set
    End Property

    Public Property ExpiredDate As String
        Get
            If Row(DATA_COL_NAME_EXPIRED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_EXPIRED_DATE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_EXPIRED_DATE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Function ProcessWSRequest() As String

        Dim dbs As VSCBrazilDBSCreditCardAuth.billingClient
        Dim strRejectCode As String, strRejectMsg As String, strAuthNum As String, blnSuccess As Boolean

        Dim oWebPasswd As WebPasswd
        Dim strToken As String
        Dim strURL As String
        Dim strCreditCardNumber As String 
        Dim isCardNumberTokenize As Boolean = True
        Dim strTokenizeCreditCardNumber As String  = CardNum

        If Not CardNum Is Nothing Then
            Try
                ' reveal the credit card number
                Reveal()
                ' de-Tokenize credit card number if valid token is available
                strCreditCardNumber = CardNum
            Catch ex As Exception
                If Not ex.InnerException Is Nothing AndAlso ex.InnerException.GetType() Is GetType(FaultException) Then
                    Dim faultExcep As FaultException
                    faultExcep = DirectCast(ex.InnerException, FaultException)
                    If faultExcep.Code.Name.Equals("110") Then
                        ' It is not a token value to reveal so it throw error code = 101 and error message Invalid Token
                        ' in this case we assume that input value is actual credit card information
                        strCreditCardNumber = CardNum
                        isCardNumberTokenize = False
                    Else
                        Throw New ElitaPlusException(faultExcep.Message, faultExcep.Code.Name, ex.InnerException)
                    End If
                Else
                    Throw New ElitaPlusException(ex.Message, ElitaPlus.Common.ErrorCodes.PCI_SECURE_ERR, ex)
                End If
            End Try
        End If

        If Not isCardNumberTokenize Then
            Try
                ' Secure the credit card number
                Secure()
                strTokenizeCreditCardNumber = CardNum
            Catch ex As Exception
                If Not ex.InnerException Is Nothing AndAlso ex.InnerException.GetType() Is GetType(FaultException) Then
                    Dim faultExcep As FaultException
                    faultExcep = DirectCast(ex.InnerException, FaultException)
                    Throw New ElitaPlusException(faultExcep.Message, faultExcep.Code.Name, ex.InnerException)
                Else
                    Throw New ElitaPlusException(ex.Message, ElitaPlus.Common.ErrorCodes.PCI_SECURE_ERR, ex)
                End If
            End Try
        End If

        Try
            oWebPasswd = New WebPasswd(Authentication.CompanyGroupId, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__BR_CC_AUTH), True)
            strURL = oWebPasswd.Url
            strToken = GenerateToken()

            dbs = New VSCBrazilDBSCreditCardAuth.billingClient(GetBinding, GetEndPoint(strURL))

            Dim objDbsResult As VSCBrazilDBSCreditCardAuth.retornoIniciarVendaCartaoCredito
            dbs.Open()

            objDbsResult = dbs.iniciarVendaCartaoCreditoV1(strToken, DbsCompanyCode, DbsProductCode, DbsSystemCode, DocumentNum, CustomerName, Email, MobileAreaCode, Mobile, PhoneAreaCode, Phone, CertNum, WarrantySalesDate, CardType, CardOwnerTaxId, NameOnCard, strCreditCardNumber, CardExpiration, CardSecurityCode, DbsPaymentType, NumberOfInstallments.ToString, Amount.ToString, DueDate, ExpiredDate, DealerCode, String.Empty, String.Empty)

            strRejectCode = objDbsResult.retorno

            If objDbsResult.retorno = 0 Then
                blnSuccess = True
                strRejectMsg = Nothing
            Else
                blnSuccess = False
                strRejectMsg = objDbsResult.mensagemRetorno + "(" + objDbsResult.codigoErro + ":" + objDbsResult.mensagemErro + ")"
            End If
            strAuthNum = objDbsResult.codigoVendaBilling

        Catch ex As Exception
            strRejectMsg = ex.Message
        Finally
            If Not dbs Is Nothing Then
                dbs.Close()
            End If
        End Try

        Try
            Dim objTrans As New TransCcauthVscbrazil
            With objTrans
                .CustomerName = CustomerName
                .DocumentNum = DocumentNum
                .CertificateNum = CertNum
                .Amount = Amount
                .NumOfInstallments = NumberOfInstallments
                .NameOnCard = NameOnCard
                .CardNum = strTokenizeCreditCardNumber
                .CardType = CardType
                .CardExpiration = CardExpiration
                .DbsCompanyCode = DbsCompanyCode
                .DbsProductCode = DbsProductCode
                .DbsSystemCode = DbsSystemCode
                .DealerCode = DealerCode
                .Email = Email
                .MobileAreaCode = MobileAreaCode
                .Mobile = Mobile
                .PhoneAreaCode = PhoneAreaCode
                .Phone = Phone
                .WarrantySalesDate = WarrantySalesDate
                .CardOwnerTaxId = CardOwnerTaxId
                .DbsPaymentType = DbsPaymentType
                .DueDate = DueDate
                .ExpiredDate = ExpiredDate
                .AuthStatus = blnSuccess.ToString
                .AuthNum = strAuthNum
                .RejectCode = strRejectCode
                .RejectMsg = strRejectMsg
                .Save()
            End With
        Catch ex As Exception
            'strRejectMsg = ex.Message
        End Try
        Return GetResponseXML(blnSuccess, strAuthNum, strRejectCode, strRejectMsg)

    End Function

#End Region

End Class

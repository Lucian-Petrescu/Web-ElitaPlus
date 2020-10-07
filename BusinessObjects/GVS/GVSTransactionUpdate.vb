Imports System.Text.RegularExpressions
Imports System.ServiceModel

Public Class GVSTransactionUpdate
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "transaction_log_header_id"
    Public Const DATA_COL_NAME_FUNCTION_TYPE As String = "function_type_code"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "TRANSACTION"

    Private Const TABLE_RESULT As String = "RESULT"
    'Private Const LOGIN_OK As String = "Ok"
    'Private Const PROCESS_REQUEST_ERROR As String = "ERROR"

#End Region

#Region "Variables"
    Private msInputXml, msFunctionToProcess As String
#End Region

#Region "Properties"

    Private Property InputXml As String
        Get
            Return msInputXml
        End Get
        Set
            msInputXml = value
        End Set
    End Property

    Private Property FuncToProc As String
        Get
            Return msFunctionToProcess
        End Get
        Set
            msFunctionToProcess = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GVSTransactionUpdateDs, ByVal xml As String, _
                   ByVal functionToProcess As String)
        MyBase.New()
        InputXml = xml
        FuncToProc = functionToProcess
    End Sub

    Public Sub New(ByVal ds As GVSTransactionUpdateDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyTransactionUpdate = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyTransactionUpdate As System.Data.DataSet
    Dim _GVSOriginalTransNo As String
    Dim _functionTypeCode As String
    Dim _transactionId As String

    Private Sub MapDataSet(ByVal ds As GVSTransactionUpdateDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As GVSTransactionUpdateDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GVSTransactionUpdate Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GVSTransactionUpdateDs)
        Try
            If ds.TRANSACTION_HEADER.Count = 0 Or ds.TRANSACTION_DATA_RECORD.Count = 0 Then Exit Sub
            With ds.TRANSACTION_HEADER.Item(0)
                TransactionId = .TRANSACTION_ID
                GVSOriginalTransNo = .GVS_ORIGINAL_TRANS_NO
                FunctionTypeCode = .FUNCTION_TYPE
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GVSTransactionUpdate Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    'Private Shared Function Bind_WSElitaServiceOrderSoap() As BasicHttpBinding
    '    Dim bind As New BasicHttpBinding()

    '    bind.Name = "WSElitaServiceOrderSoap"
    '    bind.CloseTimeout = TimeSpan.Parse("00:01:00")
    '    bind.OpenTimeout = TimeSpan.Parse("00:01:00")
    '    bind.ReceiveTimeout = TimeSpan.Parse("00:10:00")
    '    bind.SendTimeout = TimeSpan.Parse("00:01:00")
    '    bind.AllowCookies = False
    '    bind.BypassProxyOnLocal = False
    '    bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard
    '    bind.MaxBufferSize = 262144
    '    bind.MaxBufferPoolSize = 524288
    '    bind.MaxReceivedMessageSize = 262144
    '    bind.MessageEncoding = WSMessageEncoding.Text
    '    bind.TextEncoding = Text.Encoding.UTF8
    '    bind.TransferMode = TransferMode.Buffered
    '    bind.UseDefaultWebProxy = True
    '    ' readerQuotas
    '    bind.ReaderQuotas.MaxDepth = 32
    '    bind.ReaderQuotas.MaxStringContentLength = 262144
    '    bind.ReaderQuotas.MaxArrayLength = 262144
    '    bind.ReaderQuotas.MaxBytesPerRead = 4096
    '    bind.ReaderQuotas.MaxNameTableCharCount = 16384
    '    ' Security
    '    bind.Security.Mode = SecurityMode.Transport
    '    '   Transport
    '    bind.Security.Transport.ClientCredentialType = HttpClientCredentialType.None
    '    bind.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None
    '    '   Message
    '    bind.Security.Message.AlgorithmSuite = Security.SecurityAlgorithmSuite.Default

    '    Return bind
    'End Function

    'Private Shared Function Get_EndPoint(ByVal url As String) As EndpointAddress
    '    Dim eab As EndpointAddressBuilder

    '    eab = New EndpointAddressBuilder
    '    eab.Uri = New Uri(url)

    '    Return eab.ToEndpointAddress
    'End Function

    'Private Shared Function Get_ServiceClient() As GvsWSRef.WSElitaServiceOrderSoapClient
    '    Dim bind As BasicHttpBinding
    '    Dim ea As EndpointAddress
    '    Dim sc As GvsWSRef.WSElitaServiceOrderSoapClient

    '    bind = Bind_WSElitaServiceOrderSoap()
    '    ea = Get_EndPoint(AppConfig.Gvs.Url)
    '    sc = New GvsWSRef.WSElitaServiceOrderSoapClient(bind, ea)
    '    sc.Endpoint.Name = "WSElitaServiceOrderSoap"

    '    Return sc
    'End Function

    'Private Shared Function SendToGvs(ByVal xmlIn As String, ByVal functionToProcess As String) As String
    '    Dim wsGvs As GvsWSRef.WSElitaServiceOrderSoapClient
    '    Dim gvsToken As String
    '    Dim sLoginMsg As String
    '    Dim xmlOut As String

    '    wsGvs = Get_ServiceClient()
    '    gvsToken = wsGvs.Login(AppConfig.Gvs.UserId, AppConfig.Gvs.Password, sLoginMsg)

    '    If Trim(sLoginMsg) = LOGIN_OK Then
    '        xmlOut = wsGvs.ProcessRequest(gvsToken, functionToProcess, xmlIn)
    '        wsGvs.Close()
    '        If (xmlOut.ToUpper).Contains(PROCESS_REQUEST_ERROR) Then
    '            AppConfig.Log(xmlOut)
    '            Throw New Exception("Error in Gvs ProcessRequest")
    '        End If
    '    Else
    '        wsGvs.Close()
    '        Throw New Exception("No token returned")
    '    End If

    '    Return xmlOut
    'End Function

#End Region

#Region "Properties"
    Public Property TransactionId As String
        Get
            Return _transactionId
        End Get
        Set
            _transactionId = Value
        End Set
    End Property

    Public Property GVSOriginalTransNo As String
        Get
            Return _GVSOriginalTransNo
        End Get
        Set
            _GVSOriginalTransNo = Value
        End Set
    End Property

    Public Property FunctionTypeCode As String
        Get
            Return _functionTypeCode
        End Get
        Set
            _functionTypeCode = Value
        End Set
    End Property

#End Region

#Region "Public Members"

    ' TO KEEP
    Public Overrides Function ProcessWSRequest() As String
        Dim xmlOut As String

        Try
            xmlOut = GVSService.SendToGvs(InputXml, FuncToProc)
            Return xmlOut
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' TO DELETE
    'Public Overrides Function ProcessWSRequest() As String
    '    Try



    '        ' Assume GVS response with OK message
    '        ' Set the acknoledge OK response
    '        Return "SEND_TO_GAP" 'XMLHelper.GetXML_OK_Response

    '    Catch ex As BOValidationException
    '        Throw ex
    '    Catch ex As StoredProcedureGeneratedException
    '        Throw ex
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw ex
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function


#End Region

#Region "Extended Properties"

#End Region

End Class


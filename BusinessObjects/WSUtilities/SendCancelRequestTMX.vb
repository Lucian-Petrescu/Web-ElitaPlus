Imports System.Net
Imports System.IO

Public Class SendCancelRequestTMX
    Inherits BusinessObjectBase
#Region "Constants"
    Public Const DATA_COL_NAME_COMM_SERVICE_CODE As String = "commercial_service_code"
    Public Const DATA_COL_NAME_SUPL_SERVICE_CODE As String = "supl_service_code"
    Public Const DATA_COL_NAME_MOBILE_NUMBER As String = "mobile_number"
    Public Const DATA_COL_NAME_USER_NAME As String = "user_name"
    Private Const TABLE_NAME As String = "SendCancelRequestTMX"
#End Region

#Region "Constructors"

    Public Sub New(ds As SendCancelRequestTMXDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Sub MapDataSet(ds As SendCancelRequestTMXDs)

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

    Private Sub Load(ds As SendCancelRequestTMXDs)
        Try
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("SendCancelRequestTMX Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As SendCancelRequestTMXDs)
        Try
            If ds.SendCancelRequestTMX.Count = 0 Then Exit Sub

            With ds.SendCancelRequestTMX.Item(0)
                'todo - Initialize the incoming search criteria
                MobileNum = .mobile_number.Trim.ToUpper
                SupplementaryServiceCode = .supl_service_code.Trim
                CommercialServiceCode = .commercial_service_code.Trim
                UserName = .user_name.Trim.ToUpper
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("SendCancelRequestTMX Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub
#End Region

#Region "Properties"
    Public Property MobileNum As String
        Get
            If Row(DATA_COL_NAME_MOBILE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_MOBILE_NUMBER), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_MOBILE_NUMBER, Value)
        End Set
    End Property

    Public Property SupplementaryServiceCode As String
        Get
            If Row(DATA_COL_NAME_SUPL_SERVICE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_SUPL_SERVICE_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_SUPL_SERVICE_CODE, Value)
        End Set
    End Property

    Public Property CommercialServiceCode As String
        Get
            If Row(DATA_COL_NAME_COMM_SERVICE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_COMM_SERVICE_CODE), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_COMM_SERVICE_CODE, Value)
        End Set
    End Property

    Public Property UserName As String
        Get
            If Row(DATA_COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_USER_NAME), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_USER_NAME, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim sb As New System.Text.StringBuilder("<?xml version=""1.0"" encoding=""UTF-8"" ?>")
        sb.Append("<Peticion xmlns=""http://www.tmmas.com/SCLIntegrador/xml"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">")
        sb.Append("<Servicio>DEACTIV_SS</Servicio>")
        sb.Append("<Tipo>SINC</Tipo>")
        sb.Append("<Usuario>GENERAL</Usuario>")
        sb.Append("<IdTransaccion>0</IdTransaccion>")
        sb.Append("<TTL>10</TTL> ")
        sb.Append("<idTranCliente>0</idTranCliente>")
        sb.Append("<Parametros>")
        sb.Append("<Par><Nom>EN_num_celular</Nom><Val>")
        sb.Append(MobileNum)
        sb.Append("</Val></Par><Par><Nom>EV_servsupl</Nom><Val>|")
        sb.Append(SupplementaryServiceCode)
        sb.Append("|</Val></Par><Par><Nom>EV_cadena_servicio</Nom><Val>|")
        sb.Append(CommercialServiceCode)
        sb.Append("|</Val></Par><Par><Nom>EV_usuario</Nom><Val>")
        sb.Append(UserName)
        sb.Append("</Val></Par></Parametros></Peticion>")

        Dim data As String = sb.ToString

        'Test URL http://201.131.4.180:8080
        'Production URL http://201.131.4.180:8081

        Dim url As System.Uri
        If EnvironmentContext.Current.Environment = Environments.Production Then
            url = New Uri("http://201.131.4.180:8081")
        Else
            url = New Uri("http://201.131.4.180:8080")
        End If
            Dim request As HttpWebRequest = WebRequest.Create(url)
            request.Method = WebRequestMethods.Http.Post
            request.ContentLength = data.Length
            request.ContentType = "text/xml"
            Dim writer As New StreamWriter(request.GetRequestStream)
            writer.Write(data)
            writer.Close()
            Dim oResponse As HttpWebResponse = request.GetResponse()
            Dim reader As New StreamReader(oResponse.GetResponseStream())
            Dim resultXML As String = reader.ReadToEnd()
            oResponse.Close()
            Return resultXML
            'Return data
    End Function

#End Region


End Class

Imports System.Text.RegularExpressions
Imports System.Xml
Public Class GetCertInfoWEPPTMX
    Inherits BusinessObjectBase


#Region "Constructors"

    Public Sub New(ByVal ds As GetCertInfoWEPPTMXDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Constants"

    Public Const DATA_COL_NAME_CERT_NUM As String = "cert_number"
    Public Const DATA_COL_NAME_PHONE_NUM As String = "phone_number"
    Public Const DATA_COL_NAME_SERIAL_NUM As String = "serial_number"
    Public Const DATA_COL_NAME_DEALER As String = "dealer_code"
    
    Private Const TABLE_NAME As String = "GetCertInfoWEPPTMX"
    Private Const DATASET_NAME As String = "GetCertInfoWEPPTMX"
    Private Const DATASET_TABLE_NAME As String = "Certificate"

#End Region

#Region "Private Members"
    
    Private Sub MapDataSet(ByVal ds As GetCertInfoWEPPTMXDs)

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

    Private Sub Load(ByVal ds As GetCertInfoWEPPTMXDs)
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
            Throw New ElitaPlusException("GetCertInfoWEPPTMX Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetCertInfoWEPPTMXDs)
        Try
            If ds.GetCertInfoWEPPTMX.Count = 0 Then Exit Sub

            With ds.GetCertInfoWEPPTMX.Item(0)
                'todo - Initialize the incoming search criteria
                DealerCode = .dealer_code.Trim.ToUpper
                PhoneNum = .phone_number.Trim
                SerialNum = .serial_number.Trim
                CertNum = .cert_number.Trim.ToUpper
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetCertInfoWEPPTMX Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Private Function IsSearchCriteriaValid() As Boolean
        If CertNum = String.Empty AndAlso PhoneNum = String.Empty AndAlso SerialNum = String.Empty Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Properties"

    '<ValueMandatory("")> _
    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

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

    Public Property PhoneNum As String
        Get
            If Row(DATA_COL_NAME_PHONE_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_PHONE_NUM), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_PHONE_NUM, Value)
        End Set
    End Property

    Public Property SerialNum As String
        Get
            If Row(DATA_COL_NAME_SERIAL_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_SERIAL_NUM), String)
            End If
        End Get
        Set
            SetValue(DATA_COL_NAME_SERIAL_NUM, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Dim objDoc As New Xml.XmlDocument
        Dim objRoot As Xml.XmlElement
        Dim objE As XmlElement
        Dim retXml As String
        Dim objDecl As XmlDeclaration = objDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)
        Try
            If Not IsSearchCriteriaValid() Then
                objRoot = objDoc.CreateElement("GetCertInfoWEPPTMXResult")
                objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
                objDoc.AppendChild(objRoot)

                objE = objDoc.CreateElement("query_result")
                objRoot.AppendChild(objE)
                objE.InnerText = "-1"

                objE = objDoc.CreateElement("error_message")
                objRoot.AppendChild(objE)
                objE.InnerText = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_MISSING_SEARCH_CRITERION)
            Else
                Validate()
                Dim _CertDataSet As DataSet = Certificate.GetCertInfoWEPPTMX(DealerCode, CertNum, PhoneNum, SerialNum)
                If _CertDataSet.Tables(0).Rows.Count > 0 Then 'certificate found
                    Dim dr As DataRow() = _CertDataSet.Tables(0).Select("cert_Status='C'")
                    Dim I As Integer
                    If dr.Length > 0 Then 'no active, only cancelled certificate found                        
                        objRoot = objDoc.CreateElement("GetCertInfoWEPPTMXResult")
                        objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
                        objDoc.AppendChild(objRoot)

                        objE = objDoc.CreateElement("query_result")
                        objRoot.AppendChild(objE)
                        objE.InnerText = "-1"

                        objE = objDoc.CreateElement("error_message")
                        objRoot.AppendChild(objE)
                        objE.InnerText = "El contrato se encuentra expirado. Para cualquier duda o aclaración por favor comuníques con la aseguradora."
                    Else
                        _CertDataSet.DataSetName = DATASET_NAME & "Result"
                        _CertDataSet.Tables(0).TableName = DATASET_TABLE_NAME


                        retXml = XMLHelper.FromDatasetToXML(_CertDataSet, Nothing, True, True, True, False, True)
                        objDoc.LoadXml(retXml)
                        objRoot = objDoc.SelectSingleNode("/GetCertInfoWEPPTMXResult")

                        objE = objDoc.CreateElement("query_result")
                        objRoot.AppendChild(objE)
                        objE.InnerText = "0"

                        objE = objDoc.CreateElement("error_message")
                        objRoot.AppendChild(objE)
                    End If
                Else 'certificate not found
                    objRoot = objDoc.CreateElement("GetCertInfoWEPPTMXResult")
                    objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
                    objDoc.AppendChild(objRoot)

                    objE = objDoc.CreateElement("query_result")
                    objRoot.AppendChild(objE)
                    objE.InnerText = "-1"

                    objE = objDoc.CreateElement("error_message")
                    objRoot.AppendChild(objE)
                    objE.InnerText = "El número de contrato/número de serie/número de teléfono no existe, por favor corrobore el número  de certificado/número de serie/número de telefono y vuelva a intentarlo. Para cualquier duda o aclaración por favor comuníquese  con la aseguradora."
                End If
            End If

            retXml = objDoc.OuterXml

            Return retXml
            'Catch ex As BOValidationException
            '    Throw ex
            'Catch ex As StoredProcedureGeneratedException
            '    Throw ex
            'Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            '    Throw ex
        Catch ex As Exception
            'Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            objRoot = objDoc.CreateElement("GetCertInfoWEPPTMXResult")
            objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
            objDoc.AppendChild(objRoot)

            objE = objDoc.CreateElement("query_result")
            objRoot.AppendChild(objE)
            objE.InnerText = "-1"

            objE = objDoc.CreateElement("error_message")
            objRoot.AppendChild(objE)
            objE.InnerText = ex.Message
            Return objDoc.OuterXml
        End Try

    End Function

#End Region

End Class

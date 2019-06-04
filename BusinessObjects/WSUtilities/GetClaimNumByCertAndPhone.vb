Imports System.Text.RegularExpressions
Imports System.Xml
Public Class GetClaimNumByCertAndPhone
    Inherits BusinessObjectBase
#Region "Constants"

    Public Const DATA_COL_NAME_CERT_NUM As String = "cert_number"
    Public Const DATA_COL_NAME_PHONE_NUM As String = "phone_number"
    
    Private Const TABLE_NAME As String = "GetClaimNumByCertAndPhone"
    Private Const DATASET_NAME As String = "GetClaimNumByCertAndPhoneDs"

#End Region
#Region "Constructors"

    Public Sub New(ByVal ds As GetClaimNumByCertAndPhoneDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region
#Region "Private Members"
    Private Sub MapDataSet(ByVal ds As GetClaimNumByCertAndPhoneDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    Private Sub Load(ByVal ds As GetClaimNumByCertAndPhoneDs)
        Try
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetClaimNumByCertAndPhone Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetClaimNumByCertAndPhoneDs)
        Try
            If ds.GetClaimNumByCertAndPhone.Count = 0 Then Exit Sub

            With ds.GetClaimNumByCertAndPhone.Item(0)
                'todo - Initialize the incoming search criteria
                PhoneNum = .phone_number.Trim
                CertNum = .cert_number.Trim.ToUpper
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetClaimNumByCertAndPhone Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Private Function IsSearchCriteriaValid() As Boolean
        If CertNum = String.Empty OrElse PhoneNum = String.Empty Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Properties"

    Public Property CertNum() As String
        Get
            If Row(Me.DATA_COL_NAME_CERT_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CERT_NUM), String)
            End If
        End Get
        Set(ByVal Value As String)
            Me.SetValue(Me.DATA_COL_NAME_CERT_NUM, Value)
        End Set
    End Property

    Public Property PhoneNum() As String
        Get
            If Row(Me.DATA_COL_NAME_PHONE_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_PHONE_NUM), String)
            End If
        End Get
        Set(ByVal Value As String)
            Me.SetValue(Me.DATA_COL_NAME_PHONE_NUM, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Function ProcessWSRequest() As String
        Dim objDoc As New Xml.XmlDocument
        Dim objRoot As Xml.XmlElement
        Dim objE As XmlElement
        Dim retXml As String
        Dim strErrMsg As String
        Dim strClaimNum As String

        Try
            Dim objDecl As XmlDeclaration = objDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)
            objRoot = objDoc.CreateElement("GetClaimNumByCertAndPhoneResult")
            objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
            objDoc.AppendChild(objRoot)

            strErrMsg = String.Empty
            strClaimNum = String.Empty
            If Not IsSearchCriteriaValid() Then
                strErrMsg = TranslationBase.TranslateLabelOrMessage("ERR_INVALID_SEARCH_CRITERA")
            Else
                Me.Validate()
                Dim _CertDataSet As DataSet = Certificate.GetCertsWithActiveClaimByCertNumAndPhone(Me.CertNum, Me.PhoneNum)
                If _CertDataSet.Tables(0).Rows.Count = 1 Then
                    If IsDBNull(_CertDataSet.Tables(0).Rows(0)("claim_number")) Then
                        strErrMsg = TranslationBase.TranslateLabelOrMessage("CLAIM_NOT_FOUND")
                    Else
                        strClaimNum = _CertDataSet.Tables(0).Rows(0)("claim_number")
                        If strClaimNum Is Nothing OrElse strClaimNum.Trim = String.Empty Then
                            strErrMsg = TranslationBase.TranslateLabelOrMessage("CLAIM_NOT_FOUND")
                        Else
                            strClaimNum = strClaimNum.Trim
                        End If
                    End If
                    
                ElseIf _CertDataSet.Tables(0).Rows.Count = 0 Then 'no cert found or multiple found
                    strErrMsg = TranslationBase.TranslateLabelOrMessage("ERR_CERTIFICATE_NOT_FOUND")
                Else 'more than one certificates found
                    strErrMsg = TranslationBase.TranslateLabelOrMessage("DUPLICATE_CERTIFICATE")
                End If
            End If

            objE = objDoc.CreateElement("claim_number")
            objRoot.AppendChild(objE)
            objE.InnerText = strClaimNum

            If strErrMsg <> String.Empty Then
                objE = objDoc.CreateElement("error_message")
                objRoot.AppendChild(objE)
                objE.InnerText = strErrMsg
            End If

            retXml = objDoc.OuterXml

            Return retXml
        Catch ex As Exception
            Dim objDecl As XmlDeclaration = objDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)
            objRoot = objDoc.CreateElement("GetClaimNumByCertAndPhoneResult")
            objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
            objDoc.AppendChild(objRoot)

            objE = objDoc.CreateElement("claim_number")
            objRoot.AppendChild(objE)
            objE.InnerText = ""

            objE = objDoc.CreateElement("error_message")
            objRoot.AppendChild(objE)
            objE.InnerText = ex.Message
            Return objDoc.OuterXml
        End Try

    End Function
#End Region
End Class

Imports System.Text.RegularExpressions

Public Class OlitaSerialNumberUpdate
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER As String = "dealer_code"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const DATA_COL_NAME_PRODUCT_SERIAL_NUMBER As String = "serial_number"

    Public Const DATA_COL_NAME_CERT_ID As String = "cert_id"
    Public Const DATA_COL_NAME_CERT_ITEM_ID As String = "cert_item_id"

    Private Const TABLE_NAME As String = "OlitaSerialNumberUpdate"
    Private Const TABLE_NAME_PRODUCT_SERIAL_NUMBER As String = "product_serial_numbers"

    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

    'Error msgs
    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"
    Private Const COUNTRY_NOT_FOUND As String = "ERR_COUNTRY_NOT_FOUND"
    Private Const UPDATE_FAILED As String = "ERR_UPDATE_FAILED"
#End Region


#Region "Constructors"

    Public Sub New(ByVal ds As OlitaSerialNumberUpdateDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _countryId As Guid = Guid.Empty
    Private _regionId As Guid = Guid.Empty


    Private Sub MapDataSet(ByVal ds As OlitaSerialNumberUpdateDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New Dataset
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As OlitaSerialNumberUpdateDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Olita Update Serial Number Saving Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As OlitaSerialNumberUpdateDs)
        Try
            If ds.OlitaSerialNumberUpdate.Count = 0 Then Exit Sub
            With ds.OlitaSerialNumberUpdate.Item(0)
                Me.DealerCode = .dealer_code
                Me.CertNumber = .cert_number
                Me.SerialNumber = .serial_number
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Olita Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub
#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_DEALER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CertNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SerialNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_PRODUCT_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_PRODUCT_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_PRODUCT_SERIAL_NUMBER, Value)
        End Set
    End Property

#End Region


#Region "Public Members"


    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            'find dealer
            Dim dvDealrs As DataView = LookupListNew.GetDealerLookupList(compIds)
            Dim dealerId As Guid = Guid.Empty
            If Not dvDealrs Is Nothing AndAlso dvDealrs.Count > 0 Then
                dealerId = LookupListNew.GetIdFromCode(dvDealrs, Me.DealerCode)
                If dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("OlitaSerialNumberUpdate Error: ", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_CODE)
                End If
            End If

            If Dealer.GetOlitaSearchType(compIds, Me.DealerCode).Equals(Codes.OLITA_SEARCH_GENERIC) Then
                Me.CertNumber += "*"
            End If
            Dim _CertListDataSet As DataSet = Certificate.GetCertificatesList(Me.CertNumber, "", "", "", "", Me.DealerCode).Table.DataSet
            If Not _CertListDataSet Is Nothing AndAlso _CertListDataSet.Tables.Count > 0 AndAlso _CertListDataSet.Tables(0).Rows.Count > 0 Then
                If _CertListDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID) Is DBNull.Value Then
                    Throw New BOValidationException("OlitaSerialNumberUpdate Error: ", Me.CERTIFICATE_NOT_FOUND)
                Else
                    Try
                        If _CertListDataSet.Tables.Count > 1 Then
                            Throw New BOValidationException("OlitaSerialNumberUpdate Error: ", Me.CERTIFICATE_NOT_FOUND)
                        End If
                        Dim cert_id As New Guid(CType(_CertListDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte()))
                        Me.BuildCertItemAndSave(cert_id)
                    Catch ex As Exception
                        Throw New BOValidationException("OlitaSerialNumberUpdate Error: ", Me.UPDATE_FAILED)
                    End Try

                    ' Set the acknoledge OK response
                    Return XMLHelper.GetXML_OK_Response


                End If

            ElseIf _CertListDataSet Is Nothing Then
                Throw New BOValidationException("OlitaSerialNumberUpdate Error: ", Me.ERROR_ACCESSING_DATABASE)
            ElseIf Not _CertListDataSet Is Nothing AndAlso _CertListDataSet.Tables.Count > 0 AndAlso _CertListDataSet.Tables(0).Rows.Count = 0 Then
                Throw New BOValidationException("OlitaSerialNumberUpdate Error: ", Me.CERTIFICATE_NOT_FOUND)
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Private Sub BuildCertItemAndSave(ByVal certID As Guid)
        'Get the cert_item
        Dim certItemDV As CertItem.CertItemSearchDV = CertItem.GetItems(certID)
        Dim certItemBO As CertItem = New CertItem(New Guid(CType(certItemDV.Table.Rows(0).Item(Me.DATA_COL_NAME_CERT_ITEM_ID), Byte())))
        certItemBO.SerialNumber = Me.SerialNumber
        certItemBO.Save()
    End Sub

#End Region


End Class

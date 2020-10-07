Imports System.Text.RegularExpressions

Public Class SaveSerialNumberByCertNum
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const DATA_COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Private Const TABLE_NAME As String = "SaveSerialNumberByCertNum"

    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"


#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As SaveSerialNumberByCertNumDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty
    Private _serviceNetworkID As Guid = Guid.Empty


    Private Sub MapDataSet(ByVal ds As SaveSerialNumberByCertNumDs)

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

    Private Sub Load(ByVal ds As SaveSerialNumberByCertNumDs)
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
            Throw New ElitaPlusException("SaveSerialNumberByCertNum Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As SaveSerialNumberByCertNumDs)
        Try
            If ds.SaveSerialNumberByCertNum.Count = 0 Then Exit Sub
            With ds.SaveSerialNumberByCertNum.Item(0)
                CertNumber = .CERT_NUMBER
                DealerCode = .DEALER_CODE
                SerialNumber = .SERIAL_NUMBER
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("SaveSerialNumberByCertNum Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property CertNumber As String
        Get
            If Row(DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_CERT_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_DEALER_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    Public Property SerialNumber As String
        Get
            If Row(DATA_COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_SERIAL_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            Dim objSerialNumber As New SerialNumber
            objSerialNumber.DealerId = DealerId
            objSerialNumber.SerialNumber = SerialNumber
            objSerialNumber.CertNumber = CertNumber

            objSerialNumber.Validate()

            objSerialNumber.Save()

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseUniqueKeyConstraintViolationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"

    Private ReadOnly Property DealerId As Guid
        Get
            If _dealerId.Equals(Guid.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("SaveSerialNumberByCertNum Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                _dealerId = LookupListNew.GetIdFromCode(list, DealerCode)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("SaveSerialNumberByCertNum Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            Return _dealerId
        End Get
    End Property


#End Region

End Class

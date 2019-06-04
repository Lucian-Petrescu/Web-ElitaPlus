
Public Class UpdateSubscriberStatus
    Inherits BusinessObjectBase
#Region "Constants"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "DEALER_CODE"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "CERT_NUMBER"
    Public Const DATA_COL_NAME_SUBSCRIBER_STATUS As String = "SUBSCRIBER_STATUS"
    Public Const DATA_COL_NAME_STATUS_CHANGE_DATE As String = "STATUS_CHANGE_DATE"

    Private Const TABLE_NAME As String = "UpdateSubscriberStatus"
    Private Const TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE As String = "UpdateSubscriberStatusResponse"
    Private Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"
    Private Const DATASET_NAME__CERT_CHECK_RESPONSE As String = "CertificateCheckResponse"

    Private _dealerId As Guid = Guid.Empty
    Private _certId As Guid = Guid.Empty

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As UpdateSubscriberStatusDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CertificateNumber() As String
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
    Public Property SubscriberStatus As String
        Get
            If Row(Me.DATA_COL_NAME_SUBSCRIBER_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_SUBSCRIBER_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SUBSCRIBER_STATUS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property StatusChangeDate() As DateType
        Get
            If Row(Me.DATA_COL_NAME_STATUS_CHANGE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_STATUS_CHANGE_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_STATUS_CHANGE_DATE, Value)
        End Set
    End Property

#End Region

#Region "Private Members"
    Private Sub MapDataSet(ByVal ds As UpdateSubscriberStatusDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
    Private Sub Load(ByVal ds As UpdateSubscriberStatusDs)
        Try
            Initialize()
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
            Throw New ElitaPlusException("UpdateSubscriberStatus Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As UpdateSubscriberStatusDs)
        Try
            If ds.UpdateSubscriberStatus.Count = 0 Then Exit Sub
            With ds.UpdateSubscriberStatus.Item(0)
                Me.DealerCode = .DEALER_CODE
                Me.CertificateNumber = .CERT_NUMBER
                Me.SubscriberStatus = .SUBSCRIBER_STATUS
                Me.StatusChangeDate = .STATUS_CHANGE_DATE
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdateSubscriberStatus Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub
    Private Function FindDealer()

        Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

        Me._dealerId = LookupListNew.GetIdFromCode(list, Me.DealerCode)

        If _dealerId = Guid.Empty Then
            Throw New BOValidationException("UpdateSubscriberStatus Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
        End If


    End Function

    Private Function FindCertificate()

        Dim dal As New CertificateDAL
        Dim dsCert As DataSet = dal.GetCertIDWithCertNumAndDealer(Me.CertificateNumber, Me._dealerId)
        Dim strResult As String = String.Empty

        If Not dsCert Is Nothing AndAlso dsCert.Tables.Count > 0 AndAlso dsCert.Tables(0).Rows.Count = 1 Then
            If dsCert.Tables(0).Rows(0).Item("Cert_ID") Is DBNull.Value Then
                Throw New BOValidationException("UpdateSubscriberStatus Error: ", Common.ErrorCodes.ERR_NO_CERTIFICATE_FOUND)
            Else
                _certId = New Guid(CType(dsCert.Tables(0).Rows(0).Item("Cert_ID"), Byte()))
                If _certId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("UpdateSubscriberStatus Error: ", Common.ErrorCodes.ERR_NO_CERTIFICATE_FOUND)
                End If
                'check whether the certificate is active
                If (dsCert.Tables(0).Rows(0)("status_code") Is String.Empty OrElse dsCert.Tables(0).Rows(0)("status_code") <> "A") Then
                    Throw New BOValidationException("UpdateSubscriberStatus Error: ", Common.ErrorCodes.MSG_INVALID_CERTIFICATE_STATUS)
                End If
            End If
        Else
            Throw New BOValidationException("UpdateSubscriberStatus Error: ", Common.ErrorCodes.ERR_NO_CERTIFICATE_FOUND)
        End If


    End Function

    Private Function validateStatusForCertAndSubscriber(Type As String)

        If (Me.SubscriberStatus <> "A" AndAlso Me.SubscriberStatus <> "S") Then
            Throw New BOValidationException("UpdateSubscriberStatus Error: ", Common.ErrorCodes.MSG_INVALID_SUBSCRIBER_STATUS)
        End If

    End Function

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Dim dal As New CertificateDAL

            Me.Validate()

            'validate the dealer code
            If Not Me.DealerCode Is Nothing AndAlso DealerCode.Trim <> String.Empty Then
                Me.FindDealer()
            End If

            'validate the certificate number
            If Not Me.CertificateNumber Is Nothing AndAlso CertificateNumber.Trim <> String.Empty Then
                Me.FindCertificate()
            End If

            'verify the incoming Subscriber status
            validateStatusForCertAndSubscriber("Subscriber")

            'Get the Certificate information
            Dim MyBO As Certificate = New Certificate(Me._certId)
            MyBO.SubscriberStatus = LookupListNew.GetIdFromCode("SUBSTAT", Me.SubscriberStatus)
            MyBO.SubStatusChangeDate = Me.StatusChangeDate
            'validate the document type flag
            MyBO.ValFlag = MyBO.GetValFlag()

            'Save the data
            MyBO.Save()

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

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

#End Region

#Region "Extended Properties"
#End Region
End Class

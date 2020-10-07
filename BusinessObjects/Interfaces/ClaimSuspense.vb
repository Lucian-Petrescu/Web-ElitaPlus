Public Class ClaimSuspense
    Inherits BusinessObjectBase

#Region "Constants"

    Public Const COL_NAME_CLAIM_RECON_WRK_ID As String = "claim_recon_wrk_id"
    Public Const COL_NAME_CLAIMFILE_PROCESSED_ID As String = "claimfile_processed_id"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_INTERFACE_CODE As String = "interface_code"
    Public Const COL_NAME_CLAIM_ACTION As String = "claim_action"
    Public Const COL_NAME_PROCESS_ORDER As String = "process_order"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CLAIM_LOADED As String = "claim_loaded"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_CERTIFICATE_SALES_DATE As String = "certificate_sales_date"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_AUTHORIZATION_CREATION_DATE As String = "authorization_creation_date"
    Public Const COL_NAME_AUTHORIZATION_CODE As String = "authorization_code"
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = "problem_description"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_ADDITIONAL_PRODUCT_CODE As String = "additional_product_code"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_DO_NOT_PROCESS As String = "do_not_process"
    Public Const COL_NAME_DATE_CLAIM_CLOSED As String = "date_claim_closed"
    Public Const COL_NAME_STATUS_CODE As String = "status_code"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_REPLACEMENT_DATE As String = "replacement_date"
    Public Const COL_NAME_KEY_FIELD As String = "key_field"


#End Region


#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        VerifyConcurrency(sModifiedDate)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New ClaimSuspenseDAL
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
            Dim dal As New ClaimSuspenseDAL
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
            If Row(ClaimSuspenseDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimSuspenseDAL.COL_NAME_CLAIM_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimfileProcessedId As Guid
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_CLAIMFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimSuspenseDAL.COL_NAME_CLAIMFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_CLAIMFILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property RejectReason As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property ClaimLoaded As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_CLAIM_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_CLAIM_LOADED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_CLAIM_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property DealerCode As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_DEALER_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Certificate As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property



    Public Property CertificateSalesDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_CERTIFICATE_SALES_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimSuspenseDAL.COL_NAME_CERTIFICATE_SALES_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_CERTIFICATE_SALES_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property



    Public Property AuthorizationCreationDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CREATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CREATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CREATION_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property AuthorizationCode As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=6)> _
    Public Property ProductCode As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=6)> _
    Public Property AdditionalProductCode As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_ADDITIONAL_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_ADDITIONAL_PRODUCT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_ADDITIONAL_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property SerialNumber As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property ServiceCenterCode As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property



    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimSuspenseDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property DoNotProcess As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_DO_NOT_PROCESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_DO_NOT_PROCESS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_DO_NOT_PROCESS, Value)
        End Set
    End Property



    Public Property DateClaimClosed As DateType
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_DATE_CLAIM_CLOSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimSuspenseDAL.COL_NAME_DATE_CLAIM_CLOSED), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_DATE_CLAIM_CLOSED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property StatusCode As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_STATUS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_STATUS_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimSuspenseDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property ReplacementDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimSuspenseDAL.COL_NAME_REPLACEMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimSuspenseDAL.COL_NAME_REPLACEMENT_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSuspenseDAL.COL_NAME_REPLACEMENT_DATE, Value)
        End Set
    End Property

#End Region


#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimSuspenseDAL

                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function Process(ByVal dsXmlFile As Dataset) As Integer

        Dim DAL As New ClaimSuspenseDAL
        Dim dsNew As New Dataset("CLAIM_SUSPENSE")
        Dim dr() As DataRow

        'Newd to send all rows since we need to update the Database values even on the DO_NOT_PROCESS rows
        'dr = dsXmlFile.Tables(0).Select(DAL.COL_NAME_DO_NOT_PROCESS + " = 'Y'")
        'For Each dRow As DataRow In dr
        '    dsXmlFile.Tables(0).Rows.Remove(dRow)
        'Next

        Try
            For Each ds As DataSet In MyBase.SplitDatasetForXML(dsXmlFile, ClaimReconWrkDAL.COL_NAME_CERTIFICATE)
                DAL.Process(ds, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            Next
        Catch ex As Exception
            Throw New Exception(Common.ErrorCodes.BO_INVALID_OPERATION, ex)
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal certificateNumber As String, ByVal authorizationNumber As String, ByVal filename As String) As Dataset
        Try
            Dim dal As New ClaimSuspenseDAL
            Dim ds As Dataset

            If filename.Trim.Length > 0 Then
                ds = dal.LoadList(certificateNumber, authorizationNumber, filename, ElitaPlusIdentity.Current.ActiveUser.Id, 0)
            Else
                ds = dal.LoadList(certificateNumber, authorizationNumber, filename, ElitaPlusIdentity.Current.ActiveUser.Id)
            End If

            'ds.Tables(0).Constraints.Add("PK", ds.Tables(0).Columns(ClaimSuspense.COL_NAME_CLAIM_RECON_WRK_ID), True)
            ds.Tables(0).Columns(ClaimSuspense.COL_NAME_KEY_FIELD).Unique = True
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns(ClaimSuspense.COL_NAME_KEY_FIELD)}
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


#End Region


End Class

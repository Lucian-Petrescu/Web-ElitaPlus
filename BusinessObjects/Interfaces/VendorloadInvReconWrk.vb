'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/22/2017)  ********************

Public Class VendorloadInvReconWrk
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub
    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        VerifyConcurrency(sModifiedDate)
    End Sub

    Protected Sub Load()
        Try

            If Dataset.Tables.IndexOf(VendorloadInvReconWrkDal.TableNameDal) < 0 Then
                Dim dal As New VendorloadInvReconWrkDal
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(VendorloadInvReconWrkDal.TableNameDal).NewRow
            Dataset.Tables(VendorloadInvReconWrkDal.TableNameDal).Rows.Add(newRow)
            Row = newRow
            SetValue(VendorloadInvReconWrkDal.TableKeyName, Guid.NewGuid)
            Initialize()
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try

            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(VendorloadInvReconWrkDal.TableNameDal).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(VendorloadInvReconWrkDal.TableNameDal) >= 0 Then
                Row = FindRow(id, VendorloadInvReconWrkDal.TableKeyName, Dataset.Tables(VendorloadInvReconWrkDal.TableNameDal))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New VendorloadInvReconWrkDal
                dal.Load(Dataset, id)
                Row = FindRow(id, VendorloadInvReconWrkDal.TableKeyName, Dataset.Tables(VendorloadInvReconWrkDal.TableNameDal))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(VendorloadInvReconWrkDal.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorloadInvReconWrkDal.ColNameVendorloadInvReconWrkId), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property FileProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameFileProcessedId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorloadInvReconWrkDal.ColNameFileProcessedId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameFileProcessedId, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=8)>
    Public Property RecordType() As String
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameRecordType) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorloadInvReconWrkDal.ColNameRecordType), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameRecordType, value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property RejectCode() As String
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameRejectCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorloadInvReconWrkDal.ColNameRejectCode), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameRejectCode, value)
        End Set
    End Property


    <ValidStringLength("", Max:=240)>
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameRejectReason) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorloadInvReconWrkDal.ColNameRejectReason), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameRejectReason, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)>
    Public Property RecordLoaded() As String
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameRecordLoaded) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorloadInvReconWrkDal.ColNameRecordLoaded), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameRecordLoaded, value)
        End Set
    End Property



    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameServiceCenterId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorloadInvReconWrkDal.ColNameServiceCenterId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameServiceCenterId, value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)>
    Public Property VendorSku() As String
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameVendorSku) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorloadInvReconWrkDal.ColNameVendorSku), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameVendorSku, value)
        End Set
    End Property



    Public Property InventoryQuantity() As LongType
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameInventoryQuantity) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VendorloadInvReconWrkDal.ColNameInventoryQuantity), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameInventoryQuantity, value)
        End Set
    End Property



    Public Property PriceListDetailId() As Guid
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNamePriceListDetailId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorloadInvReconWrkDal.ColNamePriceListDetailId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNamePriceListDetailId, value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)>
    Public Property EntireRecord() As String
        Get
            CheckDeleted()
            If Row(VendorloadInvReconWrkDal.ColNameEntireRecord) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorloadInvReconWrkDal.ColNameEntireRecord), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(VendorloadInvReconWrkDal.ColNameEntireRecord, value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VendorloadInvReconWrkDal
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Shared Function ValidateFile(ByVal fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New VendorloadInvReconWrkDal
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.ValidateFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ProcessFile(ByVal fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New VendorloadInvReconWrkDal
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_PROCESS)
            dal.ProcessFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function DeleteFile(ByVal fileProcessedId As Guid) As Guid
        Dim interfaceStatusId As Guid
        Dim dal As New VendorloadInvReconWrkDal
        Try
            interfaceStatusId = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_DELETE)
            dal.DeleteFile(fileProcessedId, interfaceStatusId)
            Return interfaceStatusId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal fileProcessedId As Guid, ByVal recMode As Integer,
                             ByVal recordType As String, ByVal rejectCode As String, ByVal rejectReason As String,
                             ByVal pageindex As Integer, ByVal pagesize As Integer, ByVal sortExpression As String) As DataView
        Try
            Dim dal As New VendorloadInvReconWrkDal
            Dim ds As DataSet

            ds = dal.LoadList(fileProcessedId, Authentication.LangId, recMode, recordType, rejectCode, rejectReason, pageindex, pagesize, sortExpression)
            Return (ds.Tables(VendorloadInvReconWrkDal.TableNameDal).DefaultView)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

End Class



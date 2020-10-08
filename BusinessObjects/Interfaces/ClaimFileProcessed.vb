'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/12/2004)  ********************

Imports System.Collections.Generic

Public Class ClaimFileProcessed
    Inherits BusinessObjectBase
    Implements IFileLoadHeaderWork
#Region "Constants"

    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_CLAIMFILE_PROCESSED_ID As String = "claimfile_processed_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_BYPASSED As String = "bypassed"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_PROCESSED_AMOUNT As String = "processed_amount"
    Public Const COL_NAME_FILE_TYPE_CODE As String = "file_type_code"

    Private Const DEALERLOADFORM_FORM001 As String = "DEALERLOADFORM_FORM001" ' The filename does not exists or it is empty
    Private Const DEALERLOADFORM_FORM002 As String = "DEALERLOADFORM_FORM002" ' Dealer Contract Not Found

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimFileProcessedDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ClaimFileProcessedDAL
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
    Public ReadOnly Property Id As Guid Implements IFileLoadHeaderWork.Id
        Get
            If Row(ClaimFileProcessedDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimFileProcessedDAL.COL_NAME_CLAIMFILE_PROCESSED_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
  Public Property SplitSystemId As Guid
        Get
            CheckDeleted()
            If row(ClaimfileProcessedDAL.COL_NAME_SPLIT_SYSTEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimfileProcessedDAL.COL_NAME_SPLIT_SYSTEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimfileProcessedDAL.COL_NAME_SPLIT_SYSTEM_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property Filename As String Implements IFileLoadHeaderWork.FileName
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFileProcessedDAL.COL_NAME_FILENAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_FILENAME, Value)
        End Set
    End Property

    Public Property Received As LongType Implements IFileLoadHeaderWork.Received
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimFileProcessedDAL.COL_NAME_RECEIVED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_RECEIVED, Value)
        End Set
    End Property

    Public Property Bypassed As LongType Implements IFileLoadHeaderWork.Bypassed
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_BYPASSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimFileProcessedDAL.COL_NAME_BYPASSED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_BYPASSED, Value)
        End Set
    End Property


    Public Property Counted As LongType Implements IFileLoadHeaderWork.Counted
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_COUNTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimFileProcessedDAL.COL_NAME_COUNTED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_COUNTED, Value)
        End Set
    End Property



    Public Property Rejected As LongType Implements IFileLoadHeaderWork.Rejected
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_REJECTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimFileProcessedDAL.COL_NAME_REJECTED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_REJECTED, Value)
        End Set
    End Property



    Public Property Validated As LongType Implements IFileLoadHeaderWork.Validated
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_VALIDATED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimFileProcessedDAL.COL_NAME_VALIDATED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_VALIDATED, Value)
        End Set
    End Property



    Public Property Loaded As LongType Implements IFileLoadHeaderWork.Loaded
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimFileProcessedDAL.COL_NAME_LOADED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_LOADED, Value)
        End Set
    End Property

    Public Property ProcessedAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimFileProcessedDAL.COL_NAME_PROCESSED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimFileProcessedDAL.COL_NAME_PROCESSED_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimFileProcessedDAL.COL_NAME_PROCESSED_AMOUNT, Value)
        End Set
    End Property

    Public ReadOnly Property FamilyDataSet As DataSet Implements IFileLoadHeaderWork.FamilyDataSet
        Get
            Return Dataset
        End Get
    End Property

#Region "Properties External BOs"

    'Public ReadOnly Property DealerCode() As String
    '   Get
    '        If DealerId.Equals(Guid.Empty) Then Return Nothing
    '        Return LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerId)
    '    End Get
    'End Property

    'Public ReadOnly Property DealerNameLoad() As String
    '    Get
    '        If DealerId.Equals(Guid.Empty) Then Return Nothing
    'Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_DEALERS)
    '       Return LookupListNew.GetDescriptionFromId(dv, DealerId)
    '    End Get
    'End Property

    Public ReadOnly Property ClaimNameLoad As String
        Get
            If SplitSystemId.Equals(Guid.Empty) Then Return Nothing
            Dim dv As DataView = LookupListNew.DataView(LookupListNew.LK_SPLIT_SYSTEM)
            Return LookupListNew.GetDescriptionFromId(dv, SplitSystemId)
        End Get
    End Property

#End Region

#End Region

#Region "Public Members"
    Public Overrides Sub Save() Implements IFileLoadHeaderWork.Save
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimFileProcessedDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(oData As Object) As DataView
        Try
            Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
            Dim dal As New ClaimFileProcessedDAL
            Dim ds As DataSet
            Dim isInRole As Boolean = (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_VIEW) OrElse _
                ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__VIEW_ONLY))
            If isInRole = True Then
                oClaimFileProcessedData.isInRole = "Y"
            Else
                oClaimFileProcessedData.isInRole = "N"
            End If
            ds = dal.LoadList(oClaimFileProcessedData)
            Return (ds.Tables(ClaimFileProcessedDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadListForADealer(oData As Object, dealerCode As String) As DataView
        Try
            Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
            Dim dal As New ClaimFileProcessedDAL
            Dim ds As DataSet
            Dim isInRole As Boolean = (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_VIEW) OrElse _
                ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__VIEW_ONLY))
            If isInRole = True Then
                oClaimFileProcessedData.isInRole = "Y"
            Else
                oClaimFileProcessedData.isInRole = "N"
            End If
            ds = dal.LoadListForADealer(oClaimFileProcessedData, dealerCode)
            Return (ds.Tables(ClaimFileProcessedDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Structure DealerInfo
        Public dealerID As Guid
        Public layout As String
        Public dealerCode As String
        Public dealerName As String
    End Structure

    Public Shared Function GetDealerLayout(dealerID As Guid, _
    oInterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode) As DealerInfo
        Dim retDealerInfo As DealerInfo
        Dim sLayout As String
        Dim oContract As Contract
        Dim oDealer As Dealer

        With retDealerInfo
            .dealerID = Guid.Empty
            .layout = ""
            .dealerCode = ""
            .dealerName = ""
        End With

        oContract = Contract.GetCurrentContract(dealerID)
        If oContract Is Nothing Then
            oContract = Contract.GetMaxExpirationContract(dealerID)
            If oContract Is Nothing Then
                Dim errors() As ValidationError = {New ValidationError(DEALERLOADFORM_FORM002, GetType(ClaimFileProcessed), Nothing, Nothing, Nothing)}
                Throw New BOValidationException(errors, GetType(ClaimFileProcessed).FullName)
            End If
        End If
        oDealer = New Dealer(oContract.DealerId)
        sLayout = oContract.Layout
        'If oInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.PAYM Then
        sLayout = sLayout.Remove(sLayout.Length - 1, 1) & "p"
        'End If
        With retDealerInfo
            .dealerID = dealerID
            .layout = sLayout
            .dealerCode = oDealer.Dealer
            .dealerName = oDealer.DealerName
        End With

        Return retDealerInfo

    End Function


#End Region

#Region "StoreProcedures Control"

    Public Shared Sub ValidateFile(oData As Object)
        Try
            Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
            Dim dal As New ClaimFileProcessedDAL

            oClaimFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.ValidateFile(oClaimFileProcessedData)


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub ProcessFileRecords(oData As Object)
        Try
            Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
            Dim dal As New ClaimFileProcessedDAL

            oClaimFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.ProcessFileRecords(oClaimFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub DeleteFile(oData As Object)
        Try
            Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
            Dim dal As New ClaimFileProcessedDAL

            oClaimFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.DeleteFile(oClaimFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "Validation"
    Public Shared Function IsFileProcessed(oData As Object) As Boolean
        Try
            Dim oClaimFileProcessedData As ClaimFileProcessedData = CType(oData, ClaimFileProcessedData)
            Dim dal As New ClaimFileProcessedDAL

            oClaimFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)
            dal.ValidateFile(oClaimFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Sub ValidateFileName(fileLength As Integer)
        If fileLength = 0 Then
            Dim errors() As ValidationError = {New ValidationError(DEALERLOADFORM_FORM001, GetType(ClaimFileProcessed), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(ClaimFileProcessed).FullName)
        End If

    End Sub

#End Region

#Region "Invoice Item Recon Work"
    Public ReadOnly Property InvoiceReconWrkChildren As InvoiceReconWrkList
        Get
            Return New InvoiceReconWrkList(Me)
        End Get
    End Property

    Public ReadOnly Property Children As IEnumerable(Of IFileLoadReconWork) Implements IFileLoadHeaderWork.Children
        Get
            Return New InvoiceReconWrkList(Me)
        End Get
    End Property

#End Region

End Class
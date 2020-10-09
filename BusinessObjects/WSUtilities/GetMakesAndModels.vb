Imports System.Text.RegularExpressions

Public Class GetMakesAndModels
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "DealerCode"
    Private Const TABLE_NAME As String = "GetMakesAndModels"
    Private Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"
    Private Const DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE As String = "GetMakesAndModelResponse"
#End Region

#Region "Constructors"

    Public Sub New(ds As GetMakesAndModelsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    Private _dealerId As Guid = Guid.Empty
    Private Sub MapDataSet(ds As GetMakesAndModelsDs)

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

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ds As GetMakesAndModelsDs)
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
            Throw New ElitaPlusException("WSUtilities GetMakesAndModels Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetMakesAndModelsDs)
        Try
            If ds.GetMakesAndModels.Count = 0 Then Exit Sub
            With ds.GetMakesAndModels.Item(0)
                DealerCode = .DealerCode
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("WSUtilities Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

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

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String


        Try
            Validate()

            If DealerCode IsNot Nothing AndAlso DealerCode.Trim <> String.Empty Then
                Dim strErrorFindingDealer As String = FindDealer
                If strErrorFindingDealer <> String.Empty Then
                    Return strErrorFindingDealer
                End If
            End If

            Dim oDealer As New Dealer(_dealerId)
            Dim makesList As New DataSet(DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE)
            Dim ResponseStatus As DataTable
            If (oDealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, "Y")) Then
                If oDealer.EquipmentListCode Is Nothing OrElse oDealer.EquipmentListCode.Equals(String.Empty) Then
                    ResponseStatus = BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.EQUIPMENT_LIST_CODE_NULL), _
                                                                               Common.ErrorCodes.EQUIPMENT_LIST_CODE_NULL, _
                                                                               Codes.WEB_EXPERIENCE__VALIDATION_ERROR)
                    Dim _errorDataSet As New DataSet(DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE)
                    _errorDataSet.Tables.Add(ResponseStatus)
                    Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))
                End If
                makesList = Equipment.LoadEquipmentListForWS(oDealer.EquipmentListCode, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            ElseIf (oDealer.UseWarrantyMasterID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, "Y")) Then
                makesList = Manufacturer.GetMakesForWSByWarrantyMaster(_dealerId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Else
                makesList = Manufacturer.GetMakesForWS(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            End If

            If makesList.Tables(0) Is Nothing OrElse makesList.Tables(0).Rows.Count = 0 Then
                makesList.Tables.RemoveAt(0)
            End If

            ResponseStatus = BuildWSResponseStatus(Nothing, Nothing, Codes.WEB_EXPERIENCE__NO_ERROR)
            makesList.Tables.Add(ResponseStatus)

            Return XMLHelper.FromDatasetToXML(makesList, Nothing, True, True, True, False, True)

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

    Private Function FindDealer() As String
        If _dealerId.Equals(Guid.Empty) AndAlso (DealerCode IsNot Nothing AndAlso DealerCode.Trim <> String.Empty) Then
            Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            If list Is Nothing Then
                Dim ResponseStatus As DataTable = BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE), _
                                                                           Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE, _
                                                                           Codes.WEB_EXPERIENCE__FATAL_ERROR)
                Dim _errorDataSet As New DataSet(DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE)
                _errorDataSet.Tables.Add(ResponseStatus)
                Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))
            End If
            _dealerId = LookupListNew.GetIdFromCode(list, DealerCode)
            If _dealerId.Equals(Guid.Empty) Then
                Dim ResponseStatus As DataTable = BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_DEALER_NOT_FOUND), _
                                                           Common.ErrorCodes.WS_DEALER_NOT_FOUND, _
                                                           Codes.WEB_EXPERIENCE__LOOKUP_ERROR)
                Dim _errorDataSet As New DataSet(DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE)
                _errorDataSet.Tables.Add(ResponseStatus)
                Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))
            End If
            list = Nothing

        End If
        Return String.Empty
    End Function
#End Region

End Class

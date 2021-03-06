﻿Imports System.Text.RegularExpressions

Public Class GetCustomerFunctions
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CUSTOMER_IDENTIFIER As String = "CustomerIdentifier"
    Public Const DATA_COL_NAME_IDENTIFIER_TYPE As String = "IdentifierType"
    Public Const DATA_COL_NAME_DEALER As String = "CarrierID"

    Private Const TABLE_NAME As String = "GetCustomerFunctions"
    Private Const TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE As String = "GetCustomerFunctionsResponse"
    Private Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"
    Private Const DATASET_NAME__CLAIM_CHECK_RESPONSE As String = "ClaimCheckResponse"
    
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetCustomerFunctionsDS)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetCustomerFunctionsDS)

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

    Private Sub Load(ByVal ds As GetCustomerFunctionsDS)
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
            Throw New ElitaPlusException("GetCustomerFunctions Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetCustomerFunctionsDS)
        Try
            If ds.GetCustomerFunctions.Count = 0 Then Exit Sub
            With ds.GetCustomerFunctions.Item(0)
                If Not .IsCarrierIDNull Then Me.DealerCode = .CarrierID
                Me.CustomerIdentifier = .CustomerIdentifier
                Me.IdentifierType = .IdentifierType

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetCustomerFunctions Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Function IsDealerWebEnabled() As Boolean
        Dim objDealer As New Dealer(Me._dealerId)
        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.DealerSupportWebClaimsId) = "Y" Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function BuildWSCustomerFunctions(ByVal claimCount As Integer) As DataTable

        Dim CustomerFunctionsTable As New DataTable(TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE)

        CustomerFunctionsTable.Columns.Add("CustomerFunction", GetType(String))
        CustomerFunctionsTable.Columns.Add("FunctionURL", GetType(String))
        CustomerFunctionsTable.Columns.Add("IsFunctionAvailable", GetType(Boolean))

        Dim newRowFC As DataRow = CustomerFunctionsTable.NewRow()

        newRowFC("CustomerFunction") = "FileClaim"
        newRowFC("FunctionURL") = DBNull.Value
        newRowFC("IsFunctionAvailable") = True
        CustomerFunctionsTable.Rows.Add(newRowFC)

        Dim newRowCS As DataRow = CustomerFunctionsTable.NewRow()

        newRowCS("CustomerFunction") = "ClaimStatus"
        newRowCS("FunctionURL") = DBNull.Value
        If claimCount > 0 Then
            newRowCS("IsFunctionAvailable") = True
        Else
            newRowCS("IsFunctionAvailable") = False
        End If

        CustomerFunctionsTable.Rows.Add(newRowCS)

        Return CustomerFunctionsTable

    End Function

    Private Function FindDealer() As String
        If Me._dealerId.Equals(Guid.Empty) AndAlso (Not Me.DealerCode Is Nothing AndAlso Me.DealerCode.Trim <> String.Empty) Then

            Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            If list Is Nothing Then
                Dim ResponseStatus As DataTable = Me.BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE), _
                                                                           Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE, _
                                                                           Codes.WEB_EXPERIENCE__FATAL_ERROR)
                Dim _errorDataSet As New DataSet(Me.DATASET_NAME__CLAIM_CHECK_RESPONSE)
                _errorDataSet.Tables.Add(ResponseStatus)
                Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))

                ' Throw New BOValidationException("GetCustomerFunctions Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
            End If
            Me._dealerId = LookupListNew.GetIdFromCode(list, Me.DealerCode)
            If _dealerId.Equals(Guid.Empty) Then
                Dim ResponseStatus As DataTable = Me.BuildWSResponseStatus(TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.WS_DEALER_NOT_FOUND), _
                                                           Common.ErrorCodes.WS_DEALER_NOT_FOUND, _
                                                           Codes.WEB_EXPERIENCE__LOOKUP_ERROR)
                Dim _errorDataSet As New DataSet(Me.DATASET_NAME__CLAIM_CHECK_RESPONSE)
                _errorDataSet.Tables.Add(ResponseStatus)
                Return (XMLHelper.FromDatasetToXML(_errorDataSet, Nothing, True, True, True, False, True))

                'Throw New BOValidationException("GetCustomerFunctions Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
            End If
            list = Nothing
        End If
        Return String.Empty
    End Function
#End Region

#Region "Properties"

    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_DEALER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CustomerIdentifier() As String
        Get
            If Row(Me.DATA_COL_NAME_CUSTOMER_IDENTIFIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CUSTOMER_IDENTIFIER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CUSTOMER_IDENTIFIER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IdentifierType() As String
        Get
            If Row(Me.DATA_COL_NAME_IDENTIFIER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_IDENTIFIER_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_IDENTIFIER_TYPE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()
            'Not IdentifierType Is Nothing AndAlso IdentifierType.Trim <> String.Empty
            If Not Me.DealerCode Is Nothing AndAlso DealerCode.Trim <> String.Empty Then
                Dim strErrorFindingDealer As String = Me.FindDealer
                If strErrorFindingDealer <> String.Empty Then
                    Return strErrorFindingDealer
                End If
            End If

            Dim _customerFunctionsDataSet As DataSet = Certificate.WS_GetCustomerFunctions(Me.CustomerIdentifier, Me.IdentifierType, Me._dealerId)

            Return (XMLHelper.FromDatasetToXML(_customerFunctionsDataSet, Nothing, True, True, True, False, True))

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

Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class FileProcessedData
    Public Enum FileTypeCode
        BestReplacement
        Equipment
        VendorInv
    End Enum

    Public CountryId As Guid
    Public ReferenceId As Guid
    Public CompanyGroupId As Guid
    Public CompanyId As Guid
    Public DealerId As Guid
    Public FileType As FileTypeCode
End Class

Public Class FileProcessedDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_FILE_PROCESSED"
    Public Const TABLE_KEY_NAME As String = COL_NAME_FILE_PROCESSED_ID
    Public Const COL_NAME_FILE_PROCESSED_ID As String = "file_processed_id"
    Public Const COL_NAME_FILE_TYPE As String = "file_type"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_FILE_NAME As String = "file_name"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_BYPASSED As String = "bypassed"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_RETURN As String = "return"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_STATUS_DESC As String = "status_desc"
    Public Const TOTAL_PARAM = 1
    Public Const TABLE_NAMES = 1


#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("file_processed_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal oFileProcessedData As FileProcessedData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters(9) As DBHelperParameter

        With oFileProcessedData
            If .CompanyGroupId.Equals(Guid.Empty) Then
                parameters(0) = New DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, DBNull.Value)
                parameters(1) = New DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, DBNull.Value)
            Else
                parameters(0) = New DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, .CompanyGroupId)
                parameters(1) = New DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, .CompanyGroupId)
            End If
            If .CompanyId.Equals(Guid.Empty) Then
                parameters(2) = New DBHelperParameter(COL_NAME_COMPANY_ID, DBNull.Value)
                parameters(3) = New DBHelperParameter(COL_NAME_COMPANY_ID, DBNull.Value)
            Else
                parameters(2) = New DBHelperParameter(COL_NAME_COMPANY_ID, .CompanyId)
                parameters(3) = New DBHelperParameter(COL_NAME_COMPANY_ID, .CompanyId)
            End If
            If .DealerId.Equals(Guid.Empty) Then
                parameters(4) = New DBHelperParameter(COL_NAME_DEALER_ID, DBNull.Value)
                parameters(5) = New DBHelperParameter(COL_NAME_DEALER_ID, DBNull.Value)
            Else
                parameters(4) = New DBHelperParameter(COL_NAME_DEALER_ID, .DealerId)
                parameters(5) = New DBHelperParameter(COL_NAME_DEALER_ID, .DealerId)
            End If
            If .CountryId.Equals(Guid.Empty) Then
                parameters(6) = New DBHelperParameter(COL_NAME_COUNTRY_ID, DBNull.Value)
                parameters(7) = New DBHelperParameter(COL_NAME_COUNTRY_ID, DBNull.Value)
            Else
                parameters(6) = New DBHelperParameter(COL_NAME_COUNTRY_ID, .CountryId)
                parameters(7) = New DBHelperParameter(COL_NAME_COUNTRY_ID, .CountryId)
            End If
            If .ReferenceId.Equals(Guid.Empty) Then
                parameters(8) = New DBHelperParameter(COL_NAME_REFERENCE_ID, DBNull.Value)
            Else
                parameters(8) = New DBHelperParameter(COL_NAME_REFERENCE_ID, .ReferenceId)
            End If
            parameters(9) = New DBHelperParameter(COL_NAME_FILE_TYPE, FileTypeToSQL(.FileType))
        End With
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Helper"

    Public Shared Function GetFileLayout(ByVal value As FileProcessedData.FileTypeCode) As String
        Dim returnValue As String = Nothing
        Select Case value
            Case FileProcessedData.FileTypeCode.BestReplacement
                returnValue = "eqp_br"
            Case FileProcessedData.FileTypeCode.Equipment
                returnValue = "eqp_e"
        End Select
        Return returnValue
    End Function

    Public Shared Function FileTypeToSQL(ByVal value As FileProcessedData.FileTypeCode) As String
        Dim returnValue As String = Nothing
        Select Case value
            Case FileProcessedData.FileTypeCode.BestReplacement
                returnValue = "BEST REPLACEMENT"
            Case FileProcessedData.FileTypeCode.Equipment
                returnValue = "EQUIPMENT"
            Case FileProcessedData.FileTypeCode.VendorInv
                returnValue = "VENDORINV"
        End Select
        Return returnValue
    End Function

#End Region

#Region "Signatures"

    Public Delegate Sub AsyncCaller(ByVal fileProcessedId As Guid, ByVal interfaceStatusId As Guid, ByVal sqlStmt As String)

#End Region

#Region "Async Calls"
    Private Sub AsyncExecuteSP(ByVal fileProcessedId As Guid, ByVal interfaceStatusId As Guid, ByVal sqlStmt As String)
        Dim inputParameters(1) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        inputParameters(0) = New DBHelperParameter(COL_NAME_FILE_PROCESSED_ID, fileProcessedId.ToByteArray())
        inputParameters(1) = New DBHelperParameter(InterfaceStatusWrkDAL.COL_NAME_INTERFACE_STATUS_ID, interfaceStatusId.ToByteArray())
        outputParameter(0) = New DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(sqlStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
    End Sub

    Public Sub ExecuteSP(ByVal fileProcessedId As Guid, ByVal interfaceStatusId As Guid, ByVal sqlStmt As String)
        Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
        aSyncHandler.BeginInvoke(fileProcessedId, interfaceStatusId, sqlStmt, Nothing, Nothing)
    End Sub
#End Region
End Class
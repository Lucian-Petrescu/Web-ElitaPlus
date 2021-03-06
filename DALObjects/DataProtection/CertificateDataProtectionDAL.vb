'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/3/2005)********************

#Region "CommentData"


#End Region
Public Class CertificateDataProtectionDAL
    Inherits DALBase
#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DATA_PROTECTION_HISTORY"
    Public Const COL_NAME_DATA_PROTECTION_HISTORY_ID As String = "data_protection_history_id"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"
    Public Function LoadList(ByVal certId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DATA_PROTECTION_HISTORY_ID, certId.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Sub SaveForm(ByRef intErrCode As Integer, ByRef strErrMsg As String,
                         ByVal certId As Guid,
                           ByVal restricred As String, ByVal comment As String,
                           ByVal requestId As String)
        Dim sqlStmt As String
        strErrMsg = ""
        intErrCode = 0
        sqlStmt = Me.Config("/SQL/SAVE_NEW_FORM_PROCEDURE")
        Try
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("P_RETURN_CODE", intErrCode.GetType),
                            New DBHelper.DBHelperParameter("P_ErrorMsg", strErrMsg.GetType, 500)}
            Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
            Dim param As DBHelper.DBHelperParameter
            param = New DBHelper.DBHelperParameter("pi_entity_id", certId.ToByteArray)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_comments", comment)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_request_id", requestId)
            inParameters.Add(param)
            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, outParameters)
            If Not outParameters(0).Value Is Nothing Then
                Try
                    intErrCode = CType(outParameters(0).Value, Integer)
                Catch ex As Exception
                    intErrCode = 0
                End Try
            End If
            If Not outParameters(1).Value Is Nothing Then
                strErrMsg = outParameters(1).Value.ToString().Trim
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

End Class



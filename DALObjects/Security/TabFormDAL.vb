Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects.DBHelper

#Region "TabFormData"

Public Class TabFormData
    Public outReturn_code, outNew, outAdded As Integer
    Public outEnglish As String
End Class

#End Region

Public Class TabFormDAL
    Inherits DALBase

#Region "Constants"
    Public Const TAB_TABLE_NAME As String = "ELP_TAB"
    Public Const FORM_TABLE_NAME As String = "ELP_FORM"

    Public Const COL_NAME_RETURN_CODE As String = "out_return"
    Public Const COL_NAME_NEW As String = "out_new"
    Public Const COL_NAME_ADDED As String = "out_added"
    Public Const COL_NAME_ENGLISH As String = "out_english"
    Public Const COL_NAME_USER As String = "p_user"
    
    ' Store Procedure Output Parameters
    Public Const OUT_RETURN_CODE = 0
    Public Const OUT_NEW = 1
    Public Const OUT_ADDED = 2
    Public Const OUT_ENGLISH = 3
    Public Const TOTAL_PARAM = 3 '4

#End Region

#Region "StoreProcedures Control"

    Private Function ExecuteSP(selectStmt As String, userId As String) As TabFormData
        Dim oTabFormData As New TabFormData

        Dim inputParameters() As DBHelperParameter = New DBHelperParameter() { _
                           New DBHelperParameter(COL_NAME_USER, userId)}

        Dim outputParameters(TOTAL_PARAM) As DBHelperParameter

        outputParameters(OUT_RETURN_CODE) = New DBHelperParameter(COL_NAME_RETURN_CODE, GetType(Integer))
        outputParameters(OUT_NEW) = New DBHelperParameter(COL_NAME_NEW, GetType(Integer))
        outputParameters(OUT_ADDED) = New DBHelperParameter(COL_NAME_ADDED, GetType(Integer))
        outputParameters(OUT_ENGLISH) = New DBHelperParameter(COL_NAME_ENGLISH, GetType(String))
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        With oTabFormData
            .outReturn_code = outputParameters(OUT_RETURN_CODE).Value
            .outNew = outputParameters(OUT_NEW).Value
            .outAdded = outputParameters(OUT_ADDED).Value
            .outEnglish = outputParameters(OUT_ENGLISH).Value
        End With
        Return oTabFormData
    End Function

    Public Function LoadTabs(userId As String) As TabFormData
        Dim selectStmt As String = Config("/SQL/LOAD_TABS_PROCEDURE")
        Return ExecuteSP(selectStmt, userId)
    End Function

    Public Function LoadForms(userId As String) As TabFormData
        Dim selectStmt As String = Config("/SQL/LOAD_FORMS_PROCEDURE")
        Return ExecuteSP(selectStmt, userId)
    End Function

#End Region

#Region "Clear Methods"

    Public Sub ClearTabs()
        Dim deleteStmt As String = Config("/SQL/CLEAR_NEW_TABS")
       Execute(deleteStmt, Nothing)
    End Sub

    Public Sub ClearForms()
       Dim deleteStmt As String = Config("/SQL/CLEAR_NEW_FORMS")
        Execute(deleteStmt, Nothing)
    End Sub

   
#End Region

#Region "Load Methods"

    Public Function LoadNewTabList() As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/GET_NEW_TABS")
            Dim ds As New DataSet

            ds = DBHelper.Fetch(selectStmt, TAB_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadNewFormList(languageID As Guid) As DataSet
        Try
            Dim parameters() As DBHelperParameter = New DBHelperParameter() {New DBHelperParameter("language_id", languageID.ToByteArray)}
            Dim selectStmt As String = Config("/SQL/GET_NEW_FORMS")
            Dim ds As New DataSet

            'ds = DBHelper.Fetch(selectStmt, Me.FORM_TABLE_NAME)
            DBHelper.Fetch(ds, selectStmt, FORM_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "New application form maitenance"
    Public Sub SaveNewForm(ByRef intErrCode As Integer, ByRef strErrMsg As String, _
                           strUser As String, New_Form_Id As Guid, _
                           strTab As String, strCode As String, _
                           strEnglish As String, strRelativeURL As String, _
                           strNavAllowed As String, strApproved As String, _
                           strFormCategory As String, _
                           strQueryString As String)
        Dim sqlStmt As String
        strErrMsg = ""
        intErrCode = 0
        sqlStmt = Config("/SQL/SAVE_NEW_FORM_PROCEDURE")
        Try
            Dim outParameters() As DBHelperParameter = New DBHelperParameter() { _
                            New DBHelperParameter("P_RETURN_CODE", intErrCode.GetType), _
                            New DBHelperParameter("P_ErrorMsg", strErrMsg.GetType, 500)}
            Dim inParameters As New List(Of DBHelperParameter)
            Dim param As DBHelperParameter

            param = New DBHelperParameter("P_USER", strUser)
            inParameters.Add(param)

            param = New DBHelperParameter("P_NEW_APPLICATION_FORM_ID", New_Form_Id.ToByteArray)
            inParameters.Add(param)

            param = New DBHelperParameter("P_TAB", strTab)
            inParameters.Add(param)

            param = New DBHelperParameter("P_CODE", strCode)
            inParameters.Add(param)

            param = New DBHelperParameter("P_ENGLISH", strEnglish)
            inParameters.Add(param)

            param = New DBHelperParameter("P_RELATIVE_URL", strRelativeURL)
            inParameters.Add(param)

            param = New DBHelperParameter("P_NAV_ALWAYS_ALLOWED", strNavAllowed)
            inParameters.Add(param)

            If strApproved.Trim <> String.Empty Then
                param = New DBHelperParameter("P_APPROVED", strApproved)
                inParameters.Add(param)
            End If

            If strFormCategory.Trim <> String.Empty Then
                param = New DBHelperParameter("P_FORM_CATEGORY_CODE", strFormCategory)
                inParameters.Add(param)
            End If

            If strQueryString.Trim <> String.Empty Then
                param = New DBHelperParameter("P_QUERY_STRING", strQueryString)
                inParameters.Add(param)
            End If

            ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, outParameters)

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

    Public Sub DeleteNewForm(ByRef intErrCode As Integer, ByRef strErrMsg As String, New_Form_Id As Guid)
        Dim sqlStmt As String
        sqlStmt = Config("/SQL/DELETE_NEW_FORM_PROCEDURE")
        strErrMsg = ""
        intErrCode = 0
        Try
            Dim outParameters() As DBHelperParameter = New DBHelperParameter() { _
                            New DBHelperParameter("P_RETURN_CODE", intErrCode.GetType), _
                            New DBHelperParameter("P_ErrorMsg", strErrMsg.GetType, 500)}
            Dim inParameters() As DBHelperParameter = New DBHelperParameter() { _
                            New DBHelperParameter("P_NEW_APPLICATION_FORM_ID", New_Form_Id.ToByteArray)}
            Dim param As DBHelperParameter

            ExecuteSpParamBindByName(sqlStmt, inParameters, outParameters)

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

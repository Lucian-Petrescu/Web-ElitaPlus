Public Class TabForm
    Inherits BusinessObjectBase

#Region "Variables"

    Private moTabFormData As TabFormData

#End Region

#Region "Properties"

    Public Property ReturnCode As Integer
        Get
            Return moTabFormData.outReturn_code
        End Get
        Set
            moTabFormData.outReturn_code = Value
        End Set
    End Property

    Public Property Added As Integer
        Get
            Return moTabFormData.outAdded
        End Get
        Set
            moTabFormData.outAdded = Value
        End Set
    End Property

    Public Property English As String
        Get
            Return moTabFormData.outEnglish
        End Get
        Set
            moTabFormData.outEnglish = Value
        End Set
    End Property

    Public Property outNew As Integer
        Get
            Return moTabFormData.outNew
        End Get
        Set
            moTabFormData.outNew = Value
        End Set
    End Property

#End Region

#Region "StoreProcedures Control"

    Public Sub LoadTabs(ByVal userId As String)
        Try
            Dim dal As New TabFormDAL

            moTabFormData = dal.LoadTabs(userId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Sub LoadForms(ByVal userId As String)
        Try
            Dim dal As New TabFormDAL

            moTabFormData = dal.LoadForms(userId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "Clear Methods"

    Public Sub ClearTabs()
        Try
            Dim dal As New TabFormDAL

            dal.ClearTabs()

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Sub ClearForms()
        Try
            Dim dal As New TabFormDAL

            dal.ClearForms()

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetNewTabList() As DataView
        Try
            Dim dal As New TabFormDAL
            Return New DataView(dal.LoadNewTabList().Tables(TabFormDAL.TAB_TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewFormList() As DataView
        Try
            Dim dal As New TabFormDAL
            Return New DataView(dal.LoadNewFormList(Authentication.CurrentUser.LanguageId).Tables(TabFormDAL.FORM_TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "New form table maintenance"
    Public Shared Sub SaveNewForm(ByRef intErrCode As Integer, ByRef strErrMsg As String, ByVal New_Form_Id As Guid, _
                           ByVal strTab As String, ByVal strCode As String, _
                           ByVal strEnglish As String, ByVal strRelativeURL As String, _
                           ByVal strNavAllowed As String, ByVal strApproved As String, _
                           ByVal strFormCategory As String, ByVal strQueryString As String)

        Dim dal As New TabFormDAL
        dal.SaveNewForm(intErrCode, strErrMsg, Authentication.CurrentUser.UserName, New_Form_Id, strTab, strCode, _
                        strEnglish, strRelativeURL, strNavAllowed, strApproved, strFormCategory, strQueryString)
    End Sub

    Public Shared Sub DeleteNewForm(ByRef intErrCode As Integer, ByRef strErrMsg As String, ByVal New_Form_Id As Guid)

        Dim dal As New TabFormDAL
        dal.DeleteNewForm(intErrCode, strErrMsg, New_Form_Id)
    End Sub
#End Region
End Class

Imports System.IO


Public Class TabAuthorizationMgr



#Region "ENUMERATIONS"

    Enum enumAlignment
        LEFT
        RIGHT
        MIDDLE
    End Enum

#End Region


    Private moDS As DataSet
    Private Const TAB_SELECTED_LEFT As String = "tab_select_left_b2.gif"
    Private Const TAB_LEFT As String = "tab_left2.gif"
    Private Const TAB_RIGHT As String = "tab_right2.gif"
    Private Const TAB_SELECTED_RIGHT As String = "tab_select_right_b2.gif"
    Private Const TAB_SELECTED_MIDDLE_BACK As String = "tab_select_middle_back_b3.gif"
    Private Const TAB_MIDDLE_BACK As String = "tab_middle_back4.gif"
    Private Const DEFAULT_ICON As String = "tables_icon.gif"



#Region "CONSTRUCTOR"

    Public Sub New()
        'MyBase.DataPortalServerURL = ""
        'MyBase.DataServiceAssemblyName = "Assurant.ElitaPlus.DataServices"
        'MyBase.DataServiceClassName = "Assurant.ElitaPlus.DataServices.GenericPersist"
    End Sub

#End Region


#Region "FUNCTIONS FOR BUILDING TABS"

    Public Function TabIconPath(ByVal code As String, ByVal iconPath As String) As String

        Dim oTabView As New DataView()
        oTabView.Table = moDS.Tables(0)
        oTabView.RowFilter = "CODE='" & code & "'"
        Dim sImageIcon As String = iconPath & oTabView.Item(0).Item("TAB_ICON_IMG").ToString

        Dim prefix As String = sImageIcon.Substring(0, sImageIcon.IndexOf("."))
        Dim suffix As String = sImageIcon.Substring(sImageIcon.IndexOf("."))

        sImageIcon = prefix & "_xp4" & suffix

        If iconPath.IndexOf("/") = -1 Then
            Throw New ArgumentException("The Iconpath is missing the directory separator of '/' in the IconPath Variable")
        End If

        If HttpContext.Current Is Nothing Then
            Return iconPath & DEFAULT_ICON
        Else
            If File.Exists(HttpContext.Current.Server.MapPath(sImageIcon)) Then
                Return sImageIcon
            Else
                Return iconPath & DEFAULT_ICON
            End If
        End If

    End Function


    Public Function TabTitle(ByVal code As String, ByVal languageID As Guid) As String
        Dim oTabView As New DataView()
        Dim sText As String

        oTabView.Table = moDS.Tables(0)

        oTabView.RowFilter = "CODE='" & code & "'"
        If oTabView.Count = 0 Then
            Throw New DataNotFoundException("TabAuthorizationMgr.GetTabTitle")
        End If
        sText = oTabView.Item(0).Item("TRANSLATION").ToString()
        Return sText
    End Function

#End Region


    Public Function TabImage(ByVal code As String, ByVal alignment As Long) As String
        '-------------------------------------
        'Name:GetTabImage
        'Purpose:build the left center and right outlines for the rounded tabs.
        'Input Values:strAlignment - there are 3 sections for each image , one for
        'the left,center and right sections of the rounded tab.
        'Uses:
        '-------------------------------------

        'added for testing. there is no web context
        If HttpContext.Current Is Nothing Then
            Return TAB_MIDDLE_BACK

        End If

        Select Case alignment
            Case enumAlignment.LEFT

                If CType(HttpContext.Current.Session(ELPWebConstants.SELECTED_TAB), String) = code Then
                    Return TAB_SELECTED_LEFT
                Else
                    Return TAB_LEFT
                End If

            Case enumAlignment.RIGHT
                If CType(HttpContext.Current.Session(ELPWebConstants.SELECTED_TAB), String) = code Then
                    Return TAB_SELECTED_RIGHT
                Else
                    Return TAB_RIGHT
                End If

            Case enumAlignment.MIDDLE


                If CType(HttpContext.Current.Session(ELPWebConstants.SELECTED_TAB), String) = code Then
                    Return TAB_SELECTED_MIDDLE_BACK
                Else
                    Return TAB_MIDDLE_BACK
                End If
            Case Else

                Throw New ArgumentOutOfRangeException("Alignment must be either right,middle or left")

        End Select


    End Function





    'Public Function Load() As DataSet
    '    '-------------------------------------
    '    'Name:Load
    '    'Purpose:Execute sql to retrieve a dataset with the tab information
    '    'Input Values:GET_USER_TAB_AUTH- name of the sql code in the sql xml file.
    '    'Uses:
    '    '-------------------------------------
    '    Dim oDB As New DbStruct
    '    Dim oDBFactory As New DBFactory
    '    With oDB
    '        .QueryName = "NAVIGATION/GET_USER_TAB_AUTH"
    '        .CommandType = DbStruct.enumStatementType.ParameterizedStatement
    '        .SQLSource = DbStruct.enumSQLSource.GetFromSQLFile
    '    End With

    '    Dim oParam1 As IDbDataParameter = oDBFactory.CurrentFactory.GetParameter

    '    With oParam1
    '        .ParameterName = "NETWORK_ID1"
    '        .Value = ElitaPlusIdentity.Current.ActiveUser.NetworkId

    '    End With

    '    Dim oParam2 As IDbDataParameter = oDBFactory.CurrentFactory.GetParameter

    '    With oParam2
    '        .ParameterName = "NETWORK_ID2"
    '        .Value = ElitaPlusIdentity.Current.ActiveUser.NetworkId
    '    End With

    '    Dim oParam3 As IDbDataParameter = oDBFactory.CurrentFactory.GetParameter

    '    With oParam3
    '        .ParameterName = "LANGUAGE_ID"
    '        .Value = ElitaPlusIdentity.Current.ActiveUser.LanguageId.ToByteArray
    '    End With


    '    oDB.Params.Add(oParam1)
    '    oDB.Params.Add(oParam2)
    '    oDB.Params.Add(oParam3)


    '    moDS = DirectCast(MyBase.Execute(DirectCast(oDB, Object)), DataSet)
    '    If moDS.Tables(0).Rows.Count = 0 Then
    '        Throw New DataNotFoundException("TabAuthorizationMgr.Load")
    '    End If

    '    Return moDS

    'End Function

    Public Function Load() As DataSet

        'moDS = FormAuthorization.GetTabAuthorization
        moDS = FormAuthorization.GetMenus
        Return moDS
    End Function

End Class

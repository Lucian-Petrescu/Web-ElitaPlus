Imports System.Web.Caching
Imports Assurant.ElitaPlus.BusinessObjects
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.BusinessObjectData.Common
Imports Assurant.ElitaPlus.BusinessObjectData


'Public Class ApplicationMessages
'    Inherits Assurant.Common.Framework.BusinessObject.ServicedBase



'#Region "CONSTRUCTOR"

'    Public Sub New()
'        MyBase.DataPortalServerURL = ""
'        MyBase.DataServiceAssemblyName = "Assurant.ElitaPlus.DataServices"
'        MyBase.DataServiceClassName = "Assurant.ElitaPlus.DataServices.GenericPersist"
'    End Sub

'#End Region


'-------------------------------------
'Name:LoadItemIntoCache
'Purpose:Load the messages into the cache with a sliding expiration set by the config file.
'-------------------------------------
'Private Sub LoadItemIntoCache(ByVal sUniqueKey As String, ByVal sTranslation As String)
'    '  HttpContext.Current.Cache.Add(sUniqueKey, sTranslation, Nothing, Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(ELPWebConstants.CACHE_REFRESH_RATE), Caching.CacheItemPriority.Low, Nothing)
'    HttpContext.Current.Cache.Add(sUniqueKey, sTranslation, Nothing, Caching.Cache.NoAbsoluteExpiration, _
'        TimeSpan.FromMinutes(AppConfig.CommonCacheEntryMaxAgeInMin()), Caching.CacheItemPriority.Low, Nothing)

'End Sub


'-------------------------------------
'Name:GetApplicationMessage
'Purpose:Retrieve a message based on the UI_PROG_CODE. if the instance doesn't yet exist, load it from the database.
'Input Values:
'Uses:
'-------------------------------------
'Public Shared Function GetApplicationMessage(ByVal sUI_Prog_Code As String, ByVal nLanguageID As Guid, _
'                            Optional ByVal oErrorController As ErrorController = Nothing) As String
'    Dim sTranslation As String
'    Dim sUniqueKey As String = sUI_Prog_Code & nLanguageID.ToString
'    Dim oApplicationMessages As ApplicationMessages

'    'this is added only for testing.
'    If HttpContext.Current Is Nothing Then

'        oApplicationMessages = New ApplicationMessages
'        sTranslation = oApplicationMessages.GetMessage(sUI_Prog_Code, nLanguageID, oErrorController)
'        Return sTranslation
'    End If
'    If HttpContext.Current.Cache.Item(sUniqueKey) Is Nothing Then

'        oApplicationMessages = New ApplicationMessages
'        sTranslation = oApplicationMessages.GetMessage(sUI_Prog_Code, nLanguageID, oErrorController)
'        oApplicationMessages.LoadItemIntoCache(sUniqueKey, sTranslation)

'        Return sTranslation
'    Else
'        Return HttpContext.Current.Cache.Item(sUniqueKey).ToString
'    End If


'End Function


'-------------------------------------
'Name:GetMessage
'Purpose:Load a single message into the cache from the database.
'Input Values:sUI_Prog_Code, and nLanguageID
'-------------------------------------
'Private Function GetMessage(ByVal sUI_Prog_Code As String, ByVal nLanguageID As Guid, _
'                            Optional ByVal oErrorController As ErrorController = Nothing) As String
'    Dim oDB_Struct As New DbStruct
'    Dim oDBFactory As New DBFactory
'    Dim odsLocal As DataSet

'    Dim objParam1 As IDataParameter = oDBFactory.CurrentFactory.GetParameter
'    objParam1.ParameterName = "UI_PROG_CODE"
'    objParam1.Value = sUI_Prog_Code.ToUpper

'    Dim objParam2 As IDataParameter = oDBFactory.CurrentFactory.GetParameter
'    objParam2.ParameterName = "LANGUAGE_ID"
'    objParam2.Value = nLanguageID.ToByteArray

'    With oDB_Struct
'        .Params.Add(objParam1)
'        .Params.Add(objParam2)
'        .QueryName = "MESSAGES/GET_MESSAGE"
'        .CommandType = DbStruct.enumStatementType.ParameterizedStatement
'        .SQLSource = DbStruct.enumSQLSource.GetFromSQLFile
'    End With

'    odsLocal = CType(MyBase.Execute(CType(oDB_Struct, Object)), DataSet)

'    If odsLocal.Tables(0).Rows.Count > 0 Then
'        Return odsLocal.Tables(0).Rows(0).Item("TRANSLATION").ToString
'    Else
'        If Not oErrorController Is Nothing Then
'            oErrorController.AddError(sUI_Prog_Code & ": is not Translated")
'            oErrorController.Show()
'        End If

'        Return sUI_Prog_Code
'    End If

'End Function


'End Class

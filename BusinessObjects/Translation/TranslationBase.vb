Public Class TranslationBase

#Region "Variables"

    Protected mTranslationMissingList As New ArrayList

#End Region

#Region "Properties"

    Public Property TranslationMissingList As ArrayList
        Get

            Return mTranslationMissingList

        End Get

        Set

            mTranslationMissingList = Value

        End Set

    End Property

    'Public ReadOnly Property MissingTranslationsCount() As Integer
    '    Get

    '        'Dim oTranslationProcess As TranslationProcess = GetTranslationProcessReference()
    '        'Return oTranslationProcess.TranslationMissingCount

    '    End Get

    'End Property
#End Region

#Region "DATA ACCESS ROUTINES"


    Public Shared Function GetTranslationsFromCache(ByRef TranslationList As String, LanguageID As Guid) As DataTable

        Try

            If Web.HttpContext.Current Is Nothing _
                    OrElse Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageID.ToString)) Is Nothing Then
                Return Nothing
            Else
                Dim ds As DataSet = CType(Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageID.ToString)), DataSet)
                Dim dr() As DataRow

                dr = ds.Tables(0).Select(String.Format("{1} in ({0})", TranslationList, LabelDAL.COL_NAME_UI_PROG_CODE))
                If dr.Count > 0 Then
                    Dim dt As New DataTable
                    For Each drRow As DataRow In dr
                        If dt.Columns.Count = 0 Then
                            dt = drRow.Table.Clone
                        End If
                        dt.ImportRow(drRow)
                        TranslationList = TranslationList.Replace(String.Format("'{0}',", drRow(LabelDAL.COL_NAME_UI_PROG_CODE).ToString), "").Replace(String.Format("'{0}'", drRow("UI_PROG_CODE").ToString), "")
                    Next

                    Return dt
                End If

                Return Nothing

            End If

        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Shared Sub PutTranslationsIntoCache(ds As DataSet, LanguageId As Guid)

        Try

            If Web.HttpContext.Current IsNot Nothing Then
                If Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageId.ToString)) Is Nothing Then
                    Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageId.ToString)) = ds
                Else
                    Dim dsCache As DataSet = CType(Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageId.ToString)), DataSet)
                    dsCache.Tables(0).Merge(ds.Tables(0))
                    Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageId.ToString)) = dsCache
                End If
            End If

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Shared Sub UpdateTranslationInCache(ds As DataSet, Optional ByVal LanguageId As String = "")

        Try

            If Web.HttpContext.Current IsNot Nothing Then

                'If no languageId is passed, then we need to look in the dataset for languages
                If LanguageId.Trim.Equals(String.Empty) Then
                    For Each dr As DataRow In ds.Tables(DictItemTranslationDAL.TABLE_NAME).Rows
                        UpdateTranslationInCache(ds, GuidControl.ByteArrayToGuid(CType(dr(DictItemTranslationDAL.COL_NAME_LANGUAGE_ID), Byte())).ToString)
                    Next
                End If

                If Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageId)) Is Nothing Then
                    Exit Sub
                Else
                    Dim dsCache As DataSet = CType(Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageId)), DataSet)
                    Dim boolDirty As Boolean = False

                    For Each dr As DataRow In ds.Tables(DictItemTranslationDAL.TABLE_NAME).Rows

                        For Each drCache As DataRow In dsCache.Tables(0).Rows
                            If GuidControl.ByteArrayToGuid(CType(drCache.Item(DictItemTranslationDAL.COL_NAME_DICT_ITEM_ID), Byte())).ToString.Equals(GuidControl.ByteArrayToGuid(CType(dr.Item(DictItemTranslationDAL.COL_NAME_DICT_ITEM_ID), Byte())).ToString) Then
                                dsCache.Tables(0).Rows.Remove(drCache)
                                boolDirty = True
                                Exit For
                            End If
                        Next
                    Next

                    If boolDirty Then
                        Web.HttpContext.Current.Cache(String.Format("TRANSLATION_{0}", LanguageId)) = dsCache
                    End If

                End If

            End If

        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

    Public Shared Function GetTranslations(InClause As String, oLanguageID As Guid) As DataSet

        Dim oDs As DataSet
        Dim dt As DataTable

        Try

            'Strip InClause of empty translations
            InClause = InClause.Replace("'',", "").Replace("' ',", "").Replace("''", "").Replace("' ',", "").Trim(")").Trim
            dt = GetTranslationsFromCache(InClause, oLanguageID)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                oDs = New DataSet
                oDs.Tables.Add(dt)
            End If

            If InClause.Trim.Trim(",").Trim.Length > 0 Then
                Dim dal As New TranslationDAL
                If oDs Is Nothing Then
                    oDs = dal.GetTranslations(String.Format("{0})", InClause.Trim.Trim(",").Trim), oLanguageID)
                    PutTranslationsIntoCache(oDs, oLanguageID)
                Else
                    Dim dsTemp As DataSet
                    dsTemp = dal.GetTranslations(String.Format("{0})", InClause.Trim.Trim(",").Trim), oLanguageID)
                    PutTranslationsIntoCache(dsTemp, oLanguageID)
                    oDs.Tables(0).Merge(dsTemp.Tables(0))
                End If
            End If

            Return oDs
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function Get_EnglishLanguageID() As Guid
        Dim oDs As DataSet
        Dim oEnglishId As Guid

        Try
            Dim dal As New TranslationDAL

            oDs = dal.Get_EnglishLanguageID()
            oEnglishId = New Guid(DirectCast(oDs.Tables(TranslationDAL.TABLE_NAME).Rows(0).Item(0), Byte()))
            Return oEnglishId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Translate List"

    '-------------------------------------
    'Name:GetTranslation
    'Purpose:Filters the dataview using find method to return a single value according to the keyword passed in.
    'Input Values:
    'Uses:
    '-------------------------------------
    Protected Function Translate(sUIProgCode As String, oView As DataView) As String

        Dim oCurrentMissingTranslations As ArrayList

        If sUIProgCode.Trim = String.Empty Then

            'exit the function by returning the blank string.
            Return String.Empty

        End If

        'Filter the view by sending in the prog code (with any trailing colons stripped off)
        oView.RowFilter = "UI_PROG_CODE = '" & sUIProgCode.TrimEnd(":") & "' OR UI_PROG_CODE = '" & sUIProgCode & "'"

        If oView.Count = 0 Then

            'add the missing translation item to the missing list.
            TranslationMissingList.Add(sUIProgCode)
            Return sUIProgCode

        Else

            'Return the translation.  Reappend the colon if it was present
            Return oView.Item(0).Row("TRANSLATION").ToString.TrimEnd(":") + IIf(sUIProgCode.EndsWith(":"), ":", "")

        End If

    End Function

    Private Function BuildStringFromTranslationArray(aryList As TranslationItemArray, Optional ByVal sCharToJoin As String = ", ") As String

        Dim sItem As String
        Dim oWorkingArray(aryList.CurrentCount - 1) As String
        Dim nIndex As Integer = 0
        Dim sResult As String = String.Empty

        Dim oTranslationItem As TranslationItem
        Dim oDictionaryEntry As DictionaryEntry

        For Each oTranslationItem In aryList.Items

            oWorkingArray(nIndex) = "'" & oTranslationItem.TextToTranslate & "'"
            nIndex += 1

        Next

        sResult = String.Join(sCharToJoin, oWorkingArray)
        Return sResult

    End Function

    Protected Shared Function BuildStringFromArray(aryList As ArrayList, Optional ByVal sCharToJoin As String = ", ") As String

        Dim sItem As String
        Dim oWorkingArray(aryList.Count - 1) As String
        Dim nIndex As Integer = 0
        Dim sResult As String = String.Empty

        For Each sItem In aryList
            oWorkingArray(nIndex) = "'" & sItem.Replace("'", " ").Trim(":") & "'"
            nIndex += 1
        Next

        sResult = String.Join(sCharToJoin, oWorkingArray)
        Return sResult
    End Function

    Public Function TranslateList(aryItemsToTranslate As TranslationItemArray, CurrentLanguageID As Guid) As ArrayList


        Dim oDS As DataSet
        Dim oItem As TranslationItem
        Dim oView As New DataView
        Dim sResultString As String


        'build the in clause of the sql statement from the values in the grid header.
        sResultString = BuildStringFromTranslationArray(aryItemsToTranslate) & ")"

        'Load the translation data from the database.
        oDS = GetTranslations(sResultString, CurrentLanguageID)

        'set the view to use for the translation.
        If oDS IsNot Nothing AndAlso oDS.Tables.Count > 0 Then
            oView.Table = oDS.Tables(0)
        End If

        For Each oItem In aryItemsToTranslate.Items
            oItem.Translation = Translate(oItem.TextToTranslate, oView)
        Next

        '  Return Me.GetCurrentMissingTranslations
        Return TranslationMissingList()

    End Function

    Public Function TranslateList(aryItemsToTranslate As ArrayList) As ArrayList

        '  Dim oDB As New DbStruct
        Dim oDS As DataSet
        ' Dim oControl As Control
        ' Dim oStringBuilder As New StringBuilder
        Dim sCurrentText As String = String.Empty
        Dim oItem As TranslationItem
        Dim oView As New DataView
        Dim aryList As New ArrayList

        Dim sResultString As String
        Dim oLanguageID As Guid

        For Each oItem In aryItemsToTranslate

            If oItem.TextToTranslate.ToString.Trim <> String.Empty Then
                aryList.Add(oItem.TextToTranslate.ToString.Trim.ToUpper)
            End If
        Next

        'build the in clause of the sql statement from the values in the grid header.
        sResultString = BuildStringFromArray(aryList) & ")"


        'Load the translation data from the database.
        oDS = GetTranslations(sResultString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        'set the view to use for the translation.
        oView.Table = oDS.Tables(0)

        For Each oItem In aryItemsToTranslate

            oItem.Translation = Translate(oItem.TextToTranslate, oView)
        Next

        '  Return Me.GetCurrentMissingTranslations
        Return TranslationMissingList

    End Function

#End Region

#Region "Shared Translate Messages"

    Public Shared Function TranslateLabelOrMessage(UIProgCode As String) As String
        Return TranslateLabelOrMessage(UIProgCode, Authentication.LangId)
    End Function

    Public Shared Function TranslateLabelOrMessage(UIProgCode As String, LangId As Guid) As String
        ' Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
        Dim TransProcObj As New TranslationBase
        Dim oTranslationItem As New TranslationItem
        Dim Coll As New TranslationItemArray
        With oTranslationItem
            .TextToTranslate = UIProgCode.ToUpper
        End With
        Coll.Add(oTranslationItem)
        TransProcObj.TranslateList(Coll, LangId)
        ' TransProcObj = TranslateList(Coll, LangId)
        Return oTranslationItem.Translation
    End Function

    Public Shared Function TranslateLabelOrMessageList(UIProgCodes() As String) As String()
        Return TranslateLabelOrMessageList(UIProgCodes, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
    End Function

    Public Shared Function TranslateLabelOrMessageList(UIProgCodes() As String, LangId As Guid) As String()
        '  Dim TransProcObj As TranslationProcess = GetTranslationProcessReference()
        Dim TransProcObj As New TranslationBase
        Dim Coll As New TranslationItemArray
        Dim Code As String
        For Each Code In UIProgCodes
            Dim oTranslationItem As New TranslationItem
            oTranslationItem.TextToTranslate = Code.ToUpper
            Coll.Add(oTranslationItem)
        Next
        TransProcObj.TranslateList(Coll, LangId)
        ' TransProcObj = TranslateList(Coll, LangId)
        Dim Result(Coll.Count - 1) As String
        Dim i As Integer = 0
        Dim item As TranslationItem
        For Each item In Coll
            Result(i) = item.Translation
            i += 1
        Next
        Return Result
    End Function

    Public Shared Sub TranslateMessageList(aryItemsToTranslate As ArrayList)
        Dim TransProcObj As New TranslationBase

        TransProcObj.TranslateList(aryItemsToTranslate)
        
    End Sub

    Public Shared Function TranslateParameterizedMsg(strMSGText As String, intParamCnt As Integer, strParamList As String, Optional ByVal strParamListSeperator As Char = "¦") As String
        Dim strMsgTranslated As String = "", strPattern As String, blnReadyForParam As Boolean = True
        If strMSGText <> "" AndAlso strParamList <> "" AndAlso intParamCnt > 0 Then
            strMsgTranslated = strMSGText
            'First verify that message text has all the placeholders for parameters
            For i As Integer = 0 To intParamCnt - 1
                strPattern = "\{" & i.ToString & "\}"
                If Not Text.RegularExpressions.Regex.Match(strMsgTranslated, strPattern).Success Then
                    blnReadyForParam = False
                    strMsgTranslated = String.Empty
                    Exit For
                End If
            Next
            'second, format the parameters and merge with the message text
            If blnReadyForParam Then
                If intParamCnt = 1 Then
                    If strParamList <> "" Then
                        strMsgTranslated = String.Format(strMsgTranslated, GetFormatedParameterString(strParamList))
                    Else
                        strMsgTranslated = String.Empty
                    End If
                Else 'multiple parameters
                    Dim paramLst() As String
                    paramLst = strParamList.Split(strParamListSeperator)
                    If intParamCnt = paramLst.Count Then
                        For i As Integer = 0 To paramLst.Count - 1
                            paramLst(i) = GetFormatedParameterString(paramLst(i).Trim)
                        Next
                        strMsgTranslated = String.Format(strMsgTranslated, paramLst)
                    Else
                        strMsgTranslated = String.Empty
                    End If
                End If
            End If
        End If
        Return strMsgTranslated
    End Function

    Public Shared Function GetFormatedParameterString(strParam As String) As String
        Dim intParam As Integer, dblParam As Double, dtParam As DateTime, strParamType As String
        Dim strFormatedParam As String = String.Empty
        If strParam.Length > 1 Then
            strParamType = strParam.Substring(0, 1)
            strFormatedParam = strParam.Substring(1)
            Select Case strParamType.ToUpper
                Case "N"
                    If Double.TryParse(strFormatedParam, Globalization.NumberStyles.Number, Globalization.CultureInfo.InvariantCulture, dblParam) Then
                        strFormatedParam = dblParam.ToString("#,##0.00")
                    End If
                Case "I"
                    If Integer.TryParse(strFormatedParam, Globalization.NumberStyles.Integer, Globalization.CultureInfo.InvariantCulture, intParam) Then
                        strFormatedParam = intParam.ToString("#,###")
                    End If
                Case "D"
                    'date1.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                    If strFormatedParam.Length = 8 AndAlso Text.RegularExpressions.Regex.Match(strFormatedParam, "\d{8}").Success Then
                        If Date.TryParse(strFormatedParam.Substring(0, 4) & "-" & strFormatedParam.Substring(4, 2) & "-" & strFormatedParam.Substring(6, 2), Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, dtParam) Then
                            strFormatedParam = dtParam.ToString("dd-MMM-yyyy", Globalization.CultureInfo.CurrentCulture)
                        End If
                    End If
            End Select
            If strFormatedParam = strParam.Substring(1) Then ' No Change it is of type String
                strFormatedParam = strParam
            End If
        End If
        Return strFormatedParam
    End Function

#End Region
End Class

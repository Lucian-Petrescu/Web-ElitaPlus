Imports System
Imports System.Configuration
Imports System.Data
Imports System.Web
Imports System.Threading
Imports System.Text
Imports elp = Assurant.ElitaPlus.Common

Imports Assurant.ElitaPlus.BusinessObjectsNew.Codes

Public NotInheritable Class LookupListNew
    Inherits LookupListCache

#Region " Constants "


    Public Const COL_ID_NAME As String = "ID"
    Public Const COL_DESCRIPTION_NAME As String = "DESCRIPTION"
    Public Const COL_CODE_AND_DESCRIPTION_NAME As String = "CODE_AND_DESCRIPTION"
    Public Const COL_CODE_NAME As String = "CODE"

    Public Const COL_USER_ID_NAME As String = "user_id"
    Public Const COL_DEALER As String = "dealer"
    Public Const COL_COMPANY_ID_NAME As String = "company_id"
    Public Const COL_ACCT_COMPANY_ID_NAME As String = "acct_company_id"
    Public Const COL_LANGUAGE_ID_NAME As String = "language_id"
    Public Const COL_TAB_NAME As String = "tab"
    Public Const COL_LANGUAGE_ID_FULL_NAME As String = "ELP_LANGUAGE.LANGUAGE_ID"
    Public Const COL_COUNTRY_ID_NAME As String = "country_id"
    Public Const COL_COMPANY_GROUP_ID_NAME As String = "company_group_id"
    Public Const COL_COVERAGE_TYPE_ID_NAME As String = "coverage_type_id"
    Public Const COL_DEALER_ID_NAME As String = "dealer_id"
    Public Const COL_PRODUCT_CODE_ID_NAME As String = "product_code_id"
    Public Const COL_ITEM_ID_NAME As String = "item_id"
    Public Const COL_REGION_ID_NAME As String = "region_id"
    Public Const COL_TAX_TYPE_ID_NAME As String = "TAX_TYPE_ID"
    Public Const COL_MANUFACTURER_ID_NAME As String = "manufacturer_id"
    Public Const COL_YEAR_NAME As String = "model_year"
    Public Const COL_MODEL_NAME As String = "model"
    Public Const COL_TRIM_NAME As String = "trim"
    Public Const COL_RISK_GROUP_ID_NAME As String = "risk_group_id"
    Public Const COL_CURRENCY_NOTATION As String = "CURRENCY_NOTATION"

    Public Const COL_TRANSMISSION_DATE As String = "transmission_date"
    Public Const COL_BEGIN_DATE As String = "begin_date"
    Public Const COL_END_DATE As String = "end_date"
    Public Const COL_FILE_NAME As String = "file_name"
    Public Const COL_REPORT_NAME As String = "report_name"
    Public Const COL_REPORTRUNDATE As String = "reportrun_Date"
    Public Const COL_COUNT As String = "COUNT"
    Public Const COL_COMPANY_CODE As String = "company_code"
    Public Const COL_STATUS As String = "status"
    Public Const COL_ISSUE_DESCRIPTION As String = "description"


    Public Const COL_IS_MASTER_MODEL As String = "is_master_equipment"
    Public Const COL_CERT_ID As String = "cert_id"
    Public Const COL_CERT_ID_NAME As String = "cert_id"
    Public Const COL_LOSS_DATE As String = "loss_date"
    Public Const COL_CERT_ITEM_ID_NAME As String = "cert_item_id"
    Public Const COL_SERVICE_CENTER_ID_NAME As String = "service_center_id"
    'REQ-1194
    Public Const COL_RISK_TYPE_ID = "risk_type_id"

    Public Const COL_DEVICE_TYPES As String = "device_type"
    Public Const COL_LIST_ITEM_ID As String = "list_item_id"

#End Region

#Region " Constructors "


    Private Sub New()
        'private so it can not be created because there are only static methods
    End Sub


#End Region

#Region " Private Methods "

    Private Shared ReadOnly _syncLock As New Object

    Private Shared Function RetrieveList(listName As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim dv As DataView = Nothing

        dv = RetrieveFromCache(listName, displayNothingSelected)
        If (dv Is Nothing) Then
            Try 
                dv = LookupListDALNew.Load(listName)
            Catch ex As Exception
                AppConfig.Log(DirectCast(New ElitaPlusException(ex.Source, "LookupListNew::RetriveList::DAL", ex), Exception))
                Throw
            End Try
            

            Try 
                SyncLock _syncLock
                    If (RetrieveFromCache(listName, displayNothingSelected) Is Nothing) Then
                        AddToCache(listName, dv, displayNothingSelected)
                    Else
                        dv = RetrieveFromCache(listName, displayNothingSelected)
                    End If

                End SyncLock

            Catch ex As Exception
                AppConfig.Log(DirectCast(New ElitaPlusException(ex.Source, "LookupListNew::RetriveList::Lock", ex), Exception))
                Throw
            End Try

        End If


        'Now create an independent copy. So that users can sort and filter without afecting each other
        dv = New DataView(dv.Table)

        'Sort on Description by default
        If dv.Table.Columns.IndexOf(orderByColumn) >= 0 Then
            dv.Sort = orderByColumn
        End If

        Return dv

    End Function


    Private Shared Function RetrieveParamList(listName As String, params() As DBHelper.DBHelperParameter) As DataView
        Dim dv As DataView = Nothing

        dv = LookupListDALNew.Load(listName, params)


        'Now create an independent copy. So that users can sort and filter without afecting each other
        dv = New DataView(dv.Table)

        'Sort on Description by default
        If dv.Table.Columns.IndexOf(COL_DESCRIPTION_NAME) >= 0 Then
            dv.Sort = COL_DESCRIPTION_NAME
        End If

        Return dv

    End Function

    'Commented out and replaced with function below to avoid throwing errors to validate!
    'Private Shared Function GetDistint(ByVal dv As DataView, ByVal sDistinictColumnName As String) As DataView

    '    Dim PurgedDataTable As DataTable = dv.Table.Clone
    '    Dim _UniqueConstraint As UniqueConstraint = New UniqueConstraint(PurgedDataTable.Columns(sDistinictColumnName))
    '    PurgedDataTable.Constraints.Add(_UniqueConstraint)
    '    Dim j As Integer = 0
    '    While j < dv.Table.Rows.Count
    '        Try
    '            PurgedDataTable.ImportRow(dv.Table.Rows(j))
    '        Catch ex As Exception
    '            'Keep quite; I know there are doublicates.
    '        End Try
    '        System.Math.Min(System.Threading.Interlocked.Increment(j), j - 1)
    '    End While

    '    dv.Table = PurgedDataTable
    '    Return dv
    'End Function

    Private Shared Function GetDistinct(dv As DataView, sDistinictColumnName As String) As DataView

        Dim hTable = New Hashtable()
        Dim duplicateList = New ArrayList

        For Each drow As DataRow In dv.Table.Rows

            If hTable.Contains(drow(sDistinictColumnName)) Then
                duplicateList.Add(drow)
            Else
                hTable.Add(drow(sDistinictColumnName), String.Empty)
            End If
        Next

        For Each dRow As DataRow In duplicateList
            dv.Table.Rows.Remove(dRow)
        Next

        Return dv
    End Function


#End Region

#Region " Public Methods "


    Public Shared Function DataView(listName As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Return RetrieveList(listName, displayNothingSelected, orderByColumn)
    End Function

    Public Shared Function DoubleFilteredView(listName As String, filterField1 As String, filterValue1 As String, filterField2 As String, filterValue2 As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim dv As DataView = RetrieveList(listName, displayNothingSelected, orderByColumn)

        Dim rowFilter As String
        If ((filterField1 IsNot Nothing) AndAlso (filterField1.Length > 0) AndAlso (filterField2 IsNot Nothing) AndAlso (filterField2.Length > 0)) Then
            'standardize the wild card char
            If (filterValue1.IndexOf("%") > 0) Then filterValue1 = filterValue1.Replace("%", "*")
            If (filterValue2.IndexOf("%") > 0) Then filterValue2 = filterValue2.Replace("%", "*")

            If (filterValue1.IndexOf("*") > 0) Then
                rowFilter = filterField1 & " LIKE '" & filterValue1 & "' and "
            Else
                rowFilter = filterField1 & " = '" & filterValue1 & "' and "
            End If

            If (filterValue2.IndexOf("*") > 0) Then
                rowFilter &= filterField2 & " LIKE '" & filterValue2 & "'"
            Else
                rowFilter &= filterField2 & " = '" & filterValue2 & "'"
            End If

            dv.RowFilter = rowFilter
        End If

        Return dv
    End Function

    Public Shared Function FilteredView(listName As String, filterField As String, filterValue As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim dv As DataView = RetrieveList(listName, displayNothingSelected, orderByColumn)
        If ((filterField IsNot Nothing) AndAlso (filterField.Length > 0)) Then

            'standardize the wild card char
            If (filterValue.IndexOf("%") > 0) Then filterValue = filterValue.Replace("%", "*")

            If (filterValue.IndexOf("*") > 0) Then
                dv.RowFilter = filterField & " LIKE '" & filterValue & "'"
            Else
                dv.RowFilter = filterField & " = '" & filterValue & "'"
            End If

        End If

        Return dv

    End Function

    Public Shared Function FilteredView(listName As String, filterCondition As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim dv As DataView = RetrieveList(listName, displayNothingSelected, orderByColumn)

        dv.RowFilter = filterCondition

        Return dv

    End Function
    Public Shared Function FilteredDistinctView(listName As String, companyIds As ArrayList, sDistinictColumnName As String, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}

        Dim whereClauseConditions As String
        If listName = LK_DEALERS_DUAL_COLUMNS Then
            whereClauseConditions = Environment.NewLine & MiscUtil.BuildListForSql(" AND c." & COL_COMPANY_ID_NAME, companyIds, True)
        Else
            whereClauseConditions = Environment.NewLine & MiscUtil.BuildListForSql("WHERE " & COL_COMPANY_ID_NAME, companyIds, True)
        End If

        Dim dv As DataView = LookupListDALNew.Load(listName, params, whereClauseConditions, orderByColumn)
        dv = GetDistinct(dv, sDistinictColumnName)
        Return dv
    End Function

    Public Shared Function FilteredParamView(listName As String, filterCondition As String, params() As DBHelper.DBHelperParameter) As DataView
        Dim dv As DataView = RetrieveParamList(listName, params)

        dv.RowFilter = filterCondition

        Return dv

    End Function

    Public Shared Function FilteredViewWithDynamicInClause(listName As String, companyIds As ArrayList, filterField As String, filterValue As String) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}

        Dim inClauseConditions As String = Environment.NewLine & MiscUtil.BuildListForSql(" AND cc." & COL_COMPANY_ID_NAME, companyIds, True)

        Dim dv As DataView = LookupListDALNew.Load(listName, inClauseConditions)

        If filterField IsNot Nothing AndAlso filterValue IsNot Nothing Then
            dv.RowFilter = filterField & " LIKE '" & filterValue & "'"
        End If

        Return dv
    End Function

    Public Shared Function XMLView(listName As String, groupName As String, rowName As String) As String
        Dim ds As DataSet = LookupListDALNew.Load(listName, groupName, rowName)
        Return ds.GetXml()
    End Function


    Public Shared Function GetIdFromCode(listName As String, code As String) As Guid
        Dim id As Guid = Nothing
        Dim dv As DataView = DataView(listName)

        dv.Sort = "code"
        Dim idx As Integer = dv.Find(code)
        If (idx >= 0) Then id = New Guid(CType(dv(idx)("id"), Byte()))
        dv.Sort = ""

        Return id
    End Function

    Public Shared Function GetIdFromExtCode(listName As String, extcode As String) As Guid
        Dim id As Guid = Nothing
        Dim dv As DataView = DataView(listName)

        dv.Sort = "extended_code"
        Dim idx As Integer = dv.Find(extcode)
        If (idx >= 0) Then id = New Guid(CType(dv(idx)("id"), Byte()))
        dv.Sort = ""

        Return id
    End Function

    Public Shared Function GetIdFromCode(dv As DataView, code As String) As Guid
        Dim id As Guid = Nothing
        dv.Sort = "code"
        Dim idx As Integer = dv.Find(code)
        If (idx >= 0) Then id = New Guid(CType(dv(idx)("id"), Byte()))
        dv.Sort = ""

        Return id
    End Function
    Public Shared Function GetCodeFromId(listName As String, id As Guid) As String
        Dim dv As DataView = DataView(listName)
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then
                Return dv(i)(COL_CODE_NAME).ToString
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function GetSequenceFromId(dv As DataView, id As Guid) As Long
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then
                Return CType(dv(i)(BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME), Long)
            End If
        Next

        Return -1
    End Function

    Public Shared Function GetDescriptionFromId(dv As DataView, id As Guid) As String
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then
                Return CType(dv(i)(COL_DESCRIPTION_NAME), String)
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function GetDescriptionFromId(listName As String, id As Guid, languageId As Guid) As String
        Dim dv As DataView = DataView(listName)
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then

                If GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(CType(dv(i)(COL_LANGUAGE_ID_NAME), String))).Equals(languageId) Then
                    Return dv(i)(COL_DESCRIPTION_NAME).ToString
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetLanguageDescriptionFromId(listName As String, id As Guid, languageId As Guid) As String
        Dim dv As DataView = DataView(listName)
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then

                If New Guid(CType(dv(i)(COL_LANGUAGE_ID_NAME), Byte())).Equals(languageId) Then
                    Return CType(dv(i)(COL_DESCRIPTION_NAME), String)
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetCodeFromId(dv As DataView, id As Guid) As String
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then
                Return CType(dv(i)(COL_CODE_NAME), String)
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function GetDescriptionFromId(listName As String, id As Guid, Optional ByVal blnFilterByUserLangauge As Boolean = False) As String

        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim dv As DataView = DataView(listName)
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then
                If blnFilterByUserLangauge = True Then
                    'return only the description of the user language
                    If dv(i)("language_id") = GuidControl.GuidToHexString(languageId) Then
                        Return dv(i)(COL_DESCRIPTION_NAME).ToString
                    End If
                Else
                    Return dv(i)(COL_DESCRIPTION_NAME).ToString
                End If
            End If

            'If blnFilterByUserLangauge Then
            '    If GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(CType(dv(i)(LookupListNew.COL_LANGUAGE_ID_NAME), String))).Equals(languageId) Then
            '        Return dv(i)(LookupListNew.COL_DESCRIPTION_NAME).ToString
            '    End If
            'Else
            '    If New Guid(CType(dv(i)(LookupListNew.COL_ID_NAME), Byte())).Equals(id) Then
            '        Return dv(i)(LookupListNew.COL_DESCRIPTION_NAME).ToString
            '    End If
            'End If

        Next

        Return Nothing
    End Function

    Public Shared Function GetDescriptionFromCode(listName As String, code As String, languageId As Guid) As String
        Dim i As Integer
        Dim dv As DataView = DataView(listName)

        For i = 0 To dv.Count - 1
            If CType(dv(i)(COL_CODE_NAME), String).Equals(code) Then

                If GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(CType(dv(i)(COL_LANGUAGE_ID_NAME), String))).Equals(languageId) Then
                    Return dv(i)(COL_DESCRIPTION_NAME).ToString
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetDescriptionFromCode(dv As DataView, code As String) As String
        Dim desc As String = Nothing
        dv.Sort = "code"
        Dim idx As Integer = dv.Find(code)
        If (idx >= 0) Then desc = dv(idx)(COL_DESCRIPTION_NAME)
        dv.Sort = ""

        Return desc
    End Function

    Public Shared Function GetDescriptionFromCode(listName As String, code As String) As String
        Dim desc As String = Nothing
        Dim dv As DataView = DataView(listName)

        dv.Sort = "code"
        Dim idx As Integer = dv.Find(code)
        If (idx >= 0) Then desc = dv(idx)(COL_DESCRIPTION_NAME)
        dv.Sort = ""

        Return desc
    End Function

    Public Shared Function GetDescriptionFromExtCode(listName As String, languageId As Guid, extcode As String) As String
        Dim desc As String = Nothing
        Dim dv As DataView = DropdownLookupList(listName, languageId)
        dv.RowFilter = dv.RowFilter & " and ExtCode= '" & extcode & "'"
        If dv.Count > 0 Then
            desc = dv(0)(COL_DESCRIPTION_NAME)
        End If
        Return desc
    End Function
    Public Shared Function GetCodeFromDescription(dv As DataView, desc As String) As String
        Dim code As String = Nothing

        dv.Sort = "description"
        Dim idx As Integer = dv.Find(desc)
        If (idx >= 0) Then code = dv(idx)(COL_CODE_NAME)
        dv.Sort = ""

        Return code
    End Function

    Public Shared Function GetIdFromDescription(listName As String, description As String) As Guid
        Dim id As Guid = Nothing
        Dim dv As DataView = DataView(listName)

        dv.Sort = "description"
        Dim idx As Integer = dv.Find(description)
        If (idx >= 0) Then id = New Guid(CType(dv(idx)("id"), Byte()))
        dv.Sort = ""

        Return id
    End Function

    Public Shared Function GetIdFromDescription(dv As DataView, description As String) As Guid
        Dim id As Guid = Nothing
        dv.Sort = "description"
        Dim idx As Integer = dv.Find(description)
        If (idx >= 0) Then id = New Guid(CType(dv(idx)("id"), Byte()))
        dv.Sort = ""

        Return id
    End Function

#End Region

#Region "Company Related LookupLists"

    Public Shared Function GetAccountingClosingYearsLookupList(companyId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ACCOUNTING_CLOSING_YEARS, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(companyId))

        Return (dv)

    End Function

    Public Shared Function GetDealerLookupList(companyId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DEALERS, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(companyId))

        Return (dv)

    End Function

    Public Shared Function GetParentDealerLookupList(CompanyId As Guid) As DataView
        Return (FilteredView(LK_PARENT_DEALERS, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(CompanyId)))
    End Function
    Public Shared Function GetOriginalDealerLookupList(companyId As Guid, ServiceCenterId As Guid) As DataView


        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter("Company_id", companyId.ToByteArray),
        New DBHelper.DBHelperParameter("Service_center_id", ServiceCenterId.ToByteArray)}

        Dim dv As DataView = FilteredParamView(LK_ORIGINAL_DEALER_LIST, "", parameters)

        Return (dv)

    End Function

    ''' <summary>
    ''' Gets Deales from current User's Selected Companies with Edit Model = Yes
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetDealerEditModelLookupList() As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_USER_ID_NAME, ElitaPlusIdentity.Current.ActiveUser.Id)}
        Dim dv As DataView = LookupListDALNew.Load(LK_DEALER_EDIT_MODEL, params)
        Return (dv)
    End Function

    Public Shared Function GetDealerGroupLookupList(companyGrpId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DEALER_GROUPS, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(companyGrpId))

        Return (dv)

    End Function

    '08/01/2006 - ALR - Added modified method to accept multiple company ids for a filter.
    'Public Shared Function GetDealerGroupLookupList(ByVal companyIds As ArrayList) As DataView

    '    Dim strCompanies As New System.Text.StringBuilder ' Used a SB for future growth
    '    Dim strCompanyId As Guid

    '    'Initialize the filter
    '    strCompanies.Append(COL_COMPANY_ID_NAME + " in (")

    '    'Loop through the user's companies and append to the filter
    '    For Each strCompanyId In companyIds

    '        strCompanies.Append("'" + GuidControl.GuidToHexString(strCompanyId) + "'")

    '        'If in the last item of the arraylist, close the parens, else, add a comma
    '        If Not companyIds.IndexOf(strCompanyId) = companyIds.Count - 1 Then
    '            strCompanies.Append(",")
    '        Else
    '            strCompanies.Append(")")
    '        End If
    '    Next

    '    Dim dv As DataView = FilteredView(LK_DEALER_GROUPS, strCompanies.ToString)

    '    Return (dv)

    'End Function



    Public Shared Function GetDealerCommissionBreakDownLookupList(companyId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DEALER_COMMISSION_BREAKDOWN, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(companyId))

        Return (dv)

    End Function

    '07/2006 - ALR - Added to provide the dynamic query ability and enable multiple company lookups
    Public Shared Function GetDealerCommissionBreakDownLookupList(companyIds As ArrayList, Optional ByVal bAddCompanyCodeToDescription As Boolean = True, Optional ByVal sDistinictColumnName As String = Nothing) As DataView

        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql("C." & COL_COMPANY_ID_NAME, companyIds)
        Dim sListName As String
        If bAddCompanyCodeToDescription Then
            sListName = LK_DEALER_COMMISSION_BREAKDOWN_DUAL_COLUMNS
        Else
            sListName = LK_DEALER_COMMISSION_BREAKDOWN
        End If

        Dim dv As DataView

        If companyIds.Count > 1 Then
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, companyIds, sDistinictColumnName, "")
            End If
        Else
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, companyIds, sDistinictColumnName, "")
            End If
        End If

        Return (dv)

    End Function

    Public Shared Function GetUserDealerLookupList(UserId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_USER_DEALER, COL_USER_ID_NAME, DALBase.GuidToSQLString(UserId))

        Return (dv)

    End Function

    Public Shared Function GetUserDealerAssignedLookupList(userId As Guid, dealer As String) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_USER_ID_NAME, DALBase.GuidToSQLString(userId)) _
                    , New DBHelper.DBHelperParameter(COL_USER_ID_NAME, DALBase.GuidToSQLString(userId)) _
                    , New DBHelper.DBHelperParameter(COL_DEALER, dealer)}

        Dim dv As DataView = FilteredParamView(LK_USER_DEALER_ASSIGNED, Nothing, params)
        Return (dv)

    End Function

    'Public Shared Function GetUserSCLookupList(ByVal UserId As Guid) As DataView

    '    Dim dv As DataView = FilteredView(LK_USER_SC, COL_USER_ID_NAME, DALBase.GuidToSQLString(UserId))

    '    Return (dv)

    'End Function

    Public Shared Function GetDealerMonthlyBillingLookupList(companyId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_DEALERS_MONTHLY_BILLING, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(companyId))
        Return (dv)
    End Function

    Public Shared Function GetDealerMonthlyBillingLookupList(companyIds As ArrayList, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim dv As DataView = FilteredView(LK_DEALERS_MONTHLY_BILLING, sFilterCondition, True, orderByColumn)

        Return (dv)
    End Function

    Public Shared Function GetAddressLookupList(companyId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ADDRESSES, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(companyId))

        Return (dv)

    End Function

    Public Shared Function GetCompanyLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_COMPANY, "", "")

        Return (dv)

    End Function

    Public Shared Function GetCompanyLookupList(oCompId As Guid) As DataView
        Dim parameters() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter("CompanyId", oCompId.ToByteArray)}
        Dim dv As DataView = FilteredParamView(LK_GET_COMPANY, "", parameters)
        Return (dv)

    End Function

    Public Shared Function GetAcctCompanyLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_ACCTCOMPANY, "", "")

        Return (dv)

    End Function


    Public Shared Function GetCompanyGroupLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_COMPANY_GROUP, "", "")

        Return (dv)

    End Function

    Public Shared Function GetCompanyGroupNoptInUseLookupList(companyGrpIds As ArrayList) As DataView
        Dim inClauseConditions As String = Environment.NewLine & MiscUtil.BuildListForNetSql("where " & COL_COMPANY_GROUP_ID_NAME, companyGrpIds)
        ' Dim sListName As String = LK_COMPANY_GROUPS
        Dim dv As DataView = LookupListDALNew.Load(LK_COMPANY_GROUPS, inClauseConditions)
        Return (dv)

    End Function

    Public Shared Function GetCompanyCreditCardsFormatLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMPANY_CREDIT_CARDS_FORMAT, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetExtractTypeLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_EXTRACT_TYPE_LIST, Nothing)

        Return (dv)

    End Function

    '5623
    Public Shared Function GetUserCompanyLookupList(companygroupid As Guid, userid As Guid) As DataView


        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter("Company_group_id", companygroupid.ToByteArray),
        New DBHelper.DBHelperParameter("User_id", userid.ToByteArray)}

        Dim dv As DataView = FilteredParamView(LK_GET_USER_COMPANIES, "", parameters)

        Return (dv)


    End Function
    '5623
    Public Shared Function GetClaimStatusByUserRole() As DataView


        Dim dv As DataView = FilteredView(LK_GET_CLAIM_STATUS_BY_USER_ROLE, "", "")

        Return (dv)


    End Function


    Public Shared Function GetCompanyCreditCardsLookupList(languageId As Guid, CompanyId As Guid) As DataView

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, CompanyId)}

        Dim dv As DataView = FilteredParamView(LK_COMPANY_CREDIT_CARDS, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetRepairCodeLookupList(companyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_REPAIR_CODES, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(companyGroupId))

        Return (dv)

    End Function

    'Public Shared Function GetRepairCodeLookupList(ByVal companyIds As ArrayList, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
    '    Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
    '    Dim dv As DataView = FilteredView(LK_REPAIR_CODES, sFilterCondition, True, orderByColumn)

    '    Return (dv)
    'End Function

    Public Shared Function GetDealerLookupList(companyIds As ArrayList,
                                                Optional ByVal bAddCompanyCodeToDescription As Boolean = True,
                                                Optional ByVal sDistinictColumnName As String = Nothing,
                                                Optional ByVal dynamicOrderBYClause As String = "description",
                                                Optional ByVal withEditBranch As Boolean = False,
                                                Optional ByVal bIgnoreCompanyCount As Boolean = False) As DataView

        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim sListName As String

        If bAddCompanyCodeToDescription AndAlso (companyIds.Count > 1 OrElse bIgnoreCompanyCount) Then
            If withEditBranch Then
                sListName = LK_DEALERS_DUAL_COLUMNS_WITH_EDIT_BRANCH_ONLY
            Else
                sListName = LK_DEALERS_DUAL_COLUMNS
            End If
        Else
            If withEditBranch Then
                sListName = LK_DEALERS_WITH_EDIT_BRANCH_ONLY
            Else
                sListName = LK_DEALERS
            End If
        End If

        Dim dv As DataView

        If companyIds.Count > 1 Then
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, companyIds, sDistinictColumnName, dynamicOrderBYClause)
            End If
        Else
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, companyIds, sDistinictColumnName, dynamicOrderBYClause)
            End If
        End If



        If ElitaPlusIdentity.Current.ActiveUser.IsDealerGroup Then
            dv.RowFilter = "dealer_group_id = '" & DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.ScDealerId) & "'"
        End If

        Return (dv)

    End Function

    Public Shared Function GetProducerLookupList(companyIds As ArrayList) As DataView

        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim sListName As String

        sListName = LK_PRODUCERS

        Dim dv As DataView
        dv = FilteredView(sListName, sFilterCondition)

        Return (dv)

    End Function

    Public Shared Function GetDealerLookupListByAttribute(companyIds As ArrayList,
                                                           TableName As String,
                                                           UI_Prog_Code As String) As DataView

        Dim companyidstring As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)

        Dim dv As DataView
        dv = LookupListDALNew.LookuplistDealerByAttribute(companyidstring, TableName, UI_Prog_Code)


        Return (dv)

    End Function
    Public Shared Function GetDealerLookupListByDealerType(companyIds As ArrayList,
                                                            DealerType As String) As DataView

        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim sListName As String
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("sDealerType", DealerType) _
           , New DBHelper.DBHelperParameter("sSecond_dealer_type", If(DealerType = "ESC", "WEPP", DealerType))}

        sListName = LK_DEALERS_BY_DEALER_TYPE

        Dim dv As DataView = FilteredParamView(LK_DEALERS_BY_DEALER_TYPE, sFilterCondition, params)



        If ElitaPlusIdentity.Current.ActiveUser.IsDealerGroup Then
            dv.RowFilter = "dealer_group_id = '" & DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.ScDealerId) & "'"
        End If
        Return (dv)
    End Function


    Public Shared Function GetDealersCommPrdLookupList(companyIds As ArrayList) As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)

        Dim dv As DataView = FilteredView(LK_DEALERS_COMM_PROD, sFilterCondition)

        Return (dv)

    End Function

    Public Shared Function GetDealersGroupsByCompanyLookupList(companyIds As ArrayList, Optional ByVal bAddCompanyCodeToDescription As Boolean = True) As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim sListName As String

        'If bAddCompanyCodeToDescription And companyIds.Count > 1 Then
        '    sListName = LK_DEALERS_GROUPS_BY_COMPANY_DUAL_COLUMNS
        'Else
        '    sListName = LK_DEALERS_GROUPS_BY_COMPANY
        'End If

        sListName = LK_DEALERS_GROUPS_BY_COMPANY_DUAL_COLUMNS

        'Dim dv As DataView = FilteredView(sListName, sFilterCondition)
        Dim dv As DataView = FilteredDistinctView(sListName, companyIds, "Description")

        Return (dv)

    End Function

    Public Shared Function GetCampaignNumberLookupList(companyIds As ArrayList) As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        'If Not dealerId.Equals(Guid.Empty) Then
        '    sFilterCondition &= Environment.NewLine & " AND dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId, True) & ""
        'End If
        Dim dv As DataView = FilteredView(LK_CAMPAIGN_NUMBERS, sFilterCondition)

        Return (dv)

    End Function
    Public Shared Function GetDealerForProductCodeConvLookupList(companyIds As ArrayList, Optional ByVal bAddCompanyCodeToDescription As Boolean = True, Optional ByVal sDistinictColumnName As String = Nothing, Optional ByVal dynamicOrderBYClause As String = "description") As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim sListName As String
        If bAddCompanyCodeToDescription AndAlso companyIds.Count > 1 Then
            sListName = LK_DEALERS_PROD_CONV_DUAL_COLUMNS
        Else
            sListName = LK_DEALERS_PROD_CONV
        End If

        Dim dv As DataView

        If companyIds.Count > 1 Then
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, companyIds, sDistinictColumnName, dynamicOrderBYClause)
            End If
        Else
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, companyIds, sDistinictColumnName, dynamicOrderBYClause)
            End If
        End If

        Return (dv)

    End Function

    Public Shared Function GetEarningPatternStartsOnLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_EARNING_PATTERN_STARTS_ON, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetEarningCodeLookupList(companyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_EARNING_CODES, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(companyGroupId))

        Return (dv)

    End Function

    Public Shared Function GetCommEntityLookupList(companyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMMISSION_ENTITIES, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(companyGroupId))
        Return (dv)

    End Function

#End Region

#Region "Language Related LookupList"

    Public Shared Function GetLanguageLookupList(Optional ByVal DisplayNothingSelected As Boolean = False) As DataView

        Dim dv As DataView = FilteredView(LK_LANGUAGES, Nothing, DisplayNothingSelected)

        Return (dv)

    End Function

    'Public Shared Function GetServiceTypeLookupList(ByVal languageId As Guid) As DataView

    '    Dim dv As DataView = FilteredView(LK_SERVICE_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

    '    Return (dv)

    'End Function

    Public Shared Function GetPostPrePaidLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_POSTPRE_PAID, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    Public Shared Function GetComputeDeductibleBasedOnAndExpressions(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMPUTE_DEDUCTIBLE_BASED_ON_AND_EXPRESSIONS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetReplacementBasedOnLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_REPLACEMENT_BASED_ON, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCollectionCycleTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COLLECTION_CYCLE_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCoverageTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_COVERAGE_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetNotificationTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_NOTIICATION_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetWhoPaysLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_WHO_PAYS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetCoverageTypeByCompanyGroupLookupList(languageId As Guid, CompanyGroupId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, CompanyGroupId)}

        Dim dv As DataView = FilteredParamView(LK_COVERAGE_TYPE_BY_COMPANY_GROUP, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetCoverageTypeByDealerLookupList(languageId As Guid, DealerId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_DEALER_ID_NAME, DealerId)}

        Dim dv As DataView = FilteredParamView(LK_COVERAGE_TYPE_BY_DEALER, sFilterCondition, params)

        Return (dv)

    End Function
    Public Shared Function GetCoverageTypeLookupListNotInCovLoss(languageId As Guid, CompanyGroupId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
             {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, CompanyGroupId)}

        Dim dv As DataView = FilteredParamView(LK_COVERAGE_TYPES_NOT_IN_COV_LOSS, sFilterCondition, params)

        Return (dv)

    End Function
    Public Shared Function GetCoverageTypeByCompanyGroupLookupListNotInCovLoss(languageId As Guid, CompanyGroupId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
             {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, CompanyGroupId),
              New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, CompanyGroupId)}

        Dim dv As DataView = FilteredParamView(LK_COVERAGE_TYPES_BY_COMPANY_GROUP_NOT_IN_COV_LOSS, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetCauseOfLossByCoverageTypeLookupList(languageId As Guid, CompanyGroupId As Guid, CoverageTypeId As Guid, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal LoadNoneActive As Boolean = False) As DataView

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        If LoadNoneActive Then
            sFilterCondition += " and active is null"
        End If


        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, CompanyGroupId),
            New DBHelper.DBHelperParameter(COL_COVERAGE_TYPE_ID_NAME, CoverageTypeId)}

        Dim dv As DataView = FilteredParamView(LK_CAUSE_OF_LOSS_BY_COVERAGE_TYPE, sFilterCondition, params)


        'dv.RowFilter = "active is null"

        Return (dv)

    End Function

    Public Shared Function GetCauseOfLossByCoverageTypeAndSplSvcLookupList(CompanyGroupId As Guid, CoverageTypeId As Guid, DealerId As Guid, languageId As Guid, ProductCode As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal LoadNoneActive As Boolean = False) As DataView

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String '= MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        If LoadNoneActive Then
            sFilterCondition += " and active is null"
        End If


        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, CompanyGroupId),
            New DBHelper.DBHelperParameter(COL_COVERAGE_TYPE_ID_NAME, CoverageTypeId),
            New DBHelper.DBHelperParameter(COL_DEALER_ID_NAME, DealerId),
            New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageId),
            New DBHelper.DBHelperParameter(COL_PRODUCT_CODE_ID_NAME, ProductCode)}

        Dim dv As DataView = FilteredParamView(LK_CAUSE_OF_LOSS_BY_COVERAGE_TYPE_AND_SPL_SVC, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetCoverageTypeLookupList(itemId As Guid, languageId As Guid) As DataView
        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("item_id", itemId)}

        Dim dv As DataView = FilteredParamView(LK_ITEM_COVERAGETYPE, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetCertificateDurationLookupList(oItemId As Guid, oCoverageTypeId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("item_id", oItemId),
             New DBHelper.DBHelperParameter("coverage_type_id", oCoverageTypeId)}

        Dim dv As DataView = FilteredParamView(LK_CERTIFICATEDURATION, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetCoverageDurationLookupList(oItemId, oCoverageTypeId, sCertificateDuration) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("item_id", oItemId),
             New DBHelper.DBHelperParameter("coverage_type_id", oCoverageTypeId),
             New DBHelper.DBHelperParameter("sCertificateDuration", sCertificateDuration)}

        Dim dv As DataView = FilteredParamView(LK_COVERAGEDURATION, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetClaimTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CLAIM_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetColorLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COLORS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetClaimStatsActionLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ACTION, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetClaimActivityLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CLAIM_ACTIVITIES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCauseOfLossLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CAUSES_OF_LOSS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetProdLiabilityLimitBasedOnList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetCovergaePerilTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_COVERAGE_PERIL_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetBenefitsEligibleActionList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_PRODUCT_BENEFITS_ACTION, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function



    Public Shared Function GetProdLiabilityLimitPolicyList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_PROD_LIABILITY_LIMIT_POLICY_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetMethodOfRepairLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_METHODS_OF_REPAIR, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetMethodOfFinBalForUpgList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_UPG_FINANCE_BAL_COMP_METH, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetMethodOfRepairForRepairsLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_METHODS_OF_REPAIR_FOR_REPAIRS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetReasonClosedLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_REASONS_CLOSED, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetDeniedReasonLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DENIED_REASON, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetDeniedReasonLookupList(languageId As Guid, dealerCode As String) As DataView

        Dim dv As DataView = DoubleFilteredView(LK_DEALER_DENIED_REASON, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), COL_DEALER, dealerCode)

        Return (dv)

    End Function

    Public Shared Function GetCommentTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMMENT_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    Public Shared Function GetServiceOrderReportLookupList(companyID As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SERVICE_ORDER_REPORTS, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(companyID))

        Return (dv)

    End Function

    Public Shared Function GetRiskGroupsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_RISK_GROUPS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetClaimSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CLAIM_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetRepairLogisticsSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_REPAIR_LOGISTICS_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetAdjusterClaimSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ADJUSTER_CLAIM_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetNotificationSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_NOTIFICATION_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetHUBRegionsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_HUB_REGIONS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function


    Public Shared Function GetSortOrderLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SORT_ORDER, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetClaimSearchByCommentTypeFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CLAIM_SEARCH_BY_COMMENT_TYPE_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetPendingClaimSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PENDING_CLAIM_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetMonthsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_MONTHS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetClaimFollowUpSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CLAIM_FOLLOWUP_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCertificateSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CERTIFICATE_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetInvoiceSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_INVOICE_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetReplacementPolicyLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_REPLACEMENT_POLICIES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetSalutationLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SALUTATION, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetDedCollMethodLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DED_COLL_METHOD, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetSalutationLanguageLookupList(languageId As Guid) As DataView
        Dim dv As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                   {New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray)}

        dv = FilteredParamView(LK_SALUTATION_LANGUAGE, String.Empty, params)

        Return (dv)

    End Function

    Public Shared Function GetMembershipTypeLanguageLookupList(languageId As Guid) As DataView
        Dim dv As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                   {New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray)}

        dv = FilteredParamView(LK_MEMBERSHIP_TYPE_LANGUAGE, String.Empty, params)

        Return (dv)

    End Function

    Public Shared Function GetPaymentMethodLookupList(languageId As Guid, Optional ByVal IncludeBankTransfer As Boolean = True,
                                                      Optional ByVal UserId As String = Nothing,
                                                      Optional ByVal CompanyId As String = Nothing,
                                                      Optional ByVal ExcludeByRole As Boolean = False) As DataView

        Dim dv As DataView
        If ExcludeByRole = True Then
            If IncludeBankTransfer Then
                Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                   {New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray),
                    New DBHelper.DBHelperParameter("company_id", New Guid(CompanyId).ToByteArray),
                    New DBHelper.DBHelperParameter("user_id", New Guid(UserId).ToByteArray)}

                dv = FilteredParamView(LK_PAYMENTMETHOD_BY_ROLE, String.Empty, params)

            End If
        Else
            If IncludeBankTransfer Then
                dv = FilteredView(LK_PAYMENTMETHOD, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False, IncludeBankTransfer)
            Else
                dv = FilteredView(LK_PAYMENTMETHOD_WITH_OUT_BANKTRANSFER, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False, IncludeBankTransfer)
            End If
        End If

        Return (dv)

    End Function
    Public Shared Function GetPaymentReasonLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PAYMENTREASON, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function
    Public Shared Function GetAcctStatusLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ACCTSTATUS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetAccountTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ACCOUNT_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetPayeeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PAYEE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCommScheduleLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMM_SCHL, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    Public Shared Function GetPayeeTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PAYEE_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    Public Shared Function GetCoinsuranceLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COINSURANCE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function


    Public Shared Function GetCompanyGroupClaimNumberingLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMPANY_GROUP_CLAIM_NUMBERING, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCompanyGroupInvoiceNumberingLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMPANY_GROUP_INVOICE_NUMBERING, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    'REQ-1142
    Public Shared Function GetInactivatNewVehicleLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_INACTIVATE_NEW_VEHICLE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    'REQ-863
    Public Shared Function GetCompanyGroupInvoicegrpNumberingLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_COMPANY_GROUP_INVOICE_GRP_NUMBERING, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetLicenseTagFlag(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_LICENSE_TAG_FLAG, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    'REQ-1142
    'REQ-5723 start
    Public Shared Function GetVinRestricMandatoryInfo(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_VRSTID, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetPlancodeInQuoteOutput(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PLAN_QUOTE_IN_QUOTE_OUTPUT, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    'REQ-5723 End

    Public Shared Function GetCompanyGroupAuthorizationNumberingLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_COMPANY_GROUP_AUTHORIZATION_NUMBERING, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    'END REQ-863

    Public Shared Function GetCommTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COMMISSION_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetDealerBrokerLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DEALE_BROKER, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetIntegratedWithLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_INTEGRATED_WITH, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetGVSFunctionTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_GVS_FUNCTION_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetGVSTransactionStatusList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_GVS_TRANSACTION_STATUS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetOccurancesAllowedLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("OCCRALWD", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetPriceGroupDPLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("PRCGROUP", languageId, displayNothingSelected)

    End Function

#End Region

#Region "Currency Related LookupList"

    Public Shared Function GetCurrencyNotationLookupList(oCompanyId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
  {New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, oCompanyId)}

        Dim dv As DataView = FilteredParamView(LK_CURRENCY_NOTATION_BY_COUNTRY, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetCurrenciesForCompanyandDealersInCompanyLookupList(oCompanyId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
  {New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, oCompanyId.ToByteArray()),
   New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, oCompanyId.ToByteArray())}

        Dim dv As DataView = FilteredParamView(LK_CURRENCY_BY_COMPANY_AND_DEALERS_IN_COMPANY, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetCurrenciesForCompanyByUserLookupList(oUserId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
  {New DBHelper.DBHelperParameter(COL_USER_ID_NAME, oUserId.ToByteArray())}

        Dim dv As DataView = FilteredParamView(LK_CURRENCY_BY_COMPANIES_BY_USER, Nothing, params)

        Return (dv)

    End Function
#End Region

#Region "Country Related LookupLists"

    Public Shared Function GetCountryLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_COUNTRIES, Nothing)

        Return (dv)

    End Function

    Public Shared Function GetCountryLookupList(oCompanyId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
    {New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, oCompanyId)}

        Dim dv As DataView = FilteredParamView(LK_COUNTRY_BY_COMPANY, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetCompanyGroupCountryLookupList(oCompanyGroupId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
    {New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, oCompanyGroupId)}

        Dim dv As DataView = FilteredParamView(LK_COMPANY_GROUP_COUNTRIES, Nothing, params)

        Return (dv)

    End Function
    Public Shared Function GetZipDistrictLookupList(oCountryId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ZIP_DISTRICTS, COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(oCountryId), , COL_CODE_NAME)

        Return (dv)

    End Function

    Public Shared Function GetPriceGroupLookupList(oCountryId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PRICE_GROUPS, COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(oCountryId))

        Return (dv)

    End Function

    Public Shared Function GetPriceListLookupList(oCountryId As Guid) As DataView

        Dim countryIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        countryIdList.Add(oCountryId)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
    {New DBHelper.DBHelperParameter(COL_COUNTRY_ID_NAME, oCountryId)}

        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_COUNTRY_ID_NAME, countryIdList)
        Dim dv As DataView = FilteredParamView(LK_PRICE_LIST, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetServiceGroupLookupList(oCountryId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SERVICE_GROUPS, COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(oCountryId))

        Return (dv)

    End Function

    Public Shared Function GetInvTypList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_INVTYP, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetInvStatList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_INVSTAT, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function


    Public Shared Function GetServiceCenterLookupList(oCountryId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SERVICE_CENTERS, COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(oCountryId))

        Return (dv)

    End Function
    Public Shared Function GetServiceNetworkLookupList(oCompanyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SERVICE_NETWORKS, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(oCompanyGroupId))

        Return (dv)

    End Function

    Public Shared Function GetServiceCenterLookupList(oCountryIds As ArrayList) As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COUNTRY_ID_NAME, oCountryIds)
        Dim dv As DataView = FilteredView(LK_SERVICE_CENTERS, sFilterCondition)

        Return (dv)

    End Function

    Public Shared Function GetStoreLookupList(oCountryIds As ArrayList) As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COUNTRY_ID_NAME, oCountryIds)
        Dim dv As DataView = FilteredView(LK_PICKLIST_STORES, sFilterCondition)

        Return (dv)

    End Function

    Public Shared Function GetReplacementStoresLookupList(oCountryIds As ArrayList) As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COUNTRY_ID_NAME, oCountryIds)
        Dim dv As DataView = FilteredView(LK_REPLACEMENT_STORES, sFilterCondition)

        Return (dv)

    End Function

    Public Shared Function GetPicklistLookupList(oCompanyGroupId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_PICKLIST_HEADER, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(oCompanyGroupId))

        Return (dv)

    End Function

    Public Shared Function GetLoanerCenterLookupList(oCountryId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_LOANER_CENTERS, COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(oCountryId))

        Return (dv)

    End Function

    Public Shared Function GetManufacturerLookupList(oCompanyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_MANUFACTURERS, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(oCompanyGroupId))

        Return (dv)

    End Function

    Public Shared Function GetIssueLookupList(oCountryId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ISSUES, Nothing)

        Return (dv)

    End Function

    Public Shared Function GetIssueLookupListGlobal() As DataView

        Dim dv As DataView = FilteredView(LK_ISSUES, Nothing)

        Return (dv)

    End Function


    Public Shared Function GetRiskTypeLookupList(companyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_RISKTYPES, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(companyGroupId))

        Return (dv)

    End Function


    Public Shared Function GetPoliceLookupList(countryIds As ArrayList, Optional ByVal bAddCountryCodeToDescription As Boolean = True, Optional ByVal sDistinictColumnName As String = Nothing, Optional ByVal dynamicOrderBYClause As String = "description") As DataView
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql(COL_COUNTRY_ID_NAME, countryIds)
        Dim sListName As String
        'If bAddCountryCodeToDescription And countryIds.Count > 1 Then
        'sListName = LK_POLICE_STATIONS_DUAL_COLUMNS
        'Else
        sListName = LK_POLICE_STATIONS
        'End If

        Dim dv As DataView

        If countryIds.Count > 1 Then
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, countryIds, sDistinictColumnName, dynamicOrderBYClause)
            End If
        Else
            If (sDistinictColumnName = Nothing OrElse Trim(sDistinictColumnName) = "") Then
                dv = FilteredView(sListName, sFilterCondition)
            Else
                dv = FilteredDistinctView(sListName, countryIds, sDistinictColumnName, dynamicOrderBYClause)
            End If
        End If

        Return (dv)

    End Function

    Public Shared Function GetRouteLookupList(oCompanyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ROUTE, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(oCompanyGroupId))

        Return (dv)

    End Function

    Public Shared Function GetServiceCenterByDealerLookupList(oDealerId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SERVICE_CENTERS_DEALER, COL_DEALER_ID_NAME, DALBase.GuidToSQLString(oDealerId))

        Return (dv)

    End Function

    Public Shared Function GetExtendedStatusLookupList(oCompanyGroupId As Guid, languageId As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, oCompanyGroupId),
         New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageId)}

        Dim dv As DataView = FilteredParamView(LK_EXTENDED_STATUS, Nothing, params)

        Return (dv)

    End Function
    Public Shared Function GetExtendedStatusByGroupLookupList(oCompanyGroupId As Guid, languageId As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, oCompanyGroupId),
         New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageId)}

        Dim dv As DataView = FilteredParamView(LK_EXTENDED_STATUS_BY_GROUP_LIST, Nothing, params)

        Return (dv)

    End Function
    '5623
    Public Shared Function GetExtendedStatusByGroupId(oCompanyGroupId As Guid, languageId As Guid, oExtClmStatusId As Guid) As guid

        Dim dvExtendedStatus As DataView = GetExtendedStatusByGroupLookupList(oCompanyGroupId, languageId)
        Dim i As Integer, extendedStatusByGroupId As Guid

        For i = 0 To dvExtendedStatus.Count - 1
            If New Guid(CType(dvExtendedStatus(i)(COL_LIST_ITEM_ID), Byte())).Equals(oExtClmStatusId) Then
                extendedStatusByGroupId = New Guid(CType(dvExtendedStatus(i)(COL_ID_NAME), Byte()))
                Return extendedStatusByGroupId
            End If
        Next

        Return Nothing
    End Function
    Public Shared Function GetExtendedStatusByGroupUserRoleLookupList(oCompanyGroupId As Guid, languageId As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, oCompanyGroupId),
         New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageId)}

        Dim dv As DataView = FilteredParamView(LK_EXTENDED_STATUS_BY_USER_ROLE_LIST, Nothing, params)

        Return (dv)
    End Function

    Public Shared Function GetExtendedClaimStatusDefaultTypes(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_CLAIM_EXTENDED_STATUS_DEFAULT_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)

        Return (dv)

    End Function

    Public Shared Function GetSCTurnAroundTimeByGroupLookupList(oCompanyGroupId As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, oCompanyGroupId)}

        Dim dv As DataView = FilteredParamView(LK_SC_TAT_BY_GROUP_LIST, Nothing, params)

        Return (dv)

    End Function

#End Region

#Region "Dealer Related LookupLists"
    Public Shared Function GetProductCodeLookupList(dealerId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PRODUCTCODE, COL_DEALER_ID_NAME, DALBase.GuidToSQLString(dealerId), True, COL_CODE_NAME)

        Return (dv)

    End Function

    Public Shared Function GetAvailableProductCodeRiskTypeLookupList(id As Guid, tableName As String, column As String) As DataView
        Return LookupListDALNew.Load(LK_AVAILABLEPRODUCTCODERISKTYPE, id, tableName, column)
    End Function

    Public Shared Function GetSelectedProductCodeRiskTypeLookupList(id As Guid, tableName As String, column As String) As DataView
        Return LookupListDALNew.Load(LK_SELECTEDPRODUCTCODERISKTYPE, id, tableName, column)
    End Function
    Public Shared Function GetDealerTypeId(CompanyGroupId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(CompanyGroupId))}

        Dim dv As DataView = FilteredParamView(LK_DEALER_TYPE_COMPANY_GROUP_ID, Nothing, params)

        Return (dv)

    End Function
    Public Shared Function GetCompaniesFromCountry(Country_id As Guid) As DataView 'REQ-5980

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(Country_id))}

        Dim dv As DataView = FilteredParamView(LK_COMPANIES_COUNTRY_ID, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetDealersFromCountry(Country_id As Guid) As DataView 'REQ-5980

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(Country_id))}

        Dim dv As DataView = FilteredParamView(LK_DEALERS_COUNTRY_ID, Nothing, params)

        Return (dv)

    End Function
    Public Shared Function GetProductCodesFromCountry(Country_id As Guid) As DataView 'REQ-5980

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(Country_id))}

        Dim dv As DataView = FilteredParamView(LK_PRODUCTCODE_COUNTRY_ID, Nothing, params)

        Return (dv)

    End Function
    Public Shared Function GetBranchCodeLookupList(dealerId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_BRANCHCODE, COL_DEALER_ID_NAME, DALBase.GuidToSQLString(dealerId), True, COL_CODE_NAME)

        Return (dv)

    End Function

    Public Shared Function GetTemplateLookupList(dealercode As String) As DataView
        Dim dv As DataView = FilteredView(LK_TEMPLATE_DEALER, COL_DEALER, dealercode, True, COL_CODE_NAME)
        Return (dv)
    End Function

#End Region

#Region "Product Code Related LookupLists"
    Public Shared Function GetItemRiskTypeLookupList(productId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_ITEM_RISKTYPE, COL_PRODUCT_CODE_ID_NAME, DALBase.GuidToSQLString(productId))

        Return (dv)

    End Function

    Public Shared Function GetRiskProductCodeLookupList(productId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_RISK_PRODUCTCODE, COL_PRODUCT_CODE_ID_NAME, DALBase.GuidToSQLString(productId))

        Return (dv)

    End Function

    Public Shared Function GetProductItemLookupList(languageId As Guid) As DataView

        Return DropdownLookupList("PRODI", languageId)

    End Function


    Public Shared Function GetPriceMatrixLookupList(productId As Guid, languageID As Guid) As DataView

        Dim productIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        productIdList.Add(productId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_PRODUCT_CODE_ID_NAME, productIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageID)}

        Dim dv As DataView = FilteredParamView(LK_PRICEMATRIX_ITEM, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetPercentOfRetailLookup(productId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PERCENTOF_RETAIL_ITEM, COL_PRODUCT_CODE_ID_NAME, DALBase.GuidToSQLString(productId))

        Return (dv)

    End Function

    Public Shared Function GetTypeOfEquipmentLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("TEQP", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetProductCodeByCompanyLookupList(companyIds As ArrayList, Optional ByVal bAddCompanyCodeToDescription As Boolean = True, Optional ByVal sDistinictColumnName As String = Nothing, Optional ByVal dynamicOrderBYClause As String = "description") As DataView

        Dim dv As DataView = FilteredViewWithDynamicInClause(LK_PRODUCTCODE_BY_COMPANY, companyIds, Nothing, Nothing)
        Return (dv)

    End Function

    Public Shared Function GetProductCodeByDealerLookupList(dealerId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
   {New DBHelper.DBHelperParameter(COL_DEALER_ID_NAME, dealerId)}

        Dim dv As DataView = FilteredParamView(LK_PRODUCTCODE_BY_DEALER, Nothing, params)
        Return (dv)

    End Function
#End Region

#Region "General LookupList"

    Public Shared Function GetCategoriesLookupList(Tab As String) As DataView

        Dim dv As DataView = FilteredView(LK_CATEGORIES, COL_TAB_NAME, Tab, False)

        Return (dv)

    End Function

    Public Shared Function GetPostalCodeFormatLookupList() As DataView

        Dim dv As DataView = DataView(LK_POSTALCODEFORMAT)

        Return (dv)

    End Function
    Public Shared Function GetCertNumberFormatLookupList() As DataView

        Dim dv As DataView = DataView(LK_CERT_NUMBER_FORMAT)

        Return (dv)

    End Function
    Public Shared Function getCertNumberFormatDescription(CertNumberFormatId As Guid) As String
        Dim CertNumberFormatDesc As String
        Dim dv As DataView = DataView(LK_CERT_NUMBER_FORMAT)
        CertNumberFormatDesc = GetDescriptionFromId(dv, CertNumberFormatId)
        Return CertNumberFormatDesc

    End Function

    Public Shared Function GetPaymentTypeLookupList() As DataView

        Dim dv As DataView = DataView(LK_PAYMENT_TYPES)

        Return (dv)

    End Function
    Public Shared Function GetIncomeRangeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_INCOME_RANGE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function
    'req 5547
    Public Shared Function GetFastApprovalList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_FAST_APPROVAL_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function


    Public Shared Function GetPaymentTypeLookupList(CompanyGroupId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PAYMENT_TYPES, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(CompanyGroupId), False)

        Return (dv)

    End Function

    Public Shared Function GetClaimSystemLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_CLAIM_SYSTEM, Nothing)

        Return (dv)

    End Function


    Public Shared Function GetDocumentTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DOCUMENT_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetValidationTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_VALIDATION_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetAudianceTypeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_AUDIANCE_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetAuditSourceList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_AUDIT_SOURCE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function
    Public Shared Function GetSystemNotificationTypesLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_SYSTEM_NOTIFICATION_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetCurrencyTypeLookupList() As DataView

        Dim dv As DataView = DataView(LK_CURRENCY_TYPES)

        Return (dv)

    End Function
    Public Shared Function GetValidateBankInfoLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_VALIDATE_BANK_INFO, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    Public Shared Function GetCancellationReasonLookupList(compId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_CANCELLATION_REASONS, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(compId))

        Return (dv)

    End Function

    Public Shared Function GetCancellationReasonDealerFileLookupList(compId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_CANCELLATION_REASONS_DEALER_FILE, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(compId))

        Return (dv)

    End Function

    Public Shared Function GetCancellationReasonWithCodeLookupList(compId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_CANCELLATION_REASONS_WITH_CODE, COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(compId))

        Return (dv)

    End Function

    Public Shared Function GetCancellationReasonByDealerIdLookupList(dealerId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_CANCELLATION_REASONS_BY_DEALER, COL_DEALER_ID_NAME, DALBase.GuidToSQLString(dealerId))

        Return (dv)

    End Function

    Public Shared Function GetRegionLookupList() As DataView

        Dim dv As DataView = DataView(LK_REGIONS)

        Return (dv)

    End Function

    Public Shared Function GetRegionLookupList(countryId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_COUNTRY_REGIONS, COL_COUNTRY_ID_NAME, DALBase.GuidToSQLString(countryId))

        Return (dv)

    End Function

    Public Shared Function GetRegionLookupList(userCompanies As ArrayList) As DataView

        Dim userCountries As ArrayList = Country.GetCountries(userCompanies)

        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_COUNTRY_ID_NAME, userCountries)


        Dim dv As DataView = DataView(LK_COUNTRY_REGIONS)

        dv.RowFilter = sFilterCondition

        Return (dv)

    End Function

    Public Shared Function GetRegionTaxLookupList(regionId As Guid, taxTypeId As Guid) As DataView

        Dim regionIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        regionIdList.Add(regionId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_REGION_ID_NAME, regionIdList)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_TAX_TYPE_ID_NAME, taxTypeId)}

        Dim dv As DataView = FilteredParamView(LK_REGION_TAX, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetInvoiceMethodLookupList() As DataView

        Dim dv As DataView = DataView(LK_INVOICE_METHOD)

        Return (dv)

    End Function

    Public Shared Function GetFtpSiteLookupList() As DataView

        Dim dv As DataView = DataView(LK_FTP_SITE)

        Return (dv)

    End Function

    Public Shared Function GetTaxTypesLookupList() As DataView

        Dim dv As DataView = DataView(LK_TAX_TYPES)

        Return (dv)

    End Function

    Public Shared Function GetExternalUserTypesLookupList() As DataView

        Dim dv As DataView = DataView(LK_EXTERNAL_USER_TYPES)

        Return (dv)

    End Function

    Public Shared Function GetIHQRolesLookupList() As DataView

        Dim dv As DataView = DataView(LK_IHQ_ROLES)

        Return (dv)

    End Function

    '9/14/2006 - ALR - Added for the user definitions report
    Public Shared Function GetRolesLookupList() As DataView

        Dim dv As DataView = DataView(LK_ROLES_ALL)

        Return (dv)

    End Function

    Public Shared Function GetPurposeList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CASE_PURPOSE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)

    End Function


    Public Shared Function GetRelationshipList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_RELATION_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)

    End Function

    Public Shared Function GetTabsLookupList() As DataView

        Dim dv As DataView = DataView(LK_TABS_ALL)

        Return (dv)

    End Function

    Public Shared Function GetReportFormLookupList(userId As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_USER_ID_NAME, userId),
             New DBHelper.DBHelperParameter(COL_USER_ID_NAME, userId)}

        Dim dv As DataView = RetrieveParamList(LK_REPORT, params)

        Return (dv)

    End Function

    Public Shared Function GetMaritalStatusLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_MARITALSTATUS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetNationalityLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_NATIONALITY, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetPlaceOfBirthLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_PLACEOFBIRTH, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetGenderLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_GENDER, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetPersonTypeLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_PERSON_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetUpgradeTermUnitOfMeasureLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_UPGRADE_TERM_UNIT_OF_MEASURE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetUpgFinanceInfoRequireLookupList(languageId As Guid) As DataView
        Return DropdownLookupList(LK_UPG_FINANCE_INFO_REQUIRE, languageId)
    End Function
#End Region

#Region "User Related LookupLists"

    Public Shared Function GetUserCompaniesLookupList() As DataView
        Dim dv As DataView = FilteredView(LK_USER_COMPANIES, COL_USER_ID_NAME, DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.Id))

        Return (dv)

    End Function


    Public Shared Function GetUserCompanyGroupList() As DataView
        Dim dv As DataView = FilteredView(LK_USER_COMPANY_GROUPS, COL_USER_ID_NAME, DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.Id))

        Return (dv)

    End Function

    Public Shared Function GetUserCountriesLookupList() As DataView
        Dim dv As DataView = FilteredView(LK_USER_COUNTRIES, COL_USER_ID_NAME, DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.Id))
        Return (dv)
    End Function

    Public Shared Function GetUserCountriesLookupList(userCompanies As ArrayList) As DataView
        Dim dv As DataView = FilteredViewWithDynamicInClause(LK_USER_COUNTRIES_FOR_ALL_COMPANIES, userCompanies, COL_USER_ID_NAME, DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.Id))
        Return (dv)

    End Function
#End Region

#Region "Parameter LookupList"

    Public Shared Function GetSplitSystemTranslationsLookupList(companyIds As ArrayList, oInterfaceCode As String,
            olangCode As String, oListCode As String) As DataView
        '   Dim params(2) As DBHelper.DBHelperParameter
        Dim sFilterCondition As String
        Dim dv As DataView

        sFilterCondition = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("interface_code", oInterfaceCode),
             New DBHelper.DBHelperParameter("language_code", olangCode),
             New DBHelper.DBHelperParameter("list_code", oListCode)}

        dv = FilteredParamView(LK_SPLIT_SYSTEM_TRANSLATIONS, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetSplitSystemLookupList(companyIds As ArrayList, oInterfaceCode As String) As DataView
        Dim sFilterCondition As String
        Dim dv As DataView

        sFilterCondition = MiscUtil.BuildListForNetSql(COL_COMPANY_ID_NAME, companyIds)
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("interface_code", oInterfaceCode)}

        dv = FilteredParamView(LK_SPLIT_SYSTEM_CODE, sFilterCondition, params)

        Return (dv)

    End Function

#End Region

#Region "Dropdown LookupList"


    Public Shared Function AllDropdownLanguageLookupLists(languageId As Guid) As DataView
        'Generic lookup list 
        'Returns all the lists
        Dim dv As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray)}

        dv = FilteredParamView(LK_ALL_LANGUAGE_LISTS, String.Empty, params)

        Return (dv)

    End Function

    Public Shared Function DropdownLanguageLookupList(DropdownCode As String, languageId As Guid) As DataView
        'Generic lookup list 
        'Returns all the lists
        Dim dv As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("code", DropdownCode),
                New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray)
            }

        dv = FilteredParamView(LK_LIST_LANGUAGE_ITEMS, String.Empty, params)

        Return (dv)

    End Function

    Public Shared Function DropdownLookupList(DropdownCode As String, languageId As Guid, Optional ByVal DisplayNothingSelected As Boolean = False) As DataView
        'Generic lookup list 
        'Returns a list of items for the specified lookup list code

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        'DALBase.GuidToSQLString(languageId)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("code", DropdownCode)}

        Dim dv As DataView = FilteredParamView(LK_LIST_ITEMS, sFilterCondition, params)

        Return (dv)

    End Function
    Public Shared Function DropdownLookupListByDisplayToUserOption(DropdownCode As String, languageId As Guid, Optional ByVal DisplayToUserOnly As Boolean = True) As DataView
        'Generic lookup list 
        'Returns a list of items for the specified lookup list code

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)
        If DisplayToUserOnly Then
            sFilterCondition = sFilterCondition & " and DISPLAY_TO_USER = 'Y'"
        End If
        'DALBase.GuidToSQLString(languageId)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("code", DropdownCode)}

        Dim dv As DataView = FilteredParamView(LK_LIST_ITEMS, sFilterCondition, params)

        Return (dv)

    End Function

    Public Shared Function GetDescrionFromListCode(Listcode As String, code As String) As String

        Dim i As Integer
        Dim data As DataView = DropdownLookupList(Listcode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        If data IsNot Nothing Then

            For i = 0 To data.Count - 1
                If data(i)("code").ToString = code Then
                    GetDescrionFromListCode = data(i)("description").ToString
                    Exit For
                End If
            Next
        End If

    End Function

    Public Shared Function GetListItemId(ListItemId As Guid, languageId As Guid, Optional ByVal DisplayNothingSelected As Boolean = False) As DataView
        'Generic lookup list 
        'Returns the list items record for the specified lookup list item id

        Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        languageIdList.Add(languageId)
        Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, languageIdList)

        'DALBase.GuidToSQLString(languageId)

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("id", ListItemId)}

        Dim dv As DataView = FilteredParamView(LK_LIST_ITEM_ID, sFilterCondition, params)

        Return (dv)

    End Function


    Public Shared Function GetListId(languageId As Guid, Optional ByVal DisplayNothingSelected As Boolean = False) As DataView
        'Generic lookup list 
        'Returns the list record 

        'Dim languageIdList As System.Collections.ArrayList = New System.Collections.ArrayList
        'languageIdList.Add(languageId)
        'Dim sFilterCondition As String = MiscUtil.BuildListForSql(COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId)


        'Dim dv As DataView = FilteredView(LK_LIST_ID, sFilterCondition)
        Dim dv As DataView = FilteredView(LK_LIST_ID, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetList(languageId As Guid, Optional ByVal DisplayNothingSelected As Boolean = False) As DataView

        Dim dv As DataView = FilteredView(LK_LIST, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetTMKStatusLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("TMKSTATUS", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetYesNoLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("YESNO", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetTaxTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("TTYP", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetPostMigConditionLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView
        Return DropdownLookupList(LK_POST_MIG_CONDITION, languageId, displayNothingSelected)
    End Function
    Public Shared Function GetReInsStatusLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_REINSURANCE_STATUSES, languageId, displayNothingSelected)

    End Function
    Public Shared Function GetReInsStatusesWithoutPartialStatuesLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_REINS_STATUSES_WITHOUT_PARTIAL_STATUSES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    Public Shared Function GetOpenClosedLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("CLM_STAGE_STATUS", languageId, displayNothingSelected)

    End Function
    Public Shared Function GetRefundComputeMethodLookupList(oLangId As Guid, Optional ByVal DisplayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("RMETH", oLangId, DisplayNothingSelected)

    End Function

    Public Shared Function GetRefundDestinationLookupList(oLangId As Guid, Optional ByVal DisplayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("REFDS", oLangId, DisplayNothingSelected)

    End Function

    Public Shared Function GetVscUploadsLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("VSCU", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetBillingCriteriaList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("BLCR", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetCancellationDependencyList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("CNLDEP", languageId, displayNothingSelected)

    End Function
    Public Shared Function GetCancellationLumpsumBillingList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("CNLSBL", languageId, displayNothingSelected)

    End Function
    Public Shared Function GetBillingFrequencyList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("BLFQ", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetBillingStatusList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("BLST", languageId, displayNothingSelected)



    End Function

    Public Shared Function GetRoleProviderList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(ROLE_PROVIDER, languageId, displayNothingSelected)



    End Function

    Public Shared Function GetBillingStatusListShort(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = GetBillingStatusList(languageId, displayNothingSelected)

        Dim sFilterCondition As String = " and code not in ('I','C')"

        dv.RowFilter = dv.RowFilter & sFilterCondition

        Return dv

    End Function

    Public Shared Function GetRejectCodesList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("REJCODE", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetClaimDeniedAuthorizationList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("CLADELETAUTHO", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetReportCeDestLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("RCEDEST", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetRecordTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("RECTYP", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetPaymentRecordTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("PYMTRECTYP", languageId, displayNothingSelected)

    End Function
    Public Shared Function GetInvoiceRecordTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("INVOICE_REC_TYP", languageId, displayNothingSelected)

    End Function
    Public Shared Function GetPymtRecordTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("PAYMENT_REC_TYP", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetReinsuranceRecordTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_REINS_REC_TYPE, languageId, displayNothingSelected)

    End Function

    'Added for Req-610
    Public Shared Function GetPhoneTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("PHNRTP", languageId, displayNothingSelected)

    End Function


    Public Shared Function GetMobileTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("MOB_TYPE", languageId, displayNothingSelected)

    End Function

    ''REQ-796
    Public Shared Function GetTransactionTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("CNTRTYPE", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetCostTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("CT", languageId, displayNothingSelected)

    End Function


    Public Shared Function GetAddressTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("ATYPE", languageId, displayNothingSelected)

    End Function
    Public Shared Function GetFinancedFrequency(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList("FINFREQ", languageId, displayNothingSelected)

    End Function

    Public Shared Function GetIssueReopenReasonLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList(LK_ISSUE_REOPEN_REASON, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetIssueWaiveReasonLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = False) As DataView

        Return DropdownLookupList(LK_ISSUE_WAIVE_REASON, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetPreInvoiceStatusList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PRE_INVOICE_STATUS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

#End Region

#Region "VSC Related lookups"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the makes for the company group
    ''' </summary>
    ''' <param name="CompanyGroupId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	4/26/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetPlanLookupList(companyGroupId As Guid) As DataView

        ' Dim dv As DataView = RetrieveList(LK_PLANS)
        Dim dv As DataView = FilteredView(LK_PLANS, COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(companyGroupId))
        Return (dv)

    End Function

    Public Shared Function GetVSCMakeLookupList(CompanyGroupId As Guid, Optional ByVal tableName As String = LK_VSC_MANUFACTURERS) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(CompanyGroupId))}

        Dim dv As DataView = FilteredParamView(tableName, Nothing, params)

        Return (dv)

    End Function

    'this list needs to change to be by CompanyGroupId. The table needs to include the CompanyGroupId
    Public Shared Function GetVSCCoverageLimitLookupList(CompanyGroupId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(CompanyGroupId))}

        FilteredParamView(LK_COVERAGE_LIMIT, Nothing, params)
        Dim dv As DataView = FilteredParamView(LK_COVERAGE_LIMIT, Nothing, params)
        Return (dv)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the model for the make
    ''' </summary>
    ''' <param name="ManufacturerId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	4/26/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetVSCModelsLookupList(ManufacturerId As String) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_MANUFACTURER_ID_NAME, ManufacturerId)}

        Dim dv As DataView = FilteredParamView(LK_VSC_MODELS, Nothing, params)

        Return (dv)

    End Function
    Public Shared Function GetVSCModelsLookupList(ManufacturerId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_MANUFACTURER_ID_NAME, DALBase.GuidToSQLString(ManufacturerId))}

        Dim dv As DataView = FilteredParamView(LK_VSC_MODELS, Nothing, params)

        Return (dv)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the versions/trims for the model
    ''' </summary>
    ''' <param name="Model"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	4/26/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetVSCTrimLookupList(Model As String, make As String) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_MODEL_NAME, Model),
                     New DBHelper.DBHelperParameter(COL_MANUFACTURER_ID_NAME, make)}

        Dim dv As DataView = FilteredParamView(LK_VSC_TRIMS, Nothing, params)

        Return (dv)

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the years for the version/trim
    ''' </summary>
    ''' <param name="modelId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	4/26/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetVSCYearsLookupList(trim As String, model As String, make As String) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_TRIM_NAME, trim),
                    New DBHelper.DBHelperParameter(COL_MODEL_NAME, model),
                     New DBHelper.DBHelperParameter(COL_MANUFACTURER_ID_NAME, make)}

        Dim dv As DataView = FilteredParamView(LK_VSC_YEARS, Nothing, params)

        Return (dv)

    End Function


    Public Shared Function GetVSCPlanLookupList(CompanyGroupId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(CompanyGroupId))}

        Dim dv As DataView = FilteredParamView(LK_VSC_PLAN_BY_COMPANY_GROUP_ID, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetVSCClassCodesLookupList(CompanyGroupId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(CompanyGroupId))}

        Dim dv As DataView = FilteredParamView(LK_VSC_CLASS_CODES_BY_COMPANY_GROUP, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetRiskGroupsList() As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.LanguageId))}

        Dim dv As DataView = FilteredParamView(LK_RISKGROUPS, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetRiskTypesList(CompanyGroupId As Guid, RiskGroupId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_COMPANY_GROUP_ID_NAME, DALBase.GuidToSQLString(CompanyGroupId)) _
                     , New DBHelper.DBHelperParameter(COL_RISK_GROUP_ID_NAME, DALBase.GuidToSQLString(RiskGroupId))}

        Dim dv As DataView = FilteredParamView(LK_RISK_TYPES, Nothing, params)

        Return (dv)

    End Function
    'Req-1194
    Public Shared Function GetCertRiskTypeLookupList(certificateId As Guid, dealerid As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_CERT_ID, DALBase.GuidToSQLString(certificateId)) _
                    , New DBHelper.DBHelperParameter(COL_DEALER_ID_NAME, DALBase.GuidToSQLString(dealerid))}

        Dim dv As DataView = FilteredParamView(LK_CERT_RISKTYPES, Nothing, params)
        Return (dv)
    End Function
    'End Req 1194
#End Region

#Region "Accounting Lookups"

    Public Shared Function getAccountingEvents(AccountingCompanyId As Guid, LanguageId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                         {New DBHelper.DBHelperParameter(DALBase.PAR_NAME_COMPANY_ID, DALBase.GuidToSQLString(AccountingCompanyId)) _
                          , New DBHelper.DBHelperParameter(TranslationDAL.COL_NAME_LANGUAGE_ID, DALBase.GuidToSQLString(LanguageId))}

        Dim dv As DataView = FilteredParamView(LK_ACCOUNTING_EVENTS, Nothing, params)

        Return (dv)
    End Function

    Public Shared Function getAccountingCompanies(UserId As Guid, Optional ByVal IncludeInactive As Boolean = False) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                         {New DBHelper.DBHelperParameter(UserDAL.COL_NAME_USER_ID, DALBase.GuidToSQLString(UserId))}

        Dim dv As DataView = FilteredParamView(LK_ACCOUNTING_COMPANIES, IIf(IncludeInactive, Nothing, AcctCompanyDAL.COL_NAME_USE_ACCOUNTING & "='Y'"), params)
        Return (dv)

    End Function
    'DEF 2624 : added extra optional paramter sFilterCondition for filtering out the file names list 
    Public Shared Function getAccountingFileNames(CompanyId As Guid, BeginDate As String, EndDate As String, Optional ByVal sFilterCondition As String = Nothing) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, DALBase.GuidToSQLString(CompanyId)) _
                        , New DBHelper.DBHelperParameter(COL_BEGIN_DATE, BeginDate) _
                        , New DBHelper.DBHelperParameter(COL_END_DATE, EndDate)}

        Dim dv As DataView = FilteredParamView(LK_ACCOUNTING_FILENAMES, sFilterCondition, params)
        Return (dv)

    End Function

    Public Shared Function getBusinessCoverageEntity(AcctCompanyId As Guid, languageID As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {New DBHelper.DBHelperParameter(COL_ACCT_COMPANY_ID_NAME, DALBase.GuidToSQLString(AcctCompanyId)) _
                       , New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageID)}

        Dim dv As DataView = FilteredParamView(LK_BUSINESS_ENTITY_COV, Nothing, params)
        Return (dv)

    End Function

#End Region

#Region "Comuna Lookup"
    Public Shared Function GetRegionAndComunaList(regionID As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                         {New DBHelper.DBHelperParameter(COL_REGION_ID_NAME, DALBase.GuidToSQLString(regionID))}
        Dim dv As DataView = FilteredParamView("COMUNA_CODE", Nothing, params)

        Return (dv)

    End Function
#End Region

#Region "Report PageCtrl"

    Public Shared Function Get_Report_RunDate_PageNum(RptName As String, CompanyId As Guid) As DataSet
        Dim ds As DataSet = Nothing
        Try
            ds = LookupListDALNew.GET_RPT_RUNDATE_PAGENUM(RptName, CompanyId)
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function ChkIfRptRunForDate(RptName As String, ReportRun_date As String, CompanyId As Guid) As Boolean

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_REPORT_NAME, RptName) _
                        , New DBHelper.DBHelperParameter(COL_REPORTRUNDATE, ReportRun_date) _
                        , New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, CompanyId)}

        Dim dv As DataView = FilteredParamView(LK_CHECK_REPORT_RUNDATE, Nothing, params)

        If (dv.Count > 0) Then
            If dv.Item(0)(COL_STATUS).ToString = "Running" Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

#End Region

#Region "Equipment LookupList"

    Public Shared Function GetEquipmentLookupList(companyGroupId As Guid, Optional ByVal manufacturerIdString As String = Nothing,
        Optional ByVal masterModel As BooleanType = Nothing, Optional ByVal equipmentTypeIdString As String = Nothing,
        Optional ByVal model As String = Nothing) As DataView

        Dim manufacturerId As Nullable(Of Guid)
        Dim equipmentTypeId As Nullable(Of Guid)
        If (String.IsNullOrEmpty(manufacturerIdString)) Then
            manufacturerId = Nothing
        Else
            manufacturerId = New Guid(manufacturerIdString)
        End If
        If (String.IsNullOrEmpty(equipmentTypeIdString)) Then
            equipmentTypeId = Nothing
        Else
            equipmentTypeId = New Guid(equipmentTypeIdString)
        End If

        Dim filterExpression As String
        filterExpression = COL_COMPANY_GROUP_ID_NAME & " = '" & DALBase.GuidToSQLString(companyGroupId) & "'"
        If (manufacturerId.HasValue) Then
            filterExpression &= " AND " & COL_MANUFACTURER_ID_NAME & " = '" & DALBase.GuidToSQLString(manufacturerId.Value) & "'"
        End If
        If (equipmentTypeId.HasValue) Then
            filterExpression &= " AND " & EquipmentDAL.COL_NAME_EQUIPMENT_TYPE_ID & " = '" & DALBase.GuidToSQLString(equipmentTypeId.Value) & "'"
        End If
        If (masterModel IsNot Nothing) Then
            Dim isMasterModel As Guid
            If (masterModel.Value) Then
                isMasterModel = GetIdFromCode(LK_YESNO, "Y")
            Else
                isMasterModel = GetIdFromCode(LK_YESNO, "N")
            End If
            filterExpression &= " AND " & COL_IS_MASTER_MODEL & " = '" & DALBase.GuidToSQLString(isMasterModel) & "'"
        End If
        If (model IsNot Nothing) Then
            If (Trim(model) <> String.Empty) Then
                filterExpression &= " AND " & COL_CODE_NAME & " = '" & model & "'"
            End If
        End If
        Dim dv As DataView = FilteredParamView(LK_EQUIPMENTS, filterExpression, New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("companyGroupId", companyGroupId.ToByteArray())})
        Return (dv)
    End Function

    Public Shared Function GetEquipmentByEquipmentList_Lookup(companyGroupId As Guid,
                                                              EquipmentList_code As String,
                                                              Effective_on_date As Date,
                                                              Optional ByVal manufacturerIdString As String = Nothing,
                                                              Optional ByVal masterModel As BooleanType = Nothing,
                                                              Optional ByVal equipmentTypeIdString As String = Nothing,
                                                              Optional ByVal model As String = Nothing) As DataView

        Dim manufacturerId As Nullable(Of Guid)
        Dim equipmentTypeId As Nullable(Of Guid)
        If (String.IsNullOrEmpty(manufacturerIdString)) Then
            manufacturerId = Nothing
        Else
            manufacturerId = New Guid(manufacturerIdString)
        End If
        If (String.IsNullOrEmpty(equipmentTypeIdString)) Then
            equipmentTypeId = Nothing
        Else
            equipmentTypeId = New Guid(equipmentTypeIdString)
        End If

        Dim filterExpression As String
        filterExpression = COL_COMPANY_GROUP_ID_NAME & " = '" & DALBase.GuidToSQLString(companyGroupId) & "'"
        If (manufacturerId.HasValue) Then
            filterExpression &= " AND " & COL_MANUFACTURER_ID_NAME & " = '" & DALBase.GuidToSQLString(manufacturerId.Value) & "'"
        End If
        If (equipmentTypeId.HasValue) Then
            filterExpression &= " AND " & EquipmentDAL.COL_NAME_EQUIPMENT_TYPE_ID & " = '" & DALBase.GuidToSQLString(equipmentTypeId.Value) & "'"
        End If
        If (masterModel IsNot Nothing) Then
            Dim isMasterModel As Guid
            If (masterModel.Value) Then
                isMasterModel = GetIdFromCode(LK_YESNO, "Y")
            Else
                isMasterModel = GetIdFromCode(LK_YESNO, "N")
            End If
            filterExpression &= " AND " & COL_IS_MASTER_MODEL & " = '" & DALBase.GuidToSQLString(isMasterModel) & "'"
        End If
        If (model IsNot Nothing) Then
            If (Trim(model) <> String.Empty) Then
                filterExpression &= " AND " & COL_CODE_NAME & " = '" & model & "'"
            End If
        End If
        Dim dv As DataView = FilteredParamView(LK_EQUIPMENT_BY_EQUIPMENTlIST, filterExpression, New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter("companyGroupId", companyGroupId.ToByteArray()),
             New DBHelper.DBHelperParameter("Equipment_list_code", EquipmentList_code),
             New DBHelper.DBHelperParameter("eff_date", Effective_on_date)})
        Return (dv)
    End Function

    Public Shared Function GetManufacturerbyEquipmentList(EquipmentCode As String, Effective_on As Date) As DataView
        Dim dv As DataView = FilteredParamView(LK_MANUFACTURER_BY_EQUIPMENT_LIST, "",
                         New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("list_code", EquipmentCode),
                         New DBHelper.DBHelperParameter("effective_on", Effective_on)})
        Return (dv)
    End Function

    Public Shared Function GetComputeDeductibleBasedOnLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_DEDUCTIBLE_BASED_ON, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetClaimLoadFileTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_CLAIM_LOAD_FILE_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetWorkQueueAction(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_WQ_ACTION, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetEquipmentClassLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_EQUIPMENT_CLASS, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetEquipmentTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_EQUIPMENT_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetImageTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_IMAGE_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetClaimEquipmentTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_CLAIM_EQUIPMENT_TYPE, languageId, displayNothingSelected)

    End Function



#End Region

    Public Shared Function GetEventTaskStatus(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView


        Return DropdownLookupList(TASK_STATUS, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetAttributeDataTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_ATTRIBUTE_DATA_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetBestReplacementLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_BEST_REPLACEMENT, Nothing)

        Return (dv)

    End Function
    Public Shared Function GetEquipmentListLookupList(LongDate As DateTime) As DataView
        'to fix def 1997 added a data parameter

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
               {New DBHelper.DBHelperParameter("long_date", LongDate.ToString("MM/dd/yyyy hh:mm:ss"))}

        Dim dv As DataView = FilteredParamView(LK_EQUIPMENT_LIST, String.Empty, params)

        Return (dv)


    End Function

    Public Shared Function GetEquipmentMakeAndModel(equipmentId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
               {New DBHelper.DBHelperParameter("equipment_id", equipmentId.ToByteArray)}

        Dim dv As DataView = FilteredParamView(LK_EQUIPMENT_MAKE_MODEL, String.Empty, params)

        Return (dv)


    End Function

    Public Shared Function GetEquipmentListLookupListforPriceList(company_group_id As Guid, LongDate As DateTime) As DataView
        'to fix def 1997 added a data parameter

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                   {New DBHelper.DBHelperParameter("company_group_id", company_group_id.ToByteArray()),
                    New DBHelper.DBHelperParameter("long_date", LongDate.ToString("MM/dd/yyyy hh:mm:ss"))}

        Dim dv As DataView = FilteredParamView(LK_EQUIPMENT_LIST_FOR_PRICE_LIST, String.Empty, params)
        Return (dv)
    End Function

    Public Shared Function GetBestReplacementLookupList(companyGroupId As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                   {New DBHelper.DBHelperParameter("company_group_id", companyGroupId.ToByteArray)}

        Dim dv As DataView = FilteredParamView(LK_BEST_REPLACEMENT_BY_COMPANY_GROUP, String.Empty, params)

        Return (dv)

    End Function

    Public Shared Function GetIssuesListLookupList(LongDate As DateTime) As DataView
        'to fix def 1997 added a data parameter

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}

        Dim dv As DataView = FilteredParamView(LK_ISSUES, String.Empty, params)

        Return (dv)


    End Function

#Region "Questions and Question List lookup lists"
    Public Shared Function GetQuestionListLookupList(LongDate As DateTime) As DataView
        'REQ-860 - ELita Buildout - Issues/Adjudication

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                   {New DBHelper.DBHelperParameter("long_date", LongDate.ToString("MM/dd/yyyy hh:mm:ss"))}

        Dim dv As DataView = FilteredParamView(LK_QUESTION_LIST, String.Empty, params)

        Return (dv)

    End Function


    Public Shared Function GetIssueCommentTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView
        Return DropdownLookupList("ICTYP", languageId, displayNothingSelected)
    End Function

    Public Shared Function GetIssueTypeLookupList(langId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}
        Dim dv As DataView = FilteredParamView(LK_ISSUE_TYPE_LIST, String.Empty, params)
        Return (dv)
    End Function

    Public Shared Function GetIssueTypeCodeFromId(listName As String, id As Guid) As String
        Dim dv As DataView = DataView(listName)
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then
                Return dv(i)(COL_CODE_NAME).ToString
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function GetIssueTypeIdFromCode(listName As String, code As String) As Guid
        Dim dv As DataView = DataView(listName)
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If dv(i)(COL_CODE_NAME).ToString.Equals(code) Then
                Return New Guid(CType(dv(i)(COL_ID_NAME), Byte()))
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function GetNoteTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}
        Dim dv As DataView = FilteredParamView(LK_NOTE_TYPE_LIST, String.Empty, params)
        Return (dv)
    End Function

    Public Shared Function GetQuestionTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal ShowDiag As Boolean = False) As DataView

        Dim CONST_DIAG As String = "DIAG"

        Dim dv As DataView = DropdownLookupList(LK_QUESTION_TYPE, languageId, displayNothingSelected)

        If Not ShowDiag Then
            dv.RowFilter &= " AND CODE <> '" & CONST_DIAG & "'"
        End If

        Return dv

    End Function

    Public Shared Function GetAnswerTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_ANSWER_TYPE, Nothing)
        Return (dv)

    End Function

    Public Shared Function GetQuestionsLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_QUESTION, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetAnswerLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_ANSWER, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetRuleTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_RULE_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetRuleCategoryLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_RULE_CATEGORY, languageId, displayNothingSelected)

    End Function


    Public Shared Function GetEntityAttributeList(todayDate As DateTime, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter("todayDate", todayDate.ToString("MM/dd/yyyy hh:mm:ss"))}

        Dim dv As DataView = FilteredParamView(LK_ENTITY_ATTRIBUTE, String.Empty, params)

        Return (dv)

    End Function

#End Region


    'Public Shared Function GetIssueTypeLookupList(ByVal languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView
    '    Return DropdownLookupList(LK_ISSUE_TYPE, languageId, displayNothingSelected)
    'End Function

    'Public Shared Function GetNoteTypeLookupList(ByVal languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView
    '    Return DropdownLookupList(LK_NOTE_TYPE, languageId, displayNothingSelected)
    'End Function

    Public Shared Function LoadRiskTypes(certificateId As Guid, languageId As Guid, dateOfLoss As Date) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_CERT_ID_NAME, certificateId.ToByteArray),
                                     New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageId.ToByteArray),
                                     New DBHelper.DBHelperParameter(COL_LOSS_DATE, dateOfLoss)}

        Dim dv As DataView = FilteredParamView(LK_RISK_TYPE, String.Empty, params)
        Return (dv)
    End Function

    Public Shared Function LoadCoverageTypes(certificateId As Guid, ItemId As Guid, languageId As Guid, dateOfLoss As Date) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_CERT_ID_NAME, certificateId.ToByteArray),
                                     New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageId.ToByteArray),
                                     New DBHelper.DBHelperParameter(COL_LOSS_DATE, dateOfLoss),
                                     New DBHelper.DBHelperParameter(COL_CERT_ITEM_ID_NAME, ItemId.ToByteArray)}

        Dim dv As DataView = FilteredParamView(LK_COVERAGE_TYPE, String.Empty, params)
        Return (dv)
    End Function

    Public Shared Function GetPriceList(CountryId As Guid) As DataView
        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(COL_COUNTRY_ID_NAME, CountryId.ToByteArray)}

        Dim dv As DataView = FilteredParamView(LK_PRICE_LIST, String.Empty, params)
        Return (dv)
    End Function

#Region "Lookup list for Price List / Service Center"

    Public Shared Function GetServiceClassLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_SERVICE_CLASS, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetServiceTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_SERVICE_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetServiceLevelLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_SERVICE_LEVEL, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetNewServiceTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_SERVICE_TYPE_NEW, languageId, displayNothingSelected)

    End Function
    Public Shared Function GetNewServiceTypeByServiceClassLookupList(languageId As Guid, ServiceClassID As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray),
        New DBHelper.DBHelperParameter("service_class_id", ServiceClassID.ToByteArray)}

        Dim dv As DataView = FilteredParamView(LK_SERVICE_TYPE_BY_SERVICE_CLASS_NEW, "", parameters)
        Return (dv)

    End Function



    Public Shared Function GetDistributionMethodLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_DIST_METHOD, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetManageInventoryLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_MANAGE_INVENTORY, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetDayOfWeekLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_DAYS_OF_WEEK, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetAFAProductTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_AFA_PRODUCT_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetPaymentStatusLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_PAYMENT_GRP_STAT, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetClaimCloseRuleBasedOnList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_CLAIM_RULSE_BASED_ON, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetDateOfPaymentOPtionsList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_DATE_OF_PAYMENT_OPTION, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetNumberOfDigitsRoundOffList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_NUMBER_OF_DIGITS_ROUNDOFF, languageId, displayNothingSelected)

    End Function
    Public Shared Function GetBonusComputationMethodList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_BONUS_COMPUTATION_METHOD, languageId, displayNothingSelected)

    End Function

#End Region

#Region "VendorManagement"

    Public Shared Function GetServiceClassList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_SERVICE_CLASS, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetServiceTypeList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_SERVICE_TYPE, languageId, displayNothingSelected)

    End Function

    Public Shared Function GetScheduleList() As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {} '_
        Dim dv As DataView = FilteredParamView(LK_SCHEDULE, String.Empty, params)
        Return (dv)

    End Function

    Public Shared Function GetConditionLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_CONDITION, languageId, displayNothingSelected)

    End Function

#End Region

#Region "ClaimsProcess"
    Public Shared Function GetClaimStatusFromCode(languageId As Guid, code As String) As String
        Dim desc As String = Nothing
        Dim dv As DataView = FilteredView(LK_CLAIM_STATUS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        dv.Sort = "code"
        Dim idx As Integer = dv.Find(code)
        If (idx >= 0) Then desc = dv(idx)(COL_DESCRIPTION_NAME)
        dv.Sort = ""

        Return desc
    End Function
#End Region

    Public Shared Function GetTranslatedQuestionFromCode(languageId As Guid, code As String) As String
        Dim desc As String = Nothing
        Dim dv As DataView = FilteredView(LK_QUESTION, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        dv.Sort = "code"
        Dim idx As Integer = dv.Find(code)
        If (idx >= 0) Then desc = dv(idx)(COL_DESCRIPTION_NAME)
        dv.Sort = ""

        Return desc
    End Function

    Public Shared Function GetFieldNames() As DataView
        Dim dv As DataView = LookupListDALNew.Load(LK_FIELD_NAMES)
        Return dv
    End Function


    Public Shared Function GetStagesByGroupLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_STAGES, Nothing)

        Return (dv)

    End Function

    Public Shared Function GetRejectionMsgTypesLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_MSG_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetFileTypeLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_FILE_TYPE, languageId, displayNothingSelected)

    End Function
    Public Shared Function GetCommissionEntityTypeIdLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList(LK_COMM_ENTITY_TYPE_ID, languageId, displayNothingSelected)

    End Function
    Public Shared Function GetProdRewardNameLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PROD_REWARD_NAMES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function
    Public Shared Function GetProdRewardTypesLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_PROD_REWARD_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function
    Public Shared Function GetDeviceGroupsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_DEVICE_GROUPS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function
    Public Shared Function GetDeviceTypesLookupList(DeviceTypeCode As String, languageId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
       {New DBHelper.DBHelperParameter(COL_DEVICE_TYPES, DeviceTypeCode),
        New DBHelper.DBHelperParameter(COL_LANGUAGE_ID_NAME, languageId)}

        Dim dv As DataView = FilteredParamView(LK_DEVICE_TYPES, Nothing, params)

        Return (dv)

    End Function
    Public Shared Function GetCaseSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CASE_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetRewardSearchFieldsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_REWARD_SEARCH_FIELDS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCaseStatusLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CASE_STATUS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    Public Shared Function GetCaseCloseReasonLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CASE_CLOSE_REASON, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCasePurposeLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_CASE_PURPOSE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function
    Public Shared Function GetDcmQuestionSetLookupList() As DataView

        Dim dv As DataView = FilteredView(LK_DCM_QUESTION_SET, Nothing)

        Return (dv)

    End Function

    'REQ-6289
    Public Shared Function GetProdLimitAppToXCDList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_PRODUCT_LIMIT_APP_TO_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)


        Return (dv)

    End Function
    'REQ-6289-END
    Public Shared Function GetUpdateReplaceRegisteredItemsLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_UPDATE_REPLACE__REG_ITEMS, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)

        Return (dv)

    End Function

    'REQ-6313
    Public Shared Function GetParamValueSourceLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_PARAM_VALUE_SOURCE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetParamDataTypeLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_PARAM_DATA_TYPE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetYesNoXcdList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_YESNO_XCD, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function

    Public Shared Function GetRecipientSourceFieldLookupList(languageId As Guid) As DataView
        Dim dv As DataView = FilteredView(LK_RECIPIENT_SOURCE_FIELD, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), False)
        Return (dv)
    End Function
    'END-REQ-6313
    
    Public Shared Function GetUnitOfMeasureList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_UNIT_OF_MEASURE, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)


        Return (dv)

    End Function
    Public Shared Function GetBenefitTaxTypes(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim dv As DataView = FilteredView(LK_BENEFIT_TAX_TYPES, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId), displayNothingSelected)


        Return (dv)

    End Function

    'REQ-6230
    Public Shared Function GetIndixProductsSortByLookupList(languageId As Guid) As DataView

        Dim dv As DataView = FilteredView(LK_RETAIL_PRICE_SEARCH_SORTBY_LIST, COL_LANGUAGE_ID_NAME, DALBase.GuidToSQLString(languageId))

        Return (dv)

    End Function

    Public Shared Function GetCompanyGroupLookupList(oCompanyId As Guid) As DataView

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
    {New DBHelper.DBHelperParameter(COL_COMPANY_ID_NAME, oCompanyId)}

        Dim dv As DataView = FilteredParamView(LK_GET_COMPANY_GROUPS, Nothing, params)

        Return (dv)

    End Function

    Public Shared Function GetPayDeductLookupList(languageId As Guid, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Return DropdownLookupList("CLAIM_PAY_DEDUCTIBLE", languageId, displayNothingSelected)

    End Function
End Class

Imports Assurant.ElitaPlus.Common.LookupListCache

Public Class DalLookupTable

#Region "Constants"

    Private Const DAL_ITEM_PREFIX As String = "Assurant.ElitaPlus.DALObjects."
    Private Const DEALER_DAL_NAME = "DealerDAL"
    Private Const COMPANY_DAL_NAME = "CompanyDAL"
    Private Const ITEM_DAL_NAME = "ItemDAL"
    Private Const PRODUCT_CODE_DAL_NAME = "ProductCodeDAL"
    Private Const RISK_TYPE_DAL_NAME = "RiskTypeDAL"
    Private Const DEALER_GROUP_DAL_NAME = "DealerGroupDAL"
    Private Const USER_DAL_NAME = "UserDAL"
    Private Const EARNING_CODE_DAL_NAME = "EarningCodeDAL"
    Private Const CANCELLATION_REASON_DAL_NAME = "CancellationReasonDAL"
    Private Const SERVICE_CENTER_DAL_NAME = "ServiceCenterDAL"
    Private Const ROUTE_DAL_NAME = "RouteDAL"




#End Region

#Region "Private Attributes"

    Private DalLookupTable As Hashtable

#End Region

#Region "Private Methods"

    Private Sub addLookupList(ByVal DALName As String, ByVal LookuplistName As String)

        Dim fulDALName = DAL_ITEM_PREFIX & DALName
        Dim lkList As ArrayList = Me.GetLookupList(fulDALName)

        If lkList Is Nothing Then
            ' First item in the list
            lkList = New ArrayList
            lkList.Add(LookuplistName)
            DalLookupTable.Add(fulDALName, lkList)
        Else
            lkList.Add(LookuplistName)
        End If

    End Sub

#End Region

#Region "Constructors"

    Public Sub New()

        DalLookupTable = New Hashtable

        addLookupList(DEALER_DAL_NAME, LK_DEALERS)
        addLookupList(DEALER_DAL_NAME, LK_DEALERS_DUAL_COLUMNS)
        addLookupList(COMPANY_DAL_NAME, LK_COMPANIES)
        addLookupList(ITEM_DAL_NAME, LK_ITEM_RISKTYPE)
        addLookupList(ITEM_DAL_NAME, LK_RISK_PRODUCTCODE)
        addLookupList(PRODUCT_CODE_DAL_NAME, LK_PRODUCTCODE)
        addLookupList(PRODUCT_CODE_DAL_NAME, LK_PRODUCTCODE_BY_COMPANY)
        addLookupList(RISK_TYPE_DAL_NAME, LK_RISKTYPES)
        addLookupList(DEALER_GROUP_DAL_NAME, LK_DEALER_GROUPS)
        addLookupList(USER_DAL_NAME, LK_USERS)
        addLookupList(EARNING_CODE_DAL_NAME, LK_EARNING_CODES)
        addLookupList(ROUTE_DAL_NAME, LK_ROUTE)

        'ALR - Added for the Product Code COnversion
        addLookupList(DEALER_DAL_NAME, LK_DEALERS_PROD_CONV_DUAL_COLUMNS)
        addLookupList(DEALER_DAL_NAME, LK_DEALERS_PROD_CONV)

        'Cancellation Reason dropdown list
        addLookupList(CANCELLATION_REASON_DAL_NAME, LK_CANCELLATION_REASONS_BY_DEALER)

        addLookupList(SERVICE_CENTER_DAL_NAME, LK_SERVICE_CENTERS)

    End Sub

#End Region

#Region "Class Methods"

    Public Function GetLookupList(ByVal DALName As String) As ArrayList
        'Returns a list of LookupLists associated to the given DAL

        Return Me.DalLookupTable(DALName)

    End Function

    Public Function ContainsList(ByVal LookupListName As String) As Boolean
        ' Returns true if Lookuplist is associated to any DAL, false otherwise

        Dim entry As Object
        For Each entry In Me.DalLookupTable
            If entry.Value.Contains(LookupListName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function ContainsDal(ByVal DalName As String) As Boolean
        'Returns true if there is an entry for the given Dal

        Return Me.DalLookupTable.ContainsKey(DalName)

    End Function

#End Region

End Class

Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports System.Xml.Xsl

Partial Class LocateServiceCenterForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Private Const NO_DATA As String = " - "
    Public Const URL As String = "~\Certificates\LocateServiceCenterForm.aspx"
    Public Const GRID_COL_DETAIL_IDX As Integer = 0
    Public Const GRID_COL_RANK_IDX As Integer = 1
    Public Const GRID_COL_NAME_IDX As Integer = 2
    Public Const GRID_COL_ADDRESS_IDX As Integer = 3
    Public Const GRID_COL_CITY_IDX As Integer = 4
    Public Const GRID_COL_ZIP_IDX As Integer = 5
    Public Const GRID_COL_ID_IDX As Integer = 6
    Public Const GRID_COL_CODE_IDX As Integer = 7
    Public Const GRID_COL_EDIT_IDX As Integer = 8
    Public Const GRID_COL_MFG_AUTH_IDX As Integer = 9
    Public Const GRID_COL_PREF_IDX As Integer = 10

    Public Const SEARCH_BY_ZIP As String = "BY_ZIP"
    Public Const SEARCH_BY_CITY As String = "BY_CITY"
    Public Const SEARCH_ALL As String = "ALL"

    Private Const LABEL_SERVICE_CENTER As String = "SERVICE_CENTER"
#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public DealerId As Guid
        Public ZipLocator As String
        Public RiskTypeId As Guid
        Public ManufacturerId As Guid
        Public ShowAcceptButton As Boolean = True
        Public CovTypeCode As String
        Public CertItemCoverageId As Guid
        Public ClaimId As Guid
        Public WhenAcceptGoToCreateClaim As Boolean
        Public ComingFromDenyClaim As Boolean = False
        Public RecoveryButtonClick As Boolean = False
        Public ClaimedModel As String
        Public ClaimedManufacturerId As Guid
        Public ClaimedEquipment As ClaimEquipment
        'Def-23747- Added new variable country Id .
        Public CountryId As Guid

        Public Sub New(ByVal dealerId As Guid, ByVal zipLocator As String,
                        ByVal riskTypeId As Guid, ByVal ManufacturerId As Guid,
                        ByVal covTypeCode As String,
                        ByVal certItemCoverageId As Guid,
                        ByVal ClaimId As Guid,
                        Optional ByVal showAcceptButton As Boolean = True,
                        Optional ByVal whenAcceptGoToCreateClaim As Boolean = True,
                        Optional ByVal ComingFromDenyClaim As Boolean = False,
                        Optional ByVal RecoveryButtonClick As Boolean = False,
                        Optional ByVal _claimed_equipment As ClaimEquipment = Nothing,
                        Optional ByVal countryId As Guid = Nothing)
            Me.DealerId = dealerId
            Me.ZipLocator = zipLocator
            Me.RiskTypeId = riskTypeId
            Me.ManufacturerId = ManufacturerId
            Me.CovTypeCode = covTypeCode
            Me.ShowAcceptButton = showAcceptButton
            Me.WhenAcceptGoToCreateClaim = whenAcceptGoToCreateClaim
            Me.CertItemCoverageId = certItemCoverageId
            Me.ClaimId = ClaimId
            Me.ComingFromDenyClaim = ComingFromDenyClaim
            Me.RecoveryButtonClick = RecoveryButtonClick
            Me.ClaimedEquipment = _claimed_equipment
            Me.CountryId = countryId
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    'Same as Detail Page Return Parameters 

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Public Enum SearchType
        ByZip
        ByCity
        All
        None
    End Enum


    Class MyState
        Public PageIndex As Integer = 0
        Public SelectedServiceCenterId As Guid = Guid.Empty
        Public inputParameters As Parameters
        Public curSearchType As SearchType = SearchType.ByZip
        Public searchCity As String = ""
        Public searchResult As DataView
        Public default_service_center_id As Guid
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Me.State.inputParameters = CType(Me.NavController.ParametersPassed, Parameters)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.RestoreGUIState()
            Me.IsReturningFromChild = True
            Dim retObj As LocateServiceCenterDetailForm.ReturnType = CType(ReturnPar, LocateServiceCenterDetailForm.ReturnType)

            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
            End Select

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(Me.CallingParameters, Parameters)
                Me.State.inputParameters = pageParameters
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Sub SaveGUIState()
        Me.State.searchCity = Me.TextboxCity.Text
    End Sub

    Sub RestoreGUIState()
        Me.TextboxCity.Text = Me.State.searchCity
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property ProtectionAndEventDetailsControl() As ProtectionAndEventDetails
        Get
            If moProtectionAndEventDetails Is Nothing Then
                moProtectionAndEventDetails = CType(FindControl("moProtectionAndEventDetails"), ProtectionAndEventDetails)
            End If
            Return moProtectionAndEventDetails
        End Get
    End Property
#End Region

#Region "Page_Events"
    Private Sub PopulateProtectionAndEventDetail()
        Dim cssClassName As String
        Dim dateOfLoss As Date
        Dim certItemCvg As CertItemCoverage
        Dim certItem As CertItem
        Try
            'Def-23747-Added condition to check FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID value.
            'If null then get it from inputParameters.
            If Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) IsNot Nothing Then
                certItemCvg = New CertItemCoverage(CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID), Guid))
            Else
                certItemCvg = New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)
            End If


            certItem = New CertItem(certItemCvg.CertItemId)
            Dim cert As New Certificate(certItemCvg.CertId)
            dateOfLoss = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), Date)
            moProtectionAndEventDetails.CustomerName = cert.CustomerName
            If Not certItem.ManufacturerId.Equals(Guid.Empty) Then
                moProtectionAndEventDetails.EnrolledMake = New Manufacturer(certItem.ManufacturerId).Description
            End If
            moProtectionAndEventDetails.ClaimNumber = NO_DATA
            moProtectionAndEventDetails.DealerName = cert.getDealerDescription
            moProtectionAndEventDetails.EnrolledModel = certItem.Model
            moProtectionAndEventDetails.ClaimStatus = NO_DATA
            moProtectionAndEventDetails.CallerName = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME), String)

            If dateOfLoss > Date.MinValue Then moProtectionAndEventDetails.DateOfLoss = dateOfLoss.ToString(Me.DATE_FORMAT)
            moProtectionAndEventDetails.ProtectionStatus = LookupListNew.GetDescriptionFromId("SUBSTAT", cert.SubscriberStatus)
            If (LookupListNew.GetCodeFromId("SUBSTAT", cert.SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, New Dealer(Me.State.inputParameters.DealerId).UseEquipmentId) = Codes.YESNO_Y Then
                If Not Me.State.inputParameters.ClaimedEquipment Is Nothing Then
                    With Me.State.inputParameters.ClaimedEquipment
                        moProtectionAndEventDetails.ClaimedModel = .Model
                        moProtectionAndEventDetails.ClaimedMake = .Manufacturer
                    End With
                Else

                    moProtectionAndEventDetails.ClaimedModel = NO_DATA
                    moProtectionAndEventDetails.ClaimedMake = NO_DATA
                End If
            End If
            moProtectionAndEventDetails.ProtectionStatusCss = cssClassName
            moProtectionAndEventDetails.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, certItem.RiskTypeId)
            moProtectionAndEventDetails.DateOfLoss = dateOfLoss.ToString(Me.DATE_FORMAT)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        If (Me.NavController.CurrentFlow.Name = "CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM") Then
            lnkCancel.Visible = False
            divSteps.Visible = False
            moProtectionAndEventDetails.Visible = False
            searchServiceCenterH2.Visible = False
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("LOCATE SERVICE CENTER")
        Else
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PROTECTION_AND_EVENT_DETAILS")
        End If

        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Locate_Service_Center")

        PopulateProtectionAndEventDetail()
        Dim certid As Guid
        certid = (New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)).CertId
        UpdateBreadCrum(certid)

        Me.MasterPage.MessageController.Clear()
        Try
            If (Not Me.IsPostBack) Then
                PopulateCurrentSearchType()
                PopulateCountryDropdown()
                EnableDisableFields()
                Me.PopulateGridOrDropDown()
                'REQ-5546
                ControlMgr.SetVisibleControl(Me, RadioButtonNO_SVC_OPTION, Not Me.State.default_service_center_id.Equals(Guid.Empty))

                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_CANCEL")
                btnModalCancelYes.Attributes.Add("onclick", String.Format("ExecuteButtonClick('{0}');", lnkCancel.UniqueID))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub ShowGridData(ByVal dv As DataView)
        Try
            Dim dataSet As New DataSet
            selectedServiceCenterId.Value = "XXXX"
            Dim xdoc As New System.Xml.XmlDocument
            dataSet.Tables.Add(dv.ToTable())
            xdoc.LoadXml(dataSet.GetXml())
            xmlSource.TransformSource = HttpContext.Current.Server.MapPath("~/Certificates/LocateServiceCenter.xslt")
            xmlSource.TransformArgumentList = New XsltArgumentList
            xmlSource.TransformArgumentList.AddParam("recordCount", String.Empty, 30)
            xmlSource.Document = xdoc
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function IsUserCompaniesWithAcctSettings() As Boolean
        Dim objAC As AcctCompany
        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 0 OrElse (ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 1 AndAlso ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).IsNew = True) Then
            Return False
        Else
            For Each objAC In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies()
                'ALR - Added check to only return true if we are using accounting AND Felita..  
                'If using smartstream, allow all applicable service centers
                Dim certItemCvg As New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)
                Dim cert As New Certificate(certItemCvg.CertId)
                Dim certCompany As New Company(cert.CompanyId)
                If certCompany.AcctCompanyId.Equals(objAC.Id) Then
                    If objAC.UseAccounting = "Y" AndAlso objAC.AcctSystemId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), FelitaEngine.FELITA_PREFIX) Then Return True
                End If
            Next
            Return False
        End If

    End Function

    Private Sub PopulateGrid(ByRef dv As DataView)
        Dim MethodOfRepairType As String
        Dim certItemCov As New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)
        Dim cert As New Certificate(certItemCov.CertId)
        Dim dvCount As Integer = 0

        'cache the search results for multiple grid pages
        Me.State.searchResult = dv

        If Not Me.State.searchResult Is Nothing Then
            dvCount = Me.State.searchResult.Count
        End If

        ShowGridData(Me.State.searchResult)
        '''''
        Dim autoSelectSvcCenter As Guid = cert.Dealer.AutoSelectServiceCenter
        'For testing in higher env: 
        'Dim autoSelectSvcCenter As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
        Dim strComingFrom As String = Me.NavController.PrevNavState.Name

        '"Search by Zip" is the default Search parameter when the page is initially loaded.
        If (autoSelectSvcCenter.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) AndAlso Me.State.curSearchType = SearchType.ByZip) Then
            ' to check whether the user is coming from Claim Details (Action --> New Center) OR by clicking Back button in Step 4
            If (strComingFrom <> "CLAIM_DETAIL" AndAlso strComingFrom <> "CREATE_NEW_CLAIM") Then
                If (dvCount = 1) Then
                    Me.selectedServiceCenterId.Value = Me.State.searchResult.Table.Rows(0)("CODE").ToString()
                    GoToStep4()
                End If
            End If
        End If
        ''''''
        Me.HandleGridMessages(dvCount, True)
    End Sub

    Private Sub PopulateDrop(ByVal customerCountry As ArrayList)
        Try
            Dim MethodOfRepairType As String
            Dim certItemCov As New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)
            Dim cert As New Certificate(certItemCov.CertId)
            Dim methodOfRepairId As Guid
            If (certItemCov.MethodOfRepairId = Guid.Empty) Then
                methodOfRepairId = cert.MethodOfRepairId
            Else
                methodOfRepairId = certItemCov.MethodOfRepairId
            End If

            MethodOfRepairType = LookupListNew.GetCodeFromId(LookupListNew.LK_METHODS_OF_REPAIR, methodOfRepairId)

            'clear results from intelligent search to reduce the cache storage
            State.searchResult = Nothing

            Dim dv As DataView, strFilter As String

            dv = ServiceCenter.GetAllServiceCenter(customerCountry, methodOfRepairId, IsUserCompaniesWithAcctSettings())

            Dim overRideSingularity As Boolean
            If dv.Count > 0 Then
                overRideSingularity = True
            Else
                overRideSingularity = False
            End If

            moMultipleColumnDrop.SetControl(True, moMultipleColumnDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_CENTER), overRideSingularity)
            If Not Me.State.SelectedServiceCenterId.Equals(Guid.Empty) Then
                moMultipleColumnDrop.SelectedGuid = Me.State.SelectedServiceCenterId
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateGridOrDropDown()
        'get the customer countryId
        Try
            Dim certItemCov As New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)
            Dim cert As New Certificate(certItemCov.CertId)
            Dim oDealer As New Dealer(cert.DealerId)
            Dim ServNetworkSvc As New ServiceNetworkSvc
            'DEF 3482 Commenting below line because line # 387 is the only one using this variable and same was commented earlier
            'Dim address As New Address(cert.AddressId)  
            Dim objCompany As New Company(cert.CompanyId)
            Dim UseZipDistrict As Boolean = True
            Dim DealerType As String
            Dim MethodOfRepairType As String, FlagMethodOfRepairRecovery As Boolean = False

            'REQ-5546
            Dim objCountry As New Country(objCompany.CountryId)
            Me.State.default_service_center_id = objCountry.DefaultSCId

            If objCompany.UseZipDistrictId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N")) Then
                UseZipDistrict = False
            End If

            Dim SelectedCountry As New ArrayList
            Dim isNetwork As Boolean
            SelectedCountry.Add(Me.GetSelectedItem(moCountryDrop))
            'Dim srvCenterdIDs As ArrayList = Nothing
            If Not oDealer.ServiceNetworkId.Equals(Guid.Empty) Then
                ServNetworkSvc.ServiceNetworkId = oDealer.ServiceNetworkId
                isNetwork = True
            Else
                isNetwork = False
            End If

            'srvCenterdIDs = ServNetworkSvc.ServiceCentersIDs(isNetwork, cert.MethodOfRepairId)
            DealerType = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, oDealer.DealerTypeId)  '= Codes.DEALER_TYPES__VSC

            Dim methodOfRepairId As Guid
            If (certItemCov.MethodOfRepairId = Guid.Empty) Then
                methodOfRepairId = cert.MethodOfRepairId
            Else
                methodOfRepairId = certItemCov.MethodOfRepairId
            End If

            MethodOfRepairType = LookupListNew.GetCodeFromId(LookupListNew.LK_METHODS_OF_REPAIR, methodOfRepairId)
            If MethodOfRepairType = Codes.METHOD_OF_REPAIR__RECOVERY Or
               MethodOfRepairType = Codes.METHOD_OF_REPAIR__GENERAL Or
               MethodOfRepairType = Codes.METHOD_OF_REPAIR__LEGAL Then
                FlagMethodOfRepairRecovery = True
            End If

            If Me.State.inputParameters.RecoveryButtonClick = True Then
                FlagMethodOfRepairRecovery = True
                'START  INC-356
                methodOfRepairId = LookupListNew.GetIdFromCode(LookupListNew.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__RECOVERY)
                'END    INC-356
            End If

            Dim dv As ServiceCenter.LocateServiceCenterResultsDv
            With Me.State.inputParameters
                Select Case Me.State.curSearchType
                    Case SearchType.ByZip
                        dv = ServiceCenter.LocateServiceCenterByZip(.DealerId, .ZipLocator, .RiskTypeId, .ManufacturerId, .CovTypeCode, SelectedCountry, oDealer.ServiceNetworkId, isNetwork, methodOfRepairId, UseZipDistrict, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                        PopulateGrid(CType(dv, DataView))
                    Case SearchType.ByCity
                        dv = ServiceCenter.LocateServiceCenterByCity(.DealerId, .ZipLocator, Me.TextboxCity.Text, .RiskTypeId, .ManufacturerId, .CovTypeCode, SelectedCountry, oDealer.ServiceNetworkId, isNetwork, methodOfRepairId, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                        PopulateGrid(CType(dv, DataView))
                    Case SearchType.All
                        Dim selectedSCIds As New ArrayList
                        selectedSCIds.Add(moMultipleColumnDrop.SelectedGuid)
                        Me.State.SelectedServiceCenterId = moMultipleColumnDrop.SelectedGuid
                        PopulateDrop(SelectedCountry)
                        PopulateGrid(ServiceCenter.GetLocateServiceCenterDetails(selectedSCIds, .DealerId, .ManufacturerId).Tables(0).DefaultView)
                End Select
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moMultipleColumnDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles moMultipleColumnDrop.SelectedDropChanged
        Try
            PopulateGridOrDropDown()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub EnableDisableFields()
        Dim showCityFields As Boolean = Me.RadioButtonByCity.Checked
        Dim showAllFields As Boolean = Me.RadioButtonAll.Checked
        Dim NO_SVC_OPTION As Boolean = Me.RadioButtonNO_SVC_OPTION.Checked

        ' City
        ControlMgr.SetVisibleControl(Me, tdCityLabel, showCityFields)
        ControlMgr.SetVisibleControl(Me, moCityLabel, showCityFields)
        ControlMgr.SetVisibleControl(Me, tdCityTextBox, showCityFields)
        ControlMgr.SetVisibleControl(Me, TextboxCity, showCityFields)
        ControlMgr.SetVisibleControl(Me, tdClearButton, showCityFields)
        ControlMgr.SetVisibleControl(Me, btnClearSearch, showCityFields)

        ' All
        ControlMgr.SetVisibleForControlFamily(Me, moMultipleColumnDrop, showAllFields)
        ControlMgr.SetVisibleControl(Me, btnSearch, Not showAllFields)

        'NO_SVC_OPTION
        ControlMgr.SetVisibleControl(Me, btnClearSearch, Not NO_SVC_OPTION)
        ControlMgr.SetVisibleControl(Me, btnSearch, Not NO_SVC_OPTION)
        ControlMgr.SetVisibleControl(Me, moCountryDrop, Not NO_SVC_OPTION)
        ControlMgr.SetVisibleControl(Me, tdCountryLabel, Not NO_SVC_OPTION)

    End Sub

    Sub PopulateCurrentSearchType()
        Try
            Select Case Me.State.curSearchType
                Case SearchType.ByZip
                    Me.RadioButtonByZip.Checked = True
                Case SearchType.ByCity
                    Me.RadioButtonByCity.Checked = True
                Case SearchType.All
                    Me.RadioButtonAll.Checked = True
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'Def-23747- Added condition to get country Id from claim details if certtificate has no address info.
    Sub PopulateCountryDropdown()
        Try
            Dim certItemCov As New CertItemCoverage(Me.State.inputParameters.CertItemCoverageId)
            Dim dv As ServiceCenter.ServiceCenterSearchDV
            Dim CountryId As Guid
            Dim cert As New Certificate(certItemCov.CertId)
            If Not cert.AddressId.Equals(Guid.Empty) Then
                Dim address As New Address(cert.AddressId)
                CountryId = address.CountryId
            Else
                ' Get Country Id from claim details. 
                If Not Me.State.inputParameters.CountryId.Equals(Guid.Empty) Then
                    CountryId = Me.State.inputParameters.CountryId
                Else
                    'Def- 24578- Get Country Id from the claim details if Me.State.inputParameters.CountryId is nothing.
                    If Not CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim).Company.CountryId.Equals(Guid.Empty) Then
                        CountryId = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim).Company.CountryId
                    End If
                End If
            End If

            If Not CountryId.Equals(Guid.Empty) Then
                'dv = ServiceCenter.getUserCustCountries(CountryId)
                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CountryId = CountryId
                oListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                Dim UserCustCountriesList As ListItem() = CommonConfigManager.Current.ListManager.GetList("UserCountryWithSelectedCountry", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                moCountryDrop.Populate(UserCustCountriesList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })

                Me.SetSelectedItem(Me.moCountryDrop, CountryId)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


    Private Sub UpdateBreadCrum(ByVal CertId As Guid)
        If Not CertId = Guid.Empty Then
            If (Me.NavController.CurrentFlow.Name = "CREATE_NEW_CLAIM_FROM_EXISTING_CLAIM") Then
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM DETAILS") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Locate_Service_Center")
            Else
                Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("Locate_Service_Center") & ElitaBase.Sperator & "Certificate " & (New Certificate(CertId)).CertNumber
            End If
        End If
    End Sub

#End Region


#Region "Datagrid Related "

    ''The Binding LOgic is here
    'Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
    '    Try
    '        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
    '        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

    '        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
    '            If Not dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_ADDRESS1) Is DBNull.Value Then
    '                e.Item.Cells(Me.GRID_COL_ADDRESS_IDX).Text = CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_ADDRESS1), String)
    '            End If
    '            If Not dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_CITY) Is DBNull.Value Then
    '                e.Item.Cells(Me.GRID_COL_CITY_IDX).Text = CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_CITY), String)
    '            End If
    '            e.Item.Cells(Me.GRID_COL_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_SERVICE_CENTER_ID), Byte()))
    '            ControlMgr.SetVisibleControl(Me, CType(e.Item.Cells(Me.GRID_COL_MFG_AUTH_IDX).FindControl("LabelMFGAuth"), System.Web.UI.WebControls.Label), BooleanType.Parse(CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_MAN_AUTH_FLAG), String)).Value)
    '            e.Item.Cells(Me.GRID_COL_NAME_IDX).Text = CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_DESCRIPTION), String)
    '            ControlMgr.SetVisibleControl(Me, CType(e.Item.Cells(Me.GRID_COL_PREF_IDX).FindControl("LabelDealerPref"), System.Web.UI.WebControls.Label), BooleanType.Parse(CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_DEALER_PREF_FLAG), String)).Value)
    '            If Not dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_RATING_CODE) Is DBNull.Value Then
    '                e.Item.Cells(Me.GRID_COL_RANK_IDX).Text = CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_RATING_CODE), String)
    '            End If
    '            If Not dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_ZIP_LOCATOR) Is DBNull.Value Then
    '                e.Item.Cells(Me.GRID_COL_ZIP_IDX).Text = CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_ZIP_LOCATOR), String)
    '            End If
    '            e.Item.Cells(Me.GRID_COL_CODE_IDX).Text = CType(dvRow(ServiceCenter.LocateServiceCenterResultsDv.COL_NAME_CODE), String)
    '        End If
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

    'Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
    '    Try
    '        If e.CommandName = "SelectAction" Then
    '            Me.State.SelectedServiceCenterId = New Guid(e.Item.Cells(Me.GRID_COL_ID_IDX).Text)
    '            SaveGUIState()
    '            'Me.callPage(LocateServiceCenterDetailForm.URL, New LocateServiceCenterDetailForm.Parameters(Me.State.SelectedServiceCenterId, Me.State.inputParameters.CertItemCoverageId, Me.State.inputParameters.ShowAcceptButton))
    '            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER_ID) = Me.State.SelectedServiceCenterId
    '            Me.NavController.Navigate(Me, "servCenter_item_selected", New LocateServiceCenterDetailForm.Parameters(Me.State.SelectedServiceCenterId, Me.State.inputParameters.CertItemCoverageId, Me.State.inputParameters.ClaimId, Me.State.inputParameters.ShowAcceptButton, Me.State.inputParameters.WhenAcceptGoToCreateClaim, Me.State.inputParameters.ComingFromDenyClaim, Me.State.inputParameters.RecoveryButtonClick))
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub


    'Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
    '    Try
    '        Me.State.PageIndex = e.NewPageIndex
    '        Me.State.SelectedServiceCenterId = Guid.Empty
    '        If Me.State.searchResult Is Nothing Then
    '            Me.PopulateGridOrDropDown()
    '        Else
    '            showGridData(Me.State.searchResult)
    '        End If

    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub
#End Region

#Region "Buttons Clicks "

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.SelectedServiceCenterId = Guid.Empty
            Me.PopulateGridOrDropDown()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.TextboxCity.Text = ""
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.NavController.Navigate(Me, "back")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
        Try
            Me.NavController.Navigate(Me, "cancel")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByZip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonByZip.CheckedChanged
        Try
            If Me.RadioButtonByZip.Checked Then
                Me.State.curSearchType = SearchType.ByZip
            End If
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByCity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonByCity.CheckedChanged
        Try
            If Me.RadioButtonByCity.Checked Then
                Me.State.curSearchType = SearchType.ByCity
            End If
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonAll.CheckedChanged
        Try
            Me.EnableDisableFields()
            If Me.RadioButtonAll.Checked Then
                Me.State.curSearchType = SearchType.All
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(Me.GetSelectedItem(moCountryDrop))
                PopulateDrop(SelectedCountry)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonNO_SVC_OPTION_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonNO_SVC_OPTION.CheckedChanged
        Try
            Me.EnableDisableFields()
            If Me.RadioButtonNO_SVC_OPTION.Checked Then
                Me.State.curSearchType = SearchType.None
                'Dim address As New Address(Me.State.claimBO.Certificate.AddressId)
                'Dim SelectedCountry As New ArrayList
                'SelectedCountry.Add(Me.Page.GetSelectedItem(moCountryDrop))
                'PopulateCountryDropdown(SelectedCountry)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            If Me.RadioButtonAll.Checked Then
                Me.EnableDisableFields()
                Me.State.curSearchType = SearchType.All
                'get the customer countryId
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(Me.GetSelectedItem(moCountryDrop))
                PopulateDrop(SelectedCountry)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        GoToStep4()
    End Sub

    Private Sub GoToStep4()

        If Me.RadioButtonNO_SVC_OPTION.Checked Then
            'REQ-5546
            'Default SC might not null
            If Not Me.State.default_service_center_id.Equals(Guid.Empty) Then
                Me.State.SelectedServiceCenterId = Me.State.default_service_center_id
            Else
                Me.MasterPage.MessageController.AddError("TEMP SERVICE CENTER NOT FOUND")
                Exit Sub
            End If
        Else
            If (selectedServiceCenterId.Value = "XXXX") Then
                Me.MasterPage.MessageController.AddError("SELECT SERVICE CENTER")
                Exit Sub
            End If
            Me.State.SelectedServiceCenterId = ServiceCenter.GetServiceCenterID(selectedServiceCenterId.Value)
        End If

        'START DEF-2716
        Dim oldClaim As Claim = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), Claim)
        'END DEF-2716

        Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER_ID) = Me.State.SelectedServiceCenterId
        Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = New ServiceCenter(Me.State.SelectedServiceCenterId)
        Me.NavController.Navigate(Me, "servCenter_item_selected", New LocateServiceCenterDetailForm.Parameters(Me.State.SelectedServiceCenterId, Me.State.inputParameters.CertItemCoverageId, Me.State.inputParameters.ClaimId, Me.State.inputParameters.ShowAcceptButton, Me.State.inputParameters.WhenAcceptGoToCreateClaim, Me.State.inputParameters.ComingFromDenyClaim, Me.State.inputParameters.RecoveryButtonClick, Me.State.inputParameters.ClaimedEquipment))

    End Sub


    Private Sub RegisterJavaScriptCode()
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "function ToggleSelection(ctlCodeDropDown, ctlDescDropDown, change_Desc_Or_Code, lblCaption)" & Environment.NewLine
        sJavaScript &= "{" & Environment.NewLine & "var objCodeDropDown = document.getElementById(ctlCodeDropDown);" & Environment.NewLine
        sJavaScript &= "var objDescDropDown = document.getElementById(ctlDescDropDown);" & Environment.NewLine
        'sJavaScript &= " alert( 'Code name = ' + ctlCodeDropDown + ' obj =' + objCodeDropDown + '\n  Desc name = ' + ctlDescDropDown + ' obj =' + objDescDropDown + '\n  Caption name = ' + lblCaption + ' obj =' + document.getElementById(lblCaption));" & Environment.NewLine
        sJavaScript &= "if (change_Desc_Or_Code=='C'){" & Environment.NewLine & "objCodeDropDown.value = objDescDropDown.options[objDescDropDown.selectedIndex].value;}" & Environment.NewLine
        sJavaScript &= "else { objDescDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;}" & Environment.NewLine
        sJavaScript &= "if (lblCaption != '') {document.all.item(lblCaption).style.color = '';}}" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.Page.RegisterStartupScript("ToggleDropDown", sJavaScript)
    End Sub
#End Region

#Region "Error Handling"


#End Region

End Class

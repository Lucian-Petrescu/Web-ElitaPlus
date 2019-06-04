Imports System.Xml.Xsl
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements


Public Class UserControlSelectServiceCenter
    Inherits System.Web.UI.UserControl

    Private Const LABEL_SERVICE_CENTER As String = "SERVICE_CENTER"
    Public Event SelectServiceCenter As EventHandler

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public LocateServiceCenterSearchType As LocateServiceCenterSearchType
        Public claimBO As ClaimBase
        Public ServiceCenterView As DataView
        Public SelectedServiceCenterId As Guid
        Public ExistingServiceCenterId As Guid
        Public PageIndex As Integer = 0
        Public default_service_center_id As Guid
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

    Public Enum LocateServiceCenterSearchType
        ByZip
        ByCity
        All
        None
    End Enum

    Public SelectedServiceCenterId As Guid

#End Region

    Public ReadOnly Property moMessageController As MessageController
        Get
            Return Me.MessageController
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MessageController.Clear()
    End Sub

    Public Sub Populate(ByVal claim As ClaimBase, ByVal existingServiceCenterId As Guid)
        Me.State.claimBO = claim
        Me.State.ExistingServiceCenterId = existingServiceCenterId
        PopulateCurrentSearchType()
        PopulateCountryDropdown()
        PopulateGridOrDropDown()
        EnableDisableControls()
    End Sub

    Sub PopulateCurrentSearchType()
        Try
            Select Case Me.State.LocateServiceCenterSearchType
                Case LocateServiceCenterSearchType.ByZip
                    Me.RadioButtonByZip.Checked = True
                Case LocateServiceCenterSearchType.ByCity
                    Me.RadioButtonByCity.Checked = True
                Case LocateServiceCenterSearchType.All
                    Me.RadioButtonAll.Checked = True
                Case LocateServiceCenterSearchType.None
                    Me.RadioButtonNO_SVC_OPTION.Checked = True
            End Select
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Sub PopulateCountryDropdown()
        Dim address As New Address(Me.State.claimBO.Certificate.AddressId)
        Dim dv As ServiceCenter.ServiceCenterSearchDV
        Dim CountryId As Guid
        CountryId = address.CountryId
        '  dv = ServiceCenter.getUserCustCountries(CountryId)
        'need to pass country and userid in context
        'Me.Page.BindListControlToDataView(moCountryDrop, dv, , , False)--UserCountryWithSelectedCountry
        Dim listcontext As ListContext = New ListContext()
        listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
        listcontext.CountryId = CountryId
        Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("UserCountryWithSelectedCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        moCountryDrop.Populate(countryLkl, New PopulateOptions() With
        {
       .AddBlankItem = False
        })
        Me.Page.SetSelectedItem(Me.moCountryDrop, CountryId)
    End Sub

    Public Sub PopulateGridOrDropDown()
        'get the customer countryId
        Try
            Dim ServNetworkSvc As New ServiceNetworkSvc
            Dim address As New Address(Me.State.claimBO.Certificate.AddressId)
            Dim objCompany As New Company(Me.State.claimBO.Certificate.CompanyId)
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
            SelectedCountry.Add(Me.Page.GetSelectedItem(moCountryDrop))

            If Not Me.State.claimBO.Dealer.ServiceNetworkId.Equals(Guid.Empty) Then
                ServNetworkSvc.ServiceNetworkId = Me.State.claimBO.Dealer.ServiceNetworkId
                isNetwork = True
            Else
                isNetwork = False
            End If

            'srvCenterdIDs = ServNetworkSvc.ServiceCentersIDs(isNetwork, cert.MethodOfRepairId)
            DealerType = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.State.claimBO.Dealer.DealerTypeId)  '= Codes.DEALER_TYPES__VSC


            If Me.State.claimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__RECOVERY Or _
              Me.State.claimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__GENERAL Or _
               Me.State.claimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__LEGAL Then
                FlagMethodOfRepairRecovery = True
            End If

            Dim dv As ServiceCenter.LocateServiceCenterResultsDv
            Select Case Me.State.LocateServiceCenterSearchType
                Case LocateServiceCenterSearchType.ByZip
                    dv = ServiceCenter.LocateServiceCenterByZip(Me.State.claimBO.Certificate.DealerId, Me.State.claimBO.Certificate.AddressChild.ZipLocator, Me.State.claimBO.RiskTypeId, Me.State.claimBO.CertificateItem.ManufacturerId, Me.State.claimBO.CertificateItemCoverage.CoverageTypeCode, SelectedCountry, Me.State.claimBO.Dealer.ServiceNetworkId, isNetwork, Me.State.claimBO.MethodOfRepairId, UseZipDistrict, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                    PopulateServiceCenterGrid(CType(dv, DataView))
                Case LocateServiceCenterSearchType.ByCity
                    dv = ServiceCenter.LocateServiceCenterByCity(Me.State.claimBO.Certificate.DealerId, Me.State.claimBO.Certificate.AddressChild.ZipLocator, Me.TextboxCity.Text, Me.State.claimBO.RiskTypeId, Me.State.claimBO.CertificateItem.ManufacturerId, Me.State.claimBO.CertificateItemCoverage.CoverageTypeCode, SelectedCountry, Me.State.claimBO.Dealer.ServiceNetworkId, isNetwork, Me.State.claimBO.MethodOfRepairId, DealerType, FlagMethodOfRepairRecovery, MethodOfRepairType, IsUserCompaniesWithAcctSettings())
                    PopulateServiceCenterGrid(CType(dv, DataView))
                Case LocateServiceCenterSearchType.All
                    Dim selectedSCIds As New ArrayList
                    selectedSCIds.Add(moMultipleColumnDrop.SelectedGuid)
                    PopulateCountryDropdown(SelectedCountry)
                    PopulateServiceCenterGrid(ServiceCenter.GetLocateServiceCenterDetails(selectedSCIds, Me.State.claimBO.Certificate.DealerId, Me.State.claimBO.CertificateItem.ManufacturerId).Tables(0).DefaultView)
            End Select

        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub PopulateServiceCenterGrid(ByRef dv As DataView)
        Dim MethodOfRepairType As String
        Dim dvCount As Integer = 0

        'cache the search results for multiple grid pages
        Me.State.ServiceCenterView = dv

        If Not Me.State.ServiceCenterView Is Nothing Then
            dvCount = Me.State.ServiceCenterView.Count
        End If

        ShowServiceCenterGridData(Me.State.ServiceCenterView)
        Me.HandleGridMessages(dvCount, True)
    End Sub

    Private Sub HandleGridMessages(ByVal count As Int32, Optional ByVal checkForCount As Boolean = False)
        If count > 1000 AndAlso checkForCount Then
            Me.MessageController.AddInformation(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA, True)
        ElseIf count = 0 Then
            Me.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
        End If

    End Sub

    Private Sub PopulateCountryDropdown(ByVal customerCountry As ArrayList)
        Try
            'clear results from intelligent search to reduce the cache storage
            State.ServiceCenterView = Nothing

            Dim dv As DataView, strFilter As String

            dv = ServiceCenter.GetAllServiceCenter(customerCountry, Me.State.claimBO.MethodOfRepairId, IsUserCompaniesWithAcctSettings())

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
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Function IsUserCompaniesWithAcctSettings() As Boolean
        Dim objAC As AcctCompany
        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 0 OrElse (ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies().Length = 1 AndAlso ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).IsNew = True) Then
            Return False
        Else
            For Each objAC In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies()
                Dim certCompany As New Company(Me.State.claimBO.CertificateItem.CompanyId)
                If certCompany.AcctCompanyId.Equals(objAC.Id) Then
                    If objAC.UseAccounting = "Y" AndAlso objAC.AcctSystemId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), FelitaEngine.FELITA_PREFIX) Then Return True
                End If
            Next
            Return False
        End If

    End Function

    Private Sub ShowServiceCenterGridData(ByVal dv As DataView)
        Try
            Dim dataSet As New DataSet
            hdnSelectedServiceCenterId.Value = "XXXX"
            Dim xdoc As New System.Xml.XmlDocument
            dataSet.Tables.Add(dv.ToTable())
            xdoc.LoadXml(dataSet.GetXml())
            xmlSource.TransformSource = HttpContext.Current.Server.MapPath("~/Certificates/LocateServiceCenterControl.xslt")
            xmlSource.TransformArgumentList = New XsltArgumentList
            xmlSource.TransformArgumentList.AddParam("recordCount", String.Empty, 10)
            xmlSource.Document = xdoc
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub btnSelectServiceCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectServiceCenter.Click
        Try
            If (Not hdnSelectedServiceCenterId.Value = "XXXX") Then
                Me.State.SelectedServiceCenterId = ServiceCenter.GetServiceCenterID(hdnSelectedServiceCenterId.Value)
                Me.SelectedServiceCenterId = Me.State.SelectedServiceCenterId
                If (Not Me.State.ExistingServiceCenterId.Equals(Guid.Empty) AndAlso Me.State.SelectedServiceCenterId = Me.State.ExistingServiceCenterId) Then
                    Throw New GUIException("EXISTING_SERVICE_CENTER_SELECTED", "EXISTING_SERVICE_CENTER_SELECTED")
                End If
                RaiseEvent SelectServiceCenter(sender, e)
            Else
                Throw New GUIException("SELECT SERVICE CENTER", "SELECT SERVICE CENTER")
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub btnSearchServiceCenter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.SelectedServiceCenterId = Guid.Empty
            Me.PopulateGridOrDropDown()
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearchServiceCenter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.TextboxCity.Text = ""
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByZip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonByZip.CheckedChanged
        Try
            If Me.RadioButtonByZip.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.ByZip
            End If
            Me.EnableDisableControls()
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonByCity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonByCity.CheckedChanged
        Try
            If Me.RadioButtonByCity.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.ByCity
            End If
            Me.EnableDisableControls()
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonNO_SVC_OPTION_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonNO_SVC_OPTION.CheckedChanged
        Try
            Me.EnableDisableControls()
            If Me.RadioButtonNO_SVC_OPTION.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.None
                'Dim address As New Address(Me.State.claimBO.Certificate.AddressId)
                'Dim SelectedCountry As New ArrayList
                'SelectedCountry.Add(Me.Page.GetSelectedItem(moCountryDrop))
                'PopulateCountryDropdown(SelectedCountry)
            End If
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub RadioButtonAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonAll.CheckedChanged
        Try
            Me.EnableDisableControls()
            If Me.RadioButtonAll.Checked Then
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.All
                Dim address As New Address(Me.State.claimBO.Certificate.AddressId)
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(Me.Page.GetSelectedItem(moCountryDrop))
                PopulateCountryDropdown(SelectedCountry)
            End If
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub
    Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            If Me.RadioButtonAll.Checked Then
                Me.EnableDisableControls()
                Me.State.LocateServiceCenterSearchType = LocateServiceCenterSearchType.All

                Dim address As New Address(Me.State.claimBO.Certificate.AddressId)
                Dim SelectedCountry As New ArrayList
                SelectedCountry.Add(Me.Page.GetSelectedItem(moCountryDrop))

                PopulateCountryDropdown(SelectedCountry)
            End If
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub moMultipleColumnDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles moMultipleColumnDrop.SelectedDropChanged
        Try
            PopulateGridOrDropDown()
            Dim x As String = "<script language='JavaScript'> revealModal('ModalServiceCenter') </script>"
            Me.Page.RegisterStartupScript("Startup", x)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.MessageController)
        End Try
    End Sub

    Private Sub EnableDisableControls()
        Dim showCityFields As Boolean = Me.RadioButtonByCity.Checked
        Dim showAllFields As Boolean = Me.RadioButtonAll.Checked
        Dim NO_SVC_OPTION As Boolean = Me.RadioButtonNO_SVC_OPTION.Checked

        ' City
        ControlMgr.SetVisibleControl(Me.Page, tdCityLabel, showCityFields)
        ControlMgr.SetVisibleControl(Me.Page, moCityLabel, showCityFields)
        ControlMgr.SetVisibleControl(Me.Page, tdCityTextBox, showCityFields)
        ControlMgr.SetVisibleControl(Me.Page, TextboxCity, showCityFields)
        ControlMgr.SetVisibleControl(Me.Page, tdClearButton, showCityFields)
        ControlMgr.SetVisibleControl(Me.Page, btnClearSearch, showCityFields)

        ' All
        ControlMgr.SetVisibleForControlFamily(Me.Page, moMultipleColumnDrop, showAllFields)
        ControlMgr.SetVisibleControl(Me.Page, btnSearch, Not showAllFields)

        'REQ-5546
        'NO_SVC_OPTION
        ControlMgr.SetVisibleControl(Me.Page, btnClearSearch, Not NO_SVC_OPTION)
        ControlMgr.SetVisibleControl(Me.Page, btnSearch, Not NO_SVC_OPTION)
        ControlMgr.SetVisibleControl(Me.Page, RadioButtonNO_SVC_OPTION, Not Me.State.default_service_center_id.Equals(Guid.Empty))

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

End Class
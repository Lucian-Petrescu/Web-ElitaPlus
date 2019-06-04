Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Namespace Reports

    Partial Class ServiceCenterLocatorReportForm
        Inherits ElitaPlusPage

#Region "ParameterData"

        Public Structure ParameterData
            Public companyCode As String
            Public beginServiceCenterCode As String
            Public endServiceCenterCode As String
            Public countryCode As String
            Public serviceGroupCode As String
            Public priceListCode As String
            Public MethodOfRepair As String
            Public ServiceNetwork As String
            Public languageCode As String
            Public sortOrder As String
            Public priceListName As String
        End Structure

#End Region


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "SERVICE_CENTER_LOCATOR"
        Private Const RPT_FILENAME As String = "ServiceCenterLocator"
        Private Const RPT_FILENAME_EXPORT As String = "ServiceCenterLocator_Exp"
        Public Const ALL As String = "*"

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Dim selectedCountryId As Guid
        Dim selectednetworkId As Guid

#End Region

#Region "Properties"
        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
            End Get
        End Property

        Public ReadOnly Property FromMultipleDrop() As MultipleColumnDropControl
            Get
                If moFromMultipleColumnDropControl Is Nothing Then
                    moFromMultipleColumnDropControl = CType(FindControl("moFromMultipleColumnDropControl"), MultipleColumnDropControl)
                End If
                Return moFromMultipleColumnDropControl
            End Get
        End Property

        Public ReadOnly Property ToMultipleDrop() As MultipleColumnDropControl
            Get
                If moToMultipleColumnDropControl Is Nothing Then
                    moToMultipleColumnDropControl = CType(FindControl("moToMultipleColumnDropControl"), MultipleColumnDropControl)
                End If
                Return moToMultipleColumnDropControl
            End Get
        End Property
        'Public Property AutoPostBackDD() As Boolean
        '    Get
        '        Return Me.FromMultipleDrop.moMultipleColumnDrop.AutoPostBack
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        Me.FromMultipleDrop.moMultipleColumnDrop.AutoPostBack = Value
        '        Me.FromMultipleDrop.moMultipleColumnDropDesc.AutoPostBack = Value
        '        If Not Value Then
        '            Me.FromMultipleDrop.moMultipleColumnDrop.Attributes.Add("onclick", "ToggleDualDropDownsSelection('" & Me.FromMultipleDrop.moMultipleColumnDrop.ID & "', '" & Me.FromMultipleDrop.moMultipleColumnDropDesc.ID & "', 'D')")
        '            Me.FromMultipleDrop.moMultipleColumnDropDesc.Attributes.Add("onclick", "ToggleDualDropDownsSelection('" & Me.FromMultipleDrop.moMultipleColumnDrop.ID & "', '" & Me.FromMultipleDrop.moMultipleColumnDropDesc.ID & "', 'C')")
        '            'Me.RegisterJavaScriptCode()
        '        End If
        '    End Set
        'End Property
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub InitializeForm()
            moAllServiceCentersRadio.Checked = True
            moAllServiceGroupsRadio.Checked = True
            moAllPriceListsRadio.Checked = True
            moAllMethodofRepairRadio.Checked = True
            rSvcNetwork.Checked = True
            PopulateDropDowns()
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            If Me.moCountryLabel.Visible = False Then
                HideHtmlElement("ddSeparator")
            End If
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    'Else
                    ' ClearErrLabels()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDropControl) _
                        Handles moFromMultipleColumnDropControl.SelectedDropChanged
            'fromMultipleDrop.ClearMultipleDrop()
            ' moAllServiceCentersRadio.Checked = True
            If fromMultipleDrop.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                moAllServiceCentersRadio.Checked = False
                'PopulateFromServiceCenter()
            End If
        End Sub

        Private Sub OnToDrop_Changed(ByVal toMultipleDrop As MultipleColumnDropControl) _
                       Handles moToMultipleColumnDropControl.SelectedDropChanged
            ' toMultipleDrop.ClearMultipleDrop()
            ' moAllServiceCentersRadio.Checked = True
            If toMultipleDrop.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                moAllServiceCentersRadio.Checked = False
                ' PopulateToServiceCenter()
            End If
        End Sub

        Private Sub cboCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
            Try
                selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                If Me.selectedCountryId.Equals(Guid.Empty) Then
                    'HideHtmlElement("tblSericeCenter")
                    Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                Else
                    PopulateFromServiceCenter()
                    PopulateToServiceCenter()
                    PopulateServiceGroup()
                    PopulatePriceList()
                    btnGenRpt.Visible = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-Radio"

        'Private Sub moAllServiceCentersRadio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moAllServiceCentersRadio.CheckedChanged
        '    FromMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        '    ToMultipleDrop.ClearMultipleDrop()
        'End Sub

#End Region

#End Region

#Region "Clear"

        'Private Sub ClearErrLabels()

        '    Me.ClearLabelErrSign(moFromLabel)
        '    Me.ClearLabelErrSign(moToLabel)
        '    Me.ClearLabelErrSign(moServiceGroupLabel)
        '    Me.ClearLabelErrSign(moPriceGroupLabel)
        '    Me.ClearLabelErrSign(moServiceCenterTypeLabel)
        'End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDropDowns()
            PopulateCountryDropDown()
            PopulateServiceNetwork()
            PopulateFromServiceCenter()
            PopulateToServiceCenter()
            PopulateServiceGroup()
            PopulatePriceList()
            PopulateServiceCenterType()
        End Sub

        Private Sub PopulateFromServiceCenter(Optional ByVal svcnetwork As String = "")
            If rSvcNetwork.Checked = True Then
                dpSvcNetwork.SelectedIndex = -1

                FromMultipleDrop.NothingSelected = True
                If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                    selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                    FromMultipleDrop.BindData(LookupListNew.GetServiceCenterLookupList(selectedCountryId))
                Else
                    FromMultipleDrop.BindData(LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
                End If
            Else
                selectednetworkId = Me.GetSelectedItem(Me.dpSvcNetwork)
                rSvcNetwork.Checked = False

                FromMultipleDrop.NothingSelected = True
                If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                    selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                    FromMultipleDrop.BindData(ServiceCenter.GetServiceCenterbyServiceNetwork(selectednetworkId, selectedCountryId))
                Else
                    FromMultipleDrop.BindData(ServiceCenter.GetServiceCenterbyServiceNetwork(selectednetworkId, Guid.Empty))
                End If

            End If
            'Else
            'FromMultipleDrop.BindData(LookupListNew.GetServiceCenterLookupList(selectedCountryId))
            'End If

        End Sub

        Private Sub PopulateToServiceCenter(Optional ByVal svcnetwork As String = "")
            'ToMultipleDrop.NothingSelected = True
            'If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
            '    selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
            '    ToMultipleDrop.BindData(LookupListNew.GetServiceCenterLookupList(selectedCountryId))
            'Else
            '    ToMultipleDrop.BindData(LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
            'End If

            If rSvcNetwork.Checked = True Then
                dpSvcNetwork.SelectedIndex = -1

                ToMultipleDrop.NothingSelected = True
                If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                    selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                    ToMultipleDrop.BindData(LookupListNew.GetServiceCenterLookupList(selectedCountryId))
                Else
                    ToMultipleDrop.BindData(LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
                End If
            Else
                selectednetworkId = Me.GetSelectedItem(Me.dpSvcNetwork)
                rSvcNetwork.Checked = False

                ToMultipleDrop.NothingSelected = True
                If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                    selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                    ToMultipleDrop.BindData(ServiceCenter.GetServiceCenterbyServiceNetwork(selectednetworkId, selectedCountryId))
                Else
                    ToMultipleDrop.BindData(ServiceCenter.GetServiceCenterbyServiceNetwork(selectednetworkId, Guid.Empty))
                End If

            End If

        End Sub

        Private Sub PopulateCountryDropDown()
            ' Me.BindListControlToDataView(Me.cboCountry, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()

            Me.cboCountry.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
            If cboCountry.Items.Count < 3 Then
                HideHtmlElement("ddSeparator")
                ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
                ControlMgr.SetVisibleControl(Me, cboCountry, False)
            Else
                HideHtmlElement("tblSericeCenter")
                btnGenRpt.Visible = False
            End If
        End Sub
        Private Sub PopulateServiceGroup()

            If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                ' Me.BindListControlToDataView(moServiceGroupDrop,
                'LookupListNew.GetServiceGroupLookupList(selectedCountryId)) 'ServiceGroupByCountry 
                Dim listcontext As ListContext = New ListContext()
                listcontext.CountryId = selectedCountryId
                Dim svcLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceGroupByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moServiceGroupDrop.Populate(svcLkl, New PopulateOptions() With
                 {
                 .AddBlankItem = True
                 })
            Else
                ' Me.BindListControlToDataView(moServiceGroupDrop,
                'LookupListNew.GetServiceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.CountryId = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
                Dim svcLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceGroupByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moServiceGroupDrop.Populate(svcLkl, New PopulateOptions() With
                 {
                 .AddBlankItem = True
                 })
            End If
        End Sub

        'Private Sub PopulatePriceGroup()
        '    If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
        '        selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
        '        Me.BindListControlToDataView(moPriceListDrop, _
        '         LookupListNew.GetPriceGroupLookupList(selectedCountryId))
        '    Else
        '        Me.BindListControlToDataView(moPriceListDrop, LookupListNew.GetPriceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)
        '    End If
        'End Sub

        Private Sub PopulatePriceList()
            If cboCountry.Visible = True And cboCountry.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                selectedCountryId = Me.GetSelectedItem(Me.cboCountry)
                'Me.BindListControlToDataView(moPriceListDrop,
                ' LookupListNew.GetPriceListLookupList(selectedCountryId)) 'PriceListByCountry
                Dim listcontext As ListContext = New ListContext()
                listcontext.CountryId = selectedCountryId
                Dim prcLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PriceListByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moPriceListDrop.Populate(prcLkl, New PopulateOptions() With
                 {
                   .AddBlankItem = True
                    })
            Else
                'Me.BindListControlToDataView(moPriceListDrop, LookupListNew.GetPriceListLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)

                Dim listcontext As ListContext = New ListContext()
                listcontext.CountryId = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
                Dim prcLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PriceListByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moPriceListDrop.Populate(prcLkl, New PopulateOptions() With
                 {
                   .AddBlankItem = True
                    })
            End If
        End Sub

        Private Sub PopulateServiceCenterType()
            '    Me.BindListControlToDataView(moMethodofRepairDrop, LookupListNew.GetMethodOfRepairForRepairsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True) 'METHR
            Dim methodLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("METHR", Thread.CurrentPrincipal.GetLanguageCode())

            Dim filteredCountryList As DataElements.ListItem() = (From x In methodLkl
                                                                  Where x.Code = "C" Or x.Code = "H" Or x.Code = "S" Or x.Code = "P" Or x.Code = "R"
                                                                  Select x).ToArray()

            moMethodofRepairDrop.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
        End Sub

        Private Sub PopulateServiceNetwork()
            'Me.BindListControlToDataView(dpSvcNetwork, LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True) 'ServiceNetworkByCompanyGroup
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim svcLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceNetworkByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            dpSvcNetwork.Populate(svcLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
                })
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal data As ParameterData) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams() As ReportCeBaseForm.RptParam

            With data
                repParams = New ReportCeBaseForm.RptParam() _
                {
                 New ReportCeBaseForm.RptParam("V_COMPANY", .companyCode),
                 New ReportCeBaseForm.RptParam("V_BEGIN_SERVICE_CENTER_CODE", .beginServiceCenterCode),
                 New ReportCeBaseForm.RptParam("V_END_SERVICE_CENTER_CODE", .endServiceCenterCode),
                 New ReportCeBaseForm.RptParam("V_COUNTRY_CODE", .countryCode),
                 New ReportCeBaseForm.RptParam("V_SERVICE_GROUP_CODE", .serviceGroupCode),
                 New ReportCeBaseForm.RptParam("V_PRICE_LIST_CODE", .priceListCode),
                 New ReportCeBaseForm.RptParam("V_METHOD_OF_REPAIR_DESC", .MethodOfRepair),
                 New ReportCeBaseForm.RptParam("V_SERVICE_NETWORK_DESC", .ServiceNetwork),
                 New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", .languageCode),
                 New ReportCeBaseForm.RptParam("V_SORT_ORDER", .sortOrder),
                 New ReportCeBaseForm.RptParam("PRICE_LIST_NAME", .priceListName)
                }
            End With

            reportFormat = ReportCeBase.GetReportFormat(Me)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            Dim data As ParameterData
            Dim companyId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim serviceGroupId, priceListId, serviceTypeId As Guid
            Dim selectedCountryId As Guid = Me.GetSelectedItem(Me.cboCountry)
            Dim dvCountry As DataView = LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim countryCode As String

            With data
                .companyCode = LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANIES, companyId)

                If moAllServiceCentersRadio.Checked Then
                    .beginServiceCenterCode = ALL
                    .endServiceCenterCode = ALL
                ElseIf ((FromMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED) OrElse
                         (ToMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED) OrElse
                         (FromMultipleDrop.SelectedIndex > ToMultipleDrop.SelectedIndex)) Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_FROMTO_SERVICECENTER_ERR)
                Else
                    .beginServiceCenterCode = FromMultipleDrop.SelectedCode
                    .endServiceCenterCode = ToMultipleDrop.SelectedCode
                End If

                If Me.cboCountry.Visible = False Then
                    .countryCode = ALL
                Else
                    .countryCode = LookupListNew.GetCodeFromId(dvCountry, selectedCountryId)
                    If selectedCountryId.Equals(Guid.Empty) Then
                        'ElitaPlusPage.SetLabelError(moCountryLabel)
                        Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COUNTRY_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If moAllServiceGroupsRadio.Checked Then
                    .serviceGroupCode = ALL
                Else
                    serviceGroupId = Me.GetSelectedItem(moServiceGroupDrop)
                    .serviceGroupCode = LookupListNew.GetCodeFromId(LookupListNew.LK_SERVICE_GROUPS, serviceGroupId)
                End If

                If moAllPriceListsRadio.Checked Then
                    .priceListCode = ALL
                    .priceListName = Nothing
                Else
                    priceListId = Me.GetSelectedItem(moPriceListDrop)
                    .priceListCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, priceListId)
                    .priceListName = Me.GetSelectedDescription(moPriceListDrop)
                End If

                If moAllMethodofRepairRadio.Checked Then
                    .MethodOfRepair = ALL
                Else
                    ' serviceTypeId = Me.GetSelectedItem(moMethodofRepairDrop)
                    .MethodOfRepair = Me.GetSelectedDescription(moMethodofRepairDrop)
                    'LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_TYPES, serviceTypeId)
                End If

                If rSvcNetwork.Checked Then
                    .ServiceNetwork = ALL
                Else
                    ' serviceTypeId = Me.GetSelectedItem(moMethodofRepairDrop)
                    .ServiceNetwork = Me.GetSelectedDescription(dpSvcNetwork)
                    'LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_TYPES, serviceTypeId)
                End If

                .languageCode = LookupListNew.GetCodeFromId("LANGUAGES", langId)

                Select Case moSortOrderRadioList.SelectedValue
                    Case "1"
                        .sortOrder = "S"
                    Case "2"
                        .sortOrder = "C"
                    Case "3"
                        .sortOrder = "G"
                    Case "4"
                        .sortOrder = "M"
                End Select

            End With

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(data)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region


        Private Sub dpSvcNetwork_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpSvcNetwork.SelectedIndexChanged
            Try
                selectednetworkId = Me.GetSelectedItem(Me.dpSvcNetwork)
                rSvcNetwork.Checked = False

                If Me.selectednetworkId.Equals(Guid.Empty) Then
                    'HideHtmlElement("tblSericeCenter")
                    rSvcNetwork.Checked = True
                    'Throw New GUIException(Message.MSG_INVALID_COUNTRY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_NETWORK_MUST_BE_SELECTED_ERR)
                Else
                    PopulateFromServiceCenter()
                    PopulateToServiceCenter()
                    btnGenRpt.Visible = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub rSvcNetwork_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rSvcNetwork.CheckedChanged
            If rSvcNetwork.Checked = True Then
                dpSvcNetwork.SelectedIndex = -1
                PopulateFromServiceCenter()
                PopulateToServiceCenter()
                btnGenRpt.Visible = True
            End If
        End Sub
    End Class

End Namespace

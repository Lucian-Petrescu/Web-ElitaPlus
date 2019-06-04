Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Partial Class UserControlAddress_New
    Inherits System.Web.UI.UserControl
    

#Region "Constants"
    Protected Const EXCEPTION_TEXT001 As String = "AddressController can not access Data Source -- "
    Protected Const EXCEPTION_TEXT002 As String = "AddressController can not update Data -- "

    Public Const ADDRESS1_PROPERTY As String = "Address1"
    Public Const ADDRESS2_PROPERTY As String = "Address2"
    Public Const ADDRESS3_PROPERTY As String = "Address3"  ''REQ-784
    Public Const CITY_PROPERTY As String = "City"
    Public Const CONTRYID_PROPERTY As String = "CountryId"
    Public Const REGIONID_PROPERTY As String = "RegionId"
    Public Const POSTALCODE_PROPERTY As String = "PostalCode"
    Public Const SpanString As String = "<span class=""mandatory"">"
#End Region

#Region "Attributes"


#End Region

#Region "Properties"
    Dim _oCountryID As Guid
    Dim IsAddressRequired As Boolean = False
    Private Property oCountryID() As Guid
        Get
            Return _oCountryID
        End Get
        Set(ByVal Value As Guid)
            _oCountryID = Value
        End Set
    End Property

    Public Property MyBO() As Address
        Get
            Return CType(MyGenBO, Address)
        End Get
        Set(ByVal Value As Address)
            MyGenBO = Value
        End Set
    End Property

    Public Property MyGenBO() As BusinessObjectBase
        Get
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), BusinessObjectBase)
        End Get
        Set(ByVal Value As BusinessObjectBase)
            Me.Page.StateSession.Item(Me.UniqueID) = Value
        End Set
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

    Public Property RegionText() As String
        Get
            Return Me.moRegionText.Text
        End Get
        Set(ByVal Value As String)
            Me.moRegionText.Text = Value
        End Set
    End Property

    Private _profile_code As String
    Public Property ProfileCode() As String
        Get
            Return _profile_code
        End Get

        Set(value As String)
            _profile_code = value
        End Set
    End Property
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'If Not (IsPostBack) Then
        SetValidatedAddressSectionVisibility(False)
        'End If
        'Put user code to initialize the page here
        'If Not Me.MyGenBO Is Nothing Then
        '    BindBoPropertiesToLabels()
        '    Me.Page.AddLabelDecorations(Me.MyGenBO)
        'End If

        '' REQ-784
        If Parent.ID = "moUserControlContactInfo" Then
            'Dim controlname As String = Parent.ID & "_" & moRegionDrop_WRITE.Parent.ID & "_moRegionDrop_WRITE"
            'moRegionDrop_WRITE.Attributes.Add("onchange", String.Format("getComunaList('{0}');", controlname))
            'Dim textcontrolname As String = Parent.ID & "_" & moPostalText.Parent.ID & "_moPostalText"
            'moPostalText.Attributes.Add("onkeyup", String.Format("return getList('{0}','{1}')", textcontrolname, controlname))
        Else
            Dim controlname As String = moRegionDrop_WRITE.Parent.ID & "_moRegionDrop_WRITE"
            moRegionDrop_WRITE.Attributes.Add("onchange", String.Format("getComunaList('{0}');", controlname))
            Dim textcontrolname As String = moPostalText.Parent.ID & "_moPostalText"
            moPostalText.Attributes.Add("onkeyup", String.Format("return getList('{0}','{1}')", textcontrolname, controlname))
            'DEF-2173 Start
            If (Not Me.MyGenBO Is Nothing) Then
                If Me.MyGenBO.GetType.Name.ToString() = "CommissionEntity" Then
                    ControlMgr.SetVisibleControl(Page, moAddress3Label, False)
                    ControlMgr.SetVisibleControl(Page, moAddress3Text, False)
                End If
            End If
            'DEF-2173 END
        End If
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        moPostalLabel.Visible = True
        moPostalText.Visible = True
        moRegionLabel.Visible = True

        Dim blnReadOnly As Boolean = False
        If moRegionText.Visible Then
            blnReadOnly = True
        Else
            moRegionDrop_WRITE.Visible = True
        End If

        'no country selected, show all controls
        If moCountryDrop_WRITE.SelectedIndex = -1 Then Exit Sub

        Dim oCountryID As Guid = New Guid(moCountryDrop_WRITE.SelectedItem.Value)
        'selected country is empty, show all controls
        If oCountryID = Guid.Empty Then Exit Sub

        Dim countryBO As Country = New Country(oCountryID)

        Dim strCtyMailAddFmt As String

        strCtyMailAddFmt = countryBO.MailAddrFormat.ToUpper
        'no zip, hide the zip field
        If strCtyMailAddFmt.IndexOf("[ZIP]") = -1 Then
            moPostalText.Text = ""
            ControlMgr.SetVisibleControl(Page, moPostalLabel, False)
            ControlMgr.SetVisibleControl(Page, moPostalText, False)
        End If
        'no region, hide the region field
        If Not (strCtyMailAddFmt.IndexOf("[RGCODE]") > -1 OrElse strCtyMailAddFmt.IndexOf("[RGNAME]") > -1) Then
            ControlMgr.SetVisibleControl(Page, moRegionLabel, False)
            If blnReadOnly Then
                ControlMgr.SetVisibleControl(Page, moRegionText, False)
            Else
                ControlMgr.SetVisibleControl(Page, moRegionDrop_WRITE, False)
                moRegionDrop_WRITE.SelectedIndex = -1
            End If
        End If

        'Set required zip and region labels
        If IsAddressRequired Then
            Dim strAddFmt As String = countryBO.MailAddrFormat.ToUpper
            If Address.IsAddressComponentRequired(strAddFmt, "ZIP") AndAlso (Not moPostalLabel.Text.Replace(SpanString, "").StartsWith("*")) Then
                moPostalLabel.Text = "<span class=""mandatory"">*</span> " & Me.moPostalLabel.Text
            End If
            If (Address.IsAddressComponentRequired(strAddFmt, "RGCODE") OrElse Address.IsAddressComponentRequired(strAddFmt, "RGNAME")) AndAlso (Not moRegionLabel.Text.Replace(SpanString, "").StartsWith("*")) Then
                moRegionLabel.Text = "<span class=""mandatory"">*</span> " & Me.moRegionLabel.Text
            End If

        End If

        'expand the address 1,2, 3 to 3 rows for Japan address
        If countryBO.Code = "JP" Then
            moAddress1Text.Rows = 3
            moAddress2Text.Rows = 3
            moAddress3Text.Rows = 3
        End If


        'ValidatedAddress.Visible = False
    End Sub

#End Region

#Region "Control Management"
    Public Sub ChangeEnabledControlProperty(ByVal bEnable As Boolean)
        Page.ChangeEnabledControlProperty(Me.moCityText, bEnable)
        Page.ChangeEnabledControlProperty(Me.moAddress1Text, bEnable)
        Page.ChangeEnabledControlProperty(Me.moAddress2Text, bEnable)
        Page.ChangeEnabledControlProperty(Me.moAddress3Text, bEnable)
        Page.ChangeEnabledControlProperty(Me.moPostalText, bEnable)
        Page.ChangeEnabledControlProperty(Me.moRegionDrop_WRITE, bEnable)
        Page.ChangeEnabledControlProperty(Me.moCountryText, bEnable)
    End Sub
    'Req 784
    Public Sub EnableDisablecontrol(ByVal bvalue As Boolean)

        moCityText.ReadOnly = bvalue
        moAddress1Text.ReadOnly = bvalue
        moAddress2Text.ReadOnly = bvalue
        moAddress3Text.ReadOnly = bvalue
        moPostalText.ReadOnly = bvalue
        'Country
        moCountryDrop_WRITE.Visible = True
        moCountryDrop_WRITE.Enabled = True
        moCountryText.Visible = False
        moCountryText.ReadOnly = bvalue
        'Region
        moRegionDrop_WRITE.Visible = True
        moRegionDrop_WRITE.Enabled = True

    End Sub
    Public Sub ReAssignTabIndex(Optional ByVal TabIndexStartingNumber As Int16 = 0)
        If TabIndexStartingNumber > 0 Then
            Me.moAddress1Text.TabIndex = TabIndexStartingNumber
            Me.moCityText.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            Me.moPostalText.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
            Me.moAddress2Text.TabIndex = CType(TabIndexStartingNumber + 3, Int16)
            Me.moRegionDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 6, Int16)
            Me.moRegionText.TabIndex = CType(TabIndexStartingNumber + 5, Int16)
            Me.moCountryDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 4, Int16)
            Me.moCountryText.TabIndex = CType(TabIndexStartingNumber + 7, Int16)
        End If
    End Sub

    Public Sub EnableControls(ByVal bEnable As Boolean, Optional ByVal isNewCompany As Boolean = False)
        Page.EnableDisableControls(Me, bEnable)

        If bEnable Then

            ' Region
            If Not moRegionDrop_WRITE Is Nothing Then
                moRegionText.Text = ElitaPlusPage.GetSelectedDescription(moRegionDrop_WRITE)
            Else
                moRegionText.Text = String.Empty
            End If
            ControlMgr.SetVisibleControl(Page, moRegionDrop_WRITE, False)
            ControlMgr.SetVisibleControl(Page, moRegionText, True)
            moRegionText.ReadOnly = True
            moRegionText.BorderColor = Color.FromArgb(198, 198, 198)

            ' Country
            If Not moCountryDrop_WRITE Is Nothing Then
                moCountryText.Text = ElitaPlusPage.GetSelectedDescription(moCountryDrop_WRITE)
            Else
                moCountryText.Text = String.Empty
            End If
            ControlMgr.SetVisibleControl(Page, moCountryDrop_WRITE, False)
            ControlMgr.SetVisibleControl(Page, moCountryText, True)
            moCountryText.ReadOnly = True
            moCountryText.BorderColor = Color.FromArgb(198, 198, 198)

        Else
            ' Region
            ControlMgr.SetVisibleControl(Page, moRegionDrop_WRITE, True)
            ControlMgr.SetVisibleControl(Page, moRegionText, False)
            moRegionText.ReadOnly = False
            moRegionText.BorderColor = Color.FromArgb(108, 108, 108)

            ' Country
            ControlMgr.SetVisibleControl(Page, moCountryDrop_WRITE, True)
            ControlMgr.SetVisibleControl(Page, moCountryText, False)
            moCountryText.ReadOnly = False
            moCountryText.BorderColor = Color.FromArgb(108, 108, 108)

            'If isNewCompany Then
            '    ControlMgr.SetVisibleControl(Page, moCountryText, False)
            '    ControlMgr.SetVisibleControl(Page, moCountryDrop_WRITE, True)
            'End If
        End If

        'moCountryText.ReadOnly = True
        moCountryText.CssClass = "FLATTEXTBOX"
        'moCountryText.BorderColor = Color.FromArgb(198, 198, 198)
    End Sub

    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged
        oCountryID = New Guid(moCountryDrop_WRITE.SelectedItem.Value)
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CountryId = oCountryID
        Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
        moRegionDrop_WRITE.Populate(oRegionList, New PopulateOptions() With
                                       {
                                       .AddBlankItem = True
                                       })

        EnableControls(False, True)
    End Sub

    Private ReadOnly Property ParentPage As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property
    Private Sub btnValidate_Address_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate_Address.Click
        Try

            If (Validate()) Then
                Dim originalAddress As Address
                originalAddress = New Address
                originalAddress.Address1 = moAddress1Text.Text
                originalAddress.Address2 = moAddress2Text.Text
                originalAddress.Address3 = moAddress3Text.Text
                originalAddress.City = moCityText.Text
                originalAddress.PostalCode = moPostalText.Text

                Dim response As AddressValidationProvider.BusinessObjects.ValidateAddressResponse = AddressService(originalAddress.Address1, originalAddress.PostalCode)
                If (response.IsOriginalAddressValid) Then
                    validateAddressButton.Visible = True
                    LabelPopupHeader.Text = TranslationBase.TranslateLabelOrMessage("IS_ADDRESS_VALIDATED") & ": " & TranslationBase.TranslateLabelOrMessage("Yes")
                    LabelAddress1Text.Text = response.FormattedAddress.Address1
                    LabelAddress2Text.Text = response.FormattedAddress.Address2
                    LabelAddress3Text.Text = response.FormattedAddress.Address3
                    Label1CountryText.Text = response.FormattedAddress.Country
                    LabelZipText.Text = response.FormattedAddress.PostalCode
                    LabelStateText.Text = response.FormattedAddress.State
                    LabelCityText.Text = response.FormattedAddress.City
                Else
                    LabelPopupHeader.Text = TranslationBase.TranslateLabelOrMessage("IS_ADDRESS_VALIDATED") & ": " & TranslationBase.TranslateLabelOrMessage("No")
                    validateAddressButton.Visible = False
                    LabelAddress1Text.Text = String.Empty
                    LabelAddress2Text.Text = String.Empty
                    LabelAddress3Text.Text = String.Empty
                    Label1CountryText.Text = String.Empty
                    LabelZipText.Text = String.Empty
                    LabelStateText.Text = String.Empty
                    LabelCityText.Text = String.Empty
                End If
                SetValidatedAddressSectionVisibility(true)
                
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetValidatedAddressSectionVisibility(blnIsVisible As Boolean)
        ValidatedAddress.Visible = blnIsVisible

        'if the validated address section is visible, then set the address entry section as readonly
        moAddress1Text.ReadOnly = blnIsVisible
        moAddress2Text.ReadOnly = blnIsVisible
        moAddress3Text.ReadOnly = blnIsVisible
        moCityText.ReadOnly = blnIsVisible
        moPostalText.ReadOnly = blnIsVisible
        'Country
        moCountryDrop_WRITE.Enabled = Not blnIsVisible
        moCountryText.ReadOnly = blnIsVisible
        'Region
        moRegionText.ReadOnly = blnIsVisible
        moRegionDrop_WRITE.Enabled = Not blnIsVisible
    End Sub

    Private Function AddressService(ByVal keiyakusha_address As String, ByVal keiyakusha_post_no As String) As AddressValidationProvider.BusinessObjects.ValidateAddressResponse
        Dim response As AddressValidationProvider.BusinessObjects.ValidateAddressResponse = Nothing
        Dim provider = New AddressValidationProvider.AddressValidation.SpectrumAddressValidationProvider()

        If moCountryText.Text = "" Then
            moCountryText.Text = moCountryDrop_WRITE.SelectedItem.Text
        End If

        Try

            If Not String.IsNullOrWhiteSpace(keiyakusha_address) AndAlso Not String.IsNullOrWhiteSpace(keiyakusha_post_no) Then
                Dim address = New AddressValidationProvider.BusinessObjects.Address With {
                    .Address1 = keiyakusha_address,
                    .PostalCode = keiyakusha_post_no,
                    .Country = moCountryText.Text
                }
                response = provider.Validate(address, MyBO.ProfileCode)
            End If

        Catch ex As Exception
            response.ErrorMessages.Add(ex.Message)
        End Try

        Return response
    End Function

    Private Sub btnAccept_Address_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        moAddress1Text.Text = LabelAddress1Text.Text
        moAddress2Text.Text = LabelAddress2Text.Text
        moAddress3Text.Text = LabelAddress3Text.Text
        moCountryText.Text = Label1CountryText.Text
        moCityText.Text = LabelCityText.Text
        moPostalText.Text = LabelZipText.Text
        moRegionText.Text = LabelStateText.Text

        Me.Page.SetSelectedItemByText(moCountryDrop_WRITE, Label1CountryText.Text)

        Dim regionExists As Boolean = False
        Try
            ' Check if Region text Exists in Dropdown list
            For Each item As ListItem In moRegionDrop_WRITE.Items
                If item.Text = moRegionText.Text Then
                    regionExists = True
                    Exit For
                End If
            Next

            ' If Region text Not Exists in Dropdown list then take description based on Code
            If Not regionExists Then
                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CountryId = New Guid(moCountryDrop_WRITE.SelectedItem.Value)
                Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)

                Dim strRegionDesc As String = String.Empty
                ' Check if Code Exists
                If oRegionList.Any(Function(x) x.Code = moRegionText.Text) Then
                    strRegionDesc = oRegionList.Where(Function(x) x.Code = moRegionText.Text).First().Translation
                End If

                ' Check if we got Description based on Code
                If Not String.IsNullOrEmpty(strRegionDesc) Then
                    moRegionText.Text = strRegionDesc
                End If
            End If
            Me.Page.SetSelectedItemByText(moRegionDrop_WRITE, moRegionText.Text)
        Catch ex As Threading.ThreadAbortException
            Me.ParentPage.DisplayMessage(Message.MSG_PROMPT_STATE_NOT_CONFIGURED, "", Me.ParentPage.MSG_BTN_OK, Me.ParentPage.MSG_TYPE_INFO)
        Catch ex As Exception
            Me.ParentPage.DisplayMessage(Message.MSG_PROMPT_STATE_NOT_CONFIGURED, "", Me.ParentPage.MSG_BTN_OK, Me.ParentPage.MSG_TYPE_INFO)
        End Try
        SetValidatedAddressSectionVisibility(false)
    End Sub
    Private Sub btnDecline_Address_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecline.Click
        Try
            If Not Me.MyGenBO Is Nothing Then
                BindBoPropertiesToLabels()
                Me.Page.AddLabelDecorations(Me.MyGenBO)
            End If
            Me.PopulateControlFromBO()
            SetValidatedAddressSectionVisibility(false)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

        End Try
    End Sub

    Private Function Validate() As Boolean
        Dim returnValue As Boolean = True

        If ((moAddress1Text.Text Is Nothing OrElse moAddress1Text.Text.Trim() = String.Empty)) Then
            Me.ParentPage.DisplayMessage(Message.MSG_PROMPT_ADDRESS_REQUIRED, "", Me.ParentPage.MSG_BTN_OK, Me.ParentPage.MSG_TYPE_INFO)
            returnValue = False
        End If


        'If (((moCountryText.Text Is Nothing) OrElse (moCountryText.Text.Trim() = String.Empty)) And ((moCountryDrop_WRITE.SelectedItem.Text Is Nothing) OrElse (moCountryDrop_WRITE.SelectedItem.Text.Trim() = String.Empty))) Then

        If ((moCountryDrop_WRITE.SelectedItem.Text Is Nothing) OrElse (moCountryDrop_WRITE.SelectedItem.Text.Trim() = String.Empty)) Then
            Me.ParentPage.DisplayMessage(Message.MSG_PROMPT_COUNTRY_REQUIRED, "", Me.ParentPage.MSG_BTN_OK, Me.ParentPage.MSG_TYPE_INFO)
            returnValue = False
        End If

        If ((moPostalText.Text Is Nothing) OrElse (moPostalText.Text.Trim() = String.Empty)) Then
            Me.ParentPage.DisplayMessage(Message.MSG_PROMPT_ZIPCODE_REQUIRED, "", Me.ParentPage.MSG_BTN_OK, Me.ParentPage.MSG_TYPE_INFO)
            returnValue = False
        End If

        Return returnValue
    End Function

#End Region

#Region "Load"
    Private Sub ClearTexts()
        moAddress1Text.Text = Nothing
        moAddress2Text.Text = Nothing
        moAddress3Text.Text = Nothing
        moCityText.Text = Nothing
        moPostalText.Text = Nothing
    End Sub

    Public Sub ClearAll()
        ClearTexts()
        If moRegionDrop_WRITE.SelectedIndex > -1 Then moRegionDrop_WRITE.SelectedIndex = 0
        If moCountryDrop_WRITE.SelectedIndex > -1 Then moCountryDrop_WRITE.SelectedIndex = 0
    End Sub

    Private Sub LoadRegionList()
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CountryId = Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId")
        Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
        moRegionDrop_WRITE.Populate(oRegionList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })

    End Sub

    Private Sub LoadCountryList(Optional ByVal nothingSelcted As Boolean = True)
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim oCountryList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompanyGroup", context:=oListContext)
        moCountryDrop_WRITE.Populate(oCountryList, New PopulateOptions() With
                                        {
                                        .AddBlankItem = nothingSelcted
                                        })


    End Sub

    Public Sub Bind(ByVal oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        Me.PopulateControlFromBO()
    End Sub
    'KDDI
    Public Sub Bind(ByVal oBusObj As BusinessObjectBase, ByVal ProfileCode As String)
        MyGenBO = oBusObj
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            MyBO.ProfileCode = ProfileCode
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        Me.PopulateControlFromBO()
    End Sub

    '' REQ-784
    Public Sub NewClaimBind(ByVal oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        PopulateNewClaimControlFromBO()

    End Sub
    '' REQ-784
    Public Sub ClaimDetailsBind(ByVal oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        PopulateControlFromBO()

    End Sub
    Protected Sub BindBoPropertiesToLabels()
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, POSTALCODE_PROPERTY, Me.moPostalLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, ADDRESS1_PROPERTY, Me.moAddress1Label)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, ADDRESS2_PROPERTY, Me.moAddress2Label)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, ADDRESS3_PROPERTY, Me.moAddress3Label)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, CITY_PROPERTY, Me.moCityLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, CONTRYID_PROPERTY, Me.moCountryLabel)
        Me.Page.BindBOPropertyToLabel(Me.MyGenBO, REGIONID_PROPERTY, Me.moRegionLabel)
        'Me.Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    '' REQ-784
    Public Sub PopulateNewClaimControlFromBO()
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        If Not Me.MyGenBO Is Nothing Then
            LoadRegionList()
            moCountryText.Text = String.Empty
            If Not Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId").Equals(Guid.Empty) Then
                LoadCountryList(False)
                moCountryText.Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
            Else
                LoadCountryList()
            End If
            Me.Page.SetSelectedItem(moRegionDrop_WRITE, Me.Page.GetGuidValueFromPropertyName(MyGenBO, "RegionId"))
            Me.Page.SetSelectedItem(moCountryDrop_WRITE, Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
        End If
    End Sub

    Public Sub PopulateControlFromBO()
        If Not Me.MyGenBO Is Nothing Then
            LoadRegionList()
            moCountryText.Text = String.Empty
            With Me.MyGenBO
                If Not Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId").Equals(Guid.Empty) Then
                    LoadCountryList(False)
                    moCountryText.Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
                Else
                    LoadCountryList()
                End If
                '# DEF-2818 Start
                If (moRegionDrop_WRITE.Visible) Or moCountryDrop_WRITE.SelectedIndex <> -1 Then ' If not visible that means this part is not applicable. 
                    Me.Page.SetSelectedItem(moRegionDrop_WRITE, Me.Page.GetGuidValueFromPropertyName(MyGenBO, "RegionId"))
                End If
                '# DEF-2818 End

                'Me.Page.SetSelectedItem(moRegionDrop_WRITE, Me.Page.GetGuidValueFromPropertyName(MyGenBO, "RegionId"))
                Me.Page.SetSelectedItem(moCountryDrop_WRITE, Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moAddress1Text, "Address1")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moAddress2Text, "Address2")
                'DEF-2173 Start
                If (Not Me.MyGenBO Is Nothing) Then
                    If Me.MyGenBO.GetType.Name.ToString() <> "CommissionEntity" Then
                        Me.Page.PopulateControlFromPropertyName(MyGenBO, moAddress3Text, "Address3")
                    End If
                End If
                'DEF-2173 End

                Me.Page.PopulateControlFromPropertyName(MyGenBO, moCityText, "City")
                Me.Page.PopulateControlFromPropertyName(MyGenBO, moPostalText, "PostalCode")

            End With
        End If
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnIncludeCountryId As Boolean = False, Optional ByVal blnUpdateZipLocator As Boolean = False)
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        'If MyBO.IsDeleted Then
        '    Exit Sub
        'End If
        If Not Me.MyGenBO Is Nothing AndAlso Not MyGenBO.IsDeleted Then
            With Me.MyGenBO
                Me.Page.PopulateBOProperty(Me.MyGenBO, "City", moCityText)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "Address1", moAddress1Text)
                Me.Page.PopulateBOProperty(Me.MyGenBO, "Address2", moAddress2Text)
                'DEF-2173 Start
                If (Not Me.MyGenBO Is Nothing) Then
                    If Me.MyGenBO.GetType.Name.ToString() <> "CommissionEntity" Then
                        Me.Page.PopulateBOProperty(Me.MyGenBO, "Address3", moAddress3Text)
                    End If
                End If
                'DEF-2173 End


                Me.Page.PopulateBOProperty(Me.MyGenBO, "PostalCode", moPostalText)
                If (blnUpdateZipLocator) Then
                    Me.Page.PopulateBOProperty(Me.MyGenBO, "ZipLocator", moPostalText)
                End If
                'Me.Page.PopulateBOProperty(Me.MyGenBO, "RegionId", moRegionDrop_WRITE)
                'DEF-2818 Start   :  Region control details will be applicable for changes only if this control is visible else not 
                If (moRegionDrop_WRITE.Visible) Then
                    Me.Page.PopulateBOProperty(Me.MyGenBO, "RegionId", moRegionDrop_WRITE)
                End If
                'DEF-2818 End

                If blnIncludeCountryId Then Me.Page.PopulateBOProperty(Me.MyGenBO, "CountryId", moCountryDrop_WRITE)
                SetLabelColor(moPostalLabel)
                SetLabelColor(moAddress1Label)
                SetLabelColor(moCityLabel)
                SetLabelColor(moRegionLabel)
                SetLabelColor(moCountryLabel)
                If TypeOf Me.MyGenBO Is Address Then
                    .Save()
                End If
            End With
        End If
    End Sub
    '' REQ-784
    Public Function PopulateBOFromAddressControl(ByVal MyAddressBO As Address) As Address
        If Not MyAddressBO Is Nothing AndAlso Not MyAddressBO.IsDeleted Then
            With MyAddressBO
                Me.Page.PopulateBOProperty(MyAddressBO, "City", moCityText)
                Me.Page.PopulateBOProperty(MyAddressBO, "Address1", moAddress1Text)
                Me.Page.PopulateBOProperty(MyAddressBO, "Address2", moAddress2Text)
                Me.Page.PopulateBOProperty(MyAddressBO, "Address3", moAddress3Text)
                Me.Page.PopulateBOProperty(MyAddressBO, "PostalCode", moPostalText)
                Me.Page.PopulateBOProperty(MyAddressBO, "RegionId", moRegionDrop_WRITE)
                Me.Page.PopulateBOProperty(MyAddressBO, "CountryId", moCountryDrop_WRITE)
            End With
        End If

        Return MyAddressBO
    End Function

    Public Function GetCountryValue() As Guid
        If moCountryDrop_WRITE.SelectedItem.Text <> "" Then
            GetCountryValue = Me.Page.GetSelectedItem(moCountryDrop_WRITE)
        Else
            GetCountryValue = Guid.Empty
        End If
    End Function

    Public Sub SetTheRequiredFields()
        If moAddress1Label.Text.Replace(SpanString, "").IndexOf("*") <> 0 Then Me.moAddress1Label.Text = "<span class=""mandatory"">*</span> " & Me.moAddress1Label.Text
        If moCityLabel.Text.Replace(SpanString, "").IndexOf("*") <> 0 Then Me.moCityLabel.Text = "<span class=""mandatory"">*</span> " & Me.moCityLabel.Text
        'If moCountryLabel.Text.IndexOf("*") <> 0 Then Me.moCountryLabel.Text = "* " & Me.moCountryLabel.Text
        IsAddressRequired = True
        'If moCountryDrop_WRITE.SelectedIndex <> -1 Then
        '    Dim oCountryID As Guid = New Guid(moCountryDrop_WRITE.SelectedItem.Value)
        '    If oCountryID <> Guid.Empty Then
        '        Dim strAddFmt As String = New Country(oCountryID).MailAddrFormat.ToUpper
        '        If Address.IsAddressComponentRequired(strAddFmt, "ZIP") AndAlso (Not moPostalLabel.Text.StartsWith("*")) Then
        '            moPostalLabel.Text = "* " & Me.moPostalLabel.Text
        '        End If
        '        If (Address.IsAddressComponentRequired(strAddFmt, "RGCODE") OrElse Address.IsAddressComponentRequired(strAddFmt, "RGNAME")) And (Not moRegionLabel.Text.StartsWith("*")) Then
        '            moRegionLabel.Text = "* " & Me.moRegionLabel.Text
        '        End If
        '    End If
        'End If
    End Sub


    Private Sub moPostalText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moPostalText.TextChanged
        Me.moPostalText.Text = Me.moPostalText.Text.Trim
    End Sub

    Public Shared Sub SetLabelColor(ByVal lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub
    Public Sub TranslateAllLabelControl()
        moAddress1Label.Text = TranslationBase.TranslateLabelOrMessage(moAddress1Label.Text)
        moCountryLabel.Text = TranslationBase.TranslateLabelOrMessage(moCountryLabel.Text)
        moAddress2Label.Text = TranslationBase.TranslateLabelOrMessage(moAddress2Label.Text)
        moRegionLabel.Text = TranslationBase.TranslateLabelOrMessage(moRegionLabel.Text)
        moAddress3Label.Text = TranslationBase.TranslateLabelOrMessage(moAddress3Label.Text)
        moCityLabel.Text = TranslationBase.TranslateLabelOrMessage(moCityLabel.Text)
        moPostalLabel.Text = TranslationBase.TranslateLabelOrMessage(moPostalLabel.Text)
        LabelPopupHeader.Text = TranslationBase.TranslateLabelOrMessage("IS_ADDRESS_VALIDATED")
        btnAccept.Text = TranslationBase.TranslateLabelOrMessage("ACCEPT_ADDRESS")
        btnDecline.Text = TranslationBase.TranslateLabelOrMessage("DECLINE_ADDRESS")
        btnValidate_Address.Text = TranslationBase.TranslateLabelOrMessage("VALIDATE_ADDRESS")
    End Sub
#End Region

#Region "Business"

#End Region

End Class
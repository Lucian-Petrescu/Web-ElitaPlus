Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Partial Class UserControlAddress
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

        Dim strCtyMailAddFmt As String

        strCtyMailAddFmt = New Country(oCountryID).MailAddrFormat.ToUpper
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
            Dim strAddFmt As String = New Country(oCountryID).MailAddrFormat.ToUpper
            If Address.IsAddressComponentRequired(strAddFmt, "ZIP") AndAlso (Not moPostalLabel.Text.StartsWith("*")) Then
                moPostalLabel.Text = "* " & Me.moPostalLabel.Text
            End If
            If (Address.IsAddressComponentRequired(strAddFmt, "RGCODE") OrElse Address.IsAddressComponentRequired(strAddFmt, "RGNAME")) And (Not moRegionLabel.Text.StartsWith("*")) Then
                moRegionLabel.Text = "* " & Me.moRegionLabel.Text
            End If
        End If
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
        moCountryDrop_WRITE.Visible = True
        moRegionDrop_WRITE.Visible = True
        moCountryDrop_WRITE.Enabled = True
        moRegionDrop_WRITE.Enabled = True
        moCountryText.ReadOnly = bvalue

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
            If Not moRegionDrop_WRITE Is Nothing Then
                moRegionText.Text = ElitaPlusPage.GetSelectedDescription(moRegionDrop_WRITE)
            Else
                moRegionText.Text = ""
            End If

            If Not moCountryDrop_WRITE Is Nothing Then
                moCountryText.Text = ElitaPlusPage.GetSelectedDescription(moCountryDrop_WRITE)
            Else
                moCountryText.Text = ""
            End If

            ControlMgr.SetVisibleControl(Page, moRegionText, True)
            moRegionText.ReadOnly = True
            ControlMgr.SetVisibleControl(Page, moRegionDrop_WRITE, False)
            moRegionText.BorderColor = Color.FromArgb(198, 198, 198)

            ControlMgr.SetVisibleControl(Page, moCountryDrop_WRITE, False)
            moCountryText.ReadOnly = True
            ControlMgr.SetVisibleControl(Page, moCountryText, True)
            moCountryText.BorderColor = Color.FromArgb(198, 198, 198)
        Else
            ControlMgr.SetVisibleControl(Page, moRegionText, False)
            ControlMgr.SetVisibleControl(Page, moRegionDrop_WRITE, True)
            ControlMgr.SetVisibleControl(Page, moCountryDrop_WRITE, False)
            If isNewCompany Then
                ControlMgr.SetVisibleControl(Page, moCountryText, False)
                ControlMgr.SetVisibleControl(Page, moCountryDrop_WRITE, True)
            End If
            moRegionText.ReadOnly = False
            moRegionText.BorderColor = Color.FromArgb(108, 108, 108)
            moCountryText.ReadOnly = False
            moCountryText.BorderColor = Color.FromArgb(108, 108, 108)
        End If
        moCountryText.ReadOnly = True
        moCountryText.CssClass = "FLATTEXTBOX"
        'moCountryText.BackColor = Color.FromArgb(228, 228, 238)
        moCountryText.BorderColor = Color.FromArgb(198, 198, 198)
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
        Dim CompanyGroupId As Guid
        CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'Dim oCountryList As DataView = LookupListNew.GetCompanyGroupCountryLookupList(CompanyGroupId)
        'Page.BindListControlToDataView(moCountryDrop_WRITE, oCountryList, , , nothingSelcted)

        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CompanyGroupId = CompanyGroupId
        Dim countryList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompanyGroup", context:=oListContext)
        moCountryDrop_WRITE.Populate(countryList, New PopulateOptions() With
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
            moCountryText.Text = ""
            moCountryText.Text = ""
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
            moCountryText.Text = ""
            moCountryText.Text = ""
            With Me.MyGenBO
                If Not Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId").Equals(Guid.Empty) Then
                    LoadCountryList(False)
                    moCountryText.Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
                Else
                    LoadCountryList()
                End If
                Me.Page.SetSelectedItem(moRegionDrop_WRITE, Me.Page.GetGuidValueFromPropertyName(MyGenBO, "RegionId"))
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

    Public Sub PopulateBOFromControl(Optional ByVal blnIncludeCountryId As Boolean = False)
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
                Me.Page.PopulateBOProperty(Me.MyGenBO, "RegionId", moRegionDrop_WRITE)
                If blnIncludeCountryId Then Me.Page.PopulateBOProperty(Me.MyGenBO, "CountryId", moCountryDrop_WRITE)
                SetLabelColor(moPostalLabel)

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
        If moAddress1Label.Text.IndexOf("*") <> 0 Then Me.moAddress1Label.Text = "* " & Me.moAddress1Label.Text
        If moCityLabel.Text.IndexOf("*") <> 0 Then Me.moCityLabel.Text = "* " & Me.moCityLabel.Text
        If moCountryLabel.Text.IndexOf("*") <> 0 Then Me.moCountryLabel.Text = "* " & Me.moCountryLabel.Text
        IsAddressRequired = True
    End Sub

    Private Sub moPostalText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moPostalText.TextChanged
        Me.moPostalText.Text = Me.moPostalText.Text.Trim
    End Sub

    Public Shared Sub SetLabelColor(ByVal lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub
#End Region

#Region "Business"

#End Region

End Class


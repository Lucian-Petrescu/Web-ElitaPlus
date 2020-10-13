Imports System.Collections.Generic
Imports System.Diagnostics
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Partial Class UserControlAddress
    Inherits UserControl


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
        Set(Value As Guid)
            _oCountryID = Value
        End Set
    End Property

    Public Property MyBO() As Address
        Get
            Return CType(MyGenBO, Address)
        End Get
        Set(Value As Address)
            MyGenBO = Value
        End Set
    End Property

    Public Property MyGenBO() As BusinessObjectBase
        Get
            Return CType(Page.StateSession.Item(UniqueID), BusinessObjectBase)
        End Get
        Set(Value As BusinessObjectBase)
            Page.StateSession.Item(UniqueID) = Value
        End Set
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

    Public Property RegionText() As String
        Get
            Return moRegionText.Text
        End Get
        Set(Value As String)
            moRegionText.Text = Value
        End Set
    End Property
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As Object

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load, Me.Load
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
            If (MyGenBO IsNot Nothing) Then
                If MyGenBO.GetType.Name.ToString() = "CommissionEntity" Then
                    ControlMgr.SetVisibleControl(Page, moAddress3Label, False)
                    ControlMgr.SetVisibleControl(Page, moAddress3Text, False)
                End If
            End If
            'DEF-2173 END


        End If


    End Sub

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
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
                moPostalLabel.Text = "* " & moPostalLabel.Text
            End If
            If (Address.IsAddressComponentRequired(strAddFmt, "RGCODE") OrElse Address.IsAddressComponentRequired(strAddFmt, "RGNAME")) AndAlso (Not moRegionLabel.Text.StartsWith("*")) Then
                moRegionLabel.Text = "* " & moRegionLabel.Text
            End If
        End If
    End Sub

#End Region

#Region "Control Management"
    Public Sub ChangeEnabledControlProperty(bEnable As Boolean)
        Page.ChangeEnabledControlProperty(moCityText, bEnable)
        Page.ChangeEnabledControlProperty(moAddress1Text, bEnable)
        Page.ChangeEnabledControlProperty(moAddress2Text, bEnable)
        Page.ChangeEnabledControlProperty(moAddress3Text, bEnable)
        Page.ChangeEnabledControlProperty(moPostalText, bEnable)
        Page.ChangeEnabledControlProperty(moRegionDrop_WRITE, bEnable)
        Page.ChangeEnabledControlProperty(moCountryText, bEnable)
    End Sub
    'Req 784
    Public Sub EnableDisablecontrol(bvalue As Boolean)

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
            moAddress1Text.TabIndex = TabIndexStartingNumber
            moCityText.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            moPostalText.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
            moAddress2Text.TabIndex = CType(TabIndexStartingNumber + 3, Int16)
            moRegionDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 6, Int16)
            moRegionText.TabIndex = CType(TabIndexStartingNumber + 5, Int16)
            moCountryDrop_WRITE.TabIndex = CType(TabIndexStartingNumber + 4, Int16)
            moCountryText.TabIndex = CType(TabIndexStartingNumber + 7, Int16)
        End If
    End Sub

    Public Sub EnableControls(bEnable As Boolean, Optional ByVal isNewCompany As Boolean = False)
        Page.EnableDisableControls(Me, bEnable)

        If bEnable Then
            If moRegionDrop_WRITE IsNot Nothing Then
                moRegionText.Text = ElitaPlusPage.GetSelectedDescription(moRegionDrop_WRITE)
            Else
                moRegionText.Text = ""
            End If

            If moCountryDrop_WRITE IsNot Nothing Then
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

    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged
        oCountryID = New Guid(moCountryDrop_WRITE.SelectedItem.Value)

        Dim oListContext As New ListContext
        oListContext.CountryId = oCountryID
        Dim oRegionList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
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

        Dim oListContext As New ListContext
        oListContext.CountryId = Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId")
        Dim oRegionList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
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

        Dim oListContext As New ListContext
        oListContext.CompanyGroupId = CompanyGroupId
        Dim countryList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompanyGroup", context:=oListContext)
        moCountryDrop_WRITE.Populate(countryList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = nothingSelcted
                                           })

    End Sub

    Public Sub Bind(oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If
        PopulateControlFromBO()
    End Sub

    '' REQ-784
    Public Sub NewClaimBind(oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If
        PopulateNewClaimControlFromBO()

    End Sub

    '' REQ-784
    Public Sub ClaimDetailsBind(oBusObj As BusinessObjectBase)
        MyGenBO = oBusObj
        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If
        PopulateControlFromBO()

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Page.BindBOPropertyToLabel(MyGenBO, POSTALCODE_PROPERTY, moPostalLabel)
        Page.BindBOPropertyToLabel(MyGenBO, ADDRESS1_PROPERTY, moAddress1Label)
        Page.BindBOPropertyToLabel(MyGenBO, ADDRESS2_PROPERTY, moAddress2Label)
        Page.BindBOPropertyToLabel(MyGenBO, ADDRESS3_PROPERTY, moAddress3Label)
        Page.BindBOPropertyToLabel(MyGenBO, CITY_PROPERTY, moCityLabel)
        Page.BindBOPropertyToLabel(MyGenBO, CONTRYID_PROPERTY, moCountryLabel)
        Page.BindBOPropertyToLabel(MyGenBO, REGIONID_PROPERTY, moRegionLabel)
        'Me.Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    '' REQ-784
    Public Sub PopulateNewClaimControlFromBO()
        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If
        If MyGenBO IsNot Nothing Then
            LoadRegionList()
            moCountryText.Text = ""
            moCountryText.Text = ""
            If Not Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId").Equals(Guid.Empty) Then
                LoadCountryList(False)
                moCountryText.Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
            Else
                LoadCountryList()
            End If
            Page.SetSelectedItem(moRegionDrop_WRITE, Page.GetGuidValueFromPropertyName(MyGenBO, "RegionId"))
            Page.SetSelectedItem(moCountryDrop_WRITE, Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
        End If
    End Sub
   
    Public Sub PopulateControlFromBO()
        If MyGenBO IsNot Nothing Then
            LoadRegionList()
            moCountryText.Text = ""
            moCountryText.Text = ""
            With MyGenBO
                If Not Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId").Equals(Guid.Empty) Then
                    LoadCountryList(False)
                    moCountryText.Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
                Else
                    LoadCountryList()
                End If
                Page.SetSelectedItem(moRegionDrop_WRITE, Page.GetGuidValueFromPropertyName(MyGenBO, "RegionId"))
                Page.SetSelectedItem(moCountryDrop_WRITE, Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
                Page.PopulateControlFromPropertyName(MyGenBO, moAddress1Text, "Address1")
                Page.PopulateControlFromPropertyName(MyGenBO, moAddress2Text, "Address2")
                'DEF-2173 Start
                If (MyGenBO IsNot Nothing) Then
                    If MyGenBO.GetType.Name.ToString() <> "CommissionEntity" Then
                        Page.PopulateControlFromPropertyName(MyGenBO, moAddress3Text, "Address3")
                    End If
                End If
                'DEF-2173 End

                Page.PopulateControlFromPropertyName(MyGenBO, moCityText, "City")
                Page.PopulateControlFromPropertyName(MyGenBO, moPostalText, "PostalCode")

            End With
        End If
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnIncludeCountryId As Boolean = False)
        If MyGenBO IsNot Nothing Then
            BindBoPropertiesToLabels()
            Page.AddLabelDecorations(MyGenBO)
        End If
        'If MyBO.IsDeleted Then
        '    Exit Sub
        'End If
        If MyGenBO IsNot Nothing AndAlso Not MyGenBO.IsDeleted Then
            With MyGenBO
                Page.PopulateBOProperty(MyGenBO, "City", moCityText)
                Page.PopulateBOProperty(MyGenBO, "Address1", moAddress1Text)
                Page.PopulateBOProperty(MyGenBO, "Address2", moAddress2Text)
                'DEF-2173 Start
                If (MyGenBO IsNot Nothing) Then
                    If MyGenBO.GetType.Name.ToString() <> "CommissionEntity" Then
                        Page.PopulateBOProperty(MyGenBO, "Address3", moAddress3Text)
                    End If
                End If
                'DEF-2173 End


                Page.PopulateBOProperty(MyGenBO, "PostalCode", moPostalText)
                Page.PopulateBOProperty(MyGenBO, "RegionId", moRegionDrop_WRITE)
                If blnIncludeCountryId Then Page.PopulateBOProperty(MyGenBO, "CountryId", moCountryDrop_WRITE)
                SetLabelColor(moPostalLabel)

                If TypeOf MyGenBO Is Address Then
                    .Save()
                End If
            End With
        End If
    End Sub
    '' REQ-784
    Public Function PopulateBOFromAddressControl(MyAddressBO As Address) As Address
        If MyAddressBO IsNot Nothing AndAlso Not MyAddressBO.IsDeleted Then
            With MyAddressBO
                Page.PopulateBOProperty(MyAddressBO, "City", moCityText)
                Page.PopulateBOProperty(MyAddressBO, "Address1", moAddress1Text)
                Page.PopulateBOProperty(MyAddressBO, "Address2", moAddress2Text)
                Page.PopulateBOProperty(MyAddressBO, "Address3", moAddress3Text)
                Page.PopulateBOProperty(MyAddressBO, "PostalCode", moPostalText)
                Page.PopulateBOProperty(MyAddressBO, "RegionId", moRegionDrop_WRITE)
                Page.PopulateBOProperty(MyAddressBO, "CountryId", moCountryDrop_WRITE)
            End With
        End If

        Return MyAddressBO
    End Function

    Public Function GetCountryValue() As Guid
        If moCountryDrop_WRITE.SelectedItem.Text <> "" Then
            GetCountryValue = Page.GetSelectedItem(moCountryDrop_WRITE)
        Else
            GetCountryValue = Guid.Empty
        End If
    End Function

    Public Sub SetTheRequiredFields()
        If moAddress1Label.Text.IndexOf("*") <> 0 Then moAddress1Label.Text = "* " & moAddress1Label.Text
        If moCityLabel.Text.IndexOf("*") <> 0 Then moCityLabel.Text = "* " & moCityLabel.Text
        If moCountryLabel.Text.IndexOf("*") <> 0 Then moCountryLabel.Text = "* " & moCountryLabel.Text
        IsAddressRequired = True
    End Sub

    Private Sub moPostalText_TextChanged(sender As Object, e As EventArgs) Handles moPostalText.TextChanged
        moPostalText.Text = moPostalText.Text.Trim
    End Sub

    Public Shared Sub SetLabelColor(lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub
#End Region

#Region "Business"

#End Region

End Class


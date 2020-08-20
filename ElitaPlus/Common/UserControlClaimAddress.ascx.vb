Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Partial Class UserControlClaimAddress
    Inherits System.Web.UI.UserControl

#Region "Constants"
    Public Const ADDRESS1_PROPERTY As String = "Address1"
    Public Const ADDRESS2_PROPERTY As String = "Address2"
    Public Const ADDRESS3_PROPERTY As String = "Address3"
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


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object



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
                                       .AddBlankItem = True,
                                       .SortFunc = AddressOf PopulateOptions.GetExtendedCode
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

    Public Sub Bind(ByVal oBusObj As BusinessObjectBase, ByVal ProfileCode As String)
        MyGenBO = oBusObj
        If Not Me.MyGenBO Is Nothing Then
            BindBoPropertiesToLabels()
            MyBO.ProfileCode = ProfileCode
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
        Me.PopulateControlFromBO()
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

    Public Sub PopulateControlFromBO()
        If Not Me.MyGenBO Is Nothing Then
            LoadRegionList()
            'moCountryText.Text = String.Empty
            With Me.MyGenBO
                If Not Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId").Equals(Guid.Empty) Then
                    LoadCountryList(False)
                    'moCountryText.Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
                    'moCountryDrop_WRITE.SelectedItem.Text
                    Me.Page.SetSelectedItem(moCountryDrop_WRITE, Me.Page.GetGuidValueFromPropertyName(MyGenBO, "CountryId"))
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
        IsAddressRequired = True

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

    End Sub


#End Region

    Private ReadOnly Property ParentPage As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
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

        moAddress1Text.Rows = 3
        moAddress2Text.Rows = 3
        moAddress3Text.Rows = 3
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        Dim controlname As String = moRegionDrop_WRITE.Parent.ID & "_moRegionDrop_WRITE"
        moRegionDrop_WRITE.Attributes.Add("onchange", String.Format("getComunaList('{0}');", controlname))
        Dim textcontrolname As String = moPostalText.Parent.ID & "_moPostalText"
        moPostalText.Attributes.Add("onkeyup", String.Format("return getList('{0}','{1}')", textcontrolname, controlname))
    End Sub


    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged
        oCountryID = New Guid(moCountryDrop_WRITE.SelectedItem.Value)
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CountryId = oCountryID
        Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
        moRegionDrop_WRITE.Populate(oRegionList, New PopulateOptions() With
                                       {
                                       .AddBlankItem = True,
                                       .SortFunc = AddressOf PopulateOptions.GetExtendedCode
                                       })

     
    End Sub

End Class
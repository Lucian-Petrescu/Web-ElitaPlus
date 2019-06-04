Namespace Interfaces
    Partial Class ServiceClassServiceTypeControl
        Inherits System.Web.UI.UserControl

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

#Region "Constants"

        Private Const NUM_POS_FIELD_SEPARATOR As Integer = 5
        Private Const SINGLE_ITEM As Integer = 1
        Public Const NO_CAPTION As String = "00000"

        Public Enum MODES
            NEW_MODE = 1
            EDIT_MODE = 2
        End Enum


#End Region

#Region "Variables"

        Private msClassColumnName As String = "CODE"
        Private msCaption As String = ""
        'Private msTypeColumnName As String = "TYPE"
        Private msGuidValueColumnName As String = "ID"
        Private mbAddNothingSelected As Boolean = True
        Private mnCodeFieldLength As Integer = 10
        Private mnStartDropIndex As Integer = 0
        Public Event SelectedDropChanged(ByVal aSrc As ServiceClassServiceTypeControl)
        Private moAdditionalOnClickJavaScript As String = ""
        Private moClassControlID As String = "ServiceClassServiceTypeControl_cboClass"
        Private moTypeControlID As String = "ServiceClassServiceTypeControl_cboType"
        Private moCaptionControlID As String = "ServiceClassServiceTypeControl_lb_DropDown"
        Private mMode As Integer
        Private moOmitRegisteringJavaScriptCode As Boolean = False
        Private msServiceClass As String = "SVCCLASS"
        Private msServiceType As String = "SVCTYP"
        Private mbDisabled As Boolean = False
#End Region

#Region "Properties"

        Public Property Caption() As String
            Get
                Return msCaption
            End Get
            Set(ByVal Value As String)
                msCaption = Value
            End Set
        End Property
        Public Property ClassColumnName() As String
            Get
                Return msClassColumnName
            End Get
            Set(ByVal Value As String)
                msClassColumnName = Value
            End Set
        End Property

        'Public Property TypeColumnName() As String
        '    Get
        '        Return msTypeColumnName
        '    End Get
        '    Set(ByVal Value As String)
        '        msTypeColumnName = Value
        '    End Set
        'End Property

        Public Property GuidValueColumnName() As String
            Get
                Return msGuidValueColumnName
            End Get
            Set(ByVal Value As String)
                msGuidValueColumnName = Value
            End Set
        End Property

        Public Property NothingSelected() As Boolean
            Get
                Return mbAddNothingSelected
            End Get
            Set(ByVal Value As Boolean)
                mbAddNothingSelected = Value
            End Set
        End Property

        Public Property CodeFieldLength() As Integer
            Get
                Return mnCodeFieldLength
            End Get
            Set(ByVal Value As Integer)
                mnCodeFieldLength = Value
            End Set
        End Property

        Private ReadOnly Property DescStartIndex() As Integer
            Get
                Dim nStartIndex As Integer = CodeFieldLength + NUM_POS_FIELD_SEPARATOR
                Return nStartIndex
            End Get

        End Property

        Public Property StartDropIndex() As Integer
            Get
                Return mnStartDropIndex
            End Get
            Set(ByVal Value As Integer)
                mnStartDropIndex = Value
            End Set
        End Property

        Public Property SpecialServiceGuid() As Guid
            Get
                Dim oGuid As Guid = ElitaPlusPage.GetSelectedItem(cboServiceClass)
                Return oGuid
            End Get
            Set(ByVal Value As Guid)
                'ElitaPlusPage.BindSelectItem(Value.ToString, cboServiceClass)
                'ElitaPlusPage.BindSelectItem(Value.ToString, cboServiceType)
            End Set
        End Property

        Public Property ServiceClassGuid() As Guid
            Get
                Dim oGuid As Guid = ElitaPlusPage.GetSelectedItem(cboServiceClass)
                Return oGuid
            End Get
            Set(ByVal Value As Guid)
                ElitaPlusPage.BindSelectItem(Value.ToString, cboServiceClass)
                'moTextBoxServiceClass.Text = cboServiceClass.SelectedItem.Text.ToString()
                LoadServiceTypes()
                'ElitaPlusPage.BindSelectItem(Value.ToString, moMultipleColumnDropDesc)
            End Set
        End Property
        Public Property ServiceTypeGuid() As Guid
            Get
                Dim oGuid As Guid = ElitaPlusPage.GetSelectedItem(cboServiceType)
                Return oGuid
            End Get
            Set(ByVal Value As Guid)
                ElitaPlusPage.BindSelectItem(Value.ToString, cboServiceType)
                'moTextBoxServiceType.Text = cboServiceType.SelectedItem.Text.ToString()
            End Set
        End Property

        Public ReadOnly Property SelectedClass() As String
            Get
                Dim sCode As String
                Dim sText As String = ElitaPlusPage.GetSelectedDescription(cboServiceClass)
                Return sText
            End Get
        End Property

        Public ReadOnly Property SelectedType() As String
            Get
                Dim sDesc As String
                Dim sText As String = ElitaPlusPage.GetSelectedDescription(cboServiceType)

                ' If sText = String.Empty Then
                'sDesc = sText
                'Else
                '   sDesc = sText.Substring(DescStartIndex)
                'End If
                Return sText
            End Get
        End Property

        Public Property SelectedServiceClassIndex() As Integer
            Get
                Return cboServiceClass.SelectedIndex
            End Get
            Set(ByVal Value As Integer)
                cboServiceClass.SelectedIndex = Value
                'moMultipleColumnDropDesc.SelectedIndex = Value
            End Set
        End Property
        Public Property SelectedServiceTypeIndex() As Integer
            Get
                Return cboServiceType.SelectedIndex
            End Get
            Set(ByVal Value As Integer)
                cboServiceType.SelectedIndex = Value
            End Set
        End Property

        Public Property AdditionalOnClickJavaScript() As String
            Get
                Return moAdditionalOnClickJavaScript
            End Get
            Set(ByVal Value As String)
                moAdditionalOnClickJavaScript = Value
            End Set
        End Property

        Public Property DescriptionHTMLID() As String
            Get
                Return moTypeControlID
            End Get
            Set(ByVal Value As String)
                moTypeControlID = Value
            End Set
        End Property

        Public Property CodeControlHTMLID() As String
            Get
                Return moClassControlID
            End Get
            Set(ByVal Value As String)
                moClassControlID = Value
            End Set
        End Property

        Public Property CaptionHTMLID() As String
            Get
                Return moCaptionControlID
            End Get
            Set(ByVal Value As String)
                moCaptionControlID = Value
            End Set
        End Property

        Public Property Disabled() As Boolean
            Get
                Return mbDisabled
            End Get
            Set(ByVal Value As Boolean)
                mbDisabled = Value
            End Set
        End Property

        Public Property AutoPostBackDD() As Boolean
            Get
                Return cboServiceClass.AutoPostBack
            End Get
            Set(ByVal Value As Boolean)
                cboServiceClass.AutoPostBack = Value
                'cboServiceType.AutoPostBack = Value
                If Not Value Then
                    cboServiceClass.Attributes.Add("onchange", Me.AdditionalOnClickJavaScript & "ToggleSelection('" & Me.cboServiceClass.ClientID & "', '" & Me.cboServiceType.ClientID & "', 'D', '')")
                    'cboServiceType.Attributes.Add("onchange", Me.AdditionalOnClickJavaScript & "ToggleSelection('" & Me.cboServiceClass.ClientID & "', '" & Me.cboServiceType.ClientID & "', 'C', '')")
                    'If Not Me.moOmitRegisteringJavaScriptCode Then
                    '    Me.RegisterJavaScriptCode()
                    'End If

                End If
            End Set
        End Property

        Public Property Mode() As Integer
            Get
                Return mMode
            End Get
            Set(ByVal Value As Integer)
                mMode = Value
                Select Case Value
                    Case Me.MODES.EDIT_MODE
                        Me.cboServiceClass.Visible = False
                        Me.cboServiceType.Visible = False
                        'Me.ServiceClassText.Visible = True
                        'Me.ServiceTypeText.Visible = True
                        'Me.lb_DropDown.Enabled = False
                        'Me.ServiceClassLabel.Enabled = False
                        'Me.ServiceTypeLabel.Enabled = False
                    Case Me.MODES.NEW_MODE
                        Me.cboServiceClass.Visible = True
                        Me.cboServiceType.Visible = True
                        'Me.ServiceClassText.Visible = False
                        'Me.ServiceTypeText.Visible = False
                        'Me.lb_DropDown.Enabled = True
                        'Me.ServiceClassLabel.Enabled = True
                        'Me.ServiceTypeLabel.Enabled = True
                End Select

            End Set
        End Property

        Public Property VisibleDD() As Boolean
            Get
                Return cboServiceClass.Visible
                Return cboServiceType.Visible
            End Get
            Set(ByVal Value As Boolean)
                cboServiceClass.Visible = Value
                cboServiceType.Visible = Value
            End Set
        End Property

        Public Property Width() As Unit
            Get
                Return cboServiceClass.Width
            End Get
            Set(ByVal Value As Unit)
                cboServiceClass.Width = Value
            End Set
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return cboServiceClass.Items.Count
            End Get
        End Property

        Public ReadOnly Property IsSelectedTheLastItem() As Boolean
            Get
                Dim bIsLast As Boolean = (SelectedServiceClassIndex = (Count - 1))
                Return bIsLast
            End Get
        End Property

        Private Shadows ReadOnly Property Page() As ElitaPlusPage
            Get
                Return CType(MyBase.Page, ElitaPlusPage)
            End Get
        End Property

        Public ReadOnly Property CaptionLabel() As Label
            Get
                Return CType(FindControl("lb_DropDown"), Label)
            End Get
        End Property

        Public ReadOnly Property ServiceClassTextBox() As TextBox
            Get
                Return CType(FindControl("ServiceClassText"), TextBox)
            End Get
        End Property

        Public ReadOnly Property ServiceTypeTextBox() As TextBox
            Get
                Return CType(FindControl("ServiceTypeText"), TextBox)
            End Get
        End Property
        Public ReadOnly Property ServiceClassDropDown() As DropDownList
            Get
                Return cboServiceClass

            End Get
        End Property

        Public ReadOnly Property ServiceTypeDropDown() As DropDownList
            Get
                Return cboServiceType
            End Get
        End Property
#End Region

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                'If Not Me.Caption.Equals(String.Empty) Then
                '    Me.lb_DropDown.Text = Me.Caption.Replace(":", "") + ":"
                'End If
                cboServiceType.Enabled = False
                If Not Me.cboServiceClass.SelectedValue.Equals(Nothing) Then
                    'Me.cboServiceType.SelectedIndex = -1
                    If Not Me.cboServiceType.Items.Count.Equals(0) Then
                        'Me.cboServiceType.Items.FindByValue(GuidControl.GuidToHexString(Me.ServiceTypeGuid)).Selected = True
                        cboServiceType.SelectedValue = Me.ServiceTypeGuid.ToString()

                        If Not Guid.Empty = ServiceClassGuid Then
                            cboServiceType.Enabled = True
                        End If
                    End If
                End If
                'ElseIf Not cboServiceClass.AutoPostBack Then
                '    Me.RegisterJavaScriptCode()
            End If
        End Sub

#Region "Handlers-DropDown"

        Private Sub moMultipleColumnDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboServiceClass.SelectedIndexChanged
            Me.cboServiceType.SelectedIndex = -1
            'Me.cboServiceType.Items.FindByValue(cboServiceClass.SelectedValue).Selected = True
            LoadServiceTypes()
            If cboServiceClass.SelectedIndex > 0 Then
                cboServiceType.Enabled = True
            Else
                cboServiceType.SelectedIndex = 0
                cboServiceType.Enabled = False
            End If

            RaiseEvent SelectedDropChanged(Me)
        End Sub

        Private Sub LoadServiceTypes()
            Try
                If ServiceClassGuid <> Guid.Empty Then
                    cboServiceType.Items.Clear()
                    Dim dsServiceTypes As DataSet
                    dsServiceTypes = SpecialService.getServiceTypesForServiceClass(ElitaPlusPage.GetSelectedItem(cboServiceClass), ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                    Dim dv As DataView = dsServiceTypes.Tables(0).DefaultView

                    dv.Sort = ClassColumnName
                    ElitaPlusPage.BindListControlToDataView(cboServiceType, dv, ClassColumnName, , True)

                    'ElitaPlusPage.BindSelectItem(GuidControl.GuidToHexString(Me.ServiceTypeGuid), cboServiceType)
                End If
            Catch ex As Exception

            End Try
        End Sub

        'Private Sub moMultipleColumnDropDesc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboServiceType.SelectedIndexChanged
        '    Me.cboServiceClass.SelectedIndex = -1
        '    Me.cboServiceClass.Items.FindByValue(cboServiceType.SelectedValue).Selected = True
        '    RaiseEvent SelectedDropChanged(Me)
        'End Sub
#End Region

#Region "Utilities"

        'Private Function setSpace(ByVal numberOfSpaces As Integer) As String

        '    Dim Spaces As String
        '    Dim i As Integer
        '    For i = 0 To numberOfSpaces
        '        Spaces &= "&nbsp;"
        '    Next

        '    Return Server.HtmlDecode(Spaces)

        'End Function

        'Public Sub ClearMultipleDrop()
        '    ElitaPlusPage.ClearList(cboServiceClass)
        'End Sub

#End Region

#Region "Creates a Multiple Column DropDown Based on DataView"

        'Private Sub BindCodeDescToList(ByVal lstControl As ListControl, ByVal Data As DataView, ByVal ColumnName As String)
        '    Dim i As Integer
        '    Dim sCode, sDesc, sGuid, sListText As String
        '    Dim cFiller As Char = "_".Chars(0)
        '    Dim oAttr As Attribute
        '    Dim oDataViewDesc As DataView = Data

        '    lstControl.Items.Clear()
        '    If NothingSelected Then
        '        lstControl.Items.Add(New ListItem("", Guid.Empty.ToString))
        '    End If
        '    If Not Data Is Nothing Then
        '        For i = StartDropIndex To Data.Count - 1

        '            sCode = Data(i)(ClassColumnName).ToString
        '            'sDesc = Data(i)(TextColumnName).ToString
        '            sDesc = oDataViewDesc(i)(ClassColumnName).ToString

        '            sListText = sCode & setSpace(CodeFieldLength + 1 - sCode.Length) & " | " & sDesc
        '            sGuid = New Guid(CType(Data(i)(GuidValueColumnName), Byte())).ToString
        '            'lstControl.Items.Add(New ListItem(sListText, sGuid))
        '            lstControl.Items.Add(New ListItem(sCode, sGuid))
        '        Next
        '    End If

        'End Sub

        Private Sub LoadDropdownData()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim dv As DataView

                dv = LookupListNew.GetServiceClassLookupList(oLanguageId, True)
                dv.Sort = ClassColumnName
                ElitaPlusPage.BindListControlToDataView(cboServiceClass, dv, ClassColumnName, , True)

                dv = Nothing
                dv = LookupListNew.GetNewServiceTypeLookupList(oLanguageId, True)
                dv.Sort = ClassColumnName
                ElitaPlusPage.BindListControlToDataView(cboServiceType, dv, ClassColumnName, , True)

            Catch ex As Exception

            End Try
        End Sub

        'Public Sub BindData(ByVal oDataView As DataView)

        '    oDataView.Sort = ClassColumnName
        '    ElitaPlusPage.BindListControlToDataView(cboServiceClass, oDataView, ClassColumnName, , mbAddNothingSelected)
        '    oDataView.Sort = ClassColumnName
        '    ElitaPlusPage.BindListControlToDataView(cboServiceType, oDataView, ClassColumnName, , mbAddNothingSelected)

        'End Sub
        Public Sub ChangeEnabledControlProperty(ByVal blnEnabledState As Boolean)
            Page.ChangeEnabledControlProperty(Me.cboServiceClass, blnEnabledState)
            Page.ChangeEnabledControlProperty(Me.cboServiceType, blnEnabledState)
        End Sub

        Public Sub SetControl(ByVal AutoPostBack As Boolean, _
                              ByVal Mode As Integer, _
                              ByVal NothingSelected As Boolean, _
                              Optional ByVal dv As DataView = Nothing, _
                              Optional ByVal Caption As String = "", _
                              Optional ByVal overRideSingularity As Boolean = False, _
                              Optional ByVal addColonToCaption As Boolean = True, _
                              Optional ByVal additionalOnClickJavaScript As String = "", _
                              Optional ByVal Code_HTML_ID As String = "", _
                              Optional ByVal Description_HTML_ID As String = "", _
                              Optional ByVal Caption_HTML_ID As String = "", _
                              Optional ByVal OmitRegisteringJavaScriptCode As Boolean = False, _
                              Optional ByVal TabIndexStartingNumber As Int16 = 0, _
                              Optional ByVal disabled As Boolean = False)

            If Not Code_HTML_ID = "" Then Me.CodeControlHTMLID = Code_HTML_ID
            If Not Description_HTML_ID = "" Then Me.DescriptionHTMLID = Description_HTML_ID
            If Not Caption_HTML_ID = "" Then Me.CaptionHTMLID = Caption_HTML_ID

            Me.AdditionalOnClickJavaScript = additionalOnClickJavaScript
            Me.moOmitRegisteringJavaScriptCode = OmitRegisteringJavaScriptCode
            Me.AutoPostBackDD = AutoPostBack

            If Caption = "" Then
                Me.Caption = TranslationBase.TranslateLabelOrMessage("COMPANY")
            ElseIf Not Caption.Equals(Me.NO_CAPTION) Then
                Me.Caption = Caption
                'Me.lb_DropDown.Text = Me.Caption
                ' If addColonToCaption AndAlso Me.lb_DropDown.Text.IndexOf(":") < 0 Then Me.lb_DropDown.Text &= ":"
            End If

            Me.NothingSelected = NothingSelected
            Me.Disabled = disabled

            'If dv Is Nothing Then dv = LookupListNew.GetUserCompaniesLookupList()
            'Me.BindData(dv)
            LoadDropdownData()

            If Not overRideSingularity Then
                overRideSingularity = dv.Count > Me.SINGLE_ITEM
            End If

            If Mode = Me.MODES.NEW_MODE And overRideSingularity Then
                Me.Mode = Me.MODES.NEW_MODE
                Me.cboServiceType.Enabled = False
            Else
                Me.NothingSelected = False
                Me.Mode = Me.MODES.EDIT_MODE
                'Me.moTextBoxServiceClass.Text = LookupListNew.GetCodeFromId(dv, Me.Page.GetSelectedItem(Me.cboServiceClass)) 'CType(dv.Item(0).Item(0), String)
                'Me.moTextBoxServiceType.Text = LookupListNew.GetCodeFromId(dv, Me.Page.GetSelectedItem(Me.cboServiceType))
            End If

            If TabIndexStartingNumber > 0 Then
                cboServiceClass.TabIndex = TabIndexStartingNumber
                cboServiceType.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            End If

            If Me.Disabled = True Then
                cboServiceClass.SelectedIndex = -1
                cboServiceType.SelectedIndex = -1
                cboServiceClass.Visible = False
                cboServiceType.Visible = False
                moTextBoxServiceClass.Visible = True
                moTextBoxServiceType.Visible = True
            Else
                cboServiceClass.Visible = True
                cboServiceType.Visible = True
                moTextBoxServiceClass.Visible = False
                moTextBoxServiceType.Visible = False
                cboServiceClass.Enabled = True
                cboServiceType.Enabled = True
            End If
        End Sub

        'Private Sub RegisterJavaScriptCode()
        '    Dim sJavaScript As String
        '    sJavaScript = "<SCRIPT>" & Environment.NewLine
        '    sJavaScript &= "function ToggleSelection(ctlCodeDropDown, ctlDescDropDown, change_Desc_Or_Code, lblCaption)" & Environment.NewLine
        '    sJavaScript &= "{" & Environment.NewLine & "var objCodeDropDown = document.getElementById(ctlCodeDropDown);" & Environment.NewLine
        '    sJavaScript &= "var objDescDropDown = document.getElementById(ctlDescDropDown);" & Environment.NewLine
        '    'sJavaScript &= " alert( 'Code name = ' + ctlCodeDropDown + ' obj =' + objCodeDropDown + '\n  Desc name = ' + ctlDescDropDown + ' obj =' + objDescDropDown + '\n  Caption name = ' + lblCaption + ' obj =' + document.getElementById(lblCaption));" & Environment.NewLine
        '    sJavaScript &= "if (change_Desc_Or_Code=='C'){" & Environment.NewLine & "objCodeDropDown.value = objDescDropDown.options[objDescDropDown.selectedIndex].value;}" & Environment.NewLine
        '    sJavaScript &= "else { objDescDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;}" & Environment.NewLine
        '    sJavaScript &= "if (lblCaption != '') {document.all.item(lblCaption).style.color = '';}}" & Environment.NewLine
        '    sJavaScript &= "</SCRIPT>" & Environment.NewLine
        '    Me.Page.RegisterStartupScript("ToggleDropDown", sJavaScript)
        'End Sub


#End Region
    End Class
End Namespace
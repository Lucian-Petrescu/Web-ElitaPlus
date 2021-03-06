Imports elp = Assurant.ElitaPlus

Namespace Common

    Partial Class MultipleColumnDDLabelControl
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

        Private msCodeColumnName As String = "CODE"
        Private msCaption As String = ""
        Private msTextColumnName As String = "DESCRIPTION"
        Private msGuidValueColumnName As String = "ID"
        Private mbAddNothingSelected As Boolean = False
        Private mnCodeFieldLength As Integer = 10
        Private mnStartDropIndex As Integer = 0
        Public Event SelectedDropChanged(ByVal aSrc As MultipleColumnDDLabelControl)
        Private moAdditionalOnClickJavaScript As String = ""
        Private moCodeControlID As String = "multipleDropControl_moMultipleColumnDrop"
        Private moDescriptionControlID As String = "multipleDropControl_moMultipleColumnDropDesc"
        Private moCaptionControlID As String = "multipleDropControl_lb_DropDown"
        Private mMode As Integer
        Private moOmitRegisteringJavaScriptCode As Boolean = False
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
        Public Property CodeColumnName() As String
            Get
                Return msCodeColumnName
            End Get
            Set(ByVal Value As String)
                msCodeColumnName = Value
            End Set
        End Property

        Public Property TextColumnName() As String
            Get
                Return msTextColumnName
            End Get
            Set(ByVal Value As String)
                msTextColumnName = Value
            End Set
        End Property

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

        Public Property SelectedGuid() As Guid
            Get
                Dim oGuid As Guid = ElitaPlusPage.GetSelectedItem(moMultipleColumnDrop)
                Return oGuid
            End Get
            Set(ByVal Value As Guid)
                ElitaPlusPage.BindSelectItem(Value.ToString, moMultipleColumnDrop)
                ElitaPlusPage.BindSelectItem(Value.ToString, moMultipleColumnDropDesc)
            End Set
        End Property

        Public ReadOnly Property SelectedCode() As String
            Get
                Dim sCode As String
                Dim sText As String = ElitaPlusPage.GetSelectedDescription(moMultipleColumnDrop)

                'If sText = String.Empty Then
                'sCode = sText
                'Else
                '    sCode = sText.Substring(0, CodeFieldLength + 1).TrimEnd
                'End If
                Return sText
            End Get
        End Property

        Public ReadOnly Property SelectedDesc() As String
            Get
                Dim sDesc As String
                Dim sText As String = ElitaPlusPage.GetSelectedDescription(moMultipleColumnDropDesc)

                ' If sText = String.Empty Then
                'sDesc = sText
                'Else
                '   sDesc = sText.Substring(DescStartIndex)
                'End If
                Return sText
            End Get
        End Property

        Public Property SelectedIndex() As Integer
            Get
                Return moMultipleColumnDrop.SelectedIndex
            End Get
            Set(ByVal Value As Integer)
                moMultipleColumnDrop.SelectedIndex = Value
                moMultipleColumnDropDesc.SelectedIndex = Value
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
                Return moDescriptionControlID
            End Get
            Set(ByVal Value As String)
                moDescriptionControlID = Value
            End Set
        End Property

        Public Property CodeControlHTMLID() As String
            Get
                Return moCodeControlID
            End Get
            Set(ByVal Value As String)
                moCodeControlID = Value
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

        Public Property AutoPostBackDD() As Boolean
            Get
                Return moMultipleColumnDrop.AutoPostBack
            End Get
            Set(ByVal Value As Boolean)
                moMultipleColumnDrop.AutoPostBack = Value
                moMultipleColumnDropDesc.AutoPostBack = Value
                If Not Value Then
                    moMultipleColumnDrop.Attributes.Add("onchange", Me.AdditionalOnClickJavaScript & "ToggleSelection('" & Me.moMultipleColumnDrop.ClientID & "', '" & Me.moMultipleColumnDropDesc.ClientID & "', 'D', '" & Me.lb_DropDown.ClientID & "')")
                    moMultipleColumnDropDesc.Attributes.Add("onchange", Me.AdditionalOnClickJavaScript & "ToggleSelection('" & Me.moMultipleColumnDrop.ClientID & "', '" & Me.moMultipleColumnDropDesc.ClientID & "', 'C', '" & Me.lb_DropDown.ClientID & "')")
                    If Not Me.moOmitRegisteringJavaScriptCode Then
                        Me.RegisterJavaScriptCode()
                    End If

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
                        Me.moMultipleColumnDrop.Visible = False
                        Me.moMultipleColumnDropDesc.Visible = False
                        Me.moMultipleColumnTextBoxCode.Visible = True
                        Me.moMultipleColumnTextBoxDesc.Visible = True
                        Me.lb_DropDown.Enabled = False
                        Me.lblCode.Enabled = False
                        Me.lblDescription.Enabled = False
                    Case Me.MODES.NEW_MODE
                        Me.moMultipleColumnDrop.Visible = True
                        Me.moMultipleColumnDropDesc.Visible = True
                        Me.moMultipleColumnTextBoxCode.Visible = False
                        Me.moMultipleColumnTextBoxDesc.Visible = False
                        Me.lb_DropDown.Enabled = True
                        Me.lblCode.Enabled = True
                        Me.lblDescription.Enabled = True
                End Select

            End Set
        End Property

        Public Property VisibleDD() As Boolean
            Get
                Return moMultipleColumnDrop.Visible
            End Get
            Set(ByVal Value As Boolean)
                moMultipleColumnDrop.Visible = Value
                moMultipleColumnDropDesc.Visible = Value
            End Set
        End Property

        Public Property Width() As Unit
            Get
                Return moMultipleColumnDrop.Width
            End Get
            Set(ByVal Value As Unit)
                moMultipleColumnDrop.Width = Value
            End Set
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return moMultipleColumnDrop.Items.Count
            End Get
        End Property

        Public ReadOnly Property IsSelectedTheLastItem() As Boolean
            Get
                Dim bIsLast As Boolean = (SelectedIndex = (Count - 1))
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

        Public ReadOnly Property CodeTextBox() As TextBox
            Get
                Return CType(FindControl("moMultipleColumnTextBoxCode"), TextBox)
            End Get
        End Property

        Public ReadOnly Property DescriptionTextBox() As TextBox
            Get
                Return CType(FindControl("moMultipleColumnTextBoxDesc"), TextBox)
            End Get
        End Property
        Public ReadOnly Property CodeDropDown() As DropDownList
            Get
                Return moMultipleColumnDrop
            End Get
        End Property

        Public ReadOnly Property DescDropDown() As DropDownList
            Get
                Return moMultipleColumnDropDesc
            End Get
        End Property
#End Region

#Region "Handlers"



#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then
                If Not Me.Caption.Equals(String.Empty) Then
                    Me.lb_DropDown.Text = Me.Caption.Replace(":", "") + ":"
                End If
                If Not Me.moMultipleColumnDrop.SelectedValue.Equals(Nothing) Then
                    Me.moMultipleColumnDropDesc.SelectedIndex = -1
                    If Not Me.moMultipleColumnDropDesc.Items.Count.Equals(0) Then
                        Me.moMultipleColumnDropDesc.Items.FindByValue(Me.moMultipleColumnDrop.SelectedValue).Selected = True
                    End If
                End If
            ElseIf Not moMultipleColumnDrop.AutoPostBack Then
                Me.RegisterJavaScriptCode()
            End If
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub moMultipleColumnDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moMultipleColumnDrop.SelectedIndexChanged
            Me.moMultipleColumnDropDesc.SelectedIndex = -1
            Me.moMultipleColumnDropDesc.Items.FindByValue(moMultipleColumnDrop.SelectedValue).Selected = True
            RaiseEvent SelectedDropChanged(Me)
        End Sub

        Private Sub moMultipleColumnDropDesc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moMultipleColumnDropDesc.SelectedIndexChanged
            Me.moMultipleColumnDrop.SelectedIndex = -1
            Me.moMultipleColumnDrop.Items.FindByValue(moMultipleColumnDropDesc.SelectedValue).Selected = True
            RaiseEvent SelectedDropChanged(Me)
        End Sub
#End Region

#End Region

#Region "Utilities"

        Private Function setSpace(ByVal numberOfSpaces As Integer) As String

            Dim Spaces As String
            Dim i As Integer
            For i = 0 To numberOfSpaces
                Spaces &= "&nbsp;"
            Next

            Return Server.HtmlDecode(Spaces)

        End Function

        Public Sub ClearMultipleDrop()
            ElitaPlusPage.ClearList(moMultipleColumnDrop)
        End Sub

#End Region

#Region "Creates a Multiple Column DropDown Based on DataView"

        Private Sub BindCodeDescToList(ByVal lstControl As ListControl, ByVal Data As DataView, ByVal ColumnName As String)
            Dim i As Integer
            Dim sCode, sDesc, sGuid, sListText As String
            Dim cFiller As Char = "_".Chars(0)
            Dim oAttr As Attribute
            Dim oDataViewDesc As DataView = Data

            lstControl.Items.Clear()
            If NothingSelected Then
                lstControl.Items.Add(New ListItem("", Guid.Empty.ToString))
            End If
            If Not Data Is Nothing Then
                For i = StartDropIndex To Data.Count - 1

                    sCode = Data(i)(CodeColumnName).ToString
                    'sDesc = Data(i)(TextColumnName).ToString
                    sDesc = oDataViewDesc(i)(TextColumnName).ToString

                    sListText = sCode & setSpace(CodeFieldLength + 1 - sCode.Length) & " | " & sDesc
                    sGuid = New Guid(CType(Data(i)(GuidValueColumnName), Byte())).ToString
                    'lstControl.Items.Add(New ListItem(sListText, sGuid))
                    lstControl.Items.Add(New ListItem(sCode, sGuid))
                Next
            End If

        End Sub

        Public Sub BindData(ByVal oDataView As DataView)

            oDataView.Sort = CodeColumnName
            ElitaPlusPage.BindListControlToDataView(moMultipleColumnDrop, oDataView, CodeColumnName, , mbAddNothingSelected)
            oDataView.Sort = TextColumnName
            ElitaPlusPage.BindListControlToDataView(moMultipleColumnDropDesc, oDataView, TextColumnName, , mbAddNothingSelected)

        End Sub
        Public Sub ChangeEnabledControlProperty(ByVal blnEnabledState As Boolean)
            Page.ChangeEnabledControlProperty(Me.moMultipleColumnDrop, blnEnabledState)
            Page.ChangeEnabledControlProperty(Me.moMultipleColumnDropDesc, blnEnabledState)
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
                              Optional ByVal TabIndexStartingNumber As Int16 = 0)

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
                Me.lb_DropDown.Text = Me.Caption
                If addColonToCaption AndAlso Me.lb_DropDown.Text.IndexOf(":") < 0 Then Me.lb_DropDown.Text &= ":"
            End If

            Me.NothingSelected = NothingSelected

            If dv Is Nothing Then dv = LookupListNew.GetUserCompaniesLookupList()
            Me.BindData(dv)

            If Not overRideSingularity Then
                overRideSingularity = dv.Count > Me.SINGLE_ITEM
            End If

            If Mode = Me.MODES.NEW_MODE And overRideSingularity Then
                Me.Mode = Me.MODES.NEW_MODE
            Else
                Me.NothingSelected = False
                Me.Mode = Me.MODES.EDIT_MODE
                Me.moMultipleColumnTextBoxCode.Text = LookupListNew.GetCodeFromId(dv, Me.Page.GetSelectedItem(Me.moMultipleColumnDrop)) 'CType(dv.Item(0).Item(0), String)
                Me.moMultipleColumnTextBoxDesc.Text = LookupListNew.GetDescriptionFromId(dv, Me.Page.GetSelectedItem(Me.moMultipleColumnDropDesc))
            End If

            If TabIndexStartingNumber > 0 Then
                moMultipleColumnDrop.TabIndex = TabIndexStartingNumber
                moMultipleColumnDropDesc.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            End If
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


    End Class


End Namespace

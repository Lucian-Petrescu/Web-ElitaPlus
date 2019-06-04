Imports elp = Assurant.ElitaPlus

Namespace Common

    Partial Class MultipleColumnDropControl
        Inherits System.Web.UI.UserControl

#Region "Constants"

        Private Const NUM_POS_FIELD_SEPARATOR As Integer = 5

#End Region

#Region "Variables"

        Private msCodeColumnName As String = "CODE"
        Private msCaption As String = ""
        Private msTextColumnName As String = "DESCRIPTION"
        Private msGuidValueColumnName As String = "ID"
        Private mbAddNothingSelected As Boolean = False
        Private mnCodeFieldLength As Integer = 10

        Private mnStartDropIndex As Integer = 0
        Public Event SelectedDropChanged(ByVal aSrc As MultipleColumnDropControl)

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
            End Set
        End Property

        Public Property AutoPostBack() As Boolean
            Get
                Return moMultipleColumnDrop.AutoPostBack
            End Get
            Set(ByVal Value As Boolean)
                moMultipleColumnDrop.AutoPostBack = Value
            End Set
        End Property

        Public Property Enabled() As Boolean
            Get
                Return moMultipleColumnDrop.Enabled
            End Get
            Set(ByVal Value As Boolean)
                moMultipleColumnDrop.Enabled = Value
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

#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Public WithEvents moMultipleColumnDrop As System.Web.UI.WebControls.DropDownList
        Public WithEvents moMultipleColumnDropDesc As System.Web.UI.WebControls.DropDownList
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then
                If Not Me.Caption.Equals(String.Empty) Then
                    Me.lb_DropDown.Text = Me.Caption + ":"
                End If
                If Not Me.moMultipleColumnDrop.SelectedValue.Equals(Nothing) Then
                    Me.moMultipleColumnDropDesc.SelectedIndex = -1
                    If Not Me.moMultipleColumnDropDesc.Items.Count.Equals(0) Then
                        Me.moMultipleColumnDropDesc.Items.FindByValue(Me.moMultipleColumnDrop.SelectedValue).Selected = True
                    End If
                End If
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

#End Region



    End Class


End Namespace

Partial Class MakeModel
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Constants"

    Private Const END_OF_LINE As String = "^"
    Private Const END_OF_FIELD As String = "|"

    Private Const MSG_WAIT As String = "LOADING"
    Private Const MSG_SELECT_LIST As String = "SELECT_FROM_LIST"

#End Region

#Region " Properties"

    Public ReadOnly Property Make() As String
        Get
            Return MakeDrop.SelectedValue
        End Get
    End Property

    Public ReadOnly Property Year() As String
        Get
            Return YearDrop.SelectedValue
        End Get
    End Property

    Public ReadOnly Property Model() As String
        Get
            Return ModelDrop.SelectedValue
        End Get
    End Property

    Public ReadOnly Property EngineVersion() As String
        Get
            Return TrimDrop.SelectedValue
        End Get
    End Property

    Public ReadOnly Property MakeName() As String
        Get
            Return MakeDrop.SelectedItem.Text
        End Get
    End Property

    Public ReadOnly Property EngineVersionName() As String
        Get
            Return TrimDrop.SelectedItem.Text
        End Get
    End Property

#End Region

#Region "State"
    Class MyState
        Public makeState As String = String.Empty
        Public ModelState As String = String.Empty
        Public YearState As String = String.Empty
        Public EngineVersionState As String = String.Empty
        Public coverageSupportState As String = String.Empty

    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#End Region

#Region " Public Methods"

    Public Sub Reset()

        MakeDrop.SelectedIndex = 0
        YearDrop.Items.Clear()
        ModelDrop.Items.Clear()
        TrimDrop.Items.Clear()
        YearDrop.Enabled = False
        ModelDrop.Enabled = False
        TrimDrop.Enabled = False

    End Sub

#End Region

#Region " Event Handlers"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            FillDropDowns("")
        End If

    End Sub

    Protected Sub MakeDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        FillDropDowns("Make")
    End Sub

    Protected Sub ModelDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        FillDropDowns("Model")
    End Sub

    Protected Sub TrimDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        FillDropDowns("Trim")
    End Sub
#End Region

#Region " Private Methods"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Check if the dropdown is not selected
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[co1mkt]	4/10/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function isEmpty(val As String) As Boolean

        Return (val Is Nothing) OrElse val.Trim.Length = 0

    End Function

    Private Sub FillDropDowns(drop As String)

        Dim selectNext As Boolean = False
        Dim dw As DataView

        If drop = "" Then
            If isEmpty(Make) Then
                dw = LookupListNew.GetVSCMakeLookupList(Authentication.CurrentUser.CompanyGroup.Id)
                MakeDrop.DataSource = dw
                MakeDrop.DataBind()
                If dw.Count > 1 Then
                    MakeDrop.Items.Insert(0, New ListItem("", ""))
                    MakeDrop.SelectedValue = ""
                Else
                    selectNext = True
                End If
                MakeDrop.Enabled = True

                'Restore Previously selected value
                If Not isEmpty(State.makeState) Then
                    MakeDrop.SelectedValue = State.makeState
                    selectNext = True
                End If
            End If
        End If

        If drop = "Make" OrElse selectNext Then
            If Not isEmpty(Make) Then
                dw = LookupListNew.GetVSCModelsLookupList(Make)
                ModelDrop.DataSource = dw
                ModelDrop.DataBind()
                If dw.Count > 1 Then
                    ModelDrop.Items.Insert(0, New ListItem("", ""))
                    ModelDrop.SelectedValue = ""
                Else
                    selectNext = True
                End If
                ModelDrop.Enabled = True
            Else
                ModelDrop.Items.Clear()
                ModelDrop.Enabled = False
            End If
            TrimDrop.Items.Clear()
            YearDrop.Items.Clear()
            TrimDrop.Enabled = False
            YearDrop.Enabled = False

            'Restore Previously selected value
            If drop = "" AndAlso Not isEmpty(State.ModelState) Then
                ModelDrop.SelectedValue = State.ModelState
                selectNext = True
            End If
        End If

        If drop = "Model" OrElse selectNext Then
            If Not isEmpty(Model) Then
                dw = LookupListNew.GetVSCTrimLookupList(Model, Make)
                TrimDrop.DataSource = dw
                TrimDrop.DataBind()
                If dw.Count > 1 Then
                    TrimDrop.Items.Insert(0, New ListItem("", ""))
                    TrimDrop.SelectedValue = ""
                Else
                    selectNext = True
                End If
                TrimDrop.Enabled = True
            Else
                TrimDrop.Items.Clear()
                TrimDrop.Enabled = False
            End If
            YearDrop.Items.Clear()
            YearDrop.Enabled = False

            'Restore Previously selected value
            If drop = "" AndAlso Not isEmpty(State.EngineVersionState) Then
                TrimDrop.SelectedValue = State.EngineVersionState
                selectNext = True
            End If
        End If

        If drop = "Trim" OrElse selectNext Then
            If Not isEmpty(EngineVersion) Then
                dw = LookupListNew.GetVSCYearsLookupList(EngineVersion, Model, Make)
                YearDrop.DataSource = dw
                YearDrop.DataBind()
                If dw.Count > 1 Then
                    YearDrop.Items.Insert(0, New ListItem("", ""))
                    YearDrop.SelectedValue = ""
                End If
                YearDrop.Enabled = True
            Else
                YearDrop.Items.Clear()
                YearDrop.Enabled = False
            End If

            'Restore Previosly selected value
            If drop = "" AndAlso Not isEmpty(State.YearState) Then YearDrop.SelectedValue = State.YearState
        End If

    End Sub

#End Region

End Class
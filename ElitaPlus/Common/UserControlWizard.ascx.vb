Imports System.ComponentModel
Imports System.Web.UI
Imports System.Collections.Generic

<DefaultProperty("Steps"), ParseChildren(True, "Steps"), PersistChildren(False), _
    ToolboxData("<{0}:UserControlWizard runat=""server""><{0}:UserControlWizard>")> _
Public Class UserControlWizard
    Inherits UserControl

    Private _steps As New List(Of StepDefinition)

    <Category("Appearance"), Description("List of Steps in the Wizard"), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content), _
    PersistenceMode(PersistenceMode.InnerDefaultProperty), _
    MergableProperty(False), Bindable(False)> _
    Public Property Steps As List(Of StepDefinition)
        Get
            If (_steps Is Nothing) Then
                _steps = New List(Of StepDefinition)
            End If
            Return _steps
        End Get
        Set(ByVal value As List(Of StepDefinition))
            _steps = value
        End Set
    End Property

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        MyBase.Render(writer)

        Dim blnFirst As Boolean = True
        With writer
            .BeginRender()
            .RenderBeginTag("ul")
            For Each oStep As StepDefinition In Me.Steps
                .RenderBeginTag("li")

                If Not blnFirst Then
                    .AddAttribute("width", "11")
                    .AddAttribute("height", "12")
                    .AddAttribute("src", "../App_Themes/Default/Images/step_arrow.png")
                    .RenderBeginTag("img")
                    .RenderEndTag()
                End If
                blnFirst = False

                If oStep.IsSelected Then
                    .AddAttribute("class", "active")
                End If
                .RenderBeginTag("h1")
                .InnerWriter.Write(oStep.StepNumber)
                .RenderEndTag()

                If oStep.IsSelected Then
                    .AddAttribute("class", "active")
                End If
                .RenderBeginTag("span")
                If (oStep.Translate) Then
                    .InnerWriter.Write(TranslationBase.TranslateLabelOrMessage(oStep.StepName))
                Else
                    .InnerWriter.Write(oStep.StepName)
                End If

                .RenderEndTag()

                .RenderEndTag()
            Next
            .RenderEndTag()
            .EndRender()
        End With

    End Sub

    'START  DEF-2584
    Protected Overrides Sub LoadControlState(ByVal savedState As Object)
        If (Not savedState Is Nothing) Then
            Steps = CType(savedState, List(Of StepDefinition))
        End If
    End Sub
    Protected Overrides Function SaveControlState() As Object
        Return CType(Steps, Object)
    End Function
    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        MyBase.Page.RegisterRequiresControlState(Me)
    End Sub

    'END    DEF-2584
End Class

'START  DEF-2584 Added below Serializable
<TypeConverter(GetType(ExpandableObjectConverter)), Serializable()> _
Public Class StepDefinition
    Private _stepNumber As Integer = 0
    Private _stepName As String = String.Empty
    Private _isSelected As Boolean = False
    Private _translate As Boolean = True

    Public Sub New()
        Me.New(String.Empty, String.Empty, False)
    End Sub

    Public Sub New(ByVal header As String, ByVal textHtml As String)
        Me.New(header, textHtml, False)
    End Sub

    Public Sub New(ByVal header As String, ByVal textHtml As String, ByVal isSelected As Boolean)

    End Sub

    <Category("Appearance"), DefaultValue(""), Description("Step Number of Step e.g. 1, 2, 3 and so on."), NotifyParentProperty(True)> _
    Public Property StepNumber() As Integer
        Get
            Return _stepNumber
        End Get
        Set(ByVal value As Integer)
            _stepNumber = value
        End Set
    End Property

    <Category("Appearance"), DefaultValue(""), Description("Text to be displayed e.g Date of Incident etc. Use <br /> to seperate the line."), NotifyParentProperty(True)> _
    Public Property StepName() As String
        Get
            Return _stepName
        End Get
        Set(ByVal value As String)
            _stepName = value
        End Set
    End Property

    <Category("Appearance"), DefaultValue(False), Description("True when current Step is selected"), NotifyParentProperty(True)> _
    Public Property IsSelected() As Boolean
        Get
            Return _isSelected
        End Get
        Set(ByVal value As Boolean)
            _isSelected = value
        End Set
    End Property

    <Category("Appearance"), DefaultValue(True), Description("Whether or not Step Name is to be Transalated"), NotifyParentProperty(True)> _
    Public Property Translate() As Boolean
        Get
            Return _translate
        End Get
        Set(ByVal value As Boolean)
            _translate = value
        End Set
    End Property
End Class
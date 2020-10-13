Imports System
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.UI

Public Class ScrollPage
    Inherits Page

    Public Sub ScrollPage()

    End Sub

    Private m_UseScrollPersistence As Boolean = True
    Private m_BodyID As String
    Private saveScrollPosition As String = "<script language='javascript'>function saveScrollPosition() {{document.forms[0].__SCROLLPOS.value = {0}.scrollTop;}}{0}.onscroll=saveScrollPosition;</script>"
    Private setScrollPosition As String = "<script language='javascript'>function setScrollPosition() {{{0}.scrollTop =""{1}"";}}{0}.onload=setScrollPosition;</script>"

    Public Property UseScrollPersistence() As Boolean
        Get
            Return m_UseScrollPersistence
        End Get
        Set(Value As Boolean)
            m_UseScrollPersistence = Value
        End Set
    End Property

    Public Property BodyID() As String
        Get
            Return m_BodyID
        End Get
        Set(Value As String)
            m_BodyID = Value
        End Set
    End Property

    Protected Overrides Sub OnPreRender(e As EventArgs)
        If UseScrollPersistence Then
            'call the routine to add the javascript 
            RetainScrollPosition()
        End If

        MyBase.OnPreRender(e)
    End Sub

    Protected Overrides Sub Render(writer As HtmlTextWriter)
        If UseScrollPersistence AndAlso (BodyID = Nothing) Then
            Dim tempWriter As TextWriter = New StringWriter
            MyBase.Render(New HtmlTextWriter(tempWriter))
            writer.Write(Regex.Replace(tempWriter.ToString(), "<body", "<body id=""thebody"" ", RegexOptions.IgnoreCase))
        Else
            MyBase.Render(writer)
        End If
    End Sub

    Private Sub RetainScrollPosition()
        Page.RegisterHiddenField("__SCROLLPOS", "0")
        Dim s_bodyID As String
        If BodyID = Nothing Then s_bodyID = "thebody" Else s_bodyID = BodyID
        RegisterStartupScript("saveScroll", String.Format(saveScrollPosition, s_bodyID))
        If (Page.IsPostBack) Then
            RegisterStartupScript("setScroll", String.Format(setScrollPosition, s_bodyID, context.Request.Form("__SCROLLPOS")))
        End If
    End Sub


End Class



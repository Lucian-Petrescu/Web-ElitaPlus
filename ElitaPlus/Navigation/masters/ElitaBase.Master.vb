Imports System.Text

Public Class ElitaBase
    Inherits MasterBase

    Private m_BreadCrum As String
    Private m_PageTab As String
    Private m_PageTitle As String
    Private m_UsePageTabTitleInBreadCrum As Boolean = False
    Private m_DisplayRequiredFieldNote As Boolean = True

    Public Const Sperator As String = "|"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''<link href="../Styles2.css" type="text/css" rel="STYLESHEET" />
    End Sub

    Public Overrides ReadOnly Property ErrController() As ErrorController
        Get
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Gets or Sets Page Title Text
    ''' </summary>
    ''' <value>Sets the value as Page Title Text.</value>
    ''' <returns>Gets the current value of Page Title Text.</returns>
    ''' <remarks>Bold Font can be achived by adding "Strong" HTML Tag in the text.</remarks>
    Public Overrides Property PageTitle() As String
        Get
            Return m_PageTitle
        End Get
        Set(ByVal value As String)
            m_PageTitle = value.Trim
            PageHeader.InnerHtml = m_PageTitle
            If (UsePageTabTitleInBreadCrum) Then UpdateBreadCrum()
        End Set
    End Property

    Public Overrides Property PageTab() As String
        Get
            Return m_PageTab
        End Get
        Set(ByVal value As String)
            m_PageTab = value.Trim
            If (UsePageTabTitleInBreadCrum) Then UpdateBreadCrum()
        End Set
    End Property

    Public Overrides ReadOnly Property PageForm(ByVal FormName As String) As System.Web.UI.HtmlControls.HtmlForm
        Get
            If Not Me.FindControl(FormName) Is Nothing Then
                Return CType(Me.FindControl(FormName), HtmlForm)
            Else
                Return Nothing
            End If
        End Get
    End Property


    Public Overrides Property UsePageTabTitleInBreadCrum As Boolean
        Get
            Return m_UsePageTabTitleInBreadCrum
        End Get
        Set(ByVal value As Boolean)
            m_UsePageTabTitleInBreadCrum = value
            UpdateBreadCrum()
        End Set
    End Property

    ''' <summary>
    ''' Displays the note "* Indicates Required Fields" on Right Side of Master Page in the same
    ''' line that of Bread Crum.
    ''' </summary>
    ''' <value>Displays the note when value is True, Hides otherwise.</value>
    ''' <returns>Current status of Required Field Note, True when Visible, False otherwise.</returns>
    ''' <remarks>The default value of Property is True; so by default the note will be visible. If
    ''' Page requries the note to be hidden; then set the value to False.</remarks>
    Public Overrides Property DisplayRequiredFieldNote() As Boolean
        Get
            Return m_DisplayRequiredFieldNote
        End Get
        Set(ByVal value As Boolean)
            m_DisplayRequiredFieldNote = value
            UpdateBreadCrum()
        End Set
    End Property

    Public Overrides Property BreadCrum() As String
        Get
            Return m_BreadCrum
        End Get
        Set(ByVal value As String)
            m_BreadCrum = value.Trim
            UpdateBreadCrum()
        End Set
    End Property

    Private ReadOnly Property BreadArrowImageUrl() As String
        Get
            Return String.Format("../App_Themes/{0}/Images/Bread_arrow.png", Page.Theme)
        End Get
    End Property

    Private Sub UpdateBreadCrum()
        Dim breadCrumBuilder As StringBuilder
        Dim isFirst As Boolean = True
        Dim breadCrumString As String
        BreadCrumDiv.Visible = False
        If (Me.UsePageTabTitleInBreadCrum) Then
            If (Not String.IsNullOrEmpty(Me.PageTitle)) Then
                breadCrumString = Me.PageTitle
            End If
            If (Not String.IsNullOrEmpty(Me.PageTab)) Then
                breadCrumString = Me.PageTab & Sperator & breadCrumString
            End If
        End If
        breadCrumString = breadCrumString & Sperator & Me.BreadCrum

        If (breadCrumString.Length > 0) Then
            breadCrumBuilder = New StringBuilder(TranslationBase.TranslateLabelOrMessage("YOU_ARE_HERE")).Append(" : ")

            For Each breadCrumItem As String In breadCrumString.Split(Sperator.ToCharArray())
                If ((Not breadCrumItem Is Nothing) AndAlso (breadCrumItem.Trim.Length > 0)) Then
                    If (Not isFirst) Then
                        breadCrumBuilder.Append("&nbsp;<img style='vertical-align:middle' width='9' height='10' src='" & BreadArrowImageUrl & "' />&nbsp;")
                    End If
                    breadCrumBuilder.Append(String.Format("<span>{0}<//span>", breadCrumItem.Trim()))

                    isFirst = False
                End If
            Next
        End If
        If (DisplayRequiredFieldNote) Then
            breadCrumBuilder.Append(String.Format("<p><span>*</span>{0}</p>", TranslationBase.TranslateLabelOrMessage("INDICATES_REQUIRED_FIELDS")))
        End If
        If (isFirst And Not DisplayRequiredFieldNote) Then
            BreadCrumDiv.Visible = False
        Else
            BreadCrumDiv.InnerHtml = breadCrumBuilder.ToString()
            BreadCrumDiv.Visible = True
        End If
    End Sub

    Public Overrides ReadOnly Property MessageController As IMessageController
        Get
            Return DirectCast(moMessageController, IMessageController)
        End Get
    End Property
End Class
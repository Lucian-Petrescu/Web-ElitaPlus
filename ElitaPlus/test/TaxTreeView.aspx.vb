Imports Assurant.ElitaPlus.BusinessObjects
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.BusinessObjects.Tables
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Generic
Imports Assurant.ElitaPlus.BusinessObjectData.Common
Imports Microsoft.Web.UI.WebControls
Imports Assurant.Common.Framework.BusinessObject

Partial Class TaxTreeView
    Inherits System.Web.UI.Page
#Region "Constants"

    Public baseUrl As String

    Private Const MAINTAIN_TAB_EXCLUSION_FORM001 As String = "MAINTAIN_TAB_EXCLUSION_FORM001" ' Tab Exception
    Private Const MAINTAIN_TAB_EXCLUSION_FORM002 As String = "MAINTAIN_TAB_EXCLUSION_FORM002" ' Update Exception

    ' Table that populates the tree
    Private Const TABROLE_TABID As Integer = 0
    Private Const TABROLE_TABNAME As Integer = 1
    Private Const TABROLE_ROLEID As Integer = 2
    Private Const TABROLE_ROLENAME As Integer = 3
    Private Const TABROLE_PERMISSION As Integer = 4
    Private Const TABROLE_TABICON As Integer = 5
    Private Const TABROLE_ROLEICON As Integer = -1

    Private Const ROLES_TREE_LEVEL As Integer = 1

    ' Table that updates the DB
    Private Const TABDB_TABID As Integer = 0
    Private Const TABDB_ROLEID As Integer = 1
    Private Const TABDB_PREVPERMISSION As Integer = 2
    Private Const TABDB_CURRPERMISSION As Integer = 3

    ' Tab Permissions
    Private Const TAB_VISIBLE As String = "V"
    Private Const TAB_INVISIBLE As String = "N"

    Private ReadOnly moHeaders() As String = {"TAB_ID", "ROLE_ID", "PREV_PERMISSION", "CURR_PERMISSION"}

#End Region

#Region "Attributes"

    ' Private moTabExclusions As MaintainTabExclusions
    'Private moTabExclusions As TabExclusions

#End Region

#Region "Properties"

    Private ReadOnly Property Tree() As TreeController
        Get
            If moTree Is Nothing Then
                moTree = CType(FindControl("moTree"), TreeController)
            End If
            Return moTree
        End Get
    End Property

    Public ReadOnly Property HeadeFrs() As String()
        Get
            Return moHeaders
        End Get
    End Property

#End Region

#Region "Handlers"


    ' User Controls
    Protected WithEvents moTree As TreeController

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
#End Region


    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        baseUrl = Request.Url.AbsolutePath
        baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf("/"))
        If Not Page.IsPostBack Then
            PopulateTree()
        End If

    End Sub
    Private Sub PopulateTree()
        Dim oTreeLevel As TreeController.TreeLevel

        ' Tabs
        oTreeLevel = New TreeController.TreeLevel
        With oTreeLevel
            .name = "CompanyTax"
            .idPos = TABROLE_TABID
            .descPos = TABROLE_TABNAME
            '  .iconPos = TABROLE_TABICON
        End With
        Tree.AddData(oTreeLevel)

        ' Roles
        oTreeLevel = New TreeController.TreeLevel
        With oTreeLevel
            .name = "Regions"
            .idPos = TABROLE_ROLEID
            .descPos = TABROLE_ROLENAME
            .iconPos = TABROLE_ROLEICON
        End With
        Tree.AddData(oTreeLevel)

        Try
            '     Tree.BindData(CountryTax.LoadTree(Me.moApplicationUser.CompanyCountryID, moApplicationUser.LanguageID))
        Catch ex As Exception
            ELPWebConstants.ShowTranslatedMessageAsPopup(MAINTAIN_TAB_EXCLUSION_FORM001, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Page, ex)
        End Try
    End Sub
End Class

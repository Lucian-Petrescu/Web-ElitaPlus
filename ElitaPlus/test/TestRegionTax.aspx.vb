Imports Assurant.ElitaPlus.BusinessObjects
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.BusinessObjects.Tables
Imports Assurant.ElitaPlus.BusinessObjectData
Imports Assurant.ElitaPlus.BusinessObjectData.Tables
Imports Assurant.ElitaPlus.BusinessObjectData.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Imports Assurant.Common.Framework.BusinessObject.Data
Imports Assurant.Common.Framework.BusinessObject

Imports System.Text


Partial Class TestRegionTax
    Inherits ElitaPlusPage
    ' Inherits System.Web.UI.Page

    Protected WithEvents dgrRegion As System.Web.UI.WebControls.DataGrid
#Region " Constants "

    Private Const ACTION_SELECT As String = "SelectAction"

#End Region

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

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not IsPostBack Then
            'Me.MenuEnabled = True
            LoadTaxes()
        End If

    End Sub

    Private Sub LoadTaxes()

        ' Dim oGenericDataInfo As GenericDataInfo = CountryTax.LoadListTaxParm(Me.GetApplicationUser.CompanyCountryID, Me.GetApplicationUser.LanguageID)

        '     Dim oElpCommonList As New CommonListView(oGenericDataInfo)
        '   dgrResults.DataSource = oElpCommonList.DataView
        '  dgrResults.DataBind()
        

    End Sub

    Private Sub dgrResults_ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgrResults.ItemCommand


        If e.CommandName = ACTION_SELECT Then
            
        End If


    End Sub

    Private Sub dgrResults_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrResults.ItemCreated
        '-------------------------------------
        'Name:ItemCreated
        'Purpose:Add navigation symbols to datagrid page
        'Input Values: Message
        'Uses:
        '-------------------------------------

        ' get the type of item being created
        Dim elemType As ListItemType = e.Item.ItemType

        ' make sure it is the pager bar
        If elemType = ListItemType.Pager Then
            ' the pager bar as a whole has the follwoing layout
            ' <TR><TD colspan=x>.....links</TD></TR>
            ' item points to <TR>. The code below moves to <TD>
            Dim pager As TableCell = DirectCast(e.Item.Controls(0), TableCell)
            Dim i As Int32 = 0
            Dim bFound As Boolean = False
            For i = 0 To pager.Controls.Count
                Dim obj As Object = pager.Controls(i)

                If obj.GetType.ToString = "System.Web.UI.WebControls.DataGridLinkButton" Then

                    Dim h As LinkButton = CType(obj, LinkButton)

                    If h.Text.Equals("...") Then
                        If Not bFound Then
                            h.ToolTip = "Previous set of pages"
                            h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                            h.Style.Add("COLOR", "#dee3e7")
                            h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_back.gif)")
                            bFound = True
                        Else
                            h.ToolTip = "Next set of pages"
                            h.Text = ".        .."
                            h.Style.Add("COLOR", "#dee3e7")
                            h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                            h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_foward.gif)")
                        End If

                    End If

                Else
                    bFound = True
                    Dim l As System.Web.UI.WebControls.Label = CType(obj, System.Web.UI.WebControls.Label)
                    l.Text = "Page" & l.Text
                    'l.Style.Add("FONT-WEIGHT", "BOLD")
                End If

                i += 1
            Next
        End If
    End Sub

    Private Sub dgrResults_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles dgrResults.SelectedIndexChanged

    End Sub
End Class

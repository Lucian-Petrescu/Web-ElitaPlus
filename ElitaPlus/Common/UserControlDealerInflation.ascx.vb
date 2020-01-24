 Imports System.ComponentModel
 Imports Assurant.ElitaPlus.BusinessObjects.Common
 Imports Assurant.ElitaPlus.DALObjects
Public Class UserControlDealerInflation
    Inherits System.Web.UI.UserControl
    Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       Try
           If (Not Page.IsPostBack) Then
              ' Me.ThePage.TranslateGridHeader(DealerInflationGrid)
           End If
       Catch ex As Exception
           Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
       End Try
        
    End Sub

End Class
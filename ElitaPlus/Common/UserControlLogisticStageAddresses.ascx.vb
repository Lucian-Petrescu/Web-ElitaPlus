Imports System.Collections.Generic
Public Class UserControlLogisticStageAddresses
    Inherits System.Web.UI.UserControl
#Region "Constants"

     Private Const GridCtrlLblLogisticStageName As String = "lblLogisticStageName"
     Private Const GridCtrlUcAddressController As String = "moAddressController"
     Private Const GridColDataLogisticStageName As String = "LogisticStageName"
     Private Const GridColDataLogisticStageAddress As String = "LogisticStageAddress"

#End Region

#Region "Properties"

    Public Property ParentBusinessObject As List(Of LogisticStageAddresses)
    Public Property ProfileCode As String
    Public Property ValidateAddress As Boolean = false

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Public Overrides Sub DataBind()
        PopulateLogisticStageAddress()
    End Sub

    Private Sub PopulateLogisticStageAddress()
      
        If (ParentBusinessObject Is Nothing) Then
            Throw New BOInvalidOperationException("Value of ParentBusinessObject can not be null")
        End If
        repLogisticStageAddress.DataSource = ParentBusinessObject
        repLogisticStageAddress.DataBind()

    End Sub

    Protected Sub repLogisticStageAddress_OnItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblLogisticStageName As Label = DirectCast(e.Item.FindControl(GridCtrlLblLogisticStageName), Label)
            Dim addressControls As UserControlAddress_New = DirectCast(e.Item.FindControl(GridCtrlUcAddresscontroller), UserControlAddress_New)
            Dim logisticStageName As Object = DataBinder.Eval(e.Item.DataItem, GridColDataLogisticstageName)
            Dim logisticStageAddressData As Object = DataBinder.Eval(e.Item.DataItem, GridColDataLogisticstageAddress)
            addressControls.TranslateAllLabelControl()

            lblLogisticStageName.Text = logisticStageName
            addressControls.Bind(logisticStageAddressData)
            addressControls.EnableControls(False, True)

        End If

    End Sub

End Class
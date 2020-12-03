Imports System.Collections.Generic

Public Class UserControlLogisticStageAddress
    Inherits System.Web.UI.UserControl
#Region "Constants"

     Private Const GridCtrlLblLogisticStageName As String = "lblLogisticStageName"
     Private Const GridCtrlUcAddressController As String = "moAddressController"
     Private Const GridColDataLogisticStageName As String = "LogisticStageName"
     Private Const GridColDataLogisticStageAddress As String = "LogisticStageAddress"
     Private Const ValidateAddressButton As String = "btnValidate_Address"

#End Region

#Region "Variables"

#End Region

#Region "Properties"

    Public Property MyGenBO() As BusinessObjectBase
        Get
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), BusinessObjectBase)
        End Get
        Set(ByVal Value As BusinessObjectBase)
            Me.Page.StateSession.Item(Me.UniqueID) = Value
        End Set
    End Property
    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property
    Public Property ParentBusinessObject As List(Of LogisticStageAddress)
    Public Property ProfileCode As String 
    Public Property ValidateAddress As Boolean
    
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
            Dim btnValidateAddress As Button = addressControls.FindControl(ValidateAddressButton)
            addressControls.TranslateAllLabelControl()
            btnValidateAddress.Visible = If( ValidateAddress, "True", "False")

            lblLogisticStageName.Text = logisticStageName
            addressControls.Bind(logisticStageAddressData, ProfileCode)
            addressControls.EnableControls(False, True)

        End If

    End Sub

    Friend Sub Bind(filteredLogistics As List(Of LogisticStageAddress))
        ' MyGenBO = oBusObj
        repLogisticStageAddress.DataSource = filteredLogistics
        repLogisticStageAddress.DataBind()
        If Not Me.MyGenBO Is Nothing Then
            'BindBoPropertiesToLabels()
            Me.Page.AddLabelDecorations(Me.MyGenBO)
        End If
    End Sub
    Friend Sub PopulateBOFromControl(Optional ByVal blnIncludeCountryId As Boolean = False)

        For Each repeaterItem As RepeaterItem In repLogisticStageAddress.Items
            Dim addressControl As UserControlAddress_New = DirectCast(repeaterItem.FindControl(GridCtrlUcAddressController), UserControlAddress_New)
            addressControl.PopulateBOFromControl(True)
        Next

    End Sub
End Class
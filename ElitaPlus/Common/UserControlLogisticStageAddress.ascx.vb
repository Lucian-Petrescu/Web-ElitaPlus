Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew.ClaimFulfillmentWebAppGatewayService

Public Class UserControlLogisticStageAddress
    Inherits UserControl

#Region "Constants"

    Private Const GridCtrlHdnLogisticStageCode As String = "hdnLogisticStageCode"
    Private Const GridCtrlLblLogisticStageName As String = "lblLogisticStageName"
    Private Const GridCtrlUcAddressController As String = "moAddressController"
    Private Const GridColDataLogisticStageCode As String = "LogisticStageCode"
    Private Const GridColDataLogisticStageName As String = "LogisticStageName"
    Private Const GridColDataLogisticStageAddress As String = "LogisticStageAddress"
    Private Const ValidateAddressButton As String = "btnValidate_Address"
    Private Const AddressCtrlDdlCountry As String = "moCountryDrop_WRITE"
    Private Const AddressCtrlTxtPostal As String = "moPostalText"

#End Region

#Region "Variables"

#End Region

#Region "Properties"

    Public Property MyGenBo() As BusinessObjectBase
        Get
            Return CType(Page.StateSession.Item(UniqueID), BusinessObjectBase)
        End Get
        Set(ByVal value As BusinessObjectBase)
            Page.StateSession.Item(UniqueID) = value
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
    Public Property FulfillmentProviderTypeInfo As FulfillmentProviderType

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

            Dim hdnLogisticStageCode As HiddenField = DirectCast(e.Item.FindControl(GridCtrlhdnLogisticStageCode), HiddenField)
            Dim lblLogisticStageName As Label = DirectCast(e.Item.FindControl(GridCtrlLblLogisticStageName), Label)
            Dim addressControls As UserControlAddress_New = DirectCast(e.Item.FindControl(GridCtrlUcAddresscontroller), UserControlAddress_New)
            Dim logisticStageCode As Object = DataBinder.Eval(e.Item.DataItem, GridColDataLogisticStageCode)
            Dim logisticStageName As Object = DataBinder.Eval(e.Item.DataItem, GridColDataLogisticstageName)
            Dim logisticStageAddressData As Object = DataBinder.Eval(e.Item.DataItem, GridColDataLogisticstageAddress)
            Dim btnValidateAddress As Button = addressControls.FindControl(ValidateAddressButton)

            addressControls.TranslateAllLabelControl()
            btnValidateAddress.Visible = If(ValidateAddress, "True", "False")
            hdnLogisticStageCode.Value = logisticStageCode
            lblLogisticStageName.Text = logisticStageName
            addressControls.Bind(logisticStageAddressData, ProfileCode)
            addressControls.EnableControls(False, True)

        End If
    End Sub

    Friend Sub Bind(filteredLogistics As List(Of LogisticStageAddress))
        repLogisticStageAddress.DataSource = filteredLogistics
        repLogisticStageAddress.DataBind()
        If MyGenBo IsNot Nothing Then
            Page.AddLabelDecorations(MyGenBo)
        End If
    End Sub

    Friend Sub PopulateBoFromRepeaterControl(ByVal fulfillmentDetails As FulfillmentDetails)

        For Each repeaterItem As RepeaterItem In repLogisticStageAddress.Items
            Dim repAddress As new BusinessObjectsNew.Address
            Dim hdnLogisticStageCode As HiddenField = DirectCast(repeaterItem.FindControl(GridCtrlHdnLogisticStageCode), HiddenField)
            Dim addressControl As UserControlAddress_New = DirectCast(repeaterItem.FindControl(GridCtrlUcAddressController), UserControlAddress_New)
            Dim ddlCountry As DropDownList = DirectCast(addressControl.FindControl(AddressCtrlDdlCountry), DropDownList)
            Dim txtPostalCode As TextBox = DirectCast(addressControl.FindControl(AddressCtrlTxtPostal), TextBox)

            'Zip Code validation
            Dim zd As New ZipDistrict()
            zd.CountryId = New Guid(ddlCountry.SelectedValue.ToString())
            zd.ValidateZipCode(txtPostalCode.Text)
  
            if FulfillmentProviderTypeInfo <> FulfillmentProviderType.V3 Then
                 addressControl.PopulateBOFromControl(True)
            End If

            addressControl.PopulateBOFromAddressControl(repAddress)
            
            For Each ls As SelectedLogisticStage In _
                From lss In fulfillmentDetails.LogisticStages.ToList() Where lss.Code = hdnLogisticStageCode.Value
                ls.Address.Address1 = repAddress.Address1
                ls.Address.Address2 = repAddress.Address2
                ls.Address.Address3 = repAddress.Address3
                ls.Address.City = repAddress.City
                ls.Address.State = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_REGIONS, False), repAddress.RegionId)
                ls.Address.Country = repAddress.countryBO.Code
                ls.Address.PostalCode = repAddress.PostalCode
            Next

        Next
    End Sub

End Class
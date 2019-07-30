Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService
Imports System.ServiceModel
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports System.Collections.Generic
Imports System.Threading
Imports System.Globalization

Public Class UserControlDeliverySlot
    Inherits UserControl

    public class DeliveryAddressInfo
        public CountryCode as string 
        public Address1 as string 
        public Address2 as string 
        public Address3 as string 
        public City as string 
        public RegionShortDesc as string 
        public PostalCode as string 
    End Class

#Region "Properties"
    Public Property AvailableDeliveryDateTiming() As String
        Get
            Return lblAvailableDeliveryTimingData.Text.ToString
        End Get
        Set(ByVal value As String)
            lblAvailableDeliveryTimingData.Text = value
        End Set
    End Property
    Public Property DeliveryDate() As Nullable(Of Date)
        Get
            Return State.DeliveryDateSelected
        End Get
        Set(ByVal value As Nullable(Of Date))
            State.DeliveryDateSelected = value
        End Set
    End Property
    Public ReadOnly Property DeliverySlot() As String
        Get
            If ddlDeliverySlots.SelectedIndex <> -1 Then
                Return ddlDeliverySlots.SelectedValue.ToString
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public Property CountryCode() As String
        Get
            Return State.CountryCode
        End Get
        Set(ByVal value As String)
            State.CountryCode = value
        End Set
    End Property

    Public Property ServiceCenter() As String
        Get
            Return State.ServiceCenter
        End Get
        Set(ByVal value As String)
            State.ServiceCenter = value
        End Set
    End Property

    Public Property CourierCode() As String
        Get
            Return State.CourierCode
        End Get
        Set(ByVal value As String)
            State.CourierCode = value
        End Set
    End Property

    Public Property CourierProductCode() As String
        Get
            Return State.CourierProductCode
        End Get
        Set(ByVal value As String)
            State.CourierProductCode = value
        End Set
    End Property

    Public Property DeliveryAddress() As DeliveryAddressInfo
        Get
            Return State.DeliveryAddress
        End Get
        Set(ByVal value As DeliveryAddressInfo)
            State.DeliveryAddress = value
        End Set
    End Property
#End Region
#Region "Control State"

    Private Class MyState
        Public CountryCode As String
        Public ServiceCenter As String
        Public CourierCode as String
        Public CourierProductCode as string
        Public DeliveryAddress as DeliveryAddressInfo
        Public DeliveryDateList As DeliveryEstimate()
        Public DeliveryDateSelected As Nullable(Of Date)
        Public CurrentEstimate as DeliveryEstimate
        Public EnableNotSepecifyCheck as Boolean
    End Class


    Private ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

#End Region
#Region "Control Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Page.IsPostBack) Then
            TranslateLabels()
        End If
    End Sub
    Public Sub TranslateLabels()
        lblCourierProduct.Text = TranslationBase.TranslateLabelOrMessage("COURIER_PRODUCT")
        lblAvailableDeliveryTiming.Text = TranslationBase.TranslateLabelOrMessage("AVAILABLE_DELIVERY_TIMING")
        lblDesiredDate.Text = TranslationBase.TranslateLabelOrMessage("DESIRED_DELIVERY_DATE")
        lblDeliverySlot.Text = TranslationBase.TranslateLabelOrMessage("DESIRED_DELIVERY_TIME")
    End Sub
#End Region
#Region "Other Event"
    Private Function ValidateSetDeliveryDate(ByVal dtvalue As String) As Boolean
        If Not String.IsNullOrWhiteSpace(dtvalue) Then
            Dim desiredDeliveryDate As Date
            Try
                Dim formatProvider = LocalizationMgr.CurrentFormatProvider
                If formatProvider.Name.Equals("ja-JP") Then
                    Dim dateFragments() As String = dtvalue.Split("-")
                    desiredDeliveryDate = New DateTime(Integer.Parse(dateFragments(2)), Integer.Parse(dateFragments(1)), Integer.Parse(dateFragments(0))).Date
                Else
                    desiredDeliveryDate = Convert.ToDateTime(dtvalue, formatProvider)
                End If

            Catch ex As Exception
                Page.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("DESIRED_DELIVERY_DATE") & " - " & txtDeliveryDate.Text & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), False)
                DeliveryDate = Nothing 'clear selection
                Return False
            End Try

            If desiredDeliveryDate < Date.Now.Date Then
                Page.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("DESIRED_DELIVERY_DATE") & " - " & txtDeliveryDate.Text & " : " & TranslationBase.TranslateLabelOrMessage(Messages.NO_PAST_DATE), False)
                DeliveryDate = Nothing 'clear selection
                Return False
            ElseIf State.CurrentEstimate.Behavior.AllowSelectionAfterLastDeliveryDate = False Then 'check the date is within the available dates
                If State.CurrentEstimate.AvailableDeliveryDays.ToList().Exists(Function(x) x.DeliveryDate = desiredDeliveryDate) = False Then
                    Page.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("DESIRED_DELIVERY_DATE") & " - " & txtDeliveryDate.Text & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), False)
                    DeliveryDate = Nothing 'clear selection
                    Return False
                End If
            End If
            DeliveryDate = desiredDeliveryDate
        Else
            If DeliveryDate.HasValue Then
                DeliveryDate = Nothing
            End If

            ddlDeliverySlots.Items.Clear() ' remove the delivery slot options
            Return False
        End If
        Return True
    End Function
    Protected Sub txtDeliveryDate_TextChanged(sender As Object, e As EventArgs) Handles txtDeliveryDate.TextChanged
        Try
            Dim listItems As List(Of ListItem) = New List(Of ListItem)()
            If ValidateSetDeliveryDate(txtDeliveryDate.Text) Then
                If State.DeliveryDateList IsNot Nothing AndAlso State.DeliveryDateList.Length > 0 Then
                    Dim delDay As DeliveryDay = State.CurrentEstimate.AvailableDeliveryDays.FirstOrDefault(Function(q) q.DeliveryDate.Date = State.DeliveryDateSelected)

                    If delDay IsNot Nothing AndAlso delDay.DeliverySlots IsNot Nothing AndAlso delDay.DeliverySlots.Length > 0 Then
                        Dim deliverySlots As DeliverySlot() = delDay.DeliverySlots()
                        If deliverySlots IsNot Nothing Then
                            For Each delSlot As DeliverySlot In deliverySlots.OrderBy("Sequence", LinqExtentions.SortDirection.Ascending)
                                listItems.Add(New ListItem(If(LookupListNew.GetDescriptionFromCode(LookupListNew.LK_DESIRED_DELIVERY_TIME_SLOT, delSlot.Description, Authentication.CurrentUser.LanguageId), delSlot.Description), delSlot.Sequence.ToString()))
                            Next
                        End If
                    End If
                End If
            End If
            ElitaPlusPage.BindListControlToArray(ddlDeliverySlots, listItems.ToArray(), True)
        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SetControlStatus()
        If chkNotSpecify.Checked = True Then
            ResetToDefaultSetting()
        Else
            EnableDisableControl(True)

            If State.CurrentEstimate.Behavior.AllowSelectionAfterLastDeliveryDate Then
                ' enable dates starting from the fastest delivery date
                Dim fastestDeliveryDate As DeliveryDay = (From delDay As DeliveryDay In State.CurrentEstimate.AvailableDeliveryDays Select delDay Order By delDay.DeliveryDate Ascending).First()
                ElitaPlusPage.AddCalendarNewWithDisableBeforeDate(imageBtnDeliveryDate, txtDeliveryDate, "", "N", fastestDeliveryDate.DeliveryDate)
            Else
                ' only enable those dates in the delivery days list
                Dim deliveryDays As New List(Of Date)
                For Each dd As DeliveryDay In State.CurrentEstimate.AvailableDeliveryDays
                    deliveryDays.Add(dd.DeliveryDate)
                Next
                ElitaPlusPage.AddCalendarNewWithEnableDates(imageBtnDeliveryDate, txtDeliveryDate, "", "N", deliveryDays)
            End If
        End If
    End Sub
    Protected Sub chkNotSpecify_CheckedChanged(sender As Object, e As EventArgs) Handles chkNotSpecify.CheckedChanged
        SetControlStatus()
    End Sub
#End Region
#Region "Other Function"


    ''' <summary>
    ''' Gets New Instance of WebAppGateway Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="WebAppGatewayClient"/></returns>
    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As WebAppGatewayClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE), False)
        Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password

        'Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", "http://sf-au-southeast-mod.assurant.com/ElitaClaimFulfillment/modl-p1/WebAppGateway/gateway")

        'client.ClientCredentials.UserName.UserName = "InternalUsers\os0iej"
        'client.ClientCredentials.UserName.Password = "XXXX"
        Return client
    End Function

    Public Sub PopulateDeliveryDate(Optional blnNotSpecifyCheckInitState As Boolean = True, Optional blnEnableNotSpecifyCheck As Boolean = True)

        chkNotSpecify.Text = " " + TranslationBase.TranslateLabelOrMessage("NOT_SPECIFY")
        chkNotSpecify.Checked = blnNotSpecifyCheckInitState 'True  
        Page.ChangeEnabledControlProperty(chkNotSpecify, blnEnableNotSpecifyCheck)
        State.EnableNotSepecifyCheck = blnEnableNotSpecifyCheck

        'clear old values when get new
        State.CurrentEstimate = Nothing
        State.DeliveryDateList = Nothing

        ResetToDefaultSetting()

        Dim wsRequest As New GetDeliverySlotsRequest

        If String.IsNullOrWhiteSpace(State.CountryCode) OrElse State.DeliveryAddress Is Nothing OrElse String.IsNullOrWhiteSpace(State.DeliveryAddress.PostalCode) Then
            Page.MasterPage.MessageController.AddError(Message.MSG_ERR_COUNTRY_POSTAL_MANDATORY, True)
            Exit Sub
        End If

        wsRequest.CountryCode = State.CountryCode
        wsRequest.ServiceCenterCode = State.ServiceCenter
        wsRequest.CourierCode = State.CourierCode
        wsRequest.CourierProductCode = State.CourierProductCode
        wsRequest.DeliveryAddress = New Address() With {
            .CountryCode = State.DeliveryAddress.CountryCode,
            .PostalCode = State.DeliveryAddress.PostalCode,
            .StateCode = State.DeliveryAddress.RegionShortDesc,
            .City = State.DeliveryAddress.City,
            .Address1 = State.DeliveryAddress.Address1,
            .Address2 = State.DeliveryAddress.Address2,
            .Address3 = State.DeliveryAddress.Address3}

        Try
            Dim wsResponse As GetDeliverySlotsResponse = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, GetDeliverySlotsResponse)(
                                                                                GetClaimFulfillmentWebAppGatewayClient(),
                                                                            New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                            Function(ByVal c As WebAppGatewayClient)
                                                                                Return c.GetDeliverySlots(wsRequest)
                                                                            End Function)
            If wsResponse IsNot Nothing AndAlso wsResponse.DeliveryEstimates IsNot Nothing AndAlso wsResponse.DeliveryEstimates.Length > 0 Then
                State.DeliveryDateList = wsResponse.DeliveryEstimates
                ShowInitDeliveryEstimates()
            Else
                ClearDisableAll()
                Page.MasterPage.MessageController.AddInformation(Message.MSG_ERR_ESTIMATED_DELIVERY_DATE_NOT_FOUND, True)
                Exit Sub
            End If
        Catch fex As FaultException
            ClearDisableAll()
            ShowFaultException(fex)
        Catch ex As Exception
            ClearDisableAll()
            Throw
        End Try
    End Sub

    Private Sub ShowDeliveryEstimate()

        If String.IsNullOrEmpty(State.CurrentEstimate.CourierCode) = True Then
            trCourierProduct.Attributes("style") = "display: none"
        End If

        If State.CurrentEstimate.Behavior.UseDeliverySlot = False Then
            trDeliverySlot.Attributes("style") = "display: none"
        End If

        EnableDisableNotSpecifyCheckBox()
        ShowDeliveryTimeRange(State.CurrentEstimate)
    End Sub

    Private Sub EnableDisableNotSpecifyCheckBox()
        ''if selection is not allowed, then uncheck and disable the check box
        If (Not State Is Nothing AndAlso Not State.CurrentEstimate Is Nothing AndAlso Not State.CurrentEstimate.Behavior Is Nothing) Then
            chkNotSpecify.Checked = True

            If (State.CurrentEstimate.Behavior.SelectionAllowed = False) Then
                chkNotSpecify.Enabled = False
                Page.MasterPage.MessageController.AddInformation(Message.MSG_ERR_ESTIMATED_DELIVERY_DATE_TIME_NOT_SELECTABLE, True)
            Else
                chkNotSpecify.Enabled = True
            End If
        End If

        SetControlStatus()
    End Sub

    Private Sub ShowInitDeliveryEstimates()
        Dim blnUseCourier As Boolean = False
        State.CurrentEstimate = State.DeliveryDateList.First()

        If String.IsNullOrEmpty(State.CurrentEstimate.CourierCode) = False Then
            blnUseCourier = True
        End If

        If blnUseCourier = False Then ' no courier product
            If State.DeliveryDateList.Count() > 1 Then 'error response, if no courier product, only one estimate should be returned
                ClearDisableAll()
                Page.MasterPage.MessageController.AddInformation("INVALID_DELIVERY_ESTIMATE", True)
                Exit Sub
            End If
        Else ' populate the courier product dropdown
            For Each de As DeliveryEstimate In State.DeliveryDateList
                ddlCourierProduct.Items.Add(New ListItem() With {.Text = $"{de.CourierCode} - {de.CourierProductCode}", .Value = $"{de.CourierCode}{de.CourierProductCode}"})
            Next
            ddlCourierProduct.SelectedValue = $"{State.CurrentEstimate.CourierCode}{State.CurrentEstimate.CourierProductCode}"
        End If

        ShowDeliveryEstimate()

    End Sub
    Private Sub ShowFaultException(ByVal fex As FaultException)
        If fex IsNot Nothing Then
            If fex.Code IsNot Nothing Then
                ' TODO - Standardize this fault exception handling in future based on the response from the web service
                Select Case fex.Code.Name.ToUpper()
                    Case "DEVELIVERYESTIMATENOTFOUND" '"CONFIGURATIONNOTFOUND"
                        Page.MasterPage.MessageController.AddInformation(Message.MSG_ERR_ESTIMATED_DELIVERY_DATE_NOT_FOUND, True)
                    Case "SERVICECENTERNOTFOUND"
                        Page.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_WEB_APP_GATEWAY_SERVICE_ERR) & " - " & TranslationBase.TranslateLabelOrMessage("SERVICE_CENTER_NOT_FOUND_ERR"), False)
                    Case Else
                        Page.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_WEB_APP_GATEWAY_SERVICE_ERR) & " - " & fex.Message, False)
                End Select
            Else
                Page.MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_WEB_APP_GATEWAY_SERVICE_ERR) & " - " & fex.Message, False)
            End If
        End If
    End Sub


    Private Sub ShowDeliveryTimeRange(ByVal de As DeliveryEstimate)
        Dim fastestDeliveryDateTime As String = String.Empty
        Dim fastestDeliveryDate As DeliveryDay = (From delDay As DeliveryDay In de.AvailableDeliveryDays Select delDay Order By delDay.DeliveryDate Ascending).First()
        If fastestDeliveryDate IsNot Nothing Then
            Dim fDeliveryDate As String
            If Thread.CurrentThread.CurrentCulture.ToString() = "ja-JP" Then
                fDeliveryDate = fastestDeliveryDate.DeliveryDate.ToString("dd-MMM-yyyy", DateTimeFormatInfo.InvariantInfo)
            Else
                fDeliveryDate = ElitaPlusPage.GetDateFormattedStringNullable(fastestDeliveryDate.DeliveryDate)
            End If

            If de.Behavior.UseDeliverySlot Then 'add slot information 
                If fastestDeliveryDate.DeliverySlots IsNot Nothing AndAlso fastestDeliveryDate.DeliverySlots.Length > 0 AndAlso de.Behavior.SelectionAllowed Then
                    Dim fastestDeliveryTimeSlot As DeliverySlot = (From delSlot As DeliverySlot In fastestDeliveryDate.DeliverySlots Select delSlot Order By delSlot.Sequence Ascending).First()
                    If fastestDeliveryTimeSlot IsNot Nothing Then
                        fastestDeliveryDateTime = If(LookupListNew.GetDescriptionFromCode(LookupListNew.LK_DESIRED_DELIVERY_TIME_SLOT, fastestDeliveryTimeSlot.Description, Authentication.CurrentUser.LanguageId), fastestDeliveryTimeSlot.Description) + " " + fDeliveryDate
                    Else
                        fastestDeliveryDateTime = TranslationBase.TranslateLabelOrMessage("TIME_SLOT_NOT_APPLICABLE") + " " + fDeliveryDate
                    End If
                Else
                    fastestDeliveryDateTime = TranslationBase.TranslateLabelOrMessage("TIME_SLOT_NOT_APPLICABLE") + " " + fDeliveryDate
                End If
            Else
                fastestDeliveryDateTime = fDeliveryDate
            End If
        End If

        Dim lastDeliveryDateTime As String = String.Empty
        Dim lastDeliveryDate As DeliveryDay = (From delDay As DeliveryDay In de.AvailableDeliveryDays Select delDay Order By delDay.DeliveryDate Ascending).Last()
        If lastDeliveryDate IsNot Nothing Then
            Dim lDeliveryDate As String
            If Thread.CurrentThread.CurrentCulture.ToString() = "ja-JP" Then
                lDeliveryDate = lastDeliveryDate.DeliveryDate.ToString("dd-MMM-yyyy", DateTimeFormatInfo.InvariantInfo)
            Else
                lDeliveryDate = ElitaPlusPage.GetDateFormattedStringNullable(lastDeliveryDate.DeliveryDate)
            End If

            If de.Behavior.UseDeliverySlot Then 'add slot information 
                If lastDeliveryDate.DeliverySlots IsNot Nothing AndAlso lastDeliveryDate.DeliverySlots.Length > 0 AndAlso de.Behavior.SelectionAllowed Then
                    Dim lastDeliveryTimeSlot As DeliverySlot = (From delSlot As DeliverySlot In lastDeliveryDate.DeliverySlots Select delSlot Order By delSlot.Sequence Ascending).Last()
                    If lastDeliveryTimeSlot IsNot Nothing Then
                        lastDeliveryDateTime = If(LookupListNew.GetDescriptionFromCode(LookupListNew.LK_DESIRED_DELIVERY_TIME_SLOT, lastDeliveryTimeSlot.Description, Authentication.CurrentUser.LanguageId), lastDeliveryTimeSlot.Description) + " " + lDeliveryDate
                    Else
                        lastDeliveryDateTime = TranslationBase.TranslateLabelOrMessage("TIME_SLOT_NOT_APPLICABLE") + " " + lDeliveryDate
                    End If
                Else
                    lastDeliveryDateTime = TranslationBase.TranslateLabelOrMessage("TIME_SLOT_NOT_APPLICABLE") + " " + lDeliveryDate
                End If
            Else
                lastDeliveryDateTime = lDeliveryDate
            End If
        End If
        AvailableDeliveryDateTiming = fastestDeliveryDateTime + " ~ " + lastDeliveryDateTime
    End Sub

    Private Sub ResetToDefaultSetting()
        txtDeliveryDate.Text = String.Empty

        If DeliveryDate.HasValue Then
            DeliveryDate = Nothing
        End If

        ddlDeliverySlots.Items.Clear()
        If chkNotSpecify.Checked Then 'Not specified checked, disable controls
            EnableDisableControl(False)
        Else
            EnableDisableControl(True) 'Not specified checked, enable controls
        End If
    End Sub

    Private Sub EnableDisableControl(ByVal bEnable As Boolean)
        Page.ChangeEnabledControlProperty(txtDeliveryDate, bEnable)
        Page.ChangeEnabledControlProperty(imageBtnDeliveryDate, bEnable)
        Page.ChangeEnabledControlProperty(ddlDeliverySlots, bEnable)
    End Sub
    Private Sub ClearDisableAll()
        ResetToDefaultSetting()
        AvailableDeliveryDateTiming = String.Empty
        Page.ChangeEnabledControlProperty(chkNotSpecify, False)
    End Sub

    Protected Sub ddlCourierProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourierProduct.SelectedIndexChanged
        State.CurrentEstimate = State.DeliveryDateList.FirstOrDefault(Function(de) $"{de.CourierCode}{de.CourierProductCode}" = ddlCourierProduct.SelectedValue)
        ShowDeliveryEstimate()
    End Sub

    Private Sub UserControlDeliverySlot_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ' hide courier and delivery slot based on estimate data
        If Not State.CurrentEstimate Is Nothing AndAlso String.IsNullOrEmpty(State.CurrentEstimate.CourierCode) = True Then
            trCourierProduct.Attributes("style") = "display: none"
        End If

        If Not State.CurrentEstimate Is Nothing AndAlso State.CurrentEstimate.Behavior.UseDeliverySlot = False Then
            trDeliverySlot.Attributes("style") = "display: none"
        End If

        If State.DeliveryDateList Is Nothing OrElse State.DeliveryDateList.Count() = 0 Then 'No date returned
            Visible = False
        End If
    End Sub

#End Region
End Class
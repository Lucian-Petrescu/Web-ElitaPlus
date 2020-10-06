Option Strict On
Option Explicit On
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Partial Class ShippingInfoForm
    Inherits ElitaPlusPage

    Protected WithEvents ErrController As ErrorController

#Region "Member Variables"
    Protected WithEvents SearchDescriptionLabel As System.Web.UI.WebControls.Label
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

#Region "Properties"

#End Region

#Region "Constants"
    Public Const URL As String = "ShippingInfoForm.aspx"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Claim
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Claim)
            LastOperation = LastOp
            EditingBo = curEditingBo
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ShippingInfoId As Guid

        Public Sub New(ShippingInfoId As Guid)
            Me.ShippingInfoId = ShippingInfoId
        End Sub
    End Class
#End Region

    Public Enum InternalStates
        Regular
        ConfirmCreateWithAuthorizationRequired
        ConfirmAcknowledgementForClaimAdded
        ConfirmBackOnError
    End Enum
#Region "Page State"


    Class MyState
        Public MyBO, ScreenSnapShotBO As ShippingInfo
        Public CertItemCoverageBO As CertItemCoverage
        Public InputParameters As Parameters
        Public LastState As InternalStates = InternalStates.Regular
        Public LastErrMsg As String
        Public IsComingFromClaimDetail As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(NavController.State, MyState)

        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Try
            Dim objServiceCenter As ServiceCenter = CType(NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER), ServiceCenter)
            Dim objCertItemCoverage As CertItemCoverage = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE), CertItemCoverage)
            If objServiceCenter IsNot Nothing AndAlso objCertItemCoverage IsNot Nothing Then
                State.MyBO = New ShippingInfo
                State.MyBO.PrePopulate(objCertItemCoverage, objServiceCenter)
                State.CertItemCoverageBO = objCertItemCoverage
                State.IsComingFromClaimDetail = False
            Else
                State.InputParameters = CType(NavController.ParametersPassed, Parameters)
                State.MyBO = New ShippingInfo(State.InputParameters.ShippingInfoId)
                State.IsComingFromClaimDetail = True
            End If

            State.ScreenSnapShotBO = New ShippingInfo
            State.ScreenSnapShotBO.Clone(State.MyBO)

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try

            If CallingParameters IsNot Nothing Then
                If CType(CallingParameters, Guid).Equals(Guid.Empty) Then
                    State.MyBO = New ShippingInfo(CType(CallingParameters, Guid))
                    State.IsComingFromClaimDetail = True
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub
#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        ErrController.Clear_Hide()
        Try
            If Not Page.IsPostBack Then
                Trace(Me, "Shp_Id=" & GuidControl.GuidToHexString(State.MyBO.Id))
                MenuEnabled = False
                ShowMissingTranslations(ErrController)
                SetButtonsState()
                PopulateDropdowns()
                PopulateFormFromBOs()
                SetButtonsState()
                InitialEnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "CountryId", moCountryLabel)
        BindBOPropertyToLabel(State.MyBO, "RegionId", moRegionLabel)
        BindBOPropertyToLabel(State.MyBO, "Address1", moAddress1Label)
        BindBOPropertyToLabel(State.MyBO, "Address2", moAddress2Label)
        BindBOPropertyToLabel(State.MyBO, "City", moCityLabel)
        BindBOPropertyToLabel(State.MyBO, "PostalCode", moPostalLabel)
        BindBOPropertyToLabel(State.MyBO, "CreditCardNumber", LabelCredit_Card_Number)
        BindBOPropertyToLabel(State.MyBO, "AuthorizationNumber", LabelAuthorization_Number)
        BindBOPropertyToLabel(State.MyBO, "ProcessingFee", LabelPROCESSING_FEE)
        BindBOPropertyToLabel(State.MyBO, "TotalCharge", LabelTOTAL_CHARGE)

        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns(Optional ByVal blnRegionListOnly As Boolean = False)

        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CountryId = State.MyBO.CountryId
        Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
        moRegionDrop_WRITE.Populate(oRegionList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })

        If Not blnRegionListOnly Then

            Dim oCountryList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")
            moCountryDrop_WRITE.Populate(oCountryList, New PopulateOptions() With
                                            {
                                            .AddBlankItem = True
                                            })
        End If

    End Sub


    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            SetSelectedItem(moCountryDrop_WRITE, .CountryId)
            PopulateDropdowns(True)
            SetSelectedItem(moRegionDrop_WRITE, .RegionId)
            PopulateControlFromBOProperty(moAddress1Text, .Address1)
            PopulateControlFromBOProperty(moAddress2Text, .Address2)
            PopulateControlFromBOProperty(moCityText, .City)
            PopulateControlFromBOProperty(moPostalText, .PostalCode)
            PopulateControlFromBOProperty(TextboxCredit_Card_Number, .CreditCardNumber)
            PopulateControlFromBOProperty(TextboxAuthorization_Number, .AuthorizationNumber)
            PopulateControlFromBOProperty(TextboxPROCESSING_FEE, .ProcessingFee)
            PopulateControlFromBOProperty(TextboxTOTAL_CHARGE, .TotalCharge)
        End With
    End Sub

    Protected Sub PopulateBOFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop_WRITE)
            PopulateBOProperty(State.MyBO, "RegionId", moRegionDrop_WRITE)
            PopulateBOProperty(State.MyBO, "Address1", moAddress1Text)
            PopulateBOProperty(State.MyBO, "Address2", moAddress2Text)
            PopulateBOProperty(State.MyBO, "City", moCityText)
            PopulateBOProperty(State.MyBO, "PostalCode", moPostalText)
            PopulateBOProperty(State.MyBO, "CreditCardNumber", TextboxCredit_Card_Number)
            PopulateBOProperty(State.MyBO, "AuthorizationNumber", TextboxAuthorization_Number)
            PopulateBOProperty(State.MyBO, "ProcessingFee", TextboxPROCESSING_FEE)
            PopulateBOProperty(State.MyBO, "TotalCharge", TextboxTOTAL_CHARGE)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.IsComingFromClaimDetail Then
                State.MyBO.Save()
            End If
            NavController.Navigate(Me, "back")
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.IsComingFromClaimDetail Then
                NavController.Navigate(Me, "back")
            End If
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""

    End Sub

#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOFormFrom()
            If (State.IsComingFromClaimDetail) Then    'Button is Save
                If (State.MyBO.IsDirty) Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    NavController.Navigate(Me, "back")
                End If
            Else    'Button is Next
                If (State.MyBO.IsDirty) Then
                    DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    NavController.Navigate(Me, "back")
                End If
            End If


        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.LastState = InternalStates.ConfirmBackOnError
            State.LastErrMsg = ErrController.Text
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFormFrom()
            If (State.IsComingFromClaimDetail) Then    'Button is Save
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    NavController.Navigate(Me, FlowEvents.EVENT_SHIPPING_UPDATED, Message.MSG_SHIPPING_INFO_UPDATED)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Else    'Button is Next
                If (State.MyBO.IsDirty) Then State.MyBO.Validate()
                NavController.FlowSession(FlowSessionKeys.SESSION_SHIPPING_INFO) = State.MyBO
                NavController.Navigate(Me, "create_new_claim")
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew AndAlso State.ScreenSnapShotBO IsNot Nothing Then
                State.MyBO.Clone(State.ScreenSnapShotBO)
            End If
            PopulateFormFromBOs()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub


    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged

        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CountryId = GetSelectedItem(moCountryDrop_WRITE)
        Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
        moRegionDrop_WRITE.Populate(oRegionList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })

    End Sub

#End Region

#Region "Page Control Events"
    Private Sub SetButtonsState()

        If (State.IsComingFromClaimDetail) Then
            SaveButton_WRITE.Text = TranslationBase.TranslateLabelOrMessage("Save")
        Else
            SaveButton_WRITE.Text = TranslationBase.TranslateLabelOrMessage("Next")
        End If

    End Sub
    Protected Sub InitialEnableDisableFields()
        'read only fields
        ChangeEnabledProperty(LabelPROCESSING_FEE, False)
        ChangeEnabledProperty(LabelTOTAL_CHARGE, False)
        ChangeEnabledProperty(TextboxPROCESSING_FEE, False)
        ChangeEnabledProperty(TextboxTOTAL_CHARGE, False)
    End Sub
#End Region


#Region "Error Handling"

#End Region

End Class

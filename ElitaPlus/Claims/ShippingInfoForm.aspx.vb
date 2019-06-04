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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Claim)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ShippingInfoId As Guid

        Public Sub New(ByVal ShippingInfoId As Guid)
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)

        End Get
    End Property

    Protected Sub InitializeFromFlowSession()
        Try
            Dim objServiceCenter As ServiceCenter = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER), ServiceCenter)
            Dim objCertItemCoverage As CertItemCoverage = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE), CertItemCoverage)
            If Not objServiceCenter Is Nothing AndAlso Not objCertItemCoverage Is Nothing Then
                Me.State.MyBO = New ShippingInfo
                Me.State.MyBO.PrePopulate(objCertItemCoverage, objServiceCenter)
                Me.State.CertItemCoverageBO = objCertItemCoverage
                Me.State.IsComingFromClaimDetail = False
            Else
                Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)
                Me.State.MyBO = New ShippingInfo(State.InputParameters.ShippingInfoId)
                Me.State.IsComingFromClaimDetail = True
            End If

            Me.State.ScreenSnapShotBO = New ShippingInfo
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try

            If Not Me.CallingParameters Is Nothing Then
                If CType(Me.CallingParameters, Guid).Equals(Guid.Empty) Then
                    Me.State.MyBO = New ShippingInfo(CType(Me.CallingParameters, Guid))
                    Me.State.IsComingFromClaimDetail = True
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub
#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        ErrController.Clear_Hide()
        Try
            If Not Page.IsPostBack Then
                Trace(Me, "Shp_Id=" & GuidControl.GuidToHexString(Me.State.MyBO.Id))
                Me.MenuEnabled = False
                Me.ShowMissingTranslations(ErrController)
                SetButtonsState()
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.SetButtonsState()
                Me.InitialEnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CountryId", Me.moCountryLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RegionId", Me.moRegionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Address1", Me.moAddress1Label)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Address2", Me.moAddress2Label)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "City", Me.moCityLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PostalCode", Me.moPostalLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CreditCardNumber", Me.LabelCredit_Card_Number)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthorizationNumber", Me.LabelAuthorization_Number)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProcessingFee", Me.LabelPROCESSING_FEE)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TotalCharge", Me.LabelTOTAL_CHARGE)

        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns(Optional ByVal blnRegionListOnly As Boolean = False)

        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CountryId = Me.State.MyBO.CountryId
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
        With Me.State.MyBO
            Me.SetSelectedItem(Me.moCountryDrop_WRITE, .CountryId)
            Me.PopulateDropdowns(True)
            Me.SetSelectedItem(Me.moRegionDrop_WRITE, .RegionId)
            Me.PopulateControlFromBOProperty(Me.moAddress1Text, .Address1)
            Me.PopulateControlFromBOProperty(Me.moAddress2Text, .Address2)
            Me.PopulateControlFromBOProperty(Me.moCityText, .City)
            Me.PopulateControlFromBOProperty(Me.moPostalText, .PostalCode)
            Me.PopulateControlFromBOProperty(Me.TextboxCredit_Card_Number, .CreditCardNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxAuthorization_Number, .AuthorizationNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxPROCESSING_FEE, .ProcessingFee)
            Me.PopulateControlFromBOProperty(Me.TextboxTOTAL_CHARGE, .TotalCharge)
        End With
    End Sub

    Protected Sub PopulateBOFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "CountryId", Me.moCountryDrop_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "RegionId", Me.moRegionDrop_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "Address1", Me.moAddress1Text)
            Me.PopulateBOProperty(Me.State.MyBO, "Address2", Me.moAddress2Text)
            Me.PopulateBOProperty(Me.State.MyBO, "City", Me.moCityText)
            Me.PopulateBOProperty(Me.State.MyBO, "PostalCode", Me.moPostalText)
            Me.PopulateBOProperty(Me.State.MyBO, "CreditCardNumber", Me.TextboxCredit_Card_Number)
            Me.PopulateBOProperty(Me.State.MyBO, "AuthorizationNumber", Me.TextboxAuthorization_Number)
            Me.PopulateBOProperty(Me.State.MyBO, "ProcessingFee", Me.TextboxPROCESSING_FEE)
            Me.PopulateBOProperty(Me.State.MyBO, "TotalCharge", Me.TextboxTOTAL_CHARGE)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.IsComingFromClaimDetail Then
                Me.State.MyBO.Save()
            End If
            Me.NavController.Navigate(Me, "back")
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.IsComingFromClaimDetail Then
                Me.NavController.Navigate(Me, "back")
            End If
        End If

        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""

    End Sub

#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOFormFrom()
            If (Me.State.IsComingFromClaimDetail) Then    'Button is Save
                If (Me.State.MyBO.IsDirty) Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.NavController.Navigate(Me, "back")
                End If
            Else    'Button is Next
                If (Me.State.MyBO.IsDirty) Then
                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_CHANGES, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.NavController.Navigate(Me, "back")
                End If
            End If


        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.LastState = InternalStates.ConfirmBackOnError
            Me.State.LastErrMsg = Me.ErrController.Text
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            Me.PopulateBOFormFrom()
            If (Me.State.IsComingFromClaimDetail) Then    'Button is Save
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_SHIPPING_UPDATED, Message.MSG_SHIPPING_INFO_UPDATED)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
            Else    'Button is Next
                If (Me.State.MyBO.IsDirty) Then Me.State.MyBO.Validate()
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_SHIPPING_INFO) = Me.State.MyBO
                Me.NavController.Navigate(Me, "create_new_claim")
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew AndAlso Not Me.State.ScreenSnapShotBO Is Nothing Then
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            End If
            Me.PopulateFormFromBOs()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub


    Private Sub moCountryDrop_WRITE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moCountryDrop_WRITE.SelectedIndexChanged

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

        If (Me.State.IsComingFromClaimDetail) Then
            Me.SaveButton_WRITE.Text = TranslationBase.TranslateLabelOrMessage("Save")
        Else
            Me.SaveButton_WRITE.Text = TranslationBase.TranslateLabelOrMessage("Next")
        End If

    End Sub
    Protected Sub InitialEnableDisableFields()
        'read only fields
        Me.ChangeEnabledProperty(Me.LabelPROCESSING_FEE, False)
        Me.ChangeEnabledProperty(Me.LabelTOTAL_CHARGE, False)
        Me.ChangeEnabledProperty(Me.TextboxPROCESSING_FEE, False)
        Me.ChangeEnabledProperty(Me.TextboxTOTAL_CHARGE, False)
    End Sub
#End Region


#Region "Error Handling"

#End Region

End Class

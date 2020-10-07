Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.Elita.ClientIntegration
Imports Assurant.ElitaPlus.ElitaPlusWebApp.GiftCardService
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Certificates
    Partial Class RewardDetailsForm
        Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub


#End Region

#Region "Constants"
        Public Const URL As String = "RewardDetailsForm.aspx"

        Private Const UserName = "CLAIM_RECSERVICE_USERNAME"
        Private Const Password = "CLAIM_RECSERVICE_PASSWORD"
        Private Const GiftCardServiceUrl = "GIFTCARD_SERVICE_URL"

#End Region
#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Rewards
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Rewards, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

        Class MyState
            Public RewardId As Guid = Guid.Empty

            Public MyBO As Rewards

            Public RewardType As String = String.Empty
            Public CertNumber As String = String.Empty

            Public IsEditMode As Boolean = False



            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

            Public LastErrMsg As String
            Public HasDataChanged As Boolean = False

            Public SortExpression As String = PriceListDetail.PriceListDetailSearchDV.COL_EXPIRATION
            Public DetailSearchDV As PriceList.PriceListDetailSelectionView = Nothing
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 30

            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer

            Public Company As String
            Public Dealer As String
            Public GiftCardId As Guid

            Public IsNew As Boolean = False
            Public SelectedSvcCtrCount As Integer = 0

            Sub New()
            End Sub
        End Class
        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.MyBO = New Rewards(CType(CallingParameters, Guid))

                    State.IsEditMode = True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                Dim retObj As RewardDetailsForm.ReturnType = CType(ReturnPar, RewardDetailsForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.RewardId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try

                UpdateBreadcrum()
                MasterPage.MessageController.Clear()

                If Not IsPostBack Then
                    MenuEnabled = False
                    ' Me.AddControlMsg(Me.btnDelete, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                    If State.MyBO Is Nothing Then
                        State.MyBO = New Rewards
                        State.IsNew = True
                    End If

                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    'Me.EnableDisableFields(True)

                End If

                CheckIfComingFromSaveConfirm()
                BindBoPropertiesToLabels()

                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub
#End Region

        Private Sub UpdateBreadcrum()
            'Breadcrumb and titles
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("REWARD_SEARCH")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("REWARD_DETAILS")
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CERTIFICATES") & ElitaBase.Sperator & MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EDIT_REWARD_DETAILS")

        End Sub

#Region "Button Clicks"

        ''' <summary>
        ''' main back button
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Dim myBo As Rewards = State.MyBO
                Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, False)
                NavController = Nothing
                ReturnToCallingPage(retObj)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Nothing)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        ''' <summary>
        ''' Main save button
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' 
        Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try

                PopulateBOsFormFrom()
                State.MyBO.Validate()

                If (State.MyBO.IsDirty OrElse State.MyBO.IsFamilyDirty) Then
                    State.HasDataChanged = True
                    if State.MyBO.RewardPymtModeXcd IsNot Nothing AndAlso State.MyBO.RewardPymtModeXcd = Codes.REWARD_PYMT_MODE__GIFTCARD AndAlso
State.MyBO.RewardStatusXcd IsNot Nothing AndAlso State.MyBO.RewardStatusXcd = Codes.REWARD_STATUS__APPROVED then
                        GetGiftCardInfo()
                    End If

                    State.MyBO.Save()
                    State.HasDataChanged = False
                    PopulateFormFromBOs()

                      ClearGridViewHeadersAndLabelsErrSign()
                    MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                Else
                    MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        ''' <summary>
        ''' Main undo button
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnUndo_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Rewards(State.MyBO.Id)
                Else
                    State.MyBO = New Rewards
                End If
                PopulateFormFromBOs()
                'Me.EnableDisableFields(True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "RewardTypeXcd", moRewardTypelbl)
            BindBOPropertyToLabel(State.MyBO, "CertNumber", moCertNumberlbl)
            BindBOPropertyToLabel(State.MyBO, "RewardStatusXcd", moRewardStatuslbl)
            BindBOPropertyToLabel(State.MyBO, "RewardPymtModeXcd", moRewardPayModelbl)
            BindBOPropertyToLabel(State.MyBO, "FormSignedXcd", moFormSignedlbl)
            BindBOPropertyToLabel(State.MyBO, "SubscriptionFormSignedXcd", moSubscFormSignedlbl)
            BindBOPropertyToLabel(State.MyBO, "RibSignedXcd", moRibSignedlbl)
            BindBOPropertyToLabel(State.MyBO, "SequenceNumber", moSeqNumberlbl)
            BindBOPropertyToLabel(State.MyBO, "InvoiceSignedXcd", moInvoiceSignedlbl)
            BindBOPropertyToLabel(State.MyBO, "RewardAmount", moRewardAmountlbl)


        End Sub

        ''' <summary>
        ''' Put back the current form values to BO
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub PopulateBOsFormFrom()
            With State.MyBO
                PopulateBOProperty(State.MyBO, "RewardTypeXcd", moRewardTypeDD, False, True)
                PopulateBOProperty(State.MyBO, "RewardAmount", moRewardAmountTxt)
                PopulateBOProperty(State.MyBO, "CertNumber", moCertNumberTxt.Text)
                PopulateBOProperty(State.MyBO, "SequenceNumber", moSeqNumberTxt.Text)
                PopulateBOProperty(State.MyBO, "RewardStatusXcd", moRewardStatusDD, False, True)
                PopulateBOProperty(State.MyBO, "RewardPymtModeXcd", moRewardPayModeDD, False, True)
                PopulateBOProperty(State.MyBO, "FormSignedXcd", moFormSignedDD, False, True)
                PopulateBOProperty(State.MyBO, "SubscriptionFormSignedXcd", moSubscFormSignedDD, False, True)
                PopulateBOProperty(State.MyBO, "InvoiceSignedXcd", moInvoiceSignedDD, False, True)
                PopulateBOProperty(State.MyBO, "RibSignedXcd", moRibSignedDD, False, True)
                PopulateBOProperty(State.MyBO, "RewardPymtModeXcd", moRewardPayModeDD, False, True)

            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                ' Me.moRewardTypeDD.PopulateOld("REWARD_TYPE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moRewardTypeDD.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RewardTypeXcd, moRewardTypeDD)
                PopulateControlFromBOProperty(moRewardAmountTxt, .RewardAmount)
                PopulateControlFromBOProperty(moCertNumberTxt, .CertNumber)
                ''  Me.moRewardStatusDD.PopulateOld("REWARD_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moRewardStatusDD.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RewardStatusXcd, moRewardStatusDD)
                ' Me.moRibSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moRibSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RibSignedXcd, moRibSignedDD)

                ' Me.moInvoiceSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moInvoiceSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.InvoiceSignedXcd, moInvoiceSignedDD)
                ' moFormSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moFormSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.FormSignedXcd, moFormSignedDD)
                ' moSubscFormSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moSubscFormSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.SubscriptionFormSignedXcd, moSubscFormSignedDD)
                'Me.moRewardPayModeDD.PopulateOld("REWARD_PYMT_MODE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moRewardPayModeDD.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_PYMT_MODE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RewardPymtModeXcd, moRewardPayModeDD)
                PopulateControlFromBOProperty(moSeqNumberTxt, .SequenceNumber)
                PopulateControlFromBOProperty(moIbanNumberTxt, .IbanNumber)
                PopulateControlFromBOProperty(moSwiftCodeTxt, .SwiftCode)

                EnableDisableFields(True)

                If (.RewardStatusXcd = Codes.REWARD_STATUS__GIFT_CARD_SENT Or .RewardStatusXcd = Codes.REWARD_STATUS__SEPA_XFER_SENT) Then
                    ControlMgr.SetEnableControl(Me, moRewardStatusDD, False)
                    ControlMgr.SetEnableControl(Me, moRewardTypeDD, False)
                    ControlMgr.SetEnableControl(Me, moRibSignedDD, False)
                    ControlMgr.SetEnableControl(Me, moInvoiceSignedDD, False)
                    ControlMgr.SetEnableControl(Me, moFormSignedDD, False)
                    ControlMgr.SetEnableControl(Me, moSubscFormSignedDD, False)
                    ControlMgr.SetEnableControl(Me, moRewardPayModeDD, False)
                    ControlMgr.SetEnableControl(Me, moRewardAmountTxt, False)
                    ControlMgr.SetEnableControl(Me, btnApply_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
                End If                '

            End With
        End Sub

        Sub EnableDisableButtons(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        End Sub

        Protected Sub EnableDisableFields(toggle As Boolean)

            EnableDisableButtons(toggle)
            If State.IsEditMode Then
                ControlMgr.SetEnableControl(Me, btnUndo_WRITE, True)
            Else
                ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
            End If

        End Sub
        Protected Sub CreateNew()
            State.MyBO = New Rewards
            PopulateFormFromBOs()
            State.IsEditMode = False
            'Me.EnableDisableFields(True)
        End Sub


        Protected Sub CheckIfComingFromSaveConfirm()

            'Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                BindBoPropertiesToLabels()
                State.MyBO.Save()
            End If

            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If State.MyBO.IsDirty Then
                        State.MyBO.Save()
                        State.HasDataChanged = True
                        PopulateFormFromBOs()
                        MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Else
                        MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                    End If
            End Select

            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        End Sub

        Protected Sub PopulateDropdowns()
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        End Sub


        Private Sub GetGiftCardInfo()


             Dim GiftRequest As GiftCardRequest = New GiftCardRequest()
             Dim Referenceinfo As New CertificateReference()
             

            Referenceinfo.CompanyCode=State.MyBO.Company
            Referenceinfo.CertificateNumber=State.MyBO.CertNumber
            Referenceinfo.DealerCode=State.MyBO.Dealer

            GiftRequest.ReferenceType = EntityTypes.Certificate
            GiftRequest.PurposeCode="Rewards"
            GiftRequest.Amount=State.MyBO.RewardAmount
            GiftRequest.Reference = Referenceinfo


            Dim GiftCardResponse As GiftCardResponse
            Try

                GiftCardResponse = WcfClientHelper.Execute(Of GiftCardServiceClient, IGiftCardService, GiftCardResponse)(
                                                                GetClient(),
                                                                New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = ElitaPlusIdentity.Current.ActiveUser.NetworkId}},
                                                                Function(c As GiftCardServiceClient)
                                                                    Return c.ProcureGiftCard(GiftRequest)
                                                                End Function)

                State.MyBO.GiftCardRequestId=GiftCardResponse.GiftCardId

            Catch ex As FaultException
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR, TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR) & " - " & ex.Message)
            End Try

        End Sub

        Private Shared Function GetClient() As GiftCardServiceClient
            'Dim client = New GiftCardServiceClient("CustomBinding_IGiftCardService", "http://localhost/ElitaClaimService/GiftCardService.svc")
            Dim client = New GiftCardServiceClient("CustomBinding_IGiftCardService", ConfigurationManager.AppSettings(GiftCardServiceUrl))
            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings(UserName)
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings(Password)
            Return client
        End Function
#End Region

    End Class

End Namespace
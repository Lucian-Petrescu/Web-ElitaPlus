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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Rewards, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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
        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.MyBO = New Rewards(CType(Me.CallingParameters, Guid))

                    Me.State.IsEditMode = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Dim retObj As RewardDetailsForm.ReturnType = CType(ReturnPar, RewardDetailsForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.RewardId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try

                Me.UpdateBreadcrum()
                Me.MasterPage.MessageController.Clear()

                If Not Me.IsPostBack Then
                    Me.MenuEnabled = False
                    ' Me.AddControlMsg(Me.btnDelete, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New Rewards
                        Me.State.IsNew = True
                    End If

                    Me.PopulateDropdowns()
                    Me.PopulateFormFromBOs()
                    'Me.EnableDisableFields(True)

                End If

                Me.CheckIfComingFromSaveConfirm()
                Me.BindBoPropertiesToLabels()

                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub
#End Region

        Private Sub UpdateBreadcrum()
            'Breadcrumb and titles
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("REWARD_SEARCH")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("REWARD_DETAILS")
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CERTIFICATES") & ElitaBase.Sperator & Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EDIT_REWARD_DETAILS")

        End Sub

#Region "Button Clicks"

        ''' <summary>
        ''' main back button
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Dim myBo As Rewards = Me.State.MyBO
                Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, myBo, False)
                Me.NavController = Nothing
                Me.ReturnToCallingPage(retObj)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Nothing)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub

        ''' <summary>
        ''' Main save button
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' 
        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try

                Me.PopulateBOsFormFrom()
                Me.State.MyBO.Validate()

                If (Me.State.MyBO.IsDirty OrElse Me.State.MyBO.IsFamilyDirty) Then
                    Me.State.HasDataChanged = True
                    if Not Me.State.MyBO.RewardPymtModeXcd Is Nothing AndAlso Me.State.MyBO.RewardPymtModeXcd = Codes.REWARD_PYMT_MODE__GIFTCARD AndAlso
                        Not Me.State.MyBO.RewardStatusXcd Is Nothing AndAlso Me.State.MyBO.RewardStatusXcd = Codes.REWARD_STATUS__APPROVED then
                        GetGiftCardInfo()
                    End If

                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = False
                    Me.PopulateFormFromBOs()

                      Me.ClearGridViewHeadersAndLabelsErrSign()
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        ''' <summary>
        ''' Main undo button
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub btnUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Rewards(Me.State.MyBO.Id)
                Else
                    Me.State.MyBO = New Rewards
                End If
                Me.PopulateFormFromBOs()
                'Me.EnableDisableFields(True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RewardTypeXcd", Me.moRewardTypelbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertNumber", Me.moCertNumberlbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RewardStatusXcd", Me.moRewardStatuslbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RewardPymtModeXcd", Me.moRewardPayModelbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FormSignedXcd", Me.moFormSignedlbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SubscriptionFormSignedXcd", Me.moSubscFormSignedlbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RibSignedXcd", Me.moRibSignedlbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SequenceNumber", Me.moSeqNumberlbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceSignedXcd", Me.moInvoiceSignedlbl)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RewardAmount", Me.moRewardAmountlbl)


        End Sub

        ''' <summary>
        ''' Put back the current form values to BO
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub PopulateBOsFormFrom()
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "RewardTypeXcd", Me.moRewardTypeDD, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "RewardAmount", Me.moRewardAmountTxt)
                Me.PopulateBOProperty(Me.State.MyBO, "CertNumber", Me.moCertNumberTxt.Text)
                Me.PopulateBOProperty(Me.State.MyBO, "SequenceNumber", Me.moSeqNumberTxt.Text)
                Me.PopulateBOProperty(Me.State.MyBO, "RewardStatusXcd", Me.moRewardStatusDD, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "RewardPymtModeXcd", Me.moRewardPayModeDD, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "FormSignedXcd", Me.moFormSignedDD, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "SubscriptionFormSignedXcd", Me.moSubscFormSignedDD, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "InvoiceSignedXcd", Me.moInvoiceSignedDD, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "RibSignedXcd", Me.moRibSignedDD, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "RewardPymtModeXcd", Me.moRewardPayModeDD, False, True)

            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                ' Me.moRewardTypeDD.PopulateOld("REWARD_TYPE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                Me.moRewardTypeDD.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RewardTypeXcd, Me.moRewardTypeDD)
                Me.PopulateControlFromBOProperty(moRewardAmountTxt, .RewardAmount)
                Me.PopulateControlFromBOProperty(moCertNumberTxt, .CertNumber)
                ''  Me.moRewardStatusDD.PopulateOld("REWARD_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                Me.moRewardStatusDD.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RewardStatusXcd, Me.moRewardStatusDD)
                ' Me.moRibSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                Me.moRibSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RibSignedXcd, Me.moRibSignedDD)

                ' Me.moInvoiceSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                Me.moInvoiceSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.InvoiceSignedXcd, Me.moInvoiceSignedDD)
                ' moFormSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moFormSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.FormSignedXcd, Me.moFormSignedDD)
                ' moSubscFormSignedDD.PopulateOld("DOCUMENT_STATUS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                moSubscFormSignedDD.Populate(CommonConfigManager.Current.ListManager.GetList("DOCUMENT_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.SubscriptionFormSignedXcd, Me.moSubscFormSignedDD)
                'Me.moRewardPayModeDD.PopulateOld("REWARD_PYMT_MODE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
                Me.moRewardPayModeDD.Populate(CommonConfigManager.Current.ListManager.GetList("REWARD_PYMT_MODE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                   .ValueFunc = AddressOf .GetExtendedCode
                 })
                BindSelectItem(.RewardPymtModeXcd, Me.moRewardPayModeDD)
                Me.PopulateControlFromBOProperty(moSeqNumberTxt, .SequenceNumber)
                Me.PopulateControlFromBOProperty(moIbanNumberTxt, .IbanNumber)
                Me.PopulateControlFromBOProperty(moSwiftCodeTxt, .SwiftCode)

                Me.EnableDisableFields(True)

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

        Sub EnableDisableButtons(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        End Sub

        Protected Sub EnableDisableFields(ByVal toggle As Boolean)

            Me.EnableDisableButtons(toggle)
            If Me.State.IsEditMode Then
                ControlMgr.SetEnableControl(Me, btnUndo_WRITE, True)
            Else
                ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
            End If

        End Sub
        Protected Sub CreateNew()
            Me.State.MyBO = New Rewards
            Me.PopulateFormFromBOs()
            Me.State.IsEditMode = False
            'Me.EnableDisableFields(True)
        End Sub


        Protected Sub CheckIfComingFromSaveConfirm()

            'Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                Me.BindBoPropertiesToLabels()
                Me.State.MyBO.Save()
            End If

            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If Me.State.MyBO.IsDirty Then
                        Me.State.MyBO.Save()
                        Me.State.HasDataChanged = True
                        Me.PopulateFormFromBOs()
                        Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Else
                        Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                    End If
            End Select

            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        End Sub

        Protected Sub PopulateDropdowns()
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        End Sub


        Private Sub GetGiftCardInfo()


             Dim GiftRequest As GiftCardRequest = New GiftCardRequest()
             Dim Referenceinfo As New CertificateReference()
             

            Referenceinfo.CompanyCode=Me.State.MyBO.Company
            Referenceinfo.CertificateNumber=Me.State.MyBO.CertNumber
            Referenceinfo.DealerCode=Me.State.MyBO.Dealer

            GiftRequest.ReferenceType = EntityTypes.Certificate
            GiftRequest.PurposeCode="Rewards"
            GiftRequest.Amount=Me.State.MyBO.RewardAmount
            GiftRequest.Reference = Referenceinfo


            Dim GiftCardResponse As GiftCardResponse
            Try

                GiftCardResponse = WcfClientHelper.Execute(Of GiftCardServiceClient, IGiftCardService, GiftCardResponse)(
                                                                GetClient(),
                                                                New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = ElitaPlusIdentity.Current.ActiveUser.NetworkId}},
                                                                Function(ByVal c As GiftCardServiceClient)
                                                                    Return c.ProcureGiftCard(GiftRequest)
                                                                End Function)

                Me.State.MyBO.GiftCardRequestId=GiftCardResponse.GiftCardId

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
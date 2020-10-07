Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Claims

    Partial Class ReplacementForm
        Inherits ElitaPlusPage

        Public Const PAGETAB As String = "CLAIM"
        Public Const PAGESUBTAB As String = "Replacement"
        Public Const PAGETITLE As String = "Replacement"

#Region "Page State"

#Region "Parameters"

        Public Class Parameters
            Public moClaimId As Guid
            Public mbIsClaimFormCalling As Boolean

            Public Sub New(bIsClaimFormCalling As Boolean, oClaimId As Guid)
                moClaimId = oClaimId
                mbIsClaimFormCalling = bIsClaimFormCalling
            End Sub

        End Class

#End Region
#Region "MyState"

        Class MyState
            Public Claim As ClaimBase
            Public ReadOnly Property ClaimBO As ClaimBase
                Get
                    If (Claim Is Nothing) Then
                        If (moParams.moClaimId <> Guid.Empty) Then
                            Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(moParams.moClaimId)
                        End If
                    End If
                    Return Claim
                End Get
            End Property
            Public moParams As Parameters
            Public IsReplacementNew As Boolean = False
            Public IsDirty As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public supportsImei As Boolean = False
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            Try
                State.moParams = CType(CallingParameters, Parameters)
                '    TestClaim()
                If (State.moParams Is Nothing) OrElse (State.moParams.moClaimId.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If
                Dim replacedEquipment As ClaimEquipment = State.ClaimBO.ReplacementEquipment ' Me.State.ClaimBO.ClaimEquipmentChildren.Where(Function(ce) ce.ClaimEquipmentType = Codes.CLAIM_EQUIP_TYPE__REPLACEMENT).FirstOrDefault()
                SetDisplayMode(True)
                If replacedEquipment Is Nothing Then
                    ' New
                    State.IsReplacementNew = True
                    ClearAll()
                    SetButtonsState(True)
                Else
                    ' Existing one
                    State.IsReplacementNew = False
                    SetButtonsState(False)

                End If
                PopulateFormFromBo()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Constants"

        Public Const URL As String = "ReplacementForm.aspx"

#End Region

#Region "Variables"

        Private moReplacement As ClaimEquipment

#End Region

#Region "Properties"

        Private ReadOnly Property ReplacedEquipment() As ClaimEquipment
            Get
                If moReplacement Is Nothing Then
                    If State.IsReplacementNew = True Then
                        ' For creating, inserting
                        moReplacement = New ClaimEquipment()
                        moReplacement.ClaimId = State.ClaimBO.Id
                        moReplacement.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT)
                    Else
                        ' For updating, deleting
                        moReplacement = New ClaimEquipment(State.ClaimBO.ReplacementEquipment.Id)
                    End If
                End If

                Return moReplacement
            End Get
        End Property

#End Region

#Region "Handlers"


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

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear()
            Try
                ClearLabelsErrSign()

                If Not Page.IsPostBack Then
                    SetStateProperties()
                    UpdateBreadCrum()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO,
                                                                        ElitaPlusPage.MSG_TYPE_CONFIRM, True)
                Else
                    CheckIfComingFromConfirm()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub


#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Try
                If State.moParams.mbIsClaimFormCalling = True Then
                    ' Claim Detail
                    Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
                    ReturnToCallingPage(retType)
                Else
                    ' Pay Claim
                    ReturnToCallingPage()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If State.IsDirty = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM,
                                            HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveReplacementChanges()
            Try
                If ApplyReplacementChanges() = True Then
                    SetDisplayMode(True)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SaveReplacementChanges()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                SetDisplayMode(True)
                ClearAll()
                PopulateFormFromBo()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteReplacement() = True Then
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnEdit_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnEdit_WRITE.Click
            Try
                SetDisplayMode(False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"

        Protected Sub BindBoPropertiesToLabels(pClaimEquipment As ClaimEquipment, pClaim As ClaimBase)
            Try
                ' TODO: Property Binding
                BindBOPropertyToLabel(pClaimEquipment, "ManufacturerId", NewManufacturerLabel)
                BindBOPropertyToLabel(pClaimEquipment, "Model", NewModelLabel)
                BindBOPropertyToLabel(pClaimEquipment, "SerialNumber", NewSerialNumberLabel)
                BindBOPropertyToLabel(pClaimEquipment, "IMEINumber", NewImeiNumberLabel)
                BindBOPropertyToLabel(pClaimEquipment, "DeviceTypeId", DeviceTypeLabel)


                BindBOPropertyToLabel(pClaim.CertificateItem, "ManufacturerId", OldManufacturerLabel)
                BindBOPropertyToLabel(pClaim.CertificateItem, "Model", OldModelLabel)
                BindBOPropertyToLabel(pClaim.CertificateItem, "IMEINumber", OldImeiNumberLabel)
                BindBOPropertyToLabel(pClaim.CertificateItem, "SerialNumber", OldSerialNumberLabel)

                Dim dummyCertItem As New CertItem()
                BindBOPropertyToLabel(dummyCertItem, "RiskTypeId", NewRiskTypeLabel)
                BindBOPropertyToLabel(pClaim.CertificateItem, "RiskTypeId", OldRiskTypeLabel)

                AddLabelDecorations(pClaimEquipment)
                AddLabelDecorations(pClaim.CertificateItem)
                AddLabelDecorations(dummyCertItem)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(NewManufacturerLabel)
                ClearLabelErrSign(NewModelLabel)
                ClearLabelErrSign(NewSerialNumberLabel)
                ClearLabelErrSign(NewImeiNumberLabel)
                ClearLabelErrSign(DeviceTypeLabel)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Button-Management"

        Private Sub SetButtonsState(bIsNew As Boolean)
            Try
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetDisplayMode(pIsReadOnly As Boolean)
            Try
                NewManufacturerDropDown.Enabled = Not pIsReadOnly
                DeviceTypeDropDown.Enabled = Not pIsReadOnly
                EnableDisableControls(NewModelTextBox, pIsReadOnly)
                EnableDisableControls(NewSerialNumberTextBox, pIsReadOnly)
                EnableDisableControls(NewImeiNumberTextBox, pIsReadOnly)
                EnableDisableControls(NewRiskTypeTextBox, True)
                EnableDisableControls(CommentsTextBox, pIsReadOnly)
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, Not pIsReadOnly)
                ControlMgr.SetVisibleControl(Me, btnUndo_WRITE, Not pIsReadOnly)
                ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, Not pIsReadOnly)
                ControlMgr.SetVisibleControl(Me, btnBack, pIsReadOnly)
                ControlMgr.SetVisibleControl(Me, BtnEdit_WRITE, pIsReadOnly)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateFormFromBo()
            Try


                moClaimInfoController.InitController(State.ClaimBO)

                With State.ClaimBO
                    OldManufacturerTextBox.Text = Manufacturer.GetDescription(.CertificateItem.ManufacturerId)
                    EnableDisableControls(OldManufacturerTextBox, True)
                    OldModelTextBox.Text = .CertificateItem.Model
                    EnableDisableControls(OldModelTextBox, True)
                    OldRiskTypeTextBox.Text = New RiskType(.CertificateItem.RiskTypeId).Description
                    NewRiskTypeTextBox.Text = OldRiskTypeTextBox.Text
                    EnableDisableControls(OldRiskTypeTextBox, True)
                    OldSerialNumberTextBox.Text = .CertificateItem.SerialNumber
                    EnableDisableControls(OldSerialNumberTextBox, True)
                    State.supportsImei = (Not State.ClaimBO.Dealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE"))
                    If (State.supportsImei) Then
                        OldImeiNumberTextBox.Text = .CertificateItem.IMEINumber
                        EnableDisableControls(OldImeiNumberTextBox, True)

                        OldSerialNumberLabel.Text = TranslationBase.TranslateLabelOrMessage("SERIAL_NO_LABEL")
                        NewSerialNumberLabel.Text = OldSerialNumberLabel.Text
                    Else
                        OldSerialNumberLabel.Text = TranslationBase.TranslateLabelOrMessage("SERIAL_NUMBER")
                        NewSerialNumberLabel.Text = OldSerialNumberLabel.Text
                    End If

                    OldImeiNumberLabel.Visible = State.supportsImei
                    OldImeiNumberTextBox.Visible = State.supportsImei

                    NewImeiNumberLabel.Visible = State.supportsImei
                    NewImeiNumberTextBox.Visible = State.supportsImei
                End With

                ' Populate New Item
                ' Populate Manufacturer

                Dim listcontextForMgList As ListContext = New ListContext()
                listcontextForMgList.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim manufacturerList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForMgList)

                NewManufacturerDropDown.Populate(manufacturerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = False
                                                  })
                If State.IsReplacementNew = True Then
                    BindSelectItem(State.ClaimBO.CertificateItem.ManufacturerId.ToString, NewManufacturerDropDown)
                Else
                    BindSelectItem(ReplacedEquipment.ManufacturerId.ToString, NewManufacturerDropDown)
                End If

                ' Populate Device Type

                Dim deviceTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("DEVICE", Thread.CurrentPrincipal.GetLanguageCode())
                DeviceTypeDropDown.Populate(deviceTypeList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })
                If State.IsReplacementNew = False Then
                    BindSelectItem(ReplacedEquipment.DeviceTypeId.ToString, DeviceTypeDropDown)
                End If


                With ReplacedEquipment
                    PopulateControlFromBOProperty(NewModelTextBox, .Model)
                    PopulateControlFromBOProperty(NewSerialNumberTextBox, .SerialNumber)
                    PopulateControlFromBOProperty(NewImeiNumberTextBox, .IMEINumber)
                    PopulateControlFromBOProperty(CommentsTextBox, .Comments)
                End With

                BindBoPropertiesToLabels(ReplacedEquipment, State.ClaimBO)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Clear"

        Private Sub ClearAll()
            NewManufacturerDropDown.SelectedIndex = 0
            NewModelTextBox.Text = String.Empty
            NewSerialNumberTextBox.Text = String.Empty
            NewImeiNumberTextBox.Text = String.Empty
            CommentsTextBox.Text = String.Empty
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateBOFromForm(pReplacedEquipment As ClaimEquipment)
            Try
                With pReplacedEquipment
                    .ClaimId = State.moParams.moClaimId
                    ' DropDowns
                    .ManufacturerId = ElitaPlusPage.GetSelectedItem(NewManufacturerDropDown)
                    .DeviceTypeId = ElitaPlusPage.GetSelectedItem(DeviceTypeDropDown)
                    ' Texts
                    PopulateBOProperty(pReplacedEquipment, "Model", NewModelTextBox.Text)
                    PopulateBOProperty(pReplacedEquipment, "SerialNumber", NewSerialNumberTextBox.Text)
                    PopulateBOProperty(pReplacedEquipment, "IMEINumber", NewImeiNumberTextBox.Text)
                    PopulateBOProperty(pReplacedEquipment, "Comments", CommentsTextBox.Text)
                End With
                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub populateCertItemBOFromForm(oCertItem As CertItem)
            oCertItem.ManufacturerId = ElitaPlusPage.GetSelectedItem(NewManufacturerDropDown)
            PopulateBOProperty(oCertItem, "SerialNumber", NewSerialNumberTextBox.Text)
            PopulateBOProperty(oCertItem, "IMEINumber", NewImeiNumberTextBox.Text)
            PopulateBOProperty(oCertItem, "Model", NewModelTextBox.Text)
        End Sub

        Private Function IsDirtyReplacementBO() As Boolean
            Dim bIsDirty As Boolean = False
            Dim replacedEquipment As ClaimEquipment

            replacedEquipment = Me.ReplacedEquipment
            With replacedEquipment
                PopulateBOFromForm(replacedEquipment)
                bIsDirty = .IsDirty
                State.IsDirty = bIsDirty
            End With

            Return bIsDirty
        End Function

        Private Function ApplyReplacementChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim replacedEquipment As ClaimEquipment, oCertItem As CertItem

            Try
                If IsDirtyReplacementBO() = True Then
                    replacedEquipment = Me.ReplacedEquipment
                    oCertItem = New CertItem(State.ClaimBO.CertificateItem.Id)
                    populateCertItemBOFromForm(oCertItem)
                    BindBoPropertiesToLabels(replacedEquipment, State.ClaimBO)
                    HandleChildObj(oCertItem)
                    oCertItem.Save()
                    replacedEquipment.Save()
                    State.IsDirty = False
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                        MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
                State.IsReplacementNew = False
                SetButtonsState(False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteReplacement() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With ReplacedEquipment
                    .Delete()
                    .Save()
                    '  .Claim.Save()
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Sub HandleChildObj(oCertItem As CertItem)
            If (State.supportsImei AndAlso Not (OldImeiNumberTextBox.Text.Equals(NewImeiNumberTextBox.Text))) OrElse (Not State.supportsImei AndAlso Not (OldSerialNumberTextBox.Text.Equals(NewSerialNumberTextBox.Text))) Then
                Dim blnCreateExtendedStatus As Boolean = False
                Dim blnAnyStatusFound As Boolean = False
                'check if action status "Replacement IMEI Changed" is configured
                Dim dv As DataView = Assurant.ElitaPlus.BusinessObjectsNew.ClaimStatusAction.LoadList()
                If dv.Count > 0 Then
                    Dim dvAction As DataView = LookupListNew.GetClaimStatsActionLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    Dim actionCode As String
                    Dim action_current_status_id As Guid
                    Dim action_next_status_id As Guid
                    Dim dr As DataRow
                    For Each dr In dv.Table.Rows
                        actionCode = LookupListNew.GetCodeFromId(dvAction, New Guid(CType(dr("AcTION_ID"), Byte())))
                        If actionCode.Equals("REP_IMEI_CHANGE") Then
                            ' Dim dvExtendedStatus As New DataView(ClaimStatusByGroup.LoadListByCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
                            action_current_status_id = New Guid(CType(dr("CURRENT_STATUS_ID"), Byte()))
                            action_next_status_id = New Guid(CType(dr("NEXT_STATUS_ID"), Byte()))
                            'check if action current status is "ANY_STATUS"
                            Dim listcontext As ListContext = New ListContext()
                            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                            Dim claimStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                            For Each li As ListItem In claimStatusList
                                If li.ListItemId.Equals(action_current_status_id) AndAlso li.Code.ToUpper.Equals("ANY_STATUS") Then
                                    blnAnyStatusFound = True
                                    Exit For
                                End If
                            Next

                            If blnAnyStatusFound Then
                                blnCreateExtendedStatus = True
                            Else
                                Dim claim_leatest_status As Guid = State.ClaimBO.LatestClaimStatus.ClaimStatusByGroupId
                                If action_current_status_id.Equals(claim_leatest_status) Then
                                    blnCreateExtendedStatus = True
                                End If
                            End If
                            Exit For
                        End If
                    Next
                    If blnCreateExtendedStatus Then
                        Dim newclaimStatus As ClaimStatus = oCertItem.AddClaimExtendedStatus(Guid.Empty)
                        newclaimStatus.ClaimId = State.ClaimBO.Id
                        newclaimStatus.ClaimStatusByGroupId = action_next_status_id
                        newclaimStatus.StatusDate = Date.Now

                    End If
                End If

            End If

        End Sub

#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Try
                If Not confResponse = String.Empty Then
                    ' Return from the Back Button

                    Select Case confResponse
                        Case ElitaPlusPage.MSG_VALUE_YES
                            ' Save and go back to Search Page
                            If ApplyReplacementChanges() = True Then
                                GoBack()
                            End If
                        Case ElitaPlusPage.MSG_VALUE_NO
                            ' Go back to Search Page
                            GoBack()
                    End Select
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class

End Namespace

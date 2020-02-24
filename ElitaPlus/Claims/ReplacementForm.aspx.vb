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

            Public Sub New(ByVal bIsClaimFormCalling As Boolean, ByVal oClaimId As Guid)
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
                    If (Me.Claim Is Nothing) Then
                        If (Me.moParams.moClaimId <> Guid.Empty) Then
                            Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.moParams.moClaimId)
                        End If
                    End If
                    Return Me.Claim
                End Get
            End Property
            Public moParams As Parameters
            Public IsReplacementNew As Boolean = False
            Public IsDirty As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
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
                Me.State.moParams = CType(Me.CallingParameters, Parameters)
                '    TestClaim()
                If (Me.State.moParams Is Nothing) OrElse (Me.State.moParams.moClaimId.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If
                Dim replacedEquipment As ClaimEquipment = Me.State.ClaimBO.ReplacementEquipment ' Me.State.ClaimBO.ClaimEquipmentChildren.Where(Function(ce) ce.ClaimEquipmentType = Codes.CLAIM_EQUIP_TYPE__REPLACEMENT).FirstOrDefault()
                SetDisplayMode(True)
                If replacedEquipment Is Nothing Then
                    ' New
                    Me.State.IsReplacementNew = True
                    ClearAll()
                    SetButtonsState(True)
                Else
                    ' Existing one
                    Me.State.IsReplacementNew = False
                    SetButtonsState(False)

                End If
                PopulateFormFromBo()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                    If Me.State.IsReplacementNew = True Then
                        ' For creating, inserting
                        moReplacement = New ClaimEquipment()
                        moReplacement.ClaimId = Me.State.ClaimBO.Id
                        moReplacement.ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT)
                    Else
                        ' For updating, deleting
                        moReplacement = New ClaimEquipment(Me.State.ClaimBO.ReplacementEquipment.Id)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear()
            Try
                ClearLabelsErrSign()

                If Not Page.IsPostBack Then
                    Me.SetStateProperties()
                    UpdateBreadCrum()
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO,
                                                                        ElitaPlusPage.MSG_TYPE_CONFIRM, True)
                Else
                    CheckIfComingFromConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub


#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Try
                If Me.State.moParams.mbIsClaimFormCalling = True Then
                    ' Claim Detail
                    Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
                    Me.ReturnToCallingPage(retType)
                Else
                    ' Pay Claim
                    Me.ReturnToCallingPage()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If Me.State.IsDirty = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM,
                                            Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveReplacementChanges()
            Try
                If ApplyReplacementChanges() = True Then
                    SetDisplayMode(True)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SaveReplacementChanges()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                SetDisplayMode(True)
                ClearAll()
                PopulateFormFromBo()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteReplacement() = True Then
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnEdit_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit_WRITE.Click
            Try
                SetDisplayMode(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"

        Protected Sub BindBoPropertiesToLabels(ByVal pClaimEquipment As ClaimEquipment, ByVal pClaim As ClaimBase)
            Try
                ' TODO: Property Binding
                Me.BindBOPropertyToLabel(pClaimEquipment, "ManufacturerId", NewManufacturerLabel)
                Me.BindBOPropertyToLabel(pClaimEquipment, "Model", NewModelLabel)
                Me.BindBOPropertyToLabel(pClaimEquipment, "SerialNumber", NewSerialNumberLabel)
                Me.BindBOPropertyToLabel(pClaimEquipment, "IMEINumber", NewImeiNumberLabel)
                Me.BindBOPropertyToLabel(pClaimEquipment, "DeviceTypeId", DeviceTypeLabel)


                Me.BindBOPropertyToLabel(pClaim.CertificateItem, "ManufacturerId", OldManufacturerLabel)
                Me.BindBOPropertyToLabel(pClaim.CertificateItem, "Model", OldModelLabel)
                Me.BindBOPropertyToLabel(pClaim.CertificateItem, "IMEINumber", OldImeiNumberLabel)
                Me.BindBOPropertyToLabel(pClaim.CertificateItem, "SerialNumber", OldSerialNumberLabel)

                Dim dummyCertItem As New CertItem()
                Me.BindBOPropertyToLabel(dummyCertItem, "RiskTypeId", NewRiskTypeLabel)
                Me.BindBOPropertyToLabel(pClaim.CertificateItem, "RiskTypeId", OldRiskTypeLabel)

                AddLabelDecorations(pClaimEquipment)
                AddLabelDecorations(pClaim.CertificateItem)
                AddLabelDecorations(dummyCertItem)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(NewManufacturerLabel)
                Me.ClearLabelErrSign(NewModelLabel)
                Me.ClearLabelErrSign(NewSerialNumberLabel)
                Me.ClearLabelErrSign(NewImeiNumberLabel)
                Me.ClearLabelErrSign(DeviceTypeLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Button-Management"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            Try
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetDisplayMode(ByVal pIsReadOnly As Boolean)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateFormFromBo()
            Try
                Dim supportsImei As Boolean = False

                moClaimInfoController.InitController(Me.State.ClaimBO)

                With Me.State.ClaimBO
                    OldManufacturerTextBox.Text = Manufacturer.GetDescription(.CertificateItem.ManufacturerId)
                    EnableDisableControls(OldManufacturerTextBox, True)
                    OldModelTextBox.Text = .CertificateItem.Model
                    EnableDisableControls(OldModelTextBox, True)
                    OldRiskTypeTextBox.Text = New RiskType(.CertificateItem.RiskTypeId).Description
                    NewRiskTypeTextBox.Text = OldRiskTypeTextBox.Text
                    EnableDisableControls(OldRiskTypeTextBox, True)
                    OldSerialNumberTextBox.Text = .CertificateItem.SerialNumber
                    EnableDisableControls(OldSerialNumberTextBox, True)
                    supportsImei = (Not Me.State.ClaimBO.Dealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE"))
                    If (supportsImei) Then
                        OldImeiNumberTextBox.Text = .CertificateItem.IMEINumber
                        EnableDisableControls(OldImeiNumberTextBox, True)

                        OldSerialNumberLabel.Text = TranslationBase.TranslateLabelOrMessage("SERIAL_NO_LABEL")
                        NewSerialNumberLabel.Text = OldSerialNumberLabel.Text
                    Else
                        OldSerialNumberLabel.Text = TranslationBase.TranslateLabelOrMessage("SERIAL_NUMBER")
                        NewSerialNumberLabel.Text = OldSerialNumberLabel.Text
                    End If

                    OldImeiNumberLabel.Visible = supportsImei
                    OldImeiNumberTextBox.Visible = supportsImei

                    NewImeiNumberLabel.Visible = supportsImei
                    NewImeiNumberTextBox.Visible = supportsImei
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
                If Me.State.IsReplacementNew = True Then
                    BindSelectItem(Me.State.ClaimBO.CertificateItem.ManufacturerId.ToString, NewManufacturerDropDown)
                Else
                    BindSelectItem(ReplacedEquipment.ManufacturerId.ToString, NewManufacturerDropDown)
                End If

                ' Populate Device Type

                Dim deviceTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("DEVICE", Thread.CurrentPrincipal.GetLanguageCode())
                DeviceTypeDropDown.Populate(deviceTypeList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })
                If Me.State.IsReplacementNew = False Then
                    BindSelectItem(ReplacedEquipment.DeviceTypeId.ToString, DeviceTypeDropDown)
                End If


                With ReplacedEquipment
                    Me.PopulateControlFromBOProperty(NewModelTextBox, .Model)
                    Me.PopulateControlFromBOProperty(NewSerialNumberTextBox, .SerialNumber)
                    Me.PopulateControlFromBOProperty(NewImeiNumberTextBox, .IMEINumber)
                    Me.PopulateControlFromBOProperty(CommentsTextBox, .Comments)
                End With

                BindBoPropertiesToLabels(ReplacedEquipment, Me.State.ClaimBO)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

        Private Sub PopulateBOFromForm(ByVal pReplacedEquipment As ClaimEquipment)
            Try
                With pReplacedEquipment
                    .ClaimId = Me.State.moParams.moClaimId
                    ' DropDowns
                    .ManufacturerId = ElitaPlusPage.GetSelectedItem(NewManufacturerDropDown)
                    .DeviceTypeId = ElitaPlusPage.GetSelectedItem(DeviceTypeDropDown)
                    ' Texts
                    Me.PopulateBOProperty(pReplacedEquipment, "Model", NewModelTextBox.Text)
                    Me.PopulateBOProperty(pReplacedEquipment, "SerialNumber", NewSerialNumberTextBox.Text)
                    Me.PopulateBOProperty(pReplacedEquipment, "IMEINumber", NewImeiNumberTextBox.Text)
                    Me.PopulateBOProperty(pReplacedEquipment, "Comments", CommentsTextBox.Text)
                End With
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub populateCertItemBOFromForm(oCertItem As CertItem)
            oCertItem.ManufacturerId = ElitaPlusPage.GetSelectedItem(NewManufacturerDropDown)
            Me.PopulateBOProperty(oCertItem, "SerialNumber", NewSerialNumberTextBox.Text)
            Me.PopulateBOProperty(oCertItem, "IMEINumber", NewImeiNumberTextBox.Text)
            Me.PopulateBOProperty(oCertItem, "Model", NewModelTextBox.Text)
        End Sub

        Private Function IsDirtyReplacementBO() As Boolean
            Dim bIsDirty As Boolean = False
            Dim replacedEquipment As ClaimEquipment

            replacedEquipment = Me.ReplacedEquipment
            With replacedEquipment
                PopulateBOFromForm(replacedEquipment)
                bIsDirty = .IsDirty
                Me.State.IsDirty = bIsDirty
            End With

            Return bIsDirty
        End Function

        Private Function ApplyReplacementChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim replacedEquipment As ClaimEquipment, oCertItem As CertItem

            Try
                If IsDirtyReplacementBO() = True Then
                    replacedEquipment = Me.ReplacedEquipment
                    oCertItem = New CertItem(Me.State.ClaimBO.CertificateItem.Id)
                    populateCertItemBOFromForm(oCertItem)
                    BindBoPropertiesToLabels(replacedEquipment, Me.State.ClaimBO)
                    oCertItem.Save()
                    replacedEquipment.Save()
                    Me.State.IsDirty = False
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
                Me.State.IsReplacementNew = False
                SetButtonsState(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                End Select

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class

End Namespace

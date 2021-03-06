﻿Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports System.Text
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables

    Partial Public Class CommPCodeForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"

        Class MyState
            Public MyBo As CommPCode
            Public moCommPCodeEntity As CommPCodeEntity
            Public moCommPCodeId As Guid = Guid.Empty
            Public moDealerId As Guid = Guid.Empty
            Public moCommPCodeEntityId As Guid = Guid.Empty
            Public LastErrMsg As String
            Public IsPCodeNew As Boolean = False
            Public IsPCodeEntityNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public searchDV As DataView = Nothing
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public sBuffer As StringBuilder
            Public totalCF, totalMF As Double
            Public totalCFId, totalMFId As String
            Public isInstallmentPayment As Boolean
            Public oContract As Contract            

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
            Dim oProductCode As ProductCode

            Me.State.moCommPCodeId = CType(Me.CallingParameters, Guid)
            If Me.State.moCommPCodeId.Equals(Guid.Empty) Then
                Me.State.IsPCodeNew = True
                ClearPCode()
                SetPCodeButtonsState(True)
                PopulatePCode()
                TheDealerControl.ChangeEnabledControlProperty(True)
                Me.ChangeEnabledControlProperty(Me.moProductCodeDrop, True)
            Else
                Me.State.IsPCodeNew = False
                SetPCodeButtonsState(False)
                PopulatePCode()
                TheDealerControl.ChangeEnabledControlProperty(False)
                Me.ChangeEnabledControlProperty(Me.moProductCodeDrop, False)
            End If
        End Sub

#End Region


#Region "Constants"

        Public Const URL As String = "CommPCodeForm.aspx"

        ' Property Name
        Public Const NOTHING_SELECTED As Integer = 0
        Public Const COMM_P_CODE_ID_PROPERTY As String = "CommPCodeId"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const PRODUCT_CODE_ID_PROPERTY As String = "ProductCodeId"
        Public Const EFFECTIVE_DATE_PROPERTY As String = "EffectiveDate"
        Public Const EXPIRATION_DATE_PROPERTY As String = "ExpirationDate"
        Public Const PAGETITLE As String = "COMMISSION_BY_PRODUCT_CODE"
        Public Const PAGETAB As String = "TABLES"
        
        Private Const LABEL_DEALER As String = "DEALER"

#Region "Constants-Entity"

        Private Const ENTITY_ID_COL As Integer = 1
        Private Const ENTITY_PAYEE_TYPE_COL As Integer = 2
        Private Const ENTITY_ENTITY_COL As Integer = 3
        Private Const ENTITY_IS_COMM_FIXED_COL As Integer = 4
        Private Const ENTITY_COMMISSION_SCHEDULE_COL As Integer = 5
        Private Const ENTITY_COMMISSION_AMOUNT_COL As Integer = 6
        Private Const ENTITY_MARKUP_AMOUNT_COL As Integer = 7
        'REQ-976
        Private Const ENTITY_DAYS_TO_CLAWBACK_COL As Integer = 8
        Private Const ENTITY_BRANCH_ID As Integer = 9
        'End REQ-976
        
        ' Property Name
        Public Const PAYEE_TYPE_ID_PROPERTY As String = "PayeeTypeId"
        Public Const PAYEE_TYPE_PROPERTY As String = "PayeeType"
        Public Const COMM_SCHEDULE_ID_PROPERTY As String = "CommScheduleId"
        Public Const COMM_SCHEDULE_PROPERTY As String = "CommSchedule"
        Public Const ENTITY_ID_PROPERTY As String = "EntityId"
        Public Const ENTITY_PROPERTY As String = "Entity"
        Public Const IS_COMM_FIXED_ID_PROPERTY As String = "IsCommFixedId"
        Public Const IS_COMM_FIXED_CODE_PROPERTY As String = "IsCommFixedCode"
        Public Const IS_COMM_FIXED_PROPERTY As String = "IsCommFixed"
        Public Const COMMISSION_AMOUNT_PROPERTY As String = "CommissionAmount"
        Public Const IS_MARKUP_FIXED_ID_PROPERTY As String = "IsMarkupFixedId"
        Public Const IS_MARKUP_FIXED_CODE_PROPERTY As String = "IsMarkupFixedCode"
        Public Const IS_MARKUP_FIXED_PROPERTY As String = "IsMarkupFixed"
        Public Const MARKUP_AMOUNT_PROPERTY As String = "MarkupAmount"

        'REQ-976
        Public Const DAYS_TO_CLAWBACK_PROPERTY As String = "DaysToClawback"
        Public Const BRANCH_ID_PROPERTY As String = "BranchId"
        Public Const BRANCH_PROPERTY As String = "BranchName"
        'End REQ-976
       
#End Region

#End Region

#Region "Variables"

       Private moExpirationData As CommPCodeData

#End Region

#Region "Properties"

#Region "P Code-Properties"

        Private ReadOnly Property ThePCode() As CommPCode
            Get
                If Me.State.MyBo Is Nothing Then
                    If Me.State.IsPCodeNew = True Then
                        ' For creating, inserting
                        Me.State.MyBo = New CommPCode
                        Me.State.moCommPCodeId = Me.State.MyBo.Id
                    Else
                        ' For updating, deleting
                        Me.State.MyBo = New CommPCode(Me.State.moCommPCodeId)
                    End If
                End If
                BindBoPropertiesToLabels(Me.State.MyBo)
                Return Me.State.MyBo
            End Get
        End Property

        Private ReadOnly Property ExpirationCount() As Integer
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CommPCodeData
                    moExpirationData.productCodeId = Me.GetSelectedItem(moProductCodeDrop)
                End If
                Return ThePCode.ExpirationCount(moExpirationData)
            End Get
        End Property

        Private ReadOnly Property MaxExpiration() As Date
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CommPCodeData
                    moExpirationData.productCodeId = Me.GetSelectedItem(moProductCodeDrop)
                End If
                Return ThePCode.MaxExpiration(moExpirationData)
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Private Property TheDealerId() As Guid
            Get
                Dim oProductCode As ProductCode

                If Me.State.moDealerId.Equals(Guid.Empty) Then
                    oProductCode = New ProductCode(ThePCode.ProductCodeId)
                    Me.State.moDealerId = oProductCode.DealerId
                End If

                Return Me.State.moDealerId
            End Get
            Set(ByVal value As Guid)
                Me.State.moDealerId = value
            End Set
        End Property

#End Region

#Region "Properties-Entity"

        Private ReadOnly Property TheCommPCodeEntity() As CommPCodeEntity
            Get

                '  If Me.State.moCommPCodeEntity Is Nothing Then
                If Me.State.IsPCodeEntityNew = True Then
                    ' For creating, inserting
                    Me.State.moCommPCodeEntity = New CommPCodeEntity
                    Me.State.moCommPCodeEntityId = Me.State.moCommPCodeEntity.Id
                Else
                    ' For updating, deleting
                    Me.State.moCommPCodeEntity = New CommPCodeEntity(Me.State.moCommPCodeEntityId)
                End If
                '  End If

                Return Me.State.moCommPCodeEntity
            End Get

        End Property

#End Region

#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.ErrControllerMaster.Clear_Hide()
                Me.moErrorControllerEntity.Clear_Hide()
                ClearLabelsErrSign()
                ClearGridHeaders(moEntityGrid)
                If Not Page.IsPostBack Then
                    '     ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.TranslateGridHeader(moEntityGrid)
                    Me.TranslateGridControls(moEntityGrid)
                    Me.SetGridItemStyleColor(moEntityGrid)
                    Me.SetStateProperties()

                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
                                                                        Me.MSG_TYPE_CONFIRM, True)
                    Me.AddCalendar(Me.BtnEffectiveDate_WRITE, Me.moEffectiveText_WRITE)
                    Me.AddCalendar(Me.BtnExpirationDate_WRITE, Me.moExpirationText_WRITE)
                    FromGridToBuffer()
                Else
                    CheckIfComingFromConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New CommissionPeriodSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                                                Me.State.moCommPCodeId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                If IsDirtyPCodeBO() = True Then
                    Me.DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, _
                                            Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                GoBack()
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            Finally
                FromGridToBuffer()
            End Try
        End Sub

        Private Sub SavePCodeChanges()
            If ApplyPCodeChanges() = True Then
                Me.State.boChanged = True
                PopulatePCode()
                SetPCodeButtonsState(False)
            End If
        End Sub

        Protected Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePCodeChanges()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            Finally
                FromGridToBuffer()
            End Try
        End Sub

        Protected Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                ClearPCode()
                PopulatePCode()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            Finally
                FromGridToBuffer()
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.MyBo = Nothing
            Me.State.moCommPCodeId = Guid.Empty
            Me.State.IsPCodeNew = True
            ClearPCode()
            SetPCodeButtonsState(True)
            PopulatePCode()
            TheDealerControl.ChangeEnabledControlProperty(True)
            Me.ChangeEnabledControlProperty(Me.moProductCodeDrop, True)
        End Sub

        Protected Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyPCodeBO() = True Then
                    Me.DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
               Me.HandleErrors(ex, Me.ErrControllerMaster)
            Finally
                FromGridToBuffer()
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Dim newObj As New CommPCode
            Me.State.MyBo = Nothing
            ClearEntity()
            Me.State.searchDV = newObj.Copy(ThePCode)
            Me.State.IsPCodeNew = True
            Me.State.MyBo = newObj
            Me.SetGridControls(moEntityGrid, False)
            EnableDateFields()
            SetPCodeButtonsState(True)
            SetEntityButtonsState(False)
            PopulatePCodeEntity(Me.POPULATE_ACTION_NO_EDIT, False)
        End Sub

        Protected Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyPCodeBO() = True Then
                    Me.DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
               Me.HandleErrors(ex, Me.ErrControllerMaster)
            Finally
                FromGridToBuffer()
            End Try
        End Sub

        Protected Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeletePCode() = True Then
                    Me.State.boChanged = True
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDowns"

        ' Multiple Dealer Drop 
        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
           Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                EnableDateFields()
                If Me.State.IsPCodeNew = True Then
                   TheDealerId = TheDealerControl.SelectedGuid
                    PopulateProductCode()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moProductCodeDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moProductCodeDrop.SelectedIndexChanged
            If moProductCodeDrop.SelectedIndex > 0 Then                
                ' PopulateUpfrontComm()
            End If
        End Sub


#End Region

#Region "Handlers-Labels"

        Protected Sub BindBoPropertiesToLabels(ByVal oPCode As CommPCode)
            Me.BindBOPropertyToLabel(oPCode, PRODUCT_CODE_ID_PROPERTY, moProductCodeLabel)
            Me.BindBOPropertyToLabel(oPCode, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPCode, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
        End Sub

        Public Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(moEffectiveLabel)
            Me.ClearLabelErrSign(moExpirationLabel)
        End Sub
#End Region

#End Region

#Region "Button-Management"

        Private Sub SetPCodeButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        End Sub

        Private Sub EnableEffective(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moEffectiveText_WRITE, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate_WRITE, bIsEnable)
        End Sub

        Private Sub EnableExpiration(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moExpirationText_WRITE, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate_WRITE, bIsEnable)
        End Sub

        Private Sub EnableDateFields()
            Select Case ExpirationCount
                Case 0  ' New Record
                    EnableEffective(True)
                    EnableExpiration(True)
                    ' Today
                    moEffectiveText_WRITE.Text = Date.Now().ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)

                    ' Next Year
                    moExpirationText_WRITE.Text = Date.Now().AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                Case 1
                    If Me.State.IsPCodeNew = True Then
                        'New Record
                        ' Next Day MaxExpiration
                        moEffectiveText_WRITE.Text = MaxExpiration.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        ' Next Year after MaxExpiration 
                        moExpirationText_WRITE.Text = MaxExpiration.AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        EnableEffective(False)
                    Else
                        ' Modify the only record
                        EnableEffective(True)
                    End If
                    ControlMgr.SetEnableControl(Me, BtnExpirationDate_WRITE, True)
                Case Else   ' There is more than one record
                    EnableExpiration(True)
                    If Me.State.IsPCodeNew = True Then
                        'New Record
                        ' Next Day MaxExpiration
                        moEffectiveText_WRITE.Text = MaxExpiration.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        ' Next Year after MaxExpiration 
                        moExpirationText_WRITE.Text = MaxExpiration.AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                    Else
                        Dim oMaxExpiration As String = GetDateFormattedString(MaxExpiration)
                        If moExpirationText_WRITE.Text <> oMaxExpiration Then
                            ' It is not the last Record
                            EnableExpiration(False)
                            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                        End If
                    End If
                    EnableEffective(False)
            End Select
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealer()

            Dim oDataView As DataView = LookupListNew.GetDealersCommPrdLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.Caption = "* " + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
            TheDealerControl.NothingSelected = True
            TheDealerControl.BindData(oDataView)
            TheDealerControl.AutoPostBackDD = True

            If Me.State.IsPCodeNew = True Then
                TheDealerControl.NothingSelected = True
            Else
                TheDealerControl.SelectedGuid = TheDealerId
            End If

        End Sub

        Private Sub PopulateProductCode()
            If ((TheDealerControl.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX) OrElse
                (TheDealerControl.SelectedIndex = Me.BLANK_ITEM_SELECTED)) Then Return

            Dim listcontextForProductCodeList As ListContext = New ListContext()
            listcontextForProductCodeList.DealerId = TheDealerId
            Dim productCodeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ProductCodeByDealer", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForProductCodeList)

            Me.moProductCodeDrop.Populate(productCodeList, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .TextFunc = AddressOf .GetCode,
                       .SortFunc = AddressOf .GetCode
                    })
            'Me.BindListControlToDataView(moProductCodeDrop, LookupListNew.GetProductCodeLookupList(TheDealerId), "CODE")

            If Me.State.IsPCodeNew = True Then
                moProductCodeDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
            Else
                Me.SetSelectedItem(moProductCodeDrop, ThePCode.ProductCodeId)
            End If
        End Sub

        Private Sub PopulateDates()
            Me.PopulateControlFromBOProperty(moEffectiveText_WRITE, ThePCode.EffectiveDate)
            Me.PopulateControlFromBOProperty(moExpirationText_WRITE, ThePCode.ExpirationDate)
        End Sub
        Private Sub PopulateUpfrontComm()
            'Dim oProdcode As ProductCode
            'Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'BindListControlToDataView(moupfrontcommDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)

            'If Me.State.IsPCodeNew = True And Me.GetSelectedItem(moProductCodeDrop) = Guid.Empty Then
            '    ControlMgr.SetVisibleControl(Me, pnlUpfrontComm, False)
            'Else
            '    oProdcode = New ProductCode(GetSelectedItem(moProductCodeDrop))
            '    Me.State.oContract = Contract.GetContract(TheDealerId, System.DateTime.Now)
            '    If Not Me.State.oContract Is Nothing Then                                       
            '        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.oContract.InstallmentPaymentId) = Codes.YESNO_Y Then
            '            ControlMgr.SetVisibleControl(Me, pnlUpfrontComm, True)
            '            Me.SetSelectedItem(moupfrontcommDrop, oProdcode.UpfrontCommissionId)
            '            Me.ChangeEnabledControlProperty(moupfrontcommDrop, False)
            '        End If                    
            '    End If
            'End If

        End Sub

        Private Sub PopulatePCode()
            Try
                PopulateDealer()
                PopulateProductCode()
                PopulateDates()
                'PopulateUpfrontComm()
                EnableDateFields()
                PopulatePCodeEntity()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Clear"

        Private Sub ClearDealerProdCode()
            Dim oProdCode As ProductCode
            If Me.State.IsPCodeNew = True Then
                If TheDealerControl.SelectedIndex >= 0 Then TheDealerControl.SelectedIndex = 0
                If moProductCodeDrop.SelectedIndex >= 0 Then moProductCodeDrop.SelectedIndex = 0
                'If pnlUpfrontComm.Visible = True Then
                '    If TheDealerControl.SelectedIndex >= 0 Then moupfrontcommDrop.SelectedIndex = 0
                'End If
            Else
                TheDealerControl.SelectedGuid = TheDealerId
                Me.SetSelectedItem(moProductCodeDrop, ThePCode.ProductCodeId)
                'If pnlUpfrontComm.Visible = True Then
                '    oProdCode = New ProductCode(ThePCode.ProductCodeId)
                '    Me.SetSelectedItem(moupfrontcommDrop, oProdCode.UpfrontCommissionId)
                'End If

            End If

        End Sub

        Private Sub ClearPCode()
            ClearDealerProdCode()
            ClearEntity()
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulatePCodeBOFromForm(ByVal oPCode As CommPCode)
            With oPCode
                ' DropDowns
                .ProductCodeId = Me.GetSelectedItem(moProductCodeDrop)
                ' Texts
                Me.PopulateBOProperty(oPCode, EFFECTIVE_DATE_PROPERTY, moEffectiveText_WRITE)
                Me.PopulateBOProperty(oPCode, EXPIRATION_DATE_PROPERTY, moExpirationText_WRITE)
            End With


            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Function IsDirtyPCodeBO() As Boolean
            Dim bIsDirty As Boolean = True
            Dim bIsEntityDirty As Boolean
            Dim oPCode As CommPCode

            oPCode = ThePCode
            With oPCode
                PopulatePCodeBOFromForm(Me.State.MyBo)
                bIsDirty = .IsDirty
            End With
            bIsEntityDirty = IsDirtyEntityBO()
            Return bIsDirty OrElse bIsEntityDirty
            
        End Function

        
        Private Function ApplyPCodeChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPCode As CommPCode

            Try
               If IsDirtyPCodeBO() = True Then
                  oPCode = ThePCode
                    '  LoadEntityList()
                    oPCode.Save()
                  AddInfoMsgWithDelay(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    '  Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    AddInfoMsgWithDelay(Message.MSG_RECORD_NOT_SAVED)
                End If
                Me.State.IsPCodeNew = False
                Me.State.IsPCodeEntityNew = False
                SetPCodeButtonsState(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeletePCode() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPCode As CommPCode

            Try
                oPCode = ThePCode
                With oPCode
                    If DeleteAllEntity() = False Then
                        ' Error in Entity
                        .cancelEdit()
                        bIsOk = False
                    Else
                        .Delete()
                        .Save()
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region

#Region "Entity"

#Region "Entity-Handlers"

#Region "Entity Handlers-Buttons"

        Private Sub setbuttons(ByVal enable As Boolean)
            ControlMgr.SetEnableControl(Me, btnBack, enable)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, enable)
            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate_WRITE, enable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate_WRITE, enable)
        End Sub

        Private Sub NewEntity()
           Me.State.moCommPCodeEntityId = Guid.Empty
            Me.State.IsPCodeEntityNew = True
            Me.State.searchDV = FromGridToBuffer(False)
            PopulatePCodeEntity(POPULATE_ACTION_NEW, False)
            Me.SetGridControls(moEntityGrid, False)
            EnableDisableControls(Me.moPCodePanel_WRITE, True)
           
        End Sub

        Protected Sub BtnNewEntity_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNewEntity_WRITE.Click
            Try
                NewEntity()
            
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorControllerEntity)
            Finally
                FromGridToBuffer()
            End Try

        End Sub

#End Region

#Region "Entity Handlers-Dropdowns"

        Protected Sub moPayeeTypeDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim payeeDrop, entityDrop, dealerdrop As DropDownList
            Dim payeeGuid As Guid
            Dim payeeView As DataView
            Dim payeeCode As String
            Dim index As Integer

            Try
                index = DirectCast(DirectCast(DirectCast(DirectCast(DirectCast(sender, System.Web.UI.WebControls.DropDownList).Parent, System.Web.UI.Control), System.Web.UI.WebControls.DataControlFieldCell).NamingContainer, System.Web.UI.Control), System.Web.UI.WebControls.GridViewRow).RowIndex
                payeeDrop = CType(GetGridControl(moEntityGrid, index, ENTITY_PAYEE_TYPE_COL), DropDownList)
                
                ' Enitity
                entityDrop = CType(GetGridControl(moEntityGrid, index, ENTITY_ENTITY_COL), DropDownList)
                ClearEntityDrop(entityDrop)
                payeeGuid = Me.GetSelectedItem(payeeDrop)
                payeeView = LookupListNew.GetPayeeTypeLookupList(Authentication.LangId)
                payeeCode = LookupListNew.GetCodeFromId(payeeView, payeeGuid)
                If payeeCode = Codes.Payee_Type_Comm_Entity Then
                    PopulateEntityDrop(entityDrop)
                End If
                'REQ-976
                If payeeCode = Codes.Payee_Type_Branch Then
                    dealerdrop = CType(Me.moDealerMultipleDrop.FindControl("moMultipleColumnDrop"), DropDownList)
                    PopulateBranchEntityDrop(dealerdrop, entityDrop)
                End If
                'End REQ-976

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorControllerEntity)
            Finally
                FromGridToBuffer()
            End Try

        End Sub


#End Region

#Region "Entity-Handlers-Grid"


        'Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        '    Try
        '        Me.moEntityGrid.PageIndex = NewCurrentPageIndex(moEntityGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
        '        Me.State.PageSize = moEntityGrid.PageSize
        '        Me.PopulatePCodeEntity()
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.ErrControllerMaster)
        '    End Try
        'End Sub

        'Private Sub PageChanged(ByVal index As Integer)
        '    LoadEntityList()
        '    Me.moEntityGrid.PageIndex = index
        '    PopulatePCodeEntity(Me.POPULATE_ACTION_NO_EDIT, False)
        'End Sub

        'Private Sub moEntityGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moEntityGrid.PageIndexChanging
        '    Try
        '        'Me.moEntityGrid.PageIndex = e.NewPageIndex
        '        'PopulatePCodeEntity(Me.POPULATE_ACTION_NO_EDIT)
        '        PageChanged(e.NewPageIndex)
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, moErrorControllerEntity)
        '    End Try
        'End Sub

        Public Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
          
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moEntityGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oTextBox As TextBox
            Dim payeeDrop, entityDrop, isCommFixedDrop, commScheduleDrop, dealerdrop As DropDownList
            Dim payeeGuid, entityGuid, commScheduleGuid As Guid
            Dim payeeCode, isCommFixedCode As String
            Dim sCommAmount, sMarkupAmount, clawbackdays As String
            Dim payeeView As DataView
           
            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    If Not dvRow(CommPCodeEntityDAL.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                        payeeDrop = CType(e.Row.FindControl("moPayeeTypeDrop"), DropDownList)
                        PopulatePayeeDrop(payeeDrop)
                        payeeGuid = New Guid(CType(dvRow(CommPCodeEntityDAL.COL_NAME_PAYEE_TYPE_ID), Byte()))
                        If (payeeDrop.Items.FindByValue(payeeGuid.ToString) Is Nothing) Then
                            ' New Record
                            payeeDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                        Else 'Old Record
                            Me.SetSelectedItem(payeeDrop, payeeGuid)
                        End If

                    End If

                    ' Entity Depends on Payee Type
                    If Not dvRow(CommPCodeEntityDAL.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                        entityDrop = CType(e.Row.FindControl("moEntityDrop"), DropDownList)
                        ClearEntityDrop(entityDrop)
                        payeeView = LookupListNew.GetPayeeTypeLookupList(Authentication.LangId)
                        payeeCode = LookupListNew.GetCodeFromId(payeeView, payeeGuid)
                        If payeeCode = Codes.Payee_Type_Comm_Entity Then
                            PopulateEntityDrop(entityDrop)
                            If Not dvRow(CommPCodeEntityDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                                entityGuid = New Guid(CType(dvRow(CommPCodeEntityDAL.COL_NAME_ENTITY_ID), Byte()))
                            Else
                                entityGuid = Guid.Empty
                            End If
                            If (entityDrop.Items.FindByValue(entityGuid.ToString) Is Nothing) Then
                                ' New Record
                                entityDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                            Else 'Old Record
                                Me.SetSelectedItem(entityDrop, entityGuid)
                            End If
                        End If

                        ''REQ 976
                        If payeeCode = Codes.Payee_Type_Branch Then
                            dealerdrop = CType(Me.moDealerMultipleDrop.FindControl("moMultipleColumnDrop"), DropDownList)
                            PopulateBranchEntityDrop(dealerdrop, entityDrop)
                            If Not dvRow(CommPCodeEntityDAL.COL_NAME_BRANCH_ID) Is DBNull.Value Then
                                entityGuid = New Guid(CType(dvRow(CommPCodeEntityDAL.COL_NAME_BRANCH_ID), Byte()))
                            Else
                                entityGuid = Guid.Empty
                            End If
                            If (entityDrop.Items.FindByValue(entityGuid.ToString) Is Nothing) Then
                                ' New Record
                                entityDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                            Else 'Old Record
                                Me.SetSelectedItem(entityDrop, entityGuid)
                            End If
                        End If

                        If payeeCode Is Nothing Then
                            dealerdrop = CType(Me.moDealerMultipleDrop.FindControl("moMultipleColumnDrop"), DropDownList)
                            PopulateBranchEntityDrop(dealerdrop, entityDrop)
                            If Not dvRow(CommPCodeEntityDAL.COL_NAME_BRANCH_ID) Is DBNull.Value Then
                                entityGuid = New Guid(CType(dvRow(CommPCodeEntityDAL.COL_NAME_BRANCH_ID), Byte()))
                            Else
                                entityGuid = Guid.Empty
                            End If
                            If (entityDrop.Items.FindByValue(entityGuid.ToString) Is Nothing) Then
                                ' New Record
                                entityDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                            Else 'Old Record
                                Me.SetSelectedItem(entityDrop, entityGuid)
                            End If
                        End If


                        'End REQ-976

                    End If

                    'If Not dvRow(CommPCodeEntityDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                    '    entityDrop = CType(e.Row.FindControl("moEntityDrop"), DropDownList)
                    '    ClearEntityDrop(entityDrop)
                    '    payeeView = LookupListNew.GetPayeeTypeLookupList(Authentication.LangId)
                    '    payeeCode = LookupListNew.GetCodeFromId(payeeView, payeeGuid)
                    '    If payeeCode = Codes.Payee_Type_Comm_Entity Then
                    '        PopulateEntityDrop(entityDrop)
                    '        entityGuid = New Guid(CType(dvRow(CommPCodeEntityDAL.COL_NAME_ENTITY_ID), Byte()))
                    '        If (entityDrop.Items.FindByValue(entityGuid.ToString) Is Nothing) Then
                    '            ' New Record
                    '            entityDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                    '        Else 'Old Record
                    '            Me.SetSelectedItem(entityDrop, entityGuid)
                    '        End If
                    '    End If

                    'End If

                    If Not dvRow(CommPCodeEntityDAL.COL_NAME_IS_COMM_FIXED_CODE) Is DBNull.Value Then
                        isCommFixedDrop = CType(e.Row.FindControl("moIsCommFixedDrop"), DropDownList)
                        isCommFixedDrop.Attributes.Add("onchange", "setCommDrop(this, " & .RowIndex & ")")
                        PopulateYesNoDrop(isCommFixedDrop, False)
                        isCommFixedCode = CType(dvRow(CommPCodeEntityDAL.COL_NAME_IS_COMM_FIXED_CODE), String)
                        If (isCommFixedDrop.Items.FindByValue(isCommFixedCode) Is Nothing) Then
                            ' New Record
                            isCommFixedCode = "Y"
                        End If
                        Me.SetSelectedItem(isCommFixedDrop, isCommFixedCode)
                    End If

                    If Not dvRow(CommPCodeEntityDAL.COL_NAME_COMM_SCHEDULE_ID) Is DBNull.Value Then
                        commScheduleDrop = CType(e.Row.FindControl("moCommScheduleDrop"), DropDownList)
                        PopulateCommScheduleDrop(commScheduleDrop)
                        commScheduleGuid = New Guid(CType(dvRow(CommPCodeEntityDAL.COL_NAME_COMM_SCHEDULE_ID), Byte()))
                        If (commScheduleDrop.Items.FindByValue(commScheduleGuid.ToString) Is Nothing) Then
                            ' New Record
                            commScheduleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                        Else 'Old Record
                            Me.SetSelectedItem(commScheduleDrop, commScheduleGuid)
                        End If
                    End If

                    If dvRow(CommPCodeEntityDAL.COL_NAME_COMMISSION_AMOUNT) Is DBNull.Value Then
                        ' New Record
                        dvRow(CommPCodeEntityDAL.COL_NAME_COMMISSION_AMOUNT) = 0
                    End If
                    oTextBox = CType(.FindControl("moCommissionAmount"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setCommTotal(this, " & .RowIndex & ")")
                    sCommAmount = GetAmountFormattedDoubleString(CType(dvRow(CommPCodeEntityDAL.COL_NAME_COMMISSION_AMOUNT), String))
                    Me.PopulateControlFromBOProperty(oTextBox, sCommAmount)

                    'If Not dvRow(CommPCodeEntityDAL.COL_NAME_IS_MARKUP_FIXED_CODE) Is DBNull.Value Then
                    '    isMarkupFixedDrop = CType(e.Row.FindControl("moIsMarkupFixedDrop"), DropDownList)
                    '    isMarkupFixedDrop.Attributes.Add("onchange", "setMarkupDrop(this, " & .RowIndex & ")")
                    '    PopulateYesNoDrop(isMarkupFixedDrop, False)
                    '    isMarkupFixedCode = CType(dvRow(CommPCodeEntityDAL.COL_NAME_IS_MARKUP_FIXED_CODE), String)
                    '    If (isMarkupFixedDrop.Items.FindByValue(isMarkupFixedCode) Is Nothing) Then
                    '        ' New Record
                    '        isMarkupFixedCode = "Y"
                    '    End If
                    '    Me.SetSelectedItem(isMarkupFixedDrop, isMarkupFixedCode)
                    'End If

                    If dvRow(CommPCodeEntityDAL.COL_NAME_MARKUP_AMOUNT) Is DBNull.Value Then
                        ' New Record
                        dvRow(CommPCodeEntityDAL.COL_NAME_MARKUP_AMOUNT) = 0
                    End If
                    oTextBox = CType(.FindControl("moMarkupAmountText"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setMarkupTotal(this, " & .RowIndex & ")")
                    sMarkupAmount = GetAmountFormattedDoubleString(CType(dvRow(CommPCodeEntityDAL.COL_NAME_MARKUP_AMOUNT), String))
                    Me.PopulateControlFromBOProperty(oTextBox, sMarkupAmount)

                    ''REQ-976 
                    If dvRow(CommPCodeEntityDAL.COL_NAME_DAYS_TO_CLAWBACK) Is DBNull.Value Then
                        ' New Record
                        dvRow(CommPCodeEntityDAL.COL_NAME_DAYS_TO_CLAWBACK) = 0

                    End If
                    oTextBox = CType(.FindControl("modaystoclawbackText"), TextBox)

                    clawbackdays = CType(dvRow(CommPCodeEntityDAL.COL_NAME_DAYS_TO_CLAWBACK), String)
                    Me.PopulateControlFromBOProperty(oTextBox, clawbackdays)

                    'End REQ-976
                End With




              
            End If

            ' Footer
            If itemType = ListItemType.Footer Then
                e.Row.Cells(ENTITY_PAYEE_TYPE_COL).Text = BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage("TOTAL")
                ' Me.PopulateControlFromBOProperty(e.Row.Cells(3), 30.0)
                oTextBox = CType(e.Row.FindControl("moTotalCommText"), TextBox)
                oTextBox.Text = String.Empty
                EnableDisableControls(oTextBox, True)  ' ReadOnly
                Me.State.totalCFId = oTextBox.ClientID
                oTextBox = CType(e.Row.FindControl("moTotalMarkupText"), TextBox)
                oTextBox.Text = String.Empty
                EnableDisableControls(oTextBox, True)  ' ReadOnly
                Me.State.totalMFId = oTextBox.ClientID
            End If
            BaseItemBound(source, e)
            
        End Sub

        Private Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moEntityGrid.RowCommand
            Try
                'If (e.CommandName = Me.EDIT_COMMAND_NAME) Then
                '    Dim index As Integer = CInt(e.CommandArgument)
                '    Me.State.moCommPCodeEntityId = Me.GetGuidFromString( _
                '                    Me.GetGridText(Me.moEntityGrid, index, ENTITY_ID_COL))

                '    '  ControlMgr.SetVisibleControl(Me, btnEntityBack, True)
                '    moEntityGrid.EditIndex = index
                '    moEntityGrid.SelectedIndex = index
                '    Me.State.IsPCodeEntityNew = False
                '    PopulatePCodeEntity(POPULATE_ACTION_EDIT)

                '    PopulateFormFromPCodeEntityBO()

                '    Me.SetGridControls(moEntityGrid, False)
                '    Me.SetFocusInGrid(moEntityGrid, index, ENTITY_CODE_COL)
                '    EnableDisableControls(Me.moPCodePanel_WRITE, True)
                '    setbuttons(False)
                '    '  EnablePCodeEntityGrid(False)
                '    'Me.State.moCommPCodeEntity = New CommPCodeEntity(Me.State.moCommPCodeEntityId)
                '    'EditTolerance()
                '    'PopulatePeriodEntity()

                'ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then

                If (e.CommandName = Me.DELETE_COMMAND_NAME) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moCommPCodeEntityId = Me.GetGuidFromString( _
                                    Me.GetGridText(Me.moEntityGrid, index, ENTITY_ID_COL))
                    If DeleteEntity(True) = True Then
                        PopulatePCodeEntity(Me.POPULATE_ACTION_NO_EDIT)
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, moErrorControllerEntity)
            Finally
                FromGridToBuffer()
            End Try
        End Sub


#End Region

#Region "Entity-Handlers-Labels"

        Protected Sub BindBoPropertiesToGridHeader(ByVal oEntity As CommPCodeEntity)
            Me.BindBOPropertyToGridHeader(oEntity, COMMISSION_AMOUNT_PROPERTY, _
                                                Me.moEntityGrid.Columns(ENTITY_COMMISSION_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(oEntity, MARKUP_AMOUNT_PROPERTY, _
                                                Me.moEntityGrid.Columns(ENTITY_MARKUP_AMOUNT_COL))

            'REQ-976
            Me.BindBOPropertyToGridHeader(oEntity, DAYS_TO_CLAWBACK_PROPERTY, Me.moEntityGrid.Columns(ENTITY_DAYS_TO_CLAWBACK_COL))
            'END REQ-976
        End Sub


#End Region

#End Region

#Region "Entity Button-Management"

        Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
            SetEntityButtonsState(bIsEdit)
        End Sub

        Private Sub SetEntityButtonsState(ByVal bIsEdit As Boolean)
            If Me.State.IsPCodeNew = True Then
                ControlMgr.SetVisibleControl(Me, BtnNewEntity_WRITE, False)
            Else
                ControlMgr.SetVisibleControl(Me, BtnNewEntity_WRITE, True)
            End If
           
        End Sub


#End Region

#Region "Entity Populate"

        Private Function PopulatePayeeDrop(ByVal payeeDrop As DropDownList) As ListItem()
            Try
                'Dim payeeView As DataView = LookupListNew.GetPayeeTypeLookupList(Authentication.LangId)
                'Me.BindListControlToDataView(payeeDrop, payeeView, , , False)
                Dim payeeTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("PYTYPE", Thread.CurrentPrincipal.GetLanguageCode())
                payeeDrop.Populate(payeeTypes, New PopulateOptions() With
                                                    {
                                                      .AddBlankItem = False
                                                     })
                Return payeeTypes

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorControllerEntity)
            End Try
        End Function

        Private Function PopulateCommScheduleDrop(ByVal commSchlDrop As DropDownList) As ListItem()
            Try
                'Dim commSchlView As DataView = LookupListNew.GetCommScheduleLookupList(Authentication.LangId)
                'Me.BindListControlToDataView(commSchlDrop, commSchlView, , , True)

                Dim CommSchedule As ListItem() = CommonConfigManager.Current.ListManager.GetList("COMMSCHL", Thread.CurrentPrincipal.GetLanguageCode())
                commSchlDrop.Populate(CommSchedule, New PopulateOptions() With
                                                    {
                                                      .AddBlankItem = True
                                                     })

                Return CommSchedule

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorControllerEntity)
            End Try
        End Function

        Private Sub PopulateEntityDrop(ByVal entityDrop As DropDownList)
            Dim listcontextForCommEntity As ListContext = New ListContext()
            listcontextForCommEntity.CompanyGroupId = Authentication.CompanyGroupId
            Dim commEntityList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CommEntityByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForCommEntity)

            entityDrop.Populate(commEntityList, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .ValueFunc = AddressOf .GetListItemId,
                       .TextFunc = AddressOf .GetDescription,
                       .SortFunc = AddressOf .GetDescription
                    })

            'Dim enitityView As DataView = LookupListNew.GetCommEntityLookupList(Authentication.CompanyGroupId)
            'Me.BindListControlToDataView(entityDrop, enitityView, , , True)
        End Sub
        ''REQ-976
        Private Sub PopulateBranchEntityDrop(ByVal dealerdrop As DropDownList, ByVal entityDrop As DropDownList)
            Dim dealerid As Guid = Me.GetSelectedItem(dealerdrop)
            Dim listcontextForBranchCode As ListContext = New ListContext()
            listcontextForBranchCode.DealerId = TheDealerId
            Dim branchCodeList As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.BranchCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontextForBranchCode)

            entityDrop.Populate(branchCodeList, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .ValueFunc = AddressOf .GetListItemId,
                       .TextFunc = AddressOf .GetDescription,
                       .SortFunc = AddressOf .GetDescription
                    })
            'Me.BindListControlToDataView(entityDrop, LookupListNew.GetBranchCodeLookupList(dealerid), "DESCRIPTION", "ID", True)
        End Sub
        'End REQ-976

        Private Sub PopulateYesNoDrop(ByVal yesNoDrop As DropDownList, ByVal nothingSelected As Boolean)
            Try
                'Dim yesNoView As DataView = LookupListNew.GetYesNoLookupList(Authentication.LangId)
                'Me.BindListTextToDataView(yesNoDrop, yesNoView, , "CODE", nothingSelected)

                Dim yesNoLkup As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                yesNoDrop.Populate(yesNoLkup, New PopulateOptions() With
                                                    {
                                                      .AddBlankItem = nothingSelected,
                                                      .ValueFunc = AddressOf .GetCode
                                                     })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorControllerEntity)
            End Try
        End Sub

        Private Function GetPCodeEntityDataView() As DataView
            Dim commPCodeEntityView As DataView = CommPCodeEntity.getList(Me.State.MyBo.Id)
            Return commPCodeEntityView

        End Function

        Public Overrides Sub AddNewBoRow(ByVal dv As DataView)
            Dim oId As Guid = Guid.NewGuid

            Me.BaseAddNewGridRow(Me.moEntityGrid, dv, oId)
        End Sub

        Private Sub PopulatePCodeEntity(Optional ByVal oAction As String = POPULATE_ACTION_NONE, _
                                        Optional ByVal reLoad As Boolean = True)
            Dim oDataView As DataView
            Dim totalPComm As Double = 0
            Dim totalPMarkup As Double = 0

            Try
                If Me.State.IsPCodeNew = True Then
                    SetEntityButtonsState(False)
                End If

                If reLoad = True Then
                    oDataView = GetPCodeEntityDataView()
                    Me.State.MyBo.AttachEntities(totalPComm, totalPMarkup)
                    Me.State.searchDV = oDataView
                End If
                BasePopulateGrid(Me.moEntityGrid, Me.State.searchDV, Me.State.moCommPCodeEntityId, oAction)
            
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorControllerEntity)
            End Try

        End Sub

#End Region

#Region "Entity Clear"

        Private Sub ClearEntity()
            Me.moEntityGrid.DataSource = Nothing
            Me.moEntityGrid.DataBind()
        End Sub

        Private Sub ClearEntityDrop(ByVal entityDrop As DropDownList)
            Me.ClearList(entityDrop)
        End Sub

#End Region

#Region "Entity-Business Part"

        Private Function IsDirtyEntityBO() As Boolean
            Dim bIsDirty As Boolean = True

            bIsDirty = LoadEntityList()

            Return bIsDirty
        End Function

        Private Function DeleteEntity(ByVal singleEntry As Boolean) As Boolean
            Dim bIsOk As Boolean = True
            If (singleEntry = True) Then
                Me.State.moCommPCodeEntity = New CommPCodeEntity(Me.State.moCommPCodeEntityId)
            Else
                Me.State.moCommPCodeEntity = Me.State.MyBo.AddCommPCodeEntity(Me.State.moCommPCodeEntityId)
            End If

            Try
                With Me.State.moCommPCodeEntity
                    .Delete(singleEntry)
                    .Save()
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorControllerEntity)
                bIsOk = False
            End Try
            Me.State.moCommPCodeEntity = Nothing
            Return bIsOk
        End Function

        Private Function DeleteAllEntity() As Boolean
            Dim bIsOk As Boolean = True
            Dim oDataView As DataView
            Dim oRows As DataRowCollection
            Dim oRow As DataRow

            'Try
            oRows = CommPCodeEntity.getList(ThePCode.Id).Table.Rows
            For Each oRow In oRows
                Me.State.moCommPCodeEntityId = New Guid(CType(oRow(CommPCodeEntityDAL.COL_NAME_COMM_P_CODE_ENTITY_ID), Byte()))
                Me.State.moCommPCodeEntity = Me.State.MyBo.AddCommPCodeEntity(Me.State.moCommPCodeEntityId)
                If DeleteEntity(False) = False Then Return False
            Next
            Return bIsOk
        End Function

        Private Function LoadEntityList() As Boolean
            Dim entity As CommPCodeEntity
            Dim entityId As Guid
            Dim payeeId, entityDropId, isCommFixedId, commScheduleId As Guid
            Dim isFixedView As DataView


            Dim payeeDrop, entityDrop, isCommFixedDrop, commScheduleDrop As DropDownList
            Dim bIsDirty As Boolean = False
            Dim isCommFixedCode, payeedesc As String

            If moEntityGrid.Rows.Count = 0 Then Return bIsDirty
            For Each row As GridViewRow In moEntityGrid.Rows
                entityId = Me.GetGuidFromString( _
                                Me.GetGridText(Me.moEntityGrid, row.RowIndex, ENTITY_ID_COL))
                entity = Me.State.MyBo.AddCommPCodeEntity(entityId)
                BindBoPropertiesToGridHeader(entity)

                ' Descriptions
                Me.PopulateBOProperty(entity, COMMISSION_AMOUNT_PROPERTY, _
                    CType(Me.GetGridControl(moEntityGrid, row.RowIndex, ENTITY_COMMISSION_AMOUNT_COL), TextBox))
                Me.PopulateBOProperty(entity, MARKUP_AMOUNT_PROPERTY, _
                    CType(Me.GetGridControl(moEntityGrid, row.RowIndex, ENTITY_MARKUP_AMOUNT_COL), TextBox))
                'REQ-976
                Me.PopulateBOProperty(entity, DAYS_TO_CLAWBACK_PROPERTY, _
                                      CType(Me.GetGridControl(moEntityGrid, row.RowIndex, ENTITY_DAYS_TO_CLAWBACK_COL), TextBox))

                'End REQ-976

                ' Guids
                payeeDrop = CType(row.FindControl("moPayeeTypeDrop"), DropDownList)
                payeeId = Me.GetSelectedItem(payeeDrop)
                Me.PopulateBOProperty(entity, PAYEE_TYPE_ID_PROPERTY, payeeId)

                'REQ-976
                payeedesc = Me.GetSelectedDescription(payeeDrop)

                If payeedesc = "Branch" Then
                    entityDrop = CType(row.FindControl("moEntityDrop"), DropDownList)
                    entityDropId = Me.GetSelectedItem(entityDrop)
                    Me.PopulateBOProperty(entity, BRANCH_ID_PROPERTY, entityDropId)
                Else
                    ' If payeedesc = "Commission Entity" Then
                    entityDrop = CType(row.FindControl("moEntityDrop"), DropDownList)
                    entityDropId = Me.GetSelectedItem(entityDrop)
                    Me.PopulateBOProperty(entity, ENTITY_ID_PROPERTY, entityDropId)
                End If
                'End Req-976


                isCommFixedDrop = CType(row.FindControl("moIsCommFixedDrop"), DropDownList)
                isFixedView = LookupListNew.GetYesNoLookupList(Authentication.LangId)
                isCommFixedCode = Me.GetSelectedValue(isCommFixedDrop)
                isCommFixedId = LookupListNew.GetIdFromCode(isFixedView, isCommFixedCode)
                Me.PopulateBOProperty(entity, IS_COMM_FIXED_ID_PROPERTY, isCommFixedId)

                commScheduleDrop = CType(row.FindControl("mocommScheduleDrop"), DropDownList)
                commScheduleId = Me.GetSelectedItem(commScheduleDrop)
                Me.PopulateBOProperty(entity, COMM_SCHEDULE_ID_PROPERTY, commScheduleId)

                'isMarkupFixedDrop = CType(row.FindControl("moIsMarkupFixedDrop"), DropDownList)
                'isMarkupFixedCode = Me.GetSelectedValue(isMarkupFixedDrop)
                'isMarkupFixedId = LookupListNew.GetIdFromCode(isFixedView, isMarkupFixedCode)
                'Me.PopulateBOProperty(entity, IS_MARKUP_FIXED_ID_PROPERTY, isMarkupFixedId)
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
                If entity.IsDirty = True Then
                    bIsDirty = True
                End If
                ' Validate a Single Entity; It is not saving because it has a Parent (DSCreator)
                entity.Save()
            Next
            Return bIsDirty
        End Function


#End Region

#End Region

#Region "State-Management"

#Region "P Code State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button
                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyPCodeChanges() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' Go back to Search Page
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and create a new BO
                        If ApplyPCodeChanges() = True Then
                            CreateNew()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyPCodeChanges() = True Then
                            Me.State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                        FromGridToBuffer()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                        FromGridToBuffer()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                        FromGridToBuffer()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
                        FromGridToBuffer()
                End Select

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region


#End Region

#Region "Client Scripts"

        Private Sub CreateClientScripts(Optional ByVal doReg As Boolean = True)
            Dim oBuffer As StringBuilder = New StringBuilder(String.Empty)
            Dim oTextBox As TextBox

            If (doReg = True) Then
                With oBuffer
                    .Append("<script type ='text/jscript' language='JavaScript'>")
                    .AppendLine()
                    .Append("function GetCommFixedId() {")
                    .AppendLine()
                    .Append("return '" & Me.State.totalCFId & "';")
                    .Append("}")
                    .AppendLine()
                    .Append("function GetMarkupFixedId() {")
                    .AppendLine()
                    .Append("return '" & Me.State.totalMFId & "';")
                    .Append("}")
                    .AppendLine()
                    .Append("function InitFixedArrays() {")
                    .AppendLine()
                    .Append(Me.State.sBuffer.ToString())
                    .Append("}")
                    .AppendLine()
                    .Append("InitFixedArrays();")
                    .AppendLine()
                    .Append("</script>")
                    .AppendLine()
                End With
                'If Page.ClientScript.IsStartupScriptRegistered("InitFixedArraysScript") = True Then
                '    Dim hi As Boolean = True
                'End If
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "InitFixedArraysScript", oBuffer.ToString(), False)
            End If

            'moEntityCTotalPText.Text = GetAmountFormattedDoubleString(Me.State.totalCF.ToString)
            'moEntityMTotalPText.Text = GetAmountFormattedDoubleString(Me.State.totalMF.ToString)

            If (Not moEntityGrid.FooterRow Is Nothing) Then
                oTextBox = CType(moEntityGrid.FooterRow.FindControl("moTotalCommText"), TextBox)
                If (Not oTextBox Is Nothing) Then
                    oTextBox.Text = GetAmountFormattedDoubleString(Me.State.totalCF.ToString)
                End If

                oTextBox = CType(moEntityGrid.FooterRow.FindControl("moTotalMarkupText"), TextBox)
                If (Not oTextBox Is Nothing) Then
                    oTextBox.Text = GetAmountFormattedDoubleString(Me.State.totalMF.ToString)
                End If
            End If
        End Sub

        Private Function FromGridToBuffer(Optional ByVal doReg As Boolean = True) As DataView
            Dim entityList As CommPCodeEntityList
            Dim entityId, branchid As Guid
            Dim payeeId, entityDropId, isCommFixedId, commScheduleId As Guid
            Dim payeeDesc, entityDropDesc, isCommFixedDesc, commScheduleDesc As String
            Dim isFixedView As DataView
            Dim payeeDrop, entityDrop, isCommFixedDrop, commScheduleDrop, dealerdrop As DropDownList
            Dim isCommFixedCode As String
            Dim oDataSet As DataSet
            Dim dCommAmount, dMarkupAmount As Decimal


            Try
                Me.State.sBuffer = New StringBuilder(String.Empty)
                Me.State.totalCF = 0
                Me.State.totalMF = 0
                oDataSet = New DataSet
            

                For Each row As GridViewRow In moEntityGrid.Rows
                    dCommAmount = 0
                    dMarkupAmount = 0
                    entityId = Me.GetGuidFromString( _
                                Me.GetGridText(Me.moEntityGrid, row.RowIndex, ENTITY_ID_COL))

                    entityList = New CommPCodeEntityList(True, entityId, oDataSet)


                    ' Descriptions
                    Me.PopulateBOProperty(entityList, COMMISSION_AMOUNT_PROPERTY, _
                        CType(Me.GetGridControl(moEntityGrid, row.RowIndex, ENTITY_COMMISSION_AMOUNT_COL), TextBox))
                    Me.PopulateBOProperty(entityList, MARKUP_AMOUNT_PROPERTY, _
                        CType(Me.GetGridControl(moEntityGrid, row.RowIndex, ENTITY_MARKUP_AMOUNT_COL), TextBox))
                    'REQ-976
                    Me.PopulateBOProperty(entityList, DAYS_TO_CLAWBACK_PROPERTY, _
                                    CType(Me.GetGridControl(moEntityGrid, row.RowIndex, ENTITY_DAYS_TO_CLAWBACK_COL), TextBox))

                    'END REQ-976
                    ' Guids
                    payeeDrop = CType(row.FindControl("moPayeeTypeDrop"), DropDownList)
                    payeeId = Me.GetSelectedItem(payeeDrop)
                    Me.PopulateBOProperty(entityList, PAYEE_TYPE_ID_PROPERTY, payeeId)
                    payeeDesc = Me.GetSelectedDescription(payeeDrop)
                    Me.PopulateBOProperty(entityList, PAYEE_TYPE_PROPERTY, payeeDesc)

                    'entityDrop = CType(row.FindControl("moEntityDrop"), DropDownList)
                    'entityDropId = Me.GetSelectedItem(entityDrop)
                    'Me.PopulateBOProperty(entityList, ENTITY_ID_PROPERTY, entityDropId)
                    'entityDropDesc = Me.GetSelectedDescription(entityDrop)
                    'Me.PopulateBOProperty(entityList, ENTITY_PROPERTY, entityDropDesc)

                    'REQ-976
                    If payeeDesc = "Branch" Then
                        entityDrop = CType(row.FindControl("moEntityDrop"), DropDownList)
                        dealerdrop = CType(Me.moDealerMultipleDrop.FindControl("moMultipleColumnDrop"), DropDownList)
                        entityDropId = Me.GetSelectedItem(entityDrop)
                        Me.PopulateBOProperty(entityList, BRANCH_ID_PROPERTY, entityDropId)
                        entityDropDesc = Me.GetSelectedDescription(entityDrop)
                        Me.PopulateBOProperty(entityList, BRANCH_PROPERTY, entityDropDesc)

                    Else
                        entityDrop = CType(row.FindControl("moEntityDrop"), DropDownList)
                        entityDropId = Me.GetSelectedItem(entityDrop)
                        Me.PopulateBOProperty(entityList, ENTITY_ID_PROPERTY, entityDropId)
                        entityDropDesc = Me.GetSelectedDescription(entityDrop)
                        Me.PopulateBOProperty(entityList, ENTITY_PROPERTY, entityDropDesc)
                    End If
                    'End Req-976

                    isCommFixedDrop = CType(row.FindControl("moIsCommFixedDrop"), DropDownList)
                    isFixedView = LookupListNew.GetYesNoLookupList(Authentication.LangId)
                    isCommFixedCode = Me.GetSelectedValue(isCommFixedDrop)
                    isCommFixedId = LookupListNew.GetIdFromCode(isFixedView, isCommFixedCode)
                    Me.PopulateBOProperty(entityList, IS_COMM_FIXED_ID_PROPERTY, isCommFixedId)
                    Me.PopulateBOProperty(entityList, IS_COMM_FIXED_CODE_PROPERTY, isCommFixedCode)
                    isCommFixedDesc = Me.GetSelectedDescription(isCommFixedDrop)
                    Me.PopulateBOProperty(entityList, IS_COMM_FIXED_PROPERTY, isCommFixedDesc)

                    commScheduleDrop = CType(row.FindControl("moCommScheduleDrop"), DropDownList)
                    commScheduleId = Me.GetSelectedItem(commScheduleDrop)
                    Me.PopulateBOProperty(entityList, COMM_SCHEDULE_ID_PROPERTY, commScheduleId)
                    commScheduleDesc = Me.GetSelectedDescription(commScheduleDrop)
                    Me.PopulateBOProperty(entityList, COMM_SCHEDULE_PROPERTY, commScheduleDesc)

                    'isMarkupFixedDrop = CType(row.FindControl("moIsMarkupFixedDrop"), DropDownList)
                    'isMarkupFixedCode = Me.GetSelectedValue(isMarkupFixedDrop)
                    'isMarkupFixedId = LookupListNew.GetIdFromCode(isFixedView, isMarkupFixedCode)
                    'Me.PopulateBOProperty(entityList, IS_MARKUP_FIXED_ID_PROPERTY, isMarkupFixedId)
                    'Me.PopulateBOProperty(entityList, IS_MARKUP_FIXED_CODE_PROPERTY, isMarkupFixedCode)
                    'isMarkupFixedDesc = Me.GetSelectedDescription(isMarkupFixedDrop)
                    'Me.PopulateBOProperty(entityList, IS_MARKUP_FIXED_PROPERTY, isMarkupFixedDesc)

                    If (doReg = True) Then
                        Me.State.sBuffer.Append("isCommFixedArr[" & row.RowIndex & "] = '" & isCommFixedCode & "';")
                        Me.State.sBuffer.AppendLine()
                        If Not entityList.CommissionAmount Is Nothing Then
                            dCommAmount = entityList.CommissionAmount.Value
                        End If
                        Me.State.sBuffer.Append("commAmountArr[" & row.RowIndex & "] = '" & dCommAmount.ToString & "';")
                        Me.State.sBuffer.AppendLine()
                        'Me.State.sBuffer.Append("isMarkupFixedArr[" & row.RowIndex & "] = '" & isMarkupFixedCode & "';")
                        'Me.State.sBuffer.AppendLine()
                        If Not entityList.MarkupAmount Is Nothing Then
                            dMarkupAmount = entityList.MarkupAmount.Value
                        End If
                        Me.State.sBuffer.Append("markupAmountArr[" & row.RowIndex & "] = '" & dMarkupAmount.ToString & "';")
                        Me.State.sBuffer.AppendLine()
                    End If
                    If isCommFixedCode = "N" Then
                        If Not entityList.CommissionAmount Is Nothing Then
                            dCommAmount = entityList.CommissionAmount.Value
                        End If
                        Me.State.totalCF += Math.Round(dCommAmount, 2)
                    End If
                    If isCommFixedCode = "N" Then
                        If Not entityList.MarkupAmount Is Nothing Then
                            dMarkupAmount = entityList.MarkupAmount.Value
                        End If
                        Me.State.totalMF += Math.Round(dMarkupAmount, 2)
                    End If
                    'If isMarkupFixedCode = "N" Then
                    '    If Not entityList.MarkupAmount Is Nothing Then
                    '        dMarkupAmount = entityList.MarkupAmount.Value
                    '    End If
                    '    Me.State.totalMF += Math.Round(dMarkupAmount, 2)
                    'End If
                Next
                CreateClientScripts(doReg)
                If oDataSet.Tables.Contains(CommPCodeEntityListDAL.TABLE_NAME) = False Then
                    entityList = New CommPCodeEntityList(oDataSet)
                End If
                Return oDataSet.Tables(CommPCodeEntityListDAL.TABLE_NAME).DefaultView
                'If oDataSet.Tables.Contains(CommPCodeEntityListDAL.TABLE_NAME) = True Then
                '    Return oDataSet.Tables(CommPCodeEntityListDAL.TABLE_NAME).DefaultView
                'Else
                '    entityList = New CommPCodeEntityList(oDataSet)
                '    Return Nothing
                'End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Function

#End Region


        Private Sub moEntityGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles moEntityGrid.RowEditing

        End Sub
    End Class

End Namespace
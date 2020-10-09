
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables


    Partial Class ProductConversionExtendedForm
        Inherits ElitaPlusPage

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Back
            Public EditingBo As ProductCodeConversion
            Public HasDataChanged As Boolean
            Public Sub New(curEditingBo As ProductCodeConversion, Optional ByVal LastOp As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Back, Optional ByVal hasDataChanged As Boolean = True)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.InputParameters = CType(CallingParameters, Parameters)
                    State.moProductCodeConvId = State.InputParameters.moProductCodeConversionId

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductConversionForm.ReturnType = CType(ReturnPar, ProductConversionForm.ReturnType)

            State.moProductCodeConvId = retObj.ProductCodeConversionId
            SetStateProperties()
            'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
            'Me.MSG_TYPE_CONFIRM, True)
            State.LastOperation = DetailPageCommand.Redirect_

            ' EnableDisableFields()

        End Sub
#End Region

#Region "Page State"

#Region "MyState"
        Class MyState
            Public MyBO As ProductCodeConversion
            Public moProductCodeConvId As Guid = Guid.Empty
            Public IsProductCodeConvNew As Boolean = False
            Public ProductCodeConversionType As String = String.Empty

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False

            Public oDealer As Dealer
            Public ScreenSnapShotBO As ProductCodeConversion

            Public PageIndex As Integer = 0
            Public IsEditMode As Boolean
            Public IsReadOnly As Boolean = False
            Public Action As String
            Public InputParameters As Parameters

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
            'Me.State.moProductCodeId = CType(Me.CallingParameters, Guid)                        
            If State.moProductCodeConvId.Equals(Guid.Empty) Then
                State.IsProductCodeConvNew = True
                ClearAll()
                SetButtonsState(True)
            Else
                State.IsProductCodeConvNew = False
                SetButtonsState(False)
            End If
            PopulateAll()
        End Sub

#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            DealerDropControl.ChangeEnabledControlProperty(bIsNew)
            'ControlMgr.SetEnableControl(Me, moDealerDrop, bIsNew)
            ControlMgr.SetEnableControl(Me, moProductCode, bIsNew)
            ControlMgr.SetEnableControl(Me, txtEffectiveDate, bIsNew)
            ControlMgr.SetEnableControl(Me, btnEffectiveDate, bIsNew)

            
        End Sub

#End Region

#Region "Properties"
        Private ReadOnly Property TheProductConv(Optional ByVal objProd As ProductCodeConversion = Nothing) As ProductCodeConversion
            Get
                '  If objProd Is Nothing Then
                If State.MyBO Is Nothing Then
                    If State.IsProductCodeConvNew = True Then
                        ' For creating, inserting
                        State.MyBO = New ProductCodeConversion
                        State.moProductCodeConvId = State.MyBO.Id
                    Else
                        ' For updating, deleting
                        '  Dim oProductCodeId As Guid = Me.GetGuidFromString(Me.State.moProductCodeId)
                        State.MyBO = New ProductCodeConversion(State.moProductCodeConvId)
                        State.oDealer = New Dealer(State.MyBO.DealerId)
                        State.ProductCodeConversionType = LookupListNew.GetCodeFromId(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_PROD_CONV_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.oDealer.ConvertProductCodeId)

                        'dv = LookupListNew.GetDealerForProductCodeConvLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                        'dv.RowFilter = "DEALER_ID = '" + GuidControl.GuidToHexString(TheProductCodeConversion.DealerId()) + "'"

                        'If dv.Count >= 1 AndAlso (Not dv(0).Item("CONV_CODE") Is DBNull.Value) AndAlso (dv(0).Item("CONV_CODE").ToString.Equals("EXT") Or dv(0).Item("CONV_CODE").ToString.Equals("P")) Then

                    End If
                End If

                Return State.MyBO
            End Get
        End Property

#End Region

#Region "Constants"

        Protected Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const PRODUCT_CODE_CONVERSION_LIST As String = "ProductConversionForm.aspx"
        Public Const URL As String = "ProductConversionExtendedForm.aspx"
        Public Const SESSION_BO As String = "boProductConversion"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        Private Const WITH_MNF_PRODUCT_CODE As String = "WMPCO"
        Private Const LABEL_DEALER As String = "DEALER"

#End Region


#Region "Parameters"
        Public Class Parameters
            Public moProductCodeConversionId As Guid = Nothing
            Public DealerId As Guid = Guid.Empty
            Public ProductcodeId As Guid = Guid.Empty
            Public Productcode As String
            Public ExternalProductcode As String

            Public Sub New(moProductCodeConversionId As Guid, DealerId As Guid, ProductcodeId As Guid, Productcode As String, ExternalProductcode As String)
                Me.moProductCodeConversionId = moProductCodeConversionId
                Me.DealerId = DealerId
                Me.ProductcodeId = ProductcodeId
                Me.Productcode = Productcode
                Me.ExternalProductcode = ExternalProductcode
            End Sub

        End Class
#End Region
#Region "Handlers-Init"

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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Try
                MasterPage.MessageController.Clear_Hide()
                ' ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    AddCalendar(btnEffectiveDate, txtEffectiveDate)
                    AddCalendar(btnExpirationDate, txtExpirationDate)
                    UpdateBreadCrum()
                    SetStateProperties()

                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                                                      MSG_TYPE_CONFIRM, True)
                    '  EnableDisableFields()               
                Else
                    CheckIfComingFromDeleteConfirm()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(TheProductConv)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub

        'Protected Sub CheckIfComingFromSaveConfirm()
        '    Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        '    If Not confResponse Is Nothing AndAlso Microsoft.VisualBasic.IsNumeric(confResponse) Then
        '        Select Case Integer.Parse(confResponse)
        '            Case ElitaPlusPage.DetailPageCommand.Save
        '                If ApplyChanges() Then
        '                    ' Me.ReturnToCallingPage(New ReturnType(TheProductConv, ElitaPlusPage.DetailPageCommand.Save))
        '                    Dim retType As New ProductConversionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
        '                                                   Me.State.moProductCodeConvId, Me.State.InputParameters, Me.State.boChanged)
        '                    Me.ReturnToCallingPage(retType)
        '                End If
        '            Case Else
        '                'If TheProductConv.IsNew Then
        '                GoBack()
        '                'Else
        '                'Me.ReturnToCallingPage(New ReturnType(TheProductConv, ElitaPlusPage.DetailPageCommand.Back))
        '                'End If
        '        End Select
        '    End If
        '    'Clean after consuming the action
        '    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        '    Me.HiddenSaveChangesPromptResponse.Value = ""
        'End Sub


#End Region

#Region "Handlers-Buttons"

        Private Sub btnNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveChanges()

            If ApplyChanges() = True Then
                State.boChanged = True
                If State.IsProductCodeConvNew = True Then
                    State.IsProductCodeConvNew = False
                End If
                'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, _
                '                                     Me.State.moProductCodeConvId, Me.State.InputParameters, Me.State.boChanged))

                Dim retType As New ProductConversionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Save,
                                                            State.moProductCodeConvId, State.InputParameters, State.boChanged)
            End If
        End Sub

        Private Sub GoBack()

            Dim retType As New ProductConversionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                             State.moProductCodeConvId, State.InputParameters, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)

                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    '  If TheProductConv.IsNew Then
                    GoBack()
                    'Else
                    '   Me.ReturnToCallingPage(New ReturnType(TheProductConv, ElitaPlusPage.DetailPageCommand.Back))
                    'End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                SaveChanges()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.IsProductCodeConvNew Then
                    'Reload from the DB
                    State.MyBO = New ProductCodeConversion(State.moProductCodeConvId)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New ProductCodeConversion
                End If
                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            State.ScreenSnapShotBO = Nothing
            State.moProductCodeConvId = Guid.Empty
            State.IsProductCodeConvNew = True
            State.MyBO = New ProductCodeConversion
            ClearAll()
            SetButtonsState(True)
            PopulateAll()
            DealerDropControl.ChangeEnabledControlProperty(True)
        End Sub


        Private Sub CreateNewCopy()

            PopulateBOsFromForm()

            ' Dim newObjDummy As New ProductCode
            Dim newObj As New ProductCodeConversion
            newObj.Copy(TheProductConv)

            State.MyBO = newObj
            'newObjDummy = TheProductCode(newObj)

            State.moProductCodeConvId = Guid.Empty
            State.IsProductCodeConvNew = True

            With TheProductConv '(newObj)
                .ExternalProdCode = Nothing
            End With

            SetButtonsState(True)
            DealerDropControl.ChangeEnabledControlProperty(True)

            'create the backup copy
            State.ScreenSnapShotBO = New ProductCodeConversion
            State.ScreenSnapShotBO.Copy(TheProductConv)

        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteProductCode() = True Then
                    State.boChanged = True

                    Dim retType As New ProductConversionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                            State.moProductCodeConvId, State.InputParameters, State.boChanged)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function DeleteProductCode() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheProductConv
                    .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Dropdowns"

        Private Sub DealerDropChanged(DealerMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) Handles DealerDropControl.SelectedDropChanged
            Try
                PopulateProductCode()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(TheProductConv, "DealerID", DealerDropControl.CaptionLabel)
            BindBOPropertyToLabel(TheProductConv, "ProductCodeId", lblProdCode)
            BindBOPropertyToLabel(TheProductConv, "ExternalProductCode", lbldealerProdCode)
            BindBOPropertyToLabel(TheProductConv, "CertificateDuration", lblCertduration)
            BindBOPropertyToLabel(TheProductConv, "GrossAmount", lblGrossAmt)
            BindBOPropertyToLabel(TheProductConv, "Manufacturer", lblManufacturer)
            BindBOPropertyToLabel(TheProductConv, "ManufacturerWarranty", lblMfgwarranty)
            BindBOPropertyToLabel(TheProductConv, "Model", lblModel)
            BindBOPropertyToLabel(TheProductConv, "SalesPrice", lblSalesPrice)
            BindBOPropertyToLabel(TheProductConv, "EffectiveDate", lblEffectiveDate)
            BindBOPropertyToLabel(TheProductConv, "ExpirationDate", lblExpirationDate)
            ClearGridHeadersAndLabelsErrSign()

        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(DealerDropControl.CaptionLabel)
            ClearLabelErrSign(lblProdCode)
            ClearLabelErrSign(lblCertduration)
            ClearLabelErrSign(lbldealerProdCode)
            ClearLabelErrSign(lblGrossAmt)
            ClearLabelErrSign(lblManufacturer)
            ClearLabelErrSign(lblMfgwarranty)
            ClearLabelErrSign(lblModel)
            ClearLabelErrSign(lblSalesPrice)
            ClearLabelErrSign(lblEffectiveDate)
            ClearLabelErrSign(lblExpirationDate)
        End Sub
#End Region

#Region "Populate"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Product_Conversion")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Product_Conversion")
                End If
            End If
        End Sub

        Private Sub PopulateDealer()

            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                DealerDropControl.SetControl(True, DealerDropControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
                If State.IsProductCodeConvNew = True Then
                    DealerDropControl.SelectedGuid = Guid.Empty
                    DealerDropControl.ChangeEnabledControlProperty(True)
                Else
                    DealerDropControl.ChangeEnabledControlProperty(False)
                    DealerDropControl.SelectedGuid = TheProductConv.DealerId
                    PopulateProductCode()
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateProductCode()
            Dim oDealerId As Guid = DealerDropControl.SelectedGuid
            Try
                ' Me.BindListControlToDataView(Me.moProductCode, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE")
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                Dim ProdLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moProductCode.Populate(ProdLKL, New PopulateOptions() With
                {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
                })
                BindSelectItem(TheProductConv.ProductCodeId.ToString, moProductCode)
            Catch ex As Exception
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateManufacturer()
            Try
                Dim oCompanyGrpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

                '  Me.BindListControlToDataView(moManufacturer,
                'LookupListNew.GetManufacturerLookupList(oCompanyGrpId))
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), ListContext)
                moManufacturer.Populate(manufacturerLkl, New PopulateOptions() With
                {
                .AddBlankItem = True
                })
                If TheProductConv.Manufacturer IsNot Nothing Then
                    SetSelectedItemByText(moManufacturer, TheProductConv.Manufacturer)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateTexts()

            With TheProductConv
                PopulateControlFromBOProperty(txtCertDuration, .CertificateDuration)
                PopulateControlFromBOProperty(txtdealerProdCode, .ExternalProdCode)
                PopulateControlFromBOProperty(txtMfgwarranty, .ManufacturerWarranty)
                'PopulateControlFromBOProperty(Me.moManufacturerText, .Manufacturer)
                PopulateControlFromBOProperty(txtModel, .Model)
                PopulateControlFromBOProperty(txtGrossAmt, .GrossAmount)
                PopulateControlFromBOProperty(txtSalesPrice, .SalesPrice)
                PopulateControlFromBOProperty(txtEffectiveDate,.EffectiveDate)
                PopulateControlFromBOProperty(txtExpirationDate,.ExpirationDate)


            End With

        End Sub

        Private Sub PopulateAll()
            PopulateDealer()
            PopulateManufacturer()
            PopulateTexts()
            EnableDisableFields()
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateBOsFromForm()

            Dim bErr As Boolean = False

            With TheProductConv
                Try
                    ' DropDowns
                    'PopulateBOProperty(TheProductConv, "DealerId", Me.moDealerDrop)
                    .DealerId = DealerDropControl.SelectedGuid
                    PopulateBOProperty(TheProductConv, "ProductCodeId", moProductCode)
                    If Not (GetSelectedDescription(moManufacturer) = String.Empty AndAlso TheProductConv.Manufacturer = Nothing) Then
                        PopulateBOProperty(TheProductConv, "Manufacturer", moManufacturer, False)
                    End If
                    ' Texts
                    PopulateBOProperty(TheProductConv, "ExternalProdCode", txtdealerProdCode)
                    PopulateBOProperty(TheProductConv, "Model", txtModel)
                    PopulateBOProperty(TheProductConv, "CertificateDuration", txtCertDuration)
                    PopulateBOProperty(TheProductConv, "GrossAmount", txtGrossAmt)
                    PopulateBOProperty(TheProductConv, "ManufacturerWarranty", txtMfgwarranty)
                    PopulateBOProperty(TheProductConv, "SalesPrice", txtSalesPrice)
                    PopulateBOProperty(TheProductConv, "EffectiveDate", txtEffectiveDate)
                    PopulateBOProperty(TheProductConv, "ExpirationDate", txtExpirationDate)
                Catch ex As Exception
                    If ErrCollection.Count > 0 Then
                        Throw New PopulateBOErrorException
                    End If
                End Try

            End With
        End Sub

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                With TheProductConv
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With

            Catch ex As Exception
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Page.Validate()
            If Page.IsValid = False Then Return False

            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean = False
            Dim err As String

            If txtdealerProdCode.Text.Trim.Equals("") Then
                err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
                MasterPage.MessageController.AddError(lbldealerProdCode.Text + ": " + err, False)
                bIsOk = False
            End If

            If moProductCode.SelectedItem Is Nothing Then
                err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                MasterPage.MessageController.AddError(lblProdCode.Text + ": " + err, False)
                bIsOk = False
            End If

            If State.ProductCodeConversionType = "P" OrElse State.ProductCodeConversionType = "EXT" Then

                If (Not Microsoft.VisualBasic.IsNumeric(txtCertDuration.Text)) OrElse txtCertDuration.Text.Trim.Equals("") Then
                    err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED)
                    MasterPage.MessageController.AddError(lblCertduration.Text + ": " + err, False)
                    bIsOk = False
                End If
                If (Not Microsoft.VisualBasic.IsNumeric(txtMfgwarranty.Text)) OrElse txtMfgwarranty.Text.Trim.Equals("") Then
                    err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_MANUFACTURER_TERM)
                    MasterPage.MessageController.AddError(lblMfgwarranty.Text + ": " + err, False)
                    bIsOk = False
                End If

            End If

            If State.ProductCodeConversionType = "EXT" Then
                If (Not Microsoft.VisualBasic.IsNumeric(txtGrossAmt.Text)) OrElse txtGrossAmt.Text.Trim.Equals("") Then
                    err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)
                    MasterPage.MessageController.AddError(lblGrossAmt.Text + ": " + err, False)
                    bIsOk = False
                End If
            End If

            If Not String.IsNullOrEmpty(txtExpirationDate.Text.Trim) AndAlso Not String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) Then
                If DateHelper.GetDateValue(txtExpirationDate.Text.Trim) <  DateHelper.GetDateValue(txtEffectiveDate.Text.Trim) Then
                    err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_EXPIRATION_DATE_CAN_NOT_LESS_THAN_EFFECTIVE_DATE)
                    MasterPage.MessageController.AddError(lbldealerProdCode.Text + ": " + err, False)
                    bIsOk = False
                End If
                
            End If

            If bIsOk = False Then
                MasterPage.MessageController.Show()
                Return bIsOk
            End If

            With TheProductConv

                PopulateBOsFromForm()
                TheProductConv.Validate()
                bIsDirty = .IsDirty
                If bIsOk = True Then
                    If bIsDirty = True Then
                        .Save()
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Else
                        MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    End If
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            End With

            Return bIsOk
        End Function

#End Region

#Region "Clear"

        Private Sub ClearTexts()
            txtdealerProdCode.Text = Nothing
            txtCertDuration.Text = Nothing
            txtMfgwarranty.Text = Nothing
            txtGrossAmt.Text = Nothing
            txtModel.Text = Nothing
            txtSalesPrice.Text = Nothing
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            'ClearList(moDealerDrop)
            DealerDropControl.ClearMultipleDrop()
            ClearList(moProductCode)
            ClearList(moManufacturer)
        End Sub

        Protected Sub EnableDisableFields()

            Dim odealer As Dealer
            If State.IsProductCodeConvNew = True AndAlso Not DealerDropControl.SelectedGuid = Guid.Empty Then
                odealer = New Dealer(DealerDropControl.SelectedGuid)
                State.ProductCodeConversionType = LookupListNew.GetCodeFromId(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_PROD_CONV_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), odealer.ConvertProductCodeId)
            End If

            If State.ProductCodeConversionType = "EXT" OrElse State.ProductCodeConversionType = WITH_MNF_PRODUCT_CODE Then
                ControlMgr.SetVisibleControl(Me, trAmountRow, True)
                ControlMgr.SetVisibleControl(Me, trManufacturerRow, True)
                ControlMgr.SetVisibleControl(Me, trModelRow, True)
                ControlMgr.SetVisibleControl(Me, trSalesPriceRow, True)
                ControlMgr.SetVisibleControl(Me, trmfgwarrrow, True)
                ControlMgr.SetVisibleControl(Me, trmfgdurationrow, True)

            ElseIf State.ProductCodeConversionType = "P" Then
                ControlMgr.SetVisibleControl(Me, trAmountRow, False)
                ControlMgr.SetVisibleControl(Me, trManufacturerRow, False)
                ControlMgr.SetVisibleControl(Me, trModelRow, False)
                ControlMgr.SetVisibleControl(Me, trSalesPriceRow, False)
                ControlMgr.SetVisibleControl(Me, trmfgwarrrow, True)
                ControlMgr.SetVisibleControl(Me, trmfgdurationrow, True)

                txtGrossAmt.Text = Nothing
                moManufacturer.SelectedIndex = NO_ITEM_SELECTED_INDEX
                txtModel.Text = Nothing
                txtSalesPrice.Text = Nothing

            Else
                ControlMgr.SetVisibleControl(Me, trmfgwarrrow, False)
                ControlMgr.SetVisibleControl(Me, trmfgdurationrow, False)
                ControlMgr.SetVisibleControl(Me, trAmountRow, False)
                ControlMgr.SetVisibleControl(Me, trManufacturerRow, False)
                ControlMgr.SetVisibleControl(Me, trModelRow, False)
                ControlMgr.SetVisibleControl(Me, trSalesPriceRow, False)


                txtCertDuration.Text = Nothing
                txtMfgwarranty.Text = Nothing
                txtGrossAmt.Text = Nothing
                moManufacturer.SelectedIndex = NO_ITEM_SELECTED_INDEX
                txtModel.Text = Nothing
                txtSalesPrice.Text = Nothing
            End If

        End Sub

#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    State.IsEditMode = False
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                'ReturnProductPolicyFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub

#End Region


    End Class

End Namespace


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
            Public Sub New(ByVal curEditingBo As ProductCodeConversion, Optional ByVal LastOp As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Back, Optional ByVal hasDataChanged As Boolean = True)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.InputParameters = CType(Me.CallingParameters, Parameters)
                    Me.State.moProductCodeConvId = Me.State.InputParameters.moProductCodeConversionId

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductConversionForm.ReturnType = CType(ReturnPar, ProductConversionForm.ReturnType)

            Me.State.moProductCodeConvId = retObj.ProductCodeConversionId
            Me.SetStateProperties()
            'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
            'Me.MSG_TYPE_CONFIRM, True)
            Me.State.LastOperation = DetailPageCommand.Redirect_

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
            If Me.State.moProductCodeConvId.Equals(Guid.Empty) Then
                Me.State.IsProductCodeConvNew = True
                ClearAll()
                SetButtonsState(True)
            Else
                Me.State.IsProductCodeConvNew = False
                SetButtonsState(False)
            End If
            PopulateAll()
        End Sub

#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            DealerDropControl.ChangeEnabledControlProperty(bIsNew)
            'ControlMgr.SetEnableControl(Me, moDealerDrop, bIsNew)
            ControlMgr.SetEnableControl(Me, moProductCode, bIsNew)

        End Sub

#End Region

#Region "Properties"
        Private ReadOnly Property TheProductConv(Optional ByVal objProd As ProductCodeConversion = Nothing) As ProductCodeConversion
            Get
                '  If objProd Is Nothing Then
                If Me.State.MyBO Is Nothing Then
                    If Me.State.IsProductCodeConvNew = True Then
                        ' For creating, inserting
                        Me.State.MyBO = New ProductCodeConversion
                        Me.State.moProductCodeConvId = Me.State.MyBO.Id
                    Else
                        ' For updating, deleting
                        '  Dim oProductCodeId As Guid = Me.GetGuidFromString(Me.State.moProductCodeId)
                        Me.State.MyBO = New ProductCodeConversion(Me.State.moProductCodeConvId)
                        Me.State.oDealer = New Dealer(Me.State.MyBO.DealerId)
                        Me.State.ProductCodeConversionType = LookupListNew.GetCodeFromId(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_PROD_CONV_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.oDealer.ConvertProductCodeId)

                        'dv = LookupListNew.GetDealerForProductCodeConvLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                        'dv.RowFilter = "DEALER_ID = '" + GuidControl.GuidToHexString(TheProductCodeConversion.DealerId()) + "'"

                        'If dv.Count >= 1 AndAlso (Not dv(0).Item("CONV_CODE") Is DBNull.Value) AndAlso (dv(0).Item("CONV_CODE").ToString.Equals("EXT") Or dv(0).Item("CONV_CODE").ToString.Equals("P")) Then

                    End If
                End If

                Return Me.State.MyBO
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

            Public Sub New(ByVal moProductCodeConversionId As Guid, ByVal DealerId As Guid, ByVal ProductcodeId As Guid, ByVal Productcode As String, ByVal ExternalProductcode As String)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                ' ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    Me.SetStateProperties()

                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                                                      Me.MSG_TYPE_CONFIRM, True)
                    '  EnableDisableFields()               
                Else
                    CheckIfComingFromDeleteConfirm()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(TheProductConv)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                Me.MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                Me.State.LastOperation = DetailPageCommand.Nothing_
            Else
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
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
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveChanges()

            If ApplyChanges() = True Then
                Me.State.boChanged = True
                If Me.State.IsProductCodeConvNew = True Then
                    Me.State.IsProductCodeConvNew = False
                End If
                'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, _
                '                                     Me.State.moProductCodeConvId, Me.State.InputParameters, Me.State.boChanged))

                Dim retType As New ProductConversionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Save,
                                                            Me.State.moProductCodeConvId, Me.State.InputParameters, Me.State.boChanged)
            End If
        End Sub

        Private Sub GoBack()

            Dim retType As New ProductConversionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                             Me.State.moProductCodeConvId, Me.State.InputParameters, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)

                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    '  If TheProductConv.IsNew Then
                    GoBack()
                    'Else
                    '   Me.ReturnToCallingPage(New ReturnType(TheProductConv, ElitaPlusPage.DetailPageCommand.Back))
                    'End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                SaveChanges()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.IsProductCodeConvNew Then
                    'Reload from the DB
                    Me.State.MyBO = New ProductCodeConversion(Me.State.moProductCodeConvId)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New ProductCodeConversion
                End If
                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing
            Me.State.moProductCodeConvId = Guid.Empty
            Me.State.IsProductCodeConvNew = True
            Me.State.MyBO = New ProductCodeConversion
            ClearAll()
            Me.SetButtonsState(True)
            Me.PopulateAll()
            DealerDropControl.ChangeEnabledControlProperty(True)
        End Sub


        Private Sub CreateNewCopy()

            Me.PopulateBOsFromForm()

            ' Dim newObjDummy As New ProductCode
            Dim newObj As New ProductCodeConversion
            newObj.Copy(TheProductConv)

            Me.State.MyBO = newObj
            'newObjDummy = TheProductCode(newObj)

            Me.State.moProductCodeConvId = Guid.Empty
            Me.State.IsProductCodeConvNew = True

            With TheProductConv '(newObj)
                .ExternalProdCode = Nothing
            End With

            Me.SetButtonsState(True)
            DealerDropControl.ChangeEnabledControlProperty(True)

            'create the backup copy
            Me.State.ScreenSnapShotBO = New ProductCodeConversion
            Me.State.ScreenSnapShotBO.Copy(TheProductConv)

        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteProductCode() = True Then
                    Me.State.boChanged = True

                    Dim retType As New ProductConversionForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                            Me.State.moProductCodeConvId, Me.State.InputParameters, Me.State.boChanged)
                    retType.BoChanged = True
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Dropdowns"

        Private Sub DealerDropChanged(ByVal DealerMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) Handles DealerDropControl.SelectedDropChanged
            Try
                PopulateProductCode()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(TheProductConv, "DealerID", DealerDropControl.CaptionLabel)
            Me.BindBOPropertyToLabel(TheProductConv, "ProductCodeId", Me.lblProdCode)
            Me.BindBOPropertyToLabel(TheProductConv, "ExternalProductCode", Me.lbldealerProdCode)
            Me.BindBOPropertyToLabel(TheProductConv, "CertificateDuration", Me.lblCertduration)
            Me.BindBOPropertyToLabel(TheProductConv, "GrossAmount", Me.lblGrossAmt)
            Me.BindBOPropertyToLabel(TheProductConv, "Manufacturer", Me.lblManufacturer)
            Me.BindBOPropertyToLabel(TheProductConv, "ManufacturerWarranty", Me.lblMfgwarranty)
            Me.BindBOPropertyToLabel(TheProductConv, "Model", Me.lblModel)
            Me.BindBOPropertyToLabel(TheProductConv, "SalesPrice", Me.lblSalesPrice)
            Me.ClearGridHeadersAndLabelsErrSign()

        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(Me.DealerDropControl.CaptionLabel)
            Me.ClearLabelErrSign(Me.lblProdCode)
            Me.ClearLabelErrSign(Me.lblCertduration)
            Me.ClearLabelErrSign(Me.lbldealerProdCode)
            Me.ClearLabelErrSign(Me.lblGrossAmt)
            Me.ClearLabelErrSign(Me.lblManufacturer)
            Me.ClearLabelErrSign(Me.lblMfgwarranty)
            Me.ClearLabelErrSign(Me.lblModel)
            Me.ClearLabelErrSign(Me.lblSalesPrice)
        End Sub
#End Region

#Region "Populate"

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Product_Conversion")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Product_Conversion")
                End If
            End If
        End Sub

        Private Sub PopulateDealer()

            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                DealerDropControl.SetControl(True, DealerDropControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
                If Me.State.IsProductCodeConvNew = True Then
                    DealerDropControl.SelectedGuid = Guid.Empty
                    DealerDropControl.ChangeEnabledControlProperty(True)
                Else
                    DealerDropControl.ChangeEnabledControlProperty(False)
                    DealerDropControl.SelectedGuid = TheProductConv.DealerId
                    PopulateProductCode()
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateProductCode()
            Dim oDealerId As Guid = DealerDropControl.SelectedGuid
            Try
                ' Me.BindListControlToDataView(Me.moProductCode, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE")
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                Dim ProdLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                Me.moProductCode.Populate(ProdLKL, New PopulateOptions() With
                {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
                })
                BindSelectItem(TheProductConv.ProductCodeId.ToString, Me.moProductCode)
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
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
                If Not TheProductConv.Manufacturer Is Nothing Then
                    SetSelectedItemByText(Me.moManufacturer, TheProductConv.Manufacturer)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateTexts()

            With TheProductConv
                PopulateControlFromBOProperty(Me.txtCertDuration, .CertificateDuration)
                PopulateControlFromBOProperty(Me.txtdealerProdCode, .ExternalProdCode)
                PopulateControlFromBOProperty(Me.txtMfgwarranty, .ManufacturerWarranty)
                'PopulateControlFromBOProperty(Me.moManufacturerText, .Manufacturer)
                PopulateControlFromBOProperty(Me.txtModel, .Model)
                PopulateControlFromBOProperty(Me.txtGrossAmt, .GrossAmount)
                PopulateControlFromBOProperty(Me.txtSalesPrice, .SalesPrice)


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
                    Me.PopulateBOProperty(TheProductConv, "ProductCodeId", Me.moProductCode)
                    If Not (GetSelectedDescription(Me.moManufacturer) = String.Empty And TheProductConv.Manufacturer = Nothing) Then
                        Me.PopulateBOProperty(TheProductConv, "Manufacturer", Me.moManufacturer, False)
                    End If
                    ' Texts
                    Me.txtdealerProdCode.Text = Me.txtdealerProdCode.Text.ToUpper()
                    Me.PopulateBOProperty(TheProductConv, "ExternalProdCode", Me.txtdealerProdCode)
                    Me.PopulateBOProperty(TheProductConv, "Model", Me.txtModel)
                    Me.PopulateBOProperty(TheProductConv, "CertificateDuration", Me.txtCertDuration)
                    Me.PopulateBOProperty(TheProductConv, "GrossAmount", Me.txtGrossAmt)
                    Me.PopulateBOProperty(TheProductConv, "ManufacturerWarranty", Me.txtMfgwarranty)
                    Me.PopulateBOProperty(TheProductConv, "SalesPrice", Me.txtSalesPrice)
                Catch ex As Exception
                    If Me.ErrCollection.Count > 0 Then
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
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Page.Validate()
            If Page.IsValid = False Then Return False

            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean = False
            Dim err As String

            If Me.txtdealerProdCode.Text.Trim.Equals("") Then
                err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
                Me.MasterPage.MessageController.AddError(Me.lbldealerProdCode.Text + ": " + err, False)
                bIsOk = False
            End If

            If Me.moProductCode.SelectedItem Is Nothing Then
                err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                Me.MasterPage.MessageController.AddError(Me.lblProdCode.Text + ": " + err, False)
                bIsOk = False
            End If

            If Me.State.ProductCodeConversionType = "P" Or Me.State.ProductCodeConversionType = "EXT" Then

                If (Not Microsoft.VisualBasic.IsNumeric(Me.txtCertDuration.Text)) Or Me.txtCertDuration.Text.Trim.Equals("") Then
                    err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_FIELD_NUMBER_REQUIRED)
                    Me.MasterPage.MessageController.AddError(Me.lblCertduration.Text + ": " + err, False)
                    bIsOk = False
                End If
                If (Not Microsoft.VisualBasic.IsNumeric(Me.txtMfgwarranty.Text)) Or Me.txtMfgwarranty.Text.Trim.Equals("") Then
                    err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_MANUFACTURER_TERM)
                    Me.MasterPage.MessageController.AddError(Me.lblMfgwarranty.Text + ": " + err, False)
                    bIsOk = False
                End If

            End If

            If Me.State.ProductCodeConversionType = "EXT" Then
                If (Not Microsoft.VisualBasic.IsNumeric(Me.txtGrossAmt.Text)) Or Me.txtGrossAmt.Text.Trim.Equals("") Then
                    err = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)
                    Me.MasterPage.MessageController.AddError(Me.lblGrossAmt.Text + ": " + err, False)
                    bIsOk = False
                End If
            End If


            If bIsOk = False Then
                Me.MasterPage.MessageController.Show()
                Return bIsOk
            End If

            With TheProductConv

                PopulateBOsFromForm()
                TheProductConv.Validate()
                bIsDirty = .IsDirty
                If bIsOk = True Then
                    If bIsDirty = True Then
                        .Save()
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Else
                        Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    End If
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
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
            Me.txtGrossAmt.Text = Nothing
            Me.txtModel.Text = Nothing
            Me.txtSalesPrice.Text = Nothing
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
            If Me.State.IsProductCodeConvNew = True And Not DealerDropControl.SelectedGuid = Guid.Empty Then
                odealer = New Dealer(DealerDropControl.SelectedGuid)
                Me.State.ProductCodeConversionType = LookupListNew.GetCodeFromId(LookupListNew.DropdownLanguageLookupList(LookupListCache.LK_PROD_CONV_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), odealer.ConvertProductCodeId)
            End If

            If Me.State.ProductCodeConversionType = "EXT" Or Me.State.ProductCodeConversionType = WITH_MNF_PRODUCT_CODE Then
                ControlMgr.SetVisibleControl(Me, Me.trAmountRow, True)
                ControlMgr.SetVisibleControl(Me, Me.trManufacturerRow, True)
                ControlMgr.SetVisibleControl(Me, Me.trModelRow, True)
                ControlMgr.SetVisibleControl(Me, Me.trSalesPriceRow, True)
                ControlMgr.SetVisibleControl(Me, Me.trmfgwarrrow, True)
                ControlMgr.SetVisibleControl(Me, Me.trmfgdurationrow, True)

            ElseIf Me.State.ProductCodeConversionType = "P" Then
                ControlMgr.SetVisibleControl(Me, Me.trAmountRow, False)
                ControlMgr.SetVisibleControl(Me, Me.trManufacturerRow, False)
                ControlMgr.SetVisibleControl(Me, Me.trModelRow, False)
                ControlMgr.SetVisibleControl(Me, Me.trSalesPriceRow, False)
                ControlMgr.SetVisibleControl(Me, Me.trmfgwarrrow, True)
                ControlMgr.SetVisibleControl(Me, Me.trmfgdurationrow, True)

                Me.txtGrossAmt.Text = Nothing
                moManufacturer.SelectedIndex = NO_ITEM_SELECTED_INDEX
                Me.txtModel.Text = Nothing
                Me.txtSalesPrice.Text = Nothing

            Else
                ControlMgr.SetVisibleControl(Me, Me.trmfgwarrrow, False)
                ControlMgr.SetVisibleControl(Me, Me.trmfgdurationrow, False)
                ControlMgr.SetVisibleControl(Me, Me.trAmountRow, False)
                ControlMgr.SetVisibleControl(Me, Me.trManufacturerRow, False)
                ControlMgr.SetVisibleControl(Me, Me.trModelRow, False)
                ControlMgr.SetVisibleControl(Me, Me.trSalesPriceRow, False)


                txtCertDuration.Text = Nothing
                txtMfgwarranty.Text = Nothing
                Me.txtGrossAmt.Text = Nothing
                moManufacturer.SelectedIndex = NO_ITEM_SELECTED_INDEX
                Me.txtModel.Text = Nothing
                Me.txtSalesPrice.Text = Nothing
            End If

        End Sub

#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case Me.MSG_VALUE_NO
                        GoBack()
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
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            CreateNew()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    Me.State.IsEditMode = False
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                'ReturnProductPolicyFromEditing()
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
        End Sub

#End Region


    End Class

End Namespace

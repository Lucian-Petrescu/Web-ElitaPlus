'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/9/2004)  ********************

Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Certificates

    Partial Class CertAddNewItemForm
        Inherits ElitaPlusPage
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moCertificateInfoController As UserControlCertificateInfo


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"

#End Region

#Region "Parameters"
        Public Class Parameters
            Public CertId As Guid

            Public Sub New(certid As Guid)
                Me.CertId = certid
            End Sub
        End Class
#End Region
#Region "Variables"

        Private mbIsFirstPass As Boolean = True

#End Region

#Region "Attributes"

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Certificate
            Public BoChanged As Boolean = False
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Certificate, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.BoChanged = boChanged
            End Sub
        End Class
#End Region

#Region "Properties"

        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State._moCertificate.CompanyId)

                Return companyBO.Code
            End Get

        End Property


#End Region

#Region "Page State"


        Class MyState
            Public MyBO As CertItem
            Public CertItemCovBO As CertItemCoverage
            Public ScreenSnapShotBO As CertItem
            Public certificateId As Guid
            Public certificateCompanyId As Guid
            Public certificateItemId As Guid
            Public certificateCoverageId As Guid
            Public coverageInEffect As Boolean
            Public IdentificationNumber As Boolean
            Public activeOrPendingClaim As Boolean = False
            Public waitingPeriod As Boolean
            Public suspendedCertificate As Boolean
            Public IsDaysLimitExceeded As Boolean
            Public boChanged As Boolean = False
            Public companyCode As String

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String


            Public _moCertItemCoverage As CertItemCoverage
            Public _moCertificate As Certificate
            Public _moContract As Contract
            Public _moVSCModel As VSCModel
            Public _moVSCClassCode As VSCClassCode
            Public _modealer As Dealer
            Public _IsEdit As Boolean
        End Class

        Public Property moCertItemCoverage() As CertItemCoverage
            Get
                Return State._moCertItemCoverage
            End Get
            Set(Value As CertItemCoverage)
                State._moCertItemCoverage = Value
            End Set
        End Property

        Public Property moCertificate() As Certificate
            Get
                Return State._moCertificate
            End Get
            Set(Value As Certificate)
                State._moCertificate = Value
            End Set
        End Property

        Public Property moContract() As Contract
            Get
                Return State._moContract
            End Get
            Set(Value As Contract)
                State._moContract = Value
            End Set
        End Property

        Public Property IsEdit() As Boolean
            Get
                Return State._IsEdit
            End Get
            Set(Value As Boolean)
                State._IsEdit = Value
            End Set
        End Property

        Public Property moDealer() As Dealer
            Get
                Return State._modealer
            End Get
            Set(Value As Dealer)
                State._modealer = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    Me.State.MyBO = New CertItem
                    Me.State.CertItemCovBO = Me.State.MyBO.AddNewItemCoverage
                    Me.State.MyBO.CertId = CType(NavController.ParametersPassed, Parameters).CertId
                    'DEF-1766 start
                    Me.State.MyBO.CertProductCode = Me.State.MyBO.Cert.ProductCode
                    'DEF-1766 end
                    moCertificate = (CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
                    Me.State.certificateId = moCertificate.Id
                    Me.State.companyCode = GetCompanyCode
                    If Me.State.MyBO IsNot Nothing Then
                        Me.State.MyBO.BeginEdit()
                    End If
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.boChanged = False
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub


#End Region

#Region "Page Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        CreateNew()
                    End If
                    Trace(Me, "CertItem Id=" & GuidControl.GuidToHexString(State.MyBO.Id))
                    AddCalendar(ImageButtonEndDate, TextboxEndDate)

                    State.companyCode = GetCompanyCode
                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                    AddLabelDecorations(State.CertItemCovBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub
#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                btnBack.Enabled = True

                ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
                TextboxBeginDate.ReadOnly = True
                TextboxDateAdded.ReadOnly = True
                TextboxMethodOfRepair.ReadOnly = True
                TextboxProductCode.ReadOnly = True
                TextboxDeductible.ReadOnly = True
                TextboxComputeDeductible.ReadOnly = True
                TextboxDeductiblePercent.ReadOnly = True
                TextboxLiabilityLimit.ReadOnly = True

                TextboxRepairDiscountPct.ReadOnly = True
                TextboxReplacementDiscountPct.ReadOnly = True

                TextboxInvNum.ReadOnly = True

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                BindBOPropertyToLabel(State.MyBO, "RiskTypeId", LabelRiskType)
                BindBOPropertyToLabel(State.MyBO, "ManufacturerId", LabelMakeId)
                BindBOPropertyToLabel(State.MyBO, "SerialNumber", LabelSerialNumber)
                BindBOPropertyToLabel(State.MyBO, "Model", LabelModel)
                BindBOPropertyToLabel(State.MyBO, "DealerItemDesc", LabelDealerItemDesc)
                BindBOPropertyToLabel(State.CertItemCovBO, "BeginDate", LabelBeginDate)
                BindBOPropertyToLabel(State.CertItemCovBO, "EndDate", LabelEndDate)
                BindBOPropertyToLabel(State.CertItemCovBO, "CoverageTypeId", LabelCoverageType)
                ClearGridHeadersAndLabelsErrSign()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub PopulateDropdowns()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
            Try
                Dim dvCov As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetMainItemCoverages(State.certificateId)
                Dim listcontextForRiskTypeList As ListContext = New ListContext()
                listcontextForRiskTypeList.CompanyGroupId = compGroupId
                Dim risktypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForRiskTypeList)

                cboRiskTypeId.Populate(risktypeLkl, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .SortFunc = AddressOf .GetDescription
                    })
                'Me.BindListControlToDataView(Me.cboRiskTypeId, LookupListNew.GetRiskTypeLookupList(compGroupId))
                Dim listcontextForCoverageList As ListContext = New ListContext()
                listcontextForCoverageList.CertId = State.certificateId
                Dim coverageLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetCoverageByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForCoverageList)

                cboCoverageType.Populate(coverageLkl, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .SortFunc = AddressOf .GetDescription
                    })
                'Me.BindListControlToDataView(Me.cboCoverageType, dvCov, "coverage_type_description", "cert_item_coverage_id")
                Dim listcontextForMakeList As ListContext = New ListContext()
                listcontextForMakeList.CompanyGroupId = compGroupId
                Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForMakeList)

                cboManufacturerId.Populate(manufacturerLkl, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .SortFunc = AddressOf .GetDescription
                    })
                'Me.BindListControlToDataView(Me.cboManufacturerId, LookupListNew.GetManufacturerLookupList(compGroupId))
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            Dim riskTypeDesc As String
            Dim manufacturerDesc As String
            Dim todayDate As Date
            Dim dvMethodOfRepair As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Try
                moCertificateInfoController = UserCertificateCtr
                moCertificateInfoController.InitController(State.certificateId, , State.companyCode)
                With State.MyBO
                    PopulateControlFromBOProperty(TextboxMethodOfRepair, getMethodOfRepairDesc)
                    PopulateControlFromBOProperty(TextboxInvNum, moCertificate.InvoiceNumber)
                    PopulateControlFromBOProperty(TextboxProductCode, moCertificate.ProductCode)
                    PopulateControlFromBOProperty(TextboxDateAdded, GetDateFormattedString(DateTime.Today))
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Public Function getMethodOfRepairDesc() As String
            Dim dvPrdId As DataView
            Dim oPrdCodeId As Guid, MethodOfRepairDesc As String
            dvPrdId = ProductCode.GetProductCodeId(moCertificate.DealerId, moCertificate.ProductCode)
            oPrdCodeId = New Guid(CType(dvPrdId(0)(0), Byte()))
            Dim oPrd As New ProductCode(oPrdCodeId)
            'Dim i As Integer

            Dim mthdofrepairLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MethodOfRepairForRepairs", Thread.CurrentPrincipal.GetLanguageCode())
            Dim lstItem As ListItem?
            lstItem = mthdofrepairLkl.Where(Function(lstItm) lstItm.ListItemId = oPrd.MethodOfRepairId).FirstOrDefault()
            If lstItem IsNot Nothing Then
                MethodOfRepairDesc = lstItem.Value.Description
            End If

            'Dim dv As DataView = LookupListNew.GetMethodOfRepairForRepairsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'For i = 0 To dv.Count - 1
            '    If New Guid(CType(dv(i)(LookupListNew.COL_ID_NAME), Byte())).Equals(oPrd.MethodOfRepairId) Then
            '        MethodOfRepairDesc = CType(dv(i)(LookupListNew.COL_DESCRIPTION_NAME), String)
            '        Exit For
            '    End If
            'Next
            Return MethodOfRepairDesc

        End Function

        Protected Sub PopulateBOsFormFrom()
            Dim oItemNumber As Long, CoverageTypeDesc As String, oCoverageTypeId As Guid
            Try
                With State.MyBO
                    PopulateBOProperty(State.MyBO, "CertId", moCertificate.Id)
                    PopulateBOProperty(State.MyBO, "CompanyId", moCertificate.CompanyId)
                    PopulateBOProperty(State.MyBO, "RiskTypeId", cboRiskTypeId)
                    PopulateBOProperty(State.MyBO, "ManufacturerId", cboManufacturerId)
                    PopulateBOProperty(State.MyBO, "SerialNumber", TextboxSerialNumber)
                    oItemNumber = State.MyBO.GetNextItemNumber(moCertificate.Id)
                    PopulateBOProperty(State.MyBO, "ItemNumber", oItemNumber.ToString)

                    PopulateBOProperty(State.MyBO, "Model", TextboxModel)
                    PopulateBOProperty(State.MyBO, "ItemDescription", TextboxDealerItemDesc)
                    'DEF-1766 Start To save product code value with item
                    PopulateBOProperty(State.MyBO, "ProductCode", TextboxProductCode)
                    'DEF-1766 end
                End With
                With State.CertItemCovBO
                    PopulateBOProperty(State.CertItemCovBO, "CertItemId", State.MyBO.Id)
                    CoverageTypeDesc = GetSelectedDescription(cboCoverageType)
                    oCoverageTypeId = LookupListNew.GetIdFromDescription(LookupListCache.LK_COVERAGE_TYPES, CoverageTypeDesc)
                    PopulateBOProperty(State.CertItemCovBO, "CoverageTypeId", oCoverageTypeId)
                    PopulateBOProperty(State.CertItemCovBO, "BeginDate", TextboxBeginDate)
                    PopulateBOProperty(State.CertItemCovBO, "EndDate", TextboxEndDate)
                    PopulateBOProperty(State.CertItemCovBO, "CertId", moCertificate.Id)
                    PopulateBOProperty(State.CertItemCovBO, "CompanyId", moCertificate.CompanyId)
                    PopulateBOProperty(State.CertItemCovBO, "LiabilityLimits", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Deductible", "0")
                    PopulateBOProperty(State.CertItemCovBO, "GrossAmtReceived", "0")
                    PopulateBOProperty(State.CertItemCovBO, "PremiumWritten", "0")
                    PopulateBOProperty(State.CertItemCovBO, "OriginalPremium", "0")
                    PopulateBOProperty(State.CertItemCovBO, "LossCost", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Commission", "0")
                    PopulateBOProperty(State.CertItemCovBO, "AdminExpense", "0")
                    PopulateBOProperty(State.CertItemCovBO, "MarketingExpense", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Other", "0")
                    PopulateBOProperty(State.CertItemCovBO, "SalesTax", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Tax1", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Tax2", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Tax3", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Tax4", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Tax5", "0")
                    PopulateBOProperty(State.CertItemCovBO, "Tax6", "0")
                    PopulateBOProperty(State.CertItemCovBO, "MtdPayments", "0")
                    PopulateBOProperty(State.CertItemCovBO, "YtdPayments", "0")
                    PopulateBOProperty(State.CertItemCovBO, "AssurantGwp", "0")
                    PopulateBOProperty(State.CertItemCovBO, "MarkupCommission", "0")
                    PopulateBOProperty(State.CertItemCovBO, "RepairDiscountPct", "0")
                    PopulateBOProperty(State.CertItemCovBO, "ReplacementDiscountPct", "0")
                    PopulateBOProperty(State.CertItemCovBO, "DeductiblePercent", "0")
                    PopulateBOProperty(State.CertItemCovBO, "DeductibleBasedOnId", LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                End With
                State.MyBO.CertItemCoverageId = State.CertItemCovBO.Id
                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
        Private Sub cboCoverageType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCoverageType.SelectedIndexChanged
            Dim i As Integer
            TextboxBeginDate.Text = String.Empty
            TextboxEndDate.Text = String.Empty
            TextboxDeductible.Text = String.Empty
            TextboxComputeDeductible.Text = String.Empty
            TextboxDeductiblePercent.Text = String.Empty
            TextboxRepairDiscountPct.Text = String.Empty
            TextboxReplacementDiscountPct.Text = String.Empty
            TextboxLiabilityLimit.Text = String.Empty

            Dim dvCov As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetItemCoverages(State.certificateId)
            For i = 0 To dvCov.Count - 1
                If New Guid(CType(dvCov(i)("cert_item_coverage_id"), Byte())).Equals(GetSelectedItem(cboCoverageType)) Then
                    PopulateControlFromBOProperty(TextboxBeginDate, GetDateFormattedStringNullable(New DateType(CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_BEGIN_DATE), Date))))
                    PopulateControlFromBOProperty(TextboxEndDate, GetDateFormattedStringNullable(New DateType(CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_END_DATE), Date))))
                    PopulateControlFromBOProperty(TextboxLiabilityLimit, CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_LIABILITY_LIMITS), Decimal), DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(TextboxDeductible, CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE), Decimal), DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(TextboxComputeDeductible, LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, New Guid(GetGuidStringFromByteArray(CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID), Byte())))))
                    PopulateControlFromBOProperty(TextboxDeductiblePercent, CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT), Decimal), DECIMAL_FORMAT)
                    PopulateControlFromBOProperty(TextboxRepairDiscountPct, dvCov(i)(CertItemCoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT))
                    PopulateControlFromBOProperty(TextboxReplacementDiscountPct, dvCov(i)(CertItemCoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT))
                    Exit For
                End If
            Next
        End Sub

        Protected Sub CreateNew()
            ErrorCtrl.Clear_Hide()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = Nothing
            State.CertItemCovBO = Nothing
            State.MyBO = New CertItem
            State.CertItemCovBO = State.MyBO.AddNewItemCoverage
            NavController.State = Nothing
            State.MyBO.BeginEdit()
        End Sub

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If State IsNot Nothing Then
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Sub ClearFields()
            TextboxBeginDate.Text = String.Empty
            TextboxEndDate.Text = String.Empty
            TextboxModel.Text = String.Empty
            TextboxDealerItemDesc.Text = String.Empty
            TextboxSerialNumber.Text = String.Empty
            TextboxDeductible.Text = String.Empty
            TextboxComputeDeductible.Text = String.Empty
            TextboxDeductiblePercent.Text = String.Empty
            TextboxRepairDiscountPct.Text = String.Empty
            TextboxReplacementDiscountPct.Text = String.Empty
            TextboxLiabilityLimit.Text = String.Empty
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Try
                Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        'Me.CreateNew()
                        PopulateBOsFormFrom()
                        State.CertItemCovBO.Validate()
                        State.MyBO.SaveItem()
                    End If
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State._moCertificate, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State._moCertificate, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State._moCertificate, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_OK Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State._moCertificate, State.boChanged)
                            NavController.Navigate(Me, "back", retObj)
                    End Select
                End If
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub


#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.Cert, State.boChanged)
                    NavController.Navigate(Me, "back", retObj)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrorCtrl.Text
            End Try

        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                PopulateBOsFormFrom()
                State.CertItemCovBO.Validate()
                If State.MyBO.IsDirty Then
                    State.MyBO.SaveItem()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    State.MyBO.EndEdit()
                    State.boChanged = True
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO, HiddenSaveChangesPromptResponse)
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = Me.State.MyBO.CertItemCoverageId
                    'Me.NavController.Navigate(Me, "save")
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = Me.State.MyBO.CertItemCoverageId
                    'Me.NavController.Navigate(Me, "back")
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Save, State.MyBO.Cert, State.boChanged)
                    NavController.Navigate(Me, "back", retObj)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)

            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                ClearFields()
                CreateNew()
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region


    End Class
End Namespace


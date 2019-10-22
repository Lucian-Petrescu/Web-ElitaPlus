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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

            Public Sub New(ByVal certid As Guid)
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Certificate, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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
                Dim companyBO As Company = New Company(Me.State._moCertificate.CompanyId)

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
                Return Me.State._moCertItemCoverage
            End Get
            Set(ByVal Value As CertItemCoverage)
                Me.State._moCertItemCoverage = Value
            End Set
        End Property

        Public Property moCertificate() As Certificate
            Get
                Return Me.State._moCertificate
            End Get
            Set(ByVal Value As Certificate)
                Me.State._moCertificate = Value
            End Set
        End Property

        Public Property moContract() As Contract
            Get
                Return Me.State._moContract
            End Get
            Set(ByVal Value As Contract)
                Me.State._moContract = Value
            End Set
        End Property

        Public Property IsEdit() As Boolean
            Get
                Return Me.State._IsEdit
            End Get
            Set(ByVal Value As Boolean)
                Me.State._IsEdit = Value
            End Set
        End Property

        Public Property moDealer() As Dealer
            Get
                Return Me.State._modealer
            End Get
            Set(ByVal Value As Dealer)
                Me.State._modealer = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    Me.State.MyBO = New CertItem
                    Me.State.CertItemCovBO = Me.State.MyBO.AddNewItemCoverage
                    Me.State.MyBO.CertId = CType(Me.NavController.ParametersPassed, Parameters).CertId
                    'DEF-1766 start
                    Me.State.MyBO.CertProductCode = Me.State.MyBO.Cert.ProductCode
                    'DEF-1766 end
                    moCertificate = (CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate))
                    Me.State.certificateId = moCertificate.Id
                    Me.State.companyCode = GetCompanyCode
                    If Not Me.State.MyBO Is Nothing Then
                        Me.State.MyBO.BeginEdit()
                    End If
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.boChanged = False
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub


#End Region

#Region "Page Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    If Me.State.MyBO Is Nothing Then
                        Me.CreateNew()
                    End If
                    Trace(Me, "CertItem Id=" & GuidControl.GuidToHexString(Me.State.MyBO.Id))
                    Me.AddCalendar(Me.ImageButtonEndDate, Me.TextboxEndDate)

                    Me.State.companyCode = GetCompanyCode
                    PopulateDropdowns()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                    Me.AddLabelDecorations(Me.State.CertItemCovBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub
#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            Try
                Me.btnBack.Enabled = True

                ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
                Me.TextboxBeginDate.ReadOnly = True
                Me.TextboxDateAdded.ReadOnly = True
                Me.TextboxMethodOfRepair.ReadOnly = True
                Me.TextboxProductCode.ReadOnly = True
                Me.TextboxDeductible.ReadOnly = True
                Me.TextboxComputeDeductible.ReadOnly = True
                Me.TextboxDeductiblePercent.ReadOnly = True
                Me.TextboxLiabilityLimit.ReadOnly = True

                Me.TextboxRepairDiscountPct.ReadOnly = True
                Me.TextboxReplacementDiscountPct.ReadOnly = True

                Me.TextboxInvNum.ReadOnly = True

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RiskTypeId", Me.LabelRiskType)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ManufacturerId", Me.LabelMakeId)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "SerialNumber", Me.LabelSerialNumber)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "Model", Me.LabelModel)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerItemDesc", Me.LabelDealerItemDesc)
                Me.BindBOPropertyToLabel(Me.State.CertItemCovBO, "BeginDate", Me.LabelBeginDate)
                Me.BindBOPropertyToLabel(Me.State.CertItemCovBO, "EndDate", Me.LabelEndDate)
                Me.BindBOPropertyToLabel(Me.State.CertItemCovBO, "CoverageTypeId", Me.LabelCoverageType)
                Me.ClearGridHeadersAndLabelsErrSign()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub PopulateDropdowns()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
            Try
                Dim dvCov As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetMainItemCoverages(Me.State.certificateId)
                Dim listcontextForRiskTypeList As ListContext = New ListContext()
                listcontextForRiskTypeList.CompanyGroupId = compGroupId
                Dim risktypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForRiskTypeList)

                Me.cboRiskTypeId.Populate(risktypeLkl, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .SortFunc = AddressOf .GetDescription
                    })
                'Me.BindListControlToDataView(Me.cboRiskTypeId, LookupListNew.GetRiskTypeLookupList(compGroupId))
                Dim listcontextForCoverageList As ListContext = New ListContext()
                listcontextForCoverageList.CertId = Me.State.certificateId
                Dim coverageLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetCoverageByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForCoverageList)

                Me.cboCoverageType.Populate(coverageLkl, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .SortFunc = AddressOf .GetDescription
                    })
                'Me.BindListControlToDataView(Me.cboCoverageType, dvCov, "coverage_type_description", "cert_item_coverage_id")
                Dim listcontextForMakeList As ListContext = New ListContext()
                listcontextForMakeList.CompanyGroupId = compGroupId
                Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForMakeList)

                Me.cboManufacturerId.Populate(manufacturerLkl, New PopulateOptions() With
                    {
                       .AddBlankItem = True,
                       .SortFunc = AddressOf .GetDescription
                    })
                'Me.BindListControlToDataView(Me.cboManufacturerId, LookupListNew.GetManufacturerLookupList(compGroupId))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub PopulateFormFromBOs()
            Dim riskTypeDesc As String
            Dim manufacturerDesc As String
            Dim todayDate As Date
            Dim dvMethodOfRepair As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Try
                moCertificateInfoController = Me.UserCertificateCtr
                moCertificateInfoController.InitController(Me.State.certificateId, , Me.State.companyCode)
                With Me.State.MyBO
                    Me.PopulateControlFromBOProperty(Me.TextboxMethodOfRepair, getMethodOfRepairDesc)
                    Me.PopulateControlFromBOProperty(Me.TextboxInvNum, moCertificate.InvoiceNumber)
                    Me.PopulateControlFromBOProperty(Me.TextboxProductCode, moCertificate.ProductCode)
                    Me.PopulateControlFromBOProperty(Me.TextboxDateAdded, GetDateFormattedString(DateTime.Today))
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
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
            If Not lstItem Is Nothing Then
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
                With Me.State.MyBO
                    Me.PopulateBOProperty(Me.State.MyBO, "CertId", Me.moCertificate.Id)
                    Me.PopulateBOProperty(Me.State.MyBO, "CompanyId", Me.moCertificate.CompanyId)
                    Me.PopulateBOProperty(Me.State.MyBO, "RiskTypeId", Me.cboRiskTypeId)
                    Me.PopulateBOProperty(Me.State.MyBO, "ManufacturerId", Me.cboManufacturerId)
                    Me.PopulateBOProperty(Me.State.MyBO, "SerialNumber", Me.TextboxSerialNumber)
                    oItemNumber = Me.State.MyBO.GetNextItemNumber(Me.moCertificate.Id)
                    Me.PopulateBOProperty(Me.State.MyBO, "ItemNumber", oItemNumber.ToString)

                    Me.PopulateBOProperty(Me.State.MyBO, "Model", Me.TextboxModel)
                    Me.PopulateBOProperty(Me.State.MyBO, "ItemDescription", Me.TextboxDealerItemDesc)
                    'DEF-1766 Start To save product code value with item
                    Me.PopulateBOProperty(Me.State.MyBO, "ProductCode", Me.TextboxProductCode)
                    'DEF-1766 end
                End With
                With Me.State.CertItemCovBO
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "CertItemId", Me.State.MyBO.Id)
                    CoverageTypeDesc = Me.GetSelectedDescription(Me.cboCoverageType)
                    oCoverageTypeId = LookupListNew.GetIdFromDescription(LookupListCache.LK_COVERAGE_TYPES, CoverageTypeDesc)
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "CoverageTypeId", oCoverageTypeId)
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "BeginDate", Me.TextboxBeginDate)
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "EndDate", Me.TextboxEndDate)
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "CertId", Me.moCertificate.Id)
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "CompanyId", Me.moCertificate.CompanyId)
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "LiabilityLimits", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Deductible", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "GrossAmtReceived", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "PremiumWritten", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "OriginalPremium", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "LossCost", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Commission", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "AdminExpense", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "MarketingExpense", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Other", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "SalesTax", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Tax1", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Tax2", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Tax3", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Tax4", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Tax5", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "Tax6", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "MtdPayments", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "YtdPayments", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "AssurantGwp", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "MarkupCommission", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "RepairDiscountPct", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "ReplacementDiscountPct", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "DeductiblePercent", "0")
                    Me.PopulateBOProperty(Me.State.CertItemCovBO, "DeductibleBasedOnId", LookupListNew.GetIdFromCode(LookupListNew.LK_DEDUCTIBLE_BASED_ON, "FIXED"))
                End With
                Me.State.MyBO.CertItemCoverageId = Me.State.CertItemCovBO.Id
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
        Private Sub cboCoverageType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCoverageType.SelectedIndexChanged
            Dim i As Integer
            TextboxBeginDate.Text = String.Empty
            TextboxEndDate.Text = String.Empty
            TextboxDeductible.Text = String.Empty
            TextboxComputeDeductible.Text = String.Empty
            TextboxDeductiblePercent.Text = String.Empty
            TextboxRepairDiscountPct.Text = String.Empty
            TextboxReplacementDiscountPct.Text = String.Empty
            TextboxLiabilityLimit.Text = String.Empty

            Dim dvCov As CertItemCoverage.CertItemCoverageSearchDV = CertItemCoverage.GetItemCoverages(Me.State.certificateId)
            For i = 0 To dvCov.Count - 1
                If New Guid(CType(dvCov(i)("cert_item_coverage_id"), Byte())).Equals(Me.GetSelectedItem(cboCoverageType)) Then
                    Me.PopulateControlFromBOProperty(Me.TextboxBeginDate, GetDateFormattedStringNullable(New DateType(CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_BEGIN_DATE), Date))))
                    Me.PopulateControlFromBOProperty(Me.TextboxEndDate, GetDateFormattedStringNullable(New DateType(CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_END_DATE), Date))))
                    Me.PopulateControlFromBOProperty(Me.TextboxLiabilityLimit, CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_LIABILITY_LIMITS), Decimal), Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.TextboxDeductible, CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE), Decimal), Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.TextboxComputeDeductible, LookupListNew.GetDescriptionFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, New Guid(GetGuidStringFromByteArray(CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID), Byte())))))
                    Me.PopulateControlFromBOProperty(Me.TextboxDeductiblePercent, CType(dvCov(i)(CertItemCoverageDAL.COL_NAME_DEDUCTIBLE_PERCENT), Decimal), Me.DECIMAL_FORMAT)
                    Me.PopulateControlFromBOProperty(Me.TextboxRepairDiscountPct, dvCov(i)(CertItemCoverageDAL.COL_NAME_REPAIR_DISCOUNT_PCT))
                    Me.PopulateControlFromBOProperty(Me.TextboxReplacementDiscountPct, dvCov(i)(CertItemCoverageDAL.COL_NAME_REPLACEMENT_DISCOUNT_PCT))
                    Exit For
                End If
            Next
        End Sub

        Protected Sub CreateNew()
            Me.ErrorCtrl.Clear_Hide()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = Nothing
            Me.State.CertItemCovBO = Nothing
            Me.State.MyBO = New CertItem
            Me.State.CertItemCovBO = Me.State.MyBO.AddNewItemCoverage
            Me.NavController.State = Nothing
            Me.State.MyBO.BeginEdit()
        End Sub

        ' Clean Popup Input
        Private Sub CleanPopupInput()
            Try
                If Not Me.State Is Nothing Then
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenSaveChangesPromptResponse.Value = ""
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
                Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
                If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                    If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        'Me.CreateNew()
                        Me.PopulateBOsFormFrom()
                        Me.State.CertItemCovBO.Validate()
                        Me.State.MyBO.SaveItem()
                    End If
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.boChanged = True
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State._moCertificate, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State._moCertificate, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State._moCertificate, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_OK Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State._moCertificate, Me.State.boChanged)
                            Me.NavController.Navigate(Me, "back", retObj)
                    End Select
                End If
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub


#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.Cert, Me.State.boChanged)
                    Me.NavController.Navigate(Me, "back", retObj)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrorCtrl.Text
            End Try

        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Me.PopulateBOsFormFrom()
                Me.State.CertItemCovBO.Validate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.SaveItem()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    Me.State.MyBO.EndEdit()
                    Me.State.boChanged = True
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = Me.State.MyBO.CertItemCoverageId
                    'Me.NavController.Navigate(Me, "save")
                    'Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = Me.State.MyBO.CertItemCoverageId
                    'Me.NavController.Navigate(Me, "back")
                    Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Save, Me.State.MyBO.Cert, Me.State.boChanged)
                    Me.NavController.Navigate(Me, "back", retObj)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)

            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                Me.ClearFields()
                Me.CreateNew()
                Me.PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region


    End Class
End Namespace


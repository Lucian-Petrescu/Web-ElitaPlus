'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/2/2004)  ********************
Imports Codes = Assurant.ElitaPlus.BusinessObjectsNew.Codes




Partial Class RepairAndLogisticsForm
    Inherits ElitaPlusSearchPage

#Region "Variables"
    Private mbIsFirstPass As Boolean = True

    'Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
    Protected WithEvents LabelDaysLimitExceeded As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancelEdit_WRITE As System.Web.UI.WebControls.Button
    Protected WithEvents ServiceCenterPanel3 As System.Web.UI.WebControls.Panel

#End Region

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

    Public Const URL As String = "~/Claims/RepairAndLogisticsForm.aspx"
    Public ds As New DataSet
    Public params As New ArrayList
    Private Const TelefonicaArgentina As String = "TAR"
    Private Const ArgentinaVerificationNum As Integer = 540000000
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const CLAIMS As String = "CLAIMS"
    Private Const REPAIR_AND_LOGISTICS_DETAILS As String = "REPAIR_AND_LOGISTICS_DETAILS"
#End Region
#Region "Parameters"

    Public Class Parameters
        Private _moClaimId As Guid
        Public Property ClaimId() As Guid
            Get
                Return _moClaimId
            End Get
            Set(ByVal value As Guid)
                _moClaimId = value
            End Set
        End Property
        Private _moAuthorizationId As Guid
        Public Property AuthorizationId() As Guid
            Get
                Return _moAuthorizationId
            End Get
            Set(ByVal value As Guid)
                _moAuthorizationId = value
            End Set
        End Property

        Private _SelectedLvl As SelectedLevel
        Public Property selectedLvl() As SelectedLevel
            Get
                Return _SelectedLvl
            End Get
            Set(ByVal value As SelectedLevel)
                _SelectedLvl = value
            End Set
        End Property



    End Class

    Public Enum SelectedLevel
        Claim = 1
        Authorization = 2

    End Enum


#End Region

#Region "Page State"
    Class MyState
        Public MyBO As RepairAndLogistics
        Public claimId As Guid = Guid.Empty
        Public authorizationId As Guid = Guid.Empty
        Public IsEditMode As Boolean = False
        Public claimAuthorizationType As ClaimAuthorizationType
        Public selectedLvl As SelectedLevel



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
        Dim param As Parameters
        Try

            If Not Me.CallingParameters Is Nothing Then
                'Dim type = CallingPar.GetType()
                If CallingPar.GetType() Is GetType(Parameters) Then
                    param = CType(CallingPar, Parameters)
                    Me.State.MyBO = New RepairAndLogistics(param.ClaimId)
                    Me.State.claimId = param.ClaimId
                    Me.State.authorizationId = param.AuthorizationId
                    Me.State.selectedLvl = param.selectedLvl
                    'Else
                    '    Me.State.MyBO = New RepairAndLogistics(CType(CType(CallingPar, ArrayList)(0), Guid))
                    '    Me.State.claimId = CType(CType(CallingPar, ArrayList)(0), Guid)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Page Events"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State.MyBO Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage(REPAIR_AND_LOGISTICS_DETAILS)
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim claimAuthorizationBO As Assurant.ElitaPlus.BusinessObjectsNew.ClaimAuthorization
        Dim cssClassName As String

        Try
            Dim claimBO As ClaimBase

            Me.MasterPage.MessageController.Clear()

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CLAIMS)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(REPAIR_AND_LOGISTICS_DETAILS)
            UpdateBreadCrum()

            If Not Me.IsPostBack Then
                PopulateFormFromBOs()
                PopulateGrid()
                'populate the Logistics grid
                ClaimLogisticalInfo.claimId = Me.State.claimId
                ClaimLogisticalInfo.PopulateGrid()

                EnableDisableFields()
                'Disable the Change Coverages Button if the Method of Repair is Replacement. 
                If (Not (Me.State.claimId.Equals(Guid.Empty))) Then
                    claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
                    If claimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                        btnChangeCoverage.Enabled = False
                    Else
                        If claimBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple Then
                            If (CType(claimBO, MultiAuthClaim).CanChangeCoverage) Then
                                btnChangeCoverage.Enabled = True
                            Else
                                btnChangeCoverage.Enabled = False
                            End If
                        Else
                            btnChangeCoverage.Enabled = True
                        End If
                    End If
                    'If claimBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple Then
                    If Me.State.selectedLvl = SelectedLevel.Authorization Then

                        claimAuthorizationBO = New Assurant.ElitaPlus.BusinessObjectsNew.ClaimAuthorization(Me.State.authorizationId)

                        With claimAuthorizationBO
                            Me.AuthorizationNumberTD.InnerText = .AuthorizationNumber

                            Me.ServiceCenterTD.InnerText = .ServiceCenter.Description
                            Me.AUTHORIZATIONStatusTD.InnerText = [Enum].GetName(GetType(ClaimAuthorizationStatus), .ClaimAuthStatus)
                            If .ClaimAuthStatus = ClaimAuthorizationStatus.Authorized Then
                                cssClassName = "StatActive"
                            Else
                                cssClassName = "StatClosed"
                            End If
                            Me.AUTHORIZATIONStatusTD.Attributes.Item("Class") = Me.AUTHORIZATIONStatusTD.Attributes.Item("Class") & " " & cssClassName

                        End With

                        moClaimAuthorizationInfoController.Translate()
                        moClaimAuthorizationInfoController.InitController(claimAuthorizationBO, False)

                    End If
                    'End If
                End If

            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region


#Region "Grid"

    Public Sub PopulateGrid()
        Try

            Dim myDALObject As New Assurant.ElitaPlus.DALObjects.RepairAndLogisticsDAL
            Dim defaultSelectedCodeId As New Guid

            ds = myDALObject.GetReplacementParts(Me.State.MyBO.Id)

            If (ds.Tables.Count > 0) Then
                Me.Grid.DataSource = ds
                Me.Grid.DataBind()
            End If

            ControlMgr.SetVisibleControl(Me, Me.Grid, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub



#End Region
#Region "Controlling Logic"
    Protected Sub PopulateFormFromBOs()
        Dim cssClassName As String
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.TextboxCustomerName, .CustomerName)
            Me.ClaimNumberTD.InnerText = .ClaimNumber
            Me.PopulateControlFromBOProperty(Me.TextboxCoverageType, .CoverageType)
            Me.ClaimStatusTD.InnerText = LookupListNew.GetDescriptionFromCode("CLSTAT", .ClaimStatus)
            If (.ClaimStatus = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If

            Me.ClaimStatusTD.Attributes.Item("Class") = Me.ClaimStatusTD.Attributes.Item("Class") & " " & cssClassName
            Me.PopulateControlFromBOProperty(Me.TextboxDateOfClaim, .DateOfClaim)
            Me.PopulateControlFromBOProperty(Me.TextboxServiceCenter, .ServiceCenter)
            Me.PopulateControlFromBOProperty(Me.TextCurrentProductCode, .ProductCode)
            Me.PopulateControlFromBOProperty(Me.TextboxManufacturer, .ClaimedDeviceManufacturer)
            Me.PopulateControlFromBOProperty(Me.TextboxModel, .ClaimedDeviceModel)
            Me.PopulateControlFromBOProperty(Me.TextboxSKU, .ClaimedDeviceSku)
            Me.PopulateControlFromBOProperty(Me.TextboxSerialNumber, .ClaimedDeviceSerialNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxIMEINumber, .ClaimedDeviceIMEINumber)
            Me.PopulateControlFromBOProperty(Me.TextboxProblemDescription, .ProblemDescription)
            Me.PopulateControlFromBOProperty(Me.TextboxDeviceReceptionDate, .DeviceReceptionDate)
            Me.PopulateControlFromBOProperty(Me.TextboxReplacementType, .ReplacementType)
            Me.PopulateControlFromBOProperty(Me.TextboxManufacturerOfReplacedDevice, .ReplacedDeviceManufacturer)
            Me.PopulateControlFromBOProperty(Me.TextboxModelOfReplacedDevice, .ReplacedDeviceModel)
            Me.PopulateControlFromBOProperty(Me.TextboxSerialNrOfReplacedDevice, .ReplacedDeviceSerialNumber)
            Me.PopulateControlFromBOProperty(Me.TextboxIMEIOfReplacedDevice, .ReplacedDeviceIMEINumber)
            Me.PopulateControlFromBOProperty(Me.TextboxSKUOfReplacedDevice, .ReplacedDeviceSku)
            Me.PopulateControlFromBOProperty(Me.TextboxTotalLaborAmount, .LaborAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxTotalPartsAmount, .PartAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxServiceCharge, .ServiceCharge)
            Me.PopulateControlFromBOProperty(Me.TextboxShippingAmount, .ShippingAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxMaxAuthorizedAmount, .AuthorizedAmount)
            Me.PopulateControlFromBOProperty(Me.TextboxServiceLevel, .ServiceLevel)
            Me.PopulateControlFromBOProperty(Me.TextboxProblemFound, .ProblemFound)
            If (Me.State.selectedLvl = SelectedLevel.Authorization) Then
                Dim oClaimAuthorization As New ClaimAuthorization(Me.State.authorizationId)
                Me.PopulateControlFromBOProperty(Me.TextboxVerificationNumber, oClaimAuthorization.VerificationNumber)
            Else
                Me.PopulateControlFromBOProperty(Me.TextboxVerificationNumber, .VerificationNumber)
            End If



        End With
    End Sub
    'START DEF-2726
    Protected Sub PopulateBOsFromForm()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "VerificationNumber", Me.TextboxVerificationNumber)
        End With
    End Sub
    'END    DEF-2726



    Protected Sub EnableDisableFields()

        If (Me.State.IsEditMode) Then

            'Enable any Editable fields
            'When in Edit Mode:
            'Hide the "Edit" button; Make the "Save" button Visible; Enable/Disable all the possible editable fields
            'When NOT in Edit Mode:
            'Hide the "Save" button; Make the "Edit" button Visible; Enable/Disable all the possible editable fields

            ControlMgr.SetVisibleForControlFamily(Me, Me.btnEdit_WRITE, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnSave_WRITE, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnUndo_Write, True, True)

        Else

            ControlMgr.SetVisibleForControlFamily(Me, Me.btnEdit_WRITE, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnSave_WRITE, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnUndo_Write, False, True)

        End If
        'If Me.State.claimAuthorizationType = ClaimAuthorizationType.Multiple And Me.State.selectedLvl = SelectedLevel.Authorization Then
        If Me.State.selectedLvl = SelectedLevel.Authorization Then
            pnlNoUseClaimAuthorization.Visible = False
            pnlUseClaimAuthorization.Visible = True
            lnkAuthDetails.Visible = True
        Else
            pnlNoUseClaimAuthorization.Visible = True
            pnlUseClaimAuthorization.Visible = False
            lnkAuthDetails.Visible = False
        End If
        Me.SetEnabledForControlFamily(Me.TextboxVerificationNumber, Me.State.IsEditMode, True)

        Dim claimBO As ClaimBase
        If (Not (Me.State.claimId.Equals(Guid.Empty))) Then
            claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
        End If

        If claimBO.Dealer.ImeiUseXcd.Equals("IMEI_USE_LST-NOTINUSE") Then
            LabelSerialNumberIMEI.Visible = True
            LabelSerialNumber.Visible = False
            LabelIMEINumber.Visible = False
            TextboxIMEINumber.Visible = False
        Else
            LabelSerialNumberIMEI.Visible = False
            LabelSerialNumber.Visible = True
            LabelIMEINumber.Visible = True
            TextboxIMEINumber.Visible = True
        End If
        If Me.State.MyBO.ReplacedDeviceIMEINumber Is Nothing Then
            LabelIMEIOfReplacedDevice.Visible = False
            TextboxIMEIOfReplacedDevice.Visible = False
        Else
            LabelIMEIOfReplacedDevice.Visible = True
            TextboxIMEIOfReplacedDevice.Visible = True
        End If

    End Sub
#End Region

#Region "Button Clicks"
    Private Sub btnSaveUC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.State.MyBO.Save()
    End Sub
    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Dim VerificationNumber As Long
        Try
            'START DEF-2726
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                If (Not Me.State.MyBO.ClaimVerificationNumLength Is Nothing) And (Me.TextboxVerificationNumber.Text.Length <> Me.State.MyBO.ClaimVerificationNumLength) Then
                    Dim errors() As ValidationError = {New ValidationError(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_VERIFICATION_NUM_MUST_BE_IN_POSITIONS), Me.State.MyBO.ClaimVerificationNumLength.ToString()), GetType(RepairAndLogistics), Nothing, "VerificationNumber", Nothing)}
                    Throw New BOValidationException(errors, GetType(RepairAndLogistics).FullName)
                Else
                    'START DEF-2726 Convert Me.TextboxVerificationNumber.Text to Long instead of INT 
                    If Not String.IsNullOrEmpty(Me.TextboxVerificationNumber.Text) Then
                        If (Me.State.MyBO.Company) = TelefonicaArgentina AndAlso ((Long.TryParse(Me.TextboxVerificationNumber.Text, VerificationNumber) = False) OrElse (CType(Me.TextboxVerificationNumber.Text, Long) <= ArgentinaVerificationNum)) Then
                            Dim errors() As ValidationError = {New ValidationError(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_VERIFICATION_NUM_MUST_BE_GREATER), ArgentinaVerificationNum.ToString()), GetType(RepairAndLogistics), Nothing, "VerificationNumber", Nothing)}
                            Throw New BOValidationException(errors, GetType(RepairAndLogistics).FullName)
                        End If
                    End If
                    Dim objRepairAndLogistics As New RepairAndLogistics
                    If (Me.State.selectedLvl = SelectedLevel.Authorization) Then
                        objRepairAndLogistics.UpdateVerificationNumber(Me.TextboxVerificationNumber.Text, Nothing, Me.State.authorizationId)
                    Else
                        objRepairAndLogistics.UpdateVerificationNumber(Me.TextboxVerificationNumber.Text, Me.State.claimId, Nothing)
                    End If

                    Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                    Me.State.IsEditMode = False
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                End If
            Else
                Me.MasterPage.MessageController.AddWarning(Message.MSG_RECORD_NOT_SAVED, True)
                Me.State.IsEditMode = False
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Try

            Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
            Me.ReturnToCallingPage(retType)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        Me.NavController = Nothing
        Me.ReturnToCallingPage(True)
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        'Get out of Edit mode
        Try
            UndoChanges()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub UndoChanges()
        If Not Me.State.MyBO.IsNew Then
            'Reload from the DB
            Me.State.MyBO = New RepairAndLogistics(Me.State.MyBO.Id)
        End If
        Me.State.IsEditMode = False
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        Me.MasterPage.MessageController.Clear()

    End Sub
    Private Sub btnEdit_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click

        'Introduce the logic to Enable/Disable the Editable fields here
        'Also make the relevant buttons Disabled/Invisible when in Edit mode - set a flag carried in the State

        'Set the Me.State.IsEditMode flag to "True" 
        Try

            Me.State.IsEditMode = True

            Me.EnableDisableFields()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            'Me.AdjustMenuPosition()
        End Try
    End Sub

    Private Sub btnChangeCoverage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeCoverage.Click
        Dim claimBO As ClaimBase
        Try
            If (Not (Me.State.claimId.Equals(Guid.Empty))) Then

                claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
                params.Add(CType(claimBO.LossDate, String))
                params.Add(claimBO.Id)
                params.Add(claimBO.CertificateId)
                params.Add(claimBO.CertItemCoverageId)
                params.Add(claimBO.StatusCode)

                Dim invoiceProcessDate As Date = Nothing
                Select Case claimBO.ClaimAuthorizationType
                    Case ClaimAuthorizationType.Single
                        If (Not CType(claimBO, Claim).InvoiceProcessDate Is Nothing) Then invoiceProcessDate = CType(CType(claimBO, Claim).InvoiceProcessDate, Date)
                    Case ClaimAuthorizationType.Multiple
                        invoiceProcessDate = Nothing
                    Case ClaimAuthorizationType.None
                        Throw New NotImplementedException
                End Select

                params.Add(CType(invoiceProcessDate, String))
                Me.callPage(ClaimForm.COVERAGE_TYPE_URL, params)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public BoChanged As Boolean = False
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ClaimBase, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand)
            Me.LastOperation = LastOp
        End Sub
    End Class
#End Region


End Class



'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (11/2/2004)  ********************
Imports System.Diagnostics
Imports Assurant.ElitaPlus.DALObjects
Imports Codes = Assurant.ElitaPlus.BusinessObjectsNew.Codes




Partial Class RepairAndLogisticsForm
    Inherits ElitaPlusSearchPage

#Region "Variables"
    Private mbIsFirstPass As Boolean = True

    'Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents EditPanel_WRITE As Panel
    Protected WithEvents LabelDaysLimitExceeded As Label
    Protected WithEvents Label4 As Label
    Protected WithEvents Label6 As Label
    Protected WithEvents Label7 As Label
    Protected WithEvents Label8 As Label
    Protected WithEvents btnCancelEdit_WRITE As Button
    Protected WithEvents ServiceCenterPanel3 As Panel

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
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
            Set(value As Guid)
                _moClaimId = value
            End Set
        End Property
        Private _moAuthorizationId As Guid
        Public Property AuthorizationId() As Guid
            Get
                Return _moAuthorizationId
            End Get
            Set(value As Guid)
                _moAuthorizationId = value
            End Set
        End Property

        Private _SelectedLvl As SelectedLevel
        Public Property selectedLvl() As SelectedLevel
            Get
                Return _SelectedLvl
            End Get
            Set(value As SelectedLevel)
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

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Dim param As Parameters
        Try

            If CallingParameters IsNot Nothing Then
                'Dim type = CallingPar.GetType()
                If CallingPar.GetType() Is GetType(Parameters) Then
                    param = CType(CallingPar, Parameters)
                    State.MyBO = New RepairAndLogistics(param.ClaimId)
                    State.claimId = param.ClaimId
                    State.authorizationId = param.AuthorizationId
                    State.selectedLvl = param.selectedLvl
                    'Else
                    '    Me.State.MyBO = New RepairAndLogistics(CType(CType(CallingPar, ArrayList)(0), Guid))
                    '    Me.State.claimId = CType(CType(CallingPar, ArrayList)(0), Guid)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Page Events"
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State.MyBO IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage(REPAIR_AND_LOGISTICS_DETAILS)
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim claimAuthorizationBO As ClaimAuthorization
        Dim cssClassName As String

        Try
            Dim claimBO As ClaimBase

            MasterPage.MessageController.Clear()

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CLAIMS)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(REPAIR_AND_LOGISTICS_DETAILS)
            UpdateBreadCrum()

            If Not IsPostBack Then
                PopulateFormFromBOs()
                PopulateGrid()
                'populate the Logistics grid
                ClaimLogisticalInfo.claimId = State.claimId
                ClaimLogisticalInfo.PopulateGrid()

                EnableDisableFields()
                'Disable the Change Coverages Button if the Method of Repair is Replacement. 
                If (Not (State.claimId.Equals(Guid.Empty))) Then
                    claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
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

                        claimAuthorizationBO = New ClaimAuthorization(State.authorizationId)

                        With claimAuthorizationBO
                            AuthorizationNumberTD.InnerText = .AuthorizationNumber

                            ServiceCenterTD.InnerText = .ServiceCenter.Description
                            AUTHORIZATIONStatusTD.InnerText = [Enum].GetName(GetType(ClaimAuthorizationStatus), .ClaimAuthStatus)
                            If .ClaimAuthStatus = ClaimAuthorizationStatus.Authorized Then
                                cssClassName = "StatActive"
                            Else
                                cssClassName = "StatClosed"
                            End If
                            AUTHORIZATIONStatusTD.Attributes.Item("Class") = AUTHORIZATIONStatusTD.Attributes.Item("Class") & " " & cssClassName

                        End With

                        moClaimAuthorizationInfoController.Translate()
                        moClaimAuthorizationInfoController.InitController(claimAuthorizationBO, False)

                    End If
                    'End If
                End If

            End If


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region


#Region "Grid"

    Public Sub PopulateGrid()
        Try

            Dim myDALObject As New RepairAndLogisticsDAL
            Dim defaultSelectedCodeId As New Guid

            ds = myDALObject.GetReplacementParts(State.MyBO.Id)

            If (ds.Tables.Count > 0) Then
                Grid.DataSource = ds
                Grid.DataBind()
            End If

            ControlMgr.SetVisibleControl(Me, Grid, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub



#End Region
#Region "Controlling Logic"
    Protected Sub PopulateFormFromBOs()
        Dim cssClassName As String
        With State.MyBO

            PopulateControlFromBOProperty(TextboxCustomerName, .CustomerName)
            ClaimNumberTD.InnerText = .ClaimNumber
            PopulateControlFromBOProperty(TextboxCoverageType, .CoverageType)
            ClaimStatusTD.InnerText = LookupListNew.GetDescriptionFromCode("CLSTAT", .ClaimStatus)
            If (.ClaimStatus = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If

            ClaimStatusTD.Attributes.Item("Class") = ClaimStatusTD.Attributes.Item("Class") & " " & cssClassName
            PopulateControlFromBOProperty(TextboxDateOfClaim, .DateOfClaim)
            PopulateControlFromBOProperty(TextboxServiceCenter, .ServiceCenter)
            PopulateControlFromBOProperty(TextCurrentProductCode, .ProductCode)
            PopulateControlFromBOProperty(TextboxManufacturer, .ClaimedDeviceManufacturer)
            PopulateControlFromBOProperty(TextboxModel, .ClaimedDeviceModel)
            PopulateControlFromBOProperty(TextboxSKU, .ClaimedDeviceSku)
            PopulateControlFromBOProperty(TextboxSerialNumber, .ClaimedDeviceSerialNumber)
            PopulateControlFromBOProperty(TextboxIMEINumber, .ClaimedDeviceIMEINumber)
            PopulateControlFromBOProperty(TextboxProblemDescription, .ProblemDescription)
            PopulateControlFromBOProperty(TextboxDeviceReceptionDate, .DeviceReceptionDate)
            PopulateControlFromBOProperty(TextboxReplacementType, .ReplacementType)
            PopulateControlFromBOProperty(TextboxManufacturerOfReplacedDevice, .ReplacedDeviceManufacturer)
            PopulateControlFromBOProperty(TextboxModelOfReplacedDevice, .ReplacedDeviceModel)
            PopulateControlFromBOProperty(TextboxSerialNrOfReplacedDevice, .ReplacedDeviceSerialNumber)
            PopulateControlFromBOProperty(TextboxIMEIOfReplacedDevice, .ReplacedDeviceIMEINumber)
            PopulateControlFromBOProperty(TextboxSKUOfReplacedDevice, .ReplacedDeviceSku)
            PopulateControlFromBOProperty(TextboxTotalLaborAmount, .LaborAmount)
            PopulateControlFromBOProperty(TextboxTotalPartsAmount, .PartAmount)
            PopulateControlFromBOProperty(TextboxServiceCharge, .ServiceCharge)
            PopulateControlFromBOProperty(TextboxShippingAmount, .ShippingAmount)
            PopulateControlFromBOProperty(TextboxMaxAuthorizedAmount, .AuthorizedAmount)
            PopulateControlFromBOProperty(TextboxServiceLevel, .ServiceLevel)
            PopulateControlFromBOProperty(TextboxProblemFound, .ProblemFound)
            If (Me.State.selectedLvl = SelectedLevel.Authorization) Then
                Dim oClaimAuthorization As New ClaimAuthorization(State.authorizationId)
                PopulateControlFromBOProperty(TextboxVerificationNumber, oClaimAuthorization.VerificationNumber)
            Else
                PopulateControlFromBOProperty(TextboxVerificationNumber, .VerificationNumber)
            End If



        End With
    End Sub
    'START DEF-2726
    Protected Sub PopulateBOsFromForm()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "VerificationNumber", TextboxVerificationNumber)
        End With
    End Sub
    'END    DEF-2726



    Protected Sub EnableDisableFields()

        If (State.IsEditMode) Then

            'Enable any Editable fields
            'When in Edit Mode:
            'Hide the "Edit" button; Make the "Save" button Visible; Enable/Disable all the possible editable fields
            'When NOT in Edit Mode:
            'Hide the "Save" button; Make the "Edit" button Visible; Enable/Disable all the possible editable fields

            ControlMgr.SetVisibleForControlFamily(Me, btnEdit_WRITE, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnSave_WRITE, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnUndo_Write, True, True)

        Else

            ControlMgr.SetVisibleForControlFamily(Me, btnEdit_WRITE, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnSave_WRITE, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnUndo_Write, False, True)

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
        SetEnabledForControlFamily(TextboxVerificationNumber, State.IsEditMode, True)

        Dim claimBO As ClaimBase
        If (Not (State.claimId.Equals(Guid.Empty))) Then
            claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
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
        If State.MyBO.ReplacedDeviceIMEINumber Is Nothing Then
            LabelIMEIOfReplacedDevice.Visible = False
            TextboxIMEIOfReplacedDevice.Visible = False
        Else
            LabelIMEIOfReplacedDevice.Visible = True
            TextboxIMEIOfReplacedDevice.Visible = True
        End If

    End Sub
#End Region

#Region "Button Clicks"
    Private Sub btnSaveUC_Click(sender As Object, e As EventArgs)
        State.MyBO.Save()
    End Sub
    Private Sub btnSave_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_WRITE.Click
        Dim VerificationNumber As Long
        Try
            'START DEF-2726
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                If (State.MyBO.ClaimVerificationNumLength IsNot Nothing) AndAlso (TextboxVerificationNumber.Text.Length <> State.MyBO.ClaimVerificationNumLength) Then
                    Dim errors() As ValidationError = {New ValidationError(String.Format(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_VERIFICATION_NUM_MUST_BE_IN_POSITIONS), State.MyBO.ClaimVerificationNumLength.ToString()), GetType(RepairAndLogistics), Nothing, "VerificationNumber", Nothing)}
                    Throw New BOValidationException(errors, GetType(RepairAndLogistics).FullName)
                Else
                    'START DEF-2726 Convert Me.TextboxVerificationNumber.Text to Long instead of INT 
                    If Not String.IsNullOrEmpty(TextboxVerificationNumber.Text) Then
                        If (State.MyBO.Company) = TelefonicaArgentina AndAlso ((Long.TryParse(TextboxVerificationNumber.Text, VerificationNumber) = False) OrElse (CType(TextboxVerificationNumber.Text, Long) <= ArgentinaVerificationNum)) Then
                            Dim errors() As ValidationError = {New ValidationError(String.Format(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_VERIFICATION_NUM_MUST_BE_GREATER), ArgentinaVerificationNum.ToString()), GetType(RepairAndLogistics), Nothing, "VerificationNumber", Nothing)}
                            Throw New BOValidationException(errors, GetType(RepairAndLogistics).FullName)
                        End If
                    End If
                    Dim objRepairAndLogistics As New RepairAndLogistics
                    If (Me.State.selectedLvl = SelectedLevel.Authorization) Then
                        objRepairAndLogistics.UpdateVerificationNumber(TextboxVerificationNumber.Text, Nothing, State.authorizationId)
                    Else
                        objRepairAndLogistics.UpdateVerificationNumber(TextboxVerificationNumber.Text, State.claimId, Nothing)
                    End If

                    MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    State.IsEditMode = False
                    PopulateFormFromBOs()
                    EnableDisableFields()
                End If
            Else
                MasterPage.MessageController.AddWarning(Message.MSG_RECORD_NOT_SAVED, True)
                State.IsEditMode = False
                PopulateFormFromBOs()
                EnableDisableFields()
            End If


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try

            Dim retType As New ClaimForm.ReturnType(DetailPageCommand.Back)
            ReturnToCallingPage(retType)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub Back(cmd As DetailPageCommand)
        NavController = Nothing
        ReturnToCallingPage(True)
    End Sub

    Private Sub btnUndo_Write_Click(sender As Object, e As EventArgs) Handles btnUndo_Write.Click
        'Get out of Edit mode
        Try
            UndoChanges()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub UndoChanges()
        If Not State.MyBO.IsNew Then
            'Reload from the DB
            State.MyBO = New RepairAndLogistics(State.MyBO.Id)
        End If
        State.IsEditMode = False
        PopulateFormFromBOs()
        EnableDisableFields()
        MasterPage.MessageController.Clear()

    End Sub
    Private Sub btnEdit_WRITE_Click(sender As Object, e As EventArgs) Handles btnEdit_WRITE.Click

        'Introduce the logic to Enable/Disable the Editable fields here
        'Also make the relevant buttons Disabled/Invisible when in Edit mode - set a flag carried in the State

        'Set the Me.State.IsEditMode flag to "True" 
        Try

            State.IsEditMode = True

            EnableDisableFields()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            'Me.AdjustMenuPosition()
        End Try
    End Sub

    Private Sub btnChangeCoverage_Click(sender As Object, e As EventArgs) Handles btnChangeCoverage.Click
        Dim claimBO As ClaimBase
        Try
            If (Not (State.claimId.Equals(Guid.Empty))) Then

                claimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
                params.Add(CType(claimBO.LossDate, String))
                params.Add(claimBO.Id)
                params.Add(claimBO.CertificateId)
                params.Add(claimBO.CertItemCoverageId)
                params.Add(claimBO.StatusCode)

                Dim invoiceProcessDate As Date = Nothing
                Select Case claimBO.ClaimAuthorizationType
                    Case ClaimAuthorizationType.Single
                        If (CType(claimBO, Claim).InvoiceProcessDate IsNot Nothing) Then invoiceProcessDate = CType(CType(claimBO, Claim).InvoiceProcessDate, Date)
                    Case ClaimAuthorizationType.Multiple
                        invoiceProcessDate = Nothing
                    Case ClaimAuthorizationType.None
                        Throw New NotImplementedException
                End Select

                params.Add(CType(invoiceProcessDate, String))
                callPage(ClaimForm.COVERAGE_TYPE_URL, params)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub


#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimBase, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region


End Class



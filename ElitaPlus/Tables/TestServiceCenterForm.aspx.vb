'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (10/7/2004)  ********************


Partial Class TestServiceCenterForm
    Inherits ElitaPlusPage



    'Protected WithEvents ErrorCtrl As ErrorController
    'Protected WithEvents moAddressController As UserControlAddress
    'Protected WithEvents UserControlAvailableSelectedManufacturers As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected
    'Protected WithEvents UserControlAvailableSelectedDistricts As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected
    'Protected WithEvents UserControlAvailableSelectedDealers As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected
    'Protected WithEvents UsercontrolavailableselectedServiceNetworks As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

    Protected WithEvents DataGridDetailMfg As System.Web.UI.WebControls.DataGrid
    Protected WithEvents DataGridDetailDst As System.Web.UI.WebControls.DataGrid
    Protected WithEvents DataGridDetailDlr As System.Web.UI.WebControls.DataGrid

    Protected WithEvents LabelLastReconDate As System.Web.UI.WebControls.Label
    Protected WithEvents TextboxLastReconDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents ImageButtonLastReconDate As System.Web.UI.WebControls.ImageButton
    Protected WithEvents LabelMinReplCost As System.Web.UI.WebControls.Label
    Protected WithEvents cboTypeOfMarketing As System.Web.UI.WebControls.DropDownList
    Protected WithEvents LabelEmpty1 As System.Web.UI.WebControls.Label
    Protected WithEvents LabelEmpty2 As System.Web.UI.WebControls.Label

    Protected WithEvents LabelEmpty3 As System.Web.UI.WebControls.Label
    '  Protected WithEvents moBankInfoController As UserControlBankInfo


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
    Public Const URL As String = "ServiceCenterForm.aspx"
    Public Const GRID_COL_CODE As Integer = 0
    Public Const GRID_COL_NAME As Integer = 1
    Public Const NOTHING_SELECTED As Integer = 0
    Public Const BankInfoStartIndex As Int16 = 28
    Public Const AddressInfoStartIndex As Int16 = 32
    Private Const NOTHING_SELECTED_GUID As String = "00000000-0000-0000-0000-000000000000"
#End Region

#Region "Attributes"

    Private moSvcCtr As ServiceCenter

#End Region

#Region "Properties"

    Private Property SvcCtrId() As Guid
        Get
            Return State.SvcCtrId
        End Get
        Set(Value As Guid)
            State.SvcCtrId = Value
        End Set

    End Property


    Private ReadOnly Property IsNewServiceCenter() As Boolean
        Get
            Return State.MyBO.IsNew
        End Get

    End Property


    Public ReadOnly Property AddressCtr() As UserControlAddress
        Get
            Return moAddressController
        End Get
    End Property

    Public ReadOnly Property UserBankInfoCtr() As UserControlBankInfo
        Get
            If moBankInfoController Is Nothing Then
                moBankInfoController = CType(FindControl("moBankInfoController"), UserControlBankInfo)
            End If
            Return moBankInfoController
        End Get
    End Property

#End Region


#Region "Page Return Type"

    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceCenter
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ServiceCenter, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public SVCId As Guid
        Public PageDealerId As Guid
        Public mbIsComingFromDealerform As Boolean = False
        Public Sub New(SVCorDealerId As Guid, Optional ByVal bIsComingFromDealerform As Boolean = False)
            mbIsComingFromDealerform = bIsComingFromDealerform
            If bIsComingFromDealerform Then
                PageDealerId = SVCorDealerId
            Else
                SVCId = SVCorDealerId
            End If
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public MyBO As ServiceCenter
        Public ScreenSnapShotBO As ServiceCenter
        Public BankInfoBO As BankInfo
        Public pageParameters As Parameters
        Public IsNew As Boolean = False
        Public stIsComingFromDealerform As Boolean = False
        Public SvcCtrId, stdealerid As Guid
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public ForEdit As Boolean = False
        Public Statusdv As DataView

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
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.pageParameters = CType(CallingParameters, Parameters)
                If Not State.pageParameters.mbIsComingFromDealerform Then
                    State.MyBO = New ServiceCenter(State.pageParameters.SVCId)

                    If State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                        State.MyBO.isBankInfoNeedDeletion = False
                    Else
                        State.MyBO.isBankInfoNeedDeletion = True
                    End If
                    State.stdealerid = System.Guid.Empty
                    'Me.State.stIsComingFromDealerform = False
                Else
                    State.stdealerid = State.pageParameters.PageDealerId
                    State.stIsComingFromDealerform = True
                End If
            Else
                State.stdealerid = System.Guid.Empty
                'Me.State.stIsComingFromDealerform = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Dim selectedPaymentMethod As Guid
            'Caio
            'If Not Request("OriginalDealer") = Nothing Then
            'txtOriginalDealer.Text = Request("OriginalDealer").ToString

            'End If

            ErrorCtrl.Clear_Hide()
            ResolveShippingFeeVisibility()
            If Not IsPostBack Then
                MenuEnabled = False
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New ServiceCenter
                    State.IsNew = True
                End If
                BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
                PopulateCountry()
                PopulateDropdowns()
                moBankInfoController.Visible = False
                moBankInfoController.ReAssignTabIndex(BankInfoStartIndex)
                moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                State.BankInfoBO = Nothing
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If

            cboPaymentMethodId.AutoPostBack = True
            selectedPaymentMethod = GetSelectedItem(cboPaymentMethodId)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"
    Protected Sub ResolveShippingFeeVisibility()
        CheckBoxShipping.Attributes.Add("onClick", "showProcessingFee(this);")
        If Not CheckBoxShipping.Checked Then
            LabelProcessingFee.Style.Add("Display", "'none'")
            TextboxPROCESSING_FEE.Style.Add("Display", "'none'")
        Else
            LabelProcessingFee.Style.Add("Display", "''")
            TextboxPROCESSING_FEE.Style.Add("Display", "''")
        End If
    End Sub


    Protected Sub EnableDisableFields()
        'Disabled by Default
        ControlMgr.SetEnableControl(Me, TextboxCode, False)

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'ControlMgr.SetEnableControl(Me, btnAcctSettings, True)

        ' do NOT show original dealer id if users company is NOT VSC
        If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR Then
            'Me.State.stdealerid.Equals(Guid.Empty) And 
            cboOriginalDealer.Visible = True
            lblDealer.Visible = True
        Else
            cboOriginalDealer.Visible = False
            lblDealer.Visible = False
        End If

        'enable/disable/visible depending on if coming from dealerform
        ControlMgr.SetEnableControl(Me, cboOriginalDealer, State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, lblDealer, State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, cboServiceTypeId, State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, LabelServiceTypeId, State.stdealerid.Equals(Guid.Empty))

        'Now disable depending on the object state
        If State.MyBO.IsNew Then

            'Enable and blank out the Service Center Code field
            ControlMgr.SetEnableControl(Me, TextboxCode, True)
            TextboxCode.Text = String.Empty

            'Blank out the Service Center Description field
            TextboxDescription.Text = String.Empty

            'Hide the DateAdded and DateLastMaintained Labels and TextBox fields
            ControlMgr.SetVisibleControl(Me, LabelDateAdded, False)
            ControlMgr.SetVisibleControl(Me, LabelDateLastMaintained, False)

            ControlMgr.SetVisibleControl(Me, TextboxDateAdded, False)
            ControlMgr.SetVisibleControl(Me, TextboxDateLastMaintained, False)

            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            'ControlMgr.SetEnableControl(Me, btnAcctSettings, False)
        End If

        'WRITE YOUR OWN CODE HERE

        'Disable the DateAdded and DateLastMaintained fields
        ControlMgr.SetEnableControl(Me, TextboxDateAdded, False)
        ControlMgr.SetEnableControl(Me, TextboxDateLastMaintained, False)
        AddressCtr.EnableControls(False)

    End Sub

    Protected Sub BindBoPropertiesToLabels()

        BindBOPropertyToLabel(State.MyBO, "PriceGroupId", LabelPriceGroupId)
        BindBOPropertyToLabel(State.MyBO, "ServiceGroupId", LabelServiceGroupId)
        BindBOPropertyToLabel(State.MyBO, "ServiceTypeId", LabelServiceTypeId)
        BindBOPropertyToLabel(State.MyBO, "LoanerCenterId", LabelLoanerCenterId)
        BindBOPropertyToLabel(State.MyBO, "MasterCenterId", LabelMasterCenterId)
        BindBOPropertyToLabel(State.MyBO, "Code", LabelCode)
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        BindBOPropertyToLabel(State.MyBO, "RatingCode", LabelRatingCode)
        BindBOPropertyToLabel(State.MyBO, "ContactName", LabelContactName)
        BindBOPropertyToLabel(State.MyBO, "OwnerName", LabelOwnerName)
        BindBOPropertyToLabel(State.MyBO, "Phone1", LabelPhone1)
        BindBOPropertyToLabel(State.MyBO, "Phone2", LabelPhone2)
        BindBOPropertyToLabel(State.MyBO, "Fax", LabelFax)
        BindBOPropertyToLabel(State.MyBO, "Email", LabelEmail)
        BindBOPropertyToLabel(State.MyBO, "CcEmail", LabelCcEmail)
        BindBOPropertyToLabel(State.MyBO, "FtpAddress", LabelFtpAddress)
        BindBOPropertyToLabel(State.MyBO, "TaxId", LabelTaxId)
        BindBOPropertyToLabel(State.MyBO, "ServiceWarrantyDays", LabelServiceWarrantyDays)
        BindBOPropertyToLabel(State.MyBO, "StatusCode", LabelStatusCode)
        BindBOPropertyToLabel(State.MyBO, "BusinessHours", LabelBusinessHours)
        BindBOPropertyToLabel(State.MyBO, "DateAdded", LabelDateAdded)
        BindBOPropertyToLabel(State.MyBO, "DateModified", LabelDateLastMaintained)
        BindBOPropertyToLabel(State.MyBO, "comments", LabelComment1)
        BindBOPropertyToLabel(State.MyBO, "PaymentMethodId", PaymentMethodDrpLabel)
        BindBOPropertyToLabel(State.MyBO, "ProcessingFee", LabelProcessingFee)
        AddressCtr.SetTheRequiredFields()

        'added by Anindita - original dealer id was not added here
        BindBOPropertyToLabel(State.MyBO, "OriginalDealerId", lblDealer)
        ClearGridHeadersAndLabelsErrSign()

    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
        If State.IsNew Then
            ' New one
            If moCountryDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, GetSelectedDescription(moCountryDrop))
            State.MyBO.CountryId = GetSelectedItem(moCountryDrop)
        Else
            oCountry = New Country(State.MyBO.CountryId)
            SetSelectedItem(moCountryDrop, State.MyBO.CountryId)
            PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso State.IsNew) Then
            ' Multiple Countries
            ControlMgr.SetVisibleControl(Me, moCountryDrop, True)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
        Else
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, True)
        End If
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId



        Dim Statusdv As DataView = LookupListNew.DataView(LookupListNew.LK_SERVICE_CENTER_STATUS)
        State.Statusdv = Statusdv


        With State.MyBO

            'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            'If Me.State.IsNew Then
            '    ' New one
            '    If moCountryDrop.SelectedIndex < 0 Then
            '        moCountryDrop.SelectedIndex = 0
            '    End If

            '    Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            '    Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
            'End If

            BindListControlToDataView(cboPriceGroupId, LookupListNew.GetPriceGroupLookupList(.CountryId), , , True)

            BindListControlToDataView(cboServiceGroupId, LookupListNew.GetServiceGroupLookupList(.CountryId), , , True)

            BindListControlToDataView(cboServiceTypeId, LookupListNew.GetServiceTypeLookupList(langId), , , True)

            BindListControlToDataView(cboMasterCenterId, LookupListNew.GetServiceCenterLookupList(.CountryId), , , True)

            BindListControlToDataView(cboLoanerCenterId, LookupListNew.GetLoanerCenterLookupList(.CountryId), , , True)

            BindListControlToDataView(cboPaymentMethodId, LookupListNew.GetPaymentMethodLookupList(langId), , , True)

            'Me.BindListControlToDataView(Me.cboOriginalDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , , True)
            BindListControlToDataView(cboOriginalDealer, LookupListNew.GetOriginalDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId, State.MyBO.Id), , , True)

            BindListControlToDataView(cboStatusCode, Statusdv)



        End With
    End Sub

    Protected Sub PopulateFormFromBOs()
        '     Dim oServiceCenter As ServiceCenter = TheServiceCenter
        'Dim oCountry As Country

        Dim statusdv As DataView = State.Statusdv


        With State.MyBO

            'Me.SetSelectedItem(Me.cboPriceGroupId, .PriceListId)
            SetSelectedItem(cboServiceGroupId, .ServiceGroupId)
            'Me.SetSelectedItem(Me.cboServiceTypeId, .ServiceTypeId)
            SetSelectedItem(cboLoanerCenterId, .LoanerCenterId)
            SetSelectedItem(cboMasterCenterId, .MasterCenterId)
            SetSelectedItem(cboPaymentMethodId, .PaymentMethodId)

            If State.stIsComingFromDealerform Then
                SetSelectedItem(cboOriginalDealer, State.stdealerid)
            Else
                If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR AndAlso State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    cboOriginalDealer.SelectedIndex = NOTHING_SELECTED
                ElseIf LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR AndAlso Not State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    If State.MyBO.IsNew Then
                        cboOriginalDealer.SelectedIndex = NOTHING_SELECTED
                    Else
                        SetSelectedItem(cboOriginalDealer, .OriginalDealerId)
                    End If
                Else
                    SetSelectedItem(cboOriginalDealer, System.Guid.Empty)
                End If
            End If


            PopulateControlFromBOProperty(TextboxCode, .Code)
            PopulateControlFromBOProperty(TextboxDescription, .Description)
            PopulateControlFromBOProperty(TextboxRatingCode, .RatingCode)
            PopulateControlFromBOProperty(TextboxContactName, .ContactName)
            PopulateControlFromBOProperty(TextboxOwnerName, .OwnerName)
            PopulateControlFromBOProperty(TextboxPhone1, .Phone1)
            PopulateControlFromBOProperty(TextboxPhone2, .Phone2)
            PopulateControlFromBOProperty(TextboxFax, .Fax)
            PopulateControlFromBOProperty(TextboxEmail, .Email)
            PopulateControlFromBOProperty(TextboxCcEmail, .CcEmail)
            PopulateControlFromBOProperty(TextboxFtpAddress, .FtpAddress)
            PopulateControlFromBOProperty(TextboxTaxId, .TaxId)
            PopulateControlFromBOProperty(TextboxServiceWarrantyDays, .ServiceWarrantyDays)

            ''Me.PopulateControlFromBOProperty(Me.TextboxStatusCode, .StatusCode)
            PopulateControlFromBOProperty(cboStatusCode, LookupListNew.GetIdFromCode(statusdv, .StatusCode))

            PopulateControlFromBOProperty(TextboxBusinessHours, .BusinessHours)
            PopulateControlFromBOProperty(CheckBoxDefaultToEmail, .DefaultToEmailFlag)
            PopulateControlFromBOProperty(CheckBoxIvaResponsible, .IvaResponsibleFlag)
            PopulateControlFromBOProperty(TextboxDateAdded, .CreatedDate)
            PopulateControlFromBOProperty(TextboxDateLastMaintained, .ModifiedDate)
            PopulateControlFromBOProperty(TextboxComment, .Comments)
            PopulateControlFromBOProperty(CheckBoxShipping, .Shipping)
            If .Shipping Then
                ResolveShippingFeeVisibility()
                PopulateControlFromBOProperty(TextboxPROCESSING_FEE, .ProcessingFee)
            End If

            AddressCtr.Bind(.Address)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                moBankInfoController.Visible = True
                State.BankInfoBO = State.MyBO.Add_BankInfo
                'Me.State.BankInfoBO.ACCOUNT_NAME = String.Empty
                UserBankInfoCtr.Bind(State.BankInfoBO, ErrorCtrl)
            Else
                State.BankInfoBO = Nothing
                moBankInfoController.Visible = False
            End If

        End With

        PopulateDetailControls()

    End Sub

    Protected Sub PopulateBOsFromForm()

        With State.MyBO

            '*Caio
            'If txtOriginalDealer.Text = "" Then
            '    Me.PopulateBOProperty(Me.State.MyBO, "OriginalDealerId", Me.cboOriginalDealer)
            'Else
            '    Me.State.MyBO.OriginalDealerId = New Guid(Request("OriginalDealer").ToString)
            'End If
            '*


            If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR AndAlso cboOriginalDealer.SelectedValue.Trim.Length > 0 Then
                PopulateBOProperty(State.MyBO, "OriginalDealerId", cboOriginalDealer)
            End If

            PopulateBOProperty(State.MyBO, "PriceGroupId", cboPriceGroupId)
            PopulateBOProperty(State.MyBO, "ServiceGroupId", cboServiceGroupId)
            PopulateBOProperty(State.MyBO, "ServiceTypeId", cboServiceTypeId)
            PopulateBOProperty(State.MyBO, "LoanerCenterId", cboLoanerCenterId)
            PopulateBOProperty(State.MyBO, "MasterCenterId", cboMasterCenterId)
            PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop)
            PopulateBOProperty(State.MyBO, "Code", TextboxCode)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            PopulateBOProperty(State.MyBO, "RatingCode", TextboxRatingCode)
            PopulateBOProperty(State.MyBO, "ContactName", TextboxContactName)
            PopulateBOProperty(State.MyBO, "OwnerName", TextboxOwnerName)
            PopulateBOProperty(State.MyBO, "Phone1", TextboxPhone1)
            PopulateBOProperty(State.MyBO, "Phone2", TextboxPhone2)
            PopulateBOProperty(State.MyBO, "Fax", TextboxFax)
            PopulateBOProperty(State.MyBO, "Email", TextboxEmail)
            PopulateBOProperty(State.MyBO, "CcEmail", TextboxCcEmail)
            PopulateBOProperty(State.MyBO, "FtpAddress", TextboxFtpAddress)
            PopulateBOProperty(State.MyBO, "TaxId", TextboxTaxId)
            PopulateBOProperty(State.MyBO, "ServiceWarrantyDays", TextboxServiceWarrantyDays)

            ''If (Not (Me.TextboxStatusCode.Text = Nothing)) Then
            ''    Me.TextboxStatusCode.Text = Me.TextboxStatusCode.Text.ToUpper
            ''End If
            ''Me.PopulateBOProperty(Me.State.MyBO, "StatusCode", Me.TextboxStatusCode)
            PopulateBOProperty(State.MyBO, "StatusCode", LookupListNew.GetCodeFromId(State.Statusdv, New Guid(cboStatusCode.SelectedValue)))
            PopulateBOProperty(State.MyBO, "BusinessHours", TextboxBusinessHours)
            PopulateBOProperty(State.MyBO, "DefaultToEmailFlag", CheckBoxDefaultToEmail)
            PopulateBOProperty(State.MyBO, "IvaResponsibleFlag", CheckBoxIvaResponsible)
            PopulateBOProperty(State.MyBO, "Comments", TextboxComment)
            PopulateBOProperty(State.MyBO, "PaymentMethodId", cboPaymentMethodId)


            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                'Me.moBankInfoController.Visible = True
                State.MyBO.BankInfoId = State.BankInfoBO.Id
                UserBankInfoCtr.PopulateBOFromControl()
            Else
                State.BankInfoBO = Nothing
                State.MyBO.BankInfoId = Nothing
                'Me.moBankInfoController.Visible = False
            End If

            'Set the following 2 BO Properties based on whether the Selected Items in 
            'the MASTER CENTER and LOANER CENTER Dropdown Lists are "Nothing Selected" or an actual value
            'If "Nothing Selected", then the corressponding FLAG value should be = "N", else "Y"

            If State.ForEdit = True Then
                State.MyBO.Address.AddressRequiredServCenter = True
            End If
            AddressCtr.PopulateBOFromControl()

            If (State.MyBO.IsDirty) Then
                'If (Me.State.MyBO.IsFamilyDirty) Then
                If (cboMasterCenterId.SelectedIndex = NOTHING_SELECTED) Then
                    'Nothing selected
                    PopulateBOProperty(State.MyBO, "MasterFlag", "N")
                Else
                    PopulateBOProperty(State.MyBO, "MasterFlag", "Y")
                End If

                If (cboLoanerCenterId.SelectedIndex = NOTHING_SELECTED) Then
                    'Nothing selected
                    PopulateBOProperty(State.MyBO, "LoanerFlag", "N")
                Else
                    PopulateBOProperty(State.MyBO, "LoanerFlag", "Y")
                End If
            End If

            PopulateBOProperty(State.MyBO, "Shipping", CheckBoxShipping)
            If State.MyBO.Shipping Then
                PopulateBOProperty(State.MyBO, "ProcessingFee", TextboxPROCESSING_FEE)
            Else
                State.MyBO.ProcessingFee = Nothing
            End If

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    'Protected Sub PopulateBankInfoBOFromUserCtr()
    '    With Me.State.BankInfoBO
    '        Me.moBankInfoController.PopulateBOFromControl()
    '    End With
    '    'If Me.ErrCollection.Count > 0 Then
    '    '    Throw New PopulateBOErrorException
    '    'End If
    'End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New ServiceCenter
        State.IsNew = True
        PopulateCountry()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        PopulateBOsFromForm()

        Dim newObj As New ServiceCenter
        newObj.Copy(State.MyBO)

        If Not newObj.BankInfoId.Equals(Guid.Empty) Then
            ' copy the original bankinfo
            newObj.BankInfoId = Guid.Empty
            newObj.Add_BankInfo()
            newObj.BankInfoId = newObj.CurrentBankInfo.Id
            newObj.CurrentBankInfo.CopyFrom(State.MyBO.CurrentBankInfo)
            State.BankInfoBO = newObj.CurrentBankInfo
            UserBankInfoCtr.Bind(State.BankInfoBO, ErrorCtrl)
        End If

        State.MyBO = newObj
        State.MyBO.Code = Nothing
        State.MyBO.Description = Nothing
        PopulateCountry()
        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New ServiceCenter
        State.ScreenSnapShotBO.Copy(State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#Region "Detail Tabs"

    Sub PopulateUserControlAvailableSelectedManufacturers()
        UserControlAvailableSelectedManufacturers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableManufacturers()
            Dim selectedDv As DataView = State.MyBO.GetSelectedManufacturers()
            UserControlAvailableSelectedManufacturers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedManufacturers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedDistricts()
        UserControlAvailableSelectedDistricts.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDistricts, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableDistricts()
            Dim selectedDv As DataView = State.MyBO.GetSelectedDistricts()
            UserControlAvailableSelectedDistricts.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedDistricts.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDistricts, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedDealers()
        UserControlAvailableSelectedDealers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableDealers()
            Dim selectedDv As DataView = State.MyBO.GetSelectedDealers()
            UserControlAvailableSelectedDealers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UserControlAvailableSelectedDealers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedServiceNetworks()
        UsercontrolavailableselectedServiceNetworks.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UsercontrolavailableselectedServiceNetworks, False)
        If Not State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = State.MyBO.GetAvailableServiceNetworks()
            Dim selectedDv As DataView = State.MyBO.GetSelectedServiceNetworks()
            UsercontrolavailableselectedServiceNetworks.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            UsercontrolavailableselectedServiceNetworks.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UsercontrolavailableselectedServiceNetworks, True)
        End If
    End Sub

    Sub PopulateDetailControls()

        PopulateUserControlAvailableSelectedManufacturers()
        PopulateUserControlAvailableSelectedDistricts()
        PopulateUserControlAvailableSelectedDealers()
        PopulateUserControlAvailableSelectedServiceNetworks()

    End Sub

#End Region

#End Region

#Region "Detail Grid Events"


#Region "Authorized Manufacturers"

    'Private Sub DataGridDetailMfg_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetailMfg.ItemDataBound
    '    Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
    '    Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

    '    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
    '       e.Item.Cells(Me.GRID_COL_CODE).Text = dvRow(ServiceCenter.ServiceCenterManufacturerDataView.SERVICE_CENTER_COL_NAME).ToString
    '       e.Item.Cells(Me.GRID_COL_NAME).Text = dvRow(ServiceCenter.ServiceCenterManufacturerDataView.MANUFACTURER_COL_NAME).ToString
    '    End If
    'End Sub

    'Private Sub DataGridDetailMfg_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetailMfg.PageIndexChanged
    '    Me.DataGridDetailMfg.CurrentPageIndex = e.NewPageIndex
    '    Me.PopulateDetailMfgGrid()
    'End Sub

    'Private Sub DataGridDetailMfg_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetailMfg.SortCommand
    'Try
    '    If Me.State.SortExpressionDetailMfgGrid.StartsWith(e.SortExpression) Then
    '        If Me.State.SortExpressionDetailMfgGrid.EndsWith(" DESC") Then
    '            Me.State.SortExpressionDetailMfgGrid = e.SortExpression
    '        Else
    '            Me.State.SortExpressionDetailMfgGrid = e.SortExpression & " DESC"
    '        End If
    '    Else
    '        Me.State.SortExpressionDetailMfgGrid = e.SortExpression
    '    End If
    '    Me.PopulateDetailMfgGrid()
    'Catch ex As Exception
    '    Me.HandleErrors(ex, Me.ErrorCtrl)
    'End Try
    'End Sub

#End Region

#Region "Covered Districts"

    'Private Sub DataGridDetailDst_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetailDst.ItemDataBound
    '    Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
    '    Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

    '    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
    '        e.Item.Cells(Me.GRID_COL_CODE).Text = dvRow(ServiceGroup.RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME).ToString
    '        e.Item.Cells(Me.GRID_COL_NAME).Text = dvRow(ServiceGroup.RiskTypeManufacturerDataView.MANUFACTURER_COL_NAME).ToString
    '    End If
    'End Sub

    'Private Sub DataGridDetailDst_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetailDst.PageIndexChanged
    '    Me.DataGridDetailDst.CurrentPageIndex = e.NewPageIndex
    '    Me.PopulateDetailDstGrid()
    'End Sub

    'Private Sub DataGridDetailDst_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetailDst.SortCommand
    '    Try
    'If Me.State.SortExpressionDetailDstGrid.StartsWith(e.SortExpression) Then
    '    If Me.State.SortExpressionDetailDstGrid.EndsWith(" DESC") Then
    '        Me.State.SortExpressionDetailDstGrid = e.SortExpression
    '    Else
    '        Me.State.SortExpressionDetailDstGrid = e.SortExpression & " DESC"
    '    End If
    'Else
    '    Me.State.SortExpressionDetailDstGrid = e.SortExpression
    'End If
    '        Me.PopulateDetailDstGrid()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrorCtrl)
    '    End Try
    'End Sub

#End Region

#Region "Preferred Dealers"

#End Region

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
                'If (Me.State.MyBO.IsFamilyDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If State.MyBO.ConstrVoilation = False Then
                HandleErrors(ex, ErrorCtrl)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'ALR - TICKET # 1040663 -- Replaced AddConfirmMsgMessage with DisplayMessage to allow for the passing of the correct return parameters.
                'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrorCtrl.Text
            Else
                ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End If
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            State.ForEdit = True
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                '    If (Me.State.MyBO.IsFamilyDirty) Then
                State.MyBO.Save()
                State.IsNew = False
                State.HasDataChanged = True
                PopulateCountry()
                PopulateFormFromBOs()
                EnableDisableFields()
                'Caio
                'If Not txtOriginalDealer.Text = "" Then
                '    Session("ServiceCenterAssigned") = "OK"
                '    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, Me.State.MyBO, Me.State.HasDataChanged))
                'End If
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                If State.stIsComingFromDealerform Then
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, State.MyBO, State.HasDataChanged))
                End If

            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
            'Make the Date Added label and field visible - for a newly added Service Center
            ControlMgr.SetVisibleControl(Me, LabelDateAdded, True)
            ControlMgr.SetVisibleControl(Me, TextboxDateAdded, True)

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New ServiceCenter(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New ServiceCenter
            End If
            PopulateCountry()
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Dim addressDeleted As Boolean
        Try
            'Delete the Address
            State.MyBO.DeleteAndSave()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If (State.MyBO.IsDirty) Then
                '    If (Me.State.MyBO.IsFamilyDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    'Private Sub btnAcctSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcctSettings.Click
    '    Dim _d As New AcctSetting
    '    Dim dv As AcctSetting.ServiceCenterAcctSettingsDV = Nothing, blnStatus As Boolean, SCId As Guid, AcctId As Guid, SCDesc As String
    '    Try
    '        dv = _d.GetServiceCenterAcctSettingId(TextboxCode.Text)
    '        If dv.Count > 0 Then
    '            Dim dvRow As DataRowView = dv(0)
    '            AcctId = GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow(dv.ACCT_SETTINGS_ID), Byte())))
    '            SCId = GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow(dv.SERVICE_CENTER_ID), Byte())))
    '            SCDesc = dvRow(dv.DESCRIPTION).ToString
    '            blnStatus = CType(Microsoft.VisualBasic.IIf(dvRow(dv.STATUS).Equals("A"), True, False), Boolean)
    '            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AcctId, AccountingSettingForm.ReturnType.TargetType.ServiceCenter, SCId, SCDesc, blnStatus, False))
    '        Else
    '            blnStatus = CType(Microsoft.VisualBasic.IIf(TextboxStatusCode.Text.Equals("A"), True, False), Boolean)
    '            SCId = Me.State.MyBO.Id
    '            AcctId = New Guid(NOTHING_SELECTED_GUID)
    '            SCDesc = TextboxDescription.Text
    '            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AcctId, AccountingSettingForm.ReturnType.TargetType.ServiceCenter, SCId, SCDesc, blnStatus, False))
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrorCtrl)
    '    End Try
    'End Sub
#End Region

#Region "Handle-Drop"

    Private Sub moCountryDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            State.MyBO.CountryId = GetSelectedItem(moCountryDrop)
            PopulateCountry()
            PopulateDropdowns()
            PopulateFormFromBOs()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "AUTHORIZED MANUFACTURER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedManufacturers_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachManufacturers(attachedList)
                'Me.PopulateDetailMfgGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachManufacturers(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region


#Region "COVERED DISTRICTS: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDistricts_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachDistricts(attachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDistricts_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachDistricts(detachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region


#Region "PREFERRED DEALER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDealers_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachDealers(attachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachServiceNetworks(attachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDealers_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachDealers(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachServiceNetworks(detachedList)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region




    Private Sub cboPaymentMethodId_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPaymentMethodId.SelectedIndexChanged
        Try
            PopulateBOProperty(State.MyBO, "PaymentMethodId", cboPaymentMethodId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                ' SHOW THE BANK INFO USER CONTROL HERE -----
                moBankInfoController.Visible = True
                State.BankInfoBO = Nothing
                State.BankInfoBO = State.MyBO.Add_BankInfo
                UserBankInfoCtr.Bind(State.BankInfoBO, ErrorCtrl)
            Else
                moBankInfoController.Visible = False
                State.BankInfoBO = Nothing
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
End Class



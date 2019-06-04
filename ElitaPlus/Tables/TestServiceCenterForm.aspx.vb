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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Return Me.State.SvcCtrId
        End Get
        Set(ByVal Value As Guid)
            Me.State.SvcCtrId = Value
        End Set

    End Property


    Private ReadOnly Property IsNewServiceCenter() As Boolean
        Get
            Return Me.State.MyBO.IsNew
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ServiceCenter, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public SVCId As Guid
        Public PageDealerId As Guid
        Public mbIsComingFromDealerform As Boolean = False
        Public Sub New(ByVal SVCorDealerId As Guid, Optional ByVal bIsComingFromDealerform As Boolean = False)
            Me.mbIsComingFromDealerform = bIsComingFromDealerform
            If bIsComingFromDealerform Then
                Me.PageDealerId = SVCorDealerId
            Else
                Me.SVCId = SVCorDealerId
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.pageParameters = CType(Me.CallingParameters, Parameters)
                If Not Me.State.pageParameters.mbIsComingFromDealerform Then
                    Me.State.MyBO = New ServiceCenter(Me.State.pageParameters.SVCId)

                    If Me.State.MyBO.BankInfoId.Equals(Guid.Empty) Then
                        Me.State.MyBO.isBankInfoNeedDeletion = False
                    Else
                        Me.State.MyBO.isBankInfoNeedDeletion = True
                    End If
                    Me.State.stdealerid = System.Guid.Empty
                    'Me.State.stIsComingFromDealerform = False
                Else
                    Me.State.stdealerid = Me.State.pageParameters.PageDealerId
                    Me.State.stIsComingFromDealerform = True
                End If
            Else
                Me.State.stdealerid = System.Guid.Empty
                'Me.State.stIsComingFromDealerform = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Dim selectedPaymentMethod As Guid
            'Caio
            'If Not Request("OriginalDealer") = Nothing Then
            'txtOriginalDealer.Text = Request("OriginalDealer").ToString

            'End If

            Me.ErrorCtrl.Clear_Hide()
            Me.ResolveShippingFeeVisibility()
            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ServiceCenter
                    Me.State.IsNew = True
                End If
                Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
                PopulateCountry()
                PopulateDropdowns()
                Me.moBankInfoController.Visible = False
                Me.moBankInfoController.ReAssignTabIndex(BankInfoStartIndex)
                Me.moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                Me.State.BankInfoBO = Nothing
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If

            Me.cboPaymentMethodId.AutoPostBack = True
            selectedPaymentMethod = Me.GetSelectedItem(cboPaymentMethodId)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"
    Protected Sub ResolveShippingFeeVisibility()
        Me.CheckBoxShipping.Attributes.Add("onClick", "showProcessingFee(this);")
        If Not Me.CheckBoxShipping.Checked Then
            Me.LabelProcessingFee.Style.Add("Display", "'none'")
            Me.TextboxPROCESSING_FEE.Style.Add("Display", "'none'")
        Else
            Me.LabelProcessingFee.Style.Add("Display", "''")
            Me.TextboxPROCESSING_FEE.Style.Add("Display", "''")
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
            Me.cboOriginalDealer.Visible = True
            Me.lblDealer.Visible = True
        Else
            Me.cboOriginalDealer.Visible = False
            Me.lblDealer.Visible = False
        End If

        'enable/disable/visible depending on if coming from dealerform
        ControlMgr.SetEnableControl(Me, cboOriginalDealer, Me.State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, lblDealer, Me.State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, cboServiceTypeId, Me.State.stdealerid.Equals(Guid.Empty))
        ControlMgr.SetEnableControl(Me, LabelServiceTypeId, Me.State.stdealerid.Equals(Guid.Empty))

        'Now disable depending on the object state
        If Me.State.MyBO.IsNew Then

            'Enable and blank out the Service Center Code field
            ControlMgr.SetEnableControl(Me, TextboxCode, True)
            Me.TextboxCode.Text = String.Empty

            'Blank out the Service Center Description field
            Me.TextboxDescription.Text = String.Empty

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

        Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceGroupId", Me.LabelPriceGroupId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceGroupId", Me.LabelServiceGroupId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceTypeId", Me.LabelServiceTypeId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "LoanerCenterId", Me.LabelLoanerCenterId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MasterCenterId", Me.LabelMasterCenterId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.LabelCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RatingCode", Me.LabelRatingCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactName", Me.LabelContactName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OwnerName", Me.LabelOwnerName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Phone1", Me.LabelPhone1)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Phone2", Me.LabelPhone2)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Fax", Me.LabelFax)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Email", Me.LabelEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CcEmail", Me.LabelCcEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpAddress", Me.LabelFtpAddress)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxId", Me.LabelTaxId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceWarrantyDays", Me.LabelServiceWarrantyDays)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "StatusCode", Me.LabelStatusCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BusinessHours", Me.LabelBusinessHours)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DateAdded", Me.LabelDateAdded)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DateModified", Me.LabelDateLastMaintained)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "comments", Me.LabelComment1)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PaymentMethodId", Me.PaymentMethodDrpLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ProcessingFee", Me.LabelProcessingFee)
        AddressCtr.SetTheRequiredFields()

        'added by Anindita - original dealer id was not added here
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OriginalDealerId", Me.lblDealer)
        Me.ClearGridHeadersAndLabelsErrSign()

    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
        If Me.State.IsNew Then
            ' New one
            If moCountryDrop.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
        Else
            oCountry = New Country(Me.State.MyBO.CountryId)
            Me.SetSelectedItem(moCountryDrop, Me.State.MyBO.CountryId)
            Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso Me.State.IsNew) Then
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
        Me.State.Statusdv = Statusdv


        With Me.State.MyBO

            'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            'If Me.State.IsNew Then
            '    ' New one
            '    If moCountryDrop.SelectedIndex < 0 Then
            '        moCountryDrop.SelectedIndex = 0
            '    End If

            '    Me.PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            '    Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
            'End If

            Me.BindListControlToDataView(Me.cboPriceGroupId, LookupListNew.GetPriceGroupLookupList(.CountryId), , , True)

            Me.BindListControlToDataView(Me.cboServiceGroupId, LookupListNew.GetServiceGroupLookupList(.CountryId), , , True)

            Me.BindListControlToDataView(Me.cboServiceTypeId, LookupListNew.GetServiceTypeLookupList(langId), , , True)

            Me.BindListControlToDataView(Me.cboMasterCenterId, LookupListNew.GetServiceCenterLookupList(.CountryId), , , True)

            Me.BindListControlToDataView(Me.cboLoanerCenterId, LookupListNew.GetLoanerCenterLookupList(.CountryId), , , True)

            Me.BindListControlToDataView(Me.cboPaymentMethodId, LookupListNew.GetPaymentMethodLookupList(langId), , , True)

            'Me.BindListControlToDataView(Me.cboOriginalDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , , True)
            Me.BindListControlToDataView(Me.cboOriginalDealer, LookupListNew.GetOriginalDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Me.State.MyBO.Id), , , True)

            Me.BindListControlToDataView(Me.cboStatusCode, Statusdv)



        End With
    End Sub

    Protected Sub PopulateFormFromBOs()
        '     Dim oServiceCenter As ServiceCenter = TheServiceCenter
        'Dim oCountry As Country

        Dim statusdv As DataView = Me.State.Statusdv


        With Me.State.MyBO

            'Me.SetSelectedItem(Me.cboPriceGroupId, .PriceListId)
            Me.SetSelectedItem(Me.cboServiceGroupId, .ServiceGroupId)
            'Me.SetSelectedItem(Me.cboServiceTypeId, .ServiceTypeId)
            Me.SetSelectedItem(Me.cboLoanerCenterId, .LoanerCenterId)
            Me.SetSelectedItem(Me.cboMasterCenterId, .MasterCenterId)
            Me.SetSelectedItem(Me.cboPaymentMethodId, .PaymentMethodId)

            If Me.State.stIsComingFromDealerform Then
                Me.SetSelectedItem(Me.cboOriginalDealer, Me.State.stdealerid)
            Else
                If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR And _
                    Me.State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    Me.cboOriginalDealer.SelectedIndex = Me.NOTHING_SELECTED
                ElseIf LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR And _
                    Not Me.State.MyBO.OriginalDealerId.Equals(Guid.Empty) Then
                    If Me.State.MyBO.IsNew Then
                        Me.cboOriginalDealer.SelectedIndex = Me.NOTHING_SELECTED
                    Else
                        Me.SetSelectedItem(Me.cboOriginalDealer, .OriginalDealerId)
                    End If
                Else
                    Me.SetSelectedItem(Me.cboOriginalDealer, System.Guid.Empty)
                End If
            End If


            Me.PopulateControlFromBOProperty(Me.TextboxCode, .Code)
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            Me.PopulateControlFromBOProperty(Me.TextboxRatingCode, .RatingCode)
            Me.PopulateControlFromBOProperty(Me.TextboxContactName, .ContactName)
            Me.PopulateControlFromBOProperty(Me.TextboxOwnerName, .OwnerName)
            Me.PopulateControlFromBOProperty(Me.TextboxPhone1, .Phone1)
            Me.PopulateControlFromBOProperty(Me.TextboxPhone2, .Phone2)
            Me.PopulateControlFromBOProperty(Me.TextboxFax, .Fax)
            Me.PopulateControlFromBOProperty(Me.TextboxEmail, .Email)
            Me.PopulateControlFromBOProperty(Me.TextboxCcEmail, .CcEmail)
            Me.PopulateControlFromBOProperty(Me.TextboxFtpAddress, .FtpAddress)
            Me.PopulateControlFromBOProperty(Me.TextboxTaxId, .TaxId)
            Me.PopulateControlFromBOProperty(Me.TextboxServiceWarrantyDays, .ServiceWarrantyDays)

            ''Me.PopulateControlFromBOProperty(Me.TextboxStatusCode, .StatusCode)
            Me.PopulateControlFromBOProperty(Me.cboStatusCode, LookupListNew.GetIdFromCode(statusdv, .StatusCode))

            Me.PopulateControlFromBOProperty(Me.TextboxBusinessHours, .BusinessHours)
            Me.PopulateControlFromBOProperty(Me.CheckBoxDefaultToEmail, .DefaultToEmailFlag)
            Me.PopulateControlFromBOProperty(Me.CheckBoxIvaResponsible, .IvaResponsibleFlag)
            Me.PopulateControlFromBOProperty(Me.TextboxDateAdded, .CreatedDate)
            Me.PopulateControlFromBOProperty(Me.TextboxDateLastMaintained, .ModifiedDate)
            Me.PopulateControlFromBOProperty(Me.TextboxComment, .Comments)
            Me.PopulateControlFromBOProperty(Me.CheckBoxShipping, .Shipping)
            If .Shipping Then
                Me.ResolveShippingFeeVisibility()
                Me.PopulateControlFromBOProperty(Me.TextboxPROCESSING_FEE, .ProcessingFee)
            End If

            AddressCtr.Bind(.Address)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                Me.moBankInfoController.Visible = True
                Me.State.BankInfoBO = Me.State.MyBO.Add_BankInfo
                'Me.State.BankInfoBO.ACCOUNT_NAME = String.Empty
                Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO, Me.ErrorCtrl)
            Else
                Me.State.BankInfoBO = Nothing
                Me.moBankInfoController.Visible = False
            End If

        End With

        PopulateDetailControls()

    End Sub

    Protected Sub PopulateBOsFromForm()

        With Me.State.MyBO

            '*Caio
            'If txtOriginalDealer.Text = "" Then
            '    Me.PopulateBOProperty(Me.State.MyBO, "OriginalDealerId", Me.cboOriginalDealer)
            'Else
            '    Me.State.MyBO.OriginalDealerId = New Guid(Request("OriginalDealer").ToString)
            'End If
            '*


            If LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList, ElitaPlusIdentity.Current.ActiveUser.CompanyId) = Codes.COMPANY__VBR And _
                Me.cboOriginalDealer.SelectedValue.Trim.Length > 0 Then
                Me.PopulateBOProperty(Me.State.MyBO, "OriginalDealerId", Me.cboOriginalDealer)
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "PriceGroupId", Me.cboPriceGroupId)
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceGroupId", Me.cboServiceGroupId)
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceTypeId", Me.cboServiceTypeId)
            Me.PopulateBOProperty(Me.State.MyBO, "LoanerCenterId", Me.cboLoanerCenterId)
            Me.PopulateBOProperty(Me.State.MyBO, "MasterCenterId", Me.cboMasterCenterId)
            Me.PopulateBOProperty(Me.State.MyBO, "CountryId", moCountryDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.TextboxCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            Me.PopulateBOProperty(Me.State.MyBO, "RatingCode", Me.TextboxRatingCode)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactName", Me.TextboxContactName)
            Me.PopulateBOProperty(Me.State.MyBO, "OwnerName", Me.TextboxOwnerName)
            Me.PopulateBOProperty(Me.State.MyBO, "Phone1", Me.TextboxPhone1)
            Me.PopulateBOProperty(Me.State.MyBO, "Phone2", Me.TextboxPhone2)
            Me.PopulateBOProperty(Me.State.MyBO, "Fax", Me.TextboxFax)
            Me.PopulateBOProperty(Me.State.MyBO, "Email", Me.TextboxEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "CcEmail", Me.TextboxCcEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "FtpAddress", Me.TextboxFtpAddress)
            Me.PopulateBOProperty(Me.State.MyBO, "TaxId", Me.TextboxTaxId)
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceWarrantyDays", Me.TextboxServiceWarrantyDays)

            ''If (Not (Me.TextboxStatusCode.Text = Nothing)) Then
            ''    Me.TextboxStatusCode.Text = Me.TextboxStatusCode.Text.ToUpper
            ''End If
            ''Me.PopulateBOProperty(Me.State.MyBO, "StatusCode", Me.TextboxStatusCode)
            Me.PopulateBOProperty(Me.State.MyBO, "StatusCode", LookupListNew.GetCodeFromId(Me.State.Statusdv, New Guid(Me.cboStatusCode.SelectedValue)))
            Me.PopulateBOProperty(Me.State.MyBO, "BusinessHours", Me.TextboxBusinessHours)
            Me.PopulateBOProperty(Me.State.MyBO, "DefaultToEmailFlag", Me.CheckBoxDefaultToEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "IvaResponsibleFlag", Me.CheckBoxIvaResponsible)
            Me.PopulateBOProperty(Me.State.MyBO, "Comments", Me.TextboxComment)
            Me.PopulateBOProperty(Me.State.MyBO, "PaymentMethodId", Me.cboPaymentMethodId)


            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                'Me.moBankInfoController.Visible = True
                Me.State.MyBO.BankInfoId = Me.State.BankInfoBO.Id
                Me.UserBankInfoCtr.PopulateBOFromControl()
            Else
                Me.State.BankInfoBO = Nothing
                Me.State.MyBO.BankInfoId = Nothing
                'Me.moBankInfoController.Visible = False
            End If

            'Set the following 2 BO Properties based on whether the Selected Items in 
            'the MASTER CENTER and LOANER CENTER Dropdown Lists are "Nothing Selected" or an actual value
            'If "Nothing Selected", then the corressponding FLAG value should be = "N", else "Y"

            If Me.State.ForEdit = True Then
                Me.State.MyBO.Address.AddressRequiredServCenter = True
            End If
            Me.AddressCtr.PopulateBOFromControl()

            If (Me.State.MyBO.IsDirty) Then
                'If (Me.State.MyBO.IsFamilyDirty) Then
                If (cboMasterCenterId.SelectedIndex = NOTHING_SELECTED) Then
                    'Nothing selected
                    Me.PopulateBOProperty(Me.State.MyBO, "MasterFlag", "N")
                Else
                    Me.PopulateBOProperty(Me.State.MyBO, "MasterFlag", "Y")
                End If

                If (cboLoanerCenterId.SelectedIndex = NOTHING_SELECTED) Then
                    'Nothing selected
                    Me.PopulateBOProperty(Me.State.MyBO, "LoanerFlag", "N")
                Else
                    Me.PopulateBOProperty(Me.State.MyBO, "LoanerFlag", "Y")
                End If
            End If

            Me.PopulateBOProperty(Me.State.MyBO, "Shipping", Me.CheckBoxShipping)
            If Me.State.MyBO.Shipping Then
                Me.PopulateBOProperty(Me.State.MyBO, "ProcessingFee", Me.TextboxPROCESSING_FEE)
            Else
                Me.State.MyBO.ProcessingFee = Nothing
            End If

        End With
        If Me.ErrCollection.Count > 0 Then
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
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New ServiceCenter
        Me.State.IsNew = True
        PopulateCountry()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.PopulateBOsFromForm()

        Dim newObj As New ServiceCenter
        newObj.Copy(Me.State.MyBO)

        If Not newObj.BankInfoId.Equals(Guid.Empty) Then
            ' copy the original bankinfo
            newObj.BankInfoId = Guid.Empty
            newObj.Add_BankInfo()
            newObj.BankInfoId = newObj.CurrentBankInfo.Id
            newObj.CurrentBankInfo.CopyFrom(Me.State.MyBO.CurrentBankInfo)
            Me.State.BankInfoBO = newObj.CurrentBankInfo
            Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO, Me.ErrorCtrl)
        End If

        Me.State.MyBO = newObj
        Me.State.MyBO.Code = Nothing
        Me.State.MyBO.Description = Nothing
        PopulateCountry()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New ServiceCenter
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)

    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#Region "Detail Tabs"

    Sub PopulateUserControlAvailableSelectedManufacturers()
        Me.UserControlAvailableSelectedManufacturers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableManufacturers()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedManufacturers()
            Me.UserControlAvailableSelectedManufacturers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedManufacturers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedDistricts()
        Me.UserControlAvailableSelectedDistricts.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDistricts, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableDistricts()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedDistricts()
            Me.UserControlAvailableSelectedDistricts.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedDistricts.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDistricts, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedDealers()
        Me.UserControlAvailableSelectedDealers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableDealers()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedDealers()
            Me.UserControlAvailableSelectedDealers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedDealers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, True)
        End If
    End Sub

    Sub PopulateUserControlAvailableSelectedServiceNetworks()
        Me.UsercontrolavailableselectedServiceNetworks.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UsercontrolavailableselectedServiceNetworks, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView = Me.State.MyBO.GetAvailableServiceNetworks()
            Dim selectedDv As DataView = Me.State.MyBO.GetSelectedServiceNetworks()
            Me.UsercontrolavailableselectedServiceNetworks.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UsercontrolavailableselectedServiceNetworks.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
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

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
                'If (Me.State.MyBO.IsFamilyDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If Me.State.MyBO.ConstrVoilation = False Then
                Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'ALR - TICKET # 1040663 -- Replaced AddConfirmMsgMessage with DisplayMessage to allow for the passing of the correct return parameters.
                'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrorCtrl.Text
            Else
                Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.State.ForEdit = True
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                '    If (Me.State.MyBO.IsFamilyDirty) Then
                Me.State.MyBO.Save()
                Me.State.IsNew = False
                Me.State.HasDataChanged = True
                PopulateCountry()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                'Caio
                'If Not txtOriginalDealer.Text = "" Then
                '    Session("ServiceCenterAssigned") = "OK"
                '    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, Me.State.MyBO, Me.State.HasDataChanged))
                'End If
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                If Me.State.stIsComingFromDealerform Then
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Save, Me.State.MyBO, Me.State.HasDataChanged))
                End If

            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
            'Make the Date Added label and field visible - for a newly added Service Center
            ControlMgr.SetVisibleControl(Me, LabelDateAdded, True)
            ControlMgr.SetVisibleControl(Me, TextboxDateAdded, True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New ServiceCenter(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New ServiceCenter
            End If
            PopulateCountry()
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Dim addressDeleted As Boolean
        Try
            'Delete the Address
            Me.State.MyBO.DeleteAndSave()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If (Me.State.MyBO.IsDirty) Then
                '    If (Me.State.MyBO.IsFamilyDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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

    Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
            PopulateCountry()
            PopulateDropdowns()
            Me.PopulateFormFromBOs()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "AUTHORIZED MANUFACTURER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedManufacturers_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachManufacturers(attachedList)
                'Me.PopulateDetailMfgGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachManufacturers(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region


#Region "COVERED DISTRICTS: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDistricts_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachDistricts(attachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDistricts_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDistricts.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachDistricts(detachedList)
                'Me.PopulateDetailDstGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region


#Region "PREFERRED DEALER: Attach - Detach Event Handlers"


    Private Sub UserControlAvailableSelectedDealers_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachDealers(attachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachServiceNetworks(attachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedDealers_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDealers.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachDealers(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UsercontrolavailableselectedServiceNetworks_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UsercontrolavailableselectedServiceNetworks.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachServiceNetworks(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region




    Private Sub cboPaymentMethodId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPaymentMethodId.SelectedIndexChanged
        Try
            Me.PopulateBOProperty(Me.State.MyBO, "PaymentMethodId", Me.cboPaymentMethodId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.MyBO.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                ' SHOW THE BANK INFO USER CONTROL HERE -----
                Me.moBankInfoController.Visible = True
                Me.State.BankInfoBO = Nothing
                Me.State.BankInfoBO = Me.State.MyBO.Add_BankInfo
                Me.UserBankInfoCtr.Bind(Me.State.BankInfoBO, Me.ErrorCtrl)
            Else
                Me.moBankInfoController.Visible = False
                Me.State.BankInfoBO = Nothing
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
End Class



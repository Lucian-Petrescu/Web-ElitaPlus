Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class AccountingInterface
    Inherits ElitaPlusPage

#Region "Constants"

    Public Const URL As String = "Interfaces/AccountingInterface.aspx"
    Public Const PAGETITLE As String = "ACCOUNTING_INTERFACE"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const YESNO As String = "YESNO"
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const YES_STRING As String = "Y"
    Private Const NO_STRING As String = "N"
    Private Const ONE_ITEM As Integer = 1
    Private Const RETURN_SUCCESS As String = "0"

    Protected WithEvents moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl

#End Region

#Region "Properties"

    Public ReadOnly Property UserCompanyMultipleDrop() As Common.MultipleColumnDDLabelControl
        Get
            If moUserCompanyMultipleDrop Is Nothing Then
                moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), Common.MultipleColumnDDLabelControl)
            End If
            Return moUserCompanyMultipleDrop
        End Get
    End Property

#End Region

#Region "PAGE STATE"

    Class MyState
        'Public dvYesNo As DataView
        Public dvYesNo As DataElements.ListItem()
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Page Init"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        ErrControllerMaster.Clear_Hide()

        Try

            DisplayProgressBarOnClick(btnExecute, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)

            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                PopulateAll()

                'Add Enable/Disable Code to Radios for the Event List
                rdoEventAll.Attributes.Add("onclick", "document.getElementById('" & ddlAccountingEvent.ClientID & "').disabled=true;")
                rdoEventSpecific.Attributes.Add("onclick", "document.getElementById('" & ddlAccountingEvent.ClientID & "').disabled=false;")

            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Event Handlers"


    Private Sub moCompanyDropDown_SelectedIndexChanged(moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl) Handles moUserCompanyMultipleDrop.SelectedDropChanged
        Try
            If UserCompanyMultipleDrop.SelectedIndex = 0 Then
                ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                Exit Sub
            End If

            Dim _company As New Company(UserCompanyMultipleDrop.SelectedGuid)
            'BindListControlToDataView(Me.ddlAccountingEvent, BusinessObjectsNew.LookupListNew.getAccountingEvents(_company.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            PopulateAccountingEventDropdown(_company.AcctCompanyId)

            SetVendorList(_company.AcctCompanyId)

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub PopulateAccountingEventDropdown(AcctCompanyId As Guid)
        Dim AccountingEvents As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.AccountingEventByAccountingCompany,
                                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .AccountingCompanyId = AcctCompanyId,
                                                                      .LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                                                                    })

        ddlAccountingEvent.Populate(AccountingEvents.ToArray(),
                                         New PopulateOptions() With
                                         {
                                          .AddBlankItem = True
                                         })
    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        ReturnToTabHomePage()
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As System.EventArgs) Handles btnExecute.Click

        Dim _felitaEngine As FelitaEngine
        Dim dr As FelitaEngineDs.FelitaEngineRow
        Dim _FEDs As New FelitaEngineDs
        Dim strReturn As String

        Try

            If Not ValidateForm() Then Exit Sub

            If _FEDs.Tables.Count = 0 Then
                _FEDs.Tables.Add(New FelitaEngineDs.FelitaEngineDataTable)
            End If

            dr = CType(_FEDs.Tables(0).NewRow, FelitaEngineDs.FelitaEngineRow)

            dr.CompanyId = UserCompanyMultipleDrop.SelectedCode

            'If LookupListNew.GetCodeFromId(Me.State.dvYesNo, New Guid(Me.ddlVendorFiles.SelectedValue)) = YES_STRING Then
            '    dr.VendorFiles = "1"
            'Else
            '    dr.VendorFiles = "0"
            'End If

            Dim yes_Code As String = (From lst In State.dvYesNo
                                      Where lst.ListItemId = New Guid(ddlVendorFiles.SelectedValue)
                                      Select lst.Code).FirstOrDefault()

            If yes_Code = YES_STRING Then
                dr.VendorFiles = "1"
            Else
                dr.VendorFiles = "0"
            End If

            If rdoEventAll.Checked Then
                dr.AccountingEventId = String.Empty
                'If LookupListNew.GetCodeFromId(Me.State.dvYesNo, New Guid(Me.ddlIncludePending.SelectedValue)) = YES_STRING Then
                '    'ALR - run the accounting events behind the scenes
                '    AcctTransLog.PopulateAccountingEvents(Me.UserCompanyMultipleDrop.SelectedCode, Guid.Empty, CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
                'End If
                Dim yes_Code1 As String = (From lst In State.dvYesNo
                                           Where lst.ListItemId = New Guid(ddlIncludePending.SelectedValue)
                                           Select lst.Code).FirstOrDefault()

                If yes_Code1 = YES_STRING Then
                    'ALR - run the accounting events behind the scenes
                    AcctTransLog.PopulateAccountingEvents(UserCompanyMultipleDrop.SelectedCode, Guid.Empty, CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
                End If
            ElseIf ddlAccountingEvent.SelectedValue = NOTHING_SELECTED Then
                dr.AccountingEventId = FelitaEngine.NO_EVENTS

            Else
                dr.AccountingEventId = GuidControl.GuidToHexString(New Guid(ddlAccountingEvent.SelectedValue))

                'If LookupListNew.GetCodeFromId(Me.State.dvYesNo, New Guid(Me.ddlIncludePending.SelectedValue)) = YES_STRING Then
                '    'ALR - run the accounting events behind the scenes
                '    AcctTransLog.PopulateAccountingEvents(Me.UserCompanyMultipleDrop.SelectedCode, New Guid(Me.ddlAccountingEvent.SelectedValue), CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
                'End If

                Dim yes_Code2 As String = (From lst In State.dvYesNo
                                           Where lst.ListItemId = New Guid(ddlIncludePending.SelectedValue)
                                           Select lst.Code).FirstOrDefault()

                If yes_Code2 = YES_STRING Then
                    'ALR - run the accounting events behind the scenes
                    AcctTransLog.PopulateAccountingEvents(UserCompanyMultipleDrop.SelectedCode, New Guid(ddlAccountingEvent.SelectedValue), CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
                End If

            End If

            _FEDs.Tables(0).Rows.Add(dr)

            _felitaEngine = New FelitaEngine(_FEDs)
            'BUG-176521
            Dim _company As New Company(UserCompanyMultipleDrop.SelectedGuid)
            If Not _company.AcctCompanyId.Equals(System.Guid.Empty) Then
                strReturn = _felitaEngine.ProcessWSRequest()
            Else
                Throw New GUIException(Message.MSG_ACCT_COMPANY_NOT_CONFIGURED, Assurant.ElitaPlus.Common.ErrorCodes.ACCOUNTING_COMPANY_NOT_CONFIGURED_ERR)
            End If
            If strReturn = RETURN_SUCCESS Then
                AddInfoMsg(ElitaPlusWebApp.Message.MSG_INTERFACES_HAS_COMPLETED)
            Else
                ErrControllerMaster.AddErrorAndShow(strReturn)
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub


#End Region

#Region "POPULATE"

    Private Sub PopulateAll()
        Try

            'Fill Companies
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            'Fill dropdowns based on Events and Accounting Companies
            'Me.State.dvYesNo = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
            'BindListControlToDataView(Me.ddlVendorFiles, Me.State.dvYesNo)
            'BindListControlToDataView(Me.ddlIncludePending, Me.State.dvYesNo)

            Dim YesNoList As DataElements.ListItem() =
               CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                               languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            State.dvYesNo = YesNoList

            ddlVendorFiles.Populate(YesNoList, New PopulateOptions() With {.AddBlankItem = True})
            ddlIncludePending.Populate(YesNoList.ToArray(), New PopulateOptions() With {.AddBlankItem = True})

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.ChangeEnabledControlProperty(False)
                Dim _company As New Company(UserCompanyMultipleDrop.SelectedGuid)
                'BindListControlToDataView(Me.ddlAccountingEvent, BusinessObjectsNew.LookupListNew.getAccountingEvents(_company.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                PopulateAccountingEventDropdown(_company.AcctCompanyId)

                'BUG-176521
                If Not _company.AcctCompanyId.Equals(System.Guid.Empty) Then
                    SetVendorList(_company.AcctCompanyId)
                End If
            End If
        Catch ex As Exception
            ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try

    End Sub

#End Region

#Region "Controlling Logic"

    Private Function ValidateForm() As Boolean

        Dim result As Boolean = True

        Try

            If UserCompanyMultipleDrop.SelectedIndex = 0 Then
                ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                result = False
            End If

            If rdoEventSpecific.Checked AndAlso (ddlAccountingEvent.SelectedItem Is Nothing OrElse ddlAccountingEvent.SelectedValue = NOTHING_SELECTED) Then
                rdoEventSpecific.Checked = False
                rdoEventAll.Checked = True
            End If

            Return result
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub SetVendorList(AccountingCompanyId As Guid)

        Dim _AcctCompany As New AcctCompany(AccountingCompanyId)
        Dim _acctExtension As String

        If _AcctCompany IsNot Nothing Then
            _acctExtension = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _AcctCompany.AcctSystemId)
            If _acctExtension.Equals(BusinessObjectsNew.FelitaEngine.FELITA_PREFIX) Then
                ControlMgr.SetEnableControl(Me, ddlVendorFiles, True)
            Else
                'Me.SetSelectedItem(Me.ddlVendorFiles, LookupListNew.GetIdFromCode(Me.State.dvYesNo, Me.NO_STRING))
                Dim no_Id As Guid = (From lst In State.dvYesNo
                                     Where lst.Code = NO_STRING
                                     Select lst.ListItemId).FirstOrDefault()
                SetSelectedItem(ddlVendorFiles, no_Id)
                ControlMgr.SetEnableControl(Me, ddlVendorFiles, False)
            End If
        End If
    End Sub

#End Region


End Class
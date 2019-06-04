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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()

        Try

            Me.DisplayProgressBarOnClick(Me.btnExecute, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)

            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                PopulateAll()

                'Add Enable/Disable Code to Radios for the Event List
                Me.rdoEventAll.Attributes.Add("onclick", "document.getElementById('" & Me.ddlAccountingEvent.ClientID & "').disabled=true;")
                Me.rdoEventSpecific.Attributes.Add("onclick", "document.getElementById('" & Me.ddlAccountingEvent.ClientID & "').disabled=false;")

            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Event Handlers"


    Private Sub moCompanyDropDown_SelectedIndexChanged(ByVal moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl) Handles moUserCompanyMultipleDrop.SelectedDropChanged
        Try
            If Me.UserCompanyMultipleDrop.SelectedIndex = 0 Then
                Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                Exit Sub
            End If

            Dim _company As New Company(Me.UserCompanyMultipleDrop.SelectedGuid)
            'BindListControlToDataView(Me.ddlAccountingEvent, BusinessObjectsNew.LookupListNew.getAccountingEvents(_company.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            PopulateAccountingEventDropdown(_company.AcctCompanyId)

            SetVendorList(_company.AcctCompanyId)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
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

        Me.ddlAccountingEvent.Populate(AccountingEvents.ToArray(),
                                         New PopulateOptions() With
                                         {
                                          .AddBlankItem = True
                                         })
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.ReturnToTabHomePage()
    End Sub

    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click

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

            dr.CompanyId = Me.UserCompanyMultipleDrop.SelectedCode

            'If LookupListNew.GetCodeFromId(Me.State.dvYesNo, New Guid(Me.ddlVendorFiles.SelectedValue)) = YES_STRING Then
            '    dr.VendorFiles = "1"
            'Else
            '    dr.VendorFiles = "0"
            'End If

            Dim yes_Code As String = (From lst In Me.State.dvYesNo
                                      Where lst.ListItemId = New Guid(Me.ddlVendorFiles.SelectedValue)
                                      Select lst.Code).FirstOrDefault()

            If yes_Code = YES_STRING Then
                dr.VendorFiles = "1"
            Else
                dr.VendorFiles = "0"
            End If

            If Me.rdoEventAll.Checked Then
                dr.AccountingEventId = String.Empty
                'If LookupListNew.GetCodeFromId(Me.State.dvYesNo, New Guid(Me.ddlIncludePending.SelectedValue)) = YES_STRING Then
                '    'ALR - run the accounting events behind the scenes
                '    AcctTransLog.PopulateAccountingEvents(Me.UserCompanyMultipleDrop.SelectedCode, Guid.Empty, CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
                'End If
                Dim yes_Code1 As String = (From lst In Me.State.dvYesNo
                                           Where lst.ListItemId = New Guid(Me.ddlIncludePending.SelectedValue)
                                           Select lst.Code).FirstOrDefault()

                If yes_Code1 = YES_STRING Then
                    'ALR - run the accounting events behind the scenes
                    AcctTransLog.PopulateAccountingEvents(Me.UserCompanyMultipleDrop.SelectedCode, Guid.Empty, CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
                End If
            ElseIf Me.ddlAccountingEvent.SelectedValue = NOTHING_SELECTED Then
                dr.AccountingEventId = FelitaEngine.NO_EVENTS

            Else
                dr.AccountingEventId = GuidControl.GuidToHexString(New Guid(Me.ddlAccountingEvent.SelectedValue))

                'If LookupListNew.GetCodeFromId(Me.State.dvYesNo, New Guid(Me.ddlIncludePending.SelectedValue)) = YES_STRING Then
                '    'ALR - run the accounting events behind the scenes
                '    AcctTransLog.PopulateAccountingEvents(Me.UserCompanyMultipleDrop.SelectedCode, New Guid(Me.ddlAccountingEvent.SelectedValue), CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
                'End If

                Dim yes_Code2 As String = (From lst In Me.State.dvYesNo
                                           Where lst.ListItemId = New Guid(Me.ddlIncludePending.SelectedValue)
                                           Select lst.Code).FirstOrDefault()

                If yes_Code2 = YES_STRING Then
                    'ALR - run the accounting events behind the scenes
                    AcctTransLog.PopulateAccountingEvents(Me.UserCompanyMultipleDrop.SelectedCode, New Guid(Me.ddlAccountingEvent.SelectedValue), CType(If(dr.VendorFiles.Equals(1.ToString), True, False), Boolean))
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
                Me.AddInfoMsg(ElitaPlusWebApp.Message.MSG_INTERFACES_HAS_COMPLETED)
            Else
                Me.ErrControllerMaster.AddErrorAndShow(strReturn)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
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
            Me.State.dvYesNo = YesNoList

            Me.ddlVendorFiles.Populate(YesNoList, New PopulateOptions() With {.AddBlankItem = True})
            Me.ddlIncludePending.Populate(YesNoList.ToArray(), New PopulateOptions() With {.AddBlankItem = True})

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
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
            Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try

    End Sub

#End Region

#Region "Controlling Logic"

    Private Function ValidateForm() As Boolean

        Dim result As Boolean = True

        Try

            If Me.UserCompanyMultipleDrop.SelectedIndex = 0 Then
                Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                result = False
            End If

            If Me.rdoEventSpecific.Checked AndAlso (Me.ddlAccountingEvent.SelectedItem Is Nothing OrElse Me.ddlAccountingEvent.SelectedValue = Me.NOTHING_SELECTED) Then
                Me.rdoEventSpecific.Checked = False
                Me.rdoEventAll.Checked = True
            End If

            Return result
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub SetVendorList(ByVal AccountingCompanyId As Guid)

        Dim _AcctCompany As New AcctCompany(AccountingCompanyId)
        Dim _acctExtension As String

        If Not _AcctCompany Is Nothing Then
            _acctExtension = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _AcctCompany.AcctSystemId)
            If _acctExtension.Equals(BusinessObjectsNew.FelitaEngine.FELITA_PREFIX) Then
                ControlMgr.SetEnableControl(Me, Me.ddlVendorFiles, True)
            Else
                'Me.SetSelectedItem(Me.ddlVendorFiles, LookupListNew.GetIdFromCode(Me.State.dvYesNo, Me.NO_STRING))
                Dim no_Id As Guid = (From lst In Me.State.dvYesNo
                                     Where lst.Code = Me.NO_STRING
                                     Select lst.ListItemId).FirstOrDefault()
                Me.SetSelectedItem(Me.ddlVendorFiles, no_Id)
                ControlMgr.SetEnableControl(Me, Me.ddlVendorFiles, False)
            End If
        End If
    End Sub

#End Region


End Class
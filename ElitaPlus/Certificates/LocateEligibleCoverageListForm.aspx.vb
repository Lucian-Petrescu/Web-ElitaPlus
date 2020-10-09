Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Certificates

    Partial Public Class LocateEligibleCoverageListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Public Const URL As String = "~/Certificates/LocateEligibleCoverageListForm.aspx"
        Private Const SEARCH_EXCEPTION As String = "LOCATE_COVERAGE_SEARCH_EXCEPTION"
        Private Const NO_DATA As String = " - "
        Private Const PROTECTION_AND_EVENT_DETAILS As String = "PROTECTION_AND_EVENT_DETAILS"
        Private Const CERTIFICATES As String = "CERTIFICATES"
        Private Const COVERAGE_SELECTED As String = "coverage_selected"
        Private Const COVERAGE_SELECTED_LATE_REPORTED As String = "coverage_selected_late_reported"
        Private Const NO_RECORD_FOUND_DEFAULT_BLANK As Integer = 1
        Private Const NO_RECORD_FOUND As Integer = 0
#End Region

#Region "Page State"

#Region "MyState"

        Class MyState
            Public MyBO As Certificate
            Public DateOfLoss As Date
            Public DateReported As Date
            Public CallerName As String
            Public ProblemDescription As String
            Public objClaimedEquipment As ClaimEquipment
        End Class

#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get                
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property

#End Region

#Region "Page Events"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State.MyBO IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & CERTIFICATES & " " & State.MyBO.CertNumber & ElitaBase.Sperator & _
                        "File New Claim"
                End If
            End If
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            MasterPage.MessageController.Clear()

            Try
                SetDefaultButton(moDateOfLossText, btnSearch)
                If cboRiskType.SelectedIndex.Equals(NO_ITEM_SELECTED_INDEX) Then
                    EnableControls(False)
                End If
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(CERTIFICATES)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
                UpdateBreadCrum()

                If Not IsPostBack Then

                    Dim objTemp As Object
                    objTemp = NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE)
                    If objTemp IsNot Nothing Then
                        State.MyBO = CType(objTemp, Certificate)
                    ElseIf Navigator.CallingParameters IsNot Nothing Then
                        State.MyBO = New Certificate(CType(Navigator.CallingParameters, Guid))
                    End If

                    objTemp = NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS)
                    If objTemp IsNot Nothing Then
                        State.DateOfLoss = CType(objTemp, Date)
                    End If

                    objTemp = NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED)
                    If objTemp IsNot Nothing Then
                        State.DateReported = CType(objTemp, Date)
                    Else
                        State.DateReported = Date.Today
                    End If

                    Dim companyBO As Assurant.ElitaPlus.BusinessObjectsNew.Company = New Assurant.ElitaPlus.BusinessObjectsNew.Company(State.MyBO.CompanyId)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, BtnDateOfLoss, True)
                    RiskTypeTR.Visible = False

                    moDateOfLossText.Text = Nothing
                    AddCalendar_New(BtnDateOfLoss, moDateOfLossText)
                    AddCalendar_New(BtnDateReported, txtDateReported)

                    If State.DateReported > Date.MinValue Then PopulateControlFromBOProperty(txtDateReported, GetDateFormattedStringNullable(New DateType(State.DateReported)))
                    If State.DateOfLoss > Date.MinValue Then PopulateControlFromBOProperty(moDateOfLossText, GetDateFormattedStringNullable(New DateType(State.DateOfLoss)))

                    PopulateProtectionAndEventDetail()

                    If DirectCast(NavController, Assurant.Common.AppNavigationControl.NavControllerBase).PrevNavState.Url.Contains("CertItemForm") Then
                        RestorePageState()
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "New Look And Feel"
        Private Sub RestorePageState()
            If NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME) IsNot Nothing Then
                If Not NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME).ToString.Equals(String.Empty) Then
                    TextCallerName.Text = NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME).ToString
                End If
            End If
            If NavController.FlowSession(FlowSessionKeys.SESSION_PROBLEM_DESCRIPTION) IsNot Nothing Then
                If Not NavController.FlowSession(FlowSessionKeys.SESSION_PROBLEM_DESCRIPTION).ToString.Equals(String.Empty) Then
                    TextProblemDescription.Text = NavController.FlowSession(FlowSessionKeys.SESSION_PROBLEM_DESCRIPTION).ToString
                End If
            End If
            If NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE) IsNot Nothing Then
                If Not NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE).ToString.Equals(String.Empty) Then
                    If PopulateAndValidateInputs() Then
                        'Me.BindListControlToDataView(Me.cboRiskType, LookupListNew.LoadRiskTypes(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                        oListContext.CertId = State.MyBO.Id
                        oListContext.DateOfLoss = State.DateOfLoss
                        oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                        cboRiskType.Populate(CommonConfigManager.Current.ListManager.GetList("RiskTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), oListContext), New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
                        ElitaPlusPage.BindSelectItem(CStr(NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE)), cboRiskType)
                    End If
                End If
            End If
            If NavController.FlowSession(FlowSessionKeys.SESSION_COVERAGE_TYPE) IsNot Nothing Then
                If Not NavController.FlowSession(FlowSessionKeys.SESSION_COVERAGE_TYPE).ToString.Equals(String.Empty) Then
                    If PopulateAndValidateInputs() Then
                        PopulateDropdown()
                        'Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.LoadCoverageTypes(State.MyBO.Id, New Guid(Me.NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE).ToString), ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                        Dim listcontextForcoveragetypes As ListContext = New ListContext()
                        listcontextForcoveragetypes.CertId = State.MyBO.Id
                        listcontextForcoveragetypes.CertItemId = New Guid(NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE).ToString)
                        listcontextForcoveragetypes.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                        listcontextForcoveragetypes.DateOfLoss = State.DateOfLoss
                        Dim CoverageTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForcoveragetypes)
                        cboCoverageType.Populate(CoverageTypeList, New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })
                        DisableControls()
                        ElitaPlusPage.BindSelectItem(CStr(NavController.FlowSession(FlowSessionKeys.SESSION_COVERAGE_TYPE)), cboCoverageType)
                        If Not NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE).ToString.Equals(String.Empty) Then
                            ElitaPlusPage.BindSelectItem(CStr(NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE)), cboRiskType)
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub EnableControls(bToggle As Boolean)
            ControlMgr.SetVisibleControl(Me, SearchResultPanel, bToggle)
            ControlMgr.SetVisibleControl(Me, btnContinue, bToggle)
        End Sub

        Private Sub DisableControls()
            If cboRiskType.Items.Count = NO_RECORD_FOUND_DEFAULT_BLANK AndAlso cboCoverageType.Items.Count = NO_RECORD_FOUND_DEFAULT_BLANK Then
                HandleGridMessages(NO_RECORD_FOUND, True)
                ControlMgr.SetVisibleControl(Me, SearchResultPanel, False)
                ControlMgr.SetVisibleControl(Me, btnContinue, False)
            End If
        End Sub

        Public Sub PopulateDropdown()
            Try
                EnableControls(True)
                Dim listcontextForRiskTypeList As ListContext = New ListContext()
                listcontextForRiskTypeList.CertId = State.MyBO.Id
                listcontextForRiskTypeList.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                listcontextForRiskTypeList.DateOfLoss = State.DateOfLoss
                Dim risktypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCertificate", , listcontextForRiskTypeList)

                cboRiskType.Populate(risktypeList, New PopulateOptions() With
                    {
                       .AddBlankItem = True
                    })
                'Me.BindListControlToDataView(Me.cboRiskType, LookupListNew.LoadRiskTypes(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                If cboRiskType.Items.Count <= 1 Then
                    RiskTypeTR.Visible = False
                ElseIf cboRiskType.Items.Count = 2 Then
                    cboRiskType.SelectedIndex = 1
                    RiskTypeTR.Visible = False
                Else
                    RiskTypeTR.Visible = True
                End If
                If Not RiskTypeTR.Visible Then
                    Dim listcontextForcoveragetypes As ListContext = New ListContext()
                    listcontextForcoveragetypes.CertId = State.MyBO.Id
                    listcontextForcoveragetypes.CertItemId = New Guid(cboRiskType.SelectedValue)
                    listcontextForcoveragetypes.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    listcontextForcoveragetypes.DateOfLoss = State.DateOfLoss
                    Dim CoverageTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForcoveragetypes)

                    cboCoverageType.Populate(CoverageTypeList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
                    'Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.LoadCoverageTypes(State.MyBO.Id, New Guid(cboRiskType.SelectedValue), ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                    DisableControls()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub cboRiskType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRiskType.SelectedIndexChanged
            Try
                If Not cboRiskType.SelectedValue.Equals(Guid.Empty.ToString) Then
                    NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE) = cboRiskType.SelectedValue
                    Dim listcontextForcoveragetypes As ListContext = New ListContext()
                    listcontextForcoveragetypes.CertId = State.MyBO.Id
                    listcontextForcoveragetypes.CertItemId = New Guid(cboRiskType.SelectedValue)
                    listcontextForcoveragetypes.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    listcontextForcoveragetypes.DateOfLoss = State.DateOfLoss
                    Dim CoverageTypeList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCertificate", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForcoveragetypes)

                    cboCoverageType.Populate(CoverageTypeList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                  })
                    'Me.BindListControlToDataView(Me.cboCoverageType, LookupListNew.LoadCoverageTypes(State.MyBO.Id, New Guid(cboRiskType.SelectedValue), ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.DateOfLoss))
                    DisableControls()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub cboCoverageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCoverageType.SelectedIndexChanged
            Try
                NavController.FlowSession(FlowSessionKeys.SESSION_COVERAGE_TYPE) = cboCoverageType.SelectedValue
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Helper function"

        Private Sub PopulateProtectionAndEventDetail()
            Dim cssClassName As String
            Try
                moProtectionAndEventDetails.CustomerName = State.MyBO.CustomerName
                moProtectionAndEventDetails.EnrolledMake = NO_DATA
                moProtectionAndEventDetails.ClaimNumber = NO_DATA
                moProtectionAndEventDetails.DealerName = State.MyBO.getDealerDescription
                moProtectionAndEventDetails.EnrolledModel = NO_DATA
                moProtectionAndEventDetails.ClaimStatus = NO_DATA
                If NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME) Is Nothing Then
                    moProtectionAndEventDetails.CallerName = State.MyBO.CustomerName
                    TextCallerName.Text = State.MyBO.CustomerName
                ElseIf Not (NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME).ToString.Equals(String.Empty)) Then
                    moProtectionAndEventDetails.CallerName = NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME).ToString
                    TextCallerName.Text = NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME).ToString
                End If
                If State.DateOfLoss > Date.MinValue Then moProtectionAndEventDetails.DateOfLoss = GetDateFormattedStringNullable(State.DateOfLoss)
                moProtectionAndEventDetails.ProtectionStatus = LookupListNew.GetDescriptionFromId("SUBSTAT", State.MyBO.SubscriberStatus)
                If (LookupListNew.GetCodeFromId("SUBSTAT", State.MyBO.SubscriberStatus) = Codes.SUBSCRIBER_STATUS__ACTIVE) Then
                    cssClassName = "StatActive"
                Else
                    cssClassName = "StatClosed"
                End If
                moProtectionAndEventDetails.ProtectionStatusCss = cssClassName
                moProtectionAndEventDetails.ClaimedModel = NO_DATA
                moProtectionAndEventDetails.ClaimedMake = NO_DATA
                moProtectionAndEventDetails.TypeOfLoss = NO_DATA
                moProtectionAndEventDetails.DateOfLoss = NO_DATA
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function PopulateAndValidateInputs() As Boolean
            Dim dateOfLoss As Date, dateReported As Date
            Dim blnValid As Boolean = True
            Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)

            If (moDateOfLossText.Text.Equals(String.Empty)) Then
                errMsg.Add(lblDateOfLoss.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnValid = False
            Else
                Try
                    DateHelper.GetDateValue(moDateOfLossText.Text.ToString())
                Catch
                    errMsg.Add(lblDateOfLoss.Text & " : " & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                    blnValid = False
                End Try
            End If

            If (txtDateReported.Text.Equals(String.Empty)) Then
                errMsg.Add(lblDateReported.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnValid = False
            Else
                Try
                    DateHelper.GetDateValue(txtDateReported.Text.ToString())
                Catch
                    errMsg.Add(lblDateReported.Text & " : " & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                    blnValid = False
                End Try
            End If

            Dim dtDOI As DateTime
            Dim dtDOR As DateTime

            dtDOI = DateHelper.GetDateValue(moDateOfLossText.Text)
            dtDOR = DateHelper.GetDateValue(txtDateReported.Text)

            State.DateOfLoss = dtDOI
            State.DateReported = dtDOR

            If dtDOI > dtDOR Then
                errMsg.Add(lblDateOfLoss.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_DATE_OF_INCIDENT_ERR))
                blnValid = False
            End If

            If blnValid = False Then
                MasterPage.MessageController.AddError(errMsg.ToArray, False)
            End If

            Return blnValid
        End Function

        Private Function ValidateDropdowns() As Boolean
            Dim dateOfLoss As Date, dateReported As Date
            Dim blnValid As Boolean = True
            Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)

            If (moDateOfLossText.Text.Equals(String.Empty)) Then
                errMsg.Add(lblDateOfLoss.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnValid = False
            Else
                Try
                    DateHelper.GetDateValue(moDateOfLossText.Text.ToString())
                Catch
                    errMsg.Add(lblDateOfLoss.Text & " : " & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                    blnValid = False
                End Try
            End If

            If (txtDateReported.Text.Equals(String.Empty)) Then
                errMsg.Add(lblDateReported.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnValid = False
            Else
                Try
                    DateHelper.GetDateValue(txtDateReported.Text.ToString())
                Catch
                    errMsg.Add(lblDateReported.Text & " : " & TranslationBase.TranslateLabelOrMessage(Message.MSG_INVALID_DATE))
                    blnValid = False
                End Try
            End If

            Dim dtDOI As DateTime
            Dim dtDOR As DateTime
          
            dtDOI = DateHelper.GetDateValue(moDateOfLossText.Text)
            dtDOR = DateHelper.GetDateValue(txtDateReported.Text)

            If dtDOI > dtDOR Then
                errMsg.Add(lblDateOfLoss.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.INVALID_DATE_OF_INCIDENT_ERR))
                blnValid = False
            End If

            If RiskTypeTR.Visible Then
                If cboRiskType.Text.Equals(String.Empty) OrElse cboRiskType.Text.Equals(Guid.Empty.ToString) Then
                    errMsg.Add(RiskTypeLabel.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                    blnValid = False
                End If
            End If
            If cboCoverageType.Text.Equals(String.Empty) OrElse cboCoverageType.Text.Equals(Guid.Empty.ToString) Then
                errMsg.Add(CoverageTypeLabel.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnValid = False
            End If
            If TextCallerName.Text.Equals(String.Empty) Then
                errMsg.Add(CallerNameLabel.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnValid = False
            End If
            If TextProblemDescription.Text.Equals(String.Empty) Then
                errMsg.Add(ProblemDescriptionLabel.Text & " : " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                blnValid = False
            End If

            If blnValid = False Then
                MasterPage.MessageController.AddError(errMsg.ToArray, False)
            End If

            Return blnValid
        End Function

#End Region

#Region " Buttons Clicks "

        Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                If PopulateAndValidateInputs() Then
                    PopulateDropdown()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                NavController.Navigate(Me, "back")
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Try
                NavController.Navigate(Me, "back")
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        ''' <summary>s
        ''' Continue to wizard#2 screen
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
            State.CallerName = TextCallerName.Text
            State.ProblemDescription = TextProblemDescription.Text
            Dim guidCICID As Guid
            Try
                If ValidateDropdowns() Then
                    'If RiskTypeTR.Visible Then
                    '    If cboRiskType.Text.Equals(String.Empty) Or cboRiskType.Text.Equals(Guid.Empty.ToString) Then
                    '        Throw New GUIException(Message.MSG_INVALID_RISK_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RISK_TYPE_MUST_BE_SELECTED_ERR)
                    '    End If
                    'End If
                    'If Me.TextCallerName.Text.Equals(String.Empty) Then
                    '    Throw New GUIException(Message.MSG_CALLER_NAME_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CALLER_NAME_REQUIRED)
                    'End If
                    'If Me.TextProblemDescription.Text.Equals(String.Empty) Then
                    '    Throw New GUIException(Message.MSG_PROBLEM_DESCRIPTION_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_PROBLEM_DESCRIPTION_REQUIRED)
                    'End If
                    If Not cboCoverageType.SelectedValue.ToString.Equals(String.Empty) Then
                        guidCICID = New Guid(cboCoverageType.SelectedValue.ToString)
                    End If
                    If Not guidCICID.Equals(Guid.Empty) Then
                        NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE_ID) = guidCICID
                        NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS) = State.DateOfLoss
                        NavController.FlowSession(FlowSessionKeys.SESSION_DATE_CLAIM_REPORTED) = State.DateReported
                        NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME) = State.CallerName
                        NavController.FlowSession(FlowSessionKeys.SESSION_PROBLEM_DESCRIPTION) = State.ProblemDescription
                        NavController.FlowSession(FlowSessionKeys.SESSION_RISK_TYPE) = cboRiskType.SelectedValue
                        NavController.FlowSession(FlowSessionKeys.SESSION_COVERAGE_TYPE) = cboCoverageType.SelectedValue
                        Dim oCountry As Country

                        moProtectionAndEventDetails.CallerName = NavController.FlowSession(FlowSessionKeys.SESSION_CALLER_NAME).ToString

                        If State.MyBO.getMasterclaimProcFlag = Codes.MasterClmProc_BYDOL Then
                            Dim dv As Claim.MaterClaimDV = Claim.getRepairedMasterClaimsList(guidCICID, State.DateOfLoss)
                            If dv.Count > 0 Then
                                Throw New GUIException(Message.MSG_COVERAGE_TYPE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_REPAIRED_MASTERCLAIM_EXISTS)
                            End If
                        End If

                        If State.MyBO.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED AndAlso State.MyBO.Dealer.IsGracePeriodSpecified Then
                            NavController.State = Nothing
                            NavController.Navigate(Me, COVERAGE_SELECTED, New CertItemForm.Parameters("NEW_CLAIM"))
                        Else
                            If Claim.IsClaimReportedWithinPeriod(State.MyBO.Id, State.DateOfLoss, State.DateReported) Then
                                NavController.State = Nothing
                                NavController.Navigate(Me, COVERAGE_SELECTED, New CertItemForm.Parameters("NEW_CLAIM"))
                            Else
                                oCountry = New Country(State.MyBO.AddressChild.CountryId)
                                If Not (oCountry.DefaultScForDeniedClaims.Equals(Guid.Empty)) Then
                                    'Set the Service Center to the Default Service Center for Claims Not reported within Period.                            
                                    NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_CENTER) = New ServiceCenter(oCountry.DefaultScForDeniedClaims)
                                    NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_COVERAGE) = New CertItemCoverage(guidCICID)
                                    NavController.State = Nothing
                                    NavController.Navigate(Me, COVERAGE_SELECTED_LATE_REPORTED, guidCICID)
                                Else
                                    Dim errMsg As String = TranslationBase.TranslateLabelOrMessage(Message.MSG_DEFAULT_SVC_CENTER_NOT_SETUP)
                                    MasterPage.MessageController.AddError(errMsg, False)
                                End If
                            End If
                        End If
                    Else
                        Throw New GUIException(Message.MSG_COVERAGE_TYPE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_COVERAGE_TYPE_REQUIRED)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region


    End Class

End Namespace
Imports System.Diagnostics
Imports Assurant.ElitaPlus.DALObjects

Namespace Claims
    Public Class ClaimReplacementQuoteForm
        Inherits ElitaPlusSearchPage

        Public Const PAGETAB As String = "CLAIM"
        Public Const PAGETITLE As String = "REPLACEMENT_QUOTE"

#Region "Page State"


#Region "Parameters"

        Public Class Parameters
            Public moClaimId As Guid

            Public Sub New(oClaimId As Guid)
                moClaimId = oClaimId
            End Sub

        End Class

#End Region

#Region "MyState"

        Class MyState
            Public Claim As ClaimBase
            Public ReadOnly Property ClaimBO As ClaimBase
                Get
                    If (Claim Is Nothing) Then
                        If (moParams.moClaimId <> Guid.Empty) Then
                            Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(moParams.moClaimId)
                        End If
                    End If
                    Return Claim
                End Get
            End Property
            Public moParams As Parameters
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
            Try
                State.moParams = CType(CallingParameters, Parameters)
                If (State.moParams Is Nothing) OrElse (State.moParams.moClaimId.Equals(Guid.Empty)) Then
                    Throw New DataNotFoundException
                End If
                PopulateFormFromBo()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Constants"

        Public Const URL As String = "~/Claims/ClaimReplacementQuoteForm.aspx"
        Private Const NoData As String = " - "
        Private Const MethodOfRepairXcd As String = "METHR-R"
        Private Const ClaimExtendedStatusCode = "REPMTQT"

#End Region


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As Object

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
#Region "Handlers-Init"

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear()
            Try
                ClearAll()
                If Not Page.IsPostBack Then
                    SetStateProperties()
                    UpdateBreadCrum()
                    Dim claimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(ClaimExtendedStatusCode)
                    If claimStatusByGroupId = Guid.Empty Then
                        Dim strErrMsg = "CLM_REP_QT_EXT_STATUS_NOT_CONFIGURED"
                        DisplayErrorMessage(strErrMsg)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub ClearAll()
            Try
                ClearLabelErrSign(lblNewSCError)
                lblNewSCError.Visible = False
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub


#End Region

#Region "Handlers-Buttons"
        Private Sub GoBack()
            ' Claim Detail
            Dim retType As New ClaimForm.ReturnType(DetailPageCommand.Back)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                GoBack()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub btnSendQuote_Click(sender As Object, e As EventArgs) Handles btnSend.Click
            Try
                SendReplacementQuote()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateFormFromBo()

            Dim langId As Guid = Authentication.CurrentUser.LanguageId
            Dim claimInfo As ClaimBase = State.ClaimBO

            Try
                moProtectionEvtDtl.ClaimNumber = claimInfo.ClaimNumber
                moProtectionEvtDtl.ClaimStatus = LookupListNew.GetClaimStatusFromCode(langId, claimInfo.StatusCode)
                moProtectionEvtDtl.ClaimStatusCss = If(claimInfo.Status = BasicClaimStatus.Active, "StatActive", "StatClosed")
                moProtectionEvtDtl.DateOfLoss = GetDateFormattedStringNullable(claimInfo.LossDate.Value)
                moProtectionEvtDtl.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, claimInfo.CertificateItem.RiskTypeId)
                moProtectionEvtDtl.DealerName = claimInfo.Certificate.Dealer.Dealer
                moProtectionEvtDtl.ClaimedModel = claimInfo.ClaimedEquipment.Model
                moProtectionEvtDtl.ClaimedMake = claimInfo.ClaimedEquipment.Manufacturer
                moProtectionEvtDtl.CustomerName = claimInfo.Certificate.CustomerName
                moProtectionEvtDtl.EnrolledMake = NoData
                moProtectionEvtDtl.EnrolledModel = NoData
                moProtectionEvtDtl.CallerName = NoData
                moProtectionEvtDtl.ProtectionStatus = LookupListNew.GetClaimStatusFromCode(langId, claimInfo.Certificate.StatusCode)
                moProtectionEvtDtl.ProtectionStatusCss = If(claimInfo.Status = BasicClaimStatus.Active, "StatActive", "StatClosed")
                InitNewServiceCenterUserControl()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Business Part"

        Private Sub InitNewServiceCenterUserControl()
            'return if the control already initialized
            If String.IsNullOrEmpty(ucSelectServiceCenter.MethodOfRepairXcd) = False Then Exit Sub

            'Set up the service center start
            ucSelectServiceCenter.HostMessageController = MasterPage.MessageController

            ucSelectServiceCenter.TranslationFunc = Function(value As String)
                                                        Return TranslationBase.TranslateLabelOrMessage(value)
                                                    End Function

            ucSelectServiceCenter.TranslateGridHeaderFunc = Sub(grid As GridView)
                                                                TranslateGridHeader(grid)
                                                            End Sub

            ucSelectServiceCenter.HighLightSortColumnFunc = Sub(grid As GridView, sortExp As String)
                                                                HighLightSortColumn(grid, sortExp, False)
                                                            End Sub

            ucSelectServiceCenter.NewCurrentPageIndexFunc = Function(grid As GridView, intRecordCount As Integer, intNewPageSize As Integer)
                                                                Return NewCurrentPageIndex(grid, intRecordCount, intNewPageSize)
                                                            End Function
            'Set up the service center end

            Dim oCountry As Country = New Country(State.ClaimBO.Company.CountryId)

            ucSelectServiceCenter.PageSize = 30
            ucSelectServiceCenter.CountryId = oCountry.Id
            ucSelectServiceCenter.CountryCode = oCountry.Code
            ucSelectServiceCenter.CompanyCode = State.ClaimBO.Company.Code
            ucSelectServiceCenter.Dealer = State.ClaimBO.Certificate.Dealer.Dealer
            ucSelectServiceCenter.Make = State.ClaimBO.ClaimedEquipment.Manufacturer
            ucSelectServiceCenter.RiskTypeEnglish = State.ClaimBO.RiskType
            ucSelectServiceCenter.MethodOfRepairXcd = MethodOfRepairXcd
            ucSelectServiceCenter.ShowControl = True
        End Sub

        Private Sub AddClaimExtendedStatus(oClaim As ClaimBase)
            Dim oClaimStatus As ClaimStatus, claimStatusByGroupId As Guid
            claimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(ClaimExtendedStatusCode)
            If Not claimStatusByGroupId = Guid.Empty Then
                oClaimStatus = New ClaimStatus With {
                    .ClaimId = oClaim.Id,
                    .ClaimStatusByGroupId = claimStatusByGroupId,
                    .StatusDate = DateTime.UtcNow,
                    .Comments = ucSelectServiceCenter.SelectedServiceCenter.ServiceCenterCode + " : " + New ServiceCenter(ucSelectServiceCenter.SelectedServiceCenter.ServiceCenterId).Description
                }
                oClaimStatus.Save()
            End If
        End Sub

        Private Sub DisplayErrorMessage(strErrMsg As String)
            lblNewSCError.Visible = True
            lblNewSCError.Text = String.Empty
            lblNewSCError.ForeColor = Color.Red
            lblNewSCError.Text = TranslationBase.TranslateLabelOrMessage(strErrMsg) + ". "
        End Sub


        Private Sub SendReplacementQuote()
            Dim blnValid As Boolean = True, strErrMsg As String = String.Empty
            Dim claimDetails As ClaimBase = State.ClaimBO
            Try
                If ucSelectServiceCenter.SelectedServiceCenter Is Nothing Then
                    blnValid = False
                    strErrMsg = "SERVICE_CENTER_MUST_BE_SELECTED_ERR"
                Else
                    If ucSelectServiceCenter.SelectedServiceCenter.ServiceCenterId = claimDetails.ServiceCenterId Then
                        blnValid = False
                        strErrMsg = "EXISTING_SERVICE_CENTER_SELECTED"
                    End If
                End If
                Dim argumentsToAddEvent As String
                If blnValid Then
                    With claimDetails
                        argumentsToAddEvent = "ClaimId:" & DALBase.GuidToSQLString(.Id) & ";ServiceCenterId:" & DALBase.GuidToSQLString(ucSelectServiceCenter.SelectedServiceCenter.ServiceCenterId) & ""
                        PublishedTask.AddEvent(companyGroupId:=Guid.Empty,
                                               companyId:=Guid.Empty,
                                               countryId:=Guid.Empty,
                                               dealerId:= .Dealer.Id,
                                               productCode:=String.Empty,
                                               coverageTypeId:=Guid.Empty,
                                               sender:="Claim Replacement Quote",
                                               arguments:=argumentsToAddEvent,
                                               eventDate:=DateTime.UtcNow,
                                               eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP_CLM_REPLACEMNT_QUOTE),
                                               eventArgumentId:=Nothing)
                    End With
                    AddClaimExtendedStatus(claimDetails)
                    GoBack()
                Else
                    DisplayErrorMessage(strErrMsg)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class
End Namespace
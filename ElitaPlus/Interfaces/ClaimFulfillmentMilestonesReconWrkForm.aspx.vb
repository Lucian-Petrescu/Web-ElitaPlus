Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports System.ServiceModel
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.FileAdminService

Namespace Interfaces

    Partial Class ClaimFulfillmentMileStonesReconWrkForm
        Inherits ElitaPlusSearchPage

        Public Class PageParameters
            Public Property CountryCode As String
            Public Property ServiceCenterCode As String
            Public Property Filename As String

        End Class

#Region "Constants"
        Public Const URL As String = "ClaimFulfillmentMileStonesReconWrkForm.aspx"
        Public PageTile As String = "CLAIM_FULFILLMENT_MILESTONES_RECON_WORK"
        Private Const EndPointName = "CustomBinding_FileAdmin"
        Public Enum GridDefenitionEnum
            RecordType = 0
            ExternalAuthorizationNumber
            EventType
            Comment
            SkuOfDamagedDevice
            ManufacturerOfDamagedDevice
            ModelOfDamagedDevice
            ColorOfDamagedDevice
            CapacityOfDamagedDevice
            CarrierCodeOfTheDamagedDevice
            SerialNumberOfDamagedDevice
            ImeiofDamagedDevice
            ServiceCenterId
            InvoiceNumber
            ManufacturerOfReplacementDevice
            ModelOfReplacementDevice
            SerialNumberOfReplacementDevice
            ImeiOfReplacementDevice
            SkuOfReplacementDevice
        End Enum

        Public Const STATUS_FAILED As String = "F"
        Public Const STATUS_REPROCESS As String = "R"


#End Region

#Region "MyState"
        Class MyState
            Public CountryCode As String
            Public Filename As String
            Public ServiceCenterCode As String
            REM ---------
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public PageIndex As Integer
            Public PageSize As Integer = 30
            Public searchDV As DataView = Nothing
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public IsReturningFromChild As Boolean
            REM ---------
            Public StartDate As Date
            Public EndDate As Date
            Public IsFileDataEdited As Boolean = False
        End Class
#End Region


#Region "Page State"
        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()

            ' Return from the Back Button
            GoBack()

        End Sub

        Private Sub SetSession()
            With Me.State
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .searchDV = Me.State.searchDV
            End With
        End Sub
#End Region

#Region "Handlers-Buttons-Methods"
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                GoBack()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GoBack()

            Dim retType As New ClaimFulfillmentMileStonesFileForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.CountryCode, Me.State.ServiceCenterCode)
            Me.ReturnToCallingPage(retType)

        End Sub
#End Region


#Region "Page Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Interfaces")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTile)
                Me.UpdateBreadCrum()

                If Not Me.IsPostBack Then
                    TranslateGridHeader(Grid)

                    If Not Me.State.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, trPageSize, True)
                    End If

                    If Me.State.IsGridVisible Then
                        If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                            Grid.PageSize = Me.State.SelectedPageSize
                        End If
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                    Me.PopulateGrid(True)
                    SetSummary()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)

            'Me.State.BenefitProductCodeId = retObj.EditingId
            'Me.SetStateProperties()
            Me.State.LastOperation = DetailPageCommand.Redirect_

        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Dim pageParameters As PageParameters = CType(Me.CallingParameters, PageParameters)
                    If Not pageParameters Is Nothing Then
                        Me.State.CountryCode = pageParameters.CountryCode
                        Me.State.ServiceCenterCode = pageParameters.ServiceCenterCode
                        Me.State.Filename = pageParameters.Filename
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Methods"

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_MILESTONES_RECON_WORK")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_MILESTONES_RECON_WORK")
                End If
            End If
        End Sub

        Public Sub SetSummary()
            Try
                Me.moFileNameText.Text = State.Filename
                Me.moDealerNameText.Text = State.ServiceCenterCode

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Grid Handles"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection")?.ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageIndex = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub Grid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Populate"
        Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)
            Try

                If Me.State.searchDV Is Nothing OrElse refreshData Then GetFileRecords(refreshData)


                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GetFileRecords(Optional ByVal refreshData As Boolean = False)
            Dim strTemp As String = String.Empty

            Dim request As New GetFileRecordsRequest()
            request.CountryCode = State.CountryCode
            request.ServiceCenterCode = State.ServiceCenterCode
            request.FileName = State.Filename

            Try
                Dim wsResponse = WcfClientHelper.Execute(Of FileAdminClient, FileAdmin, FileAdminService.GetFileRecordsResponse)(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As FileAdminClient)
                        Return c.GetFileRecords(request)
                    End Function)
                If wsResponse IsNot Nothing Then
                    BindNotesGrid(wsResponse)
                End If
            Catch ex As FaultException
                ThrowWsFaultExceptions(ex)
            End Try
        End Sub

        Private Sub BindNotesGrid(ByVal response As GetFileRecordsResponse)

            If (response IsNot Nothing AndAlso
                response.FileRecordsDetails IsNot Nothing) Then

                Grid.DataSource = response.FileRecordsDetails
                Grid.DataBind()
            End If

        End Sub

        ''' <returns>Instance of <see cref="FileAdminClient"/></returns>
        Private Shared Function GetClient() As FileAdminClient
            Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__FILE_ADMIN_SERVICE), False)
            Dim client = New FileAdminClient(EndPointName, oWebPasswd.Url)
            client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            client.ClientCredentials.UserName.Password = oWebPasswd.Password
            Return client
        End Function

        Private Sub ThrowWsFaultExceptions(fex As FaultException)
            Me.Log(fex)
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR) & " - " & fex.Message, False)

        End Sub

#End Region

    End Class

End Namespace

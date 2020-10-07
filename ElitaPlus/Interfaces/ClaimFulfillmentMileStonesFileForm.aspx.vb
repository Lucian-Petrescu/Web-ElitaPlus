Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.FileAdminService

Namespace Interfaces

    Public Class ClaimFulfillmentMileStonesFileForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Public PageTile As String = "CLAIM_FULFILLMENT_MILESTONES_FILE"
        Private Shared FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters
        Private Const EndPointName = "CustomBinding_FileAdmin"

        Public Enum GridDefenitionEnum
            Filename = 0
            Filedate
            Received
            Counted
            Validated
            Processed
            FileProcessedId
        End Enum

        Public Const COL_FILE_PROCESSED_ID As String = "FILE_PROCESSED_ID"
        Public Const COL_FILE_NAME As String = "FILE_NAME"
        Public Const COL_CREATED_DATE As String = "CREATED_DATE"
        Public Const COL_RECEIVED As String = "RECEIVED"
        Public Const COL_COUNTED As String = "COUNTED"
        Public Const COL_VALIDATED As String = "VALIDATED"
        Public Const COL_PROCESSED As String = "PROCESSED"

        REM -------------------
        Public Const GRID_COMMAND_SHOW_PROCESSED As String = "ShowProcessed"
        Public Const GRID_COMMAND_SHOW_FILES As String = "ShowFiles"
        REM -------------------
        Public Const STATUS_PROCESSED = "P"
        Public Const STATUS_REPROCESS = "R"
#End Region

#Region "Page Navigation"
        Private IsReturningFromChild As Boolean = False

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public CountryCode As String
            Public ServiceCenterCode As String

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, countryCode As String, serviceCenterCode As String)
                LastOperation = LastOp
                Me.CountryCode = countryCode
                Me.ServiceCenterCode = serviceCenterCode
            End Sub

        End Class

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back

                            State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)

                        Case Else

                    End Select
                    Grid.PageIndex = State.PageIndex
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Company Control"
        Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl
        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moCompanyMultipleDrop Is Nothing Then
                    moCompanyMultipleDrop = CType(FindControl("drpCompany"), MultipleColumnDDLabelControl)
                End If
                Return moCompanyMultipleDrop
            End Get
        End Property
#End Region
#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 30
            Public PageSort As String
            REM ---------------
            Public SelectedDealerFileLayout As String = ""
            Public FileProcessedId As Guid
            Public CountryCode As String
            Public ServiceCenterCode As String
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public IsReturningFromChild As Boolean
            Public searchDV As GetFilesResponse = Nothing
            Public SortExpression As String = $"{AppleGBIFileReconWrk.COL_NAME_FILE_NAME} desc "
            REM ---------------
            Public bnoRow As Boolean = False

            Public Sub New()
                Console.WriteLine("Building the state")
            End Sub

        End Class
#End Region


#Region "Page event handlers"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Interfaces")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTile)
                UpdateBreadCrum()

                If Not IsPostBack Then
                    PopulateDropdowns()
                    TranslateGridHeader(Grid)

                    If Not State.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, trPageSize, True)
                    End If

                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                            Grid.PageSize = State.SelectedPageSize
                        End If
                    End If
                    SetGridItemStyleColor(Grid)

                    If IsReturningFromChild Then
                        PopulateDropdowns()
                        PopulateGrid(True)

                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()

            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(PageTile)
            End If

        End Sub

#End Region

#Region "Constructor"


        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get

                Return CType(MyBase.State, MyState)

            End Get
        End Property

#End Region

#Region "Grid Handles"
        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Public Sub Grid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As FileInfoDto = CType(e.Row.DataItem, FileInfoDto)
                Dim btnEditItem As LinkButton
                If dvRow IsNot Nothing And Not State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                        If Not String.IsNullOrEmpty(dvRow.LogicalFileName) Then
                            btnEditItem = CType(e.Row.Cells(GridDefenitionEnum.Filename).FindControl("btnGridShow"), LinkButton)
                            btnEditItem.Text = dvRow.LogicalFileName
                            btnEditItem.CommandName = "ShowFiles"
                        End If

                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "Controlling Logic"


        Public Sub PopulateSearchFieldsFromState()
            Try
                ddlCountry.SelectedValue = State.CountryCode
                ddlServiceCenter.SelectedValue = State.ServiceCenterCode

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateDropdowns()
            Try
                Dim CountryList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)
                Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                                Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                                Select Country).ToArray()
                ddlCountry.Populate(UserCountries.ToArray(),
                                    New PopulateOptions() With
                                    {
                                          .AddBlankItem = True,
                                          .TextFunc = AddressOf .GetDescription,
                                          .BlankItemValue = String.Empty,
                                          .ValueFunc = AddressOf .GetCode
                                    })

                Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)

                For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                    Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                    If ServiceCenters.Count > 0 Then
                        If ServiceCenterList IsNot Nothing Then
                            ServiceCenterList.AddRange(ServiceCenters)
                        Else
                            ServiceCenterList = ServiceCenters.Clone()
                        End If
                    End If
                Next

                ddlServiceCenter.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = AddressOf .GetDescription,
                    .BlankItemValue = String.Empty,
                    .ValueFunc = AddressOf .GetCode
                })

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ClearSearch()
            Try
                ddlCountry.SelectedValue = String.Empty
                ddlServiceCenter.SelectedValue = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Shared Function GetUniqueDirectory(path As String, username As String) As String
            Dim uniqueIdDirectory As String = path & username & "_" & RemoveInvalidChar(Date.Now.ToString)
            Return uniqueIdDirectory
        End Function

        Public Shared Function RemoveInvalidChar(filename As String) As String

            Dim index As Integer
            For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
                'replace the invalid character with blank
                filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
            Next
            Return filename
        End Function

        Public Shared Sub CreateFolder(folderName As String)
            Dim objDir As New IO.DirectoryInfo(folderName)
            If Not objDir.Exists() Then
                objDir.Create()
            End If
        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)
            Try

                If State.searchDV Is Nothing OrElse refreshData Then SearchGBIFiles(refreshData)


                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SearchGBIFiles(Optional ByVal refreshData As Boolean = False)

            Dim request As New GetFilesRequest With {
                .CountryCode = ddlCountry.SelectedValue,
                .ServiceCenterCode = ddlServiceCenter.SelectedValue
            }

            Try
                Dim wsResponse = WcfClientHelper.Execute(Of FileAdminClient, FileAdmin, FileAdminService.GetFilesResponse)(
                                                            GetClient(),
                                                            New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                            Function(c As FileAdminClient)
                                                                Return c.GetFiles(request)
                                                            End Function)
                If wsResponse IsNot Nothing Then
                    BindNotesGrid(wsResponse)
                End If
            Catch ex As FaultException
                ThrowWsFaultExceptions(ex)
            End Try

        End Sub

        Private Sub BindNotesGrid(response As GetFilesResponse)

            If (response IsNot Nothing AndAlso
                response.FileDetails IsNot Nothing) Then
                State.searchDV = response
                Grid.DataSource = response.FileDetails
                Grid.DataBind()
            End If

        End Sub

        ''' <summary>
        ''' Gets New Instance of File Admin Service Client with Credentials Configured
        ''' </summary>
        ''' <returns>Instance of <see cref="FileAdminClient"/></returns>
        Private Shared Function GetClient() As FileAdminClient
            Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__FILE_ADMIN_SERVICE), False)
            Dim client = New FileAdminClient(EndPointName, oWebPasswd.Url)
            client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
            client.ClientCredentials.UserName.Password = oWebPasswd.Password
            Return client
        End Function


        Private Sub ThrowWsFaultExceptions(fex As FaultException)
            Log(fex)
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR) & " - " & fex.Message, False)

        End Sub
#End Region
#Region "Button Event Handlers"
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                ClearSearch()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try

                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    State.IsGridVisible = True
                End If
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing

                If Not (ddlServiceCenter.SelectedValue = String.Empty) AndAlso Not (ddlCountry.SelectedValue = String.Empty) Then
                    PopulateGrid()
                Else
                    Throw New GUIException(Message.MSG_INVALID_SEARCH, Assurant.ElitaPlus.Common.ErrorCodes.WS_MISSING_SEARCH_CRITERIA)
                End If



            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
                .searchDV = State.searchDV
                .CountryCode = ddlCountry.SelectedValue
                .ServiceCenterCode = ddlServiceCenter.SelectedValue
            End With
        End Sub


#End Region

        Protected Sub Grid_OnRowCommand(sender As Object, e As GridViewCommandEventArgs)
            Try

                If e.CommandName = "ShowFiles" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Dim btnEditItem = CType(Grid.Rows(index).Cells(GridDefenitionEnum.Filename).FindControl("btnGridShow"), LinkButton)
                    Dim filename As String = btnEditItem.Text

                    Dim parameters As ClaimFulfillmentMileStonesReconWrkForm.PageParameters = New ClaimFulfillmentMileStonesReconWrkForm.PageParameters With {
                        .CountryCode = ddlCountry.SelectedValue,
                        .ServiceCenterCode = ddlServiceCenter.SelectedValue,
                        .Filename = filename
                    }


                    callPage(ClaimFulfillmentMileStonesReconWrkForm.URL, parameters)
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
    End Class

End Namespace
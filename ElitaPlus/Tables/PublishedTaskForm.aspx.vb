Option Strict Off


Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.BusinessObjectsNew.PublishedTask
Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Public Class PublishedTaskForm
    Inherits ElitaPlusSearchPage

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
    Public Const URL As String = "Tables/PublisTask.aspx"
    Public Const PAGETITLE As String = "PUBLISHED_TASKS"
    Public Const PAGETAB As String = "TABLES"
    Public Const SUMMARYTITLE As String = "PUBLISHED_TASKS"

    Private Const GRID_COMMAND_DELETE As String = "DELETE_TASK"
    Private Const GRID_COMMAND_RESET As String = "RESET_TASK"
#End Region

#Region "Page State"
    Private IsLoadGrid As Boolean = False

    Class MyState
        Public SelectedPageSize As Integer
        Public PageIndex As Integer
        Public SearchDV As PublishedTask.PublishedTaskSearchDV
        Public searchClick As Boolean = False
        Public dealerName As String = String.Empty

        Public companyId As Guid

        Public dealerId As Guid
        Public countryId As Guid

        Public product As String

        Public coverageTypeId As Guid
        'Public eventType As String
        Public eventTypeId As Guid

        Public task As String
        'Public status As String
        Public statusId As Guid

        Public sender As String
        Public arguments As String
        Public machineName As String

        Public startDate As String
        Public endDate As String

        Public LabelEventTaskInfoStr As String
        Public LabelExecutionInfoA(4) As String
        Public LabelOriginInfoA(5) As String

        Public Sub New()

        End Sub
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

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear()

            If Not Me.IsPostBack Then
                If Not (Me.State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                    Me.State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE
                    cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                    grdPublishTask.PageSize = Me.State.SelectedPageSize
                End If

                TranslateGridHeader(grdPublishTask)

                Me.UpdateBreadCrum()

                PopulateSearchControls()

                Me.AddCalendar(Me.BtnStartDate, Me.txtStartDate)
                Me.AddCalendar(Me.BtnEndDate, Me.txtEndDate)


                ' If Me.IsLoadGrid Then
                'Me.PopulateGrid()
                'End If
            End If

            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()

        With Me.MasterPage
            .PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            .PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            .UsePageTabTitleInBreadCrum = False
            .BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End With

    End Sub
#End Region

#Region "Button event handlers"
    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try

            ' Clear all search options typed or selected by the user
            Me.ClearAllSearchOptions()

            ' Update the Bo state properties with the new value
            '           Me.ClearStateValues()
            '
            '          Me.SetStateProperties()
            ' Me.State.IsGridVisible = 

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If txtStartDate.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblDateRange1, txtStartDate.Text)
            End If

            If txtEndDate.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblDateRange1, txtEndDate.Text)

                If txtStartDate.Text.Trim() <> String.Empty AndAlso DateHelper.GetDateValue(txtStartDate.Text) > DateHelper.GetDateValue(txtEndDate.Text) Then
                    ElitaPlusPage.SetLabelError(lblDateRange1)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                End If
            End If

            Me.lblDateRange1.ForeColor = Me.lblCompany.ForeColor
            Me.lblDateRange2.ForeColor = Me.lblCompany.ForeColor

            Me.SetStateProperties()
            Me.State.PageIndex = 0
            ' Me.State.IsGridVisible = True
            Me.State.searchClick = True
            Me.State.SearchDV = Nothing

            Me.PopulateGrid()

        Catch ex As Exception
            Me.lblDateRange2.ForeColor = Me.lblDateRange1.ForeColor
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        Try
            Me.State.SearchDV = Nothing

            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Helper functions"
    Private Sub SetStateProperties()
        '  Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

        With Me
            .State.companyId = .GetSelectedItem(.lstCompany)
            .State.dealerId = .GetSelectedItem(.lstDealer)

            .State.eventTypeId = .GetSelectedItem(.lstEventType)
            .State.statusId = .GetSelectedItem(Me.lstStatus)

            .State.product = .txtProduct.Text
            .State.startDate = .txtStartDate.Text
            .State.endDate = .txtEndDate.Text
        End With
    End Sub

    Private Sub ClearAllSearchOptions()
        With Me
            .lstCompany.SelectedIndex = 0
            .lstDealer.SelectedIndex = 0

            .lstEventType.SelectedIndex = 0
            .lstStatus.SelectedIndex = 0

            .txtProduct.Text = String.Empty
            .txtStartDate.Text = String.Empty
            .txtEndDate.Text = String.Empty
        End With

        SetStateProperties()
    End Sub

    Private Sub PopulateSearchControls()
        Try
            'populate dealer list
            'Me.PopulateDealerDropdown(Me.lstDealer)

            ' Load Company List
            ' Me.BindListControlToDataView(lstCompany, LookupListNew.GetUserCompaniesLookupList()) 'Company

            Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
            Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim filteredList As ListItem() = (From x In compLkl
                                              Where list.Contains(x.ListItemId)
                                              Select x).ToArray()

            lstCompany.Populate(filteredList, New PopulateOptions() With
             {
            .AddBlankItem = True
                  })
            ' Load Dealer List
            'Me.BindListControlToDataView(Me.lstDealer,
            'LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
            Dim oDealerList = GetDealerListByCompanyForUser()
            Me.lstDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

            ' Load Even Type List
            'Me.BindListControlToDataView(lstEventType, LookupListNew.DropdownLookupList("EVNT_TYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            lstEventType.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_TYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

            ' Load Task Status List
            'Me.BindListControlToDataView(lstStatus, LookupListNew.DropdownLookupList("TASK_STATUS", ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            lstStatus.Populate(CommonConfigManager.Current.ListManager.GetList("TASK_STATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

            ' Me.BindListControlToDataView(lslCountry, LookupListNew.GetUserCountriesLookupList(), )
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If Not oDealerList Is Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function

#End Region

#Region "Controlling Logic"

    Private Sub PopulateGrid()

        With Me.State
            If (.SearchDV Is Nothing) Then
                .SearchDV = PublishedTask.GetList(.companyId _
                                                , .dealerId _
                                                , .countryId _
                                                , .product _
                                                , .coverageTypeId _
                                                , .eventTypeId _
                                                , .task _
                                                , .statusId _
                                                , .sender _
                                                , .arguments _
                                                , .machineName _
                                                , .startDate _
                                                , .endDate)

                Me.ValidSearchResultCountNew(.SearchDV.Count, True)
            End If

            SetPageAndSelectedIndexFromGuid(.SearchDV, Guid.Empty, Me.grdPublishTask, .PageIndex)
        End With

        Me.SortAndBindGrid()
    End Sub



    Private Sub SortAndBindGrid()

        Me.State.PageIndex = Me.grdPublishTask.PageIndex
        Me.grdPublishTask.DataSource = Me.State.SearchDV
        Me.grdPublishTask.DataBind()

        Session("recCount") = Me.State.SearchDV.Count

        If Me.State.SearchDV.Count > 0 Then
            If Me.grdPublishTask.Visible Then
                Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.grdPublishTask.Visible Then
                Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub
#End Region

#Region "Static Methods"
    'Private Shared Sub AppendDisplayLabelValue(ByVal sb As StringBuilder, ByVal label As String, ByVal value As String)
    '    sb.AppendFormat("<u>{0}</u> : {1}", label, value)
    'End Sub

    Public Function GetOriginInfo(ByVal row As DataRow) As String
        Dim strTaskStatus As String
        Dim strB As New StringBuilder

        With Me.State
            If .LabelOriginInfoA(0) = String.Empty Then
                .LabelOriginInfoA(0) = "<U>" & TranslationBase.TranslateLabelOrMessage("COMPANY_GROUP") & "</u> : {0}<br/>"
                .LabelOriginInfoA(1) = "<U>" & TranslationBase.TranslateLabelOrMessage("COMPANY") & "</u> : {0}<br/>"
                .LabelOriginInfoA(2) = "<U>" & TranslationBase.TranslateLabelOrMessage("COUNTRY") & "</u> : {0}<br/>"
                .LabelOriginInfoA(3) = "<U>" & TranslationBase.TranslateLabelOrMessage("DEALER") & "</u> : {0}<br/>"
                .LabelOriginInfoA(4) = "<U>" & TranslationBase.TranslateLabelOrMessage("PRODUCT CODE") & "</u> : {0}<br/>"
                .LabelOriginInfoA(5) = "<U>" & TranslationBase.TranslateLabelOrMessage("COVERAGE TYPE") & "</u> : {0}<br/>"
            End If

            Dim sb As New StringBuilder

            If (PublishedTaskSearchDV.CompanyGroup(row).Trim() & "") <> String.Empty Then
                strB.AppendFormat(.LabelOriginInfoA(0), PublishedTaskSearchDV.CompanyGroup(row).Trim() & "")
            End If

            If (PublishedTaskSearchDV.Company(row).Trim() & "") <> String.Empty Then
                strB.AppendFormat(.LabelOriginInfoA(1), PublishedTaskSearchDV.Company(row).Trim() & "")
            End If

            If (PublishedTaskSearchDV.Country(row).Trim() & "") <> String.Empty Then
                strB.AppendFormat(.LabelOriginInfoA(2), PublishedTaskSearchDV.Country(row).Trim() & "")
            End If

            If (PublishedTaskSearchDV.Dealer(row).Trim() & "") <> String.Empty Then
                strB.AppendFormat(.LabelOriginInfoA(3), PublishedTaskSearchDV.Dealer(row).Trim() & "")
            End If

            If (PublishedTaskSearchDV.ProductCode(row).Trim() & "") <> String.Empty Then
                strB.AppendFormat(.LabelOriginInfoA(4), PublishedTaskSearchDV.ProductCode(row).Trim() & "")
            End If

            If (PublishedTaskSearchDV.CoverageType(row).Trim() & "") <> String.Empty Then
                strB.AppendFormat(.LabelOriginInfoA(5), PublishedTaskSearchDV.CoverageType(row).Trim() & "")
            End If
        End With

        Return strB.ToString()

    End Function
    'Public Shared ReadOnly Property OriginInfo(ByVal row As DataRow) As String
    '    Get
    '        Dim sb As New StringBuilder
    '        If (Not PublishedTaskSearchDV.CompanyGroup(row) Is Nothing AndAlso PublishedTaskSearchDV.CompanyGroup(row).Trim().Length > 0) Then
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("COMPANY_GROUP"), PublishedTaskSearchDV.CompanyGroup(row))
    '        End If
    '        If (Not PublishedTaskSearchDV.Company(row) Is Nothing AndAlso PublishedTaskSearchDV.Company(row).Trim().Length > 0) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("COMPANY"), PublishedTaskSearchDV.Company(row))
    '        End If
    '        If (Not PublishedTaskSearchDV.Country(row) Is Nothing AndAlso PublishedTaskSearchDV.Country(row).Trim().Length > 0) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("COUNTRY"), PublishedTaskSearchDV.Country(row))
    '        End If
    '        If (Not PublishedTaskSearchDV.Dealer(row) Is Nothing AndAlso PublishedTaskSearchDV.Dealer(row).Trim().Length > 0) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("DEALER"), PublishedTaskSearchDV.Dealer(row))
    '        End If
    '        If (Not PublishedTaskSearchDV.ProductCode(row) Is Nothing AndAlso PublishedTaskSearchDV.ProductCode(row).Trim().Length > 0) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("PRODUCT CODE"), PublishedTaskSearchDV.Dealer(row))
    '        End If
    '        Return sb.ToString()
    '    End Get
    'End Property
    Public Function GetExecutionInfo(ByVal row As DataRow) As String

        Dim strTaskStatus As String
        Dim strB As New StringBuilder

        With Me.State
            If .LabelExecutionInfoA(0) = String.Empty Then
                .LabelExecutionInfoA(0) = "<U>" & TranslationBase.TranslateLabelOrMessage("MACHINE_NAME") & "</u> : {0}<br/>"
                .LabelExecutionInfoA(1) = "<U>" & TranslationBase.TranslateLabelOrMessage("LOCK_DATE") & "</u> : {0}<br/>"
                .LabelExecutionInfoA(2) = "<U>" & TranslationBase.TranslateLabelOrMessage("LAST_ATTEMPT_DATE") & "</u> : {0}<br/>"
                .LabelExecutionInfoA(3) = "<U>" & TranslationBase.TranslateLabelOrMessage("RETRY_COUNT") & "</u> : {0}<br/>"
                .LabelExecutionInfoA(4) = "<U>" & TranslationBase.TranslateLabelOrMessage("FAIL_REASON") & "</u> : {0}<br/>"
            End If

            If (PublishedTaskSearchDV.MachineName(row).Trim() & "") <> String.Empty Then
                strB.AppendFormat(.LabelExecutionInfoA(0), PublishedTaskSearchDV.MachineName(row).Trim() & "")
            End If

            If Not (PublishedTaskSearchDV.LockDate(row) Is Nothing) Then
                'Sridhar strB.AppendFormat(.LabelExecutionInfoA(1), PublishedTaskSearchDV.LockDate(row).Value.ToString(DATE_TIME_FORMAT_12, System.Threading.Thread.CurrentThread.CurrentCulture))
                strB.AppendFormat(.LabelExecutionInfoA(1), GetLongDate12FormattedString(PublishedTaskSearchDV.LockDate(row).Value))
            End If

            If Not (PublishedTaskSearchDV.LastAttemptDate(row) Is Nothing) Then
                'Sridhar strB.AppendFormat(.LabelExecutionInfoA(2), PublishedTaskSearchDV.LastAttemptDate(row).Value.ToString(DATE_TIME_FORMAT_12, System.Threading.Thread.CurrentThread.CurrentCulture))
                strB.AppendFormat(.LabelExecutionInfoA(2), GetLongDate12FormattedString(PublishedTaskSearchDV.LastAttemptDate(row).Value))
            End If

            If Not (PublishedTaskSearchDV.RetryCount(row) Is Nothing) Then
                strB.AppendFormat(.LabelExecutionInfoA(3), PublishedTaskSearchDV.RetryCount(row))
            End If

            If (PublishedTaskSearchDV.FailReason(row).Trim() & "" <> String.Empty) Then
                strB.AppendFormat(.LabelExecutionInfoA(4), PublishedTaskSearchDV.FailReason(row))
            End If
        End With

        Return strB.ToString()

    End Function

    'Public Shared ReadOnly Property ExecutionInfo(ByVal row As DataRow) As String
    '    Get
    '        Dim sb As New StringBuilder
    '        If (Not PublishedTaskSearchDV.MachineName(row) Is Nothing AndAlso PublishedTaskSearchDV.MachineName(row).Trim().Length > 0) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("MACHINE_NAME"), PublishedTaskSearchDV.MachineName(row))
    '        End If
    '        If (Not PublishedTaskSearchDV.LockDate(row) Is Nothing) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("LOCK_DATE"), PublishedTaskSearchDV.LockDate(row).Value.ToString(DATE_TIME_FORMAT_12, System.Threading.Thread.CurrentThread.CurrentCulture))
    '        End If
    '        If (Not PublishedTaskSearchDV.LastAttemptDate(row) Is Nothing AndAlso PublishedTaskSearchDV.LastAttemptDate(row).HasValue) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("LAST_ATTEMPT_DATE"), PublishedTaskSearchDV.LastAttemptDate(row).Value.ToString(DATE_TIME_FORMAT_12, System.Threading.Thread.CurrentThread.CurrentCulture))
    '        End If
    '        If (Not PublishedTaskSearchDV.RetryCount(row) Is Nothing) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("RETRY_COUNT"), PublishedTaskSearchDV.RetryCount(row))
    '        End If
    '        If (Not PublishedTaskSearchDV.FailReason(row) Is Nothing AndAlso PublishedTaskSearchDV.FailReason(row).Trim().Length > 0) Then
    '            If (sb.Length > 0) Then sb.Append("<br />")
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("FAIL_REASON"), PublishedTaskSearchDV.FailReason(row))
    '        End If
    '        Return sb.ToString()
    '    End Get
    'End Property

    Private Function GetEventTaskInfo(ByVal row As DataRow) As String

        Dim valueArray(5) As String
        Dim strTaskStatus As String

        If Me.State.LabelEventTaskInfoStr = String.Empty Then
            Me.State.LabelEventTaskInfoStr = "<U>" & TranslationBase.TranslateLabelOrMessage("EVENT_TYPE") & "</u> : {0}<br/>" _
                                        & "<U>" & TranslationBase.TranslateLabelOrMessage("TASK") & "</u> : {1}<br/>" _
                                        & "<U>" & TranslationBase.TranslateLabelOrMessage("SENDER") & "</u> : {2}<br/>" _
                                        & "<U>" & TranslationBase.TranslateLabelOrMessage("ARGUMENTS") & "</u> : {3}<br/>" _
                                        & "<U>" & TranslationBase.TranslateLabelOrMessage("EVENT_DATE") & "</u> : {4}<br/>" _
                                        & "<U>" & TranslationBase.TranslateLabelOrMessage("TASK STATUS") & "</u> : {5}<br/>"
        End If

        valueArray(0) = PublishedTaskSearchDV.EventType(row)
        valueArray(1) = PublishedTaskSearchDV.Task(row)
        valueArray(2) = PublishedTaskSearchDV.Sender(row)
        valueArray(3) = PublishedTaskSearchDV.Arguments(row)

        If PublishedTaskSearchDV.EventDate(row) Is Nothing Then
            valueArray(4) = ""
        Else
            'Sridhar valueArray(4) = PublishedTaskSearchDV.EventDate(row).Value.ToString(DATE_TIME_FORMAT_12, System.Threading.Thread.CurrentThread.CurrentCulture)
            valueArray(4) = GetLongDate12FormattedString(PublishedTaskSearchDV.EventDate(row).Value)
        End If

        If (PublishedTaskSearchDV.TaskStatusCode(row) = Codes.TASK_STATUS__FAILED) Then
            valueArray(5) = String.Format("<span class=""StatClosed"">{0}</span>", PublishedTaskSearchDV.TaskStatus(row))
        Else
            valueArray(5) = String.Format("<span class=""StatActive"">{0}</span>", PublishedTaskSearchDV.TaskStatus(row))
        End If

        Return String.Format(Me.State.LabelEventTaskInfoStr.ToString, valueArray)
    End Function



    'Public Shared ReadOnly Property EventTaskInfo(ByVal row As DataRow) As String
    '    Get

    '        Dim strTaskStatus As String
    '        Dim sb As StringBuilder

    '        AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("EVENT_TYPE"), PublishedTaskSearchDV.EventType(row))
    '        sb.Append("<br />")

    '        AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("TASK"), PublishedTaskSearchDV.Task(row))
    '        sb.Append("<br />")

    '        AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("SENDER"), PublishedTaskSearchDV.Sender(row))
    '        sb.Append("<br />")

    '        If (Not PublishedTaskSearchDV.Arguments(row) Is Nothing AndAlso PublishedTaskSearchDV.Arguments(row).Trim.Length > 0) Then
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("ARGUMENTS"), PublishedTaskSearchDV.Arguments(row))
    '            sb.Append("<br />")
    '        End If

    '        If PublishedTaskSearchDV.EventDate(row) Is Nothing Then
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("EVENT_DATE"), "")
    '        Else
    '            AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("EVENT_DATE") _
    '                                    , PublishedTaskSearchDV.EventDate(row).Value.ToString(DATE_TIME_FORMAT_12, System.Threading.Thread.CurrentThread.CurrentCulture))
    '        End If

    '        sb.Append("<br />")

    '        AppendDisplayLabelValue(sb, TranslationBase.TranslateLabelOrMessage("TASK_STATUS"), strTaskStatus)

    '        Return sb.ToString()
    '    End Get
    'End Property
#End Region

#Region "Grid Actions"
    Public Sub DeleteTask(ByVal taskId As Guid)
        Try
            PublishedTask.DeleteTask(taskId)
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Reset(ByVal taskId As Guid)
        Try
            PublishedTask.ResetTask(taskId)
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Grid Events"

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        grdPublishTask.PageIndex = NewCurrentPageIndex(grdPublishTask, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
        PopulateGrid()
    End Sub

    Protected Sub grdPublishTask_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPublishTask.PageIndexChanging
        Me.State.PageIndex = e.NewPageIndex
        PopulateGrid()
    End Sub

    Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdPublishTask.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub grdPublishTask_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdPublishTask.RowCommand
        Try
            If e.CommandName = Me.GRID_COMMAND_RESET Then
                PublishedTask.ResetTask(New Guid(e.CommandArgument.ToString()))
            ElseIf e.CommandName = Me.GRID_COMMAND_DELETE Then
                PublishedTask.DeleteTask(New Guid(e.CommandArgument.ToString()))
            End If
            Me.State.SearchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub grdPublishTask_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPublishTask.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim div As HtmlGenericControl

            div = CType(e.Row.Cells(0).FindControl("OriginInfoDiv"), HtmlControl)
            div.InnerHtml = Me.GetOriginInfo(dvRow.Row)

            div = CType(e.Row.Cells(1).FindControl("EventTaskInfoDiv"), HtmlControl)
            'div.InnerHtml = PublishedTaskForm.EventTaskInfo(dvRow.Row)
            div.InnerHtml = Me.GetEventTaskInfo(dvRow.Row)

            div = CType(e.Row.Cells(2).FindControl("ExecutionInfoDiv"), HtmlControl)
            'div.InnerHtml = PublishedTaskForm.ExecutionInfo(dvRow.Row)
            div.InnerHtml = Me.GetExecutionInfo(dvRow.Row)

            Dim lb As LinkButton
            lb = CType(e.Row.Cells(1).FindControl("btnDelete"), LinkButton)

            If (PublishedTaskSearchDV.TaskStatusCode(dvRow.Row) = Codes.TASK_STATUS__IN_PROGRESS) Then
                lb.Visible = False
            Else
                lb.Visible = True
                lb.CommandArgument = PublishedTaskSearchDV.PublishedTaskId(dvRow.Row).ToString()
                lb.CommandName = GRID_COMMAND_DELETE
                lb.Text = TranslationBase.TranslateLabelOrMessage("DELETE")
            End If

            lb = CType(e.Row.Cells(1).FindControl("btnReset"), LinkButton)

            If (PublishedTaskSearchDV.TaskStatusCode(dvRow.Row) = Codes.TASK_STATUS__FAILED) Then
                lb.Visible = True
                lb.CommandArgument = PublishedTaskSearchDV.PublishedTaskId(dvRow.Row).ToString()
                lb.CommandName = GRID_COMMAND_RESET
                lb.Text = TranslationBase.TranslateLabelOrMessage("RESET")
            Else
                lb.Visible = False
            End If
        End If

    End Sub
#End Region


End Class
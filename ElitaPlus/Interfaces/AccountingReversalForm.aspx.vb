Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class AccountingReversalForm
    Inherits ElitaPlusPage
#Region "Constants"


    Public Const URL As String = "AccountingReversalForm.aspx"
    Public Const PAGETITLE As String = "ACCOUNTING_REVERSAL"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const ONE_ITEM As Integer = 1
    Private Const RETURN_SUCCESS As String = "0"


#End Region

#Region "PAGE STATE"

    Class MyState
        Public AcctTransId As Guid
        Public MyObj As AcctTransmission
        Public CompanyObj As Company
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
                Me.AddCalendar(Me.btnDate, Me.txtDate)

                Populate()

                'Add Enable/Disable Code to Radios for the Event and date Lists
                Me.rdoAllDates.Attributes.Add("onclick", "selectDate();")
                Me.rdoGreaterDate.Attributes.Add("onclick", "selectDate();")
                Me.rdoLessDate.Attributes.Add("onclick", "selectDate();")
                Me.rdoSpecificDate.Attributes.Add("onclick", "selectDate();")

                Me.rdoEventAll.Attributes.Add("onclick", "selectEvent();")
                Me.rdoEventSpecific.Attributes.Add("onclick", "selectEvent();")

            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not CallingPar Is Nothing Then
                'Get the id from the parent
                Me.State.AcctTransId = CType(Me.CallingParameters, Guid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "EVENT HANDLERS"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.ReturnToCallingPage()
    End Sub

    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click

        Try
            Dim _felitaEngine As New FelitaEngine()
            Dim dt As Date = Date.MinValue
            Dim dtMove As FelitaEngine.DateFilter
            Dim eventCode As String

            If rdoEventAll.Checked Then
                eventCode = String.Empty
            Else
                If ddlAccountingEvent.SelectedValue = Me.NOTHING_SELECTED Then
                    eventCode = String.Empty
                    rdoEventAll.Checked = True
                    rdoEventSpecific.Checked = False
                Else
                    eventCode = BusinessObjectsNew.LookupListNew.GetCodeFromId(BusinessObjectsNew.LookupListNew.getAccountingEvents(Me.State.CompanyObj.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(ddlAccountingEvent.SelectedValue))
                End If
            End If

            If Not rdoAllDates.Checked Then
                If Not Date.TryParse(Me.txtDate.Text, dt) Then
                    Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_INVALID_EMPTY_DATE)
                    Exit Sub
                End If
            End If

            If rdoAllDates.Checked Then
                dtMove = Nothing
            ElseIf rdoGreaterDate.Checked Then
                dtMove = FelitaEngine.DateFilter.GreaterThan
            ElseIf rdoLessDate.Checked Then
                dtMove = FelitaEngine.DateFilter.LessThan
            ElseIf rdoSpecificDate.Checked Then
                dtMove = FelitaEngine.DateFilter.SpecificDate
            End If

            Dim strReturn As String = _felitaEngine.ReverseAccountingEntries(Me.State.AcctTransId, eventCode, dt, dtMove, Me.txtDescription.Text)
            If strReturn = RETURN_SUCCESS Then
                Me.AddInfoMsg(ElitaPlusWebApp.Message.MSG_INTERFACES_HAS_COMPLETED)
                ControlMgr.SetEnableControl(Me, Me.btnExecute, False)
            Else
                Me.ErrControllerMaster.AddErrorAndShow(strReturn)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "POPULATE"

    Private Sub Populate()

        Try
            'Fill AcctTransmission Record
            Me.State.MyObj = New AcctTransmission(Me.State.AcctTransId, True)

            Me.State.CompanyObj = New Company(Me.State.MyObj.CompanyId)
            'BindListControlToDataView(Me.ddlAccountingEvent, BusinessObjectsNew.LookupListNew.getAccountingEvents(Me.State.CompanyObj.AcctCompanyId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            Dim AccountingEvents As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.AccountingEventByAccountingCompany,
                                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                    context:=New ListContext() With
                                                                    {
                                                                        .AccountingCompanyId = Me.State.CompanyObj.AcctCompanyId
                                                                    })

            Me.ddlAccountingEvent.Populate(AccountingEvents.ToArray(),
                                         New PopulateOptions() With
                                         {
                                             .AddBlankItem = True
                                         })

            Me.txtBatchNumber.Text = Me.State.MyObj.FileName.Substring(Me.State.MyObj.FileName.LastIndexOf("-") + 1).Replace(".XML", "")
            Me.txtCreditAmount.Text = Me.State.MyObj.CreditAmount.ToString
            Me.txtDebitAmount.Text = Me.State.MyObj.DebitAmount.ToString
            Me.txtTransDate.Text = Me.State.MyObj.CreatedDate.ToString

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region



End Class
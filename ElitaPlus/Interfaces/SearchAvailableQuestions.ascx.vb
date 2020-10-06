Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Public Class SearchAvailableQuestions
    Inherits System.Web.UI.UserControl

#Region "Event Declaration"
    Public Delegate Sub SearchFilterEventHandler(sender As Object, Args As SearchAvailableQuestionsEventArgs)
    Public Delegate Sub SaveQuestionsListDetail(sender As Object, Args As SearchAvailableQuestionsEventArgs)
    Public Delegate Sub CancelButtonClicked(sender As Object, args As SearchAvailableQuestionsEventArgs)
    Public Delegate Sub CustomPopulateDropDown(sender As Object, args As SearchAvailableQuestionsEventArgs)

    Public Event ExecuteSearchFilter As SearchFilterEventHandler
    Public Event EventSaveQuestionsListDetail As SaveQuestionsListDetail
    Public Event EventCancelButtonClicked As CancelButtonClicked
    Public Event EventCustomPopulateDropDown As CustomPopulateDropDown
#End Region

#Region "Variables"
    Dim EventParam As SearchAvailableQuestionsEventArgs
    Private _dvAvailableQuestions As DataView
    Private _dvSelectedQuestions As DataView
    Private _ShowCancelButton As Boolean
    Private _ShowUpButton As Boolean
    Private _ShowDownButton As Boolean
#End Region

#Region "Constants"
    Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
    Const COL_NAME_ID As String = "ID"
#End Region

#Region "Properties"

    Public Property dvAvailableQuestions As DataView
        Get
            Return _dvAvailableQuestions
        End Get
        Set(value As DataView)
            _dvAvailableQuestions = value
        End Set
    End Property

    Public Property dvSelectedQuestions As DataView
        Get
            Return _dvSelectedQuestions
        End Get
        Set(value As DataView)
            _dvSelectedQuestions = value
        End Set
    End Property

    Public ReadOnly Property listSelectedQuestions As ArrayList
        Get
            Return UserControlAvailableSelectedQuestionsCodes.SelectedList
        End Get
    End Property

    Public ReadOnly Property Issue As Guid
        Get
            Return New Guid(cboIssue.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property QuestionList As String
        Get
            Return moQuestionList.Text
        End Get
    End Property

    Public ReadOnly Property SearchTags As String
        Get
            Return moSearchTags.Text
        End Get
    End Property

    Public Property ShowCancelButton As Boolean
        Get
            Return _ShowCancelButton
        End Get
        Set(value As Boolean)
            _ShowCancelButton = value
        End Set
    End Property


    Public Property ShowUpButton As Boolean
        Get
            Return _ShowUpButton
        End Get
        Set(value As Boolean)
            _ShowUpButton = value
        End Set
    End Property

    Public Property ShowDownButton As Boolean
        Get
            Return _ShowDownButton
        End Get
        Set(value As Boolean)
            _ShowDownButton = value
        End Set
    End Property

#End Region

#Region "Event Handlers"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GetAvailableEquipment(Me)
        UserControlAvailableSelectedQuestionsCodes.RemoveSelectedFromAvailable()
    End Sub

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        clearControls()
        UserControlAvailableSelectedQuestionsCodes.RemoveSelectedFromAvailable()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim args As New SearchAvailableQuestionsEventArgs(Me)
        RaiseEvent EventSaveQuestionsListDetail(btnSearch, args)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserControlAvailableSelectedQuestionsCodes.BackColor = "#d5d6e4"
        If Not Page.IsPostBack Then
            PopulateuserControl()
            GetAvailableEquipment()
            btnCancel.Visible = ShowCancelButton
            UserControlAvailableSelectedQuestionsCodes.ShowUpButton = ShowUpButton
            UserControlAvailableSelectedQuestionsCodes.ShowDownButton = ShowDownButton
            BindSelected(dvSelectedQuestions)
        End If
        UserControlAvailableSelectedQuestionsCodes.ShowUpButton = True
        UserControlAvailableSelectedQuestionsCodes.ShowDownButton = True
    End Sub
#End Region

#Region "Private Members"
    Private Sub PopulateuserControl()
        'ElitaPlusPage.BindListControlToDataView(cboIssue, LookupListNew.GetIssueLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), COL_NAME_DESCRIPTION, COL_NAME_ID, True)

        Dim IssueTypeList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="IssueTypeList",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        cboIssue.Populate(IssueTypeList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
    End Sub

    Private Sub GetAvailableEquipment(Optional ByVal pControl As SearchAvailableQuestions = Nothing)
        Dim args As New SearchAvailableQuestionsEventArgs(pControl)
        RaiseEvent ExecuteSearchFilter(btnSearch, args)
        If args.dvAvailableQuestions IsNot Nothing Then
            UserControlAvailableSelectedQuestionsCodes.SetAvailableData(args.dvAvailableQuestions, "DESCRIPTION", "ID")
        End If
    End Sub
    Private Sub clearControls()
        cboIssue.SelectedValue = Guid.Empty.ToString
        moQuestionList.Text = String.Empty
        moSearchTags.Text = String.Empty
        GetAvailableEquipment(Me)

    End Sub
    Public Sub BindSelected(dvSelected As DataView)
        If dvSelected IsNot Nothing Then
            With UserControlAvailableSelectedQuestionsCodes
                .SetSelectedData(dvSelected, COL_NAME_DESCRIPTION, COL_NAME_ID, False, False)
                .RemoveSelectedFromAvailable()
            End With
        End If
    End Sub
#End Region

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearControls()
        Dim args As New SearchAvailableQuestionsEventArgs(Me)
        RaiseEvent EventCancelButtonClicked(btnCancel, args)
        BindSelected(dvSelectedQuestions)
    End Sub

End Class

#Region "Event argument Class"
Public Class SearchAvailableQuestionsEventArgs
    Inherits System.EventArgs

#Region "Local Variables"
    Dim _Issue As Guid
    Dim _QuestionList As String
    Dim _SearchTags As String
    Dim _dvResults As DataView
    Dim _selectedQuestions As ArrayList
#End Region

#Region "Constructor"
    Sub New(pObject As SearchAvailableQuestions)
        If pObject IsNot Nothing Then
            With pObject
                Issue = .Issue
                _QuestionList = .QuestionList
                _SearchTags = .SearchTags
                listSelectedQuestions = .listSelectedQuestions
            End With
        End If
    End Sub
#End Region

#Region "Properties"
    Public Property Issue As Guid
        Get
            Return _Issue
        End Get
        Set(value As Guid)
            _Issue = value
        End Set
    End Property
    Public Property QuestionList As String
        Get
            Return _QuestionList
        End Get
        Set(value As String)
            _QuestionList = value
        End Set
    End Property
    Public Property SearchTags As String
        Get
            Return _SearchTags
        End Get
        Set(value As String)
            _SearchTags = value
        End Set
    End Property

    Public Property dvAvailableQuestions As DataView
        Get
            Return _dvResults
        End Get
        Set(value As DataView)
            _dvResults = value
        End Set
    End Property

    Public Property listSelectedQuestions As ArrayList
        Get
            Return _selectedQuestions
        End Get
        Set(value As ArrayList)
            _selectedQuestions = value
        End Set
    End Property

#End Region

End Class

#End Region
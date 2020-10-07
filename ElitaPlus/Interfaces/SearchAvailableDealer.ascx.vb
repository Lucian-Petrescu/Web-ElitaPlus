Public Class SearchAvailableDealer
    Inherits System.Web.UI.UserControl

#Region "Event Declaration"
    Public Delegate Sub SearchFilterEventHandler(sender As Object, Args As SearchAvailableDealerEventArgs)
    Public Delegate Sub SaveDealerListDetail(sender As Object, Args As SearchAvailableDealerEventArgs)
    Public Delegate Sub CancelButtonClicked(sender As Object, args As SearchAvailableDealerEventArgs)
    Public Delegate Sub CustomPopulateDropDown(sender As Object, args As SearchAvailableDealerEventArgs)

    Public Event ExecuteDealerSearchFilter As SearchFilterEventHandler
    Public Event EventSaveDealerListDetail As SaveDealerListDetail
    Public Event EventCancelButtonClicked As CancelButtonClicked
    Public Event EventCustomPopulateDropDown As CustomPopulateDropDown
#End Region

#Region "Variables"
    Dim EventParam As SearchAvailableDealerEventArgs
    Private _dvAvailableDealer As DataView
    Private _dvSelectedDealer As DataView
    Private _ShowCancelButton As Boolean
    Private _ShowUpButton As Boolean
    Private _ShowDownButton As Boolean
#End Region

#Region "Constants"
    Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
    Const COL_NAME_ID As String = "ID"
#End Region

#Region "Properties"

    Public Property dvAvailableDealer As DataView
        Get
            Return _dvAvailableDealer
        End Get
        Set(value As DataView)
            _dvAvailableDealer = value
        End Set
    End Property

    Public Property dvSelectedDealer As DataView
        Get
            Return _dvSelectedDealer
        End Get
        Set(value As DataView)
            _dvSelectedDealer = value
        End Set
    End Property

    Public ReadOnly Property listSelectedDealer As ArrayList
        Get
            Return UserControlAvailableSelectedDealerCodes.SelectedList
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim args As New SearchAvailableDealerEventArgs(Me)
        RaiseEvent EventSaveDealerListDetail(btnSave, args)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserControlAvailableSelectedDealerCodes.BackColor = "#d5d6e4"
        If Not Page.IsPostBack Then
            PopulateuserControl()
            GetAvailableDealer()
            btnCancel.Visible = ShowCancelButton
            UserControlAvailableSelectedDealerCodes.ShowUpButton = ShowUpButton
            UserControlAvailableSelectedDealerCodes.ShowDownButton = ShowDownButton
            BindSelected(dvSelectedDealer)
        End If
    End Sub
#End Region

#Region "Private Members"
    Private Sub PopulateuserControl()
    End Sub

    Private Sub GetAvailableDealer(Optional ByVal pControl As SearchAvailableDealer = Nothing)
        Dim args As New SearchAvailableDealerEventArgs(pControl)
        RaiseEvent ExecuteDealerSearchFilter(btnSearch, args)
        If args.dvAvailableDealer IsNot Nothing Then
            UserControlAvailableSelectedDealerCodes.SetAvailableData(args.dvAvailableDealer, "DESCRIPTION", "ID")
        End If
    End Sub

    Private Sub clearControls()
        GetAvailableDealer(Me)
    End Sub

    Public Sub BindSelected(dvSelected As DataView)
        If dvSelected IsNot Nothing Then
            With UserControlAvailableSelectedDealerCodes
                .SetSelectedData(dvSelected, COL_NAME_DESCRIPTION, COL_NAME_ID)
                .RemoveSelectedFromAvailable()
            End With
        End If
    End Sub
#End Region

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearControls()
        Dim args As New SearchAvailableDealerEventArgs(Me)
        RaiseEvent EventCancelButtonClicked(btnCancel, args)
        BindSelected(dvSelectedDealer)
    End Sub

End Class

#Region "Event argument Class"
Public Class SearchAvailableDealerEventArgs
    Inherits System.EventArgs

#Region "Local Variables"
    Dim _dvResults As DataView
    Dim _selectedDealer As ArrayList
#End Region

#Region "Constructor"
    Sub New(pObject As SearchAvailableDealer)
        If pObject IsNot Nothing Then
            With pObject
                listSelectedDealer = .listSelectedDealer
            End With
        End If
    End Sub
#End Region

#Region "Properties"
    Public Property dvAvailableDealer As DataView
        Get
            Return _dvResults
        End Get
        Set(value As DataView)
            _dvResults = value
        End Set
    End Property

    Public Property listSelectedDealer As ArrayList
        Get
            Return _selectedDealer
        End Get
        Set(value As ArrayList)
            _selectedDealer = value
        End Set
    End Property

#End Region

End Class

#End Region
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Public Class SearchAvailableSelected
    Inherits System.Web.UI.UserControl

#Region "Event Declaration"
    Public Delegate Sub SearchFilterEventHandler(sender As Object, Args As SearchAvailableSelectedEventArgs)
    Public Delegate Sub SaveEquipmentListDetail(sender As Object, Args As SearchAvailableSelectedEventArgs)
    Public Delegate Sub CancelButtonClicked(sender As Object, args As SearchAvailableSelectedEventArgs)
    Public Delegate Sub CustomPopulateDropDown(sender As Object, args As SearchAvailableSelectedEventArgs)

    Public Event ExecuteSearchFilter As SearchFilterEventHandler
    Public Event EventSaveEquipmentListDetail As SaveEquipmentListDetail
    Public Event EventCancelButtonClicked As CancelButtonClicked
    Public Event EventCustomPopulateDropDown As CustomPopulateDropDown
#End Region

#Region "Variables"
    Dim EventParam As SearchAvailableSelectedEventArgs
    Private _dvAvailableEquipment As DataView
    Private _dvSelectedEquipment As DataView
    Private _ShowCancelButton As Boolean

#End Region

#Region "Constants"
    Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
    Const COL_NAME_ID As String = "ID"
#End Region

#Region "Properties"

    Public Property dvAvailableEquipment As DataView
        Get
            Return _dvAvailableEquipment
        End Get
        Set(value As DataView)
            _dvAvailableEquipment = value
        End Set
    End Property

    Public Property dvSelectedEquipment As DataView
        Get
            Return _dvSelectedEquipment
        End Get
        Set(value As DataView)
            _dvSelectedEquipment = value
        End Set
    End Property

    Public ReadOnly Property listSelectedEquipment As ArrayList
        Get
            Return UserControlAvailableSelectedEquipmentCodes.SelectedList
        End Get
    End Property

    Public ReadOnly Property ManufactorerID As Guid
        Get
            Return New Guid(cboMake.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property EquipmentClass As Guid
        Get
            Return New Guid(cboEquipmentClass.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property EquipmentType As Guid
        Get
            Return New Guid(cboEquipmenttype.SelectedValue)
        End Get
    End Property

    Public ReadOnly Property Model As String
        Get
            Return txtModel.Text
        End Get
    End Property

    Public ReadOnly Property Description As String
        Get
            Return txtDescription.Text
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
#End Region

#Region "Event Handlers"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GetAvailableEquipment(Me)
        UserControlAvailableSelectedEquipmentCodes.RemoveSelectedFromAvailable()
    End Sub

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        clearControls()
        UserControlAvailableSelectedEquipmentCodes.RemoveSelectedFromAvailable()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim args As New SearchAvailableSelectedEventArgs(Me)
        RaiseEvent EventSaveEquipmentListDetail(btnSearch, args)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserControlAvailableSelectedEquipmentCodes.BackColor = "#d5d6e4"
        If Not Page.IsPostBack Then
            PopulateuserControl()
            GetAvailableEquipment()
            btnCancel.Visible = ShowCancelButton
            BindSelected(dvSelectedEquipment)
        End If

    End Sub
#End Region

#Region "Private Members"
    Private Sub PopulateuserControl()
        'ElitaPlusPage.BindListControlToDataView(cboMake, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), COL_NAME_DESCRIPTION, COL_NAME_ID, True)

        ' New
        Dim Manufacturer As DataElements.ListItem() =
            CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup",
                                                            context:=New ListContext() With
                                                            {
                                                              .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                            })

        cboMake.Populate(Manufacturer.ToArray(),
                                    New PopulateOptions() With
                                    {
                                     .AddBlankItem = True
                                    })


        'ElitaPlusPage.BindListControlToDataView(cboEquipmentClass, LookupListNew.GetEquipmentClassLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), COL_NAME_DESCRIPTION, COL_NAME_ID, True)

        Dim Equipments As DataElements.ListItem() =
            CommonConfigManager.Current.ListManager.GetList(listCode:="EQPCLS",
                                                            languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        cboEquipmentClass.Populate(Equipments.ToArray(),
                                    New PopulateOptions() With
                                    {
                                     .AddBlankItem = True
                                    })

        Dim args As New SearchAvailableSelectedEventArgs(Nothing)
        RaiseEvent EventCustomPopulateDropDown(cboEquipmenttype, args)
        'ElitaPlusPage.BindListControlToDataView(cboEquipmenttype, args.dvmakeList, COL_NAME_DESCRIPTION, COL_NAME_ID, True)

        Dim EquipmentType As DataElements.ListItem() =
            CommonConfigManager.Current.ListManager.GetList(listCode:="EQPTYPE",
                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        cboEquipmenttype.Populate(EquipmentType.ToArray(),
                            New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })
    End Sub

    Private Sub GetAvailableEquipment(Optional ByVal pControl As SearchAvailableSelected = Nothing)
        Dim args As New SearchAvailableSelectedEventArgs(pControl)
        RaiseEvent ExecuteSearchFilter(btnSearch, args)
        If args.dvAvailableEquipment IsNot Nothing Then
            UserControlAvailableSelectedEquipmentCodes.SetAvailableData(args.dvAvailableEquipment, "DESCRIPTION", "ID")
        End If
    End Sub
    Private Sub clearControls()
        cboMake.SelectedValue = Guid.Empty.ToString
        cboEquipmentClass.SelectedValue = Guid.Empty.ToString
        cboEquipmenttype.SelectedValue = Guid.Empty.ToString
        txtModel.Text = ""
        txtDescription.Text = ""
        GetAvailableEquipment(Me)

    End Sub
    Public Sub BindSelected(dvSelected As DataView)
        If dvSelected IsNot Nothing Then
            With UserControlAvailableSelectedEquipmentCodes
                .SetSelectedData(dvSelected, COL_NAME_DESCRIPTION, COL_NAME_ID)
                .RemoveSelectedFromAvailable()
            End With
        End If
    End Sub
#End Region

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearControls()
        Dim args As New SearchAvailableSelectedEventArgs(Me)
        RaiseEvent EventCancelButtonClicked(btnCancel, args)
        BindSelected(dvSelectedEquipment)
    End Sub

End Class

#Region "Event argument Class"
Public Class SearchAvailableSelectedEventArgs
    Inherits System.EventArgs

#Region "Local Variables"
    Dim _manufactorerID As Guid
    Dim _model As String
    Dim _description As String
    Dim _equipmentClass As Guid
    Dim _equipmentType As Guid
    Dim _dvResults As DataView
    Dim _selectedEquipment As ArrayList
    Dim _dvmakeList As DataView
#End Region

#Region "Constructor"
    Sub New(pObject As SearchAvailableSelected)
        If pObject IsNot Nothing Then
            With pObject
                ManufactorerID = .ManufactorerID
                EquipmentClass = .EquipmentClass
                EquipmentType = .EquipmentType
                Model = .Model
                Description = .Description
                listSelectedEquipment = .listSelectedEquipment
            End With
        End If
    End Sub
#End Region

#Region "Properties"
    Public Property ManufactorerID As Guid
        Get
            Return _manufactorerID
        End Get
        Set(value As Guid)
            _manufactorerID = value
        End Set
    End Property
    Public Property Model As String
        Get
            Return _model
        End Get
        Set(value As String)
            _model = value
        End Set
    End Property
    Public Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property
    Public Property EquipmentClass As Guid
        Get
            Return _equipmentClass
        End Get
        Set(value As Guid)
            _equipmentClass = value
        End Set
    End Property
    Public Property EquipmentType As Guid
        Get
            Return _equipmentType
        End Get
        Set(value As Guid)
            _equipmentType = value
        End Set
    End Property

    Public Property dvAvailableEquipment As DataView
        Get
            Return _dvResults
        End Get
        Set(value As DataView)
            _dvResults = value
        End Set
    End Property

    Public Property listSelectedEquipment As ArrayList
        Get
            Return _selectedEquipment
        End Get
        Set(value As ArrayList)
            _selectedEquipment = value
        End Set
    End Property

    Public Property dvmakeList As DataView
        Get
            Return _dvmakeList
        End Get
        Set(value As DataView)
            _dvmakelist = value
        End Set
    End Property
#End Region

End Class

#End Region
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common


Partial Class UserControlPoliceReport
    Inherits System.Web.UI.UserControl

    Protected WithEvents moPoliceMultipleColumnDropControl As MultipleColumnDDLabelControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Private Const LABEL_POLICE_STATION As String = "POLICE_STATION"
#End Region

#Region "Variables"
    Private moIsStarNeededforDualDrop As Boolean = False
#End Region

#Region "State"
    Class MyState
        Public myPoliceReport As PoliceReport
        Public ErrControllerId As String
        Public SelectedPoliceStationId As Guid = Guid.Empty
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property

#End Region

#Region "Properties"

    Public ReadOnly Property PoliceMultipleDrop() As MultipleColumnDDLabelControl
        Get
            If moPoliceMultipleColumnDropControl Is Nothing Then
                moPoliceMultipleColumnDropControl = CType(FindControl("mPoliceMultipleColumnDropControl"), MultipleColumnDDLabelControl)
            End If
            Return moPoliceMultipleColumnDropControl
        End Get
    End Property

    Private ReadOnly Property ErrCtrl() As ErrorController
        Get
            If State.ErrControllerId IsNot Nothing Then
                Return CType(Page.FindControl(State.ErrControllerId), ErrorController)
            End If
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

    Public Property IsStarNeededforDualDrop() As Boolean
        Get
            Return moIsStarNeededforDualDrop
        End Get
        Set(Value As Boolean)
            moIsStarNeededforDualDrop = Value
        End Set
    End Property

#End Region

#Region "Page Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        'If Not Me.IsPostBack AndAlso Not Me.State.myPoliceReport Is Nothing Then
        '    PopulatePoliceStationDropDown(moIsStarNeededforDualDrop)
        'End If

        If State.myPoliceReport IsNot Nothing Then
            BindBoPropertiesToLabels()
            'Else
            '    SetTheRequiredFields()
        End If

    End Sub

    Public Sub SetTheRequiredFields()
        If LabelReportNumber.Text.IndexOf("*") <> 0 Then LabelReportNumber.Text = "* " & LabelReportNumber.Text
        'If LabelOfficerName.Text.IndexOf("*") <> 0 Then Me.LabelOfficerName.Text = "* " & Me.LabelOfficerName.Text
    End Sub
    Public Sub Bind(PoliceReportBo As PoliceReport, containerErrorController As ErrorController, Optional ByVal starneeded As Boolean = False, Optional ByVal bypassdualdropinitialization As Boolean = False)
        With State
            .ErrControllerId = containerErrorController.ID
            .myPoliceReport = PoliceReportBo
        End With
        TextboxOfficerName.Text = String.Empty
        TextboxReportNumber.Text = String.Empty
        'Me.IsStarNeededforDualDrop = starneeded
        moIsStarNeededforDualDrop = starneeded
        PopulateControlFromBo(bypassdualdropinitialization)
        Page.ShowMissingTranslations(ErrCtrl)
    End Sub
    Public Sub ReAssignTabIndex(Optional ByVal TabIndexStartingNumber As Int16 = 0)
        If TabIndexStartingNumber > 0 Then
            'Me.moPoliceMultipleColumnDropControl.TabIndex = TabIndexStartingNumber
            TextboxReportNumber.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            TextboxOfficerName.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
        End If
    End Sub
    Protected Sub BindBoPropertiesToLabels()
        Page.BindBOPropertyToLabel(State.myPoliceReport, "ReportNumber", LabelReportNumber)
        Page.BindBOPropertyToLabel(State.myPoliceReport, "OfficerName", LabelOfficerName)
        'Me.LabelDummy.Text = TranslationBase.TranslateLabelOrMessage(LABEL_POLICE_STATION)
        Page.BindBOPropertyToLabel(State.myPoliceReport, "PoliceStationId", PoliceMultipleDrop.CaptionLabel)
        'Me.Page.BindBOPropertyToLabel(Me.State.myPoliceReport, "PoliceStationId", LabelDummy) 'PoliceMultipleDrop.CaptionLabel)
        Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Public Sub ChangeEnabledControlProperty(blnEnabledState As Boolean)
        Page.ChangeEnabledControlProperty(TextboxReportNumber, blnEnabledState)
        Page.ChangeEnabledControlProperty(TextboxOfficerName, blnEnabledState)
        moPoliceMultipleColumnDropControl.ChangeEnabledControlProperty(blnEnabledState)
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False)
        If State.myPoliceReport IsNot Nothing Then
            With State.myPoliceReport
                BindBoPropertiesToLabels()
                Page.PopulateBOProperty(State.myPoliceReport, "ReportNumber", TextboxReportNumber)
                Page.PopulateBOProperty(State.myPoliceReport, "OfficerName", TextboxOfficerName)
                State.myPoliceReport.PoliceStationId = PoliceMultipleDrop.SelectedGuid

                If Page.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
                '         Dim blnExcludeSave As Boolean
                ' do NOT save the record in the database if the mandatory fields are not entered
                'If Not blnExcludeSave Then .Save()

                'If Not Me.isempty Then
                '.Save()
                'End If

            End With
        End If
    End Sub

#End Region

#Region "Public"
    Public Function isempty() As Boolean
        If (TextboxReportNumber.Text = String.Empty) OrElse _
            (moPoliceMultipleColumnDropControl.SelectedGuid.Equals(System.Guid.Empty)) Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Private"

    Private Sub PopulateControlFromBo(Optional ByVal bypassdualdropinitialization As Boolean = False)
        If Not bypassdualdropinitialization Then
            PopulatePoliceStationDropDown(moIsStarNeededforDualDrop)
        End If

        If State.myPoliceReport IsNot Nothing Then
            With State.myPoliceReport
                Page.PopulateControlFromBOProperty(TextboxReportNumber, .ReportNumber)
                Page.PopulateControlFromBOProperty(TextboxOfficerName, .OfficerName)
                If .PoliceStationId.Equals(System.Guid.Empty) Then
                    PoliceMultipleDrop.SelectedGuid = System.Guid.Empty
                    'Else
                    '   PoliceMultipleDrop.SelectedGuid = System.Guid.Empty
                    '  PoliceMultipleDrop.SelectedGuid = .PoliceStationId
                End If
            End With
        End If
    End Sub

    Private Sub PopulatePoliceStationDropDown(isstarneeded As Boolean)

        Dim dv As DataView = LookupListNew.GetPoliceLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
        PoliceMultipleDrop.NothingSelected = True
        Dim lblCaption As String
        If isstarneeded Then
            'added the following spaces in the caption to align it with the stand alone police report form
            lblCaption = "&nbsp;&nbsp;" & "* " & TranslationBase.TranslateLabelOrMessage(LABEL_POLICE_STATION)
        Else
            'added the following spaces in the caption to align it with the new claim screen
            lblCaption = "&nbsp;&nbsp; &nbsp;" & TranslationBase.TranslateLabelOrMessage(LABEL_POLICE_STATION)
        End If
        PoliceMultipleDrop.SetControl(False, _
                                          PoliceMultipleDrop.MODES.NEW_MODE, _
                                          True, _
                                          dv, _
                                          lblCaption, _
                                          True, , , _
                                          "mcUserControlPoliceReport_mPoliceMultipleColumnDropControl_moMultipleColumnDrop", _
                                          "mcUserControlPoliceReport_mPoliceMultipleColumnDropControl_moMultipleColumnDropDesc", _
                                          "mcUserControlPoliceReport_mPoliceMultipleColumnDropControl_lb_DropDown")
        If Not State.myPoliceReport.PoliceStationId.Equals(Guid.Empty) Then
            'PoliceMultipleDrop.SelectedGuid = System.Guid.Empty
            PoliceMultipleDrop.SelectedGuid = State.myPoliceReport.PoliceStationId
        End If

    End Sub
#End Region



End Class

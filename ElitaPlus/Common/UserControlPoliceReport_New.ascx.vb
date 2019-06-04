Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common


Partial Class UserControlPoliceReport_New
    Inherits System.Web.UI.UserControl

    Protected WithEvents moPoliceMultipleColumnDropControl As MultipleColumnDDLabelControl_New

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property

#End Region

#Region "Properties"

    Public ReadOnly Property PoliceMultipleDrop() As MultipleColumnDDLabelControl_New
        Get
            If moPoliceMultipleColumnDropControl Is Nothing Then
                moPoliceMultipleColumnDropControl = CType(FindControl("mPoliceMultipleColumnDropControl"), MultipleColumnDDLabelControl_New)
            End If
            Return moPoliceMultipleColumnDropControl
        End Get
    End Property

    Private ReadOnly Property ErrCtrl() As MessageController
        Get
            If Not Me.State.ErrControllerId Is Nothing Then
                Return CType(Me.Page.MasterPage.FindControl(Me.State.ErrControllerId), MessageController)
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
        Set(ByVal Value As Boolean)
            moIsStarNeededforDualDrop = Value
        End Set
    End Property

#End Region

#Region "Page Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        'If Not Me.IsPostBack AndAlso Not Me.State.myPoliceReport Is Nothing Then
        '    PopulatePoliceStationDropDown(moIsStarNeededforDualDrop)
        'End If

        If Not Me.State.myPoliceReport Is Nothing Then
            BindBoPropertiesToLabels()
            'Else
            '    SetTheRequiredFields()
        End If

    End Sub

    Public Sub SetTheRequiredFields()
        If LabelReportNumber.Text.IndexOf("*") <> 0 Then Me.LabelReportNumber.Text = "* " & Me.LabelReportNumber.Text
        'If LabelOfficerName.Text.IndexOf("*") <> 0 Then Me.LabelOfficerName.Text = "* " & Me.LabelOfficerName.Text
    End Sub
    Public Sub Bind(ByVal PoliceReportBo As PoliceReport, ByVal containerErrorController As MessageController, Optional ByVal starneeded As Boolean = False, Optional ByVal bypassdualdropinitialization As Boolean = False)
        With State
            .ErrControllerId = containerErrorController.ID
            .myPoliceReport = PoliceReportBo
        End With
        Me.TextboxOfficerName.Text = String.Empty
        Me.TextboxReportNumber.Text = String.Empty
        'Me.IsStarNeededforDualDrop = starneeded
        Me.moIsStarNeededforDualDrop = starneeded
        Me.PopulateControlFromBo(bypassdualdropinitialization)
        Me.Page.ShowMissingTranslations(Me.ErrCtrl)
    End Sub
    Public Sub ReAssignTabIndex(Optional ByVal TabIndexStartingNumber As Int16 = 0)
        If TabIndexStartingNumber > 0 Then
            'Me.moPoliceMultipleColumnDropControl.TabIndex = TabIndexStartingNumber
            Me.TextboxReportNumber.TabIndex = CType(TabIndexStartingNumber + 1, Int16)
            Me.TextboxOfficerName.TabIndex = CType(TabIndexStartingNumber + 2, Int16)
        End If
    End Sub
    Protected Sub BindBoPropertiesToLabels()
        Me.Page.BindBOPropertyToLabel(Me.State.myPoliceReport, "ReportNumber", LabelReportNumber)
        Me.Page.BindBOPropertyToLabel(Me.State.myPoliceReport, "OfficerName", LabelOfficerName)
        'Me.LabelDummy.Text = TranslationBase.TranslateLabelOrMessage(LABEL_POLICE_STATION)
        Me.Page.BindBOPropertyToLabel(Me.State.myPoliceReport, "PoliceStationId", PoliceMultipleDrop.CaptionLabel)
        'Me.Page.BindBOPropertyToLabel(Me.State.myPoliceReport, "PoliceStationId", LabelDummy) 'PoliceMultipleDrop.CaptionLabel)
        Me.Page.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Public Sub ChangeEnabledControlProperty(ByVal blnEnabledState As Boolean)
        Page.ChangeEnabledControlProperty(Me.TextboxReportNumber, blnEnabledState)
        Page.ChangeEnabledControlProperty(Me.TextboxOfficerName, blnEnabledState)
        Me.moPoliceMultipleColumnDropControl.ChangeEnabledControlProperty(blnEnabledState)
    End Sub

    Public Sub PopulateBOFromControl(Optional ByVal blnExcludeSave As Boolean = False)
        If Not Me.State.myPoliceReport Is Nothing Then
            With Me.State.myPoliceReport
                Me.BindBoPropertiesToLabels()
                Me.Page.PopulateBOProperty(Me.State.myPoliceReport, "ReportNumber", TextboxReportNumber)
                Me.Page.PopulateBOProperty(Me.State.myPoliceReport, "OfficerName", TextboxOfficerName)
                Me.State.myPoliceReport.PoliceStationId = PoliceMultipleDrop.SelectedGuid

                If Me.Page.ErrCollection.Count > 0 Then
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
        If (Me.TextboxReportNumber.Text = String.Empty) OrElse _
            (Me.moPoliceMultipleColumnDropControl.SelectedGuid.Equals(System.Guid.Empty)) Then
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

        If Not Me.State.myPoliceReport Is Nothing Then
            With Me.State.myPoliceReport
                Me.Page.PopulateControlFromBOProperty(Me.TextboxReportNumber, .ReportNumber)
                Me.Page.PopulateControlFromBOProperty(Me.TextboxOfficerName, .OfficerName)
                If .PoliceStationId.Equals(System.Guid.Empty) Then
                    PoliceMultipleDrop.SelectedGuid = System.Guid.Empty
                    'Else
                    '   PoliceMultipleDrop.SelectedGuid = System.Guid.Empty
                    '  PoliceMultipleDrop.SelectedGuid = .PoliceStationId
                End If
            End With
        End If
    End Sub

    Private Sub PopulatePoliceStationDropDown(ByVal isstarneeded As Boolean)

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
        If Not Me.State.myPoliceReport.PoliceStationId.Equals(Guid.Empty) Then
            'PoliceMultipleDrop.SelectedGuid = System.Guid.Empty
            PoliceMultipleDrop.SelectedGuid = Me.State.myPoliceReport.PoliceStationId
        End If

    End Sub
#End Region



End Class
